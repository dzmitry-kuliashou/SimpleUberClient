using System.Web.Mvc;
using System.Web.Routing;

namespace MVCTypeScriptReact3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Comments",
                url: "comments",
                defaults: new { controller = "CommentBox", action = "Comments" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "CommentBox", action = "Login" }
            );

            routes.MapRoute(
                name: "NewComment",
                url: "comments/new",
                defaults: new { controller = "CommentBox", action = "AddComment" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

