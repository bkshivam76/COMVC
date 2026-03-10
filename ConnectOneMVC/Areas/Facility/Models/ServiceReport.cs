using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class ServiceReport
    {
        public string MISC_NAME { get; set; }
        public string SR_PROG_NAME { get; set; }
        public string SR_PROG_VENUE { get; set; }
        public DateTime? SR_From_Date { get; set; }
        public DateTime? SR_To_Date { get; set; }
        public string SR_DATE { get; set; }
        public Int32 SR_PERIOD { get; set; }
        public string SR_TIME { get; set; }
        public string SR_TIME_PER { get; set; }
        public string SR_BRIEF { get; set; }
        public string SR_SPEAKER { get; set; }
        public string SR_SPL { get; set; }
        public Int32 SR_BENEFIT { get; set; }
        public string SR_FOLLOW { get; set; }
        public string SR_FEEDBACK { get; set; }  
        public string SR_NewsLink { get; set; }  
        public string SR_Prog_Category { get; set; }  
        public string SR_VVIP_Testimonial { get; set; }  
        public string SR_Cultural { get; set; }  
        public string SR_OrganizedFor { get; set; }    
        public string SR_ParticipantType { get; set; }
        public string SR_ParticipantCategory { get; set; }
        public string SR_ParticipantSubCategory { get; set; }
        public string SR_ProgramType { get; set; }
        public string SR_LocationType { get; set; }
        public string SR_PRogCoordinator1 { get; set; }
        public string SR_PRogCoordinator2 { get; set; }
        public string ID { get; set; }
        public string Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime Action_Date { get; set; }

        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }       

        //Added for Audit Icon Filter
        public Int32? VOUCHING_ACCEPTED_COUNT { get; set; }
        public Int32? VOUCHING_PENDING_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? VOUCHING_REJECTED_COUNT { get; set; }
        public Int32? VOUCHING_TOTAL_COUNT { get; set; }
        public Int32? AUDIT_PENDING_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? AUDIT_REJECTED_COUNT { get; set; }
        public Int32? AUDIT_TOTAL_COUNT { get; set; }
        public Int32? IS_AUTOVOUCHING { get; set; }
        public Int32? IS_CORRECTED_ENTRY { get; set; }
        public string iIcon { get; set; }
        public string ReportLink { get; set; }
        public Int32? FORM_INST_ID { get; set; }
        public string VenueTypes { get; set; }
        public Int32? SR_CHILD_BENEFIT { get; set; }
        public Int32? SR_YOUTH_BENEFIT { get; set; }
        public Int32? SR_MALE_BENEFIT { get; set; }
        public Int32? SR_FEMALE_BENEFIT { get; set; }
        public Int32? SR_WOMEN_BENEFIT { get; set; }
        public Int32? SR_SENIOR_CITIZEN_BENEFIT { get; set; }

        public string Reporter_Mobile { get; set; }
        public string Reporter_Email { get; set; }
    }

    public class scheduleInfo
    {
        public string NAME { get; set; }
        public int ID { get; set; }
        public string SCHEDULE_TYPE { get; set; }
        public string FROM_TIME_1 { get; set; }
    }
    [Serializable]
    public class Service_Report_Event
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string ProjID { get; set; }
        public string Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Venue { get; set; }
        public string Cen_Name { get; set; }
        public int? Cen_ID { get; set; }
    }
    public class Service_Report_Institute
    {
        public string Ins_Name { get; set; }
        public string Ins_ID { get; set; }
        public string Ins_Short { get; set; }
    }
}