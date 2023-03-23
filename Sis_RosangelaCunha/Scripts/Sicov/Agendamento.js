$(document).ready(function () {

    Listar();

    $('#listaagendamentos').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
});

//Listar
function Listar() {

    var url = "/Agendamento/Listar";

    var busca = $("#busca").val();

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        , data: { busca: busca }
        , success: function (data) {
            $("#listaragendamentos").html("");
            $("#listaragendamentos").html(data);
            AplicarDataTable("listaagendamentos");
        }
    });

}


//Novo
function NovoAgendamento() {
    var url = "/Agendamento/Novo";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , success: function (data) {
            AbrirModal('Novo Agendamento', data);
            AplicarDatePicker();
            AplicarMascara();

            setTimeout(function () {
                $("#IdCliente").select2({
                    placeholder: "Selecione",
                    allowClear: true,
                    theme: "bootstrap"
                });

            }, 200);

        }
    })
}

function EditarAgendamento(id) {

    var url = "/Agendamento/Editar";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , data: { id: id }
        , success: function (data) {
            AbrirModal('Editar Agendamento', data);
            $("#AgendamentoId").val(id);
            $(".datepicker").datepicker();
        }
    })
}

function SalvarAgendamento() {

    //Verifica se o Formulario é válido

    if (ValidarFormulario()) {
        //recupera os dados
        var id = $("#AgendamentoId").val();
        var dataAgendamento = $("#DataAgendamento").val();
        var horarioAgendamento = $("#HorarioAgendamento").val();
        var indicacao = $("#Indicacao").val();
        var indicadopor = $("#IndicadoPor").val();
        var status = $("#Status option:selected").val();
        var IdCliente = $("#IdCliente option:selected").val();
        var IdUsuario = $("#IdUsuario").val();

        // Cria um Objeto
        var agendamentoVM = {
            AgendamentoId: id
            , DataAgendamento: dataAgendamento
            , HorarioAgendamento: horarioAgendamento
            , Indicacao: indicacao
            , IndicadoPor: indicadopor
            , Status: status
            , IdUsuario: IdUsuario
            , IdCliente: IdCliente
        };

        var url = "/Agendamento/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "") {
            url = "/Agendamento/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url
            , datatype: "json"
            , type: "POST"
            , data: { agendamentoVM: agendamentoVM }
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
    $("#frmAgendamento").validate({
        rules: {
            bonificacao: "required",
            dataVencimento: "required",
            dataAquisicao: "required",
            desconto: "required",
            migracoes: "required",
            observacoes: "required",
            loginSerasa1: "required",
            loginSerasa2: "required",
            loginSerasa3: "required",
            loginSerasa4: "required",
            loginSerasa5: "required",
            IdUsuario: "required",
            IdAgendamento: "required",
            IdAvaliacao: "required",
            IdCliente: "required",
            IdPlano: "required",
        },
        messages: {
            login: "Informe o nome do login",
            senha: "Senha de 4 a 8 dígitos",
            nome: "Informe o nome",
            email: "Informe o e-mail",
            celular: "Informe o número de celular",
            perfilAcesso: "Selecione o Perfil de Acesso",

            bonificacao: "Informe se há Bonificação de Produtos Gratis",
            dataVencimento: "A data de Vencimento será 07,10 ou 15?",
            dataAquisicao: "Informe a data de Aquisição",
            desconto: "Informe se há desconto",
            migracoes: "Informe se há migrações",
            observacoes: "Algo há relatar?",
            loginSerasa1: "Login Master",
            loginSerasa2: "Informe o Login",
            loginSerasa3: "Informe o Login",
            loginSerasa4: "Informe o Login",
            loginSerasa5: "Informe o Login",
            IdUsuario: "Selecione",
            IdAgendamento: "Selecione",
            IdAvaliacao: "Selecione",
            IdCliente: "Selecione",
            IdPlano: "Selecione",
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

    return $("#frmAgendamento").valid();
}

function ExcluirAquisicaoContrato(id) {

    if (confirm('Deseja excluir esta Aquisição?')) {
        var url = "/AquisicaoContrato/Excluir";

        $.ajax({
            url: url,
            type: 'json',
            datatype: "POST",
            data: { id: id },
            success: function (data) {
                if (data.Resultado) {
                    Listar();
                } else {
                    toastr.error('Erro ao excluir, verifique os dados e tente novamente');
                }
            }
        });
    }
}

function NovoCliente() {

    $("#myModalCliente").modal();

}

function SalvarClienteSimples() {

    var nome = $("#nomecliente").val();


    if (nome === "") {
        alert("Informe o nome do cliente");
        return false;
    }

    var url = "/Cliente/NovoClienteSimples";

    $.ajax({
        url: url,
        datatype: "json",
        type: "POST"
            , data: { nome: nome }
    })
        .done(function (data) {

            if (data.Resultado) {
                $("#myModalCliente").modal('hide');

                AtualizarDDLCliente();
            }

        })
        .fail();


}

function AtualizarDDLCliente() {

    var url = "/Cliente/ListarClientes";

    $("#IdCliente").empty();

    $.ajax({
        url: url
        , datatype: "json"
        , type: "GET"
        , cache: false
        , success: function (data) {
            if (data.Resultado.length > 0) {

                var dadosGrid = data.Resultado;

                $("#IdCliente").append('<option value="">Selecione</option>');

                $.each(dadosGrid, function (indice, item) {

                    var opt = "";


                    opt = '<option value="' + item["Value"] + '">' + item["Text"] + '</option>';


                    $("#IdCliente").append(opt);
                });
            }

        }
        , error: function (jqXHR, exception) {
            TratamendodeErro(jqXHR, exception);
        }
    });
}
