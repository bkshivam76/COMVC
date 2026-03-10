using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferItemList
    {
        public string ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
    }
}