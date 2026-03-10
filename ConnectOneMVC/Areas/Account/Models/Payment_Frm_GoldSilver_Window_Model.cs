using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Payment_Frm_GoldSilver_Window_Model
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        [Required]
        [Display(Name = "Type:")]
        public string Cmd_Type { get; set; }
        [Required]
        [Display(Name = "Item Name:")]
        public string BE_ItemName { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string MiscList { get; set; }
        [Required]
        [Display(Name = "Weight (gm):")]
        public double? Txt_Weight { get; set; }
        [Required(ErrorMessage = "Location Not Selected...!")]
        [Display(Name = "Location:")]
        public string LocList { get; set; }
        [Display(Name = "Other Details:")]
        public string Txt_Others { get; set; }
        public string MiscList_Text { get; set; }
        public string Tr_M_ID { get; set; }
        public List<Return_LocationList> LocationListData { get; set; }
        public List<Return_GoldSilver_MiscList> MiscListData { get; set; }

    }






}