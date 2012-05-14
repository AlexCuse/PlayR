using System.Web.Mvc;
using System.Web.Routing;
using PlayR.Core;
using PlayR.Hubs;
using PlayR.Infrastructure;
using SignalR;
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
            ObjectFactory.Initialize(c =>
            {
                c.For<IMessageLogger>().Singleton().Use<DatabaseMessageLogger>();
                c.Scan(scanner =>
                           {
                               scanner.TheCallingAssembly();
                               scanner.AddAllTypesOf<Hub>();
                           });

                //defaults
                /*
                c.For<IMessageBus>().Use<InProcessMessageBus>();
                c.For<IConnectionIdGenerator>().Use<GuidConnectionIdGenerator>();
                c.For<IAssemblyLocator>().Use<DefaultAssemblyLocator>();
                c.For<IJavaScriptProxyGenerator>().Use<DefaultJavaScriptProxyGenerator>();
                c.For<IJavaScriptMinifier>().Use<NullJavaScriptMinifier>();
                c.For<IJsonSerializer>().Use<JsonConvertAdapter>();
                c.For<IHubManager>().Use<DefaultHubManager>();
                c.For<ITraceManager>().Use<TraceManager>();
                */

                //c.For<IHubActivator>().Use<StructureMapHubActivator>();
                
                //c.For<IDependencyResolver>().Use<StructureMapResolver>();
            });

            //GlobalHost.DependencyResolver = ObjectFactory.GetInstance<IDependencyResolver>();

            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new StructureMapHubActivator());

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