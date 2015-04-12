<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="Samples.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Create Table" OnClick="Button1_Click" />
        </div>
        <div>
            <asp:Button ID="Button2" runat="server" Text="Insert Entity" OnClick="Button2_Click" />
        </div>
        <div>
            <asp:Button ID="Button3" runat="server" Text="Delete Entity" OnClick="Button3_Click" />
        </div>
        <div>
            <asp:Button ID="Button4" runat="server" Text="Transaction" />
        </div>
    </form>
</body>
</html>
