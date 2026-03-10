using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class DonationInKind_Item_Detail
    {
        [Required(ErrorMessage ="Item Name Is Required...")]
        public string GLookUp_ItemList_DNK_Itm { get; set; }
        public string GLookUp_ItemName_DNK_Itm { get; set; }
        public string BE_Item_Head_DNK_Itm { get; set; }
        public double? Txt_Qty_DNK_Itm { get; set; }
        public double? Txt_Rate_DNK_Itm { get; set; }
        public double Txt_Amount_DNK_Itm { get; set; }
        public string Cmd_UOM_DNK_Itm { get; set; }
        [Required(ErrorMessage = "Purpose Is Required...")]
        public string GLookUp_PurList_DNK_Itm{ get; set; }
        public string Txt_Remarks_DNK_Itm { get; set; }

        public string ActionMethod_DNK_Itm { get; set; }
        public string Me_Text_DNK_Itm { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag_DNK_Itm { get; set; }
        public string TitleX_DNK_Itm { get; set; }      

        //Global Variables
        public string iVoucher_Type_DNK_Itm { get; set; }
        public string iTrans_Type_DNK_Itm { get; set; }
        public string iLed_ID_DNK_Itm { get; set; }
        public string iLed_Type_DNK_Itm { get; set; }
        public string iCon_Led_Type_DNK_Itm { get; set; }
        public string iSpecific_ItemID_DNK_Itm { get; set; }
        public string iParty_Req_DNK_Itm { get; set; }
        public string iProfile_DNK_Itm { get; set; }
        public string iProfile_OLD_DNK_Itm { get; set; }
        public bool iSpecific_Allow_DNK_Itm { get; set; }
        public string iPur_ID_DNK_Itm { get; set; }
        public bool Calc_Allow_DNK_Itm { get; set; }
        public string iCond_Ledger_ID_DNK_Itm { get; set; }
        public double iMinValue_DNK_Itm { get; set; }
        public double iMaxValue_DNK_Itm { get; set; }
        public int Cnt_BankAccount_DNK_Itm { get; set; }
        public string Vno_DNK_Itm { get; set; }
        public string Vdt_DNK_Itm { get; set; }
        public string iTxnM_ID_DNK_Itm { get; set; }
        public string iReference_DNK_Itm { get; set; }
        public string iRefType_DNK_Itm { get; set; }
        public string X_LOC_ID_DNK_Itm { get; set; }

      

        public string GS_DESC_MISC_ID_DNK_Itm { get; set; }
        public double GS_ITEM_WEIGHT_DNK_Itm { get; set; }

        public string AI_TYPE_DNK_Itm { get; set; }
        public string AI_MAKE_DNK_Itm { get; set; }
        public string AI_MODEL_DNK_Itm { get; set; }
        public string AI_SERIAL_NO_DNK_Itm { get; set; }
        public string AI_PUR_DATE_DNK_Itm { get; set; }      
        public double AI_WARRANTY_DNK_Itm { get; set; }
        public Byte[] AI_IMAGE_DNK_Itm { get; set; }
        public double AI_INS_AMT_DNK_Itm { get; set; }

        public string LS_NAME_DNK_Itm { get; set; }
        public string LS_BIRTH_YEAR_DNK_Itm { get; set; }
        public string LS_INSURANCE_DNK_Itm { get; set; }
        public string LS_INSURANCE_ID_DNK_Itm { get; set; }
        public string LS_INS_POLICY_NO_DNK_Itm { get; set; }
        public string LS_INS_DATE_DNK_Itm { get; set; }
        public double LS_INS_AMT_DNK_Itm { get; set; }       

        public string VI_MAKE_DNK_Itm { get; set; }
        public string VI_MODEL_DNK_Itm { get; set; }
        public string VI_REG_NO_PATTERN_DNK_Itm { get; set; }
        public string VI_REG_NO_DNK_Itm { get; set; }
        public string VI_REG_DATE_DNK_Itm { get; set; }
        public string VI_OWNERSHIP_DNK_Itm { get; set; }
        public string VI_OWNERSHIP_AB_ID_DNK_Itm { get; set; }
        public string VI_DOC_RC_BOOK_DNK_Itm { get; set; }
        public string VI_DOC_AFFIDAVIT_DNK_Itm { get; set; }
        public string VI_DOC_WILL_DNK_Itm { get; set; }
        public string VI_DOC_TRF_LETTER_DNK_Itm { get; set; }
        public string VI_DOC_FU_LETTER_DNK_Itm { get; set; }
        public string VI_DOC_OTHERS_DNK_Itm { get; set; }
        public string VI_DOC_NAME_DNK_Itm { get; set; }
        public string VI_INSURANCE_ID_DNK_Itm { get; set; }
        public string VI_INS_POLICY_NO_DNK_Itm { get; set; }
        public string VI_INS_EXPIRY_DATE_DNK_Itm { get; set; }

        public string LB_PRO_TYPE_DNK_Itm { get; set; }
        public string LB_PRO_CATEGORY_DNK_Itm { get; set; }
        public string LB_PRO_USE_DNK_Itm { get; set; }
        public string LB_PRO_NAME_DNK_Itm { get; set; }
        public string LB_PRO_ADDRESS_DNK_Itm { get; set; }
        public string LB_ADDRESS1_DNK_Itm { get; set; }
        public string LB_ADDRESS2_DNK_Itm { get; set; }
        public string LB_ADDRESS3_DNK_Itm { get; set; }
        public string LB_ADDRESS4_DNK_Itm { get; set; }
        public string LB_STATE_ID_DNK_Itm { get; set; }
        public string LB_DISTRICT_ID_DNK_Itm { get; set; }
        public string LB_CITY_ID_DNK_Itm { get; set; }
        public string LB_PINCODE_DNK_Itm { get; set; }
        public string LB_OWNERSHIP_DNK_Itm { get; set; }
        public string LB_OWNERSHIP_PARTY_ID_DNK_Itm { get; set; }
        public string LB_SURVEY_NO_DNK_Itm { get; set; }
        public string LB_CON_YEAR_DNK_Itm { get; set; }
        public string LB_RCC_ROOF_DNK_Itm { get; set; }
        public string LB_PAID_DATE_DNK_Itm { get; set; }
        public string LB_PERIOD_FROM_DNK_Itm { get; set; }
        public string LB_PERIOD_TO_DNK_Itm { get; set; }
        public string LB_DOC_OTHERS_DNK_Itm { get; set; }
        public string LB_DOC_NAME_DNK_Itm { get; set; }
        public string LB_OTHER_DETAIL_DNK_Itm { get; set; }
        public string LB_REC_ID_DNK_Itm { get; set; }     
        public double LB_TOT_P_AREA_DNK_Itm { get; set; }
        public double LB_CON_AREA_DNK_Itm { get; set; }
        public double LB_DEPOSIT_AMT_DNK_Itm { get; set; }
        public double LB_MONTH_RENT_DNK_Itm { get; set; }
        public double LB_MONTH_O_PAYMENTS_DNK_Itm { get; set; }
        public string List_LB_DOCS_ARRAY_DNK_Itm { get; set; }
        public DataTable LB_DOCS_ARRAY_DNK_Itm { get; set; }
        public string List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm { get; set; }
        public DataTable LB_EXTENDED_PROPERTY_TABLE_DNK_Itm { get; set; }

        public string Ref_RecID_DNK_Itm { get; set; }


        public int Sr { get; set; }

    }
    [Serializable]
    public class ItemList_Itm_Dtel
    {        
        public string ITEM_NAME { get; set; }
        public string LED_NAME { get; set; }
        public string LED_TYPE { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string ITEM_PARTY_REQ { get; set; }
        public string ITEM_PROFILE { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public string ITEM_CON_MIN_VALUE { get; set; }
        public string ITEM_CON_MAX_VALUE { get; set; }
        public string CON_LED_TYPE { get; set; }
        public string ITEM_ID { get; set; }
    }
    [Serializable]
    public class PurList_Itm_Dtel 
    {
        public string PUR_NAME { get; set; }
        public string PUR_ID { get; set; }
    }
}