dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["loadingAnimation"] = function LoadingAnimationPostBackHandler(options) {

        var loadingIconCssClass = options.loadingIconCssClass;
        var buttonCssClass = options.buttonCssClass;

        function startAnimation(sender) {
            var icon = null;

            // create icon
            if (loadingIconCssClass) {
                icon = document.createElement("i");
                icon.className = loadingIconCssClass;
                sender.appendChild(icon);
            }

            // set button CSS class
            if (buttonCssClass) {
                sender.classList.add(buttonCssClass);
            }

            return icon;
        }

        function stopAnimation(sender, icon) {
            if (icon) {
                sender.removeChild(icon);
            }

            if (buttonCssClass) {
                sender.classList.remove(buttonCssClass);
            }
        }

        return {
            execute: function (callback, args) {

                var icon = startAnimation(args.sender);

                return callback()
                    .then(function(result) {
                        stopAnimation(args.sender, icon);
                        return result;
                    },
                    function(result) {
                        stopAnimation(args.sender, icon);
                        return result;
                    });
            }
        };
    };
});