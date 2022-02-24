function deleteTraining(data) {
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
                    document.getElementById('deleteSpinner').classList.toggle('d-none');
                },
                success: (result, status, xhr) => {
                    document.getElementById('deleteSpinner').classList.toggle('d-none');
                    window.location.replace('/trainings');
                },
                error: (result, status, xhr) => {
                    document.getElementById('deleteSpinner').classList.toggle('d-none');
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