jQuery.validator.addMethod("maximumage", function(value, element, param) {
    if (!value) return false;

    var dateToCheck = new Date(value);
    if (isNaN(dateToCheck.getTime())) return false;

    var maxYears = Number(param);
    var cutoffDate = new Date();
    cutoffDate.setFullYear(cutoffDate.getFullYear() - maxYears);

    // Check if the input date is on or after the cutoff (i.e., age <= maxYears)
    return dateToCheck >= cutoffDate;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("maximumage", "years");
