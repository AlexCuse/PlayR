using System;
using System.Web.Mvc;
using System.Web.Routing;
using PlayR.Core;
using PlayR.Hubs;
using PlayR.Infrastructure;
using SignalR;
using SignalR.Hosting.AspNet;
using SignalR.Hubs;
using SignalR.Infrastructure;
using StructureMap;
using IDependencyResolver = SignalR.IDependencyResolver;

namespace PlayR
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        DatabaseMessageLogger logger = new DatabaseMessageLogger();

        protected void Application_Start()
        {
            //ObjectFactory.Initialize(c => {
            //    c.For<IMessageLogger>().Singleton().Use(() => logger);
            //    c.For<Chat>().Use<Chat>();
            //    c.For<IDependencyResolver>().Add<StructureMapResolver>();
            //});

            //GlobalHost.DependencyResolver = ObjectFactory.GetInstance<IDependencyResolver>();
            RouteTable.Routes.MapHubs();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            logger.Dispose();
        }
    }
}