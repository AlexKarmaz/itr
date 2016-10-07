function LoadTemplate(e) {
    $.ajax({
        type: 'POST',
        url: "/Sites/LoadTemplate",
        data: { id: e.id },
        success: function (data) {
            $("#main-form").empty();
            $("#main-form").append(data);
        }
    });
    LoadScript();
}


function LoadMenuEditor() {
    $.ajax({
        type: 'POST',
        url: "/Sites/LoadMenuEditor",
        success: function (data) {
            $("#allbutId").empty();
            $("#allbutId").append(data);
            setTimeout(function () {
                LoadScript();
            }, 20);
        }
    });

}

function LoadScript() {
    $("#dragg1").draggable({ helper: "clone", scope: "push" });
    $("#dragg2").draggable({ helper: "clone", scope: "push" });
    $("#dragg3").draggable({ helper: "clone", scope: "push" });
    setTimeout(function () {
        $(".dropzone").droppable({
            scope: "push",
            over: function () {
                $(this).css("background-color");
            },
            drop: function (event, ui) {
                $(this).empty();
                let draggableId = ui.draggable.attr("id");

                if (draggableId == "dragg1") {
                    var text = $(this);
                    let word = null;
                    $(this).append($("#text").html());
                    tinymce.init({
                        selector: '#link'
                    });
                    $("#textinput").click(
                        function () {
                            word = tinyMCE.get("link").getContent();
                            text.empty();
                            text.append(word);
                            text.addClass(".selected");
                        });
                }


                if (draggableId == "dragg2") {
                    var image = $(this);
                    cloudinary.openUploadWidget(
                        {
                            cloud_name: 'dgy6x5krf', upload_preset: 'ntblzmxf', 'theme': 'purple'
                        }, function (error, result) {
                            //image.Id
                            //launchEditor("uploadImg", result[0].secure_url);
                            image.empty();
                            var contentImage = result[0].secure_url;
                            $(image).append("<img class=\"imaga\" src=\"" + contentImage + "\"  />");
                            
                        }
                        );

                }

                if (draggableId == "dragg3") {
                    var video = $(this);
                    var link = null;
                    $('#linkall').dialog({
                        title: 'Введите ссылку',
                        buttons: [{
                            text: "OK", click: function () {
                                link = $("#linkvideo").val();
                                if (link !== null) {
                                    $(video).append("<iframe width=\"100%\" height=\"99%\" src=\"" + link + "\" frameborder=\"0\" allowfullscreen></iframe>");
                                }
                                $(this).dialog("close")
                            }
                        }],
                        resizable: false
                    });

                }
            }
        });
    }, 20);
};

function SavePage() {
    //let html = $(".no-js").html();
    let html = $("#main-form").html();
    var savePage = {
        PageId: pageId,
        HtmlCode: html
    }

    $.ajax({
        type: 'POST',
        url: "/Page/SavePage",
        dataType: 'text',
        data: JSON.stringify(savePage),
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (data) {
            window.location.replace("https://localhost:44300/Sites/Details/"+siteId);
        }
    });
};

