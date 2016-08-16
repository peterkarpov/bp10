using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace GameStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Game с данными изображения
            Profile profile = new Profile
            {
                ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"),
                fName = "Игра2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile> {
                new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1"},
                profile,
                new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959843"), fName = "Игра3"}
            }.AsQueryable());

            // Организация - создание контроллера
            ProfileController controller = new ProfileController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"));

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(profile.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(m => m.Profiles).Returns(new List<Profile> {
                new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959841"), fName = "Игра1"},
                new Profile {ProfileId = Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959842"), fName = "Игра2"}
            }.AsQueryable());

            // Организация - создание контроллера
            ProfileController controller = new ProfileController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(Guid.Parse("3ddf6730-1f01-474c-bc4b-1af765959810"));

            // Утверждение
            Assert.IsNull(result);
        }
    }
}