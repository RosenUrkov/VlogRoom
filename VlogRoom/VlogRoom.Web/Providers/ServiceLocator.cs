using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Providers
{
    // my supermix of patterns and antipatterns in 20 lines
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public static IServiceLocator Provider { get; set; } =
            new ServiceLocator(DependencyResolver.Current.GetService<IKernel>());

        public T GetService<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}