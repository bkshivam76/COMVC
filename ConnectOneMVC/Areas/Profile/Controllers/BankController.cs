using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
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

//using DevExpress.Web.Demos.Mvc;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class BankController : BaseController
    {
        #region Global Initializations      
        public List<DbOperations.Audit.Return_GetDocumentMapping> BankInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("BankInfo_DetailGrid_Data_BankInfo");
            }
            set
            {
                SetBaseSession("BankInfo_DetailGrid_Data_BankInfo", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> Bank_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("Bank_AdditionalInfoGrid_BankInfo");
            }
            set
            {
                SetBaseSession("Bank_AdditionalInfoGrid_BankInfo", value);
            }
        }
        public List<Common_Lib.DbOperations.BankAccounts.Return_GetAccountList> BankExportData
        {
            get
            {
                return (List<Common_Lib.DbOperations.BankAccounts.Return_GetAccountList>)GetBaseSession("BankExportData_BankInfo");
            }
            set
            {
                SetBaseSession("BankExportData_BankInfo", value);
            }
        }
        public List<BANK_INFO> BankDDList
        {
            get { return (List<BANK_INFO>)GetBaseSession("BankDDList_BankWindow"); }
            set { SetBaseSession("BankDDList_BankWindow", value); }
        }
        public List<BANK_BRANCH_INFO> BranchDDList
        {
            get { return (List<BANK_BRANCH_INFO>)GetBaseSession("BranchDDList_BankWindow"); }
            set { SetBaseSession("BranchDDList_BankWindow", value); }
        }
        public List<ADDRESS_BOOK> SignDDList
        {
            get { return (List<ADDRESS_BOOK>)GetBaseSession("SignDDList_BankWindow"); }
            set { SetBaseSession("SignDDList_BankWindow", value); }
        }
        #endregion
        #region Default Grid Page Action Method GET: Profile/Bank
        public ActionResult Frm_Bank_Info(string PopupId = null)// resolved redmine #132996 
        {          
            Bank_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Profile_BankAccounts, "List")))
            {
                if (!string.IsNullOrWhiteSpace(PopupId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('"+ PopupId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_BankAccounts).ToString()) ? 1 : 0;
            ViewBag.PopupID = PopupId == null ? "" : PopupId;
            ViewBag.UserType = BASE._open_User_Type;
            List<DbOperations.BankAccounts.Return_GetAccountList> Final_Data = BASE._BankAccountDBOps.GetAccountList();
            BankExportData = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock);
            ViewData["BankInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Final_Data);
        }
        public ActionResult Frm_Bank_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default",string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            Bank_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            
            if (BankExportData == null || command == "REFRESH")
            {
                List<DbOperations.BankAccounts.Return_GetAccountList> FinalData = BASE._BankAccountDBOps.GetAccountList();
                BankExportData = FinalData;
            }
            var Final_Data = BankExportData;
            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock);
            return PartialView("Frm_Bank_Info_Grid", Final_Data);
        }

        #region nested grid
        public ActionResult Frm_Bank_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.BankInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.BankInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_BankAccounts);
                    BankInfo_DetailGrid_Data = _docList;
                    Session["BankInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_BankAccounts);
                    BankInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["BankInfo_detailGrid_Data"] = data.DocumentMapping;
                    Bank_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }        
            return PartialView(BankInfo_DetailGrid_Data);
        }
        public ActionResult Frm_Voucher_Info_Bank_Grid_Auditor_AdditionalInfo()
        {
            return View(Bank_AdditionalInfoGrid);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "BankListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "BankListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["BankInfo_detailGrid_Data"];
        }
        #endregion
        public ActionResult LeftPaneContent(string ID,bool VouchingMode)
        {
            ViewBag.ID = ID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public void GetGridData()
        {
            List<DbOperations.BankAccounts.Return_GetAccountList> FinalData = BASE._BankAccountDBOps.GetAccountList();
            BankExportData = FinalData;
        }
        public ActionResult Refresh_GridIcon_PreviewRow(string TempID,string NestedRowKeyValue)
        {
            GetGridData();
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(TempID, "", ClientScreen.Profile_BankAccounts);
            BankInfo_DetailGrid_Data = _docList;
            var AttachmentRow = BankInfo_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History= AttachmentRow.Vouching_History;      
            string Main_iIcon= BankExportData.Where(x => x.ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History             
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "devextreme"
        public ActionResult Frm_Bank_Info_dx(string PopupId = null) 
        {
            Bank_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Profile_BankAccounts, "List")))
            {
                if (!string.IsNullOrWhiteSpace(PopupId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopupId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_BankAccounts).ToString()) ? 1 : 0;
            ViewBag.PopupID = PopupId == null ? "" : PopupId;
            ViewBag.UserType = BASE._open_User_Type;         
            BankExportData = new List<DbOperations.BankAccounts.Return_GetAccountList>();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock);
            ViewData["BankInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View();
        }
        [HttpGet]
        public ActionResult Frm_Bank_Info_GridData_dx()
        {
            BankExportData = BASE._BankAccountDBOps.GetAccountList();
            return Content(JsonConvert.SerializeObject(BankExportData), "application/json");
        }
        public ActionResult Frm_Bank_Info_GridData_detail_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_BankAccounts, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Bank_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_BankAccounts)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Bank_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        #region Bank Account Close Windows Methods
        [HttpGet]
        public ActionResult Frm_Bank_Window_Close(string ActionMethod, string id)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            BankAccountOperation model = new BankAccountOperation();
            var BankData = BankExportData.Where(x => x.ID == id).FirstOrDefault();
            var InfoLastEditOn = BankData.Edit_Date;
            DataTable d1 = BASE._BankAccountDBOps.GetRecord(id);
            if (d1 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (d1.Rows.Count == 0)
            {
                jsonParam.message = Messages.RecordChanged("Current Account");
                jsonParam.title = "Record Changed / Removed in Background!!";
                jsonParam.result = false;
                jsonParam.refreshgrid = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (BASE.AllowMultiuser())
            {
                if (InfoLastEditOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                {
                    jsonParam.message = Messages.RecordChanged("Current Account");
                    jsonParam.title = "Record Already Changed !!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            if (Convert.IsDBNull(d1.Rows[0]["BA_CLOSE_DATE"]))
            {
                model.BA_ClOSE_DATE = Convert.IsDBNull(d1.Rows[0]["BA_CLOSE_DATE"]) ? (DateTime?)null : Convert.ToDateTime(d1.Rows[0]["BA_CLOSE_DATE"]);
            }
            model.ID = id;
            model.ActionName = ActionMethod == "ACCOUNT-REOPEN" ? "_Re_Open" : "_Close";
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionName);
            model.BankName = BankData.Name;
            model.BankBranch = BankData.Branch;
            string xTemp_AccNo = "";
            if (BankData.BA_ACCOUNT_TYPE.ToUpper().Trim() == Common.Bank_Trans_Type.SAVING.ToString().ToUpper())
            {
                xTemp_AccNo = "Account No.: " + BankData.BA_ACCOUNT_NO;
            }
            else
            {
                xTemp_AccNo = "Customer No.: " + BankData.BA_CUST_NO;
            }
            model.AccountNo = xTemp_AccNo;
            model.AccountType = "Account Type: " + BankData.BA_ACCOUNT_TYPE;
            ViewBag.Title = ActionMethod == "ACCOUNT-REOPEN" ? "Bank Account Re-Open" : "Bank Account Closed";
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Bank_Window_Close(BankAccountOperation model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string ActionMethod = model.ActionName;
                string xID = model.ID;
                DateTime? Txt_Date = model.BA_ClOSE_DATE;
                var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Close || Tag == Common.Navigation_Mode._Re_Open)
                    {
                        DataTable bankacc_DbOps = BASE._BankAccountDBOps.GetRecord(xID);
                        if (bankacc_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Account");
                            jsonParam.title = "Record Already Changed !!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(bankacc_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Account");
                            jsonParam.title = "Record Already Changed !!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Tag == Common.Navigation_Mode._Close)
                    {
                        DataTable BA_Table = BASE._BankAccountDBOps.GetList(xID);
                        if (BA_Table.Rows[0]["BA_ACCOUNT_TYPE"].ToString() == "FD")
                        {
                            jsonParam.message = "FD Bank Account cannot be closed...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable Bank_Det = BASE._Voucher_DBOps.GetBankBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Edt, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
                        DataRow[] rows = Bank_Det.Select("ID = '" + BA_Table.Rows[0]["ID"] + "'");
                        if (rows.Count() > 0)
                        {
                            object xBalance = Convert.ToDouble(rows[0]["CLOSING"].ToString());
                            if (xBalance == null)
                            {
                                jsonParam.message = "Account not Listed...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((double)xBalance < 0 || (double)xBalance > 0)
                            {
                                jsonParam.message = "This Bank Account cannot be closed...!<br><br>Some changes have been made by other user...! <br><br>Closing Balance must be Nil...!<br><br>Current Closing Balance: " + Convert.ToDouble(xBalance).ToString("#,0.00");
                                jsonParam.title = "Refered Record Already Changed...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                if (Tag == Common.Navigation_Mode._Close)
                {
                    if (CheckDate(Txt_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        jsonParam.focusid = "BA_ClOSE_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Txt_Date < BASE._open_Year_Sdt || Txt_Date > BASE._open_Year_Edt)
                    {
                        jsonParam.message = "Date Not As Per Financial Year...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        jsonParam.focusid = "BA_ClOSE_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Txt_Date > DateTime.Now)
                    {
                        jsonParam.message = "Future Date not Allowed...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        jsonParam.focusid = "BA_ClOSE_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var LastTran = Check_Transaction((DateTime)Txt_Date, xID);
                    if (LastTran != null)
                    {
                        jsonParam.message = "Date must Be Greater Than Last Transaction Date...!<br><br>Last Tr. Date: " + ((DateTime)LastTran).ToString("dd/MM/yyyy");
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        jsonParam.focusid = "BA_ClOSE_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                //CHECKING LOCK STATUS
                if (Tag == Common.Navigation_Mode._Close || Tag == Common.Navigation_Mode._Re_Open)
                {
                    object MaxValue = 0;
                    MaxValue = BASE._BankAccountDBOps.GetStatus(xID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry can not be Changed...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Tag == Common.Navigation_Mode._Close)
                {
                    string CloseDate = null;
                    if (CheckDate(Txt_Date.ToString()))
                    {
                        CloseDate = Convert.ToDateTime(Txt_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    if (BASE._BankAccountDBOps.Close(CloseDate, xID))
                    {
                        jsonParam.message = "A/c. Closed Successfully";
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Tag == Common.Navigation_Mode._Re_Open)
                {
                    if (BASE._BankAccountDBOps.Reopen(xID))
                    {
                        jsonParam.message = "A/c. Re-Open Successfully";
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Add/Edit Bank Account Details for popup

        public JsonResult Check_Frm_Bank_Window(string ActionMethod = null, string id = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            var BankData = BankExportData.Where(x => x.ID == id).FirstOrDefault();
            var xTemp_ID = BankData.ID;
            var xTemp_CDate = BankData.BA_CLOSE_DATE;
            var xOpenActions = BankData.OpenActions;
            var xTemp_Year = BankData.YearID;
            var xStatus = BankData.Action_Status;
            var value = (Common.Record_Status)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
            try
            {
                if (ActionMethod == "Edit")
                {
                    if (xTemp_CDate != null)
                    {
                        jsonParam.message = "Can't Edit, Account Already Closed...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    bool? IsBankCarriedForward = BASE._BankAccountDBOps.IsBankCarriedForward(xTemp_ID, xTemp_Year.ToString());
                    if (IsBankCarriedForward == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsBankCarriedForward == true && BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                    {
                        jsonParam.message = "Entry Cannot be edited . . !<br><br>This entry has been carried forward from previous year(s). Updation(Partial) can be done only after finalization of previous year accounts...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    object MaxValue = 0;
                    bool AllowUser = false;
                    MaxValue = BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string multiUserMsg = "";
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry can not be Edited / Deleted... !<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (value != (Common.Record_Status)MaxValue)
                    {
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            multiUserMsg = "<br><br>The Record has been locked in the background by another user.";
                        }
                        else if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                        {
                            multiUserMsg = "<br><br>The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "<br><br>Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (AllowUser)
                        {
                            jsonParam.message = multiUserMsg + "<br><br>Do You Want To continue...?";
                            jsonParam.title = "Confirmation...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (ActionMethod == "Delete")
                {
                    bool? IsBankCarriedForward = BASE._BankAccountDBOps.IsBankCarriedForward(xTemp_ID, BankData.YearID.ToString());
                    if (IsBankCarriedForward == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsBankCarriedForward == true && BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                    {
                        jsonParam.message = "Entry Cannot be Deleted . . !<br><br>This entry has been carried forward from previous year(s)...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xOpenActions > 0)
                    {
                        jsonParam.message = "Entry Cannot be Deleted . . !<br><br>There are Actions/Queries Posted Against It...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    object MaxValue = 0;
                    bool AllowUser = false;
                    bool DeleteAllow = true;
                    string UsedPage = "";
                    if (DeleteAllow)
                    {
                        MaxValue = BASE._BankAccountDBOps.GetFDCount(xTemp_ID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue > 0)
                        {
                            DeleteAllow = false;
                            UsedPage = "F.D. Information...";
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxValue = BASE._BankAccountDBOps.GetTransCount(xTemp_ID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue > 0)
                        {
                            DeleteAllow = false;
                            UsedPage = "Voucher Entry...";
                        }
                    }
                    if (DeleteAllow == false)
                    {
                        jsonParam.message = "Can't Delete...!<br><br>This Information Is being Used In Another Page...!<br><br>Name : " + UsedPage;
                        jsonParam.title = "Warning...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    MaxValue = 0;
                    MaxValue = BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string multiUserMsg = "";
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry can not be Edited / Deleted... !<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (value != (Common.Record_Status)MaxValue)
                    {
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            multiUserMsg = "<br><br>The Record has been locked in the background by another user.";
                        }
                        else if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                        {
                            multiUserMsg = "<br><br>The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "<br><br>Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (AllowUser)
                        {
                            jsonParam.message = multiUserMsg + "<br><br>Do You Want To continue...?";
                            jsonParam.title = "Confirmation...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (ActionMethod == "View")
                {
                    if (BASE._BankAccountDBOps.GetStatus(xTemp_ID) == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Frm_Bank_Window(string ActionMethod = null, string id = null, string BA_CLOSE_DATE = null)
        {
            Bank_user_rights();          
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (int i = 0; i < Rights.Length; i++)
            {
                if (ActionMethod == AM[i] && !CheckRights(BASE, ClientScreen.Profile_BankAccounts, Rights[i]))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Bank_Window','Not Allowed','No Rights');</script>");
                }
            }
            BankAccount model = new BankAccount();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;      
            model.ID = id;
            List<Rad_AccType_item> Rad_AccType_list = new List<Rad_AccType_item>();
            foreach (string _getname in Enum.GetNames(typeof(Common.Bank_Trans_Type)))
            {
                Rad_AccType_item row = new Rad_AccType_item();
                row.text = _getname;
                row.value = _getname;
                Rad_AccType_list.Add(row);
            }
            ViewBag.Rad_AccType_list = Rad_AccType_list;
            model.MsgBalance = "Balance as on "+ (Convert.ToDateTime(BASE._open_Year_Sdt).AddDays(-1)).ToString("dd MMMM, yyyy");
            model.Titlex_Bank = "Bank Account";
            model.SubTitlex_Bank="As on "+ (Convert.ToDateTime(BASE._open_Year_Sdt).AddDays(-1)).ToString("dd MMMM, yyyy");
            string message = "";
            if (model.TempActionMethod == "Edit" || model.TempActionMethod == "View" || model.TempActionMethod == "Delete")
            {               
                DataTable d1 = BASE._BankAccountDBOps.GetRecord(model.ID);
                if (d1 == null)
                {
                    message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    string viewstr = "";
                    if (model.TempActionMethod == "View")
                    {
                        viewstr = "view";
                    }
                    var Info_LastEditedOn = BankExportData.Where(x => x.ID == model.ID).FirstOrDefault().Edit_Date;
                    if (Info_LastEditedOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                    {
                        message = Messages.RecordChanged("Current BankAccount", viewstr);
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Record Already Changed!!');HidePopup('popup_frm_Bank_Window')</script>");
                    }
                }
                model.GLookUp_BranchList_Bank = d1.Rows[0]["BA_BRANCH_ID"].ToString();
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.YearID = d1.Rows[0]["BA_COD_YEAR_ID"].ToString();
                model.chk_Corpus_Bank = Convert.ToBoolean(d1.Rows[0]["CORPUS"]);
                model.chk_Reloadable_Card_Bank = Convert.ToBoolean(d1.Rows[0]["RELOAD"]);
                DataTable d2 = BASE._BankAccountDBOps.GetBranchDetailForID(model.GLookUp_BranchList_Bank);
                if (d2 == null)
                {
                    message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
                }
                if (d2.Rows[0]["BI_BANK_ID"].ToString().Length > 0)
                {
                    model.GLookUp_BankList_Bank = d2.Rows[0]["BI_BANK_ID"].ToString();
                }
                if (d2.Rows[0]["BB_BRANCH_ID"].ToString().Length > 0)
                {
                    model.GLookUp_BranchList_Bank = d2.Rows[0]["BB_BRANCH_ID"].ToString();
                }
                model.Txt_TAN_Bank= d1.Rows[0]["BA_TAN_NO"].ToString();
                model.Txt_TelNo_Bank= d1.Rows[0]["BA_TEL_NOS"].ToString();
                model.Txt_EmailID_Bank = d1.Rows[0]["BA_EMAIL_ID"].ToString();
                model.Rad_AccType_Bank= d1.Rows[0]["BA_ACCOUNT_TYPE"].ToString();
                model.Txt_No_Bank = d1.Rows[0]["BA_ACCOUNT_NO"].ToString();
                model.Txt_No_Tag = model.Txt_No_Bank;
                if (!Convert.IsDBNull(d1.Rows[0]["BA_ACCOUNT_NEW"]))
                {
                    if (d1.Rows[0]["BA_ACCOUNT_NEW"].ToString() == "YES")
                    {
                        model.Rad_AccKind_Bank = "NEW";
                    }
                    else
                    {
                        model.Rad_AccKind_Bank = "EXISTING";
                    }
                }
                else
                {
                    model.Rad_AccKind_Bank = "EXISTING";
                }
                if (!Convert.IsDBNull(d1.Rows[0]["BA_OPEN_DATE"]))
                {
                    model.Txt_Date_Bank = Convert.ToDateTime(d1.Rows[0]["BA_OPEN_DATE"]);
                }
                model.Txt_CustNo_Bank = d1.Rows[0]["BA_CUST_NO"].ToString();
                model.Txt_CustNo_Tag = model.Txt_CustNo_Bank;
                model.Txt_Remarks_Bank = d1.Rows[0]["BA_OTHER_DETAIL"].ToString().Trim();//Redmine Bug #132562 resolved
                model.Look_SignList1_Bank = d1.Rows[0]["BA_SIGN_AB_ID_1"].ToString();
                model.Look_SignList2_Bank = d1.Rows[0]["BA_SIGN_AB_ID_2"].ToString();
                model.Look_SignList3_Bank = d1.Rows[0]["BA_SIGN_AB_ID_3"].ToString();
                if (model.TempActionMethod != "View")
                {
                    model.Txt_Amount_Bank = Convert.ToDouble(BASE._BankAccountDBOps.GetBankOpeningBalance(model.ID).Rows[0]["OP_AMOUNT"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["BA_FERA_ACC"]))
                {
                    if (d1.Rows[0]["BA_FERA_ACC"].ToString() == "YES")
                    {
                        model.Chk_FERA_Bank = true;
                    }
                    else
                    {
                        model.Chk_FERA_Bank = false;
                    }
                }
                else
                {
                    model.Chk_FERA_Bank = false;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["BA_FCRA_UTIL"]))
                {
                    if ((bool)d1.Rows[0]["BA_FCRA_UTIL"] == true)
                    {
                        model.chk_FCRA_Util_Bank = true;
                    }
                    else
                    {
                        model.chk_FCRA_Util_Bank = false;
                    }
                }
                else
                {
                    model.chk_FCRA_Util_Bank = false;
                }
            }
            if (model.TempActionMethod == "Edit")
            {
                bool? IsBankCarriedForward = BASE._BankAccountDBOps.IsBankCarriedForward(model.ID, model.YearID);
                if (IsBankCarriedForward == null)
                {
                    message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
                }
                if (IsBankCarriedForward == false)
                {
                    DataTable Fds = BASE._BankAccountDBOps.GetFDIDsByAccID(model.ID);
                    if (Fds == null)
                    {
                        message = Messages.SomeError;
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
                    }
                    object Expenses_Income = BASE._BankAccountDBOps.GetTxnsCountByAccID(model.ID);
                    if (Expenses_Income == null)
                    {
                        message = Messages.SomeError;
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
                    }
                    if (Fds.Rows.Count > 0 || Convert.ToDouble(Expenses_Income) > 0)
                    {
                        model.Rad_AccType_readonly = true;
                        model.Rad_AccKind_readonly = true;
                        if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                        {
                            model.GLookUp_BankList_readonly = true;
                            model.GLookUp_BranchList_readonly = true;
                        }
                    }
                    if (BASE._Completed_Year_Count > 0)
                    {
                        model.Rad_AccType_readonly = true;
                        model.Rad_AccKind_readonly = true;
                    }
                }
                else
                {
                    if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                    {                      
                        model.GLookUp_BranchList_readonly = true;
                    }
                    model.GLookUp_BankList_readonly = true;
                    model.Rad_AccType_readonly = true;
                    model.Rad_AccKind_readonly = true;
                    model.Txt_Amount_readonly = true;
                }
            }
            if (model.TempActionMethod == "New")
            {
                model.Rad_AccType_Bank = Common.Bank_Trans_Type.SAVING.ToString().ToUpper();
                if (BASE._Completed_Year_Count > 0)
                {
                    model.Rad_AccKind_Bank = "NEW";
                    model.Rad_AccKind_readonly = true;
                }
                else
                {
                    if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                    {
                        model.Rad_AccKind_Bank = "NEW";
                        model.Rad_AccKind_readonly = true;
                    }
                }
            }
            if (model.TempActionMethod == "Edit")
            {
                if (BASE._Completed_Year_Count > 0)
                {
                    if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                    {                      
                        model.Rad_AccKind_readonly = true;
                    }
                }
            }
            DataTable CenTask = BASE._BankAccountDBOps.GetCenterTask();
            if (CenTask == null)
            {
                message = Messages.SomeError;
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('popup_frm_Bank_Window')</script>");
            }
            DataView DV1 = new DataView(CenTask);
            DV1.Sort = "TASK_NAME";
            if (DV1.Count > 0)
            {
                int index = DV1.Find("FOREIGN DONATION");
                if (index >= 0)
                {
                    if (DV1[index]["PERMISSION"].ToString().IndexOf("F") != -1)
                    {
                        model.Fera_Visibility = true;
                    }
                    else
                    {
                        model.Fera_Visibility = false;
                    }
                }
            }
            else
            {
                model.Fera_Visibility = false;
            }
            model.Txt_No_Bank = model.Txt_No_Bank.HandleEscapeCharacters();
            model.Txt_CustNo_Bank = model.Txt_CustNo_Bank.HandleEscapeCharacters();
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankacc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Frm_Bank_Window(BankAccount bankacc)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                bankacc.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + bankacc.TempActionMethod);
                if (BASE.AllowMultiuser())
                {
                    if (bankacc.ActionMethod == Common.Navigation_Mode._Delete || bankacc.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        DataTable bankaccount_DbOps = BASE._BankAccountDBOps.GetRecord(bankacc.ID);
                        if (bankaccount_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (bankaccount_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current BankAccount");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (bankacc.LastEditedOn != Convert.ToDateTime(bankaccount_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current BankAccount");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._BankAccountDBOps.GetStatus(bankacc.ID);
                        if ((int)MaxValue == 0)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            jsonParam.message = Messages.RecordChanged("Current BankAccount");
                            jsonParam.title = "Record Status Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (bankacc.ActionMethod == Common.Navigation_Mode._Delete)
                    {
                        var openActions = BASE._Action_Items_DBOps.GetOpenActions(bankacc.ID, "BANK_ACCOUNT_INFO");
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
                        if ((int)openActions > 0)
                        {
                            jsonParam.message = "Entry Cannot be Deleted..!<br><br>There are open actions / queries posted against it...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        bool DeleteAllow = true;
                        string UsedPage = "";
                        object MaxValue = 0;
                        if (DeleteAllow)
                        {
                            MaxValue = (Common.Record_Status)BASE._BankAccountDBOps.GetFDCount(bankacc.ID);
                            if (MaxValue == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((int)MaxValue > 0)
                            {
                                DeleteAllow = false;
                                UsedPage = "F.D. Information...";
                            }
                        }
                        if (DeleteAllow)
                        {
                            MaxValue = BASE._BankAccountDBOps.GetTransCount(bankacc.ID);
                            if (MaxValue == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((int)MaxValue > 0)
                            {
                                DeleteAllow = false;
                                UsedPage = "Voucher Entry...";
                            }
                        }
                        if (!DeleteAllow)
                        {
                            jsonParam.message = "Can't Delete<br><br>Some Chnages Have Been Made by Some Other User...!<br><br>Name: " + UsedPage;
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (bankacc.ActionMethod == Common.Navigation_Mode._New || bankacc.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(bankacc.Rad_AccType_Bank))
                    {
                        jsonParam.message = "Account Type not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rad_AccType_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (bankacc.Rad_AccKind_Bank == "NEW")
                    {
                        if (IsDate(bankacc.Txt_Date_Bank.ToString()) == false)
                        {
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Date_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (bankacc.Txt_Date_Bank > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Date Cannot Be Higher Than End Date Of Financial Year...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Date_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (IsDate(bankacc.Txt_Date_Bank.ToString()))
                        {
                            if (bankacc.Txt_Date_Bank >= BASE._open_Year_Sdt)
                            {
                                jsonParam.message = "Date Must Be Earlier Than Start Financial Year...!";
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Date_Bank";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(bankacc.GLookUp_BankList_Bank))
                    {
                        jsonParam.message = "Bank Name Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(bankacc.GLookUp_BranchList_Bank))
                    {
                        jsonParam.message = "Branch Name Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BranchList_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(bankacc.Look_SignList1_Bank))
                    {
                        jsonParam.message = "Signatory Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Look_SignList1_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (bankacc.Look_SignList1_Bank == bankacc.Look_SignList2_Bank || bankacc.Look_SignList1_Bank == bankacc.Look_SignList3_Bank)
                    {
                        jsonParam.message = "Signatories Must Be Different...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Look_SignList1_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((!string.IsNullOrWhiteSpace(bankacc.Look_SignList2_Bank)) && bankacc.Look_SignList2_Bank == bankacc.Look_SignList3_Bank)
                    {
                        jsonParam.message = "Signatories Must Be Different...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Look_SignList2_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrWhiteSpace(bankacc.Txt_EmailID_Bank))
                    {
                        if (BASE.IsEmail(bankacc.Txt_EmailID_Bank) == false)
                        {
                            jsonParam.message = "Email ID Incorrect...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_EmailID_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (bankacc.Rad_AccType_Bank != Common.Bank_Trans_Type.FD.ToString().ToUpper())
                    {
                        if (string.IsNullOrWhiteSpace(bankacc.Txt_No_Bank))
                        {
                            jsonParam.message = "Account No. Cannot Be Blank..!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_No_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(bankacc.Txt_CustNo_Bank))
                    {
                        jsonParam.message = "Customer No. Cannot Be Blank..!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_CustNo_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((bankacc.Txt_Amount_Bank == null || bankacc.Txt_Amount_Bank <= 0) && bankacc.Rad_AccType_Bank == Common.Bank_Trans_Type.SAVING.ToString().ToUpper() && bankacc.Rad_AccKind_Bank == "EXISTING")
                    {
                        jsonParam.message = "Amount Should be Greater Than Zero..!!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_Bank";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // Checking Duplicate Record....
                    object MaxValue = 0;
                    int xID = 0;
                    // for saving bank
                    if (bankacc.Rad_AccType_Bank == Common.Bank_Trans_Type.SAVING.ToString().ToUpper())
                    {
                        MaxValue = BASE._BankAccountDBOps.GetCountByAccountNo(bankacc.Txt_No_Bank);
                        if ((MaxValue == null))
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue != 0 && bankacc.ActionMethod == Common.Navigation_Mode._New)
                        {
                            jsonParam.message = "Same Account No. Already Available...!";
                            jsonParam.title = "Duplicate...(" + bankacc.Txt_No_Bank + ")";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_No_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if ((int)MaxValue != 0 && bankacc.ActionMethod == Common.Navigation_Mode._Edit)
                        {
                            if (bankacc.Txt_No_Bank != bankacc.Txt_No_Tag)
                            {
                                jsonParam.message = "Same Account No. Already Available...!";
                                jsonParam.title = "Duplicate...(" + bankacc.Txt_No_Bank + ")";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_No_Bank";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        //  for fd bank
                        MaxValue = BASE._BankAccountDBOps.GetCountByCustomerNo(bankacc.Txt_CustNo_Bank);
                        if (MaxValue == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue != 0 && bankacc.ActionMethod == Common.Navigation_Mode._New)
                        {
                            jsonParam.message = "Same Customer No. Already Available...!";
                            jsonParam.title = "Duplicate...(" + bankacc.Txt_CustNo_Bank + ")";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_CustNo_Bank";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if ((int)MaxValue != 0 && bankacc.ActionMethod == Common.Navigation_Mode._Edit)
                        {
                            if (bankacc.Txt_CustNo_Bank != bankacc.Txt_CustNo_Tag)
                            {
                                jsonParam.message = "Same Account No. Already Available...!";
                                jsonParam.title = "Duplicate...(" + bankacc.Txt_CustNo_Bank + ")";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_CustNo_Bank";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                // -------------------------// Dependencies //-----------------------------
                if (BASE.AllowMultiuser())
                {
                    if (bankacc.ActionMethod == Common.Navigation_Mode._New || bankacc.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        DateTime? oldEditOn;
                        DateTime? NewEditOn;
                        // Address book(signatories) dependency checks #Ref z8,z9
                        // Sig 1
                        if (!string.IsNullOrEmpty(bankacc.Look_SignList1_Bank))
                        {
                            DataTable d1 = BASE._BankAccountDBOps.GetSignatories(bankacc.Look_SignList1_Bank);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = bankacc.SignList1_REC_EDIT_ON;
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NewEditOn = Convert.ToDateTime(d1.Rows[0][3]);
                                if (NewEditOn != oldEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        // Sif2
                        if (!string.IsNullOrEmpty(bankacc.Look_SignList2_Bank))
                        {
                            DataTable d1 = BASE._BankAccountDBOps.GetSignatories(bankacc.Look_SignList2_Bank);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = bankacc.SignList2_REC_EDIT_ON;
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NewEditOn = Convert.ToDateTime(d1.Rows[0][3]);
                                if (NewEditOn != oldEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        // Sig3
                        if (!string.IsNullOrEmpty(bankacc.Look_SignList3_Bank))
                        {
                            DataTable d1 = BASE._BankAccountDBOps.GetSignatories(bankacc.Look_SignList3_Bank);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = bankacc.SignList3_REC_EDIT_ON;
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NewEditOn = Convert.ToDateTime(d1.Rows[0][3]);
                                if (NewEditOn != oldEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book(signatories)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                string Status_Action = "";
                Status_Action = ((int)Common.Record_Status._Completed).ToString();
                if (bankacc.ActionMethod.ToString() == "_Delete")
                {
                    Status_Action = ((int)Common.Record_Status._Deleted).ToString();
                }
                if (bankacc.ActionMethod == Common.Navigation_Mode._New)
                {
                    bool Result = true;
                    if (bankacc.Rad_AccKind_Bank == "NEW")
                    {
                        bankacc.Rad_AccKind_Bank = "'YES'";
                    }
                    else
                    {
                        bankacc.Rad_AccKind_Bank = " NULL ";
                    }
                    bankacc.ID = Guid.NewGuid().ToString();
                    // Insert bank and balance 
                    Parameter_Insert_BankandBalance_BankAccounts InParam = new Parameter_Insert_BankandBalance_BankAccounts();
                    InParam.BranchID = bankacc.GLookUp_BranchList_Bank;
                    InParam.TanNo = bankacc.Txt_TAN_Bank;
                    InParam.AccountType = bankacc.Rad_AccType_Bank;
                    InParam.AccountNew = bankacc.Rad_AccKind_Bank;
                    if (IsDate(bankacc.Txt_Date_Bank.ToString()))
                    {
                        InParam.OpeningDate = Convert.ToDateTime(bankacc.Txt_Date_Bank).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.AccountNo = bankacc.Txt_No_Bank ?? "";
                    InParam.CustNo = bankacc.Txt_CustNo_Bank ?? "";
                    InParam.FeraAccount = bankacc.Chk_FERA_Bank ? "YES" : "NO";
                    InParam.FCRA_Utility_Account = bankacc.chk_FCRA_Util_Bank;
                    InParam.TelNo = bankacc.Txt_TelNo_Bank ?? "";
                    InParam.EmailID = bankacc.Txt_EmailID_Bank ?? "";
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList1_Bank))
                    {
                        InParam.Sign1ABID = bankacc.Look_SignList1_Bank;
                    }
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList2_Bank))
                    {
                        InParam.Sign2ABID = bankacc.Look_SignList2_Bank;
                    }
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList3_Bank))
                    {
                        InParam.Sign3ABID = bankacc.Look_SignList3_Bank;
                    }
                    InParam.OtherDetails = bankacc.Txt_Remarks_Bank==null?"": bankacc.Txt_Remarks_Bank.Trim();//Redmine Bug #132562 resolved
                    InParam.Status_Action = Status_Action;
                    InParam.Amount = Convert.ToDouble(bankacc.Txt_Amount_Bank);
                    InParam.RecID = bankacc.ID;
                    InParam.Corpus_Account = bankacc.chk_Corpus_Bank;
                    InParam.Reloadable_Card = bankacc.chk_Reloadable_Card_Bank;
                    if (!BASE._BankAccountDBOps.Insert_Bank_and_Balance(InParam))
                    {
                        Result = false;
                    }
                    if (Result)
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = bankacc.Titlex_Bank;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid= bankacc.ID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (bankacc.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    // edit
                    bool Result = true;
                    if (bankacc.Rad_AccKind_Bank == "NEW")
                    {
                        bankacc.Rad_AccKind_Bank = "'YES'"; //Bug #132530 fixed
                    }
                    else
                    {
                        bankacc.Rad_AccKind_Bank = " NULL ";
                    }
                    Parameter_Update_BankandBalance_BankAccounts UpParam = new Parameter_Update_BankandBalance_BankAccounts();
                    UpParam.BranchID = bankacc.GLookUp_BranchList_Bank;
                    UpParam.TanNo = bankacc.Txt_TAN_Bank;
                    UpParam.AccountType = bankacc.Rad_AccType_Bank;
                    UpParam.AccountNew = bankacc.Rad_AccKind_Bank;
                    if (IsDate(bankacc.Txt_Date_Bank.ToString()))
                    {
                        UpParam.OpeningDate = Convert.ToDateTime(bankacc.Txt_Date_Bank).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.AccountNo = bankacc.Txt_No_Bank ?? "";
                    UpParam.CustNo = bankacc.Txt_CustNo_Bank ?? "";
                    UpParam.FeraAccount = bankacc.Chk_FERA_Bank ? "YES" : "NO";
                    UpParam.FCRA_Utility_Account = bankacc.chk_FCRA_Util_Bank;
                    UpParam.TelNo = bankacc.Txt_TelNo_Bank ?? "";
                    UpParam.EmailID = bankacc.Txt_EmailID_Bank ?? "";
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList1_Bank))
                    {
                        UpParam.Sign1ABID = bankacc.Look_SignList1_Bank;
                    }
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList2_Bank))
                    {
                        UpParam.Sign2ABID = bankacc.Look_SignList2_Bank;
                    }
                    if (!string.IsNullOrEmpty(bankacc.Look_SignList3_Bank))
                    {
                        UpParam.Sign3ABID = bankacc.Look_SignList3_Bank;
                    }
                    UpParam.OtherDetails = bankacc.Txt_Remarks_Bank==null?"": bankacc.Txt_Remarks_Bank.Trim();//Redmine Bug #132562 resolved
                    UpParam.Amount = Convert.ToDouble(bankacc.Txt_Amount_Bank);
                    UpParam.Rec_ID = bankacc.ID;
                    UpParam.Corpus_Account = bankacc.chk_Corpus_Bank;
                    UpParam.Reloadable_Card = bankacc.chk_Reloadable_Card_Bank;
                    if (!BASE._BankAccountDBOps.Update_Bank_and_Balance(UpParam))
                    {
                        Result = false;
                    }
                    if (Result)
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = bankacc.Titlex_Bank;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (bankacc.ActionMethod == Common.Navigation_Mode._Delete)
                {
                    if (!BASE._BankAccountDBOps.Delete_and_Remove_Balance(bankacc.ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = bankacc.Titlex_Bank;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

            #region Lock/Unlock functionality
        [HttpPost]
        public JsonResult DataNavigation(string ActionMethod, string id, string[] Selectedid,string GridToBeRefreshed= "BankListGrid")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod == "LOCKED" || ActionMethod == "UNLOCKED" || ActionMethod == "PRINT-LIST")
                    {
                        var BankData = BankExportData.Where(x => x.ID == id).FirstOrDefault();
                        DataTable d1 = BASE._BankAccountDBOps.GetRecord(id);
                        if (d1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current BankAccount");
                            jsonParam.title = "Record Changed/Removed In Background!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        if (RecEdit_Date != BankData.Edit_Date)
                        {
                            jsonParam.message = Messages.RecordChanged("Current BankAccount");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (ActionMethod == "LOCKED")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock))
                    {
                        //for (int i = 0; i < Selectedid.Length; i++)
                        //{
                            string xTemp_ID = id;
                            var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                            string xTemp_Year = BankData.YearID.ToString();
                            object MaxValue = 0;
                            bool AllowUser = false;
                            MaxValue = BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                            var xStatus = BankData.Action_Status;
                            var value = (Common.Record_Status)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                            object xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Tables.BANK_ACCOUNT_INFO, xTemp_ID);
                            string Msg = "";
                            if (value != (Common.Record_Status)MaxValue)
                            {
                                Msg = "Record Status has been changed in the background by another user";
                                if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                                {
                                    AllowUser = true;
                                }
                            }
                            else
                            {
                                Msg = "Information...";
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                            {
                                jsonParam.message = "Already locked Entries can't be Re-locked...!<br><br>Please unselect already locked Entries...!";
                                jsonParam.title = Msg;
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Incomplete)
                            {
                                jsonParam.message = "Incomplete Entries can't be locked...!<br><br>Please unselect incomplete Entries or ask Center to Complete it...!";
                                jsonParam.title = Msg;
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (xRemarks != null && !Convert.IsDBNull(xRemarks))
                            {
                                if ((int)MaxValue > 0)
                                {
                                    jsonParam.message = "Entries with pending queries can't be Locked...!<br><br>Please unselect such entries...!";
                                    jsonParam.title = Msg;
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (AllowUser == true)
                            {
                                jsonParam.message = "The Record has been Unlocked in the background by another user<br><br>Do you want to continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        //}
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = "Not Allowed";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "UNLOCKED")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock))
                    {
                        //for (int i = 0; i < Selectedid.Length; i++)
                        //{
                            string xTemp_ID = id;
                            var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                            var xStatus = BankData.Action_Status;
                            var value = (Common.Record_Status)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                            object MaxValue = 0;
                            MaxValue = BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                            bool AllowUser = false;
                            string Msg = "";
                            if (value != (Common.Record_Status)MaxValue)
                            {
                                Msg = "Record Status has been changed in the background by another user";
                                if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                                {
                                    AllowUser = true;
                                }
                            }
                            else
                            {
                                Msg = "Information...";
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                            {
                                jsonParam.message = "Already Unlocked Entries can't be Re-Unlocked...!<br><br>Please unselect already unlocked Entries...!";
                                jsonParam.title = Msg;
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Incomplete)
                            {
                                jsonParam.message = "Incomplete Entries can't be Unlocked...!<br><br>Please unselect incomplete Entries or ask Center to Complete it...!";
                                jsonParam.title = Msg;
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (AllowUser == true)
                            {
                                jsonParam.message = "The Record has been Locked in the background by another user<br><br>Do you want to continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        //}
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = "Not Allowed";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "ACCOUNT-REOPEN")
                {
                    string xTemp_ID = id;
                    var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                    string xTemp_Name = BankData.Name;
                    string xTemp_Branch = BankData.Branch;
                    string xTemp_Type = "Account Type: " + BankData.BA_ACCOUNT_TYPE;
                    DateTime? xTemp_CDate = BankData.BA_CLOSE_DATE;
                    string xTemp_Year = BankData.YearID.ToString();
                    if (xTemp_CDate == null)
                    {
                        jsonParam.message = "Account Already Opened...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string xTemp_AccNo = "";
                    if (BankData.BA_ACCOUNT_TYPE.ToUpper().Trim() == Common.Bank_Trans_Type.SAVING.ToString().ToUpper())
                    {
                        xTemp_AccNo = "Account No.: " + BankData.BA_ACCOUNT_NO;
                    }
                    else
                    {
                        xTemp_AccNo = "Customer No.: " + BankData.BA_CUST_NO;
                    }
                    object MaxValue = 0;
                    MaxValue = BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry cannot be changed...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (ActionMethod == "ACCOUNT-CLOSE")
                {
                    var xTemp_ID = id;
                    var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                    if (BankData.BA_ACCOUNT_TYPE.Trim() == "FD")
                    {
                        jsonParam.message = "FD Bank Account cannot be closed...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    object xBalance = Get_Bank_Closing_Balance(xTemp_ID);
                    string xTemp_Name = BankData.Name;
                    string xTemp_Branch = BankData.Branch;
                    string xTemp_Type = "Account Type: " + BankData.BA_ACCOUNT_TYPE;
                    string xTemp_Year = BankData.YearID.ToString();
                    string xTemp_AccNo = "";
                    DateTime? xTemp_CDate = BankData.BA_CLOSE_DATE;

                    if (xTemp_CDate != null)
                    {
                        jsonParam.message = "Account Already Closed...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BankData.BA_ACCOUNT_TYPE.ToUpper().Trim() == Common.Bank_Trans_Type.SAVING.ToString().ToUpper())
                    {
                        xTemp_AccNo = "Account No.: " + BankData.BA_ACCOUNT_NO;
                    }
                    else
                    {
                        xTemp_AccNo = "Customer No.: " + BankData.BA_CUST_NO;
                    }
                    object MaxValue = 0;
                    MaxValue = (Common.Record_Status)BASE._BankAccountDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry cannot be changed...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (xBalance == null)
                    {
                        jsonParam.message = "Account Not Listed...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if ((double)xBalance < 0 || (double)xBalance > 0)
                    {
                        jsonParam.message = "This Bank Account cannot be closed...!<br><br>Closing Balance must be Nil...!<br><br>Current Closing Balance: " + Convert.ToDouble(xBalance).ToString("#,0.00");
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (ActionMethod == "REMARKS")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.View_Remarks))
                    {
                        string xTemp_ID = id;
                        var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                        string xStatus = BankData.Action_Status;
                        jsonParam.result = true;
                        jsonParam.popup_title = "Audit Actions";
                        jsonParam.popup_form_name = "Frm_Action_Items_Info";
                        jsonParam.popup_form_path = "/Help/ActionItems/Frm_Action_Items_Info/";
                        jsonParam.popup_querystring = "RefScreen=" + "Profile_BankAccounts" + "&RefTable=" + "BANK_ACCOUNT_INFO" + "&RefRecID=" + xTemp_ID + "&Status=" + xStatus + "&PopupID=" + "BankPrf_ActionItem_Popup";
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }     
                }
                if (ActionMethod == "ADD_REMARKS")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Manage_Remarks))
                    {
                        string xTemp_ID = id;
                        var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                        string xStatus = BankData.Action_Status;
                        int xTemp_Year = BankData.YearID;
                        if (xTemp_Year != BASE._open_Year_ID)
                        {
                            jsonParam.result = false;
                            jsonParam.message = "Please Open Previous Year Data To Add Queries";
                            jsonParam.title = "Information...";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (xStatus.ToUpper() != "LOCKED")
                        {
                            jsonParam.result = true;
                            jsonParam.popup_title = "New ~ Action";
                            jsonParam.popup_form_name = "Frm_Action_Items_Window";
                            jsonParam.popup_form_path = "/Help/ActionItems/Frm_Action_Items_Window/";
                            jsonParam.popup_querystring = "ActionMethod=New" + "&RefScreen=" + "Profile_BankAccounts" + "&RefTable=" + "BANK_ACCOUNT_INFO" + "&RefRecID=" + xTemp_ID + "&PopupID=" + "BankPrf_ActionItem_Popup" + "&GridToBeRefreshed=" + GridToBeRefreshed;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            jsonParam.result = false;
                            jsonParam.message = "Queries Can't Be Added To Freezed Records.";
                            jsonParam.title = "Information...";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult BankUnlock(string Selectedid)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                int ctr = 0;
                //for (int i = 0; i < Selectedid.Length; i++)
                //{
                    string xTemp_ID = Selectedid;
                    var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                    var xTemp_Year = BankData.YearID;
                    ctr += 1;
                    if (!BASE._BankAccountDBOps.MarkAsComplete(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                //}
                if (ctr > 0)
                {
                    jsonParam.message = Messages.UnlockedSuccess(ctr);
                    jsonParam.title = "Locked...";
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam,
                        xid=xTemp_ID
                    }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Banklock(string Selectedid)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                int ctr = 0;
                //for (int i = 0; i < Selectedid.Length; i++)
                //{
                    string xTemp_ID = Selectedid;
                    var BankData = BankExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                    ctr += 1;
                    if (!BASE._BankAccountDBOps.MarkAsLocked(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                //}
                if (ctr > 0)
                {
                    jsonParam.message = Messages.LockedSuccess(ctr);
                    jsonParam.title = "Locked...";
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = xTemp_ID
                    }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Submit Bank Account Details
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }

        public object Get_Bank_Closing_Balance(string xAccount_Rec_ID)
        {

            DataTable Bank_Det = BASE._Voucher_DBOps.GetBankBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Edt, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Det == null)
            {
                return null;
            }
            DataRow[] rows = Bank_Det.Select("ID = '" + xAccount_Rec_ID + "'");
            if (rows.Count() > 0)
            {
                return Convert.ToDouble(rows[0]["CLOSING"].ToString());
            }
            else
            {
                return null;
            }
        }

        protected bool CheckDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DateTime? Check_Transaction(DateTime xDate, string xAccID)
        {
            DateTime? Last_Tr_Date = null;
            object MaxValue = null;
            MaxValue = BASE._BankAccountDBOps.GetTransactionMaxDate(xAccID, xDate);
            if (MaxValue == null || Convert.IsDBNull(MaxValue))
            {
                return Last_Tr_Date;
            }
            if (CheckDate(MaxValue.ToString()))//Redmine Bug #133246 fixed
            {
                Last_Tr_Date = (DateTime)MaxValue;
            }
            return Last_Tr_Date;
        }

        #endregion

        #region "Start--> LookupEdit Events"
        public ActionResult RefreshBankList()
        {
            DataTable Banks = BASE._BankAccountDBOps.GetBankList();
            if (Banks == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            BankDDList = DatatableToModel.DataTabletoBANK_INFO(Banks);
            BankDDList = BankDDList.OrderBy(x => x.BI_BANK_NAME).ToList();
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetBankList(DataSourceLoadOptions loadOptions)
        {
            if (BankDDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<BANK_INFO>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BankDDList, loadOptions)), "application/json");
        }
        public ActionResult RefreshBranchList(string bankID)
        {
            DataTable branchlist = (DataTable)BASE._BankAccountDBOps.GetBranchesForBank(bankID);
            if (branchlist == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            BranchDDList = DatatableToModel.DataTabletoBANK_BRANCH_INFO(branchlist);
            BranchDDList = BranchDDList.OrderBy(x => x.BB_BRANCH_NAME).ToList();
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetBranchList(DataSourceLoadOptions loadOptions)
        {
            if (BranchDDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<BANK_BRANCH_INFO>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BranchDDList, loadOptions)), "application/json");
        }
        public ActionResult RefreshSignList()
        {
            DataTable signatories = BASE._BankAccountDBOps.GetSignatories() as DataTable;
            if (signatories == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            DataView dview = new DataView(signatories);
            dview.Sort = "Name";
            SignDDList = DatatableToModel.DataTabletoADDRESS_BOOK(dview.ToTable());
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LookUp_Sign1List(DataSourceLoadOptions loadOptions)
        {
            if (SignDDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<ADDRESS_BOOK>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SignDDList, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult LookUp_Sign2List(DataSourceLoadOptions loadOptions)
        {
            if (SignDDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<ADDRESS_BOOK>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SignDDList, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult LookUp_Sign3List(DataSourceLoadOptions loadOptions)
        {
            if (SignDDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<ADDRESS_BOOK>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SignDDList, loadOptions)), "application/json");
        }

        #endregion

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Bank_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_BankInfo");
            Session.Remove("BankInfo_detailGrid_Data");
        }
        public void SessionClear_Window()
        {
            ClearBaseSession("_BankWindow");
        }
        public void Bank_user_rights()
        {
            ViewData["Bank_AddRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Add");
            ViewData["Bank_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Update");
            ViewData["Bank_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "View");
            ViewData["Bank_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Delete");
            ViewData["Bank_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Export");
            ViewData["Bank_ListRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "List");
            ViewData["Bank_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["Bank_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_BankAccounts, Common.ClientAction.Lock_Unlock);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }  

    }
    public class Rad_AccType_item
    {
        public string text { get; set; }
        public string value { get; set; }
    }
    
}