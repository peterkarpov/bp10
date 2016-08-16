using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.Domain.Concrete;

namespace ESN.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Здесь размещаются привязки

            //        Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            //        mock.Setup(m => m.Profiles).Returns(new List<Profile>
            //{
            //            new Profile { UserId = Guid.Parse("ab4cb8e8-148d-4c9f-9272-831641ad925d"), fName = "ExampleFirstName", dob = default(DateTime) },
            //            new Profile { UserId = Guid.Parse("ab4cb8e8-148d-4c9f-9272-831641ad925e"), fName = "ExampleFirstName2", dob = default(DateTime) }
            //});
            //        kernel.Bind<IProfileRepository>().ToConstant(mock.Object);

            //kernel.Bind<IProfileRepository>().To<EFProfileRepository>();

            kernel.Bind<IProfileRepository>().To<EFProfileRepository>();

            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager
            //        .AppSettings["Email.WriteAsFile"] ?? "false")
            //};

            //kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            //    .WithConstructorArgument("settings", emailSettings);

            //kernel.Bind<IAuthProvider>().To<FormAuthProvider>();

        }
    }
}