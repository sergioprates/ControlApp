using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Services
{
    public interface IClientService : IServiceBase<Client>
    {
        void Update(Client obj);

        void Delete(string acronym);

        Client GetByAcronym(string acronym);

        IEnumerable<Client> Search(string name);

        void Register(string acronym, string name, string socialReazon, bool active);
    }
}
