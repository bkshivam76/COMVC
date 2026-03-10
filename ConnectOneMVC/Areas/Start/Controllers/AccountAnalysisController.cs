using Common_Lib;
using ConnectOneMVC.Areas.Start.Models;
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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    public class AccountAnalysisController : BaseController
    {       
        public DataTable Catalog
        {
            get { return (DataTable)GetBaseSession("Catalog_AccountAnalysis"); }
            set { SetBaseSession("Catalog_AccountAnalysis", value); }
        }
        public DataTable MainTable
        {
            get { return (DataTable)GetBaseSession("MainTable_AccountAnalysis"); }
            set { SetBaseSession("MainTable_AccountAnalysis", value); }
        }
        public DataSet DSET
        {
            get { return (DataSet) GetBaseSession("DSET_AccountAnalysis"); }
            set { SetBaseSession("DSET_AccountAnalysis", value); }
        }
        public DataTable NestedData
        {
            get { return (DataTable)GetBaseSession("NestedData_AccountAnalysis"); }
            set { SetBaseSession("NestedData_AccountAnalysis", value); }
        }
        [CheckLogin]
        public ActionResult Frm_Audit_Dashboard()
        {
            if (BASE._Action_Items_DBOps == null)
            {
                BASE.Get_Configure_Setting();
            }
            AccountAnalysis model = new AccountAnalysis();
            DataSet dSet = (DataSet)BASE._Action_Items_DBOps.GetAuditExceptions();        
            DSET = dSet;
            model.Catalog = dSet.Tables["Catalog"];
            model.MainTable = dSet.Tables["MainTable"];
            Catalog = model.Catalog;
            MainTable = model.MainTable;
            Session["AccountAnalysisData"] = dSet;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(model);
     
        }
        public ActionResult AuditCheckpoints_Grid(string command)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (command == "REFRESH" || Catalog == null)
            {
                DataSet dSet = (DataSet)BASE._Action_Items_DBOps.GetAuditExceptions();
                DSET = dSet;
                Session["AccountAnalysisData"] = dSet;
                Catalog = dSet.Tables["Catalog"];
            }
            return View(Catalog);
        }
        public ActionResult AuditExceptions_Grid(string command)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (command == "REFRESH" || Catalog == null)
            {
                DataSet dSet = (DataSet)BASE._Action_Items_DBOps.GetAuditExceptions();          
                DSET = dSet;
                Session["AccountAnalysisData"] = dSet;
                MainTable = dSet.Tables["MainTable"];
            }
            return View(MainTable);
        }
        public ActionResult AuditExceptions_DetailGrid(int ID = 0)
        {
            ViewBag.ID = ID;
            DataTable data = new DataTable();          
            foreach (DataTable dt in DSET.Tables)
            {
                if (dt.TableName != "MainTable" && dt.TableName != "Catalog")
                {
                    if (dt.Columns.Contains("ID"))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if ((int)dt.Rows[0]["ID"] == ID)
                            {                                
                                data = dt;                          
                                break;
                            }
                        }
                    }
                }
            }
            return View(data);
        }
        public static GridViewSettings NestedGridExportSettings(int RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "AuditExceptionsDetailListGrid" + RecID;
            settings.Width = Unit.Percentage(100); 
            settings.SettingsDetail.MasterGridName = "AuditExceptions_Grid";
            return settings;
        }
        public static DataTable NestedGridExportData(int RecID)
        {
            DataSet data = (DataSet)System.Web.HttpContext.Current.Session["AccountAnalysisData"];
            DataTable detailData = new DataTable();
            foreach (DataTable dt in data.Tables)
            {
                if (dt.TableName != "MainTable" && dt.TableName != "Catalog")
                {
                    if (dt.Columns.Contains("ID"))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if ((int)dt.Rows[0]["ID"] == RecID)
                            {
                                detailData = dt;
                                break;
                            }
                        }
                    }
                }
            }
            return detailData;
        }
        public ActionResult Frm_Export_Options(int SelectedIndex)
        {
            string view = SelectedIndex == 0 ? "Frm_Export_Options_AuditExceptions" : "Frm_Export_Options_AuditCheckpoints";
            return View(view);
        }
        public void SessionClear()
        {
            Session.Remove("AccountAnalysisData");
            ClearBaseSession("_AccountAnalysis");
        }
    }
}