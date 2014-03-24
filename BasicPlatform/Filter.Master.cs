using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicPlatform
{
    public partial class Filter : System.Web.UI.MasterPage
    {

        protected override void Render(HtmlTextWriter writer)
        {
            ph.RenderControl(writer);
        }
    }
}