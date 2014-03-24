using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Web.Models
{
    public class Action
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
        /// 应用
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonElement("a")]
        public ObjectId App { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("r")]
        public HashSet<ObjectId> Roles { get; set; }

        /// <summary>
        /// 是否分不同的app设置
        /// </summary>
        [BsonIgnoreIfDefault]
        [BsonElement("i")]
        public bool Individual { get; set; }

        static IEnumerable<Action> individualActions;
        static IEnumerable<Action> sysActions;

        /// <summary>
        /// 分别设置操作的列表
        /// </summary>
        public static IEnumerable<Action> IndividualActions
        {
            get
            {
                return individualActions ?? (individualActions = Query.EQ("i", true).GetActions());
            }
        }

        /// <summary>
        /// 系统操作列表
        /// </summary>
        public static IEnumerable<Action> SysActions
        {
            get
            {
                return sysActions ?? (sysActions = Query.And(Query.NotExists("a"), Query.NotExists("i")).GetActions());
            }
        }

        static IDictionary<ObjectId, string> mapLabel = Query.NotExists("a").GetActions().ToDictionary(x => x.Id, x => x.Label);
        static IDictionary<string, ObjectId> mapId = Query.NotExists("a").GetActions().ToDictionary(x => x.Label, x => x.Id);

        /// <summary>
        /// 根据ID获取标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetLabel(ObjectId id) { return mapLabel.ContainsKey(id) ? mapLabel[id] : null; }

        /// <summary>
        /// 根据标签获取ID
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static ObjectId GetId(string label) { return mapId.ContainsKey(label) ? mapId[label] : default(ObjectId); }
    }
}
