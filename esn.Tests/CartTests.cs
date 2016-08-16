using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using ESN.Domain.Entities;
using ESN.Domain.Abstract;
using Moq;
using ESN.WebUI.Controllers;
using System.Web.Mvc;
using ESN.WebUI.Models;

namespace ESN.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация - создание нескольких тестовых игр
            Profile profile1 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1" };
            Profile profile2 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(profile1, 1);
            cart.AddItem(profile2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Profile, profile1);
            Assert.AreEqual(results[1].Profile, profile2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Организация - создание нескольких тестовых игр
            Profile Profile1 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1", dob = 100};
            Profile Profile2 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2", dob = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(Profile1, 1);
            cart.AddItem(Profile2, 1);
            cart.AddItem(Profile1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Profile.ProfileId).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых игр
            Profile Profile1 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1" };
            Profile Profile2 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2" };
            Profile Profile3 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959843"), fName = "Игра3" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - добавление нескольких игр в корзину
            cart.AddItem(Profile1, 1);
            cart.AddItem(Profile2, 4);
            cart.AddItem(Profile3, 2);
            cart.AddItem(Profile2, 1);

            // Действие
            cart.RemoveLine(Profile2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Profile == Profile2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация - создание нескольких тестовых игр
            Profile Profile1 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1", dob = 100 };
            Profile Profile2 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2", dob = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(Profile1, 1);
            cart.AddItem(Profile2, 1);
            cart.AddItem(Profile1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация - создание нескольких тестовых игр
            Profile Profile1 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1", dob = 100 };
            Profile Profile2 = new Profile { ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2", dob = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(Profile1, 1);
            cart.AddItem(Profile2, 1);
            cart.AddItem(Profile1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            Mock<IOrderProcessor> mock2 = new Mock<IOrderProcessor>();

            mock.Setup(m => m.Profiles).Returns(new List<Profile> {
        new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af76595984a"), fName = "Игра1", Gender = "Кат1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, mock2.Object);

            // Действие - добавить игру в корзину
            controller.AddToCart(cart, Guid.Parse("3ddf6730-1f01-474c-bc4b-1af76595984a"), null);

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Profile.ProfileId, Guid.Parse("3ddf6730-1f01-474c-bc4b-1af76595984a"));
        }

        /// <summary>
        /// После добавления игры в корзину, должно быть перенаправление на страницу корзины
        /// </summary>
        [TestMethod]
        public void Adding_Profile_To_Cart_Goes_To_Cart_Screen()
        {
            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            Mock<IOrderProcessor> mock2 = new Mock<IOrderProcessor>();


            mock.Setup(m => m.Profiles).Returns(new List<Profile> {
        new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af76595984a"), fName = "Игра1", Gender = "Кат1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, mock2.Object);

            // Действие - добавить игру в корзину
            RedirectToRouteResult result = controller.AddToCart(cart, Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), "myUrl");

            // Утверждение
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        // Проверяем URL
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController target = new CartController(null, null);

            // Действие - вызов метода действия Index()
            CartIndexViewModel result
                = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Утверждение
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация - создание пустой корзины
            Cart cart = new Cart();

            // Организация - создание деталей о доставке
            ShippingDetails shippingDetails = new ShippingDetails();

            // Организация - создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Действие
            ViewResult result = controller.Checkout(cart, shippingDetails);

            // Утверждение — проверка, что заказ не был передан обработчику 
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            // Утверждение — проверка, что метод вернул стандартное представление 
            Assert.AreEqual("", result.ViewName);

            // Утверждение - проверка, что-представлению передана неверная модель
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация — создание корзины с элементом
            Cart cart = new Cart();
            cart.AddItem(new Profile(), 1);

            // Организация — создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Организация — добавление ошибки в модель
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка перехода к оплате
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Утверждение - проверка, что заказ не передается обработчику
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            // Утверждение - проверка, что метод вернул стандартное представление
            Assert.AreEqual("", result.ViewName);

            // Утверждение - проверка, что-представлению передана неверная модель
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация — создание корзины с элементом
            Cart cart = new Cart();
            cart.AddItem(new Profile(), 1);

            // Организация — создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Действие - попытка перехода к оплате
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Утверждение - проверка, что заказ передан обработчику
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            // Утверждение - проверка, что метод возвращает представление 
            Assert.AreEqual("Completed", result.ViewName);

            // Утверждение - проверка, что представлению передается допустимая модель
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}