using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicPlatform.Web.Models
{
    public class App
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [BsonElement("n")]
        public string Name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [BsonElement("l")]
        public string Label { get; set; }

        /// <summary>
        /// 入口地址
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("u")]
        public string Url { get; set; }

        /// <summary>
        /// 账号接入url
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("c")]
        public string ConnectUrl { get; set; }

        /// <summary>
        /// 是否可以自行连接：约定
        /// 如果需要他人预先设定此在app系统中的用户数据，则不要进行自行连接
        /// 如果app系统中没有自行连接界面，则不要进行自行连接
        /// 如果app系统中上述两点均不成立，则可以开启
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonElement("m")]
        public bool SelfConnectable { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("s")]
        public string Secret { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("r")]
        public HashSet<ObjectId> Roles { get; set; }

        /// <summary>
        /// 是否可公开访问
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonElement("a")]
        public bool Accessable { get; set; }
    }
}
