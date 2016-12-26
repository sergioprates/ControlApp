using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Services
{
    public interface IGroupService : IServiceBase<Group>
    {
        void Update(Group obj);

        void Delete(string hash);

        void Create(string name, bool active);

        Group GetByName(string name);

        IEnumerable<Group> Search(string name);

        Group GetByHash(string hash);
    }
}
