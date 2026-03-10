using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferPurposeList
    {
        public string PUR_NAME { get; set; }
        public string PUR_ID { get; set; }
    }
}