
using ControlApp.Resources;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text;
using System.Text.RegularExpressions;
namespace ControlApp.Dominio.Model
{
    [BsonIgnoreExtraElements]
    public class Permission
    {
        private string _feature;

        public Permission(string name)
        {
            this.Name = name;
            this.Hash = Guid.NewGuid().ToString().Substring(0, 8);
        }
        public string Hash { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public bool Read { get; set; }

        public bool Write { get; set; }

        /// <summary>
        /// Return the key of permission
        /// </summary>
        /// <returns></returns>
        public string Feature
        {
            get
            {
                return GetFeature();
            }
            set
            {
                _feature = value;
            }
        }

        /// <summary>
        /// Return the key of permission
        /// </summary>
        /// <returns></returns>
        private string GetFeature()
        {
            AssertionConcern.AssertArgumentNotEmpty(this.Name, "Nome inválido.");

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(this.Name);
            var str = System.Text.Encoding.UTF8.GetString(bytes);

            string regExp = @"[^\w\d]";
            return Regex.Replace(str, regExp, "").ToLower();
        }
    }
}
