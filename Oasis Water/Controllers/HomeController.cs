using BusinessLogic;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oasis_Water.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //logout
        public ActionResult logout()
        {
            Session.Abandon();

            Session.Remove("id");
            Session.Remove("TaskID");
            Session.Remove("Fk_EmployeeID");
            Session.Remove("SenderId");
            Session.Remove("Username");
            Session.Remove("Fk_Reciever");
            Session.Remove("notificationID");


            return RedirectToAction("index", "Home");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Login(String Email, String Password)
        {
            List<Users> userManager = new List<Users>();


            UserAccounts AccountList = new UserAccounts("Select");

            userManager = AccountList.Login();



            foreach (var U in userManager)
            {

                if (U.Email == Email && U.Password == Password)
                {
                    Session["username"] = "Welcome  " + U.FullNames;
                    Session["userId"] = U.id;

                    Session["UserRole"] = U.UserRole;
                    switch (U.UserRole)
                    {
                        case "ProccessAreaEmployee":
                            Session["addtoLayout"] = "_Layout.cshtml";

                            return RedirectToAction("HomeIndex", "Home");
                        case "StorageAreaEmployee":
                            Session["addtoLayout"] = "_Layout.cshtml";

                            return RedirectToAction("HomeIndex", "Home");
                        case "FrontEndEmployee":
                            Session["addtoLayout"] = "_Layout.cshtml";

                            return RedirectToAction("HomeIndex", "Home");

                        case "Manager":
                            Session["addtoLayout"] = "_LayoutManagement.cshtml";
                            return RedirectToAction("HomeIndex", "Home");

                        case "ProccessMaintananceEmployee":
                            Session["addtoLayout"] = "_Layout.cshtml";
                            return RedirectToAction("HomeIndex", "Home");
                        case "Packeger":
                            Session["addtoLayout"] = "_Layout.cshtml";
                            return RedirectToAction("HomeIndex", "Home");
                            

                    }
                }

            }
            return View();

        }


        //home Index displalys notifications
        public ActionResult HomeIndex()
        {
            NotificationsDisplay notifications = new NotificationsDisplay("Select");

            List<Notifications> pNotiList = new List<Notifications>();


            pNotiList = notifications.dispalayNotifications();

            List<Notifications> Mynotifications = new List<Notifications>();
            
            foreach (var n in pNotiList)
            {
                if (n.RecieverRole == Session["UserRole"].ToString())
                {
                    Notifications notifications1 = new Notifications();
                    notifications1 = n;
                    Mynotifications.Add(notifications1);

                }



            }

            return View(Mynotifications);

        }


        //display detalils about a notification
        public ActionResult ViewNotification(int id)
        {
            NotificationsDisplay notifications = new NotificationsDisplay("Select");

            Session["notificationId"] = id;
            Notifications mynotificatin = notifications.GetNotificationsID(id);

            return View(mynotificatin);
        }

        //task coloboration
        

       
    }
}