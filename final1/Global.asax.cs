using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;


namespace final1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register); 

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Session_Start()
        //{
        //    List<final1.Models.Location> ShoppingCart = new List<final1.Models.Location>();
        //    Session["ShoppingCart"] = ShoppingCart;
        //}
        protected void Session_Start()
        {
            List<final1.Models.Landmark> ShoppingCart = new List<final1.Models.Landmark>();
            Session["ShoppingCart"] = ShoppingCart;
        }
    }
}
