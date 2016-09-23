<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="GeneBlood.Web.Admin.userlist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/pageclass.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-c">
        <!--
        日期范围：
		<input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" class="input-text Wdate" style="width: 120px;">
        -
		<input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" class="input-text Wdate" style="width: 120px;">
        <input type="text" class="input-text" style="width: 250px"  id="" name="">-->

        <asp:TextBox runat="server" ID="txtSeach" style="width: 250px" CssClass="input-text" placeholder="输入会员名称、电话、邮箱" ></asp:TextBox>
        <i class="Hui-iconfont">&#xe665;</i><asp:Button runat="server" Text="查 找" ID="btnSeachUser" CssClass="btn btn-success radius"
            OnClick="btnSeachUser_Click" />

<%--        <button type="submit" class="btn btn-success radius" id="" name=""><i class="Hui-iconfont">&#xe665;</i> 搜用户</button>--%>
    </div>
    <div class="cl pd-5 bg-1 bk-gray mt-20">
        <span class="l">
            <%--<a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</a>--%>
            <a href="user.aspx"  class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加用户</a>
        </span>
        <%--<span class="r">共有数据：<strong>88</strong> 条</span>--%>
    </div>
    <div class="mt-20">
        <table class="table table-border table-bordered table-hover table-bg table-sort">
            <thead>
                <tr class="text-c">
                    <th width="80">ID</th>
                    <th width="100">登录名</th>
                    <th width="100">真实姓名</th>
                    <th width="90">手机</th>
                    <th width="150">邮箱</th>
                    <th width="130">加入时间</th>
                    <th width="70">是否启用</th>
                    <th width="100">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repUserList">
                    <ItemTemplate>
                        <tr class="text-c">
                            <td>
                                <%#Eval("Id")%>
                            </td>
                            <td>
                                <%#Eval("LoginName")%>
                            </td>
                            <td>
                                <%#Eval("TrueName")%>
                            </td>
                            <td>
                                <%#Eval("Phone")%>
                            </td>
                             <td>
                                <%#Eval("Email")%>
                            </td>
                             <td>
                                <%#Eval("CTime")%>
                            </td>
                            <td>
                                <%#IsDelete(Eval("State"))%>
                            </td>
                            <td class="td-manage">
                                <%#IsDelete(Eval("State"), Eval("Id"))%>
                                <a href="user.aspx?action=update&userid=<%#Eval("Id") %>"  class="btn btn-primary radius"><i class="Hui-iconfont">编 辑</i></a>
                                <%--<a href="javascript:;" onclick="member_add('编辑用户','user.aspx?action=update&userid=<%#Eval("Id") %>','','510')" class="btn btn-primary radius"><i class="Hui-iconfont">编 辑</i></a>--%>
                                <a href="javascript:;" onclick="if(confirm('是否重置该用户密码为:123456 ？')){document.location.href = 'userlist.aspx?action=resetpwd&userid=<%#Eval("Id") %>'+'&r='+ Math.random()}" class="btn btn-primary radius"><i class="Hui-iconfont">密码重置</i></a>

