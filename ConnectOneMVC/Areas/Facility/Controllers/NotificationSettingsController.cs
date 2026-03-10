//https://docs.google.com/document/d/15szN3a_scpA1CqCg7tnqwlmpeJZcWDvnwPOmbSKIi3k/edit?tab=t.0
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
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
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Forms;
using System.Net.Security;
using Newtonsoft.Json.Linq;
using ConnectOneMVC.Areas.Facility.Controllers;

namespace ConnectOneMVC.Areas.Facility.Controllers
{    
    public class NotificationSettingsController : BaseController
    {
        public DataTable dt_chartResponseNotificationBatchQueueGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseNotificationBatchQueueGrid_notificationSet"); }
            set { SetBaseSession("dt_chartResponseNotificationBatchQueueGrid_notificationSet", value); }
        }
        public DataTable dt_chartResponseWhatsappNotificationQueueGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseWhatsappNotificationQueueGrid_notificationSet"); }
            set { SetBaseSession("dt_chartResponseWhatsappNotificationQueueGrid_notificationSet", value); }
        }
        public DataTable dt_chartResponseEmailNotificationQueueGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseEmailNotificationQueueGrid_notificationSet"); }
            set { SetBaseSession("dt_chartResponseEmailNotificationQueueGrid_notificationSet", value); }
        }
        public DataTable dt_chartResponseWhatsappNotificationDeliveryLogGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseWhatsappNotificationDeliveryLogGrid_notificationSet"); }
            set { SetBaseSession("dt_chartResponseWhatsappNotificationDeliveryLogGrid_notificationSet", value); }
        }
        public DataTable dt_chartResponseEmailNotificationDeliveryLogGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseEmailNotificationDeliveryLogGrid_notificationSet"); }
            set { SetBaseSession("dt_chartResponseEmailNotificationDeliveryLogGrid_notificationSet", value); }
        }
        public DataTable dt_addressbookDropDownsMasterData
        {
            get { return (DataTable)GetBaseSession("dt_addressbookDropDownsMasterData_notificationSet"); }
            set { SetBaseSession("dt_addressbookDropDownsMasterData_notificationSet", value); }
        }

        // GET: Facility/NotificationSettings
        [CheckLogin]
        public ActionResult Frm_Notification_Settings_Info(string instanceID = null)
        {            
            DataTable dt_addressbookDropDownsMasterData = BASE._Address_DBOps.GetAllMasters("Name", "ID");           
            return View(dt_addressbookDropDownsMasterData);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Get_OTP(string Mobile)
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
            string s1 = null;
            if (d1 != null)
            {
                s1 = d1.Rows[0]["OTP"].ToString();
                if (s1.Equals(null) || s1.Equals(""))
                {
                    jsonParam.message = s1;
                }
                else if (s1.Equals("0"))
                {
                    jsonParam.message = "Login Process Over!!!";
                }
                else
                {
                    jsonParam.message = s1;
                }
                jsonParam.title = "Information...";
                jsonParam.result = true;
            }
            else
            {
                jsonParam.message = "Error while fetching OTP. Please right click and refresh then retry.";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_InsertRowForOTP(string Mobile)
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
            string s1 = null;
            if (d1 != null)
            {
                s1 = d1.Rows[0]["OTP"].ToString();
                if (s1.Equals(null) || s1.Equals(""))
                {
                    jsonParam.message = s1;
                }
                else if (s1.Equals("0"))
                {
                    jsonParam.message = "Login Process Over!!!";
                }
                else
                {
                    jsonParam.message = s1;
                }
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
        [CheckLogin]
        [HttpGet]
        public ActionResult Frm_ChartResponses_Add_Notification(string ActionMethod, string Chart_Instance_Id, string Chart_Response_Id, string Chart_Response_Status, int RegistrationsTotalCount = 0, int ApprovedResponseCount = 0, int RejectionResponseCount = 0, int DefaultStatus = 0)
        {
            ViewBag.Chart_Instance_Id = Chart_Instance_Id;
            ViewBag.Chart_Response_Id = Chart_Response_Id;
            ViewBag.RegistrationsTotalCount = RegistrationsTotalCount;
            ViewBag.ApprovedResponseCount = ApprovedResponseCount;
            ViewBag.RejectionResponseCount = RejectionResponseCount;
            ViewBag.DefaultStatus = DefaultStatus;
            Notification_Chart_Responses model = new Notification_Chart_Responses();
            model.Tag = ActionMethod;
            model.Chart_Instance_Id = Chart_Instance_Id;
            model.ResponseStatus_Notify_ChartResponses = Chart_Response_Status;
            return View(model);
        }
        [CheckLogin]
        [HttpPost]
        public async Task<ActionResult> Frm_ChartResponses_Add_Notification(Notification_Chart_Responses model)
        {          
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag.Equals("_New"))
                {
                    if (model.SelectedTabTitle_Notify_ChartResponses.Equals("Whatsapp"))
                    {
                        DataTable dt = BASE._Form_dbops.Check_Unique_BatchName(model.BatchName_Whatsapp_Notify_ChartResponses, model.SelectedTabTitle_Notify_ChartResponses);
                        if (Int32.Parse(dt.Rows[0][0].ToString()) == 1)
                        {
                            jsonParam.message = "Batch Name is not Unique. Please enter the Unique Batch name.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        Common_Lib.DbOperations.Forms.Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                        InParam.AdminEmail = model.AdminEmail_Notify_ChartResponses;
                        InParam.AdminWhatsAppNo = model.AdminWhatsappNo_Notify_ChartResponses;
                        if(model.SetNotificationTo_Radio== "formresponses")
                        {                            
                            InParam.ChartInstanceId = model.Chart_Instance_Id;
                        }                        
                        InParam.ResponseStatus = model.ResponseStatus_Notify_ChartResponses;
                        if (model.File_Whatsapp_Notify_ChartResponses != null && model.File_Whatsapp_Notify_ChartResponses.ContentLength > 0)
                        {
                            BinaryReader reader = new BinaryReader(model.File_Whatsapp_Notify_ChartResponses.InputStream);
                            byte[] imageBytes = reader.ReadBytes(model.File_Whatsapp_Notify_ChartResponses.ContentLength);
                            reader.Close();
                            reader.Dispose();
                            string FileName = CommonFunctions.TransformFileName(model.File_Whatsapp_Notify_ChartResponses.FileName, imageBytes, true);
                            var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                            if (!regex.IsMatch(FileName))
                            {
                                jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + FileName;
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            InParam.File = imageBytes;
                            InParam.File_Name = ConfigurationManager.AppSettings["attachmentpath"] + FileName;
                        }
                        InParam.BatchName = model.BatchName_Whatsapp_Notify_ChartResponses;                        
                        InParam.Content = model.Content_Whatsapp_Notify_ChartResponses.ConvertHtmlToWhatsappText();
                        InParam.DeliverySpeed = model.DeliverySpeed_Whatsapp_Notify_ChartResponses;
                        InParam.SenderWhatsappNoType = model.Radio_SenderWhatsappNumber_Notify_ChartResponses;
                        if (InParam.SenderWhatsappNoType.Equals("General"))
                        {
                            InParam.Whatsappno = ConfigurationManager.AppSettings["DefaultWhatsAppSender"];
                        }
                        else if (InParam.SenderWhatsappNoType.Equals("Private"))
                        {
                            InParam.Whatsappno = model.SenderWhatsapp_Notify_ChartResponses;
                        }
                        InParam.SendNotifictionTypeSampleOrNow = model.SendNotificationButtonPressed_Notify_ChartResponses;
                        InParam.Mode = model.SelectedTabTitle_Notify_ChartResponses;
                        if (model.SetNotificationTo_Radio == "formresponses")
                        {
                            InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            if(model.selectedFormResponses_group == "predefined_responses")
                            {
                                InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;                            
                            }
                            else
                            {
                                InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                                InParam.selectedFormResponses_str = model.selectedFormResponses_str;
                            }
                        }
                        else if (model.SetNotificationTo_Radio == "addressbook")
                        {
                            InParam.selectedAddresses_group = model.selectedAddresses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            model.Chart_Instance_Id = "0";
                            if (model.selectedAddresses_group == "predefined_addresses")
                            {
                                InParam.SelectedDesignationIds = model.SelectedDesignationIds_notificationSet;
                                InParam.SelectedOccupationIds = model.SelectedOccupationIds_notificationSet;
                                InParam.SelectedCategoryIds = model.SelectedCategoryIds_notificationSet;
                                InParam.SelectedMagazineIds = model.SelectedMagazineIds_notificationSet;
                                InParam.SelectedSpecialitiesIds = model.SelectedSpecialitiesIds_notificationSet;
                                InParam.SelectedEventsIds = model.SelectedEventsIds_notificationSet;
                                InParam.SelectedTitleIds = model.SelectedTitleIds_notificationSet;
                                InParam.SelectedDistrictIds = model.SelectedDistrictIDs_notificationSet;
                                InParam.SelectedCityIds = model.SelectedCityIDs_notificationSet;
                                InParam.SelectedWingIds = model.SelectedWingMemberIds_notificationSet;                        
                            }
                            else
                            {
                                InParam.selectedAddresses_group = model.selectedAddresses_group;
                                InParam.selectedContacts_str = model.selectedContacts_str;
                            }
                        }
                        else if(model.SetNotificationTo_Radio == "phonenumbers")
                        {
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            InParam.enteredPhoneNumbersOrEmails_str = model.EnteredPhoneNumbersOrEmails_notificationSet;
                            model.Chart_Instance_Id = "0";
                        }
                        else if (model.SetNotificationTo_Radio == "googlesheet")
                        {
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            InParam.enteredPhoneNumbersOrEmails_str = model.EnteredPhoneNumbersOrEmails_notificationSet;
                            model.Chart_Instance_Id = "0";
                        }
                        if (model.IsPublic == true) //for whatsapp public page 
                        {
                            if (string.IsNullOrWhiteSpace(model.Public_hardcoded_AttachmentPath) == false) 
                            {
                                InParam.File_Name = model.Public_hardcoded_AttachmentPath;
                            }
                            if (string.IsNullOrWhiteSpace(model.Public_Addby) == false)
                            {
                                InParam.AddBy = model.Public_Addby;
                            }
                            if (model.Public_CentreID>0)
                            {
                                InParam.CenID = model.Public_CentreID;
                            }
                        }
                            //InParam.SendNotifictionTypeSampleOrNow = "";
                        if (InParam.SendNotifictionTypeSampleOrNow.Equals("Now"))
                        {
                            int Queue_count = BASE._Form_dbops.Insert_Form_Response_Notifications(InParam, Int32.Parse(model.Chart_Instance_Id));
                            if (Queue_count > 0)
                            {
                                if (string.IsNullOrWhiteSpace(model.Public_BatchCreation_NotifyMessage) == false) 
                                {
                                    BASE._Notifications_DBOps.InsertWhatsappQueue("9491143191", model.Public_BatchCreation_NotifyMessage, "Public_BatchCreation", null, null, ConfigurationManager.AppSettings["DefaultWhatsAppSender"],-100);
                                }
                                jsonParam.message = "WhatsApp Notification queued successfully. Queue Count is  ("+Queue_count + ")";
                                jsonParam.title = "Information...";
                                jsonParam.result = true;
                            }
                            else if (Queue_count == 0)
                            {
                                jsonParam.message = "Queue Not created.";
                                jsonParam.title = "Information...";
                                jsonParam.result = true;
                            }

                        }
                        else if (InParam.SendNotifictionTypeSampleOrNow.Equals("Sample"))
                        {
                            BASE._Form_dbops.Insert_Sample_Form_Response_NotificationSettings(InParam, Convert.ToInt32(model.Chart_Instance_Id));
                            jsonParam.message = "Sample Whatsapp successfully set up to Admin Whatsapp Number.";
                            jsonParam.title = "Information...";
                            jsonParam.result = true;
                        }
                    }
                    else if (model.SelectedTabTitle_Notify_ChartResponses.Equals("Email"))
                    {
                        DataTable dt = BASE._Form_dbops.Check_Unique_BatchName(model.BatchName_Email_Notify_ChartResponses, model.SelectedTabTitle_Notify_ChartResponses);
                        if (Int32.Parse(dt.Rows[0][0].ToString()) == 1)
                        {
                            jsonParam.message = "Batch Name is not Unique. Please enter the Unique Batch name.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        Common_Lib.DbOperations.Forms.Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                        InParam.AdminEmail = model.AdminEmail_Notify_ChartResponses;
                        InParam.AdminWhatsAppNo = model.AdminWhatsappNo_Notify_ChartResponses;
                        if (model.SetNotificationTo_Radio == "formresponses")
                        {
                            InParam.ChartInstanceId = model.Chart_Instance_Id;
                        }
                        InParam.ResponseStatus = string.IsNullOrWhiteSpace(model.ResponseStatus_Notify_ChartResponses) ? "Default" : model.ResponseStatus_Notify_ChartResponses;
                        InParam.BatchName = model.BatchName_Email_Notify_ChartResponses;
                        InParam.Content = model.Content_Email_Notify_ChartResponses.ReplacePwithDivTags();
                        InParam.CC = model.CC_Email_Notify_ChartResponses;
                        InParam.BCC = model.BCC_Email_Notify_ChartResponses;
                        InParam.ReplyToEmail = model.ReplyToEmail_Email_Notify_ChartResponses;
                        InParam.Subject = model.Subject_Email_Notify_ChartResponses;
                        InParam.SenderEmailType = model.Radio_SenderEmail_Notify_ChartResponses;
                        InParam.SendNotifictionTypeSampleOrNow = model.SendNotificationButtonPressed_Notify_ChartResponses;
                        InParam.Mode = model.SelectedTabTitle_Notify_ChartResponses;
                        if (InParam.SenderEmailType.Equals("General"))
                        {
                            InParam.Email = ConfigurationManager.AppSettings["SenderId"];
                            InParam.Password = ConfigurationManager.AppSettings["senderPassword"];
                        }
                        else
                        {
                            InParam.Email = model.Email_Email_Notify_ChartResponses;
                            InParam.Password = model.Password_Email_Notify_ChartResponses;
                        }
                        if (model.SetNotificationTo_Radio == "formresponses")
                        {
                            InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            if (model.selectedFormResponses_group == "predefined_responses")
                            {
                                InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            }
                            else
                            {
                                InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                                InParam.selectedFormResponses_str = model.selectedFormResponses_str;
                            }
                        }
                        else if (model.SetNotificationTo_Radio == "addressbook")
                        {
                            InParam.selectedAddresses_group = model.selectedAddresses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            if (model.selectedAddresses_group == "predefined_addresses")
                            {
                                InParam.SelectedDesignationIds = model.SelectedDesignationIds_notificationSet;
                                InParam.SelectedOccupationIds = model.SelectedOccupationIds_notificationSet;
                                InParam.SelectedCategoryIds = model.SelectedCategoryIds_notificationSet;
                                InParam.SelectedMagazineIds = model.SelectedMagazineIds_notificationSet;
                                InParam.SelectedSpecialitiesIds = model.SelectedSpecialitiesIds_notificationSet;
                                InParam.SelectedEventsIds = model.SelectedEventsIds_notificationSet;
                                InParam.SelectedTitleIds = model.SelectedTitleIds_notificationSet;
                                InParam.SelectedDistrictIds = model.SelectedDistrictIDs_notificationSet;
                                InParam.SelectedCityIds = model.SelectedCityIDs_notificationSet;
                                InParam.SelectedWingIds = model.SelectedWingMemberIds_notificationSet;
                            }
                            else
                            {
                                InParam.selectedAddresses_group = model.selectedAddresses_group;
                                InParam.selectedContacts_str = model.selectedContacts_str;
                            }
                        }
                        else if (model.SetNotificationTo_Radio == "phonenumbers")
                        {
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            InParam.enteredPhoneNumbersOrEmails_str = model.EnteredPhoneNumbersOrEmails_notificationSet;
                            model.Chart_Instance_Id = "0";
                        }
                        if (InParam.SendNotifictionTypeSampleOrNow.Equals("Now"))
                        {
                            int Queue_count = BASE._Form_dbops.Insert_Form_Response_Notifications(InParam, Int32.Parse(model.Chart_Instance_Id));                            
                            if (Queue_count > 0)
                            {
                                jsonParam.message = "Email Notification queued successfully. Queue Count is (" + Queue_count+ ")";
                                jsonParam.title = "Information...";
                                jsonParam.result = true;
                            }                            
                            else if (Queue_count == 0)
                            {
                                jsonParam.message = "Queue Not created.";
                                jsonParam.title = "Information...";
                                jsonParam.result = true;
                            }
                        }
                        else if (InParam.SendNotifictionTypeSampleOrNow.Equals("Sample"))
                        {
                            BASE._Form_dbops.Insert_Sample_Form_Response_NotificationSettings(InParam, Convert.ToInt32(model.Chart_Instance_Id));
                            jsonParam.message = "Sample email successfully set up to Admin Email.";
                            jsonParam.title = "Information...";
                            jsonParam.result = true;
                        }
                    }
                    else if (model.SelectedTabTitle_Notify_ChartResponses.Equals("SMS"))
                    {
                        DataTable dt = BASE._Form_dbops.Check_Unique_BatchName(model.BatchName_Email_Notify_ChartResponses, model.SelectedTabTitle_Notify_ChartResponses);
                        if (Int32.Parse(dt.Rows[0][0].ToString()) == 1)
                        {
                            jsonParam.message = "Batch Name is not Unique. Please enter the Unique Batch name.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        Common_Lib.DbOperations.Forms.Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                        InParam.Mode = model.SelectedTabTitle_Notify_ChartResponses;
                        InParam.Content = model.Content_SMS_Notify_ChartResponses;
                        //if (InParam.SendNotifictionTypeSampleOrNow.Equals("Now"))
                        //{
                        //    if (BASE._Form_dbops.Insert_Form_Response_Notifications(InParam, Int32.Parse(model.Chart_Instance_Id)) > 0)
                        //    {
                        //        jsonParam.message = "SMS Notification queued successfully.";
                        //        jsonParam.title = "Information...";
                        //        jsonParam.result = true;
                        //    }
                        //}
                        //else if (InParam.SendNotifictionTypeSampleOrNow.Equals("Sample"))
                        //{
                        //    BASE._Form_dbops.Insert_Sample_Form_Response_NotificationSettings(InParam, Convert.ToInt32(model.Chart_Instance_Id));
                        //    jsonParam.message = "Your SMS Notification has been queued to Admin Mobile no. Please confirm same is OK.";
                        //    jsonParam.title = "Information...";
                        //    jsonParam.result = true;
                        //}
                        //InParam.Content = model.Content_SMS_Notify_ChartResponses;
                        //if (BASE._Form_dbops.Insert_SMS_Form_Response_Notifications(InParam, Int32.Parse(model.Chart_Instance_Id)) > 0)
                        //{
                        //    jsonParam.message = "SMS Notification queued successfully.";
                        //    jsonParam.title = "Information...";
                        //    jsonParam.result = true;
                        //}
                    }

                    else if (model.SelectedTabTitle_Notify_ChartResponses.Equals("WhatsappPaid"))
                    {
                        //DataTable dt = BASE._Form_dbops.Check_Unique_BatchName(model.BatchName_Whatsapp_Notify_ChartResponses, model.SelectedTabTitle_Notify_ChartResponses);
                        //if (Int32.Parse(dt.Rows[0][0].ToString()) == 1)
                        //{
                        //    jsonParam.message = "Batch Name is not Unique. Please enter the Unique Batch name.";
                        //    jsonParam.title = "Information...";
                        //    jsonParam.result = false;
                        //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        //}
                        Common_Lib.DbOperations.Forms.Param_FormResponse_Notification InParam = new Param_FormResponse_Notification();
                        InParam.AdminEmail = model.AdminEmail_Notify_ChartResponses;
                        InParam.AdminWhatsAppNo = model.SampleWhatsappNumber_paid_CustomNotification;
                        if (model.SetNotificationTo_Radio == "formresponses")
                        {
                            InParam.ChartInstanceId = model.Chart_Instance_Id;
                        }
                        InParam.ResponseStatus = model.ResponseStatus_Notify_ChartResponses;
                        //if (model.File_Whatsapp_Notify_ChartResponses != null && model.File_Whatsapp_Notify_ChartResponses.ContentLength > 0)
                        //{
                        //    BinaryReader reader = new BinaryReader(model.File_Whatsapp_Notify_ChartResponses.InputStream);
                        //    byte[] imageBytes = reader.ReadBytes(model.File_Whatsapp_Notify_ChartResponses.ContentLength);
                        //    reader.Close();
                        //    reader.Dispose();
                        //    string FileName = CommonFunctions.TransformFileName(model.File_Whatsapp_Notify_ChartResponses.FileName, imageBytes, true);
                        //    var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        //    if (!regex.IsMatch(FileName))
                        //    {
                        //        jsonParam.message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + FileName;
                        //        jsonParam.title = "Information";
                        //        jsonParam.result = false;
                        //        return Json(new
                        //        {
                        //            jsonParam
                        //        }, JsonRequestBehavior.AllowGet);
                        //    }
                        //    InParam.File = imageBytes;
                        //    InParam.File_Name = ConfigurationManager.AppSettings["attachmentpath"] + FileName;
                        //}
                        InParam.BatchName = model.BatchName_WhatsappPaid;
                        InParam.Content = model.Content_WhatsappPaid_customNotification;//.ConvertHtmlToWhatsappText();
                        InParam.DeliverySpeed = model.DeliverySpeed_Whatsapp_Notify_ChartResponses;
                        InParam.SenderWhatsappNoType = model.Radio_SenderWhatsappNumber_Notify_ChartResponses;
                        if (InParam.SenderWhatsappNoType.Equals("General"))
                        {
                            InParam.Whatsappno = ConfigurationManager.AppSettings["DefaultWhatsAppSender"];
                        }
                        else if (InParam.SenderWhatsappNoType.Equals("Private"))
                        {
                            InParam.Whatsappno = model.SenderWhatsapp_Notify_ChartResponses;
                        }
                        InParam.SendNotifictionTypeSampleOrNow = model.SendNotificationButtonPressed_Notify_ChartResponses;
                        InParam.Mode = model.SelectedTabTitle_Notify_ChartResponses;
                        if (model.SetNotificationTo_Radio == "formresponses")
                        {
                            InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            if (model.selectedFormResponses_group == "predefined_responses")
                            {
                                InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            }
                            else
                            {
                                InParam.selectedFormResponses_group = model.selectedFormResponses_group;
                                InParam.selectedFormResponses_str = model.selectedFormResponses_str;
                            }
                        }
                        else if (model.SetNotificationTo_Radio == "addressbook")
                        {
                            InParam.selectedAddresses_group = model.selectedAddresses_group;
                            InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                            model.Chart_Instance_Id = "0";
                            if (model.selectedAddresses_group == "predefined_addresses")
                            {
                                InParam.SelectedDesignationIds = model.SelectedDesignationIds_notificationSet;
                                InParam.SelectedOccupationIds = model.SelectedOccupationIds_notificationSet;
                                InParam.SelectedCategoryIds = model.SelectedCategoryIds_notificationSet;
                                InParam.SelectedMagazineIds = model.SelectedMagazineIds_notificationSet;
                                InParam.SelectedSpecialitiesIds = model.SelectedSpecialitiesIds_notificationSet;
                                InParam.SelectedEventsIds = model.SelectedEventsIds_notificationSet;
                                InParam.SelectedTitleIds = model.SelectedTitleIds_notificationSet;
                                InParam.SelectedDistrictIds = model.SelectedDistrictIDs_notificationSet;
                                InParam.SelectedCityIds = model.SelectedCityIDs_notificationSet;
                                InParam.SelectedWingIds = model.SelectedWingMemberIds_notificationSet;
                            }
                            else
                            {
                                InParam.selectedAddresses_group = model.selectedAddresses_group;
                                InParam.selectedContacts_str = model.selectedContacts_str;
                            }
                        }
                        
                        if (model.IsPublic == true) //for whatsapp public page 
                        {
                            if (string.IsNullOrWhiteSpace(model.Public_hardcoded_AttachmentPath) == false)
                            {
                                InParam.File_Name = model.Public_hardcoded_AttachmentPath;
                            }
                            if (string.IsNullOrWhiteSpace(model.Public_Addby) == false)
                            {
                                InParam.AddBy = model.Public_Addby;
                            }
                            if (model.Public_CentreID > 0)
                            {
                                InParam.CenID = model.Public_CentreID;
                            }
                        }
                        //InParam.SendNotifictionTypeSampleOrNow = "";
                        if (InParam.SendNotifictionTypeSampleOrNow.Equals("Now"))
                        {
                            if (model.SetNotificationTo_Radio == "phonenumbers")
                            {
                                //InParam.SelectedNotificationRadio = model.SetNotificationTo_Radio;
                                //InParam.enteredPhoneNumbersOrEmails_str = model.EnteredPhoneNumbersOrEmails_notificationSet;
                                //model.Chart_Instance_Id = "0";

                                string[] phoneNumberArray = model.EnteredPhoneNumbersOrEmails_notificationSet.Split(',');
                                foreach (string phoneNumber in phoneNumberArray)
                                {
                                    if (model.withorwithoutMedia_whatsappPaid == "withMedia")
                                    {
                                        var result = await new WhatsappCloudAPIController().sendMessageTemplate_Media(phoneNumber, model.accessToken_whatsappPaid,
                                            model.TemplateName_WhatsappPaid_CustomNotification, model.phoneNumberId_whatsappPaid, model.language_whatsappPaid,
                                            model.File_WhatsappPaid_customNotification);

                                        if (result is ContentResult contentResult)
                                        {
                                            string resultString = contentResult.Content;
                                            // Process resultString here                                        
                                            BASE._Form_dbops.wpaidDeliveryLog(Guid.NewGuid().ToString(), phoneNumber, model.BatchName_WhatsappPaid, "", model.Content_WhatsappPaid_customNotification);
                                            
                                        }
                                    }
                                    else if (model.withorwithoutMedia_whatsappPaid == "withoutMedia")
                                    {
                                        var result = await new WhatsappCloudAPIController().SendMessageTemplate(phoneNumber, model.accessToken_whatsappPaid,
                                            model.TemplateName_WhatsappPaid_CustomNotification, model.phoneNumberId_whatsappPaid, model.language_whatsappPaid);
                                        // If the result is ContentResult
                                        if (result is ContentResult contentResult)
                                        {
                                            string resultString = contentResult.Content;
                                            // Process resultString here
                                            BASE._Form_dbops.wpaidDeliveryLog(Guid.NewGuid().ToString(), phoneNumber, model.BatchName_WhatsappPaid, "", model.Content_WhatsappPaid_customNotification);
                                        }
                                    }
                                    // Perform operations for each phone number

                                }

                            }
                            else if (model.SetNotificationTo_Radio == "addressbook" || model.SetNotificationTo_Radio == "formresponses")
                            {                                
                                DataTable phone_nosdt = BASE._Form_dbops.get_phonenos_ofBatch_Wpaid(InParam, Int32.Parse(model.Chart_Instance_Id));
                                if (phone_nosdt.Rows.Count > 0)
                                {
                                    for(int i = 0; i<phone_nosdt.Rows.Count; i++)
                                    {
                                        if (model.withorwithoutMedia_whatsappPaid == "withMedia")
                                        {
                                            if (phone_nosdt.Rows[i][0].ToString() != "" || phone_nosdt.Rows[i][0].ToString() != null)
                                            {
                                                var result = await new WhatsappCloudAPIController().sendMessageTemplate_Media(phone_nosdt.Rows[i][0].ToString(), 
                                                    model.accessToken_whatsappPaid, model.TemplateName_WhatsappPaid_CustomNotification, model.phoneNumberId_whatsappPaid,
                                                    model.language_whatsappPaid, model.File_WhatsappPaid_customNotification);

                                                if (result is ContentResult contentResult)
                                                {
                                                    string resultString = contentResult.Content;
                                                    // Process resultString here                                        
                                                    BASE._Form_dbops.wpaidDeliveryLog(Guid.NewGuid().ToString(), phone_nosdt.Rows[i][0].ToString(), model.BatchName_WhatsappPaid, "", model.Content_WhatsappPaid_customNotification);

                                                }
                                            }
                                        }
                                        else if (model.withorwithoutMedia_whatsappPaid == "withoutMedia")
                                        {
                                            if(phone_nosdt.Rows[i][0].ToString() != "" || phone_nosdt.Rows[i][0].ToString() != null)
                                            {
                                                var result = await new WhatsappCloudAPIController().SendMessageTemplate(phone_nosdt.Rows[i][0].ToString(), 
                                                    model.accessToken_whatsappPaid, model.TemplateName_WhatsappPaid_CustomNotification, model.phoneNumberId_whatsappPaid, 
                                                    model.language_whatsappPaid);
                                                // If the result is ContentResult
                                                if (result is ContentResult contentResult)
                                                {
                                                    string resultString = contentResult.Content;
                                                    // Process resultString here
                                                    BASE._Form_dbops.wpaidDeliveryLog(Guid.NewGuid().ToString(), phone_nosdt.Rows[i][0].ToString(), model.BatchName_WhatsappPaid, "", model.Content_WhatsappPaid_customNotification);
                                                }
                                            }
                                        }                                        
                                    }
                                    jsonParam.message = "WhatsApp Template Message sent successfully";
                                    //jsonParam.message = resultString+"WhatsApp Template Message sent successfully";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = true;
                                }
                                else if (phone_nosdt.Rows.Count == 0)
                                {
                                    jsonParam.message = "Whatsapp Template Messages not sent. Because, there are no phone numbers in the selection category";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = true;
                                }
                            }
                        }                        
                    }
                }
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_NotificationBatchQueue(string Chart_Instance_Id)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Chart_Instance_Id = Chart_Instance_Id;
            //ViewBag.RegistrationsTotalCount = RegistrationsTotalCount;
            //ViewBag.ApprovedResponseCount = ApprovedResponseCount;
            //ViewBag.RejectionResponseCount = RejectionResponseCount;
            //ViewBag.DefaultResponseCount = DefaultStatus;
            dt_chartResponseNotificationBatchQueueGrid = BASE._Form_dbops.get_Form_Response_Notifications(int.Parse(Chart_Instance_Id));
            return View(dt_chartResponseNotificationBatchQueueGrid);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_NotificationBatchQueue_Grid(string Chart_Instance_Id, string command, int ShowHorizontalBar = 1, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            if (command == "REFRESH" || dt_chartResponseNotificationBatchQueueGrid == null)
            {
                dt_chartResponseNotificationBatchQueueGrid = BASE._Form_dbops.get_Form_Response_Notifications(int.Parse(Chart_Instance_Id));
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            return View(dt_chartResponseNotificationBatchQueueGrid);
        }
        [CheckLogin]
        public ActionResult Frm_notificationSet_NotificationQueue_Info()
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Uid = BASE._open_Cen_ID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            dt_chartResponseEmailNotificationQueueGrid = BASE._Form_dbops.get_Email_Notifications_Queue();
            dt_chartResponseWhatsappNotificationQueueGrid = BASE._Form_dbops.get_WhatsApp_Notifications_Queue();
            DataSet model = new DataSet();
            model.Tables.Add(dt_chartResponseEmailNotificationQueueGrid);
            model.Tables.Add(dt_chartResponseWhatsappNotificationQueueGrid);
            return View(model);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Email_NotificationQueue_Info_Grid(string Chart_Instance_Id, string command, int ShowHorizontalBar = 1, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            if (command == "REFRESH" || dt_chartResponseEmailNotificationQueueGrid == null)
            {
                dt_chartResponseEmailNotificationQueueGrid = BASE._Form_dbops.get_Email_Notifications_Queue();
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            return View(dt_chartResponseEmailNotificationQueueGrid);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Whatsapp_NotificationQueue_Info_Grid(string Chart_Instance_Id, string command, int ShowHorizontalBar = 1, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            if (command == "REFRESH" || dt_chartResponseWhatsappNotificationQueueGrid == null)
            {
                if (BASE._open_Cen_ID == 7113)//fetch all Queues specific for CENID=7113 and UID in PROD Env: 09000/GS/002
                {
                    dt_chartResponseWhatsappNotificationQueueGrid = BASE._Form_dbops.get_All_WhatsApp_Notifications_Queue();
                }
                else { 
                    dt_chartResponseWhatsappNotificationQueueGrid = BASE._Form_dbops.get_WhatsApp_Notifications_Queue(); 
                }
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.Uid = BASE._open_Cen_ID;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            return View(dt_chartResponseWhatsappNotificationQueueGrid);
        }        
        [CheckLogin]
        public ActionResult Frm_ChartResponses_NotificationDeliveryLog_Info(DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            FromDate = FromDate == null ? DateTime.Now.AddMonths(-1) : FromDate;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            DataSet model = new DataSet();
            //dt_chartResponseEmailNotificationDeliveryLogGrid = BASE._Form_dbops.get_Email_Notifications_Delivery_Log(FromDate, ToDate);
            dt_chartResponseWhatsappNotificationDeliveryLogGrid = BASE._Form_dbops.get_Whatsapp_Notifications_Delivery_Log(FromDate, ToDate);

            model.Tables.Add(dt_chartResponseWhatsappNotificationDeliveryLogGrid);
            //model.Tables.Add(dt_chartResponseEmailNotificationDeliveryLogGrid);
            return View(model);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Whatsapp_NotificationDeliveryLog_Info_Grid(DateTime? FromDate, DateTime? ToDate, string command, int ShowHorizontalBar = 1, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            if (command == "REFRESH" || dt_chartResponseWhatsappNotificationDeliveryLogGrid == null)
            {
                dt_chartResponseWhatsappNotificationDeliveryLogGrid = BASE._Form_dbops.get_Whatsapp_Notifications_Delivery_Log(FromDate, ToDate);
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            return View(dt_chartResponseWhatsappNotificationDeliveryLogGrid);
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Email_NotificationDeliveryLog_Info_Grid(DateTime? FromDate, DateTime? ToDate, string command, int ShowHorizontalBar = 1, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {            
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            return View(dt_chartResponseEmailNotificationDeliveryLogGrid);
        }
        [CheckLogin]
        public ActionResult SendEmailLogsToEmailSchedulerQueue(string period, string recids_str)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (period == "") { period = null; }
            if (recids_str == "") { recids_str = null; }
            if((period == "" || period == null) && (recids_str == "" || recids_str == null))
            {
                jsonParam.message = "No logs to send to email queue";
                jsonParam.title = "Information...";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            
            try
            {
                BASE._Form_dbops.sendEmailLogsToEmailQueue(period, recids_str);
                jsonParam.message = "Sent The Logs to Email Queue";
                jsonParam.title = "Information...";
                jsonParam.result = true;
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult EmailNotificationDeliveryLogsData_Load(string FromDate = null, string ToDate = null)
        {
            DateTime? startdate;
            DateTime? enddate;

            startdate  = string.IsNullOrWhiteSpace(FromDate) ? (DateTime?)DateTime.Now.AddMonths(-1) : Convert.ToDateTime(FromDate); //FromDate == null ? DateTime.Now.AddMonths(-1) : FromDate;
            enddate = string.IsNullOrWhiteSpace(ToDate) ? (DateTime?)null : Convert.ToDateTime(ToDate);
            dt_chartResponseEmailNotificationDeliveryLogGrid = BASE._Form_dbops.get_Email_Notifications_Delivery_Log(startdate, enddate);
            
            return Content(JsonConvert.SerializeObject(dt_chartResponseEmailNotificationDeliveryLogGrid), "application/json");
        }
        [CheckLogin]
        public ActionResult Frm_Export_Notification_DeliveryLog(int selectedIndex)
        {
            string view = selectedIndex == 0 ? "Frm_Export_Options_Whatsapp_Notification_DeliveryLog" : "Frm_Export_Options_Email_Notification_DeliveryLog";
            return View(view);
        }
        [CheckLogin]
        public ActionResult Frm_Export_Notification_Queue(int selectedIndex)
        {
            string view = selectedIndex == 0 ? "Frm_Export_Options_Whatsapp_Notification_Queue" : "Frm_Export_Options_Email_Notification_Queue";
            return View(view);
        }        
        [CheckLogin]
        public ActionResult Delete_ChartResponses_Notificaion_Queue(string RecId, string Notification_Type)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string[] ids = RecId.Split(',');
            short counter = 0;
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (Notification_Type.Equals("Whatsapp"))
                    {
                        BASE._Form_dbops.Delete_Form_Response_Notification_Queue(ids[i], "Whatsapp");
                    }
                    else if (Notification_Type.Equals("Email"))
                    {
                        BASE._Form_dbops.Delete_Form_Response_Notification_Queue(ids[i], "Email");
                    }
                    //else if (Notification_Type.Equals("SMS"))
                    //{
                    //    BASE._Form_dbops.Delete_Form_Response_Notification_Queue(ids[i], "SMS");  
                    //}
                    counter = (short)i;
                }
                if (counter + 1 == ids.Length)
                {
                    jsonParam.message = ids.Length + " " + Notification_Type + " Notification Queue deleted Successfully.";
                    jsonParam.title = "Information...";
                    jsonParam.result = true;
                }
                else
                {
                    jsonParam.message = "Unable to delete all " + ids.Length + " Notification Queue. Please right click and refresh then retry.";
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                }

            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Delete_ChartResponses_NotificaionBatchQueue(string RecId, string Notification_Type)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                BASE._Form_dbops.Delete_Form_Response_Notification_Setting(RecId);
                jsonParam.message = "Selected Batch and its Queue is Deleted Successfully From Pending queue.";
                jsonParam.title = "Information...";
                jsonParam.result = true;
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Frm_FormResponsesGrid_notificationSet()
        {
            bool isApprovalRequired = false;
            ViewBag.Purpose = "";
            ViewBag.isApprovalRequired = isApprovalRequired;
            return View();
        }
        [CheckLogin]
        public ActionResult FormResponsesData_Load(string chartInstanceID = null, string regCenId = null, Boolean isChartResponseMedicalView = false, string questionFilter = "")
        {
            DataTable dt = null;
            int typeCastedIntVal;
            if (isChartResponseMedicalView)
            {
               dt = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), Int32.Parse(regCenId), questionFilter);
               
            }
            if (string.IsNullOrWhiteSpace(regCenId))
            {
                dt = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), null, questionFilter);
            }
            else
            {
                if (int.TryParse(regCenId, out typeCastedIntVal) && isChartResponseMedicalView == false)//If Centre is not in Form but trying to edit Response then it will give "false" as CenId
                {
                    dt = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), typeCastedIntVal, questionFilter);
                }
                else
                {

                    dt = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), null, questionFilter);
                }
            }
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }
        [CheckLogin]
        public ActionResult Frm_AddressBookGrid_notificationSet()
        {
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_AddressBook).ToString()) ? 1 : 0;
            ViewBag.VouchingMode = false;
            return View();
        }
        [CheckLogin]
        public ActionResult Frm_AddressBook_notificationSet_GridData()
        {
            bool ShowAttachmentIndicator = false;
            bool ShowVouchingIndicator = false;
            DataTable TP_Table = BASE._Address_DBOps.GetAddressBookListing(ShowAttachmentIndicator, ShowVouchingIndicator);
            List<Address_Book> addressBook = new List<Address_Book>();

            foreach (DataRow row in TP_Table.Rows)
            {
                Address_Book newdata = new Address_Book();

                newdata.ID = row["ID"].ToString();
                newdata.Title = row["Title"].ToString();
                newdata.Name = row["Name"].ToString();
                newdata.Organization = row["Organization"].ToString();
                newdata.Designation = row["Designation"].ToString();//Redmine Bug #131387 fixed
                newdata.Occupation = row["Occupation"].ToString();
                newdata.Education = row["Education"].ToString();
                newdata.PinCode = row["PinCode"].ToString();
                newdata.City = row["City"].ToString();
                newdata.State = row["State"].ToString();
                newdata.Country = row["Country"].ToString();
                newdata.Passport_No = row["Passport No."].ToString();
                newdata.PAN_No = row["PAN No."].ToString();
                newdata.VAT_TIN_No = row["VAT TIN No."].ToString();
                newdata.CST_TIN_No = row["CST TIN No."].ToString();
                newdata.GST_TIN_No = row["GSTIN No."].ToString();
                newdata.TAN_No = row["TAN No."].ToString();
                newdata.UID_No = row["UID No."].ToString();
                newdata.Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString();
                newdata.Voter_ID = row["VOTER ID No."].ToString();
                newdata.Ration_Card_No = row["Ration Card No."].ToString();
                newdata.DL_No = row["DL No."].ToString();
                newdata.Taxpayer_ID = row["Tax ID No."].ToString();
                newdata.Address_Line1 = row["Address Line.1"].ToString();
                newdata.Address_Line2 = row["Address Line.2"].ToString();
                newdata.Address_Line3 = row["Address Line.3"].ToString();
                newdata.Address_Line4 = row["Address Line.4"].ToString();
                newdata.District = row["District"].ToString();
                newdata.Resi_Tel_No = row["Resi.Tel.No(s)"].ToString();
                newdata.Office_Tel_No = row["Office Tel.No(s)"].ToString();
                newdata.Office_Fax_No = row["Office Fax No(s)"].ToString();
                newdata.Resi_Fax_No = row["Resi.Fax No(s)"].ToString();
                newdata.Mobile_No = row["Mobile No(s)"].ToString();
                newdata.Email = row["Email"].ToString();
                newdata.Website = row["Website"].ToString();
                newdata.Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]);
                newdata.Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]);
                newdata.Blood_Group = row["Blood Group"].ToString();
                newdata.Status = row["Status"].ToString();
                newdata.BK_Title = row["BK Title"].ToString();
                newdata.BK_PAD_No = row["BK PAD No."].ToString();
                newdata.Class_At = row["Class At"].ToString();
                newdata.Centre_Category = row["Centre Category"].ToString();
                newdata.Centre_Name = row["Centre Name"].ToString();
                newdata.Category = row["Category"].ToString();
                newdata.Referene = row["Referene"].ToString();
                newdata.Remarks = row["Remarks"].ToString();
                newdata.Events = row["Events"].ToString();
                newdata.Add_By = row["Add By"].ToString();
                newdata.Add_Date = row["Add Date"].ToString();
                newdata.Edit_By = row["Edit By"].ToString();
                newdata.Edit_Date = row["Edit Date"].ToString();
                newdata.Action_Status = row["Action Status"].ToString();
                newdata.Action_By = row["Action By"].ToString();
                newdata.Action_Date = row["Action Date"].ToString();
                newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                newdata.iIcon = "";

                if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newdata.iIcon += "GreenShield|";
                }
                else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newdata.iIcon += "YellowShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newdata.iIcon += "BlueShield|";
                }
                if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedFlag|";
                }
                if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newdata.iIcon += "RequiredAttachment|";
                }
                else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newdata.iIcon += "AdditionalAttachment|";
                }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                addressBook.Add(newdata);
            }
            //Addressbook_ExportData = addressBook;
            return Content(JsonConvert.SerializeObject(addressBook), "application/json");
        }
        [CheckLogin]
        public ActionResult LookUp_Get_AllFormsList(DataSourceLoadOptions loadOptions, string eventid = null)
        {
            DataTable dt = BASE._Form_dbops.get_AllFormsList(BASE._open_Cen_ID);
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Content(json, "application/json");
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }
        [CheckLogin]
        public ActionResult LookUp_Get_Cities(DataSourceLoadOptions loadOptions)
        {
            DataTable dt = BASE._Form_dbops.get_Cities();
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Content(json, "application/json");            
        }
        [CheckLogin]
        public ActionResult LookUp_Get_Districts(DataSourceLoadOptions loadOptions)
        {
            DataTable dt = BASE._Form_dbops.get_Districts();
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Content(json, "application/json");            
        }
        [CheckLogin]
        public ActionResult LookUp_Get_Wings(DataSourceLoadOptions loadOptions)
        {
            DataTable dt = BASE._Address_DBOps.GetWings();
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Content(json, "application/json");            
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponseNotificationExport_Options()
        {
            return PartialView();
        }
        [CheckLogin]
        public void SessionClearNotificationSet()
        {
            ClearBaseSession("_notificationSet");
        }       
        public ActionResult SendWhatsApp(string No,string Msg) 
        { 
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SendWhatsApp(Notification_Chart_Responses model)
        {
            return await Frm_ChartResponses_Add_Notification(model);       
        }
        [CheckLogin]
        public ActionResult Frm_ChartResponses_Notification_ImportDataFromGoogleSheet(string url, string email)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DataTable importedData_dt = BASE._Form_dbops.ImportDataFromGoogleSheet(url, email);
            if (importedData_dt == null)
            {
                jsonParam.message = "No data to display";
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }
            else if (importedData_dt.Columns.Contains("Error"))
            {
                string errorMessage = importedData_dt.Rows[0]["Error"]?.ToString();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    jsonParam.message = errorMessage;
                    jsonParam.title = "Error";
                    jsonParam.result = false;
                }
            }
            else
            {
                jsonParam.message = JsonConvert.SerializeObject(importedData_dt);
                jsonParam.title = "Success";
                jsonParam.result = true;
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetTemplateNamesAndLanguages(DataSourceLoadOptions loadOptions)
        {
            string _baseUrl = "https://graph.facebook.com/v21.0/";
            string _accessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW";
            //string phoneNumberId = "104658105823288";
            string BusinessAccountID = "100277319602234";// "2223942904447592";// "100277319602234";// "101701759395660";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;            
            //var s = await CommonFunctions.SendTemplateMessage_WhatsAppAPIAsync("ph", "t", "f","f","s");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var url = $"{_baseUrl}{BusinessAccountID}/message_templates";
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);                    
                    return Content(JsonConvert.SerializeObject(jsonResponse["data"], Formatting.Indented), "application/json");
                }
                return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString}" });
            }
        }
    }
}