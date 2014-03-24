using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;

namespace BasicPlatform.UserControls
{
    public partial class MgtNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public App App { get; set; }
        public int NavId { get; set; }
    }
}