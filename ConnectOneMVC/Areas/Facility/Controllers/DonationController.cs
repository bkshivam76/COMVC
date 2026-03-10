using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class DonationController : BaseController
    {
        public ActionResult Frm_Donation_Window() 
        {
            return View();
        }
        public ActionResult Frm_BankUpi_Window()
        {
            return View();
        }
        public ActionResult ShareQRCodeLink(string cenid,string MobileNoInput) 
        {
            string ResultString = "";
            BASE._Notifications_DBOps.SendSMS(null, "ShareBankQRCodeLink", ref ResultString,null, cenid, MobileNoInput);
            return Json(new { ResultString }, JsonRequestBehavior.AllowGet);
        }
    }
}