using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using BasicPlatform.Web.Models;
using System.Text.RegularExpressions;

namespace BasicPlatform.Core
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var u = Query.In("n", new BsonArray(usernames)).GetUsers();
            var r = Query.And(Query.In("l", new BsonArray(roleNames)), Query.NotExists("a")).GetRoles().Select(x => x.Id);
            foreach (var user in u)
            {
                user.SetRoles(r);
                user.Save();
            }
        }

        public override string ApplicationName { get; set; }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return Query.And(Query.EQ("r", roleName), Query.EQ("n", new Regex(Regex.Escape(usernameToMatch)))).GetUsers().Select(x => x.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return Query.NotExists("a").GetRoles().Select(x => x.Label).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var u = username.GetUser();
            if (u == null || u.Roles == null) return new string[0];
            else return Query.And(Query.In("_id", new BsonArray(username.GetUser().Roles)), Query.NotExists("a")).GetRoles().Select(x => x.Label).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return Query.EQ("r", roleName).GetUsers().Select(x => x.Username).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var r = roleName.GetRole();
            if (r == null) return false;
            else return Query.And(Query.EQ("n", username), Query.EQ("r", r.Id)).GetUsers().Any();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return roleName.GetRole() != null;
        }
    }
}