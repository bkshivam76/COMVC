using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ConnectOneMVC.Models;
using static Common_Lib.DbOperations.Suppliers;

namespace ConnectOneMVC.Areas.Stock.Models
{
    //public class SupplierMasterGrid
    //{
    //    [Display(Name = "Supplier Name")]
    //    [Required(ErrorMessage = "Supplier Name Cannot be blank...!")]
    //    public string Supplier_Name { get; set; }

    //    [Display(Name = "Company Code")]
    //    [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
    //    public string Supplier_Company_Code { get; set; }

    //    [Display(Name = "Registered Number")]
    //    [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
    //    public string Supplier_Registered_Number { get; set; }


    //    [Display(Name = "Supplier Address")]
    //    public string Supplier_Address { get; set; }

    //    [Display(Name = "Contact Person")]
    //    [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
    //    public DateTime? Supplier_Contact_Person { get; set; }

    //    [Display(Name = "Country")]
    //    public string Supplier_Country { get; set; }

    //    [Display(Name = "State")]    
    //    public string Supplier_State { get; set; }


    //    [Display(Name = "City")]
    //    public string Supplier_City { get; set; }

    //    [Display(Name = "PAN Number")]
    //    public DateTime? Supplier_Pan_No { get; set; }

    //    [Display(Name = "Contact No.")]
    //    public DateTime? Supplier_ContactNo { get; set; }

    //    [Display(Name = "Email ID")]
    //    public DateTime? Supplier_Email { get; set; }

    //    [Display(Name = "Other Details")]
    //    public DateTime? Supplier_Other_Details { get; set; }
    //    public String AddedBy { get; set; }
    //    public DateTime? AddedOn { get; set; }
    //    public DateTime? EditedOn { get; set; }
    //    public String EditedBy { get; set; }
    //    public int ID { get; set; } 
    //    public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
    //    public string TempActionMethod { get; set; }

    //}
    [Serializable]
    public class SupplierMasterNEVD : AllRights
    {
        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier Name Cannot be blank...!")]
        public string Supplier_AB_ID { get; set; }

        [Display(Name = "Company Code")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
        public string Supplier_Company_Code { get; set; }

        [Display(Name = "Registered Number")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
        public string Supplier_Reg_No { get; set; }
        
        public string Supplier_PostSucessFunction { get; set; }

        [Display(Name = "Contact Person")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Please Enter Alphanumeric values only")]
        public string Supplier_Contact_Person { get; set; }
      
        public int SupplierID { get; set; }   

        [Display(Name = "Other Details")]
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]

        public string Supplier_Other_Details { get; set; }
        public string ActionMethod { get; set; }

        public List<Return_GetRegister_MainGrid> SupplierMaster_GridLoad { get; set; }
        /// <summary>
        /// Newly Added properties below
        /// </summary> //Mantis bug 0001200 fixed
        public string Supplier_State { get; set; }
        public string Supplier_Country { get; set; }
        public string Supplier_Address { get; set; }
        public string Supplier_City { get; set; }
        public string Supplier_Pan_No { get; set; }
        public string Supplier_EmailID { get; set; }
        public string Supplier_ContactNo { get; set; }

    }
    [Serializable]
    public class SupplierBankAccountDetails
    {
        public int Sr_No { get; set; }

        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "Bank Name Cannot be blank...!")]
        public string Supplier_Bank_Name_ID { get; set; }//this is used for saving ID only//Mantis bug 0000986 fixed
        public string SM_BankName { get; set; }//this is used for saving NAME only

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Branch Name Cannot be blank...!")]
        public string Supplier_Branch_Name { get; set; }

        [Display(Name = "Account No")]
        [Required(ErrorMessage = "Account No Cannot be blank...!")]
        public string Supplier_Account_No { get; set; }

        [Display(Name = "IFSC Code")]
        public string Supplier_IFSC_Code { get; set; }
        public int ID { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }

        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class SupplierItemMapping
    {
       
        [Display(Name = "Mapped Supplier")]
        [Required(ErrorMessage = "Mapped Supplier Cannot be blank...!")]
        public int? SI_Mapping_Supplier_ID { get; set; }

        [Display(Name = "Stock Item Names")]
        [Required(ErrorMessage = "Stock Item Names Cannot be blank...!")]
        public string SI_Mapping_Item_ID { get; set; }

        [Display(Name = "Remarks")]
        public string SI_Mapping_Remarks { get; set; }
        public string SI_ItemID { get; set; }
        public int ID { get; set; }
        public string ActionMethod { get; set; }

    }
    [Serializable]
    public class ItemSupplierMapping
    {
        [Display(Name = "Stock Item Names")]
        [Required(ErrorMessage = "Stock Item Names Cannot be blank...!")]
        public int? IS_Mapping_Item_ID { get; set; }


        [Display(Name = "Mapped Supplier")]
        [Required(ErrorMessage = "Mapped Supplier Cannot be blank...!")]
        public string IS_Mapping_Supplier_ID { get; set; }

      

        [Display(Name = "Remarks")]
        public string IS_Mapping_Remarks { get; set; }

        public int ID { get; set; }
        public string ActionMethod { get; set; }
        public string IS_PostSuccessFunction { get; set; }
        public string IS_PopUPId { get; set; }

    }
    [Serializable]
    public class UpdateItemSupplierMapping
    {
        public int Sr { get; set; }

        [Display(Name = "Mapped Supplier")]
        [Required(ErrorMessage = "Mapped Supplier Cannot be blank...!")]
        public int? USI_Mapping_Supplier_ID { get; set; }

        [Display(Name = "Stock Item Names")]
        [Required(ErrorMessage = "Stock Item Names Cannot be blank...!")]
        public int? UIS_Mapping_Item_ID { get; set; }

        [Display(Name = "Remarks")]
        public string UIS_Mapping_Remarks { get; set; }

        public int MappingID { get; set; }
        public string ActionMethod { get; set; }

    }
    [Serializable]
    public class SupplierMasterMappingNEVD
    {
        
        
        public string SupplierMapping_ItemCategory { get; set; }

      
        public int? SupplierMapping_Supplier { get; set; }

        public int? SupplierMapping_ItemName { get; set; }

        public string ActionMethod { get; set; }
    }


}