using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class TDSRegisterController : BaseController
    {
        // GET: Account/TDSRegister
        public ActionResult Frm_Tds_Register_Info()
        {
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Account_TDS_Register).ToString()) ? 1 : 0;
            return View();
        }
        public ActionResult Tds_Register_Grid_Data()
        {
            return Content(JsonConvert.SerializeObject(BASE._TDS_DBOps.GetTDSRegister()), "application/json");
        }
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Account_TDS_Register, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_export_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.Filename = "TdsRegister_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            return PartialView();
        }
    }
}