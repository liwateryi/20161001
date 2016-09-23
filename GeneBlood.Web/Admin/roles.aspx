<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="GeneBlood.Web.Admin.roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>角色名称: </label>
        <div class="formControls col-5">
            <asp:TextBox runat="server" ID="txtRoles" CssClass="input-text"></asp:TextBox>
            <asp:HiddenField ID="txtRolesId" Value="0" runat="server" />
        </div>
        <div class="col-4"></div>
    </div>

    <div class="row cl">
        <label class="form-label col-3"><span class="c-red">*</span>角色权限:</label>
        <div class="formControls col-5">
            <asp:ListBox runat="server" ID="listRights" Height="196px" AutoPostBack="True" SelectionMode="Multiple"
                CssClass="select" Width="311px"></asp:ListBox>
            按住Ctrl键多选
        </div>
        <div class="col-4"></div>
    </div>
    <div class="row cl">
        <div class="col-9 col-offset-3">
            <asp:Button runat="server" Text="保存" ID="btnAddRoles" CssClass="btn btn-primary radius" OnClientClick="return CheckRoleData();" OnClick="btnAddRoles_Click" />
        </div>
    </div>

    <script type="text/javascript">
        function CheckRoleData() {
            var rolesName = $("#<%=txtRoles.ClientID%>").val();
             var roles = $("#<%=listRights.ClientID %> option:selected");
             if (rolesName.length == 0) {
                 alert("请填写角色名！");
                 return false;
             }
             if (roles.length == 0) {
                 alert("请至少选中一个权限分配！");
                 return false;
             }
             return true;
        }
        function layerclose() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
            parent.location.reload();
        }
    </script>
</asp:Content>
