$(document)
    .ready(function() {

        if ($("#Id").val() !== "0") {
            ListarArquivos($("#Id").val());
        }

    });


function Upload() {
    

    if ($("#Id").val() == "0") {
        
    }

    myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));

}

///Listar Empresas
function ListarArquivos(idAquisicao) {

    var url = "/ItemAnexado/ListarArquivos";
    var data = { idEmpresa: idAquisicao }
    var div = $("#listaArquivos");

    if (idAquisicao == "" || idAquisicao == undefined)
        idAquisicao = parseInt($("#Id").val());

    $.ajax({
        url: url
        , data: data
        , cache: false
        , success: function (data) {
            div.empty();
            div.html(data);
        }
        , error: function (jqXHR, exception) {
            TratamendodeErro(jqXHR, exception);
        }
    });
}