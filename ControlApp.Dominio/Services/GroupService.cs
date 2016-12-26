using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using ControlApp.Resources;
using System;
using System.Collections.Generic;

namespace ControlApp.Dominio.Services
{
    public class GroupService : ServiceBase<Group>, IGroupService
    {
        private readonly IGroupRepository _repositoryGroup;

        public GroupService(IGroupRepository repositoryGroup)
            : base(repositoryGroup)
        {
            _repositoryGroup = repositoryGroup;
        }

        public void Update(Group obj)
        {
            _repositoryGroup.Update(obj);
        }

        public void Delete(string hash)
        {
            AssertionConcern.AssertArgumentNotEmpty(hash, "Grupo inválido.");
            var group = GetByHash(hash);
            AssertionConcern.AssertArgumentNotNull(group, "Grupo inexistente.");
            _repositoryGroup.Delete(hash);
        }

        public Group GetByName(string name)
        {
            return _repositoryGroup.GetByName(name);
        }

        public Group GetByHash(string hash)
        {
            AssertionConcern.AssertArgumentNotEmpty(hash, "Hash inválido.");
            return _repositoryGroup.GetByHash(hash);
        }

        public IEnumerable<Group> Search(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome inválido.");
            return _repositoryGroup.Search(name);
        }

        public void Create(string name, bool active)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome de grupo inválido.");

            var hasGroup = GetByName(name);

            AssertionConcern.AssertArgumentIsNull(hasGroup, "Grupo já cadastrado.");

            Group group = new Group(name);
            group.ChangeActivation(active);
            _repositoryGroup.Add(group);
        }

    }
}
