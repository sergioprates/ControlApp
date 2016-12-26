using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Application.Interfaces
{
    public interface IGroupAppService : IAppServiceBase<Group>
    {
        void Update(Group obj);

        void Delete(string hash);

        void Create(string name, bool active);

        IEnumerable<Group> Search(string name);

        Group GetByName(string name);

        Group GetByHash(string hash);
    }
}
