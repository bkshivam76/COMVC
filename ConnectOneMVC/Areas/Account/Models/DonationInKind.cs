using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class DonationInKind
    {
        public string ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string GLookUp_PartyList1_DNK { get; set; }        

        public string BE_City_DNK { get; set; }
        public string BE_PAN_No_DNK { get; set; }
        public string Txt_V_NO_DNK { get; set; }
        public string Txt_Narration_DNK { get; set; }
        public string Txt_Reference_DNK { get; set; }
        
        public string BE_ADD1_DNK { get; set; }
        public string BE_ADD2_DNK { get; set; }
        public string BE_ADD3_DNK { get; set; }
        public string BE_ADD4_DNK { get; set; }
        public string BE_STATE_DNK { get; set; }
        public string BE_DISTRICT_DNK { get; set; }
        public string BE_COUNTRY_DNK { get; set; }
        public string BE_PINCODE_DNK { get; set; }
        public string BE_ID_No_DNK { get; set; }
        public string BE_UID_No_DNK { get; set; }
        public string BE_Party_Category_DNK { get; set; }
        public string TitleX { get; set; }
        public string Me_Text { get; set; }
        public string xID { get; set; }
        public string xMID { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string iSpecific_ItemID { get; set; }
        [Required(ErrorMessage = "Voucher Date Required")]
        public DateTime? Txt_V_Date_DNK { get; set; }
        public bool CheckEdit2_DNK { get; set; }
     
        
        public double Txt_SubTotal_DNK { get; set; }

        public bool iParty_Req { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_DNK { get; set; }
        public string DNK_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_DNK { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_DNK { get; set; }
    }
    [Serializable]
    public class PartyList1_DNK 
    {
        public string C_NAME { get; set; }
        public string C_PAN_NO { get; set; }
        public string C_PASSPORT_NO { get; set; }
        public string C_R_ADD1 { get; set; }
        public string C_R_ADD2 { get; set; }
        public string C_R_ADD3 { get; set; }
        public string C_R_ADD4 { get; set; }
        public string C_R_PINCODE { get; set; }
        public string CI_NAME { get; set; }
        public string ST_NAME { get; set; }
        public string DI_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string C_ID { get; set; }
        public string C_UID_NO { get; set; }
        public string C_OTHER_ID { get; set; }
        public string C_OTHER_ID_LABEL { get; set; }
        public string C_CATEGORY { get; set; }
    }
    [Serializable]
    public class DonationInKind_GridData
    {
        public int Sr { get; set; }
        public string Item_ID { get; set; }
        public string Item_Led_ID { get; set; }
        public string Item_Trans_Type { get; set; }
        public string Item_Party_Req { get; set; }
        public string Item_Profile { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string Item_Name { get; set; }  
        public string Head { get; set; }  
        public string Unit { get; set; }
        public string Remarks { get; set; }
        public string Pur_ID { get; set; }
        public string LOC_ID { get; set; }
        public string GS_DESC_MISC_ID { get; set; }
        public string AI_TYPE { get; set; }
        public string AI_MAKE { get; set; }
        public string AI_MODEL { get; set; }
        public string AI_SERIAL_NO { get; set; }        
        public byte[] AI_IMAGE { get; set; }
        public string AI_PUR_DATE { get; set; }
        public string LS_NAME { get; set; }
        public string LS_BIRTH_YEAR { get; set; }
        public string LS_INSURANCE { get; set; }
        public string LS_INSURANCE_ID { get; set; }
        public string LS_INS_POLICY_NO { get; set; }
        public string LS_INS_DATE { get; set; }
        public string VI_MAKE { get; set; }
        public string VI_MODEL { get; set; }
        public string VI_REG_NO_PATTERN { get; set; }
        public string VI_REG_NO { get; set; }
        public string VI_REG_DATE { get; set; }
        public string VI_OWNERSHIP { get; set; }
        public string VI_OWNERSHIP_AB_ID { get; set; }
        public string VI_DOC_RC_BOOK { get; set; }
        public string VI_DOC_AFFIDAVIT { get; set; }
        public string VI_DOC_WILL { get; set; }
        public string VI_DOC_TRF_LETTER { get; set; }
        public string VI_DOC_FU_LETTER { get; set; }
        public string VI_DOC_OTHERS { get; set; }
        public string VI_DOC_NAME { get; set; }
        public string VI_INSURANCE_ID { get; set; }
        public string VI_INS_POLICY_NO { get; set; }
        public string VI_INS_EXPIRY_DATE { get; set; }
        public string LB_PRO_TYPE { get; set; }
        public string LB_PRO_CATEGORY { get; set; }
        public string LB_PRO_USE { get; set; }
        public string LB_PRO_NAME { get; set; }
        public string LB_PRO_ADDRESS { get; set; }
        public string LB_OWNERSHIP { get; set; }
        public string LB_OWNERSHIP_PARTY_ID { get; set; }
        public string LB_SURVEY_NO { get; set; }
        public string LB_CON_YEAR { get; set; }
        public string LB_RCC_ROOF { get; set; }
        public string LB_PAID_DATE { get; set; }
        public string LB_PERIOD_FROM { get; set; }
        public string LB_PERIOD_TO { get; set; }
        public string LB_DOC_OTHERS { get; set; }
        public string LB_DOC_NAME { get; set; }
        public string LB_OTHER_DETAIL { get; set; }
        public string LB_REC_ID { get; set; }
        public string LB_ADDRESS1 { get; set; }
        public string LB_ADDRESS2 { get; set; }
        public string LB_ADDRESS3 { get; set; }
        public string LB_ADDRESS4 { get; set; }
        public string LB_STATE_ID { get; set; }
        public string LB_DISTRICT_ID { get; set; }
        public string LB_CITY_ID { get; set; }
        public string LB_PINCODE { get; set; }
        public string REF_REC_ID { get; set; }
        public string REFERENCE { get; set; }
        public string WIP_REF_TYPE { get; set; }
        public Double Qty { get; set; }
        public Double Rate { get; set; }
        public Double Amount { get; set; }
        public Double AI_WARRANTY { get; set; }
        public Double LS_INS_AMT { get; set; }
        public Double LB_TOT_P_AREA { get; set; }
        public Double LB_CON_AREA { get; set; }
        public Double LB_DEPOSIT_AMT { get; set; }
        public Double LB_MONTH_RENT { get; set; }
        public Double LB_MONTH_O_PAYMENTS { get; set; }
        public Decimal GS_ITEM_WEIGHT { get; set; } 
    }
}