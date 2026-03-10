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
    public class CashController : BaseController
    {
        // GET: Profile/Cash
        #region Global Variables        
        public List<DbOperations.Cash.Return_CashProfile> Cash_ExportData
        {
            get
            {
                return (List<DbOperations.Cash.Return_CashProfile>)GetBaseSession("Cash_ExportData_Cash");
            }
            set
            {
                SetBaseSession("Cash_ExportData_Cash", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> CashInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("CashInfo_DetailGrid_Data_Cash");
            }
            set
            {
                SetBaseSession("CashInfo_DetailGrid_Data_Cash", value);
            }
        }

        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> CashInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("CashInfo_AdditionalInfoGrid_Cash");
            }
            set
            {
                SetBaseSession("CashInfo_AdditionalInfoGrid_Cash", value);
            }
        }
        #endregion
        public ActionResult Frm_Cash_Info()
        {
            Cash_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Profile_Cash, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Cash').hide();</script>");//Code written for User Authorization do not remove
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Cash).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Check_Defaulf_Cash_Account();
            var Cash_data = BASE._CashDBOps.GetList();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["CashInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
     
            if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
            {
                DataTable Balance = BASE._Voucher_DBOps.GetCashBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Sdt.AddDays(-1), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
                if (Balance == null)
                {
                    return View();
                }
                if (Balance.Rows.Count > 0)
                {
                    Cash_data[0].OP_AMOUNT = Cash_data[0].OP_AMOUNT + (decimal)Balance.Rows[0]["CLOSING"];
                }
            }           
            Cash_ExportData = Cash_data;

            return View(Cash_data);
        }
        public PartialViewResult Frm_Cash_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Cash_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Cash_ExportData == null || command == "REFRESH")
            {
                Check_Defaulf_Cash_Account();
                var Cash_data = BASE._CashDBOps.GetList();
                if ((Cash_data.Count == 0))
                {
                    return PartialView();
                }
                if ((BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID)))
                {
                    DataTable Balance = BASE._Voucher_DBOps.GetCashBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Sdt.AddDays(-1), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
                    if ((Balance == null))
                    {
                        return PartialView();
                    }

                    if ((Balance.Rows.Count > 0))
                    {
                        Cash_data[0].OP_AMOUNT = Cash_data[0].OP_AMOUNT + (decimal)Balance.Rows[0]["CLOSING"];
                    }
                }
                Cash_ExportData = Cash_data;
            }
            return PartialView("Frm_Cash_Info_Grid", Cash_ExportData);
        }
        #region <--Nested Grid-->
        public ActionResult Frm_Cash_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.CashInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.CashInfo_RecID = RecID;
            ViewBag.CashInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {                    
                    CashInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Cash);
                    Session["CashInfo_detailGrid_Data"] = CashInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Cash);
                    CashInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    CashInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(CashInfo_DetailGrid_Data);
        }

        public ActionResult AdditionalInfo_Grid()
        {
            return View(CashInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public ActionResult Refresh_GridIcon_PreviewRow(string TempID, string NestedRowKeyValue)
        {            
            Cash_ExportData = BASE._CashDBOps.GetList();
            CashInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(TempID, TempID, ClientScreen.Profile_Cash);
            var AttachmentRow = CashInfo_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History = AttachmentRow.Vouching_History;
            string Main_iIcon = Cash_ExportData.Where(x => x.ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History
            }, JsonRequestBehavior.AllowGet);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "CashListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "CashListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["CashInfo_detailGrid_Data"];
        }

        #endregion // <--Nested Grid-->

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
                    Status = Action_Status == "Locked" ? "Locked" : Action_Status == null ? null : "UnLocked";
                }
                catch (Exception ex)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Lbl_Status_Color = "blue";
                    Pic_Status = "fa fa-lock";
                }
                else
                {
                    Pic_Status = "fa fa-unlock";
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
                    Lbl_StatusOn = Lbl_StatusOn,
                    Pic_Status
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
            if (!CheckRights(BASE, ClientScreen.Profile_Cash, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Cash_report_modal','Not Allowed','No Rights');$('#CashModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion

        public void SessionClear()
        {
            ClearBaseSession("_Cash");
            Session.Remove("CashInfo_detailGrid_Data");

        }
        public void Cash_user_rights()
        {
            ViewData["Cash_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "Add");
            ViewData["Cash_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "Update");
            ViewData["Cash_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "View");
            ViewData["Cash_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "Delete");
            ViewData["Cash_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "Export");
            ViewData["Cash_ListRight"] = CheckRights(BASE, ClientScreen.Profile_Cash, "List");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["Cash_SpecialGroupings"] = BASE.CheckActionRights(ClientScreen.Profile_Cash, Common.ClientAction.Special_Groupings);
        }
    }
}