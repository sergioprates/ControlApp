using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ControlApp.Dominio.Model
{
     [BsonIgnoreExtraElements]
    public class Access
    {
        public User User { get; set; }

        public DateTime Date { get; set; }
    }
}
