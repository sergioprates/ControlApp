using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Dominio.Interfaces.Services
{
    public interface IApplicationService : IServiceBase<Application>
    {
        void Update(Application obj);

        void Delete(string hash);

        Application GetByHash(string hash);

        Application GetByName(string name);

        IEnumerable<Application> Search(string name);

        void Create(string name, string description, bool active);
    }
}
