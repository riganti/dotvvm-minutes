export default context => new BackgroundOperationsModule(context);

class BackgroundOperationsModule{

    constructor(context) {
        const operationId = ko.dataFor(context.elements[0]).OperationId();

        this.connection = new signalR.HubConnectionBuilder().withUrl("/hubs/BackgroundOperations").build();
        this.connection.on("reportProgress", args => {
            const progress = args[0];
            const viewModelObservable = ko.contextFor(context.elements[0]).$rawData;

            viewModelObservable.patchState({
                Description: progress.description,
                Percent: progress.percent,
                IsCompleted: progress.isCompleted,
                IsError: progress.isError
            });
        });
        this.connection.start().then(() => {
            this.connection.invoke("subscribe", operationId);
        });
    }

}