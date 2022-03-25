$(document).ready(fillCompaniesTable());

document.getElementById('companiesTable_length').classList.add('mb-2');

function fillCompaniesTable() {
    $('#companiesTable').dataTable({
        "responsive": true,
        "processing": true,
        "serverSide": true,
        "bDestroy": true,
        "filter": true,
        "ajax": {
            "url": "/api/companies",
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
            {
                "data": "name", "name": "Name", "autowidth": true,
                "render": function (data, type, row) {
                    return `<a href="/account/company-profile?id=${row.id}">
                                ${data}
                            </a>`
                }
            },
            { "data": "city", "name": "City", "autowidth": true },
            { "data": "industry", "name": "Industry", "autowidth": true },
            {
                "data": "foundedAt", "name": "FoundedAt", "autowidth": true,
                "render": function (data, type, row) {
                    return `<td>${data.split("T")[0]}</td>`
                }
            },
            { "data": "companySize.sizeRange", "name": "companySize.sizeName", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return `<button class="btn btn-danger" onclick=deleteCompany('${row.id}')>
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

const deleteCompany = (data) => {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this and all related data to this company (trainings,..) will be deleted also!",
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
                    fillCompaniesTable();
                    Swal.fire(
                        'Deleted!',
                        'Student has been deleted.',
                        'success'
                    );
                },
                error: (result, status, xhr) => {
                    document.getElementById(data).classList.toggle('d-none');
                    Swal.fire(
                        'Error!',
                        'Something went wrong.',
                        'error'
                    );
                }
            });
        }
    });
}