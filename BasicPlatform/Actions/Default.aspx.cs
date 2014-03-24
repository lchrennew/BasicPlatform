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

namespace BasicPlatform.Actions
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long totalRecords;
            var f = ContextHelper.Filter;
            if (f != Query.Null) f = Query.And(f, Query.NotExists("a"));
            else f = Query.NotExists("a");
            var actions = f.GetActions(pager.CurrentPage = ContextHelper.PageIndex, pager.PageSize, out totalRecords);
            rpt.DataSource = actions;
            pager.TotalRecords = totalRecords;
            pager.LinkPattern = ContextHelper.PagePattern;
            var r = Query.And(Query.In("_id", new BsonArray(actions.Where(x => x.Roles != null).SelectMany(x => x.Roles).Distinct())), Query.NotExists("a")).GetRoles().ToDictionary(x => x.Id);
            roles = actions.Where(x => x.Roles != null).ToDictionary(x => x.Id, x => x.Roles.Where(y => r.ContainsKey(y)).Select(y => r[y]));
            rpt.DataBind();
        }
        Dictionary<ObjectId, IEnumerable<Role>> roles;
        protected IEnumerable<Role> GetRoles(ObjectId action)
        {
            return roles.ContainsKey(action) ? roles[action] : null;
        }
    }
}