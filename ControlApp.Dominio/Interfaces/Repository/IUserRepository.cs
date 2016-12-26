using ControlApp.Dominio.Model;
using System.Collections.Generic;

namespace ControlApp.Dominio.Interfaces.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void Update(User obj);

        void Delete(string login);

        User GetByEmail(string email);

        User GetByLogin(string login);

        IEnumerable<User> Search(string user);
    }
}
