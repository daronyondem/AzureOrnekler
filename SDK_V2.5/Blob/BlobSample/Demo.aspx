<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="BlobSample.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Create Container" OnClick="Button1_Click" />
        </div>
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="Button2" runat="server" Text="Upload File" OnClick="Button2_Click" />
        </div>
        <div>
            <asp:Button ID="Button3" runat="server" Text="Set Permissions" OnClick="Button3_Click" />
        </div>
        <div>
            <asp:Button ID="Button4" runat="server" Text="Delete all blobs" OnClick="Button4_Click" />
        </div>
    </form>
</body>
</html>

