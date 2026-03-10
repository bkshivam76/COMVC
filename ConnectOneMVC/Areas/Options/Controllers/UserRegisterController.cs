using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.ClientUserInfo;
using ConnectOneMVC.Helper;
using Newtonsoft.Json;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Data;
using System.Drawing;
using System.IO;
using ConnectOneMVC.Areas.Options.Models;
using Common_Lib;
using System.Collections;
using static Common_Lib.DbOperations;
using Common_Lib.RealTimeService;
using System.Web.UI.WebControls;


namespace ConnectOneMVC.Areas.Options.Controllers
{
    public class UserRegisterController : BaseController
    {
        // GET: Options/UserRegister
        #region Global Variables
        public List<Return_GetRegister> UserRegisterdata
        {
            get
            {
                return (List<Return_GetRegister>)GetBaseSession("UserRegisterdata_Date_UserReg");
            }
            set
            {
                SetBaseSession("UserRegisterdata_Date_UserReg", value);
            }
        }
        public List<Return_GetPrivilegesRegister> UserPrivilegesRegisterdata
        {
            get
            {
                return (List<Return_GetPrivilegesRegister>)GetBaseSession("UserPrivilegesRegisterdata_UserReg");
            }
            set
            {
                SetBaseSession("UserPrivilegesRegisterdata_UserReg", value);
            }
        }

        public List<Jobs.Return_GetStockPersonnels> PersonnelNameData
        {
            get
            {
                return (List<Jobs.Return_GetStockPersonnels>) GetBaseSession("PersonnelNameData__UsrRegWindow");
            }
            set
            {
                SetBaseSession("PersonnelNameData__UsrRegWindow", value);
            }
        }

        public List<Return_GetGroupRegister_MainTable> GetgroupData
        {
            get
            {
                return (List<Return_GetGroupRegister_MainTable>) GetBaseSession("GetgroupData__UsrRegWindow");
            }
            set
            {
                SetBaseSession("GetgroupData__UsrRegWindow", value);
            }
        }


        #endregion

