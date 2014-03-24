using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 权限处理接口
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// 权限判定
        /// </summary>
        /// <param name="action">操作Label</param>
        /// <returns>有权则返回true，无权则返回false</returns>
        bool CheckPermission(string action);
    }
}
