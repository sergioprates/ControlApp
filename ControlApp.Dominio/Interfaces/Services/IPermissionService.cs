using ControlApp.Dominio.Model;

namespace ControlApp.Dominio.Interfaces.Services
{
    public interface IPermissionService : IServiceBase<Permission>
    {
        void Update(Permission obj);

        void Delete(Permission obj);
    }
}
