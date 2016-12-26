using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using System;

namespace ControlApp.Application
{
    public class PermissionAppService : AppServiceBase<Permission>, IPermissionAppService
    {
        private readonly IPermissionService _service;

        public PermissionAppService(IPermissionService service)
             :base(service)
        {
            _service = service;
        }

        public void Update(Permission obj)
        {
            _service.Update(obj);
        }

        public void Delete(Permission obj)
        {
            _service.Delete(obj);
        }
    }
}
