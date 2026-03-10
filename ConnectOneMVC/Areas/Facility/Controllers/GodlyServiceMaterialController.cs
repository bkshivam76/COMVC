using System;
using System.Collections.Generic;
using ConnectOneMVC.Controllers;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Helper;
using System.Data;
using ConnectOneMVC.Areas.Facility.Models;
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using System.Web.UI.WebControls;
using System.Collections;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using static Common_Lib.DbOperations.Attachments;
using ConnectOneMVC.Areas.Help.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class GodlyServiceMaterialController : BaseController
    {
        // GET: Facility/GodlyServiceMaterial
        #region Global Variables
        public List<ServiceMaterial> ServiceMaterial_ExportData
        {
            get { return (List<ServiceMaterial>)GetBaseSession("ServiceMaterial_ExportData_SerMat"); }
            set { SetBaseSession("ServiceMaterial_ExportData_SerMat", value); }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> ServiceMaterialInfo_DetailGrid_Data
        {
            get { return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("ServiceMaterialInfo_DetailGrid_Data_SerMat"); }
            set { SetBaseSession("ServiceMaterialInfo_DetailGrid_Data_SerMat", value); }
        }

        public DateTime? dtnull = null;
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Service_Material_Info() 
        {
            ServiceMaterial_UserRights();
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            if (CheckRights(BASE,Common_Lib.RealTimeService.ClientScreen.Facility_GodlyServiceMaterial,"List")) 
            {
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_GodlyServiceMaterial).ToString()) ? 1 : 0;

                Grid_Display();
                ViewData["CENID"] =  BASE._open_Cen_ID.ToString();
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ServiceMaterial_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                    || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

                return View(ServiceMaterial_ExportData);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
            }            
        }
        public void Grid_Display() 
        {
            DataTable SM_Table = BASE._ServiceMaterial_dbops.GetList();

            var BuildData = from SM in SM_Table.AsEnumerable()                            
                            select new ServiceMaterial
                            {
                                TITLE = SM.Field<string>("TITLE"),
                                MATERIAL_TYPE = SM.Field<string>("MATERIAL_TYPE"),
                                Project_ID = SM.Field<string>("Project_ID"),
                                PUBLISH_DATE = SM.Field<string>("PUBLISH_DATE"),
                                CEN_ID = SM.Field<Int32>("CEN_ID"),
                                ONLINE_LINK = SM.Field<string>("ONLINE_LINK"),
                                BRIEF_SUMMARY = SM.Field<string>("BRIEF_SUMMARY"),
                                SPEAKER_AUTHOR_PUBLISHER = SM.Field<string>("SPEAKER_AUTHOR_PUBLISHER"),
                                ID = SM.Field<string>("ID"),
                                Add_By = SM.Field<string>("Add By"),
                                Add_Date = SM.Field<DateTime>("Add Date"),
                                Edit_By = SM.Field<string>("Edit By"),
                                Edit_Date = SM.Field<DateTime>("Edit Date"),
                                Action_Status = SM.Field<string>("Action Status"),
                                Action_By = SM.Field<string>("Action By"),
                                Action_Date = SM.Field<DateTime>("Action Date"),
                                PreviewImagePath= SM.Field<string>("GSM_PREVIEW_IMAGE_PATH"),
                                MaterialSubCategory = SM.Field<string>("GSM_MATERIAL_SUB_CATEGORY"),
                                Project_Occassion = SM.Field<string>("MISC_NAME"),
                                REQ_ATTACH_COUNT = Convert.IsDBNull(SM["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(SM["REQ_ATTACH_COUNT"]),
                                COMPLETE_ATTACH_COUNT = Convert.IsDBNull(SM["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(SM["COMPLETE_ATTACH_COUNT"]),
                                RESPONDED_COUNT = Convert.IsDBNull(SM["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(SM["RESPONDED_COUNT"]),
                                REJECTED_COUNT = Convert.IsDBNull(SM["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(SM["REJECTED_COUNT"]),
                                OTHER_ATTACH_CNT = Convert.IsDBNull(SM["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(SM["OTHER_ATTACH_CNT"]),
                                ALL_ATTACH_CNT = Convert.IsDBNull(SM["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(SM["ALL_ATTACH_CNT"]),

                                VOUCHING_PENDING_COUNT = SM.Field<Int32?>("VOUCHING_PENDING_COUNT"),
                                VOUCHING_ACCEPTED_COUNT = SM.Field<Int32?>("VOUCHING_ACCEPTED_COUNT"),
                                VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = SM.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"),
                                VOUCHING_REJECTED_COUNT = SM.Field<Int32?>("VOUCHING_REJECTED_COUNT"),
                                VOUCHING_TOTAL_COUNT = SM.Field<Int32?>("VOUCHING_TOTAL_COUNT"),
                                AUDIT_PENDING_COUNT = SM.Field<Int32?>("AUDIT_PENDING_COUNT"),
                                AUDIT_ACCEPTED_COUNT = SM.Field<Int32?>("AUDIT_ACCEPTED_COUNT"),
                                AUDIT_ACCEPTED_WITH_REMARKS_COUNT = SM.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"),
                                AUDIT_REJECTED_COUNT = SM.Field<Int32?>("AUDIT_REJECTED_COUNT"),
                                AUDIT_TOTAL_COUNT = SM.Field<Int32?>("AUDIT_TOTAL_COUNT"),
                                IS_AUTOVOUCHING = SM.Field<Int32?>("IS_AUTOVOUCHING"),
                                IS_CORRECTED_ENTRY = SM.Field<Int32?>("IS_CORRECTED_ENTRY")
                            };
            var Final_Data = BuildData.ToList();
            var count = Final_Data.Count;
            for (int i = 0; i < count; i++)
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
            ServiceMaterial_ExportData = Final_Data;
        }
        public PartialViewResult Frm_Service_Material_InfoGrid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "") 
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ServiceMaterial_UserRights();
            if (command == "REFRESH" || ServiceMaterial_ExportData == null)
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

            return PartialView(ServiceMaterial_ExportData);
        }

        public ActionResult Frm_Service_Material_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.ServiceMaterialInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ServiceMaterialInfo_RecID = RecID;
            ViewBag.ServiceMaterialInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_GodlyServiceMaterial);
                    ServiceMaterialInfo_DetailGrid_Data = _docList;
                    Session["ServiceMaterialInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Facility_GodlyServiceMaterial);
                    ServiceMaterialInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["ServiceMaterialInfo_detailGrid_Data"] = data.DocumentMapping;
                    //ServiceMaterialInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(ServiceMaterialInfo_DetailGrid_Data);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ServiceMaterialListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            //settings.Columns.Add("Item_Name");
            //settings.Columns.Add("Document_Name");
            //settings.Columns.Add("Reason");
            //settings.Columns.Add("FromDate");
            //settings.Columns.Add("ToDate");
            //settings.Columns.Add("Description");
            //settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "ServiceMaterialListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["ServiceMaterialInfo_DetailGrid_Data"];
        }

        [HttpGet]
        public ActionResult Frm_Service_Material_Window(string ActionMethod = "_New", string xID = "", string Info_LastEditedOn = null, string PopupName = "Dynamic_Content_popup", string GridToRefresh = "ServiceMaterialListGrid") 
        {
            GodlyServiceMaterial model = new GodlyServiceMaterial();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            model.ActionMethod = model.Tag.ToString();
            model.xID_GSM = xID ?? "";
            model.TitleX_GSM = "Godly Service Material";
            model.ProjectNameList_Data = LookUp_Get_ProjectOccasionList();
            model.WingListData_GSM = Get_WingsList();      
            model.MaterialCategoryList = GetMaterialCategory();      
            model.MaterialSubCategoryList = GetMaterialSubCategory();
            model.SaveAttachments = new List<Model_Attachment_Window>();
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || (model.Tag == Common.Navigation_Mode._New && string.IsNullOrWhiteSpace(model.xID_GSM) == false))
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d1 = BASE._ServiceMaterial_dbops.GetRecord(model.xID_GSM);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    string message = Messages.RecordChanged("Current Service Material");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Changed / Removed in Background!!','" + GridToRefresh + "');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                        {
                            string message = Messages.RecordChanged("Current Service Material");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Look_ProjList_GSM = d1.Rows[0]["GSM_PROJ_ID"].ToString() ?? null;
                model.Txt_Title_GSM = d1.Rows[0]["GSM_TITLE"].ToString() ?? "";
                model.Look_MaterialType_GSM = d1.Rows[0]["GSM_MATERIAL_TYPE"].ToString() ?? "";
                if (!Convert.IsDBNull(d1.Rows[0]["GSM_DATE_OF_PUBLISH"]) && CommonFunctions.IsDate(Convert.ToDateTime(d1.Rows[0]["GSM_DATE_OF_PUBLISH"])))
                {
                    model.PublishDate_GSM = Convert.ToDateTime(d1.Rows[0]["GSM_DATE_OF_PUBLISH"]);
                }
                else
                {
                    model.PublishDate_GSM = dtnull;
                }

                model.Txt_OnlineLink_GSM = d1.Rows[0]["GSM_ONLINE_LINK"].ToString() ?? "";
                model.Txt_ProgBrief_GSM = d1.Rows[0]["GSM_BRIEF_SUMMARY"].ToString() ?? "";
                model.Txt_ProgSpeaker_GSM = d1.Rows[0]["GSM_SPEAKER_AUTHOR_PUBLISHER"].ToString() ?? "";
                model.Txt_PreviewImagePath_GSM = d1.Rows[0]["GSM_PREVIEW_IMAGE_PATH"].ToString();
                model.Look_MaterialSubCategory_GSM = d1.Rows[0]["GSM_MATERIAL_SUB_CATEGORY"].ToString();
                model.Switch_PublicPrivate_GSM = Convert.ToBoolean(d1.Rows[0]["GSM_PRIVATE"]);
                model.Wings_List_GSM = new List<WingsList>();
                foreach (DataRow xRow in BASE._ServiceMaterial_dbops.GetWingsRecord(model.xID_GSM).Rows)
                {
                    model.Wings_List_GSM.Add(new Models.WingsList()
                    {
                        ID = xRow["SM_WING_ID"].ToString()
                    });
                }
                if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                {
                    List<Attachment_List> Attachments = BASE._Attachments_DBOps.GetList(model.xID_GSM);
                    if (Attachments != null && Attachments.Count > 0)
                    {
                        model.AttachmentDetails = new List<SR_AttachmentDetails>();
                        model.PreviewAttachmentDetails = new List<SR_AttachmentDetails>();
                        for (int i = 0; i < Attachments.Count; i++)
                        {
                            SR_AttachmentDetails row = new SR_AttachmentDetails();
                            row.Attachment_ID = Attachments[i].ID;
                            row.Attachment_FileName = Attachments[i].File_Name;
                            row.Attachment_Description = Attachments[i].Only_Description;
                            row.Attachment_Type = MimeMapping.GetMimeMapping(Attachments[i].File_Name);
                            row.Attachment_Path = GetAttachmentPath(Attachments[i].ID, Attachments[i].File_Name);
                            if (Attachments[i].Misc_Rec_ID == "773a84f7-d926-4fda-866b-af972394980d") //download attachment
                            {
                                model.AttachmentDetails.Add(row);
                            }
                            else if (Attachments[i].Misc_Rec_ID == "e9123660-5011-48e9-b7ce-668060188a31") //preview attachment
                            {
                                model.PreviewAttachmentDetails.Add(row);
                            }
                        }
                        if (model.AttachmentDetails != null && model.AttachmentDetails.Count > 0)
                        {
                            model.AttachmentFileNames = string.Join(", ", model.AttachmentDetails.Select(x => x.Attachment_FileName));
                        }
                        if (model.PreviewAttachmentDetails != null && model.PreviewAttachmentDetails.Count > 0)
                        {
                            model.PreviewAttachmentFileNames = string.Join(", ", model.PreviewAttachmentDetails.Select(x => x.Attachment_FileName));
                        }
                    }
                }
            }
            if (model.Tag == Common.Navigation_Mode._New)
            {
                model.xID_GSM = Guid.NewGuid().ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Service_Material_Window(GodlyServiceMaterial model) 
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
            }
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        DataTable ServMat_DbOps = BASE._ServiceMaterial_dbops.GetRecord(model.xID_GSM);
                        if (ServMat_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Service Material");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.LastEditedOn),Convert.ToDateTime(ServMat_DbOps.Rows[0]["REC_EDIT_ON"])) == false)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Service Material");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    //if (string.IsNullOrWhiteSpace(model.Look_ProjList_GSM) == true)
                    //{
                    //    jsonParam.message = "Project Name Not Selected. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Look_ProjList_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}//Redmine Bug #136129 fixed
                    if (string.IsNullOrWhiteSpace(model.Txt_Title_GSM) || model.Txt_Title_GSM.Trim().Length == 0)
                    {
                        jsonParam.message = "Title cannot be Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Title_GSM";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Look_MaterialType_GSM) || model.Look_MaterialType_GSM.Trim().Length == 0)
                    {
                        jsonParam.message = "Material Type Not Selected...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Look_MaterialType_GSM";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Look_MaterialSubCategory_GSM))
                    {
                        jsonParam.message = "Material Sub Category Not Selected...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Look_MaterialSubCategory_GSM";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //if (string.IsNullOrWhiteSpace(model.Txt_OnlineLink_GSM) || model.Txt_OnlineLink_GSM.Trim().Length == 0)
                    //{
                    //    jsonParam.message = "Online Link cannot be Blank. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Txt_OnlineLink_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}
                    //if (CommonFunctions.IsDate(model.PublishDate_GSM) == false)
                    //{
                    //    jsonParam.message = "Date of Publish Incorrect / Blank. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "PublishDate_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}//Redmine Bug #136129 fixed
                    //if (string.IsNullOrWhiteSpace(model.Txt_ProgSpeaker_GSM) || model.Txt_ProgSpeaker_GSM.Trim().Length == 0)
                    //{
                    //    jsonParam.message = "Speaker / Author / Publisher cannot be Blank. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Txt_ProgSpeaker_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}
                    //if (string.IsNullOrWhiteSpace(model.Txt_ProgBrief_GSM) == true || model.Txt_ProgBrief_GSM.Trim().Length == 0)
                    //{
                    //    jsonParam.message = "Brief Summary cannot be Blank. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Txt_ProgBrief_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}//Redmine Bug #136129 fixed
                    //if (model.Wings_List_GSM.Where(x => x.Selected == true).Count() <= 0)
                    //{
                    //    jsonParam.message = "Wing Name Not Selected. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Wings_List_GSM";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}//Redmine Bug #136129 fixed
                }

                Param_Txn_Insert_ServiceMaterial InNewParam = new Param_Txn_Insert_ServiceMaterial();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New)         //'new
                {
                    Common_Lib.RealTimeService.Parameter_Insert_ServiceMaterial InParam = new Common_Lib.RealTimeService.Parameter_Insert_ServiceMaterial();
                    InParam.ProjectID = model.Look_ProjList_GSM ?? null;
                    InParam.Title = model.Txt_Title_GSM ?? "";
                    InParam.Material_Type = model.Look_MaterialType_GSM ?? "";
                    InParam.PublishDate = CommonFunctions.IsDate(model.PublishDate_GSM) ? Convert.ToDateTime(model.PublishDate_GSM).ToString(BASE._Server_Date_Format_Long) : dtnull.ToString();
                    InParam.Brief_Summary = model.Txt_ProgBrief_GSM ?? "";
                    InParam.OnlineLink = model.Txt_OnlineLink_GSM ?? "";
                    InParam.Speaker_Author_Publisher = model.Txt_ProgSpeaker_GSM ?? "";
                    
                    InParam.Rec_ID = model.xID_GSM ?? "";
                    InParam.CenID = BASE._open_Cen_ID.ToString();
                    InParam.PreviewImagePath = model.Txt_PreviewImagePath_GSM;
                    InParam.MaterialSubCategory = model.Look_MaterialSubCategory_GSM;
                    InParam.Is_Private = model.Switch_PublicPrivate_GSM;
                    InNewParam.param_Insert_ServiceMaterial = InParam;
                    
                    List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial> InWingsInfo = new List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial>();
                    if (model.Wings_List_GSM.Where(x => x.Selected == true).Count() > 0)
                    {
                        foreach (var CurrSelection in model.Wings_List_GSM.Where(x => x.Selected == true))
                        {
                            Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial InWings = new Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial();
                            InWings.Sr_ID = model.xID_GSM;
                            InWings.WingID = CurrSelection.ID;
                            InWings.Rec_ID = System.Guid.NewGuid().ToString();
                            InWingsInfo.Add(InWings);
                        }
                    }
                    
                    InNewParam.InsertWings = InWingsInfo.ToArray();

                    if (!BASE._ServiceMaterial_dbops.InsertServiceMaterial_Txn(InNewParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    string filesUploadMessage = "";
                    if (model.SaveAttachments!=null && model.SaveAttachments.Count > 0) 
                    {
                        for (int i = 0; i < model.SaveAttachments.Count; i++) 
                        {
                            if (string.IsNullOrWhiteSpace(model.SaveAttachments[i].Help_Document_NameID) == false)
                            {
                                var x = Attachment_Save(model.SaveAttachments[i]);
                                string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)x).Data);
                                Return_Attachment_Post DynamicData = JsonConvert.DeserializeObject<Return_Attachment_Post>(jsonString);
                                filesUploadMessage = filesUploadMessage+DynamicData.Message.ToString();
                            }
                        }
                    }
           
                    jsonParam.message = Common_Lib.Messages.SaveSuccess+"<br>"+ filesUploadMessage;
                    jsonParam.title = model.TitleX_GSM;
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, xID = model.xID_GSM }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_Update_ServiceMaterial EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_ServiceMaterial();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)   //'edit
                {
                    Common_Lib.RealTimeService.Parameter_Update_ServiceMaterial UpParam = new Common_Lib.RealTimeService.Parameter_Update_ServiceMaterial();
                    UpParam.ProjectID = model.Look_ProjList_GSM ?? null;                    
                    UpParam.Title = model.Txt_Title_GSM ?? "";
                    UpParam.Material_Type = model.Look_MaterialType_GSM ?? "";
                    UpParam.PublishDate = CommonFunctions.IsDate(model.PublishDate_GSM) ? Convert.ToDateTime(model.PublishDate_GSM).ToString(BASE._Server_Date_Format_Long) : dtnull.ToString();
                    UpParam.Brief_Summary = model.Txt_ProgBrief_GSM ?? "";
                    UpParam.OnlineLink = model.Txt_OnlineLink_GSM ?? "";
                    UpParam.Speaker_Author_Publisher = model.Txt_ProgSpeaker_GSM ?? "";
                    UpParam.Rec_ID = model.xID_GSM;
                    UpParam.CenID = BASE._open_Cen_ID.ToString();
                    UpParam.PreviewImagePath = model.Txt_PreviewImagePath_GSM;
                    UpParam.MaterialSubCategory = model.Look_MaterialSubCategory_GSM;
                    UpParam.Is_Private = model.Switch_PublicPrivate_GSM;
                    EditParam.param_Update_ServiceMaterial = UpParam;


                    EditParam.RecID_DeleteWing = model.xID_GSM;
                    List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial> InWingsInfo = new List<Parameter_InsertWings_ServiceMaterial>();

                    if (model.Wings_List_GSM.Where(x => x.Selected == true).Count() > 0)
                    {
                        foreach (var currSelection in model.Wings_List_GSM.Where(x => x.Selected == true))
                        {
                            Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial InWing = new Common_Lib.RealTimeService.Parameter_InsertWings_ServiceMaterial();
                            InWing.Sr_ID = model.xID_GSM;
                            InWing.WingID = currSelection.ID;
                            InWing.Rec_ID = System.Guid.NewGuid().ToString();
                            InWingsInfo.Add(InWing);
                        }
                    }
                    
                    EditParam.InsertWings = InWingsInfo.ToArray();                    

                    if (!BASE._ServiceMaterial_dbops.UpdateServiceMaterial_Txn(EditParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    string filesUploadMessage = "";
                    if (model.SaveAttachments != null && model.SaveAttachments.Count > 0)
                    {
                        for (int i = 0; i < model.SaveAttachments.Count; i++)
                        {
                            if (string.IsNullOrWhiteSpace(model.SaveAttachments[i].Help_Document_NameID) == false)
                            {
                                var x = Attachment_Save(model.SaveAttachments[i]);
                                string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)x).Data);
                                Return_Attachment_Post DynamicData = JsonConvert.DeserializeObject<Return_Attachment_Post>(jsonString);
                                filesUploadMessage = filesUploadMessage + DynamicData.Message.ToString();
                            }
                        }
                    }
                    jsonParam.message = Common_Lib.Messages.UpdateSuccess+"<br>"+ filesUploadMessage;
                    jsonParam.title = model.TitleX_GSM;
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, xID = model.xID_GSM }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_Delete_ServiceMaterial DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_ServiceMaterial();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)  //'DELETE
                {
                    DelParam.RecID_DeleteWing = model.xID_GSM;
                    DelParam.RecID_Delete = model.xID_GSM;

                    if (!BASE._ServiceMaterial_dbops.DeleteServiceMaterial_Txn(DelParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX_GSM;
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<ProjectNameList_GSM> LookUp_Get_ProjectOccasionList(string Remark2Filter = "")
        {
            DataTable d1 = BASE._ServiceMaterial_dbops.GetProjects_Occasions("Name", "ID", Remark2Filter);
            return DatatableToModel.DataTabletoProjectNameList_GSM(d1);
        }
        public List<stringData> GetMaterialCategory()
        {
            return DatatableToModel.DataTableTo_ListString(BASE._ServiceMaterial_dbops.GetMaterialCategory(), "MaterialType");
        }       
        public List<stringData> GetMaterialSubCategory()
        {
            return DatatableToModel.DataTableTo_ListString(BASE._ServiceMaterial_dbops.GetMaterialSubCategory(), "MaterialSubCategory");
        }  
        public List<WingsList> Get_WingsList()
        {
            DataTable d1 = BASE._SR_DBOps.GetWings();
            return DatatableToModel.DataTabletoGSR_GetWingsList(d1);
        }
        public ActionResult Frm_Export_Options()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_GodlyServiceMaterial, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            }
            return PartialView();
        }
        public void ServiceMaterial_UserRights() 
        {
            ViewData["Service_Material_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_GodlyServiceMaterial, "Export");
            ViewData["Service_Material_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_GodlyServiceMaterial, "List");
            ViewData["Service_Material_AddRight"] = CheckRights(BASE, ClientScreen.Facility_GodlyServiceMaterial, "Add");
            ViewData["Service_Material_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_GodlyServiceMaterial, "Update");
            ViewData["Service_Material_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_GodlyServiceMaterial, "Delete");
            ViewData["Service_Material_ReportRight"] = CheckRights(BASE, ClientScreen.Facility_GodlyServiceMaterial, "Report");

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
        }
        public void SessionClear() 
        {
            ClearBaseSession("_SerMat");
            Session.Remove("ServiceMaterialInfo_DetailGrid_Data");
        }
    }
}