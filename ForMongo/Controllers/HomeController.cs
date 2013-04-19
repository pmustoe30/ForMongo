using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oak;
using Massive;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using ForMongo.Repositories;

namespace ForMongo.Controllers
{
    public class Person : DynamicMongo 
    {
        //public dynamic Bson()
        //{
        //    if(_.RespondsTo("_id"))
        //    {
        //        return new BsonDocument(_.Select("_id", "FirstName", "LastName").HashOfProperties());    
        //    }

        //    return new BsonDocument(_.Select("FirstName", "LastName").HashOfProperties());    
        //}
    }

    public class HomeController : Controller
    {
        People people = new People();

        static HomeController()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            dynamic person = new Person();

            person.FirstName = Guid.NewGuid().ToString().Substring(0, 5);

            person.LastName = Guid.NewGuid().ToString().Substring(0, 5);

            person.Age = new Random().Next(14, 16);

            people.Insert(person);

            var query = people.Query(new { Age = 15 }).ToList();

            var result = new
            {
                inserted = people.Get(person._id),
                is15 = query
            };

            return new DynamicJsonResult(result);
        }
    }
}
