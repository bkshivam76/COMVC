using Common_Lib;
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
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Account.Controllers
{

    [CheckLogin]
    public class BankToBankTransferVoucherController : BaseController
    {
        public ActionResult Frm_Voucher_Win_B2B(string Tag, string xID, string iSpecific_ItemID = "", bool Chk_Incompleted = false, string Info_LastEditedOn = null, string GridToRefresh = "CashBookListGrid")
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Add")) && (Tag == "_New" || Tag == "_New_From_Selection"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Update")) && (Tag == "_Edit"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "View")) && (Tag == "_View"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Delete")) && (Tag == "_Delete"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            Voucher_BankToBank model = new Voucher_BankToBank();
            ViewBag.GridToRefresh = GridToRefresh;
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.TempActionMethod = Tag;
            model.TitleX_B2B = "Bank to Bank Transfer";
            model.xID_B2B = xID;

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_B2B = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.ActionMethod);
            model.splVchrRefsCount_B2B = model.SpecialReferenceList_Data_B2B.Count();
            model.Bank1_Data_B2B = RefreshBankList();
            model.Bank2_Data_B2B = RefreshBankList();
            model.B2B_Item_Data = RefreshItemList();
            model.PurposeList_B2B = LookUp_GetPurposeList();
            if (model.Bank1_Data_B2B == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (model.Bank2_Data_B2B == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (model.PurposeList_B2B == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (model.B2B_Item_Data == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (Tag == "_Edit" || Tag == "_View" || Tag == "_Delete")
            {
                DataTable d1 = BASE._Cash_Bank_DBOps.GetRecord(xID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                model.Txt_V_Date_B2B = (DateTime?)d1.Rows[0]["TR_DATE"];

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_B2B = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete || model.ActionMethod == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            viewstr = "View";
                        }
                        if (Convert.ToDateTime(Info_LastEditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            string message = Messages.RecordChanged("Current Transfer", viewstr);                            
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                model.LastEditedOn_B2B = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_V_NO_B2B = d1.Rows[0]["TR_VNO"].ToString();
                model.Cmd_Mode_B2B = d1.Rows[0]["TR_MODE"].ToString();
                if (d1.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.GLookUp_ItemList_B2B = d1.Rows[0]["TR_ITEM_ID"].ToString();
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_SUB_CR_LED_ID"]))
                {
                    if (d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_BankList1_B2B = d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                    }
                }
                model.Txt_Ref_No_B2B = d1.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_DATE"]))
                {
                    model.Txt_Ref_Date_B2B = Convert.ToDateTime(d1.Rows[0]["TR_REF_DATE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_CDATE"]))
                {
                    model.Txt_Ref_CDate_B2B = Convert.ToDateTime(d1.Rows[0]["TR_REF_CDATE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_SUB_DR_LED_ID"]))
                {
                    if (d1.Rows[0]["TR_SUB_DR_LED_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_BankList2_B2B = d1.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                    }
                }
                model.Txt_Amount_B2B = Convert.ToDouble(d1.Rows[0]["TR_AMOUNT"]);
                model.Txt_Narration_B2B = d1.Rows[0]["TR_NARRATION"].ToString();
                model.Txt_Remarks_B2B = d1.Rows[0]["TR_REMARKS"].ToString();
                model.Txt_Reference_B2B = d1.Rows[0]["TR_REFERENCE"].ToString();
                model.PurposeID_B2B = BASE._b2b_DBOps.GetPurposeID(xID);
            }
            if (Tag == "_New_From_Selection")
            {
                model.GLookUp_ItemList_B2B = iSpecific_ItemID;
            }   
            return View(model);            
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_B2B(Voucher_BankToBank model, string resultflag)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);

                if (BASE.AllowMultiuser())
                {
                    if (model.GLookUp_BankList1_B2B != null && model.GLookUp_BankList1_B2B.Length > 0)
                    {
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList1_B2B, "");
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
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted...! <br><br>In this entry, Used Bank A / c No.: " + AccNo + " was closed...!!!";
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
                    if (model.GLookUp_BankList2_B2B != null && model.GLookUp_BankList2_B2B.Length > 0)
                    {
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList2_B2B, "");
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
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted...! <br><br>In this entry, Used Bank A / c No.: " + AccNo + " was closed...!!!";
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
                    if (model.ActionMethod == Common.Navigation_Mode._Edit | model.ActionMethod == Common.Navigation_Mode._Delete)
                    {
                        DataTable b2b_DbOps = BASE._b2b_DBOps.GetRecord(model.xID_B2B);
                        if (b2b_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (b2b_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transfer");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn_B2B != Convert.ToDateTime(b2b_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transfer");
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
                        MaxValue = BASE._b2b_DBOps.GetStatus(model.xID_B2B);
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
                            jsonParam.message = "Locked Entry Cannot Be Edited/Deleted...<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.GLookUp_ItemList_B2B))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_B2B.ToString()) == false)
                    {
                        jsonParam.message = "Voucher Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_V_Date_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_B2B.ToString()) == true)
                    {
                        if (model.Txt_V_Date_B2B < BASE._open_Year_Sdt || model.Txt_V_Date_B2B > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Voucher Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_V_Date_B2B";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_BankList1_B2B))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList1_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Txt_Ref_No_B2B))
                    {
                        jsonParam.message = model.Cmd_Mode_B2B + " No. Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_No_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_Ref_Date_B2B.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Date_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_BankList2_B2B))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList2_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.GLookUp_BankList1_B2B == model.GLookUp_BankList2_B2B)
                    {
                        jsonParam.message = "Both Bank Are Same...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList2_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Amount_B2B <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_B2B";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Ref_CDate_B2B != null && IsDate(model.Txt_Ref_CDate_B2B.ToString()))
                    {
                        if (Convert.ToDateTime(model.Txt_Ref_CDate_B2B) < Convert.ToDateTime(model.Txt_Ref_Date_B2B))
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate_B2B";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.PurposeID_B2B))
                    {
                        jsonParam.message = "Purpose is Required. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "PurposeID_B2B";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._New | model.ActionMethod == Common.Navigation_Mode._New_From_Selection | model.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        DateTime NewEditOn = default(DateTime);
                        DateTime oldEditOn = default(DateTime);
                        if (!string.IsNullOrEmpty(model.GLookUp_BankList1_B2B))
                        {
                            oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank1_B2B);
                            DataTable d1 = BASE._b2b_DBOps.GetBankAccounts(model.GLookUp_BankList1_B2B);
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

                        if (!string.IsNullOrEmpty(model.GLookUp_BankList2_B2B))
                        {
                            oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank2_B2B);
                            DataTable d1 = BASE._b2b_DBOps.GetBankAccounts(model.GLookUp_BankList2_B2B);
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
                    }
                }

                string Status_Action = "";
                Status_Action = Convert.ToString((int)Common.Record_Status._Completed);
                if (model.ActionMethod == Common.Navigation_Mode._New | model.ActionMethod == Common.Navigation_Mode._New_From_Selection)
                {
                    string Dr_Led_id = "00079";
                    string Cr_Led_id = "00079";
                    string xID = Guid.NewGuid().ToString();
                    Parameter_Insert_Voucher_BankToBank InParam = new Parameter_Insert_Voucher_BankToBank();
                    InParam.TransCode = (int)Common.Voucher_Screen_Code.Bank_To_Bank_Transfer;
                    InParam.VNo = model.Txt_V_NO_B2B ?? "";
                    if (IsDate(model.Txt_V_Date_B2B.ToString()))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.Txt_V_Date_B2B.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_B2B;
                    InParam.Type = model.iTrans_Type_B2B;
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = model.GLookUp_BankList1_B2B;
                    InParam.Sub_Dr_Led_ID = model.GLookUp_BankList2_B2B;
                    InParam.Mode = model.Cmd_Mode_B2B;
                    InParam.Ref_No = model.Txt_Ref_No_B2B ?? "";
                    if (IsDate(model.Txt_Ref_Date_B2B.ToString()))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = model.Txt_Ref_Date_B2B.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_B2B.ToString()))
                    {
                        InParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_ChequeDate = model.Txt_Ref_CDate_B2B.ToString();
                    }
                    InParam.Amount = Convert.ToDouble(model.Txt_Amount_B2B);
                    InParam.Narration = model.Txt_Narration_B2B ?? "";
                    InParam.Remarks = model.Txt_Remarks_B2B ?? "";
                    InParam.Reference = model.Txt_Reference_B2B ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.PurposeID = model.PurposeID_B2B;
                    InParam.RecID = xID;

                    //FCRA Insert Process
                    if (model.B2B_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.B2B_SplVchrReferenceSelected.Split(',');
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

                    if (BASE._b2b_DBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Bank to Bank Transfer...";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = "Null"+xID
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
                //edit
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    string Dr_Led_id = "00079";
                    string Cr_Led_id = "00079";
                    Parameter_Update_Voucher_BankToBank UpParam = new Parameter_Update_Voucher_BankToBank();
                    UpParam.VNo = model.Txt_V_NO_B2B ?? "";
                    if (IsDate(model.Txt_V_Date_B2B.ToString()))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.Txt_V_Date_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.Txt_V_Date_B2B.ToString();
                    }
                    UpParam.ItemID = model.GLookUp_ItemList_B2B;
                    UpParam.Type = model.iTrans_Type_B2B;
                    UpParam.Cr_Led_ID = Cr_Led_id;
                    UpParam.Dr_Led_ID = Dr_Led_id;
                    UpParam.Sub_Cr_Led_ID = model.GLookUp_BankList1_B2B;
                    UpParam.Sub_Dr_Led_ID = model.GLookUp_BankList2_B2B;
                    UpParam.Mode = model.Cmd_Mode_B2B;
                    UpParam.Ref_No = model.Txt_Ref_No_B2B ?? "";
                    if (IsDate(model.Txt_Ref_Date_B2B.ToString()))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_Date = model.Txt_Ref_Date_B2B.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_B2B.ToString()))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_B2B).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.Txt_Ref_CDate_B2B.ToString();
                    }
                    UpParam.Amount = Convert.ToDouble(model.Txt_Amount_B2B);
                    UpParam.Narration = model.Txt_Narration_B2B ?? "";
                    UpParam.Remarks = model.Txt_Remarks_B2B ?? "";
                    UpParam.Reference = model.Txt_Reference_B2B ?? "";
                    UpParam.RecID = model.xID_B2B;
                    UpParam.PurposeID = model.PurposeID_B2B;

                    //FCRA Update Process               
                    if (model.B2B_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.B2B_SplVchrReferenceSelected.Split(',');
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
                    if (BASE._b2b_DBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Bank to Bank Transfer...";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = "Null" + model.xID_B2B
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
                    if (BASE._b2b_DBOps.Delete(model.xID_B2B))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Bank to Bank Transfer...";
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
        #region LookUp_GetItemList
        public List<VoucherTypeItems> RefreshItemList()
        {
            DataTable d1 = BASE._b2b_DBOps.GetItemList();
            if (d1 == null)return null;
            // Duplicate each row in the DataTable
            //DataTable d1_duplicated = d1.Clone(); // Create an empty table with the same schema
            //foreach (DataRow row in d1.Rows)
            //{
            //    // Add the same row twice to the new DataTable
            //    d1_duplicated.ImportRow(row);
            //    d1_duplicated.ImportRow(row);
            //}
            DataView dview = new DataView(d1);
            //DataView dview = new DataView(d1_duplicated);
            dview.Sort = "ITEM_NAME";       
            return DatatableToModel.DataTabletoVoucherWinCashLookUp_GetItemList(dview.ToTable());
        }
        #endregion
        #region LookUp bank list
        public List<BankList> RefreshBankList()
        {
            DataTable BA_Table = BASE._Cash_Bank_DBOps.GetBankAccounts();
            if (BA_Table == null)return null;
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
            if (BB_Table == null)return null;
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

            var Final_Data = BuildData.ToList();
            Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            return Final_Data;            
        }
        #endregion
        #region Lookup_GetPurposeList
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> LookUp_GetPurposeList()
        {
            return BASE._Donation_DBOps.GetPurposes();
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

        public void SessionClear()
        {
            ClearBaseSession("_B2B");
        }
    }


}