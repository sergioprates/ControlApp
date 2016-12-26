
using ControlApp.Application;
using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Services;
using ControlApp.Infraestrutura.Data.Repository;
using SimpleInjector;
using System;
namespace ControlApp.DependencyResolver
{
    public static class DependencyInjectionResolver
    {
        public static Container GetContainer()
        {
            var container = new Container();

            // Register your types, for instance:

            //APPLICATION
            container.Register<IClientAppService, ClientAppService>();
            container.Register<IGroupAppService, GroupAppService>();
            container.Register<IPermissionAppService, PermissionAppService>();
            container.Register<IUserAppService, UserAppService>();
            container.Register<IApplicationAppService, ApplicationAppService>();


            //SERVICES
            container.Register<IUserService, UserService>();
            container.Register<IPermissionService, PermissionService>();
            container.Register<IGroupService, GroupService>();
            container.Register<IClientService, ClientService>();
            container.Register<IApplicationService, ApplicationService>();

            //REPOSITORYS
            container.Register<IUserRepository, UserRepository>();
            container.Register<IPermissionRepository, PermissionRepository>();
            container.Register<IGroupRepository, GroupRepository>();
            container.Register<IClientRepository, ClientRepository>();
            container.Register<IApplicationRepository, ApplicationRepository>();

            return container;
        }
    }
}
