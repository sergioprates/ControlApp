using ControlApp.Resources;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace ControlApp.Dominio.Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        protected User()
        {}

        public User(string name, string login)
        {
            AssertionConcern.AssertArgumentNotNull(name, "Nome não pode ser nulo.");
            AssertionConcern.AssertArgumentNotNull(login, "Login não pode ser nulo.");
            this.Name = name;
            this.Login = login;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public void SetPassword(string password, string confirmPassword)
        {
            AssertionConcern.AssertArgumentNotNull(password, "Senha inválida");
            AssertionConcern.AssertArgumentNotNull(confirmPassword, "Senha inválida");
            AssertionConcern.AssertArgumentLength(password, 6, 20, "Senha inválida");
            AssertionConcern.AssertArgumentEquals(password, confirmPassword, "Senhas não conferem");

            this.Password = PasswordAssertionConcern.Encrypt(password);
        }

        public string ResetPassword()
        {
            string password = Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = PasswordAssertionConcern.Encrypt(password);
            return password;
        }

        public void SetActivation(bool active)
        {
            this.Active = active;
        }

        public void ChangeEmail(string email)
        {
            EmailAssertionConcern.AssertIsValid(email);
            this.Email = email;
        }

        public void ChangeName(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome inválido.");
            this.Name = name;
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(this.Name, 3, 250, "Nome inválido");
            EmailAssertionConcern.AssertIsValid(this.Email);
            PasswordAssertionConcern.AssertIsValid(this.Password);
        }
    }
}
