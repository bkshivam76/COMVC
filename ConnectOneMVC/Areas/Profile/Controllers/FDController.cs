using Common_Lib;
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
using Common_Lib.RealTimeService;
using System.Web.UI.WebControls;
namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class FDController : BaseController
    {
        // GET: Profile/FD
        #region Global Variables        
        public List<FD_Info> FD_ExportData
        {
            get
            {
                return (List<FD_Info>)GetBaseSession("FD_ExportData_FD");
            }
            set
            {
                SetBaseSession("FD_ExportData_FD", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> FD_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("FD_AdditionalInfoGrid_FD");
            }
            set
            {
                SetBaseSession("FD_AdditionalInfoGrid_FD", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> FDInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("FDInfo_DetailGrid_Data_FD");
            }
            set
            {
                SetBaseSession("FDInfo_DetailGrid_Data_FD", value);
            }
        }
        #endregion
        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/FD)
        public ActionResult Frm_FD_Info()
        {
            FD_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Profile_FD, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_FD').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_FD).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            FD model = new FD();

            model.RowFlag1 = false;
            GetGridData();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["FDList_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                //this.GridView1.Columns("Entry_Type").Group[];
                //this.GridView1.Columns("Action_Status").Group[];
                //this.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            }
            else
            {
                //this.GridView1.Columns("Action_Status").Visible = false;
                //this.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Manage_Remarks))
            {
                //this.T_ADD_REMARKS.Visible = false;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }

            return View(FD_ExportData);
        }
        public ActionResult Frm_FD_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            FD_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            FD model = new FD();

            model.RowFlag1 = false;
            if (FD_ExportData == null || command == "REFRESH")
            {
                GetGridData();
            }
            //this.GridView1.Columns("OpenActions").Visible = false;
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                //this.GridView1.Columns("Entry_Type").Group[];
                //this.GridView1.Columns("Action_Status").Group[];
                //this.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            }
            else
            {
                //this.GridView1.Columns("Action_Status").Visible = false;
                //this.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Manage_Remarks))
            {
                //this.T_ADD_REMARKS.Visible = false;
            }

            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }

            return View(FD_ExportData);
        }
        #region nested grid
        public ActionResult Frm_FD_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.FDInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.FDInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_FD);
                    FDInfo_DetailGrid_Data = _docList;
                    Session["FDInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_FD);
                    FDInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["FDInfo_detailGrid_Data"] = data.DocumentMapping;
                    FD_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }     
            return PartialView(FDInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(FD_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode)
        {
            ViewBag.ID = ID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public void GetGridData()
        {
            DataTable Final_Data = BASE._FDDBOps.GetProfileList();
            List<FD_Info> FD = new List<FD_Info>();
            foreach (DataRow row in Final_Data.Rows)
            {
                FD_Info newRow = new FD_Info();
                newRow.BI_BANK_NAME = row["BI_BANK_NAME"].ToString();
                newRow.BB_BRANCH_NAME = row["BB_BRANCH_NAME"].ToString();
                newRow.FD_NO = row["FD_NO"].ToString();
                newRow.FD_DATE = Convert.ToDateTime(row["FD_DATE"]);
                newRow.FD_AS_DATE = Convert.ToDateTime(row["FD_AS_DATE"]);
                newRow.FD_AMOUNT = Convert.ToDecimal(row["FD_AMOUNT"]);
                newRow.FD_INT_RATE = Convert.ToDecimal(row["FD_INT_RATE"]);
                newRow.FD_INT_PAY_COND = row["FD_INT_PAY_COND"].ToString();
                newRow.FD_MAT_DATE = Convert.ToDateTime(row["FD_MAT_DATE"]);
                newRow.FD_MAT_AMT = Convert.ToDecimal(row["FD_MAT_AMT"]);
                newRow.BA_CUST_NO = row["BA_CUST_NO"].ToString();
                newRow.Add_By = row["Add_By"].ToString();
                newRow.Add_Date = Convert.ToDateTime(row["Add_Date"]);
                newRow.Edit_By = row["Edit_By"].ToString();
                newRow.Edit_Date = Convert.ToDateTime(row["Edit_Date"]);
                newRow.Action_Status = row["Action_Status"].ToString();
                newRow.Action_By = row["Action_By"].ToString();
                newRow.Action_Date = Convert.ToDateTime(row["Action_Date"]);
                newRow.TR_ID = row["TR_ID"].ToString();
                newRow.ID = row["ID"].ToString();
                newRow.FD_Status = row["FD_Status"].ToString();
                //newRow.CLOSE_DATE = Convert.ToDateTime(row["CLOSE_DATE"]) ;
                newRow.CLOSE_DATE = string.IsNullOrEmpty(row["CLOSE_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(row["CLOSE_DATE"]);
                newRow.Entry_Type = row["Entry_Type"].ToString();
                //newRow.Interest_Recd = Convert.ToDecimal(row["Interest_Recd"]);
                newRow.Interest_Recd = string.IsNullOrEmpty(row["Interest_Recd"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["Interest_Recd"]);
                //newRow.TDS_Paid = Convert.ToDecimal(row["TDS_Paid"]);
                newRow.TDS_Paid = string.IsNullOrEmpty(row["TDS_Paid"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["TDS_Paid"]);
                //newRow.Nett_Interest = Convert.ToDecimal(row["Nett_Interest"]);
                newRow.Nett_Interest = string.IsNullOrEmpty(row["Nett_Interest"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["Nett_Interest"]);
                newRow.Other_Detail = row["Other Detail"].ToString();
                newRow.FD_Less_Maturity = Convert.ToDecimal(row["FD_Less_Maturity"]);
                newRow.Remarks = Convert.ToInt16(row["Remarks"]);
                newRow.RemarkStatus = row["RemarkStatus"].ToString();
                newRow.OpenActions = Convert.ToInt16(row["OpenActions"]);
                newRow.CrossedTimeLimit = Convert.ToInt16(row["CrossedTimeLimit"]);
                newRow.YearID = Convert.ToInt16(row["YearID"]);
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
                newRow.iIcon = "";
                newRow.Special_Ref = Convert.IsDBNull(row["Special_Ref"]) ? "" : Convert.ToString(row["Special_Ref"]);
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
                FD.Add(newRow);
            }            
            var FD_FinalData = FD.ToList();
            FD_ExportData = FD_FinalData;
        }
        public ActionResult Refresh_GridIcon_PreviewRow(string TempID, string NestedRowKeyValue)
        {
            GetGridData();
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(TempID, "", ClientScreen.Profile_FD);
            FDInfo_DetailGrid_Data = _docList;
            var AttachmentRow = FDInfo_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History = AttachmentRow.Vouching_History;
            string Main_iIcon = FD_ExportData.Where(x => x.ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History
            }, JsonRequestBehavior.AllowGet);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "FDListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "FDListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["FDInfo_detailGrid_Data"];
        }
        #endregion
        public ActionResult FDCustomDataAction(string key)
        {
            var FinalData = FD_ExportData as List<FD_Info>;
            var FDData = (FD_Info)FinalData.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (FDData != null)
            {
                itstr = FDData.BI_BANK_NAME + "![" + FDData.BB_BRANCH_NAME + "![" + FDData.FD_NO + "![" + FDData.FD_DATE + "![" + FDData.FD_AS_DATE + "![" +
                            FDData.FD_AMOUNT + "![" + FDData.FD_INT_RATE + "![" + FDData.FD_INT_PAY_COND + "![" + FDData.FD_MAT_DATE + "![" + FDData.FD_MAT_AMT + "![" +
                             FDData.BA_CUST_NO + "![" + FDData.Add_By + "![" + FDData.Add_Date + "![" + FDData.Edit_By + "![" + FDData.Edit_Date + "![" +
                             FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date + "![" + FDData.TR_ID + "![" + FDData.ID + "![" +
                            FDData.FD_Status + "![" + FDData.CLOSE_DATE + "![" + FDData.Entry_Type + "![" + FDData.Interest_Recd + "![" + FDData.TDS_Paid + "![" +
                            FDData.Nett_Interest + "![" + FDData.Other_Detail + "![" + FDData.FD_Less_Maturity + "![" + FDData.Remarks + "![" + FDData.RemarkStatus + "![" +
                            FDData.OpenActions + "![" + FDData.CrossedTimeLimit + "![" + FDData.YearID;

            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        #endregion

        #region Add/Edit Bank Account Details for popup
        [HttpGet]
        public ActionResult Frm_FD_Window(string EditedOn, string ActionMethod = null, string id = null, string YearID = null, DateTime? Edit_Date = null)
        {
            FD_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_Deposit, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_Frm_FD_Window','Not Allowed','No Rights');</script>");
                }
            }

            FD model = new FD();
            Common_Lib.Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;
            //Below Tag variable is specifically used for Dropdown
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.Cmd_Type = "ON MATURITY";
            //this.TitleX.Text = "Fixed Deposit (F.D.)";
            model.SubTitleX = ("As on " + ("\r\n" + Convert.ToDateTime(BASE._open_Year_Sdt).AddDays(-1).ToString("dd MMMM, yyyy")));

            model.xID = id;
            model.Info_LastEditedOn = Edit_Date;
            if (((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                        || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Tag) == Common_Lib.Common.Navigation_Mode._View)
            {
                model = Data_Binding(model, Tag);
            }

            if ((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
            {
                // If Not Base._open_Year_ID = YearID Then 'not Current Year Entry (add check for non-carried fwd record in split data) 

                //    bool IsfdCarriedFwd;
                //    BASE._FDDBOps.IsFDCarriedForward(model.xID, YearID);
                //    if ((IsfdCarriedFwd == null))
                //    {
                //        FormClosingEnable = false;
                //        this.Close();
                //    }

                //    if ((IsfdCarriedFwd == true))
                //    {
                //        // Unsplit record wont be opened for editing at all , thus this case runs for split years only 
                //        Look_BankList.Properties.ReadOnly = true;
                //        Txt_No.Properties.ReadOnly = true;
                //        Txt_Date.Properties.ReadOnly = true;
                //        Txt_As_Date.Properties.ReadOnly = true;
                //        Txt_Amount.Properties.ReadOnly = true;
                //    }

                //}

            }
            return View(model);
        }

        public JsonResult DataNavigation(string ActionMethod = null, string id = null, string CLOSE_DATE = null, string YearID = null, string Action_Status = null, string Edit_Date = null, string OpenActions = null, string AccountType = null, string AccountNumber = null, string BranchName = null, string BankName = null, string BA_Customer_Number = null, int ConfirmID = 0, string TR_ID = null, string Entry_Type = null)
        {
            //object xRowPos = this.GridView1.FocusedRowHandle;
            //object xColPos = this.GridView1.FocusedColumn;
            // ------------------------------------------------------
            FD model = new FD();
            if (BASE.AllowMultiuser())
            {
                if (((ActionMethod == "LOCKED") || ((ActionMethod == "UNLOCKED") || (ActionMethod == "PRINT-LIST"))))
                {
                    //if (((this.GridView1.RowCount > 0)
                    //            && (double.Parse(this.GridView1.FocusedRowHandle) >= 0)))
                    //{

                    string xTemp_ID = id;

                    DataTable d1 = BASE._FDDBOps.GetRecord(xTemp_ID);
                    if ((d1 == null))
                    {
                        return Json(new
                        {
                            Message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((d1.Rows.Count == 0))
                    {
                        return Json(new
                        {
                            Message = (Common_Lib.Messages.RecordChanged("Current FD") + "Record Changed / Removed in Background!!"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    object RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    if ((RecEdit_Date.ToString() != Edit_Date))
                    {
                        return Json(new
                        {
                            Message = (Common_Lib.Messages.RecordChanged("Current FD") + "Record Already Changed!!"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //}

                }

            }

            if (ActionMethod == "NEW")
            {
                if ((BASE._Completed_Year_Count > 0))
                {
                    return Json(new
                    {
                        Message = (("Entry Cannot be created. . !" + ("<br/>" + ("<br/>" + "Required Profile Entries have already been created for this centre...!"))) + "Information..."),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                {
                    return Json(new
                    {
                        Message = ("Profile Entries not allowed for a newly opened center" + " Information..."),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            if (ActionMethod == "EDIT")
            {
                //if (((this.GridView1.RowCount > 0) && (double.Parse(this.GridView1.FocusedRowHandle) >= 0)))
                //{
                //int RowHandle = this.GridView1.FocusedRowHandle;
                string xTemp_ID = id;
                //string xTemp_ID = this.GridView1.GetRowCellValue(RowHandle, "ID").ToString();
                //  If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "YearID").ToString() <> Base._open_Year_ID Then  'NOT Current Year Entry (add check for non-carried fwd record in split data) 
                bool? IsFDCarriedFwd = BASE._FDDBOps.IsFDCarriedForward(xTemp_ID, YearID);
                if ((IsFDCarriedFwd == null))
                {
                    //Base.HandleDBError_OnNothingReturned();
                    //return;
                    return Json(new
                    {
                        Message = "No Data Found . . . !",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if (((IsFDCarriedFwd == true) && BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID)))
                {
                    //  in case of split, we allow restricted updations 
                    return Json(new
                    {
                        Message = ("Entry Cannot be edited . . !" + ("<br/>" + ("<br/>" + "This entry has been carried forward from previous year(s). Updations(Partial) can be done only after " + "finalization of previous year accounts....!"))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }

                //object xTr_ID = this.GridView1.GetRowCellValue(RowHandle, "TR_ID");
                var xTr_ID = TR_ID;
                var xCLOSE_DATE = CLOSE_DATE;
                //object xCLOSE_DATE = this.GridView1.GetRowCellValue(RowHandle, "CLOSE_DATE");
                if (!(xCLOSE_DATE == null))
                {
                    if ((xCLOSE_DATE.ToString().Length > 0))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(("F D   A l r e a d y   C l o s e d / R e n e w e d . . . !" + ("\r\n" + ("\r\n" + "Please delete Renewal / Closure entry first ...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return Json(new
                        {
                            Message = ("F D   Already Closed / Renewed . . . !" + ("<br/>" + ("<br/>" + "Please delete Renewal / Closure entry first ...!"))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                if (!(xTr_ID == null))
                {
                    if ((xTr_ID.ToString().Length > 0))
                    {
                        return Json(new
                        {
                            Message = (("Entry Cannot be Edited / Deleted . . !" + ("<br/>" + ("<br/>" + "This Entry Managed by Voucher Entry...!")))) + "Information...",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                string xStatus = Action_Status;
                var value = (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), "_" + xStatus);
                Common.Record_Status? MaxValue = 0;
                bool AllowUser = false;
                MaxValue = (Common_Lib.Common.Record_Status)BASE._FDDBOps.GetStatus(xTemp_ID);
                if ((MaxValue == null))
                {
                    return Json(new
                    {
                        Message = (("Entry Cannot be Edited / Deleted . . . !" + ("<br/>" + ("<br/>" + "This Entry Managed by Voucher Entry...!")))) + "Information...",
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
                        multiUserMsg = ("<br/>" + ("<br/>" + "The Recprdhas been unlocked in the background by a" + " nother user."));
                        AllowUser = true;
                    }
                    else
                    {
                        multiUserMsg = ("<br/>" + ("<br/>" + "Record Status has been changed in the background by another user"));
                        AllowUser = true;
                    }

                    //if (AllowUser)
                    //{
                    //    Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                    //    if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", (multiUserMsg + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                    //    {
                    //        xPromptWindow.Dispose();
                    //    }
                    //    else
                    //    {
                    //        xPromptWindow.Dispose();
                    //        Grid_Display();
                    //        return;
                    //    }

                    //}

                }

                if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                {
                    return Json(new
                    {
                        Message = (("Locked Entry Cannot be Edited / Deleted . . . !" + ((multiUserMsg + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!")))))))))))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((BASE._FDDBOps.GetExpense_IncomeCount(xTemp_ID) == null))
                {
                    return Json(new
                    {
                        Message = (("FD Entry with Interest / TDS Cannot be Edited / Deleted . . . !" + ((multiUserMsg + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Delete all such vouchers to edit / delete this Entry." + "-------"))))))))))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                //Frm_FD_Window_Profile xfrm = new Frm_FD_Window_Profile();
                //xfrm.Text = ("Edit ~ " + this.Text);
                model.Tag = Common_Lib.Common.Navigation_Mode._Edit;
                model.xID = xTemp_ID;
                model.YearID = YearID;
                // -----------------------------+
                // Start : Edit date sent to Check if entry already changed 
                // -----------------------------+
                model.Info_LastEditedOn = Convert.ToDateTime(Edit_Date);
                // -----------------------------+
                // End : Edit date sent to Check if entry already changed 
                // -----------------------------+
                if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                {
                    model.Chk_Incompleted = false;
                }
                else
                {
                    model.Chk_Incompleted = true;
                }

                //xfrm.ShowDialog(this);
                //if (((xfrm.DialogResult == DialogResult.OK)
                //            || (xfrm.DialogResult == DialogResult.Retry)))
                //{
                //    xid = xfrm.xID.Text;
                //    Grid_Display();
                //if (Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock))
                //{
                //    Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                //    if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", "Do you want to <color=red>Lock</color> this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                //    {
                //        xPromptWindow.Dispose();
                //        GridView1.SelectRow(RowHandle);
                //        DataNavigation("LOCKED");
                //    }
                //    else if (!(xPromptWindow == null))
                //    {
                //        xPromptWindow.Dispose();
                //    }

                //}

                //if (!(xfrm == null))
                //{
                //    xfrm.Dispose();
                //}

                //}

                //}
            }

            if (ActionMethod == "DELETE")
            {
                //if (((this.GridView1.RowCount > 0) && (double.Parse(this.GridView1.FocusedRowHandle) >= 0)))
                //{
                string xTemp_ID = id;
                //string xTemp_ID = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "ID").ToString();
                // If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "YearID").ToString() <> Base._open_Year_ID Then  'NOT Current Year Entry (add check for non-carried fwd record in split data) 
                bool? IsFDCarriedFwd = BASE._FDDBOps.IsFDCarriedForward(xTemp_ID, YearID);
                if ((IsFDCarriedFwd == null))
                {
                    //Base.HandleDBError_OnNothingReturned();
                    //return;
                    return Json(new
                    {
                        Message = "No Data Found . . . !",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((IsFDCarriedFwd == true))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(("E n t r y   C a n n o t   b e   d e l e t e d . . !" + ("\r\n" + ("\r\n" + "This entry has been carried forward from previous year(s)...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    return Json(new
                    {
                        Message = "Entry Cannot be deleted . . . !" + ("\r\n" + ("\r\n" + "This entry has been carried forward from previous year(s)...!")) + "Information...",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                var xTr_ID = TR_ID;
                var xCLOSE_DATE = CLOSE_DATE;
                int xOpenActions = Convert.ToInt32(OpenActions);
                if ((xOpenActions > 0))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(("E n t r y   C a n n o t   b e  D e l e t e d. . !" + ("\r\n" + ("\r\n" + "There are open actions / queries posted against it...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    return Json(new
                    {
                        Message = "Entry Cannot be Deleted . . . !" + ("\r\n" + ("\r\n" + "There are open actions / queries posted against it...!")) + "Information...",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if (!(xCLOSE_DATE == null))
                {
                    if ((xCLOSE_DATE.ToString().Length > 0))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(("F D  A l r e a d y  C l o s e d / R e n e w e d . . . !" + ("\r\n" + ("\r\n" + "Please delete Renewal / Closure entry first ...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return Json(new
                        {
                            Message = "FD Already Closed / Renewed . . . !" + ("\r\n" + ("\r\n" + "Please delete Renewal / Closure entry first ...!")) + "Information...",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (!(xTr_ID == null))
                {
                    if ((xTr_ID.ToString().Length > 0))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(("E n t r y   C a n n o t   E d i t   /   D e l e t e . . !" + ("\r\n" + ("\r\n" + "This Entry Managed by Voucher Entry...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return Json(new
                        {
                            Message = "Entry Cannot Edit / Delete . . . !" + ("\r\n" + ("\r\n" + "This Entry Managed by Voucher Entry...!")) + "Information...",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                var IncomeCount = BASE._FDDBOps.GetExpense_IncomeCount(xTemp_ID);
                if ((IncomeCount == null))
                {
                    //Base.HandleDBError_OnNothingReturned();
                    //return;
                    return Json(new
                    {
                        Message = "No Data Found . . . !",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((Convert.ToInt32(IncomeCount) > 0))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(("E n t r y   C a n n  o t   b e   D e l e t e d. . !" + ("\r\n" + ("\r\n" + "Some Expenses / Income has been entered against this FD...! Please delete those entries to delete thi" +
                    //    "s FD."))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    return Json(new
                    {
                        Message = ("Entry Cannot be Deleted . . . !" + ("\r\n" + ("\r\n" + "Some Expenses / Income has been entered against this FD...! Please delete those entries to delete thi" + "s FD."))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                string xStatus = Action_Status;
                var value = (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                Common.Record_Status? MaxValue = 0;
                bool AllowUser = false;

                if ((BASE._FDDBOps.GetStatus(xTemp_ID) == null))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   I n   B a c k g r o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return Json(new
                    {
                        Message = ("Entry Not Found In Background . . . !" + "Information"),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                MaxValue = (Common_Lib.Common.Record_Status)BASE._FDDBOps.GetStatus(xTemp_ID);

                string multiUserMsg = "";
                if ((value != MaxValue))
                {
                    if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been locked in the background by another user."));
                    }
                    else if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                    {
                        multiUserMsg = ("<br/>" + ("<br/>" + "The Record has been unlocked in the background by a" + "nother user."));
                        AllowUser = true;
                    }
                    else
                    {
                        multiUserMsg = ("<br>" + ("<br/>" + "Record Status has been changed in the background by another user"));
                        AllowUser = true;
                    }

                    //if (AllowUser)
                    //{
                    //    Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                    //    if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", (multiUserMsg + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                    //    {
                    //        xPromptWindow.Dispose();
                    //    }
                    //    else
                    //    {
                    //        xPromptWindow.Dispose();
                    //        Grid_Display();
                    //        return;
                    //    }

                    //}

                }

                if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                {
                    return Json(new
                    {
                        Message = ("Locked Entry cannot be Edited / Deleted . . . !" + "Information") + (multiUserMsg + ("\r\n" + ("\r\n" + ("Note:" + ("\r\n" + ("-------" + ("\r\n" + ("Drop your Request to Madhuban for Unlock this Entry," + ("\r\n" + "If you really want to do some action...!"))))))))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                model.xID = xTemp_ID;
                // -----------------------------+
                // Start : Edit date sent to Check if entry already changed 
                // -----------------------------+
                model.Info_LastEditedOn = Convert.ToDateTime(Edit_Date);
                // -----------------------------+
                // End : Edit date sent to Check if entry already changed 
                // -----------------------------+

            }

            if (ActionMethod == "VIEW")
            {


                string xTemp_ID = id;
                Common.Record_Status? MaxValue = 0;
                MaxValue = (Common_Lib.Common.Record_Status)BASE._FDDBOps.GetStatus(xTemp_ID);
                if ((BASE._FDDBOps.GetStatus(xTemp_ID) == null))
                {
                    return Json(new
                    {
                        Message = ("Entry Not Found / Changed In Backgroud !" + "Information"),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }



                model.xID = xTemp_ID;
                // -----------------------------+
                // Start : Edit date sent to Check if entry already changed 
                // -----------------------------+
                model.Info_LastEditedOn = Convert.ToDateTime(Edit_Date);
                // -----------------------------+
                // End : Edit date sent to Check if entry already changed 
                // -----------------------------+


            }

            if (ActionMethod == "LOCKED")
            {
                string xid = "";
                if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock)))
                {
                    int Ctr = 0;
                    //foreach (int CurrRowHandle in this.GridView1.GetSelectedRows)
                    //{
                    if (id != null)
                    {
                        // TODO: Continue For... Warning!!! not translated


                        string xTemp_ID = id;
                        string xType = Entry_Type;
                        object xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.FD_INFO, xTemp_ID);
                        // Please Note that this is a dependency , in case fd creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                        if ((xType.ToUpper() == "Voucher_Entry".ToUpper()))
                        {
                            return Json(new
                            {
                                Message = ("FDs Created from Vouchers can be Audited from Vouchers O" + "nly...! " + ("\r\n" + ("\r\n" + "Please unselect FDs Created from Voucher ...!") + "Information")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string xStatus = Action_Status;
                        var value = (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                        Common.Record_Status? MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = (Common_Lib.Common.Record_Status)BASE._FDDBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if ((value != MaxValue))
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                            {
                                AllowUser = true;
                            }

                            //if (AllowUser)
                            //{
                            //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                            //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", ("Record has been Unlocked in the background by another user" + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                            //{
                            //    xPromptWindow.Dispose();
                            //}
                            //else
                            //{
                            //    xPromptWindow.Dispose();
                            //    Grid_Display();
                            //    return;
                            //}

                            //}

                        }
                        else
                        {
                            Msg = "Information...";
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(("A l r e a d y  L o c k e d  E n t r i e s  c a n \'t  b e  R e -L o c k e d. . . !" + ("\r\n" + ("\r\n" + "Please unselect already locked FDs ...!"))), Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return Json(new
                            {
                                Message = ("Already Locked Entries can't be Re-Locked...!" + ("<br/>" + ("<br/>" + "Please unselect already locked FDs ...!"))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(("I n c o m p l e t e   E n t r i e s   c a n \' t   b e   L o c k e d. . . !" + ("\r\n" + ("\r\n" + "Please unselect incomplete FDs or ask Center to Complete it...!"))), Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return Json(new
                            {
                                Message = ("Incomplete Entries can't be Locked!" + ("<br/>" + "<br/> " + "Please unselect incomplete FDs or ask Center to Complete it...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((!(xRemarks == null) && !Convert.IsDBNull(xRemarks)))
                        {
                            if ((MaxValue > 0))
                            {
                                // DevExpress.XtraEditors.XtraMessageBox.Show(("E n t r i e s   w i t h   p e n d i n g   q u e r i e s   c a n \' t   b e   L o c k e d. . . !" + ("\r\n" + ("\r\n" + "Please unselect such FDs...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return Json(new
                                {
                                    Message = ("Entries with pending queries can't be Locked ...!") + ("<br/>" + ("<br/>" + "Please unselect such FDs...!")),
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }


                    //foreach (int CurrRowHandle in this.GridView1.GetSelectedRows)
                    //{
                    if (id != null)
                    {
                        // TODO: Continue For... Warning!!! not translated


                        string xTemp_ID = id;
                        xid = xTemp_ID;
                        Ctr++;
                        if (!BASE._FDDBOps.MarkAsLocked(xTemp_ID))
                        {
                            return Json(new
                            {
                                Message = (Common_Lib.Messages.SomeError + "Error!!"),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    if ((Ctr > 0))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.LockedSuccess(this.GridView1.SelectedRowsCount), "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Grid_Display();
                        //return;
                        return Json(new
                        {
                            Message = (Common_Lib.Messages.LockedSuccess(Ctr) + "Locked..."),
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
                int Ctr = 0;
                if ((BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock)))
                {
                    //foreach (int CurrRowHandle in this.GridView1.GetSelectedRows)
                    //{
                    if (id != null)
                    {
                        // TODO: Continue For... Warning!!! not translated
                        string xTemp_ID = id;
                        string xType = Entry_Type;
                        // Please Note that this is a dependency , in case fd creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                        if ((xType.ToUpper() == "Voucher_Entry".ToUpper()))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(("F D s  C r e a t e d  f r o m  V o u c h e r s  c a n  b e  A u d i t e d  f r o m  V o u c h e r s  " +
                            //    "O n l y . . . !" + ("\r\n" + ("\r\n" + "Please unselect FDs Created from Voucher ...!"))), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                            return Json(new
                            {
                                Message = ("FDs Created from Vouchers can be Audited from Vouchers" + "Only...!" + ("\r\n" + ("\r\n" + "Please unselect FDs Created from Voucher ...!"))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        string xStatus = Action_Status;
                        var value = (Common_Lib.Common.Record_Status)Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                        Common.Record_Status? MaxValue = 0;
                        bool AllowUser = false;
                        MaxValue = (Common_Lib.Common.Record_Status)BASE._FDDBOps.GetStatus(xTemp_ID);
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
                                //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                                //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", ("The Record has been Locked in the background by another user" + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                                //{
                                //    xPromptWindow.Dispose();
                                //}
                                //else
                                //{
                                //    xPromptWindow.Dispose();
                                //    Grid_Display();
                                //    return;
                                //}

                            }

                        }
                        else
                        {
                            Msg = "Information...";
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Completed))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(("A l r e a d y   U n l o c k e d   E n t r i e s   c a n \'t   b e   R e -U n l o c k e d. . . !" + ("\r\n" + ("\r\n" + "Please unselect already unlocked FDs ...!"))), Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return Json(new
                            {
                                Message = ("Already Unlocked Entries can't be Re-Unlocked ...!" + ("<br/>" + ("<br/>" + "Please unselect already unlocked FDs ...!"))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(("I n c o m p l e t e   E n t r i e s   c a n \' t   b e   U n l o c k e d. . . !" + ("\r\n" + ("\r\n" + "Please unselect incomplete FDs or ask Center to Complete it...!"))), Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return Json(new
                            {
                                Message = ("Incomplete Entries can't be Unlocked ...!" + ("<br/>" + ("<br/>" + "Please unselect incomplete FDs or ask Center to Complete it...!"))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // If Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
                        //     If Not Me.GridView1.GetRowCellValue(CurrRowHandle, "Action_By").ToString().ToUpper.Equals(Base._open_User_ID.ToUpper) Then
                        //         DevExpress.XtraEditors.XtraMessageBox.Show("R e c o r d s    l o c k e d    b y    o t h e r    u s e r s   c a n   b e   u n l o c k e d   b y   S u p e r U s e r s   O n l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        //     End If
                        // End If
                    }

                    //foreach (int CurrRowHandle in this.GridView1.GetSelectedRows)
                    //{
                    if (id != null)
                    {
                        // TODO: Continue For... Warning!!! not translated


                        string xTemp_ID = id;
                        var xid = xTemp_ID;
                        Ctr++;
                        if (!BASE._FDDBOps.MarkAsComplete(xTemp_ID))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return Json(new
                            {
                                Message = (Common_Lib.Messages.SomeError + "Error!!"),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    if ((Ctr > 0))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UnlockedSuccess(this.GridView1.SelectedRowsCount), "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return Json(new
                        {
                            Message = (Common_Lib.Messages.UnlockedSuccess(Ctr) + "Locked..."),
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


            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Frm_FD_Window(FD model)
        {
            //Hide_Properties();

            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + model.TempActionMethod);
            Common_Lib.Common.Navigation_Mode Tag = model.ActionMethod;


            // -----------------------------+
            // Start : Check if entry already changed 
            // -----------------------------+
            if (BASE.AllowMultiuser())
            {
                if (((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            || (Tag == Common_Lib.Common.Navigation_Mode._Delete)))
                {
                    DataTable fd_DbOps = BASE._FDDBOps.GetRecord(model.xID.ToString());
                    if ((fd_DbOps == null))
                    {
                        //Base.HandleDBError_OnNothingReturned();
                        //return;
                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((fd_DbOps.Rows.Count == 0))
                    {
                        return Json(new
                        {
                            message = (Common_Lib.Messages.RecordChanged("Current FD") + "Record Already Changed!!"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((model.LastEditedOn != Convert.ToDateTime(fd_DbOps.Rows[0]["REC_EDIT_ON"])))
                    {

                        return Json(new
                        {
                            message = (Common_Lib.Messages.RecordChanged("Current FD") + "Record Already Changed!!"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                if (((Tag) == Common_Lib.Common.Navigation_Mode._Edit) || (Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    DataTable FD_Table = BASE._FDDBOps.GetList("Voucher Entry", "Profile Entry", model.xID.ToString());
                    if ((FD_Table == null))
                    {
                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((!(FD_Table.Rows[0]["FD_CLOSE_DATE"] == null) && !Convert.IsDBNull(FD_Table.Rows[0]["FD_CLOSE_DATE"])))
                    {

                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((BASE._FDDBOps.GetExpense_IncomeCount(model.xID.ToString()) == null))
                    {

                        return Json(new
                        {
                            message = "FD Entry with Interest/TDS entries cannot be Edited / !" + "Deleted...!" + ("\r\n" + ("\r\n" + ("Note:" + ("-------" + ("Delete all such vouchers to edit / delete this Entry." + "-------"))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    object MaxValue = 0;
                    MaxValue = BASE._FDDBOps.GetStatus(model.xID.ToString());
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {

                        return Json(new
                        {
                            message = (Common_Lib.Messages.RecordChanged("Current FD") + "Record Status Already Changed!!"),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                if ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    object openActions = BASE._Action_Items_DBOps.GetOpenActions(model.xID.ToString(), "FD_INFO");
                    if ((openActions == null))
                    {

                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((Convert.ToInt16(openActions) > 0))
                    {

                        return Json(new
                        {
                            message = "Entry Cannot be Deleted ...!" + ("\r\n" + ("\r\n" + "There are open actions / queries posted against it...!")) + "Information...",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }

            // -----------------------------+
            // End : Check if entry already changed 
            // -----------------------------+
            if (((Tag) == Common_Lib.Common.Navigation_Mode._New)
                        || (Tag) == Common_Lib.Common.Navigation_Mode._Edit)
            {
                // If Base.DateTime_Mismatch Then
                //     Me.DialogResult = Windows.Forms.DialogResult.None
                //     Exit Sub
                // End If
                // check FD Bank as blank
                //if (((model.BankList.ToString().Trim().Length <= 0) || (model.BankList.ToString().Trim().Length <= 0)))
                if ((string.IsNullOrEmpty(model.BankList) || model.BankList.ToString().Trim().Length <= 0))
                {
                    return Json(new
                    {
                        message = "Bank Name Not Selected...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //this.ToolTip1.Hide(this.Look_BankList);
                }

                // check FD NO as blank
                if ((string.IsNullOrEmpty(model.Txt_No) || model.Txt_No.ToString().Length == 0))
                {
                    return Json(new
                    {
                        message = "Please Enter F.D. Account No....!" + model.Txt_No,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                // check FD date as blank
                if ((IsDate(model.Txt_Date.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect / Blank...!" + model.Txt_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.Txt_Date >= BASE._open_Year_Sdt))
                {
                    return Json(new
                    {
                        message = "Date must be Earlier than Financial Year...!" + model.Txt_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                // check FD As date as blank
                if ((IsDate(model.Txt_As_Date.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect / Blank...!" + model.Txt_As_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.Txt_As_Date > model.Txt_Date))
                {
                    return Json(new
                    {
                        message = "As of Date must be Equal / Lower than to F.D.Date.." + ".!" + model.Txt_As_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                // check FD Amount as blank
                if (model.Txt_Amount == null)
                {
                    return Json(new
                    {
                        message = "FD Amount cannot be Zero / Negative ..." + model.Txt_Amount,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                // check FD Rate as blank
                if (((double.Parse(model.txt_Rate.ToString()) <= 0) || (double.Parse(model.txt_Rate.ToString()) > 100)))
                {
                    return Json(new
                    {
                        message = "FD Rate Incorrect ...!" + model.txt_Rate,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                // check FD Maturity Amount as blank
                if (model.Txt_Mat_Amount.ToString() == null)
                {
                    return Json(new
                    {
                        message = "Maturity Amount cannot be Zero / Negative ...!" + model.Txt_Mat_Amount,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((double.Parse(model.Txt_Mat_Amount.ToString()) < double.Parse(model.Txt_Amount.ToString())))
                {
                    return Json(new
                    {
                        message = "Maturity Amount cannot be Less than F.D. Amount...!" + model.Txt_Mat_Amount,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Maturity date as blank
                if ((IsDate(model.Txt_Mat_Date.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect / Blank...!" + model.Txt_Mat_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Maturity date > FD Date
                if ((model.Txt_Mat_Date <= model.Txt_Date))
                {
                    return Json(new
                    {
                        message = "Maturity Date Must Be Greater Than F.D. Date...!" + model.Txt_Mat_Date,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }

            // ----------------------------------------
            if (BASE.AllowMultiuser())
            {
                if (((Tag == Common_Lib.Common.Navigation_Mode._New) || (Tag == Common_Lib.Common.Navigation_Mode._Edit)))
                {
                    DataTable d1 = BASE._FDDBOps.GetBankAccounts(model.BankList);
                    if ((d1 == null))
                    {

                        return Json(new
                        {
                            message = "No Data Found . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    DateTime oldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank);
                    if ((d1.Rows.Count <= 0))
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.DependencyChanged("Bank Account") + "Referred Record Already Deleted!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["Edit Date"]);
                        if ((oldEditOn != NewEditOn))
                        {
                            // A/E,E/E
                            return Json(new
                            {
                                message = (Common_Lib.Messages.DependencyChanged("Bank Account") + "Referred Record Already Changed!!"),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }

            }

            // ----------------------------------------
            // 'CHECKING LOCK STATUS
            // If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            //     Dim MaxVal As Object = 0
            //     MaxVal = Base._FDDBOps.GetStatus(Me.xID.Text)
            //     If MaxVal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            //     If MaxVal = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            // End If
            //SimpleButton btn;
            //btn = ((SimpleButton)(sender));
            string Status_Action = string.Empty;
            if (model.Chk_Incompleted)
            {
                Status_Action = Convert.ToString((int)Common_Lib.Common.Record_Status._Incomplete);
            }
            else
            {
                Status_Action = Convert.ToString((int)Common_Lib.Common.Record_Status._Completed);
            }

            //if ((btn.Name == BUT_DEL.Name))
            //{
            //    Status_Action = Common_Lib.Common.Record_Status._Deleted;
            //}

            if ((Tag) == Common_Lib.Common.Navigation_Mode._New)
            {
                // new
                model.xID = System.Guid.NewGuid().ToString();
                Common_Lib.RealTimeService.Parameter_InsertandUpdateBalance_FD InBal = new Common_Lib.RealTimeService.Parameter_InsertandUpdateBalance_FD();
                InBal.BankAccountID = model.BankList;
                InBal.FDNo = model.Txt_No.ToString();
                if (IsDate(model.Txt_Date.ToString()))
                {
                    InBal.FDDate = Convert.ToDateTime(model.Txt_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                if (IsDate(model.Txt_As_Date.ToString()))
                {
                    InBal.AsOnDate = Convert.ToDateTime(model.Txt_As_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                InBal.Amount = double.Parse(model.Txt_Amount.ToString());
                InBal.InterestRate = double.Parse(model.txt_Rate.ToString());
                InBal.IntCondition = model.Cmd_Type.ToString();
                if (IsDate(model.Txt_Mat_Date.ToString()))
                {
                    InBal.MaturityDate = Convert.ToDateTime(model.Txt_Mat_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                InBal.MaturityAmount = double.Parse(model.Txt_Mat_Amount.ToString());
                InBal.Remarks = string.IsNullOrEmpty(model.Txt_Remarks) ? "" : model.Txt_Remarks;
                InBal.Status_Action = Status_Action;
                InBal.RecID = model.xID.ToString();
                if (BASE._FDDBOps.Insert_and_Update_Balance(InBal))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, this.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.DialogResult = DialogResult.OK;
                    //FormClosingEnable = false;
                    //this.Close();
                    return Json(new
                    {
                        message = (Common_Lib.Messages.SaveSuccess),
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //return;
                    return Json(new
                    {
                        message = (Common_Lib.Messages.SomeError + "Error!!"),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            if ((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
            {
                // editItem
                Common_Lib.RealTimeService.Parameter_UpdateandUpdateBalance_FD UpBal = new Common_Lib.RealTimeService.Parameter_UpdateandUpdateBalance_FD();
                UpBal.BankAccountID = model.BankList;
                UpBal.FDNo = model.Txt_No.ToString();
                if (IsDate(model.Txt_Date.ToString()))
                {
                    UpBal.FDDate = Convert.ToDateTime(model.Txt_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                if (IsDate(model.Txt_As_Date.ToString()))
                {
                    UpBal.AsOnDate = Convert.ToDateTime(model.Txt_As_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                UpBal.Amount = double.Parse(model.Txt_Amount.ToString());
                UpBal.InterestRate = double.Parse(model.txt_Rate.ToString());
                UpBal.IntCondition = model.Cmd_Type.ToString();
                if (IsDate(model.Txt_Mat_Date.ToString()))
                {
                    UpBal.MaturityDate = Convert.ToDateTime(model.Txt_Mat_Date.ToString()).ToString(BASE._Server_Date_Format_Long);
                }

                UpBal.MaturityAmount = double.Parse(model.Txt_Mat_Amount.ToString());
                UpBal.Remarks = model.Txt_Remarks == null ? "" : model.Txt_Remarks.ToString();
                // UpBal.Status_Action = Status_Action
                UpBal.RecID = model.xID.ToString();
                if (BASE._FDDBOps.Update_and_Update_Balance(UpBal))
                {
                    return Json(new
                    {
                        message = (Common_Lib.Messages.UpdateSuccess),
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = (Common_Lib.Messages.SomeError) + "Error!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            if ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)
            {
                // DELETE
                // Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                //if ((DialogResult.Yes == xPromptWindow.ShowDialog(this.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue)))
                //{
                // Dim STR1 As String = "DELETE  TABLE FROM FD_INFO WHERE REC_ID = '" & Me.xID.Text & "'"
                if (BASE._FDDBOps.Delete_and_Update_Balance(model.xID.ToString(), model.BankList))
                {
                    return Json(new
                    {
                        message = (Common_Lib.Messages.DeleteSuccess),
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = (Common_Lib.Messages.SomeError) + "Error!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                //}
            }

            //     'FD BALANCE UPDATE FOR BANK A/C.
            //     Dim MaxValue As Object = 0
            //     MaxValue = Base._FDDBOps.GetFDSum(Me.Look_BankList.Tag)
            //     If IsDBNull(MaxValue) Then MaxValue = 0
            // If Not Base._FDDBOps.UpdateBankAccountBalance(MaxValue, Me.Look_BankList.Tag) Then
            //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            // End If


            return Json(new
            {
                message = Common_Lib.Messages.SaveSuccess,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Start--> LookupEdit Events"
        public ActionResult LookUp_GetBankList_FD(DataSourceLoadOptions loadOptions)
        {
            DataTable BA_Table = BASE._FDDBOps.GetBankAccounts();
            if ((BA_Table == null))
            {
                //Base.HandleDBError_OnNothingReturned();
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

            DataTable BB_Table = BASE._FDDBOps.GetBranchDetails(Branch_IDs);
            if ((BB_Table == null))
            {
                //Base.HandleDBError_OnNothingReturned();
                return null;
            }

            // BUILD DATA
            var BuildData = from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
                            on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                            select new
                            {
                                Name = B.Field<string>("Name"),
                                Branch = B.Field<string>("Branch"),
                                BA_CUST_NO = A.Field<string>("BA_CUST_NO"),
                                ID = A.Field<string>("BA_BANK_ID"),
                                RecEditOn = A.Field<DateTime?>("Edit Date")
                            };
            var Final_Data = BuildData.ToList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Final_Data, loadOptions)), "application/json");
        }

        public FD Data_Binding(FD model, Common_Lib.Common.Navigation_Mode Tag)
        {
            DataTable d1 = BASE._FDDBOps.GetRecord(model.xID.ToString());
            if ((d1 == null))
            {
                return model;
            }
            // -----------------------------+
            // Start : Check if entry already changed 
            // -----------------------------+
            if (BASE.AllowMultiuser())
            {
                if (((Tag == Common_Lib.Common.Navigation_Mode._Edit) || ((Tag == Common_Lib.Common.Navigation_Mode._Delete) || (Tag == Common_Lib.Common.Navigation_Mode._View))))
                {

                    if ((model.Info_LastEditedOn != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])))
                    {

                        return model;
                    }

                }

            }

            // -----------------------------+
            // End : Check if entry already changed 
            // -----------------------------+
            model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            if (d1.Rows[0]["FD_BA_ID"].ToString().Length > 0)
            {

                //var dataBankList = LookUp_GetBankList_FD(new DataSourceLoadOptions());
                model.BankList = d1.Rows[0]["FD_BA_ID"].ToString();
                FD_Bank dataModelBankList = (JsonConvert.DeserializeObject<List<FD_Bank>>(((System.Web.Mvc.ContentResult)LookUp_GetBankList_FD(new DataSourceLoadOptions())).Content)).FirstOrDefault(x => x.ID == d1.Rows[0]["FD_BA_ID"].ToString());
                if (dataModelBankList != null)
                {
                    model.Lbl_Branches = dataModelBankList.Branch;
                    model.Lbl_AccountNumber = dataModelBankList.BA_CUST_NO;
                    model.REC_EDIT_ON_Bank = dataModelBankList.RecEditOn.ToString();

                }
            }
            model.Txt_No = d1.Rows[0]["FD_NO"].ToString();
            model.Txt_Amount = d1.Rows[0]["FD_AMT"].ToString();
            model.txt_Rate = d1.Rows[0]["FD_INT_RATE"].ToString();
            model.Txt_Mat_Amount = Convert.ToDouble(d1.Rows[0]["FD_MAT_AMT"]);
            model.Cmd_Type = d1.Rows[0]["FD_INT_PAY_COND"].ToString();
            model.Txt_Remarks = d1.Rows[0]["FD_OTHER_DETAIL"].ToString();
            DateTime? xDate = null;
            xDate = Convert.ToDateTime(d1.Rows[0]["FD_DATE"]);
            model.Txt_Date = xDate;
            xDate = Convert.ToDateTime(d1.Rows[0]["FD_AS_DATE"]);
            model.Txt_As_Date = xDate;
            xDate = Convert.ToDateTime(d1.Rows[0]["FD_MAT_DATE"]);
            model.Txt_Mat_Date = xDate;
            TimeSpan ts1 = (Convert.ToDateTime(model.Txt_Mat_Date)) - (Convert.ToDateTime(model.Txt_As_Date));
            double diff1 = ts1.TotalDays;
            model.Lbl_timePeriod = diff1;
            return model;
        }

        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_FD, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('FD_report_modal','Not Allowed','No Rights');$('#FDModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        #region DevExtreme
        public ActionResult Frm_FD_Info_dx()
        {
            FD_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Profile_FD, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_FD').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_FD).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["FDList_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_FD, Common_Lib.Common.ClientAction.Lock_Unlock))
            {
                ViewBag.LockUnlock = false;
            }
            return View();
        }
        public ActionResult Frm_FD_Info_GridData_dx()
        {        
            return Content(JsonConvert.SerializeObject(GetGridData_dx()), "application/json");
        }
        public List<FD_Info> GetGridData_dx()
        {
            DataTable Final_Data = BASE._FDDBOps.GetProfileList();
            List<FD_Info> FD = new List<FD_Info>();
            foreach (DataRow row in Final_Data.Rows)
            {
                FD_Info newRow = new FD_Info();
                newRow.BI_BANK_NAME = row["BI_BANK_NAME"].ToString();
                newRow.BB_BRANCH_NAME = row["BB_BRANCH_NAME"].ToString();
                newRow.FD_NO = row["FD_NO"].ToString();
                newRow.FD_DATE = Convert.ToDateTime(row["FD_DATE"]);
                newRow.FD_AS_DATE = Convert.ToDateTime(row["FD_AS_DATE"]);
                newRow.FD_AMOUNT = Convert.ToDecimal(row["FD_AMOUNT"]);
                newRow.FD_INT_RATE = Convert.ToDecimal(row["FD_INT_RATE"]);
                newRow.FD_INT_PAY_COND = row["FD_INT_PAY_COND"].ToString();
                newRow.FD_MAT_DATE = Convert.ToDateTime(row["FD_MAT_DATE"]);
                newRow.FD_MAT_AMT = Convert.ToDecimal(row["FD_MAT_AMT"]);
                newRow.BA_CUST_NO = row["BA_CUST_NO"].ToString();
                newRow.Add_By = row["Add_By"].ToString();
                newRow.Add_Date = Convert.ToDateTime(row["Add_Date"]);
                newRow.Edit_By = row["Edit_By"].ToString();
                newRow.Edit_Date = Convert.ToDateTime(row["Edit_Date"]);
                newRow.Action_Status = row["Action_Status"].ToString();
                newRow.Action_By = row["Action_By"].ToString();
                newRow.Action_Date = Convert.ToDateTime(row["Action_Date"]);
                newRow.TR_ID = row["TR_ID"].ToString();
                newRow.ID = row["ID"].ToString();
                newRow.FD_Status = row["FD_Status"].ToString();
                //newRow.CLOSE_DATE = Convert.ToDateTime(row["CLOSE_DATE"]) ;
                newRow.CLOSE_DATE = string.IsNullOrEmpty(row["CLOSE_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(row["CLOSE_DATE"]);
                newRow.Entry_Type = row["Entry_Type"].ToString();
                //newRow.Interest_Recd = Convert.ToDecimal(row["Interest_Recd"]);
                newRow.Interest_Recd = string.IsNullOrEmpty(row["Interest_Recd"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["Interest_Recd"]);
                //newRow.TDS_Paid = Convert.ToDecimal(row["TDS_Paid"]);
                newRow.TDS_Paid = string.IsNullOrEmpty(row["TDS_Paid"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["TDS_Paid"]);
                //newRow.Nett_Interest = Convert.ToDecimal(row["Nett_Interest"]);
                newRow.Nett_Interest = string.IsNullOrEmpty(row["Nett_Interest"].ToString()) ? (decimal?)null : Convert.ToDecimal(row["Nett_Interest"]);
                newRow.Other_Detail = row["Other Detail"].ToString();
                newRow.FD_Less_Maturity = Convert.ToDecimal(row["FD_Less_Maturity"]);
                newRow.Remarks = Convert.ToInt16(row["Remarks"]);
                newRow.RemarkStatus = row["RemarkStatus"].ToString();
                newRow.OpenActions = Convert.ToInt16(row["OpenActions"]);
                newRow.CrossedTimeLimit = Convert.ToInt16(row["CrossedTimeLimit"]);
                newRow.YearID = Convert.ToInt16(row["YearID"]);
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
                newRow.iIcon = "";
                newRow.Special_Ref = Convert.IsDBNull(row["Special_Ref"]) ? "" : Convert.ToString(row["Special_Ref"]);
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
                FD.Add(newRow);
            }
            return FD.ToList();
        }
        public ActionResult Frm_FD_Info_DetailGrid_dx(string RecID="", bool VouchingMode = false)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_FD, !VouchingMode)), "application/json");
        }
        public ActionResult AdditionalInfo_Grid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_FD)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_FD, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('FD_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
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
            ClearBaseSession("_FD");
            Session.Remove("FDInfo_detailGrid_Data");
        }
        public void FD_user_rights()
        {
            ViewData["FD_AddRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "Add");
            ViewData["FD_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "Update");
            ViewData["FD_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "View");
            ViewData["FD_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "Delete");
            ViewData["FD_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "Export");
            ViewData["FD_ListRight"] = CheckRights(BASE, ClientScreen.Profile_FD, "List");
            ViewData["FD_ListBankRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "List");
            ViewData["FD_AddHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Add");
            ViewData["FD_ListHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "List");
            ViewData["FD_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_FD, Common.ClientAction.Lock_Unlock);
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
    }
}