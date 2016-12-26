using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;

namespace ControlApp.Application
{
    public class UserAppService : AppServiceBase<User>, IUserAppService
    {
        private readonly IUserService _service;

        public UserAppService(IUserService service)
            : base(service)
        {
            _service = service;
        }

        public void Update(User obj)
        {
            _service.Update(obj);
        }

        public void Delete(string login)
        {
            _service.Delete(login);
        }

        public User Authenticate(string login, string password)
        {
            return _service.Authenticate(login, password);
        }

        public IEnumerable<User> Search(string user)
        {
            return _service.Search(user);
        }

        public void Register(string name, string login, string email, string password, string confirmPassword, bool active)
        {
            _service.Register(name, login, email, password, confirmPassword, active);
        }

        public User GetByEmail(string email)
        {
            return _service.GetByEmail(email);
        }

        public User GetByLogin(string login)
        {
            return _service.GetByLogin(login);
        }
    }
}
