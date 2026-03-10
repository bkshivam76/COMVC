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
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class CollectionBoxVoucherController : BaseController
    {
        // GET: Accounts/CollectionBoxVoucher
        public ActionResult Frm_Voucher_Win_C_Box(string Tag, string xID, string iSpecific_ItemID = "", bool Chk_Incompleted = false, string Info_LastEditedOn = null, string iTrans_Type = "DEBIT",string GridToRefresh = "CashBookListGrid")
        {
            if (BASE._open_Ins_ID == "00001" || BASE._open_Ins_ID == "00005" || BASE._open_Ins_ID == "00009")
            {
                // Details: Collection Box Voucher is allowed to Open in PBKIVV, TSRCT & SID Institute. So, no code here.
            }
            else
            {
                // Details: Collection Box Voucher is not allowed to Open in Institutes other than PBKIVV, TSRCT & SID.

                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','In  " + BASE._open_Ins_Name + " </br> </br> Collection Box Entry Not Applicable...!','Voucher Entry');</script>");
            }

            var i = 0;
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            string[] AM = { "_New", "_New_From_Selection", "_Edit", "_View", "_Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, Rights[i]) && Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            ViewData["ColBox_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["ColBox_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");

            CollectionBoxVoucher model = new CollectionBoxVoucher();
            model.CBox_BankList = RefreshBankList();
            model.CBox_ItemList = RefreshItemList();
            model.CBox_PurposeList = LookUp_GetPurposeList();
            model.CBox_PartyList = RefreshPartyList();
            model.CBox_RefBankList = RefreshRefBankList();
            Common.Navigation_Mode ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = ActionMethod;
            model.TempActionMethod = ActionMethod.ToString();
            model.TitleX = "Collection Box";
            model.xID = xID;
            if (model.TempActionMethod == "_New" || model.TempActionMethod == "_New_From_Selection")
            {
                model.Text = "New ~ " + model.TitleX;
                model.Txt_V_NO_CBox = "";
            }
            else if (Tag == "_Edit" || Tag == "_View" || Tag == "_Delete")
            {
                model.Text = "Edit ~ " + model.TitleX;
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d1 = BASE._CollectionBox_DBOps.GetRecord(xID);
                string message = Messages.SomeError;
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                DateTime? xDate = null;
                xDate = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
                model.Txt_V_Date_CBox = xDate;
                // -----------------------------+
                // Start : Check if entry already changed 
                // -----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete || model.ActionMethod == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (model.Info_LastEditedOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            message = Messages.RecordChanged("Current Contribution", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_V_NO_CBox = d1.Rows[0]["TR_VNO"].ToString();
                model.Cmd_Mode_CBox = d1.Rows[0]["TR_MODE"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["TR_ITEM_ID"]))
                {
                    if (d1.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_ItemList_CBox = d1.Rows[0]["TR_ITEM_ID"].ToString();
                    }
                }
                string Bank_ID = "";
                if (d1.Rows[0]["TR_TYPE"].ToString().ToUpper() == "DEBIT")
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["TR_SUB_CR_LED_ID"]) && d1.Rows[0]["TR_CR_LED_ID"].ToString() == "00079")
                    {
                        Bank_ID = d1.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                    }
                }
                else
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["TR_SUB_DR_LED_ID"]) && d1.Rows[0]["TR_DR_LED_ID"].ToString() == "00079")
                    {
                        Bank_ID = d1.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                    }
                }
                if (Bank_ID.ToString().Length > 0)
                {
                    model.GLookUp_BankList_CBox = Bank_ID;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_BANK_ID"]))
                {
                    if (d1.Rows[0]["TR_REF_BANK_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RefBankList_CBox = d1.Rows[0]["TR_REF_BANK_ID"].ToString();
                    }
                }

                model.Txt_Ref_Branch_CBox = d1.Rows[0]["TR_REF_BRANCH"].ToString();
                model.Txt_Ref_No_CBox = d1.Rows[0]["TR_REF_NO"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_DATE"]))
                {
                    xDate = Convert.ToDateTime(d1.Rows[0]["TR_REF_DATE"]);
                    model.Txt_Ref_Date_CBox = xDate;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_REF_CDATE"]))
                {
                    xDate = Convert.ToDateTime(d1.Rows[0]["TR_REF_CDATE"]);
                    model.Txt_Ref_CDate_CBox = xDate;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_AB_ID_1"]))
                {
                    if (d1.Rows[0]["TR_AB_ID_1"].ToString().Length > 0)
                    {
                        model.GLookUp_PartyList1_CBox = d1.Rows[0]["TR_AB_ID_1"].ToString();
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["TR_AB_ID_2"]))
                {
                    if (d1.Rows[0]["TR_AB_ID_2"].ToString().Length > 0)
                    {
                        model.GLookUp_PartyList2_CBox = d1.Rows[0]["TR_AB_ID_2"].ToString();
                    }
                }
                model.Txt_Amount_CBox = Convert.ToDouble(d1.Rows[0]["TR_AMOUNT"]);
                model.Txt_Remarks_CBox = d1.Rows[0]["TR_REMARKS"].ToString();
                model.Txt_Reference_CBox = d1.Rows[0]["TR_REFERENCE"].ToString();

                var _Denominations = BASE._CollectionBox_DBOps.GetDenominations(model.xID);
                model.Txt_2000 = _Denominations.Count_2000;
                model.Txt_1000 = _Denominations.Count_1000;
                model.Txt_500 = _Denominations.Count_500;
                model.Txt_200 = _Denominations.Count_200;
                model.Txt_100 = _Denominations.Count_100;
                model.Txt_50 = _Denominations.Count_50;
                model.Txt_20 = _Denominations.Count_20;
                model.Txt_10 = _Denominations.Count_10;
                model.Txt_5 = _Denominations.Count_5;
                model.Txt_2 = _Denominations.Count_2;
                model.Txt_1 = _Denominations.Count_1;
                model.Txt_Narration_CBox = d1.Rows[0]["TR_NARRATION"].ToString();
            }
            if (Tag == "_View")
            {
                model.Text = "View ~ " + model.TitleX;
            }
            else if (Tag == "_Delete")
            {
                model.Text = "Delete ~ " + model.TitleX;
            }
            if (Tag == "_New_From_Selection")
            {
                model.GLookUp_ItemList_CBox = iSpecific_ItemID;
                model.iSpecific_ItemID = iSpecific_ItemID;
            }
            ViewBag.Allow_Bank_In_C_Box = BASE.Allow_Bank_In_C_Box;
            if (model.TempActionMethod == "_New" || model.TempActionMethod == "_New_From_Selection")
            {
                string TR_AB_ID_1 = "";
                string TR_AB_ID_2 = "";
                DataTable Witnesses = BASE._CollectionBox_DBOps.GetPastWitness();
                if (Witnesses == null)
                {
                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                if (Witnesses.Rows.Count > 0)
                {
                    TR_AB_ID_1 = Witnesses.Rows[0]["TR_AB_ID_1"].ToString();
                    TR_AB_ID_2 = Witnesses.Rows[0]["TR_AB_ID_2"].ToString();
                }
                if (TR_AB_ID_1.Length > 0)
                {
                    model.GLookUp_PartyList1_CBox = TR_AB_ID_1;
                }
                if (TR_AB_ID_2.Length > 0)
                {
                    model.GLookUp_PartyList2_CBox = TR_AB_ID_2;
                }
            }
            model.PurposeList__CollBox = BASE._CollectionBox_DBOps.GetPurposeID(xID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_CollectionBoxVoucher_Window(CollectionBoxVoucher model, string resultflag)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                Common.Navigation_Mode Tag = model.ActionMethod;
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable collectionBox_DbOps = BASE._CollectionBox_DBOps.GetRecord(model.xID);
                        if (collectionBox_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (collectionBox_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Contribution");
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
                        MaxValue = BASE._CollectionBox_DBOps.GetStatus(model.xID);
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

                        if (model.LastEditedOn != Convert.ToDateTime(collectionBox_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Contribution");
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
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    decimal Denomination_Amt = Convert.ToDecimal(((model.Txt_2000??0) * 2000) + ((model.Txt_1000??0) * 1000) + ((model.Txt_500??0) * 500) + ((model.Txt_200??0) * 200) + ((model.Txt_100??0) * 100) + ((model.Txt_50??0) * 50) + ((model.Txt_20??0) * 20) + ((model.Txt_10??0) * 10) + ((model.Txt_5??0) * 5) + ((model.Txt_2??0) * 2) + ((model.Txt_1??0) * 1));
                    if (string.IsNullOrEmpty(model.GLookUp_ItemList_CBox))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (IsDate(model.Txt_V_Date_CBox) == false)
                    {
                        jsonParam.message = "Voucher Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_V_Date_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_CBox) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_V_Date_CBox) < Convert.ToDateTime(BASE._open_Year_Sdt)
                            || Convert.ToDateTime(model.Txt_V_Date_CBox) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Voucher Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_V_Date_CBox";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (string.IsNullOrEmpty(model.GLookUp_PartyList1_CBox))
                    {
                        jsonParam.message = "First Person Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList1_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_PartyList2_CBox))
                    {
                        jsonParam.message = "Second Person Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList2_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.GLookUp_PartyList1_CBox.Trim() == model.GLookUp_PartyList2_CBox.Trim())
                    {
                        jsonParam.message = "Both Person Name Cannot Be Same...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList2_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Denomination_Amt == 0)
                    {
                        jsonParam.message = "Please Enter Denomination...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_2000";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Amount_CBox <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmd_Mode_CBox))
                    {
                        jsonParam.message = "Mode Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Mode_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_BankList_CBox) && model.Cmd_Mode_CBox.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else { model.GLookUp_BankList_CBox = model.GLookUp_BankList_CBox ?? ""; }

                    if (string.IsNullOrEmpty(model.GLookUp_RefBankList_CBox) && model.Cmd_Mode_CBox.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_RefBankList_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else { model.GLookUp_RefBankList_CBox = model.GLookUp_RefBankList_CBox ?? ""; }

                    if (string.IsNullOrEmpty(model.Txt_Ref_Branch_CBox) && model.Cmd_Mode_CBox.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Branch Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Branch_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else { model.Txt_Ref_Branch_CBox = model.Txt_Ref_Branch_CBox ?? ""; }

                    if (string.IsNullOrEmpty(model.Txt_Ref_No_CBox) && model.Cmd_Mode_CBox.ToUpper() != "CASH")
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_No_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else { model.Txt_Ref_No_CBox = model.Txt_Ref_No_CBox ?? ""; }

                    if (IsDate(model.Txt_Ref_Date_CBox) == false && model.Cmd_Mode_CBox.ToUpper() != "CASH")
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Date_CBox";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Ref_CDate_CBox != null && IsDate(model.Txt_Ref_CDate_CBox))
                    {
                        if (Convert.ToDateTime(model.Txt_Ref_CDate_CBox) < Convert.ToDateTime(model.Txt_Ref_CDate_CBox))
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate_CBox";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.PurposeList__CollBox))
                    {
                        jsonParam.message = "Purpose is Required. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "PurposeList__CollBox";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                // ---------------------------// Start Dependencies //-----------------
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection || Tag == Common.Navigation_Mode._Edit)
                    {
                        DateTime NewEditOn = new DateTime();
                        DateTime oldEditOn = new DateTime();
                        if (model.GLookUp_PartyList1_CBox.Length > 0)
                        {
                            DataTable d1 = BASE._CollectionBox_DBOps.GetAddresses(model.GLookUp_PartyList1_CBox);
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
                            if (model.Party1_LastEditedOn!=null)
                            {
                                oldEditOn = Convert.ToDateTime(model.Party1_LastEditedOn);
                            }
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
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
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
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
                        if (model.GLookUp_PartyList2_CBox.Length > 0)
                        {
                            DataTable d1 = BASE._CollectionBox_DBOps.GetAddresses(model.GLookUp_PartyList2_CBox);
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
                            
                            if (model.Party2_LastEditedOn!=null)
                            {
                                oldEditOn = Convert.ToDateTime(model.Party2_LastEditedOn);
                            }
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
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
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
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
                string Status_Action = ((int)Common.Record_Status._Completed).ToString();
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    Status_Action = ((int)Common.Record_Status._Deleted).ToString();
                }
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    string Dr_Led_id = "";
                    string Cr_Led_id = "";
                    string Sub_Dr_Led_ID = "";
                    string Sub_Cr_Led_ID = "";
                    if (model.iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.iLed_ID;
                        if (model.Cmd_Mode_CBox.ToUpper() == "CASH")
                        {
                            Cr_Led_id = "00080";
                        }
                        else
                        {
                            Cr_Led_id = "00079";
                            Sub_Cr_Led_ID = "";
                        }
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;
                        if (model.Cmd_Mode_CBox.ToUpper() == "CASH")
                        {
                            Dr_Led_id = "00080";
                        }
                        else
                        {
                            Dr_Led_id = "00079";
                            Sub_Dr_Led_ID = model.GLookUp_BankList_CBox;
                        }
                    }
                    model.xID = Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_Voucher_CollectionBox InParam = new Common_Lib.RealTimeService.Parameter_Insert_Voucher_CollectionBox();
                    InParam.TransCode = Convert.ToInt32(Common.Voucher_Screen_Code.Collection_Box);
                    InParam.VNo = model.Txt_V_NO_CBox ?? "";
                    if (IsDate(model.Txt_V_Date_CBox))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_CBox).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.Txt_V_Date_CBox.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_CBox;
                    InParam.Type = model.iTrans_Type ?? "";
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam.Mode = model.Cmd_Mode_CBox ?? "";
                    InParam.Ref_Bank_ID = model.GLookUp_RefBankList_CBox;
                    InParam.Ref_Branch = model.Txt_Ref_Branch_CBox;
                    InParam.Ref_No = model.Txt_Ref_No_CBox;
                    if (IsDate(model.Txt_Ref_Date_CBox))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_CBox).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = model.Txt_Ref_Date_CBox.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_CBox))
                    {
                        InParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_CBox).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_ChequeDate = model.Txt_Ref_CDate_CBox.ToString();
                    }
                    InParam.Amount = Convert.ToDouble(model.Txt_Amount_CBox);
                    InParam.Witness1 = model.GLookUp_PartyList1_CBox;
                    InParam.Witness2 = model.GLookUp_PartyList2_CBox;
                    InParam.Narration = model.Txt_Narration_CBox ?? "";
                    InParam.Remarks = model.Txt_Remarks_CBox ?? "";
                    InParam.Reference = model.Txt_Reference_CBox ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.PurposeID = model.PurposeList__CollBox;
                    InParam.RecID = model.xID;
                    Common_Lib.RealTimeService.Parameter_CollectionBox_Denomination InDenomination = new Common_Lib.RealTimeService.Parameter_CollectionBox_Denomination();
                    if (model.Txt_2000 > 0) { InDenomination.Count_2000 = (int)model.Txt_2000; }
                    if (model.Txt_1000 > 0) { InDenomination.Count_1000 = (int)model.Txt_1000; }
                    if (model.Txt_500 > 0) { InDenomination.Count_500 = (int)model.Txt_500; }
                    if (model.Txt_200 > 0) { InDenomination.Count_200 = (int)model.Txt_200; }
                    if (model.Txt_100 > 0) { InDenomination.Count_100 = (int)model.Txt_100; }
                    if (model.Txt_50 > 0) { InDenomination.Count_50 = (int)model.Txt_50; }
                    if (model.Txt_20 > 0) { InDenomination.Count_20 = (int)model.Txt_20; }
                    if (model.Txt_10 > 0) { InDenomination.Count_10 = (int)model.Txt_10; }
                    if (model.Txt_5 > 0) { InDenomination.Count_5 = (int)model.Txt_5; }
                    if (model.Txt_2 > 0) { InDenomination.Count_2 = (int)model.Txt_2; }
                    if (model.Txt_1 > 0) { InDenomination.Count_1 = (int)model.Txt_1; }
                    InParam.param_Denominations = InDenomination;

                    if (BASE._CollectionBox_DBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK ="Null"+model.xID
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
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    // edit
                    string Dr_Led_id = "";
                    string Cr_Led_id = "";
                    string Sub_Dr_Led_ID = "";
                    string Sub_Cr_Led_ID = "";
                    if (model.iTrans_Type.ToUpper() == "DEBIT")
                    {
                        Dr_Led_id = model.iLed_ID;
                        if (model.Cmd_Mode_CBox.ToUpper() == "CASH")
                        {
                            Cr_Led_id = "00080";
                        }
                        else
                        {
                            Cr_Led_id = "00079";
                            Sub_Cr_Led_ID = "";
                        }
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;
                        if (model.Cmd_Mode_CBox.ToUpper() == "CASH")
                        {
                            Dr_Led_id = "00080";
                        }
                        else
                        {
                            Dr_Led_id = "00079";
                            Sub_Dr_Led_ID = model.GLookUp_BankList_CBox;
                        }
                    }

                    Common_Lib.RealTimeService.Parameter_Update_Voucher_CollectionBox UpParam = new Common_Lib.RealTimeService.Parameter_Update_Voucher_CollectionBox();
                    UpParam.VNo = model.Txt_V_NO_CBox ?? "";
                    if (IsDate(model.Txt_V_Date_CBox))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.Txt_V_Date_CBox).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.Txt_V_Date_CBox.ToString();
                    }

                    UpParam.ItemID = model.GLookUp_ItemList_CBox;
                    UpParam.Type = model.iTrans_Type ?? "";
                    UpParam.Cr_Led_ID = Cr_Led_id;
                    UpParam.Dr_Led_ID = Dr_Led_id;
                    UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    UpParam.Mode = model.Cmd_Mode_CBox;
                    UpParam.Ref_Bank_ID = model.GLookUp_RefBankList_CBox;
                    UpParam.Ref_Branch = model.Txt_Ref_Branch_CBox;
                    UpParam.Ref_No = model.Txt_Ref_No_CBox;
                    if (IsDate(model.Txt_Ref_Date_CBox))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_CBox).ToString(BASE._Server_Date_Format_Short);//redmine Bug #133419 fixed
                    }
                    else
                    {
                        UpParam.Ref_Date = model.Txt_Ref_Date_CBox.ToString();
                    } 
                    if (IsDate(model.Txt_Ref_CDate_CBox))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_CBox).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.Txt_Ref_CDate_CBox.ToString();
                    }
                    UpParam.Amount = Convert.ToDouble(model.Txt_Amount_CBox);
                    UpParam.Witness1 = model.GLookUp_PartyList1_CBox;
                    UpParam.Witness2 = model.GLookUp_PartyList2_CBox;
                    UpParam.Narration = model.Txt_Narration_CBox;
                    UpParam.Remarks = model.Txt_Remarks_CBox;
                    UpParam.Reference = model.Txt_Reference_CBox;
                    UpParam.PurposeID = model.PurposeList__CollBox;
                    UpParam.RecID = model.xID;

                    Common_Lib.RealTimeService.Parameter_CollectionBox_Denomination InDenomination = new Common_Lib.RealTimeService.Parameter_CollectionBox_Denomination();
                    if (model.Txt_2000 > 0) { InDenomination.Count_2000 = (int)model.Txt_2000; }
                    if (model.Txt_1000 > 0) { InDenomination.Count_1000 = (int)model.Txt_1000; }
                    if (model.Txt_500 > 0) { InDenomination.Count_500 = (int)model.Txt_500; }
                    if (model.Txt_200 > 0) { InDenomination.Count_200 = (int)model.Txt_200; }
                    if (model.Txt_100 > 0) { InDenomination.Count_100 = (int)model.Txt_100; }
                    if (model.Txt_50 > 0) { InDenomination.Count_50 = (int)model.Txt_50; }
                    if (model.Txt_20 > 0) { InDenomination.Count_20 = (int)model.Txt_20; }
                    if (model.Txt_10 > 0) { InDenomination.Count_10 = (int)model.Txt_10; }
                    if (model.Txt_5 > 0) { InDenomination.Count_5 = (int)model.Txt_5; }
                    if (model.Txt_2 > 0) { InDenomination.Count_2 = (int)model.Txt_2; }
                    if (model.Txt_1 > 0) { InDenomination.Count_1 = (int)model.Txt_1; }
                    UpParam.param_Denominations = InDenomination;
                    if (BASE._CollectionBox_DBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            CashbookGridPK = "Null" + model.xID
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
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    if (BASE._CollectionBox_DBOps.Delete(model.xID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
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
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }



        #region "Start--> LookupEdit Events"

        public List<VoucherTypeItems> RefreshItemList()
        {
            DataTable d1 = BASE._CollectionBox_DBOps.GetItemList();
            if (d1 == null)
            {
                return null;
            }
            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            return DatatableToModel.DataTabletoVoucherCollectionLookUp_GetItemList(dview.ToTable());            
        }
        public List<CollectionBoxPartyList> RefreshPartyList()
        {
            DataTable d1 = BASE._CollectionBox_DBOps.GetAddresses();
            if (d1 == null)
            {
                return null;
            }
            DataView dview = new DataView(d1);
            dview.Sort = "C_NAME";
            return DatatableToModel.DataTabletoVoucherCollectionBoxLookUp_GetPartyList(dview.ToTable());
        }
        public ActionResult LookUp_GetPartyListJson()
        {
            return Content(JsonConvert.SerializeObject(RefreshPartyList()), "application/json");//Returned in JSON So that it can be stored in Array DataStore. Complex DataTypes can't be implicitly Converted
        }
        public List<BankList> RefreshBankList()
        {
            DataTable BA_Table = BASE._CollectionBox_DBOps.GetBankAccounts();
            if (BA_Table == null)
            {
                return null;
            }
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
            DataTable BB_Table = BASE._CollectionBox_DBOps.GetBranches(Branch_IDs);
            if (BB_Table == null)
            {
                return null;
            }
            var BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new BankList
                             {
                                 BANK_NAME = B["Name"].ToString(),
                                 BI_SHORT_NAME = B["BI_SHORT_NAME"].ToString(),
                                 BANK_BRANCH = B["Branch"].ToString(),
                                 BANK_ACC_NO = A["BA_ACCOUNT_NO"].ToString(),
                                 BA_ID = A["ID"].ToString()
                                
                             });

            var Final_Data = BuildData.ToList();
            Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            return Final_Data;            
        }
        public List<RefBank> RefreshRefBankList()
        {
            DataTable B2 = BASE._CollectionBox_DBOps.GetBanks();
            if (B2 == null)
            {
                return null;
            }
            DataView dview = new DataView(B2);
            dview.Sort = "BI_BANK_NAME";
            return DatatableToModel.DataTabletoCollectionBoxRefBankList(dview.ToTable());
        }
        #endregion

        #region Lookup_GetPurposeList
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> LookUp_GetPurposeList()
        {
           return BASE._Donation_DBOps.GetPurposes();
        }
        #endregion
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
        public void SessionClear()
        {
            ClearBaseSession("_CBox");            
        }

    }
}