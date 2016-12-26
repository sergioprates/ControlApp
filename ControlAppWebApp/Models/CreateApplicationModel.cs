using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlAppWebApp.Models
{
    public class CreateApplicationModel
    {
        public CreateApplicationModel()
        {}
        public CreateApplicationModel(Application application)
        {
            this.Name = application.Name;
            this.Active = application.Active;
            this.Hash = application.Hash;
            this.Description = application.Description;
            this.Groups = application.Groups;
            this.Permissions = application.Permissions;
        }

        public string Hash { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public IList<Group> Groups { get; set; }

        public IList<Permission> Permissions { get; set; }
    }
}