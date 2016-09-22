<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="GeneBlood.Web.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btn_SendEmail" runat="server" Text="SendEmail" OnClick="btn_SendEmail_Click" />
         <asp:Button ID="btn_SendSMS" runat="server" Text="SendSMS" OnClick="btn_SendSMS_Click" />
    </div>
    </form>
</body>
</html>
