using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;

namespace ControlApp.Application
{
    public class ClientAppService : AppServiceBase<Client>, IClientAppService
    {
        private readonly IClientService _service;

        public ClientAppService(IClientService service)
            : base(service)
        {
            _service = service;
        }

        public void Update(Client obj)
        {
            _service.Update(obj);
        }

        public void Delete(string acronym)
        {
            _service.Delete(acronym);
        }

        public Client GetByAcronym(string acronym)
        {
            return _service.GetByAcronym(acronym);
        }

        public IEnumerable<Client> Search(string name)
        {
            return _service.Search(name);
        }

        public void Register(string acronym, string name, string socialReazon, bool active)
        {
            _service.Register(acronym, name, socialReazon, active);
        }
    }
}
