using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using A = BasicPlatform.Web.Models.Action;

namespace BasicPlatform.Apps.Manage
{
    public partial class CreateAction : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (!IsPostBack)
            {
                if (App.Roles != null && App.Roles.Count > 0)
                {
                    roles.DataSource = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
                }
            }
            DataBind();
        }

        public App App { get; set; }

        protected void Create(object sender, EventArgs e)
        {
            PageResult.ok = true;
            A action = new A();
            action.Name = name.Text.Trim();
            if ((action.Label = label.Text.Trim()).GetAction(App.Id) != null)
            {
                PageResult.ok = false;
                PageResult.errors[label.ID] = "label already exists";
            }
            var r = Request.Form.GetValues("roles");
            if (r != null && r.Length > 0)
            {
                action.SetRoles(r.Select(x => new ObjectId(x)));
            }

            if (PageResult.ok)
            {
                action.App = App.Id;
                action.Save();
                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.url = "Actions.aspx?id=" + App.Id;
            }
            SendJson();
        }
    }
}