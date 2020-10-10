using Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            person person = new person();

            person.name = " my first data structure project";

            person.phoneNumbr = 088682;

            irepo<person> p = new generic<person>();

            p.addAddperson(person);

            string myReturn = p.DidpersonAdd();



            return View(p);

        }
    }
}