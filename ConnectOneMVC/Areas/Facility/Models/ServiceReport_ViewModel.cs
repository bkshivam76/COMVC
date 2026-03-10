using Common_Lib;
using ConnectOneMVC.Areas.Help.Models;
using DataAnnotationsExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class ServiceReport_ViewModel
    {
        public string ActionMethod { get; set; }
        //[Required(ErrorMessage = "Project Name Not Selected...!")]
        [Display(Name = "Project Name:")]
        public string Look_ProjList_GSR { get; set; }
        [Required(ErrorMessage = "Program Name cannot be Blank...!")]
        [Display(Name = "Program Name:")]
        public string Txt_ProgName_GSR { get; set; }
        [Required(ErrorMessage = "Program Type Not Selected...!")]
        public string Look_ProgType_GSR { get; set; }
        [Required(ErrorMessage = "Program Venue Cannot Be Blank...!")]
        [Display(Name = "Program Venue:")]
        public string Txt_ProgVenue_GSR { get; set; }
        public string Chk_WingBasedFlag_GSR { get; set; }
        [Required(ErrorMessage = "From Date Incorrect / Blank...!")]
        public DateTime? DE_FR_DT_GSR { get; set; }
        [Required(ErrorMessage = "To Date Incorrect / Blank...!")]
        public DateTime? DE_TO_DT_GSR { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }     
        public string TitleX_GSR { get; set; }
        public string xID_GSR { get; set; }
        public string PopupName_GSR { get; set; }

        public Common.Navigation_Mode Tag { get; set; }
        [Required(ErrorMessage = "From Time Incorrect / Blank...!")]
        public DateTime? Txt_Fr_Time_GSR { get; set; }
        [Required(ErrorMessage = "To Time Incorrect / Blank...!")]
        public DateTime? Txt_To_Time_GSR { get; set; }
        public string Txt_Period_Time_GSR { get; set; }
        public string Txt_NewsLink_GSR { get; set; }
        [Display(Name = "No. of Beneficiaries:")]      
        public int? Txt_ProgBeneficiaries_GSR { get; set; }

        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestName_1_GSR { get; set; }
        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestDesig_1_GSR { get; set; }
        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestName_2_GSR { get; set; }
        public string Txt_GuestDesig_2_GSR { get; set; }
        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestName_3_GSR { get; set; }
        public string Txt_GuestDesig_3_GSR { get; set; }
        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestName_4_GSR { get; set; }
        public string Txt_GuestDesig_4_GSR { get; set; }
        //[RegularExpression(" ^[a - zA - Z] + $")]
        public string Txt_GuestName_5_GSR { get; set; }
        public string Txt_GuestDesig_5_GSR { get; set; }
        public string Txt_ProgSpeaker_GSR { get; set; }

        public string Txt_ProgBrief_GSR { get; set; }
        public string Txt_ProgSpecialMom_GSR { get; set; }
        public string Txt_CulturalProg_GSR { get; set; }
        public string Txt_ProgFollowUp_GSR { get; set; }
        public string Txt_ProgFeedback_GSR { get; set; }
        public string Txt_VVIPTestimonial_GSR { get; set; }
        public string Txt_MediaLink_GSR { get; set; }
        public int Txt_NoOfEvents_GSR { get; set; }
        public List<WingsList> Wings_List_GSR { get; set; }
        public List<ProjectNameList> ProjectNameList { get; set; }
        public List<WingsList> WingListData { get; set; }
        public string ReporterCenID { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterMobile { get; set; }
        public string DocumentNameID { get; set; }
        public string Sender { get; set; }
        public string PreSelectedWing { get; set; }
        [Required(ErrorMessage = "Master Project Not Selected...!")]
        public string Look_MasterProject_GSR { get; set; }
        public int? Txt_OnlineProgBeneficiaries_GSR { get; set; }
        public int Txt_TotalProgBeneficiaries_GSR { get; set; }
        public List<ProjectNameList> MasterProjectNameList { get; set; }
        public string SelectedActivities_GSR { get; set; }
        public int? Txt_DeaddictionPledgeCount_GSR { get; set; }        
        public int? Txt_BloodDonationUnitsCount_GSR { get; set; }       
        public int? Txt_AwardsReceivedCount_GSR { get; set; }
        public string Txt_Organiser_GSR { get; set; }
        public string ProgType_ID_GSR { get; set; }
        public string ProgType_Text_GSR { get; set; }
        public List<ProgramTypeList> ProgType_Data { get; set; }
        public string ProgramPictures_GSR { get; set; }
        public string ProgramPressRelease_GSR { get; set; }
        public string ProgramBanner_GSR { get; set; }
        public string ProgramPromo_GSR { get; set; }
        public string Look_ProgOccasion_GSR { get; set; }
        public List<ProgramOccasionList> ProgOccasion_Data { get; set; }          
        public string ProgOccasion_Text_GSR { get; set; }
        public Model_Attachment_Window attachment_Window { get; set; }
        public List<SR_AttachmentDetails> Attachment_Details { get; set; }
        public string Look_OrganisedFor_GSR { get; set; }
        public string Look_ParticipantType_GSR { get; set; }
        public string Look_ParticipantCategory_GSR { get; set; }
        public string Look_ParticipantSubCategory_GSR { get; set; }
        public string Look_ProgramType_GSR { get; set; }
        public string Look_ProgramLocationType_GSR { get; set; }
        public string Look_ProgramCoordinator1_GSR { get; set; }
        public string Look_ProgramCoordinator2_GSR { get; set; }
        public bool InsertAdditonalinfo_GSR { get; set; }        
        public string Coll_CEN_ID_GSR { get; set; }  
        public List<ProgramOrganiserNames> organiserNames_GSR { get; set; }
        //[Required(ErrorMessage = "Institute Not Selected...!")]
        public string Institute_GSR { get; set; }
        public List<Service_Report_Institute> Institutes_GSR { get; set; }
        public bool Is_InstituteName_GodlyService_GSR { get; set; }

        public int? Txt_ChildBeneficiaries_GSR { get; set; }
        public int? Txt_YouthBeneficiaries_GSR { get; set; }
        public int? Txt_MaleBeneficiaries_GSR { get; set; }
        public int? Txt_FemaleBeneficiaries_GSR { get; set; }
        public int? Txt_WomenBeneficiaries_GSR { get; set; }
        public int? Txt_SrCitizenBeneficiaries_GSR { get; set; }
        public string Look_ProgVenueType_GSR { get; set; }
        public JArray VenueTypeData { get; set; }
        public string Txt_TreeLocation_GSR { get; set; }       
        public string Cen_Location_Coordinates { get; set; }
        public string Txt_MobNoVerification_GSR { get; set; }
        public string Cen_Mobile { get; set; }
        public bool IsReporterMobile_Verified { get; set; }
        public string FinalPlantationCoordinates_GSR { get; set; }
        public string FinalVerifiedMobNo_GSR { get; set; }
    }
    [Serializable]
    public class Deleted_Attachments_Data
    {
        public string[] AttachmentID { get; set; }
        public string[] FileName { get; set; }
    }
    [Serializable]
    public class ProjectNameList
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class TreePlantList
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class ProgramTypeList
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }    
    [Serializable]
    public class ProgramOccasionList
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class ProgramOrganiserNames
    {        
        public string CEN_NAME { get; set; }
    }
    [Serializable]
    public class CentreList
    {
        public int Cen_id { get; set; }
        public string Cen_bk_pad_no { get; set; } 
        public string cen_name { get; set; }
        public string cen_ins_id { get; set; }
        public string cen_uid { get; set; }
    }
    [Serializable]
    public class InsList
    {
        public int Cen_id { get; set; }      
        public string ins_id { get; set; }
        public string ins_name { get; set; }
    }
    [Serializable]
    public class WingsList
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string REC_ID { get; set; }
        public bool? Selected { get; set; }
    }
    [Serializable]
    public class ProgBanner
    {
        public string AttachmentID { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class ProgPictures
    {
        public string AttachmentID { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class ServiceEventDetails
    {
        public string ProjName { get; set; }
        public string ProgName { get; set; }
        public string Title { get; set; }
        public int StarRating { get; set; }
        public string FromToDate{get;set;}
        public string FromToTime{get;set;}
        public string Organizer { get; set; }
        public string Speaker { get; set; }
        public string SubjectTopicTheme { get; set; }
        public string Category { get; set; }
        public string AudienceType { get; set; }
        public string CenterPhone { get; set; }
        public string CenterEmail { get; set; }
        public string Links { get; set; }
        public string Venue { get; set; }
        public string CenterName { get; set; }
        public string Wings { get; set; }
        public string BannerPath { get; set; }
        public string Brief { get; set; }
        public string Guests { get; set; }
        public string Testimonials { get; set; }
        public string FollowUp { get; set; }
        public string Feedback { get; set; }
        public string SplMoment { get; set; }
        public string Cultural { get; set; }
        public List<string> AttachmentPaths { get; set; }
        public string RecID { get; set; }
        public string CenID { get; set; }
        public DateTime? LastEditOn { get; set; }
        public string Sender { get; set; }

    }
    [Serializable]
    public class TreePlantationDetails 
    {   
        public int Srno { get; set; }    
        [Display(Name = "Tree Type")]
        [Required(ErrorMessage = "Tree Type Is Required")]
        public string TreeMiscID { get; set; }   
        [Required(ErrorMessage = "Count Is Required")]
        [Range(1,Int32.MaxValue,ErrorMessage ="Tree Plantation Count Should be Greater Than 0")]
        public int Count { get; set; }    
        public string KP_PlantID { get; set; }
      
    }
    [Serializable]
    public class SR_AttachmentDetails 
    {
        public string Attachment_ID { get; set; }
        public string Attachment_FileName { get; set; }
        public string Attachment_Type { get; set; }
        public string Attachment_Description { get; set; }
        public int Attachment_Rating { get; set; }
        public string Attachment_Path { get; set; }
    }
    [Serializable]
    public class PartyList 
    {
        public string NAME { get; set; }
        public string Org { get; set; }
        public string Remarks { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime? EditOn { get; set; }
        public string ID { get; set; }

    }
}