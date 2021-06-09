var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/university/getall",
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
                         <a href="/university/upsert/${data}" class='btn btn-success text-white'
                         style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                        &nbsp;
                        <a onclick=Delete("/university/delete/${data}") class='btn btn-danger text-white'
                         style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                            </div>
                    `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(_url) {
    //Sweet Alert
    swal({
        title: "Confirm delete this university ?",
        text: "It will never be able to restore !",
        icon: "warning",
        button: true,
        showCancelButton: true,
        dangerMode: true
    }).then((obj) => {
        if (obj) {
            $.ajax({
                type: 'DELETE',
                url: _url,
                success: function (data) {
                    //success and message from Delete Controller
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
