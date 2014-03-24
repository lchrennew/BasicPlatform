using BasicPlatform.Core;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Ajax.Apps
{
    public partial class GetRoles : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App app = new ObjectId(ContextHelper.ObjId).GetApp();
            if (app != null)
            {
                var roles = Query.And(Query.In("_id", new BsonArray(app.Roles)), Query.EQ("a", app.Id)).GetRoles();
                if (ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(app))
                    roles = new[] { Role.AppAdmin }.Union(roles);
                Response.Write(BasicPlatform.Core.JsonHelper.ToJson(roles.Select(x => new { id = x.Id.ToString(), text = x.Name })));
            }
            Response.End();
        }
    }
}