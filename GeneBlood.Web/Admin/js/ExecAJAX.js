var LOGINURL = "Ajax/login.ashx";

//执行ajax请求
function ExecAJAX(url, para, callback) {
    $.ajax({
        type: "post",
        dataType: "JSON",
        cache: false,
        async: false,
        data: para,
        url: url,
        brforeSend: function () { },
        complete: function (XMLHttpRequest, textStatus) { },
        success: callback,
        error: function () { alert("服务器异常，请稍后重试！"); }
    });
}