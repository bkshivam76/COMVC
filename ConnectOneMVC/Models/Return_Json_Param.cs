using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Models
{
    [Serializable]
    public class Return_Json_Param
    {
        public string innerMessage;
        public string message { get; set; }
        public string title { get; set; }
        public string focusid { get; set; }   
        public bool result { get; set; }      
        public string flag { get; set; }
        public bool closeform { get; set; }
        public bool isconfirm { get; set; }
        
        public string popup_title { get; set; }
        public string popup_form_name { get; set; }
        public string popup_form_path { get; set; }
        public string popup_querystring { get; set; }       
        public bool refreshgrid { get; set; }        
        public int Next_Unattended_Attachment_Index{ get; set; }        
        public Common_Lib.Common.DialogResult DialogResult { get; set; }
        public System.Net.Http.HttpResponseMessage response { get; set; }
        public string status { get; set; }
        public string responseMessage { get; set; }
        public bool responseState { get; set; }
        public string accountStatment { get; set; }
        public string FailRequestContent { get; set; }
        public string responseData { get; set; }
        public string encryptedRequest { get; set; }
        public string encryptedResponse { get; set; }
        public string stackTrace { get; set; }
        public string errorType { get; internal set; }
        public string innerStackTrace { get; internal set; }
    }
}