ko.bindingHandlers["dotvvm-formhelpers-defaultbuttoncontainer"] = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element).keyup(function (e) {
            var buttons = [];
            if (e.which === 13) {
                buttons = $(element).find("*[data-dotvvm-formhelpers-defaultbutton=true]");
            } else if (e.which === 27) {
                buttons = $(element).find("*[data-dotvvm-formhelpers-cancelbutton=true]");
            }

            if (buttons.length > 0) {
                $(e.target).blur();
                $(buttons[0]).click();
                e.stopPropagation();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    }
};