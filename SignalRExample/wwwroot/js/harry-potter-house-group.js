let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");

let lbl_houseJoined = document.getElementById("lbl_houseJoined");

const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/harry-potter-house-group", signalR.HttpTransportType.WebSockets)
    .build();

function fulfilled() {
    console.log("Successful connection")
}

function rejected() {
    console.error("Hub connection failed")
}

connection.on("subscriptionStatus", (groupsJoined, houseName, isSubscribed) => {
        lbl_houseJoined.innerText = groupsJoined;

        if (isSubscribed) {
            switch (houseName) {
                case 'Gryffindor':
                    btn_gryffindor.style.display = "none";
                    btn_un_gryffindor.style.display = "";
                    break;
                case 'Slytherin':
                    btn_slytherin.style.display = "none";
                    btn_un_slytherin.style.display = "";
                    break;
                case 'Hufflepuff':
                    btn_hufflepuff.style.display = "none";
                    btn_un_hufflepuff.style.display = "";
                    break;
                case 'Ravenclaw':
                    btn_ravenclaw.style.display = "none";
                    btn_un_ravenclaw.style.display = "";
                    break;
                default:
                    break;
            }

            toastr.success(`You have Subscribed Successfully to ${houseName}`);

        } 
        else {
            switch (houseName) {
                case 'Gryffindor':
                    btn_gryffindor.style.display = "";
                    btn_un_gryffindor.style.display = "none";
                    break;
                case 'Slytherin':
                    btn_slytherin.style.display = "";
                    btn_un_slytherin.style.display = "none";
                    break;
                case 'Hufflepuff':
                    btn_hufflepuff.style.display = "";
                    btn_un_hufflepuff.style.display = "none";
                    break;
                case 'Ravenclaw':
                    btn_ravenclaw.style.display = "";
                    btn_un_ravenclaw.style.display = "none";
                    break;
                default:
                    break;
            }

            toastr.error(`You have Unsubscribed Successfully from ${houseName}`);
        }
    }
)

connection.on("memberJoinedToHouse", (houseName) => {
    toastr.info(`A new member joined to ${houseName}`);
});

connection.on("memberLeftTheHouse", (houseName) => {
    toastr.warning(`A member left the ${houseName}`);
});

connection.on("notificationReceived", (houseName) => {
    toastr.info(`A new notification received from ${houseName}`);
});

connection.start().then(fulfilled, rejected);

// Subscribe
btn_gryffindor.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("JoinHouse", "Gryffindor");
});

btn_slytherin.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("JoinHouse", "Slytherin");
});

btn_hufflepuff.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("JoinHouse", "Hufflepuff");
});

btn_ravenclaw.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("JoinHouse", "Ravenclaw");
});

// Unsubscribe
btn_un_gryffindor.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("LeaveHouse", "Gryffindor");
});

btn_un_slytherin.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("LeaveHouse", "Slytherin");
});

btn_un_hufflepuff.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("LeaveHouse", "Hufflepuff");
});

btn_un_ravenclaw.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("LeaveHouse", "Ravenclaw");
});

// Trigger notification
trigger_gryffindor.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("TriggerHouseNotification", "Gryffindor");
});

trigger_hufflepuff.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("TriggerHouseNotification", "Hufflepuff");
});

trigger_ravenclaw.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("TriggerHouseNotification", "Ravenclaw");
});

trigger_slytherin.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("TriggerHouseNotification", "Slytherin");
});