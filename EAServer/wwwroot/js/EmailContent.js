(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {

            sendData();

        });
    };

    async function sendData() {
        getFrom();
    }

    async function getFrom() {
        //Get the from and append the client's name
        const msgFrom = Office.context.mailbox.item.from;
        var fromField = msgFrom.displayName;

        //Get the subject and append it
        var subjectField = Office.context.mailbox.item.subject;

        console.log("Got subject and from");

        await Office.context.mailbox.item.body.getAsync(
            "text",
            function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    var bodyField = result.value;

                    console.log(bodyField);

                    axios.get("/Home/testing", {
                        params:
                        {
                            from: fromField,
                            subject: subjectField,
                            body: bodyField
                        }
                    })
                        .then(res => {
                            console.log(res.data.colortagged);
                        });

                    //jsonstring = JSON.stringify(jsonstring);
                    // return jsonstring
                }
                else {
                    console.log(result.status);
                }
            }
        )

    }
})();