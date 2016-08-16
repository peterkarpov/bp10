using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.WebUI.Controllers;
using ESN.WebUI.Models;
using ESN.WebUI.HtmlHelpers;

namespace ESN.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
            {
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e1"), fName = "name1"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e2"), fName = "name2"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e3"), fName = "name3"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e4"), fName = "name4"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e5"), fName = "name5"},
            });
            ProfileController controller = new ProfileController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ProfilesListViewModel result = (ProfilesListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Profile> profiles = result.Profiles.ToList();
            Assert.IsTrue(profiles.Count == 2);
            Assert.AreEqual(profiles[0].fName, "name4");
            Assert.AreEqual(profiles[1].fName, "name5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e1"), fName = "name1"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e2"), fName = "name2"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e3"), fName = "name3"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e4"), fName = "name4"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e5"), fName = "name5"},
    });
            ProfileController controller = new ProfileController(mock.Object);
            controller.pageSize = 3;

            // Act
            ProfilesListViewModel result
                = (ProfilesListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Profiles()
        {
            // Организация (arrange)
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e1"), Gender = "male", fName = "name1"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e2"), Gender = "male", fName = "name2"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e3"), Gender = "female", fName = "name3"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e4"), Gender = "female", fName = "name4"},
                new Profile { ProfileId = Guid.Parse("747f1020-e55d-45a8-a2e8-7b14a81a75e5"), Gender = "female", fName = "name5"},
    });
            ProfileController controller = new ProfileController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Profile> result = ((ProfilesListViewModel)controller.List("male", 1).Model)
                .Profiles.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].fName == "name1" && result[0].Gender == "male");
            Assert.IsTrue(result[1].fName == "name2" && result[1].Gender == "male");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile> {
                new Profile { fName = "Игра1", Gender="Симулятор"},
                new Profile { fName = "Игра2", Gender="Симулятор"},
                new Profile { fName = "Игра3", Gender="Шутер"},
                new Profile { fName = "Игра4", Gender="RPG"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new Profile[] {
        new Profile { fName = "Игра1", Gender="Симулятор"},
        new Profile { fName = "Игра2", Gender="Шутер"}
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Profile_Count()
        {
            /// Организация (arrange)
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
        new Profile { fName = "Игра1", Gender="Cat1"},
        new Profile { fName = "Игра2", Gender="Cat2"},
        new Profile { fName = "Игра3", Gender="Cat1"},
        new Profile { fName = "Игра4", Gender="Cat2"},
        new Profile { fName = "Игра5", Gender="Cat3"}
    });
            ProfileController controller = new ProfileController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((ProfilesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProfilesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProfilesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProfilesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }


    }
}
