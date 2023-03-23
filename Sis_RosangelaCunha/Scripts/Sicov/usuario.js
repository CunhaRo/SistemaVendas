$(document).ready(function () {

    Listar();

    $('#listausuarios').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
});

//Listar
function Listar() {
    var url = "/Usuario/Listar";
    
    var busca = $("#busca").val();

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        ,data:{busca:busca}
        , success: function (data) {
            $("#listarusuarios").html("");
            $("#listarusuarios").html(data);
        }
    });

}


//Novo
function NovoUsuario() {

    var url = "/Usuario/Novo";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , success: function (data) {
            AbrirModal('Novo Usuario', data);
            AplicarMascara();
            $(".datepicker").datepicker();
        }
    })
}

function EditarUsuario(id) {

    var url = "/Usuario/Editar";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , data: { id: id }
        , success: function (data) {
            AbrirModal('Editar Usuario', data);
            $("#UsuarioId").val(id);
            $(".datepicker").datepicker();
        }
    })
}

function SalvarUsuario() {

    //Verifica se o Formulario é válido

    if (ValidarFormulario())
    {
        //recupera os dados
        var id = $("#UsuarioId").val();
        var login = $("#Login").val();
        var senha = $("#Senha").val();
        var nome = $("#Nome").val();
        var email = $("#Email").val();
        var celular = $("#Celular").val();
        var perfilAcesso = $("#EnumPerfilAcesso").val();

        // Cria um Objeto
        var usuario = {
            UsuarioId: id
            , Login: login
            , Senha: senha
            , Nome: nome
            , Email: email
            , Celular: celular
            , EnumPerfilAcesso: perfilAcesso
        };

        var url = "/Usuario/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "")
        {
            url = "/Usuario/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url
            , datatype: "json"
            , type: "POST"
            , data: { usuario: usuario }
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

function ValidarFormulario() {
    // Validate signup form on keyup and submit
    $("#frmUsuario").validate({
        rules: {
            Login: "required",
            Senha: "required",
            Nome: "required",
            Email:{
                    required: true
                    , email: true
                },
            Celular: "required",
            EnumPerfilAcesso: "required",
        },
        messages: {
            Login: "Informe o nome do login",
            Senha: "Senha de 4 a 8 dígitos",
            Nome: "Informe o nome",
            Email: { required: "Informe o e-mail", email: "Informe um e-mail válido" },
            Celular: "Informe o número de celular",
            EnumPerfilAcesso: "Selecione o Perfil de Acesso",
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

    return $("#frmUsuario").valid();
}

function ExcluirUsuario(id) {

    if (confirm('Deseja excluir este Usuário?')) {
        var url = "/Usuario/Excluir";

        $.ajax({
            url: url
            , type: 'POST'
            , datatype: "json"
            , data: { id: id }
            , success: function (data) {
                if (data.Resultado) {
                    Listar();
                }
                else {
                    toastr.error('Erro ao excluir, verifique os dados e tente novamente');
                }
            }
        })
    }


}