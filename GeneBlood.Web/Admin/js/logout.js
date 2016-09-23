//退出
function LogOut() {
    if (confirm('是否退出系统？')) {
        var para = { "action": "logout" };
        ExecAJAX(LOGINURL, para, function (data) {
            if (data.ok == 0) {
                alert(data.msg);
                $.cookie('_userinfo', null);
                document.location.href = 'login.html';
            } else {
                alert(data.msg);
            }
        });
    }
}