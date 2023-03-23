$(document).ready(function () {

    Listar();

    $('#listaaquisicoes').dataTable({
        "bPaginate": true,
        "bFilter": false,
        "bInfo": false
    });
});

//Listar
function Listar() {

    var url = "/AquisicaoContrato/Listar";
    
    var busca = $("#busca").val();

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        ,data:{busca:busca}
        , success: function (data) {
            $("#listaraquisicoes").html("");
            $("#listaraquisicoes").html(data);
        }
    });

}



function VerificarAgendamento()
{

    var url = "/AquisicaoContrato/Verificar";

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
    }).done(function (data) {
        AbrirModal("Agendamento", data);
        ListarAgendamento();
    }).fail(function (jqXHR, exception) {
        TratamendodeErro(jqXHR, exception);
    });
}



function ListarAgendamento()
{

    var url = "/Agendamento/ListarJson";

    $.ajax({

        url: url
        , datatype: "json"
        , type: "GET"
        , cache: false
    }).done(function (data) {

        

        if (data.Resultado.length > 0) {

            var dadosGrid = data.Resultado;

            $("#agendamento").append("<option value=''>Selecione</option>");

            $.each(dadosGrid, function (indice, item) {
                var opt = '<option value="' + item["Value"] + '">' + item["Text"] + '</option>';
                $("#agendamento").append(opt);
            });
        } else {

            NovoAquisicoesContratos();

        }
    });
}




//Novo
function NovoAquisicoesContratos() {


    var id = $("#agendamento option:selected").val();
    var check = $("#chkSemAgendamento").prop("checked");


    FecharModal();

    var url = "/AquisicaoContrato/Novo";

    if (id == "" || id == undefined) {
        id = 0;
    }


        $.ajax({
            url: url
            , datatype: 'html'
            , type: 'GET'
            , data: {idAgendamento : id , SemAgendamento : check }
            , success: function (data) {
                AbrirModal('Novo Aquisição Contrato', data);
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
        });
    
}

function EditarAquisicoesContratos(id) {

    var url = "/AquisicaoContrato/Editar";

    $.ajax({
        url: url
        , datatype: 'html'
        , type: 'GET'
        , data: { id: id }
        , success: function (data) {
            AbrirModal('Editar AquisicaoContrato', data);
            $("#AquisicaoId").val(id);
            $(".datepicker").datepicker();
        }
    })
}

function SalvarAquicoesContratos() {
    //Verifica se o Formulario é válido
    if (ValidarFormulario())
    {
        //recupera os dados
        var id = $("#AquisicaoId").val();
        var dataAquisicao = $("#DataAquisicao").val();
        var desconto = $("#Desconto").val();
        var migracoes = $("#Migracoes").val();
        var observacoes = $("#Observacoes").val();
        var loginSerasa1 = $("#loginSerasa1").val();
        var loginSerasa2 = $("#loginSerasa2").val();
        var loginSerasa3 = $("#loginSerasa3").val();
        var loginSerasa4 = $("#loginSerasa4").val();
        var loginSerasa5 = $("#loginSerasa5").val();
        var IdUsuario = $("#IdUsuario option:selected").val();
        var IdAgendamento = $("#IdAgendamento option:selected").val();
        var IdAvaliacao = $("#IdAvaliacao option:selected").val();
        var IdCliente = $("#IdCliente option:selected").val();
        var IdPlano = $("#IdPlano option:selected").val();

        // Cria um Objeto
        var aquisicao = {
            AquisicaoId: id
            , DataAquisicao: dataAquisicao
            , Desconto: desconto
            , Migracoes: migracoes
            , Observacoes: observacoes
            , LoginSerasa1: loginSerasa1
            , LoginSerasa2: loginSerasa2
            , LoginSerasa3: loginSerasa3
            , LoginSerasa4: loginSerasa4
            , LoginSerasa5: loginSerasa5
            , IdUsuario: IdUsuario
            , IdAgendamento: IdAgendamento
            , IdAvaliacao: IdAvaliacao
            , IdCliente: IdCliente
            , IdPlano: IdPlano
        };

        var url = "/AquisicaoContrato/Novo";

        // Caso o ID seja maior que ZERO Muda o URL
        if (id != undefined && id != "0" && id != "")
        {
            url = "/AquisicaoContrato/Editar";
        }

        // Faz a chamada AJAX
        $.ajax({
            url: url
            , datatype: "json"
            , type: "POST"
            , data: { aquisicao: aquisicao }
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
    $("#frmAquisicao").validate({
        rules: {
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

    return $("#frmAquisicao").valid();
}

function ExcluirAquisicaoContrato(id) {

    if (confirm('Deseja excluir esta Aquisição?')) {
        var url = "/AquisicaoContrato/Excluir";

        $.ajax({
            url: url,
            type: 'json',
            datatype: "POST",
            data: { id: id },
            success: function(data) {
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


        if (nome === ""){
            alert("Informe o nome do cliente");
            return false;
        }

        var url = "/Cliente/NovoClienteSimples";

        $.ajax({
                url: url,
                datatype: "json",
                type: "POST"
                , data : {nome:nome}
            })
            .done(function(data) {

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
