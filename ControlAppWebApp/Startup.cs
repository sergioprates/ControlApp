using ControlApp.Application;
using ControlApp.Application.Interfaces;
using ControlApp.DependencyResolver;
using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Services;
using ControlAppWebApp.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System.Web.Http.Dispatcher;
using System.Reflection;

namespace ControlAppWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var container = DependencyInjectionResolver.GetContainer();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);


            using (container.BeginExecutionContextScope())
            {
                ConfigureOAuth(app, container.GetInstance<IUserAppService>());
            }

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, IUserAppService service)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(service)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}