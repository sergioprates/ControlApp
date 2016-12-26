using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Interfaces.Services;
using ControlApp.Dominio.Model;
using ControlApp.Resources;
using System;
using System.Collections.Generic;

namespace ControlApp.Dominio.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repositoryUser;

        public UserService(IUserRepository repositoryUser)
            : base(repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public void Update(User obj)
        {
            _repositoryUser.Update(obj);
        }

        public User GetByEmail(string email)
        {
            EmailAssertionConcern.AssertIsValid(email);
            return _repositoryUser.GetByEmail(email);
        }

        public User GetByLogin(string login)
        {
            AssertionConcern.AssertArgumentNotEmpty(login, "Login inválido.");
            return _repositoryUser.GetByLogin(login);
        }

        public void Register(string name, string login, string email, string password, string confirmPassword, bool active)
        {
            var hasUser = _repositoryUser.GetByEmail(email);

            AssertionConcern.AssertArgumentNotNull(hasUser, "Email já cadastrado.");

            hasUser = _repositoryUser.GetByLogin(login);

            AssertionConcern.AssertArgumentNotNull(hasUser, "Login já cadastrado!");

            User user = new User(name, login);
            user.ChangeEmail(email);
            user.SetPassword(password, confirmPassword);
            user.SetActivation(active);
            user.Validate();

            _repositoryUser.Add(user);
        }

        public void Delete(string login)
        {
            var user = GetByLogin(login);
            AssertionConcern.AssertArgumentNotNull(user, "Usuário inexistente.");
            _repositoryUser.Delete(login);
        }

        public User Authenticate(string login, string password)
        {
            User user = GetByLogin(login);

            AssertionConcern.AssertArgumentNotNull(user, "Login inexistente.");

            if (user.Login == login && user.Password == PasswordAssertionConcern.Encrypt(password))
            {
                return user;
            }
            else
            {
                throw new Exception("Login ou senha estão incorretos.");
            }

        }

        public IEnumerable<User> Search(string user)
        {
            return _repositoryUser.Search(user);
        }
    }
}
