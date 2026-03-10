using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class RequestBox
    {
        [Display(Name = "My Request Detail:")]
        [Required(ErrorMessage = "My Request Detail Can not be blank...!")]
        public string Txt_Detail { get; set; }

        public string AjaxSuccessForm { get; set; }
        

        [Display(Name = "Administrator Remarks:")]
        public string Txt_Administrator { get; set; }
        public string _Send_From { get; set; }
        public int status_Action { get; set; }
        public string recID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public bool Chk_Incompleted { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public string PopUpId { get; set; }
    }
}