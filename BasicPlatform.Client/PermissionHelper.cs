using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 权限帮助器
    /// </summary>
    public static class PermissionHelper
    {
        static IPermission permission = Activator.CreateInstance(Type.GetType(Config.iPermission, false, true) ?? typeof(Permission)) as IPermission ?? new Permission();

        /// <summary>
        /// 检查某个操作是否有权进行
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Check(string action)
        {
            if (permission == null || string.IsNullOrEmpty(action)) return false;
            else return permission.CheckPermission(action);
        }

    }
}
