using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Oak;

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
            collection.Insert(o.Bson());
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
            var obj = Collection().Find(MongoDB.Driver.Builders.Query.EQ("_id", BsonValue.Create(id))).FirstOrDefault();

            if (obj == null) return null;

            return Projection(obj);
        }
    }
}