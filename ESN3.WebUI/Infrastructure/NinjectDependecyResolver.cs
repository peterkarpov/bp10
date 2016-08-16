using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.Domain.Concrete;
using ESN3.WebUI.Infrastructure.Abstract;
using ESN3.WebUI.Infrastructure.Concrete;

namespace ESN3.WebUI.Infrastructure
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
            kernel.Bind<IProfileRepository>().To<EFProfileRepository>();
            kernel.Bind<IOtherRepository>().To<EFOtherRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();



        }
    }
}