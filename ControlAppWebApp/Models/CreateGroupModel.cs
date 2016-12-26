using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlAppWebApp.Models
{
    public class CreateGroupModel
    {
        public CreateGroupModel()
        {}
        public CreateGroupModel(Group group)
        {
            this.Name = group.Name;
            this.Active = group.Active;
            this.Hash = group.Hash;
        }

        public string Name { get; set; }

        public bool Active { get; set; }

        public string Hash { get; set; }
    }
}