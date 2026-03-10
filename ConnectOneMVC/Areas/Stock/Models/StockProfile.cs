using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations.StockProfile;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class StockProfile : AllRights//Mantis bug 0000367 fixed
    {
        public string Store_Name { get; set; }
        public string Item_Name { get; set; }
        public string Head { get; set; }
        public string Serial_Lot_No { get; set; }
        public string Item_Type { get; set; }
        public string Item_Code { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public string Unit { get; set; }
        public decimal Opening_Qty { get; set; }
        public decimal Opening_Value { get; set; }
        public DateTime? Date_of_Purchase { get; set; }
        public string Location { get; set; }
        public decimal Current_Qty { get; set; }
        public decimal Current_Value { get; set; }
        public decimal Total_Value { get; set; }
        public string Remarks { get; set; }
        public DateTime REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public string REC_STATUS { get; set; }
        public DateTime REC_STATUS_ON { get; set; }
        public string REC_STATUS_BY { get; set; }
        public string Proj_Name { get; set; }
        public int REC_ID { get; set; }
        public int Store_ID{ get; set; }
        public string Store_Id { get; set; }
        public string Stock_TR_ID { get; set; }
        public int YEAR_ID { get; set; }

        public List<StockProfile> Stock_Profile_data { get; set; }//Mantis bug 0000367 fixed
    }
    [Serializable]
    public class ADDStockProfile
    {
        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store Name Can not be blank...!")]
        public string Store_Name { get; set; }
        public Int32 Store_Id { get; set; }
        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item Name Can not be blank...!")]
        public string Item_Name { get; set; }
        public string Head { get; set; }
        [Display(Name = "Serial Lot_No")]
        [Required(ErrorMessage = "Serial Lot_No Can not be blank...!")]
        public string Serial_Lot_No { get; set; }
        public string Item_Type { get; set; }
        public string Item_Code { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public string Unit { get; set; }
        public string UnitID { get; set; }
        [Display(Name = "Opening Qty")]
        [Required(ErrorMessage = "Opening Qty Can not be blank...!")]
        public double Opening_Qty { get; set; }
        public double Opening_Value { get; set; }
        public DateTime? Date_of_Purchase { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location Can not be blank...!")]
        public string Location { get; set; }
        public double Current_Qty { get; set; }
        public double Current_Value { get; set; }
        [Display(Name = "Total Value")]
        [Required(ErrorMessage = "Total Value Can not be blank...!")]
        public double Total_Value { get; set; }
        public string Stock_Remarks { get; set; }
        public Nullable<int> Project { get; set; }
        //public int Project { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int Rec_ID { get; set; }
        public int Item_ID { get; set; }
    }
    [Serializable]
    public class StoreInfo
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string DeptName { get; set; }
        public string SubDeptName { get; set; }
        public string DeptInchargeName { get; set; }
        public string StoreInchargeName { get; set; }
    }
    [Serializable]
    public class ItemInfo
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategory { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string Unit { get; set; }
        public string UnitID { get; set; }
    }
    [Serializable]
    public class LocationInfo
    {
        public string LocId { get; set; }
        public string LocationName { get; set; }
        public string MatchedType { get; set; }
        public string MatchedName { get; set; }
        public string MatchedInstt { get; set; }
    }
    [Serializable]
    public class ProjectInfo
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Sanctionno { get; set; }
        public string ComplexName { get; set; }       
    }
    [Serializable]
    public class ChangeLocationInfo
    {
        public int REC_ID { get; set; }
        public string Store_Id { get; set; }
        public string MyProperty { get; set; }
        public string Item_Name { get; set; }
        public string Item_Type { get; set; }
        public string Item_Code { get; set; }
        public string Exist_Location { get; set; }
        //[Required(ErrorMessage ="Select Location...")]
        public string Stk_Profile_Location { get; set; }//Mantis bug 0001186 fixed
        public DateTime Change_Date{ get; set; }
        public DateTime Change_Time { get; set; }
        public string Remarks { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public string Rec_Edit_On { get; set; }
    }
    [Serializable]
    public class LookUpLocation
    {
        public string ID { get; set; }
        public string Location_Name { get; set; }
        public string Matched_Name  { get; set; }
        public string Matched_Type  { get; set; }
    }
    [Serializable]
    public class ItemProperties
    {
        public int SrNo { get; set; }
        public string Property_Name { get; set; }
        public string Property_Value { get; set; }
        public string Remarks { get; set; }
    }
}