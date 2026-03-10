using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common_Lib.RealTimeService;


namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class FDVoucherController : BaseController
    {    
        #region Fd Type Grid
        public ActionResult Frm_Voucher_Win_FD_Type()
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            DataTable d1 = (DataTable)BASE._FD_Voucher_DBOps.GetFdItemCount();
            if (d1 == null)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
            }      
            return View(DatatableToModel.DataTabletoFdType(d1));
        }
        #endregion 
        #region Frm_Voucher_Win_FD
        [HttpGet]
        public ActionResult Frm_Voucher_Win_FD(string Tag = "", string xMID = "", string xID = "", string Info_LastEditedOn = null, string iAction = "", string iSpecific_ItemID = "", string TitleX = "New", string CreatedFDID = "",string GridToRefresh = "CashBookListGrid")
        {
            var i = 0;
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            string[] AM = { "_New", "_New_From_Selection", "_Edit", "_View", "_Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, Rights[i]) && Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            ViewData["FDVoucher_ViewRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_FD, "View");
            Model_Frm_Voucher_Win_FD model = new Model_Frm_Voucher_Win_FD();
            model.ActionMethod = Tag;
            model.xID = xID;
            model.xMID = xMID;
            model.FDiAction = iAction;
            model.iSpecific_ItemID = iSpecific_ItemID;
            model.TitleX = TitleX;
            model.CreatedFDID = CreatedFDID;         
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            model.iAction = null;
            if (string.IsNullOrEmpty(model.TitleX) == false)
            {
                if (model.TitleX.StartsWith("New"))
                {
                    model.TitleX = "FD related Income / Expenses";
                }
            }//Redmine Bug #132913 fixed
            if (!string.IsNullOrEmpty(model.FDiAction))
            {
                model.iAction = (Common.FDAction)Enum.Parse(typeof(Common.FDAction), model.FDiAction);
            }           
            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_FD = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_FD = model.SpecialReferenceList_Data_FD.Count();
            model.ItemData = RefreshItemList();
            if (model.iAction == Common.FDAction.New_FD)
            {
                model.FD_GLookUp_ItemList = "f6e4da62-821f-4961-9f93-f5177fca2a77";
            }
            else if (model.iAction == Common.FDAction.Renew_FD)
            {
                model.FD_GLookUp_ItemList = "4eb60d78-ce90-4a9f-891b-7a82d79dc84b";
            }
            else if (model.iAction == Common.FDAction.Close_FD) 
            {
                model.FD_GLookUp_ItemList = "65730a27-e365-4195-853e-2f59225fe8f4";
            }
           else if (model.iAction == null)
            {
                model.FD_ItemID = model.iSpecific_ItemID;
                if (model.FD_ItemID == "c92da5ab-082d-45d9-b6b7-78752625c715" && (model.ActionMethod == "_Edit" || model.ActionMethod == "_Delete"))
                {
                    object value = BASE._FD_Voucher_DBOps.GetItemCountInSameMaster(model.xMID, "d0219173-45ff-4284-ae0e-89ba0e8d76b4");
                    if (value == null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                    }
                    if ((int)value > 0)
                    {
                        model.FD_ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                    }
                }
                model.FD_GLookUp_ItemList = model.iSpecific_ItemID;
            }
            if (model.iAction != Common.FDAction.New_FD) 
            {
                model.FDData=RefreshFDList(model.FD_GLookUp_ItemList,model.Tag.ToString());
            }
            model.BankData=RefreshBankList();
            if (model.ActionMethod == "_New" || model.ActionMethod == "_New_From_Selection")
            {
                model.FD_Txt_V_NO = "";
            }
            else if (model.ActionMethod == "_Edit" || model.ActionMethod == "_View" || model.ActionMethod == "_Delete")
            {
                string message;
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable NewFDTable = null;
                DataTable CloseFDTable = null;
                DataTable Closetable = null;
                DataTable Renewtable = null;
                DataTable MasterTable = null;
                DataTable PaymentMode = null;
                DataTable InterestTable = null;
                string Query = "";
                DateTime? xDate = null;

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_FD = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                switch (model.iAction)
                {
                    case Common.FDAction.New_FD:
                        Closetable = BASE._FD_Voucher_DBOps.GetRecord(model.xMID);
                        if (Closetable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        xDate = Convert.ToDateTime(Closetable.Rows[0]["TR_DATE"]);
                        model.FD_Txt_V_Date = xDate;
                        if (BASE.AllowMultiuser())
                        {
                            string viewstr = "";
                            if (model.Tag == Common.Navigation_Mode._View)
                            {
                                viewstr = "view";
                            }
                            if (model.Info_LastEditedOn != Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]))
                            {
                                message = Messages.RecordChanged("Current FD", viewstr);
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                            }
                        }
                        model.LastEditedOn = Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]);
                        CloseFDTable = BASE._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString());
                        if (CloseFDTable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        break;
                    case Common.FDAction.Renew_FD:
                        Closetable = BASE._FD_Voucher_DBOps.GetRecord(model.xMID, "65730a27-e365-4195-853e-2f59225fe8f4");
                        if (Closetable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        xDate = Convert.ToDateTime(Closetable.Rows[0]["TR_DATE"]);
                        model.FD_Txt_V_Date = xDate;
                        if (BASE.AllowMultiuser())
                        {
                            if (model.Info_LastEditedOn != Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]))
                            {
                                message = Messages.RecordChanged("Current FD");
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                            }
                        }
                        model.LastEditedOn = Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]);
                        Renewtable = BASE._FD_Voucher_DBOps.GetSelectedColumns(model.xMID, "4eb60d78-ce90-4a9f-891b-7a82d79dc84b");
                        if (Renewtable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        CloseFDTable = BASE._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString());
                        NewFDTable = BASE._FD_Voucher_DBOps.GetFDRecordByID(Renewtable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString());
                        MasterTable = BASE._FD_Voucher_DBOps.GetMasterRecord(model.xMID);
                        PaymentMode = BASE._FD_Voucher_DBOps.GetPaymentRecords(model.xMID);
                        InterestTable = BASE._FD_Voucher_DBOps.GetInterestRecords(model.xMID, "c92da5ab-082d-45d9-b6b7-78752625c715");
                        if (CloseFDTable == null || NewFDTable == null || MasterTable == null || PaymentMode == null || InterestTable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        break;
                    case Common.FDAction.Close_FD:
                        Closetable = BASE._FD_Voucher_DBOps.GetRecord(model.xMID, "65730a27-e365-4195-853e-2f59225fe8f4");
                        if (Closetable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        DataView d1 = new DataView(Closetable);
                        d1.Sort = "TR_REF_BANK_ID DESC";                       
                        Closetable = d1.ToTable();
                        xDate = Convert.ToDateTime(Closetable.Rows[0]["TR_DATE"]);
                        model.FD_Txt_V_Date = xDate;
                        if (BASE.AllowMultiuser())
                        {
                            if (model.Info_LastEditedOn != Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]))
                            {
                                message = Messages.RecordChanged("Current FD");
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                            }
                        }
                        model.LastEditedOn = Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]);
                        CloseFDTable = BASE._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString());
                        MasterTable = BASE._FD_Voucher_DBOps.GetMasterRecord(model.xMID);
                        InterestTable = BASE._FD_Voucher_DBOps.GetInterestRecords(model.xMID, "1ed5cbe4-c8aa-4583-af44-eba3db08e117");
                        if (CloseFDTable == null || MasterTable == null || InterestTable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        break;
                    default:
                        Closetable = BASE._FD_Voucher_DBOps.GetRecord(model.xMID);
                        if (Closetable == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                        }
                        xDate = Convert.ToDateTime(Closetable.Rows[0]["TR_DATE"]);
                        model.FD_Txt_V_Date = xDate;
                        if (BASE.AllowMultiuser())
                        {
                            message = Messages.RecordChanged("Current FD");
                            if (model.Info_LastEditedOn != Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]))
                            {
                                message = Messages.RecordChanged("Current FD");
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                            }
                        }
                        model.LastEditedOn = Convert.ToDateTime(Closetable.Rows[0]["REC_EDIT_ON"]);
                        break;
                }
                Data_Binding(ref model, NewFDTable, CloseFDTable, Closetable, Renewtable, MasterTable, PaymentMode, InterestTable);
                model.PurposeID_FD = BASE._FD_Voucher_DBOps.GetPurposeID(xMID);
            }
            if (model.ActionMethod == "_New_From_Selection") 
            {
                model.FD_GLookUp_ItemList = model.iSpecific_ItemID;
            }
            model.FD_Txt_Ref_No = CommonFunctions.EscapeText(model.FD_Txt_Ref_No);          
            return View(model);
        }
        public void Data_Binding(ref Model_Frm_Voucher_Win_FD model, DataTable NewFDTable, DataTable CloseFDTable, DataTable Closetable, DataTable Renewtable, DataTable MasterTable, DataTable PaymentMode, DataTable InterestTable)
        {
            model.FD_Txt_V_NO = Closetable.Rows[0]["TR_VNO"].ToString();
            model.FD_Cmd_Mode = Closetable.Rows[0]["TR_MODE"].ToString();
            model.FD_Txt_Amount = Convert.ToDouble(Closetable.Rows[0]["TR_AMOUNT"]);
            model.FD_Txt_Narration = Closetable.Rows[0]["TR_NARRATION"].ToString();
            model.FD_Txt_Remarks = Closetable.Rows[0]["TR_REMARKS"].ToString();
            model.FD_Txt_Reference = Closetable.Rows[0]["TR_REFERENCE"].ToString();
            model.xID = Closetable.Rows[0]["REC_ID"].ToString();
            if (model.iAction == Common.FDAction.New_FD)
            {
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_SUB_CR_LED_ID"]))
                {
                    if (Closetable.Rows[0]["TR_SUB_CR_LED_ID"].ToString().Length > 0)
                    {
                        model.FD_GLookUp_BankList = Closetable.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                    }
                }
                model.FD_Txt_Ref_No = Closetable.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_DATE"]))
                {
                    model.FD_Txt_Ref_Date = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_DATE"]);
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_CDATE"]))
                {
                    model.FD_Txt_Ref_CDate = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_CDATE"]);
                }
                if (CloseFDTable.Rows.Count > 0)
                {
                    if (!Convert.IsDBNull(CloseFDTable.Rows[0]["FD_DATE"]))
                    {
                        model.FD_TXT_NRC_DATE = Convert.ToDateTime(CloseFDTable.Rows[0]["FD_DATE"]);
                    }
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_ITEM_ID"]))
                {
                    if (Closetable.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                    {
                        model.FD_GLookUp_ItemList = Closetable.Rows[0]["TR_ITEM_ID"].ToString();
                    }
                }
            }
            if (model.iAction == Common.FDAction.Renew_FD)
            {
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                {
                    if (Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_FD_List = Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                    }
                }
                if (PaymentMode.Rows.Count > 0)
                {
                    if (!Convert.IsDBNull(PaymentMode.Rows[0]["TR_MODE"]))
                    {
                        model.FD_Cmb_Rec_Mode = PaymentMode.Rows[0]["TR_MODE"].ToString();
                    }
                    model.FD_Txt_Ref_No = PaymentMode.Rows[0]["TR_REF_NO"].ToString();
                    if (!Convert.IsDBNull(PaymentMode.Rows[0]["TR_REF_DATE"]))
                    {
                        model.FD_Txt_Ref_Date = Convert.ToDateTime(PaymentMode.Rows[0]["TR_REF_DATE"]);
                    }
                    if (!Convert.IsDBNull(PaymentMode.Rows[0]["TR_REF_CDATE"]))
                    {
                        model.FD_Txt_Ref_CDate = Convert.ToDateTime(PaymentMode.Rows[0]["TR_REF_CDATE"]);
                    }
                    model.FD_GLookUp_BankList = PaymentMode.Rows[0]["TR_REF_BANK_ID"].ToString();
                }
                model.FD_GLookUp_ItemList = "4eb60d78-ce90-4a9f-891b-7a82d79dc84b";
                DateTime? fdDate = null;
                if (IsDate(CloseFDTable.Rows[0]["FD_CLOSE_DATE"].ToString()))
                {
                    fdDate = Convert.ToDateTime(CloseFDTable.Rows[0]["FD_CLOSE_DATE"]);
                    model.FD_TXT_NRC_DATE = fdDate;
                }
                model.FD_TXT_RENEWAL_MATURITY_AMOUNT = 0;
                foreach (DataRow CurrRow in Renewtable.Rows)
                {
                    model.FD_TXT_RENEWAL_MATURITY_AMOUNT += Convert.ToDouble(CurrRow["TR_AMOUNT"]);
                }
                model.FD_TXT_REC_AMOUNT = Convert.ToDouble(MasterTable.Rows[0]["TR_SUB_AMT"]);
                if ((model.FD_TXT_REC_AMOUNT != model.FD_TXT_RENEWAL_MATURITY_AMOUNT) && PaymentMode.Rows.Count == 0)
                {
                    model.FD_Cmb_Rec_Mode = "CASH";
                }
                model.FD_TXT_TDS = Convert.ToDouble(MasterTable.Rows[0]["TR_TDS_AMT"]);
            }
            if (model.iAction == Common.FDAction.Close_FD)
            {
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                {
                    if (Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_FD_List = Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_MODE"]))
                {
                    model.FD_Cmb_Rec_Mode = Closetable.Rows[0]["TR_MODE"].ToString();
                }
                model.FD_Txt_Ref_No = Closetable.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_DATE"]))
                {
                    model.FD_Txt_Ref_Date = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_DATE"]);
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_CDATE"]))
                {
                    model.FD_Txt_Ref_CDate = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_CDATE"]);
                }
                model.FD_TXT_TDS = Convert.ToDouble(MasterTable.Rows[0]["TR_TDS_AMT"]);
                model.FD_GLookUp_ItemList = "65730a27-e365-4195-853e-2f59225fe8f4";
                DateTime? fdDate = null;
                if (IsDate(CloseFDTable.Rows[0]["FD_CLOSE_DATE"].ToString()))
                {
                    fdDate = Convert.ToDateTime(CloseFDTable.Rows[0]["FD_CLOSE_DATE"]);
                    model.FD_TXT_NRC_DATE = fdDate;
                }
                model.FD_TXT_REC_AMOUNT = Convert.ToDouble(MasterTable.Rows[0]["TR_SUB_AMT"]);
                model.FD_GLookUp_BankList = Closetable.Rows[0]["TR_REF_BANK_ID"].ToString();
            }
            if (model.iAction == null)
            {
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                {
                    if (Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_FD_List = Closetable.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_MODE"]))
                {
                    model.FD_Cmb_Rec_Mode = Closetable.Rows[0]["TR_MODE"].ToString();
                }
                model.FD_Txt_Ref_No = Closetable.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_DATE"]))
                {
                    model.FD_Txt_Ref_Date = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_DATE"]);
                }
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_CDATE"]))
                {
                    model.FD_Txt_Ref_CDate = Convert.ToDateTime(Closetable.Rows[0]["TR_REF_CDATE"]);
                }
                string _ItemID = Closetable.Rows[0]["TR_ITEM_ID"].ToString();
                if (_ItemID == "c92da5ab-082d-45d9-b6b7-78752625c715")
                {
                    if ((int)BASE._FD_Voucher_DBOps.GetItemCountInSameMaster(model.xMID, "d0219173-45ff-4284-ae0e-89ba0e8d76b4") > 0)
                    {
                        _ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                    }
                }
                model.FD_GLookUp_ItemList = _ItemID;
                model.FD_TXT_RENEWAL_MATURITY_AMOUNT = Convert.ToDouble(Closetable.Rows[0]["TR_AMOUNT"]);
                if (!Convert.IsDBNull(Closetable.Rows[0]["TR_REF_BANK_ID"]))
                {
                    model.FD_GLookUp_BankList = Closetable.Rows[0]["TR_REF_BANK_ID"].ToString();
                }
            }
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_FD(Model_Frm_Voucher_Win_FD model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            if (model.FDiAction != null)
            {
                model.iAction = (Common.FDAction)Enum.Parse(typeof(Common.FDAction), model.FDiAction);
            }
            if (model.iAction == Common.FDAction.Renew_FD)
            {
                return RenewFD_Functonality(model);
            }
            else if (model.iAction == Common.FDAction.Close_FD)
            {
                return CloseFD_Functonality(model);
            }
            else if (model.iAction == Common.FDAction.New_FD)
            {
                return NewFD_Functionality(model);
            }
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList) && model.FD_GLookUp_BankList.ToString().Length > 0)
                    {
                        string AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "").ToString();
                        if (AccNo == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.IsDBNull(AccNo))
                        {
                            AccNo = "";
                        }
                        if (AccNo.Length > 0)
                        {
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted...! In this entry Used Bank A / c No.: " + AccNo + " was closed...!!!";
                            jsonParam.title = "Referred record already changed by some other user";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable FDvoucher_DbOps = BASE._FD_Voucher_DBOps.GetRecord(model.xMID);
                        if (FDvoucher_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current FD");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(FDvoucher_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current FD");
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
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
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
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.FD_GLookUp_ItemList))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_ItemList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_V_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_V_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_V_Date.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.FD_Txt_V_Date) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.FD_Txt_V_Date) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_Txt_V_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_FD_List))
                    {
                        jsonParam.message = "Select FD...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_FD_List";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.FD_Txt_Amount <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.FD_Cmd_Mode) && model.FD_GLookUp_ItemList != "d0219173-45ff-4284-ae0e-89ba0e8d76b4")
                    {
                        jsonParam.message = "Select Mode...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Cmd_Mode";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT==null||model.FD_TXT_RENEWAL_MATURITY_AMOUNT <= 0)
                    {
                        jsonParam.message = "Enter Amount Received...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_RENEWAL_MATURITY_AMOUNT";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.FD_GLookUp_BankList))
                    {
                        model.FD_GLookUp_BankList = "";
                    }
                    if (string.IsNullOrWhiteSpace(model.FD_GLookUp_BankList) && model.FD_GLookUp_ItemList != "d0219173-45ff-4284-ae0e-89ba0e8d76b4" && model.FD_Cmd_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_BankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.FD_GLookUp_BankList))
                    {
                        model.FD_GLookUp_BankList = "";
                    }
                    if (string.IsNullOrEmpty(model.FD_Txt_Ref_No) && model.FD_GLookUp_ItemList != "d0219173-45ff-4284-ae0e-89ba0e8d76b4" && model.FD_Cmd_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_No";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.FD_GLookUp_BankList) && model.FD_GLookUp_ItemList != "d0219173-45ff-4284-ae0e-89ba0e8d76b4" && model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_BankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_Ref_Date.ToString()) == false && model.FD_GLookUp_ItemList != "d0219173-45ff-4284-ae0e-89ba0e8d76b4" && model.FD_Cmd_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_Ref_CDate.ToString()) == true)
                    {
                        if (model.FD_Txt_Ref_CDate < model.FD_Txt_Ref_Date)
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_Txt_Ref_CDate";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.PurposeID_FD))
                    {
                        jsonParam.message = "Purpose is Required. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "PurposeID_FD";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                //-----------------------------// Start Dependencies //--------------------------

                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection || Tag == Common.Navigation_Mode._Edit)
                    {
                        DataTable d1 = default(DataTable);
                        DateTime oldEditOn = default(DateTime);
                        DateTime NewEditOn = default(DateTime);
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                                NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                        //FDs dependency check #Ref I38, AH38
                        if (!string.IsNullOrEmpty(model.GLookUp_FD_List))
                        {
                            if ((model.FD_GLookUp_ItemList != "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" && model.FD_GLookUp_ItemList != "f6e4da62-821f-4961-9f93-f5177fca2a77" && model.FD_GLookUp_ItemList != "65730a27-e365-4195-853e-2f59225fe8f4") || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                            {
                                d1 = BASE._FD_Voucher_DBOps.GetFDs(true, model.GLookUp_FD_List, true);
                            }
                            else
                            {
                                d1 = BASE._FD_Voucher_DBOps.GetFDs(false, model.GLookUp_FD_List);
                            }
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = Convert.ToDateTime(model.FD_List_REC_EDIT_ON);
                            //A/D, E/D
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("FD");
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
                                NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("FD");
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
                //---------------------------// End Dependencies //-----------------------------

                string Status_Action = Convert.ToString((int)Common.Record_Status._Completed);
                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Cr_Led_id = "";
                string Sub_Dr_Led_id = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.iLed_ID;
                    if (model.FD_Cmd_Mode != null && model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        Cr_Led_id = "00079";
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            Sub_Cr_Led_id = "'" + model.FD_GLookUp_BankList + "'";
                        }
                    }
                    else
                    {
                        Cr_Led_id = "00080";
                    }
                }
                else
                {
                    Cr_Led_id = model.iLed_ID;
                    if (model.FD_Cmd_Mode.ToUpper() != "CASH")
                    {
                        Dr_Led_id = "00079";
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            Sub_Dr_Led_id = "'" + model.FD_GLookUp_BankList + "'";
                        }
                    }
                    else
                    {
                        Dr_Led_id = "00080";
                    }
                }

                Param_Txn_IncomeExpenses_InsertVoucherFD InNewParam = new Param_Txn_IncomeExpenses_InsertVoucherFD();
                //new
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    model.xID = Guid.NewGuid().ToString();
                    model.xMID = Guid.NewGuid().ToString();
                    //Try
                    //Master Record 
                    Parameter_InsertMasterInfo_VoucherFD InMInfo = new Parameter_InsertMasterInfo_VoucherFD();
                    InMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    //#5410 fix
                    InMInfo.SubTotal = 0;
                    InMInfo.Cash = 0;
                    InMInfo.Bank = 0;
                    InMInfo.TDS = 0;
                    InMInfo.Status_Action = Status_Action;
                    InMInfo.RecID = model.xMID;
                    InNewParam.param_InsertMaster = InMInfo;
                    InNewParam.PurposeID = model.PurposeID_FD;
                    if (model.FD_GLookUp_ItemList == "d0219173-45ff-4284-ae0e-89ba0e8d76b4")
                    {
                        //TDS by bank
                        Parameter_Insert_VoucherFD InParam = new Parameter_Insert_VoucherFD();
                        InParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        //InParam.TDate = Me.Txt_V_Date.Text.Trim()
                        InParam.ItemID = model.FD_GLookUp_ItemList;
                        InParam.Type = model.iTrans_Type;
                        InParam.Cr_Led_ID = "";
                        InParam.Dr_Led_ID = Dr_Led_id;
                        InParam.SUB_Cr_Led_ID = "";
                        InParam.SUB_Dr_Led_ID = "";
                        InParam.Amount = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        InParam.Mode = "";
                        InParam.Ref_BANK_ID = "";
                        InParam.Ref_Branch = "";
                        InParam.Ref_No = "";
                        InParam.Ref_Date = "";
                        InParam.Ref_CDate = "";
                        InParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParam.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        InParam.MasterTxnID = model.xMID;
                        InParam.Status_Action = Status_Action;                       
                        InParam.RecID = model.xID;
                        InNewParam.param_TDSbyBank = InParam;
                        //iNTEREST RECEIVED
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherFD InParam1 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherFD();
                        InParam1.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParam1.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParam1.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam1.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        //InParam1.TDate = Me.Txt_V_Date.Text.Trim()
                        InParam1.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                        InParam1.Type = "CREDIT";
                        InParam1.Cr_Led_ID = "00069";
                        InParam1.Dr_Led_ID = "";
                        InParam1.SUB_Cr_Led_ID = "";
                        InParam1.SUB_Dr_Led_ID = "";
                        InParam1.Amount = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        InParam1.Mode = "";
                        InParam1.Ref_BANK_ID = "";
                        InParam1.Ref_Branch = "";
                        InParam1.Ref_No = "";
                        InParam1.Ref_Date = "";
                        InParam1.Ref_CDate = "";
                        InParam1.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParam1.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParam1.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParam1.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        InParam1.MasterTxnID = model.xMID;
                        InParam1.Status_Action = Status_Action;
                        InParam1.PurposeID = model.PurposeID_FD;
                        InParam1.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_InsertIntRec = InParam1;
                    }
                    else
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherFD InParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherFD();
                        InParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        //InParams.TDate = Me.Txt_V_Date.Text.Trim()
                        InParams.ItemID = model.FD_GLookUp_ItemList;
                        InParams.Type = model.iTrans_Type;
                        InParams.Cr_Led_ID = Cr_Led_id;
                        InParams.Dr_Led_ID = Dr_Led_id;
                        InParams.SUB_Cr_Led_ID = Sub_Cr_Led_id;
                        InParams.SUB_Dr_Led_ID = Sub_Dr_Led_id;
                        InParams.Amount = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        InParams.Mode = model.FD_Cmd_Mode;
                        InParams.Ref_BANK_ID = !string.IsNullOrEmpty(model.FD_GLookUp_BankList) ? model.FD_GLookUp_BankList : "";
                        InParams.Ref_Branch = !string.IsNullOrEmpty(model.FD_Txt_Branch) ? model.FD_Txt_Branch : "";
                        InParams.Ref_No = !string.IsNullOrEmpty(model.FD_Txt_Ref_No) ? model.FD_Txt_Ref_No : "";
                        if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                        {
                            InParams.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                        }
                        if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                        {
                            InParams.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                        }
                        InParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParams.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        InParams.MasterTxnID = model.xMID;
                        InParams.Status_Action = Status_Action;                       
                        InParams.RecID = model.xID;
                        InNewParam.param_Insert = InParams;
                    }

                    //FCRA Insert Process
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._FD_Voucher_DBOps.InsertIncomeAndExpenses_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = "Success...";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK =model.xMID+ model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_UpdateVoucherFD EditParam = new Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_UpdateVoucherFD();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD UpMInfo = new Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD();
                    UpMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    UpMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    UpMInfo.SubTotal = 0;
                    UpMInfo.Cash = 0;
                    UpMInfo.Bank = 0;
                    UpMInfo.TDS = 0;
                    UpMInfo.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMInfo;
                    EditParam.MID_DeleteTxns = model.xMID;
                    EditParam.PurposeID = model.PurposeID_FD;
                    model.xID = Guid.NewGuid().ToString();
                    if (model.FD_GLookUp_ItemList == "d0219173-45ff-4284-ae0e-89ba0e8d76b4")
                    {
                        Parameter_Insert_VoucherFD InPms = new Parameter_Insert_VoucherFD();
                        InPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPms.ItemID = Convert.ToString(model.FD_GLookUp_ItemList);
                        InPms.Type = model.iTrans_Type;
                        InPms.Cr_Led_ID = "";
                        InPms.Dr_Led_ID = Dr_Led_id;
                        InPms.SUB_Cr_Led_ID = "";
                        InPms.SUB_Dr_Led_ID = "";
                        InPms.Amount = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        InPms.Mode = "";
                        InPms.Ref_BANK_ID = "";
                        InPms.Ref_Branch = "";
                        InPms.Ref_No = "";
                        InPms.Ref_Date = "";
                        InPms.Ref_CDate = "";
                        InPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPms.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        InPms.MasterTxnID = model.xMID;
                        InPms.Status_Action = Status_Action;
                        InPms.RecID = model.xID;
                        EditParam.param_TDSbyBank = InPms;
                        //iNTEREST RECEIVED
                        Parameter_Insert_VoucherFD inParam = new Parameter_Insert_VoucherFD();
                        inParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        inParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            inParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            inParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        inParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                        inParam.Type = "CREDIT";
                        inParam.Cr_Led_ID = "00069";
                        inParam.Dr_Led_ID = "";
                        inParam.SUB_Cr_Led_ID = "";
                        inParam.SUB_Dr_Led_ID = "";
                        inParam.Amount = (double)model.FD_TXT_RENEWAL_MATURITY_AMOUNT;
                        inParam.Mode = "";
                        inParam.Ref_BANK_ID = "";
                        inParam.Ref_Branch = "";
                        inParam.Ref_No = "";
                        inParam.Ref_Date = "";
                        inParam.Ref_CDate = "";
                        inParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        inParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        inParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        inParam.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        inParam.MasterTxnID = model.xMID;
                        inParam.Status_Action = Status_Action;
                        inParam.RecID = Guid.NewGuid().ToString();

                        EditParam.param_InsertIntRec = inParam;
                    }
                    else
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherFD inParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherFD();
                        inParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        inParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            inParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            inParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        inParams.ItemID = model.FD_GLookUp_ItemList;
                        inParams.Type = model.iTrans_Type;
                        inParams.Cr_Led_ID = Cr_Led_id;
                        inParams.Dr_Led_ID = Dr_Led_id;
                        inParams.SUB_Cr_Led_ID = Sub_Cr_Led_id;
                        inParams.SUB_Dr_Led_ID = Sub_Dr_Led_id;
                        inParams.Amount = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        inParams.Mode = model.FD_Cmd_Mode;
                        inParams.Ref_BANK_ID = !string.IsNullOrEmpty(model.FD_GLookUp_BankList) ? model.FD_GLookUp_BankList : "";
                        inParams.Ref_Branch = !string.IsNullOrEmpty(model.FD_Txt_Branch) ? model.FD_Txt_Branch : "";
                        inParams.Ref_No = !string.IsNullOrEmpty(model.FD_Txt_Ref_No) ? model.FD_Txt_Ref_No : "";
                        if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                        {
                            inParams.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            inParams.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                        }
                        if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                        {
                            inParams.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            inParams.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                        }
                        inParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        inParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        inParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        inParams.FDID = !string.IsNullOrEmpty(model.GLookUp_FD_List) ? model.GLookUp_FD_List : "";
                        inParams.MasterTxnID = model.xMID;
                        inParams.Status_Action = Status_Action;                     
                        inParams.RecID = model.xID;
                        EditParam.param_Insert = inParams;
                    }

                    //FCRA Update Process               
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._FD_Voucher_DBOps.UpdateIncomeAndExpenses_Txn(EditParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.UpdateSuccess;
                    jsonParam.title = "Success...";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK = model.xMID + model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_DeleteVoucherFD DelParam = new Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_DeleteVoucherFD();
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;

                    if (!BASE._FD_Voucher_DBOps.DeleteIncomeAndExpenses_Txn(DelParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.title = "Success...";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                return null;
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
        public ActionResult NewFD_Functionality(Model_Frm_Voucher_Win_FD model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            if (BASE.AllowMultiuser())
            {
                if (Tag == Common.Navigation_Mode._Edit | Tag == Common.Navigation_Mode._Delete)
                {
                    DataTable FDvoucher_DbOps = BASE._FD_Voucher_DBOps.GetRecord(model.xMID);
                    if (FDvoucher_DbOps.Rows.Count == 0)
                    {
                        jsonParam.message = Messages.RecordChanged("Current FD");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.LastEditedOn != Convert.ToDateTime(FDvoucher_DbOps.Rows[0]["REC_EDIT_ON"]))
                    {
                        jsonParam.message = Messages.RecordChanged("Current FD");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (Tag == Common.Navigation_Mode._New | Tag == Common.Navigation_Mode._Edit | Tag == Common.Navigation_Mode._New_From_Selection)
            {
                if (string.IsNullOrEmpty(model.FD_GLookUp_ItemList))
                {
                    jsonParam.message = "Item Name Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_GLookUp_ItemList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_V_Date.ToString()) == false)
                {
                    jsonParam.message = "Date Incorrect / Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_V_Date";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_V_Date.ToString()) == true)
                {
                    if (Convert.ToDateTime(model.FD_Txt_V_Date) < Convert.ToDateTime(BASE._open_Year_Sdt)
                        || Convert.ToDateTime(model.FD_Txt_V_Date) > Convert.ToDateTime(BASE._open_Year_Edt))
                    {
                        jsonParam.message = "Date not as per Financial Year...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_V_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.FD_Txt_Amount == null || model.FD_Txt_Amount <= 0) //Redmine Bug #133257 fixed
                {
                    jsonParam.message = "Amount cannot be Zero/Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Amount";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_Cmd_Mode))
                {
                    jsonParam.message = "Select Mode...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Cmd_Mode";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_GLookUp_BankList) && model.FD_Cmd_Mode.ToUpper() != "CASH")
                {
                    jsonParam.message = "Bank Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_GLookUp_BankList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                {
                    model.FD_GLookUp_BankList = "";
                }
                if ((string.IsNullOrEmpty(model.FD_Txt_Ref_No)) & model.FD_Cmd_Mode.ToUpper() != "CASH" & model.FD_Cmd_Mode.ToUpper() != "BANK ACCOUNT")
                {
                    jsonParam.message = "No. Not Specified...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Ref_No";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_Ref_Date.ToString()) == false & model.FD_Cmd_Mode.ToUpper() != "CASH" & model.FD_Cmd_Mode.ToUpper() != "BANK ACCOUNT")
                {
                    jsonParam.message = "Date Incorrect/Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Ref_Date";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                if (IsDate(model.FD_Txt_Ref_CDate.ToString()) == true)
                {
                    if (Convert.ToDateTime(model.FD_Txt_Ref_CDate) < Convert.ToDateTime(model.FD_Txt_Ref_Date))
                    {
                        jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_CDate";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (string.IsNullOrWhiteSpace(model.PurposeID_FD))
                {
                    jsonParam.message = "Purpose is Required. . . !";
                    jsonParam.result = false;
                    jsonParam.title = "Incomplete Information . . .";
                    jsonParam.focusid = "PurposeID_FD";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }         
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
            {
                //model.xMID = Guid.NewGuid().ToString();
                Model_FD_Window fdWindow = new Model_FD_Window();
                fdWindow.Txt_Amount_FD = model.FD_Txt_Amount;
                fdWindow.TempActionMethod = model.ActionMethod;
                fdWindow.Status = model.FDiAction;
                fdWindow.Fdiaction = model.FDiAction;
                fdWindow.TxnID = model.xMID;
                fdWindow.Txt_Date_FD = model.FD_TXT_NRC_DATE;
                fdWindow.Txt_As_Date_FD = model.FD_TXT_NRC_DATE;
                return Frm_FD_Window(fdWindow);            
            }
            if (Tag == Common.Navigation_Mode._Edit)
            {
                Model_FD_Window fdWindow = new Model_FD_Window();
                fdWindow.Txt_Amount_FD = model.FD_Txt_Amount;
                fdWindow.TempActionMethod = model.ActionMethod;
                fdWindow.Status = model.FDiAction;
                fdWindow.Fdiaction = model.FDiAction;
                fdWindow.TxnID = model.xMID;
                fdWindow.Txt_Date_FD = model.FD_TXT_NRC_DATE;
                fdWindow.Txt_As_Date_FD = model.FD_TXT_NRC_DATE;
                return Frm_FD_Window(fdWindow);        
            }
            Param_Txn_NewFD_DeleteVoucherFD DelParam = new Param_Txn_NewFD_DeleteVoucherFD();
            if (Tag == Common.Navigation_Mode._Delete)
            {
                try
                {
                    if (BASE.AllowMultiuser())
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //Closed Bank Acc Check #g35
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Deleted...!<br<br>In this entry, Used Bank A/c No.: " + AccNo.ToString() + " was Closed...!!!";
                                jsonParam.title = "Referred record already changed by some other user...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDate(model.xMID);
                        if (IsDate(CloseDate.ToString()))
                        {
                            jsonParam.message = "Current FD has already been Renewed/Closed.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)BASE._FD_Voucher_DBOps.GetCount(model.xMID, model.CreatedFDID, 1) > 0)
                        {
                            jsonParam.message = "Interest / TDS Posted against Current FD.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DelParam.MID_DeleteFDHistory = model.xMID;
                    DelParam.MID_DeleteFD = model.xMID;
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;


                    if (!BASE._FD_Voucher_DBOps.DeleteNewFD_Txn(DelParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.title = "Success..";
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
            return null;
        }
        public ActionResult NewFD_Functionality_Save(Model_Frm_Voucher_Win_FD model)
        {
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            string Status_Action = Convert.ToString((int)Common.Record_Status._Completed);
            string Dr_Led_id = "";
            string Cr_Led_id = "";
            string Sub_Dr_Led_ID = "";
            string Sub_Cr_Led_ID = "";
            if (model.iTrans_Type.ToUpper() == "DEBIT")
            {
                Dr_Led_id = model.iLed_ID;
                if (model.FD_Cmd_Mode.ToUpper() == "CASH")
                {
                    Cr_Led_id = "00080";
                }
                else
                {
                    Cr_Led_id = "00079";
                    Sub_Cr_Led_ID = model.FD_GLookUp_BankList;
                }
            }
            else
            {
                Cr_Led_id = model.iLed_ID;
                if (model.FD_Cmd_Mode.ToUpper() == "CASH")
                {
                    Dr_Led_id = "00080";
                }
                else
                {
                    Dr_Led_id = "00079";
                    Sub_Dr_Led_ID = model.FD_GLookUp_BankList;
                }
            }
            try
            {
                Param_Txn_NewFD_InsertVoucherFD InNewParam = new Param_Txn_NewFD_InsertVoucherFD();

                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    model.xID = Guid.NewGuid().ToString();                 
                    string FDRecID = model.xID_FDWindow;
                    if (BASE.AllowMultiuser())
                    {
                        if (model.GLookUp_BankListEnabled)
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added....!<br><br>In this entry,Used Bank A/C No.: " + AccNo.ToString() + " was closed...!!";
                                jsonParam.result = false;
                                jsonParam.title = "Referred record already changed by some other user";
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.Look_BankList.Length > 0)
                        {
                            DataTable FDAccount = BASE._FD_Voucher_DBOps.GetFDBankAccounts(model.Look_BankList);
                            if (FDAccount == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            DateTime EditTime = Convert.ToDateTime(model.Look_BankList_RecEditOn);
                            if (FDAccount.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Referred FD Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(FDAccount.Rows[0]["REC_EDIT_ON"]);
                                if (EditTime != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Referred FD Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (BASE.AllowMultiuser())
                    {
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            DataTable d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    try
                    {
                        Parameter_InsertFD_VoucherFD InFD = JsonConvert.DeserializeObject<Parameter_InsertFD_VoucherFD>(model.In_UP_FD);
                        Parameter_InsertFDHistory_VoucherFD InFDHty = JsonConvert.DeserializeObject<Parameter_InsertFDHistory_VoucherFD>(model.In_UP_FDHty);
                        Parameter_InsertFDHistory_VoucherFD InRenFDHis = JsonConvert.DeserializeObject<Parameter_InsertFDHistory_VoucherFD>(model.In_UP_RenFDHis);

                        Parameter_InsertMasterInfo_VoucherFD InMInfo = new Parameter_InsertMasterInfo_VoucherFD();
                        InMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InMInfo.SubTotal = Convert.ToDouble(model.FD_Txt_Amount);
                        InMInfo.Cash = 0;
                        InMInfo.Bank = 0;
                        InMInfo.TDS = 0;
                        InMInfo.Status_Action = Status_Action;
                        InMInfo.RecID = model.xMID;
                        InNewParam.param_InsertMaster = InMInfo;
                        InNewParam.param_InsertFD = InFD;
                        InNewParam.param_InsertFDHistory = InFDHty;

                        Parameter_Insert_VoucherFD InParam = new Parameter_Insert_VoucherFD();
                        InParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParam.ItemID = model.FD_GLookUp_ItemList;
                        InParam.Type = model.iTrans_Type;
                        InParam.Cr_Led_ID = Cr_Led_id;
                        InParam.Dr_Led_ID = Dr_Led_id;
                        InParam.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                        InParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                        InParam.Amount = Convert.ToDouble(model.FD_Txt_Amount);
                        InParam.Mode = model.FD_Cmd_Mode;
                        InParam.Ref_BANK_ID = "";
                        InParam.Ref_Branch = "";
                        InParam.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                        if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                        {
                            InParam.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                        }
                        if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                        {
                            InParam.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                        }
                        InParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParam.FDID = FDRecID;
                        InParam.MasterTxnID = model.xMID;
                        InParam.Status_Action = Status_Action;
                        InParam.PurposeID = model.PurposeID_FD;
                        InParam.RecID = model.xID;
                        InNewParam.param_Insert = InParam;

                        //FCRA Insert Process
                        if (model.FD_SplVchrReferenceSelected != null)
                        {
                            var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                        if (BASE._FD_Voucher_DBOps.InsertNewFD_Txn(InNewParam))
                        {
                            jsonParam.message = Messages.SaveSuccess;
                            jsonParam.title = "Success..";
                            jsonParam.result = true;
                            return Json(new
                            {
                                jsonParam,
                                CashbookGridPK =model.xMID+ model.xID
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
                Param_Txn_NewFD_UpdateVoucherFD EditParam = new Param_Txn_NewFD_UpdateVoucherFD();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    string FDRecID = model.xID_FDWindow;
                    if (BASE.AllowMultiuser())
                    {
                        if (model.Look_BankList.Length > 0)
                        {
                            DataTable FDAccount = BASE._FD_Voucher_DBOps.GetFDBankAccounts(model.Look_BankList);
                            if (FDAccount == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            DateTime EditTime = Convert.ToDateTime(model.Look_BankList_RecEditOn);
                            if (FDAccount.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Referred FD Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(FDAccount.Rows[0]["REC_EDIT_ON"]);
                                if (EditTime != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Referred FD Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (BASE.AllowMultiuser())
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Edited...! In this entry Used Bank A / c No.: " + AccNo + " was closed...!!!";
                                jsonParam.title = "Referred record already changed by some other user";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            DataTable d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDate(model.xMID);
                        if (IsDate(CloseDate.ToString()))
                        {
                            jsonParam.message = "Current FD has already been Renewed/Closed.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)BASE._FD_Voucher_DBOps.GetCount(model.xMID, FDRecID, 1) > 0)
                        {
                            jsonParam.message = "Interest / TDS Posted against Current FD.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Parameter_UpdateFD_VoucherFD UpFD = JsonConvert.DeserializeObject<Parameter_UpdateFD_VoucherFD>(model.In_UP_FD);
                    Parameter_UpdateFDHistory_VoucherFD UpFDHis = JsonConvert.DeserializeObject<Parameter_UpdateFDHistory_VoucherFD>(model.In_UP_FDHty);
                    Parameter_UpdateFDHistory_VoucherFD UpRenFDHty = JsonConvert.DeserializeObject<Parameter_UpdateFDHistory_VoucherFD>(model.In_UP_RenFDHis);

                    Parameter_UpdateMasterInfo_VoucherFD UpMInfo = new Parameter_UpdateMasterInfo_VoucherFD();
                    UpMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    UpMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    UpMInfo.SubTotal = Convert.ToDouble(model.FD_Txt_Amount);
                    UpMInfo.Cash = 0;
                    UpMInfo.Bank = 0;
                    UpMInfo.TDS = 0;
                    UpMInfo.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMInfo;
                    EditParam.param_UpdateFD = UpFD;
                    EditParam.param_UpdateFDHistory = UpFDHis;
                    EditParam.param_DeleteVoucher_Txn_MID = model.xMID;

                    Parameter_Insert_VoucherFD InParam1 = new Parameter_Insert_VoucherFD();
                    InParam1.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InParam1.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InParam1.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InParam1.ItemID = model.FD_GLookUp_ItemList;
                    InParam1.Type = model.iTrans_Type;
                    InParam1.Cr_Led_ID = Cr_Led_id;
                    InParam1.Dr_Led_ID = Dr_Led_id;
                    InParam1.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam1.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam1.Amount = Convert.ToDouble(model.FD_Txt_Amount);
                    InParam1.Mode = model.FD_Cmd_Mode;
                    InParam1.Ref_BANK_ID = "";
                    InParam1.Ref_Branch = "";
                    InParam1.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                    if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                    {
                        InParam1.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                    }
                    if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                    {
                        InParam1.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                    }
                    InParam1.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                    InParam1.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                    InParam1.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                    InParam1.FDID = FDRecID;
                    InParam1.MasterTxnID = model.xMID;
                    InParam1.Status_Action = Status_Action;
                    InParam1.PurposeID = model.PurposeID_FD;
                    InParam1.RecID = model.xID;
                    EditParam.param_Insert = InParam1;

                    //FCRA Update Process               
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (BASE._FD_Voucher_DBOps.UpdateNewFD_Txn(EditParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = model.xMID + model.xID
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
                return null;
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
        private ActionResult RenewFD_Functonality(Model_Frm_Voucher_Win_FD model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            if (BASE.AllowMultiuser())
            {
                if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                {
                    DataTable FDvoucher_DbOps = BASE._FD_Voucher_DBOps.GetRecord(model.xMID, "65730a27-e365-4195-853e-2f59225fe8f4");
                    if (FDvoucher_DbOps.Rows.Count == 0)
                    {
                        jsonParam.message = Messages.RecordChanged("Current FD");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.LastEditedOn != Convert.ToDateTime(FDvoucher_DbOps.Rows[0]["REC_EDIT_ON"]))
                    {
                        jsonParam.message = Messages.RecordChanged("Current FD");
                        jsonParam.title = "Record Already Changed!!";
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
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection)
            {
                if (string.IsNullOrEmpty(model.FD_GLookUp_ItemList))
                {
                    jsonParam.message = "Item Name Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_GLookUp_ItemList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_V_Date.ToString()) == false)
                {
                    jsonParam.message = "Date Incorrect / Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_V_Date";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_V_Date.ToString()) == true)
                {
                    if (Convert.ToDateTime(model.FD_Txt_V_Date) < Convert.ToDateTime(BASE._open_Year_Sdt)
                        || Convert.ToDateTime(model.FD_Txt_V_Date) > Convert.ToDateTime(BASE._open_Year_Edt))
                    {
                        jsonParam.message = "Date not as per Financial Year...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_V_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (string.IsNullOrEmpty(model.GLookUp_FD_List))
                {
                    jsonParam.message = "FD Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "GLookUp_FD_List";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.GLookUp_FD_List))
                {
                    model.GLookUp_FD_List = "";
                }
                if (model.FD_Txt_Amount <= 0)
                {
                    jsonParam.message = "Amount cannot be Zero/Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Amount";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                if (IsDate(model.FD_TXT_NRC_DATE.ToString()) == false)
                {
                    jsonParam.message = "Date Incorrect / Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_TXT_NRC_DATE";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_TXT_NRC_DATE.ToString()) == true)
                {
                    if (Convert.ToDateTime(model.FD_TXT_NRC_DATE) < Convert.ToDateTime(BASE._open_Year_Sdt)
                        || Convert.ToDateTime(model.FD_TXT_NRC_DATE) > Convert.ToDateTime(BASE._open_Year_Edt))
                    {
                        jsonParam.message = "Date not as per Financial Year...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_NRC_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDateTime(model.FD_AsDate) >= model.FD_TXT_NRC_DATE)
                    {
                        jsonParam.message = "Renewal Date must be Greater than FD As Of Date...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_NRC_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT == null)
                {
                    jsonParam.message = "Renewal Amount Not Entered...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_TXT_RENEWAL_MATURITY_AMOUNT";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.FD_TXT_REC_AMOUNT == null)
                {
                    jsonParam.message = "Rceceived Amount Not Entered...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_TXT_REC_AMOUNT";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.FD_TXT_INTEREST == null)// redmine bug 132800 fixed
                {
                    jsonParam.message = "Interest Not Entered...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_TXT_INTEREST";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_GLookUp_BankList) && model.FD_TXT_REC_AMOUNT != model.FD_TXT_RENEWAL_MATURITY_AMOUNT && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                {
                    jsonParam.message = "Bank Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_GLookUp_BankList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                {
                    model.FD_GLookUp_BankList = "";
                }
                if (string.IsNullOrEmpty(model.FD_Txt_Ref_No) && model.FD_Cmb_Rec_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH" && model.FD_TXT_REC_AMOUNT != model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                {
                    jsonParam.message = "No. Not Specified...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Ref_No";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_Ref_Date.ToString()) == false && model.FD_Cmb_Rec_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH" && model.FD_TXT_REC_AMOUNT != model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                {
                    jsonParam.message = "Date Incorrect/Blank..!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Txt_Ref_Date";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.FD_Cmb_Rec_Mode))
                {
                    jsonParam.message = "Receipt Mode Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "FD_Cmb_Rec_Mode";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.FD_Txt_Ref_CDate.ToString()) == true)
                {
                    if (model.FD_Txt_Ref_CDate < model.FD_Txt_Ref_Date)
                    {
                        jsonParam.message = "Clearing Date Cannot Be Less Than Reference Date...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_CDate";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (string.IsNullOrWhiteSpace(model.PurposeID_FD))
                {
                    jsonParam.message = "Purpose is Required. . . !";
                    jsonParam.result = false;
                    jsonParam.title = "Incomplete Information . . .";
                    jsonParam.focusid = "PurposeID_FD";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }          
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
            {
                //model.xMID = Guid.NewGuid().ToString();
                Model_FD_Window fdWindow = new Model_FD_Window();
                fdWindow.Look_BankList_FD = model.FdListBank_ID;
                fdWindow.Txt_Date_FD = model.FD_TXT_NRC_DATE;
                fdWindow.Txt_Amount_FD = model.FD_TXT_RENEWAL_MATURITY_AMOUNT;
                fdWindow.iRenewFrom = model.GLookUp_FD_List;
                fdWindow.iRenewFDNo = model.FD_RenewFromFDNo;
                fdWindow.MatDate = model.FD_MaturityDate;
                fdWindow.TempActionMethod = model.ActionMethod;
                fdWindow.Fdiaction = model.FDiAction;
                fdWindow.Status = model.FD_MaturityDate<=model.FD_TXT_NRC_DATE ? "Matured_Renewed_FD" : "Premature_Renewed_FD";
                fdWindow.TxnID = model.xMID;
                return Frm_FD_Window(fdWindow);          
            }
            if (Tag == Common.Navigation_Mode._Edit)
            {
                Model_FD_Window fdWindow = new Model_FD_Window();
                fdWindow.Look_BankList_FD = model.FdListBank_ID;
                fdWindow.Txt_Date_FD = model.FD_TXT_NRC_DATE;
                fdWindow.Txt_Amount_FD = model.FD_TXT_RENEWAL_MATURITY_AMOUNT;
                fdWindow.iRenewFrom = model.GLookUp_FD_List;
                fdWindow.iRenewFDNo = model.FD_RenewFromFDNo;
                fdWindow.MatDate = model.FD_MaturityDate;
                fdWindow.TempActionMethod = model.ActionMethod;
                fdWindow.Fdiaction = model.FDiAction;
                fdWindow.Status = model.FD_MaturityDate <= model.FD_TXT_NRC_DATE ? "Matured_Renewed_FD" : "Premature_Renewed_FD";
                fdWindow.TxnID = model.xMID;
                return Frm_FD_Window(fdWindow);      
            }
            Param_Txn_RenewFD_DeleteVoucherFD DelParam = new Param_Txn_RenewFD_DeleteVoucherFD();
            //DELETE
            if (Tag == Common.Navigation_Mode._Delete)
            {
                try
                {
                    if (BASE.AllowMultiuser())
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added/Edited/Deleted...! In this entry Used Bank A / c No.: " + AccNo + " was closed...!!!";
                                jsonParam.title = "Referred record already changed by some other user";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDate(model.xMID);
                        if (IsDate(CloseDate.ToString()))
                        {
                            jsonParam.message = "Current FD has already been Renewed/Closed.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        object New_FD = null;
                        New_FD = model.CreatedFDID;
                        if ((int)BASE._FD_Voucher_DBOps.GetCount(model.xMID, New_FD.ToString(), 1) > 0)
                        {
                            jsonParam.message = "Interest / TDS Posted against Current FD.";
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
                    DelParam.MID_DeleteFDHistory = model.xMID;
                    DelParam.MID_DeleteFD = model.xMID;
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;


                    if (BASE._FD_Voucher_DBOps.DeleteRenewFD_Txn(DelParam))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
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
            return null;
        }
        public ActionResult RenewFD_Functonality_Save(Model_Frm_Voucher_Win_FD model)
        {
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                Param_Txn_RenewFD_InsertVoucherFD InNewParam = new Param_Txn_RenewFD_InsertVoucherFD();

                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    model.xID = Guid.NewGuid().ToString();                   
                    string FDRecID = model.xID_FDWindow;
                    if (BASE.AllowMultiuser())
                    {
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added....!<br><br>In this entry,Used Bank A/C No.: " + AccNo.ToString() + " was closed...!!";
                                jsonParam.result = false;
                                jsonParam.title = "Referred record already changed by some other user";
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDateByFdID(model.GLookUp_FD_List);
                        if (IsDate(CloseDate.ToString()))
                        {
                            jsonParam.message = "Current FD has already been Renewed/Closed.";
                            jsonParam.result = false;
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            DataTable d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                        if (!string.IsNullOrEmpty(model.GLookUp_FD_List))
                        {
                            DataTable FDs;
                            if ((model.FD_GLookUp_ItemList != "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" && model.FD_GLookUp_ItemList != "f6e4da62-821f-4961-9f93-f5177fca2a77" && model.FD_GLookUp_ItemList != "65730a27-e365-4195-853e-2f59225fe8f4") || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                            {
                                FDs = BASE._FD_Voucher_DBOps.GetFDs(true, model.GLookUp_FD_List, true);
                            }
                            else
                            {
                                FDs = BASE._FD_Voucher_DBOps.GetFDs(false, model.GLookUp_FD_List);
                            }
                            oldEditOn = Convert.ToDateTime(model.FD_List_REC_EDIT_ON);
                            if (FDs.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("FD");
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
                                DateTime NewEditOn = Convert.ToDateTime(FDs.Rows[0]["REC_EDIT_ON"]);
                                if (NewEditOn != oldEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("FD");
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
                    string Status_Action = Convert.ToString((int)Common.Record_Status._Completed);     
                    string Dr_Led_id = "";string Cr_Led_id = "";
                    if (model.iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.iLed_ID;
                        Cr_Led_id = model.iLed_ID;
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;
                        Dr_Led_id = model.iLed_ID;
                    }
                    try
                    {
                        Parameter_InsertFD_VoucherFD InFD = JsonConvert.DeserializeObject<Parameter_InsertFD_VoucherFD>(model.In_UP_FD);
                        Parameter_InsertFDHistory_VoucherFD InFDHty = JsonConvert.DeserializeObject<Parameter_InsertFDHistory_VoucherFD>(model.In_UP_FDHty);
                        Parameter_InsertFDHistory_VoucherFD InRenFDHis = JsonConvert.DeserializeObject<Parameter_InsertFDHistory_VoucherFD>(model.In_UP_RenFDHis);

                        Parameter_InsertMasterInfo_VoucherFD InMInfo = new Parameter_InsertMasterInfo_VoucherFD();
                        InMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InMInfo.SubTotal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                        InMInfo.Cash = 0;
                        InMInfo.Bank = 0;
                        InMInfo.TDS = Convert.ToDouble(model.FD_TXT_TDS); ;
                        InMInfo.Status_Action = Status_Action;
                        InMInfo.RecID = model.xMID;
                        InNewParam.param_InsertMaster = InMInfo;
                        InNewParam.param_InsertFD = InFD;
                        InNewParam.param_InFDHistory = InFDHty;
                        InNewParam.param_InRenewFDHistory = InRenFDHis;
                        var DrAmount = model.FD_TXT_RENEWAL_MATURITY_AMOUNT;
                        if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT > model.FD_TXT_REC_AMOUNT)
                        {
                            DrAmount = model.FD_TXT_REC_AMOUNT;
                        }
                        Parameter_Insert_VoucherFD InParams = new Parameter_Insert_VoucherFD();
                        InParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParams.ItemID = model.FD_GLookUp_ItemList;
                        InParams.Type = "DEBIT";
                        InParams.Cr_Led_ID = "";
                        InParams.Dr_Led_ID = Dr_Led_id;
                        InParams.SUB_Cr_Led_ID = "";
                        InParams.SUB_Dr_Led_ID = "";
                        InParams.Amount = Convert.ToDouble(DrAmount);
                        InParams.Mode = "";
                        InParams.Ref_BANK_ID = "";
                        InParams.Ref_Branch = "";
                        InParams.Ref_No = "";       
                        InParams.Ref_Date ="";                            
                        InParams.Ref_CDate ="";                        
                        InParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParams.FDID = FDRecID;
                        InParams.MasterTxnID = model.xMID;
                        InParams.Status_Action = Status_Action;
                        InParams.PurposeID = model.PurposeID_FD;
                        InParams.RecID = model.xID;
                        InNewParam.param_InsertNewFD = InParams;

                        double CloseFDJournal = Convert.ToDouble(model.FD_Txt_Amount);
                        if (model.FD_TXT_REC_AMOUNT < CloseFDJournal)
                        {
                            CloseFDJournal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                        }
                        if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT < CloseFDJournal)
                        {
                            CloseFDJournal = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        }
                        if ((CloseFDJournal < model.FD_Txt_Amount) && model.FD_TXT_REC_AMOUNT <= model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                        {
                            CloseFDJournal = Convert.ToDouble(model.FD_Txt_Amount);
                        }
                        Parameter_Insert_VoucherFD InPms = new Parameter_Insert_VoucherFD();
                        InPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPms.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                        InPms.Type = "CREDIT";
                        InPms.Cr_Led_ID = Cr_Led_id;
                        InPms.Dr_Led_ID = "";
                        InPms.SUB_Cr_Led_ID = "";
                        InPms.SUB_Dr_Led_ID = "";
                        InPms.Amount = CloseFDJournal;
                        InPms.Mode = "";
                        InPms.Ref_BANK_ID = "";
                        InPms.Ref_Branch = "";
                        InPms.Ref_No = "";
                        InPms.Ref_Date = "";
                        InPms.Ref_CDate = "";
                        InPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPms.FDID = model.GLookUp_FD_List;
                        InPms.MasterTxnID = model.xMID;
                        InPms.Status_Action = Status_Action;
                        InPms.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_InCloseFDJournal = InPms;

                        if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount && model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                        {
                            Parameter_Insert_VoucherFD inParam = new Parameter_Insert_VoucherFD();
                            inParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            inParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                inParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                inParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            inParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                            inParam.Type = "CREDIT";
                            inParam.Cr_Led_ID = Cr_Led_id;
                            inParam.Dr_Led_ID = "";
                            inParam.SUB_Cr_Led_ID = "";
                            inParam.SUB_Dr_Led_ID = "";
                            inParam.Amount = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                            inParam.Mode = "";
                            inParam.Ref_BANK_ID = "";
                            inParam.Ref_Branch = "";
                            inParam.Ref_No = "";
                            inParam.Ref_Date = "";
                            inParam.Ref_CDate = "";
                            inParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            inParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            inParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            inParam.FDID = model.GLookUp_FD_List;
                            inParam.MasterTxnID = model.xMID;
                            inParam.Status_Action = Status_Action;
                            inParam.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InAdjExcessAmtRec = inParam;
                        }
                        if (model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT && model.FD_Txt_Amount > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                        {
                            double BalanceReceived = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                            if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT < model.FD_Txt_Amount && model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                            {
                                BalanceReceived = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                            }
                            if (model.FD_Cmb_Rec_Mode != "CASH")
                            {
                                Parameter_Insert_VoucherFD InPmts = new Parameter_Insert_VoucherFD();
                                InPmts.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InPmts.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InPmts.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPmts.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InPmts.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                                InPmts.Type = "CREDIT";
                                InPmts.Cr_Led_ID = Cr_Led_id;
                                InPmts.Dr_Led_ID = "00079";
                                InPmts.SUB_Cr_Led_ID = "";
                                InPmts.SUB_Dr_Led_ID = model.FD_GLookUp_BankList??"";
                                InPmts.Amount = BalanceReceived;
                                InPmts.Mode = model.FD_Cmb_Rec_Mode;
                                InPmts.Ref_BANK_ID = model.FD_GLookUp_BankList;
                                InPmts.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                                InPmts.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                                if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                                {
                                    InPmts.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPmts.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                                }
                                if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                                {
                                    InPmts.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPmts.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                                }
                                InPmts.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InPmts.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InPmts.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InPmts.FDID = model.GLookUp_FD_List;
                                InPmts.MasterTxnID = model.xMID;
                                InPmts.Status_Action = Status_Action;
                                InPmts.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InBalRec_Notcash = InPmts;
                            }
                            else
                            {
                                Parameter_Insert_VoucherFD InPams = new Parameter_Insert_VoucherFD();
                                InPams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InPams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InPams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InPams.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                                InPams.Type = "CREDIT";
                                InPams.Cr_Led_ID = Cr_Led_id;
                                InPams.Dr_Led_ID = "00080";
                                InPams.SUB_Cr_Led_ID = "";
                                InPams.SUB_Dr_Led_ID = "";
                                InPams.Amount = BalanceReceived;
                                InPams.Mode = model.FD_Cmb_Rec_Mode;
                                InPams.Ref_BANK_ID = "";
                                InPams.Ref_Branch = "";
                                InPams.Ref_No = "";
                                InPams.Ref_Date = "";
                                InPams.Ref_CDate = "";
                                InPams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InPams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InPams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InPams.FDID = model.GLookUp_FD_List;
                                InPams.MasterTxnID = model.xMID;
                                InPams.Status_Action = Status_Action;
                                InPams.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InBalRec_Cash = InPams;
                            }
                        }
                        double Journal_Interest = 0;
                        double Credited_Interest = 0;
                        double Payment = 0;
                        if (model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                        {
                            if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                            {
                                if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT > model.FD_Txt_Amount)
                                {
                                    Journal_Interest = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT - model.FD_Txt_Amount);
                                    Credited_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT) - Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                                }
                                else
                                {
                                    Credited_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT) - Convert.ToDouble(model.FD_Txt_Amount);
                                }
                            }
                        }
                        else if (model.FD_TXT_REC_AMOUNT < model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                        {
                            Payment = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT - model.FD_TXT_REC_AMOUNT);
                            if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                            {
                                Journal_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_Txt_Amount);
                            }
                        }
                        else
                        {
                            if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                            {
                                Journal_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_Txt_Amount);
                            }
                        }
                        if (Journal_Interest > 0)
                        {
                            Parameter_Insert_VoucherFD inPams = new Parameter_Insert_VoucherFD();
                            inPams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            inPams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                inPams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                inPams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            inPams.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                            inPams.Type = "CREDIT";
                            inPams.Cr_Led_ID = "00069";
                            inPams.Dr_Led_ID = "";
                            inPams.SUB_Cr_Led_ID = "";
                            inPams.SUB_Dr_Led_ID = "";
                            inPams.Amount = Journal_Interest;
                            inPams.Mode = "";
                            inPams.Ref_BANK_ID = "";
                            inPams.Ref_Branch = "";
                            inPams.Ref_No = "";
                            inPams.Ref_Date = "";
                            inPams.Ref_CDate = "";
                            inPams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            inPams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            inPams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            inPams.FDID = model.GLookUp_FD_List;
                            inPams.MasterTxnID = model.xMID;
                            inPams.Status_Action = Status_Action;
                            inPams.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InJournalInterest = inPams;
                        }
                        if (Credited_Interest > 0)
                        {
                            if (model.FD_Cmb_Rec_Mode != "CASH")
                            {
                                Parameter_Insert_VoucherFD InParameter = new Parameter_Insert_VoucherFD();
                                InParameter.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InParameter.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InParameter.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParameter.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InParameter.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                                InParameter.Type = "CREDIT";
                                InParameter.Cr_Led_ID = "00069";
                                InParameter.Dr_Led_ID = "00079";
                                InParameter.SUB_Cr_Led_ID = "";
                                InParameter.SUB_Dr_Led_ID = model.FD_GLookUp_BankList;
                                InParameter.Amount = Credited_Interest;
                                InParameter.Mode = model.FD_Cmb_Rec_Mode;
                                InParameter.Ref_BANK_ID = model.FD_GLookUp_BankList;
                                InParameter.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                                InParameter.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                                if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                                {
                                    InParameter.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParameter.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                                }
                                if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                                {
                                    InParameter.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParameter.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                                }
                                InParameter.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InParameter.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InParameter.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InParameter.FDID = model.GLookUp_FD_List;
                                InParameter.MasterTxnID = model.xMID;
                                InParameter.Status_Action = Status_Action;
                                InParameter.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InCreditedInterest_Notcash = InParameter;
                            }
                            else
                            {
                                Parameter_Insert_VoucherFD InParameters = new Parameter_Insert_VoucherFD();
                                InParameters.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InParameters.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InParameters.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParameters.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InParameters.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                                InParameters.Type = "CREDIT";
                                InParameters.Cr_Led_ID = "00069";
                                InParameters.Dr_Led_ID = "00080";
                                InParameters.SUB_Cr_Led_ID = "";
                                InParameters.SUB_Dr_Led_ID = "";
                                InParameters.Amount = Credited_Interest;
                                InParameters.Mode = model.FD_Cmb_Rec_Mode;
                                InParameters.Ref_BANK_ID = "";
                                InParameters.Ref_Branch = "";
                                InParameters.Ref_No = "";
                                InParameters.Ref_Date = "";
                                InParameters.Ref_CDate = "";
                                InParameters.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InParameters.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InParameters.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InParameters.FDID = model.GLookUp_FD_List;
                                InParameters.MasterTxnID = model.xMID;
                                InParameters.Status_Action = Status_Action;
                                InParameters.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InCreditedInterest_Cash = InParameters;
                            }
                        }
                        if (Payment > 0)
                        {
                            if (model.FD_Cmb_Rec_Mode != "CASH")
                            {
                                Parameter_Insert_VoucherFD InPrms = new Parameter_Insert_VoucherFD();
                                InPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InPrms.ItemID = model.FD_GLookUp_ItemList;
                                InPrms.Type = "DEBIT";
                                InPrms.Cr_Led_ID = "00079";
                                InPrms.Dr_Led_ID = Dr_Led_id;
                                InPrms.SUB_Cr_Led_ID = model.FD_GLookUp_BankList;
                                InPrms.SUB_Dr_Led_ID = "";
                                InPrms.Amount = Payment;
                                InPrms.Mode = model.FD_Cmb_Rec_Mode;
                                InPrms.Ref_BANK_ID = model.FD_GLookUp_BankList;
                                InPrms.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                                InPrms.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                                if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                                {
                                    InPrms.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPrms.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                                }
                                if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                                {
                                    InPrms.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InPrms.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                                }
                                InPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InPrms.FDID = FDRecID;
                                InPrms.MasterTxnID = model.xMID;
                                InPrms.Status_Action = Status_Action;
                                InPrms.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InPmt_NotCash = InPrms;
                            }
                            else
                            {
                                Parameter_Insert_VoucherFD InParm = new Parameter_Insert_VoucherFD();
                                InParm.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                                InParm.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                                if (IsDate(model.FD_Txt_V_Date.ToString()))
                                {
                                    InParm.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParm.TDate = Convert.ToString(model.FD_Txt_V_Date);
                                }
                                InParm.ItemID = model.FD_GLookUp_ItemList;
                                InParm.Type = "DEBIT";
                                InParm.Cr_Led_ID = "00080";
                                InParm.Dr_Led_ID = Dr_Led_id;
                                InParm.SUB_Cr_Led_ID = "";
                                InParm.SUB_Dr_Led_ID = "";
                                InParm.Amount = Payment;
                                InParm.Mode = model.FD_Cmb_Rec_Mode;
                                InParm.Ref_BANK_ID = "";
                                InParm.Ref_Branch = "";
                                InParm.Ref_No = "";
                                InParm.Ref_Date = "";
                                InParm.Ref_CDate = "";
                                InParm.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                                InParm.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                                InParm.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                                InParm.FDID = FDRecID;
                                InParm.MasterTxnID = model.xMID;
                                InParm.Status_Action = Status_Action;
                                InParm.RecID = Guid.NewGuid().ToString();
                                InNewParam.param_InPmt_Cash = InParm;
                            }
                        }
                        double InterestOverhead = 0;
                        if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount)
                        {
                            InterestOverhead = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                        }
                        if (InterestOverhead > 0)
                        {
                            DataTable BankCharges = BASE._FD_Voucher_DBOps.GetBankChargesItemDetail();
                            Parameter_Insert_VoucherFD IntParam = new Parameter_Insert_VoucherFD();
                            IntParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            IntParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                IntParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IntParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            IntParam.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4";
                            IntParam.Type = BankCharges.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                            IntParam.Cr_Led_ID = "";
                            IntParam.Dr_Led_ID = BankCharges.Rows[0]["ITEM_LED_ID"].ToString();
                            IntParam.SUB_Cr_Led_ID = "";
                            IntParam.SUB_Dr_Led_ID = "";
                            IntParam.Amount = InterestOverhead;
                            IntParam.Mode = "";
                            IntParam.Ref_BANK_ID = "";
                            IntParam.Ref_Branch = "";
                            IntParam.Ref_No = "";
                            IntParam.Ref_Date = "";
                            IntParam.Ref_CDate = "";
                            IntParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            IntParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            IntParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            IntParam.FDID = model.GLookUp_FD_List;
                            IntParam.MasterTxnID = model.xMID;
                            IntParam.Status_Action = Status_Action;
                            IntParam.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InInterestOverhead = IntParam;
                        }
                        if (model.FD_TXT_TDS > 0)
                        {
                            Parameter_Insert_VoucherFD IntParams = new Parameter_Insert_VoucherFD();
                            IntParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            IntParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                IntParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IntParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            IntParams.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                            IntParams.Type = "DEBIT";
                            IntParams.Cr_Led_ID = "";
                            IntParams.Dr_Led_ID = "00008";
                            IntParams.SUB_Cr_Led_ID = "";
                            IntParams.SUB_Dr_Led_ID = "";
                            IntParams.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                            IntParams.Mode = "";
                            IntParams.Ref_BANK_ID = "";
                            IntParams.Ref_Branch = "";
                            IntParams.Ref_No = "";
                            IntParams.Ref_Date = "";
                            IntParams.Ref_CDate = "";
                            IntParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            IntParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            IntParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            IntParams.FDID = model.GLookUp_FD_List;
                            IntParams.MasterTxnID = model.xMID;
                            IntParams.Status_Action = Status_Action;
                            IntParams.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InTDS_Deducted1 = IntParams;

                            Parameter_Insert_VoucherFD InsParam = new Parameter_Insert_VoucherFD();
                            InsParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            InsParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                InsParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InsParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            InsParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                            InsParam.Type = "CREDIT";
                            InsParam.Cr_Led_ID = "00069";
                            InsParam.Dr_Led_ID = "";
                            InsParam.SUB_Cr_Led_ID = "";
                            InsParam.SUB_Dr_Led_ID = "";
                            InsParam.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                            InsParam.Mode = "";
                            InsParam.Ref_BANK_ID = "";
                            InsParam.Ref_Branch = "";
                            InsParam.Ref_No = "";
                            InsParam.Ref_Date = "";
                            InsParam.Ref_CDate = "";
                            InsParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            InsParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            InsParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            InsParam.FDID = model.GLookUp_FD_List;
                            InsParam.MasterTxnID = model.xMID;
                            InsParam.Status_Action = Status_Action;
                            InsParam.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InTDS_Deducted2 = InsParam;
                        }
                    }
                    catch (Exception ex)
                    {
                        BASE._FD_Voucher_DBOps.DeleteFDHistory(model.xMID);
                        BASE._FD_Voucher_DBOps.DeleteFD(model.xMID);
                        string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                        jsonParam.message = msg;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //FCRA Insert Process
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._FD_Voucher_DBOps.InsertRenewFD_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK =model.xMID+ model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Txn_RenewFD_UpdateVoucherFD EditParam = new Param_Txn_RenewFD_UpdateVoucherFD();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    string FDRecID = model.xID_FDWindow;
                    if (BASE.AllowMultiuser())
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;                           
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added/Edited/Deleted...! In this entry Used Bank A / c No.: " + AccNo + " was closed...!!!";
                                jsonParam.title = "Referred record already changed by some other user";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDate(model.xMID);
                        if (IsDate(CloseDate.ToString()))
                        {
                            jsonParam.message = "Current FD has already been Renewed/Closed.";
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            DataTable BankAcc = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (BankAcc.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                                DateTime NewEditOn = Convert.ToDateTime(BankAcc.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
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

                        if ((int)BASE._FD_Voucher_DBOps.GetCount(model.xMID, FDRecID, 1) > 0)
                        {
                            jsonParam.message = "Interest / TDS Posted against Current FD.";
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
                    string Status_Action = Convert.ToString((int)Common.Record_Status._Completed);           
                    string Dr_Led_id = "", Cr_Led_id = "";
                    if (model.iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.iLed_ID;
                        Cr_Led_id = model.iLed_ID;
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;
                        Dr_Led_id = model.iLed_ID;
                    }
                    Parameter_UpdateFD_VoucherFD UpFD = JsonConvert.DeserializeObject<Parameter_UpdateFD_VoucherFD>(model.In_UP_FD);
                    Parameter_UpdateFDHistory_VoucherFD UpFDHis = JsonConvert.DeserializeObject<Parameter_UpdateFDHistory_VoucherFD>(model.In_UP_FDHty);
                    Parameter_UpdateFDHistory_VoucherFD UpRenFDHty = JsonConvert.DeserializeObject<Parameter_UpdateFDHistory_VoucherFD>(model.In_UP_RenFDHis);

                    Parameter_UpdateMasterInfo_VoucherFD UpMInfo = new Parameter_UpdateMasterInfo_VoucherFD();
                    UpMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    UpMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    UpMInfo.SubTotal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                    UpMInfo.Cash = 0;
                    UpMInfo.Bank = 0;
                    UpMInfo.TDS = Convert.ToDouble(model.FD_TXT_TDS);
                    UpMInfo.RecID = model.xMID; ;
                    EditParam.param_UpdateMaster = UpMInfo;
                    EditParam.param_UpdateFD = UpFD;
                    EditParam.param_UpFDHistory = UpFDHis;
                    EditParam.param_UpRenFDHistory = UpRenFDHty;
                    EditParam.MID_DeleteTxns = model.xMID;
                    model.xID = Guid.NewGuid().ToString();

                    var DrAmount = model.FD_TXT_RENEWAL_MATURITY_AMOUNT;
                    if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT > model.FD_TXT_REC_AMOUNT)
                    {
                        DrAmount = model.FD_TXT_REC_AMOUNT;
                    }
                    Parameter_Insert_VoucherFD InsPms = new Parameter_Insert_VoucherFD();
                    InsPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InsPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InsPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InsPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InsPms.ItemID = model.FD_GLookUp_ItemList;
                    InsPms.Type = "DEBIT";
                    InsPms.Cr_Led_ID = "";
                    InsPms.Dr_Led_ID = Dr_Led_id;
                    InsPms.SUB_Cr_Led_ID = "";
                    InsPms.SUB_Dr_Led_ID = "";
                    InsPms.Amount = Convert.ToDouble(DrAmount);
                    InsPms.Mode = "";
                    InsPms.Ref_BANK_ID = "";
                    InsPms.Ref_Branch = "";
                    InsPms.Ref_No = "";
                    InsPms.Ref_Date = "";
                    InsPms.Ref_CDate = "";
                    InsPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                    InsPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                    InsPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                    InsPms.FDID = FDRecID;
                    InsPms.MasterTxnID = model.xMID;
                    InsPms.Status_Action = Status_Action;
                    InsPms.PurposeID = model.PurposeID_FD;
                    InsPms.RecID = model.xID;

                    EditParam.param_InsertNewFD = InsPms;

                    double CloseFDJournal = Convert.ToDouble(model.FD_Txt_Amount);
                    if (model.FD_TXT_REC_AMOUNT < CloseFDJournal)
                    {
                        CloseFDJournal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                    }
                    if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT < CloseFDJournal)
                    {
                        CloseFDJournal = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                    }
                    if (CloseFDJournal < model.FD_Txt_Amount && model.FD_TXT_REC_AMOUNT <= model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                    {
                        CloseFDJournal = Convert.ToDouble(model.FD_Txt_Amount);
                    }
                    Parameter_Insert_VoucherFD InsPrms = new Parameter_Insert_VoucherFD();
                    InsPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InsPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InsPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InsPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InsPrms.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                    InsPrms.Type = "CREDIT";
                    InsPrms.Cr_Led_ID = Cr_Led_id;
                    InsPrms.Dr_Led_ID = "";
                    InsPrms.SUB_Cr_Led_ID = "";
                    InsPrms.SUB_Dr_Led_ID = "";
                    InsPrms.Amount = CloseFDJournal;
                    InsPrms.Mode = "";
                    InsPrms.Ref_BANK_ID = "";
                    InsPrms.Ref_Branch = "";
                    InsPrms.Ref_No = "";
                    InsPrms.Ref_Date = "";
                    InsPrms.Ref_CDate = "";
                    InsPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                    InsPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                    InsPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                    InsPrms.FDID = model.GLookUp_FD_List;
                    InsPrms.MasterTxnID = model.xMID;
                    //InsPrms.PurposeID = model.PurposeID_FD;
                    InsPrms.Status_Action = Status_Action;
                    InsPrms.RecID = Guid.NewGuid().ToString();
                    EditParam.param_InCloseFDJournal = InsPrms;

                    if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount && model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                    {
                        Parameter_Insert_VoucherFD InstParam = new Parameter_Insert_VoucherFD();
                        InstParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InstParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InstParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InstParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InstParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                        InstParam.Type = "CREDIT";
                        InstParam.Cr_Led_ID = Cr_Led_id;
                        InstParam.Dr_Led_ID = "";
                        InstParam.SUB_Cr_Led_ID = "";
                        InstParam.SUB_Dr_Led_ID = "";
                        InstParam.Amount = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                        InstParam.Mode = "";
                        InstParam.Ref_BANK_ID = "";
                        InstParam.Ref_Branch = "";
                        InstParam.Ref_No = "";
                        InstParam.Ref_Date = "";
                        InstParam.Ref_CDate = "";
                        InstParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InstParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InstParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InstParam.FDID = model.GLookUp_FD_List;
                        InstParam.MasterTxnID = model.xMID;
                        InstParam.Status_Action = Status_Action;
                        InstParam.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InAdjExcessAmtRec = InstParam;
                    }
                    if (model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT && model.FD_Txt_Amount > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                    {
                        double BalanceReceived = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT < model.FD_Txt_Amount && model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                        {
                            BalanceReceived = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                        }
                        if (model.FD_Cmb_Rec_Mode != "CASH")
                        {
                            Parameter_Insert_VoucherFD IParam = new Parameter_Insert_VoucherFD();
                            IParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            IParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                IParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            IParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                            IParam.Type = "CREDIT";
                            IParam.Cr_Led_ID = Cr_Led_id;
                            IParam.Dr_Led_ID = "00079";
                            IParam.SUB_Cr_Led_ID = "";
                            IParam.SUB_Dr_Led_ID = model.FD_GLookUp_BankList??"";
                            IParam.Amount = BalanceReceived;
                            IParam.Mode = model.FD_Cmb_Rec_Mode;
                            IParam.Ref_BANK_ID = model.FD_GLookUp_BankList;
                            IParam.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                            IParam.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                            if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                            {
                                IParam.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IParam.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                            }
                            if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                            {
                                IParam.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IParam.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                            }
                            IParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            IParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            IParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            IParam.FDID = model.GLookUp_FD_List;
                            IParam.MasterTxnID = model.xMID;
                            IParam.Status_Action = Status_Action;
                            IParam.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InBalRec_Notcash = IParam;
                        }
                        else
                        {
                            Parameter_Insert_VoucherFD IParams = new Parameter_Insert_VoucherFD();
                            IParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            IParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                IParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                IParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            IParams.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4";
                            IParams.Type = "CREDIT";
                            IParams.Cr_Led_ID = Cr_Led_id;
                            IParams.Dr_Led_ID = "00080";
                            IParams.SUB_Cr_Led_ID = "";
                            IParams.SUB_Dr_Led_ID = "";
                            IParams.Amount = BalanceReceived;
                            IParams.Mode = model.FD_Cmb_Rec_Mode;
                            IParams.Ref_BANK_ID = "";
                            IParams.Ref_Branch = "";
                            IParams.Ref_No = "";
                            IParams.Ref_Date = "";
                            IParams.Ref_CDate = "";
                            IParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            IParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            IParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            IParams.FDID = model.GLookUp_FD_List;
                            IParams.MasterTxnID = model.xMID;
                            IParams.Status_Action = Status_Action;
                            IParams.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InBalRec_Cash = IParams;
                        }
                    }

                    double Journal_Interest = 0;
                    double Credited_Interest = 0;
                    double Payment = 0;
                    if (model.FD_TXT_REC_AMOUNT > model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                    {
                        if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                        {
                            if (model.FD_TXT_RENEWAL_MATURITY_AMOUNT > model.FD_Txt_Amount)
                            {
                                Journal_Interest = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT - model.FD_Txt_Amount);
                                Credited_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT) - Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT);
                            }
                            else
                            {
                                Credited_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT) - Convert.ToDouble(model.FD_Txt_Amount);
                            }
                        }
                    }
                    else if (model.FD_TXT_REC_AMOUNT < model.FD_TXT_RENEWAL_MATURITY_AMOUNT)
                    {
                        Payment = Convert.ToDouble(model.FD_TXT_RENEWAL_MATURITY_AMOUNT - model.FD_TXT_REC_AMOUNT);
                        if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                        {
                            Journal_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_Txt_Amount);
                        }
                    }
                    else
                    {
                        if (model.FD_TXT_REC_AMOUNT > model.FD_Txt_Amount)
                        {
                            Journal_Interest = Convert.ToDouble(model.FD_TXT_REC_AMOUNT - model.FD_Txt_Amount);
                        }
                    }
                    if (Journal_Interest > 0)
                    {
                        Parameter_Insert_VoucherFD IPrms = new Parameter_Insert_VoucherFD();
                        IPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        IPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            IPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            IPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        IPrms.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                        IPrms.Type = "CREDIT";
                        IPrms.Cr_Led_ID = "00069";
                        IPrms.Dr_Led_ID = "";
                        IPrms.SUB_Cr_Led_ID = "";
                        IPrms.SUB_Dr_Led_ID = "";
                        IPrms.Amount = Journal_Interest;
                        IPrms.Mode = "";
                        IPrms.Ref_BANK_ID = "";
                        IPrms.Ref_Branch = "";
                        IPrms.Ref_No = "";
                        IPrms.Ref_Date = "";
                        IPrms.Ref_CDate = "";
                        IPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        IPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        IPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        IPrms.FDID = model.GLookUp_FD_List;
                        IPrms.MasterTxnID = model.xMID;
                        IPrms.PurposeID = model.PurposeID_FD;
                        //IPrms.Status_Action = Status_Action;
                        IPrms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InJournalInterest = IPrms;
                    }
                    if (Credited_Interest > 0)
                    {
                        if (model.FD_Cmb_Rec_Mode != "CASH")
                        {
                            Parameter_Insert_VoucherFD InParam = new Parameter_Insert_VoucherFD();
                            InParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            InParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                InParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            InParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                            InParam.Type = "CREDIT";
                            InParam.Cr_Led_ID = "00069";
                            InParam.Dr_Led_ID = "00079";
                            InParam.SUB_Cr_Led_ID = "";
                            InParam.SUB_Dr_Led_ID = model.FD_GLookUp_BankList;
                            InParam.Amount = Credited_Interest;
                            InParam.Mode = model.FD_Cmb_Rec_Mode;
                            InParam.Ref_BANK_ID = model.FD_GLookUp_BankList;
                            InParam.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                            InParam.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                            if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                            {
                                InParam.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                            }
                            if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                            {
                                InParam.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                            }
                            InParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            InParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            InParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            InParam.FDID = model.GLookUp_FD_List;
                            InParam.MasterTxnID = model.xMID;
                            InParam.Status_Action = Status_Action;
                            InParam.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InCreditedInterest_Notcash = InParam;
                        }
                        else
                        {
                            Parameter_Insert_VoucherFD InParams = new Parameter_Insert_VoucherFD();
                            InParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            InParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                InParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            InParams.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                            InParams.Type = "CREDIT";
                            InParams.Cr_Led_ID = "00069";
                            InParams.Dr_Led_ID = "00080";
                            InParams.SUB_Cr_Led_ID = "";
                            InParams.SUB_Dr_Led_ID = "";
                            InParams.Amount = Credited_Interest;
                            InParams.Mode = model.FD_Cmb_Rec_Mode;
                            InParams.Ref_BANK_ID = "";
                            InParams.Ref_Branch = "";
                            InParams.Ref_No = "";
                            InParams.Ref_Date = "";
                            InParams.Ref_CDate = "";
                            InParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            InParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            InParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            InParams.FDID = model.GLookUp_FD_List;
                            InParams.MasterTxnID = model.xMID;
                            InParams.Status_Action = Status_Action;
                            InParams.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InCreditedInterest_Cash = InParams;
                        }
                    }
                    if (Payment > 0)
                    {
                        if (model.FD_Cmb_Rec_Mode != "CASH")
                        {
                            Parameter_Insert_VoucherFD InPrms = new Parameter_Insert_VoucherFD();
                            InPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            InPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                InPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            InPrms.ItemID = model.FD_GLookUp_ItemList;
                            InPrms.Type = "DEBIT";
                            InPrms.Cr_Led_ID = "00079";
                            InPrms.Dr_Led_ID = Dr_Led_id;
                            InPrms.SUB_Cr_Led_ID = model.FD_GLookUp_BankList;
                            InPrms.SUB_Dr_Led_ID = "";
                            InPrms.Amount = Payment;
                            InPrms.Mode = model.FD_Cmb_Rec_Mode;
                            InPrms.Ref_BANK_ID = model.FD_GLookUp_BankList;
                            InPrms.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                            InPrms.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                            if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                            {
                                InPrms.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrms.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                            }
                            if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                            {
                                InPrms.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrms.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                            }
                            InPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            InPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            InPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            InPrms.FDID = FDRecID;
                            InPrms.MasterTxnID = model.xMID;
                            InPrms.Status_Action = Status_Action;
                            InPrms.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InPmt_NotCash = InPrms;
                            InNewParam.param_InPmt_NotCash = InPrms;
                        }
                        else
                        {
                            Parameter_Insert_VoucherFD InPrmts = new Parameter_Insert_VoucherFD();
                            InPrmts.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                            InPrmts.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                            if (IsDate(model.FD_Txt_V_Date.ToString()))
                            {
                                InPrmts.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrmts.TDate = Convert.ToString(model.FD_Txt_V_Date);
                            }
                            InPrmts.ItemID = model.FD_GLookUp_ItemList;
                            InPrmts.Type = "DEBIT";
                            InPrmts.Cr_Led_ID = "00080";
                            InPrmts.Dr_Led_ID = Dr_Led_id;
                            InPrmts.SUB_Cr_Led_ID = "";
                            InPrmts.SUB_Dr_Led_ID = "";
                            InPrmts.Amount = Payment;
                            InPrmts.Mode = model.FD_Cmb_Rec_Mode;
                            InPrmts.Ref_BANK_ID = "";
                            InPrmts.Ref_Branch = "";
                            InPrmts.Ref_No = "";
                            InPrmts.Ref_Date = "";
                            InPrmts.Ref_CDate = "";
                            InPrmts.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                            InPrmts.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                            InPrmts.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                            InPrmts.FDID = FDRecID;
                            InPrmts.MasterTxnID = model.xMID;
                            InPrmts.Status_Action = Status_Action;
                            InPrmts.RecID = Guid.NewGuid().ToString();
                            EditParam.param_InPmt_Cash = InPrmts;
                        }
                    }
                    double InterestOverhead = 0;
                    if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount)
                    {
                        InterestOverhead = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                    }
                    if (InterestOverhead > 0)
                    {
                        DataTable BankCharges = BASE._FD_Voucher_DBOps.GetBankChargesItemDetail();
                        Parameter_Insert_VoucherFD InPms = new Parameter_Insert_VoucherFD();
                        InPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPms.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4";
                        InPms.Type = BankCharges.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                        InPms.Cr_Led_ID = "";
                        InPms.Dr_Led_ID = BankCharges.Rows[0]["ITEM_LED_ID"].ToString();
                        InPms.SUB_Cr_Led_ID = "";
                        InPms.SUB_Dr_Led_ID = "";
                        InPms.Amount = InterestOverhead;
                        InPms.Mode = "";
                        InPms.Ref_BANK_ID = "";
                        InPms.Ref_Branch = "";
                        InPms.Ref_No = "";
                        InPms.Ref_Date = "";
                        InPms.Ref_CDate = "";
                        InPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPms.FDID = model.GLookUp_FD_List;
                        InPms.MasterTxnID = model.xMID;
                        InPms.Status_Action = Status_Action;
                        InPms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InInterestOverhead = InPms;
                    }
                    if (model.FD_TXT_TDS > 0)
                    {
                        Parameter_Insert_VoucherFD InParameter = new Parameter_Insert_VoucherFD();
                        InParameter.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParameter.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParameter.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParameter.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParameter.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                        InParameter.Type = "DEBIT";
                        InParameter.Cr_Led_ID = "";
                        InParameter.Dr_Led_ID = "00008";
                        InParameter.SUB_Cr_Led_ID = "";
                        InParameter.SUB_Dr_Led_ID = "";
                        InParameter.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        InParameter.Mode = "";
                        InParameter.Ref_BANK_ID = "";
                        InParameter.Ref_Branch = "";
                        InParameter.Ref_No = "";
                        InParameter.Ref_Date = "";
                        InParameter.Ref_CDate = "";
                        InParameter.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParameter.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParameter.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParameter.FDID = model.GLookUp_FD_List;
                        InParameter.MasterTxnID = model.xMID;
                        InParameter.Status_Action = Status_Action;
                        InParameter.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InTDS_Deducted1 = InParameter;

                        Parameter_Insert_VoucherFD InParameters = new Parameter_Insert_VoucherFD();
                        InParameters.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParameters.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParameters.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParameters.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParameters.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715";
                        InParameters.Type = "CREDIT";
                        InParameters.Cr_Led_ID = "00069";
                        InParameters.Dr_Led_ID = "";
                        InParameters.SUB_Cr_Led_ID = "";
                        InParameters.SUB_Dr_Led_ID = "";
                        InParameters.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        InParameters.Mode = "";
                        InParameters.Ref_BANK_ID = "";
                        InParameters.Ref_Branch = "";
                        InParameters.Ref_No = "";
                        InParameters.Ref_Date = "";
                        InParameters.Ref_CDate = "";
                        InParameters.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParameters.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParameters.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParameters.FDID = model.GLookUp_FD_List;
                        InParameters.MasterTxnID = model.xMID;
                        InParameters.Status_Action = Status_Action;
                        InParameters.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InTDS_Deducted2 = InParameters;
                    }

                    //FCRA Update Process               
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (BASE._FD_Voucher_DBOps.UpdateRenewFD_Txn(EditParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = model.xMID + model.xID
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
                return null;
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
        public ActionResult CloseFD_Functonality(Model_Frm_Voucher_Win_FD model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                    {
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FD_GLookUp_BankList, "");
                        if (Convert.IsDBNull(AccNo))
                        {
                            AccNo = "";
                        }
                        if (AccNo.ToString().Length > 0)
                        {
                            jsonParam.message = "Entry cannot be Added/Deleted/Edited....!<br><br>In this entry,Used Bank A/C No.: " + AccNo.ToString() + " was closed...!!";
                            jsonParam.result = false;
                            jsonParam.title = "Referred record already changed by some other user";
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Tag == Common.Navigation_Mode._Edit | Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable FDvoucher_DbOps = BASE._FD_Voucher_DBOps.GetRecord(model.xMID, "65730a27-e365-4195-853e-2f59225fe8f4");
                        if (FDvoucher_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current FD");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(FDvoucher_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current FD");
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
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
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
                if (Tag == Common.Navigation_Mode._New | Tag == Common.Navigation_Mode._Edit | Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.FD_GLookUp_ItemList))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_ItemList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_V_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_V_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_V_Date.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.FD_Txt_V_Date) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.FD_Txt_V_Date) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_Txt_V_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_FD_List))
                    {
                        jsonParam.message = "FD Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_FD_List";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_FD_List))
                    {
                        model.GLookUp_FD_List = "";
                    }
                    if (model.FD_Txt_Amount <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_TXT_NRC_DATE.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_NRC_DATE";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_TXT_NRC_DATE.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.FD_TXT_NRC_DATE) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.FD_TXT_NRC_DATE) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_TXT_NRC_DATE";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDateTime(model.FD_Date) >= model.FD_TXT_NRC_DATE)
                        {
                            jsonParam.message = "Closure Date must be Greater than FD Start Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_TXT_NRC_DATE";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.FD_TXT_REC_AMOUNT == null)
                    {
                        jsonParam.message = "Rceceived Amount Not Entered...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_REC_AMOUNT";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.FD_TXT_INTEREST == null)
                    {
                        jsonParam.message = "Interest Not Entered...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_TXT_INTEREST";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.FD_GLookUp_BankList) && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_GLookUp_BankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                    {
                        model.FD_GLookUp_BankList = "";
                    }
                    if (string.IsNullOrEmpty(model.FD_Txt_Ref_No) && model.FD_Cmb_Rec_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_No";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_Ref_Date.ToString()) == false && model.FD_Cmb_Rec_Mode.ToUpper() != "BANK ACCOUNT" && model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Date Incorrect/Blank..!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Txt_Ref_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.FD_Cmb_Rec_Mode) && model.FD_TXT_INTEREST > 0)
                    {
                        jsonParam.message = "Receipt Mode Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "FD_Cmb_Rec_Mode";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.FD_Txt_Ref_CDate.ToString()) == true)
                    {
                        if (model.FD_Txt_Ref_CDate < model.FD_Txt_Ref_Date)
                        {
                            jsonParam.message = "Clearing Date Cannot Be Less Than Reference Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "FD_Txt_Ref_CDate";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.PurposeID_FD))
                    {
                        jsonParam.message = "Purpose is Required. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "PurposeID_FD";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._New | Tag == Common.Navigation_Mode._Edit | Tag == Common.Navigation_Mode._New_From_Selection)
                    {
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.FD_GLookUp_BankList))
                        {
                            DataTable closeFD = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FD_GLookUp_BankList);
                            oldEditOn = Convert.ToDateTime(model.GlookUp_BankList_REC_EDIT_ON);
                            if (closeFD.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                                DateTime NewEditOn = Convert.ToDateTime(closeFD.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
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
                        if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                        {
                            if (!string.IsNullOrEmpty(model.GLookUp_FD_List))
                            {
                                DataTable FDs;
                                if ((model.FD_GLookUp_ItemList != "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" && model.FD_GLookUp_ItemList != "f6e4da62-821f-4961-9f93-f5177fca2a77" && model.FD_GLookUp_ItemList != "65730a27-e365-4195-853e-2f59225fe8f4") || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                                {
                                    FDs = BASE._FD_Voucher_DBOps.GetFDs(true, model.GLookUp_FD_List, true);
                                }
                                else
                                {
                                    FDs = BASE._FD_Voucher_DBOps.GetFDs(false, model.GLookUp_FD_List);
                                }
                                oldEditOn = Convert.ToDateTime(model.FD_List_REC_EDIT_ON);
                                if (FDs.Rows.Count <= 0)
                                {
                                    jsonParam.message = Messages.DependencyChanged("FD");
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
                                    DateTime NewEditOn = Convert.ToDateTime(FDs.Rows[0]["REC_EDIT_ON"]);
                                    if (NewEditOn != oldEditOn)
                                    {
                                        jsonParam.message = Messages.DependencyChanged("FD");
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
                            object CloseDate = BASE._FD_Voucher_DBOps.GetFDCloseDateByFdID(model.GLookUp_FD_List);
                            if (IsDate(CloseDate.ToString()))
                            {
                                jsonParam.message = "Current FD has already been Renewed/Closed.";
                                jsonParam.result = false;
                                jsonParam.title = "Referred Record Already Changed!!";
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
                string Status_Action = ((int)Common.Record_Status._Completed).ToString();
                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Dr_Led_ID = "";
                string Sub_Cr_Led_ID = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.iLed_ID;
                    if (model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                    {
                        Cr_Led_id = "00079";
                        Sub_Cr_Led_ID = model.FD_GLookUp_BankList;
                    }
                    else
                    {
                        Cr_Led_id = "00080";
                    }
                }
                else
                {
                    if (model.FD_Cmb_Rec_Mode.ToUpper() != "CASH")
                    {
                        Dr_Led_id = "00079";
                        Sub_Dr_Led_ID = model.FD_GLookUp_BankList;
                    }
                    else
                    {
                        Dr_Led_id = "00080";
                    }
                    Cr_Led_id = model.iLed_ID;
                }
                Param_Txn_CloseFD_InsertVoucherFD InNewParam = new Param_Txn_CloseFD_InsertVoucherFD();
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    model.xMID = Guid.NewGuid().ToString();
                    model.xID = Guid.NewGuid().ToString();

                    Parameter_InsertMasterInfo_VoucherFD InMinfo = new Parameter_InsertMasterInfo_VoucherFD();
                    InMinfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InMinfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InMinfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMinfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InMinfo.SubTotal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                    InMinfo.Cash = 0;
                    InMinfo.Bank = 0;
                    InMinfo.TDS = Convert.ToDouble(model.FD_TXT_TDS); ;
                    InMinfo.Status_Action = Status_Action;
                    InMinfo.RecID = model.xMID;
                    InNewParam.param_InsertMaster = InMinfo;

                    double Close_Amount = Convert.ToDouble(model.FD_Txt_Amount);
                    double InterestOverhead = 0;
                    if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount)
                    {
                        Close_Amount = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                        InterestOverhead = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                    }
                    Parameter_Insert_VoucherFD InsPrms = new Parameter_Insert_VoucherFD();
                    InsPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InsPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InsPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InsPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InsPrms.ItemID = model.FD_GLookUp_ItemList;
                    InsPrms.Type = model.iTrans_Type;
                    InsPrms.Cr_Led_ID = Cr_Led_id;
                    InsPrms.Dr_Led_ID = Dr_Led_id;
                    InsPrms.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                    InsPrms.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                    InsPrms.Amount = Close_Amount;
                    InsPrms.Mode = model.FD_Cmb_Rec_Mode;
                    InsPrms.Ref_BANK_ID = model.FD_GLookUp_BankList;
                    InsPrms.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                    InsPrms.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                    if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                    {
                        InsPrms.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InsPrms.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                    }
                    if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                    {
                        InsPrms.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InsPrms.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                    }
                    InsPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                    InsPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                    InsPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                    InsPrms.FDID = model.GLookUp_FD_List;
                    InsPrms.MasterTxnID = model.xMID;
                    InsPrms.Status_Action = Status_Action;
                    InsPrms.PurposeID = model.PurposeID_FD;
                    InsPrms.RecID = model.xID;
                    InNewParam.param_Insert = InsPrms;
                    if (InterestOverhead > 0)
                    {
                        Parameter_Insert_VoucherFD InParamts = new Parameter_Insert_VoucherFD();
                        InParamts.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParamts.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParamts.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParamts.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParamts.ItemID = model.FD_GLookUp_ItemList;
                        InParamts.Type = model.iTrans_Type;
                        InParamts.Cr_Led_ID = Cr_Led_id;
                        InParamts.Dr_Led_ID = "";
                        InParamts.SUB_Cr_Led_ID = "";
                        InParamts.SUB_Dr_Led_ID = "";
                        InParamts.Amount = InterestOverhead;
                        InParamts.Mode = "";
                        InParamts.Ref_BANK_ID = "";
                        InParamts.Ref_Branch = "";
                        InParamts.Ref_No = "";
                        InParamts.Ref_Date = "";
                        InParamts.Ref_CDate = "";
                        InParamts.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParamts.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParamts.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParamts.FDID = model.GLookUp_FD_List;
                        InParamts.MasterTxnID = model.xMID;
                        InParamts.Status_Action = Status_Action;
                        InParamts.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_InterestOverhead = InParamts;

                        DataTable BankCharges = BASE._FD_Voucher_DBOps.GetBankChargesItemDetail();
                        Parameter_Insert_VoucherFD InPams = new Parameter_Insert_VoucherFD();
                        InPams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPams.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4";
                        InPams.Type = BankCharges.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                        InPams.Cr_Led_ID = "";
                        InPams.Dr_Led_ID = BankCharges.Rows[0]["ITEM_LED_ID"].ToString();
                        InPams.SUB_Cr_Led_ID = "";
                        InPams.SUB_Dr_Led_ID = "";
                        InPams.Amount = InterestOverhead;
                        InPams.Mode = "";
                        InPams.Ref_BANK_ID = "";
                        InPams.Ref_Branch = "";
                        InPams.Ref_No = "";
                        InPams.Ref_Date = "";
                        InPams.Ref_CDate = "";
                        InPams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPams.FDID = model.GLookUp_FD_List;
                        InPams.MasterTxnID = model.xMID;
                        InPams.Status_Action = Status_Action;
                        InPams.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_InterestOverhead_BankCharges = InPams;
                    }
                    string _Status = Common.FDStatus.Matured_Closed_FD.ToString();
                    if (model.FD_MaturityDate > model.FD_TXT_NRC_DATE)
                    {
                        _Status = Common.FDStatus.Premature_Closed_FD.ToString();
                    }
                    Parameter_InsertFDHistory_VoucherFD InFDHis = new Parameter_InsertFDHistory_VoucherFD();
                    InFDHis.FDID = model.GLookUp_FD_List;
                    InFDHis.FDAction = Common.FDAction.Close_FD.ToString();
                    InFDHis.FDStatus = _Status;
                    InFDHis.TxnID = model.xMID;
                    InFDHis.Status_Action = Status_Action;
                    InFDHis.RecID = Guid.NewGuid().ToString();
                    InNewParam.param_InsertFDHistory = InFDHis;
                    if (model.FD_TXT_INTEREST > 0)
                    {
                        Parameter_Insert_VoucherFD IntParam = new Parameter_Insert_VoucherFD();
                        IntParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        IntParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            IntParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            IntParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        IntParam.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117";
                        IntParam.Type = "CREDIT";
                        IntParam.Cr_Led_ID = "00069";
                        IntParam.Dr_Led_ID = Dr_Led_id;
                        IntParam.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                        IntParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                        IntParam.Amount = Convert.ToDouble(model.FD_TXT_INTEREST);
                        IntParam.Mode = model.FD_Cmb_Rec_Mode;
                        IntParam.Ref_BANK_ID = model.FD_GLookUp_BankList;
                        IntParam.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                        IntParam.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                        if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                        {
                            IntParam.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            IntParam.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                        }
                        if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                        {
                            IntParam.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            IntParam.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                        }
                        IntParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        IntParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        IntParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        IntParam.FDID = model.GLookUp_FD_List;
                        IntParam.MasterTxnID = model.xMID;
                        IntParam.Status_Action = Status_Action;
                        IntParam.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_InFDCloseInterest = IntParam;
                    }
                    if (model.FD_TXT_TDS > 0)
                    {
                        Parameter_Insert_VoucherFD IntParams = new Parameter_Insert_VoucherFD();
                        IntParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        IntParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            IntParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            IntParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        IntParams.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                        IntParams.Type = "DEBIT";
                        IntParams.Cr_Led_ID = "";
                        IntParams.Dr_Led_ID = "00008";
                        IntParams.SUB_Cr_Led_ID = "";
                        IntParams.SUB_Dr_Led_ID = "";
                        IntParams.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        IntParams.Mode = "";
                        IntParams.Ref_BANK_ID = "";
                        IntParams.Ref_Branch = "";
                        IntParams.Ref_No = "";
                        IntParams.Ref_Date = "";
                        IntParams.Ref_CDate = "";
                        IntParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        IntParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        IntParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        IntParams.FDID = model.GLookUp_FD_List;
                        IntParams.MasterTxnID = model.xMID;
                        IntParams.Status_Action = Status_Action;
                        IntParams.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_TDSDeducted1 = IntParams;

                        Parameter_Insert_VoucherFD InParam = new Parameter_Insert_VoucherFD();
                        InParam.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParam.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParam.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117";
                        InParam.Type = "CREDIT";
                        InParam.Cr_Led_ID = "00069";
                        InParam.Dr_Led_ID = "";
                        InParam.SUB_Cr_Led_ID = "";
                        InParam.SUB_Dr_Led_ID = "";
                        InParam.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        InParam.Mode = "";
                        InParam.Ref_BANK_ID = "";
                        InParam.Ref_Branch = "";
                        InParam.Ref_No = "";
                        InParam.Ref_Date = "";
                        InParam.Ref_CDate = "";
                        InParam.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParam.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParam.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParam.FDID = model.GLookUp_FD_List;
                        InParam.MasterTxnID = model.xMID;
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = Guid.NewGuid().ToString();
                        InNewParam.param_TDSDeducted2 = InParam;
                    }

                    //FCRA Insert Process
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (BASE._FD_Voucher_DBOps.InsertCloseFD_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK =model.xMID+ model.xID
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
                Param_Txn_CloseFD_UpdateVoucherFD EditParam = new Param_Txn_CloseFD_UpdateVoucherFD();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    Parameter_UpdateMasterInfo_VoucherFD UpMInfo = new Parameter_UpdateMasterInfo_VoucherFD();
                    UpMInfo.TxnCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    UpMInfo.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    UpMInfo.SubTotal = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                    UpMInfo.Cash = 0;
                    UpMInfo.Bank = 0;
                    UpMInfo.TDS = Convert.ToDouble(model.FD_TXT_TDS); ;
                    UpMInfo.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMInfo;
                    EditParam.MID_DeleteTxns = model.xMID;

                    double Close_Amount = Convert.ToDouble(model.FD_Txt_Amount);
                    double InterestOverhead = 0;
                    if (model.FD_TXT_REC_AMOUNT < model.FD_Txt_Amount)
                    {
                        Close_Amount = Convert.ToDouble(model.FD_TXT_REC_AMOUNT);
                        InterestOverhead = Convert.ToDouble(model.FD_Txt_Amount - model.FD_TXT_REC_AMOUNT);
                    }
                    model.xID = Guid.NewGuid().ToString();
                    Parameter_Insert_VoucherFD InParams = new Parameter_Insert_VoucherFD();
                    InParams.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                    InParams.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                    if (IsDate(model.FD_Txt_V_Date.ToString()))
                    {
                        InParams.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParams.TDate = Convert.ToString(model.FD_Txt_V_Date);
                    }
                    InParams.ItemID = model.FD_GLookUp_ItemList;
                    InParams.Type = model.iTrans_Type;
                    InParams.Cr_Led_ID = Cr_Led_id;
                    InParams.Dr_Led_ID = Dr_Led_id;
                    InParams.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParams.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParams.Amount = Close_Amount;
                    InParams.Mode = model.FD_Cmb_Rec_Mode;
                    InParams.Ref_BANK_ID = model.FD_GLookUp_BankList;
                    InParams.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                    InParams.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                    if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                    {
                        InParams.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParams.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                    }
                    if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                    {
                        InParams.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParams.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                    }
                    InParams.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                    InParams.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                    InParams.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                    InParams.FDID = model.GLookUp_FD_List;
                    InParams.MasterTxnID = model.xMID;
                    InParams.Status_Action = Status_Action;
                    InParams.PurposeID = model.PurposeID_FD;
                    InParams.RecID = model.xID;
                    EditParam.param_InsertCloseAmt = InParams;
                    if (InterestOverhead > 0)
                    {
                        Parameter_Insert_VoucherFD InPms = new Parameter_Insert_VoucherFD();
                        InPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPms.ItemID = model.FD_GLookUp_ItemList;
                        InPms.Type = model.iTrans_Type;
                        InPms.Cr_Led_ID = Cr_Led_id;
                        InPms.Dr_Led_ID = "";
                        InPms.SUB_Cr_Led_ID = "";
                        InPms.SUB_Dr_Led_ID = "";
                        InPms.Amount = InterestOverhead;
                        InPms.Mode = "";
                        InPms.Ref_BANK_ID = "";
                        InPms.Ref_Branch = "";
                        InPms.Ref_No = "";
                        InPms.Ref_Date = "";
                        InPms.Ref_CDate = "";
                        InPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPms.FDID = model.GLookUp_FD_List;
                        InPms.MasterTxnID = model.xMID;
                        InPms.Status_Action = Status_Action;
                        InPms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InterestOverhead = InPms;

                        DataTable BankCharges = BASE._FD_Voucher_DBOps.GetBankChargesItemDetail();
                        Parameter_Insert_VoucherFD InsPms = new Parameter_Insert_VoucherFD();
                        InsPms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InsPms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InsPms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InsPms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InsPms.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4";
                        InsPms.Type = BankCharges.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                        InsPms.Cr_Led_ID = "";
                        InsPms.Dr_Led_ID = BankCharges.Rows[0]["ITEM_LED_ID"].ToString();
                        InsPms.SUB_Cr_Led_ID = "";
                        InsPms.SUB_Dr_Led_ID = "";
                        InsPms.Amount = InterestOverhead;
                        InsPms.Mode = "";
                        InsPms.Ref_BANK_ID = "";
                        InsPms.Ref_Branch = "";
                        InsPms.Ref_No = "";
                        InsPms.Ref_Date = "";
                        InsPms.Ref_CDate = "";
                        InsPms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InsPms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InsPms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InsPms.FDID = model.GLookUp_FD_List;
                        InsPms.MasterTxnID = model.xMID;
                        InsPms.Status_Action = Status_Action;
                        InsPms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InterestOverhead_BankCharges = InsPms;
                    }
                    string _Status = Common.FDStatus.Matured_Closed_FD.ToString();
                    if (model.FD_MaturityDate > model.FD_TXT_NRC_DATE)
                    {
                        _Status = Common.FDStatus.Premature_Closed_FD.ToString();
                    }
                    Parameter_InsertFDHistory_VoucherFD InFDHty = new Parameter_InsertFDHistory_VoucherFD();
                    InFDHty.FDID = model.GLookUp_FD_List;
                    InFDHty.FDAction = Common.FDAction.Close_FD.ToString();
                    InFDHty.FDStatus = _Status;
                    InFDHty.TxnID = model.xMID;
                    InFDHty.Status_Action = Status_Action;
                    InFDHty.RecID = Guid.NewGuid().ToString();
                    EditParam.param_InsertFDHistory = InFDHty;
                    if (model.FD_TXT_INTEREST > 0)
                    {
                        Parameter_Insert_VoucherFD InPrms = new Parameter_Insert_VoucherFD();
                        InPrms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InPrms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InPrms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPrms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InPrms.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117";
                        InPrms.Type = "CREDIT";
                        InPrms.Cr_Led_ID = "00069";
                        InPrms.Dr_Led_ID = Dr_Led_id;
                        InPrms.SUB_Cr_Led_ID = Sub_Cr_Led_ID;
                        InPrms.SUB_Dr_Led_ID = Sub_Dr_Led_ID;
                        InPrms.Amount = Convert.ToDouble(model.FD_TXT_INTEREST);
                        InPrms.Mode = model.FD_Cmb_Rec_Mode;
                        InPrms.Ref_BANK_ID = model.FD_GLookUp_BankList;
                        InPrms.Ref_Branch = model.FD_Txt_Branch == null ? "" : model.FD_Txt_Branch;
                        InPrms.Ref_No = model.FD_Txt_Ref_No == null ? "" : model.FD_Txt_Ref_No;
                        if (IsDate(model.FD_Txt_Ref_Date.ToString()))
                        {
                            InPrms.Ref_Date = Convert.ToDateTime(model.FD_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPrms.Ref_Date = Convert.ToString(model.FD_Txt_Ref_Date);
                        }
                        if (IsDate(model.FD_Txt_Ref_CDate.ToString()))
                        {
                            InPrms.Ref_CDate = Convert.ToDateTime(model.FD_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPrms.Ref_CDate = Convert.ToString(model.FD_Txt_Ref_CDate);
                        }
                        InPrms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InPrms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InPrms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InPrms.FDID = model.GLookUp_FD_List;
                        InPrms.MasterTxnID = model.xMID;
                        InPrms.Status_Action = Status_Action;
                        InPrms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InFDCloseInterest = InPrms;
                    }
                    if (model.FD_TXT_TDS > 0)
                    {
                        Parameter_Insert_VoucherFD InParms = new Parameter_Insert_VoucherFD();
                        InParms.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParms.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParms.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParms.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParms.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4";
                        InParms.Type = "DEBIT";
                        InParms.Cr_Led_ID = "";
                        InParms.Dr_Led_ID = "00008";
                        InParms.SUB_Cr_Led_ID = "";
                        InParms.SUB_Dr_Led_ID = "";
                        InParms.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        InParms.Mode = "";
                        InParms.Ref_BANK_ID = "";
                        InParms.Ref_Branch = "";
                        InParms.Ref_No = "";
                        InParms.Ref_Date = "";
                        InParms.Ref_CDate = "";
                        InParms.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParms.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParms.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParms.FDID = model.GLookUp_FD_List;
                        InParms.MasterTxnID = model.xMID;
                        InParms.Status_Action = Status_Action;
                        InParms.RecID = Guid.NewGuid().ToString();
                        EditParam.param_TDSDeducted1 = InParms;

                        Parameter_Insert_VoucherFD InParamts = new Parameter_Insert_VoucherFD();
                        InParamts.TransCode = (int)Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParamts.VNo = !string.IsNullOrEmpty(model.FD_Txt_V_NO) ? model.FD_Txt_V_NO : "";
                        if (IsDate(model.FD_Txt_V_Date.ToString()))
                        {
                            InParamts.TDate = Convert.ToDateTime(model.FD_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParamts.TDate = Convert.ToString(model.FD_Txt_V_Date);
                        }
                        InParamts.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117";
                        InParamts.Type = "CREDIT";
                        InParamts.Cr_Led_ID = "00069";
                        InParamts.Dr_Led_ID = "";
                        InParamts.SUB_Cr_Led_ID = "";
                        InParamts.SUB_Dr_Led_ID = "";
                        InParamts.Amount = Convert.ToDouble(model.FD_TXT_TDS);
                        InParamts.Mode = "";
                        InParamts.Ref_BANK_ID = "";
                        InParamts.Ref_Branch = "";
                        InParamts.Ref_No = "";
                        InParamts.Ref_Date = "";
                        InParamts.Ref_CDate = "";
                        InParamts.Narration = !string.IsNullOrEmpty(model.FD_Txt_Narration) ? model.FD_Txt_Narration : "";
                        InParamts.Remarks = !string.IsNullOrEmpty(model.FD_Txt_Remarks) ? model.FD_Txt_Remarks : "";
                        InParamts.Reference = !string.IsNullOrEmpty(model.FD_Txt_Reference) ? model.FD_Txt_Reference : "";
                        InParamts.FDID = model.GLookUp_FD_List;
                        InParamts.MasterTxnID = model.xMID;
                        InParamts.Status_Action = Status_Action;
                        InParamts.RecID = Guid.NewGuid().ToString();
                        EditParam.param_TDSDeducted2 = InParamts;
                    }

                    //FCRA Update Process               
                    if (model.FD_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.FD_SplVchrReferenceSelected.Split(',');
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

                    if (BASE._FD_Voucher_DBOps.UpdateCloseFD_Txn(EditParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = model.xMID + model.xID
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
                Param_Txn_CloseFD_DeleteVoucherFD DelParam = new Param_Txn_CloseFD_DeleteVoucherFD();
                //DELETE
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;
                    DelParam.MID_DeleteFDHistory = model.xMID;
                    if (BASE._FD_Voucher_DBOps.DeleteCloseFD_Txn(DelParam))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = model.xMID + model.xID
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
                return null;
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
        #region Frm_FD_Window
        [HttpPost]
        public ActionResult Frm_FD_Window(Model_FD_Window model)
        {
            ViewData["FDVoucher_BankListRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "List");
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_FD, "View")) && model.TempActionMethod == "_View")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Frm_Voucher_FD_popup','Not Allowed','No Rights');</script>");
            }       
            if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_View" || model.TempActionMethod == "_Delete")
            {
                DataTable d1;
                if (string.IsNullOrEmpty(model.xFdID))
                {
                    d1 = BASE._FD_Voucher_DBOps.GetFDRecordByTxnID(model.TxnID);
                }
                else
                {
                    d1 = BASE._FD_Voucher_DBOps.GetFDRecordByID(model.xFdID);
                }
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                Data_Binding_Win(ref model, d1);
            }
            return View("Frm_FD_Window", model);
        }
        public void Data_Binding_Win(ref Model_FD_Window model, DataTable d1)
        {
            model.xID_FD = d1.Rows[0]["REC_ID"].ToString();
            model.Look_BankList_FD = d1.Rows[0]["FD_BA_ID"].ToString();
            model.Txt_No_FD = d1.Rows[0]["FD_NO"].ToString();
            if (model.Txt_Amount_FD == null)
            {
                model.Txt_Amount_FD = Convert.ToDouble(d1.Rows[0]["FD_AMT"]);
            }
            model.txt_Rate_FD = Convert.ToDouble(d1.Rows[0]["FD_INT_RATE"]);
            model.Txt_Mat_Amount_FD = Convert.ToDouble(d1.Rows[0]["FD_MAT_AMT"]);
            model.Cmd_Type_FD = d1.Rows[0]["FD_INT_PAY_COND"].ToString();
            model.Txt_Remarks_FD = d1.Rows[0]["FD_OTHER_DETAIL"].ToString();
            if (!IsDate(model.Txt_Date_FD.ToString()))
            {
                model.Txt_Date_FD = Convert.ToDateTime(d1.Rows[0]["FD_DATE"]);
            }
            if (!IsDate(model.Txt_As_Date_FD.ToString()))
            {
                model.Txt_As_Date_FD = Convert.ToDateTime(d1.Rows[0]["FD_AS_DATE"]);
            }
            model.Txt_Mat_Date_FD = Convert.ToDateTime(d1.Rows[0]["FD_MAT_DATE"]);
        }
        [HttpPost]
        public ActionResult Frm_FD_Window_Save(Model_FD_Window model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            model.iAction_window = (Common.FDAction)Enum.Parse(typeof(Common.FDAction), model.Fdiaction);
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            switch (model.Fdiaction)
            {
                case "New_FD":
                    model._action = "New_FD";
                    break;
                case "Renew_FD":
                    model._action = "Renew_FD";
                    break;
                case "Close_FD":
                    model._action = "Close_FD";
                    break;
            }
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection || Tag == Common.Navigation_Mode._Edit)
            {
                if (string.IsNullOrEmpty(model.Look_BankList_FD))
                {
                    jsonParam.message = "Bank Name Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Look_BankList_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.Txt_No_FD))
                {
                    jsonParam.message = "Please Enter FD Account No...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_No_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!IsDate(model.Txt_Date_FD.ToString()))
                {
                    jsonParam.message = "Date Incorrect/Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Date_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Date_FD < Convert.ToDateTime(BASE._open_Year_Sdt) || model.Txt_Date_FD > Convert.ToDateTime(BASE._open_Year_Edt))
                {
                    jsonParam.message = "Date not as per Financial Year...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Date_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!IsDate(model.Txt_As_Date_FD.ToString()))
                {
                    jsonParam.message = "Date Incorrect/Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_As_Date_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_As_Date_FD > model.Txt_Date_FD)
                {
                    jsonParam.message = "As of Date must be Equal/Lower than to F.D. Date...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_As_Date_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Amount_FD <= 0)
                {
                    jsonParam.message = "FD Amount cannot be Zero/Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Amount_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.txt_Rate_FD <= 0 || model.txt_Rate_FD > 100)
                {
                    jsonParam.message = "FD Rate Incorrect...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "txt_Rate_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.Cmd_Type_FD))
                {
                    jsonParam.message = "Interest Payment Condition Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Cmd_Type_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Mat_Amount_FD == null || model.Txt_Mat_Amount_FD <= 0)
                {
                    jsonParam.message = "Maturity Amount cannot be Zero/Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Mat_Amount_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Mat_Amount_FD < model.Txt_Amount_FD)
                {
                    jsonParam.message = "Maturity Amount cannot be less than Fd Amount...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Mat_Amount_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!IsDate(model.Txt_Mat_Date_FD.ToString()))
                {
                    jsonParam.message = "Date Incorrect/Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Mat_Date_FD";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Mat_Date_FD <= model.Txt_Date_FD)
                {
                    jsonParam.message = "Maturity Date Must Be Greater Than FD Date...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Mat_Date";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (Tag == Common.Navigation_Mode._Edit)
            {
                int? MaxValue = 0;
                MaxValue = Convert.ToInt32(BASE._FD_Voucher_DBOps.GetStatusByID(model.xID_FD));
                if ((MaxValue == null))
                {
                    jsonParam.message = "Entry Not Found...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((MaxValue == (int)Common.Record_Status._Locked))
                {
                    jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br<br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            var Status_Action = ((int)Common.Record_Status._Completed).ToString();
            try
            {
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    // new
                    model.xID_FD = Guid.NewGuid().ToString();
                    string xRenewFrom = "NULL";
                    if (model.Fdiaction == "Renew_FD")
                    {
                        xRenewFrom = "'" + model.iRenewFrom + "'";
                    }
                    Parameter_InsertFD_VoucherFD InFD = new Parameter_InsertFD_VoucherFD();
                    InFD.BankAccID = model.Look_BankList_FD;
                    InFD.FDNo = model.Txt_No_FD;
                    if (IsDate(model.Txt_Date_FD.ToString()))
                    {
                        InFD.FDDate = Convert.ToDateTime(model.Txt_Date_FD).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDDate = model.Txt_Date_FD.ToString();
                    }
                    if (IsDate(model.Txt_As_Date_FD.ToString()))
                    {
                        InFD.FDAsDate = Convert.ToDateTime(model.Txt_As_Date_FD.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDAsDate = model.Txt_As_Date_FD.ToString();
                    }
                    InFD.FDAmount = Convert.ToDouble(model.Txt_Amount_FD);
                    InFD.FDIntRate = Convert.ToDouble(model.txt_Rate_FD);
                    InFD.PaymentCondition = model.Cmd_Type_FD;
                    if (IsDate(model.Txt_Mat_Date_FD.ToString()))
                    {
                        InFD.FDMaturityDate = Convert.ToDateTime(model.Txt_Mat_Date_FD.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDMaturityDate = model.Txt_Mat_Date_FD.ToString();
                    }
                    InFD.FDMaturityAmount = Convert.ToDouble(model.Txt_Mat_Amount_FD);
                    InFD.Remarks = model.Txt_Remarks_FD == null ? "" : model.Txt_Remarks_FD;
                    InFD.TxnID = model.TxnID;
                    InFD.RenewFrom_ID = xRenewFrom;
                    InFD.Status_Action = Status_Action;
                    InFD.RecID = model.xID_FD;

                    Parameter_InsertFDHistory_VoucherFD InFDHty = new Parameter_InsertFDHistory_VoucherFD();
                    InFDHty.FDID = model.xID_FD;
                    InFDHty.FDAction = model._action;
                    InFDHty.FDStatus = Common.FDStatus.New_FD.ToString();
                    InFDHty.TxnID = model.TxnID;
                    InFDHty.Status_Action = Status_Action;
                    InFDHty.RecID = Guid.NewGuid().ToString();
                    Parameter_InsertFDHistory_VoucherFD InRenFDHis = new Parameter_InsertFDHistory_VoucherFD();
                    if (model.iAction_window == Common.FDAction.Renew_FD)
                    {                       
                        InRenFDHis.FDID = model.iRenewFrom == null ? "" : model.iRenewFrom;
                        InRenFDHis.FDAction = Common.FDAction.Renew_FD.ToString();
                        InRenFDHis.FDStatus = model.Status;
                        InRenFDHis.TxnID = model.TxnID;
                        InRenFDHis.Status_Action = Status_Action;
                        InRenFDHis.RecID = Guid.NewGuid().ToString();
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xID_FD,
                        InFD,
                        InFDHty,
                        InRenFDHis
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    Parameter_UpdateFD_VoucherFD UpFD = new Parameter_UpdateFD_VoucherFD();
                    UpFD.BankAccID = model.Look_BankList_FD;
                    UpFD.FDNo = model.Txt_No_FD;
                    if (IsDate(model.Txt_Date_FD.ToString()))
                    {
                        UpFD.FDDate = Convert.ToDateTime(model.Txt_Date_FD.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDDate = model.Txt_Date_FD.ToString();
                    }
                    if (IsDate(model.Txt_As_Date_FD.ToString()))
                    {
                        UpFD.FDAsDate = Convert.ToDateTime(model.Txt_As_Date_FD.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDAsDate = model.Txt_As_Date_FD.ToString();
                    }
                    UpFD.FDAmount = Convert.ToDouble(model.Txt_Amount_FD);
                    UpFD.FDIntRate = Convert.ToDouble(model.txt_Rate_FD);
                    UpFD.PaymentCondition = model.Cmd_Type_FD;
                    if (IsDate(model.Txt_Mat_Date_FD.ToString()))
                    {
                        UpFD.FDMaturityDate = Convert.ToDateTime(model.Txt_Mat_Date_FD.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDMaturityDate = model.Txt_Mat_Date_FD.ToString();
                    }
                    UpFD.FDMaturityAmount = Convert.ToDouble(model.Txt_Mat_Amount_FD);
                    UpFD.Remarks = model.Txt_Remarks_FD == null ? "" : model.Txt_Remarks_FD;
                    UpFD.TxnID = model.TxnID;
                    UpFD.RenewFrom_ID = model.iRenewFrom == null ? "" : model.iRenewFrom;
                    Parameter_UpdateFDHistory_VoucherFD UpFDHis = new Parameter_UpdateFDHistory_VoucherFD();
                    UpFDHis.FDAction = model._action;
                    UpFDHis.FDStatus = Common.FDStatus.New_FD.ToString();
                    UpFDHis.TxnId = model.TxnID;
                    UpFDHis.FDID = model.xID_FD;
                    Parameter_UpdateFDHistory_VoucherFD UpRenFDHty = new Parameter_UpdateFDHistory_VoucherFD();
                    if (model.iAction_window == Common.FDAction.Renew_FD)
                    {                        
                        UpRenFDHty.FDAction = model._action;
                        UpRenFDHty.FDStatus = model.Status;
                        UpRenFDHty.TxnId = model.TxnID;
                        UpRenFDHty.FDID = model.iRenewFrom == null ? "" : model.iRenewFrom;
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xID_FD,
                        UpFD,
                        UpFDHis,
                        UpRenFDHty
                    }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    xid = model.xID_FD
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
        public ActionResult CHECKduplicateAccNo(string Txt_No, string Look_BankList, string xID, string iRenewFDNo)
        {
            int? MaxValue = 0;
            MaxValue = (int?)BASE._FD_Voucher_DBOps.GetAccountNoCount(xID, Txt_No, Look_BankList);
            if (MaxValue == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            if (MaxValue > 0 && !(Txt_No.ToUpper() == iRenewFDNo.ToUpper()))
            {
                return Json(new
                {
                    message = "Same Account No. already exists...!<br>Do you want to Continue...?",
                    result = "AccountNoSame"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  "LookUp"
        public List<FDVoucherItems> RefreshItemList()
        {
            return DatatableToModel.DataTabletoFDVoucher_GetItemList(BASE._FD_Voucher_DBOps.GetItemList()).OrderBy(x => x.ITEMNAME).ToList();
        } 
        public List<FDBankList> RefreshBankList()
        {
            DataTable BA_Table = BASE._FD_Voucher_DBOps.GetBankAccounts();     
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if ((Branch_IDs.Trim().Length > 0))
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if ((Branch_IDs.Trim().Length == 0))
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._FD_Voucher_DBOps.GetBranches(Branch_IDs);  
            // BUILD DATA
            List<FDBankList> BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new FDBankList
                             {
                                 BANK_NAME = (string)B["Name"],
                                 BI_SHORT_NAME = (string)B["BI_SHORT_NAME"],
                                 BANK_BRANCH = (string)B["Branch"],
                                 BANK_ACC_NO = (string)A["BA_ACCOUNT_NO"],
                                 BA_ID = (string)A["ID"],
                                 REC_EDIT_ON = (DateTime?)A["REC_EDIT_ON"]
                             }).ToList();        
            BuildData.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            return BuildData;
        }
        public ActionResult GetBankDataOnRefresh() 
        {
            return Content(JsonConvert.SerializeObject(RefreshBankList()), "application/json");
        }
        public List<FdListItems> RefreshFDList(string Item = null, string Tag = "_New")
        {
            DataTable Final_Data = null;
            Common.Navigation_Mode TagAction = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            if ((Item != "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" && Item != "f6e4da62-821f-4961-9f93-f5177fca2a77" && Item != "65730a27-e365-4195-853e-2f59225fe8f4") || TagAction == Common.Navigation_Mode._Edit || TagAction == Common.Navigation_Mode._Delete)
            {
                Final_Data = BASE._FD_Voucher_DBOps.GetFDs(true, null, true);
            }
            else
            {
                Final_Data = BASE._FD_Voucher_DBOps.GetFDs(false);
            }
           return DatatableToModel.DataTabletoFDVoucher_FDList(Final_Data);         
        }
        public ActionResult LookUp_GetPurposeList(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BASE._Donation_DBOps.GetPurposes(), loadOptions)), "application/json");
        }
        public List<FDBankList_window> RefreshFDBankList()
        {
            DataTable BA_Table = BASE._FD_Voucher_DBOps.GetFDBankAccounts();
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if ((Branch_IDs.Trim().Length > 0))
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if ((Branch_IDs.Trim().Length == 0))
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._FD_Voucher_DBOps.GetBranches(Branch_IDs);
            // BUILD DATA
            List<FDBankList_window> BuildData = (from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
                            on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                            select new FDBankList_window
                            {
                                Name = B.Field<string>("Name"),
                                Branch = B.Field<string>("Branch"),
                                BA_CUST_NO = A.Field<string>("BA_CUST_NO"),
                                ID = A.Field<string>("ID"),
                                REC_EDIT_ON = A.Field<DateTime?>("REC_EDIT_ON")
                            }).ToList();
            BuildData.Sort((x, y) => x.Name.CompareTo(y.Name));
            return BuildData;
        }
        public ActionResult LookUp_GetBankList_FD()
        {
            return Content(JsonConvert.SerializeObject(RefreshFDBankList()), "application/json");
        }      
        #endregion
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        [HttpPost]
        public ActionResult GetTDS(string FD_ID = null, string xMID = null)
        {
            var tds = BASE._FD_Voucher_DBOps.GetTDS(xMID, FD_ID);
            var tdsReversal = BASE._FD_Voucher_DBOps.GetTDSReversal(xMID, FD_ID);
            return Json(new
            {
                tds,
                tdsReversal
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetItemCountInSameMaster(string xMID = null)
        {
            var value = BASE._FD_Voucher_DBOps.GetItemCountInSameMaster(xMID, "d0219173-45ff-4284-ae0e-89ba0e8d76b4");
            if (value == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            if ((int)value > 0)
            {
                return Json(new
                {
                    result = true,
                    itemid = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = true,
                itemid = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public void SessionClear()
        {
            ClearBaseSession("_FDVoucher");
        }   
    }
}