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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class DepositsController : BaseController
    {
        // GET: Profile/Deposits
        #region Global Variables        
        public List<DepositsInfo> Deposits_ExportData
        {
            get
            {
                return (List<DepositsInfo>)GetBaseSession("Deposits_ExportData_Deposits");
            }
            set
            {
                SetBaseSession("Deposits_ExportData_Deposits", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> DepositsInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("DepositsInfo_DetailGrid_Data_Deposits");
            }
            set
            {
                SetBaseSession("DepositsInfo_DetailGrid_Data_Deposits", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> DepositsInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("DepositsInfo_AdditionalInfoGrid_Deposits");
            }
            set
            {
                SetBaseSession("DepositsInfo_AdditionalInfoGrid_Deposits", value);
            }
        }
        #endregion
        public ActionResult Frm_Deposits_Info()
        {
            Deposits_user_rights();
            if (!CheckRights(BASE,ClientScreen.Profile_Deposit, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Deposit').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Deposit).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetDepositProfileListing DepProfile = new Common_Lib.RealTimeService.Param_GetDepositProfileListing();
            DepProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            DepProfile.Next_YearID = BASE._next_Unaudited_YearID;
            DataTable DI_Table = BASE._DepositsDBOps.GetProfileListing(DepProfile);
            ViewData["Deposits_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((DI_Table == null))
            {
                return View();
            }
            else
            {
                var depositsdata = DatatableToModel.DataTabletoDeposits_INFO(DI_Table);
                Deposits_ExportData = depositsdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No ;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString() ;
                return View(depositsdata);
            }
        }

        public PartialViewResult Frm_Deposits_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Deposits_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Deposits_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetDepositProfileListing DepProfile = new Common_Lib.RealTimeService.Param_GetDepositProfileListing();
                DepProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                DepProfile.Next_YearID = BASE._next_Unaudited_YearID;
                DataTable DI_Table = BASE._DepositsDBOps.GetProfileListing(DepProfile);
                var depositsdata = DatatableToModel.DataTabletoDeposits_INFO(DI_Table);
                Deposits_ExportData = depositsdata;
            }
            return PartialView("Frm_Deposits_Info_Grid", Deposits_ExportData);
        }

        //public ActionResult DepositsCustomDataAction(string key)
        //{
        //    var Final_Data = Deposits_ExportData as List<DepositsInfo>;
        //    var it = (DepositsInfo)Final_Data.Where(f => f.ID == key).FirstOrDefault();
        //    string itstr = "";
        //    if (it != null)
        //    {
        //        itstr = it.ID + "," + it.tP_NOField + "," + it.telMiscIdField + "," + it.categoryField + "," +
        //                    it.typeField + "," + it.other_DetField + "," + it.EditDate + "," +
        //                    it.AddDate + "," + it.AddBy + "," + it.EditBy + "," + it.ActionStatus;
        //    }
        //    return GridViewExtension.GetCustomDataCallbackResult(itstr);
        //}
        #region <--NestedGrid-->
        public ActionResult Frm_Deposits_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.DepositsInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.DepositsInfo_RecID = RecID;
            ViewBag.DepositsInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    DepositsInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Deposit);                    
                    Session["DepositsInfo_detailGrid_Data"] = DepositsInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Deposit);
                    DepositsInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["DepositsInfo_detailGrid_Data"] = data.DocumentMapping;
                    DepositsInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(DepositsInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(DepositsInfo_AdditionalInfoGrid);
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
            settings.Name = "DepositsListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "DepositsListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["DepositsInfo_detailGrid_Data"];
        }
        #endregion // <--NestedGrid-->

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
                try
                {
                    Status = Action_Status;
                }
                catch (Exception ex)
                {
                }
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

        #region Add/Edit Deposits for popup

        [HttpGet]
        public ActionResult Frm_Deposits_Window(string ActionMethod = null, string id = null, string BA_CLOSE_DATE = null, DateTime? EditDate = null)
        {
            // dep
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete"};
            string[] AM = { "New", "Edit", "View", "Delete"};
            for (i = 0; i < Rights.Length; i++)
            { 
                if (!CheckRights(BASE, ClientScreen.Profile_Deposit, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Deposits_Window','Not Allowed','No Rights');</script>");
                }            
            }
            DepositsInfo depositeInfo = new DepositsInfo();
            return PartialView(depositeInfo);
        }
        #endregion


        #region "Start--> LookupEdit Events"
        [HttpGet]
        public ActionResult LookUp_GetItemList(DataSourceLoadOptions loadOptions)
        {
            DataTable itemList = BASE._DepositsDBOps.GetOpeningProfileDepositItems("ID","Name") as DataTable;
            var depositData = DatatableToModel.DataTabletoDeposits_INFO(itemList);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(depositData, loadOptions)), "application/json");
        }
        #endregion

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Deposit, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Deposits_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_Deposits");
            Session.Remove("DepositsInfo_detailGrid_Data");
        }
        public void Deposits_user_rights()
        {
            ViewData["Deposits_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Deposit, "Add");
            ViewData["Deposits_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Deposit, "Update");
            ViewData["Deposits_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Deposit, "View");
            ViewData["Deposits_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Deposit, "Delete");
            ViewData["Deposits_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Deposit, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }

        #region Dev Extreme
        public ActionResult Frm_Deposits_Info_dx()
        {
            Deposits_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Deposit, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Deposit').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Deposit).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Deposits_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View();

        }
        [HttpGet]
        public ActionResult Frm_Deposits_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetDepositProfileListing DepProfile = new Common_Lib.RealTimeService.Param_GetDepositProfileListing();
            DepProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            DepProfile.Next_YearID = BASE._next_Unaudited_YearID;
            DataTable DI_Table = BASE._DepositsDBOps.GetProfileListing(DepProfile);
            var depositsdata = DatatableToModel.DataTabletoDeposits_INFO(DI_Table);
            Deposits_ExportData = depositsdata;
            return Content(JsonConvert.SerializeObject(Deposits_ExportData), "application/json");
        }
        public ActionResult Frm_Deposits_Info_DetailGrid_dx( bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_Deposit, !VouchingMode)), "application/json");
        }
        public ActionResult AdditionalInfo_Grid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_Deposit)), "application/json");
        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Deposit, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Deposits_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }

        #endregion
    }
}