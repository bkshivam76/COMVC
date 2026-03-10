using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib.RealTimeService;
using static Common_Lib.DbOperations.Report_Ledgers;
using DevExtreme.AspNet.Mvc;
using System.Data;
using ConnectOneMVC.Helper;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Areas.Magazine.Models;

namespace ConnectOneMVC.Areas.Reports.Controllers
{

    [CheckLogin]
    public class MagSubDispatchReportController : BaseController
    {
        public MagSubDispatchReport MagSubDispatchData
        {
            get { return (MagSubDispatchReport)GetBaseSession("MagSubDispatchData_MagSubDispatchRpt"); }

            set {SetBaseSession("MagSubDispatchData_MagSubDispatchRpt", value); }
        }
        public List<MagazineList_LookUp> MagazineList
        {
            get { return (List<MagazineList_LookUp>)GetBaseSession("MagazineList_MagSubDispatchRpt"); }

            set { SetBaseSession("MagazineList_MagSubDispatchRpt", value); }
        }
        public ActionResult Frm_MagSubDispatch_Report()
        {
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["MagSubDispatchReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["MagSubDispatchReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_MagSubDispatch, "Export");
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.FinBegindate = BASE._open_Year_Sdt;
            ViewBag.FinTodate = BASE._open_Year_Edt;
            MagSubDispatchReport model = new MagSubDispatchReport();
            model.Grid1 = new List<MagSubDispatchReport_Grid1>();
            model.Grid2 = new List<MagSubDispatchReport_Grid2>();
            MagSubDispatchData = model;
            return View(MagSubDispatchData);
        }
        public ActionResult Frm_MagSubDispatch_Report_Grid(string command,string MagID,string FromDate,string ToDate, int ShowHorizontalBar = 0, bool VouchingMode = false, string ViewMode = "Default",string Layout=null)
        {
            ViewData["MagSubDispatchReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_MagSubDispatch, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.FinBegindate = BASE._open_Year_Sdt;
            ViewBag.FinTodate = BASE._open_Year_Edt;
            if (command == "REFRESH" || MagSubDispatchData == null &&(FromDate!="Invalid date"&&ToDate!="Invalid date"))
            {
                Param_MagSubDispatchReport inparam = new Param_MagSubDispatchReport();
                inparam.FromDate = Convert.ToDateTime(FromDate);
                inparam.ToDate = Convert.ToDateTime(ToDate);
                inparam.MagID = MagID;
                inparam.YearID = BASE._open_Year_ID;
                MagSubDispatchData = BASE._Reports_Ledgers_DBOps.GetMagSubDispatchReport(inparam);
            }
            return View(MagSubDispatchData.Grid1);
        }
        public ActionResult Frm_MagSubDispatch_Report_Grid2(string command, string MagID, string FromDate, string ToDate, int ShowHorizontalBar = 0, bool VouchingMode = false, string ViewMode = "Default", string Layout = null)
        {
            ViewData["MagSubDispatchReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_MagSubDispatch, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.FinBegindate = BASE._open_Year_Sdt;
            ViewBag.FinTodate = BASE._open_Year_Edt;
            if (command == "REFRESH" || MagSubDispatchData == null && (FromDate != "Invalid date" && ToDate != "Invalid date"))
            {
                Param_MagSubDispatchReport inparam = new Param_MagSubDispatchReport();
                inparam.FromDate = Convert.ToDateTime(FromDate);
                inparam.ToDate = Convert.ToDateTime(ToDate);
                inparam.MagID = MagID;
                inparam.YearID = BASE._open_Year_ID;
                MagSubDispatchData = BASE._Reports_Ledgers_DBOps.GetMagSubDispatchReport(inparam);
            }
            return View(MagSubDispatchData.Grid2);
        }
        public ActionResult Frm_Export_Options(int SelectedIndex)
        {
            if (SelectedIndex == 0)
            {
                return View();
            }
            else
            {
                return View("Frm_Export_Options2");
            }
        }
        public ActionResult LookUp_GetMagazineList(DataSourceLoadOptions loadOptions)
        {
            if (MagazineList == null)
            {
                DataTable d1 = BASE._Magazine_DBOps.GetList("", "", "") as DataTable;
                DataView d1View = new DataView(d1);
                d1View.Sort = "Name";
                MagazineList = DatatableToModel.GetMagazineLis_Info(d1View.ToTable());
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MagazineList, loadOptions)), "application/json");
        }
        [HttpPost]
        public void SessionClear()
        {
            ClearBaseSession("_MagSubDispatchRpt");
        }
    }
}