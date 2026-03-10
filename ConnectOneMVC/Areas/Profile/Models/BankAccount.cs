using Common_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class BankAccount
    {
        public string ID { get; set; }
        [Display(Name = "Bank Name:")]
        [Required(ErrorMessage = "Bank Name Not Selected...!")]
        public string GLookUp_BankList_Bank { get; set; }
        [Required(ErrorMessage = "Branch Name Not Selected...!")]
        [Display(Name = "Branch Name:")]
        public string GLookUp_BranchList_Bank { get; set; }
        [Required(ErrorMessage = "Account type is Required")]
        [Display(Name = "Account Type:")]
        public string Rad_AccType_Bank { get; set; }
        [Display(Name = "Bank Account No.:")]
        [RegularExpression(@"[A-Za-z0-9\/?/-]{0,20}", ErrorMessage = "* Bank Account No....")]
        public string Txt_No_Bank { get; set; }
        [Display(Name = "Applicable For:")]
        public bool Chk_FERA_Bank { get; set; }       
        public bool chk_FCRA_Util_Bank { get; set; }
        public bool chk_Corpus_Bank { get; set; }
        public bool chk_Reloadable_Card_Bank { get; set; }
        [Display(Name = "Customer Relationship No. / Client ID:")]
        [RegularExpression(@"[A-Za-z0-9\/?/-]{0,20}", ErrorMessage = "* Customer Relationship No./Client ID...")]
        public string Txt_CustNo_Bank { get; set; }
        [Display(Name = "Bank Balance:")]
        [RegularExpression(@"^(0|-?\d{0,19}(\.\d{0,2})?)$")]
        public double? Txt_Amount_Bank { get; set; }
        [Display(Name = "Other Details:")]
        public string Txt_Remarks_Bank { get; set; }
        [Display(Name = "IFSC Code:")]
        public string Txt_IFSC_Bank { get; set; }
        [Display(Name = "MICR Code:")]
        public string Txt_MICR_Bank { get; set; }
        [Display(Name = "Bank PAN No.:")]
        public string Txt_PAN_Bank { get; set; }
        [Display(Name = "Bank TAN No.:")]
        [RegularExpression(@"[A-Za-z]{4}\d{5}[A-Za-z]{1}", ErrorMessage = "* Invalid TAN No...")]
        public string Txt_TAN_Bank { get; set; }
        [Display(Name = "Bank Tel. No.:")]
        public string Txt_TelNo_Bank { get; set; }
        [Display(Name = "Bank Email ID:")]
        public string Txt_EmailID_Bank { get; set; }
        [Display(Name = "NEW/Existing Account:")]
        public string Rad_AccKind_Bank { get; set; }
        [Display(Name = "Opening Date:")]
       // [Required(ErrorMessage = "Date Incorrect Or Blank...!")] rm bug 132572 fix
        public DateTime? Txt_Date_Bank { get; set; }
        [Display(Name = "Closing Date:")]
        public DateTime? BA_CLOSE_DATE { get; set; }
        [Display(Name = "1.")]
        [Required(ErrorMessage = "Signatories not Selected...!")]
        public string Look_SignList1_Bank { get; set; }
        [Display(Name = "2.")]
        public string Look_SignList2_Bank { get; set; }
        [Display(Name = "3.")]
        public string Look_SignList3_Bank { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EdiBy { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string ActionStatus { get; set; }
    

        public string YearID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }


        public string bankData { get; set; }

        public string MsgBalance { get; set; }

       
       
        public string Titlex_Bank { get; set; }
        public string SubTitlex_Bank { get; set; }
        public string Txt_No_Tag { get; set; }
        public string Txt_CustNo_Tag { get; set; }
        public DateTime? SignList1_REC_EDIT_ON { get; set; }
        public DateTime? SignList2_REC_EDIT_ON { get; set; }
        public DateTime? SignList3_REC_EDIT_ON { get; set; }
        public bool Rad_AccType_readonly { get; set; }
        public bool Rad_AccKind_readonly { get; set; }
        public bool GLookUp_BankList_readonly { get; set; }
        public bool GLookUp_BranchList_readonly { get; set; }
        public bool Txt_Amount_readonly { get; set; }
        public bool Fera_Visibility { get; set; }
    }
    [Serializable]
    public class BANK_ACCOUNT_INFO
    {
        public int? BA_CEN_ID { get; set; }
        public string BA_ACCOUNT_TYPE { get; set; }
        public string BA_BRANCH_ID { get; set; }
        public string BA_TAN_NO { get; set; }
        public string BA_TEL_NOS { get; set; }
        public string BA_EMAIL_ID { get; set; }
        public string BA_ACCOUNT_NO { get; set; }
        public string BA_CUST_NO { get; set; }
        public string BA_SIGN_AB_ID_1 { get; set; }
        public string BA_SIGN_AB_ID_2 { get; set; }
        public string BA_SIGN_AB_ID_3 { get; set; }
        public string BA_OTHER_DETAIL { get; set; }
        public DateTime? REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public string REC_EDIT_BY { get; set; }
        public int REC_STATUS { get; set; }
        public DateTime? REC_STATUS_ON { get; set; }
        public string REC_STATUS_BY { get; set; }
        public string REC_ID { get; set; }
        public string BA_ACCOUNT_NEW { get; set; }
        public DateTime? BA_OPEN_DATE { get; set; }
        public DateTime? BA_CLOSE_DATE { get; set; }
        public string BA_FERA_ACC { get; set; }
        public int? BA_COD_YEAR_ID { get; set; }
    
        public BankAccount bankInfo(BANK_ACCOUNT_INFO ba)
        {
            BankAccount bk = new BankAccount();
            bk.GLookUp_BranchList_Bank = ba.BA_BRANCH_ID;
            bk.Txt_No_Bank = ba.BA_ACCOUNT_NO;
            bk.Rad_AccKind_Bank = ba.BA_ACCOUNT_NEW;
            bk.Rad_AccType_Bank = ba.BA_ACCOUNT_TYPE;
            //bk.BA_CEN_ID = ba.BA_CEN_ID;
            bk.BA_CLOSE_DATE = ba.BA_CLOSE_DATE == null ? null : ba.BA_CLOSE_DATE;
            //bk.BA_COD_YEAR_ID = ba.BA_COD_YEAR_ID;
            bk.Txt_CustNo_Bank = ba.BA_CUST_NO;
            //bk.BA_FERA_ACC = ba.BA_FERA_ACC;
            bk.Txt_EmailID_Bank = ba.BA_EMAIL_ID;
            //bk.BA_OPEN_DATE = ba.BA_OPEN_DATE;
            bk.Txt_Remarks_Bank = ba.BA_OTHER_DETAIL;
            bk.Look_SignList1_Bank = ba.BA_SIGN_AB_ID_1;
            bk.Look_SignList2_Bank = ba.BA_SIGN_AB_ID_2;
            bk.Look_SignList3_Bank = ba.BA_SIGN_AB_ID_3;
            bk.Txt_TAN_Bank = ba.BA_TAN_NO;
            bk.Txt_TelNo_Bank = ba.BA_TEL_NOS;
            bk.AddBy = ba.REC_ADD_BY;
            bk.AddDate = ba.REC_ADD_ON == null ? DateTime.Now : Convert.ToDateTime(ba.REC_ADD_ON);
            bk.EdiBy = ba.REC_EDIT_BY;
            bk.ID = ba.REC_ID;
            //bk.REC_STATUS = ba.RemarkStatus;
            bk.ActionStatus = ba.REC_STATUS_BY;
            //bk.REC_STATUS_ON = ba.EditDate;
            return bk;
        }
    }
    [Serializable]
    public class BankAccountOperation
    {
        public string ID { get; set; }
        public string ActionName { get; set; }
        [Display(Name = "Close Date")]     
        public DateTime? BA_ClOSE_DATE { get; set; }
        public Common.Navigation_Mode Tag { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public DateTime? LastEditedOn { get; set; }
    }
    [Serializable]
    public class DownLoadBankStatements
    {  
        public DateTime RunOn { get; set; }
        public List<BankStatementStatus> Status { get; set; }
        public bool Auto { get; set; }
    }
    [Serializable]    
    public class BankStatementStatus
    {
       public string Bank_Account { get; set; }
       public string Status { get; set; }      
        public string Remarks { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int SrNo { get; set; }
        public string BankShort { get; set; }
        public string Type { get; set; }
    }

}