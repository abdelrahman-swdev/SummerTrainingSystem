$(document).ready(fillSupervisorsTable());

document.getElementById('supervisorsTable_length').classList.add('mb-2');

function fillSupervisorsTable() {
    $('#supervisorsTable').dataTable({
        "responsive": true,
        "processing": true,
        "serverSide": true,
        "bDestroy": true,
        "filter": true,
        "ajax": {
            "url": "/api/supervisors",
            "type": "Post",
            "datatype": "json",
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false,
            }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "firstName", "name": "FirstName", "autowidth": true },
            { "data": "lastName", "name": "LastName", "autowidth": true },
            { "data": "email", "name": "Email", "autowidth": true },
            { "data": "universityID", "name": "UniversityID", "autowidth": true },
            {
                "name": "Department.Name",
                "render": function (data, type, row) {
                    return `<td>
                                ${row.department === null ? 'Not Assigned' : row.department.name}
                            </td>`;
                },
                "autowidth": true
            },
            {
                "render": function (data, type, row) {
                    return `<button class="btn btn-danger" onclick=deleteSupervisor('${row.id}')>
                                <i class="fas fa-trash"></i>
                                <div class="spinner-border text-white spinner-border-sm d-none" role="status" id="${row.id}">
                                    <span class="visually-hidden">Loading...</span>
                                </div>
                            </button>`;
                },
                "orderable": false
            }
        ]
    });
}

const deleteSupervisor = (data) => {
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
                url: '/account/delete/' + data,
                method: 'Delete',
                beforeSend: (xhr) => {
                    document.getElementById(data).classList.toggle('d-none');
                },
                success: (result, status, xhr) => {
                    document.getElementById(data).classList.toggle('d-none');
                    fillSupervisorsTable();
                    Swal.fire(
                        'Deleted!',
                        'Supervisor has been deleted.',
                        'success'
                    );
                },
                error: (result, status, xhr) => {
                    document.getElementById(data).classList.toggle('d-none');
                    Swal.fire(
                        'Error!',
                        'Something went wrong.',
                        'error'
                    )
                }
            });
        }
    });
}