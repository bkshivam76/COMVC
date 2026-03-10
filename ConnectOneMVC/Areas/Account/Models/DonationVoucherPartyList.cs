using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class DonationVoucherPartyList
    {
        public string C_ID { get; set; }
        public string C_NAME { get; set; }
        public string C_PASSPORT_NO { get; set; }
        public string CI_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string C_PAN_NO { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }

        public string C_R_ADD1 { get; set; }
        public string C_R_ADD2 { get; set; }
        public string C_R_ADD3 { get; set; }
        public string C_R_ADD4 { get; set; }
        public string C_R_PINCODE { get; set; }
        public string ST_NAME { get; set; }
        public string DI_NAME { get; set; }
    }
}