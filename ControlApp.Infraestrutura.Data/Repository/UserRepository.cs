using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Model;
using MongoDB.Bson;
using MongoDB.Data.Contexto;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ControlApp.Infraestrutura.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public User GetByEmail(string email)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Email", email);

                var collection = ctx.Collection<User>();
                return collection.FindOneAs<User>(query);
            }
        }

        public User GetByLogin(string login)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Login", login);

                var collection = ctx.Collection<User>();
                return collection.FindOneAs<User>(query);
            }
        }


        public void Update(User obj)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                UpdateBuilder update = new UpdateBuilder();
                update.Set("Name", obj.Name);
                update.Set("Login", obj.Login);
                update.Set("Email", obj.Email);
                update.Set("Password", obj.Password);
                update.Set("Active", obj.Active);

                ctx.Collection<User>().Update(Query.EQ("Login", obj.Login), update, UpdateFlags.None);
            }
        }

        public void Delete(string login)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                ctx.Collection<User>().Remove(Query.EQ("Login", login));
            }
        }

        public IEnumerable<User> Search(string user)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.Or(Query.Matches("Login", new BsonRegularExpression(user, "i")), Query.Matches("Name", new BsonRegularExpression(user, "i")));
                
                var collection = ctx.Collection<User>();
                return collection.Find(query);
            }
        }
    }
}
