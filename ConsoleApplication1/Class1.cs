using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MinLength(6)]
        [MaxLength(100)]
        public string Email { get; set; }

        public int Age { get; set; }
        public byte[] Photo { get; set; }

        // Ссылка на заказы
        public virtual List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }

        [StringLength(500, MinimumLength = 5)]
        public string ProductName { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Ссылка на покупателя
        public Customer Customer { get; set; }
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().Property(c => c.FirstName)
            .HasMaxLength(30);

        modelBuilder.Entity<Customer>().Property(c => c.Email)
            .HasMaxLength(100);

        modelBuilder.Entity<Order>().Property(o => o.ProductName)
            .HasMaxLength(500);
    }
}