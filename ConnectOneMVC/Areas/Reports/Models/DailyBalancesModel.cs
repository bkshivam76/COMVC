using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Reports.Models
{
    [Serializable]
    public class DailyBalancesModel
    {
        [Display(Name = "View Type")]
        public int Cmb_View_DB { get; set; }

        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Balances")]
        public string rdo_Balances_Mode_DB { get; set; }

        [Display(Name = "Output")]
        public string RadioGroup2_DB { get; set; }

        [Display(Name = "Status")]
        public string RadioGroup3_DB { get; set; }

        [Display(Name = "Bank")]
        public string GLookUp_BankList_DB { get; set; }

        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Display(Name = "A/c.No.")]
        public string Acno { get; set; }

        public string ID { get; set; }

        public string Fromdate { get; set; }

        public string Todate { get; set; }

        public string Fromdate2 { get; set; }

        public string Todate2 { get; set; }

        public DateTime xFr_Date { get; set; }

        public DateTime xTo_Date { get; set; }

        public DateTime? ITR_Date { get; set; }

        public string Item { get; set; }

        public string Mode { get; set; }

        public string RefNo { get; set; }

        public string Party { get; set; }

        public DateTime? Instrumentdate { get; set; }

        public DateTime? Clearingdate { get; set; }

        public double? Debit { get; set; }

        public double? Credit { get; set; }

        public double? Balance { get; set; }
        public string iREC_ID { get; set; }
        public string iTR_M_ID { get; set; }
        public string iTR_ITEM_ID { get; set; }
        public string iTR_TEMP_ID { get; set; }
        public string Grid_PK { get; set; }
        public string ActionType { get; set; }
        
        public string Balancesradio { get; set; }
        public string BE_Bank_Acc_No { get; set; }
        public string Xr_BankName { get; set; }
        public string Statusradio { get; set; }
        public string Outputradio { get; set; }
        public string Bank_Acc_ID { get; set; }
        public DateTime? xFrTemp_Date { get; set; }
        public DateTime? xToTemp_Date { get; set; }
        public string SpecialVoucherReference { get; set; }
        public int? iREQ_ATTACH_COUNT { get; set; }
        public int? iCOMPLETE_ATTACH_COUNT { get; set; }
        public int? iRESPONDED_COUNT { get; set; }
        public int? iREJECTED_COUNT { get; set; }
        public int? iOTHER_ATTACH_CNT { get; set; }
        public int? iALL_ATTACH_CNT { get; set; }

        //Added for Audit Icon Filter
        public int? VOUCHING_PENDING_COUNT { get; set; }
        public int? VOUCHING_ACCEPTED_COUNT { get; set; }
        public int? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public int? VOUCHING_REJECTED_COUNT { get; set; }
        public int? VOUCHING_TOTAL_COUNT { get; set; }
        public int? AUDIT_PENDING_COUNT { get; set; }
        public int? AUDIT_ACCEPTED_COUNT { get; set; }
        public int? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public int? AUDIT_REJECTED_COUNT { get; set; }
        public int? AUDIT_TOTAL_COUNT { get; set; }
        public string iIcon { get; set; }
    }
    [Serializable]
    public class DailyBalance
    {      
        public string BANK_ID { get; set; }
      
        public string BA_ID { get; set; }
    
        public string BANK_ACC_NO { get; set; }
    
        public string BANK_BRANCH { get; set; }
    
        public string BI_SHORT_NAME { get; set; }
     
        public string BANK_NAME { get; set; }
       
        public string BB_BRANCH_ID { get; set; }

       
    }
    [Serializable]
    public class BankReconcileModel
    {
        public string VoucherDate { get; set; }

        public string ClearingDate { get; set; }

        public string Mode { get; set; }

        public string RefNo { get; set; }

        public string Amount { get; set; }

        public string status { get; set; }

        public string Id { get; set; }


    }
    [Serializable]
    public class DB_SpeceficPeriod
    {
        [Required(ErrorMessage = "From Date Is Required")]
        public DateTime DB_Fromdate { get; set; }
        [Required(ErrorMessage = "To Date Is Required")]
        public DateTime DB_Todate { get; set; }
    }
}