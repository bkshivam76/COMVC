using Common_Lib;
using Common_Lib.RealTimeService;
using static Common_Lib.Common;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Voucher_Donation;
using static Common_Lib.DbOperations.Vouchers;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Account.Controllers
{

    [CheckLogin]
    public class DonationVoucherController : BaseController
    {
        #region Global Variables      
        public List<Return_DonationVoucherItemList> GetItemList_ForeignDonation
        {
            get
            {
                return (List<Return_DonationVoucherItemList>)GetBaseSession("GetItemList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetItemList_ForeignDonation_F", value);
            }
        }
        public List<Return_DonationVoucherPartyList> GetPartyList_ForeignDonation
        {
            get
            {
                return (List<Return_DonationVoucherPartyList>)GetBaseSession("GetPartyList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetPartyList_ForeignDonation_F", value);
            }
        }
        public List<CategoryList> GetCatList_ForeignDonation
        {
            get
            {
                return (List<CategoryList>)GetBaseSession("GetCatList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetCatList_ForeignDonation_F", value);
            }
        }
        public List<Return_GetBankAccounts> GetBankList_ForeignDonation
        {
            get
            {
                return (List<Return_GetBankAccounts>)GetBaseSession("GetBankList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetBankList_ForeignDonation_F", value);
            }
        }
        public List<CurrencyList> GetCurList_ForeignDonation
        {
            get
            {
                return (List<CurrencyList>)GetBaseSession("GetCurList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetCurList_ForeignDonation_F", value);
            }
        }
        public List<Return_ReferenceBankList> GetRefBankList_ForeignDonation
        {
            get
            {
                return (List<Return_ReferenceBankList>)GetBaseSession("GetRefBankList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetRefBankList_ForeignDonation_F", value);
            }
        }
        public List<Return_DonationVocuherPurpose> GetPurList_ForeignDonation
        {
            get
            {
                return (List<Return_DonationVocuherPurpose>)GetBaseSession("GetPurList_ForeignDonation_F");
            }
            set
            {
                SetBaseSession("GetPurList_ForeignDonation_F", value);
            }
        }
        //List<Return_DonationVoucherPurpose> GetPurList_ForeignDonation_F

        #endregion

        #region Donation_R
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Donation_R(string Tag = null, string xID = null, string iSpecific_ItemID = "", string P_Chk_Dd_Ref = "", string TR_AB_ID = "", string Info_LastEditedOn = "", string GridToRefresh = "CashBookListGrid")
        {
            var i = 0;
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            string[] AM = { "_New", "_New_From_Selection", "_Edit", "_View", "_Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Donation, Rights[i]) && Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewData["DonationVou_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["DonationVou_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewBag.GridToRefresh = GridToRefresh;
            DonationVoucher model = new DonationVoucher();
            model.ActionMethod = (Navigation_Mode)Enum.Parse(typeof(Navigation_Mode), Tag);
            model.TempActionMethod = model.ActionMethod.ToString();
            model.xID = xID;
            model.TitleX = "Donation";
            model.Cmd_Mode_Donation = "CASH";
            model.ItemData = LookUp_GetItemList();
            if (model.ItemData.Count == 1)
            {
                model.GLookUp_ItemList_Donation = model.ItemData[0].ITEM_ID;
            }
            model.PartyData = LookUp_GetPartyList();
            model.BankData = LookUp_GetBankList();
            if (model.BankData.Count == 1)
            {
                model.GLookUp_BankList_Donation = model.BankData[0].BA_ID;
            }
            model.RefBankData = LookUp_GetRefBankList();
            model.PurposeData = LookUp_GetPurposeList();
            if (model.ActionMethod == Navigation_Mode._New || model.ActionMethod == Navigation_Mode._New_From_Selection)
            {
                model.Text = "New ~ " + model.TitleX;
                model.Txt_V_NO_Donation = "";
            }

            //Special Voucher References (FCRA Related) Code Get items from Center Task info 
            model.SpecialReferenceList_Data_DnR = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.ActionMethod);
            model.splVchrRefsCount_DnR = model.SpecialReferenceList_Data_DnR.Count();

            if (model.ActionMethod == Navigation_Mode._Edit || model.ActionMethod == Navigation_Mode._Delete || model.ActionMethod == Navigation_Mode._View)
            {
                model.Text = "Edit ~ " + model.TitleX;
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);

                //FCRA Related or Special Voucher References Related onEditGet dbfunction calling from Special Voucher transactions data.
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_DnR = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                var d1 = BASE._Donation_DBOps.GetRecord(model.xID);
                var d4 = BASE._Donation_DBOps.GetSlipRecord(model.xID);
                var d5 = new List<Return_GetSlipMasterRecord>();
                if (d4 != null)
                {
                    if (d4.Count > 0)
                    {
                        d5 = BASE._Voucher_DBOps.GetSlipMAsterRecord(d4[0].TR_SLIP_ID);
                    }
                }
                if (d1 == null)
                {
                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                model.Txt_V_Date_Donation = d1[0].TR_DATE; ;
                if (BASE.AllowMultiuser())
                {
                    string viewstr = "";
                    if (model.ActionMethod == Navigation_Mode._View)
                    {
                        viewstr = "view";
                    }
                    if (model.Info_LastEditedOn != d1[0].REC_EDIT_ON)
                    {
                        string message = Messages.RecordChanged("Current Donation", viewstr);
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                    }
                }

                DataTable d2 = BASE._Donation_DBOps.GetPurposeRecord(model.xID);
                DataTable d3 = BASE._Donation_DBOps.GetReceiptPrintRecord(model.xID);
                if (d2 == null || d3 == null)
                {
                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                if (d1.Count <= 0 || d2.Rows.Count <= 0)
                {
                    string message = Messages.No_InvalidData;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                Data_Binding(ref model, d1, d2, d3, d4, d5);
            }
            if (model.ActionMethod == Navigation_Mode._View)
            {
                model.Text = "View ~ " + model.TitleX;
            }
            else if (model.ActionMethod == Navigation_Mode._Delete)
            {
                model.Text = "Delete ~ " + model.TitleX;
            }
            else if (model.ActionMethod == Navigation_Mode._New_From_Selection)
            {
                model.iSpecific_ItemID = iSpecific_ItemID;
                model.GLookUp_ItemList_Donation = iSpecific_ItemID;
            }
            model.Txt_Ref_No_Donation = model.Txt_Ref_No_Donation.HandleEscapeCharacters();
            return View(model);
        }
        public void Data_Binding(ref DonationVoucher model, List<Return_DonationGetRecord> d1, DataTable d2, DataTable d3, List<Return_DonationGetSlipRecord> d4, List<Return_GetSlipMasterRecord> d5)
        {
            model.LastEditedOn = d1[0].REC_EDIT_ON;
            model.Txt_V_NO_Donation = d1[0].TR_VNO;
            if (d3.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(d3.Rows[0]["DR_NO"]))
                {
                    model.BE_Receipt_No_Donation = (string)d3.Rows[0]["DR_NO"];
                }
            }
            model.Cmd_Mode_Donation = d1[0].TR_MODE;
            model.Txt_Amount_Donation = (double)d1[0].TR_AMOUNT;
            if (d1[0].TR_ITEM_ID.Length > 0)
            {
                model.GLookUp_ItemList_Donation = d1[0].TR_ITEM_ID;
            }
            string Bank_ID = "";
            if (d1[0].TR_TYPE.ToUpper() == "DEBIT")
            {
                if (!Convert.IsDBNull(d1[0].TR_SUB_CR_LED_ID) && d1[0].TR_CR_LED_ID == "00079")
                {
                    Bank_ID = d1[0].TR_SUB_CR_LED_ID;
                }
            }
            else if (!Convert.IsDBNull(d1[0].TR_SUB_DR_LED_ID) && d1[0].TR_DR_LED_ID == "00079")
            {
                Bank_ID = d1[0].TR_SUB_DR_LED_ID;
            }
            if (!string.IsNullOrEmpty(Bank_ID))
            {
                model.GLookUp_BankList_Donation = Bank_ID;
            }
            if (!string.IsNullOrEmpty(d1[0].TR_AB_ID_1))
            {
                model.GLookUp_PartyList_Donation = d1[0].TR_AB_ID_1;
            }
            if (!string.IsNullOrEmpty(d1[0].TR_REF_BANK_ID))
            {
                model.GLookUp_RefBankList_Donation = d1[0].TR_REF_BANK_ID;
            }
            model.Txt_Ref_Branch_Donation = d1[0].TR_REF_BRANCH;
            model.Txt_Ref_No_Donation = d1[0].TR_REF_NO;
            if (!Convert.IsDBNull(d1[0].TR_REF_DATE))
            {
                model.Txt_Ref_Date_Donation = d1[0].TR_REF_DATE;
            }
            if (!Convert.IsDBNull(d1[0].TR_REF_CDATE))
            {
                model.Txt_Ref_CDate_Donation = d1[0].TR_REF_CDATE;
            }
            if (!Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_PurList_Donation = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }
            model.Txt_Narration_Donation = d1[0].TR_NARRATION;
            model.Txt_Remarks_Donation = d1[0].TR_REMARKS;
            model.Txt_Reference_Donation = d1[0].TR_REFERENCE;
            if (d5 != null)
            {
                if (d5.Count > 0)
                {
                    model.Txt_Slip_No_Donation = d5[0].SL_NO;
                }
            }
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Donation_R(DonationVoucher model)
        {
            model.ActionMethod = (Navigation_Mode)Enum.Parse(typeof(Navigation_Mode), model.TempActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrEmpty(model.GLookUp_BankList_Donation))
                    {
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList_Donation, "");
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
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ActionMethod == Navigation_Mode._Edit || model.ActionMethod == Navigation_Mode._Delete)
                    {
                        var donation_DbOps = BASE._Donation_DBOps.GetRecord(model.xID);
                        if (donation_DbOps == null)
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
                        if (donation_DbOps.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Donation");
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != donation_DbOps[0].REC_EDIT_ON)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Donation");
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._Donation_DBOps.GetStatus(model.xID);
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
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // Special Checks 
                        string xTemp_D_Status = "";
                        DataTable Status = BASE._Voucher_DBOps.GetDonationStatus(model.xID);
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
                        if (Status.Rows.Count > 0)
                        {
                            xTemp_D_Status = Status.Rows[0]["DS_STATUS"].ToString();
                        }
                        //                                                   "Donation Accepted"                                          "Receipt Request Rejected"                                        "Receipt Cancelled"
                        if ((xTemp_D_Status != "") && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted...!<br><br>Note: Donation Status Changed. Check Donation Register..!!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.ActionMethod == Navigation_Mode._New || model.ActionMethod == Navigation_Mode._Edit || model.ActionMethod == Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.GLookUp_ItemList_Donation))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_ItemList_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._open_Year_ID < 2122)
                    {
                        if (model.GLookUp_ItemList_Donation == "46f72cc7-14b7-4758-affc-e1952bfb469a" && string.IsNullOrWhiteSpace(model.BE_PAN_No_Donation))
                        {
                            jsonParam.message = "PAN is Compulsory for CSR donation...!";
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
                    if (BASE._open_Year_ID >= 2627)
                    {
                        if (model.Cmd_Mode_Donation == "CASH TO BANK")
                        {
                            jsonParam.message = "Income Tax के AIS में दिखाया गया `Cash Deposit in Bank` का अमाउंट हमारी Books of Accounts में दिखाए गए `Cash Deposit in Bank` से match होना अनिवार्य है।<br/>"+
                                                "इसी कारण अब “Cash to Bank” mode उपलब्ध नहीं है। क्यूँकि यह कैश एवं बैंक दोनों का mix mode है।<br/>" +
                                                "➡️ जो cash donation donor द्वारा direct centre के bank account में जमा किया गया है:<br/>" +
                                                "उस donation की entry को Cash mode में करें।<br/> " +
                                                "एवं<br/>" +
                                                "उसी date पर same amount की entry “Cash Deposit in Bank” में भी करें।!";
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

                    if (BASE._open_Year_ID >= 2122 && BASE._open_Ins_ID != "00001" && BASE._open_Ins_ID != "00005")
                    {
                        if (model.GLookUp_ItemList_Donation == "46f72cc7-14b7-4758-affc-e1952bfb469a" && string.IsNullOrWhiteSpace(model.BE_PAN_No_Donation))
                        {
                            jsonParam.message = "PAN is Compulsory for CSR donation...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "BE_PAN_No_Donation";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.GLookUp_ItemList_Donation != "46f72cc7-14b7-4758-affc-e1952bfb469a" && string.IsNullOrWhiteSpace(model.BE_PAN_No_Donation) && string.IsNullOrWhiteSpace(model.BE_AADHAAR_No_Donation) && string.IsNullOrWhiteSpace(model.BE_ID_No_Donation))
                        {
                            jsonParam.message = "Atleast one of the PAN No./Aadhar No./Passport No./Driving License/Voter ID/Ration Card No. is Compulsory for Regular donation. . . !";
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
                    if (IsDate(model.Txt_V_Date_Donation) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_V_Date_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_Donation) == true)
                    {
                        if (model.Txt_V_Date_Donation < BASE._open_Year_Sdt || model.Txt_V_Date_Donation > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Date Not As Per Financial Year...!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_V_Date_Donation";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_PartyList_Donation))
                    {
                        jsonParam.message = "Donor Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_PartyList_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.BE_Country_Donation))
                    {
                        jsonParam.message = "Donor Address Incomplete...!<br>Mandatory: Country...";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_PartyList_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (model.BE_Country_Donation.ToUpper().Equals("INDIA"))
                        {
                            if (string.IsNullOrEmpty(model.BE_Add1_Donation) || string.IsNullOrEmpty(model.BE_City_Donation) || string.IsNullOrEmpty(model.BE_District_Donation) || string.IsNullOrEmpty(model.BE_State_Donation) || string.IsNullOrEmpty(model.BE_Country_Donation))
                            {
                                jsonParam.message = "Donor Address Incomplete...!<br>Mandatory: Address Line.1, City, District, State & Country...";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "GLookUp_PartyList_Donation";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(model.BE_Add1_Donation) || string.IsNullOrEmpty(model.BE_Country_Donation))
                            {
                                jsonParam.message = "Donor Address Incomplete...!<br>Mandatory: Address Line.1 & Country...";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "GLookUp_PartyList_Donation";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmd_Mode_Donation))
                    {
                        jsonParam.message = "MOde Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Cmd_Mode_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Amount_Donation <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Amount_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //if (BASE._open_Cen_ID != 4216 && BASE._open_Cen_ID != 4218)
                    //{
                    //    if (string.IsNullOrEmpty(model.GLookUp_RefBankList_Donation) && (model.Cmd_Mode_Donation != "CASH" && model.Cmd_Mode_Donation != "MO" && model.Cmd_Mode_Donation != "CASH TO BANK"))
                    //    {
                    //        jsonParam.message = "Bank Not Selected...!";
                    //        jsonParam.result = false;
                    //        jsonParam.closeform = false;
                    //        jsonParam.title = "Incomplete Information...";
                    //        jsonParam.focusid = "GLookUp_RefBankList_Donation";
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }

                    //    if (string.IsNullOrEmpty(model.Txt_Ref_Branch_Donation) && (model.Cmd_Mode_Donation != "CASH" && model.Cmd_Mode_Donation != "MO" && model.Cmd_Mode_Donation != "CASH TO BANK"))
                    //    {
                    //        jsonParam.message = "Bank Branch Not Specified...!";
                    //        jsonParam.result = false;
                    //        jsonParam.closeform = false;
                    //        jsonParam.title = "Incomplete Information...";
                    //        jsonParam.focusid = "Txt_Ref_Branch_Donation";
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    model.GLookUp_RefBankList_Donation = model.GLookUp_RefBankList_Donation ?? "";
                    model.Txt_Ref_Branch_Donation = model.Txt_Ref_Branch_Donation ?? "";
                    if (string.IsNullOrEmpty(model.Txt_Ref_No_Donation) && (model.Cmd_Mode_Donation != "CASH" && model.Cmd_Mode_Donation != "MO"))
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Ref_No_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    model.Txt_Ref_No_Donation = model.Txt_Ref_No_Donation ?? "";
                    //if (IsDate(model.Txt_Ref_Date_Donation) == false && (model.Cmd_Mode_Donation.ToUpper() != "CASH") && (model.Cmd_Mode_Donation.ToUpper() != "MO"))
                    //{
                    //    jsonParam.message = "Date Incorrect/Blank...!";
                    //    jsonParam.result = false;
                    //    jsonParam.closeform = false;
                    //    jsonParam.title = "Incomplete Information...";
                    //    jsonParam.focusid = "Txt_Ref_Date_Donation";
                    //    return Json(new
                    //    {
                    //        jsonParam
                    //    }, JsonRequestBehavior.AllowGet);
                    //}
                    if (string.IsNullOrEmpty(model.GLookUp_BankList_Donation) && (model.Cmd_Mode_Donation != "CASH" && model.Cmd_Mode_Donation != "MO"))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_BankList_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    model.GLookUp_BankList_Donation = model.GLookUp_BankList_Donation ?? "";
                    if (string.IsNullOrEmpty(model.GLookUp_PurList_Donation))
                    {
                        jsonParam.message = "Purpose Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_PurList_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Slip_No_Donation > 0)
                    {
                        DataTable BankAccIDTable = BASE._DepositSlipsDBOps.GetList((int)model.Txt_Slip_No_Donation, model.GLookUp_BankList_Donation);
                        if (BankAccIDTable.Rows.Count > 0)
                        { // --Slip Exists
                            if (!Convert.IsDBNull(BankAccIDTable.Rows[0]["BA_REC_ID"]))
                            {
                                if (BankAccIDTable.Rows[0]["BA_REC_ID"].ToString() != model.GLookUp_BankList_Donation)
                                {
                                    jsonParam.message = "Selected slip has transaction of other bank...!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = false;
                                    jsonParam.title = "Incomplete Information...";
                                    jsonParam.focusid = "Txt_Slip_No_Donation";
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
                                jsonParam.focusid = "Txt_Slip_No_Donation";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else if (((model.Cmd_Mode_Donation.ToString().ToUpper() != "CASH") && (model.Cmd_Mode_Donation.ToString().ToUpper() != "MO")))
                    {
                        jsonParam.message = "Slip No Cannot be Negative Or Zero...!";
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Slip_No_Donation";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_Ref_CDate_Donation) == true)
                    {
                        if (model.Txt_Ref_Date_Donation > model.Txt_Ref_CDate_Donation)
                        {
                            jsonParam.message = "Clearing Date cannot be less than Reference Date!!";
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_Ref_CDate_Donation";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // ----------------------// Start Dependencies //----------------------
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._New_From_Selection || model.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        // Address Book(Donor) dependency check #Ref Z32

                        if (model.GLookUp_PartyList_Donation.Length > 0)
                        {
                            var d1 = BASE._Donation_DBOps.GetPartyDetails(false, model.GLookUp_PartyList_Donation);
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
                            var oldEditOn = model.Donor_RecEditOn;
                            if (d1.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var NewEditOn = d1[0].REC_EDIT_ON;
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (string.IsNullOrEmpty(d1[0].CO_NAME))
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // Check for full address of donor
                            if (d1[0].CO_NAME.ToUpper().Equals("INDIA"))
                            {
                                if (d1[0].C_R_ADD1.Length <= 0 || d1[0].CI_NAME.Length <= 0 || d1[0].ST_NAME.Length <= 0 || d1[0].DI_NAME.Length <= 0 || d1[0].CO_NAME.Length <= 0)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (d1[0].C_R_ADD1.Length <= 0 || d1[0].CO_NAME.Length <= 0)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        // Bank Account Dependency Check #Ref G32

                        if (!string.IsNullOrEmpty(model.GLookUp_BankList_Donation))
                        {
                            var BankAcc = BASE._Donation_DBOps.GetBankAccounts(false, model.GLookUp_BankList_Donation);
                            var oldEditOn = model.DepositBank_RecEditOn;
                            if (BankAcc.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var NewEditOn = BankAcc[0].REC_EDIT_ON;
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
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
                // -------------------------// End Dependencies //----------------------------

                // CHECKING LOCK STATUS

                string Old_Status_ID = "";
                if (model.ActionMethod == Navigation_Mode._Edit)
                {
                    object MaxValue = 0;
                    MaxValue = BASE._Donation_DBOps.GetStatus(model.xID);
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
                    // CHECKING OLD STATUS ID
                    MaxValue = 0;
                    MaxValue = BASE._Donation_DBOps.GetOldStatusID(model.xID);
                    if (Convert.IsDBNull(MaxValue))
                    {
                        Old_Status_ID = "";
                    }
                    else
                    {
                        Old_Status_ID = (string)MaxValue;
                    }
                }
                string Status_Action = ((int)Record_Status._Completed).ToString();
                if (model.ActionMethod == Navigation_Mode._Delete)
                {
                    Status_Action = ((int)Record_Status._Deleted).ToString();
                }
                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Dr_Led_ID = "";
                string Sub_Cr_Led_ID = "";
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.iLed_ID;
                    if (model.Cmd_Mode_Donation.ToUpper() == "CASH" || model.Cmd_Mode_Donation.ToUpper() == "MO")
                    {
                        Cr_Led_id = "00080"; //cash acc
                    }
                    else
                    {
                        Cr_Led_id = "00079";   // Bank A/c.
                        Sub_Cr_Led_ID = model.GLookUp_BankList_Donation;
                    }
                }
                else
                {
                    Cr_Led_id = model.iLed_ID;
                    if (model.Cmd_Mode_Donation.ToUpper() == "CASH" || model.Cmd_Mode_Donation.ToUpper() == "MO")
                    {
                        Dr_Led_id = "00080"; //cash acc
                    }
                    else
                    {
                        Dr_Led_id = "00079";       // Bank A/c.
                        Sub_Dr_Led_ID = model.GLookUp_BankList_Donation;
                    }
                }
                Param_Txn_Insert_VoucherDonation InNewParam = new Param_Txn_Insert_VoucherDonation();
                if (model.ActionMethod == Navigation_Mode._New || model.ActionMethod == Navigation_Mode._New_From_Selection)
                {
                    model.xID = Guid.NewGuid().ToString();
                    Parameter_Insert_Voucher_Donation InParam = new Parameter_Insert_Voucher_Donation();
                    InParam.TransCode = (int)Voucher_Screen_Code.Donation_Regular;
                    InParam.VNo = model.Txt_V_NO_Donation == null ? "" : model.Txt_V_NO_Donation;
                    if (IsDate(model.Txt_V_Date_Donation))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.Txt_V_Date_Donation.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_Donation;
                    InParam.Type = model.iTrans_Type;
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam.Mode = model.Cmd_Mode_Donation;
                    InParam.RefBankID = model.GLookUp_RefBankList_Donation == null ? "" : model.GLookUp_RefBankList_Donation;
                    InParam.RefBranch = model.Txt_Ref_Branch_Donation == null ? "" : model.Txt_Ref_Branch_Donation;
                    InParam.Ref_No = model.Txt_Ref_No_Donation == null ? "" : model.Txt_Ref_No_Donation;
                    if (IsDate(model.Txt_Ref_Date_Donation))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = model.Txt_Ref_Date_Donation.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_Donation))
                    {
                        InParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_ChequeDate = model.Txt_Ref_CDate_Donation.ToString();
                    }
                    InParam.Amount = (decimal)model.Txt_Amount_Donation;
                    InParam.DonorID = model.GLookUp_PartyList_Donation;
                    InParam.Narration = model.Txt_Narration_Donation == null ? "" : model.Txt_Narration_Donation;
                    InParam.Remarks = model.Txt_Remarks_Donation == null ? "" : model.Txt_Remarks_Donation;
                    InParam.Reference = model.Txt_Reference_Donation == null ? "" : model.Txt_Reference_Donation;
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = model.xID;
                    InNewParam.param_Insert = InParam;

                    Parameter_InsertPurpose_Voucher_Donation InPurpose = new Parameter_InsertPurpose_Voucher_Donation();
                    InPurpose.TxnID = model.xID;
                    InPurpose.PurposeID = model.GLookUp_PurList_Donation;
                    InPurpose.Amount = (decimal)model.Txt_Amount_Donation;
                    InPurpose.Status_Action = Status_Action;
                    InPurpose.RecID = Guid.NewGuid().ToString();
                    InNewParam.param_InsertPurpose = InPurpose;

                    Parameter_InsertDonStatus_Voucher_Donation InDonStatus = new Parameter_InsertDonStatus_Voucher_Donation();
                    InDonStatus.TxnID = model.xID;
                    InDonStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";
                    InDonStatus.Status_Action = Status_Action;
                    InDonStatus.RecID = Guid.NewGuid().ToString();
                    InNewParam.param_InsertDonStatus = InDonStatus;

                    if (model.Txt_Slip_No_Donation > 0)
                    {
                        Parameter_InsertSlip_VoucherDonation inSlip = new Parameter_InsertSlip_VoucherDonation();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        inSlip.SlipNo = (int)model.Txt_Slip_No_Donation;
                        inSlip.TxnID = model.xID;
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        InNewParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Insert Process
                    if (model.DnR_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DnR_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Donation_DBOps.InsertDonation_Txn(InNewParam))
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
                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.title = model.TitleX;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK = "Null" + model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Param_Txn_Update_VoucherDonation EditParam = new Param_Txn_Update_VoucherDonation();
                if (model.ActionMethod == Navigation_Mode._Edit)
                {
                    Parameter_Update_Voucher_Donation UpParam = new Parameter_Update_Voucher_Donation();
                    UpParam.VNo = model.Txt_V_NO_Donation == null ? "" : model.Txt_V_NO_Donation;
                    if (IsDate(model.Txt_V_Date_Donation))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.Txt_V_Date_Donation.ToString();
                    }
                    UpParam.ItemID = model.GLookUp_ItemList_Donation;
                    UpParam.Type = model.iTrans_Type;
                    UpParam.Cr_Led_ID = Cr_Led_id;
                    UpParam.Dr_Led_ID = Dr_Led_id;
                    UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    UpParam.Mode = model.Cmd_Mode_Donation;
                    UpParam.RefBankID = model.GLookUp_RefBankList_Donation == null ? "" : model.GLookUp_RefBankList_Donation;
                    UpParam.RefBranch = model.Txt_Ref_Branch_Donation == null ? "" : model.Txt_Ref_Branch_Donation;
                    UpParam.Ref_No = model.Txt_Ref_No_Donation == null ? "" : model.Txt_Ref_No_Donation;
                    if (IsDate(model.Txt_Ref_Date_Donation))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_Date = model.Txt_Ref_Date_Donation.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_Donation))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_Donation).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.Txt_Ref_CDate_Donation.ToString();
                    }
                    UpParam.Amount = (decimal)model.Txt_Amount_Donation;
                    UpParam.DonorID = model.GLookUp_PartyList_Donation;
                    UpParam.Narration = model.Txt_Narration_Donation == null ? "" : model.Txt_Narration_Donation;
                    UpParam.Remarks = model.Txt_Remarks_Donation == null ? "" : model.Txt_Remarks_Donation;
                    UpParam.Reference = model.Txt_Reference_Donation == null ? "" : model.Txt_Reference_Donation;
                    UpParam.RecID = model.xID;
                    EditParam.param_Update = UpParam;

                    Parameter_UpdatePurpose_Voucher_Donation UpPurpose = new Parameter_UpdatePurpose_Voucher_Donation();
                    UpPurpose.PurposeID = model.GLookUp_PurList_Donation;
                    UpPurpose.Amount = (decimal)model.Txt_Amount_Donation;
                    UpPurpose.RecID = model.xID;
                    EditParam.param_UpdatePurpose = UpPurpose;
                    if (Old_Status_ID.Length <= 0)
                    {
                        Parameter_InsertDonStatus_Voucher_Donation InDonStatus = new Parameter_InsertDonStatus_Voucher_Donation();
                        InDonStatus.TxnID = model.xID;
                        InDonStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";
                        InDonStatus.Status_Action = Status_Action;
                        InDonStatus.RecID = Guid.NewGuid().ToString();
                        EditParam.param_InsertDonSattus = InDonStatus;
                    }
                    else
                    {
                        Parameter_UpdateStatus_Voucher_Donation UpStatus = new Parameter_UpdateStatus_Voucher_Donation();
                        UpStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";
                        UpStatus.RecID = model.xID;
                        EditParam.param_UpdateStatus = UpStatus;
                    }
                    EditParam.ID_DeleteSlip = model.xID;
                    if (model.Txt_Slip_No_Donation > 0)
                    {
                        Parameter_InsertSlip_VoucherDonation inSlip = new Parameter_InsertSlip_VoucherDonation();
                        inSlip.RecID = Guid.NewGuid().ToString();
                        inSlip.SlipNo = (int)model.Txt_Slip_No_Donation;
                        inSlip.TxnID = model.xID;
                        inSlip.Dep_BA_ID = Sub_Dr_Led_ID;
                        EditParam.param_InsertSlip = inSlip;
                    }

                    //FCRA Update Process               
                    if (model.DnR_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DnR_SplVchrReferenceSelected.Split(',');
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


                    if (!BASE._Donation_DBOps.UpdateDonation_Txn(EditParam))
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
                    jsonParam.message = Messages.UpdateSuccess;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.title = model.TitleX;
                    return Json(new
                    {
                        jsonParam,
                        CashbookGridPK = "Null" + model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Param_Txn_Delete_VoucherDonation DelParam = new Param_Txn_Delete_VoucherDonation();
                if (model.ActionMethod == Navigation_Mode._Delete)
                {
                    DelParam.RecID_DeletePurpose = model.xID;
                    DelParam.RecID_DeleteStatus = model.xID;
                    DelParam.RecID_Delete = model.xID;
                    DelParam.ID_DeleteSlip = model.xID;
                    if (!BASE._Donation_DBOps.DeleteDonation_Txn(DelParam))
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
                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.title = model.TitleX;
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
                ModelState.AddModelError(string.Empty, jsonParam.message);
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult slipCountCheck(int SlipCount, int SlipNo, string BankId, string xID)
        {
            if (SlipCount < (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(SlipNo, BankId, ClientScreen.Accounts_Voucher_Donation, null, xID))
            {
                return Json(new
                {
                    message = "Some transaction(s) have been posted in deposit slip being used by you<br><br> Continue posting voucher using same deposit slip...?",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region "Start--> LookupEdit Events_R"
        public List<Return_DonationVoucherItemList> LookUp_GetItemList()
        {
            var d1 = BASE._Donation_DBOps.GetItemList();
            d1.Sort((x, y) => x.ITEM_NAME.CompareTo(y.ITEM_NAME));
            return d1;
        }
        public List<Return_DonationVoucherPartyList> LookUp_GetPartyList()
        {
            var d1 = BASE._Donation_DBOps.GetPartyDetails(false);
            d1.Sort((x, y) => x.C_NAME.CompareTo(y.C_NAME));
            return d1;
        }
        public ActionResult RefreshPartyList()
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetPartyList()), "application/json");
        }
        public List<Return_GetBankAccounts> LookUp_GetBankList()
        {
            var BA_Table = BASE._Donation_DBOps.GetBankAccounts();
            BA_Table.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            return BA_Table;
        }
        public List<Return_ReferenceBankList> LookUp_GetRefBankList()
        {
            var B2 = BASE._Donation_DBOps.GetBankList();
            B2.Sort((x, y) => x.BI_BANK_NAME.CompareTo(y.BI_BANK_NAME));
            return B2;
        }
        public List<Return_DonationVocuherPurpose> LookUp_GetPurposeList()
        {
            return BASE._Donation_DBOps.GetPurposes();
        }

        public ActionResult GetSlipNo_SlipCount(string BankList, string xID)
        {
            int Slip_No = (int)BASE._DepositSlipsDBOps.GetMaxOpenSlipNo(BankList, ClientScreen.Accounts_Voucher_Donation);
            int Slip_Count = (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(Slip_No, BankList, ClientScreen.Accounts_Voucher_Donation, null, xID);
            return Json(new
            {
                Slip_No,
                Slip_Count
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Txt_Slip_No_EditValueChanged(string BankList, int Slip_No, string xID)
        {
            int Slip_Count = (int)BASE._DepositSlipsDBOps.GetSlipTxnCount(Slip_No, BankList, ClientScreen.Accounts_Voucher_Donation, null, xID);
            return Json(new
            {
                Slip_Count
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Donation_F
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Donation_F(string Tag = "", string xID = "", string Info_LastEditedOn = "", string iSpecific_ItemID = "", string GridToRefresh = "CashBookListGrid")
        {
            ForeignDonationV model = new ForeignDonationV();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = model.Tag.ToString();

            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            Common.Navigation_Mode[] AM = { Common.Navigation_Mode._New, Common.Navigation_Mode._New_From_Selection, Common.Navigation_Mode._Edit, Common.Navigation_Mode._View, Common.Navigation_Mode._Delete };
            for (int i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, Rights[i]) && model.Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            ViewData["DonationVou_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["DonationVou_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            model.xID_F = xID;
            model.TitleX_F = "Foreign Donation";
            RefreshItemList_ForeignDonation_F();
            RefreshPartyList_ForeignDonation_F();
            RefreshGetBankList_ForeignDonation_F();
            RefreshGetRefBankList_ForeignDonation_F();
            RefreshGetPurList_ForeignDonation_F();
            RefreshGetCurList_ForeignDonation_F();
            RefreshGetCatList_ForeignDonation_F();
            model.Cmd_Mode_Donation_F = "CHEQUE";

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_DnF = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_DnF = model.SpecialReferenceList_Data_DnF.Count();

            if (model.Tag == Navigation_Mode._New_From_Selection || model.Tag == Navigation_Mode._New)
            {
                model.Text_F = "New ~ " + model.TitleX_F;
            }
            if (model.Tag == Navigation_Mode._Edit || model.Tag == Navigation_Mode._View || model.Tag == Navigation_Mode._Delete)
            {
                model.Text_F = "Edit ~ " + model.TitleX_F;
                model.Info_LastEditedOn_F = Convert.ToDateTime(Info_LastEditedOn);
                var d1 = BASE._Donation_DBOps.GetRecord(model.xID_F);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }

                DateTime? xDate = null;
                xDate = d1[0].TR_DATE;
                model.Txt_V_Date_Donation_F = xDate;

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_DnF = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                //-----------------------------+
                //Start : Check if entry already changed 
                //-----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Navigation_Mode._Edit || model.Tag == Navigation_Mode._View || model.Tag == Navigation_Mode._Delete)
                    {
                        string viewstr = "";
                        if (model.Tag == Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (model.Info_LastEditedOn_F != Convert.ToDateTime(d1[0].REC_EDIT_ON))
                        {
                            string message = Messages.RecordChanged("Current Donation", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "', 'Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                //-----------------------------+
                //End : Check if entry already changed 
                //-----------------------------+
                DataTable d2 = BASE._Donation_DBOps.GetPurposeRecord(model.xID_F);
                DataTable d3 = BASE._Donation_DBOps.GetForeignRecord(model.xID_F);
                DataTable d4 = BASE._Donation_DBOps.GetReceiptPrintRecord(model.xID_F);

                if (d2 == null || d3 == null || d4 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                Data_Binding_F(d1, d2, d3, d4, ref model);
            }
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Text_F = "Delete ~ " + model.TitleX_F;
            }
            if (model.Tag == Common.Navigation_Mode._View)
            {
                model.Text_F = "View ~ " + model.TitleX_F;
            }
            if (model.Tag == Navigation_Mode._New_From_Selection)
            {
                model.iSpecific_ItemID_F = iSpecific_ItemID;
                model.GLookUp_ItemList_Donation_F = iSpecific_ItemID;
            }
            return View(model);
        }
        public void Data_Binding_F(List<Return_DonationGetRecord> d1, DataTable d2, DataTable d3, DataTable d4, ref ForeignDonationV model)
        {
            DateTime? xDate = null;
            model.LastEditedOn_F = Convert.ToDateTime(d1[0].REC_EDIT_ON);
            model.Txt_V_NO_Donation_F = d1[0].TR_VNO;

            if (d4.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(d4.Rows[0]["DR_NO"])))
                {
                    model.BE_Receipt_No_Donation_F = Convert.ToString(d4.Rows[0]["DR_NO"]);
                }
            }
            model.Cmd_Mode_Donation_F = Convert.ToString(d1[0].TR_MODE);
            if (!string.IsNullOrEmpty(d1[0].TR_ITEM_ID) && d1[0].TR_ITEM_ID.Length > 0)
            {
                model.GLookUp_ItemList_Donation_F = d1[0].TR_ITEM_ID;
            }
            string Bank_ID = "";
            if (d1[0].TR_TYPE.ToUpper() == "DEBIT")
            {
                if (!string.IsNullOrWhiteSpace(d1[0].TR_SUB_CR_LED_ID) && d1[0].TR_CR_LED_ID == "00079")
                {
                    Bank_ID = d1[0].TR_SUB_CR_LED_ID;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(d1[0].TR_SUB_DR_LED_ID) && d1[0].TR_DR_LED_ID.ToString() == "00079")
                {
                    Bank_ID = d1[0].TR_SUB_DR_LED_ID;
                }
            }
            if (Bank_ID.Length > 0)
            {
                model.GLookUp_BankList_Donation_F = Bank_ID;
            }

            if (!string.IsNullOrEmpty(d1[0].TR_AB_ID_1) && d1[0].TR_AB_ID_1.Length > 0)
            {
                model.GLookUp_PartyList_Donation_F = d1[0].TR_AB_ID_1;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_D_CAT_MISC_ID"]))
            {
                if (d3.Rows[0]["TR_D_CAT_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_CatList_Donation_F = d3.Rows[0]["TR_D_CAT_MISC_ID"].ToString();
                }
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_CUR_ID"]))
            {
                if (d3.Rows[0]["TR_CUR_ID"].ToString().Length > 0)
                {
                    model.GLookUp_CurList_Donation_F = d3.Rows[0]["TR_CUR_ID"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(d1[0].TR_REF_BANK_ID) && d1[0].TR_REF_BANK_ID.Length > 0)
            {
                model.GLookUp_RefBankList_Donation_F = d1[0].TR_REF_BANK_ID;
            }
            model.Txt_Ref_Branch_Donation_F = d1[0].TR_REF_BRANCH.HandleEscapeCharacters();
            model.Txt_Ref_No_Donation_F = d1[0].TR_REF_NO.HandleEscapeCharacters();
            if (d1[0].TR_REF_DATE != null)
            {
                model.Txt_Ref_Date_Donation_F = d1[0].TR_REF_DATE;
            }
            if (d1[0].TR_REF_CDATE != null)
            {
                model.Txt_Ref_CDate_Donation_F = d1[0].TR_REF_CDATE;
            }
            model.Txt_Co_Bank_Donation_F = d3.Rows[0]["TR_CO_BANK"].ToString();
            model.Txt_Co_Branch_Donation_F = d3.Rows[0]["TR_CO_BRANCH"].ToString();

            if (!Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_PurList_Donation_F = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }

            model.Txt_Narration_Donation_F = d1[0].TR_NARRATION.HandleEscapeCharacters() ?? "";
            model.Txt_Remarks_Donation_F = d1[0].TR_REMARKS.HandleEscapeCharacters() ?? "";
            model.Txt_Reference_Donation_F = d1[0].TR_REFERENCE.HandleEscapeCharacters() ?? "";

            model.Txt_Foreign_Amt_Donation_F = Convert.ToDecimal(d3.Rows[0]["TR_FOREIGN_AMT"]);
            model.Txt_Cur_Rate_Donation_F = Convert.ToDecimal(d3.Rows[0]["TR_CUR_RATE"]);
            model.Txt_INR_Amt_Donation_F = Convert.ToDecimal(d3.Rows[0]["TR_INR_AMT"]);
            model.Txt_Bank_Charges_Donation_F = Convert.ToDecimal(d3.Rows[0]["TR_BANK_CHARGES"]);
            model.Txt_Amount_Donation_F = Convert.ToDecimal(d3.Rows[0]["TR_NET_AMT"]);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Donation_Window_F(ForeignDonationV model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrWhiteSpace(model.GLookUp_BankList_Donation_F))
                    {
                        object AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList_Donation_F, "");
                        if (Convert.IsDBNull(AccNo))
                        {
                            AccNo = "";
                        }
                        if (AccNo.ToString().Length > 0)
                        {
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted. . . !" + "<br>" + "<br>" + "In this entry, Used Bank A/c No.: " + AccNo.ToString() + " was closed...!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        var donationF_DbOps = BASE._Donation_DBOps.GetRecord(model.xID_F);
                        if (donationF_DbOps == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (donationF_DbOps.Count() == 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn_F != Convert.ToDateTime(donationF_DbOps[0].REC_EDIT_ON))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        object MaxValue = BASE._Donation_DBOps.GetStatus(model.xID_F);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found . . . !";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted . . . !" + "<br>" + "<br>" + "Note:" + "<br>" + "-------" + "<br>" + "Drop your Request to Madhuban for Unlock this Entry," + "<br>" + "If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        //Special Checks 

                        string xTemp_D_Status = "";
                        DataTable Status = BASE._Voucher_DBOps.GetDonationStatus(model.xID_F);
                        if (Status == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status.Rows.Count > 0)
                        {
                            xTemp_D_Status = Status.Rows[0]["DS_STATUS"].ToString();
                        }
                        if ((xTemp_D_Status != "") && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))        //'--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted. . . !" + "<br>" + "<br>" + "Note: Donation Status Changed, Check Donation Register..!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                //-----------------------------+
                //End : Check if entry already changed 
                //-----------------------------+

                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (model.Txt_INR_Amt_Donation_F.ToString().Length > 20)
                    {
                        jsonParam.message = "Amount too Big . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_INR_Amt_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ItemList_Donation_F))
                    {
                        jsonParam.message = "Item Name Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_Donation_F) == false)
                    {
                        jsonParam.message = "Voucher Date Incorrect/Blank . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_V_Date_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(Convert.ToDateTime(model.Txt_V_Date_Donation_F)))
                    {
                        if (Convert.ToDateTime(model.Txt_V_Date_Donation_F) < Convert.ToDateTime(BASE._open_Year_Sdt) || Convert.ToDateTime(model.Txt_V_Date_Donation_F) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Voucher Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_V_Date_Donation_F";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PartyList_Donation_F))
                    {
                        jsonParam.message = "Donor Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.BE_Country_Donation_F))
                    {
                        jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1 & Country...";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (model.BE_Country_Donation_F.ToUpper() == "INDIA")
                        {
                            if (string.IsNullOrWhiteSpace(model.BE_Add1_Donation_F) || string.IsNullOrWhiteSpace(model.BE_City_Donation_F) || string.IsNullOrWhiteSpace(model.BE_District_Donation_F) || string.IsNullOrWhiteSpace(model.BE_State_Donation_F))
                            {
                                jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1, City, District, State & Country...";
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.result = false;
                                jsonParam.focusid = "GLookUp_PartyList_Donation_F";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(model.BE_Add1_Donation_F))
                            {
                                jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1 & Country...";
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.result = false;
                                jsonParam.focusid = "GLookUp_PartyList_Donation_F";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (BASE._open_Year_ID >= 2122 && BASE._open_Ins_ID != "00001" && BASE._open_Ins_ID != "00005")
                    {
                        if (string.IsNullOrWhiteSpace(model.BE_Passport_No_Donation_F) && string.IsNullOrWhiteSpace(model.BE_TAXATION_No_Donation_F))
                        {
                            jsonParam.message = "Either Passport No. or Taxpayer Identification No. is Compulsory for Foreign donation. . . !";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "BE_Passport_No_Donation_F";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_CatList_Donation_F))
                    {
                        jsonParam.message = "Donor Category Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_CatList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_CurList_Donation_F))
                    {
                        jsonParam.message = "Currency Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_CurList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_BankList_Donation_F))
                    {
                        jsonParam.message = "Deposited Bank Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDouble(model.Txt_Foreign_Amt_Donation_F) <= 0)
                    {
                        jsonParam.message = "Foreign Currency Amount cannot be Zero/Negative . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Foreign_Amt_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDouble(model.Txt_Cur_Rate_Donation_F) <= 0)
                    {
                        jsonParam.message = "Currency Rate cannot be Zero/Negative. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Cur_Rate_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDouble(model.Txt_Bank_Charges_Donation_F) < 0)
                    {
                        jsonParam.message = "Bank Charges Amount cannot be Negative. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Bank_Charges_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //if (Convert.ToDouble(model.Txt_Amount_Donation_F)<=0)
                    //{
                    //    jsonParam.message = "Amount cannot be blank. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.result = false;
                    //    jsonParam.focusid = "Txt_Amount_Donation_F";
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}
                    if (Convert.ToDouble(model.Txt_Amount_Donation_F) <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PurList_Donation_F))
                    {
                        jsonParam.message = "Purpose Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PurList_Donation_F";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(Convert.ToDateTime(model.Txt_Ref_CDate_Donation_F)))
                    {
                        if (model.Txt_Ref_CDate_Donation_F < model.Txt_Ref_Date_Donation_F)
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate_Donation_F";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                //--------------------------// Start Dependencies //--------------------------
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)       //Address Book(Donor) dependency check #Ref Z32
                    {
                        if (!string.IsNullOrWhiteSpace(model.GLookUp_PartyList_Donation_F))
                        {
                            var d1 = BASE._Donation_DBOps.GetPartyDetails(false, model.GLookUp_PartyList_Donation_F);
                            if (d1 == null)
                            {
                                jsonParam.message = Common_Lib.Messages.SomeError;
                                jsonParam.title = "Error";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            DateTime oldEditOn = Convert.ToDateTime(model.PartyList_REC_EDIT_ON_F);
                            if (d1.Count <= 0)
                            {
                                jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1[0].REC_EDIT_ON);
                                if (oldEditOn != NewEditOn) //A/E,E/E
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            //'Check for full address of donor
                            if (string.IsNullOrWhiteSpace(d1[0].CO_NAME))
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // Check for full address of donor
                            if (d1[0].CO_NAME.ToUpper().Equals("INDIA"))
                            {
                                if (string.IsNullOrWhiteSpace(d1[0].C_R_ADD1) || string.IsNullOrWhiteSpace(d1[0].CI_NAME) || string.IsNullOrWhiteSpace(d1[0].ST_NAME) || string.IsNullOrWhiteSpace(d1[0].DI_NAME) || string.IsNullOrWhiteSpace(d1[0].CO_NAME))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(d1[0].C_R_ADD1) || string.IsNullOrWhiteSpace(d1[0].CO_NAME))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.GLookUp_BankList_Donation_F))
                        {
                            var BankAcc = BASE._Donation_DBOps.GetBankAccounts(true, model.GLookUp_BankList_Donation_F);
                            if (BankAcc == null)
                            {
                                jsonParam.message = Common_Lib.Messages.SomeError;
                                jsonParam.title = "Error";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                            DateTime oldEditOn = Convert.ToDateTime(model.BankList_REC_EDIT_ON_F);
                            if (BankAcc.Count <= 0)
                            {
                                jsonParam.message = Common_Lib.Messages.DependencyChanged("Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = BankAcc[0].REC_EDIT_ON;
                                if (oldEditOn != NewEditOn) //A/E,E/E
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                //---------------------------// End Dependencies //---------------------------------

                //CHECKING LOCK STATUS
                string Old_Status_ID = "";
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    Object MaxValue = 0;
                    MaxValue = BASE._Donation_DBOps.GetStatus(model.xID_F);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found. . . !";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    //CHECKING OLD STATUS ID

                    MaxValue = 0;
                    MaxValue = BASE._Donation_DBOps.GetOldStatusID(model.xID_F);
                    if (Convert.IsDBNull(MaxValue))
                    {
                        Old_Status_ID = "";
                    }
                    else
                    {
                        Old_Status_ID = MaxValue.ToString();
                    }
                }

                string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                if (model.Tag == Navigation_Mode._Delete)
                {
                    Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
                }

                string Dr_Led_id = "";
                string Cr_Led_id = "";
                string Sub_Dr_Led_ID = "";
                string Sub_Cr_Led_ID = "";
                if (model.iTrans_Type_F.ToUpper() == "DEBIT")
                {
                    Dr_Led_id = model.iLed_ID_F;
                    Cr_Led_id = "00079";
                    Sub_Cr_Led_ID = model.GLookUp_BankList_Donation_F; //Bank A/c.
                }
                else
                {
                    Cr_Led_id = model.iLed_ID_F;
                    Dr_Led_id = "00079";
                    Sub_Dr_Led_ID = model.GLookUp_BankList_Donation_F;  //Bank A/c.
                }

                Common_Lib.RealTimeService.Param_Txn_Insert_VoucherDonation InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherDonation();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection) //new
                {
                    model.xID_F = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_Voucher_Donation InParam = new Common_Lib.RealTimeService.Parameter_Insert_Voucher_Donation();
                    InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Foreign;
                    InParam.VNo = model.Txt_V_NO_Donation_F ?? "";

                    if (IsDate(Convert.ToDateTime(model.Txt_V_Date_Donation_F)))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = model.Txt_V_Date_Donation_F.ToString();
                    }

                    InParam.ItemID = model.GLookUp_ItemList_Donation_F ?? "";
                    InParam.Type = model.iTrans_Type_F ?? "";
                    InParam.Cr_Led_ID = Cr_Led_id ?? "";
                    InParam.Dr_Led_ID = Dr_Led_id ?? "";
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID ?? "";
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID ?? "";
                    InParam.Mode = model.Cmd_Mode_Donation_F ?? "";
                    InParam.RefBankID = model.GLookUp_RefBankList_Donation_F ?? "";
                    InParam.RefBranch = model.Txt_Ref_Branch_Donation_F ?? "";
                    InParam.Ref_No = model.Txt_Ref_No_Donation_F ?? "";

                    if (IsDate(model.Txt_Ref_Date_Donation_F))
                    {
                        InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_Date = model.Txt_Ref_Date_Donation_F.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_Donation_F))
                    {
                        InParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.Ref_ChequeDate = model.Txt_Ref_CDate_Donation_F.ToString();
                    }

                    InParam.Amount = Convert.ToDecimal(model.Txt_INR_Amt_Donation_F);
                    InParam.DonorID = model.GLookUp_PartyList_Donation_F ?? "";
                    InParam.Narration = model.Txt_Narration_Donation_F ?? "";
                    InParam.Remarks = model.Txt_Remarks_Donation_F ?? "";
                    InParam.Reference = model.Txt_Reference_Donation_F ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = model.xID_F ?? "";

                    InNewParam.param_Insert = InParam;

                    Common_Lib.RealTimeService.Parameter_InsertPurpose_Voucher_Donation InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_Voucher_Donation();
                    InPurpose.TxnID = model.xID_F ?? "";
                    InPurpose.PurposeID = model.GLookUp_PurList_Donation_F ?? "";
                    InPurpose.Amount = Convert.ToDecimal(model.Txt_Amount_Donation_F);
                    InPurpose.Status_Action = Status_Action;
                    InPurpose.RecID = System.Guid.NewGuid().ToString();

                    InNewParam.param_InsertPurpose = InPurpose;

                    Common_Lib.RealTimeService.Parameter_InsertForeignInfo_Voucher_Donation InFgnInfo = new Common_Lib.RealTimeService.Parameter_InsertForeignInfo_Voucher_Donation();
                    InFgnInfo.TxnID = model.xID_F ?? "";
                    InFgnInfo.CoBank = model.Txt_Co_Bank_Donation_F ?? "";
                    InFgnInfo.CoBranch = model.Txt_Co_Branch_Donation_F ?? "";
                    InFgnInfo.CurrID = model.GLookUp_CurList_Donation_F ?? "";
                    InFgnInfo.CurrRate = Convert.ToDecimal(model.Txt_Cur_Rate_Donation_F ?? 0);
                    InFgnInfo.ForeignAmount = Convert.ToDecimal(model.Txt_Foreign_Amt_Donation_F ?? 0);
                    InFgnInfo.INR = Convert.ToDecimal(model.Txt_INR_Amt_Donation_F ?? 0);
                    InFgnInfo.Bankcharges = Convert.ToDecimal(model.Txt_Bank_Charges_Donation_F ?? 0);
                    InFgnInfo.NettAmt = Convert.ToDecimal(model.Txt_Amount_Donation_F ?? 0);
                    InFgnInfo.CatID = model.GLookUp_CatList_Donation_F ?? "";
                    InFgnInfo.Status_Action = Status_Action;
                    InFgnInfo.RecID = System.Guid.NewGuid().ToString();

                    InNewParam.param_InsertFgnInfo = InFgnInfo;

                    Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation InDStatus = new Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation();
                    InDStatus.TxnID = model.xID_F ?? "";
                    InDStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";
                    InDStatus.Status_Action = Status_Action;
                    InDStatus.RecID = System.Guid.NewGuid().ToString();

                    InNewParam.param_InsertDonStatus = InDStatus;

                    //FCRA Insert Process
                    if (model.DnF_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DnF_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Donation_DBOps.InsertDonation_Txn(InNewParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Common_Lib.Messages.SaveSuccess;
                    jsonParam.title = model.TitleX_F;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = "Null" + model.xID_F }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_Txn_Update_VoucherDonation EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_VoucherDonation();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)  //edit
                {
                    Common_Lib.RealTimeService.Parameter_Update_Voucher_Donation UpParam = new Common_Lib.RealTimeService.Parameter_Update_Voucher_Donation();
                    UpParam.VNo = model.Txt_V_NO_Donation_F ?? "";
                    if (IsDate(model.Txt_V_Date_Donation_F))
                    {
                        UpParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.TDate = model.Txt_V_Date_Donation_F.ToString();
                    }

                    UpParam.ItemID = model.GLookUp_ItemList_Donation_F ?? "";
                    UpParam.Type = model.iTrans_Type_F ?? "";
                    UpParam.Cr_Led_ID = Cr_Led_id ?? "";
                    UpParam.Dr_Led_ID = Dr_Led_id ?? "";
                    UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID ?? "";
                    UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID ?? "";
                    UpParam.Mode = model.Cmd_Mode_Donation_F ?? "";
                    UpParam.RefBankID = model.GLookUp_RefBankList_Donation_F ?? "";
                    UpParam.RefBranch = model.Txt_Ref_Branch_Donation_F ?? "";
                    UpParam.Ref_No = model.Txt_Ref_No_Donation_F ?? "";
                    if (IsDate(model.Txt_Ref_Date_Donation_F))
                    {
                        UpParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_Date = model.Txt_Ref_Date_Donation_F.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_Donation_F))
                    {
                        UpParam.Ref_ChequeDate = Convert.ToDateTime(model.Txt_Ref_CDate_Donation_F).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpParam.Ref_ChequeDate = model.Txt_Ref_CDate_Donation_F.ToString();
                    }

                    UpParam.Amount = Convert.ToDecimal(model.Txt_INR_Amt_Donation_F ?? 0);
                    UpParam.DonorID = model.GLookUp_PartyList_Donation_F ?? "";
                    UpParam.Narration = model.Txt_Narration_Donation_F ?? "";
                    UpParam.Remarks = model.Txt_Remarks_Donation_F ?? "";
                    UpParam.Reference = model.Txt_Reference_Donation_F ?? "";

                    UpParam.RecID = model.xID_F ?? "";

                    EditParam.param_Update = UpParam;

                    Common_Lib.RealTimeService.Parameter_UpdatePurpose_Voucher_Donation UpPurpose = new Common_Lib.RealTimeService.Parameter_UpdatePurpose_Voucher_Donation();
                    UpPurpose.PurposeID = model.GLookUp_PurList_Donation_F ?? "";
                    UpPurpose.Amount = Convert.ToDecimal(model.Txt_Amount_Donation_F ?? 0);
                    UpPurpose.RecID = model.xID_F ?? "";

                    EditParam.param_UpdatePurpose = UpPurpose;

                    Common_Lib.RealTimeService.Parameter_UpdateForeignInfo_Voucher_Donation UpFgnInfo = new Common_Lib.RealTimeService.Parameter_UpdateForeignInfo_Voucher_Donation();
                    UpFgnInfo.TxnID = model.xID_F ?? "";
                    UpFgnInfo.CoBank = model.Txt_Co_Bank_Donation_F ?? "";
                    UpFgnInfo.CoBranch = model.Txt_Co_Branch_Donation_F ?? "";
                    UpFgnInfo.CurrID = model.GLookUp_CurList_Donation_F ?? "";
                    UpFgnInfo.CurrRate = Convert.ToDecimal(model.Txt_Cur_Rate_Donation_F ?? 0);
                    UpFgnInfo.ForeignAmount = Convert.ToDecimal(model.Txt_Foreign_Amt_Donation_F ?? 0);
                    UpFgnInfo.INR = Convert.ToDecimal(model.Txt_INR_Amt_Donation_F ?? 0);
                    UpFgnInfo.Bankcharges = Convert.ToDecimal(model.Txt_Bank_Charges_Donation_F ?? 0);
                    UpFgnInfo.NettAmt = Convert.ToDecimal(model.Txt_Amount_Donation_F ?? 0);
                    UpFgnInfo.CatID = model.GLookUp_CatList_Donation_F ?? "";

                    EditParam.param_UpdateFgnInfo = UpFgnInfo;

                    if (Old_Status_ID.Length <= 0)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation InStatus = new Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation();
                        InStatus.TxnID = model.xID_F ?? "";
                        InStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";
                        InStatus.Status_Action = Status_Action;
                        InStatus.RecID = System.Guid.NewGuid().ToString();

                        EditParam.param_InsertDonSattus = InStatus;
                    }
                    else
                    {
                        Common_Lib.RealTimeService.Parameter_UpdateStatus_Voucher_Donation UpStatus = new Common_Lib.RealTimeService.Parameter_UpdateStatus_Voucher_Donation();
                        UpStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c";

                        UpStatus.RecID = model.xID_F ?? "";

                        EditParam.param_UpdateStatus = UpStatus;
                    }


                    //FCRA Update Process               
                    if (model.DnF_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DnF_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Donation_DBOps.UpdateDonation_Txn(EditParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Common_Lib.Messages.UpdateSuccess;
                    jsonParam.title = model.TitleX_F;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = "Null" + model.xID_F }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_Txn_Delete_VoucherDonation DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_VoucherDonation();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)//DELETE
                {
                    DelParam.RecID_DeletePurpose = model.xID_F ?? "";
                    DelParam.RecID_DeleteFgnInfo = model.xID_F ?? "";
                    DelParam.RecID_DeleteStatus = model.xID_F ?? "";
                    DelParam.RecID_Delete = model.xID_F ?? "";

                    if (!BASE._Donation_DBOps.DeleteDonation_Txn(DelParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX_F;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
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

        #region LookupEdit Events_F
        public ActionResult LookUp_GetItemList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetItemList_ForeignDonation == null)
            {
                RefreshItemList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetItemList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshItemList_ForeignDonation_F()
        {
            GetItemList_ForeignDonation = BASE._Donation_DBOps.GetItemList(true);
            GetItemList_ForeignDonation = GetItemList_ForeignDonation.OrderBy(x => x.ITEM_NAME).ToList();
        }

        public ActionResult LookUp_GetPartyList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetPartyList_ForeignDonation == null)
            {
                RefreshPartyList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPartyList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshPartyList_ForeignDonation_F()
        {
            GetPartyList_ForeignDonation = BASE._Donation_DBOps.GetPartyDetails(false);
            GetPartyList_ForeignDonation = GetPartyList_ForeignDonation.OrderBy(x => x.C_NAME).ToList();
        }

        public ActionResult LookUp_GetCatList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetCatList_ForeignDonation == null)
            {
                RefreshGetCatList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetCatList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshGetCatList_ForeignDonation_F()
        {
            DataTable d1 = BASE._Donation_DBOps.GetCategories();
            DataView dview = new DataView(d1);
            GetCatList_ForeignDonation = DatatableToModel.DataTableto_GetCategoryList(dview.ToTable());
        }

        public ActionResult LookUp_GetBankList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetBankList_ForeignDonation == null)
            {
                RefreshGetBankList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetBankList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshGetBankList_ForeignDonation_F()
        {
            GetBankList_ForeignDonation = BASE._Donation_DBOps.GetBankAccounts(true);
            GetBankList_ForeignDonation = GetBankList_ForeignDonation.OrderBy(x => x.BA_BRANCH_ID).ToList();
        }

        public ActionResult LookUp_GetCurList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetCurList_ForeignDonation == null)
            {
                RefreshGetCurList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetCurList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshGetCurList_ForeignDonation_F()
        {
            DataTable d1 = BASE._Donation_DBOps.GetCurrencies();
            DataView dview = new DataView(d1);
            GetCurList_ForeignDonation = DatatableToModel.DataTableto_GetCurList(dview.ToTable());
        }

        public ActionResult LookUp_GetRefBankList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetRefBankList_ForeignDonation == null)
            {
                RefreshGetRefBankList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetRefBankList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshGetRefBankList_ForeignDonation_F()
        {
            GetRefBankList_ForeignDonation = BASE._Donation_DBOps.GetBankList();
        }

        public ActionResult LookUp_GetPurList_ForeignDonation_F(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || GetPurList_ForeignDonation == null)
            {
                RefreshGetPurList_ForeignDonation_F();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPurList_ForeignDonation, loadOptions)), "application/json");
        }
        public void RefreshGetPurList_ForeignDonation_F()
        {
            GetPurList_ForeignDonation = BASE._Donation_DBOps.GetPurposes();
        }
        #endregion

        #region Misc
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
            ClearBaseSession("_Donation");
            ClearBaseSession("_F");
        }
        #endregion
    }
}