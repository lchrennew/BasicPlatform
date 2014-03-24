using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace BasicPlatform.Core
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class Button : HtmlButton
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.Attributes.Remove("onclick");
            this.Attributes.Add("name", this.UniqueID);
            this.Attributes["type"] = "submit";
            this.Attributes.Add("data-loading-text", this.LoadingText);
            base.Render(writer);
        }

        string loadingText;

        public string LoadingText
        {
            get { return loadingText ?? this.InnerHtml; }
            set { loadingText = value; }
        }
    }
}