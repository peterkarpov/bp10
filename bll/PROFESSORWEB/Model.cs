using CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }

        // Заказанные покупателем товары
        public List<Product> Products { get; set; }

        public Profile Profile { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        // Список покупателей, заказавших этот товар
        public List<Customer> Customers { get; set; }
    }


    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        [ForeignKey("Customer")]
        public int UserId { get; set; }

        public Customer Customer { get; set; }
    }

    public class Profile
    {
        [Key]
        [ForeignKey("CustomerOf")]
        public int CustomerId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public Customer CustomerOf { get; set; }
    }

    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identifier { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Cost { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        public Address Address { get; set; }
    }

    [ComplexType]
    public class UserInfo
    {
        public int SocialNumber { get; set; }

        // Не является простым свойством
        public FullName FullName { get; set; }
    }

    public class FullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
