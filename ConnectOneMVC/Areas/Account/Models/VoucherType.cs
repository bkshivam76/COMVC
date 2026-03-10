using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class VoucherType
    {
        public int ID { get; set; }
        [Display(Name = "Item Name :")]
        public string GLookUp_ItemList_CBVoucher { get; set; }

        public string Get_Voucher_Type { get; set; }
        public string Voucher_Type { get; set; }
        public bool Selection_By_Item { get; set; }
    }
}