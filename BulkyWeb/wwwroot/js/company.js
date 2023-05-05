var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "streetAddress", "width": "15%" },
            { data: "city", "width": "10%" },
            { data: "state", "width": "10%" },
            { data: "phoneNumber", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div><a href="/admin/company/upsert?id=${data}"><i class="fa-regular fa-pen-to-square" style="color:darkslateblue"></i></a>
                             <a onClick=Delete('/admin/company/delete/${data}')><i class="fa-solid fa-trash" style="color:red"></i></a></div>`

                },
                "width": "15%"
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}
