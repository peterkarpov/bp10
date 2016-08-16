using System;
using System.Data.Entity;
using CodeFirst;

namespace ProfessorWeb.EntityFramework
{
    public partial class DatabaseGenerated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Эта настройка нужна для того, чтобы база данных автоматически
            // удалялась и заново создавалась при изменении структуры модели
            // (чтобы было удобно тестировать примеры)
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<SampleContext>());

            SampleContext context = new SampleContext();

            Project project = new Project
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                Cost = null
            };

            context.Projects.Add(project);
            context.SaveChanges();
        }
    }
}