//File Upload response from the server

//Dropzone.options.dropzoneForm = {

//    maxFiles: 5
//    ,dictDefaultMessage: "Arraste aqui os seus arquivos ou clique"
//    ,dictFallbackMessage: "Seu Navegador não suporta a função de arrastae e soltar arquivos"
//    ,dictFallbackText: "Please use the fallback form below to upload your files like in the olden days."
//    ,dictFileTooBig: "Arquivo é muito grande ({{filesize}}MiB). Máximo tamanho arquivo : {{maxFilesize}}MiB."
//    ,dictInvalidFileType: "Você não pode carregar este tipo de arquivo"
//    ,dictResponseError: "Erro de Servidor Código:{{statusCode}}."
//    ,dictCancelUpload: "Upload Cancelado"
//    ,dictCancelUploadConfirmation: "Tem certeza que deseja cancelar este upload?"
//    ,dictRemoveFile: "Arquivo removido"
//    ,dictMaxFilesExceeded: "Você não pode carregar mais arquivos."
//    , autoDiscover : false
//    , url: "/Empresa/SaveFiles"
//    , success: function (file, response) {
//        var imgName = response;
//        file.previewElement.classList.add("dz-success");
//    }
//    , init: function () {
        
//        this.on("maxfilesexceeded", function (data) {
//            var res = eval('(' + data.xhr.responseText + ')');
//        });

//        this.on("addedfile", function (file) {
//            // Create the remove button
//            var removeButton = Dropzone.createElement("<a href='#' class='btn btn-danger'>Excluir arquivo</a>");

//            // Capture the Dropzone instance as closure.
//            var _this = this;

//            // Listen to the click event
//            removeButton.addEventListener("click", function (e) {

//                // Make sure the button click doesn't submit the form:
//                e.preventDefault();

//                e.stopPropagation();

//                // Remove the file preview.
//                _this.removeFile(file);

//                // If you want to the delete the file on the server as well,
//                // you can do the AJAX request here.
//            });

//            // Add the button to the file preview element.
//            file.previewElement.appendChild(removeButton);
//        });
//    }
//};



//Dropzone.autoDiscover = false;
//$("#dropzoneForm").dropzone({
//    addRemoveLinks: true
//    , success: function (file, response) {
//        var imgName = response;
//        file.previewElement.classList.add("dz-success");
//    }
//    ,error: function (file, response) {
//        file.previewElement.classList.add("dz-error");
//    }
//});


//var previewNode = document.querySelector("#template");
//previewNode.id = "";
//var previewTemplate = previewNode.parentNode.innerHTML;
//previewNode.parentNode.removeChild(previewNode);

//var myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
//    url: "/Empresa/ProcessRequest", // Set the url
//    thumbnailWidth: 80,
//    thumbnailHeight: 80,
//    parallelUploads: 20,
//    previewTemplate: previewTemplate,
//    autoQueue: false, // Make sure the files aren't queued until manually added
//    previewsContainer: "#previews", // Define the container to display the previews
//    clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
//});

    // Get the template HTML and remove it from the doumenthe template HTML and remove it from the doument

    var previewNode = document.querySelector("#template");
    previewNode.id = $("#Id").val();
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);


    var myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
        url: "/ItemAnexado/SaveFiles/", // Set the url
        thumbnailWidth: 80,
        thumbnailHeight: 80,
        parallelUploads: 20,
        previewTemplate: previewTemplate,
        autoQueue: false, // Make sure the files aren't queued until manually added
        previewsContainer: "#previews", // Define the container to display the previews
        clickable: ".fileinput-button", // Define the element that should be used as click trigger to select files.
        acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg,.pdf,.doc,.docx,.xls,.xlsx,.csv,.txt",

        //removedfile: function (file) {
        //    var name = file.name;
        //    $.ajax({
        //        type: 'POST',
        //        url: '/Empresa/RemovedFile',
        //        data: { file : name, Id : $("#Id").val() },
        //        dataType: 'json'
        //    });

        //    // Exclui a linha da Tela
        //    var _ref;
        //    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
        //},

        init: function () {

            //Acrecento o ID da Empresa para Salvar o Registro
            this.on("sending", function (file, xhr, formData) {

                formData.append("Id", $("#Id").val());
            });


            this.on("addedfile", function (file) {
                // Caso o arquivo não seja permitido
                //if (!file.accepted) {
                //    Message("Formato arquivo inválido", 'erro');
                //    this.removeFile(file);
                //    return;
                //}

                var ext = file.name.split('.').pop();
                ext = '' + ext.toLowerCase();

                console.log(file);

                if (ext != "png"
                    && ext != "jpg"
                    && ext != "JPG"
                    && ext != "gif"
                    && ext != "bmp"
                    && ext != "pdf"
                    && ext != "doc"
                    && ext != "docx"
                    && ext != "xls"
                    && ext != "csv"
                    && ext != "txt"
                    ) {
                    Message("Formato arquivo inválido", 'erro');
                    this.removeFile(file);
                }
            });
        }
    });

    myDropzone.on("addedfile", function (file) {

        // Hookup the start button
        file.previewElement.querySelector(".start").onclick = function () { myDropzone.enqueueFile(file); };
    });

    // Update the total progress bar
    //myDropzone.on("totaluploadprogress", function (progress) {
    //    document.querySelector("#total-progress .progress-bar").style.width = progress + "%";
    //});

    myDropzone.on("sending", function (file) {

        // Show the total progress bar when upload starts
        //document.querySelector("#total-progress").style.opacity = "1";
        // And disable the start button
        file.previewElement.querySelector(".start").setAttribute("disabled", "disabled");
    });

    // Hide the total progress bar when nothing's uploading anymore
    myDropzone.on("queuecomplete", function (progress) {
        document.querySelector("#total-progress").style.opacity = "0";
    });

    // Setup the buttons for all transfers
    // The "add files" button doesn't need to be setup because the config
    // `clickable` has already been specified.
    document.querySelector("#actions .start").onclick = function () {
        myDropzone.URL = "/Empresa/SaveFiles/" + $("#Id").val();
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };

    document.querySelector("#actions .cancel").onclick = function () {
        myDropzone.removeAllFiles(true);
    };