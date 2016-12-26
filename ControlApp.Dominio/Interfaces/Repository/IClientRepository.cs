using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Repository
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        void Update(Client obj);

        void Delete(string acronym);

        Client GetByAcronym(string acronym);

        IEnumerable<Client> Search(string name);
    }
}
