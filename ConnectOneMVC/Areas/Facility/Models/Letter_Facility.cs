using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class LetterWindow
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethod { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }

        [Required(ErrorMessage = "Date Incorrect / Blank...!")]
        public DateTime? Txt_Date_LWF { get; set; }
        //public string Txt_Desc_Notes { get; set; }
        public string TitleX_LWF { get; set; }
        public string xFrm_Text_LWF { get; set; }


        public string xID { get; set; }

        [Required(ErrorMessage = "Institute Name Not Selected...!")]
        public string Look_InsList_LWF { get; set; }
        public List<Institute_Info> Look_InsList_Data { get; set; }


        public string Txt_Ref_LWF { get; set; }
        public string Txt_Matter_LWF { get; set; }
        public string List_Lang_LWF { get; set; }
    }

    [Serializable]
    public class Institute_Info
    {
        public string INS_NAME { get; set; }
        public string INS_ID { get; set; }
        public string INS_SHORT { get; set; }
    }

    [Serializable]
    public class LetterPrint_Dialog
    {
        public string PaperType_LPD { get; set; }
        public string Txt_TopMargin_LPD { get; set; }
        public string xTemp_ID_LPD { get; set; }
        public string xTemp_Title_LPD { get; set; }



    }
}