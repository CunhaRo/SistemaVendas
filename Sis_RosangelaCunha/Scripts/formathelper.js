jQuery(function ($) {
    AplicarMascara();
});

function AplicarMascara() {
    $(".cpf").mask("999.999.999-99");

    $(".cnpj").mask("99.999.999/9999-99", { placeholder: "" });

    $(".hora").mask("99:99");

    $(".data").mask("99/99/9999");

    $(".datetime").mask("99/99/9999 99:99:99");

    $(".psa").mask("PSA9999999999");

    $("#PSA").mask("PSA9999999999");

    $(".cep").mask("99999-999");

    $(".mesano").mask("99/9999");

    $(".ddd").mask("99");

    $(".tel8").mask("9999-9999");

    $(".tel").mask("9999-9999?9")
    .focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("99999-999?9");
        } else {
            element.mask("9999-9999?9");
        }
    });

    $(".telefone").mask("(99) 9999-9999?9")
    .focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    });

}
