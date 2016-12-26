using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using ControlApp.Resources;
using System;
using System.Collections.Generic;

namespace ControlApp.Dominio.Services
{
    public class ApplicationService : ServiceBase<Application>, IApplicationService
    {
         private readonly IApplicationRepository _repositoryApplication;

         public ApplicationService(IApplicationRepository repositoryApplication)
             : base(repositoryApplication)
        {
            _repositoryApplication = repositoryApplication;
        }

        public void Update(Application obj)
        {
            _repositoryApplication.Update(obj);
        }

        public void Delete(string hash)
        {
            AssertionConcern.AssertArgumentNotEmpty(hash, "Aplicação inválida.");
            var group = GetByHash(hash);
            AssertionConcern.AssertArgumentNotNull(group, "Aplicação inexistente.");
            _repositoryApplication.Delete(hash);
        }

        public Application GetByHash(string hash)
        {
            AssertionConcern.AssertArgumentNotEmpty(hash, "Hash inválido.");
            return _repositoryApplication.GetByHash(hash);
        }

        public Application GetByName(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome inválido.");
            return _repositoryApplication.GetByName(name);
        }

        public IEnumerable<Application> Search(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome inválido.");
            return _repositoryApplication.Search(name);
        }

        public void Create(string name, string description, bool active)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome de aplicação inválido.");

            var hasGroup = GetByName(name);

            AssertionConcern.AssertArgumentIsNull(hasGroup, "aplicação já cadastrada.");

            Application app = new Application(name);
            app.ChangeActivation(active);
            app.ChangeDescription(description);
            _repositoryApplication.Add(app);
        }
    }
}
