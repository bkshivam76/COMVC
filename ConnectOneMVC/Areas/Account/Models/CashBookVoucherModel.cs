using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class CashBookVoucherModel
    {
        public string BandedGrid { get; internal set; }
        public object iACTION_STATUS { get; internal set; }
        public DateTime iREC_ADD_ON { get; internal set; }
        public DateTime iREC_EDIT_ON { get; internal set; }
        public string iREC_ID { get; internal set; }
        public int iTR_CODE { get; internal set; }
        public DateTime iTR_DATE { get; internal set; }
        public string iTR_ITEM_ID { get; internal set; }
        public string iTR_M_ID { get; internal set; }
        public string iTR_SR_NO { get; internal set; }
        public string iTR_TYPE { get; internal set; }
    }

}