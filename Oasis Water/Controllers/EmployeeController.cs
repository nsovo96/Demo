using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic;
namespace Oasis_Water.Controllers
{
    public class EmployeeController : Controller
    {

        public ActionResult DisplayEmployee()
        {

            List<Users> userManager = new List<Users>();


            UserAccounts AccountList = new UserAccounts("Select");

            userManager = AccountList.Login();
            return View(userManager);
        }
        // GET: Employee
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult detete()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Users value)
        {


            List<Users> products = new List<Users>();

            products.Add(value);

            UserAccounts genericProduct = new UserAccounts(value, "Insert");

            genericProduct.Register();

            return RedirectToAction("Index", "Home");
        }
    }
}