using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicPlatform.Web.Models
{
    public class Role
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
        /// 应用（全局无）
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonElement("a")]
        public ObjectId App { get; set; }

        static Role sysAdmin;
        static Role appAdmin;

        /// <summary>
        /// 获取内置系统管理员角色对象
        /// </summary>
        public static Role SysAdmin
        {
            get
            {
                return sysAdmin ?? (sysAdmin = "SysAdmin".GetRole());
            }
        }

        /// <summary>
        /// 获取应用角色对象
        /// </summary>
        public static Role AppAdmin
        {
            get
            {
                return appAdmin ?? (appAdmin = "AppAdmin".GetRole());
            }
        }
    }
}
