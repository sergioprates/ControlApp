using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Repository
{
    public interface IGroupRepository : IRepositoryBase<Group>
    {
        void Update(Group obj);

        void Delete(string hash);

        Group GetByName(string name);

        Group GetByHash(string hash);

        IEnumerable<Group> Search(string name);
    }
}
