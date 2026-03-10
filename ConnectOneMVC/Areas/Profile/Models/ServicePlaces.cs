using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Common_Lib;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable] // This is will create a string to store the current session details in to DB
    public class ServicePlaces
    {
        public string xID { get; set; } // ID of the this specific entry 
        public string TitleX_ServicePlace { get; set; }
        public string SubTitleX_ServicePlace { get; set; }

        [Display(Name = "Place Name:")]  //This is to use for the label name of this field
        [Required(ErrorMessage = "Service Place Name Cannot be Blank..!")] //Validation and validation message        
        public string Txt_Name_ServicePlaces { get; set; }
        public string Txt_Name_Tag_ServicePlaces { get; set; }

        [Display(Name = "Place Type:")]
        [Required(ErrorMessage = "Service Place Type Not Specified..!")]
        public string Cmb_PlaceType_ServicePlaces { get; set; }

        [Display(Name = "Start Date:")]
        [Required(ErrorMessage = "Start Date Not Specified..!")]
        public DateTime? Txt_St_Date_ServicePlaces { get; set; }

        [Required(ErrorMessage = "Timings Not Specified..!")]
        [Display(Name = "Time:")]
        public DateTime? Txt_Timings_ServicePlaces { get; set; }

        [Display(Name = "Status:")]
        [Required(ErrorMessage = "Status Not Specified..!")]
        public string cmb_Status_ServicePlaces { get; set; }

        [Display(Name = "Person Name:")]
        [Required(ErrorMessage = "Owner Not Selected..!")]
        public string Look_PlaceOwner_ServicePlaces { get; set; }
        
        //public string Lbl_PlaceAddress_ServicePlaces { get; set; }
        //public string Lbl_Contact_ServicePlaces { get; set; }
        

        [Display(Name = "Person Name:")]
        [Required(ErrorMessage = "Responsible Person Not Selected..!")]
        public string Look_ResponsiblePerson_ServicePlaces { get; set; }
        public string lbl_ResponsibleContact_ServicePlaces { get; set; }
        public string Txt_OtherDetails_ServicePlaces { get; set; }
        public Int32 Txt_MaxCapacity_ServicePlaces { get; set; }
        public string ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Text_ServicePlace { get; set; }
        public DateTime LastEditedOn_ServicePlace { get; set; }
        public DateTime Info_LastEditedOn { get; set; }
        
        public bool Chk_Mon { get; set; }
        public bool Chk_Tue { get; set; }
        public bool Chk_Wed { get; set; }
        public bool Chk_Thu { get; set; }
        public bool Chk_Fri { get; set; }
        public bool Chk_Sat { get; set; }
        public bool Chk_Sun { get; set; }
        
        public int chk_Weekdays_CheckedItems()
        {
            int cnt = 0;
            if(Chk_Mon == true) { cnt += 1; }
            if(Chk_Tue == true) { cnt += 1; }
            if(Chk_Wed == true) { cnt += 1; }
            if(Chk_Thu == true) { cnt += 1; }
            if(Chk_Fri == true) { cnt += 1; }
            if(Chk_Sat == true) { cnt += 1; }
            if(Chk_Sun == true) { cnt += 1; }

            return cnt;

        }

    }

    

    [Serializable]
    public class PlaceOwners
    {
        public string NAME { get; set; }
        public string BUILDING { get; set; }
        public string HOUSE_NO { get; set; }
        public string AREA_STREET { get; set; }
        public string DISTRICT { get; set; }
        public string MOBILE { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }  

    }

    [Serializable]
    public class ResponsiblePerson
    {
        public string NAME { get; set; }
        public string BUILDING { get; set; }
        public string HOUSE_NO { get; set; }
        public string AREA_STREET { get; set; }
        public string DISTRICT { get; set; }
        public string MOBILE { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }

    }

}