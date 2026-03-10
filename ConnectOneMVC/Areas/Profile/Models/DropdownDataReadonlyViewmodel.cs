using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class DropdownDataReadonlyViewmodel
    {
        public string selectData { get; set; }
        public bool? IsReadOnly { get; set; }
    }
    [Serializable]
    public class DropdownDataReadonlyViewmodelForFD
    {
        public string ItemData{ get; set; }        
        public string Tag { get; set;}
        public string selectData { get; set; }
        public bool? IsReadOnly { get; set; }
    }

    [Serializable]
    public class DropdownDataReadonlyViewmodelForInternalTransfer
    {
        public bool? IsVisible { get; set; }
        public string selectData { get; set; }
        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string HQ_IDs { get; set; }
    }

}