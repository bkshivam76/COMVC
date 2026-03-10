using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Help.Models;
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


namespace ConnectOneMVC.Areas.Help.Controllers
{
    [CheckLogin]
    public class ActionItemsController : BaseController
    {
        // GET: Help/ActionItems
        #region Global Variables
        public List<ActionItemsInfo> ActionItems_ExportData
        {
            get
            {
                return (List<ActionItemsInfo>)GetBaseSession("ActionItems_ExportData_ActionItem");
            }
            set
            {
                SetBaseSession("ActionItems_ExportData_ActionItem", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> ActionItemsInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("ActionItemsInfo_DetailGrid_Data_ActionItem");
            }
            set
            {
                SetBaseSession("ActionItemsInfo_DetailGrid_Data_ActionItem", value);
            }
        }
 
        #endregion
        #region "Start--> Procedures" (Default Grid Page Action Method GET: Help/ActionItems)
        public ActionResult Frm_Action_Items_Info(string RefScreen = "", string RefTable = "", string RefRecID = "", string Status = "", string PopupID = "",string GridToBeRefreshed="")//redmine Bug #132876 fixed
        {
            ActionItems_user_rights();
            ViewBag.RefScreen = RefScreen;
            ViewBag.RefTable = RefTable;
            ViewBag.RefRecID = RefRecID;
            ViewBag.Status = Status;
            ViewBag.PopupId = PopupID;
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Action_Items).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.GridToBeRefreshed = GridToBeRefreshed;
            if (CheckRights(BASE, ClientScreen.Help_Action_Items, "List"))
            {
                if (BASE._Action_Items_DBOps == null)
                {
                    BASE.Get_Configure_Setting();
                }
                DataTable d1 = null;
                if (string.IsNullOrWhiteSpace(RefRecID))
                {
                    d1 = BASE._Action_Items_DBOps.GetList();
                }
                else
                {
                    d1 = BASE._Action_Items_DBOps.GetList(RefRecID, RefTable);
                }
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                ActionItems_ExportData = ConverToList(d1);
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ActionItemsInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

                return View(ActionItems_ExportData);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopupID))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopupID + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
        }
        public ActionResult Frm_Action_Items_Info_Grid(string command, string RefScreen = "", string RefTable = "", string RefRecID = "", string Status = "", int ShowHorizontalBar = 0, bool VouchingMode = false, string Layout = null, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ActionItems_user_rights();
            ViewBag.RefScreen = RefScreen;
            ViewBag.RefTable = RefTable;
            ViewBag.RefRecID = RefRecID;
            ViewBag.Status = Status;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (ActionItems_ExportData == null || command == "REFRESH")
            {
                if (BASE._Action_Items_DBOps == null)
                {
                    BASE.Get_Configure_Setting();
                }
                DataTable d1 = null;
                if (string.IsNullOrWhiteSpace(RefRecID))
                {
                    d1 = BASE._Action_Items_DBOps.GetList();
                }
                else
                {
                    d1 = BASE._Action_Items_DBOps.GetList(RefRecID, RefTable);
                }
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                ActionItems_ExportData = ConverToList(d1);
            }
            return PartialView(ActionItems_ExportData);
        }
        #region nested grid
        public ActionResult Frm_Action_Items_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.ActionItemsInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ActionItemsInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Help_Action_Items);
            ActionItemsInfo_DetailGrid_Data = _docList;
            Session["ActionItemsInfo_detailGrid_Data"] = _docList;
            return PartialView(_docList);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ActionItemsListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "ActionItemsListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["ActionItemsInfo_detailGrid_Data"];
        }
        #endregion
        #endregion

        #region Add/Edit Audit Action Details for popup

        [HttpGet]
        public ActionResult Frm_Action_Items_Window(string ActionMethod = null, string Info_LastEditedOn = null, string ID = null, string RefScreen = "", string RefTable = "", string RefRecID = "", string PopupID = "Dynamic_Content_popup", string PostSuccessFunction = "", string GridToBeRefreshed = "")
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View" };
            string[] AM = { "New", "Edit", "View" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Help_Action_Items, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','Not Allowed','No Rights');</script>");
                }
            }
            if ((ActionMethod == "Edit" || ActionMethod == "New") && !BASE.CheckActionRights(ClientScreen.Help_Action_Items, Common.ClientAction.Add_Edit_Action))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','Not Allowed','No Rights');</script>");
            }
            string message = "";
            ActionItems model = new ActionItems();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            model.RefRecID = RefRecID;
            model.RefScreen = RefScreen;
            model.RefTable = RefTable;
            model.GridToBeRefreshed = GridToBeRefreshed;
            ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "Dynamic_Content_popup";
            ViewBag.PostSuccessFunction = PostSuccessFunction.Length > 0 ? PostSuccessFunction : "ActionItemsAjaxSuccessForm";
            model.xID = ID;
            model.TitleX = "Audit Actions";
            DataTable action_Table = BASE._Action_Items_DBOps.GetActionTitles("ID", "Name") as DataTable;           
            ViewBag.Titledatalist = DatatableToModel.DataTabletoActionItems_GetTitleList_INFO(action_Table); 
            model.CurrDateTime = (DateTime)BASE._Action_Items_DBOps.GetServerDateTime();
            if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete || Tag == Common.Navigation_Mode._Close || Tag == Common.Navigation_Mode._Re_Open || Tag == Common.Navigation_Mode._View)
            {
                DataTable d1 = BASE._Action_Items_DBOps.GetRecord(model.xID);
                if (d1 == null)
                {
                    message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','" + message + "','Error');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    message = Messages.RecordChanged("Current Action");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','" + message + "','Record Changed / Removed in Background!!');  if (typeof ActionItemsListGrid !== 'undefined'){ActionItemsListGrid.Refresh();}</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete || Tag == Common.Navigation_Mode._Re_Open || Tag == Common.Navigation_Mode._View)
                    {
                        //var Info_LastEditedOn_ = ActionItems_ExportData.Where(x => x.ID == ID).FirstOrDefault();
                        //if (Convert.ToDateTime(Info_LastEditedOn_.Edit_Date) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        if (Convert.ToDateTime(Info_LastEditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            message = Messages.RecordChanged("Current Action");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','" + message + "','Record Already Changed!!');  if (typeof ActionItemsListGrid !== 'undefined'){ActionItemsListGrid.Refresh();}</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_Status_AI = d1.Rows[0]["AI_STATUS"].ToString();
                model.Cmd_Title_AI = d1.Rows[0]["AI_TITLE"].ToString();
                model.Txt_Contact_Person_AI = d1.Rows[0]["AI_CONTACT_NAME"].ToString();
                model.Txt_ContactNo_AI = d1.Rows[0]["AI_CONTACT_NUMBER"].ToString();
                model.Txt_Auditor_AI = d1.Rows[0]["REC_ADD_BY"].ToString();
                string[] str = d1.Rows[0]["AI_REMARKS"].ToString().Split(new string[] {"*--","--*"},StringSplitOptions.None);
                //string[] str = Regex.Split(d1.Rows[0]["AI_REMARKS"].ToString(), @"\*--");
                model.Txt_Detail_AI = string.Join("\n", str).ToString();
                //model.Txt_Detail_AI = d1.Rows[0]["AI_REMARKS"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["AI_CEN_REMARKS"]))
                {
                    model.Txt_Centre_Remarks_AI = d1.Rows[0]["AI_CEN_REMARKS"].ToString();
                }
                model.Txt_Closure_Remarks_AI = d1.Rows[0]["AI_CLOSURE_REMARKS"].ToString();
                if (Tag != Common.Navigation_Mode._Close)
                {
                    model.Txt_ClosedBy_AI = d1.Rows[0]["AI_CLOSURE_BY"].ToString();
                }
                model.Cmb_Type_AI = d1.Rows[0]["AI_TYPE"].ToString();
                model.Cmb_Due_On_AI = d1.Rows[0]["AI_DUE_TYPE"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["REC_ADD_ON"]))
                {
                    model.Txt_AddedOn_AI = Convert.ToDateTime(d1.Rows[0]["REC_ADD_ON"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["AI_DUE_ON"]))
                {
                    model.Txt_DueDate_AI = Convert.ToDateTime(d1.Rows[0]["AI_DUE_ON"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["AI_CLOSURE_ON"]) && Tag != Common.Navigation_Mode._Close)
                {
                    model.Txt_ClosedDate_AI = Convert.ToDateTime(d1.Rows[0]["AI_CLOSURE_ON"]);
                }
                model.xID = d1.Rows[0]["REC_ID"].ToString();
                if (Tag == Common.Navigation_Mode._Re_Open)
                {
                    model.OrgRecID = model.xID;
                    model.xID = Guid.NewGuid().ToString();
                    model.Txt_Detail_AI = model.Txt_Detail_AI + " \n *--Action Item Reopened on " + model.CurrDateTime + " by " + BASE._open_User_ID + "--* \n ";
                    model.Txt_Closure_Remarks_AI = "";
                }
            }
            if (Tag == Common.Navigation_Mode._Close)
            {
                model.Txt_ClosedDate_AI = Convert.ToDateTime(model.CurrDateTime);
                model.Txt_ClosedBy_AI = BASE._open_User_ID;
            }
            if (Tag == Common.Navigation_Mode._Re_Open)
            {
                model.Txt_ClosedDate_AI = Convert.ToDateTime(model.CurrDateTime);
                model.Txt_ClosedBy_AI = BASE._open_User_ID;
            }
            if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Re_Open)
            {
                model.Txt_AddedOn_AI = Convert.ToDateTime(model.CurrDateTime);
                model.Txt_Auditor_AI = BASE._open_User_ID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Action_Items_Window(ActionItems model)
        {
            var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete || Tag == Common.Navigation_Mode._Re_Open)
                    {
                        DataTable actionitems_DbOps = null;
                        if (!(Tag == Common.Navigation_Mode._Re_Open))
                        {
                            actionitems_DbOps = BASE._Action_Items_DBOps.GetRecord(model.xID);
                        }
                        else
                        {
                            actionitems_DbOps = BASE._Action_Items_DBOps.GetRecord(model.OrgRecID);
                        }
                        if (actionitems_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (actionitems_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Action");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((model.LastEditedOn != Convert.ToDateTime(actionitems_DbOps.Rows[0]["REC_EDIT_ON"])))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Action");
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
                bool MarkClosed = false;
                model.Txt_Closure_Remarks_AI = BASE.CleanTextForDB(string.IsNullOrEmpty(model.Txt_Closure_Remarks_AI) ? "" : model.Txt_Closure_Remarks_AI);
                model.Txt_Contact_Person_AI = BASE.CleanTextForDB(string.IsNullOrEmpty(model.Txt_Contact_Person_AI) ? "" : model.Txt_Contact_Person_AI);
                model.Txt_ContactNo_AI = BASE.CleanTextForDB(string.IsNullOrEmpty(model.Txt_ContactNo_AI) ? "" : model.Txt_ContactNo_AI);
                model.Txt_Detail_AI = BASE.CleanTextForDB(string.IsNullOrEmpty(model.Txt_Detail_AI) ? "" : model.Txt_Detail_AI);
                model.Cmd_Title_AI = BASE.CleanTextForDB(string.IsNullOrEmpty(model.Cmd_Title_AI) ? "" : model.Cmd_Title_AI);
                if (model.MarkClosed == "true")
                {
                    MarkClosed = true;
                }
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Re_Open || Tag == Common.Navigation_Mode._Close)
                {
                    if (string.IsNullOrWhiteSpace(model.Cmd_Title_AI))
                    {
                        jsonParam.message = "Action Title Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Title_AI";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Cmb_Due_On_AI.ToUpper() == "SPECIFIC DATE" && model.Txt_DueDate_AI == null)
                    {
                        jsonParam.message = "Due Date Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_DueDate_AI";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrWhiteSpace(model.Txt_Closure_Remarks_AI) && (MarkClosed == true || Convert.ToInt16(Tag) == 4))
                    {
                        jsonParam.message = "Closure Remarks Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Closure_Remarks_AI";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string Status_Action = "";
                Status_Action = ((int)Common.Record_Status._Completed).ToString();
                if (Tag.ToString() == "_Delete")
                {
                    Status_Action = ((int)Common.Record_Status._Deleted).ToString();
                }
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Re_Open)
                {
                    // new or Reopened
                    bool Result = true;
                    model.xID = Guid.NewGuid().ToString();
                    string Status = ActionStatus.Open.ToString();
                    if (Tag == Common.Navigation_Mode._Re_Open)
                    {
                        Status = ActionStatus.ReOpened.ToString();
                    }
                    Param_Txn_Insert_ActionItems InNewParam = new Param_Txn_Insert_ActionItems();
                    Parameter_Insert_Action_Items InParam = new Parameter_Insert_Action_Items();
                    InParam.Status = Status;
                    InParam.Remarks = model.Txt_Detail_AI ?? "";
                    InParam.Type = model.Cmb_Type_AI ?? "";
                    InParam.Title = model.Cmd_Title_AI ?? "";
                    InParam.DueType = model.Cmb_Due_On_AI ?? "";
                    if (IsDate(model.Txt_DueDate_AI.ToString()))
                    {
                        InParam.DueOn = Convert.ToDateTime(model.Txt_DueDate_AI).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.ContactName = model.Txt_Contact_Person_AI ?? "";
                    InParam.ContactNo = model.Txt_ContactNo_AI ?? "";
                    InParam.Ref_Table = model.RefTable ?? "";
                    InParam.Ref_Screen = model.RefScreen ?? "";
                    InParam.Ref_Rec_ID = model.RefRecID ?? "";
                    InParam.RecID = model.xID;
                    InNewParam.param_InsertActionItems = InParam;
                    if (MarkClosed)
                    {
                        Parameter_Close_Action_Items Cls = new Parameter_Close_Action_Items();
                        Cls.closeRemarks = model.Txt_Closure_Remarks_AI ?? "";
                        Cls.RecId = model.xID.ToString();
                        InNewParam.param_CloseActionItems = Cls;
                    }
                    if (!BASE._Action_Items_DBOps.InsertActionItems_Txn(InNewParam))
                    {
                        Result = false;
                    }
                    if (Result)
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
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
                Param_Txn_Update_ActionItems EditParam = new Param_Txn_Update_ActionItems();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    // edit
                    bool Result = true;
                    Parameter_Update_Action_Items UpParam = new Parameter_Update_Action_Items();
                    UpParam.Status = model.Txt_Status_AI ?? "";
                    UpParam.Remarks = model.Txt_Detail_AI ?? "";
                    UpParam.Type = model.Cmb_Type_AI ?? "";
                    UpParam.Title = model.Cmd_Title_AI ?? "";
                    UpParam.DueType = model.Cmb_Due_On_AI ?? "";
                    if (IsDate(model.Txt_DueDate_AI.ToString()))
                    {
                        UpParam.DueOn = Convert.ToDateTime(model.Txt_DueDate_AI).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.ContactName = model.Txt_Contact_Person_AI ?? "";
                    UpParam.ContactNo = model.Txt_ContactNo_AI ?? "";
                    UpParam.Ref_Table = model.RefTable ?? "";
                    UpParam.Ref_Screen = model.RefScreen ?? "";
                    UpParam.Ref_Rec_ID = model.RefRecID ?? "";
                    UpParam.RecID = model.xID;
                    EditParam.param_UpdateActionItems = UpParam;
                    if (MarkClosed)
                    {
                        Parameter_Close_Action_Items cls = new Parameter_Close_Action_Items();
                        cls.closeRemarks = model.Txt_Closure_Remarks_AI ?? "";
                        cls.RecId = model.xID.ToString();
                        EditParam.param_CloseActionItems = cls;
                    }
                    if (!BASE._Action_Items_DBOps.UpdateActionItems_Txn(EditParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
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

                if (Tag == Common.Navigation_Mode._Close)
                {
                    bool Result = true;
                    Parameter_Close_Action_Items Cls = new Parameter_Close_Action_Items();
                    Cls.closeRemarks = model.Txt_Closure_Remarks_AI ?? "";
                    Cls.RecId = model.xID;
                    if (!BASE._Action_Items_DBOps.Close(Cls))
                    {
                        Result = false;
                    }
                    if (Result)
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
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
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Centre_Remarks_Window(string ID = null,string ActionMethod=null)

        {
            CentreRemarks model = new CentreRemarks();
            var ActionItemData = ActionItems_ExportData.Where(x => x.ID == ID).FirstOrDefault();

            string xTemp_ID = ID;
            string xTemp_Status = ActionItemData.Status;
            string xTemp_Title = ActionItemData.Title;
            string xTemp_Desc = ActionItemData.Description;
            string xTemp_Remarks = ActionItemData.Centre_Remarks;
            model.ActionMethod= (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.TempActionMethod = model.ActionMethod.ToString();
            model.xID = xTemp_ID;
            model.Txt_Centre_Remarks_AI = xTemp_Remarks;
            model.xNAME_AI = (xTemp_Title + ("\r\n" + ("------------------------------------------------------------------------------------------------" + ("\r\n" + xTemp_Desc))));
                 
            return View(model);

        }

        [HttpPost]
        public ActionResult Frm_Centre_Remarks_Window(ActionItems model)
        {
            try
            {
                if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.Txt_Centre_Remarks_AI))
                    {
                        return Json(new
                        {
                            message = "Centre Remarks cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Txt_Centre_Remarks_AI = model.Txt_Centre_Remarks_AI.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                    }
                }
                Parameter_Update_Action_Items UpParam = new Parameter_Update_Action_Items();
                UpParam.CentreRemarks = model.Txt_Centre_Remarks_AI;
                UpParam.RecID = model.xID;
                if (BASE._Action_Items_DBOps.UpdateCentreRemarks(UpParam))
                {
                    return Json(new
                    {
                        message = Messages.UpdateSuccess,
                        xid = model.xID,
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
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);             
                return Json(new
                {
                    message= msg,
                    result = false
            }, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
               
      

        #region Export
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Action_Items, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Action_Items_report_modal','Not Allowed','No Rights');$('#ActionItemBUT_PRINT').hide();</script>");
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
            ClearBaseSession("_ActionItem");
            Session.Remove("ActionItemsInfo_detailGrid_Data");

        }
        public void ActionItems_user_rights()
        {
            ViewData["ActionItems_AddRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Add");
            ViewData["ActionItems_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Update");
            ViewData["ActionItems_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Delete");
            ViewData["ActionItems_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "View");
            ViewData["ActionItems_ExportRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["ActionItems_Add_Edit_Action"] = BASE.CheckActionRights(ClientScreen.Help_Action_Items, Common.ClientAction.Add_Edit_Action);
            ViewData["ActionItems_Close_Reopen_Actions"] = BASE.CheckActionRights(ClientScreen.Help_Action_Items, Common.ClientAction.Close_Reopen_Actions);
        }
        public List<ActionItemsInfo> ConverToList(DataTable d1)
        {
            List<ActionItemsInfo> actionItems = new List<ActionItemsInfo>();
            foreach (DataRow row in d1.Rows)
            {
                ActionItemsInfo newdata = new ActionItemsInfo();

                newdata.Type = row["Type"].ToString();
                newdata.Status = row["Status"].ToString();
                newdata.Date = Convert.IsDBNull(row["Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Date"]);
                newdata.Auditor = row["Auditor"].ToString();
                newdata.Title = row["Title"].ToString();
                newdata.Description = row["Description"].ToString();
                newdata.Due_On = IsDate(row["Due On"].ToString()) ? Convert.ToDateTime(row["Due On"]).ToString("dd-MMM-yyyy") : row["Due On"].ToString();
                newdata.Centre_Remarks = row["Centre Remarks"].ToString();
                newdata.Close_Remarks = row["Close Remarks"].ToString();
                newdata.Closed_On = row["Closed On"].ToString();
                newdata.Closed_By = row["Closed By"].ToString();
                newdata.ID = row["ID"].ToString();
                newdata.CrossedTimeLimit = Convert.ToInt16(row["CrossedTimeLimit"]);
                newdata.Add_By = row["Add By"].ToString();
                newdata.Add_Date = Convert.ToDateTime(row["Add Date"]);
                newdata.Edit_By = row["Edit By"].ToString();
                newdata.Edit_Date = Convert.IsDBNull(row["Edit Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Edit Date"]);
                newdata.Action_Status = row["Action Status"].ToString();
                newdata.Action_By = row["Action By"].ToString();
                newdata.Action_Date = Convert.IsDBNull(row["Action Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Action Date"]);
                newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);//redmine bug 133067 fixed
                newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);//redmine bug 133067 fixed
                newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);//redmine bug 133067 fixed
                newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);//redmine bug 133067 fixed
                newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
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
                actionItems.Add(newdata);
            }

            //actionItems = (from DataRow row in d1.AsEnumerable()
            //               select new ActionItemsInfo
            //               {
            //                   Type = row["Type"].ToString(),
            //                   Status = row["Status"].ToString(),
            //                   Date = Convert.IsDBNull(row["Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Date"]),
            //                   Auditor = row["Auditor"].ToString(),
            //                   Title = row["Title"].ToString(),
            //                   Description = row["Description"].ToString(),
            //                   Due_On = IsDate(row["Due On"].ToString()) ? Convert.ToDateTime(row["Due On"]).ToString("dd-MMM-yyyy") : row["Due On"].ToString(),
            //                   Centre_Remarks = row["Centre Remarks"].ToString(),
            //                   Close_Remarks = row["Close Remarks"].ToString(),
            //                   Closed_On = row["Closed On"].ToString(),
            //                   Closed_By = row["Closed By"].ToString(),
            //                   ID = row["ID"].ToString(),
            //                   CrossedTimeLimit = Convert.ToInt16(row["CrossedTimeLimit"]),
            //                   Add_By = row["Add By"].ToString(),
            //                   Add_Date = Convert.ToDateTime(row["Add Date"]),
            //                   Edit_By = row["Edit By"].ToString(),
            //                   Edit_Date = Convert.IsDBNull(row["Edit Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Edit Date"]),
            //                   Action_Status = row["Action Status"].ToString(),
            //                   Action_By = row["Action By"].ToString(),
            //                   Action_Date = Convert.IsDBNull(row["Action Date"]) ? (DateTime?)null : Convert.ToDateTime(row["Action Date"]),
            //                   REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]),//redmine bug 133067 fixed
            //                   COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]),//redmine bug 133067 fixed
            //                   RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]),//redmine bug 133067 fixed
            //                   REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]),//redmine bug 133067 fixed
            //                   OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]),
            //                   ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]),

            //               }).ToList();
            return actionItems;
        }



        #region Devextreme
        public ActionResult Frm_Action_Items_Info_dx(string PopupId = null)
        {
            ActionItems_user_rights();
           
           
            if (CheckRights(BASE, ClientScreen.Help_Action_Items, "List"))
            {
                ViewBag.PopupID = PopupId == null ? "" : PopupId;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Action_Items).ToString()) ? 1 : 0;
                ViewBag.UserType = BASE._open_User_Type; ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ActionItemsInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

                return View();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopupId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopupId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
        }

        [HttpGet]
        public ActionResult Action_Items_Info_Grid_Load(string OpenPeriod = null)
        {
            if (BASE._Action_Items_DBOps == null)
            {
                BASE.Get_Configure_Setting();
            }
            ActionItems_ExportData = ConverToList(BASE._Action_Items_DBOps.GetList());
            return Content(JsonConvert.SerializeObject(ActionItems_ExportData), "application/json");

        }
        public ActionResult Action_Items_Info_detailGrid_dx(string RecID = "")
        {
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Help_Action_Items);
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Help_Action_Items)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Action_Items, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Action_Items_report_modal','Not Allowed','No Rights');$('#ActionItemBUT_PRINT').hide();</script>");
            }
            return PartialView();
        }
        #endregion
    }
}