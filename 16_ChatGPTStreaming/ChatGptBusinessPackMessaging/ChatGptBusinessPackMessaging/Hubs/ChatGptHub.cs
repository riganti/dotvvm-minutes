using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.SignalR;

namespace ChatGptBusinessPackMessaging.Hubs
{
    public class ChatGptHub : Hub
    {
        public const string ModelName = "gpt-35-turbo";

        private readonly OpenAIClient openAiClient;
        private readonly ConcurrentDictionary<Guid, ChatCompletionsOptions> conversations = new();

        public ChatGptHub(OpenAIClient openAiClient)
        {
            this.openAiClient = openAiClient;
        }

        public override Task OnConnectedAsync()
        {
            // TODO: you should add a limit on the max number of conversations allowed for some reasonable time period

            Groups.AddToGroupAsync(Context.ConnectionId, GetCurrentConversationId().ToString().ToLower());

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            message = message.Trim();

            await Clients.Group(GetCurrentConversationId().ToString().ToLower())
                .SendAsync("MessageStarted");

            // find the conversation
            var conversation = GetOrCreateCurrentConversation();
            conversation.Messages.Add(new ChatRequestUserMessage(message));

            try
            {
                if (message.Length > 200)
                {
                    throw new Exception("Sorry, your message is too long.");
                }
                // TODO: you should perform additional validations on the max conversation length etc.

                // get response from ChatGPT
                var response = await openAiClient.GetChatCompletionsStreamingAsync(conversation);

                // stream the chunks of the message
                var replyBuilder = new StringBuilder();
                await foreach (var chunk in response.EnumerateValues())
                {
                    if (chunk.ContentUpdate == null)
                    {
                        continue;
                    }

                    await Clients.Group(GetCurrentConversationId().ToString().ToLower())
                        .SendAsync("NewMessageChunk", chunk.ContentUpdate);

                    replyBuilder.Append(chunk.ContentUpdate);
                }

                // store the message in the cache
                conversation.Messages.Add(new ChatRequestAssistantMessage(replyBuilder.ToString()));
            }
            catch
            {
                await Clients.Group(GetCurrentConversationId().ToString().ToLower())
                    .SendAsync("NewMessageChunk", "Sorry, there was a problem calling the API.");
            }

            await Clients.Group(GetCurrentConversationId().ToString().ToLower())
                .SendAsync("MessageCompleted");
        }

        private Guid GetCurrentConversationId()
        {
            return Guid.Parse(Context.GetHttpContext().Request.Query["conversationId"]);
        }

        private ChatCompletionsOptions GetOrCreateCurrentConversation()
        {
            return conversations.GetOrAdd(GetCurrentConversationId(), _ =>
            {
                var conversation = new ChatCompletionsOptions()
                {
                    DeploymentName = ModelName
                };
                conversation.Messages.Add(new ChatRequestSystemMessage(
                    """
                           You are a chatbot which answers questions about the DotVVM framework.
                           If you don't know the anwer, point the user to the documentation at https://www.dotvvm.com/docs
                           """));
                return conversation;
            });
        }
    }
}
