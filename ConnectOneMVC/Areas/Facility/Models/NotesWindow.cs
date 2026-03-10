using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class NotesWindow
    {
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string Txt_Desc_Notes { get; set; }
        public string xID { get; set; }
        public string Txt_Status_Notes { get; set; }

    }
}