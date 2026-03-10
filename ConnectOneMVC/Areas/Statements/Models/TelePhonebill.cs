using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Statements.Models
{
    [Serializable]
    public class TelePhonebill
    {

        [Display(Name = "TelePhoneNo.")]
        [Required(ErrorMessage = "TelePhone No Can not be blank...!")]
        public string Telephone { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "PlanType")]
        public string PlanType { get; set; }

        [Display(Name = "Period From")]
        [Required(ErrorMessage = "Period From Can not be blank...!")]
        public DateTime PeriodFrom { get; set; }

        [Display(Name = "Period To")]
        [Required(ErrorMessage = "Period To Can not be blank...!")]
        public DateTime PeriodTo { get; set; }

        public string ID { get; set; }
    }
    [Serializable]
    public class TelePhonebillinfo
    {
        public string ID { get; set; }
        public string TelePhoneNo {get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public string PlanType { get; set; }
    }

}