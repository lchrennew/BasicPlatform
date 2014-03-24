using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Apps
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("SysAdmin"))
            {
                long totalRecords;
                rpt.DataSource = ContextHelper.Filter.GetApps(pager.CurrentPage = ContextHelper.PageIndex, pager.PageSize, out totalRecords);
                pager.TotalRecords = totalRecords;
                pager.LinkPattern = ContextHelper.PagePattern;
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = ContextHelper.Filter.GetApps();
                rpt.DataBind();
            }
        }
    }
}