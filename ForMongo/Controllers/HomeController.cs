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
    public class HomeController : Controller
    {
        static HomeController()
        {
            Gemini.Initialized<Gemini>(d => new MongoModule(d));
        }

        People people = new People();

        public ActionResult Index()
        {
            dynamic person = new Gemini();

            person.Name = "wwwaazzzuppp";

            people.Insert(person);

            var fromDb = people.Get(person._id);

            return View();
        }
    }
}
