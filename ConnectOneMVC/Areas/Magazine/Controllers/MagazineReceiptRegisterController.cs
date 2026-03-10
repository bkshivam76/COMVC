using Common_Lib;
using ConnectOneMVC.Areas.Magazine.Models;
using System.Collections;
using System.Collections.Generic;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Data;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Globalization;
using Newtonsoft.Json;
using System;
using System.Web.UI.WebControls;
using System.Reflection;
using ConnectOneMVC.Models;
//using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Magazine.Controllers
{
    public class MagazineReceiptRegisterController : BaseController
    {
        private static string MemberType = "";
        private static double Arrears_Before_Curr_txn = 0;
        private static DateTime SubsStartDate = DateTime.MinValue;
        private static string VoucherType = "";
        private static string AcNo = "";
        private static string Branch = "";
        private static string Purpose = "";
        private static DateTime Default_Issue_Date = DateTime.MinValue;
        private static DateTime PrevSubsEndDate = DateTime.MinValue;
        private static DateTime Txt_Fr_Date = DateTime.MinValue;
        private static DateTime Txt_To_Date = DateTime.MinValue;
        private static double MSTF_Indian_Fee = 0;
        private static double MSTF_Foreign_Fee = 0;
        private static int CurrYearIssueCount = 0;
        private static int Txt_Issues = 0;
        private static int iCategory = 0;
        private static int Txt_Free = 0;
        private static double DT_Charges = 0;
        private static double BE_TotalAmt = 0;
        private static double BE_SubAmt = 0;
        private static double BE_ExtraAmt = 0;
        //string xMID = System.Guid.NewGuid().ToString();
        private static string Last_dispatched_Issue_Date = "";
        private static string xMID = "";
        private static string Txn_Date = "";
        private static string Membership = "New";
        private static string _RecId = "";
        private static string Email_ID = "";
        private static DateTime PrevSubsStartDate = DateTime.MinValue;
        private DataTable Subs_Detail = new DataTable();
        private DataTable Payment_Detail = new DataTable();
        private DataTable Default_Subs_Detail = new DataTable();
        private DataTable Default_Payment_Detail = new DataTable();
        private DataTable Dispatch_detail = new DataTable();
        public bool GenerateReceipt = false;
        DataTable Deleted_Vouchers = new DataTable();
        public bool IsMobile
        {
            get
            {
                return (bool)GetBaseSession("IsMobile_MagazineRRInfo");
            }
            set
            {
                SetBaseSession("IsMobile_MagazineRRInfo", value);
            }
        }
        public DataTable MagazineRR_GridData
        {
            get
            {
                return (DataTable)GetBaseSession("MagazineRR_GridData_MRRInfo");
            }
            set
            {
                SetBaseSession("MagazineRR_GridData_MRRInfo", value);
            }
        }
        public DateTime? xFr_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xFr_Date_MagazineRRInfo");
            }
            set
            {
                SetBaseSession("xFr_Date_MagazineRRInfo", value);
            }
        }
        public DateTime? xTo_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xTo_Date_MagazineRRInfo");
            }
            set
            {
                SetBaseSession("xTo_Date_MagazineRRInfo", value);

            }
        }
        public List<MagazineRR_Period> MagazineRR_PeriodSelectionData
        {
            get
            {
                return (List<MagazineRR_Period>)GetBaseSession("MagazineRR_PeriodSelectionData_MagazineRRInfo");
            }
            set
            {
                SetBaseSession("MagazineRR_PeriodSelectionData_MagazineRRInfo", value);
            }
        }
        public List<CancelledList> CancelledListData_MagazineRR
        {
            get
            {
                return (List<CancelledList>)GetBaseSession("CancelledListData_MagazineRRInfo");
            }
            set
            {
                SetBaseSession("CancelledListData_MagazineRRInfo", value);
            }
        }
        public DateTime _xFr_Date()
        {
            return Convert.ToDateTime(xFr_Date);
        }
        public DateTime _xTo_Date()
        {
            return Convert.ToDateTime(xTo_Date);
        }

        // GET: Magazine/MagazineReceiptRegister
        public ActionResult Index()
        {
            return View();
        }
        #region Grid
        //public ActionResult GetGridData(string FrmDate, string Todate)
        //{
        //    ViewBag.ShowHorizontalBar = 0;
        //    CultureInfo provider = CultureInfo.InvariantCulture;
        //    var toDate = DateTime.Now;
        //    var fromDate = DateTime.Now;
        //    if (Todate.Contains("-") && FrmDate.Contains("-"))
        //    {
        //        toDate = DateTime.ParseExact(Todate, "MM-dd-yyyy", provider);
        //        fromDate = DateTime.ParseExact(FrmDate, "MM-dd-yyyy", provider);
        //    }
        //    else
        //    {
        //        toDate = DateTime.Parse(Todate);
        //        fromDate = DateTime.Parse(FrmDate);
        //    }

        //    DataSet RR_Data = BASE._Magazine_DBOps.GetList_ReceiptRegister(fromDate, toDate, BASE._open_Year_Sdt);
        //    Session["MagGrid_Table"] = RR_Data.Tables[0];
        //    if (RR_Data == null)
        //    {
        //        return View();
        //    }
        //    else
        //    {

        //        var MagazineRR = DatatableToModel.DataTabletoMagazineReg_INFO(RR_Data.Tables[0]);
        //        Session["MagazineRecReg_ExportData"] = MagazineRR;
        //        Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
        //        Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
        //        Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
        //        if (RR_Data.Tables[1] != null)
        //        {
        //            var Copies = "Copies:";
        //            string Members = "Members:";
        //            foreach (DataRow cRow in RR_Data.Tables[1].Rows)
        //            {
        //                Copies = Copies + " " + cRow["COPIES"].ToString() + "(" + cRow["MAGAZINE"].ToString() + ")";
        //                Members = Members + " " + cRow["MEMBERS"].ToString() + "(" + cRow["MAGAZINE"].ToString() + ")";
        //            }
        //            Session["BE_Stats"] = Copies + " " + Members;
        //        }
        //        return PartialView("Frm_Magazine_Recei_Reg_Info_Grid", MagazineRR);
        //    }
        //}
        #endregion

        public ActionResult MagazineRegCustomDataAction(string key)
        {
            var FinalRegData = Session["MagazineRecReg_ExportData"] as List<MagazineReciptRegInfo>;
            var FDData = FinalRegData != null ? (MagazineReciptRegInfo)FinalRegData.Where(f => f.ID == key).FirstOrDefault() : null;
            string MagReg = "";
            if (FDData != null)
            {
                MagReg = FDData.ID + "![" + FDData.Membership_ID + "![" + FDData.Member_Name + "![" + FDData.MM_MEMBER_ID + "![" + FDData.Add_By + "![" +
                           FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" + FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date + "![" + FDData.Period.Replace("![", ".")
                           + "![" + FDData.Subs_Type + "![" + FDData.Cash_Amount + "![" + FDData.MM_MEMBER_TYPE + "![" + FDData.Start_Date + "![" + FDData.Date + "![" + FDData.AddDate + "![" + FDData.Receipt_NO + "![" +
                           FDData.Receipt_ID + "![" + FDData.MS_REC_ID;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MagReg);
        }
        public ActionResult Frm_Magazine_Recei_Reg_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            Magazine_user_rights();
            DataSet RR_GridData = BASE._Magazine_DBOps.GetList_ReceiptRegister(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt);
            if (MagazineRR_GridData == null || command == "REFRESH")
            {               
                MagazineRR_GridData = RR_GridData.Tables[0];
            }
            //if (Session["MagazineRecReg_ExportData"] == null || command == "REFRESH")
            //{
            //    //Common_Lib.RealTimeService.Param_GetProfileListing LBProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            //    //LBProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING;
            //    //LBProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            //    //LBProfile.Next_YearID = BASE._next_Unaudited_YearID;
            //    //LBProfile.TableName = Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO;
            //    //DataTable LB_Table = BASE._L_B_DBOps.GetProfileListing(LBProfile);
            //    //var propertydata = DatatableToModel.DataTabletoProperty_INFO(LB_Table);

            //}
            //return PartialView("Frm_Magazine_Recei_Reg_Info", Session["MagazineRecReg_ExportData"]);
            if (RR_GridData == null)
            {
                return PartialView(MagazineRR_GridData);
            }
            else
            {
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["Layout"] = Layout == "" ? null : Layout;
                ViewData["RowKeyToFocus"] = RowKeyToFocus;
                ViewBag.ShowHorizontalBar = ShowHorizontalBar;
                ViewBag.VouchingMode = VouchingMode;
                ViewBag.ViewMode = ViewMode;
                ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
                ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
                if (RR_GridData.Tables[1] != null)
                {
                    var Copies = "Copies:";
                    string Members = "Members:";
                    foreach (DataRow cRow in RR_GridData.Tables[1].Rows)
                    {
                        Copies = Copies + " " + cRow["COPIES"].ToString() + "(" + cRow["MAGAZINE"].ToString() + ")";
                        Members = Members + " " + cRow["MEMBERS"].ToString() + "(" + cRow["MAGAZINE"].ToString() + ")";
                    }
                    ViewBag.BE_Stats = Copies + " " + Members;
                }
            }
            return PartialView(MagazineRR_GridData);

        }
        public ActionResult Frm_Magazine_Recei_Reg_Info()
        {
            Magazine_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Magazine_Receipt_Register, "List")))//Code written for User Authorization do not remove
            {                
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Magazine_Receipt_Register').hide();</script>");//Code written for User Authorization do not remove                              
            }
            ViewBag.ShowHorizontalBar = 0;
            FillChangePeriod();
            if (DateTime.Now.Date <= BASE._open_Year_Edt)
            {
                xFr_Date = DateTime.Now.Date;
                xTo_Date = DateTime.Now.Date;
                ViewBag.AllowDialogBox = false;
                ViewBag.PeriodSelectedIndex = 20;
            }
            else
            {
                xFr_Date = BASE._open_Year_Edt;
                xTo_Date = BASE._open_Year_Edt;
                ViewBag.AllowDialogBox = false;
                ViewBag.PeriodSelectedIndex = 20;
            }
            ViewBag.AllowDialogBox = true;
            //LookUp_Get_ViewType_List();
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            //if (model == null)
            //{
            //    return View();
            //}
            //else
            //{
            //    Session["BE_Stats"] = " ";
            //    return View(model);
            //}
            Session["BE_Stats"] = " ";
            return View();
        }
        #region export
        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }
        #endregion
        public ActionResult Frm_CancelledList_Export_Options()
        {
            return PartialView();
        }

        #region Change Period
        public ActionResult Frm_Magazine_Recei_Reg_Period(bool PeriodSelectionFirstTime = false)
        {
            ViewBag.PeriodSelectionFirstTime = PeriodSelectionFirstTime;
            MagazineRR_SpeceficPeriod model = new MagazineRR_SpeceficPeriod();
            model.MagazineRR_Fromdate = _xFr_Date();
            model.MagazineRR_Todate = _xTo_Date();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod(MagazineRR_SpeceficPeriod model)
        {
            if (model.MagazineRR_Todate >= model.MagazineRR_Fromdate)
            {
                xFr_Date =model.MagazineRR_Fromdate;
                xTo_Date = model.MagazineRR_Todate;
                string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd - MMM, yyyy");
                return Json(new
                {
                    result = true,
                    BE_View_Period,
                    //BE_Cash_Bank_Text,
                    FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                    ToDate = _xTo_Date().ToString("MM/dd/yyyy")
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Cannot Be Less From From Date..!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult Cmb_View_SelectedIndexChanged(int? SelectedIndex = null)
        {
            var Perioddata = (List<MagazineRR_Period>)MagazineRR_PeriodSelectionData;
            string Text = Perioddata.Where(x => x.SelectedIndex == SelectedIndex).First().Period;
            if (SelectedIndex >= 0 && SelectedIndex <= 11)
            {
                string Sel_Mon = Text.Substring(0, 3).ToUpper();
                int SEL_MM = Sel_Mon == "JAN" ? 1 : Sel_Mon == "FEB" ? 2 : Sel_Mon == "MAR" ? 3 : Sel_Mon == "APR" ? 4 : Sel_Mon == "MAY" ? 5 : Sel_Mon == "JUN" ? 6 : Sel_Mon == "JUL" ? 7 : Sel_Mon == "AUG" ? 8 : Sel_Mon == "SEP" ? 9 : Sel_Mon == "OCT" ? 10 : Sel_Mon == "NOV" ? 11 : Sel_Mon == "DEC" ? 12 : 4;
                xFr_Date = new DateTime(Convert.ToInt32(Text.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = _xFr_Date().AddMonths(1).AddDays(-1);
            }
            else if (SelectedIndex == 12)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 13)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 14)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 15)
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 16)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 17)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 18)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(9).AddDays(-1);
            }
            else if (SelectedIndex == 19)
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
            return Json(new
            {
                BE_View_Period,
                FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                ToDate = _xTo_Date().ToString("MM/dd/yyyy")
            }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult LookUp_ViewType_ChangeEvent(string Chaval)
        //{
        //    MagazineReciptReginfo model = new MagazineReciptReginfo();
        //    //DateTime xFr_Date = DateTime.Now;
        //    //DateTime xTo_Date = DateTime.Now;
        //    if (Chaval == "1st Quarter")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
        //        xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
        //    }
        //    else if (Chaval == "2rd Quarter")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
        //        xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
        //    }
        //    else if (Chaval == "3th Quarter")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
        //        xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
        //    }

        //    else if (Chaval == "4th Quarter")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
        //        xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
        //    }
        //    else if (Chaval == "1st Half Yearly")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
        //        xTo_Date = _xFr_Date().AddMonths(6).AddDays(-1);
        //    }
        //    else if (Chaval == "2nd Half Yearly")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
        //        xTo_Date = _xFr_Date().AddMonths(6).AddDays(-1);
        //    }
        //    else if (Chaval == "Nine Months")
        //    {
        //        xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
        //        xTo_Date = _xFr_Date().AddMonths(9).AddDays(-1);
        //    }
        //    else if (Chaval == "Financial Year")
        //    {
        //        xFr_Date = BASE._open_Year_Sdt;
        //        xTo_Date = BASE._open_Year_Edt;
        //    }
        //    else
        //    {
        //        var Sel_Month = Chaval.Substring(0, 3).ToUpper();
        //        var SEL_MM = 0;
        //        switch (Sel_Month)
        //        {
        //            case "JAN":
        //                SEL_MM = 1;
        //                break;
        //            case "FEB":
        //                SEL_MM = 2;
        //                break;
        //            case "MAR":
        //                SEL_MM = 3;
        //                break;
        //            case "APR":
        //                SEL_MM = 4;
        //                break;
        //            case "MAY":
        //                SEL_MM = 5;
        //                break;
        //            case "JUN":
        //                SEL_MM = 6;
        //                break;
        //            case "JUL":
        //                SEL_MM = 7;
        //                break;
        //            case "AUG":
        //                SEL_MM = 8;
        //                break;
        //            case "SEP":
        //                SEL_MM = 9;
        //                break;
        //            case "OCT":
        //                SEL_MM = 10;
        //                break;
        //            case "NOV":
        //                SEL_MM = 11;
        //                break;
        //            case "DEC":
        //                SEL_MM = 12;
        //                break;
        //            default:
        //                SEL_MM = 0;
        //                break;
        //        }
        //        xFr_Date = new DateTime(Convert.ToInt32(Chaval.Substring(4, 4)), SEL_MM, 1);
        //        xTo_Date = _xFr_Date().AddMonths(1).AddDays(-1);
        //    }
        //    model.BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
        //    model.Fromdate = _xFr_Date().ToString("MM/dd/yyyy");
        //    model.Todate = _xTo_Date().ToString("MM/dd/yyyy");
        //    return Json(new
        //    {
        //        Message = model,
        //        result = true
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #region Dropdown data List
        public void FillChangePeriod()
        {
            var period = new List<MagazineRR_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                MagazineRR_Period row1 = new MagazineRR_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                MagazineRR_Period row2 = new MagazineRR_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            MagazineRR_Period row = new MagazineRR_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            MagazineRR_Period row3 = new MagazineRR_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            MagazineRR_Period row4 = new MagazineRR_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            MagazineRR_Period row5 = new MagazineRR_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            MagazineRR_Period row6 = new MagazineRR_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            MagazineRR_Period row7 = new MagazineRR_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            MagazineRR_Period row8 = new MagazineRR_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            MagazineRR_Period row9 = new MagazineRR_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            MagazineRR_Period row10 = new MagazineRR_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            MagazineRR_PeriodSelectionData = period;
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            if (MagazineRR_PeriodSelectionData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<MagazineRR_Period>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MagazineRR_PeriodSelectionData, loadOptions)), "application/json");
        }
        //public ActionResult LookUp_Get_ViewType_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        //{

        //    var bankdata = new List<SelectListItem>() /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/;
        //    string xMonth = string.Empty;
        //    for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
        //    {

        //        switch (I)
        //        {
        //            case 1:
        //                xMonth = "JAN";
        //                break;
        //            case 2:
        //                xMonth = "FEB";
        //                break;
        //            case 3:
        //                xMonth = "MAR";
        //                break;
        //            case 4:
        //                xMonth = "APR";
        //                break;
        //            case 5:
        //                xMonth = "MAY";
        //                break;
        //            case 6:
        //                xMonth = "JUN";
        //                break;
        //            case 7:
        //                xMonth = "JUL";
        //                break;
        //            case 8:
        //                xMonth = "AUG";
        //                break;
        //            case 9:
        //                xMonth = "SEP";
        //                break;
        //            case 10:
        //                xMonth = "OCT";
        //                break;
        //            case 11:
        //                xMonth = "NOV";
        //                break;
        //            case 12:
        //                xMonth = "DEC";
        //                break;
        //            default:
        //                xMonth = "";
        //                break;
        //        }
        //        bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Sdt.Year, Text = xMonth + "-" + BASE._open_Year_Sdt.Year });

        //    }
        //    for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
        //    {

        //        switch (I)
        //        {
        //            case 1:
        //                xMonth = "JAN";
        //                break;
        //            case 2:
        //                xMonth = "FEB";
        //                break;
        //            case 3:
        //                xMonth = "MAR";
        //                break;
        //            case 4:
        //                xMonth = "APR";
        //                break;
        //            case 5:
        //                xMonth = "MAY";
        //                break;
        //            case 6:
        //                xMonth = "JUN";
        //                break;
        //            case 7:
        //                xMonth = "JUL";
        //                break;
        //            case 8:
        //                xMonth = "AUG";
        //                break;
        //            case 9:
        //                xMonth = "SEP";
        //                break;
        //            case 10:
        //                xMonth = "OCT";
        //                break;
        //            case 11:
        //                xMonth = "NOV";
        //                break;
        //            case 12:
        //                xMonth = "DEC";
        //                break;
        //            default:
        //                xMonth = "";
        //                break;
        //        }
        //        bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Edt.Year, Text = xMonth + "-" + BASE._open_Year_Edt.Year });

        //    }

        //    bankdata.AddRange(new List<SelectListItem>() { new SelectListItem { Value = "1st Quarter", Text = "1st Quarter" },
        //                                                   new SelectListItem { Value = "2rd Quarter", Text = "2rd Quarter" },
        //                                                   new SelectListItem { Value = "3th Quarter", Text = "3th Quarter" },
        //                                                   new SelectListItem { Value = "4th Quarter", Text = "4th Quarter" } ,
        //                                                   new SelectListItem { Value = "1st Half Yearly", Text = "1st Half Yearly" } ,
        //                                                   new SelectListItem { Value = "2nd Half Yearly", Text = "2nd Half Yearly" } ,
        //                                                   new SelectListItem { Value = "Nine Months", Text = "Nine Months" } ,
        //                                                   new SelectListItem { Value = "Financial Year", Text = "Financial Year" } ,
        //                                                   new SelectListItem { Value = "Specific Period", Text = "Specific Period" } });
        //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(bankdata, loadOptions)), "application/json");
        //}
        #endregion
        #region Subscribe
        public ActionResult Frm_Property_Window_Profile(string EditedOn = null, string ActionMethod = null, string ID = null)
        {
            return PartialView();
        }
        #endregion
        #region Cancelled List
        public ActionResult Frm_Magazine_Cancelled_Register(string PopupID="")
        {
            ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "popup_CancelledList_Window";

            DataTable _db_Table = BASE._Voucher_Magazine_DBOps.GetCancelledReceipts();
            if (_db_Table == null)
            {
                //Base.HandleDBError_OnNothingReturned();
                return View();
            }
            else
            {
                var cancelledlist = new List<CancelledList>();
                foreach (DataRow row in _db_Table.Rows)
                {
                    var addItem = new CancelledList();
                    addItem.NAME = row.Field<string>("NAME");
                    addItem.Number = row.Field<int?>("No.").ToString();
                    addItem.Date = row.Field<DateTime?>("Date").ToString();
                    addItem.Remarks = row.Field<string>("Remarks");
                    addItem.Cancelled_on = row.Field<DateTime?>("Cancelled on").ToString();
                    cancelledlist.Add(addItem);

                }
                CancelledListData_MagazineRR = cancelledlist;
                return PartialView(cancelledlist);
            }
            //return PartialView();
        }            
        public ActionResult Frm_CancelledList_Info_Grid()
        {
            //if (Session["Cancelled_ExportData"] == null)
            //{
                return PartialView(CancelledListData_MagazineRR);
            //}
            //else
            //{
            //    return PartialView(Session["Cancelled_ExportData"]);
            //}

        }
        #endregion
        public ActionResult MagazineReceiptRegister_Print(string ID = "")
        {

            Return_Json_Param jsonParam = new Return_Json_Param();
            IsMobile = isMobileBrowser();
            ViewBag.IsMobile = IsMobile;
            string xTemp_ID = ID;
            //string xRecNo = Receipt_No;
            object rCount = BASE._Voucher_Magazine_DBOps.GetReceiptCount(xTemp_ID);
            if (rCount == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Convert.ToInt32(rCount) <= 0)
            {
                jsonParam.message = "Membership Receipt Not Generated...!";
                jsonParam.title = "Information..!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            ConnectOne.D0010._001.Rec_Magazine_Membership_Print xRep = new ConnectOne.D0010._001.Rec_Magazine_Membership_Print(BASE, xTemp_ID);
            return View(xRep);
        }
        #region DataNaviGation
        [HttpGet]
        public JsonResult DataNavigation(string ActionMethod = "", string xTemp_ID = "", string xTemp_0 = "", string xTemp_1 = "", string xTemp_2 = "", string xTemp_3 = "", string xTemp_4 = "",
            string xTemp_5 = "", string xTemp_6 = "", DateTime? Memdate = null, DateTime? Vdate = null, DateTime? AddDate = null, string Receipt_No = "", string Receipt_ID = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string MemDate = "";
                string VDate = "";
                string XDate = "";
                DataRow RowData = MagazineRR_GridData.AsEnumerable()
             .SingleOrDefault(r => r.Field<string>("ID") == xTemp_ID);
                MagazineReciptRegInfo model = new MagazineReciptRegInfo();
                model.xID = xTemp_ID;
                xTemp_0 = RowData["Member Name"].ToString();
                xTemp_1 = RowData["Membership ID"].ToString();
                xTemp_2 = RowData["Subs. Type"].ToString();
                xTemp_3 = RowData["Period"].ToString();
                xTemp_4 = RowData["Total Amount"].ToString();
                xTemp_5 = RowData["MM_MEMBER_ID"].ToString();
                xTemp_6 = RowData["MM_MEMBER_TYPE"].ToString();
                //if (Memdate != null)
                //{
                    MemDate = Convert.ToDateTime(RowData["Start Date"].ToString()).ToString(BASE._Server_Date_Format_Short);
                //}
                //else
                //{
                //    MemDate = "";
                //}
                //if (Vdate != null)
                //{
                    VDate = Convert.ToDateTime(RowData["Date"].ToString()).ToString(BASE._Server_Date_Format_Short);
                //}
                //else
                //{
                //    VDate = "";
                //}
                //if (AddDate != null)
                //{
                    XDate = RowData["Add Date"].ToString();
                //}
                //else
                //{
                //    XDate = "";
                //}
                string xEntry = "Member Name" + new string(' ', 6) + ":  <color=Maroon><b>" + xTemp_0 + "</b></color><br><br/>Membership No." + new string(' ', 3) + ":  " + xTemp_1 + "<br><br/>Membership" + new string(' ', 10) + ":  " + xTemp_2 + "<br><br/>Period" + new string(' ', 20) + ":  " + xTemp_3 + "<br><br/>Amount" + new string(' ', 19) + ":  " + xTemp_4;
                if (ActionMethod == "RECEIPT - GENERATE")
                {
                    //'Check for Discontinued
                    object GetValue = "";
                    GetValue = BASE._Voucher_Magazine_DBOps.GetDiscontinued(true, xTemp_ID);
                    if (GetValue == null)
                    {
                        jsonParam.message = "Entry Not Found...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Receipt cannot be Generated...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'1 Check Already Generated Receipt
                    object rCount = BASE._Voucher_Magazine_DBOps.GetReceiptCount(xTemp_ID);
                    if (rCount == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        //jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)rCount > 0)
                    {
                        jsonParam.message = "Membership Receipt already generated...!<br></br>Note: Refresh Current List, if Receipt not shown.";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var SuccessParams = new
                        {
                            actionMethod = ActionMethod,
                            Member_Name = xTemp_0,
                            Membership_No = xTemp_1,
                            Membership = xTemp_2,
                            Period = xTemp_3,
                            Amount = xTemp_4,
                            xDate = XDate,
                            XTemp_ID = xTemp_ID,
                            memDate = MemDate,
                            vDate = VDate,
                            XTemp_5 = xTemp_5,
                            XTemp_6 = xTemp_6,
                            receiptNo = Receipt_No
                        };
                        jsonParam.message = "";
                        jsonParam.result = true;
                        return Json(
                            new
                            {
                                jsonParam,
                                successParams = SuccessParams,
                                flag = "ReceiptGenerate"
                            }, JsonRequestBehavior.AllowGet);
                    }
                }
                //if (ActionMethod == "RECEIPT - PRINT")
                //{

                //    object rCount = BASE._Voucher_Magazine_DBOps.GetReceiptCount(xTemp_ID);
                //    if (rCount == null)
                //    {
                //        jsonParam.message = Messages.SomeError;
                //        jsonParam.title = "Error!!";
                //        jsonParam.result = false;
                //        return Json(new
                //        {
                //            jsonParam
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //    if (rCount.ToString().Length <= 0)
                //    {
                //        jsonParam.message = "Membership Receipt Not Generated...!";
                //        jsonParam.title = "Information..!";
                //        jsonParam.result = false;
                //        return Json(new
                //        {
                //            jsonParam
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //    //String xRecNo = Receipt_No;
                //    Rec_Membership_Print xRep = new Rec_Membership_Print();
                //    xRep.MainBase = BASE;
                //    string Master_ID = xTemp_ID;
                //    DataTable R1 = BASE._Reports_Common_DBOps.GetMagazineReceipt(Common_Lib.RealTimeService.ClientScreen.Magazine_Receipt_Register, Master_ID);
                //    if (R1 == null)
                //    {

                //    }
                //    //R1.Rows[0]["MS_INWORDS"] = "Rupees (in words): "+ BASE.Common.ConvertNumToAlphaValue(Val(R1.Rows[0]["TOT_AMOUNT"].ToString()), VbStrConv.ProperCase) + " Only.";
                //    string pdf = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Magazine/Views/MagazineMaster/PrintReceipt.cshtml"));
                //    pdf = pdf.Replace("{{ReceiptNo}}", Receipt_No);
                //    pdf = pdf.Replace("{{ReceiptDate}}", R1.Rows[0]["MS_START_DATE"].ToString());
                //    pdf = pdf.Replace("{{MS_NO}}", R1.Rows[0]["MS_NO"].ToString());
                //    pdf = pdf.Replace("{{MS_MAG_NAME_TYPE}}", R1.Rows[0]["MS_MAG_NAME_TYPE"].ToString());
                //    pdf = pdf.Replace("{{MS_NAME}}", R1.Rows[0]["MS_NAME"].ToString());
                //    pdf = pdf.Replace("{{MS_ADDRESS}}", R1.Rows[0]["MS_ADDRESS"].ToString());
                //    pdf = pdf.Replace("{{MS_PERIOD}}", R1.Rows[0]["MS_PERIOD"].ToString());
                //    pdf = pdf.Replace("{{MS_AMOUNT}}", R1.Rows[0]["MS_AMOUNT"].ToString());
                //    pdf = pdf.Replace("{{COPIES}}", R1.Rows[0]["COPIES"].ToString());
                //    pdf = pdf.Replace("{{CASH_AMOUNT}}", R1.Rows[0]["CASH_AMOUNT"].ToString());
                //    pdf = pdf.Replace("{{BANK_AMOUNT}}", R1.Rows[0]["BANK_AMOUNT"].ToString());
                //    pdf = pdf.Replace("{{TOT_AMOUNT}}", R1.Rows[0]["TOT_AMOUNT"].ToString());
                //    pdf = pdf.Replace("{{DISCOUNT_AMOUNT}}", R1.Rows[0]["DISCOUNT_AMOUNT"].ToString());
                //    //pdf = pdf.Replace("{{Test2}}", "123456");
                //    //pdf = pdf.Replace("{{Test}}", "7867676");
                //    jsonParam.message = "";
                //    jsonParam.result = true;
                //    return Json(new
                //    {     
                //        jsonParam,
                //        PdfContent = pdf,
                //        flag = "ReceiptPrint",                       
                //    }, JsonRequestBehavior.AllowGet);
                //}
                if (ActionMethod == "RECEIPT - CANCELLED")
                {
                    xTemp_1 = xTemp_2.ToTitleCase() + " Membership No.: " + xTemp_1 ;
                    xTemp_4 = "Period: " + xTemp_3.ToTitleCase();
                    xTemp_5 = "Receipt No.: " + RowData["Receipt No."].ToString();
                    xTemp_6 = "";
                    //xTemp_6 = "Receipt Date: " + Convert.ToDateTime(VDate).ToString("MM/dd/yyyy").Replace("-", "/");
                    DateTime? rptDate = RowData.IsNull("Date") ? (DateTime?)null : (DateTime?)RowData["Date"];
                    if (IsDate(rptDate))
                    {
                        xTemp_6 = "Receipt Date: " + Convert.ToDateTime(RowData["Date"]).ToString(BASE._Date_Format_Current);
                    }
                    string xTemp_7 = RowData["Receipt ID"].ToString();
                     //xTemp_7 = Receipt_ID; ;
                    //'1. Check Receipt Generated
                    object rCount = BASE._Voucher_Magazine_DBOps.GetReceiptCount(xTemp_ID);
                    if (rCount == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam/*, flag = "ReceiptCancel"*/ }, JsonRequestBehavior.AllowGet);
                        //return Json(new
                        //{
                        //    Message = "Error",
                        //    flag = "ReceiptCancel",
                        //    result = false,
                        //}, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)rCount <= 0)
                    {
                        jsonParam.message = "Membership Receipt cannot be Cancelled....<br></br>Reason:    Membership Receipt not generated...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam/*, flag = "ReceiptCancel"*/ }, JsonRequestBehavior.AllowGet);
                    }
                    //'2 Check for Another Transacation  
                    DateTime xDate = Convert.ToDateTime(RowData["Add Date"].ToString());
                    bool TrFound = false;
                    //Format(xDate, "yyyy-MM-dd HH:mm:ss")
                    string CurRecordAddOn = String.Format(xDate.ToString(), "yyyy-MM-dd HH:mm:ss");
                    DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
                    if (T1 == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam/*, falg = "ReceiptCancel"*/ }, JsonRequestBehavior.AllowGet);
                        //return Json(new
                        //{
                        //    Message = "Error",
                        //    falg = "ReceiptCancel",
                        //    result = false
                        //});
                    }
                    if (T1.Rows.Count > 0)
                    {
                        foreach (DataRow XRow in T1.Rows)
                        {
                            if (XRow["REC_ID"].ToString() == xTemp_ID)
                            {
                                continue;
                            }
                            else
                            {
                                xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                if (Convert.ToDateTime(String.Format(xDate.ToString(), "yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                                {
                                    if (Convert.IsDBNull(XRow["MR_NO"]))
                                    {
                                        TrFound = true;
                                        break;
                                    }
                                }
                            }
                        }
                        //foreach (var XRow in T1.Rows)
                        //{
                        //    if (XRow["REC_ID"] == xTemp_ID)
                        //    {
                        //        continue;
                        //    }
                        //    else
                        //    {
                        //        xDate = XRow["REC_ADD_ON"];
                        //        if (Strings.Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn)
                        //        {
                        //            if (!Information.IsDBNull(XRow("MR_NO")))
                        //            {
                        //                TrFound = true; break; // TODO: might not be correct. Was : Exit For
                        //            }
                        //        }
                        //    }
                        //}
                        if (TrFound)
                        {
                            jsonParam.message = "Please cancel the receipt of other latest entries before cancelling the receipt of this entry...!";
                            jsonParam.title = "Cannot Cancelled...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                                //falg = "ReceiptCancel"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //'3 Check for Discontinued
                    object GetValue = ""; 
                    GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xTemp_ID); 
                    if (GetValue == null)
                    {
                        jsonParam.message = "Entry Not Found...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam/*, falg = "ReceiptCancel"*/ }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Receipt Entry cannot be Cancelled...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam/*,falg = "ReceiptCancel"*/ }, JsonRequestBehavior.AllowGet);
                    }
                    var SuccessParams = new
                    {
                        XTemp_0 = xTemp_0,
                        XTemp_4 = xTemp_4,
                        XTemp_1 = xTemp_1,
                        XTemp_5 = xTemp_5,
                        XTemp_6 = xTemp_6,
                        XTemp_7 = xTemp_7
                    };
                    jsonParam.message = "";
                    //jsonParam.title = "Information...";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        flag = "ReceiptCancel",
                        successParams = SuccessParams                        
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
            jsonParam.message = ""; 
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Generate Receipt
        [HttpGet]
        public ActionResult Prompt_Window(string ActionMethod = "", string Member_Name = "", string Membership_No = "", string Membership = "",
            string Period = "", string Amount = "", string xDate = "", string xTemp_ID = "", string MemDate = "", string vDate = "", string xTemp_5 = "", string xTemp_6 = "", string Flag = "", string Receipt_No = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();

            model.Member_Name = Member_Name;
            model.Membership_No = Membership_No;
            model.Membership = Membership;
            model.Period = Period;
            model.Amount = Amount;
            model.xDate = xDate;
            model.xTemp_ID = xTemp_ID;
            model.MemDate = MemDate;
            model.vDate = vDate;
            model.xTemp_5 = xTemp_5;
            model.xTemp_6 = xTemp_6;
            model.flag = Flag;
            model.Receipt_No = Receipt_No;
            return View(model);
        }
        public JsonResult ReceiptGenerate(string ActionMethod = "", string xTemp_ID = "", string xTemp_0 = "", string xTemp_1 = "", string xTemp_2 = "", string xTemp_3 = "", string xTemp_4 = "",
            string xTemp_5 = "", string xTemp_6 = "", string Memdate = "", string Vdate = "", string xDate = "", string Receipt_No = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DataRow membershipRowData = MagazineRR_GridData.AsEnumerable()
             .SingleOrDefault(r => r.Field<string>("ID") == xTemp_ID);
            xDate = membershipRowData["Add Date"].ToString();
            Vdate = membershipRowData["Date"].ToString();
            Memdate = membershipRowData["Start Date"].ToString();
            bool TrFound = false;
            //'2 Check for Another Transacation without Receipt Generated
            string CurRecordAddOn = Convert.ToDateTime(xDate).ToString("yyyy-MM-dd HH:mm:ss");
            DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
            if (T1 == null)
            {
                jsonParam.message = Common_Lib.Messages.SomeError;
                jsonParam.title = "Error";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            if (T1.Rows.Count > 0)
            {
                foreach (DataRow XRow in T1.Rows)
                {
                    if (XRow["REC_ID"].ToString() == xTemp_ID)
                    {
                        continue;
                    }
                    else
                    {
                       // xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                        if (Convert.ToDateTime(String.Format(xDate.ToString(), "yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                        {
                            TrFound = true;
                            break;
                        }
                    }
                    //if (XRow["REC_ID"]. == xTemp_ID)
                    //    continue;
                    //else
                    //{
                    //    xDate = XRow("REC_ADD_ON");
                    //    if (Format(xDate, "yyyy-MM-dd HH:mm:ss") < CurRecordAddOn)
                    //    {
                    //        if (Information.IsDBNull(XRow("MR_NO")))
                    //        {
                    //            TrFound = true; break;
                    //        }
                    //    }
                    //}
                }
                if (TrFound)
                {
                    //Message = "Please Generate receipts for older entries of the same Member before generating this receipt...!", 
                    jsonParam.message = "Please Generate receipts for older entries of the same Member before generating this receipt...!";
                    jsonParam.title = "Cannot Generate...";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (!BASE._Voucher_Magazine_DBOps.InsertMagazineMembership_Receipt(xTemp_ID, Convert.ToDateTime(Memdate), Convert.ToDateTime(Vdate), xTemp_5, xTemp_6))
            {
                jsonParam.message = Common_Lib.Messages.SomeError;
                jsonParam.title = "Error";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var successParams = new
                {
                    actionMethod = ActionMethod,
                    Member_Name = xTemp_0,
                    Membership_No = xTemp_1,
                    Membership = xTemp_2,
                    Period = xTemp_3,
                    Amount = xTemp_4,
                    XDate = xDate,
                    XTemp_ID = xTemp_ID,
                    memDate = Memdate,
                    vDate = Vdate,
                    XTemp_5 = xTemp_5,
                    XTemp_6 = xTemp_6,
                    ReceiptNo = Receipt_No
                };
                jsonParam.message = "Membership Receipt Generated Successfully";
                jsonParam.title = "Information...";
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    successParams
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_Magazine_Receipt_Cancel(string XTemp_0 = "", string XTemp_4 = "", string XTemp_1 = "", string XTemp_5 = "", string XTemp_6 = "", string XTemp_7 = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            model.xTemp0 = XTemp_0;
            model.xTemp_4 = XTemp_4;
            model.xTemp_1 = XTemp_1;
            model.xTemp_5 = XTemp_5;
            model.xTemp_6 = XTemp_6;
            model.xTemp_7 = XTemp_7;
            return View(model);
        }
        [HttpPost]
        public JsonResult ApplyToCancelReceipt(string ActionMethod = "", string XTemp_7 = "", string Reason = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), "_" + ActionMethod);
            if (Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (Reason.Length == 0)
                {
                    if (string.IsNullOrWhiteSpace(Reason))
                    {
                        jsonParam.message = "Reason Not Specified...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "IDReason";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (!BASE._Voucher_Magazine_DBOps.DeleteReceipt(Reason, XTemp_7))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            jsonParam.message = "Membership Receipt Cancelled Successully";
            jsonParam.title = "Membership Receipt Cancellation";
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PrintMemberShipNowPopUp(string ActionMethod, string Member_Name = "", string Membership_No = "", string Membership = "",
           string Period = "", string Amount = "", string xDate = "", string xTemp_ID = "", string MemDate = "", string vDate = "", string xTemp_5 = "", string xTemp_6 = "")
        {
            MagazineReciptRegInfo model = new MagazineReciptRegInfo();
            model.Member_Name = Member_Name;
            model.Membership_No = Membership_No;
            model.Membership = Membership;
            model.Period = Period;
            model.Amount = Amount;
            model.xDate = xDate;
            model.xTemp_ID = xTemp_ID;
            model.MemDate = MemDate;
            model.vDate = vDate;
            model.xTemp_5 = xTemp_5;
            model.xTemp_6 = xTemp_6;
            return View(model);
        }
        #endregion
        public ActionResult Frm_Magazine_Recei_Reg_Subscribe(string ActionMethod = "", string REC_ID = "", int Row_Index = 0)

        {
            if (ActionMethod != null)
            {
                xMID = Guid.NewGuid().ToString();
                Subscriptioninfo model = new Subscriptioninfo();
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                var actionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                model.ActionMethodName = actionMethod.ToString();
                NewMethod(ActionMethod, model);

                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    model.GridRow_Index = Row_Index;
                    DataTable Griddt = Session["MagGrid_Table"] as DataTable;
                    model.Grid_Count = Griddt.Rows.Count;
                    REC_ID = Griddt.Rows[Row_Index]["MS_REC_ID"].ToString();

                    if (REC_ID != null && REC_ID.Length > 0)
                    {
                        _RecId = REC_ID;
                        DataTable ms_tbl = BASE._Magazine_DBOps.GetRecord_Membership(REC_ID);
                        if (ms_tbl.Rows.Count > 0)
                        {
                            var BE_MagazineName = ms_tbl.Rows[0]["MM_MI_ID"].ToString();
                            var _Member = ms_tbl.Rows[0]["MM_MS_ID"].ToString();
                            var Cmb_MemType = ms_tbl.Rows[0]["MM_MEMBER_TYPE"].ToString();
                            var Cmb_SearchType = "MEMBER ID";
                            bool open_specific_Magazine = true;
                            bool Use_Rec_ID = false;
                            Common_Lib.RealTimeService.Parameter_GetMembers_VoucherMagazine Param = new Common_Lib.RealTimeService.Parameter_GetMembers_VoucherMagazine();
                            Param.Mem_Type = Cmb_MemType;
                            Param.SearchString = _Member;
                            Param.Use_Rec_ID = Use_Rec_ID.ToString();
                            Param.SearchType = Cmb_SearchType;
                            Param.Magazine_ID = open_specific_Magazine ? BE_MagazineName : null;
                            Param.Prev_Year_ID = BASE._prev_Unaudited_YearID;
                            DataTable d1 = BASE._Voucher_Magazine_DBOps.GetMembers(Param);
                            model.REC_ID = REC_ID;
                            model.Cmb_SearchType = Cmb_SearchType;
                            model.PC_MemberName = d1.Rows[0]["MEM_NAME"].ToString();
                            model.Txt_MS_ID = d1.Rows[0]["MM_MS_ID"].ToString();
                            model.Txt_MS_Old_ID = d1.Rows[0]["MM_MS_OLD_ID"].ToString();
                            model.BE_STATUS = d1.Rows[0]["MM_STATUS"].ToString();
                            model.RAD_Category = d1.Rows[0]["MM_CATEGORY"].ToString();
                            model.BE_Name = d1.Rows[0]["MEM_NAME"].ToString();
                            model.BE_Address_1 = d1.Rows[0]["MEM_ADD_1"].ToString();
                            model.BE_Address_2 = d1.Rows[0]["MEM_ADD_2"].ToString();
                            model.BE_Address_3 = d1.Rows[0]["MEM_ADD_3"].ToString();
                            model.BE_Address_4 = d1.Rows[0]["MEM_ADD_4"].ToString();
                            model.BE_Address_5 = d1.Rows[0]["MEM_ADD_5"].ToString();
                            model.BE_Address_6 = d1.Rows[0]["MEM_ADD_6"].ToString();
                            model.Email_Id = d1.Rows[0]["MEM_EMAIL"].ToString();
                            model.Chk_ConCenter = string.IsNullOrEmpty(d1.Rows[0]["MM_CC_APPLICABLE"].ToString()) ? false : Convert.ToBoolean(true);
                            model.BE_MagazineName = d1.Rows[0]["MAG_ID"].ToString();
                            model.MAG_ID = d1.Rows[0]["MAG_ID"].ToString();
                            model.Cmb_CC_MemType = d1.Rows[0]["CC_MEMBER_TYPE"].ToString();
                            model.Chk_Sponsored = d1.Rows[0]["MM_CC_SPONSORED"].ToString() == "0" ? false : Convert.ToBoolean(true);
                            model.BE_CCMemberName = d1.Rows[0]["MM_CC_MS_ID"].ToString();
                            model.Mem_ID = d1.Rows[0]["MEM_ID"].ToString();
                            model.ME_OtherDetails = d1.Rows[0]["MM_OTHER_DETAIL"].ToString();
                            model.Cmb_MemType = d1.Rows[0]["MM_MEMBER_TYPE"].ToString();
                            model.Txt_MS_Date = Convert.ToDateTime(d1.Rows[0]["MM_MS_START_DATE"].ToString());
                            var _Country = d1.Rows[0]["MEM_COUNTRY"].ToString();
                            var Membership = "";
                            if (model.Txt_MS_ID == "" || model.Txt_MS_ID == null)
                            {
                                //Membership = "New";
                                model.Membership = "New";
                                model.lblCurrActivity = "New Member";
                            }
                            else
                            {
                                model.Membership = "Renewal";
                                model.lblCurrActivity = "Existing Member";
                            }
                            Session["Membership"] = Membership;
                            if (model.Txt_MS_Date.ToString() == "" || model.Txt_MS_Date.ToString() == null)
                            {
                                model.Txt_MS_Date = DateTime.Now;
                            }
                            var Params = new Common_Lib.RealTimeService.Parameter_GetVoucherDetails_OnMemberSelection();
                            Params.MS_RecId = REC_ID;
                            Params.Txn_M_Id = xMID;
                            Params.Mag_ID = model.BE_MagazineName;
                            Params.Subs_Start_Date = Membership == "Renewal" ? DateTime.Today : Convert.ToDateTime(model.Txt_MS_Date);
                            Params.Category = _Country == "India" ? "INDIAN" : "FOREIGN";
                            Params.Prev_Year_ID = BASE._prev_Unaudited_YearID;
                            DataSet VoucherDetails = BASE._Voucher_Magazine_DBOps.GetVoucherDetails_OnMemberSelection(Params);
                            if (VoucherDetails.Tables.Count > 0)
                            {
                                DataTable Subs_Detail = VoucherDetails.Tables[1];
                                Session["Subs_Detail_Data"] = Subs_Detail;
                                DataTable Payment_Detail = VoucherDetails.Tables[2];
                                Session["Payment_Data"] = Payment_Detail;
                                DataTable Dispatch_detail = VoucherDetails.Tables[5];
                                Session["Dispatch_detail_Data"] = Dispatch_detail;
                                var Lbl_Opening = "";
                                var Last_dispatched_Issue_Date = "";
                                var Arrears_Opening = "";
                                var Advance_Opening = "";
                                DataTable d2 = VoucherDetails.Tables[0];
                                if (d2.Rows.Count > 0)
                                {
                                    if (d2.Rows[0]["op. Balance Type"].ToString() == "DUE")
                                    {
                                        Arrears_Opening = d2.Rows[0]["op. Balance"].ToString();
                                        Lbl_Opening = "Opening Due:";
                                    }
                                    else if (d2.Rows[0]["op. Balance Type"].ToString() == "ADVANCE")
                                    {
                                        Advance_Opening = d2.Rows[0]["op. Balance"].ToString(); // Payment made by user before current transaction
                                        Lbl_Opening = "Opening Advance:";
                                    }
                                    if (!string.IsNullOrEmpty(d2.Rows[0]["op. Balance"].ToString()))
                                    {
                                        model.BE_OpAdvDue = Convert.ToDouble(d2.Rows[0]["op. Balance"]);
                                    }
                                    else
                                    {
                                        model.BE_OpAdvDue = 0;
                                    }
                                    if (!string.IsNullOrEmpty(d2.Rows[0]["Next Issue Date"].ToString()))
                                    {
                                        model.Txt_Nxt_Issue_Date = d2.Rows[0]["Next Issue Date"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(d2.Rows[0]["Last dispatched Issue"].ToString()))
                                    {
                                        Last_dispatched_Issue_Date = d2.Rows[0]["Last dispatched Issue"].ToString();
                                    }
                                    var copies = 0;
                                    if (Subs_Detail.Rows.Count > 0)
                                    {
                                        foreach (DataRow cRow in Subs_Detail.Rows)
                                        {
                                            if (!string.IsNullOrEmpty(cRow["Tr_M_ID"].ToString()))
                                            {
                                                double amount = 0;
                                                model.Lbl_SubsDuringYear = 0;
                                                model.Lbl_SubsCurrTxn = cRow["Tr_M_ID"].ToString() == xMID ? Convert.ToDouble(cRow["Total Amt."]) : 0;
                                                amount = Convert.ToDouble(cRow["Total Amt."]);
                                                model.Lbl_SubsDuringYear = model.Lbl_SubsDuringYear + amount;
                                            }
                                            if (!string.IsNullOrEmpty(cRow["To Date"].ToString()))
                                            {
                                                if (Convert.ToDateTime(cRow["Fr. Date"]) <= DateTime.Today & Convert.ToDateTime(cRow["To Date"]) >= DateTime.Today)
                                                    copies = copies + Convert.ToInt32(cRow["copies"]);
                                            }
                                            else if (Convert.ToDateTime(cRow["Fr. Date"]) <= DateTime.Today)
                                                copies = Convert.ToInt32(cRow["copies"]);
                                        }
                                        model.lblDispatchCount = copies.ToString();
                                    }
                                    else
                                    {

                                    }
                                    List<Subscription_Window_Grid> Subscription_Window_Grid_List = new List<Subscription_Window_Grid>();
                                    foreach (DataRow item in Subs_Detail.Rows)
                                    {
                                        Subscription_Window_Grid subscription_Window_Grid_Object = new Subscription_Window_Grid();
                                        subscription_Window_Grid_Object.SubsDate = Convert.ToDateTime(item["Subs. Date"].ToString());
                                        subscription_Window_Grid_Object.SubsType = item["Subs. Type"].ToString();
                                        subscription_Window_Grid_Object.Pur_ID = item["Pur_ID"].ToString();
                                        subscription_Window_Grid_Object.Free = Convert.ToInt32(item["Free"].ToString());
                                        subscription_Window_Grid_Object.Copies = Convert.ToInt32(item["Copies"].ToString());
                                        subscription_Window_Grid_Object.FrDate = Convert.ToDateTime(item["Fr. Date"].ToString());
                                        subscription_Window_Grid_Object.ToDate = Convert.ToDateTime(item["To Date"].ToString());
                                        subscription_Window_Grid_Object.SubAmt = Convert.ToDecimal(item["SubAmt"].ToString());
                                        subscription_Window_Grid_Object.DispType = item["Disp. Type"].ToString();
                                        subscription_Window_Grid_Object.DispAmt = Convert.ToDecimal(item["Disp.Amt"].ToString());
                                        subscription_Window_Grid_Object.TotalAmt = Convert.ToDecimal(item["Total Amt."].ToString());
                                        subscription_Window_Grid_Object.Narration = item["Narration"].ToString();
                                        subscription_Window_Grid_Object.Reference = item["Reference"].ToString();
                                        subscription_Window_Grid_Object.RECEIPT_ID = item["RECEIPT_ID"].ToString();
                                        subscription_Window_Grid_Object.RECEIPT_NO = item["RECEIPT_NO"].ToString();
                                        subscription_Window_Grid_Object.REC_ADD_BY = item["REC_ADD_BY"].ToString();
                                        subscription_Window_Grid_Object.REC_ADD_ON = item["REC_ADD_ON"].ToString();
                                        subscription_Window_Grid_Object.REC_EDIT_BY = item["REC_EDIT_BY"].ToString();
                                        subscription_Window_Grid_Object.REC_EDIT_ON = item["REC_EDIT_ON"].ToString();
                                        subscription_Window_Grid_Object.REC_ID = item["REC_ID"].ToString();
                                        subscription_Window_Grid_Object.CURR_YEAR_INCOME = Convert.ToDecimal(item["CURR_YEAR_INCOME"] == null ? "" : item["CURR_YEAR_INCOME"].ToString());
                                        subscription_Window_Grid_Object.DisponCC = item["Disp.on CC"].ToString();
                                        subscription_Window_Grid_Object.Sr = Convert.ToInt32(item["Sr."].ToString());
                                        subscription_Window_Grid_Object.Subs_ID = item["Subs_ID"].ToString();
                                        subscription_Window_Grid_Object.TR_CODE = Convert.ToInt32(item["TR_CODE"].ToString());
                                        subscription_Window_Grid_Object.Type = item["Type"].ToString();
                                        subscription_Window_Grid_Object.Tr_M_ID = item["Tr_M_ID"].ToString();
                                        subscription_Window_Grid_Object.Dis_ID = item["Dis_ID"] == null ? "" : item["Dis_ID"].ToString();
                                        Subscription_Window_Grid_List.Add(subscription_Window_Grid_Object);
                                    }
                                    Session["Grid_Data_Subscription_Window_Data_Session"] = Subscription_Window_Grid_List;
                                    if (Payment_Detail.Rows.Count > 0)
                                    {
                                        foreach (DataRow cRow in Payment_Detail.Rows)
                                        {
                                            double amount = 0;
                                            model.Lbl_Pmt_Total = 0;
                                            model.Lbl_PmtCurrTxn = cRow["Tr_M_ID"].ToString().Equals(xMID) & (cRow["Mode"].ToString() != "DISCOUNT" & cRow["Mode"].ToString() != "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                                            amount = (cRow["Mode"].ToString() != "DISCOUNT" & cRow["Mode"].ToString() != "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                                            model.Lbl_Pmt_Total = model.Lbl_Pmt_Total + amount;
                                            model.Lbl_OtherCurrTxn = cRow["Tr_M_ID"].ToString() == xMID + (cRow["Mode"].ToString() == "DISCOUNT" | cRow["Mode"].ToString() == "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                                            model.Lbl_Other_Total = (cRow["Mode"].ToString() == "DISCOUNT" | cRow["Mode"].ToString() == "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                                        }
                                    }
                                    else
                                    {
                                    }
                                    List<Payment_Window_Grid> gridRowsToView = new List<Payment_Window_Grid>();
                                    for (int i = 0; i < Payment_Detail.Rows.Count; i++)
                                    {
                                        Payment_Window_Grid payment_Window_Grid_Object = new Payment_Window_Grid();
                                        payment_Window_Grid_Object.Sr = Convert.ToInt32(Payment_Detail.Rows[i]["Sr."]);
                                        payment_Window_Grid_Object.Mode = Payment_Detail.Rows[i]["Mode"].ToString();
                                        payment_Window_Grid_Object.Amount = Convert.ToDecimal(Payment_Detail.Rows[i]["Amount"].ToString());
                                        payment_Window_Grid_Object.Pmt_Date = Convert.ToDateTime(Payment_Detail.Rows[i]["Pmt_Date"].ToString());
                                        payment_Window_Grid_Object.Deposited_Bank_ID = Payment_Detail.Rows[i]["Dep_Bank_ID"].ToString();
                                        payment_Window_Grid_Object.Deposited_Bank = Payment_Detail.Rows[i]["Deposited Bank"].ToString();
                                        payment_Window_Grid_Object.Deposited_Branch = Payment_Detail.Rows[i]["Deposited Branch"].ToString();
                                        payment_Window_Grid_Object.Deposited_Ac_No = Payment_Detail.Rows[i]["Deposited A/c. No."].ToString();
                                        payment_Window_Grid_Object.Ref_No = Payment_Detail.Rows[i]["Ref No."].ToString();
                                        payment_Window_Grid_Object.Ref_Date = Payment_Detail.Rows[i]["Ref Date"].ToString()==""?DateTime.Now: Convert.ToDateTime(Payment_Detail.Rows[i]["Ref Date"].ToString());
                                        payment_Window_Grid_Object.Ref_Clearing_Date = Payment_Detail.Rows[i]["Ref Clearing Date"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(Payment_Detail.Rows[i]["Ref Clearing Date"].ToString());
                                        payment_Window_Grid_Object.Ref_Branch = Payment_Detail.Rows[i]["Ref Branch"].ToString();
                                        payment_Window_Grid_Object.RefBank_Name = Payment_Detail.Rows[i]["Ref Bank"].ToString();
                                        payment_Window_Grid_Object.Ref_Bank_ID = Payment_Detail.Rows[i]["Ref_Bank_ID"].ToString();
                                        payment_Window_Grid_Object.Pur_ID = Payment_Detail.Rows[i]["Pur_ID"].ToString();
                                        payment_Window_Grid_Object.Narration = Payment_Detail.Rows[i]["Narration"].ToString();
                                        payment_Window_Grid_Object.Reference = Payment_Detail.Rows[i]["Reference"].ToString();
                                        payment_Window_Grid_Object.Tr_M_ID = Payment_Detail.Rows[i]["Tr_M_ID"].ToString();
                                        payment_Window_Grid_Object.UPDATED = Convert.ToBoolean(Payment_Detail.Rows[i]["UPDATED"].ToString());
                                        payment_Window_Grid_Object.REC_GENERATED = Convert.ToBoolean(Payment_Detail.Rows[i]["REC_GENERATED"].ToString());
                                        payment_Window_Grid_Object.TR_CODE = Convert.ToInt32(Payment_Detail.Rows[i]["TR_CODE"].ToString());
                                        payment_Window_Grid_Object.Type = Payment_Detail.Rows[i]["Type"].ToString();
                                        payment_Window_Grid_Object.REC_ID = Payment_Detail.Rows[i]["REC_ID"].ToString();
                                        payment_Window_Grid_Object.REC_ADD_ON = Convert.ToDateTime(Payment_Detail.Rows[i]["REC_ADD_ON"].ToString());
                                        payment_Window_Grid_Object.REC_ADD_BY = Payment_Detail.Rows[i]["REC_ADD_BY"].ToString();
                                        payment_Window_Grid_Object.REC_EDIT_ON = Convert.ToDateTime(Payment_Detail.Rows[i]["REC_EDIT_ON"].ToString());
                                        payment_Window_Grid_Object.REC_EDIT_BY = Payment_Detail.Rows[i]["REC_EDIT_BY"].ToString();
                                        payment_Window_Grid_Object.RECEIPT_NO = Payment_Detail.Rows[i]["RECEIPT_NO"].ToString();
                                        payment_Window_Grid_Object.RECEIPT_ID = Payment_Detail.Rows[i]["RECEIPT_ID"].ToString();
                                        gridRowsToView.Add(payment_Window_Grid_Object);
                                    }
                                    Session["Grid_Data_Payment_Window_Data_Session"] = gridRowsToView;
                                    //var dataToView = gridRowsToView.FirstOrDefault(x => x.Sr == model.Sr);
                                    if (Subs_Detail.Rows.Count > 0)
                                    {
                                        PrevSubsStartDate = default(DateTime); PrevSubsEndDate = default(DateTime);
                                        foreach (DataRow cRow in Subs_Detail.Rows)
                                        {
                                            if (!string.IsNullOrEmpty(cRow["To Date"].ToString()))
                                            {
                                                if (PrevSubsEndDate < Convert.ToDateTime(cRow["To Date"]))
                                                    PrevSubsEndDate = Convert.ToDateTime(cRow["To Date"]);
                                                Session["PrevSubsEndDate"] = PrevSubsEndDate;
                                            }
                                            if (!string.IsNullOrEmpty(cRow["Fr. Date"].ToString()))
                                            {
                                                if (PrevSubsStartDate > Convert.ToDateTime(cRow["Fr. Date"]) | PrevSubsStartDate == DateTime.MinValue)
                                                    PrevSubsStartDate = Convert.ToDateTime(cRow["Fr. Date"]);
                                                Session["PrevSubsStartDate"] = PrevSubsStartDate;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(Subs_Detail.Rows[Subs_Detail.Rows.Count - 1]["To Date"].ToString()))
                                            model.Lbl_ActiveSubsPeriod = PrevSubsStartDate.ToString("MMM") + "' " + PrevSubsStartDate.Year.ToString() + " to " + PrevSubsEndDate.ToString("MMM") + "' " + PrevSubsEndDate.Year.ToString();
                                        else
                                            model.Lbl_ActiveSubsPeriod = PrevSubsStartDate.ToString("MMM") + "' " + PrevSubsStartDate.Year.ToString() + " to Lifetime..." + " ";
                                    }
                                }
                            }
                            else
                            {
                                model.Lbl_SubsCurrTxn = 0;
                                model.Lbl_SubsDuringYear = 0;
                                model.Lbl_PmtCurrTxn = 0;
                                model.Lbl_Pmt_Total = 0;
                                model.Lbl_OtherCurrTxn = 0;
                                model.Lbl_Other_Total = 0;
                                model.BE_OpAdvDue = 0;
                            }

                        }
                    }
                }
                return PartialView(model);
            }
            else
            {
                return null;
            }
        }

        private static void NewMethod(string ActionMethod, Subscriptioninfo model)
        {
            if (ActionMethod == "New")
            {
                model.Txt_MS_Date = DateTime.Now;
                model.RAD_Category = "INDIAN";
                model.Membership = "New";
            }
        }

        public JsonResult PrintReciptByPdf(string ActionMethod = "", string xTemp_ID = "")
        //public FileResult PrintReciptByPdf(string ActionMethod = "", string xTemp_ID = "", string xTemp_0 = "", string xTemp_1 = "", string xTemp_2 = "", string xTemp_3 = "", string xTemp_4 = "",
        //string xTemp_5 = "", string xTemp_6 = "", DateTime? Memdate = null, DateTime? Vdate = null, DateTime? AddDate = null, string Receipt_No = "")
        {
            MemoryStream stream = new MemoryStream();
            Rec_Membership_Print xRep = new Rec_Membership_Print();
            xRep.MainBase = BASE;
            string Master_ID = xTemp_ID;
            DataTable R1 = BASE._Reports_Common_DBOps.GetMagazineReceipt(Common_Lib.RealTimeService.ClientScreen.Magazine_Receipt_Register, Master_ID);
            if (R1 == null)
            {

            }
            //R1.Rows[0]["MS_INWORDS"] = "Rupees (in words): "+ BASE.Common.ConvertNumToAlphaValue(Val(R1.Rows[0]["TOT_AMOUNT"].ToString()), VbStrConv.ProperCase) + " Only.";
            string pdf = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Magazine/Views/MagazineMaster/PrintReceipt.cshtml"));
            pdf = pdf.Replace("{{Text}}", "12346");
            return Json(new
            {
                Message = pdf,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Frm_Magazinerecreg_Window_BUT_SAVE_Click(Subscriptioninfo model)
        {
            if (model.Cmb_MemType == null)
            {
                return Json(new
                {
                    Message = "M e m b e r   T y p e   N o t   S e l e c t e d . . . !",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            var paymentList = (List<Payment_Window_Grid>)Session["Grid_Data_Payment_Window_Data_Session"];
            var Subscr = (List<Subscription_Window_Grid>)Session["Grid_Data_Subscription_Window_Data_Session"];
            Payment_Detail = DataTableHelper.ToDataTable(paymentList.ToList());
            Subs_Detail = DataTableHelper.ToDataTable(Subscr.ToList());
            //'Subscription Start date related checks 
            string Txt_ms_Date = Convert.ToDateTime(model.Txt_MS_Date).ToString("MM/dd/yyyy").Replace("-", "/");
            //string Txt_ms_Date = model.Txt_MS_Date;
            CheckSubsDate(Txt_ms_Date, model.Membership);
            if (Convert.ToInt32(model.PC_MemberName.Length) == 0)
            {
                return Json(new { Message = "M e m b e r   N a m e   N o t   S e l e c t e d . . . !", result = false }, JsonRequestBehavior.AllowGet);
            }

            if (model.Chk_ConCenter == true)
            {
                if (model.BE_CCMemberName == null || model.BE_CCMemberName == "")
                {
                    return Json(new { Message = "C o n n e c t e d   M e m b e r   n o t   S e l e c t e d . . . !", result = false }, JsonRequestBehavior.AllowGet);
                }

            }
            if (Convert.ToInt32(model.BE_MagazineName.Length) == 0)
            {
                return Json(new { Message = "M a g a z i n e   n o t   S e l e c t e d . . . !", result = false }, JsonRequestBehavior.AllowGet);
            }

            if (Convert.ToInt32(model.Cmb_MemType.Length) == 0)
            {
                return Json(new { Message = "M e m b e r   T y p e   N o t   S e l e c t e d . . . !", result = false }, JsonRequestBehavior.AllowGet);
            }
            if (Subs_Detail.Rows.Count <= 0 && Payment_Detail.Rows.Count >= 0)
            {
                return Json(new { Message = "T h e r e    S h o u l d    B e    A t l e a s t    O n e    S u b s c r i p t i o n    I n    C u r r e n t    T r a n s a c t i o n  . . . . !", result = false }, JsonRequestBehavior.AllowGet);
            }
            if (Subs_Detail.Rows.Count == 0 && Payment_Detail.Rows.Count == 0 && model.Membership == "Renewal")
            {
                return Json(new { Message = "Saving Current voucher shall delete the existing Membership. Continue ...?", result = false }, JsonRequestBehavior.AllowGet);
            }

            ////    Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
            ////'  If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
            ////If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
            ////Total_Subs = 0 : Subs_Months_Curr_Voucher = 0 : Subs_Months_Curr_Year = 0 : Total_Subs = 0 : Subs_Amt_Per_Month = 0 : Membership_Fee = 0 : Membership_Fee_exceeding_Curr_year = 0 : Subs_Months_Exceeding_Curr_Year = 0 : Subs_Amt_Per_Month = 0
            ////CashPmt = 0 : Discount_Adj = 0 : Discount_Unadj = 0 : Writtenoff_Adj = 0 : WrittenOff_Unadj = 0 : BankPmt = 0
            //'-------------------------------------------formulae-------------------------------------------------


            int xCnt = 0;
            DataSet Link_DS = new DataSet();
            DataTable Link_DT = Link_DS.Tables.Add("Link_Item");
            DataSet Payment_DS = new DataSet();
            DataTable Payment_DT = Payment_DS.Tables.Add("Data");

            Payment_DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            Payment_DT.Columns.Add("Amount", Type.GetType("System.Double"));
            Payment_DT.Columns.Add("Mode", Type.GetType("System.String"));
            Payment_DT.Columns.Add("No.", Type.GetType("System.String"));
            Payment_DT.Columns.Add("Date", Type.GetType("System.String"));
            Payment_DT.Columns.Add("CDate", Type.GetType("System.String"));
            Payment_DT.Columns.Add("Ref_ID", Type.GetType("System.String"));
            //bank id
            Payment_DT.Columns.Add("Branch", Type.GetType("System.String"));
            Payment_DT.Columns.Add("Dep_ID", Type.GetType("System.String"));
            //Deposited Bank a/c. id
            xCnt = 1;
            foreach (DataRow XRow in Payment_Detail.Rows)
            {
                DataRow ROW = Payment_DT.NewRow();
                ROW["Sr."] = xCnt;
                ROW["Amount"] = XRow["Amount"];
                ROW["Mode"] = XRow["Mode"];
                ROW["No."] = XRow["Ref_No"];
                ROW["Date"] = XRow["Ref_Date"];
                ROW["CDate"] = XRow["Ref_Clearing_Date"];
                ROW["Ref_ID"] = XRow["Ref_Bank_ID"];
                ROW["Branch"] = XRow["Ref_Branch"];
                ROW["Dep_ID"] = XRow["Ref_Bank_ID"];
                Payment_DT.Rows.Add(ROW);
                xCnt += 1;
            }
            double CashPmt = 0;
            double Discount_Unadj = 0;
            double WrittenOff_Unadj = 0;
            double BankPmt = 0;
            double Pmt_Before_Curr_txn = 0;
            double Total_Subs = 0;
            double Subs_Before_Curr_txn = 0;
            double Total_Payment = 0;
            string connected_member_ab_id = "";
            if (Subs_Detail.Rows.Count > 0)
            {
                foreach (DataRow xROW in Subs_Detail.Rows)
                {
                    if (xROW["Tr_M_ID"] != null)
                    {
                        // Current transaction
                        if (xROW["Tr_M_ID"].ToString() == model.xMID)
                        {
                            Total_Subs = Total_Subs + Convert.ToDouble(xROW["SubAmt"]);
                        }
                        //Subs_Months_Curr_Voucher = xROW["To Date"] != null ? Convert.ToDateTime(xROW["Fr. Date"].ToString()).Month- Convert.ToDateTime(xROW["To Date"].ToString()).Month : Convert.ToDateTime(xROW["Fr. Date"].ToString()).Month- Convert.ToDateTime(BASE._open_Year_Edt).Month;
                        //Subs_Months_Curr_Voucher = IIf(Not xROW("To Date") Is Nothing, DateDiff(DateInterval.Month, DateValue(xROW("Fr. Date").ToString), DateValue(xROW("To Date").ToString)), DateDiff(DateInterval.Month, DateValue(xROW("Fr. Date").ToString), DateValue(Base._open_Year_Edt)))
                        // Subs_Months_Exceeding_Curr_Year = IIf(Not xROW("To Date") Is Nothing, DateDiff(DateInterval.Month, DateValue(xROW("To Date").ToString), Base._open_Year_Edt), 0)
                        //Subs_Months_Curr_Year = Subs_Months_Curr_Voucher - Subs_Months_Exceeding_Curr_Year
                        //Subs_Amt_Per_Month = Total_Subs / Subs_Months_Curr_Voucher

                        //Membership_Fee = Subs_Months_Curr_Year * Subs_Amt_Per_Month ' credit 
                        //Membership_Fee_exceeding_Curr_year = Subs_Months_Exceeding_Curr_Year * Subs_Amt_Per_Month
                        else
                        {
                            if (xROW["Type"].ToString() != "Opening") Subs_Before_Curr_txn = Subs_Before_Curr_txn + Convert.ToDouble(xROW["SubAmt"]);
                        }
                    }
                }
            }
            if (Payment_Detail.Rows.Count > 0)
            {
                foreach (DataRow xROW in Payment_Detail.Rows)
                {
                    if (xROW["Tr_M_ID"] != null)
                    {
                        //Current transaction
                        if (xROW["Tr_M_ID"].ToString() == model.xMID)
                        {
                            if (xROW["Mode"].ToString() == "CASH" || xROW["Mode"].ToString() == "MO" || xROW["Mode"].ToString() == "EMO")
                            {
                                CashPmt = CashPmt + Convert.ToDouble(xROW["Amount"]);
                            }
                            else if (xROW["Mode"].ToString() == "DISCOUNT")
                            {
                                Discount_Unadj = Discount_Unadj + Convert.ToDouble(xROW["Amount"].ToString());
                            }
                            else if (xROW["Mode"].ToString() == "WRITTEN OFF")
                            {
                                WrittenOff_Unadj = WrittenOff_Unadj + Convert.ToDouble(xROW["Amount"]);
                            }
                            else
                            {
                                BankPmt = BankPmt + Convert.ToDouble(xROW["Amount"].ToString());
                            }
                        }
                        else
                        {
                            Pmt_Before_Curr_txn = Pmt_Before_Curr_txn + Convert.ToDouble(xROW["Amount"].ToString());
                        }
                    }
                }
                Total_Payment = CashPmt + BankPmt;
            }
            bool HasSubscription = false;
            bool HasPayment = false;

            foreach (DataRow cRow in Subs_Detail.Rows)
            {
                if (cRow["Tr_M_ID"] != null && cRow["Tr_M_ID"] != null)
                {
                    if (cRow["Tr_M_ID"].ToString() == model.xMID) HasSubscription = true;
                }
            }
            foreach (DataRow cRow in Payment_Detail.Rows)
            {
                if ((cRow["Tr_M_ID"] != null) && cRow["Tr_M_ID"] != null)
                {
                    if (cRow["Tr_M_ID"].ToString() == model.xMID) HasPayment = true;
                }
            }


            string DispatchCheckMsg = CheckDispatchRestrictions();

            if (DispatchCheckMsg.Length > 0)
            {
                return Json(new { Message = "DispatchCheckMsg", result = false }, JsonRequestBehavior.AllowGet);
            }
            Common_Lib.RealTimeService.Param_Txn_Insert_VoucherMagazineMembership InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherMagazineMembership();
            string xMS_Rec_ID = model.MS_RecId;
            DateTime? VDate = null;

            Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership[] InsertMasters = new Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership[2];
            if (HasPayment | HasSubscription)
            {
                Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership InMinfo = new Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership();
                //InMinfo.TxnCode = (Convert.ToString(Membership) == "Renewal" ? Convert.ToString(Common_Lib.Common.Voucher_Screen_Code.Magazine_Renew) : Convert.ToString(Common_Lib.Common.Voucher_Screen_Code.Magazine_New));
                InMinfo.TxnCode = 0;
                InMinfo.VNo = "";
                //if (IsDate(model.Txn_Date)) InMinfo.TDate = model.Txn_Date.ToString(BASE._Server_Date_Format_Short); else InMinfo.TDate = model.Txn_Date;
                if (IsDate(model.Txn_Date)) InMinfo.TDate = model.Txn_Date; else InMinfo.TDate = model.Txn_Date;
                VDate = Convert.ToDateTime(model.Txn_Date);
                //InMinfo.TDate = Txn_Date
                InMinfo.PartyID = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                InMinfo.Ref_ID = model.xMS_Rec_ID;
                InMinfo.SubTotal = Total_Subs;
                InMinfo.Cash = CashPmt;
                InMinfo.Bank = BankPmt;
                InMinfo.Status_Action = model.Status_Action;
                InMinfo.RecID = model.xMID;
                InMinfo.Status_Action = "1";
                InsertMasters[0] = InMinfo;
            }
            //'Addition of masters for Subs Grid
            foreach (DataRow xRow in Subs_Detail.Rows)
            {
                if (Convert.ToBoolean(xRow["UPDATED"]))
                {
                    if (xRow["Tr_M_ID"] != null)
                    {
                        bool MasterAlreadyCreated = false;
                        foreach (Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership cMaster in InsertMasters)
                        {
                            if (cMaster != null)
                            {
                                if (xRow["Tr_M_ID"] != null && xRow["Tr_M_ID"] != null)
                                {
                                    if (cMaster.RecID == xRow["Tr_M_ID"].ToString())
                                    {
                                        MasterAlreadyCreated = true;
                                    }
                                }
                            }
                            if (MasterAlreadyCreated == true)
                            {
                                continue;
                            }
                        }
                        //'--Calculate cash and bank payment 


                        decimal PrevCash = 0;
                        decimal PrevBank = 0;
                        //': Dim PrevTotal As Decimal = 0
                        foreach (DataRow xRowPay in Payment_Detail.Rows)
                        {
                            if (xRowPay["Tr_M_ID"] != null)
                            {
                                if (Convert.ToBoolean(xRowPay["UPDATED"].ToString()) && (xRowPay["Tr_M_ID"].ToString() == xRow["Tr_M_ID"].ToString()))
                                {
                                    if (xRowPay["Mode"] != null)
                                    {
                                        if (xRowPay["Mode"].ToString() == "CASH")
                                        {
                                            PrevCash = PrevCash + Convert.ToDecimal(xRowPay["Amount"].ToString());
                                        }
                                        if (xRowPay["Mode"].ToString() == "MO")
                                        {
                                            PrevCash = PrevCash + Convert.ToDecimal(xRowPay["Amount"].ToString());
                                        }
                                        if (xRowPay["Mode"].ToString() == "EMO")
                                        {
                                            PrevCash = PrevCash + Convert.ToDecimal(xRowPay["Amount"].ToString());
                                        }
                                        if (xRowPay["Mode"].ToString() == "DISCOUNT")
                                        {
                                            PrevCash = PrevCash;
                                        }
                                        if (xRowPay["Mode"].ToString() == "WRITTEN OFF")
                                        {
                                            PrevCash = PrevCash;
                                        }
                                    }
                                    else
                                    {
                                        PrevBank = PrevBank + Convert.ToDecimal(xRowPay["Amount"].ToString());
                                    }
                                }
                            }
                        }

                        Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership InMinfo = new Common_Lib.RealTimeService.Parameter_InsertMaster_VoucherMagazineMembership();

                        if (model.Membership == "Renewal")
                        {
                            InMinfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Magazine_Renew;
                        }
                        else
                        {
                            InMinfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Magazine_New;
                        }
                        InMinfo.VNo = "";
                        DateTime Pmt_Date = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                        InMinfo.TDate = Pmt_Date.ToString(BASE._Server_Date_Format_Short);
                        InMinfo.PartyID = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                        InMinfo.Ref_ID = model.xMS_Rec_ID;
                        InMinfo.SubTotal = Convert.ToDouble(PrevCash) + Convert.ToDouble(PrevBank);
                        InMinfo.Cash = Convert.ToDouble(PrevCash);
                        InMinfo.Bank = Convert.ToDouble(PrevBank);
                        InMinfo.Status_Action = model.Status_Action;
                        InMinfo.RecID = xRow["Tr_M_ID"].ToString();
                        InMinfo.Status_Action = "1";
                        if (InsertMasters.Length == 1)
                        {
                            InsertMasters[0] = InMinfo;
                        }
                        else
                        {
                            //Array.Resize(InsertMasters, (InsertMasters.Count + 1));
                            //InsertMasters((InsertMasters.Count - 1)) = InMinfo;
                        }
                    }
                }
            }
            InNewParam.Param_InsertMaster = InsertMasters;
            bool has_Annual_Membership = false;
            int Bal_cnt = 0;

            int total_bal_count = ((Subs_Detail.Rows.Count < 2) ? 2 : ((Subs_Detail.Rows.Count - 1) * 2));
            Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership[] Insert_Transaction_subs = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership[total_bal_count + 1];
            Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership[] InsertBal_subs = new Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership[total_bal_count + 1];
            Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMagazineMembership[] InsertPurpose_Subs = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMagazineMembership[total_bal_count + 1];

            if (Subs_Detail.Rows.Count > 0)
            {
                foreach (DataRow xRow in Subs_Detail.Rows)
                {
                    if (xRow["IS_LIFE"] != null)
                    {
                        if (Convert.ToInt32(xRow["IS_LIFE"]) == 0)
                        {
                            has_Annual_Membership = true;
                        }
                    }
                    if (xRow["Tr_M_ID"] != null)
                    {
                        if (xRow["Tr_M_ID"] == model.xMID)
                        {
                            if (Convert.ToDateTime(xRow["Fr. Date"]) <= Convert.ToDateTime(Last_dispatched_Issue_Date))
                            {
                                //System.DateTime Todate = (xRow["To Date"] == null ? BASE._open_Year_Edt : xRow["To Date"]);
                                return Json(new { Message = "", result = false }, JsonRequestBehavior.AllowGet);
                                //TODO::
                                //    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "Subscriptions have been made for issues already in dispatch(" & xRow("Fr. Date") & " to " & IIf(Todate > Last_dispatched_Issue_Date, Last_dispatched_Issue_Date, Todate) & ")." & vbNewLine & " <u><b>Do you want to mark these issues as dispatched by hand...?</b></u>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                                //   Dim inDispatch As New Common_Lib.RealTimeService.Parameter_Insert_dispatch_New_Voucher()
                                //    inDispatch.FromDate = xRow("Fr. Date")
                                //    inDispatch.ToDate = IIf(Todate > Last_dispatched_Issue_Date, Last_dispatched_Issue_Date, Todate)
                                //    inDispatch.MagID = GLookUp_MagList.Tag
                                //    inDispatch.MembershipID = xMS_Rec_ID
                                //    inDispatch.subDate = xRow("Subs. Date")
                                //    inDispatch.subsCopies = xRow("Copies")
                                //    'Base._Magazine_DBOps.Insert_Magazine_Dispatch_New_Voucher(inDispatch)
                                //    InNewParam.Param_Insert_dispatch_New_Voucher = inDispatch
                                //End If
                            }
                        }
                        if (xRow["Tr_M_ID"] != null)
                        {
                            if (model.xMID.Length == 0)
                            {
                                if (Convert.ToBoolean(xRow["UPDATED"].ToString()) == true)
                                {
                                    {
                                        Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership Inbal = new Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership();
                                        //'  Inbal.MS_ID = xMS_Rec_ID
                                        Inbal.SUB_ID = xRow["Subs_ID"].ToString();
                                        Inbal.MEM_START_DATE = Convert.ToDateTime(xRow["Subs. Date"]);
                                        Inbal.FROM_DATE = Convert.ToDateTime(xRow["Fr. Date"]);
                                        if (xRow["To Date"] != null)
                                        {
                                            Inbal.TO_DATE = Convert.ToDateTime(xRow["To Date"]);
                                        }
                                        else
                                        {
                                            Inbal.TO_DATE = DateTime.MinValue;
                                        }
                                        //'Inbal.TO_DATE = xRow("To Date")
                                        Inbal.MMB_SUBS_AMT = Convert.ToDouble(xRow["SubAmt"]);
                                        Inbal.MEM_COPIES = Convert.ToInt16(xRow["Copies"]);
                                        Inbal.MEM_FREE = Convert.ToInt32(xRow["Free"]);
                                        Inbal.MEM_DISPATCH_ID = xRow["Dis_ID"].ToString();
                                        Inbal.MMB_DISPATCH_AMT = Convert.ToDouble(xRow["Disp.Amt"]);
                                        Inbal.MMB_BALANCE_TYPE = "SUBS";
                                        Inbal.CC_DISPATCH = xRow["Disp.on CC"].ToString();
                                        Inbal.BAL_AMOUNT = Convert.ToDouble(xRow["Total Amt."]);
                                        //'Total Amt.
                                        Inbal.Tr_Id = xRow["Tr_M_ID"].ToString();
                                        Inbal.Tr_Sr_No = xRow["Sr."] == null ? 0 : Convert.ToInt32(xRow["Sr."]);
                                        Inbal.MEM_TYPE = model.Cmb_MemType;
                                        //' Inbal.MEM_START_DATE = Txt_MS_Date.Text
                                        if (Membership != "New")
                                        {
                                            Inbal.MS_NO = Convert.ToInt64(model.MS_No);
                                            Inbal.MS_ID = model.Txt_MS_ID;
                                            Inbal.MS_OLD_ID = model.Txt_MS_Old_ID;
                                            Inbal.MEMBER_ID = model.PC_MemberName;
                                            if (model.Chk_ConCenter == true)
                                            {
                                                Inbal.CC_MS_ID = model.BE_CCMemberName;
                                                Inbal.CC_APPLICABLE = "YES";
                                                Inbal.CC_SPONSORED = (model.Chk_Sponsored == true ? true : false);
                                            }
                                            else
                                            {
                                                Inbal.CC_MS_ID = "";
                                                Inbal.CC_SPONSORED = (model.Chk_Sponsored == true ? true : false);
                                                Inbal.CC_APPLICABLE = "NO";
                                            }
                                            Inbal.MAG_ID = model.BE_MagazineName;
                                            Inbal.MAG_CATEGORY = model.RAD_Category;
                                            Inbal.OTHER_DETAILS = model.ME_OtherDetails;
                                            Inbal.MS_REC_ID = model.xMS_Rec_ID;
                                            Inbal.NEXT_YEAR_ID = BASE._next_Unaudited_YearID;
                                            InsertBal_subs[Bal_cnt] = Inbal;
                                            //'DEBIT TXN FOR LIFE SUBS
                                            if (Convert.ToInt32(xRow["IS_LIFE"].ToString()) == 1 || Convert.ToBoolean(xRow["IS_LIFE"].ToString()) == true)
                                            {
                                                string ID = System.Guid.NewGuid().ToString();
                                                //' Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                                InParam.VNo = "";
                                                DateTime subDate = Convert.ToDateTime(xRow["Subs. Date"].ToString());
                                                InParam.TDate = subDate.ToString(BASE._Server_Date_Format_Short);
                                                InParam.ItemID = "303C5E71-112D-4E6F-B53D-96B1CBE81EBA";
                                                InParam.Type = "DEBIT";
                                                InParam.Cr_Led_ID = "";
                                                InParam.Dr_Led_ID = "00227";
                                                InParam.SUB_Cr_Led_ID = "";
                                                InParam.SUB_Dr_Led_ID = "";
                                                InParam.Amount = xRow["Total Amt."] == null ? 0 : Convert.ToDouble(xRow["Total Amt."].ToString());
                                                InParam.Mode = "LIFE";
                                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                                InParam.Narration = "Life Subscription for " + xRow["Copies"].ToString() + " copies for Party" + model.PC_MemberName + " with ID :" + model.Txt_MS_ID;
                                                //'InParam.Reference = xRow("Reference")
                                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                                InParam.SrNo = "1";
                                                InParam.Status_Action = model.Status_Action;
                                                InParam.RecID = Guid.NewGuid().ToString();
                                                Insert_Transaction_subs[Bal_cnt] = InParam;
                                                Bal_cnt += 1;
                                            }
                                            //'CREDIT TXN FOR LIFE SUBS
                                            if (Convert.ToInt32(xRow["IS_LIFE"].ToString()) == 1 || Convert.ToBoolean(xRow["IS_LIFE"].ToString()) == true)
                                            {
                                                string ID = System.Guid.NewGuid().ToString();
                                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                                InParam.VNo = "";
                                                DateTime subDate = Convert.ToDateTime(xRow["Subs. Date"].ToString());
                                                InParam.TDate = subDate.ToString(BASE._Server_Date_Format_Short);
                                                InParam.ItemID = "29B916FE-AD12-4485-97A3-DDD6053D140E";
                                                InParam.Type = "CREDIT";
                                                InParam.Cr_Led_ID = "00224";
                                                InParam.Dr_Led_ID = "";
                                                InParam.SUB_Cr_Led_ID = "";
                                                InParam.SUB_Dr_Led_ID = "";
                                                InParam.Amount = xRow["Total Amt."] == null ? 0 : Convert.ToDouble(xRow["Total Amt."]);
                                                InParam.Mode = "LIFE";
                                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                                InParam.Narration = "Life Subscription for " + xRow["Copies"].ToString() + " copies for Party :" + model.PC_MemberName + " with ID : " + model.Txt_MS_ID;
                                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                                InParam.SrNo = "2";
                                                InParam.Status_Action = model.Status_Action;
                                                InParam.RecID = Guid.NewGuid().ToString();
                                                Insert_Transaction_subs[Bal_cnt] = InParam;
                                                Bal_cnt++;
                                            }
                                            //'DEBIT TXN FOR ANNUAL SUBS
                                            if (Convert.ToInt32(xRow["IS_LIFE"].ToString()) == 0 || Convert.ToBoolean(xRow["IS_LIFE"].ToString()) == false)
                                            {
                                                string ID = System.Guid.NewGuid().ToString();
                                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                                InParam.VNo = "";
                                                DateTime subDate = Convert.ToDateTime(xRow["Subs. Date"].ToString());
                                                InParam.TDate = subDate.ToString(BASE._Server_Date_Format_Short);
                                                InParam.ItemID = "303C5E71-112D-4E6F-B53D-96B1CBE81EBA";
                                                InParam.Type = "DEBIT";
                                                InParam.Cr_Led_ID = "";
                                                InParam.Dr_Led_ID = "00227";
                                                InParam.SUB_Cr_Led_ID = "";
                                                InParam.SUB_Dr_Led_ID = "";
                                                InParam.Amount = xRow["CURR_YEAR_INCOME"] == null ? 0 : Convert.ToDouble(xRow["CURR_YEAR_INCOME"]);
                                                InParam.Mode = "MAG. INCOME";
                                                InParam.Party1 = model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName;
                                                InParam.Narration = "Current Year Income posted for annual subscription of " + xRow["Copies"].ToString() + " copies for Party(" + model.PC_MemberName + ") with ID (" + model.Txt_MS_ID + ")";
                                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                                InParam.SrNo = "3";
                                                InParam.Status_Action = model.Status_Action;
                                                InParam.RecID = Guid.NewGuid().ToString();
                                                Insert_Transaction_subs[Bal_cnt] = InParam;
                                                Bal_cnt++;
                                            }
                                            //'CREDIT TXN FOR ANNUAL SUBS
                                            if (Convert.ToInt32(xRow["IS_LIFE"].ToString()) == 0 || Convert.ToBoolean(xRow["IS_LIFE"].ToString()) == false)
                                            {
                                                string ID = System.Guid.NewGuid().ToString();
                                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                                InParam.VNo = "";
                                                DateTime subDate = Convert.ToDateTime(xRow["Subs. Date"].ToString());
                                                InParam.TDate = subDate.ToString(BASE._Server_Date_Format_Short);
                                                InParam.ItemID = "2DB5DBA1-3CF8-400F-BA8D-803BAC8C1C02";
                                                InParam.Type = "CREDIT";
                                                InParam.Cr_Led_ID = "00113";
                                                InParam.Dr_Led_ID = "";
                                                InParam.SUB_Cr_Led_ID = "";
                                                InParam.SUB_Dr_Led_ID = "";
                                                InParam.Amount = xRow["CURR_YEAR_INCOME"] == null ? 0 : Convert.ToDouble(xRow["CURR_YEAR_INCOME"]);
                                                InParam.Mode = "MAG. INCOME";
                                                InParam.Party1 = model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName;
                                                InParam.Narration = "Current Year Income posted for annual subscription of " + xRow["Copies"].ToString() + " copies for Party(" + model.PC_MemberName + ") with ID (" + model.Txt_MS_ID + ")";
                                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                                InParam.SrNo = "3";
                                                InParam.Status_Action = model.Status_Action;
                                                InParam.RecID = Guid.NewGuid().ToString();
                                                Insert_Transaction_subs[Bal_cnt] = InParam;
                                                Bal_cnt++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            InNewParam.Param_InsertSubsBalances = InsertBal_subs;
            InNewParam.Param_Insert_Subs_Txn = Insert_Transaction_subs;
            InNewParam.Param_InsertPurpose_subs = InsertPurpose_Subs;
            int Pmt_cnt = 0;
            int total_pmt_count = ((Payment_Detail.Rows.Count < 2) ? 2 : ((Payment_Detail.Rows.Count - 1) * 2));
            Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership[] InsertBal_Pmt = new Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership[total_pmt_count + 1];
            Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership[] Insert_Transaction_pmt = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership[total_pmt_count + 1];
            Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMagazineMembership[] InsertPurpose_Pmt = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMagazineMembership[total_pmt_count + 1];
            //test
            if ((Payment_Detail.Rows.Count > 0))
            {
                foreach (DataRow xRow in Payment_Detail.Rows)
                {
                    if (xRow["Tr_M_ID"] != null)
                    {
                        if (xRow["Tr_M_ID"].ToString() == model.xMID || Convert.ToBoolean(xRow["UPDATED"].ToString()) == true)
                        {
                            // iNSERT pAYMENT BALANCE
                            Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership Inbal = new Common_Lib.RealTimeService.Parameter_InsertBalances_VoucherMagazineMembership();
                            Inbal.MEM_START_DATE = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                            Inbal.MMB_BALANCE_TYPE = "PAYMENT";
                            Inbal.BAL_AMOUNT = Convert.ToDouble(xRow["Amount"].ToString());
                            // Total Amt.
                            Inbal.Tr_Id = xRow["Tr_M_ID"].ToString();
                            Inbal.Tr_Sr_No = xRow["Sr."] == null ? 0 : Convert.ToInt32(xRow["Sr."].ToString());
                            Inbal.MS_ID = model.Txt_MS_ID;
                            Inbal.MS_REC_ID = model.xMS_Rec_ID;
                            Inbal.NEXT_YEAR_ID = BASE._next_Unaudited_YearID;
                            InsertBal_Pmt[Pmt_cnt] = Inbal;
                            if (((xRow["Mode"] != "DISCOUNT") && (xRow["Mode"] != "WRITTEN OFF")))
                            {
                                string ID = System.Guid.NewGuid().ToString();
                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                InParam.VNo = "";
                                DateTime PmtDate = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                                InParam.TDate = PmtDate.ToString(BASE._Server_Date_Format_Short);
                                InParam.ItemID = "4E49B7D2-EB84-4B0D-A011-80DF20DDF116";
                                InParam.Type = "CREDIT";
                                InParam.Cr_Led_ID = "00226";
                                InParam.Dr_Led_ID = (((xRow["Mode"] == "CASH") || ((xRow["Mode"] == "MO") || (xRow["Mode"] == "EMO"))) ? "00080" : "00079");
                                InParam.SUB_Cr_Led_ID = "";
                                if (xRow["Mode"].ToString() == "CASH" || xRow["Mode"].ToString() == "MO" || xRow["Mode"].ToString() == "EMO")
                                {
                                    InParam.SUB_Dr_Led_ID = xRow["Dep_Bank_ID"].ToString();
                                }
                                InParam.Amount = xRow["Amount"] == null ? 0 : Convert.ToDouble(xRow["Amount"]);
                                InParam.Mode = xRow["Mode"] == null ? "" : xRow["Mode"].ToString();
                                InParam.Ref_BANK_ID = xRow["Ref_Bank_ID"] == null ? "" : xRow["Ref_Bank_ID"].ToString();
                                InParam.Ref_Branch = xRow["Ref Branch"] == null ? "" : xRow["Ref Branch"].ToString();
                                InParam.Ref_No = xRow["Ref No."] == null ? "" : xRow["Ref No."].ToString();
                                if (xRow["Ref Date"] != null)
                                {
                                    if (xRow["Ref Date"].ToString() != "")
                                    {
                                        InParam.Ref_Date = Convert.ToDateTime((xRow["Ref Date"] == null ? "" : xRow["Ref Date"])).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InParam.Ref_Date = xRow["Ref Date"] == null ? "" : xRow["Ref Date"].ToString();
                                    }
                                }
                                if (xRow["Ref Clearing Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Clearing Date"].ToString()))
                                    {
                                        if (xRow["Ref Clearing Date"].ToString() != "")
                                        {
                                            InParam.Ref_CDate = Convert.ToDateTime(((xRow["Ref Clearing Date"] == null) ? "" : xRow["Ref Clearing Date"])).ToString(BASE._Server_Date_Format_Short);
                                        }
                                        else
                                        {
                                            InParam.Ref_CDate = xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"].ToString();
                                        }
                                    }

                                }
                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                InParam.Narration = ("Payment of Rs. " + (InParam.Amount.ToString() + (" Received from Party(" + (model.PC_MemberName + (") with ID (" + (model.Txt_MS_ID + ") for Magazine Subscription\'"))))));
                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                InParam.SrNo = xRow["Sr."] == null ? "" : xRow["Sr."].ToString();
                                InParam.Status_Action = model.Status_Action;
                                InParam.RecID = Guid.NewGuid().ToString();
                                Insert_Transaction_pmt[Pmt_cnt] = InParam;
                                Pmt_cnt++;
                            }

                            // debit of discount
                            if ((xRow["Mode"] == "DISCOUNT"))
                            {
                                string ID = System.Guid.NewGuid().ToString();
                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                InParam.VNo = "";
                                DateTime PmtDate = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                                InParam.TDate = PmtDate.ToString(BASE._Server_Date_Format_Short);
                                InParam.ItemID = (has_Annual_Membership ? "2DB5DBA1-3CF8-400F-BA8D-803BAC8C1C02" : "29B916FE-AD12-4485-97A3-DDD6053D140E");
                                InParam.Type = "DEBIT";
                                InParam.Cr_Led_ID = "";
                                InParam.Dr_Led_ID = (has_Annual_Membership ? "00113" : "00224");
                                InParam.SUB_Cr_Led_ID = "";
                                InParam.SUB_Dr_Led_ID = "";
                                InParam.Amount = xRow["Amount"] == null ? 0 : Convert.ToDouble(xRow["Amount"].ToString());
                                InParam.Mode = xRow["Mode"] == null ? "" : xRow["Mode"].ToString();
                                InParam.Ref_BANK_ID = xRow["Ref_Bank_ID"] == null ? "" : xRow["Ref_Bank_ID"].ToString();
                                InParam.Ref_Branch = xRow["Ref Branch"] == null ? "" : xRow["Ref Branch"].ToString();
                                InParam.Ref_No = xRow["Ref No."] == null ? "" : xRow["Ref No."].ToString();
                                if (xRow["Ref Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Date"].ToString()))
                                    {
                                        if (xRow["Ref Date"].ToString() != "")
                                        {
                                            InParam.Ref_Date = Convert.ToDateTime(((xRow["Ref Date"] == null) ? "" : xRow["Ref Date"])).ToString(BASE._Server_Date_Format_Short);
                                        }
                                    }
                                }
                                else
                                {
                                    InParam.Ref_Date = xRow["Ref Date"] == null ? "" : xRow["Ref Date"].ToString();
                                }


                                //if (IsDate(xRow["Ref Clearing Date"]) == null ? "" : xRow["Ref Clearing Date"].ToString())
                                if (xRow["Ref Clearing Date"].ToString() != null)
                                {
                                    InParam.Ref_CDate = Convert.ToDateTime(((xRow["Ref Clearing Date"] == null) ? "" : xRow["Ref Clearing Date"])).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParam.Ref_CDate = xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"].ToString();
                                }

                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                InParam.Narration = ("Discount of Rs. " + (InParam.Amount.ToString() + (" given to Party(" + (model.PC_MemberName + (") with ID (" + (model.Txt_MS_ID + ") on Magazine Subscription\'"))))));
                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                InParam.SrNo = xRow["Sr."] == null ? "" : xRow["Sr."].ToString();
                                InParam.Status_Action = model.Status_Action;
                                InParam.RecID = Guid.NewGuid().ToString();
                                Insert_Transaction_pmt[Pmt_cnt] = InParam;
                                Pmt_cnt++;
                            }

                            // credit of discount
                            if ((xRow["Mode"] == "DISCOUNT"))
                            {
                                string ID = System.Guid.NewGuid().ToString();
                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                InParam.VNo = "";
                                DateTime PmtDate = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                                InParam.TDate = PmtDate.ToString(BASE._Server_Date_Format_Short);
                                InParam.ItemID = has_Annual_Membership ? "fa35f372-f43f-44ce-9256-f698cf3b5e2e" : "DC1DA818-73BE-4E35-BA16-2DF4EF487ED5";
                                InParam.Type = "CREDIT";
                                InParam.Cr_Led_ID = "00227";
                                InParam.Dr_Led_ID = "";
                                InParam.SUB_Cr_Led_ID = "";
                                InParam.SUB_Dr_Led_ID = "";
                                InParam.Amount = xRow["Amount"] == null ? 0 : Convert.ToDouble(xRow["Amount"].ToString());
                                InParam.Mode = xRow["Mode"] == null ? "" : xRow["Mode"].ToString();
                                InParam.Ref_BANK_ID = xRow["Ref_Bank_ID"] == null ? "" : xRow["Ref_Bank_ID"].ToString();
                                InParam.Ref_Branch = xRow["Ref Branch"] == null ? "" : xRow["Ref Branch"].ToString();
                                InParam.Ref_No = xRow["Ref No."] == null ? "" : xRow["Ref No."].ToString();
                                if (xRow["Ref Date"] != null)
                                {
                                    //if (IsDate(xRow["Ref Date"])==true)
                                    {
                                        InParam.Ref_Date = Convert.ToDateTime(xRow["Ref Date"] == null ? "" : xRow["Ref Date"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                }
                                else
                                {
                                    InParam.Ref_Date = xRow["Ref Date"] == null ? "" : xRow["Ref Date"].ToString();
                                }
                                if (xRow["Ref Clearing Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Clearing Date"].ToString()))
                                    {
                                        InParam.Ref_CDate = Convert.ToDateTime(xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                }
                                else
                                {
                                    InParam.Ref_CDate = xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"].ToString();
                                }


                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                InParam.Narration = ("Discount of Rs. " + (InParam.Amount.ToString() + (" given to Party(" + (model.PC_MemberName + (") with ID (" + (model.Txt_MS_ID + ") on Magazine Subscription\'"))))));
                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                InParam.SrNo = xRow["Sr."] == null ? "" : xRow["Sr."].ToString();
                                InParam.Status_Action = model.Status_Action;
                                InParam.RecID = Guid.NewGuid().ToString();
                                Insert_Transaction_pmt[Pmt_cnt] = InParam;
                                Pmt_cnt++;
                            }

                            // DEBIT of WRITTEN OFF
                            if ((xRow["Mode"].ToString() == "WRITTEN OFF"))
                            {
                                string ID = System.Guid.NewGuid().ToString();
                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                InParam.VNo = "";
                                DateTime PmtDate = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                                InParam.TDate = PmtDate.ToString(BASE._Server_Date_Format_Short);
                                InParam.ItemID = "155b3fa6-4cd2-4869-9e0d-44560321cda4";
                                InParam.Type = "DEBIT";
                                InParam.Cr_Led_ID = "";
                                InParam.Dr_Led_ID = "00120";
                                InParam.SUB_Cr_Led_ID = "";
                                InParam.SUB_Dr_Led_ID = "";
                                InParam.Amount = xRow["Amount"] == null ? 0 : Convert.ToDouble(xRow["Amount"].ToString());
                                InParam.Mode = xRow["Mode"] == null ? "" : xRow["Mode"].ToString();
                                InParam.Ref_BANK_ID = xRow["Ref_Bank_ID"] == null ? "" : xRow["Ref_Bank_ID"].ToString();
                                InParam.Ref_Branch = xRow["Ref Branch"] == null ? "" : xRow["Ref Branch"].ToString();
                                InParam.Ref_No = xRow["Ref No."] == null ? "" : xRow["Ref No."].ToString();
                                //if (IsDate(xRow["Ref Date"] == null ? "" : xRow["Ref Date"]))
                                if (xRow["Ref Date"].ToString() != null)
                                {
                                    InParam.Ref_Date = Convert.ToDateTime(xRow["Ref Date"] == null ? "" : xRow["Ref Date"]).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InParam.Ref_Date = xRow["Ref Date"] == null ? "" : xRow["Ref Date"].ToString();
                                }
                                if (xRow["Ref Clearing Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Clearing Date"].ToString()))
                                    {
                                        InParam.Ref_CDate = Convert.ToDateTime((xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"])).ToString(BASE._Server_Date_Format_Short);
                                    }
                                }
                                else
                                {
                                    InParam.Ref_CDate = xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"].ToString();
                                }
                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                InParam.Narration = ("Balance of Rs. " + (InParam.Amount.ToString() + (" written off against Party(" + (model.PC_MemberName + (") with ID (" + (model.Txt_MS_ID + ") on Magazine Subscription\'"))))));
                                InParam.MasterTxnID = xRow["Tr_M_ID"].ToString();
                                InParam.SrNo = xRow["Sr."] == null ? "" : xRow["Sr."].ToString();
                                InParam.Status_Action = model.Status_Action;
                                InParam.RecID = Guid.NewGuid().ToString();
                                Insert_Transaction_pmt[Pmt_cnt] = InParam;
                                Pmt_cnt++;
                            }

                            // credit of WRITTEN OFF
                            if ((xRow["Mode"] == "WRITTEN OFF"))
                            {
                                string ID = System.Guid.NewGuid().ToString();
                                //  Dim ScreenCode As Global_Set.Voucher_Screen_Code = Voucher_Screen_Code.Membership_Renewal
                                Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMagazineMembership();
                                InParam.TransCode = Convert.ToInt32(Common_Lib.Common.Voucher_Screen_Code.Magazine_Payment);
                                InParam.VNo = "";
                                DateTime PmtDate = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                                InParam.TDate = PmtDate.ToString(BASE._Server_Date_Format_Short);
                                InParam.ItemID = "22AEE686-FD90-43E7-9DEA-60E6CDA0ACB6";
                                InParam.Type = "CREDIT";
                                InParam.Cr_Led_ID = "00227";
                                InParam.Dr_Led_ID = "";
                                InParam.SUB_Cr_Led_ID = "";
                                InParam.SUB_Dr_Led_ID = "";
                                InParam.Amount = xRow["Amount"] == null ? 0 : Convert.ToDouble(xRow["Amount"].ToString());
                                InParam.Mode = xRow["Mode"] == null ? "" : xRow["Mode"].ToString();
                                InParam.Ref_BANK_ID = xRow["Ref_Bank_ID"] == null ? "" : xRow["Ref_Bank_ID"].ToString();
                                InParam.Ref_Branch = xRow["Ref Branch"] == null ? "" : xRow["Ref Branch"].ToString();
                                InParam.Ref_No = xRow["Ref No."] == null ? "" : xRow["Ref No."].ToString();
                                if (xRow["Ref Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Date"].ToString()))
                                    {

                                        InParam.Ref_Date = Convert.ToDateTime((xRow["Ref Date"] == null ? "" : xRow["Ref Date"])).ToString(BASE._Server_Date_Format_Short);
                                    }

                                }
                                else
                                {
                                    InParam.Ref_Date = xRow["Ref Date"] == null ? "" : xRow["Ref Date"].ToString();
                                }
                                if (xRow["Ref Clearing Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Clearing Date"].ToString()))
                                    {
                                        InParam.Ref_CDate = Convert.ToDateTime((xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"])).ToString(BASE._Server_Date_Format_Short);
                                    }
                                }
                                else
                                {
                                    //xRow["Ref Clearing Date"]
                                    InParam.Ref_CDate = xRow["Ref Clearing Date"].ToString();
                                }
                                if (xRow["Ref Clearing Date"] != null)
                                {
                                    if (IsDate(xRow["Ref Clearing Date"].ToString()))
                                    {
                                        InParam.Ref_CDate = Convert.ToDateTime((xRow["Ref Clearing Date"] == null ? "" : xRow["Ref Clearing Date"])).ToString(BASE._Server_Date_Format_Short);
                                    }
                                }
                                else
                                {
                                    //xRow["Ref Clearing Date"]
                                    InParam.Ref_CDate = xRow["Ref Clearing Date"].ToString();
                                }

                                InParam.Party1 = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                                InParam.Narration = ("Balance of Rs. " + (InParam.Amount.ToString() + (" written off against Party(" + (model.PC_MemberName + (") with ID (" + (model.Txt_MS_ID + ") on Magazine Subscription\'"))))));
                                //xRow["Tr_M_ID"];
                                InParam.MasterTxnID = "";
                                //InParam.SrNo = xRow["Sr."] == null ? 1 : xRow["Sr."].ToString();
                                InParam.SrNo = "1";
                                InParam.Status_Action = model.Status_Action;
                                InParam.RecID = Guid.NewGuid().ToString();
                                Insert_Transaction_pmt[Pmt_cnt] = InParam;
                                Pmt_cnt++;
                            }
                        }
                    }
                }
            }
            InNewParam.Param_InsertPurpose_Pmt = InsertPurpose_Pmt;
            InNewParam.Param_Insert_Payment_Txn = Insert_Transaction_pmt;
            InNewParam.Param_InsertPmtBalances = InsertBal_Pmt;

            Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMagazineMembership[] InsertPayment = new Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMagazineMembership[Payment_Detail.Rows.Count];
            Int32 ctr = 0;

            if ((Payment_Detail.Rows.Count > 0))
            {
                foreach (DataRow xRow in Payment_Detail.Rows)
                {
                    if (((xRow["Tr_M_ID"] != null) && !(xRow["Tr_M_ID"] == null)))
                    {
                        if (xRow["Tr_M_ID"].ToString() == model.xMID || Convert.ToBoolean(xRow["UPDATED"].ToString()))
                        {
                            Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMagazineMembership InPmt = new Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMagazineMembership();
                            InPmt.TxnMID = xRow["Tr_M_ID"].ToString();
                            InPmt.Type = "MAGAZINE";
                            InPmt.SrNo = (xRow["Sr."].ToString() == null ? "" : xRow["Sr."].ToString());
                            InPmt.Mode = (xRow["Mode"].ToString() == null ? "" : xRow["Mode"].ToString());
                            InPmt.RefID = (xRow["Ref_Bank_ID"].ToString() == null ? "" : xRow["Ref_Bank_ID"].ToString());
                            InPmt.RefBranch = ((xRow["Ref Branch"].ToString() == null) ? "" : xRow["Ref Branch"].ToString());
                            InPmt.RefNo = (xRow["Ref No."].ToString() == null ? "" : xRow["Ref No."].ToString());
                            DateTime RefDate = (Convert.ToDateTime(xRow["Ref Date"].ToString()) == null ? DateTime.MinValue : Convert.ToDateTime(xRow["Ref Date"].ToString()));
                            if (IsDate((xRow["Ref Date"].ToString() == null ? "" : xRow["Ref Date"].ToString())))
                            {
                                InPmt.RefDate = RefDate.ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPmt.RefDate = DateTime.MinValue.ToString();
                            }

                            //DateTime RefCDate = ((Convert.ToDateTime(xRow["Ref Clearing Date"].ToString()) == null) ? DateTime.MinValue.ToString() : xRow["Ref Date"].ToString());
                            DateTime? RefCDate = null;

                            if (IsDate(((xRow["Ref Clearing Date"].ToString() == null) ? "" : xRow["Ref Clearing Date"].ToString())))
                            {
                                //InPmt.ClearingDate = RefCDate.ToString(BASE._Server_Date_Format_Short);
                                InPmt.ClearingDate = RefCDate.ToString();
                            }
                            else
                            {
                                InPmt.ClearingDate = DateTime.MinValue.ToString();
                            }

                            InPmt.RefAmount = double.Parse(xRow["Amount"].ToString());
                            InPmt.Dep_BA_ID = ((xRow["Dep_Bank_ID"].ToString() == null) ? "" : xRow["Dep_Bank_ID"].ToString());
                            InPmt.Status_Action = model.Status_Action;
                            InPmt.RecID = System.Guid.NewGuid().ToString();
                            DateTime Pmt_Date = Convert.ToDateTime(xRow["Pmt_Date"].ToString());
                            InPmt.Pmt_Date = Pmt_Date.ToString(BASE._Server_Date_Format_Short);
                            InPmt.Status_Action = "1";
                            InsertPayment[ctr] = InPmt;
                            ctr++;
                        }
                    }
                }
            }
            InNewParam.Param_InsertPayment = InsertPayment;
            string[] delVouchers = new string[Deleted_Vouchers.Rows.Count + 1];
            if ((Deleted_Vouchers.Rows.Count > 0))
            {
                for (Int16 ctrs = 0; (ctrs <= (Deleted_Vouchers.Rows.Count - 1)); ctrs++)
                {
                    delVouchers[ctrs] = Deleted_Vouchers.Rows[ctrs]["Tr_M_ID"].ToString();
                }

            }
            InNewParam.Param_DeletedVouchers = delVouchers;

            if ((Membership == "Renewal"))
            {
                Common_Lib.RealTimeService.Param_Update_Magazine_Membership update_Magazine = new Common_Lib.RealTimeService.Param_Update_Magazine_Membership();
                if (model.Chk_ConCenter == true)
                {
                    update_Magazine.MM_CC_MS_ID = model.BE_CCMemberName;
                    update_Magazine.MM_CC_APPLICABLE = "YES";
                    update_Magazine.MM_CC_SPONSORED = (model.Chk_Sponsored == true ? true : false);
                }
                else
                {
                    update_Magazine.MM_CC_MS_ID = null;
                    update_Magazine.MM_CC_APPLICABLE = "NO";
                    update_Magazine.MM_CC_SPONSORED = (model.Chk_Sponsored == true ? true : false);
                }

                update_Magazine.MM_MS_OLD_ID = model.Txt_MS_Old_ID;
                update_Magazine.MM_OTHER_DETAIL = model.ME_OtherDetails;
                update_Magazine.MS_REC_ID = model.xMS_Rec_ID;
                update_Magazine.PartyID = (model.Chk_Sponsored == true ? connected_member_ab_id : model.PC_MemberName);
                InNewParam.Param_Update_Membership = update_Magazine;
            }
            if (!BASE._Voucher_Magazine_DBOps.InsertMagazineMembership_Txn(InNewParam))
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
            }

            if ((GenerateReceipt && (HasPayment || HasSubscription)))
            {
                object GetValue = "";
                object xTemp_ID = model.xMID;
                GetValue = BASE._Voucher_Magazine_DBOps.GetDiscontinued(true, model.xTemp_ID);
                if ((GetValue == null))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                    // Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                }

                if ((GetValue.ToString().ToUpper() == "DISCONTINUED"))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   R e c e i p t   c a n n o t   b e   G e n e r a t e d . . . !" +
                    //    "", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return Json(new { Message = "D i s c o n t i u n e d   M e m b e r   R e c e i p t   c a n n o t   b e   G e n e r a t e d . . . !", result = false }, JsonRequestBehavior.AllowGet);
                    // Base.ShowMessagebox("Information...", "D i s c o n t i u n e d   M e m b e r   R e c e i p t   c a n n o t   b e   G e n e r a t e d . . . !", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                }

                // 1 Check Already Generated Receipt
                object rCount = BASE._Voucher_Magazine_DBOps.GetReceiptCount(model.xTemp_ID);
                if ((rCount == null))
                {
                    //Base.HandleDBError_OnNothingReturned();
                    return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                }

                if ((rCount.ToString().Length > 0))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(("M e m b e r s h i p   R e c e i p t   a l r e a d y   g e n e r a t e d . . . !" + ("\r\n" + ("\r\n" + "Note: Refresh Current List, if Receipt not shown."))), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Base.ShowMessagebox("Information....", "M e m b e r s h i p   R e c e i p t   a l r e a d y   g e n e r a t e d . . . !" & vbNewLine & vbNewLine & "Note: Refresh Current List, if Receipt not shown.", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    return Json(new { Message = "M e m b e r s h i p   R e c e i p t   a l r e a d y   g e n e r a t e d . . . !" + "\r\n" + "\r\n" + "Note: Refresh Current List, if Receipt not shown." });
                }
                //return Json(new { Message= "Confirmation...", "<u><b>Sure you want to Generate Receipt of this Entry...?</b></u>",result=true })
                //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", "<u><b>Sure you want to Generate Receipt of this Entry...?</b></u>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                {
                    // xPromptWindow.Dispose()
                    // '2 Check for Another Transacation without Receipt Generated
                    // Dim xDate As DateTime = Me.GridView.GetRowCellValue(Val(Me.GridView.Tag), "Add Date").ToString() : Dim TrFound As Boolean = False
                    // Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                    // Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xTemp_ID)
                    // If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                    // If T1.Rows.Count > 0 Then
                    //     For Each XRow In T1.Rows
                    //         If XRow("REC_ID") = xTemp_ID Then : Continue For
                    //         Else
                    //             xDate = XRow("REC_ADD_ON")
                    //             If Format(xDate, "yyyy-MM-dd HH:mm:ss") < CurRecordAddOn Then
                    //                 If IsDBNull(XRow("MR_NO")) Then TrFound = True : Exit For
                    //             End If
                    //         End If
                    //     Next
                    //     If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("P l e a s e   G e n e r a t e   r e c e i p t s   f o r   o l d e r   e n t r i e s   o f   t h e   s a m e   M e m b e r   b e f o r e   g e n e r a t i n g   t h i s   r e c e i p t . . . !", "Cannot Generate...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                    // End If
                    if (!BASE._Voucher_Magazine_DBOps.InsertMagazineMembership_Receipt(model.xTemp_ID, Convert.ToDateTime(model.Txt_MS_Date), model.VDate, model.PC_MemberName, model.Cmb_MemType))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        // Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                        return Json(new { Message = "Error!!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    //else
                    //{
                    //    var successParams = new
                    //    {
                    //        Text = "Text",
                    //    };
                    //    return Json(new
                    //    {
                    //        Message = "M e m b e r s h i p   R e c e i p t   G e n e r a t e d   S u c c e s s f u l l y",
                    //        result = true,
                    //        SuccessParams = successParams
                    //    }, JsonRequestBehavior.AllowGet);
                    //}

                    //DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   G e n e r a t e d   S u c c e s s f u l l y", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Base.ShowMessagebox(Me.Text, "M e m b e r s h i p   R e c e i p t   G e n e r a t e d   S u c c e s s f u l l y", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    //xPromptWindow = new Common_Lib.Prompt_Window();
                    //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", "<u><b>Print Membership Receipt Now...?</b></u>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                    //{
                    //    // xPromptWindow.Dispose()
                    //    Rec_Membership_Print xRep = new Rec_Membership_Print();
                    //    xRep.MainBase = Base;
                    //    xRep.Master_ID = xTemp_ID;
                    //    Base.Show_ReportPreview(xRep, ("Receipt Dated " + VDate.ToString("dd-MMM-yy")), this, false);
                    //}
                    //else
                    //{
                    //    // xPromptWindow.Dispose()
                    //}

                }

            }
            return Json(new { Message = "Saved Successfully", result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertMagazineMembership_Receipt(string xTemp_ID = "", string Txt_MS_Date = "", string VDate = "", string PC_MemberName = "", string Cmb_MemType = "")
        {
            if (!BASE._Voucher_Magazine_DBOps.InsertMagazineMembership_Receipt(xTemp_ID, Convert.ToDateTime(Txt_MS_Date), Convert.ToDateTime(VDate), PC_MemberName, Cmb_MemType))
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult PrintReceipt()
        {

            // xPromptWindow.Dispose()
            //Rec_Membership_Print xRep = new Rec_Membership_Print();
            //xRep.MainBase = Base;
            //xRep.Master_ID = xTemp_ID;
            //Base.Show_ReportPreview(xRep, ("Receipt Dated " + VDate.ToString("dd-MMM-yy")), this, false);
            //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, "Magazine Membership Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information)
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckSubsDate(string Txt_MS_Date = "", string Membership = "")
        {

            if (IsDate(Txt_MS_Date) == false)
            {
                return Json(new { Message = "D a t e   I n c o r r e c t   /   B l a n k . . . ", result = false }, JsonRequestBehavior.AllowGet);
            }
            if ((IsDate(Txt_MS_Date) == true) && Membership == "New")
            {
                //1
                DateTime d1 = BASE._open_Year_Sdt;
                DateTime d2 = Convert.ToDateTime(Txt_MS_Date);
                double diff = (d2 - d1).TotalDays;
                if (diff < 0)
                {
                    return Json(new { Message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", result = false }, JsonRequestBehavior.AllowGet);
                }
                //2
                DateTime d3 = BASE._open_Year_Edt;
                DateTime d4 = Convert.ToDateTime(Txt_MS_Date);
                diff = (d4 - d3).TotalDays;
                if (diff > 0)
                {
                    return Json(new { Message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Message = "Success", result = true }, JsonRequestBehavior.AllowGet);

        }
        private string CheckDispatchRestrictions()
        {
            string RetString = "";
            foreach (DataRow subRow in Subs_Detail.Rows)
            {
                foreach (DataRow dispRow in Dispatch_detail.Rows)
                {
                    DateTime? ToDate = null;
                    if ((subRow["To Date"] != null))
                    {
                        ToDate = Convert.ToDateTime(dispRow["ISSUE_DATE"].ToString());
                    }
                    else
                    {
                        ToDate = Convert.ToDateTime(subRow["To Date"].ToString());
                    }

                    if (((Convert.ToDateTime(dispRow["ISSUE_DATE"].ToString()) >= Convert.ToDateTime(subRow["Fr. Date"].ToString())) && (Convert.ToDateTime(dispRow["ISSUE_DATE"].ToString()) <= ToDate)))
                    {
                        dispRow["ACTUAL_COPIES"] = (double.Parse(dispRow["ACTUAL_COPIES"].ToString()) + double.Parse(subRow["Copies"].ToString()));
                    }

                }

            }

            foreach (DataRow dispRow in Dispatch_detail.Rows)
            {
                if ((dispRow["ACTUAL_COPIES"].ToString().Length < dispRow["DISP_COPIES"].ToString().Length))
                {
                    DateTime issueDate = Convert.ToDateTime(dispRow["ISSUE_DATE"].ToString());
                    RetString = "Dispatched Copies" + dispRow["DISP_COPIES"].ToString() + " greater than subscribed copies" + dispRow["ACTUAL_COPIES"].ToString() + " for Issue dated :" + issueDate.ToString(BASE._Date_Format_DD_MMM_YYYY);
                }

            }

            foreach (DataRow dispRow in Dispatch_detail.Rows)
            {
                dispRow["ACTUAL_COPIES"] = 0;
            }

            return RetString;
        }
        [HttpGet]
        public ActionResult Frm_Magazine_Subs_Window(string ActionMethod, string SrID = null, string MagazineID = null, string Membership = null, DateTime? Txt_Subs_Date = null, string Publish_On = null, string iLastSubs_ID = null, int Txt_Copies = 0, string iLastDis_Id = null, string Tr_MID = null, string Type = null)
        {
            DataTable Subtype = new DataTable();
            DataTable Dispatch = new DataTable();
            Subscriptiondetails_Model model = new Subscriptiondetails_Model();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            Subs_Detail = Session["Subs_Detail_Data"] as DataTable;
            Payment_Detail = Session["Payment_Data"] as DataTable;

            Membership = Session["Membership"] == null ? "New" : Session["Membership"].ToString();
            ViewBag.MagazineId = MagazineID;
            model.VoucherType = Membership == "Renewal" ? "ADDITION" : "CREATION";
            if (ActionMethod == "_New")
            {

                Boolean AlreadyAddedPayment = false;
                if (Payment_Detail != null)
                {
                    foreach (DataRow cRow in Payment_Detail.Rows)
                    {
                        if (!string.IsNullOrEmpty(cRow["Tr_M_ID"].ToString()))
                            if (cRow["Tr_M_ID"].Equals(xMID))
                                AlreadyAddedPayment = true;
                    }
                }

                if (Membership == "Renewal" && AlreadyAddedPayment == false)
                {
                    Txt_Subs_Date = DateTime.Today;
                }

                PrevSubsEndDate = string.IsNullOrEmpty(Convert.ToString(Session["PrevSubsEndDate"])) ? PrevSubsEndDate : Convert.ToDateTime(Session["PrevSubsEndDate"]);
                PrevSubsStartDate = string.IsNullOrEmpty(Convert.ToString(Session["PrevSubsStartDate"])) ? PrevSubsStartDate : Convert.ToDateTime(Session["PrevSubsStartDate"]);
                Default_Subs_Detail = Session["Default_Subs_Detail"] as DataTable;
                if (Default_Subs_Detail != null)
                {
                    if (Default_Subs_Detail.Rows.Count > 0)
                    {
                        Txt_Copies = Convert.ToInt32(Default_Subs_Detail.Rows[0]["Copies"]);
                    }
                    else
                    {
                        Txt_Copies = 0;
                    }
                }

                //GLookUp_STypeList
                string GLookUp_STypeList = "";
                DataTable Magazinelist = BASE._Magazine_DBOps.GetList_SubscriptionTypeList("", "", " and ST.MST_MI_ID = '" + MagazineID + "' ") as DataTable;
                var List_GLookUp_STypeList = Magazinelist;
                Session["GLookUp_STypeList"] = List_GLookUp_STypeList;
                string DefaultSubID = "";
                foreach (DataRow cRow in Magazinelist.Rows)
                {
                    if (Convert.ToBoolean(cRow["Default"]))
                    {
                        DefaultSubID = cRow["ID"].ToString();
                        model.SubscriptionType = cRow["Type"].ToString();
                        model.Subs_Short_Name = cRow["Short Name"].ToString();
                        model.Subs_Start_Month = Convert.ToInt32(cRow["St_Month"]);
                        model.Subs_Min_Months = Convert.ToInt32(cRow["Min.Months"]);
                        model.Subs_Fixed_Period = cRow["Fixed Period"].ToString();
                        model.Subs_PeriodWiseFeeCalc = cRow["Period wise Fee Calculation"].ToString();
                    }
                }
                if (Membership == "Renewal")
                {
                    if (iLastSubs_ID.Length > 0)
                    {
                        GLookUp_STypeList = iLastSubs_ID;
                    }
                }
                else
                {
                    if (DefaultSubID.Length > 0)
                    {
                        GLookUp_STypeList = DefaultSubID;
                    }
                }

                if (!(Default_Issue_Date == DateTime.MinValue))
                    Txt_Fr_Date = Default_Issue_Date;
                if (IsDate(Convert.ToDateTime(Txt_Subs_Date).ToString()))
                {
                    if (Txt_Fr_Date == DateTime.MinValue)
                        CalculateFromDate(PrevSubsEndDate, Txt_Subs_Date, Membership, Default_Issue_Date, Publish_On, model.Subs_Min_Months);
                    if (model.Subs_Min_Months == 0)
                    {
                        if (model.Subs_Fixed_Period == "YES")
                        {
                            Txt_Fr_Date = new DateTime(DateTime.Now.Year, model.Subs_Start_Month, 1);
                            Txt_To_Date = DateTime.Now; ;
                            //this.Txt_Fr_Date.Enabled = false; this.Txt_To_Date.Enabled = false;
                        }
                        else
                        {
                            Txt_To_Date = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (model.Subs_Fixed_Period == "YES")
                        {
                            Txt_Fr_Date = new DateTime(DateTime.Now.Year, model.Subs_Start_Month, 1);
                            DateTime _FrDate = Txt_Fr_Date;
                            Txt_To_Date = _FrDate.AddMonths(model.Subs_Min_Months).AddDays(-1);

                        }
                        else
                        {
                            if (IsDate(Txt_Fr_Date.ToString()))
                            {
                                DateTime _FrDate = Txt_Fr_Date;
                                Txt_To_Date = _FrDate.AddMonths(model.Subs_Min_Months).AddDays(-1);
                            }
                        }
                    }
                    DateTime _SDate = Convert.ToDateTime(Txt_Subs_Date);
                    Subtype = BASE._Magazine_DBOps.GetSubscriptionTypeFee(GLookUp_STypeList, _SDate);
                    if (Subtype.Rows.Count > 0)
                    {
                        MSTF_Indian_Fee = Convert.ToDouble(Subtype.Rows[0]["MSTF_INDIAN_FEE"]);
                        MSTF_Foreign_Fee = Convert.ToDouble(Subtype.Rows[0]["MSTF_FOREIGN_FEE"]);
                        DateTime xDate = default(DateTime);
                        if (!string.IsNullOrEmpty(Subtype.Rows[0]["MSTF_EFF_DATE"].ToString()))
                            xDate = Convert.ToDateTime(Subtype.Rows[0]["MSTF_EFF_DATE"]);
                        model.MSTF_Indian_Fee = MSTF_Indian_Fee;
                        model.MSTF_Foreign_Fee = MSTF_Foreign_Fee;
                    }
                }
                else
                {

                }
                //GLookUp_STypeList
                //DisPatch List
                DataTable Dispatchinfo = BASE._Magazine_DBOps.GetList_DispatchTypeList("", "", "");
                Session["Dispatchinfo"] = Dispatchinfo;
                string GLookUp_DTypeList = "";
                string DefaultDispID = "";
                foreach (DataRow cRow1 in Dispatchinfo.Rows)
                {
                    if (Convert.ToBoolean(cRow1["Default"]))
                        DefaultDispID = cRow1["ID"].ToString();
                    model.DispatchName = cRow1["Name"].ToString();
                }
                if (Membership == "Renewal")
                {
                    if (iLastDis_Id.Length > 0)
                    {
                        GLookUp_DTypeList = iLastDis_Id;
                    }
                }
                else
                {
                    if (DefaultSubID.Length > 0)
                    {
                        GLookUp_DTypeList = DefaultDispID;
                    }
                }

                if (IsDate(Convert.ToDateTime(Txt_Subs_Date).ToString()))
                {
                    DateTime _SDate = Convert.ToDateTime(Txt_Subs_Date);
                    Dispatch = BASE._Magazine_DBOps.GetDispatchTypeCharges(GLookUp_DTypeList, _SDate);
                    if (Dispatch.Rows.Count > 0)
                        DT_Charges = Convert.ToDouble(Dispatch.Rows[0]["MDTC_CHARGES"]) * Convert.ToDouble(Txt_Issues);
                    else
                        DT_Charges = 0;
                }
                else
                {

                }
                model.DT_Charges = DT_Charges;
                //DisPatch List
                DataTable Purposes = BASE._Gift_DBOps.GetProjects("PUR_NAME", "PUR_ID");
                var gLookUp_PurList = new List<Subscriptiondetails_Model>();
                foreach (DataRow row in Purposes.Rows)
                {
                    var addItem = new Subscriptiondetails_Model();
                    addItem.PUR_NAME = row.Field<string>("PUR_NAME");
                    addItem.PUR_ID = row.Field<string>("PUR_ID");
                    gLookUp_PurList.Add(addItem);
                }
                var list = gLookUp_PurList.Where(f => f.PUR_ID == "8f6b3279-166a-4cd9-8497-ca9fc6283b25").Select(p => p.PUR_NAME).ToList();
                Amount_Calculation(Txt_Copies, Txt_Fr_Date, Txt_To_Date, MagazineID, model.Subs_PeriodWiseFeeCalc, model.Subs_Min_Months, Publish_On, DT_Charges);


                model.Txt_Subs_Date = Txt_Subs_Date;
                model.GLookUp_STypeList = Subtype.Rows.Count > 0 ? GLookUp_STypeList : "";
                model.BE_Purpose = list[0];
                model.Txt_Free = Convert.ToInt32(Txt_Free);
                model.Txt_Copies = Convert.ToInt32(Txt_Copies);
                model.Txt_Fr_Date = Txt_Fr_Date;
                model.Txt_To_Date = Txt_To_Date;
                model.BE_SubAmt = BE_SubAmt;
                model.GLookUp_DTypeList = Dispatch.Rows.Count > 0 ? GLookUp_DTypeList : "";
                model.BE_ExtraAmt = BE_ExtraAmt;
                model.Txt_Issues = Txt_Issues;
                model.Rad_DisOnCC = "NO";
                model.BE_TotalAmt = BE_TotalAmt;
                model.CurrYearIssueCount = CurrYearIssueCount;
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Subscription_Window_Grid>)Session["Grid_Data_Subscription_Window_Data_Session"];
                var dataToEdit = all_data.FirstOrDefault(x => x.Sr == Sr);

                DataTable MagazinelistEdit = BASE._Magazine_DBOps.GetList_SubscriptionTypeList("", "", " and ST.MST_MI_ID = '" + MagazineID + "' ") as DataTable;
                string DefaultSubID = "";
                // DataRow[] dr = MagazinelistEdit.Select("Type = " + dataToEdit.SubsType);
                List<DataRow> dr = (from row in MagazinelistEdit.AsEnumerable()
                                    where row.Field<string>("Type") == dataToEdit.SubsType
                                    select row).ToList();
                if (dr.Count > 0)
                {
                    DefaultSubID = dr.FirstOrDefault()["ID"].ToString();
                    model.Subs_Short_Name = dr.FirstOrDefault()["Type"].ToString();
                    model.SubscriptionType = dr.FirstOrDefault()["Short Name"].ToString();
                    model.Subs_Start_Month = Convert.ToInt32(dr.FirstOrDefault()["St_Month"]);
                    model.Subs_Min_Months = Convert.ToInt32(dr.FirstOrDefault()["Min.Months"]);
                    model.Subs_Fixed_Period = dr.FirstOrDefault()["Fixed Period"].ToString();
                    model.Subs_PeriodWiseFeeCalc = dr.FirstOrDefault()["Period wise Fee Calculation"].ToString();
                }

                model.Membership = Type == "CREATION" ? "New" : "Renewal";

                DateTime _SDate = Convert.ToDateTime(Txt_Subs_Date);
                DataTable Dispatch_Edit = BASE._Magazine_DBOps.GetDispatchTypeCharges(dataToEdit.Dis_ID, _SDate);
                if (Dispatch.Rows.Count > 0)
                    DT_Charges = Convert.ToDouble(Dispatch.Rows[0]["MDTC_CHARGES"]) * Convert.ToDouble(Txt_Issues);
                else
                    DT_Charges = 0;
                if (Subs_Detail != null)
                {
                    foreach (DataRow R1 in Subs_Detail.Rows)
                    {
                        if (!string.IsNullOrEmpty(R1["Tr_M_ID"].ToString()))
                        {
                            if (R1["Tr_M_ID"].ToString() != dataToEdit.Tr_M_ID)
                            {
                                if (!string.IsNullOrEmpty(R1["To Date"].ToString()))
                                {
                                    if (PrevSubsEndDate < Convert.ToDateTime(R1["To Date"]))
                                    {
                                        PrevSubsEndDate = Convert.ToDateTime(R1["To Date"]);
                                    }
                                }
                                if (!string.IsNullOrEmpty(R1["Fr. Date"].ToString()))
                                {
                                    if (PrevSubsEndDate > Convert.ToDateTime(R1["Fr. Date"]) || PrevSubsStartDate == DateTime.MinValue)
                                    {
                                        PrevSubsStartDate = Convert.ToDateTime(R1["Fr. Date"]);
                                    }
                                }
                            }
                        }
                    }
                }

                Amount_Calculation(Txt_Copies, Txt_Fr_Date, Txt_To_Date, MagazineID, model.Subs_PeriodWiseFeeCalc, model.Subs_Min_Months, Publish_On, DT_Charges);
                model.DT_Charges = DT_Charges;
                model.Sr = Sr;
                model.Txt_Subs_Date = dataToEdit.SubsDate;
                model.GLookUp_STypeList = dataToEdit.Subs_ID;
                model.BE_Purpose = dataToEdit.Pur_ID;
                model.Txt_Free = dataToEdit.Free;
                model.Txt_Copies = dataToEdit.Copies;
                model.Txt_Fr_Date = dataToEdit.FrDate;
                model.Txt_To_Date = dataToEdit.ToDate;
                model.BE_SubAmt = Convert.ToDouble(dataToEdit.SubAmt);
                model.GLookUp_DTypeList = dataToEdit.Dis_ID;
                model.BE_ExtraAmt = Convert.ToDouble(dataToEdit.DispAmt);
                model.Txt_Issues = dataToEdit.Txtissues;
                model.Rad_DisOnCC = dataToEdit.DisponCC;
                model.BE_TotalAmt = Convert.ToDouble(dataToEdit.TotalAmt);
                model.Txt_Narration = dataToEdit.Narration;
                model.Txt_Reference = dataToEdit.Reference;
                model.CurrYearIssueCount = CurrYearIssueCount;
                model.Tr_M_ID = dataToEdit.Tr_M_ID;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Magazine_Subs_Window(Subscriptiondetails_Model SubScription)
        {
            SubScription.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), SubScription.ActionMethod.ToString());

            try
            {
                if (SubScription.Tag == Common_Lib.Common.Navigation_Mode._New | SubScription.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (SubScription.GLookUp_DTypeList == null && SubScription.GLookUp_DTypeList == "")
                    {
                        return Json(new
                        {
                            Message = "S u b c r i p t i o n   T y p e   n o t   S e l e c t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(SubScription.Txt_Fr_Date.ToString()) == false)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "F r o m  D a t e   I n c o r r e c t   /   B l a n k . . . !"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(SubScription.Txt_To_Date.ToString()) == false)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "T O D a t e   I n c o r r e c t   /   B l a n k . . . !"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(SubScription.Txt_Fr_Date.ToString()) == false && IsDate(SubScription.Txt_To_Date.ToString()) == false && xMID != null)
                    {

                    }
                    if (SubScription.Txt_Copies < 0)
                    {
                        return Json(new
                        {
                            Message = "N o .   o f   C o p i e s   c a n n o t   b e   N e g a t i v e  . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (SubScription.GLookUp_DTypeList == null && SubScription.GLookUp_DTypeList == "")
                    {
                        return Json(new
                        {
                            Message = "D i s p a t c h   T y p e   N o t   S e l e c t e d . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (SubScription.Txt_Copies < SubScription.Txt_Free)
                    {
                        return Json(new
                        {
                            Message = "F r e e   C o p i e s   c a n n o t   b e   g r e a t e r   t h a n   T o t a l   C o p i e s . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (IsDate(SubScription.Txt_Subs_Date.ToString()) == true)
                    {
                        DateTime firstDate = BASE._open_Year_Sdt;
                        DateTime secondDate = SubScription.Txt_Subs_Date ?? DateTime.Now;
                        TimeSpan diff = secondDate.Subtract(firstDate);
                        TimeSpan diff1 = secondDate - firstDate;
                        double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());

                        if (diff2 < 0)
                        {
                            return Json(new
                            {
                                result = false,
                                message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                            }, JsonRequestBehavior.AllowGet);
                        }

                        DateTime firstDate1 = BASE._open_Year_Edt;
                        DateTime secondDate1 = SubScription.Txt_Subs_Date ?? DateTime.Now;
                        TimeSpan diff3 = secondDate1.Subtract(firstDate1);
                        TimeSpan diff4 = secondDate1 - firstDate1;
                        double diff5 = Convert.ToDouble((secondDate1 - firstDate1).TotalDays.ToString());

                        if (diff5 > 0)
                        {
                            return Json(new
                            {
                                result = false,
                                message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (VoucherType != "CREATION")
                        {
                            //diff = DateDiff(DateInterval.Day, DateValue(this.Txt_Pmt_Date.DateTime), SubsStartDate);
                            DateTime firstDate2 = SubScription.Txt_Subs_Date ?? DateTime.Now;
                            DateTime secondDate2 = SubsStartDate;
                            TimeSpan diff6 = secondDate2.Subtract(firstDate2);
                            TimeSpan diff7 = secondDate2 - firstDate1;
                            double diff8 = Convert.ToDouble((secondDate2 - firstDate1).TotalDays.ToString());
                            if (diff8 > 0)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "D a t e   c a n ' t   b e    s m a l l e r   t h a n   S u b s .  S t a r t   D a t e  (" + SubsStartDate.ToShortDateString() + "). . . !"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                else
                {

                }


                List<Subscription_Window_Grid> gridRows = new List<Subscription_Window_Grid>();
                if (Session["Grid_Data_Subscription_Window_Data_Session"] != null)
                {
                    gridRows = (List<Subscription_Window_Grid>)Session["Grid_Data_Subscription_Window_Data_Session"];
                }
                if (SubScription.Tag == Common_Lib.Common.Navigation_Mode._New)
                {
                    double Curr_year_income = 0;
                    if (Curr_year_income == 0)
                    {
                        Curr_year_income = 0;
                        if ((SubScription.Txt_Issues != 0))
                            Curr_year_income = CurrYearIssueCount == Convert.ToDouble(SubScription.Txt_Issues) ? Convert.ToDouble(SubScription.BE_TotalAmt) : (Convert.ToDouble(SubScription.BE_TotalAmt) * Convert.ToDouble(CurrYearIssueCount)) / Convert.ToDouble(SubScription.Txt_Issues);
                    }

                    Subscription_Window_Grid grid = new Subscription_Window_Grid();
                    if ((gridRows.Count <= 0))
                    {
                        grid.Sr = 1;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(gridRows.FirstOrDefault().Sr.ToString()))
                        { grid.Sr = 1; }
                        else
                        { grid.Sr = gridRows.Count + 1; }
                    }

                    grid.SubsType = SubScription.SubscriptionType;
                    grid.Subs_ID = SubScription.GLookUp_STypeList;
                    grid.SubsDate = SubScription.Txt_Subs_Date;
                    grid.FrDate = SubScription.Txt_Fr_Date;
                    grid.ToDate = SubScription.Txt_To_Date;
                    grid.Copies = SubScription.Txt_Copies;
                    grid.Free = SubScription.Txt_Free;
                    grid.SubAmt = Convert.ToDecimal(SubScription.BE_SubAmt);
                    grid.TotalAmt = Convert.ToDecimal(SubScription.BE_TotalAmt);
                    grid.DispType = SubScription.DispatchName;
                    grid.Dis_ID = SubScription.GLookUp_DTypeList;
                    grid.DispAmt = Convert.ToDecimal(SubScription.BE_ExtraAmt);
                    grid.DisponCC = SubScription.Rad_DisOnCC;
                    grid.Pur_ID = SubScription.GLookUp_PurList;
                    grid.Narration = SubScription.Txt_Narration;
                    grid.Reference = SubScription.Txt_Reference;
                    grid.Tr_M_ID = xMID;
                    grid.Type = SubScription.Membership == "Renewal" ? "ADDITION" : "CREATION";
                    grid.UPDATED = false;
                    grid.REC_GENERATED = false;
                    grid.TR_CODE = SubScription.Membership == "Renewal" ? 19 : 18;
                    grid.IS_LIFE = SubScription.Subs_Min_Months == 0 ? true : false;
                    grid.CURR_YEAR_INCOME = Convert.ToDecimal(Curr_year_income);
                    grid.Txtissues = SubScription.Txt_Issues;
                    gridRows.Add(grid);
                }
                else if (SubScription.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {

                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == SubScription.Sr);

                    decimal Curr_year_income = 0;
                    int Total_Issue_Count = 0;
                    int Curr_year_Issue_Count = 0;
                    if (SubScription.Txt_Issues > 0)
                    {
                        Total_Issue_Count = SubScription.Txt_Issues;
                        Curr_year_Issue_Count = SubScription.CurrYearIssueCount;
                        Curr_year_income = 0;
                        if ((SubScription.Txt_Issues != 0))
                            Curr_year_income = CurrYearIssueCount == Convert.ToDecimal(SubScription.Txt_Issues) ? Convert.ToDecimal(SubScription.BE_TotalAmt) : (Convert.ToDecimal(SubScription.BE_TotalAmt) * Convert.ToDecimal(CurrYearIssueCount)) / Convert.ToDecimal(SubScription.Txt_Issues);
                    }

                    dataToEdit.SubsDate = SubScription.Txt_Subs_Date;
                    dataToEdit.SubsType = SubScription.SubscriptionType;
                    dataToEdit.Pur_ID = SubScription.BE_Purpose;
                    dataToEdit.Free = SubScription.Txt_Free;
                    dataToEdit.Copies = SubScription.Txt_Copies;
                    dataToEdit.FrDate = SubScription.Txt_Fr_Date;
                    dataToEdit.ToDate = SubScription.Txt_To_Date;
                    dataToEdit.SubAmt = Convert.ToDecimal(SubScription.BE_SubAmt);
                    dataToEdit.DispType = SubScription.DispatchName;
                    dataToEdit.DispAmt = Convert.ToDecimal(SubScription.BE_ExtraAmt);
                    dataToEdit.Txtissues = SubScription.Txt_Issues;
                    dataToEdit.DisponCC = SubScription.Rad_DisOnCC;
                    dataToEdit.TotalAmt = Convert.ToDecimal(SubScription.BE_TotalAmt);
                    dataToEdit.Narration = SubScription.Txt_Narration;
                    dataToEdit.Reference = SubScription.Txt_Reference;
                    dataToEdit.CURR_YEAR_INCOME = Curr_year_income;
                    dataToEdit.IS_LIFE = SubScription.Subs_Min_Months == 0 ? true : false;
                    dataToEdit.Tr_M_ID = SubScription.Tr_M_ID;
                    dataToEdit.UPDATED = true;
                    dataToEdit.Type = SubScription.Membership;
                }

                Session["Grid_Data_Subscription_Window_Data_Session"] = gridRows;
                return Json(new
                {
                    message = "Saved Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void CalculateFromDate(DateTime PrevSubsEndDate, DateTime? Txt_Subs_Date, string Membership, DateTime Default_Issue_Date, string Publish_On, int Subs_Min_Months)
        {
            DateTime BaseDate = Convert.ToDateTime(Txt_Subs_Date);
            if (Membership == "Renewal" & BaseDate <= PrevSubsEndDate)
                BaseDate = PrevSubsEndDate.AddDays(1);
            DateTime XDATE;
            if (Default_Issue_Date == DateTime.MinValue | Convert.ToDateTime(Txt_Subs_Date) <= PrevSubsEndDate)
            {
                XDATE = new DateTime(BaseDate.Year, BaseDate.Month, 1);
                if (Publish_On == "FORTNIGHTLY")
                    XDATE = new DateTime(BaseDate.Year, BaseDate.Month, BaseDate.Day > 15 ? 16 : 1);
                if (Publish_On == "WEEKLY")
                    XDATE = new DateTime(BaseDate.Year, BaseDate.Month, BaseDate.Day > 14 ? (BaseDate.Day > 21 ? 22 : 15) : (BaseDate.Day > 7 ? 8 : 1));
                Txt_Fr_Date = XDATE;
            }
            else
            {
                Txt_Fr_Date = Default_Issue_Date;
                XDATE = Default_Issue_Date;
            }
            if (Subs_Min_Months != 0)
            {
                Txt_To_Date = XDATE.AddMonths(Subs_Min_Months).AddDays(-1);
            }
        }
        public void Amount_Calculation(int Txt_Copies, DateTime Txt_Fr_Date, DateTime Txt_To_Date, string MagazineID, string Subs_PeriodWiseFeeCalc, int Subs_Min_Months, string Publish_On, double DT_Charges)
        {
            Txt_Issues = 0;
            CurrYearIssueCount = 0;

            if (Txt_Copies == 0)
            {
                BE_TotalAmt = 0;
                return;
            }
            BE_SubAmt = iCategory == 0 ? MSTF_Indian_Fee : MSTF_Foreign_Fee;
            BE_ExtraAmt = DT_Charges;
            decimal subRate = Convert.ToDecimal(BE_SubAmt);
            decimal perCopyRate = 0;
            if (Txt_To_Date != DateTime.MinValue)
            {
                Txt_Issues = BASE._Magazine_DBOps.GetCount_Issues(Txt_Fr_Date, Txt_To_Date, MagazineID);
                CurrYearIssueCount = BASE._Magazine_DBOps.GetCount_Issues(Txt_Fr_Date, Txt_To_Date, MagazineID, true);
            }
            if (Subs_PeriodWiseFeeCalc == "YES" & IsDate(Txt_Fr_Date.ToString()) & IsDate(Txt_To_Date.ToString()))
            {
                switch (Publish_On)
                {
                    case "WEEKLY":
                        {
                            perCopyRate = (subRate / (Subs_Min_Months * 4)); // sub per week
                            break;
                        }

                    case "FORTNIGHTLY":
                        {
                            perCopyRate = (subRate / (Subs_Min_Months * 2)); // sub per fortnight
                            break;
                        }

                    case "MONTHLY":
                        {
                            perCopyRate = (subRate / Subs_Min_Months); // sub per month
                            break;
                        }

                    case "BI-MONTHLY":
                        {
                            perCopyRate = (subRate * 2 / Subs_Min_Months); // sub per 2 months
                            break;
                        }

                    case "QUARTERLY":
                        {
                            perCopyRate = (subRate * 3 / Subs_Min_Months); // sub per Quater
                            break;
                        }

                    case "HALF-YEARLY":
                        {
                            perCopyRate = (subRate * 6 / Subs_Min_Months); // sub per 2 Quaters
                            break;
                        }

                    case "YEARLY":
                        {
                            perCopyRate = (subRate * 12 / Subs_Min_Months); // sub per 2 Quaters
                            break;
                        }
                }
                subRate = perCopyRate * Convert.ToDecimal(Txt_Issues);
                BE_SubAmt = Convert.ToDouble(subRate);
            }
            BE_TotalAmt = (Convert.ToDouble(subRate) * Convert.ToDouble(Txt_Issues) - Convert.ToDouble(Txt_Free)) + Convert.ToDouble(BE_ExtraAmt);
        }

        public ActionResult Amount_Calculation_Changed_Values(int Txt_Copies, DateTime Txt_Fr_Date, DateTime Txt_To_Date, string MagazineID, string Subs_PeriodWiseFeeCalc, int Subs_Min_Months, string Publish_On, double DT_Charges, double MSTF_Indian_Fee, double MSTF_Foreign_Fee)
        {
            Subscriptiondetails_Model model = new Subscriptiondetails_Model();
            ViewBag.MagazineID = MagazineID;
            model.Subs_PeriodWiseFeeCalc = Subs_PeriodWiseFeeCalc;
            model.Subs_Min_Months = Subs_Min_Months;
            model.DT_Charges = DT_Charges;
            Txt_Issues = 0;
            CurrYearIssueCount = 0;

            if (Txt_Copies == 0)
            {
                BE_TotalAmt = 0;
                model.Txt_Issues = 0;
                model.BE_TotalAmt = 0;
                return Json(new
                {
                    Message = model,
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            BE_SubAmt = iCategory == 0 ? MSTF_Indian_Fee : MSTF_Foreign_Fee;
            BE_ExtraAmt = DT_Charges;
            decimal subRate = Convert.ToDecimal(BE_SubAmt);
            decimal perCopyRate = 0;
            if (Txt_To_Date != DateTime.MinValue)
            {
                Txt_Issues = BASE._Magazine_DBOps.GetCount_Issues(Txt_Fr_Date, Txt_To_Date, MagazineID);
                CurrYearIssueCount = BASE._Magazine_DBOps.GetCount_Issues(Txt_Fr_Date, Txt_To_Date, MagazineID, true);
            }
            if (Subs_PeriodWiseFeeCalc == "YES" & IsDate(Txt_Fr_Date.ToString()) & IsDate(Txt_To_Date.ToString()))
            {
                switch (Publish_On)
                {
                    case "WEEKLY":
                        {
                            perCopyRate = (subRate / (Subs_Min_Months * 4)); // sub per week
                            break;
                        }

                    case "FORTNIGHTLY":
                        {
                            perCopyRate = (subRate / (Subs_Min_Months * 2)); // sub per fortnight
                            break;
                        }

                    case "MONTHLY":
                        {
                            perCopyRate = (subRate / Subs_Min_Months); // sub per month
                            break;
                        }

                    case "BI-MONTHLY":
                        {
                            perCopyRate = (subRate * 2 / Subs_Min_Months); // sub per 2 months
                            break;
                        }

                    case "QUARTERLY":
                        {
                            perCopyRate = (subRate * 3 / Subs_Min_Months); // sub per Quater
                            break;
                        }

                    case "HALF-YEARLY":
                        {
                            perCopyRate = (subRate * 6 / Subs_Min_Months); // sub per 2 Quaters
                            break;
                        }

                    case "YEARLY":
                        {
                            perCopyRate = (subRate * 12 / Subs_Min_Months); // sub per 2 Quaters
                            break;
                        }
                }
                subRate = perCopyRate * Convert.ToDecimal(Txt_Issues);
                BE_SubAmt = Convert.ToDouble(subRate);
            }
            BE_TotalAmt = (Convert.ToDouble(subRate) * Convert.ToDouble(Txt_Issues) - Convert.ToDouble(Txt_Free)) + Convert.ToDouble(BE_ExtraAmt);
            model.Txt_Issues = Txt_Issues;
            model.BE_TotalAmt = BE_TotalAmt;
            model.BE_ExtraAmt = BE_ExtraAmt;
            model.BE_SubAmt = BE_SubAmt;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GLookUp_Changed_Values(DateTime Txt_Subs_Date, int Txt_Copies, string MagazineID, string Publish_On, double DT_Charges, int Subs_Min_Months, int Type = 1)
        {
            Subscriptiondetails_Model model = new Subscriptiondetails_Model();
            ViewBag.MagazineID = MagazineID;
            //Type==1 Means Txt_Subs_Date changed event fire and 2 means Sub type Changed event fire 
            if (Type == 1)
            {
                CalculateFromDate(PrevSubsEndDate, Txt_Subs_Date, Membership, Default_Issue_Date, Publish_On, Subs_Min_Months);
            }
            DataTable dt = Session["GLookUp_STypeList"] as DataTable;
            List<DataRow> dr = (from row in dt.AsEnumerable()
                                where row.Field<string>("Type") == Publish_On
                                select row).ToList();
          //  DataRow[] dr = dt.Select("Type = " + Publish_On);
            var DefaultSubID_Id = "";
            if (dr.Count > 0)
            {
                DefaultSubID_Id = dr[0]["ID"].ToString();
                model.Subs_Short_Name = dr[0]["Short Name"].ToString();
                model.Subs_Start_Month = Convert.ToInt32(dr[0]["St_Month"]);
                model.Subs_Min_Months = Convert.ToInt32(dr[0]["Min.Months"]);
                model.Subs_Fixed_Period = dr[0]["Fixed Period"].ToString();
                model.Subs_PeriodWiseFeeCalc = dr[0]["Period wise Fee Calculation"].ToString();
            }

            if (!(Default_Issue_Date == DateTime.MinValue))
                Txt_Fr_Date = Default_Issue_Date;
            if (IsDate(Convert.ToDateTime(Txt_Subs_Date).ToString()))
            {
                if (Txt_Fr_Date == DateTime.MinValue)
                    CalculateFromDate(PrevSubsEndDate, Txt_Subs_Date, Membership, Default_Issue_Date, Publish_On, model.Subs_Min_Months);
                if (model.Subs_Min_Months == 0)
                {
                    if (model.Subs_Fixed_Period == "YES")
                    {
                        Txt_Fr_Date = new DateTime(DateTime.Now.Year, model.Subs_Start_Month, 1);
                        Txt_To_Date = DateTime.Now; ;
                        //this.Txt_Fr_Date.Enabled = false; this.Txt_To_Date.Enabled = false;
                    }
                    else
                    {
                        Txt_To_Date = DateTime.Now;
                    }
                }
                else
                {
                    if (model.Subs_Fixed_Period == "YES")
                    {
                        Txt_Fr_Date = new DateTime(DateTime.Now.Year, model.Subs_Start_Month, 1);
                        DateTime _FrDate = Txt_Fr_Date;
                        Txt_To_Date = _FrDate.AddMonths(model.Subs_Min_Months).AddDays(-1);

                    }
                    else
                    {
                        if (IsDate(Txt_Fr_Date.ToString()))
                        {
                            DateTime _FrDate = Txt_Fr_Date;
                            Txt_To_Date = _FrDate.AddMonths(model.Subs_Min_Months).AddDays(-1);
                        }
                    }
                }
                DateTime _SDate = Convert.ToDateTime(Txt_Subs_Date);
                DataTable dt_table = BASE._Magazine_DBOps.GetSubscriptionTypeFee(DefaultSubID_Id, _SDate);
                if (dt_table.Rows.Count > 0)
                {
                    MSTF_Indian_Fee = Convert.ToDouble(dt_table.Rows[0]["MSTF_INDIAN_FEE"]);
                    MSTF_Foreign_Fee = Convert.ToDouble(dt_table.Rows[0]["MSTF_FOREIGN_FEE"]);
                    DateTime xDate = default(DateTime);
                    if (!string.IsNullOrEmpty(dt_table.Rows[0]["MSTF_EFF_DATE"].ToString()))
                        xDate = Convert.ToDateTime(dt_table.Rows[0]["MSTF_EFF_DATE"]);
                    model.MSTF_Indian_Fee = MSTF_Indian_Fee;
                    model.MSTF_Foreign_Fee = MSTF_Foreign_Fee;
                }
            }
            Amount_Calculation(Txt_Copies, Txt_Fr_Date, Txt_To_Date, MagazineID, model.Subs_PeriodWiseFeeCalc, model.Subs_Min_Months, Publish_On, DT_Charges);
            model.BE_SubAmt = BE_SubAmt;
            model.BE_ExtraAmt = BE_ExtraAmt;
            model.BE_TotalAmt = BE_TotalAmt;
            model.Txt_Issues = Txt_Issues;
            model.Txt_Fr_Date = Txt_Fr_Date;
            model.Txt_To_Date = Txt_To_Date;
            model.DT_Charges = DT_Charges;

            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GLookUp_DTypeList_ChangeEvent(string Txt_Subs_Date, int Txt_Copies, string MagazineID, string Publish_On, int Subs_Min_Months, DateTime Txt_Fr_Date, DateTime Txt_To_Date, string DispatchType, string Subs_PeriodWiseFeeCalc)
        {
            Subscriptiondetails_Model model = new Subscriptiondetails_Model();
            ViewBag.MagazineID = MagazineID;

            DataTable dt_info = Session["Dispatchinfo"] as DataTable;
            DataRow[] dr = dt_info.Select("Name = " + DispatchType);
            var DispatchSubID_Id = "";
            if (dr.Length > 0)
            {
                DispatchSubID_Id = dr[0]["ID"].ToString();
            }
            if (IsDate(Convert.ToDateTime(Txt_Subs_Date).ToString()))
            {
                DateTime _SDate = Convert.ToDateTime(Txt_Subs_Date);
                DataTable Dispatch_info = BASE._Magazine_DBOps.GetDispatchTypeCharges(DispatchSubID_Id, _SDate);
                if (Dispatch_info.Rows.Count > 0)
                    DT_Charges = Convert.ToDouble(Dispatch_info.Rows[0]["MDTC_CHARGES"]) * Convert.ToDouble(Txt_Issues);
                else
                    DT_Charges = 0;
            }

            Amount_Calculation(Txt_Copies, Txt_Fr_Date, Txt_To_Date, MagazineID, Subs_PeriodWiseFeeCalc, Subs_Min_Months, Publish_On, DT_Charges);
            model.BE_SubAmt = BE_SubAmt;
            model.BE_ExtraAmt = BE_ExtraAmt;
            model.BE_TotalAmt = BE_TotalAmt;
            model.Txt_Issues = Txt_Issues;
            model.Txt_Fr_Date = Txt_Fr_Date;
            model.Txt_To_Date = Txt_To_Date;
            model.DT_Charges = DT_Charges;
            model.Subs_PeriodWiseFeeCalc = Subs_PeriodWiseFeeCalc;
            model.Subs_Min_Months = Subs_Min_Months;

            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_PaymentDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            var allData = (List<Payment_Window_Grid>)Session["Grid_Data_Payment_Window_Data_Session"];
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == Sr).FirstOrDefault() : new Payment_Window_Grid();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Session["Grid_Data_Property_Window_Data_Session"] = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_SubscriptionDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            var allData = (List<Subscription_Window_Grid>)Session["Grid_Data_Subscription_Window_Data_Session"];
            var dataToDelete = allData.FirstOrDefault(x => x.Sr == Sr);
            allData.Remove(dataToDelete);
            Session["Grid_Data_Subscription_Window_Data_Session"] = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_Voucher_Magazine_Membership_Payment(string ActionMethod, string SrID = null)
        {
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);
            PaymentDetails_Model model = new PaymentDetails_Model();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            if (ActionMethod == "_New")
            {
                var paymtdate = DateTime.Now;
                model.Txt_Pmt_Date = paymtdate;
                DataTable BA_Table = BASE._Payment_DBOps.GetBankAccounts();
                model.Cmd_Mode = "CASH";
                string Branch_IDs = "";
                foreach (DataRow xRow in BA_Table.Rows)
                    Branch_IDs = "'" + xRow["BA_BRANCH_ID"] + "'";
                if (Branch_IDs.Trim().Length > 0)
                    //Branch_IDs = Interaction.IIf(Branch_IDs.Trim().EndsWith(","), Microsoft.VisualBasic.Strings.Mid(Branch_IDs.Trim().ToString(), 1, Branch_IDs.Trim().Length - 1), Branch_IDs.Trim().ToString());
                    if (Branch_IDs.Trim().Length == 0)
                        Branch_IDs = "''";

                DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
                var BuildData = from B in BB_Table.AsEnumerable()
                                join A in BA_Table.AsEnumerable()
                            on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                                select new
                                {
                                    BANK_NAME = B.Field<String>("Name"),
                                    BI_SHORT_NAME = B.Field<String>("BI_SHORT_NAME"),
                                    BANK_BRANCH = B.Field<String>("Branch"),
                                    BANK_ACC_NO = A.Field<String>("BA_ACCOUNT_NO"),
                                    BA_ID = A.Field<String>("ID"),
                                    BA_EDIT_ON = A.Field<DateTime?>("REC_EDIT_ON"),
                                    BANK_ID = B.Field<String>("BANK_ID"),

                                };
                var Final_Data = BuildData.ToList();
                AcNo = Final_Data[0].BANK_ACC_NO;
                Branch = Final_Data[0].BANK_BRANCH;
                model.BE_Bank_Name = Final_Data[0].BANK_NAME;
                model.BE_Bank_Branch = Final_Data[0].BANK_BRANCH;
                model.BE_Bank_Acc_No = Final_Data[0].BANK_ACC_NO;
                model.BE_Bank_ID = Final_Data[0].BA_ID;
                DataTable d1 = BASE._Rect_DBOps.GetPurposes();
                DataView dview = new DataView(d1);
                if (d1 == null)
                {
                    return View();
                }
                else
                {
                    var gLookUp_PurList = new List<GLookUp_PurList>();
                    foreach (DataRow row in d1.Rows)
                    {
                        var addItem = new GLookUp_PurList();
                        addItem.PUR_NAME = row.Field<string>("PUR_NAME");
                        addItem.PUR_ID = row.Field<string>("PUR_ID");
                        gLookUp_PurList.Add(addItem);
                    }
                    var list = gLookUp_PurList.Where(f => f.PUR_ID == "8f6b3279-166a-4cd9-8497-ca9fc6283b25").Select(p => p.PUR_NAME).ToList();
                    Purpose = list[0];
                    model.BE_Purpose = list[0];
                }
                return PartialView(model);
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                model.ActionMethod = Tag;
                //model.TempActionMethod = Tag.ToString();

                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Payment_Window_Grid>)Session["Grid_Data_Payment_Window_Data_Session"];
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Payment_Window_Grid();
                model.Sr = Sr;
                model.Cmd_Mode = dataToEdit != null ? dataToEdit.Mode : "";
                model.Txt_Amount = Convert.ToDouble(dataToEdit != null ? dataToEdit.Amount : 0);
                model.Txt_Pmt_Date = dataToEdit != null ? dataToEdit.Pmt_Date : DateTime.Now;
                model.BE_Bank_ID = dataToEdit != null ? dataToEdit.Deposited_Bank_ID : "";
                model.BE_Bank_Name = dataToEdit != null ? dataToEdit.Deposited_Bank : "";
                //if (string.IsNullOrEmpty(dataToEdit!=null? dataToEdit.Deposited_Branch:"") || dataToEdit!=null? dataToEdit.Deposited_Branch:"" == null)
                //{
                //}
                model.BE_Bank_Branch = dataToEdit != null ? dataToEdit.Deposited_Branch : "";
                model.BE_Bank_Acc_No = dataToEdit != null ? dataToEdit.Deposited_Ac_No : "";
                model.Txt_Ref_No = dataToEdit != null ? dataToEdit.Ref_No : "";
                model.Txt_Ref_Date = dataToEdit.Ref_Date;
                model.Txt_Ref_CDate = dataToEdit.Ref_Clearing_Date;
                model.Txt_Ref_Branch = dataToEdit != null ? dataToEdit.Ref_Branch : "";
                model.RefBank_Name = dataToEdit != null ? dataToEdit.RefBank_Name : "";
                model.Ref_Bank_Name = dataToEdit != null ? dataToEdit.Ref_Bank_ID : "";
                model.Txt_Narration = dataToEdit != null ? dataToEdit.Narration : "";
                model.Txt_Reference = dataToEdit != null ? dataToEdit.Reference : "";
                model.BE_Purpose = Purpose;
            }
            return PartialView(model);
        }
        public ActionResult SubscriptionDetails(string Membership = null, DateTime? Txt_MS_Date = null, string BE_STATUS = null, string Lbl_ActiveSubsPeriod = null)
        {
            Subs_Detail = Session["Subs_Detail_Data"] as DataTable;
            Deleted_Vouchers = null;
            if (Subs_Detail != null && Subs_Detail.Rows.Count == 0 && Deleted_Vouchers.Rows.Count > 0)
            {
                return Json(new
                {
                    result = false,
                    message = "S o r r y !  P l e a s e   s a v e   C u r r e n t   V o u c h e r   F i r s t  . . . . !",
                }, JsonRequestBehavior.AllowGet);
            }
            if (BE_STATUS.ToLower().Contains("dis"))
            {
                return Json(new
                {
                    result = false,
                    message = "S o r r y !  N o   f u r t h e r   S u b s c r i p t i o n s   a l l o w e d  f o r   D i s c o n t i n u e d   M e m b e r  . . . . !",
                }, JsonRequestBehavior.AllowGet);
            }
            if (Lbl_ActiveSubsPeriod.ToLower().Contains("lifetime"))
            {
                return Json(new
                {
                    result = false,
                    message = "S o r r y !  N o   f u r t h e r   S u b s c r i p t i o n s   a l l o w e d  f o r   L i f e   M e m b e r  . . . . !",
                }, JsonRequestBehavior.AllowGet);
            }

            if (Subs_Detail != null && Subs_Detail.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Subs_Detail.Rows[Subs_Detail.Rows.Count - 1]["Tr_M_ID"])))
                    if (Convert.ToString(Subs_Detail.Rows[Subs_Detail.Rows.Count - 1]["Tr_M_ID"]) == xMID)
                        return Json(new
                        {
                            result = false,
                            message = "O n l y   O n e   S u b s c r i p t i o n   a l l o w e d   p e r   t r a n s a c t i o n . . . . !",
                        }, JsonRequestBehavior.AllowGet);
            }
            DateTime txttime = Convert.ToDateTime(Txt_MS_Date);
            if (IsDate(Convert.ToDateTime(Txt_MS_Date).ToString()) == false)
            {
                return Json(new
                {
                    result = false,
                    message = "D a t e   I n c o r r e c t   /   B l a n k . . . !"
                }, JsonRequestBehavior.AllowGet);
            }
            if (IsDate(Convert.ToDateTime(Txt_MS_Date).ToString()) == true && Membership == "New")
            {
                DateTime firstDate = BASE._open_Year_Sdt;
                DateTime secondDate = Convert.ToDateTime(Txt_MS_Date);
                TimeSpan diff = secondDate.Subtract(firstDate);
                TimeSpan diff1 = secondDate - firstDate;
                double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());

                if (diff2 < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }

                DateTime firstDate1 = BASE._open_Year_Edt;
                DateTime secondDate1 = Convert.ToDateTime(Txt_MS_Date);
                TimeSpan diff3 = secondDate1.Subtract(firstDate1);
                TimeSpan diff4 = secondDate1 - firstDate1;
                double diff5 = Convert.ToDouble((secondDate1 - firstDate1).TotalDays.ToString());

                //if (diff5 > 0)
                //{
                //    return Json(new
                //    {
                //        result = false,
                //        message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                //    }, JsonRequestBehavior.AllowGet);
                //}

            }
            return Json(new
            {
                result = true,
                message = "",
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Magazine_Membership_Payment(PaymentDetails_Model model)
        {
            model.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());

            if (model.Tag == Common_Lib.Common.Navigation_Mode._New | model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (model.Txt_Amount < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "A m o u n t   c a n n o t   b e   N e g a t i v e . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }

                if (IsDate(model.Txt_Pmt_Date.ToString()) == false)
                {
                    return Json(new
                    {
                        result = false,
                        message = "P a y m e n t    D a t e   I n c o r r e c t   /   B l a n k . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }


                if (IsDate(model.Txt_Pmt_Date.ToString()) == true)
                {
                    DateTime firstDate = BASE._open_Year_Sdt;
                    DateTime secondDate = model.Txt_Pmt_Date ?? DateTime.Now;
                    TimeSpan diff = secondDate.Subtract(firstDate);
                    TimeSpan diff1 = secondDate - firstDate;
                    double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());

                    if (diff2 < 0)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (VoucherType == "CREATION")
                {
                    //diff = DateDiff(DateInterval.Day, DateValue(this.Txt_Pmt_Date.DateTime), SubsStartDate);
                    DateTime firstDate = model.Txt_Pmt_Date ?? DateTime.Now;
                    DateTime secondDate = SubsStartDate;
                    TimeSpan diff = secondDate.Subtract(firstDate);
                    TimeSpan diff1 = secondDate - firstDate;
                    double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());
                    if (diff2 > 0)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "D a t e   c a n ' t   b e    s m a l l e r   t h a n   S u b s .  S t a r t   D a t e  (" + SubsStartDate.ToShortDateString() + "). . . !"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if ((model.Ref_Bank_Name == null || string.IsNullOrEmpty(model.Ref_Bank_Name)) && (model.Cmd_Mode == "CHEQUE" || model.Cmd_Mode == "DD" || model.Cmd_Mode == "CBS" || model.Cmd_Mode == "RTGS" || model.Cmd_Mode == "NEFT" || model.Cmd_Mode == "ECS"))
                {

                    return Json(new
                    {
                        result = false,
                        message = "B a n k   N o t   S e l e c t e d . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((model.Txt_Ref_Branch == null || string.IsNullOrEmpty(model.Txt_Ref_Branch)) && (model.Cmd_Mode == "CHEQUE" || model.Cmd_Mode == "DD" || model.Cmd_Mode == "CBS" || model.Cmd_Mode == "RTGS" || model.Cmd_Mode == "NEFT" || model.Cmd_Mode == "ECS"))
                {

                    return Json(new
                    {
                        result = false,
                        message = "B a n k   B r a n c h   N o t   S p e c i f i e d . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((model.Txt_Ref_No == null || string.IsNullOrEmpty(model.Txt_Ref_No)) && (model.Cmd_Mode == "CHEQUE" || model.Cmd_Mode == "DD" || model.Cmd_Mode == "CBS" || model.Cmd_Mode == "RTGS" || model.Cmd_Mode == "NEFT" || model.Cmd_Mode == "ECS"))
                {

                    return Json(new
                    {
                        result = false,
                        message = "N o .   N o t   S p e c i f i e d . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.Txt_Ref_Date.ToString()) == false && (model.Cmd_Mode == "CHEQUE" || model.Cmd_Mode == "DD" || model.Cmd_Mode == "CBS" || model.Cmd_Mode == "RTGS" || model.Cmd_Mode == "NEFT" || model.Cmd_Mode == "ECS"))
                {

                    return Json(new
                    {
                        result = false,
                        message = "D a t e   I n c o r r e c t   /   B l a n k . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.BE_Bank_Name == null && (model.Cmd_Mode == "CHEQUE" || model.Cmd_Mode == "DD" || model.Cmd_Mode == "CBS" || model.Cmd_Mode == "RTGS" || model.Cmd_Mode == "NEFT" || model.Cmd_Mode == "ECS" || model.Cmd_Mode == "BANK ACCOUNT"))
                {

                    return Json(new
                    {
                        result = false,
                        message = "B a n k   N o t   S e l e c t e d . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.BE_Purpose == null || string.IsNullOrEmpty(model.BE_Purpose))
                {

                    return Json(new
                    {
                        result = false,
                        message = "P u r p o s e   N o t   S e l e c t e d . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Cmd_Mode == "WRITTEN OFF" && model.Txt_Amount > Arrears_Before_Curr_txn)
                {

                    return Json(new
                    {
                        result = false,
                        message = "W r i t t e n   O f f   V a l u e     C a n n o t     B e     G r e a t e r     T h a n     P r e v i o u s   D u e(" + Arrears_Before_Curr_txn + "). . . . !" + "\n" + "Please use Discount Mode for any dues arising from current transaction.."
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            List<Payment_Window_Grid> gridRows = new List<Payment_Window_Grid>();
            if (Session["Grid_Data_Payment_Window_Data_Session"] != null)
            {
                gridRows = (List<Payment_Window_Grid>)Session["Grid_Data_Payment_Window_Data_Session"];
            }
            if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
            {


                Payment_Window_Grid grid = new Payment_Window_Grid();
                if ((gridRows.Count <= 0))
                {
                    grid.Sr = 1;
                }
                else
                {
                    if (string.IsNullOrEmpty(gridRows.FirstOrDefault().Sr.ToString()))
                    { grid.Sr = 1; }
                    else
                    { grid.Sr = gridRows.Count + 1; }
                }
                grid.Mode = model.Cmd_Mode;
                grid.Amount = Convert.ToDecimal(model.Txt_Amount);
                grid.Pmt_Date = model.Txt_Pmt_Date;
                grid.Deposited_Bank_ID = model.BE_Bank_ID;
                grid.Deposited_Bank = model.BE_Bank_Name;
                if (model.BE_Bank_Branch == null || string.IsNullOrEmpty(model.BE_Bank_Branch))
                { grid.Deposited_Branch = Branch; }
                else { grid.Deposited_Branch = model.BE_Bank_Branch; }
                if (model.BE_Bank_Acc_No == null || string.IsNullOrEmpty(model.BE_Bank_Acc_No))
                { grid.Deposited_Ac_No = AcNo; }
                else { grid.Deposited_Ac_No = model.BE_Bank_Acc_No; }
                grid.Ref_No = model.Txt_Ref_No;
                grid.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date);
                grid.Ref_Clearing_Date = Convert.ToDateTime(model.Txt_Ref_CDate);
                grid.Ref_Branch = model.Txt_Ref_Branch;
                grid.Ref_Bank_ID = model.Ref_Bank_Name;
                grid.RefBank_Name = model.RefBank_Name;
                grid.Narration = model.Txt_Narration;
                grid.Reference = model.Txt_Reference;
                gridRows.Add(grid);
            }
            else if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Mode = model.Cmd_Mode;
                dataToEdit.Amount = Convert.ToDecimal(model.Txt_Amount);
                dataToEdit.Pmt_Date = model.Txt_Pmt_Date;
                dataToEdit.Deposited_Bank_ID = model.BE_Bank_ID;
                dataToEdit.Deposited_Bank = model.BE_Bank_Name;
                if (model.BE_Bank_Branch == null || string.IsNullOrEmpty(model.BE_Bank_Branch))
                { dataToEdit.Deposited_Branch = Branch; }
                else { dataToEdit.Deposited_Branch = model.BE_Bank_Branch; }
                if (model.BE_Bank_Acc_No == null || string.IsNullOrEmpty(model.BE_Bank_Acc_No))
                { dataToEdit.Deposited_Ac_No = AcNo; }
                else { dataToEdit.Deposited_Ac_No = model.BE_Bank_Acc_No; }
                dataToEdit.Ref_No = model.Txt_Ref_No;
                dataToEdit.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date);
                dataToEdit.Ref_Clearing_Date = Convert.ToDateTime(model.Txt_Ref_CDate);
                dataToEdit.Ref_Branch = model.Txt_Ref_Branch;
                dataToEdit.Ref_Bank_ID = model.Ref_Bank_Name;
                dataToEdit.RefBank_Name = model.RefBank_Name;
                dataToEdit.Narration = model.Txt_Narration;
                dataToEdit.Reference = model.Txt_Reference;

            }
            Session["Grid_Data_Payment_Window_Data_Session"] = gridRows;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentGridData(string ActionMethodName, string ID = null)
        {
            DataTable DT = new DataTable();
            DataRow ROW;
            // ActionMethodName = string.IsNullOrEmpty(ActionMethodName) ? "_View" : ActionMethodName;
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethodName);
            DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT.Columns.Add("Mode", Type.GetType("System.String"));
            DT.Columns.Add("Amount", Type.GetType("System.Decimal"));
            DT.Columns.Add("Pmt_Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Dep_Bank_ID", Type.GetType("System.String"));
            DT.Columns.Add("Deposited Bank", Type.GetType("System.String"));
            DT.Columns.Add("Deposited Branch", Type.GetType("System.String"));
            DT.Columns.Add("Deposited A/c. No.", Type.GetType("System.String"));
            DT.Columns.Add("Ref No.", Type.GetType("System.String"));
            DT.Columns.Add("Ref Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Ref Clearing Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Ref Branch", Type.GetType("System.String"));
            DT.Columns.Add("Ref_Bank_ID", Type.GetType("System.String"));
            DT.Columns.Add("Ref Bank", Type.GetType("System.String"));
            DT.Columns.Add("Pur_ID", Type.GetType("System.String"));
            DT.Columns.Add("Narration", Type.GetType("System.String"));
            DT.Columns.Add("Reference", Type.GetType("System.String"));
            DT.Columns.Add("Tr_M_ID", Type.GetType("System.String"));
            DT.Columns.Add("UPDATED", Type.GetType("System.Boolean"));
            DT.Columns.Add("REC_GENERATED", Type.GetType("System.Boolean"));
            DT.Columns.Add("TR_CODE", Type.GetType("System.Int32"));
            DT.Columns.Add("Type", Type.GetType("System.String"));
            DT.Columns.Add("REC_ID", Type.GetType("System.String"));
            DT.Columns.Add("REC_ADD_ON", Type.GetType("System.String"));
            DT.Columns.Add("REC_ADD_BY", Type.GetType("System.String"));
            DT.Columns.Add("REC_EDIT_ON", Type.GetType("System.String"));
            DT.Columns.Add("REC_EDIT_BY", Type.GetType("System.String"));
            DT.Columns.Add("RECEIPT_NO", Type.GetType("System.String"));
            DT.Columns.Add("RECEIPT_ID", Type.GetType("System.String"));

            if (Tag == Common_Lib.Common.Navigation_Mode._New)
            {
                //ROW = DT.NewRow();
                //DT.Rows.Add(ROW);
            }


            if ((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || ((Tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                if (Session["Payment_Data"] != null)
                {
                    DT = Session["Payment_Data"] as DataTable;
                    //Txn_Date = Payment_Detail.Rows[0]["Pmt_Date"].ToString();
                }


            }
            List<Payment_Window_Grid> data = DatatableToModel.DataTabletoPayment_Window_Grid_INFO(DT);
            if (Session["Grid_Data_Payment_Window_Data_Session"] != null)
            {
                ViewBag.Grid_Data_Payment_Window_Data = Session["Grid_Data_Payment_Window_Data_Session"];
            }
            else
            {
                ViewBag.Grid_Data_Payment_Window_Data = data;

            }
            return PartialView();
        }

        public ActionResult SubscriptionGridData(string ActionMethodName, string ID = null)
        {
            DataTable DT = new DataTable();
            DataRow ROW;
            // ActionMethodName = string.IsNullOrEmpty(ActionMethodName) ? "_View" : ActionMethodName;
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethodName);
            DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT.Columns.Add("Subs. Type", Type.GetType("System.String"));
            DT.Columns.Add("Subs_ID", Type.GetType("System.String"));
            DT.Columns.Add("Subs. Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Fr. Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("To Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Copies", Type.GetType("System.Int32"));
            DT.Columns.Add("Free", Type.GetType("System.Int32"));
            DT.Columns.Add("SubAmt", Type.GetType("System.Decimal"));
            DT.Columns.Add("Total Amt.", Type.GetType("System.Decimal"));
            DT.Columns.Add("Disp. Type", Type.GetType("System.String"));
            DT.Columns.Add("Dis_ID", Type.GetType("System.String"));
            DT.Columns.Add("Disp.Amt", Type.GetType("System.Decimal"));
            DT.Columns.Add("Disp.on CC", Type.GetType("System.String"));
            DT.Columns.Add("Pur_ID", Type.GetType("System.String"));
            DT.Columns.Add("Narration", Type.GetType("System.String"));
            DT.Columns.Add("Reference", Type.GetType("System.String"));
            DT.Columns.Add("Tr_M_ID", Type.GetType("System.String"));
            DT.Columns.Add("Type", Type.GetType("System.String"));
            DT.Columns.Add("UPDATED", Type.GetType("System.Boolean"));
            DT.Columns.Add("REC_GENERATED", Type.GetType("System.Boolean"));
            DT.Columns.Add("TR_CODE", Type.GetType("System.Int32"));
            DT.Columns.Add("REC_ID", Type.GetType("System.String"));
            DT.Columns.Add("REC_ADD_ON", Type.GetType("System.DateTime"));
            DT.Columns.Add("REC_ADD_BY", Type.GetType("System.String"));
            DT.Columns.Add("REC_EDIT_ON", Type.GetType("System.DateTime"));
            DT.Columns.Add("REC_EDIT_BY", Type.GetType("System.String"));
            DT.Columns.Add("RECEIPT_NO", Type.GetType("System.String"));
            DT.Columns.Add("RECEIPT_ID", Type.GetType("System.String"));

            DT.Columns.Add("IS_LIFE", Type.GetType("System.Boolean"));
            DT.Columns.Add("CURR_YEAR_INCOME", Type.GetType("System.Decimal"));
            DT.Columns.Add("Txtissues", Type.GetType("System.Int32"));

            if (Tag == Common_Lib.Common.Navigation_Mode._New)
            {
                //ROW = DT.NewRow();
                //DT.Rows.Add(ROW);
            }


            if ((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || ((Tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                if (Session["Subs_Detail_Data"] != null)
                {
                    DT = Session["Subs_Detail_Data"] as DataTable;
                }
            }
            List<Subscription_Window_Grid> data = DatatableToModel.DataTabletoSubscription_Window_Grid_INFO(DT);
            if (Session["Grid_Data_Subscription_Window_Data_Session"] != null)
            {
                ViewBag.Grid_Data_Subscription_Window_Data = Session["Grid_Data_Subscription_Window_Data_Session"];
            }
            else
            {
                ViewBag.Grid_Data_Subscription_Window_Data = data;

            }

            return PartialView();
        }

        public ActionResult Frm_Magazine_Dispatch_Window()
        {
            return PartialView();
        }
        public ActionResult GLookUp_BankList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable BA_Table = BASE._Payment_DBOps.GetBankAccounts();
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
                Branch_IDs = "'" + xRow["BA_BRANCH_ID"] + "'";

            if (Branch_IDs.Trim().Length == 0)
                Branch_IDs = "''";

            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            var BuildData = from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
                        on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                            select new
                            {
                                BANK_NAME = B.Field<string>("Name"),
                                BI_SHORT_NAME = B.Field<string>("BI_SHORT_NAME"),
                                BANK_BRANCH = B.Field<string>("Branch"),
                                BANK_ACC_NO = A.Field<string>("BA_ACCOUNT_NO"),
                                BA_ID = A.Field<string>("ID").ToString(),
                                BA_EDIT_ON = A.Field<DateTime?>("REC_EDIT_ON"),
                                BANK_ID = B.Field<string>("BANK_ID"),

                            };
            var Final_Data = BuildData.ToList();
            var gLookUp_BankList = new List<GLookUp_BankList>();
            foreach (var item in Final_Data)
            {
                var addItem = new GLookUp_BankList();
                addItem.BI_BANK_NAME = item.BANK_NAME;
                addItem.BANK_BRANCH = item.BANK_BRANCH;
                addItem.BANK_ACC_NO = item.BANK_ACC_NO;
                addItem.BANK_ID = item.BANK_ID;
                addItem.ID = item.BA_ID;
                gLookUp_BankList.Add(addItem);
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(gLookUp_BankList, loadOptions)), "application/json");
        }

        public ActionResult GLookUp_RefBankList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
          var Bank = BASE._Payment_DBOps.GetBanks();
          //  var Listdata = DatatableToModel.DataTabletMagBank_INFO(Bank);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Bank, loadOptions)), "application/json");
        }
        public ActionResult GLookUp_PurList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Rect_DBOps.GetPurposes();
            DataView dview = new DataView(d1);
            if (d1 == null)
            {
                //Base.HandleDBError_OnNothingReturned();
                return View();
            }
            else
            {
                var gLookUp_PurList = new List<GLookUp_PurList>();
                foreach (DataRow row in d1.Rows)
                {
                    var addItem = new GLookUp_PurList();
                    addItem.PUR_NAME = row.Field<string>("PUR_NAME");
                    addItem.PUR_ID = row.Field<string>("PUR_ID");
                    gLookUp_PurList.Add(addItem);
                }
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(gLookUp_PurList, loadOptions)), "application/json");
            }

        }

        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
            string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
                string Pic_Status = string.Empty;
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;
                string Lbl_Status_Color = string.Empty;
                try
                {
                    Status = Action_Status;
                }
                catch (Exception ex)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Pic_Status = "Fa Fa-Lock";
                    Lbl_Status_Color = "blue";
                }
                else
                {
                    Pic_Status = "Fa Fa-UnLock";
                    Lbl_Status = Status;
                    if (Status.ToUpper().Trim().ToString() == "COMPLETED")
                        Lbl_Status_Color = "green";
                    else
                        Lbl_Status_Color = "red";
                }
                if (BASE.IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (BASE.IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Locked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Locked On: " + "?";
                    }
                    Lbl_StatusBy = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                else
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Unlocked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Unlocked On: " + "?";
                    }
                    Lbl_StatusBy = "Unlocked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Lbl_Status_Color,
                    Pic_Status = Pic_Status,
                    Lbl_StatusBy = Lbl_StatusBy,
                    Lbl_StatusOn = Lbl_StatusOn
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = "",
                    Pic_Status = "",
                    Lbl_StatusBy = "",
                    Lbl_StatusOn = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public ActionResult Get_MemberList(string Cmb_SearchType = "", string PC_MemberName = "", string BE_MagazineName = "", string Cmb_MemType = "", bool Use_Rec_ID = false, bool open_specific_Magazine = false)
        {

            Common_Lib.RealTimeService.Parameter_GetMembers_VoucherMagazine Param = new Common_Lib.RealTimeService.Parameter_GetMembers_VoucherMagazine();
            Param.Mem_Type = Cmb_MemType;
            Param.SearchString = PC_MemberName;
            Param.Use_Rec_ID = Use_Rec_ID.ToString();
            Param.SearchType = Cmb_SearchType;
            Param.Magazine_ID = open_specific_Magazine ? BE_MagazineName : null;// IIf(open_specific_Magazine, this.GLookUp_MagList.Tag, null/* TODO Change to default(_) if this is not a reference type */);
            Param.Prev_Year_ID = BASE._prev_Unaudited_YearID;
            DataTable d1 = BASE._Voucher_Magazine_DBOps.GetMembers(Param);
            var listData = DatatableToModel.DataTabletoPCMember_INFO(d1);
            Session["PC_MemeberData"] = listData;
            return PartialView("PCMemberGridData", listData);
        }

        public ActionResult PCMemberGridData()
        {
            if (Session["PC_MemeberData"] != null)
            {
                return View(Session["PC_MemeberData"]);
            }
            return null;
        }
        public ActionResult GLookUp_MagList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable Magazinelist = BASE._Magazine_DBOps.GetList("", "", "");
            var Listdata = DatatableToModel.DataTabletoMag_INFO(Magazinelist);
            Session["Magazine_Data"] = Listdata;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Listdata, loadOptions)), "application/json");
        }
        public ActionResult GetMagzineData(string Magzine_ID = "", string Txt_MS_ID = "", string country = "")
        {
            if (Session["Magazine_Data"] != null)
            {
                var Mag_Data = (List<GLoopUpMagaList>)Session["Magazine_Data"];
                var data = Mag_Data.FirstOrDefault(x => x.ID == Magzine_ID);
                var MS_No = BASE._Magazine_DBOps.GetNewMembershipNo(Magzine_ID);
                var Category = "";
                if (country == "India") { Category = "I"; }
                else
                { Category = "F"; }
                Txt_MS_ID = data.Mag_Short_Name + "/" + data.Language.Substring(0, 3) + "/" + Category + "/" + MS_No.ToString().PadLeft(7, '0');
            }
            return Json(new
            {
                result = true,
                message = "",
                Txt_MS_ID = Txt_MS_ID,

            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowOpenings(string Magzine_ID = "", string MS_RecId = "", string Txt_MS_Date = "", string membership = "", string country = "")
        {
            if (IsDate(Txt_MS_Date) == false)
            {
                return Json(new
                {
                    result = false,
                    message = "Date   Incorrect   /   Blank . . . !"
                }, JsonRequestBehavior.AllowGet);
            }
            if (IsDate(Txt_MS_Date) == true && membership == "New")
            {
                return Json(new
                {
                    result = false,
                    message = "Date   not   as   per   Financial   Year . . . !"
                }, JsonRequestBehavior.AllowGet);
            }
            DateTime firstDate1 = BASE._open_Year_Edt;
            DateTime secondDate2 = Convert.ToDateTime(Txt_MS_Date);
            TimeSpan diff6 = secondDate2.Subtract(firstDate1);
            TimeSpan diff7 = secondDate2 - firstDate1;
            double diff8 = Convert.ToDouble((secondDate2 - firstDate1).TotalDays.ToString());
            if (diff8 > 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Date   not   as   per   Financial   Year . . . !"
                }, JsonRequestBehavior.AllowGet);
            }
            if (MS_RecId == "" || MS_RecId == null)
            {
                MS_RecId = Guid.NewGuid().ToString();
                _RecId = MS_RecId;
            }
            if (Txt_MS_Date == "")
            {
                Txt_MS_Date = DateTime.Now.ToString();
            }
            var Param = new Common_Lib.RealTimeService.Parameter_GetVoucherDetails_OnMemberSelection();
            Param.MS_RecId = MS_RecId;
            Param.Txn_M_Id = xMID;
            Param.Mag_ID = Magzine_ID;
            Param.Subs_Start_Date = membership == "Renewal" ? DateTime.Today : Convert.ToDateTime(Txt_MS_Date);
            Param.Category = country == "India" ? "INDIAN" : "FOREIGN";
            Param.Prev_Year_ID = BASE._prev_Unaudited_YearID;
            DataSet VoucherDetails = BASE._Voucher_Magazine_DBOps.GetVoucherDetails_OnMemberSelection(Param);
            if (VoucherDetails.Tables.Count > 0)
            {
                Subs_Detail = VoucherDetails.Tables[1];
                Payment_Detail = VoucherDetails.Tables[2];
                Default_Subs_Detail = VoucherDetails.Tables[3];
                Default_Payment_Detail = VoucherDetails.Tables[4];
                Dispatch_detail = VoucherDetails.Tables[5];
                DataTable dtSub = AddSubcriptionData(Default_Subs_Detail, membership);
                DataTable dtPay = AddPaymentData(Default_Payment_Detail, membership);
                Session["Payment_Data"] = null;
                Session["Subs_Detail_Data"] = null;
                Session.Remove("Payment_Data");
                Session.Remove("Subs_Detail_Data");
                Session["Subs_Detail_Data"] = dtSub;
                Session["Payment_Data"] = dtPay;
                var subscriptionGridData = ConvertViewToString("SubscriptionGridData", dtSub);
                var paymentGridData = ConvertViewToString("PaymentGridData", dtPay);
                var Lbl_Opening = "";
                var BE_OpAdvDue = "";
                var Txt_Nxt_Issue_Date = "";
                var Last_dispatched_Issue_Date = "";
                var Arrears_Opening = "";
                var Advance_Opening = "";
                Session["Default_Subs_Detail"] = Default_Subs_Detail;
                DataTable d1 = VoucherDetails.Tables[0];
                if (d1.Rows.Count > 0)
                {
                    if (d1.Rows[0]["op. Balance Type"].ToString() == "DUE")
                    {
                        Arrears_Opening = d1.Rows[0]["op. Balance"].ToString();
                        Lbl_Opening = "Opening Due:";
                    }
                    else if (d1.Rows[0]["op. Balance Type"].ToString() == "ADVANCE")
                    {
                        Advance_Opening = d1.Rows[0]["op. Balance"].ToString(); // Payment made by user before current transaction
                        Lbl_Opening = "Opening Advance:";
                    }
                    BE_OpAdvDue = d1.Rows[0]["op. Balance"].ToString();
                    if (!string.IsNullOrEmpty(d1.Rows[0]["Next Issue Date"].ToString()))
                        Txt_Nxt_Issue_Date = d1.Rows[0]["Next Issue Date"].ToString();
                    if (!string.IsNullOrEmpty(d1.Rows[0]["Last dispatched Issue"].ToString()))
                        Last_dispatched_Issue_Date = d1.Rows[0]["Last dispatched Issue"].ToString();
                }

                double Lbl_SubsCurrTxn = 0;
                double Lbl_SubsDuringYear = 0;
                double Lbl_PmtCurrTxn = 0;
                double Lbl_OtherCurrTxn = 0;
                double Lbl_Other_Total = 0;
                double Lbl_CurrAdvDue = 0;
                double Lbl_Pmt_Total = 0;
                var Lbl_ActiveSubsPeriod = "--";
                var lblDispatchCount = "--";
                // double total_Amount = 0;
                var copies = 0;
                if (Subs_Detail.Rows.Count > 0)
                {
                    //Lbl_SubsCurrTxn = ; Lbl_SubsDuringYear = 0;
                    foreach (DataRow cRow in Subs_Detail.Rows)
                    {
                        if (!string.IsNullOrEmpty(cRow["Tr_M_ID"].ToString()))
                        {
                            Lbl_SubsCurrTxn = cRow["Tr_M_ID"].ToString() == xMID ? Convert.ToDouble(cRow["Total Amt."]) : 0;
                            Lbl_SubsDuringYear = Lbl_SubsDuringYear + Convert.ToDouble(cRow["Total Amt."]);
                        }
                        if (!string.IsNullOrEmpty(cRow["To Date"].ToString()))
                        {
                            if (Convert.ToDateTime(cRow["Fr. Date"]) <= DateTime.Today & Convert.ToDateTime(cRow["To Date"]) >= DateTime.Today)
                                copies = copies + Convert.ToInt32(cRow["copies"]);
                        }
                        else if (Convert.ToDateTime(cRow["Fr. Date"]) <= DateTime.Today)
                            copies = Convert.ToInt32(cRow["copies"]);
                    }
                    lblDispatchCount = copies.ToString();
                }
                else
                {
                    Lbl_ActiveSubsPeriod = "--";
                    lblDispatchCount = "--";
                    //Lbl_SubsCurrTxn = 0;  Lbl_SubsDuringYear = 0;
                }

                if (Payment_Detail.Rows.Count > 0)
                {
                    //Lbl_PmtCurrTxn = 0; Lbl_Pmt_Total = 0; Lbl_OtherCurrTxn = 0; Lbl_Other_Total = 0;
                    foreach (DataRow cRow in Payment_Detail.Rows)
                    {
                        double _amount = 0;
                        Lbl_PmtCurrTxn = cRow["Tr_M_ID"].ToString().Equals(xMID) & (cRow["Mode"].ToString() != "DISCOUNT" & cRow["Mode"].ToString() != "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                        _amount = (cRow["Mode"].ToString() != "DISCOUNT" & cRow["Mode"].ToString() != "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                        Lbl_Pmt_Total = Lbl_Pmt_Total + _amount;
                        Lbl_OtherCurrTxn = cRow["Tr_M_ID"].ToString() == xMID + (cRow["Mode"].ToString() == "DISCOUNT" | cRow["Mode"].ToString() == "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                        Lbl_Other_Total = (cRow["Mode"].ToString() == "DISCOUNT" | cRow["Mode"].ToString() == "WRITTEN OFF") ? Convert.ToDouble(cRow["Amount"]) : 0;
                    }
                    //Lbl_Pmt_Total= total_Amount.ToString();
                }
                else
                {
                    //Lbl_PmtCurrTxn = 0; Lbl_Pmt_Total = 0; Lbl_OtherCurrTxn = 0; Lbl_Other_Total = 0;
                }


                if (Subs_Detail.Rows.Count > 0)
                {
                    PrevSubsStartDate = default(DateTime); PrevSubsEndDate = default(DateTime);
                    foreach (DataRow cRow in Subs_Detail.Rows)
                    {
                        if (!string.IsNullOrEmpty(cRow["To Date"].ToString()))
                        {
                            if (PrevSubsEndDate < Convert.ToDateTime(cRow["To Date"]))
                                PrevSubsEndDate = Convert.ToDateTime(cRow["To Date"]);
                        }
                        if (!string.IsNullOrEmpty(cRow["Fr. Date"].ToString()))
                        {
                            if (PrevSubsStartDate > Convert.ToDateTime(cRow["Fr. Date"]) | PrevSubsStartDate == DateTime.MinValue)
                                PrevSubsStartDate = Convert.ToDateTime(cRow["Fr. Date"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(Subs_Detail.Rows[Subs_Detail.Rows.Count - 1]["To Date"].ToString()))
                        Lbl_ActiveSubsPeriod = PrevSubsStartDate.ToString("MMM") + "' " + PrevSubsStartDate.Year.ToString() + " to " + PrevSubsEndDate.ToString("MMM") + "' " + PrevSubsEndDate.Year.ToString();
                    else
                        Lbl_ActiveSubsPeriod = PrevSubsStartDate.ToString("MMM") + "' " + PrevSubsStartDate.Year.ToString() + " to Lifetime..." + " ";
                }
                else
                {
                    PrevSubsStartDate = default(DateTime); PrevSubsEndDate = default(DateTime); Lbl_ActiveSubsPeriod = "--";
                }
                return Json(new
                {
                    result = true,
                    message = "",
                    Lbl_Opening = Lbl_Opening,
                    Advance_Opening = Advance_Opening,
                    BE_OpAdvDue = BE_OpAdvDue,
                    Lbl_SubsCurrTxn = Lbl_SubsCurrTxn,
                    lblDispatchCount = lblDispatchCount,
                    Lbl_PmtCurrTxn = Lbl_PmtCurrTxn,
                    Lbl_Pmt_Total = Lbl_Pmt_Total,
                    Lbl_OtherCurrTxn = Lbl_OtherCurrTxn,
                    Lbl_Other_Total = Lbl_Other_Total,
                    Lbl_CurrAdvDue = Lbl_CurrAdvDue,
                    Lbl_ActiveSubsPeriod = Lbl_ActiveSubsPeriod,
                    Txt_Nxt_Issue_Date = Txt_Nxt_Issue_Date,
                    Lbl_SubsDuringYear = Lbl_SubsDuringYear,
                    paymentGridData = paymentGridData,
                    subscriptionGridData = subscriptionGridData
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var Arrears_Opening = "0";
                var Lbl_Opening = "Opening Due:";
                var Advance_Opening = "0";
                var BE_OpAdvDue = "0";
                //Lbl_SubsCurrTxn = "0";
                //Lbl_SubsDuringYear = "0";
                //Lbl_PmtCurrTxn = "0";
                //Lbl_Pmt_Total = "0";
                //Lbl_OtherCurrTxn = "0";
                //Lbl_Other_Total = "0";
                //Lbl_CurrAdvDue = "0";
                return Json(new
                {
                    result = true,
                    message = "",
                    Lbl_Opening = Lbl_Opening,
                    Advance_Opening = Advance_Opening,
                    BE_OpAdvDue = BE_OpAdvDue,

                }, JsonRequestBehavior.AllowGet);
            }

        }
        private DataTable AddSubcriptionData(DataTable Default_Sub, string membership)
        {
            DataRow ROW = Subs_Detail.NewRow();
            ROW["Sr."] = Default_Sub.Rows[0]["Sr."];
            ROW["Subs. Date"] = Default_Sub.Rows[0]["Subs. Date"];
            ROW["Subs. Type"] = Default_Sub.Rows[0]["Subs. Type"]; ;
            ROW["Subs_ID"] = Default_Sub.Rows[0]["Subs_ID"]; ;
            ROW["SubAmt"] = Default_Sub.Rows[0]["SubAmt"]; ;
            ROW["Fr. Date"] = Default_Sub.Rows[0]["Fr. Date"];
            ROW["To Date"] = Convert.ToDateTime(Default_Sub.Rows[0]["To Date"]) > DateTime.MinValue ? Default_Sub.Rows[0]["To Date"] : DBNull.Value;
            ROW["Copies"] = Default_Sub.Rows[0]["Copies"];
            ROW["Free"] = Default_Sub.Rows[0]["Free"];
            ROW["Disp. Type"] = Default_Sub.Rows[0]["Disp. Type"];
            ROW["Dis_ID"] = Default_Sub.Rows[0]["Dis_ID"];
            ROW["Disp.Amt"] = Default_Sub.Rows[0]["Disp.Amt"];
            ROW["Disp.on CC"] = Default_Sub.Rows[0]["Disp.on CC"];
            ROW["Total Amt."] = Default_Sub.Rows[0]["Total Amt."];
            ROW["Narration"] = Default_Sub.Rows[0]["Narration"];
            ROW["Reference"] = Default_Sub.Rows[0]["Reference"];
            ROW["Pur_ID"] = Default_Sub.Rows[0]["Pur_ID"];
            ROW["Tr_M_ID"] = Default_Sub.Rows[0]["Tr_M_ID"];
            ROW["Type"] = Default_Sub.Rows[0]["Type"];
            ROW["UPDATED"] = Convert.ToBoolean(Default_Sub.Rows[0]["UPDATED"]) ? true : false;
            ROW["REC_GENERATED"] = 0;
            ROW["TR_CODE"] = membership == "Renewal" ? 19 : 18;
            ROW["REC_GENERATED"] = 0;
            ROW["IS_LIFE"] = Default_Sub.Rows[0]["IS_LIFE"];
            ROW["CURR_YEAR_INCOME"] = Default_Sub.Rows[0]["CURR_YEAR_INCOME"];
            Subs_Detail.Rows.Add(ROW);
            return Subs_Detail;
        }
        private DataTable AddPaymentData(DataTable Default_Payment, string membership)
        {
            DataRow ROW = Payment_Detail.NewRow();
            ROW["Sr."] = Default_Payment.Rows[0]["Sr."];
            ROW["Mode"] = Default_Payment.Rows[0]["Mode"];
            ROW["Amount"] = Default_Payment.Rows[0]["Amount"];
            ROW["Pmt_Date"] = Default_Payment.Rows[0]["Pmt_Date"];
            ROW["Ref Bank"] = Default_Payment.Rows[0]["Ref Bank"];
            ROW["Ref Branch"] = Default_Payment.Rows[0]["Ref Branch"];
            ROW["Ref No."] = Default_Payment.Rows[0]["Ref No."];
            ROW["Ref Date"] = Default_Payment.Rows[0]["Ref Date"].ToString() != "" ? Default_Payment.Rows[0]["Ref Date"] : System.DBNull.Value;
            ROW["Ref Clearing Date"] = Default_Payment.Rows[0]["Ref Date"].ToString() != "" ? Default_Payment.Rows[0]["Ref Clearing Date"] : System.DBNull.Value;
            ROW["Ref_Bank_ID"] = Default_Payment.Rows[0]["Ref_Bank_ID"];
            if (((Default_Payment.Rows[0]["Mode"].ToString() != "CASH") && ((Default_Payment.Rows[0]["Mode"].ToString() != "DISCOUNT") && ((Default_Payment.Rows[0]["Mode"].ToString() != "WRITTEN OFF") && ((Default_Payment.Rows[0]["Mode"].ToString() != "MO") && (Default_Payment.Rows[0]["Mode"].ToString() != "EMO"))))))
            {
                ROW["Deposited Bank"] = Default_Payment.Rows[0]["Deposited Bank"];
                ROW["Deposited Branch"] = Default_Payment.Rows[0]["Deposited Branch"];
                ROW["Deposited A/c. No."] = Default_Payment.Rows[0]["Deposited A/c. No."];
                ROW["Dep_Bank_ID"] = Default_Payment.Rows[0]["Dep_Bank_ID"];
            }

            ROW["Tr_M_ID"] = xMID;
            ROW["UPDATED"] = Convert.ToBoolean(Default_Payment.Rows[0]["UPDATED"]) ? true : false;
            ROW["REC_GENERATED"] = 0;
            ROW["Type"] = Default_Payment.Rows[0]["Type"];
            ROW["TR_CODE"] = membership == "Renewal" ? 19 : 18;
            Payment_Detail.Rows.Add(ROW);
            return Payment_Detail;
        }
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        public ActionResult GLookUp_CC_Member_List(DataSourceLoadOptions loadOptions)
        {
            string Cmb_CC_MemType = "";
            if (MemberType == "")
            {
                Cmb_CC_MemType = "CENTRE / SUB-CENTRE";
            }
            else
            {
                Cmb_CC_MemType = MemberType;
            }

            DataTable CCMemberList = BASE._Magazine_DBOps.GetList_ConnectedMembership(Cmb_CC_MemType);
            var Listdata = DatatableToModel.DataTabletoCCMember_INFO(CCMemberList);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Listdata, loadOptions)), "application/json");
        }
        //public ActionResult GetCCMemberData(DataSourceLoadOptions loadOptions, string MemberType = "")
        //{
        //    //GLookUp_CC_Member_List(loadOptions, MemberType);
        //    CC_MemberType = MemberType;
        //    DataTable CCMemberList = BASE._Magazine_DBOps.GetList_ConnectedMembership(CC_MemberType);
        //    var Listdata = DatatableToModel.DataTabletoCCMember_INFO(CCMemberList);
        //    return null;
        //}
        public ActionResult Frm_Ledger_Info(string Txt_MS_ID = null, string PC_MemberName = null, string CC_Member_List = null, bool Chk_Sponsored = false)
        {
            string Txt_Title_Text = "";
            decimal Balancedue = 0.00M;
            if (Chk_Sponsored == true)
            {
                Txt_Title_Text = "Ledger for " + CC_Member_List;
            }
            else
            {
                Txt_Title_Text = "Ledger for " + PC_MemberName;
            }
            ViewBag.Txt_Title_Text = Txt_Title_Text;
            DataTable _db_Table = BASE._Voucher_Magazine_DBOps.GetPayeeLedger(Txt_MS_ID);
            List<LedgerList> BuildData = new List<LedgerList>();
            if (_db_Table.Rows.Count > 0)
            {
                Balancedue = Convert.ToDecimal(_db_Table.Rows[_db_Table.Rows.Count - 1]["Balance Due"].ToString());
            }
            else
            {
                Balancedue = 0.00M;
            }
            ViewBag.Balancedue = Balancedue;

            BuildData = (from DataRow T in _db_Table.AsEnumerable()
                         select new LedgerList
                         {
                             Date = string.IsNullOrEmpty(T["Date"].ToString()) ? "" : Convert.ToDateTime(T["Date"]).ToString("MM/dd/yyyy"),
                             Membership = T["Membership"].ToString(),
                             Opening = T["Opening"].ToString(),
                             Subscription = T["Subscription"].ToString(),
                             Payment = T["Payment"].ToString(),
                             BalanceDue = T["Balance Due"].ToString(),
                             MemberName = T["Member Name"].ToString(),
                             ID = T["ID"].ToString(),

                         }).ToList();
            var Final_Data = BuildData.ToList();
            Session["Ledger_ExportData"] = Final_Data;
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Final_Data);
        }
        public ActionResult Frm_Ledger_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_Data = Session["Ledger_ExportData"] as List<LedgerList>;
            return PartialView(Session["Ledger_ExportData"]);
        }

        public ActionResult Frm_Accnt_Info(string Txt_MS_ID = null, string PC_MemberName = null, string PC_MemberName_ID = null)
        {
            string Txt_Title_Text = "";
            decimal Balancedue = 0.00M;
            Txt_Title_Text = "Accounting Ledger for " + PC_MemberName;
            ViewBag.Txt_Title_Text = Txt_Title_Text;
            DataTable _db_Table = BASE._Voucher_Magazine_DBOps.GetMagazineAccLedger(PC_MemberName_ID);
            List<AccntList> BuildData = new List<AccntList>();
            if (_db_Table.Rows.Count > 0)
            {
                foreach (DataRow _row in _db_Table.Rows)
                {
                    Balancedue = Balancedue + Convert.ToDecimal(_row["dr"]);
                    Balancedue = Balancedue - Convert.ToDecimal(_row["cr"]);
                }
            }
            else
            {
                Balancedue = 0.00M;
            }
            ViewBag.Balancedue = Balancedue;

            BuildData = (from DataRow T in _db_Table.AsEnumerable()
                         select new AccntList
                         {
                             Name = T["Name"].ToString(),
                             Led_Id = T["LED_ID"].ToString(),
                             Led_Name = T["LED_NAME"].ToString(),
                             Date = string.IsNullOrEmpty(T["DATE"].ToString()) ? "" : Convert.ToDateTime(T["DATE"]).ToString("MM/dd/yyyy"),
                             Dr = T["DR"].ToString(),
                             Cr = T["CR"].ToString(),
                         }).ToList();
            var Final_Data = BuildData.ToList();
            Session["Accnt_ExportData"] = Final_Data;
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Final_Data);
        }
        public ActionResult Frm_Accnt_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_Data = Session["Accnt_ExportData"] as List<LedgerList>;
            return PartialView(Session["Accnt_ExportData"]);
        }

        public ActionResult Frm_Dispatch_Info(string Txt_MS_ID = null, string PC_MemberName = null, string PC_MemberName_ID = null)
        {
            //  LookUp_GetMagIssues(false, null, "d7b002cd-6637-492f-8aa3-15e272ba6dbb");
            string Txt_Title_Text = "";
            string lblNetBalance = "";
            string Membership_ID = null;
            string Membership_Old_ID = null;
            string Issue_Date = null;
            string Magazine = null;
            Txt_Title_Text = "Dispatch Details of " + PC_MemberName;
            ViewBag.Txt_Title_Text = Txt_Title_Text;

            DataSet MM_DS = BASE._Magazine_DBOps.GetList_MagazineDispatchRegister(Membership_ID, Membership_Old_ID, Issue_Date, Magazine, Txt_MS_ID);
            DataTable SummaryTable = MM_DS.Tables[5];

            foreach (DataRow cRow in MM_DS.Tables[3].Rows)
                lblNetBalance = lblNetBalance + " " + cRow["Magazine"].ToString() + ":" + cRow["Copies"].ToString() + " , ";
            if (lblNetBalance.Length > 0)
                lblNetBalance = lblNetBalance.Substring(0, lblNetBalance.Length - 3);
            ViewBag.lblNetBalance = lblNetBalance;
            DataRelation Dispatch_Detail = MM_DS.Relations.Add("Dispatch Detail", MM_DS.Tables[0].Columns["ISSUE_MEMBER"], MM_DS.Tables[1].Columns["ISSUE_MEMBER"], false);
            MM_DS.Tables[0].Columns["ISSUE_MEMBER"].ColumnMapping = MappingType.Hidden;
            MM_DS.Tables[1].Columns["ISSUE_MEMBER"].ColumnMapping = MappingType.Hidden;

            List<DispatchList> BuildData = new List<DispatchList>();
            List<DispatchList> BuildDataDetails = new List<DispatchList>();

            BuildData = (from DataRow T in MM_DS.Tables[0].AsEnumerable()
                         select new DispatchList
                         {
                             MemberID = T["Member ID"].ToString(),
                             MemberOldID = T["Member Old ID"].ToString(),
                             Member = T["Member"].ToString(),
                             Magazine = T["Magazine"].ToString(),
                             TotalCopies = T["Total Copies"].ToString(),
                             DispatchedCopies = T["Dispatched Copies"].ToString(),
                             Status = T["Status"].ToString(),
                             MemberStatus = T["Member Status"].ToString(),
                             MII_ISSUE_DATE = T["MII_ISSUE_DATE"].ToString(),
                             DISP_DONE_MODE = T["DISP_DONE_MODE"].ToString(),
                             MAG_REC_ID = T["MAG_REC_ID"].ToString(),
                             ExpiryStatus = T["Expiry Status"].ToString(),
                             REGION = T["REGION"].ToString(),
                             RMS = T["RMS"].ToString(),
                             PSO = T["PSO"].ToString(),
                             CONTACT_NO = T["CONTACT_NO"].ToString(),
                             CURRTIME = T["CURRTIME"].ToString(),
                             Issue = T["Issue"].ToString(),
                             ISSUE_ID = T["ISSUE_ID"].ToString(),
                             MEM_ID = T["MEM_ID"].ToString(),
                             ISSUE_MEMBER = T["ISSUE_MEMBER"].ToString(),
                         }).ToList();
            var Final_Data = BuildData.ToList();
            Session["Dispatch_ExportData"] = Final_Data;
            //Table1
            BuildDataDetails = (from DataRow T in MM_DS.Tables[1].AsEnumerable()
                                select new DispatchList
                                {
                                    ISSUE_MEMBER = T["ISSUE_MEMBER"].ToString(),
                                    Date = T["Date"].ToString(),
                                    DispatchMode = T["Dispatch Mode"].ToString(),
                                    DispatchStatus = T["Dispatch Status"].ToString(),
                                    Remarks = T["Remarks"].ToString(),
                                    Copies = T["Copies"].ToString(),
                                }).ToList();
            var Final_DataDetails = BuildDataDetails.ToList();
            Session["DispatchDetails_ExportData"] = Final_DataDetails;
            //Table1
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Final_Data);
        }
        public ActionResult Frm_Dispatch_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_Data = Session["Dispatch_ExportData"] as List<DispatchList>;
            return PartialView(Session["Dispatch_ExportData"]);
        }

        public ActionResult Frm_Dispatch_Info_Grid_Detail(string command, string ISSUE_MEMBER, int ShowHorizontalBar = 0)
        {
            ViewData["ISSUE_MEMBER"] = ISSUE_MEMBER;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_DataDetail = Session["DispatchDetails_ExportData"] as List<DispatchList>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ISSUE_MEMBER == ISSUE_MEMBER);
            var ret_list = new List<DispatchList> { sel_bal_info };
            return PartialView("Frm_Dispatch_Info_Grid_Detail", ret_list);
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(string ISSUE_MEMBER)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "detailGrid_" + ISSUE_MEMBER;
            settings.SettingsDetail.MasterGridName = "DispatchListGrid";
            settings.Width = Unit.Percentage(100);

            //settings.KeyFieldName = "pKey";
            settings.Columns.Add("ISSUE_MEMBER").Visible = false;
            settings.Columns.Add("Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("DispatchMode");
            settings.Columns.Add("DispatchStatus");
            settings.Columns.Add("Remarks");
            settings.Columns.Add("Copies");
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }

        public static IEnumerable GetDispatchdetails(string ISSUE_MEMBER)
        {
            var Final_DataDetail = System.Web.HttpContext.Current.Session["DispatchDetails_ExportData"] as List<DispatchList>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ISSUE_MEMBER == ISSUE_MEMBER);
            var ret_list = new List<DispatchList> { sel_bal_info };
            return ret_list;
        }
        //Dispatch functionality
        public ActionResult Frm_Related_Info(string Txt_MS_ID = null, string PC_MemberName = null, string MS_REC_ID = null)
        {
            //  LookUp_GetMagIssues(false, null, "d7b002cd-6637-492f-8aa3-15e272ba6dbb");
            string Txt_Title_Text = "";
            Txt_Title_Text = "Members Related to " + PC_MemberName;
            ViewBag.Txt_Title_Text = Txt_Title_Text;

            DataSet MM_Dataset = BASE._Magazine_DBOps.GetList_RelatedMembership("", "", "", "", MS_REC_ID);

            DataTable MM_Table = MM_Dataset.Tables[0];
            DataTable SummaryTable = MM_Dataset.Tables[2];
            DataSet JointDS = new DataSet(); JointDS.Tables.Add(MM_Table.Copy()); JointDS.Tables.Add(MM_Dataset.Tables[1].Copy());
            DataRelation MemberBalances = JointDS.Relations.Add("Subscription Detail", JointDS.Tables[0].Columns["ID"], JointDS.Tables[1].Columns["ID"], false);
            JointDS.Tables[1].Columns["ID"].ColumnMapping = MappingType.Hidden;

            List<RelatedList> BuildData = new List<RelatedList>();
            List<RelatedList> BuildDataDetails = new List<RelatedList>();

            BuildData = (from DataRow T in MM_Dataset.Tables[0].AsEnumerable()
                         select new RelatedList
                         {
                             ID = T["ID"].ToString(),
                             Tag = T["Tag"].ToString(),
                             Status = T["Status"].ToString(),
                             MembershipId = T["Membership ID"].ToString(),
                             MemberName = T["Member Name"].ToString(),
                             Magazine = T["Magazine"].ToString(),
                             COPIES = T["COPIES"].ToString(),
                             Period = T["Period"].ToString(),
                             MembershipOldId = T["Membership Old ID"].ToString(),
                             AddressLane1 = T["Address Line.1"].ToString(),
                             AddressLane2 = T["Address Line.2"].ToString(),
                             AddressLane3 = T["Address Line.3"].ToString(),
                             AddressLane4 = T["Address Line.4"].ToString(),
                             Pincode = T["Pincode"].ToString(),
                             City = T["City"].ToString(),
                             State = T["State"].ToString(),
                             District = T["District"].ToString(),
                             Country = T["Country"].ToString(),
                             TelNos = T["Tel.No(s)"].ToString(),
                             Membertype = T["Member Type"].ToString(),
                             Disponcc = T["Disp on CC"].ToString(),
                             StartDate = T["Start Date"].ToString(),
                             Due = T["Due"].ToString(),
                             Advance = T["Advance"].ToString(),
                             Category = T["Category"].ToString(),
                             EntryType = T["Entry Type"].ToString(),
                             MM_MI_ID = T["MM_MI_ID"].ToString(),
                             MM_MEMBER_ID = T["MM_MEMBER_ID"].ToString(),
                             MMB_CC_DISPATCH = T["MMB_CC_DISPATCH"].ToString(),
                             MM_CC_MS_ID = T["MM_CC_MS_ID"].ToString(),
                             MM_CC_SPONSORED = T["MM_CC_SPONSORED"].ToString(),
                             MM_MS_NO1 = T["MM_MS_NO1"].ToString(),
                         }).ToList();
            var Final_Data = BuildData.ToList();
            Session["Related_ExportData"] = Final_Data;
            //Table1
            BuildDataDetails = (from DataRow T in MM_Dataset.Tables[1].AsEnumerable()
                                select new RelatedList
                                {
                                    ID = T["ID"].ToString(),
                                    Subscription = T["Subscription"].ToString(),
                                    DateofSubs = T["Date of Subs"].ToString(),
                                    From = T["From"].ToString(),
                                    To = T["To"].ToString(),
                                    NoofCopies = T["No. of Copies"].ToString(),
                                    SubsAmt = T["Subs. Amt"].ToString(),
                                    DispAmt = T["Disp. Amt"].ToString(),
                                    TotalAmt = T["Total Amt"].ToString(),
                                    DispatchType = T["Dispatch Type"].ToString(),
                                    DispatchedOn = T["Dispatched on"].ToString(),
                                    LifeMember = T["Life Member"].ToString(),
                                }).ToList();
            var Final_DataDetails = BuildDataDetails.ToList();
            Session["RelatedDetails_ExportData"] = Final_DataDetails;
            //Table1
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Final_Data);
        }

        public ActionResult Frm_Related_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_Data = Session["Related_ExportData"] as List<RelatedList>;
            return PartialView(Session["Related_ExportData"]);
        }

        public ActionResult Frm_Related_Info_Grid_Details(string command, string ID, int ShowHorizontalBar = 0)
        {
            ViewData["ID"] = ID;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_DataDetail = Session["RelatedDetails_ExportData"] as List<RelatedList>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ID == ID);
            var ret_list = new List<RelatedList> { sel_bal_info };
            return PartialView("Frm_Related_Info_Grid_Details", ret_list);
        }

        public static GridViewSettings CreateGeneralDetailGridSettings_Related(string ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "detailGrid_" + ID;
            settings.SettingsDetail.MasterGridName = "RelatedListGrid";
            settings.Width = Unit.Percentage(100);

            //settings.KeyFieldName = "pKey";
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("Subscription");
            settings.Columns.Add("Date of Jobs");
            settings.Columns.Add("From");
            settings.Columns.Add("To");
            settings.Columns.Add("No. of Copies");
            settings.Columns.Add("Subs. Amt");
            settings.Columns.Add("Disp. Amt");
            settings.Columns.Add("Total Amt");
            settings.Columns.Add("Dispatch Type");
            settings.Columns.Add("Dispatched On");
            settings.Columns.Add("Life Member");
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }

        public static IEnumerable GetRelateddetails(string ID)
        {
            var Final_DataDetail = System.Web.HttpContext.Current.Session["RelatedDetails_ExportData"] as List<RelatedList>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ID == ID);
            var ret_list = new List<RelatedList> { sel_bal_info };
            return ret_list;
        }

        public ActionResult RelatedDataNavigation(string Type = null, string ID=null)
        {
            int Ctr = 0;
            try
            {
                if (BASE._Magazine_DBOps.Delete_Membership(ID))
                {
                    Ctr += 1;
                    return Json(new { Message = Ctr.ToString() + " Record(s) deleted Successfully", result = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = "Record not deleted ", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
           catch(Exception ex)
            {
                return Json(new { Message = ex, result = false }, JsonRequestBehavior.AllowGet);
            }
          
        }

        public ActionResult DataNavigation(string Type = null, int DispatchedCopies = 0, string ISSUE_ID = null, string MEM_ID = null, string MII_ISSUE_DATE = null, string Member = null, string MemberID = null, int TotalCopies = 0, string MAG_REC_ID = null)
        {
            string ErrMsg = "";
            if (Type == "NOT DELIVERED")
            {
                if (DispatchedCopies > 0)
                {
                    Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch inparam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch();
                    inparam.Dispatch_ID = null;
                    DateTime dDate = DateTime.Now;
                    inparam.DispatchDate = dDate.ToString(BASE._Server_Date_Format_Short);
                    inparam.Issue_ID = ISSUE_ID;
                    inparam.Membership_ID = MEM_ID;
                    inparam.Status = "NOT DELIVERED";
                    inparam.Copies = DispatchedCopies;
                    inparam.Remarks = "";
                    inparam.Tr_ID = System.Guid.NewGuid().ToString();
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Dispatch(inparam))
                    {
                        return Json(new { Message = Common_Lib.Messages.SomeError, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string _ISSUE_DATE = string.IsNullOrEmpty(MII_ISSUE_DATE.ToString()) ? "" : Convert.ToDateTime(MII_ISSUE_DATE).ToString(BASE._Date_Format_DD_MMM_YYYY);
                    if (ErrMsg.Length == 0)
                    {
                        ErrMsg = "Sorry !! Copies not dispatched for " + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    else
                    {
                        ErrMsg += ", " + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    if (ErrMsg.Length > 0)
                    {
                        return Json(new { Message = ErrMsg, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (Type == "NOT DELIVERED")
            {
                if (DispatchedCopies > 0)
                {
                    Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch inparam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch();
                    inparam.Dispatch_ID = null;
                    DateTime dDate = DateTime.Now;
                    inparam.DispatchDate = dDate.ToString(BASE._Server_Date_Format_Short);
                    inparam.Issue_ID = ISSUE_ID;
                    inparam.Membership_ID = MEM_ID;
                    inparam.Status = "DELIVERY RETURNED";
                    inparam.Copies = DispatchedCopies;
                    inparam.Remarks = "";
                    inparam.Tr_ID = System.Guid.NewGuid().ToString();
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Dispatch(inparam))
                    {
                        return Json(new { Message = Common_Lib.Messages.SomeError, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string _ISSUE_DATE = string.IsNullOrEmpty(MII_ISSUE_DATE.ToString()) ? "" : Convert.ToDateTime(MII_ISSUE_DATE).ToString(BASE._Date_Format_DD_MMM_YYYY);
                    if (ErrMsg.Length == 0)
                    {
                        ErrMsg = "Sorry !! Copies not dispatched for " + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    else
                    {
                        ErrMsg += ", " + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    if (ErrMsg.Length > 0)
                    {
                        return Json(new { Message = ErrMsg, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (Type == "BY HAND")
            {
                int Copies = 0;
                Copies = TotalCopies - DispatchedCopies;
                if (Copies > 0)
                {

                    Common_Lib.RealTimeService.Parameter_Insert_dispatch_New_Voucher inDispatch = new Common_Lib.RealTimeService.Parameter_Insert_dispatch_New_Voucher();
                    inDispatch.FromDate = string.IsNullOrEmpty(MII_ISSUE_DATE.ToString()) ? DateTime.Now : Convert.ToDateTime(MII_ISSUE_DATE);
                    inDispatch.ToDate = string.IsNullOrEmpty(MII_ISSUE_DATE.ToString()) ? DateTime.Now : Convert.ToDateTime(MII_ISSUE_DATE);
                    inDispatch.MagID = MAG_REC_ID;
                    inDispatch.MembershipID = MEM_ID;
                    inDispatch.subDate = DateTime.Now;
                    inDispatch.subsCopies = Copies;
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Dispatch_New_Voucher(inDispatch))
                    {
                        return Json(new { Message = Common_Lib.Messages.SomeError, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string _ISSUE_DATE = string.IsNullOrEmpty(MII_ISSUE_DATE.ToString()) ? "" : Convert.ToDateTime(MII_ISSUE_DATE).ToString(BASE._Date_Format_DD_MMM_YYYY);
                    if (ErrMsg.Length == 0)
                    {
                        ErrMsg = "Sorry !! Copies already dispatched for" + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    else
                    {
                        ErrMsg += ", " + Member + " (" + MemberID + ") for Issue dated " + _ISSUE_DATE;
                    }
                    if (ErrMsg.Length > 0)
                    {
                        return Json(new { Message = ErrMsg, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetMagIssues(bool? IsVisible, DataSourceLoadOptions loadOptions, string CC_MemType = "")
        {
            DataTable Magazinelist = BASE._Magazine_DBOps.GetMagazinesIssues(CC_MemType) as DataTable;
            var Listdata = DatatableToModel.LookUp_GetMagIssues(Magazinelist);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Listdata, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetSubscriptionTypeList(bool? IsVisible, DataSourceLoadOptions loadOptions, string MagazineID = "")
        {
            DataTable MagazineTypelist = BASE._Magazine_DBOps.GetList_SubscriptionTypeList("", "", " and ST.MST_MI_ID = '" + MagazineID + "' ") as DataTable;
            var ListTypedata = DatatableToModel.GLookUp_STypeList(MagazineTypelist);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ListTypedata, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetDispatchTypeList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable Dispatchinfo = BASE._Magazine_DBOps.GetList_DispatchTypeList("", "", "");
            var ListDispatchdata = DatatableToModel.LookUp_GetDispatchList(Dispatchinfo);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ListDispatchdata, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetPurposeList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable ListPurposedatainfo = BASE._Gift_DBOps.GetProjects("PUR_NAME", "PUR_ID");
            var ListPurposedata = DatatableToModel.LookUp_GetPurposeList(ListPurposedatainfo);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ListPurposedata, loadOptions)), "application/json");
        }

        public ActionResult Btn_AutoRenewal(string MM_AUTO_RENEWAL = "", string MS_RecId = "")
        {
            if (MM_AUTO_RENEWAL == "Enabled")
            {
                if (MS_RecId == "" || MS_RecId == null)
                {
                    MS_RecId = _RecId;
                }
                BASE._Magazine_DBOps.ConsiderForAutoRenewal(false, MS_RecId);
                var mm_auto_renewel = "Disabled";
                var mm_auto_tooltip = "Auto Renewal Disabled";
                return Json(new
                {
                    result = true,
                    message = "",
                    mm_auto_renewel = mm_auto_renewel,
                    mm_auto_tooltip = mm_auto_tooltip
                }, JsonRequestBehavior.AllowGet);

                //DevExpress.XtraEditors.XtraMessageBox.Show("Membership shall not be considered for Auto-Renewal . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MS_RecId == "" || MS_RecId == null)
                {
                    MS_RecId = _RecId;
                }
                BASE._Magazine_DBOps.ConsiderForAutoRenewal(true, MS_RecId);
                var mm_auto_renewel = "Enabled";
                var mm_auto_tooltip = "Auto Renewal Disabled";
                return Json(new
                {
                    result = true,
                    message = "",
                    mm_auto_renewel = mm_auto_renewel,
                    mm_auto_tooltip = mm_auto_tooltip
                }, JsonRequestBehavior.AllowGet);
                //DevExpress.XtraEditors.XtraMessageBox.Show("Membership shall be considered for Auto-Renewal . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public ActionResult Btn_Discontinue()
        {
            return null;
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
        #region export
        public ActionResult Frm_Ledger_Export_Options()
        {
            return PartialView();
        }
        public ActionResult Frm_Accnt_Export_Options()
        {
            return PartialView();
        }
        #endregion
        public JsonResult DataMaintainForDDL(string value = "")
        {
            MemberType = value;
            return Json(new { Message = "updated", result = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult toClearSession()
        {
            Session["Grid_Data_Payment_Window_Data_Session"] = null;
            Session["Grid_Data_Subscription_Window_Data_Session"] = null;

            return Json(new { Message = "Cleared", result = true }, JsonRequestBehavior.AllowGet);
        }
        public void Magazine_user_rights()
        {
            //ViewData["MagazineRR_AddRight"] = CheckRights(BASE, ClientScreen.Magazine_Receipt_Register, "Add");
            //ViewData["MagazineRR_UpdateRight"] = CheckRights(BASE, ClientScreen.Magazine_Receipt_Register, "Update");
            //ViewData["MagazineRR_ViewRight"] = CheckRights(BASE, ClientScreen.Magazine_Receipt_Register, "View");
            //ViewData["MagazineRR_DeleteRight"] = CheckRights(BASE, ClientScreen.Magazine_Receipt_Register, "Delete");
            //ViewData["MagazineRR_ExportRight"] = CheckRights(BASE, ClientScreen.Magazine_Receipt_Register, "Export");
            //ViewData["MagazineRR_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            //ViewData["MagazineRR_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Magazine_Receipt_Register, Common.ClientAction.Lock_Unlock);

            //ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            //ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            //ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            //ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["MagazineRR_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
        }
        private bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = System.Web.HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "android","midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
    public static class DataTableHelper1
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}