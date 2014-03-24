using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Net;
using System.Web;

namespace BasicPlatform.Web.Models
{
    public static class ConnectionExtension
    {
        static MongoCollection<Connection> c = Databases.Mongo.GetCollection<Connection>("connections");

        static ConnectionExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("u", "a"), IndexOptions.SetUnique(true).SetBackground(true));
            c.EnsureIndex(IndexKeys.Ascending("n", "a"), IndexOptions.SetUnique(true).SetBackground(true));
        }

        /// <summary>
        /// 保存连接信息Upsert，如果已经存在，则更新alias；如果不存在则插入
        /// </summary>
        /// <param name="connection"></param>
        public static void Save(this Connection connection)
        {
            if (connection != null)
            {
                var c1 = GetConnection(connection.Alias, connection.App);
                if (c1 != null && c1.User != connection.User)
                    c1.Delete();
                c.Update(
                    Query.And(Query.EQ("u", connection.User), Query.EQ("a", connection.App)),
                    Update.Set("n", connection.Alias.ToLowerInvariant()),
                    UpdateFlags.Upsert);
            }
        }

        /// <summary>
        /// 删除连接
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteConnection(this ObjectId id)
        {
            c.Remove(Query.EQ("_id", id));
        }

        /// <summary>
        /// 删除连接
        /// </summary>
        /// <param name="connection"></param>
        public static void Delete(this Connection connection)
        {
            if (connection != null) connection.Id.DeleteConnection();
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="id"></param>
        public static Connection GetConnection(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static Connection GetConnection(this ObjectId user, ObjectId app)
        {
            return c.FindOne(Query.And(Query.EQ("u", user), Query.EQ("a", app)));
        }

        /// <summary>
        /// 根据app帐号获取一个连接
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static Connection GetConnection(this string alias, ObjectId app)
        {
            return c.FindOne(Query.And(Query.EQ("n", alias), Query.EQ("a", app)));
        }

        /// <summary>
        /// 搜索连接
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static IEnumerable<Connection> GetConnections(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {
            totalRecords = c.Count(query);
            return c.Find(query).SetSortOrder("n").SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();
        }

        /// <summary>
        /// 搜索连接
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<Connection> GetConnections(this IMongoQuery query)
        {
            return c.Find(query).ToList();
        }
    }
}
