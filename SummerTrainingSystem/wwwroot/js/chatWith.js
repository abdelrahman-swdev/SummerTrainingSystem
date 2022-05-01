"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceivePrivateMessage", function (senderName, message, when) {

    console.log("Received Message");

    var RecieverEmail = document.getElementById("RecieverEmail").value;
    var myEmail = document.getElementById("MyEmail").value;

    if (senderName === myEmail || senderName === RecieverEmail) {
        var classesName = "";
        if (senderName === myEmail) {
            classesName = "list-group-item list-group-item-success";
        }
        else {
            classesName = 'list-group-item list-group-item-secondary';
        }

        document.getElementById("chatWrapper").insertAdjacentHTML("beforeend", `
            <span class="badge bg-dark">${senderName}</span>
            <h6 class="${classesName}">${message}</h6>
            <span class="badge bg-light">${when}</span>
            <hr />
        `);
    }

});

function sendPrivateMessage() {
    var receiver = document.getElementById("RecieverEmail").value;
    var message = document.getElementById("messageInput");

    if (receiver != "") {

        connection.invoke("SendMessageToGroup", receiver, message.value)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    message.value = "";
}