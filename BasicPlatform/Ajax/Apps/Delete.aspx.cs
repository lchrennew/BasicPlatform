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
    public partial class Delete : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new ObjectId(ContextHelper.Search).DeleteApp();
            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = false;
            SendJson();
        }
    }
}