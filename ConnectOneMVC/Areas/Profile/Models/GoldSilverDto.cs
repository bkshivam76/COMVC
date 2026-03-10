using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class GoldSilverDto
    {
        public string ID { get; set; }
       
        public DateTime LastEditedOn { get; set; }
        public DateTime Info_LastEditedOn { get; set; }
        public String YearID { get; set; }
        [Display(Name = "Description:")]
        [Required(ErrorMessage = "Description Not Selected...!")]
        public string Look_MiscList { get; set; }
        public string REC_EDIT_ON { get; set; }
        public string GS_COD_YEAR_ID { get; set; }
        [Display(Name = "Type:")]
        [Required(ErrorMessage = "Type Not Selected...!")]
        public string GS_TYPE { get; set; }
        [Display(Name = "Weight(gm):")]
        public string Txt_Weight { get; set; } 

        [Display(Name = "Amount:")]
        public string Txt_Amount { get; set; }
        [Display(Name = "Item Name:")]
        [Required(ErrorMessage = "Item Name Not Selected...!")]
        public string Look_ItemList { get; set; }
       
        public string GS_DESC_MISC_ID { get; set; }
        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Location Not Selected...!")]

        public string GS_LOC_AL_ID { get; set; }
        [Display(Name = "Other Detail:")]
        public string Txt_Others { get; set; }
        public bool Chk_Incompleted { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EdiBy { get; set; }
        public DateTime EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public string ActionDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
    }
    [Serializable]
    public class Item_INFO_GS
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
}