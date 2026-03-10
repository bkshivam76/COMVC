using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations.StockRequisitionRequest;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class RequestRegister_Period
    {
        public string RR_BE_View_Period { get; set; }
        public DateTime RR_Fromdate { get; set; }
        public DateTime RR_Todate { get; set; }
        public string RR_PeriodSelection { get; set; }
        public DateTime RR_Opendate { get; set; }
        public DateTime RR_Closedate { get; set; }
    }
    [Serializable]
    public class Model_NEVD_Requisition
    {
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        [Required(ErrorMessage = "Requisition Date Cannot Be Blank...!")]
        public DateTime RequisitionDate { get; set; }
        public string RequisitionNumber { get; set; }
        public string RequisitionStatus { get; set; }
        public int? RequisitionProjectID { get; set; }
        public int? RequisitionJobID { get; set; }
        [Required(ErrorMessage = "Requesting Store/Dept. Cannot Be Blank...!")]
        public int? RequisitionRequestorStoreDeptID { get; set; }

        [Required(ErrorMessage = "Requestor Cannot Be Blank...!")]
        public int? RequisitionRequestorID { get; set; }
        [Required(ErrorMessage = "Requisition Type Cannot Be Blank...!")]
        public string RequisitionType { get; set; }
        public int? RequisitionTransferFromDeptID { get; set; }
        public Int32? RequisitionPurchasedByID { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string RequisitionRemarks { get; set; }
        public Decimal? RequisitionSpecialDiscount { get; set; }
        public int RR_ID { get; set; }
        public string RRSubmitMode { get; set; }
        public bool is_Requestor { get; set; }
        public bool is_Store_Keeper { get; set; }
        public int is_Approver { get; set; }
        public int? RR_Curr_User_Personnel_ID { get; set; }
        public int? RR_Curr_User_Store_ID { get; set; }
        public string RRPopupName { get; set; }
        public string RRPostSuccessFunction { get; set; }
        public string RR_CurrUserRole { get; set; }
        public string RR_UOSubitemID { get; set; } // for createUO-RR link  
        public string RR_UO_ItemRecID { get; set; } // for createUO-RR link
        public int RR_UOID { get; set; } // for createUO-RR link
    }
    [Serializable]
    public class Model_Requisition_Item_Requested
    {
        [Required(ErrorMessage = "Requested Item Cannot Be Blank...!")]
        public int? Requisition_RequestedItem_Item_ID { get; set; }
        public string Requisition_RequestedItem_Item_Name { get; set; }
        public string Requisition_RequestedItem_Head { get; set; }
        public string Requisition_RequestedItem_Item_Type { get; set; }
        public string Requisition_RequestedItem_Item_Code { get; set; }
        public string Requisition_RequestedItem_Make { get; set; }
        public string Requisition_RequestedItem_Model { get; set; }
        public int? Requisition_RequestedItem_Supplier_ID { get; set; }
        public string Requisition_RequestedItem_Supplier { get; set; }
        public string Requisition_RequestedItem_Delivery_Location_ID { get; set; }
        public string Requisition_RequestedItem_Delivery_Location { get; set; }

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Requested Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Requested Quantity Cannot Be Blank...!")]
        public double Requisition_RequestedItem_Requested_Qty { get; set; }

        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string Requisition_RequestedItem_Unit_ID { get; set; }
        public string Requisition_RequestedItem_Unit { get; set; }

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Approved Quantity Should Be Between 0.01 and 99999999999999999.99")]
        public double? Requisition_RequestedItem_Approved_Qty { get; set; }
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Rate Should Be Between 0.01 and 99999999999999999.99")]
        public double? Requisition_RequestedItem_Rate { get; set; }

        public double? Requisition_RequestedItem_Rate_After_Discount { get; set; }
        //[Range(0.01, 100, ErrorMessage = "Percentage Should Be Between 0.01 and 100")]
        public double? Requisition_RequestedItem_Discount { get; set; }
        public double? Requisition_RequestedItem_Taxes { get; set; }
       public double? Requisition_RequestedItem_Tax_Percent { get; set; }
        public string Requisition_RequestedItem_Priority { get; set; }
        [Required(ErrorMessage = "Delivery Date Cannot Be Blank...!")]
        public DateTime? Requisition_RequestedItem_Reqd_Delivery_date { get; set; }
        public double? Requisition_RequestedItem_Amount { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Requisition_RequestedItem_Remarks { get; set; }
        public int Requisition_RequestedItem_ID { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public int Sr { get; set; }
        public int? UODeptID { get; set; }
         public int? UOSubDeptID { get; set; }
         public Common_Lib.Common.Navigation_Mode ActionMethod{ get; set; }
        public int RR_UOID { get; set; } // for createUO-RR link


        //Add Taxes Form Fields

        [Required(ErrorMessage = "Tax Type Cannot Be Blank...!")]
        public string Requisition_Addtax_TaxTypeID { get; set; }

        public string Requisition_Addtax_TaxType { get; set; }

        [Required(ErrorMessage = "Tax Percent Cannot Be Blank...!")]
        public decimal Requisition_Addtax_TaxPercent { get; set; }
        public string Requisition_Addtax_Remarks { get; set; }

        public double? Requisition_Addtax_TaxAmount { get; set; }

        public string RR_Taxes_ActionMethod { get; set; }

        public int Requisition_Addtax_RecID { get; set; }
    }
}