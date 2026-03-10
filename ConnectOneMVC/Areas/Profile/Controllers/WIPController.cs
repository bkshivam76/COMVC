using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class WIPController : BaseController
    {
        #region "Start--> Default Variables"             
        public List<DbOperations.Return_ReferenceType> WIP_ExportData
        {
            get
            {
                return (List<DbOperations.Return_ReferenceType>)GetBaseSession("WIP_ExportData_WIP");
            }
            set
            {
                SetBaseSession("WIP_ExportData_WIP", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> WIPInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("WIPInfo_DetailGrid_Data_WIP");
            }
            set
            {
                SetBaseSession("WIPInfo_DetailGrid_Data_WIP", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> WIPInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("WIPInfo_AdditionalInfoGrid_WIP");
            }
            set
            {
                SetBaseSession("WIPInfo_AdditionalInfoGrid_WIP", value);
            }
        }
        #endregion       

        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/WIP)
        public ActionResult Frm_WIP_Info()
        {
            WIP_user_rights();
            if (!CheckRights(BASE,ClientScreen.Profile_WIP,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_WIP').hide();</script>");
            }

            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_WIP).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing_WIP WIPProfile = new Common_Lib.RealTimeService.Param_GetProfileListing_WIP();
            WIPProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            WIPProfile.Next_YearID = BASE._next_Unaudited_YearID;
            var WIP_Table = BASE._WIPDBOps.GetProfileListing_WIP(WIPProfile);  // BASE._AssetDBOps.GetList(Voucher_Entry, Profile_Entry)

            if (WIP_Table == null)
            {
                return PartialView("Frm_WIP_Info", null);
            }

            // BUILD DATA

            var Final_Data = WIP_Table;
            WIP_ExportData = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["WIPInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Final_Data);
        }
        public PartialViewResult Frm_WIP_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            WIP_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (WIP_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetProfileListing_WIP WIPProfile = new Common_Lib.RealTimeService.Param_GetProfileListing_WIP();
                WIPProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                WIPProfile.Next_YearID = BASE._next_Unaudited_YearID;
                var WIP_Table = BASE._WIPDBOps.GetProfileListing_WIP(WIPProfile);

                // BASE._AssetDBOps.GetList(Voucher_Entry, Profile_Entry)
                if ((WIP_Table == null))
                {
                    return PartialView("Frm_WIP_Info", null);
                }

                WIP_ExportData = WIP_Table;
            }
            return PartialView("Frm_WIP_Info_Grid", WIP_ExportData);
        }

        #region <--NestedGrid-->
        public ActionResult Frm_WIP_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.WIPInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.WIPInfo_RecID = RecID;
            ViewBag.WIPInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    WIPInfo_DetailGrid_Data =  BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_WIP);                    
                    Session["WIPInfo_detailGrid_Data"] = WIPInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_WIP);
                    WIPInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["WIPInfo_detailGrid_Data"] = data.DocumentMapping;
                    WIPInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(WIPInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(WIPInfo_AdditionalInfoGrid);
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
            settings.Name = "WIPListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "WIPListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["WIPInfo_detailGrid_Data"];
        }
        #endregion // <--NestedGrid-->
        public ActionResult WIPCustomDataAction(string key)
        {
            var _Final_Data = (List<DbOperations.Return_ReferenceType>)WIP_ExportData;

            var it = (DbOperations.Return_ReferenceType)_Final_Data.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.ID + "![" + it.Edit_Date + "![" + it.Add_Date + "![" + it.Add_By + "![" + it.Edit_By + "!["
                    + it.Action_Status + "![" + it.Action_Date + "![" + it.Action_By + "![" + it.Opening + "![" + it.LED_ID + "![" + it.WIP_Ledger + "![" + it.Reference + "![" + it.Date_of_Creation + "![" + it.TR_ID +"![" + it.Entry_Type + "![" + it.OpenActions + "![" + it.YearID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
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
                    Pic_Status = "Fa Fa-Lock";
                    Lbl_Status_Color = "blue";
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
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
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
                    //Lbl_By = Lbl_By,
                    Pic_Status = Pic_Status
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
                    Lbl_StatusOn = "",
                    Pic_Status = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_WIP, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WIP_report_modal_result','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
 
        #region Add/Edit Bank Account Details for popup
        public ActionResult DataNavigation(string ActionMethod, string ID,  DateTime? Edit_Date, string Action_Status,string Entry_Type,string TR_ID,int? YearID=0,int? OpenActions=0,bool MultiUserConfirmation = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (ActionMethod == "New")
                {
                    if (BASE._Completed_Year_Count > 0 && BASE._GoldSilverDBOps.IsTBImportedCentre() == false)
                    {
                        jsonParam.message = "Entry Cannot be created. . !<br><br>Required Profile Entries have already been created for this centre...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                       // return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (!BASE._WIPDBOps.IsTBImportedCentre())
                        {
                            jsonParam.message = "Profile Entries not allowed for a newly opened center";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            //jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    int Ctr = 0;
                    string xTemp_ID = "";
                    object MaxValue = 0;
                    string xType, xStatus;  
                    xTemp_ID = ID;
                    xType = Entry_Type;
                    xStatus =Action_Status;
                    string xTr_ID = TR_ID;

                    if (ActionMethod == "Edit")
                    {
                        xTemp_ID = ID ?? "";
                        bool? IsWIPCarriedFwd = BASE._WIPDBOps.IsWIPCarriedForward(xTemp_ID, (int)YearID);
                        if (IsWIPCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsWIPCarriedFwd == true && BASE._prev_Unaudited_YearID != 0)
                        {
                            jsonParam.message = "Entry Cannot be Edited . . !<br><br>This entry has been carried forward from previous year(s). Updation(Partial) can be done only after finalization of previous year accounts....!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(null, xTemp_ID);
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + "." + "<br><br> Please delete the record for editing this Entry. ";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        xTr_ID = TR_ID;
                        xStatus = Action_Status;
                        var value = (int)Enum.Parse(typeof(Common_Lib.Common.Record_Status), "_" + xStatus);
                        //object MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = BASE._WIPDBOps.GetStatus(xTemp_ID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found / Changed In Background . . . !";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        string multiUserMsg = "";
                        if ((int)value != (int)MaxValue)
                        {
                            if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record has been locked in the background by another user.";
                            }
                            else if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record has been unlocked in the background by another user.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "Record Status has been changed in the background by another user";
                                AllowUser = true;
                            }
                            if (MultiUserConfirmation == false)
                            {
                                if (AllowUser)
                                {
                                    jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                    jsonParam.title = "Confirmation...";
                                    jsonParam.result = false;
                                    jsonParam.isconfirm = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                        {
                            multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted... !" + multiUserMsg + "<br><br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = xTemp_ID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                        param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                        DataTable jvAdj = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_WIP);
                        if (jvAdj == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        MaxValue = jvAdj.Rows.Count;
                        if ((int)MaxValue > 0)
                        {
                            jsonParam.message = "Entry cannot be Edited/Deleted...!<br><br>There are journal voucher references posted against it...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (ActionMethod == "Delete")
                    {
                        bool? IsWIPCarriedFwd = BASE._WIPDBOps.IsWIPCarriedForward(xTemp_ID, (int)YearID);
                        if (IsWIPCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsWIPCarriedFwd == true)
                        {
                            jsonParam.message = "Entry Cannot be deleted . . !<br><br>This entry has been carried forward from previous year(s)...! ";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }                      
                        if (xTr_ID.Trim().Length > 0)
                        {
                            jsonParam.message = "Entry Cannot be Edited / Deleted . . !<br><br>This Entry Managed by Voucher Entry...! ";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(null, xTemp_ID);    //'Get Dependent Entries on WIP creted by current Txn
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + "." + "<br><br> Please delete the record for deleting this Entry. ";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        int xOpenActions = (int)OpenActions;
                        if (xOpenActions > 0)
                        {
                            jsonParam.message = "Entry Cannot be Deleted. . !<br><br>There are open actions / queries posted against it...! ";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        xStatus = Action_Status;
                        var value = (int)Enum.Parse(typeof(Common_Lib.Common.Record_Status), "_" + xStatus);
                        //object MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = BASE._WIPDBOps.GetStatus(xTemp_ID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found / Changed In Background. . . !";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        string multiUserMsg = "";
                        if ((int)value != (int)MaxValue)
                        {
                            if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record has been locked in the background by another user.";
                            }
                            else if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record has been unlocked in the background by another user.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "Record Status has been changed in the background by another user";
                                AllowUser = true;
                            }
                            if (MultiUserConfirmation == false)
                            {
                                if (AllowUser)
                                {
                                    jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                    jsonParam.title = "Confirmation...";
                                    jsonParam.result = false;
                                    jsonParam.isconfirm = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                        {
                            multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                            jsonParam.message = "Locked Entry Cannot Be Edited/Deleted...!" + multiUserMsg + "<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (ActionMethod == "View")
                    {
                        xTemp_ID = ID;

                        MaxValue = BASE._WIPDBOps.GetStatus(xTemp_ID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found / Changed In Background. . . ! ";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (ActionMethod == "LOCKED")
                    {
                        if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_WIP, Common_Lib.Common.ClientAction.Lock_Unlock) && ActionMethod == "LOCKED")
                        {
                            jsonParam.message = "Not Allowed!!";
                            jsonParam.title = "No Rights";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        xTemp_ID = ID;
                        xType = Entry_Type;
                        DataTable _dtableTelData = BASE._WIPDBOps.GetRecord(xTemp_ID);
                        var xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.WIP_INFO, xTemp_ID);
                        if (xType.ToUpper() == Voucher_Entry.ToUpper())
                        {
                            jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only . . . !" + "<br><br>" + "Please unselect Entries Created from Voucher ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        xStatus = Action_Status;
                        var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                        bool AllowUser = false;
                        MaxValue = BASE._WIPDBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if (value != (int)MaxValue)
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Completed)
                            {
                                AllowUser = true;
                            }
                            if (MultiUserConfirmation == false)
                            {
                                if (AllowUser)
                                {
                                    jsonParam.message = "The Record Has Been UnLocked In The Background By Another User<br><br>Do You Want To Continue...?";
                                    jsonParam.title = "Confirmation...";
                                    jsonParam.result = false;
                                    jsonParam.isconfirm = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Already Locked Entries Caanot Be Re-Locked...!<br><br>Please Unselect Already Locked Entries ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Incomplete)
                        {
                            jsonParam.message = "Incomplete Entries Cannot Be Locked...!<br><br>Please Unselect InComplete Entries or ask Center to Complete it...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (xRemarks != null && !Convert.IsDBNull(xRemarks))
                        {
                            if ((int)MaxValue > 0)
                            {
                                jsonParam.message = "Entries With Pending Queries Can't Be Locked...!<br><br>Please Unselect Such Entries...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        xTemp_ID = ID;
                        //xid = xTemp_ID;
                        Ctr += 1;
                        if (!BASE._WIPDBOps.MarkAsLocked(xTemp_ID))
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Ctr > 0)
                        {
                            jsonParam.message = Messages.LockedSuccess(Ctr);
                            jsonParam.title = "Locked...";
                            jsonParam.result = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (ActionMethod == "UNLOCKED")
                    {
                        if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_WIP, Common_Lib.Common.ClientAction.Lock_Unlock) && ActionMethod == "UNLOCKED")
                        {
                            jsonParam.message = "Not Allowed!!";
                            jsonParam.title = "No Rights";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        xTemp_ID = ID;
                        xType = Entry_Type;

                        bool AllowUser = false;
                        xStatus = Action_Status;
                        var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                        MaxValue = BASE._WIPDBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if (value != (int)MaxValue)
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Locked)
                            {
                                AllowUser = true;
                            }
                            if (MultiUserConfirmation == false)
                            {
                                if (AllowUser)
                                {
                                    jsonParam.message = "The Record Has Been Locked In The Background By Another User<br><br>Do You Want To Continue...?";
                                    jsonParam.title = "Confirmation...";
                                    jsonParam.result = false;
                                    jsonParam.isconfirm = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (xType.ToUpper() == Voucher_Entry.ToUpper())
                        {
                            jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only. . . !<br><br>Please unselect Entries Created from Voucher ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common_Lib.Common.Record_Status._Completed)
                        {
                            jsonParam.message = "Already Unlocked Entries can't be Re-Unlocked . . . !<br><br>Please unselect already unlocked Entries...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        xTemp_ID = ID;
                        //xid = xTemp_ID;
                        Ctr += 1;
                        if (!BASE._WIPDBOps.MarkAsComplete(xTemp_ID))
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Ctr > 0)
                        {
                            jsonParam.message = Messages.UnlockedSuccess(Ctr);
                            jsonParam.title = "UnLocked...";
                            jsonParam.result = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (ActionMethod == "ADD REMARKS")
                    {
                        if (BASE.CheckActionRights(ClientScreen.Profile_WIP, Common.ClientAction.Manage_Remarks))
                        {
                            if (xType.ToUpper() == "VOUCHER ENTRY")
                            {
                                jsonParam.message = "Entries created from vouchers can be audited from vouchers only...!<br><br>Please unselect Entries Created from Voucher ...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if (xStatus.ToUpper() != "LOCKED")
                            {
                                jsonParam.result = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                jsonParam.message = "Queries Can't Be Added to freezed Records...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            jsonParam.message = "Not Allowed!!";
                            jsonParam.title = "No Rights";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                jsonParam.message = "";
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Exception !!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }

        }
       
        [HttpGet]
        public ActionResult Frm_WIP_Window(DateTime? EditedOn, string ActionMethod = null, string ID = null)
        {
            try
            {
                ViewBag.TempActionMethod = ActionMethod;
                ViewBag.Id = ID;
                WIP_user_rights();

                var i = 0;
                string[] Rights = { "Add", "Update", "View", "Delete" };
                string[] AM = { "New", "Edit", "View", "Delete" };
                for (i = 0; i < Rights.Length; i++)
                {
                    if (!CheckRights(BASE, ClientScreen.Profile_WIP, Rights[i]) && ActionMethod == AM[i])
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                    }
                }

                WIPInfo model = new WIPInfo();
                model.xID = ID;
                var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                model.ActionMethod = Navigation_Mode_tag;
                model.TempActionMethod = Navigation_Mode_tag.ToString();
                if (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit
                            || Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete
                            || Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)
                {
                    model.EditDate = Convert.ToDateTime(EditedOn);

                    DataTable d1 = BASE._WIPDBOps.GetRecord(ID);
                    if (d1 == null)
                    {
                        string message = Messages.SomeError;
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                    }
                    if (d1.Rows.Count > 0)
                    {                     
                        model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        model.YearID = Convert.ToInt32(d1.Rows[0]["WIP_COD_YEAR_ID"]);
                        if (Convert.IsDBNull(d1.Rows[0]["WIP_LED_ID"]) == false)
                        {
                            if (string.IsNullOrWhiteSpace(d1.Rows[0]["WIP_LED_ID"].ToString()) == false)
                            {
                                model.LED_ID = d1.Rows[0]["WIP_LED_ID"].ToString();
                                model.GLookUp_WIP_LedgerList_WIPWindow = d1.Rows[0]["WIP_LED_ID"].ToString();
                            }
                        }
                        model.Reference_WIPWindow = d1.Rows[0]["WIP_REF"].ToString().HandleEscapeCharacters(); //textbox Handling
                        model.Opening_WIPWindow = Convert.ToDouble(d1.Rows[0]["WIP_AMT"]);
                        model.TR_ID = Convert.IsDBNull(d1.Rows[0]["WIP_TR_ID"]) == true ? null : d1.Rows[0]["WIP_TR_ID"].ToString();                        
                    }
                }
                if (Navigation_Mode_tag == Common.Navigation_Mode._Edit)
                {
                    model.IsWIPCarriedFwd = (bool)BASE._WIPDBOps.IsWIPCarriedForward(model.xID, model.YearID);                   
                }
                return View(model);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);                
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + msg + "','Exception !!!');</script>");
            }
            
        }
        [HttpPost]
        public ActionResult Frm_WIP_Window(WIPInfo model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_WIP_LedgerList_WIPWindow))
                    {
                        jsonParam.message = "Ledger Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_WIP_LedgerList";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Reference_WIPWindow))
                    {
                        jsonParam.message = "Please enter relevant reference which you may remember while converting WIP to assets . . . !";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Reference_WIPWindow";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Opening_WIPWindow==null||model.Opening_WIPWindow <= 0)
                    {
                        jsonParam.message = "Opening Value cannot be Zero / Negative. . . !";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Opening_WIPWindow";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //Check Duplicate Reference
                    Param_GetDuplicateReferenceCount Param = new Param_GetDuplicateReferenceCount();
                    Param.Reference = model.Reference_WIPWindow;
                    Param.Tag = Convert.ToInt32(model.ActionMethod);
                    if (model.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        Param.RecID = model.ID;
                    }
                    int cnt = Convert.ToInt32(BASE._WIPCretionVouchers.GetDuplicateReferenceCount(Param));
                    if (cnt > 0)
                    {
                        jsonParam.message = "Same Reference Already Exists";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                string Status_Action = "";
                Status_Action = Convert.ToString((int)Common.Record_Status._Completed);
                //WIP Insert 
                if (model.ActionMethod == Common.Navigation_Mode._New)
                {
                    Param_Insert_WIP_Profile InParam = new Param_Insert_WIP_Profile();
                    InParam.LedID = model.GLookUp_WIP_LedgerList_WIPWindow;
                    InParam.Reference = model.Reference_WIPWindow;
                    InParam.Amount = Convert.ToDecimal(model.Opening_WIPWindow);
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = Guid.NewGuid().ToString();
                    if (BASE._WIPDBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "WIP";                       
                        jsonParam.closeform = true;
                        jsonParam.result = true;
                        return Json(new { jsonParam, xid= InParam.RecID }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                //WIP Update
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    Parameter_Update_WIP_Profile UpParam = new Parameter_Update_WIP_Profile();
                    UpParam.LedID = model.GLookUp_WIP_LedgerList_WIPWindow;
                    UpParam.Reference = model.Reference_WIPWindow;
                    UpParam.Amount = Convert.ToDouble(model.Opening_WIPWindow);
                    UpParam.RecID = model.ID;

                    if (BASE._WIPDBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "WIP";                       
                        jsonParam.closeform = true;
                        //jsonParam.refreshgrid = true;
                        jsonParam.result = true;
                        return Json(new { jsonParam, xid = model.ID }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                //WIP Delete
                if (model.ActionMethod == Common.Navigation_Mode._Delete)
                {
                    object openActions = BASE._Action_Items_DBOps.GetOpenActions(model.ID, "WIP_INFO");
                    if (openActions == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(openActions) > 0)
                    {
                        jsonParam.message = Messages.DependencyChanged("There are open actions / queries posted against it...!");
                        jsonParam.title = "Referred Record Already Changed!!";
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._WIPDBOps.Delete(model.ID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "WIP";
                        jsonParam.closeform = true;      
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Exception !!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            

        }
        public ActionResult LookUp_GetWIPLedgerList_WIP(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            var d1 = BASE._WIP_Finalization_DBOps.GetListOfWIPLedgers(BASE.Is_HQ_Centre);
            d1.Sort((x, y) => x.WIP_LEDGER.CompareTo(y.WIP_LEDGER));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(d1, loadOptions)), "application/json");
        }
        #endregion
                
        public void SessionClear_WIPWindow()
        {
            ClearBaseSession("_WIPProfile");           
        }
        public void SessionClear()
        {
            ClearBaseSession("_WIP");
            Session.Remove("WIPInfo_detailGrid_Data");
        }
        public void WIP_user_rights()
        {
            ViewData["WIP_AddRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "Add");
            ViewData["WIP_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "Update");
            ViewData["WIP_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "View");
            ViewData["WIP_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "Delete");
            ViewData["WIP_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "Export");
            ViewData["WIP_ReportRight"] = CheckRights(BASE, ClientScreen.Profile_WIP, "Report");
            ViewData["WIP_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_WIP, Common.ClientAction.Lock_Unlock);
            ViewData["WIP_SpecialGrouping"] = BASE.CheckActionRights(ClientScreen.Profile_WIP, Common.ClientAction.Special_Groupings);
            ViewData["WIP_ManageRemarks"] = BASE.CheckActionRights(ClientScreen.Profile_WIP, Common.ClientAction.Manage_Remarks);
            ViewData["WIP_ViewRemarks"] = BASE.CheckActionRights(ClientScreen.Profile_WIP, Common.ClientAction.View_Remarks);
            ViewData["AccVou_Payment_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Update");
            ViewData["AccVou_Payment_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "View");
            ViewData["AccVou_Payment_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Delete");
            
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        #region Dev Extreme
        public ActionResult Frm_WIP_Info_dx(bool VouchingMode = false)
        {
            WIP_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_WIP, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_WIP').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_WIP).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.VouchingMode = VouchingMode;

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["WIPInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View();
        }
        [HttpGet]
        public ActionResult Frm_WIP_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetProfileListing_WIP WIPProfile = new Common_Lib.RealTimeService.Param_GetProfileListing_WIP();
            WIPProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            WIPProfile.Next_YearID = BASE._next_Unaudited_YearID;       
            return Content(JsonConvert.SerializeObject(BASE._WIPDBOps.GetProfileListing_WIP(WIPProfile)), "application/json");           
        }
        public ActionResult Frm_WIP_Info_GridData_detail_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_WIP, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_WIP_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_WIP)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx(string GridName)
        {
            if (!CheckRights(BASE, ClientScreen.Profile_WIP, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WIP_report_modal_result','Not Allowed','No Rights');</script>");
            }        
            ViewBag.GridName = GridName;
            ViewBag.Filename = GridName + "_" + BASE._open_UID_No + "_" + BASE._open_Year_ID;
            return View("Common_Export");
        }
        public ActionResult Frm_WIP_Txn_Report(string WIP_ID = "", double Opening = 0, string LedgerID = "", string LedgerName = "", string Reference = "", DateTime? OpeningDate = null, string CallingFrom = "WIP_Profile", string WIP_PopupID = "WIP_Txn_Report_modal",string EntryType="")
        {
            WIP_Txn_Report model = new WIP_Txn_Report();
            model.ID = WIP_ID;
            model.Opening = Opening;
            model.LED_ID = LedgerID;
            model.WIP_Ledger = LedgerName;
            model.Reference = Reference;
            model.Date_of_Creation = OpeningDate != null ? Convert.ToDateTime(OpeningDate) : BASE._open_Year_Sdt;
            model.GridData = new List<WIP_Txn_Report>();
            model.CallingFrom = CallingFrom == "WIP_Profile" ? "WIP_Profile" : "WIP_Finalization";
            model.WIP_PopupID = CallingFrom == "WIP_Profile" ? "WIP_Txn_Report_modal" : WIP_PopupID;
            model.EntryType = EntryType;
            return View(model);
        }
        public ActionResult Frm_WIP_Txn_Grid(DataSourceLoadOptions loadOptions, DateTime? OpeningDate = null, string LED_ID = "", string WIP_ID = "", decimal Opening = 0,string EntryType="")
        {
            List<WIP_Txn_Report_Grid> data = new List<WIP_Txn_Report_Grid>();
            if (string.IsNullOrWhiteSpace(LED_ID) == false)
            {
                DateTime FrDate = Convert.ToDateTime(OpeningDate);
                DateTime ToDate = BASE._open_Year_Edt;

                Common_Lib.RealTimeService.Param_GetTxnReport param = new Common_Lib.RealTimeService.Param_GetTxnReport();
                param.FromDate = BASE._open_Year_Sdt;
                param.ToDate = ToDate;
                param.Led_ID = LED_ID;
                param.InsttId = BASE._open_Ins_ID;
                param.YearID = BASE._open_Year_ID;
                param.WIP_ID = WIP_ID;
                DataTable _Party_Table = BASE._WIPDBOps.GetTxn_Report(param);
                _Party_Table.Columns.Add(new DataColumn("Balance", Type.GetType("System.String")));

                DataRow OpeningRow = _Party_Table.NewRow();
                OpeningRow["Date"] = FrDate;
                OpeningRow["Item Name"] = "Opening Balance";
                if (Opening > 0)
                {
                    OpeningRow["Debit"] = Opening.ToString("0.00");
                    OpeningRow["Credit"] = 0.0.ToString("0.00");
                }
                if (Opening < 0)
                {
                    OpeningRow["Credit"] = Opening.ToString("0.00");
                    OpeningRow["Debit"] = 0.0.ToString("0.00");
                }
                if (Opening == 0)
                {
                    OpeningRow["Balance"] = 0.ToString("0.00");
                }
                _Party_Table.Rows.InsertAt(OpeningRow, 0);

                decimal Total = 0;
                double TotCredit = 0;
                double TotDebit = 0;

                foreach (DataRow cRow in _Party_Table.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(cRow["Debit"].ToString()))
                    {
                        if (cRow["Debit"].ToString().Length > 0)
                        {
                            Total += Convert.ToDecimal(Convert.ToDouble(cRow["Debit"]));
                            TotDebit += Convert.ToDouble(cRow["Debit"]);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(cRow["Credit"].ToString()))
                    {
                        if (cRow["Credit"].ToString().Length > 0)
                        {
                            Total = Total - Convert.ToDecimal(Convert.ToDouble(cRow["Credit"]));
                            TotCredit += Convert.ToDouble(cRow["Credit"]);
                        }
                    }
                    if (cRow["Balance"].ToString().Length == 0)
                    {
                        if (Total > 0)
                        {
                            cRow["Balance"] = Total.ToString("0.00") + " Dr";
                        }
                        else if (Total < 0)
                        {
                            cRow["Balance"] = (Total * -1).ToString("0.00") + " Cr";
                        }
                        else
                        {
                            cRow["Balance"] = 0.ToString("0.00") + " Cr";
                        }
                    }
                    if ((cRow["Item Name"].ToString() == "Opening Balance") && (EntryType=="Profile Entry") && (OpeningDate == BASE._open_Year_Sdt))
                    {
                        TotDebit = TotDebit - Convert.ToDouble(cRow["Debit"]);
                        TotCredit = TotCredit - Convert.ToDouble(cRow["Credit"]);
                        cRow["Debit"] = 0.ToString("0.00");
                        cRow["Credit"] = 0.ToString("0.00");
                    }
                }
                DataRow ClosingRow = _Party_Table.NewRow();
                ClosingRow["Item Name"] = "Total:";
                ClosingRow["Credit"] = TotCredit.ToString("0.00");
                ClosingRow["Debit"] = TotDebit.ToString("0.00");
                _Party_Table.Rows.InsertAt(ClosingRow, _Party_Table.Rows.Count);

                data = DatatableToModel.DataTableto_WIP_Txn_Report_Grid(_Party_Table);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
            //WIP_Txn_Grid_ExportData = data;
            //return PartialView("Frm_WIP_Txn_Grid", WIP_Txn_Grid_ExportData);
        }
        public ActionResult WIP_Txn_Report_Export_Options()
        {
            return PartialView();
        }
        #endregion
    }
}