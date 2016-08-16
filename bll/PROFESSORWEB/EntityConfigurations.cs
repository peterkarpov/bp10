using System.Data.Entity.ModelConfiguration;

namespace CodeFirst
{
    // Настройки для Customer
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.Property(c => c.FirstName).IsRequired().HasMaxLength(30);
            this.Property(c => c.Email).HasMaxLength(100);
            this.Property(c => c.Photo).HasColumnType("image");
            this.ToTable("NewName_Customer");
        }
    }

    // Настройки для Order
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.Property(o => o.Description).HasMaxLength(500);
        }
    }
}