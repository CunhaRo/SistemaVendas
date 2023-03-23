//Index da Página
$(document).ready(function () {

    Listar();

    $('#listaclientes').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
    AplicarMascara();
});

function Listar() {
    var url = "/Cliente/Listar";

    var busca = $("#busca").val();


    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        , data : {busca : busca}
        , success: function (data){
            $("#listarclientes").html("");
            $("#listarclientes").html(data);
            
        }
    });

}

function NovoCliente() {

    var url = "/Cliente/Novo";
    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , success: function (data){
            AbrirModal('Novo Cliente', data);
            AplicarMascara();
            $(".datepicker").datepicker();
        }
    })
}

function EditarCliente(id) {

    var url = "/Cliente/Editar";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , data: { id: id }
        , success: function (data) {
            AbrirModal('Editar Cliente', data);
            $("#clienteId").val(id);
            $(".datepicker").datepicker();
            
        }
    })
}

function SalvarCliente() {
   
    if (ValidarFormulario())
    {

        var id = $("#ClienteId").val();
        var fantasia = $("#NomeFantasia").val();
        var razaoSocial = $("#RazaoSocial").val();
        var cnpj = $("#Cnpj").val();
        var ie = $("#Ie").val();
        var responsavelEmpresa = $("#ResponsavelEmpresa").val();
        var cpfResponsavel = $("#CpfResponsavel").val();
        var cidade = $("#Cidade").val();
        var estado = $("#EnumEstado").val();
        var cep = $("#Cep").val();
        var endereco = $("#Endereco").val();
        var bairro = $("#Bairro").val();
        var complemento = $("#Complemento").val();
        var email = $("#Email").val();
        var telefone1 = $("#Telefone1").val();
        var telefone2 = $("#Telefone2").val();
        var celular = $("#Celular").val();

        // Cria um Objeto
        var cliente = {
            ClienteId: id
            , NomeFantasia: fantasia
            , RazaoSocial: razaoSocial
            , Cnpj: cnpj
            , Ie: ie
            , ResponsavelEmpresa: responsavelEmpresa
            , CpfResponsavel: cpfResponsavel
            , Cidade: cidade + ""
            , EnumEstado: estado
            , Cep: cep + ""
            , Endereco: endereco + ""
            , Bairro: bairro + ""
            , Complemento: complemento + ""
            , Email: email + ""
            , Telefone1: telefone1 + ""
            , Telefone2: telefone2 + ""
            , Celular: celular + ""
        };

        var url = "/Cliente/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "")
        {
            url = "/Cliente/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url
            , datatype: "json"
            , type: "POST"
            , data: {cliente:cliente }
            , success: function (data) {
                if (data.Resultado) {
                    Listar();
                    toastr.success('Dados Gravados com Sucesso!');
                }

                $("#myModal").modal('hide');
            }
        })

    }
}

// Valida o Formulario
function ValidarFormulario() {


    $("#frmCliente").validate({
        rules: {
            NomeFantasia: {
                required: true
            },

            RazaoSocial: "required",

            Cnpj: "required",
            //ie: "required",

            ResponsavelEmpresa: "required",
            //cpfResponsavel: "required",

            Cidade: "required",

            EnumEstado: "required",
            //cep: "required",

            Endereco: "required",

            Bairro: "required",

            //complemento: "required",

            Email: "required",

            Telefone1: "required",
            //telefone2: "required",

            //Celular: "required",
            
        },
        messages: {
            NomeFantasia: { required: "Informe o nome Fantasia" },
            RazaoSocial:{ required: "Informe a Razão Social"},
            Cnpj: { required:"Informe o CNPJ"},
            //ie: "Informe a Inscrição Social",
            ResponsavelEmpresa: { required:"Informe o Responsável pela empresa"},
            //cpfResponsavel: "Informe o CPF do responsável",
            Cidade: { required:"Informe a Cidade"},
            EnumEstado: { required:"Informe o UF"},
            //cep: "Informe o CEP",
            Endereco:{ required: "Informe o endereço"},
            Bairro: { required:"Informe o bairro"},
            //complemento: "Informe o complemento",
            Email: { required:"Informe o e-mail"},
            Telefone1: { required: "Informe o telefone" }
            //elefone2: "Informe o telefone",
            //Celular: "Informe o numero de Celular",

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

    return $("#frmCliente").valid();
}

function ExcluirCliente(id) {

    if (confirm('Deseja excluir este registro?')) {
        var url = "/Cliente/Excluir";

        $.ajax({
            url: url
            , type: "POST"
            , datatype: "json"
            , data: { id: id }
            , success: function (data) {
                if (data.Resultado) {
                    Listar();
                }
                else {
                    toastr.error("Erro ao excluir, verifique os dados e tente novamente");
                }
            }
        });
    }


}