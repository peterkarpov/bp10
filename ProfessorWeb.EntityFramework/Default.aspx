<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProfessorWeb.EntityFramework.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>professorweb.ru - Entity Framework 6</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ListView ID="ListView1" runat="server" ItemType="ProfessorWeb.EntityFramework.Customer"
             SelectMethod="GetCustomers" UpdateMethod="EditCustomer" DeleteMethod="DeleteCustomer"
             InsertMethod="InsertCustomer" DataKeyNames="CustomerId">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>ID</th>
                        <th>Имя</th>
                        <th>Фамилия</th>
                        <th>Город</th>
                        <th>Возраст</th>
                        <th></th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Item.CustomerId %></td>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.City %></td>
                    <td><%# Item.Age %></td>
                    <td>
                        <asp:Button CommandName="Edit" runat="server" Text="Изменить" />
                        <asp:Button CommandName="Delete" runat="server" Text="Удалить" />
                    </td>
                </tr>
            </ItemTemplate>

            <EditItemTemplate>
                <tr>
                    <td><%# Item.CustomerId %></td>
                    <td><input id="firstName" runat="server" value="<%# BindItem.FirstName %>" /></td>
                    <td><input id="lastName" runat="server" value="<%# BindItem.LastName %>" /></td>
                    <td><input id="city" runat="server" value="<%# BindItem.City %>" /></td>
                    <td><input id="age" runat="server" value="<%# BindItem.Age %>" /></td>
                    <td>
                        <asp:Button CommandName="Update" runat="server" Text="Сохранить" />
                        <asp:Button CommandName="Delete" runat="server" Text="Отмена" />
                    </td>
                </tr>
            </EditItemTemplate>

            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td><input id="firstName" runat="server" value="<%# BindItem.FirstName %>" /></td>
                    <td><input id="lastName" runat="server" value="<%# BindItem.LastName %>" /></td>
                    <td><input id="city" runat="server" value="<%# BindItem.City %>" /></td>
                    <td><input id="age" runat="server" value="<%# BindItem.Age %>" /></td>
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