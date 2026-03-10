using Common_Lib;
using Common_Lib.RealTimeService;
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class TelephoneController : BaseController
    {

        #region "Start--> Default Variables"

        public DateTime LastEditedOn
        {
            get
            {
                return (DateTime)GetBaseSession("LastEditedOn_Telephone");
            }
            set
            {
                SetBaseSession("LastEditedOn_Telephone", value);
            }
        }
        public List<TelephoneProfile> Telephone_ExportData
        {
            get
            {
                return (List<TelephoneProfile>)GetBaseSession("Telephone_ExportData_Telephone");
            }
            set
            {
                SetBaseSession("Telephone_ExportData_Telephone", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> TelephoneInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("TelephoneInfo_DetailGrid_Data_Telephone");
            }
            set
            {
                SetBaseSession("TelephoneInfo_DetailGrid_Data_Telephone", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> TelephoneInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("TelephoneInfo_AdditionalInfoGrid_Telephone");
            }
            set
            {
                SetBaseSession("TelephoneInfo_AdditionalInfoGrid_Telephone", value);
            }
        }
        public List<Telecom_INFO> Telecomdata
        {
            get { return (List<Telecom_INFO>)GetBaseSession("Telecomdata_Telephone"); }
            set { SetBaseSession("Telecomdata_Telephone",value); }
        }        
        public void SetDefaultValues()
        {
            LastEditedOn = default(DateTime);
        }
        #endregion
        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/Telephone)
        public ActionResult Frm_Telephone_Info(string xid = "")
        {
            SetDefaultValues();
            Telephone_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Telephone, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Telephone').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Telephone).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            ViewData["xid"] = xid;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Telephone_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View();
        }
        public PartialViewResult Frm_Telephone_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool getlatestdata = false, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Telephone_user_rights();
            if(command == "REFRESH" || getlatestdata == true)
            {
                Grid_Display();
            }
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            return PartialView(Telephone_ExportData);
        }
        #region nested grid
        public ActionResult Frm_Telephone_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID ="")
        {
            ViewBag.TelephoneInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.TelephoneInfo_RecID = RecID;
            ViewBag.TelephoneInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    TelephoneInfo_DetailGrid_Data =  BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Telephone);                    
                    Session["TelephoneInfo_detailGrid_Data"] = TelephoneInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Telephone);
                    TelephoneInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["TelephoneInfo_detailGrid_Data"] = data.DocumentMapping;
                    TelephoneInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(TelephoneInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(TelephoneInfo_AdditionalInfoGrid);
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
            settings.Name = "TelephoneListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "TelephoneListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["TelephoneInfo_detailGrid_Data"];
        }
        #endregion
        public ActionResult TelephoneCustomDataAction(string key)
        {
            var Final_Data = Telephone_ExportData as List<TelephoneProfile>;
            var it = (TelephoneProfile)Final_Data.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.ID + "![" + it.tP_NOField + "![" + it.telMiscIdField + "![" + it.categoryField + "![" +
                            it.typeField +"![" + it.EditDate + "![" +
                            it.AddDate + "![" + it.AddBy + "![" + it.EditBy + "![" + it.ActionStatus + "![" + it.ActionDate+ "![" + it.ActionBy;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Grid_Display()
        {
            DataTable MISC_Table = BASE._telephoneDBOps.GetTelecomCompanies("MISC_ID", "MISC_NAME");
            if (MISC_Table == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            DataTable TP_Table = BASE._telephoneDBOps.GetList();
            if (MISC_Table == null || TP_Table == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }            
            var BuildData = from T in TP_Table.AsEnumerable()
                            join M in MISC_Table.AsEnumerable() on T["TP_TELECOM_MISC_ID"] equals M["MISC_ID"]
                            select new TelephoneProfile
                            {                                
                                tP_NOField = T["TP_NO"].ToString(),
                                telMiscIdField = M["MISC_NAME"].ToString(),
                                categoryField = T["Category"].ToString(),
                                typeField = T["TP_TYPE"].ToString(),
                                other_DetField = T["TP_OTHER_DETAIL"].ToString(),
                                AddBy = T["Add By"].ToString(),
                                AddDate = Convert.ToDateTime(T["Add Date"].ToString()),
                                EditBy = T["Edit By"].ToString(),
                                EditDate = Convert.ToDateTime(T["Edit Date"].ToString()),
                                ActionStatus = T["Action Status"].ToString(),
                                ActionBy = T["Action By"].ToString(),
                                ActionDate = Convert.ToDateTime(T["Action Date"]),
                                ID = T["ID"].ToString(),
                                REQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]),
                                COMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]),
                                RESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]),
                                REJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]),
                                OTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]),
                                ALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]),
                                VOUCHING_PENDING_COUNT = T.Field<Int32?>("VOUCHING_PENDING_COUNT"),
                                VOUCHING_ACCEPTED_COUNT = T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT"),
                                VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = T.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"),
                                VOUCHING_REJECTED_COUNT = T.Field<Int32?>("VOUCHING_REJECTED_COUNT"),
                                VOUCHING_TOTAL_COUNT = T.Field<Int32?>("VOUCHING_TOTAL_COUNT"),
                                AUDIT_PENDING_COUNT = T.Field<Int32?>("AUDIT_PENDING_COUNT"),
                                AUDIT_ACCEPTED_COUNT = T.Field<Int32?>("AUDIT_ACCEPTED_COUNT"),
                                AUDIT_ACCEPTED_WITH_REMARKS_COUNT = T.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"),
                                AUDIT_REJECTED_COUNT = T.Field<Int32?>("AUDIT_REJECTED_COUNT"),
                                AUDIT_TOTAL_COUNT = T.Field<Int32?>("AUDIT_TOTAL_COUNT"),
                                IS_AUTOVOUCHING = T.Field<Int32?>("IS_AUTOVOUCHING"),
                                IS_CORRECTED_ENTRY = T.Field<Int32?>("IS_CORRECTED_ENTRY")
                            };           
            var Final_Data = BuildData.ToList();
            for (int i = 0; i < Final_Data.Count; i++)
            {
                if (Final_Data[i].IS_CORRECTED_ENTRY > 0)
                {
                    Final_Data[i].iIcon += "CorrectedEntry|";
                }
                if (Final_Data[i].IS_AUTOVOUCHING > 0)
                {
                    Final_Data[i].iIcon += "AutoVouching|";
                }
                if ((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) == 0 && (Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0))
                {
                    Final_Data[i].iIcon += "RedShield|";
                }
                else if (((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) >= (Final_Data[i].REQ_ATTACH_COUNT ?? 0)) && ((Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0) && ((Final_Data[i].RESPONDED_COUNT ?? 0) == 0)))
                {
                    Final_Data[i].iIcon += "GreenShield|";
                }
                else if ((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) > 0 && (((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) < (Final_Data[i].REQ_ATTACH_COUNT ?? 0))))
                {
                    Final_Data[i].iIcon += "YellowShield|";
                }
                else if (((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) >= (Final_Data[i].REQ_ATTACH_COUNT ?? 0)) && ((Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0) && ((Final_Data[i].RESPONDED_COUNT ?? 0) > 0)))
                {
                    Final_Data[i].iIcon += "BlueShield|";
                }
                if (((Final_Data[i].REJECTED_COUNT ?? 0) > 0))
                {
                    Final_Data[i].iIcon += "RedFlag|";
                }
                if ((((Final_Data[i].ALL_ATTACH_CNT ?? 0) > 0) && (Final_Data[i].OTHER_ATTACH_CNT ?? 0) == 0))
                {
                    Final_Data[i].iIcon += "RequiredAttachment|";
                }
                else if ((((Final_Data[i].ALL_ATTACH_CNT ?? 0) > 0) && (Final_Data[i].OTHER_ATTACH_CNT ?? 0) != 0))
                {
                    Final_Data[i].iIcon += "AdditionalAttachment|";
                }
                if (Final_Data[i].VOUCHING_TOTAL_COUNT == Final_Data[i].VOUCHING_ACCEPTED_COUNT && Final_Data[i].VOUCHING_ACCEPTED_WITH_REMARKS_COUNT == 0 && Final_Data[i].VOUCHING_ACCEPTED_COUNT > 0) { Final_Data[i].iIcon += "VouchingAccepted|"; }
                if (Final_Data[i].VOUCHING_REJECTED_COUNT > 0) { Final_Data[i].iIcon += "VouchingReject|"; }
                if (Final_Data[i].VOUCHING_TOTAL_COUNT == Final_Data[i].VOUCHING_ACCEPTED_COUNT && Final_Data[i].VOUCHING_ACCEPTED_WITH_REMARKS_COUNT > 0) { Final_Data[i].iIcon += "VouchingAcceptWithRemarks|"; }
                if (Final_Data[i].VOUCHING_PENDING_COUNT > 0 && (Final_Data[i].VOUCHING_ACCEPTED_COUNT > 0 || Final_Data[i].VOUCHING_REJECTED_COUNT > 0)) { Final_Data[i].iIcon += "VouchingPartial|"; }
                if (Final_Data[i].AUDIT_TOTAL_COUNT == Final_Data[i].AUDIT_ACCEPTED_COUNT && Final_Data[i].AUDIT_ACCEPTED_WITH_REMARKS_COUNT == 0 && Final_Data[i].AUDIT_ACCEPTED_COUNT > 0) { Final_Data[i].iIcon += "AuditAccepted|"; }
                if (Final_Data[i].AUDIT_REJECTED_COUNT > 0) { Final_Data[i].iIcon += "AuditReject|"; }
                if (Final_Data[i].AUDIT_TOTAL_COUNT == Final_Data[i].AUDIT_ACCEPTED_COUNT && Final_Data[i].AUDIT_ACCEPTED_WITH_REMARKS_COUNT > 0) { Final_Data[i].iIcon += "AuditAcceptWithRemarks|"; }
                if (Final_Data[i].AUDIT_PENDING_COUNT > 0 && (Final_Data[i].AUDIT_ACCEPTED_COUNT > 0 || Final_Data[i].AUDIT_REJECTED_COUNT > 0)) { Final_Data[i].iIcon += "AuditPartial|"; }

            }
            Telephone_ExportData = Final_Data;
            return Json(new
            {
                message = "",
                result = true,
                TotalRowCount = Final_Data.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Add/Edit Bank Account Details for popup
        public JsonResult DataNavigation(string ActionMethod, string ID)
        {
            try
            {
                if (ActionMethod == "Edit" || ActionMethod == "Delete")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._telephoneDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        return Json(new
                        {
                            message = "Entry Not Found / Changed In Background... !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        return Json(new
                        {
                            message = "Locked Entry can not be Edited / Deleted... !<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "Delete")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    bool DeleteAllow = true;
                    string UsedPage = "";
                    if (DeleteAllow)
                    {
                        MaxValue = 0;
                        MaxValue = BASE._telephoneDBOps.GetCountInTxn(ClientScreen.Profile_Telephone, xTemp_ID);
                        if (MaxValue == null)
                        {
                            return Json(new { message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue > 0)
                        {
                            DeleteAllow = false;
                        }
                        UsedPage = "Cash Book Information...";
                    }

                    if (DeleteAllow == false)
                    {
                        return Json(new
                        {
                            message = "Can't Delete...!<br><br>This information is being used in Another Page...!<br><br>Name : " + UsedPage,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "View")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._telephoneDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        return Json(new
                        {
                            message = "Entry Not Found / Changed In Background... !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_Telephone_Window(string EditedOn, string ActionMethod = null, string ID = null)
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (ActionMethod == AM[i] && CheckRights(BASE, ClientScreen.Profile_Telephone, Rights[i])==false)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Telephone_Window','Not Allowed','No Rights');</script>");
                }
            }
            TelephoneProfile model = new TelephoneProfile();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            if (Navigation_Mode_tag == Common.Navigation_Mode._Edit || Navigation_Mode_tag == Common.Navigation_Mode._Delete || Navigation_Mode_tag == Common.Navigation_Mode._View)
            {
                model.EditDate = Convert.ToDateTime(EditedOn);
                DataTable _dtableTelData = BASE._telephoneDBOps.GetRecord(ID);
                if (_dtableTelData == null)
                {
                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','No Rights');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (Navigation_Mode_tag == Common.Navigation_Mode._Edit || Navigation_Mode_tag == Common.Navigation_Mode._Delete || Navigation_Mode_tag == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (Navigation_Mode_tag == Common.Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (model.EditDate != Convert.ToDateTime(_dtableTelData.Rows[0]["REC_EDIT_ON"]))
                        {
                            string message = Messages.RecordChanged("Current TelephoneInfo", viewstr);
                            return Content("<script language='javascript' type='text/javascript'> Telephone_IsSucessPopup = true;MultiUserPrevention('popup_frm_Telephone_Window','" + message + "','Record Already Changed!!');RefreshGridView('TelephoneListGrid');</script>");
                        }
                    }
                }
                // -----------------------------+
                // End : Check if entry already changed 
                //-----------------------------+           
                model.tP_NOField = (string)_dtableTelData.Rows[0]["TP_NO"];
                model.Old_tP_NOField = model.tP_NOField;
                model.telMiscIdField = (string)_dtableTelData.Rows[0]["TP_TELECOM_MISC_ID"];
                model.typeField = (string)_dtableTelData.Rows[0]["TP_TYPE"];
                model.categoryField = (string)_dtableTelData.Rows[0]["TP_CATEGORY"];
                model.other_DetField = (string)_dtableTelData.Rows[0]["TP_OTHER_DETAIL"];
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Telephone_Window(TelephoneProfile model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._New)
                {
                    var regex = new Regex(@"^[0-9]{1,11}$");
                    if (!regex.IsMatch(model.tP_NOField))
                    {
                        jsonParam.message = " Only Numbers Are Allowed in Telephone No..";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "tP_NOField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }//redmine bug 132819 fix 
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete)
                    {
                        DataTable telephone_DbOps = BASE._telephoneDBOps.GetRecord(model.ID);
                        if (telephone_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (telephone_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current TelephoneInfo");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.EditDate != Convert.ToDateTime(telephone_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current TelephoneInfo");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._telephoneDBOps.GetStatus(model.ID);
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
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Delete)
                        {
                            MaxValue = 0;
                            // check if Telephone info is used in any txn(payment voucher) #Ref AC23
                            MaxValue = BASE._telephoneDBOps.GetCountInTxn(ClientScreen.Profile_Telephone, model.ID);
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
                            if ((int)MaxValue > 0)
                            {
                                // D/AE
                                jsonParam.message = "Can't Delete<br><br>This information Is Being Used In Another Page...";
                                jsonParam.title = "Warning...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+

                if (model.ActionMethod == Common.Navigation_Mode._New || model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.tP_NOField))
                    {
                        jsonParam.message = "Telephone No. Cannot Be Blank..";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "tP_NOField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.telMiscIdField))
                    {
                        jsonParam.message = "Telecom Company Not Selected..";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "telMiscIdField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.categoryField))
                    {
                        jsonParam.message = "Category Not Selected..";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "categoryField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.typeField))
                    {
                        jsonParam.message = "Type Not Selected..";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "typeField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // Checking Duplicate Record....
                    object MaxValue = null;
                    int xID = 0;
                    MaxValue = BASE._telephoneDBOps.GetRecordByTeleNumber(model.tP_NOField);
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
                    if ((int)MaxValue != 0 && model.ActionMethod == Common.Navigation_Mode._New)
                    {
                        jsonParam.message = "Same Telephone No. Already Available..";
                        jsonParam.title = "Duplicate. . . (" + model.tP_NOField + ")";
                        jsonParam.result = false;
                        jsonParam.focusid = "tP_NOField";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((int)MaxValue != 0 && model.ActionMethod == Common.Navigation_Mode._Edit)
                    {
                        if (model.Old_tP_NOField != model.tP_NOField)
                        {
                            jsonParam.message = "Same Telephone No. Already Available...<br><br>--> Edit No.: "+ model.Old_tP_NOField;
                            jsonParam.title = "Duplicate. . . (" + model.tP_NOField + ")";
                            jsonParam.result = false;
                            jsonParam.focusid = "tP_NOField";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // CHECKING LOCK STATUS
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    object MaxValue = 0;
                    MaxValue = BASE._telephoneDBOps.GetStatus(model.ID);
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
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                int Status_Action = Convert.ToInt16(Common.Record_Status._Completed);
                if (model.ActionMethod == Common.Navigation_Mode._Delete)
                {
                    Status_Action = Convert.ToInt16(Common.Record_Status._Deleted);
                }

                if (model.ActionMethod == Common.Navigation_Mode._New)
                {
                    // new
                    Parameter_Insert_Telephones InParam = new Parameter_Insert_Telephones();
                    InParam.TP_NO = model.tP_NOField;
                    InParam.TelMiscId = model.telMiscIdField;
                    InParam.Category = model.categoryField;
                    InParam.Type = model.typeField;
                    InParam.Other_Det = model.other_DetField;
                    InParam.Status_Action = Status_Action.ToString();                   
                    if (BASE._telephoneDBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Telephone";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            xid=Guid.NewGuid()
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
                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                {
                    // edit
                    Parameter_Update_Telephones UpParam = new Parameter_Update_Telephones();
                    UpParam.TP_NO = model.tP_NOField;
                    UpParam.TELECOM_MISC_ID = model.telMiscIdField;
                    UpParam.CATEGORY = model.categoryField;
                    UpParam.Cmd_Type = model.typeField;
                    UpParam.Details = model.other_DetField;
                    //UpParam.Status_Action = Status_Action
                    UpParam.Rec_ID = model.ID;
                    if (BASE._telephoneDBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Telephone";
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

                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    // DELETE                  

                    if (BASE._telephoneDBOps.Delete(model.ID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Telephone";
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
                jsonParam.result = true;
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

        #endregion

        #region "Start--> LookupEdit Events"
        [HttpGet]
        public ActionResult RefreshTelcomList()
        {
            DataTable telecomlist = BASE._telephoneDBOps.GetTelecomCompanies("MISC_ID", "MISC_NAME");
            if (telecomlist == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            Telecomdata = DatatableToModel.DataTabletoTelecom_INFO(telecomlist);
            return Json(new
            {
                message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_TeleCom_List(DataSourceLoadOptions loadOptions)
        {
            if (Telecomdata == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Telecom_INFO>(), loadOptions)), "application/json");
            }
           
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Telecomdata, loadOptions)), "application/json");
        }
        #endregion

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Telephone, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Telephone_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }

        //public ActionResult test()
        //{
        //    return View(new FormViewModel
        //    {
        //        ID = 1,
        //        FirstName = "John",
        //        LastName = "Heart",
        //        CompanyName = "Super Mart of the West",
        //        Position = "CEO",
        //        OfficeNo = "901",
        //        BirthDate = new DateTime(1964, 3, 16),
        //        HireDate = new DateTime(1995, 1, 15),
        //        Address = "351 S Hill St.",
        //        City = "Los Angeles",
        //        //State = "CA",
        //        //Zipcode = "90013",
        //        //Phone = "+1(213) 555-9392",
        //        //Email = "jheart@dx-email.com",
        //        Skype = "jheart_DX_skype"
        //    });
        //}
        public ActionResult ExportTo()
        {
            GridViewExportFormat format = CommonFunctions.GetExportFormat(Request.Params["ExportFormat"]);
            if (GridViewExportHelper.ExportFormatsInfo.ContainsKey(format))
                return GridViewExportHelper.ExportFormatsInfo[format](GridViewExportHelper.ExportTelephoneGrid, Telephone_ExportData);
            return RedirectToAction("Frm_Telephone_Info");
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

                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Lbl_Status_Color,
                    Lbl_StatusBy = Lbl_StatusBy
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = ""
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
            ClearBaseSession("_Telephone");
            Session.Remove("TelephoneInfo_detailGrid_Data");

        }
        public void Telephone_user_rights()
        {
            ViewData["Telephone_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Telephone, "Add");
            ViewData["Telephone_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Telephone, "Update");
            ViewData["Telephone_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Telephone, "View");
            ViewData["Telephone_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Telephone, "Delete");
            ViewData["Telephone_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Telephone, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
    }
}