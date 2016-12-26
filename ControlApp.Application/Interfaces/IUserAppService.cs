using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Application.Interfaces
{
    public interface IUserAppService : IAppServiceBase<User>
    {
        void Update(User obj);

        void Delete(string login);

        User Authenticate(string login, string password);

        void Register(string name, string login, string email, string password, string confirmPassword, bool active);

        IEnumerable<User> Search(string user);

        User GetByEmail(string email);

        User GetByLogin(string login);
    }
}
