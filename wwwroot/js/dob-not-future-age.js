jQuery.validator.addMethod("notfuturedate", function (value, element) {
    if (!value) return false;

    var date = new Date(value);
    if (isNaN(date.getTime())) return false;

    var today = new Date();
    today.setHours(0, 0, 0, 0); // ignore time part

    return date <= today;
});

jQuery.validator.unobtrusive.adapters.addBool("notfuturedate");
