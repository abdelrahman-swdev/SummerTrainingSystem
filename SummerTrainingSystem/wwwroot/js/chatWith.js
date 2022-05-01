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
    var withPictureUrl = document.getElementById("WithPictureUrl").value;
    var myEmail = document.getElementById("MyEmail").value;
    var myPicUrl = document.querySelector(".profile-picture").getAttribute("src");

    if (senderName === myEmail || senderName === RecieverEmail) {
        if (senderName === myEmail) {
            document.getElementById("chatWrapper").insertAdjacentHTML("beforeend", `
                <div class="sendermessagebody">
                    <div class="sender-info">
                        <div class="sender-img">
                            <img src="${myPicUrl}" alt="${senderName}">
                        </div>
                        <div class="sender-name">
                            <h5>${senderName}</h5>
                            <div class="sender-time">
                                ${when}
                            </div>
                        </div>
                    </div>
                    <div class="content"
                        style="background-color: #0a80ff;color: #fff;border-radius: 7px;padding: 10px 20px;word-break:break-all;">
                        ${message}
                    </div>
                </div>
            `);
        }
        else
        {
            document.getElementById("chatWrapper").insertAdjacentHTML("beforeend", `
                <div class="reciever">
                    <div class="sender-info">
                        <div class="sender-img">
                            <img src="uploads/${withPictureUrl}" alt="${senderName}">
                        </div>
                        <div class="sender-name">
                            <h5>${senderName}</h5>
                            <div class="sender-time">
                                ${when}
                            </div>
                        </div>
                    </div>
                    <div class="recievecontent"
                    style="background-color: #293145;color: #fff;border-radius: 7px;padding: 10px 20px;word-break:break-all;">
                        ${message}
                    </div>
                </div>
            `);
        }

    }

});

function sendPrivateMessage(event) {

    event.preventDefault();

    var receiver = document.getElementById("RecieverEmail").value;
    var message = document.getElementById("messageInput");

    if (receiver != "" && message.value.trim() !== "") {

        connection.invoke("SendMessageToGroup", receiver, message.value)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    message.value = "";
}