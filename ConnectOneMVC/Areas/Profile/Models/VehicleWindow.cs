using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class VehicleWindow
    {     
        public string TitleX_VPW { get; set; } //Header
        public string SubTitleX_VPW { get; set; } //Header
        public string RAD_RegPattern_VPW { get; set; } //RadioGroup
        public string Txt_RegNo_VPW { get; set; } //TextBox: linked to RadioGroup
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public bool Chk_RC_Book_VPW { get; set; } //CheckBox
        public bool Chk_Affidavit_VPW { get; set; } //CheckBox
        public bool Chk_Will_VPW { get; set; } //CheckBox
        public bool Chk_Trf_Letter_VPW { get; set; } //CheckBox
        public bool Chk_FU_Letter_VPW { get; set; } //CheckBox
        public bool Chk_OtherDoc_VPW { get; set; } //CheckBox
        public string Txt_OtherDoc_VPW { get; set; } //TextBox: Other Documents

        [Display(Name = "Ownership")]
        [Required(ErrorMessage = "Please select the Ownership")]
        public string Cmd_Ownership_VPW { get; set; } //SelectBox

        [Display(Name = "Opening Amount")]        
        public double? Txt_VI_Amount_VPW { get; set; } // NumberBox: Opening Value        
        public string Txt_PolicyNo_VPW { get; set; } // TextBox: Policy No.               
        public string Txt_Others_VPW { get; set; } // TextBox: "Other Details"
        public DateTime? Txt_E_Date_VPW { get; set; } // DateBox: Policy Expiry         
        public DateTime? Txt_RegDate_VPW { get; set; } //DateBox: Registration Date         

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Please select the Item Name")]
        public string Look_ItemList_VPW { get; set; } //Dropdown
        public string Look_ItemList_DisplayName_VPW { get; set; } //Created to store the dispaly value of Item dropdown.

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please select the Location")]
        public string Look_LocList_VPW { get; set; } //Dropdown
        public string Cmd_Make_VPW { get; set; } //Dropdown
        public string Cmd_Model_VPW { get; set; } //Dropdown

        [Display(Name = "Owner / Incharge Name")]
        [Required(ErrorMessage = "Please select the Owner / Incharge Name")]
        public string Look_OwnList_VPW { get; set; }//Dropdown 
        public DateTime oldEditOn_VPW { get; set; } // "REC_EDIT_ON" column in Owner List Dropdown
        public string Look_InsList_VPW { get; set; } //Dropdown
        public string TextEdit1_VPW { get; set; } // Hidden Text Box 
        public string xSPID { get; set; } // Location ID         
        public string SuperUserAndAuditorCheck_VPW { get; set; }
        public string xID_VPW { get; set; }
        public bool Chk_Incompleted_VPW { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string YearID { get; set; }
        public bool? IsVehicleCarriedFwd_VPW { get; set; }

        public List<Item_INFO_Vehicle> Item_DD_Data_VPW { get; set; }
        public List<ADDRESS_BOOK> Owner_DD_Data_VPW { get; set; }
        public List<InsMISC_INFO> Insurance_DD_Data_VPW { get; set; }
        public List<ASSET_LOCATION_INFO> Location_DD_Data_VPW { get; set; }
        public List<MISC_INFO> VehicleMake_DD_Data_VPW { get; set; }
        public List<MISC_INFO> VehicleModel_DD_Data_VPW { get; set; }

    }

    [Serializable]
    public class VehicleWindow_
    {

        public string xID { get; set; }
        [Required(ErrorMessage = "Item not selected...!")]
        [Display(Name = "Item Name")]
        public string Look_ItemList { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location Not Selected...!")]
        public string Look_LocList { get; set; }
        [Display(Name = "Opening Value:")]
        public string Txt_VI_Amount { get; set; }
        [Display(Name = "Make/Company:")]
        public string Cmd_Make { get; set; }
        public string txt_OwnList { get; set; }
        public string TextEdit1 { get; set; }
        [Display(Name = "Model:")]
        public string Cmd_Model { get; set; }
        public string Desc { get; set; }
        public string TitleX { get; set; }
        [Required]
        [Display(Name = "Registration No. (New Pattern)")]
        public string RAD_RegPattern { get; set; }
        [Display(Name = "Date of Registration")]
        [Required(ErrorMessage = "Date of Registration Not Selected...!")]
        public DateTime? Txt_RegDate { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public bool Chk_Incompleted { get; set; }
        [Display(Name = "OwnerShip")]
        [Required(ErrorMessage = "OwnerShip Not Selected...!")]
        public string Cmd_Ownership { get; set; }

        [Display(Name = "Owner/Incharge Name")]
        //[Required(ErrorMessage = "Owner/Incharge Name Not Selected...!")]
        public string Look_OwnList { get; set; }

        public string OwnList_RecEditOn { get; set; }

        [Display(Name = "R.C Book")]
        public bool? Chk_RC_Book { get; set; }
        [Display(Name = "Affodavit")]
        public bool? Chk_Affidavit { get; set; }
        [Display(Name = "Will")]
        public bool? Chk_Will { get; set; }
        [Display(Name = "Transfer Letter")]
        public bool? Chk_Trf_Letter { get; set; }
        [Display(Name = "Free Use Letter")]
        public bool? Chk_FU_Letter { get; set; }
        public bool? Chk_OtherDoc { get; set; }
        [Display(Name = "Other Document")]
        public string Txt_OtherDoc { get; set; }
        [Display(Name = "Insurance Company Name")]
        [Required(ErrorMessage = "Insurance Company Name Not Selected...!")]
        public string Look_InsList { get; set; }
        [Display(Name = "Policy No")]
        [Required(ErrorMessage = " Policy No Is Required!")]
        public string Txt_PolicyNo { get; set; }
        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Expiry Date Not Selected...!")]
        public DateTime? Txt_E_Date { get; set; }

        [Display(Name = "Other Detail")]
        public string Txt_Others { get; set; }
        public string status_ActionField { get; set; }
        [Required]
        public string Txt_RegNo { get; set; }
        public string SubTitleX { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }


    }
    [Serializable]
    public class VehicleInsurance
    {
        public string xID { get; set; }
        public string Look_ItemList { get; set; }
        public string Cmd_Make { get; set; }       
        public string Cmd_Model { get; set; }
        public string Desc { get; set; }
        public string Txt_RegNo { get; set; }
        [Required(ErrorMessage = "Insurance Company Not Selected...!")]
        public string Look_InsList { get; set; }
        [Display(Name = "Policy No")]
        [Required(ErrorMessage = "Policy No. cannot be Blank...!")]
        public string Txt_PolicyNo { get; set; }
        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Date Incorrect / Blank...!")]
        public DateTime? Txt_I_Date { get; set; }
        public string Tag { get; set; }
        public Int16 Rad_Insurance { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string Screen { get; set; }
        public int Txt_InsAmt { get; set; }
        public List<InsMISC_INFO> Insurance_DD_Data { get; set; }
    }
    [Serializable]
    public class MISC_INFO
    {
        public string Name { get; set; }
    }
    [Serializable]
    public class ASSET_LOCATION_INFO
    {
        public string AL_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string Matched_Type { get; set; }
        public string Matched_Name { get; set; }
        public string Matched_Instt { get; set; }
        public decimal? Final_Amount { get; set; }
        public string Location_Name { get; set; }

    }
    [Serializable]
    public class ActionItems
    {
        public string ID { get; set; }
        public bool RowFlag1 { get; set; }
        public string xID { get; set; }
        public string RefRecID { get; set; }
        public string RefTable { get; set; }
        public string RefScreen { get; set; }
        public string Status { get; set; }
        public string TitleX { get; set; }
        public string OrgRecID { get; set; }
        [Display(Name = "Status")]
        public string Txt_Status { get; set; }

        [Display(Name = "Type")]
        public string Cmb_Type { get; set; }
        [Required(ErrorMessage = "Action Title cannot be Blank")]
        [Display(Name = "Title")]
        public string Cmd_Title { get; set; }

        [Display(Name = "Description / Remarks")]
        public string Txt_Detail { get; set; }

        [Display(Name = "Added On:")]
        public DateTime? Txt_AddedOn { get; set; }
        [Display(Name = "Due Till:")]
        public string Cmb_Due_On { get; set; }

        [Display(Name = "Due Date:")]
        public DateTime? Txt_DueDate { get; set; }
        [Display(Name = "Auditor:")]
        public string Txt_Auditor { get; set; }
        [Display(Name = "Contact Person:")]
        public string Txt_Contact_Person { get; set; }
        [Display(Name = "Contact No.:")]
        public string Txt_ContactNo { get; set; }
        [Display(Name = "Center Remarks:")]
        public string Txt_Centre_Remarks { get; set; }
        [Display(Name = "Closure Remarks:")]
        public string Txt_Closure_Remarks { get; set; }
        [Display(Name = "Closed By:")]
        public string Txt_ClosedBy { get; set; }
        [Display(Name = "Closed On:")]
        public DateTime? Txt_ClosedDate { get; set; }
        public string _Ref_Table { get; set; }
        public string _Ref_Screen { get; set; }
        public string _Ref_Rec_ID { get; set; }

        public bool Chk_Incompleted { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }

        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string CurrDateTime { get; set; }
    }
}
