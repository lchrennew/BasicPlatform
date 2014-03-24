using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicPlatform.Client.Bps;
using System.IO;
using System.Web.Security;
using System.Web;

namespace BasicPlatform.Client
{
    internal static class Bpc
    {
        static FileChannelFactory<IService> factory;
        static ServiceClient client;

        static Bpc()
        {
            try
            {
                factory = new FileChannelFactory<IService>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\BasicPlatform.Client.config"));
                client = new ServiceClient(factory.Endpoint.Binding, factory.Endpoint.Address);
                client.ClientCredentials.UserName.UserName = Config.apiKey;
                client.ClientCredentials.UserName.Password = Config.apiSecret;
            }
            catch (Exception e)
            {
                throw new Exception("Bps服务配置加载失败", e);
            }
        }

        internal static User GetUserByAlias(string alias)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var u = context.Items["u"] as User;
                if (u != null) return u;
                else
                {
                    if (context.Items["not_connected"] != null) return null;
                    context.Items["u"] = u = client.GetUserByAlias(alias, Config.apiKey, Config.apiSecret);
                    if (u.Username == alias) context.Items["not_connected"] = false;
                    return u;
                }
            }
            else
            {
                return client.GetUserByAlias(alias, Config.apiKey, Config.apiSecret);
            }
        }

        internal static string GetUsernameByAlias(string alias)
        {
            var u = GetUserByAlias(alias);
            if (u == null) return alias;
            else return u.Username;
        }

        internal static bool ValidateUser(string username, string password)
        {
            if (Config.connected) username = GetUsernameByAlias(username);
            return client.ValidateUser(username, password, Config.apiKey, Config.apiSecret);
        }

        internal static User GetUser(string username)
        {
            if (Config.connected) username = GetUsernameByAlias(username);
            return client.GetUser(username, Config.apiKey, Config.apiSecret);
        }

        internal static User GetUserById(string id)
        {
            return client.GetUserById(id, Config.apiKey, Config.apiSecret);
        }

        internal static IEnumerable<User> GetUsers(long pageIndex, long pageSize, out long totalRecords)
        {
            return client.GetUsers(out totalRecords, pageIndex, pageSize, Config.apiKey, Config.apiSecret);
        }

        internal static MembershipUser AsUser(this User u, string appName)
        {
            if (u == null) return null;
            var m = new MembershipUser(appName, u.Username, u.Id, u.Email, string.Empty, string.Empty, true, false, default(DateTime), default(DateTime), default(DateTime), default(DateTime), default(DateTime));
            return m;
        }

        internal static string[] GetUsersInRole(string roleName, string username = null)
        {
            return client.GetUsersInRole(roleName, username, Config.apiKey, Config.apiSecret).ToArray();
        }

        internal static string[] GetRolesForUser(string username)
        {
            if (Config.connected) username = GetUsernameByAlias(username);
            var r = client.GetRolesForUser(username, Config.apiKey, Config.apiSecret).ToArray();
            return r;
        }

        internal static bool IsUserInRole(string username, string roleName)
        {
            if (Config.connected) username = GetUsernameByAlias(username);
            return client.IsUserInRole(username, roleName, Config.apiKey, Config.apiSecret);
        }

        internal static bool RoleExists(string roleName)
        {
            return client.IsRoleExists(roleName, Config.apiKey, Config.apiSecret);
        }

        internal static string[] GetAllRoles()
        {
            return client.GetRoles(Config.apiKey, Config.apiSecret).ToArray();
        }

        internal static User GetUserByAccessToken(string accessToken)
        {
            return client.GetUserByAccessToken(Config.clientIdentifier, accessToken, Config.apiKey, Config.apiSecret);
        }

        internal static string GetAliasByAccessToken(string accessToken, out bool ok)
        {
            return client.GetAliasByAccessToken(out ok, Config.clientIdentifier, accessToken, Config.apiKey, Config.apiSecret);
        }

        internal static void SetAliasByAccessToken(string accessToken, string alias, out bool ok)
        {
            ok = client.SetAliasByAccessToken(Config.clientIdentifier, accessToken, alias, Config.apiKey, Config.apiSecret);
        }

        internal static string GetAliasOfUserByAccessToken(string accessToken, string username, out bool ok)
        {
            return client.GetAliasOfUserByAccessToken(out ok, Config.clientIdentifier, accessToken, username, Config.apiKey, Config.apiSecret);
        }

        internal static void SetAliasOfUserByAccessToken(string accessToken, string username, string alias, out bool ok)
        {
            ok = client.SetAliasOfUserByAccessToken(Config.clientIdentifier, accessToken, username, alias, Config.apiKey, Config.apiSecret);
        }

        internal static App GetApp()
        {
            return client.GetApp(Config.apiKey, Config.apiSecret);
        }

        internal static HashSet<string> GetActions(string username)
        {
            return new HashSet<string>(client.GetActions(username, Config.apiKey, Config.apiSecret).Split(','));
        }

    }
}
