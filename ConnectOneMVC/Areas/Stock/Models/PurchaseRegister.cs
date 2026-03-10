using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations.StockPurchaseOrder;
namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class PurchaseRegister_Period
    {
        public string PO_BE_View_Period { get; set; }
        public DateTime PO_Fromdate { get; set; }
        public DateTime PO_Todate { get; set; }
        public string PO_PeriodSelection { get; set; }
        public DateTime PO_Opendate { get; set; }
        public DateTime PO_Closedate { get; set; }
    }
    [Serializable]
    public class Model_NEVD_Purchase
    {
        public String ActionMethod { get; set; }

        [Display(Name = "PO Number")]
        public string PO_Number{ get; set; }

        [Display(Name = "PO Status")]
        public string PO_Status { get; set; }

        [Required(ErrorMessage = "PO Created Date Cannot Be Blank...!")]
        [Display(Name = "PO Created Date")]
        public DateTime PO_Date { get; set; }

        [Display(Name = "PO Supplier")]
        public int? PO_SupplierID { get; set; }

        [Display(Name = "PO Supplier")]
        public String PO_Supplier { get; set; }

        [Display(Name = "Purchased By")]
        public String PO_PurchasedBy { get; set; }

        [Display(Name = "Delivery Status")]
        public String PO_DeliveryStatus { get; set; }

        [Display(Name = "Total Amount")]
        public decimal PO_TotalAmount { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal PO_PaidAmount { get; set; }

        [Display(Name = "Pending Amount")]
        public decimal PO_PendingAmount { get; set; }

        [Display(Name = "Your Remarks")]
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public String PO_Remarks { get; set; }
        
        public int PO_ID { get; set; }

        public string PO_StatusButtonClick { get; set; }
        public Decimal? PO_SpecialDiscount { get; set; }

    }
    [Serializable]
    public class PurchaseItemOrderedDetail
    {
        [Required(ErrorMessage = "Requested Stock Cannot Be Blank...!")]
        public int Purchase_IO_ItemID { get; set; }

        public String ActionMethod { get; set; }
        public int Sr { get; set; }
               
        public string Purchase_IO_RR { get; set; }

          public string Purchase_IO_ItemName { get; set; }

        public string Purchase_IO_ItemCode { get; set; }

        public string Purchase_IO_ItemType{ get; set; } 
             

        public string Purchase_IO_Head { get; set; }

        [Required(ErrorMessage = "Destination Location Cannot Be Blank...!")]
        public string Purchase_IO_LocationID { get; set; }

        public string Purchase_IO_Make { get; set; }

        public string Purchase_IO_Model { get; set; }


        public decimal Purchase_IO_RequestedQty { get; set; } 

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Ordered Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Ordered Quantity Cannot Be Blank...!")]
        public decimal Purchase_IO_OrderedQty { get; set; }

      //  [Range(0.01, 99999999999999999.99, ErrorMessage = "Rate Should Be Between 0.01 and 99999999999999999.99")]
        public decimal? Purchase_IO_Rate { get; set; }
        public double? Purchase_IO_Rate_After_Discount { get; set; }

        public decimal? Purchase_IO_Amount { get; set; }
        public decimal? Purchase_IO_Taxes { get; set; }
     //   [Range(0.01, 99999999999999999.99, ErrorMessage = "Discount Value Should Be Between 0.01 and 99999999999999999.99")]
        public decimal? Purchase_IO_Discount { get; set; }


        public string Purchase_IO_Unit { get; set; }
      
        public string Purchase_IO_DestLocation { get; set; }
        public string Purchase_IO_POI_Priority { get; set; }
      
      
        public DateTime? Purchase_IO_Reqd_Del_Date { get; set; }//0000109 bug Fixed

        public int ID { get; set; }

        public DateTime Purchase_IO_AddedOn { get; set; }


        public string Purchase_IO_AddedBy { get; set; } 

      

        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string Purchase_IO_UnitID { get; set; }
        public string Purchase_IO_Remarks { get; set; }
        
        public string Purchase_IO_AddUpdateReason { get; set; }
        public string Purchase_IO_PostedBy { get; set; }

        public int? Purchase_IO_RR_Item_Sr_No { get; set; }
        public decimal PendingQty { get; set; }

        public int? Purchase_IO_RequestID { get; set; } 
        public int POid { get; set; }

        //Add Taxes Form Fields

        [Required(ErrorMessage = "Tax Type Cannot Be Blank...!")]
        public string PO_Addtax_TaxTypeID { get; set; }

        public string PO_Addtax_TaxType { get; set; }

        [Required(ErrorMessage = "Tax Percent Cannot Be Blank...!")]
        public decimal PO_Addtax_TaxPercent { get; set; }
        public string PO_Addtax_Remarks { get; set; }

        public double? PO_Addtax_TaxAmount { get; set; }

        public string PO_Taxes_ActionMethod { get; set; }

        public int PO_Addtax_RecID { get; set; }

    }
    [Serializable]
    public class PurchaseGoodsReceivedDetail
    {
        public String ActionMethod { get; set; }
        public int Sr { get; set; }

        public int POid { get; set; }
        // [Display(Name = "Item Name")]

        public string Purchase_GR_ItemName { get; set; }
        public decimal? Purchase_GR_Rate { get; set; }

        // [Display(Name = "Item Code")]
        public string Purchase_GR_ItemCode { get; set; }

        //[Display(Name = "Head")]
        public string Purchase_GR_Head { get; set; }

        //[Display(Name = "Head")]
        public string Purchase_GR_Make { get; set; }

        public string Purchase_GR_Priority { get; set; } 
        // [Display(Name = "Model")]
        public string Purchase_GR_Model { get; set; }

        public decimal Purchase_GR_PendingQuantity { get; set; }

        //[Display(Name = "Ordered Qty")]
        [Required(ErrorMessage = "Received Quantity Cannot Be Blank...!")]
        public decimal Purchase_GR_ReceivedQty { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_Unit { get; set; }

        // [Display(Name = "Ordered Qty")]
        [Required(ErrorMessage = "Received Date Cannot Be Blank...!")]
        public DateTime Purchase_GR_ReceivedDate { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_LotNo { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_BillNo { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_ChallanNo { get; set; }

        // [Display(Name = "Ordered Qty")]
       
        public string Purchase_GR_ShipmentMode { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_Carrier { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_ReceivedBy { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GR_Remarks { get; set; }

        //[Display(Name = "Ordered Qty")]
        [Required(ErrorMessage = "FOB Cannot Be Blank...!")]
        public bool? Purchase_GR_FOB { get; set; }
        // [Display(Name = "Rate")]

       
        public string Purchase_GR_DelLocation { get; set; }
        public int ID { get; set; }
        public int Purchase_GR_SubitemID { get; set; }
        public int Purchase_GR_CreatedStockID { get; set; }


        public DateTime Purchase_GR_AddedOn { get; set; }


       public string Purchase_GR_AddedBy { get; set; }

        public DateTime Purchase_GR_EditedOn { get; set; }


        public string Purchase_GR_EditedBy { get; set; }
        [Required(ErrorMessage = "Requested Stock Item Cannot Be Blank...!")]
        public int Purchase_GR_ItemID { get; set; }

        public DateTime? Purchase_GR_ReqDelDate { get; set; }
        [Required(ErrorMessage = "Shipment Mode Cannot Be Blank...!")]
        public string Purchase_GR_ShipmentModeID { get; set; }

        [Required(ErrorMessage = "Received Location Cannot Be Blank...!")]
        public string Purchase_GR_LocationID { get; set; }

        public int? Purchase_GR_ReceivedByID { get; set; }

        public int Purchase_GR_PO_Item_Rec_ID { get; set; }
      
        

        public int Purchase_GR_Stock_Store_Dept_ID { get; set; }

        public int? Purchase_GR_Stock_Proj_ID { get; set; }

        public decimal Purchase_GR_Stock_Value { get; set; }

        public string Purchase_GR_Warranty { get; set; }

        public string Purchase_GR_UnitID { get; set; }

        public string Purchase_GR_MobileNo{ get; set; }

}
    [Serializable]
    public class PurchaseGoodsReturnedDetail
    {
        public String ActionMethod { get; set; }
        public int Sr { get; set; }


        [Required(ErrorMessage = "Returned Stock Item Cannot Be Blank...!")]
        // [Display(Name = "Item Name")]
        public string Purchase_GRE_ItemName { get; set; }


        // [Display(Name = "Item Code")]
        public string Purchase_GRE_ItemCode { get; set; }

        //[Display(Name = "Head")]
        public int Purchase_GRE_ItemID { get; set; }
        
      

        //[Display(Name = "Head")]
        public string Purchase_GRE_Make { get; set; }


        // [Display(Name = "Model")]
        public string Purchase_GRE_Model { get; set; }

        public decimal Purchase_GRE_PendingQuantity { get; set; } //mantis #822
        public decimal Purchase_GR_RecdQuantity { get; set; }
        //[Display(Name = "Ordered Qty")]
        [Required(ErrorMessage = "Returned Quantity Cannot Be Blank...!")]
        public decimal Purchase_GRE_ReturnedQty { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_Unit { get; set; }

        public string Purchase_GRE_Priority { get; set; } //not in return class
                                                          // [Display(Name = "Ordered Qty")]

        public DateTime Purchase_GRE_ReceivedDate { get; set; }

        [Required(ErrorMessage = "Returned Date Cannot Be Blank...!")]
        public DateTime Purchase_GRE_ReturnedDate { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_LotNo { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_BillNo { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_ChallanNo { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_ShipmentMode { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_Carrier { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_ReturnedBy { get; set; }

        // [Display(Name = "Ordered Qty")]
        public string Purchase_GRE_Remarks { get; set; }

        public int ID { get; set; }
        public int Purchase_GRE_PO_Item_Rec_ID { get; set; }
        public int Purchase_GRE_ReturnedStockID { get; set; }

        public DateTime Purchase_GRE_AddedOn { get; set; }


        public string Purchase_GRE_AddedBy { get; set; }

        public DateTime Purchase_GRE_EditedOn { get; set; }
        
        public string Purchase_GRE_EditedBy { get; set; }
               
        public int Purchase_GRE_RecdEntryID { get; set; }

        [Required(ErrorMessage = "Shipment Mode Cannot Be Blank...!")]

        public string Purchase_GRE_ShipmentModeID { get; set; }

        public int? Purchase_GRE_ReturnedByID { get; set; }
        public int Purchase_GRE_SubitemID { get; set; }
        public int POid { get; set; }
    }
    [Serializable]
    public class PurchasePaymentsDetail
    {
        
        public String ActionMethod { get; set; }
        public int Sr { get; set; }

        public string Purchase_Payment_Mode { get; set; }

        //  [Display(Name = "Item Name")]
        public decimal? Purchase_Payment_Amt { get; set; }

        public DateTime? Purchase_Payment_Date { get; set; }

        public string Purchase_Payment_Deposited_BankBranch { get; set; }

        public string Purchase_Payment_Deposited_AcctNo { get; set; }

        public string Purchase_Payment_Ref_No { get; set; }

        public DateTime? Purchase_Payement_Ref_Date { get; set; }
        public DateTime? Purchase_Payement_ClearingDate { get; set; }
        public string Purchase_Payment_Branch { get; set; }
        public string Purchase_Payment_Bank { get; set; }
        public string Purchase_Payment_AcctNo { get; set; }
       
        public int ID { get; set; }

        public DateTime? Purchase_Payment_AddedOn { get; set; }

        public string Purchase_Payment_AddedBy { get; set; }

        public string Purchase_Payment_TxnMID { get; set; }

        public int Purchase_Payment_MapID { get; set; }

    }
    [Serializable]
    public class POUpDateStatus
    {
        public int PO_ID { get; set; }
        public string PO_StatusType { get; set; }
        //[Required(ErrorMessage = "Remarks Cannot be blank...!")]
        //public string PO_Status_Remark { get; set; }
       // [Required(ErrorMessage = "Completion Date Cannot Be Blank...!")]
       // public DateTime PO_Completion_Date { get; set; }
        public string PopupHeader { get; set; }
    }

    [Serializable]
    public class PriceHistory
    {

        [Required(ErrorMessage = "Item Name Cannot be blank...!")]
        public int PH_Item_ID { get; set; }
        public string PH_Item_Code { get; set; }
        public int PH_Supplier { get; set; }
        
    }
}