using System;
using System.Collections.Generic;
using System.Linq;

namespace ForMongo.Repositories
{
    public class People : MongoRepository
    {
        public People()
            : base("People",
                "test",
                "mongodb://localhost")
        {

        }
    }
}
