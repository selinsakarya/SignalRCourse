let dataTable;
let connection = new signalR.HubConnectionBuilder().withUrl("/hubs/orders").build();

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    console.log("asd");
    
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Home/Orders"
        },
        "columns": [
            {"data": "id", "width": "5%"},
            {"data": "name", "width": "15%"},
            {"data": "itemName", "width": "15%"},
            {"data": "count", "width": "15%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href=""
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}

connection.on("newOrderReceived", () => {
    dataTable.ajax.reload();
    toastr.success("New order received");
});

connection.start().then();
