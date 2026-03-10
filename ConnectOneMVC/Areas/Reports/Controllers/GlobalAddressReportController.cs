using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class GlobalAddressReportController : BaseController
    {
        public DataTable GlobalAddress_ExportData
        {
            get
            {
                return (DataTable) GetBaseSession("GlobalAddress_ExportData_GlobalAddress");
            }
            set
            {
                SetBaseSession("GlobalAddress_ExportData_GlobalAddress", value);
            }

        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> GlobalAddress_DetailGridData
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>) GetBaseSession("GlobalAddress_DetailGridData_GlobalAddress");
            }
            set
            {
                SetBaseSession("GlobalAddress_DetailGridData_GlobalAddress", value);
            }
        }

        // GET: Reports/GlobalAddressReport
        public ActionResult Frm_Global_Address_Report_Info()
        {
            UserRights();
            if ((bool)ViewData["Report_AddressSearch_ListRight"]==false)
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(true);</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_AddressBook_Search).ToString()) ? 1 : 0;

            Grid_Display(null, null, null);
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(GlobalAddress_ExportData);
        }

        public PartialViewResult Frm_Global_Address_Report_Info_Grid(string SearchMode, string SearchBy, string SearchText, string command, int ShowHorizontalBar = 0, string Layout = null)
        {
            UserRights();
            //GlobalAddress_ExportData = 
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            if (command == "REFRESH")
            {
                Grid_Display(SearchMode, SearchBy, SearchText);
            }
            
            return PartialView(GlobalAddress_ExportData);
        }

        public ActionResult Frm_GlobalAddress_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, string MID = "")
        {
            ViewBag.GlobalAddressInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.GlobalAddressInfo_RecID = RecID;
            ViewBag.GlobalAddressInfo_MID = MID;        
            if (command == "REFRESH")
            {
                GlobalAddress_DetailGridData = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_AddressBook);             
                    Session["GlobalAddress_DetailGridData"] = GlobalAddress_DetailGridData;
               
            }
            return PartialView(GlobalAddress_DetailGridData);
        }

        //Nested Grid export settings related 
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GlobalAddressReportListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "GlobalAddressReportListGrid";
            return settings;
        }

        //Nested Grid Export Data Action.
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["GlobalAddress_DetailGridData"];
        }

        public void Grid_Display(string SearchMode, string SearchBy, string SearchText)
        {
            if(GlobalAddress_ExportData == null)
            {
                GlobalAddress_ExportData = new DataTable();
                //GlobalAddress_ExportData.TableName = "Address_Info";
                GlobalAddress_ExportData.Columns.Add("C_NAME", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_ADD1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_ADD2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_ADD3", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_ADD4", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_CITY", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_DISTRICT", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_STATE", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_COUNTRY", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_R_PINCODE", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_EMAIL_ID_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_EMAIL_ID_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TEL_NO_R_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TEL_NO_R_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_FAX_NO_R_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_FAX_NO_R_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TEL_NO_O_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TEL_NO_O_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_FAX_NO_O_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_FAX_NO_O_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_MOB_NO_1", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_MOB_NO_2", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_CST_TIN_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_DL_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_GST_TIN_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_PAN_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_PASSPORT_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_RATION_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_STR_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TAN_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_TAX_ID_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_UID_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_VAT_TIN_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("C_VOTER_ID_NO", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("REC_ID", Type.GetType("System.String"));
                GlobalAddress_ExportData.Columns.Add("CEN_UID", Type.GetType("System.String"));
            }
            else
            {
                //if()
                GlobalAddress_ExportData = BASE._Reports_Common_DBOps.GlobalAddressSearchList(SearchMode, SearchBy, SearchText);
                //Get data from Database using BASE function.
            }
        }

        public void SessionClear()
        {
            ClearBaseSession("_GlobalAddress");
            Session.Remove("GlobalAddress_DetailGridData");
        }

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_AddressBook_Search, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            }
            return PartialView();
        }
        public void UserRights()
        {
            ViewData["Report_AddressSearch_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_AddressBook_Search, "Export");
            ViewData["Report_AddressSearch_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_AddressBook_Search, "List");
        }
        #endregion
    }
}