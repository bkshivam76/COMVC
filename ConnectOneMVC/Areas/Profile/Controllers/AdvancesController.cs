using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class AdvancesController : BaseController
    {

        #region "Start--> Default Variables"
        
        public DateTime LastEditedOn
        {
            get
            {
                return (DateTime)GetBaseSession("LastEditedOn_Advances");
            }
            set
            {
                SetBaseSession("LastEditedOn_Advances", value);
            }
        }
        public string Voucher_Entry
        {
            get
            {
                return (string)GetBaseSession("Voucher_Entry_Advances");
            }
            set
            {
                SetBaseSession("Voucher_Entry_Advances", value);
            }
        }
        public List<AdvancesProfile> Advances_ExportData
        {
            get
            {
                return (List<AdvancesProfile>)GetBaseSession("Advances_ExportData_Advances");
            }
            set
            {
                SetBaseSession("Advances_ExportData_Advances", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> AdvancesInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("AdvancesInfo_DetailGrid_Data_Advances");
            }
            set
            {
                SetBaseSession("AdvancesInfo_DetailGrid_Data_Advances", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> AdvancesInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("AdvancesInfo_AdditionalInfoGrid_Advances");
            }
            set
            {
                SetBaseSession("AdvancesInfo_AdditionalInfoGrid_Advances", value);
            }
        }
        public void SetDefaultValues()
        {
            LastEditedOn = default(DateTime);
            Voucher_Entry = "Voucher Entry";
        }        
        #endregion

        #region Page Constructor
        public AdvancesController()
        {

        }
        #endregion 

        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/Advances Grid_Display() Of ALL_Projects)
        public ActionResult Frm_Advances_Info()
        {
            Advances_user_rights();
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.IsAuditor = true;
            }
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.GroupShowFooter = false;
            }
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Manage_Remarks))
            {
                ViewBag.AddRemarks = false;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }
            SetDefaultValues();
            if (!CheckRights(BASE,ClientScreen.Profile_Advances,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Advances').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Advances).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetAdvProfileListing AdvProfile = new Common_Lib.RealTimeService.Param_GetAdvProfileListing();
            AdvProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            AdvProfile.Next_YearID = BASE._next_Unaudited_YearID;
            DataTable AI_Table = BASE._AdvanceDBOps.GetProfileListing(AdvProfile);
            if ((AI_Table == null))
            {
                return PartialView("Frm_Advances_Info_Grid", null);
            }
            bool Remarks = false;

            DateTime? dtnull = null;
            // BUILD DATA
            List<AdvancesProfile> BuildData = new List<AdvancesProfile>();
            foreach (DataRow row in AI_Table.Rows)
            {
                AdvancesProfile newdata = new AdvancesProfile();

                newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                newdata.AI_ITEM_ID = row["AI_ITEM_ID"].ToString();
                newdata.AI_PARTY_ID = row["AI_PARTY_ID"].ToString();
                newdata.PARTY_NAME = row["PARTY_NAME"].ToString();
                newdata.AI_ADV_DATE = Convert.IsDBNull(row["AI_ADV_DATE"]) ? dtnull : Convert.ToDateTime(row["AI_ADV_DATE"]);
                newdata.Advance = (decimal)row["Advance"];
                newdata.Addition = (decimal)row["Addition"];
                newdata.Adjusted = (decimal)row["Adjusted"];
                newdata.Refund = (decimal)row["Refund"];
                newdata.OutStanding = (decimal)row["Out-Standing"];
                newdata.Reason = row["Reason"].ToString();
                newdata.AI_OTHER_DETAIL = row["AI_OTHER_DETAIL"].ToString();
                newdata.TR_ID = row["TR_ID"].ToString();
                newdata.YearID = (Int32)row["YearID"];
                newdata.ID = row["ID"].ToString();
                newdata.Type = row["Type"].ToString();
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
                newdata.ActionDate = Convert.IsDBNull(row["Action Date"]) ? dtnull : Convert.ToDateTime(row["Action Date"]);
                newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
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
                //var BuildData = from T in AI_Table.AsEnumerable()
                //            select new AdvancesProfile
                //            {
                //                ITEM_NAME = T["ITEM_NAME"].ToString(),
                //                AI_ITEM_ID = T["AI_ITEM_ID"].ToString(),
                //                AI_PARTY_ID = T["AI_PARTY_ID"].ToString(),
                //                PARTY_NAME = T["PARTY_NAME"].ToString(),
                //                AI_ADV_DATE = Convert.IsDBNull(T["AI_ADV_DATE"])? dtnull:Convert.ToDateTime(T["AI_ADV_DATE"]),
                //                Advance = (decimal)T["Advance"],
                //                Addition = (decimal)T["Addition"],
                //                Adjusted = (decimal)T["Adjusted"],
                //                Refund = (decimal)T["Refund"],
                //                OutStanding = (decimal)T["Out-Standing"],
                //                Reason = T["Reason"].ToString(),
                //                AI_OTHER_DETAIL = T["AI_OTHER_DETAIL"].ToString(),
                //                TR_ID = T["TR_ID"].ToString(),
                //                YearID = (Int32)T["YearID"],
                //                ID = T["ID"].ToString(),
                //                Type = T["Type"].ToString(),
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
                //                ActionDate = Convert.IsDBNull(T["Action Date"])?dtnull:Convert.ToDateTime(T["Action Date"]),
                //                REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),
                //                COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),
                //                RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),
                //                REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),
                //                OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
                //                ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),

                //            };
            var Final_Data = BuildData.ToList();
            Advances_ExportData = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["AdvancesInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Final_Data);
        }
        public PartialViewResult Frm_Advances_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default",string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Advances_user_rights();
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.GroupShowFooter = false;
            }
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Manage_Remarks))
            {
                ViewBag.AddRemarks = false;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            if (Advances_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetAdvProfileListing AdvProfile = new Common_Lib.RealTimeService.Param_GetAdvProfileListing();
                AdvProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                AdvProfile.Next_YearID = BASE._next_Unaudited_YearID;
                DataTable AI_Table = BASE._AdvanceDBOps.GetProfileListing(AdvProfile);
                if ((AI_Table == null))
                {
                    return PartialView("Frm_Advances_Info_Grid", null);
                }
                bool Remarks = false;
                //AI_Table.Columns.Add("Remarks", Remarks.GetType());
                //foreach (var XRow in AI_Table.Rows)
                //{
                //    XRow["RemarkCount"] = ((XRow["RemarkCount"] > 0) ? true : false);
                //}
                DateTime? dtnull = null;

                // BUILD DATA
                List<AdvancesProfile> BuildData = new List<AdvancesProfile>();
                foreach (DataRow row in AI_Table.Rows)
                {
                    AdvancesProfile newdata = new AdvancesProfile();

                    newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                    newdata.AI_ITEM_ID = row["AI_ITEM_ID"].ToString();
                    newdata.AI_PARTY_ID = row["AI_PARTY_ID"].ToString();
                    newdata.PARTY_NAME = row["PARTY_NAME"].ToString();
                    newdata.AI_ADV_DATE = Convert.IsDBNull(row["AI_ADV_DATE"]) ? dtnull : Convert.ToDateTime(row["AI_ADV_DATE"]);
                    newdata.Advance = (decimal)row["Advance"];
                    newdata.Addition = (decimal)row["Addition"];
                    newdata.Adjusted = (decimal)row["Adjusted"];
                    newdata.Refund = (decimal)row["Refund"];
                    newdata.OutStanding = (decimal)row["Out-Standing"];
                    newdata.Reason = row["Reason"].ToString();
                    newdata.AI_OTHER_DETAIL = row["AI_OTHER_DETAIL"].ToString();
                    newdata.TR_ID = row["TR_ID"].ToString();
                    newdata.YearID = (Int32)row["YearID"];
                    newdata.ID = row["ID"].ToString();
                    newdata.Type = row["Type"].ToString();
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
                    newdata.ActionDate = Convert.IsDBNull(row["Action Date"]) ? dtnull : Convert.ToDateTime(row["Action Date"]);
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
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
                //var BuildData = from T in AI_Table.AsEnumerable()
                //                select new AdvancesProfile
                //                {
                //                    ITEM_NAME = T["ITEM_NAME"].ToString(),
                //                    AI_ITEM_ID = T["AI_ITEM_ID"].ToString(),
                //                    AI_PARTY_ID = T["AI_PARTY_ID"].ToString(),
                //                    PARTY_NAME = T["PARTY_NAME"].ToString(),
                //                    AI_ADV_DATE = Convert.IsDBNull(T["AI_ADV_DATE"])?dtnull:Convert.ToDateTime(T["AI_ADV_DATE"]),
                //                    Advance = (decimal)T["Advance"],
                //                    Addition = (decimal)T["Addition"],
                //                    Adjusted = (decimal)T["Adjusted"],
                //                    Refund = (decimal)T["Refund"],
                //                    OutStanding = (decimal)T["Out-Standing"],
                //                    Reason = T["Reason"].ToString(),
                //                    AI_OTHER_DETAIL = T["AI_OTHER_DETAIL"].ToString(),
                //                    TR_ID = T["TR_ID"].ToString(),
                //                    YearID = (Int32)T["YearID"],
                //                    ID = T["ID"].ToString(),
                //                    Type = T["Type"].ToString(),
                //                    RemarkCount = (Int32)T["RemarkCount"],
                //                    RemarkStatus = T["RemarkStatus"].ToString(),
                //                    OpenActions = (Int32)T["OpenActions"],
                //                    CrossedTimeLimit = (Int32)T["CrossedTimeLimit"],
                //                    AddBy = T["Add By"].ToString(),
                //                    AddDate = Convert.ToDateTime(T["Add Date"].ToString()),
                //                    EditBy = T["Edit By"].ToString(),
                //                    EditDate = Convert.ToDateTime(T["Edit Date"].ToString()),
                //                    ActionStatus = T["Action Status"].ToString(),
                //                    ActionBy = T["Action By"].ToString(),
                //                    ActionDate = Convert.IsDBNull(T["Action Date"])?dtnull:Convert.ToDateTime(T["Action Date"]),
                //                    REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),
                //                    COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),
                //                    RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),
                //                    REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),
                //                    OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
                //                    ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),
                //                };
                var fdata = BuildData.ToList();
                Advances_ExportData = fdata;
            }

            var Final_Data = Advances_ExportData as List<AdvancesProfile>;
            
            return PartialView(Final_Data);
        }
        #region <--NestedGrid-->
        public ActionResult Frm_Advances_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.AdvancesInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.AdvancesInfo_RecID = RecID;
            ViewBag.AdvancesInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Advances);
                    AdvancesInfo_DetailGrid_Data = _docList;
                    Session["AdvancesInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Advances);
                    AdvancesInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["AdvancesInfo_detailGrid_Data"] = data.DocumentMapping;
                    AdvancesInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(AdvancesInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(AdvancesInfo_AdditionalInfoGrid);
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
            settings.Name = "AdvancesListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "AdvancesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["AdvancesInfo_detailGrid_Data"];
        }
        #endregion // <--NestedGrid-->

        public ActionResult AdvancesCustomDataAction(string key)
        {
            var Final_Data = Advances_ExportData as List<AdvancesProfile>;
            var it = Final_Data.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.ID + "![" + it.ITEM_NAME + "![" + it.PARTY_NAME + "![" + it.AI_ADV_DATE + "![" +
                            it.AI_OTHER_DETAIL + "![" + it.Advance + "![" + it.EditDate + "![" + it.YearID + "![" +
                            it.TR_ID + "![" + it.AI_PARTY_ID + "![" + it.AddDate + "![" + it.AddBy + "![" + it.EditBy + "![" + it.ActionStatus
                            + "![" + it.ActionDate + "![" + it.ActionBy + "![" + it.Type;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        #endregion

        #region Add/Edit Advances Account Details for popup
        [HttpGet]
        public ActionResult Frm_Advances_Window(string EditedOn, string ActionMethod = null, string ID = null)
        {
            ViewData["Advances_AdresBookListRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_Advances, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Advances_Window','Not Allowed','No Rights');</script>");
                }
            }
            //LookUp_GetPartyList_Advances(ActionMethod == "View" || ActionMethod == "Delete");
            DateTime? EditDate = Convert.ToDateTime(EditedOn);
            AdvancesParam model = new Models.AdvancesParam();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);


            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                DataTable _dtableTelData = BASE._AdvanceDBOps.GetRecord(ID);
                if ((_dtableTelData == null))
                {
                    ModelState.AddModelError("data", "No Record Found");
                }
                else
                {
                    model = DatatableToModel.DataTabletoAdvancesINFO(_dtableTelData).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(model.AdvanceDate))
                {
                    model.AdvanceDate = Convert.ToDateTime(model.AdvanceDate).ToShortDateString();
                }
                model.EditDate = Convert.ToDateTime(EditedOn);
                // -----------------------------+
                // Start : Check if entry already changed 
                // -----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                                || ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                                || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))))
                    {
                        string viewstr = "";
                        if ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))
                        {
                            viewstr = "view";
                        }

                        if (EditDate != null)
                        {
                            if ((EditDate != Convert.ToDateTime(_dtableTelData.Rows[0]["REC_EDIT_ON"])))
                            {
                                ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current AdvancesInfo", viewstr));
                            }
                        }

                    }

                }
            }
            model.Tag = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Advances_Window(AdvancesParam model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + model.TempActionMethod);
            LastEditedOn = Convert.ToDateTime(model.EditDate);


            // -----------------------------+
            // Start : Check if entry already changed 
            // -----------------------------+
            if (BASE.AllowMultiuser())
            {
                if (((model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            || (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)))
                {
                    DataTable advances_DbOps = BASE._AdvanceDBOps.GetRecord(model.RecID);
                    if ((advances_DbOps == null))
                    {
                        return Json(new
                        {
                            Message = "something went wrong!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((advances_DbOps.Rows.Count == 0))
                    {
                        return Json(new
                        {
                            Message = "Record Already Deleted!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((LastEditedOn != Convert.ToDateTime(advances_DbOps.Rows[0]["REC_EDIT_ON"])))
                    {
                        return Json(new
                        {
                            Message = "Record Already Changed!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    Common.Record_Status? MaxValue = 0;
                    MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetStatus(model.RecID);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Something went wrong!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = "Record Status Already Changed!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    bool DelEditAllow = true;
                    string UsedPage = "";
                    MaxValue = 0;
                    // 1
                    if (DelEditAllow)
                    {
                        // Payment, Receipt dependency check #Ref AC 18, AD18
                        MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetAdvancePaymentCount(model.RecID);
                        if ((MaxValue == null))
                        {
                            return Json(new
                            {
                                Message = "Something went wrong!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue > 0))
                        {
                            DelEditAllow = false;
                        }

                        UsedPage = "Voucher Entry...";
                        if ((MaxValue > 0))
                        {
                            DelEditAllow = false;
                        }

                        UsedPage = "Voucher Entry...";
                    }

                    if (DelEditAllow)
                    {
                        // check journal references 
                        Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = model.RecID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                        // Journal Ref dependency check #Ref AP18
                        DataTable jAdj = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances);
                        if ((jAdj == null))
                        {
                            return Json(new
                            {
                                Message = "Something went wrong!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        MaxValue = (Common.Record_Status)jAdj.Rows.Count;
                        if ((MaxValue > 0))
                        {
                            DelEditAllow = false;
                        }

                        UsedPage = "Journal Voucher Entry...";
                    }

                    if (!DelEditAllow)
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be Edited / Deleted. . !" + ("<br/>" + ("<br/>" + ("This information is being used in Another Pa g e. . . !" + ("<br/>" + ("<br/>" + ("Name : " + UsedPage)))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                // Check for open actions
                if ((model.Tag == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    var openActions = BASE._Action_Items_DBOps.GetOpenActions(model.RecID, "ADVANCES_INFO");
                    if ((openActions != null))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be Deleted..!" + ("<br/>" + ("<br/>" + "There are open actions / queries posted against it...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }

            // -----------------------------+
            // End : Check if entry already changed 
            // -----------------------------+
            if (((model.Tag == Common_Lib.Common.Navigation_Mode._New)
                        || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)))
            {
                if ((model.ItemID.Trim().Length == 0))
                {
                    return Json(new
                    {
                        Message = "Item Name Not Selected...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.PartyID.Trim().Length == 0))
                {
                    return Json(new
                    {
                        Message = "Party Name Not Selected...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if (IsDate(model.AdvanceDate))
                {
                    if ((Convert.ToDateTime(model.AdvanceDate) >= BASE._open_Year_Sdt))
                    {
                        return Json(new
                        {
                            Message = "Date must be Earlier than Start Financial Year...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if ((model.Amount) <= 0)
                {
                    return Json(new
                    {
                        Message = "Amount cannot be Zero / Negative...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                //if ((model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
                //{
                //    if ((Total_amt > 0))
                //    {
                //        if ((double.Parse(this.Txt_Amt.Text) < Total_amt))
                //        {
                //            string next_Year_msg = "";
                //            if ((BASE._next_Unaudited_YearID != 0))
                //            {
                //                next_Year_msg = " This includes Adjustments / Refunds / Additions made in next year too. ";
                //            }

                //            return Json(new
                //            {
                //                Message = "Amount cannot be less than the total of Adjusted and Refund Amount of "
                //                            + (Total_amt + ("...!" + next_Year_msg))), this.Txt_Amt, 0, this.Txt_Amt.Height, 5000),
                //                result = false
                //            }, JsonRequestBehavior.AllowGet);

                //        }

                //    }

                //}

            }

            // -----------------------------------------
            if (BASE.AllowMultiuser())
            {
                if ((model.Tag == Common_Lib.Common.Navigation_Mode._New))
                {
                    if ((model.PartyID.ToString().Length > 0))
                    {
                        // Party(AddressBook) Dependency Check #Ref Z18
                        DateTime oldEditOn = Convert.ToDateTime(model.PartyEditDate);
                        DataTable d1 = BASE._AdvanceDBOps.GetPartiesByRecID(model.PartyID);
                        if ((d1.Rows.Count <= 0))
                        {
                            // A/D,E/D
                            return Json(new
                            {
                                Message = "Referred Record Already Deleted!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            // Record has not been deleted
                            DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0][4]);
                            if (NewEditOn.ToLongDateString() != oldEditOn.ToLongDateString())
                            {
                                // A/E,E/E
                                return Json(new
                                {
                                    Message = "Referred Record Already Changed!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }

                    }

                }

            }

            // ------------------------------------------
            // 'CHECKING LOCK STATUS
            // If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            //     Dim MaxValue As Object = 0
            //     MaxValue = Base._AdvanceDBOps.GetStatus(Me.xID.Text)
            //     If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            //     If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            // End If

            int Status_Action;
            if (model.Chk_Incompleted)
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Incomplete);
            }
            else
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);
            }

            if ((model.TempActionMethod == "Delete"))
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Deleted);
            }

            if ((model.Tag == Common_Lib.Common.Navigation_Mode._New))
            {
                // new
                model.RecID = System.Guid.NewGuid().ToString();
                Common_Lib.RealTimeService.Parameter_Insert_Advances InParam = new Common_Lib.RealTimeService.Parameter_Insert_Advances();
                InParam.ItemID = model.ItemID;
                InParam.PartyID = model.PartyID;
                if (IsDate(model.AdvanceDate))
                {
                    InParam.AdvanceDate = Convert.ToDateTime(model.AdvanceDate).ToString(BASE._Server_Date_Format_Long);
                }

                InParam.Amount = Convert.ToDouble(model.Amount);
                InParam.Purpose = model.Purpose;
                InParam.Remarks = model.Remarks;
                InParam.Status_Action = Status_Action.ToString();
                if (BASE._AdvanceDBOps.Insert(InParam))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            if ((model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
            {
                // edit
                Common_Lib.RealTimeService.Parameter_Update_Advances UpParam = new Common_Lib.RealTimeService.Parameter_Update_Advances();
                UpParam.ItemID = model.ItemID;
                UpParam.PartyID = model.PartyID;
                if (IsDate(model.AdvanceDate))
                {
                    UpParam.AdvanceDate = Convert.ToDateTime(model.AdvanceDate).ToString(BASE._Server_Date_Format_Long);
                }

                UpParam.Amount = Convert.ToDouble(model.Amount);
                UpParam.Purpose = model.Purpose;
                UpParam.Remarks = model.Remarks;
                // UpParam.Status_Action = Status_Action
                UpParam.Rec_ID = model.RecID;
                if (BASE._AdvanceDBOps.Update(UpParam))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.UpdateSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            if ((model.Tag == Common_Lib.Common.Navigation_Mode._Delete))
            {
                // DELETE
              //  Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();

                if (BASE._AdvanceDBOps.Delete(model.RecID))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.DeleteSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }
            }

            return View(model);
        }

        public JsonResult DataNavigation(AdvancesProfile adv,string EditDate, string ActionMethod = null)
        {
            try
            {
                // ------------------------------------------------------
                if (BASE.AllowMultiuser())
                {
                    if (((ActionMethod.ToUpper() == "PRINT-LIST")
                                || ((ActionMethod.ToUpper() == "LOCKED")
                                || (ActionMethod.ToUpper() == "UNLOCKED"))))
                    {
                        string xTemp_ID = adv.ID;
                        DataTable d1 = BASE._AdvanceDBOps.GetRecord(xTemp_ID);
                        if ((d1 == null))
                        {
                            return Json(new
                            {
                                Message = "Entry Cannot be created..!" + ("<br/>" + ("<br/>" + "Required Profile Entries have already been created for this centre...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((d1.Rows.Count == 0))
                        {
                            return Json(new
                            {
                                Message = "Record Changed / Removed in Background!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        var RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        if ((RecEdit_Date != Convert.ToDateTime(EditDate)))
                        {
                            return Json(new
                            {
                                Message = "Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }

                // ---------------------------------------------------------
                if ((ActionMethod.ToUpper()) == "NEW")
                {
                    if ((BASE._Completed_Year_Count > 0))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be created..!" + ("<br/>" + ("<br/>" + "Required Profile Entries have already been created for this centre...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                    {
                        return Json(new
                        {
                            Message = "Profile Entries not allowed for a newly opened center",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if ((ActionMethod.ToUpper()) == "EDIT")
                {
                    string xTemp_ID = adv.ID;
                    //  If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "YearID").ToString() <> Base._open_Year_ID Then  'NOT Current Year Entry (add check for non-carried fwd record in split data) 
                    bool? IsAdvCarriedFwd = null;
                    IsAdvCarriedFwd = BASE._AdvanceDBOps.IsAdvRecordCarriedForward(xTemp_ID, Convert.ToInt32(adv.YearID));
                    if ((IsAdvCarriedFwd == null))
                    {
                        return Json(new
                        {
                            Message = "Something went wrong!!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((IsAdvCarriedFwd == true))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be edited..!" + ("<br/>" + ("<br/>" + "This entry has been carried forward from previous year(s)...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xTr_ID = adv.TR_ID;
                    if ((xTr_ID != null))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be Edited / Deleted..!" + ("<br/>" + ("<br/>" + "This Entry Managed by Voucher Entry...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xStatus = adv.ActionStatus;
                    var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                    Common.Record_Status? MaxValue = 0;
                    bool AllowUser = false;
                    MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Entry Not Found / Changed In Background...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    string multiUserMsg = "";
                    if ((value != MaxValue))
                    {
                        if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been locked in the background by another user."));
                        }
                        else if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been unlocked in the background by another user."));
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "Record Status has been changed in the background by another user"));
                            AllowUser = true;
                        }

                        if (AllowUser)
                        {
                            return Json(new
                            {
                                Message = (multiUserMsg + ("<br/>" + ("<br/>" + "Do you want to continue...?"))),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = "Locked Entry cannot be Edited / Deleted...!"
                                        + (multiUserMsg + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    AdvancesParam xfrm = new AdvancesParam();
                    xfrm.RecID = xTemp_ID;
                    // -----------------------------+
                    // Start : Edit date sent to Check if entry already changed 
                    // -----------------------------+
                    if (EditDate != null)
                    {
                        xfrm.EditDate = Convert.ToDateTime(EditDate);
                    }
                    // -----------------------------+
                    // End : Edit date sent to Check if entry already changed 
                    // -----------------------------+
                    Decimal next_year_deductions = 0;
                    if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                    {
                        // Payment Table...
                        DataTable ADV_PAY_TABLE = BASE._AdvanceDBOps.GetPayments(("\'"
                                        + adv.AI_PARTY_ID), BASE._next_Unaudited_YearID);
                        // Get Current Year Payments 
                        if ((ADV_PAY_TABLE == null))
                        {
                            return Json(new
                            {
                                Message = "",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Journal Adjustments 
                        Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = adv.ID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only;
                        param.YearID = BASE._next_Unaudited_YearID;
                        // Get Current Year Adjustments  
                        DataTable JOURNAL_ADJ_TABLE = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances);
                        if ((JOURNAL_ADJ_TABLE == null))
                        {
                            return Json(new
                            {
                                Message = "",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Journal Additions 
                        param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = adv.ID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only;
                        param.YearID = BASE._next_Unaudited_YearID;
                        // Get Current Year Adjustments  
                        DataTable JOURNAL_ADDITION_TABLE = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances);
                        if ((JOURNAL_ADDITION_TABLE == null))
                        {
                            return Json(new
                            {
                                Message = "",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Payment
                        foreach (DataRow XRow in ADV_PAY_TABLE.Rows)
                        {
                            if ((adv.ID == XRow["TR_REF_ID"].ToString()))
                            {
                                next_year_deductions = (next_year_deductions + Convert.ToDecimal(XRow["Adjusted"]));
                                next_year_deductions = (next_year_deductions + Convert.ToDecimal(XRow["Refund"]));
                            }

                        }

                        // Adjustment
                        foreach (DataRow XRow in JOURNAL_ADJ_TABLE.Rows)
                        {
                            next_year_deductions = (next_year_deductions + Convert.ToDecimal(XRow["AMOUNT"]));
                        }

                        // Addition
                        foreach (DataRow XRow in JOURNAL_ADDITION_TABLE.Rows)
                        {
                            next_year_deductions = (next_year_deductions - Convert.ToDecimal(XRow["AMOUNT"]));
                        }

                    }

                    //xfrm.Amount = Convert.ToDouble((double.Parse(adv.Adjusted).ToString())
                    //            + (double.Parse(adv.Refund).ToString()) + next_year_deductions);
                    //xfrm.Os_Amn = (double.Parse(adv.OutStanding.ToString()) - next_year_deductions);
                    //if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                    //{
                    //    xfrm.Chk_Incompleted.Checked = false;
                    //}
                    //else
                    //{
                    //    xfrm.Chk_Incompleted.Checked = true;
                    //}
                    return Json(new
                    {
                        Message = "",
                        result = true,
                        Data = xfrm,
                    }, JsonRequestBehavior.AllowGet);
                }

                else if ((ActionMethod.ToUpper()) == "DELETE")
                {

                    string xTemp_ID = adv.ID;
                    // If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "YearID").ToString() <> Base._open_Year_ID Then  'NOT Current Year Entry (add check for non-carried fwd record in split data) 
                    bool? IsAdvCarriedFwd = null;
                    IsAdvCarriedFwd = BASE._AdvanceDBOps.IsAdvRecordCarriedForward(xTemp_ID, Convert.ToInt32(adv.YearID));
                    if ((IsAdvCarriedFwd == null))
                    {
                        return Json(new
                        {
                            Message = "Something went wrong!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((IsAdvCarriedFwd == true))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be deleted..!" + ("<br/>" + ("<br/>" + "This entry has been carried forward from previous year(s)...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xTr_ID = adv.TR_ID;
                    if ((xTr_ID != null))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot Edited / Deleted..!" + ("<br/>" + ("<br/>" + "This Entry Managed by Voucher Entry...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    int xOpenActions = Convert.ToInt32(adv.OpenActions);
                    if ((xOpenActions > 0))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be Deleted..!" + ("<br/>" + ("<br/>" + "There are open actions / queries posted against it...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xStatus = adv.ActionStatus;
                    var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                    Common.Record_Status? MaxValue = 0;
                    bool AllowUser = false;
                    MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Entry Not Found / Changed In Background...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string multiUserMsg = "";
                    if ((value != MaxValue))
                    {
                        if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been locked in the background by another user."));
                        }
                        else if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been unlocked in the background by another user."));
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = ("<br/>" + ("<br/>" + "Record Status has been changed in the background by another user"));
                            AllowUser = true;
                        }

                        if (AllowUser)
                        {
                            return Json(new
                            {
                                Message = (multiUserMsg + ("<br/>" + ("<br/>" + "Do you want to continue...?"))),
                                result = true
                            }, JsonRequestBehavior.AllowGet);

                        }

                    }

                    if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = "Locked Entry cannot be Edited / Deleted...!"
                                        + (multiUserMsg + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    bool DeleteAllow = true;
                    string UsedPage = "";
                    MaxValue = 0;
                    // 1
                    if (DeleteAllow)
                    {
                        MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetAdvancePaymentCount(xTemp_ID);
                        if ((MaxValue == null))
                        {
                            return Json(new
                            {
                                Message = "",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue > 0))
                        {
                            DeleteAllow = false;
                        }

                        UsedPage = "Voucher Entry...";
                        if ((MaxValue > 0))
                        {
                            DeleteAllow = false;
                        }

                        UsedPage = "Voucher Entry...";
                    }

                    if (DeleteAllow)
                    {
                        // check journal references 
                        Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = xTemp_ID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                        MaxValue = (Common.Record_Status)BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances).Rows.Count;
                        if ((MaxValue > 0))
                        {
                            DeleteAllow = false;
                        }

                        UsedPage = "Journal Voucher Entry...";
                    }

                    if (DeleteAllow)
                    {
                        AdvancesParam xfrm = new AdvancesParam();
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete;
                        xfrm.RecID = xTemp_ID;
                        // -----------------------------+
                        // Start : Edit date sent to Check if entry already changed 
                        // -----------------------------+
                        xfrm.EditDate = Convert.ToDateTime(EditDate);
                        // -----------------------------+
                        // End : Edit date sent to Check if entry already changed 
                        // -----------------------------+
                        return Json(new
                        {
                            Message = "",
                            result = true,
                            data = xfrm,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                else if ((ActionMethod.ToUpper()) == "VIEW")
                {
                    string xTemp_ID = adv.ID;
                    object MaxValue = 0;
                    MaxValue = BASE._AdvanceDBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Entry Not Found / Changed In Background...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    AdvancesParam xfrm = new AdvancesParam();
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._View;
                    xfrm.RecID = xTemp_ID;
                    // -----------------------------+
                    // Start : Edit date sent to Check if entry already changed 
                    // -----------------------------+
                    xfrm.EditDate = Convert.ToDateTime(EditDate);
                    //xfrm.Amount = adv.Advance;
                    // -----------------------------+
                    // End : Edit date sent to Check if entry already changed 
                    // -----------------------------+
                    return Json(new
                    {
                        Message = "",
                        result = true,
                        data = xfrm,
                    }, JsonRequestBehavior.AllowGet);
                }

                else if ((ActionMethod.ToUpper()) == "LOCKED")
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Lock_Unlock))
                    {
                        int Ctr = 0;

                        string xTemp_ID = adv.ID;
                        string xType = adv.Type;
                        var xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.ADVANCES_INFO, xTemp_ID).ToString();
                        // Please Note that this is a dependency , in case advance creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                        if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                        {
                            return Json(new
                            {
                                Message = "Entries Created from Vouchers can be Audited from Vouchers Only...!" + ("<br/>" + ("<br/>" + "Please unselect Entries Created from Voucher ...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string xStatus = adv.ActionStatus;
                        var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                        Common.Record_Status? MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if ((value != MaxValue))
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                            {
                                AllowUser = true;
                            }

                            if (AllowUser)
                            {
                                return Json(new
                                {
                                    Message = "Record has been Unlocked in the background by another user" + ("<br/>" + ("<br/>" + "Do you want to continue...?")),
                                    result = true
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {
                            Msg = "Information...";
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            return Json(new
                            {
                                Message = "Already Locked Entries can't be Re-Locked...!" + ("<br/>" + ("<br/>" + "Please unselect already locked Entries ...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                        {
                            return Json(new
                            {
                                Message = "Incomplete Entries can't be Locked...!" + ("<br/>" + ("<br/>" + "Please unselect incomplete Entries or ask Center to Complete it...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (!(string.IsNullOrEmpty(xRemarks)))
                        {
                            if ((MaxValue > 0))
                            {
                                return Json(new
                                {
                                    Message = "Entries with pending queries can't be Locked. . . !" + ("<br/>" + ("<br/>" + "Please unselect such Entries...!")),
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                        Ctr++;
                        if (!BASE._AdvanceDBOps.MarkAsLocked(xTemp_ID))
                        {
                            return Json(new
                            {
                                Message = "Error",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }



                        if ((Ctr > 0))
                        {
                            return Json(new
                            {
                                Message = "Locked...",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new
                        {
                            Message = ("Not Allowed..No Rights"),
                            result = "NoLockRights"
                        }, JsonRequestBehavior.AllowGet);

                    }
                }

                else if ((ActionMethod.ToUpper()) == "UNLOCKED")
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Lock_Unlock))
                    {
                        int Ctr = 0;

                        string xTemp_ID = adv.ID;
                        string xType = adv.Type;
                        // Please Note that this is a dependency , in case advance creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                        if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                        {
                            return Json(new
                            {
                                Message = "Entries Created from Vouchers can be Audited from Vouchers Only...!" + ("<br/>" + ("<br/>" + "Please unselect Entries Created from Voucher ...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        Common.Record_Status? MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = (Common.Record_Status)BASE._AdvanceDBOps.GetStatus(xTemp_ID);
                        string xStatus = adv.ActionStatus;
                        var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                        string Msg = "";
                        if ((value != MaxValue))
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                            {
                                AllowUser = true;
                            }

                            if (AllowUser)
                            {
                                return Json(new
                                {
                                    Message = "The Record has been Locked in the background by another user" + ("<br/>" + ("<br/>" + "Do you want to continue...?")),
                                    result = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            Msg = "Information...";
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                        {
                            return Json(new
                            {
                                Message = "Already Unlocked Entries can't be Re-Unlocked...!" + ("<br/>" + ("<br/>" + "Please unselect already unlocked Entries ...!")),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                        {
                            return Json(new
                            {
                                Message = "Incomplete Entries can't be Unlocked...!" + ("<br/>" + ("<br/>" + "Please unselect incomplete Entries or ask Center to Complete it...!")),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // If Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
                        //     If Not Me.GridView1.GetRowCellValue(CurrRowHandle, "Action By").ToString().ToUpper.Equals(Base._open_User_ID.ToUpper) Then
                        //         DevExpress.XtraEditors.XtraMessageBox.Show("R e c o r d s    l o c k e d    b y    o t h e r    u s e r s   c a n   b e   u n l o c k e d   b y   S u p e r U s e r s   O n l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        //     End If
                        // End If



                        Ctr++;
                        if (!BASE._AdvanceDBOps.MarkAsComplete(xTemp_ID))
                        {
                            return Json(new
                            {
                                Message = "Error",
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }



                        if ((Ctr > 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.UnlockedSuccess(Ctr),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new
                        {
                            Message = ("Not Allowed..No Rights"),
                            result = "NoUnLockRights"
                        }, JsonRequestBehavior.AllowGet);

                    }
                }

                else if ((ActionMethod.ToUpper()) == "REMARKS")
                {
                    if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.View_Remarks)))
                    {
                        string xType = adv.Type.ToString();
                        if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                        {
                            return Json(new
                            {
                                Message = "Entries Created from Vouchers can be Audited from Vouchers Only...!" + ("<br/>" + ("<br/>" + "Please unselect Entries Created from Voucher ...!")),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string xTemp_ID = adv.ID.ToString();
                        //xID = xTemp_ID;
                        string xStatus = adv.ActionStatus.ToString();
                        //View_Actions(xTemp_ID, Common_Lib.RealTimeService.Tables.ADVANCES_INFO, Common_Lib.RealTimeService.ClientScreen.Profile_Advances, xStatus, this);
                    }
                }

                else if ((ActionMethod.ToUpper()) == "ADD REMARKS")
                {
                    if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Manage_Remarks)))
                    {
                        string xType = adv.Type.ToString();
                        if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                        {
                            return Json(new
                            {
                                Message = "Entries Created from Vouchers can be Audited from Vouchers Only...!" + ("<br/>" + ("<br/>" + "Please unselect Entries Created from Voucher ...!")),
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string xTemp_ID = adv.ID.ToString();
                        //xID = xTemp_ID;
                        string xStatus = adv.ActionStatus.ToString();
                        if ((xStatus.ToUpper() != "LOCKED"))
                        {
                            //Add_Actions(xTemp_ID, Common_Lib.RealTimeService.Tables.ADVANCES_INFO, Common_Lib.RealTimeService.ClientScreen.Profile_Advances, this);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = "Queries Can't be added to Freezed Records.",
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = "Something went wrong!!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region "Start--> LookupEdit Events"
        [HttpGet]
        public ActionResult LookUp_GetItemList(DataSourceLoadOptions loadOptions)
        {
            DataTable ItemList = BASE._AdvanceDBOps.GetOpeningProfileAdvanceItems("ID", "Name") as DataTable;
            var Itemdata = DatatableToModel.DataTabletoItem_INFO(ItemList);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Itemdata, loadOptions)), "application/json");
        }

        //public ActionResult LookUp_GetPartyList_Advances(bool? IsVisible)
        //{
        //    DataTable d1 = BASE._AdvanceDBOps.GetParties();
        //    if ((d1 == null))
        //    {
        //        return null;
        //    }

        //    var Partydata = DatatableToModel.DataTabletoAdvancesPartyList(d1);
        //    ViewData["AdvancesPartyList"] = Partydata;

        //    return PartialView(new DropdownDataReadonlyViewmodel { IsReadOnly = IsVisible });
        //}

        [HttpGet]
        public ActionResult LookUp_GetPartyList_Advances(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._AdvanceDBOps.GetParties();

            DataView dview = new DataView(d1);

            dview.Sort = "NAME";

            var data = DatatableToModel.DataTabletoAdvancesPartyList(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        #endregion

        #region Create detail

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
                    Lbl_Status_Color = "blue";
                    Pic_Status = "Fa Fa-Lock";
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
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
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
                else {
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

        #region Export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Advances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Advances_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion

        public void SessionClear()
        {
            ClearBaseSession("_Advances");
            Session.Remove("AdvancesInfo_detailGrid_Data");
        }
        public void Advances_user_rights()
        {
            ViewData["Advances_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Advances, "Add");
            ViewData["Advances_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Advances, "Update");
            ViewData["Advances_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Advances, "View");
            ViewData["Advances_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Advances, "Delete");
            ViewData["Advances_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Advances, "Export");            
            ViewData["Advances_AdresBookListRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");            
            ViewData["Advances_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_Advances, Common.ClientAction.Lock_Unlock);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
        #region Dev Extreme
        public ActionResult Frm_Advances_Info_dx()
        {
            Advances_user_rights();
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.IsAuditor = true;
            }
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.GroupShowFooter = false;
            }
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Manage_Remarks))
            {
                ViewBag.AddRemarks = false;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Advances, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }
            SetDefaultValues();
            if (!CheckRights(BASE, ClientScreen.Profile_Advances, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Advances').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Advances).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["AdvancesInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View();
        }
        [HttpGet]
        public ActionResult Frm_Advances_Info_Grid_dx()
        {
             Common_Lib.RealTimeService.Param_GetAdvProfileListing AdvProfile = new Common_Lib.RealTimeService.Param_GetAdvProfileListing();
                AdvProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                AdvProfile.Next_YearID = BASE._next_Unaudited_YearID;
                DataTable AI_Table = BASE._AdvanceDBOps.GetProfileListing(AdvProfile);
                DateTime? dtnull = null;

                // BUILD DATA
                List<AdvancesProfile> BuildData = new List<AdvancesProfile>();
                foreach (DataRow row in AI_Table.Rows)
                {
                    AdvancesProfile newdata = new AdvancesProfile();

                    newdata.ITEM_NAME = row["ITEM_NAME"].ToString();
                    newdata.AI_ITEM_ID = row["AI_ITEM_ID"].ToString();
                    newdata.AI_PARTY_ID = row["AI_PARTY_ID"].ToString();
                    newdata.PARTY_NAME = row["PARTY_NAME"].ToString();
                    newdata.AI_ADV_DATE = Convert.IsDBNull(row["AI_ADV_DATE"]) ? dtnull : Convert.ToDateTime(row["AI_ADV_DATE"]);
                    newdata.Advance = (decimal)row["Advance"];
                    newdata.Addition = (decimal)row["Addition"];
                    newdata.Adjusted = (decimal)row["Adjusted"];
                    newdata.Refund = (decimal)row["Refund"];
                    newdata.OutStanding = (decimal)row["Out-Standing"];
                    newdata.Reason = row["Reason"].ToString();
                    newdata.AI_OTHER_DETAIL = row["AI_OTHER_DETAIL"].ToString();
                    newdata.TR_ID = row["TR_ID"].ToString();
                    newdata.YearID = (Int32)row["YearID"];
                    newdata.ID = row["ID"].ToString();
                    newdata.Type = row["Type"].ToString();
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
                    newdata.ActionDate = Convert.IsDBNull(row["Action Date"]) ? dtnull : Convert.ToDateTime(row["Action Date"]);
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
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
                var fdata = BuildData.ToList();
                Advances_ExportData = fdata;
            

            var Final_Data = Advances_ExportData as List<AdvancesProfile>;

            return Content(JsonConvert.SerializeObject(Final_Data), "application/json");

        }
        public ActionResult Frm_Advances_Info_DetailGrid_dx( bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_Advances, !VouchingMode)), "application/json");
        }
        public ActionResult AdditionalInfo_Grid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_Advances)), "application/json");

        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Advances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Advances_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }

        #endregion
    }
}