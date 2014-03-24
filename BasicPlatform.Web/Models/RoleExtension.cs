using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Web.Models
{
    public static class RoleExtension
    {
        static MongoCollection<Role> c = Databases.Mongo.GetCollection<Role>("role");
        static RoleExtension()
        {
            c.DropAllIndexes();
            c.EnsureIndex(IndexKeys.Ascending("l", "a"), IndexOptions.SetUnique(true).SetSparse(false).SetBackground(true).SetName("ix_label_app"));
            if (c.FindOne(Query.EQ("l", "SysAdmin")) == null)
                new Role { Label = "SysAdmin", Name = "System Administrator" }.Save();
            if (c.FindOne(Query.EQ("l", "AppAdmin")) == null)
                new Role { Label = "AppAdmin", Name = "App Administrator" }.Save();
        }
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role"></param>
        public static void Save(this Role role)
        {
            if (role != null)
                if (role.Label == "SysAdmin" && role.App != default(ObjectId)) throw new ArgumentException("Roles of app cannot be SysAdmin");
                else if (role.Label == "AppAdmin" && role.App != default(ObjectId)) throw new ArgumentException("Roles of app cannot be AppAdmin");
                else c.Save(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        public static void DeleteRole(this ObjectId role)
        {
            c.Remove(Query.And(Query.EQ("_id", role), Query.NotIn("l", new BsonArray(new string[] { "SysAdmin", "AppAdmin" }))), RemoveFlags.Single);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role"></param>
        public static void Delete(this Role role)
        {
            if (role != null) role.Id.DeleteRole();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Role GetRole(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Role GetRole(this string label)
        {
            return c.FindOne(Query.And(Query.EQ("l", label), Query.NotExists("a")));
        }

        /// <summary>
        /// 获取某个应用的角色
        /// </summary>
        /// <param name="label"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static Role GetRole(this string label, ObjectId app)
        {
            if (label == "SysAdmin") return Role.SysAdmin;
            else if (label == "AppAdmin") return Role.AppAdmin;
            else return c.FindOne(Query.And(Query.EQ("l", label), Query.EQ("a", app)));
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<Role> GetRoles(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {

            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("l").SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();

        }

        /// <summary>
        /// 搜索角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<Role> GetRoles(this IMongoQuery query)
        {
            return c.Find(query).ToList();
        }

        /// <summary>
        /// 是否存在符合条件的数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool AnyRole(this IMongoQuery query)
        {
            return c.FindOne(query) != null;
        }
    }
}
