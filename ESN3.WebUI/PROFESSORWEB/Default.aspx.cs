using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.IO;
using ESN3.WebUI.PROFESSORWEB;

namespace ProfessorWeb.EntityFramework
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                CustomerSet customer = new CustomerSet();

                // Получить данные из формы с помощью средств
                // привязки моделей ASP.NET
                IValueProvider provider =
                    new FormValueProvider(ModelBindingExecutionContext);
                if (TryUpdateModel<CustomerSet>(customer, provider))
                {
                    // Загрузить фото профиля с помощью средств .NET
                    HttpPostedFile photo = Request.Files["photo"];
                    if (photo != null)
                    {
                        BinaryReader b = new BinaryReader(photo.InputStream);
                        customer.Photo = b.ReadBytes((int)photo.InputStream.Length);
                    }

                    // В этой точке непосредственно начинается работа с Entity Framework

                    // Создать объект контекста
                    MyShop1Entities context = new MyShop1Entities();

                    // Вставить данные в таблицу Customers с помощью LINQ
                    context.CustomerSet.Add(customer);

                    // Сохранить изменения в БД
                    context.SaveChanges();

                    Repeater1.DataBind();
                }
            }
        }

        // Загрузить данные всех пользователей
        public IEnumerable<CustomerSet> GetCustomers()
        {
            // Создать объект контекста
            MyShop1Entities context = new MyShop1Entities();

            return context.CustomerSet.AsQueryable();
        }

        // Загрузить изображение из базы данных
        public string LoadImage(byte[] photo)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String(photo);
        }
    }
}