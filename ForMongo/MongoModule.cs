using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Oak;

namespace ForMongo
{
    public class MongoModule
    {
        dynamic o;

        public MongoModule(dynamic o)
        {
            this.o = o;

            Init();
        }

        void Init()
        {
            o.Bson = new DynamicFunction(Bson);
        }

        dynamic Bson()
        {
            return new BsonDocument(o.HashOfProperties());
        }
    }
}
