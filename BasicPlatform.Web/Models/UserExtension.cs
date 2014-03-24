using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Web.Security;

namespace BasicPlatform.Web.Models
{
    public static class UserExtension
    {
        static MongoCollection<User> c = Databases.Mongo.GetCollection<User>("user");
        static UserExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("n"), IndexOptions.SetUnique(true).SetBackground(true).SetName("ix_username"));
            c.EnsureIndex(IndexKeys.Ascending("e"), IndexOptions.SetUnique(true).SetBackground(true).SetName("ix_email"));
            c.EnsureIndex(IndexKeys.Ascending("n", "r"));
            if (c.FindOne(Query.EQ("n", "admin")).IsAnonymous())
            {
                var u = new User { Email = "admin@admin.com", Username = "admin", Password = "admin", Label = "administrator" };
                u.SetRoles("SysAdmin");
                u.Save(true);
            }
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public static User Save(this User user, bool encrypt = false)
        {
            if (user != null)
            {
                if (user.Username == "admin")
                    if (user.Roles == null) user.SetRoles("SysAdmin");
                    else user.Roles.Add("SysAdmin".GetRole().Id);
                if (encrypt) user.Password = user.Password.Encry();
                if (user.Id == default(ObjectId)) user.Id = ObjectId.GenerateNewId();
                c.Save(user);
                return user;
            }
            else return null;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUser(this string username)
        {
            return c.FindOne(Query.EQ("n", username))
                ?? c.FindOne(Query.EQ("e", username));
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUser(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 是否匿名用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsAnonymous(this User user)
        {
            return user == null || user.Id == default(ObjectId);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User ValidateUser(this string username, string password)
        {
            string encpwd = password.Encry();
            return c.FindOne(Query.And(Query.EQ("n", username), Query.EQ("p", encpwd)))
                ?? c.FindOne(Query.And(Query.EQ("e", username), Query.EQ("p", encpwd)));
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        static string Encry(this string clearText, string format = "md5")
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(clearText, format);
        }

        /// <summary>
        /// 检查密码是否匹配
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckPassword(this User user, string password)
        {
            if (user.IsAnonymous()) return false;
            else return password.Encry() == user.Password;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this User user, params ObjectId[] roles)
        {
            user.SetRoles(roles as IEnumerable<ObjectId>);
        }

        /// <summary>
        /// 设置用户角色（使用Label）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this User user, params string[] roles)
        {
            user.SetRoles(Query.In("l", new BsonArray(roles)).GetRoles().Select(x => x.Id).ToArray());
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this User user, IEnumerable<ObjectId> roles, bool append = false)
        {
            if (user != null)
            {
                if (roles != null && roles.Count() > 0)
                    user.Roles = new HashSet<ObjectId>(append ? user.Roles.Concat(roles).Distinct() : roles.Distinct());
                else user.Roles = null;
            }
        }

        /// <summary>
        /// 设置用户应用
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apps"></param>
        public static void SetApps(this User user, params ObjectId[] apps)
        {
            user.SetApps(apps as IEnumerable<ObjectId>);
        }

        /// <summary>
        /// 设置用户应用
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apps"></param>
        public static void SetApps(this User user, IEnumerable<ObjectId> apps)
        {
            if (user != null)
            {
                if (apps != null && apps.Count() > 0)
                    user.Apps = new HashSet<ObjectId>(apps.Distinct());
                else user.Apps = null;
            }
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {
            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("l").SetLimit((int)pageSize).SetSkip((int)((pageIndex - 1) * pageSize)).ToList();
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(this IMongoQuery query)
        {
            return c.Find(query).ToList();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteUser(this ObjectId id)
        {
            c.Remove(Query.And(Query.EQ("_id", id), Query.NE("n", "admin")), RemoveFlags.Single);
        }

        /// <summary>
        /// 添加app到一组用户
        /// </summary>
        /// <param name="app"></param>
        /// <param name="users"></param>
        public static IEnumerable<User> AddAppToUsers(this ObjectId app, params ObjectId[] users)
        {
            if (users != null && users.Length > 0)
            {
                var q = Query.In("_id", new BsonArray(users));
                c.Update(Query.And(q, Query.Exists("a")), Update.AddToSetWrapped("a", app), UpdateFlags.Multi);
                c.Update(Query.And(q, Query.NotExists("a")), Update.SetWrapped("a", new[] { app }), UpdateFlags.Multi);
                return q.GetUsers();
            }
            else return new User[0];
        }

        public static bool ContainsRoles(this User user, params ObjectId[] roles)
        {
            return user != null && user.Roles != null && user.Roles.Count > 0 && roles != null && roles.Length > 0 && user.Roles.Any(x => roles.Contains(x));
        }

        static ObjectId sysAdminRole = Role.SysAdmin.Id;

        /// <summary>
        /// 是否系统管理员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsSysAdmin(this User user)
        {
            return user.ContainsRoles(sysAdminRole);
        }

        /// <summary>
        /// 是否是app管理员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static bool IsAppAdmin(this User user, App app)
        {
            if (app != null)
                return user.Id.IsAppAdmin(app.Id);
            else return false;
        }

    }
}
