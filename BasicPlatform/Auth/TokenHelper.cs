using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using BasicPlatform.Web.Models;
using System.Web.Security;

namespace BasicPlatform.Auth
{
    public static class TokenHelper
    {
        static ITokenProvider tokenProvider = (Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["ITokenProvider"] ?? "", false, false) ?? typeof(DefaultTokenProvider)) as ITokenProvider) ?? new DefaultTokenProvider();

        /// <summary>
        /// 生成请求令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public static string GenerateRequestToken(User user, App app, string userToken)
        {
            return tokenProvider.GenerateRequestToken(user, app, userToken);
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="requestToken"></param>
        /// <returns></returns>
        public static string GenerateAccessToken(User user, App app, string requestToken)
        {
            return tokenProvider.GenerateAccessToken(user, app, requestToken);
        }

        /// <summary>
        /// 校验加密后的request_token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="userToken"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static bool ValidateRequestToken(User user, App app, string requestToken, string secret)
        {
            return tokenProvider.ValidateRequestToken(user, app, requestToken, secret);

        }

        /// <summary>
        /// 验证访问令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static bool ValidateAccessToken(User user, App app, string accessToken)
        {
            return tokenProvider.ValidateAccessToken(user, app, accessToken);
        }
    }
}