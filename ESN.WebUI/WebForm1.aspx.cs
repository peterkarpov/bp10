using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESN.WebUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ListView ListView1 = new ListView();

        Model1Container dbContext;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbContext = new Model1Container();
            ListView1.InsertItemPosition = InsertItemPosition.LastItem;
        }

        // Отобразить всех покупателей
        public IQueryable<Profile1> GetCustomers()
        {
            // Используем LINQ-запрос для извлечения данных
            return dbContext.Profile1Set.AsQueryable<Profile1>();
        }

        // Редактировать данные покупателя
        public void EditCustomer(Guid? userId)
        {
            Profile1 customer = dbContext.Profile1Set
                .Where(c => c.userId == userId).FirstOrDefault();

            if (customer != null && TryUpdateModel<Profile1>(customer))
            {
                // Обновить данные в БД с помощью Entity Framework
                dbContext.Entry<Profile1>(customer).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        // Удалить покупателя
        public void DeleteCustomer()
        {
            Profile1 customer = new Profile1();

            if (TryUpdateModel<Profile1>(customer))
            {
                dbContext.Entry<Profile1>(customer).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        // Вставить нового покупателя
        public void InsertCustomer()
        {
            Profile1 customer = new Profile1();

            if (TryUpdateModel<Profile1>(customer))
            {
                dbContext.Entry<Profile1>(customer).State = EntityState.Added;
                dbContext.SaveChanges();
            }
        }
    }
}