using CLogManagement.Web.Api.Areas.HelpPage;
using System.Web.Http;

namespace CLogManagement.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HelpPageAreaRegistration.RegisterAllAreas();
        }
    }
}
