using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class ProfileFDVoucher
    {
        public string bankAccIDField { get; set; }
        public string fDNoField { get; set; }
        public string fDDateField { get; set; }
        public string fDAsDateField { get; set; }
        public double fDAmountField { get; set; }
        public double fDIntRateField { get; set; }
        public string paymentConditionField { get; set; }
        public string fDMaturityDateField { get; set; }
        public double fDMaturityAmountField { get; set; }
        public string remarksField { get; set; }
        public string txnIDField { get; set; }
        public string renewFrom_IDField { get; set; }
        public string status_ActionField { get; set; }
        public string recIDField { get; set; }

    }
}