//选择主题及初始化主题逻辑
//(function () {
//    $(".theme-colors > ul > li").click(function () {
//        var color = $(this).attr("data-style");
//        $.cookie('currentTheme', color, { expires: 7, path: '/' });
//    });
//    var currentTheme = $.cookie('currentTheme');
//    if (currentTheme != null && currentTheme) {
//        $('#style_color').attr("href", "/administration/content/assets/css/themes/" + currentTheme + ".css");
//    }
//})();

//表单验证
(function () {
    $(".form-horizontal .field-validation-error").each(function () {
        if ($(this).html() != "") {
            $(this).parent().parent().parent().addClass("has-error");
        }
    });
})();

//TODO:新菜单根据Url决定逻辑, 目前适配二级菜单
(function () {
    var locationHref = window.location.href.toLocaleLowerCase();
    $(".page-sidebar > ul > li > a").each(function () {

        //判断首页
        if (locationHref.indexOf($(this).attr("href")) > 0) {
            $(this).parent().addClass("active");

            $(this).append("<span class='selected'></span>");

            $("#navigation .page-title span").html($(this).text());
            $("#navigation .page-title small").html($(this).attr("title") || "");
            $("#navigation .breadcrumb li:eq(1) span").html($(this).text());
            $("#navigation .breadcrumb li:eq(1) i").remove();
            $("#navigation .breadcrumb li:eq(2)").remove();

            document.title = $(this).text() + " - " + document.title;

            return false;
        }
        else {
            var parent = $(this);
            $(this).next("ul").each(function () {
                $("a", $(this)).each(function () {
                    if (locationHref.indexOf($(this).attr("key")) > 0) {
                        $(this).parent().addClass("active");

                        parent.parent().addClass("active");
                        $(".arrow", parent).addClass("open").before("<span class='selected'></span>");

                        $("#navigation .page-title span").html($(this).text());
                        $("#navigation .page-title small").html($(this).attr("title") || "");
                        //$("#navigation .breadcrumb li:eq(1) span").html(parent.html());
                        //$("#navigation .breadcrumb li:eq(2) a").html($(this).html());
                        //$("#navigation .breadcrumb li:eq(2) a").attr($(this).attr("href"));

                        var htmlTemplate = "";                        
                        htmlTemplate += "<li>";
                        htmlTemplate += "<i class='" + parent.find("i").attr("class") + "'>" + "</i>";
                        htmlTemplate += "<span>" + parent.attr("title") + "</span>";
                        htmlTemplate += "<i class='fa fa-angle-right'></i>";
                        htmlTemplate += "</li>";
                        
                        htmlTemplate += "<li>";
                        htmlTemplate += "<i class='" + $(this).find("i").attr("class") + "'>" + "</i>";
                        htmlTemplate += "<a href='" + $(this).attr("href") + "'>" + $(this).attr("title") + "</a>";

                        if (locationHref.indexOf("edit") > 0 || locationHref.indexOf("create") > 0) {
                            htmlTemplate += "<i class='fa fa-angle-right'></i>";
                        }

                        htmlTemplate += "</li>";

                        if (locationHref.indexOf("edit") > 0 || locationHref.indexOf("create") > 0) {
                            htmlTemplate += $(".page-title-recover span").html();
                            $("#navigation .page-title span").html($(".page-title-recover span").text());
                            $("#navigation .page-title small").html($(".page-title-recover small").text() || "");
                        }

                        $("#navigation .breadcrumb").append(htmlTemplate);

                        document.title = $(this).text() + " - " + document.title;

                        return false;
                    }
                });
            });
        }
    });
})();

(function () {
    var isIE8Or9 = false;

    if (window.ActiveXObject) {
        var ua = navigator.userAgent.toLowerCase();
        var ie = ua.match(/msie ([\d.]+)/)[1]
        if (ie == 8.0 || ie == 9.0) {
            isIE8Or9 = true;
        }

        if (ie == 6.0) {
            alert("您的浏览器版本是IE6，在本系统中不能达到良好的视觉效果，建议你升级到IE8及以上！")
        }
    }

    if (!isIE8Or9) {
        //alert("您的浏览器版本不是IE8或IE9，在本系统中不能达到良好的视觉效果，建议你升级到IE8以上！")
    }
})();

$("#checkall").click(function () {
    var ischecked = this.checked;
    $("input:checkbox[name='ids']").each(function () {
        this.checked = ischecked;
    });

    $.uniform.update(':checkbox');
});

$("#delete").click(function () {
    var message = "你确定要删除勾选的记录吗?";
    if ($(this).attr("message"))
        message = $(this).attr("message") + "，" + message;
    if (confirm(message))
        $("#mainForm").submit();
});



