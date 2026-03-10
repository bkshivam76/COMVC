using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class AssetController : BaseController
    {
        // GET: Profile/Asset
        #region Global Variables
        public List<AssetInfo> Asset_ExportData
        {
            get
            {
                return (List<AssetInfo>)GetBaseSession("Asset_ExportData_Asset");
            }
            set
            {
                SetBaseSession("Asset_ExportData_Asset", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> AssetInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("AssetInfo_DetailGrid_Data_Asset");
            }
            set
            {
                SetBaseSession("AssetInfo_DetailGrid_Data_Asset", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> AssetInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("AssetInfo_AdditionalInfoGrid_Asset");
            }
            set
            {
                SetBaseSession("AssetInfo_AdditionalInfoGrid_Asset", value);
            }
        }
        #endregion

        public ActionResult Frm_Asset_Info()
        {
            MovAsset_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Assets, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Assets').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Assets).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing AssetProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            AssetProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
            AssetProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            AssetProfile.Next_YearID = BASE._next_Unaudited_YearID;
            AssetProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
            DataTable AI_Table = BASE._AssetDBOps.GetProfileListing(AssetProfile);
            if (AI_Table.Rows.Count > 0)
            {
                CreateQRCodeBarCode(AI_Table.Rows[0]["QRCodeID"].ToString());
            } // "M:15b42a21-7daf-4d8a-bb6d-0ae7b150ce82");
            ViewData["AssetInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;


            // Base._AssetDBOps.GetList(Voucher_Entry, Profile_Entry)
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Assets, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.CheckActionRights = true;
            }
            else
            {
                ViewBag.CheckActionRights = false;
            }

            if ((AI_Table == null))
            {
                return View();
            }
            else
            {
                var assetdata = DatatableToModel.DataTabletoAsset_INFO(AI_Table);
                Asset_ExportData = assetdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(assetdata);
            }
        }

        public PartialViewResult Frm_Asset_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            MovAsset_user_rights();
          
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Assets, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.CheckActionRights = true;
            }
            else
            {
                ViewBag.CheckActionRights = false;
            }
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            if (Asset_ExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetProfileListing AssetProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
                AssetProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
                AssetProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                AssetProfile.Next_YearID = BASE._next_Unaudited_YearID;
                AssetProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
                DataTable AI_Table = BASE._AssetDBOps.GetProfileListing(AssetProfile);
                var assetdata = DatatableToModel.DataTabletoAsset_INFO(AI_Table);
                Asset_ExportData = assetdata; // redmine issue 133053 resolved
            }
            return PartialView(Asset_ExportData);
        }
        #region nested grid
        public ActionResult Frm_Asset_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.AssetInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.AssetInfo_RecID = RecID;
            ViewBag.AssetInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    AssetInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Assets);                   
                    Session["AssetInfo_detailGrid_Data"] = AssetInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Assets);
                    AssetInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    AssetInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(AssetInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(AssetInfo_AdditionalInfoGrid);
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
            settings.Name = "AssetListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "AssetListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["AssetInfo_detailGrid_Data"];
        }
        #endregion
        public ActionResult AssetCustomDataAction(string key)
        {
            var assetdata = Asset_ExportData as List<AssetInfo>;
            var it = (AssetInfo)assetdata.Where(f => f.ID == key).FirstOrDefault();
            string itstr = "";
            if (it != null)
            {
                itstr = it.ID + "![" + it.Edit_Date + "![" + it.Add_Date + "![" + it.Add_By + "![" + it.Edit_By + "![" + it.Action_Status + "![" + it.Action_Date + "![" + it.Action_By;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }

        public XRBarCode CreateQRCodeBarCode(string BarCodeText)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to QRCode.
            barCode.Symbology = new QRCodeGenerator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 150;

            // If the AutoModule property is set to false, uncomment the next line.
            barCode.AutoModule = true;
            // barcode.Module = 3;

            // Adjust the properties specific to the bar code type.
            //((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
            //((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            //((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;

            return barCode;
        }

        public ActionResult Frm_Asset_Window_Profile(string ActionMethod = null)
        {
            MovAsset_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Vehicles, "Add") && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Asset_Window','Not Allowed','No Rights');</script>");
            }

            return View();
        }

        #region Create detail

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
                else {
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
        #region export
        public void SessionClear()
        {
            ClearBaseSession("_Asset");
            Session.Remove("AssetInfo_detailGrid_Data");
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Assets, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Asset_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void MovAsset_user_rights()
        {
            ViewData["MovAsset_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Assets, "Add");
            ViewData["MovAsset_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Assets, "Update");
            ViewData["MovAsset_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Assets, "View");
            ViewData["MovAsset_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Assets, "Delete");
            ViewData["MovAsset_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Assets, "Export");            
            ViewData["MovAsset_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_Assets, Common.ClientAction.Lock_Unlock);
            //ViewData["MovAsset_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            //ViewData["Vehicle_AdresBookListRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            //ViewData["Vehicle_AddHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Add");
            //ViewData["Vehicle_ListHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "List");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
        #region Devextreme
        public ActionResult Frm_Asset_Info_dx()
        {
            MovAsset_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Assets, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Assets').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Assets).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["AssetInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Assets, Common_Lib.Common.ClientAction.Special_Groupings))
            {
                ViewBag.CheckActionRights = true;
            }
            else
            {
                ViewBag.CheckActionRights = false;
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View();
        }
        [HttpGet]
        public ActionResult Frm_Asset_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetProfileListing AssetProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            AssetProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
            AssetProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            AssetProfile.Next_YearID = BASE._next_Unaudited_YearID;
            AssetProfile.TableName = Common_Lib.RealTimeService.Tables.ASSET_INFO;
            DataTable AI_Table = BASE._AssetDBOps.GetProfileListing(AssetProfile);
            if (AI_Table.Rows.Count > 0)
            {
                CreateQRCodeBarCode(AI_Table.Rows[0]["QRCodeID"].ToString());
            } // "M:15b42a21-7daf-4d8a-bb6d-0ae7b150ce82");
            var assetdata = DatatableToModel.DataTabletoAsset_INFO(AI_Table);
            Asset_ExportData = assetdata; // redmine issue 133053 resolved
            return Content(JsonConvert.SerializeObject(Asset_ExportData), "application/json");
        }
        public ActionResult Frm_Asset_Info_DetailGrid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_Assets, !VouchingMode)), "application/json");

        }
        public ActionResult AdditionalInfo_Grid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_Assets)), "application/json");
        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Assets, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Asset_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }

        #endregion
    }
}