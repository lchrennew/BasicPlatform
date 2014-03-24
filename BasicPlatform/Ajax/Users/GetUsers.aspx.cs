using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver.Builders;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace BasicPlatform.Ajax.Users
{
    public partial class GetUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var regex = Regex.Escape(ContextHelper.Search);

            var users = Query.Or(Query.Matches("l", regex), Query.Matches("n", regex), Query.Matches("e", regex)).GetUsers().Where(x => !x.ContainsRoles(Role.SysAdmin.Id));
            if (!string.IsNullOrEmpty(ContextHelper.ObjId))
            {
                var app = new ObjectId(ContextHelper.ObjId);
                var appAdmins = Query.And(Query.EQ("a", app), Query.EQ("r", Role.AppAdmin.Id)).GetAppRoles().Select(x => x.User);
                users = users.Where(x => !appAdmins.Contains(x.Id));
            }

            Response.Write(JsonHelper.ToJson(users.Select(x => new { id = x.Id.ToString(), l = x.Label, n = x.Username, e = x.Email })));
            Response.End();
        }
    }
}