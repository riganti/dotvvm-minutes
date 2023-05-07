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
            conversation.Messages.Add(new ChatMessage(ChatRole.User, message));

            try
            {
                if (message.Length > 200)
                {
                    throw new Exception("Sorry, your message is too long.");
                }
                // TODO: you should perform additional validations on the max conversation length etc.

                // get response from ChatGPT
                var response = await openAiClient.GetChatCompletionsStreamingAsync(ModelName, conversation);

                // get first choice
                var enumerator = response.Value.GetChoicesStreaming().GetAsyncEnumerator();
                if (!await enumerator.MoveNextAsync())
                {
                    throw new Exception("No choices returned.");
                }
                var choice = enumerator.Current;
                    
                // stream the chunks of the message
                var replyBuilder = new StringBuilder();
                await foreach (var chunk in choice.GetMessageStreaming())
                {
                    if (chunk.Content == null)
                    {
                        continue;
                    }

                    await Clients.Group(GetCurrentConversationId().ToString().ToLower())
                        .SendAsync("NewMessageChunk", chunk.Content);
                    
                    replyBuilder.Append(chunk.Content);
                }

                // store the message in the cache
                conversation.Messages.Add(new ChatMessage(ChatRole.Assistant, replyBuilder.ToString()));
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
                var conversation = new ChatCompletionsOptions();
                conversation.Messages.Add(new ChatMessage(ChatRole.System, """
                    You are a chatbot which answers questions about the DotVVM framework. If you don't know the answer.
                    If you don't know the anwer, point the user to the documentation at https://www.dotvvm.com/docs
                    """));
                return conversation;
            });
        }
    }
}
