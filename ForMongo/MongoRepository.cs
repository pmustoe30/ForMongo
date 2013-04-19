using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Oak;
using M = MongoDB.Driver.Builders;

namespace ForMongo
{
    public class MongoRepository
    {
        string collectionName;
        string connectionString;
        string db;

        public Func<BsonDocument, dynamic> Projection { get; set; }
        
        public MongoRepository(string collectionName, 
            string db, 
            string connectionString)
        {
            this.db = db;
            this.connectionString = connectionString;
            this.collectionName = collectionName;
            Projection = d => new Gemini(d.ToDictionary());
        }

        public virtual void Insert(dynamic o)
        {
            var collection = Collection();

            var bson = o.Bson();

            collection.Insert(bson);

            o._id = (ObjectId)bson["_id"].Value;
        }

        public virtual void Update(dynamic o)
        {
            var collection = Collection();

            collection.Save(o.Bson());
        }

        public virtual IEnumerable<dynamic> Query(dynamic o)
        {
            var collection = Collection();

            dynamic gemini = new Gemini(o);

            var memberName = gemini.Members()[0];

            var memberValue = gemini.GetMember(memberName);

            var objs = collection.Find(M.Query.EQ(memberName, BsonValue.Create(memberValue))) as MongoCursor<BsonDocument>;

            return objs.Select(s => Projection(s));
        }

        private MongoCollection<BsonDocument> Collection()
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(db);
            var collection = database.GetCollection(collectionName);
            return collection;
        }

        public virtual dynamic Get(ObjectId id)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(db);
            var obj = Collection().Find(M.Query.EQ("_id", BsonValue.Create(id))).FirstOrDefault();

            if (obj == null) return null;

            return Projection(obj);
        }
    }
}