
using ControlApp.Dominio.Model;
using System;
namespace ControlAppWebApp.Models
{
    public class RegisterUserModel
    {
        public RegisterUserModel()
        {}

        public RegisterUserModel(User user)
        {
            this.Name = user.Name;
            this.Password = user.Password;
            this.Email = user.Email;
            this.Login = user.Login;
            this.Active = user.Active;
            this.Password = GetHash();
            this.ConfirmPassword = this.Password;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public bool Active { get; set; }

        public string GetHash()
        {
            return "3b9774e4593aae6ada3c";
        }
    }
}