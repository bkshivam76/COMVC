using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    [CheckLogin]
    public class DocumentCheckingController : BaseController
    {
        public List<DbOperations.Audit.Return_GetDocumentChecking> DocumentGridData
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentChecking>)GetBaseSession("DocumentGridData_DocumentChecking");
            }
            set
            {
                SetBaseSession("DocumentGridData_DocumentChecking", value);
            }
        }

        public ActionResult DocumentCheckingInfo()
        {
            CheckRights();           
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View();
        }
        public ActionResult DocumentChecking_Grid(string command,string Category=null,string Name=null,string Status=null,string AuditStatus = null,string DataScope=null,string CheckedBy=null, int ShowHorizontalBar = 0,string Layout=null)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout;
            Audited_Status_Options _AuditStatus = (Audited_Status_Options)Enum.Parse(typeof(Audited_Status_Options), AuditStatus??"All");

            Data_Scope_Options _option = (Data_Scope_Options)Enum.Parse(typeof(Data_Scope_Options), DataScope??"Exclusive");
            
            Category = Category == "" ? null : Category;
            Name = Name == "" ? null : Name;
            CheckedBy = CheckedBy ?? "";
            if (DocumentGridData == null || command == "REFRESH")
            {
                DocumentGridData = BASE._Audit_DBOps.GetDocumentChecking(_AuditStatus, _option,Status, Name,Category, CheckedBy);
            }
            return View(DocumentGridData);
        }
        public ActionResult AcceptDocument(string AttachID)
        {
            try
            {
                if (BASE._Attachments_DBOps.Mark_Accepted_attachment(AttachID))
                {
                    return Json(new
                    {
                        result = true,
                        message = "Document Has Been Accepted.."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
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
        }
        public ActionResult UncheckDocument(string AttachID)
        {
            try
            {
                if (BASE._Attachments_DBOps.Mark_Unchecked_attachment(AttachID))
                {
                    return Json(new
                    {
                        result = true,
                        message = "Document Has Been Unchecked.."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
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
        }
        public ActionResult RejectDocument(string AttachID, string Reason)
        {
            try
            {
                Reason = Reason.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                if (BASE._Attachments_DBOps.Mark_Rejected_attachment(AttachID, Reason))
                {
                    return Json(new
                    {
                        result = true,
                        message = "Document Has Been Rejected.."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
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
        }
        public ActionResult Frm_DC_RejectReason()
        {
            return View();
        }    
        public ActionResult LookUp_GetDocument_Category( DataSourceLoadOptions loadOptions)
        {
            var documentCategories = BASE._Attachments_DBOps.GetDocument_Categories();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(documentCategories, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetDocument_Name(DataSourceLoadOptions loadOptions, string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                CategoryName = "0";
            }
            var DocumentNames = BASE._Attachments_DBOps.GetDocument_Names(CategoryName);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DocumentNames, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetCheckedBy(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._ClientUserDBOps.GetAuditors_Superusers(ClientScreen.Accounts_CashBook);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        public void SessionClear()
        {
            ClearBaseSession("_DocumentChecking");
        }
        public ActionResult GetFileMimeType(string FileName)
        {
            string mimeType = MimeMapping.GetMimeMapping(FileName);
            return Json(new
            {
                mimeType
            }, JsonRequestBehavior.AllowGet);
        }
        public void CheckRights()
        {
            ViewData["Help_Attachment_UpdateRight"]= CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachment_DeleteRight"]= CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
        }
    }
}
