using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class JournalVoucher
    {
        public string xID { get; set; }
        public string xMID { get; set; }
        public string TitleX { get; set; }
        public string Me_Text { get; set; }
        public string TempActionMethod { get; set; }        
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethod { get; set; }
        public string Txt_V_NO_Jv { get; set; }
        [Required(ErrorMessage = "Voucher Date Required")]
        public DateTime? Txt_V_Date_Jv { get; set; }
        public bool CheckEdit_Jv { get; set; }
        public decimal? Txt_DiffAmt_Jv { get; set; }
        public decimal? Txt_DrTotal_Jv { get; set; }
        public decimal? Txt_CrTotal_Jv { get; set; }
        public string Txt_Narration_Jv { get; set; }
        public string Txt_Reference_Jv { get; set; }
        public DateTime? Info_LastEditedOn_Jv { get; set; }
        public DateTime? LastEditedOn_Jv { get; set; }
        public string iSpecific_ItemID_Jv { get; set; }
        //public IDictionary<string, DateTime> Info_MaxEditedOn { get; set; }
        public bool CheckEdit2_Jv { get; set; }
        public bool isconfirmTDS_Jv { get; set; } = false;
        public bool isconfirmPayCntTDS_Jv { get; set; } = false;

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_Jv { get; set; }
        public string Jv_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_Jv { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_Jv { get; set; }
        public int itemGrid_RowCount_Jv { get; set; }
    }
}