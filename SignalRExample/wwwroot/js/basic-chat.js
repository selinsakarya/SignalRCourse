let sendMessageButton = document.getElementById('sendMessage');
let chatMessage = document.getElementById('chatMessage');
let senderEmail = document.getElementById('senderEmail');
let receiverEmail = document.getElementById('receiverEmail');
let messagesList = document.getElementById("messagesList");

sendMessageButton.disabled = true;

const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/basic-chat", signalR.HttpTransportType.WebSockets)
    .build();

connection.on("newMessageReceived", (sender, message) => {
    console.log({sender});
    console.log({message});
    let li = document.createElement("li");
    messagesList.appendChild(li);
    li.textContent = `${sender} - ${message}`;
});

connection.start().then(() => {
    console.log("Connected to chat hub")
    sendMessageButton.disabled = false;
});

sendMessageButton.addEventListener('click', (event) => {
    event.preventDefault();

    let message = chatMessage.value;
    let sender = senderEmail.value;
    let receiver = receiverEmail.value;

    chatMessage.value = '';

    if (receiver) {
        connection.send("SendPrivateMessage", sender, receiver, message);

    } else {
        connection.send("SendMessageToAll", sender, message);
    }
})