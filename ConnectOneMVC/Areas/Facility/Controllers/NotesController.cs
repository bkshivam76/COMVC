using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Facility.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    [CheckLogin]
    public class NotesController : BaseController
    {
        // GET: Facility/Notes
        #region Global variables
        public List<DbOperations.Notes.Return_NotesInfo> NotesExportData
        {
            get
            {
                return (List<DbOperations.Notes.Return_NotesInfo>)GetBaseSession("NotesExportData_Notes");
            }
            set
            {
                SetBaseSession("NotesExportData_Notes", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> NotesInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("NotesInfo_DetailGrid_Data_Notes");
            }
            set
            {
                SetBaseSession("NotesInfo_DetailGrid_Data_Notes", value);
            }
        }        
        #endregion        
        public ActionResult Frm_Notes_Info()
        {
            if (CheckRights(BASE, ClientScreen.Facility_Notes, "List"))
            {
                CheckRights();
                var Notes = BASE._Notes_DBOps.GetList();
                if (Notes == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                NotesExportData = Notes;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["Notes_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

                ViewBag.ShowHorizontalBar = 0;
                ViewBag.UserType = BASE._open_User_Type;
                BASE.Refresh_Notes_List = true;
                return View(NotesExportData);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_Notes').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public PartialViewResult Frm_Notes_Info_Grid(string command = null, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false)
        {
            CheckRights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.VouchingMode=VouchingMode;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["Layout"] = Layout;
            if (NotesExportData == null || command == "REFRESH")
            {
                var Notesdata = BASE._Notes_DBOps.GetList();
                NotesExportData = Notesdata;
            }
            return PartialView(NotesExportData);
        }
        public ActionResult Frm_Notes_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.NotesInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.NotesInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Facility_Notes);
            NotesInfo_DetailGrid_Data = _docList;
            Session["NotesInfo_detailGrid_Data"] = _docList;
            return PartialView(_docList);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "Notes_InfoListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "Notes_InfoListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["NotesInfo_DetailGrid_Data"];
        }
        [HttpGet]
        public ActionResult Frm_Notes_Window(string ActionMethod = "", string ID = "", string Info_LastEditedOn = "")
        {
            NotesWindow model = new NotesWindow();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.TempActionMethod = model.ActionMethod.ToString();
            model.xID = ID;
            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                DataTable d1 = BASE._Notes_DBOps.GetRecord(model.xID);
                string message = "";
                if (d1 == null)
                {
                    message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Error');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete || model.ActionMethod == Common.Navigation_Mode._View)
                    {
                        if (Convert.ToDateTime(Info_LastEditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                        {
                            message = Messages.RecordChanged("Current Notes");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!');if (typeof Notes_InfoListGrid !== 'undefined'){Notes_InfoListGrid.Refresh();}</script>");
                        }
                    }
                }
                model.Txt_Desc_Notes = d1.Rows[0]["NOTE_DETAIL"].ToString();
                model.Txt_Status_Notes = d1.Rows[0]["NOTE_STATUS"].ToString();
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Notes_Window(NotesWindow model)
        {
            var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            if  ((!(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                        && !(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
            {
                jsonParam.message = "Not Allowed !!";
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable notes_DbOps = BASE._Notes_DBOps.GetRecord(model.xID);
                        if (notes_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (notes_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Notes");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(notes_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Notes");
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
                if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New)
                {
                    if (string.IsNullOrWhiteSpace(model.Txt_Desc_Notes))
                    {
                        jsonParam.message = "Notes Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Desc_Notes";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Txt_Desc_Notes = model.Txt_Desc_Notes.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                    }
                }
                if (Tag == Common.Navigation_Mode._New)
                {
                    model.xID = Guid.NewGuid().ToString();
                    Parameter_Insert_Notes InParam = new Parameter_Insert_Notes();
                    InParam.Note = model.Txt_Desc_Notes ?? "";
                    InParam.Status = model.Txt_Status_Notes ?? "";
                    InParam.Rec_ID = model.xID;
                    if (BASE._Notes_DBOps.Insert(InParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Notes";
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
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    Parameter_Update_Notes UpParam = new Parameter_Update_Notes();
                    UpParam.Note = model.Txt_Desc_Notes ?? "";
                    UpParam.Status = model.Txt_Status_Notes ?? "";
                    UpParam.Rec_ID = model.xID;
                    if (BASE._Notes_DBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Notes";
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
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    if (BASE._Notes_DBOps.Delete(model.xID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Notes";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
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
        public ActionResult ChangeStatus(string Status = "", string ID = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (Status.ToLower() == "completed")
                {
                    if (BASE._Notes_DBOps.Complete(ID))
                    {
                        jsonParam.message = "Status Updated Successfully...!";
                        jsonParam.title = "Notes";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = ID
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
                else {
                    if (BASE._Notes_DBOps.Incomplete(ID))
                    {
                        jsonParam.message = "Status Updated Successfully...!";
                        jsonParam.title = "Notes";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = ID
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
            ClearBaseSession("_Notes");
            Session.Remove("NotesInfo_detailGrid_Data");
        }
        public void CheckRights()
        {
            ViewData["Notes_NewRight"] = CheckRights(BASE, ClientScreen.Facility_Notes, "Add");
            ViewData["Notes_EditRight"] = CheckRights(BASE, ClientScreen.Facility_Notes, "Update");
            ViewData["Notes_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_Notes, "Delete");
            ViewData["Notes_ExportRight"] = CheckRights(BASE, ClientScreen.Facility_Notes, "Export");//Redmine bug 132847 fixed
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
        }
        #region export       
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Facility_Notes, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Notes_Info_report_modal','Not Allowed','No Rights');$('#Notes_InfosModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
    }
}