using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
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
    public class MagazineController : BaseController
    {
        #region Global Initializations        
        public DateTime LastEditedOn
        {
            get
            {
                return (DateTime)GetBaseSession("LastEditedOn_Magazine");
            }
            set
            {
                SetBaseSession("LastEditedOn_Magazine", value);
            }
        }
        public DataTable MM_Table_Mag
        {
            get
            {
                return (DataTable)GetBaseSession("MM_Table_Magazine");
            }
            set
            {
                SetBaseSession("MM_Table_Magazine", value);
            }
        }
        public List<DbOperations.Magazine.Return_Existing_Mag_Membership_List> Existing_MM_Data_Mag
        {
            get
            {
                return (List<DbOperations.Magazine.Return_Existing_Mag_Membership_List>)GetBaseSession("Existing_MM_Data_Magazine");
            }
            set
            {
                SetBaseSession("Existing_MM_Data_Magazine", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> Magazine_MembersInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("Magazine_MembersInfo_DetailGrid_Data_Magazine");
            }
            set
            {
                SetBaseSession("Magazine_MembersInfo_DetailGrid_Data_Magazine", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> Magazine_MembersInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("Magazine_MembersInfo_AdditionalInfoGrid_Magazine");
            }
            set
            {
                SetBaseSession("Magazine_MembersInfo_AdditionalInfoGrid_Magazine", value);
            }
        }
        #endregion

        #region Page Constructor
        public MagazineController()
        {
            BASE = new Common_Lib.Common();
            Helper.CommonFunctions.Programming_Mode(BASE);
        }
        #endregion

        #region Default Grid Page Action Method GET: Profile/Bank
        public ActionResult Frm_Magazine_Membership_Info()
        {
            BASE = new Common_Lib.Common();
            CommonFunctions.Programming_Mode(BASE);
            string MemName = "", MemID = "", MemOldNo = "";
            DataTable MM_Table = BASE._Magazine_DBOps.GetList_Membership(MemName, MemID, MemOldNo);
            MM_Table_Mag = MM_Table;
            return View(DatatableToModel.DataTable_to_Magazine_Membership_Info(MM_Table));
        }
        public PartialViewResult Frm_Magazine_Membership_Info_Grid(bool FilterVisible = false, bool SearchVisible = false, bool ShowGroupedColumns = false)
        {

            ViewBag.FilterVisible = FilterVisible;
            ViewBag.SearchVisible = SearchVisible;
            ViewBag.ShowGroupedColumns = ShowGroupedColumns;
            BASE = new Common_Lib.Common();
            CommonFunctions.Programming_Mode(BASE);
            string MemName = "", MemID = "", MemOldNo = "";
            DataTable MM_Table = BASE._Magazine_DBOps.GetList_Membership(MemName, MemID, MemOldNo);
            MM_Table_Mag = MM_Table;

            return PartialView("Frm_Magazine_Membership_Info_Grid", DatatableToModel.DataTable_to_Magazine_Membership_Info(MM_Table));
        }
        #endregion

        #region "Existing Magazine Members"
        public ActionResult Frm_Magazine_Members_Info()
        {
            Magazine_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Existing_Mag_member').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Existing_Mag_member).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            var Existing_MM_Data = BASE._Magazine_DBOps.GetExistingMembers("");
            Existing_MM_Data_Mag = Existing_MM_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Magazine_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(Existing_MM_Data_Mag);
        }

        public PartialViewResult Frm_Magazine_Members_Info_Grid(string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Magazine_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Existing_MM_Data_Mag == null || command == "REFRESH")
            {
                var Existing_MM_Data = BASE._Magazine_DBOps.GetExistingMembers("");
                Existing_MM_Data_Mag = Existing_MM_Data;
            }
            return PartialView("Frm_Magazine_Members_Info_Grid", Existing_MM_Data_Mag);
        }
        public ActionResult Frm_Magazine_Members_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.Magazine_MembersInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.Magazine_MembersInfo_RecID = RecID;
            ViewBag.Magazine_MembersInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Magazine_Membership);
                    Magazine_MembersInfo_DetailGrid_Data = _docList;
                    Session["Magazine_MembersInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Magazine_Membership);
                    Magazine_MembersInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Magazine_MembersInfo_detailGrid_Data"] = data.DocumentMapping;
                    Magazine_MembersInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(Magazine_MembersInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(Magazine_MembersInfo_AdditionalInfoGrid);
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
            settings.Name = "ExistingMagMemberListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "ExistingMagMemberListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["Magazine_MembersInfo_detailGrid_Data"];
        }
        #endregion

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('ExistingMagMember_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_Magazine");
            Session.Remove("Magazine_MembersInfo_detailGrid_Data");
        }
        public void Magazine_user_rights()
        {
            ViewData["Magazine_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "Add");
            ViewData["Magazine_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "Update");
            ViewData["Magazine_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "View");
            ViewData["Magazine_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "Delete");
            ViewData["Magazine_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Existing_Mag_member, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
        }
    }
}