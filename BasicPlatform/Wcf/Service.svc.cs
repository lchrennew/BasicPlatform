using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Configuration;

namespace BasicPlatform.Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service”。
    public class Service : IService
    {

        void CheckApp(string appKey, string appSecret)
        {
            if (!appKey.CheckApp(appSecret))
                throw new ConfigurationErrorsException("client app setting not correct");
        }

        public User GetUserByAlias(string alias, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var connection = alias.GetConnection(new ObjectId(appKey));
            if (connection == null) return alias.GetUser();
            else
                return connection.User.GetUser();
        }

        public User GetUser(string username, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            return username.GetUser();
        }

        public bool ValidateUser(string username, string password, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            return !username.ValidateUser(password).IsAnonymous();
        }

        public User GetUserById(string id, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            return new ObjectId(id).GetUser();
        }


        public IEnumerable<User> GetUsers(long pageIndex, long pageSize, out long totalRecords, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            return Query.Null.GetUsers(pageIndex, pageSize, out totalRecords);
        }


        public IEnumerable<string> GetUsersInRole(string roleName, string username, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            if (username == null) return Query.EQ("r", roleName).GetUsers().Select(x => x.Username);
            else return Query.And(Query.EQ("r", roleName), Query.EQ("n", new Regex(Regex.Escape(username)))).GetUsers().Select(x => x.Username);
        }


        public IEnumerable<string> GetRoles(string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var app = new ObjectId(appKey).GetApp();
            return Query.And(Query.In("_id", new BsonArray(app.Roles)), Query.EQ("a", app.Id)).GetRoles().Union(new Role[] { Role.AppAdmin, Role.SysAdmin }).Select(x => x.Label);
        }

        public IEnumerable<string> GetRolesForUser(string username, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var u = username.GetUser();
            if (u.IsAnonymous()) return null;


            return Query.And(Query.In("_id", new BsonArray(
                Query.And(Query.EQ("u", u.Id), Query.EQ("a", new ObjectId(appKey)))
                .GetAppRoles()
                .SelectMany(x => x.Roles).Distinct())), Query.EQ("a", new ObjectId(appKey)))
                .GetRoles()
                .Select(x => x.Label);
        }


        public bool IsUserInRole(string username, string roleName, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var u = username.GetUser();
            if (u.IsAnonymous()) return false;
            else
            {
                var app = new ObjectId(appKey).GetApp();
                var r = roleName.GetRole(app.Id);
                if (app.Roles != null && r != null && app.Roles.Contains(r.Id))
                    return Query.And(Query.EQ("u", u.Id), Query.EQ("a", app.Id), Query.EQ("r", r.Id)).AnyAppRoles();
                else return false;
            }
        }

        public bool IsRoleExists(string roleName, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var app = new ObjectId(appKey).GetApp();
            Role r = null;
            return app.Roles != null && (r = roleName.GetRole(app.Id)) != null && app.Roles.Contains(r.Id);
        }


        public User GetUserByAccessToken(string clientIdentifier, string accessToken, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var t = accessToken.GetAccessToken();
            if (t == null) return null; // 如果token不存在
            else
            {
                t.Delete(); // 清理
                var app = clientIdentifier.GetApp();
                if (app == null || app.Label != clientIdentifier || app.Id != t.App) return null; // 如果app不存在或者与token不相符
                else
                {
                    var u = t.User.GetUser();
                    if (u == null) return null; // 如果用户不存在
                    else
                    {
                        if (!u.IsSysAdmin() && !u.IsAppAdmin(app) && (u.Apps == null || !u.Apps.Contains(t.App))) return null;
                        else return u;
                    }
                }
            }
        }

        public string GetAliasByAccessToken(string clientIdentifier, string accessToken, out bool ok, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            ok = false;
            var t = accessToken.GetAccessToken();
            if (t == null) return null; // 如果token不存在
            else
            {
                t.Delete();
                var app = clientIdentifier.GetApp();
                if (app == null || app.Label != clientIdentifier || app.Id != t.App) return null; // 如果app不存在或者与token不相符
                else
                {
                    var u = t.User.GetUser();
                    if (u == null) return null; // 如果用户不存在
                    else
                    {
                        ok = true;
                        var connection = u.Id.GetConnection(app.Id);
                        if (connection != null && !string.IsNullOrEmpty(connection.Alias)) return connection.Alias;
                        else if (u.IsSysAdmin() || u.IsAppAdmin(app)) return u.Username;
                        else return null;
                    }
                }
            }
        }


        public App GetApp(string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            ObjectId appid;
            if (ObjectId.TryParse(appKey, out appid))
            {
                App app = appid.GetApp();
                if (app != null && app.Secret == appSecret) return app;
                else return null;
            }
            else return null;
        }


        public void SetAliasByAccessToken(string clientIdentifier, string accessToken, string alias, out bool ok, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            ok = false;
            var t = accessToken.GetAccessToken();
            if (t == null) return; // 如果token不存在
            else
            {
                t.Delete();
                var app = clientIdentifier.GetApp();
                if (app == null || app.Label != clientIdentifier || app.Id != t.App) return; // 如果app不存在或者与token不相符
                else
                {
                    var u = t.User.GetUser();
                    if (u == null) return; // 如果用户不存在
                    else
                    {
                        ok = true;
                        new Connection { Alias = alias, App = app.Id, User = u.Id }.Save();

                    }
                }
            }
        }

        public bool CheckAction(string action, string[] roles, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var app = new ObjectId(appKey).GetApp();
            var a = action.GetAction();
            if (a == null || a.Roles == null || a.App != app.Id || app.Roles == null) return false;
            else
            {
                var r = a.Roles.Intersect(app.Roles);
                if (!r.Any()) return false;
                return Query.And(Query.In("_id", new BsonArray(r)), Query.In("l", new BsonArray(roles)), Query.EQ("a", app.Id)).AnyRole();
            }
        }


        public string GetActions(string username, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            var app = new ObjectId(appKey).GetApp();
            var u = username.GetUser();
            var actions = Query.EQ("a", app.Id).GetActions();
            if (u.IsSysAdmin() || u.IsAppAdmin(app))
            {
                return string.Join(",", actions.Select(x => x.Label));
            }
            else
            {
                var appRole = app.Id.GetAppRole(u.Id);
                if (appRole != null && appRole.Roles != null && appRole.Roles.Count > 0)
                {
                    return string.Join(",", actions.Where(x => appRole.ContainsRoles(x.Roles)).Select(x => x.Label));
                }
                else return string.Empty;
            }
        }


        public void SetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, string alias, out bool ok, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            ok = false;
            var t = accessToken.GetAccessToken();
            if (t == null) return; // 如果token不存在
            else
            {
                t.Delete();

                var app = clientIdentifier.GetApp();
                if (app == null || app.Label != clientIdentifier || app.Id != t.App) return; // 如果app不存在或者与token不相符
                else
                {
                    var u = username.GetUser(); // 被操作人
                    var user = t.User.GetUser(); // 操作人

                    BasicPlatform.Web.Models.Action a = "ConnectUsersOfApps".GetAction();
                    AppAction action = app.Id.GetAppAction(a.Id);
                    AppRole role = app.Id.GetAppRole(user.Id);
                    if (user.IsSysAdmin() || user.IsAppAdmin(app) || (role != null && action != null && action.Roles != null && role.Roles != null && role.Roles.Intersect(action.Roles).Any())) // 操作人权限检查
                    {
                        ok = true;
                        new Connection { Alias = alias, App = app.Id, User = u.Id }.Save();
                    }
                }
            }
        }

        public string GetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, out bool ok, string appKey, string appSecret)
        {
            CheckApp(appKey, appSecret);
            ok = false;
            var t = accessToken.GetAccessToken();
            if (t == null) return null; // 如果token不存在
            else
            {

                t.Delete();

                var app = clientIdentifier.GetApp();
                if (app == null || app.Label != clientIdentifier || app.Id != t.App) return null; // 如果app不存在或者与token不相符
                else
                {
                    var u = username.GetUser();
                    if (u == null) return null; // 如果用户不存在
                    else
                    {
                        ok = true;
                        var connection = u.Id.GetConnection(app.Id);
                        if (connection == null || string.IsNullOrEmpty(connection.Alias)) return null;
                        else return connection.Alias;
                    }
                }
            }
        }
    }
}
