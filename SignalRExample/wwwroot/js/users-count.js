const userCountConnection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets)
    .build();


function newWindowLoadedOnClient(){
    userCountConnection.invoke("NewWindowLoaded").then((val) => console.log({val}))
}

userCountConnection.on("updateTotalViews", (value) =>{
    const newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();    
});

userCountConnection.on("updateTotalActiveUsers", (value) =>{
    const newCountSpan = document.getElementById("totalActiveUsersCounter");
    newCountSpan.innerText = value.toString();
});

function fulfilled() {
    //do something on start
    console.log("Connection to User Hub Successful");
    newWindowLoadedOnClient();
}
function rejected() {
    //rejected logs
}

userCountConnection.start().then(fulfilled, rejected);