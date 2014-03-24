using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService”。
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        User GetUserByAlias(string alias, string appKey, string appSecret);

        [OperationContract]
        User GetUser(string username, string appKey, string appSecret);

        [OperationContract]
        User GetUserById(string id, string appKey, string appSecret);

        [OperationContract]
        bool ValidateUser(string username, string password, string appKey, string appSecret);

        [OperationContract]
        IEnumerable<User> GetUsers(long pageIndex, long pageSize, out long totalRecords, string appKey, string appSecret);

        [OperationContract]
        IEnumerable<string> GetUsersInRole(string roleName, string username, string appKey, string appSecret);

        [OperationContract]
        IEnumerable<string> GetRoles(string appKey, string appSecret);

        [OperationContract]
        IEnumerable<string> GetRolesForUser(string username, string appKey, string appSecret);

        [OperationContract]
        bool IsUserInRole(string username, string roleName, string appKey, string appSecret);

        [OperationContract]
        bool IsRoleExists(string roleName, string appKey, string appSecret);

        [OperationContract]
        App GetApp(string appKey, string appSecret);

        [OperationContract]
        User GetUserByAccessToken(string clientIdentifier, string accessToken, string appKey, string appSecret);

        [OperationContract]
        void SetAliasByAccessToken(string clientIdentifier, string accessToken, string alias, out bool ok, string appKey, string appSecret);

        [OperationContract]
        string GetAliasByAccessToken(string clientIdentifier, string accessToken, out bool ok, string appKey, string appSecret);

        [OperationContract]
        void SetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, string alias, out bool ok, string appKey, string appSecret);

        [OperationContract]
        string GetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, out bool ok, string appKey, string appSecret);

        [OperationContract]
        string GetActions(string username, string appKey, string appSecret);
    }
}
