using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class CB_Period
    {
        public string Period { get; set; }
        public int SelectedIndex { get; set; }
    }
    [Serializable]
    public class CB_Reason
    {
        public string RefRecId { get; set; }
      
        //[RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Reason_CB { get; set; }
        public int MappingID { get; set; }
        public int TrCode { get; set; }
        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class Voucher_Updates
    {
        public string xMID { get; set; }
        public string xID { get; set; }
        public string GridToRefresh { get; set; }
        public string GLookUp_Purpose_VoucherUpdate { get; set; }
        public string Txt_Narration_VoucherUpdate { get; set; }
        public string Txt_Reference_VoucherUpdate { get; set; }
        public List<Return_SplVchrRefsList> SpecialVoucherReferenceList_VoucherUpdate { get; set; }
        public int SpecialVoucherReferenceList_Count { get; set; }
        public string ChosenSpecialVoucherReference_VoucherUpdate { get; set; }        
    }
}