        #region UserRegister_Info & Grid
        public ActionResult Frm_UserRegister_Info()
        {
            UserReg_user_rights();
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Options_UserRegister, "List"))//Code written for User Authorization do not remove//0000260 bug fixed
            {
                ViewBag.ShowHorizontalBar = 0;
                var UserRegister = BASE._ClientUserDBOps.GetRegister();
                if ((UserRegister == null))
                {
                    return PartialView("Frm_UserRegister_Info_Grid", null);
                }
                UserRegisterdata = UserRegister;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                
                return View(UserRegister);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Options_UserRegister').hide();</script>");//Code written for User Authorization do not remove
            }
        }

        public ActionResult Frm_UserRegister_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            UserReg_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;

            //This part is to store the detail grid related data in to a session variable.
            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = null;
            InParam.UserID = null;

            if (UserPrivilegesRegisterdata == null || command == "REFRESH")
            {
                UserPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
                Session["UserPrivilegesRegisterdata"] = UserPrivilegesRegisterdata;
            } //This is to end the storage of data into the session variable.
            


            if (UserRegisterdata == null || command == "REFRESH")
            {
                var UserRegisterItems = BASE._ClientUserDBOps.GetRegister();
                if (UserRegisterItems != null)
                {
                    var Usergrid = UserRegisterItems;
                    UserRegisterdata = Usergrid;
                }
            }
            var usergrid_data = UserRegisterdata as List<DbOperations.ClientUserInfo.Return_GetRegister>;

            if (usergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(usergrid_data);
        }

        #region Detail Grid code for the list of previleges of the selected user in the info page.
        public ActionResult Frm_UserRegister_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.UserRegisterInfo_RecID = RecID;

            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = null;
            InParam.UserID = null;

            if (UserPrivilegesRegisterdata == null || command == "REFRESH")
            {
                UserPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
                Session["UserPrivilegesRegisterdata"] = UserPrivilegesRegisterdata;
            }
            //List<Return_GetPrivilegesRegister> udata = new List<Return_GetPrivilegesRegister>();
            //udata = (List<Return_GetPrivilegesRegister>)System.Web.HttpContext.Current.Session["UserPrivilegesRegisterdata"];
            //var udata1 = udata.FindAll(x => x.UserID == RecID);
            var udata = UserPrivilegesRegisterdata.FindAll(x => x.UserID == RecID);

            return PartialView(udata);
        }

        public static IEnumerable GetUserPrivilegesData(string RecID)
        {
            //List<DbOperations.Membership.Return_ExistingWingMembership_Balances> _MemberBalances = (List<DbOperations.Membership.Return_ExistingWingMembership_Balances>)System.Web.HttpContext.Current.Session["Membership_Wow_Balance_ExportData"];
            List<Return_GetPrivilegesRegister> udata = new List<Return_GetPrivilegesRegister>();
            udata = (List<Return_GetPrivilegesRegister>)System.Web.HttpContext.Current.Session["UserPrivilegesRegisterdata"];
            var udata1 = udata.FindAll(x => x.UserID == RecID);
            //var dist = gdata1.GroupBy(c => c.ScreenID).Select(y => y.First()).Distinct();
            //var old = (System.Web.HttpContext.Current.Session["UserPrivilegesRegisterdata"] as List<Return_GetPrivilegesRegister>).FindAll(x => x.UserID == RecID);
      
            return udata1;
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "UserRegisterListGridD" + RecID;
            settings.SettingsDetail.MasterGridName = "UserRegisterListGrid";
            settings.Width = Unit.Percentage(100);
            settings.KeyFieldName = "UserID";
            settings.Columns.Add("AddedBy").Visible = false;
            settings.Columns.Add(column =>
            {
                column.FieldName = "AddedOn";
                column.PropertiesEdit.DisplayFormatString = "d";
                column.Visible = false;

            });
            settings.Columns.Add("EditedBy").Visible = false;
            settings.Columns.Add("EntityName");
            settings.Columns.Add("GroupName");
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
            settings.Columns.Add("UserName").Visible = false;
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


        public ActionResult UserRegisterCustomDataAction(string key)
        {
            var FinalUser_Data = UserRegisterdata as List<Common_Lib.DbOperations.ClientUserInfo.Return_GetRegister>;
            var it = FinalUser_Data != null ? (Common_Lib.DbOperations.ClientUserInfo.Return_GetRegister)FinalUser_Data.Where(f => f.ID == key).FirstOrDefault() : null;
            string UserReg = "";
            if (it != null)
            {
                UserReg = it.UserName + "![" + it.PersonnelName + "![" + it.SewaDept + "![" + it.ContactNo + "![" + it.EmailID + "![" +
                     it.Groups + "![" + it.Is_Admin + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.EditedBy + "![" + it.EditedOn + "![" + it.ID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(UserReg);
        }
        #endregion

        #region UserRegister_Window

        public JsonResult DataNavigation(string ActionMethod,string UserID = "")//0000177 bug fixed
        {            
            if ((!CheckRights(BASE, ClientScreen.Options_UserRegister, "Delete")) && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            Model_UserRegisterWindow model = new Model_UserRegisterWindow();
            if (ActionMethod == "Delete")
            {
                int count = BASE._ClientUserDBOps.Get_ClientUser_EntriesCount(UserID);//0000177 bug fixed
                if (count > 0)
                {
                    return Json(new { result = false, message = "User which has been used in add/edit of any entry in any screen can not be deleted..!!", }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = true, message = Common_Lib.Messages.DeleteSuccess, }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = false, message = "", }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Frm_UserRegister_Window(string ActionMethod, string ID = "")
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View" };
            string[] AM = { "New", "Edit", "View" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Options_UserRegister, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }
     
            

            Model_UserRegisterWindow model = new Model_UserRegisterWindow();
            model.ActionMethod = ActionMethod;
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                Common_Lib.DbOperations.ClientUserInfo.Return_GetUserDetails UserRegisterInfo = BASE._ClientUserDBOps.GetUserDetails(ID);
                if (UserRegisterInfo != null)
                {
                    model.clientpersonnelName = UserRegisterInfo.PersonnelName;
                    model.clientpersonnelID = Convert.ToInt32(UserRegisterInfo.PersonnelID);
                    model.clientsewaDepartment = UserRegisterInfo.SewaDept;
                    model.clientUsername = UserRegisterInfo.UserName;
                    model.clientEmailID = UserRegisterInfo.EmailID;
                    model.clientMobileNo = UserRegisterInfo.ContactNo;
                    model.clientselectGroups = UserRegisterInfo.Groups;
                    model.clientIsAdminstrator = UserRegisterInfo.Is_Admin;
                    model.clientIsSelfPostedOnly = UserRegisterInfo.SelfPosted;
                    model.Password_UserRegister = UserRegisterInfo.UserPassword;
                    model.Confirm_Password_UserRegister = UserRegisterInfo.UserPassword;
                    model.REC_ID = ID;                    
                }
            }
            if (ActionMethod == "Edit")
            {
                ViewBag.UserEntriesCount = BASE._ClientUserDBOps.Get_ClientUser_EntriesCount(model.clientUsername);
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_UserRegister_Window(Model_UserRegisterWindow model)
        {
            string actionmethod = model.ActionMethod;
            try
            {
                if (actionmethod == "New")
                {
                    if (string.IsNullOrWhiteSpace(model.clientUsername))
                    {
                        return Json(new { result = false, message = "Username shouldn't have any space or it shouldn't be null..!!", }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Password_UserRegister))
                    {
                        return Json(new { result = false, message = "Password  Cannot be Empty..!!", }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Password_UserRegister.Length<8)
                    {
                        return Json(new { result = false, message = "Password Must be minimum of 8 characters in length..!!", }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Confirm_Password_UserRegister != model.Password_UserRegister)
                    {
                        return Json(new { result = false, message = "Password and Confirm Password are not Matching..!", }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    DataTable dt_UserInCenterData = BASE._ClientUserDBOps.GetUserDataIfAlreadyExists(model.clientUsername, model.REC_ID);
                    if (dt_UserInCenterData.Rows.Count > 0)
                    {
                        string msg = null;
                        for (int i = 0; i < dt_UserInCenterData.Rows.Count; i++)
                        {
                            if (i > 0) msg = msg + "<br>";
                            msg = msg + "Sorry Username(" + model.clientUsername + ") already exists in (" + dt_UserInCenterData.Rows[i]["CEN_UID"].ToString() + " " + dt_UserInCenterData.Rows[i]["CEN_NAME"].ToString() + ")";
                        }
                        return Json(new { result = false, message = msg, }, JsonRequestBehavior.AllowGet);
                    }
                    var usercount = Convert.ToInt32(BASE._ClientUserDBOps.GetUserNameCount(model.clientUsername,model.REC_ID));//0000175 bug fixed
                    if (model.clientUsername == null)
                    {
                        return Json(new { result = false, message = "Username  Cannot be Empty..!!",  }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((int)usercount > 0)
                    {
                        return Json(new { result = false, message = "Username should be Unique..!!",  }, JsonRequestBehavior.AllowGet);
                    }

                    

                    else if (model.clientIsAdminstrator == null)
                    {
                        return Json(new { result = false, message = "Select YES if Administrator, NO if not..!!",  }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (actionmethod == "New")
                {                    
                    Common_Lib.RealTimeService.Param_InsertClientUser Inparam = new Common_Lib.RealTimeService.Param_InsertClientUser();

                    if (Convert.ToInt32(model.clientpersonnelName) == 0 || model.clientpersonnelName == null)//Mantis bug 0000296 fixed
                    {
                        Inparam.USER_PERSONNEL_ID = null;
                    }
                    else
                    {
                        Inparam.USER_PERSONNEL_ID = Convert.ToInt32(model.clientpersonnelName);//Mantis bug 0000296 fixed
                    }
                    Inparam.USER_Name = model.clientUsername;
                    Inparam.USER_IS_ADMIN = Convert.ToBoolean(model.clientIsAdminstrator);
                    Inparam.SelfPostedOnly = Convert.ToBoolean(model.clientIsSelfPostedOnly);
                    Inparam.User_Password = model.Password_UserRegister;
                    if (model.selectGroups != null)
                    {
                        model.clientGroupID = ToIntArray(model.selectGroups, ',');
                        Inparam.Mapped_Group_IDs = model.clientGroupID;
                    }                    
                    if (BASE._ClientUserDBOps.InsertClientUser(Inparam))
                    {
                        return Json(new { result = true, message ="User "+model.clientUsername +" is created successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError,  }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Edit")
                {
                    Common_Lib.RealTimeService.Param_UpdateClientUser UpParam = new Common_Lib.RealTimeService.Param_UpdateClientUser();

                    UpParam.USER_ID = model.REC_ID;

                    if (Convert.ToInt32(model.clientpersonnelName) == 0 || model.clientpersonnelName == null)
                    {
                        UpParam.USER_PERSONNEL_ID = null;
                    }
                    else
                    {
                        UpParam.USER_PERSONNEL_ID = Convert.ToInt32(model.clientpersonnelName);
                    }

                    if (model.selectGroups == null)
                    {
                        UpParam.Mapped_Group_IDs = model.clientGroupID = null;
                    }
                    else
                    {
                        model.clientGroupID = ToIntArray(model.selectGroups, ',');
                        UpParam.Mapped_Group_IDs = model.clientGroupID;
                    }

                    UpParam.USER_Name = model.clientUsername;
                    UpParam.USER_IS_ADMIN = Convert.ToBoolean(model.clientIsAdminstrator);                                        
                    UpParam.SelfPostedOnly = Convert.ToBoolean(model.clientIsSelfPostedOnly);
                   
                    if (BASE._ClientUserDBOps.UpdateClientUser(UpParam))
                    {
                        return Json(new { result = true, message = Common_Lib.Messages.SaveSuccess, }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError,  }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Delete")
                {               
                    if (BASE._ClientUserDBOps.DeleteClientUser(model.REC_ID))
                    {

                        return Json(new { result = true, message = Common_Lib.Messages.DeleteSuccess, }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Common_Lib.Messages.SomeError,  }, JsonRequestBehavior.AllowGet);
                    }
                }               
                return Json(new { result = true, message = "",  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new { result = false, message = Message, }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion  


        #region export
            public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE,ClientScreen.Options_UserRegister,"Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('UserRegister_report_modal','Not Allowed','No Rights');$('#UserRegisterListPreview').hide();</script>");
            }
            return PartialView();
        }
        
        #endregion   


        #region "DropDown"
        public ActionResult LookUp_GetPersonnelName(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            PersonnelNameData = BASE._Jobs_Dbops.GetStockPersonnels();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PersonnelNameData, loadOptions)), "application/json");
        }

        public ActionResult LookUp_GetGroupsMapped(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            GetgroupData = BASE._ClientUserDBOps.GetGroupRegister().main_Register;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetgroupData, loadOptions)), "application/json");
        }

        #endregion

        #region "MISC"
        static int[] ToIntArray(string value, char separator)
        {
            return Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        }
        public void Sessionclear()
        {
            ClearBaseSession("_UserReg");
        } //clears session variable on popup close
        public void Sessionclear_Window()
        {
            ClearBaseSession("_UsrRegWindow");
        } //clears session variable on popup close

        public void UserReg_user_rights()
        {
            ViewData["UserReg_AddRight"] = CheckRights(BASE, ClientScreen.Options_UserRegister, "Add");
            ViewData["UserReg_UpdateRight"] = CheckRights(BASE, ClientScreen.Options_UserRegister, "Update");
            ViewData["UserReg_ViewRight"] = CheckRights(BASE, ClientScreen.Options_UserRegister, "View");
            ViewData["UserReg_DeleteRight"] = CheckRights(BASE, ClientScreen.Options_UserRegister, "Delete");
            ViewData["UserReg_ExportRight"] = CheckRights(BASE, ClientScreen.Options_UserRegister, "Export");
            ViewData["UserReg_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["UserReg_AddGroupRight"] = CheckRights(BASE, ClientScreen.Options_GroupMaster, "Add");           
            ViewData["UserReg_MngPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "List");           
            ViewData["UserReg_AddPrivilegeRight"] = CheckRights(BASE, ClientScreen.Options_Manage_Privileges, "Add");           
                 
        }
        #endregion
        
        #region Password Reset Action Method
        public JsonResult ResetPassword(string userid, string user_name)
        {
            if ((!CheckRights(BASE, ClientScreen.Options_UserRegister, "Edit")))
            {
                return Json(new { result = "NoEditRights", message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //string LoginUserID = BASE._open_UID_No;
                string username = user_name;
                string ps_word = BASE._Password_DBOps.GeneratePassword(16, Password.Password_Type.Only_Aplha_Numeric);

                if(BASE._Chang_Password_DBOps.ChangePassword(BASE._open_Cen_ID, username, ps_word))
                {
                    return Json(new { result = true, message = "Password Reset Successfully <br> <br> <b>User ID:</b> " + username + "<br>" + "<b>Password: </b>" + ps_word, }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = Common_Lib.Messages.SomeError,
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion
        #region Dev Extreme
        public ActionResult Frm_UserRegister_Info_dx()
        {
            UserReg_user_rights();
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Options_UserRegister, "List"))//Code written for User Authorization do not remove//0000260 bug fixed
            {
                ViewBag.ShowHorizontalBar = 0;
                
                //if ((UserRegister == null))
                //{
                //    return PartialView("Frm_UserRegister_Info_Grid", null);
                //}
                
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Options_UserRegister').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        [HttpGet]
        public ActionResult Frm_UserRegister_Info_Grid_dx()
        {
            //Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            //InParam.GroupID = null;
            //InParam.UserID = null;

            //if (UserPrivilegesRegisterdata == null)
            //{
            //    UserPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
            //    Session["UserPrivilegesRegisterdata"] = UserPrivilegesRegisterdata;
            //} //This is to end the storage of data into the session variable.

            UserRegisterdata = BASE._ClientUserDBOps.GetRegister();
            var usergrid_data = UserRegisterdata as List<DbOperations.ClientUserInfo.Return_GetRegister>;
            return Content(JsonConvert.SerializeObject(usergrid_data), "application/json");

        }
        public ActionResult Frm_UserRegister_Info_DetailGrid_dx(string RecID)
        {
            Param_GetPrivilegeRegister InParam = new Param_GetPrivilegeRegister();
            InParam.GroupID = null;
            InParam.UserID = null;

            if (UserPrivilegesRegisterdata == null )
            {
                UserPrivilegesRegisterdata = BASE._ClientUserDBOps.GetPrivilegesRegister(InParam); //These are useful for DetailGrid
            }
            var udata = UserPrivilegesRegisterdata.FindAll(x => x.UserID == RecID);
            return Content(JsonConvert.SerializeObject(udata), "application/json");

        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Options_UserRegister, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('UserRegister_report_modal','Not Allowed','No Rights');$('#UserRegisterListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
    }
}