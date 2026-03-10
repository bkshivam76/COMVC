using System;
using System.ComponentModel.DataAnnotations;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class Model_Store_Dept_Master
    {
        [Display(Name = "Store/Dept Name:")]
     //   [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        [Required(ErrorMessage = "Store/Main Dept./Sub Dept. Name Cannot Be Blank...!")]
        public string Store_Dept_Name { get; set; }
        [Display(Name = "Category:")]
        [Required(ErrorMessage = "Category Cannot Be Blank...!")]
        public string Store_Category { get; set; }
        [Display(Name = "Connecting Main Dept:")]
        public int? Store_Main_DeptID { get; set; }
        [Display(Name = "Connecting Sub Dept:")]
        public int? Store_Sub_DeptID { get; set; }
        [Display(Name = "Registration No/Store Number:")]
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        [Required(ErrorMessage = "Registration/Store Number Cannot Be Blank...!")]
        public string Store_Registeration_No { get; set; }
        [Display(Name = "In-Charge Name:")]
        //[Required(ErrorMessage = "In-Charge Name Cannot Be Blank...!")]
        public int? Store_InchargeID { get; set; }
        [Display(Name = "Contact No:")]
        public string Store_Contact_No { get; set; }
        [Display(Name = "Contact Person:")]
        public int? Store_ContactPersonID { get; set; }
        [Display(Name = "Premises Type:")]
        [Required(ErrorMessage = "Premises Type Cannot Be Blank...!")]
        public string Store_PremesisType { get; set; }
        [Display(Name = "Premises Name:")]
        [Required(ErrorMessage = "Premises Name Cannot Be Blank...!")]
        public string Store_PremesisNameID { get; set; }
        [Display(Name = "Is Central Store?")]
        public bool? Store_IsCentralStore { get; set; }
        [Display(Name = "Remarks:")]
        //  [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Store_Remarks { get; set; }
        [Display(Name = "Mapped Locations:")]
        public string Store_MappedLocations { get; set; }
        [Display(Name = "Address:")]
        public string Store_Dept_Address { get; set; }
        [Display(Name = "State:")]
        public string Store_Dept_State { get; set; }
        [Display(Name = "City:")]
        public string Store_Dept_City { get; set; }
        public int ID { get; set; }
        public string ActionMethod { get; set; }
        public string PostSuccessFunction { get; set; }
        public string PopupName { get; set; }
    }

    [Serializable]
    public class Store_Dept_Master_Closure
    {

        [Display(Name = "Store/Dept. Name:")]
        public string Store_Dept_Name { get; set; }
        public int SD_ID { get; set; }
        [Display(Name = "Closure Date:")]
        [Required(ErrorMessage = "Closure Date Cannot Be Blank...!")]
        public DateTime SD_Close_Date { get; set; }
        [Display(Name = "Reason for Closure:")]
        //    [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string SD_Close_Remarks { get; set; }
    }

}

