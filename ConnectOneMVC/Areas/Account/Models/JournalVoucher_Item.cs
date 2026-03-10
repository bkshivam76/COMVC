using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class JournalVoucher_Item
    {
        public string TitleX_JV_Itm { get; set; }
        public int? Sr { get; set; }
        public string ActionMethod_JV_Itm { get; set; }
        public string Me_Text_JV_Itm { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag_JV_Itm { get; set; }        
        public double? Txt_Amt_JV_Itm { get; set; }
        public double? Txt_Qty_JV_Itm { get; set; }
        public string TXT_Reference_JV_Itm { get; set; }
        public string Txt_Remarks_JV_Itm { get; set; }
        public int? RdAction_JV_Itm { get; set; }
        public string BE_Item_Head_JV_Itm { get; set; }
        public string Txt_ItemNature_JV_Itm { get; set; }
        public string GLookUp_ItemList_JV_Itm { get; set; }
        public string GLookUp_ItemName_JV_Itm { get; set; }
        public string GLookUp_PartyList_JV_Itm { get; set; }
        public string GLookUp_PartyName_JV_Itm { get; set; }        
        public string GLookUp_PurList_JV_Itm { get; set; }
        public string GLookUp_PurName_JV_Itm { get; set; }
        public string Cmb_RefType_JV_Itm { get; set; }
        public string BE_PAN_No_JV_Itm { get; set; }
        public string BE_UID_No_JV_Itm { get; set; }
        public string BE_ID_No_JV_Itm { get; set; }
        public string BE_Party_Category_JV_Itm { get; set; }
        //Global variables
        public string iTxnM_ID_JV_Itm { get; set; }
        public string iLed_ID_JV_Itm { get; set; }        
        public string iParty_Req_JV_Itm { get; set; }
        public string iProfile_JV_Itm { get; set; }
        public string iPartyID_JV_Itm { get; set; }
        public string iSpecific_ItemID_JV_Itm { get; set; }
        public string iPur_ID_JV_Itm { get; set; }
        public string Cross_RefID_JV_Itm { get; set; }
        public DateTime? RefItem_RecEditOn_JV_Itm { get; set; } = DateTime.MinValue;
        public DateTime? Party_RecEditOn_JV_Itm { get; set; } = DateTime.MinValue;
        public double? Qty_JV_Itm { get; set; }
        public string iTDScode_JV_Itm { get; set; }
        public int? iTop_JV_Itm { get; set; }
        public string iReference_JV_Itm { get; set; }
        public string X_LOC_ID_JV_Itm { get; set; }
        public bool Calc_Allow_JV_Itm { get; set; }

        public string xMID { get; set; }

        public string Ref_RecID_JV_Itm {get; set; }
        public string iProfile_OLD_JV_Itm {get; set; }
        public string iCond_Ledger_ID_JV_Itm {get; set; }
        public double? iMinValue_JV_Itm { get; set; }
        public double? iMaxValue_JV_Itm { get; set; }
        public string iLed_Type_JV_Itm {get; set; }
        public string iCon_Led_Type_JV_Itm {get; set; }
        public string Vdt_JV_Itm {get; set; }
        public string iRefType_JV_Itm {get; set; }

        //Gold/Silver
        public string GS_DESC_MISC_ID_JV_Itm { get; set; }
        public double? GS_ITEM_WEIGHT_JV_Itm { get; set; }

        
        //Other Assets
        public string AI_TYPE_JV_Itm { get; set; }
        public string AI_MAKE_JV_Itm { get; set; }
        public string AI_MODEL_JV_Itm { get; set; }
        public string AI_SERIAL_NO_JV_Itm { get; set; }
        public string AI_PUR_DATE_JV_Itm { get; set; }
        public double? AI_WARRANTY_JV_Itm { get; set; }

        //Live Stock
        public string LS_NAME_JV_Itm { get; set; }
        public string LS_BIRTH_YEAR_JV_Itm { get; set; }
        public string LS_INSURANCE_JV_Itm { get; set; }
        public string LS_INSURANCE_ID_JV_Itm { get; set; }
        public string LS_INS_POLICY_NO_JV_Itm { get; set; }
        public string LS_INS_DATE_JV_Itm { get; set; }
        public double? LS_INS_AMT_JV_Itm { get; set; }

        //Vehicle
        public string VI_MAKE_JV_Itm { get; set; }
        public string VI_MODEL_JV_Itm { get; set; }
        public string VI_REG_NO_PATTERN_JV_Itm { get; set; }
        public string VI_REG_NO_JV_Itm { get; set; }
        public string VI_REG_DATE_JV_Itm { get; set; }
        public string VI_OWNERSHIP_JV_Itm { get; set; }
        public string VI_OWNERSHIP_AB_ID_JV_Itm { get; set; }
        public string VI_DOC_RC_BOOK_JV_Itm { get; set; }
        public string VI_DOC_AFFIDAVIT_JV_Itm { get; set; }
        public string VI_DOC_WILL_JV_Itm { get; set; }
        public string VI_DOC_TRF_LETTER_JV_Itm { get; set; }
        public string VI_DOC_FU_LETTER_JV_Itm { get; set; }
        public string VI_DOC_OTHERS_JV_Itm { get; set; }
        public string VI_DOC_NAME_JV_Itm { get; set; }
        public string VI_INSURANCE_ID_JV_Itm { get; set; }
        public string VI_INS_POLICY_NO_JV_Itm { get; set; }
        public string VI_INS_EXPIRY_DATE_JV_Itm { get; set; }


        //Land and Building
        public string LB_PRO_TYPE_JV_Itm { get; set; }
        public string LB_PRO_CATEGORY_JV_Itm { get; set; }
        public string LB_PRO_USE_JV_Itm { get; set; }
        public string LB_PRO_NAME_JV_Itm { get; set; }
        public string LB_PRO_ADDRESS_JV_Itm { get; set; }
        public string LB_OWNERSHIP_JV_Itm { get; set; }
        public string LB_OWNERSHIP_PARTY_ID_JV_Itm { get; set; }
        public string LB_SURVEY_NO_JV_Itm { get; set; }
        public double? LB_TOT_P_AREA_JV_Itm { get; set; }
        public double? LB_CON_AREA_JV_Itm { get; set; }
        public string LB_CON_YEAR_JV_Itm { get; set; }
        public string LB_RCC_ROOF_JV_Itm { get; set; }
        public double? LB_DEPOSIT_AMT_JV_Itm { get; set; }
        public string LB_PAID_DATE_JV_Itm { get; set; }
        public double? LB_MONTH_RENT_JV_Itm { get; set; }
        public double? LB_MONTH_O_PAYMENTS_JV_Itm { get; set; }
        public string LB_PERIOD_FROM_JV_Itm { get; set; }
        public string LB_PERIOD_TO_JV_Itm { get; set; }
        public string LB_DOC_OTHERS_JV_Itm { get; set; }
        public string LB_DOC_NAME_JV_Itm { get; set; }
        public string LB_OTHER_DETAIL_JV_Itm { get; set; }
        public string LB_REC_ID_JV_Itm { get; set; }
        public string LB_ADDRESS1_JV_Itm { get; set; }
        public string LB_ADDRESS2_JV_Itm { get; set; }
        public string LB_ADDRESS3_JV_Itm { get; set; }
        public string LB_ADDRESS4_JV_Itm { get; set; }
        public string LB_STATE_ID_JV_Itm { get; set; }
        public string LB_DISTRICT_ID_JV_Itm { get; set; }
        public string LB_CITY_ID_JV_Itm { get; set; }
        public string LB_PINCODE_JV_Itm { get; set; }
        public string LB_ADDRESS_JV_Itm { get; set; }
        public string List_LB_DOCS_ARRAY_JV_Itm { get; set; }
        public DataTable LB_DOCS_ARRAY_JV_Itm { get; set; }
        public string List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm { get; set; }
        public DataTable LB_EXTENDED_PROPERTY_TABLE_JV_Itm { get; set; }

        //WIP
        public string REF_REC_ID_JV_Itm { get; set; }
        public string REFERENCE_JV_Itm { get; set; }
        public string WIP_REF_TYPE_JV_Itm { get; set; }

    }

    [Serializable]
    public class ItemList_JV_Itm
    {
        public string ITEM_NAME { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string ITEM_PARTY_REQ { get; set; }
        public string ITEM_PROFILE { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public int? ITEM_CON_MIN_VALUE { get; set; }
        public int? ITEM_CON_MAX_VALUE { get; set;}
        public string ITEM_TDS_CODE { get; set; }
        public string ITEM_ID { get; set; }
        public int? TDS_RATE { get; set;}
        public string LED_TYPE { get; set; }
        public string CON_LED_TYPE { get; set; }
    }

    [Serializable]
    public class PurList_JV_Itm
    {
        public string PUR_NAME { get; set; }
        public string PUR_ID { get; set; }
    }

}