(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {

            //sendData();
            getMasterCategories();

        });
    };

    function getMasterCategories() {
        Office.context.mailbox.masterCategories.getAsync(function (asyncResult) {
            if (asyncResult.status === Office.AsyncResultStatus.Succeeded) {
                const categories = asyncResult.value;
                if (categories && categories.length > 0) {
                    console.log("Master categories:");
                    console.log(JSON.stringify(categories));
                } else {
                    console.log("There are no categories in the master list.");
                }
            } else {
                console.error(asyncResult.error);
            }
        });
    }

})();