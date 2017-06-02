var AppSettings =
    {
        Api:
            {
                Owners: "http://localhost:50628/api/Owners/",
                Pets: "http://localhost:50628/api/Pets/"

            },
        Utils:
            {
                ShowMessage: function (status, messages) {
                    var arr = new Array();
                    if (messages instanceof Array) {
                        console.log("array");
                        for (i = 0; i < messages.length; i++) {
                            arr.push("<div class='alert alert-" + status + "'>");
                            arr.push(messages[i]);
                            arr.push("</div>");
                        }
                    }
                    else {
                        console.log("string");
                        arr.push("<div class='alert alert-" + status + "'>");
                        arr.push(messages);
                        arr.push("</div>");
                    }
                    $("#ErrorBlock").prepend(arr.join("")).show();
                    setTimeout(function () {
                        $('#ErrorBlock').html("").hide();
                    }, 5000);
                },
                ParseModelState: function (modelState) {
                    var errors = [];
                    for (var key in modelState) {
                        for (var i = 0; i < modelState[key].length; i++) {
                            errors.push(modelState[key][i]);
                        }
                    }
                    return errors;
                }
            }
    }