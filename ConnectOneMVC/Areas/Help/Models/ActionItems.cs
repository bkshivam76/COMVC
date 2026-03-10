using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class ActionItems
    {     
        public string xID { get; set; }
        public string RefRecID { get; set; }
        public string RefTable { get; set; }
        public string RefScreen { get; set; }    
        public string TitleX { get; set; }
        public string OrgRecID { get; set; }
        [Display(Name = "Status:")]
        public string Txt_Status_AI { get; set; }

        [Display(Name = "Type:")]
        public string Cmb_Type_AI { get; set; }
        [Required(ErrorMessage = "Action Title cannot be Blank")]
        [Display(Name = "Title:")]
        public string Cmd_Title_AI { get; set; }

        [Display(Name = "Description / Remarks:")]
        public string Txt_Detail_AI { get; set; }

        [Display(Name = "Added On:")]
        public DateTime? Txt_AddedOn_AI { get; set; }
        [Display(Name = "Due Till:")]
        public string Cmb_Due_On_AI { get; set; }

        [Display(Name = "Due Date:")]
        public DateTime? Txt_DueDate_AI { get; set; }
        [Display(Name = "Auditor:")]
        public string Txt_Auditor_AI { get; set; }
        [Display(Name = "Contact Person:")]
        public string Txt_Contact_Person_AI { get; set; }
        [Display(Name = "Contact No.:")]
        public string Txt_ContactNo_AI { get; set; }
        [Display(Name = "Center Remarks:")]
        public string Txt_Centre_Remarks_AI { get; set; }
        [Display(Name = "Closure Remarks:")]
        public string Txt_Closure_Remarks_AI { get; set; }
        [Display(Name = "Closed By:")]
        public string Txt_ClosedBy_AI { get; set; }
        [Display(Name = "Closed On:")]
        public DateTime? Txt_ClosedDate_AI { get; set; }     
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string MarkClosed { get; set; }
        public string TempActionMethod { get; set; }
        public DateTime? LastEditedOn { get; set; }     
        public DateTime? CurrDateTime { get; set; }
        public string GridToBeRefreshed { get; set; }
    }
}