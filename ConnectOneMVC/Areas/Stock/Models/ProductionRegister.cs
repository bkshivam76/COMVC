using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations.StockProduction;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class ProductionRegister_Period
    {
        public string Production_BE_View_Period { get; set; }
        public DateTime Production_Fromdate { get; set; }
        public DateTime Production_Todate { get; set; }
        public string Production_PeriodSelection { get; set; }
        public DateTime Production_Opendate { get; set; }
        public DateTime Production_Closedate { get; set; }        
    }
    [Serializable]
    public class Model_NEVD_Production : AllRights
    {
        public string ActionMethod { get; set; }      
        [Required(ErrorMessage = "Production Date Cannot Be Blank...!")]
        public DateTime? ProductionDate { get; set; }
        public string ProductionNumber { get; set; }
        public int? ProductionProjectNameID { get; set; }
        public int? Prod_ProjectID { get; set; }//Mantis bug 0000954 fixed
        public string ProductionProjectName { get; set; }       
        [Required(ErrorMessage = "Production Done By Cannot Be Blank...!")]
        public string ProductionDoneBy { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string ProductionRemarks { get; set; }
        public DateTime? ProductionFromDate { get; set; }
        public DateTime? ProductionToDate { get; set; }
        [Required(ErrorMessage = "Production Store Cannot Be Blank...!")]
        public int? ProductionStoreID { get; set; }
        public string ProductionStore { get; set; }
        [Required(ErrorMessage = "Production Location Cannot Be Blank...!")]
        public string ProductionLocationID { get; set; }
        public string ProductionLocation { get; set; }
        [Required(ErrorMessage = "Production Lot Number Cannot Be Blank...!")]
        [RegularExpression("([0-9]|[A-Za-z][0-9])", ErrorMessage = "Only Numbers Or Alphanumeric Allowed")]
        public string ProductionLotNumber { get; set; }
        public string ProductionSanctionNo { get; set; }      
        public int Prod_ID { get; set; }      
    }
    [Serializable]
    public class ProductionActualManpowerUsage
    {
        [Required(ErrorMessage = "Person Name Cannot be blank...!")]
        public int? Production_ManpowerID { get; set; }
        public int? Production_Manpower_ChargesID { get; set; }
        public string Production_PersonName { get; set; }

        public DateTime? PM_Leave_Date { get; set; }//Mantis bug 0001045 resolved
        public DateTime? PM_Joined_Date { get; set; }//Mantis bug 0001045 resolved

        public string Production_ManpowerUnitsID { get; set; }
        public string Production_PersonType { get; set; }
        [Display(Name = "Work Period From")]
        [Required(ErrorMessage = "Work Period From Cannot be blank...!")]
        public DateTime? Production_W_PeriodFrom { get; set; }
        [Display(Name = "Work Period To")]
        [Required(ErrorMessage = "Work Period To Cannot be blank...!")]
        public DateTime? Production_W_PeriodTo { get; set; }
        [Required(ErrorMessage = "Unit of Work Cannot be blank...!")]
        public string Production_ManpowerUnits { get; set; }
        [Display(Name = "Actual Unit Worked")]
        [Range(0.01, 999.99, ErrorMessage = "Actual Unit Worked Should Be Between 0 and 999.99 ...!")]
        [Required(ErrorMessage = "Actual Unit Worked Cannot be blank...!")]
        public double? Production_Units_Worked { get; set; }
        [Required(ErrorMessage = "Rate/Unit Cannot Be Blank ...!")]
        public double Production_RatePerUnit { get; set; }
        [Required(ErrorMessage = "Total Amount Cannot Be Blank ...!")]
        public double? Production_TotalCost { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Production_ManpowerRemarks { get; set; }
        public int ID { get; set; }       
        public string ActionMethod { get; set; }        
        public int sr { get; set; }
    }
    [Serializable]
    public class ProductionScrapCreatedDetails
    {
        [Required(ErrorMessage = "Stock Cannot Be Blank...!")]
        public int? Production_ScrapItem_ID { get; set; }
        public string Production_ScrapItem_Name { get; set; }
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public double Production_ScrapQty { get; set; }
        public string Production_ScrapUnit { get; set; }
        public string Production_ScrapUnitID { get; set; }
        public string Production_ScrapCode { get; set; }
        public decimal Production_ScrapRate { get; set; }
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Amount Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Amount Cannot Be Blank...!")]
        public double Production_ScrapAmount { get; set; }
        public string Production_ScrapHead { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Production_ScrapRemarks { get; set; }
        public int ID { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public int CreatedStockID { get; set; }
        public int sr { get; set; }
        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class ProductionStockProducedDetails
    {
        [Required(ErrorMessage = "Consumed Stock Cannot Be Blank...!")]
        public int? Production_StkPrd_Item_ID { get; set; }
        public string Production_StkPrd_Item_Name { get; set; }
        public string Production_StkPrd_Head { get; set; }
        public string Production_StkPrd_Type { get; set; }
        public string Production_StkPrd_Code { get; set; }
        public string Production_StkPrd_Category { get; set; }

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public double Production_StkPrd_Produced_Qty { get; set; }
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public double Production_StkPrd_Accepted_Qty { get; set; }
        public double Production_StkPrd_Rejected_Qty { get; set; }
        public double? Production_StkPrd_MarketRate { get; set; }//0000071 bug fixed.
        public double? Production_StkPrd_MarketPrice { get; set; }//0000071 bug fixed.
        [Range(0.01, 100.00, ErrorMessage = "Percentage Should be Between 0.01 and 100.00")]
        [Required(ErrorMessage = "Percentage Cannot Be Blank...!")]
        public double? Production_StkPrd_Percentage { get; set; }
        public double Production_StkPrd_TotalValue { get; set; }
        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string Production_StkPrd_Unit { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Production_StkPrd_Remarks { get; set; }
        public int ID { get; set; }
        public string StkPrd_UnitID { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public int StockID { get; set; }
        public int sr { get; set; }
        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class ProductionStockConsumedDetails
    {
        [Required(ErrorMessage = "Consumed Stock Cannot Be Blank...!")]
        public int? Production_Item_ID { get; set; }
        public string Production_Item_Name { get; set; }
        public string Production_Head { get; set; }
        public string Production_Make { get; set; }
        public string Production_Type { get; set; }
        public string Production_Code { get; set; }
        public string Production_Model { get; set; }

        public double Production_Available_Qty { get; set; }

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Consumed Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Consumed Quantity Cannot Be Blank...!")]
        public double Production_Consumed_Qty { get; set; }
        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string Production_Unit { get; set; }
        [Required(ErrorMessage = "Amount Cannot Be Blank...!")]
        public double Production_Amount { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Production_Remarks { get; set; }
        public int consumptionID { get; set; }
     //   public int avl_qty { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public int StockID { get; set; }
        public int sr { get; set; }
        public string ActionMethod { get; set; }
    }
    [Serializable]
    public class ProductionMachineUsageDetail
    {
        public int Sr_No { get; set; }
        [Required(ErrorMessage = "Machine Name Can Not be Empty...!")]
        public int? Production_Machine_NameID { get; set; }
        public string Production_Machine_No { get; set; }
        [Range(1, 1000000, ErrorMessage = "Machine Count Cannot Be Less Than 1...!")]
        [Required(ErrorMessage = "Machine Count Cannot be Empty...!")]
        public int? Production_Machine_Count { get; set; }
        [Required(ErrorMessage = "Machine Usage Can Not be Empty...!")]
        [Range(0.01, 9999999.99, ErrorMessage = "Machine Usage Hours Should Be Between 0.01 and 9999999.99")]
        public double? Production_Machine_Usage_Hours { get; set; }
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Production_Machine_Remarks { get; set; }
        public string Production_Machine_Name { get; set; }
        public int ID { get; set; }
        public string ActionMethod { get; set; }     
        public decimal Curr_Qty { get; set; }

    }
           
}

