using Common_Lib.RealTimeService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class StockItemWindow
    {
        public int SubItemID { get; set; }
        public string ItemMaster_ActionMethod { get; set; }

        //[RegularExpression("^[A-Za-z0-9 ,-]{1,255}$", ErrorMessage = "Only alphaNumeric - is allowed")] - All characters are allowed, restrcition in post function #1359
        [Required(ErrorMessage = "Enter Item Name")]
        public string Sub_Item_Name { get; set; }
        //public string Consumption_Type { get; set; }

        [Required(ErrorMessage = "Enter Consumption Type")]
        public string Sub_Item_Consumption_Type { get; set; }

        [Required(ErrorMessage = "Enter Accounting Item")]
        public string Sub_Item_Accounting_Item { get; set; }

        [Required(ErrorMessage = "Enter Main Category")]
        public string Sub_Item_Main_Category { get; set; }

        //[Required(ErrorMessage = "Enter Sub-Category")]
        public string Sub_Item_Sub_Category { get; set; }
        public string Sub_Item_Code { get; set; }

        [Required(ErrorMessage = "Enter Primary Unit")]
        public string Sub_Item_Unit_Id { get; set; }
        public string Sub_Item_Unit_Name{ get; set; }

        [XmlElement(DataType = "base64Binary")]
        public byte[] Sub_Item_Image { get; set; }
        public string Sub_Item_Remarks { get; set; }

        public string select_mapped_store_list { get; set; }//Mantis bug 0000334 fixed


        public Param_SubItem_Insert_store_Mapping[] Param_Sub_Item_Store_Mapping { get; set; }
        public Param_SubItem_Insert_Unit_Conversion[] Param_Sub_Item_Unit_Conversion { get; set; }
        public Param_SubItem_Insert_Item_Properties[] Param_Sub_Item_Item_Properties { get; set; }
        public List<GetPropertiesList_SubItem> itemProperties { get; set; }
        public List<GetUnitConversionList_SubItem> itemConversion { get; set; }
        
     
        public string PostSuccessFunction { get; set; }
        public string PopupName { get; set; }

      
    }
    [Serializable]
    public class GetPropertiesList_SubItem
    {
        public string SI_Prop_Item_Name { get; set; }
        [Required(ErrorMessage = "Enter Property Name")]
        public string SI_Prop_Property_Name { get; set; }
        [Required(ErrorMessage = "Enter Property Value")]
        public string SI_Prop_Property_Value { get; set; }
        public string SI_Prop_Remarks { get; set; }
        public string SI_Prop_ActionMethod { get; set; }
        public int SI_Prop_SrNo { get; set; }
        public int subitemid { get; set; }

        public GetPropertiesList_SubItem()
        {

        }
    }
    [Serializable]
    public class GetUnitConversionList_SubItem
    {
        public string Conversion_Item_Id { get; set; }//Mantis bug 0000334 fixed
        public string Conversion_Item_Name { get; set; }//Mantis bug 0000334 fixed
        public string ConvertedUnit_ActionMethod { get; set; }
        public int ConvertedUnit_SrNo { get; set; }
        public string Converted_Unit { get; set; }

        [Required(ErrorMessage = "Enter Unit ID")]
        public string Converted_UnitID { get; set; }

        [Required(ErrorMessage = "Enter Conversion Rate")]
        public decimal Rate_Of_Conversion { get; set; }

        [Required(ErrorMessage = "Enter Effective Date")]
        public DateTime Effective_Date { get; set; }
        public int subitemid { get; set; }
        public GetUnitConversionList_SubItem()
        {

        }
        public List<GetStoreList> GetStoreListItems { get; set; }
    }
    [Serializable]
    public class GetStoreList
    {
        public string Stock_Item_Name { get; set; }
        public string Item_Category { get; set; }
        public string Item_Type { get; set; }
        public string Item_Code { get; set; }
        public string Unit { get; set; }
        public string UnitID { get; set; }
        public int Item_ID { get; set; }
        public string StoreIDs_Mapped { get; set; }

        public GetStoreList()
        {

        }
    }
    


}