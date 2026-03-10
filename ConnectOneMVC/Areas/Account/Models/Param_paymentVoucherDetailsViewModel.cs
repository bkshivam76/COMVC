using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Param_paymentVoucherDetailsViewModel
    {
        public string xID { get; set; }
        public string xMID { get; set; }
        public string TempActionMethod { get; set; }
        public Common.Navigation_Mode Tag { get; set; }

        public string GLookUp_PartyList1_Tag { get; set; }
        public string GLookUp_PartyList1_Txt { get; set; }
        public string BE_City { get; set; }
        public string BE_PAN_No { get; set; }
        public string BE_AADHAR_No { get; set; }
        public DateTime? Txt_Inv_Date { get; set; }
        public string Txt_Inv_No { get; set; }
        [Required(ErrorMessage = "Date Incorrect Or Blank !!")]
        public DateTime? Txt_V_Date { get; set; }
        public string Txt_V_NO { get; set; }

        public decimal Txt_DiffAmt { get; set; }
        public decimal Txt_SubTotal { get; set; }

        public decimal Txt_CashAmt { get; set; }
        public decimal Txt_CreditAmt { get; set; }
        public decimal Txt_BankAmt { get; set; }
        public decimal Txt_AdvAmt { get; set; }
        public decimal Txt_LB_Amt { get; set; }
        public decimal Txt_TDS_Amt { get; set; }
        public string Txt_Narration { get; set; }
        public DateTime? Txt_DueDate { get; set; }
        public string Txt_Reference { get; set; }

        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iSpecific_ItemID { get; set; }
        public DateTime? GLookUp_PartyList1_REC_EDIT_ON { get; set; }
        public Helper.PaymentType SelectedPaymentType { get; set; }
        public string PaymentType { get; set; }
        public int Cnt_BankAccount { get; set; }
        public bool iParty_Req { get; set; }
        public DataTable LB_DOCS_ARRAY { get; set; }
        public DataTable LB_EXTENDED_PROPERTY_TABLE { get; set; }
        public DateTime LastEditedOn { get; set; }
        public DateTime Info_LastEditedOn { get; set; }
        public string Sel_Bank_ID { get; set; }
        public bool IsProfileCreationVoucher { get; set; }
        public string TitleX { get; set; }
        public string Text { get; set; }
        public string PostSucessFunction { get; set; }
        public string PopupName { get; set; }
        public bool ConfirmFlag { get; set; }
        public bool CheckEdit2 { get; set; }
        public string CreditTooltip { get; set; }
        public string PartyTooltip{get;set;}
        public bool IsPartyReadOnly { get; set; }
        public bool IsCreditReadOnly { get; set; }
        public string WindowText { get; set; }

        public List<Return_PaymentVoucherPartyList> PartyListData { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_Pmt { get; set; }
        public string Pmt_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_Pmt { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_Pmt { get; set; }
    }    
}