using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using MongoDB.Bson;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Ajax.Connections
{
    public partial class Disconnect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection con = new ObjectId(ContextHelper.Search).GetConnection();

            if (con != null)
            {
                App app = con.App.GetApp();
                User u = ContextHelper.User;
                BasicPlatform.Web.Models.Action a = "DisconnectUsersOfApps".GetAction();
                AppAction action = app.Id.GetAppAction(a.Id);
                AppRole role = app.Id.GetAppRole(u.Id);

                if (u.Id == con.User)
                    con.Delete();
                else if (u.IsSysAdmin() || u.IsAppAdmin(app) || (role != null && action != null && action.Roles != null && role.Roles != null && role.Roles.Intersect(action.Roles).Any()))
                    con.Delete();
            }

            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = false;
            SendJson();
        }
    }
}