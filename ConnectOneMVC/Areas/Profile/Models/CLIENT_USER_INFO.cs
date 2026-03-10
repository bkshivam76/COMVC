using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class CLIENT_USER_INFO
    {
        public string USER_ID { get; set; }
        public string USER_PWD { get; set; }
        public string USER_ROLE_ID { get; set; }
    }
}