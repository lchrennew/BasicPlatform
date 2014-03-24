using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using MongoDB.Bson;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Ajax.Apps
{
    public partial class RemoveAction : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var app = new ObjectId(ContextHelper.ObjId).GetApp();
            var action = new ObjectId(ContextHelper.Search).GetAction();
            if (action != null && action.App == new ObjectId(ContextHelper.ObjId))
                action.Delete();
            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = false;
            PageResult.resetbtn = false;
            SendJson("Actions.aspx?id=" + app.Id);
        }
    }
}