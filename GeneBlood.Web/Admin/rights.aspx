<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="rights.aspx.cs" Inherits="GeneBlood.Web.Admin.rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="lib/icheck/icheck.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>父权限(菜单)：</label>
        <div class="formControls col-5">
            <asp:ListBox runat="server" ID="listRight" Height="247px" CssClass="select" AutoPostBack="true"
                Width="200px"></asp:ListBox>
            分配角色：
                                <asp:ListBox runat="server" ID="listRoles" Height="244px" CssClass="select"
                                    SelectionMode="Multiple" Width="200px" AutoPostBack="true"></asp:ListBox>
            排序数字:<asp:TextBox runat="server" ID="txtOrderNum" CssClass="input-text" Width="100" onkeyup="InputNumber(this)" Text="0"></asp:TextBox>
        </div>
        <div class="col-4"></div>
    </div>
    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>权限名称(菜单名称)：</label>
        <div class="formControls col-5">
            <div class="formControls col-5">
                <asp:TextBox runat="server" ID="txtRightName" Style="width: 440px;" CssClass="input-text"></asp:TextBox>

            </div>
            <div class="col-4"></div>
        </div>
    </div>
    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>Url (WebRoot相对路径):</label>
        <div class="formControls col-5">
            <div class="formControls col-5">
                <asp:TextBox runat="server" ID="txtUrl" CssClass="input-text" Style="width: 440px;"></asp:TextBox>
                <asp:HiddenField ID="txtRightId" runat="server" Value="0" />
                <asp:CheckBox runat="server" ID="chkDisplay"></asp:CheckBox>是否显示导航
            </div>
            <div class="col-4"></div>
        </div>
    </div>


    <div class="row cl">
        <label class="form-label col-3"><span class="c-red"></span></label>
        <div class="formControls col-5">
            <div class="col-9 col-offset-3">
                <asp:Button runat="server" Text="保存" ID="btn_AddRight" CssClass="btn btn-primary radius" OnClientClick="return checkRightOrRolesData();" OnClick="btn_AddRight_Click" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function checkRightOrRolesData() {
            var rights = $("#<%=listRight.ClientID %> option:selected");
            if (rights == undefined || rights.length == 0) {
                alert("请选中一个父菜单");
                return false;
            }

            var roles = $("#<%=listRoles.ClientID %> option:selected");
            if (roles.length == 0) {
                alert("请至少选中一个分配角色！");
                return false;
            }

            var rightName = $("#<%=txtRightName.ClientID %>").val();
            if (rightName.length == 0) {
                alert("请填写菜单名称！");
                return false;
            }

            var rightUrl = $("#<%=txtUrl.ClientID %>").val();
            if (rightUrl.length == 0) {
                alert("请填写菜单对于的Url地址！");
                return false;
            }
            if ($("#<%=chkDisplay.ClientID %>").attr("checked") == "checked") {
                if (confirm("您确定当前菜单(“" + rightName + "”)是作为导航菜单显示？")) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (confirm("您确定当前菜单(“" + rightName + "”)不作为导航菜单显示？")) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        $(function () {
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
        });
    </script>
</asp:Content>
