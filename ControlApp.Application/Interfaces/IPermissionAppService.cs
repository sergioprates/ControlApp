using ControlApp.Dominio.Model;

namespace ControlApp.Application.Interfaces
{
    public interface IPermissionAppService : IAppServiceBase<Permission>
    {
        void Update(Permission obj);

        void Delete(Permission obj);
    }
}
