using ControlApp.Application.Interfaces;
using ControlApp.DependencyResolver;
using ControlApp.Dominio.Model;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControlAppWebApp.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserAppService _service;

        public SimpleAuthorizationServerProvider(IUserAppService service)
        {
            _service = service;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //Método que consulta os dados e realiza a autenticação
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var usuario = _service.Authenticate(context.UserName, context.Password);
            }
            catch (Exception e)
            {
                context.SetError("invalid_grant", e.Message);
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            context.Validated(identity);
        }
    }
}