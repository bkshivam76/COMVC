using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using static Common_Lib.DbOperations.StockMachineToolAllocation;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class MTAllocationRegister_Period
    {
        public string MTAllocation_BE_View_Period { get; set; }
        public DateTime MTAllocation_Fromdate { get; set; }
        public DateTime MTAllocation_Todate { get; set; }
        public string MTAllocation_PeriodSelection { get; set; }
        public DateTime MTAllocation_Opendate { get; set; }
        public DateTime MTAllocation_Closedate { get; set; }
    }
    [Serializable]
    public class Model_NEVD_MTAllocation
    {
        public string ActionMethod { get; set; }

        [Required(ErrorMessage = "Issue Date Cannot Be Blank...!")]
        public DateTime? MT_IssueDate { get; set; }

        [Required(ErrorMessage = "Issuing Store Cannot Be Blank...!")]
        public int? MT_IssuingStoreID { get; set; }
        [Required(ErrorMessage = "Issued By Cannot Be Blank...!")]
        public int? MT_IssuedBy { get; set; }
        [Required(ErrorMessage = "Issued To Cannot Be Blank...!")]
        public int? MT_IssuedTo { get; set; }
        public int? MT_JobID { get; set; }
        [Required(ErrorMessage = "Usage Site Cannot Be Blank...!")]
        public int? MT_UsageSiteID { get; set; }
        [Required(ErrorMessage = "Machine/Tool Name Cannot Be Blank...!")]
        public string MTName { get; set; }
        public int MT_ID { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string MT_Remarks { get; set; }
        public int StockID { get; set; }
        public int? LoginUserID { get; set; }

        public List<MT_Issued_Tools> AllocatedTools_Grid_data { get; set; }

        public List<Return_GetMachToolIssueRemarks> Existing_RemarksGrid_data { get; set; }
        
    }
    [Serializable]
    public class Model_Return_MTAllocation
    {
        public string ActionMethod { get; set; }       
        public DateTime? MTReturn_IssueDate { get; set; }
        [Required(ErrorMessage = "Return Date Cannot Be Blank...!")]
        public DateTime? MTReturn_ReturnDate { get; set; }
        [Required(ErrorMessage = "Issuing Store Cannot Be Blank...!")]
        public int? MTReturn_IssuingStoreID { get; set; }       
        [Required(ErrorMessage = "Machine/Tool Name Cannot Be Blank...!")]
        public int MTReturn_Tool_ID { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string MTReturn_Remarks { get; set; }
        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public int? MTReturn_ReturnQuantity { get; set; }
        public int? MTReturn_PendingQuantity { get; set; }
        [Required(ErrorMessage = "Return to Cannot Be Blank...!")]
        public int? MTReturn_ReturnedTo { get; set; }
        public int MTReturn_ID { get; set; }
        public int? LoginUserID { get; set; }
        public int? MTReturn_MainIssueID { get; set; }
        public int? MTReturn_IssueRecID { get; set; }
        public List<Return_GetMachToolReturnRemarks> Return_RemarksGrid_data { get; set; }

    }
    [Serializable]
    public class Model_Full_Return_MTAllocation
    {
        [Required(ErrorMessage = "Return Date Cannot Be Blank...!")]
        public DateTime MTFullReturn_ReturnDate { get; set; }
        [Required(ErrorMessage = "Return to Cannot Be Blank...!")]
        public string MTFullReturn_ReturnedTo { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string MTFullReturn_Remarks { get; set; }
        public List<FullReturnGrid> FullReturn_Grid_data { get; set; }
    }
    [Serializable]
    public class FullReturnGrid
    {
        public string MachineName { get; set; }
        public DateTime IssueDate { get; set; }
        public int QtyIssued { get; set; }
        public int QtyPending { get; set; }
        public int QtyReturned { get; set; }
        public string IssuingStore { get; set; }
        public int IssueRowID { get; set; }
        public int MachineToolID { get; set; }
        public int Sr { get; set; }
        public int ID { get; set; }
    }
    [Serializable]
    public class ReturnExisintRemark
    {
        public string Remark { get; set; }
        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class MT_Issued_Tools
    {
        [Required(ErrorMessage = "Select A Machine/Tool")]
        public string Machine_Tool_Name { get; set; }
        public int ToolStockID { get; set; }
        [Min(1, ErrorMessage = "Cannot Be Less Than 1")]
        public decimal Qty_Issued { get; set; }
        public int ID { get; set; }
        public int Sr { get; set; }
    }

  
}