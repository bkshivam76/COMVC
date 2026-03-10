using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class Magazine_DispatchType_Charge
    {
        [Display(Name = "Effective Date:")]
        [Required(ErrorMessage = "Effective Date Cannot Be Blank...!")]
        public DateTime? EffectiveDate { get; set; }
        
        [Required(ErrorMessage = "Charges Cannot Be Blank...!")]
        [Display(Name ="Charges:")]
        [Range(0.01,99999999999999999.99,ErrorMessage = "Charges Should Be Between 0.01 and 99999999999999999.99")]
        public double? Charges { get; set; }
        public string DispatchType_Name { get; set; }
        public string DispatchType_ID { get; set; }
        public string DispatchCharge_ID { get; set; }
        public DateTime EditDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }

    }
}