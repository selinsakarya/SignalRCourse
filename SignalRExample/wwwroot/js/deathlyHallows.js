let cloakScore = document.getElementById("cloakScore");
let stoneScore = document.getElementById("stoneScore");
let wandScore = document.getElementById("wandScore");

const deathlyHallowsConnection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/deathlyHallows", signalR.HttpTransportType.WebSockets)
    .build();

deathlyHallowsConnection.on("updateDeathlyHallowsScores", (cloak, stone, wand) => {
    cloakScore.innerText = cloak.toString();
    stoneScore.innerText = stone.toString();
    wandScore.innerText = wand.toString();
});

function fulfilled() {
    //do something on start
    console.log("Connection to Deathly Hallows Hub Successful");

    deathlyHallowsConnection.invoke("GetRaceStatus").then(raceStatus => {

        if (!raceStatus) {
            return;
        }

        cloakScore.innerText = raceStatus.cloak.toString();
        stoneScore.innerText = raceStatus.stone.toString();
        wandScore.innerText = raceStatus.wand.toString();
    });
}

function rejected() {
    //rejected logs
}

deathlyHallowsConnection.start().then(fulfilled, rejected);