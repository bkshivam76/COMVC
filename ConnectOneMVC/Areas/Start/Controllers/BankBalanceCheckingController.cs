using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using ConnectOneMVC.Areas.Start.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using Common_Lib.RealTimeService;
using System.Collections;
using DevExpress.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    [CheckLogin]
    public class BankBalanceCheckingController : BaseController
    {
        public DataTable BankCheckingInfo_GridData
        {
            get { return (DataTable)GetBaseSession("BankCheckingInfo_GridData_BankChecking"); }
            set { SetBaseSession("BankCheckingInfo_GridData_BankChecking", value); }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> BankCheckingInfo_DetailGridData
        {
            get { return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("BankCheckingInfo_DetailGridData_BankChecking"); }
            set { SetBaseSession("BankCheckingInfo_DetailGridData_BankChecking", value); }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> BankChecking_AdditionalInfoGrid
        {
            get { return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("BankChecking_AdditionalInfoGrid_BankChecking"); }
            set { SetBaseSession("BankChecking_AdditionalInfoGrid_BankChecking", value); }
        }

        public ActionResult Frm_BankChecking_Info()
        {
            BankChecking_User_Rights();

            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Start_BankChecking).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.Open_FY_Edt = BASE._open_Year_Edt;
            BankCheckingInfo_GridData = BASE._BankAccountDBOps.Get_BankAccountCheckingList("PENDING");

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["BankCheckingInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
          || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;


            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Lock_Unlock);

            return View(BankCheckingInfo_GridData);
        }
        public ActionResult Frm_BankChecking_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string CheckingStatus = "ALL")
        {
            BankChecking_User_Rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;

            if (BankCheckingInfo_GridData == null || command == "REFRESH")
            {
                BankCheckingInfo_GridData = BASE._BankAccountDBOps.Get_BankAccountCheckingList(CheckingStatus);
            }

            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Lock_Unlock);

            return PartialView("Frm_BankChecking_Info_Grid", BankCheckingInfo_GridData);
        }

        public ActionResult Frm_BankChecking_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, int CEN_ID = 0)
        {
            ViewBag.BankCheckingInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.BankCheckingInfo_RecID = RecID;
            ViewBag.BankCheckingInfo_CEN_ID = CEN_ID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH") 
            {
                if (VouchingMode == false)
                {
                    BankCheckingInfo_DetailGridData = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_BankAccounts, true, CEN_ID);

                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, "", CEN_ID, ClientScreen.Profile_BankAccounts);
                    BankCheckingInfo_DetailGridData = data.DocumentMapping;
                    BankChecking_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(BankCheckingInfo_DetailGridData);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, int CEN_ID = 0)
        {
            ViewBag.ID = ID;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.BankCheckingLeftPane_CEN_ID = CEN_ID;
            return View();
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(BankChecking_AdditionalInfoGrid);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "BankCheckingGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "BankCheckingGrid";
            return settings;
        }
        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SaveBankBalance(string BA_REC_ID , decimal passbookbalance, string accountStatus, DateTime? lastTxnDate = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            if (BASE._BankAccountDBOps.InsertBankPassbookBalance(BA_REC_ID, passbookbalance, accountStatus, lastTxnDate)) {

                updateBankCheckingGrid_SessionData(BA_REC_ID, passbookbalance, accountStatus, lastTxnDate);

                jsonParam.message = "Bank Account Details are updated successfully.";
                jsonParam.title = "Success !!";
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

        public void updateBankCheckingGrid_SessionData(string BA_REC_ID, decimal passbookbalance, string accountStatus, DateTime? lastTxnDate = null)
        {
            DataRow dr = BankCheckingInfo_GridData.Select("ID='" + BA_REC_ID +"'").FirstOrDefault();
            if(dr != null)
            {
                dr["PASSBOOK_BALANCE"] = passbookbalance;
                dr["ACCOUNT_STATUS"] = accountStatus;
                dr["PASSBOOK_LAST_TXN_DT"] = lastTxnDate?? (object)System.DBNull.Value;
            }
        }
        public void BankChecking_User_Rights()
        {
            ViewData["BankChecking_AddRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "Add");
            ViewData["BankChecking_UpdateRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "Update");
            ViewData["BankChecking_ViewRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "View");
            ViewData["BankChecking_DeleteRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "Delete");
            ViewData["BankChecking_ExportRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "Export");
            ViewData["BankChecking_ListRight"] = CheckRights(BASE, ClientScreen.Start_BankChecking, "List");
            ViewData["BankChecking_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["BankChecking_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Start_BankChecking, Common.ClientAction.Lock_Unlock);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }

        public void SessionClear()
        {
            ClearBaseSession("_BankChecking");            
        }
    }
}