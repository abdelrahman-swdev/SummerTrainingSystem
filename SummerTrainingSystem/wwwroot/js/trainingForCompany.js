function deleteTraining(data) {
    const spinner = document.getElementById(data);
    const parent = spinner.parentElement.parentElement.parentElement;
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
                url: '/trainings/' + data,
                method: 'Delete',
                beforeSend: (xhr) => {
                    spinner.classList.toggle('d-none');
                },
                success: (result, status, xhr) => {
                    spinner.classList.toggle('d-none');
                    parent.className = "d-none";
                    Swal.fire(
                        'Deleted!',
                        'Training deleted successfully.',
                        'success'
                    );
                },
                error: (result, status, xhr) => {
                    spinner.classList.toggle('d-none');
                    Swal.fire(
                        'Error!',
                        'Something went wrong.',
                        'error'
                    );
                }
            })
        }
    });
}

function display(m) {
    const ds = new Date();
    const as = document.getElementById("date");
    const allSeconds = (ds - m) / 1000;
    const days = allSeconds / 3600 / 24;
    as.innerHTML = days;
}