using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class CenterPurposeList
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name Can not be blank...!")]
        public string Name { get; set; }
        [Display(Name = "Short Name : ")]
        [Required(ErrorMessage = "Short Name Can not be blank...!")]
        public string ShortName { get; set; }
        [Display(Name = "Language : ")]
        [Required(ErrorMessage = "Language not Selected...!")]
        public string Language { get; set; }

        [Display(Name = "Publish On : ")]
        [Required(ErrorMessage = "Publish On not Selected...!")]
        public string PublishOn { get; set; }
        [Display(Name = "Magazine Regd. : ")]
        public string MagazineRegd { get; set; }
        [Display(Name = "Postal Regd. No. : ")]
        public string PostalRegdNo { get; set; }

        [Display(Name = "Membership Start No.")]
        public int MembershipStart { get; set; }
        [Display(Name = "Foreign Subscriptions")]
        public string Foreign { get; set; }
        public string ID { get; set; }
        public string LangName { get; set; }
        public string LangID { get; set; }
        public string PublishName { get; set; }
        public string PublishID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }

        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
}