using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class TdsReportController : BaseController
    {
        // GET: Reports/TdsReport
        public ActionResult Frm_Tds_Applicability_Info()
        {
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_TDS_Applicability).ToString()) ? 1 : 0;
            return View();
        }
        public ActionResult Tds_Applicability_Grid_Data()
        {
            return Content(JsonConvert.SerializeObject(BASE._Reports_Common_DBOps.TDS_Applicability_Report()), "application/json");
        }
        public ActionResult Frm_Export_Options()
        {
            //if ((!CheckRights(BASE, ClientScreen.Report_TDS_Applicability, "Export")))
            //{
            //    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_export_modal','Not Allowed','No Rights');</script>");
            //}
            ViewBag.Filename = "TdsApplicabilityReport_" + BASE._open_Ins_Short + "_" + BASE._open_Year_ID.ToString();
            return PartialView();
        }
    }
}