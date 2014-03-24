using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Dynamic;

namespace BasicPlatform.Web.Models
{
    public static class ActionExtension
    {
        static MongoCollection<Action> c = Databases.Mongo.GetCollection<Action>("action");

        static ActionExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("l", "a"), IndexOptions.SetUnique(true).SetBackground(true).SetName("ix_l_a"));
            if (c.FindOne() == null)
            {
                c.InsertBatch(new List<Action> { 

                    new Action { Label = "ViewRolesOfApps", Name = "View Roles Of Apps", Individual = true }
                  , new Action { Label = "CreateRolesOfApps", Name = "Create Roles Of Apps", Individual = true } 
                  , new Action { Label = "EditRolesOfApps", Name = "Edit Roles Of Apps", Individual = true } 
                  , new Action { Label = "AppendRolesOfApps", Name = "Append Roles Of Apps", Individual = true } 
                  , new Action { Label = "RemoveRolesOfApps", Name = "Remove Roles Of Apps", Individual = true } 
                  
                  , new Action { Label = "ViewUsersOfApps", Name = "View Users Of Apps", Individual = true } 
                  , new Action { Label = "CreateUsersOfApps", Name = "Create Users Of Apps", Individual = true } 
                  , new Action { Label = "AppendUsersOfApps", Name = "Append Users Of Apps", Individual = true } 
                  , new Action { Label = "EditUsersOfApps", Name = "Edit Users Of Apps", Individual = true } 
                  , new Action { Label = "RemoveUsersOfApps", Name = "Remove Users Of Apps", Individual = true } 
                  , new Action { Label = "ConnectUsersOfApps", Name = "Connect Users Of Apps", Individual = true } 
                  , new Action { Label = "DisconnectUsersOfApps", Name = "Dicsonnect Users Of Apps", Individual = true } 
                  
                  , new Action { Label = "ViewActionsOfApps", Name = "View Actions Of Apps", Individual = true } 
                  , new Action { Label = "CreateActionsOfApps", Name = "Create Actions Of Apps", Individual = true } 
                  , new Action { Label = "EditActionsOfApps", Name = "Edit Actions Of Apps", Individual = true } 
                  , new Action { Label = "RemoveActionsOfApps", Name = "Remove Actions Of Apps", Individual = true } 

                  , new Action { Label = "ViewApps", Name = "View Apps" } 
                  , new Action { Label = "AddApps", Name = "Add Apps" } 
                  , new Action { Label = "EditApps", Name = "Edit Apps" } 
                  , new Action { Label = "DeleteApps", Name = "Delete Apps" } 

                  , new Action { Label = "ViewActions", Name = "Vew Actions" } 
                  //, new Action { Label = "AddActions", Name = "Add Actions" } 
                  , new Action { Label = "EditActions", Name = "Edit Actions" } 
                  //, new Action { Label = "DeleteActions", Name = "Delete Actions" } 

                  , new Action { Label = "ViewUsers", Name = "View Users" } 
                  , new Action { Label = "AddUsers", Name = "Add Users" } 
                  , new Action { Label = "EditUsers", Name = "Edit Users" } 
                  , new Action { Label = "DeleteUsers", Name = "Delete Users" } 

                  , new Action { Label = "ViewRoles", Name = "View Roles" } 
                  , new Action { Label = "AddRoles", Name = "Add Roles" } 
                  , new Action { Label = "EditRoles", Name = "Edit Roles" } 
                  , new Action { Label = "DeleteRoles", Name = "Delete Roles" } 
                    
                });

            }
            if (c.FindOne(Query.EQ("l", "EditRolesOfApps")) == null)
                new Action { Label = "EditRolesOfApps", Name = "Edit Roles Of Apps", Individual = true }.Save();
            if (c.FindOne(Query.EQ("l", "ConnectUsersOfApps")) == null)
                new Action { Label = "ConnectUsersOfApps", Name = "Connect Users Of Apps", Individual = true }.Save();
            if (c.FindOne(Query.EQ("l", "DisconnectUsersOfApps")) == null)
                new Action { Label = "DisconnectUsersOfApps", Name = "Dicsonnect Users Of Apps", Individual = true }.Save();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="action"></param>
        public static void Save(this Action action)
        {
            if (action != null) c.Save(action);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteAction(this ObjectId id)
        {
            c.Remove(Query.And(Query.EQ("_id", id), Query.Exists("a")));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="action"></param>
        public static void Delete(this Action action)
        {
            if (action != null) action.Id.DeleteAction();
        }

        /// <summary>
        /// 获取操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Action GetAction(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 获取操作
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Action GetAction(this string label)
        {
            return c.FindOne(Query.And(Query.EQ("l", label), Query.NotExists("a")));
        }

        /// <summary>
        /// 获取App的Action
        /// </summary>
        /// <param name="label"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static Action GetAction(this string label, ObjectId app)
        {
            return c.FindOne(Query.And(Query.EQ("l", label), Query.EQ("a", app)));
        }

        /// <summary>
        /// 搜索操作
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<Action> GetActions(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {

            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("l").SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();
        }

        /// <summary>
        /// 搜索操作
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<Action> GetActions(this IMongoQuery query)
        {
            return c.Find(query);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(Action action, params ObjectId[] roles)
        {
            action.SetRoles(roles as IEnumerable<ObjectId>);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        public static void SetRoles(this Action action, IEnumerable<ObjectId> roles)
        {
            if (action != null)
            {
                if (roles != null && roles.Count() > 0)
                    action.Roles = new HashSet<ObjectId>(roles.Distinct());
                else action.Roles = null;
            }
        }
    }
}
