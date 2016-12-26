using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Application.Interfaces
{
    public interface IApplicationAppService : IAppServiceBase<ControlApp.Dominio.Model.Application>
    {
        void Update(ControlApp.Dominio.Model.Application obj);

        void Delete(string hash);

        IEnumerable<ControlApp.Dominio.Model.Application> Search(string name);

        ControlApp.Dominio.Model.Application GetByName(string name);

        ControlApp.Dominio.Model.Application GetByHash(string hash);

        void Create(string name, string description, bool active);
    }
}
