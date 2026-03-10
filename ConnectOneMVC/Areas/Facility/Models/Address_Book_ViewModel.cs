using Common_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class Address_Book_ViewModel
    {
        public string popuphead { get; set; }
        public bool DistrictCodeOffRead { get; set; }

        public bool DistrictCodeResRead { get; set; }
        public int ID { get; set; }

        [Display(Name = "Accounting Party")]
        public string Rad_Acc_Party_AB { get; set; }
        [Display(Name = "Date of Birth (Alokik)")]

        public DateTime? DE_DOB_A_AB { get; set; }
        [Display(Name = "BK Title")]

        public string Com_BK_Title_AB { get; set; }

        [Display(Name = "Blood Group")]
        public string Com_BloodGroup_AB { get; set; }
        public string Look_CategoryList_AB { get; set; }
        public string Category_Other { get; set; }
        [Display(Name = "Center Category")]
        public int? Cmb_Cen_Cagetory_AB { get; set; }
        [Display(Name = "Class Address")]

        public string Txt_Class_Add_AB { get; set; }
        [Display(Name = "Class At")]

        public string Com_Class_At_AB { get; set; }
        public string ClassCID { get; set; }
        [Display(Name = "Date Of Contact")]
        public DateTime? DE_DOC_AB { get; set; }

        [Display(Name = "Contact Mode")]
        public string Look_ConModeList_AB { get; set; }

        [Display(Name = "GST TIN NO")]
        public string GST_TIN { get; set; }

        [Display(Name = "CST TIN NO")]
        public string Txt_CST_TIN_AB { get; set; }
        public DateTime DateOfSurr { get; set; }

        [Display(Name = "Designation")]
        public string Txt_Desig_AB { get; set; }

        [Display(Name = "Education")]
        public string Txt_Education_AB { get; set; }

        [Display(Name = "Email ID")]
        public string Txt_Email1_AB { get; set; }
        public string Txt_Email2_AB { get; set; }
        public List<Events> Events { get; set; }
        public string EventsOther { get; set; }

        [Display(Name = "FaceBook ID")]
        public string Txt_Facebook_AB { get; set; }

        [Display(Name = "Gender")]
        public string Rad_Gender_AB { get; set; }

        [Display(Name = "Google talk ID")]
        public string Txt_GTalk_AB { get; set; }

        [Display(Name = "Date of Birth(Lokik)")]
        public DateTime? DE_DOB_L_AB { get; set; }
        public List<Magazine> Magazine { get; set; }

        [Display(Name = "Mobile No(s)")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Mobile No...")]
        public string Txt_Mob_1_AB { get; set; }

        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Mobile No 2...")]
        public string Txt_Mob_2_AB { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string Txt_Name_AB { get; set; }

        [Display(Name = "Occupation")]
        public string Look_OccupationList_AB { get; set; }

        [Display(Name = "Office Fax No(s)")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Office Fax No...")]
        public string Txt_O_Fax_1_AB { get; set; }

        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Office Fax No 2...")]
        public string Txt_O_Fax_2_AB { get; set; }

        [Display(Name = "Office Tel.No(s)")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Office Tel No...")]
        public string Txt_O_Tel_1_AB { get; set; }
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Office Tel No...")]
        public string Txt_O_Tel_2_AB { get; set; }

        [Display(Name = "Line 1")]
        public string Txt_O_Add1_AB { get; set; }

        [Display(Name = "Line 2")]
        public string Txt_O_Add2_AB { get; set; }

        [Display(Name = "Line 3")]
        public string Txt_O_Add3_AB { get; set; }

        [Display(Name = "Line 4")]
        public string Txt_O_Add4_AB { get; set; }

        [Display(Name = "City")]
        public string GLookUp_OCityList_AB { get; set; }

        [Display(Name = "Country")]
        public string GLookUp_OCountryList_AB { get; set; }

        [Display(Name = "District")]
        public string GLookUp_ODistrictList_AB { get; set; }

        [Display(Name = "PinCode")]
        //[RegularExpression(@"^\d{6}?$", ErrorMessage = "Invalid PinCode")]
        public string Txt_O_Pincode_AB { get; set; }

        [Display(Name = "State")]
        public string GLookUp_OStateList_AB { get; set; }
        public string OrgAB_RecId { get; set; }

        [Display(Name = "Organization Name")]
        public string Txt_OrgName_AB { get; set; }
        [Display(Name = "Pad No")]

        public string Txt_PAD_No_AB { get; set; }

        [Display(Name = "PAN No")]
        //[RegularExpression(@"[A-Z]{5}\d{4}[A-Z]{1}", ErrorMessage = "* Invalid PAN No...")]
        public string Txt_PAN_No_AB { get; set; }

        [Display(Name = "Passport No")]
        public string Txt_Passport_No_AB { get; set; }
        public string Rec_ID { get; set; }
        public string Txt_Ref_AB { get; set; }
        public string Txt_Remark1_AB { get; set; }
        public string Txt_Remark2_AB { get; set; }

        [Display(Name = "Resi.Fax No(s)")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Resi.Fax No...")]
        public string Txt_R_Fax_1_AB { get; set; }

        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Resi.Fax No 2...")]
        public string Txt_R_Fax_2_AB { get; set; }

        [Display(Name = "Resi.Tel.No(s)")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Resi.Tel.No...")]
        public string Txt_R_Tel_1_AB { get; set; }

        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid Resi.Tel.No 2...")]
        public string Txt_R_Tel_2_AB { get; set; }

        [Display(Name = "Line 1")]
        public string Txt_R_Add1_AB { get; set; }

        [Display(Name = "Line 2")]
        public string Txt_R_Add2_AB { get; set; }

        [Display(Name = "Line 3")]
        public string Txt_R_Add3_AB { get; set; }

        [Display(Name = "Line 4")]
        public string Txt_R_Add4_AB { get; set; }
        [Display(Name = "City")]
        public string Res_city { get; set; }

        [Display(Name = "City")]
        public string GLookUp_RCityList_AB { get; set; }

        [Display(Name = "Country")]
        public string GLookUp_RCountryList_AB { get; set; }
        [Display(Name = "District")]
        public string GLookUp_RDistrictList_AB { get; set; }

        [Display(Name = "PinCode")]
        //[RegularExpression(@"^\d{6}?$", ErrorMessage = "Invalid PinCode")]
        public string Txt_R_Pincode_AB { get; set; }
        [Display(Name = "State")]
        public string GLookUp_RStateList_AB { get; set; }

        [Display(Name = "Skype ID")]
        public string Txt_Skype_AB { get; set; }
        public List<Specialitites> Specialities { get; set; }
        public string Special_Other { get; set; }
        public string Rad_Status_AB { get; set; }
        public string Status_Action { get; set; }

        [Display(Name = "Service Tax Registration No")]
        //[RegularExpression(@"[A-Z]{5}\d{4}[A-Z]{1}ST\d{2}1", ErrorMessage = "* Invalid Service Tax Registration No...")]
        public string Txt_STR_AB { get; set; }
        public string SubCityID { get; set; }

        [Display(Name = "TAN NO")]
        //[RegularExpression(@"[A-Z]{4}\d{5}[A-Z]{1}", ErrorMessage = "* Invalid TAN No...")]
        public string Txt_TAN_AB { get; set; }

        public string Look_TitleList_AB { get; set; }

        public string C_TITLE_NAME { get; set; }

        [Display(Name = "Twitter ID")]
        public string Txt_Twitter_AB { get; set; }

        [Display(Name = "UID NO")]
        [RegularExpression(@"^\d{12}?$", ErrorMessage = "* Invalid UID No...")]
        public string Txt_UID_AB { get; set; }

        [Display(Name = "Voter ID")]        
        public string Txt_VoterID_AB { get; set; }

        [Display(Name = "Ration Card No.")]
        public string Txt_RationCardNo_AB { get; set; }

        [Display(Name = "Driving License No.")]
        public string Txt_DLNo_AB { get; set; }

        [Display(Name = "Taxpayer Identification No")]
        public string Txt_TaxpayerID_AB { get; set; }

        [Display(Name = "VAT TIN NO")]
        [RegularExpression(@"^\d{11}?$", ErrorMessage = "* Invalid VAT TIN No...")]
        public string Txt_VAT_TIN_AB { get; set; }
        public string Txt_Website_AB { get; set; }
        public List<WingsMember> WingsMember { get; set; }
        public int YearID { get; set; }
        //public DateTime? LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }

        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public DateTime? Info_LastEditedOn { get; set; }
        public string Org_RecID { get; set; }
        public string PostSuccessFunction { get; set; }
        public string PopUpId { get; set; }
        public string Party_DataGridID { get; set; }
        public string NewFunctionName { get; set; }
        public string DropDownFunctionName { get; set; }
        #region New added             
        public string Photo_AB { get; set; }
        [Display(Name = "Center Name")]
        public string GLookUp_Cen_List_AB { get; set; }
        public string Selected_Res_CountryID { get; set; }
        public bool IsBkReadOnly { get; set; }
        public string tempCountryCode { get; set; }
        public string tempStateCode { get; set; }
        public string tempOffCountryCode { get; set; }
        public string tempOffStateCode { get; set; }
        public bool Edit_Confirm { get; set; }
        public bool New_Confirm { get; set; }
        public bool Replicate_Confirm { get; set; }
        #endregion
        public List<Title_INFO> Title_DS { get; set; }
        public List<Organization_INFO> Organization_DS { get; set; }
        public List<Title_INFO> Designation_DS { get; set; }
        public List<Title_INFO> Education_DS { get; set; }
        public List<Title_INFO> Occupation_DS { get; set; }
        public List<Country_INFO> Country_R_DS { get; set; }
        public List<DbOperations.Return_StateList> State_R_DS { get; set; }
        public List<DbOperations.Return_DistrictList> District_R_DS { get; set; }
        public List<DbOperations.Return_CityList> City_R_DS { get; set; }
        public List<Country_INFO> Country_O_DS { get; set; }
        public List<DbOperations.Return_StateList> State_O_DS { get; set; }
        public List<DbOperations.Return_DistrictList> District_O_DS { get; set; }
        public List<DbOperations.Return_CityList> City_O_DS { get; set; }
        public List<Title_INFO> ContactMode_DS { get; set; }
        public List<Title_INFO> MagazineList_DS { get; set; }
        public List<Title_INFO> Category_DS { get; set; }
        public List<Title_INFO> WingsList_DS { get; set; }
        public List<Title_INFO> EventsList_DS { get; set; }
        public List<Title_INFO> SpecialitiesList_DS { get; set; }

    }
    [Serializable]
    public class Magazine
    {

        //public string AB_Rec_ID { get; set; }

        public string Magazine_Misc_ID { get; set; }

        public bool? Selected { get; set; }

        //public string Rec_ID { get; set; }

        //public string Status_Action { get; set; }
    }
    [Serializable]
    public class WingsMember
    {
        public string AB_Rec_ID { get; set; }
        public string Rec_ID { get; set; }
        public string Status_Action { get; set; }
        public string Wings_Misc_ID { get; set; }
        public bool? Selected { get; set; }
    }
    [Serializable]
    public class Specialitites
    {
        public string AB_Rec_ID { get; set; }
        public string Rec_ID { get; set; }
        public string Specialities_Misc_ID { get; set; }
        public string Status_Action { get; set; }
        public bool? Selected { get; set; }
    }
    [Serializable]
    public class Events
    {
        public string AB_Rec_ID { get; set; }
        public string Events_Misc_ID { get; set; }
        public string Rec_ID { get; set; }
        public string Status_Action { get; set; }
        public bool? Selected { get; set; }
    }
}

