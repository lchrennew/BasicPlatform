using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace BasicPlatform.Web.Models
{
    public static class AppRoleExtension
    {
        static MongoCollection<AppRole> c = Databases.Mongo.GetCollection<AppRole>("approle");

        static AppRoleExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("a", "u"), IndexOptions.SetUnique(true).SetName("ix_a_u"));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="appRole"></param>
        /// <param name="combine">（角色）合并(true)或替换(false)</param>
        public static void Save(this AppRole appRole, bool combine)
        {
            if (appRole != null)
            {
                if (appRole.Roles != null && appRole.Roles.Any())
                    if (combine)
                        c.Update(Query.And(Query.EQ("u", appRole.User), Query.EQ("a", appRole.App)), Update.AddToSetEachWrapped("r", appRole.Roles as IEnumerable<ObjectId>), UpdateFlags.Upsert);
                    else
                        c.Update(Query.And(Query.EQ("u", appRole.User), Query.EQ("a", appRole.App)), Update.SetWrapped("r", appRole.Roles), UpdateFlags.Upsert);
                else if (!combine)
                    c.Remove(Query.And(Query.EQ("u", appRole.User), Query.EQ("a", appRole.App)));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteAppRole(this ObjectId appRole)
        {
            c.Remove(Query.EQ("_id", appRole));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="appRole"></param>
        public static void Delete(this AppRole appRole)
        {
            if (appRole != null) appRole.Id.DeleteAppRole();
        }

        /// <summary>
        /// 删除用户使所有权限
        /// </summary>
        /// <param name="user"></param>
        public static void DeleteByUser(this ObjectId user)
        {
            c.Remove(Query.EQ("u", user));
        }

        /// <summary>
        /// 获取应用权限
        /// </summary>
        /// <param name="id"></param>
        public static AppRole GetAppRole(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 获取应用权限
        /// </summary>
        /// <param name="app"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AppRole GetAppRole(this ObjectId app, ObjectId user)
        {
            return c.FindOne(Query.And(Query.EQ("a", app), Query.EQ("u", user)));
        }

        /// <summary>
        /// 搜索应用权限
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<AppRole> GetAppRoles(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {
            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("l").SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();
        }

        /// <summary>
        /// 搜索应用权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<AppRole> GetAppRoles(this IMongoQuery query)
        {
            return c.Find(query).ToList();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool AnyAppRoles(this IMongoQuery query)
        {
            return c.FindOne(query) != null;
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this AppRole appRole, params ObjectId[] roles)
        {
            appRole.SetRoles(roles as IEnumerable<ObjectId>);
        }

        /// <summary>
        /// 是否存在某用户角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool Exists(this IMongoQuery query)
        {
            return c.FindOne(query) != null;
        }

        static ObjectId appAdminRole = Role.AppAdmin.Id;

        public static bool IsAppAdmin(this ObjectId user, ObjectId app)
        {
            return app.GetAppRole(user).ContainsRoles(appAdminRole);
        }

        public static bool IsAppAdmin(this AppRole appRole)
        {
            return appRole.ContainsRoles(appAdminRole);
        }

        public static bool ContainsRoles(this AppRole appRole, params ObjectId[] roles)
        {
            return appRole.ContainsRoles(roles as IEnumerable<ObjectId>);
        }

        public static bool ContainsRoles(this AppRole appRole, IEnumerable<ObjectId> roles)
        {
            return appRole != null && appRole.Roles != null && appRole.Roles.Count > 0 && roles != null && roles.Count() > 0 && appRole.Roles.Any(x => roles.Contains(x));
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this AppRole appRole, IEnumerable<ObjectId> roles)
        {
            if (appRole != null)
            {
                if (roles != null && roles.Count() > 0)
                    appRole.Roles = new HashSet<ObjectId>(roles.Distinct());
                else appRole.Roles = null;
            }
        }



        public static void AddRoledUsersToApp(this App app, IEnumerable<ObjectId> users, IEnumerable<ObjectId> roles = null)
        {
            if (app == null) return;
            if (users == null || !users.Any()) return;
            users = app.Id.AddAppToUsers(users.ToArray()).Select(x => x.Id);
            if (roles == null || !roles.Any()) return;
            foreach (var user in users)
            {
                new AppRole { App = app.Id, User = user, Roles = new HashSet<ObjectId>(roles) }.Save(true);
            }
        }


    }
}
