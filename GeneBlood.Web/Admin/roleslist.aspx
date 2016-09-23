<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="roleslist.aspx.cs" Inherits="GeneBlood.Web.Admin.roleslist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cl pd-5 bg-1 bk-gray mt-20">
        <span class="l">
            <a href="roles.aspx"class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加角色</a>
        </span>
    </div>
    <div class="mt-20">
        <table class="table table-border table-bordered table-hover table-bg table-sort" border="0" cellpadding="0" cellspacing="0" >
            <tr class="text-c">
                <th>角色名称</th>
                <th>操 作</th>
            </tr>
            <%
                if (rolesList != null && rolesList.Count > 0)
                {
                    foreach (var item in rolesList)
                    {%>
            <tr class="text-c">
                <td><%=item.RoleName %></td>
                <td>
                    <a href="roles.aspx?action=update&rolesid=<%=item.RoleId %>"  class="btn btn-primary radius"><i class="Hui-iconfont">编 辑</i></a>
                </td>
            </tr>
            <%}
                }
            %>
        </table>
    </div>
    <script>
        function member_add(title, url, w, h) {
            layer_show(title, url, w, h);
        }
    </script>
</asp:Content>
