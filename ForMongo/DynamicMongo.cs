using System;
using System.Collections.Generic;
using System.Linq;
using Oak;

namespace ForMongo
{
    public class DynamicMongo : Gemini
    {
        static DynamicMongo()
        {
            Gemini.Initialized<DynamicMongo>(d => 
            {
                new MongoModule(d);
            });
        }
    }
}
