using Common_Lib;
using Common_Lib.RealTimeService;
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
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class LiabilitiesController : BaseController
    {

        // GET: Profile/Liabilities
        #region Global Variables        
        public List<Models.LiabilitiesInfo> Liability_ExportData
        {
            get
            {
                return (List<Models.LiabilitiesInfo>)GetBaseSession("Liability_ExportData_Liabilities");
            }
            set
            {
                SetBaseSession("Liability_ExportData_Liabilities", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> LiabilitiesInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("LiabilitiesInfo_DetailGrid_Data_Liabilities");
            }
            set
            {
                SetBaseSession("LiabilitiesInfo_DetailGrid_Data_Liabilities", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> LiabilitiesInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("LiabilitiesInfo_AdditionalInfoGrid_Liabilities");
            }
            set
            {
                SetBaseSession("LiabilitiesInfo_AdditionalInfoGrid_Liabilities", value);
            }
        }
        #endregion


        public ActionResult Frm_Liabilities_Info()
        {
            Liabilities_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Liabilities, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Liabilities').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Liabilities).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetLiabProfileListing LiabProfile = new Common_Lib.RealTimeService.Param_GetLiabProfileListing();
            LiabProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            LiabProfile.Next_YearID = BASE._next_Unaudited_YearID;
            DataTable LI_Table = BASE._LiabilityDBOps.GetProfileListing(LiabProfile);
            ViewData["Liabilities_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            // Dim LI_Table As DataTable = Base._LiabilityDBOps.GetList(Voucher_Entry, Profile_Entry)
            if ((LI_Table == null))
            {
                return View();
            }
            else
            {
                var liabilititesdata = DatatableToModel.DataTabletoLiabilities_INFO(LI_Table);
                Liability_ExportData = liabilititesdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(liabilititesdata);
            }
        }

        public ActionResult Frm_Liabilities_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Liabilities_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Liability_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetLiabProfileListing LiabProfile = new Common_Lib.RealTimeService.Param_GetLiabProfileListing();
                LiabProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                LiabProfile.Next_YearID = BASE._next_Unaudited_YearID;
                DataTable LI_Table = BASE._LiabilityDBOps.GetProfileListing(LiabProfile);
                //  Base._VehicleDBOps.GetList(Voucher_Entry, Profile_Entry)
                if ((LI_Table == null))
                {
                    return PartialView();
                }
                else
                {
                    var liabilitiesdata = DatatableToModel.DataTabletoLiabilities_INFO(LI_Table);
                    Liability_ExportData = liabilitiesdata;
                }
            }
            return PartialView("Frm_Liabilities_Info_Grid", Liability_ExportData);
        }
        #region <--NestedGrid-->
        public ActionResult Frm_Liabilities_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.LiabilitiesInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.LiabilitiesInfo_RecID = RecID;
            ViewBag.LiabilitiesInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    LiabilitiesInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Liabilities);
                    Session["LiabilitiesInfo_detailGrid_Data"] = LiabilitiesInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Liabilities);
                    LiabilitiesInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["LiabilitiesInfo_detailGrid_Data"] = data.DocumentMapping;
                    LiabilitiesInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(LiabilitiesInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(LiabilitiesInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "LiabilitiesListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "LiabilitiesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["LiabilitiesInfo_detailGrid_Data"];
        }
        #endregion // <--NestedGrid-->
        public ActionResult Frm_Liabilities_Window(string ActionMethod = null)
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_Liabilities, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Liabilities_Window','Not Allowed','No Rights');</script>");
                }
            }
            return View();
        }

        #region Create detail

        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
            string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
                string Pic_Status = string.Empty;
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;
                string Lbl_Status_Color = string.Empty;
                Status = Action_Status;
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Lbl_Status_Color = "blue";
                    Pic_Status = "Fa Fa-Lock";
                }
                else
                {
                    Pic_Status = "Fa Fa-UnLock";
                    Lbl_Status = Status;
                    if (Status.ToUpper().Trim().ToString() == "COMPLETED")
                        Lbl_Status_Color = "green";
                    else
                        Lbl_Status_Color = "red";
                }
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Locked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Locked On: " + "?";
                    }
                    Lbl_StatusBy = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                else
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Unlocked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Unlocked On: " + "?";
                    }
                    Lbl_StatusBy = "Unlocked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Lbl_Status_Color,
                    Lbl_StatusBy = Lbl_StatusBy,
                    Lbl_StatusOn = Lbl_StatusOn
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = "",
                    Lbl_StatusBy = "",
                    Lbl_StatusOn = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        #endregion

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Liabilities, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Liabilities_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_Liabilities");
            Session.Remove("LiabilitiesInfo_detailGrid_Data");
        }
        public void Liabilities_user_rights()
        {
            ViewData["Liabilities_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Liabilities, "Add");
            ViewData["Liabilities_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Liabilities, "Update");
            ViewData["Liabilities_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Liabilities, "View");
            ViewData["Liabilities_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Liabilities, "Delete");
            ViewData["Liabilities_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Liabilities, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }

        //*******************************#RegionDevextreme***************************************************************  
        #region devextreme

        public ActionResult Frm_Liabilities_Info_dx()
        {
            try
            {
                Liabilities_user_rights();

                if (!CheckRights(BASE, ClientScreen.Profile_Liabilities, "List"))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Liabilities').hide();</script>");
                }

                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Liabilities).ToString()) ? 1 : 0;
                ViewBag.UserType = BASE._open_User_Type;

                ViewData["Liabilities_Auto_Vouching_Mode"] = BASE._IsUnderAudit &&
                    (BASE._open_User_Type.Equals(Common_Lib.Common.ClientUserType.SuperUser, StringComparison.OrdinalIgnoreCase) ||
                    BASE._open_User_Type.Equals(Common_Lib.Common.ClientUserType.Auditor, StringComparison.OrdinalIgnoreCase));

                return View();
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }


        public ActionResult Frm_Liabilities_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetLiabProfileListing LiabProfile = new Common_Lib.RealTimeService.Param_GetLiabProfileListing
            {
                Prev_YearId = BASE._prev_Unaudited_YearID,
                Next_YearID = BASE._next_Unaudited_YearID
            };
            DataTable LI_Table = BASE._LiabilityDBOps.GetProfileListing(LiabProfile);
            var liabilitiesdata = DatatableToModel.DataTabletoLiabilities_INFO(LI_Table);
            Liability_ExportData = liabilitiesdata;
            return Content(JsonConvert.SerializeObject(Liability_ExportData), "application/json");
        }

        public ActionResult Frm_Liabilities_Info_DetailGrid_dx(bool VouchingMode = false, string RecID = "")
        {
            var result = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_Liabilities, !VouchingMode);
            string jsonResult = JsonConvert.SerializeObject(result);

            return Content(jsonResult, "application/json");
        }

        public ActionResult AdditionalInfo_Grid_dx(bool VouchingMode = false, string RecID = "")
        {

            var result = BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_Liabilities);
            string jsonResult = JsonConvert.SerializeObject(result);

            return Content(jsonResult, "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Liabilities, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Liabilities_report_modal','Not Allowed','No Rights');$('#OnLiabilitiesListPreviewClick').hide();</script>");
            }
            return PartialView();
        }

        #endregion

    }

}