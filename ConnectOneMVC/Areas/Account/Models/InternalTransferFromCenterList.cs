using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferFromCenterList
    {
        public string FR_UID { get; set; }
        public string FR_CEN_NAME { get; set; }

        public string FR_PAD_NO { get; set; }
        public string FR_INCHARGE { get; set; }
        public string FR_ZONE { get; set; }

        public int? FR_CEN_ID { get; set; }
        public string FR_TEL_NO { get; set; }
        public string FR_ID { get; set; }
    }
    [Serializable]
    public class InternalTransferBankList
    {
        public string BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BA_ID { get; set; }
        public string BANK_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
    }

 }