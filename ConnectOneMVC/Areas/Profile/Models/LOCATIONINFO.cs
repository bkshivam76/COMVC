using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class LOCATIONINFO
    {
        [Display(Name = "Location Name")]
        public string LocationLabel { get; set; }
        public string OtherDetailsLabel { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        [Required]
        public string LocationName { get; set; }
        public string otherDetails { get; set; }
        public string AC_or_NonAC { get; set; }
        public string Category { get; set; }
        public string roomfloor { get; set; }
        public string ID { get; set; }
        public Int32 max_Capacity { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
    }
}