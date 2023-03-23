$(document).ready(function () {

    Listar();

    $('#listaplanos').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
    AplicarMascara();
});

//Listar
function Listar() {
    var url = "/Plano/Listar";

    var busca = $("#busca").val();

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        , data: { busca: busca }
        , success: function (data) {
            $("#listarplanos").html("");
            $("#listarplanos").html(data);
        }
    });

}


//Novo
function NovoPlano() {
    var url = "/Plano/Novo";

    $.ajax({
        url: url,
        datatype: 'html',
        type: 'GET',
        success: function(data) {
            AbrirModal('Novo Plano', data);
            AplicarMascara();
            $(".datepicker").datepicker();
        }
    })
}

function EditarPlano(id) {

    var url = "/Plano/Editar";

    $.ajax({
        url: url,
        datatype: 'html',
        type: 'GET',
        data: { id: id },
        success: function(data) {
            AbrirModal('Editar Plano', data);
            $("#PlanoId").val(id);
            $(".datepicker").datepicker();
        }
    })
}

function SalvarPlano() {
    //Verifica se o Formulario é válido
    if (ValidarFormulario())
    {
        //recupera os dados
        var id = $("#PlanoId").val();
        var nomePlano = $("#NomePlano").val();
        var valor = $("#Valor").val();
        var mensalidades = $("#Mensalidades").val();
        var IdDescricao = $("#IdDescricao option:selected").val();
        

        // Cria um Objeto
        var plano = {
            PlanoId: id
            , NomePlano: nomePlano
            , Valor: valor
            , Mensalidades: mensalidades
            , IdDescricao: IdDescricao
            
        };

        var url = "/Plano/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "")
        {
            url = "/Plano/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url,
            datatype: "json",
            type: "POST",
            data: { plano: plano },
            success: function(data) {
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
            NomePlano: "required",
            Valor: "required",
            Mensalidades: "required",
            IdDescricao: "required",
        },
        messages: {
            NomePlano: "Informe o nome a descrição do plano",
            Valor: "informe o Valor",
            Mensalidades: "Selecione o tipo de mensalidade",
            IdDescricao: "Selecione a descricao",

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

function ExcluirPlano(id) {

    if (confirm('Deseja excluir este Plano?')) {
        var url = "/Plano/Excluir";

        $.ajax({
            url: url,
            type: 'POST',
            datatype: "json",
            data: { id: id },
            success: function(data) {
                if (data.Resultado) {
                    Listar();
                } else {
                    toastr.error('Erro ao excluir, Plano em utilização');
                }
            }
        });
    }


}