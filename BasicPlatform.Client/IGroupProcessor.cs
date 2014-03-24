using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 用户组处理器
    /// </summary>
    public interface IGroupProcessor
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns>字典（键=ID，值=显示名称）</returns>
        Dictionary<string, string> GetAllGroups();

        /// <summary>
        /// 设置用户到用户组
        /// </summary>
        /// <param name="username">用户名(对应于OPS的Alias)</param>
        /// <param name="groups">分组ID列表（当用户失去入口权限时，groups可能为null或没有元素）</param>
        void SetGroupsOfUser(string username, params string[] groups);

        /// <summary>
        /// 获取用户所在组
        /// </summary>
        /// <param name="username"></param>
        /// <returns>返回组ID列表</returns>
        IEnumerable<string> GetGroupsOfUser(string username);
    }
}
