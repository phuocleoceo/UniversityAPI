var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/university/GetAllUniversity",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "address", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                         <a href="/university/Upsert/${data}" class='btn btn-success text-white'
                         style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                        &nbsp;
                        <a onclick=Delete("/university/Delete/${data})" class='btn btn-danger text-white'
                         style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                            </div>
                    `;
                }, "width": "30%"
            }
        ]
    });
}
