using Castle.Windsor;
using MVCTypeScriptReact3.Installers;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCTypeScriptReact3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();

            var configAssembly = Assembly.Load("MVCTypeScriptReact3.Config");
            var installWindsorRegistratorsClass = configAssembly.GetType("MVCTypeScriptReact3.Config.Installers.InstallWindsorRegistrators");
            installWindsorRegistratorsClass.GetMethod("Install", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { container });

            container.Install(new ApplicationCastleInstaller());

            var castleControllerFactory = new CastleControllerFactory(container);

            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
