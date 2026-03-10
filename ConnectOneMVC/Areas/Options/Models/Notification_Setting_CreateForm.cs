using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class Notification_Setting_CreateForm
    {
        public string Categories_Notification_Setting_CreateForm { get; set; }
        public string AdminEmail_Notification_Setting_CreateForm { get; set; }
        public string AdminWhatsappNo_Notification_Setting_CreateForm { get; set; }
        #region Whatsapp 
        public string Radio_SenderWhatsappNumber_Notification_Setting_CreateForm { get; set; }
        public string SenderWhatsapp_Notification_Setting_CreateForm { get; set; }
        public string DeliverySpeed_Whatsapp_Notification_Setting_CreateForm { get; set; }
        #endregion
        #region Email
        public string CC_Email_Notification_Setting_CreateForm { get; set; }
        public string BCC_Email_Notification_Setting_CreateForm { get; set; }
        public string ReplyToEmail_Email_Notification_Setting_CreateForm { get; set; }
        public string Radio_SenderEmail_Notification_Setting_CreateForm { get; set; }
        public string Email_Email_Notification_Setting_CreateForm { get; set; }
        public string Password_Email_Notification_Setting_CreateForm { get; set; }
        #endregion
        #region SMS
        public string Content_SMS_Notification_Setting_CreateForm { get; set; }
        #endregion
   
        public bool IsEmailVisible { get; set; } 
        public bool IsWhatsappVisible { get; set; }
        public bool IsSMSVisible { get; set; } 
        public bool IsEmailConfirmVisible { get; set; } 
        public bool IsEmailApproveVisible { get; set; } 
        public bool IsEmailRejectVisible { get; set; } 
        public bool IsWhatsappConfirmVisible { get; set; } 
        public bool IsWhatsappApproveVisible { get; set; } 
        public bool IsWhatsappRejectVisible { get; set; } 
        public bool IsSMSConfirmVisible { get; set; } 
        public bool IsSMSApproveVisible { get; set; } 
        public bool IsSMSRejectVisible { get; set; }
        [AllowHtml]
        public string EmailConfirm { get; set; }
        [AllowHtml]
        public string EmailApprove { get; set; }
        [AllowHtml]
        public string EmailReject { get; set; }
        [AllowHtml]
        public string EmailConfirm_Raw { get; set; }
        [AllowHtml]
        public string EmailApprove_Raw { get; set; }
        [AllowHtml]
        public string EmailReject_Raw { get; set; }
        public string SMSConfirm { get; set; } 
        public string SMSApprove { get; set; } 
        public string SMSReject { get; set; }
        [AllowHtml]
        public string WhatsappConfirm { get; set; }
        [AllowHtml]
        public string WhatsappApprove { get; set; }
        [AllowHtml]
        public string WhatsappReject { get; set; }
        [AllowHtml]
        public string WhatsappConfirm_Raw { get; set; }
        [AllowHtml]
        public string WhatsappApprove_Raw { get; set; }
        [AllowHtml]
        public string WhatsappReject_Raw { get; set; }
        public string FormInstanceID_AddNotification { get;set; }
        public bool OpenDuringFormSave { get; set; }
    }
}