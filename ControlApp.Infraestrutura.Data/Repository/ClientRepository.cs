using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Model;
using MongoDB.Bson;
using MongoDB.Data.Contexto;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;

namespace ControlApp.Infraestrutura.Data.Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public void Update(Client obj)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                UpdateBuilder update = new UpdateBuilder();
                update.Set("Name", obj.Name);
                update.Set("Acronym", obj.Acronym);
                update.Set("Active", obj.Active);
                update.Set("SocialReazon", obj.SocialReazon);

                ctx.Collection<Client>().Update(Query.EQ("Acronym", obj.Acronym), update, UpdateFlags.None);
            }
        }

        public void Delete(string acronym)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                ctx.Collection<Client>().Remove(Query.EQ("Acronym", acronym));
            }
        }

        public Client GetByAcronym(string acronym)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Acronym", acronym);

                var collection = ctx.Collection<Client>();
                return collection.FindOneAs<Client>(query);
            }
        }

        public IEnumerable<Client> Search(string name)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {

                var query = Query.Or(
                    Query.Matches("Name", new BsonRegularExpression(name, "i")), 
                    Query.Matches("SocialReazon", new BsonRegularExpression(name, "i")), 
                    Query.Matches("Acronym", new BsonRegularExpression(name, "i"))
                    );

                var collection = ctx.Collection<Client>();
                return collection.Find(query);
            }
        }
    }
}
