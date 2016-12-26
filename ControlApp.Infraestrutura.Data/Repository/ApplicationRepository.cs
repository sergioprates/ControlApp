using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Model;
using MongoDB.Bson;
using MongoDB.Data.Contexto;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace ControlApp.Infraestrutura.Data.Repository
{
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        public void Update(Application obj)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                UpdateBuilder<Application> update = new UpdateBuilder<Application>();
                update.Set(x=> x.Name, obj.Name);
                update.Set(x=> x.Active, obj.Active);
                update.Set(x=> x.Hash, obj.Hash);
                update.Set(x=> x.Description, obj.Description);
                update.AddToSetEach(x=> x.Permissions, obj.Permissions);
                update.AddToSetEach(x => x.Access, obj.Access);

                ctx.Collection<Application>().Update(Query.EQ("Hash", obj.Hash), update, UpdateFlags.None);
            }
        }

        public void Delete(string hash)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                ctx.Collection<Application>().Remove(Query.EQ("Hash", hash));
            }
        }

        public Application GetByName(string name)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Name", name);

                var collection = ctx.Collection<Application>();
                return collection.FindOneAs<Application>(query);
            }
        }

        public Application GetByHash(string hash)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Hash", hash);

                var collection = ctx.Collection<Application>();
                return collection.FindOneAs<Application>(query);
            }
        }

        public IEnumerable<Application> Search(string name)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.Matches("Name", new BsonRegularExpression(name, "i"));
                var collection = ctx.Collection<Application>();
                return collection.Find(query);
            }
        }
    }
}
