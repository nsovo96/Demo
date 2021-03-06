﻿using BusinessLogic;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Oasis_Water.Controllers
{
   
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult TEstCss()
        {
            return View();
        }
      
     
        //add check list
        public ActionResult ManagementCheckList()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ManagementCheckListB(
            decimal _4_Softner,
           decimal _5_1_Micron_Preasure,
           decimal _6_Membrane_Preasure,
           decimal _7_Flow_Rate_RAW,
             decimal _2_PF_5_Micron,
           decimal _1_PF_10_Micron,
           decimal TDs_Main,
           decimal TDs_clean,
           decimal _3_Sand_Filter


            )
        {
            CheckList checkList = new CheckList("InsertB");
            checkList.AddcheckListB(
               _4_Softner,
            _5_1_Micron_Preasure,
            _6_Membrane_Preasure,
            _7_Flow_Rate_RAW,
              _2_PF_5_Micron,
            _1_PF_10_Micron,
            TDs_Main,
            TDs_clean,
            _3_Sand_Filter);


            return RedirectToAction("GetCheckList");
        }

        public ActionResult GetCheckList()
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();
            return View(managementCheckLists);
        }

        static int listID;
        public ActionResult EditCheckList(int ChecklistID)
        {
            listID = ChecklistID;

            return View();
        }
        [HttpPost]
        public ActionResult EditCheckList(decimal _4_Softner = 0,
           decimal _5_1_Micron_Preasure = 0,
           decimal _6_Membrane_Preasure = 0,
           decimal _7_Flow_Rate_RAW = 0,
           string _8_Flow_Rate_Clean = "",
           string _9_Pump_Functional = "",
           decimal Conductive = 0,
           decimal Clean_Water_Meter_Reading = 0,
           decimal Litres_Purified = 0,
            decimal _3_Sand_Filter = 0,
           decimal _2_PF_5_Micron = 0,
           decimal _1_PF_10_Micron = 0,
           decimal Clean_tank_level = 0,
           decimal Mun_Tank_level = 0,
           string Silcone_Crystal_Color = "",
           string Ozone = "",
           decimal TDs_Main = 0,
           decimal TDs_clean = 0)
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList();


            checkList.UpdateString
                (listID,

                _4_Softner,
            _5_1_Micron_Preasure,
            _6_Membrane_Preasure,
            _7_Flow_Rate_RAW,
            _8_Flow_Rate_Clean,
            _9_Pump_Functional,
            Conductive,
            Clean_Water_Meter_Reading,
            Litres_Purified,

              _2_PF_5_Micron,
            _1_PF_10_Micron,
            Clean_tank_level,
            Mun_Tank_level,
            Silcone_Crystal_Color,
            Ozone,
            TDs_Main,
            TDs_clean,
            _3_Sand_Filter);


            return RedirectToAction("GetCheckList");
        }


        public ActionResult Delete(int ChecklistID)
        {
            CheckList CheckLists = new CheckList("Delete");




            DialogResult dr = MessageBox.Show("Are you sure to delete row?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                CheckLists.Delete(ChecklistID);
                return RedirectToAction("GetCheckList");

            }
            else if (dr == DialogResult.No)
            {
            }


            return RedirectToAction("GetCheckList");

        }

        public ActionResult ViewReport()
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

                if (completedCount != 0 && notcompletedCount != 0)
                {
                    objdata.CompletedTaskEv = Convert.ToDecimal(completedCount) / Convert.ToDecimal((completedCount + notcompletedCount)) * 100;
                    //decimal y = Convert.ToDecimal( notCompletedCount) / Convert.ToDecimal(totalTask) ;
                 

                }
                objdata.EmployeeName = E.FullNames;
                obj.Add(objdata);
                count += 1;
            }

            Session["perfomance"] = obj;

            return View();
        }
        public ActionResult getData()
        {
            int countPink = 0;
            int countPurp = 0;
            int CountOk = 0;
            int CountNotOk = 0;
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();

            foreach (var c in managementCheckLists)
            {
                if (c.Silcone_Crystal_Color == "Pink")
                {
                    countPink += 1;

                }
                else
                {
                    countPurp += 1;

                }


                if(c.Ozone=="OK")
                {
                    CountOk += 1;
                }else
                {
                    CountNotOk += 1;
                }
            }

            ratio obj = new ratio();

            obj.pink = countPink;
            obj.purple = countPurp;
            obj.TotalOKs = CountOk;
            obj.TotalNotOks = CountNotOk;

            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TaskAvarage()
        {
            //get oasisTask
            oasisTask notifications = new oasisTask("Select");
            List<Tasks> pNotiList = new List<Tasks>();
            pNotiList = notifications.GetTasks();

            //get emploees name
            List<Users> userManager = new List<Users>();
            UserAccounts AccountList = new UserAccounts("Select");
            userManager = AccountList.Login();

            List<EverageModel> obj = new List<EverageModel>();

            int totalTask = 0;
            for (int i = 0; i < pNotiList.Count(); i++)
            {
               
                totalTask += 1;

            }
            int count = 0;
            foreach (var E in userManager)
            {
                EverageModel objdata = new EverageModel();

                int completedCount = 0;
                int notCompletedCount = 0;
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
                            notCompletedCount += 1;
                        }


                    }


               

                }
                if (completedCount != 0 && notCompletedCount != 0)
                {

                    objdata.CompletedTaskEv = Convert.ToDecimal(completedCount) / Convert.ToDecimal(totalTask) * (100);
                    //decimal y = Convert.ToDecimal( notCompletedCount) / Convert.ToDecimal(totalTask) ;
                    objdata.NotCompletedTaskEv = Convert.ToDecimal(notCompletedCount) / Convert.ToDecimal(totalTask) * (100); ;
                 
                }
                objdata.EmployeeName = E.FullNames;
                obj.Add(objdata);
                count += 1;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PeekData()
        {
            //get oasisTask
            oasisTask notifications = new oasisTask("Select");
            List<Tasks> pNotiList = new List<Tasks>();
            pNotiList = notifications.GetTasks();

            //get emploees name
            List<Users> userManager = new List<Users>();
            UserAccounts AccountList = new UserAccounts("Select");
            userManager = AccountList.Login();

            List<EverageModel> DailyPeek = new List<EverageModel>();
           
                int totalTask = 1;
               // int count = 0;
                int completedCount = 0;
                int notCompletedCount = 0;
            if(pNotiList.Count()!=0)
            {
                DateTime day = pNotiList[0].dateAssigned;
                EverageModel objdata = new EverageModel();

                for (int i = 0; i < pNotiList.Count(); i++)
                {

                    if (day.DayOfYear == pNotiList[i].dateAssigned.DayOfYear)
                    {

                        if (pNotiList[i].TaskStatus == "Completed")
                        {
                            completedCount += 1;
                        }
                        else
                        {
                            notCompletedCount += 1;
                        }

                        totalTask += 1;

                    }
                    else
                    {
                        if (completedCount != 0 && notCompletedCount != 0)
                        {
                            day = pNotiList[i].dateAssigned;
                            objdata.CompletedTaskEv = Convert.ToDecimal(completedCount) / Convert.ToDecimal(completedCount + notCompletedCount) * 100;
                            //decimal y = Convert.ToDecimal( notCompletedCount) / Convert.ToDecimal(totalTask) ;
                            objdata.NotCompletedTaskEv = Convert.ToDecimal(notCompletedCount) / Convert.ToDecimal(completedCount + notCompletedCount) * 100;


                        }

                        objdata.PeekDate = pNotiList[i].dateAssigned;
                        DailyPeek.Add(objdata);

                        objdata = new EverageModel();
                    }


                }
            }
              

     
            


            return Json(DailyPeek, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TanklevesData()
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();
            List<ratio> MunTankLevel = new List<ratio>();


            foreach(var t in managementCheckLists)
            {
                ratio objMun = new ratio();
                ratio objClean = new ratio();

                objMun.CleanTanPercentage = t.Clean_tank_level;
                objMun.munTankPercentage = t.Mun_Tank_level;
                objMun.TotalLevel = Convert.ToInt32( t.Clean_tank_level + t.Mun_Tank_level);
                objMun.RatioDate = t.date;
                MunTankLevel.Add(objMun);
            }


            return Json(MunTankLevel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PfLevels()
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();

            List<PfRatio> objPf = new List<PfRatio>();

            foreach(var t in managementCheckLists)
            {
                PfRatio obj = new PfRatio();

                obj.Pf_Micron_10 = t._1_PF_10_Micron;
                obj.Pf_Micron_5 = t._2_PF_5_Micron;
                obj.SandFiler = t._4_Softner;
                obj.Softner = t._3_Sand_Filter;
                obj.dateRatio = t.date;
                objPf.Add(obj);

            }

            return Json(objPf, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Pressurelevel()
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();

            List<PressureRatio> objPf = new List<PressureRatio>();

            foreach (var t in managementCheckLists)
            {
                PressureRatio obj = new PressureRatio();

                obj.MicronPressure = t._5_1_Micron_Preasure;
                obj.mebranePressure = t._6_Membrane_Preasure;
              
                obj.dateratio= t.date;
                objPf.Add(obj);

            }

            return Json(objPf, JsonRequestBehavior.AllowGet);

        }



        public ActionResult Pump()
        {
            List<ManagementCheckList> managementCheckLists = new List<ManagementCheckList>();
            CheckList checkList = new CheckList("Select");
            managementCheckLists = checkList.GetManagementCheckLists();

            List<PumRatio> objPf = new List<PumRatio>();

            foreach (var t in managementCheckLists)
            {
                PumRatio obj = new PumRatio();

                obj.CleanPumed= t._8_Flow_Rate_Clean;
                obj.RawWater = t._7_Flow_Rate_RAW;

                obj.dateRatio = t.date;
                objPf.Add(obj);

            }

            return Json(objPf, JsonRequestBehavior.AllowGet);

        }

        static string Sname;
        [HttpPost]
        public ActionResult CreateSurvey(string name, string question)
        {
            Sname = name;

            return View();
        }

        [HttpPost]
        public ActionResult Assigntask(string inputask,string Employeerole)
        {
            NotificationsDisplay notifications = new NotificationsDisplay();
            notifications.CreateAnotification(inputask , Employeerole);

            return RedirectToAction("ViewReport");
        }

    }



    public class ratio
    {
        public int purple { get; set; }
        public int pink { get; set; }
        public int TotalOKs { get; set; }
        public int TotalNotOks { get; set; }
 
        public decimal munTankPercentage { get; set; }
        public decimal CleanTanPercentage { get; set; }
        public DateTime RatioDate { get; set; }
        public int TotalLevel { get; set; }
    }

    public class EverageModel
    {
        public decimal NotCompletedTaskEv { get; set; }
        public decimal CompletedTaskEv { get; set; }
        public DateTime PeekDate { get; set; }
        public string EmployeeName { get; set; }
       
    }

   public class  PfRatio
    {
        public decimal SandFiler { get; set; }
        public decimal Softner { get; set; }
        public decimal Pf_Micron_10 { get; set; }
        public decimal Pf_Micron_5 { get; set; }
        public DateTime dateRatio { get; set; }
    }

    public class PressureRatio
    {
        public decimal mebranePressure { get; set; }
        public decimal MicronPressure { get; set; }

        public DateTime dateratio { get; set; }
    }


    public class PumRatio
    {
        public decimal CleanPumed { get; set; }
        public decimal RawWater { get; set; }
        public DateTime dateRatio { get; set; }
    }
}