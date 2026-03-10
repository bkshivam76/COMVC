using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Membership.Models
{  
    [Serializable]
    public class MRR_SpeceficPeriod
    {
        [Required(ErrorMessage = "Value must be a Date!!")]//Redmine Bug #135467 fixed
        public DateTime MRR_Fromdate { get; set; }

        [Required(ErrorMessage = "Value must be a Date!!")]//Redmine Bug #135467 fixed
        public DateTime MRR_Todate { get; set; }
    }
    [Serializable]
    public class SubscriptionList
    {
        public string SI_REC_ID { get; set; }
        public string SI_NAME { get; set; }
        public string SI_CATEGORY { get; set; }
        public int? SI_START_MONTH { get; set; }
        public int? SI_TOTAL_MONTH { get; set; }
    }
    [Serializable]
    public class MembershipNamesList
    {
        public string C_NAME { get; set; }
        public string C_ORG_ID { get; set; }
        public string C_R_ADD1 { get; set; }
        public string C_R_ADD2 { get; set; }
        public string C_R_ADD3 { get; set; }
        public string C_R_ADD4 { get; set; }
        public string CI_NAME { get; set; }
        public string ST_NAME { get; set; }
        public string DI_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string C_R_PINCODE { get; set; }
        public string TEL_NOS { get; set; }
        public string MOB_NOS { get; set; }
        public string EMAILS { get; set; }
        public string C_EDUCATION { get; set; }
        public string C_OCCUPATION { get; set; }
        public DateTime? C_DOB { get; set; }
        public string C_AGE { get; set; }
        public string CEN_NAME { get; set; }
        public string CEN_UID { get; set; }
        public DateTime? C_REC_EDIT_ON { get; set; }
        public int? C_CEN_CATEGORY { get; set; }
        public string C_CLASS_CEN_ID { get; set; }
        public string C_ID { get; set; }
    }
    [Serializable]
    public class WingList
    {
        public string WING_NAME { get; set; }
        public string WING_SHORT_MS { get; set; }
        public string WING_REC_ID { get; set; }
        public string TASK_NAME { get; set; }
        public string PERMISSION { get; set; }
        public string TASK_REF_ID { get; set; }
    }
    [Serializable]
    public class Period_Till_MRR
    {
        public int SelectedIndex_MRR { get; set; }
        public string Period_MRR { get; set; }
    }

}