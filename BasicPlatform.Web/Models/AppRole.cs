using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicPlatform.Web.Models
{
    /// <summary>
    /// 用来描述某个用户在某个应用内具备哪些角色
    /// </summary>
    [BsonIgnoreExtraElements]
    public class AppRole
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [BsonElement("u")]
        public ObjectId User { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        [BsonElement("a")]
        public ObjectId App { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [BsonElement("r")]
        [BsonIgnoreIfNull]
        public HashSet<ObjectId> Roles { get; set; }
    }
}
