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
using A = BasicPlatform.Web.Models.Action;

namespace BasicPlatform.Actions
{
    public partial class Edit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Action = new ObjectId(ContextHelper.ObjId).GetAction();
                if (Action == null || Action.App != default(ObjectId)) Response.Redirect("./", true);
                else
                {
                    name.Text = Action.Name;
                    label.Text = Action.Label;
                    roles.DataSource = Query.NotExists("a").GetRoles();
                    InitRoles = Action.Roles ?? new HashSet<ObjectId>();
                    DataBind();
                }
            }
        }

        public ObjectId InitApp { get; set; }
        public HashSet<ObjectId> InitRoles { get; set; }
        public A Action { get; set; }

        protected void Save(object sender, EventArgs e)
        {
            PageResult.ok = true;
            Action = new ObjectId(ContextHelper.ObjId).GetAction();
            if (Action == null || Action.App != default(ObjectId)) SendJson("./");
            else
            {
                string l = label.Text.Trim();
                if (Action.Label.CompareTo(l) != 0 && (Action.Label = l).GetApp() != null)
                {
                    PageResult.ok = false;
                    PageResult.errors[label.ID] = "label exists";
                }

                if (PageResult.ok)
                {
                    Action.Name = name.Text.Trim();

                    if (roles.Visible)
                    {
                        var r = Request.Form.GetValues("roles");

                        if (r != null && r.Length > 0)
                            Action.SetRoles(r.Select(x => new ObjectId(x)));
                        else Action.Roles = null;
                    }
                    Action.Save();
                    PageResult.reload = true;
                    PageResult.resetbtn = false;
                    PageResult.showtip = true;
                    PageResult.url = "./";
                }
                SendJson();

            }
        }
    }
}