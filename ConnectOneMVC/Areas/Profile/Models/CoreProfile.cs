using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class CoreProfile
    {
        [Display(Name ="Location Name : ")]
        public string LocationLabel { get; set; }
        [Display(Name ="Other Details : ")]
        [StringLength(255)]
        
        public string OtherDetailsLabel { get; set; }//Redmine Bug #133431 fixed

        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        [Required]
        public string LocationName { get; set; }

        public string AC_or_NONAC { get; set; }
        public string Category { get; set; }
        public string floor { get; set; }

        [Required]
        [StringLength(255)]        
        public string otherDetails { get; set; }//Redmine Bug #133431 fixed
        public string OldLocationName { get; set; }
        public string OldOtherDetails { get; set; }
        //--Close,--Open
        public string OpenNewPopUp { get; set; }
        public string MatchedLocationName { get; set; }
        public string MatchedOtherDetails { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "ItemName Can not be blank...!")]
        public string Property_ID { get; set; }
        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "ItemName Can not be blank...!")]
        public string ServicePlace_ID { get; set; }

        


        [Display(Name = "Registration No. (New Pattern)")]
        public string RAD_PropertyService { get; set; }
        public bool IsDisabledProp { get; set; }
        public bool IsDisabledServ { get; set; }
        public string Matching { get; set; }

        public int Result { get; set; }//0--Nothing,1--Inserted,2--Updated

        public int Max_Capacity_core { get; set; }



        //[Display(Name = "Center Start Date")]
        //public DateTime? CenterStartDate { get; set; }

        [Display(Name = "Responsible Person")]
        [Required]
        public string typeField { get; set; }
        [Display(Name = "Locations Name")]
        [Required]
        public string Locations { get; set; }
        public bool PropServRB { get; set; }
        public bool IsShow { get; set; }
        public DateTime EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public bool Chk_Incompleted { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public string locationDropdownRefreshFunction { get; set; } // Details: This property is to store the name of the dropdown refresh function if "Add Location" screen is called from other screen.
        
    }
    [Serializable]
    public class ProperyDDValues
    {
        public string Institute { get; set; }
        public string Type { get; set; }
        public string PropName { get; set; }
        public string Category { get; set; }
        public string OwnerShip { get; set; }
        public string UseOf { get; set; }
        public string Property_ID { get; set; }
    }
    [Serializable]
    public class ServiceDDValues
    {
        public string Institute { get; set; }
        public string CenterNo { get; set; }
        public string UID { get; set; }
        public string No { get; set; }
        public string ServicePlaceName { get; set; }
        public string PlaceType { get; set; }
        public string ServicePlace_ID { get; set; }
    }
    [Serializable]
    public class ResponsePerson
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
}