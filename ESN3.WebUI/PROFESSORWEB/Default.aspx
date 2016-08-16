<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
    Inherits="ProfessorWeb.EntityFramework.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Code-First в Entity Framework</title>
</head>
<body>
    <div class="form">
        <form id="form1" runat="server" enctype="multipart/form-data">
            <h3>Данные заказчика</h3>
            <div class="data">
                <div>
                    <label>Имя</label>
                    <input name="firstname" />
                </div>
                <div>
                    <label>Фамилия</label>
                    <input name="lastname" />
                </div>
                <div>
                    <label>Email</label>
                    <input name="email" />
                </div>
                <div>
                    <label>Возраст</label>
                    <input name="age" />
                </div>
                <div>
                    <label>Фото</label>
                    <input type="file" name="photo" />
                </div>
                <input type="submit" value="Вставить в БД" />
            </div>
        </form>
    </div>

    <table>
        <asp:Repeater ID="Repeater1" runat="server" ItemType="ESN3.WebUI.PROFESSORWEB.CustomerSet"
            SelectMethod="GetCustomers">
            <HeaderTemplate>
                <tr>
                    <th>Id</th>
                    <th>Имя</th>
                    <th>Фамилия</th>
                    <th>Возраст</th>
                    <th>Email</th>
                    <th>Фото</th>
                </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Item.CustomerId %></td>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.Age + " лет" %></td>
                    <td><%# Item.Email %></td>
                    <td><img id="img" runat="server" src="<%# LoadImage(Item.Photo) %>" /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

    <style>
        .form { width: 470px; margin: 20px auto; background: #888; border-radius: 5px; padding: 5px;  }
        form { background: #fff; border-radius: 2px;  }
        .data { border-top: 1px solid #d5d5d5; padding: 10px 15px; }
        .data div { margin: 8px 0; }
        h3 { padding: 10px 15px; margin: 0; }
        label { min-width: 100px; display: block; float: left; }
        input[type="submit"] { margin-top: 10px; }
        table { margin: 10px auto }
        th, td { padding: 8px; }
        th { background: #28a4fa; color: white; font-weight: bold; }
        tr:nth-of-type(even) { background: #eee; }
        tr:nth-of-type(odd) { background: #fffbd6; }
        td img { max-width: 50px; max-height: 50px; }
    </style>
</body>
</html>