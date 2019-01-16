(function ($) {
    $.validator.addMethod('GenericRequired', function (value, element, params) {

        if (value === "" || value === null) {
            return false;
        }
        return true;

    }, '');

    $.validator.unobtrusive.adapters.add("GenericRequired", ["GenericRequired"], function (options) {
        options.rules["GenericRequired"] = "#" + options.params.genericrequired;
        options.messages["GenericRequired"] = options.message;
    });
})(jQuery);