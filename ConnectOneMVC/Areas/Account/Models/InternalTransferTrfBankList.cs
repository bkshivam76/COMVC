using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferTrfBankList
    {
        public string TRF_BI_ID { get; set; }
        public string TRF_BI_BANK_NAME { get; set; }

        public string TRF_BI_SHORT_NAME { get; set; }
        public string BA_FERA_ACC { get; set; }
        public string TRF_BB_BRANCH_NAME { get; set; }
        public string TRF_BA_ACCOUNT_NO { get; set; }
        public string BA_REC_ID { get; set; }
        public string TRF_IFSC_CODE { get; set; }
        public string TRF_STATUS { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }       
    }
}