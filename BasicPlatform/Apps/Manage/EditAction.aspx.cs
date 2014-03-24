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
    public partial class EditAction : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (!IsPostBack)
            {
                A action = new ObjectId(ContextHelper.Search).GetAction();
                if (action == null || (action.App != App.Id)) Response.Redirect("Actions.aspx?id=" + App.Id, true);
                else
                {
                    name.Text = action.Name;
                    label.Text = action.Label;
                    if (App.Roles != null && App.Roles.Count > 0)
                    {
                        roles.DataSource = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
                    }
                    InitRoles = action.Roles ?? new HashSet<ObjectId>();
                }
            }
            DataBind();
        }
        public App App { get; set; }
        public HashSet<ObjectId> InitRoles { get; set; }

        protected void Save(object sender, EventArgs e)
        {
            PageResult.ok = true;
            A action = new ObjectId(ContextHelper.Search).GetAction();
            if (action == null)
                SendJson("Actions.aspx?id=" + App.Id);
            else
            {
                string l = label.Text.Trim();
                var a = l.GetAction(App.Id);
                if (a != null
                    && a.Id != action.Id
                    && action.Label == a.Label)
                {
                    PageResult.ok = false;
                    PageResult.errors[label.ID] = "label exists";
                }

                if (roles.Visible)
                {
                    var r = Request.Form.GetValues("roles");
                    if (r != null && r.Length > 0)
                        action.SetRoles(r.Select(x => new ObjectId(x)));
                    else action.Roles = null;
                }

                if (PageResult.ok)
                {
                    action.Name = name.Text.Trim();
                    action.Save();
                    PageResult.reload = true;
                    PageResult.resetbtn = false;
                    PageResult.showtip = true;
                    PageResult.url = "Actions.aspx?id=" + App.Id;
                }
                SendJson();

            }
        }
    }
}