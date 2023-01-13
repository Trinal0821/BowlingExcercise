(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {

            /*$("#get-from").click(getFrom);
            console.log("GOT THE FROM, Subject and body");

            $.ajax({
                type: "POST",
                url: "/Home/Run",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    console.log("the list", msg)
                },
                error: function (req, status, error) {
                    console.log(":(")
                }
            });*/

            sendData();

        });
    };

    async function sendData() {

        // $("#get-from").click(getFrom);
        //console.log("GOT THE FROM, Subject and body");

        /*        const temp = async () => {
                    const body = await getBody();
                    console.log("HUZZAH "  + body);
                }*/
        //  console.log("THIS IS THE BODY HAHHAHA: " + body);

        //var parsedJson = await getFrom();
        // getBody().then(function (body) {console.log(body) });
        //var body = getBody();
       // console.log(body);
        getFrom();
        // console.log("This is from the method call" + parsedJson);   

        /*        axios.get("/Home/testing", {
                    params: JSON.stringify(parsedJson)
                })
                    .then(res => {
                        console.log(res);
                    });*/


        /*        axios.get("/Home/testing", {
                    params: {
                        from: "Trina",
                        subject: "the subject",
                        body: "the body"
                    }
                })
                    .then(res => {
                        console.log(res);
                    });
        */



        /*  const data = { from: 'example' };
  
          fetch('/Home/testing', {
              method: 'GET', // or 'PUT'
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify(data),
          })
              .then((response) => response.json())
              .then((data) => {
                  console.log('Success:', data);
              })
              .catch((error) => {
                  console.error('Error:', error);
              });*/

        /*      $.ajax({
                  type: "POST",
                  url: "/Home/testing",
                 // dataType: "json",
                  contentType: "application/json",
                  dataType: 'json',
                  data: JSON.stringify(testing),
                  success: function (msg) {
                     // console.log("HEY O FROM SENDDATa");
                      console.log(msg);
                     // console.log("From: ", msg.from + "Subject: " + msg.subject + "body: " + msg.body)
                     // console.log("data sent", getFrom())
                  },
                  error: function (req, status, error) {
                      console.log(":(")
                      console.log(error);
                  }
              });*/
    }

    async function getBody() {
        let body = await 'FILLER : ';
        await Office.context.mailbox.item.body.getAsync(
            "text",
            function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    body = result.value;

                    console.log(body);


                    //  jsonstring += body + ";"

                    // jsonstring = JSON.stringify(jsonstring);
                    // return jsonstring
                }
                else {
                    console.log(result.status);
                }
            }
        )

        console.log("This is the  body inside the body " + body);
        return body;
    }

    async function getFrom() {

        console.log("I'm HERE HSHSH");
        //Get the from and append the client's name
        const msgFrom = Office.context.mailbox.item.from;
        var fromField = msgFrom.displayName + ";";

        //Get the subject and append it
        var subjectField = Office.context.mailbox.item.subject + ";";

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
                            console.log(res);
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


/*(function () {
  "use strict";

  var messageBanner;

  // The Office initialize function must be run each time a new page is loaded.
  Office.initialize = function (reason) {
    $(document).ready(function () {
      var element = document.querySelector('.MessageBanner');
      messageBanner = new components.MessageBanner(element);
      messageBanner.hideBanner();
      loadProps();
    });
  };

  // Take an array of AttachmentDetails objects and build a list of attachment names, separated by a line-break.
  function buildAttachmentsString(attachments) {
    if (attachments && attachments.length > 0) {
      var returnString = "";
      
      for (var i = 0; i < attachments.length; i++) {
        if (i > 0) {
          returnString = returnString + "<br/>";
        }
        returnString = returnString + attachments[i].name;
      }

      return returnString;
    }

    return "None";
  }

  // Format an EmailAddressDetails object as
  // GivenName Surname <emailaddress>
  function buildEmailAddressString(address) {
    return address.displayName + " &lt;" + address.emailAddress + "&gt;";
  }

  // Take an array of EmailAddressDetails objects and
  // build a list of formatted strings, separated by a line-break
  function buildEmailAddressesString(addresses) {
    if (addresses && addresses.length > 0) {
      var returnString = "";

      for (var i = 0; i < addresses.length; i++) {
        if (i > 0) {
          returnString = returnString + "<br/>";
        }
        returnString = returnString + buildEmailAddressString(addresses[i]);
      }

      return returnString;
    }

    return "None";
  }

  // Load properties from the Item base object, then load the
  // message-specific properties.
  function loadProps() {
    var item = Office.context.mailbox.item;

    $('#dateTimeCreated').text(item.dateTimeCreated.toLocaleString());
    $('#dateTimeModified').text(item.dateTimeModified.toLocaleString());
    $('#itemClass').text(item.itemClass);
    $('#itemId').text(item.itemId);
    $('#itemType').text(item.itemType);

    $('#message-props').show();

    $('#attachments').html(buildAttachmentsString(item.attachments));
    $('#cc').html(buildEmailAddressesString(item.cc));
    $('#conversationId').text(item.conversationId);
    $('#from').html(buildEmailAddressString(item.from));
    $('#internetMessageId').text(item.internetMessageId);
    $('#normalizedSubject').text(item.normalizedSubject);
    $('#sender').html(buildEmailAddressString(item.sender));
    $('#subject').text(item.subject);
    $('#to').html(buildEmailAddressesString(item.to));
  }

  // Helper function for displaying notifications
  function showNotification(header, content) {
    $("#notificationHeader").text(header);
    $("#notificationBody").text(content);
    messageBanner.showBanner();
    messageBanner.toggleExpansion();
  }
})();*/
