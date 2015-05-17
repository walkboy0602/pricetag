using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using PriceTag.Controllers;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using PriceTag.Service;
using PriceTag.Entities.Models;
using PriceTag.DAL;
using PriceTag.Services;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;

namespace PriceTag
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container
               .RegisterType<AccountDBContext>(new HierarchicalLifetimeManager())
               .RegisterType<IDataContextAsync, PriceTagContext>(new PerRequestLifetimeManager())
               .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType<AccountController>(new InjectionConstructor())
               .RegisterType<ManageController>(new InjectionConstructor())
               .RegisterType<BaseController>(new InjectionConstructor())
                // Repository
               .RegisterType<IRepositoryAsync<Tag>, Repository<Tag>>()
                // Services
               .RegisterType<IAzureService, AzureService>()
               .RegisterType<ITagService, TagService>()
               .RegisterType<IUserProfileService, UserProfileService>()
               ;

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}