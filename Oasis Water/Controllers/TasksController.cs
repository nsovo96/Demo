using BusinessLogic;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oasis_Water.Controllers
{
    public class TasksController : Controller
    {
        // tasks option



        public ActionResult TaskOptions()
        {
            return View();
        }


        //Send Task Type
        public ActionResult SendTaskType()
        {
            return RedirectToAction("NewTask", new { tasktype = "DailyTask" });
            }


        //fill in daTA FOR  a new  task
        public ActionResult Filltask()
        {



            return View();

        }
        [HttpPost]
        public ActionResult Filltask(string Taksdetail)
        {


            Task MynewTask = new Task("Insert_no_model");
            NotificationsDisplay notifications = new NotificationsDisplay();
            MynewTask.NewTask(Convert.ToInt32(Session["userId"]), "assgined", Taksdetail);


            if (Session["userRole"].ToString()== "ProccessMaintananceEmployee")
            {
                notifications.CreateAnotification("A task with detail : " + Taksdetail + " has been established to fix checklist readings", "Manager");

            }else
            {
                notifications.CreateAnotification("A task with detail : " + Taksdetail + " is fulfilled", "Manager");

            }

            return RedirectToAction("GetTask");

        }
        //post new task detail//this are pre difined tasks
        [HttpPost]
        public ActionResult OasisTask(string NewTask)
        {


            Task MynewTask = new Task("Insert_no_model");

            MynewTask.NewTask(Convert.ToInt32(Session["userId"]), "oasisTask", NewTask);

            return RedirectToAction("GetTask");
        }

        //task view//detrtimines a type of a task to do
        public ActionResult NewTask(string tasktype)
        {
            Session.Remove("tasktype");
            Session["tasktype"] = tasktype;
            return View();
        }

        //Get All tasks

        public ActionResult GetTask()
        {
            Task notifications = new Task("Select");

            List<Tasks> pNotiList = new List<Tasks>();

            pNotiList = notifications.GetTasks();

            Session.Remove("taskDetail");

            return View(pNotiList);


        }

        //task update
        static int EditTaskID;
        public ActionResult UpdateTask(int TaskID)
        {
            EditTaskID = TaskID;
            Session.Remove("EditaskID");

            if (Session["UserRole"].ToString() == "ProccessMaintananceEmployee")
            {
                return RedirectToAction("AddNotification", new { TaskID = TaskID });

            }
            if (Session["UserRole"].ToString() == "Packeger")
            {
                return RedirectToAction("AddNotification", new { TaskID = TaskID });

            }
            else
            {
                Task updateTask = new Task("Update");

                updateTask.Update(TaskID);


                return RedirectToAction("GetTask");

            }


        }

        //update task and notify management
        public ActionResult AddNotification(int TaskID)
        {

            Task Mytask = new Task("Select");

            Session.Remove("taskID");


            Tasks t = new Tasks();


            List<Tasks> TasksValues = Mytask.GetTasks();


            foreach (var myt in TasksValues)
            {
                if (myt.id == TaskID)
                {
                    t = myt;
                }
            }
            Session["taskID"] = TaskID;


            return View(t);
        }


        [HttpPost]
        public ActionResult AddNotification(string Notification)
        {
            NotificationsDisplay notifications = new NotificationsDisplay();
            notifications.CreateAnotification("Task fulfiled, Task detail read as follow: " + Notification, "Manager");
            return RedirectToAction("HomeIndex", "Home");
        }


     


        //task collaboration
        public ActionResult TaskColobaration()
        {
            Task notifications = new Task("Select");

            List<Tasks> pNotiList = new List<Tasks>();


            pNotiList = notifications.GetTasks();


            List<Users> userManager = new List<Users>();


            UserAccounts AccountList = new UserAccounts("Select");

            Session["userdetails"] = AccountList.Login();

            return View(pNotiList);

        }
        static int Taskid;
        static List<Comments> pNotiList = new List<Comments>();

        public ActionResult Colaboration(int TaskId)
        {
            Taskid = TaskId;
            Session.Remove("taskID");
            NotificationsDisplay notifications = new NotificationsDisplay();


            Coloboration coloborations = new Coloboration("Select");

            pNotiList = coloborations.GetComments(TaskId);
            Session["taskID"] = TaskId;

            return View(pNotiList);
        }

        [HttpPost]
        public ActionResult MyColaboration(string Comment)
        {

            Coloboration coloborations = new Coloboration("Insert");

            coloborations.SendCollab(Convert.ToInt32(Session["userId"]), Taskid, Comment, Session["taskDetail"].ToString());
              
             NotificationsDisplay notifications = new NotificationsDisplay();
            notifications.CreateAnotification("A collaboration on a task with details of : ' " + Session["taskDetail"].ToString() + " ' has occured ", "Manager");
            notifications.CreateAnotification("A collaboration on a task with details of : '" + Session["taskDetail"].ToString() + " ' has occured ", "ProccessAreaEmployee");
            notifications.CreateAnotification("A collaboration on a task with details of : ' " + Session["taskDetail"].ToString() + " ' has occured ", "StorageAreaEmployee");
            notifications.CreateAnotification("A collaboration on a task with details of ' " + Session["taskDetail"].ToString() + " ' has occured ", "FrontEndEmployee");
            notifications.CreateAnotification("A collaboration on a task with details of ' " + Session["taskDetail"].ToString() + " ' has occured ", "ProccessMaintananceEmployee");

            Session.Remove("taskDetail");

            return RedirectToAction("Colaboration", new { Taskid = Taskid }); ;
        }

       
    }
}