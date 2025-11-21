$(document).ready(function() {
    $('.persian-date').each(function() {
        $(this).persianDatepicker({
            format: 'YYYY/MM/DD',
            initialValue: false,
            autoClose: true,
            calendar: {
                persian: {
                    locale: 'fa'
                }
            }
        });
    });
});