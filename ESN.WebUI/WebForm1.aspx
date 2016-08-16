<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ESN.WebUI.Profile1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>professorweb.ru - Entity Framework 6</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ListView ID="ListView1" runat="server" ItemType="ESN.WebUI.Profile1"
             SelectMethod="GetCustomers" UpdateMethod="EditCustomer" DeleteMethod="DeleteCustomer"
             InsertMethod="InsertCustomer" DataKeyNames="CustomerId">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>ID</th>
                        <th>Имя</th>
                        <th>Фамилия</th>
                        
                        <th></th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Item.userId %></td>
                    <td><%# Item.fName %></td>
                    <td><%# Item.dob %></td>
                    <td>
                        <asp:Button CommandName="Edit" runat="server" Text="Изменить" />
                        <asp:Button CommandName="Delete" runat="server" Text="Удалить" />
                    </td>
                </tr>
            </ItemTemplate>

            <EditItemTemplate>
                <tr>
                    <td><%# Item.userId %></td>
                    <td><input id="userId" runat="server" value="<%# BindItem.userId %>" /></td>
                    <td><input id="fName" runat="server" value="<%# BindItem.fName %>" /></td>
                    
                    <td>
                        <asp:Button CommandName="Update" runat="server" Text="Сохранить" />
                        <asp:Button CommandName="Delete" runat="server" Text="Отмена" />
                    </td>
                </tr>
            </EditItemTemplate>

            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td><input id="userId" runat="server" value="<%# BindItem.userId %>" /></td>
                    <td><input id="fName" runat="server" value="<%# BindItem.fName %>" /></td>
                    
                    <td>
                        <asp:Button ID="Button1" CommandName="Insert" runat="server" Text="Вставить" />
                    </td>
                </tr>
            </InsertItemTemplate>
        </asp:ListView>
    </form>

    <style>
        th, td { padding: 8px; }
        th { background: #28a4fa; color: white; font-weight: bold; }
        tr:nth-of-type(even) { background: #eee; }
        tr:nth-of-type(odd) { background: #fffbd6; }
    </style>
</body>
</html>