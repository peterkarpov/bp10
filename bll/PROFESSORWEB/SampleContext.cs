using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CodeFirst
{
    public class SampleContext : DbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public SampleContext() : base("MyShop")
        { }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Project> Projects { get; set; }


        // Переопределяем метод OnModelCreating для добавления
        // настроек конфигурации
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());

            modelBuilder.Entity<Project>().HasKey(p => p.Identifier);

            modelBuilder.Entity<Project>().Property(p => p.Identifier)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.ComplexType<Address>().Property(a => a.StreetAddress)
                .HasMaxLength(100);

            modelBuilder.Entity<Project>().Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Project>().Property(p => p.Cost)
                .HasPrecision(6, 3);

            modelBuilder.Entity<Profile>()
                .HasRequired(p => p.CustomerOf)
                .WithOptional(c => c.Profile);


            modelBuilder.Entity<Profile>()
                .HasOptional(p => p.CustomerOf)
                .WithOptionalDependent()
                .Map(p => p.MapKey("ProfileKey"));

            // Аналогичная настройка
            modelBuilder.Entity<Customer>()
                .HasOptional(c => c.Profile)
                .WithOptionalPrincipal()
                .Map(c => c.MapKey("CustomerKey"));


            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Customers)
                .Map(m =>
                {
                    // Ссылка на промежуточную таблицу
                    m.ToTable("Orders");

                    // Настройка внешних ключей промежуточной таблицы
                    m.MapLeftKey("CustomerId");
                    m.MapRightKey("ProductId");
                });



        }



    }
}