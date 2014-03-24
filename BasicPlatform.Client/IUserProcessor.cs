using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 用户信息处理接口
    /// </summary>
    public interface IUserProcessor
    {
        /// <summary>
        /// 用户名对应的用户是否存在
        /// </summary>
        /// <param name="username">小写用户名</param>
        /// <returns></returns>
        bool Exists(string username);

        /// <summary>
        /// 如果用户名不存在则自动创建用户，若存在则返回
        /// </summary>
        /// <param name="username">小写用户名</param>
        void CreateIfNotExists(string username);


    }
}
