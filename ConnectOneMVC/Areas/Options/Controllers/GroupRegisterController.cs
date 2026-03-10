using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Options.Models;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using static Common_Lib.DbOperations.ClientUserInfo;
using Common_Lib;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Options.Controllers
{
    [CheckLogin]
    public class GroupRegisterController : BaseController
    {
        // GET: Options/GroupRegister
        #region Global Variables
        public List<Return_GetGroupRegister_MainTable> GroupRegisterMaindata
        {
            get
            {
                return (List<Return_GetGroupRegister_MainTable>)GetBaseSession("GroupRegisterMaindata_GroupReg");
            }
            set
            {
                SetBaseSession("GroupRegisterMaindata_GroupReg", value);
            }
        }
        public List<Return_GetGroupRegister_NestedTable> GroupRegisterNesteddata
        {
            get
            {
                return (List<Return_GetGroupRegister_NestedTable>)GetBaseSession("GroupRegisterNesteddata_GroupReg");
            }
            set
            {
                SetBaseSession("GroupRegisterNesteddata_GroupReg", value);
            }
        }

        public List<Return_GetPrivilegesRegister> GroupPrivilegesRegisterdata
        {
            get
            {
                return (List<Return_GetPrivilegesRegister>)GetBaseSession("GroupPrivilegesRegisterdata_GroupReg");
            }
            set
            {
                SetBaseSession("GroupPrivilegesRegisterdata_GroupReg", value);
            }
        }


        public List<Return_GetRegister> GroupMappingGridData
        {
            get
            {
                return (List<Return_GetRegister>)GetBaseSession("GroupMappingGridData_GroupReg");
            }
            set
            {
                SetBaseSession("GroupMappingGridData_GroupReg", value);
            }
        }
        public List<Return_GetPrivilegesRegister> ManagPrivilegesData
        {
            get
            {
                return (List<Return_GetPrivilegesRegister>)GetBaseSession("ManagPrivilegesData_MngPrivileges");
            }
            set
            {
                SetBaseSession("ManagPrivilegesData_MngPrivileges", value);
            }
        }

        //Add Privileges Global Variables
        public List<Return_GetScreens> GroupMasterScreenData
        {
            get
            {
                return (List<Return_GetScreens>)GetBaseSession("GroupMasterScreenData_AddPrivileges");
            }
            set
            {
                SetBaseSession("GroupMasterScreenData_AddPrivileges", value);
            }
        }

        public List<Return_GetPrivileges> AddPrivilegesData
        {
            get
            {
                return (List<Return_GetPrivileges>)GetBaseSession("AddPrivilegesData_AddPrivileges");
            }
            set
            {
                SetBaseSession("AddPrivilegesData_AddPrivileges", value);
            }
        }

        //Manage Privileges Global variables
        public List<Return_GetRegister> UserPrivilegesData
        {
            get
            {
                return (List<Return_GetRegister>)GetBaseSession("UsersPrivdata_MngPrivileges");
            }
            set
            {
                SetBaseSession("UsersPrivdata_MngPrivileges", value);
            }
        }

        public List<Return_GetGroupRegister_MainTable> GroupPrivilegesData
        {
            get
            {
                return (List<Return_GetGroupRegister_MainTable>)GetBaseSession("GroupPrivdata_MngPrivileges");
            }
            set
            {
                SetBaseSession("GroupPrivdata_MngPrivileges", value);
            }
        }

        #endregion
        #region GroupRegister_Grid
        public ActionResult Frm_GroupRegister_Info()
        {
            GroupReg_user_rights();
            if (CheckRights(BASE, ClientScreen.Options_GroupMaster, "List"))//Code written for User Authorization do not remove//0000260 bug fixed
            {
                ViewBag.ShowHorizontalBar = 0;
                var GroupRegister = BASE._ClientUserDBOps.GetGroupRegister();
                if (GroupRegister == null)
                {
                    return View();
                }
                var GroupRegister_MainGrid = GroupRegister.main_Register;
                var GroupRegister_NestedGrid = GroupRegister.nested_Register;
                GroupRegisterMaindata = GroupRegister_MainGrid;
                GroupRegisterNesteddata = GroupRegister_NestedGrid;
                Session["GroupRegisterNesteddata"] = GroupRegisterNesteddata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();                

                return View(GroupRegister_MainGrid);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Options_GroupMaster').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Frm_GroupRegister_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            GroupReg_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (GroupRegisterMaindata == null || command == "REFRESH")
            {
                var GroupRegister = BASE._ClientUserDBOps.GetGroupRegister();

                if (GroupRegister != null)
                {
                    GroupRegisterMaindata = GroupRegister.main_Register;
                    GroupRegisterNesteddata = GroupRegister.nested_Register;
                }
            }
            var GroupRegisterGridData = GroupRegisterMaindata;

            
            //The Below Few Lines is to store the detail grid data into session variable for further use
            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = null;
            InParam.UserID = null;

            if (GroupPrivilegesRegisterdata == null || command == "REFRESH")
            {
                GroupPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
                Session["GroupPrivilegesRegisterdata"] = GroupPrivilegesRegisterdata;
            }//End of the Storing detail grid data into session variable.



            if (GroupRegisterGridData == null)
            {
                return View();
            }
            else
            {
                return View(GroupRegisterGridData);
            }
        }
        public PartialViewResult Frm_GroupRegister_Nested_Grid(string command, int? ID)
        {
            if (GroupRegisterNesteddata == null || command == "REFRESH")
            {
                var GroupRegisterNested_Data = BASE._ClientUserDBOps.GetGroupRegister().nested_Register;
                GroupRegisterNesteddata = GroupRegisterNested_Data;
            }
            ViewData["GroupRegister_ExpandedGroupID"] = ID;
            ViewData["Datafor_ID"] = ID;
            var Nested_Data = (List<Return_GetGroupRegister_NestedTable>)GroupRegisterNesteddata;
            List<Return_GetGroupRegister_NestedTable> data = Nested_Data.FindAll(x => x.GroupID == ID);
            return PartialView(data);
        }
        public ActionResult GroupRegisterCustomDataAction(int key)
        {
            var Group_Data = GroupRegisterMaindata as List<Return_GetGroupRegister_MainTable>;
            var it = Group_Data != null ? Group_Data.Where(f => f.ID == key).FirstOrDefault() : null;
            string GroupReg = "";
            if (it != null)
            {
                GroupReg = it.AddedBy + "![" + it.AddedOn + "!["
                        + it.EditedBy + "![" + it.EditedOn + "![" + it.ID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(GroupReg);
        }
        public ActionResult GroupRegisterNestedCustomDataAction(string key)
        {
            var NestedData = GroupRegisterNesteddata as List<Return_GetGroupRegister_NestedTable>;
            var merge = NestedData != null ? NestedData.FindAll(x => x.ID == key).FirstOrDefault() : null;

            string NestedReg = "";
            if (merge != null)
            {
                NestedReg = merge.AddedBy + "![" + merge.AddedOn + "!["
                          + merge.EditedBy + "![" + merge.EditedOn + "![" + merge.ID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(NestedReg);
        }
        public static GridViewSettings GroupRegisterNestedGridSettings(int GroupID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GroupRegisterNestedGrid" + GroupID;
            settings.SettingsDetail.MasterGridName = "GroupRegisterListGrid";
            settings.KeyFieldName = "GroupID";
            settings.Columns.Add("UserName");
            settings.Columns.Add("Personnel_Name");
            settings.Columns.Add("Sewa_Dept").Visible = true;
            settings.Columns.Add("SkillTypes").Visible = true;
            settings.Columns.Add("UserType").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("GroupID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "GroupRegisterNestedGridFocusedRowChanged";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "GroupRegisterdblclickEdit";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for exporting nested grid 
        public static IEnumerable GetUserName(int GroupID)
        {
            List<Return_GetGroupRegister_NestedTable> data = (List<Return_GetGroupRegister_NestedTable>)System.Web.HttpContext.Current.Session["GroupRegisterNesteddata"];
            List<Return_GetGroupRegister_NestedTable> UserNamelist = data.FindAll(x => x.GroupID == GroupID);
            return UserNamelist;
        }//binding data to nested grid
        #endregion
        #region GroupRegister_NEVD
        public ActionResult DataNavigation(string ActionMethod, int ID = 0)
        {    
            if (ActionMethod == "Delete")
            {
                int count = BASE._ClientUserDBOps.GetGroupRegister().nested_Register.FindAll(x => x.GroupID == ID).Count();
                if (count > 0)
                {
                    return Json(new { result = false, message = "A group which has users mapped to it , cannot be deleted..!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_GroupRegister_Window(string ActionMethod, int ID = 0, string PopupName = null, string PostSucessFunction = null)
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View" };
            string[] AM = { "New", "Edit", "View" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Options_GroupMaster, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }            

            Model_GroupRegister_Window model = new Model_GroupRegister_Window();
            model.ActionMethod = ActionMethod;

            model.PostSuccessFunction = PostSucessFunction != null ? PostSucessFunction : "GroupRegisterAjaxSuccessForm";
            model.PopupName = PopupName != null ? PopupName : "Dynamic_Content_popup";

            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var Data = BASE._ClientUserDBOps.GetGroupDetails(ID.ToString());
                if (Data != null)
                {
                    model.Grup_Register_GrupName = Data.GroupName;
                    model.Grup_Register_GrupDescription = Data.GroupRemarks;
                    model.ID = ID;
                }
            }
            return View(model);
        }//Group Register NEW BUTTON Form data is fetched.
        [HttpPost]
        public ActionResult Frm_GroupRegister_Window(Model_GroupRegister_Window model)//Group Register NEW, EDIT & DELETE BUTTON  Form getting posted
        {
            try
            {
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    if (string.IsNullOrEmpty(model.Grup_Register_GrupName))
                    {
                        return Json(new { result = false, message = "Group name field is mandatory..!!" }, JsonRequestBehavior.AllowGet);
                    }
                    var DataCount = BASE._ClientUserDBOps.GetGroupNameCount(model.Grup_Register_GrupName, model.ID);
                    if ((int)DataCount > 0)
                    {
                        return Json(new { result = false, message = "Group Name should be unique in UID..!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "New")
                {
                    Param_InsertClientUserGroup param = new Param_InsertClientUserGroup();
                    param.Group_Name = model.Grup_Register_GrupName;
                    param.Remarks = model.Grup_Register_GrupDescription;
                    var DataAdd = BASE._ClientUserDBOps.InsertClientUserGroup(param);
                    if (DataAdd == true)
                    {
                        return Json(new { result = true, message = model.Grup_Register_GrupName + " is created successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "Edit")
                {
                    Param_UpdateClientUserGroup UpParam = new Param_UpdateClientUserGroup();
                    UpParam.Group_Name = model.Grup_Register_GrupName;
                    UpParam.Remarks = model.Grup_Register_GrupDescription;
                    UpParam.Group_ID = model.ID;
                    var DataUpdate = BASE._ClientUserDBOps.UpdateClientUserGroup(UpParam);
                    if (DataUpdate == true)
                    {
                        return Json(new { result = true, message = Common_Lib.Messages.UpdateSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "Delete")
                {
                    var DataDelete = BASE._ClientUserDBOps.DeleteClientUserGroup(model.ID);
                    if (DataDelete == true)
                    {
                        return Json(new { result = true, message = Common_Lib.Messages.DeleteSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_GroupRegister_Export_Options()
        {
            if (!CheckRights(BASE,ClientScreen.Options_GroupMaster,"Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#GroupRegisterListPreview').hide();</script>");
            }
            return View();
        }
        #endregion
        #region Add_User_to_Group
        [HttpGet]
        public ActionResult Frm_GroupRegister_UserGroupMapping(string UgFLAG, string UserID = "", int GroupID = 0)
        {
            if (UgFLAG == "User")
            {
                if (!CheckRights(BASE,ClientScreen.Options_UserRegister,"Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#UserRegisterAdUsrGrp').hide();</script>");
                }
            }
            else if (UgFLAG == "Group")
            {
                if (!CheckRights(BASE,ClientScreen.Options_GroupMaster,"Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#GroupRegisterAdUsrGrp').hide();</script>");
                }
            }
            Model_GroupMapping_Window model = new Model_GroupMapping_Window();
            model.UgFLAG = UgFLAG;

            if (GroupID > 0) { model.GrupMapping_GrupName = GroupID; }

            if (UserID == null || UserID == "")
            { return View(model); }
            else
            { ViewData["MappedUserID"] = UserID; }

            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_GroupRegister_UserGroupMapping(Model_GroupMapping_Window model)
        {
            string[] splitMU;
            try
            {
                if (model.GrupMapping_GrupName == 0)
                {
                    return Json(new { result = false, message = "Group name field is mandatory..!!" }, JsonRequestBehavior.AllowGet);
                }

                if (model.MappedUser == null || model.MappedUser.Length < 1 || model.MappedUser[0] == "")
                {
                    return Json(new { result = false, message = "No user is mapped to Group..!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var MUValue = model.MappedUser[0];
                    splitMU = MUValue.Split(',').Select(sValue => sValue.Trim()).ToArray();
                }

                Param_SaveClientUserGroupMapping UpParam = new Param_SaveClientUserGroupMapping();
                UpParam.GroupID = (int)model.GrupMapping_GrupName;
                UpParam.MappedUserIDs = splitMU;

                var data = BASE._ClientUserDBOps.SaveClientUserGroupMapping(UpParam);
                if (data == true)
                {
                    return Json(new { result = true, message = Common_Lib.Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = true, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Find_MappedUser(int DropdownID)
        {
            var Nested_Data = (List<Return_GetGroupRegister_NestedTable>)BASE._ClientUserDBOps.GetGroupRegister().nested_Register;
            var data = Nested_Data.FindAll(x => x.GroupID == DropdownID);
            var userid = data.Select(x => x.ID).ToList();
            return Json(userid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GroupMapping_MappedUserGrid(string command)
        {
            if (GroupMappingGridData == null || command == "REFRESH")
            {
                var data = BASE._ClientUserDBOps.GetRegister();
                GroupMappingGridData = data;
                return View(data);
            }
            else
            {
                var data1 = GroupMappingGridData as List<Return_GetRegister>;
                return View(data1);
            }

        }
        public ActionResult UserGroupMapping_Dropdown(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._ClientUserDBOps.GetGroupRegister().main_Register;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        #endregion
        #region Manage_Privileges
        #region Manage_Privileges_Grid
        [HttpGet]
        public ActionResult Frm_GroupRegister_ManagPrivileges(string UGFlag, string PopupID, int? GroupID = null, string UserID = null)
        {
            ManagePrivilege_UserRight();
            if ((bool)ViewData["MngPrvlg_MngPrivilegeRight"] == false)//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (!(GroupID==0) && !(GroupID==null))
            {
                if (!CheckRights(BASE,ClientScreen.Options_GroupMaster, "Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#GroupRegisterManagPrivileges').hide();</script>");                    
                }
            }
            if (UserID != null || UserID != "" || UserID != "undefined")
            {
                if (!CheckRights(BASE,ClientScreen.Options_UserRegister,"Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#UserRegisterManagPrivileges').hide();</script>");
                }
            }
            ViewBag.UserIsAdmin = BASE._open_User_User_Is_Admin;
            ViewData["GroupRegisterUGFlag"] = UGFlag;            
            Model_ManagPrivileges_Window model = new Model_ManagPrivileges_Window();
            //model.PopupID = UGFlag == "User" ? "ManagPrivilegeUserPopup_report_modal" : "ManagPrivilegeGroupPopup_report_modal";
            model.PopupID = PopupID;
            model.UGFlag = UGFlag;
            model.GridData = new List<Return_GetPrivilegesRegister>();
            model.ManPri_GroupID = GroupID;
            model.ManPri_UserID = UserID;
            return View(model);
        }//Manage Privileges Screen Loading Action Method data is Fetched.
        public ActionResult Frm_ManagPrivileges_Grid(int? GroupID, string UserID, string command, int ShowHorizontalBar = 0)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = GroupID;
            InParam.UserID = UserID;
            if (ManagPrivilegesData == null || command == "REFRESH")
            {
                var ManagPrivileges = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam);

                if (ManagPrivileges != null)
                {
                    ManagPrivilegesData = ManagPrivileges;
                    ViewBag.ManagPrivilegesData = ManagPrivilegesData;

                }
            }
            var ManagPrivilegesGridData = ManagPrivilegesData as List<Return_GetPrivilegesRegister>;



            if (ManagPrivilegesGridData == null)
            {
                return View();
            }
            else
            {
                return View(ManagPrivilegesGridData);
            }
        }//Grid of Manage Privileges screen will be loaded by this//connected with view
       
        #endregion
        #region Manage_Privileges_Dropdown
        public ActionResult ManPri_User_Dropdown(DataSourceLoadOptions loadOptions, bool AllUser = false)//connected with view
        {
            if(UserPrivilegesData == null)
            {
                UserPrivilegesData = BASE._ClientUserDBOps.GetRegister(!AllUser);
            }
            
            //Session["UsersPrivdata_MngPrivileges"] = data;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UserPrivilegesData, loadOptions)), "application/json");

        }
        public ActionResult ManPri_Group_Dropdown(DataSourceLoadOptions loadOptions)
        {
            if(GroupPrivilegesData == null)
            {
                GroupPrivilegesData = BASE._ClientUserDBOps.GetGroupRegister().main_Register;
            }                       
            //Session["GroupPrivdata_MngPrivileges"] = data;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GroupPrivilegesData, loadOptions)), "application/json");
        }//connected with view
        #endregion


        #region Detail Grid code for the list of previleges of the selected user in the info page.
        public ActionResult Frm_GroupPrivileges_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.GroupRegisterInfo_RecID = RecID;

            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = null;
            InParam.UserID = null;

            if (GroupPrivilegesRegisterdata == null || command == "REFRESH")
            {
                GroupPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
                Session["GroupPrivilegesRegisterdata"] = GroupPrivilegesRegisterdata;
            }

            var gdata = GroupPrivilegesRegisterdata.FindAll(x => x.GrpID == Int32.Parse(RecID));
            //List<Return_GetPrivilegesRegister> gdata = new List<Return_GetPrivilegesRegister>();
            //gdata = (List<Return_GetPrivilegesRegister>)System.Web.HttpContext.Current.Session["GroupPrivilegesRegisterdata"];
            //var gdata1 = gdata.FindAll(x => x.GrpID == Int32.Parse(RecID));
            //var dist = gdata1.GroupBy(c => c.ScreenID).Select(y => y.First()).Distinct(); //This line makes the table distinct.   
            var dist = gdata.GroupBy(c => c.ScreenID).Select(y => y.First()).Distinct(); //This line makes the table distinct.   
            
            return PartialView(dist);
        }

        public static IEnumerable GetGroupPrivilegesData(int RecID) 
        {
            //List<DbOperations.Membership.Return_ExistingWingMembership_Balances> _MemberBalances = (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)System.Web.HttpContext.Current.Session["Membership_Wow_Balance_ExportData"];
            //var sel_bal_info = (System.Web.HttpContext.Current.Session["GroupPrivilegesRegisterdata"] as List<Return_GetPrivilegesRegister>).FindAll(x => x.GrpID == RecID);
            List<Return_GetPrivilegesRegister> gdata = new List<Return_GetPrivilegesRegister>();
            gdata = (List<Return_GetPrivilegesRegister>)System.Web.HttpContext.Current.Session["GroupPrivilegesRegisterdata"];
            var gdata1 = gdata.FindAll(x => x.GrpID == RecID);
            var dist = gdata1.GroupBy(c => c.ScreenID).Select(y => y.First()).Distinct();
            return dist;
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(int RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GroupRegisterListGridD" + RecID;
            settings.SettingsDetail.MasterGridName = "GroupRegisterListGrid";
            settings.Width = Unit.Percentage(100);
            settings.KeyFieldName = "GrpID";
            settings.Columns.Add("AddedBy").Visible = false;
            settings.Columns.Add(column =>
            {
                column.FieldName = "AddedOn";
                column.PropertiesEdit.DisplayFormatString = "d";
                column.Visible = false;

            });
            settings.Columns.Add("EditedBy").Visible = false;
            settings.Columns.Add("EntityName");
            settings.Columns.Add("GroupName").Visible = false;
            settings.Columns.Add("GroupNameV").Visible = false;
            settings.Columns.Add("GrpID").Visible = false;
            settings.Columns.Add("PrivilegeID").Visible = false;
            settings.Columns.Add("PrivilegesGiven");
            settings.Columns.Add("ScreenID").Visible = false;            
            settings.Columns.Add(column =>
            {
                column.FieldName = "EditedOn";
                column.PropertiesEdit.DisplayFormatString = "d";
                column.Visible = false;
            });
            settings.Columns.Add("UserID").Visible = false;
            settings.Columns.Add("UserName");
            settings.Columns.Add("UserNameV").Visible = false;
            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = "Amount";
            //    column.PropertiesEdit.DisplayFormatString = "c";
            //});
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }
        #endregion




        #region Manage_Privileges_NEVD
        [HttpGet]
        public ActionResult Frm_ManagPrivileges_AdUserPrivileges(string ActionMethod, string PrivilegeID = null, int? ScreenID = null, int? GroupID = null, string UserID = null, string UserName = null, string PopupID = null)//Add User Privileges NEW,Edit,Delete,View BUTTON Form Data loaded
        {
            ManagePrivilege_UserRight();
            if ((bool)ViewData["MngPrvlg_AddPrivilegeRight"] == false && ActionMethod=="New")//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }
            if ((bool)ViewData["MngPrvlg_UpdatePrivilegeRight"] == false && ActionMethod=="Edit")//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }
            if ((bool)ViewData["MngPrvlg_ViewPrivilegeRight"] == false && ActionMethod=="View")//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }
            if ((bool)ViewData["MngPrvlg_DeletePrivilegeRight"] == false && ActionMethod=="Delete")//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }

            Model_AdUserPrivileges_Window model = new Model_AdUserPrivileges_Window();
            GroupID = GroupID == 0 ? null : GroupID;
            if (GroupID != null) 
            {
                model.AdUserPrivileges_GroupName = GroupRegisterMaindata.Find(x => x.ID == GroupID).Group_Name;
            }
            model.AdUserPrivileges_GroupID = GroupID;
            model.AdUserPrivileges_UserID = UserID;
            if (UserName == null) { UserName = ""; }
            model.AdUserPrivileges_UserName = UserName;
            model.ActionMethod = ActionMethod;            
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var PriID = PrivilegeID;
                if(ScreenID != null && ScreenID != 0)
                {
                    model.AdUserPrivileges_EntityScreenID = new int[1];
                    model.AdUserPrivileges_EntityScreenID[0] = (int) ScreenID;
                }
          
                model.AdUserPrivileges_PrivilegesAllowed = PriID;
            }
            model.PopupID = PopupID;
            return View(model);
        }//
        [HttpPost]
        public ActionResult Frm_ManagPrivileges_AdUserPrivileges(Model_AdUserPrivileges_Window model)//Add User Privileges NEW, EDIT & DELETE BUTTON  Form getting posted
        {            
            try
            {
                string[] AdUserPrivileges_EntityScreenarr = null;
                AdUserPrivileges_EntityScreenarr = model.AdUserPrivileges_EntityScreenStr.Split(',');
                model.AdUserPrivileges_EntityScreenID = Array.ConvertAll(AdUserPrivileges_EntityScreenarr, int.Parse);
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.AdUserPrivileges_PrivilegesAllowed))//redmine bug 132964 fix
                    {
                        return Json(new { result = false, message = "Privileges allowed field is mandatory..!!" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (model.AdUserPrivileges_EntityScreenID == null || model.AdUserPrivileges_EntityScreenID.Length == 0)
                    {
                        return Json(new { result = false, message = "Entity/Screen field is mandatory..!!" }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((string.IsNullOrEmpty(model.AdUserPrivileges_UserID) || model.AdUserPrivileges_UserID == "") && model.AdUserPrivileges_GroupID == 0)
                    {
                        return Json(new { result = false, message = "Either Group or User field has to be selected..!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "New")
                {
                    var DataAdd = true;

                    for (int i = 0; i < model.AdUserPrivileges_EntityScreenID.Length; i++)
                    {
                        Param_InsertClientUserPrivileges InParam = new Param_InsertClientUserPrivileges();
                        if (model.AdUserPrivileges_GroupID == null || model.AdUserPrivileges_GroupID == 0)
                        {
                            InParam.GroupID = 0;
                            InParam.UserName = model.AdUserPrivileges_UserName;
                        }
                        if (model.AdUserPrivileges_UserName == null || model.AdUserPrivileges_UserName == "")
                        {
                            InParam.UserName = null;
                            InParam.GroupID = (Int32)model.AdUserPrivileges_GroupID;
                        }
                        InParam.TaskID = (Int32)model.AdUserPrivileges_EntityScreenID[i];
                        //These are list selected privileges
                        var Privileg_Split = model.AdUserPrivileges_PrivilegesAllowed.Split(',');
                        for (int j = 0; j < Privileg_Split.Length; j++)
                        {
                            InParam.Privilege_Code = Privileg_Split[j];
                            DataAdd = BASE._ClientUserDBOps.InsertClientUserPrivileges(InParam);
                            if (DataAdd == false)
                            {
                                return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                            }
                        }

                    }

                    if (DataAdd == true)
                    {
                        if (model.AdUserPrivileges_GroupID == null || model.AdUserPrivileges_GroupID == 0)
                        {
                            return Json(new { result = true, message = "Privileges granted to " + model.AdUserPrivileges_UserName + " successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.AdUserPrivileges_UserName == null || model.AdUserPrivileges_UserName == "")
                        {
                            return Json(new { result = true, message = "Privileges granted to " + model.AdUserPrivileges_GroupName + " successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }//0000187 bug fixed
                }

                if (model.ActionMethod == "Edit")
                {
                    var DataDelete = false;
                    if (model.AdUserPrivileges_GroupID == null || model.AdUserPrivileges_GroupID == 0)
                    {
                        DataDelete = BASE._ClientUserDBOps.DeleteClientUserPrivileges((int)model.AdUserPrivileges_EntityScreenID[0], BASE._open_Cen_ID, model.AdUserPrivileges_UserName);
                    }
                    if (model.AdUserPrivileges_UserName == null || model.AdUserPrivileges_UserName == "")
                    {
                        DataDelete = BASE._ClientUserDBOps.DeleteClientUserPrivileges((int)model.AdUserPrivileges_EntityScreenID[0], BASE._open_Cen_ID, null, (int)model.AdUserPrivileges_GroupID);
                    }

                    var DataAdd = false;
                    Param_InsertClientUserPrivileges InParam = new Param_InsertClientUserPrivileges();
                    if (model.AdUserPrivileges_GroupID == null || model.AdUserPrivileges_GroupID == 0)
                    {
                        InParam.GroupID = 0;
                        InParam.UserName = model.AdUserPrivileges_UserName;
                    }
                    if (model.AdUserPrivileges_UserName == null || model.AdUserPrivileges_UserName == "")
                    {
                        InParam.UserName = null;
                        InParam.GroupID = (Int32)model.AdUserPrivileges_GroupID;
                    }
                    InParam.TaskID = (int)model.AdUserPrivileges_EntityScreenID[0];
                    var Privileg_Split = model.AdUserPrivileges_PrivilegesAllowed.Split(',');
                    for (int i = 0; i < Privileg_Split.Length; i++)
                    {
                        InParam.Privilege_Code = Privileg_Split[i];
                        DataAdd = BASE._ClientUserDBOps.InsertClientUserPrivileges(InParam);
                        if (DataAdd == false)
                        {
                            return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (DataAdd == true)
                    {
                        return Json(new { result = true, message = Common_Lib.Messages.UpdateSuccess }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "Delete")
                {
                    var DataDelete = false;
                    if (model.AdUserPrivileges_GroupID == null || model.AdUserPrivileges_GroupID == 0)
                    {
                        DataDelete = BASE._ClientUserDBOps.DeleteClientUserPrivileges((int)model.AdUserPrivileges_EntityScreenID[0], BASE._open_Cen_ID, model.AdUserPrivileges_UserName);
                    }
                    if (model.AdUserPrivileges_UserName == null || model.AdUserPrivileges_UserName == "")
                    {
                        DataDelete = BASE._ClientUserDBOps.DeleteClientUserPrivileges((int)model.AdUserPrivileges_EntityScreenID[0], BASE._open_Cen_ID, null, (int)model.AdUserPrivileges_GroupID);
                    }
                    if (DataDelete == true)
                    {
                        return Json(new { result = true, message = Common_Lib.Messages.DeleteSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }            
        }
        public ActionResult Frm_ManagPrivileges_Export_Options()//connected with View
        {
            if(CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Export") == false)
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'ManagPrivileges_report_modal');</script>");
            }
            return View();
        }
        public ActionResult AdUserPrivileges_User_Dropdown(DataSourceLoadOptions loadOptions,bool AllUser=false)
        {
            var data = BASE._ClientUserDBOps.GetRegister(!AllUser);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
            
        }//Connected with view
        public ActionResult AdUserPrivileges_EntityScreen_Dropdown(DataSourceLoadOptions loadOptions)
        {
            if(GroupMasterScreenData == null)
            {
                GroupMasterScreenData = BASE._ClientUserDBOps.GetScreens(ClientScreen.Options_GroupMaster);
            }
            
            //Session["GroupMasterScreenData_AddPrivileges"] = data;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GroupMasterScreenData, loadOptions)), "application/json");
        }//connected with view
        public ActionResult LookUp_GetPrivilegesAllowedList(DataSourceLoadOptions loadOptions)
        {
            if(AddPrivilegesData == null)
            {
                AddPrivilegesData = BASE._ClientUserDBOps.GetPrivileges();
            }            
            //Session["AddPrivilegesData_AddPrivileges"] = data;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AddPrivilegesData, loadOptions)), "application/json");
        }
        #endregion
        #endregion
        #region MISC
        public void SessionClear()
        {
            ClearBaseSession("_GroupReg");
        }

        public void SessionClear_AddPrivileges()
        {
            ClearBaseSession("_AdPrivileges");
        }

        public void SessionClear_MngPrivileges()
        {
            ClearBaseSession("_MngPrivileges");
        }

        public void GroupReg_user_rights()
        {            
            ViewData["GroupReg_AddRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Add");
            ViewData["GroupReg_UpdateRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Update");
            ViewData["GroupReg_ViewRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "View");
            ViewData["GroupReg_DeleteRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Delete");
            ViewData["GroupReg_ExportRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Export");
            ViewData["GroupReg_ReportRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Report");
            ViewData["GroupReg_ListRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "List");
            ViewData["GroupReg_MngPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "List");
            ViewData["GroupReg_AddPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Add");        
        }
        public void ManagePrivilege_UserRight()
        {
            ViewData["MngPrvlg_MngPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "List");
            ViewData["MngPrvlg_AddPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Add");
            ViewData["MngPrvlg_UpdatePrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Update");
            ViewData["MngPrvlg_ViewPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "View");
            ViewData["MngPrvlg_DeletePrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Delete");
            ViewData["MngPrvlg_ExportPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Export");
        }
        #endregion
        #region Devextreme
        public ActionResult Frm_GroupRegister_ManagPrivileges_dx(string UGFlag, string PopupID, int? GroupID = null, string UserID = null)
        {
            ManagePrivilege_UserRight();
            if ((bool)ViewData["MngPrvlg_MngPrivilegeRight"] == false)//Code written for User Authorization do not remove//0000260 bug fixed
            {
                //Code written for User Authorization do not remove
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'" + PopupID + "');</script>");
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (!(GroupID == 0) && !(GroupID == null))
            {
                if (!CheckRights(BASE, ClientScreen.Options_GroupMaster, "Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#GroupRegisterManagPrivileges').hide();</script>");
                }
            }
            if (UserID != null || UserID != "" || UserID != "undefined")
            {
                if (!CheckRights(BASE, ClientScreen.Options_UserRegister, "Update"))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#UserRegisterManagPrivileges').hide();</script>");
                }
            }
            ViewBag.UserIsAdmin = BASE._open_User_User_Is_Admin;
            ViewData["GroupRegisterUGFlag"] = UGFlag;
            Model_ManagPrivileges_Window model = new Model_ManagPrivileges_Window();
            //model.PopupID = UGFlag == "User" ? "ManagPrivilegeUserPopup_report_modal" : "ManagPrivilegeGroupPopup_report_modal";
            model.PopupID = PopupID;
            model.UGFlag = UGFlag;
            model.GridData = new List<Return_GetPrivilegesRegister>();
            model.ManPri_GroupID = GroupID;
            model.ManPri_UserID = UserID;
            return View(model);
        }//Manage Privileges Screen Loading Action Method data is Fetched.
        public ActionResult Frm_ManagPrivileges_Grid_dx(int? GroupID, string UserID)
        {
            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = GroupID;
            InParam.UserID = UserID;
            var ManagPrivileges = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam);
            if (ManagPrivileges != null)
            {
                ManagPrivilegesData = ManagPrivileges;
                ViewBag.ManagPrivilegesData = ManagPrivilegesData;
            }
            var ManagPrivilegesGridData = ManagPrivilegesData as List<Return_GetPrivilegesRegister>;

            return Content(JsonConvert.SerializeObject(ManagPrivilegesGridData), "application/json");
        }//Grid of Manage Privileges screen will be loaded by this//connected with view
        public ActionResult Frm_ManagPrivileges_Export_Options_dx()//connected with View
        {
            if (CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Export") == false)
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'ManagPrivileges_report_modal');</script>");
            }
            return View();
        }

        #endregion
    }
}