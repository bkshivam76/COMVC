using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
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
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class ReceiptVoucherController : BaseController
    {        
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Gen_Rec(string Tag = "", string xID = "", string xMID = "", string Info_LastEditedOn = "", string iSpecific_ItemID = "", string GridToRefresh = "CashBookListGrid")
        {
            ReceiptVoucherModel model = new ReceiptVoucherModel();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = Tag;
            bool HasRight = true;
            if (model.Tag == Common.Navigation_Mode._New && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, "Add") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._New_From_Selection && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, "Add") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._Edit && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, "Update") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._Delete && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, "Delete") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._View && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, "View") == false)
            {
                HasRight = false;
            }
            if (HasRight == false)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            ViewBag.GridToRefresh = GridToRefresh;
            ViewData["RecVoucher_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["RecVoucher_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            model.xID = xID;
            model.xMID = xMID;
            model.iSpecific_ItemID = iSpecific_ItemID;
            if (!string.IsNullOrWhiteSpace(Info_LastEditedOn))
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
            }
            model.TitleX = "Receipt";
            model.ItemList_DD_Data_Rpt = Receipt_LookUp_GetItemList();
            model.DepBankList_DD_Data_Rpt = ReceiptDepositeLookUp_GetBankList();
            model.RefBankList_DD_Data_Rpt = ReceiptLookUp_GetRefBankList();
            model.PartyList_DD_Data_Rpt = LookUp_GetPartyList();
            model.PurposeList_Data_Rpt = LookUp_GetPurposeList();
            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_Rpt = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_Rpt = model.SpecialReferenceList_Data_Rpt.Count();

            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.Me_Text = "New ~ " + model.TitleX;
                model.Rpt_Txt_V_NO = "";
                model.Rpt_Cmd_Mode = "CASH";
                model.Rpt_RAD_Receipt = 0;
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "Edit ~ " + model.TitleX;
                model.xMID = BASE._Rect_DBOps.GetMasterID(model.xID).ToString();

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_Rpt = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                DataTable d1 = BASE._Rect_DBOps.GetMasterRecord(model.xMID);
                DataTable d2 = BASE._Rect_DBOps.GetPurposeRecord(model.xMID);
                DataTable d3 = BASE._Rect_DBOps.GetRecord(model.xMID);
                DataTable d4 = BASE._Rect_DBOps.GetSlipRecord(model.xMID);
                var d5 = new List<Return_GetSlipMasterRecord>();
                if (d4 != null)
                {
                    if (d4.Rows.Count > 0)
                    {
                        d5 = BASE._Voucher_DBOps.GetSlipMAsterRecord(d4.Rows[0]["TR_SLIP_ID"].ToString());
                    }
                }
                if (d1 == null || d2 == null || d4 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('"+Messages.SomeError+"','Error!!');</script>");
                }
                if (d3.Rows.Count == 0)
                {
                    d3 = BASE._Rect_DBOps.GetJournalRecord(model.xMID);
                }
                if (BASE.AllowMultiuser())
                {
                    string viewstr = "";
                    if (model.Tag == Common.Navigation_Mode._View)
                    {
                        viewstr = "view";
                    }                
                    if (model.Info_LastEditedOn != Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]))
                    {
                        string message = Messages.RecordChanged("Current Receipt", viewstr);
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                    }
                }
                Data_Binding(ref model, d1, d2, d3, d4, d5);
            }
            if (model.Tag == Common.Navigation_Mode._Delete) 
            {
                model.Me_Text = "Delete ~ " + model.TitleX;
            }
            if (model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "View ~ " + model.TitleX;
            }
            if (model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.iSpecific_ItemID = iSpecific_ItemID;
                model.Rpt_GLookUp_ItemList = model.iSpecific_ItemID;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gen_Rec_Save(ReceiptVoucherModel model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.xMID = string.IsNullOrEmpty(model.xMID) ? "xMID" : model.xMID;
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrWhiteSpace(model.Rpt_GLookUp_BankList))
                    {
                        // Closed Bank Acc Check #g31
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.Rpt_GLookUp_BankList, "");
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
                        if (AccNo.ToString().Length > 0)
                        {
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted...!<br><br>In this entry, Used Bank A / c No.: " + AccNo + " was closed...!!!";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable receipt_DbOps = BASE._Rect_DBOps.GetMasterRecord(model.xMID);
                        if (receipt_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (receipt_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Receipt");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(receipt_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Receipt");
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
                        MaxValue = BASE._Rect_DBOps.GetStatus(model.xID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br><br>Note:<br><br>----------<br><br> Drop your Request to Madhuban for Unlock this Entry<br> If You really want to do some action";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string LiabId = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(model.xMID);
                        if (LiabId == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (LiabId.Length > 0)
                        {
                           var UseCount = BASE._LiabilityDBOps.GetTransactionCount(LiabId);
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Liability created by Current Receipt Entry is used in some other entry. . . !" +
                                    "<br>" + "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = LiabId;
                            param.Excluded_Rec_M_ID = model.xMID;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count;
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Liability created by current Receipt entry is used in some other entry. . . !" +
                                    "<br>" + "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (BASE._open_Year_ID >= 2627)
                    {
                        if (model.Rpt_Cmd_Mode == "CASH TO BANK")
                        {
                            jsonParam.message = "Income Tax के AIS में दिखाया गया “Cash Deposit in Bank” का अमाउंट हमारी Books of Accounts में दिखाए गए “Cash Deposit in Bank” से match होना अनिवार्य है।</br>" +
                                                "इसी कारण अब “Cash to Bank” mode उपलब्ध नहीं है। क्यूँकि यह कैश एवं बैंक दोनों का mix mode है।</br>" +
                                                "➡️ अतः:</br>" +
                                                "Receipt की entry Cash mode में करें।</br>" +
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
                    if (string.IsNullOrEmpty(model.Rpt_GLookUp_ItemList))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_GLookUp_ItemList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Rpt_Txt_V_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_V_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Rpt_Txt_V_Date.ToString()) == true)
                    {
                        if (model.Rpt_Txt_V_Date < Convert.ToDateTime(BASE._open_Year_Sdt) || model.Rpt_Txt_V_Date > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_V_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.iParty_Req.ToString().Trim().ToUpper() == "YES")
                    {
                        if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_PartyList1))
                        {
                            jsonParam.message = "Party Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_GLookUp_PartyList1";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_PartyList1))
                    {
                        model.Rpt_GLookUp_PartyList1 = "";
                    }
                    if (!string.IsNullOrWhiteSpace(model.iLink_ID))
                    {
                        if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_Adjustment))
                        {    
                            jsonParam.message = "Adjustment Reference Not  Selected...!";                           
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_GLookUp_Adjustment";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(model.Rpt_GLookUp_Adjustment))
                    {
                        model.Rpt_GLookUp_Adjustment = "";
                    }
                    if (model.Rpt_Txt_Diff < 0)
                    {
                        jsonParam.message = "Amount cannot be greater than " + model.Rpt_Txt_Out_Standing + "...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Rpt_RAD_Receipt != 2)
                    {
                        if (model.Rpt_Txt_Amount==null)
                        {
                            jsonParam.message = "Amount cannot be Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Rpt_Txt_Amount <= 0)
                        {
                            jsonParam.message = "Amount cannot be Zero/Negative...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((!string.IsNullOrWhiteSpace(model.iLink_ID)) && model.Rpt_RAD_Receipt == 0)
                    {
                        if (model.Rpt_Txt_Amount >= model.Rpt_Txt_Out_Standing)
                        {
                            jsonParam.message = "Amount cannot be greater than or equal to Out-Standing Amount...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                        {
                            if (model.Rpt_Txt_Amount >= model.REF_OUTSTAND_NEXT_YEAR)
                            {
                                jsonParam.message = "Amount cannot be  greater than or equal  to Out-Standing Amount at end of Next year( " + model.REF_OUTSTAND_NEXT_YEAR + " )...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Rpt_Txt_Amount";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if ((!string.IsNullOrWhiteSpace(model.iLink_ID)) && model.Rpt_RAD_Receipt == 1)
                    {
                        if (model.Rpt_Txt_Amount > model.Rpt_Txt_Out_Standing)
                        {
                            jsonParam.message = "Amount cannot be greater than Out-Standing Amount...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                        {
                            if (model.Rpt_Txt_Amount > model.REF_OUTSTAND_NEXT_YEAR)
                            {
                                jsonParam.message = "Amount cannot be  greater than Out-Standing Amount at end of Next year (" + model.REF_OUTSTAND_NEXT_YEAR + ")...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Rpt_Txt_Amount";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_BankList) && model.Rpt_Cmd_Mode.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_GLookUp_BankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_BankList))
                    {
                        model.Rpt_GLookUp_BankList = "";
                    }
                    if (string.IsNullOrEmpty(model.Rpt_GLookUp_RefBankList) && (model.Rpt_Cmd_Mode.ToUpper() != "BANK ACCOUNT") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH TO BANK") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH"))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_GLookUp_RefBankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Rpt_GLookUp_RefBankList))
                    {
                        model.Rpt_GLookUp_RefBankList = "";
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_Txt_Ref_Branch) && (model.Rpt_Cmd_Mode.ToUpper() != "BANK ACCOUNT") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH TO BANK") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH"))
                    {
                        jsonParam.message = "Bank Branch Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_Ref_Branch";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_Txt_Ref_No) && (model.Rpt_Cmd_Mode.ToUpper() != "BANK ACCOUNT") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH"))
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_Ref_No";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((IsDate(model.Rpt_Txt_Ref_Date.ToString()) == false) && (model.Rpt_Cmd_Mode.ToUpper() != "BANK ACCOUNT") && (model.Rpt_Cmd_Mode.ToUpper() != "CASH"))
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_Ref_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Rpt_GLookUp_PurList))
                    {
                        jsonParam.message = "Purpose Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_GLookUp_PurList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Rpt_Txt_Slip_No > 0)
                    {
                        DataTable BankAccIDTable = BASE._DepositSlipsDBOps.GetList((int)model.Rpt_Txt_Slip_No, model.Rpt_GLookUp_BankList);
                        if (BankAccIDTable.Rows.Count > 0)
                        {
                            // --Slip Exists
                            if (!Convert.IsDBNull(BankAccIDTable.Rows[0]["BA_REC_ID"]))
                            {
                                if (BankAccIDTable.Rows[0]["BA_REC_ID"].ToString() != model.Rpt_GLookUp_BankList)
                                {
                                    jsonParam.message = "Selected slip has transactions of other bank...!";
                                    jsonParam.title = "Incomplete Information..";
                                    jsonParam.result = false;
                                    jsonParam.focusid = "Rpt_Txt_Slip_No";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (!Convert.IsDBNull(BankAccIDTable.Rows[0]["Date of Print"]))
                            {
                                jsonParam.message = "Selected slip is already printed...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Rpt_Txt_Slip_No";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else if (model.Rpt_Cmd_Mode != "CASH")
                    {
                        jsonParam.message = "Slip no. cannot be negative or Zero...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Rpt_Txt_Slip_No";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()) == true)
                    {
                        if (model.Rpt_Txt_Ref_CDate < model.Rpt_Txt_Ref_Date)
                        {
                            jsonParam.message = "Clearing Date Cannot be less than  Reference  Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Rpt_Txt_Ref_CDate";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                // -------------------------- // Start Dependencies // ----------------------------
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        DateTime oldEditOn;
                        if (!string.IsNullOrEmpty(model.Rpt_GLookUp_BankList))
                        {
                            // Bank Account dependency check #Ref G31
                            DataTable d1 = BASE._Rect_DBOps.GetBankAccounts(false, model.Rpt_GLookUp_BankList);
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
                            oldEditOn = Convert.ToDateTime(model.Bank_REC_EDIT_ON);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Referred Bank Account");
                                jsonParam.title = "Referred Record Already Deleted..";
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
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"].ToString());
                                if (oldEditOn != NewEditOn)
                                {
                                    // A/E,E/E
                                    jsonParam.message = Messages.DependencyChanged("Referred Bank Account");
                                    jsonParam.title = "Referred Record Already Changed..";
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
                        // Address Book dependency check #Ref Z31
                        if (!string.IsNullOrEmpty(model.Rpt_GLookUp_PartyList1))
                        {
                            DataTable Address = BASE._Rect_DBOps.GetParties(model.Rpt_GLookUp_PartyList1);
                            if (Address == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            oldEditOn = Convert.ToDateTime(model.PartyList_REC_EDIT_ON);
                            if (Address.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Reffered Party Record");
                                jsonParam.title = "Referred Record Already Deleted..";
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
                                DateTime NewEditOn = Convert.ToDateTime(Address.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn.Date != NewEditOn.Date)
                                {
                                    // A/E,E/E
                                    jsonParam.message = Messages.DependencyChanged("Referred Party Record");
                                    jsonParam.title = "Referred Record Already Changed..";
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
                        // Advance Table...
                        Param_GetReceiptAdvances RecptParam = new Param_GetReceiptAdvances();
                        RecptParam.Advance_ItemID = model.iLink_ID;
                        RecptParam.Advance_PartyID = model.Rpt_GLookUp_PartyList1;
                        RecptParam.Next_YearID = BASE._next_Unaudited_YearID;
                        RecptParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        RecptParam.Advance_RecID = model.Rpt_GLookUp_Adjustment;
                        RecptParam.Tr_M_Id =model.xMID;
                        DataTable OS_TABLE = BASE._Rect_DBOps.GetAdvances(RecptParam);

                        if (OS_TABLE == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (OS_TABLE.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(OS_TABLE.Rows[0]["REF_OUTSTAND"]) < Convert.ToDecimal(model.Rpt_Txt_Amount))
                            {
                                jsonParam.message = Messages.DependencyChanged("Referred Advance's Closing Balance");
                                jsonParam.title = "Referred Record value Already Changed...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            // Bug #5612
                            if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                            {
                                if (Convert.ToDecimal(OS_TABLE.Rows[0]["REF_OUTSTAND_NEXT_YEAR"]) < Convert.ToDecimal(model.Rpt_Txt_Amount))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Referred Advance's Closing Balance in next year");
                                    jsonParam.title = "Referred Record value Already Changed...";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DateTime CreationDate_Adv = Convert.ToDateTime(Convert.IsDBNull(OS_TABLE.Rows[0]["REF_CREATION_DATE"]) ? BASE._open_Year_Sdt : OS_TABLE.Rows[0]["REF_CREATION_DATE"]);
                            if (model.Rpt_Txt_V_Date < CreationDate_Adv)
                            {
                                jsonParam.message = "Current Reference Voucher Date cannot be less than Advance Creation Voucher dated " + CreationDate_Adv.ToLongDateString() + "...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Rpt_Txt_V_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        Param_GetReceiptDeposits RecDeposits = new Param_GetReceiptDeposits();
                        RecDeposits.Dep_ItemID = model.iLink_ID;
                        RecDeposits.Dep_PartyID = model.Rpt_GLookUp_PartyList1;
                        RecDeposits.Dep_RecID = model.Rpt_GLookUp_Adjustment;
                        RecDeposits.Next_YearID = BASE._next_Unaudited_YearID;
                        RecDeposits.Prev_YearId = BASE._prev_Unaudited_YearID;
                        RecDeposits.TR_M_ID = model.xMID;
                        DataTable OS_TABLE1 = BASE._Rect_DBOps.GetPendingDeposits(RecDeposits);
                        if (OS_TABLE1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (OS_TABLE1.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(OS_TABLE1.Rows[0]["REF_OUTSTAND"]) < (Convert.ToDecimal(model.Rpt_Txt_Amount) + Convert.ToDecimal(model.Rpt_Txt_Diff)))
                            {
                                jsonParam.message = Messages.DependencyChanged("Referred Deposit's Closing Balance");
                                jsonParam.title = "Referred Record value Already Changed...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // Bug 5612
                            if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                            {
                                if (Convert.ToDecimal(OS_TABLE1.Rows[0]["REF_OUTSTAND_NEXT_YEAR"]) < (Convert.ToDecimal(model.Rpt_Txt_Amount) + Convert.ToDecimal(model.Rpt_Txt_Diff)))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Referred Deposit's Closing Balance in Next Year");
                                    jsonParam.title = "Referred Record value Already Changed...";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DateTime CreationDate_dep = Convert.ToDateTime(Convert.IsDBNull(OS_TABLE1.Rows[0]["REF_CREATION_DATE"]) ? BASE._open_Year_Sdt : OS_TABLE1.Rows[0]["REF_CREATION_DATE"]);
                            if (model.Rpt_Txt_V_Date < CreationDate_dep)
                            {
                                jsonParam.message = "Current Reference Voucher Date cannot be less than Deposit Creation Voucher dated " + CreationDate_dep.ToLongDateString() + "...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Rpt_Txt_V_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if (model.Rpt_Txt_V_Date >= CreationDate_dep && model.Rpt_RAD_Receipt == 1)
                            {
                                //  Voucher Date is not Less than Any Ref Voucher Date for final payment 
                                Param_GetAssetMaxTxnDate inparam = new Param_GetAssetMaxTxnDate();
                                inparam.Creation_Date = CreationDate_dep;
                                inparam.Asset_RecID = model.Rpt_GLookUp_Adjustment;
                                inparam.YearID = BASE._open_Year_ID;
                                inparam.Tr_M_ID = model.xMID;
                                DateTime MxDate = Convert.ToDateTime(BASE._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam));
                                if (MxDate == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.title = "Error..";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                if (model.Rpt_Txt_V_Date < MxDate)
                                {
                                    jsonParam.message = "Final Adjustment Voucher Date cannot be less than previous transaction on same deposit dated " + MxDate.ToLongDateString() + "...!";
                                    jsonParam.title = "Incomplete Information..";
                                    jsonParam.result = false;
                                    jsonParam.focusid = "Rpt_Txt_V_Date";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        if (OS_TABLE.Rows.Count == 0 && OS_TABLE1.Rows.Count == 0 && model.IsAdjustmentEnable == true)
                        {
                            jsonParam.message = Messages.DependencyChanged("Referred Advance/Deposit Record");
                            jsonParam.title = "Referred Record value Already Changed..";
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
                if (model.Rpt_Cmd_Mode.ToUpper() == "CASH")
                {
                    model.Rpt_GLookUp_BankList = "";
                    model.Rpt_GLookUp_RefBankList = "";
                    model.Rpt_Txt_Ref_Branch = "";
                    model.Rpt_Txt_Ref_No = "";
                    model.Rpt_Txt_Ref_Date = null;
                    model.Rpt_Txt_Ref_CDate = null;
                }

                // +----JV LEDGER DETAIL----+
                string JV_TRANS_TYPE = "";
                string JV_Cr_Led_id = "";
                string JV_Dr_Led_id = "";
                if ((!string.IsNullOrWhiteSpace(model.iLink_ID)) && (!string.IsNullOrWhiteSpace(model.iOffSet_ID)))
                {
                    // Dim JV_SQL_1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & iOffSet_ID & "'   "
                    DataTable JV_DT = (DataTable)BASE._Rect_DBOps.GetItemsListByID(model.iOffSet_ID);
                    if (JV_DT.Rows.Count > 0)
                    {
                        JV_TRANS_TYPE = JV_DT.Rows[0]["Item_Trans_Type"].ToString();
                        if ((JV_DT.Rows[0]["Item_Trans_Type"].ToString() == "DEBIT"))
                        {
                            JV_Dr_Led_id = JV_DT.Rows[0]["ITEM_LED_ID"].ToString();
                        }
                        else
                        {
                            JV_Cr_Led_id = JV_DT.Rows[0]["ITEM_LED_ID"].ToString();
                        }
                    }
                    else
                    {
                        jsonParam.message = "Adjustment Item Not Found..!";
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                // +----------END-----------+
                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Dr_Led_ID = "";
                string Sub_Cr_Led_ID = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.iLed_ID;
                    if (model.Rpt_Cmd_Mode.ToUpper() == "CASH")
                    {
                        Cr_Led_id = "00080";
                    }
                    else
                    {
                        Cr_Led_id = "00079";
                        Sub_Cr_Led_ID = model.Rpt_GLookUp_BankList;
                    }
                }
                else
                {
                    Cr_Led_id = model.iLed_ID;
                    if (model.Rpt_Cmd_Mode.ToUpper() == "CASH")
                    {
                        Dr_Led_id = "00080";
                    }
                    else
                    {
                        Dr_Led_id = "00079";
                        Sub_Dr_Led_ID = model.Rpt_GLookUp_BankList; ;
                    }
                }

                Param_Txn_Insert_VoucherReceipt InNewParam = new Param_Txn_Insert_VoucherReceipt();
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    // new
                    model.xMID = Guid.NewGuid().ToString();
                    model.xID = Guid.NewGuid().ToString();
                    string STR2 = "";
                    double XCASH = 0.0;
                    double XBANK = 0.0;
                    if (model.Rpt_Cmd_Mode.ToUpper() == "CASH")
                    {
                        XCASH = Convert.ToDouble(model.Rpt_Txt_Amount);
                    }
                    else
                    {
                        XBANK = Convert.ToDouble(model.Rpt_Txt_Amount);
                    }
                    // MASTER ENTRY
                    Parameter_InsertMasterInfo_VoucherReceipt InMInfo = new Parameter_InsertMasterInfo_VoucherReceipt();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Receipt;
                    InMInfo.VNo = model.Rpt_Txt_V_NO ?? "";
                    if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.Rpt_Txt_V_Date.ToString();
                    }

                    // InMInfo.TDate = Me.Txt_V_Date.Text
                    InMInfo.PartyID = model.Rpt_GLookUp_PartyList1;
                    InMInfo.SubTotal = Convert.ToDouble(model.Rpt_Txt_Amount);
                    InMInfo.Cash = XCASH;
                    InMInfo.Bank = XBANK;
                    InMInfo.Advance = 0;
                    InMInfo.Liability = 0;
                    InMInfo.Credit = 0;
                    if (model.iProfile == "OTHER LIABILITIES")
                    {
                        InMInfo.Credit = Convert.ToDouble(model.Rpt_Txt_Amount);
                    }
                    InMInfo.TDS = 0;
                    InMInfo.ReceiptType = model.Rpt_RAD_Receipt.ToString();
                    InMInfo.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    InMInfo.RecID = model.xMID;
                    InNewParam.param_InsertMaster = InMInfo;
                    if (model.Rpt_Txt_Amount > 0)
                    {
                        Parameter_Insert_VoucherReceipt InParam = new Parameter_Insert_VoucherReceipt();
                        InParam.TransCode = (int)Common.Voucher_Screen_Code.Receipt;
                        InParam.VNo = model.Rpt_Txt_V_NO ?? "";
                        if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Rpt_Txt_V_Date.ToString();
                        }
                        // InParam.TDate = Me.Txt_V_Date.Text
                        InParam.ItemID = model.Rpt_GLookUp_ItemList;
                        InParam.Type = model.iTrans_Type;
                        InParam.Cr_Led_ID = Cr_Led_id;
                        InParam.Dr_Led_ID = Dr_Led_id;
                        InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                        InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                        InParam.Mode = model.Rpt_Cmd_Mode;
                        InParam.Ref_Bank = model.Rpt_GLookUp_RefBankList??"";
                        InParam.Ref_Branch = model.Rpt_Txt_Ref_Branch ?? "";
                        InParam.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                        if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                        {
                            InParam.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                        }

                        if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                        {
                            InParam.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                        }
                        InParam.Amount = Convert.ToDouble(model.Rpt_Txt_Amount);
                        InParam.PartyID = model.Rpt_GLookUp_PartyList1;
                        InParam.Narration = model.Rpt_Txt_Narration ?? "";
                        InParam.Remarks = model.Rpt_Txt_Remarks ?? "";
                        InParam.Reference = model.Rpt_Txt_Reference ?? "";
                        InParam.Tr_M_ID = model.xMID;
                        InParam.TxnSrNo = 1;
                        InParam.Cross_Ref_ID = model.Rpt_GLookUp_Adjustment ?? "";
                        InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                        InParam.RecID = model.xID;
                        InNewParam.param_Insert = InParam;
                    }
                    if (!string.IsNullOrEmpty(model.iLink_ID))
                    {
                        // JV ENTRY IF FINAL ADJUSTMENT
                        if ((model.Rpt_RAD_Receipt == 1 || model.Rpt_RAD_Receipt == 2) && model.Rpt_Txt_Diff > 0 && (!string.IsNullOrWhiteSpace(model.iOffSet_ID)))
                        {
                            // CREDIT ENTRY
                            Parameter_Insert_VoucherReceipt InParam1 = new Parameter_Insert_VoucherReceipt();
                            InParam1.TransCode = (int)Common.Voucher_Screen_Code.Receipt;
                            InParam1.VNo = model.Rpt_Txt_V_NO ?? "";
                            if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                            {
                                InParam1.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.TDate = model.Rpt_Txt_V_Date.ToString();
                            }
                            InParam1.ItemID = model.Rpt_GLookUp_ItemList;
                            InParam1.Type = model.iTrans_Type;
                            InParam1.Cr_Led_ID = Cr_Led_id;
                            InParam1.Dr_Led_ID = "";
                            InParam1.Sub_Cr_Led_ID = "";
                            InParam1.Sub_Dr_Led_ID = "";
                            InParam1.Mode = model.Rpt_Cmd_Mode;
                            InParam1.Ref_Bank = "";
                            InParam1.Ref_Branch = "";
                            InParam1.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                            if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                            {
                                InParam1.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                            }
                            if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                            {
                                InParam1.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                            }

                            // InParam1.RefDate = Me.Txt_Ref_Date.Text
                            // InParam1.RefCDate = Me.Txt_Ref_CDate.Text
                            InParam1.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InParam1.PartyID = model.Rpt_GLookUp_PartyList1;
                            InParam1.Narration = model.Rpt_Txt_Narration ?? "";
                            InParam1.Remarks = model.Rpt_Txt_Remarks ?? "";
                            InParam1.Reference = model.Rpt_Txt_Reference ?? "";
                            InParam1.Tr_M_ID = model.xMID;
                            InParam1.TxnSrNo = 1;
                            InParam1.Cross_Ref_ID = model.Rpt_GLookUp_Adjustment ?? "";
                            InParam1.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InParam1.RecID = Guid.NewGuid().ToString();

                            InNewParam.param_InsertCreditJV = InParam1;
                            // DEBIT ENTRY
                            Parameter_Insert_VoucherReceipt InParam = new Parameter_Insert_VoucherReceipt();
                            InParam.TransCode = (int)Common.Voucher_Screen_Code.Receipt;
                            InParam.VNo = model.Rpt_Txt_V_NO ?? "";
                            if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                            {
                                InParam.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.TDate = model.Rpt_Txt_V_Date.ToString();
                            }
                            // InParam.TDate = Me.Txt_V_Date.Text
                            InParam.ItemID = model.iOffSet_ID;
                            InParam.Type = JV_TRANS_TYPE;
                            InParam.Cr_Led_ID = "";
                            InParam.Dr_Led_ID = JV_Dr_Led_id;
                            InParam.Sub_Cr_Led_ID = "";
                            InParam.Sub_Dr_Led_ID = "";
                            InParam.Mode = model.Rpt_Cmd_Mode;
                            InParam.Ref_Bank = "";
                            InParam.Ref_Branch = "";
                            InParam.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                            if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                            {
                                InParam.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                            }

                            if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                            {
                                InParam.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                            }

                            // InParam.RefDate = Me.Txt_Ref_Date.Text
                            // InParam.RefCDate = Me.Txt_Ref_CDate.Text
                            InParam.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InParam.PartyID = model.Rpt_GLookUp_PartyList1;
                            InParam.Narration = model.Rpt_Txt_Narration ?? "";
                            InParam.Remarks = model.Rpt_Txt_Remarks ?? "";
                            InParam.Reference = model.Rpt_Txt_Reference ?? "";
                            InParam.Tr_M_ID = model.xMID;
                            InParam.TxnSrNo = 2;
                            InParam.Cross_Ref_ID = "";
                            InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InParam.RecID = Guid.NewGuid().ToString();
                            InNewParam.param_InsertDebitJV = InParam;
                            // purpose
                            Parameter_InsertPurpose_VoucherReceipt InPurpose_dr = new Parameter_InsertPurpose_VoucherReceipt();
                            InPurpose_dr.TxnID = model.xMID;
                            InPurpose_dr.PurposeID = model.Rpt_GLookUp_PurList;
                            InPurpose_dr.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InPurpose_dr.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InPurpose_dr.RecID = Guid.NewGuid().ToString();
                            InPurpose_dr.SrNo = 2;
                            InNewParam.param_InsertPurposedr = InPurpose_dr;
                            // PAYMENT - ADJUSTMENT
                            Parameter_InsertAandLPayment_VoucherReceipt InPmt = new Parameter_InsertAandLPayment_VoucherReceipt();
                            InPmt.TxnMID = model.xMID;
                            InPmt.Type = "ADJUSTMENT";
                            InPmt.SrNo = "2";
                            InPmt.RefID = model.Rpt_GLookUp_Adjustment ?? "";
                            InPmt.RefAmount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InPmt.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InPmt.RecID = Guid.NewGuid().ToString();

                            InNewParam.param_InPmtAdjusted = InPmt;
                        }

                        // PAYMENT - REFUND
                        Parameter_InsertAandLPayment_VoucherReceipt InPmt1 = new Parameter_InsertAandLPayment_VoucherReceipt();
                        InPmt1.TxnMID = model.xMID;
                        InPmt1.Type = "REFUND";
                        InPmt1.SrNo = "1";
                        InPmt1.RefID = model.Rpt_GLookUp_Adjustment ?? "";
                        InPmt1.RefAmount = Convert.ToDouble(model.Rpt_Txt_Amount);
                        InPmt1.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                        InPmt1.RecID = Guid.NewGuid().ToString();

                        InNewParam.param_InPmtRefund = InPmt1;
                    }

                    //Liability
                    //add check for liab profile item here 
                    if (model.iProfile == "OTHER LIABILITIES")
                    {
                        Parameter_InsertTRID_Liabilities[] InLiab = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities[1];
                        Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities _Liab = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities();
                        _Liab.Amount = Convert.ToDouble(model.Rpt_Txt_Amount);
                        _Liab.ItemID = model.Rpt_GLookUp_ItemList;
                        if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                        {
                            _Liab.LiabilityDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            _Liab.LiabilityDate = model.Rpt_Txt_V_Date.ToString();
                        }
                        _Liab.PartyID = model.Rpt_GLookUp_PartyList1;
                        _Liab.RecID = Guid.NewGuid().ToString();
                        _Liab.Remarks = model.Rpt_Txt_Remarks;
                        _Liab.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                        _Liab.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                        _Liab.TxnID = model.xMID;
                        InLiab[0] = _Liab;
                        InNewParam.InsertLiability = InLiab;
                    }
                    // purpose
                    Parameter_InsertPurpose_VoucherReceipt InPurpose = new Parameter_InsertPurpose_VoucherReceipt();
                    InPurpose.TxnID = model.xMID;
                    InPurpose.PurposeID = model.Rpt_GLookUp_PurList;
                    InPurpose.Amount = Convert.ToDouble(model.Rpt_Txt_Amount);
                    InPurpose.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    InPurpose.RecID = Guid.NewGuid().ToString();
                    InPurpose.SrNo = 1;
                    InNewParam.param_InsertPurpose = InPurpose;
                    if (model.Rpt_Txt_Slip_No > 0)
                    {
                        Parameter_InsertSlip_VoucherReceipt inSlip = new Parameter_InsertSlip_VoucherReceipt();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        inSlip.SlipNo = Convert.ToInt32(model.Rpt_Txt_Slip_No);
                        inSlip.TxnID = model.xMID;
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        InNewParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Insert Process
                    if (model.Rpt_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Rpt_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Rect_DBOps.InsertReceipt_Txn(InNewParam))
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
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK = model.xMID+model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Param_Txn_Update_VoucherReceipt EditParam = new Param_Txn_Update_VoucherReceipt();
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    // edit
                    string STR2 = "";
                    double XCASH = 0.0;
                    double XBANK = 0.0;
                    if (model.Rpt_Cmd_Mode.ToUpper() == "CASH")
                    {
                        XCASH = Convert.ToDouble(model.Rpt_Txt_Amount);
                    }
                    else
                    {
                        XBANK = Convert.ToDouble(model.Rpt_Txt_Amount);
                    }

                    // MASTER ENTRY
                    Parameter_UpdateMaster_VoucherReceipt UpMaster = new Parameter_UpdateMaster_VoucherReceipt();
                    UpMaster.VNo = model.Rpt_Txt_V_NO ?? "";
                    if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                    {
                        UpMaster.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.TDate = model.Rpt_Txt_V_Date.ToString();
                    }

                    // UpMaster.TDate = Me.Txt_V_Date.Text
                    UpMaster.PartyID = model.Rpt_GLookUp_PartyList1;
                    UpMaster.SubTotal = Convert.ToDouble(model.Rpt_Txt_Amount);
                    UpMaster.Cash = XCASH;
                    UpMaster.Bank = XBANK;
                    UpMaster.ReceiptType = model.Rpt_RAD_Receipt.ToString();
                    // UpMaster.Status_Action = Status_Action
                    UpMaster.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMaster;
                    EditParam.MID_DeletePurpose = model.xMID;
                    EditParam.MID_DeleteSlip = model.xMID;
                    EditParam.MID_Delete = model.xMID;
                    EditParam.MID_DeletePayment = model.xMID;
                    if (Convert.ToDouble(model.Rpt_Txt_Amount) > 0)
                    {
                        Parameter_Insert_VoucherReceipt InParam = new Parameter_Insert_VoucherReceipt();
                        InParam.TransCode = (int)Common.Voucher_Screen_Code.Receipt;
                        InParam.VNo = model.Rpt_Txt_V_NO ?? "";
                        if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Rpt_Txt_V_Date.ToString();
                        }

                        // InParam.TDate = Me.Txt_V_Date.Text
                        InParam.ItemID = model.Rpt_GLookUp_ItemList;
                        InParam.Type = model.iTrans_Type;
                        InParam.Cr_Led_ID = Cr_Led_id;
                        InParam.Dr_Led_ID = Dr_Led_id;
                        InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                        InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                        InParam.Mode = model.Rpt_Cmd_Mode;
                        InParam.Ref_Bank = model.Rpt_GLookUp_RefBankList ?? "";
                        InParam.Ref_Branch = model.Rpt_Txt_Ref_Branch ?? "";
                        InParam.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                        if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                        {
                            InParam.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                        }

                        if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                        {
                            InParam.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                        }

                        // InParam.RefDate = Me.Txt_Ref_Date.Text
                        // InParam.RefCDate = Me.Txt_Ref_CDate.Text
                        InParam.Amount = Convert.ToDouble(model.Rpt_Txt_Amount);
                        InParam.PartyID = model.Rpt_GLookUp_PartyList1;
                        InParam.Narration = model.Rpt_Txt_Narration ?? "";
                        InParam.Remarks = model.Rpt_Txt_Remarks ?? "";
                        InParam.Reference = model.Rpt_Txt_Reference ?? "";
                        InParam.Tr_M_ID = model.xMID;
                        InParam.TxnSrNo = 1;
                        InParam.Cross_Ref_ID = model.Rpt_GLookUp_Adjustment ?? "";
                        InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                        InParam.RecID = model.xID;

                        EditParam.param_Insert = InParam;
                    }

                    if (!string.IsNullOrWhiteSpace(model.iLink_ID))
                    {
                        // JV ENTRY IF FINAL ADJUSTMENT
                        if ((model.Rpt_RAD_Receipt == 1 || model.Rpt_RAD_Receipt == 2) && model.Rpt_Txt_Diff > 0 && (!string.IsNullOrWhiteSpace(model.iOffSet_ID)))
                        {
                            // CREDIT ENTRY
                            Parameter_Insert_VoucherReceipt InParams = new Parameter_Insert_VoucherReceipt();
                            InParams.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Receipt;
                            InParams.VNo = model.Rpt_Txt_V_NO ?? "";
                            if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                            {
                                InParams.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.TDate = model.Rpt_Txt_V_Date.ToString();
                            }
                            // InParams.TDate = Me.Txt_V_Date.Text
                            InParams.ItemID = model.Rpt_GLookUp_ItemList;
                            InParams.Type = model.iTrans_Type;
                            InParams.Cr_Led_ID = Cr_Led_id;
                            InParams.Dr_Led_ID = "";
                            InParams.Sub_Cr_Led_ID = "";
                            InParams.Sub_Dr_Led_ID = "";
                            InParams.Mode = model.Rpt_Cmd_Mode;
                            InParams.Ref_Bank = "";
                            InParams.Ref_Branch = "";
                            InParams.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                            if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                            {
                                InParams.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                            }

                            if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                            {
                                InParams.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                            }

                            // InParams.RefDate = Me.Txt_Ref_Date.Text
                            // InParams.RefCDate = Me.Txt_Ref_CDate.Text
                            InParams.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InParams.PartyID = model.Rpt_GLookUp_PartyList1;
                            InParams.Narration = model.Rpt_Txt_Narration ?? "";
                            InParams.Remarks = model.Rpt_Txt_Remarks ?? "";
                            InParams.Reference = model.Rpt_Txt_Reference ?? "";
                            InParams.Tr_M_ID = model.xMID;
                            InParams.TxnSrNo = 1;
                            InParams.Cross_Ref_ID = model.Rpt_GLookUp_Adjustment ?? "";
                            InParams.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InParams.RecID = Guid.NewGuid().ToString();
                            // If Not Base._Rect_DBOps.Insert(InParams) Then
                            //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            //     Exit Sub
                            // End If
                            EditParam.param_InsertCreditJV = InParams;
                            // DEBIT ENTRY
                            Parameter_Insert_VoucherReceipt InParam1 = new Parameter_Insert_VoucherReceipt();
                            InParam1.TransCode = (int)Common.Voucher_Screen_Code.Receipt;
                            InParam1.VNo = model.Rpt_Txt_V_NO;
                            if (IsDate(model.Rpt_Txt_V_Date.ToString()))
                            {
                                InParam1.TDate = Convert.ToDateTime(model.Rpt_Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.TDate = model.Rpt_Txt_V_Date.ToString();
                            }

                            // InParam1.TDate = Me.Txt_V_Date.Text
                            InParam1.ItemID = model.iOffSet_ID;
                            InParam1.Type = JV_TRANS_TYPE;
                            InParam1.Cr_Led_ID = "";
                            InParam1.Dr_Led_ID = JV_Dr_Led_id;
                            InParam1.Sub_Cr_Led_ID = "";
                            InParam1.Sub_Dr_Led_ID = "";
                            InParam1.Mode = model.Rpt_Cmd_Mode;
                            InParam1.Ref_Bank = "";
                            InParam1.Ref_Branch = "";
                            InParam1.Ref_No = model.Rpt_Txt_Ref_No ?? "";
                            if (IsDate(model.Rpt_Txt_Ref_Date.ToString()))
                            {
                                InParam1.RefDate = Convert.ToDateTime(model.Rpt_Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.RefDate = model.Rpt_Txt_Ref_Date.ToString();
                            }

                            if (IsDate(model.Rpt_Txt_Ref_CDate.ToString()))
                            {
                                InParam1.RefCDate = Convert.ToDateTime(model.Rpt_Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam1.RefCDate = model.Rpt_Txt_Ref_CDate.ToString();
                            }

                            // InParam1.RefDate = Me.Txt_Ref_Date.Text
                            // InParam1.RefCDate = Me.Txt_Ref_CDate.Text
                            InParam1.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InParam1.PartyID = model.Rpt_GLookUp_PartyList1;
                            InParam1.Narration = model.Rpt_Txt_Narration ?? "";
                            InParam1.Remarks = model.Rpt_Txt_Remarks ?? "";
                            InParam1.Reference = model.Rpt_Txt_Reference ?? "";
                            InParam1.Tr_M_ID = model.xMID;
                            InParam1.TxnSrNo = 2;
                            InParam1.Cross_Ref_ID = "";
                            InParam1.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InParam1.RecID = Guid.NewGuid().ToString();

                            EditParam.param_InsertDebitJV = InParam1;
                            // PAYMENT - ADJUSTMENT
                            Parameter_InsertAandLPayment_VoucherReceipt InPmt = new Parameter_InsertAandLPayment_VoucherReceipt();
                            InPmt.TxnMID = model.xMID;
                            InPmt.Type = "ADJUSTMENT";
                            InPmt.SrNo = "2";
                            InPmt.RefID = model.Rpt_GLookUp_Adjustment ?? "";
                            InPmt.RefAmount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InPmt.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InPmt.RecID = Guid.NewGuid().ToString();

                            EditParam.param_InPmtAdjusted = InPmt;
                            // purpose
                            Parameter_InsertPurpose_VoucherReceipt InPurpose_dr = new Parameter_InsertPurpose_VoucherReceipt();
                            InPurpose_dr.TxnID = model.xMID;
                            InPurpose_dr.PurposeID = model.Rpt_GLookUp_PurList;
                            InPurpose_dr.Amount = Convert.ToDouble(model.Rpt_Txt_Diff);
                            InPurpose_dr.Status_Action = ((int)Common.Record_Status._Completed).ToString(); ;
                            InPurpose_dr.RecID = Guid.NewGuid().ToString();
                            InPurpose_dr.SrNo = 2;

                            EditParam.param_InsertPurposedr = InPurpose_dr;
                        }

                        // PAYMENT - REFUND
                        Parameter_InsertAandLPayment_VoucherReceipt InPmt1 = new Parameter_InsertAandLPayment_VoucherReceipt();
                        InPmt1.TxnMID = model.xMID;
                        InPmt1.Type = "REFUND";
                        InPmt1.SrNo = "1";
                        InPmt1.RefID = model.Rpt_GLookUp_Adjustment ?? "";
                        InPmt1.RefAmount = Convert.ToDouble(model.Rpt_Txt_Amount);
                        InPmt1.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                        InPmt1.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InPmtRefund = InPmt1;
                    }
                    // purpose
                    Parameter_InsertPurpose_VoucherReceipt InPurpose = new Parameter_InsertPurpose_VoucherReceipt();
                    InPurpose.TxnID = model.xMID;
                    InPurpose.PurposeID = model.Rpt_GLookUp_PurList;
                    InPurpose.Amount = Convert.ToDouble(model.Rpt_Txt_Amount);
                    InPurpose.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    InPurpose.RecID = Guid.NewGuid().ToString();
                    InPurpose.SrNo = 1;

                    EditParam.param_InsertPurpose = InPurpose;
                    if (model.Rpt_Txt_Slip_No > 0)
                    {
                        Parameter_InsertSlip_VoucherReceipt inSlip = new Parameter_InsertSlip_VoucherReceipt();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        inSlip.SlipNo = Convert.ToInt32(model.Rpt_Txt_Slip_No);
                        inSlip.TxnID = model.xMID;
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        EditParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Update Process               
                    if (model.Rpt_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Rpt_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Rect_DBOps.UpdateReceipt_Txn(EditParam))
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
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK = model.xMID + model.xID
                    }, JsonRequestBehavior.AllowGet);
                }

                Param_Txn_Delete_VoucherReceipt DelParam = new Param_Txn_Delete_VoucherReceipt();
                if (model.Tag == Common.Navigation_Mode._Delete)
                {

                    DelParam.MID_DeleteItems = model.xMID;
                    DelParam.MID_DeletePayment = model.xMID;
                    DelParam.MID_DeletePurpose = model.xMID;
                    DelParam.MID_DeleteSlip = model.xMID;
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;
                    if (!BASE._Rect_DBOps.DeleteReceipt_Txn(DelParam))
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
                    jsonParam.title = model.TitleX;
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
        public void Data_Binding(ref ReceiptVoucherModel model, DataTable d1, DataTable d2, DataTable d3, DataTable d4, List<Return_GetSlipMasterRecord> d5)
        {

            DateTime xDate = Convert.ToDateTime(d3.Rows[0]["TR_DATE"]);
            model.Rpt_Txt_V_Date = xDate;

            model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            model.Rpt_Txt_V_NO = d3.Rows[0]["TR_VNO"].ToString();
            model.Rpt_Cmd_Mode = d3.Rows[0]["TR_MODE"].ToString();

            if (Convert.IsDBNull(d3.Rows[0]["TR_ITEM_ID"]) == false)
            {
                if (d3.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.Rpt_GLookUp_ItemList = d3.Rows[0]["TR_ITEM_ID"].ToString();
                    GetDataFromItemDropDown(ref model);
                }
            }
            if (Convert.IsDBNull(d3.Rows[0]["TR_AB_ID_1"]) == false)
            {
                if (d3.Rows[0]["TR_AB_ID_1"].ToString().Length > 0)
                {
                    model.Rpt_GLookUp_PartyList1 = d3.Rows[0]["TR_AB_ID_1"].ToString();
                }
            }
            model.Rpt_Txt_Amount = Convert.ToDouble(d1.Rows[0]["TR_SUB_AMT"]);
            if (Convert.IsDBNull(d3.Rows[0]["TR_TRF_CROSS_REF_ID"]) == false)
            {
                if (d3.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                {
                    model.Rpt_GLookUp_Adjustment = d3.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                }
            }
            string Bank_ID = "";
            if (model.iTrans_Type.ToUpper() == "DEBIT")
            {
                if ((!Convert.IsDBNull(d3.Rows[0]["TR_SUB_CR_LED_ID"])) && (Convert.IsDBNull(d3.Rows[0]["TR_CR_LED_ID"]) ? "" : d3.Rows[0]["TR_CR_LED_ID"].ToString()) == "00079")
                {
                    Bank_ID = d3.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                }
            }
            else
            {
                if ((!Convert.IsDBNull(d3.Rows[0]["TR_SUB_DR_LED_ID"])) && (Convert.IsDBNull(d3.Rows[0]["TR_DR_LED_ID"]) ? "" : d3.Rows[0]["TR_DR_LED_ID"].ToString()) == "00079")
                {
                    Bank_ID = d3.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                }
            }
            if (Bank_ID.Length > 0)
            {
                model.Rpt_GLookUp_BankList = Bank_ID;
            }
            if (Convert.IsDBNull(d3.Rows[0]["TR_REF_BANK_ID"]) == false)
            {
                if (d3.Rows[0]["TR_REF_BANK_ID"].ToString().Length > 0)
                {
                    model.Rpt_GLookUp_RefBankList = d3.Rows[0]["TR_REF_BANK_ID"].ToString();
                }
            }
            model.Rpt_Txt_Ref_Branch = d3.Rows[0]["TR_REF_BRANCH"].ToString().HandleEscapeCharacters();
            model.Rpt_Txt_Ref_No = d3.Rows[0]["TR_REF_NO"].ToString().HandleEscapeCharacters();
            if (d5.Count > 0)
            {
                model.Rpt_Txt_Slip_No = d5[0].SL_NO;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_REF_DATE"]))
            {
                xDate = Convert.ToDateTime(d3.Rows[0]["TR_REF_DATE"]);
                model.Rpt_Txt_Ref_Date = xDate;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_REF_CDATE"]))
            {
                xDate = Convert.ToDateTime(d3.Rows[0]["TR_REF_CDATE"]);
                model.Rpt_Txt_Ref_CDate = xDate;
            }

            if (!Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.Rpt_GLookUp_PurList = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }

            model.Rpt_Txt_Narration = d3.Rows[0]["TR_NARRATION"].ToString();
            model.Rpt_Txt_Remarks = d3.Rows[0]["TR_REMARKS"].ToString();
            model.Rpt_Txt_Reference = d3.Rows[0]["TR_REFERENCE"].ToString();
            model.Rpt_RAD_Receipt = Convert.ToInt32(d1.Rows[0]["TR_RECEIPT_TYPE"]);
            bool isDeposit = model.iProfile.Equals("OTHER DEPOSITS");
            model.PaymentList_DD_Data_Rpt = Party_Outstanding_DepositsAndOutstanding_Advances(model.iLink_ID, model.xMID, model.Rpt_GLookUp_PartyList1, isDeposit);
        }
        #region LookUp
        public List<VoucherTypeItems> Receipt_LookUp_GetItemList()
        {
            DataTable d1 = BASE._Rect_DBOps.GetLedgerItems(BASE.Is_HQ_Centre);
            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            return DatatableToModel.DataTabletoVoucherReceiptLookUp_GetItemList(dview.ToTable());
        }
        public List<ReceiptPartyList> LookUp_GetPartyList()
        {
            DataTable d1 = BASE._Rect_DBOps.GetParties();
            DataView dview = new DataView(d1);
            dview.Sort = "C_NAME";
            return DatatableToModel.DataTabletoVoucherReceiptLookUp_GetPartyList(dview.ToTable());
        }
        public ActionResult Refresh_LookUp_GetPartyList()
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetPartyList()), "application/json");
        }
        public List<DbOperations.Voucher_Receipt.Return_RefBank> ReceiptLookUp_GetRefBankList()
        {            
            return BASE._Rect_DBOps.GetBanks();
        }
        public List<ReceiptPurposeList> LookUp_GetPurposeList()
        {
            DataTable d1 = BASE._Rect_DBOps.GetPurposes();
            DataView dview = new DataView(d1);
            return DatatableToModel.DataTabletoVoucherReceiptLookUp_GetPurposeList(dview.ToTable());            
        }
        public List<BankList> ReceiptDepositeLookUp_GetBankList()
        {
            DataTable BA_Table = BASE._Rect_DBOps.GetBankAccounts();
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("'"
                            + (xRow["BA_BRANCH_ID"].ToString() + "',")));
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            DataTable BB_Table = BASE._Rect_DBOps.GetBranchDetails(Branch_IDs);
            var BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new BankList
                             {
                                 BANK_NAME = B["Name"].ToString(),
                                 BI_SHORT_NAME = B["BI_SHORT_NAME"].ToString(),
                                 BANK_BRANCH = B["Branch"].ToString(),
                                 BANK_ACC_NO = A["BA_ACCOUNT_NO"].ToString(),
                                 BANK_ID=B["BANK_ID"].ToString(),
                                 BA_ID = A["ID"].ToString(),
                                 REC_EDIT_ON = Convert.ToDateTime(A["REC_EDIT_ON"])
                             });
            return BuildData.ToList();
        }
        public ActionResult Refresh_Party_Outstanding_DepositsAndOutstanding_Advances(string ItemID="", string TR_M_ID="", string PartyID="", bool IsDeposit=false)
        {                
            return Content(JsonConvert.SerializeObject(Party_Outstanding_DepositsAndOutstanding_Advances(ItemID, TR_M_ID, PartyID, IsDeposit)), "application/json");
        }
        public List<ReceiptAdjustmentList> Party_Outstanding_DepositsAndOutstanding_Advances(string ItemID="", string TR_M_ID="", string PartyID="", bool IsDeposit=false)
        {    
            if (IsDeposit == true)
            {
                return GetPendingDeposits(ItemID, PartyID, TR_M_ID);
            }
            else 
            {
                return GetAdvances(ItemID, PartyID, TR_M_ID);
            }
        }

        #endregion
        #region Misc
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        [HttpPost]
        public List<ReceiptAdjustmentList> GetPendingDeposits(string Dep_ItemID, string Dep_PartyID, string TR_M_ID)
        {
            Param_GetReceiptDeposits RecDeposits = new Param_GetReceiptDeposits();
            RecDeposits.Dep_ItemID = Dep_ItemID;
            RecDeposits.Dep_PartyID = Dep_PartyID;
            RecDeposits.Next_YearID = BASE._next_Unaudited_YearID;
            RecDeposits.Prev_YearId = BASE._prev_Unaudited_YearID;
            RecDeposits.TR_M_ID = TR_M_ID;
            DataTable OS_TABLE = BASE._Rect_DBOps.GetPendingDeposits(RecDeposits);
            if (OS_TABLE == null)
            {
                return null;
            }
            else
            {
                DataView dview = new DataView(OS_TABLE);
                if (OS_TABLE.Rows.Count > 0)
                {
                    OS_TABLE.DefaultView.Sort = "REF_DATE";
                }
                return DatatableToModel.DataTabletoVoucherReceiptGLookUp_Adjustment_List(OS_TABLE);
            }
        }
        [HttpPost]
        public List<ReceiptAdjustmentList> GetAdvances(string Advance_ItemID, string Advance_PartyID, string TR_M_ID)
        {
            Param_GetReceiptAdvances RecDeposits = new Param_GetReceiptAdvances();
            RecDeposits.Advance_ItemID = Advance_ItemID;
            RecDeposits.Advance_PartyID = Advance_PartyID;
            RecDeposits.Next_YearID = BASE._next_Unaudited_YearID;
            RecDeposits.Prev_YearId = BASE._prev_Unaudited_YearID;
            RecDeposits.Tr_M_Id = TR_M_ID;
            DataTable OS_TABLE = BASE._Rect_DBOps.GetAdvances(RecDeposits);

            DataView dview = new DataView(OS_TABLE);
            if (OS_TABLE.Rows.Count > 0)
            {
                OS_TABLE.DefaultView.Sort = "REF_DATE";
            }
            return DatatableToModel.DataTabletoVoucherReceiptGLookUp_Adjustment_List(OS_TABLE);// redmine bug #133028 fix          
        }
        [HttpPost]
        public ActionResult CheckSlipNo(int SlipNo, string BankId, string xMID, int SlipCount)
        {
            int count = (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(SlipNo, BankId, ClientScreen.Accounts_Voucher_Receipt, xMID);
            if (SlipCount < count)
            {
                return Json(new
                {
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetSlipTxnCount(int SlipNo, string BankId, string xMID)
        {
            int count = (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(SlipNo, BankId, ClientScreen.Accounts_Voucher_Receipt, xMID);
            return Json(new
            {
                count
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMaxOpenSlipNo(string BankId)
        {
            int count = (int)BASE._DepositSlipsDBOps.GetMaxOpenSlipNo(BankId, ClientScreen.Accounts_Voucher_Receipt);
            return Json(new
            {
                count
            }, JsonRequestBehavior.AllowGet);
        }
        public void GetDataFromItemDropDown(ref ReceiptVoucherModel model)
        {
            if (model.ItemList_DD_Data_Rpt != null && model.ItemList_DD_Data_Rpt.Count > 0)
            {
                var itemid = model.Rpt_GLookUp_ItemList;
                var itemdata = model.ItemList_DD_Data_Rpt.Where(x => x.ITEMID == itemid).First();
                model.Rpt_BE_Item_Head = itemdata.LED_NAME;
                model.iVoucher_Type = itemdata.ITEM_VOUCHER_TYPE;
                model.iLed_ID = itemdata.ITEM_LED_ID;
                model.iTrans_Type = itemdata.ITEM_TRANS_TYPE;
                model.iLink_ID = itemdata.ITEM_LINK_REC_ID;
                model.iProfile = itemdata.ITEM_PROFILE;
            }
        }
        public void SessionClear()
        {
            ClearBaseSession("_Receipt");
        }
        #endregion
    }
}