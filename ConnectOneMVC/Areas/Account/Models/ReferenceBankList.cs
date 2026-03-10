using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class ReferenceBankList
    {
        public string BI_ID { get; set; }
        //public string RefBankList { get; set; }

        public string BI_BANK_NAME { get; set; }

        public string BI_SHORT_NAME { get; set; }
    }
}