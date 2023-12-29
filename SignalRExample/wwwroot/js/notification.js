let sendButton = document.getElementById('sendButton');
let notificationInput = document.getElementById('notificationInput');
let notificationCounter = document.getElementById('notificationCounter');
let messageList = document.getElementById('messageList');

sendButton.disabled = true;

const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/notifications", signalR.HttpTransportType.WebSockets)
    .build();

function fulfilled() {
    console.log("Successful connection")

    sendButton.disabled = false;

    connection.invoke("GetMessages").then((messages) => {
        if (messages) {
            notificationCounter.innerHTML = "<span>(" + messages.length + ")</span>";
            for (let i = messages.length - 1; i >= 0; i--) {
                let li = document.createElement("li");
                li.textContent = "Notification - " + messages[i];
                messageList.appendChild(li);
            }
        }
    })
}

function rejected() {
    console.error("Hub connection failed")
}

sendButton.addEventListener("click", function (event) {

    event.preventDefault();

    let message = notificationInput.value;

    connection.send("SendMessage", message).then(() => {
        notificationInput.value = '';
    });
    
});

connection.on("NewMessageReceived", (message, messageCount) => {
    notificationCounter.innerHTML = "<span>(" + messageCount + ")</span>";

    let li = document.createElement("li");
    li.textContent = "Notification - " + message;
    messageList.appendChild(li);
    
});

connection.start().then(fulfilled, rejected);

