using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class Model_PersonnelMaster_Window : AllRights
    {
        [Display(Name = "Person Name")]
        [Required(ErrorMessage = "Person Name Cannot be blank...!")]
        public string Personnel_PersonNameID { get; set; }
        [Display(Name = "Personnel Type")]
        [Required(ErrorMessage = "Person Type Cannot be blank...!")]
        public string Personnel_Type { get; set; }       
        [Display(Name = "Skill Type")]
        [Required(ErrorMessage = "Skill Type Cannot be blank...!")]
        public string Personnel_SkillTypeID { get; set; }
        [Display(Name = "Payment Mode")]
        
        public string Personnel_PaymentMode { get; set; }
        [Display(Name = "PF No.")]//Mantis bug 0001108 resolved
        [RegularExpression("^[A-Z]{2}/[0-9]{5}/[0-9]{7}$", ErrorMessage = "Not A Valid PF No. Valid Format Is XY/12345/1234567")]
        public string Personnel_PFNO { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime? Personnel_JoiningDate { get; set; }
        [Display(Name = "Leaving Date")]
        public DateTime? Personnel_LeavingDate { get; set; }
        [Display(Name = "Other Details")]
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Personnel_OtherDetails { get; set; }
        [Display(Name = "Department Name")]
        //[Required(ErrorMessage = "Department Name Cannot be blank...!")] //mantis bug 1243
        public int? Personnel_DeptID { get; set; }
        [Display(Name = "Sub Department/Store")]
        public int? Personnel_SubDeptID { get; set; }
        [Display(Name = "Designation")]
        public string Personnel_DesignationID { get; set; }       
        [Display(Name = "Contractor Name")]
        public string Personnel_ContractorID { get; set; }
        [Display(Name = "Gender")]
        public string Personnel_Gender { get; set; }
        [Display(Name = "PAN No.")]//Mantis bug 0001108 resolved
        public string Personnel_PANNo { get; set; }
        [Display(Name = "Aadhar No.")]//Mantis bug 0001108 resolved
        public string Personnel_AadharNo { get; set; }
        [Display(Name = "Date Of Birth")]
        public string Personnel_DOB { get; set; }
        [Display(Name = "Contact No.")]//Mantis bug 0001108 resolved
        public string Personnel_ContactNO { get; set; }
        public string Personnel_Name { get; set; }
        public int REC_ID { get; set; }        
        public string ActionMethod { get; set; }
        public string PostSuccessFunction { get; set; }
        public string PopupName { get; set; }
    }
    [Serializable]
    public class Model_PersonnelRates_Window
    {
        [Display(Name = "Remarks")]
     //   [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis 1359
        public string PersonnelCharges_Remarks { get; set; }
        [Display(Name = "Effective Date")]
        [Required(ErrorMessage = "Effective Date Cannot Be Blank")]
        public DateTime? PersonnelCharges_EffDate { get; set; }
        [Display(Name = "Charges")]
        [Required(ErrorMessage = "Charges Cannot Be Blank")]
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Charges Should Be Between 0.01 and 99999999999999999.99 ")]
        public double? PersonnelCharges_Charges { get; set; }       
        public string PersonnelCharges_PersonnelName { get; set; }
        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Unit Cannot Be Blank")]
        public string PersonnelCharges_UnitID { get; set; }
        public int PersonnelCharges_ChargeID { get; set; }
        public int PersonnelCharges_PersonnelID { get; set; }       
        public string ActionMethod { get; set; }  
    }
    [Serializable]
    public class Model_PersonnelMasterLeft_Window
    {
        public string PersonnelLeft_Name { get; set; }
        public int Personnelleft_PersonnelID { get; set; }      
        public DateTime? Personnelleft_JoiningDate { get; set; }
        [Display(Name = "Leaving Date")]
        [Required(ErrorMessage ="Leaving Date Cannot Be Blank")]
        public DateTime? Personnelleft_LeavingDate { get; set; }

    }
}
