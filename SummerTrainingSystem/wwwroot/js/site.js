// update profile photo for student, company and supervisor
function loadFile(event) {
    // update image in ui
    const image = document.getElementById("ProfilePicture");
    image.src = URL.createObjectURL(event.target.files[0]);

    // send file to server as FormFile
    var formData = new FormData();
    formData.append("file", event.target.files[0]);

    // call backend with fileName to update it
    const spinner = document.getElementById('profilePicSpinner');
    $.ajax({
        url: `/account/update-photo`,
        method: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        beforeSend: function (xhr) {
            spinner.classList.toggle('d-none');
        },
        success: function (result, status, xhr) {
            spinner.classList.toggle('d-none');
            toastr.options.closeButton = true;
            toastr.success('Profile picture updated.');
        },
        error: function (result, status, xhr) {
            spinner.classList.toggle('d-none');
            Swal.fire(
                'Error!',
                'Something went wrong.',
                'error'
            );
        }
    });
};