using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class CentreRemarks
    {
        public string xID { get; set; }
        [Display(Name ="Title & Description:")]
        public string xNAME_AI { get; set; }
        [Display(Name = "Centre Remarks: ")]
        public string Txt_Centre_Remarks_AI { get; set; }
  
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }

    }
}