using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Models
{
    [Serializable]
    public class BaseModel
    {
        public Guid CenterGuid { get; set; }
        public Common_Lib.Common BASE { get; set; }
    }

    [Serializable]
    public class Response_AndroidNotificationAPI
    {
        public string status { get; set; }
        public string remarks { get; set; }

    }

}