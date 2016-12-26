using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;

namespace ControlApp.Application
{
    public class GroupAppService : AppServiceBase<Group>, IGroupAppService
    {
        private readonly IGroupService _service;

        public GroupAppService(IGroupService service)
            : base(service)
        {
            _service = service;
        }

        public void Update(Group obj)
        {
            _service.Update(obj);
        }

        public void Delete(string hash)
        {
            _service.Delete(hash);
        }

        public void Create(string name, bool active)
        {
            _service.Create(name, active);
        }

        public IEnumerable<Group> Search(string name)
        {
            return _service.Search(name);
        }

        public Group GetByName(string name)
        {
            return _service.GetByName(name);
        }

        public Group GetByHash(string hash)
        {
            return _service.GetByHash(hash);
        }
    }
}
