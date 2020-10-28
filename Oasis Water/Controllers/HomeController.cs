using BusinessLogic;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
                    Session["Myusername"] = U.FullNames;
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


        public string getlocaltime()
        {
            TimeSpan ts = new TimeSpan(0, 0, 30);

            System.Threading.Thread.Sleep(ts);

            return " closed";
        }

        public void autoclose()
        {

            oasisTask notifications = new oasisTask("Select");

            List<Tasks> pNotiList = new List<Tasks>();

            pNotiList = notifications.GetTasks();


            Task<string> tasks = new Task<string>(getlocaltime);

            foreach(var t in pNotiList)
            {
                if(t.TaskStatus != "Completed")
                {
                    int day = t.dateAssigned.Day - DateTime.Now.Day;
                    int month = t.dateAssigned.Month - DateTime.Now.Month;
                    int year = t.dateAssigned.Year - DateTime.Now.Year;
                    int minute =(t.dateAssigned.Minute ) + DateTime.Now.Minute;
                    int hour = t.dateAssigned.Hour - DateTime.Now.Hour;

                    int y = System.Math.Abs(year);
                    int m = System.Math.Abs(month);

                    int d = System.Math.Abs(day);
                    int h = System.Math.Abs(hour);

                    int mn = System.Math.Abs(minute);

                    if (mn > 60 && h >= 1 && m==0 && y==0 && day==0)
                    {
                        oasisTask updateTask = new oasisTask();

                        updateTask.autoclose(t.id);
                    }

                }
            }


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


            autoclose();

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
        
        public ActionResult myperformance()
        {
            oasisTask notifications = new oasisTask("Select");
            List<Tasks> pNotiList = new List<Tasks>();
            pNotiList = notifications.GetTasks();
            //get emploees name
            List<Users> userManager = new List<Users>();
            UserAccounts AccountList = new UserAccounts("Select");
            userManager = AccountList.Login();
            List<EverageModel> obj = new List<EverageModel>();
            int totalTask = 1;
            int count = 0;
            foreach (var E in userManager)
            {
                EverageModel objdata = new EverageModel();
                int completedCount = 0;
                int notcompletedCount = 0;

                for (int i = 0; i < pNotiList.Count(); i++)
                {
                    if (E.id == pNotiList[i].Fk_EmployeeID)
                    {

                        if (pNotiList[i].TaskStatus == "Completed")
                        {
                            completedCount += 1;
                        }
                        else
                        {
                            notcompletedCount += 1;
                        }
                    }
                    totalTask += 1;   
                }
                if(completedCount!=0 && notcompletedCount!=0)
                {
                    objdata.CompletedTaskEv = Convert.ToDecimal(completedCount) / Convert.ToDecimal((completedCount + notcompletedCount)) * 100;
                    objdata.NotCompletedTaskEv = Convert.ToDecimal(notcompletedCount) / Convert.ToDecimal((completedCount + notcompletedCount)) * 100;
                    //decimal y = Convert.ToDecimal( notCompletedCount) / Convert.ToDecimal(totalTask) ;
                    objdata.EmployeeName = E.FullNames;
                    obj.Add(objdata);
                    count += 1;
                }
             
                
             
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenTask()
        {
           

            oasisTask notifications = new oasisTask("Select");

            List<Tasks> pNotiList = new List<Tasks>();

            pNotiList = notifications.GetTasks();

            Session.Remove("taskDetail");

            return Json(pNotiList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int taskId)
        {
            NotificationsDisplay notifications = new NotificationsDisplay();

            notifications.Delete(taskId);


            return RedirectToAction("HomeIndex");
        }
    }
}