<%--                               <input type="button" value="修改" class="btn btn82 btn_save2" onclick="goToUrl('user.aspx?action=update&userid=<%#Eval("Id") %>')" />
                                
                                <input type="button" value="密码重置" onclick="if(confirm('是否重置该用户密码为:123456 ？')){document.location.href = 'userlist.aspx?action=resetpwd&userid=<%#Eval("Id") %>'+'&r='+ Math.random()}"
                                    class="btn btn82 btn_save2" />
                                <input type="button" value="删除" onclick="if(confirm('此操作将彻底删除该用户数据，是否继续？')){document.location.href = 'userlist.aspx?action=dodelete&userid=<%#Eval("Id") %>        '+'&r='+ Math.random()}"
                                    class="btn btn82 btn_save2" />--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
                <%--<tr class="text-c">
                    <td>1</td>
                    <td><u style="cursor: pointer" class="text-primary" onclick="member_show('张三','member-show.html','10001','360','400')">张三</u></td>
                    <td>男</td>
                    <td>13000000000</td>
                    <td>admin@mail.com</td>
                    <td class="text-l">北京市 海淀区</td>
                    <td>2014-6-11 11:11:42</td>
                    <td class="td-status"><span class="label label-success radius">已启用</span></td>
                    <td class="td-manage">
                        <a style="text-decoration: none" onclick="member_stop(this,'10001')" href="javascript:;" title="停用"><i class="btn btn-success radius">停用</i></a> 
                        <a title="编辑" href="javascript:;" onclick="member_edit('编辑','member-add.html','4','','510')" class="ml-5" style="text-decoration: none"><i class="Hui-iconfont">&#xe6df;</i></a> 
                        <a style="text-decoration: none" class="ml-5" onclick="change_password('修改密码','change-password.html','10001','600','270')" href="javascript:;" title="修改密码"><i class="Hui-iconfont">&#xe63f;</i></a> 
                        <a title="删除" href="javascript:;" onclick="member_del(this,'1')" class="ml-5" style="text-decoration: none"><i class="Hui-iconfont">&#xe6e2;</i></a>
                    </td>
                </tr>--%>
            </tbody>
            <tr class="text-c">
                 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoText="" FirstPageText="首页"
                InputBoxClass="input" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                ShowCustomInfoSection="Left" SubmitButtonClass="btn" ShowFirstLast="true" ShowPrevNext="true"
                NumericButtonCount="5" ShowDisabledButtons="true" ShowPageIndexBox="Always" AlwaysShow="True"
                CustomInfoClass="CustomInfoClass" PageIndexBoxClass="srk" CustomInfoSectionWidth="30%"
                Height="0px" Width="1000px" CssClass="paginator" CurrentPageButtonClass="cpb"
                Visible="true" OnPageChanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
            </tr>
        </table>
    </div>
    <script>

        $(function () {
            $('.table-sort').dataTable({
                "aaSorting": [[1, "desc"]],//默认第几个排序
                "bStateSave": true,//状态保存
                "aoColumnDefs": [
                  //{"bVisible": false, "aTargets": [ 3 ]} //控制列的隐藏显示
                  { "orderable": false, "aTargets": [0, 8, 9] }// 制定列不参与排序
                ]
            });
            $('.table-sort tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });
        });
        /*用户-添加*/
        function member_add(title, url, w, h) {
            layer_show(title, url, w, h);
        }
        /*用户-查看*/
        function member_show(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        /*用户-停用*/
        function member_stop(obj, id) {
            layer.confirm('确认要停用吗？', function (index) {
                $(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="member_start(this,id)" href="javascript:;" title="启用"><i class="Hui-iconfont">&#xe6e1;</i></a>');
                $(obj).parents("tr").find(".td-status").html('<span class="label label-defaunt radius">已停用</span>');
                $(obj).remove();
                layer.msg('已停用!', { icon: 5, time: 1000 });
            });
        }

        /*用户-启用*/
        function member_start(obj, id) {
            layer.confirm('确认要启用吗？', function (index) {
                $(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="member_stop(this,id)" href="javascript:;" title="停用"><i class="Hui-iconfont">&#xe631;</i></a>');
                $(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">已启用</span>');
                $(obj).remove();
                layer.msg('已启用!', { icon: 6, time: 1000 });
            });
        }
        /*用户-编辑*/
        function member_edit(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        /*密码-修改*/
        function change_password(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        /*用户-删除*/
        function member_del(obj, id) {
            layer.confirm('确认要删除吗？', function (index) {
                $(obj).parents("tr").remove();
                layer.msg('已删除!', { icon: 1, time: 1000 });
            });
        }
        
        function IsDelete(msg, userid, isdelete) {
            if (confirm("是否" + msg + "该用户?")) {
                document.location.href = "userlist.aspx?action=delete&userid=" + userid + "&isdelete=" + isdelete + "&r=" + Math.random();
            }
        }
    </script>
</asp:Content>
