using ConnectOneMVC.Areas.Magazine.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using Common_Lib;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
using ConnectOneMVC.Areas.Facility.Models;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Magazine.Controllers
{
    public class MagazineMasterController : BaseController
    {
        public string Voucher_Entry = "Voucher Entry";
        public string Profile_Entry = "Profile Entry";

        private static string CountryCodeRes = "";
        private static string StateCodeRes = "";
        private static string DistirictRes = "";
        // GET: Magazine/MagazineMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Frm_Magazine_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Magazine, "List"))//Code written for User Authorization do not remove
            {
                ViewBag.ShowHorizontalBar = 0;
                MagazineMaster model = new MagazineMaster();

                //MagaZine List Start
                DataTable ML_Table = BASE._Magazine_DBOps.GetList(Voucher_Entry, Profile_Entry, "");
                if (ML_Table != null)
                    model.showMagazineList = ML_Table.AsEnumerable().Select(T =>
                      new MagazineList
                      {
                          Name = T["Name"].ToString(),
                          ShortName = T["Short Name"].ToString(),
                          Language = T["Language"].ToString(),
                          PublishOn = T["Publish On"].ToString(),
                          MagazineRegd = T["Magazine Regd. No."].ToString(),
                          PostalRegdNo = T["Postal Regd. No."].ToString(),
                          MembershipStart = Convert.ToInt32(T["Membership Start No."].ToString()),
                          Foreign = T["Foreign Subscriptions"].ToString(),
                          ID = T["ID"].ToString(),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();

                var MagListData = model.showMagazineList;
                Session["MagList_ExportData"] = MagListData;
                //MagaZine List End

                //Magazine Subtype List Start
                var MagagineListdata = Session["MagList_ExportData"] as List<MagazineList>;
                var MagazineId = MagagineListdata != null ? MagagineListdata.Select(f => f.ID).FirstOrDefault() : "0";

                DataTable MT_Table = BASE._Magazine_DBOps.GetList_SubscriptionTypeList(Voucher_Entry, Profile_Entry, " AND ST.MST_CEN_ID ='" + BASE._open_Cen_ID + "' ");
                if (MT_Table != null)
                    model.showMagazineSubType = MT_Table.AsEnumerable().Where(q => q["MST_MI_ID"].Equals(MagazineId)).Select(T =>
                        new MagazineSubType
                        {
                            Sr = T["Sr."].ToString(),
                            Type = T["Type"].ToString(),
                            ShortNameSub = T["Short Name"].ToString(),
                            StartMonth = T["Start Month"].ToString(),
                            St_Month = T["St_Month"].ToString(),
                            MinMonths = Convert.ToInt32(T["Min.Months"].ToString()),
                            FixedPeriod = T["Fixed Period"].ToString(),
                            PeriodwiseFee = T["Period wise Fee Calculation"].ToString(),
                            SubTypeID = T["ID"].ToString(),
                            Default = Convert.ToBoolean(T["Default"].ToString()),
                            MagListID = T["MST_MI_ID"].ToString(),

                            Add_By = T["Add By"].ToString(),
                            Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                            Edit_By = T["Edit By"].ToString(),
                            Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                            Action_Status = T["Action Status"].ToString(),
                            Action_By = T["Action By"].ToString(),
                            Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                        }).ToList();
                var MagSubTypeData = model.showMagazineSubType;
                Session["MagSubType_ExportData"] = MagSubTypeData;
                //Magazine Subtype List End

                //Magazine SubFees List Start
                var MagagineSubTypedata = Session["MagSubType_ExportData"] as List<MagazineSubType>;
                var MagazineSubTypeId = MagagineSubTypedata != null ? MagagineSubTypedata.Select(f => f.SubTypeID).FirstOrDefault() : "0";

                DataTable MF_Table = BASE._Magazine_DBOps.GetList_SubscriptionTypeFeeList(Voucher_Entry, Profile_Entry, "");
                if (MF_Table != null)
                    model.showMagazineSubFees = MF_Table.AsEnumerable().Where(q => q["MSTF_MST_ID"].Equals(MagazineSubTypeId)).Select(T =>
                      new MagazineSubFees
                      {
                          EffectiveDate = string.IsNullOrEmpty(T["Effective Date"].ToString()) ? "" : T["Effective Date"].ToString(),
                          IndianFee = Convert.ToDecimal(T["Indian Fee"].ToString()),
                          ForeignFee = Convert.ToDecimal(T["Foreign Fee"].ToString()),
                          SubFeesID = T["ID"].ToString(),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var MagSubFeesData = model.showMagazineSubFees;
                Session["MagSubFees_ExportData"] = MagSubFeesData;
                //Magazine SubFees List End

                //Magazine Issue List Start
                DataTable MI_Table = BASE._Magazine_DBOps.GetList_Issues();
                if (MI_Table != null)
                    model.showMagazineIssues = MI_Table.AsEnumerable().Select(T =>
                      new MagazineIssues
                      {
                          Magazine = T["Magazine"].ToString(),
                          IssueDate = string.IsNullOrEmpty(T["Issue Date"].ToString()) ? "" : Convert.ToDateTime(T["Issue Date"]).ToString("MM/dd/yyyy"),
                          PartNo = Convert.ToInt32(T["Part No"].ToString()),
                          VolNo = Convert.ToInt32(T["Vol. No."].ToString()),
                          IssueNo = Convert.ToInt32(T["Issue No."].ToString()),
                          RegSeed = Convert.ToInt32(T["Reg Seed"].ToString()),
                          PerCopyWeight = Convert.ToDecimal(T["Per Copy Weight"].ToString()),
                          BundleMaxFgn = Convert.ToDecimal(T["Bundle Max Fgn Weight"].ToString()),
                          BundleMax = Convert.ToDecimal(T["Bundle Max Weight"].ToString()),
                          PerCopyWeight1 = Convert.ToDecimal(T["Per Copy Weight1"].ToString()),
                          CopiesforAuto = Convert.ToInt32(T["Copies for Auto Registry"].ToString()),
                          CopiesforFgn = Convert.ToInt32(T["Copies for Fgn Registry"].ToString()),
                          RegExp = Convert.ToDecimal(T["Reg Exp"].ToString()),
                          RegFgnExp = Convert.ToDecimal(T["Reg Fgn Exp"].ToString()),
                          IssueID = T["ID"].ToString(),
                          Default = Convert.ToBoolean(T["Default"].ToString()),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var MagIssuesData = model.showMagazineIssues;
                Session["MagIssues_ExportData"] = MagIssuesData;
                //Magazine Issue List End

                Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(model);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Magazine').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Frm_Magazine_Window(string ActionMethod = null, string ID = null)
        {
            MagazineList model = new MagazineList();
            model.TempActionMethod = ActionMethod;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;

            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                DataTable _dtableTelData = BASE._Magazine_DBOps.GetRecord(ID);
                if (_dtableTelData.Rows.Count > 0)
                {
                    model.Name = _dtableTelData.Rows[0]["MI_NAME"].ToString();
                    model.ShortName = _dtableTelData.Rows[0]["MI_SHORT_NAME"].ToString();
                    model.Language = _dtableTelData.Rows[0]["MI_LANGUAGE"].ToString();
                    model.PublishOn = _dtableTelData.Rows[0]["MI_PUBLISH_ON"].ToString();
                    model.MagazineRegd = _dtableTelData.Rows[0]["MI_MAG_REGD_NO"].ToString();
                    model.PostalRegdNo = _dtableTelData.Rows[0]["MI_POSTAL_REGD_NO"].ToString();
                    model.MembershipStart = Convert.ToInt32(_dtableTelData.Rows[0]["MI_MS_START_NO"].ToString());
                    model.Foreign = _dtableTelData.Rows[0]["MI_FS_APPLICABLE"].ToString();
                    model.ID = ID;
                }
            }
            return View(model);
        }

        public ActionResult Frm_Magazine_ST_Window(string ActionMethod = null, string MagListID = null, string Subtypeid = null)
        {
            MagazineSubType Sub = new MagazineSubType();
            Sub.TempActionMethod = ActionMethod;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Sub.ActionMethod = Navigation_Mode_tag;
            Sub.TempActionMethod = ActionMethod;
            Sub.MagListID = MagListID;
            Sub.FixedPeriod = "NO";
            Sub.PeriodwiseFee = "NO";

            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                DataTable _dtableTelData = BASE._Magazine_DBOps.GetRecord_Subs_Type(Subtypeid);
                if (_dtableTelData.Rows.Count > 0)
                {
                    Sub.Type = _dtableTelData.Rows[0]["MST_TYPE"].ToString();
                    Sub.ShortNameSub = _dtableTelData.Rows[0]["MST_SHORT_NAME"].ToString();
                    Sub.StartMonth = _dtableTelData.Rows[0]["MST_START_MONTH"].ToString();
                    Sub.MinMonths = Convert.ToInt32(_dtableTelData.Rows[0]["MST_MIN_MONTHS"].ToString());
                    Sub.FixedPeriod = _dtableTelData.Rows[0]["MST_FIXED_PERIOD"].ToString();
                    Sub.PeriodwiseFee = _dtableTelData.Rows[0]["MST_FEE_PERIOD_WISE"].ToString();
                    Sub.SubTypeID = Subtypeid;
                    Sub.MagListID = MagListID;
                    Sub.Sr = _dtableTelData.Rows[0]["MST_SR_NO"].ToString();
                }
            }
            return View(Sub);
        }

        public ActionResult Frm_Magazine_STF_Window(string ActionMethod = null, string SubTypeID = null, string SubFeesID = null)
        {
            MagazineSubFees Fees = new MagazineSubFees();
            Fees.TempActionMethod = ActionMethod;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Fees.ActionMethod = Navigation_Mode_tag;
            Fees.TempActionMethod = ActionMethod;
            Fees.MagSubTypeID = SubTypeID;

            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                DataTable _dtableTelData = BASE._Magazine_DBOps.GetRecord_Subs_Type_Fee(SubFeesID);
                if (_dtableTelData.Rows.Count > 0)
                {
                    Fees.EffectiveDate = string.IsNullOrEmpty(_dtableTelData.Rows[0]["MSTF_EFF_DATE"].ToString()) ? "" : _dtableTelData.Rows[0]["MSTF_EFF_DATE"].ToString();
                    Fees.IndianFee = Convert.ToDecimal(_dtableTelData.Rows[0]["MSTF_INDIAN_FEE"].ToString());
                    Fees.ForeignFee = Convert.ToDecimal(_dtableTelData.Rows[0]["MSTF_FOREIGN_FEE"].ToString());
                    Fees.SubFeesID = SubFeesID;
                    Fees.MagSubTypeID = SubTypeID;
                }
            }
            return View(Fees);
        }
        public ActionResult Frm_Magazine_Issues_Window(string ActionMethod = null, string MagListID = null, string MagazineName = null, string IssueID = null, string Magazine = null, string IssueDate = null,
            int PartNo = 0, int VolNo = 0, int IssueNo = 0, int RegSeed = 0, decimal PerCopyWeight = 0,
            decimal BundleMaxFgn = 0, decimal BundleMax = 0, decimal PerCopyWeight1 = 0, int CopiesforAuto = 0, int CopiesforFgn = 0, decimal RegExpval = 0, decimal RegFgnExp = 0)
        {
            MagazineIssues Issues = new MagazineIssues();
            Issues.TempActionMethod = ActionMethod;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Issues.ActionMethod = Navigation_Mode_tag;
            Issues.TempActionMethod = ActionMethod;
            Issues.MagListID = MagListID;

            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                Issues.Magazine = Magazine;
                Issues.IssueDate = IssueDate;
                Issues.PartNo = PartNo;
                Issues.VolNo = VolNo;
                Issues.IssueNo = IssueNo;
                Issues.RegSeed = RegSeed;
                Issues.PerCopyWeight = PerCopyWeight;
                Issues.BundleMaxFgn = BundleMaxFgn;
                Issues.BundleMax = BundleMax;
                Issues.PerCopyWeight1 = PerCopyWeight1;
                Issues.CopiesforAuto = CopiesforAuto;
                Issues.CopiesforFgn = CopiesforFgn;
                Issues.RegExp = RegExpval;
                Issues.RegFgnExp = RegFgnExp;

            }
            else
            {
                Issues.Magazine = MagazineName;
            }
            return View(Issues);
        }

        public ActionResult Frm_Magazine_Membership_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Magazine_Membership, "List"))//Code written for User Authorization do not remove
            {
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#Magazine_Membership').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Frm_SubCity_Window()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Magazine_SubCity, "List"))//Code written for User Authorization do not remove
            {
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#Magazine_SubCity').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Frm_Voucher_Magazine_Membership()
        {
            return View();
        }
        public ActionResult Frm_Magazine_Subs_Window()
        {
            return View();
        }
        public ActionResult Frm_Voucher_Magazine_Membership_Payment()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Frm_Address_Info_Window_Small(string ActionMethod = "", string RecordID = "", string RecordTxtName = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            var Navigation_Mode_tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;
            model.xID = RecordID;
            if (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._New ||
                Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit ||
                Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
            {
            }
            if (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit ||
                   Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
            {
                Data_Binding(model, RecordID);
            }
            if (Navigation_Mode_tag == Common.Navigation_Mode._New || (Navigation_Mode_tag == Common.Navigation_Mode._New && RecordTxtName.Length > 0))
            {
                model.Rad_city_OthCity = "CITY";
                model.CityRadio = "checked";
                model.PC_City_Name_Read_only = false;
                model.Txt_R_Other_City_Readonly = true;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Address_Info_Window_Small(string TempActionMethod = "", string xID = "", string org_RegId = "", string Txt_Name = "", string Txt_R_Add1 = "", string Txt_R_Add2 = "", string Txt_R_Add3 = "",
            string Txt_R_Add4 = "", string PC_City_Name = "", string Txt_R_Other_City = "", string GLookUp_RStateList = "", string GLookUp_RDistrictList = "", string GLookUp_RCountryList = "", string Txt_R_Pincode = "",
            string Txt_Mob_1 = "", string Txt_Email1 = "", string Txt_Remark1 = "", string Txt_Remark2 = "", string Txt_R_Tel_1 = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + TempActionMethod);
            if (BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.SuperUser.ToUpper() && BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.Auditor.ToUpper())
            {
                if (BASE._CenterDBOps.IsFinalAuditCompleted())
                {
                    return Json(
                        new
                        {
                            Message = Common_Lib.Messages.RecordChanged("Party Cannot be added / updated / deleted . . . !</br></br>") + " Final Audit has been Completed for this year.",
                            resultType = "Information...",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                }
            }
            //If Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.SuperUser.ToUpper AndAlso Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
            //If Base._CenterDBOps.IsFinalAuditCompleted() Then
            //DevExpress.XtraEditors.XtraMessageBox.Show("Party Cannot be added / updated / deleted . . . !" & vbNewLine & vbNewLine & "Final Audit has been Completed for this year.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
            //FormClosingEnable = False : Me.Close()
            //Exit Sub
            //End If
            //End If

            //'-------------------------------------+
            //'Start : Check if entry already changed 
            //'-------------------------------------+
            if (BASE.AllowMultiuser())
            {
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    DataTable address_DbOps = BASE._Address_DBOps.GetRecord(xID);
                    if (address_DbOps == null)
                    {
                        return Json(
                            new
                            {
                                Message = "something went wrong!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                    }
                    if (address_DbOps.Rows.Count == 0)
                    {
                        return Json(
                            new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Contact") + "Record Already Changed!!",
                                resultType = "OK",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                    }
                    string LastEditedOn = address_DbOps.Rows[0]["REC_EDIT_ON"].ToString();
                    if (LastEditedOn != address_DbOps.Rows[0]["REC_EDIT_ON"].ToString())
                    {
                        return Json(
                            new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Contact") + "Record Already Changed!!",
                                resultType = "OK",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._Address_DBOps.GetStatus(xID);
                        if (MaxValue == null)
                        {
                            return Json(
                                new
                                {
                                    Message = Common_Lib.Messages.RecordChanged("E n t r y   N o t   F o u n d . . . !"),
                                    resultType = "OK",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                        }
                        bool DeleteAllow = false;
                        string UsedPage = "";
                        DataTable MaxCount = null;
                        //foreach (DataRow cRow in BASE._Address_DBOps.GetAddressRecIDs_ForAllYears(model.xID).Rows)
                        //{
                        DeleteAllow = CheckAddressUsage(xID, ref UsedPage);
                        //}

                        if (!DeleteAllow)
                        {
                            return Json(
                                new
                                {
                                    Message = Common_Lib.Messages.RecordChanged("C a n ' t   D e l e t e . . . !\n\n") + "T h i s   c o n t a c t   i s   b e i n g   r e f e r r e d   i n   A n o t h e r   r e c o r d   i n   c u r r e n t   o r   o t h e r   y e a r s . . . !\n\n Name : " + UsedPage,
                                    resultType = "Warning",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // '------------------------------------+
                // 'End : Check if entry already changed 
                // '------------------------------------+
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    DataTable d1 = BASE._Membership_DBOps.GetUsageAsPastMember(org_RegId);

                    d1 = BASE._Donation_DBOps.GetUsageAsPastDonor(xID);
                    if (d1.Rows.Count > 0 && (Txt_R_Add1.Trim().Length <= 0 || PC_City_Name.Length == 0 || GLookUp_RDistrictList.Length == 0 || GLookUp_RDistrictList.Length == 0 || GLookUp_RCountryList.Length == 0))
                    {
                        return Json(
                            new
                            {
                                Message = Common_Lib.Messages.RecordChanged("A d d r e s s    I n c o m p l e t e    f o r   a   E x i s t i n g    D o n o r . . . !\n\n") + "Residence Address Line1 , City, District, State, Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.",
                                resultType = "Information...",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                    }
                    d1 = BASE._Donation_DBOps.GetUsageAsPastForeignDonor(xID);
                    if (d1.Rows.Count > 0 && (Txt_R_Add1.Trim().Length <= 0 || PC_City_Name.Trim().Length == 0 || GLookUp_RCountryList.Length == 0))
                    {
                        return Json(
                            new
                            {
                                Message = Messages.RecordChanged("A d d r e s s    I n c o m p l e t e    f o r   a   E x i s t i n g    D o n o r . . . !\n\n") + "Residence Address Line1 , City, Country must be mentioned for a Party used in Foreign Donation Voucher.",
                                resultType = "Information...",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                    }
                }
                int I = 0; string SelectedItems = "";
                //Need to Handle in Javascript
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (Txt_Name.Length == 0 || Txt_Name.Trim() == "-- Not Specified --")
                    {
                        return Json(
                            new
                            {
                                Message = Messages.RecordChanged("N a m e   c a n n o t   b e   B l a n k . . . !"),
                                result = false
                            });
                    }
                    else
                    {

                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Insert_Addresses InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_Addresses();
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
                {
                    xID = System.Guid.NewGuid().ToString();
                    string ReplicationID = System.Guid.NewGuid().ToString();
                    string STR1 = "";
                    bool Result = true;
                    Common_Lib.RealTimeService.Parameter_Insert_Addresses InParam = new Common_Lib.RealTimeService.Parameter_Insert_Addresses();
                    InParam.Title = "";
                    InParam.Name = Txt_Name.Replace("'", "`");
                    InParam.Gender = "";
                    InParam.OrgName = "";
                    InParam.Designation = "";
                    InParam.Education = "";
                    InParam.Reference = "";
                    InParam.Remarks1 = Txt_Remark1.Replace("'", "`");
                    InParam.Remarks2 = Txt_Remark2.Replace("'", "`");
                    InParam.BloodGroup = "";
                    InParam.PANNo = "";
                    InParam.VAT_TIN = "";
                    InParam.CST_TIN = "";
                    InParam.TAN = "";
                    InParam.UID = "";
                    InParam.STRNo = "";
                    InParam.PassportNo = "";
                    InParam.Magazine = null;
                    InParam.Res_Add1 = Txt_R_Add1.Replace("'", "`");
                    InParam.Res_Add2 = Txt_R_Add2.Replace("'", "`");
                    InParam.Res_Add3 = Txt_R_Add3.Replace("'", "`");
                    InParam.Res_Add4 = Txt_R_Add4.Replace("'", "`");
                    if (model.PC_City_Name != null)
                    {
                        InParam.Res_cityID = PC_City_Name.Length == 0 ? "" : PC_City_Name;
                    }
                    else
                    {
                        InParam.Res_cityID = "";
                    }
                    InParam.Res_city = Txt_R_Other_City.Length == 0 ? "" : Txt_R_Other_City.Replace("'", "`");
                    if (GLookUp_RStateList != null)
                    {
                        InParam.Res_StateID = GLookUp_RStateList.Length == 0 ? "" : GLookUp_RStateList;
                    }
                    if (GLookUp_RDistrictList != null)
                    {
                        InParam.Res_DisttID = GLookUp_RDistrictList.Length == 0 ? "" : GLookUp_RDistrictList;
                    }
                    if (GLookUp_RCountryList != null)
                    {
                        InParam.Res_CountryID = GLookUp_RCountryList.Length == 0 ? "" : GLookUp_RCountryList;
                    }
                    //InParam.SubCityID = IIf(Len(Me.SubCityID) = 0, Nothing, Me.SubCityID)
                    InParam.Res_PinCode = Txt_R_Pincode.Replace("'", "`");
                    InParam.ResTel1 = Txt_R_Tel_1.Replace("'", "`");
                    InParam.Mob1 = Txt_Mob_1.Replace("'", "`");
                    InParam.Email1 = Txt_Email1.Replace("'", "`");
                    //InParam.Status_Action =  (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status),"_Common_Lib.Common.Record_Status._Completed)
                    InParam.Rec_ID = xID;
                    InParam.OrgAB_RecId = xID;
                    InParam.YearID = Convert.ToInt32(BASE._open_Year_ID);
                    InNewParam.param_InsertAddresses = InParam;
                    if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_Addresses InParam_NextYear = new Common_Lib.RealTimeService.Parameter_Insert_Addresses();
                        CopyObject(InParam, InParam_NextYear);
                        InParam_NextYear.Rec_ID = ReplicationID;
                        InParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        InNewParam.param_InsertAddresses_NextYear = InParam_NextYear;
                    }
                    //'START :Add Magazine Info 
                    int ctr = 0;
                    Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];

                    InNewParam.InsertMagazine = InMagInfo;
                    InNewParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                    //'END MAGAZINE Addition
                    //'START :ADD WINGS Info 
                    Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                    InNewParam.InsertWings = InWingInfo;
                    InNewParam.InsertWings_NextYear = InWingInfo_NextYear;
                    //'END WINGS Addition
                    //'START :ADD SPECIALITIES Info
                    Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                    InNewParam.InsertSpecialities = InSpecInfo;
                    InNewParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                    //'END SPECIALITIES Addition

                    //'START :ADD EVENTS Info 
                    Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                    InNewParam.InsertEvents = InEventInfo;
                    InNewParam.InsertEvents_NextYear = InEventInfo_NextYear;
                    //'END EVENTS Addition

                    Common_Lib.RealTimeService.Param_Get_Duplicates param = new Common_Lib.RealTimeService.Param_Get_Duplicates();
                    param.insertPAram = InNewParam.param_InsertAddresses;
                    param.Rec_ID = xID;
                    object Messages = BASE._Address_DBOps.GetDuplicateColumnMsg(param);

                    if (Messages == null)
                    {
                        return Json(
                           new
                           {
                               Message = "something went wrong!!",
                               result = false
                           }, JsonRequestBehavior.AllowGet);
                    }

                    if (Messages.ToString().Length > 0)
                    {
                        var SuccessParams = new
                        {
                            tempActionMethod = TempActionMethod,
                            XID = xID,
                            Org_RegId = org_RegId,
                            txt_Name = Txt_Name,
                            txt_R_Add1 = Txt_R_Add1,
                            txt_R_Add2 = Txt_R_Add2,
                            txt_R_Add3 = Txt_R_Add3,
                            txt_R_Add4 = Txt_R_Add4,
                            pC_City_Name = PC_City_Name,
                            txt_R_Other_City = Txt_R_Other_City,
                            gLookUp_RStateList = GLookUp_RStateList,
                            gLookUp_RDistrictList = GLookUp_RDistrictList,
                            gLookUp_RCountryList = GLookUp_RCountryList,
                            txt_R_Pincode = Txt_R_Pincode,
                            txt_Mob_1 = Txt_Mob_1,
                            txt_Email1 = Txt_Email1,
                            txt_Remark1 = Txt_Remark1,
                            txt_Remark2 = Txt_Remark2,
                            txt_R_Tel_1 = Txt_R_Tel_1
                        };
                        //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                        //if (DialogResult.No == xPromptWindow.ShowDialog("Some Possible Duplicates!", Message + Constants.vbNewLine + "Do you still want to insert the Record?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._799x300, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue))
                        //{
                        //    return;
                        //}
                        return Json(new { successParams = SuccessParams, Message = Messages, result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (!BASE._Address_DBOps.InsertAddresses_Txn(InNewParam))
                    {
                        return Json(
                             new
                             {
                                 Message = "Not Success",
                                 result = false
                             }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(
                            new
                            {
                                Message = "New record added",
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Update_Addresses EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_Addresses();
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    bool ReplicateChange = false;
                    string ReplicationRecID = "";
                    // If Base._next_Unaudited_YearID<> Nothing Then
                    // Dim xPromptWindow As New Common_Lib.Prompt_Window
                    // If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Do you Want to make same changes in this Contact in year " & Base._next_Unaudited_YearID.ToString.Substring(0, 2) & "-" & Base._next_Unaudited_YearID.ToString.Substring(2, 2) & " too?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    // ReplicateChange = True
                    // ReplicationRecID = Base._Address_DBOps.GetAddressRecID(Me.xID.Text, Base._next_Unaudited_YearID)
                    // xPromptWindow.Dispose()
                    // End If
                    // End If
                    bool Result = true;
                    Common_Lib.RealTimeService.Parameter_Update_Addresses UpParam = new Common_Lib.RealTimeService.Parameter_Update_Addresses();
                    UpParam.Name = Txt_Name.Replace("'", "`");
                    UpParam.Remarks1 = Txt_Remark1.Replace("'", "`");
                    UpParam.Remarks2 = Txt_Remark2.Replace("'", "`");
                    UpParam.Res_Add1 = Txt_R_Add1.Replace("'", "`");
                    UpParam.Res_Add2 = Txt_R_Add2.Replace("'", "`");
                    UpParam.Res_Add3 = Txt_R_Add3.Replace("'", "`");
                    UpParam.Res_Add4 = Txt_R_Add4.Replace("'", "`");

                    if (PC_City_Name != null)
                    {
                        //UpParam.Res_cityID = IIf(Len(model.PC_City_Name.Tag) = 0, Nothing, Me.PC_City_Name.Tag)
                        UpParam.Res_cityID = PC_City_Name.Length == 0 ? "" : PC_City_Name;
                    }
                    else
                    {
                        UpParam.Res_cityID = "";
                    }
                    if (Txt_R_Other_City != null)
                    {
                        UpParam.Res_city = Txt_R_Other_City.Length == 0 ? "" : Txt_R_Other_City.Replace("'", "`");
                    }


                    if (GLookUp_RStateList != null)
                    {
                        UpParam.Res_StateID = GLookUp_RStateList.Length == 0 ? "" : GLookUp_RStateList;
                    }
                    if (GLookUp_RDistrictList != null)
                    {
                        UpParam.Res_DisttID = GLookUp_RDistrictList.Length == 0 ? "" : GLookUp_RDistrictList;
                    }
                    if (GLookUp_RCountryList != null)
                    {
                        UpParam.Res_CountryID = GLookUp_RCountryList.Length == 0 ? "" : GLookUp_RCountryList;
                    }
                    //UpParam.SubCityID = (((model.SubCityID.Length == 0)  || (Rad_city_OthCity.SelectedIndex == 1)) ? null : this.SubCityID);
                    UpParam.Res_PinCode = Txt_R_Pincode.Replace("\'", "`");
                    UpParam.ResTel1 = Txt_R_Tel_1.Replace("'", "`");
                    UpParam.Mob1 = Txt_Mob_1.Replace("'", "`");
                    UpParam.Email1 = Txt_Email1.Replace("'", "`");
                    UpParam.Status = "";
                    UpParam.Rec_ID = xID;
                    EditParam.param_UpdateAddresses = UpParam;
                    if (ReplicateChange)
                    {
                        Common_Lib.RealTimeService.Parameter_Update_Addresses UpParam_NextYear = new Common_Lib.RealTimeService.Parameter_Update_Addresses();
                        CopyObject(UpParam, UpParam_NextYear);
                        UpParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        UpParam_NextYear.ReplicationUpdate = true;
                        EditParam.param_UpdateAddresses_NextYear = UpParam_NextYear;
                    }
                    //'START :Update Magazine Info 
                    //'Delete Magazines for current Contact and then add them all
                    EditParam.RecID_DeleteMagazine = xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteMagazine_NextYear = ReplicationRecID;
                    }
                    Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                    EditParam.InsertMagazine = InMagInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                    }
                    //''END MAGAZINE UPDATE
                    //''START: Update WINGS Info
                    //''Delete WINGS for current Contact and then add them all
                    EditParam.RecID_DelteWings = xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DelteWings_NextYear = ReplicationRecID;
                    }
                    Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                    EditParam.InsertWings = InWingInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertWings_NextYear = InWingInfo_NextYear;
                    }
                    //''END WINGS UPDATE
                    //''START: Update SPECIALITIES Info
                    //''Delete SPECIALITIES for current Contact and then add them all
                    EditParam.RecID_DeleteSpeciality = xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteSpeciality_NextYear = ReplicationRecID;
                    }

                    Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                    EditParam.InsertSpecialities = InSpecInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                    }
                    //''END SPECIALITIES UPDATE
                    //''START: Update EVENTS Info
                    //''Delete EVENTS for current Contact and then add them all
                    EditParam.RecID_DeleteEvents = xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteEvents_NextYear = ReplicationRecID;
                    }
                    Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventsInfo = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                    Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                    EditParam.InsertEvents = InEventsInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertEvents_NextYear = InEventInfo_NextYear;
                    }
                    //''END EVENTS UPDATE

                    Common_Lib.RealTimeService.Param_Get_Duplicates param = new Common_Lib.RealTimeService.Param_Get_Duplicates();
                    param.updatePAram = EditParam.param_UpdateAddresses;
                    param.Rec_ID = model.xID;
                    object Messages = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                    if (Messages == null)
                    {
                        return Json(
                            new
                            {
                                Message = "Error",
                                result = false
                            });
                    }



                    if (Messages.ToString().Length > 0)
                    {
                        var SuccessParams = new
                        {
                            tempActionMethod = TempActionMethod,
                            XID = xID,
                            Org_RegId = org_RegId,
                            txt_Name = Txt_Name,
                            txt_R_Add1 = Txt_R_Add1,
                            txt_R_Add2 = Txt_R_Add2,
                            txt_R_Add3 = Txt_R_Add3,
                            txt_R_Add4 = Txt_R_Add4,
                            pC_City_Name = PC_City_Name,
                            txt_R_Other_City = Txt_R_Other_City,
                            gLookUp_RStateList = GLookUp_RStateList,
                            gLookUp_RDistrictList = GLookUp_RDistrictList,
                            gLookUp_RCountryList = GLookUp_RCountryList,
                            txt_R_Pincode = Txt_R_Pincode,
                            txt_Mob_1 = Txt_Mob_1,
                            txt_Email1 = Txt_Email1,
                            txt_Remark1 = Txt_Remark1,
                            txt_Remark2 = Txt_Remark2,
                            txt_R_Tel_1 = Txt_R_Tel_1
                        };
                        //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                        //if (DialogResult.No == xPromptWindow.ShowDialog("Some Possible Duplicates!", Message + Constants.vbNewLine + "Do you still want to insert the Record?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._799x300, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue))
                        //{
                        //    return;
                        //}
                        return Json(new { successParams = SuccessParams, Message = Messages, result = true }, JsonRequestBehavior.AllowGet);
                    }

                    if (!BASE._Address_DBOps.UpdateAddresses_Txn(EditParam))
                    {
                        return Json(new
                        {
                            Message = "UnSuccessfull",
                            result = false
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Successfully Updated",
                            result = true
                        });
                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Delete_Addresses DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_Addresses();
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    bool Result = true;
                    DataTable Table = BASE._Address_DBOps.GetAddressRecIDs_ForAllYears(model.xID);
                    Common_Lib.RealTimeService.Param_Txn_Delete_AddressSet[] DelAddressSetsAll = new Common_Lib.RealTimeService.Param_Txn_Delete_AddressSet[Table.Rows.Count + 1];
                    foreach (DataRow cRow in Table.Rows)
                    {
                        Common_Lib.RealTimeService.Param_Txn_Delete_AddressSet DelAddressSet = new Common_Lib.RealTimeService.Param_Txn_Delete_AddressSet();
                        DelAddressSet.RecID_DeleteMagazine = cRow[0].ToString();
                        DelAddressSet.RecID_DelteWings = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteSpeciality = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteEvents = cRow[0].ToString();
                        DelAddressSet.RecID_Delete = cRow[0].ToString();
                        //DelAddressSetsAll[ctr] = DelAddressSet;
                    }
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult xPromptWindow(string PromptMsg = "", string tempActionMethod = "", string xID = "", string Org_RegId = "", string Txt_Name = "", string Txt_R_Add1 = "",
            string Txt_R_Add2 = "", string Txt_R_Add3 = "", string Txt_R_Add4 = "", string PC_City_Name = "", string Txt_R_Other_City = "", string GLookUp_RStateList = "", string GLookUp_RDistrictList = "",
            string GLookUp_RCountryList = "", string Txt_R_Pincode = "", string Txt_Mob_1 = "", string Txt_Email1 = "", string Txt_Remark1 = "", string Txt_Remark2 = "", string Txt_R_Tel_1 = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            model.xID = xID;
            model.org_RegId = Org_RegId;
            model.PromptMsg = PromptMsg;
            model.Txt_Name = Txt_Name;
            model.Txt_R_Add1 = Txt_R_Add1;
            model.Txt_R_Add2 = Txt_R_Add2;
            model.Txt_R_Add3 = Txt_R_Add3;
            model.Txt_R_Add4 = Txt_R_Add4;
            model.PC_City_Name = PC_City_Name;
            model.Txt_R_Other_City = Txt_R_Other_City;
            model.GLookUp_RStateList = GLookUp_RStateList;
            model.GLookUp_RDistrictList = GLookUp_RDistrictList;
            model.GLookUp_RCountryList = GLookUp_RCountryList;
            model.Txt_R_Pincode = Txt_R_Pincode;
            model.Txt_R_Tel_1 = Txt_R_Tel_1;
            model.Txt_Mob_1 = Txt_Mob_1;
            model.Txt_Email1 = Txt_Email1;
            model.Txt_Remark1 = Txt_Remark1;
            model.Txt_Remark2 = Txt_Remark2;
            model.Txt_R_Tel_1 = Txt_R_Tel_1;
            return View(model);
        }
        [HttpPost]
        public ActionResult xPromptWindow(string TempActionMethod = "", string xID = "", string org_RegId = "", string Txt_Name = "", string Txt_R_Add1 = "", string Txt_R_Add2 = "", string Txt_R_Add3 = "", string Txt_R_Add4 = "",
            string PC_City_Name = "", string Txt_R_Other_City = "", string GLookUp_RStateList = "", string GLookUp_RDistrictList = "", string GLookUp_RCountryList = "", string Txt_R_Pincode = "", string Txt_R_Tel_1 = "",
            string Txt_Mob_1 = "", string Txt_Email1 = "", string Txt_Remark1 = "", string Txt_Remark2 = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + TempActionMethod);
            Common_Lib.RealTimeService.Param_Txn_Insert_Addresses InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_Addresses();
            if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
            {
                model.xID = System.Guid.NewGuid().ToString();
                string ReplicationID = System.Guid.NewGuid().ToString();
                string STR1 = "";
                bool Result = true;
                Common_Lib.RealTimeService.Parameter_Insert_Addresses InParam = new Common_Lib.RealTimeService.Parameter_Insert_Addresses();
                InParam.Title = "";
                InParam.Name = Txt_Name.Replace("'", "`");
                InParam.Gender = "";
                InParam.OrgName = "";
                InParam.Designation = "";
                InParam.Education = "";
                InParam.Reference = "";
                InParam.Remarks1 = Txt_Remark1.Replace("'", "`");
                InParam.Remarks2 = Txt_Remark2.Replace("'", "`");
                InParam.BloodGroup = "";
                InParam.PANNo = "";
                InParam.VAT_TIN = "";
                InParam.CST_TIN = "";
                InParam.TAN = "";
                InParam.UID = "";
                InParam.STRNo = "";
                InParam.PassportNo = "";
                InParam.Magazine = null;
                InParam.Res_Add1 = Txt_R_Add1.Replace("'", "`");
                InParam.Res_Add2 = Txt_R_Add2.Replace("'", "`");
                InParam.Res_Add3 = Txt_R_Add3.Replace("'", "`");
                InParam.Res_Add4 = Txt_R_Add4.Replace("'", "`");
                if (PC_City_Name != null && PC_City_Name != "")
                {
                    InParam.Res_cityID = PC_City_Name.Length == 0 ? "" : PC_City_Name;
                }
                else
                {
                    InParam.Res_cityID = "";
                }
                InParam.Res_city = Txt_R_Other_City.Length == 0 ? "" : Txt_R_Other_City.Replace("'", "`");
                if (GLookUp_RStateList != null && GLookUp_RStateList != null)
                {
                    InParam.Res_StateID = GLookUp_RStateList.Length == 0 ? "" : GLookUp_RStateList;
                }
                if (GLookUp_RDistrictList != null && GLookUp_RDistrictList != "")
                {
                    InParam.Res_DisttID = GLookUp_RDistrictList.Length == 0 ? "" : GLookUp_RDistrictList;
                }
                if (GLookUp_RCountryList != null)
                {
                    InParam.Res_CountryID = GLookUp_RCountryList.Length == 0 ? "" : GLookUp_RCountryList;
                }
                //InParam.SubCityID = IIf(Len(Me.SubCityID) = 0, Nothing, Me.SubCityID)
                InParam.Res_PinCode = Txt_R_Pincode.Replace("'", "`");
                InParam.ResTel1 = Txt_R_Tel_1.Replace("'", "`");
                InParam.Mob1 = Txt_Mob_1.Replace("'", "`");
                InParam.Email1 = Txt_Email1.Replace("'", "`");
                //InParam.Status_Action =  (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status),"_Common_Lib.Common.Record_Status._Completed)
                InParam.Rec_ID = xID;
                InParam.OrgAB_RecId = xID;
                InParam.YearID = Convert.ToInt32(BASE._open_Year_ID);
                InNewParam.param_InsertAddresses = InParam;
                if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                {
                    Common_Lib.RealTimeService.Parameter_Insert_Addresses InParam_NextYear = new Common_Lib.RealTimeService.Parameter_Insert_Addresses();
                    CopyObject(InParam, InParam_NextYear);
                    InParam_NextYear.Rec_ID = ReplicationID;
                    InParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                    InNewParam.param_InsertAddresses_NextYear = InParam_NextYear;
                }
                //'START :Add Magazine Info 
                int ctr = 0;
                Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];

                InNewParam.InsertMagazine = InMagInfo;
                InNewParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                //'END MAGAZINE Addition
                //'START :ADD WINGS Info 
                Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                InNewParam.InsertWings = InWingInfo;
                InNewParam.InsertWings_NextYear = InWingInfo_NextYear;
                //'END WINGS Addition
                //'START :ADD SPECIALITIES Info
                Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                InNewParam.InsertSpecialities = InSpecInfo;
                InNewParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                //'END SPECIALITIES Addition

                //'START :ADD EVENTS Info 
                Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                InNewParam.InsertEvents = InEventInfo;
                InNewParam.InsertEvents_NextYear = InEventInfo_NextYear;
                //'END EVENTS Addition

                Common_Lib.RealTimeService.Param_Get_Duplicates param = new Common_Lib.RealTimeService.Param_Get_Duplicates();
                param.insertPAram = InNewParam.param_InsertAddresses;
                param.Rec_ID = model.xID;
                if (!BASE._Address_DBOps.InsertAddresses_Txn(InNewParam))
                {
                    return Json(
                         new
                         {
                             Message = "Not Success",
                             result = false
                         }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(
                        new
                        {
                            Message = "New record added",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                }
            }
            Common_Lib.RealTimeService.Param_Txn_Update_Addresses EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_Addresses();
            if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
            {
                bool ReplicateChange = false;
                string ReplicationRecID = "";
                // If Base._next_Unaudited_YearID<> Nothing Then
                // Dim xPromptWindow As New Common_Lib.Prompt_Window
                // If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Do you Want to make same changes in this Contact in year " & Base._next_Unaudited_YearID.ToString.Substring(0, 2) & "-" & Base._next_Unaudited_YearID.ToString.Substring(2, 2) & " too?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                // ReplicateChange = True
                // ReplicationRecID = Base._Address_DBOps.GetAddressRecID(Me.xID.Text, Base._next_Unaudited_YearID)
                // xPromptWindow.Dispose()
                // End If
                // End If
                bool Result = true;
                Common_Lib.RealTimeService.Parameter_Update_Addresses UpParam = new Common_Lib.RealTimeService.Parameter_Update_Addresses();
                UpParam.Name = Txt_Name.Replace("'", "`");
                UpParam.Remarks1 = Txt_Remark1.Replace("'", "`");
                UpParam.Remarks2 = Txt_Remark2.Replace("'", "`");
                UpParam.Res_Add1 = Txt_R_Add1.Replace("'", "`");
                UpParam.Res_Add2 = Txt_R_Add2.Replace("'", "`");
                UpParam.Res_Add3 = Txt_R_Add3.Replace("'", "`");
                UpParam.Res_Add4 = Txt_R_Add4.Replace("'", "`");

                if (PC_City_Name != null)
                {
                    //UpParam.Res_cityID = IIf(Len(model.PC_City_Name.Tag) = 0, Nothing, Me.PC_City_Name.Tag)
                    UpParam.Res_cityID = PC_City_Name.Length == 0 ? "" : PC_City_Name;
                }
                else
                {
                    UpParam.Res_cityID = "";
                }
                if (Txt_R_Other_City != null)
                {
                    UpParam.Res_city = Txt_R_Other_City.Length == 0 ? "" : Txt_R_Other_City.Replace("'", "`");
                }


                if (GLookUp_RStateList != null)
                {
                    UpParam.Res_StateID = GLookUp_RStateList.Length == 0 ? "" : GLookUp_RStateList;
                }
                if (GLookUp_RDistrictList != null)
                {
                    UpParam.Res_DisttID = GLookUp_RDistrictList.Length == 0 ? "" : GLookUp_RDistrictList;
                }
                if (GLookUp_RCountryList != null)
                {
                    UpParam.Res_CountryID = GLookUp_RCountryList.Length == 0 ? "" : GLookUp_RCountryList;
                }

                //UpParam.SubCityID = (((model.SubCityID.Length == 0)  || (Rad_city_OthCity.SelectedIndex == 1)) ? null : this.SubCityID);
                UpParam.Res_PinCode = Txt_R_Pincode.Replace("\'", "`");
                UpParam.ResTel1 = Txt_R_Tel_1.Replace("'", "`");
                UpParam.Mob1 = Txt_Mob_1.Replace("'", "`");
                UpParam.Email1 = Txt_Email1.Replace("'", "`");
                UpParam.Status = "";
                UpParam.Rec_ID = xID;
                EditParam.param_UpdateAddresses = UpParam;
                if (ReplicateChange)
                {
                    Common_Lib.RealTimeService.Parameter_Update_Addresses UpParam_NextYear = new Common_Lib.RealTimeService.Parameter_Update_Addresses();
                    CopyObject(UpParam, UpParam_NextYear);
                    UpParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                    UpParam_NextYear.ReplicationUpdate = true;
                    EditParam.param_UpdateAddresses_NextYear = UpParam_NextYear;
                }
                //'START :Update Magazine Info 
                //'Delete Magazines for current Contact and then add them all
                EditParam.RecID_DeleteMagazine = xID;
                if (ReplicateChange)
                {
                    EditParam.RecID_DeleteMagazine_NextYear = ReplicationRecID;
                }
                Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertMagazine_Addresses[1];
                EditParam.InsertMagazine = InMagInfo;
                if (ReplicateChange)
                {
                    EditParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                }
                //''END MAGAZINE UPDATE
                //''START: Update WINGS Info
                //''Delete WINGS for current Contact and then add them all
                EditParam.RecID_DelteWings = xID;
                if (ReplicateChange)
                {
                    EditParam.RecID_DelteWings_NextYear = ReplicationRecID;
                }
                Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertWings_Addresses[1];
                EditParam.InsertWings = InWingInfo;
                if (ReplicateChange)
                {
                    EditParam.InsertWings_NextYear = InWingInfo_NextYear;
                }
                //''END WINGS UPDATE
                //''START: Update SPECIALITIES Info
                //''Delete SPECIALITIES for current Contact and then add them all
                EditParam.RecID_DeleteSpeciality = xID;
                if (ReplicateChange)
                {
                    EditParam.RecID_DeleteSpeciality_NextYear = ReplicationRecID;
                }

                Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertSpecialities_Addresses[1];
                EditParam.InsertSpecialities = InSpecInfo;
                if (ReplicateChange)
                {
                    EditParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                }
                //''END SPECIALITIES UPDATE
                //''START: Update EVENTS Info
                //''Delete EVENTS for current Contact and then add them all
                EditParam.RecID_DeleteEvents = xID;
                if (ReplicateChange)
                {
                    EditParam.RecID_DeleteEvents_NextYear = ReplicationRecID;
                }
                Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventsInfo = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Common_Lib.RealTimeService.Parameter_InsertEvents_Addresses[1];
                EditParam.InsertEvents = InEventsInfo;
                if (ReplicateChange)
                {
                    EditParam.InsertEvents_NextYear = InEventInfo_NextYear;
                }
                //''END EVENTS UPDATE

                Common_Lib.RealTimeService.Param_Get_Duplicates param = new Common_Lib.RealTimeService.Param_Get_Duplicates();
                param.updatePAram = EditParam.param_UpdateAddresses;
                param.Rec_ID = model.xID;
                object Messages = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                if (Messages == null)
                {
                    return Json(
                        new
                        {
                            Message = "Error",
                            result = false
                        });
                }



                //if (Messages.ToString().Length > 0)
                //{
                //    var SuccessParams = new
                //    {
                //        tempActionMethod = TempActionMethod,
                //        XID = xID,
                //        Org_RegId = org_RegId,
                //        txt_Name = Txt_Name,
                //        txt_R_Add1 = Txt_R_Add1,
                //        txt_R_Add2 = Txt_R_Add2,
                //        txt_R_Add3 = Txt_R_Add3,
                //        txt_R_Add4 = Txt_R_Add4,
                //        pC_City_Name = PC_City_Name,
                //        txt_R_Other_City = Txt_R_Other_City,
                //        gLookUp_RStateList = GLookUp_RStateList,
                //        gLookUp_RDistrictList = GLookUp_RDistrictList,
                //        gLookUp_RCountryList = GLookUp_RCountryList,
                //        txt_R_Pincode = Txt_R_Pincode,
                //        txt_Mob_1 = Txt_Mob_1,
                //        txt_Email1 = Txt_Email1,
                //        txt_Remark1 = Txt_Remark1,
                //        txt_Remark2 = Txt_Remark2,
                //        txt_R_Tel_1 = Txt_R_Tel_1
                //    };
                //    //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                //    //if (DialogResult.No == xPromptWindow.ShowDialog("Some Possible Duplicates!", Message + Constants.vbNewLine + "Do you still want to insert the Record?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._799x300, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue))
                //    //{
                //    //    return;
                //    //}
                //    return Json(new { successParams = SuccessParams, Message = Messages, result = true }, JsonRequestBehavior.AllowGet);
                //}

                if (!BASE._Address_DBOps.UpdateAddresses_Txn(EditParam))
                {
                    return Json(new
                    {
                        Message = "UnSuccessfull",
                        result = false
                    });
                }
                else
                {
                    return Json(new
                    {
                        Message = "Successfully Updated",
                        result = true
                    });
                }
            }
            //MagazineReciptReginfo model = new MagazineReciptReginfo();
            //model.PromptMsg = PromptMsg;
            return View(model);
        }
        private void CopyObject(object SourceObj, object DestObj)
        {
            PropertyInfo[] destinationProperties = DestObj.GetType().GetProperties();
            foreach (PropertyInfo destinationPI in destinationProperties)
            {
                PropertyInfo sourcePI = SourceObj.GetType().GetProperty(destinationPI.Name);
                destinationPI.SetValue(DestObj, sourcePI.GetValue(SourceObj, null), null);
            }

        }
        public bool CheckAddressUsage(string AB_ID, ref string UsedPage)
        {
            bool DeleteAllow = false;
            UsedPage = "";
            DataTable MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, AB_ID);
            if (MaxCount.Rows.Count > 0)
            {
                if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                {
                    DeleteAllow = false;
                    UsedPage = "Voucher Entry...";
                    if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                    {
                        UsedPage = (UsedPage + ("(Year:" + (MaxCount.Rows[0]["YEARID"] + ")")));
                    }
                    else
                    {

                    }
                    DeleteAllow = true;
                }
                if (DeleteAllow)
                {
                    MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.ADVANCES_INFO, AB_ID);
                    if (MaxCount.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                        {
                            DeleteAllow = false;
                        }
                        UsedPage = "Advances Information...";
                        if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                        {
                            UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                        }
                        DeleteAllow = true;
                    }
                }
                if (DeleteAllow)
                {
                    MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.DEPOSITS_INFO, AB_ID);
                    if (MaxCount.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                        {
                            DeleteAllow = false;
                        }
                        UsedPage = "Liabilities Information...";
                        if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                        {
                            UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                        }
                        else
                        {

                        }
                        DeleteAllow = true;
                    }
                }
                if (DeleteAllow)
                {
                    MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LIABILITIES_INFO, AB_ID);
                    if (MaxCount.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                        {
                            DeleteAllow = false;
                        }
                        UsedPage = "Liabilities Information...";
                        if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                        {
                            UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                        }
                        else
                        {

                        }
                        DeleteAllow = true;
                    }
                }
                if (DeleteAllow)
                {
                    MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, AB_ID);
                    if (MaxCount.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                        {
                            DeleteAllow = false;
                        }
                        UsedPage = "Vehicles Information...";
                        if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                        {
                            UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                        }
                        else
                        {

                        }
                        DeleteAllow = true;
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.BANK_ACCOUNT_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Bank Account Information...";
                            if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                            {
                                UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                            }
                            else
                            {

                            }
                            DeleteAllow = true;
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.CENTRE_SUPPORT_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Core Information...";
                        }
                        else
                        {
                            DeleteAllow = true;
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Land and Building Information...";
                            if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                            {
                                UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                            }
                        }
                        else
                        {
                            DeleteAllow = true;
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.SERVICE_PLACE_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Service Places Information...";
                            if (Convert.ToInt32(MaxCount.Rows[0]["YEARID"].ToString()) != BASE._open_Year_ID)
                            {
                                UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                            }
                            else
                            {
                                DeleteAllow = true;
                            }
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MEMBERSHIP_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Membership Information...";
                        }
                        else
                        {
                            DeleteAllow = true;
                        }
                    }
                    if (DeleteAllow)
                    {
                        MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MAGAZINE_MEMBERSHIP_INFO, AB_ID);
                        if (MaxCount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(MaxCount.Rows[0]["CNT"].ToString()) > 0)
                            {
                                DeleteAllow = false;
                            }
                            UsedPage = "Magazine Membership Register...";
                        }
                        else
                        {
                            DeleteAllow = true;
                        }
                    }
                }
            }
            return DeleteAllow;

        }
        // Public Function CheckAddressUsage(AB_ID As String, ByRef UsedPage As String) As Boolean
        // Dim DeleteAllow As Boolean = False : UsedPage = ""
        //Dim MaxCount As DataTable = Nothing
        //'1
        //MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, AB_ID)
        //If MaxCount.Rows.Count > 0 Then
        //If MaxCount.Rows(0)("CNT") > 0 Then
        //DeleteAllow = False : UsedPage = "Voucher Entry..."
        //If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //End If
        //Else
        //DeleteAllow = True
        //End If

        //If DeleteAllow Then
        //     MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.ADVANCES_INFO, AB_ID)
        //     If MaxCount.Rows.Count > 0 Then
        //          If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Advances Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //     Else : DeleteAllow = True
        //     End If
        //End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.DEPOSITS_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Other Deposits Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LIABILITIES_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Liabilities Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Vehicles Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.BANK_ACCOUNT_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Bank Account Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.CENTRE_SUPPORT_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Core Information..."
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Land and Building Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.SERVICE_PLACE_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Service Places Information..." : If MaxCount.Rows(0)("YEARID") <> Base._open_Year_ID Then UsedPage = UsedPage & "(Year:" & MaxCount.Rows(0)("YEARID") & ")"
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MEMBERSHIP_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Membership Information..."
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // If DeleteAllow Then
        //      MaxCount = Base._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MAGAZINE_MEMBERSHIP_INFO, AB_ID)
        //      If MaxCount.Rows.Count > 0 Then
        //           If MaxCount.Rows(0)("CNT") > 0 Then DeleteAllow = False : UsedPage = "Magazine Membership Register..."
        //      Else : DeleteAllow = True
        //      End If
        // End If
        // Return DeleteAllow
        // End Function
        public MagazineReciptRegInfo Data_Binding(MagazineReciptRegInfo model, string xID = "")
        {
            byte[] xPHOTO = new byte[] { };
            DataTable d1 = BASE._Address_DBOps.GetRecord(model.xID);
            /*------------------------------------------------------------------------------------------+
             Start :Checks for Existence of Record, and has not been deleted in background by other user 
             -------------------------------------------------------------------------------------------*/
            if (d1 == null)
            {
                model.message = Common_Lib.Messages.RecordChanged("Current Contact") + "Record Changed / Removed in Background!!";
                model.result = "false";
                return model;
                //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contact"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                //Me.DialogResult = Windows.Forms.DialogResult.Retry
                //FormClosingEnable = False : Me.Close()
                //Exit Sub
            }
            if (d1.Rows.Count == 0)
            {
                model.message = Common_Lib.Messages.RecordChanged("Current Contact") + "Record Changed / Removed in Background!!";
                model.result = "false";
                return model;

                //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contact"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                //Me.DialogResult = Windows.Forms.DialogResult.Retry
                //FormClosingEnable = False : Me.Close()
                //Exit Sub
            }
            /*--------------------------------------------------------------------------------------+
            End:Checks for Existence of Record, and has not been deleted in background by other user 
             ---------------------------------------------------------------------------------------+*/
            /*-------------------------------------+
              Start : Check if entry already changed 
              -------------------------------------+*/
            if (BASE.AllowMultiuser())
            {
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit ||
                    model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    DateTime? Info_LastEditedOn = null;
                    if (Info_LastEditedOn > DateTime.MinValue)
                    {
                        if (Info_LastEditedOn.ToString() != d1.Rows[0]["REC_EDIT_ON"].ToString())
                        {
                            model.message = Common_Lib.Messages.RecordChanged("Current Contact") + "Record Already Changed !!";
                            model.result = "false";
                            return model;
                        }
                    }
                }
            }
            //  If Base.AllowMultiuser() Then
            //   If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            //        If Info_LastEditedOn > DateTime.MinValue Then
            //             If Info_LastEditedOn<> Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON")) Then
            //                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contact"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //                    Me.DialogResult = Windows.Forms.DialogResult.Retry
            //                    FormClosingEnable = False : Me.Close()
            //                    Exit Sub
            //               End If
            //          End If
            //     End If
            //End If
            /*-----------------------------------+
             End : Check if entry already changed 
             ------------------------------------+*/
            //Dim dMag As DataTable = Base._Address_DBOps.GetMagazineRecords(Me.xID.Text)
            //Dim dWing As DataTable = Base._Address_DBOps.GetWingRecords(Me.xID.Text)
            //Dim dSpecial As DataTable = Base._Address_DBOps.GetSpecialityRecords(Me.xID.Text)
            //Dim dEvents As DataTable = Base._Address_DBOps.GetEventRecords(Me.xID.Text)
            DateTime? LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"].ToString());
            model.Org_RecID = d1.Rows[0]["C_ORG_REC_ID"].ToString();
            //LastEditedOn = Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON"))
            //Org_RecID = d1.Rows(0)("C_ORG_REC_ID")

            //1
            //XtraTabControl1.SelectedTabPage = Page_Basic
            //
            //Look_TitleList.EditValue = d1.Rows(0)("C_TITLE")
            //Look_TitleList.Tag = Look_TitleList.EditValue
            //Look_TitleList.Properties.Tag = "SHOW"

            model.Txt_Name = d1.Rows[0]["C_NAME"] == null ? "" : d1.Rows[0]["C_NAME"].ToString();
            //TODO:Issue
            //model.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.Txt_Name);


            //Txt_Name.DataBindings.Add("text", d1, "C_NAME")
            //Txt_Name.Tag = Txt_Name.Text
            //Rad_Gender.DataBindings.Add("EditValue", d1, "C_GENDER")
            //Txt_OrgName.DataBindings.Add("text", d1, "C_ORG_NAME")
            //Txt_Desig.DataBindings.Add("text", d1, "C_DESIGNATION")
            //Txt_Education.DataBindings.Add("text", d1, "C_EDUCATION")

            //If Not IsDBNull(d1.Rows(0)("C_OCCUPATION_ID")) Then
            //    Look_OccupationList.EditValue = d1.Rows(0)("C_OCCUPATION_ID")
            //    Look_OccupationList.Tag = Look_OccupationList.EditValue
            //    Look_OccupationList.Properties.Tag = "SHOW"
            //End If

            //Txt_Ref.DataBindings.Add("text", d1, "C_REF")
            model.Txt_Remark1 = d1.Rows[0]["C_REMARKS_1"] == null ? "" : d1.Rows[0]["C_REMARKS_1"].ToString();
            model.Txt_Remark2 = d1.Rows[0]["C_REMARKS_2"] == null ? "" : d1.Rows[0]["C_REMARKS_2"].ToString();

            //Txt_Remark1.DataBindings.Add("text", d1, "C_REMARKS_1")
            //Txt_Remark2.DataBindings.Add("text", d1, "C_REMARKS_2")


            //Dim byteBLOBData(0) As Byte
            //If Not IsDBNull(d1.Rows(0)("C_PHOTO")) Then
            //    byteBLOBData = CType(d1.Rows(0)("C_PHOTO"), Byte())
            //    Dim stmBLOBData As MemoryStream = New MemoryStream(byteBLOBData)
            //    Pic_Photo.Image = Image.FromStream(stmBLOBData)
            //End If

            //2
            //XtraTabControl1.SelectedTabPage = Page_Address

            model.Txt_R_Add1 = d1.Rows[0]["C_R_ADD1"] == null ? "" : d1.Rows[0]["C_R_ADD1"].ToString();
            model.Txt_R_Add2 = d1.Rows[0]["C_R_ADD2"] == null ? "" : d1.Rows[0]["C_R_ADD2"].ToString();
            model.Txt_R_Add3 = d1.Rows[0]["C_R_ADD3"] == null ? "" : d1.Rows[0]["C_R_ADD3"].ToString();
            model.Txt_R_Add4 = d1.Rows[0]["C_R_ADD4"] == null ? "" : d1.Rows[0]["C_R_ADD4"].ToString();

            string _Member = d1.Rows[0]["C_R_CITY_ID"] == null ? "" : d1.Rows[0]["C_R_CITY_ID"].ToString();
            model.GLookUp_RStateList = d1.Rows[0]["C_R_STATE_ID"].ToString();
            if (_Member != null && _Member != "")
            {
                DataTable d2 = BASE._Magazine_DBOps.GetMapping_SubCities(_Member, (model.GLookUp_RStateList.Length > 0 ? model.GLookUp_RStateList : null));
                model.PC_City_Name = d2.Rows[0]["City & Area"].ToString();
                model.Txt_R_Other_City_Readonly = true;
                model.CityRadio = "checked";
                model.PC_City_Name_Read_only = false;
            }
            else
            {
                //model.Txt_R_Other_City = d2.Rows[0]["City & Area"].ToString();
                model.Txt_R_Other_City_Readonly = true;
            }

            //Txt_R_Add1.DataBindings.Add("text", d1, "C_R_ADD1")
            //Txt_R_Add2.DataBindings.Add("text", d1, "C_R_ADD2")
            //Txt_R_Add3.DataBindings.Add("text", d1, "C_R_ADD3")
            //Txt_R_Add4.DataBindings.Add("text", d1, "C_R_ADD4")
            //Txt_R_Other_City.DataBindings.Add("text", d1, "C_R_CITY")

            DataTable dtCountry = BASE._Address_DBOps.GetCountries("R_CO_NAME", "R_CO_CODE", "R_CO_REC_ID");
            DataView dviewCountry = new DataView(dtCountry);
            dviewCountry.Sort = "R_CO_NAME";

            var countryData = DatatableToModel.DataTabletoCountry_INFO(dviewCountry.ToTable());

            if (d1.Rows[0]["C_R_COUNTRY_ID"] != null && d1.Rows[0]["C_R_COUNTRY_ID"].ToString() != "" && d1.Rows[0]["C_R_COUNTRY_ID"].ToString() != "null")
            {
                if (d1.Rows[0]["C_R_COUNTRY_ID"].ToString().Length > 0)
                {
                    model.GLookUp_RCountryList = d1.Rows[0]["C_R_COUNTRY_ID"].ToString();
                    model.tempCountryCode = countryData.Count > 0 ? countryData.Find(x => x.R_CO_REC_ID == model.GLookUp_RCountryList).R_CO_CODE : "";
                    CountryCodeRes = countryData.Count > 0 ? countryData.Find(x => x.R_CO_REC_ID == model.GLookUp_RCountryList).R_CO_CODE : "";
                    model.GLookUp_RCountryLis_Properties_Tag = "SHOW";
                }
            }
            else
            {
                model.GLookUp_RCountryList = "f9970249-121c-4b8f-86f9-2b53e850809e";
                model.tempCountryCode = countryData.Count > 0 ? countryData.Find(x => x.R_CO_REC_ID == model.GLookUp_RCountryList).R_CO_CODE : "";
                CountryCodeRes = countryData.Count > 0 ? countryData.Find(x => x.R_CO_REC_ID == model.GLookUp_RCountryList).R_CO_CODE : "";
                model.GLookUp_RCountryLis_Properties_Tag = "SHOW";
            }
            if (d1.Rows[0]["C_R_STATE_ID"].ToString() != null && d1.Rows[0]["C_R_STATE_ID"].ToString() != "null")
            {
                if (d1.Rows[0]["C_R_STATE_ID"].ToString().Length > 0)
                {
                    model.GLookUp_RStateList = d1.Rows[0]["C_R_STATE_ID"].ToString();
                  var dtState = BASE._Address_DBOps.GetStates(model.tempCountryCode, "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
                    //DataView dviewState = new DataView(dtState);
                  //  dviewState.Sort = "R_ST_NAME";
                  //  var stateData = DatatableToModel.DataTabletoState_INFO(dviewState.ToTable());
                    model.tempStateCode = dtState.Count > 0 ? dtState.Find(x => x.R_ST_REC_ID == model.GLookUp_RStateList).R_ST_CODE : "";
                    StateCodeRes = dtState.Count > 0 ? dtState.Find(x => x.R_ST_REC_ID == model.GLookUp_RStateList).R_ST_CODE : "";
                    model.GLookUp_RStateList_Properties_Tag = "SHOW";
                }
            }
            else
            {

            }
            if (d1.Rows[0]["C_R_DISTRICT_ID"].ToString() != null && d1.Rows[0]["C_R_DISTRICT_ID"].ToString() != "null")
            {
                if (d1.Rows[0]["C_R_DISTRICT_ID"].ToString().Length > 0)
                {
                    model.GLookUp_RDistrictList = d1.Rows[0]["C_R_DISTRICT_ID"].ToString();
                  var dtDistrict = BASE._Address_DBOps.GetDistricts(CountryCodeRes, StateCodeRes, "R_DI_NAME", "R_DI_REC_ID");
                   // DataView dviewDistrict = new DataView(dtDistrict);
                   // dviewDistrict.Sort = "R_DI_NAME";
                  //  var districtData = DatatableToModel.DataTabletoDistrict_INFO(dviewDistrict.ToTable());
                    DistirictRes = dtDistrict.Count > 0 ? dtDistrict.Find(x => x.R_DI_REC_ID == model.GLookUp_RDistrictList).R_DI_CODE : "";
                    model.GLookUp_RDistrictList_Properties_Tag = "SHOW";
                }
            }
            if (d1.Rows[0]["C_R_SUB_CITY_ID"] != null || d1.Rows[0]["C_R_CITY_ID"] != null)
            {
                if (d1.Rows[0]["C_R_SUB_CITY_ID"].ToString() != null && d1.Rows[0]["C_R_SUB_CITY_ID"].ToString() != "")
                {
                    model.PC_City_Name = d1.Rows[0]["C_R_SUB_CITY_ID"].ToString();
                    model.Rad_city_OthCity = "OTHER CITY";
                    model.OthCityRadio = "checked";
                    model.PC_City_Name_Read_only = true;
                }
                else
                {
                    if (d1.Rows[0]["C_R_CITY_ID"].ToString() != null && d1.Rows[0]["C_R_CITY_ID"].ToString() != "")
                    {
                        model.PC_City_Tag = d1.Rows[0]["C_R_CITY_ID"].ToString();
                        model.Rad_city_OthCity = "CITY";
                        model.CityRadio = "checked";
                        model.PC_City_Name_Read_only = false;
                    }
                }
            }

            //TO DO::Need to Verify

            if (d1.Rows[0]["C_R_PINCODE"] != null)
            {
                if (d1.Rows[0]["C_R_PINCODE"].ToString() != null && d1.Rows[0]["C_R_PINCODE"].ToString() != "null")
                {
                    model.Txt_R_Pincode = d1.Rows[0]["C_R_PINCODE"].ToString();
                }
            }
            //If Not IsDBNull(d1.Rows(0)("C_R_COUNTRY_ID")) Then
            //If d1.Rows(0)("C_R_COUNTRY_ID").ToString.Length > 0 Then
            //Me.GLookUp_RCountryList.ShowPopup() : Me.GLookUp_RCountryList.ClosePopup()
            //Me.GLookUp_RCountryListView.FocusedRowHandle = Me.GLookUp_RCountryListView.LocateByValue("R_CO_REC_ID", d1.Rows(0)("C_R_COUNTRY_ID"))
            //Me.GLookUp_RCountryList.EditValue = d1.Rows(0)("C_R_COUNTRY_ID")
            //Me.GLookUp_RCountryList.Tag = Me.GLookUp_RCountryList.EditValue
            //Me.GLookUp_RCountryList.Properties.Tag = "SHOW"
            //End If
            //Else
            //Me.GLookUp_RCountryList.ShowPopup() : Me.GLookUp_RCountryList.ClosePopup()
            //Me.GLookUp_RCountryListView.FocusedRowHandle = Me.GLookUp_RCountryListView.LocateByValue("R_CO_REC_ID", "f9970249-121c-4b8f-86f9-2b53e850809e")
            //Me.GLookUp_RCountryList.EditValue = "f9970249-121c-4b8f-86f9-2b53e850809e"
            //Me.GLookUp_RCountryList.Tag = Me.GLookUp_RCountryList.EditValue
            //Me.GLookUp_RCountryList.Properties.Tag = "SHOW"

            //End If : Me.GLookUp_RCountryList.Properties.ReadOnly = False

            //If Not IsDBNull(d1.Rows(0)("C_R_STATE_ID")) Then
            //If d1.Rows(0)("C_R_STATE_ID").ToString.Length > 0 Then
            //Me.GLookUp_RStateList.ShowPopup() : Me.GLookUp_RStateList.ClosePopup()
            //Me.GLookUp_RStateListView.FocusedRowHandle = Me.GLookUp_RStateListView.LocateByValue("R_ST_REC_ID", d1.Rows(0)("C_R_STATE_ID"))
            //Me.GLookUp_RStateList.EditValue = d1.Rows(0)("C_R_STATE_ID")
            //Me.GLookUp_RStateList.Tag = Me.GLookUp_RStateList.EditValue
            //Me.GLookUp_RStateList.Properties.Tag = "SHOW"
            //End If
            //End If : Me.GLookUp_RStateList.Properties.ReadOnly = False
            //If Not IsDBNull(d1.Rows(0)("C_R_DISTRICT_ID")) Then
            //If d1.Rows(0)("C_R_DISTRICT_ID").ToString.Length > 0 Then
            //Me.GLookUp_RDistrictList.ShowPopup() : Me.GLookUp_RDistrictList.ClosePopup()
            //Me.GLookUp_RDistrictListView.FocusedRowHandle = Me.GLookUp_RDistrictListView.LocateByValue("R_DI_REC_ID", d1.Rows(0)("C_R_DISTRICT_ID"))
            //Me.GLookUp_RDistrictList.EditValue = d1.Rows(0)("C_R_DISTRICT_ID")
            //Me.GLookUp_RDistrictList.Tag = Me.GLookUp_RDistrictList.EditValue
            //Me.GLookUp_RDistrictList.Properties.Tag = "SHOW"
            //End If
            //End If : Me.GLookUp_RDistrictList.Properties.ReadOnly = False
            //If Not IsDBNull(d1.Rows(0)("C_R_SUB_CITY_ID")) Then
            //PC_City_Name.Text = d1.Rows(0)("C_R_SUB_CITY_ID")  'load by sub city ID
            //Else
            //If Not IsDBNull(d1.Rows(0)("C_R_CITY_ID")) Then
            //PC_City_Name.Text = d1.Rows(0)("C_R_CITY_ID")  'load by city ID
            //End If
            //End If
            //If PC_City_Name.Text.Trim.Length > 0 Then
            //Get_MappingList() : If GridView_Search1.RowCount > 0 Then GridView_Search1.FocusedRowHandle = 0
            //BUT_SELECT_Click(Nothing, Nothing)
            //End If
            //Rad_city_OthCity.EditValue = ""
            //If PC_City_Name.Text.Trim.Length > 0 Then
            //Rad_city_OthCity.EditValue = "CITY" : Rad_city_OthCity.SelectedIndex = 0
            //Else
            //Rad_city_OthCity.EditValue = "OTHER CITY" : Rad_city_OthCity.SelectedIndex = 1
            //End If
            //If Not IsDBNull(d1.Rows(0)("C_R_PINCODE")) Then
            //Txt_R_Pincode.DataBindings.Add("text", d1, "C_R_PINCODE")
            //End If


            /* Start: Previously Commentd
             Txt_O_Add1.DataBindings.Add("text", d1, "C_O_ADD1")
             Txt_O_Add2.DataBindings.Add("text", d1, "C_O_ADD2")
             Txt_O_Add3.DataBindings.Add("text", d1, "C_O_ADD3")
             Txt_O_Add4.DataBindings.Add("text", d1, "C_O_ADD4")
             If Not IsDBNull(d1.Rows(0)("C_O_COUNTRY_ID")) Then
                 If d1.Rows(0)("C_O_COUNTRY_ID").ToString.Length > 0 Then
                     Me.GLookUp_OCountryList.ShowPopup() : Me.GLookUp_OCountryList.ClosePopup()
                     Me.GLookUp_OCountryListView.FocusedRowHandle = Me.GLookUp_OCountryListView.LocateByValue("R_CO_REC_ID", d1.Rows(0)("C_O_COUNTRY_ID"))
                     Me.GLookUp_OCountryList.EditValue = d1.Rows(0)("C_O_COUNTRY_ID")
                     Me.GLookUp_OCountryList.Tag = Me.GLookUp_OCountryList.EditValue
                     Me.GLookUp_OCountryList.Properties.Tag = "SHOW"
                 End If
             End If : Me.GLookUp_OCountryList.Properties.ReadOnly = False
             If Not IsDBNull(d1.Rows(0)("C_O_STATE_ID")) Then
                 If d1.Rows(0)("C_O_STATE_ID").ToString.Length > 0 Then
                     Me.GLookUp_OStateList.ShowPopup() : Me.GLookUp_OStateList.ClosePopup()
                     Me.GLookUp_OStateListView.FocusedRowHandle = Me.GLookUp_OStateListView.LocateByValue("O_ST_REC_ID", d1.Rows(0)("C_O_STATE_ID"))
                     Me.GLookUp_OStateList.EditValue = d1.Rows(0)("C_O_STATE_ID")
                     Me.GLookUp_OStateList.Tag = Me.GLookUp_OStateList.EditValue
                     Me.GLookUp_OStateList.Properties.Tag = "SHOW"
                 End If
             End If : Me.GLookUp_OStateList.Properties.ReadOnly = False
             If Not IsDBNull(d1.Rows(0)("C_O_DISTRICT_ID")) Then
                 If d1.Rows(0)("C_O_DISTRICT_ID").ToString.Length > 0 Then
                     Me.GLookUp_ODistrictList.ShowPopup() : Me.GLookUp_ODistrictList.ClosePopup()
                     Me.GLookUp_ODistrictListView.FocusedRowHandle = Me.GLookUp_ODistrictListView.LocateByValue("O_DI_REC_ID", d1.Rows(0)("C_O_DISTRICT_ID"))
                     Me.GLookUp_ODistrictList.EditValue = d1.Rows(0)("C_O_DISTRICT_ID")
                     Me.GLookUp_ODistrictList.Tag = Me.GLookUp_ODistrictList.EditValue
                     Me.GLookUp_ODistrictList.Properties.Tag = "SHOW"
                 End If
             End If : Me.GLookUp_ODistrictList.Properties.ReadOnly = False
             If Not IsDBNull(d1.Rows(0)("C_O_CITY_ID")) Then
                 If d1.Rows(0)("C_O_CITY_ID").ToString.Length > 0 Then
                     Me.GLookUp_OCityList.ShowPopup() : Me.GLookUp_OCityList.ClosePopup()
                     Me.GLookUp_OCityListView.FocusedRowHandle = Me.GLookUp_OCityListView.LocateByValue("O_CI_REC_ID", d1.Rows(0)("C_O_CITY_ID"))
                     Me.GLookUp_OCityList.EditValue = d1.Rows(0)("C_O_CITY_ID")
                     Me.GLookUp_OCityList.Tag = Me.GLookUp_OCityList.EditValue
                     Me.GLookUp_OCityList.Properties.Tag = "SHOW"
                 End If
             End If : Me.GLookUp_OCityList.Properties.ReadOnly = False
             If Not IsDBNull(d1.Rows(0)("C_O_PINCODE")) Then
                 Txt_O_Pincode.DataBindings.Add("text", d1, "C_O_PINCODE")
             End If



             '3
             XtraTabControl1.SelectedTabPage = Page_Contact
             End : Previously Commented*/

            model.Txt_R_Tel_1 = d1.Rows[0]["C_TEL_NO_R_1"].ToString();
            // Txt_R_Tel_1.DataBindings.Add("text", d1, "C_TEL_NO_R_1")
            /*Start : Previously Commented
            'Txt_R_Tel_2.DataBindings.Add("text", d1, "C_TEL_NO_R_2")
            'Txt_R_Fax_1.DataBindings.Add("text", d1, "C_FAX_NO_R_1")
            'Txt_R_Fax_2.DataBindings.Add("text", d1, "C_FAX_NO_R_2")
            'Txt_O_Tel_1.DataBindings.Add("text", d1, "C_TEL_NO_O_1")
            'Txt_O_Tel_2.DataBindings.Add("text", d1, "C_TEL_NO_O_2")
            'Txt_O_Fax_1.DataBindings.Add("text", d1, "C_FAX_NO_O_1")
            'Txt_O_Fax_2.DataBindings.Add("text", d1, "C_FAX_NO_O_2")
                  End :  Previousily Commentd */
            model.Txt_Mob_1 = d1.Rows[0]["C_MOB_NO_1"].ToString();
            //Txt_Mob_1.DataBindings.Add("text", d1, "C_MOB_NO_1")
            model.Txt_Mob_2 = d1.Rows[0]["C_MOB_NO_2"].ToString();
            //'Txt_Mob_2.DataBindings.Add("text", d1, "C_MOB_NO_2")
            model.Txt_Email1 = d1.Rows[0]["C_EMAIL_ID_1"].ToString();
            //Txt_Email1.DataBindings.Add("text", d1, "C_EMAIL_ID_1")
            //'Txt_Email2.DataBindings.Add("text", d1, "C_EMAIL_ID_2")
            //'Txt_Website.DataBindings.Add("text", d1, "C_WEBSITE")
            //'Txt_Skype.DataBindings.Add("text", d1, "C_SKYPE_ID")
            //'Txt_Facebook.DataBindings.Add("text", d1, "C_FACEBOOK_ID")
            //'Txt_Twitter.DataBindings.Add("text", d1, "C_TWITTER_ID")
            //'Txt_GTalk.DataBindings.Add("text", d1, "C_GTALK_ID")
            //model.OthCityRadio = "checked";
            model.CityOpcityclass = "opacityClass";

            //''4
            //'XtraTabControl1.SelectedTabPage = Page_General
            //'Dim xDate As DateTime = Nothing
            //'If Not IsDBNull(d1.Rows(0)("C_DOB_L")) Then
            //'    xDate = d1.Rows(0)("C_DOB_L")
            //'    DE_DOB_L.DateTime = xDate
            //'Else
            //'    DE_DOB_L.EditValue = ""
            //'End If
            //
            //'Com_BloodGroup.DataBindings.Add("text", d1, "C_BLOODGROUP")
            //'If Not IsDBNull(d1.Rows(0)("C_CONTACT_MODE_ID")) Then
            //'    Look_ConModeList.DataBindings.Add("EditValue", d1, "C_CONTACT_MODE_ID")
            //'    Look_ConModeList.Tag = Look_ConModeList.EditValue
            //'    Look_ConModeList.Properties.Tag = "SHOW"
            //'    Look_ConModeList.ClosePopup()
            //'End If
            //
            //'Txt_PAN.DataBindings.Add("text", d1, "C_PAN_NO")
            //'Txt_VAT_TIN.DataBindings.Add("text", d1, "C_VAT_TIN_NO")
            //'Txt_CST_TIN.DataBindings.Add("text", d1, "C_CST_TIN_NO")
            //'Txt_TAN.DataBindings.Add("text", d1, "C_TAN_NO")
            //'Txt_UID.DataBindings.Add("text", d1, "C_UID_NO")
            //'Txt_STR.DataBindings.Add("text", d1, "C_STR_NO")
            //'If Not IsDBNull(d1.Rows(0)("C_PASSPORT_NO")) Then
            //'    Txt_Passport_No.DataBindings.Add("text", d1, "C_PASSPORT_NO")
            //'End If

            //''Chk_Magazine.DataBindings.Add("tag", d1._dc_DataTable, "C_MAGAZINE")
            //'Dim magTable As DataView = CType(Chk_Magazine.DataSource, DataView)
            //'For counter As Integer = 0 To Chk_Magazine.ItemCount - 1
            //'    For Each currRow In dMag.Rows
            //'        If magTable(counter)("ID").ToString().Equals(currRow("C_MISC_REC_ID").ToString()) Then
            //'            Chk_Magazine.SetItemChecked(counter, True)
            //'        End If
            //'    Next
            //'Next counter
            //
            //
            //''5
            //'XtraTabControl1.SelectedTabPage = Page_Status
            //''
            //'Rad_Status.DataBindings.Add("EditValue", d1, "C_STATUS") 'Status
            //'If Rad_Status.SelectedIndex = 0 Then
            //'    If Not IsDBNull(d1.Rows(0)("C_CON_DT")) Then
            //'        xDate = d1.Rows(0)("C_CON_DT")
            //'        DE_DOC.DateTime = xDate
            //'    Else
            //'        DE_DOC.EditValue = ""
            //'    End If
            //'Else
            //'    If Not IsDBNull(d1.Rows(0)("C_DOB_A")) Then 'Alokik DoB
            //'        xDate = d1.Rows(0)("C_DOB_A")
            //'        DE_DOB_A.DateTime = xDate
            //'    Else
            //'        DE_DOB_A.EditValue = ""
            //'    End If
            //'    Com_BK_Title.DataBindings.Add("EditValue", d1, "C_BK_TITLE") 'BK Title
            //'    Txt_PAD_No.DataBindings.Add("text", d1, "C_BK_PAD_NO") 'BK PAd NO: text
            //'    Com_Class_At.DataBindings.Add("EditValue", d1, "C_CLASS_AT") 'Class At:Dropdown
            //
            //'    If Not IsDBNull(d1.Rows(0)("C_CEN_CATEGORY")) Then
            //'        Me.Cmb_Cen_Cagetory.SelectedIndex = d1.Rows(0)("C_CEN_CATEGORY")
            //'    Else
            //'        Me.Cmb_Cen_Cagetory.SelectedIndex = 0
            //'    End If
            //'    If Me.Cmb_Cen_Cagetory.SelectedIndex = 0 Then
            //'        If Not IsDBNull(d1.Rows(0)("C_CLASS_CEN_ID")) Then
            //'            Me.GLookUp_Cen_List.ShowPopup() : Me.GLookUp_Cen_List.ClosePopup()
            //'            Me.GLookUp_Cen_ListView.MoveBy(Me.GLookUp_Cen_ListView.LocateByValue("CEN_ID", d1.Rows(0)("C_CLASS_CEN_ID")))
            //'            Me.GLookUp_Cen_ListView.FocusedRowHandle = Me.GLookUp_Cen_ListView.LocateByValue("CEN_ID", d1.Rows(0)("C_CLASS_CEN_ID"))
            //'            Me.GLookUp_Cen_List.EditValue = d1.Rows(0)("C_CLASS_CEN_ID")
            //'            Me.GLookUp_Cen_List.Tag = Me.GLookUp_Cen_List.EditValue
            //'            Me.GLookUp_Cen_List.Properties.Tag = "SHOW"
            //'        End If : Me.GLookUp_Cen_List.Properties.ReadOnly = False
            //'    End If
            //'    If Me.Cmb_Cen_Cagetory.SelectedIndex = 1 Then
            //'        If Not IsDBNull(d1.Rows(0)("C_CLASS_CEN_ID")) Then
            //'            Me.GLookUp_OCen_List.ShowPopup() : Me.GLookUp_OCen_List.ClosePopup()
            //'            Me.GLookUp_OCen_ListView.MoveBy(Me.GLookUp_OCen_ListView.LocateByValue("CEN_ID", d1.Rows(0)("C_CLASS_CEN_ID")))
            //'            Me.GLookUp_OCen_ListView.FocusedRowHandle = Me.GLookUp_OCen_ListView.LocateByValue("CEN_ID", d1.Rows(0)("C_CLASS_CEN_ID"))
            //'            Me.GLookUp_OCen_List.EditValue = d1.Rows(0)("C_CLASS_CEN_ID")
            //'            Me.GLookUp_OCen_List.Tag = Me.GLookUp_OCen_List.EditValue
            //'            Me.GLookUp_OCen_List.Properties.Tag = "SHOW"
            //'        End If : Me.GLookUp_OCen_List.Properties.ReadOnly = False
            //'    End If
            //
            //'Txt_Class_Add.DataBindings.Add("text", d1, "C_CLASS_ADD1") 'Class Addess:text
            //'End If
            //
            //'If Not IsDBNull(d1.Rows(0)("C_CATEGORY")) Then 'Category: Dropdown
            //'    Look_CategoryList.EditValue = d1.Rows(0)("C_CATEGORY")
            //'    Look_CategoryList.Text = d1.Rows(0)("C_CATEGORY")
            //'    Look_CategoryList.ClosePopup()
            //'End If
            //
            //
            //''6
            //'XtraTabControl1.SelectedTabPage = Page_Special
            //''Wings
            //'Dim wingsTable As DataView = CType(Chk_WingsList.DataSource, DataView)
            //'For counter As Integer = 0 To Chk_WingsList.ItemCount - 1
            //'    For Each currRow In dWing.Rows
            //'        If wingsTable(counter)("ID").ToString().Equals(currRow("C_WING_ID").ToString()) Then
            //'            Chk_WingsList.SetItemChecked(counter, True)
            //'        End If
            //'    Next
            //'Next counter
            //''Specialities
            //'Dim specialTable As DataView = CType(Chk_SpecialList.DataSource, DataView)
            //'For counter As Integer = 0 To Chk_SpecialList.ItemCount - 1
            //'    For Each currRow In dSpecial.Rows
            //'        If specialTable(counter)("ID").ToString().Equals(currRow("C_MISC_REC_ID").ToString()) Then
            //'            Chk_SpecialList.SetItemChecked(counter, True)
            //'        End If
            //'    Next
            //'Next counter
            //
            //''Events
            //'Dim eventsTable As DataView = CType(Chk_EventsList.DataSource, DataView)
            //'For counter As Integer = 0 To Chk_EventsList.ItemCount - 1
            //'    For Each currRow In dEvents.Rows
            //'        If eventsTable(counter)("ID").ToString().Equals(currRow("C_MISC_REC_ID").ToString()) Then
            //'            Chk_EventsList.SetItemChecked(counter, True)
            //'        End If
            //'    Next
            //'Next counter

            model.message = "Success";
            model.result = "true";

            return model;
        }
        [HttpGet]
        public ActionResult LookUp_GetCountryList_R(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Address_DBOps.GetCountries("R_CO_NAME", "R_CO_CODE", "R_CO_REC_ID") as DataTable;
            DataView d1view = new DataView(d1);
            d1view.Sort = "R_CO_NAME";
            var data = DatatableToModel.DataTabletoCountry_INFO(d1view.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult ChangeAddressBookState_R(DataSourceLoadOptions loadOptions, string selectData = "IN")
        {
            if (selectData == "" || selectData == null)
            {
                selectData = CountryCodeRes;
            }
        var d1 = BASE._Address_DBOps.GetStates(selectData, "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            //DataView d1View = new DataView(d1);
           // d1View.Sort = "R_ST_NAME";
           // var data = DatatableToModel.DataTabletoState_INFO(d1View.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(d1, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult GLookUp_RDistrictList_R(DataSourceLoadOptions loadOptions, string country = "", string state = "")
        {
            if (country == null || country == "")
            {
                country = CountryCodeRes;
            }
            if (state == "" || state == null)
            {
                state = StateCodeRes;
            }
            var data = new List<District_INFO>();
            if (!String.IsNullOrEmpty(country) && !string.IsNullOrEmpty(state))
            {
               var d1 = BASE._Address_DBOps.GetDistricts(country, state, "R_DI_NAME", "R_DI_REC_ID");
                // DataView d1View = new DataView(d1);
                // d1View.Sort = "R_DI_NAME";
                // data = DatatableToModel.DataTabletoDistrict_INFO(d1View.ToTable());
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(d1, loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult Get_Frm_Mapping_List(string Member = "", string GLookUp_RStateList = "")
        {
            string JsonData = string.Empty;
            DataTable d1 = BASE._Magazine_DBOps.GetMapping_SubCities(Member, GLookUp_RStateList.Length > 0 ? GLookUp_RStateList : null);
            List<CityList> cityList = new List<CityList>();
            var cityList1 = (from DataRow row in d1.Rows
                             select new CityList
                             {
                                 CityName = row["City & Area"].ToString(),
                                 City_REC_ID = row["CI_REC_ID"].ToString(),
                                 DistrictName = row["District"].ToString(),
                                 StateName = row["State"].ToString(),
                                 State_REC_ID = row["ST_REC_ID"].ToString(),
                                 PinCode = row["Pincode"].ToString(),
                                 CountryName = row["Country"].ToString(),
                                 Coutry_REC_ID = row["CO_REC_ID"].ToString()
                             }).ToList();
            if (d1 == null)
            {

            }
            var CityData = cityList1;
            Session["CityList_ExportData"] = CityData;
            var Final_Data = Session["CityList_ExportData"] as List<CityList>;
            DataView dview = new DataView();
            JsonData = JsonConvert.SerializeObject(d1);
            return PartialView("Frm_Mapping_List", cityList1);
        }
        public ActionResult Frm_Mapping_List()
        {
            if (Session["CityList_ExportData"] != null)
            {
                return View(Session["CityList_ExportData"]);
            }
            return null;
        }
        public ActionResult MapingCustomDataAction(string key)
        {
            var Final_Data = Session["CityList_ExportData"] as List<CityList>;
            var it = (CityList)Final_Data.Where(f => f.City_REC_ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.City_REC_ID + "![" + it.CityName;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Frm_Magazine_Dispatch_Window()
        {
            return View();
        }
        public ActionResult Frm_DispatchEntryWindow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Frm_Magazine_Window(MagazineList Maglist)
        {
            Maglist.ShortName = Maglist.ShortName.ToUpper();
            Maglist.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Maglist.TempActionMethod);
            try
            {
                if (((Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (Maglist.MembershipStart <= 0)
                    {
                        return Json(new
                        {
                            Message = "M e m b e r s h i p   S t a r t   N o   c a n n o t   b e   N e g a t i v e   /   Z e r o . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // Checking Duplicate  Magazine name Record....
                    int? MaxValue = null;

                    var MaxValuestring = BASE._Magazine_DBOps.GetMagazineCountByName(Maglist.Name);
                    if (!string.IsNullOrEmpty(MaxValuestring.ToString()))
                    {
                        MaxValue = (int)MaxValuestring;
                    }
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Max Value can not be null!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValue != 0) && (Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "M a g a z i n e   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValue != 0) && (Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        var MaxValuestr = BASE._Magazine_DBOps.GetMagazineCountByName(Maglist.Name, Maglist.ID);
                        if ((int)MaxValuestr != 0)
                        {
                            return Json(new
                            {
                                Message = "M a g a z i n e   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Checking Duplicate  Magazine name Record....

                    // Checking Duplicate  Magazine Short Name Record....

                    int? MaxValueShort = null;

                    var MaxValuestringShort = BASE._Magazine_DBOps.GetMagazineCountByShortName(Maglist.ShortName);
                    if (!string.IsNullOrEmpty(MaxValuestringShort.ToString()))
                    {
                        MaxValueShort = (int)MaxValuestring;
                    }
                    if ((MaxValueShort == null))
                    {
                        return Json(new
                        {
                            Message = "Max Value can not be null!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValueShort != 0) && (Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "M a g a z i n e   w i t h   s a m e   s h o r t   n a m e   a l r e a d y   C r e a t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValueShort != 0) && (Maglist.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        var MaxValuestrShort = BASE._Magazine_DBOps.GetMagazineCountByShortName(Maglist.ShortName, Maglist.ID);
                        if ((int)MaxValuestrShort != 0)
                        {
                            return Json(new
                            {
                                Message = "M a g a z i n e   w i t h   s a m e   s h o r t   n a m e   a l r e a d y   C r e a t e d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Checking Duplicate  Magazine Short name Record....
                }
                else
                {

                }


                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Maglist.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    // new
                    bool Result = true;


                    Common_Lib.RealTimeService.Parameter_Insert_Magazine InParam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine();
                    Maglist.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);

                    int Copies_per_year = 0;

                    if (Maglist.PublishOn == "BI-MONTHLY")
                        Copies_per_year = 6;
                    if (Maglist.PublishOn == "FORTNIGHTLY")
                        Copies_per_year = 24;
                    if (Maglist.PublishOn == "MONTHLY")
                        Copies_per_year = 12;
                    if (Maglist.PublishOn == "WEEKLY")
                        Copies_per_year = 48;
                    if (Maglist.PublishOn == "QUARTERLY")
                        Copies_per_year = 4;
                    if (Maglist.PublishOn == "YEARLY")
                        Copies_per_year = 1;
                    if (Maglist.PublishOn == "HALF-YEARLY")
                        Copies_per_year = 2;

                    InParam.Name = Maglist.Name;
                    InParam.ShortName = Maglist.ShortName;
                    InParam.Language = Maglist.Language;
                    InParam.Magazine_Regd_No = Maglist.MagazineRegd;
                    InParam.Postal_Regd_No = Maglist.PostalRegdNo;
                    InParam.PublishOn = Maglist.PublishOn;
                    InParam.FS_Applicable = Maglist.Foreign;
                    InParam.MS_Start_No = Convert.ToInt32(Maglist.MembershipStart);
                    InParam.Year_Sdt = BASE._open_Year_Sdt;
                    InParam.Status_Action = Maglist.status_Action.ToString();
                    InParam.Copies_Per_Year = Copies_per_year;

                    if (!BASE._Magazine_DBOps.Insert(InParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Saved Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Maglist.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    //Edit
                    bool Result = true;


                    //  OBacc.ID = System.Guid.NewGuid().ToString();

                    Common_Lib.RealTimeService.Parameter_Update_Magazine UpParam = new Common_Lib.RealTimeService.Parameter_Update_Magazine();

                    UpParam.Name = Maglist.Name;
                    UpParam.ShortName = Maglist.ShortName;
                    UpParam.Language = Maglist.Language;
                    UpParam.Magazine_Regd_No = Maglist.MagazineRegd;
                    UpParam.Postal_Regd_No = Maglist.PostalRegdNo;
                    UpParam.PublishOn = Maglist.PublishOn;
                    UpParam.FS_Applicable = Maglist.Foreign;
                    UpParam.MS_Start_No = Convert.ToInt32(Maglist.MembershipStart);
                    UpParam.Rec_ID = Maglist.ID;


                    if (!BASE._Magazine_DBOps.Update(UpParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Maglist.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    bool Result = true;

                    if (!BASE._Magazine_DBOps.Delete(Maglist.ID))
                    {
                        Result = false;
                    }
                }
                return Json(new
                {
                    Message = "Deleted Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Frm_Magazine_ST_Window(MagazineSubType MagSubType)
        {
            MagSubType.ShortNameSub = MagSubType.ShortNameSub.ToUpper();
            MagSubType.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + MagSubType.TempActionMethod);
            try
            {

                if (((MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (MagSubType.MinMonths <= 0)
                    {
                        return Json(new
                        {
                            Message = "M i n i m u m   M o n t h (s)   I n c o r r e c t . . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // Checking Duplicate  Magazine Subscription name Record....
                    int? MaxValue = null;

                    var MaxValuestring = BASE._Magazine_DBOps.GetMagazineSubsTypeCountByName(MagSubType.Type, MagSubType.MagListID);
                    if (!string.IsNullOrEmpty(MaxValuestring.ToString()))
                    {
                        MaxValue = (int)MaxValuestring;
                    }
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Max Value can not be null!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValue != 0) && (MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "S u b s c r i p t i o n   T y p e   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValue != 0) && (MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        var MaxValuestr = BASE._Magazine_DBOps.GetMagazineSubsTypeCountByName(MagSubType.Type, MagSubType.MagListID, MagSubType.SubTypeID);
                        if ((int)MaxValuestr != 0)
                        {
                            return Json(new
                            {
                                Message = "M a g a z i n e   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Checking Duplicate  Magazine Subscription name Record....

                    // Checking Duplicate  Magazine Subscription Short Name Record....

                    int? MaxValueShort = null;

                    var MaxValuestringShort = BASE._Magazine_DBOps.GetMagazineSubsTypeCountByShortName(MagSubType.ShortNameSub, MagSubType.MagListID);
                    if (!string.IsNullOrEmpty(MaxValuestringShort.ToString()))
                    {
                        MaxValueShort = (int)MaxValuestring;
                    }
                    if ((MaxValueShort == null))
                    {
                        return Json(new
                        {
                            Message = "Max Value can not be null!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValueShort != 0) && (MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "S u b s c r i p t i o n   T y p e   w i t h   s a m e   s h o r t   n a m e   a l r e a d y   C r e a t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValueShort != 0) && (MagSubType.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        var MaxValuestrShort = BASE._Magazine_DBOps.GetMagazineSubsTypeCountByShortName(MagSubType.ShortNameSub, MagSubType.MagListID, MagSubType.SubTypeID);
                        if ((int)MaxValuestrShort != 0)
                        {
                            return Json(new
                            {
                                Message = "S u b s c r i p t i o n   T y p e   w i t h   s a m e   s h o r t   n a m e   a l r e a d y   C r e a t e d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Checking Duplicate  Magazine Subscription Short name Record....
                }
                else
                {

                }


                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + MagSubType.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    // new
                    bool Result = true;


                    Common_Lib.RealTimeService.Parameter_Insert_Magazine_Subs_Type InParam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine_Subs_Type();
                    MagSubType.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);

                    InParam.Mag_ID = MagSubType.MagListID;
                    InParam.Name = MagSubType.Type;
                    InParam.ShortName = MagSubType.ShortNameSub;
                    InParam.MinMonth = MagSubType.MinMonths;
                    InParam.StartMonth = Convert.ToInt32(MagSubType.StartMonth);
                    InParam.PeriodWise = MagSubType.PeriodwiseFee;
                    InParam.FixedPeriod = MagSubType.FixedPeriod;
                    InParam.Status_Action = MagSubType.status_Action.ToString();

                    if (!BASE._Magazine_DBOps.Insert_Magazine_Subs_Type(InParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Saved Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + MagSubType.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    //Edit
                    bool Result = true;


                    //  OBacc.ID = System.Guid.NewGuid().ToString();

                    Common_Lib.RealTimeService.Parameter_Update_Magazine_Subs_Type UpParam = new Common_Lib.RealTimeService.Parameter_Update_Magazine_Subs_Type();

                    UpParam.Name = MagSubType.Type;
                    UpParam.ShortName = MagSubType.ShortNameSub;
                    UpParam.StartMonth = Convert.ToInt32(MagSubType.MinMonths);
                    UpParam.MinMonth = Convert.ToInt32(MagSubType.MinMonths);
                    UpParam.PeriodWise = MagSubType.PeriodwiseFee;
                    UpParam.FixedPeriod = MagSubType.FixedPeriod;
                    UpParam.Rec_ID = MagSubType.SubTypeID;


                    if (!BASE._Magazine_DBOps.Update_Subs_Type(UpParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + MagSubType.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    bool Result = true;

                    if (!BASE._Magazine_DBOps.Delete_Subs_Type(MagSubType.SubTypeID))
                    {
                        Result = false;
                    }
                }
                return Json(new
                {
                    Message = "Deleted Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Frm_Magazine_STF_Window(MagazineSubFees SubTypeFees)
        {
            SubTypeFees.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + SubTypeFees.TempActionMethod);
            try
            {
                if (((SubTypeFees.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || SubTypeFees.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (SubTypeFees.IndianFee < 0)
                    {
                        return Json(new
                        {
                            Message = "F e e   I n c o r r e c t . . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (SubTypeFees.ForeignFee < 0)
                    {
                        return Json(new
                        {
                            Message = "F e e   I n c o r r e c t . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    // Checking Duplicate  Magazine Effective Date....
                    int? MaxValue = null;

                    var MaxValuestring = BASE._Magazine_DBOps.GetMagazineSubsFeeCountByEffDate(Convert.ToDateTime(SubTypeFees.EffectiveDate).ToString(BASE._Server_Date_Format_Short), SubTypeFees.MagSubTypeID);
                    if (!string.IsNullOrEmpty(MaxValuestring.ToString()))
                    {
                        MaxValue = (int)MaxValuestring;
                    }
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Max Value can not be null!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValue != 0) && (SubTypeFees.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "S u b s c r i p t i o n   T y p e   F e e   w i t h   s a m e   e f f e c t i v e   d a t e   a l r e a d y   C r e a t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValue != 0) && (SubTypeFees.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        var MaxValuestr = BASE._Magazine_DBOps.GetMagazineSubsFeeCountByEffDate(Convert.ToDateTime(SubTypeFees.EffectiveDate).ToString(BASE._Server_Date_Format_Short), SubTypeFees.MagSubTypeID, SubTypeFees.SubFeesID);
                        if ((int)MaxValuestr != 0)
                        {
                            return Json(new
                            {
                                Message = "M a g a z i n e   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Checking Duplicate  Magazine Effective Date....

                }
                else
                {

                }


                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + SubTypeFees.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    // new
                    bool Result = true;


                    Common_Lib.RealTimeService.Parameter_Insert_Magazine_Subs_Type_Fee InParam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine_Subs_Type_Fee();
                    SubTypeFees.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);

                    InParam.ST_ID = SubTypeFees.MagSubTypeID;
                    InParam.Eff_Date = Convert.ToDateTime(SubTypeFees.EffectiveDate).ToString(BASE._Server_Date_Format_Long);
                    InParam.Indian_Fee = Convert.ToDouble(SubTypeFees.IndianFee);
                    InParam.Foreign_Fee = Convert.ToDouble(SubTypeFees.ForeignFee);
                    InParam.Status_Action = SubTypeFees.status_Action.ToString();
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Subs_Type_Fee(InParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Saved Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + SubTypeFees.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    //Edit
                    bool Result = true;


                    //  OBacc.ID = System.Guid.NewGuid().ToString();

                    Common_Lib.RealTimeService.Parameter_Update_Magazine_Subs_Type_Fee UpParam = new Common_Lib.RealTimeService.Parameter_Update_Magazine_Subs_Type_Fee();


                    UpParam.Eff_Date = Convert.ToDateTime(SubTypeFees.EffectiveDate).ToString(BASE._Server_Date_Format_Long);
                    UpParam.Indian_Fee = Convert.ToDouble(SubTypeFees.IndianFee);
                    UpParam.Foreign_Fee = Convert.ToDouble(SubTypeFees.ForeignFee);
                    UpParam.Rec_ID = SubTypeFees.SubFeesID;


                    if (!BASE._Magazine_DBOps.Update_Subs_Type_Fee(UpParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + SubTypeFees.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    bool Result = true;

                    if (!BASE._Magazine_DBOps.Delete_Subs_Type_Fee(SubTypeFees.SubFeesID))
                    {
                        Result = false;
                    }
                }
                return Json(new
                {
                    Message = "Deleted Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Frm_Magazine_Issues_Window(MagazineIssues Magissues)
        {
            Magissues.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Magissues.TempActionMethod);
            try
            {
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Magissues.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    if (Magissues.IssueNo <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   I s s u e  N o . . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.PerCopyWeight <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   p e r   C o p y   W e i g h t . . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.CopiesforAuto <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   R e g i s t r y  B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.CopiesforFgn <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   R e g i s t r y  B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.BundleMax <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   M a x.  B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.BundleMaxFgn <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   M a x.  B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.RegExp <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   E x p e n s e s  B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Magissues.RegFgnExp <= 0)
                    {
                        return Json(new
                        {
                            Message = "P l e a s e   f i l l   c o r r e c t   E x p e n s e s   B u n d l e   S i z e. . . ! ",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // new
                    bool Result = true;

                    Common_Lib.RealTimeService.Param_Insert_Magazine_Issue InParam = new Common_Lib.RealTimeService.Param_Insert_Magazine_Issue();
                    Magissues.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);

                    InParam.BUNDLE_MAX_WEIGHT = Magissues.BundleMax;
                    InParam.BUNDLE_MAX_WEIGHT_FGN = Magissues.BundleMaxFgn;
                    InParam.BUNDLE_REG_SIZE = Magissues.CopiesforAuto;
                    InParam.BUNDLE_REG_SIZE_FGN = Magissues.CopiesforFgn;
                    InParam.ISSUE_DATE = Convert.ToDateTime(Magissues.IssueDate);
                    InParam.ISSUE_NO = Magissues.IssueNo;
                    InParam.ISSUE_PART_NO = Magissues.PartNo;
                    InParam.ISSUE_VOL_NO = Magissues.VolNo;
                    InParam.MAG_ID = Magissues.MagListID;
                    InParam.PER_COPY_WEIGHT = Magissues.PerCopyWeight;
                    InParam.REG_EXP_FGN = Magissues.RegFgnExp;
                    InParam.REG_EXP_IND = Magissues.RegExp;
                    InParam.RPC_SEED = Magissues.RegSeed;
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Issue(InParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Saved Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Magissues.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    //Edit
                    bool Result = true;


                    //  OBacc.ID = System.Guid.NewGuid().ToString();

                    Common_Lib.RealTimeService.Param_Update_Magazine_Issue UpParam = new Common_Lib.RealTimeService.Param_Update_Magazine_Issue();

                    UpParam.BUNDLE_MAX_WEIGHT = Magissues.BundleMax;
                    UpParam.BUNDLE_MAX_WEIGHT_FGN = Magissues.BundleMaxFgn;
                    UpParam.BUNDLE_REG_SIZE = Magissues.CopiesforAuto;
                    UpParam.BUNDLE_REG_SIZE_FGN = Magissues.CopiesforFgn;
                    UpParam.ISSUE_DATE = Convert.ToDateTime(Magissues.IssueDate);
                    UpParam.ISSUE_NO = Magissues.IssueNo;
                    UpParam.ISSUE_PART_NO = Magissues.PartNo;
                    UpParam.ISSUE_VOL_NO = Magissues.VolNo;
                    UpParam.ISSUE_ID = Magissues.IssueID;
                    UpParam.PER_COPY_WEIGHT = Magissues.PerCopyWeight;
                    UpParam.REG_EXP_FGN = Magissues.RegFgnExp;
                    UpParam.REG_EXP_IND = Magissues.RegExp;
                    UpParam.RPC_SEED = Magissues.RegSeed;

                    if (!BASE._Magazine_DBOps.Update_Magazine_Issues(UpParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Magissues.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    bool Result = true;

                    if (!BASE._Magazine_DBOps.Delete_Magazine_Issues(Magissues.IssueID))
                    {
                        Result = false;
                    }
                }
                return Json(new
                {
                    Message = "Deleted Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // MagaZine Similar Issues
        public ActionResult InsertMagazineSimilarIssue(int YrCount = 0, string id = null)
        {
            try
            {
                Common_Lib.RealTimeService.Param_Insert_Magazine_Similar_Issues InParam = new Common_Lib.RealTimeService.Param_Insert_Magazine_Similar_Issues();
                InParam.YrCount = YrCount;
                InParam.ISSUE_ID = id;
                bool Result = true;

                Result = BASE._Magazine_DBOps.Insert_Magazine_Similar_Issue(InParam);
                if (Result == true)
                {
                    return Json(new
                    {
                        Message = "",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Some error happened during current operation!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Some error happened during current operation!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        // MagaZine Similar Issues

        // MagaZine Issues Set as default and Remove Defaults
        public ActionResult SetasDefaultIssue(string id = null)
        {
            try
            {
                bool Result = true;

                Result = BASE._Magazine_DBOps.Set_Default_Magazine_Issue(id);
                if (Result == true)
                {
                    return Json(new
                    {
                        Message = "Selected Issue set as Default..",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Some error happened during current operation!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Some error happened during current operation!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult RemoveDefaultIssue()
        {
            try
            {
                bool Result = true;

                Result = BASE._Magazine_DBOps.Remove_Default_Magazine_Issue();
                if (Result == true)
                {
                    return Json(new
                    {
                        Message = "Removed Default Issue(s) ..",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Some error happened during current operation!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Some error happened during current operation!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        // MagaZine Issues Set as default and Remove Defaults

        // MagaZine Subscription Type Set as default and Remove Defaults
        public ActionResult MagaZineSubTypeSetasDefault(string id = null)
        {
            try
            {
                bool Result = true;

                Result = BASE._Magazine_DBOps.Set_Default_Magazine_Subscription(id);
                if (Result == true)
                {
                    return Json(new
                    {
                        Message = "Selected Issue set as Default..",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Some error happened during current operation!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Some error happened during current operation!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult MagaZineSubTypeRemoveDefaultIssue(string id = null)
        {
            try
            {
                bool Result = true;

                Result = BASE._Magazine_DBOps.Remove_Default_Magazine_Subscription(id);
                if (Result == true)
                {
                    return Json(new
                    {
                        Message = "Removed Default Issue(s) ..",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Some error happened during current operation!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Some error happened during current operation!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        // MagaZine Subscription Type Set as default and Remove Defaults

        #region Magazine RowActions
        public ActionResult MagListCustomDataAction(string key)
        {
            var FinalListData = Session["MagList_ExportData"] as List<MagazineList>;
            var FDData = (MagazineList)FinalListData.Where(f => f.ID == key).FirstOrDefault();
            string Maglist = "";
            if (FDData != null)
            {
                Maglist = FDData.Name + "![" + FDData.ShortName + "![" + FDData.Language + "![" + FDData.PublishOn + "![" + FDData.MagazineRegd + "![" +
                            FDData.PostalRegdNo + "![" + FDData.MembershipStart + "![" + FDData.Foreign + "![" + FDData.ID + "![" + FDData.Add_By + "![" +
                             FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" + FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date;

            }
            return GridViewExtension.GetCustomDataCallbackResult(Maglist);
        }

        public ActionResult MagSubTypeCustomDataAction(string key)
        {
            var FinalSubData = Session["MagSubType_ExportData"] as List<MagazineSubType>;
            var FDData = FinalSubData != null ? (MagazineSubType)FinalSubData.Where(f => f.SubTypeID == key).FirstOrDefault() : null;
            string MagSubtype = "";
            if (FDData != null)
            {
                MagSubtype = FDData.Sr + "![" + FDData.Type + "![" + FDData.ShortNameSub + "![" + FDData.StartMonth + "![" + FDData.St_Month + "![" +
                            FDData.MinMonths + "![" + FDData.FixedPeriod + "![" + FDData.PeriodwiseFee + "![" + FDData.SubTypeID + "![" + FDData.Add_By + "![" +
                             FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" + FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date + "![" + FDData.MagListID;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MagSubtype);
        }

        public ActionResult MagSubFeesCustomDataAction(string key)
        {
            var FinalFeesData = Session["MagSubFees_ExportData"] as List<MagazineSubFees>;
            var FDData = FinalFeesData != null ? (MagazineSubFees)FinalFeesData.Where(f => f.SubFeesID == key).FirstOrDefault() : null;
            string MagSubFees = "";
            if (FDData != null)
            {
                MagSubFees = FDData.EffectiveDate + "![" + FDData.IndianFee + "![" + FDData.ForeignFee + "![" + FDData.SubFeesID + "![" + FDData.Add_By + "![" +
                             FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" + FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MagSubFees);
        }

        public ActionResult MagIssueCustomDataAction(string key)
        {
            var FinalissueData = Session["MagIssues_ExportData"] as List<MagazineIssues>;
            var FDData = FinalissueData != null ? (MagazineIssues)FinalissueData.Where(f => f.IssueID == key).FirstOrDefault() : null;
            string MagIssue = "";
            if (FDData != null)
            {
                MagIssue = FDData.Magazine + "![" + FDData.IssueDate + "![" + FDData.PartNo + "![" + FDData.VolNo + "![" + FDData.IssueNo + "![" +
                            FDData.RegSeed + "![" + FDData.PerCopyWeight + "![" + FDData.BundleMaxFgn + "![" + FDData.BundleMax + "![" + FDData.PerCopyWeight1 + "![" + FDData.CopiesforAuto + "![" +
                          FDData.CopiesforFgn + "![" + FDData.RegExp + "![" + FDData.RegFgnExp + "![" + FDData.IssueID + "![" + FDData.Add_By + "![" +
                             FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" + FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MagIssue);
        }
        #endregion
        #region Magazine Grids
        public ActionResult Frm_MagList_Tab_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            MagazineMaster model = new MagazineMaster();
            if (Session["MagList_ExportData"] == null || command == "REFRESH")
            {
                DataTable ML_Table = BASE._Magazine_DBOps.GetList(Voucher_Entry, Profile_Entry, "");
                if (((ML_Table == null)))
                {
                    return PartialView("Frm_MagList_Tab_Grid", null);
                }
                // BUILD DATA
                if (ML_Table != null)
                    model.showMagazineList = ML_Table.AsEnumerable().Select(T =>
                      new MagazineList
                      {
                          Name = T["Name"].ToString(),
                          ShortName = T["Short Name"].ToString(),
                          Language = T["Language"].ToString(),
                          PublishOn = T["Publish On"].ToString(),
                          MagazineRegd = T["Magazine Regd. No."].ToString(),
                          PostalRegdNo = T["Postal Regd. No."].ToString(),
                          MembershipStart = Convert.ToInt32(T["Membership Start No."].ToString()),
                          Foreign = T["Foreign Subscriptions"].ToString(),
                          ID = T["ID"].ToString(),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var fdata = model.showMagazineList;
                Session["MagList_ExportData"] = fdata;
            }
            var Final_Data = Session["MagList_ExportData"] as List<MagazineList>;
            //ViewData["newKey"] = Final_Data.OrderByDescending(a => a.ID).FirstOrDefault().ID;
            return View(Session["MagList_ExportData"]);
        }

        public ActionResult Frm_MagSubType_Tab_Grid(string Id, string command = null, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            MagazineMaster model = new MagazineMaster();
            if (Id != null)
            {
                DataTable MT_Table = BASE._Magazine_DBOps.GetList_SubscriptionTypeList(Voucher_Entry, Profile_Entry, " AND ST.MST_CEN_ID ='" + BASE._open_Cen_ID + "' ");
                if (((MT_Table == null)))
                {
                    return PartialView("Frm_MagSubType_Tab_Grid", null);
                }
                if (MT_Table != null)
                    model.showMagazineSubType = null;
                model.showMagazineSubType = MT_Table.AsEnumerable().Where(q => q["MST_MI_ID"].Equals(Id)).Select(T =>
                      new MagazineSubType
                      {
                          Sr = T["Sr."].ToString(),
                          Type = T["Type"].ToString(),
                          ShortNameSub = T["Short Name"].ToString(),
                          StartMonth = T["Start Month"].ToString(),
                          St_Month = T["St_Month"].ToString(),
                          MinMonths = Convert.ToInt32(T["Min.Months"].ToString()),
                          FixedPeriod = T["Fixed Period"].ToString(),
                          PeriodwiseFee = T["Period wise Fee Calculation"].ToString(),
                          SubTypeID = T["ID"].ToString(),
                          Default = Convert.ToBoolean(T["Default"].ToString()),
                          MagListID = T["MST_MI_ID"].ToString(),
                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var fdata = model.showMagazineSubType;
                Session["MagSubType_ExportData"] = fdata;
            }
            var Final_Data = Session["MagSubType_ExportData"] as List<MagazineSubType>;
            //ViewData["newKey"] = Final_Data.OrderByDescending(a => a.ID).FirstOrDefault().ID;
            return View(Session["MagSubType_ExportData"]);
            //return PartialView("Frm_MagSubType_Tab_Grid", model.showMagazineSubType);
        }

        public ActionResult Frm_MagSubFees_Tab_Grid(string Id, string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            MagazineMaster model = new MagazineMaster();
            if (Id != null)
            {
                DataTable MF_Table = BASE._Magazine_DBOps.GetList_SubscriptionTypeFeeList(Voucher_Entry, Profile_Entry, "");
                if (((MF_Table == null)))
                {
                    return PartialView("Frm_MagSubFees_Tab_Grid", null);
                }
                if (MF_Table != null)
                    model.showMagazineSubFees = MF_Table.AsEnumerable().Where(q => q["MSTF_MST_ID"].Equals(Id)).Select(T =>
                      new MagazineSubFees
                      {
                          EffectiveDate = string.IsNullOrEmpty(T["Effective Date"].ToString()) ? "" : T["Effective Date"].ToString(),
                          IndianFee = Convert.ToDecimal(T["Indian Fee"].ToString()),
                          ForeignFee = Convert.ToDecimal(T["Foreign Fee"].ToString()),
                          SubFeesID = T["ID"].ToString(),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var fdata = model.showMagazineSubFees;
                Session["MagSubFees_ExportData"] = fdata;
            }
            var Final_Data = Session["MagSubFees_ExportData"] as List<MagazineSubFees>;
            //ViewData["newKey"] = Final_Data.OrderByDescending(a => a.ID).FirstOrDefault().ID;
            return View(Session["MagSubFees_ExportData"]);
        }

        public ActionResult Frm_MagIssue_Tab_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            MagazineMaster model = new MagazineMaster();
            if (Session["MagIssues_ExportData"] == null || command == "REFRESH")
            {
                DataTable MI_Table = BASE._Magazine_DBOps.GetList_Issues();
                if (((MI_Table == null)))
                {
                    return PartialView("Frm_MagIssue_Tab_Grid", null);
                }
                if (MI_Table != null)
                    model.showMagazineIssues = MI_Table.AsEnumerable().Select(T =>
                      new MagazineIssues
                      {
                          Magazine = T["Magazine"].ToString(),
                          IssueDate = string.IsNullOrEmpty(T["Issue Date"].ToString()) ? "" : Convert.ToDateTime(T["Issue Date"]).ToString("MM/dd/yyyy"),
                          PartNo = Convert.ToInt32(T["Part No"].ToString()),
                          VolNo = Convert.ToInt32(T["Vol. No."].ToString()),
                          IssueNo = Convert.ToInt32(T["Issue No."].ToString()),
                          RegSeed = Convert.ToInt32(T["Reg Seed"].ToString()),
                          PerCopyWeight = Convert.ToDecimal(T["Per Copy Weight"].ToString()),
                          BundleMaxFgn = Convert.ToDecimal(T["Bundle Max Fgn Weight"].ToString()),
                          BundleMax = Convert.ToDecimal(T["Bundle Max Weight"].ToString()),
                          PerCopyWeight1 = Convert.ToDecimal(T["Per Copy Weight1"].ToString()),
                          CopiesforAuto = Convert.ToInt32(T["Copies for Auto Registry"].ToString()),
                          CopiesforFgn = Convert.ToInt32(T["Copies for Fgn Registry"].ToString()),
                          RegExp = Convert.ToDecimal(T["Reg Exp"].ToString()),
                          RegFgnExp = Convert.ToDecimal(T["Reg Fgn Exp"].ToString()),
                          IssueID = T["ID"].ToString(),
                          Default = Convert.ToBoolean(T["Default"].ToString()),

                          Add_By = T["Add By"].ToString(),
                          Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                          Edit_By = T["Edit By"].ToString(),
                          Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                          Action_Status = T["Action Status"].ToString(),
                          Action_By = T["Action By"].ToString(),
                          Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),

                      }).ToList();
                var fdata = model.showMagazineIssues;
                Session["MagIssues_ExportData"] = fdata;
            }
            var Final_Data = Session["MagIssues_ExportData"] as List<MagazineIssues>;
            //ViewData["newKey"] = Final_Data.OrderByDescending(a => a.ID).FirstOrDefault().ID;
            return View(Session["MagIssues_ExportData"]);
        }
        #endregion
        #region "Start--> LookupEdit Events"

        public ActionResult LookUp_Get_Language_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable magLanglist = BASE._Magazine_DBOps.GetLanguagesList("MISC_ID", "Name") as DataTable;
            var MagLanguage = DatatableToModel.DataTabletoLang_INFO(magLanglist);
            //ViewData["BankList"] = bankdata;
            //return PartialView(new DropdownDataReadonlyViewmodel { IsReadOnly = IsVisible });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MagLanguage, loadOptions)), "application/json");
        }

        public ActionResult LookUp_Get_PublishOn_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable magPublishOn = BASE._Magazine_DBOps.GetPublishOnList("MISC_ID", "Name") as DataTable;
            var magPublish = DatatableToModel.DataTabletoPublishOn_INFO(magPublishOn);
            //ViewData["BankList"] = bankdata;
            //return PartialView(new DropdownDataReadonlyViewmodel { IsReadOnly = IsVisible });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(magPublish, loadOptions)), "application/json");
        }

        public ActionResult LookUp_Get_Month_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {

            var Monthdata = new List<SelectListItem>() /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/;
            Monthdata.AddRange(new List<SelectListItem>() {new SelectListItem { Value = "1", Text = "JAN" },
                                                           new SelectListItem { Value = "2", Text = "FEB" },
                                                           new SelectListItem { Value = "3", Text = "MAR" },
                                                           new SelectListItem { Value = "4", Text = "APR" } ,
                                                           new SelectListItem { Value = "5", Text = "MAY" } ,
                                                           new SelectListItem { Value = "6", Text = "JUN" } ,
                                                           new SelectListItem { Value = "7", Text = "JUL" } ,
                                                           new SelectListItem { Value = "8", Text = "AUG" } ,
                                                           new SelectListItem { Value = "9", Text = "SEP" } ,
                                                           new SelectListItem { Value = "10", Text = "OCT" } ,
                                                           new SelectListItem { Value = "11", Text = "NOV" } ,
                                                           new SelectListItem { Value = "12", Text = "DEC" }});
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Monthdata, loadOptions)), "application/json");
        }
        #endregion "end--> LookupEdit Events"
        #region export
        public ActionResult Frm_MagList_Export_Options()
        {
            return PartialView();
        }
        public ActionResult Frm_MagSubType_Export_Options()
        {
            return PartialView();
        }
        public ActionResult Frm_MagSubFees_Export_Options()
        {
            return PartialView();
        }
        public ActionResult Frm_MagIssues_Export_Options()
        {
            return PartialView();
        }
        #endregion
        //[HttpPost]
        //public FileResult Export(string PdfHtml)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    StringReader sr = new StringReader(PdfHtml);
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //    pdfDoc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    pdfDoc.Close();
        //    return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //}

    }
}