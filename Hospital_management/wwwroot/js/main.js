"use strict";

$(window).on('load', function () {

    let result = $('#response').attr('result');

    switch (result) {

        case 'AddDoctorSuccess':
            showSuccessMsg('<h5>Həkim uğurla <b>əlavə olundu</b>.</h5>');
            break;

        case 'EditDoctorSuccess':
            showSuccessMsg('<h5>Həkim uğurla <b>redaktə edildi</b>.</h5>');
            break;

        case 'DeleteDoctorSuccess':
            showSuccessMsg('<h5>Həkim uğurla <b>silindi</b>.</h5>');
            break;

        case 'RegistrationSuccess':
            showSuccessMsg('<h5>Uğurla qeydiyyatdan keçdiniz. Həkim ilə görüş təyin etmək üçün hesabınıza daxil olun.</h5>');
            break;

        case 'AppointmentSuccess':
            showSuccessMsg('<h5>Həkimlə görüş uğurla təyin olundu.</h5>');
            break;

        case 'AppointmentDateFailed':
            showWarningMsg('<h5>Seçdiyiniz tarix keçərli olmadığından görüş təyin olunmadı.</h5>');
            break;
    }
});

$('.deleteDoctor-btn').click(function () {

    let thisEl = $(this);

    swal.fire({
        icon: 'warning',
        html: "<h5>Həkimi <b>silmək</b> istədiyinizdən əminsiniz?</h5>",
        showCancelButton: true,
        confirmButtonColor: "#1FAB45",
        confirmButtonText: "Bəli",
        cancelButtonText: "Xeyr",
        buttonsStyling: true
    })
        .then(function (result) {
            if (result.isConfirmed)
                $('.deleteForm')
                    .attr('action', `/Admin/DeleteDoctor/${thisEl.attr('doctorId')}`)
                    .submit();
        })
});

$('.makeAppointment-btn').click(function (e) {

    e.preventDefault();
    let thisEl = $(this);

    swal.fire({
        html: `
                <div class="sweet-alert-container text-left">
                    <form action="/Home/MakeAppointment" method="post" id="sweet-form">
                        <i class="fas fa-calendar-alt d-block mx-auto mb-35" style="font-size: 40px; width: fit-content; color: var(--main-skyBlue)"></i>
                        <label class="form-label mb-15">Təyinat tarixini və saatı seçin</label>
                        <input name="appointmentDateTime" type="datetime-local" class="form-element">
                        <input type="hidden" name="id" value="${thisEl.attr('id')}">
                    </form>
                </div>`,
        showCancelButton: true,
        confirmButtonColor: "#1FAB45",
        confirmButtonText: "Təsdiq Et",
        cancelButtonText: "Ləğv Et",
        buttonsStyling: true
    })
        .then(function (result) {
            if (result.isConfirmed)
                $('#sweet-form').submit();
        })
})


$('.logout-btn').click(function () {

    let thisEl = $(this);

    swal.fire({
        icon: "warning",
        html: "<h5>Hesabdan çıxmaq istədiyinizdən əminsiniz?</h5>",
        showCancelButton: true,
        confirmButtonColor: "#1FAB45",
        confirmButtonText: "Bəli",
        cancelButtonText: "Xeyr",
        buttonsStyling: true
    })
        .then(function (result) {
            if (result.isConfirmed)
                thisEl.closest('form').submit();
        })
})
