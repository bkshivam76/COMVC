using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class StockItemInfo
    {
        public string Item_Name { get; set; }
        public string Store_Applicable { get; set; }
        public string Consumption_Type { get; set; }
        public string Accounting_Type { get; set; }
        public string Main_Category { get; set; }
        public string Sub_Category { get; set; }
        public string Item_Code { get; set; }
        public string Primary_Unit { get; set; }
        public string Item_Properties { get; set; }
        public string Conversion_Units { get; set; }
        public DateTime Rec_Add_On { get; set; }
        public string REC_ADD_bY { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public int REC_ID { get; set; }
        public string Closed_On { get; set; }
    }
    [Serializable]
    public class StoreLitInfo
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string DeptName { get; set; }
        public string SubDeptName { get; set; }
        public string DeptInchargeName { get; set; }
        public string StoreInchargeName { get; set; }
    }
    [Serializable]
    public class StoreListInfo
    {
        public int Store_Id { get; set; }
        public string Store_Name { get; set; }
        public string Dept_Name { get; set; }
        public string SubDept_Name { get; set; }
        public string DeptInchargeName { get; set; }
        public string StoreInchargeName { get; set; }
    }
    [Serializable]
    public class CloseItemInfo
    {
        public string Item_Name { get; set; }
        public int SubItemID { get; set; }
        public DateTime CloseDate { get; set; }
        public string CloseRemarks { get; set; }
    }
    [Serializable]
    public class MapIteminfo
    {

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store Cannot Be Blank...!")]
        public int? MapItem_Store_id { get; set; }
        public string MapItem_Consumption_Type { get; set; }//Mantis bug 0000334 fixed
        public string Main_Category { get; set; }
        public string Sub_Category { get; set; }
        public string Search_Text { get; set; }
        public string mapped_items_id { get; set; }
        public string unmapped_items_id { get; set; }
    }
    [Serializable]
    public class MapItemsGridinfo
    {
        public int? ItemID { get; set; }
        public string Stock_Item_Name { get; set; }
        public string Item_Category { get; set; }
        public string Item_Type { get; set; }
        public string Item_Code { get; set; }
        public string Unit { get; set; }
        public string UnitID { get; set; }
        public string StoreIDs_Mapped { get; set; }
    }
    [Serializable]
    public class MainCategoryInfo
    {
        public string Main_Category { get; set; }
    }
    [Serializable]
    public class SubCategoryInfo
    {
        public string Sub_Category { get; set; }
    }
}