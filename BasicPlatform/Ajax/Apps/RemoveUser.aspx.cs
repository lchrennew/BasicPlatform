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
    public partial class RemoveUser : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = new ObjectId(ContextHelper.Search).GetUser();
            var app = new ObjectId(ContextHelper.ObjId);
            user.Apps.Remove(new ObjectId(ContextHelper.ObjId));
            user.Save();
            app.GetAppRole(user.Id).Delete();
            PageResult.ok = true;
            PageResult.reload = true;
            SendJson();
        }
    }
}