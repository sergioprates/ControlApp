using ControlApp.Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlAppWebApp.Models
{
    public class RegisterClientModel
    {
        public RegisterClientModel()
        {}

        public RegisterClientModel(Client client)
        {
            this.Name = client.Name;
            this.SocialReazon = client.SocialReazon;
            this.Active = client.Active;
            this.Acronym = client.Acronym;
            this.Applications = client.Applications;
        }

        public string Name { get; set; }

        public string SocialReazon { get; set; }

        public bool Active { get; set; }

        public string Acronym { get; set; }

        public IEnumerable<Application> Applications { get; set; }
    }
}