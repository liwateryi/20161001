<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="log.aspx.cs" Inherits="GeneBlood.Web.Admin.log" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/pageclass.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt-20">
        <div runat="server" id="loglist">
            <table class="table table-border table-bordered table-hover table-bg table-sort" border="0" cellpadding="0" cellspacing="0">
                <tr class="text-c">
                    <th>操作人
                    </th>
                    <th>事件
                    </th>
                    <th>IP 地址
                    </th>
                    <th>发生时间
                    </th>
                </tr>
                <asp:Repeater ID="repLogList" runat="server">
                    <ItemTemplate>
                        <tr class="text-c">
                            <td>
                                <%#Eval("UserName")%>
                            </td>
                            <td>
                                <%#Eval("LogContext")%>
                            </td>
                            <td>
                                <%#Eval("Ip")%>&nbsp;&nbsp;<span style="color: #459E23"><%#Eval("Address")%></span>
                            </td>
                            <td>
                                <%#Eval("LogTime")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="text-c">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoText="" FirstPageText="首页"
                        InputBoxClass="input" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                        ShowCustomInfoSection="Left" SubmitButtonClass="btn" ShowFirstLast="true" ShowPrevNext="true"
                        NumericButtonCount="5" ShowDisabledButtons="true" ShowPageIndexBox="Always" AlwaysShow="True"
                        CustomInfoClass="CustomInfoClass" PageIndexBoxClass="srk" CustomInfoSectionWidth="30%"
                        Height="0px" Width="1000px" CssClass="paginator" CurrentPageButtonClass="cpb"
                        Visible="true" OnPageChanged="AspNetPager1_PageChanged">
                    </webdiyer:AspNetPager>
            </table>
        </div>
    </div>
</asp:Content>
