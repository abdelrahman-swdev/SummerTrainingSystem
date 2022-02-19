$(document).ready(function () {
    $('#studentsTable').dataTable({
        "responsive": true,
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/students",
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
            { "data": "level", "name": "Level", "autowidth": true },
            { "data": "gpa", "name": "Gpa", "autowidth": true },
            { "data": "universityID", "name": "UniversityID", "autowidth": true },
            { "data": "department.name", "name": "department.name", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-danger" onclick=deleteStudent(' + row.universityID + ')>Delete</button>';
                },
                "orderable": false
            }
        ]

    });
});

const deleteStudent = (data) => {
    console.log(data);
}