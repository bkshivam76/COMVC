using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class Magazine_DispatchType_Type
    {
        [Display(Name = "Dispatch Type:")]
        [Required(ErrorMessage = "Dispatch Type Cannot be blank...!")]
        [RegularExpression("^[a-zA-Z0-9]+[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed And Should Not Start With Special Characters")]
        public string DispatchType_Name { get; set; }
        public string ID { get; set; }
        public DateTime EditDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
    }
}