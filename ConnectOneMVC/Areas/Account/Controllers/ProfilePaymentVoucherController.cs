using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class ProfilePaymentVoucherController : BaseController
    {
        private enum LB_Category
        {
            PURCHASED,
            PURCHASED_CONSTRUCTED,
            GIFTED,
            GIFTED_CONSTRUCTED,
            LEASED_Long_Term,
            LEASED_CONSTRUCTED_Long_Term,
            LEASED
        }
        public string Tr_M_ID_PayProfile
        {
            get
            {
                return (string)GetBaseSession("Tr_M_ID_Profile");
            }
            set
            {
                SetBaseSession("Tr_M_ID_Profile", value);
            }
        }       
        public byte[] Profile_Asset_Image
        {
            get
            {
                return (byte[])GetBaseSession("Payment_Asset_Image_Payment");
            }
            set
            {
                SetBaseSession("Payment_Asset_Image_Payment", value);
            }
        }          
        public List<Return_ReferenceType> WIP_ExistingData
        {
            get
            {
                return (List<Return_ReferenceType>)GetBaseSession("WIP_ExistingData_Profile");
            }
            set
            {
                SetBaseSession("WIP_ExistingData_Profile", value);
            }
        }    
        public List<Return_PropertyData> PropertyList
        {
            get
            {
                return (List<Return_PropertyData>)GetBaseSession("PropertyList_Profile");
            }
            set
            {
                SetBaseSession("PropertyList_Profile", value);
            }
        }      
        public List<Property_Window_ExtendedProperty_Grid> ExtendedProperty_GridData
        {
            get
            {
                return (List<Property_Window_ExtendedProperty_Grid>)GetBaseSession("ExtendedProperty_GridData_Profile");
            }
            set
            {
                SetBaseSession("ExtendedProperty_GridData_Profile", value);
            }
        }     
        public DataTable Txn_Report_Data
        {
            get {return (DataTable)GetBaseSession("Txn_Report_Data_Profile"); }
            set {SetBaseSession("Txn_Report_Data_Profile",value); }
        }
        #region Property_Select
        public ActionResult Frm_Property_Select(string LB_REC_ID, string Txn_M_ID,bool FromDNK=false)
        {
            var _db_table = BASE._L_B_DBOps.GetListForExpenses(Txn_M_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment);
            if (_db_table == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Frm_Voucher_Item_Content_popup','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            else
            {
                PropertyList = _db_table;
            }
            ViewBag.LB_REC_ID = LB_REC_ID;
            ViewBag.FromDNK = FromDNK;            
            return View(_db_table);
        }
        public ActionResult Frm_Property_Select_Grid()
        {
            return View(PropertyList);
        }
        public ActionResult PropertyListCustomDataAction(string key)
        {

            var Data = PropertyList;
            var it = Data != null ? Data.Where(f => f.REC_ID == key).FirstOrDefault() : null;
            string PropData = "";
            if (it != null)
            {
                PropData = it.REC_ID + "![" + it.REC_EDIT_ON.ToString(BASE._Server_Date_Format_Long) + "!["
                        + it.Final_Amount + "![" + it.Name;
            }
            return GridViewExtension.GetCustomDataCallbackResult(PropData);
        }
        #endregion

        #region Frm_Reference_Type
        public ActionResult Frm_Reference_Type(string Tag, string iRefType, string iLed_ID, bool FromDNK = false)
        {
            Frm_Reference_Type_Model model = new Frm_Reference_Type_Model();
            model.Tag = Tag;
            model.RefType = iRefType;
            model.Led_ID = iLed_ID;
            ViewBag.FromDNK = FromDNK;
            return View(model);
        }
        public ActionResult Frm_Existing_References(string Led_ID, string Txn_M_ID,string Ref_Rec_ID,bool FromDNK=false)
        {
            Common_Lib.RealTimeService.Param_GetProfileListing_WIP WIPProfile = new Common_Lib.RealTimeService.Param_GetProfileListing_WIP();
            WIPProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            WIPProfile.Next_YearID = BASE._next_Unaudited_YearID;
            WIPProfile.WIP_LED_ID = Led_ID;
            WIPProfile.TR_M_ID = Txn_M_ID;         
            var _db_Table = BASE._WIPDBOps.GetProfileListing_WIP(WIPProfile);
            if (_db_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = "Some Error Occurred During The Current Operation...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (_db_Table.Count <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "No Reference Present for selected Ledger. Please add New Reference, if you want to proceed"
                }, JsonRequestBehavior.AllowGet);
            }
            WIP_ExistingData = _db_Table;
            ViewBag.FromDNK = FromDNK;
            ViewBag.Ref_Rec_ID = Ref_Rec_ID;
            return View(WIP_ExistingData);
        }
        public ActionResult Frm_Reference_Type_Info_Grid()
        {
            return PartialView("Frm_Reference_Type_Info_Grid", WIP_ExistingData);
        }
        public ActionResult AddToExistingWIPReference(string Reference_RefRecId, string Reference_Name, double Reference_Opening, double Reference_Closing, double Reference_NextYearClosingValue, double Txt_Amount, string iRefType, string Ref_RecID, string Specific_ItemID, string Txn_M_ID,bool From_DNK)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (Reference_Closing + Txt_Amount < 0)
                {
                    jsonParam.message = "Sorry ! Updating of Reference Amount creates a Negative Closing Balance in Current Year for WIP(" + Reference_Name + ") with Original Value " + Reference_Opening;
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = From_DNK==false?"GLookUp_PurList": "GLookUp_PurList_DNK_Itm";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);

                }

                if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                {
                    if (Reference_NextYearClosingValue + Txt_Amount < 0)
                    {
                        jsonParam.message = "Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Next Year for WIP( " + Reference_Name + " ) with Original Value " + Reference_Opening;
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = From_DNK == false ? "GLookUp_PurList" : "GLookUp_PurList_DNK_Itm";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                if (iRefType == "EXISTING")
                {
                    if (!(Ref_RecID == null))
                    {
                        if (Ref_RecID.Length > 0 && Reference_RefRecId != Ref_RecID)
                        {
                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", Specific_ItemID, Txn_M_ID, null, Common.Navigation_Mode._Edit, Ref_RecID);
                            if (PROF_TABLE.Rows.Count > 0)
                            {
                                if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                                {
                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                    {
                                        jsonParam.message = "Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Next Year for WIP with Original Value " + PROF_TABLE.Rows[0]["Org Value"];
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);

                                    }
                                }
                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                {
                                    jsonParam.message = "Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Current Year for WIP with Original Value " + PROF_TABLE.Rows[0]["Org Value"];
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                jsonParam.result = true;
                return Json(new { jsonParam, Ref_RecID = Reference_RefRecId, iRefType = "EXISTING", iReference = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_WIP_Window(string LedID, double Amount, string Tag, string Reference,string xID, string iTxnM_ID, string iRefType, 
            string iSpecific_ItemID, string RefRecID, bool FromDNK = false, bool FromJV = false)
        {
            Frm_WIP_Window_Model model = new Frm_WIP_Window_Model();
            model.TempActionMethod = Tag;
            model.xID = xID;
            model.LedID = LedID;
            model.Amount_WIP = Amount;
            model.Reference_WIP = Reference;
            model.iTxnM_ID = iTxnM_ID;
            model.iReftype = iRefType;
            model.iSpecific_itemID = iSpecific_ItemID;
            model.Ref_RecID = RefRecID;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            model.Reference_WIP = model.Reference_WIP.HandleEscapeCharacters();
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            model.WIPLedgerListData = LookUp_GetWIPLedgerList();
            model.GLookUp_WIP_LedgerList = model.LedID;
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_WIP_Window_BUT_SAVE_Click(Frm_WIP_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_WIP_LedgerList))
                    {
                        jsonParam.message = "Ledger Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_WIP_LedgerList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Reference_WIP))
                    {
                        jsonParam.message = "Please enter relevant reference which you may remember while converting WIP to assets...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Reference";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Duplicate Reference
                    Common_Lib.RealTimeService.Param_GetDuplicateReferenceCount Param = new Common_Lib.RealTimeService.Param_GetDuplicateReferenceCount();
                    Param.Reference = model.Reference_WIP;
                    Param.Tag = (int)(Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                    Param.iTxnM_ID = model.iTxnM_ID;
                    if (model.TempActionMethod == "_Edit")
                    {
                        Param.RecID = model.xID;
                    }
                    int cnt = Convert.ToInt32(BASE._WIPCretionVouchers.GetDuplicateReferenceCount(Param));
                    if (cnt > 0)
                    {
                        jsonParam.message = "Same Reference Already Exists";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Reference";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.iReftype == "EXISTING")
                {
                    if (!string.IsNullOrEmpty(model.Ref_RecID))
                    {
                        if (model.Ref_RecID.Length > 0)
                        {
                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", model.iSpecific_itemID, model.iTxnM_ID, "", Common.Navigation_Mode._Edit, model.Ref_RecID);
                            if (PROF_TABLE.Rows.Count > 0)
                            {
                                if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                                {
                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                    {
                                        jsonParam.message = "Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Next Year for WIP with Original Value " + PROF_TABLE.Rows[0]["Org Value"];
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);

                                    }
                                }
                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                {
                                    jsonParam.message = "Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Current Year for WIP with Original Value " + PROF_TABLE.Rows[0]["Org Value"];
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);

                                }
                            }
                        }
                    }
                }
                jsonParam.result = true;
                return Json(new { jsonParam, Ref_RecID = "", iRefType = "NEW", iReference = model.Reference_WIP }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Return_WIP_Ledger> LookUp_GetWIPLedgerList()
        {
            var d2 = BASE._WIP_Finalization_DBOps.GetListOfWIPLedgers(BASE.Is_HQ_Centre);      
            d2.Sort((x, y) => x.WIP_LEDGER.CompareTo(y.WIP_LEDGER));
            return d2;          
        }  
        #endregion

        #region Frm_Telephone_Select       
        public ActionResult Frm_Telephone_Select(string Tag, string xID, string TP_BILL_NO, string TP_BILL_DATE, string TP_PERIOD_FROM, string TP_PERIOD_TO)
        {
            Payment_Frm_Telephone_Select_Model model = new Payment_Frm_Telephone_Select_Model();
            model.TelephoneListData = LookUp_GetTeleList();        
                if (!string.IsNullOrEmpty(TP_BILL_DATE))
            {
                model.Txt_Bill_Date = Convert.ToDateTime(TP_BILL_DATE);
            }
            if (!string.IsNullOrEmpty(TP_PERIOD_FROM))
            {
                model.Txt_Fr_Date = Convert.ToDateTime(TP_PERIOD_FROM);
            }
            if (!string.IsNullOrEmpty(TP_PERIOD_TO))
            {
                model.Txt_To_Date = Convert.ToDateTime(TP_PERIOD_TO);
            }
            model.Txt_Bill_No = TP_BILL_NO;
            model.xID = xID;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.TempActionMethod = Tag;
            model.GLookUp_TeleList = xID;
            if (model.TelephoneListData.Count == 1)
            {
                model.GLookUp_TeleList = model.TelephoneListData[0].TP_ID;
            }
            return PartialView(model);
        }
        public ActionResult Frm_Telephone_Select_Window(Payment_Frm_Telephone_Select_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.GLookUp_TeleList))
                    {
                        jsonParam.message = "Telephone No. Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_TeleList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.BE_Plantype.ToString().ToUpper().Trim() == "POST PAID")
                    {
                        if (model.Txt_Fr_Date.HasValue == false)
                        {
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Fr_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_To_Date.HasValue == false)
                        {
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_To_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_Fr_Date.HasValue == true && model.Txt_To_Date.HasValue == true)
                        {
                            if (model.Txt_To_Date.Value < model.Txt_Fr_Date.Value)
                            {
                                jsonParam.message = "Date must be Higher/Equal to From Date...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_To_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (string.IsNullOrEmpty(model.Txt_Bill_No))
                        {
                            jsonParam.message = "Bill No. cannot be Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Bill_No";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);

                        }
                        if (model.Txt_Bill_Date.HasValue == false)
                        {
                            jsonParam.message = "Date Incorrect/Blank....!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Bill_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                jsonParam.message = "Success";
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    billdate = model.Txt_Bill_Date != null ? Convert.ToDateTime(model.Txt_Bill_Date).ToString("dd/MM/yyyy") : null,
                    periodfrom = model.Txt_Fr_Date != null ? Convert.ToDateTime(model.Txt_Fr_Date).ToString("dd/MM/yyyy") : null,
                    periodto = model.Txt_To_Date != null ? Convert.ToDateTime(model.Txt_To_Date).ToString("dd/MM/yyyy") : null
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }    
        public List<Return_Payment_Telephone_Select> LookUp_GetTeleList()
        {
            var d1 = BASE._telephoneDBOps.GetListByCondition(Common_Lib.RealTimeService.ClientScreen.Profile_Telephone, "");       
            d1.Sort((x, y) => x.TP_NO.CompareTo(y.TP_NO));
            return d1;          
        }
        #endregion

        #region Frm_GoldSilver_Window
        public ActionResult Frm_GoldSilver_Window(string BE_ItemName, string Tag, string Cmd_Type, string GS_DESC_MISC_ID, string GS_LOC_AL_ID,
            string Txt_Weight, string Txt_Others, string Tr_M_ID = "", bool FromDNK = false, bool FromJV = false)
        {
            ProfilePayment_User_Rights();
            Payment_Frm_GoldSilver_Window_Model model = new Payment_Frm_GoldSilver_Window_Model();
            model.MiscListData = LookUp_GetMiscList();
            model.LocationListData = LookUp_GetLocList();         
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.BE_ItemName = BE_ItemName;
            model.Cmd_Type = Cmd_Type;
            model.MiscList = GS_DESC_MISC_ID;
            model.LocList = GS_LOC_AL_ID;
            model.Txt_Weight = Convert.ToDouble(Txt_Weight);
            model.Txt_Others = Txt_Others;
            Tr_M_ID_PayProfile = Tr_M_ID;
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            if (model.LocationListData.Count == 1)
            {
                model.LocList = model.LocationListData[0].AL_ID;
            }
            return View(model);
        }

        public ActionResult Frm_GoldSilver_Window_SaveClick(Payment_Frm_GoldSilver_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrEmpty(model.MiscList))
                    {
                        jsonParam.message = "Description  Not  Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "MiscList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Weight <= 0)
                    {
                        jsonParam.message = "Item   Weight   cannot   be   Zero / Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Weight";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.LocList))
                    {
                        jsonParam.message = "Location Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "LocList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.MiscList_Text.ToUpper().Trim() == "OTHERS")
                    {
                        if (string.IsNullOrWhiteSpace(model.Txt_Others))
                        {
                            jsonParam.message = "Other Detail Not Specified...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Others";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);

                        }
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
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Return_GoldSilver_MiscList> LookUp_GetMiscList()
        {
            var d1 = BASE._Gift_DBOps.GetGSItems("Name", "ID");       
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));         
            return d1;        
        }
        #endregion

        #region Frm_Asset_Window
        [HttpGet]
        public ActionResult Frm_Asset_Window(string BE_ItemName, string Tag, string Txt_Amt, string Txt_Rate,string Txt_Date, string Txt_Qty, string Txt_Make,
            string Txt_Model, string Txt_Serial, string Txt_Warranty, string AI_PUR_DATE, string Txt_Others, string AI_LOC_AL_ID, string Txt_V_Date, 
            string ActionMethod = null, bool IsGift = false, bool Asset_Window_FromWIPFinalization = false, bool FromDNK = false, bool FromJV = false)
        {
            ProfilePayment_User_Rights();
            Payment_Frm_Asset_Window_Model model = new Payment_Frm_Asset_Window_Model();
            model.BE_ItemName = BE_ItemName;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "" + Tag);       
            if (IsDate(Txt_Date))
            {
                model.Txt_Date = Convert.ToDateTime(Txt_Date);
            }
            model.IsGift = IsGift;
            model.Txt_Make = Txt_Make;
            model.Txt_Model = Txt_Model;
            model.Txt_Serial = Txt_Serial;
            if (!string.IsNullOrEmpty(Txt_Warranty)) model.Txt_Warranty = double.Parse(Txt_Warranty);
            model.LocationListData = LookUp_GetLocList();          
            if (Tag == "_Edit" || Tag == "_View" || Tag == "_Delete")
            {
                if (IsDate(AI_PUR_DATE)) 
                {
                    model.Txt_Date = Convert.ToDateTime(AI_PUR_DATE);
                }
                model.Txt_Others = Txt_Others;
            }
            if (!string.IsNullOrEmpty(Txt_Amt)) model.Txt_Amts = Convert.ToDouble(Txt_Amt);
            if (!string.IsNullOrEmpty(Txt_Rate)) model.Txt_Rates = double.Parse(Txt_Rate);
            if (!string.IsNullOrEmpty(Txt_Qty)) model.Txt_Qtys = double.Parse(Txt_Qty);
            model.AI_LOC_AL_ID = AI_LOC_AL_ID;   
            model.LocList = AI_LOC_AL_ID;            
            ViewBag.Asset_Window_FromWIPFinalization = Asset_Window_FromWIPFinalization;
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            ViewBag.Payment_Asset_Image = Profile_Asset_Image;
            if (model.LocationListData.Count == 1)
            {
                model.LocList = model.LocationListData[0].AL_ID;
            }
            return View(model);
        } 
        [HttpPost]
        public ActionResult Frm_Asset_Window(Payment_Frm_Asset_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.Tag.ToString());
                DateTime PurDate = (DateTime)model.Txt_Date;
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit)
                {
                    if (model.Txt_Warranty < 0)
                    {
                        jsonParam.message = "Warranty cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Warranty";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Amts <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero / Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amts";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (model.Txt_Qtys <= 0)
                    {
                        jsonParam.message = "Quantity cannot be Zero / Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Qtys";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (model.Txt_Date == null || IsDate(model.Txt_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (IsDate(model.Txt_Date.ToString()) == true && !model.IsGift)
                    {
                        if (IsDate(model.Txt_Date.ToString()) == true)
                        {
                            if (model.Txt_Date < BASE._open_Year_Sdt || model.Txt_Date > BASE._open_Year_Edt)
                            {
                                jsonParam.message = "Date not as per Financial Year...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(model.LocList) || model.LocList.Trim().Length == 0)
                    {
                        jsonParam.message = "Location Not Selected ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "LocList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (string.IsNullOrEmpty(model.Txt_Make) || model.Txt_Make.Trim().Length == 0)
                    {
                        jsonParam.message = "Make Incorrect / Blank ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Make";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (string.IsNullOrEmpty(model.Txt_Model) || model.Txt_Model.Trim().Length == 0)
                    {
                        jsonParam.message = "Model Incorrect / Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Model";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (model.Txt_Qtys <= 0)
                    {
                        model.Txt_Qtys = 1;
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    AI_Pur_Date = PurDate.ToString(BASE._Server_Date_Format_Short)
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Upload()
        {
            var myFile = Request.Files["paymentAsset_myFile"];
            string[] imageExtensions = { ".jpg", ".jpeg", ".png" };
            var fileName = myFile.FileName.ToLower();
            var isValidExtenstion = imageExtensions.Any(ext =>
            {
                return fileName.LastIndexOf(ext) > -1;
            });
            if (isValidExtenstion)
            {
                BinaryReader reader = new BinaryReader(myFile.InputStream);
                byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);
                Profile_Asset_Image = imageBytes;
            }
            return new EmptyResult();
        }
        public void RemoveAssetImage()
        {
            BASE._SessionDictionary.Remove("Payment_Asset_Image_Payment");
        }
        public ActionResult Asset_PreviewImageControl()
        {
            ViewBag.Payment_Asset_Image = Profile_Asset_Image;
            return View();
        }
        #endregion

        #region Add Location
        public ActionResult AddLocations_Check(string ActionMethod = "New")
        {
            if (ActionMethod == "New")
            {
                if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                {
                    return Json(new
                    {
                        message = "Locations cannot be Added...!" + ("<br>" + ("<br>" + "Additions of Locations in Current year can be done only after finalization of previous year accounts.")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_Location_Window(string ActionMethod = "New", string PopupID = "PaymentAddLocation", string CallingScreen = "Payment", string Dropdown_DataGrid = "Frm_Window_GLookUp_LocList_datagrid", string DropdownRefreshFunction = "", string Info_LastEditedOn = null, string LastEditedOn = null, string xID = null)//Mantis bug 0000503 fixed//Mantis bug 0000505 fixed
        {
            ProfilePayment_User_Rights();
            if (!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Core, "Add"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','Not Allowed','No Rights');</script>");
            }
            Payment_Frm_Location_Window_Model model = new Payment_Frm_Location_Window_Model();
            model.PopupID = PopupID;//Mantis bug 0000503 fixed
            model.CallingScreen = CallingScreen;//Mantis bug 0000505 fixed
            model.Dropdown_DataGrid = Dropdown_DataGrid;//Mantis bug 0000505 fixed
            model.DropdownRefreshFunction = DropdownRefreshFunction; //redmine Bug #133151 fixed
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.xID = xID;
            model.Info_LastEditedOn = Info_LastEditedOn == null ? (DateTime?)null : Convert.ToDateTime(Info_LastEditedOn);
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                DataTable d1 = BASE._AssetLocDBOps.GetRecord(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (d1 == null)
                {
                    var message = Common_Lib.Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    var message = Common_Lib.Messages.RecordChanged("Current Location");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','" + message + "','Record Changed / Removed in Background!!');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod == "Edit" || ActionMethod == "Delete")
                    {
                        if (model.Info_LastEditedOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            var message = Common_Lib.Messages.RecordChanged("Current Location");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','" + message + "','Record Already Changed!!');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_Name_Location = d1.Rows[0]["AL_LOC_NAME"].ToString();
                model.Txt_Name_Tag = model.Txt_Name_Location;
                model.Txt_Others_Location = d1.Rows[0]["AL_OTHER_DETAIL"].ToString();
                model.xMAIN = d1.Rows[0]["AL_LOC_MAIN"].ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Location_Window(Payment_Frm_Location_Window_Model model)
        {
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.Tag.ToString());
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit)
            {
                if (string.IsNullOrWhiteSpace(model.Txt_Name_Location))
                {
                    return Json(new
                    {
                        message = "Location cannot be Blank...!",
                        result = false,
                        focus = "Txt_Name_Location"
                    }, JsonRequestBehavior.AllowGet);
                }
                object MaxValue = 0;
                MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(model.Txt_Name_Location.Trim(), Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (MaxValue == null)
                {
                    return Json(new
                    {
                        message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((int)MaxValue != 0 && Tag == Common.Navigation_Mode._New)
                {
                    return Json(new
                    {
                        message = "Same Location Already Available( " + model.Txt_Name_Location + " )...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else if ((int)MaxValue != 0 && Tag == Common.Navigation_Mode._Edit)
                {
                    if (model.Txt_Name_Location.ToUpper().Trim() != model.Txt_Name_Tag.ToUpper().Trim())
                    {
                        return Json(new
                        {
                            message = "Same Location Already Available( " + model.Txt_Name_Location + " )...!<br><br>--> Edit Location: ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (BASE.AllowMultiuser())
            {
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit)
                {
                    DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                    if (!(LocNames == null))
                    {
                        if (LocNames.Rows.Count > 0)
                        {
                            for (int I = 0; I <= (LocNames.Rows.Count - 1); I++)
                            {
                                if (model.Txt_Name_Location.ToUpper().Trim() == LocNames.Rows[I][0].ToString().ToUpper())
                                {
                                    return Json(new
                                    {
                                        message = Messages.DependencyChanged("Location name") + "</br>Referred Record Already Changed, Location with same name already exists in pending Transfers!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
            {
                object MaxValue = 0;
                MaxValue = BASE._AssetLocDBOps.GetStatus(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (MaxValue == null)
                {
                    return Json(new
                    {
                        message = "Entry Not Found...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                {
                    return Json(new
                    {
                        message = "Locked Entry Cannot Be Edited/Deleted...<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (Tag == Common.Navigation_Mode._New)
            {
                return Json(new
                {
                    message = "",
                    PopupFormName = "Frm_Location_Map_Window",
                    PopupFormPath = "/Account/ProfilePaymentVoucher/Frm_Location_Map_Window/",
                    popTitle = "New ~ Location Matching",
                    Text = "(Location Matching)...",
                    QueryString = "Tag=_New&xLocName=" + Url.Encode(model.Txt_Name_Location) +
                    "&xLocRemarks=" + Url.Encode(model.Txt_Others_Location),
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else if (Tag == Common.Navigation_Mode._Edit)
            {
                return Json(new
                {
                    message = "",
                    PopupFormName = "Frm_Location_Map_Window",
                    PopupFormPath = "/Account/ProfilePaymentVoucher/Frm_Location_Map_Window/",
                    popTitle = "Edit ~ Location Matching",
                    Text = "(Location Matching)...",
                    QueryString = "Tag=_Edit&xLocName=" + Url.Encode(model.Txt_Name_Location) +
                    "&xLocRemarks=" + Url.Encode(model.Txt_Others_Location) + "&xID=" + Url.Encode(model.xID) + "&Txt_Name_Tag=" + Url.Encode(model.Txt_Name_Tag) + "&LastEditedOn=" + Url.Encode(model.LastEditedOn.ToString()),
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else if (Tag == Common.Navigation_Mode._Delete)
            {
                if (BASE.AllowMultiuser())
                {
                    DataTable assetLoc_DbOps = BASE._AssetLocDBOps.GetRecord(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if (assetLoc_DbOps == null)
                    {
                        return Json(new
                        {
                            message = Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (assetLoc_DbOps.Rows.Count == 0)
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.RecordChanged("Current Location"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.LastEditedOn != Convert.ToDateTime(assetLoc_DbOps.Rows[0]["REC_EDIT_ON"]))
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.RecordChanged("Current Location"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(model.xID, false);
                    bool DeleteAllow = true;
                    if (!string.IsNullOrEmpty(UsedPage))
                    {
                        DeleteAllow = false;
                    }
                    if (DeleteAllow == false)
                    {
                        return Json(new
                        {
                            message = "Can't Delete...!<br><br>This information is being used in Another Page...!<br><br>Name: " + UsedPage,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BASE._AssetLocDBOps.Delete(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation))
                {
                    return Json(new
                    {
                        message = Messages.DeleteSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
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
        [HttpGet]
        public ActionResult Frm_Location_Map_Window(string xLocName, string Tag, string xLocRemarks, string Status_Action = "", string xID = "", string LastEditedOn = null, string Txt_Name_Tag = null)
        {
            Payment_Frm_Location_Map_Window_Model model = new Payment_Frm_Location_Map_Window_Model();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "" + Tag);
            model.xLocName = xLocName;
            model.xLocRemarks = xLocRemarks;
            model.xID = xID;
            model.LastEditedOn_Map = LastEditedOn == null ? (DateTime?)null : Convert.ToDateTime(LastEditedOn);
            model.Txt_Name_Tag_Map = Txt_Name_Tag;
            model.ServicePlaceListData = Frm_Location_Map_Window_GLookUp_SerList();
            model.PropertylistData = Frm_Location_Map_Window_GLookUp_ProList();
            if (Tag == "_Edit" || Tag == "_View" || Tag == "_Delete")
            {
                DataTable d1 = BASE._AssetLocDBOps.GetRecord(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (d1.Rows[0]["LB_REC_ID"].ToString().Length > 0)
                {
                    model.Look_ProList = d1.Rows[0]["LB_REC_ID"].ToString();
                    model.Rad_Matching = "0";
                }
                else
                {
                    model.Look_SerList = d1.Rows[0]["SP_REC_ID"].ToString();
                    model.Rad_Matching = "1";
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Location_Map_Window(Payment_Frm_Location_Map_Window_Model model)
        {
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.Tag.ToString());
            if (Tag == Common.Navigation_Mode._Edit)
            {
                if (BASE.IsInsuranceAudited())
                {
                    return Json(new
                    {
                        message = "Location Rematching Cannot Be Done After Completion Of Insurance Audit",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit)
            {
                if (Convert.ToInt16(model.Rad_Matching) == 0 && string.IsNullOrWhiteSpace(model.Look_ProList))
                {
                    return Json(new
                    {
                        message = "Property Not Selected",
                        result = false,
                        focus = "Look_ProList"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToInt16(model.Rad_Matching) == 1 && string.IsNullOrWhiteSpace(model.Look_SerList))
                {
                    return Json(new
                    {
                        message = "Service Place Not Selected",
                        result = false,
                        focus = "Look_SerList"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            string SP_Id = string.IsNullOrEmpty(model.Look_SerList) ? "" : model.Look_SerList;
            string LB_Id = string.IsNullOrEmpty(model.Look_ProList) ? "" : model.Look_ProList;
            if (Tag == Common.Navigation_Mode._New)
            {
                if (BASE.AllowMultiuser())
                {
                    object MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(model.xLocName.ToString().Trim(), Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            message = Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)MaxValue != 0)
                    {
                        return Json(new
                        {
                            message = Messages.DependencyChanged("Location name") + "Location with same name already exists!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt16(model.Rad_Matching) == 0)
                    {
                        Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
                        PropParam.Next_YearID = BASE._next_Unaudited_YearID;
                        PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
                        PropParam.YearID = BASE._open_Year_ID;
                        PropParam.Asset_RecID = LB_Id;
                        var PropertyList = BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);
                        if (PropertyList == null)
                        {
                            return Json(new
                            {
                                message = Messages.SomeError,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (PropertyList.Count <= 0)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Property") + " Referred Record Already Sold/Transferred/Deleted!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if (CommonFunctions.AreDatesEqual(model.Look_ProList_REC_EDIT_ON, PropertyList[0].REC_EDIT_ON) == false)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Property") + " Referred Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Convert.ToInt16(model.Rad_Matching) == 1)
                    {
                        DataTable d1 = BASE._ServPlacesDBOps.GetRecord(SP_Id);
                        if (d1 == null)
                        {
                            return Json(new
                            {
                                message = "Some Error Occured During The Current Operation..!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows.Count <= 0)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Service Place") + " Referred Record Already Deleted!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (CommonFunctions.AreDatesEqual(model.Look_SerList_REC_EDIT_ON,Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))==false)
                            {
                                return Json(new
                                {
                                    message = Messages.DependencyChanged("Service Place") + " Referred Record Already Changed!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                if (BASE._AssetLocDBOps.Insert(Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation, model.xLocName, model.xLocRemarks, ((int)Common.Record_Status._Completed).ToString(), SP_Id, LB_Id))
                {
                    return Json(new
                    {
                        message = Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else if (Tag == Common.Navigation_Mode._Edit)
            {
                if (BASE.AllowMultiuser())
                {
                    DataTable assetLoc_DbOps = BASE._AssetLocDBOps.GetRecord(model.xID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if (assetLoc_DbOps == null)
                    {
                        return Json(new
                        {
                            message = Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (assetLoc_DbOps.Rows.Count == 0)
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.RecordChanged("Current Location"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.LastEditedOn_Map != Convert.ToDateTime(assetLoc_DbOps.Rows[0]["REC_EDIT_ON"]))
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.RecordChanged("Current Location"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    object MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(model.xLocName.ToString().Trim(), Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            message = Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)MaxValue != 0)
                    {
                        if (model.xLocName.ToUpper().Trim() != model.Txt_Name_Tag_Map.ToUpper().Trim())
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Location name") + " Location with same name already exists!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Convert.ToInt16(model.Rad_Matching) == 0)
                    {
                        Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
                        PropParam.Next_YearID = BASE._next_Unaudited_YearID;
                        PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
                        PropParam.YearID = BASE._open_Year_ID;
                        PropParam.Asset_RecID = LB_Id;
                        var PropertyList = BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);
                        if (PropertyList == null)
                        {
                            return Json(new
                            {
                                message = Messages.SomeError,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (PropertyList.Count <= 0)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Property") + " Referred Record Already Sold/Transferred/Deleted!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if (model.Look_ProList_REC_EDIT_ON != PropertyList[0].REC_EDIT_ON)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Property") + " Referred Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Convert.ToInt16(model.Rad_Matching) == 1)
                    {
                        DataTable d1 = BASE._ServPlacesDBOps.GetRecord(SP_Id);
                        if (d1 == null)
                        {
                            return Json(new
                            {
                                message = "Some Error Occured During The Current Operation..!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows.Count <= 0)
                        {
                            return Json(new
                            {
                                message = Messages.DependencyChanged("Service Place") + " Referred Record Already Deleted!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (model.Look_SerList_REC_EDIT_ON.ToString() != d1.Rows[0]["REC_EDIT_ON"].ToString())
                            {
                                return Json(new
                                {
                                    message = Messages.DependencyChanged("Service Place") + " Referred Record Already Changed!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                string Matching = "";
                if (Convert.ToInt16(model.Rad_Matching) == 0)
                {
                    Matching = "PROPERTY";
                }
                else
                {
                    Matching = "SERVICE PLACE";
                }
                if (BASE._AssetLocDBOps.Update_Global(Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation, model.xLocName, model.xLocRemarks, Matching, LB_Id, SP_Id, model.xID))
                {
                    return Json(new
                    {
                        message = Messages.UpdateSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public List<Return_GetAllPropertyList> Frm_Location_Map_Window_GLookUp_ProList()
        {
            Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
            PropParam.Next_YearID = BASE._next_Unaudited_YearID;
            PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
            PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
            PropParam.YearID = BASE._open_Year_ID;
            return BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);          
        }
        public List<ServicePlaces.Return_GetAllServicePlaceList> Frm_Location_Map_Window_GLookUp_SerList()
        {
            return BASE._ServPlacesDBOps.GetAllServicePlaceList(Common_Lib.RealTimeService.ClientScreen.Profile_Core);           
        }

        #endregion      

        #region Frm_Live_Stock_Window
        public ActionResult Frm_Live_Stock_Window(string BE_ItemName, string Tag, string Txt_Name, string LS_BIRTH_YEAR, string LS_INSURANCE, string LS_INS_ID, string LS_INS_POLICY_NO, string LS_INS_AMT, string LS_INS_DATE,
            string Txt_Others, string LS_LOC_AL_ID, string Tr_M_ID = "",bool FromDNK = false, bool FromJV = false)
        {
            ProfilePayment_User_Rights();
            Frm_Live_Stock_Window_Model model = new Frm_Live_Stock_Window_Model();
            model.InsuranceListData = LookUp_GetInsList();
            model.LocationListData = LookUp_GetLocList();        
            model.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), Tag);
            model.Txt_Name = Txt_Name;
            model.BE_ItemName = BE_ItemName;
            model.BirthYearData = new List<string>();
            for (int i = BASE._open_Year_Sdt.Year; i >= 1950; i--)
            {
                model.BirthYearData.Add(i.ToString());
            }                
            model.Rad_Insurance = LS_INSURANCE;
            model.Cmd_Year = LS_BIRTH_YEAR;
            model.Look_InsList = LS_INS_ID;
            model.Txt_PolicyNo = LS_INS_POLICY_NO;
            if (LS_INS_AMT != null && LS_INS_AMT.Length > 0)
            {
                model.Txt_InsAmt = Convert.ToDouble(LS_INS_AMT);
            }
            model.Txt_Others = Txt_Others;
            DateTime xDate;
            if (IsDate(LS_INS_DATE))
            {
                xDate = Convert.ToDateTime(LS_INS_DATE);
                model.Txt_I_Date = xDate;
            }
            if (!string.IsNullOrEmpty(LS_LOC_AL_ID))
            {
                model.Look_LocList = LS_LOC_AL_ID;
            }
            model.Txt_PolicyNo = model.Txt_PolicyNo.HandleEscapeCharacters();
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            if (model.LocationListData.Count == 1)
            {
                model.Look_LocList = model.LocationListData[0].AL_ID;
            }
            return View(model);
        }
        public ActionResult Frm_Live_Stock_Window_SaveClick(Frm_Live_Stock_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.Txt_Name))
                    {
                        jsonParam.message = "Name cannot be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Name";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrEmpty(model.Cmd_Year))
                    {
                        if (double.Parse(model.Cmd_Year) < 1950)
                        {
                            jsonParam.message = "Enter Proper Birth Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Year";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Rad_Insurance))
                    {
                        if (model.Rad_Insurance == "YES")
                        {
                            if (string.IsNullOrWhiteSpace(model.Look_InsList))
                            {
                                jsonParam.message = "Insurance Company Not Selected...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Look_InsList";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (string.IsNullOrWhiteSpace(model.Txt_PolicyNo))
                            {
                                jsonParam.message = "Policy No. Cannot Be Blank...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_PolicyNo";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.Txt_InsAmt < 0)
                            {
                                jsonParam.message = "Insurance Amount cannot be Negative...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_InsAmt";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.Txt_I_Date == null || IsDate(model.Txt_I_Date.ToString()) == false)
                            {
                                jsonParam.message = "Date Incorrect/Blank......!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_I_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.Txt_I_Date != null && IsDate(model.Txt_I_Date.ToString()) == true)
                            {
                                if (model.Txt_I_Date < DateTime.Now)
                                {
                                    jsonParam.message = "Date must be higher than Today's...!";
                                    jsonParam.title = "Incomplete Information..";
                                    jsonParam.result = false;
                                    jsonParam.focusid = "Txt_I_Date";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.Look_LocList))
                    {
                        jsonParam.message = "Location Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Look_LocList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                if (model.Txt_I_Date != null)
                {
                    DateTime InsDate = (DateTime)model.Txt_I_Date;
                    return Json(new { jsonParam, Ins_Date = InsDate.ToString(BASE._Server_Date_Format_Short) }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { jsonParam, Ins_Date = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }        
        #endregion

        #region Frm_Vehicles_Window
        public ActionResult Frm_Vehicles_Window(string Tag, string BE_ItemName, string Item_Name_Display,
            string Cmd_Make, string VI_MODEL, string VI_REG_NO_PATTERN, string VI_REG_NO,
            string VI_REG_DATE, string VI_OWNERSHIP, string VI_OWNERSHIP_AB_ID,
            string VI_DOC_RC_BOOK, string VI_DOC_AFFIDAVIT, string VI_DOC_WILL,
            string VI_DOC_TRF_LETTER, string VI_DOC_FU_LETTER, string VI_DOC_OTHERS,
            string VI_DOC_NAME, string VI_INSURANCE_ID, string Txt_PolicyNo, string VI_E_DATE,
            string Txt_Others, string VI_LOC_AL_ID,bool FromDNK=false, bool FromJV = false)
        {
            ProfilePayment_User_Rights();
            Tr_M_ID_PayProfile = "";
            Payment_Frm_Vehicles_Window_Model model = new Payment_Frm_Vehicles_Window_Model();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.TextEdit1 = BASE._open_Ins_Name;
            model.BE_ItemName = BE_ItemName;
            model.LocList = VI_LOC_AL_ID == "" ? null : VI_LOC_AL_ID;
            model.Cmd_Make = Cmd_Make == "" ? null : Cmd_Make;
            model.Cmd_Model = VI_MODEL == "" ? null : VI_MODEL;
            model.RAD_RegPattern = VI_REG_NO_PATTERN == null || VI_REG_NO_PATTERN.Length == 0 ? "New" : VI_REG_NO_PATTERN;
            model.Txt_RegNo = VI_REG_NO == "" ? null : VI_REG_NO;

            model.OwnerListData = LookUp_GetOwnList();
            model.InsuranceListData = LookUp_GetInsList();
            model.MakeListData = Get_Vehicle_Make_List();
            model.LocationListData = LookUp_GetLocList();
            if (model.LocationListData.Count == 1) 
            {
                model.LocList = model.LocationListData[0].AL_ID;
            }
            if (!string.IsNullOrWhiteSpace(model.Cmd_Make))
            {
                model.ModelListData = Get_Vehicle_Model_List(model.Cmd_Make);
            }
            else 
            {
                model.ModelListData = new List<Return_Vehicles_ModelList>();
            }
            if (IsDate(VI_REG_DATE))
            {
                model.Txt_RegDate = Convert.ToDateTime(VI_REG_DATE);
            }       
            model.Cmd_Ownership = VI_OWNERSHIP == "" ? null : VI_OWNERSHIP;
            if (VI_OWNERSHIP_AB_ID != null)
            {
                if (VI_OWNERSHIP_AB_ID.Length > 0)
                {
                    VI_OWNERSHIP_AB_ID = VI_OWNERSHIP_AB_ID.Trim().StartsWith("'") ? VI_OWNERSHIP_AB_ID.Substring(2, VI_OWNERSHIP_AB_ID.Length) : VI_OWNERSHIP_AB_ID.Trim();
                    VI_OWNERSHIP_AB_ID = VI_OWNERSHIP_AB_ID.Trim().EndsWith("'") ? VI_OWNERSHIP_AB_ID.Substring(1, VI_OWNERSHIP_AB_ID.Length - 1) : VI_OWNERSHIP_AB_ID.Trim();
                }
                model.OwnList = VI_OWNERSHIP_AB_ID == "" ? null : VI_OWNERSHIP_AB_ID;
            }

            if (VI_DOC_RC_BOOK!=null&&VI_DOC_RC_BOOK.Trim().ToUpper() == "YES")
            {
                model.Chk_RC_Book = true;
            }
            else
            {
                model.Chk_RC_Book = false;
            }
            if (VI_DOC_AFFIDAVIT != null && VI_DOC_AFFIDAVIT.Trim().ToUpper() == "YES")
            {
                model.Chk_Affidavit = true;
            }
            else
            {
                model.Chk_Affidavit = false;
            }
            if (VI_DOC_WILL != null && VI_DOC_WILL.Trim().ToUpper() == "YES")
            {
                model.Chk_Will = true;
            }
            else
            {
                model.Chk_Will = false;
            }
            if (VI_DOC_TRF_LETTER != null && VI_DOC_TRF_LETTER.Trim().ToUpper() == "YES")
            {
                model.Chk_Trf_Letter = true;
            }
            else
            {
                model.Chk_Trf_Letter = false;
            }
            if (VI_DOC_FU_LETTER != null && VI_DOC_FU_LETTER.Trim().ToUpper() == "YES")
            {
                model.Chk_FU_Letter = true;
            }
            else
            {
                model.Chk_FU_Letter = false;
            }
            if (VI_DOC_OTHERS != null && VI_DOC_OTHERS.Trim().ToUpper() == "YES")
            {
                model.Chk_OtherDoc = true;
            }
            model.Txt_OtherDoc = VI_DOC_NAME == "" ? null : VI_DOC_NAME;
            model.InsList = VI_INSURANCE_ID == "" ? null : VI_INSURANCE_ID;
            model.Txt_PolicyNo = Txt_PolicyNo == "" ? null : Txt_PolicyNo;
            if (IsDate(VI_E_DATE))
            {
                model.Txt_E_Date = Convert.ToDateTime(VI_E_DATE);
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                model.Txt_Others = Txt_Others;
            }
            model.Txt_OtherDoc = model.Txt_OtherDoc.HandleEscapeCharacters();
            model.Cmd_Make = model.Cmd_Make.HandleEscapeCharacters();
            model.Cmd_Model = model.Cmd_Model.HandleEscapeCharacters();
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            return View(model);
        }

        public ActionResult Frm_Vehicles_Window_SaveClick(Payment_Frm_Vehicles_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {

                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (!(model.BE_ItemName.ToUpper().Trim() == "YO BIKE" || model.BE_ItemName.ToUpper().Trim() == "CYCLE" || model.BE_ItemName.ToUpper().Trim() == "GOLF CART"))
                    {
                        if (model.Txt_RegNo == null || model.Txt_RegNo.Trim().Length == 0)
                        {
                            jsonParam.message = "Registration No. cannot be Blank...!"; /*RedmineBug#134803*/
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_RegNo";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_RegDate == null || (IsDate(model.Txt_RegDate.ToString()) == false))
                        {
                            jsonParam.message = "Date Of First Registration Is Incorrect/Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_RegDate";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Cmd_Ownership == null || (model.Cmd_Ownership.Trim().Length == 0))
                    {
                        jsonParam.message = "Ownership Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Ownership";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Cmd_Ownership != "INSTITUTION")
                    {
                        if (model.OwnList == null || model.OwnList.Trim().Length == 0)
                        {
                            jsonParam.message = "Owner Name Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "OwnList";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!(model.BE_ItemName.ToUpper().Trim() == "YO BIKE" || model.BE_ItemName.ToUpper().Trim() == "CYCLE" || model.BE_ItemName.ToUpper().Trim() == "GOLF CART"))
                    {
                        if (model.InsList == null || model.InsList.Trim().Length <= 0)
                        {
                            jsonParam.message = "Insurance Company Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "InsList";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_PolicyNo == null || model.Txt_PolicyNo.Trim().Length == 0)
                        {
                            jsonParam.message = "Policy No. cannot be Blank...!"; /*RedmineBug#134810*/
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_PolicyNo";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_E_Date == null || IsDate(model.Txt_E_Date.ToString()) == false)
                        {
                            jsonParam.message = "Expiry Date Is Incorrect/Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_E_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_E_Date.ToString()) == true)
                        {
                            if ((model.Txt_E_Date - model.Txt_RegDate).Value.TotalDays <= 0)
                            {
                                jsonParam.message = "Date must be higher than First Date of Registration...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_E_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((model.Txt_E_Date - DateTime.Now.Date).Value.TotalDays <= 0)
                            {
                                jsonParam.message = "Date must be higher than Today's...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_E_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (model.LocList.Trim().Length == 0)
                    {
                        jsonParam.message = "Location Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "LocList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    VI_REG_DATE = model.Txt_RegDate == null ? null : Convert.ToDateTime(model.Txt_RegDate).ToString(BASE._Server_Date_Format_Short),
                    VI_EXP_DATE = model.Txt_E_Date == null ? null : Convert.ToDateTime(model.Txt_E_Date).ToString(BASE._Server_Date_Format_Short)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Return_Vehicles_OwnerList> LookUp_GetOwnList() 
        {
            var d1 = BASE._Gift_DBOps.GetVehicleOwners();
            d1.Sort((x, y) => x.ID.CompareTo(y.ID));
            return d1;
        }
        public ActionResult RefreshOwnerList()
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetOwnList()), "application/json");
        }
        public List<Return_Vehicles_MakeList> Get_Vehicle_Make_List() 
        {
            var d1 = BASE._Gift_DBOps.GetVehicleMake("Name", "ID");
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));
            return d1;
        }
        public List<Return_Vehicles_ModelList> Get_Vehicle_Model_List(string Cmd_Make="") 
        {
            var d1 = BASE._Gift_DBOps.GetVehicleModels(Cmd_Make);      
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));
            return d1;
        }
        public ActionResult RefreshModelList(string Cmd_Make)
        {
            return Content(JsonConvert.SerializeObject(Get_Vehicle_Model_List(Cmd_Make)), "application/json");
        }     
        #endregion

        #region Frm_Property_Window
        public ActionResult Frm_Property_Window(string Tag, string List_LB_EXTENDED_PROPERTY_TABLE, string List_LB_DOCS_ARRAY, string ITEM_ID, string LB_PRO_TYPE, 
            string LB_PRO_CATEGORY, string LB_PRO_USE, string LB_PRO_NAME, string LB_PRO_ADDRESS, string LB_ADDRESS1, string LB_ADDRESS2, string LB_ADDRESS3, 
            string LB_ADDRESS4, string LB_CITY_ID, string LB_DISTRICT_ID, string LB_STATE_ID, string LB_PINCODE, string LB_OWNERSHIP, string LB_OWNERSHIP_PARTY_ID, 
            string LB_SURVEY_NO, string LB_CON_YEAR, string LB_RCC_ROOF, string LB_PAID_DATE, string LB_PERIOD_FROM, string LB_PERIOD_TO, string LB_DOC_OTHERS, 
            string LB_DOC_NAME, string LB_OTHER_DETAIL, string LB_TOT_P_AREA, string LB_CON_AREA, string LB_DEPOSIT_AMT, string LB_MONTH_RENT, 
            string LB_MONTH_O_PAYMENTS, string LB_REC_ID, string xID,bool IsGift=false,bool FromDNK=false, bool FromJV = false)
        {
            Frm_Property_Window_Model model = new Frm_Property_Window_Model();
            model.IsGift = IsGift;
            model.IsJV = FromJV;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethodPropertyWindow = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ITEM_ID = ITEM_ID;
            model.Chk_OtherDoc = false;
            model.TextEdit1 = BASE._open_Ins_Name;
            model.xID = xID;
            model.PPDocumentList = Get_Documents_List();
            List<string> YearBind = new List<string>();
            for (int i = BASE._open_Year_Edt.Year; i >= 1900; i += -1)
            {
                YearBind.Add(i.ToString());
            }
            model.Cmd_Con_Year_Bind = YearBind;
            List<Cmd_Con_Year_Bind_Info> dic_Cmd_PUse = new List<Cmd_Con_Year_Bind_Info>();
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "0", Value = "MAIN CENTRE" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "1", Value = "CLASS" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "2", Value = "MUSEUM" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "3", Value = "STORE" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "4", Value = "GARDEN" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "5", Value = "GARAGE" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "6", Value = "DEPARTMENT" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "7", Value = "RESIDENCE" });
            dic_Cmd_PUse.Add(new Cmd_Con_Year_Bind_Info { Index = "8", Value = "NOT IN USE" });
            model.PuseListData = dic_Cmd_PUse;
            model.Document = null;
            model.OwnerListData = LookUp_Prop_GetOwnList();
            model.StateListData = LookUp_Get_StateList();
            model.DistrictListData = new List<Return_DistrictList>();
            model.CityListData = new List<Return_CityList>();
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                if (List_LB_DOCS_ARRAY != null)
                {
                    var Obj_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<Profile.Models.LB_DOCS_ARRAY_List>>(List_LB_DOCS_ARRAY);
                    model.LB_DOCS_ARRAY = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Obj_LB_DOCS_ARRAY);
                }
                if (List_LB_EXTENDED_PROPERTY_TABLE != null)
                {
                    var Obj_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(List_LB_EXTENDED_PROPERTY_TABLE);
                    model.LB_EXTENDED_PROPERTY_TABLE = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Obj_LB_EXTENDED_PROPERTY_TABLE);
                }
                DataTable d1_Ext = model.LB_EXTENDED_PROPERTY_TABLE;
                DataTable d1_Doc = model.LB_DOCS_ARRAY;
                model.Cmd_PType = LB_PRO_TYPE;
                model.Cmd_PCategory = LB_PRO_CATEGORY;
                model.Cmd_PUse = dic_Cmd_PUse.Find(x => x.Value == LB_PRO_USE).Index;
                model.Txt_PName = LB_PRO_NAME;
                if (!(string.IsNullOrEmpty(LB_PRO_ADDRESS)))
                {
                    model.Txt_Address = LB_PRO_ADDRESS;
                    model.Txt_Add = LB_PRO_ADDRESS;
                }
                if (LB_ADDRESS1.Length > 0)
                {
                    model.Txt_R_Add1 = LB_ADDRESS1;
                    model.Txt_R_Add2 = LB_ADDRESS2;
                    model.Txt_R_Add3 = LB_ADDRESS3;
                    model.Txt_R_Add4 = LB_ADDRESS4;
                    string StateCode = null;                  
                    if (LB_STATE_ID.Length > 0)
                    {
                        model.GLookUp_StateList = LB_STATE_ID;
                        if (model.StateListData.Count > 0)
                        {
                            var state = model.StateListData.Find(x => x.R_ST_REC_ID == LB_STATE_ID);
                            if (state != null) 
                            {
                                StateCode = state.R_ST_CODE;
                            }
                        }
                    }
                    if (LB_DISTRICT_ID.Length > 0)
                    {
                        model.GLookUp_DistrictList = LB_DISTRICT_ID;
                        model.DistrictListData=LookUp_Get_DistrictList(StateCode);
                    }
                    if (LB_CITY_ID.Length > 0)
                    {
                        model.GLookUp_CityList = LB_CITY_ID;
                        model.CityListData = LookUp_Get_CityList(StateCode);
                    }
                    model.Txt_R_Pincode = LB_PINCODE;
                }
                model.Cmd_Ownership = LB_OWNERSHIP;
                if (!string.IsNullOrEmpty(LB_OWNERSHIP_PARTY_ID) && LB_OWNERSHIP_PARTY_ID != "NULL")
                {
                    model.Look_OwnList = LB_OWNERSHIP_PARTY_ID;
                }
                model.Txt_SNo = LB_SURVEY_NO;
                model.Txt_Tot_Area = string.IsNullOrWhiteSpace(LB_TOT_P_AREA) ? 0 : Convert.ToDouble(LB_TOT_P_AREA);
                model.Txt_Con_Area = string.IsNullOrWhiteSpace(LB_CON_AREA) ? 0 : Convert.ToDouble(LB_CON_AREA);
                model.Cmd_Con_Year = LB_CON_YEAR;
                model.Cmd_RccType = LB_RCC_ROOF;
                model.Txt_Dep_Amt = string.IsNullOrWhiteSpace(LB_DEPOSIT_AMT) ? 0 : Convert.ToDouble(LB_DEPOSIT_AMT);
                model.Txt_Mon_Rent = string.IsNullOrWhiteSpace(LB_MONTH_RENT) ? 0 : Convert.ToDouble(LB_MONTH_RENT);
                model.Txt_Other_Payments = string.IsNullOrEmpty(LB_MONTH_O_PAYMENTS) ? 0 : Convert.ToDouble(LB_MONTH_O_PAYMENTS);
                DateTime? xDate = null;
                if ((!string.IsNullOrEmpty(LB_PAID_DATE)
                            && IsDate(LB_PAID_DATE)))
                {
                    model.Txt_PaidDate = Convert.ToDateTime(LB_PAID_DATE);
                }

                if ((!string.IsNullOrEmpty(LB_PERIOD_FROM)
                            && IsDate(LB_PERIOD_FROM)))
                {

                    model.Txt_F_Date = Convert.ToDateTime(LB_PERIOD_FROM);
                }

                if ((!string.IsNullOrEmpty(LB_PERIOD_TO)
                            && IsDate(LB_PERIOD_TO)))
                {
                    model.Txt_T_Date = Convert.ToDateTime(LB_PERIOD_TO);
                }
                if (LB_DOC_OTHERS!=null&&(LB_DOC_OTHERS.ToUpper().Trim() == "YES"))
                {
                    model.Chk_OtherDoc = true;
                }
                else
                {
                    model.Chk_OtherDoc = false;
                }
                List<Documents> Document = new List<Documents>();
                foreach (DataRow XROW in d1_Doc.Rows)
                {
                    Documents newrow = new Documents();
                    newrow.ID = XROW["LB_MISC_ID"].ToString();
                    newrow.Selected = true;
                    Document.Add(newrow);
                }
                model.Document = Document;
                model.Txt_OtherDoc = LB_DOC_NAME.HandleEscapeCharacters(); /*RedmineBug#134887*/

                model.Txt_Remarks_Pw = LB_OTHER_DETAIL;
            }
            model.Txt_PName = model.Txt_PName.HandleEscapeCharacters();
            model.Txt_Address = model.Txt_Address.HandleEscapeCharacters();
            model.Txt_Add = model.Txt_Add.HandleEscapeCharacters();
            model.Txt_R_Add1 = model.Txt_R_Add1.HandleEscapeCharacters();
            model.Txt_R_Add2 = model.Txt_R_Add2.HandleEscapeCharacters();
            model.Txt_R_Add3 = model.Txt_R_Add3.HandleEscapeCharacters();
            model.Txt_R_Add4 = model.Txt_R_Add4.HandleEscapeCharacters();
            model.Txt_SNo = model.Txt_SNo.HandleEscapeCharacters();
            ViewBag.FromDNK = FromDNK;
            ViewBag.FromJV = FromJV;
            return PartialView(model);
        }
        public ActionResult Frm_Property_Window_Property_Address(string Txt_Address, string Txt_R_Add1, string Txt_R_Add2, string Txt_R_Add3, string Txt_R_Add4, string GLookUp_StateList, string GLookUp_DistrictList, string GLookUp_CityList, string Txt_R_Pincode, Frm_Property_Window_Model model)
        {
            // Frm_Property_Window_Model model = new Frm_Property_Window_Model();
            model.Txt_Address = Txt_Address;
            model.Txt_R_Add1 = Txt_R_Add1;
            model.Txt_R_Add2 = Txt_R_Add2;
            model.Txt_R_Add3 = Txt_R_Add3;
            model.Txt_R_Add4 = Txt_R_Add4;
            model.GLookUp_StateList = GLookUp_StateList;
            model.GLookUp_DistrictList = GLookUp_DistrictList;
            model.GLookUp_CityList = GLookUp_CityList;
            model.Txt_R_Pincode = Txt_R_Pincode;
            return PartialView(model);
        }
        public List<Return_LB_Documents> Get_Documents_List()
        {
            var d1 = BASE._L_B_Voucher_DBOps.GetDocuments();
            return d1;
        }
        public ActionResult RefreshPropOwner() 
        {
            return Content(JsonConvert.SerializeObject(LookUp_Prop_GetOwnList()), "application/json");
        }
        public List<Return_LB_Owners> LookUp_Prop_GetOwnList()
        {
            var d1 = BASE._L_B_Voucher_DBOps.GetOwners();
            d1.Sort((x, y) => x.ID.CompareTo(y.ID));
           return d1;
        }    
        public List<Return_StateList> LookUp_Get_StateList()
        {
            var d1 = BASE._Address_DBOps.GetStates("IN", "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            d1.Sort((x, y) => x.R_ST_NAME.CompareTo(y.R_ST_NAME));          
            return d1;           
        }
        public ActionResult RefreshDistrictList(string state)
        {
            return Content(JsonConvert.SerializeObject(LookUp_Get_DistrictList(state)), "application/json");
        }
        public List<Return_DistrictList> LookUp_Get_DistrictList(string state)
        {
            var d1 = BASE._Address_DBOps.GetDistricts("IN", string.IsNullOrEmpty(state) ? "0" : state, "R_DI_NAME", "R_DI_REC_ID");
            d1.Sort((x, y) => x.R_DI_NAME.CompareTo(y.R_DI_NAME));
            return d1;
        }
        public ActionResult RefreshCityList(string state)
        {
            return Content(JsonConvert.SerializeObject(LookUp_Get_CityList(state)), "application/json");
        }
        public List<Return_CityList> LookUp_Get_CityList(string state)
        {
            var d1 = BASE._Address_DBOps.GetCitiesBySt_Co_Code("IN", string.IsNullOrEmpty(state) ? "0" : state, "R_CI_NAME", "R_CI_REC_ID");
            d1.Sort((x, y) => x.R_CI_NAME.CompareTo(y.R_CI_NAME));
            return d1;
        }
        public ActionResult AddressChecks(string Txt_R_Add1, string GLookUp_StateList, string GLookUp_DistrictList, string GLookUp_CityList, string Txt_R_Pincode)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(Txt_R_Add1))
                {
                    jsonParam.message = "Property Address Cannot Be Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_R_Add1";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(GLookUp_StateList))
                {
                    jsonParam.message = "State Cannot Be Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "GLookUp_StateList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(GLookUp_DistrictList))
                {
                    jsonParam.message = "District Cannot Be Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "GLookUp_DistrictList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(GLookUp_CityList))
                {
                    jsonParam.message = "City Cannot Be Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "GLookUp_CityList";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(Txt_R_Pincode))
                {
                    jsonParam.message = "Pincode Cannot Be Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_R_Pincode";
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
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Cmd_PUse_SelectedIndexChanged()
        {
            DataTable CenterData = BASE._L_B_Voucher_DBOps.GetMainCenterAdd();
            if (CenterData == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Txt_PName = string.Empty;
            string Txt_R_Add1 = string.Empty;
            string Txt_R_Add2 = string.Empty;
            string Txt_R_Add3 = string.Empty;
            string Txt_R_Add4 = string.Empty;
            string Cen_State_ID = string.Empty;
            string Cen_District_ID = string.Empty;
            string Cen_City_ID = string.Empty;
            string Cen_State = string.Empty;
            string Cen_District = string.Empty;
            string Cen_City = string.Empty;
            string Txt_R_Pincode = string.Empty;
            string Txt_Add = string.Empty;

            if (CenterData.Rows.Count > 0)
            {
                Txt_PName = CenterData.Rows[0]["CEN_B_NAME"].ToString();
                Txt_R_Add1 = CenterData.Rows[0]["CEN_ADD1"].ToString();
                Txt_R_Add2 = CenterData.Rows[0]["CEN_ADD2"].ToString();
                Txt_R_Add3 = CenterData.Rows[0]["CEN_ADD3"].ToString();
                Txt_R_Add4 = CenterData.Rows[0]["CEN_ADD4"].ToString();
                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_STATE_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_STATE_ID"].ToString().Length > 0)
                    {
                        Cen_State_ID = CenterData.Rows[0]["CEN_STATE_ID"].ToString();
                        Cen_State = CenterData.Rows[0]["State"].ToString();
                    }
                }
                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_DISTRICT_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_DISTRICT_ID"].ToString().Length > 0)
                    {
                        Cen_District_ID = CenterData.Rows[0]["CEN_DISTRICT_ID"].ToString();
                        Cen_District = CenterData.Rows[0]["Dist"].ToString();
                    }
                }
                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_CITY_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_CITY_ID"].ToString().Length > 0)
                    {
                        Cen_City_ID = CenterData.Rows[0]["CEN_CITY_ID"].ToString();
                        Cen_City = CenterData.Rows[0]["City"].ToString();
                    }
                }
                Txt_R_Pincode = CenterData.Rows[0]["CEN_PINCODE"].ToString();
            }
            if (Txt_R_Add1.Length > 0)
            {
                Txt_Add = Txt_R_Add1 + (Txt_R_Add2.Trim().Length > 0 ? ", " + Txt_R_Add2 : "") + (Txt_R_Add3.Trim().Length > 0 ? ", " + Txt_R_Add3 : "") + (Txt_R_Add4.Trim().Length > 0 ? ", " + Txt_R_Add4 : "") +
                         ", " + Cen_City.ToUpper() + ", Dist. " + Cen_District + ", " + Cen_State + "-" + Txt_R_Pincode;
            }
            return Json(new
            {
                result = true,
                Txt_PName,
                Txt_R_Add1,
                Txt_R_Add2,
                Txt_R_Add3,
                Txt_R_Add4,
                Cen_State_ID,
                Cen_District_ID,
                Cen_City_ID,
                Txt_R_Pincode,
                Txt_Add
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Frm_Property_Window_BUT_SAVE_Click(Frm_Property_Window_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethodPropertyWindow.ToString());

                if (model.Tag == Common.Navigation_Mode._New)
                {
                    model.xID = Guid.NewGuid().ToString();
                }
                if (model.Tag == Common.Navigation_Mode._New | model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.Cmd_PCategory))
                    {
                        jsonParam.message = "Property Category Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PCategory";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmd_PType))
                    {
                        jsonParam.message = "Property Type Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PType";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmd_PUse))
                    {
                        jsonParam.message = "Use of Property Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PUse";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_PName))
                    {
                        jsonParam.message = "Property/Building Name cannot be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_Add))
                    {
                        jsonParam.message = "Property/Building Address Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Add";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_R_Add1))
                    {
                        jsonParam.message = "Property Address Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_R_Add1";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_StateList))
                    {
                        jsonParam.message = "State Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_StateList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_DistrictList))
                    {
                        jsonParam.message = "District Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_DistrictList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_CityList))
                    {
                        jsonParam.message = "City Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_CityList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_R_Pincode))
                    {
                        jsonParam.message = "Pincode Cannot Be  Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_R_Pincode";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmd_Ownership))
                    {
                        jsonParam.message = "Ownership Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Ownership";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //free
                    if (model.Cmd_Ownership.ToUpper() == "THIRD PARTY")
                    {
                        if (string.IsNullOrWhiteSpace(model.Look_OwnList))
                        {
                            jsonParam.message = "Owner Name Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Look_OwnList";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_SNo))
                    {
                        jsonParam.message = "Survey No cannot be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_SNo";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.Txt_Tot_Area <= 0 || model.Txt_Tot_Area == null) && !model.Cmd_PType.Contains("FLAT"))
                    {
                        jsonParam.message = "Total Plot Area cannot be Zero or Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Tot_Area";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Tot_Area < 0 && model.Txt_Tot_Area != null)
                    {
                        jsonParam.message = "Total Plot Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Tot_Area";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (model.Txt_Con_Area < 0)
                    {
                        jsonParam.message = "Construction Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Con_Area";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Cmd_PType.ToUpper() != "LAND")
                    {
                        if (model.Txt_Con_Area <= 0 || model.Txt_Con_Area == null)
                        {
                            jsonParam.message = "Total Constructed Area cannot be Zero or Negative...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Con_Area";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrWhiteSpace(model.Cmd_Con_Year))
                        {
                            jsonParam.message = "Construction Year not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Con_Year";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrWhiteSpace(model.Cmd_RccType))
                        {
                            jsonParam.message = "RCC Roof Construction Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_RccType";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Txt_Dep_Amt < 0)
                    {
                        jsonParam.message = "Security Deposit Amount cannot be Negative...!"; /*RedmineBug#134825*/
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Dep_Amt";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Mon_Rent < 0)
                    {
                        jsonParam.message = "Monthly Rent Amount cannot be Negative...!";/*RedmineBug#134825*/
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Mon_Rent";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Other_Payments < 0)
                    {
                        jsonParam.message = "Other Monthly Payment cannot be Negative...!";/*RedmineBug#134825*/
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Other_Payments";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrEmpty(model.Cmd_Con_Year) && model.Cmd_Con_Year.Trim().Length > 0)
                    {
                        if (Convert.ToInt32(model.Cmd_Con_Year) > BASE._open_Year_Edt.Year)
                        {
                            jsonParam.message = "Construction Year must be Less than/Equal to Current Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Con_Year";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((model.Cmd_PCategory.ToUpper().Contains(LB_Category.PURCHASED.ToString()) || model.Cmd_PCategory.ToUpper().Contains(LB_Category.GIFTED.ToString())) && !model.Cmd_PType.ToUpper().Contains("LAND"))
                    {
                        if (string.IsNullOrEmpty(model.Cmd_RccType) || model.Cmd_RccType.Trim().Length == 0)
                        {
                            jsonParam.message = "RCC Constructed Roof Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_RccType";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (IsDate(model.Txt_F_Date.ToString()) == true && IsDate(model.Txt_T_Date.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_F_Date) >= Convert.ToDateTime(model.Txt_T_Date))
                        {
                            jsonParam.message = "Date must be Higher than From Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_T_Date";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Cmd_PCategory.ToUpper().Contains(LB_Category.LEASED.ToString()) && model.Cmd_PCategory.ToUpper().Contains("LONG"))
                    {
                        if (IsDate(model.Txt_F_Date.ToString()) == true & IsDate(model.Txt_T_Date.ToString()) == true)
                        {
                            double diff = (Convert.ToDateTime(model.Txt_T_Date) - Convert.ToDateTime(model.Txt_F_Date)).TotalDays;
                            if (diff < 3650)
                            {
                                jsonParam.message = "Leased(Long Term) Period Cannot be Less than 10 Years...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_T_Date";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    string query = "";
                    DataTable MainCenters = null;
                    if (model.Cmd_PUse == "0")
                    {
                        if (model.Tag == Common.Navigation_Mode._New)
                            MainCenters = BASE._L_B_DBOps.GetMainCenters();
                        if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            MainCenters = BASE._L_B_DBOps.GetMainCenters(model.xID);
                        if (MainCenters == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (MainCenters.Rows.Count > 0)
                        {
                            jsonParam.message = "Main Centre (" + MainCenters.Rows[0]["LB_PRO_NAME"].ToString() + ")  already Created in  " + MainCenters.Rows[0]["CEN_UID"].ToString() + " in year " + MainCenters.Rows[0]["YEAR_ID"].ToString() + "...!";
                            jsonParam.title = "Duplicate Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_PUse";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    int? MaxValue = 0;
                    MaxValue = Convert.ToInt32(BASE._L_B_Voucher_DBOps.CheckDuplicatePropertyName((int)model.Tag, model.xID, model.Txt_PName));
                    if (MaxValue == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (MaxValue > 0)
                    {
                        jsonParam.message = "Property with same name already Created...!";
                        jsonParam.title = "Duplicate Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    int? MaxValue_Loc = 0;
                    MaxValue_Loc = Convert.ToInt32(BASE._AssetLocDBOps.GetRecordCountByName(model.Txt_PName, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment, BASE._open_PAD_No_Main));
                    if (MaxValue_Loc == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (MaxValue_Loc != 0 && model.Tag == Common.Navigation_Mode._New)
                    {
                        jsonParam.message = "Location With Same Name Already Available...!";
                        jsonParam.title = "Duplicate. . . (" + model.Txt_PName + ")";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                        if (LocNames != null)
                        {
                            if (LocNames.Rows.Count > 0)
                            {
                                if (model.Txt_PName.ToString().Length > 0)
                                {
                                    for (int I = 0; I <= LocNames.Rows.Count - 1; I++)
                                    {
                                        if (model.Txt_PName.ToUpper() == LocNames.Rows[I][0].ToString().ToUpper())
                                        {
                                            jsonParam.message = "Location With Same Name Already Available...!";
                                            jsonParam.title = "Duplicate. . . (" + model.Txt_PName + ")";
                                            jsonParam.result = false;
                                            jsonParam.focusid = "Txt_PName";
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

                if (string.IsNullOrEmpty(model.Cmd_RccType) || model.Cmd_RccType.Trim().Length == 0)
                {
                    model.Cmd_RccType = "NO";
                }

                //Extended Property
                FillExtensionTable(model.LB_EXTENDED_PROPERTY_TABLE);
                List<LB_EXTENDED_PROPERTY_TABLE_List> return_LB_EXTENDED_PROPERTY_TABLE_List = new List<LB_EXTENDED_PROPERTY_TABLE_List>();
                var all_data_Of_Grid = ExtendedProperty_GridData;
                if (all_data_Of_Grid != null && all_data_Of_Grid.Count > 0)
                {
                    for (int I = 0; I <= all_data_Of_Grid.Count() - 1; I++)
                    {
                        if (all_data_Of_Grid[I].Sr > 0)
                        {
                            LB_EXTENDED_PROPERTY_TABLE_List newrow = new LB_EXTENDED_PROPERTY_TABLE_List();
                            newrow.LB_MOU_DATE = Convert.ToDateTime(all_data_Of_Grid[I].M_O_U_Date).ToString("dd/MM/yyyy");
                            newrow.LB_SR_NO = all_data_Of_Grid[I].Sr.ToString();
                            newrow.LB_INS_ID = all_data_Of_Grid[I].Ins_ID.ToString();
                            newrow.LB_TOT_P_AREA = all_data_Of_Grid[I].Total_Plot_Area.ToString();
                            newrow.LB_CON_AREA = all_data_Of_Grid[I].Constructed_Area.ToString();
                            newrow.LB_CON_YEAR = all_data_Of_Grid[I].Construction_Year.ToString();
                            newrow.LB_VALUE = all_data_Of_Grid[I].Value.ToString();
                            newrow.LB_OTHER_DETAIL = all_data_Of_Grid[I].Other_Detail;
                            newrow.LB_REC_ID = model.xID;
                            return_LB_EXTENDED_PROPERTY_TABLE_List.Add(newrow);
                        }
                    }
                }
                //Documents 
                model.LB_DOCS_ARRAY = new DataTable();
                var _with1 = model.LB_DOCS_ARRAY;
                _with1.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                _with1.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

                List<LB_DOCS_ARRAY_List> return_LB_DOCS_ARRAY_List = new List<LB_DOCS_ARRAY_List>();
                if (model.Document != null)
                {
                    foreach (var currSelection in model.Document.Where(x => x.Selected == true))
                    {
                        LB_DOCS_ARRAY_List newrow = new LB_DOCS_ARRAY_List();
                        newrow.LB_MISC_ID = currSelection.ID;
                        newrow.LB_REC_ID = model.xID;
                        return_LB_DOCS_ARRAY_List.Add(newrow);

                        DataRow Row = model.LB_DOCS_ARRAY.NewRow();
                        Row["LB_MISC_ID"] = currSelection.ID.ToString();
                        Row["LB_REC_ID"] = model.xID;
                        model.LB_DOCS_ARRAY.Rows.Add(Row);
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    LB_PRO_TYPE = model.Cmd_PType,
                    LB_PRO_CATEGORY = model.Cmd_PCategory,
                    LB_PRO_USE = model.Cmd_PUse_Text,
                    LB_PRO_NAME = model.Txt_PName,
                    LB_PRO_ADDRESS = model.Txt_Address == null ? "" : model.Txt_Address,
                    LB_ADDRESS1 = model.Txt_R_Add1,
                    LB_ADDRESS2 = model.Txt_R_Add2,
                    LB_ADDRESS3 = model.Txt_R_Add3,
                    LB_ADDRESS4 = model.Txt_R_Add4,
                    LB_STATE_ID = model.GLookUp_StateList,
                    LB_CITY_ID = model.GLookUp_CityList,
                    LB_DISTRICT_ID = model.GLookUp_DistrictList,
                    LB_PINCODE = model.Txt_R_Pincode,
                    LB_OWNERSHIP = model.Cmd_Ownership,
                    LB_OWNERSHIP_PARTY_ID = model.Cmd_Ownership.ToUpper() == "THIRD PARTY" ? model.Look_OwnList : (model.IsGift || model.IsJV)?"NULL":null,
                    LB_SURVEY_NO = model.Txt_SNo,
                    LB_CON_YEAR = model.Cmd_Con_Year,
                    LB_RCC_ROOF = model.Cmd_RccType,
                    LB_PAID_DATE = IsDate(model.Txt_PaidDate.ToString()) ? Convert.ToDateTime(model.Txt_PaidDate).ToString(BASE._Server_Date_Format_Short) : string.Empty,
                    LB_PERIOD_FROM = IsDate(model.Txt_F_Date.ToString()) ? Convert.ToDateTime(model.Txt_F_Date).ToString(BASE._Server_Date_Format_Short) : string.Empty,
                    LB_PERIOD_TO = IsDate(model.Txt_T_Date.ToString()) ? Convert.ToDateTime(model.Txt_T_Date).ToString(BASE._Server_Date_Format_Short) : string.Empty,
                    LB_DOC_OTHERS = model.Chk_OtherDoc == true ? "YES" : "NO",
                    LB_DOC_NAME = model.Txt_OtherDoc,
                    LB_OTHER_DETAIL = model.Txt_Remarks_Pw,
                    LB_TOT_P_AREA = model.Txt_Tot_Area.HasValue ? model.Txt_Tot_Area.Value.ToString() : "",
                    LB_CON_AREA = model.Txt_Con_Area.HasValue ? model.Txt_Con_Area.Value.ToString() : "",
                    LB_DEPOSIT_AMT = model.Txt_Dep_Amt.HasValue ? model.Txt_Dep_Amt.Value.ToString() : "",
                    LB_MONTH_RENT = model.Txt_Mon_Rent.HasValue ? model.Txt_Mon_Rent.Value.ToString() : "",
                    LB_MONTH_O_PAYMENTS = model.Txt_Other_Payments.HasValue ? model.Txt_Other_Payments.Value.ToString() : "",
                    LB_REC_ID = model.xID,
                    return_LB_EXTENDED_PROPERTY_TABLE_List = return_LB_EXTENDED_PROPERTY_TABLE_List,
                    return_LB_DOCS_ARRAY_List = return_LB_DOCS_ARRAY_List
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void ClearPropertySession()
        {
            BASE._SessionDictionary.Remove("ExtendedProperty_GridData_Profile");
        }

        #region Frm_Property_Window_Ext
        public ActionResult Frm_Property_Window_Ext(string ActionMethod, string SrID = null)
        {
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            Frm_Property_Window_Ext_Model model = new Frm_Property_Window_Ext_Model();
            model.Cmd_Ext_Con_Year_Bind = new List<int>();
            for (int i = BASE._open_Year_Sdt.Year; i >= 1900; i += -1)
            {
                model.Cmd_Ext_Con_Year_Bind.Add(i);
            }
            model.InstituteListData = LookUp_GetInstituteList();
            if (ActionMethod == "_Edit")
            {
                model.ActionMethod = Tag;

                var Sr = Convert.ToInt16(SrID);
                var all_data = ExtendedProperty_GridData;
                var dataToEdit = all_data.FirstOrDefault(x => x.Sr == Sr);
                model.Sr = Sr;
                model.Institution = dataToEdit.Institution;
                model.Look_InsList = dataToEdit.Ins_ID;
                model.Txt_Ext_Tot_Area = dataToEdit.Total_Plot_Area;
                model.Txt_Ext_Con_Area = dataToEdit.Constructed_Area;
                model.Cmd_Ext_Con_Year = string.IsNullOrEmpty(dataToEdit.Construction_Year) ? (int?)null : Convert.ToInt32(dataToEdit.Construction_Year);
                model.Txt_MOU_Date = Convert.ToDateTime(dataToEdit.M_O_U_Date);
                model.xAmt = dataToEdit.Value;
                model.Txt_Others = dataToEdit.Other_Detail;
            }
            return PartialView(model);
        }     
        public List<Return_LB_Institution> LookUp_GetInstituteList()
        {
            var d1 = BASE._L_B_Voucher_DBOps.GetInstt();
            d1 = d1.OrderBy(x => x.ID).ToList();           
            return d1;            
        }
        [HttpPost]
        public ActionResult Frm_Property_Window_Ext(Frm_Property_Window_Ext_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod.ToString());

                if (model.Tag == Common.Navigation_Mode._New | model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.Look_InsList))
                    {
                        jsonParam.message = "Institute Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Look_InsList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Ext_Tot_Area < 0)
                    {
                        jsonParam.message = "Total Plot Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ext_Tot_Area";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Ext_Con_Area < 0)
                    {
                        jsonParam.message = "Construction Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ext_Con_Area";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Cmd_Ext_Con_Year != null && model.Cmd_Ext_Con_Year != 0)
                    {
                        if (Convert.ToInt32(model.Cmd_Ext_Con_Year) > Convert.ToInt32(BASE._open_Year_Sdt.Year))
                        {
                            jsonParam.message = "Construction Year must be Less than/Equal to Start Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Ext_Con_Year";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Txt_MOU_Date == null || IsDate(model.Txt_MOU_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_MOU_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    List<Property_Window_ExtendedProperty_Grid> gridRows = new List<Property_Window_ExtendedProperty_Grid>();
                    if (ExtendedProperty_GridData != null)
                    {
                        gridRows = ExtendedProperty_GridData;
                    }
                    if (model.Tag == Common.Navigation_Mode._New)
                    {
                        Property_Window_ExtendedProperty_Grid grid = new Property_Window_ExtendedProperty_Grid();

                        grid.Institution = model.Institution;
                        grid.Ins_ID = model.Look_InsList;
                        grid.Total_Plot_Area = model.Txt_Ext_Tot_Area??0.00;
                        grid.Constructed_Area = model.Txt_Ext_Con_Area??0.00;
                        grid.Construction_Year = Convert.ToString(model.Cmd_Ext_Con_Year);
                        grid.M_O_U_Date = Convert.ToDateTime(model.Txt_MOU_Date).ToString("dd/MM/yyyy");
                        grid.Value = model.xAmt??0;
                        grid.Other_Detail = model.Txt_Others;
                        gridRows.Add(grid);
                    }
                    else if (model.Tag == Common.Navigation_Mode._Edit)
                    {
                        var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                        dataToEdit.Institution = model.Institution;
                        dataToEdit.Ins_ID = model.Look_InsList;
                        dataToEdit.Total_Plot_Area = model.Txt_Ext_Tot_Area??0.00;
                        dataToEdit.Constructed_Area = model.Txt_Ext_Con_Area??0.00;
                        dataToEdit.Construction_Year = Convert.ToString(model.Cmd_Ext_Con_Year);
                        dataToEdit.M_O_U_Date = Convert.ToDateTime(model.Txt_MOU_Date).ToString("dd/MM/yyyy");
                        dataToEdit.Value = model.xAmt??0;
                        dataToEdit.Other_Detail = model.Txt_Others;
                    }
                    for (int i = 0; i < gridRows.Count; i++)
                    {
                        gridRows[i].Sr = i + 1;
                    }
                    ExtendedProperty_GridData = gridRows;
                    jsonParam.result = true;
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
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Property_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            var allData = ExtendedProperty_GridData;
            var dataToDelete = allData.FirstOrDefault(x => x.Sr == Sr);
            allData.Remove(dataToDelete);
            ExtendedProperty_GridData = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        private void FillExtensionTable(DataTable LB_EXTENDED_PROPERTY_TABLE)
        {
            LB_EXTENDED_PROPERTY_TABLE = new DataTable();
            var _with1 = LB_EXTENDED_PROPERTY_TABLE;
            _with1.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            _with1.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            _with1.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            _with1.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            _with1.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            _with1.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            _with1.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            _with1.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            _with1.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
        }
        public ActionResult Property_ExtendedPropertyGrid(string ActionMethodName, DataTable LB_EXTENDED_PROPERTY_TABLE)
        {
            List<Property_Window_ExtendedProperty_Grid> grid_data = new List<Property_Window_ExtendedProperty_Grid>();
            if (ActionMethodName == "_New")
            {
                return View(grid_data);
            }
            if (ExtendedProperty_GridData == null)
            {
                DataTable INS_Table = BASE._L_B_Voucher_DBOps.GetInstt("INS_NAME", "INS_ID", "SNAME");
                if (INS_Table == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('some Error Occurred During Current Operation','Error!!');</script>");
                }
                DataTable EXT_Table = LB_EXTENDED_PROPERTY_TABLE;
                foreach (DataRow XROW in EXT_Table.Rows)
                {
                    Property_Window_ExtendedProperty_Grid ROW = new Property_Window_ExtendedProperty_Grid();

                    ROW.Sr = Convert.ToInt16(XROW["LB_SR_NO"]);
                    ROW.Institution = INS_Table.AsEnumerable()
                                      .Where((row) => row.Field<string>("INS_ID").Equals(XROW["LB_INS_ID"].ToString()))
               .Select((row) => row.Field<string>("INS_NAME"))
               .FirstOrDefault();
                    ROW.Ins_ID = XROW["LB_INS_ID"].ToString();
                    ROW.Total_Plot_Area = Convert.ToDouble(XROW["LB_TOT_P_AREA"]);
                    ROW.Constructed_Area = Convert.ToDouble(XROW["LB_CON_AREA"]);
                    ROW.Construction_Year = XROW["LB_CON_YEAR"].ToString();
                    ROW.M_O_U_Date = Convert.ToDateTime(XROW["LB_MOU_DATE"]).ToString(BASE._Date_Format_Current);
                    ROW.Value = Convert.ToDouble(XROW["LB_VALUE"]);
                    ROW.Other_Detail = XROW["LB_OTHER_DETAIL"].ToString();
                    grid_data.Add(ROW);
                }
                ExtendedProperty_GridData = grid_data;
            }
            return View(ExtendedProperty_GridData);
        }
        #endregion

        #endregion

        #region Frm_Txn_Report
        public ActionResult Frm_Txn_Report(string CallingGridBaseSessionKey, string LedgerID, string LedgerName, string popupID, int GridPK = 0)
        {
            Frm_Txn_Report_model model = new Frm_Txn_Report_model();
            var GridData = (DataTable)GetBaseSession(CallingGridBaseSessionKey);
            if (GridData != null && GridData.Rows.Count > 0)
            {
                DataRow[] row = GridData.Select("[Sr.] =" + GridPK);
                if (row.Length > 0)
                {
                    model.WIP_ID = Convert.ToString(row[0]["Profile_WIP_RecID"]);
                    model.Opening = Convert.ToDecimal(row[0]["OPENING"]);
                    model.LedgerID = LedgerID;
                    model.LedgerName = LedgerName;
                    model.Reference = Convert.ToString(row[0]["Reference"]);
                    model.OpeningDate = Convert.IsDBNull(row[0]["Date of Creation"]) ? BASE._open_Year_Sdt : Convert.ToDateTime(row[0]["Date of Creation"]);
                }
            }
            if (!string.IsNullOrWhiteSpace(model.LedgerID))
            {
                Grid_Display_Frm_Txn_Report(model.OpeningDate, model.LedgerID, model.WIP_ID, model.Opening);
            }
            model.PopupID = popupID;
            return View(model);
        }
        public void Grid_Display_Frm_Txn_Report(DateTime OpeningDate, string LedgerID, string WIP_ID, decimal Opening)
        {
            DateTime FrDate = OpeningDate;
            DateTime ToDate = BASE._open_Year_Edt;
            Common_Lib.RealTimeService.Param_GetTxnReport param = new Common_Lib.RealTimeService.Param_GetTxnReport();
            param.FromDate = BASE._open_Year_Sdt;
            param.ToDate = ToDate;
            param.Led_ID = LedgerID;
            param.InsttId = BASE._open_Ins_ID;
            param.YearID = BASE._open_Year_ID;
            param.WIP_ID = WIP_ID;
            DataTable _Party_Table = BASE._WIPDBOps.GetTxn_Report(param);
            _Party_Table.Columns.Add("Balance", Type.GetType("System.String"));
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

            double Total = 0;
            double TotCredit = 0;
            double TotDebit = 0;
            foreach (DataRow cRow in _Party_Table.Rows)
            {
                if (!Convert.IsDBNull(cRow["Debit"]))
                {
                    if (string.IsNullOrWhiteSpace(cRow["Debit"].ToString()) == false)
                    {
                        Total += Convert.ToDouble(cRow["Debit"]);
                        TotDebit += Convert.ToDouble(cRow["Debit"]);
                    }
                }
                if (!Convert.IsDBNull(cRow["Credit"]))
                {
                    if (string.IsNullOrWhiteSpace(cRow["Credit"].ToString()) == false)
                    {
                        Total -= Convert.ToDouble(cRow["Credit"]);
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
                if ((cRow["Item Name"].ToString() == "Opening Balance") && (OpeningDate == BASE._open_Year_Sdt))
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
            Txn_Report_Data = _Party_Table;
        }
        public ActionResult Frm_Txn_Report_Grid(string OpeningDate, string LedgerID, string WIP_ID, decimal? Opening,string command)
        {
            if (command == "REFRESH")
            {
                Grid_Display_Frm_Txn_Report(Convert.ToDateTime(OpeningDate), LedgerID, WIP_ID,(decimal)Opening);
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Txn_Report_Data);
        }
        public ActionResult Txn_Report_Export_Options()
        {
            return View();
        }
        #endregion
        public List<Return_GetInsList> LookUp_GetInsList()
        {
            var d1 = BASE._Gift_DBOps.GetInsuranceItems("Name", "ID");         
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));
            return  d1;           
        }
        public List<Return_LocationList> LookUp_GetLocList()
        {
            var d2 = BASE._Gift_DBOps.GetAssetLocations(Tr_M_ID_PayProfile);
            d2.Sort((x, y) => x.Location_Name.CompareTo(y.Location_Name));
            return d2;
        }
        public ActionResult RefreshLocationList()
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetLocList()), "application/json");
        }
        public void ProfilePaymentClearSession()
        {
            ClearBaseSession("_Profile");
        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void ProfilePayment_User_Rights()
        {
            ViewData["ProfileCore_AddRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Core, "Add");
        }
    }

}