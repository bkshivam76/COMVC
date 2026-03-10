using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransfer
    {
        public bool ITV_TDS_Open { get; set; }
        public string ITV_iVoucher_Type { get; set; }
        public string ITV_iTrans_Type { get; set; }
        public string ITV_iLed_ID { get; set; }
        public string ITV_iSpecific_ItemID { get; set; }

        public string HQ_IDs { get; set; }
        [DefaultValue(false)]
        public bool USE_CROSS_REF { get; set; }
        public string CROSS_REF_ID { get; set; }
        public string CROSS_M_ID { get; set; }
        public string iTO_CEN_ID { get; set; }
        public string iFR_CEN_ID { get; set; }
        public DateTime? FR_REC_EDIT_ON { get; set; }
        public string Ref_Bank_ID { get; set; }
        public string Ref_Branch { get; set; }
        public string _a_Item_ID { get; set; }
        public string _Date { get; set; }
        public string _Mode { get; set; }
        public string _CEN_ID { get; set; }
        public string _BI_ID { get; set; }
        public string _REF_BI_ID { get; set; }
        public string _BI_ACC_NO { get; set; }
        public string _REF_BRANCH { get; set; }
        public string _REF_BANK_ACC_NO { get; set; }
        public string _BI_BRANCH { get; set; }
        public string _BI_REF_NO { get; set; }
        public string _REF_OTHERS { get; set; }
        public string _BI_REF_DT { get; set; }
        public string _Amount { get; set; }
        public string _a_PUR_ID { get; set; }

        [DefaultValue(false)]
        public bool _Accepted_From_Register { get; set; }
        [DefaultValue(false)]
        public bool _Delete_From_Register { get; set; }
        [DefaultValue(false)]
        public bool _Edit_From_Register { get; set; }
        [DefaultValue(false)]
        public bool _New_From_Register { get; set; }
        [DefaultValue(false)]
        public bool _Pending_List { get; set; }

        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        [DefaultValue(true)]
        public bool ShowChangeItemEffects { get; set; }

        public string Selected_Bank_ID { get; set; }
        public string Selected_Mode { get; set; }
        public string Selected_Item_ID { get; set; }
        public DateTime? Selected_V_Date { get; set; }
        public string Selected_Trans_Type { get; set; }
        [DefaultValue(0)]
        public double? Selected_Amount { get; set; }
        public DateTime? Selected_Ref_Date { get; set; }
        public DateTime? Selected_RefC_Date { get; set; }
        public string Selected_Drawee_Bank_ID { get; set; }
        public string Selected_Drawee_Branch { get; set; }
        public string Selected_RefNo { get; set; }

        
        public string ITV_GLookUp_ItemList { get; set; }
        [Required(ErrorMessage = "Voucher Date Not Selected...!")]//Redmine Bug #133237 fixed
        public DateTime? ITV_Txt_V_Date { get; set; }
        public string ITV_BE_Item_Head { get; set; }
        [Required(ErrorMessage = "Mode Not Selected...!")]//Redmine Bug #133166 fixed
        public string ITV_Cmd_Mode { get; set; }
        public string ITV_Txt_V_NO { get; set; }

        public string ITV_GLookUp_FrCen_List { get; set;}
        public string ITV_BE_Fr_UID { get; set; }
        public string ITV_BE_Fr_Institute { get; set; }
        public string ITV_BE_Fr_Pad_No { get; set; }
        public string ITV_BE_Fr_Incharge { get; set; }
        public string ITV_BE_Fr_Tel_No { get; set; }
        public string ITV_BE_Fr_Zone { get; set; }

        public string ITV_GLookUp_ToCen_List { get; set; }
        public string ITV_BE_To_UID { get; set; }
        public string ITV_BE_To_Institute { get; set; }
        public string ITV_BE_To_Pad_No { get; set; }
        public string ITV_BE_To_Incharge { get; set; }
        public string ITV_BE_To_Tel_No { get; set; }
        public string ITV_BE_To_Zone { get; set; }

        public string ITV_GLookUp_BankList { get; set; }
        public string ITV_BE_Bank_Acc_No { get; set; }
        public string ITV_BE_Bank_Branch { get; set; }
        public double? ITV_Txt_Slip_Count { get; set; }
        public double? ITV_Txt_Slip_No { get; set; }

        public string ITV_GLookUp_TrfBankList { get; set; }
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid Format(6 digits Required) <br> If Cheque / D.D. Number is less than six characters please type zero in the beginning.<br> <b>Example:</b> <b>Cheque / D.D.No. 123</b> has to be entered as <b>000123</b>")]//Redmine Bug #133208 fixed
        public string ITV_Txt_DD_Fr_Chq_No { get; set; }
        public string ITV_Txt_Ref_No { get; set; }
        public string ITV_Txt_Trf_Branch { get; set; }
        public DateTime? ITV_Txt_Ref_Date { get; set; }
        public string ITV_Txt_Trf_ANo { get; set; }
        public DateTime? ITV_Txt_Ref_CDate { get; set; }

        public string ITV_Txt_Narration { get; set; }
        public double? ITV_Txt_Amount { get; set; }
        public string ITV_Txt_Remarks { get; set; }
        public double? ITV_Txt_Bank_Chg { get; set; }
        public string ITV_Txt_Reference { get; set; }
        public double? ITV_Txt_Total { get; set; }
        public string ITV_GLookUp_PurList { get; set; }

        public string xID_1 { get; set; }
        public string xID_2 { get; set; }
        public string xMID { get; set; }
        public string ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
     
        public int OpenYearID { get; set; }





      
   

 
       
      
 
        public string REC_EDIT_ON { get; set; }
       
        public string lbl_Trf_ANo_Tag { get; set; }
   
    
        public string TitleX { get; set; }
        public string ToCenterName { get; set; }
        public string FromCenterName { get; set; } 
        public string REC_EDIT_ON_Bank { get; set; }
        public string REC_EDIT_ON_Trf_Bank { get; set; }
        public string ITV_Cmd_Mode_AccessibleDescription { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_ITV { get; set; }
        public string ITV_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_ITV { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_ITV { get; set; }

    }
    [Serializable]
    public class Out_TDS {
        public string RefMID { get; set; }
       public double TDS_Ded { get; set; }
    }
    [Serializable]
    public class PendingInternalTransferInfo
    {
        public string Status { get; set; }
        public string Centre_Name { get; set; }
        public string Centre_UID { get; set; }
        public int? No { get; set; }

        public string Zone { get; set; }
        public string Sub_Zone { get; set; }

        public string CEN_ID { get; set; }
        public string Description { get; set; }

        public string ITEM_ID { get; set; }

        public DateTime Date { get; set; }
        public string Mode { get; set; }
        public double? Amount { get; set; }
        public string BI_ID { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Bank_Ac_No { get; set; }
        public string Incharge { get; set; }
        public string Contact_No { get; set; }
        public string Purpose { get; set; }
        public string PUR_ID { get; set; }
        public string REF_BI_ID { get; set; }
        public string Ref_Branch { get; set; }
        public string Ref_No { get; set; }
        public string Ref_Date { get; set; }
        public string Ref_Others { get; set; }
        public string ID { get; set; }
        public string M_ID { get; set; }
        public string Ref_Bank_AccNo { get; set; }
        public string Narration { get; set; }
        public string Add_By { get; set; }
        public string Add_Date { get; set; }
        public string Edit_By { get; set; }
        public string Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public string Action_Date { get; set; }

    }

    [Serializable]
    public class PendingInternalTransferItemList
    {
        public string ItemList { get; set; }
        public string ITem_ID { get; set; }
        public string iTransType { get; set; }
        public string Led_Name { get; set; }
    }
    [Serializable]
    public class InternalTransferTdsSentInfo
    {
        public DateTime? Txn_Date { get; set; }
        public string Party { get; set; }
        public decimal? Dr_Amount { get; set; }
        public decimal? TDS_Deducted { get; set; }
        public decimal? Remaining_Amount { get; set; }
        public decimal? TDS_Already_Sent { get; set; }
        
        public decimal? TDS_Send { get; set; }
        public string REC_ID { get; set; }
        public double? lblMentionedInvoucher { get; set; }
        public double? lblNetBalance { get; set; }
        public double? lblOnScreenSelection { get; set; }

    }

}