using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Model
{
    public class Sequencia
    {
        [BsonId]
        public ObjectId _Id { get; set; }
        public string Name { get; set; }
        public long Value { get; set; }
    }
}
