using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using MLib.Data;

namespace OrangeSummer.Web.MasterApplication
{
    public class global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}"
            );

            RouteTable.Routes.MapHttpRoute(
                name: "ApiAction",
                routeTemplate: "api/{controller}/{action}"
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //foreach (string key in Request.QueryString)
            //    Element.CheckInput(Request.QueryString[key]);
            //foreach (string key in Request.Form)
            //    Element.CheckInput(Request.Form[key]);
            //foreach (string key in Request.Cookies)
            //    Element.CheckInput(Request.Cookies[key].Value);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}