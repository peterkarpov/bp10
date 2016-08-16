using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using CodeFirst;

namespace ProfessorWeb.EntityFramework
{
    public partial class CascadeDelete : System.Web.UI.Page
    {
        protected void Save_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<SampleContext>());

            // Создать заказчика
            Customer customer = new Customer
            {
                FirstName = "Василий",
                LastName = "Пупкин",
                Age = 20,

                // Добавим заказы для этого покупателя
                Orders = new List<Order>
                {
                     new Order { ProductName = "Товар 1", Quantity = 4,
                                 PurchaseDate = DateTime.Now },
                     new Order { ProductName = "Товар 2", Quantity = 2,
                                 PurchaseDate = DateTime.Now },
                     new Order { ProductName = "Товар 3", Quantity = 5,
                                 PurchaseDate = DateTime.Now },
                }
            };

            // Вставить заказчика в базу данных
            SampleContext context = new SampleContext();

            context.Customers.Add(customer);
            context.SaveChanges();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            SampleContext context = new SampleContext();

            // Извлечь нужного покупателя из таблицы вместе с заказами
            Customer customer = context.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.FirstName == "Василий");

            // Удалить этого покупателя
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }
    }
}