$(document).ready(function () {
    $('#supervisorsTable').dataTable({
        "responsive": true,
        "processing": true,
        "serverSide": true,
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
            { "data": "department.name", "name": "department.name", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-danger" onclick=deleteSupervisor(' + row.universityID + ')>Delete</button>';
                },
                "orderable": false
            }
        ]

    });
});

const deleteSupervisor = (data) => {
    alert(data);
}