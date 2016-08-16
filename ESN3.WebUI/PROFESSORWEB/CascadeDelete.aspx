<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CascadeDelete.aspx.cs" 
    Inherits="ProfessorWeb.EntityFramework.CascadeDelete" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
        <asp:Button ID="Delete" runat="server" Text="Удалить" OnClick="Delete_Click" />
    </form>
</body>
</html>