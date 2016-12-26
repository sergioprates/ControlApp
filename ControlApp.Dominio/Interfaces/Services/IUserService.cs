using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        void Update(User obj);

        void Delete(string login);

        User Authenticate(string login, string password);

        User GetByEmail(string email);

        User GetByLogin(string login);

        void Register(string name, string login, string email, string password, string confirmPassword, bool active);

        IEnumerable<User> Search(string user);
    }
}
