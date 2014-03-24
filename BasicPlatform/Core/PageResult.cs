using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicPlatform.Core
{
    /// <summary>
    /// 页面返回结果
    /// </summary>
    public class PageResult
    {
        public bool ok;
        public string url = ContextHelper.GetReturnUrl();
        public bool showtip = true;
        public bool resetbtn = true;
        public bool reload = true;
        public Dictionary<string, string> errors = new Dictionary<string, string>();
        public dynamic data;
    }
}