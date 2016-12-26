using System.Collections;
using System.Linq;
using System.Collections.Generic;
using ControlApp.Resources;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ControlApp.Dominio.Model
{
    [BsonIgnoreExtraElements]
    public class Group
    {
        protected Group()
        { }

        public Group(string name)
        {
            this.Name = name;
            this.Users = new List<User>();
            this.Permissions = new List<Permission>();
            this.Hash = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public string Hash { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public IList<Permission> Permissions { get; set; }

        public IList<User> Users { get; set; }

        public void ChangeActivation(bool active)
        {
            this.Active = active;
        }

        public void ChangeName(string name)
        {
            AssertionConcern.AssertArgumentNotNull(name, "Nome de grupo inválido.");
            this.Name = name;
        }

        public void AddUser(User user)
        {
            AssertionConcern.AssertArgumentNotNull(user, "Usuário inválido.");
            this.Users.Add(user);
        }

        public void AddPermission(Permission permission)
        {
            AssertionConcern.AssertArgumentNotNull(permission, "Permissão inválida.");
            this.Permissions.Add(permission);
        }

        public void RemovePermission(string name)
        {
            var permission = this.Permissions.FirstOrDefault(x => x.Name == name);
            AssertionConcern.AssertArgumentNotNull(permission, "Permissão não encontrada.");

            this.Permissions.Remove(permission);
        }

        public void RemoveUser(string login)
        {
            var user = this.Users.FirstOrDefault(x => x.Login == login);
            AssertionConcern.AssertArgumentNotNull(user, "Usuário não encontrado.");

            this.Users.Remove(user);
        }
    }
}
