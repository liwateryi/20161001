
//点击更换验证码
function changeCode() {
    $('#checkcode').attr('src', 'checkcode.aspx?r=' + Math.random());
}

//登录
function dologin() {
    var u = $("#u").val();
    var p = $("#p").val();
    var c = $("#c").val();
    if (checkData(u, p, c)) {
        var para = { "action": "dologin", "u": u, "p": p, "c": c }
       
        ExecAJAX(LOGINURL, para, function (data) {
            if (data.ok == 0) {
                document.location.href = 'index.aspx';
            } else {
                $("#imessage").text(data.msg);
            }
        });
    }
}

//检测登录
function checklogin() {

    var userinfo = $.cookie("_userinfo");
    if (userinfo != "null" && userinfo!="" && userinfo.length>0) {
        window.location = 'index.aspx';
    }
    /*
    var para = { "action": "checklogin" };
    ExecAJAX(LOGINURL, para, function (data) {
        if (data.ok == 0) {
            document.location.href = 'index.aspx';
        }
    });*/
}


//客户表单端验证
function checkData(u, p, c) {
    $("#imessage").text("");
    if (u == undefined || u.length == 0) {
        changeCode();
        $("#imessage").text("请输入登录名！");
        return false;
    }
    if (p == undefined || p.length == 0) {
        changeCode();
        $("#imessage").text("请输入密码！");
        return false;
    }
    if (c == undefined || c.length == 0) {
        changeCode();
        $("#imessage").text("请输入验证码！");
        return false;
    }
    $("#imessage").text("");
    $("#p").val("");
    $("#c").val("");
    return true;
}

$(function () {
    $("#sub_btn").click(function () {
        dologin();
    });

    $("#u").blur(function () {
        var u = $(this).val();
        if (u == undefined || u == 'undefined' || u.length == 0) {
            $("#imessage").text("请输入登录名！");
        } else {
            $("#imessage").text("");
        }
    });

    $("#p").blur(function () {
        var p = $(this).val();
        if (p == undefined || p == 'undefined' || p.length == 0) {
            $("#imessage").text("请输入密码！");
        } else {
            $("#imessage").text("");
        }
    });

    $("#c").blur(function () {
        var c = $(this).val();
        if (c == undefined || c == 'undefined' || c.length == 0) {
            $("#imessage").text("请输入验证码！");
        } else {
            $("#imessage").text("");
        }
    });
});


document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e && e.keyCode == 13) { // enter 键
        dologin();
    }
}; 


/*checklogin();*/