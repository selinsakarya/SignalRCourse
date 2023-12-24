const userCountConnection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets)
    .build();


function newWindowLoadedOnClient(){
    userCountConnection.invoke("NewWindowLoaded").then((val) => console.log({val}))
}

function fulfilled() {
    //do something on start
    console.log("Connection to User Hub Successful");
    newWindowLoadedOnClient();
}
function rejected() {
    //rejected logs
}

userCountConnection.start().then(fulfilled, rejected);