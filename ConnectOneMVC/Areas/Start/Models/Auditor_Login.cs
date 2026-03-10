using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Start.Models
{
    [Serializable]
    public class Auditor_Login
    {
        [Required(ErrorMessage = "Please Enter User Name...!")]
        public string Txt_User { get; set; }
        [Required(ErrorMessage = "Please Enter Password...!")]
        public string Txt_Pass { get; set; }
        //public bool chk_Remember { get; set; }

        public string DEVICE_TOKEN { get; set; }
        public string ANDROID_ID { get; set; }
        public string RedirectToAndroid { get; set; }
    }
}