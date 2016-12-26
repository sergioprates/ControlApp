using ControlApp.Dominio.Interfaces.Repository;
using ControlApp.Dominio.Model;
using MongoDB.Data.Contexto;
using MongoDB.Data.Modelo;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Infraestrutura.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly MongoDBConfig config;

        protected readonly ICollection<TEntity> db;

        public RepositoryBase()
        {
            if (config == null)
            {
                config = new MongoDBConfig(27017, "localhost", "ControlApp");
            }
        }       

        public void Add(TEntity obj)
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                ctx.Collection<TEntity>().Insert(obj);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                return ctx.Collection<TEntity>().FindAllAs<TEntity>();
            }
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            using (MongoDBContext ctx = new MongoDBContext(config))
            {
                var query = Query.EQ("Active", true);
                return ctx.Collection<TEntity>().FindAs<TEntity>(query);
            }
        }
    }
}
