(function () {
    const converter = new showdown.Converter({
        smoothLivePreview: true
    });

    ko.bindingHandlers["markdown-viewer"] = {
        update(element, valueAccessor, allBindings, viewModel, bindingContext) {
            const value = ko.unwrap(valueAccessor());
            element.innerHTML = converter.makeHtml(value);
        }
    }
})();