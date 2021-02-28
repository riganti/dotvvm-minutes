dotvvm.events.init.subscribe(function () {

    dotvvm.postbackHandlers["unsavedChangedNotice"] = function UnsavedChangesNoticePostbackHandler(options) {

        return {
            execute: function(callback) {
                return new Promise(function(resolve, reject) {

                    var hasUnsavedChanges = ko.unwrap(options.hasUnsavedChangesBinding);
                    if (hasUnsavedChanges) {

                        // if there are unsaved changes, show the dialog
                        showBootstrapModal(
                            ko.unwrap(options.title),
                            ko.unwrap(options.message),
                            ko.unwrap(options.continueButtonText),
                            ko.unwrap(options.cancelButtonText)
                        ).then(
                            function () {
                                // proceed with the original postback
                                callback().then(resolve, reject);
                            },
                            function () {
                                // reject the original postback
                                reject({
                                    type: "handler",
                                    handler: this,
                                    message: "The postback was aborted by user."
                                });
                            });

                    } else {

                        // just continue with the original postback
                        callback().then(resolve, reject);
                    }

                });
            }
        };
    };

    // helper function that shows the Bootstrap modal dialog and returns a promise
    // - the promise resolves when the dialog was confirmed
    // - the promise rejects when it has been canceled
    function showBootstrapModal(title, message, continueButtonText, cancelButtonText) {
        return new Promise(function (resolve, reject) {

            var wasConfirmed = false;

            // build the bootstrap modal dialog
            var modal = $(`<div class="modal" tabindex="-1" role="dialog">
                          <div class="modal-dialog" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <h5 class="modal-title"></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                              </div>
                              <div class="modal-body">
                                <p></p>
                              </div>
                              <div class="modal-footer">
                                <button type="button" class="btn btn-primary"></button>
                                <button type="button" class="btn btn-secondary" data-dismiss="modal"></button>
                              </div>
                            </div>
                          </div>
                        </div>`);

            // set texts
            modal.find(".modal-header h5").text(title);
            modal.find(".modal-body p").text(message);
            modal.find(".modal-footer button.btn-primary").text(continueButtonText);
            modal.find(".modal-footer button.btn-secondary").text(cancelButtonText);

            // handle events
            modal.find(".modal-footer button.btn-primary").on("click",
                function () {
                    wasConfirmed = true;
                    resolve();
                    modal.modal("hide");
                });
            modal.on("hidden.bs.modal",
                function () {
                    modal.remove();
                    if (!wasConfirmed) {
                        reject();
                    }
                });

            // add the dialog in the page and show it
            $(document.body).append(modal);
            modal.modal("show");
        });
    }

});