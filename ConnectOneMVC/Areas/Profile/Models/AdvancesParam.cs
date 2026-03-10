using Common_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class AdvancesParam
    {
        [Display(Name = "Item Name:")]
        [Required(ErrorMessage = "Item Name Not Selected...!")]
        public string ItemID { get; set; }

        [Display(Name = "Party Name:")]
        [Required(ErrorMessage = "Party Name Not Selected...!")]
        public string PartyID { get; set; }

        [Display(Name = "Given Date:")]
        public string AdvanceDate { get; set; }

        [Display(Name = "Amount:")]
        [RegularExpression(@"^(0|-?\d{0,7}(\.\d{0,2})?)$", ErrorMessage ="Amount is not in valid format. Please enter amount in format like :9999999.99")]
        [Required(ErrorMessage = "Amount can not be blank...!")]
        public decimal Amount { get; set; }

        [Display(Name = "Reason:")]
        public string Purpose { get; set; }

        [Display(Name = "Other Detail:")]
        public string Remarks { get; set; }

        public string Status_Action { get; set; }

        public string RecID { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? PartyEditDate { get; set; }

        public Common.Navigation_Mode Tag { get; set; }

        public string TempActionMethod { get; set; }

        public bool Chk_Incompleted { get; set; }

    }
}