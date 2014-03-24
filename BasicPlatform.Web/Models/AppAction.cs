using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BasicPlatform.Web.Models
{
    /// <summary>
    /// 应用管理动作
    /// </summary>
    [BsonIgnoreExtraElements]
    public class AppAction
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        [BsonElement("a")]
        public ObjectId App { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [BsonElement("ac")]
        public ObjectId Action { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("r")]
        public HashSet<ObjectId> Roles { get; set; }
    }
}
