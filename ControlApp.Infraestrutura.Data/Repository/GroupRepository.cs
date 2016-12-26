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
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public void Update(Group obj)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                UpdateBuilder update = new UpdateBuilder();
                update.Set("Name", obj.Name);
                update.Set("Active", obj.Active);
                update.Set("Hash", obj.Hash);

                ctx.Collection<Group>().Update(Query.EQ("Hash", obj.Hash), update, UpdateFlags.None);
            }
        }

        public void Delete(string hash)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                ctx.Collection<Group>().Remove(Query.EQ("Hash", hash));
            }
        }

        public Group GetByName(string name)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Name", name);

                var collection = ctx.Collection<Group>();
                return collection.FindOneAs<Group>(query);
            }
        }

        public Group GetByHash(string hash)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Hash", hash);

                var collection = ctx.Collection<Group>();
                return collection.FindOneAs<Group>(query);
            }
        }

        public IEnumerable<Group> Search(string name)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.Matches("Name", new BsonRegularExpression(name, "i"));
                var collection = ctx.Collection<Group>();
                return collection.Find(query);
            }
        }
    }
}
