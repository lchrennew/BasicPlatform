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
    public partial class RemoveRole : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var app = new ObjectId(ContextHelper.ObjId).GetApp();
            if (app != null)
            {
                if (app.Roles != null)
                {
                    if (app.Roles.Remove(new ObjectId(ContextHelper.Search)))
                        app.Save();
                }
            }
            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = false;
            PageResult.resetbtn = false;
            SendJson();
        }
    }
}