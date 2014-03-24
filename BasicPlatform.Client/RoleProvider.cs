using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Caching;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 角色提供者
    /// </summary>
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        static MemoryCache cache = new MemoryCache("role");
        static RoleProvider()
        {
            Config.Initialize();
        }
        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleName"></param>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="throwOnPopulatedRole"></param>
        /// <returns></returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据角色找用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="usernameToMatch"></param>
        /// <returns></returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return Bpc.GetUsersInRole(roleName, usernameToMatch);
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            return Bpc.GetAllRoles();
        }

        /// <summary>
        /// 根据用户名找角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            var ctx = HttpContext.Current;
            string key = username;
            HashSet<string> roles = ctx.Items["roles"] as HashSet<string>;
            if (roles == null)
            {
                roles = cache.Get(key) as HashSet<string>;
                if (roles == null)
                {
                    ctx.Items["roles"] = roles = new HashSet<string>(Bpc.GetRolesForUser(key));
                    cache.Add(key, roles, DateTime.Now.AddMinutes(5));
                }
            }

            return roles.ToArray();
        }

        /// <summary>
        /// 根据角色找用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override string[] GetUsersInRole(string roleName)
        {
            return Bpc.GetUsersInRole(roleName);
        }

        /// <summary>
        /// 判断是否用户具有角色
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var ctx = HttpContext.Current;
            string key = username;
            HashSet<string> roles = ctx.Items["roles"] as HashSet<string>;
            if (roles == null)
            {
                roles = cache.Get(key) as HashSet<string>;
                if (roles == null)
                {
                    ctx.Items["roles"] = roles = new HashSet<string>(Bpc.GetRolesForUser(key));
                    cache.Add(key, roles, DateTime.Now.AddMinutes(5));
                }
            }

            if (roles == null) return false;
            return roles.Contains(roleName);
        }

        /// <summary>
        /// 从用户中移除角色
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 角色是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool RoleExists(string roleName)
        {
            return Bpc.RoleExists(roleName);
        }
    }
}
