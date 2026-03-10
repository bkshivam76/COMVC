using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Options.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using DevExpress.RichEdit.Export;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static Common_Lib.DbOperations.Forms;

namespace ConnectOneMVC.Areas.Options.Controllers
{
    [CheckLogin]
    public class CreateFormController : BaseController
    {
        // GET: Options/CreateForm   
        public ActionResult Frm_CreateForm_Window(string Tag = "_New", string InstanceRecID = "", string SRID = null, string EventID = null, string ProjectID = null, string Screen = "")
        {
            CreateForm model = new CreateForm();
            Chart_User_Rights();
            model.Tag = Tag;
            model.Event_ID = EventID;
            model.SR_ID = SRID;
            model.GenerateReg_createForm = true;
            model.ProfileSettings = new List<FormUserProfileSetting>();
            model.FormInstanceID = InstanceRecID;
            model.GroupHeadingsList = new List<string>();
            model.PreDefinedOptionList = GetPredefinedOptionList();
            model.QuestionTagList = GetQuestionTagList();
            model.UserProfileVisibility_createForm = true;
            DataTable Admin= BASE._Form_dbops.GetAdminEmailAndWhatsApp(BASE._open_Cen_ID);
            if (Admin.Rows.Count > 0) 
            {
                model.DefaultAdminEmail_createForm = Admin.Rows[0]["AdminEmail"].ToString();
                model.DefaultAdminWhatsApp_createForm = Admin.Rows[0]["AdminMobile"].ToString();
            }
            if (Tag == "_New")
            {
                model.ProfileSettings = FillProfileSettings();
                model.GroupProfileSettings = FillProfileSettings(null, "GROUP");
            }
            //if (string.IsNullOrWhiteSpace(SRID) == false)
            //{
            //    DataTable d1 = BASE._SR_DBOps.GetRecord(SRID);
            //    if (d1 != null && d1.Rows.Count > 0)
            //    {
            //        model.ProjectID_createForm = d1.Rows[0]["SR_PROJ_ID"].ToString();
            //    }
            //}
            if (string.IsNullOrWhiteSpace(ProjectID) == false)
            {
                model.ProjectID_createForm = ProjectID;
            }
            if (Tag != "_New")
            {
                DataTable FormInfo = BASE._Form_dbops.GetFormRecord(InstanceRecID);
                DataTable FormQuestions = BASE._Form_dbops.GetFormQuestions(InstanceRecID);
                if (FormInfo == null || FormQuestions == null || FormInfo.Rows.Count == 0 || FormQuestions.Rows.Count == 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                model.ResponseCount = (int)BASE._Form_dbops.GetFormReponseCount(FormInfo.Rows[0]["REC_ID"].ToString());
                if (Tag == "_Delete")
                {
                    if (model.ResponseCount > 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Form Cannot Be Deleted<br> Responses Have Been Posted','Error!!');</script>");
                    }
                }
                model.Event_ID = FormInfo.Rows[0]["CSN_EVENT_ID"].ToString();
                model.SR_ID = FormInfo.Rows[0]["CSN_SERVICE_REPORT_ID"].ToString();
                model.FormID = FormInfo.Rows[0]["REC_ID"].ToString();
                model.FormName_createForm = FormInfo.Rows[0]["CI_CHARTNAME"].ToString();
                model.FormTitle_createForm = FormInfo.Rows[0]["CI_CHARTTITLE"].ToString();
                model.FormDescription_createForm = FormInfo.Rows[0]["CI_DESCRIPTION"].ToString();
                model.FormLoginRequired_createForm = FormInfo.Rows[0]["CI_LOGIN_REQUIRED"].ToString();
                model.ProjectID_createForm = FormInfo.Rows[0]["CI_PROJECT_ID"].ToString();
                model.Frequency_createForm = FormInfo.Rows[0]["CI_FREQUENCY"].ToString();
                model.Purpose_createForm = FormInfo.Rows[0]["CI_PURPOSE"].ToString();
                model.ApprovalReq_createForm = Convert.ToBoolean(FormInfo.Rows[0]["CI_APPROVAL_REQUIREMENT"]);
                model.ConfirmMsg_createForm = FormInfo.Rows[0]["CI_CONFIRMATION_MESSAGE"].ToString();
                model.GenerateReg_createForm = Convert.ToBoolean(FormInfo.Rows[0]["CI_GENERATE_REG_NO"]);
                model.GenerateRegApproval_createForm = Convert.ToBoolean(FormInfo.Rows[0]["CI_GENERATE_REG_NO_ON_APPROVAL"]);          
                if (Convert.IsDBNull(FormInfo.Rows[0]["CI_SCHEDULE_ID"]) == false)
                {
                    model.CustomSchedule_createForm = Convert.ToInt32(FormInfo.Rows[0]["CI_SCHEDULE_ID"]);
                }
                model.NotificationOnInstanceCreation_createForm = Convert.ToBoolean(FormInfo.Rows[0]["CI_NOTIFICATION_INST_CREATION"]);
                string RegNoFormat = FormInfo.Rows[0]["CI_REG_NO_FORMAT"].ToString();
                if (string.IsNullOrWhiteSpace(RegNoFormat) == false)
                {
                    model.RegNo_createForm = "#REGNO#";
                    string[] split = RegNoFormat.Split(new string[] { "#REGNO#" }, StringSplitOptions.None);
                    model.RegNoPrefix_createForm = split[0];
                    model.RegNoSuffix_createForm = split[1];
                }
                model.AllowResubmission_createForm = Convert.ToBoolean(FormInfo.Rows[0]["CI_ALLOW_RESUBMISSION"]);
                model.ApprovalMsg_createForm = FormInfo.Rows[0]["CI_APPROVAL_MESSAGE"].ToString();
                model.RejectionMsg_createForm = FormInfo.Rows[0]["CI_REJECTION_MESSAGE"].ToString();
                model.PreRequiredChartSrID_createForm = FormInfo.Rows[0]["CSN_PRE_REQUIRED_CHART_SR_ID"].ToString();
                if (Convert.IsDBNull(FormInfo.Rows[0]["CI_FROM_DATE"]) == false)
                {
                    model.StartDate_createForm = Convert.ToDateTime(FormInfo.Rows[0]["CI_FROM_DATE"]);
                }
                if (Convert.IsDBNull(FormInfo.Rows[0]["CI_TO_DATE"]) == false)
                {
                    model.EndDate_createForm = Convert.ToDateTime(FormInfo.Rows[0]["CI_TO_DATE"]);
                }
                if (Convert.IsDBNull(FormInfo.Rows[0]["CSN_ACTIVE_FROM"]) == false)
                {
                    model.ActiveFrom_createForm = new DateTime(((TimeSpan)FormInfo.Rows[0]["CSN_ACTIVE_FROM"]).Ticks);
                }
                if (Convert.IsDBNull(FormInfo.Rows[0]["CSN_ACTIVE_TO"]) == false)
                {
                    model.ActiveTo_createForm = new DateTime(((TimeSpan)FormInfo.Rows[0]["CSN_ACTIVE_TO"]).Ticks);
                }
                model.StartDateMsg_createForm = FormInfo.Rows[0]["CI_CHART_NOT_STARTED_MESSAGE"].ToString();
                model.EndDateMsg_createForm = FormInfo.Rows[0]["CI_CHART_ENDED_MESSAGE"].ToString();
                model.DisplayMode_createForm = FormInfo.Rows[0]["CI_DISPLAY_MODE"].ToString();
                if (FormInfo.Rows[0]["CI_FILE_NAME"].ToString().Length > 0)
                {
                    model.FormImagePath = GetAttachmentPath(model.FormID, FormInfo.Rows[0]["CI_FILE_NAME"].ToString(), true);
                    model.FormImageWidth_createForm = Convert.ToInt32(FormInfo.Rows[0]["CI_IMAGE_WIDTH"]);
                    model.FormImageHeight_createForm = Convert.ToInt32(FormInfo.Rows[0]["CI_IMAGE_HEIGHT"]);
                    model.FormImageFileName = FormInfo.Rows[0]["CI_FILE_NAME"].ToString();
                }
                if (FormInfo.Rows[0]["CI_LOGIN_FILE_NAME"].ToString().Length > 0)
                {
                    model.LoginImagePath = GetAttachmentPath(model.FormID, FormInfo.Rows[0]["CI_LOGIN_FILE_NAME"].ToString(), true);
                    model.LoginImageFileName = FormInfo.Rows[0]["CI_LOGIN_FILE_NAME"].ToString();
                }
                if (FormInfo.Rows[0]["CI_THUMB_FILE_NAME"].ToString().Length > 0)
                {
                    model.ThumbsImagePath = GetAttachmentPath(model.FormID, FormInfo.Rows[0]["CI_THUMB_FILE_NAME"].ToString(), true);
                    model.ThumbsImageFileName = FormInfo.Rows[0]["CI_THUMB_FILE_NAME"].ToString();
                }
                if (FormInfo.Rows[0]["CI_LOGIN_RESPONSIVE_FILE_NAME"].ToString().Length > 0)
                {
                    model.ResponsiveImagePath = GetAttachmentPath(model.FormID, FormInfo.Rows[0]["CI_LOGIN_RESPONSIVE_FILE_NAME"].ToString(), true);
                    model.ResponsiveImageFileName = FormInfo.Rows[0]["CI_LOGIN_RESPONSIVE_FILE_NAME"].ToString();
                }
                DataTable groups = BASE._Form_dbops.GetQuestionGroups(model.FormID);
                if (groups != null)
                {
                    model.GroupHeadingsList = groups.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    model.GroupHeadingsList.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                }
                model.FormBgColor_createForm = FormInfo.Rows[0]["CI_CHART_BG_COLOR"].ToString();
                model.QuestionBgColor_createForm = FormInfo.Rows[0]["CI_QUESTION_BG_COLOR"].ToString();
                model.QuestionFgColor_createForm = FormInfo.Rows[0]["CI_QUESTION_FG_COLOR"].ToString();
                model.QuestionFontsize_createForm = FormInfo.Rows[0]["CI_QUESTION_FONT_SIZE"].ToString();
                model.FormBgImage_createForm = FormInfo.Rows[0]["CI_CHART_BG_IMAGE_PATH"].ToString();
                model.GroupTitleBgColor_createForm = FormInfo.Rows[0]["CI_GROUPTITLE_BG_COLOR"].ToString();
                model.GroupTitleFgColor_createForm = FormInfo.Rows[0]["CI_GROUPTITLE_FG_COLOR"].ToString();
                model.GroupTitleFontsize_createForm = FormInfo.Rows[0]["CI_GROUPTITLE_FONT_SIZE"].ToString();
                if (Convert.IsDBNull(FormInfo.Rows[0]["CI_MAX_ENTRIES_ALLOWED"]) == false)
                {
                    model.MaxEntriesAllowed_createForm = Convert.ToInt32(FormInfo.Rows[0]["CI_MAX_ENTRIES_ALLOWED"]);
                }
                if (Convert.IsDBNull(FormInfo.Rows[0]["CI_MAX_GROUP_REGISTRATIONS_ALLOWED"]) == false)
                {
                    model.Max_GroupRegistrations_Allowed_createForm = Convert.ToInt32(FormInfo.Rows[0]["CI_MAX_GROUP_REGISTRATIONS_ALLOWED"]);
                }
                model.ProfileSettings = FillProfileSettings(model.FormID);
                model.GroupProfileSettings = FillProfileSettings(model.FormID, "GROUP");
                int UserProfileVisibleFields = model.ProfileSettings.Count(x => x.Visible == true);
                if (UserProfileVisibleFields == 0)
                {
                    model.UserProfileVisibility_createForm = false;
                }
                if (model.Max_GroupRegistrations_Allowed_createForm > 0)
                {
                    model.RegistrationType_createForm = true;
                }
                model.Section = new List<FormQuestions>();
                DataView dv = FormQuestions.DefaultView;
                dv.Sort = "CQ_SRNO ASC";
                FormQuestions = dv.ToTable();
                DataTable FormQuestion_options = BASE._Form_dbops.GetFormQuestion_Options("", model.FormID);
                foreach (DataRow d1 in FormQuestions.Rows)
                {
                    FormQuestions newQues = new FormQuestions();
                    newQues.Question = d1["CQ_CHARTQUESTION"].ToString();
                    newQues.Mode = d1["CQ_MODE"].ToString();
                    newQues.Required = Convert.ToBoolean(d1["CQ_REQUIRED"]);
                    newQues.Type = d1["CQ_TYPE"].ToString();
                    newQues.QuestionSrNo = Convert.ToInt32(d1["CQ_SRNO"]);
                    newQues.GroupHeading = d1["CQ_GroupName"].ToString();
                    newQues.Description = d1["CQ_DESCRIPTION"].ToString();
                    newQues.DefaultValue = d1["CQ_DEFAULT_VALUE"].ToString();
                    newQues.Tag = d1["CQ_TAG"].ToString();
                    if (Convert.IsDBNull(d1["CQ_DEFAULT_VISIBILITY"]))
                    {
                        newQues.DefaultVisibility = true;
                    }
                    else
                    {
                        newQues.DefaultVisibility = Convert.ToBoolean(d1["CQ_DEFAULT_VISIBILITY"]);
                    }
                    if (Convert.IsDBNull(d1["CQ_RowNo"]) == false && d1["CQ_RowNo"].ToString().Length > 0)
                    {
                        newQues.RowNo = Convert.ToInt32(d1["CQ_RowNo"]);
                    }
                    else { newQues.RowNo = newQues.QuestionSrNo; }
                    if (Convert.IsDBNull(d1["CQ_ColumnSpan"]) == false && d1["CQ_ColumnSpan"].ToString().Length > 0)
                    {
                        newQues.ColumnSpan = Convert.ToInt32(d1["CQ_ColumnSpan"]);
                    }
                    else { newQues.ColumnSpan = 12; }
                    if (Convert.IsDBNull(d1["CQ_MIN"]) == false && d1["CQ_MIN"].ToString().Length > 0)
                    {
                        newQues.Min = Convert.ToInt64(d1["CQ_MIN"]);
                    }
                    if (Convert.IsDBNull(d1["CQ_MAX"]) == false && d1["CQ_MAX"].ToString().Length > 0)
                    {
                        newQues.Max = Convert.ToInt64(d1["CQ_MAX"]);
                    }
                    if (newQues.Mode == "Date" || newQues.Mode == "Time")
                    {
                        if (newQues.Min != null)
                        {
                            newQues.MinDateTime = new DateTime((long)newQues.Min);
                        }
                        if (newQues.Max != null)
                        {
                            newQues.MaxDateTime = new DateTime((long)newQues.Max);
                        }
                    }
                    else if (newQues.Mode == "Paragraph" || newQues.Mode == "Short Answer")
                    {
                        newQues.MaxLength = Convert.ToInt32(newQues.Max);
                    }
                    newQues.QuestionFormula = d1["CQ_FORMULA"].ToString();
                    newQues.Rec_ID = d1["REC_ID"].ToString();
                    if (d1["CQ_FILE_NAME"].ToString().Length > 0)
                    {
                        newQues.QuestionImagePath = GetAttachmentPath(newQues.Rec_ID, d1["CQ_FILE_NAME"].ToString(), true);
                        newQues.QuestionImageWidth = Convert.ToInt32(d1["CQ_IMAGE_WIDTH"]);
                        newQues.QuestionImageHeight = Convert.ToInt32(d1["CQ_IMAGE_HEIGHT"]);
                        newQues.QuestionImageFileName = d1["CQ_FILE_NAME"].ToString();
                    }
                    newQues.Options = new List<QuestionOptions>();
                    if (newQues.Mode == "CheckBox" || newQues.Mode == "DropDown" || newQues.Mode == "RadioButton")
                    {
                        DataRow[] opt = FormQuestion_options.Select("CQO_QUESTION_ID=" + newQues.Rec_ID);
                        if (opt != null && opt.Count() > 0)
                        {
                            opt = opt.OrderBy(x => x.Field<int>("CQO_SRNO")).ToArray();
                            foreach (DataRow d3 in opt)
                            {
                                QuestionOptions newOpt = new QuestionOptions();
                                newOpt.Options = d3["CQO_OPTION"].ToString();
                                newOpt.Rec_ID = d3["REC_ID"].ToString();
                                newOpt.Option_SrNo = Convert.ToInt32(d3["CQO_SRNO"]);
                                newOpt.QuestionID = d3["CQO_QUESTION_ID"].ToString();
                                if (d3["CQO_FILE_NAME"].ToString().Length > 0)
                                {
                                    newOpt.optionImagePath = GetAttachmentPath(newOpt.Rec_ID, d3["CQO_FILE_NAME"].ToString(), true);
                                    newOpt.OptionImageFileName = d3["CQO_FILE_NAME"].ToString();
                                }
                                newOpt.DependentQuestion = d3["CQO_Dependent_Question"].ToString();
                                newOpt.DependentQuestionVisibility = string.IsNullOrWhiteSpace(newOpt.DependentQuestion) ? null : Convert.ToBoolean(d3["CQO_Dependent_Question_Visibility"]) ? "Show" : "Hide";
                                newOpt.Points = Convert.IsDBNull(d3["CQO_Points"]) ? (double?)null : Convert.ToDouble(d3["CQO_Points"]);
                                newQues.Options.Add(newOpt);
                            }
                        }
                        newQues.IsOptionsPredefined = string.IsNullOrWhiteSpace(newQues.Type) == false ? true : false;
                        newQues.OptionPredefinedMiscID = newQues.Type;
                    }
                    model.Section.Add(newQues);
                }
                DataTable instances = BASE._Form_dbops.GetAllChartInstance(model.FormID);
                if (instances != null)
                {
                    model.CreatedInstanceCount_createForm = instances.Rows.Count;
                    if (instances.Rows.Count > 0)
                    {
                        if (Convert.IsDBNull(instances.Rows[0]["CSN_FROM"]) == false)
                        {
                            model.LastInstance_StartDate_createForm = Convert.ToDateTime(instances.Rows[0]["CSN_FROM"]);
                        }
                        if (Convert.IsDBNull(instances.Rows[0]["CSN_TO"]) == false)
                        {
                            model.LastInstance_EndDate_createForm = Convert.ToDateTime(instances.Rows[0]["CSN_TO"]);
                        }
                    }
                }
                DataSet FormNotifications = BASE._Form_dbops.GetFormNotifications(Int32.Parse(model.FormInstanceID));              
                if (FormNotifications.Tables.Count == 2)
                {
                    if (FormNotifications.Tables[0].Rows.Count > 0) //template and settings
                    {
                        DataTable Noti = FormNotifications.Tables[0];
                        for (int i = 0; i < Noti.Rows.Count; i++)
                        {
                            if (Noti.Rows[i]["SCNS_NOTIFICATION_MODE"].Equals("EMAIL"))
                            {
                                model.IsEmailNotification_createForm = true;
                                //notification_Setting.IsEmailVisible = true;
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("ADD FORM"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.ConfirmMsg_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsEmailConfirmEnabled_createForm = true;
                                        //notification_Setting.EmailConfirm = model.ConfirmMsg_createForm;
                                        //notification_Setting.IsEmailConfirmVisible = model.IsEmailConfirmEnabled_createForm;
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("APPROVE"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.ApprovalMsg_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsEmailAcceptEnabled_createForm = true;
                                        //notification_Setting.EmailApprove = model.ApprovalMsg_createForm;
                                        //notification_Setting.IsEmailApproveVisible = model.IsEmailAcceptEnabled_createForm;
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("REJECT"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.RejectionMsg_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsEmailRejectEnabled_createForm = true;
                                        //notification_Setting.EmailReject = model.RejectionMsg_createForm;
                                        //notification_Setting.IsEmailRejectVisible = model.IsEmailRejectEnabled_createForm;
                                    }
                                }
                            }
                            else if (Noti.Rows[i]["SCNS_NOTIFICATION_MODE"].Equals("WHATSAPP"))
                            {
                                model.IsWhatsappNotification_createForm = true;
                                //notification_Setting.IsWhatsappVisible = true;
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("ADD FORM"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        //model.ConfirmWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString().ConvertWhatsApptoHtml();
                                        model.ConfirmWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();                                        
                                        model.IsWhatsappConfirmEnabled_createForm = true;
                                        //notification_Setting.WhatsappConfirm = model.ConfirmWhatsapp_createForm;
                                        //notification_Setting.IsWhatsappConfirmVisible = model.IsWhatsappConfirmEnabled_createForm;
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("APPROVE"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        //model.ApprovalWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString().ConvertWhatsApptoHtml();
                                        model.ApprovalWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsWhatsappAcceptEnabled_createForm = true;
                                        //notification_Setting.WhatsappApprove = model.ApprovalWhatsapp_createForm;
                                        //notification_Setting.IsWhatsappApproveVisible = model.IsWhatsappAcceptEnabled_createForm;                                        
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("REJECT"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        //model.RejectionWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString().ConvertWhatsApptoHtml();
                                        model.RejectionWhatsapp_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsWhatsappRejectEnabled_createForm = true;
                                        //notification_Setting.WhatsappReject = model.RejectionWhatsapp_createForm;
                                        //notification_Setting.IsWhatsappRejectVisible = model.IsWhatsappRejectEnabled_createForm;                                        
                                    }
                                }
                            }
                            else if (Noti.Rows[i]["SCNS_NOTIFICATION_MODE"].Equals("SMS"))
                            {
                                model.IsSMSNotification_createForm = true;
                                //notification_Setting.IsSMSVisible = true;
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("ADD FORM"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.ConfirmSMS_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsSMSConfirmEnabled_createForm = true;
                                        //notification_Setting.SMSConfirm = model.ConfirmSMS_createForm;
                                        ///notification_Setting.IsSMSConfirmVisible = model.IsSMSConfirmEnabled_createForm;
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("APPROVE"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.ApprovalSMS_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsSMSAcceptEnabled_createForm = true;
                                        //notification_Setting.SMSApprove = model.ApprovalSMS_createForm;
                                        //notification_Setting.IsSMSApproveVisible = model.IsSMSAcceptEnabled_createForm;
                                    }
                                }
                                if (Noti.Rows[i]["SCNS_NOTIFICATION_EVENT"].ToString().Contains("REJECT"))
                                {
                                    if (string.IsNullOrWhiteSpace(Noti.Rows[i]["TEMPLATE_TEXT"].ToString()) == false)
                                    {
                                        model.RejectionSMS_createForm = Noti.Rows[i]["TEMPLATE_TEXT"].ToString();
                                        model.IsSMSRejectEnabled_createForm = true;
                                        //notification_Setting.SMSReject = model.RejectionSMS_createForm;
                                        //notification_Setting.IsSMSRejectVisible = model.IsSMSRejectEnabled_createForm;
                                    }
                                }
                            }
                        }
                    }
                    if (FormNotifications.Tables[1].Rows.Count > 0) //batches
                    {
                        Notification_Setting_CreateForm notification_Setting = new Notification_Setting_CreateForm();
                        DataTable Batches = FormNotifications.Tables[1];
                        notification_Setting.AdminEmail_Notification_Setting_CreateForm = Batches.Rows[0]["NBI_ADMIN_EMAIL"].ToString();
                        notification_Setting.AdminWhatsappNo_Notification_Setting_CreateForm = Batches.Rows[0]["NBI_ADMIN_WHATSAPP_NO"].ToString();
                        notification_Setting.Categories_Notification_Setting_CreateForm = Batches.Rows[0]["NBI_RESPONSE_STATUS"].ToString();
                        if (string.IsNullOrWhiteSpace(notification_Setting.AdminWhatsappNo_Notification_Setting_CreateForm) == false) 
                        {
                            if (notification_Setting.AdminWhatsappNo_Notification_Setting_CreateForm.StartsWith("+91"))
                            {
                                notification_Setting.AdminWhatsappNo_Notification_Setting_CreateForm= notification_Setting.AdminWhatsappNo_Notification_Setting_CreateForm.Remove(0, 3);
                            }
                        }
                        for (int i = 0; i < Batches.Rows.Count; i++)
                        {
                            if (Batches.Rows[i]["NBI_NOTIFICATION_MODE"].Equals("Email"))
                            {
                                notification_Setting.CC_Email_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_CC"].ToString();
                                notification_Setting.BCC_Email_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_BCC"].ToString();
                                notification_Setting.ReplyToEmail_Email_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_REPLY_TO"].ToString();
                                notification_Setting.Radio_SenderEmail_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_SENDER_EMAIL_TYPE"].ToString();
                                if (notification_Setting.Radio_SenderEmail_Notification_Setting_CreateForm == "Private")
                                {
                                    notification_Setting.Email_Email_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_SENDER_EMAIL"].ToString();
                                    notification_Setting.Password_Email_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_SENDER_PASSWORD"].ToString();
                                }
                            }
                            if (Batches.Rows[i]["NBI_NOTIFICATION_MODE"].Equals("Whatsapp"))
                            {
                                notification_Setting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_SENDER_WHATSAPP_TYPE"].ToString();
                                notification_Setting.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_DELIVERY_SPEED"].ToString();
                                if (notification_Setting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm == "Private")
                                {
                                    notification_Setting.SenderWhatsapp_Notification_Setting_CreateForm = Batches.Rows[i]["NBI_SENDER_WHATSAPPNO"].ToString();
                                    if (string.IsNullOrWhiteSpace(notification_Setting.SenderWhatsapp_Notification_Setting_CreateForm) == false)
                                    {
                                        if (notification_Setting.SenderWhatsapp_Notification_Setting_CreateForm.StartsWith("+91"))
                                        {
                                            notification_Setting.SenderWhatsapp_Notification_Setting_CreateForm = notification_Setting.SenderWhatsapp_Notification_Setting_CreateForm.Remove(0, 3);
                                        }
                                    }                                    
                                }
                                string batchName = Batches.Rows[i]["NBI_BATCH_NAME"].ToString();
                                if ( batchName.Contains("_ADDFORM_") && model.IsWhatsappConfirmEnabled_createForm && (!string.IsNullOrWhiteSpace(batchName)))
                                {
                                    string[] batDataArr = batchName.Split('|');
                                    if (batDataArr.Length == 2)
                                    {
                                        string[] TemplateAndLang = batDataArr[1].Split('~');
                                        if (TemplateAndLang.Length == 2)
                                        {
                                            model.Template_DD_ConfirmWhatsapp_TemplatePreview_CreateForm = TemplateAndLang[0];
                                            model.ConfirmWhatsapp_Template_Language_createForm = TemplateAndLang[1];
                                        }
                                    }
                                }
                                if ( batchName.Contains("_APPROVE_") && model.IsWhatsappAcceptEnabled_createForm && (!string.IsNullOrWhiteSpace(batchName)))
                                {
                                    string[] batDataArr = batchName.Split('|');
                                    if (batDataArr.Length == 2)
                                    {
                                        string[] TemplateAndLang = batDataArr[1].Split('~');
                                        if (TemplateAndLang.Length == 2)
                                        {
                                            model.Template_DD_ApprovalWhatsapp_TemplatePreview_CreateForm = TemplateAndLang[0];
                                            model.ApprovalWhatsapp_Template_Language_createForm = TemplateAndLang[1];
                                        }
                                    }
                                }
                                if ( batchName.Contains("_REJECT_") && model.IsWhatsappRejectEnabled_createForm && (!string.IsNullOrWhiteSpace(batchName)))
                                {
                                    string[] batDataArr = batchName.Split('|');
                                    if (batDataArr.Length == 2)
                                    {
                                        string[] TemplateAndLang = batDataArr[1].Split('~');
                                        if (TemplateAndLang.Length == 2)
                                        {
                                            model.Template_DD_RejectWhatsapp_TemplatePreview_CreateForm = TemplateAndLang[0];
                                            model.RejectionWhatsapp_Template_Language_createForm = TemplateAndLang[1];
                                        }
                                    }
                                }
                            }
                        }
                        model.NotificationSetting = notification_Setting;
                    }
                }               
            }
            model.Sr_Event_createForm = string.IsNullOrWhiteSpace(model.SR_ID) ? string.IsNullOrWhiteSpace(model.Event_ID) ? null : model.Event_ID : model.SR_ID;
            model.Screen = Screen;
            //model.PrevStartDate_createForm = model.StartDate_createForm;
            return View(model);
        }
        public List<FormUserProfileSetting> FillProfileSettings(string FormID = null, string Type = "MAIN")
        {
            List<FormUserProfileSetting> settings = new List<FormUserProfileSetting>();
            DataSet profileSettings;
            DataTable dt_Visibility = new DataTable();
            DataTable dt_Mandatory = new DataTable();
            DataTable dt_Enabled = new DataTable();
            if (string.IsNullOrWhiteSpace(FormID) == false)
            {
                profileSettings = BASE._Form_dbops.Get_chartProfileSettings(Convert.ToInt32(FormID), Type);
                if (profileSettings != null && profileSettings.Tables.Count > 0)
                {
                    dt_Visibility = profileSettings.Tables[0];
                    dt_Mandatory = profileSettings.Tables[1];
                    dt_Enabled = profileSettings.Tables[2];
                }
            }
            for (int i = 0; i < 21; i++)
            {
                FormUserProfileSetting row = new FormUserProfileSetting();
                switch (i)
                {
                    case 0: //Full name
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["FULL NAME"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["FULL NAME"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["FULL NAME"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["FULL NAME"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 1: //Mobile
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["MOBILE NUMBER"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["MOBILE NUMBER"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["MOBILE NUMBER"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["MOBILE NUMBER"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 2: //Email
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["EMAIL ID"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["EMAIL ID"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["EMAIL ID"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["EMAIL ID"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 3: //Profile pic
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["PROFILE PICTURE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["PROFILE PICTURE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["PROFILE PICTURE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["PROFILE PICTURE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 4: //Gender
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["GENDER"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["GENDER"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["GENDER"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["GENDER"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 5: //Age
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["AGE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["AGE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["AGE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["AGE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 6: //DOB
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["DOB"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["DOB"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["DOB"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["DOB"].ToString();
                        }
                        else
                        {
                            row.Visible = false;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 7: //Country
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["COUNTRY"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["COUNTRY"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["COUNTRY"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["COUNTRY"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 8: //State
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["STATE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["STATE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["STATE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["STATE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 9: //City
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["CITY"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["CITY"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["CITY"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["CITY"].ToString();

                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 10: //Pincode
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["PINCODE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["PINCODE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["PINCODE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["PINCODE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 11: //Address
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["ADDRESS"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["ADDRESS"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["ADDRESS"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["ADDRESS"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 12: //Meditation Course
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["MEDITATION COURSE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["MEDITATION COURSE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["MEDITATION COURSE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["MEDITATION COURSE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = true;
                            row.Enable = true;
                        }
                        break;
                    case 13: // Course Date
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["DATE OF COURSE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["DATE OF COURSE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["DATE OF COURSE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["DATE OF COURSE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 14: //Course Centre
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["CURRENT CENTRE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["CURRENT CENTRE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["CURRENT CENTRE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["CURRENT CENTRE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 15: //BK Title
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["BK TITLE"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["BK TITLE"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["BK TITLE"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["BK TITLE"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 16: //Education
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["EDUCATION"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["EDUCATION"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["EDUCATION"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["EDUCATION"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 17: //Occupation
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["OCCUPATION"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["OCCUPATION"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["OCCUPATION"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["OCCUPATION"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 18: //Specialities
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["SPECIALITIES"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["SPECIALITIES"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["SPECIALITIES"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["SPECIALITIES"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 19: //ID Proof
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["ID PROOF"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["ID PROOF"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["ID PROOF"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["ID PROOF"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    case 20: //ID Number
                        if (dt_Visibility.Rows.Count > 0)
                        {
                            row.Visible = Convert.ToBoolean(dt_Visibility.Rows[0]["ID NUMBER"]);
                            row.Mandatory = Convert.ToBoolean(dt_Mandatory.Rows[0]["ID NUMBER"]);
                            row.Enable = Convert.ToBoolean(dt_Enabled.Rows[0]["ID NUMBER"]);
                            row.Rec_ID = dt_Visibility.Rows[1]["ID NUMBER"].ToString();
                        }
                        else
                        {
                            row.Visible = true;
                            row.Mandatory = false;
                            row.Enable = true;
                        }
                        break;
                    default:
                        row.Visible = true;
                        row.Mandatory = false;
                        row.Enable = true;
                        break;
                }
                settings.Add(row);
            }
            return settings;
        }
        public ActionResult Questions(int Secno, string Mode = "", string RowNo = null, string ColSpan = null)
        {
            ViewBag.Mode = Mode;
            ViewBag.RowNo = RowNo;
            ViewBag.ColSpan = ColSpan;
            return View(Secno);
        }
        public ActionResult Answers(string Mode, int Secno, string Purpose = "")
        {
            ViewBag.Mode = Mode;
            ViewBag.Purpose = Purpose;
            return View(Secno);
        }
        public ActionResult MultipleOption(string Mode, int Secno, int optCount, string Purpose = "")
        {
            ViewBag.Mode = Mode;
            ViewBag.optCount = optCount;
            ViewBag.Purpose = Purpose;
            return View(Secno);
        }
        [HttpPost]
        public ActionResult SaveForm(CreateForm model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                List<SecAndOptionData> QuesAndOptionsData = JsonConvert.DeserializeObject<List<SecAndOptionData>>(model.SecAndOptionData);
                //List<SecAndOptionData> QuesAndOptionsData = JsonConvert.DeserializeObject<List<SecAndOptionData>>(null);
                List<Deleted_Ques_Options> Deleted_Ques_option_RecIDs = JsonConvert.DeserializeObject<List<Deleted_Ques_Options>>(model.Deleted_Ques_option_RecIDs);
                if (string.IsNullOrWhiteSpace(model.FormName_createForm))
                {
                    jsonParam.message = "Name Is Required.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "FormName_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (BASE._Form_dbops.CheckIfChartNameIsUniqueInUID(model.FormName_createForm, model.FormID) == false)
                {
                    jsonParam.message = "Form Name Must Be Unique Within The UID.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "FormName_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.FormLoginRequired_createForm))
                {
                    jsonParam.message = "Login Mode Is Required.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "FormLoginRequired_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.Purpose_createForm))
                {
                    jsonParam.message = "Purpose Is Required.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "Purpose_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.Frequency_createForm))
                {
                    jsonParam.message = "Frequency Is Required.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "Frequency_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Frequency_createForm == "Custom" && model.CustomSchedule_createForm == null)
                {
                    jsonParam.message = "Schedule Is Required.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "CustomSchedule_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (/*model.Purpose_createForm == "REGISTRATION" ||*/ model.GenerateReg_createForm == true || model.GenerateRegApproval_createForm == true)
                {
                    if (string.IsNullOrWhiteSpace(model.RegNoPrefix_createForm) && string.IsNullOrWhiteSpace(model.RegNoSuffix_createForm))
                    {
                        jsonParam.message = "Registration No. Format Is Incomplete<br> Atleast One From Registration No. Prefix And Suffix Needs To Be Mentioned To Complete The Registration No. Format";
                        jsonParam.title = "Information";
                        jsonParam.focusid = "RegNoPrefix_createForm";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Purpose_createForm == "REGISTRATION")
                {
                    if (model.IsEmailNotification_createForm && string.IsNullOrWhiteSpace(model.ConfirmMsg_createForm) && model.IsEmailConfirmEnabled_createForm)
                    {
                        jsonParam.message = "Confirmation Message Upon Registration Is Required.";
                        jsonParam.title = "Information";
                        jsonParam.focusid = "ConfirmMsg_createForm";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ApprovalReq_createForm == true)
                {
                    //if (model.IsEmailVisible_createForm && string.IsNullOrWhiteSpace(model.ApprovalMsg_createForm))
                    //{
                    //    jsonParam.message = "Approval Message Is Required.";
                    //    jsonParam.title = "Information";
                    //    jsonParam.focusid = "ApprovalMsg_createForm";
                    //    jsonParam.result = false;
                    //    return Json(new
                    //    {
                    //        jsonParam
                    //    }, JsonRequestBehavior.AllowGet);
                    //}
                    //if (model.IsEmailVisible_createForm && string.IsNullOrWhiteSpace(model.RejectionMsg_createForm))
                    //{
                    //    jsonParam.message = "Rejection Message Is Required.";
                    //    jsonParam.title = "Information";
                    //    jsonParam.focusid = "RejectionMsg_createForm";
                    //    jsonParam.result = false;
                    //    return Json(new
                    //    {
                    //        jsonParam
                    //    }, JsonRequestBehavior.AllowGet);
                    //}
                }
                if ((model.Purpose_createForm == "REGISTRATION" || model.Purpose_createForm == "FEEDBACK") && string.IsNullOrWhiteSpace(model.SR_ID) == true)
                {
                    jsonParam.message = "Registration Or Feedback Form Must Be Created For Service Report or Event";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "Purpose_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                //if (string.IsNullOrWhiteSpace(model.ProjectID_createForm) == false && model.Purpose_createForm == "REGISTRATION" && (string.IsNullOrWhiteSpace(model.SR_ID)==true && string.IsNullOrWhiteSpace(model.Event_ID) == true)) 
                //{
                //    DataTable d1 = BASE._Form_dbops.GetRegistrationFormsCreatedInProjectOnly(model.ProjectID_createForm, model.FormID);
                //    if (d1 != null && d1.Rows.Count > 0) 
                //    {
                //        jsonParam.message = "Only One Registration Form Can Exist For A Project.<br>Registration Form Already Created For This Project<br>Form Name: <b>" + d1.Rows[0]["CI_CHARTNAME"].ToString()+"</b>";
                //        jsonParam.title = "Information";
                //        jsonParam.focusid = "Purpose_createForm";
                //        jsonParam.result = false;
                //        return Json(new
                //        {
                //            jsonParam
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //}
                if (string.IsNullOrWhiteSpace(model.ProjectID_createForm) == false && model.Purpose_createForm == "BASIC DETAILS" && (string.IsNullOrWhiteSpace(model.SR_ID) == true && string.IsNullOrWhiteSpace(model.Event_ID) == true))
                {
                    DataTable d1 = BASE._Form_dbops.GetBasicDetailsFormsCreatedInProjectOnly(model.ProjectID_createForm, model.FormID);
                    if (d1 != null && d1.Rows.Count > 0)
                    {
                        jsonParam.message = "Only One 'Basic Details' Form Can Exist For A Project.<br>Basic Details Form Already Created For This Project<br>Form Name: <b>" + d1.Rows[0]["CI_CHARTNAME"].ToString() + "</b>";
                        jsonParam.title = "Information";
                        jsonParam.focusid = "Purpose_createForm";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (QuesAndOptionsData.Count == 0)
                {
                    jsonParam.message = "No Questions Have Been Added.";
                    jsonParam.title = "Information";
                    jsonParam.focusid = "FormTitle_createForm";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Tag == "_Edit")
                {
                    //DataTable MInMAxDate = BASE._Form_dbops.Get_MinStart_And_MaxTo_Date_Of_Form_Instances(model.FormID);
                    //if (Convert.IsDBNull(MInMAxDate.Rows[0]["MIN_CSN_FROM"]) == false)
                    //{
                    //    if (model.StartDate_createForm != null)
                    //    {
                    //        DateTime InstanceMInDate = Convert.ToDateTime(MInMAxDate.Rows[0]["MIN_CSN_FROM"]);
                    //        if (model.StartDate_createForm > InstanceMInDate)
                    //        {
                    //            jsonParam.message = "Form Start Date Cannot Be Greater Than From Date Of First Instance. Date: <b>" + InstanceMInDate.ToString("dd-MM-yyyy") + "</b>";
                    //            jsonParam.title = "Information";
                    //            jsonParam.focusid = "StartDate_createForm";
                    //            jsonParam.result = false;
                    //            return Json(new
                    //            {
                    //                jsonParam
                    //            }, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                    //if (Convert.IsDBNull(MInMAxDate.Rows[0]["MAX_CSN_TO"]) == false)
                    //{
                    //    if (model.EndDate_createForm != null)
                    //    {
                    //        DateTime InstanceMaxDate = (Convert.ToDateTime(MInMAxDate.Rows[0]["MAX_CSN_TO"])).Date;
                    //        if (model.EndDate_createForm < InstanceMaxDate)
                    //        {
                    //            jsonParam.message = "Form End Date Cannot Be Less Than To Date Of Last Instance. Date: <b>" + InstanceMaxDate.ToString("dd-MM-yyyy") + "</b>";
                    //            jsonParam.title = "Information";
                    //            jsonParam.focusid = "EndDate_createForm";
                    //            jsonParam.result = false;
                    //            return Json(new
                    //            {
                    //                jsonParam
                    //            }, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                    //DataTable instances = BASE._Form_dbops.GetAllChartInstance(model.FormID);
                    //if (instances != null)
                    //{
                    //    model.CreatedInstanceCount_createForm = instances.Rows.Count;
                    //}
                    //if (model.CreatedInstanceCount_createForm > 1) //multiple instances created
                    //{
                    //    if (model.Frequency_createForm == "Once")
                    //    {
                    //        jsonParam.message = "Multiple Instances Has Already Been Created For The Form<br> Cannot Change The Frequency To Once";
                    //        jsonParam.title = "Information";
                    //        jsonParam.focusid = "Frequency_createForm";
                    //        jsonParam.result = false;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);                
                    //    }
                    //    if (model.StartDate_createForm != model.PrevStartDate_createForm) // start date change
                    //    {
                    //        jsonParam.message = "Multiple Instances Has Already Been Created For The Form<br> Cannot Change The Start Date Of the Form";
                    //        jsonParam.title = "Information";
                    //        jsonParam.focusid = "Frequency_createForm";
                    //        jsonParam.result = false;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //    if (model.EndDate_createForm != null)
                    //    {
                    //        DateTime InstanceMaxDate = (Convert.ToDateTime(MInMAxDate.Rows[0]["MAX_CSN_TO"])).Date;
                    //        if (model.EndDate_createForm < InstanceMaxDate)
                    //        {
                    //            jsonParam.message = "Multiple Instances Has Already Been Created For The Form<br>Form End Date Cannot Be Less Than To Date Of Last Instance. Date: <b>" + InstanceMaxDate.ToString("dd-MM-yyyy") + "</b>";
                    //            jsonParam.title = "Information";
                    //            jsonParam.focusid = "EndDate_createForm";
                    //            jsonParam.result = false;
                    //            return Json(new
                    //            {
                    //                jsonParam
                    //            }, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                }
                if (model.Tag == "_New")
                {
                    //insert form
                    Param_Insert_Form Inparam = new Param_Insert_Form();

                    // filling form settings and form images
                    Param_Form_Master inMaster = new Param_Form_Master();
                    inMaster.Title = model.FormTitle_createForm.ReplacePwithDivTags();
                    inMaster.Description = model.FormDescription_createForm.ReplacePwithDivTags();
                    inMaster.LoginRequired = model.FormLoginRequired_createForm;
                    inMaster.Project_ID = model.ProjectID_createForm;
                    inMaster.Frequency = model.Frequency_createForm;
                    inMaster.Purpose = model.Purpose_createForm;
                    inMaster.Approval_Required = model.ApprovalReq_createForm;
                    inMaster.Confirmation_Message = model.ConfirmMsg_createForm.ReplacePwithDivTags();
                    inMaster.Reg_No_Format = string.Concat(model.RegNoPrefix_createForm ?? "", model.RegNo_createForm, model.RegNoSuffix_createForm ?? "");
                    inMaster.EventID = model.Event_ID;
                    inMaster.ServiceReportID = model.SR_ID;
                    inMaster.AllowResubmission = model.AllowResubmission_createForm;
                    if (model.CustomSchedule_createForm != null)
                    {
                        inMaster.CustomScheduleID = model.CustomSchedule_createForm.ToString();
                    }
                    inMaster.NotificationOnInstanceCreation = model.NotificationOnInstanceCreation_createForm;
                    inMaster.ApprovalMsg = model.ApprovalMsg_createForm.ReplacePwithDivTags();
                    inMaster.RejectionMsg = model.RejectionMsg_createForm.ReplacePwithDivTags();    
                    inMaster.Generate_Reg_No = model.GenerateReg_createForm;
                    inMaster.Generate_Reg_No_Approval = model.GenerateRegApproval_createForm;
                    inMaster.Name = model.FormName_createForm;
                    inMaster.PreRequiredSrnNo = model.PreRequiredChartSrID_createForm;
                    if (CommonFunctions.IsDate(model.StartDate_createForm))
                    {
                        inMaster.StartDate = Convert.ToDateTime(model.StartDate_createForm).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.EndDate_createForm))
                    {
                        inMaster.EndDate = (Convert.ToDateTime(model.EndDate_createForm)/*.Add(new TimeSpan(23,59,59))*/).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.ActiveFrom_createForm))
                    {
                        inMaster.ActiveFrom = Convert.ToDateTime(model.ActiveFrom_createForm).ToLongTimeString();
                    }
                    if (CommonFunctions.IsDate(model.ActiveTo_createForm))
                    {
                        inMaster.ActiveTo = Convert.ToDateTime(model.ActiveTo_createForm).ToLongTimeString();
                    }
                    inMaster.StartDateMsg = model.StartDateMsg_createForm.ReplacePwithDivTags();
                    inMaster.EndDateMsg = model.EndDateMsg_createForm.ReplacePwithDivTags();
                    inMaster.DisplayMode = model.DisplayMode_createForm;
                    inMaster.FormBgColor = model.FormBgColor_createForm;
                    inMaster.QuestionBgColor = model.QuestionBgColor_createForm;
                    inMaster.QuestionFgColor = model.QuestionFgColor_createForm;
                    inMaster.QuestionFontsize = model.QuestionFontsize_createForm;
                    inMaster.MaxEntries = model.MaxEntriesAllowed_createForm;
                    inMaster.MaxGroupRegistrations = model.Max_GroupRegistrations_Allowed_createForm;
                    inMaster.FormBgImagePath = model.FormBgImage_createForm;
                    inMaster.GrpTitleBgColor = model.GroupTitleBgColor_createForm;
                    inMaster.GrpTitleFgColor = model.GroupTitleFgColor_createForm;
                    inMaster.GrpTitleFontsize = model.GroupTitleFontsize_createForm;
                    if (model.FormImageFile_createForm != null && model.FormImageFile_createForm.ContentLength > 0) //form header image
                    {
                        BinaryReader reader = new BinaryReader(model.FormImageFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormImageFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormImageFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormImageFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        inMaster.File_Name = FileName;
                        inMaster.File = imageBytes;
                        inMaster.ImageWidth = 1180;
                        inMaster.ImageHeight = 365;
                    }
                    if (model.FormLoginFile_createForm != null && model.FormLoginFile_createForm.ContentLength > 0) //form login image
                    {
                        BinaryReader reader = new BinaryReader(model.FormLoginFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormLoginFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormLoginFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormLoginFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        inMaster.Login_File_Name = FileName;
                        inMaster.Login_File = imageBytes;
                    }
                    if (model.FormMobileFile_createForm != null && model.FormMobileFile_createForm.ContentLength > 0) //form login responsive image
                    {
                        BinaryReader reader = new BinaryReader(model.FormMobileFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormMobileFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormMobileFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormMobileFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        inMaster.Responsive_File_Name = FileName;
                        inMaster.Responsive_File = imageBytes;
                    }
                    if (model.FormThumbnailFile_createForm != null && model.FormThumbnailFile_createForm.ContentLength > 0) //form thumbnail image
                    {
                        BinaryReader reader = new BinaryReader(model.FormThumbnailFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormThumbnailFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormThumbnailFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormThumbnailFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        inMaster.Thumbnail_File_Name = FileName;
                        inMaster.Thumbnail_File = imageBytes;
                    }
                    Inparam.Master = inMaster;
                    Param_Form_Notification InsertFormNotification = new Param_Form_Notification();
                    if (model.IsEmailNotification_createForm || model.IsWhatsappNotification_createForm || model.IsSMSNotification_createForm)
                    {
                        InsertFormNotification.Approval_Required = model.ApprovalReq_createForm;
                        InsertFormNotification.Name = model.FormName_createForm;
                        InsertFormNotification.FormInstanceId = model.FormInstanceID;

                        InsertFormNotification.IsEmailNotificationSelected = model.IsEmailNotification_createForm;
                        InsertFormNotification.IsEmailConfirmSelected = model.IsEmailConfirmEnabled_createForm;
                        InsertFormNotification.IsEmailApproveSelected = model.IsEmailAcceptEnabled_createForm;
                        InsertFormNotification.IsEmailRejectSelected = model.IsEmailRejectEnabled_createForm;
                        InsertFormNotification.ConfirmationEmail_Message = model.ConfirmMsg_createForm.ReplacePwithDivTags();
                        InsertFormNotification.ApprovalEmail_Message = model.ApprovalMsg_createForm.ReplacePwithDivTags();
                        InsertFormNotification.RejectionEmail_Message = model.RejectionMsg_createForm.ReplacePwithDivTags();

                        InsertFormNotification.IsWhatsappNotificationSelected = model.IsWhatsappNotification_createForm;
                        InsertFormNotification.IsWhatsAppConfirmSelected = model.IsWhatsappConfirmEnabled_createForm;
                        InsertFormNotification.IsWhatsAppApproveSelected = model.IsWhatsappAcceptEnabled_createForm;
                        InsertFormNotification.IsWhatsAppRejectSelected = model.IsWhatsappRejectEnabled_createForm;
                        //InsertFormNotification.ConfirmationWhatsApp_Message = model.ConfirmWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        InsertFormNotification.ConfirmationWhatsApp_Message = model.ConfirmWhatsapp_createForm;
                        InsertFormNotification.ConfirmationWhatsApp_Message_Template = model.Template_DD_ConfirmWhatsapp_TemplatePreview_CreateForm +'~'+model.ConfirmWhatsapp_Template_Language_createForm;
                        //InsertFormNotification.ApprovalWhatsApp_Message = model.ApprovalWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        InsertFormNotification.ApprovalWhatsApp_Message = model.ApprovalWhatsapp_createForm;
                        InsertFormNotification.ApprovalWhatsApp_Message_Template = model.Template_DD_ApprovalWhatsapp_TemplatePreview_CreateForm + '~' + model.ApprovalWhatsapp_Template_Language_createForm;
                        //InsertFormNotification.RejectionWhatsApp_Message = model.RejectionWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        InsertFormNotification.RejectionWhatsApp_Message = model.RejectionWhatsapp_createForm;
                        InsertFormNotification.RejectionWhatsApp_Message_Template = model.Template_DD_RejectWhatsapp_TemplatePreview_CreateForm + '~' + model.RejectionWhatsapp_Template_Language_createForm;

                        InsertFormNotification.IsSMSNotificationSelected = model.IsSMSNotification_createForm;
                        InsertFormNotification.IsSMSConfirmSelected = model.IsSMSConfirmEnabled_createForm;
                        InsertFormNotification.IsSMSApproveSelected = model.IsSMSAcceptEnabled_createForm;
                        InsertFormNotification.IsSMSRejectSelected = model.IsSMSRejectEnabled_createForm;
                        InsertFormNotification.ConfirmationSMS_Message = model.ConfirmSMS_createForm;
                        InsertFormNotification.ApprovalSMS_Message = model.ApprovalSMS_createForm;
                        InsertFormNotification.RejectionSMS_Message = model.RejectionSMS_createForm;

                        //settings

                        InsertFormNotification.Category = model.NotificationSetting.Categories_Notification_Setting_CreateForm;
                        InsertFormNotification.AdminEmail = model.NotificationSetting.AdminEmail_Notification_Setting_CreateForm;
                        InsertFormNotification.AdminWhatsAppNo = model.NotificationSetting.AdminWhatsappNo_Notification_Setting_CreateForm;
                        if ((model.IsEmailConfirmEnabled_createForm || model.IsEmailAcceptEnabled_createForm || model.IsEmailRejectEnabled_createForm) && model.IsEmailNotification_createForm)
                        {
                            InsertFormNotification.CC = model.NotificationSetting.CC_Email_Notification_Setting_CreateForm;
                            InsertFormNotification.BCC = model.NotificationSetting.BCC_Email_Notification_Setting_CreateForm;
                            InsertFormNotification.ReplyToEmail = model.NotificationSetting.ReplyToEmail_Email_Notification_Setting_CreateForm;
                            InsertFormNotification.SenderEmailType = model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm;
                            if (model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm.Equals("Private"))
                            {
                                InsertFormNotification.Email = model.NotificationSetting.Email_Email_Notification_Setting_CreateForm;
                                InsertFormNotification.Password = model.NotificationSetting.Password_Email_Notification_Setting_CreateForm;
                            }
                            else if (model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm.Equals("General"))
                            {
                                InsertFormNotification.Email = ConfigurationManager.AppSettings["SenderId"];
                                InsertFormNotification.Password = ConfigurationManager.AppSettings["senderPassword"];
                            }
                            //InsertFormNotification.Mode = "Email";
                        }
                        if ((model.IsWhatsappConfirmEnabled_createForm || model.IsWhatsappAcceptEnabled_createForm || model.IsWhatsappRejectEnabled_createForm) && model.IsWhatsappNotification_createForm)
                        {
                            InsertFormNotification.DeliverySpeed = model.NotificationSetting.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm;
                            InsertFormNotification.SenderWhatsappNoType = model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm;
                            if (model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm.Equals("Private"))
                            {                               
                                InsertFormNotification.Whatsappno = model.NotificationSetting.SenderWhatsapp_Notification_Setting_CreateForm;
                            }
                            else if (model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm.Equals("General"))
                            {
                                InsertFormNotification.Whatsappno = ConfigurationManager.AppSettings["DefaultWhatsAppSender"];
                            }
                            InsertFormNotification.DeliverySpeed = model.NotificationSetting.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm;
                            //InsertFormNotification.Mode = "Whatsapp";
                        }
                    }
                    else
                    {
                        InsertFormNotification = null;
                    }
                    Inparam.NotificationSettings = InsertFormNotification;
                    // filling form user profile settings
                    List<Param_Form_ProfileSettings> inUser = new List<Param_Form_ProfileSettings>();
                    for (int i = 0; i < model.ProfileSettings.Count; i++)
                    {
                        Param_Form_ProfileSettings profileSetting = new Param_Form_ProfileSettings();
                        profileSetting.Field = model.ProfileSettings[i].Field;
                        profileSetting.Visible = model.ProfileSettings[i].Visible;
                        profileSetting.Enable = model.ProfileSettings[i].Enable;
                        profileSetting.Mandatory = model.ProfileSettings[i].Mandatory;
                        inUser.Add(profileSetting);
                    }
                    Inparam.ProfileSettings = inUser.ToArray();
                    List<Param_Form_ProfileSettings> inGroupUserProfile = new List<Param_Form_ProfileSettings>();
                    for (int i = 0; i < model.GroupProfileSettings.Count; i++)
                    {
                        Param_Form_ProfileSettings profileSetting = new Param_Form_ProfileSettings();
                        profileSetting.Field = model.GroupProfileSettings[i].Field;
                        profileSetting.Visible = model.GroupProfileSettings[i].Visible;
                        profileSetting.Enable = model.GroupProfileSettings[i].Enable;
                        profileSetting.Mandatory = model.GroupProfileSettings[i].Mandatory;
                        inGroupUserProfile.Add(profileSetting);
                    }
                    Inparam.GroupProfileSettings = inGroupUserProfile.ToArray();

                    //filling questions and options               
                    List<Param_Form_Questions> inQuestions = new List<Param_Form_Questions>();
                    for (int i = 0; i < QuesAndOptionsData.Count; i++)
                    {
                        int SecNo = QuesAndOptionsData[i].secno;
                        FormQuestions questionData = model.Section[SecNo];
                        string Mode = questionData.Mode;
                        if (string.IsNullOrWhiteSpace(questionData.Question))
                        {
                            jsonParam.message = "Question Cannot Be Blank";
                            jsonParam.title = "Information";
                            jsonParam.focusid = "Section[" + SecNo + "].Question";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrWhiteSpace(Mode))
                        {
                            jsonParam.message = "Mode Cannot Be Blank";
                            jsonParam.title = "Information";
                            jsonParam.focusid = "Section[" + SecNo + "].Mode";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        Param_Form_Questions quest = new Param_Form_Questions();
                        quest.Question = questionData.Question.Trim();
                        quest.Mode = Mode;
                        quest.Required = questionData.Required;
                        quest.SrNo = i + 1;
                        quest.RowNo = questionData.RowNo;
                        quest.ColumnSpan = questionData.ColumnSpan;
                        quest.GroupName = questionData.GroupHeading;
                        quest.Description = questionData.Description;
                        //quest.DefaultValue= questionData.Options[0].Options;
                        quest.DefaultVisibility = questionData.DefaultVisibility;
                        quest.Tag = questionData.Tag;
                        if (Mode == "Short Answer" || Mode == "Paragraph")
                        {
                            quest.Max = questionData.MaxLength;
                        }
                        else if (Mode == "Number")
                        {
                            quest.Type = questionData.Type;
                            quest.Min = questionData.Min;
                            quest.Max = questionData.Max;
                        }
                        else if (Mode == "Slider")
                        {
                            //quest.Type = questionData.Type;
                            quest.Min = questionData.Min;
                            quest.Max = questionData.Max;
                        }
                        else if (Mode == "Date")
                        {
                            if (questionData.MaxDateTime != null)
                            {
                                quest.Max = questionData.MaxDateTime.Value.Ticks;
                            }
                            if (questionData.MinDateTime != null)
                            {
                                quest.Min = questionData.MinDateTime.Value.Ticks;
                            }
                        }
                        else if (Mode == "Time")
                        {
                            if (questionData.MaxDateTime != null)
                            {
                                quest.Max = questionData.MaxDateTime.Value.Ticks;
                            }
                            if (questionData.MinDateTime != null)
                            {
                                quest.Min = questionData.MinDateTime.Value.Ticks;
                            }
                        }
                        else if (Mode == "Attachment")
                        {
                            quest.Type = questionData.Type;
                        }
                        else if (Mode == "Formula")
                        {
                            if (string.IsNullOrWhiteSpace(questionData.Options[0].Options))
                            {
                                jsonParam.message = "Formula Option Cannot be Blank";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Section[" + SecNo + "].Options[0].Options";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            quest.Formula = questionData.Options[0].Options;
                        }
                        else if (Mode == "Label")
                        {
                            if (string.IsNullOrWhiteSpace(questionData.Options[0].Options))
                            {
                                jsonParam.message = "Label Text Cannot be Blank";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Section[" + SecNo + "].Options[0].Options";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            quest.DefaultValue = questionData.Options[0].Options;
                        }
                        else if (Mode == "CheckBox" || Mode == "DropDown" || Mode == "RadioButton") //filling options
                        {
                            List<Param_Form_Question_Options> inOptions = new List<Param_Form_Question_Options>();
                            int[] options = QuesAndOptionsData[i].options;
                            for (int j = 0; j < options.Length; j++)
                            {
                                int optNo = options[j];
                                QuestionOptions OptionData = questionData.Options[optNo];
                                if (questionData.IsOptionsPredefined == false)//user defined
                                {
                                    if (string.IsNullOrWhiteSpace(OptionData.Options))
                                    {
                                        jsonParam.message = "Answer Option Cannot be Blank";
                                        jsonParam.title = "Information";
                                        jsonParam.focusid = "Section[" + SecNo + "].Options[" + optNo + "].Options";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                if (string.IsNullOrWhiteSpace(OptionData.Options) == false)
                                {
                                    Param_Form_Question_Options opt = new Param_Form_Question_Options();
                                    opt.Options = OptionData.Options;
                                    opt.OptionSrNo = j + 1;
                                    if (string.IsNullOrWhiteSpace(OptionData.DependentQuestion) == false)
                                    {
                                        string[] DependentQuestion_split = OptionData.DependentQuestion.Split(',');
                                        string DependentQuestion = "";
                                        for (int k = 0; k < DependentQuestion_split.Count(); k++)
                                        {
                                            int index = QuesAndOptionsData.FindIndex(x => x.secno == Convert.ToInt32(DependentQuestion_split[k]));
                                            if (index > -1)
                                            {
                                                if (DependentQuestion.Length == 0)
                                                {
                                                    DependentQuestion = DependentQuestion + (index + 1);
                                                }
                                                else
                                                {
                                                    DependentQuestion = DependentQuestion + "," + (index + 1);
                                                }
                                            }
                                        }
                                        opt.Dependent_Questions = DependentQuestion;
                                        opt.Dependent_Questions_Visibility = string.IsNullOrWhiteSpace(OptionData.DependentQuestion) ? (bool?)null : OptionData.DependentQuestionVisibility == "Hide" ? false : true;
                                    }
                                    opt.Points = OptionData.Points;
                                    HttpPostedFileBase Optionimage = OptionData.OptionImageFile;
                                    if (Optionimage != null && Optionimage.ContentLength > 0)//option attachment
                                    {
                                        BinaryReader reader = new BinaryReader(Optionimage.InputStream);
                                        byte[] imageBytes = reader.ReadBytes((int)Optionimage.ContentLength);
                                        reader.Close();
                                        reader.Dispose();
                                        string FileName = CommonFunctions.TransformFileName(Optionimage.FileName, imageBytes, true);
                                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                                        if (!regex.IsMatch(FileName))
                                        {
                                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + Optionimage.FileName;
                                            jsonParam.title = "Information";
                                            jsonParam.focusid = "Section[" + SecNo + "].Options[" + optNo + "].Options";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                        opt.File_Name = FileName;
                                        opt.File = imageBytes;
                                    }
                                    inOptions.Add(opt);
                                }
                            }
                            quest.Options = inOptions.ToArray();
                            if (questionData.IsOptionsPredefined == true)
                            {
                                if (string.IsNullOrWhiteSpace(questionData.OptionPredefinedMiscID))
                                {
                                    jsonParam.message = "Pre Defined Option Type Is Mandatory";
                                    jsonParam.title = "Information";
                                    jsonParam.focusid = "Section[" + SecNo + "].OptionPredefinedMiscID";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                quest.Type = questionData.OptionPredefinedMiscID;
                            }
                        }
                        HttpPostedFileBase image = questionData.QuestionImageFile;  //question attachment
                        if (image != null && image.ContentLength > 0)
                        {
                            BinaryReader reader = new BinaryReader(image.InputStream);
                            byte[] imageBytes = reader.ReadBytes((int)image.ContentLength);
                            reader.Close();
                            reader.Dispose();
                            string FileName = CommonFunctions.TransformFileName(image.FileName, imageBytes, true);
                            var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                            if (!regex.IsMatch(FileName))
                            {
                                jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + image.FileName;
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Question_" + SecNo + "_createForm";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            quest.File_Name = FileName;
                            quest.File = imageBytes;
                        }
                        if (string.IsNullOrWhiteSpace(quest.File_Name) == false)
                        {
                            if (questionData.QuestionImageWidth <= 0)
                            {
                                jsonParam.message = "Question Image Width Cannot Be Zero";
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                jsonParam.focusid = "Section[" + SecNo + "].QuestionImageWidth";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (questionData.QuestionImageHeight <= 0)
                            {
                                jsonParam.message = "Question Image Height Cannot Be Zero";
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                jsonParam.focusid = "Section[" + SecNo + "].QuestionImageHeight";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            quest.ImageWidth = questionData.QuestionImageWidth;
                            quest.ImageHeight = questionData.QuestionImageHeight;
                        }
                        inQuestions.Add(quest);
                    }
                    Inparam.Questions = inQuestions.ToArray();
                    int FormID = BASE._Form_dbops.InsertForm_Txn(Inparam);
                    DataTable Instance = BASE._Form_dbops.GetAllChartInstance(FormID.ToString());
                    string instanceID = "";
                    if (Instance != null && Instance.Rows.Count > 0)
                    {
                        instanceID = Instance.Rows[0]["REC_ID"].ToString();
                    }
                    if (FormID > 0)
                    {
                        string message = "Form Created Successfully";
                        jsonParam.message = message + "<br><b>Form Link:</b><br>" + LinkForResponse(instanceID);
                        jsonParam.title = "Information";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.Tag == "_Edit")
                {
                    //Update form
                    Param_Update_Form UpParam = new Param_Update_Form();

                    // filling form settings and form images
                    Param_Form_Master UpMaster = new Param_Form_Master();
                    UpMaster.Title = model.FormTitle_createForm.ReplacePwithDivTags();
                    UpMaster.Description = model.FormDescription_createForm.ReplacePwithDivTags();
                    UpMaster.LoginRequired = model.FormLoginRequired_createForm;
                    UpMaster.Project_ID = model.ProjectID_createForm;
                    UpMaster.Frequency = model.Frequency_createForm;
                    UpMaster.Purpose = model.Purpose_createForm;
                    UpMaster.Approval_Required = model.ApprovalReq_createForm;
                    UpMaster.Reg_No_Format = string.Concat(model.RegNoPrefix_createForm ?? "", model.RegNo_createForm, model.RegNoSuffix_createForm ?? "");
                    UpMaster.EventID = model.Event_ID;
                    UpMaster.ServiceReportID = model.SR_ID;
                    UpMaster.AllowResubmission = model.AllowResubmission_createForm;
                    if (model.CustomSchedule_createForm != null)
                    {
                        UpMaster.CustomScheduleID = model.CustomSchedule_createForm.ToString();
                    }
                    UpMaster.NotificationOnInstanceCreation = model.NotificationOnInstanceCreation_createForm;
                    UpMaster.Confirmation_Message = model.ConfirmMsg_createForm.ReplacePwithDivTags();
                    UpMaster.ApprovalMsg = model.ApprovalMsg_createForm.ReplacePwithDivTags();
                    UpMaster.RejectionMsg = model.RejectionMsg_createForm.ReplacePwithDivTags();
                          
                    UpMaster.FormInstanceId = Int32.Parse(model.FormInstanceID);
                    UpMaster.Generate_Reg_No = model.GenerateReg_createForm;
                    UpMaster.Generate_Reg_No_Approval = model.GenerateRegApproval_createForm;
                    UpMaster.Name = model.FormName_createForm;
                    UpMaster.PreRequiredSrnNo = model.PreRequiredChartSrID_createForm;
                    if (CommonFunctions.IsDate(model.StartDate_createForm))
                    {
                        UpMaster.StartDate = Convert.ToDateTime(model.StartDate_createForm).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.EndDate_createForm))
                    {
                        UpMaster.EndDate = (Convert.ToDateTime(model.EndDate_createForm)/*.Add(new TimeSpan(23, 59, 59))*/).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.ActiveFrom_createForm))
                    {
                        UpMaster.ActiveFrom = Convert.ToDateTime(model.ActiveFrom_createForm).ToLongTimeString();
                    }
                    if (CommonFunctions.IsDate(model.ActiveTo_createForm))
                    {
                        UpMaster.ActiveTo = Convert.ToDateTime(model.ActiveTo_createForm).ToLongTimeString();
                    }
                    UpMaster.StartDateMsg = model.StartDateMsg_createForm.ReplacePwithDivTags();
                    UpMaster.EndDateMsg = model.EndDateMsg_createForm.ReplacePwithDivTags();
                    UpMaster.DisplayMode = model.DisplayMode_createForm;
                    UpMaster.FormBgColor = model.FormBgColor_createForm;
                    UpMaster.QuestionBgColor = model.QuestionBgColor_createForm;
                    UpMaster.QuestionFgColor = model.QuestionFgColor_createForm;
                    UpMaster.QuestionFontsize = model.QuestionFontsize_createForm;
                    if (model.FormImageFile_createForm != null && model.FormImageFile_createForm.ContentLength > 0) //form header image
                    {
                        BinaryReader reader = new BinaryReader(model.FormImageFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormImageFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormImageFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormImageFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        UpMaster.File_Name = FileName;
                        UpMaster.File = imageBytes;
                        UpMaster.ImageWidth = 1180;
                        UpMaster.ImageHeight = 365;
                    }
                    else if (string.IsNullOrWhiteSpace(model.FormImageFileName) == false)
                    {
                        UpMaster.File_Name = model.FormImageFileName;
                        UpMaster.ImageWidth = 1180;
                        UpMaster.ImageHeight = 365;
                    }
                    if (model.FormLoginFile_createForm != null && model.FormLoginFile_createForm.ContentLength > 0) //form login image
                    {
                        BinaryReader reader = new BinaryReader(model.FormLoginFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormLoginFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormLoginFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormLoginFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        UpMaster.Login_File_Name = FileName;
                        UpMaster.Login_File = imageBytes;
                    }
                    else if (string.IsNullOrWhiteSpace(model.LoginImageFileName) == false)
                    {
                        UpMaster.Login_File_Name = model.LoginImageFileName;
                    }
                    if (model.FormMobileFile_createForm != null && model.FormMobileFile_createForm.ContentLength > 0) //form login responsive image
                    {
                        BinaryReader reader = new BinaryReader(model.FormMobileFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormMobileFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormMobileFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormMobileFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        UpMaster.Responsive_File_Name = FileName;
                        UpMaster.Responsive_File = imageBytes;
                    }
                    else if (string.IsNullOrWhiteSpace(model.ResponsiveImageFileName) == false)
                    {
                        UpMaster.Responsive_File_Name = model.ResponsiveImageFileName;
                    }
                    if (model.FormThumbnailFile_createForm != null && model.FormThumbnailFile_createForm.ContentLength > 0) //form thumbnail image
                    {
                        BinaryReader reader = new BinaryReader(model.FormThumbnailFile_createForm.InputStream);
                        byte[] imageBytes = reader.ReadBytes(model.FormThumbnailFile_createForm.ContentLength);
                        reader.Close();
                        reader.Dispose();
                        string FileName = CommonFunctions.TransformFileName(model.FormThumbnailFile_createForm.FileName, imageBytes, true);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(FileName))
                        {
                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + model.FormThumbnailFile_createForm.FileName;
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        UpMaster.Thumbnail_File_Name = FileName;
                        UpMaster.Thumbnail_File = imageBytes;
                    }
                    else if (string.IsNullOrWhiteSpace(model.ThumbsImageFileName) == false)
                    {
                        UpMaster.Thumbnail_File_Name = model.ThumbsImageFileName;
                    }
                    UpMaster.MaxEntries = model.MaxEntriesAllowed_createForm;
                    UpMaster.MaxGroupRegistrations = model.Max_GroupRegistrations_Allowed_createForm;
                    UpMaster.Rec_ID = model.FormID;
                    UpMaster.StartDateChange = model.StartDateChange_createForm;
                    UpMaster.FormBgImagePath = model.FormBgImage_createForm;
                    UpMaster.GrpTitleBgColor = model.GroupTitleBgColor_createForm;
                    UpMaster.GrpTitleFgColor = model.GroupTitleFgColor_createForm;
                    UpMaster.GrpTitleFontsize = model.GroupTitleFontsize_createForm;
                    UpParam.Master = UpMaster;

                    Param_Form_Notification UpdateFormNotification = new Param_Form_Notification();
                    if (model.IsEmailNotification_createForm || model.IsWhatsappNotification_createForm || model.IsSMSNotification_createForm)
                    {
                        UpdateFormNotification.Approval_Required = model.ApprovalReq_createForm;
                        UpdateFormNotification.Name = model.FormName_createForm;
                        UpdateFormNotification.FormInstanceId = model.FormInstanceID;

                        UpdateFormNotification.IsEmailNotificationSelected = model.IsEmailNotification_createForm;
                        UpdateFormNotification.IsEmailConfirmSelected = model.IsEmailConfirmEnabled_createForm;
                        UpdateFormNotification.IsEmailApproveSelected = model.IsEmailAcceptEnabled_createForm;
                        UpdateFormNotification.IsEmailRejectSelected = model.IsEmailRejectEnabled_createForm;
                        UpdateFormNotification.ConfirmationEmail_Message = model.ConfirmMsg_createForm.ReplacePwithDivTags();
                        UpdateFormNotification.ApprovalEmail_Message = model.ApprovalMsg_createForm.ReplacePwithDivTags();
                        UpdateFormNotification.RejectionEmail_Message = model.RejectionMsg_createForm.ReplacePwithDivTags();

                        UpdateFormNotification.IsWhatsappNotificationSelected = model.IsWhatsappNotification_createForm;
                        UpdateFormNotification.IsWhatsAppConfirmSelected = model.IsWhatsappConfirmEnabled_createForm;
                        UpdateFormNotification.IsWhatsAppApproveSelected = model.IsWhatsappAcceptEnabled_createForm;
                        UpdateFormNotification.IsWhatsAppRejectSelected = model.IsWhatsappRejectEnabled_createForm;
                        //UpdateFormNotification.ConfirmationWhatsApp_Message = model.ConfirmWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        UpdateFormNotification.ConfirmationWhatsApp_Message = model.ConfirmWhatsapp_createForm;
                        UpdateFormNotification.ConfirmationWhatsApp_Message_Template = model.Template_DD_ConfirmWhatsapp_TemplatePreview_CreateForm + '~' + model.ConfirmWhatsapp_Template_Language_createForm;

                        //UpdateFormNotification.ApprovalWhatsApp_Message = model.ApprovalWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        UpdateFormNotification.ApprovalWhatsApp_Message = model.ApprovalWhatsapp_createForm;
                        UpdateFormNotification.ApprovalWhatsApp_Message_Template = model.Template_DD_ApprovalWhatsapp_TemplatePreview_CreateForm + '~' + model.ApprovalWhatsapp_Template_Language_createForm;

                        //UpdateFormNotification.RejectionWhatsApp_Message = model.RejectionWhatsapp_createForm.ConvertHtmlToWhatsappText();
                        UpdateFormNotification.RejectionWhatsApp_Message = model.RejectionWhatsapp_createForm;
                        UpdateFormNotification.RejectionWhatsApp_Message_Template = model.Template_DD_RejectWhatsapp_TemplatePreview_CreateForm + '~' + model.RejectionWhatsapp_Template_Language_createForm;

                        UpdateFormNotification.IsSMSNotificationSelected = model.IsSMSNotification_createForm;
                        UpdateFormNotification.IsSMSConfirmSelected = model.IsSMSConfirmEnabled_createForm;
                        UpdateFormNotification.IsSMSApproveSelected = model.IsSMSAcceptEnabled_createForm;
                        UpdateFormNotification.IsSMSRejectSelected = model.IsSMSRejectEnabled_createForm;
                        UpdateFormNotification.ConfirmationSMS_Message = model.ConfirmSMS_createForm;
                        UpdateFormNotification.ApprovalSMS_Message = model.ApprovalSMS_createForm;
                        UpdateFormNotification.RejectionSMS_Message = model.RejectionSMS_createForm;

                        //settings
                        UpdateFormNotification.Category = model.NotificationSetting.Categories_Notification_Setting_CreateForm;
                        UpdateFormNotification.AdminEmail = model.NotificationSetting.AdminEmail_Notification_Setting_CreateForm;
                        UpdateFormNotification.AdminWhatsAppNo = model.NotificationSetting.AdminWhatsappNo_Notification_Setting_CreateForm;
                        if (model.IsEmailNotification_createForm)
                        {
                            UpdateFormNotification.CC = model.NotificationSetting.CC_Email_Notification_Setting_CreateForm;
                            UpdateFormNotification.BCC = model.NotificationSetting.BCC_Email_Notification_Setting_CreateForm;
                            UpdateFormNotification.ReplyToEmail = model.NotificationSetting.ReplyToEmail_Email_Notification_Setting_CreateForm;
                            UpdateFormNotification.SenderEmailType = model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm;
                            if (model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm.Equals("Private"))
                            {
                                UpdateFormNotification.Email = model.NotificationSetting.Email_Email_Notification_Setting_CreateForm;
                                UpdateFormNotification.Password = model.NotificationSetting.Password_Email_Notification_Setting_CreateForm;
                            }
                            else if (model.NotificationSetting.Radio_SenderEmail_Notification_Setting_CreateForm.Equals("General"))
                            {
                                UpdateFormNotification.Email = ConfigurationManager.AppSettings["SenderId"];
                                UpdateFormNotification.Password = ConfigurationManager.AppSettings["senderPassword"];
                            }
                            //UpdateFormNotification.Mode = "Email";
                        }
                        if (model.IsWhatsappNotification_createForm)
                        {
                            UpdateFormNotification.DeliverySpeed = model.NotificationSetting.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm;
                            UpdateFormNotification.SenderWhatsappNoType = model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm;
                            if (model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm.Equals("Private"))
                            {
                                UpdateFormNotification.Whatsappno = model.NotificationSetting.SenderWhatsapp_Notification_Setting_CreateForm;
                            }
                            else if (model.NotificationSetting.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm.Equals("General"))
                            {
                                UpdateFormNotification.Whatsappno = ConfigurationManager.AppSettings["DefaultWhatsAppSender"];
                            }
                            UpdateFormNotification.DeliverySpeed = model.NotificationSetting.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm;
                            //UpdateFormNotification.Mode = "Whatsapp";
                        }
                    }
                    else
                    {
                        UpdateFormNotification = null;
                    }         
                    UpParam.NotificationSettings = UpdateFormNotification;

                    // filling form user profile settings
                    List<Param_Form_ProfileSettings> UpUserSetting = new List<Param_Form_ProfileSettings>();
                    for (int i = 0; i < model.ProfileSettings.Count; i++)
                    {
                        Param_Form_ProfileSettings profileSetting = new Param_Form_ProfileSettings();
                        profileSetting.Field = model.ProfileSettings[i].Field;
                        profileSetting.Visible = model.ProfileSettings[i].Visible;
                        profileSetting.Enable = model.ProfileSettings[i].Enable;
                        profileSetting.Mandatory = model.ProfileSettings[i].Mandatory;
                        profileSetting.Rec_ID = model.ProfileSettings[i].Rec_ID;
                        profileSetting.Form_ID = model.FormID;
                        UpUserSetting.Add(profileSetting);
                    }
                    UpParam.ProfileSettings = UpUserSetting.ToArray();
                    List<Param_Form_ProfileSettings> UpGroupUserSetting = new List<Param_Form_ProfileSettings>();
                    for (int i = 0; i < model.GroupProfileSettings.Count; i++)
                    {
                        Param_Form_ProfileSettings profileSetting = new Param_Form_ProfileSettings();
                        profileSetting.Field = model.GroupProfileSettings[i].Field;
                        profileSetting.Visible = model.GroupProfileSettings[i].Visible;
                        profileSetting.Enable = model.GroupProfileSettings[i].Enable;
                        profileSetting.Mandatory = model.GroupProfileSettings[i].Mandatory;
                        profileSetting.Rec_ID = model.GroupProfileSettings[i].Rec_ID;
                        profileSetting.Form_ID = model.FormID;
                        UpGroupUserSetting.Add(profileSetting);
                    }
                    UpParam.GroupProfileSettings = UpGroupUserSetting.ToArray();

                    //filling questions and options               
                    List<Param_Form_Questions> UpQuestions = new List<Param_Form_Questions>();
                    for (int i = 0; i < QuesAndOptionsData.Count; i++)
                    {
                        int SecNo = QuesAndOptionsData[i].secno;
                        FormQuestions questionData = model.Section[SecNo];
                        string Mode = questionData.Mode;
                        if (string.IsNullOrWhiteSpace(questionData.Question))
                        {
                            jsonParam.message = "Question Cannot Be Blank";
                            jsonParam.title = "Information";
                            jsonParam.focusid = "Section[" + SecNo + "].Question";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrWhiteSpace(Mode))
                        {
                            jsonParam.message = "Mode Cannot Be Blank";
                            jsonParam.title = "Information";
                            jsonParam.focusid = "Section[" + SecNo + "].Mode";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        Param_Form_Questions UPquest = new Param_Form_Questions();
                        UPquest.Question = questionData.Question.Trim();
                        UPquest.Mode = Mode;
                        UPquest.Required = questionData.Required;
                        UPquest.SrNo = i + 1;
                        UPquest.Rec_ID = questionData.Rec_ID;
                        UPquest.Form_ID = model.FormID;
                        UPquest.RowNo = questionData.RowNo;
                        UPquest.ColumnSpan = questionData.ColumnSpan;
                        UPquest.GroupName = questionData.GroupHeading;
                        UPquest.Description = questionData.Description;
                        UPquest.DefaultVisibility = questionData.DefaultVisibility;
                        UPquest.Tag = questionData.Tag;
                        if (Mode == "Short Answer" || Mode == "Paragraph")
                        {
                            UPquest.Max = questionData.MaxLength;
                        }
                        else if (Mode == "Number")
                        {
                            UPquest.Type = questionData.Type;
                            UPquest.Min = questionData.Min;
                            UPquest.Max = questionData.Max;
                        }
                        else if (Mode == "Slider")
                        {
                            //UPquest.Type = questionData.Type;
                            UPquest.Min = questionData.Min;
                            UPquest.Max = questionData.Max;
                        }
                        else if (Mode == "Date")
                        {
                            if (questionData.MaxDateTime != null)
                            {
                                UPquest.Max = questionData.MaxDateTime.Value.Ticks;
                            }
                            if (questionData.MinDateTime != null)
                            {
                                UPquest.Min = questionData.MinDateTime.Value.Ticks;
                            }
                        }
                        else if (Mode == "Time")
                        {
                            if (questionData.MaxDateTime != null)
                            {
                                UPquest.Max = questionData.MaxDateTime.Value.Ticks;
                            }
                            if (questionData.MinDateTime != null)
                            {
                                UPquest.Min = questionData.MinDateTime.Value.Ticks;
                            }
                        }
                        else if (Mode == "Attachment")
                        {
                            UPquest.Type = questionData.Type;
                        }
                        else if (Mode == "Formula")
                        {
                            if (string.IsNullOrWhiteSpace(questionData.Options[0].Options))
                            {
                                jsonParam.message = "Formula Option Cannot be Blank";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Section[" + SecNo + "].Options[0].Options";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            UPquest.Formula = questionData.Options[0].Options;
                        }
                        else if (Mode == "Label")
                        {
                            if (string.IsNullOrWhiteSpace(questionData.Options[0].Options))
                            {
                                jsonParam.message = "Label Text Cannot be Blank";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Section[" + SecNo + "].Options[0].Options";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            UPquest.DefaultValue = questionData.Options[0].Options;
                        }
                        else if (Mode == "CheckBox" || Mode == "DropDown" || Mode == "RadioButton") //filling options
                        {
                            List<Param_Form_Question_Options> UpOptions = new List<Param_Form_Question_Options>();
                            int[] options = QuesAndOptionsData[i].options;
                            for (int j = 0; j < options.Length; j++)
                            {
                                int optNo = options[j];
                                QuestionOptions OptionData = questionData.Options[optNo];
                                if (questionData.IsOptionsPredefined == false)//user defined
                                {
                                    if (string.IsNullOrWhiteSpace(OptionData.Options))
                                    {
                                        jsonParam.message = "Answer Option Cannot be Blank";
                                        jsonParam.title = "Information";
                                        jsonParam.focusid = "Section[" + SecNo + "].Options[" + optNo + "].Options";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                if (string.IsNullOrWhiteSpace(OptionData.Options) == false)
                                {
                                    Param_Form_Question_Options Upopt = new Param_Form_Question_Options();
                                    Upopt.Question_ID = questionData.Rec_ID;
                                    Upopt.Rec_ID = OptionData.Rec_ID;
                                    Upopt.Form_ID = model.FormID;
                                    Upopt.Question_ID = questionData.Rec_ID;
                                    Upopt.Options = OptionData.Options;
                                    Upopt.OptionSrNo = j + 1;
                                    if (string.IsNullOrWhiteSpace(OptionData.DependentQuestion) == false)
                                    {
                                        string[] DependentQuestion_split = OptionData.DependentQuestion.Split(',');
                                        string DependentQuestion = "";
                                        for (int k = 0; k < DependentQuestion_split.Count(); k++)
                                        {
                                            int index = QuesAndOptionsData.FindIndex(x => x.secno == Convert.ToInt32(DependentQuestion_split[k]));
                                            if (index > -1)
                                            {
                                                if (DependentQuestion.Length == 0)
                                                {
                                                    DependentQuestion = DependentQuestion + (index + 1);
                                                }
                                                else
                                                {
                                                    DependentQuestion = DependentQuestion + "," + (index + 1);
                                                }
                                            }
                                        }
                                        Upopt.Dependent_Questions = DependentQuestion;
                                        Upopt.Dependent_Questions_Visibility = string.IsNullOrWhiteSpace(OptionData.DependentQuestion) ? (bool?)null : OptionData.DependentQuestionVisibility == "Hide" ? false : true;
                                    }
                                    Upopt.Points = OptionData.Points;
                                    HttpPostedFileBase Optionimage = OptionData.OptionImageFile;
                                    if (Optionimage != null && Optionimage.ContentLength > 0)//option attachment
                                    {
                                        BinaryReader reader = new BinaryReader(Optionimage.InputStream);
                                        byte[] imageBytes = reader.ReadBytes((int)Optionimage.ContentLength);
                                        reader.Close();
                                        reader.Dispose();
                                        string FileName = CommonFunctions.TransformFileName(Optionimage.FileName, imageBytes, true);
                                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                                        if (!regex.IsMatch(FileName))
                                        {
                                            jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + Optionimage.FileName;
                                            jsonParam.title = "Information";
                                            jsonParam.focusid = "Section[" + SecNo + "].Options[" + optNo + "].Options";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                        Upopt.File_Name = FileName;
                                        Upopt.File = imageBytes;
                                    }
                                    else if (string.IsNullOrWhiteSpace(OptionData.OptionImageFileName) == false)
                                    {
                                        Upopt.File_Name = OptionData.OptionImageFileName;
                                    }
                                    UpOptions.Add(Upopt);
                                }
                            }
                            UPquest.Options = UpOptions.ToArray();
                            if (questionData.IsOptionsPredefined == true)// predefined option
                            {
                                if (string.IsNullOrWhiteSpace(questionData.OptionPredefinedMiscID))
                                {
                                    jsonParam.message = "Pre Defined Option Type Is Mandatory";
                                    jsonParam.title = "Information";
                                    jsonParam.focusid = "Section[" + SecNo + "].OptionPredefinedMiscID";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                UPquest.Type = questionData.OptionPredefinedMiscID;
                            }
                        }
                        HttpPostedFileBase image = questionData.QuestionImageFile;  //question attachment
                        if (image != null && image.ContentLength > 0)
                        {
                            BinaryReader reader = new BinaryReader(image.InputStream);
                            byte[] imageBytes = reader.ReadBytes((int)image.ContentLength);
                            reader.Close();
                            reader.Dispose();
                            string FileName = CommonFunctions.TransformFileName(image.FileName, imageBytes, true);
                            var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                            if (!regex.IsMatch(FileName))
                            {
                                jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + image.FileName;
                                jsonParam.title = "Information";
                                jsonParam.focusid = "Question_" + SecNo + "_createForm";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            UPquest.File_Name = FileName;
                            UPquest.File = imageBytes;
                        }
                        else if (string.IsNullOrWhiteSpace(questionData.QuestionImageFileName) == false)
                        {
                            UPquest.File_Name = questionData.QuestionImageFileName;
                        }
                        if (string.IsNullOrWhiteSpace(UPquest.File_Name) == false)
                        {
                            if (questionData.QuestionImageWidth <= 0)
                            {
                                jsonParam.message = "Question Image Width Cannot Be Zero";
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                jsonParam.focusid = "Section[" + SecNo + "].QuestionImageWidth";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (questionData.QuestionImageHeight <= 0)
                            {
                                jsonParam.message = "Question Image Height Cannot Be Zero";
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                jsonParam.focusid = "Section[" + SecNo + "].QuestionImageHeight";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            UPquest.ImageWidth = questionData.QuestionImageWidth;
                            UPquest.ImageHeight = questionData.QuestionImageHeight;
                        }
                        UpQuestions.Add(UPquest);
                    }
                    UpParam.Questions = UpQuestions.ToArray();
                    UpParam.DeleteOptions_RecID = Deleted_Ques_option_RecIDs[0].DeleteOptionRecID;
                    UpParam.DeleteQuestions_RecID = Deleted_Ques_option_RecIDs[0].DeleteQuestionsRecID;
                    if (BASE._Form_dbops.UpdateForm_Txn(UpParam))
                    {
                        string message = "Form Updated Successfully";
                        jsonParam.message = message + "<br><b>Form Link:</b><br>" + LinkForResponse(model.FormInstanceID);
                        jsonParam.title = "Information";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                else if (model.Tag == "_Delete")
                {
                    if ((int)BASE._Form_dbops.GetFormReponseCount(model.FormID) > 0)
                    {
                        jsonParam.message = "Form Cannot Be Deleted<br> Responses Have Been Posted";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (BASE._Form_dbops.DeleteForm_Txn(model.FormID))
                    {
                        jsonParam.message = "Form Deleted Successfully";
                        jsonParam.title = "Information";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Information";
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
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_FormResponse_Window(string Form_ID = "21")
        {

            FormResponses model = new FormResponses();
            DataTable Form_Info = BASE._Form_dbops.GetFormRecord(Form_ID);
            if (Form_Info == null || Form_Info.Rows.Count == 0)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
            }
            if (Convert.ToBoolean(Form_Info.Rows[0]["F_LOGINREQUIRED"]) == true)
            {
                if (string.IsNullOrWhiteSpace(BASE._open_User_ID) == true)
                {
                    string defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"];
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("area", defaultpath.Split('/')[1]);
                    redirectTargetDictionary.Add("action", defaultpath.Split('/')[3]);
                    redirectTargetDictionary.Add("controller", defaultpath.Split('/')[2]);
                    return new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            model.Form_Description_FormResponse = Form_Info.Rows[0]["F_DESCRIPTION"].ToString();
            model.Form_Title_FormResponse = Form_Info.Rows[0]["F_TITLE"].ToString();
            model.FormID = Form_Info.Rows[0]["REC_ID"].ToString();
            if (Form_Info.Rows[0]["F_FILE_NAME"].ToString().Length > 0)
            {
                model.FormImagePath = GetAttachmentPath(model.FormID, Form_Info.Rows[0]["F_FILE_NAME"].ToString());
                model.FormImageWidth = Convert.ToInt32(Form_Info.Rows[0]["F_IMAGE_WIDTH"]);
                model.FormImageHeight = Convert.ToInt32(Form_Info.Rows[0]["F_IMAGE_HEIGHT"]);
            }
            model.Questions = new List<FormQuestions_Response>();
            DataTable Form_Question_Info = BASE._Form_dbops.GetFormQuestions(Form_ID);
            if (Form_Question_Info == null || Form_Question_Info.Rows.Count == 0)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
            }
            DataView dv = Form_Question_Info.DefaultView;
            dv.Sort = "FQ_SRNO ASC";
            Form_Question_Info = dv.ToTable();
            foreach (DataRow d1 in Form_Question_Info.Rows)
            {
                FormQuestions_Response newQues = new FormQuestions_Response();
                newQues.Question = d1["FQ_QUESTION"].ToString();
                newQues.Mode = d1["FQ_MODE"].ToString();
                newQues.Required = Convert.ToBoolean(d1["FQ_REQUIRED"]);
                newQues.Type = d1["FQ_TYPE"].ToString();
                if (Convert.IsDBNull(d1["FQ_MIN"]))
                {
                    newQues.Min = int.MinValue;
                }
                else
                {
                    newQues.Min = Convert.ToInt32(d1["FQ_MIN"]);
                }
                if (Convert.IsDBNull(d1["FQ_MAX"]))
                {
                    newQues.Max = int.MaxValue;
                }
                else
                {
                    newQues.Max = Convert.ToInt32(d1["FQ_MAX"]);
                }
                newQues.Rec_ID = d1["REC_ID"].ToString();
                if (d1["FQ_FILE_NAME"].ToString().Length > 0)
                {
                    newQues.QuestionImagePath = GetAttachmentPath(newQues.Rec_ID, d1["FQ_FILE_NAME"].ToString());
                    newQues.QuestionImageWidth = Convert.ToInt32(d1["FQ_IMAGE_WIDTH"]);
                    newQues.QuestionImageHeight = Convert.ToInt32(d1["FQ_IMAGE_HEIGHT"]);
                }
                model.Questions.Add(newQues);
            }
            ViewBag.UserID = BASE._open_User_ID;
            return View(model);
        }
        public ActionResult LookUp_GetQuestion_Options(DataSourceLoadOptions loadOptions, string QuestionID)
        {
            var data = new List<QuestionOptions>();
            DataTable Form_Option_Info = BASE._Form_dbops.GetFormQuestion_Options(QuestionID);
            DataView dv = Form_Option_Info.DefaultView;
            dv.Sort = "FQO_SRNO ASC";
            Form_Option_Info = dv.ToTable();
            foreach (DataRow d3 in Form_Option_Info.Rows)
            {
                QuestionOptions newData = new QuestionOptions();
                newData.Options = d3["FQO_OPTION"].ToString();
                newData.Rec_ID = d3["REC_ID"].ToString();
                if (d3["FQO_FILE_NAME"].ToString().Length > 0)
                {
                    newData.optionImagePath = GetAttachmentPath(newData.Rec_ID, d3["FQO_FILE_NAME"].ToString());
                }
                data.Add(newData);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult Save_FormResponses(FormCollection collection)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (collection["AllowResubmission"] == "False")
                {
                    if (string.IsNullOrWhiteSpace(collection["UserID"]) == false)
                    {
                        DataTable Registered = BASE._Form_dbops.CheckIfUserAlreadyRegistered(collection["FormInstanceID"], collection["UserID"]);
                        if (Registered != null && Registered.Rows.Count > 0)
                        {
                            string ResponseID = Registered.Rows[0]["CR_RESPONSE_ID"].ToString();
                            if (string.IsNullOrWhiteSpace(ResponseID) == false)
                            {
                                jsonParam.result = true;
                                return Json(new
                                {
                                    jsonParam,
                                    ResponseID = ResponseID,
                                    InstanceID = collection["FormInstanceID"],
                                    pathprefix = Request.Url.Scheme + "://" + Request.Url.Authority,
                                    AlreadyRegistered = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                int TotalQuestions = Convert.ToInt32(collection["Sections.Count"]);
                Param_Insert_Form_Response Inparam = new Param_Insert_Form_Response();
                Inparam.ServiceUserID = Convert.ToInt32(collection["UserID"]);
                Inparam.Response_ID = Guid.NewGuid().ToString();
                Inparam.FormInstanceID = collection["FormInstanceID"];
                List<Param_Form_Response> param = new List<Param_Form_Response>();
                for (int i = 1; i <= TotalQuestions; i++)
                {
                    Param_Form_Response InResponse = new Param_Form_Response();
                    string AnsResponse = collection["AnsControl_" + i + "_FormResponse"];
                    bool required = Convert.ToBoolean(collection["Sections[" + (i - 1) + "].Required"]);
                    string mode = collection["Sections[" + (i - 1) + "].Mode"];
                    if (mode == "Attachment")
                    {
                        HttpPostedFileBase Responseimage = Request.Files["AnsControl_" + i + "_FormResponse"];
                        if (Responseimage == null || Responseimage.ContentLength == 0)
                        {
                            if (required == true)
                            {
                                jsonParam.message = "Question " + i + " Is Required.";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "AnsControl_" + i + "_FormResponse_FileName";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            BinaryReader reader = new BinaryReader(Responseimage.InputStream);
                            byte[] imageBytes = reader.ReadBytes((int)Responseimage.ContentLength);
                            reader.Close();
                            reader.Dispose();
                            string FileName = CommonFunctions.TransformFileName(Responseimage.FileName, imageBytes, true);
                            var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                            if (!regex.IsMatch(FileName))
                            {
                                jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + Responseimage.FileName;
                                jsonParam.title = "Information";
                                jsonParam.focusid = "AnsControl_" + i + "_FormResponse_FileName";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            InResponse.File_Name = FileName;
                            InResponse.File = imageBytes;
                        }
                    }
                    else
                    {
                        if (required == true)
                        {
                            if (string.IsNullOrWhiteSpace(AnsResponse))
                            {
                                jsonParam.message = "Question " + i + " Is Required.";
                                jsonParam.title = "Information";
                                jsonParam.focusid = "AnsControl_" + i + "_FormResponse";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    InResponse.Response = AnsResponse;
                    InResponse.Question_ID = collection["Sections[" + (i - 1) + "].Rec_ID"];
                    param.Add(InResponse);
                }
                Inparam.InFormResponse = param.ToArray();
                if (BASE._Form_dbops.InsertFormResponse_Txn(Inparam))
                {
                    jsonParam.message = "Form Response Saved Succesfully";
                    jsonParam.title = "Form Response";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        ResponseID = Inparam.Response_ID,
                        InstanceID = Inparam.FormInstanceID,
                        pathprefix = Request.Url.Scheme + "://" + Request.Url.Authority,
                        AlreadyRegistered = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Form Response";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public string LinkForResponse(string InstanceID)
        {
            return ConfigurationManager.AppSettings["Servicespath"] + "form/" + InstanceID;
        }
        public ActionResult LookUp_Get_ProjectList(DataSourceLoadOptions loadOptions, string Remark2Filter = "")
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTabletoProjectNameList_GSM(BASE._SR_DBOps.GetProjects("Name", "ID", Remark2Filter)), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_ChartInstance(DataSourceLoadOptions loadOptions, string EventID = "", string ServiceReportID = "", string ChartID = "")
        {
            if (string.IsNullOrWhiteSpace(EventID) && string.IsNullOrWhiteSpace(ServiceReportID))
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<PreviousChartInstance>(), loadOptions)), "application/json");
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTabletoPreviousChartInstance(BASE._Form_dbops.GetPreviousChartInstances(EventID, ServiceReportID, ChartID)), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_ScheduleList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Schedule_DBOps.Get_Schedules(true);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_scheduleList(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_ServiceReport_Event(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.Get_ServiceReports_And_Events();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Event(d1), loadOptions)), "application/json");
        }
        public int GetQuestionReponseCount(string QuestionID)
        {
            return (int)BASE._Form_dbops.GetQuestionReponseCount(QuestionID);
        }
        public List<miscInfo> GetQuestionTagList()
        {
            DataTable d1 = BASE._Form_dbops.GetQuestionTags("NAME", "ID");
            return DatatableToModel.DataTableTo_Name_ID(d1);
        }
        public List<miscInfo> GetPredefinedOptionList()
        {
            DataTable d1 = BASE._Form_dbops.GetPreDefinedOptionList("NAME", "ID");
            return DatatableToModel.DataTableTo_Name_ID(d1);
        }
        public ActionResult GetQuestionTextSizeList(DataSourceLoadOptions loadOptions)
        {
            List<string> data = new List<string>();
            for (int i = 6; i <= 36; i++)
            {
                data.Add(i + "px");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        public ActionResult ViewFormUserSide(string ChartInstanceId = null, string UserID = null, string User_AB_ID = null, string SessionGUID = null, string ActionMethod = "_New", string ResponseID = null, string GuideSessionID = null, string Cone_UserID = null, string Cone_CenID = null, bool For_C_One_user = true, string FormUrl = "", string PopupID = "")
        {
            FormResponses model = new FormResponses();
            model.FormInstanceID = ChartInstanceId;
            model.UserID = UserID;
            model.UserABID = User_AB_ID;
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod.StartsWith("_") ? ActionMethod : "_" + ActionMethod);
            model.ResponseID = ResponseID;
            model.Connectone_UserID = string.IsNullOrWhiteSpace(Cone_UserID) ? BASE._open_User_ID : Cone_UserID;
            model.Connectone_CenID = string.IsNullOrWhiteSpace(Cone_CenID) ? BASE._open_Cen_ID.ToString() : Cone_CenID;
            ViewBag.For_C_One_user = For_C_One_user;
            ViewBag.FormUrl = FormUrl;
            ViewBag.PopupID = PopupID;
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Add_Notification_Setting_CreateForm(string EmailConfirm, string EmailApprove, string EmailReject, string SMSConfirm, string SMSApprove, string SMSReject, string WhatsappConfirm, string WhatsappApprove, string WhatsappReject, bool IsEmailVisible, bool IsWhatsappVisible, bool IsSMSVisible, bool IsEmailConfirmVisible, bool IsEmailApproveVisible, bool IsEmailRejectVisible, bool IsWhatsappConfirmVisible, bool IsWhatsappApproveVisible, bool IsWhatsappRejectVisible, bool IsSMSConfirmVisible, bool IsSMSApproveVisible, bool IsSMSRejectVisible, string SessionGUID = null, string FormInstanceID = "",string BatchData="",string DefaultAdminEmail="",string DefaultAdminWhatsApp="",bool OpenduringFormSave=false)
        {
            Notification_Setting_CreateForm model = new Notification_Setting_CreateForm();
            model.FormInstanceID_AddNotification = FormInstanceID;
            model.IsEmailVisible = IsEmailVisible;
            model.IsWhatsappVisible = IsWhatsappVisible;
            model.IsSMSVisible = IsSMSVisible;
            model.IsEmailConfirmVisible = IsEmailConfirmVisible;
            model.IsEmailApproveVisible = IsEmailApproveVisible;
            model.IsEmailRejectVisible = IsEmailRejectVisible;
            model.IsWhatsappConfirmVisible = IsWhatsappConfirmVisible;
            model.IsWhatsappApproveVisible = IsWhatsappApproveVisible;
            model.IsWhatsappRejectVisible = IsWhatsappRejectVisible;
            model.IsSMSConfirmVisible = IsSMSConfirmVisible;
            model.IsSMSApproveVisible = IsSMSApproveVisible;
            model.IsSMSRejectVisible = IsSMSRejectVisible;
            model.SMSConfirm = SMSConfirm; //content
            model.SMSApprove = SMSApprove;
            model.SMSReject = SMSReject;
            model.AdminEmail_Notification_Setting_CreateForm = DefaultAdminEmail;
            model.AdminWhatsappNo_Notification_Setting_CreateForm = DefaultAdminWhatsApp;
            model.OpenDuringFormSave = OpenduringFormSave;
            if ((IsWhatsappConfirmVisible || IsWhatsappApproveVisible || IsWhatsappRejectVisible) && model.IsWhatsappVisible)
            {
                model.WhatsappApprove = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(WhatsappApprove), "</?p>", String.Empty).ExtractTextFromHTML();
                model.WhatsappConfirm = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(WhatsappConfirm), "</?p>", String.Empty).ExtractTextFromHTML();
                model.WhatsappReject = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(WhatsappReject), "</?p>", String.Empty).ExtractTextFromHTML();
                model.WhatsappApprove_Raw = WebUtility.HtmlDecode(WhatsappApprove);
                model.WhatsappConfirm_Raw = WebUtility.HtmlDecode(WhatsappConfirm);
                model.WhatsappReject_Raw = WebUtility.HtmlDecode(WhatsappReject);
            }
            if ((IsEmailConfirmVisible || IsEmailApproveVisible || IsEmailRejectVisible) && IsEmailVisible)
            {
                model.EmailApprove = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(EmailApprove), "</?p>", String.Empty).ExtractTextFromHTML();
                model.EmailConfirm = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(EmailConfirm), "</?p>", String.Empty).ExtractTextFromHTML();
                model.EmailReject = System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(EmailReject), "</?p>", String.Empty).ExtractTextFromHTML();
                model.EmailApprove_Raw = WebUtility.HtmlDecode(EmailApprove);
                model.EmailConfirm_Raw = WebUtility.HtmlDecode(EmailConfirm);
                model.EmailReject_Raw = WebUtility.HtmlDecode(EmailReject);
            }
            if ((IsSMSConfirmVisible || IsSMSApproveVisible || IsSMSRejectVisible) && IsSMSVisible)
            {
                if (string.IsNullOrWhiteSpace(SMSConfirm) && string.IsNullOrWhiteSpace(SMSApprove) && string.IsNullOrWhiteSpace(SMSReject))
                {
                    model.IsSMSVisible = false;
                }
            }
            if (string.IsNullOrWhiteSpace(BatchData) == false) 
            {
                Notification_Setting_CreateForm data = JsonConvert.DeserializeObject<Notification_Setting_CreateForm>(HttpUtility.UrlDecode(BatchData));
                if (data != null) 
                {
                    model.Categories_Notification_Setting_CreateForm = data.Categories_Notification_Setting_CreateForm;
                    model.AdminEmail_Notification_Setting_CreateForm = data.AdminEmail_Notification_Setting_CreateForm;
                    model.AdminWhatsappNo_Notification_Setting_CreateForm = data.AdminWhatsappNo_Notification_Setting_CreateForm;
                    if ((IsEmailConfirmVisible || IsEmailApproveVisible || IsEmailRejectVisible) && IsEmailVisible)
                    {
                        model.CC_Email_Notification_Setting_CreateForm = data.CC_Email_Notification_Setting_CreateForm;
                        model.BCC_Email_Notification_Setting_CreateForm = data.BCC_Email_Notification_Setting_CreateForm;
                        model.ReplyToEmail_Email_Notification_Setting_CreateForm = data.ReplyToEmail_Email_Notification_Setting_CreateForm;
                        model.Radio_SenderEmail_Notification_Setting_CreateForm = data.Radio_SenderEmail_Notification_Setting_CreateForm;
                        model.Email_Email_Notification_Setting_CreateForm = data.Email_Email_Notification_Setting_CreateForm;
                        model.Password_Email_Notification_Setting_CreateForm = data.Password_Email_Notification_Setting_CreateForm;
                    }
                    if ((IsWhatsappConfirmVisible || IsWhatsappApproveVisible || IsWhatsappRejectVisible) && model.IsWhatsappVisible)
                    {
                        model.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm = data.Radio_SenderWhatsappNumber_Notification_Setting_CreateForm;
                        model.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm = data.DeliverySpeed_Whatsapp_Notification_Setting_CreateForm;
                        model.SenderWhatsapp_Notification_Setting_CreateForm = data.SenderWhatsapp_Notification_Setting_CreateForm;
                    }                    
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SendSampleEmail(string EmailBody, string From, string To, string CC, string Bcc, string ReplyTo, string Pwd, string EmailerName, string ChartInstanceID, string EmailType = "General")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                int chartInstanceID = string.IsNullOrWhiteSpace(ChartInstanceID) ? 0 : Convert.ToInt32(ChartInstanceID);
                EmailBody = WebUtility.HtmlDecode(EmailBody).ReplacePwithDivTags();
                if (EmailType == "General")
                {
                    From = ConfigurationManager.AppSettings["SenderId"];
                    Pwd = ConfigurationManager.AppSettings["senderPassword"];
                }
                if (string.IsNullOrWhiteSpace(ReplyTo)) 
                {
                    ReplyTo = From;
                }
                string Subject = EmailerName;
                Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                InParam.AdminEmail = To;
                InParam.CC = CC;
                InParam.BCC = Bcc;
                InParam.ReplyToEmail = ReplyTo;
                InParam.BatchName = EmailerName;
                InParam.Subject = Subject;
                InParam.Email = From;
                InParam.Password = Pwd;
                InParam.Content = EmailBody;
                InParam.ChartInstanceId = ChartInstanceID;
                InParam.Mode = "Email";
                BASE._Form_dbops.Insert_Sample_Form_Response_NotificationSettings(InParam, chartInstanceID);
                //bool sent = BASE._Notifications_DBOps.SendEmail(To, Subject, EmailBody, EmailerName, CC, Bcc, ReplyTo, "", "", From, Pwd, To);

                jsonParam.message = To + ": Email notification queued successfully.";
                jsonParam.title = "Information";
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
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SendSampleWhatsApp(string Msg, string FromNo, string ToNo,string BatchName, string ChartInstanceID,string Speed, string SenderType = "General")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                int chartInstanceID = string.IsNullOrWhiteSpace(ChartInstanceID) ? 0 : Convert.ToInt32(ChartInstanceID);
                Msg = WebUtility.HtmlDecode(Msg).ConvertHtmlToWhatsappText();
               // if (string.IsNullOrWhiteSpace(ChartInstanceID)) 
                //{
                    Msg = Msg.Replace("#", "");
                //}
                Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                InParam.AdminWhatsAppNo = ToNo;
                InParam.BatchName = BatchName;
                InParam.Content = Msg;
                InParam.Whatsappno = FromNo;
                InParam.DeliverySpeed = Speed;
                InParam.ChartInstanceId = ChartInstanceID;
                InParam.Mode = "Whatsapp";
                BASE._Form_dbops.Insert_Sample_Form_Response_NotificationSettings(InParam, chartInstanceID);
                //bool sent = BASE._Notifications_DBOps.SendEmail(To, Subject, EmailBody, EmailerName, CC, Bcc, ReplyTo, "", "", From, Pwd, To);

                jsonParam.message = ToNo + ": WhatsApp notification queued successfully.";
                jsonParam.title = "Information";
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
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_GetOtp_WhatsAppLinking(string Mobile)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (string.IsNullOrWhiteSpace(Mobile))
            {
                jsonParam.message = "Invalid mobile number.";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            DataTable d1 = BASE._Form_dbops.InsertMobileLoginOTPFormNotifications(Mobile);       
            if (d1 != null)
            {       
                jsonParam.message = d1.Rows[0]["OTP"].ToString();                 
                jsonParam.title = "Information...";
                jsonParam.result = true;
            }
            else
            {
                jsonParam.message = "Error while fetching OTP.";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckLoginWhatsAppLinking(string Mobile)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (string.IsNullOrWhiteSpace(Mobile))
            {
                jsonParam.message = "Invalid mobile number.";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            DataTable d1 = BASE._Form_dbops.get_RefreshCallForOTP(Mobile);  
            if (d1 != null)
            {                            
                jsonParam.message = d1.Rows[0]["OTP"].ToString();                
                jsonParam.title = "Information...";
                jsonParam.result = true;
            }
            else
            {
                jsonParam.message = "Error while fetching OTP.";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public void Chart_User_Rights()
        {
            ViewBag.Purpose_CHART = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE CHART");
            ViewBag.Purpose_REGISTRATION = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE REGISTRATION");
            ViewBag.Purpose_FORM = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE FORM");
            ViewBag.Purpose_FEEDBACK = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE FEEDBACK");
            ViewBag.Purpose_TRAVEL_DETAIL = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE TRAVEL DETAIL");
            ViewBag.Purpose_ACCOMMODATION = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE ACCOMMODATION");
            ViewBag.Purpose_EVENT_REQUEST = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE EVENT REQUEST");
            ViewBag.Purpose_TASK = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE TASK");
            ViewBag.Purpose_BASIC_DETAILS = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE BASIC DETAILS");
            ViewBag.Purpose_SURVEY_FORMS = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE SURVEY FORMS");
            ViewBag.Purpose_QUIZ = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE QUIZ");
            ViewBag.Purpose_SEVADHARI = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CHART PURPOSE SEVADHARI");
        }
    }
}