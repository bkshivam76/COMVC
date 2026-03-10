using DevExpress.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class LiveStockController : BaseController
    {
        #region "Start--> Default Variables"        
        public DateTime LastEditedOn
        {
            get
            {
                return (DateTime)GetBaseSession("LastEditedOn_LiveStock");
            }
            set
            {
                SetBaseSession("LastEditedOn_LiveStock", value);
            }
        }
        public List<LiveStockInfo> LiveStock_ExportData
        {
            get
            {
                return (List<LiveStockInfo>)GetBaseSession("LiveStock_ExportData_LiveStock");
            }
            set
            {
                SetBaseSession("LiveStock_ExportData_LiveStock", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> LiveStockInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("LiveStockInfo_DetailGrid_Data_LiveStock");
            }
            set
            {
                SetBaseSession("LiveStockInfo_DetailGrid_Data_LiveStock", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> LiveStockInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("LiveStockInfo_AdditionalInfoGrid_LiveStock");
            }
            set
            {
                SetBaseSession("LiveStockInfo_AdditionalInfoGrid_LiveStock", value);
            }
        }
        #endregion
        public void SetDefaultValues()
        {
            LastEditedOn = default(DateTime);
        }
        #region Page Constructor
        public LiveStockController()
        {
            //BASE = new Common_Lib.Common();
            //Helper.CommonFunctions.Programming_Mode(BASE);
            //BASE.AllowMultiuser = true;
        }
        #endregion 

        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/LiveStock)
        public ActionResult Frm_LiveStock_Info()
        {
            SetDefaultValues();
            LiveStock_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_LiveStock').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_LiveStock).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing LSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            LSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK;
            LSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            LSProfile.Next_YearID = BASE._next_Unaudited_YearID;
            LSProfile.TableName = Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO;
            DataTable LS_Table = BASE._LiveStockDBOps.GetProfileListing(LSProfile);
            // BASE._LiveStockDBOps.GetList(Voucher_Entry, Profile_Entry)
            if ((LS_Table == null))
            {
                return PartialView("Frm_LiveStock_Info", null);
            }

            //bool Remarks;
            //LS_Table.Columns.Add("Remarks", Remarks.GetType);
            //foreach (XRow in LS_Table.Rows)
            //{
            //    XRow("Remarks") = ((XRow("RemarkCount") > 0) ? true : false);
            //}
            DateTime? dtnull = null;//Redmine Bug #133138 fixed

            //BUILD DATA
            List<LiveStockInfo> BuildData = new List<LiveStockInfo>();
            foreach (DataRow row in LS_Table.Rows)
            {
                LiveStockInfo newdata = new LiveStockInfo();

                newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                newdata.LS_ITEM_ID = row["LS_ITEM_ID"].ToString();
                newdata.LivestockName = row["Livestock Name"].ToString();
                newdata.BirthYear = row["Birth Year"].ToString();
                newdata.Insurance = row["Insurance"].ToString();
                newdata.INSURANCE_ID = row["INSURANCE_ID"].ToString();
                newdata.INSURANCE_COMPANY = row["Insurance Company"].ToString();
                newdata.LS_INS_POLICY_NO = row["LS_INS_POLICY_NO"].ToString();
                newdata.LS_INS_DATE = Convert.IsDBNull(row["LS_INS_DATE"]) ? dtnull : Convert.ToDateTime(row["LS_INS_DATE"]);//Redmine Bug #133138 fixed
                newdata.LS_INS_AMT = Convert.ToDouble(row["LS_INS_AMT"]);
                newdata.LS_AMT = Convert.ToDouble(row["LS_AMT"]);
                newdata.CurrValue = Convert.ToDouble(row["Curr Value"]);
                newdata.AL_LOC_NAME = row["AL_LOC_NAME"].ToString();
                newdata.LS_LOC_AL_ID = row["LS_LOC_AL_ID"].ToString();
                newdata.LS_OTHER_DETAIL = row["LS_OTHER_DETAIL"].ToString();
                newdata.SaleStatus = row["Sale Status"].ToString();

                newdata.YearID = row["YearID"].ToString();
                newdata.TR_ID = row["TR_ID"].ToString();
                newdata.EntryType = row["Entry Type"].ToString();
                newdata.RemarkCount = (Int32)row["RemarkCount"];
                newdata.RemarkStatus = row["RemarkStatus"].ToString();
                newdata.OpenActions = (Int32)row["OpenActions"];
                newdata.CrossedTimeLimit = (Int32)row["CrossedTimeLimit"];
                newdata.AddBy = row["Add By"].ToString();
                newdata.AddDate = Convert.ToDateTime(row["Add Date"].ToString());
                newdata.EditBy = row["Edit By"].ToString();
                newdata.EditDate = Convert.ToDateTime(row["Edit Date"].ToString());
                newdata.ActionStatus = row["Action Status"].ToString();
                newdata.ActionBy = row["Action By"].ToString();
                newdata.ActionDate = (DateTime)row["Action Date"];
                newdata.ID = row["ID"].ToString();
                newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);//redmine bug 133067 fixed
                newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);//redmine bug 133067 fixed
                newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);//redmine bug 133067 fixed
                newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);//redmine bug 133067 fixed
                newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);

                newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();

                newdata.iIcon = "";

                if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newdata.iIcon += "GreenShield|";
                }
                else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newdata.iIcon += "YellowShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newdata.iIcon += "BlueShield|";
                }
                if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedFlag|";
                }
                if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newdata.iIcon += "RequiredAttachment|";
                }
                else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newdata.iIcon += "AdditionalAttachment|";
                }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                BuildData.Add(newdata);
            }
            //var BuildData = from T in LS_Table.AsEnumerable()
            //                select new LiveStockInfo
            //{
            //ITEM_NAME = T["ITEM_NAME"].ToString(),
            //    LS_ITEM_ID = T["LS_ITEM_ID"].ToString(),
            //    LivestockName = T["Livestock Name"].ToString(),
            //    BirthYear = T["Birth Year"].ToString(),
            //    Insurance = T["Insurance"].ToString(),
            //    INSURANCE_ID = T["INSURANCE_ID"].ToString(),
            //    INSURANCE_COMPANY = T["Insurance Company"].ToString(),
            //    LS_INS_POLICY_NO = T["LS_INS_POLICY_NO"].ToString(),
            //    LS_INS_DATE = Convert.IsDBNull(T["LS_INS_DATE"])?dtnull:Convert.ToDateTime(T["LS_INS_DATE"]),//Redmine Bug #133138 fixed
            //    LS_INS_AMT = Convert.ToDouble(T["LS_INS_AMT"]),
            //    LS_AMT = Convert.ToDouble(T["LS_AMT"]),
            //    CurrValue = Convert.ToDouble(T["Curr Value"]),
            //    AL_LOC_NAME = T["AL_LOC_NAME"].ToString(),
            //    LS_LOC_AL_ID = T["LS_LOC_AL_ID"].ToString(),
            //    LS_OTHER_DETAIL = T["LS_OTHER_DETAIL"].ToString(),
            //    SaleStatus = T["Sale Status"].ToString(),

            //    YearID = T["YearID"].ToString(),
            //    TR_ID = T["TR_ID"].ToString(),
            //    EntryType = T["Entry Type"].ToString(),
            //    RemarkCount = (Int32)T["RemarkCount"],
            //    RemarkStatus = T["RemarkStatus"].ToString(),
            //    OpenActions = (Int32)T["OpenActions"],
            //    CrossedTimeLimit = (Int32)T["CrossedTimeLimit"],
            //    AddBy = T["Add By"].ToString(),
            //    AddDate = Convert.ToDateTime(T["Add Date"].ToString()),
            //    EditBy = T["Edit By"].ToString(),
            //    EditDate = Convert.ToDateTime(T["Edit Date"].ToString()),
            //    ActionStatus = T["Action Status"].ToString(),
            //    ActionBy = T["Action By"].ToString(),
            //    ActionDate = (DateTime)T["Action Date"],
            //    ID = T["ID"].ToString(),                            
            //    REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),//redmine bug 133067 fixed
            //    COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),//redmine bug 133067 fixed
            //    RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),//redmine bug 133067 fixed
            //    REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),//redmine bug 133067 fixed
            //    OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
            //    ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),

            //};
            var Final_Data = BuildData.ToList();
            LiveStock_ExportData = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["LiveStock_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Final_Data);
        }
        public PartialViewResult Frm_LiveStock_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            LiveStock_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (LiveStock_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetProfileListing LSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
                LSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK;
                LSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                LSProfile.Next_YearID = BASE._next_Unaudited_YearID;
                LSProfile.TableName = Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO;
                DataTable LS_Table = BASE._LiveStockDBOps.GetProfileListing(LSProfile);
                //BASE._LiveStockDBOps.GetList(Voucher_Entry, Profile_Entry)
                if ((LS_Table == null))
                {
                    return PartialView("Frm_LiveStock_Info", null);
                }
                DateTime? dtnull = null;//Redmine Bug #133138 fixed

                List<LiveStockInfo> BuildData = new List<LiveStockInfo>();
                foreach (DataRow row in LS_Table.Rows)
                {
                    LiveStockInfo newdata = new LiveStockInfo();

                    newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                    newdata.LS_ITEM_ID = row["LS_ITEM_ID"].ToString();
                    newdata.LivestockName = row["Livestock Name"].ToString();
                    newdata.BirthYear = row["Birth Year"].ToString();
                    newdata.Insurance = row["Insurance"].ToString();
                    newdata.INSURANCE_ID = row["INSURANCE_ID"].ToString();
                    newdata.INSURANCE_COMPANY = row["Insurance Company"].ToString();
                    newdata.LS_INS_POLICY_NO = row["LS_INS_POLICY_NO"].ToString();
                    newdata.LS_INS_DATE = Convert.IsDBNull(row["LS_INS_DATE"]) ? dtnull : Convert.ToDateTime(row["LS_INS_DATE"]);//Redmine Bug #133138 fixed
                    newdata.LS_INS_AMT = Convert.ToDouble(row["LS_INS_AMT"]);
                    newdata.LS_AMT = Convert.ToDouble(row["LS_AMT"]);
                    newdata.CurrValue = Convert.ToDouble(row["Curr Value"]);
                    newdata.AL_LOC_NAME = row["AL_LOC_NAME"].ToString();
                    newdata.LS_LOC_AL_ID = row["LS_LOC_AL_ID"].ToString();
                    newdata.LS_OTHER_DETAIL = row["LS_OTHER_DETAIL"].ToString();
                    newdata.SaleStatus = row["Sale Status"].ToString();

                    newdata.YearID = row["YearID"].ToString();
                    newdata.TR_ID = row["TR_ID"].ToString();
                    newdata.EntryType = row["Entry Type"].ToString();
                    newdata.RemarkCount = (Int32)row["RemarkCount"];
                    newdata.RemarkStatus = row["RemarkStatus"].ToString();
                    newdata.OpenActions = (Int32)row["OpenActions"];
                    newdata.CrossedTimeLimit = (Int32)row["CrossedTimeLimit"];
                    newdata.AddBy = row["Add By"].ToString();
                    newdata.AddDate = Convert.ToDateTime(row["Add Date"].ToString());
                    newdata.EditBy = row["Edit By"].ToString();
                    newdata.EditDate = Convert.ToDateTime(row["Edit Date"].ToString());
                    newdata.ActionStatus = row["Action Status"].ToString();
                    newdata.ActionBy = row["Action By"].ToString();
                    newdata.ActionDate = (DateTime)row["Action Date"];
                    newdata.ID = row["ID"].ToString();
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);//redmine bug 133067 fixed
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);//redmine bug 133067 fixed
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);//redmine bug 133067 fixed
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);//redmine bug 133067 fixed
                    newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                    newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);

                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();

                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    BuildData.Add(newdata);
                }

                //var BuildData = from T in LS_Table.AsEnumerable()
                //            select new LiveStockInfo
                //            {
                //                ITEM_NAME = T["ITEM_NAME"].ToString(),
                //                LS_ITEM_ID = T["LS_ITEM_ID"].ToString(),
                //                LivestockName = T["Livestock Name"].ToString(),
                //                BirthYear = T["Birth Year"].ToString(),
                //                Insurance = T["Insurance"].ToString(),
                //                INSURANCE_ID = T["INSURANCE_ID"].ToString(),
                //                INSURANCE_COMPANY = T["Insurance Company"].ToString(),
                //                LS_INS_POLICY_NO = T["LS_INS_POLICY_NO"].ToString(),
                //                LS_INS_DATE = Convert.IsDBNull(T["LS_INS_DATE"]) ? dtnull : Convert.ToDateTime(T["LS_INS_DATE"]),//Redmine Bug #133138 fixed
                //                LS_INS_AMT = Convert.ToDouble(T["LS_INS_AMT"]),
                //                LS_AMT = Convert.ToDouble(T["LS_AMT"]),
                //                CurrValue = Convert.ToDouble(T["Curr Value"]),
                //                AL_LOC_NAME = T["AL_LOC_NAME"].ToString(),
                //                LS_LOC_AL_ID = T["LS_LOC_AL_ID"].ToString(),
                //                LS_OTHER_DETAIL = T["LS_OTHER_DETAIL"].ToString(),
                //                SaleStatus = T["Sale Status"].ToString(),
                //                YearID = T["YearID"].ToString(),
                //                TR_ID = T["TR_ID"].ToString(),
                //                EntryType = T["Entry Type"].ToString(),
                //                RemarkCount = (Int32)T["RemarkCount"],
                //                RemarkStatus = T["RemarkStatus"].ToString(),
                //                OpenActions = (Int32)T["OpenActions"],
                //                CrossedTimeLimit = (Int32)T["CrossedTimeLimit"],
                //                AddBy = T["Add By"].ToString(),
                //                AddDate = Convert.ToDateTime(T["Add Date"].ToString()),
                //                EditBy = T["Edit By"].ToString(),
                //                EditDate = Convert.ToDateTime(T["Edit Date"].ToString()),
                //                ActionStatus = T["Action Status"].ToString(),
                //                ActionBy = T["Action By"].ToString(),
                //                ActionDate = (DateTime)T["Action Date"],
                //                ID = T["ID"].ToString(),
                //                REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),//redmine bug 133067 fixed
                //                COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),//redmine bug 133067 fixed
                //                RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),//redmine bug 133067 fixed
                //                REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),//redmine bug 133067 fixed
                //                OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
                //                ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),



                //            };
                var Final_Data = BuildData.ToList();
                LiveStock_ExportData = Final_Data;
            }
            return PartialView("Frm_LiveStock_Info_Grid", LiveStock_ExportData);
        }
        #endregion

        #region <--Nested Grid-->
        public ActionResult Frm_LiveStock_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.LiveStockInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.LiveStockInfo_RecID = RecID;
            ViewBag.LiveStockInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    LiveStockInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_LiveStock);
                    Session["LiveStockInfo_detailGrid_Data"] = LiveStockInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_LiveStock);
                    LiveStockInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["LiveStockInfo_detailGrid_Data"] = data.DocumentMapping;
                    LiveStockInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(LiveStockInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(LiveStockInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "LiveStockListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "LiveStockListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["LiveStockInfo_detailGrid_Data"];
        }
        #endregion // <--Nested Grid-->

        #region Add/Edit Bank Account Details for popup
        public JsonResult DataNavigation(string ActionMethod, string ID, DateTime? Edit_Date)
        {
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_LiveStock, Common_Lib.Common.ClientAction.Lock_Unlock) && ActionMethod == "UNLOCKED")
            {
                return Json(new
                {
                    Message = ("Not Allowed..No Rights"),
                    result = "NoUnLockRights"
                }, JsonRequestBehavior.AllowGet);
            }
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_LiveStock, Common_Lib.Common.ClientAction.Lock_Unlock) && ActionMethod == "LOCKED")
            {
                return Json(new
                {
                    Message = ("Not Allowed..No Rights"),
                    result = "NoLockRights"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_Live_Stock_Window_Profile(string ActionMethod = null)
        {
            if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "Add") && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_LiveStock_Window','Not Allowed','No Rights');</script>");
            }
            return View();
        }


        //[HttpGet]
        //public ActionResult Frm_LiveStock_Window(DateTime? EditedOn, string ActionMethod = null, string ID = null)
        //{
        //    if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "Update") && ActionMethod == "Edit")
        //    {
        //       return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_LiveStock_Window','Not Allowed','No Rights');</script>");
        //    }
        //    if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "View") && ActionMethod == "View")
        //    {
        //       return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_LiveStock_Window','Not Allowed','No Rights');</script>");
        //    }
        //    if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "Delete") && ActionMethod == "Delete")
        //    {
        //       return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_LiveStock_Window','Not Allowed','No Rights');</script>");
        //    }
        //    LiveStockInfo model = new Models.LiveStockInfo();
        //    var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
        //    model.ActionMethod = Navigation_Mode_tag;
        //    model.TempActionMethod = Navigation_Mode_tag.ToString();
        //    if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
        //                || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
        //                || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
        //    {

        //        LiveStockInfo telinfo = new Models.LiveStockInfo();
        //        model.EditDate = Convert.ToDateTime(EditedOn);

        //        DataTable _dtableTelData = BASE._LiveStockDBOps.GetRecord(ID);
        //        if ((_dtableTelData == null))
        //        {
        //        }
        //        else
        //        {
        //            telinfo = DatatableToModel.DataTabletoLiveStockINFO(_dtableTelData).FirstOrDefault();
        //        }

        //        // -----------------------------+
        //        // Start : Check if entry already changed 
        //        // -----------------------------+
        //        if (BASE.AllowMultiuser())
        //        {
        //            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
        //                        || ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
        //                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))))
        //            {
        //                string viewstr = "";
        //                if ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))
        //                {
        //                    viewstr = "view";
        //                }

        //                if (EditedOn != null)
        //                {
        //                    if ((EditedOn != Convert.ToDateTime(_dtableTelData.Rows[0]["REC_EDIT_ON"])))
        //                    {
        //                        ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current LiveStockInfo", viewstr));
        //                    }
        //                }

        //            }

        //        }

        //        // -----------------------------+
        //        // End : Check if entry already changed 
        //        //-----------------------------+
        //        model.EditDate = Convert.ToDateTime(telinfo.REC_EDIT_ON);
        //        model.tP_NOField = telinfo.TP_NO;
        //        model.Old_tP_NOField = telinfo.TP_NO;
        //        model.telMiscIdField = telinfo.TP_TELECOM_MISC_ID;
        //        model.typeField = telinfo.TP_TYPE;
        //        model.categoryField = telinfo.TP_CATEGORY;
        //        model.other_DetField = telinfo.TP_OTHER_DETAIL;
        //    }

        //    if (((Navigation_Mode_tag) == Common_Lib.Common.Navigation_Mode._Delete))
        //    {
        //        model.Chk_Incompleted = false;
        //    }

        //    if (((Navigation_Mode_tag) == Common_Lib.Common.Navigation_Mode._View))
        //    {
        //        model.Chk_Incompleted = false;
        //    }

        //    return PartialView(model);
        //}

        [HttpPost]
        public ActionResult Frm_LiveStock_Window(LiveStockInfo model)
        {
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);

            return Json(new
            {
                Message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Start--> LookupEdit Events"
        //[HttpGet]
        //public ActionResult LookUp_Get_TeleCom_List(DataSourceLoadOptions loadOptions)
        //{
        //    DataTable telecomlist = BASE._LiveStockDBOps.GetTelecomCompanies("MISC_ID", "MISC_NAME") as DataTable;
        //    var telecomdata = DatatableToModel.DataTabletoTelecom_INFO(telecomlist);
        //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(telecomdata, loadOptions)), "application/json");
        //}
        #endregion

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('LiveStock_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion

        #region Create detail

        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
            string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                //this.Pic_Status.Visible = true;
                //this.Lbl_Seprator1.Visible = true;

                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
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
                    //this.Pic_Status.Image = My.Resources.@lock;
                    Lbl_Status_Color = "blue";
                }
                else
                {
                    Lbl_Status = Status;
                    //this.Pic_Status.Image = My.Resources.unlock;
                    if (Status.ToUpper().Trim().ToString() == "COMPLETED")
                        Lbl_Status_Color = "green";
                    else
                        Lbl_Status_Color = "red";
                }
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
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
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_LiveStock");
            Session.Remove("LiveStockInfo_detailGrid_Data");

        }
        public void SessionClear_Window()
        {
            ClearBaseSession("_LiveStockWindow");
        }
        public void LiveStock_user_rights()
        {
            ViewData["LiveStock_AddRight"] = CheckRights(BASE, ClientScreen.Profile_LiveStock, "Add");
            ViewData["LiveStock_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_LiveStock, "Update");
            ViewData["LiveStock_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_LiveStock, "View");
            ViewData["LiveStock_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_LiveStock, "Delete");
            ViewData["LiveStock_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_LiveStock, "Export");
            ViewData["LiveStock_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_LiveStock, Common.ClientAction.Lock_Unlock);
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
        }


        //*******************************#RegionDevextreme***************************************************************  
        #region devextreme

        public ActionResult Frm_LiveStock_Info_dx()
        {
            SetDefaultValues();
            LiveStock_user_rights();

            if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "List"))
            {
                return Content("<script>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_LiveStock').hide();</script>");
            }

            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains(ClientScreen.Profile_LiveStock.ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;

            ViewData["LiveStock_Auto_Vouching_Mode"] = BASE._IsUnderAudit &&
                (BASE._open_User_Type.Equals(Common_Lib.Common.ClientUserType.SuperUser, StringComparison.OrdinalIgnoreCase) ||
                BASE._open_User_Type.Equals(Common_Lib.Common.ClientUserType.Auditor, StringComparison.OrdinalIgnoreCase));

            return View();
        }

        public ActionResult Frm_LiveStock_Info_Grid_dx()
        {
            try
            {
                

                Common_Lib.RealTimeService.Param_GetProfileListing LSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing
                {
                    Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK,
                    Prev_YearId = BASE._prev_Unaudited_YearID,
                    Next_YearID = BASE._next_Unaudited_YearID,
                    TableName = Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO
                };

                DataTable LS_Table = BASE._LiveStockDBOps.GetProfileListing(LSProfile);


                List<LiveStockInfo> BuildData = new List<LiveStockInfo>();

                foreach (DataRow row in LS_Table.Rows)
                {
                    LiveStockInfo newdata = new LiveStockInfo();

                    newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                    newdata.LS_ITEM_ID = row["LS_ITEM_ID"].ToString();
                    newdata.LivestockName = row["Livestock Name"].ToString();
                    newdata.BirthYear = row["Birth Year"].ToString();
                    newdata.Insurance = row["Insurance"].ToString();
                    newdata.INSURANCE_ID = row["INSURANCE_ID"].ToString();
                    newdata.INSURANCE_COMPANY = row["Insurance Company"].ToString();
                    newdata.LS_INS_POLICY_NO = row["LS_INS_POLICY_NO"].ToString();
                    newdata.LS_INS_DATE = Convert.IsDBNull(row["LS_INS_DATE"]) ? (DateTime?)null : Convert.ToDateTime(row["LS_INS_DATE"]);
                    newdata.LS_INS_AMT = row["LS_INS_AMT"] == System.DBNull.Value ? 0 : Convert.ToDouble(row["LS_INS_AMT"]);
                    newdata.LS_AMT = Convert.ToDouble(row["LS_AMT"]);
                    newdata.CurrValue = Convert.ToDouble(row["Curr Value"]);
                    newdata.AL_LOC_NAME = row["AL_LOC_NAME"].ToString();
                    newdata.LS_OTHER_DETAIL = row["LS_OTHER_DETAIL"].ToString();
                    newdata.SaleStatus = row["Sale Status"].ToString();
                    newdata.YearID = row["YearID"].ToString();
                    newdata.TR_ID = row["TR_ID"].ToString();
                    newdata.EntryType = row["Entry Type"].ToString();
                    newdata.AddBy = row["Add By"].ToString();
                    newdata.AddDate = Convert.ToDateTime(row["Add Date"].ToString());
                    

                    newdata.EditBy = row["Edit By"].ToString();
                    newdata.EditDate = Convert.ToDateTime(row["Edit Date"].ToString());
                    

                    newdata.ActionStatus = row["Action Status"].ToString();
                    newdata.ActionBy = row["Action By"].ToString();
                    newdata.ActionDate = Convert.ToDateTime(row["Action Date"].ToString());
                    

                    newdata.ID = row["ID"].ToString();
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                    newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                    newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                    newdata.VOUCHING_PENDING_COUNT = row.Field<int?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<int?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<int?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<int?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<int?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<int?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<int?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<int?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<int?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<int?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    BuildData.Add(newdata);

                }
                var Final_Data = BuildData.ToList();

                return Content(JsonConvert.SerializeObject(BuildData), "application/json");
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Frm_LiveStock_Info_DetailGrid_dx(bool VouchingMode = false, string RecID = "")
        {

            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_LiveStock, !VouchingMode)), "application/json");
        }

        public ActionResult Frm_LiveStock_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {

            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_LiveStock)), "application/json");
        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_LiveStock, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('LiveStock_report_modal','Not Allowed','No Rights');$('#OnLiveStockListPreviewClick_dx').hide();</script>");
            }
            return PartialView();
        }

        #endregion

    }
}