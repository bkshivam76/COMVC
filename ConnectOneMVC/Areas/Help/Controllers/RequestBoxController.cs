using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Help.Models;
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

namespace ConnectOneMVC.Areas.Help.Controllers
{
    public class RequestBoxController : BaseController
    {
        // GET: Help/RequestBox
        #region Global Variables
        public List<DbOperations.Request.Return_RequestInfo> Requests_ExportData
        {
            get
            {
                return (List<DbOperations.Request.Return_RequestInfo>)GetBaseSession("Requests_ExportData_ReqBox");
            }
            set
            {
                SetBaseSession("Requests_ExportData_ReqBox", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> RequestBoxInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("RequestBoxInfo_DetailGrid_Data_ReqBox");
            }
            set
            {
                SetBaseSession("RequestBoxInfo_DetailGrid_Data_ReqBox", value);
            }
        }
        public string Name { get { return "PopupControl"; } }
        #endregion        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Request_Info()
        {
            ReqBox_user_rights();
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Request_Box, "List"))
            {
                //ViewBag.ShowHorizontalBar = 0;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Request_Box).ToString()) ? 1 : 0;

                ViewBag.UserType = BASE._open_User_Type;
                var Request_Data = BASE._Req_DBOps.GetList();

                Requests_ExportData = Request_Data;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ReqBox_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View(Request_Data);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Help_Request_Box').hide();</script>");//Code written for User Authorization do not remove
            }
        }

