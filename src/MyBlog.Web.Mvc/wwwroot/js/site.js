jQuery(document).ready(function () {
    jQuery("time.timeago").timeago();
});

function confirmDelete(ev, customText) {
    ev.preventDefault();
    var urlToRedirect = ev.currentTarget.getAttribute('href');

    swal.fire({
        text: customText,
        icon: "warning",
        showDenyButton: true,
        showCancelButton: true,
        showConfirmButton: false,
        denyButtonText: "Excluir",

    })
        .then((result) => {
            if (result.isDenied) {
                location.href = urlToRedirect;
            }
        });
}
