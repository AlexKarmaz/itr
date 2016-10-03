$(function () {
    $(".thumb img").click(function () {
        $(".thumb")
            .children()
            .each((i, element) =>
                $(element).removeClass("selected"));
        $(this).addClass("selected");
    });

    $(".thumbMenu img").click(function () {
        $(".thumbMenu")
            .children()
            .each((i, element) =>
                $(element).removeClass("selected"));
                $(this).addClass("selected");        
    });


    $("#save").click(
    function () {
        var url = window.location.href;
        let selectedTemplate = null;
        let selectedMenu = null;
        $(".thumb img.selected").each(function (index, element) {
            selectedTemplate = element.id;
        })
        $(".thumbMenu img.selected").each(function (index, element) {
            selectedMenu = $(element).attr("data-id");
        })        

            var site = {
                'Title': $("#Title").val(),
                'Description': $("#Description").val(),
                'TemplateId': selectedTemplate,
                'MenuId': selectedMenu
            };          

            $.ajax({
                url: 'Create',
                type: 'POST',
                contentType: 'application/json',
                traditional: true,
                success: function (response) {
                    if (response.result == 'Redirect')
                        window.location = response.url;
                },
                fail: function () {
                    alert(data);
                },
                data: JSON.stringify({ site: site})
            });      

    });
})





