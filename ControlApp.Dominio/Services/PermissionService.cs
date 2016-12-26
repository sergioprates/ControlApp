using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using System;

namespace ControlApp.Dominio.Services
{
    public class PermissionService : ServiceBase<Permission>, IPermissionService
    {
         private readonly IPermissionRepository _repositoryPermission;

         public PermissionService(IPermissionRepository repositoryPermission)
             : base(repositoryPermission)
        {
            _repositoryPermission = repositoryPermission;
        }

        public void Update(Permission obj)
        {
            _repositoryPermission.Add(obj);
        }

        public void Delete(Permission obj)
        {
            _repositoryPermission.Delete(obj);
        }
    }
}
