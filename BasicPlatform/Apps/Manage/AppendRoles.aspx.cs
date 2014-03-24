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
    public partial class AppendRoles : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (!IsPostBack)
            {
                roles.DataSource = Query.EQ("a", App.Id).GetRoles();
            }
            DataBind();
        }
        public App App { get; set; }

        protected void Grant(object sender, EventArgs e)
        {
            var r = Request.Form.GetValues(roles.ID);
            if (r != null && r.Length > 0)
            {
                App.AddRoles(r.Select(x => new ObjectId(x)));
                App.Save();
            }
            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = true;
            PageResult.resetbtn = true;
            SendJson("Roles.aspx?id=" + App.Id);
        }
    }
}