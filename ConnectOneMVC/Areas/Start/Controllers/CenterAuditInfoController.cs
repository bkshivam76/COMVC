using ConnectOneMVC.Areas.Start.Models;
using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    [CheckLogin]
    public class CenterAuditInfoController : BaseController
    {       
        public DataTable grdGradings
        {
            get { return (DataTable)GetBaseSession("grdGradings_AuditInfo"); }
            set { SetBaseSession("grdGradings_AuditInfo", value); }
        }
        public DataTable grdAuditHistory
        {
            get { return (DataTable)GetBaseSession("grdAuditHistory_AuditInfo"); }
            set { SetBaseSession("grdAuditHistory_AuditInfo", value); }
        }
        public DataTable grdAllocations
        {
            get { return (DataTable)GetBaseSession("grdAllocations_AuditInfo"); }
            set { SetBaseSession("grdAllocations_AuditInfo", value); }
        }
        public ActionResult Frm_Audit_Info()
        {
            if (BASE._Action_Items_DBOps == null)
            {
                BASE.Get_Configure_Setting();
            }
            AuditInfo model = new AuditInfo();
            DataSet dSet = BASE._CenterDBOps.Client_Audit_Info();
            if (dSet.Tables.Count > 0)
            {
                model.grdGradings = dSet.Tables[0];
                grdGradings= dSet.Tables[0]; 
            }
            if (dSet.Tables[1].Rows.Count > 0)
            {
                ViewBag.lblGradingHeader = dSet.Tables[1].Rows[0][0].ToString();
            }
            if (dSet.Tables.Count > 2)
            {
                model.grdAuditHistory = dSet.Tables[2];
                grdAuditHistory = dSet.Tables[2];
            }
            if (dSet.Tables[3].Rows.Count > 0)
            {
                ViewBag.lblAuditHeader = dSet.Tables[3].Rows[0][0].ToString();
            }
            if (dSet.Tables.Count > 4)
            {
                model.grdAllocations = dSet.Tables[4];
                grdAllocations= dSet.Tables[4];
            }
            if (dSet.Tables[5].Rows.Count > 0)
            {
                ViewBag.lblAllocationsHeader = dSet.Tables[5].Rows[0][0].ToString();
            }            
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(model);
        }
        public ActionResult GradingInfo_Grid(string command)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (command == "Refresh"|| grdGradings==null)
            {
                DataSet dSet = BASE._CenterDBOps.Client_Audit_Info();
                if (dSet.Tables.Count > 0)
                {                   
                    grdGradings = dSet.Tables[0];
                }
            }
            return View(grdGradings);
        }
        public ActionResult AuditHistory_Grid(string command)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (command == "Refresh" || grdAuditHistory == null)
            {
                DataSet dSet = BASE._CenterDBOps.Client_Audit_Info();
                if (dSet.Tables.Count > 2)
                {
                    grdAuditHistory = dSet.Tables[2];
                }
            }
            return View(grdAuditHistory);
        }
        public ActionResult AuditAllocations_Grid(string command)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (command == "Refresh" || grdAllocations == null)
            {
                DataSet dSet = BASE._CenterDBOps.Client_Audit_Info();
                if (dSet.Tables.Count > 4)
                {
                    grdAllocations = dSet.Tables[0];
                }
            }
            return View(grdAllocations);
        }
        public void SessionClear()
        {
            ClearBaseSession("_AuditInfo");
        }
        public ActionResult Frm_Export_Options(int SelectedIndex)
        {
            string view = SelectedIndex == 0 ? "Frm_Export_Options_GradingInfo" : SelectedIndex == 1? "Frm_Export_Options_AuditHistory" : "Frm_Export_Options_AuditAllocations";
            return View(view);
        }
    }
}