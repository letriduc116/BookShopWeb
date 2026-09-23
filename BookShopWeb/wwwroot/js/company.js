
var dataTable;
// phương thức này sẽ chạy khi trang web được load
$(document).ready(function () {
    loadDataTable();
});

// hàm giúp hiển thị thông tin sản phẩm
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    debugger;
                    return `<div>
                                <a class="btn btn-primary" href = "/Admin/Company/Upsert?id=${data}">
                                    <i class="bi bi-pencil"></i>
                                </a>

                                <a class="btn btn-danger" onclick="Delete('/Admin/Company/Delete/${data}')" >
                                    <i class="bi bi-trash3"></i>
                                </a>
                            </div>`
                },
               "width": "15%" 
            }
        ]
    });
}

// hàm xóa sản phẩm

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //dataTable.ajax.reload();
                        dataTable.ajax.reload(null, false);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}