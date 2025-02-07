$(document).ready(function() {
    $('#calculateBtn').click(function() {
        // Validate input
        const hours = parseFloat($('#hours').val());
        if (isNaN(hours) || hours <= 0) {
            alert('Please enter a valid positive number of hours');
            return;
        }

        // Calculate total
        const rate = parseFloat($('#rate').val());
        const total = hours * rate;

        // Display result
        $('#total').val('$' + total.toFixed(2));
    });
});