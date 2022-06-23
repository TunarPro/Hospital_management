"use strict";

function showErrorMsg() {
    swal.fire({
        text: "Xəta baş verdi, bir qədər sonra əməliyyatı yenidən icra edin.",
        icon: 'error',
        showConfirmButton: true,
        confirmButtonText: "Səhifəyə keç",
        showCancelButton: false
    })
    console.log('ERROR')
}

function showSuccessMsg(msg) {
    swal.fire({
        icon: 'success',
        html: msg,
        showConfirmButton: true,
        confirmButtonText: "Səhifəyə keç",
        confirmButtonColor: "#5ea41d",
        buttonsStyling: true,
    })
    console.log('SUCCESS')
}


function showWarningMsg(msg) {
    swal.fire({
        html: msg,
        icon: 'error',
        showConfirmButton: true,
        confirmButtonText: "Səhifəyə keç",
        showCancelButton: false
    })
    console.log('WARNING')
}

