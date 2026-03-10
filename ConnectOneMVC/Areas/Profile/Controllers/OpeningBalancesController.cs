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
using System.Collections;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class OpeningBalancesController : BaseController
    {
        // GET: Profile/OpeningBalances    
        #region Global Variables
        public List<OpeningBalances> OpeningBalances_ExportData
        {
            get
            {
                return (List<OpeningBalances>)GetBaseSession("OpeningBalances_ExportData_OpeningBalances");
            }
            set
            {
                SetBaseSession("OpeningBalances_ExportData_OpeningBalances", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> OpeningBalancesInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("OpeningBalancesInfo_DetailGrid_Data_OpeningBalances");
            }
            set
            {
                SetBaseSession("OpeningBalancesInfo_DetailGrid_Data_OpeningBalances", value);
            }
        }

        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> OpeningBalancesInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("OpeningBalancesInfo_AdditionalInfoGrid_OpeningBalances");
            }
            set
            {
                SetBaseSession("OpeningBalancesInfo_AdditionalInfoGrid_OpeningBalances", value);
            }
        }

        #endregion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_OpeningBalances_Window(string ActionMethod = null, string ID = null, string Head = null, string HeadType = null, string EditedOn = null)
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_OpeningBalances, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_OpeningBalances_Window','Not Allowed','No Rights');</script>");
                }
            }
            
            OpeningBalances OBacc = new OpeningBalances();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            DateTime? EditDate = Convert.ToDateTime(EditedOn);
            OBacc.ActionMethod = Navigation_Mode_tag;
            OBacc.TempActionMethod = ActionMethod;
            OBacc.Type = "Debit";
            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                       || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                OBacc.Edit_Date = Convert.ToDateTime(EditedOn);

                DataTable _dtableTelData = BASE._OpeningBalances_DBOps.GetRecord(ID);
                if (_dtableTelData.Rows.Count > 0)
                {
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
                                    ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current OpeningBalances", viewstr));  //need to change
                                }
                            }

                        }

                    }

                    // -----------------------------+
                    // End : Check if entry already changed 
                    //-----------------------------+
                    OBacc.ItemName = _dtableTelData.Rows[0]["OP_ITEM_ID"].ToString();
                    OBacc.Head = Head;
                    OBacc.HeadType = HeadType;
                    OBacc.Amount = _dtableTelData.Rows[0]["OP_AMOUNT"].ToString();
                    OBacc.Type = _dtableTelData.Rows[0]["OP_DEBIT_CREDIT"].ToString();
                    OBacc.other_DetField = _dtableTelData.Rows[0]["OP_REMARKS"].ToString();
                    OBacc.ID = _dtableTelData.Rows[0]["OP_ITEM_ID"].ToString();
                    OBacc.OldItemName= _dtableTelData.Rows[0]["OP_ITEM_ID"].ToString();
                }
            }
            return View(OBacc);
        }
        [HttpPost]
        public ActionResult Frm_OpeningBalances_Window(OpeningBalances OBacc)
        {
            OBacc.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod);

            if (((OBacc.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || OBacc.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
            {

                // Checking Duplicate Record....
                int? MaxValue = null;
                int xID = 0;
                var MaxValuestring = (BASE._OpeningBalances_DBOps.GetDuplicateCount(OBacc.ItemName));
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

                if (((int)MaxValue != 0) && (OBacc.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                {
                    return Json(new
                    {
                        Message = "Same item Already Available...!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else if ((((int)MaxValue != 0) && (OBacc.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                {
                    if (OBacc.OldItemName != OBacc.ItemName)
                    {
                        return Json(new
                        {
                            Message = "Same item Already Available...!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {

            }


            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
            {
                // new
                bool Result = true;


                OBacc.ID = System.Guid.NewGuid().ToString();
                // Insert opening balance 
                Common_Lib.RealTimeService.Parameter_Insert_OpeningBalances InParam = new Common_Lib.RealTimeService.Parameter_Insert_OpeningBalances();
                OBacc.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);

                InParam.ItemID = OBacc.ItemName;
                InParam.Amount =Convert.ToDouble(OBacc.Amount);
                InParam.DebitCredit = OBacc.Type;
                InParam.OtherDetails = OBacc.other_DetField;
                InParam.Status_Action = OBacc.status_Action.ToString();

                if (!BASE._OpeningBalances_DBOps.Insert(InParam))
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
            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
            {
                //Edit
                bool Result = true;


                //  OBacc.ID = System.Guid.NewGuid().ToString();
                // Insert opening balance 
                Common_Lib.RealTimeService.Parameter_Update_OpeningBalances UpParam = new Common_Lib.RealTimeService.Parameter_Update_OpeningBalances();

                UpParam.ItemID = OBacc.ItemName;
                UpParam.Amount = Convert.ToDouble(OBacc.Amount);
                UpParam.DebitCredit = OBacc.Type;
                UpParam.OtherDetails = OBacc.other_DetField;
                UpParam.Rec_ID = OBacc.ID;


                if (!BASE._OpeningBalances_DBOps.Update(UpParam))
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
            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
            {
                bool Result = true;

                if (!BASE._OpeningBalances_DBOps.Delete(OBacc.ID))
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
        public ActionResult Frm_OpeningBalances_Info()
        {
            OpeningBalance_user_rights();
            if (!(CheckRights(BASE,ClientScreen.Profile_OpeningBalances,"List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_OpeningBalances').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_OpeningBalances).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            DataTable TP_Table = BASE._OpeningBalances_DBOps.GetList();
            List<OpeningBalances> BuildData = new List<OpeningBalances>();
            // BUILD DATA
            foreach(DataRow row in TP_Table.Rows)
            {
                OpeningBalances newRow = new OpeningBalances();
                newRow.ItemName = row["Item Name"].ToString();
                newRow.Head = row["Head"].ToString();
                newRow.HeadType = row["Head Type"].ToString();
                newRow.other_DetField = row["Other Details"].ToString();
                newRow.Type = row["Type"].ToString();
                newRow.Amount = row["Amount"].ToString();
                newRow.ID = row["ID"].ToString();

                newRow.Add_By = row["Add By"].ToString();
                newRow.Add_Date = string.IsNullOrEmpty(row["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Add Date"]);
                newRow.Edit_By = row["Edit By"].ToString();
                newRow.Edit_Date = string.IsNullOrEmpty(row["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Edit Date"]);
                newRow.Action_Status = row["Action Status"].ToString();
                newRow.Action_By = row["Action By"].ToString();
                newRow.Action_Date = string.IsNullOrEmpty(row["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Action Date"]);
                newRow.TR_ID = row["ITEM_ID"].ToString();

                newRow.Remarks = string.IsNullOrEmpty(row["RemarkCount"].ToString()) ? 0 : Convert.ToInt16(row["RemarkCount"]);
                newRow.RemarkStatus = row["RemarkStatus"].ToString();
                newRow.OpenActions = string.IsNullOrEmpty(row["OpenActions"].ToString()) ? 0 : Convert.ToInt16(row["OpenActions"]);
                newRow.CrossedTimeLimit = string.IsNullOrEmpty(row["CrossedTimeLimit"].ToString()) ? 0 : Convert.ToInt16(row["CrossedTimeLimit"]);
                newRow.YearID = string.IsNullOrEmpty(row["YearID"].ToString()) ? 0 : Convert.ToInt16(row["YearID"]);
                newRow.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                newRow.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                newRow.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                newRow.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                newRow.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                newRow.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                newRow.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                newRow.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                newRow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                newRow.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                newRow.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                newRow.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                newRow.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                newRow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                newRow.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                newRow.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                newRow.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                newRow.iIcon = "";
                if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newRow.iIcon += "RedShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newRow.iIcon += "GreenShield|";
                }
                else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newRow.iIcon += "YellowShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newRow.iIcon += "BlueShield|";
                }
                if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newRow.iIcon += "RedFlag|";
                }
                if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newRow.iIcon += "RequiredAttachment|";
                }
                else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newRow.iIcon += "AdditionalAttachment|";
                }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAccepted|"; }
                if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingReject|"; }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "VouchingPartial|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAccepted|"; }
                if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditReject|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "AuditPartial|"; }
                if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newRow.iIcon += "AutoVouching|"; }
                if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newRow.iIcon += "CorrectedEntry|"; }
                BuildData.Add(newRow);
            }
            //BuildData = (from DataRow T in TP_Table.AsEnumerable()
            //             select new OpeningBalances
            //             {
            //                 ItemName = T["Item Name"].ToString(),
            //                 Head = T["Head"].ToString(),
            //                 HeadType = T["Head Type"].ToString(),
            //                 other_DetField = T["Other Details"].ToString(),
            //                 Type = T["Type"].ToString(),
            //                 Amount = T["Amount"].ToString(),
            //                 ID = T["ID"].ToString(),

            //                 Add_By = T["Add By"].ToString(),
            //                 Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
            //                 Edit_By = T["Edit By"].ToString(),
            //                 Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
            //                 Action_Status = T["Action Status"].ToString(),
            //                 Action_By = T["Action By"].ToString(),
            //                 Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),
            //                 TR_ID = T["ITEM_ID"].ToString(),

            //                 Remarks = string.IsNullOrEmpty(T["RemarkCount"].ToString()) ? 0 : Convert.ToInt16(T["RemarkCount"]),
            //                 RemarkStatus = T["RemarkStatus"].ToString(),
            //                 OpenActions = string.IsNullOrEmpty(T["OpenActions"].ToString()) ? 0 : Convert.ToInt16(T["OpenActions"]),
            //                 CrossedTimeLimit = string.IsNullOrEmpty(T["CrossedTimeLimit"].ToString()) ? 0 : Convert.ToInt16(T["CrossedTimeLimit"]),
            //                 YearID = string.IsNullOrEmpty(T["YearID"].ToString()) ? 0 : Convert.ToInt16(T["YearID"]),
            //                 REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),
            //                 COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),
            //                 RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),
            //                 REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),
            //                 OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
            //                 ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),

            //             }).ToList();
            var Final_Data = BuildData.ToList();
            OpeningBalances_ExportData = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["OpeningBalances_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Final_Data);
        }
        public ActionResult Frm_OpeningBalances_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            OpeningBalance_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (OpeningBalances_ExportData == null || command == "REFRESH")
            {
                DataTable TP_Table = BASE._OpeningBalances_DBOps.GetList();
                if (((TP_Table == null)))
                {
                    return PartialView("Frm_OpeningBalances_Info_Grid", null);
                }
                List<OpeningBalances> BuildData = new List<OpeningBalances>();
                // BUILD DATA
                foreach (DataRow row in TP_Table.Rows)
                {
                    OpeningBalances newRow = new OpeningBalances();
                    newRow.ItemName = row["Item Name"].ToString();
                    newRow.Head = row["Head"].ToString();
                    newRow.HeadType = row["Head Type"].ToString();
                    newRow.other_DetField = row["Other Details"].ToString();
                    newRow.Type = row["Type"].ToString();
                    newRow.Amount = row["Amount"].ToString();
                    newRow.ID = row["ID"].ToString();

                    newRow.Add_By = row["Add By"].ToString();
                    newRow.Add_Date = string.IsNullOrEmpty(row["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Add Date"]);
                    newRow.Edit_By = row["Edit By"].ToString();
                    newRow.Edit_Date = string.IsNullOrEmpty(row["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Edit Date"]);
                    newRow.Action_Status = row["Action Status"].ToString();
                    newRow.Action_By = row["Action By"].ToString();
                    newRow.Action_Date = string.IsNullOrEmpty(row["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["Action Date"]);
                    newRow.TR_ID = row["ITEM_ID"].ToString();

                    newRow.Remarks = string.IsNullOrEmpty(row["RemarkCount"].ToString()) ? 0 : Convert.ToInt16(row["RemarkCount"]);
                    newRow.RemarkStatus = row["RemarkStatus"].ToString();
                    newRow.OpenActions = string.IsNullOrEmpty(row["OpenActions"].ToString()) ? 0 : Convert.ToInt16(row["OpenActions"]);
                    newRow.CrossedTimeLimit = string.IsNullOrEmpty(row["CrossedTimeLimit"].ToString()) ? 0 : Convert.ToInt16(row["CrossedTimeLimit"]);
                    newRow.YearID = string.IsNullOrEmpty(row["YearID"].ToString()) ? 0 : Convert.ToInt16(row["YearID"]);
                    newRow.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newRow.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newRow.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newRow.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                    newRow.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                    newRow.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                    newRow.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newRow.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newRow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newRow.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newRow.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newRow.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newRow.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newRow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newRow.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newRow.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newRow.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                    newRow.iIcon = "";
                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newRow.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newRow.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newRow.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newRow.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newRow.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newRow.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newRow.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newRow.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newRow.iIcon += "CorrectedEntry|"; }
                    BuildData.Add(newRow);
                }

                //BuildData = (from DataRow T in TP_Table.AsEnumerable()
                //             select new OpeningBalances
                //             {
                //                 ItemName = T["Item Name"].ToString(),
                //                 Head = T["Head"].ToString(),
                //                 HeadType = T["Head Type"].ToString(),
                //                 other_DetField = T["Other Details"].ToString(),
                //                 Type = T["Type"].ToString(),
                //                 Amount = T["Amount"].ToString(),
                //                 ID = T["ID"].ToString(),

                //                 Add_By = T["Add By"].ToString(),
                //                 Add_Date = string.IsNullOrEmpty(T["Add Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Add Date"]),
                //                 Edit_By = T["Edit By"].ToString(),
                //                 Edit_Date = string.IsNullOrEmpty(T["Edit Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Edit Date"]),
                //                 Action_Status = T["Action Status"].ToString(),
                //                 Action_By = T["Action By"].ToString(),
                //                 Action_Date = string.IsNullOrEmpty(T["Action Date"].ToString()) ? DateTime.Now : Convert.ToDateTime(T["Action Date"]),
                //                 TR_ID = T["ITEM_ID"].ToString(),

                //                 Remarks = string.IsNullOrEmpty(T["RemarkCount"].ToString())?0: Convert.ToInt16(T["RemarkCount"]),
                //                 RemarkStatus = T["RemarkStatus"].ToString(),
                //                 OpenActions = string.IsNullOrEmpty(T["OpenActions"].ToString()) ? 0 : Convert.ToInt16(T["OpenActions"]),
                //                 CrossedTimeLimit = string.IsNullOrEmpty(T["CrossedTimeLimit"].ToString()) ? 0 : Convert.ToInt16(T["CrossedTimeLimit"]),
                //                 YearID = string.IsNullOrEmpty(T["YearID"].ToString()) ? 0 : Convert.ToInt16(T["YearID"]),
                //                 REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),
                //                 COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),
                //                 RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),
                //                 REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),
                //                 OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
                //                 ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),

                //             }).ToList();
                var fdata = BuildData.ToList();
                OpeningBalances_ExportData = fdata;
            }
            var Final_Data = OpeningBalances_ExportData as List<OpeningBalances>;
            //ViewData["newKey"] = Final_Data.OrderByDescending(a => a.ID).FirstOrDefault().ID;
            return View(OpeningBalances_ExportData);
        }
        #region <--Nested grid-->
        public ActionResult Frm_OpeningBalances_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.OpeningBalancesInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.OpeningBalancesInfo_RecID = RecID;
            ViewBag.OpeningBalancesInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    OpeningBalancesInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_OpeningBalances);                    
                    Session["OpeningBalancesInfo_detailGrid_Data"] = OpeningBalancesInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_OpeningBalances);
                    OpeningBalancesInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    OpeningBalancesInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(OpeningBalancesInfo_DetailGrid_Data);

        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(OpeningBalancesInfo_AdditionalInfoGrid);
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
            settings.Name = "OpeningbalancesListGrid" + RecID;
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "OpeningbalancesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["OpeningBalancesInfo_detailGrid_Data"];
        }
        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Openingbalances_report_modal','Not Allowed','No Rights');$('#OpeningBalancesModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        public JsonResult DataNavigation(string ActionMethod, string ID, string Edit_Date =null, string YearID=null, string Action_Status=null)
        {
            try
            {
                // ------------------------------------------------------

                OpeningBalances model = new OpeningBalances();
                // var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                //  model.ActionMethod = Tag;
                if (BASE.AllowMultiuser())
                {
                    if (((ActionMethod == "LOCKED") || ((ActionMethod == "UNLOCKED"))))
                    {
                        DataTable d1 = BASE._OpeningBalances_DBOps.GetRecord(ID);
                        if ((d1 == null))
                        {
                            return Json(new
                            {
                                Message = "E n t r y   N o t   F o u n d  /  C h a n g e d   I n   B a c k g r o u n d . . . !",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((d1.Rows.Count == 0))
                        {
                            return Json(new
                            {
                                //Message = "Record Changed / Removed in Background!!",
                                Message = (Common_Lib.Messages.RecordChanged("Current") + "Record Changed / Removed in Background!!"),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }


                        DateTime RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);

                        string EditDate = Edit_Date.Split('+')[0];

                        //var dt = DateTime.ParseExact(EditDate,"ddd MMM dd yyyy HH':'mm':'ss 'GMT'",System.Globalization.CultureInfo.InvariantCulture);

                        //if ((RecEdit_Date != dt))
                        if ((RecEdit_Date.ToString() != Edit_Date))
                        {
                            return Json(new
                            {
                                Message = "Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (ActionMethod == "LOCKED")
                {
                    string xid = "";

                    if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_OpeningBalances, Common_Lib.Common.ClientAction.Lock_Unlock)))
                    {
                        int Ctr = 0;
                        if (ID != null)
                        {
                            string xTemp_ID = ID;
                            xid = xTemp_ID;
                            string xTemp_Year = YearID;
                            Common.Record_Status? MaxValue = 0;
                            bool AllowUser = false;
                            MaxValue = (Common.Record_Status)BASE._OpeningBalances_DBOps.GetStatus(xTemp_ID);
                            string xStatus = Action_Status;
                            var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                            var xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.OPENING_BALANCES_INFO, xTemp_ID);
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
                                        Message = "Record has been Unlocked in the background by another user.",
                                        result = false
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
                                    Message = "Already Locked Entries can\'t be Re-Locked...!" + "<br/> <br/>" + "Please unselect already locked Entries ...!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if ((MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                            {
                                return Json(new
                                {
                                    Message = "Incomplete Entries can\'t be Locked...!" + "<br/> <br/>" + "Please unselect incomplete Entries or ask Center to Complete it...!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                            //if ((!(xRemarks == null)))
                            if ((!(xRemarks == null) && !Convert.IsDBNull(xRemarks)))
                            {
                                if ((Convert.ToInt16(MaxValue) > 0))
                                {
                                    return Json(new
                                    {
                                        Message = "Entries with pending queries can\'t be Locked...!" + "<br/> <br/>" + "Please unselect such Entries...!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        if (ID != null)
                        {
                            string xTemp_ID = ID;

                            Ctr++;
                            if (!BASE._OpeningBalances_DBOps.MarkAsLocked(xTemp_ID))
                            {
                                return Json(new
                                {
                                    Message = Common_Lib.Messages.SomeError + "Error!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }

                        if ((Ctr > 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.LockedSuccess(Ctr) + "Locked...",
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
                if (ActionMethod == "UNLOCKED")
                {
                    if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_OpeningBalances, Common_Lib.Common.ClientAction.Lock_Unlock)))
                    {
                        int Ctr = 0;
                        if (ID != null)
                        {
                            string xTemp_ID = ID;
                            string xStatus = Action_Status;

                            var value = (Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                            Common.Record_Status? MaxValue = 0;
                            MaxValue = (Common.Record_Status)BASE._OpeningBalances_DBOps.GetStatus(xTemp_ID);
                            bool AllowUser = false;
                            string Msg = "";
                            if ((value != MaxValue))
                            {
                                Msg = "Record Status has been changed in the background by another user";
                                if ((value.ToString() == Common_Lib.Common.Record_Status._Locked.ToString()))
                                {
                                    AllowUser = true;
                                }

                                if (AllowUser)
                                {
                                    //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                                    return Json(new
                                    {
                                        Message = "The Record has been Locked in the background by another user.",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                Msg = "Information...";
                            }

                            if ((value.ToString() == Common_Lib.Common.Record_Status._Completed.ToString()))
                            {
                                return Json(new
                                {
                                    Message = "Already Unlocked Entries can\'t be Re-Unlocked...!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if ((value.ToString() == Common_Lib.Common.Record_Status._Incomplete.ToString()))
                            {
                                return Json(new
                                {
                                    Message = "In complete Entries can\'t be Unlocked...!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (ID != null)
                        {
                            string xTemp_ID = ID;
                            string xTemp_Year = YearID;

                            Ctr++;
                            if (!BASE._OpeningBalances_DBOps.MarkAsComplete(xTemp_ID))
                            {
                                return Json(new
                                {
                                    Message = "Error!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if ((Ctr > 0))
                        {
                            return Json(new
                            {
                                Message = "1 Entries Unlocked Successfully!!",
                                result = false
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
                // ---------------------------------------------------------
                if (ActionMethod == "New")
                {
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "Edit")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._OpeningBalances_DBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background... !", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new { Message = ("Locked Entry can not be Edited / Deleted... !" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))), result = false }, JsonRequestBehavior.AllowGet);
                    }
                    model.ID = ID;
                    model.Edit_Date = Convert.ToDateTime(Edit_Date);
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed))
                    {
                        model.Chk_Incompleted = false;
                    }
                    else
                    {
                        model.Chk_Incompleted = true;
                    }
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);

                }
                else if (ActionMethod == "Delete")
                {

                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._OpeningBalances_DBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background...!", result = false }, JsonRequestBehavior.AllowGet);
                    }

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new { Message = ("Locked Entry can not be  Edit/ Delete...!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))), result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "View")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._OpeningBalances_DBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OBCustomDataAction(string key)
        {
            var FinalData = OpeningBalances_ExportData as List<OpeningBalances>;
            var FDData = (OpeningBalances)FinalData.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (FDData != null)
            {
                itstr = FDData.ItemName + "![" + FDData.Head + "![" + FDData.HeadType + "![" + FDData.Amount + "![" + FDData.Type + "![" +
                            FDData.other_DetField + "![" + FDData.Add_By + "![" + FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" +
                             FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date + "![" + FDData.TR_ID + "![" + FDData.ID + "!["  + FDData.Remarks + "![" + FDData.RemarkStatus + "![" +
                            FDData.OpenActions + "![" + FDData.CrossedTimeLimit + "![" + FDData.YearID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        #region "Start--> LookupEdit Events"

        public ActionResult LookUp_GetOpeningBalancesList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable OBlist = BASE._OpeningBalances_DBOps.GetOpeningBalancesItems() as DataTable;
            var bankdata = DatatableToModel.DataTabletoOB_INFO(OBlist);
            //ViewData["BankList"] = bankdata;
            //return PartialView(new DropdownDataReadonlyViewmodel { IsReadOnly = IsVisible });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(bankdata, loadOptions)), "application/json");
        }
        #endregion "end--> LookupEdit Events"
        public void SessionClear()
        {
            ClearBaseSession("_OpeningBalances");
            Session.Remove("OpeningBalancesInfo_detailGrid_Data");
        }
        public void OpeningBalance_user_rights()
        {
            ViewData["OpenBalance_AddRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "Add");
            ViewData["OpenBalance_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "Update");
            ViewData["OpenBalance_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "View");
            ViewData["OpenBalance_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "Delete");
            ViewData["OpenBalance_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "Export");
            ViewData["OpenBalance_ListRight"] = CheckRights(BASE, ClientScreen.Profile_OpeningBalances, "List");
            ViewData["OpenBalance_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_OpeningBalances, Common.ClientAction.Lock_Unlock);
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
    }
}