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
    public static class AppExtension
    {
        static MongoCollection<App> c = Databases.Mongo.GetCollection<App>("app");

        static AppExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("l"), IndexOptions.SetUnique(true).SetBackground(true).SetName("ix_label"));

        }

        /// <summary>
        /// 保存应用信息
        /// </summary>
        /// <param name="app"></param>
        public static void Save(this App app)
        {
            if (app != null) c.Save(app);
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="app"></param>
        public static void DeleteApp(this ObjectId id)
        {
            c.Remove(Query.EQ("_id", id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="app"></param>
        public static void Delete(this App app)
        {
            if (app != null) app.Id.DeleteApp();
        }

        /// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="id"></param>
        public static App GetApp(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static App GetApp(this string label)
        {
            return c.FindOne(Query.EQ("l", label));
        }

        /// <summary>
        /// 搜索应用
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<App> GetApps(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {
            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("l").SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();
        }

        /// <summary>
        /// 搜索应用
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<App> GetApps(this IMongoQuery query)
        {
            if (query == Query.Null) return c.FindAll();
            else return c.Find(query).ToList();
        }

        /// <summary>
        /// 检查App密钥
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static bool CheckApp(this string appKey, string appSecret)
        {
            ObjectId appid;
            if (ObjectId.TryParse(appKey, out appid))
            {
                App app = appid.GetApp();
                if (app != null && app.Secret == appSecret) return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this App app, params ObjectId[] roles)
        {
            app.SetRoles(roles as IEnumerable<ObjectId>);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this App app, IEnumerable<ObjectId> roles)
        {
            if (app != null)
            {
                if (roles != null && roles.Count() > 0)
                    app.Roles = new HashSet<ObjectId>(roles.Distinct());
                else app.Roles = null;
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="app"></param>
        /// <param name="roles"></param>
        public static void AddRoles(this App app, IEnumerable<ObjectId> roles)
        {
            if (app != null && roles != null && roles.Count() > 0)
            {
                if (app.Roles == null) app.Roles = new HashSet<ObjectId>(roles);
                else app.Roles = new HashSet<ObjectId>(app.Roles.Concat(roles));
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="app"></param>
        /// <param name="roles"></param>
        public static void AddRoles(this App app, params ObjectId[] roles)
        {
            app.AddRoles(roles as IEnumerable<ObjectId>);
        }

    }
}
