using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using static Common_Lib.DbOperations;
using static Common_Lib.Common;
using static Common_Lib.DbOperations.SubItems;
using Common_Lib.RealTimeService;
using static Common_Lib.DbOperations.StockDeptStores;
using System.IO;
using Common_Lib;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    public class StockItemWindowController : BaseController
    {
        // GET: Stock/StockItemWindow
        #region Global variables 
        public int SubItemID
        {
            get
            {
                return (int)GetBaseSession("SubItemID_Item");
            }
            set
            {
                SetBaseSession("SubItemID_Item", value);
            }
        }
        public string MainCategoryByDataLoad
        {
            get
            {
                return (string)GetBaseSession("MainCategoryByDataLoad_Item");
            }
            set
            {
                SetBaseSession("MainCategoryByDataLoad_Item", value);
            }
        }
        public string UnitRefresh
        {
            get
            {
                return (string)GetBaseSession("UnitRefresh_Item");
            }
            set
            {
                SetBaseSession("UnitRefresh_Item", value);
            }
        }
        public DataTable itemPropertyDataTable
        {
            get
            {
                return (DataTable)GetBaseSession("itemPropertyDataTable_Item");
            }
            set
            {
                SetBaseSession("itemPropertyDataTable_Item", value);
            }
        }
        public List<DbOperations.SubItems.Return_GetList> StockItemData
        {
            get
            {
                return (List<DbOperations.SubItems.Return_GetList>)GetBaseSession("StockItemData_ItemInfo");
            }
            set
            {
                SetBaseSession("StockItemData_ItemInfo", value);
            }
        }
        public byte[] SI_Image
        {
            get
            {
                return (byte[])GetBaseSession("SI_Image_Item");
            }
            set
            {
                SetBaseSession("SI_Image_Item", value);
            }
        }
        public List<Return_GetPropertiesList_SubItem> Item_Properties_grid_Data_Glob
        {
            get
            {
                return (List<Return_GetPropertiesList_SubItem>)GetBaseSession("Item_Properties_grid_Data_Glob_Item");
            }
            set
            {
                SetBaseSession("Item_Properties_grid_Data_Glob_Item", value);
            }
        }
        public List<Return_GetUnitConversionList_SubItem> Grid_Data_Item_GetUnitConversionList_grid_Data
        {
            get
            {
                return (List<Return_GetUnitConversionList_SubItem>)GetBaseSession("Grid_Data_Item_GetUnitConversionList_grid_Data_Item");
            }
            set
            {
                SetBaseSession("Grid_Data_Item_GetUnitConversionList_grid_Data_Item", value);
            }
        }
        public List<Return_GetFilteredStockItems> Map_Item_Grid_Data
        {
            get
            {
                return (List<Return_GetFilteredStockItems>)GetBaseSession("Map_Item_Grid_Data_Item");
            }
            set
            {
                SetBaseSession("Map_Item_Grid_Data_Item", value);
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public void SetDefaultValues()
        {
            SubItemID = 0;
            MainCategoryByDataLoad = "";
            UnitRefresh = "";
            itemPropertyDataTable = new DataTable();
        }
        #endregion

        #region Grid/Nested Grid
        public ActionResult Frm_Stock_Item_Info()
        {
            SetDefaultValues();
            Item_user_rights();      
            if (CheckRights(BASE, ClientScreen.Stock_Sub_Item, "List"))//Code written for User Authorization do not remove
            {
                ViewBag.ShowHorizontalBar = 0;
                var StockItems = BASE._Sub_Item_DBOps.GetList(null);

                var Itemgrid = StockItems;
                StockItemData = Itemgrid;

                List<DbOperations.SubItems.Return_GetList> grid_data = StockItemData as List<DbOperations.SubItems.Return_GetList>;
                Basic_Info_Item_Master();
                return PartialView(grid_data);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Sub_Item').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public void Basic_Info_Item_Master()
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
        }

        public ActionResult Frm_Stock_Item_Info_Grid(string command, int ShowHorizontalBar = 0, int? Store_ID = 0)
        {
            Item_user_rights();
            Basic_Info_Item_Master();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
      
            if (StockItemData == null || command == "REFRESH" || Store_ID != 0)
            {
                if (Store_ID == 0)
                {
                    Store_ID = null;
                }
                var StockItems = BASE._Sub_Item_DBOps.GetList(Store_ID);
                if (StockItems != null)
                {
                    var Itemgrid = StockItems;
                    StockItemData = Itemgrid;

                }
            }
            List<DbOperations.SubItems.Return_GetList> Mastergrid_data = StockItemData as List<DbOperations.SubItems.Return_GetList>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }

        public ActionResult StoreItemGrid(int Store_ID = 0)
        {
            var StockItems = BASE._Sub_Item_DBOps.GetList(Store_ID);

            var Itemgrid = StockItems;
            StockItemData = Itemgrid;

            Basic_Info_Item_Master();
            return PartialView("Frm_Stock_Item_Info_Grid", Itemgrid);
        }
        public ActionResult StockItemRowCustomDataAction(int? key)
        {
            var FinalRegData = StockItemData as List<DbOperations.SubItems.Return_GetList>;

            string StockReg = "";
            if (FinalRegData != null)
            {
                var FDData = FinalRegData.Where(f => f.REC_ID == key).FirstOrDefault();
                if (FDData != null)
                {
                    StockReg = FDData.REC_ID + "![" + FDData.REC_ADD_ON + "![" + FDData.REC_ADD_BY + "![" +
                           FDData.REC_EDIT_ON + "![" + FDData.REC_EDIT_BY + "![" + FDData.Closed_On + "![" + FDData.Consumption_Type
                           + "![" + FDData.Conversion_Units + "![" + FDData.Item_Name + "![" + FDData.Item_Properties + "![" + FDData.Item_Code + "![" + FDData.Sub_Category + "![" +
                           FDData.Primary_Unit + "![" + FDData.Main_Category + "![" + FDData.Accounting_Item + "![" + FDData.Store_Applicable;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(StockReg);
        }

        public ActionResult LookUp_GetStoreList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Storelist = BASE._StockDeptStores_dbops.GetStoreList();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Storelist, loadOptions)), "application/json");
        }
        #endregion

        #region NEVD

        public ActionResult DataNavigation(string ActionMethod, int ID, DateTime? Closed_On)
        {
            SubItemID = ID;
            StockItemWindow model = new StockItemWindow();
            if (ActionMethod == "CloseItem")
            {
                if (Closed_On != null)
                {
                    return Json(new { Message = "Already closed Item can not be closed..!", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "ReOpenItem")
            {
                if (Closed_On == null)
                {
                    return Json(new { Message = "Already opened item can not be re-opened..!", result = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var UsedStoreUnmapped = "";
                    if (!BASE._Sub_Item_DBOps.ReOpenSubItem(ID, ref UsedStoreUnmapped))
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            if (ActionMethod == "CloseCheck")
            {
                if (Closed_On != null)
                {
                    return Json(new { Message = "Selected Item is a closed Item.", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "MapItemStore")
            {
                if (Closed_On != null)
                {
                    return Json(new { Message = "Selected Items contain a closed Item. Closed Items cannot be mapped to Stores.!", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "MapItemSupplier")
            {
                if (Closed_On != null)
                {
                    return Json(new { Message = "Selected Items contain a closed Item. Closed Items cannot be mapped to Suppliers!", result = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (ActionMethod == "_Edit" || ActionMethod == "_Delete")
            {

                var IsUsed = BASE._Sub_Item_DBOps.GetUsageList(ID, ClientScreen.Stock_Sub_Item);
                if (IsUsed.Count > 0)
                {
                    if (ActionMethod == "_Delete")
                    {
                        return Json(new { Message = "Stock Item used in Any UO/ Job / Stock Profile / RR can not be deleted", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (ActionMethod == "_Edit")
                    {
                        return Json(new { Message = "Stock Item used in Any UO/Job/Stock Profile/RR can not be updated", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { Message = "", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMainCategories(DataSourceLoadOptions loadOptions)
        {
            var categorydata = BASE._Sub_Item_DBOps.GetMainCategoriesMaster();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(categorydata, loadOptions)), "application/json");
        }
        public ActionResult GetMainCategories_SMap(DataSourceLoadOptions loadOptions)
        {
            var categorydata = BASE._Sub_Item_DBOps.GetMainCategoriesMaster();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(categorydata, loadOptions)), "application/json");
        }

        public ActionResult GetsubCategoriesMaster(DataSourceLoadOptions loadOptions, string MainCategoryId = "")
        {

            var subcategorydata = BASE._Sub_Item_DBOps.GetSubCategoriesMaster(MainCategoryId);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(subcategorydata, loadOptions)), "application/json");
        }//Mantis bug 0000334 fixed   

        public ActionResult GetsubCategoriesMaster_SMap(DataSourceLoadOptions loadOptions, string MainCategoryId = "")
        {

            var subcategorydata = BASE._Sub_Item_DBOps.GetSubCategoriesMaster(MainCategoryId);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(subcategorydata, loadOptions)), "application/json");
        }

        public ActionResult GetStockUnits(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockProfile.Return_GetUnits> data = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }//Mantis bug 0000334 fixed

        public ActionResult GetMappedStoreList(DataSourceLoadOptions loadOptions)
        {
            //var ListData = new List<Return_GetStoreListForMap>();
            var ListData = BASE._StockDeptStores_dbops.GetStoreList();
            //ListData = data.Select(m => new Return_GetStoreListForMap
            //{
            //    StoreID = m.StoreID,
            //    Store_Name = m.Store_Name
            //}).ToList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ListData, loadOptions)), "application/json");
        }
        [HttpGet]
        public ActionResult StockItemWindowNew(string ActionMethod = null, int ID = 0, string PostSucessFunction = null, string PopupName = "popup_frm_StockItem_Window", string ButtonID = "StockItemNew")
        {
            var j = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "_New", "_Edit", "_View", "_Delete" };
            for (j = 0; j < Rights.Length; j++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Sub_Item, Rights[j]) && ActionMethod == AM[j])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            
            ItemMasterSessionclear();
            Basic_Info_Item_Master();
            StockItemWindow model = new StockItemWindow();
            model.ItemMaster_ActionMethod = ActionMethod;

            model.PostSuccessFunction = PostSucessFunction != null ? PostSucessFunction : "AjaxSuccessForItemWindow";
            model.PopupName = PopupName != null ? PopupName : "popup_frm_StockItem_Window";

            if (ActionMethod == "_New")
            {
                SI_Image = null;
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View" || ActionMethod == "_Delete")
            {

                var subitemdata = BASE._Sub_Item_DBOps.GetRecord(ID);

                model.SubItemID = ID;
                model.Sub_Item_Name = subitemdata.Item_Name;
                model.Sub_Item_Consumption_Type = subitemdata.Consumption_Type;
                model.Sub_Item_Accounting_Item = "c75de479-bdb6-48ba-9ebc-c3d495ae68f3";
                model.Sub_Item_Code = subitemdata.Item_Code;
                model.Sub_Item_Main_Category = subitemdata.Main_Category;
                model.Sub_Item_Sub_Category = subitemdata.Sub_Category;
                model.Sub_Item_Unit_Id = subitemdata.Primary_Unit;
                model.Sub_Item_Remarks = subitemdata.Remarks;
                for (int i = 0; i < subitemdata.MappedStores.Length-1; i++)
                {
                    if (model.select_mapped_store_list == null)
                    {
                        model.select_mapped_store_list = Convert.ToString(subitemdata.MappedStores[i]);
                    }
                    else
                    {
                        model.select_mapped_store_list = model.select_mapped_store_list + "," + subitemdata.MappedStores[i];
                    }
                }
                SI_Image = subitemdata.image;
                //  MainCategoryByDataLoad = Main_Category;
                //model.MainCategoryValue = Main_Category;

                //var data1 = BASE._StockDeptStores_dbops.GetStoreList();
                //var StoreData = data1.Select(q => new Return_GetStoreListForMap
                //{
                //    StoreID = q.StoreID,
                //    Store_Name = q.Store_Name
                //}).ToList();
                //model.MapStore = (List<Return_GetStoreListForMap>)StoreData;
            }

            //var data = BASE._Sub_Item_DBOps.GetPropertiesList_SubItem(SubItemID);
            //var PropData = data.Select(q => new GetPropertiesList_SubItem
            //{
            //    // SI_Prop_SrNo = q.SrNo,
            //    SI_Prop_Property_Name = q.Property_Name,
            //    SI_Prop_Property_Value = q.Property_Value,
            //    SI_Prop_Remarks = q.Remarks
            //}).ToList();

            //var unitData = BASE._Sub_Item_DBOps.GetUnitConversionList_SubItem(SubItemID);//Mantis bug 0000343 fixed
            //Grid_Data_Item_GetUnitConversionList_grid_Data = unitData;
            ////List<ItemProperties> itemProperties = DatatableToModel.GetItemProperties(DT);
            //var UnitDataSelect = unitData.Select(q => new GetUnitConversionList_SubItem
            //{
            //    SrNo = q.SrNo,
            //    Converted_Unit = q.Converted_Unit,
            //    Effective_Date = q.Effective_Date,
            //    Rate_Of_Conversion = q.Rate_Of_Conversion
            //}).ToList();

            //Grid_Data_Item_GetUnitConversionList_grid_Data = UnitDataSelect;
            //model.itemConversion = (List<GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
            //Item_Properties_grid_Data_Glob = PropData;
            //model.itemProperties = (List<GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
            ViewData["SI_Image"] = SI_Image;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SaveItemWindow(StockItemWindow model)
        {
            try
            {
                if (model.ItemMaster_ActionMethod == "_New" || model.ItemMaster_ActionMethod == "_Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.Sub_Item_Name))
                    {
                        return Json(new
                        {
                            message = "Item Name cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Sub_Item_Name = model.Sub_Item_Name.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                }

                if(model.Sub_Item_Remarks != null)
                {
                    model.Sub_Item_Remarks = model.Sub_Item_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                if (model.ItemMaster_ActionMethod == "_New")
                {
                    Param_SubItem_Insert_Txn param_SubItem_Insert_Txn = new Param_SubItem_Insert_Txn();

                    param_SubItem_Insert_Txn.Sub_Item_Unit_Id = model.Sub_Item_Unit_Id == null ? "" : model.Sub_Item_Unit_Id;//Mantis bug 0000334 fixed
                    param_SubItem_Insert_Txn.Sub_Item_Item_Id = "ce025c3a-eda4-46c5-84aa-64e004769869";
                    param_SubItem_Insert_Txn.Sub_Item_Name = model.Sub_Item_Name == null ? "" : model.Sub_Item_Name;
                    param_SubItem_Insert_Txn.Sub_Item_Main_Category = model.Sub_Item_Main_Category == null ? "" : model.Sub_Item_Main_Category;
                    param_SubItem_Insert_Txn.Sub_Item_Sub_Category = model.Sub_Item_Sub_Category == null ? "" : model.Sub_Item_Sub_Category;
                    param_SubItem_Insert_Txn.Sub_Item_Code = model.Sub_Item_Code == null ? "" : model.Sub_Item_Code;
                    param_SubItem_Insert_Txn.Sub_Item_Image = (byte[])SI_Image;
                    param_SubItem_Insert_Txn.Sub_Item_Remarks = model.Sub_Item_Remarks == null ? "" : model.Sub_Item_Remarks;
                    param_SubItem_Insert_Txn.Sub_Item_Consumption_Type = model.Sub_Item_Consumption_Type == null ? "" : model.Sub_Item_Consumption_Type;//Mantis bug 0000334 fixed


                    var prpdata = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
                    if (prpdata != null)
                    {
                        var obj = new Param_SubItem_Insert_Item_Properties[prpdata.Count()];
                        for (int i = 0; i < prpdata.Count(); i++)
                        {
                            Param_SubItem_Insert_Item_Properties param_SubItem_Insert_Item_Properties = new Param_SubItem_Insert_Item_Properties();
                            param_SubItem_Insert_Item_Properties.Property_Name = prpdata[i].Property_Name;
                            param_SubItem_Insert_Item_Properties.Property_Value = prpdata[i].Property_Value;
                            //param_SubItem_Insert_Item_Properties.
                            param_SubItem_Insert_Item_Properties.Sub_Item_ID = (int)model.SubItemID;
                            obj[i] = param_SubItem_Insert_Item_Properties;
                        }
                        param_SubItem_Insert_Txn.Param_Sub_Item_Item_Properties = obj;
                    }
                    var unitData = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
                    if (unitData != null)
                    {
                        var Unitobj = new Param_SubItem_Insert_Unit_Conversion[unitData.Count()];
                        for (int j = 0; j < unitData.Count(); j++)
                        {
                            Param_SubItem_Insert_Unit_Conversion param_SubItem_Insert_Unit_Conversion = new Param_SubItem_Insert_Unit_Conversion();
                            param_SubItem_Insert_Unit_Conversion.Sub_Item_ID = (int)model.SubItemID;
                            param_SubItem_Insert_Unit_Conversion.Unit_ID = unitData[j].Converted_UnitID;
                            param_SubItem_Insert_Unit_Conversion.Rate_for_Conversion = unitData[j].Rate_Of_Conversion;
                            param_SubItem_Insert_Unit_Conversion.Effective_From = unitData[j].Effective_Date;

                            Unitobj[j] = param_SubItem_Insert_Unit_Conversion;
                        }
                        param_SubItem_Insert_Txn.Param_Sub_Item_Unit_Conversion = Unitobj;
                    }

                    var Param_Sub_Item_Store_Mapping = new List<Param_SubItem_Insert_store_Mapping>();

                    if (model.select_mapped_store_list != null)
                    {

                        var store_mapping = model.select_mapped_store_list.Split(',');
                        var storemappingtobj = new Param_SubItem_Insert_store_Mapping[store_mapping.Length];
                        if (store_mapping.Length > 0)
                        {
                            for (int i = 0; i < store_mapping.Length; i++)
                            {
                                Param_SubItem_Insert_store_Mapping newrow = new Param_SubItem_Insert_store_Mapping();
                                newrow.Store_ID = Convert.ToInt32(store_mapping[i]);
                                newrow.Sub_Item_ID = 0;
                                storemappingtobj[i] = newrow;
                            }
                            param_SubItem_Insert_Txn.Param_Sub_Item_Store_Mapping = storemappingtobj;
                        }
                    }//Mantis bug 0000334 fixed
                    //var Image_data = Session["image_Bytes"] as byte[];
                    //byte[] Image = new byte[0];
                    //if (Image_data != null)
                    //{
                    //    model.Sub_Item_Image = SI_Image;
                    //}
                  
                    //param_SubItem_Insert_Txn.Param_Sub_Item_Store_Mapping = Param_Sub_Item_Store_Mapping.ToArray();//Mantis bug 0000334 fixed



                    if (!BASE._Sub_Item_DBOps.InsertSubItem(param_SubItem_Insert_Txn))
                    {
                        return Json(new { message = "Error..!!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "Successfully Saved ", result = true }, JsonRequestBehavior.AllowGet);//Mantis bug 0000334 fixed
                    }
                }

                if (model.ItemMaster_ActionMethod == "_Edit")
                {
                    Param_SubItem_Update_Txn param_SubItem_Update_Txn = new Param_SubItem_Update_Txn();
                    var prpdata = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
                    if (prpdata != null)
                    {
                        var obj = new Param_SubItem_Insert_Item_Properties[prpdata.Count()];
                        for (int i = 0; i < prpdata.Count(); i++)
                        {
                            Param_SubItem_Insert_Item_Properties param_SubItem_Insert_Item_Properties = new Param_SubItem_Insert_Item_Properties();
                            param_SubItem_Insert_Item_Properties.Property_Name = prpdata[i].Property_Name;
                            param_SubItem_Insert_Item_Properties.Property_Value = prpdata[i].Property_Value;
                            param_SubItem_Insert_Item_Properties.Sub_Item_ID = (int)model.SubItemID;
                            obj[i] = param_SubItem_Insert_Item_Properties;
                        }
                        param_SubItem_Update_Txn.Param_Sub_Item_Item_Properties = obj;
                    }

                    var unitData = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
                    if (unitData != null)
                    {
                        var Unitobj = new Param_SubItem_Insert_Unit_Conversion[unitData.Count()];
                        for (int j = 0; j < unitData.Count(); j++)
                        {
                            Param_SubItem_Insert_Unit_Conversion param_SubItem_Insert_Unit_Conversion = new Param_SubItem_Insert_Unit_Conversion();
                            param_SubItem_Insert_Unit_Conversion.Sub_Item_ID = (int)model.SubItemID;
                            param_SubItem_Insert_Unit_Conversion.Unit_ID = unitData[j].Converted_UnitID;
                            param_SubItem_Insert_Unit_Conversion.Rate_for_Conversion = unitData[j].Rate_Of_Conversion;
                            param_SubItem_Insert_Unit_Conversion.Effective_From = unitData[j].Effective_Date;

                            Unitobj[j] = param_SubItem_Insert_Unit_Conversion;
                        }
                        param_SubItem_Update_Txn.Param_Sub_Item_Unit_Conversion = Unitobj;
                    }
                    //var Param_Sub_Item_Store_Mapping = new Param_SubItem_Insert_store_Mapping[0];
                    //model.Sub_Item_Image = (byte[])SI_Image;

                    var Param_Sub_Item_Store_Mapping = new List<Param_SubItem_Insert_store_Mapping>();

                    if (model.select_mapped_store_list != null)
                    {

                        var store_mapping = model.select_mapped_store_list.Split(',');
                        var storemappingtobj = new Param_SubItem_Insert_store_Mapping[store_mapping.Length];
                        if (store_mapping.Length > 0)
                        {
                            for (int i = 0; i < store_mapping.Length; i++)
                            {
                                Param_SubItem_Insert_store_Mapping newrow = new Param_SubItem_Insert_store_Mapping();
                                newrow.Store_ID = Convert.ToInt32(store_mapping[i]);
                                newrow.Sub_Item_ID = (int)model.SubItemID; 
                                storemappingtobj[i] = newrow;
                            }
                            param_SubItem_Update_Txn.Param_Sub_Item_Store_Mapping = storemappingtobj;
                        }
                    }//Mantis bug 0000334 fixed

                    param_SubItem_Update_Txn.Sub_Item_ID = (int)model.SubItemID;
                    param_SubItem_Update_Txn.Sub_Item_Unit_Id = model.Sub_Item_Unit_Id;
                    param_SubItem_Update_Txn.Sub_Item_Item_Id = "ce025c3a-eda4-46c5-84aa-64e004769869";
                    param_SubItem_Update_Txn.Sub_Item_Name = model.Sub_Item_Name == null ? "" : model.Sub_Item_Name;
                    param_SubItem_Update_Txn.Sub_Item_Main_Category = model.Sub_Item_Main_Category == null ? "" : model.Sub_Item_Main_Category;
                    param_SubItem_Update_Txn.Sub_Item_Sub_Category = model.Sub_Item_Sub_Category == null ? "" : model.Sub_Item_Sub_Category;
                    param_SubItem_Update_Txn.Sub_Item_Code = model.Sub_Item_Code == null ? "" : model.Sub_Item_Code;
                    param_SubItem_Update_Txn.Sub_Item_Image = (byte[])SI_Image;
                    param_SubItem_Update_Txn.Sub_Item_Remarks = model.Sub_Item_Remarks == null ? "" : model.Sub_Item_Remarks;
                    param_SubItem_Update_Txn.Sub_Item_Consumption_Type = model.Sub_Item_Consumption_Type == null ? "" : model.Sub_Item_Consumption_Type;
                    //param_SubItem_Update_Txn.Param_Sub_Item_Store_Mapping = Param_Sub_Item_Store_Mapping;
                    //param_SubItem_Update_Txn.Param_Sub_Item_Unit_Conversion = Unitobj;
                    //param_SubItem_Update_Txn.Param_Sub_Item_Item_Properties = obj;


                    if (BASE._Sub_Item_DBOps.UpdateSubItem(param_SubItem_Update_Txn))
                    {
                        return Json(new { message = "Updated Successfully", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ItemMaster_ActionMethod == "_Delete")
                {
                    if (BASE._Sub_Item_DBOps.DeleteSubItem((int)model.SubItemID))
                    {
                        return Json(new { message = "Deleted Successfully", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { message = "Error", result = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckMappedStoreUsageCount(int ID, int storeid)
        {
            var storelist = BASE._Sub_Item_DBOps.GetUsageList(ID, ClientScreen.Stock_Sub_Item, storeid);
            if (storelist.Count > 0)
            {
                return Json(new
                {
                    Message = "",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Image
        public ActionResult Upload()
        {
            var myFile = Request.Files["SI_myFile"];
            string[] imageExtensions = { ".jpg", ".jpeg", ".png" };
            var fileName = myFile.FileName.ToLower();
            var isValidExtenstion = imageExtensions.Any(ext =>
            {
                return fileName.LastIndexOf(ext) > -1;
            });
            if (isValidExtenstion)
            {
                BinaryReader reader = new BinaryReader(myFile.InputStream);
                byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);
                SI_Image = imageBytes;
            }
            return new EmptyResult();
        }
        public void RemoveSIImage()
        {
            SI_Image = null;
        }
        public ActionResult SI_PreviewImageControl()
        {
            ViewData["SI_Image"] = SI_Image;
            return View();
        }

        public ActionResult ViewStockImage(int ID = 0)
        {
            StockItemWindow model = new StockItemWindow();
            var subitemdata = BASE._Sub_Item_DBOps.GetRecord(ID);
            if (subitemdata.image == null)
            {
                return Json(new
                { message = "No Image uploaded",            
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }   
            SI_Image = subitemdata.image;
            ViewData["SI_Image"] = SI_Image;
            return PartialView(model);
        }

        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#StockItem_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#StockItemListPreview').hide();</script>");
            }
            return View();
        }

        public ActionResult Frm_Export_Options_Property()
        {
           
            return View();
        }
        #endregion

        #region Properties
        public ActionResult GetItemPropertiesMaster(DataSourceLoadOptions loadOptions)
        {
            //Sub_Category
            //Construction Material
            var data = BASE._Sub_Item_DBOps.GetItemPropertiesMaster();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        public ActionResult Item_Properties_grid_Data(string ActionMethodName, int SubItemID = 0)
        {           
            Basic_Info_Item_Master();
            var ItemPropList = new List<Return_GetPropertiesList_SubItem>();
            if (ActionMethodName == "_New")
            {
                return PartialView(ItemPropList);
            }

            if (Item_Properties_grid_Data_Glob == null)
            {
                //    var Propertydata = (List<GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
                ////List<GetPropertiesList_SubItem> itemProperties = new List<GetPropertiesList_SubItem>();
                //if (Propertydata == null)
                //{
                var Item_Prop_Grid_data = BASE._Sub_Item_DBOps.GetPropertiesList_SubItem((int)SubItemID);
                if (Item_Prop_Grid_data != null)
                {
                    ItemPropList = Item_Prop_Grid_data;
                }
                Item_Properties_grid_Data_Glob = ItemPropList;
            }
            return PartialView(Item_Properties_grid_Data_Glob);
        }

        public ActionResult ItemPropertiesCustomDataAction(int key = 0)
        {
            var FinalData = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
            var it = FinalData.Where(f => f.SrNo == key).FirstOrDefault();
            string istr = "";
            if (it != null)
            {
                istr = it.SrNo + "![" + it.Property_Name + "![" + it.Property_Value + "![" + it.Remarks;
            }
            return GridViewExtension.GetCustomDataCallbackResult(istr);
        }

        [HttpGet]
        public ActionResult frm_Item_Window_Add_Property(string ActionMethod = null, int SrNo = 0, int subitemid = 0, string Itemname = null)
        {
            GetPropertiesList_SubItem model = new GetPropertiesList_SubItem();

            model.SI_Prop_ActionMethod = ActionMethod;
            model.subitemid = subitemid;
            if (ActionMethod == "_New")
            {
                if (subitemid != 0)
                {
                    model.SI_Prop_Item_Name = Itemname;
                }
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View" || ActionMethod == "_Delete")
            {
                var Sr = Convert.ToInt16(SrNo);
                var all_data = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
                var dataToEdit = all_data != null ? all_data.Where(x => x.SrNo == Sr).FirstOrDefault() : new Return_GetPropertiesList_SubItem();

                model.SI_Prop_SrNo = Sr;
                model.SI_Prop_Property_Name = dataToEdit.Property_Name;
                model.SI_Prop_Property_Value = dataToEdit.Property_Value;
                model.SI_Prop_Remarks = dataToEdit.Remarks;

            }


            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SavePropertyClick(GetPropertiesList_SubItem model)
        {


            if (model.subitemid != 0)
            {
                try
                {
                    if (model.SI_Prop_Remarks != null)
                    {
                        model.SI_Prop_Remarks = model.SI_Prop_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    Param_SubItem_Insert_Item_Properties inparam = new Param_SubItem_Insert_Item_Properties();
                    inparam.Sub_Item_ID = model.subitemid;
                    inparam.Property_Name = model.SI_Prop_Property_Name;
                    inparam.Property_Value = model.SI_Prop_Property_Value;
                    inparam.Property_Remarks = model.SI_Prop_Remarks;



                    if (BASE._Sub_Item_DBOps.InsertItemProperties(inparam))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }


                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        Message = message,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                // var Propertydata = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;

                List<Return_GetPropertiesList_SubItem> getPropertiesList_SubItem = new List<Return_GetPropertiesList_SubItem>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Item_Properties_grid_Data_Glob != null)
                {
                    getPropertiesList_SubItem = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
                    gridRowsCount = getPropertiesList_SubItem.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(getPropertiesList_SubItem[gridRowsCount - 1].SrNo) : 0;
                    NewSr = LastRowSr + 1;
                }

                    if (model.SI_Prop_ActionMethod == "_New")
                {
                    Return_GetPropertiesList_SubItem PropertiesLisObj = new Return_GetPropertiesList_SubItem();
                    PropertiesLisObj.SrNo = NewSr;
                    PropertiesLisObj.Property_Name = model.SI_Prop_Property_Name;
                    PropertiesLisObj.Property_Value = model.SI_Prop_Property_Value;
                    PropertiesLisObj.Remarks = model.SI_Prop_Remarks;
                    getPropertiesList_SubItem.Add(PropertiesLisObj);

                }
                else if (model.SI_Prop_ActionMethod == "_Edit")
                {
                    var dataToEdit = getPropertiesList_SubItem.FirstOrDefault(x => x.SrNo == model.SI_Prop_SrNo);
                    if (dataToEdit != null)
                    {
                        dataToEdit.Property_Name = model.SI_Prop_Property_Name;
                        dataToEdit.Property_Value = model.SI_Prop_Property_Value;
                        dataToEdit.Remarks = model.SI_Prop_Remarks;

                    }
                }
                Item_Properties_grid_Data_Glob = getPropertiesList_SubItem;
                return Json(new { message = "Saved Successfully", result = true }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Frm_Property_Grid_Delete_Record(string ActionMethod, int? SrNo = null)
        {
            var Sr = Convert.ToInt16(SrNo);
            var allData = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;
            var dataToDelete = allData != null ? allData.Where(x => x.SrNo == Sr).FirstOrDefault() : new Return_GetPropertiesList_SubItem();

            if (allData != null)
            {

                allData.Remove(dataToDelete);
            }
            Item_Properties_grid_Data_Glob = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Unit Conversion
        public ActionResult GetStockUnits_UnitConversion(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockProfile.Return_GetUnits> data = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }//Mantis bug 0000334 fixed

        public ActionResult UnitConversionList_SubItem_Grid_Data(string ActionMethodName, int SubItemID = 0)
        {

            var UnitData = new List<Return_GetUnitConversionList_SubItem>();


            if (ActionMethodName == "_New")
            {
                return PartialView(UnitData);
            }

            if (Grid_Data_Item_GetUnitConversionList_grid_Data == null)
            {
                var Unit_Grid_data = BASE._Sub_Item_DBOps.GetUnitConversionList_SubItem(SubItemID);

                if (Unit_Grid_data != null)
                {
                    UnitData = Unit_Grid_data;
                }
                Grid_Data_Item_GetUnitConversionList_grid_Data = UnitData;
            }
            return PartialView(Grid_Data_Item_GetUnitConversionList_grid_Data);
        }

        public ActionResult UnitConversionCustomDataAction(int key = 0)
        {
            var FinalData = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
            var it = FinalData.Where(f => f.SrNo == key).FirstOrDefault();
            string istr = "";
            if (it != null)
            {
                istr = it.SrNo + "![" + it.Converted_Unit + "![" + it.Effective_Date + "![" + it.Rate_Of_Conversion + "![" + it.Converted_UnitID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(istr);
        }
        [HttpGet]
        public ActionResult frm_Item_Window_Add_Unit_conversion(string ActionMethod = null, int SrNo = 0, int subitemid = 0, string Itemname = null)
        {
            GetUnitConversionList_SubItem model = new GetUnitConversionList_SubItem();
            model.ConvertedUnit_ActionMethod = ActionMethod;
            model.subitemid = subitemid;
            if (ActionMethod == "_New")
            {
                model.Effective_Date = DateTime.Now;
                if (subitemid != 0)
                {
                    model.Conversion_Item_Name = Itemname;
                }

            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View" || ActionMethod == "_Delete")
            {
                var Sr = Convert.ToInt16(SrNo);
                var all_data = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.SrNo == Sr).FirstOrDefault() : new Return_GetUnitConversionList_SubItem();
                model.ConvertedUnit_SrNo = Sr;
                model.Converted_UnitID = dataToEdit.Converted_UnitID;
                model.Rate_Of_Conversion = dataToEdit.Rate_Of_Conversion;
                model.Effective_Date = dataToEdit.Effective_Date;
                model.Converted_Unit = dataToEdit.Converted_Unit;

            }

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult SaveUnitClick(GetUnitConversionList_SubItem model)
        {
            if (model.ConvertedUnit_ActionMethod == "_New" | model.ConvertedUnit_ActionMethod == "_Edit")
            {

                if (model.Rate_Of_Conversion <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Rate of conversion is a mandatory field...!"
                    }, JsonRequestBehavior.AllowGet);
                }
              
            }
            if (model.subitemid != 0)
            {
                try
                {
                    
                    Param_SubItem_Insert_Unit_Conversion inparam = new Param_SubItem_Insert_Unit_Conversion();
                    inparam.Sub_Item_ID = model.subitemid;
                    inparam.Unit_ID = model.Converted_UnitID;
                    inparam.Rate_for_Conversion = model.Rate_Of_Conversion;
                    inparam.Effective_From = model.Effective_Date;





                    if (BASE._Sub_Item_DBOps.InsertItemUnitconversion(inparam))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }


                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        Message = message,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                // var Propertydata = (List<Return_GetPropertiesList_SubItem>)Item_Properties_grid_Data_Glob;

                List<Return_GetUnitConversionList_SubItem> getUnitList_SubItem = new List<Return_GetUnitConversionList_SubItem>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Grid_Data_Item_GetUnitConversionList_grid_Data != null)
                {
                    getUnitList_SubItem = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
                    gridRowsCount = getUnitList_SubItem.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(getUnitList_SubItem[gridRowsCount - 1].SrNo) : 0;
                    NewSr = LastRowSr + 1;
                }

            



                if (model.ConvertedUnit_ActionMethod == "_New")
                {
                    Return_GetUnitConversionList_SubItem UnitLisObj = new Return_GetUnitConversionList_SubItem();
                    UnitLisObj.SrNo = NewSr;
                    UnitLisObj.Converted_UnitID = model.Converted_UnitID;
                    UnitLisObj.Rate_Of_Conversion = model.Rate_Of_Conversion;
                    UnitLisObj.Effective_Date = model.Effective_Date;
                    UnitLisObj.Converted_Unit = model.Converted_Unit;
                    getUnitList_SubItem.Add(UnitLisObj);

                }
                else if (model.ConvertedUnit_ActionMethod == "_Edit")
                {
                    var dataToEdit = getUnitList_SubItem.FirstOrDefault(x => x.SrNo == model.ConvertedUnit_SrNo);
                    if (dataToEdit != null)
                    {

                        dataToEdit.Converted_UnitID = model.Converted_UnitID;
                        dataToEdit.Rate_Of_Conversion = model.Rate_Of_Conversion;
                        dataToEdit.Effective_Date = model.Effective_Date;
                        dataToEdit.Converted_Unit = model.Converted_Unit;

                    }
                }
                Grid_Data_Item_GetUnitConversionList_grid_Data = getUnitList_SubItem;
                return Json(new { message = "Saved Successfully", result = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_Unit_Grid_Delete_Record(string ActionMethod, int? SrNo = null)
        {
            var Sr = Convert.ToInt16(SrNo);
            var allData = (List<Return_GetUnitConversionList_SubItem>)Grid_Data_Item_GetUnitConversionList_grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.SrNo == Sr).FirstOrDefault() : new Return_GetUnitConversionList_SubItem();

            if (allData != null)
            {

                allData.Remove(dataToDelete);
            }
            Grid_Data_Item_GetUnitConversionList_grid_Data = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }




        #endregion

        #region Map Store Window

        public ActionResult LookUp_StoreList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Storelistdata = BASE._StockDeptStores_dbops.GetStoreList();
            //var list = Storelist.Select(q => new StoreListInfo
            //{
            //    Store_Name = q.Store_Name,
            //    Store_Id = q.StoreID,
            //    Dept_Name = q.Dept_Name,
            //    SubDept_Name = q.Sub_Dept_Name,
            //    StoreInchargeName = q.Store_Incharge_Name,
            //    DeptInchargeName = q.Dept_Incharge_Name
            //}).ToList();
            //DataTable dt = ToDataTable(list);
            //var Storelistdata = DatatableToModel.LookUp_GetStoreList(dt);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Storelistdata, loadOptions)), "application/json");
        }

        public ActionResult FindItemGridKeyValue()
        {
            var griddata = Map_Item_Grid_Data as List<Return_GetFilteredStockItems>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = Convert.ToString(griddata[i].Item_ID);
                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult MapItemData(string Main_Category = null, string Sub_Category = null, string MapItemConsumption_Type = null, string Search_Text = null, int? Store_ID = null)

        {
           
            Common_Lib.RealTimeService.Param_GetFilteredStockItems Param = new Common_Lib.RealTimeService.Param_GetFilteredStockItems();
            Param.Consumption_Type = object.ReferenceEquals(MapItemConsumption_Type, "") ? null : MapItemConsumption_Type;
            Param.Main_Category = object.ReferenceEquals(Main_Category, "") ? null : Main_Category;
            Param.Sub_Category = object.ReferenceEquals(Sub_Category, "") ? null : Sub_Category;
            Param.Search_Text = object.ReferenceEquals(Search_Text, "") ? null : Search_Text;
            Param.StoreID = object.ReferenceEquals(Store_ID, "") ? null : Store_ID;
            List<Return_GetFilteredStockItems> MapItems = BASE._Sub_Item_DBOps.GetFilteredStockItems(Param);
            //if (MapItems.Count > 0)
            //{
            //    var MapCheck = MapItems[0].StoreIDs_Mapped;
            //    var Item_Id = MapItems[0].Item_ID;
            //    Session["MappedCheck"] = MapCheck;
            //}

            Map_Item_Grid_Data = MapItems;
            return PartialView("MapItemGridData", MapItems);
        }



        public ActionResult OnAllSelect()
        {


            var all_Mapped_Grid_data = (List<Return_GetFilteredStockItems>)Map_Item_Grid_Data;
            if (all_Mapped_Grid_data != null)
            {
                for (int I = 0; I <= all_Mapped_Grid_data.Count() - 1; I++)
                {


                    if (all_Mapped_Grid_data[I].StoreIDs_Mapped == true)
                    {

                    }
                    else
                    {
                        all_Mapped_Grid_data[I].StoreIDs_Mapped = true;
                    }

                }
            }
        

            return Json(new
            {
                result = true,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnChangeChecked(int ID = 0, int StoreID = 0, string Mapstatus = null)
        {


            var all_Mapped_Grid_data = (List<Return_GetFilteredStockItems>)Map_Item_Grid_Data;
            if (all_Mapped_Grid_data != null)
            {
                for (int I = 0; I <= all_Mapped_Grid_data.Count() - 1; I++)
                {
                    if (all_Mapped_Grid_data[I].Item_ID == ID)
                    {
                        if (Mapstatus == "true")
                        {
                            if (all_Mapped_Grid_data[I].StoreIDs_Mapped == true)
                            {
                            }
                            else
                            {
                                all_Mapped_Grid_data[I].StoreIDs_Mapped = true;
                            }
                        }
                        else if (Mapstatus == "false")
                        {
                            if (all_Mapped_Grid_data[I].StoreIDs_Mapped == true)
                            {
                                var countusage = BASE._Sub_Item_DBOps.GetUsageList(ID, ClientScreen.Stock_Sub_Item, StoreID);
                                if (countusage.Count > 0)
                                {

                                    return Json(new { Message = "Any Store Item against which any UO/TO/PO/RR/Job/Project has been created in selected store, can not be de-allocated", result = false }, JsonRequestBehavior.AllowGet);

                                }
                                else
                                {
                                    all_Mapped_Grid_data[I].StoreIDs_Mapped = false;
                                }
                            }
                            else
                            {
                              
                            }


                        }
                    }
                }
            }

            return Json(new
            {
                result = true,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapItemGridData(bool Mapped_Item = false)
        {
            //ViewBag.MappedItemCheck = Session["MappedCheck"];
            if (Map_Item_Grid_Data != null)
            {
                return PartialView("MapItemGridData", Map_Item_Grid_Data);
            }
            return null;
        }

        public ActionResult MapItem_Grid_Custom_Data(int key = 0)
        {
            var FinalRegData = Map_Item_Grid_Data as List<Return_GetFilteredStockItems>;
            var FDData = FinalRegData != null ? FinalRegData.Where(f => f.Item_ID == key).FirstOrDefault() : null;
            string MapItemReg = "";
            if (FDData != null)
            {
                MapItemReg = FDData.Item_ID + "," + FDData.UnitID + "," + FDData.StoreIDs_Mapped + "," +
                           FDData.Stock_Item_Name;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MapItemReg);
        }
        public ActionResult Frm_Stock_Item_Map(int recId = 0, string ItemName = null, int? Store_ID = null)
        {
            MapIteminfo model = new MapIteminfo();

            if (Store_ID != null)
            {
                model.MapItem_Store_id = Store_ID;
            }
            else if (Store_ID == null)
            {
                model.MapItem_Store_id = null;
            }
            

            model.Search_Text = ItemName;
            return View(model);
        }
        public ActionResult Frm_Stock_Item_Map_Save(MapIteminfo model)
        {
            try
            {
                var mapped_items_id = string.Empty;
                var unmapped_items_id = string.Empty;
                var Store_ID = model.MapItem_Store_id;
                if (model.MapItem_Store_id == null)
                {
                    return Json(new { Message = "Store Item Can not be blank...!", result = false }, JsonRequestBehavior.AllowGet);
                }
                var alldata = Map_Item_Grid_Data as List<Return_GetFilteredStockItems>;

                if (alldata != null)
                {
                    for (int i = 0; i < alldata.Count; i++)
                    {

                        if (alldata[i].StoreIDs_Mapped)
                        {
                            if (mapped_items_id == "")
                            {
                                mapped_items_id = Convert.ToString(alldata[i].Item_ID);
                            }
                            else
                            {
                                mapped_items_id = mapped_items_id + ',' + Convert.ToString(alldata[i].Item_ID);
                            }
                        }

                        else if (!alldata[i].StoreIDs_Mapped)
                        {
                            if (unmapped_items_id == "")
                            {
                                unmapped_items_id = Convert.ToString(alldata[i].Item_ID);
                            }
                            else
                            {
                                unmapped_items_id = unmapped_items_id + ',' + Convert.ToString(alldata[i].Item_ID);
                            }
                        }


                    }
                }



                if (!BASE._Sub_Item_DBOps.UpdateSubItem_Store_Mapping(Convert.ToInt32(Store_ID), mapped_items_id, unmapped_items_id))
                {
                    return Json(new
                    {
                        Message = "Error!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    Message = "Updated Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Closed Item
        public ActionResult Frm_Stock_Item_Close(int recId = 0, string ItemName = null)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Update"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#popup_frm_StockItem_Window_Close').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#StockCloseItem').hide();</script>");
            }
            CloseItemInfo model = new CloseItemInfo();
            model.Item_Name = ItemName;
            model.SubItemID = recId;
            model.CloseDate = DateTime.Now;
            return View(model);
        }
        public ActionResult Frm_Stock_Item_Closed_Save(CloseItemInfo model)
        {
            try
            {
                if (model.CloseDate == null)
                {
                    return Json(new { Message = "Close Date Can not be blank...!", result = false }, JsonRequestBehavior.AllowGet);
                }
                if (model.CloseRemarks == null || string.IsNullOrEmpty(model.CloseRemarks))
                {
                    return Json(new { Message = "Close Remarks Can not be blank...!", result = false }, JsonRequestBehavior.AllowGet);
                }
                if (model.CloseRemarks != null)
                {
                    model.CloseRemarks = model.CloseRemarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                Common_Lib.RealTimeService.Param_CloseSubItem Param = new Common_Lib.RealTimeService.Param_CloseSubItem();
                Param.Sub_Item_ID = model.SubItemID;
                Param.CloseDate = model.CloseDate;
                Param.CloseRemarks = model.CloseRemarks;
                var UsedStoreUnmapped = "";
                if (!BASE._Sub_Item_DBOps.CloseSubItem(model.SubItemID, model.CloseDate, model.CloseRemarks, ref UsedStoreUnmapped))
                {
                    return Json(new
                    {
                        Message = "Error!!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    Message = "Updated Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(
                    new
                    {
                        Message = ex.Message,
                        result = false
                    }
                    , JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region MISC

        public void Item_user_rights()
        {
            ViewData["Item_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");
            ViewData["Item_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Update");
            ViewData["Item_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "View");
            ViewData["Item_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Delete");
            ViewData["Item_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Export");            
            ViewData["Item_ListRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "List");        
        }
        public void ItemMasterSessionclear()
        {            
            ClearBaseSession("_Item");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_ItemInfo");
        }
        #endregion

    }

}
   