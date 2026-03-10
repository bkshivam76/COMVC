using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class Notification_Chart_Responses
    {
        public string Tag { get; set; }
        public string Chart_Instance_Id { get; set; }
        public string SetNotificationTo_Radio { get; set; }
        public string LoggedIn_User_Id { get; set; }
        public string ResponseStatus_Notify_ChartResponses { get; set; }
        public Int32 ResponseCount_Notify_ChartResponses { get; set; }
        public string AdminEmail_Notify_ChartResponses { get; set; }
        public string AdminWhatsappNo_Notify_ChartResponses { get; set; }
        #region Whatsapp
        public string SelectedTabTitle_Notify_ChartResponses { get; set; }
        public HttpPostedFileBase File_Whatsapp_Notify_ChartResponses { get; set; }
        public string Content_Whatsapp_Notify_ChartResponses { get; set; }
        public string BatchName_Whatsapp_Notify_ChartResponses { get; set; }
        public string Radio_SenderWhatsappNumber_Notify_ChartResponses { get; set; }
        public string SenderWhatsapp_Notify_ChartResponses { get; set; }
        public string DeliverySpeed_Whatsapp_Notify_ChartResponses { get; set; }
        #endregion
        #region Whatsapp Paid API
        public string TemplateID_WhatsappPaid_CustomNotification { get; set; }
        public string TemplateName_WhatsappPaid_CustomNotification { get; set; }
        public string Content_WhatsappPaid_customNotification { get; set; }
        public string BatchName_WhatsappPaid { get; set; }
        public HttpPostedFileBase File_WhatsappPaid_customNotification { get; set; }
        public string SampleWhatsappNumber_paid_CustomNotification { get; set; }
        public string phoneNumberId_whatsappPaid { get; set; }
        public string accessToken_whatsappPaid { get; set; }
        public string language_whatsappPaid { get; set; }
        public string withorwithoutMedia_whatsappPaid { get; set; }
        #endregion
        #region Email
        public string Content_Email_Notify_ChartResponses { get; set; }
        public string Subject_Email_Notify_ChartResponses { get; set; }
        public string CC_Email_Notify_ChartResponses { get; set; }
        public string BCC_Email_Notify_ChartResponses { get; set; }
        public string ReplyToEmail_Email_Notify_ChartResponses { get; set; }
        public string BatchName_Email_Notify_ChartResponses { get; set; }
        public string Radio_SenderEmail_Notify_ChartResponses { get; set; }
        public string Email_Email_Notify_ChartResponses { get; set; }
        public string Password_Email_Notify_ChartResponses { get; set; }
        #endregion
        public string Content_SMS_Notify_ChartResponses { get; set; }
        public string SendNotificationButtonPressed_Notify_ChartResponses { get; set; }

        public string selectedFormResponses_group { get; set; }
        public string selectedAddresses_group { get; set; }
        public string selectedFormResponses_str { get; set; }
        public string selectedContacts_str { get; set; }

        //Arrays of Address Book related data
        public string SelectedDesignationIds_notificationSet { get; set; }
        public string SelectedOccupationIds_notificationSet { get; set; }
        public string SelectedCategoryIds_notificationSet { get; set; }
        public string SelectedMagazineIds_notificationSet { get; set; }
        public string SelectedSpecialitiesIds_notificationSet { get; set; }
        public string SelectedEventsIds_notificationSet { get; set; }
        public string SelectedTitleIds_notificationSet { get; set; }
        public string SelectedWingMemberIds_notificationSet { get; set; }
        public string SelectedCityIDs_notificationSet { get; set; }
        public string SelectedDistrictIDs_notificationSet { get; set; }
        public string EnteredPhoneNumbersOrEmails_notificationSet { get; set; }
        public bool IsPublic { get; set; }
        public string Public_hardcoded_AttachmentPath { get; set; }
        public string Public_Addby { get; set; }
        public int Public_CentreID { get; set; }
        public string Public_BatchCreation_NotifyMessage { get; set; }
    }
}