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
using static Common_Lib.DbOperations.Voucher_CashBank;

namespace ConnectOneMVC.Areas.Account.Controllers
{

    [CheckLogin]
    public class CashDepositAndWithdrawnVoucherController : BaseController
    {    
        #region Frm_Voucher_Win_Cash

        [HttpGet]
        public ActionResult Frm_Voucher_Win_Cash(string Tag, string iSpecific_ItemID = "", string xID = null, bool Chk_Incompleted = false, string Info_LastEditedOn = null,string GridToRefresh= "CashBookListGrid")
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_CashBank, "Add")) && (Tag == "_New" || Tag == "_New_From_Selection"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_CashBank, "Update")) && (Tag == "_Edit"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_CashBank, "View")) && (Tag == "_View"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_CashBank, "Delete")) && (Tag == "_Delete"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            Voucher_Win_Cash model = new Voucher_Win_Cash();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Tag;
            ViewBag.GridToRefresh = GridToRefresh;
            model.ItemListData = LookUp_GetItemList();
            if (model.ItemListData.Count == 1) 
            {
                model.GLookUp_ItemList_CDW = model.ItemListData[0].ITEMID;
            }
            model.BankListData = LookUp_GetBankList();
            if (model.BankListData.Count < 1) 
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup', 'Voucher Entry Not Allowed<br><br>Note: Cash Deposited / Withdrawn requires minimum One Bank Account...!','Error!!');</script>");
            }
            if (model.BankListData.Count == 1) 
            {
                model.GLookUp_BankList_CDW = model.BankListData[0].BA_ID;
            }
                model.PurposeListData = LookUp_GetPurposeList();
            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_CDW = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank,model.ActionMethod);
            model.splVchrRefsCount = model.SpecialReferenceList_Data_CDW.Count();         
                      
            if (model.ActionMethod == Common.Navigation_Mode._New | model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
            {
                model.Txt_V_NO_CDW = null;                           
            }
            if (model.ActionMethod == Common.Navigation_Mode._Edit | model.ActionMethod == Common.Navigation_Mode._Delete | model.ActionMethod == Common.Navigation_Mode._View)
            {
                model.RecID = xID;                
                DataTable d1 = BASE._Cash_Bank_DBOps.GetRecord(xID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                DateTime? xDate = null;
                xDate = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
                model.Txt_V_Date_CDW = xDate;
                model.ActionMethod = Navigation_Mode_tag;
                model.TempActionMethod = Tag;
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.PurposeID_CDW = BASE._Cash_Bank_DBOps.GetPurposeID(xID);

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data= BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xID);
                if (SpecialReference_Data.Rows.Count > 0) 
                {
                    model.SpecialReference_Get_SelectedValue_CDW = SpecialReference_Data.AsEnumerable().Select(r=> r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._Edit | model.ActionMethod == Common.Navigation_Mode._Delete | model.ActionMethod == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            viewstr = "View";
                        }
                        if (Convert.ToDateTime(Info_LastEditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            string message = Messages.RecordChanged("Current Transaction", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','"+GridToRefresh+"');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_V_NO_CDW = d1.Rows[0]["TR_VNO"].ToString();

                if (!Convert.IsDBNull(d1.Rows[0]["TR_ITEM_ID"]))
                {
                    if (d1.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_ItemList_CDW = d1.Rows[0]["TR_ITEM_ID"].ToString();                        
                        model.LED_ID = model.ItemListData.Find(x => x.ITEMID == model.GLookUp_ItemList_CDW).ITEM_LED_ID;
                        model.iTrans_Type = model.ItemListData.Find(x => x.ITEMID == model.GLookUp_ItemList_CDW).ITEM_TRANS_TYPE;
                        model.VOUCHER_TYPE = model.ItemListData.Find(x => x.ITEMID == model.GLookUp_ItemList_CDW).ITEM_VOUCHER_TYPE;

                        if (model.VOUCHER_TYPE.ToUpper() == "CASH DEPOSITED")
                        {
                            model.iMode = "CASH";
                        }
                        else
                        {
                            model.iMode = "CHEQUE";
                        }
                    }
                }
                string Bank_ID = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    if (model.LED_ID == "00079")
                    {
                        Bank_ID = Convert.IsDBNull(d1.Rows[0]["TR_SUB_DR_LED_ID"]) ? "" : d1.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                    }
                }
                else
                {
                    if (model.LED_ID == "00079")
                    {
                        Bank_ID = Convert.IsDBNull(d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString()) ? "" : d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                    }
                }
                if (Bank_ID.Length > 0)
                {
                    model.GLookUp_BankList_CDW = Bank_ID;
                }
                model.Txt_Ref_No_CDW = d1.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_DATE"]))
                {
                    xDate = Convert.ToDateTime(d1.Rows[0]["TR_REF_DATE"]);
                    model.Txt_Ref_Date_CDW = xDate;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_CDATE"]))
                {
                    xDate = Convert.ToDateTime(d1.Rows[0]["TR_REF_CDATE"]);
                    model.Txt_Ref_CDate_CDW = xDate;
                }
                model.Txt_Amount_CDW = Convert.ToDouble(d1.Rows[0]["TR_AMOUNT"]);
                model.Txt_Narration_CDW = d1.Rows[0]["TR_NARRATION"].ToString();
                model.Txt_Remarks_CDW = d1.Rows[0]["TR_REMARKS"].ToString();
                model.Txt_Reference_CDW = d1.Rows[0]["TR_REFERENCE"].ToString();
                var _Denominations = BASE._Cash_Bank_DBOps.GetDenominations(model.RecID);
                model.Txt_2000_CDW = _Denominations.Count_2000;
                model.Txt_1000_CDW = _Denominations.Count_1000;
                model.Txt_500_CDW = _Denominations.Count_500;
                model.Txt_200_CDW = _Denominations.Count_200;
                model.Txt_100_CDW = _Denominations.Count_100;
                model.Txt_50_CDW = _Denominations.Count_50;
                model.Txt_20_CDW = _Denominations.Count_20;
                model.Txt_10_CDW = _Denominations.Count_10;
                model.Txt_5_CDW = _Denominations.Count_5;
                model.Txt_2_CDW = _Denominations.Count_2;
                model.Txt_1_CDW = _Denominations.Count_1;
            }

            if (model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
            {
                model.GLookUp_ItemList_CDW = iSpecific_ItemID;
            }            
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Cash(Voucher_Win_Cash model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);


                if (BASE.AllowMultiuser())
                {
                    if (model.GLookUp_BankList_CDW != null && model.GLookUp_BankList_CDW.ToString().Length > 0)
                    {
                        var AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList_CDW, "");

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
                            jsonParam.message = Messages.CashDepositWithDrawn.UsedBankAccClosed(AccNo.ToString());
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
                    if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete)
                    {
                        model.LastEditedOn = Convert.ToDateTime(model.LastEditedOn);
                        DataTable cash_bank_DbOps = BASE._Cash_Bank_DBOps.GetRecord(model.RecID);
                        if (cash_bank_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (cash_bank_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transaction");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.LastEditedOn != Convert.ToDateTime(cash_bank_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transaction");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // CHECKING LOCK STATUS
                        object MaxValue = 0;
                        MaxValue = BASE._Cash_Bank_DBOps.GetStatus(model.RecID);
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
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.GLookUp_ItemList_CDW))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList_CDW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (IsDate(model.Txt_V_Date_CDW.ToString()) == false)
                    {
                        jsonParam.message = "Voucher Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_V_Date_CDW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_CDW.ToString()) == true)
                    {
                        if (model.Txt_V_Date_CDW < BASE._open_Year_Sdt || model.Txt_V_Date_CDW > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Voucher Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_V_Date_CDW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (string.IsNullOrEmpty(model.GLookUp_BankList_CDW) || model.GLookUp_BankList_CDW.Trim().Length == 0)
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList_CDW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_Ref_Date_CDW.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_Ref_Date_CDW) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.Txt_Ref_Date_CDW) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Reference Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_Date_CDW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (IsDate(model.Txt_Ref_CDate_CDW.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_Ref_CDate_CDW) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.Txt_Ref_CDate_CDW) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Clearing Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate_CDW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_Ref_Date_CDW.ToString()) == true)
                        {
                            if (Convert.ToDateTime(model.Txt_Ref_CDate_CDW) < Convert.ToDateTime(model.Txt_Ref_Date_CDW))
                            {
                                jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Ref_CDate_CDW";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (model.Txt_Amount_CDW <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_CDW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                // -------------------// Start Dependencies //-------------------------
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._New
                                || model.ActionMethod == Common.Navigation_Mode._New_From_Selection
                                || model.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        if (!string.IsNullOrEmpty(model.GLookUp_BankList_CDW))
                        {
                            DateTime oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank_CDW);
                            DataTable d1 = BASE._Cash_Bank_DBOps.GetBankAccounts(model.GLookUp_BankList_CDW);
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
                                if (CommonFunctions.AreDatesEqual(oldEditOn,NewEditOn)==false)
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
                    }
                }
                string Status_Action = "";
                Status_Action = ((int)Common.Record_Status._Completed).ToString();


                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Dr_Led_ID = "";
                string Sub_Cr_Led_ID = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.LED_ID;
                    Cr_Led_id = "00080";
                    if (model.LED_ID == "00079")
                    {
                        Sub_Dr_Led_ID = model.GLookUp_BankList_CDW;
                    }
                }
                else
                {
                    Cr_Led_id = model.LED_ID;
                    Dr_Led_id = "00080";
                    if (model.LED_ID == "00079")
                    {
                        Sub_Cr_Led_ID = model.GLookUp_BankList_CDW;
                    }
                }

                // ................................................................................./
                if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
                {
                    // new
                    string xID = Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_Voucher_CashBank InParam = new Common_Lib.RealTimeService.Parameter_Insert_Voucher_CashBank();
                    InParam.TransCode = (int)Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn;
                    InParam.VNo = model.Txt_V_NO_CDW ?? "";
                    if (IsDate(model.Txt_V_Date_CDW.ToString()))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.Txt_V_Date_CDW.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_CDW;
                    InParam.Type = model.iTrans_Type;
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam.Mode = model.iMode;
                    InParam.Ref_No = model.Txt_Ref_No_CDW ?? "";
                    if (IsDate(model.Txt_Ref_Date_CDW.ToString()))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = model.Txt_Ref_Date_CDW.ToString();
                    }

                    if (IsDate(model.Txt_Ref_CDate_CDW.ToString()))
                    {
                        InParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_ChequeDate = model.Txt_Ref_CDate_CDW.ToString();
                    }
                    InParam.Amount = Convert.ToDouble(model.Txt_Amount_CDW);
                    InParam.Narration = model.Txt_Narration_CDW ?? "";
                    InParam.Remarks = model.Txt_Remarks_CDW ?? "";
                    InParam.Reference = model.Txt_Reference_CDW ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.PurposeID = model.PurposeID_CDW;
                    InParam.RecID = xID;

                    //FCRA Insert Process
          
                    if(model.cdw_SplVchrReferenceSelected != null) 
                    { 
                        var SplVchrRefsSplit = model.cdw_SplVchrReferenceSelected.Split(',');
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
                            InParam.InsertSplVchrRefs = InsertSplVchrRefs;
                        }                       
                    }
                    Parameter_CashBank_Denomination InDenomination = new Parameter_CashBank_Denomination();
                    if (model.Txt_2000_CDW > 0) { InDenomination.Count_2000 = model.Txt_2000_CDW; }
                    if (model.Txt_1000_CDW > 0) { InDenomination.Count_1000 = model.Txt_1000_CDW; }
                    if (model.Txt_500_CDW > 0) { InDenomination.Count_500 = model.Txt_500_CDW; }
                    if (model.Txt_200_CDW > 0) { InDenomination.Count_200 = model.Txt_200_CDW; }
                    if (model.Txt_100_CDW > 0) { InDenomination.Count_100 = model.Txt_100_CDW; }
                    if (model.Txt_50_CDW > 0) { InDenomination.Count_50 = model.Txt_50_CDW; }
                    if (model.Txt_20_CDW > 0) { InDenomination.Count_20 = model.Txt_20_CDW; }
                    if (model.Txt_10_CDW > 0) { InDenomination.Count_10 = model.Txt_10_CDW; }
                    if (model.Txt_5_CDW > 0) { InDenomination.Count_5 = model.Txt_5_CDW; }
                    if (model.Txt_2_CDW > 0) { InDenomination.Count_2 = model.Txt_2_CDW; }
                    if (model.Txt_1_CDW > 0) { InDenomination.Count_1 = model.Txt_1_CDW; }
                    InParam.param_Denominations = InDenomination;
                    if (BASE._Cash_Bank_DBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success...";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK ="Null"+ xID
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
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    // edit
                    Common_Lib.RealTimeService.Parameter_Update_Voucher_CashBank UpParam = new Common_Lib.RealTimeService.Parameter_Update_Voucher_CashBank();
                    UpParam.VNo = model.Txt_V_NO_CDW == null ? "" : model.Txt_V_NO_CDW;
                    if (IsDate(model.Txt_V_Date_CDW.ToString()))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.Txt_V_Date_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.Txt_V_Date_CDW.ToString();
                    }
                    UpParam.ItemID = model.GLookUp_ItemList_CDW;
                    UpParam.Type = model.iTrans_Type;
                    UpParam.Cr_Led_ID = Cr_Led_id;
                    UpParam.Dr_Led_ID = Dr_Led_id;
                    UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    UpParam.Mode = model.iMode;
                    UpParam.Ref_No = model.Txt_Ref_No_CDW;
                    if (IsDate(model.Txt_Ref_Date_CDW.ToString()))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_Date = model.Txt_Ref_Date_CDW.ToString();
                    }

                    if (IsDate(model.Txt_Ref_CDate_CDW.ToString()))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_CDW).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.Txt_Ref_CDate_CDW.ToString();
                    }
                    UpParam.Amount = Convert.ToDouble(model.Txt_Amount_CDW);
                    UpParam.Narration = model.Txt_Narration_CDW ?? "";
                    UpParam.Remarks = model.Txt_Remarks_CDW ?? "";
                    UpParam.Reference = model.Txt_Reference_CDW ?? "";
                    UpParam.PurposeID = model.PurposeID_CDW;
                    UpParam.RecID = model.RecID;

                    //FCRA Update Process               
                    if (model.cdw_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.cdw_SplVchrReferenceSelected.Split(',');
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
                            UpParam.UpdateSplVchrRefs = UpdateSplVchrRefs;
                        }
                    }
                    Parameter_CashBank_Denomination InDenomination = new Parameter_CashBank_Denomination();
                    if (model.Txt_2000_CDW > 0) { InDenomination.Count_2000 = model.Txt_2000_CDW; }
                    if (model.Txt_1000_CDW > 0) { InDenomination.Count_1000 = model.Txt_1000_CDW; }
                    if (model.Txt_500_CDW > 0) { InDenomination.Count_500 = model.Txt_500_CDW; }
                    if (model.Txt_200_CDW > 0) { InDenomination.Count_200 = model.Txt_200_CDW; }
                    if (model.Txt_100_CDW > 0) { InDenomination.Count_100 = model.Txt_100_CDW; }
                    if (model.Txt_50_CDW > 0) { InDenomination.Count_50 = model.Txt_50_CDW; }
                    if (model.Txt_20_CDW > 0) { InDenomination.Count_20 = model.Txt_20_CDW; }
                    if (model.Txt_10_CDW > 0) { InDenomination.Count_10 = model.Txt_10_CDW; }
                    if (model.Txt_5_CDW > 0) { InDenomination.Count_5 = model.Txt_5_CDW; }
                    if (model.Txt_2_CDW > 0) { InDenomination.Count_2 = model.Txt_2_CDW; }
                    if (model.Txt_1_CDW > 0) { InDenomination.Count_1 = model.Txt_1_CDW; }
                    UpParam.param_Denominations = InDenomination;
                    if (BASE._Cash_Bank_DBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Success...";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = "Null" + model.RecID
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
                if (model.ActionMethod == Common.Navigation_Mode._Delete)
                {
                    if (BASE._Cash_Bank_DBOps.Delete(model.RecID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Success...";
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
        #region LookUp_GetItemList
        public List<VoucherTypeItems> LookUp_GetItemList()
        {
            DataTable d1 = BASE._Cash_Bank_DBOps.GetItemList();        
            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";           
            return DatatableToModel.DataTabletoVoucherWinCashLookUp_GetItemList(dview.ToTable());     
        } 
        #endregion
        #region LookUp bank list
        public List<BankList> LookUp_GetBankList()
        {
            DataTable BA_Table = BASE._Cash_Bank_DBOps.GetBankAccounts();      
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
            DataTable BB_Table = BASE._Cash_Bank_DBOps.GetBranchDetails(Branch_IDs);    
            var BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new BankList
                             {
                                 BANK_NAME = B["Name"].ToString(),
                                 BI_SHORT_NAME = B["BI_SHORT_NAME"].ToString(),
                                 BANK_BRANCH = B["Branch"].ToString(),
                                 BANK_ACC_NO = A["BA_ACCOUNT_NO"].ToString(),
                                 BA_ID = A["ID"].ToString(),
                                 REC_EDIT_ON = Convert.ToDateTime(A["REC_EDIT_ON"])
                             });

            return BuildData.ToList();
           // Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));  
        }      
        #endregion
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> LookUp_GetPurposeList()
        {            
            return BASE._Donation_DBOps.GetPurposes();               
        }     
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void SessionClear()
        {
            ClearBaseSession("_CDW");
        }
    }
}