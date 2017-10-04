[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VlogRoom.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VlogRoom.Web.App_Start.NinjectWebCommon), "Stop")]

namespace VlogRoom.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using VlogRoom.Data.UnitOfWork;
    using Ninject.Extensions.Conventions;
    using System.Data.Entity;
    using VlogRoom.Data;
    using Data.Repository;
    using VlogRoom.Web.Common.ActionFilters;
    using VlogRoom.Web.Common.Attributes;
    using System.Web.Mvc;
    using VlogRoom.Services.Common.Contracts;
    using VlogRoom.Services.Common;
    using System.Reflection;
    using VlogRoom.Services.Data;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.From(Assembly.GetAssembly(typeof(LoggerService)).FullName,
                    Assembly.GetAssembly(typeof(UserDataService)).FullName)
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind(typeof(IUnitOfWork)).To(typeof(EfUnitOfWork));

            kernel.BindFilter<SaveChangesFilter>(FilterScope.Controller, 0).WhenActionMethodHas<SaveChangesAttribute>();
        }
    }
}
