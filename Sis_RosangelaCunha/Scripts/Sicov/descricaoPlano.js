$(document).ready(function () {

    Listar();

    $('#listadescricoes').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
    AplicarMascara();
});

//Listar
function Listar() {
    var url = "/DescricaoPlano/Listar";

    var busca = $("#busca").val();

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        , data: { busca: busca }
        , success: function (data) {
            $("#listardescricoes").html("");
            $("#listardescricoes").html(data);
        }
    });

}


//Novo
function NovoDescricaoPlano() {
    var url = "/DescricaoPlano/Novo";

    $.ajax({
        url: url,
        datatype: 'html',
        type: 'GET',
        success: function (data) {
            AbrirModal('Novo Plano', data);
            AplicarMascara();
            $(".datepicker").datepicker();
        }
    })
}

function EditarPlano(id) {

    var url = "/DescricaoPlano/Editar";

    $.ajax({
        url: url,
        datatype: 'html',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            AbrirModal('Editar Plano', data);
            $("#PlanoId").val(id);
            $(".datepicker").datepicker();
        }
    })
}

function SalvarDescricaoPlano() {
    //Verifica se o Formulario é válido
    if (ValidarFormulario()) {
        //recupera os dados
        var id = $("#PlanoId").val();
        var descricaoPlano = $("#DescricaoPlano").val();
        var valor = $("#Valor").val();
        var mensalidades = $("#Mensalidades").val();


        // Cria um Objeto
        var plano = {
            PlanoId: id
            , DescricaoPlano: descricaoPlano
            , Valor: valor
            , Mensalidades: mensalidades

        };

        var url = "/DescricaoPlano/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "") {
            url = "/DescricaoPlano/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url,
            datatype: "json",
            type: "POST",
            data: { plano: plano },
            success: function (data) {
                if (data.Resultado) {
                    Listar();
                    toastr.success('Dados Gravados com Sucesso!');
                }

                $("#myModal").modal('hide');
            }
        })


    }
}

function ValidarFormulario() {
    // Validate signup form on keyup and submit
    $("#frmPlano").validate({
        rules: {
            descricaoPlano: "required",
            valor: "required",
            mensalidades: "required",
        },
        messages: {
            descricaoPlano: "Informe o nome a descrição do plano",
            valor: "informe o Valor",
            mensalidades: "Selecione se for mensalidade",

        }
        , highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    return $("#frmPlano").valid();
}

function ExcluirDescricaoPlano(id) {

    if (confirm('Deseja excluir esta PlanoDescricao?')) {
        var url = "/DescricaoPlano/Excluir";

        $.ajax({
            url: url,
            type: 'POST',
            datatype: "json",
            data: { id: id },
            success: function (data) {
                if (data.Resultado) {
                    Listar();
                } else {
                    toastr.error('Erro ao excluir, DescricaoPlano em utilização');
                }
            }
        });
    }


}