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
    public partial class EditAppAction : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (!IsPostBack)
            {
                A action = new ObjectId(ContextHelper.Search).GetAction();
                if (action == null || !action.Individual) Response.Redirect("Actions.aspx?id=" + App.Id, true);
                else
                {
                    name.Text = action.Name;
                    label.Text = action.Label;

                    // 绑定app可用角色（不包括AppAdmin，因为AppAdmin具有全部权限，不需要在这里控制）
                    if (App.Roles != null && App.Roles.Count > 0)
                        roles.DataSource = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();

                    // 初始化的角色表，应当使用AppAction的
                    var appAction = App.Id.GetAppAction(action.Id);
                    if (appAction == null || appAction.Roles == null) InitRoles = new HashSet<ObjectId>();
                    else
                        InitRoles = appAction.Roles;
                }
            }
            DataBind();
        }

        public App App { get; set; }
        public HashSet<ObjectId> InitRoles { get; set; }

        protected void Save(object sender, EventArgs e)
        {
            A action = new ObjectId(ContextHelper.Search).GetAction();
            if (action == null || !action.Individual)
                SendJson("Actions.aspx?id=" + App.Id);
            else
            {
                AppAction appAction = App.Id.GetAppAction(action.Id) ?? new AppAction { App = App.Id, Action = action.Id };

                PageResult.ok = true;

                var r = Request.Form.GetValues("roles");
                if (r != null && r.Length > 0)
                {
                    appAction.Roles = new HashSet<ObjectId>(r.Select(x => new ObjectId(x)));
                    appAction.Save();
                }
                else if (appAction.Id != default(ObjectId))
                    appAction.Delete();
                PageResult.reload = true;
                PageResult.resetbtn = false;
                PageResult.showtip = true;
                PageResult.url = "Actions.aspx?id=" + App.Id;

                SendJson();

            }
        }
    }
}