using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class DepositedBankList
    {

        public string BB_BRANCH_ID { get; set; }

        public string NAME { get; set; }

        public string BRANCH { get; set; }

        public string BI_SHORT_NAME { get; set; }
    }
}