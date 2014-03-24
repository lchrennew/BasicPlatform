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
    public static class AccessTokenExtension
    {
        static MongoCollection<AccessToken> c = Databases.Mongo.GetCollection<AccessToken>("tokens");

        static AccessTokenExtension()
        {
            c.EnsureIndex(IndexKeys.Ascending("s"), IndexOptions.SetTimeToLive(TimeSpan.FromHours(1)).SetSparse(true));
            c.EnsureIndex("s");
            c.EnsureIndex(IndexKeys.Ascending("u", "a"), IndexOptions.SetUnique(true));
        }

        public static void Save(this AccessToken token)
        {
            if (token != null)
            {
                token.Expiration = DateTime.Now.AddHours(1);
                c.Update(Query.And(Query.EQ("u", token.User), Query.EQ("a", token.App)), Update.Set("s", token.Token).Set("e", token.Expiration), UpdateFlags.Upsert);
            }
        }

        public static AccessToken GetAccessToken(this string token)
        {
            return c.FindOne(Query.EQ("s", token));
        }

        public static AccessToken GetAccessToken(this ObjectId user, ObjectId app)
        {
            return c.FindOne(Query.And(Query.EQ("u", user), Query.EQ("a", app)));
        }

        public static void Delete(this AccessToken token)
        {
            if (token != null)
            {
                c.Remove(Query.EQ("_id", token.Id), RemoveFlags.Single);
            }
        }
    }
}
