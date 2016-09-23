<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="GeneBlood.Web.Admin.user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="lib/icheck/icheck.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row cl">
        <label class="form-label col-3">
            <span class="c-red">*</span>登录名:
            <asp:HiddenField runat="server" ID="txtUserId" Value="0"></asp:HiddenField>
        </label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtLoginName" CssClass="input-text"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>
    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>真实姓名:</label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtTrueName" CssClass="input-text"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red"></span>QQ账号:</label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtQQ" CssClass="input-text"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red"></span>手机号码:</label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtPhone" CssClass="input-text"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red"></span>邮箱账号:</label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtEmail" CssClass="input-text"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>所属角色:</label>
        <div class="formControls col-5">
            <asp:ListBox runat="server" ID="listRoles" Height="217px" Width="236px" AutoPostBack="True"
                SelectionMode="Multiple" CssClass="select"></asp:ListBox>
            按住Ctrl键多选
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl" style="display:none;">
        <label class="form-label col-3"><span class="c-red"></span>个人简介:</label>
        <div class="formControls col-5">
            <asp:TextBox ID="txtIntro" runat="server" Height="238px" TextMode="MultiLine"
                Width="486px"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red"></span></label>
        <div class="formControls col-5">
            <asp:Button runat="server" Text="保存" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return CheckData();"
                CssClass="btn btn-primary radius" /> （用户初始密码为:123456）<span onclick="layerclose();">test</span>
        </div>
    </div>

    <script type="text/javascript">
        function CheckData() {
            var loginName = $("#<%=txtLoginName.ClientID %>").val();
            var trueName = $("#<%=txtTrueName.ClientID %>").val();
            var roles = $("#<%=listRoles.ClientID %> option:selected");
            if (loginName.length == 0) {
                alert("请填写登录名!");
                return false;
            }
            if (trueName.length == 0) {
                alert("请填写真实姓名!");
                return false;
            }
            if (roles.length == 0) {
                alert("请至少选中一个角色分配！");
                return false;
            }
            return true;
        }
        $(function () {
            /*
            $('.skin-minimal input').iCheck({
                checkboxClass: 'icheckbox-blue',
                radioClass: 'iradio-blue',
                increaseArea: '20%'
            });
           

            $("#form_admin_add").Validform({
                tiptype: 2,
                callback: function (form) {
                    form[0].submit();
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.$('.btn-refresh').click();
                    parent.layer.close(index);
                }
            });
             */
        });
        
        function layerclose() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        }
    </script>
</asp:Content>


