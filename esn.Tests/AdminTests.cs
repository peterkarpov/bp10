using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ESN.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Profiles()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
            {
                new Profile { ProfileId = Guid.NewGuid(), fName = "Игра1"},
                new Profile { ProfileId = Guid.NewGuid(), fName = "Игра2"},
                new Profile { ProfileId = Guid.NewGuid(), fName = "Игра3"},
                new Profile { ProfileId = Guid.NewGuid(), fName = "Игра4"},
                new Profile { ProfileId = Guid.NewGuid(), fName = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Profile> result = ((IEnumerable<Profile>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Игра1", result[0].fName);
            Assert.AreEqual("Игра2", result[1].fName);
            Assert.AreEqual("Игра3", result[2].fName);
        }

        [TestMethod]
        public void Can_Edit_Profile()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1"), fName = "Игра1"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2"), fName = "Игра2"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd3"), fName = "Игра3"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd4"), fName = "Игра4"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd5"), fName = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Profile Profile1 = controller.Edit(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1")).ViewData.Model as Profile;
            Profile Profile2 = controller.Edit(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2")).ViewData.Model as Profile;
            Profile Profile3 = controller.Edit(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd3")).ViewData.Model as Profile;

            // Assert
            Assert.AreEqual(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1"), Profile1.ProfileId);
            Assert.AreEqual(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2"), Profile2.ProfileId);
            Assert.AreEqual(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd3"), Profile3.ProfileId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Profile()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1"), fName = "Игра1"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2"), fName = "Игра2"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd3"), fName = "Игра3"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd4"), fName = "Игра4"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd5"), fName = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Profile result = controller.Edit(Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1")).ViewData.Model as Profile;

            // Assert
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Profile
            Profile profile = new Profile { fName = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(profile);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveProfile(profile));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Profile
            Profile profile = new Profile { fName = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(profile);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveProfile(It.IsAny<Profile>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Profiles()
        {
            // Организация - создание объекта Profile
            Profile profile = new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2"), fName = "Игра2" };

            // Организация - создание имитированного хранилища данных
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile>
    {
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd1"), fName = "Игра1"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd2"), fName = "Игра2"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd3"), fName = "Игра3"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd4"), fName = "Игра4"},
        new Profile { ProfileId = Guid.Parse("fb953de6-33f2-47cf-a0b0-073265d03bd5"), fName = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(profile.ProfileId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Profile
            mock.Verify(m => m.DeleteProfile(profile.ProfileId));
        }
    }
}