using ControlApp.DependencyResolver;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using SimpleInjector;

namespace ControlAppWebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
