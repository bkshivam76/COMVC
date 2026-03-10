using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class InternalTransferController : BaseController
    {
        #region "Start--> Default Variables"    

        public string xId
        {
            get { return (string)GetBaseSession("xId_ITVou"); }
            set { SetBaseSession("xId_ITVou", value); }
        }
        public List<Out_TDS> TDS_Deduction_List
        {
            get { return (List<Out_TDS>)GetBaseSession("Closed_Bank_Account_No_ITVou"); }
            set { SetBaseSession("Closed_Bank_Account_No_ITVou", value); }
        }
        public string Closed_Bank_Account_No
        {
            get { return (string)GetBaseSession("Closed_Bank_Account_No_ITVou"); }
            set { SetBaseSession("Closed_Bank_Account_No_ITVou", value); }
        }
        public List<Return_InternalTransferRegister_Posted> ITR_GridView1
        {
            get { return (List<Return_InternalTransferRegister_Posted>)GetBaseSession("ITR_GridView1_ITReg"); }
            set { SetBaseSession("ITR_GridView1_ITReg", value); }
        }
        public List<Return_InternalTransferRegister_Pending> ITR_GridView2
        {
            get { return (List<Return_InternalTransferRegister_Pending>)GetBaseSession("ITR_GridView2_ITReg"); }
            set { SetBaseSession("ITR_GridView2_ITReg", value); }
        }
        public string HQ_IDs
        {
            get { return (string)GetBaseSession("HQ_IDs_ITVou"); }
            set { SetBaseSession("HQ_IDs_ITVou", value); }
        }
        public List<InternalTransferFromCenterList> ITV_FrCenList
        {
            get { return (List<InternalTransferFromCenterList>)GetBaseSession("ITV_FrCenList_ITVou"); }
            set { SetBaseSession("ITV_FrCenList_ITVou", value); }
        }
        public List<InternalTransferToCenterList> ITV_ToCenList
        {
            get { return (List<InternalTransferToCenterList>)GetBaseSession("ITV_ToCenList_ITVou"); }
            set { SetBaseSession("ITV_ToCenList_ITVou", value); }
        }
        public List<InternalTransferItemList> ITV_ItemList
        {
            get { return (List<InternalTransferItemList>)GetBaseSession("ITV_ItemList_ITVou"); }
            set { SetBaseSession("ITV_ItemList_ITVou", value); }
        }
        public List<InternalTransferTrfBankList> ITV_TrfBankList
        {
            get { return (List<InternalTransferTrfBankList>)GetBaseSession("ITV_TrfBankList_ITVou"); }
            set { SetBaseSession("ITV_TrfBankList_ITVou", value); }
        }
        public List<BankList> ITV_BankList
        {
            get { return (List<BankList>)GetBaseSession("ITV_BankList_ITVou"); }
            set { SetBaseSession("ITV_BankList_ITVou", value); }
        }
        public List<PendingInternalTransferInfo> IT_Pending_data
        {
            get { return (List<PendingInternalTransferInfo>)GetBaseSession("IT_Pending_data_ITVPending"); }
            set { SetBaseSession("IT_Pending_data_ITVPending", value); }
        }
        public List<InternalTransferTdsSentInfo> TdsSentInternalTransfer_ExportData
        {
            get { return (List<InternalTransferTdsSentInfo>)GetBaseSession("TdsSentInternalTransfer_ExportData_ITVTds"); }
            set { SetBaseSession("TdsSentInternalTransfer_ExportData_ITVTds", value); }
        }
        public List<InternalTransfer_Matching> InternalTransfer_MatchingGrid_Data
        {
            get { return (List<InternalTransfer_Matching>)GetBaseSession("InternalTransfer_MatchingGrid_Data_ITVMatch"); }
            set { SetBaseSession("InternalTransfer_MatchingGrid_Data_ITVMatch", value); }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> ITR_Posted_DetailGrid_Data
        {
            get { return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("ITR_Posted_DetailGrid_Data_ITReg"); }
            set { SetBaseSession("ITR_Posted_DetailGrid_Data_ITReg", value); }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> ITR_Posted_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("ITR_Posted_AdditionalInfoGrid_ITReg");
            }
            set
            {
                SetBaseSession("ITR_Posted_AdditionalInfoGrid_ITReg", value);
            }
        }
        #endregion

        #region <--RegisterGrid-->
        public ActionResult Frm_I_Transfer_Reg()
        {
            if (CheckRights(BASE, ClientScreen.Account_InternalTrf_Register, "List"))
            {
                InternalTransferRegister model = new InternalTransferRegister();
                model.xID = xId;
                ViewBag.IsVolumeCentre = BASE._IsVolumeCenter;
                ViewBag.UserType = BASE._open_User_Type;
                ViewBag.IsunmatchTransfer = false;
                if (BASE._IsVolumeCenter)
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) || BASE._open_Cen_ID == 4216)
                    {
                        ViewBag.IsunmatchTransfer = true;
                    }
                }
                List<Return_InternalTransferRegister_Posted> P1 = BASE._Internal_Tf_Voucher_DBOps.GetList();
                if (P1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');$('.TabPanalMenu - tabs.active span').click()</script>");
                }
                else
                {
                    model.GridView1 = P1;
                    ITR_GridView1 = P1;
                }
                int RowCount = 0;
                DataSet dSet = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(RowCount, null);
                DataTable P2 = dSet.Tables[0];
                if (P2 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');$('.TabPanalMenu - tabs.active span').click()</script>");
                }
                else
                {
                    model.GridView2 = ConvertToList(P2);
                    ITR_GridView2 = model.GridView2;
                }
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
                ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
                ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
                ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
                ViewData["IntrnlTranfrReg_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View(model);
            }
            else 
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Frm_I_Transfer_Reg_Posted_Grid(string command, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",string RowKeyToFocus = "")
        {
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.IsVolumeCentre = BASE._IsVolumeCenter;
            ViewBag.IsunmatchTransfer= false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            if (BASE._IsVolumeCenter)
            {
                if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) || BASE._open_Cen_ID == 4216)
                {
                    ViewBag.IsunmatchTransfer = true;
                }
            }     
            if (ITR_GridView1 == null || command == "REFRESH")
            {
                List<Return_InternalTransferRegister_Posted> P1 = BASE._Internal_Tf_Voucher_DBOps.GetList();
                ITR_GridView1 = P1;
            }
            List<Return_InternalTransferRegister_Posted> Final_Data = ITR_GridView1;

            return View(Final_Data);
        }
        public ActionResult Frm_I_Transfer_Reg_Posted_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, string MID = null, bool VouchingMode = false)
        {
            ViewBag.ITR_Posted_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ITR_Posted_RecID = RecID;
            ViewBag.ITR_Posted_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Accounts_CashBook);
                    ITR_Posted_DetailGrid_Data = _docList;
                    Session["ITR_Posted_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook);
                    ITR_Posted_DetailGrid_Data = data.DocumentMapping;
                    Session["ITR_Posted_detailGrid_Data"] = data.DocumentMapping;
                    ITR_Posted_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(ITR_Posted_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(ITR_Posted_AdditionalInfoGrid);
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
            settings.Name = "ITR_PostedGrid" + RecID;
            settings.SettingsDetail.MasterGridName = "ITR_PostedGrid";
            
            settings.Width = Unit.Percentage(100);
            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["ITR_Posted_DetailGrid_Data"];                        
        }
        public ActionResult Frm_I_Transfer_Reg_Pending_Grid(string command, bool VouchingMode = false)
        {
            ViewBag.IsVolumeCentre = BASE._IsVolumeCenter;
            ViewBag.IsunmatchTransfer = false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.VouchingMode = VouchingMode;
            if (BASE._IsVolumeCenter)
            {
                if (BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock) || BASE._open_Cen_ID == 4216)
                {
                    ViewBag.IsunmatchTransfer = true;
                }
            }
            if (ITR_GridView2 == null || command == "REFRESH")
            {
                DataSet dSet = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, null);
                DataTable P2 = dSet.Tables[0];
                ITR_GridView2 = ConvertToList(P2);
            }
            List<Return_InternalTransferRegister_Pending> Final_Data = ITR_GridView2;

            return View(Final_Data);
        }        
        public ActionResult PostedGridCustomDataAction(string key)
        {
            var Final_Data = ITR_GridView1;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.ActionBy + "![" + it.ActionDate + "![" + it.ActionStatus + "![" + it.AddBy + "![" + it.AddDate + "![" + it.EditBy + "![" + it.EditDate + "![" + it.ID + "![" + it.MID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult PostedDetailGridCustomDataAction(string key)
        {
            var Final_Data = ITR_Posted_DetailGrid_Data;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.UniqueID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Doc_Status + "![" + it.Params_Mandatory + "![" + it.LABEL_FROM_DATE + "![" + it.LABEL_TO_DATE + "![" + it.LABEL_DESCRIPTION + "![" + it.Document_Category + "![" + it.Document_ID + "![" + it.ATTACH_ID + "![" + it.TxnID + "![" + it.TxnMID + "![" + it.MAP_ID + "![" + it.Reason + "![" + it.ATTACH_FILE_NAME + "![" + it.Attachment_Action_Status + "![" + it.UniqueID + "![" + it.ReasonID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);           
        }
        public ActionResult PendingGridCustomDataAction(string key)
        {
            var Final_Data = ITR_GridView2;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.ActionBy + "![" + it.ActionDate + "![" + it.ActionStatus + "![" + it.AddBy + "![" + it.AddDate + "![" + it.EditBy + "![" + it.EditDate + "![" + it.ID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Frm_Export_Options(int SelectedIndex)
        {
            string view = SelectedIndex == 0 ? "Frm_Export_Options_ITR_Posted" : "Frm_Export_Options_ITR_Pending";
            return View(view);
        }
        public void SessionClear_REG()
        {
            ClearBaseSession("_ITReg");
            Session.Remove("ITR_Posted_DetailGrid_Data");            
        }
        #endregion
        public ActionResult MultiUser_ITR_Confirm(string Posted_Id = null)
        {
            var Grid1_Data = ITR_GridView1.Where(x => x.ID == Posted_Id).First();
            if (Posted_Id != null)
            {
                string xTemp_ID = Posted_Id;
                string xTemp_MID = Grid1_Data.MID;
                string xMaster_ID = "";
                if (xTemp_MID.Length > 0)
                {
                    xMaster_ID = xTemp_MID;
                }
                else
                {
                    xMaster_ID = xTemp_ID;
                }
                int xRec_Status = 0;
                string multiUserMsg = "";
                bool AllowUser = false;
                var Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                if (Status == null)
                {
                    return Json(new
                    {
                        message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Status.Rows.Count > 0)
                {
                    xRec_Status = (int)Status.Rows[0]["REC_STATUS"];
                    string xStatus = Grid1_Data.ActionStatus;
                    var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                    if (value != xRec_Status)
                    {
                        if (xRec_Status == (int)Common.Record_Status._Locked)
                        {
                            multiUserMsg = "<br><br>The Record has been locked in the background by another user.";
                        }
                        else if (xRec_Status == (int)Common_Lib.Common.Record_Status._Completed)
                        {
                            multiUserMsg = "<br><br>The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "<br><br>The Record Status has been Changed in the background by another user.";
                            AllowUser = true;
                        }
                        if (AllowUser)
                        {
                            return Json(new
                            {
                                message = multiUserMsg + "<br><br> Do You Want To Continue..?",
                                result = "MultiUser_Confirm"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "Posted Internal Transfer Entry Not Selected...!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Checks_Edit_DeleteClick_ITR(string Posted_Id = null)
        {
            var Grid1_Data = ITR_GridView1.Where(x => x.ID == Posted_Id).First();

            string xTemp_ID = Posted_Id;
            string xTemp_MID = Grid1_Data.MID;
            string xMaster_ID = "";
            if (xTemp_MID.Length > 0)
            {
                xMaster_ID = xTemp_MID;
            }
            else
            {
                xMaster_ID = xTemp_ID;
            }
            bool isRecChanged = false;
            if (!string.IsNullOrEmpty(xTemp_ID))
            {
                bool Entry_Found = false;
                int xRec_Status = 0;
                string xTR_CODE = "";
                string xTemp_D_Status = "";
                string xCross_Ref_Id = "";
                string multiUserMsg = "";
                bool AllowUser = false;
                var Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                if (Status.Rows.Count > 0)
                {
                    Entry_Found = true;
                    xRec_Status = (int)Status.Rows[0]["REC_STATUS"];
                    xTR_CODE = Status.Rows[0]["TR_CODE"].ToString();
                    foreach (DataRow cRow in Status.Rows)
                    {
                        if (!Convert.IsDBNull(cRow["TR_TRF_CROSS_REF_ID"]))
                        {
                            xCross_Ref_Id = cRow["TR_TRF_CROSS_REF_ID"].ToString();
                        }
                        if (xCross_Ref_Id.Length > 0)
                        {
                            break;
                        }
                    }
                    string xStatus = Grid1_Data.ActionStatus;
                    var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                    if (value != xRec_Status)
                    {
                        if (xRec_Status == (int)Common.Record_Status._Locked)
                        {
                            multiUserMsg = "<br><br>The Record has been locked in the background by another user.";
                        }
                        else if (xRec_Status == (int)Common_Lib.Common.Record_Status._Completed)
                        {
                            multiUserMsg = "<br><br>The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "<br><br>The Record Status has been Changed in the background by another user.";
                            AllowUser = true;
                        }
                    }
                }
                if (Entry_Found == false)
                {
                    return Json(new
                    {
                        message = "Entry Not Found Or Changed In Background..!!",
                        refreshGrid = true,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if (xRec_Status == (int)Common_Lib.Common.Record_Status._Locked)
                {
                    return Json(new
                    {
                        message = "Locked Entry Cannot Be Edited/Deleted...!" + multiUserMsg + "<br><br>Note:<br>--------<br>Drop Your Request to Madhuban for Unlock this Entry<br>If You Really Want To Do Some Action...!",
                        result = false,
                        refreshGrid = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Get_Closed_Bank_Status(xMaster_ID))
                {
                    return Json(new
                    {
                        message = "Entry Cannot Be Edited/Deleted..!<br><br>In this entry, Used Bank A/c No.: " + Closed_Bank_Account_No + " was closed...!",
                        result = false,
                        refreshGrid = false
                    }, JsonRequestBehavior.AllowGet);
                }
                DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                if (!(d1 == null))
                {
                    if (d1.Rows.Count > 0)
                    {
                        if (Grid1_Data.EditDate != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            isRecChanged = true;
                        }
                        if (!Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                        {
                            if ((d1.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString()).Length > 0)
                            {
                                multiUserMsg = "<br><br> Record has already been matched in the background";
                                return Json(new
                                {
                                    message = "Matched Internal Transfer Cannot Be Edited/Deleted...! " + multiUserMsg,
                                    refreshGrid = isRecChanged,
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (isRecChanged)
                            {
                                return Json(new
                                {
                                    message = "Record has already been unmatched in the background",
                                    refreshGrid = true,
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, ClientScreen.Accounts_Voucher_Internal_Transfer))
                {
                    return Json(new
                    {
                        message = "Sorry! Deposit slip has already been printed for current transaction!!",
                        refreshGrid = false,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    message = "",
                    refreshGrid = false,
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "Posted Internal Transfer Entry Not Selected...!",
                    refreshGrid = false,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UnmatchTransferClick(string Posted_Id,string Tr_Date,string EditDate,string MID)
        {
            if (ITR_GridView1 != null && ITR_GridView1.Count > 0) 
            {
                var Grid1_Data = ITR_GridView1.Where(x => x.ID == Posted_Id).First();       
                Tr_Date = Grid1_Data.xDate.ToString();
                EditDate = Grid1_Data.EditDate.ToString();
                MID = Grid1_Data.MID;
            }
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (Posted_Id != null)
                    {
                        string xTemp_ID = Posted_Id;
                        if (xTemp_ID.ToLower() != "opening balance" && xTemp_ID.ToLower() != "note-book")
                        {
                            var RecEdit_Date = BASE._Voucher_DBOps.GetEditOnByRecID(xTemp_ID);
                            if (Convert.IsDBNull(RecEdit_Date) || RecEdit_Date == null)
                            {
                                return Json(new
                                {
                                    message = Messages.RecordChanged("Current Voucher"),
                                    refreshGrid = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((DateTime)RecEdit_Date != Convert.ToDateTime(EditDate))
                            {
                                return Json(new
                                {
                                    message = Messages.RecordChanged("Current Voucher"),
                                    refreshGrid = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                if (BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock) || BASE._open_Cen_ID == 4216)
                {
                    if (Posted_Id != null)
                    {
                        string xTemp_ID = Posted_Id;
                        string xTemp_MID = MID;
                        var Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID);
                        if (Status == null)
                        {
                            return Json(new
                            {
                                message = Messages.SomeError,
                                refreshGrid = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status.Rows.Count > 0)
                        {
                            if ((int)Common.Record_Status._Locked == Convert.ToInt32(Status.Rows[0]["REC_STATUS"]))
                            {
                                return Json(new
                                {
                                    message = "Locked Entry Cannot Be Unmatched...<br>Note:<br>Drop Your Request to Madhuban For Unlock This Entry.<br> If You want to do some action..!!",
                                    refreshGrid = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                message = "Entry Not Found..!!",
                                refreshGrid = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                        string multiUserMsg = "";
                        bool isRecChanged = false;
                        var d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                        if (Convert.ToDateTime(EditDate) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            isRecChanged = true;
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]) || d1.Rows[0]["TR_TRF_CROSS_REF_ID"] == null)
                        {
                            multiUserMsg = "<br><br>Record has already been unmatched in the background";
                            return Json(new
                            {
                                message = "Selected Record Is Already UnMatched..." + multiUserMsg,
                                refreshGrid = isRecChanged
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (isRecChanged)
                            {
                                return Json(new
                                {
                                    message = "Transfer Voucher Matched in the Background..." + multiUserMsg,
                                    refreshGrid = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        Status = BASE._Voucher_DBOps.GetStatus_TrCode_OtherCentre(d1.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString());
                        if (Status == null)
                        {
                            return Json(new
                            {
                                message = Messages.SomeError,
                                refreshGrid = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(Common.Record_Status._Locked) == Convert.ToInt32(Status.Rows[0]["REC_STATUS"]))
                            {
                                return Json(new
                                {
                                    message = "Entry matched with this Record is Already locked...< br > Note:< br > Drop Your Request to Madhuban For Unlock This Entry.< br > If You want to do some action..!!",
                                    refreshGrid = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                message = "Entry Not Found..!!",
                                refreshGrid = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                        {
                            return Json(new
                            {
                                message = "Transfer Voucher Already Unmatched..!!",
                                refreshGrid = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._Internal_Tf_Voucher_DBOps.UnMatchTransfers(xTemp_ID, d1.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString(),Convert.ToDateTime(Tr_Date)))
                        {
                            return Json(new
                            {
                                message = "Transfer Entry UnMatched Successfully!!",
                                refreshGrid = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                message = "Transfer Entry Could Not Be UnMatched Successfully!!",
                                refreshGrid = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            message = "Internal Transfer Entry Not Selected!!",
                            refreshGrid = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    message = "",
                    refreshGrid = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    refreshGrids = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void AccepTransferClick_ITR(ref InternalTransfer model, string ID)
        {
            if (ITR_GridView2 != null && ITR_GridView2.Count > 0)
            {
                Return_InternalTransferRegister_Pending data = (ITR_GridView2).Where(x => x.ID == ID).First();
                model._Date = data.xDate.ToString();
                model._a_Item_ID = data.ITEM_ID;
                model._Mode = data.Mode;
                model._CEN_ID = data.CEN_ID;
                model._BI_ID = data.BI_ID;
                model._BI_BRANCH = data.Branch_Name;
                model._BI_ACC_NO = data.Bank_AC_No;
                model._REF_BI_ID = data.REF_BI_ID;
                model._REF_BRANCH = data.Ref_Branch;
                model._REF_OTHERS = data.Ref_Others;
                model._BI_REF_NO = data.RefNo;
                model._BI_REF_DT = data.RefDate != null ? Convert.ToDateTime(data.RefDate).ToString() : null;
                model._Amount = data.Amount.ToString();
                model._a_PUR_ID = data.PUR_ID;
                model.CROSS_REF_ID = data.ID;
                model.CROSS_M_ID = data.MID;
                model.FR_REC_EDIT_ON = data.EditDate;
                model._REF_BANK_ACC_NO = data.Ref_Bank_AccNo;
            }
            model._Accepted_From_Register = true;
            model.Tag = Common.Navigation_Mode._New;
        }
        public void DeleteClick_ITR(ref InternalTransfer model, string ID)
        {
            var Grid1_Data = ITR_GridView1.Where(x => x.ID == ID).First();
            string xMId = Grid1_Data.MID;
            model._Delete_From_Register = true;
            model.ActionMethod = "Delete";
            model.xID_1 = ID;
            model.xMID = xMId.Length > 0 ? xMId : ID;
            model.Info_LastEditedOn = Grid1_Data.EditDate;
        }
        public void EditClick_ITR(ref InternalTransfer model, string ID)
        {
            var Grid1_Data = ITR_GridView1.Where(x => x.ID == ID).First();
            string xMId = Grid1_Data.MID;
            model._Edit_From_Register = true;
            model.ActionMethod = "Edit";
            model.xID_1 = ID;
            model.xMID = xMId.Length > 0 ? xMId : ID;
            model.Info_LastEditedOn = Grid1_Data.EditDate;
        }
        public void GetDataFromItemDropDown(ref InternalTransfer model)
        {
            if (ITV_ItemList == null)
            {
                RefreshItemList();
            }
            var itemid = model.ITV_GLookUp_ItemList;
            var count = ITV_ItemList.Where(x => x.ITEM_ID == itemid).Count();
            if (count > 0)
            {
                var data = ITV_ItemList.Where(x => x.ITEM_ID == itemid).First();
                model.ITV_BE_Item_Head = data.LED_NAME;
                model.ITV_iVoucher_Type = data.ITEM_VOUCHER_TYPE;
                model.ITV_iLed_ID = data.ITEM_LED_ID;
                model.ITV_iTrans_Type = data.ITEM_TRANS_TYPE;
            }//Redmine Bug #133237 fixed
        }
        public void GetToCenID(ref InternalTransfer model)
        {
            if (ITV_ToCenList == null)
            {
                RefreshToCentreList(model.ITV_iVoucher_Type, model.ITV_iTrans_Type);
            }
            string ID = model.ITV_GLookUp_ToCen_List;
            if (ITV_ToCenList.Where(x => x.TO_ID == ID).Count() > 0)
            {
                model.iTO_CEN_ID = (ITV_ToCenList.Where(x => x.TO_ID == ID).First().TO_CEN_ID).ToString();
            }        
        }
        public void GetFrCenID(ref InternalTransfer model)
        {
            if (ITV_FrCenList == null)
            {
                RefreshFrCentreList(model.ITV_iVoucher_Type, model.ITV_iTrans_Type);
            }
            string ID = model.ITV_GLookUp_FrCen_List;
            if (ITV_FrCenList.Where(x => x.FR_ID == ID).Count() > 0)
            {
                model.iFR_CEN_ID = ITV_FrCenList.Where(x => x.FR_ID == ID).First().FR_CEN_ID.ToString();
            }
        }
        [HttpGet]
        public ActionResult Frm_Voucher_Win_I_Transfer(InternalTransfer model,string Tag = "", string xID_1 = "", string xID_2 = "", string xMID = "", bool _Accepted_From_Register = false, bool _Delete_From_Register = false, bool _Edit_From_Register = false, string RegisterGridRowID = null, string Info_LastEditedOn = null, string iSpecific_ItemID = "", string GridToRefresh = "CashBookListGrid")
        {
            ModelState.Clear();
            var i = 0;
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            string[] AM = { "_New", "_New_From_Selection", "_Edit", "_View", "_Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, Rights[i]) && Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            //model = new InternalTransfer();
            if (_Accepted_From_Register)
            {
                AccepTransferClick_ITR(ref model, RegisterGridRowID);
            }
            else if (_Delete_From_Register)
            {
                DeleteClick_ITR(ref model, RegisterGridRowID);
            }
            else if (_Edit_From_Register)
            {
                EditClick_ITR(ref model, RegisterGridRowID);
            }
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = Tag;
            model.TempActionMethod = model.ActionMethod;
            model.xMID = xMID;
            model.xID_1 = xID_1;
            model.xID_2 = xID_2;
            model.ITV_iSpecific_ItemID = iSpecific_ItemID;
            model.OpenYearID = BASE._open_Year_ID;
            ViewBag.Open_Ins_Name = BASE._open_Ins_Name;
            model.ShowChangeItemEffects = true;
            model._Pending_List = false;
            model.TitleX = "Internal Transfer";
            RefreshItemList();
            RefreshBankList();
            RefreshTrfBankList();
            DataTable HQ_DT = BASE._Internal_Tf_Voucher_DBOps.GetHQCenters();
            foreach (DataRow xRow in HQ_DT.Rows)
            {
                model.HQ_IDs += "'" + xRow["HQ_CEN_ID"].ToString() + "',";
            }
            if (model.HQ_IDs.Trim().Length > 0)
            {
                model.HQ_IDs = model.HQ_IDs.Trim().EndsWith(",") ? model.HQ_IDs.Trim().Substring(0, model.HQ_IDs.Trim().Length - 1) : model.HQ_IDs.Trim();
            }
            else if (model.HQ_IDs.Trim().Length == 0)
            {
                model.HQ_IDs = "''";
            }
            HQ_IDs = model.HQ_IDs;

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_ITV = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_ITV = model.SpecialReferenceList_Data_ITV.Count();

            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.ITV_Txt_V_NO = "";
                if (BASE._IsVolumeCenter)
                {
                    model.ITV_GLookUp_PurList = "8f6b3279-166a-4cd9-8497-ca9fc6283b25";
                }
            }
            else if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)//data binding
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_ITV = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(model.xMID, 1);
                DataTable d4 = BASE._Rect_DBOps.GetSlipRecord(model.xMID);
                var d5 = new List<Return_GetSlipMasterRecord>();
                if (d4 != null)
                {
                    if (d4.Rows.Count > 0)
                    {
                        d5 = BASE._Voucher_DBOps.GetSlipMAsterRecord(d4.Rows[0]["TR_SLIP_ID"].ToString());
                    }
                }
                if (d1 == null || d1.Rows.Count <= 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    string viewstr = "";
                    if (model.Tag == Common.Navigation_Mode._View)
                    {
                        viewstr = "view";
                    }
                    if (model.Info_LastEditedOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                    {
                        string message = Messages.RecordChanged("Current Transfer", viewstr);
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.xID_1 = d1.Rows[0]["REC_ID"].ToString();
                DataTable d2 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(model.xMID, 2);
                if (d2 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                if (d2.Rows.Count > 0)
                {
                    model.xID_2 = d2.Rows[0]["REC_ID"].ToString();
                }
                else
                {
                    model.xID_2 = "";
                }
                DataTable d3 = BASE._Internal_Tf_Voucher_DBOps.GetPurposeRecord(model.xID_1);
                if (d3 == null || d3.Rows.Count <= 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                DataBinding_ITR(ref model, d1, d2, d3, d4, d5);
            }
            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                if (model._Accepted_From_Register == true)
                {
                    AcceptFromRegister(ref model);
                }
                else
                {
                    if (string.IsNullOrEmpty(model.Selected_Item_ID))
                    {
                        model.USE_CROSS_REF = false;
                        DataSet d1 = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(1, null);
                        DataTable P1 = d1.Tables[0];
                        if (P1 == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        if (P1.Rows.Count > 0)
                        {
                            model._Pending_List = true;
                        }
                        else
                        {
                            model._Pending_List = false;
                        }
                    }
                }
            }
            else
            {
                model.USE_CROSS_REF = false;
            }
            if (model.Tag == Common.Navigation_Mode._New_From_Selection && model.USE_CROSS_REF == false)
            {
                model.ITV_GLookUp_ItemList = model.ITV_iSpecific_ItemID;
            }
            Load_from_Last_voucher(ref model);
            model.ITV_Txt_Ref_No = model.ITV_Txt_Ref_No.HandleEscapeCharacters();
            model.ITV_Txt_Trf_ANo = model.ITV_Txt_Trf_ANo.HandleEscapeCharacters();
            model.ITV_Txt_Trf_Branch = model.ITV_Txt_Trf_Branch.HandleEscapeCharacters();
            model.ITV_Txt_DD_Fr_Chq_No = model.ITV_Txt_DD_Fr_Chq_No.HandleEscapeCharacters();

            //if (!string.IsNullOrWhiteSpace(model.ITV_GLookUp_TrfBankList))
            //{
            //    string BA_REC_ID = null;
            //    if (!string.IsNullOrWhiteSpace(model.ITV_Txt_Trf_ANo))
            //    {
            //        var BankData = ITV_TrfBankList.Find(x => x.TRF_BI_ID == model.ITV_GLookUp_TrfBankList && x.TRF_BA_ACCOUNT_NO == model.ITV_Txt_Trf_ANo);
            //        if (BankData != null)
            //        {
            //            BA_REC_ID = BankData.BA_REC_ID;
            //        }                 
            //    }
            //    if (model.ITV_GLookUp_TrfBankList.Contains("|") == false)
            //    {
            //        model.ITV_GLookUp_TrfBankList = model.ITV_GLookUp_TrfBankList + "|" + BA_REC_ID;
            //    }
            //}
            return View("Frm_Voucher_Win_I_Transfer", model);
        }
        public void DataBinding_ITR(ref InternalTransfer model, DataTable d1, DataTable d2, DataTable d3, DataTable d4, List<Return_GetSlipMasterRecord> d5)
        {
            model.ITV_Txt_V_NO = d1.Rows[0]["TR_VNO"].ToString();
            model.ITV_Cmd_Mode = d1.Rows[0]["TR_MODE"].ToString();
            if (!Convert.IsDBNull(d1.Rows[0]["TR_ITEM_ID"]))
            {
                if (d1.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.ITV_GLookUp_ItemList = d1.Rows[0]["TR_ITEM_ID"].ToString();                    
                }
            }
            model.ITV_Txt_V_Date = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
            string Tr_AB_ID_1 = "";
            string Tr_AB_ID_2 = "";
            if (d1.Rows[0]["TR_TYPE"].ToString().ToUpper() == "DEBIT")
            {
                Tr_AB_ID_1 = d1.Rows[0]["Tr_AB_ID_1"].ToString();
                Tr_AB_ID_2 = d1.Rows[0]["Tr_AB_ID_2"].ToString();
            }
            else
            {
                Tr_AB_ID_1 = d1.Rows[0]["Tr_AB_ID_2"].ToString();
                Tr_AB_ID_2 = d1.Rows[0]["Tr_AB_ID_1"].ToString();
            }
            if (Tr_AB_ID_1.Length > 0)
            {
                model.ITV_GLookUp_ToCen_List = Tr_AB_ID_1;
            }
            if (Tr_AB_ID_2.Length > 0)
            {
                model.ITV_GLookUp_FrCen_List = Tr_AB_ID_2;
            }
            string Bank_ID = "";
            if (d1.Rows[0]["TR_TYPE"].ToString().ToUpper() == "DEBIT")
            {
                if ((!Convert.IsDBNull(d1.Rows[0]["TR_SUB_CR_LED_ID"])) && (Convert.IsDBNull(d1.Rows[0]["TR_CR_LED_ID"]) ? "" : d1.Rows[0]["TR_CR_LED_ID"].ToString()) == "00079")
                {
                    Bank_ID = d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                }
            }
            else
            {
                if ((!Convert.IsDBNull(d1.Rows[0]["TR_SUB_DR_LED_ID"])) && (Convert.IsDBNull(d1.Rows[0]["TR_DR_LED_ID"]) ? "" : d1.Rows[0]["TR_DR_LED_ID"].ToString()) == "00079")
                {
                    Bank_ID = d1.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                }
            }
            if (Bank_ID.Length > 0)
            {
                model.ITV_GLookUp_BankList = Bank_ID;
            }
            model.ITV_Txt_Ref_No = d1.Rows[0]["TR_REF_NO"].ToString();
            model.ITV_Txt_Ref_Date = Convert.IsDBNull(d1.Rows[0]["TR_REF_DATE"]) ? (DateTime?)null : Convert.ToDateTime(d1.Rows[0]["TR_REF_DATE"]);
            model.ITV_Txt_Ref_CDate = Convert.IsDBNull(d1.Rows[0]["TR_REF_CDATE"]) ? (DateTime?)null : Convert.ToDateTime(d1.Rows[0]["TR_REF_CDATE"]);
            if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_OTHERS"]))
            {
                model.ITV_Txt_DD_Fr_Chq_No = d1.Rows[0]["TR_REF_OTHERS"].ToString();
            }
            if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_BANK_ID"]))
            {
                if (d1.Rows[0]["TR_REF_BANK_ID"].ToString().Length > 0)
                {
                    model.ITV_GLookUp_TrfBankList = d1.Rows[0]["TR_REF_BANK_ID"].ToString();
                    model.ITV_Txt_Trf_Branch = d1.Rows[0]["TR_REF_BRANCH"].ToString();
                    model.ITV_Txt_Trf_ANo = d1.Rows[0]["TR_MT_ACC_NO"].ToString();
                    GetDataFromItemDropDown(ref model);
                    GetToCenID(ref model);
                    GetFrCenID(ref model);
                    RefreshTrfBankList(model.ITV_Cmd_Mode, model.ITV_iTrans_Type, model.iTO_CEN_ID, model.iFR_CEN_ID);
                    //string BA_REC_ID=null;
                    //if (model.ITV_Cmd_Mode.ToUpper() == "CBS" || model.ITV_Cmd_Mode.ToUpper() == "RTGS" || model.ITV_Cmd_Mode.ToUpper() == "NEFT" || model.ITV_Cmd_Mode.ToUpper() == "CHEQUE" || model.ITV_Cmd_Mode.ToUpper() == "CASH TO BANK") 
                    //{
                    //    if (!string.IsNullOrWhiteSpace(model.ITV_Txt_Trf_ANo)) 
                    //    {
                    //        BA_REC_ID = ITV_TrfBankList.Find(x => x.TRF_BI_ID == d1.Rows[0]["TR_REF_BANK_ID"].ToString() && x.TRF_BA_ACCOUNT_NO == d1.Rows[0]["TR_MT_ACC_NO"].ToString()).BA_REC_ID;
                    //    }
                    //}
                    //model.ITV_GLookUp_TrfBankList = model.ITV_GLookUp_TrfBankList + "|" + BA_REC_ID;
                }
            }
            model.ITV_Txt_Amount = Convert.ToDouble(d1.Rows[0]["TR_AMOUNT"]);
            if (d2.Rows.Count > 0)
            {
                model.ITV_Txt_Bank_Chg = Convert.ToDouble(d2.Rows[0]["TR_AMOUNT"]);
            }
            else
            {
                model.ITV_Txt_Bank_Chg = 0.00;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d3.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.ITV_GLookUp_PurList = d3.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }
            model.ITV_Txt_Narration = d1.Rows[0]["TR_NARRATION"].ToString();
            model.ITV_Txt_Remarks = d1.Rows[0]["TR_REMARKS"].ToString();
            model.ITV_Txt_Reference = d1.Rows[0]["TR_REFERENCE"].ToString();
            if (d5.Count > 0)
            {
                model.ITV_Txt_Slip_No = d5[0].SL_NO;
            }
        }
        public void AcceptFromRegister(ref InternalTransfer model)
        {
            model.USE_CROSS_REF = true;
            string xItem_ID = model._a_Item_ID;
            if (xItem_ID.Length > 0)
            {
                model.ITV_GLookUp_ItemList = xItem_ID;
                GetDataFromItemDropDown(ref model);
            }
            model.ITV_Txt_V_Date = Convert.ToDateTime(model._Date);
            model.ITV_Cmd_Mode = model._Mode;
            string Tr_AB_ID_1 = "";
            string Tr_AB_ID_2 = "";
            if (model.ITV_iTrans_Type != null && model.ITV_iTrans_Type.ToUpper() == "DEBIT")
            {
                Tr_AB_ID_1 = model._CEN_ID;
                Tr_AB_ID_2 = BASE._open_Cen_Rec_ID;
            }
            else
            {
                Tr_AB_ID_1 = BASE._open_Cen_Rec_ID;
                Tr_AB_ID_2 = model._CEN_ID;
            }
            if (Tr_AB_ID_1.Length > 0)
            {
                model.ITV_GLookUp_ToCen_List = Tr_AB_ID_1;
            }
            if (Tr_AB_ID_2.Length > 0)
            {
                model.ITV_GLookUp_FrCen_List = Tr_AB_ID_2;
            }
            model.Ref_Bank_ID = "";
            model.Ref_Branch = "";
            GetToCenID(ref model);
            GetFrCenID(ref model);
            RefreshTrfBankList(model.ITV_Cmd_Mode, model.ITV_iTrans_Type, model.iTO_CEN_ID, model.iFR_CEN_ID);
            if (model.ITV_Cmd_Mode.ToUpper() != "CASH")
            {
                string xBANK_ID = model._BI_ID;
                string xREF_BANK_ID = model._REF_BI_ID;
                string xREF_ACC_NO = model._BI_ACC_NO;
                if (!string.IsNullOrWhiteSpace(xBANK_ID))
                {
                    if (model.ITV_iTrans_Type != null && model.ITV_iTrans_Type.ToUpper() == "CREDIT")
                    {
                        if (model.ITV_Cmd_Mode.ToUpper() == "DD")
                        {
                            model.ITV_GLookUp_TrfBankList = xREF_BANK_ID;
                            model.lbl_Trf_ANo_Tag = xREF_BANK_ID;
                            model.ITV_Txt_Trf_Branch = model._REF_BRANCH;
                            model.ITV_Txt_Trf_ANo = model._REF_BANK_ACC_NO;
                        }
                        else
                        {
                            ITV_TrfBankList = ITV_TrfBankList.Where(x => x.BA_REC_ID == xBANK_ID).ToList();
                            if (ITV_TrfBankList.Count > 0)
                            {
                                xBANK_ID = ITV_TrfBankList[0].TRF_BI_ID;
                                model.lbl_Trf_ANo_Tag = xBANK_ID;
                                model.ITV_GLookUp_TrfBankList = xBANK_ID;
                            }
                            model.ITV_Txt_Trf_Branch = model._BI_BRANCH;
                            model.ITV_Txt_Trf_ANo = model._BI_ACC_NO;
                            model.Ref_Bank_ID = model._REF_BI_ID;
                            model.Ref_Branch = model._REF_BRANCH;
                        }
                    }
                    else if (model.ITV_iTrans_Type != null && model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                    {
                        if (model.ITV_Cmd_Mode.ToUpper() == "DD" || model.ITV_Cmd_Mode.ToUpper() == "CHEQUE")
                        {
                            model.ITV_GLookUp_TrfBankList = xREF_BANK_ID;
                            model.lbl_Trf_ANo_Tag = xREF_BANK_ID;
                            model.ITV_Txt_Trf_Branch = model._BI_BRANCH;
                            model.ITV_Txt_Trf_ANo = model._BI_ACC_NO;
                        }
                        else
                        {
                            ITV_TrfBankList = ITV_TrfBankList.Where(x => x.BA_REC_ID == xBANK_ID).ToList();
                            if (ITV_TrfBankList.Count > 0)
                            {
                                xBANK_ID = ITV_TrfBankList[0].TRF_BI_ID;
                                model.lbl_Trf_ANo_Tag = xBANK_ID;
                                model.ITV_GLookUp_TrfBankList = xBANK_ID;
                                model.ITV_Txt_Trf_ANo = ITV_TrfBankList[0].TRF_BA_ACCOUNT_NO;
                            }
                        }
                    }
                    if (ITV_BankList.Count == 1)
                    {
                        if (model.ITV_Cmd_Mode.ToUpper() == "CBS" || model.ITV_Cmd_Mode.ToUpper() == "RTGS" || model.ITV_Cmd_Mode.ToUpper() == "NEFT")
                        {
                            if (!string.IsNullOrWhiteSpace(xREF_BANK_ID))
                            {
                                var bankData = ITV_BankList.Where(x => x.BANK_ACC_NO == xREF_ACC_NO).ToList();
                                if (bankData.Count > 0)
                                {
                                    var GLookUp_BankListView_FocusedRowHandle = bankData[0];
                                    if (GLookUp_BankListView_FocusedRowHandle != null)
                                    {
                                        model.ITV_GLookUp_BankList = GLookUp_BankListView_FocusedRowHandle.BA_ID;
                                    }
                                }
                            }
                        }
                    }
                }
                if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD")
                {
                    if (xREF_BANK_ID.Length > 0)
                    {
                        model.ITV_GLookUp_TrfBankList = xREF_BANK_ID;
                        model.ITV_Txt_Trf_Branch = model._REF_BRANCH;
                        model.ITV_Txt_Trf_ANo = "";
                    }
                }
                model.ITV_Txt_Ref_No = model._BI_REF_NO;
                if (model.ITV_iTrans_Type != null && model.ITV_iTrans_Type.ToUpper() == "DEBIT" && model.ITV_Cmd_Mode.ToUpper() == "DD")
                {
                    model.ITV_Txt_DD_Fr_Chq_No = model._REF_OTHERS;
                }
                if (model._BI_REF_DT != null)
                {
                    model.ITV_Txt_Ref_Date = Convert.ToDateTime(model._BI_REF_DT);
                }
            }
            model.ITV_Txt_Amount = Convert.ToDouble(model._Amount);
            model.ITV_Txt_Bank_Chg = 0.00;
            model.ITV_GLookUp_PurList = model._a_PUR_ID;
            model._BI_REF_NO = model._BI_REF_NO.HandleEscapeCharacters();
            model._REF_BRANCH = model._REF_BRANCH.HandleEscapeCharacters();
            model._REF_BANK_ACC_NO = model._REF_BANK_ACC_NO.HandleEscapeCharacters();
            model._BI_BRANCH = model._REF_BRANCH.HandleEscapeCharacters();
            model._BI_ACC_NO = model._REF_BANK_ACC_NO.HandleEscapeCharacters();
        }
        public void Pending_List(ref InternalTransfer model, DataTable P1)
        {
            model.USE_CROSS_REF = false;

        }
        public void Load_from_Last_voucher(ref InternalTransfer model)
        {
            if (!string.IsNullOrEmpty(model.Selected_Mode))
            {
                if (!string.IsNullOrEmpty(model.Selected_Item_ID))
                {
                    model.ITV_GLookUp_ItemList = model.Selected_Item_ID;
                    GetDataFromItemDropDown(ref model);
                }
                model.ITV_Txt_V_Date = model.Selected_V_Date;
                model.ITV_Cmd_Mode = model.Selected_Mode;
                if (model.ITV_Cmd_Mode.ToUpper() != "CASH")
                {
                    model.ITV_GLookUp_BankList = model.Selected_Bank_ID;
                }
            }
            if (model.Selected_Amount > 0)
            {
                model.ITV_Txt_Amount = model.Selected_Amount;
                if (model.Selected_Ref_Date != DateTime.MinValue)
                {
                    model.ITV_Txt_Ref_Date = model.Selected_Ref_Date;
                }
                if (model.Selected_RefC_Date != DateTime.MinValue)
                {
                    model.ITV_Txt_Ref_CDate = model.Selected_RefC_Date;
                }
                model.ITV_Txt_Ref_No = model.Selected_RefNo;
                if (!string.IsNullOrEmpty(model.Selected_Drawee_Bank_ID))
                {
                    if (string.IsNullOrWhiteSpace(model.iTO_CEN_ID)) 
                    {
                        GetToCenID(ref model);
                    }
                    if (string.IsNullOrWhiteSpace(model.iFR_CEN_ID))
                    {
                        GetFrCenID(ref model);
                    }
                    RefreshTrfBankList(model.ITV_Cmd_Mode, model.ITV_iTrans_Type, model.iTO_CEN_ID, model.iFR_CEN_ID);
                    model.ITV_GLookUp_TrfBankList = model.Selected_Drawee_Bank_ID;
                }
                model.ITV_Txt_Trf_Branch = model.Selected_Drawee_Branch;
                model.Selected_RefNo = model.Selected_RefNo.HandleEscapeCharacters();
                model.Selected_Drawee_Branch = model.Selected_Drawee_Branch.HandleEscapeCharacters();
            }
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_I_Transfer(InternalTransfer model, string ActionMethod = null, double? ITV_Txt_Amount = null, double? Txt_Total = null, bool SaveFlag = false)
        {
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            model.Tag = Tag;
            model.ITV_iTrans_Type = model.ITV_iTrans_Type ?? "";//Redmine Bug #133237 fixed
            model.xID_2 = model.xID_2 ?? "";//Redmine Bug #133251 fixed          
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.CROSS_REF_ID = model.CROSS_REF_ID ?? ""; //redmine 132899 bug fix
                if (model.ITV_TDS_Open == false)
                {
                    if (BASE.AllowMultiuser())
                    {
                        if (!string.IsNullOrEmpty(model.ITV_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.ITV_GLookUp_BankList, "");
                            if (AccNo == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Error!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added/Edited/Deleted...!<br><br>In this entry, Used Bank A/c No.: " + AccNo + " was closed...!!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                jsonParam.title = "Information...";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                        {
                            DataTable internalTf_DbOps = BASE._Internal_Tf_Voucher_DBOps.GetRecord(model.xMID, 1);
                            if (internalTf_DbOps == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Error!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (internalTf_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Transfer");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                jsonParam.title = "Record Already Changed!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.LastEditedOn != Convert.ToDateTime(internalTf_DbOps.Rows[0]["REC_EDIT_ON"]))
                            {
                                jsonParam.message = Messages.RecordChanged("Current Transfer");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                jsonParam.title = "Record Already Changed!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            object MaxValue = 0;
                            MaxValue = BASE._Internal_Tf_Voucher_DBOps.GetStatus(model.xID_1);
                            if (MaxValue == null)
                            {
                                jsonParam.message = "Entry Not Found...!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Information...";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                            {
                                jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br><br>Note:<br>-----------<br> Drop your Request to Madhuban To Unlock this Entry,<br>If you really want to do some action...!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                jsonParam.title = "Information...";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(model.xID_1);
                            if (Status == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Error!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            string xCross_Ref_Id = string.Empty;
                            if (Status.Rows.Count > 0)
                            {
                                if (!Convert.IsDBNull(Status.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                                {
                                    xCross_Ref_Id = Status.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                                }
                            }
                            if (!Convert.IsDBNull(xCross_Ref_Id))
                            {
                                if (xCross_Ref_Id.Length > 0)
                                {
                                    jsonParam.message = "Matched Internal Transfer Cannot Be Edited/Deleted...!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.title = "Information...";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }

                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                    {
                        if (string.IsNullOrEmpty(model.ITV_iTrans_Type)|| string.IsNullOrWhiteSpace(model.ITV_iTrans_Type))
                        {
                            jsonParam.message = "Item Txn Type Not Selected...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_GLookUp_ItemList";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.ITV_GLookUp_ItemList))
                        {
                            if (string.IsNullOrEmpty(model._a_Item_ID))// AcceptFromRegister._a_Item_ID will have value//Redmine Bug #133237 fixed
                            {
                                jsonParam.message = "Item Name Not Selected...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_GLookUp_ItemList";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (IsDate(model.ITV_Txt_V_Date) == false)
                        {
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_V_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.ITV_Txt_V_Date) == true)
                        {
                            if (model.ITV_Txt_V_Date < BASE._open_Year_Sdt || model.ITV_Txt_V_Date > BASE._open_Year_Edt)
                            {
                                jsonParam.message = "Date Not As Per Financial Year...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_Txt_V_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List))
                        {
                            jsonParam.message = "From Center Not Selected...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_GLookUp_FrCen_List";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List))
                        {
                            jsonParam.message = "To Center Not Selected...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_GLookUp_ToCen_List";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.ITV_GLookUp_FrCen_List == model.ITV_GLookUp_ToCen_List)
                        {
                            jsonParam.message = "Both Centre are Same...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_GLookUp_ToCen_List";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._open_Year_ID >= 2627)
                        {
                            if (model.ITV_Cmd_Mode == "CASH TO BANK")
                            {
                                jsonParam.message = "Income Tax के AIS में दिखाया गया “Cash Deposit in Bank” का अमाउंट हमारी Books of Accounts में दिखाए गए “Cash Deposit in Bank” से match होना अनिवार्य है।।</br>"+
                                                    "इसी कारण अब “Cash to Bank” mode उपलब्ध नहीं है। क्यूँकि यह कैश एवं बैंक दोनों का mix mode है।</br>" +
                                                    "निम्नानुसार entry करें:</br>" +
                                                    "1) यदि किसी centre ने cash किसी अन्य centre के bank account में direct जमा किया है, तो:</br>" +
                                                    "Internal Transfer की entry में mode “Cash” select करें।</br>" +
                                                    "2) यदि किसी centre के bank account में किसी अन्य centre द्वारा cash direct जमा किया गया है, तो:</br>" +
                                                    "Internal Transfer की entry में mode “Cash” select करें।</br>" +
                                                    "एवं</br>" +
                                                    "उसी date पर same amount की entry “Cash Deposit in Bank” में भी करें।";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "BE_PAN_No_Donation";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        bool BankSelect = false;
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH")
                        {
                            BankSelect = false;
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false)
                        {
                            BankSelect = false;
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF)
                        {
                            BankSelect = true;
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO BANK" && model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                        {
                            BankSelect = false;
                        }
                        else
                        {
                            BankSelect = true;
                        }
                        if (BankSelect)
                        {
                            if (string.IsNullOrEmpty(model.ITV_GLookUp_BankList))
                            {
                                jsonParam.message = "Bank Not Selected...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_GLookUp_BankList";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        model.ITV_GLookUp_BankList = model.ITV_GLookUp_BankList ?? "";

                        bool BANK_FLAG = false;
                        bool BRANCH_FLAG = false;
                        bool ACC_FLAG = false;
                        if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                        {
                            if (model.ITV_Cmd_Mode.ToUpper() == "CBS" || model.ITV_Cmd_Mode.ToUpper() == "RTGS" || model.ITV_Cmd_Mode.ToUpper() == "NEFT")
                            {
                                BANK_FLAG = true;
                                BRANCH_FLAG = true;
                                ACC_FLAG = true;
                            }
                            else if (model.ITV_Cmd_Mode.ToString().ToUpper() == "CASH TO DD" || model.ITV_Cmd_Mode.ToString().ToUpper() == "DD")
                            {
                                BANK_FLAG = true;
                                BRANCH_FLAG = true;
                                ACC_FLAG = false;
                            }
                            else if (model.ITV_Cmd_Mode.ToString().ToUpper() == "CASH TO BANK" && model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                            {
                                BANK_FLAG = true;
                                BRANCH_FLAG = true;
                                ACC_FLAG = true;
                            }
                            else
                            {
                                BANK_FLAG = false;
                                BRANCH_FLAG = false;
                                ACC_FLAG = false;
                            }
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "DD")
                        {
                            BANK_FLAG = true;
                            BRANCH_FLAG = true;
                            ACC_FLAG = false;
                        }
                        else
                        {
                            BANK_FLAG = false;
                            BRANCH_FLAG = false;
                            ACC_FLAG = false;
                        }
                        if (BANK_FLAG)
                        {
                            if (string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) && model.ITV_Cmd_Mode.ToUpper() != "CASH")
                            {
                                jsonParam.message = "Bank Not Selected...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_GLookUp_TrfBankList";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            model.ITV_GLookUp_TrfBankList = model.ITV_GLookUp_TrfBankList ?? "";
                        }
                        if (BRANCH_FLAG)
                        {
                            if (string.IsNullOrEmpty(model.ITV_Txt_Trf_Branch) && model.ITV_Cmd_Mode.ToUpper() != "CASH")
                            {
                                jsonParam.message = "Bank Branch Not Specified...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_Txt_Trf_Branch";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (ACC_FLAG)
                        {
                            if (string.IsNullOrEmpty(model.ITV_Txt_Trf_ANo) && model.ITV_Cmd_Mode.ToUpper() != "CASH")
                            {
                                jsonParam.message = "Bank A/C No. Not Specified...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_Txt_Trf_ANo";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.ITV_iTrans_Type.ToUpper() == "DEBIT" && string.IsNullOrEmpty(model.ITV_Txt_DD_Fr_Chq_No) && model.ITV_Cmd_Mode.ToUpper() == "DD")
                        {
                            jsonParam.message = "Cheque No. Not Specified...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_DD_Fr_Chq_No";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.ITV_Txt_Ref_No) && model.ITV_Cmd_Mode.ToUpper() != "CASH")
                        {
                            jsonParam.message = "No. Not Specified...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_Ref_No";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.ITV_Txt_Ref_Date) == false && model.ITV_Cmd_Mode.ToUpper() != "CASH")
                        {
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_Ref_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.ITV_Txt_Amount == null || model.ITV_Txt_Amount <= 0)//Redmine Bug #133165 fixed
                        {
                            jsonParam.message = "Amount cannot be Zero/Negative...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.ITV_Txt_Bank_Chg < 0)
                        {
                            jsonParam.message = "Bank Charges cannot be Negative...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_Txt_Bank_Chg";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.ITV_GLookUp_PurList))
                        {
                            jsonParam.message = "Purpose Not Selected...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "ITV_GLookUp_PurList";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.USE_CROSS_REF == false)
                        {
                            object MaxValue = 0;
                            string Tr_AB_ID_1 = string.Empty;
                            string Tr_AB_ID_2 = string.Empty;
                            bool IsEdit = false;
                            if (model.Tag == Common.Navigation_Mode._Edit)
                            {
                                IsEdit = true;
                            }
                            if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                            {
                                Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                                Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                            }
                            else
                            {
                                Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                                Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                            }
                            if (model.ITV_Cmd_Mode.ToUpper() == "CASH")
                            {
                                MaxValue = BASE._Internal_Tf_Voucher_DBOps.GetCashTxnCount(((int)Common.Voucher_Screen_Code.Internal_Transfer).ToString(), model.ITV_GLookUp_ItemList, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short), IsEdit, model.xMID);
                                if (MaxValue == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                if (Convert.ToInt32(MaxValue) != 0)
                                {
                                    if (!BASE.AllowMultiuser())
                                    {
                                        jsonParam.message = "Cash Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists ...!" + "<br><br>" + "Note: Please Edit The Existing Entry...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = false;
                                        jsonParam.title = "Internal Transfer...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        jsonParam.message = "Cash Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists ...!" + "<br><br>" + "Note: Please Edit The Existing Entry...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.title = "Referred Record Already Changed...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                MaxValue = BASE._Internal_Tf_Voucher_DBOps.GetCashPendingTxnCount(((int)Common.Voucher_Screen_Code.Internal_Transfer).ToString(), model.ITV_GLookUp_ItemList, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short), IsEdit, model.xMID);
                                if (MaxValue == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                if (Convert.ToInt32(MaxValue) != 0)
                                {
                                    if (!BASE.AllowMultiuser())
                                    {
                                        jsonParam.message = "Cash Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists in Pending List...!" + "<br><br>" + "Note: Please Select Same Entry From Pending List...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = false;
                                        jsonParam.title = "Internal Transfer...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        jsonParam.message = "Cash Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists in Pending List...!" + "<br><br>" + "Note: Please Select Same Entry From Pending List...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.title = "Referred Record Already Changed...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                string _RefBankID = string.Empty;
                                if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                                {
                                    _RefBankID = string.IsNullOrEmpty(model.lbl_Trf_ANo_Tag) ? string.Empty : model.lbl_Trf_ANo_Tag;
                                }
                                else
                                {
                                    _RefBankID = string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) ? string.Empty : model.ITV_GLookUp_TrfBankList;
                                }
                                MaxValue = BASE._Internal_Tf_Voucher_DBOps.GetNonCashTxnCount(((int)Common.Voucher_Screen_Code.Internal_Transfer).ToString(), model.ITV_GLookUp_ItemList, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short), model.ITV_Cmd_Mode.ToUpper(), _RefBankID, model.ITV_Txt_Ref_No, IsEdit, model.xMID);
                                if (MaxValue == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                if (Convert.ToInt32(MaxValue) != 0)
                                {
                                    if (!BASE.AllowMultiuser())
                                    {
                                        jsonParam.message = model.ITV_Cmd_Mode + " Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists ...!" + "<br><br>" + "Note: Please Edit The Existing Entry...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = false;
                                        jsonParam.title = "Internal Transfer...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        jsonParam.message = model.ITV_Cmd_Mode + " Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists ...!" + "<br><br>" + "Note: Please Edit The Existing Entry...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.title = "Referred Record Already Changed...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                MaxValue = BASE._Internal_Tf_Voucher_DBOps.GetNonCashPendingTxnCount(((int)Common.Voucher_Screen_Code.Internal_Transfer).ToString(), model.ITV_GLookUp_ItemList, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short), model.ITV_Cmd_Mode.ToUpper(), _RefBankID, model.ITV_Txt_Ref_No, IsEdit, model.xMID);
                                if (MaxValue == null)
                                {
                                    return Json(new
                                    {
                                        message = "Some Error Happened During The Current Operation...!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }

                                if (Convert.ToInt32(MaxValue) != 0)
                                {
                                    if (!BASE.AllowMultiuser())
                                    {
                                        jsonParam.message = model.ITV_Cmd_Mode + " Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists in Pending List...!" + "<br><br>" + "Note: Please Select Same Entry From Pending List...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = false;
                                        jsonParam.title = "Internal Transfer...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        jsonParam.message = model.ITV_Cmd_Mode + " Transfer Entry from " + model.FromCenterName + " to " + model.ToCenterName + " on " + Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Date_Format_Current) + " already Exists in Pending List...!" + "<br><br>" + "Note: Please Select Same Entry From Pending List...!";
                                        jsonParam.result = false;
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.title = "Referred Record Already Changed...";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (model.ITV_iTrans_Type.ToUpper() == "CREDIT")
                        {
                            if (BankSelect)
                            {
                                if (model.ITV_Txt_Slip_No > 0)
                                {
                                    DataTable BankAccIDTable = BASE._DepositSlipsDBOps.GetList(Convert.ToInt32(model.ITV_Txt_Slip_No), model.ITV_GLookUp_BankList);
                                    if (BankAccIDTable.Rows.Count > 0)
                                    {
                                        // --Slip Exists
                                        if (!Convert.IsDBNull(BankAccIDTable.Rows[0]["BA_REC_ID"]))
                                        {
                                            if (BankAccIDTable.Rows[0]["BA_REC_ID"].ToString() != model.ITV_GLookUp_BankList)
                                            {
                                                jsonParam.message = "Selected slip has transaction of other bank...!";
                                                jsonParam.result = false;
                                                jsonParam.closeform = false;
                                                jsonParam.title = "Incomplete Information...";
                                                jsonParam.focusid = "ITV_Txt_Slip_No";
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        if (!Convert.IsDBNull(BankAccIDTable.Rows[0]["Date of Print"]))
                                        {
                                            jsonParam.message = "Selected slip is already printed...!";
                                            jsonParam.result = false;
                                            jsonParam.closeform = false;
                                            jsonParam.title = "Incomplete Information...";
                                            jsonParam.focusid = "ITV_Txt_Slip_No";
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                                else if (model.ITV_Cmd_Mode.ToUpper() != "CASH")
                                {
                                    jsonParam.message = "Slip No Cannot be Negative Or Zero...!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Incomplete Information...";
                                    jsonParam.focusid = "ITV_Txt_Slip_No";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (IsDate(model.ITV_Txt_Ref_CDate))
                        {
                            if (model.ITV_Txt_Ref_Date > model.ITV_Txt_Ref_CDate)
                            {
                                jsonParam.message = "Clearing Date cannot be less than Reference Date!!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "ITV_Txt_Ref_CDate";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    // ----------------------------- // Start Dependencies //-------------------------------
                    if (BASE.AllowMultiuser())
                    {
                        if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection || Tag == Common.Navigation_Mode._Edit)
                        {
                            DateTime oldEditOn;
                            if (model.ITV_GLookUp_BankList.Length > 0)
                            {
                                DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetBankAccounts(model.ITV_GLookUp_BankList);
                                if (d1 == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank);
                                if (d1.Rows.Count <= 0)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.title = "Referred Record Already Deleted!!";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                    if (NewEditOn != oldEditOn)
                                    {
                                        jsonParam.message = Messages.DependencyChanged("Bank Account");
                                        jsonParam.result = false;
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.title = "Referred Record Already Changed!!";
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) && (model.ITV_GLookUp_TrfBankList.Length > 0))
                            {
                                bool Not_App;
                                if (model.ITV_iTrans_Type == "DEBIT" && model.ITV_Cmd_Mode.ToUpper() == "CHEQUE")
                                {
                                    Not_App = true;
                                }
                                else
                                {
                                    Not_App = false;
                                }
                                if (!Not_App)
                                {
                                    if (model.ITV_Cmd_Mode.ToUpper() == "CBS" || model.ITV_Cmd_Mode.ToUpper() == "RTGS" || model.ITV_Cmd_Mode.ToUpper() == "NEFT" || model.ITV_Cmd_Mode.ToUpper() == "CHEQUE")
                                    {
                                        string XCEN_ID = string.Empty;
                                        if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                                        {
                                            XCEN_ID = string.IsNullOrEmpty(model.iTO_CEN_ID) ? string.Empty : model.iTO_CEN_ID;
                                        }
                                        else
                                        {
                                            XCEN_ID = string.IsNullOrEmpty(model.iFR_CEN_ID) ? string.Empty : model.iFR_CEN_ID;
                                        }
                                        DataTable d2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(true, XCEN_ID, model.ITV_Txt_Trf_ANo);
                                        var BankData = ITV_TrfBankList.Find(x => x.TRF_BA_ACCOUNT_NO == model.ITV_Txt_Trf_ANo && x.TRF_BI_ID == model.ITV_GLookUp_TrfBankList);
                                        if (BankData != null)
                                        {
                                            oldEditOn = Convert.ToDateTime(BankData.REC_EDIT_ON);
                                        }
                                        else 
                                        {
                                            oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Trf_Bank);                                            
                                        }
                                        if (d2.Rows.Count <= 0)
                                        {
                                            jsonParam.message = Messages.DependencyChanged("Transfer Bank Account");
                                            jsonParam.result = false;
                                            jsonParam.closeform = true;
                                            jsonParam.refreshgrid = true;
                                            jsonParam.title = "Referred Record Already Deleted!!";
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            DateTime NewEditOn = Convert.ToDateTime(d2.Rows[0]["REC_EDIT_ON"]);
                                            if (NewEditOn != oldEditOn)
                                            {
                                                jsonParam.message = Messages.DependencyChanged("Transfer Bank Account");
                                                jsonParam.result = false;
                                                jsonParam.closeform = true;
                                                jsonParam.refreshgrid = true;
                                                jsonParam.title = "Referred Record Already Changed!!";
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // ------------------------------- // End Dependencies // -----------------------------------
                // Entry made by other centre has been changed/deleted in other centre
                if (!string.IsNullOrEmpty(model.CROSS_M_ID) && model.CROSS_M_ID.ToString().Length > 0)
                {
                    // bug #4944
                    DataTable AccTf = BASE._Internal_Tf_Voucher_DBOps.GetRecord(model.CROSS_M_ID, 1);
                    if (AccTf == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime oldEditOn = Convert.ToDateTime(model.FR_REC_EDIT_ON);
                    if (AccTf.Rows.Count == 0)
                    {
                        jsonParam.message = Messages.DependencyChanged("Internal Transfer entry made by other centre has been deleted");
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.title = "Information....!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        DateTime NewEditOn = Convert.ToDateTime(AccTf.Rows[0]["REC_EDIT_ON"]);
                        if (NewEditOn != oldEditOn)
                        {
                            jsonParam.message = Messages.DependencyChanged("Internal Transfer entry made by other centre has been deleted");
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            jsonParam.title = "Information....!!";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                if (model.Tag.ToString() == "_Delete")
                {
                    Status_Action = Convert.ToString((int)Common_Lib.Common.Record_Status._Deleted);
                }
                string BC_Item_ID = "a044fa70-5398-483f-9e63-47ba9386da4b";
                string BC_Dr_Led_id = string.Empty;
                string BC_Cr_Led_id = string.Empty;
                string BC_Sub_Dr_Led_ID = string.Empty;
                string BC_Sub_Cr_Led_ID = string.Empty;
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection && (model.ITV_Txt_Bank_Chg > 0))
                {
                    DataTable BC_DT = BASE._Internal_Tf_Voucher_DBOps.GetItemsByID(BC_Item_ID);
                    if (BC_DT == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BC_DT.Rows.Count > 0)
                    {
                        BC_Dr_Led_id = BC_DT.Rows[0]["ITEM_LED_ID"].ToString();
                    }
                    else
                    {
                        jsonParam.message = "Bank Charges Item Not Found..!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Internal Transfer...";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                    {
                        BC_Cr_Led_id = "00080";
                    }
                    else
                    {
                        BC_Cr_Led_id = "00079";
                        BC_Sub_Cr_Led_ID = string.IsNullOrEmpty(model.ITV_GLookUp_BankList) ? string.Empty : model.ITV_GLookUp_BankList;
                    }
                }
                // +-----------------END------------------+
                Param_Txn_Insert_InternalTransfer InNewParam = new Param_Txn_Insert_InternalTransfer();
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    string Tr_AB_ID_1 = string.Empty;
                    string Tr_AB_ID_2 = string.Empty;
                    string Dr_Led_id = string.Empty;
                    string Cr_Led_id = string.Empty;
                    string Sub_Dr_Led_ID = string.Empty;
                    string Sub_Cr_Led_ID = string.Empty;

                    if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.ITV_iLed_ID;
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || model.ITV_Cmd_Mode.ToUpper() == "CASH TO BANK" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                        {
                            Cr_Led_id = "00080";
                        }
                        else
                        {
                            Cr_Led_id = "00079";
                            Sub_Cr_Led_ID = string.IsNullOrEmpty(model.ITV_GLookUp_BankList) ? string.Empty : model.ITV_GLookUp_BankList;
                        }
                        Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                        Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                    }
                    else
                    {
                        Cr_Led_id = string.IsNullOrEmpty(model.ITV_iLed_ID) ? string.Empty : model.ITV_iLed_ID;
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                        {
                            Dr_Led_id = "00080";
                        }
                        else
                        {
                            Dr_Led_id = "00079";
                            Sub_Dr_Led_ID = string.IsNullOrEmpty(model.ITV_GLookUp_BankList) ? string.Empty : model.ITV_GLookUp_BankList;
                        }

                        Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                        Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                    }
                    // +------------------TDS Deduction Reference-------------------+
                    if (model.ITV_GLookUp_ItemList == "d611ce4a-756a-42d0-b7ee-51dd7add7263" && BASE._open_Year_ID > 1415)
                    {
                        if (model.ITV_TDS_Open == false)
                        {
                            //jsonParam.message = "OPEN_TDS_SENT_GRID";
                            //jsonParam.result = false;
                            //jsonParam.closeform = false;
                            //jsonParam.title = "Internal Transfer...";
                            //return Json(new
                            //{
                            //    jsonParam
                            //}, JsonRequestBehavior.AllowGet);
                        }
                        if (TDS_Deduction_List != null && TDS_Deduction_List.Count > 0)//Redmine Bug #132899 fixed
                        {
                            List<Parameter_InsertTDSDeduction_VoucherInternalTransfer> inTDSParam = new List<Parameter_InsertTDSDeduction_VoucherInternalTransfer>();

                            for (int i = 0; i <= TDS_Deduction_List.Count; i++)
                            {
                                Parameter_InsertTDSDeduction_VoucherInternalTransfer inTDSDeduct = new Parameter_InsertTDSDeduction_VoucherInternalTransfer();
                                inTDSDeduct.RecID = System.Guid.NewGuid().ToString();
                                inTDSDeduct.RefTxnID = TDS_Deduction_List[i].RefMID;
                                inTDSDeduct.TDS_Sent = Convert.ToDecimal(TDS_Deduction_List[i].TDS_Ded);
                                inTDSDeduct.TxnMID = model.xMID;
                                inTDSParam.Add(inTDSDeduct);
                            }
                            InNewParam.param_Insert_TDSDed = inTDSParam.ToArray();
                        }
                    }
                    // +-----------------END------------------+
                    // Master Entry

                    Parameter_InsertMasterInfo_VoucherInternalTransfer InMInfo = new Parameter_InsertMasterInfo_VoucherInternalTransfer();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer;
                    InMInfo.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                    if (IsDate(model.ITV_Txt_V_Date))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.ITV_Txt_V_Date.ToString();
                    }
                    InMInfo.PartyID = Tr_AB_ID_1;
                    InMInfo.SubTotal = Convert.ToDouble(model.ITV_Txt_Amount);
                    if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                    {
                        InMInfo.Cash = double.Parse(model.ITV_Txt_Amount.ToString());
                    }
                    else
                    {
                        InMInfo.Cash = 0;
                    }
                    if (model.ITV_Cmd_Mode.ToUpper() != "CASH")
                    {
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false)
                        {
                            InMInfo.Bank = 0;
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                        {
                            InMInfo.Bank = double.Parse(model.ITV_Txt_Amount.ToString());
                        }
                        else
                        {
                            InMInfo.Bank = double.Parse(model.ITV_Txt_Amount.ToString());
                        }
                    }
                    else
                    {
                        InMInfo.Bank = 0;
                    }
                    InMInfo.Status_Action = Status_Action;
                    InMInfo.RecID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                    InNewParam.param_InsertMaster = InMInfo;
                    // ENTRY PART #1
                    model.xID_1 = Guid.NewGuid().ToString();
                    string xCROSS_REF_ID = string.Empty;
                    if (model.USE_CROSS_REF)
                    {
                        xCROSS_REF_ID = " '" + model.CROSS_REF_ID + "' ";
                    }
                    else
                    {
                        xCROSS_REF_ID = " NULL ";
                    }
                    if (BASE.AllowMultiuser())
                    {
                        if (model.ITV_iTrans_Type == "CREDIT")
                        {
                            DataTable IntTf = (DataTable)BASE._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_Internal_Transfer, model.CROSS_REF_ID, false, BASE._open_Year_ID);
                            if (IntTf.Rows.Count > 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Internal Transfer");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Referred Record Already Matched !!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    Parameter_Insert_VoucherInternalTransfer InParam = new Parameter_Insert_VoucherInternalTransfer();
                    InParam.TransCode = (int)Common.Voucher_Screen_Code.Internal_Transfer;
                    InParam.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                    if (IsDate(model.ITV_Txt_V_Date))
                    {
                        InParam.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.ITV_Txt_V_Date.ToString();
                    }
                    InParam.ItemID = string.IsNullOrEmpty(model.ITV_GLookUp_ItemList) ? string.Empty : model.ITV_GLookUp_ItemList;
                    InParam.Type = string.IsNullOrEmpty(model.ITV_iTrans_Type) ? string.Empty : model.ITV_iTrans_Type;
                    InParam.Cr_Led_ID = string.IsNullOrEmpty(Cr_Led_id) ? string.Empty : Cr_Led_id;
                    InParam.Dr_Led_ID = string.IsNullOrEmpty(Dr_Led_id) ? string.Empty : Dr_Led_id;
                    InParam.SUB_Cr_Led_ID = string.IsNullOrEmpty(Sub_Cr_Led_ID) ? string.Empty : Sub_Cr_Led_ID;
                    InParam.SUB_Dr_Led_ID = string.IsNullOrEmpty(Sub_Dr_Led_ID) ? string.Empty : Sub_Dr_Led_ID;
                    InParam.Amount = double.Parse(model.ITV_Txt_Amount.ToString());
                    if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                    {
                        InParam.Mode = "DD";
                    }
                    else
                    {
                        InParam.Mode = model.ITV_Cmd_Mode;
                    }

                    // 'testing for Cheque issue in debit entries
                    string RefBank = string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) ? string.Empty : model.ITV_GLookUp_TrfBankList;
                    //  If ITV_iTrans_Type.ToUpper = "DEBIT" Then RefBank = ITV_GLookUp_BankList.Tag   'commented for testing
                    InParam.Ref_BANK_ID = string.IsNullOrEmpty(RefBank) ? string.Empty : RefBank;
                    string RefBranch = string.IsNullOrEmpty(model.ITV_Txt_Trf_Branch) ? string.Empty : model.ITV_Txt_Trf_Branch;
                    //  If ITV_iTrans_Type.ToUpper = "DEBIT" Then RefBranch = BE_Bank_Branch.Text  'commented for testing
                    InParam.Ref_Branch = RefBranch;
                    InParam.Ref_No = string.IsNullOrEmpty(model.ITV_Txt_Ref_No) ? string.Empty : model.ITV_Txt_Ref_No;
                    if (IsDate(model.ITV_Txt_Ref_Date))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.ITV_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = string.Empty;
                    }
                    if (IsDate(model.ITV_Txt_Ref_CDate))
                    {
                        InParam.Ref_CDate = Convert.ToDateTime(model.ITV_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_CDate = string.Empty;
                    }

                    // InParam.Ref_Date = Me.ITV_Txt_Ref_Date.Text
                    // InParam.Ref_CDate = Me.ITV_Txt_Ref_CDate.Text
                    InParam.Ref_Others = string.IsNullOrEmpty(model.ITV_Txt_DD_Fr_Chq_No) ? string.Empty : model.ITV_Txt_DD_Fr_Chq_No;
                    InParam.MTBankID = string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) ? string.Empty : model.ITV_GLookUp_TrfBankList;
                    string MTAccNo = string.IsNullOrEmpty(model.ITV_Txt_Trf_ANo) ? string.Empty : model.ITV_Txt_Trf_ANo;
                    // If ITV_iTrans_Type.ToUpper = "DEBIT" Then MTAccNo = BE_Bank_Acc_No.Text
                    InParam.MTAccNo = MTAccNo;
                    InParam.AB_ID_1 = Tr_AB_ID_1;
                    InParam.AB_ID_2 = Tr_AB_ID_2;
                    InParam.Narration = string.IsNullOrEmpty(model.ITV_Txt_Narration) ? string.Empty : model.ITV_Txt_Narration;
                    InParam.Remarks = string.IsNullOrEmpty(model.ITV_Txt_Remarks) ? string.Empty : model.ITV_Txt_Remarks;
                    InParam.Reference = string.IsNullOrEmpty(model.ITV_Txt_Reference) ? string.Empty : model.ITV_Txt_Reference;
                    InParam.MasterTxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                    InParam.Sr_No = 1;
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = string.IsNullOrEmpty(model.xID_1) ? string.Empty : model.xID_1;
                    InParam.Cross_Ref_ID = xCROSS_REF_ID;

                    InNewParam.param_InsertEP1 = InParam;
                    // Purpose
                    Parameter_InsertPurpose_VoucherInternalTransfer InPurpose = new Parameter_InsertPurpose_VoucherInternalTransfer();
                    InPurpose.TxnID = string.IsNullOrEmpty(model.xID_1) ? string.Empty : model.xID_1;
                    InPurpose.PurposeID = string.IsNullOrEmpty(model.ITV_GLookUp_PurList) ? string.Empty : model.ITV_GLookUp_PurList;
                    if (model.ITV_Txt_Amount != null)
                    {
                        InPurpose.Amount = double.Parse(model.ITV_Txt_Amount.ToString());
                    }
                    else
                    {
                        InPurpose.Amount = 0.00;
                    }

                    InPurpose.Status_Action = Convert.ToString((int)Common.Record_Status._Completed);
                    InPurpose.RecID = Guid.NewGuid().ToString();
                    InNewParam.param_InsertPurposeEp1 = InPurpose;
                    // ENTRY PART #2
                    if (model.ITV_Txt_Bank_Chg != null && model.ITV_Txt_Bank_Chg > 0)
                    {
                        model.xID_2 = Guid.NewGuid().ToString();
                        Parameter_Insert_VoucherInternalTransfer InParams = new Parameter_Insert_VoucherInternalTransfer();
                        InParams.TransCode = (int)Common.Voucher_Screen_Code.Internal_Transfer;
                        InParams.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                        if (IsDate(model.ITV_Txt_V_Date))
                        {
                            InParams.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.TDate = string.IsNullOrEmpty(model.ITV_Txt_V_Date.ToString()) ? string.Empty : model.ITV_Txt_V_Date.ToString();
                        }
                        InParams.ItemID = BC_Item_ID;
                        InParams.Type = "DEBIT";
                        InParams.Cr_Led_ID = BC_Cr_Led_id;
                        InParams.Dr_Led_ID = BC_Dr_Led_id;
                        InParams.SUB_Cr_Led_ID = BC_Sub_Cr_Led_ID;
                        InParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID;
                        if (model.ITV_Txt_Bank_Chg != null)
                        {
                            InParams.Amount = Convert.ToDouble(model.ITV_Txt_Bank_Chg);
                        }
                        else
                        {
                            InParams.Amount = 0.00;
                        }
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                        {
                            InParams.Mode = "DD";
                        }
                        else
                        {
                            InParams.Mode = model.ITV_Cmd_Mode;
                        }
                        InParams.Ref_BANK_ID = string.Empty;
                        InParams.Ref_Branch = string.Empty;
                        InParams.Ref_No = string.IsNullOrEmpty(model.ITV_Txt_Ref_No) ? string.Empty : model.ITV_Txt_Ref_No;
                        if (IsDate(model.ITV_Txt_Ref_Date))
                        {
                            InParams.Ref_Date = Convert.ToDateTime(model.ITV_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_Date = string.Empty;
                        }

                        if (IsDate(model.ITV_Txt_Ref_CDate))
                        {
                            InParams.Ref_CDate = Convert.ToDateTime(model.ITV_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_CDate = string.Empty;
                        }
                        InParams.Ref_Others = string.IsNullOrEmpty(model.ITV_Txt_DD_Fr_Chq_No) ? string.Empty : model.ITV_Txt_DD_Fr_Chq_No.ToString();
                        InParams.MTBankID = string.Empty;
                        InParams.MTAccNo = string.Empty;
                        InParams.AB_ID_1 = string.Empty;
                        InParams.AB_ID_2 = string.Empty;
                        InParams.Narration = "Auto Generated entry from Internal Transfer.";
                        InParams.Remarks = string.Empty;
                        InParams.Reference = string.Empty;
                        InParams.MasterTxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                        InParams.Sr_No = 2;
                        InParams.Status_Action = Status_Action;
                        InParams.RecID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                        InParams.Cross_Ref_ID = xCROSS_REF_ID;
                        InNewParam.param_InsertEP2 = InParams;
                        // Purpose
                        Parameter_InsertPurpose_VoucherInternalTransfer InPurp = new Parameter_InsertPurpose_VoucherInternalTransfer();
                        InPurp.TxnID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                        InPurp.PurposeID = string.IsNullOrEmpty(model.ITV_GLookUp_PurList) ? string.Empty : model.ITV_GLookUp_PurList;
                        if (model.ITV_Txt_Bank_Chg != null)
                        {
                            InPurp.Amount = double.Parse(model.ITV_Txt_Bank_Chg.ToString());
                        }
                        else
                        {
                            InPurp.Amount = 0.00;
                        }
                        InPurp.Status_Action = Status_Action;
                        InPurp.RecID = Guid.NewGuid().ToString();
                        InNewParam.parma_InsertPurposeEP2 = InPurp;
                    }

                    if (model.USE_CROSS_REF)
                    {
                        Param_Voucher_InternalTransfer_Update_CrossReference UpCrossRef = new Param_Voucher_InternalTransfer_Update_CrossReference();
                        UpCrossRef.Cross_Ref_ID = string.IsNullOrEmpty(model.xID_1.ToString()) ? string.Empty : model.xID_1.ToString();
                        UpCrossRef.RecID = string.IsNullOrEmpty(model.CROSS_M_ID) ? string.Empty : model.CROSS_M_ID;
                        InNewParam.param_UpdateCrossRef = UpCrossRef;
                    }
                    if (model.ITV_Txt_Slip_No != null && model.ITV_Txt_Slip_No > 0)
                    {
                        Parameter_InsertSlip_VoucherInternalTransfer inSlip = new Parameter_InsertSlip_VoucherInternalTransfer();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        if (model.ITV_Txt_Slip_No != null)
                        {
                            inSlip.SlipNo = Convert.ToInt32(model.ITV_Txt_Slip_No);
                        }
                        else
                        {
                            inSlip.SlipNo = 0;
                        }
                        inSlip.TxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID.ToString();
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        InNewParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Insert Process
                    if (model.ITV_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.ITV_SplVchrReferenceSelected.Split(',');
                        var splitLength = SplVchrRefsSplit.Length;
                        if (splitLength > 0)
                        {
                            Parameter_InsertSplVchrRef_Vouchers[] InsertSplVchrRefs = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers[splitLength];
                            for (int j = 0; j < splitLength; j++)
                            {
                                Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers _SplVchr = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers();
                                _SplVchr.Task_Name = SplVchrRefsSplit[j];
                                InsertSplVchrRefs[j] = _SplVchr;
                            }
                            InNewParam.InsertSplVchrRefs = InsertSplVchrRefs;
                        }
                    }

                    if (!(bool)BASE._Internal_Tf_Voucher_DBOps.Insert_InternalTransfer_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        jsonParam.title = model.TitleX;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK =model.xMID+ model.xID_1,
                            IsVolumeCenter = BASE._IsVolumeCenter,
                            xID_1 = model.xID_1
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Txn_Update_InternalTransfer EditParam = new Param_Txn_Update_InternalTransfer();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    string Dr_Led_id = string.Empty;
                    string Cr_Led_id = string.Empty;
                    string Sub_Dr_Led_ID = string.Empty;
                    string Sub_Cr_Led_ID = string.Empty;
                    string Tr_AB_ID_1 = string.Empty;
                    string Tr_AB_ID_2 = string.Empty;
                    if (model.ITV_iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.ITV_iLed_ID;
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || model.ITV_Cmd_Mode.ToUpper() == "CASH TO BANK" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                        {
                            Cr_Led_id = "00080";
                        }
                        else
                        {
                            Cr_Led_id = "00079";
                            Sub_Cr_Led_ID = string.IsNullOrEmpty(model.ITV_GLookUp_BankList) ? string.Empty : model.ITV_GLookUp_BankList;
                        }
                        Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                        Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                    }
                    else
                    {
                        Cr_Led_id = string.IsNullOrEmpty(model.ITV_iLed_ID) ? string.Empty : model.ITV_iLed_ID;
                        if (model.ITV_Cmd_Mode.ToString().ToUpper() == "CASH" || (model.ITV_Cmd_Mode.ToString().ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                        {
                            Dr_Led_id = "00080";
                        }
                        else
                        {
                            Dr_Led_id = "00079";
                            Sub_Dr_Led_ID = string.IsNullOrEmpty(model.ITV_GLookUp_BankList) ? string.Empty : model.ITV_GLookUp_BankList;
                        }

                        Tr_AB_ID_1 = string.IsNullOrEmpty(model.ITV_GLookUp_FrCen_List) ? string.Empty : model.ITV_GLookUp_FrCen_List;
                        Tr_AB_ID_2 = string.IsNullOrEmpty(model.ITV_GLookUp_ToCen_List) ? string.Empty : model.ITV_GLookUp_ToCen_List;
                    }

                    // +------------------TDS Deduction Reference-------------------+

                    if (model.ITV_GLookUp_ItemList == "d611ce4a-756a-42d0-b7ee-51dd7add7263" && BASE._open_Year_ID > 1415)
                    {
                        if (model.ITV_TDS_Open == false)
                        {
                            //jsonParam.message = "OPEN_TDS_SENT_GRID";
                            //jsonParam.result = false;
                            //jsonParam.closeform = false;
                            //jsonParam.title = "Internal Transfer...";
                            //return Json(new
                            //{
                            //    jsonParam
                            //}, JsonRequestBehavior.AllowGet);
                        }
                        if (TDS_Deduction_List != null && TDS_Deduction_List.Count > 0)//Redmine Bug #132899 fixed
                        {                          
                            List<Parameter_InsertTDSDeduction_VoucherInternalTransfer> inTDSParam = new List<Parameter_InsertTDSDeduction_VoucherInternalTransfer>();
                            for (int i = 0; i <= TDS_Deduction_List.Count; i++)
                            {
                                Parameter_InsertTDSDeduction_VoucherInternalTransfer inTDSDeduct = new Parameter_InsertTDSDeduction_VoucherInternalTransfer();
                                inTDSDeduct.RecID = System.Guid.NewGuid().ToString();
                                inTDSDeduct.RefTxnID = TDS_Deduction_List[i].RefMID;
                                inTDSDeduct.TDS_Sent = Convert.ToDecimal(TDS_Deduction_List[i].TDS_Ded);
                                inTDSDeduct.TxnMID = model.xMID;
                                inTDSParam.Add(inTDSDeduct);
                            }
                            EditParam.param_Insert_TDSDed = inTDSParam.ToArray();
                        }
                    }
                    // +-----------------END------------------+
                    // Master Entry
                    Parameter_UpdateMasterInfo_VoucherInternalTransfer UpMInfo = new Parameter_UpdateMasterInfo_VoucherInternalTransfer();
                    UpMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer;
                    UpMInfo.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                    if (IsDate(model.ITV_Txt_V_Date))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = model.ITV_Txt_V_Date.ToString();
                    }
                    UpMInfo.PartyID = Tr_AB_ID_1;
                    UpMInfo.SubTotal = double.Parse(model.ITV_Txt_Amount.ToString());
                    if (model.ITV_Cmd_Mode.ToUpper() == "CASH" || (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false))
                    {
                        UpMInfo.Cash = double.Parse(model.ITV_Txt_Amount.ToString());
                    }
                    else
                    {
                        UpMInfo.Cash = 0;
                    }
                    if (model.ITV_Cmd_Mode.ToUpper() != "CASH")
                    {
                        if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == false)
                        {
                            UpMInfo.Bank = 0;
                        }
                        else if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                        {
                            UpMInfo.Bank = double.Parse(model.ITV_Txt_Amount.ToString());
                        }
                        else
                        {
                            UpMInfo.Bank = double.Parse(model.ITV_Txt_Amount.ToString());
                        }
                    }
                    else
                    {
                        UpMInfo.Bank = 0;
                    }
                    UpMInfo.RecID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                    EditParam.param_UpdateMaster = UpMInfo;
                    // ENTRY PART #1
                    string xCROSS_REF_ID = string.Empty;
                    if (model.USE_CROSS_REF)
                    {
                        xCROSS_REF_ID = " '" + model.CROSS_REF_ID + "' ";
                    }
                    else
                    {
                        xCROSS_REF_ID = " NULL ";
                    }
                    Parameter_Update_VoucherInternalTransfer UpParam = new Parameter_Update_VoucherInternalTransfer();
                    UpParam.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                    if (IsDate(model.ITV_Txt_V_Date))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.ITV_Txt_V_Date.ToString();
                    }
                    UpParam.ItemID = string.IsNullOrEmpty(model.ITV_GLookUp_ItemList) ? string.Empty : model.ITV_GLookUp_ItemList;
                    UpParam.Type = string.IsNullOrEmpty(model.ITV_iTrans_Type) ? string.Empty : model.ITV_iTrans_Type;
                    UpParam.Cr_Led_ID = Cr_Led_id;
                    UpParam.Dr_Led_ID = Dr_Led_id;
                    UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    UpParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                    if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                    {
                        UpParam.Mode = "DD";
                    }
                    else
                    {
                        UpParam.Mode = model.ITV_Cmd_Mode;
                    }
                    string RefBank = string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) ? string.Empty : model.ITV_GLookUp_TrfBankList;
                    UpParam.RefBankID = RefBank;
                    string RefBranch = string.IsNullOrEmpty(model.ITV_Txt_Trf_Branch) ? string.Empty : model.ITV_Txt_Trf_Branch;
                    UpParam.RefBranch = RefBranch;
                    UpParam.Ref_No = string.IsNullOrEmpty(model.ITV_Txt_Ref_No) ? string.Empty : model.ITV_Txt_Ref_No;
                    UpParam.Ref_Others = string.IsNullOrEmpty(model.ITV_Txt_DD_Fr_Chq_No) ? string.Empty : model.ITV_Txt_DD_Fr_Chq_No; ;
                    if (IsDate(model.ITV_Txt_Ref_Date))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.ITV_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_Date = model.ITV_Txt_Ref_Date.ToString();
                    }

                    if (IsDate(model.ITV_Txt_Ref_CDate))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.ITV_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.ITV_Txt_Ref_CDate.ToString();
                    }
                    UpParam.Amount = double.Parse(model.ITV_Txt_Amount.ToString());
                    UpParam.AB_ID_1 = Tr_AB_ID_1;
                    UpParam.AB_ID_2 = Tr_AB_ID_2;
                    UpParam.MT_Bank_ID = string.IsNullOrEmpty(model.ITV_GLookUp_TrfBankList) ? string.Empty : model.ITV_GLookUp_TrfBankList;
                    string RefAccNo = string.IsNullOrEmpty(model.ITV_Txt_Trf_ANo) ? string.Empty : model.ITV_Txt_Trf_ANo;
                    UpParam.MT_AccNo = RefAccNo;
                    UpParam.Narration = string.IsNullOrEmpty(model.ITV_Txt_Narration) ? string.Empty : model.ITV_Txt_Narration;
                    UpParam.Remarks = string.IsNullOrEmpty(model.ITV_Txt_Remarks) ? string.Empty : model.ITV_Txt_Remarks;
                    UpParam.Reference = string.IsNullOrEmpty(model.ITV_Txt_Reference) ? string.Empty : model.ITV_Txt_Reference;
                    UpParam.RecID = string.IsNullOrEmpty(model.xID_1) ? string.Empty : model.xID_1;
                    EditParam.param_UpdateEP1 = UpParam;
                    // Purpose
                    Parameter_UpdatePurpose_VoucherInternalTransfer UpPurpose = new Parameter_UpdatePurpose_VoucherInternalTransfer();
                    UpPurpose.PurposeID = string.IsNullOrEmpty(model.ITV_GLookUp_PurList) ? string.Empty : model.ITV_GLookUp_PurList;
                    if (model.ITV_Txt_Bank_Chg != null)
                    {
                        UpPurpose.Amount = double.Parse(model.ITV_Txt_Bank_Chg.ToString());
                    }
                    else
                    {
                        UpPurpose.Amount = 0.00;
                    }
                    UpPurpose.RecID = string.IsNullOrEmpty(model.xID_1) ? string.Empty : model.xID_1;
                    EditParam.param_UpdatePurposeEP1 = UpPurpose;
                    // ENTRY PART #2
                    if (model.ITV_Txt_Bank_Chg != null && model.ITV_Txt_Bank_Chg > 0)
                    {
                        if (model.xID_2.ToString().Trim().Length > 0)
                        {
                            Parameter_Update_VoucherInternalTransfer UpParams = new Parameter_Update_VoucherInternalTransfer();
                            UpParams.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                            if (IsDate(model.ITV_Txt_V_Date))
                            {
                                UpParams.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                UpParams.TDate = string.Empty;
                            }

                            // UpParams.TDate = Me.ITV_Txt_V_Date.Text
                            UpParams.ItemID = BC_Item_ID;
                            UpParams.Type = "DEBIT";
                            UpParams.Cr_Led_ID = BC_Cr_Led_id;
                            UpParams.Dr_Led_ID = BC_Dr_Led_id;
                            UpParams.Sub_Cr_Led_ID = BC_Sub_Cr_Led_ID;
                            UpParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID;
                            if (model.ITV_Cmd_Mode.ToUpper() == "CASH TO DD" && model.USE_CROSS_REF == true)
                            {
                                UpParams.Mode = "DD";
                            }
                            else
                            {
                                UpParams.Mode = model.ITV_Cmd_Mode;
                            }
                            UpParams.RefBankID = string.Empty;
                            UpParams.RefBranch = string.Empty;
                            UpParams.Ref_No = string.IsNullOrEmpty(model.ITV_Txt_Ref_No) ? string.Empty : model.ITV_Txt_Ref_No;
                            UpParams.Ref_Others = string.IsNullOrEmpty(model.ITV_Txt_DD_Fr_Chq_No) ? string.Empty : model.ITV_Txt_DD_Fr_Chq_No;
                            if (IsDate(model.ITV_Txt_Ref_Date))
                            {
                                UpParams.Ref_Date = Convert.ToDateTime(model.ITV_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                UpParams.Ref_Date = model.ITV_Txt_Ref_Date.ToString();
                            }

                            if (IsDate(model.ITV_Txt_Ref_CDate))
                            {
                                UpParams.Ref_ChequeDate = Convert.ToDateTime(model.ITV_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                UpParams.Ref_ChequeDate = model.ITV_Txt_Ref_CDate.ToString();
                            }
                            if (model.ITV_Txt_Bank_Chg != null)
                            {
                                UpParams.Amount = double.Parse(model.ITV_Txt_Bank_Chg.ToString());
                            }
                            else
                            {
                                UpParams.Amount = 0.00;
                            }
                            UpParams.AB_ID_1 = string.Empty;
                            UpParams.AB_ID_2 = string.Empty;
                            UpParams.MT_Bank_ID = string.Empty;
                            UpParams.MT_AccNo = string.Empty;
                            UpParams.Narration = "Auto Generated entry from Internal Transfer.";
                            UpParams.Remarks = string.Empty;
                            UpParams.Reference = string.Empty;
                            UpParams.RecID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                            EditParam.param_UpdateEP2 = UpParams;
                            // Purpose
                            Parameter_UpdatePurpose_VoucherInternalTransfer UpPurp = new Parameter_UpdatePurpose_VoucherInternalTransfer();
                            UpPurp.PurposeID = string.IsNullOrEmpty(model.ITV_GLookUp_PurList) ? string.Empty : model.ITV_GLookUp_PurList;
                            UpPurp.Amount = double.Parse(model.ITV_Txt_Amount.ToString());
                            UpPurp.RecID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                            EditParam.param_UpdatePurposeEP2 = UpPurp;
                        }
                        else
                        {
                            model.xID_2 = Guid.NewGuid().ToString();
                            Parameter_Insert_VoucherInternalTransfer InParams = new Parameter_Insert_VoucherInternalTransfer();
                            InParams.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer;
                            InParams.VNo = string.IsNullOrEmpty(model.ITV_Txt_V_NO) ? string.Empty : model.ITV_Txt_V_NO;
                            if (IsDate(model.ITV_Txt_V_Date))
                            {
                                InParams.TDate = Convert.ToDateTime(model.ITV_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.TDate = model.ITV_Txt_V_Date.ToString();
                            }
                            InParams.ItemID = BC_Item_ID;
                            InParams.Type = "DEBIT";
                            InParams.Cr_Led_ID = BC_Cr_Led_id;
                            InParams.Dr_Led_ID = BC_Dr_Led_id;
                            InParams.SUB_Cr_Led_ID = BC_Sub_Cr_Led_ID;
                            InParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID;
                            if (model.ITV_Txt_Bank_Chg != null)
                            {
                                InParams.Amount = double.Parse(model.ITV_Txt_Bank_Chg.ToString());
                            }
                            else
                            {
                                InParams.Amount = 0.00;
                            }
                            InParams.Mode = "OTHERS";
                            InParams.Ref_BANK_ID = string.Empty;
                            InParams.Ref_Branch = string.Empty;
                            InParams.Ref_No = string.Empty;
                            InParams.Ref_Date = string.Empty;
                            InParams.Ref_CDate = string.Empty;
                            InParams.Ref_Others = string.Empty;
                            InParams.MTBankID = string.Empty;
                            InParams.MTAccNo = string.Empty;
                            InParams.AB_ID_1 = string.Empty;
                            InParams.AB_ID_2 = string.Empty;
                            InParams.Narration = "Auto Generated entry from Internal Transfer.";
                            InParams.Remarks = string.Empty;
                            InParams.Reference = string.Empty;
                            InParams.MasterTxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                            InParams.Sr_No = 2;
                            InParams.Status_Action = Status_Action;
                            InParams.RecID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                            InParams.Cross_Ref_ID = xCROSS_REF_ID;
                            EditParam.param_InsertEP2 = InParams;
                            // Purpose
                            Parameter_InsertPurpose_VoucherInternalTransfer InPurpose = new Parameter_InsertPurpose_VoucherInternalTransfer();
                            InPurpose.TxnID = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                            InPurpose.PurposeID = string.IsNullOrEmpty(model.ITV_GLookUp_PurList) ? string.Empty : model.ITV_GLookUp_PurList;
                            if (model.ITV_Txt_Bank_Chg != null)
                            {
                                InPurpose.Amount = double.Parse(model.ITV_Txt_Bank_Chg.ToString());
                            }
                            else
                            {
                                InPurpose.Amount = 0.00;
                            }
                            InPurpose.Status_Action = Status_Action;
                            InPurpose.RecID = System.Guid.NewGuid().ToString();
                            EditParam.param_InsertPurposeEP2 = InPurpose;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.xID_2) && model.xID_2.Trim().Length > 0)
                        {
                            EditParam.ID2_DeletePurpose = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                            EditParam.ID2_Delete = string.IsNullOrEmpty(model.xID_2) ? string.Empty : model.xID_2;
                        }
                    }
                    EditParam.ID_DeleteSlip = model.xMID;
                    if (model.ITV_Txt_Slip_No != null && double.Parse(model.ITV_Txt_Slip_No.ToString()) > 0)
                    {
                        Parameter_InsertSlip_VoucherInternalTransfer inSlip = new Parameter_InsertSlip_VoucherInternalTransfer();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        if (model.ITV_Txt_Slip_No != null)
                        {
                            inSlip.SlipNo = Convert.ToInt32(model.ITV_Txt_Slip_No.ToString());
                        }
                        else
                        {
                            inSlip.SlipNo = 0;
                        }
                        inSlip.TxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        EditParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Update Process               
                    if (model.ITV_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.ITV_SplVchrReferenceSelected.Split(',');
                        var splitLength = SplVchrRefsSplit.Length;
                        if (splitLength > 0)
                        {
                            Parameter_InsertSplVchrRef_Vouchers[] UpdateSplVchrRefs = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers[splitLength];
                            for (int j = 0; j < splitLength; j++)
                            {
                                Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers _SplVchr = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers();
                                _SplVchr.Task_Name = SplVchrRefsSplit[j];
                                UpdateSplVchrRefs[j] = _SplVchr;
                            }
                            EditParam.UpdateSplVchrRefs = UpdateSplVchrRefs;
                        }
                    }

                    if ((bool)BASE._Internal_Tf_Voucher_DBOps.Update_InternalTransfer_Txn(EditParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        jsonParam.title = model.TitleX;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = model.xMID + model.xID_1,
                            xID_1=model.xID_1
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Txn_Delete_InternalTransfer DelParam = new Param_Txn_Delete_InternalTransfer();
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    DelParam.ID1_DeletePurpose = model.xID_1 ?? "";
                    DelParam.ID2_DeletePurpose = model.xID_2 ?? "";
                    DelParam.ID1_Delete = model.xID_1 ?? "";
                    DelParam.ID2_Delete = model.xID_2 ?? "";
                    DelParam.MID_DeleteMaster = model.xMID ?? "";
                    DelParam.ID_DeleteSlip = model.xMID ?? "";
                    DelParam.ID_DeleteTDSDeduction = model.xMID ?? "";
                    if ((bool)BASE._Internal_Tf_Voucher_DBOps.Delete_InternalTransfer_Txn(DelParam))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        jsonParam.title = model.TitleX;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.result = false;
                    jsonParam.closeform = false;
                    jsonParam.title = "Error!!";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.result = false;
                jsonParam.title = model.TitleX;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }


        #region Frm_I_Transfer_Pending
        public ActionResult Frm_I_Transfer_Pending()
        {
            ViewBag.Open_Cen_Id = BASE._open_Cen_ID;
            ViewBag.Open_Cen_Rec_Id = BASE._open_Cen_Rec_ID;
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View();
        }//Redmine bug 132871 fixed

        public ActionResult Frm_I_Transfer_Pending_Grid(string command, string TO_CEN_NAME = "", int TO_CEN_ID = 0, bool GOClick = false)
        {
            if (IT_Pending_data == null || command == "REFRESH")
            {
                DataSet p1 = new DataSet();
                if (BASE._open_Cen_ID == 4216)
                {
                    int? CenID = GOClick==false?0: (int?)null;
                    if (TO_CEN_NAME != "All Centres" && string.IsNullOrWhiteSpace(TO_CEN_NAME)==false)
                    {
                        CenID = TO_CEN_ID;
                    }
                    p1 = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, CenID);
                }
                else
                {
                    p1 = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, null);
                }
                if (p1 == null)
                {
                    return null;
                }
                List<PendingInternalTransferInfo> pInternalItems = new List<PendingInternalTransferInfo>();
                pInternalItems = (from DataRow row in p1.Tables[0].AsEnumerable()

                                  select new PendingInternalTransferInfo
                                  {
                                      Status = row["Status"].ToString(),
                                      Centre_Name = row["Centre Name"].ToString(),
                                      Centre_UID = row["Centre UID"].ToString(),
                                      No = Convert.ToInt32(row["No."]),
                                      Zone = row["Zone"].ToString(),
                                      Sub_Zone = row["Sub Zone"].ToString(),
                                      CEN_ID = row["CEN_ID"].ToString(),
                                      Description = row["Description"].ToString(),
                                      ITEM_ID = row["ITEM_ID"].ToString(),
                                      Date = (DateTime)row["Date"],
                                      Mode = row["Mode"].ToString(),
                                      Amount = Convert.ToDouble(row["Amount"]),
                                      BI_ID = row["BI_ID"].ToString(),
                                      Bank_Name = row["Bank Name"].ToString(),
                                      Branch_Name = row["Branch Name"].ToString(),
                                      Bank_Ac_No = row["Bank A/c. No."].ToString(),
                                      Incharge = row["Incharge"].ToString(),
                                      Contact_No = row["Contact No."].ToString(),
                                      Purpose = row["Purpose"].ToString(),
                                      PUR_ID = row["PUR_ID"].ToString(),
                                      REF_BI_ID = row["REF_BI_ID"].ToString(),
                                      Ref_Branch = row["Ref.Branch"].ToString(),
                                      Ref_No = row["Ref.No."].ToString(),
                                      Ref_Date = (row["Ref.Date"].ToString()).Length>0?Convert.ToDateTime(row["Ref.Date"]).ToString("dd-MM-yyyy"): row["Ref.Date"].ToString(),
                                      Ref_Others = row["Ref.Others"].ToString(),
                                      ID = row["ID"].ToString(),
                                      M_ID = row["M_ID"].ToString(),
                                      Ref_Bank_AccNo = row["Ref_Bank_AccNo"].ToString(),
                                      Narration = row["Narration"].ToString(),
                                      Add_By = row["Add By"].ToString(),
                                      Add_Date = row["Add Date"].ToString(),
                                      Edit_By = row["Edit By"].ToString(),
                                      Edit_Date = row["Edit Date"].ToString(),
                                      Action_Status = row["Action Status"].ToString(),
                                      Action_By = row["Action By"].ToString(),
                                      Action_Date = row["Action Date"].ToString(),
                                  }).ToList();
                var FinalData = pInternalItems.ToList();
                IT_Pending_data = FinalData;
            }
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();//Redmine bug 132871 fixed
            return PartialView(IT_Pending_data);
        }

        public ActionResult pInternalTransferCustomDataAction(string key)
        {
            var FinalData = IT_Pending_data;
            var pInternalTransfer = (PendingInternalTransferInfo)FinalData.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (pInternalTransfer != null)
            {
                itstr = pInternalTransfer.Status + "![" + pInternalTransfer.Centre_Name + "![" + pInternalTransfer.Centre_UID + "![" + pInternalTransfer.No + "![" + pInternalTransfer.Zone + "![" +
                       pInternalTransfer.Sub_Zone + "![" + pInternalTransfer.CEN_ID + "![" + pInternalTransfer.Description + "![" + pInternalTransfer.ITEM_ID + "![" + pInternalTransfer.Date + "![" +
                       pInternalTransfer.Mode + "![" + pInternalTransfer.Amount + "![" + pInternalTransfer.BI_ID + "![" + pInternalTransfer.Bank_Name + "![" + pInternalTransfer.Branch_Name + "![" +
                       pInternalTransfer.Bank_Ac_No + "![" + pInternalTransfer.Incharge + "![" + pInternalTransfer.Contact_No + "![" + pInternalTransfer.Purpose + "![" + pInternalTransfer.PUR_ID + "![" +
                       pInternalTransfer.REF_BI_ID + "![" + pInternalTransfer.Ref_Branch + "![" + pInternalTransfer.Ref_No + "![" + pInternalTransfer.Ref_Date + "![" + pInternalTransfer.Ref_Others + "![" +
                       pInternalTransfer.ID + "![" + pInternalTransfer.M_ID + "![" + pInternalTransfer.Ref_Bank_AccNo + "![" + pInternalTransfer.Narration + "![" + pInternalTransfer.Add_By + "![" +
                        pInternalTransfer.Add_Date + "![" + pInternalTransfer.Edit_By + "![" + pInternalTransfer.Edit_Date + "![" + pInternalTransfer.Action_Status + "![" + pInternalTransfer.Action_By + "![" +
                        pInternalTransfer.Action_Date;

            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public JsonResult DataNavigation(string ActionMethod = null, string Date = null, string Item_ID = null, string Mode = null, string CEN_ID = null, string BI_ID = null, string BI_BRANCH = null, string BI_ACC_NO = null, string REF_BI_ID = null, string REF_BRANCH = null, string REF_OTHERS = null, string BI_REF_NO = null, string BI_REF_DT = null, double? Amount = null, string PUR_ID = null, string ID = null, string M_ID = null, string EDIT_DATE = null, string REF_BANK_ACC_NO = null)
        {
            string _Date = Date;
            string _Item_ID = Item_ID;
            string _Mode = Mode;
            string _CEN_ID = CEN_ID;
            string _BI_ID = BI_ID;
            string _BI_BRANCH = BI_BRANCH;
            string _BI_ACC_NO = BI_ACC_NO;
            string _REF_BI_ID = REF_BI_ID;
            string _REF_BRANCH = REF_BRANCH;
            string _REF_OTHERS = REF_OTHERS;
            string _BI_REF_NO = BI_REF_NO;
            string _BI_REF_DT = BI_REF_DT;
            double? _Amount = Amount;
            string _PUR_ID = PUR_ID;
            string _ID = ID;
            string _EDIT_DATE = EDIT_DATE;
            string _REF_BANK_ACC_NO = REF_BANK_ACC_NO;


            return Json(new { Message = "Internal Transfer Entry Not Selected... !", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options_IT_Pending()
        {
            return View();
        }
        public void IT_Pending_SessionClear()
        {
            ClearBaseSession("_ITVPending");
        }

        #endregion Frm_I_Transfer_Pending

        #region Frm_I_Transfer_Tds_Sent
        public ActionResult Frm_I_Transfer_Tds_Sent(string xMID = null, double? ITV_Txt_Amount = null)
        {
            ViewBag.ITR_TDS_xMID = xMID;
            ViewBag.Amount = ITV_Txt_Amount;
            DataTable p1 = BASE._Payment_DBOps.GetTDS_Deducted_Not_Sent(xMID);
            if (p1 == null)
            {
                return null;
            }
            List<InternalTransferTdsSentInfo> TdsSentInternalItems = new List<InternalTransferTdsSentInfo>();
            TdsSentInternalItems = (from DataRow row in p1.AsEnumerable()

                                    select new InternalTransferTdsSentInfo
                                    {
                                        Txn_Date = Convert.ToDateTime(row["Txn_Date"]),
                                        Party = row["Party"].ToString(),
                                        Dr_Amount = Convert.ToDecimal(row["Dr Amount"]),
                                        TDS_Deducted = Convert.ToInt32(row["TDS_Deducted"]),
                                        Remaining_Amount = Convert.ToInt32(row["Remaining_Amount"]),
                                        TDS_Already_Sent = Convert.ToInt32(row["TDS_Already_Sent"]),
                                        TDS_Send = Convert.ToInt32(row["TDS_Send"]),
                                        REC_ID = row["REC_ID"].ToString(),
                                    }).ToList();
            var FinalData = TdsSentInternalItems.ToList();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            TdsSentInternalTransfer_ExportData = FinalData;
            return PartialView(FinalData);
        }
        public ActionResult Frm_I_Transfer_Tds_Sent_Grid(string command, string xMID = "")
        {
            ViewBag.ITR_TDS_xMID = xMID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (TdsSentInternalTransfer_ExportData == null || command == "REFRESH")
            {
                DataTable p1 = BASE._Payment_DBOps.GetTDS_Deducted_Not_Sent(xMID);
                if (p1 == null)
                {
                    return null;
                }
                List<InternalTransferTdsSentInfo> TdsSentInternalItems = new List<InternalTransferTdsSentInfo>();
                TdsSentInternalItems = (from DataRow row in p1.AsEnumerable()

                                        select new InternalTransferTdsSentInfo
                                        {
                                            Txn_Date = Convert.ToDateTime(row["Txn_Date"]),
                                            Party = row["Party"].ToString(),
                                            Dr_Amount = Convert.ToDecimal(row["Dr Amount"]),
                                            TDS_Deducted = Convert.ToInt32(row["TDS_Deducted"]),
                                            Remaining_Amount = Convert.ToInt32(row["Remaining_Amount"]),
                                            TDS_Already_Sent = Convert.ToInt32(row["TDS_Already_Sent"]),
                                            TDS_Send = Convert.ToInt32(row["TDS_Send"]),
                                            REC_ID = row["REC_ID"].ToString(),
                                        }).ToList();
                var FinalData = TdsSentInternalItems.ToList();
                TdsSentInternalTransfer_ExportData = FinalData;
            }
            return View(TdsSentInternalTransfer_ExportData);
        }
        public void Frm_I_Transfer_Tds_Sent_SaveClick()
        {
            var data = TdsSentInternalTransfer_ExportData;
            TDS_Deduction_List = new List<Out_TDS>();
            for (var i = 0; i < data.Count; i++)
            {
                var TDS_Send_value = data[i].TDS_Send;
                if (TDS_Send_value > 0)
                {
                    Out_TDS inParam = new Out_TDS();
                    inParam.RefMID = data[i].REC_ID;
                    inParam.TDS_Ded = Convert.ToDouble(data[i].TDS_Send);
                    TDS_Deduction_List.Add(inParam);
                }
            }
        }
        public void ITR_TDS_SessionClear()
        {
            ClearBaseSession("_ITVTds");

        }
        public ActionResult Frm_Export_Options_TDS()
        {
            return PartialView();
        }
        [HttpPost]
        public void UpdateTDSValue(string key, string field, string value)
        {
            var data = TdsSentInternalTransfer_ExportData;
            var dataToEdit = data.Where(x => x.REC_ID == key).First();
            dataToEdit.TDS_Send = Convert.ToDecimal(value);
            TdsSentInternalTransfer_ExportData = data;
        }
        public ActionResult SetBalances(decimal MentionedInvoucher = 0)
        {
            var data = TdsSentInternalTransfer_ExportData;
            decimal OnScreenSelection = 0;
            for (var i = 0; i < data.Count; i++)
            {
                decimal TDS_Send_value = Convert.ToDecimal(data[i].TDS_Send);
                OnScreenSelection = OnScreenSelection + TDS_Send_value;
            }
            decimal NetBalance = MentionedInvoucher - OnScreenSelection;
            return Json(new
            {
                OnScreenSelection,
                NetBalance
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Frm_I_Transfer_Matching
        [HttpGet]
        public ActionResult Frm_I_Transfer_Matching(string to_Match_Txn_ID = "", string to_Cen_Rec_ID = "")
        {
            DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetList(to_Match_Txn_ID, Convert.ToInt32(to_Cen_Rec_ID));
            var data = ToList(d1);
            InternalTransfer_MatchingGrid_Data = data;
            ViewBag.Header = "Match Internal Transfer(" + BASE._open_UID_No + ")";
            ViewBag.to_Cen_Rec_ID = to_Cen_Rec_ID;
            ViewBag.to_Match_Txn_ID = to_Match_Txn_ID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(InternalTransfer_MatchingGrid_Data);
        }
        public ActionResult MatchClick(string xTemp_ID, string to_Match_Txn_ID)
        {
            if (BASE._Internal_Tf_Voucher_DBOps.MatchTransfers(to_Match_Txn_ID, xTemp_ID))
            {
                return Json(new
                {
                    message = "Transfer Entry Matched Successfully...",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "Sorry! Transfer Entry Could Not Be Matched Successfully...",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_I_Transfer_Matching_Grid(string command, string to_Match_Txn_ID, string to_Cen_Rec_ID)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (InternalTransfer_MatchingGrid_Data == null || command == "REFRESH")
            {
                DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetList(to_Match_Txn_ID, Convert.ToInt32(to_Cen_Rec_ID));
                var data = ToList(d1);
                InternalTransfer_MatchingGrid_Data = data;
            }
            return View(InternalTransfer_MatchingGrid_Data);
        }
        public ActionResult Frm_Export_Options_Match()
        {
            return View();
        }
        public void SessionClear_Match()
        {
            ClearBaseSession("_ITVMatch");
        }
        #endregion

        #region "Start--> LookupEdit Events"
        [HttpGet]
        public ActionResult LookUp_GetPurposeList(DataSourceLoadOptions loadOptions)
        {
            DataTable PurposeList = BASE._Internal_Tf_Voucher_DBOps.GetPurposes() as DataTable;
            var Purposedata = DatatableToModel.DataTabletoInternalTransferPurposeList(PurposeList);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Purposedata, loadOptions)), "application/json");
        }
        public ActionResult RefreshItemList()
        {
            string ITEM_APPLICABLE = "";
            if (BASE.Is_HQ_Centre)
            {

                ITEM_APPLICABLE = "'GENERAL','H.Q.'";
            }
            else
            {
                ITEM_APPLICABLE = "'GENERAL','CENTRE'";
            }
            DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetItemList(ITEM_APPLICABLE);
            if (d1 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            var Itemdata = DatatableToModel.DataTabletoInternalTransferItemList(dview.ToTable());
            ITV_ItemList = Itemdata;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetItemList_InternalTransfer(DataSourceLoadOptions loadOptions,bool DDRefresh=false)
        {
            if (ITV_ItemList == null|| DDRefresh==true)
            {
                RefreshItemList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ITV_ItemList, loadOptions)), "application/json");
        }
        public ActionResult RefreshTrfBankList(string Cmd_Mode = "", string iTrans_Type = "", string iTO_CEN_ID = "", string iFR_CEN_ID = "")
        {
            DataTable B2 = null;
            if (Cmd_Mode == "")
            {
                B2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(false);
            }
            else if (Cmd_Mode.ToString().ToUpper() == "CBS" || Cmd_Mode.ToString().ToUpper() == "RTGS" || Cmd_Mode.ToString().ToUpper() == "NEFT" || Cmd_Mode.ToString().ToUpper() == "CHEQUE" || Cmd_Mode.ToString().ToUpper() == "CASH TO BANK")
            {
                string XCEN_ID = "";
                if (iTrans_Type.ToUpper() == "DEBIT")
                {
                    XCEN_ID = iTO_CEN_ID??"";
                }
                else
                {
                    XCEN_ID = iFR_CEN_ID??"";
                }
                B2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(true, XCEN_ID);
            }
            else
            {
                B2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(false);
            }
            if (B2 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
           // DataView dview = new DataView(B2);
            //dview.Sort = "TRF_BI_BANK_NAME";
            var BankList = DatatableToModel.DataTabletoInternalTransferBankList(B2);
            ITV_TrfBankList = BankList;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetTrfBankList_InternalTransfer(DataSourceLoadOptions loadOptions, bool DDRefresh = false,string Cmd_Mode = "", string iTrans_Type = "", string iTO_CEN_ID = "", string iFR_CEN_ID = "")
        {
            if (ITV_TrfBankList == null||DDRefresh==true)
            {
                RefreshTrfBankList(Cmd_Mode, iTrans_Type, iTO_CEN_ID, iFR_CEN_ID);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ITV_TrfBankList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetTrfBankList_InternalTransfer_CHEQUE(string ITV_Cmd_Mode = "", string ITV_iTrans_Type = "", string iTO_CEN_ID = "", string iFR_CEN_ID = "")
        {
            DataTable B2 = null;
            if (((ITV_Cmd_Mode.ToString().ToUpper() == "CBS")
                        || (ITV_Cmd_Mode.ToString().ToUpper() == "RTGS")
                        || (ITV_Cmd_Mode.ToString().ToUpper() == "NEFT")
                        || (ITV_Cmd_Mode.ToString().ToUpper() == "CHEQUE")
                        || (ITV_Cmd_Mode.ToString().ToUpper() == "CASH TO BANK")))
            {
                string XCEN_ID = "";
                if ((ITV_iTrans_Type.ToUpper() == "DEBIT"))
                {
                    XCEN_ID = iTO_CEN_ID;
                }
                else
                {
                    XCEN_ID = iFR_CEN_ID;
                }

                B2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(true, XCEN_ID);
            }
            else
            {
                B2 = BASE._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(false);
            }

            if ((B2 == null))
            {
                //Base.HandleDBError_OnNothingReturned();
                return null;
            }

            DataView dview = new DataView(B2);
            // If ITV_iTrans_Type.ToUpper = "DEBIT" Then dview.RowFilter = "BA_FERA_ACC = 'NO'" 'FCRA banks not to be shown in Debit transactions 
            var TrfBankList = DatatableToModel.DataTabletoInternalTransferTrfBankList(B2);
            ViewData["DonationVoucherTrfBankList"] = TrfBankList;

            return PartialView(new DropdownDataReadonlyViewmodel());
            //this.ITV_GLookUp_TrfBankList.Properties.ValueMember = "TRF_BI_ID";
            //this.ITV_GLookUp_TrfBankList.Properties.DisplayMember = "TRF_BI_BANK_NAME";
            //this.ITV_GLookUp_TrfBankList.Properties.DataSource = dview;
            //this.GLookUp_TrfBankListView.RefreshData();
            //this.ITV_GLookUp_TrfBankList.Properties.Tag = "SHOW";
            //if ((dview.Count <= 0))
            //{
            //    //this.ITV_GLookUp_TrfBankList.Properties.Tag = "NONE";
            //    model.ITV_Txt_Trf_ANo = "";
            //    model.ITV_Txt_Trf_Branch = "";
            //}

            //if (((double.Parse(this.Tag) == Common_Lib.Common.Navigation_Mode._New)
            //            || (double.Parse(this.Tag) == Common_Lib.Common.Navigation_Mode._New_From_Selection)))
            //{
            //    this.ITV_GLookUp_TrfBankList.Properties.ReadOnly = false;
            //}
        }
        public ActionResult RefreshBankList()
        {
            DataTable BA_Table = BASE._Internal_Tf_Voucher_DBOps.GetBankAccounts();
            if (BA_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = Branch_IDs + "'" + xRow.Field<string>("BA_BRANCH_ID").ToString() + "',";
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, Branch_IDs.Trim().Length - 1) : Branch_IDs.Trim().ToString();
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            DataTable BB_Table = BASE._Internal_Tf_Voucher_DBOps.GetBranches(Branch_IDs);
            if (BB_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            var BuildData = from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
                            on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                            select new BankList
                            {
                                BANK_NAME = B.Field<string>("Name"),
                                BI_SHORT_NAME = B.Field<string>("BI_SHORT_NAME"),
                                BANK_BRANCH = B.Field<string>("Branch"),
                                BANK_ACC_NO = A.Field<string>("BA_ACCOUNT_NO"),
                                BA_ID = A.Field<string>("ID"),
                                BANK_ID = B.Field<string>("BANK_ID"),
                                REC_EDIT_ON = A.Field<DateTime>("REC_EDIT_ON")
                            };


            var Final_Data = BuildData.ToList();
            //Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            ITV_BankList = Final_Data;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetBankList_InternalTransfer(DataSourceLoadOptions loadOptions,bool DDRefresh=false)
        {
            if (ITV_BankList == null||DDRefresh==true)
            {
                RefreshBankList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ITV_BankList, loadOptions)), "application/json");
        }
        public ActionResult RefreshToCentreList(string iVoucher_Type = "", string iTrans_Type = "")
        {
            DataTable D1 = null;
            if (iVoucher_Type.ToUpper().Trim() == "INTERNAL TRANSFER WITH H.Q.")
            {
                if (iTrans_Type == "DEBIT")
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetToCenterList(HQ_IDs, "", "", "");
                }
                else
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
                }
            }
            else
            {
                if (iTrans_Type == "DEBIT")
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetToCenterList("", HQ_IDs, "", "'" + BASE._open_Cen_Rec_ID + "'");
                }
                else
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
                }
            }
            if (D1 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            DataView dview = new DataView(D1);
            dview.Sort = "TO_CEN_NAME,TO_UID";
            var ToCenterData = DatatableToModel.DataTabletoInternalTransferToCenterList(dview.ToTable());
            ITV_ToCenList = ToCenterData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_To_Centre_InternalTransfer(DataSourceLoadOptions loadOptions, bool DDRefresh = false, string iVoucher_Type = "", string iTrans_Type = "")
        {
            if (ITV_ToCenList == null||DDRefresh==true)
            {
                RefreshToCentreList(iVoucher_Type, iTrans_Type);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ITV_ToCenList, loadOptions)), "application/json");
        }
        public ActionResult RefreshFrCentreList(string iVoucher_Type = "", string iTrans_Type = "")
        {            
            DataTable D1 = null;
            if (iVoucher_Type.ToUpper().Trim() == "INTERNAL TRANSFER WITH H.Q.")
            {
                if (iTrans_Type == "DEBIT")
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetFrCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
                }
                else
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetFrCenterList(HQ_IDs, "", "", "");
                }
            }
            else
            {
                if (iTrans_Type == "DEBIT")
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetFrCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
                }
                else
                {
                    D1 = BASE._Internal_Tf_Voucher_DBOps.GetFrCenterList("", HQ_IDs, "", "'" + BASE._open_Cen_Rec_ID + "'");
                }
            }
            if (D1 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            DataView dview = new DataView(D1);
            dview.Sort = "FR_CEN_NAME,FR_UID";
            var FrCenterData = DatatableToModel.DataTabletoInternalTransferFromCenterList(dview.ToTable());
            ITV_FrCenList = FrCenterData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_Fr_Centre_InternalTransfer(DataSourceLoadOptions loadOptions,bool DDRefresh=false, string iVoucher_Type = "", string iTrans_Type = "")
        {
            if (ITV_FrCenList == null||DDRefresh==true)
            {
                RefreshFrCentreList(iVoucher_Type, iTrans_Type);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ITV_FrCenList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Cen_List_InternalTransfer(DataSourceLoadOptions loadOptions)
        {
            DataTable Centres = BASE._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "", "");
            if (Centres == null)
            {
                return null;
            }
            DataView dview = new DataView(Centres);
            DataRow ROW;
            ROW = Centres.NewRow();
            ROW["TO_CEN_NAME"] = "All Centres";
            dview.Table.Rows.InsertAt(ROW, 0);
            dview.Sort = "TO_CEN_NAME";
            var ToCenterData = DatatableToModel.DataTabletoInternalTransferCenterList(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ToCenterData, loadOptions)), "application/json");
        }

        #endregion

        public bool Get_Closed_Bank_Status(string xRecID)
        {
            bool Flag = false;
            string CR_LED_ID = "";
            string DR_LED_ID = "";
            string xTR_MODE = "";
            int xTR_CODE = 0;
            DataTable d4 = BASE._Voucher_DBOps.GetTransactionDetail(xRecID);
            if (d4.Rows.Count > 0)
            {
                foreach (DataRow xRow in d4.Rows)
                {
                    if (!Convert.IsDBNull(xRow["TR_SUB_CR_LED_ID"]))
                    {
                        CR_LED_ID = xRow["TR_SUB_CR_LED_ID"].ToString();
                    }
                    else { CR_LED_ID = ""; }
                    if (!Convert.IsDBNull(xRow["TR_SUB_DR_LED_ID"]))
                    {
                        DR_LED_ID = xRow["TR_SUB_DR_LED_ID"].ToString();
                    }
                    else { DR_LED_ID = ""; }
                    if (!Convert.IsDBNull(xRow["TR_CODE"]))
                    {
                        xTR_CODE = Convert.ToInt32(xRow["TR_CODE"]);
                    }
                    else { xTR_CODE = 0; }
                    if (!Convert.IsDBNull(xRow["TR_MODE"]))
                    {
                        xTR_MODE = xRow["TR_MODE"].ToString();
                    }
                    else { xTR_MODE = ""; }

                    if (xTR_CODE == 6 || xTR_CODE == 1 || xTR_MODE.ToUpper() != "CASH")
                    {
                        object MaxValue = null;
                        MaxValue = BASE._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID);
                        if (Convert.IsDBNull(MaxValue) || string.IsNullOrEmpty(MaxValue.ToString()))
                        {
                            Flag = false;
                            Closed_Bank_Account_No = "";
                        }
                        else
                        {
                            Flag = true;
                            Closed_Bank_Account_No = MaxValue.ToString();
                            break;
                        }
                    }
                }
            }
            return Flag;
        }
        [HttpPost]
        public ActionResult GetSlipCount(int SlipNo, string BankId, string xMID)
        {
            int count = (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(SlipNo, BankId, ClientScreen.Accounts_Voucher_Donation, xMID);
            return Json(new
            {
                count
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMaxOpenSlipNo(string BankId)
        {
            int count = (int)BASE._DepositSlipsDBOps.GetMaxOpenSlipNo(BankId, ClientScreen.Accounts_Voucher_Donation);
            return Json(new
            {
                count
            }, JsonRequestBehavior.AllowGet);
        }
        private bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public List<Return_InternalTransferRegister_Pending> ConvertToList(DataTable Dt)
        {
            var data = new List<Return_InternalTransferRegister_Pending>();
            if (Dt.Rows.Count == 0)
            {
                return data;
            }
            else
            {
                foreach (DataRow row in Dt.Rows)
                {
                    var newrow = new Return_InternalTransferRegister_Pending();
                    newrow.Status = row.Field<string>("Status");
                    newrow.Centre_Name = row.Field<string>("Centre Name");
                    newrow.Center_UID = row.Field<string>("Centre UID");
                    newrow.No = row.Field<string>("No.");
                    newrow.Zone = row.Field<string>("Zone");
                    newrow.Sub_Zone = row.Field<string>("Sub Zone");
                    newrow.Description = row.Field<string>("Description");
                    newrow.xDate = row.Field<DateTime?>("Date");
                    newrow.Mode = row.Field<string>("Mode");
                    newrow.Amount = row.Field<Decimal?>("Amount");
                    newrow.Bank_Name = row.Field<string>("Bank Name");
                    newrow.Branch_Name = row.Field<string>("Branch Name");
                    newrow.Bank_AC_No = row.Field<string>("Bank A/C. No.");
                    newrow.RefNo = row.Field<string>("Ref.No.");
                    newrow.RefDate = row.Field<DateTime?>("Ref.Date");
                    newrow.Purpose = row.Field<string>("Purpose");
                    newrow.ID = row.Field<string>("ID");
                    newrow.MID = row.Field<string>("M_ID");
                    newrow.AddBy = row.Field<string>("Add By");
                    newrow.AddDate = row.Field<DateTime>("Add Date");
                    newrow.EditBy = row.Field<string>("Edit By");
                    newrow.EditDate = row.Field<DateTime>("Edit Date");
                    newrow.ActionStatus = row.Field<string>("Action Status");
                    newrow.ActionBy = row.Field<string>("Action By");
                    newrow.ActionDate = row.Field<DateTime>("Action Date");
                    newrow.CEN_ID = row.Field<string>("CEN_ID");
                    newrow.ITEM_ID = row.Field<string>("ITEM_ID");
                    newrow.BI_ID = row.Field<string>("BI_ID");
                    newrow.Incharge = row.Field<string>("Incharge");
                    newrow.ContactNo = row.Field<string>("Contact No.");
                    newrow.PUR_ID = row.Field<string>("PUR_ID");
                    newrow.REF_BI_ID = row.Field<string>("REF_BI_ID");
                    newrow.Ref_Branch = row.Field<string>("Ref.Branch");
                    newrow.Ref_Others = row.Field<string>("Ref.Others");
                    newrow.Ref_Bank_AccNo = row.Field<string>("Ref_Bank_AccNo");
                    newrow.Narration = row.Field<string>("Narration");
                    data.Add(newrow);
                }
                return data;
            }
        }
        public List<InternalTransfer_Matching> ToList(DataTable Dt)
        {
            var data = new List<InternalTransfer_Matching>();
            if (Dt.Rows.Count == 0)
            {
                return data;
            }
            else
            {
                foreach (DataRow row in Dt.Rows)
                {
                    var newrow = new InternalTransfer_Matching();
                    newrow.Matched = row.Field<string>("Matched");
                    newrow.Entering_Centre = row.Field<string>("Entering Centre");
                    newrow.Item = row.Field<string>("Item");
                    newrow.Amount = row.Field<decimal>("Amount");
                    newrow.Date = row.Field<DateTime>("Date");
                    newrow.Mode = row.Field<string>("Mode");
                    newrow.RefNo = row.Field<string>("RefNo");
                    newrow.Receiving_Center = row.Field<string>("Receiving Center");
                    newrow.Paying_Center = row.Field<string>("Paying Center");
                    newrow.REC_ID = row.Field<string>("REC_ID");
                    data.Add(newrow);
                }
                return data;
            }
        }
        #region Export
        public ActionResult ExportTo()
        {
            GridViewExportFormat format = CommonFunctions.GetExportFormat(Request.Params["ExportFormat"]);
            if (GridViewExportHelper.ExportFormatsInfo.ContainsKey(format))
                return GridViewExportHelper.ExportFormatsInfo[format](GridViewExportHelper.ExportpInternalTransferGrid, Session["pInternalTransfer_ExportData"]);
            return RedirectToAction("Frm_I_Transfer_Pending");
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_ITVou");
        }
        #region Report
        public ActionResult ReportViewerPartial(bool isFromReportViewer = false)
        {
            if (!isFromReportViewer)
            {
                Session["pInternalTransferReport"] = null;
                Session["pInternalTransferReport"] = CreateReport();
            }
            return PartialView();
        }
        public ActionResult ReportViewerExportTo()
        {
            return ReportViewerExtension.ExportTo((XtraReport)Session["pInternalTransferReport"]);
        }

        public ActionResult GridViewExportTo()
        {
            return GridViewExtension.ExportToPdf(GridViewExportHelper.ExportpInternalTransferGrid, Session["pInternalTransfer_ExportData"]);
        }
        private XtraReport CreateReport()
        {
            PrintingSystem ps = new PrintingSystem();
            var xTopLeft_Header = "PendingInternalTransfer";
            var xTopCentre_Header = "UID: " + BASE._open_UID_No;
            var xTopRight_Header = "Year: " + BASE._open_Year_Name;

            var xBottomLeft_Footer = "Page [Page # of Pages #]";
            var xBottomRight_Footer = "Print On: [Date Printed], [Time Printed]";
            var xBottomCentre_Footer = "";

            PrintableComponentLink link1 = new PrintableComponentLink(ps);
            link1.Component = GridViewExtension.CreatePrintableObject(GridViewExportHelper.ExportpInternalTransferGrid, Session["pInternalTransfer_ExportData"]);
            link1.Landscape = true;
            link1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            link1.Margins = new System.Drawing.Printing.Margins(40, 40, 50, 40);
            link1.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth);

            link1.PrintingSystem.Document.Name = "PendingInternalTransfer";
            DevExpress.XtraPrinting.PageHeaderArea headerArea = new DevExpress.XtraPrinting.PageHeaderArea();
            headerArea.Font = new Font("Arial", 14, FontStyle.Regular);
            headerArea.LineAlignment = BrickAlignment.None;
            headerArea.Content.Add(xTopLeft_Header);
            headerArea.Content.Add(xTopCentre_Header);
            headerArea.Content.Add(xTopRight_Header);
            DevExpress.XtraPrinting.PageFooterArea footerArea = new DevExpress.XtraPrinting.PageFooterArea();
            footerArea.Font = new Font("Verdana", 8, FontStyle.Regular);
            footerArea.LineAlignment = BrickAlignment.Far;
            footerArea.Content.Add(xBottomLeft_Footer);
            footerArea.Content.Add(xBottomCentre_Footer);
            footerArea.Content.Add(xBottomRight_Footer);
            DevExpress.XtraPrinting.PageHeaderFooter HeaderFooter = new DevExpress.XtraPrinting.PageHeaderFooter(headerArea, footerArea);
            link1.PageHeaderFooter = HeaderFooter;

            link1.PrintingSystem.ExecCommand(PrintingSystemCommand.HandTool, new object[] {
                    true});
            link1.CreateDocument();

            MemoryStream stream = new MemoryStream();
            link1.PrintingSystem.SaveDocument(stream);

            XtraReport report = new XtraReport();
            report.PrintingSystem.LoadDocument(stream);

            /**/
            report.DisplayName = "PendingInternalTransfer";
            /**/

            return report;
        }
        #endregion Report

        #region devextreme
        public ActionResult Frm_I_Transfer_Reg_dx()
        {
            if (CheckRights(BASE, ClientScreen.Account_InternalTrf_Register, "List"))
            {             
                ViewBag.IsVolumeCentre = BASE._IsVolumeCenter;
                ViewBag.UserType = BASE._open_User_Type;
                ViewBag.IsunmatchTransfer = false;
                if (BASE._IsVolumeCenter)
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) || BASE._open_Cen_ID == 4216)
                    {
                        ViewBag.IsunmatchTransfer = true;
                    }
                }      
                ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
                ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
                ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
                ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
                ViewData["IntrnlTranfrReg_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove
            }
        }
        [HttpGet]
        public ActionResult Posted_Grid_Data_dx()
        {      
            return Content(JsonConvert.SerializeObject(BASE._Internal_Tf_Voucher_DBOps.GetList()), "application/json");
        }
        [HttpGet]
        public ActionResult Pending_Grid_Data_dx()
        {
            DataSet dSet = BASE._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, null);
            List<Return_InternalTransferRegister_Pending> Final_Data = ConvertToList(dSet.Tables[0]);         
            return Content(JsonConvert.SerializeObject(Final_Data), "application/json");
        }
        public ActionResult Itr_Posted_DetailGrid_Data(bool VouchingMode = false, string RecID = "", string MID = null)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Accounts_CashBook, !VouchingMode)), "application/json");
        }
        public ActionResult Itr_Posted_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "", string MID = null)
        {      
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx(string SelectedTab)
        {
            string GridName = SelectedTab == "Posted" ? "ITR_PostedGrid" : "ITR_PendingGrid";
            ViewBag.GridName = GridName;
            ViewBag.Filename = GridName + "_" + BASE._open_UID_No + "_" + BASE._open_Year_ID;
            return View("Common_Export");           
        }
        #endregion
    }
}
