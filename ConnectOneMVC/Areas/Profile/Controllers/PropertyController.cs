using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class PropertyController : BaseController
    {
        // GET: Profile/Property
        #region Global Variables
        public string defaultPropCategory
        {
            get
            {
                return (string)GetBaseSession("defaultPropCategory_PropertyWindow");
            }
            set { SetBaseSession("defaultPropCategory_PropertyWindow", value); }
        }
        public string EntryType
        {
            get
            {
                return (string)GetBaseSession("EntryType_PropertyWindow");
            }
            set { SetBaseSession("EntryType_PropertyWindow", value); }
        }
        public string YearID
        {
            get
            {
                return (string)GetBaseSession("YearID_PropertyWindow");
            }
            set { SetBaseSession("YearID_PropertyWindow", value); }
        }
        public List<ADDRESS_BOOK> OwnerList_DD_LBW
        {
            get { return (List<ADDRESS_BOOK>)GetBaseSession("OwnerList_DD_PropertyWindow"); }
            set { SetBaseSession("OwnerList_DD_PropertyWindow", value); }
        }
        public List<Return_StateList> StateList_DD_LBW
        {
            get
            {
                return (List<Return_StateList>)GetBaseSession("LBW_StateList_PropertyWindow");
            }
            set
            {
                SetBaseSession("LBW_StateList_PropertyWindow", value);
            }
        }
        public List<Return_DistrictList> DistrictList_DD_LBW
        {
            get
            {
                return (List<Return_DistrictList>)GetBaseSession("LBW_DistrictList_PropertyWindow");
            }
            set
            {
                SetBaseSession("LBW_DistrictList_PropertyWindow", value);
            }
        }
        public List<Return_CityList> CityList_DD_LBW
        {
            get
            {
                return (List<Return_CityList>)GetBaseSession("LBW_CityList_PropertyWindow");
            }
            set
            {
                SetBaseSession("LBW_CityList_PropertyWindow", value);
            }
        }
        public List<LookUp_GetInsList_Info> Institution_DD_LBW
        {
            get
            {
                return (List<LookUp_GetInsList_Info>)GetBaseSession("LBW_InstitutionList_PropertyWindow");
            }
            set
            {
                SetBaseSession("LBW_InstitutionList_PropertyWindow", value);
            }
        }
        public List<Property_Window_Grid> Ext_property_Data
        {
            get
            {
                return (List<Property_Window_Grid>)GetBaseSession("Ext_property_Data_PropertyWindow");
            }
            set
            {
                SetBaseSession("Ext_property_Data_PropertyWindow", value);
            }
        }
        public DataTable MOU_property_data
        {
            get
            {
                return (DataTable)GetBaseSession("MOU_property_data_PropertyWindow");
            }
            set
            {
                SetBaseSession("MOU_property_data_PropertyWindow", value);
            }
        }

        public List<PropertiesInfo> Property_ExportData
        {
            get
            {
                return (List<PropertiesInfo>)GetBaseSession("Property_ExportData_Property_Info");
            }
            set
            {
                SetBaseSession("Property_ExportData_Property_Info", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> PropertyInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("PropertyInfo_DetailGrid_Data_PropertyInfo");
            }
            set
            {
                SetBaseSession("PropertyInfo_DetailGrid_Data_PropertyInfo", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> PropertyInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("PropertyInfo_AdditionalInfoGrid_PropertyInfo");
            }
            set
            {
                SetBaseSession("PropertyInfo_AdditionalInfoGrid_PropertyInfo", value);
            }
        }
        private enum LB_Category
        {
            PURCHASED,
            PURCHASED_CONSTRUCTED,
            GIFTED,
            GIFTED_CONSTRUCTED,
            RENTED,
            LEASED_Short_Term,
            LEASED_Long_Term,
            LEASED_CONSTRUCTED_Long_Term,
            MORTGAGE_Short_Term,
            MORTGAGE_Long_Term,
            FREE_USE,
            MOU
        }

        #endregion
        #region Grid        
        public ActionResult Frm_Property_Info()
        {
            if (!(CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_LandAndBuilding').hide();</script>");
            }
            Property_user_rights();

            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.TitleX = "Land & Building Information";//(BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR" && BASE._open_Ins_ID != "00009") ? "Land & Building Information(Rented , Free Use Only)" :
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_LandAndBuilding).ToString()) ? 1 : 0;

            ViewData["PropertyLB_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                     || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Grid_Display();
            return View(Property_ExportData);
        }
        public PartialViewResult Frm_Property_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Property_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            if (Property_ExportData == null || command == "REFRESH")
            {
                Grid_Display();
            }
            return PartialView("Frm_Property_Info_Grid", Property_ExportData);
        }
        public void Grid_Display()
        {
            Param_GetProfileListing LBProfile = new Param_GetProfileListing();
            LBProfile.Asset_Profile = AssetProfiles.LAND_BUILDING;
            LBProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            LBProfile.Next_YearID = BASE._next_Unaudited_YearID;
            LBProfile.TableName = Tables.LAND_BUILDING_INFO;
            DataTable LB_Table = BASE._L_B_DBOps.GetProfileListing(LBProfile);
            LB_Table.Columns.Add(new DataColumn { ColumnName = "Remarks", DataType = typeof(bool), AllowDBNull = true });
            //LB_Table.Columns.Add("Remarks", typeof(Boolean?));
            //ArrayList RemovableRows = new ArrayList();
            //for (int i = 0; i < LB_Table.Rows.Count; i++)
            //{
            //    LB_Table.Rows[i]["Remarks"] = Convert.ToInt32(LB_Table.Rows[i]["RemarkCount"]) > 0 ? true : false;
            //    if (BASE._open_User_Type.ToUpper() != Common.ClientUserType.SuperUser.ToUpper() && BASE._open_User_Type.ToUpper() != Common.ClientUserType.Auditor.ToUpper() && BASE._open_Ins_ID != "00009")
            //    {
            //        if (IsAccountingPropertyCategory(LB_Table.Rows[i]["Category"].ToString(),true))
            //        {
            //            RemovableRows.Add(LB_Table.Rows[i]);
            //        }
            //    }
            //}
            //for (int i = 0; i < RemovableRows.Count; i++)
            //{
            //    LB_Table.Rows.Remove((DataRow)RemovableRows[i]);
            //}
            Property_ExportData = DatatableToModel.DataTabletoProperty_INFO(LB_Table);
        }
        #region <--Nested Grid-->
        public ActionResult Frm_Property_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.PropertyInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.PropertyInfo_RecID = RecID;
            ViewBag.PropertyInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    PropertyInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_LandAndBuilding);
                    Session["PropertyInfo_detailGrid_Data"] = PropertyInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_LandAndBuilding);
                    PropertyInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    PropertyInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(PropertyInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(PropertyInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PropertyListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "PropertyListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["PropertyInfo_detailGrid_Data"];
        }
        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Property_report_modal','Not Allowed','No Rights');$('#PropertyModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        #endregion
        public JsonResult DataNavigation(string ActionMethod, string ID, bool MultiUserConfirmation = false, bool isCallForPropertyTypeChange = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var properyRowData = Property_ExportData.Where(f => f.ID == ID).FirstOrDefault();
                string xTemp_ID = ID;
                DateTime Edit_Date = Convert.ToDateTime(properyRowData.Edit_Date);
                string YearID = properyRowData.YearID;
                string xType = properyRowData.Entry_Type;
                string xStatus = properyRowData.Action_Status;
                string Category = properyRowData.Category;
                int xOpenActions = Convert.ToInt32(properyRowData.Open_Actions);                
                Common.Record_Status value = (Common.Record_Status)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod == "LOCKED" || ActionMethod == "UNLOCKED" || ActionMethod == "PRINT-LIST")
                    {
                        DataTable d1 = BASE._L_B_DBOps.GetRecord(xTemp_ID);
                        if (d1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current");
                            jsonParam.title = "Record Changed / Removed in Background!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        if (RecEdit_Date != Edit_Date)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Property");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (ActionMethod == "Edit")
                {
                    if (MultiUserConfirmation == false)
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Entry Cannot Be Edited After Completion Of Insurance Audit";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        bool? IsLBCarriedFwd = BASE._L_B_DBOps.IsPropertyCarriedForward(xTemp_ID, Convert.ToInt32(YearID));
                        if (IsLBCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsLBCarriedFwd == true)
                        {
                            if (BASE._prev_Unaudited_YearID != 0)
                            {
                                jsonParam.message = "Entry Cannot be Edited..! <br><br> This entry has been carried forward from previous year(s). Updation(Partial) can be done only after finalization of previous year accounts..!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new

                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        //if (xType.ToUpper() == "VOUCHER ENTRY" && isCallForPropertyTypeChange == true)
                        //{
                        //    jsonParam.message = "Property Type of Voucher Entry Cannot be Changed from Profile Screen..! <br><br> Please use Cash Book book for editing this.";
                        //    jsonParam.title = "Information...";
                        //    jsonParam.result = false;
                        //    return Json(new

                        //    {
                        //        jsonParam
                        //    }, JsonRequestBehavior.AllowGet);
                        //}

                        DataTable transfers = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_LandAndBuilding, 0, xTemp_ID, false);
                        if (transfers == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (transfers.Rows.Count > 0)
                        {
                            jsonParam.message = "Entry cannot be Edited...!<br><br>This Property has already been transferred to another centre.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    bool AllowUser = false;
                    object MaxValue = 0;
                    MaxValue = BASE._L_B_DBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string multiUserMsg = "";
                    if (value != (Common.Record_Status)MaxValue)
                    {
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            multiUserMsg = "The Record has been locked in the background by another user.";
                        }
                        else if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                        {
                            multiUserMsg = "The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (AllowUser == true && MultiUserConfirmation == false)
                        {
                            jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                            jsonParam.title = "Confirmation...";
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                    {
                        multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                        jsonParam.message = "Locked Entry cannot be Edited/Deleted... !" + multiUserMsg + "<br><br>Note:<br>---------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                    if (SaleRecord != null)
                    {
                        if (SaleRecord.Rows.Count > 0)
                        {
                            jsonParam.message = "Entry Cannot be Edited / Deleted..!<br><br>This item has already been sold...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        Info_LastEditedOn = Edit_Date.ToString("O"),
                        EntryType = xType,
                        Address = properyRowData.Address,
                        defaultPropCategory = Category                      
                    }, JsonRequestBehavior.AllowGet);

                }
                else if (ActionMethod == "Delete")
                {
                    if (MultiUserConfirmation == false)
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Entry Cannot Be Deleted After Completion Of Insurance Audit";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        bool? IsLBCarriedFwd = BASE._L_B_DBOps.IsPropertyCarriedForward(xTemp_ID, Convert.ToInt32(YearID));
                        if (IsLBCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsLBCarriedFwd == true && BASE._GoldSilverDBOps.IsTBImportedCentre() == false)
                        {
                            if (properyRowData.Category.ToUpper() != "FREE USE" && properyRowData.Category.ToUpper() != "RENTED" && properyRowData.Category.ToUpper() != "LEASED (SHORT TERM)" && properyRowData.Category.ToUpper() != "MORTGAGE (SHORT TERM)" && properyRowData.Category.ToUpper() != "MORTGAGE (LONG TERM)")
                            {
                                string NonAccountingMsg = "Only Non-Accounting Categories from previous years can be deleted.";
                                if (BASE._prev_Unaudited_YearID != 0)
                                {
                                    NonAccountingMsg = "";
                                }
                                jsonParam.message = "Entry Cannot be Deleted..! <br><br> This entry has been carried forward from previous year(s). " + NonAccountingMsg;
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new

                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (BASE._prev_Unaudited_YearID != 0)
                                {
                                    jsonParam.message = "Entry Cannot be Deleted..! <br><br> This entry has been carried forward from previous year(s). Deletion can be done only after finalization of previous year accounts..!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new

                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_LandAndBuilding, 0, xTemp_ID, false).Rows.Count > 0)
                        {
                            jsonParam.message = "Entry cannot be Deleted...!<br><br>This Property has already been transferred to another centre.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (xOpenActions > 0)
                        {
                            jsonParam.message = "Entry cannot be Deleted...!<br><br>There are open actions / queries posted against it...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (xType.ToUpper() == "VOUCHER ENTRY")
                        {
                            jsonParam.message = "Entry cannot be Deleted...!<br><br>Please unselect Entries Created from Voucher ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                        if (SaleRecord != null)
                        {
                            if (SaleRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Entry Cannot be Edited / Deleted..!<br><br>This item has already been sold...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._L_B_DBOps.GetTransactionCount(xTemp_ID) > 0)
                        {
                            jsonParam.message = "Entry Cannot be Deleted..!<br><br>Some Expenses has been entered against this Property...! Please delete those entries to delete this Property.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    bool AllowUser = false;
                    object MaxValue = 0;
                    MaxValue = BASE._L_B_DBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string multiUserMsg = "";
                    if (value != (Common.Record_Status)MaxValue)
                    {
                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            multiUserMsg = "The Record has been locked in the background by another user.";
                        }
                        else if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                        {
                            multiUserMsg = "The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (AllowUser == true && MultiUserConfirmation == false)
                        {
                            jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                            jsonParam.title = "Confirmation...";
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                    {
                        multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                        jsonParam.message = "Locked Entry cannot be Edited/Deleted... !" + multiUserMsg + "<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    string UsageMessage = FindLocationUsage(xTemp_ID, false);
                    if (UsageMessage.Length > 0)
                    {
                        jsonParam.message = UsageMessage;
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        Info_LastEditedOn = Edit_Date.ToString("O"),
                        EntryType = xType,
                        Address = properyRowData.Address,
                        defaultPropCategory = Category
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "View")
                {
                    object MaxValue = 0;
                    MaxValue = BASE._L_B_DBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In the Background";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        Info_LastEditedOn = Edit_Date.ToString("O"),
                        EntryType = xType,
                        Address = properyRowData.Address,
                        defaultPropCategory = properyRowData.Category,
                        Txt_LB_Amount = properyRowData.Opening_Value
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "LOCKED")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Lock_Unlock))
                    {
                        if (MultiUserConfirmation == false)
                        {
                            if (xType.ToUpper() == "VOUCHER ENTRY")
                            {
                                jsonParam.message = "Entries created from vouchers can be audited from vouchers only...!<br><br>Please unselect Entries Created from Voucher ...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        bool AllowUser = false;
                        object MaxValue = 0;
                        MaxValue = BASE._L_B_DBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if (value != (Common.Record_Status)MaxValue)
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Completed)
                            {
                                AllowUser = true;
                            }
                            if (AllowUser && MultiUserConfirmation == false)
                            {
                                jsonParam.message = "Record has been Unlocked in the background by another user<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            Msg = "Information...";
                        }
                        if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Already Locked Entries can't Be Re-Locked...!<br><br>Please unselect Already Locked Entries ...!";
                            jsonParam.title = Msg;
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Incomplete)
                        {
                            jsonParam.message = "Incomplete Entries Can't Be Locked...!<br><br>Please unselect Incomplete Entries or Ask Center To Complete it...!";
                            jsonParam.title = Msg;
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        Object xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Tables.LAND_BUILDING_INFO, xTemp_ID);
                        if (xRemarks != null && !Convert.IsDBNull(xRemarks))
                        {
                            if ((int)MaxValue > 0)
                            {
                                jsonParam.message = "Entries With Pending Queries Can't Be Locked...!<br><br>Please unselect such entries...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (BASE._L_B_DBOps.MarkAsLocked(xTemp_ID))
                        {
                            jsonParam.message = Messages.LockedSuccess(1);
                            jsonParam.title = "Locked...";
                            jsonParam.result = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "UNLOCKED")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Lock_Unlock))
                    {
                        if (MultiUserConfirmation == false)
                        {
                            if (xType.ToUpper() == "VOUCHER ENTRY")
                            {
                                jsonParam.message = "Entries created from vouchers can be audited from vouchers only...!<br><br>Please unselect Entries Created from Voucher ...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        bool AllowUser = false;
                        object MaxValue = 0;
                        MaxValue = BASE._L_B_DBOps.GetStatus(xTemp_ID);
                        string Msg = "";
                        if (value != (Common.Record_Status)MaxValue)
                        {
                            Msg = "Record Status has been changed in the background by another user";
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                            {
                                AllowUser = true;
                            }
                            if (AllowUser && MultiUserConfirmation == false)
                            {
                                jsonParam.message = "Record has been Locked in the background by another user<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            Msg = "Information...";
                        }
                        if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed)
                        {
                            jsonParam.message = "Already Unlocked Entries can't Be Re-Unlocked...!<br><br>Please unselect Already Unlocked Entries ...!";
                            jsonParam.title = Msg;
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Incomplete)
                        {
                            jsonParam.message = "Incomplete Entries Can't Be Unlocked...!<br><br>Please unselect Incomplete Entries or Ask Center To Complete it...!";
                            jsonParam.title = Msg;
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._L_B_DBOps.MarkAsComplete(xTemp_ID))
                        {
                            jsonParam.message = Messages.UnlockedSuccess(1);
                            jsonParam.title = "Locked...";
                            jsonParam.result = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "ADD REMARKS")
                {
                    if (BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Manage_Remarks))
                    {
                        if (xType.ToUpper() == "VOUCHER ENTRY")
                        {
                            jsonParam.message = "Entries created from vouchers can be audited from vouchers only...!<br><br>Please unselect Entries Created from Voucher ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (xStatus.ToUpper() != "LOCKED")
                        {
                            jsonParam.result = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            jsonParam.message = "Queries Can't Be Added to freezed Records...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_Property_Window_Profile(string Info_LastEditedOn = null, string Tag = null, string xID = null, string Address = null, string YearID = "", string EntryType = "", string defaultPropCategory = "", double? Txt_LB_Amount = null, bool isCallForPropertyTypeChange = false)
        {
            Property_user_rights();
            var j = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (j = 0; j < Rights.Length; j++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, Rights[j]) && Tag == AM[j])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }
            PropertyWindowProfile model = new PropertyWindowProfile();
            this.defaultPropCategory = defaultPropCategory;
            this.YearID = YearID;
            this.EntryType = EntryType;
            model.xID = xID;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Tag);
            model.TempActionMethod = model.Tag.ToString();
            model.TitleX = "Land & Building";
            model.TextEdit1_LBW = BASE._open_Ins_Name;
            model.SubTitleX = "As On " + BASE._open_Year_Sdt.AddDays(-1).ToString("dd MMMM, yyyy");
            model.Txt_LB_Amount_LBW = Txt_LB_Amount;
            model.isCallForPropertyTypeChange = isCallForPropertyTypeChange;
            List<string> YearBind = new List<string>();
            for (int i = BASE._open_Year_Edt.Year; i >= 1900; i += -1)
            {
                YearBind.Add(i.ToString());
            }
            ViewBag.Cmd_Con_Year_Bind = YearBind;
            RefreshOwnerList();
            ViewBag.PPDocumentList = Get_Documents_List();
            RefreshStateList();
            if (model.Tag == Common.Navigation_Mode._New)
            {
                if (BASE.IsInsuranceAudited())
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Entry Cannot Be created After The Completion Of Insurance Audit','Information...');</script>");
                }
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d1 = BASE._L_B_DBOps.GetRecord(model.xID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.Tag == Common.Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn),Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                        {
                            string message = Messages.RecordChanged("Current Property", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','PropertyListGrid');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.YearID = d1.Rows[0]["LB_COD_YEAR_ID"].ToString();
                this.YearID = model.YearID;
                DataTable d1_Ext = BASE._L_B_DBOps.GetExtendedRecord(model.xID);
                if (d1_Ext == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                DataTable d1_Doc = BASE._L_B_DBOps.GetDocumentRecord(model.xID);
                if (d1_Doc == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                model.Cmd_PType_LBW = d1.Rows[0]["LB_PRO_TYPE"].ToString();              
                model.Cmd_PCategory_LBW = d1.Rows[0]["LB_PRO_CATEGORY"].ToString();
                model.Cmd_PUse_LBW = d1.Rows[0]["LB_PRO_USE"].ToString();
                if (model.Tag != Common.Navigation_Mode._View)
                {
                    model.Txt_LB_Amount_LBW = Convert.ToDouble(d1.Rows[0]["LB_VALUE"]);
                }
                model.Txt_PName_LBW = d1.Rows[0]["LB_PRO_NAME"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PRO_ADDRESS"]))
                {
                    model.Txt_Address_LBW = d1.Rows[0]["LB_PRO_ADDRESS"].ToString();
                    model.Txt_Add_LBW = d1.Rows[0]["LB_PRO_ADDRESS"].ToString();
                }
                if (!string.IsNullOrEmpty(Address))
                {                    
                    model.Txt_Add_LBW = Address;
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_ADDRESS1"]))
                {
                    if (d1.Rows[0]["LB_ADDRESS1"].ToString().Length > 0)
                    {
                        model.Txt_R_Add1_LBW = d1.Rows[0]["LB_ADDRESS1"].ToString();
                        model.Txt_R_Add2_LBW = d1.Rows[0]["LB_ADDRESS2"].ToString();
                        model.Txt_R_Add3_LBW = d1.Rows[0]["LB_ADDRESS3"].ToString();
                        model.Txt_R_Add4_LBW = d1.Rows[0]["LB_ADDRESS4"].ToString();
                        string StateCode = null;
                        if (!Convert.IsDBNull(d1.Rows[0]["LB_STATE_ID"]))
                        {
                            if (d1.Rows[0]["LB_STATE_ID"].ToString().Length > 0)
                            {
                                model.GLookUp_StateList_LBW = d1.Rows[0]["LB_STATE_ID"].ToString();
                                if (StateList_DD_LBW.Count > 0)
                                {
                                    StateCode = StateList_DD_LBW.Where(x => x.R_ST_REC_ID == model.GLookUp_StateList_LBW).First().R_ST_CODE;
                                }
                            }
                        }
                        if (!Convert.IsDBNull(d1.Rows[0]["LB_DISTRICT_ID"]))
                        {
                            if (d1.Rows[0]["LB_DISTRICT_ID"].ToString().Length > 0)
                            {
                                model.GLookUp_DistrictList_LBW = d1.Rows[0]["LB_DISTRICT_ID"].ToString();
                                RefreshDistrictList(StateCode);
                            }
                        }
                        if (!Convert.IsDBNull(d1.Rows[0]["LB_CITY_ID"]))
                        {
                            if (d1.Rows[0]["LB_CITY_ID"].ToString().Length > 0)
                            {
                                model.GLookUp_CityList_LBW = d1.Rows[0]["LB_CITY_ID"].ToString();
                                RefreshCityList(StateCode);
                            }
                        }
                        if (!Convert.IsDBNull(d1.Rows[0]["LB_PINCODE"]))
                        {
                            model.Txt_R_Pincode_LBW = d1.Rows[0]["LB_PINCODE"].ToString();
                        }
                    }
                }
                model.Cmd_Ownership_LBW = d1.Rows[0]["LB_OWNERSHIP"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["LB_OWNERSHIP_PARTY_ID"]))
                {
                    model.Look_OwnList_LBW = d1.Rows[0]["LB_OWNERSHIP_PARTY_ID"].ToString();
                }
                model.Txt_SNo_LBW = d1.Rows[0]["LB_SURVEY_NO"].ToString();
                model.Txt_Tot_Area_LBW = Convert.ToDouble(d1.Rows[0]["LB_TOT_P_AREA"]);
                model.Txt_Con_Area_LBW = Convert.ToDouble(d1.Rows[0]["LB_CON_AREA"]);
                model.Cmd_Con_Year_LBW = d1.Rows[0]["LB_CON_YEAR"].ToString();
                model.Cmd_RccType_LBW = d1.Rows[0]["LB_RCC_ROOF"].ToString();
                model.Txt_Dep_Amt_LBW = Convert.ToDouble(d1.Rows[0]["LB_DEPOSIT_AMT"]);
                model.Txt_Mon_Rent_LBW = Convert.ToDouble(d1.Rows[0]["LB_MONTH_RENT"]);
                model.Txt_Other_Payments_LBW = Convert.ToDouble(d1.Rows[0]["LB_MONTH_O_PAYMENTS"]);
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PAID_DATE"]))
                {
                    model.Txt_PaidDate_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PAID_DATE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PERIOD_FROM"]))
                {
                    model.Txt_F_Date_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PERIOD_FROM"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PERIOD_TO"]))
                {
                    model.Txt_T_Date_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PERIOD_TO"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_TR_ID"]))
                {
                    model.LB_TR_ID = d1.Rows[0]["LB_TR_ID"].ToString();
                }
                if (d1.Rows[0]["LB_DOC_OTHERS"].ToString().ToUpper().Trim() == "YES")
                {
                    model.Chk_OtherDoc_LBW = true;
                }
                else
                {
                    model.Chk_OtherDoc_LBW = false;
                }
                model.Txt_OtherDoc_LBW = d1.Rows[0]["LB_DOC_NAME"].ToString();
                List<Documents> Document = new List<Documents>();
                for (int i = 0; i < d1_Doc.Rows.Count; i++)
                {
                    Document.Add(new Documents { ID = d1_Doc.Rows[i]["LB_MISC_ID"].ToString(), Selected = true });
                }
                model.Document = Document;
                model.Txt_Remarks_LBW = d1.Rows[0]["LB_OTHER_DETAIL"].ToString();
            }
            model.Txt_PName_LBW = model.Txt_PName_LBW.HandleEscapeCharacters();
            model.Txt_Address_LBW = (model.Txt_Address_LBW??"").Replace(Environment.NewLine, "").HandleEscapeCharacters();
            model.Txt_Add_LBW = (model.Txt_Add_LBW??"").Replace(Environment.NewLine, "").HandleEscapeCharacters();      
            model.Txt_R_Add1_LBW = model.Txt_R_Add1_LBW.HandleEscapeCharacters();
            model.Txt_R_Add2_LBW = model.Txt_R_Add2_LBW.HandleEscapeCharacters();
            model.Txt_R_Add3_LBW = model.Txt_R_Add3_LBW.HandleEscapeCharacters();
            model.Txt_R_Add4_LBW = model.Txt_R_Add4_LBW.HandleEscapeCharacters();
            model.Txt_SNo_LBW = model.Txt_SNo_LBW.HandleEscapeCharacters();
            model.Txt_OtherDoc_LBW = model.Txt_OtherDoc_LBW.HandleEscapeCharacters();
            if (model.Tag == Common.Navigation_Mode._Edit)
            {
                ViewBag.TransactionCount = BASE._L_B_DBOps.GetTransactionCount(model.xID);
                ViewBag.InsuranceAuditor = BASE.IsInsuranceAuditor(BASE._open_User_ID);
            }
            ViewBag.userType = BASE._open_User_Type;         
            return View(model);
        }
        public ActionResult Frm_Property_Window_BUT_SAVE_Click(PropertyWindowProfile model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod.ToString());
                if (model.MOU_Property_Select == false)
                {
                    if (model.Cmd_PCategory_LBW != null && model.Cmd_PCategory_LBW.ToUpper() == "MOU")
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "MOU Property Select";
                        jsonParam.popup_form_name = "Frm_MOU_Property_Select";
                        jsonParam.popup_form_path = "/Profile/Property/Frm_MOU_Property_Select/";
                        jsonParam.popup_querystring = "";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        DataTable property_DbOps = BASE._L_B_DBOps.GetRecord(model.xID);
                        if (property_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (property_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Property");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.LastEditedOn), Convert.ToDateTime(property_DbOps.Rows[0]["REC_EDIT_ON"])) == false)
                           // if (model.LastEditedOn != Convert.ToDateTime(property_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Property");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        model.propertyType_PreviousValue = property_DbOps.Rows[0]["LB_PRO_TYPE"].ToString();
                        model.LB_ORG_REC_ID = property_DbOps.Rows[0]["LB_ORG_REC_ID"].ToString();

                        DataTable TR_TABLE = null;
                        if (BASE._prev_Unaudited_YearID != 0)
                        {
                            TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + model.xID + "'", BASE._prev_Unaudited_YearID);
                        }
                        else
                        {
                            TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                        }
                        var sale_qty = 0;
                        if (TR_TABLE.Rows.Count > 0)
                        {
                            sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                        }
                        if (BASE._prev_Unaudited_YearID != 0)
                        {
                            TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                            if (TR_TABLE.Rows.Count > 0)
                            {
                                sale_qty += Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                            }
                        }
                        if (sale_qty != 0)
                        {
                            jsonParam.message = "Entry Cannot Be Edited/Deleted..!<br><br>This item has already been sold...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._L_B_DBOps.GetStatus(model.xID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Property");
                            jsonParam.title = "Record Status Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_LandAndBuilding, 0, model.xID, false).Rows.Count > 0)
                        {
                            jsonParam.message = "Entry Cannot Be Edited/Deleted..!<br><br>This property has already been transferred to another centre.";
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                        {
                            var openActions = BASE._Action_Items_DBOps.GetOpenActions(model.xID, "LAND_BUILDING_INFO");
                            if (openActions == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if ((int)openActions > 0)
                            {
                                jsonParam.message = "Entry Cannot Be Deleted..!<br><br>There are open actions/queries posted against it...";
                                jsonParam.title = "Information...";
                                jsonParam.refreshgrid = true;
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            MaxValue = 0;
                            string Message = "";
                            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Gift, model.xID);
                            if (Locations == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            for (int i = 0; i < Locations.Rows.Count; i++)
                            {
                                string LocationID = Locations.Rows[i][0].ToString();
                                string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(LocationID, false);
                                bool DeleteAllow = true;
                                if (UsedPage.Length > 0)
                                {
                                    DeleteAllow = false;
                                }
                                if (!DeleteAllow)
                                {
                                    Message = "Property Being Deleted in this voucher is being used in another page as Location...!<b><br>Name: " + UsedPage;
                                    jsonParam.message = Message;
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if ((int)BASE._L_B_DBOps.GetTransactionCount(model.xID) > 0)
                            {
                                jsonParam.message = "Entry Cannot Be Deleted..!<br><br>Some Expenses has been entered against this property...! Please delete those entries to delete this Property.";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.Tag == Common.Navigation_Mode._Edit)
                        {
                            if (model.Txt_LB_Amount_ReadOnly == false)
                            {
                                if ((int)BASE._L_B_DBOps.GetTransactionCount(model.xID) > 0)
                                {
                                    jsonParam.message = "Entry Cannot Be Edited in this attempt..!<br><br>Some Expenses has been entered against this property in the background and all property fields are enabled...! Please Try again.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrEmpty(model.Cmd_PCategory_LBW))
                    {
                        jsonParam.message = "Property Category Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PCategory_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Cmd_PType_LBW))
                    {
                        jsonParam.message = "Property Type Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PType_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Txt_PName_LBW))
                    {
                        jsonParam.message = "Property/Building Name cannot be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrWhiteSpace(model.Txt_Add_LBW))
                    {
                        jsonParam.message = "Property/Building Address Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Add_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Cmd_PUse_LBW))
                    {
                        jsonParam.message = "Use of Property Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_PUse_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_LBW))
                    {
                        jsonParam.message = "Property Address Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_R_Add1_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_StateList_LBW))
                    {
                        jsonParam.message = "State Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_StateList_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_DistrictList_LBW))
                    {
                        jsonParam.message = "District Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_DistrictList_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_CityList_LBW))
                    {
                        jsonParam.message = "City Cannot Be Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_CityList_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Txt_R_Pincode_LBW))
                    {
                        jsonParam.message = "Pincode Cannot Be  Blank...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_R_Pincode_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.Cmd_Ownership_LBW))
                    {
                        jsonParam.message = "Ownership Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Ownership_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //free
                    if (model.Cmd_Ownership_LBW.ToUpper() == "THIRD PARTY")
                    {
                        if (string.IsNullOrEmpty(model.Look_OwnList_LBW))
                        {
                            jsonParam.message = "Owner Name Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Look_OwnList_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (IsAccountingPropertyCategory(model.Cmd_PCategory_LBW) == true || model.Cmd_PType_LBW.Contains("LAND"))
                    {
                        if (string.IsNullOrWhiteSpace(model.Txt_SNo_LBW))
                        {
                            jsonParam.message = "Survey No cannot be Blank...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_SNo_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!IsAccountingPropertyCategory(model.Cmd_PCategory_LBW))
                    {
                        if (model.Txt_LB_Amount_LBW != 0 && model.Txt_LB_Amount_LBW != null)
                        {
                            jsonParam.message = "Invalid Opening Value...!<br><br>" + model.Cmd_PCategory_LBW + " Property Cannot have Opening Values..";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_LB_Amount_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((model.Txt_Tot_Area_LBW <= 0 || model.Txt_Tot_Area_LBW == null) && model.Cmd_PType_LBW.Contains("LAND"))
                    {
                        jsonParam.message = "Total Plot Area cannot be Zero or Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Tot_Area_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Tot_Area_LBW < 0 && model.Txt_Tot_Area_LBW != null)
                    {
                        jsonParam.message = "Total Plot Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Tot_Area_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Con_Area_LBW < 0)
                    {
                        jsonParam.message = "Construction Area cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Con_Area_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Cmd_PType_LBW.ToUpper() != "LAND" && IsAccountingPropertyCategory(model.Cmd_PCategory_LBW) == true)
                    {
                        if (model.Txt_Con_Area_LBW <= 0 || model.Txt_Con_Area_LBW == null)
                        {
                            jsonParam.message = "Total Constructed Area cannot be Zero or Negative...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Con_Area_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.Cmd_Con_Year_LBW))
                        {
                            jsonParam.message = "Construction Year not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Con_Year_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(model.Cmd_RccType_LBW))
                        {
                            jsonParam.message = "RCC Roof Construction Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_RccType_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    if (model.Txt_Dep_Amt_LBW < 0)
                    {
                        jsonParam.message = "Security Deposit Amount cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Dep_Amt_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Mon_Rent_LBW < 0)
                    {
                        jsonParam.message = "Monthly Rent Amount cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Mon_Rent_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Other_Payments_LBW < 0)
                    {
                        jsonParam.message = "Other Monthly Payment cannot be Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Other_Payments_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrEmpty(model.Cmd_Con_Year_LBW))
                    {
                        if (Convert.ToInt32(model.Cmd_Con_Year_LBW) > BASE._open_Year_Sdt.Year && EntryType.ToUpper() != "VOUCHER ENTRY")
                        {
                            jsonParam.message = "Construction Year must be Less than/Equal to Start Financial Year...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_Con_Year_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((model.Cmd_PCategory_LBW.ToUpper() == LB_Category.PURCHASED.ToString() || model.Cmd_PCategory_LBW.ToUpper() == LB_Category.PURCHASED_CONSTRUCTED.ToString() || model.Cmd_PCategory_LBW.ToUpper() == LB_Category.GIFTED.ToString() || model.Cmd_PCategory_LBW.ToUpper() == LB_Category.GIFTED_CONSTRUCTED.ToString()) && !model.Cmd_PType_LBW.ToUpper().Contains("LAND"))
                    {
                        if (string.IsNullOrEmpty(model.Cmd_RccType_LBW))
                        {
                            jsonParam.message = "RCC Constructed Roof Not Selected...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_RccType_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (IsDate(model.Txt_F_Date_LBW.ToString()) == true && IsDate(model.Txt_T_Date_LBW.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_F_Date_LBW) >= Convert.ToDateTime(model.Txt_T_Date_LBW))
                        {
                            jsonParam.message = "To Date must be Higher than From Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_T_Date_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Cmd_PCategory_LBW.ToUpper() == "LEASED (LONG TERM)" || model.Cmd_PCategory_LBW.ToUpper() == "LEASED AND CONSTRUCTED (LONG TERM)")
                    {
                        if (IsDate(model.Txt_F_Date_LBW.ToString()) == true & IsDate(model.Txt_T_Date_LBW.ToString()) == true)
                        {
                            double diff = (Convert.ToDateTime(model.Txt_T_Date_LBW) - Convert.ToDateTime(model.Txt_F_Date_LBW)).TotalDays;
                            if (diff < 3650)
                            {
                                jsonParam.message = "Leased(Long Term) Period Cannot be Less than 10 Years...!";
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_T_Date_LBW";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (model.Tag == Common.Navigation_Mode._Edit)
                    {
                        object TxnCount = BASE._L_B_DBOps.GetTransactionCount(model.xID);
                        if (TxnCount == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)TxnCount > 0 && (model.Cmd_PCategory_LBW.ToUpper() != "PURCHASED" && model.Cmd_PCategory_LBW.ToUpper() != "PURCHASED AND CONSTRUCTED" && model.Cmd_PCategory_LBW.ToUpper() != "LEASED (LONG TERM)" && model.Cmd_PCategory_LBW.ToUpper() != "LEASED AND CONSTRUCTED (LONG TERM)" && model.Cmd_PCategory_LBW.ToUpper() != "GIFTED" && model.Cmd_PCategory_LBW.ToUpper() != "GIFTED AND CONSTRUCTED"))
                        {
                            jsonParam.message = "Invalid Category Selected..!<br><br>Some Expenses has been entered against this property...Only Capitalizable Categories can be selected!<br>Please delete those entries to select this Property Category.";
                            jsonParam.title = "Incorrect Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_PCategory_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DataTable MainCenters = null;
                    if (model.Cmd_PUse_LBW == "MAIN CENTRE")
                    {
                        if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
                        {
                            MainCenters = BASE._L_B_DBOps.GetMainCenters();
                        }
                        if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                        {
                            MainCenters = BASE._L_B_DBOps.GetMainCenters(model.xID);
                        }
                        if (MainCenters == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (MainCenters.Rows.Count > 0)
                        {
                            jsonParam.message = "Main Centre (" + MainCenters.Rows[0]["LB_PRO_NAME"].ToString() + ")  already Created in  " + MainCenters.Rows[0]["CEN_UID"].ToString() + " in year " + MainCenters.Rows[0]["YEAR_ID"].ToString() + "...!";
                            jsonParam.title = "Duplicate Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cmd_PUse_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DataTable LB_Name = null;
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
                    {
                        LB_Name = (DataTable)BASE._L_B_DBOps.GetPropertyrByName(model.Txt_PName_LBW);
                    }
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        LB_Name = (DataTable)BASE._L_B_DBOps.GetPropertyrByName(model.Txt_PName_LBW, model.xID);
                    }
                    if (LB_Name == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (LB_Name.Rows.Count > 0)
                    {
                        jsonParam.message = "Property with same name already Created in " + LB_Name.Rows[0]["CEN_UID"].ToString() + " in year " + LB_Name.Rows[0]["YEAR_ID"].ToString() + "...!";
                        jsonParam.title = "Duplicate Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    object MaxValue_Loc = 0;
                    MaxValue_Loc = BASE._AssetLocDBOps.GetRecordCountByName(model.Txt_PName_LBW, ClientScreen.Profile_LandAndBuilding, BASE._open_PAD_No_Main);
                    if (MaxValue_Loc == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)MaxValue_Loc != 0 && model.Tag == Common.Navigation_Mode._New)
                    {
                        jsonParam.message = "Location With Same Name Already Available...!";
                        jsonParam.title = "Duplicate...(" + model.Txt_PName_LBW + ")";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_PName_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Tag == Common.Navigation_Mode._New | model.Tag == Common.Navigation_Mode._Edit)
                    {
                        DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                        if (LocNames != null)
                        {
                            if (LocNames.Rows.Count > 0)
                            {
                                if (model.Txt_PName_LBW.Length > 0)
                                {
                                    for (int I = 0; I <= LocNames.Rows.Count - 1; I++)
                                    {
                                        if (model.Txt_PName_LBW.ToUpper() == LocNames.Rows[I][0].ToString().ToUpper())
                                        {
                                            jsonParam.message = "Location With Same Name Already Exists In Pending Transfers...!";
                                            jsonParam.title = "Duplicate...(" + model.Txt_PName_LBW + ")";
                                            jsonParam.result = false;
                                            jsonParam.focusid = "Txt_PName_LBW";
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Look_OwnList_LBW))
                        {
                            DateTime oldEditOn = (DateTime)model.Owner_Rec_Edit_On;
                            var d1 = BASE._L_B_DBOps.GetOwners(model.Look_OwnList_LBW);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (d1.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (oldEditOn != d1[0].REC_EDIT_ON)
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                model.Cmd_RccType_LBW = model.Cmd_RccType_LBW ?? "NO";
                //SET ITEM ID  
                string X_ITEM_ID = string.Empty;
                if (model.Cmd_PType_LBW.Trim().ToUpper() == "LAND")
                {
                    X_ITEM_ID = "f8f4b6c3-f340-4e99-892b-16ebf33c8c28";
                }
                if (model.Cmd_PType_LBW.Trim().ToUpper() == "BUILDING")
                {
                    X_ITEM_ID = "c4d9b556-6a36-41f5-8f56-245d2c6e0be4";
                }
                if ((model.Cmd_PType_LBW.Trim().ToUpper() == "FLAT"))
                {
                    X_ITEM_ID = "e769d245-0299-4737-9b74-55cc2147f389";
                }
                if ((model.Cmd_PType_LBW.Trim().ToUpper() == "FLAT (IN MULTIPLE FLOOR)"))
                {
                    X_ITEM_ID = "f90dfc01-7c18-447e-8331-3eae64288087";
                }
                if (BASE.IsInsuranceAudited())
                {
                    jsonParam.message = "Entry Cannot be created/edited after the completion of insurance audit";
                    jsonParam.title = "Centre marked as Insurance audited in the background...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                string Status_Action = model.Tag == Common_Lib.Common.Navigation_Mode._Delete ? ((int)Common_Lib.Common.Record_Status._Deleted).ToString() : ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                //Insert a record
                Param_Txn_InsertProperty_LandAndBuilding InNewParam = new Param_Txn_InsertProperty_LandAndBuilding();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
                {
                    model.xID = Guid.NewGuid().ToString();
                    string _Owner_Party = null;
                    if (model.Cmd_Ownership_LBW.ToUpper() == "THIRD PARTY")
                    {
                        _Owner_Party = "'" + model.Look_OwnList_LBW + "'";
                    }
                    Parameter_Insert_LandAndBuilding InParam = new Parameter_Insert_LandAndBuilding();
                    InParam.ItemID = X_ITEM_ID;
                    InParam.PropertyType = model.Cmd_PType_LBW;
                    InParam.Category = model.Cmd_PCategory_LBW;
                    InParam.Use = model.Cmd_PUse_LBW;
                    InParam.Name = model.Txt_PName_LBW;
                    InParam.Address = model.Txt_Address_LBW;
                    InParam.Ownership = model.Cmd_Ownership_LBW;
                    InParam.Owner_Party_ID = _Owner_Party;
                    InParam.SurveyNo = model.Txt_SNo_LBW ?? "";
                    InParam.TotalArea = model.Txt_Tot_Area_LBW ?? 0.00;
                    InParam.ConstructedArea = model.Txt_Con_Area_LBW ?? 0.00;
                    InParam.ConstructionYear = model.Cmd_Con_Year_LBW ?? "";
                    InParam.RCCRoof = model.Cmd_RccType_LBW;
                    InParam.DepositAmount = model.Txt_Dep_Amt_LBW ?? 0.00;
                    if (IsDate(model.Txt_PaidDate_LBW.ToString()))
                    {
                        InParam.PaymentDate = Convert.ToDateTime(model.Txt_PaidDate_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.MonthlyRent = model.Txt_Mon_Rent_LBW ?? 0.00;
                    InParam.MonthlyOtherExpenses = model.Txt_Other_Payments_LBW ?? 0.00;
                    if (IsDate(model.Txt_F_Date_LBW.ToString()))
                    {
                        InParam.PeriodFrom = Convert.ToDateTime(model.Txt_F_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (IsDate(model.Txt_T_Date_LBW.ToString()))
                    {
                        InParam.PeriodTo = Convert.ToDateTime(model.Txt_T_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.OtherDocs = model.Chk_OtherDoc_LBW ? "YES" : "NO";
                    InParam.DocNames = model.Txt_OtherDoc_LBW ?? "";
                    InParam.Value = model.Txt_LB_Amount_LBW ?? 0.00;
                    InParam.OtherDetails = model.Txt_Remarks_LBW ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = model.xID;
                    InParam.LB_Add1 = model.Txt_R_Add1_LBW ?? "";
                    InParam.LB_Add2 = model.Txt_R_Add2_LBW ?? "";
                    InParam.LB_Add3 = model.Txt_R_Add3_LBW ?? "";
                    InParam.LB_Add4 = model.Txt_R_Add4_LBW ?? "";
                    InParam.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e";
                    if (!string.IsNullOrEmpty(model.GLookUp_CityList_LBW))
                    {
                        InParam.LB_CityID = model.GLookUp_CityList_LBW;
                    }
                    if (!string.IsNullOrEmpty(model.GLookUp_StateList_LBW))
                    {
                        InParam.LB_StateID = model.GLookUp_StateList_LBW;
                    }
                    if (!string.IsNullOrEmpty(model.GLookUp_DistrictList_LBW))
                    {
                        InParam.LB_DisttID = model.GLookUp_DistrictList_LBW;
                    }
                    InParam.LB_PinCode = model.Txt_R_Pincode_LBW ?? "";
                    InNewParam.param_InsertLandAndBuilding = InParam;
                    Parameter_InsertExtendedInfo_LandAndBuilding[] InExtInfoParam = new Parameter_InsertExtendedInfo_LandAndBuilding[Ext_property_Data.Count];
                    Ext_property_Data = Ext_property_Data.OrderBy(o => o.Sr).ToList();
                    if (Ext_property_Data.Count > 0 && Ext_property_Data[0].Sr > 0)
                    {
                        for (int i = 0; i < Ext_property_Data.Count; i++)
                        {
                            Parameter_InsertExtendedInfo_LandAndBuilding InEInfo = new Parameter_InsertExtendedInfo_LandAndBuilding();
                            InEInfo.LB_Rec_ID = model.xID;
                            InEInfo.SrNo = Ext_property_Data[i].Sr.ToString();
                            InEInfo.Inst_ID = Ext_property_Data[i].Ins_ID;
                            InEInfo.TotalArea = Ext_property_Data[i].Total_Plot_Area ?? 0.00;
                            InEInfo.ConstructedArea = Ext_property_Data[i].Constructed_Area ?? 0.00;
                            InEInfo.ConYear = Ext_property_Data[i].Construction_Year ?? "";
                            InEInfo.MOU_Date = Convert.ToDateTime(Ext_property_Data[i].M_O_U_Date).ToString(BASE._Server_Date_Format_Short);
                            InEInfo.Value = Ext_property_Data[i].Value ?? 0.00;
                            InEInfo.OtherDetails = Ext_property_Data[i].Other_Detail ?? "";
                            InEInfo.Status_Action = Status_Action;
                            InEInfo.RecID = System.Guid.NewGuid().ToString();
                            InExtInfoParam[i] = InEInfo;
                        }
                    }
                    InNewParam.InExtendedInfo = InExtInfoParam;
                    Parameter_InsertDocInfo_LandAndBuilding[] InDocInfoParam = new Parameter_InsertDocInfo_LandAndBuilding[model.Document.Where(x => x.Selected == true).Count()];
                    int ctr = 0;
                    foreach (Documents currSelection in model.Document.Where(x => x.Selected == true))
                    {
                        Parameter_InsertDocInfo_LandAndBuilding InDocInfo = new Parameter_InsertDocInfo_LandAndBuilding();
                        InDocInfo.LB_Rec_ID = model.xID;
                        InDocInfo.Doc_Misc_ID = currSelection.ID;
                        InDocInfo.Status_Action = Status_Action;
                        InDocInfo.RecID = System.Guid.NewGuid().ToString();
                        InDocInfoParam[ctr] = InDocInfo;
                        ctr += 1;
                    }
                    InNewParam.InDocInfo = InDocInfoParam;
                    var InAssetLoc = new Common_Lib.RealTimeService.Param_AssetLoc_Insert
                    {
                        name = model.Txt_PName_LBW.Trim(),
                        OtherDetails = "Property Type: " + model.Cmd_PType_LBW,
                        Status_Action = Status_Action,
                        Match_LB_ID = model.xID,
                        Match_SP_ID = ""
                    };
                    InNewParam.param_InsertAssetLoc = InAssetLoc;
                    if (!BASE._L_B_DBOps.InsertProperty_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                //update a record
                Param_Txn_UpdateProperty_LandAndBuilding EditParam = new Common_Lib.RealTimeService.Param_Txn_UpdateProperty_LandAndBuilding();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    string _Owner_Party = null;
                    if (model.Cmd_Ownership_LBW.ToUpper() == "THIRD PARTY")
                    {
                        _Owner_Party = "'" + model.Look_OwnList_LBW + "'";
                    }
                    Parameter_Update_LandAndBuilding UpParam = new Common_Lib.RealTimeService.Parameter_Update_LandAndBuilding();
                    UpParam.ItemID = X_ITEM_ID;
                    UpParam.PropertyType = model.Cmd_PType_LBW;
                    UpParam.Category = model.Cmd_PCategory_LBW;
                    UpParam.Use = model.Cmd_PUse_LBW;
                    UpParam.Name = model.Txt_PName_LBW;
                    UpParam.Address = model.Txt_Address_LBW;
                    UpParam.Ownership = model.Cmd_Ownership_LBW;
                    UpParam.Owner_Party_ID = _Owner_Party;
                    UpParam.SurveyNo = model.Txt_SNo_LBW ?? "";
                    UpParam.TotalArea = model.Txt_Tot_Area_LBW ?? 0.00;
                    UpParam.ConstructedArea = model.Txt_Con_Area_LBW ?? 0.00;
                    UpParam.ConstructionYear = model.Cmd_Con_Year_LBW ?? "";
                    UpParam.RCCRoof = model.Cmd_RccType_LBW ?? "";
                    UpParam.DepositAmount = model.Txt_Dep_Amt_LBW ?? 0.00;
                    if (IsDate(model.Txt_PaidDate_LBW.ToString()))
                    {
                        UpParam.PaymentDate = Convert.ToDateTime(model.Txt_PaidDate_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.MonthlyRent = model.Txt_Mon_Rent_LBW ?? 0.00;
                    UpParam.MonthlyOtherExpenses = model.Txt_Other_Payments_LBW ?? 0.00;
                    if (IsDate(model.Txt_F_Date_LBW.ToString()))
                    {
                        UpParam.PeriodFrom = Convert.ToDateTime(model.Txt_F_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (IsDate(model.Txt_T_Date_LBW.ToString()))
                    {
                        UpParam.PeriodTo = Convert.ToDateTime(model.Txt_T_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.OtherDocs = model.Chk_OtherDoc_LBW == true ? "YES" : "NO";
                    UpParam.DocNames = model.Txt_OtherDoc_LBW ?? "";
                    UpParam.Value = model.Txt_LB_Amount_LBW ?? 0.00;
                    UpParam.OtherDetails = model.Txt_Remarks_LBW ?? "";
                    // UpParam.Status_Action = Status_Action
                    UpParam.RecID = model.xID;
                    UpParam.LB_Add1 = model.Txt_R_Add1_LBW ?? "";
                    UpParam.LB_Add2 = model.Txt_R_Add2_LBW ?? "";
                    UpParam.LB_Add3 = model.Txt_R_Add3_LBW ?? "";
                    UpParam.LB_Add4 = model.Txt_R_Add4_LBW ?? "";
                    if (!string.IsNullOrEmpty(model.GLookUp_CityList_LBW))
                    {
                        UpParam.LB_CityID = model.GLookUp_CityList_LBW;
                    }
                    if (!string.IsNullOrEmpty(model.GLookUp_StateList_LBW))
                    {
                        UpParam.LB_StateID = model.GLookUp_StateList_LBW;
                    }
                    if (!string.IsNullOrEmpty(model.GLookUp_DistrictList_LBW))
                    {
                        UpParam.LB_DisttID = model.GLookUp_DistrictList_LBW;
                    }
                    UpParam.LB_PinCode = model.Txt_R_Pincode_LBW ?? "";
                    EditParam.param_UpdateLandAndBuilding = UpParam;
                    EditParam.RecID_DeleteComplexBuilding = model.xID;
                    EditParam.RecID_DeleteExtendedInfo = model.xID;
                    Ext_property_Data = Ext_property_Data.OrderBy(o => o.Sr).ToList();
                    Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfoParam = new Parameter_InsertExtendedInfo_LandAndBuilding[Ext_property_Data.Count];
                    if (Ext_property_Data.Count > 0 && Ext_property_Data[0].Sr > 0)
                    {
                        for (int i = 0; i < Ext_property_Data.Count; i++)
                        {
                            Parameter_InsertExtendedInfo_LandAndBuilding InEInfo = new Parameter_InsertExtendedInfo_LandAndBuilding();
                            InEInfo.LB_Rec_ID = model.xID;
                            InEInfo.SrNo = Ext_property_Data[i].Sr.ToString();
                            InEInfo.Inst_ID = Ext_property_Data[i].Ins_ID;
                            InEInfo.TotalArea = Ext_property_Data[i].Total_Plot_Area ?? 0.00;
                            InEInfo.ConstructedArea = Ext_property_Data[i].Constructed_Area ?? 0.00;
                            InEInfo.ConYear = Ext_property_Data[i].Construction_Year ?? "";
                            InEInfo.MOU_Date = Convert.ToDateTime(Ext_property_Data[i].M_O_U_Date).ToString(BASE._Server_Date_Format_Short);
                            InEInfo.Value = Ext_property_Data[i].Value ?? 0.00;
                            InEInfo.OtherDetails = Ext_property_Data[i].Other_Detail ?? "";
                            InEInfo.Status_Action = Status_Action;
                            InEInfo.RecID = System.Guid.NewGuid().ToString();
                            ExtInfoParam[i] = InEInfo;
                        }
                    }
                    EditParam.InExtendedInfo = ExtInfoParam;
                    EditParam.RecID_DeleteDocumentInfo = model.xID;
                    Parameter_InsertDocInfo_LandAndBuilding[] DocInfoParam = new Parameter_InsertDocInfo_LandAndBuilding[model.Document.Where(x => x.Selected == true).Count()];
                    int ctr1 = 0;
                    foreach (Documents currSelection in model.Document.Where(x => x.Selected == true))
                    {
                        Parameter_InsertDocInfo_LandAndBuilding InDInfo = new Parameter_InsertDocInfo_LandAndBuilding();
                        InDInfo.LB_Rec_ID = model.xID;
                        InDInfo.Doc_Misc_ID = currSelection.ID;
                        InDInfo.Status_Action = Status_Action;
                        InDInfo.RecID = System.Guid.NewGuid().ToString();
                        DocInfoParam[ctr1] = InDInfo;
                        ctr1++;
                    }
                    EditParam.InDocInfo = DocInfoParam;
                    if (!BASE._L_B_DBOps.UpdateProperty_Txn(EditParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if(model.isCallForPropertyTypeChange == true)
                    {
                        if (model.propertyType_PreviousValue != model.Cmd_PType_LBW)
                        {
                            DbOperations.LandAndBuilding.Param_Insert_PropertTypeChange InParam = new DbOperations.LandAndBuilding.Param_Insert_PropertTypeChange();
                            InParam.LB_REC_ID = model.xID;
                            InParam.LB_ORG_REC_ID = model.LB_ORG_REC_ID;
                            InParam.TYPE_FROM = model.propertyType_PreviousValue;
                            InParam.TYPE_TO = model.Cmd_PType_LBW;

                            if (!BASE._L_B_DBOps.InsertPropertTypeChangeLog(InParam))
                            {
                                jsonParam.message = Messages.SomeError + "<br> Operation: Inserting log of Property Type Change.";
                                jsonParam.title = "Error..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }

                    string Message = "";
                    DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Gift, model.xID);
                    if (Locations == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Locations.Rows.Count > 0)
                    {
                        Message = "<br><br>No Subsequent Changes have been made in Location mapped to this property.<br>User may make the required changes manually from Profile - > Core - > Locations.";
                    }
                    jsonParam.message = Messages.UpdateSuccess + Message;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xID
                    }, JsonRequestBehavior.AllowGet);
                }
                Param_Txn_DeleteProperty_LandAndBuilding DelParam = new Param_Txn_DeleteProperty_LandAndBuilding();
                //delete a record
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    DelParam.RecID_DeleteComplexBuilding = model.xID;
                    DelParam.RecID_DeleteExtendedInfo = model.xID;
                    DelParam.RecID_DeleteDocumentInfo = model.xID;
                    DelParam.RecID_Delete = model.xID;
                    DelParam.RecID_DeleteByLB = model.xID;
                    DelParam.LBOrgRecID_DeletePropertyTypeChangeLog = model.LB_ORG_REC_ID;
                    if (!BASE._L_B_DBOps.DeleteProperty_Txn(DelParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Cmd_PCategory_SelectedIndexChanged(string ActionMethod, string PCategory, string xID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            if (Tag == Common_Lib.Common.Navigation_Mode._New || Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (BASE._Completed_Year_Count > 0 && BASE._GoldSilverDBOps.IsTBImportedCentre() == false)
                {
                    if (Tag == Common.Navigation_Mode._New)
                    {
                        if (PCategory != "FREE USE" && PCategory != "RENTED" && PCategory != "LEASED (Short Term)" && PCategory != "MORTGAGE (Long Term)" && PCategory != "MORTGAGE (Short Term)" && PCategory != "MOU")
                        {
                            jsonParam.message = "Only Non-Accounting Properties can be created in Current Year..!<br><br>Please use vouchers to create Account related Properties...!";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (Tag == Common_Lib.Common.Navigation_Mode._Edit && PCategory.ToUpper() != defaultPropCategory.ToUpper())
                    {
                        if (PCategory != "FREE USE" && PCategory != "RENTED" && PCategory != "LEASED (Short Term)" && PCategory != "MORTGAGE (Long Term)" && PCategory != "MORTGAGE (Short Term)" && PCategory != "MOU")
                        {
                            jsonParam.message = "Only Non-Accounting Properties can be created in Current Year..!<br></br>Please use vouchers to create Account related Properties...!";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                                defaultPropCategory
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                    {
                        if (Tag == Common_Lib.Common.Navigation_Mode._New)
                        {
                            if (PCategory != "FREE USE" && PCategory != "RENTED" && PCategory != "LEASED (Short Term)" && PCategory != "MORTGAGE (Long Term)" && PCategory != "MORTGAGE (Short Term)" && PCategory != "MOU")
                            {
                                jsonParam.message = "Only Non-Accounting Properties are allowed for a newly created center..!<br></br>Please use vouchers to create Account related Properties...!";
                                jsonParam.title = "Information..";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (EntryType == "Profile Entry")
                            {
                                if (PCategory != "FREE USE" && PCategory != "RENTED" && PCategory != "LEASED (Short Term)" && PCategory != "MORTGAGE (Long Term)" && PCategory != "MORTGAGE (Short Term)" && PCategory != "MOU")
                                {
                                    jsonParam.message = "Only Non-Accounting Properties are allowed for a newly created center..!<br></br>Please use vouchers to create Account related Properties...!";
                                    jsonParam.title = "Information..";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            if (Tag == Common.Navigation_Mode._Edit)
            {
                Boolean? IsLBCarriedFwd = BASE._L_B_DBOps.IsPropertyCarriedForward(xID, Convert.ToInt32(YearID));
                if (IsLBCarriedFwd == null)
                {
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsLBCarriedFwd == true)
                {
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        IsLBCarriedFwd,
                        _open_User_Type = BASE._open_User_Type
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            jsonParam.result = true;
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cmd_PUse_SelectedIndexChanged()
        {
            DataTable CenterData = (DataTable)BASE._L_B_DBOps.GetCenterDetails();
            if (CenterData == null)
            {
                return Json(new
                {
                    result = false,
                    message = Common_Lib.Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Txt_PName = string.Empty;
            string Txt_R_Add1 = string.Empty;
            string Txt_R_Add2 = string.Empty;
            string Txt_R_Add3 = string.Empty;
            string Txt_R_Add4 = string.Empty;
            string Cen_State_ID = string.Empty;
            string Cen_District_ID = string.Empty;
            string Cen_City_ID = string.Empty;
            string Txt_R_Pincode = string.Empty;
            if (CenterData.Rows.Count > 0)
            {
                Txt_PName = CenterData.Rows[0]["CEN_B_NAME"].ToString();
                Txt_R_Add1 = CenterData.Rows[0]["CEN_ADD1"].ToString();
                Txt_R_Add2 = CenterData.Rows[0]["CEN_ADD2"].ToString();
                Txt_R_Add3 = CenterData.Rows[0]["CEN_ADD3"].ToString();
                Txt_R_Add4 = CenterData.Rows[0]["CEN_ADD4"].ToString();

                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_STATE_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_STATE_ID"].ToString().Length > 0)
                    {
                        Cen_State_ID = CenterData.Rows[0]["CEN_STATE_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_DISTRICT_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_DISTRICT_ID"].ToString().Length > 0)
                    {
                        Cen_District_ID = CenterData.Rows[0]["CEN_DISTRICT_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(CenterData.Rows[0]["CEN_CITY_ID"]))
                {
                    if (CenterData.Rows[0]["CEN_CITY_ID"].ToString().Length > 0)
                    {
                        Cen_City_ID = CenterData.Rows[0]["CEN_CITY_ID"].ToString();
                    }
                }
                Txt_R_Pincode = CenterData.Rows[0]["CEN_PINCODE"].ToString();
            }
            if (Txt_R_Add1.Length > 0)
            {
                return Json(new
                {
                    result = true,
                    message = "",
                    Txt_PName,
                    Txt_R_Add1,
                    Txt_R_Add2,
                    Txt_R_Add3,
                    Txt_R_Add4,
                    Cen_State_ID,
                    Cen_District_ID,
                    Cen_City_ID,
                    Txt_R_Pincode
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = false,
                message = Common_Lib.Messages.SomeError
            }, JsonRequestBehavior.AllowGet);
        }
        public void RefreshOwnerList()
        {
            DataTable d1 = BASE._VehicleDBOps.GetOwners_List() as DataTable;
            OwnerList_DD_LBW = DatatableToModel.DataTabletoADDRESS_BOOK(d1);
        }
        public ActionResult LookUp_GetOwnList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (DDRefresh == true || OwnerList_DD_LBW == null)
            {
                RefreshOwnerList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(OwnerList_DD_LBW, loadOptions)), "application/json");

        }
        #region Address
        public ActionResult Frm_Property_Window_Property_Address(string Txt_Address, string Txt_R_Add1, string Txt_R_Add2, string Txt_R_Add3, string Txt_R_Add4, string GLookUp_StateList, string GLookUp_DistrictList, string GLookUp_CityList, string Txt_R_Pincode)
        {
            var model = new ConnectOneMVC.Areas.Profile.Models.PropertyWindowProfile();
            model.Txt_Address_LBW = Txt_Address;
            model.Txt_R_Add1_LBW = Txt_R_Add1;
            model.Txt_R_Add2_LBW = Txt_R_Add2;
            model.Txt_R_Add3_LBW = Txt_R_Add3;
            model.Txt_R_Add4_LBW = Txt_R_Add4;
            model.GLookUp_StateList_LBW = GLookUp_StateList;
            model.GLookUp_DistrictList_LBW = GLookUp_DistrictList;
            model.GLookUp_CityList_LBW = GLookUp_CityList;
            model.Txt_R_Pincode_LBW = Txt_R_Pincode;
            return PartialView(model);
        }
        public ActionResult RefreshStateList()
        {
            var d1 = BASE._Address_DBOps.GetStates("IN", "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            if (d1 == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            d1.Sort((x, y) => x.R_ST_NAME.CompareTo(y.R_ST_NAME));
            StateList_DD_LBW = d1;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_StateList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (StateList_DD_LBW == null || DDRefresh == true)
            {
                RefreshStateList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StateList_DD_LBW, loadOptions)), "application/json");
        }
        public ActionResult RefreshDistrictList(string state)
        {
            var d1 = BASE._Address_DBOps.GetDistricts("IN", string.IsNullOrEmpty(state) ? "0" : state, "R_DI_NAME", "R_DI_REC_ID");
            if (d1 == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            d1.Sort((x, y) => x.R_DI_NAME.CompareTo(y.R_DI_NAME));
            DistrictList_DD_LBW = d1;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_DistrictList(DataSourceLoadOptions loadOptions, string state, bool DDRefresh = false)
        {
            if (DistrictList_DD_LBW == null || DDRefresh == true)
            {
                RefreshDistrictList(state);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DistrictList_DD_LBW, loadOptions)), "application/json");

        }
        public ActionResult RefreshCityList(string state)
        {
            var d1 = BASE._Address_DBOps.GetCitiesBySt_Co_Code("IN", string.IsNullOrEmpty(state) ? "0" : state, "R_CI_NAME", "R_CI_REC_ID");
            if (d1 == null)
            {
                return Json(new
                {
                    message = Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            d1.Sort((x, y) => x.R_CI_NAME.CompareTo(y.R_CI_NAME));
            CityList_DD_LBW = d1;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_CityList(DataSourceLoadOptions loadOptions, string state, bool DDRefresh = false)
        {
            if (CityList_DD_LBW == null || DDRefresh == true)
            {
                RefreshCityList(state);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(CityList_DD_LBW, loadOptions)), "application/json");

        }
        #endregion
        #region Extended
        public ActionResult SetGridData(string ActionMethodName, string ID = null)
        {
            if (Ext_property_Data == null)
            {
                DataTable DT = new DataTable();
                DataRow ROW;
                Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethodName);
                DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
                DT.Columns.Add("Institution", Type.GetType("System.String"));
                DT.Columns.Add("Ins_ID", Type.GetType("System.String"));
                DT.Columns.Add("Total Plot Area (Sq.Ft.)", Type.GetType("System.Double"));
                DT.Columns.Add("Constructed Area (Sq.Ft.)", Type.GetType("System.Double"));
                DT.Columns.Add("Construction Year", Type.GetType("System.String"));
                DT.Columns.Add("M.O.U. Date", Type.GetType("System.String"));
                DT.Columns.Add("Value", Type.GetType("System.Double"));
                DT.Columns.Add("Other Detail", Type.GetType("System.String"));
                if (Tag == Common_Lib.Common.Navigation_Mode._New)
                {
                    ROW = DT.NewRow();
                    DT.Rows.Add(ROW);
                }

                if ((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            || ((Tag == Common_Lib.Common.Navigation_Mode._Delete)
                            || (Tag == Common_Lib.Common.Navigation_Mode._View)))
                {
                    DataTable INS_Table = BASE._L_B_DBOps.GetInstitutes();
                    DataTable EXT_Table = BASE._L_B_DBOps.GetExtendedRecord(ID);
                    if (INS_Table == null || EXT_Table == null)
                    {
                        return Json(new
                        {
                            result = false,
                            message = Common_Lib.Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //BUILD DATA
                    var BuildData = from EXT in EXT_Table.AsEnumerable()
                                    join I in INS_Table.AsEnumerable()
                                on EXT.Field<string>("LB_INS_ID") equals I.Field<string>("INS_ID")
                                    select new
                                    {
                                        LB_SR_NO = EXT.Field<Int32>("LB_SR_NO"),
                                        INS_NAME = I.Field<String>("INS_NAME"),
                                        INS_ID = I.Field<String>("INS_ID"),
                                        LB_TOT_P_AREA = EXT.Field<Decimal>("LB_TOT_P_AREA"),
                                        LB_CON_AREA = EXT.Field<Decimal>("LB_CON_AREA"),
                                        LB_CON_YEAR = EXT.Field<String>("LB_CON_YEAR"),
                                        LB_MOU_DATE = EXT.Field<DateTime?>("LB_MOU_DATE"),
                                        LB_VALUE = EXT.Field<Decimal>("LB_VALUE"),
                                        LB_OTHER_DETAIL = EXT.Field<String>("LB_OTHER_DETAIL")
                                    };
                    var Final_Data = BuildData.ToList();
                    foreach (var FieldName in Final_Data)
                    {
                        ROW = DT.NewRow();
                        ROW["Sr."] = FieldName.LB_SR_NO;
                        ROW["Institution"] = FieldName.INS_NAME;
                        ROW["Ins_ID"] = FieldName.INS_ID;
                        ROW["Total Plot Area (Sq.Ft.)"] = FieldName.LB_TOT_P_AREA;
                        ROW["Constructed Area (Sq.Ft.)"] = FieldName.LB_CON_AREA;
                        ROW["Construction Year"] = FieldName.LB_CON_YEAR;
                        ROW["M.O.U. Date"] = string.Format(Convert.ToDateTime(FieldName.LB_MOU_DATE).ToString(), BASE._Date_Format_Current);
                        ROW["Value"] = FieldName.LB_VALUE;
                        ROW["Other Detail"] = FieldName.LB_OTHER_DETAIL;
                        DT.Rows.Add(ROW);
                    }
                    if (DT.Rows.Count == 0)
                    {
                        ROW = DT.NewRow();
                        DT.Rows.Add(ROW);
                    }
                }
                Ext_property_Data = DatatableToModel.DataTabletoProperty_Window_Grid_INFO(DT);
            }
            return PartialView(Ext_property_Data);
        }
        public ActionResult Frm_Property_Window_Ext(string ActionMethod, string SrID = null)
        {
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);
            Frm_Property_Window_Ext_Model model = new Frm_Property_Window_Ext_Model();
            List<int> YearBind = new List<int>();
            for (int i = BASE._open_Year_Sdt.Year; i >= 1900; i += -1)
            {
                YearBind.Add(i);
            }
            ViewBag.Cmd_Ext_Con_Year_Bind = YearBind;
            RefreshInsList();
            if (ActionMethod == "_Edit")
            {
                model.ActionMethod = Tag;
                var Sr = Convert.ToInt16(SrID);
                var dataToEdit = Ext_property_Data.FirstOrDefault(x => x.Sr == Sr);
                model.Sr_LBW = Sr;
                model.Institution = dataToEdit.Institution;
                model.Look_InsList_LBW = dataToEdit.Ins_ID;
                model.Txt_Ext_Tot_Area_LBW = dataToEdit.Total_Plot_Area;
                model.Txt_Ext_Con_Area_LBW = dataToEdit.Constructed_Area;
                model.Cmd_Ext_Con_Year_LBW = string.IsNullOrEmpty(dataToEdit.Construction_Year) ? (int?)null : Convert.ToInt32(dataToEdit.Construction_Year);
                model.Txt_LB_Ext_Amount_LBW = dataToEdit.Value;
                model.Txt_Others_LBW = dataToEdit.Other_Detail;
                model.Txt_MOU_Date_LBW = Convert.ToDateTime(dataToEdit.M_O_U_Date);
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Property_Window_Ext(Frm_Property_Window_Ext_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            model.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());

            if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (string.IsNullOrEmpty(model.Look_InsList_LBW))
                {
                    jsonParam.message = "Institute Not Selected...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Look_InsList_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);

                }
                if (model.Txt_Ext_Tot_Area_LBW < 0)
                {
                    jsonParam.message = "Total Plot Area cannot be Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Ext_Tot_Area_LBW";

                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Ext_Con_Area_LBW < 0)
                {
                    jsonParam.message = "Construction Area cannot be Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Ext_Con_Area_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Cmd_Ext_Con_Year_LBW != null && model.Cmd_Ext_Con_Year_LBW != 0)
                {
                    if (Convert.ToInt32(model.Cmd_Ext_Con_Year_LBW) > Convert.ToInt32(BASE._open_Year_Sdt.Year))
                    {
                        jsonParam.message = "Construction Year must be Less than/Equal to Start Financial Year...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmd_Ext_Con_Year_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Txt_MOU_Date_LBW == null || IsDate(model.Txt_MOU_Date_LBW.ToString()) == false)
                {
                    jsonParam.message = "Date Incorrect/Blank...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_MOU_Date_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            List<Property_Window_Grid> gridRows = new List<Property_Window_Grid>();
            if (Ext_property_Data != null)
            {
                gridRows = Ext_property_Data;
            }
            gridRows.RemoveAll(x => x.Sr == null);
            if (model.Tag == Common.Navigation_Mode._New)
            {
                Property_Window_Grid grid = new Property_Window_Grid();

                grid.Institution = model.Institution;
                grid.Ins_ID = model.Look_InsList_LBW;
                grid.Total_Plot_Area = model.Txt_Ext_Tot_Area_LBW ?? 0.00;
                grid.Constructed_Area = model.Txt_Ext_Con_Area_LBW ?? 0.00;
                grid.Construction_Year = Convert.ToString(model.Cmd_Ext_Con_Year_LBW);
                grid.M_O_U_Date = Convert.ToDateTime(model.Txt_MOU_Date_LBW).ToString("dd/MM/yyyy");
                grid.Value = model.Txt_LB_Ext_Amount_LBW ?? 0;
                grid.Other_Detail = model.Txt_Others_LBW;
                gridRows.Add(grid);
            }
            else if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr_LBW);
                dataToEdit.Institution = model.Institution;
                dataToEdit.Ins_ID = model.Look_InsList_LBW;
                dataToEdit.Total_Plot_Area = model.Txt_Ext_Tot_Area_LBW ?? 0.00;
                dataToEdit.Constructed_Area = model.Txt_Ext_Con_Area_LBW ?? 0.00;
                dataToEdit.Construction_Year = Convert.ToString(model.Cmd_Ext_Con_Year_LBW);
                dataToEdit.M_O_U_Date = Convert.ToDateTime(model.Txt_MOU_Date_LBW).ToString("dd/MM/yyyy");
                dataToEdit.Value = model.Txt_LB_Ext_Amount_LBW ?? 0;
                dataToEdit.Other_Detail = model.Txt_Others_LBW;
            }
            for (int i = 0; i < gridRows.Count; i++)
            {
                gridRows[i].Sr = i + 1;
            }
            Ext_property_Data = gridRows;
            jsonParam.result = true;
            jsonParam.message = "";
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Property_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            var allData = (List<ConnectOneMVC.Areas.Profile.Models.Property_Window_Grid>)Ext_property_Data;
            var dataToDelete = allData.FirstOrDefault(x => x.Sr == Sr);
            allData.Remove(dataToDelete);
            for (int i = 0; i < allData.Count; i++)
            {
                allData[i].Sr = i + 1;
            }
            Ext_property_Data = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public void RefreshInsList()
        {
            Institution_DD_LBW = DatatableToModel.DataTabletoLookUp_GetInsList_INFO(BASE._L_B_DBOps.GetInstitutes());
        }
        public ActionResult LookUp_GetInsList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (Institution_DD_LBW == null || DDRefresh == true)
            {
                RefreshInsList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Institution_DD_LBW, loadOptions)), "application/json");
        }

        #endregion
        #region MOU Property
        public ActionResult Frm_MOU_Property_Select(string LB_REC_ID = "", string Txn_M_ID = "")
        {
            //DataTable _db_Table = BASE._L_B_DBOps.GetListForMOU(ClientScreen.Profile_LandAndBuilding);
            //if (_db_Table == null)
            //{
            //    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('MOU_Property_Select_popup','Some Error Occurred During The Current Operation!!','Error!!');</script>");
            //}
            //else
            //{
            //    MOU_property_data = _db_Table;
            //}
            ViewBag.LB_REC_ID = LB_REC_ID;
            ViewBag.Txn_M_ID = Txn_M_ID;
            return View();
        }
        public ActionResult Frm_MOU_Property_Select_Grid()
        {
            return Content(JsonConvert.SerializeObject(BASE._L_B_DBOps.GetListForMOU(ClientScreen.Profile_LandAndBuilding)), "application/json");        
            //return View(MOU_property_data);
        }
        #endregion
        private string FindLocationUsage(string PropertyID, bool Exclude_Sold_TF = true)
        {
            string Message = "";
            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Gift, PropertyID);
            for (int i = 0; i < Locations.Rows.Count; i++)
            {
                string LocationID = Locations.Rows[i][0].ToString();
                string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF);
                bool DeleteAllow = true;
                if (UsedPage.Length > 0)
                {
                    DeleteAllow = false;
                }
                if (!DeleteAllow)
                {
                    Message = "Property being deleted in this Voucher is being used in Another Page as Location...!<br></br>Name : " + UsedPage;
                    break;
                }
            }
            return Message;
        }
        private bool IsAccountingPropertyCategory(string Category, bool infoPageCall = false)
        {
            if (infoPageCall == true)
            {
                if (Category.ToUpper() == "PURCHASED" || Category.ToUpper() == "PURCHASED AND CONSTRUCTED" || Category.ToUpper() == "GIFTED" || Category.ToUpper() == "GIFTED AND CONSTRUCTED" || Category.ToUpper() == "LEASED (LONG TERM)" || Category.ToUpper() == "LEASED AND CONSTRUCTED (LONG TERM)")
                {
                    return true;
                }
            }
            else
            {
                if (Category.ToUpper() == "PURCHASED" || Category.ToUpper() == "PURCHASED AND CONSTRUCTED" || Category.ToUpper() == "GIFTED" || Category.ToUpper() == "GIFTED AND CONSTRUCTED" || Category.ToUpper() == "LEASED (LONG TERM)" || Category.ToUpper() == "LEASED AND CONSTRUCTED (LONG TERM)" || Category.ToUpper() == "MOU")
                {
                    return true;
                }
            }
            return false;
        }
        public ActionResult Frm_Property_Change_Rented_Detail(string xID, string Info_LastEditedOn = null)
        {
            PropertyWindowProfile model = new PropertyWindowProfile();
            model.xID = xID;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_Edit");
            model.TempActionMethod = model.Tag.ToString();
            model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
            DataTable d1 = BASE._L_B_DBOps.GetRecord(model.xID);
            model.Cmd_PCategory_LBW = d1.Rows[0]["LB_PRO_CATEGORY"].ToString();
            if (model.Cmd_PCategory_LBW.Contains("LEASED") || model.Cmd_PCategory_LBW.Contains("MORTGAGE") || model.Cmd_PCategory_LBW.Contains("RENTED"))
            {
                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xID);
                if (SaleRecord != null)
                {
                    if (SaleRecord.Rows.Count > 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Rent Details Cannot Be Changed..<br>This Property has already been Sold...!','Information');</script>");
                    }
                }
                if (BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_LandAndBuilding, 0, xID).Rows.Count > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Rent Details Cannot Be Changed..<br>This Property has already been transferred to another centre.!','Information');</script>");
                }
                model.Txt_Dep_Amt_LBW = Convert.ToDouble(d1.Rows[0]["LB_DEPOSIT_AMT"]);
                model.Txt_Mon_Rent_LBW = Convert.ToDouble(d1.Rows[0]["LB_MONTH_RENT"]);
                model.Txt_Other_Payments_LBW = Convert.ToDouble(d1.Rows[0]["LB_MONTH_O_PAYMENTS"]);
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PAID_DATE"]))
                {
                    model.Txt_PaidDate_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PAID_DATE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PERIOD_FROM"]))
                {
                    model.Txt_F_Date_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PERIOD_FROM"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["LB_PERIOD_TO"]))
                {
                    model.Txt_T_Date_LBW = Convert.ToDateTime(d1.Rows[0]["LB_PERIOD_TO"]);
                }
                return View(model);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Rent Details Can Only Be Changed For Leased,Mortage,Rented Property','Information');</script>");
            }
        }
        [HttpPost]
        public ActionResult Frm_Property_Change_Rented_Detail(PropertyWindowProfile model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    DataTable d1 = BASE._L_B_DBOps.GetRecord(model.xID);
                    if (d1.Rows.Count == 0)
                    {
                        jsonParam.message = Messages.RecordChanged("Current Property(Rent Details)");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                    {
                        jsonParam.message = Messages.RecordChanged("Current Property(Rent Details)");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_LandAndBuilding, 0, model.xID).Rows.Count > 0)
                    {
                        jsonParam.message = "Rent Details Cannot Be Changed.. <br> This Property has already been transferred to another centre.!";
                        jsonParam.title = "Referred record already changed...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable TR_TABLE = null;
                    if (BASE._prev_Unaudited_YearID != 0)
                    {
                        TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._prev_Unaudited_YearID);
                    }
                    else
                    {
                        TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                    }
                    var sale_qty = 0;
                    if (TR_TABLE.Rows.Count > 0)
                    {
                        sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                    }
                    if (BASE._prev_Unaudited_YearID != 0)
                    {
                        TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                        if (TR_TABLE.Rows.Count > 0)
                        {
                            sale_qty += Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                        }
                    }
                    if (sale_qty != 0)
                    {
                        jsonParam.message = "Rent Details Cannot Be Changed.. <br> This Property has already been sold..!";
                        jsonParam.title = "Referred record already changed...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Txt_Dep_Amt_LBW < 0)
                {
                    jsonParam.message = "Security Deposit Amount cannot be Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Dep_Amt_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Mon_Rent_LBW < 0)
                {
                    jsonParam.message = "Monthly Rent Amount cannot be Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Mon_Rent_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Other_Payments_LBW < 0)
                {
                    jsonParam.message = "Other Monthly Payment cannot be Negative...!";
                    jsonParam.title = "Incomplete Information..";
                    jsonParam.result = false;
                    jsonParam.focusid = "Txt_Other_Payments_LBW";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (IsDate(model.Txt_F_Date_LBW.ToString()) == true && IsDate(model.Txt_T_Date_LBW.ToString()) == true)
                {
                    if (Convert.ToDateTime(model.Txt_F_Date_LBW) >= Convert.ToDateTime(model.Txt_T_Date_LBW))
                    {
                        jsonParam.message = "To Date must be Higher than From Date...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_T_Date_LBW";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Cmd_PCategory_LBW.ToUpper() == "LEASED (LONG TERM)" || model.Cmd_PCategory_LBW.ToUpper() == "LEASED AND CONSTRUCTED (LONG TERM)")
                {
                    if (IsDate(model.Txt_F_Date_LBW.ToString()) == true & IsDate(model.Txt_T_Date_LBW.ToString()) == true)
                    {
                        double diff = (Convert.ToDateTime(model.Txt_T_Date_LBW) - Convert.ToDateTime(model.Txt_F_Date_LBW)).TotalDays;
                        if (diff < 3650)
                        {
                            jsonParam.message = "Leased(Long Term) Period Cannot be Less than 10 Years...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_T_Date_LBW";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                Parameter_Update_Property_RentDetails UpParam = new Common_Lib.RealTimeService.Parameter_Update_Property_RentDetails();
                UpParam.DepositAmount = model.Txt_Dep_Amt_LBW ?? 0.00;
                if (IsDate(model.Txt_PaidDate_LBW.ToString()))
                {
                    UpParam.PaymentDate = Convert.ToDateTime(model.Txt_PaidDate_LBW).ToString(BASE._Server_Date_Format_Long);
                }
                UpParam.MonthlyRent = model.Txt_Mon_Rent_LBW ?? 0.00;
                UpParam.MonthlyOtherExpenses = model.Txt_Other_Payments_LBW ?? 0.00;
                if (IsDate(model.Txt_F_Date_LBW.ToString()))
                {
                    UpParam.PeriodFrom = Convert.ToDateTime(model.Txt_F_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                }
                if (IsDate(model.Txt_T_Date_LBW.ToString()))
                {
                    UpParam.PeriodTo = Convert.ToDateTime(model.Txt_T_Date_LBW).ToString(BASE._Server_Date_Format_Long);
                }
                UpParam.RecID = model.xID;
                if (!BASE._L_B_DBOps.UpdateRentDetails_Property_Txn(UpParam))
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error..";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.UpdateSuccess;
                jsonParam.title = model.TitleX;
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    xid = model.xID
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Common_Lib.DbOperations.Return_LB_Documents> Get_Documents_List()
        {
            return BASE._L_B_DBOps.GetDocumentsList("ID", "Name");
        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void SessionClear_Window()
        {
            ClearBaseSession("_PropertyWindow");
        }
        public void SessionClear()
        {
            ClearBaseSession("_PropertyInfo");
            Session.Remove("PropertyInfo_detailGrid_Data");
        }
        public void Property_user_rights()
        {
            ViewData["PropertyLB_AddRight"] = CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Add");
            ViewData["PropertyLB_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Update");
            ViewData["PropertyLB_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "View");
            ViewData["PropertyLB_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Delete");
            ViewData["PropertyLB_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Export");
            ViewData["PropertyLB_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["PropertyLB_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Lock_Unlock);
            ViewData["PropertyLB_ManageRemarks"] = BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Manage_Remarks);
            ViewData["PropertyLB_ViewRemarks"] = BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.View_Remarks);
            ViewData["PropertyLB_SpecialGrouping"] = BASE.CheckActionRights(ClientScreen.Profile_LandAndBuilding, Common.ClientAction.Special_Groupings);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }


        #region Devextreme
        public ActionResult Frm_Property_Info_dx()
        {
            if (!(CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_LandAndBuilding').hide();</script>");
            }
            Property_user_rights();

            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.TitleX = "Land & Building Information";//(BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR" && BASE._open_Ins_ID != "00009") ? "Land & Building Information(Rented , Free Use Only)" :
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_LandAndBuilding).ToString()) ? 1 : 0;

            ViewData["PropertyLB_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                     || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            //Grid_Display();
            return View();
        }
        [HttpGet]
        public ActionResult Frm_Property_Info_dx_Grid_Load()
        {
            Property_user_rights();
            Grid_Display();
            return Content(JsonConvert.SerializeObject(Property_ExportData), "application/json");
        }

        public ActionResult Frm_Property_Info_DetailGrid_dx_Load(bool VouchingMode = false, string RecID = "", string MID="")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_LandAndBuilding, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Property_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_LandAndBuilding)), "application/json");
        }


        public ActionResult Frm_Export_Options_dx(string GridName)
        {
            if (!CheckRights(BASE, ClientScreen.Profile_LandAndBuilding, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Property_report_modal','Not Allowed','No Rights');$('#PropertyModelListPreview').hide();</script>");
            }
            ViewBag.GridName = GridName;
            ViewBag.Filename = GridName + "_" + BASE._open_UID_No + "_" + BASE._open_Year_ID;
            return View("Common_Export");           
        }

        #endregion 
    }
}