$(function () {
    $("#btn_pwd").click(function () {
        var p1 = $("#pd1").val();
        var p2 = $("#pd2").val();
        var p3 = $("#pd3").val();
        if (checkPwdData(p1, p2, p3)) {
            var data = { "action": "pwd", "p1": p1, "p2": p2, "p3": p3 };
            ExecAJAX(LOGINURL, data, function (data) {
                if (data.ok == 0) {
                    alert(data.msg);
                    parent.location = 'login.html'
                } else {
                    alert(data.msg);
                }
            });
        }
    });


    $("#pd1").blur(function () {
        var u = $(this).val();
        if (u == undefined || u == 'undefined' || u.length == 0) {
            $("#pwdmsg").text("请填写原始密码！");
        } else {
            $("#pwdmsg").text("");
        }
    });

    $("#pd2").blur(function () {
        var p = $(this).val();
        if (p == undefined || p == 'undefined' || p.length == 0) {
            $("#pwdmsg").text("请填写新密码！");
        } else {
            $("#pwdmsg").text("");
        }
    });

    $("#pd3").blur(function () {
        var c = $(this).val();
        if (c == undefined || c == 'undefined' || c.length == 0) {
            $("#pwdmsg").text("请填写确认密码！");
        } else {
            $("#pwdmsg").text("");
        }
    });
});

function checkPwdData(p1, p2, p3) {
    if (p1.length == 0 || p1==undefined || p1=="undefined") {
        $("#pwdmsg").text("请填写原始密码!");
        return false;
    }
    if (p2.length == 0 || p2 == undefined || p2 == "undefined") {
        $("#pwdmsg").text("请填写新密码!");
        return false;
    }
    if (p3.length == 0 || p3 == undefined || p3 == "undefined") {
        $("#pwdmsg").text("请填写确认密码!");
        return false;
    }
    if (p2 != p3) {
        $("#pwdmsg").text("新密码和确认密码不一致!");
        return false;
    }
    if (p1 == p2){
        $("#pwdmsg").text("修改后的密码不能和原密码一样!");
        return false;
    }
    if (p2.length < 6) {
        $("#pwdmsg").text("设置密码不能小于6个字符!");
        return false;
    }
    $("#pwdmsg").text("");
    return true;
}