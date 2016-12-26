using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using System.Collections.Generic;

namespace ControlApp.Application
{
    public class ApplicationAppService : AppServiceBase<ControlApp.Dominio.Model.Application>, IApplicationAppService
    {
        private readonly IApplicationService _service;

        public ApplicationAppService(IApplicationService service)
            : base(service)
        {
            _service = service;
        }

        public void Update(ControlApp.Dominio.Model.Application obj)
        {
            _service.Update(obj);
        }

        public void Delete(string hash)
        {
            _service.Delete(hash);
        }

        public void Create(string name, string description, bool active)
        {
            _service.Create(name,description, active);
        }

        public IEnumerable<ControlApp.Dominio.Model.Application> Search(string name)
        {
            return _service.Search(name);
        }

        public ControlApp.Dominio.Model.Application GetByName(string name)
        {
            return _service.GetByName(name);
        }

        public ControlApp.Dominio.Model.Application GetByHash(string hash)
        {
            return _service.GetByHash(hash);
        }
    }
}
