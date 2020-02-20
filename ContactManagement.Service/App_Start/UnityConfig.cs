using ContactManagement.DL.Models;
using ContactManagement.DL.Repository;
using ContactMgmt.BL.Service;
using ContactMgmt.BL.Service.Impl;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ContactManagement.Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<DbContext, ContactManagementDBContext>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<IContactService, ContactService>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}