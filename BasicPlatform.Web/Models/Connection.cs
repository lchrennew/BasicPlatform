using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BasicPlatform.Web.Models
{
    /// <summary>
    /// 账号接入
    /// </summary>
    public class Connection
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
        /// 别名（应用中的名称）
        /// </summary>
        [BsonElement("n")]
        public string Alias { get; set; }

    }
}
