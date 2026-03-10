using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using Common_Lib;
using System.ComponentModel.DataAnnotations;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class Chart
    {
        public string header_image { get; set; }
        public string chartResponseStatus { get; set; }
        public string confirmation_message { get; set; }
        public string approval_message { get; set; }
        public string rejection_message { get; set; }
        public int header_width { get; set; }
        public int header_height { get; set; }
        public bool show_reg_slip { get; set; }

        //For popup data
        public string heading { get; set; }
        public string event_name { get; set; }
        public string thumbnail_image_path { get; set; }
        public string schedule_date { get; set; }
        public string schedule_time { get; set; }
        public string visitor_name { get; set; }
        public string visitor_contact { get; set; }
        public string visitor_location { get; set; }
        public string visitor_location_qrcode_url { get; set; }
        public string visitor_mobileNo { get; set; }
        public string visitor_emailID { get; set; }
        public string Organizer { get; set; }
        public string Reg_Slip_Instructions { get; set; }
        public string helpline { get; set; }
        public string registration_no { get; set; }
        public bool AlreadyRegistered { get; set; }
        public bool FromEmail { get; set; }
        public string ChartResponseId { get; set; }
        public int ChartInstanceId { get; set; }
        public string urlOrigin { get; set; }
        public string formTitle { get; set; }
        public string formDescription { get; set; }

        // Properties related to e-mail Functionality

        public string emailSubject { get; set; }
        public string emailerName { get; set; }
        public string emailId_To { get; set; }
        public string emailId_CC { get; set; }
        public string emailId_BCC { get; set; }
        public string emailId_ReplyTo { get; set; }
        public string emailSentStatusMessage { get; set; }
        public bool emailSentStatus { get; set; }

        public string Visitor_Name_Label { get; set; }
        public string Visitor_Details_Label { get; set; }
        public string Visitor_Location_Label { get; set; }
        public string Helpline_No_Label { get; set; }
        public string Organizer_Label { get; set; }
        public string Reg_Confirmation_Footer { get; set; }
    }
    [Serializable]
    public class previewResponse
    {
        public string headerImage { get; set; }
        public string formName { get; set; }
        public string formDescription { get; set; }
        public DataRow[] responseRow { get; set; }
    }
    [Serializable]
    public class ResponseMiscDetails
    {
        public string AccommodationMiscID_Multi { get; set; }
        public int BedCount { get; set; }
        public string Remarks { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public DateTime? fromtime { get; set; }
        public DateTime? totime { get; set; }
        public int TotalRegistrations { get; set; }

        public string bedcounts { get; set; }
        public string resp_ids { get; set; }
        public string registrations { get; set; }
        public string namesOfRegisteredPersons { get; set; }
        public DataTable recommendationSummaryTbl { get; set; }
    }
    [Serializable]
    public class createChartCopy
    {

        public string chartName_createCopy { get; set; }
        [AllowHtml]
        public string chartTitle_createCopy { get; set; }
        [AllowHtml]
        public string chartDescription_createCopy { get; set; }   
        public string chartID { get; set; }
        public int cenID_createCopy { get; set; }
        public DateTime? StartDate_createCopy { get; set; }
        public DateTime? EndDate_createCopy { get; set; }
        public string ServiceReport_Id { get; set; }
        public string Event_Id { get; set; }
        public string Project_ID_createCopy { get; set; }
        public string ServReport_Event_ID_createCopy { get; set; }
    }
    [Serializable]
    public class ShiftChart
    {
        public string chartName_shiftChart { get; set; }
        public string chartId_shiftChart { get; set; }
        public string cenName_shiftChart { get; set; }
        public string cenUId_shiftChart { get; set; }
        public int cenId_shiftChart { get; set; }
    }
    [Serializable]
    public class ChartVisibilityGridData 
    {
        public int Rec_ID { get; set; }
        public string Centre { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public string Institute { get; set; }
        public string AccType { get; set; }
        public string UserType { get; set; }
        public string VisibleInMenu { get; set; }
        public int? Cen_ID { get; set; }
        public string Ins_ID { get; set; }
        public string Cen_UID { get; set; }
        public string AccType_ID { get; set; }
    }
    [Serializable]
    public class ChartVisibilityInfo
    { 
        public string ChartID { get; set; }
    }
    [Serializable]
    public class ChartVisibilityWindow
    {
        public string ChartID { get; set; }
        public int? RecID { get; set; }
        public string Tag { get; set; }
        public int? Cen_ID_ChartVisibility { get; set; }
        public int? FromYear_ChartVisibility { get; set; }
        public int? ToYear_ChartVisibility { get; set; }
        public string Ins_ID_ChartVisibility { get; set; }
        public string AccType_ID_ChartVisibility { get; set; }
        public string UserType_ChartVisibility { get; set; }
        public string Menu_ChartVisibility { get; set; }
        public bool Ins_All_ChartVisibility { get; set; }
        public bool Cen_All_ChartVisibility { get; set; }
        public bool AccType_All_ChartVisibility { get; set; }
    }
    [Serializable]
    public class FyDetail
    {
        public int cod_year_id { get; set; }
        public string cod_year_name { get; set; }
        public DateTime cod_year_sdt { get; set; }
        public DateTime cod_year_edt { get; set; }
    }
    [Serializable]
    public class CentreDetail
    {
        public int cen_id { get; set; }
        public string cen_uid { get; set; }
        public string cen_name { get; set; }
        public string ins_name { get; set; }
        public string ins_short { get; set; }
    }
    [Serializable]
    public class ServiceProject
    {
        public Common.Navigation_Mode Tag { get; set; }
        [Required(ErrorMessage ="Project Name Is Mandatory")]
        public string Project_ServiceProject { get; set; }
        public string AdminID_ServiceProject { get; set; }
        public DateTime? FromDate_ServiceProject { get; set; }
        public DateTime? ToDate_ServiceProject { get; set; }
        public bool Status_ServiceProject { get; set; }
        public string Rec_ID { get; set; }
        public string TempActionMethod { get; set; }
    }
    [Serializable]
    public class ProjectNotification
    {    
        public string Title_ProjectNotification { get; set; }
        public string SubTitle_ProjectNotification { get; set; }
        public string URL_ProjectNotification { get; set; }
        public string ImageURL_ProjectNotification { get; set; }    
        public string ProjID { get; set; }
        public string ProjName { get; set; }
        public string SelectedUserRecID { get; set; }
        public int schedule_ID { get; set; }
        public DateTime? FromDate_ScheduleInstance { get; set; }
        public DateTime? ToDate_ScheduleInstance { get; set; }


    }

    [Serializable]
    public class BulkAllottment
    {
        public Int64 instanceid { get; set; }
        public Int64 recid_blk { get; set; }
        public string actionmethod {get; set;}
        public string buildingid {get; set;}
        public string acNonac {get; set;}
        public string category {get; set;}
        public Int32 roomCapacity {get; set;}
        public Int32 BedCount {get; set;}
        public Int32 totalregistrations {get; set;}
        public Int32 alreadyallottedCount {get; set;}
        public Int32 remainingCount {get; set;} 
        public DateTime? FromDate_Blk { get; set; }
        public DateTime? ToDate_Blk { get; set; }
        public DateTime? FromTime_Blk { get; set; }
        public DateTime? ToTime_Blk { get; set; }

    }
    [Serializable]
    public class BulkAllottment_Recommendation
    {
        public Int64 instanceid { get; set; }
        public Int64 recid_rcmnd { get; set; }
        public string actionmethod {get; set;}
        public string buildingid {get; set;}
        public string acNonac {get; set;}
        public string category {get; set;}
        public Int32 roomCapacity {get; set;}
        public Int32 BedCount {get; set;}
        public Int32 totalregistrations {get; set;}
        public Int32 alreadyallottedCount {get; set;}
        public Int32 remainingCount {get; set;} 
        public DateTime? FromDate_rcmnd { get; set; }
        public DateTime? ToDate_rcmnd { get; set; }
        public DateTime? FromTime_rcmnd { get; set; }
        public DateTime? ToTime_rcmnd { get; set; }

    }

    [Serializable]
    public class User_Profile_Chart_Responses_ViewModel
    {
        public string AB_ID { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public string BK_TITLE { get; set; }
        public string Centre { get; set; }
        public string Centre_Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Course { get; set; }
        public string DATE_OF_COURSE { get; set; }
        public string DOB { get; set; }
        public string Education { get; set; }
        public string Email1 { get; set; }
        public string Gender { get; set; }
        public string ID_NAME { get; set; }
        public string ID_NO { get; set; }
        public string Mob1 { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string PIN { get; set; }
        public string ProfilePicPath { get; set; }
        public string RESP_ID { get; set; }
        public string Specialities { get; set; }
        public string State { get; set; }
        public string ServiceReportID { get; set; }
        public string UserID { get; set; }
    }
}