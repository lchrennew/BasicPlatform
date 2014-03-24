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
    public partial class Actions : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            var a = Query.EQ("i", true).GetActions().Concat(Query.EQ("a", App.Id).GetActions());
            if (App.Roles != null && App.Roles.Count > 0)
                roles = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
            rpt.DataSource = a;
            actions = a.ToDictionary(x => x.Id);
            appActions = Query.EQ("a", App.Id).GetAppActions().ToDictionary(x => x.Action);
            DataBind();
        }

        IEnumerable<Role> roles;
        Dictionary<ObjectId, BasicPlatform.Web.Models.Action> actions;
        Dictionary<ObjectId, AppAction> appActions;

        public App App { get; set; }

        protected IEnumerable<Role> GetRoles(ObjectId action)
        {
            IEnumerable<ObjectId> r = null;
            if (roles != null)
            {
                var a = actions[action];
                if (a.Individual)
                {
                    if (appActions.ContainsKey(action))
                        r = appActions[action].Roles;
                }
                else
                {
                    r = actions[action].Roles;
                }
            }
            if (r == null) return new Role[0];
            else
                return roles.Where(x => r.Contains(x.Id));
        }

    }
}