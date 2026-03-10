using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class CenterPurpose
    {
        public string ID { get; set; }

        [Display(Name = "Center Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int Status_Action { get; set; }
        public List<CenterPurpose> CenterPurposes { get; set; }
        public DateTime? REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public string REC_STATUS { get; set; }
        public DateTime? REC_STATUS_ON { get; set; }
        public string REC_STATUS_BY { get; set; }
    }
}
