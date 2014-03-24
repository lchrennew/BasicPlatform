using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicPlatform.Web.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [BsonElement("n")]
        public string Username { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        [BsonDefaultValue("")]
        [BsonElement("e")]
        public string Email { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        [BsonElement("l")]
        [BsonDefaultValue("")]
        public string Label { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [BsonElement("p")]
        public string Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("r")]
        public HashSet<ObjectId> Roles { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("a")]
        public HashSet<ObjectId> Apps { get; set; }
    }
}