        public PartialViewResult Frm_Request_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ReqBox_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Requests_ExportData == null || command == "REFRESH")
            {
                var Request_Data = BASE._Req_DBOps.GetList();
                Requests_ExportData = Request_Data;
            }
            return PartialView("Frm_Request_Info_Grid", Requests_ExportData);
        }
        #region nested grid
        public ActionResult Frm_Request_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.RequestInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.RequestInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Help_Request_Box);
            RequestBoxInfo_DetailGrid_Data = _docList;
            Session["RequestBoxInfo_detailGrid_Data"] = _docList;
            return PartialView(_docList);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "Request_InfoListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "Request_InfoListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["RequestBoxInfo_detailGrid_Data"];
        }
        #endregion
        public ActionResult Frm_Request_Window(string ActionMethod = null, string ID=null, string EditedOn = null,string _Send_From=null,string PopUpId= "popup_frm_RequestBox_Window", string AjaxSuccessForm=null,string CallingFrom=null)
        {
            RequestBox OBacc = new RequestBox();            
            OBacc.PopUpId = PopUpId != null ? PopUpId : "popup_frm_RequestBox_Window";

            if ((!CheckRights(BASE,ClientScreen.Help_Request_Box, "Add")) && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");//1251 bug fixed
            }

            OBacc.AjaxSuccessForm = AjaxSuccessForm != null ? AjaxSuccessForm : "RequestAjaxSuccessForm";
            if (CallingFrom == "Production_Scrap")
            {
                OBacc.Txt_Detail = "Please add Scrap Item Namely '________________'";
            }
            OBacc._Send_From = _Send_From != null ? _Send_From : "Direct Entry";            
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            OBacc.ActionMethod = Navigation_Mode_tag;
            OBacc.TempActionMethod = ActionMethod;
            DateTime? EditDate = Convert.ToDateTime(EditedOn);
            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                      || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                      || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                DataTable _dtableTelData = BASE._Req_DBOps.GetRecord(ID);
                if (_dtableTelData.Rows.Count > 0)
                { 
                   
                    OBacc.Txt_Detail = _dtableTelData.Rows[0]["R_DETAIL"].ToString();
                    OBacc.Txt_Administrator = _dtableTelData.Rows[0]["R_REMARKS"].ToString();
                    OBacc.recID = ID;
                }
               
            }
                return View(OBacc);
        }
        [HttpPost]
        public ActionResult Frm_Request_Window(RequestBox OBacc)
        {
            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
            {
                // new
                OBacc.status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);
                OBacc.recID = System.Guid.NewGuid().ToString();            
                Common_Lib.RealTimeService.Parameter_Insert_Request InParam = new Common_Lib.RealTimeService.Parameter_Insert_Request();
                InParam.Request = OBacc.Txt_Detail;
                InParam.RequestFrom = OBacc._Send_From;
                InParam.Status_Action = OBacc.status_Action.ToString();
                InParam.RecID = OBacc.recID;
              
                    if (BASE._Req_DBOps.Insert(InParam))
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
            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
            {
                // Edit
                Common_Lib.RealTimeService.Parameter_Update_Request UpParam = new Common_Lib.RealTimeService.Parameter_Update_Request();
                UpParam.Request = OBacc.Txt_Detail;                
                UpParam.Rec_ID = OBacc.recID;
                
                    if (BASE._Req_DBOps.Update(UpParam))
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
            if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + OBacc.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
            {
 
                    if (!BASE._Req_DBOps.Delete(OBacc.recID))
                    {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                }
              
            return Json(new
            {
                Message = Common_Lib.Messages.DeleteSuccess,
                result = true
            }, JsonRequestBehavior.AllowGet);
           
            
        }

        public JsonResult DataNavigation(string ActionMethod, string ID, string Edit_Date = null, string Status = null)
        {
            
            if ((!CheckRights(BASE,ClientScreen.Help_Request_Box, "Update")) && ActionMethod == "Edit")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if ((!CheckRights(BASE,ClientScreen.Help_Request_Box, "Update")) && ActionMethod == "Read")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if ((!CheckRights(BASE, ClientScreen.Help_Request_Box, "Update")) && ActionMethod == "UnRead")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if ((!CheckRights(BASE,ClientScreen.Help_Request_Box, "Delete")) && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if ((!CheckRights(BASE,ClientScreen.Help_Request_Box, "View")) && ActionMethod == "View")
            {
                return Json(new { result = "NoViewRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DateTime? EditDate = Convert.ToDateTime(Edit_Date);
                RequestBox model = new RequestBox();
               
                if (ActionMethod == "Edit")
                {                  
                    if ((Status == "PENDING"))
                    {
                        model.Edit_Date = string.IsNullOrEmpty(Edit_Date.ToString()) ? DateTime.Now : Convert.ToDateTime(Edit_Date);
                    }
                  else
                    {
                        return Json(new { Message = "Request cannot be Edit / Delete...!", result = false }, JsonRequestBehavior.AllowGet);                    
                    }
                   

                }
                else if (ActionMethod == "Delete")
                {
                    if ((Status == "PENDING"))
                    {
                        model.Edit_Date = string.IsNullOrEmpty(Edit_Date.ToString()) ? DateTime.Now : Convert.ToDateTime(Edit_Date);
                    }
                    else
                    {
                        return Json(new { Message = "Request cannot be Edit / Delete...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "View")
                {
                    string xTemp_ID = ID;
                    bool MaxValue = false;
                    MaxValue = BASE._Req_DBOps.MarkAsRead(xTemp_ID);
                }
                else if (ActionMethod == "Read")
                {
                    string xTemp_ID = ID;
                    bool MaxValue = false;
                    MaxValue = BASE._Req_DBOps.MarkAsRead(xTemp_ID);
                    if (MaxValue == true)
                    {
                        return Json(new { Message = "Record status is read", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Message = "Some error occured", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "UnRead")
                {
                    string xTemp_ID = ID;
                    bool MaxValue = false;
                    MaxValue = BASE._Req_DBOps.MarkAsUnread(xTemp_ID);
                    if (MaxValue == true)
                    {
                        return Json(new { Message = "Record status is Unread", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Message = "Some error occured", result = false }, JsonRequestBehavior.AllowGet);
                    }
                   
                }

                DataTable _dtableTelData = BASE._Req_DBOps.GetRecord(ID);
                if (_dtableTelData.Rows.Count > 0)
                {
                    if (ActionMethod == "Delete" || ActionMethod == "View")
                    {
                        if ((EditDate != Convert.ToDateTime(_dtableTelData.Rows[0]["REC_EDIT_ON"])))
                        {
                            return Json(new { Message = "Record Already Changed!!", result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(new { Message = "Record Changed / Removed in Background!", result = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

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
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE,ClientScreen.Help_Request_Box, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Request_Info_report_modal','Not Allowed','No Rights');$('#Request_InfosModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_ReqBox");
            Session.Remove("RequestBoxInfo_detailGrid_Data");

        }
        public void ReqBox_user_rights()
        {
            ViewData["ReqBox_AddRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Add");
            ViewData["ReqBox_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Update");
            ViewData["ReqBox_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Delete");
            ViewData["ReqBox_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "View");
            ViewData["ReqBox_ExportRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
    }
}