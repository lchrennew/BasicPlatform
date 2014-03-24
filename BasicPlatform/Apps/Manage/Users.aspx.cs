using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using BasicPlatform.Core;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Apps.Manage
{
    public partial class Users : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            App.AddRoles(Role.AppAdmin.Id);
            users = Query.And(Query.EQ("a", App.Id), ContextHelper.Filter).GetUsers();
            appRoles = Query.EQ("a", App.Id).GetAppRoles().ToDictionary(x => x.User);
            roles = Query.And(Query.In("_id", new BsonArray(appRoles.SelectMany(x => x.Value.Roles).Distinct().Where(x => App.Roles.Contains(x)))), Query.EQ("a", App.Id)).GetRoles();
            connections = Query.And(Query.In("u", new BsonArray(users.Select(x => x.Id))), Query.EQ("a", App.Id)).GetConnections().ToDictionary(x => x.User, x => x.Alias.ToLowerInvariant());

            if (!string.IsNullOrEmpty(Alias))
            {
                users = users.Where(u => connections.ContainsKey(u.Id) && connections[u.Id].Contains(Alias.ToLowerInvariant()));
            }
            if (Roles != null && Roles.Length > 0)
            {
                users = users.Where(u => appRoles.ContainsKey(u.Id) && appRoles[u.Id].ContainsRoles(Roles));
            }

            rpt.DataSource = users;
            DataBind();
        }
        Dictionary<ObjectId, AppRole> appRoles;
        IEnumerable<Role> roles;
        IEnumerable<User> users;
        Dictionary<ObjectId, string> connections;
        public App App { get; set; }

        public string Alias { get { return Request.QueryString["alias"]; } }
        ObjectId[] Roles { get { return Request.QueryString["roles"] == null ? null : Request.QueryString["roles"].Split(',').Select(x => new ObjectId(x)).ToArray(); } }

        protected string GetAlias(ObjectId user)
        {
            if (connections.ContainsKey(user)) return connections[user];
            else return null;
        }

        protected IEnumerable<Role> GetRoles(ObjectId user)
        {
            if (appRoles.ContainsKey(user))
            {
                var roles = appRoles[user].Roles;
                if (roles == null) return new Role[0];
                else if (roles.Contains(Role.AppAdmin.Id))
                    return new[] { Role.AppAdmin };
                else
                    return roles.Join(this.roles, x => x, y => y.Id, (x, y) => y);
            }
            else return new Role[0];
        }
    }
}