using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Users
{
    public partial class Edit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Web.Models.User u = ObjectId.Parse(ContextHelper.ObjId).GetUser();
                username.Text = u.Username;
                label.Text = u.Label;
                email.Text = u.Email;
                if (roles.Visible)
                {
                    InitRoles = u.Roles ?? new HashSet<ObjectId>();
                    roles.DataSource = Query.And(Query.NE("l", "AppAdmin"), Query.NotExists("a")).GetRoles();
                    roles.DataBind();
                }
                if (apps.Visible)
                {
                    InitApps = u.Apps ?? new HashSet<ObjectId>();
                    Aliases = Query.And(Query.EQ("u", u.Id), Query.In("a", new BsonArray(InitApps))).GetConnections().ToDictionary(x => x.App, x => x.Alias);
                    var appRoles = Query.And(Query.EQ("u", u.Id), Query.In("a", new BsonArray(InitApps))).GetAppRoles();
                    var appRoleIds = appRoles.SelectMany(x => x.Roles).Distinct().ToArray();
                    var appRoleDict = Query.And(Query.In("_id", new BsonArray(appRoleIds)), Query.Exists("a")).GetRoles().Union(new[] { Role.AppAdmin }).ToDictionary(x => x.Id);
                    InitAppRoles = appRoles.ToDictionary(x => x.App, x => x.Roles.Where(y => appRoleDict.ContainsKey(y)).Select(y => appRoleDict[y]));
                    apps.DataSource = Query.Null.GetApps();
                    apps.DataBind();
                }

            }
        }

        public HashSet<ObjectId> InitRoles { get; set; }
        public HashSet<ObjectId> InitApps { get; set; }
        public Dictionary<ObjectId, string> Aliases { get; set; }
        Dictionary<ObjectId, IEnumerable<Role>> InitAppRoles { get; set; }


        public string GetAppRoleIds(ObjectId app)
        {
            if (InitAppRoles.ContainsKey(app)) return HttpUtility.HtmlAttributeEncode(string.Join(",", InitAppRoles[app].Select(x => x.Id)));
            else return string.Empty;
        }

        public string GetAppRoleNames(ObjectId app)
        {
            if (InitAppRoles.ContainsKey(app)) return HttpUtility.HtmlAttributeEncode(string.Join(",", InitAppRoles[app].Select(x => x.Name)));
            else return string.Empty;
        }

        public string GetAlias(ObjectId app)
        {
            return Aliases.ContainsKey(app) ? Aliases[app] : string.Empty;
        }

        public bool IsSysAdmin { get; set; }

        protected void Save(object sender, EventArgs e)
        {

            PageResult.ok = true;
            Web.Models.User u = ObjectId.Parse(ContextHelper.ObjId).GetUser();
            if (u.IsAnonymous())
            {
                PageResult.showtip = false;
                PageResult.resetbtn = false;
                PageResult.reload = true;
                PageResult.url = "./";
            }
            else
            {
                string em = email.Text.Trim().ToLowerInvariant();

                if (u.Email.CompareTo(em) != 0 && !em.GetUser().IsAnonymous())
                {
                    PageResult.ok = false;
                    PageResult.errors[email.ID] = "email already exists";
                }
                else u.Email = em;

                if (roles.Visible)
                {
                    var r = Request.Form.GetValues(roles.ID);
                    if (r != null && r.Length > 0)
                        u.SetRoles(r.Select(x => new ObjectId(x)));
                    else
                        u.Roles = null;
                }

                if (PageResult.ok)
                {
                    u.Label = label.Text.Trim();
                    if (apps.Visible)
                    {
                        var a = Request.Form.GetValues(apps.ID);
                        if (a != null && a.Length > 0)
                        {
                            List<ObjectId> aids = a.Select(x => new ObjectId(x)).ToList();
                            u.SetApps(aids);

                            // TODO: 设置AppRole
                            aids.ForEach(x =>
                            {
                                if (!string.IsNullOrEmpty(Request.Form["roles" + x]))
                                {
                                    var appRole = x.GetAppRole(u.Id) ?? new AppRole { App = x, User = u.Id };
                                    appRole.SetRoles(Request.Form["roles" + x].Split(',').Select(y => new ObjectId(y)).Distinct());
                                    appRole.Save(false);
                                }
                                else new AppRole { App = x, User = u.Id }.Save(false);
                            });
                        }
                        else
                            u.Apps = null;
                    }

                    if (string.IsNullOrEmpty(password.Text))
                        u.Save();
                    else
                    {
                        u.Password = password.Text;
                        u.Save(true);
                    }
                    PageResult.reload = true;
                    PageResult.showtip = true;
                    PageResult.resetbtn = false;
                    PageResult.url = "./";
                }
            }

            SendJson();
        }
    }
}