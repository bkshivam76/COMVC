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
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class MembershipController : BaseController
    {
        // GET: Profile/Membership

        #region Global Variables
        public List<DbOperations.Membership.Return_ExistingWingMembership> Membership_Wow_ExportData
        {
            get
            {
                return (List<DbOperations.Membership.Return_ExistingWingMembership>)GetBaseSession("Membership_Wow_ExportData_WithoutWing");
            }
            set
            {
                SetBaseSession("Membership_Wow_ExportData_WithoutWing", value);
            }
        }
        public List<DbOperations.Membership.Return_ExistingWingMembership> Membership_Ww_ExportData
        {
            get
            {
                return (List<DbOperations.Membership.Return_ExistingWingMembership>)GetBaseSession("Membership_Ww_ExportData_WithWing");
            }
            set
            {
                SetBaseSession("Membership_Ww_ExportData_WithWing", value);
            }
        }

        public List<DbOperations.Membership.Return_ExistingWingMembership_Balances> Membership_Ww_Balance_ExportData
        {
            get
            {
                return (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)GetBaseSession("Membership_Ww_Balance_ExportData_WithWing");
            }
            set
            {
                SetBaseSession("Membership_Ww_Balance_ExportData_WithWing", value);
            }
        }
        public List<DbOperations.Membership.Return_ExistingWingMembership_Balances> Membership_Wow_Balance_ExportData
        {
            get
            {
                return (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)GetBaseSession("Membership_Wow_Balance_ExportData_WithoutWing");
            }
            set
            {
                SetBaseSession("Membership_Wow_Balance_ExportData_WithoutWing", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> MembershipWow_DetailGridData
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("MembershipWow_DetailGridData_WithoutWing");
            }
            set
            {
                SetBaseSession("MembershipWow_DetailGridData_WithoutWing", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> MembershipInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("MembershipInfo_DetailGrid_Data_WithoutWing");
            }
            set
            {
                SetBaseSession("MembershipInfo_DetailGrid_Data_WithoutWing", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> MembershipWowInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("MembershipInfo_AdditionalInfoGrid_WithoutWing");
            }
            set
            {
                SetBaseSession("MembershipInfo_AdditionalInfoGrid_WithoutWing", value);
            }
        }

        //This is for Subs List dropdown of Info_WithoutWing Screen
        public List<Subscribers_wow> SubsList_DD_Data_Wow
        {
            get { return (List<Subscribers_wow>)GetBaseSession("SubsList_DD_Data_Membership_WithoutWing"); }
            set { SetBaseSession("SubsList_DD_Data_Membership_WithoutWing", value); }
        }
        public List<Subscribers_ww> SubsList_DD_Data_Ww
        {
            get { return (List<Subscribers_ww>)GetBaseSession("SubsList_DD_Data_Membership_WithWing"); }
            set { SetBaseSession("SubsList_DD_Data_Membership_WithWing", value); }
        }


        //This list is for wings list dropdown of the info with wing screen.
        public List<WingsList_ww> WingList_DD_Data
        {
            get { return (List<WingsList_ww>)GetBaseSession("WingList_DD_Data_Membership_WithWing"); }
            set { SetBaseSession("WingList_DD_Data_Membership_WithWing", value); }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> MembershipWwInfo_AttachmentGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("Info_AttachmentGrid_Data_Membership_WithWing");
            }
            set
            {
                SetBaseSession("Info_AttachmentGrid_Data_Membership_WithWing", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> MembershipWowInfo_AttachmentGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("Info_AttachmentGrid_Data_Membership_WithoutWing");
            }
            set
            {
                SetBaseSession("Info_AttachmentGrid_Data_Membership_WithoutWing", value);
            }
        }

        
        public void SetDefaultValues()
        {
            //Membership_Wow_Balance_ExportData = new List<DbOperations.Membership.Return_ExistingWingMembership_Balances>();
            
        }
        #endregion

        #region Navigation logic to open one view on both.
        public ActionResult returnProperMembershipView()
        {
            //if (BASE._ClientUserDBOps.GetWingsCenterTasks().Rows.Count > 0)
            //{
                return Frm_Membership_Info();
            //}
            //else
            //{
            //    return Frm_Membership_WithoutWing_Info();
            //}
        }

        #endregion

        #region These are With Wing related Action results

        public ActionResult Frm_Membership_Info()
        {
            Membership_user_rights();
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Membership).ToString()) ? 1 : 0;

            ViewBag.WingsCenterTasks = BASE._ClientUserDBOps.GetWingsCenterTasks().Rows.Count;
            string Voucher_Entry = "Voucher Entry"; //These two are global variables. I need to change the declaration later on.
            string Profile_Entry = "Profile Entry";
            string OtherCondition = " AND MS.MS_NO = '' ";
            string OtherCondition_1 = "";
            ViewBag.open_yr_Acc_type = BASE._open_Year_Acc_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Membership_Wow_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            Exist_Wing_Members model = new Exist_Wing_Members();
            model.WingList_Data_Membership_Ww = LookUp_GetWingList_Membership_Ww();
            model.SubsList_Data_Membership_Ww = LookUp_GetSubsList_Membership_Ww();            
            Membership_Ww_ExportData = BASE._Membership_DBOps.GetList(Voucher_Entry, Profile_Entry, OtherCondition + OtherCondition_1);
            model.GridData_Membership_Ww = Membership_Ww_ExportData;

            return View("Frm_Membership_Info", model);
        }

        public PartialViewResult Frm_Membership_Ww_Info_Grid(string OtherCondition, string OtherCondition_1, string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeShownIndex = "", string ColumnToBeHidddenIndex = "")
        {
            Membership_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Membership_Ww_ExportData == null || command == "REFRESH")
            {
                Grid_Display(OtherCondition, OtherCondition_1);
            }
            ViewBag.SpecialGroupings = BASE.CheckActionRights(ClientScreen.Profile_Membership, Common.ClientAction.Special_Groupings);
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Profile_Membership, Common.ClientAction.Manage_Remarks);
            return PartialView("Frm_Membership_Ww_Info_Grid", Membership_Ww_ExportData);
        }


        public ActionResult Frm_Membership_Ww_Info_DetailGrid(string RecID, string command, string Gridpk, int ShowHorizontalBar = 0, string MID = "")
        {
            ViewBag.MembershipWwInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.MembershipWwInfo_RecID = RecID;
            ViewBag.MembershipWwInfo_MID = MID;
            ViewBag.MembershipWwGridPrimaryKey = Gridpk;
            if (command == "REFRESH")
            {
                Membership_Ww_Balance_ExportData = BASE._Membership_DBOps.GetBalancesList(""); //These are useful for DetailGrid
                Session["Membership_Ww_Balance_ExportData"] = Membership_Ww_Balance_ExportData;
                //MembershipWow_DetailGridData = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_AddressBook);
                //Session["MembershipWow_DetailGridData"] = MembershipWow_DetailGridData;

            }

            return PartialView(Membership_Ww_Balance_ExportData.FindAll(x => x.MS_ID == RecID));

            //Amount decimal
            //Description
            //Entry_Date datetime
            //MS_ID string
            //Mem_No
            //Membership
            //Period_From datetime
            //Period_To datetime
            //pKey string
        }

        //This code is for Audit Vouching detail grid
        public ActionResult Frm_Membership_Ww_Info_AttachmentGrid(string RecID, string command, string Gridpk, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.MembershipWwInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.MembershipWwInfo_RecID = RecID;
            ViewBag.MembershipWwInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.MembershipWwGridPrimaryKey = Gridpk;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    MembershipWwInfo_AttachmentGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Membership);
                    Session["Info_AttachmentGrid_Data_Membership_WithWing"] = MembershipWwInfo_AttachmentGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Membership);
                    MembershipWwInfo_AttachmentGrid_Data = data.DocumentMapping;
                    Session["Info_AttachmentGrid_Data_Membership_WithWing"] = data.DocumentMapping;
                    MembershipWowInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(MembershipWwInfo_AttachmentGrid_Data);
        }
        #endregion

        public ActionResult Frm_Membership_WithoutWing_Info()
        {
            Membership_user_rights();            
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Membership).ToString()) ? 1 : 0;

            string Voucher_Entry = "Voucher Entry"; //These two are global variables. I need to change the declaration later on.
            string Profile_Entry = "Profile Entry";
            string OtherCondition = "";
            string OtherCondition_1 = "";
            ViewBag.open_yr_Acc_type = BASE._open_Year_Acc_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Membership_Wow_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            Membership_Wow_ExportData = BASE._Membership_DBOps.GetList(Voucher_Entry, Profile_Entry, OtherCondition + OtherCondition_1);
             
            //Membership_Wow_Balance_ExportData = BASE._Membership_DBOps.GetBalancesList(""); //These are useful for DetailGrid
            //Session["Membership_Wow_Balance_ExportData"] = Membership_Wow_Balance_ExportData;
            return View("Frm_Membership_WithoutWing_Info", Membership_Wow_ExportData);
        }
        public PartialViewResult Frm_Membership_WithoutWing_Info_Grid(string OtherCondition, string OtherCondition_1, string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeShownIndex = "", string ColumnToBeHidddenIndex = "")
        {
            Membership_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;


            if (Membership_Wow_ExportData == null || command == "REFRESH")
            {                
                Grid_Display(OtherCondition, OtherCondition_1);
            }
            return PartialView("Frm_Membership_WithoutWing_Info_Grid", Membership_Wow_ExportData);
        }


        public ActionResult Frm_Membership_WithoutWing_Info_DetailGrid(string RecID, string command, string Gridpk, int ShowHorizontalBar = 0, string MID = "")
        {
            ViewBag.MembershipWowInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.MembershipWowInfo_RecID = RecID;
            ViewBag.MembershipWowInfo_MID = MID;
            ViewBag.MembershipWowGridPrimaryKey = Gridpk;
            if (command == "REFRESH")
            {
                Membership_Wow_Balance_ExportData = BASE._Membership_DBOps.GetBalancesList(""); //These are useful for DetailGrid
                Session["Membership_Wow_Balance_ExportData"] = Membership_Wow_Balance_ExportData;
                //MembershipWow_DetailGridData = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_AddressBook);
                //Session["MembershipWow_DetailGridData"] = MembershipWow_DetailGridData;

            }

            return PartialView(Membership_Wow_Balance_ExportData.FindAll(x => x.MS_ID == RecID));

            //Amount decimal
            //Description
            //Entry_Date datetime
            //MS_ID string
            //Mem_No
            //Membership
            //Period_From datetime
            //Period_To datetime
            //pKey string
        }

        //This code is for Audit Vouching detail grid
        public ActionResult Frm_Membership_Wow_Info_AttachmentGrid(string RecID, string command, string Gridpk, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.MembershipWwInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.MembershipWwInfo_RecID = RecID;
            ViewBag.MembershipWwInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.MembershipWowGridPrimaryKey = Gridpk;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    MembershipWowInfo_AttachmentGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Membership);
                    Session["Info_AttachmentGrid_Data_Membership_WithoutWing"] = MembershipWowInfo_AttachmentGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Membership);
                    MembershipWowInfo_AttachmentGrid_Data = data.DocumentMapping;
                    Session["Info_AttachmentGrid_Data_Membership_WithoutWing"] = data.DocumentMapping;
                    MembershipWowInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(MembershipWowInfo_AttachmentGrid_Data);
        }

        public void Grid_Display(string OtherCondition, string OtherCondition_1)
        {
            string Voucher_Entry = "Voucher Entry"; //These two are global variables. I need to change the declaration later on.
            string Profile_Entry = "Profile Entry";
            Membership_Ww_ExportData = BASE
                ._Membership_DBOps.GetList(Voucher_Entry, Profile_Entry, OtherCondition + OtherCondition_1);                
        }
        public void Grid_Display_Wow(string OtherCondition, string OtherCondition_1)
        {
            string Voucher_Entry = "Voucher Entry"; //These two are global variables. I need to change the declaration later on.
            string Profile_Entry = "Profile Entry";
            Membership_Wow_ExportData = BASE
                ._Membership_DBOps.GetList(Voucher_Entry, Profile_Entry, OtherCondition + OtherCondition_1);                
        }

        //Subs List Dropdown action definition for info without wing screen.
        public ActionResult LookUp_GetSubsList_Membership_Wow(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (SubsList_DD_Data_Wow == null || DDRefresh == true)
            {
                RefreshSubsList_Wow();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SubsList_DD_Data_Wow, loadOptions)), "application/json");
        }
        public void RefreshSubsList_Wow() //This fucntion is for info without wing screen Dropdown data retrieve from database
        {
            DataTable d1 = BASE._Membership_DBOps.GetSubscriptionList(BASE._open_Ins_ID);
            DataRow Row = d1.NewRow();
            Row["SI_NAME"] = "ALL";
            Row["SI_REC_ID"] = "ALL";
            d1.Rows.Add(Row);
            DataView dview = new DataView(d1);
            dview.Sort = "SI_NAME";
            ViewBag.SubsListRowCount = dview.Count;
            SubsList_DD_Data_Wow = DatatableToModel.DataTabletoMembershipLookUp_GetSubsList_Wow(dview.ToTable());
        }

        //Subs List Dropdown action definition for info without wing screen. 
        public List<Subscribers_ww> LookUp_GetSubsList_Membership_Ww(/*DataSourceLoadOptions loadOptions, bool DDRefresh = false*/)
        {            
            DataTable d1 = BASE._Membership_DBOps.GetSubscriptionList(BASE._open_Ins_ID).Select("SI_NAME = 'ANNUAL' OR SI_NAME = 'LIFE'").CopyToDataTable();
            //if (d1 == null)
            //{
            //    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
            //}
            DataRow Row = d1.NewRow();
            Row["SI_NAME"] = "ALL";
            Row["SI_REC_ID"] = "ALL";
            d1.Rows.Add(Row);

            DataView dview = new DataView(d1);
            dview.Sort = "SI_NAME";
            ViewBag.SubsListRowCount = dview.Count;
            //SubsList_DD_Data_Ww =
            return (List<Subscribers_ww>) DatatableToModel.DataTabletoMembershipLookUp_GetSubsList(dview.ToTable());
        }

        public List<WingsList_ww> LookUp_GetWingList_Membership_Ww(/*DataSourceLoadOptions loadOptions, bool DDRefresh = false*/)
        {
            DataTable d1 = BASE._Membership_DBOps.GetWings();
            DataView dview = new DataView(d1);
            dview.Sort = "WING_NAME";
            ViewBag.WingListRowCount = dview.Count;
            return DatatableToModel.DataTabletoMembershipLookUp_GetWingList(dview.ToTable());
        }

        public JsonResult DataNavigationWw(string ActionMethod, String ID, bool MultiUserConfirmation = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var MembershipWwRowData = Membership_Ww_ExportData.Where(f => f.ID == ID).FirstOrDefault();
                string xTemp_ID = ID;
                DateTime Edit_Date = Convert.ToDateTime(MembershipWwRowData.Edit_Date);
                //string YearID = MembershipWwRowData.YearID;
                string xType = MembershipWwRowData.Entry_Type;
                string xStatus = MembershipWwRowData.Action_Status;
                //string Category = MembershipWwRowData.Category;
                //int xOpenActions = Convert.ToInt32(MembershipWwRowData.Open_Actions);
                Common.Record_Status value = (Common.Record_Status)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                if (BASE.AllowMultiuser())
                {
                    if(ActionMethod == "ADD REMARKS")
                    {
                        if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Membership, Common_Lib.Common.ClientAction.Manage_Remarks))
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



        public ActionResult Frm_Membership_WithoutWing_Infod()
        {
            SetDefaultValues();
            Membership_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_Membership, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Membership').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.UserType = BASE._open_User_Type;
            var Members_Data = BASE._Membership_DBOps.GetList("Voucher Entry", "Profile Entry", "");
            Membership_Wow_ExportData = Members_Data;

            var MemberBal_Data = BASE._Membership_DBOps.GetBalancesList("");
            Membership_Wow_Balance_ExportData = MemberBal_Data;
            Session["Membership_Wow_Balance_ExportData"] = Membership_Wow_Balance_ExportData;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Membership_WW_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            return View(Membership_Wow_ExportData);
        }

        public PartialViewResult Frm_Membership_WithoutWing_Info_Gridd(string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Membership_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Membership_Wow_ExportData == null || command == "REFRESH")
            {
                var Members_Data = BASE._Membership_DBOps.GetList("Voucher Entry", "Profile Entry", "");
                Membership_Wow_ExportData = Members_Data;
            }
            return PartialView("Frm_Membership_WithoutWing_Info_Grid", Membership_Wow_ExportData);
        }
        //public PartialViewResult Frm_Membership_WithoutWing_Info_Grid_Detaild(string command, string MemberID)
        //{
        //    ViewData["MemberID"] = MemberID;
        //    if (Membership_Wow_Balance_ExportData == null || command == "REFRESH")
        //    {
        //        var MemberBal_Data = BASE._Membership_DBOps.GetBalancesList("");
        //        Membership_Wow_Balance_ExportData = MemberBal_Data;
        //    }
        //    List<DbOperations.Membership.Return_ExistingWingMembership_Balances> _MemberBalances = (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)Membership_Wow_Balance_ExportData;
        //    var sel_bal_info = _MemberBalances.FindAll(x => x.MS_ID == MemberID);
        //    var ret_list = new List<DbOperations.Membership.Return_ExistingWingMembership_Balances> { sel_bal_info };
        //    return PartialView("Frm_Membership_WithoutWing_Info_Grid_Detail", ret_list);
        //}
        public ActionResult Frm_Membership_WithoutWing_Info_DetailGridd(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.MembershipInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.MembershipInfo_RecID = RecID;
            ViewBag.MembershipInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_Membership);
                    MembershipInfo_DetailGrid_Data = _docList;
                    Session["MembershipInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_Membership);
                    MembershipInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["MembershipInfo_detailGrid_Data"] = data.DocumentMapping;
                    MembershipWowInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(MembershipInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(MembershipWowInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public ActionResult LeftPaneContentWw(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }

        
        public static IEnumerable AttachmentGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["MembershipWowInfo_AttachmentGrid_Data"];
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
                catch (Exception)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Lbl_Status_Color = "blue";
                    Pic_Status = "Fa Fa-Lock";
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
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
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
        public ActionResult Frm_Export_OptionsWw()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Membership, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WingMember_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        
        public static GridViewSettings CreateGeneralDetailGridSettingsWw(string Gridpk)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MembershipWwListGrid" + Gridpk;
            settings.SettingsDetail.MasterGridName = "MembershipWithWingListGrid";
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "pKey";
            settings.Columns.Add("MS_ID").Visible = false;
            settings.Columns.Add("Entry_Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Mem_No");
            settings.Columns.Add("Membership");
            settings.Columns.Add("Description");
            settings.Columns.Add("Period_From").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Period_To").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add(column =>
            {
                column.FieldName = "Amount";
                column.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }

        //This code is for Attachment Grid Export Settings.
        public static GridViewSettings AttachmentGridExportSettingsWw(string Gridpk)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MembershipWw_Attachment_ListGrid" + Gridpk;
            settings.SettingsDetail.MasterGridName = "MembershipWithWingListGrid";
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            
            return settings;
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Membership, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WingMember_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        
        public static GridViewSettings CreateGeneralDetailGridSettings(string Gridpk)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MembershipWowListGrid" + Gridpk;
            settings.SettingsDetail.MasterGridName = "MembershipWithoutWingListGrid";
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "pKey";
            settings.Columns.Add("MS_ID").Visible = false;
            settings.Columns.Add("Entry_Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Mem_No");
            settings.Columns.Add("Membership");
            settings.Columns.Add("Description");
            settings.Columns.Add("Period_From").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Period_To").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add(column =>
            {
                column.FieldName = "Amount";
                column.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }

        //This code is for Attachment Grid Export Settings.
        public static GridViewSettings AttachmentGridExportSettings(string Gridpk)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MembershipWow_Attachment_ListGrid" + Gridpk;
            settings.SettingsDetail.MasterGridName = "MembershipWithoutWingListGrid";
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            
            return settings;
        }

        #endregion
        public static IEnumerable GetMembershipBalances(string RecID)
        {
            List<DbOperations.Membership.Return_ExistingWingMembership_Balances> _MemberBalances = (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)System.Web.HttpContext.Current.Session["Membership_Wow_Balance_ExportData"];
            var sel_bal_info = _MemberBalances.Find(x => x.MS_ID == RecID);
            var ret_list = new List<DbOperations.Membership.Return_ExistingWingMembership_Balances> { sel_bal_info };
            return ret_list;
        }
        public void SessionClear()
        {
            ClearBaseSession("_WithoutWing");
            ClearBaseSession("_WithWing");
            Session.Remove("Info_AttachmentGrid_Data_Membership_WithWing");
            Session.Remove("Info_AttachmentGrid_Data_Membership_WithoutWing");
            Session.Remove("MembershipWowInfo_AttachmentGrid_Data"); 
            Session.Remove("Membership_Wow_Balance_ExportData");
            Session.Remove("Membership_Ww_Balance_ExportData");            
        }
        
       
        public void Membership_user_rights()
        {
            ViewData["Membership_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Membership, "Add");
            ViewData["Membership_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Membership, "Update");
            ViewData["Membership_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Membership, "View");
            ViewData["Membership_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Membership, "Delete");
            ViewData["Membership_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Membership, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["MembershipWow_SpecialGroupings"] = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_Membership, Common_Lib.Common.ClientAction.Special_Groupings);

            //This line added by BKRK on 14th Sep, 2021.            
            ViewBag.ManageRemarks = BASE.CheckActionRights(ClientScreen.Profile_Membership, Common.ClientAction.Manage_Remarks);
            ViewBag.LockUnlock = BASE.CheckActionRights(ClientScreen.Profile_Membership, Common.ClientAction.Lock_Unlock);
        }

        public JsonResult Frm_Membership_OpenPageLogic()
        {
            string screenName;
            if (BASE._ClientUserDBOps.GetWingsCenterTasks().Rows.Count >0)
            {
                //screenName = "Frm_Membership_Info";
                screenName = "Frm_Membership_WithoutWing_Info";
            }
            else
            {
                screenName  = "Frm_Membership_WithoutWing_Info";                
            }
             
            return Json(new { screenName }, JsonRequestBehavior.AllowGet);
        }
    }
}