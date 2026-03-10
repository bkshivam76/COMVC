using ConnectOneMVC.Areas.Facility.Models;
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
using System.Text.RegularExpressions;
using ConnectOne.D0010._001;



namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class LettersController : BaseController
    {
        // GET: Facility/Letters
        #region Global Variables
        public List<DbOperations.Letters.Return_LetterInfo> LetterExportData
        {
            get
            {
                return (List<DbOperations.Letters.Return_LetterInfo>)GetBaseSession("LetterExportData_Letter");
            }
            set
            {
                SetBaseSession("LetterExportData_Letter", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> LetterInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("LetterInfo_DetailGrid_Data_Letter");
            }
            set
            {
                SetBaseSession("LetterInfo_DetailGrid_Data_Letter", value);
            }
        }
        public bool IsMobile
        {
            get
            {
                return (bool)GetBaseSession("IsMobile_Letter");
            }
            set
            {
                SetBaseSession("IsMobile_Letter", value);
            }
        }

        DateTime? dtnull = null;

        #endregion
        public ActionResult Frm_Letter_Info()
        {
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_Letter, "List"))
            {
                Letter_UserRights();
                var _Letterdata = BASE._Letters_DBOps.GetList();
                LetterExportData = _Letterdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewBag.ShowHorizontalBar = 0;
                ViewBag.UserType = BASE._open_User_Type;
                ViewData["Letter_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

                if (_Letterdata.Count == 0)
                {
                    return View(_Letterdata);//Redmine bug 132848 fixed
                }

                return View(_Letterdata);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_Letter').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public PartialViewResult Frm_Letter_Info_Grid(string command = null, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Letter_UserRights();
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (LetterExportData == null || command == "REFRESH")
            {
                var Letterdata = BASE._Letters_DBOps.GetList();
                LetterExportData = Letterdata;
            }
            var _Letterdata = LetterExportData as List<Common_Lib.DbOperations.Letters.Return_LetterInfo>;
            if (_Letterdata.Count == 0)
            {
                return PartialView();
            }
            return PartialView(_Letterdata);
        }
        public ActionResult Frm_Letter_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.LetterInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.LetterInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Facility_Letter);
            LetterInfo_DetailGrid_Data = _docList;
            Session["LetterInfo_detailGrid_Data"] = _docList;
            return PartialView(_docList);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "Letter_InfoListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "Letter_InfoListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["LetterInfo_detailGrid_Data"];
        }
        [HttpGet]
        public ActionResult Frm_Letter_Window(string Tag = "_New", string Info_LastEditedOn = null, string PopupName = "Dynamic_Content_popup", string GridToRefresh = "Letter_InfoListGrid", string xID = "")
        {
            ViewBag.NullValuePrompt = BASE._Date_Format_Current;
            LetterWindow model = new LetterWindow();
            model.ActionMethod = Tag;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.Look_InsList_Data = LookUp_GetInsList_LWF();
            model.TitleX_LWF = "Letter";
            model.xFrm_Text_LWF = "New ~ ";
            model.xID = xID;
            model.Info_LastEditedOn = Convert.IsDBNull(Info_LastEditedOn) ? dtnull : Convert.ToDateTime(Info_LastEditedOn);

            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
            {
                DataTable d1 = BASE._Letters_DBOps.GetRecord(model.xID);

                //-----------------------------+
                //Start :Checks for Existence of Record, and has not been deleted in background by other user 
                //-----------------------------+
                var viewstr = "";
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    string message = Messages.RecordChanged("Current Letter", viewstr);
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Changed / Removed in Background!!','" + GridToRefresh + "');</script>");
                }
                //-----------------------------+
                //End:Checks for Existence of Record, and has not been deleted in background by other user 
                //-----------------------------+

                //-----------------------------+
                //Start : Check if entry already changed 
                //-----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        if (model.Info_LastEditedOn != (DateTime)d1.Rows[0]["REC_EDIT_ON"])
                        {
                            string message = Messages.RecordChanged("Current Letter", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                //-----------------------------+
                //End : Check if entry already changed 
                //-----------------------------+

                Data_Binding_LWF(ref model, d1);
            }

            return PartialView(model);
        }
        public void Data_Binding_LWF(ref LetterWindow model, DataTable d1)
        {
            model.Look_InsList_LWF = d1.Rows[0]["L_H_INS_ID"].ToString();

            DateTime? xDate = null;
            xDate = Convert.IsDBNull(d1.Rows[0]["L_DATE"]) ? dtnull : Convert.ToDateTime(d1.Rows[0]["L_DATE"]);
            model.Txt_Date_LWF = xDate;

            model.Txt_Ref_LWF = Convert.IsDBNull(d1.Rows[0]["L_REF"]) ? "" : Convert.ToString(d1.Rows[0]["L_REF"]).HandleEscapeCharacters();

            model.List_Lang_LWF = Convert.IsDBNull(d1.Rows[0]["L_LANG"]) ? "" : Convert.ToString(d1.Rows[0]["L_LANG"]);

            //model.List_Lang_LWF = model.List_Lang_LWF == "ENGLISH" ? "0" : "1";

            model.Txt_Matter_LWF = Convert.IsDBNull(d1.Rows[0]["L_MATTER"]) ? "" : Uri.EscapeDataString(d1.Rows[0]["L_MATTER"].ToString()).HandleEscapeCharacters();

            model.LastEditedOn = Convert.IsDBNull(d1.Rows[0]["REC_EDIT_ON"]) ? dtnull : Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
        }
        
        [HttpPost] 
        public ActionResult Frm_Letter_Window(LetterWindow model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            bool Result = true;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            try
            {
                //-----------------------------+
                //Start : Check if entry already changed 
                //-----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        DataTable letter_DbOps = BASE._Letters_DBOps.GetRecord(model.xID);
                        if (letter_DbOps == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                        }
                        if (letter_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Letter");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.LastEditedOn != Convert.ToDateTime(letter_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Letter");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                //-----------------------------+
                //End : Check if entry already changed 
                //-----------------------------+

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.Look_InsList_LWF) || (model.Look_InsList_LWF.ToString().Trim().Length == 0))
                    {
                        jsonParam.message = "Institute Name Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Look_InsList_LWF";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_Date_LWF.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Date_LWF";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New)        //new
                {                    
                    model.xID = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_Letters InParam = new Common_Lib.RealTimeService.Parameter_Insert_Letters();
                    InParam.InstID = model.Look_InsList_LWF;
                    InParam.LetterDate = Convert.ToDateTime(model.Txt_Date_LWF).ToString(BASE._Server_Date_Format_Long);
                    InParam.Reference = model.Txt_Ref_LWF ?? "";
                    InParam.Language = model.List_Lang_LWF == "0" ? "ENGLISH" : "HINDI";
                    InParam.Matter = model.Txt_Matter_LWF ?? "";
                    InParam.Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                    InParam.Rec_ID = model.xID;

                    if (!BASE._Letters_DBOps.Insert(InParam))
                    { Result = false; }
                    if (Result)
                    {
                        jsonParam.message = Common_Lib.Messages.SaveSuccess;
                        jsonParam.title = model.TitleX_LWF;
                        jsonParam.closeform = true;
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.closeform = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit) //edit
                {
                    Common_Lib.RealTimeService.Parameter_Update_Letters UpParam = new Common_Lib.RealTimeService.Parameter_Update_Letters();
                    UpParam.InstID = model.Look_InsList_LWF;
                    UpParam.LetterDate = Convert.ToDateTime(model.Txt_Date_LWF).ToString(BASE._Server_Date_Format_Long);
                    UpParam.Reference = model.Txt_Ref_LWF ?? "";
                    UpParam.Language = model.List_Lang_LWF == "0" ? "ENGLISH" : "HINDI";
                    UpParam.Matter = model.Txt_Matter_LWF ?? "";
                    //UpParam.Status_Action = Common_Lib.Common.Record_Status._Completed;
                    UpParam.Rec_ID = model.xID;

                    if (!BASE._Letters_DBOps.Update(UpParam)) { Result = false; }
                    if (Result)
                    {
                        jsonParam.message = Common_Lib.Messages.SaveSuccess;
                        jsonParam.title = model.TitleX_LWF;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.closeform = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if(model.Tag == Common_Lib.Common.Navigation_Mode._Delete) //DELETE
                {
                    if (!BASE._Letters_DBOps.Delete(model.xID)) { Result = false; }
                    if (Result) 
                    {
                        jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                        jsonParam.title = model.TitleX_LWF;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.closeform = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
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
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_Letter_Print_Dialog(string xTemp_ID, string xTemp_Title = "")
        {
            LetterPrint_Dialog model = new LetterPrint_Dialog();
            model.xTemp_ID_LPD = xTemp_ID;
            model.xTemp_Title_LPD = xTemp_Title;

            return PartialView(model);
        }
        public ActionResult LetterWriting_Print(string ID, string oPaperType, double oTopMargin) 
        {
            IsMobile = isMobileBrowser();
            ViewBag.IsMobile = IsMobile;
            ConnectOne.D0010._001.Print_Letter xRep = new ConnectOne.D0010._001.Print_Letter();
            xRep.Xid = ID;
            xRep.Title = "Letter";
            xRep.Xoption = (Convert.ToInt16(oPaperType) + 1 ).ToString();
            xRep.XTopMargin = oTopMargin;
            xRep.MainBase = BASE;

            //xRep.ShowPreviewDialog();
            return View(xRep);
        }
        public List<Institute_Info> LookUp_GetInsList_LWF() 
        {
            var d1 = BASE._Letters_DBOps.GetInstitutes();

            DataRow ROW = d1.NewRow();
            ROW["INS_NAME"] = "- NONE -";
            ROW["INS_SHORT"] = "NONE";
            ROW["INS_ID"] = "00000";
            d1.Rows.Add(ROW);
            DataView dview = new DataView(d1);
            dview.Sort = "INS_ID";

            return DatatableToModel.DataTabletoLookUp_GetInsList_LWF(dview.ToTable());
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
                    Lbl_StatusBy = "Status By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
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
        public void SessionClear()
        {
            ClearBaseSession("_Letter");
            Session.Remove("LetterInfo_detailGrid_Data");
        }

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Facility_Letter, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Letter_Info_report_modal','Not Allowed','No Rights');$('#Letter_InfosModelListExport').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        public bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            System.Web.HttpContext context = System.Web.HttpContext.Current;

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
        public void Letter_UserRights()
        {
            ViewData["LetterW_ExportRight"] = CheckRights(BASE, ClientScreen.Facility_Letter, "Export");            
            ViewData["LetterW_AddRight"] = CheckRights(BASE, ClientScreen.Facility_Letter, "Add");
            ViewData["LetterW_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_Letter, "Update");
            ViewData["LetterW_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_Letter, "Delete");

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
        }
    }
}