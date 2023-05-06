import { easepick } from '@easepick/bundle';

ko.bindingHandlers["easepick"] = {
    init(element: HTMLElement, valueAccessor: () => any, allBindings: KnockoutAllBindingsAccessor, viewModel: any, bindingContext: KnockoutBindingContext) {

        const picker = new easepick.create({
            element: element,
            css: ['https://cdn.jsdelivr.net/npm/@easepick/bundle@1.2.1/dist/index.css'],
            lang: dotvvm.getCulture()
        });
        picker.on("select", e => {
            const prop = valueAccessor();
            if (ko.isWriteableObservable(prop)) {
                prop(dotvvm.serialization.serializeDate(e.detail.date, false));
                element.dispatchEvent(new Event("change"));
            }
        });
        element["easepick"] = picker;

    },
    update(element: HTMLElement, valueAccessor: () => any, allBindings: KnockoutAllBindingsAccessor, viewModel: any, bindingContext: KnockoutBindingContext) {

        const picker = element["easepick"] as easepick.Core;
        let value = ko.unwrap(valueAccessor());

        if (typeof value === "string") {
            value = dotvvm.serialization.parseDate(value, false);
        }
        if (value instanceof Date) {
            picker.setDate(value);
        } else {
            picker.clear();
        }

    }
}