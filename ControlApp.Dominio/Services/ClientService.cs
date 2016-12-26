using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using ControlApp.Resources;
using System.Collections.Generic;

namespace ControlApp.Dominio.Services
{
    public class ClientService : ServiceBase<Client>, IClientService
    {
        private readonly IClientRepository _repositoryClient;

        public ClientService(IClientRepository repositoryClient)
            : base(repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public void Register(string acronym, string name, string socialReazon, bool active)
        {
            var hasClient = _repositoryClient.GetByAcronym(acronym);
            AssertionConcern.AssertArgumentIsNull(hasClient, "Sigla já cadastrada para outro cliente.");

            Client client = new Client(acronym);
            client.ChangeName(name);
            client.ChangeSocialReazon(socialReazon);
            client.ChangeActivation(active);
            _repositoryClient.Add(client);
        }

        public void Update(Client obj)
        {
            _repositoryClient.Update(obj);
        }

        public void Delete(string acronym)
        {
            var hasClient = _repositoryClient.GetByAcronym(acronym);
            AssertionConcern.AssertArgumentNotNull(hasClient, "Cliente inexistente.");
            _repositoryClient.Delete(acronym);
        }


        public Client GetByAcronym(string acronym)
        {
            AssertionConcern.AssertArgumentNotNull(acronym, "Sigla inválida.");
            return _repositoryClient.GetByAcronym(acronym);
        }

        public IEnumerable<Client> Search(string name)
        {
            AssertionConcern.AssertArgumentNotNull(name, "Nome inválido.");
            return _repositoryClient.Search(name);
        }
    }
}
