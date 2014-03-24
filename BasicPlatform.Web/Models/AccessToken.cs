using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BasicPlatform.Web.Models
{
    public class AccessToken
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("u")]
        public ObjectId User { get; set; }

        [BsonElement("a")]
        public ObjectId App { get; set; }

        [BsonElement("s")]
        public string Token { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement("e")]
        public DateTime Expiration { get; set; }
    }
}
