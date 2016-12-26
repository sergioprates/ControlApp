using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Application.Interfaces
{
    public interface IClientAppService : IAppServiceBase<Client>
    {
        void Update(Client obj);

        void Delete(string acronym);

        Client GetByAcronym(string acronym);

        IEnumerable<Client> Search(string name);

        void Register(string acronym, string name, string socialReazon, bool active);
    }
}
