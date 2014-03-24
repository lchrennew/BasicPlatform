using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Web;

namespace BasicPlatform.Client
{
    class Permission : IPermission
    {
        static MemoryCache cache = new MemoryCache("bpo");
        public bool CheckPermission(string action)
        {
            var ctx = HttpContext.Current;
            string key = ctx.User.Identity.Name;
            HashSet<string> actions = (ctx.Items["actions"] ?? (ctx.Items["actions"] = cache.Get(key) ?? cache.AddOrGetExisting(key, Bpc.GetActions(key), DateTime.Now.AddMinutes(5)))) as HashSet<string>;
            return actions != null && actions.Contains(action);
        }
    }
}
