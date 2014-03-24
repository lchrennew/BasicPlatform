using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using MongoDB.Driver.Builders;
using System.Text;
using BasicPlatform.Web.Models;
using System.Configuration;

namespace BasicPlatform.Core
{
    /// <summary>
    /// 上下文帮助器
    /// </summary>
    public class ContextHelper
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public static string GetReturnUrl(string defaultUrl = null)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["returnurl"])) return HttpContext.Current.Request["returnurl"];
            else if (defaultUrl != null) return defaultUrl;
            else if (HttpContext.Current.Request.UrlReferrer != null) return HttpContext.Current.Request.UrlReferrer.AbsolutePath;
            else return RequestHelper.RawUrl();
        }
        /// <summary>
        /// 页面对象Id
        /// </summary>
        public static string ObjId { get { return HttpContext.Current.Request["id"]; } }

        /// <summary>
        /// 范围分页时的锚点位置
        /// </summary>
        public static string Range { get { return (HttpContext.Current.Request["range"] ?? string.Empty).Trim('-'); } }

        /// <summary>
        /// 范围分页时的锚点方向
        /// </summary>
        public static bool RangeDirection { get { return !(HttpContext.Current.Request["range"] ?? string.Empty).StartsWith("-"); } }

        /// <summary>
        /// 范围分页是否第一页
        /// </summary>
        public static bool IsFirstPage { get { return string.IsNullOrEmpty(HttpContext.Current.Request["range"]); } }

        /// <summary>
        /// 查询字符串中的搜索值
        /// </summary>
        public static string Search { get { return HttpContext.Current.Request["$"] ?? string.Empty; } }

        /// <summary>
        /// 从URL获取QueryDoc，用于查询MongoDB
        /// </summary>
        public static IMongoQuery Filter
        {
            get
            {
                IMongoQuery q = Query.Null;
                BsonDocument doc = null;
                try
                {
                    doc = BsonDocument.Parse(HttpContext.Current.Request.QueryString["$"] ?? "{}");    // 筛选采用参数$
                    q = new QueryDocument();
                }
                catch
                {
                    return q;    // 如果传参出错，直接返回qdoc
                }

                // 遍历每个文档字段，字段名称规则
                foreach (var element in doc.Elements)
                {
                    BsonValue value = element.Value;
                    if (value == null) value = BsonNull.Value;

                    var parts = element.Name.Trim("~!*-".ToCharArray()).Split("~!*-".ToCharArray());    // 分割符号可以使用~!*-
                    if (parts.Length < 2) continue;
                    /**
                     * 字段规则：
                     * 1、数据库中的数据字段名称（全名称），如Properties.created
                     * 2、数据类型，QueryDoc会将字段值转换为此类型，注意区分大小写！现在支持的包括
                     *      Date（日期）
                     *      Regex（正则模糊匹配）
                     *      Int（整型）
                     *      Float（浮点）
                     *      String（字符串）
                     *      注意！如果传入多个值，则必须使用运算符，并且只使用可转换为目标类型的有效值，并会对值进行排重和排序。
                     *          例如：
                     *          { 'Status:Int:$in': ["2","3", 1,2,"abcd","123"]}，则会被转换为{"Status":{$in:[1,2,3,123]}}
                     *          例如： 
                     *          { 'Tags:String:$all':[1, 'test'] }，则会被转换为 {"Tags":{$all: ["1", "test"]}}
                     * 3、操作符，即MongoDB中的筛选表达式比较运算符，如$lt、$gte等，如果不传，按照等于进行比较
                     */

                    switch (parts[1])
                    {
                        case "_":   // ObjectId
                            if (value.IsString && !string.IsNullOrEmpty(value.AsString))
                            {
                                ObjectId i;
                                if (ObjectId.TryParse(value.AsString, out i)) value = i;
                                else value = BsonNull.Value;
                            }
                            else if (value.IsBsonArray)
                            {
                                ObjectId i;
                                value = new BsonArray(value.AsBsonArray
                                    .Select(x =>
                                    {
                                        if (ObjectId.TryParse(x.ToString(), out i)) return i;
                                        else return ObjectId.Empty;
                                    })
                                    .Where(x => x != ObjectId.Empty)
                                    .OrderBy(x => x)
                                    .Distinct());
                            }
                            else value = BsonNull.Value;
                            break;
                        case "b":
                            if (value.IsString && !string.IsNullOrEmpty(value.AsString))
                            {
                                if (value.AsString.ToLowerInvariant() == "true" || value.AsString.ToLowerInvariant() == "1") value = true;
                                else if (value.AsString.ToLowerInvariant() == "false" || value.AsString.ToLowerInvariant() == "0") value = false;
                                else value = BsonNull.Value;
                            }
                            else if (value.IsInt32)
                            {
                                value = value.AsInt32 > 0;
                            }
                            else if (!value.IsBoolean)
                            {
                                value = BsonNull.Value;
                            }
                            break;
                        case "t":   // Time
                            if (value.IsString && !string.IsNullOrEmpty(value.AsString))
                            {
                                DateTime d;
                                if (DateTime.TryParse(value.AsString, out d)) value = d;
                                else value = BsonNull.Value;
                            }
                            else if (!value.IsBsonDateTime) value = BsonNull.Value;
                            break;
                        case "d":   // Int
                            if (value.IsString && !string.IsNullOrEmpty(value.AsString))
                            {
                                int i;
                                if (int.TryParse(value.AsString, out i))
                                {
                                    value = i;
                                }
                                else value = BsonNull.Value;

                            }
                            else if (value.IsBsonArray)
                            {
                                int i;
                                value = new BsonArray(value.AsBsonArray
                                    .Select(x =>
                                    {
                                        if (int.TryParse(x.ToString(), out i)) return i;
                                        else return int.MaxValue;
                                    })
                                    .Where(x => x < int.MaxValue)
                                    .OrderBy(x => x)
                                    .Distinct());
                            }
                            else if (!value.IsInt32) value = BsonNull.Value;
                            break;
                        case "f":   // Float
                            if (value.IsString && !string.IsNullOrEmpty(value.AsString))
                            {
                                double d;
                                if (double.TryParse(value.AsString, out d)) value = d;
                                else value = BsonNull.Value;
                            }
                            else if (value.IsBsonArray)
                            {
                                double i;
                                value = new BsonArray(value.AsBsonArray
                                    .Select(x =>
                                    {
                                        if (double.TryParse(x.ToString(), out i)) return i;
                                        else return double.MaxValue;
                                    })
                                    .Where(x => x < double.MaxValue)
                                    .OrderBy(x => x)
                                    .Distinct());
                            }
                            else if (!value.IsDouble) value = BsonNull.Value;
                            break;
                        case "s":   // String
                            if (value.IsBsonArray)
                            {
                                value = new BsonArray(value.AsBsonArray
                                    .Select(x => x.ToString())
                                    .Distinct()
                                    .OrderBy(x => x));
                            }
                            break;
                        case "r":   // Regex
                            value = new BsonRegularExpression(value.ToString());
                            break;
                        default:
                            break;
                    }

                    string field = parts[0];
                    // 构造qdoc的元素字段
                    if (parts.Length == 2)
                    {
                        q = Query.And(q, Query.EQ(field, value));
                    }
                    else if (parts.Length == 3)
                    {
                        string op = parts[2];
                        switch (op)
                        {
                            case "all":
                                if (value.IsBsonArray)
                                    q = Query.And(q, Query.All(field, value.AsBsonArray));
                                else if (!value.IsBsonNull) q = Query.And(q, Query.EQ(field, value));
                                break;
                            case "exists":
                                q = Query.And(q, Query.Exists(field));
                                break;
                            case "gt":
                                if (!value.IsBsonNull) q = Query.And(q, Query.GT(field, value));
                                break;
                            case "gte":
                                if (!value.IsBsonNull) q = Query.And(q, Query.GTE(field, value));
                                break;
                            case "in":
                                if (value.IsBsonArray) q = Query.And(q, Query.In(field, value.AsBsonArray));
                                else if (!value.IsBsonNull) q = Query.And(q, Query.EQ(field, value));
                                break;
                            case "lt":
                                if (value.IsBsonNull) q = Query.And(q, Query.LT(field, value));
                                break;
                            case "lte":
                                if (!value.IsBsonNull) q = Query.And(q, Query.LTE(field, value));
                                break;
                            case "regex":
                                if (value.IsString) q = Query.And(q, Query.Matches(field, Regex.Escape(value.AsString)));
                                else if (value.IsBsonRegularExpression) q = Query.And(q, Query.Matches(field, value.AsBsonRegularExpression));
                                break;
                            case "notext":
                                q = Query.And(q, Query.NotExists(field));
                                break;
                            case "ne":
                                q = Query.And(q, Query.NE(field, value));
                                break;
                            case "nin":
                                if (value.IsBsonArray) q = Query.And(q, Query.NotIn(field, value.AsBsonArray));
                                else if (!value.IsBsonNull) q = Query.And(q, Query.NE(field, value));
                                break;
                            case "not":
                                q = Query.Not(q);
                                break;
                            case "size":
                                if (value.IsInt32) q = Query.And(q, Query.Size(field, value.AsInt32));
                                break;
                            case "type":
                                if (value.IsString)
                                {
                                    BsonType t;
                                    if (Enum.TryParse<BsonType>(value.AsString, true, out t)) q = Query.And(q, Query.Type(field, t));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                return q;
            }
        }

        /// <summary>
        /// 页面url，不包含查询字符串
        /// </summary>
        public static string PageUrl { get { return HttpContext.Current.Request.FilePath; } }

        /// <summary>
        /// 包含查询字符串的页面地址
        /// </summary>
        public static string PageSearch
        {
            get
            {
                var qstr = HttpContext.Current.Request.QueryString;
                return string.Format("{0}?{1}", PageUrl, string.Join("&", qstr.AllKeys.Where(x => x.ToLowerInvariant() != "p").Select(x => string.Join("=", HttpUtility.UrlEncode(x), HttpUtility.UrlEncode(qstr[x])))));
            }
        }

        /// <summary>
        /// 包含查询的翻页地址模式字符串
        /// </summary>
        public static string PagePattern
        {
            get
            {
                string s = PageSearch;
                return string.Format("{0}&p={{0}}", s).Replace("?&", "?");
            }
        }

        /// <summary>
        /// 翻页页码
        /// </summary>
        public static int PageIndex { get { return RequestHelper.GetInt("p", 1); } }


        public static User User
        {
            get
            {
                return (HttpContext.Current.Items["user"] ?? (HttpContext.Current.Items["user"] = HttpContext.Current.User.Identity.Name.GetUser())) as User;
            }
        }

        /// <summary>
        /// 当前用户可以进行的系统操作
        /// </summary>
        public static HashSet<string> Actions
        {
            get
            {
                var actions = HttpContext.Current.Items["actions"] as HashSet<string>;
                if (actions == null)
                {
                    var roles = User.Roles;
                    if (roles == null) HttpContext.Current.Items["actions"] = actions = new HashSet<string>();
                    else HttpContext.Current.Items["actions"] = actions = new HashSet<string>(Query.And(Query.NotExists("a"), Query.In("r", new BsonArray(roles))).GetActions().Select(x => x.Label));
                    return actions;
                }
                else return actions;
            }
        }

        /// <summary>
        /// 获取当前用户在apps中的操作权限
        /// </summary>
        /// <param name="app"></param>
        /// <returns>appid --- actionlabels</returns>
        public static Dictionary<ObjectId, HashSet<string>> GetActions(params ObjectId[] apps)
        {
            if (apps != null && apps.Length > 0)
            {
                // 先读取内存中已经从数据库取出的，然后判断是否还有即将需要从数据库中取出的
                var ctx = HttpContext.Current.Items;
                var result = apps.ToDictionary(x => x, y => (ctx[y] as HashSet<string>));
                apps = result.Where(x => x.Value == null).Select(x => x.Key).ToArray();

                if (!apps.Any()) return result; // 如果全都已经加入内存，则返回结果

                // 如果还有未载入内存的应用，则：
                // 取出当前用户的所有应用角色，用于判断用户是否有某些应用动作的权限
                var appRoles = Query.And(Query.EQ("u", User.Id), Query.In("a", new BsonArray(apps)))
                    .GetAppRoles()
                    .Where(x => x.Roles != null)
                    .ToDictionary(x => x.App, x => x.Roles);


                return Query.In("a", new BsonArray(apps)).GetAppActions()
                    .Where(x => appRoles.ContainsKey(x.App) && x.Roles != null && appRoles[x.App].Any(y => x.Roles.Contains(y)))
                    .GroupBy(x => x.App)
                    .ToDictionary(
                    x => x.Key,
                    x => new HashSet<string>(x.Select(y => BasicPlatform.Web.Models.Action.GetLabel(y.Action))));

                //// 如果没有应用角色
                //if (appRoles.Count == 0) return result; // 如果不再有任何权限分配，则返回结果

                //// 如果取出了相关的应用角色，则：取出动作列表，与应用角色比对，从而判定用户可以进行哪些动作
                //// 取出该应用所有动作列表
                //var actions = Query.In("a", BsonArray.Create(apps))
                //    .GetActions()
                //    .GroupBy(x => x.App)
                //    .ToDictionary(x => x.Key);
                //// 并取出全局动作列表
                //var gactions = Query.EQ("i", true).GetActions();   // 取全局操作 *！！！将加入AppAction类，到时只取App管理动作：当前App用户的Roles可以进行的管理动作
                //var appActions = Query.In("a", BsonArray.Create(apps)).GetAppActions().GroupBy(x => x.App).ToDictionary(x => x.Key);
                //// 开始比对！

                //var partial = apps.ToDictionary(x => x,
                //    x => new HashSet<string>(
                //        (actions.ContainsKey(x) ? (actions[x].Concat(gactions)) : gactions)  // 如果没取到该应用的操作列表，就使用全局，否则使用操作列表与全局操作列表的并集
                //        .Where(y => y.Roles != null && y.Roles.Any(z => appRoles[x].Contains(z)))   // 角色比对，是否存在角色交集（即某一个roleid重叠），如果比对成功，说明用户的应用角色之一在动作限定的角色范围内
                //        .Select(z => z.Label))); // 取出动作label

                //foreach (var app in partial.Keys)
                //{
                //    ctx[app] = result[app] = partial[app];
                //}
                //return result;
            }
            else return new Dictionary<ObjectId, HashSet<string>>();

        }

        /// <summary>
        /// 页面允许的系统操作
        /// </summary>
        public static IEnumerable<HashSet<string>> PageActions
        {
            get
            {
                var s = ConfigurationManager.AppSettings["actions"];
                if (s == null) return null;
                else return s.Split(',').Select(x => new HashSet<string>(x.Split('+')));
            }
        }
    }
}
