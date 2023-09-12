var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Trail/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data":"nationalPark.name","width":"15%"},
            { "data": "name", "width": "15%" },
            { "data": "distance", "width": "15%" },
            { "data": "elevation", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                 <div class="text-center">
                   <a href="Trail/Upsert/${data}" class="btn btn-info">
                        <i class="fas fa-edit"></i></a>
                   <a class="btn btn-danger" onclick=Delete("Trail/Delete/${data}")>
                     <i class="fas fa-trash-alt"></i></a>
                     </div>


                 `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want to Delete Data",
        text: "Delete information!!!",
        buttons: true,
        dangerModel: true,
        icon: "warning"
    }).then((Willdelete) => {
        if (Willdelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}