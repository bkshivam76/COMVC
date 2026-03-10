using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class FormResponses
    {
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string FormID { get; set; }
        public string FormInstanceID { get; set; }
        public string UserID { get; set; }
        public string UserABID { get; set; }
        public string LoginRequired { get; set; }
        public bool AllowResubmission { get; set; }
        public string ResponseID { get; set; }
        [AllowHtml]
        public string Form_Title_FormResponse { get; set; }
        [AllowHtml]
        public string Form_Description_FormResponse { get; set; }
        public string FormImagePath { get; set; }
        public int FormImageWidth { get; set; }
        public int FormImageHeight { get; set; }
        public List<FormQuestions_Response> Questions { get; set; }
        public string ResponseMode { get; set; }
        public string UniqueRegNo { get; set; }
        public string UniqueRegSrno { get; set; }
        public string Remarks { get; set; }
        public string Response_Add_On { get; set; }
        public string Response_Add_By { get; set; }
        public List<Multiview> MultiViewItem { get; set; }
        public string DisplayMode { get; set; }
        public int Iteration { get; set; }
        public string Chart_BG_Color { get; set; }
        public string Question_BG_Color { get; set; }
        public string Question_FG_Color { get; set; }
        public string Question_Font_Size { get; set; }
        public string PRE_REQUIRED_CHART_SR_ID { get; set; }
        public string Response_Status_On { get; set; }
        public string Response_Status_By { get; set; }
        public string Response_Status { get; set; }
        public bool UserProfile_Visible { get; set; }
        public string Connectone_UserID { get; set; }
        public string Connectone_CenID { get; set; }
    }
    [Serializable]
    public class FormQuestions_Response
    {
        [AllowHtml]
        public string Question { get; set; }
        public string Mode { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
        public long? Min { get; set; }
        public long? Max { get; set; }
        public int QuestionSrNo { get; set; }
        public string Rec_ID { get; set; }
        public string QuestionImagePath { get; set; }
        public int QuestionImageWidth { get; set; }
        public int QuestionImageHeight { get; set; }
        public string QuestionImageFileName { get; set; }
        public Responses Answers { get; set; }
        public string QuestionFormula { get; set; }
        public DateTime? MaxDateTime { get; set; }
        public DateTime? MinDateTime { get; set; }
        public int RowNo { get; set; }
        public int ColumnSpan { get; set; }
        [AllowHtml]
        public string GroupHeading { get; set; }
        public string DefaultValue { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool IsOptionsPredefined { get; set; }
        public string OptionPredefinedMiscID { get; set; }
        public bool DefaultVisibility { get; set; }
        public bool VisibleStatusDuringSave { get; set; }
    }
    public class Responses
    {
        [AllowHtml]
        public string Response { get; set; }
        [AllowHtml]
        public string Response_Other { get; set; }
        public string Response_FileName { get; set; }
        public HttpPostedFileBase Response_File { get; set; }
        public string Response_FilePath { get; set; }
        public string Response_FileMimeType { get; set; }
        [AllowHtml]
        public string MultiOptionText { get; set; }
        public string Rec_ID { get; set; }
    }
    public class Multiview
    {
        public string html { get; set; }
        public string text { get; set; }
        public bool disabled { get; set; }
        public int index { get; set; }
    }
    [Serializable]
    public class FormQuestions
    {
        [AllowHtml]
        public string Question { get; set; }
        public string Mode { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
        public long? Min { get; set; }
        public long? Max { get; set; }
        public int QuestionSrNo { get; set; }
        public string Rec_ID { get; set; }
        public string QuestionImagePath{ get; set; }
        public int QuestionImageWidth { get; set; }
        public int QuestionImageHeight { get; set; }
        public string QuestionImageFileName { get; set; }
        public HttpPostedFileBase QuestionImageFile { get; set; }
        public List<QuestionOptions> Options { get; set; }
        public string QuestionFormula { get; set; }
        public int? MaxLength { get; set; }
        public DateTime? MaxDateTime { get; set; }
        public DateTime? MinDateTime { get; set; }
        public int RowNo { get; set; }
        public int ColumnSpan { get; set; }
        [AllowHtml]
        public string GroupHeading { get; set; }
        public string DefaultValue { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool IsOptionsPredefined { get; set; }
        public string OptionPredefinedMiscID { get; set; }
        public bool DefaultVisibility { get; set; }
        public string Tag { get; set; }

    }
    [Serializable]
    public class QuestionOptions
    {
        [AllowHtml]
        public string Options { get; set; }
        public string Rec_ID { get; set; }
        public string optionImagePath { get; set; }
        public string QuestionID { get; set; }
        public int  Option_SrNo { get; set; }
        public string OptionImageFileName { get; set; }
        public HttpPostedFileBase OptionImageFile { get; set; }
        public string DependentQuestion { get; set; }
        public string DependentQuestionVisibility { get; set; }
        public double? Points { get; set; }
    }
    [Serializable]
    public class SecAndOptionData
    {
        public int secno { get; set; }
        public int[] options { get; set; }
        public string[] DeleteOptionRecID { get; set; }
        public string[] DeleteQuestionsRecID { get; set; }
    }
    [Serializable]
    public class Deleted_Ques_Options
    {
        public string[] DeleteOptionRecID { get; set; }
        public string[] DeleteQuestionsRecID { get; set; }
    }
    [Serializable]
    public class Form_Info
    {
        public string F_Title { get; set; }
        public string F_Description { get; set; }
        public string F_LoginRequired { get; set; }
        public DateTime? REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public string REC_STATUS { get; set; }
        public DateTime? REC_STATUS_ON { get; set; }
        public string REC_STATUS_BY { get; set; }
        public string REC_ID { get; set; }
    }
    [Serializable]
    public class CreateForm
    {
        public string Sr_Event_createForm { get; set; }
        public string Event_ID { get; set; }
        public string SR_ID { get; set; }
        public string FormID { get; set; }
        public string FormInstanceID { get; set; }
        public string Tag { get; set; }
        public string FormName_createForm { get; set; }
        [AllowHtml]
        public string FormTitle_createForm { get; set; }
        [AllowHtml]
        public string FormDescription_createForm { get; set; }
        public string FormLoginRequired_createForm { get; set; }
        public string ProjectID_createForm { get; set; }
        public string Frequency_createForm { get; set; }
        public string Purpose_createForm { get; set; }
        public bool ApprovalReq_createForm { get; set; }
        [AllowHtml]
        public string ConfirmMsg_createForm { get; set; }
        public bool GenerateReg_createForm { get; set; }
        public bool GenerateRegApproval_createForm { get; set; }
        public string RegNo_createForm { get; set; }
        public bool AllowResubmission_createForm { get; set; }
        public string RegNoPrefix_createForm { get; set; }
        public string RegNoSuffix_createForm { get; set; }
        [AllowHtml]
        public string ApprovalMsg_createForm { get; set; }
        [AllowHtml]
        public string RejectionMsg_createForm { get; set; }
        public string PreRequiredChartSrID_createForm { get; set; }
        public List<FormUserProfileSetting> ProfileSettings { get; set; }
        public List<FormUserProfileSetting> GroupProfileSettings { get; set; }
        public int Max_GroupRegistrations_Allowed_createForm { get; set; }
        public HttpPostedFileBase FormImageFile_createForm { get; set; }
        public string FormImagePath { get; set; }
        public string FormImageFileName { get; set; }
        public int FormImageWidth_createForm { get; set; }
        public int FormImageHeight_createForm { get; set; }
        public HttpPostedFileBase FormLoginFile_createForm { get; set; }
        public string LoginImagePath { get; set; }
        public string LoginImageFileName { get; set; }
        public HttpPostedFileBase FormMobileFile_createForm { get; set; }
        public string ResponsiveImagePath { get; set; }
        public string ResponsiveImageFileName { get; set; }
        public HttpPostedFileBase FormThumbnailFile_createForm { get; set; }
        public string ThumbsImagePath { get; set; }
        public string ThumbsImageFileName { get; set; }
        public DateTime? StartDate_createForm { get; set; }
        public DateTime? EndDate_createForm { get; set; }
        [AllowHtml]
        public string StartDateMsg_createForm { get; set; }
        [AllowHtml]
        public string EndDateMsg_createForm { get; set; }
        public string DisplayMode_createForm { get; set; }
        public string FormBgColor_createForm { get; set; }
        public string QuestionBgColor_createForm { get; set; }
        public string QuestionFgColor_createForm { get; set; }
        public string QuestionFontsize_createForm { get; set; }
        public string GroupTitleBgColor_createForm { get; set; }
        public string GroupTitleFontsize_createForm { get; set; }
        public string GroupTitleFgColor_createForm { get; set; }
        public string FormBgImage_createForm { get; set; }
        public DateTime? ActiveFrom_createForm { get; set; }
        public DateTime? ActiveTo_createForm { get; set; }
        public List<FormQuestions> Section { get; set; }
        public string SecAndOptionData { get; set; }
        public string Deleted_Ques_option_RecIDs { get; set; }
        public int ResponseCount { get; set; }
        public List<string> GroupHeadingsList { get; set; }
        public List<miscInfo> PreDefinedOptionList { get; set; }
        public bool UserProfileVisibility_createForm { get; set; }
        public int? CustomSchedule_createForm { get; set; }
        public bool NotificationOnInstanceCreation_createForm { get; set; }
        public int CreatedInstanceCount_createForm { get; set; }
        public DateTime? LastInstance_StartDate_createForm { get; set; }
        public DateTime? LastInstance_EndDate_createForm { get; set; }
        //public DateTime? PrevStartDate_createForm { get; set; }
        public int? MaxEntriesAllowed_createForm { get; set; }
        public List<miscInfo> QuestionTagList { get; set; }
        public bool StartDateChange_createForm { get; set; }
        public bool RegistrationType_createForm { get; set; }
        public string Screen { get; set; }
        [AllowHtml]
        public string ConfirmWhatsapp_createForm { get; set; }
        public string ConfirmSMS_createForm { get; set; }
        [AllowHtml]
        public string ApprovalWhatsapp_createForm { get; set; }
        [AllowHtml]
        public string RejectionWhatsapp_createForm { get; set; }
        public string ApprovalSMS_createForm { get; set; }
        public string RejectionSMS_createForm { get; set; }
        public bool IsEmailNotification_createForm { get; set; }//Msg
        public bool IsWhatsappNotification_createForm { get; set; }
        public bool IsSMSNotification_createForm { get; set; }
        public bool IsEmailConfirmEnabled_createForm { get; set; }
        public bool IsEmailAcceptEnabled_createForm { get; set; }
        public bool IsEmailRejectEnabled_createForm { get; set; }
        public bool IsWhatsappConfirmEnabled_createForm { get; set; }
        public bool IsWhatsappAcceptEnabled_createForm { get; set; }
        public bool IsWhatsappRejectEnabled_createForm { get; set; }
        public bool IsSMSConfirmEnabled_createForm { get; set; }
        public bool IsSMSAcceptEnabled_createForm { get; set; }
        public bool IsSMSRejectEnabled_createForm { get; set; }
        public string DefaultAdminEmail_createForm { get; set; }
        public string DefaultAdminWhatsApp_createForm { get; set; }
        public bool NotificationSettingsOpened_createForm { get; set; }
        public string Template_DD_ConfirmWhatsapp_TemplatePreview_CreateForm { get; set; }
        public string Template_DD_ApprovalWhatsapp_TemplatePreview_CreateForm { get; set; }
        public string Template_DD_RejectWhatsapp_TemplatePreview_CreateForm { get; set; }
        public string ConfirmWhatsapp_Template_Language_createForm { get; set; }
        public string ApprovalWhatsapp_Template_Language_createForm { get; set; }
        public string RejectionWhatsapp_Template_Language_createForm { get; set; }
        public ConnectOneMVC.Areas.Options.Models.Notification_Setting_CreateForm NotificationSetting{get; set;}        
    }
    [Serializable]
    public class FormUserProfileSetting
    {
        public string Field { get; set; }
        public bool Visible { get; set; }
        public bool Mandatory { get; set; }
        public bool Enable { get; set; }
        public string Rec_ID { get; set; }
    }
    [Serializable]
    public class PreviousChartInstance
    {
        public string ChartName { get; set; }
        public string SrNO { get; set; }
    }
    [Serializable]
    public class miscInfo
    {
        public string NAME { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public class AccommodationList
    {
        public string Building { get; set; }
        public string Room { get; set; }
        public string REC_ID { get; set; }
        public Int32 Allowed { get; set; }
        public Int32 Available { get; set; }
        public Int32 Allotted { get; set; }
        public Int32 CurrentAllottment { get; set; }
    }
    [Serializable]
    public class LiveFormsList
    {
        public Int64 INSTANCE_ID {get; set; } 
        public Int64 CSN_CHART_ID {get; set; } 
        public string FORM_NAME {get; set; } 
        public string EVENTNAME {get; set; } 
        public Int64 CSN_SRNO {get; set; } 
        public string CSN_SERVICE_REPORT_ID {get; set; } 
        public DateTime? FROMDATE {get; set; } 
        public DateTime? TODATE { get; set; }
    
    }
    [Serializable]
    public class AccommodationDetailedList
    {
        public string CNAME { get; set; }
        public string GENDER { get; set; }
        public string CITY { get; set; }
        public string CenterName { get; set; }
        public DateTime? Arr_Date { get; set; }
        public DateTime? Dep_Date { get; set; }
        public DateTime? Dep_Time { get; set; }
        public string ROOMID { get; set; }
        public string CHARTRESP_ID { get; set; }
    }

    [Serializable]
    public class ServicePlaceNamesList
    {
        public string BUILDING_NAME { get; set; }
        public string REC_ID { get; set; }
    }
}