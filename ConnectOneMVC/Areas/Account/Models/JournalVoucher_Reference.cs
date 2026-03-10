using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class JournalVoucher_Reference
    {
        public string Txt_Item_JV_Ref { get; set; }
        public string Txt_Party_JV_Ref { get; set; }
        public string GLookUp_ReferenceList_JV_Ref {get; set; }
        public string GLookUp_ReferenceName_JV_Ref {get; set; }
        public string Help_Document_Description {get; set; }        
        public double? Txt_Amt_JV_Ref  {get; set; }
        public string Txt_Action_JV_Ref {get; set; }
        public double Txt_Qty_JV_Ref { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethodRefWindow { get; set; }
        public string Titlex_JV_Ref { get; set; }

        //Default Variables
        public string iItemProfile_JV_Ref  { get; set; }
        public string SelectedRefID_JV_Ref  {get; set; }
        public string SelectedItemID_JV_Ref  {get; set; }
        public DataTable ReferenceData_JV_Ref  { get; set; }
        public string iTxnM_ID_JV_Ref {get; set; }
        public string Ref_Rec_ID_JV_Ref {get; set; }


        //http post variables
        

    }
    [Serializable]
    public class RefList_JV_Ref
    {
        public string Item { get; set; }
        public string Party { get; set; }
        public string REC_ID { get; set; }
        public string Reason { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Use { get; set; }
        public string OWNER { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Ledger { get; set; }
        public string Reference { get; set; }
        public string Head { get; set; }
        public string Head_Type { get; set; }
        public string Vehicle { get; set; }
        public string Reg_No { get; set; }
        public string DESC { get; set; }
        public string DETAILS { get; set; }
        public string Name { get; set; }
        public string BIRTH_YEAR { get; set; }
        public string Due { get; set; }
        public decimal? Org_Value { get; set; }
        public decimal? Curr_Value { get; set; }
        public decimal? Next_Year_Closing_Value { get; set; }
        public decimal? Org_Qty { get; set; }
        public decimal? Curr_Qty { get; set; }
        public decimal? Org_Weight { get; set; }
        public decimal? Curr_Weight { get; set; }
        public decimal? Period { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }

    }
}