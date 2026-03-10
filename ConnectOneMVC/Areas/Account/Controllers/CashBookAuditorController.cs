using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.ClientUserInfo;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class CashBookAuditorController : BaseController
    {
        #region "Variables"

        public List<Audit.Return_GetCashBookVouching> CB_GridData
        {
            get
            {
                return (List<Audit.Return_GetCashBookVouching>)GetBaseSession("MainGrid_Data_CBAudit");
            }
            set
            {
                SetBaseSession("MainGrid_Data_CBAudit", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> CashBookNestedGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("CashBookNestedGrid_CBAudit");
            }
            set
            {
                SetBaseSession("CashBookNestedGrid_CBAudit", value);
            }
        }
        public DataTable AllowedVouchingCategories
        {
            get {return (DataTable)GetBaseSession("AllowedVouchingCategories_CBAuditVouchingCategory"); }
            set { SetBaseSession("AllowedVouchingCategories_CBAuditVouchingCategory", value); }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> AdditionalInfoData
        {
            get { return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("AdditionalInfoData_CBAudit"); }
            set { SetBaseSession("AdditionalInfoData_CBAudit", value); }
        }
        public bool iTR_REC_JOURNAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_REC_JOURNAL_Remove_CBAudit");
            }
            set
            {
                SetBaseSession("iTR_REC_JOURNAL_Remove_CBAudit", value);
            }
        }
        public bool iTR_REC_TOTAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_REC_TOTAL_Remove_CBAudit");
            }
            set
            {
                SetBaseSession("iTR_REC_TOTAL_Remove_CBAudit", value);
            }
        }
        public bool iTR_PAY_JOURNAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_PAY_JOURNAL_Remove_CBAudit");
            }
            set
            {
                SetBaseSession("iTR_PAY_JOURNAL_Remove_CBAudit", value);
            }
        }
        public bool iTR_PAY_TOTAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_PAY_TOTAL_Remove_CBAudit");
            }
            set
            {
                SetBaseSession("iTR_PAY_TOTAL_Remove_CBAudit", value);
            }
        }
        public bool iRef_no_Visible
        {
            get
            {
                return (bool)GetBaseSession("iRef_no_Visible_CBAudit");
            }
            set
            {
                SetBaseSession("iRef_no_Visible_CBAudit", value);
            }
        }
        public string Negative_MsgStr
        {
            get
            {
                return (string)GetBaseSession("Negative_MsgStr_CBAudit");
            }
            set
            {
                SetBaseSession("Negative_MsgStr_CBAudit", value);
            }
        }

        public string CB_VouchingStatus
        {
            get
            {
                return (string)GetBaseSession("CB_VouchingStatus_CBAudit");
            }
            set { SetBaseSession("CB_VouchingStatus_CBAudit", value); }
        }
        public string CB_VouchedBy
        {
            get
            {
                return (string)GetBaseSession("CB_VouchedBy_CBAudit");
            }
            set { SetBaseSession("CB_VouchedBy_CBAudit", value); }
        }
        public string CB_ReviewedBy
        {
            get
            {
                return (string)GetBaseSession("CB_ReviewedBy_CBAudit");
            }
            set { SetBaseSession("CB_ReviewedBy_CBAudit", value); }
        }
        public string[] CB_Zone
        {
            get
            {
                return (string[])GetBaseSession("CB_Zone_CBAudit");
            }
            set { SetBaseSession("CB_Zone_CBAudit", value); }
        }
        public string[] CB_SubZone
        {
            get
            {
                return (string[])GetBaseSession("CB_SubZone_CBAudit");
            }
            set { SetBaseSession("CB_SubZone_CBAudit", value); }
        }
        public string[] CB_State
        {
            get
            {
                return (string[])GetBaseSession("CB_State_CBAudit");
            }
            set { SetBaseSession("CB_State_CBAudit", value); }
        }
        public string[] CB_UID
        {
            get
            {
                return (string[])GetBaseSession("CB_UID_CBAudit");
            }
            set { SetBaseSession("CB_UID_CBAudit", value); }
        }
        public string[] CB_EntryScreen
        {
            get { return (string[])GetBaseSession("CB_EntryScreen_CBAudit"); }
            set { SetBaseSession("CB_EntryScreen_CBAudit", value); }
        }
        public string[] CB_Head
        {
            get
            {
                return (string[])GetBaseSession("CB_Head_CBAudit");
            }
            set { SetBaseSession("CB_Head_CBAudit", value); }
        }
        public string[] CB_Item
        {
            get
            {
                return (string[])GetBaseSession("CB_Item_CBAudit");
            }
            set { SetBaseSession("CB_Item_CBAudit", value); }
        }
        public int? CB_Amount1
        {
            get
            {
                return (int?)GetBaseSession("CB_Amount1_CBAudit");
            }
            set { SetBaseSession("CB_Amount1_CBAudit", value); }
        }
        public int? CB_Amount2
        {
            get
            {
                return (int?)GetBaseSession("CB_Amount2_CBAudit");
            }
            set { SetBaseSession("CB_Amount2_CBAudit", value); }
        }
        public string CB_Mode
        {
            get
            {
                return (string)GetBaseSession("CB_Mode_CBAudit");
            }
            set { SetBaseSession("CB_Mode_CBAudit", value); }
        }
        public string CB_Type
        {
            get
            {
                return (string)GetBaseSession("CB_Type_CBAudit");
            }
            set { SetBaseSession("CB_Type_CBAudit", value); }
        }
        public string[] CB_Purpose
        {
            get
            {
                return (string[])GetBaseSession("CB_Purpose_CBAudit");
            }
            set { SetBaseSession("CB_Purpose_CBAudit", value); }
        }
        public string CB_Narration
        {
            get
            {
                return (string)GetBaseSession("CB_Narration_CBAudit");
            }
            set { SetBaseSession("CB_Narration_CBAudit", value); }
        }
        public string CB_RejectReason
        {
            get
            {
                return (string)GetBaseSession("CB_RejectReason_CBAudit");
            }
            set { SetBaseSession("CB_RejectReason_CBAudit", value); }
        }
        public int? CB_ReviewdCount1
        {
            get
            {
                return (int?)GetBaseSession("CB_ReviewdCount1_CBAudit");
            }
            set { SetBaseSession("CB_ReviewdCount1_CBAudit", value); }
        }
        public int? CB_ReviewdCount2
        {
            get
            {
                return (int?)GetBaseSession("CB_ReviewdCount2_CBAudit");
            }
            set { SetBaseSession("CB_ReviewdCount2_CBAudit", value); }
        }
        public string CB_Document
        {
            get
            {
                return (string)GetBaseSession("CB_Document_CBAudit");
            }
            set { SetBaseSession("CB_Document_CBAudit", value); }
        }
        public string CB_DocumentCategory
        {
            get
            {
                return (string)GetBaseSession("CB_DocumentCategory_CBAudit");
            }
            set { SetBaseSession("CB_DocumentCategory_CBAudit", value); }
        }
        public DateTime? CB_DocumentFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("CB_DocumentFromDate_CBAudit");
            }
            set { SetBaseSession("CB_DocumentFromDate_CBAudit", value); }
        }
        public DateTime? CB_DocumentToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("CB_DocumentToDate_CBAudit");
            }
            set { SetBaseSession("CB_DocumentToDate_CBAudit", value); }
        }
        public string CB_DocumentDescription
        {
            get
            {
                return (string)GetBaseSession("CB_DocumentDescription_CBAudit");
            }
            set { SetBaseSession("CB_DocumentDescription_CBAudit", value); }
        }
        public string CB_VouchingCategory
        {
            get
            {
                return (string)GetBaseSession("CB_VouchingCategory_CBAudit");
            }
            set { SetBaseSession("CB_VouchingCategory_CBAudit", value); }
        }
        public string CB_DataScope
        {
            get
            {
                return (string)GetBaseSession("CB_DataScope_CBAudit");
            }
            set { SetBaseSession("CB_DataScope_CBAudit", value); }
        }
        public string CB_Poolsize
        {
            get
            {
                return (string)GetBaseSession("CB_Poolsize_CBAudit");
            }
            set { SetBaseSession("CB_Poolsize_CBAudit", value); }
        }
        public Boolean? CB_Include_Audited_Period
        {
            get
            {
                return (Boolean?)GetBaseSession("CB_Include_Audited_Period_CBAudit");
            }
            set { SetBaseSession("CB_Include_Audited_Period_CBAudit", value); }
        }
        public List<Center.Return_Get_Zone> ZoneList
        {
            get { return (List<Center.Return_Get_Zone>)GetBaseSession("ZoneList_CBAudit"); }
            set { SetBaseSession("ZoneList_CBAudit", value); }
        }
        public List<Center.Return_GetSubZoneList> SubzoneList
        {
            get { return (List<Center.Return_GetSubZoneList>)GetBaseSession("SubzoneList_CBAudit"); }
            set { SetBaseSession("SubzoneList_CBAudit", value); }
        }
        public List<Return_StateList> StateList
        {
            get { return (List<Return_StateList>)GetBaseSession("StateList_CBAudit"); }
            set { SetBaseSession("StateList_CBAudit", value); }
        }
        public List<Center.Return_GetInstUIDList> UIDList
        {
            get { return (List<Center.Return_GetInstUIDList>)GetBaseSession("UIDList_CBAudit"); }
            set { SetBaseSession("UIDList_CBAudit", value); }
        }
        public List<Return_LedgerList> HeadList
        {
            get { return (List<Return_LedgerList>)GetBaseSession("HeadList_CBAudit"); }
            set { SetBaseSession("HeadList_CBAudit", value); }
        }
        public List<Audit.Return_GetItemList> ItemList
        {
            get { return (List<Audit.Return_GetItemList>)GetBaseSession("ItemList_CBAudit"); }
            set { SetBaseSession("ItemList_CBAudit", value); }
        }
        public List<Audit.Return_GetPurpose> PurposeList
        {
            get { return (List<Audit.Return_GetPurpose>)GetBaseSession("PurposeList_CBAudit"); }
            set { SetBaseSession("PurposeList_CBAudit", value); }
        }
        public List<Attachments.Return_GetDocument_Names> DocumentList
        {
            get { return (List<Attachments.Return_GetDocument_Names>)GetBaseSession("DocumentList_CBAudit"); }
            set { SetBaseSession("DocumentList_CBAudit", value); }
        }
        public List<Attachments.Return_GetDocument_Categories> DocumentCategoryList
        {
            get { return (List<Attachments.Return_GetDocument_Categories>)GetBaseSession("DocumentCategoryList_CBAudit"); }
            set { SetBaseSession("DocumentCategoryList_CBAudit", value); }
        }
        public List<Return_VouchingCategoryList> VouchingCategoryList
        {
            get { return (List<Return_VouchingCategoryList>)GetBaseSession("VouchingCategoryList_CBAudit"); }
            set { SetBaseSession("VouchingCategoryList_CBAudit", value); }
        }
        public List<ClientUserInfo.Return_GetAuditors_Superusers> UserList
        {
            get { return (List<ClientUserInfo.Return_GetAuditors_Superusers>)GetBaseSession("UserList_CBAudit"); }
            set { SetBaseSession("UserList_CBAudit", value); }
        }
        public List<ScreenList> EntryScreenList
        {
            get { return (List<ScreenList>)GetBaseSession("EntryScreenList_CBAudit"); }
            set { SetBaseSession("EntryScreenList_CBAudit", value); }
        }

        public int GridTotalRowCount
        {
            get { return (int)GetBaseSession("GridTotalRowCount_CBAudit"); }
            set { SetBaseSession("GridTotalRowCount_CBAudit", value); }
        }
        public int GridTotalEntryCount
        {
            get { return (int)GetBaseSession("GridTotalEntryCount_CBAudit"); }
            set { SetBaseSession("GridTotalEntryCount_CBAudit", value); }
        }
        public bool NonVouchingCenter
        {
            get { return (bool)GetBaseSession("NonVouchingCenter_CBAudit"); }
            set { SetBaseSession("NonVouchingCenter_CBAudit", value); }
        }
        #endregion
        public ActionResult Frm_Voucher_Info_CB_Auditor(string VouchingCategory = null)
        {
            Clearfilters();
            Negative_MsgStr = "";
            NonVouchingCenter = true;
            DataTable Cen_Task = BASE._ClientUserDBOps.GetCenterTasks();
            if (Cen_Task != null)
            {
                DataView DV1 = new DataView(Cen_Task);
                DV1.Sort = "TASK_NAME";
                if (DV1.Count > 0)
                {
                    int index = DV1.Find("VOUCHING");
                    if (index >= 0)
                    {
                        NonVouchingCenter = false;
                    }
                }                   
            }
            if (NonVouchingCenter == true)
            {
                CB_UID =new string[] { BASE._open_Cen_ID.ToString() };
            }
            CB_VouchingCategory = VouchingCategory;
            return View();
        }
        public PartialViewResult Frm_Voucher_Info_CB_Grid_Auditor(string command = "", bool GetLatestData = false, int ShowHorizontalBar = 0, bool AllowCellMerge = true, string ViewMode = "Compact", string Layout = null, bool FreshData = false)
        {
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.CB_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ShowNarration = AllowCellMerge;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            if (GetLatestData == true || CB_GridData == null || command == "REFRESH")
            {
                var data = Grid_Display(FreshData);
            }
            CreateViewData();
            return PartialView(CB_GridData);
        }
        public PartialViewResult Frm_Voucher_Info_CB_Grid_Nested_Auditor(string ID, string MID, Int32 CENID, string command, int ShowHorizontalBar = 0)
        {
            ViewBag.CB_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.CashBookID = ID;
            ViewBag.CashBookMID = MID;
            var Data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(ID, MID, CENID, ClientScreen.Account_CashbookAuditor, false);               
            CashBookNestedGrid = Data.DocumentMapping;              
            AdditionalInfoData = Data.AdditionalInfo;           
            return PartialView(CashBookNestedGrid);
        }
        public ActionResult Frm_Voucher_Info_CB_Grid_Auditor_AdditionalInfo(string ID, string MID, Int32 CENID, string command)
        {
            ViewBag.CashBookID = ID;
            ViewBag.CashBookMID = MID;
            ViewBag.CENID = CENID;
            if (command == "REFRESH")
            {
                AdditionalInfoData = BASE._Audit_DBOps.GetAdditionalInfo(ID, MID, CENID, ClientScreen.Account_CashbookAuditor);
            }
            return PartialView(AdditionalInfoData);
        }
        public ActionResult CB_GetGridData(int? key)
        {
            string itstr = "";
            if (key != null)
            {
                string NextRowTempID = "";
                if (key != null)
                {
                    var FinalData = CB_GridData as List<Audit.Return_GetCashBookVouching>;
                    var actionItems = (Audit.Return_GetCashBookVouching)FinalData.Where(f => f.Sr == key).FirstOrDefault();
                    if (FinalData.Count == key)
                    {
                        NextRowTempID = "Last_Row";
                    }
                    else
                    {
                        NextRowTempID = FinalData[(int)key].iTR_TEMP_ID;
                    }
                    if (actionItems != null)
                    {
                        itstr = actionItems.iTR_TEMP_ID + "![" + actionItems.iREC_ID + "![" + actionItems.iREC_EDIT_ON + "![" + actionItems.iTR_ITEM_ID + "![" + actionItems.iTR_AB_ID_1 + "![" +
                                    actionItems.iTR_M_ID + "![" + actionItems.iACTION_STATUS + "![" + actionItems.iTR_CODE + "![" + actionItems.iREC_EDIT_BY + "![" + actionItems.iTR_SR_NO + "!["
                                    + actionItems.iREC_ADD_ON + "![" + actionItems.iTR_DATE + "![" + actionItems.iTR_ITEM + "![" + actionItems.iREC_EDIT_ON + "![" + actionItems.iREC_ADD_BY + "!["
                                    + actionItems.iREC_STATUS_BY + "![" + actionItems.iREC_STATUS_ON + "![" + actionItems.iRef_no + "![" + actionItems.iTR_REF_NO + "![" + actionItems.iREQ_ATTACH_COUNT
                                     + "![" + actionItems.iRESPONDED_COUNT + "![" + actionItems.iCOMPLETE_ATTACH_COUNT + "![" + NextRowTempID + "![" + actionItems.CEN_ID + "![" + actionItems.VOUCHING_CATEGORY;
                    }
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult CB_GetNestedGridData(string ID)
        {
            string itstr = "";
            if (ID != null)
            {
                var FinalData = CashBookNestedGrid as List<DbOperations.Audit.Return_GetDocumentMapping>;

                DbOperations.Audit.Return_GetDocumentMapping actionItems = (DbOperations.Audit.Return_GetDocumentMapping)FinalData.Where(f => f.UniqueID == ID).FirstOrDefault();

                if (actionItems != null)
                {
                    itstr = actionItems.Doc_Status + "![" + actionItems.Params_Mandatory + "![" + actionItems.LABEL_FROM_DATE + "![" + actionItems.LABEL_TO_DATE + "![" + actionItems.LABEL_DESCRIPTION + "![" + actionItems.Document_Category + "![" + actionItems.Document_ID + "![" + actionItems.ATTACH_ID + "![" + actionItems.TxnID + "![" + actionItems.TxnMID + "![" + actionItems.Tr_Code + "![" + actionItems.MAP_ID + "![" + actionItems.Reason + "![" + actionItems.ATTACH_FILE_NAME + "![" + actionItems.Attachment_Action_Status + "![" + actionItems.ReasonID.ToString();
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Grid_Display(bool FreshData)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            iTR_REC_JOURNAL_Remove = true;
            iTR_REC_TOTAL_Remove = true;
            iTR_PAY_JOURNAL_Remove = true;
            iTR_PAY_TOTAL_Remove = true;
            iTR_PAY_JOURNAL_Remove = false;
            iTR_PAY_TOTAL_Remove = false;
            iTR_REC_JOURNAL_Remove = false;
            iTR_REC_TOTAL_Remove = false;
            var GridData = new List<Audit.Return_GetCashBookVouching>();
            Audit.Param_GetCashBookVouching Inparam = new Audit.Param_GetCashBookVouching();

            Inparam.Vouching_Status = CB_VouchingStatus ?? "";
            Inparam.Vouched_by = CB_VouchedBy ?? "";
            Inparam.ZoneIDs = CB_Zone == null ? "" : CB_Zone[0];
            Inparam.SubZoneIDs = CB_SubZone == null ? "" : CB_SubZone[0];
            Inparam.StateIDs = CB_State == null ? "" : CB_State[0];
            Inparam.CenIDs = CB_UID == null ? "" : CB_UID[0];
            Inparam.Tr_Codes = CB_EntryScreen == null ? "" : CB_EntryScreen[0];
            Inparam.LedgerIDs = CB_Head == null ? "" : CB_Head[0];
            Inparam.ItemRecIDs = CB_Item == null ? "" : CB_Item[0];
            Inparam.AmountFrom = CB_Amount1 == null? -1 : (int)CB_Amount1;
            Inparam.AmountTo = CB_Amount2 == null? -1 : (int)CB_Amount2;
            Inparam.Mode = CB_Mode ?? "";
            Inparam.Type = CB_Type ?? "";
            Inparam.PurposeIDs = CB_Purpose == null ? "" : CB_Purpose[0];
            Inparam.Narration = CB_Narration ?? "";
            Inparam.Rejection_Reason = CB_RejectReason ?? "";
            Inparam.Reviewed_By = CB_ReviewedBy ?? "";
            Inparam.Review_Count_From = CB_ReviewdCount1 == null? -1 : (int)CB_ReviewdCount1;
            Inparam.Review_Count_To = CB_ReviewdCount2 == null? -1 : (int)CB_ReviewdCount2;
            Inparam.Document_IDs = CB_Document == null ? "" : CB_Document;
            Inparam.DocumentCategory = CB_DocumentCategory ?? "";
            Inparam.DocumentDescription = CB_DocumentDescription ?? "";
            Inparam.DocumentFromDate = CB_DocumentFromDate;
            Inparam.DocumentToDate = CB_DocumentToDate;
            Inparam.VouchingCategory = CB_VouchingCategory ?? "";
            Inparam.Selection_Pool = CB_DataScope ?? "Exclusive";
            Inparam.Record_Pool_Size = CB_Poolsize ?? "All";
            Inparam.AuditorID = BASE._open_User_ID;
            Inparam.InsttID = BASE._open_Ins_ID;
            Inparam.YearID = BASE._open_Year_ID;
            Inparam.FreshData = FreshData;
            Inparam.Skip_Audited_Period = CB_Include_Audited_Period == true ? false : true;
            GridData = BASE._Audit_DBOps.GetCashBookVouching(Inparam);

            //GridData = GridData.OrderBy(x => x.iTR_DATE).ToList();
            //GridData = GridData.OrderBy(x => x.iTR_ROW_POS).ToList();
            //GridData = GridData.OrderBy(x => x.iTR_ENTRY).ToList();
            //GridData = GridData.OrderBy(x => x.iREC_ADD_ON).ToList();
            //GridData = GridData.OrderBy(x => x.iTR_M_ID).ToList();
            //GridData = GridData.OrderBy(x => x.iTR_SORT).ToList();
            //GridData = GridData.OrderBy(x => x.iTR_SR_NO).ToList();
            string _TEMP = "";
            if (GridData.Count > 0)
            {
                _TEMP = GridData[0].iTR_TEMP_ID;
            }
            int _SR = 1;         
            for (int i = 0; i < GridData.Count; i++)
            {
                if (GridData[i].iTR_TEMP_ID == _TEMP)
                {
                    GridData[i].iTR_REF_NO = _SR;
                }
                else
                {
                    _TEMP = GridData[i].iTR_TEMP_ID.ToString();
                    _SR = _SR + 1;
                    GridData[i].iTR_REF_NO = _SR;
                }
            }
            CB_GridData = GridData;
            GridTotalRowCount = CB_GridData.Count;
            if (GridTotalRowCount > 0)
            {
                GridTotalEntryCount = CB_GridData.Select(x => x.iTR_REF_NO).AsParallel().Distinct().Count();
            }
            else { GridTotalEntryCount = 0; }
            if (BASE._IsVolumeCenter)
            {
                iRef_no_Visible = true;
            }
            else
            {
                iRef_no_Visible = false;
            }
            if (Negative_MsgStr.Trim().Length > 0)
            {
                jsonParam.message = Negative_MsgStr;
                jsonParam.title = "Alert..";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            return Json(new
            {
                jsonParam           
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeftPaneContent(string ID, string MID, Int32 CENID)
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.CENID = CENID;          
            return View();
        }
        public ActionResult Frm_CB_Remarks(string RefRecId = "", string ActionMethod = "", string MainGridName = "",string TempID="",string CallingScreen="",String EntryCenID="", string VouchingCategory = "CASHBOOK", string AttachmentID = null, Int32? TxnDocRespID = null,string NestedRowKeyvalue="")
        {
            VouchingCategory = GetVouchingCategoryForCallingScreen(CallingScreen);

            ViewBag.RefRecId = RefRecId;
            ViewBag.ActionMethod = ActionMethod;
            ViewBag.MainGridName = MainGridName;
            ViewBag.TempID = TempID;
            ViewBag.CallingScreen = CallingScreen.Length == 0 ? "VouchingAudit" : CallingScreen;
            ViewBag.EntryCenID = EntryCenID;
            ViewBag.VouchingCategory = VouchingCategory;
            ViewBag.AttachmentID = AttachmentID;
            ViewBag.TxnDocRespID = TxnDocRespID;
            ViewBag.NestedRowKeyvalue = NestedRowKeyvalue;
            return View();
        }
        public bool CheckDocumentStatus()
        {
            bool result = true;
            for (int i = 0; i < CashBookNestedGrid.Count; i++)
            {
                if (CashBookNestedGrid[i].Doc_Checking_Status.ToUpper() == "PENDING")
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public String GetVouchingCategoryForCallingScreen(string CallingScreen)
        {
            string VouchingCategory = "CASHBOOK";
            switch (CallingScreen.ToUpper())
            {
                case "PROFILE_BANKACCOUNTS":
                    VouchingCategory = "BANK ACCOUNT";
                    break;
                case "PROFILE_CASH":
                    VouchingCategory = "CASH";
                    break;
                case "PROFILE_FD":
                    VouchingCategory = "FD";
                    break;
                case "PROFILE_OPENINGBALANCES":
                    VouchingCategory = "OPENING";
                    break;
                case "PROFILE_LANDANDBUILDING":
                    VouchingCategory = "LAND & BUILDING";
                    break;
                case "PROFILE_GOLDSILVER":
                    VouchingCategory = "GOLD SILVER";
                    break;
                case "PROFILE_VEHICLES":
                    VouchingCategory = "VEHICLES";
                    break;
                case "PROFILE_ASSETS":
                    VouchingCategory = "OTHER ASSETS";
                    break;
                case "PROFILE_LIVESTOCK":
                    VouchingCategory = "LIVESTOCK";
                    break;
                case "PROFILE_WIP":
                    VouchingCategory = "WIP";
                    break;
                case "PROFILE_DEPOSIT":
                    VouchingCategory = "OTHER DEPOSITS";
                    break;
                case "PROFILE_ADVANCES":
                    VouchingCategory = "ADVANCES";
                    break;
                case "PROFILE_LIABILITIES":
                    VouchingCategory = "OTHER LIABILITIES";
                    break;
                case "PROFILE_TELEPHONE":
                    VouchingCategory = "TELEPHONE";
                    break;
                case "PROFILE_MEMBERSHIP":
                    VouchingCategory = "WING MEMBER";
                    break;
                case "MAGAZINE_MEMBERSHIP":
                    VouchingCategory = "MAGAZINE MEMBER";
                    break;
                case "FACILITY_ADDRESSBOOK":
                    VouchingCategory = "ADDRESS BOOK";
                    break;
            }
            return VouchingCategory;
        }
        public ActionResult AcceptRejectVoucher(string Remarks, string ID, string ActionMethod,string TempID, string EntryCenID="", string CallingScreen="", string VouchingCategory = "CASHBOOK", string AttachmentID = null, Int32? TxnDocRespID = null)
        {
            CallingScreen = CallingScreen.Length == 0 ? "VouchingAudit" : CallingScreen;
            VouchingCategory = GetVouchingCategoryForCallingScreen(CallingScreen);
            EntryCenID = EntryCenID.Length == 0 ? BASE._open_Cen_ID.ToString() : EntryCenID.ToString();
            try
            {
                if (ActionMethod == "Reject")
                {
                    if (string.IsNullOrWhiteSpace(Remarks))
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Remarks Not Specified.."
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (!string.IsNullOrWhiteSpace(Remarks))
                {
                    Remarks = Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|').Replace("&", " And ");
                }

                if (ActionMethod == "Reject")
                {
                    if (BASE._Audit_DBOps.EntryVouchingRejected(BASE._open_User_ID, TempID, Remarks,Convert.ToInt32(EntryCenID), ClientScreen.Accounts_CashBook,VouchingCategory,AttachmentID,TxnDocRespID))
                    {
                        return Json(new
                        {
                            result = true,
                            message = "Entry Rejected"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //if (CallingScreen == "VouchingAudit")
                    //{
                    //    if (CheckDocumentStatus() == false && !TempID.ToLower().Contains("note-book"))
                    //    {
                    //        return Json(new
                    //        {
                    //            result = false,
                    //            message = "Entry Cannot Be Accepted<br>There Are Documents Which Have Not Been Checked<br>Documents Must Have Accepted Or Rejected Status"
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}           
                    if (BASE._Audit_DBOps.EntryVouchingAccepted(TempID, Remarks, Convert.ToInt32(EntryCenID), ClientScreen.Accounts_CashBook, VouchingCategory, AttachmentID, TxnDocRespID))
                    {
                        return Json(new
                        {
                            result = true,
                            message = "Entry Accepted"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UndecidedVoucher(string TempID)
        {
            if (BASE._Audit_DBOps.EntryVouchingSkip(BASE._open_User_ID, TempID,ClientScreen.Accounts_CashBook))
            {
                return Json(new
                {
                    result = true,
                    message = "Entry Skipped"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Export_Options() { return View(); }
        public ActionResult CashBookFilters()
        {
            ViewBag.LoginUserID = BASE._open_User_ID;
            ViewBag.NonVouchingCenter = NonVouchingCenter;
            CashbookFilters model = new CashbookFilters();
            model.CB_VouchingStatus = CB_VouchingStatus==null? "Pending": CB_VouchingStatus;
            model.CB_VouchedBy = CB_VouchedBy;
            model.CB_ReviewedBy =CB_ReviewedBy;
            model.CB_Zone =CB_Zone;
            model.CB_SubZone = CB_SubZone;
            model.CB_State =CB_State;
            model.CB_UID = CB_UID;
            model.CB_EntryScreen = CB_EntryScreen;
            model.CB_Head = CB_Head;
            model.CB_Item = CB_Item;
            model.CB_Amount1 = CB_Amount1;
            model.CB_Amount2 =CB_Amount2;
            model.CB_Mode =CB_Mode;
            model.CB_Type = CB_Type;
            model.CB_Purpose =CB_Purpose;
            model.CB_Narration =CB_Narration;
            model.CB_RejectReason = CB_RejectReason;
            model.CB_ReviewdCount1 = CB_ReviewdCount1;
            model.CB_ReviewdCount2 = CB_ReviewdCount2;
            model.CB_Document = CB_Document;
            model.CB_DocumentCategory = CB_DocumentCategory;
            model.CB_DocumentFromDate = CB_DocumentFromDate;
            model.CB_DocumentToDate = CB_DocumentToDate;
            model.CB_DocumentDescription = CB_DocumentDescription;
            model.CB_VouchingCategory = CB_VouchingCategory;
            model.CB_DataScope = CB_DataScope;
            model.CB_Poolsize = CB_Poolsize;
            model.CB_Include_Audited_Period = CB_Include_Audited_Period;
            return View(model);

        }
        [HttpPost]
        public ActionResult CashBookFilters(CashbookFilters model)
        {
            CB_VouchingStatus = model.CB_VouchingStatus;
            CB_VouchedBy = model.CB_VouchedBy;
            CB_ReviewedBy = model.CB_ReviewedBy;
            CB_Zone = model.CB_Zone;
            CB_SubZone = model.CB_SubZone;
            CB_State = model.CB_State;
            CB_UID = model.CB_UID;
            CB_EntryScreen = model.CB_EntryScreen;
            CB_Head = model.CB_Head;
            CB_Item = model.CB_Item;
            CB_Amount1 = model.CB_Amount1;
            CB_Amount2 = model.CB_Amount2;
            CB_Mode = model.CB_Mode;
            CB_Type = model.CB_Type;
            CB_Purpose = model.CB_Purpose;
            CB_Narration = model.CB_Narration;
            CB_RejectReason = model.CB_RejectReason;
            CB_ReviewdCount1 = model.CB_ReviewdCount1;
            CB_ReviewdCount2 = model.CB_ReviewdCount2;
            CB_Document = model.CB_Document;
            CB_DocumentCategory = model.CB_DocumentCategory;
            CB_DocumentFromDate = model.CB_DocumentFromDate;
            CB_DocumentToDate = model.CB_DocumentToDate;
            CB_DocumentDescription = model.CB_DocumentDescription;
            CB_VouchingCategory = model.CB_VouchingCategory;
            CB_DataScope = model.CB_DataScope;
            CB_Poolsize = model.CB_Poolsize;
            CB_Include_Audited_Period = model.CB_Include_Audited_Period;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Get_VouchingPreference()
        {
            AllowedVouchingCategories=BASE._ClientUserDBOps.GetAllowedVouchingCategories();
            if (AllowedVouchingCategories != null && AllowedVouchingCategories.Rows.Count < 3)
            {
                return Json(new
                {
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return View(AllowedVouchingCategories);
        }
        public ActionResult Frm_Get_VouchingPreference_Grid(string command)
        {
            if (command == "REFRESH"|| AllowedVouchingCategories==null)
            {
                AllowedVouchingCategories = BASE._ClientUserDBOps.GetAllowedVouchingCategories();
            }
            return View(AllowedVouchingCategories);
        }
        public void CreateViewData()
        {
            ViewData["iTR_REC_JOURNAL_Remove"] = iTR_REC_JOURNAL_Remove;
            ViewData["iTR_REC_TOTAL_Remove"] = iTR_REC_TOTAL_Remove;
            ViewData["iTR_PAY_JOURNAL_Remove"] = iTR_PAY_JOURNAL_Remove;
            ViewData["iTR_PAY_TOTAL_Remove"] = iTR_PAY_TOTAL_Remove;

            ViewData["iTR_REC_BANK_Visible"] = true;
            ViewData["iTR_PAY_BANK_Visible"] = true;

            ViewData["iRef_no_Visible"] = iRef_no_Visible;

            ViewData["CashBook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "Export");
            ViewData["GridTotalRowCount"] = GridTotalRowCount;
            ViewData["GridTotalEntryCount"] = GridTotalEntryCount;
        }

        #region Filter DD
        public ActionResult RefreshZoneList()
        {
            ZoneList = BASE._CenterDBOps.GetZoneList(ClientScreen.Accounts_CashBook);
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetZone(DataSourceLoadOptions loadOptions)
        {
            if (ZoneList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Center.Return_Get_Zone>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ZoneList, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult RefreshSubzoneList(string[] Zone)
        {          
            SubzoneList = BASE._CenterDBOps.GetSubZoneList(ClientScreen.Accounts_CashBook, Zone);
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetSubZone(DataSourceLoadOptions loadOptions)
        {
            if (SubzoneList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Center.Return_GetSubZoneList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SubzoneList, loadOptions)), "application/json");
        }
        public ActionResult RefreshStateList()
        {
            StateList = BASE._Address_DBOps.GetStates("IN", "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetState(DataSourceLoadOptions loadOptions)
        {
            if (StateList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_StateList>(), loadOptions)), "application/json");
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StateList, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult RefreshUIDList(string[] Zone, string[] Subzone, string[] Stateid)
        {
            
            //if ((Zone == null||Zone[0]==""||Zone[0]==null) && (Subzone == null || Subzone[0] == "" || Subzone[0] == null) && (Stateid == null || Stateid[0] == "" || Stateid[0] == null))
            //{
            //    UIDList = BASE._CenterDBOps.GetInstUIDList(ClientScreen.Accounts_CashBook, BASE._open_Ins_ID, Zone, Subzone, Stateid);
            //    //UIDList = new List<Center.Return_GetInstUIDList>();
            //    return Json(new
            //    {
            //        result = true,
            //        message = ""
            //    }, JsonRequestBehavior.AllowGet);
            //}
            UIDList = BASE._CenterDBOps.GetInstUIDList(ClientScreen.Accounts_CashBook, BASE._open_Ins_ID, Zone, Subzone, Stateid);

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetUID(DataSourceLoadOptions loadOptions)
        {
            if (UIDList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Center.Return_GetInstUIDList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UIDList, loadOptions)), "application/json");
        }
        public ActionResult RefreshHeadList()
        {
            HeadList = Tolist(BASE._Voucher_DBOps.GetLedgers());

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetHead(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._Voucher_DBOps.GetLedgers();
            if (HeadList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_LedgerList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(HeadList, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult RefreshItemList(string[] Head)
        {
            if (Head == null || Head[0] == "" || Head[0] == null)
            {
                ItemList = new List<Audit.Return_GetItemList>();
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
            var head_array = Head[0].Split(',');
            ItemList = BASE._Audit_DBOps.GetItemList(Head);
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetItem(DataSourceLoadOptions loadOptions)
        {
            if (ItemList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Audit.Return_GetItemList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ItemList, loadOptions)), "application/json");
        }
        public ActionResult RefreshPurposeList()
        {
            PurposeList = BASE._Audit_DBOps.GetPurpose();
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetPurpose(DataSourceLoadOptions loadOptions)
        {
            if (PurposeList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Audit.Return_GetPurpose>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PurposeList, loadOptions)), "application/json");
        }
        public ActionResult RefreshDocumentList()
        {
            DocumentList = BASE._Attachments_DBOps.GetDocument_Names("");
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LookUp_GetDocument(DataSourceLoadOptions loadOptions)
        {
            if (DocumentList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Attachments.Return_GetDocument_Names>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DocumentList, loadOptions)), "application/json");
        }
        public ActionResult RefreshDocumentCategoryList()
        {
            DocumentCategoryList = BASE._Attachments_DBOps.GetDocument_Categories();
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LookUp_GetDocumentCategory(DataSourceLoadOptions loadOptions)
        {
            if (DocumentCategoryList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Attachments.Return_GetDocument_Categories>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DocumentCategoryList, loadOptions)), "application/json");
        }
        public ActionResult RefreshVouchingCategoryList()
        {
            VouchingCategoryList = BASE._ClientUserDBOps.GetVouchingCategories();
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LookUp_GetVouchingCategory(DataSourceLoadOptions loadOptions)
        {
            if (VouchingCategoryList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_VouchingCategoryList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(VouchingCategoryList, loadOptions)), "application/json");
        }
        public ActionResult RefreshUserList()
        {
            UserList = BASE._ClientUserDBOps.GetAuditors_Superusers(ClientScreen.Accounts_CashBook);
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Lookup_GetAuditorSuperuser(DataSourceLoadOptions loadOptions)
        {
            if (UserList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<ClientUserInfo.Return_GetAuditors_Superusers>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UserList, loadOptions)), "application/json");
        }
        public ActionResult Lookup_GetEntryScreen(DataSourceLoadOptions loadOptions)
        {
            if (EntryScreenList == null)
            {
                List<ScreenList> screens = new List<ScreenList>();
                screens.Add(new ScreenList { Name = "Cash Deposit Withdrawn", ID = 1 });
                screens.Add(new ScreenList { Name = "Bank To Bank Transfer", ID = 2 });
                screens.Add(new ScreenList { Name = "Payment", ID = 3 });
                screens.Add(new ScreenList { Name = "Receipt", ID = 4 });
                screens.Add(new ScreenList { Name = "Donation Regular", ID = 5 });
                screens.Add(new ScreenList { Name = "Donation Foreign", ID = 6 });
                screens.Add(new ScreenList { Name = "Donation Gift", ID = 7 });
                screens.Add(new ScreenList { Name = "Internal Transfer", ID = 8 });
                screens.Add(new ScreenList { Name = "Collection Box", ID = 9 });
                screens.Add(new ScreenList { Name = "Fixed Deposits", ID = 10 });
                screens.Add(new ScreenList { Name = "Sale Asset", ID = 11 });
                screens.Add(new ScreenList { Name = "Membership", ID = 12 });
                screens.Add(new ScreenList { Name = "Membership Renewal", ID = 13 });
                screens.Add(new ScreenList { Name = "Journal", ID = 14 });
                screens.Add(new ScreenList { Name = "Asset Transfer", ID = 15 });
                screens.Add(new ScreenList { Name = "Membership Conversion", ID = 16 });
                screens.Add(new ScreenList { Name = "WIP Finalization", ID = 17 });
                screens.Add(new ScreenList { Name = "Magazine New", ID = 18 });
                screens.Add(new ScreenList { Name = "Magazine Renew", ID = 19 });
                screens.Add(new ScreenList { Name = "Magazine Payment", ID = 20 });
                EntryScreenList = screens;
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(EntryScreenList, loadOptions)), "application/json");
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_CBAudit");
        }
        public void SessionClear_VouchingCategory()
        {
            ClearBaseSession("_CBAuditVouchingCategory");
        }
        public List<Return_LedgerList> Tolist(DataTable Dt)
        {
            var ledger = new List<Return_LedgerList>();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Return_LedgerList row = new Return_LedgerList();
                row.LED_NAME = Dt.Rows[i]["LED_NAME"].ToString();
                row.LED_ID = Dt.Rows[i]["LED_ID"].ToString();
                ledger.Add(row);
            }
            return ledger;
        }
        public void Clearfilters()
        {
            CB_VouchingStatus = null;
            CB_VouchedBy = null;
            CB_ReviewedBy = null;
            CB_Zone = null;
            CB_SubZone = null;
            CB_State = null;
            CB_UID = null;
            CB_EntryScreen = null;
            CB_Head = null;
            CB_Item = null;
            CB_Amount1 = null;
            CB_Amount2 = null;
            CB_Mode = null;
            CB_Type = null;
            CB_Purpose = null;
            CB_Narration = null;
            CB_RejectReason = null;
            CB_ReviewdCount1 = null;
            CB_ReviewdCount2 = null;
            CB_Document = null;
            CB_DocumentCategory = null;
            CB_DocumentFromDate = null;
            CB_DocumentToDate = null;
            CB_DocumentDescription = null;
            CB_VouchingCategory = null;
            CB_DataScope = null;
            CB_Poolsize = null;
            CB_Include_Audited_Period = false;
        }
        public string Vouching_GenerateFilterText()
        {
            string Filtertext = "";
            if (CB_VouchingStatus != null && CB_VouchingStatus.Length > 0)
            {
                Filtertext = "Vouching Status : " + CB_VouchingStatus + " > ";
            }
            else
            {
                Filtertext= "Vouching Status : Pending > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_VouchedBy))
            {
                //string Vouchedby = UserList.Where(x => x.REC_ID == CB_VouchedBy).FirstOrDefault().USER_ID;
                Filtertext += "Vouched By : " + CB_VouchedBy + " > ";
            }         
            if (!string.IsNullOrWhiteSpace(CB_ReviewedBy))
            {
                //string Reviewdby = UserList.Where(x => x.REC_ID == CB_ReviewedBy).FirstOrDefault().USER_ID;
                Filtertext += "Reviewed By : " + CB_ReviewedBy + " > ";
            }
            if (CB_ReviewdCount1 != null)
            {
                Filtertext += "Review Count From : " + CB_ReviewdCount1 + " > ";
            }
            if (CB_ReviewdCount2 != null)
            {
                Filtertext += "Review Count To : " + CB_ReviewdCount2 + " > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_Document))
            {
                string DocumentName = DocumentList.Where(x => x.ID == CB_Document).FirstOrDefault().Name;
                Filtertext += "Document Name : " + DocumentName + " > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_DocumentCategory))
            {
                string DocumentCategory = DocumentCategoryList.Where(x => x.ID == CB_DocumentCategory).FirstOrDefault().Category;
                Filtertext += "Document Category : " + DocumentCategory + " > ";
            }
            if (CB_DocumentFromDate != null)
            {
                Filtertext += "Document From : " + Convert.ToDateTime(CB_DocumentFromDate).ToString("dd/MM/yyyy") + " > ";
            }
            if (CB_DocumentToDate != null)
            {
                Filtertext += "Document To : " + Convert.ToDateTime(CB_DocumentToDate).ToString("dd/MM/yyyy") + " > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_DocumentDescription))
            {               
                Filtertext += "Document Description : " + CB_DocumentDescription + " > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_VouchingCategory))
            {
                if (VouchingCategoryList == null)
                {
                    RefreshVouchingCategoryList();
                }
                string VouchingCategory = VouchingCategoryList.Where(x => x.Code == CB_VouchingCategory).FirstOrDefault().Category;
                Filtertext += "Vouching Category : " + VouchingCategory + " > ";
            }
            if (CB_DataScope != null && CB_DataScope.Length > 0)
            {
                Filtertext += "DataScope : " + CB_DataScope + " > ";
            }
            else
            {
                Filtertext += "DataScope : Exclusive > ";
            }
            if (!string.IsNullOrWhiteSpace(CB_Poolsize))
            {
                Filtertext += "PoolSize : " + CB_Poolsize + " > ";
            }
            if (CB_Zone != null && !string.IsNullOrWhiteSpace(CB_Zone[0]))
            {
                string Zone = "";
                if (CB_Zone[0].Length > 0)
                {
                    string[] split = CB_Zone[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            Zone = Zone + split[i] + ",";
                        }
                        else { Zone = Zone + split[i]; }
                    }
                    Filtertext += "Zone : " + Zone + " > ";
                }
            }
            if (CB_SubZone != null && !string.IsNullOrWhiteSpace(CB_SubZone[0]))
            {
                string SubZone = "";
                if (CB_SubZone[0].Length > 0)
                {
                    string[] split = CB_SubZone[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            SubZone = SubZone + SubzoneList.Where(x=>x.ShortName== split[i]).FirstOrDefault().Name + ",";
                        }
                        else { SubZone = SubZone + SubzoneList.Where(x => x.ShortName == split[i]).FirstOrDefault().Name; }
                    }
                    Filtertext += "SubZone : " + SubZone + " > ";
                }
            }
            if (CB_State != null && !string.IsNullOrWhiteSpace(CB_State[0]))
            {
                string State = "";
                if (CB_State[0].Length > 0)
                {
                    string[] split = CB_State[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            State = State + StateList.Where(x => x.R_ST_REC_ID == split[i]).FirstOrDefault().R_ST_NAME + ",";
                        }
                        else { State = State + StateList.Where(x => x.R_ST_REC_ID == split[i]).FirstOrDefault().R_ST_NAME; }
                    }
                    Filtertext += "State : " + State + " > ";
                }
            }
            if (CB_UID != null && !string.IsNullOrWhiteSpace(CB_UID[0]))
            {
                string UID = "";
                if (CB_UID[0].Length > 0)
                {
                    string[] split = CB_UID[0].Split(',');
                    if (UIDList == null)
                    {
                        RefreshUIDList(CB_Zone,CB_SubZone,CB_State);
                    }
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            UID = UID + UIDList.Where(x => x.CEN_ID == Convert.ToInt32(split[i])).FirstOrDefault().UID + ",";
                        }
                        else { UID = UID + UIDList.Where(x => x.CEN_ID == Convert.ToInt32(split[i])).FirstOrDefault().UID; }
                    }
                    Filtertext += "UID : " + UID + " > ";
                }
            }
            if (CB_EntryScreen != null && !string.IsNullOrWhiteSpace(CB_EntryScreen[0]))
            {
                string EntryScreen = "";
                if (CB_EntryScreen[0].Length > 0)
                {
                    string[] split = CB_EntryScreen[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            EntryScreen = EntryScreen + EntryScreenList.Where(x => x.ID == Convert.ToInt32(split[i])).FirstOrDefault().Name + ",";
                        }
                        else { EntryScreen = EntryScreen + EntryScreenList.Where(x => x.ID == Convert.ToInt32(split[i])).FirstOrDefault().Name; }
                    }
                    Filtertext += "Entry Screen : " + EntryScreen + " > ";
                }
            }
            if (CB_Head != null && !string.IsNullOrWhiteSpace(CB_Head[0]))
            {
                string Head = "";
                if (CB_Head[0].Length > 0)
                {
                    string[] split = CB_Head[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            Head = Head + HeadList.Where(x => x.LED_ID == split[i]).FirstOrDefault().LED_NAME + ",";
                        }
                        else { Head = Head + HeadList.Where(x => x.LED_ID == split[i]).FirstOrDefault().LED_NAME; }
                    }
                    Filtertext += "Head : " + Head + " > ";
                }
            }
            if (CB_Item != null && !string.IsNullOrWhiteSpace(CB_Item[0]))
            {
                string Item = "";
                if (CB_Item[0].Length > 0)
                {
                    string[] split = CB_Item[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            Item = Item + ItemList.Where(x => x.ItemID == split[i]).FirstOrDefault().ItemName + ",";
                        }
                        else { Item = Item + ItemList.Where(x => x.ItemID == split[i]).FirstOrDefault().ItemName; }
                    }
                    Filtertext += "Item : " + Item + " > ";
                }
            }
            if (CB_Amount1 != null)
            {
                Filtertext += "Amount From : " + CB_Amount1 + " > ";
            }
            if (CB_Amount2 != null)
            {
                Filtertext += "Amount To : " + CB_Amount2 + " > ";
            }
            if (CB_Mode != null && CB_Mode.Length > 0)
            {
                Filtertext += "Mode : " + CB_Mode + " > ";
            }
            if (CB_Type != null && CB_Type.Length > 0)
            {
                Filtertext += "Type : " + CB_Type + " > ";
            }
            if (CB_Purpose != null && !string.IsNullOrWhiteSpace(CB_Purpose[0]))
            {
                string Purpose = "";
                if (CB_Purpose[0].Length > 0)
                {
                    string[] split = CB_Purpose[0].Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (i != split.Length - 1)
                        {
                            Purpose = Purpose + PurposeList.Where(x => x.ID == split[i]).FirstOrDefault().Purpose + ",";
                        }
                        else { Purpose = Purpose + PurposeList.Where(x => x.ID == split[i]).FirstOrDefault().Purpose; }
                    }
                    Filtertext += "Purpose : " + Purpose + " > ";
                }
            }
            if (CB_Narration != null && CB_Narration.Length > 0)
            {
                Filtertext += "Narration : " + CB_Narration + " > ";
            }
            if (CB_RejectReason != null && CB_RejectReason.Length > 0)
            {
                Filtertext += "Reject Reason : " + CB_RejectReason;
            }
            if (CB_Include_Audited_Period == true)
            {
                Filtertext += "Audited Period: Included" ;
            }
            else
            {
                Filtertext += "Audited Period: Excluded";
            }
            if (Filtertext.EndsWith(" > "))
            {
                Filtertext = Filtertext.Substring(0, Filtertext.Length - 3);
            }
            return Filtertext;
        }

    }
    [Serializable()]
    public class Return_LedgerList
    {
        public string LED_NAME { get; set; }
        public string LED_ID { get; set; }
    }
    [Serializable()]
    public class ScreenList
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }
}