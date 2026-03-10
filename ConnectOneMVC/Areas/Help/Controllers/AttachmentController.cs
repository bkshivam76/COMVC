using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Attachments;
using static Common_Lib.DbOperations;
using Common_Lib;
using System.Text.RegularExpressions;
using ConnectOneMVC.Helper;
using System.Net;
using System.Web.Configuration;

namespace ConnectOneMVC.Areas.Help.Controllers
{

    public class AttachmentController : BaseController
    {
        public List<Attachment_List> AttachmentGridData
        {
            get
            {
                return (List<Attachment_List>)GetBaseSession("AttachmentGridData_HelpAttachmentInfo");
            }
            set
            {
                SetBaseSession("AttachmentGridData_HelpAttachmentInfo", value);
            }
        }
        public bool? HelpSuperuser_Auditor
        {
            get
            {
                return (bool?)GetBaseSession("HelpSuperuser_Auditor_HelpAttachmentInfo");
            }
            set
            {
                SetBaseSession("HelpSuperuser_Auditor_HelpAttachmentInfo", value);
            }
        } 
        public List<Attachment_List> LinkAttachmentGridData
        {
            get
            {
                return (List<Attachment_List>)GetBaseSession("LinkAttachmentGridData_LinkAttachment");
            }
            set
            {
                SetBaseSession("LinkAttachmentGridData_LinkAttachment", value);
            }
        }
        #region "Grid"
        [CheckLogin]
        public ActionResult Frm_Attachment_Info(string RefRecID = "ALL", string PopUpId = null, string filter = "")
        {
            if (CheckRights(BASE, ClientScreen.Help_Attachments, "List"))
            {
                UserAuthorization();
                //ViewBag.ShowHorizontalBar = 0;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Attachments).ToString()) ? 1 : 0;


                ViewBag.Attachment_popupId = PopUpId;
                ViewBag.filter = filter;
                ViewBag.OpenUserID = BASE._open_User_ID;
                ViewBag.RefRecID = RefRecID;
                var attachmentlist = new List<Attachment_List>();
                // var attachmentgriddata = BASE._Attachments_DBOps.GetList(RefRecID);
                // if (attachmentgriddata == null)
                //{
                //    return View(attachmentlist);
                //}
                //attachmentlist = attachmentgriddata;

                //AttachmentGridData = attachmentlist;

                return View(attachmentlist);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopUpId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopUpId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Help_Attachment').hide();</script>");//Code written for User Authorization do not remove
                }
            }
        }
        public ActionResult Frm_Attachment_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string RefRecID = "ALL")
        {
            UserAuthorization();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (AttachmentGridData == null || command == "REFRESH")
            {
                RefRecID = string.IsNullOrWhiteSpace(RefRecID) ? "ALL" : RefRecID;
                var attachmentgriddata = BASE._Attachments_DBOps.GetList(RefRecID);
                if (attachmentgriddata != null)
                {
                    AttachmentGridData = attachmentgriddata;
                }
            }
            var attachmentgrid_data = AttachmentGridData;
            if (attachmentgrid_data == null)
            {
                return PartialView();
            }
            return PartialView(attachmentgrid_data);
        }
        public ActionResult AttachmentCustomDataAction(string key)
        {

            var FinalData = AttachmentGridData;
            var attachment = FinalData.Where(f => f.UniqueID == key).FirstOrDefault();
            string itstr = "";
            if (attachment != null)
            {
                itstr = attachment.Add_Date + "![" + attachment.Add_By + "![" + attachment.Edit_By + "![" + attachment.Edit_Date + "!["
                        + attachment.RefID + "![" + attachment.Checked + "![" + attachment.File_Name + "![" + attachment.Action_Status + "![" + attachment.Checking_Status + "![" + attachment.ID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);

        }
        #endregion
        #region"NEVD"
        public ActionResult DataNavigation(string AM, string ID, string editon)
        {
            if ((!CheckRights(BASE, ClientScreen.Help_Attachments, "Update")) && AM == "Edit")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            Return_Attachment_Record attachmentdata = BASE._Attachments_DBOps.GetRecord(ID);
            if (attachmentdata == null)
            {
                return Json(new
                {
                    result = false,
                    message = "Record Already Changed...!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (editon != attachmentdata.EditOn)
            {
                return Json(new
                {
                    result = false,
                    message = "Record Already Changed...!"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AM">Action Method</param>
        /// <param name="PostSucessFunctionName">javascript function that is called on success of popup close</param>
        /// <param name="Post_action_Name"></param>
        /// <param name="Post_Controller_Name"></param>
        /// <param name="Post_Area_Name"></param>
        /// <param name="Document_Grid_Data_SessionName"></param>
        /// <param name="Document_Grid_SrNo"></param>
        /// <param name="Document_Grid_PopupName"></param>
        /// <param name="ID"></param>
        /// <param name="ByteSessionVariable"></param>
        /// <param name="Refrecid"></param>
        /// <param name="RefScreen"></param>
        /// <param name="DocumentName"></param>
        /// <param name="DocumentCategory"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Frm_Attachment_Window(string AM, string PostSucessFunctionName = "", string Post_action_Name = "", string Post_Controller_Name = "", string Post_Area_Name = "", string Document_Grid_Data_SessionName = "", int Document_Grid_SrNo = 0, string Document_Grid_PopupName = "", string ID = null, string ByteSessionVariable = "", string Refrecid = null, string RefScreen = null, string DocumentName = "", string DocumentCategory = "", string DocumentDescription = "", int ParamMandatory = 0, string FromDate_Label = "", string ToDate_Label = "", string Description_Label = "", bool CallFromHelpAttachment = false)
        {
            UserAuthorization();
            var popupID = Document_Grid_PopupName == "" ? "Dynamic_Content_popup" : Document_Grid_PopupName;
            if (AM == "New")
            {
                if (!CheckRights(BASE, ClientScreen.Help_Attachments, "Add"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + popupID + "','Not Allowed','No Rights');</script>");
                }
            }
            else if (AM == "Edit")
            {
                if ((!CheckRights(BASE, ClientScreen.Help_Attachments, "Update")))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + popupID + "','Not Allowed','No Rights');</script>");
                }
            }
            else if (AM == "Delete")
            {
                if ((!CheckRights(BASE, ClientScreen.Help_Attachments, "Delete")))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + popupID + "','Not Allowed','No Rights');</script>");
                }
            }

            Model_Attachment_Window model = new Model_Attachment_Window();
            model.ActionMethod = AM;
            model.Help_ParamMandatory = ParamMandatory == 1 ? true : false;
            model.Help_FromDate_Label = FromDate_Label ?? "";
            model.Help_ToDate_Label = ToDate_Label ?? "";
            model.Help_Description_Label = Description_Label ?? "";
            if (PostSucessFunctionName.Length > 0) model.Help_PostSucessFunctionName = PostSucessFunctionName;
            if (Post_action_Name.Length > 0) model.Help_Post_Action_Name = Post_action_Name;
            model.Help_Post_Controller_Name = Post_Controller_Name.Length > 0 ? Post_Controller_Name : "Attachment";
            model.Help_Post_Area_Name = Post_Area_Name.Length > 0 ? Post_Area_Name : "Help";
            model.Help_Byte_SessionName = ByteSessionVariable.Length > 0 ? ByteSessionVariable : "HelpDocument";
            model.Help_Document_PopupName = Document_Grid_PopupName.Length > 0 ? Document_Grid_PopupName : "Dynamic_Content_popup";
            model.Help_REF_REC_ID = Refrecid;
            model.Help_REF_SCREEN = RefScreen;
            model.Help_Document_NameID = DocumentName;
            model.Help_Document_CategoryID = DocumentCategory;
            model.Help_Document_Description = DocumentDescription;
            model.Help_CallFromHelpAttachment = CallFromHelpAttachment;
            if ((AM == "Edit" || AM == "Delete" || AM == "View") && Document_Grid_SrNo == 0)
            {
                Return_Attachment_Record attachmentdata = BASE._Attachments_DBOps.GetRecord(ID);
                model.ID = ID;
                model.Help_Document_FileName = attachmentdata.File_Name;
                model.Help_Doc_To_Date = attachmentdata.Applicable_To;
                model.Help_Doc_From_Date = attachmentdata.Applicable_From;
                model.Help_Document_NameID = attachmentdata.NameID;
                model.Help_Document_Description = attachmentdata.Description;
                model.Help_Document_CategoryID = attachmentdata.Category;
            }
            if ((AM == "Edit" || AM == "Delete" || AM == "View") && Document_Grid_SrNo != 0)
            {
                model.ID = ID;

                var all_data = (List<Return_GetDocumentsGridData>)GetBaseSession(Document_Grid_Data_SessionName);
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr_No == Document_Grid_SrNo).FirstOrDefault() : new Return_GetDocumentsGridData();
                model.Sr_no = Document_Grid_SrNo;
                model.Help_Document_CategoryID = dataToEdit.Document_Type;
                model.Help_Document_NameID = dataToEdit.Document_Name_ID;
                model.Help_Document_FileName = dataToEdit != null ? dataToEdit.File_Name : "";
                model.Help_Document_Name = dataToEdit.Document_Name != null ? dataToEdit.Document_Name : "";
                model.Help_Document_Description = dataToEdit.Remarks != null ? dataToEdit.Remarks.ToString() : "";
                model.Help_Doc_From_Date = dataToEdit.Applicable_From != null ? dataToEdit.Applicable_From : DateTime.Now;
                model.Help_Doc_To_Date = dataToEdit.Applicable_To != null ? dataToEdit.Applicable_To : DateTime.Now;
                model.Help_filefield = dataToEdit.File_Array;
                model.ID = dataToEdit.ID;
            }        
            return View("Frm_Attachment_Window", model);
        }
        [HttpPost]
        public ActionResult Frm_Attachment_Window(Model_Attachment_Window model)
        {
            try
            {                
                if (string.IsNullOrWhiteSpace(model.Help_uploadControlActionMethod) == false) 
                {
                    model.ActionMethod = model.Help_uploadControlActionMethod;
                }            
                model.Help_FileList = new List<Byte[]>();
                List<string> DocumentFileNameList = new List<string>();
                if (string.IsNullOrWhiteSpace(model.Help_uploadControlName) == false)
                {
                    var AllFiles = Request.Files;
                    if (AllFiles.Count == 1)
                    {
                        if (model.Help_uploadMethod == "fileCollection")
                        {
                            HttpFileCollectionBase myFile = Request.Files;
                            HttpPostedFileBase File = myFile[0];
                            if (File.ContentLength > 0)
                            {
                                BinaryReader reader = new BinaryReader(File.InputStream);
                                byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                model.Help_filefield = imageBytes;
                                model.Help_FileList.Add(imageBytes);
                                DocumentFileNameList.Add(File.FileName);
                                imageBytes = null;
                                myFile = null;
                                reader.Close();
                                reader.Dispose();
                            }
                        }
                        else
                        {
                            HttpPostedFileBase myFile = Request.Files[model.Help_uploadControlName];
                            if (myFile.ContentLength > 0)
                            {
                                BinaryReader reader = new BinaryReader(myFile.InputStream);
                                byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);
                                model.Help_filefield = imageBytes;
                                model.Help_FileList.Add(imageBytes);
                                DocumentFileNameList.Add(myFile.FileName);
                                imageBytes = null;
                                myFile = null;
                                reader.Close();
                                reader.Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (model.Help_uploadMethod == "fileCollection")
                        {
                            HttpFileCollectionBase myFile = Request.Files;//.GetMultiple(model.Help_uploadControlName); //This is working for file collection.
                            for (int i = 0; i < myFile.Count; i++)
                            {
                                HttpPostedFileBase File = myFile[i];
                                if (File.ContentLength > 0)
                                {
                                    BinaryReader reader = new BinaryReader(File.InputStream);
                                    byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                    model.Help_FileList.Add(imageBytes);
                                    DocumentFileNameList.Add(File.FileName);
                                    imageBytes = null;
                                    File = null;
                                    reader.Close();
                                    reader.Dispose();
                                }
                            }
                            myFile = null;
                        }
                        else
                        {
                            IList<HttpPostedFileBase> myFile = Request.Files.GetMultiple(model.Help_uploadControlName);
                            for (int i = 0; i < myFile.Count; i++)
                            {
                                HttpPostedFileBase File = myFile[i];
                                if (File.ContentLength > 0)
                                {
                                    BinaryReader reader = new BinaryReader(File.InputStream);
                                    byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                    model.Help_FileList.Add(imageBytes);
                                    DocumentFileNameList.Add(File.FileName);
                                    imageBytes = null;
                                    File = null;
                                    reader.Close();
                                    reader.Dispose();
                                }
                            }
                            myFile = null;
                        }
                    }
                    AllFiles = null;
                }
                else
                {
                    object FileField;
                    object DocumentFileName;
                    FileField = GetBaseSession(model.Help_Byte_SessionName + "_HelpAttachment");
                    DocumentFileName = GetBaseSession(model.Help_Byte_SessionName + "_DocumentFileName" + "_HelpAttachment");
                    if (FileField != null)
                    {
                        if (FileField.GetType() == typeof(Byte[]))
                        {
                            model.Help_filefield = FileField as byte[];
                        }
                        else
                        {
                            model.Help_FileList = FileField as List<byte[]>;
                        }
                        if (model.Help_FileList == null || model.Help_FileList.Count == 0)
                        {
                            model.Help_FileList.Add(model.Help_filefield);
                        }
                    }

                    if (DocumentFileName != null)
                    {
                        if (DocumentFileName.GetType() == typeof(String))
                        {
                            model.Help_Document_FileName = (string)DocumentFileName;
                        }
                        else
                        {
                            DocumentFileNameList = DocumentFileName as List<string>;
                        }
                        if (DocumentFileNameList == null || DocumentFileNameList.Count == 0)
                        {
                            DocumentFileNameList.Add(model.Help_Document_FileName);
                        }
                    }
                }             
          
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    for (int i = 0; i < DocumentFileNameList.Count; i++)
                    {
                        DocumentFileNameList[i] = CommonFunctions.TransformFileName(DocumentFileNameList[i], model.Help_FileList.Count > 0 ? model.Help_FileList[i] : model.Help_filefield);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(DocumentFileNameList[i]))
                        {
                            return Json(new
                            {
                                result = false,
                                Message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + DocumentFileNameList[i]
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    model.Help_Document_FileName = DocumentFileNameList.Count == 0 ? model.Help_Document_FileName : DocumentFileNameList[0];
                    //if (model.Help_Document_FileName.Split('.').Length <= 1)
                    //{
                    //    return Json(new
                    //    {
                    //        result = false,
                    //        Message = "File Without Extension Not Allowed...!"
                    //    }, JsonRequestBehavior.AllowGet);
                    //}

                    if (string.IsNullOrEmpty(model.Help_Document_NameID))
                    {
                        return Json(new
                        {
                            result = false,
                            Message = "Document Name Is Not Selected..!!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Help_ParamMandatory == true)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Help_FromDate_Label))
                        {
                            if (IsDate(model.Help_Doc_From_Date) == false)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_FromDate_Label + " Is Blank Or Invalid..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.Help_ToDate_Label))
                        {
                            if (IsDate(model.Help_Doc_To_Date) == false)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_ToDate_Label + " Is Blank Or Invalid..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.Help_Description_Label))
                        {
                            if (string.IsNullOrWhiteSpace(model.Help_Document_Description))
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_Description_Label + " Is Not Specefied..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(model.Help_Document_Description))
                    {
                        model.Help_Document_Description = model.Help_Document_Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                    }
                }


                if (model.Help_Post_Area_Name == "Help" && model.Help_Post_Controller_Name == "Attachment")
                {
                    if (model.ActionMethod == "New")
                    {
                        var InEInfo = new Parameter_Insert_Attachment();

                        InEInfo.Description = model.Help_Document_Description == null ? "" : model.Help_Document_Description;
                        InEInfo.NameID = model.Help_Document_NameID;
                        InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                        InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                        InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                        InEInfo.Ref_Screen = model.Help_REF_SCREEN;
                        InEInfo.Checked = model.Help_Checked;
                        InEInfo.Vouching_Category = CommonFunctions.GetVouchingCategoryForCallingScreen(model.Help_REF_SCREEN ?? "");
                        var AttachmentID = "";
                        string msg = "";
                        string FileList = "";
                        string AttachmentIdsList = "";               
                        for (int i = 0; i < model.Help_FileList.Count; i++)
                        {
                            string ErrMsg = PreventFileUpload(DocumentFileNameList[i]);
                            if (string.IsNullOrWhiteSpace(ErrMsg) == false)
                            {
                                msg = msg + ErrMsg;
                            }
                            else
                            {
                                InEInfo.RecID = System.Guid.NewGuid().ToString();
                                AttachmentID = InEInfo.RecID;
                                InEInfo.File = model.Help_FileList[i];
                                InEInfo.FileName = DocumentFileNameList[i];
                                //if (model.Help_uploadMethod == "fileCollection")
                                if (model.Help_File_caption != null && model.Help_File_caption.Count > 0)
                                {
                                    InEInfo.Description = model.Help_File_caption[i];
                                    if (string.IsNullOrWhiteSpace(InEInfo.Description) == false)
                                    {
                                        InEInfo.Description = InEInfo.Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                                    }
                                }
                                var AttachmentSrNo = BASE._Attachments_DBOps.Insert(InEInfo);
                                if (AttachmentSrNo.Length > 0)
                                {
                                    FileList += DocumentFileNameList[i] + ", ";
                                    AttachmentIdsList += InEInfo.RecID + ", ";
                                    msg = msg + DocumentFileNameList[i] + " Uploaded...<b>(" + AttachmentSrNo + ")</b><br>";
                                }
                                else
                                {
                                    msg = msg + DocumentFileNameList[i] + " <b>Upload Failed...</b><br>";
                                }
                            }
                        }
                        if (FileList.Length > 0)
                        {
                            FileList = FileList.Remove(FileList.Length - 2);
                            AttachmentIdsList = AttachmentIdsList.Remove(AttachmentIdsList.Length - 2);
                        }
                        if (msg.Length > 0)
                        {
                            return Json(new
                            {
                                Message = msg,
                                result = true,
                                AttachmentID,
                                FileList,
                                AttachmentIdsList
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Messages.SomeError,
                                result = false,
                                AttachmentID,
                                FileList,
                                AttachmentIdsList
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ActionMethod == "Edit")
                    {
                        var InEInfo = new Parameter_Update_Attachment();
                        InEInfo.FileName = model.Help_Document_FileName;
                        InEInfo.Description = model.Help_Document_Description == null ? "" : model.Help_Document_Description;
                        InEInfo.CategoryID = model.Help_Document_NameID;
                        InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                        InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                        InEInfo.File = model.Help_filefield;
                        InEInfo.RecID = model.ID;
                        InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                        InEInfo.Ref_Screen = model.Help_REF_SCREEN;
                        InEInfo.Checked = model.Help_Checked;
                        InEInfo.Vouching_Category = CommonFunctions.GetVouchingCategoryForCallingScreen(model.Help_REF_SCREEN ?? "");
                        var AttachmentID = InEInfo.RecID;
                        if (BASE._Attachments_DBOps.Update(InEInfo))
                        {
                            return Json(new
                            {
                                Message = "Attachment Updated Successfully...",
                                result = true,
                                AttachmentID
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SomeError,
                                result = false,
                                AttachmentID
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ActionMethod == "Delete")
                    {
                        string Actual_File_Name = model.Help_Document_FileName;
                        string Delete_File_Name = "";
                        var FileParts = Actual_File_Name.Split('.');
                        if (FileParts.Length > 1)
                        {
                            Delete_File_Name = model.ID + "." + FileParts[FileParts.Length - 1];
                        }
                        else
                        {
                            Delete_File_Name = model.ID;
                        }
                        if (BASE._Attachments_DBOps.Unlink_aLL(model.ID))
                        {
                            if (BASE._Attachments_DBOps.Delete_attachment(Delete_File_Name))
                            {
                                return Json(new
                                {
                                    Message = "Attachment Deleted Successfully...",
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
                        else
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SomeError,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (model.Help_Post_Controller_Name.Length > 0)
                {
                    SetBaseSession(model.Help_Post_Action_Name + "_AttachmentData", model);
                    TempData["ModelData"] = model;
                    //return RedirectToAction("Production_Documents_Attachment", "ProductionRegister",new {Area="Stock" });
                    return RedirectToAction(model.Help_Post_Action_Name, model.Help_Post_Controller_Name, new { Area = model.Help_Post_Area_Name, SessionGUID = Request.QueryString["SessionGUID"] });

                }

                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Upload(string sessionvariable, string BrowseBtnID = "help_myFile", string ActionMethod = "Edit", bool CallFromHelpAttachment = false, bool AllowMultiple = false)
        {
            var myFile = Request.Files[BrowseBtnID];
            BinaryReader reader = new BinaryReader(myFile.InputStream);
            byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);

            if ((sessionvariable == "HelpDocument" && BrowseBtnID == "help_myFile" && ActionMethod == "New" && CallFromHelpAttachment == true) || AllowMultiple == true)
            {
                List<byte[]> ImageByteList = new List<byte[]>();
                var ImageByteListInSession = (List<byte[]>)GetBaseSession(sessionvariable + "_HelpAttachment");
                if (ImageByteListInSession != null)
                {
                    ImageByteList = ImageByteListInSession;
                }
                ImageByteList.Add(imageBytes);
                SetBaseSession(sessionvariable + "_HelpAttachment", ImageByteList);
                List<string> DocumentFileNameList = new List<string>();
                var DocumentFileNameInSession = (List<string>)GetBaseSession(sessionvariable + "_DocumentFileName" + "_HelpAttachment");
                if (DocumentFileNameInSession != null)
                {
                    DocumentFileNameList = DocumentFileNameInSession;
                }
                DocumentFileNameList.Add(myFile.FileName);
                SetBaseSession(sessionvariable + "_DocumentFileName" + "_HelpAttachment", DocumentFileNameList);

            }
            else
            {
                SetBaseSession(sessionvariable + "_HelpAttachment", imageBytes);
                SetBaseSession(sessionvariable + "_DocumentFileName" + "_HelpAttachment", myFile.FileName);
            }
           // Session[sessionvariable] = imageBytes;


            return new EmptyResult();
        }

        //public string TransformFileName(string FileName, byte[] FileByteArray)
        //{
        //    string filetype = "";
        //    string mimeType = "";
        //    string FileNameWithoutExtension = "";
        //    var FileNameSplit = FileName.Split('.');
        //    mimeType = MimeMapping.GetMimeMapping(FileName);
        //    if (FileNameSplit.Length < 2)
        //    {
        //        filetype = GetFileType(FileByteArray);
        //    }
        //    else if (FileNameSplit[FileNameSplit.Length - 1].ToLower() == "csv")
        //    {
        //        filetype = "csv";
        //    }
        //    else if (mimeType == "application/octet-stream")
        //    {
        //        filetype = GetFileType(FileByteArray);
        //    }
        //    else
        //    {
        //        filetype = FileNameSplit[FileNameSplit.Length - 1];
        //    }
        //    if (FileNameSplit.Length == 1)
        //    {
        //        FileNameWithoutExtension = FileNameSplit[0];
        //    }
        //    else
        //    {
        //        for (int i = 0; i < FileNameSplit.Length - 1; i++)
        //        {
        //            if (i == 0)
        //            {
        //                FileNameWithoutExtension = FileNameSplit[0];
        //            }
        //            else
        //            {
        //                FileNameWithoutExtension = FileNameWithoutExtension + "." + FileNameSplit[i];
        //            }
        //        }
        //    }
        //    FileNameWithoutExtension = Regex.Replace(FileNameWithoutExtension, "[^0-9A-Za-z]+", "_");
        //    var FinalFileName = "";
        //    if (filetype.Length > 0)
        //    {
        //        FinalFileName = FileNameWithoutExtension + "." + filetype;
        //    }
        //    else
        //    {
        //        FinalFileName = FileNameWithoutExtension;
        //    }
        //    return FinalFileName;
        //}
        //public string GetFileType(byte[] FileField)
        //{
        //    string filetype = "";
        //    byte[] first16Bytes = new byte[16];
        //    Array.Copy(FileField, 0, first16Bytes, 0, 16);
        //    string data_as_hex = BitConverter.ToString(first16Bytes);
        //    string MagicNumber = data_as_hex.Substring(0, 11);
        //    if (MagicNumber.StartsWith("42-4D"))
        //    {
        //        filetype = "bmp";
        //        return filetype;
        //    }
        //    else if (MagicNumber.StartsWith("FF-FB") || MagicNumber.StartsWith("49-44-33"))
        //    {
        //        filetype = "mp3";
        //        return filetype;
        //    }
        //    switch (MagicNumber)
        //    {
        //        case "25-50-44-46":
        //            filetype = "pdf";
        //            break;
        //        case "FF-D8-FF-DB":
        //        case "FF-D8-FF-EE":
        //        case "FF-D8-FF-E0":
        //        case "FF-D8-FF-E1":
        //            filetype = "jpg";
        //            break;
        //        case "89-50-4E-47":
        //            filetype = "png";
        //            break;
        //        case "47-49-46-38":
        //            filetype = "gif";
        //            break;
        //        case "7B-5C-72-74":
        //            filetype = "rtf";
        //            break;
        //        case "50-4B-03-04":
        //            filetype = "docx";
        //            break;
        //        case "D0-CF-11-E0":
        //            filetype = "doc";
        //            break;
        //        case "49-49-2A-00":
        //        case "4D-4D-00-2A":
        //            filetype = "tiff";
        //            break;
        //        case "38-42-50-53":
        //            filetype = "psd";
        //            break;
        //        case "52-49-46-46":
        //            filetype = "webp";
        //            break;

        //    }
        //    if (filetype.Length == 0)
        //    {
        //        string MagicNumber_4byteOffset = data_as_hex.Substring(12, 11);
        //        switch (MagicNumber_4byteOffset)
        //        {
        //            case "66-74-79-70":
        //                filetype = "mp4";
        //                break;
        //        }
        //    }
        //    return filetype;
        //}
        public ActionResult CheckViewRight()
        {
            if (CheckRights(BASE, ClientScreen.Help_Attachments, "View"))
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else { return Json(new { result = false }, JsonRequestBehavior.AllowGet); }
        }
        [CheckLogin]
        public ActionResult Frm_Attachment_ViewFile(string filename, string SessionVariable, string ID = null)
        {
            if (!CheckRights(BASE, ClientScreen.Help_Attachments, "View"))
            {
                return Json(new { result = "NoViewRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            byte[] FileBytes = null;
            string mimeType;
            string FileNameToDownload;
            string FileType = "";
            object FileField = GetBaseSession(SessionVariable + "_HelpAttachment");
            if (FileField != null)
            {
                if (FileField.GetType() == typeof(Byte[]))
                {
                    FileBytes = FileField as byte[];
                }
                else
                {
                    FileBytes = ((List<byte[]>)FileField)[0];
                }
            }
            if (ID != null)
            {
                if (filename == null)
                {
                    filename = BASE._Attachments_DBOps.GetRecord(ID).File_Name;
                }

                var FileParts = filename.Split('.');
                if (FileParts.Length > 1)
                {
                    FileType = FileParts[FileParts.Length - 1];
                    FileNameToDownload = ID + "." + FileType;
                }
                else
                {
                    FileNameToDownload = ID;
                }
            }
            else
            {
                FileNameToDownload = filename;
            }
            if (ID == null)
            {
                mimeType = MimeMapping.GetMimeMapping(filename);

                //FileBytes = (byte[])GetBaseSession(SessionVariable + "_HelpAttachment");
            }
            else
            {
                if (GetBaseSession(SessionVariable + "_HelpAttachment") != null)
                {
                    mimeType = MimeMapping.GetMimeMapping(filename);
                    //FileBytes = GetBaseSession(SessionVariable + "_HelpAttachment") as byte[];
                }
                else
                {
                    mimeType = MimeMapping.GetMimeMapping(FileNameToDownload);
                    FileBytes = BASE._Attachments_DBOps.Download_File(FileNameToDownload);
                }
            }
            return File(FileBytes, mimeType, FileNameToDownload);
        }
        public ActionResult Frm_Attachment_Unlink(string ID, string RefId)
        {
            try
            {
                if (!CheckRights(BASE, ClientScreen.Help_Attachments, "Delete"))
                {
                    return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Attachments_DBOps.Unlink_attachment(RefId, ID))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Unlinked Successfully..!!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        Message = Common_Lib.Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Attachment_Check(string ID)
        {
            try
            {
                if (HelpSuperuser_Auditor == false)
                {
                    return Json(new { result = "NoSuperuser_Auditor_Check_UncheckRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Attachments_DBOps.Mark_Accepted_attachment(ID))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Accepted..!!"
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
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Attachment_RejectReason(string ID, string MainGridName = "", string NestedGridName = "")
        {
            ViewBag.Help_Rec_ID = ID;
            ViewBag.MainGridName = MainGridName;
            ViewBag.NestedGridName = NestedGridName;
            return View();
        }
        public ActionResult Frm_Attachment_UnCheck(string ID, string Reason)
        {
            try
            {
                if (HelpSuperuser_Auditor == false)
                {
                    return Json(new { result = "NoSuperuser_Auditor_Check_UncheckRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Attachments_DBOps.Mark_Rejected_attachment(ID, Reason))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Marked As Rejected..!!"
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
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Attachment_Lock(string ID)
        {
            try
            {
                if (HelpSuperuser_Auditor == false)
                {
                    return Json(new { result = false, Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Attachments_DBOps.MarkAsLocked(ID))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Locked..!!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Attachment_UnLock(string ID)
        {
            try
            {
                if (HelpSuperuser_Auditor == false)
                {
                    return Json(new { result = false, Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Attachments_DBOps.MarkAsUnlocked(ID))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Unlocked..!!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AttachmentLinkCheck(string AttachmentID, string RefScreen, string RefRecID)
        {
            Parameter_GetAttachmentLinkCount inparam = new Parameter_GetAttachmentLinkCount();
            inparam.AttachmentID = AttachmentID;
            inparam.RefScreen = RefScreen;
            inparam.RefRecordID = RefRecID;
            var screen = BASE._Attachments_DBOps.GetAttachmentLinkScreen(inparam);
            if (!string.IsNullOrEmpty(screen))
            {
                if (screen != RefScreen)
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From " + RefScreen + "..?"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To Some Other Entry In " + screen + ".Do You Want To Unlink It From This Entry..?"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_AttachmentReason(string ActionMethod, string Reason, string RefRecID, string screen, string MainGridName, string NestedGridName, int? MappingID = null)
        {
            ViewBag.ActionMethod = ActionMethod;
            ViewBag.Reason = Reason;
            ViewBag.RefRecID = RefRecID;
            ViewBag.MappingID = MappingID;
            ViewBag.screen = screen;
            ViewBag.MainGridName = MainGridName;
            ViewBag.NestedGridName = NestedGridName;
            return View();
        }
        public ActionResult Frm_AttachmentReason_Save(string ActionMethod, string Reason, string RefRecID, string screen, int? MappingID = null)
        {
            try
            {
                if (ActionMethod == "New" || ActionMethod == "Edit")
                {
                    if (string.IsNullOrWhiteSpace(Reason))
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Reason Not Specified.."
                        }, JsonRequestBehavior.AllowGet);
                    }
                    Reason = Reason.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                }

                if (ActionMethod == "New")
                {
                    ClientScreen _Screen = (ClientScreen)Enum.Parse(typeof(ClientScreen), screen);
                    if (BASE._Audit_DBOps.DocumentAbsentReasonAdded(RefRecID, Reason, (int)MappingID, _Screen))
                    {
                        return Json(new
                        {
                            result = true
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
                else if (ActionMethod == "Edit")
                {
                    ClientScreen _Screen = (ClientScreen)Enum.Parse(typeof(ClientScreen), screen);
                    if (BASE._Audit_DBOps.DocumentAbsentReasonUpdated(RefRecID, Reason, (int)MappingID, _Screen))
                    {
                        return Json(new
                        {
                            result = true
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
                else if (ActionMethod == "Delete")
                {
                    if (BASE._Audit_DBOps.DocumentAbsentReasonDelete(RefRecID, (int)MappingID))
                    {
                        return Json(new
                        {
                            result = true
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
                return Json(new
                {
                    result = true
                }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Frm_Attachment_DeleteByDescription()
        {
            if (BASE._open_User_ID.ToLower() != "bksaurabh")
            {
                return Json(new
                {
                    result = false,
                    message = "Not Allowed..."
                }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
        public ActionResult DeleteByDescription(string Description = "")
        {
            try
            {
                if (BASE._Attachments_DBOps.Delete_By_Description(Description))
                {
                    return Json(new
                    {
                        result = true,
                        message = "Deleted Successfully..."
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
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Help_Attachments, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Attachment_report_modal','Not Allowed','No Rights');$('#Help_BUT_PRINT').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        #region"DropDown"
        public ActionResult LookUp_GetDocument_Category(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
               // RefreshDocumentCategory();
                //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_GetDocument_Categories>(), loadOptions)), "application/json");
          

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BASE._Attachments_DBOps.GetDocument_Categories(), loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetDocument_Name(bool? IsVisible, DataSourceLoadOptions loadOptions, string CategoryName, bool DDRefresh = false)
        {
                //RefreshDocumentName(CategoryName);
                //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_GetDocument_Names>(), loadOptions)), "application/json");
           
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BASE._Attachments_DBOps.GetDocument_Names(CategoryName), loadOptions)), "application/json");
        }
        #endregion
        #region "Misc"
        public void UserAuthorization()
        {
            HelpSuperuser_Auditor = false;
            ViewData["Help_Attachments_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachments_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
            ViewData["Help_Attachments_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachments_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
            ViewData["Help_Attachments_ExportRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Export");
            if (BASE._open_User_Type != "CLIENT ROLE")
            {
                HelpSuperuser_Auditor = true;
            }
            ViewData["HelpSuperuser_Auditor"] = HelpSuperuser_Auditor;
        }
        public void Sessionclear(string ByteSessionVariable = "HelpDocument")
        {
            Session.Remove(ByteSessionVariable);
            BASE._SessionDictionary.Remove(ByteSessionVariable + "_HelpAttachment");
            BASE._SessionDictionary.Remove("DocumentName_HelpAttachment");
            BASE._SessionDictionary.Remove("DocumentCategory_HelpAttachment");
            BASE._SessionDictionary.Remove(ByteSessionVariable + "_DocumentFileName" + "_HelpAttachment");
        }
        public void Sessionclear_AddMore(string ByteSessionVariable = "HelpDocument")
        {
            Session.Remove(ByteSessionVariable);
            BASE._SessionDictionary.Remove(ByteSessionVariable + "_HelpAttachment");
            BASE._SessionDictionary.Remove(ByteSessionVariable + "_DocumentFileName" + "_HelpAttachment");
        }
        public void SessionClearInfo()
        {
            ClearBaseSession("_HelpAttachmentInfo");
        }
        #endregion
        public ActionResult Frm_Select_Attachment(string Referece_ID, string Referece_Screen, string GridName, string NestedGridName)
        {
            BASE.Get_Configure_Setting();
            ViewBag.Referece_ID = Referece_ID;
            ViewBag.Referece_Screen = Referece_Screen;
            ViewBag.GridName = GridName;
            ViewBag.NestedGridName = NestedGridName;

            var _db_Table = BASE._Attachments_DBOps.GetList("ALL");
            if (_db_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            if (_db_Table.Count <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "No Selectable Attachment Exists!"
                }, JsonRequestBehavior.AllowGet);
            }
            LinkAttachmentGridData = _db_Table;
            return View(_db_Table);
        }
        public ActionResult Frm_Select_Attachment_Grid(string command)
        {
            if (command == "REFRESH" || LinkAttachmentGridData == null)
            {
                LinkAttachmentGridData = BASE._Attachments_DBOps.GetList();
            }
            return View(LinkAttachmentGridData);
        }
        [HttpGet]
        public ActionResult LinkAttachment(string Attachment_ID = "", string Referece_ID = "", string Referece_Screen = "", string AttachmentSrNo = "", string AttachmentFileName = "")
        {
            try
            {
                string msg = "";
                int count = 0;
                if (AttachmentSrNo.Length > 0 || AttachmentFileName.Length > 0)
                {
                    Attachment_ID = "";
                    var Document_split = AttachmentSrNo.Length > 0 ? AttachmentSrNo.Split(',') : AttachmentFileName.Split(',');
                    for (int i = 0; i < Document_split.Count(); i++)
                    {
                        if (!string.IsNullOrWhiteSpace(Document_split[i].Trim()))
                        {
                            string ID = "";
                            if (AttachmentSrNo.Length > 0)
                            {
                                ID = BASE._Attachments_DBOps.GetAttachmentIDBySrno(Document_split[i].Trim());
                            }
                            else
                            {
                                ID = BASE._Attachments_DBOps.GetAttachmentIDByFileName(Document_split[i].Trim());
                            }
                            if (string.IsNullOrWhiteSpace(ID))
                            {
                                if (AttachmentSrNo.Length > 0)
                                {
                                    msg = msg + "Attachment With Serial No.(<b>" + Document_split[i].Trim() + "</b>) Not Found<br>";
                                }
                                else
                                {
                                    msg = msg + "Attachment With File Name(<b>" + Document_split[i].Trim() + "</b>) Not Found<br>";
                                }
                            }
                            else
                            {
                                Attachment_ID = Attachment_ID + "|" + ID;
                            }
                        }
                    }
                    if (Attachment_ID.StartsWith("|"))
                    {
                        Attachment_ID = Attachment_ID.Substring(1, Attachment_ID.Length - 1);
                    }
                }
                if (!string.IsNullOrWhiteSpace(Attachment_ID))
                {
                    var Existing_Links = BASE._Attachments_DBOps.GetList(Referece_ID);
                    var AllAttachmentIds = Attachment_ID.Split('|');
                    for (int i = 0; i < AllAttachmentIds.Count(); i++)
                    {
                        List<Attachment_List> AlreadyExistingLink = Existing_Links.Where(x => x.ID == AllAttachmentIds[i]).ToList();
                        if (AlreadyExistingLink.Count > 0)
                        {
                            msg = msg + "Attachment With ID( " + AllAttachmentIds[i] + " ) Is Already Linked With The Entry<br>";
                        }
                        else
                        {
                            Parameter_Insert_Attachment_Link InParam = new Parameter_Insert_Attachment_Link();
                            InParam.AttachmentID = AllAttachmentIds[i];
                            InParam.Ref_Rec_ID = Referece_ID;
                            InParam.Ref_Screen = Referece_Screen;
                            if (BASE._Attachments_DBOps.InsertLink(InParam))
                            {
                                count++;
                            }
                            else
                            {
                                msg = msg + Messages.SomeError + " Attachment ID ( " + AllAttachmentIds[i] + " )<br>";
                            }
                        }
                    }
                }
                if (msg.Length == 0)
                {
                    return Json(new
                    {
                        message = "All Attachment(" + count + ") Linked Successfully",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (count > 0)
                {
                    return Json(new
                    {
                        message = msg + "Rest Attachments(" + count + ") Linked Successfully",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = msg,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void sessionClear_LinkAttachment()
        {
            ClearBaseSession("_LinkAttachment");
        }
        public ActionResult Frm_Attachment_LinkSrNo(string Referece_ID = "", string Referece_Screen = "", string MainGridName = "", string NestedGridName = "", int FocusedRowIndex = -1, bool LinkBySerialNo = true)
        {
            ViewBag.Referece_ID = Referece_ID;
            ViewBag.Referece_Screen = Referece_Screen;
            ViewBag.MainGridName = MainGridName;
            ViewBag.NestedGridName = NestedGridName;
            ViewBag.FocusedRowIndex = FocusedRowIndex;
            ViewBag.LinkBySerialNo = LinkBySerialNo;
            return View();
        }
        private bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public ActionResult FTPUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FTPUpload(string name = "")
        {
            HttpPostedFileBase myFile = Request.Files["FTP_myFile"];
            // FTP Server URL
            // string ftp = "ftp://192.168.61.142:21/";
            string ftp = WebConfigurationManager.AppSettings["FTPServer"];
            // FTP Folder name. Leave blank if you want to upload to root folder
            // (really blank, not "/" !)
            string ftpFolder = "";
            byte[] fileBytes = null;
           // string ftpUserName = "ftpuser";
           // string ftpPassword = "shivbaba";
            string ftpUserName = WebConfigurationManager.AppSettings["FTPUser"];
            string ftpPassword = WebConfigurationManager.AppSettings["FTPPassword"];

            // read the File and convert it to Byte array.
            string fileName = Path.GetFileName(myFile.FileName);
            using (BinaryReader br = new BinaryReader(myFile.InputStream))
            {
                fileBytes = br.ReadBytes(myFile.ContentLength);
            }

            try
            {
                // create FTP Request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // enter FTP Server credentials
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                request.ContentLength = fileBytes.Length;
                request.UsePassive = true;
                request.UseBinary = true;   // or FALSE for ASCII files
                request.ServicePoint.ConnectionLimit = fileBytes.Length;
                request.EnableSsl = false;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileBytes, 0, fileBytes.Length);
                    requestStream.Close();
                    requestStream.Dispose();
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                response.Dispose();
                myFile = null;
                fileBytes = null;
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
            return View();

        }
        #region Devextreme
        public ActionResult Frm_Attachment_Info_dx(string RefRecID = "ALL", string PopUpId = null, string filter = "")
        {
            if (CheckRights(BASE, ClientScreen.Help_Attachments, "List"))
            {
                UserAuthorization();
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Attachments).ToString()) ? 1 : 0;
                ViewBag.Attachment_popupId = PopUpId;
                ViewBag.filter = filter;
                ViewBag.OpenUserID = BASE._open_User_ID;
                ViewBag.RefRecID = RefRecID;
                var attachmentlist = new List<Attachment_List>();
                return View();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopUpId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopUpId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Help_Attachment').hide();</script>");//Code written for User Authorization do not remove
                }
            }
        }
        public ActionResult Frm_Attachment_Info_Grid_dx(string RefRecID = "", bool showclick = false)
        {
            RefRecID = string.IsNullOrWhiteSpace(RefRecID) ? "ALL" : RefRecID;
            if (showclick)
                AttachmentGridData = BASE._Attachments_DBOps.GetList(RefRecID);
            else
                AttachmentGridData = new List<Attachment_List>();

            return Content(JsonConvert.SerializeObject(AttachmentGridData), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Help_Attachments, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Attachment_report_modal','Not Allowed','No Rights');$('#Help_BUT_PRINT').hide();</script>");
            }
            return PartialView();
        }

        #endregion
    }

}