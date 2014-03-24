using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using BasicPlatform.Config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Web.Models
{
    public static class AppActionExtension
    {
        static MongoCollection<AppAction> c = Databases.Mongo.GetCollection<AppAction>("appaction");

        static AppActionExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("a", "ac"), IndexOptions.SetUnique(true));
        }

        public static void Save(this AppAction appAction)
        {
            if (appAction != null)
            {
                if (appAction.Roles != null && appAction.Roles.Any())
                {
                    c.Update(Query.And(Query.EQ("a", appAction.App), Query.EQ("ac", appAction.Action)), Update.SetWrapped("r", appAction.Roles), UpdateFlags.Upsert);
                }
                else
                {
                    c.Remove(Query.And(Query.EQ("a", appAction.App), Query.EQ("ac", appAction.Action)));
                }
            }
        }

        public static AppAction GetAppAction(this ObjectId id)
        {
            return c.FindOneById(id);
        }

        public static AppAction GetAppAction(this ObjectId app, ObjectId action)
        {
            return c.FindOne(Query.And(Query.EQ("a", app), Query.EQ("ac", action)));
        }

        public static void DeleteAppAction(this ObjectId id)
        {
            c.Remove(Query.EQ("_id", id));
        }

        public static void Delete(this AppAction appAction)
        {
            if (appAction != null)
            {
                appAction.Id.DeleteAppAction();
            }
        }

        public static IEnumerable<AppAction> GetAppActions(this IMongoQuery query, long pageIndex, long pageSize, out long totalRecords)
        {
            totalRecords = c.Count(query);
            return c.Find(query).SetSkip((int)((pageIndex - 1) * pageSize)).SetLimit((int)pageSize).ToList();
        }

        public static IEnumerable<AppAction> GetAppActions(this IMongoQuery query)
        {
            return c.Find(query).ToList();
        }
    }
}
