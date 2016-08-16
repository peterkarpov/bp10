using ESN.Domain.Entities;
using System.Data.Entity;
using System.Web;

namespace ESN.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            //: base(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + HttpContext.Current.Server.MapPath("~/App_Data/ProfileStore.mdf")
            //        + ";Integrated Security=True")
            : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Downloads\bp10\ESN.WebUI\App_Data\ESN3.mdf;Integrated Security=True")
        { }

        public EFDbContext(string connectionString) : base(nameOrConnectionString: connectionString)
        { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Photobook> Photobooks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Comment> Comments { get; set; }

       
    } 
}
