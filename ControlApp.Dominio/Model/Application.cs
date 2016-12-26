using ControlApp.Resources;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlApp.Dominio.Model
{
   [BsonIgnoreExtraElements]
    public class Application
    {
        protected Application()
        {}

        public Application(string name)
        {
            this.Name = name;
            this.Groups = new List<Group>();
            this.Permissions = new List<Permission>();
            this.Access = new List<Access>();
            this.Hash = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public string Hash { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public IList<Group> Groups { get; set; }

        public IList<Permission> Permissions { get; set; }

        public IList<Access> Access { get; set; }

        public void ChangeName(string name)
        {
            AssertionConcern.AssertArgumentNotNull(name, "Nome de aplicação inválido.");
            this.Name = name;
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentNotNull(description, "Descrição inválida.");
            this.Description = description;
        }

        public void ChangeActivation(bool active)
        {
            this.Active = active;
        }

        public void AddGroup(Group item)
        {
            AssertionConcern.AssertArgumentNotNull(item, "Grupo inválido.");
            this.Groups.Add(item);
        }

        public void RemoveGroup(string hash)
        {
            var item = this.Groups.FirstOrDefault(x => x.Hash == hash);
            AssertionConcern.AssertArgumentNotNull(item, "Grupo não encontrado.");

            this.Groups.Remove(item);
        }

        public void AddPermission(Permission item)
        {
            AssertionConcern.AssertArgumentNotNull(item, "Permissão inválida.");

            if (ExistsPermission(item) == false)
            {
                this.Permissions.Add(item);
            }            
        }

        private bool ExistsPermission(Permission item)
        {
            AssertionConcern.AssertArgumentNotNull(item, "Permissão inválida.");

            return this.Permissions.FirstOrDefault(x => x.Feature == item.Feature) != null;
        }

        public void RemovePermission(string hash)
        {
            var item = this.Permissions.FirstOrDefault(x => x.Hash == hash);
            AssertionConcern.AssertArgumentNotNull(item, "Permissão não encontrada.");

            this.Permissions.Remove(item);
        }
    }
}
