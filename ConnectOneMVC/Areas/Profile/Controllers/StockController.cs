using Common_Lib;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class StockController : BaseController
    {
        // GET: Profile/Stock
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Stock_Info()
        {
            ViewBag.ShowHorizontalBar = 0;
            List<Common_Lib.DbOperations.StockProfile.Return_GetProfiledata> Stockdata = BASE._Stock_Profile_DBOps.GetProfiledata();
            Session["StockprofileData"] = Stockdata;
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((Stockdata.Count == 0))
            {
                return View();
            }
            return View(Stockdata);
        }

        public PartialViewResult Frm_Stock_Info_Grid(string command = null, int ShowHorizontalBar = 0)
        {
                             
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Session["StockprofileData"] == null || command == "REFRESH")
            {
                var Stockdata = BASE._Stock_Profile_DBOps.GetProfiledata();
                Session["StockprofileData"] = Stockdata;
            }
            var _data = Session["StockprofileData"] as List<Common_Lib.DbOperations.StockProfile.Return_GetProfiledata>;
           // return PartialView("Frm_Stock_Info_Grid", Session["StockprofileData"]);
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }

        public ActionResult Frm_Stock_Window()
        {
            return View();
        }

        public ActionResult Frm_Stock_Change_Location()
        {
            return View();
        }
        public ActionResult Frm_Stock_Change_Project()
        {
            return View();
        }
     
    }
}