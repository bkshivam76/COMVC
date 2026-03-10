using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class OpeningBalances
    {

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "ItemName Can not be blank...!")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Head")]
        public string Head { get; set; }

        [Required]
        [Display(Name = "Head Type")]
        public string HeadType { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount Can not be blank...!")]
        public string Amount { get; set; }

        [Display(Name = "Type")]
        [Required]
        public string Type { get; set; }

        [Display(Name = "Other Details")]
        public string other_DetField { get; set; }

        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public string TR_ID { get; set; }
        public string ID { get; set; }
        public int? Remarks { get; set; }
        public string RemarkStatus { get; set; }
        public int? OpenActions { get; set; }
        public int? CrossedTimeLimit { get; set; }
        public int? YearID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }
        public bool Chk_Incompleted { get; set; }
        public string OldItemName { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public int? OTHER_ATTACH_CNT { get; set; }
        public int? ALL_ATTACH_CNT { get; set; }
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
        public string iIcon { get; set; }
        public string Special_Ref { get; set; }
    }
    [Serializable]
    public class OpeningBalancesinfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }
        public string HeadType { get; set; }
    }
}