<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="rightslist.aspx.cs" Inherits="GeneBlood.Web.Admin.rightslist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cl pd-5 bg-1 bk-gray mt-20">
        <span class="l">
            <a href="rights.aspx" class="btn btn-primary radius"><i class="Hui-iconfont"></i>添加权限</a>
        </span>
    </div>
    <div class="mt-20">
        <table class="table table-border table-bordered table-hover table-bg table-sort" border="0" cellpadding="0" cellspacing="0">
            <tr class="text-c">
                <th>权限(菜单)名称
                </th>
                <th>Url
                </th>
                <th>是否作为菜单显示
                </th>
                <th>显示排序
                </th>
                <th>操作
                </th>
            </tr>
            <%if (rightList != null && rightList.Count > 0)
                {
                    //一级菜单
                    var parentMenu = rightList.FindAll(p => p.ParentId == 0);
                    foreach (var parent in parentMenu)
                    {%>
            <tr class="text-c">
                <td>
                    <span style="font-size: 18px; cursor: pointer;" onclick="displayList('rightChildMenu_<%=parent.Rid %>')">
                        <b>
                            <%=parent.RName %></b></span>
                </td>
                <td>
                    <%=parent.Url %>
                </td>
                <td>
                    <%=parent.IsDisplay ==0?"是":"否" %>
                </td>
                <td>
                    <%=parent.OrderByNum %>
                </td>
                <td>
                    <a href='rights.aspx?action=update&rightid=<%=parent.Rid %>' class="btn btn-primary radius"><i class="Hui-iconfont"></i>编 辑</a>
                    <a href='JavaScript:;' onclick="if(confirm('确定要删除该菜单吗？')){document.location.href = 'rightslist.aspx?action=delete&rightid=<%=parent.Rid %>'+'&r='+ Math.random()}" class="btn btn-primary radius"><i class="Hui-iconfont"></i>删 除</a>
                </td>
            </tr>
            <%
                //二级菜单
                var childMenu = rightList.FindAll(p => p.ParentId == parent.Rid);%>
            <%foreach (var child in childMenu)
                {%>
            <tr class="rightChildMenu_<%=parent.Rid %>" style="display: none;" class="text-c">
                <td>----<%=child.RName %>
                </td>
                <td>
                    <%=child.Url %>
                </td>
                <td>
                    <%=child.IsDisplay ==0?"是":"否" %>
                </td>
                <td>
                    <%=child.OrderByNum%>
                </td>
                <td>
                    <a href='rights.aspx?action=update&rightid=<%=child.Rid %>' class="btn btn-primary radius"><i class="Hui-iconfont"></i>编 辑</a>
                    <a href='JavaScript:;' onclick="if(confirm('确定要删除该菜单吗？')){document.location.href = 'rightslist.aspx?action=delete&rightid=<%=child.Rid %>'+'&r='+ Math.random()}" class="btn btn-primary radius"><i class="Hui-iconfont"></i>删 除</a>
                </td>
            </tr>
            <%}

                    }
                } %>
        </table>
    </div>
    <script type="text/javascript">
        function displayList(id) {
            //alert($("." + id).css("display"));
            if ($("." + id).css("display") == "none") {
                $("." + id).css("display", "");
            }
            else if ($("." + id).css("display") == "" || $("." + id).css("display") == "table-row") {
                $("." + id).css("display", "none");
            }
        }
    </script>
</asp:Content>
