using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Payment_Frm_Asset_Window_Model
    {      
        [DefaultValue(false)]
        public bool IsGift { get; set; }
        [DefaultValue("")]
        public string Tr_M_ID { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        [Required]
        [Display(Name = "Amount:")]
        public double? Txt_Amts { get; set; }
        [Required(ErrorMessage = "Date Incorrect /Blank...!")]
        [Display(Name = "Purchase/Creation Date:")]
        public DateTime? Txt_Date { get; set; }
        [Display(Name = "Item Name:")]
        public string BE_ItemName { get; set; }
        public double? Txt_Rates { get; set; }
        [Required(ErrorMessage ="Quantity cannot be Zero / Negative...!")]
        [Display(Name = "Quantity:")]
        public double? Txt_Qtys { get; set; }   
        [Required(ErrorMessage ="Make Incorrect / Blank...!")]
        [Display(Name = "Item Make / Company:")]
        public string Txt_Make { get; set; }
        [Required(ErrorMessage ="Model Incorrect / Blank...!")]
        [Display(Name = "Item Model:")]
        public string Txt_Model { get; set; }
        [Display(Name = "Item Serial No.:")]
        public string Txt_Serial { get; set; }
        [Display(Name = "Item Warranty: (in months)")]        
        public double? Txt_Warranty { get; set; } 
        public string AI_PUR_DATE { get; set; }
        [Display(Name = "Other Details:")]
        public string Txt_Others { get; set; }
        public string AI_LOC_AL_ID { get; set; }      
        [Required(ErrorMessage = "Location not Selected...!")]
        [Display(Name = "Location:")]
        public string LocList { get; set; }

        public bool Asset_Window { get; set; }
        public List<Return_LocationList> LocationListData { get; set; }

    }
    [Serializable]
    public class PaymentVoucher_Frm_Window_Select_SelectLocationList
    {
        public string LocationName { get; set; }
        public string AL_ID { get; set; }

        public DateTime REC_EDIT_ON { get; set; }
        public string MatchedType { get; set; }
        public string MatchedName { get; set; }
        public string MatchedInstt { get; set; }
        public decimal? Final_Amount { get; set; }

    }
    [Serializable]
    public class Payment_Frm_Location_Window_Model
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Txt_Others_Location { get; set; }
        [Required(ErrorMessage = "Location Name Cannot be Blank...!")]
        [Display(Name = "Location Name:")]
        public string Txt_Name_Location { get; set; }

        public string xMAIN { get; set; }
        public string xID { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }

        public string Txt_Name_Tag { get; set; }
        public string Status_Action { get; set; }
        public string PopupID { get; set; }//Mantis bug 0000503 fixed
        public string CallingScreen { get; set; }//Mantis bug 0000505 fixed
        public string Dropdown_DataGrid { get; set; }//Mantis bug 0000505 fixed
        public string DropdownRefreshFunction { get; set; }
    }
    [Serializable]
    public class Payment_Frm_Location_Map_Window_Model
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string xLocName { get; set; }
        public string xLocRemarks { get; set; }

        public string Rad_Matching { get; set; }
        public string xID { get; set; }

        public string Look_ProList { get; set; }

        public string Look_SerList { get; set; }

        public DateTime Look_ProList_REC_EDIT_ON { get; set; }

        public DateTime Look_SerList_REC_EDIT_ON { get; set; }

        public string Status_Action { get; set; }
        public DateTime? LastEditedOn_Map { get; set; }
        public string Txt_Name_Tag_Map { get; set; }
        public List<Return_GetAllPropertyList> PropertylistData { get; set; }
        public List<ServicePlaces.Return_GetAllServicePlaceList> ServicePlaceListData{ get; set; }
    }
    [Serializable]
    public class Payment_Frm_Location_Map_Window_ProList_Model
    {
        public string LB_ID { get; set; }
        public string Institute { get; set; }

        public string TYPE { get; set; }
        public string PROP_NAME { get; set; }
        public string CATEGORY { get; set; }
        public string LED_ID { get; set; }
        public decimal CURR_VALUE { get; set; }

        public string OWNERSHIP { get; set; }
        public string USE_OF_PROPERTY { get; set; }
        public DateTime REC_EDIT_ON { get; set; }


    }
    [Serializable]
    public class Payment_Frm_Location_Map_Window_SerList_Model
    {
        public string SP_ID { get; set; }
        
        public string Institute { get; set; }
        public string Centre_No { get; set; }
        public string UID { get; set; }
        public string No { get; set; }
        public string Service_Place_Name { get; set; }
        public string Place_Type { get; set; }

        public DateTime REC_EDIT_ON { get; set; }
    }

}