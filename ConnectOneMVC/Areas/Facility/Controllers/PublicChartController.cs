using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class PublicChartController : BaseController
    {
        public DataTable dt_centerwiseEventConfmList
        {
            get { return (DataTable)GetBaseSession("dt_centerwiseEventConfmList_PublicChart"); }
            set { SetBaseSession("dt_centerwiseEventConfmList_PublicChart", value); }
        }
        public DataTable dt_centerwiseEventConfmListSummary
        {
            get { return (DataTable)GetBaseSession("dt_centerwiseEventConfmListSummary_PublicChart"); }
            set { SetBaseSession("dt_centerwiseEventConfmListSummary_PublicChart", value); }
        }

        public ActionResult centerWiseEventConfirmationInformation_Info(string Chart_Instance_ID, int cenid)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = Chart_Instance_ID;
            
            //ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            DataSet _DS = BASE._Form_dbops.get_chartCenterwiseConfirmationList(Convert.ToInt32(Chart_Instance_ID), cenid);
            dt_centerwiseEventConfmList = _DS.Tables[0];
            dt_centerwiseEventConfmListSummary = _DS.Tables[1];
           
            ViewBag.NBK_Brother = dt_centerwiseEventConfmListSummary.Rows[0]["NBK_Brother"].ToString();
            ViewBag.Sur_Brother = dt_centerwiseEventConfmListSummary.Rows[0]["Sur_Brother"].ToString(); ;
            ViewBag.Total_Brother = dt_centerwiseEventConfmListSummary.Rows[0]["Total_Brother"].ToString(); 

            ViewBag.NBK_Sister = dt_centerwiseEventConfmListSummary.Rows[0]["NBK_Sister"].ToString();
            ViewBag.Sur_Sister = dt_centerwiseEventConfmListSummary.Rows[0]["Sur_Sister"].ToString();
            ViewBag.Total_Sister = dt_centerwiseEventConfmListSummary.Rows[0]["Total_Sister"].ToString();

            ViewBag.NBK_Total = dt_centerwiseEventConfmListSummary.Rows[0]["NBK_Total"].ToString();
            ViewBag.Sur_Total = dt_centerwiseEventConfmListSummary.Rows[0]["Sur_Total"].ToString();
            ViewBag.Total_Total = dt_centerwiseEventConfmListSummary.Rows[0]["Total_Total"].ToString();

            ViewBag.CentreName = _DS.Tables[2].Rows[0]["Centre"].ToString();
            ViewBag.EventName = _DS.Tables[2].Rows[0]["EventHeading"].ToString();
            ViewBag.Period = _DS.Tables[2].Rows[0]["Period"].ToString();

            return View("centerWiseEventConfirmationInformation", dt_centerwiseEventConfmList);
        }
        public ActionResult centerWiseEventConfirmationInformation_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "", string cenid = "0",
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "")
        {
            if (command == "REFRESH" || dt_centerwiseEventConfmList == null)
            {
                dt_centerwiseEventConfmList = BASE._Form_dbops.get_chartCenterwiseConfirmationList(Convert.ToInt32(Chart_Instance_ID), Convert.ToInt32(cenid)).Tables[0];
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            //ViewBag.summary_type = summary_type;
            ViewBag.ChartInstanceID = Chart_Instance_ID;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            return View(dt_centerwiseEventConfmList);
        }

        
    }
}