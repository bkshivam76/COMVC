using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class TelephoneProfile
    {
        public string ID { get; set; }

        [Display(Name = "Telephone No.")]
        [Required(ErrorMessage = "Telephone No. Can not be blank...!")]
        public string tP_NOField { get; set; }

        public string Old_tP_NOField { get; set; }

        [Required(ErrorMessage = "Telecom Company Not Selected...!")]
        [Display(Name = "Telecom Company")]
        public string telMiscIdField { get; set; }

        [Required(ErrorMessage = "Category Not Selected...!")]
        [Display(Name = "Category")]
        public string categoryField { get; set; }

        [Display(Name = "Plan Type")]
        [Required]
        public string typeField { get; set; }

        [Display(Name = "Other Detail")]
        [MaxLength(250, ErrorMessage = "Other Detail cannot be longer than 250 characters.")]
        public string other_DetField { get; set; }
        
        public bool Chk_Incompleted { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public string TempActionMethod { get; set; }
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
    }
}