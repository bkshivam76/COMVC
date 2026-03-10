using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class VehicleController : BaseController
    {
        // GET: Profile/Vehicle
        #region <--Global Variables-->
        public int MaskType
        {
            get
            {
                return (int)GetBaseSession("MaskType_Vehicle");
            }
            set
            {
                SetBaseSession("MaskType_Vehicle", value);
            }
        }
        public DateTime LastEditedOn
        {
            get
            {
                return (DateTime)GetBaseSession("LastEditedOn_Vehicle");
            }
            set
            {
                SetBaseSession("LastEditedOn_Vehicle", value);
            }
        }
        public string Voucher_Entry
        {
            get
            {
                return (string)GetBaseSession("Voucher_Entry_Vehicle");
            }
            set
            {
                SetBaseSession("Voucher_Entry_Vehicle", value);
            }
        }
        public string Profile_Entry
        {
            get
            {
                return (string)GetBaseSession("Profile_Entry_Vehicle");
            }
            set
            {
                SetBaseSession("Profile_Entry_Vehicle", value);
            }
        }
        public List<VehiclesInfo> Vehicles_ExportData
        {
            get
            {
                return (List<VehiclesInfo>)GetBaseSession("Vehicles_ExportData_Vehicle");
            }
            set
            {
                SetBaseSession("Vehicles_ExportData_Vehicle", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> VehiclesInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("VehiclesInfo_DetailGrid_Data_Vehicle");
            }
            set
            {
                SetBaseSession("VehiclesInfo_DetailGrid_Data_Vehicle", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> VehiclesInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("VehiclesInfo_AdditionalInfoGrid_Vehicle");
            }
            set
            {
                SetBaseSession("VehiclesInfo_AdditionalInfoGrid_Vehicle", value);
            }
        }
        #endregion

        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/Advances Grid_Display() Of ALL_Projects)

        public void SetDefaultValues()
        {
            MaskType = 0;
            LastEditedOn = default(DateTime);

            Voucher_Entry = "Voucher Entry";
            Profile_Entry = "Profile Entry";
        }
        public ActionResult Frm_Vehicles_Info()
        {
            SetDefaultValues();
            Vehicle_user_rights();
            ViewBag.Special_Groupings = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Special_Groupings);
            ViewBag.Manage_Remarks = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Manage_Remarks);

            if (!CheckRights(BASE, ClientScreen.Profile_Vehicles, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Vehicles').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Vehicles).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing VehProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            VehProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
            VehProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            VehProfile.Next_YearID = BASE._next_Unaudited_YearID;
            VehProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
            DataTable VI_Table = BASE._VehicleDBOps.GetProfileListing(VehProfile);
            //  Base._VehicleDBOps.GetList(Voucher_Entry, Profile_Entry)

            ViewData["Vehicle_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((VI_Table == null))
            {
                return View();
            }
            else
            {
                var vehicledata = DatatableToModel.DataTabletoVehicles_INFO(VI_Table);
                Vehicles_ExportData = vehicledata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();


                return View(vehicledata);
            }
        }

        public PartialViewResult Frm_Vehicles_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Vehicle_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.RowKeyToFocus = RowKeyToFocus;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.Special_Groupings = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Special_Groupings);
            ViewBag.Manage_Remarks = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Manage_Remarks);
            if (Vehicles_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetProfileListing VehProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
                VehProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
                VehProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                VehProfile.Next_YearID = BASE._next_Unaudited_YearID;
                VehProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
                DataTable VI_Table = BASE._VehicleDBOps.GetProfileListing(VehProfile);
                //  Base._VehicleDBOps.GetList(Voucher_Entry, Profile_Entry)
                if ((VI_Table == null))
                {
                    return PartialView();
                }
                else
                {
                    var vehicledata = DatatableToModel.DataTabletoVehicles_INFO(VI_Table);
                    Vehicles_ExportData = vehicledata;

                }
            }
            return PartialView(Vehicles_ExportData);
        }

        #region <--Nested Grid-->
        public ActionResult Frm_Vehicles_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.VehiclesInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VehiclesInfo_RecID = RecID;
            ViewBag.VehiclesInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    VehiclesInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Vehicles);
                    Session["VehiclesInfo_detailGrid_Data"] = VehiclesInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Vehicles);
                    VehiclesInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["VehiclesInfo_detailGrid_Data"] = data.DocumentMapping;
                    VehiclesInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(VehiclesInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(VehiclesInfo_AdditionalInfoGrid);
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
            settings.Name = "VehiclesListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "VehiclesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["VehiclesInfo_detailGrid_Data"];
        }
        #endregion

        public ActionResult VehiclesCustomDataAction(string key)
        {
            Common_Lib.RealTimeService.Param_GetProfileListing VehProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            VehProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
            VehProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            VehProfile.Next_YearID = BASE._next_Unaudited_YearID;
            VehProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
            DataTable VI_Table = BASE._VehicleDBOps.GetProfileListing(VehProfile);
            var Final_Data = DatatableToModel.DataTabletoVehicles_INFO(VI_Table);
            var it = (VehiclesInfo)Final_Data.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.ID + "![" + it.Edit_Date + "![" + it.Add_Date + "![" + it.Add_By + "![" + it.Edit_By + "![" + it.Action_Status + "![" + it.YEAR_ID + "!["
                             + it.ITEM_NAME + "![" + it.MAKE + "![" + it.Model + "![" + it.VI_REG_NO + "![" + it.INSURANCE_ID + "![" + it.VI_INS_POLICY_NO + "!["
                             + it.Expiry_Date + "![" + it.Ownership + "![" + it.Opening_Value + "![" + it.TR_ID + "![" + it.Open_Actions + "!["
                             + it.AL_LOC_AL_ID + "![" + it.Entry_Type + "![" + it.Action_Date + "![" + it.Action_By;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        #endregion

        #region <--Create Detail-->
        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
            string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
                string Pic_Status = string.Empty;
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;
                string Lbl_Status_Color = string.Empty;
                try
                {
                    Status = Action_Status;
                }
                catch (Exception ex)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Pic_Status = "Fa Fa-Lock";
                    Lbl_Status_Color = "blue";
                }
                else
                {
                    Pic_Status = "Fa Fa-UnLock";
                    Lbl_Status = Status;
                    if (Status.ToUpper().Trim().ToString() == "COMPLETED")
                        Lbl_Status_Color = "green";
                    else
                        Lbl_Status_Color = "red";
                }
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).Date.ToString("dd-MM-yyyy")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Locked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Locked On: " + "?";
                    }
                    Lbl_StatusBy = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                else
                {

                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Unlocked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Unlocked On: " + "?";
                    }
                    Lbl_StatusBy = "Unlocked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Lbl_Status_Color,
                    Pic_Status = Pic_Status,
                    Lbl_StatusBy = Lbl_StatusBy,
                    Lbl_StatusOn = Lbl_StatusOn
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = "",
                    Pic_Status = "",
                    Lbl_StatusBy = "",
                    Lbl_StatusOn = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        #endregion

        public ActionResult LookUp_Get_Vehicles_List(DataSourceLoadOptions loadOptions)
        {
            DataTable vehiclelist = BASE._VehicleDBOps.GetVehicles("ITEM_ID", "INSURANCE_COMPANY") as DataTable;
            var vehicledata = DatatableToModel.DataTabletoVehicles_INFO(vehiclelist);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(vehicledata, loadOptions)), "application/json");
        }

        #region <--Export-->
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Vehicles, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Vehicles_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion

        #region <--Change Insurance-->

        [HttpGet]
        public ActionResult Frm_Change_Insurance_Window(string Tag = null, string xID = null, string Rad_Insurance = "0", string xName = null, string screen = null,
                                                        string xDesc = null, string xIns_ID = null, string Txt_PolicyNo = null, string Txt_I_Date = null, string Info_LastEditedOn = null)
        {
            VehicleInsurance model = new VehicleInsurance();
            model.Tag = Tag;
            model.Desc = xDesc; 
            model.Look_InsList = xIns_ID; 
            model.Txt_PolicyNo = Txt_PolicyNo;     
            model.xID = xID;
            model.Look_ItemList = xName;
            model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
            model.Screen = screen;
            model.Rad_Insurance = Convert.ToInt16(Rad_Insurance);
            model.Insurance_DD_Data = RefreshInsuranceListDropdown();
            if (Txt_I_Date != null && Txt_I_Date != "")
            {
                model.Txt_I_Date = Convert.ToDateTime(Txt_I_Date);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Insurance_Window(VehicleInsurance model)
        {
            //All the Checks
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit.ToString())
                    {
                        if (model.Screen == "LiveStock")
                        {
                            DataTable livestock_DbOps = BASE._LiveStockDBOps.GetRecord(model.xID);
                            if (livestock_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = "RecordChanged('Current LiveStock')";
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                            if (model.Info_LastEditedOn != Convert.ToDateTime(livestock_DbOps.Rows[0]["REC_EDIT_ON"]))
                            {
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.message = "RecordChanged('Current LiveStock')";
                                jsonParam.result = false;
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            //#Ref N16
                            if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_LiveStock, Convert.ToInt32(null), model.xID).Rows.Count > 0)
                            {
                                jsonParam.title = "Referred record already changed...";
                                jsonParam.message = "Insurance detail Cannot be changed..! <br><br> Livestock has already been transferred to another centre.";
                                jsonParam.result = false;
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        { //Vehicles
                            DataTable vehicle_DbOps = BASE._VehicleDBOps.GetRecord(model.xID);
                            if (vehicle_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = Common_Lib.Messages.RecordChanged("Current Asset(Insurance)");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                            if (model.Info_LastEditedOn != Convert.ToDateTime(vehicle_DbOps.Rows[0]["REC_EDIT_ON"].ToString()))
                            {
                                jsonParam.message = Common_Lib.Messages.RecordChanged("Current Asset(Insurance)");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            //#Ref N14
                            if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Convert.ToInt16(null), model.xID).Rows.Count > 0)
                            {
                                jsonParam.title = "Referred record already changed...";
                                jsonParam.message = "Insurance detail Cannot be changed..! <br><br> This Vehicle has already been transferred to another centre.";
                                jsonParam.DialogResult = Common.DialogResult.Retry;
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        DataTable TR_TABLE = null;
                        if (BASE._prev_Unaudited_YearID != Convert.ToInt16(null))
                        {
                            TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._prev_Unaudited_YearID);
                        }
                        else
                        {
                            TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                        }
                        //Check for sale of asset
                        int sale_qty = 0;
                        if (TR_TABLE.Rows.Count > 0)
                        {
                            sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                        }

                        if (BASE._prev_Unaudited_YearID != Convert.ToInt32(null))
                        {
                            TR_TABLE = BASE._LiveStockDBOps.GetTransactions("'" + model.xID + "'", BASE._open_Year_ID);
                            if (TR_TABLE.Rows.Count > 0)
                            {
                                sale_qty = sale_qty + Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                            }
                        }

                        if (sale_qty != 0)
                        {
                            jsonParam.title = "Information...";
                            jsonParam.message = "Insurance detail Cannot be changed..! <br><br> This item has already been sold...!";
                            jsonParam.DialogResult = Common.DialogResult.Retry;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New.ToString() || model.Tag == Common_Lib.Common.Navigation_Mode._Edit.ToString())
                {

                    if (model.Rad_Insurance == 0)
                    {
                        if (model.Look_InsList.Length <= 0 || model.Look_InsList.Trim().Length <= 0)
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "Insurance Company Not Selected...!";
                            jsonParam.focusid = "Look_InsList";
                            jsonParam.DialogResult = Common.DialogResult.None;
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_PolicyNo.Trim().Length == 0)
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "Policy No. cannot be Blank...!";
                            jsonParam.focusid = "Txt_PolicyNo";
                            jsonParam.DialogResult = Common.DialogResult.None;
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_I_Date.ToString()) == false)
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "Date Incorrect/Blank...!";
                            jsonParam.focusid = "Txt_I_Date";
                            jsonParam.DialogResult = Common.DialogResult.None;
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_I_Date.ToString()) == true)
                        {
                            if ((model.Txt_I_Date <= DateTime.Now) && (!(BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Edit_Data))))
                            {
                                jsonParam.title = "Incorrect Information . . .";
                                jsonParam.message = "Date must be higher than Today's...!";
                                jsonParam.focusid = "Txt_I_Date";
                                jsonParam.DialogResult = Common.DialogResult.None;
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.refreshgrid = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.Txt_InsAmt < 0)
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "Amount cannot be Negative...!";
                            jsonParam.focusid = "Txt_InsAmt";
                            jsonParam.DialogResult = Common.DialogResult.None;
                            jsonParam.result = false;
                            jsonParam.closeform = false;
                            jsonParam.refreshgrid = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                //Update Process
                Common_Lib.RealTimeService.Parameter_Update_Vehicles UpParam = new Common_Lib.RealTimeService.Parameter_Update_Vehicles();
                if (model.Look_InsList.ToString().Length > 0)
                {
                    UpParam.Insurance_ID = model.Look_InsList;
                }
                UpParam.Ins_Policy_No = model.Txt_PolicyNo;
                if (IsDate(model.Txt_I_Date.ToString()))
                {
                    UpParam.Ins_Expiry_Date = Convert.ToDateTime(model.Txt_I_Date).ToString(BASE._Server_Date_Format_Long);
                }
                UpParam.Rec_ID = model.xID;
                if (BASE._VehicleDBOps.UpdateInsuranceDetail(UpParam))
                {
                    jsonParam.title = "Success";
                    jsonParam.message = Common_Lib.Messages.UpdateSuccess;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.title = "Error!!";
                    jsonParam.message = Common_Lib.Messages.SomeError;
                    jsonParam.result = false;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                jsonParam.title = "Error!!";
                jsonParam.message = e.Message;
                jsonParam.result = false;
                jsonParam.closeform = true;
                jsonParam.refreshgrid = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        // Details: Below function i.e "Check_Frm_Vehicles_Window_Profile" is equivalent to DataNavigation() of Window's Application
        public ActionResult DataNavigation(string ActionMethod = null, string xID = null, bool MultiUserConfirmation = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (xID == null || xID == "")
                {
                    jsonParam.message = "Please Select one row";
                    jsonParam.title = "Alert ";
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }

                string xTemp_ID = xID;
                var VehicleSelectedRowData = Vehicles_ExportData.Where(x => x.ID == xTemp_ID).FirstOrDefault();
                string YEAR_ID = VehicleSelectedRowData.YEAR_ID;
                string Ownership = VehicleSelectedRowData.Ownership;
                string xTr_ID = VehicleSelectedRowData.TR_ID;
                string xStatus = VehicleSelectedRowData.Action_Status;
                int? xOpenActions = VehicleSelectedRowData.Open_Actions;
                string xType = VehicleSelectedRowData.Entry_Type;
                object xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_ID);
                DateTime xEdit_Date = VehicleSelectedRowData.Edit_Date;
                string _xSPID;
                var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                bool AllowUser = false;
                object MaxValue = 0;
                MaxValue = BASE._VehicleDBOps.GetStatus(xTemp_ID);
                string multiUserMsg = "";

                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod == "Locked" || ActionMethod == "Unlocked" || ActionMethod == "PRINT-LIST" || ActionMethod == "CHANGE-INSURANCE")
                    {
                        if (MultiUserConfirmation == false)
                        {
                            DataTable d1 = BASE._VehicleDBOps.GetRecord(xTemp_ID);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (d1.Rows.Count == 0)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle");
                                jsonParam.title = "Record Changed / Removed in Background!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            DateTime RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                            if (RecEdit_Date != xEdit_Date)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                #region <--Edit Button Click Restrictions/Checks-->

                if (ActionMethod == "Edit")
                {
                    if (MultiUserConfirmation == false)
                    {
                        bool? IsVehicleCarriedFwd = BASE._VehicleDBOps.IsVehicleCarriedForward(xTemp_ID, Convert.ToInt32(YEAR_ID));
                        if (IsVehicleCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsVehicleCarriedFwd == true)
                        {
                            // 'Restricted in case of Unsplit only 
                            if (BASE._prev_Unaudited_YearID != 0)
                            {
                                if (Ownership.ToUpper() == "FREE USE")
                                {
                                    jsonParam.message = "Entry Cannot be Edited..! <br><br> This entry has been carried forward from previous year(s).<br>Updation(Partial) can be done only after finalization of previous year accounts....! ";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    string Msg = "";
                                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Edit_Data))
                                    {
                                        Msg = " Updation(Partial) can be done only after finalization of previous year accounts";

                                    }
                                    jsonParam.message = "Entry Cannot be Edited..! <br><br> This entry has been carried forward from previous year(s)." + Msg + "....! ";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (Ownership.ToUpper() != "FREE USE")
                            {
                                //'Restricted in case of centre users in coth split/unsplit
                                if (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Edit_Data))
                                {
                                    jsonParam.message = "Entry Cannot be Edited..! <br><br> This entry has been carried forward from previous year(s)....!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        //'Bug #5339 fix
                        if (BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Profile_Vehicles, 0, xTemp_ID, false).Rows.Count > 0)
                        {
                            jsonParam.message = "Entry cannot be Edited...!<br><br>This Vehicle has already been transferred to another centre.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // '#5341 fix
                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                        if (SaleRecord != null)
                        {
                            if (SaleRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Entry Cannot be Edited..!<br><br>This item has already been sold...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (xTr_ID.Trim().Length > 0)
                        {
                            jsonParam.message = "Entry Cannot be Edited..!<br><br>This Entry Is Managed by Voucher Entry...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found/Changed In Background...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (value != Convert.ToInt32(MaxValue))
                    {
                        if (Convert.ToInt32(MaxValue) == (int)(Common_Lib.Common.Record_Status._Locked))
                        {
                            multiUserMsg = "The Record has been locked in the background by another user.";
                        }
                        else if (Convert.ToInt32(MaxValue) == (int)(Common_Lib.Common.Record_Status._Completed))
                        {
                            multiUserMsg = " The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (MultiUserConfirmation == false)
                        {
                            if (AllowUser)
                            {
                                jsonParam.message = multiUserMsg + "</br> </br> Do you want to continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Convert.ToInt32(MaxValue) == (int)(Common_Lib.Common.Record_Status._Locked))
                    {

                        jsonParam.message = "Locked Entry cannot be  Edited/Deleted...!" + multiUserMsg + "<br> <br> Note: <br> ------- <br> Drop your Request to Madhuban for Unlock this Entry,<br> If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    int RecStatus = Convert.ToInt32(MaxValue);
                    Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                    param.CrossRefId = xTemp_ID;
                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                    param.NextUnauditedYearID = BASE._next_Unaudited_YearID; //'#5341 fix
                    MaxValue = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles).Rows.Count;
                    if (Convert.ToInt32(MaxValue) > 0)
                    {
                        jsonParam.message = "Entry Cannot be Edited..!<br><br>There are journal voucher references posted against it...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //'unconfirmed vehicles
                    if (RecStatus == (int)(Common_Lib.Common.Record_Status._Deleted))
                    {
                        _xSPID = VehicleSelectedRowData.AL_LOC_AL_ID;
                    }
                    else
                    {
                        _xSPID = null;
                    }

                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                        _xSPID
                    }, JsonRequestBehavior.AllowGet);
                }

                #endregion //<--Edit Button Click Restrictions-->

                #region <--Delete Button Click Restrictions/Checks-->

                if (ActionMethod == "Delete")
                {
                    if (MultiUserConfirmation == false)
                    {
                        bool? IsVehicleCarriedFwd = BASE._VehicleDBOps.IsVehicleCarriedForward(xTemp_ID, Convert.ToInt32(YEAR_ID));
                        if (IsVehicleCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // 'Bug #5645 Deletion barred for all users and all types in case of unsplit only 
                        if (IsVehicleCarriedFwd == true)
                        {
                            if (BASE._prev_Unaudited_YearID != 0)
                            {
                                string Msg = "";
                                if (Ownership.ToString().ToUpper() == "FREE USE")
                                {
                                    Msg = "Deletion can be done only after finalization of previous year accounts.";

                                }
                                jsonParam.message = "Entry Cannot be Deleted..!</br> </br> This entry has been carried forward from previous year(s)...! " + Msg;
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (Ownership.ToString().ToUpper() != "FREE USE")
                                {
                                    jsonParam.message = "Entry Cannot be Deleted..!</br></br>This entry has been carried forward from previous year(s)...! Only Free Use Vehicles can be deleted.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new

                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        // 'Bug #5339 fix
                        if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, 0, xTemp_ID, false).Rows.Count > 0)
                        {
                            jsonParam.message = "Entry cannot be Deleted...!</br></br>This Vehicle has already been transferred to another centre.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new

                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (xOpenActions > 0)
                        {
                            jsonParam.message = "Entry Cannot be Deleted..!</br></br>There are open actions / queries posted against it...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new

                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // '#5341 fix
                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                        if (SaleRecord != null)
                        {
                            if (SaleRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Entry Cannot be Deleted..!</br></br>This item has already been sold...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new

                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (xTr_ID.Trim().Length > 0)
                        {
                            jsonParam.message = "Entry Cannot be Deleted..!</br></br>This Entry Managed by Voucher Entry...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new

                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found/Changed In Background...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new

                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (value != Convert.ToInt32(MaxValue))
                    {
                        if (Convert.ToInt32(MaxValue) == (int)(Common_Lib.Common.Record_Status._Locked))
                        {
                            multiUserMsg = "The Record has been locked in the background by another user.";
                        }
                        else if (Convert.ToInt32(MaxValue) == (int)(Common_Lib.Common.Record_Status._Completed))
                        {
                            multiUserMsg = "The Record has been unlocked in the background by another user.";
                            AllowUser = true;
                        }
                        else
                        {
                            multiUserMsg = "Record Status has been changed in the background by another user";
                            AllowUser = true;
                        }
                        if (MultiUserConfirmation == false)
                        {
                            if (AllowUser)
                            {
                                jsonParam.message = multiUserMsg + "</br> </br> Do you want to continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry cannot be Edited/Deleted...!" + multiUserMsg + "</br> </br> Note:</br> ------- </br> Drop your Request to Madhuban for Unlock this Entry,</br> If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                    param.CrossRefId = xTemp_ID;
                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                    param.NextUnauditedYearID = BASE._next_Unaudited_YearID; //'#5341 fix
                    MaxValue = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles).Rows.Count;

                    if (Convert.ToInt32(MaxValue) > 0)
                    {
                        jsonParam.message = "Entry Cannot be Deleted..!</br></br>There are journal voucher references posted against it...!";
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
                    }, JsonRequestBehavior.AllowGet);
                }

                #endregion // <--Delete Button Click Restrictions/Checks-->

                #region <--View Button Click Restrictions/Checks-->

                if (ActionMethod == "View")
                {
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }

                #endregion // <--View Button Click Restrictions/Checks-->

                #region <--Change Insurance Button Click Restrictions/Checks-->
                if (ActionMethod == "CHANGE-INSURANCE")
                {
                    bool? IsVehicleCarriedFwd = BASE._VehicleDBOps.IsVehicleCarriedForward(xTemp_ID, Convert.ToInt32(YEAR_ID));
                    if (IsVehicleCarriedFwd == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam, }, JsonRequestBehavior.AllowGet);
                    }

                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                    if (SaleRecord != null)
                    {
                        if (SaleRecord.Rows.Count > 0)
                        {
                            jsonParam.message = "Insurance detail Cannot be changed..! </br> </br> This item has already been sold...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam, }, JsonRequestBehavior.AllowGet);
                        }
                    }

                                            
                    if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, 0, xTemp_ID, false).Rows.Count > 0)
                    {
                        jsonParam.message = "Insurance detail Cannot be changed..!<br/><br/> This Vehicle has already been transferred to another centre.";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam, }, JsonRequestBehavior.AllowGet);
                    }
                    string xTemp_Item = VehicleSelectedRowData.ITEM_NAME;
                    string xTemp_Make = VehicleSelectedRowData.MAKE;
                    string xTemp_Model = VehicleSelectedRowData.Model;
                    string xTemp_RegNo = VehicleSelectedRowData.VI_REG_NO;
                    string xTemp_InsID = VehicleSelectedRowData.INSURANCE_ID;
                    string xTemp_P_No = VehicleSelectedRowData.VI_INS_POLICY_NO;                    
                    DateTime? xTemp_E_Dt = null;
                    // check for  Null
                    if (IsDate(VehicleSelectedRowData.Expiry_Date.ToString()) == true)
                    {
                        xTemp_E_Dt = VehicleSelectedRowData.Expiry_Date;
                    }

                    jsonParam.result = true;
                    jsonParam.popup_title = "Change ~ Vehicle Insurance Detail";
                    jsonParam.popup_form_name = "Frm_Change_Insurance_Window";
                    jsonParam.popup_form_path = "/Profile/Vehicle/Frm_Change_Insurance_Window/";
                    jsonParam.popup_querystring = "Tag=" + Url.Encode(Common_Lib.Common.Navigation_Mode._Edit.ToString());
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&xID=" + Url.Encode(xTemp_ID) + "&Rad_Insurance="+ Url.Encode("0") + "&xName=" + Url.Encode(xTemp_Item) +
                                                    "&xDesc=" + Url.Encode(xTemp_Make + ", " + xTemp_Model + ", " + xTemp_RegNo) + "&xIns_ID=" + Url.Encode(xTemp_InsID) 
                                                    + "&Txt_PolicyNo=" + Url.Encode(xTemp_P_No)+ "&screen=" + Url.Encode("Vehicle");
                    if(xTemp_E_Dt != null)
                    {
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_I_Date=" + Url.Encode(xTemp_E_Dt.ToString());
                    }
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Info_LastEditedOn=" + Url.Encode(VehicleSelectedRowData.Edit_Date.ToString());

                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                    #endregion

                #region <--Lock Button Click Restrictions/Checks-->

                if (ActionMethod == "Locked")
            {
                if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Lock_Unlock) == false)
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }

                if (MultiUserConfirmation == false)
                {
                    if (xType.ToUpper() == Voucher_Entry.ToUpper())
                    {
                        jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only . . . ! </br> </br> Please unselect Entry Created from Voucher ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                // Note: In Windows --> Dim Msg As String = "" but In Web --> We are using multiUserMsg variable instead of Msg
                if (value != Convert.ToInt32(MaxValue))
                {
                    multiUserMsg = "Record Status has been changed in the background by another user";

                    if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Completed)
                    {
                        AllowUser = true;
                    }
                    if (MultiUserConfirmation == false)
                    {
                        if (AllowUser)
                        {
                            jsonParam.message = "Record has been Unlocked in the background by another user </br> </br> Do you want to continue...?";
                            jsonParam.title = "Confirmation...";
                            jsonParam.isconfirm = true;
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    multiUserMsg = "Information...";
                }
                if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Locked)
                {
                    jsonParam.message = "Already Locked Entries can't be Re-Locked...!</br></br> Please unselect already locked Entries ...!";
                    jsonParam.title = multiUserMsg;
                    jsonParam.refreshgrid = true;
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Incomplete)
                {
                    jsonParam.message = "Incomplete Entry can't be Locked...!</br></br> Please unselect incomplete Entry or ask Center to Complete it...!";
                    jsonParam.title = multiUserMsg;
                    jsonParam.refreshgrid = true;
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((Ownership.ToString().ToUpper() != "FREE USE") && (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Deleted))
                {
                    jsonParam.message = "Only free Use Vehicles can be Confirmed directly.</br></br>Accounting vehicles need to be confirmed via Gift Voucher . . . !";
                    jsonParam.title = multiUserMsg;
                    jsonParam.refreshgrid = true;
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (xRemarks != null && !Convert.IsDBNull(xRemarks))
                {
                    if (Convert.ToInt32(MaxValue) > 0)
                    {
                        jsonParam.message = "Entries with pending queries can't be Locked...!</br> </br> Please unselect the Entry...!";
                        jsonParam.title = "Information...";
                        jsonParam.refreshgrid = true;
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (!BASE._VehicleDBOps.MarkAsLocked(xTemp_ID))
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }

                jsonParam.message = Messages.LockedSuccess(1);
                jsonParam.title = "Locked...";
                jsonParam.refreshgrid = true;
                    jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                }, JsonRequestBehavior.AllowGet);

            }

            #endregion // <--Lock Button Click Restrictions/Checks-->

                #region <--Unlock Button Click Restrictions/Checks-->

                if (ActionMethod == "Unlocked")
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Lock_Unlock) == false)
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (MultiUserConfirmation == false)
                    {
                        if (xType.ToUpper() == Voucher_Entry.ToUpper())
                        {
                            jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only . . . ! <br><br> Please unselect Entry Created from Voucher ...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    // Note: In Windows --> Dim Msg As String = "" but In Web --> We are using multiUserMsg variable instead of Msg

                    if (value != Convert.ToInt32(MaxValue))
                    {
                        multiUserMsg = "Record Status has been changed in the background by another user";

                        if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Locked)
                        {
                            AllowUser = true;
                        }
                        if (MultiUserConfirmation == false)
                        {
                            if (AllowUser)
                            {
                                jsonParam.message = "Record has been Locked in the background by another user </br> </br> Do you want to continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.isconfirm = true;
                                jsonParam.refreshgrid = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        multiUserMsg = "Information...";
                    }
                    if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Completed)
                    {
                        jsonParam.message = "Already Unlocked Entry can't be Re-Unlocked...!<br><br>Please unselect already unlocked Entry ...!";
                        jsonParam.title = multiUserMsg;
                        jsonParam.refreshgrid = true;
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(MaxValue) == (int)Common_Lib.Common.Record_Status._Incomplete)
                    {
                        jsonParam.message = "Incomplete Entry can't be Unlocked...!<br><br> Please unselect incomplete Entry or ask Center to Complete it...!";
                        jsonParam.title = multiUserMsg;
                        jsonParam.refreshgrid = true;
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!BASE._VehicleDBOps.MarkAsComplete(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Messages.UnlockedSuccess(1);
                    jsonParam.title = "UnLocked...";
                    jsonParam.refreshgrid = true;
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);

                }

                #endregion // <--Unlock Button Click Restrictions/Checks-->

                #region <--Remarks [View Audit Actions] Click Restrictions/Checks-->

                if (ActionMethod == "Remarks")
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.View_Remarks) == false)
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xType.ToUpper() == Voucher_Entry.ToUpper())
                    {
                        jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only . . . ! </br> </br> Please unselect Entry Created from Voucher ...!";
                        jsonParam.title = "Information...";
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);

                }
                #endregion // <--Remarks [View Audit Actions] Click Restrictions/Checks-->

                #region <--AddRemarks [New Audit Actions] Click Restrictions/Checks-->

                if (ActionMethod == "AddRemarks")
                {
                    if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Manage_Remarks) == false)
                    {
                        jsonParam.message = "Not Allowed!!";
                        jsonParam.title = "No Rights";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xType.ToUpper() == Voucher_Entry.ToUpper())
                    {
                        jsonParam.message = "Entries Created from Vouchers can be Audited from Vouchers Only . . . ! </br> </br> Please unselect Entry Created from Voucher ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xStatus.ToUpper() != "LOCKED")
                    {
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = "Queries Can't be added to Freezed Records...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                #endregion //  <--AddRemarks [New Audit Actions] Click Restrictions/Checks-->

                // Details: Below code is written because function should return in all cases & below code will only execute if Action Method is not received by this function.
                jsonParam.message = "Invalid ActionMethod in Code";
                jsonParam.title = "Information...";
                return Json(new
                {
                    jsonParam,
                }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Exception !!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // Details: Below function i.e "Frm_Vehicles_Window_Profile" is equivalent to SetDefault() of Window's Application
        public ActionResult Frm_Vehicles_Window_Profile(string ActionMethod = null, string xSPID = null, string xID = null, string Info_LastEditedOn = null, string Txt_VI_Amount = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                #region <--CheckingRights-->

                if (ActionMethod == "New" && CheckRights(BASE, ClientScreen.Profile_Vehicles, "Add") == false)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
                if (ActionMethod == "Edit" && CheckRights(BASE, ClientScreen.Profile_Vehicles, "Update") == false)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
                if (ActionMethod == "View" && CheckRights(BASE, ClientScreen.Profile_Vehicles, "View") == false)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
                if (ActionMethod == "Delete" && CheckRights(BASE, ClientScreen.Profile_Vehicles, "Delete") == false)
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }

                #endregion //<--Checking Rights-->

                Vehicle_user_rights();
                VehicleWindow model = new VehicleWindow();               
                var Navigation_Mode_tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                model.ActionMethod = Navigation_Mode_tag;
                model.xSPID = xSPID;
                model.xID_VPW = xID;
                model.Txt_VI_Amount_VPW = string.IsNullOrWhiteSpace(Txt_VI_Amount)?(double?)null:Convert.ToDouble(Txt_VI_Amount);
                model.TempActionMethod = ActionMethod;
                model.TitleX_VPW = " Vehicle";
                model.SubTitleX_VPW = "As on " + (Convert.ToDateTime(BASE._open_Year_Sdt).AddDays(-1)).ToString("dd MMMM, yyyy");
                model.TextEdit1_VPW = BASE._open_Ins_Name; // Hidden Text Box
                ViewBag.CompletedYearCount = BASE._Completed_Year_Count; //For Owner's SelectBox
                ViewBag.GoldSilverIsTBImportedCentre = BASE._GoldSilverDBOps.IsTBImportedCentre(); //For Owner's SelectBox

                model.Item_DD_Data_VPW = RefreshItemListDropdown();
                model.Owner_DD_Data_VPW = RefreshOwnerListDropdown();                
                model.Insurance_DD_Data_VPW = RefreshInsuranceListDropdown();                
                model.Location_DD_Data_VPW = RefreshLocationListDropdown();                
                model.VehicleMake_DD_Data_VPW = RefreshMakeListDropdown();                
                model.VehicleModel_DD_Data_VPW = new List<MISC_INFO>();

                if ((BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) && (BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.Auditor.ToUpper()))
                {
                    model.SuperUserAndAuditorCheck_VPW = "False";
                }
                
                if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
                {
                    #region <--Data_Binding()-->

                    //DateTime Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                    DataTable d1 = BASE._VehicleDBOps.GetRecord(xID);
                    if (d1 == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE.AllowMultiuser())
                    {
                        if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
                        {
                            string viewstr = "";
                            if (ActionMethod == "View")
                            {
                                viewstr = "View";
                            }
                            if (Convert.ToDateTime(Info_LastEditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle", viewstr);
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    model.YearID = (d1.Rows[0]["VI_COD_YEAR_ID"]).ToString();
                    model.Look_ItemList_VPW = (d1.Rows[0]["VI_ITEM_ID"]).ToString();
                    if(Convert.IsDBNull(d1.Rows[0]["VI_MAKE"]) == false)
                    {
                        model.Cmd_Make_VPW = (d1.Rows[0]["VI_MAKE"]).ToString();
                        model.VehicleModel_DD_Data_VPW = RefreshModelListDropdown(model.Cmd_Make_VPW);
                    }
                    if (Convert.IsDBNull(d1.Rows[0]["VI_MODEL"]) == false)
                    {
                        model.Cmd_Model_VPW = (d1.Rows[0]["VI_MODEL"]).ToString();
                    }

                    if (!Convert.IsDBNull(d1.Rows[0]["VI_REG_NO_PATTERN"]))
                    {
                        model.RAD_RegPattern_VPW = (d1.Rows[0]["VI_REG_NO_PATTERN"]).ToString();
                    }
                    else
                    {
                        model.RAD_RegPattern_VPW = "OTHER";
                    }
                    model.Txt_RegNo_VPW = (d1.Rows[0]["VI_REG_NO"]).ToString();

                    if (ActionMethod != "View")
                    {
                        if (!Convert.IsDBNull(d1.Rows[0]["VI_AMOUNT"]))
                        {
                            model.Txt_VI_Amount_VPW = Convert.ToDouble(d1.Rows[0]["VI_AMOUNT"]);
                        }
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["VI_REG_DATE"]))
                    {
                        model.Txt_RegDate_VPW = Convert.ToDateTime(d1.Rows[0]["VI_REG_DATE"]);
                    }
                    model.Cmd_Ownership_VPW = (d1.Rows[0]["VI_OWNERSHIP"]).ToString();
                    if (!Convert.IsDBNull(d1.Rows[0]["VI_OWNERSHIP_AB_ID"]))
                    {
                        model.Look_OwnList_VPW = (d1.Rows[0]["VI_OWNERSHIP_AB_ID"]).ToString();
                    }
                    if ((d1.Rows[0]["VI_DOC_RC_BOOK"]).ToString().ToUpper().Trim() == "YES") { model.Chk_RC_Book_VPW = true; } else { model.Chk_RC_Book_VPW = false; }
                    if ((d1.Rows[0]["VI_DOC_AFFIDAVIT"]).ToString().ToUpper().Trim() == "YES") { model.Chk_Affidavit_VPW = true; } else { model.Chk_Affidavit_VPW = false; }
                    if ((d1.Rows[0]["VI_DOC_WILL"]).ToString().ToUpper().Trim() == "YES") { model.Chk_Will_VPW = true; } else { model.Chk_Will_VPW = false; }
                    if ((d1.Rows[0]["VI_DOC_TRF_LETTER"]).ToString().ToUpper().Trim() == "YES") { model.Chk_Trf_Letter_VPW = true; } else { model.Chk_Trf_Letter_VPW = false; }
                    if ((d1.Rows[0]["VI_DOC_FU_LETTER"]).ToString().ToUpper().Trim() == "YES") { model.Chk_FU_Letter_VPW = true; } else { model.Chk_FU_Letter_VPW = false; }
                    if ((d1.Rows[0]["VI_DOC_OTHERS"]).ToString().ToUpper().Trim() == "YES") { model.Chk_OtherDoc_VPW = true; } else { model.Chk_OtherDoc_VPW = false; }

                    model.Txt_OtherDoc_VPW = (d1.Rows[0]["VI_DOC_NAME"]).ToString();
                    model.Look_InsList_VPW = (d1.Rows[0]["VI_INSURANCE_ID"]).ToString();
                    model.Txt_PolicyNo_VPW = (d1.Rows[0]["VI_INS_POLICY_NO"]).ToString();
                    if (!Convert.IsDBNull(d1.Rows[0]["VI_INS_EXPIRY_DATE"]))
                    {
                        model.Txt_E_Date_VPW = Convert.ToDateTime(d1.Rows[0]["VI_INS_EXPIRY_DATE"]);
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["VI_LOC_AL_ID"]))
                    {
                        model.Look_LocList_VPW = (d1.Rows[0]["VI_LOC_AL_ID"]).ToString();
                    }
                    model.Txt_Others_VPW = (d1.Rows[0]["VI_OTHER_DETAIL"]).ToString();

                    #region <--Handling TextBox Values-->

                    model.Txt_RegNo_VPW = model.Txt_RegNo_VPW.HandleEscapeCharacters();
                    model.Txt_OtherDoc_VPW = model.Txt_OtherDoc_VPW.HandleEscapeCharacters();
                    model.Txt_PolicyNo_VPW = model.Txt_PolicyNo_VPW.HandleEscapeCharacters();
                    model.Txt_Others_VPW = model.Txt_Others_VPW.HandleEscapeCharacters();

                    #endregion // <--Handling TextBox Values-->

                    #endregion //<--Data_Binding()-->

                }
                if (ActionMethod == "Edit")
                {
                    model.IsVehicleCarriedFwd_VPW = BASE._VehicleDBOps.IsVehicleCarriedForward(xID, Convert.ToInt32(model.YearID));
                    if (model.IsVehicleCarriedFwd_VPW == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                return View(model);
            }

            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Exception !!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // Details: Below function i.e "[HttpPost] Frm_Vehicles_Window_Profile" is equivalent to Save & Delete function of Window's Application

        [HttpPost]
        public ActionResult Frm_Vehicles_Window_Profile(VehicleWindow model)
        {
            string Chk_RC_Book;
            string Chk_Affidavit;
            string Chk_Will;
            string Chk_Trf_Letter;
            string Chk_FU_Letter;
            string Chk_OtherDoc;

            if (model.Chk_RC_Book_VPW) { Chk_RC_Book = "YES"; } else { Chk_RC_Book = "NO"; }
            if (model.Chk_Affidavit_VPW) { Chk_Affidavit = "YES"; } else { Chk_Affidavit = "NO"; }
            if (model.Chk_Will_VPW) { Chk_Will = "YES"; } else { Chk_Will = "NO"; }
            if (model.Chk_Trf_Letter_VPW) { Chk_Trf_Letter = "YES"; } else { Chk_Trf_Letter = "NO"; }
            if (model.Chk_FU_Letter_VPW) { Chk_FU_Letter = "YES"; } else { Chk_FU_Letter = "NO"; }
            if (model.Chk_OtherDoc_VPW) { Chk_OtherDoc = "YES"; } else { Chk_OtherDoc = "NO"; }

            if(string.IsNullOrWhiteSpace(model.Txt_RegNo_VPW) == false)
            {
                model.Txt_RegNo_VPW = model.Txt_RegNo_VPW.Trim().ToUpper();
                // THIS IS WRITTEN AS IN MASKING OF REGISTRATION NUMBER LOWER CASE ALPHABETS ARE ALLOWED BUT FOR DB WE NEED UPPER CASE.
            }

            Return_Json_Param jsonParam = new Return_Json_Param();
            string xid;
            try
            {
                #region <--Restrictions: Add/Edit/Delete-->

                if (BASE.AllowMultiuser())
                {
                    if (model.TempActionMethod == "New" || model.TempActionMethod == "Edit" || model.TempActionMethod == "Delete")
                    {
                        //Gets PropertyID for the selected location
                        var LB_ID = BASE._AssetLocDBOps.GetPropertyID(model.Look_LocList_VPW);
                        if (LB_ID == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        // Transfer of location property check #Ref AM14
                        if (!Convert.IsDBNull(Convert.ToString(LB_ID)))
                        {

                            if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_LandAndBuilding, 0, Convert.ToString(LB_ID), false).Rows.Count > 0)
                            {
                                jsonParam.message = "<p style='text-align:center'> <b>Entry cannot be Added/Edited/Deleted ... !!!</b></p> <p style='text-align:center'>This vehicle location Property has already been transferred to another centre</p>";
                                jsonParam.title = "Information...";
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);

                            }
                        }


                        if (model.TempActionMethod == "Edit" || model.TempActionMethod == "Delete")
                        {
                            DataTable vehicle_DbOps = BASE._VehicleDBOps.GetRecord(model.xID_VPW);
                            if (vehicle_DbOps == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if (vehicle_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle");
                                jsonParam.title = "Record Already Changed!!";                              
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if (model.LastEditedOn != Convert.ToDateTime(vehicle_DbOps.Rows[0]["REC_EDIT_ON"]))
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            DataTable TR_TABLE = null;
                            if (BASE._prev_Unaudited_YearID != 0)
                            {
                                TR_TABLE = BASE._GoldSilverDBOps.GetTransactions("'" + model.xID_VPW + "'", Convert.ToString(BASE._prev_Unaudited_YearID));
                            }
                            else
                            {
                                TR_TABLE = BASE._GoldSilverDBOps.GetTransactions("'" + model.xID_VPW + "'", Convert.ToString(BASE._open_Year_ID));
                            }

                            int sale_qty = 0;
                            if (TR_TABLE.Rows.Count > 0)
                            {
                                sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Weight"]);
                            }

                            if (BASE._prev_Unaudited_YearID != 0)
                            {
                                TR_TABLE = BASE._GoldSilverDBOps.GetTransactions("'" + model.xID_VPW + "'", Convert.ToString(BASE._open_Year_ID));

                                if (TR_TABLE.Rows.Count > 0)
                                {
                                    sale_qty = sale_qty + Convert.ToInt32(TR_TABLE.Rows[0]["Sale Weight"]);
                                }
                            }
                            // Sale of asset Dependency Check #Ref AO14
                            if (sale_qty != 0)
                            {
                                jsonParam.message = "<p style='text-align:center'> <b>E n t r y   C a n n  o t   E d i t e d   /   D e l e t e d . . !</b></br> This item has already been sold...!</p>";
                                jsonParam.title = "Information...";
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            object MaxValue = 0;
                            MaxValue = BASE._VehicleDBOps.GetStatus(model.xID_VPW);
                            if (MaxValue == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Vehicle");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new

                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // Journal references dependency check #Ref AP14
                            Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = model.xID_VPW;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;  // #5341 fix
                            MaxValue = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles).Rows.Count;
                            if (Convert.ToInt32(MaxValue) > 0) // if there are any journal adjustments made against the entry
                            {
                                jsonParam.message = "<p style='text-align:center'> <b>E n t r y   C a n n  o t   E d i t e d   /   D e l e t e d . . !</b></br> There are journal voucher references posted against it...!</p>";
                                jsonParam.title = "Information...";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // check for vehicle transfer #Ref AM14
                            if (BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, 0, model.xID_VPW, false).Rows.Count > 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("This Vehicle has already been transferred to another centre.");
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            // sale of location property check #Ref AO14
                            if (!Convert.IsDBNull(LB_ID))
                            {
                                if (BASE._prev_Unaudited_YearID != 0)
                                {
                                    TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + Convert.ToString(LB_ID) + "'", BASE._prev_Unaudited_YearID);
                                }
                                else
                                {
                                    TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + Convert.ToString(LB_ID) + "'", BASE._open_Year_ID);
                                }
                                if (TR_TABLE.Rows.Count > 0)
                                {
                                    sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                                }
                                if (BASE._prev_Unaudited_YearID != 0)
                                {
                                    TR_TABLE = BASE._L_B_DBOps.GetTransactions("'" + Convert.ToString(LB_ID) + "'", BASE._prev_Unaudited_YearID);
                                    if (TR_TABLE.Rows.Count > 0)
                                    {
                                        sale_qty = sale_qty + Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                                    }
                                }
                                if (sale_qty != 0) // if location property has already been sold
                                {
                                    jsonParam.message = Messages.DependencyChanged("This vehcile location property has already been sold...!");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.closeform = true;
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        if (model.TempActionMethod == "Delete")
                        {
                            object openActions = BASE._Action_Items_DBOps.GetOpenActions(model.xID_VPW, "VEHICLES_INFO");
                            if (openActions == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (Convert.ToInt32(openActions) > 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("There are open actions / queries posted against it...!");
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }

                if (model.TempActionMethod == "New" || model.TempActionMethod == "Edit")
                {
                    if (model.Look_ItemList_VPW == null)
                    {
                        jsonParam.message = "I t e m   N a m e   N o t   S e l e c t e d . . . !";
                        jsonParam.title = "Incomplete Information . . .";                    
                        jsonParam.focusid = "Look_ItemList_VPW";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //'Bug #5647 Fix
                    if (model.Cmd_Ownership_VPW == "FREE USE")
                    {
                        if (model.Txt_VI_Amount_VPW != 0 && model.Txt_VI_Amount_VPW != null)
                        {
                            jsonParam.message = "I n v a l i d   O p e n i n g   V a l u e . . . ! </br> Free Use Vehicles Can not have Opening Values..";
                            jsonParam.title = "Incomplete Information . . .";                         
                            jsonParam.focusid = "Txt_VI_Amount_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (!(model.Look_ItemList_DisplayName_VPW.ToString().ToUpper().Trim() == "YO BIKE" || model.Look_ItemList_DisplayName_VPW.ToString().ToUpper().Trim() == "CYCLE" || model.Look_ItemList_DisplayName_VPW.ToString().ToUpper().Trim() == "GOLF CART"))
                    {
                        if (model.Txt_RegNo_VPW.ToString().ToUpper().Trim().Length == 0)
                        {
                            jsonParam.message = "R e g i s t r a t i o n   N o .   c a n n o t   b e   B l a n k . . . !";
                            jsonParam.title = "Incomplete Information . . .";                      
                            jsonParam.focusid = "Txt_RegNo_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_RegDate_VPW == null && (model.xSPID == null || model.xSPID.Length == 0)) // Details: model.xSPID is checked for null, as in C# it gives null reference exception
                        {
                            jsonParam.message = "D a t e   I n c o r r e c t   /   B l a n k . . . !";
                            jsonParam.title = "Incomplete Information . . .";                        
                            jsonParam.focusid = "Txt_RegDate_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        bool? IsVehicleCarriedFwd = BASE._VehicleDBOps.IsVehicleCarriedForward(model.xID_VPW, Convert.ToInt32(model.YearID));
                        if (IsVehicleCarriedFwd == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (!(IsVehicleCarriedFwd == true))
                        {
                            if (model.Txt_RegDate_VPW != null && model.Cmd_Ownership_VPW != "FREE USE" && string.IsNullOrWhiteSpace(model.xSPID))
                            {
                                double diff = (Convert.ToDateTime(model.Txt_RegDate_VPW) - BASE._open_Year_Sdt).TotalDays;
                                if (diff >= 0)
                                {
                                    jsonParam.message = "D a t e   m u s t   b e   E a r l i e r   t h a n   S t a r t   F i n a n c i a l   Y e a r . . . !";
                                    jsonParam.title = "Incorrect Information . . .";                                  
                                    jsonParam.focusid = "Txt_RegDate_VPW";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                    }

                    if ((model.Cmd_Ownership_VPW).Trim().Length == 0)
                    {
                        jsonParam.message = "O w n e r s h i p   N o t   S e l e c t e d . . . !";
                        jsonParam.title = "Incomplete Information . . .";                      
                        jsonParam.focusid = "Cmd_Ownership_VPW";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Cmd_Ownership_VPW != "INSTITUTION")
                    {
                        if ((model.Look_OwnList_VPW).Trim().Length == 0)
                        {
                            jsonParam.message = "O w n e r   N a m e   N o t   S e l e c t e d . . . !";
                            jsonParam.title = "Incomplete Information . . .";                      
                            jsonParam.focusid = "Look_OwnList_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (!((model.Look_ItemList_DisplayName_VPW).ToString().ToUpper().Trim() == "YO BIKE" || (model.Look_ItemList_DisplayName_VPW).ToString().ToUpper().Trim() == "CYCLE" || (model.Look_ItemList_DisplayName_VPW).ToString().ToUpper().Trim() == "GOLF CART") && (model.xSPID == null || model.xSPID.Length == 0)) // Details: model.xSPID is checked for null, as in C# it gives null reference exception
                    {
                        if (model.Look_InsList_VPW.Trim().Length <= 0)
                        {
                            jsonParam.message = "I n s u r a n c e   C o m p a n y   N o t   S e l e c t e d . . . !";
                            jsonParam.title = "Incomplete Information . . .";                           
                            jsonParam.focusid = "Look_InsList_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_PolicyNo_VPW.Trim().Length == 0)
                        {
                            jsonParam.message = "P o l i c y   N o .   c a n n o t   b e   B l a n k . . . !";
                            jsonParam.title = "Incomplete Information . . .";                         
                            jsonParam.focusid = "Txt_PolicyNo_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_E_Date_VPW.ToString()) == false)
                        {
                            jsonParam.message = "D a t e   I n c o r r e c t   /   B l a n k . . . !";
                            jsonParam.title = "Incomplete Information . . .";                          
                            jsonParam.focusid = "Txt_E_Date_VPW";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_E_Date_VPW.ToString()) == true)
                        {
                            TimeSpan interval1 = (Convert.ToDateTime(model.Txt_E_Date_VPW) - Convert.ToDateTime(model.Txt_RegDate_VPW));
                            double diff1 = interval1.TotalDays;

                            TimeSpan interval2 = (Convert.ToDateTime(model.Txt_E_Date_VPW) - DateTime.Now);
                            double diff2 = interval2.TotalDays;
                            if (diff1 <= 0)
                            {
                                jsonParam.message = "Insurance Policy Expiry Date must be higher than First Date of Registration . . . !";
                                jsonParam.title = "Incorrect Information . . .";                              
                                jsonParam.focusid = "Txt_E_Date_VPW";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((diff2 <= 0) && (!BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Edit_Data)))
                            {
                                jsonParam.message = "Insurance Policy Expiry Date must be higher than Today's . . . !";
                                jsonParam.title = "Incorrect Information . . .";                              
                                jsonParam.focusid = "Txt_E_Date_VPW";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    if (model.Look_LocList_VPW.Trim().Length == 0)
                    {
                        jsonParam.message = "L o c a t i o n   N o t   S e l e c t e d . . . !";
                        jsonParam.title = "Incomplete Information . . .";                     
                        jsonParam.focusid = "Look_LocList_VPW";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                if (BASE.AllowMultiuser())
                {
                    if (model.TempActionMethod == "New" || model.TempActionMethod == "Edit")
                    {
                        if (string.IsNullOrWhiteSpace(model.Look_OwnList_VPW)==false)
                        {
                            // 'Owner(AddressBook) Dependency Check #Ref Z14

                            DateTime oldEditOn = model.oldEditOn_VPW;
                            DataTable d1 = BASE._VehicleDBOps.GetOwners_List(model.Look_OwnList_VPW);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            int cnt = d1.Rows.Count; //'A/D,E/D
                            if (cnt <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book(Owner)");
                                jsonParam.title = "Referred Record Already Deleted !!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else // 'Record has not been deleted
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0][4]);
                                if (oldEditOn != NewEditOn)   //'A/E,E/E
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book(Owner)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        // 'Location Dependency Check #Ref U14
                        DataTable isLocChanged = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, model.Look_LocList_VPW, null);
                        if (isLocChanged == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (isLocChanged.Rows.Count <= 0)
                        {
                            jsonParam.message = Messages.DependencyChanged("Asset Location");
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                #endregion // <--Restrictions: Add/Edit/Delete-->

                string Status_Action = "";

                if (model.TempActionMethod == "Delete" || string.IsNullOrWhiteSpace(model.xSPID) == false) { 
                    Status_Action = ((int)(Common_Lib.Common.Record_Status._Deleted)).ToString(); 
                } 
                else { 
                    Status_Action = ((int)(Common_Lib.Common.Record_Status._Completed)).ToString(); 
                }


                double? Amount = null;

                #region <--Save in Database for "New" mode-->

                if (model.TempActionMethod == "New")
                {                   

                    string _Owner_Free = null;

                    if (model.Cmd_Ownership_VPW == "INCHARGE" || model.Cmd_Ownership_VPW == "FREE USE")
                    {
                        _Owner_Free = model.Look_OwnList_VPW;
                    }

                    Amount = model.Txt_VI_Amount_VPW;

                    Common_Lib.RealTimeService.Parameter_Insert_Vehicles InParam = new Common_Lib.RealTimeService.Parameter_Insert_Vehicles();
                    InParam.ItemID = model.Look_ItemList_VPW;
                    InParam.Make = model.Cmd_Make_VPW;
                    InParam.Model = model.Cmd_Model_VPW;
                    InParam.Reg_No_Pattern = model.RAD_RegPattern_VPW;
                    InParam.Reg_No = model.Txt_RegNo_VPW;
                    if (IsDate((model.Txt_RegDate_VPW).ToString())) { InParam.RegDate = Convert.ToDateTime(model.Txt_RegDate_VPW).ToString(BASE._Server_Date_Format_Long); }
                    InParam.Amount = Convert.ToDouble(Amount);
                    InParam.Ownership = model.Cmd_Ownership_VPW;
                    InParam.Ownership_AB_ID = _Owner_Free;
                    InParam.Doc_RC_Book = Chk_RC_Book;
                    InParam.Doc_Affidavit = Chk_Affidavit;
                    InParam.Doc_Will = Chk_Will;
                    InParam.Doc_TRF_Letter = Chk_Trf_Letter;
                    InParam.DOC_FU_Letter = Chk_FU_Letter;
                    InParam.Doc_Is_Others = Chk_OtherDoc;
                    InParam.Doc_Others_Name = model.Txt_OtherDoc_VPW;
                    if (model.Look_InsList_VPW != null) { if (model.Look_InsList_VPW.Length > 0) { InParam.Insurance_ID = model.Look_InsList_VPW; } }
                    InParam.Ins_Policy_No = model.Txt_PolicyNo_VPW;
                    if (IsDate((model.Txt_E_Date_VPW).ToString())) { InParam.Ins_Expiry_Date = Convert.ToDateTime(model.Txt_E_Date_VPW).ToString(BASE._Server_Date_Format_Long); }
                    InParam.Location_ID = model.Look_LocList_VPW;
                    InParam.Other_Details = model.Txt_Others_VPW;
                    InParam.Status_Action = Status_Action;
                    InParam.Rec_ID = System.Guid.NewGuid().ToString();
                    if (BASE._VehicleDBOps.Insert(InParam))
                    {
                        xid = InParam.Rec_ID;
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.TitleX_VPW;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            xid
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

                #endregion //<--Save in Database for "New" mode-->

                #region <--Save in Database for "Edit" mode-->

                if (model.TempActionMethod == "Edit")
                {
                    // Details: xDate & iDate variables are created and assigned as per window's code.
                    // Txt_RegDate_Tag & Txt_E_Date_Tag variables are also created as per window's code.
                    // It is checked that both these variables are not used anywhere.
                    DateTime xDate;
                    if (IsDate((model.Txt_RegDate_VPW).ToString()))
                    {
                        xDate = new DateTime(Convert.ToDateTime(model.Txt_RegDate_VPW).Year, Convert.ToDateTime(model.Txt_RegDate_VPW).Month, Convert.ToDateTime(model.Txt_RegDate_VPW).Day);
                        string Txt_RegDate_Tag = "#" + xDate.ToString(BASE._Date_Format_Short) + "#";

                    }
                    else
                    {
                        string Txt_RegDate_Tag = " NULL ";
                    }
                    DateTime iDate;
                    if (IsDate((model.Txt_E_Date_VPW).ToString()))
                    {
                        iDate = new DateTime(Convert.ToDateTime(model.Txt_E_Date_VPW).Year, Convert.ToDateTime(model.Txt_E_Date_VPW).Month, Convert.ToDateTime(model.Txt_E_Date_VPW).Day);
                        string Txt_E_Date_Tag = "#" + iDate.ToString(BASE._Date_Format_Short) + "#";
                    }
                    else
                    {
                        string Txt_E_Date_Tag = " NULL ";
                    }

                    string _Owner_Free = null;
                    if (model.Cmd_Ownership_VPW == "INCHARGE") { _Owner_Free = model.Look_OwnList_VPW; }
                    if (model.Cmd_Ownership_VPW == "FREE USE") { _Owner_Free = model.Look_OwnList_VPW; }

                    Amount = model.Txt_VI_Amount_VPW;
                    Common_Lib.RealTimeService.Parameter_Update_Vehicles UpParam = new Common_Lib.RealTimeService.Parameter_Update_Vehicles();
                    UpParam.ItemID = model.Look_ItemList_VPW;
                    UpParam.Make = model.Cmd_Make_VPW;
                    UpParam.Model = model.Cmd_Model_VPW;
                    UpParam.Reg_No_Pattern = model.RAD_RegPattern_VPW;
                    UpParam.Reg_No = model.Txt_RegNo_VPW;
                    if (IsDate((model.Txt_RegDate_VPW).ToString())) { UpParam.RegDate = Convert.ToDateTime(model.Txt_RegDate_VPW).ToString(BASE._Server_Date_Format_Long); }
                    UpParam.Amount = Convert.ToDouble(Amount);
                    UpParam.Ownership = model.Cmd_Ownership_VPW;
                    UpParam.Ownership_AB_ID = _Owner_Free;
                    UpParam.Doc_RC_Book = Chk_RC_Book;
                    UpParam.Doc_Affidavit = Chk_Affidavit;
                    UpParam.Doc_Will = Chk_Will;
                    UpParam.Doc_TRF_Letter = Chk_Trf_Letter;
                    UpParam.DOC_FU_Letter = Chk_FU_Letter;
                    UpParam.Doc_Is_Others = Chk_OtherDoc;
                    UpParam.Doc_Others_Name = model.Txt_OtherDoc_VPW;
                    if (model.Look_InsList_VPW != null) { if (model.Look_InsList_VPW.Length > 0) { UpParam.Insurance_ID = model.Look_InsList_VPW; } }
                    UpParam.Ins_Policy_No = model.Txt_PolicyNo_VPW;
                    if (IsDate((model.Txt_E_Date_VPW).ToString())) { UpParam.Ins_Expiry_Date = Convert.ToDateTime(model.Txt_E_Date_VPW).ToString(BASE._Server_Date_Format_Long); }
                    UpParam.Location_ID = model.Look_LocList_VPW;
                    UpParam.Other_Details = model.Txt_Others_VPW;
                    UpParam.Rec_ID = model.xID_VPW;

                    if (BASE._VehicleDBOps.Update(UpParam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = model.TitleX_VPW;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            xid=model.xID_VPW
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

                #endregion //<--Save in Database for "Edit" mode-->

                #region <--Delete from Database for "Delete" mode-->

                if (model.TempActionMethod == "Delete")
                {
                    if (BASE._VehicleDBOps.Delete(model.xID_VPW))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = model.TitleX_VPW;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.result = true;
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

                #endregion  // <--Delete from Database for "Delete" mode-->



            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Exception";
                jsonParam.closeform = false;
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }

            // Details: Below code is written because function should return in all cases & below code will only execute if Action Method is not received by this function.
            jsonParam.message = "Invalid ActionMethod in Code";
            jsonParam.title = "Information...";
            jsonParam.result = false;
            return Json(new
            {
                jsonParam,
            }, JsonRequestBehavior.AllowGet);
        }

        #region <--Dropdowns: Vehicle Window-->

        public List<Item_INFO_Vehicle> RefreshItemListDropdown()
        {
            DataTable d1 = BASE._VehicleDBOps.GetOpeningProfile_Vehicles("ID", "Name") as DataTable;
            return DatatableToModel.DataTabletoItem_INFO_Vehicle(d1);
        }
        
        public List<ASSET_LOCATION_INFO> RefreshLocationListDropdown()
        {
            DataTable d1 = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, null, null) as DataTable;
            return DatatableToModel.DataTabletoASSET_LOCATION_INFO(d1);
        }

        public ActionResult RefreshLocationListDropdownJson()
        {
            return Content(JsonConvert.SerializeObject(RefreshLocationListDropdown()), "application/json");//Returned in JSON So that it can be stored in Array DataStore. Complex DataTypes can't be implicitly Converted
        }
        public List<MISC_INFO> RefreshMakeListDropdown()
        {
            DataTable make_Table = BASE._VehicleDBOps.GetVehicle_Makes() as DataTable;
            return DatatableToModel.DataTabletoMISC_INFO(make_Table);
        }
        public List<MISC_INFO> RefreshModelListDropdown(string Cmd_Make)
        {
            DataTable model_Table = BASE._VehicleDBOps.GetVehicle_Models(Cmd_Make) as DataTable;
            return DatatableToModel.DataTabletoMISC_INFO(model_Table);
        }
        public ActionResult RefreshModelListJson(string Cmd_Make)
        {
            return Content(JsonConvert.SerializeObject(RefreshModelListDropdown(Cmd_Make)), "application/json");
        }
        public List<ADDRESS_BOOK> RefreshOwnerListDropdown()
        {
            DataTable d1 = BASE._VehicleDBOps.GetOwners_List() as DataTable;
            return DatatableToModel.DataTabletoADDRESS_BOOK(d1);
        }

        public ActionResult RefreshOwnerListDropdownJson()
        {
            return Content(JsonConvert.SerializeObject(RefreshOwnerListDropdown()), "application/json");//Returned in JSON So that it can be stored in Array DataStore. Complex DataTypes can't be implicitly Converted
        }
        public List<InsMISC_INFO> RefreshInsuranceListDropdown()
        {
            DataTable d1 = BASE._VehicleDBOps.GetInsuranceCompanies("ID", "Name") as DataTable;
            return DatatableToModel.DataTabletoInsMISC_INFO(d1);
        }

        #endregion // <--Dropdowns: Vehicle Window-->

        [HttpGet]
        public ActionResult Get_OwnerShip_ValueChanged()
        {
            string Institute = BASE._open_Ins_Name;
            return Json(new
            {
                Institute = Institute,
                //BE_Fr_Tel_No = BE_Fr_Tel_No,
            }, JsonRequestBehavior.AllowGet);
        }

        public void SessionClear_VPWindow()
        {
            ClearBaseSession("_VPWindow");
        }

        public void SessionClear()
        {
            ClearBaseSession("_Vehicle");
            Session.Remove("VehiclesInfo_detailGrid_Data");
        }
        public void Vehicle_user_rights()
        {
            ViewData["Vehicle_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Vehicles, "Add");
            ViewData["Vehicle_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Vehicles, "Update");
            ViewData["Vehicle_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Vehicles, "View");
            ViewData["Vehicle_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Vehicles, "Delete");
            ViewData["Vehicle_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Vehicles, "Export");
            ViewData["Vehicle_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            ViewData["Vehicle_AdresBookListRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["Vehicle_AddHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Add");
            ViewData["Vehicle_ListHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "List");

            ViewData["Vehicle_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_Vehicles, Common.ClientAction.Lock_Unlock);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
        }

        #region DevExtreme
        public ActionResult Frm_Vehicles_Info_dx()
        {
            SetDefaultValues();
            Vehicle_user_rights();
            ViewBag.Special_Groupings = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Special_Groupings);
            ViewBag.Manage_Remarks = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Common_Lib.Common.ClientAction.Manage_Remarks);
            if (!CheckRights(BASE, ClientScreen.Profile_Vehicles, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Vehicles').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Vehicles).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            
            ViewData["Vehicle_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View();
        }

        public ActionResult Frm_Vehicles_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetProfileListing VehProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            VehProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
            VehProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            VehProfile.Next_YearID = BASE._next_Unaudited_YearID;
            VehProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
            DataTable VI_Table = BASE._VehicleDBOps.GetProfileListing(VehProfile);
            Vehicles_ExportData = DatatableToModel.DataTabletoVehicles_INFO(VI_Table);
            return Content(JsonConvert.SerializeObject(Vehicles_ExportData), "application/json");
        }
        public ActionResult Frm_Vehicles_Info_DetailGrid_dx(string RecID, bool VouchingMode = false)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_Vehicles, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Vehicle_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_Vehicles)), "application/json");
        }
        public ActionResult AdditionalInfo_Grid_dx()
        {
            return View(VehiclesInfo_AdditionalInfoGrid);
        }
        public ActionResult Frm_Export_Options_dx(string GridName)
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Vehicles, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Vehicles_report_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridName = GridName;
            ViewBag.Filename = GridName + "_" + BASE._open_UID_No + "_" + BASE._open_Year_ID;
            return View("Common_Export");
        }
        #endregion
    }
}