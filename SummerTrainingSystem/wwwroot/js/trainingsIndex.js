const spinner = document.getElementById('spinner');
const alertMessage = document.getElementById('alertMessage');

$(document).ready(function () {

    $.ajax({
        url: '/trainings/getall',
        method: 'get',
        beforeSend: (xhr) => {
            spinner.classList.toggle('d-none');
        },
        success: (result, status, xhr) => {

            setTimeout(() => {
                spinner.classList.toggle('d-none');
                if (result) {
                    $('#trainingsContainer').html(result);
                } else {
                    alertMessage.classList.toggle('d-none');
                }
            }, 2000);

        },
        error: (result, status, xhr) => {
            setTimeout(() => {
                spinner.classList.toggle('d-none');
                Swal.fire(
                    'Error!',
                    'Something went wrong.',
                    'error'
                );
            }, 2000);
        }
    });
});


function updateTrainings(depid, abbreviation) {
    
    const linkClicked = document.getElementById(abbreviation);
    const selectedListItem = document.querySelector('.list-group-item.active');
    selectedListItem.classList.toggle('active');
    linkClicked.classList.toggle('active');

    let url = `/trainings/getall?depid=${depid}`;
    const searchQuery = document.getElementById('searchQuery').value.trim();
    if (searchQuery) {
        url = `${url}&search=${searchQuery}`;
    }

    $.ajax({
        url: url,
        method: 'Get',
        beforeSend: (xhr) => {
            $('#trainingsContainer').empty();
            if (!alertMessage.classList.contains('d-none')) {
                alertMessage.classList.toggle('d-none')
            }
            spinner.classList.toggle('d-none');
        },
        success: (result, status, xhr) => {

            setTimeout(() => {
                spinner.classList.toggle('d-none');
                if (result) {
                    $('#trainingsContainer').html(result);
                } else {
                    alertMessage.classList.toggle('d-none');
                }
            }, 2000);

        },
        error: (result, status, xhr) => {
            setTimeout(() => {
                spinner.classList.toggle('d-none');
                Swal.fire(
                    'Error!',
                    'Something went wrong.',
                    'error'
                );
            }, 2000);
        }
    });
}


const onSearch = (event) => {

    event.preventDefault();
    
    const searchQuery = document.getElementById('searchQuery').value.trim();

    if (searchQuery) {

        const selectedListItem = document.querySelector('.list-group-item.active');
        let selectedDep = 0;
        if (selectedListItem) {
            selectedDep = selectedListItem.getAttribute('data-depid');
        }

        const url = `/trainings/getall?depid=${selectedDep}&search=${searchQuery}`;

        $.ajax({
            url: url,
            method: 'Get',
            beforeSend: (xhr) => {
                $('#trainingsContainer').empty();
                if (!alertMessage.classList.contains('d-none')) {
                    alertMessage.classList.toggle('d-none')
                }
                spinner.classList.toggle('d-none');
            },
            success: (result, status, xhr) => {

                setTimeout(() => {
                    spinner.classList.toggle('d-none');
                    if (result) {
                        $('#trainingsContainer').html(result);
                    } else {
                        alertMessage.classList.toggle('d-none');
                    }
                }, 2000);

            },
            error: (result, status, xhr) => {
                setTimeout(() => {
                    spinner.classList.toggle('d-none');
                    Swal.fire(
                        'Error!',
                        'Something went wrong.',
                        'error'
                    );
                }, 2000);
            }
        });
    }

}