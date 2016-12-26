using ControlApp.Resources;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ControlApp.Dominio.Model
{
     [BsonIgnoreExtraElements]
    public class Client
    {
        protected Client()
        {
            this.Applications = new List<Application>();
        }

        public Client(string acronym)
        {
            ChangeAcronym(acronym);
            this.Applications = new List<Application>();
        }

        public string Name { get; set; }

        public string SocialReazon { get; set; }

        public bool Active { get; set; }

        public string Acronym { get; set; }

        public IList<Application> Applications { get; set; }

        public void AddApplication(Application application)
        {
            AssertionConcern.AssertArgumentNotNull(application, "Aplicação inválida.");
            this.Applications.Add(application);
        }

        public void RemoveApplication(Application application)
        {
            AssertionConcern.AssertArgumentNotNull(application, "Aplicação inválida.");
            var p = this.Applications.FirstOrDefault(x => x.Hash == application.Hash);
            AssertionConcern.AssertArgumentNotNull(p, "Aplicação não encontrada.");
            this.Applications.Remove(application);
        }

        public void ChangeName(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "Nome inválido");
            this.Name = name;
        }

        public void ChangeSocialReazon(string socialReazon)
        {
            AssertionConcern.AssertArgumentNotEmpty(socialReazon, "Razão social inválida");
            this.SocialReazon = socialReazon;
        }

        public void ChangeAcronym(string acronym)
        {
            AssertionConcern.AssertArgumentNotEmpty(acronym, "Sigla inválida.");
            AssertionConcern.AssertArgumentRange(acronym.Length, 3, 3, "Sigla inválida.");
            this.Acronym = acronym.ToUpper();
        }

        public void ChangeActivation(bool active)
        {
            this.Active = active;
        }
    }
}
