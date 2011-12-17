using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace eGo.ScrumMolder.Web.Service
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Edit the base address of DeveloperInterface by replacing the "DeveloperInterface" string below
            RouteTable.Routes.Add(new ServiceRoute("DeveloperInterface", new WebServiceHostFactory(), typeof(DeveloperInterface)));
        }
    }
}
