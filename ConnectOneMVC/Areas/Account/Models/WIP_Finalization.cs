using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class WIP_Finalization
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string GLookUp_WIPLedgerList_WIPF { get; set; }
        public string GLookUp_FinalizedAssetList_WIPF { get; set; }
        public string Finalized_Asset_text { get; set; }
        public int? Rad_AssetType_WIPF { get; set; }
        public DateTime? Txt_V_Date_WIPF { get; set; }
        public string Txt_V_NO_WIPF { get; set; }
        public double Txt_TotalAmount_WIPF { get; set; }
        public string Txt_Narration_WIPF { get; set; }
        public string Txt_Reference_WIPF { get; set; }
        public string ActionMethod { get; set; }
        public string TitleX { get; set; }
        public string Txn_Cr_ItemId_WIPF { get; set; }   
        public string xID { get; set; }
        public string xMID { get; set; }
        public string Asset_Item_ID_WIPF { get; set; }
        public string AI_LED_ID_WIPF { get; set; }
        public string iLed_Type_WIPF { get; set; }
        public string iCon_Led_Type_WIPF { get; set; }
        public string iCond_Ledger_ID_WIPF { get; set; }
        public double iMinValue_WIPF { get; set; }
        public double iMaxValue_WIPF { get; set; }   
        public string AI_Type_WIPF { get; set; }
        public string Me_Text { get; set; }
        public string AI_MAKE_WIPF { get; set; }
        public string AI_MODEL_WIPF { get; set; }
        public string AI_SERIAL_NO_WIPF { get; set; }
        public double? AI_WARRANTY_WIPF { get; set; }
        public double? QTY_WIPF { get; set; }
        public string AI_RATE_WIPF { get; set; }
        public string Other_Details_WIPF { get; set; }
        public string X_LOC_ID_WIPF { get; set; }
        public string Existing_Asset_RecID_Profile { get; set; }         
        public bool Saving_From_Asset_Window { get; set; }
        public bool Saving_From_Select_Asset { get; set; }
        public string PurposeID_WIPF { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_WIPF { get; set; }
        public string WIPF_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_WIPF { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_WIPF { get; set; }
    }
    [Serializable]
    public class WIPFinal_Reference_GridData 
    {
        public Int64 Sr { get; set; }
        public string WIP_LED_ID { get; set; }
        public string Reference { get; set; }
        public decimal? OPENING { get; set; }
        public decimal? WIP_Amount { get; set; }
        public decimal? Next_Year_WIP_Amount { get; set; }
        public decimal? Finalized_Amount { get; set; }
        public DateTime? Date_of_Creation { get; set; }
        public string Profile_WIP_RecID { get; set; }

    }
    [Serializable]
    public class WIP_Final_Select_Asset 
    {
        public string FinalAsset_Item_ID_Fin_Sel_Asset { get; set; }
        public string Tr_M_ID_Fin_Sel_Asset { get; set; }
        public string Asset_RecID_Fin_Sel_Asset { get; set; }
        public DateTime? REC_EDIT_ON_Fin_Sel_Asset { get; set; }      
    }
    [Serializable]
    public class WIP_Final_Select_Asset_Grid 
    {        
        public string Item { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Org_Qty { get; set; }
        public decimal Curr_Qty { get; set; }
        public decimal Org_Value { get; set; }
        public decimal Curr_Value { get; set; }
        public string REC_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string AI_TR_ID { get; set; }
    }
    [Serializable]
    public class WIPFinal_Item_Detail
    {
        public Common_Lib.Common.Navigation_Mode Tag_ItemDetail_WIPF { get; set; }
        public string Txt_Reference_ItemDetail_WIPF { get; set; }
        public string Title_ItemDetail_WIPF { get; set; }
        public string iTxnM_ID_ItemDetail_WIPF { get; set; }      
        public string Txt_Amount_ItemDetail_WIPF { get; set; } 
        public decimal? Txt_Finalized_Amount_ItemDetail_WIPF { get; set; }
        public string Next_year_WIP_Amount_ItemDetail_WIPF { get; set; }
        public string ActionMethod_ItemDetail_WIPF { get; set; }
        public int Grid_PK { get; set; }

    }
    [Serializable]
    public class WIP_FinalizedAssetList 
    {
        public string Finalized_Asset { get; set; }
        public string LED_TYPE { get; set; }
        public string CON_LED_TYPE { get; set; }
        public int? ITEM_CON_MIN_VALUE { get; set; }
        public int? ITEM_CON_MAX_VALUE { get; set; }
        public string Asset_Item_ID { get; set; }
        public string Asset_LED_ID { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
    }
    [Serializable]
    public class Frm_Txn_Report_model
    {
        public string WIP_ID { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string Reference { get; set; }
        public string PopupID { get; set; }
        public decimal Opening { get; set; }
        public DateTime OpeningDate { get; set; }

    }
}