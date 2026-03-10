using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Areas.Start.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using Common_Lib;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    public class LoginController : BaseController
    {
        public DataTable InstituteData
        {
            get { return (DataTable)GetBaseSession("InstituteData_ChangeInstitute"); }
            set { SetBaseSession("InstituteData_ChangeInstitute", value); }
        }
        public DataTable CenterData
        {
            get { return (DataTable)GetBaseSession("CenterData_ChangeInstitute"); }
            set { SetBaseSession("CenterData_ChangeInstitute", value); }
        }
        public int? cenID
        {
            get { return (int?)GetBaseSession("CenID_ChangeInstitute"); }
            set { SetBaseSession("CenID_ChangeInstitute", value); }
        }
        public int? yearID
        {
            get { return (int?)GetBaseSession("YearID_ChangeInstitute"); }
            set { SetBaseSession("YearID_ChangeInstitute", value); }
        }
        public int? CompletedYearCount
        {
            get { return (int?)GetBaseSession("CompletedYearCount_ChangeInstitute"); }
            set { SetBaseSession("CompletedYearCount_ChangeInstitute", value); }
        }
        #region Global Initializations
        public class Frm_Login_Params
        {
            string INS_NAME;
            string CEN_NAME_X;
            string CEN_NAME;
            string CEN_PAD_NO_Main;
            string CEN_ID_MAIN;
            string CEN_ZONE_ID;
            string CEN_ZONE_SUB_ID;
            string CEN_UID;
            string CEN_PAD_NO;
            string CEN_ACC_TYPE_ID;
            string CEN_ID;
            string COD_YEAR_EDT;
            string COD_YEAR_ID;
            string COD_YEAR_NAME;
            string CEN_REC_ID;
            string COD_YEAR_SDT;
            string INS_ID;
            string INS_SHORT;
            string IS_VOLUME;
        }
        #endregion

        #region Page Constructor
        public LoginController()
        {
            //Helper.CommonFunctions.Programming_Mode(BASE);
        }
        #endregion

        #region Default Center Selection Pages
        public ActionResult Frm_COD_Selection(string deviceToken = "", string androidID = "", string RedirectToAndroid = "NO")
        {
            ViewBag.androidID = androidID;
            ViewBag.RedirectToAndroid = RedirectToAndroid;
            return View(model: deviceToken);
        }
        public ActionResult Frm_COD_Selection_form()
        {
            return View();
        }
        #endregion

        #region Center Institute Selection Grid
        public ActionResult Frm_COD_Selection_Ins_Grid(string certificate_no)
        {
            try
            {
                ViewBag.certificate_number = certificate_no;
                if (Session["CODSelectionInsExportData"] == null)
                {
                    Session["CODSelectionInsExportData"] = (DataTable)BASE._CenterDBOps.GetCen_Ins_List(certificate_no);
                }
                return View(Session["CODSelectionInsExportData"]);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult CODSelectionInsCustomDataAction(string key)
        {
            string itstr = "";
            DataRow[] row = ((DataTable)Session["CODSelectionInsExportData"]).Select("[CEN_ID] ='" + key + "'");
            if (row.Length > 0)
            {
                itstr = row[0]["INS_NAME"] + "![" + row[0]["CEN_NAME_X"] + "![" + row[0]["CEN_UID"] + "![" + row[0]["CEN_PAD_NO"] + "![" +
                            row[0]["CEN_ACC_TYPE_ID"] + "![" + row[0]["CEN_ID"] + "![" + row[0]["COD_YEAR_EDT"] + "![" + row[0]["COD_YEAR_ID"] + "![" +
                            row[0]["COD_YEAR_NAME"] + "![" + row[0]["CEN_REC_ID"] + "![" + row[0]["COD_YEAR_SDT"] + "![" + row[0]["INS_ID"] + "![" + row[0]["INS_SHORT"] + "![" + row[0]["IS_VOLUME"];
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Frm_COD_Selection_Ins_Check(string certificate_number)
        {
            ViewBag.certificate_number = certificate_number;
            SelectedCenterDetails CenterDetails = new SelectedCenterDetails();
            try

            {
                DataTable cData = null;
                int Cert = 0;
                if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)

                            || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                {
                    if (string.IsNullOrEmpty(certificate_number))
                    {
                        return Json(new { result = false, message = "Please enter certificate no." }, JsonRequestBehavior.AllowGet);
                    }
                    if (!int.TryParse(certificate_number, out Cert))
                    {
                        //return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                        return Json(new { result = false, message = (("Please enter Valid Center Certificate No..!" + ("<br/>" + ("<br/>" + "Please enter Numeric Value only. e.g. 1780 for Certificate No ES\\SC\\ORI\\1780\\10")))) }, JsonRequestBehavior.AllowGet);
                    }

                    cData = BASE._CenterDBOps.GetSelectCentreList(certificate_number);
                }

                if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)
                            || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                {
                    if ((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Local))
                    {
                        //CenterDetails.CEN_NAME = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "Centre Name").ToString();
                        //CenterDetails.CEN_ID_Main = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "ID").ToString();
                        //CenterDetails.CEN_PAD_NO_Main = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "Certificate No").ToString();
                        //CenterDetails.CEN_ZONE_ID = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "XCEN_ZONE_ID").ToString();
                        //CenterDetails.CEN_ZONE_SUB_ID = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "XCEN_ZONE_SUB_ID").ToString();
                    }
                    else if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)
                                || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                    {
                        if ((cData == null))
                        {
                            return Json(new { result = false, message = "center not created yet or server down" }, JsonRequestBehavior.AllowGet);
                        }

                        if ((cData.Rows.Count == 0))
                        {
                            return Json(new { result = false, message = "center not created yet or server down" }, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            CenterDetails.CEN_NAME = cData.Rows[0]["Centre Name"].ToString();
                            CenterDetails.CEN_ID_Main = cData.Rows[0]["ID"].ToString();
                            CenterDetails.CEN_PAD_NO_Main = cData.Rows[0]["Certificate No"].ToString();
                            CenterDetails.CEN_ZONE_ID = cData.Rows[0]["XCEN_ZONE_ID"].ToString();
                            CenterDetails.CEN_ZONE_SUB_ID = cData.Rows[0]["XCEN_ZONE_SUB_ID"].ToString();
                            return PartialView("frm_COD_Selection_Ins_Check", CenterDetails);
                            //return Json(CenterDetails,JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return Json(CenterDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_COD_Selection_Ins(string certificate_number)
        {
            ViewBag.certificate_number = certificate_number;
            SelectedCenterDetails CenterDetails = new SelectedCenterDetails();
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                DataTable cData = null;
                int Cert = 0;
                if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)

                            || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                {
                    if (string.IsNullOrWhiteSpace(certificate_number))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Please Enter Center Certificate No..!";
                        jsonParam.title = "Information Missing!";

                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (!int.TryParse(certificate_number, out Cert))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Please Enter Valid Center Certificate No..!<br><br>Please Enter Numeric Value Only. e.g. 1780 for Certificate No ES\\SC\\ORI\\1780\\10";
                        jsonParam.title = "Invalid Information !";
                        //return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    cData = BASE._CenterDBOps.GetSelectCentreList(certificate_number);
                }

                if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)
                            || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                {
                    if ((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Local))
                    {
                        //CenterDetails.CEN_NAME = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "Centre Name").ToString();
                        //CenterDetails.CEN_ID_Main = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "ID").ToString();
                        //CenterDetails.CEN_PAD_NO_Main = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "Certificate No").ToString();
                        //CenterDetails.CEN_ZONE_ID = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "XCEN_ZONE_ID").ToString();
                        //CenterDetails.CEN_ZONE_SUB_ID = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "XCEN_ZONE_SUB_ID").ToString();
                    }
                    else if (((BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.Online)
                                || (BASE.curr_Db_Conn_Mode(Common_Lib.RealTimeService.ClientScreen.Start_Select_Center) == Common_Lib.Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore)))
                    {
                        if ((cData == null))
                        {
                            jsonParam.result = false;
                            jsonParam.message = Messages.NoCenterCreated;
                            jsonParam.title = "";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if ((cData.Rows.Count == 0))
                        {
                            jsonParam.result = false;
                            jsonParam.message = Messages.NoCenterCreated;
                            jsonParam.title = "";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable Cen_Ins_List = (DataTable)BASE._CenterDBOps.GetCen_Ins_List(certificate_number.Trim());
                        Session["CODSelectionInsExportData"] = Cen_Ins_List;
                        if (Cen_Ins_List == null)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Cen_Ins_List.Rows.Count == 0)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Messages.NoCenterCreated;
                            jsonParam.title = "";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            CenterDetails.CEN_NAME = cData.Rows[0]["Centre Name"].ToString();
                            CenterDetails.CEN_ID_Main = cData.Rows[0]["ID"].ToString();
                            CenterDetails.CEN_PAD_NO_Main = cData.Rows[0]["Certificate No"].ToString();
                            CenterDetails.CEN_ZONE_ID = cData.Rows[0]["XCEN_ZONE_ID"].ToString();
                            CenterDetails.CEN_ZONE_SUB_ID = cData.Rows[0]["XCEN_ZONE_SUB_ID"].ToString();
                            return PartialView("frm_COD_Selection_Ins", CenterDetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.result = false;
                jsonParam.title = "Error!";
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            return Json(CenterDetails, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Auditor Login Navigation
        public ActionResult Frm_Auditor_Login(string deviceToken = "", string androidID = "", string RedirectToAndroid = "NO")
        {

            Auditor_Login auditor_Login = new Auditor_Login();
            auditor_Login.DEVICE_TOKEN = deviceToken;
            auditor_Login.ANDROID_ID = androidID;
            auditor_Login.RedirectToAndroid = RedirectToAndroid;
            TempData["Ischk_Remember"] = false;
            if (Request.Cookies["Frm_Auditor_Cookies"] != null)
            {
                HttpCookie getCookie = Request.Cookies.Get("Frm_Auditor_Cookies");
                auditor_Login.Txt_User = getCookie.Values["Txt_User"].ToString();
                auditor_Login.Txt_Pass = getCookie.Values["Txt_Pass"].ToString();
                //auditor_Login.chk_Remember = true;
                BASE._open_User_Remember = true;
                TempData["Ischk_Remember"] = true;
            }
            return View(auditor_Login);
        }

        [HttpPost]
        public ActionResult Frm_Auditor_Login(Auditor_Login AL, string SessionGUID, bool chk_Remember = false)
        {
            if (chk_Remember == true)
            {
                BASE._open_User_Remember = true;
                int timeout = chk_Remember ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
                var ticket = new FormsAuthenticationTicket(AL.Txt_User, chk_Remember, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                HttpCookie Frm_Auditor_Cookies = new HttpCookie("Frm_Auditor_Cookies");
                Response.Cookies.Remove("Frm_Auditor_Cookies");
                Response.Cookies.Add(Frm_Auditor_Cookies);
                Frm_Auditor_Cookies.Values.Add("Txt_User", AL.Txt_User);
                Frm_Auditor_Cookies.Values.Add("Txt_Pass", AL.Txt_Pass);
                Frm_Auditor_Cookies.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
                Frm_Auditor_Cookies.HttpOnly = true; // cookie not available in javascript.
            }
            else
            {
                if (Request.Cookies["Frm_Auditor_Cookies"] != null)
                {
                    Response.Cookies.Remove("Frm_Auditor_Cookies");
                    HttpCookie Frm_Auditor_Cookies = new HttpCookie("Frm_Auditor_Cookies");
                    Frm_Auditor_Cookies.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(Frm_Auditor_Cookies);
                }
                BASE._open_User_Remember = false;
            }

            DataTable UserInfo = BASE._ClientUserDBOps.GetUserInfo(AL.Txt_User == null ? "" : AL.Txt_User);
            if ((UserInfo == null))
            {
                return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
            }

            if ((UserInfo.Rows.Count == 0))
            {
                return Json(new { result = false, message = Common_Lib.Messages.InvalidUserID }, JsonRequestBehavior.AllowGet);
            }

            string actualPassword = UserInfo.Rows[0]["USER_PWD"].ToString();
            string UserType = UserInfo.Rows[0]["USER_ROLE_ID"].ToString();
            AL.Txt_User = UserInfo.Rows[0]["USER_ID"].ToString();
            if (((UserType.ToUpper() != Common_Lib.Common.ClientUserType.Auditor.ToUpper())
                        && (UserType.ToUpper() != Common_Lib.Common.ClientUserType.SuperUser.ToUpper())))
            {
                return Json(new { result = false, message = Common_Lib.Messages.InvalidAuditorID }, JsonRequestBehavior.AllowGet);
            }

            AL.Txt_Pass = string.IsNullOrEmpty(AL.Txt_Pass) ? "" : AL.Txt_Pass;

            if ((actualPassword == null))
            {
                return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
            }
            else if ((BASE.Encrypt(AL.Txt_Pass.ToUpper()) == actualPassword.ToUpper()))
            {
                BASE._open_User_User_Is_Admin = Convert.ToBoolean(UserInfo.Rows[0]["USER_IS_ADMIN"]);
                BASE._open_User_Is_Central_Store = Convert.ToBoolean(UserInfo.Rows[0]["IS_CENTRAL_STORE"]);

                BASE._open_User_PersonnelID = UserInfo.Rows[0]["PersonnelID"] as int?;
                BASE._open_User_MainDeptID = UserInfo.Rows[0]["MainDeptID"] as int?;
                BASE._open_User_SubDeptID = UserInfo.Rows[0]["SubDeptID"] as int?;

                BASE._open_User_ID = UserInfo.Rows[0]["USER_ID"].ToString();
                BASE._open_User_Password = AL.Txt_Pass;
                BASE._open_User_Type = UserType;
                TempData["UserName"] = AL.Txt_User;

                if (AL.DEVICE_TOKEN != null && AL.DEVICE_TOKEN.Length > 0 && AL.ANDROID_ID != null && AL.ANDROID_ID.Length > 0)
                {
                    BASE._AndroidDBOps.insertUsersDeviceToken(AL.DEVICE_TOKEN, BASE._open_User_ID, AL.ANDROID_ID);
                }

                bool IsMobileCheck = System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;

                DataSet DS = BASE._UserPreferences_DBOps.GetSelectedScreens_DataView();
                DataTable DT_SelectedScreensDesktop = DS.Tables[1];
                DataTable DT_SelectedScreensMobile = DS.Tables[2];

                if (IsMobileCheck)
                {
                    if (DT_SelectedScreensMobile != null && DT_SelectedScreensMobile.Rows.Count > 0)
                    {
                        BASE._List_Of_FullData_Screen = DT_SelectedScreensMobile.AsEnumerable().Select(p => p.Field<string>("SELECTED_SCREEN_ID")).ToList();
                    }
                }
                else
                {
                    if (DT_SelectedScreensDesktop != null && DT_SelectedScreensDesktop.Rows.Count > 0)
                    {
                        BASE._List_Of_FullData_Screen = DT_SelectedScreensDesktop.AsEnumerable().Select(p => p.Field<string>("SELECTED_SCREEN_ID")).ToList();
                    }
                }

                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                return Json(new { result = true, Guid = MyGuid, androidID = AL.ANDROID_ID, deviceToken = AL.DEVICE_TOKEN, RedirectToAndroid = AL.RedirectToAndroid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false, message = "Incorrect Password . . . !" }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpGet]
        public ActionResult UnloadCookie()
        {
            Response.Cookies.Remove("Frm_Auditor_Cookies");
            HttpCookie Frm_Auditor_Cookies = new HttpCookie("Frm_Auditor_Cookies");
            Frm_Auditor_Cookies.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(Frm_Auditor_Cookies);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Auditor Center Institute Grid Selection
        public ActionResult Frm_COD_Selection_Ins_Auditor()
        {
            ViewBag.lbl_User_ID = BASE._open_User_ID;
            DataTable Final_Data = BASE._CenterDBOps.GetIns_List(Common_Lib.RealTimeService.ClientScreen.Start_Auditor_Login);
            if (Final_Data == null)
            {
                return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
            }
            InstituteData = Final_Data;
            return View(InstituteData);
        }
        public ActionResult Frm_COD_Selection_Ins_Auditor_Grid(string command)
        {
            if (command == "REFRESH" || InstituteData == null)
            {
                DataTable Final_Data = BASE._CenterDBOps.GetIns_List(Common_Lib.RealTimeService.ClientScreen.Start_Auditor_Login);
                InstituteData = Final_Data;
            }
            return View(InstituteData);
        }
        public void SessionClear_SelectIns()
        {
            ClearBaseSession("_ChangeInstitute");
        }
        #endregion

        #region Form Login Navigation
        //[HttpPost]
        public ActionResult Frm_Login_Post(Center_Ins_List center_Ins_Model, string UserName, string Password, string SessionGUID, bool chKRememberMe = false)
        {
            try
            {
                DataTable UserInfo = null;

                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;

                try
                {
                    BASE._open_User_ID = UserName;
                    // Proper fix for Bug #6068
                    UserInfo = BASE._ClientUserDBOps.GetCenterUserInfo(center_Ins_Model.CEN_ID, UserName);
                    if ((UserInfo == null))
                    {
                        //return Json(new { result = false, message = "Sorry! You seem to have entered Invalid User ID" }, JsonRequestBehavior.AllowGet);
                        return Json(new { result = false, message = "Sorry! You are denied login by Madhuban.Some Administrative / Audit Activity is under process." }, JsonRequestBehavior.AllowGet);
                    }

                    if ((UserInfo.Rows.Count <= 0))
                    {
                        return Json(new { result = false, message = "Incorrect Username (Superusers try Auditor Login) . . . !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (chKRememberMe == true)
                    {
                        BASE._open_User_Remember = true;
                        int timeout = chKRememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.                                                          
                        HttpCookie Frm_Login_Cookies = new HttpCookie("Frm_Login_Cookies");
                        Response.Cookies.Remove("Frm_Login_Cookies");
                        Response.Cookies.Add(Frm_Login_Cookies);
                        Frm_Login_Cookies.Values.Add("UserName", UserName);
                        Frm_Login_Cookies.Values.Add("Password", Password);
                        Frm_Login_Cookies.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
                        Frm_Login_Cookies.HttpOnly = true; // cookie not available in javascript.
                    }
                    else
                    {
                        if (Request.Cookies["Frm_Login_Cookies"] != null)
                        {
                            Response.Cookies.Remove("Frm_Login_Cookies");
                            HttpCookie Frm_Login_Cookies = new HttpCookie("Frm_Login_Cookies");
                            Frm_Login_Cookies.Expires = DateTime.Now.AddDays(-1d);
                            Response.Cookies.Add(Frm_Login_Cookies);
                        }
                        BASE._open_User_Remember = false;
                    }
                    BASE._open_User_User_Is_Admin = (Boolean)(UserInfo.Rows[0]["USER_IS_ADMIN"]);
                    BASE._open_User_Self_Posted = (Boolean)(UserInfo.Rows[0]["SELFPOSTED"]);
                    BASE._open_User_Is_Central_Store = Convert.ToBoolean(UserInfo.Rows[0]["IS_CENTRAL_STORE"]);
                    BASE._open_User_PersonnelID = UserInfo.Rows[0]["PersonnelID"] as int?;
                    BASE._open_User_MainDeptID = UserInfo.Rows[0]["MainDeptID"] as int?;
                    BASE._open_User_SubDeptID = UserInfo.Rows[0]["SubDeptID"] as int?;
                    BASE._open_User_ID = UserName;
                    BASE._open_User_Password = Password;
                    BASE._open_User_Type = UserInfo.Rows[0]["USER_ROLE_ID"].ToString();
                    BASE._open_Cen_ID = Convert.ToInt16(center_Ins_Model.CEN_ID);
                    BASE._open_Cen_Rec_ID = center_Ins_Model.CEN_REC_ID;
                    BASE._open_Cen_ID_Main = Convert.ToInt32(center_Ins_Model.CEN_ID_MAIN);
                    BASE._open_Cen_Name = center_Ins_Model.CEN_NAME_X;
                    BASE._open_PAD_No_Main = center_Ins_Model.CEN_PAD_NO_MAIN;
                    BASE._open_PAD_No = center_Ins_Model.CEN_PAD_NO;
                    BASE._open_Ins_ID = center_Ins_Model.INS_ID;
                    BASE._open_Ins_Name = center_Ins_Model.INS_NAME;
                    BASE._open_Ins_Short = center_Ins_Model.INS_SHORT;
                    BASE._open_Year_ID = Convert.ToInt32(center_Ins_Model.COD_YEAR_ID);
                    BASE._open_Year_Name = center_Ins_Model.COD_YEAR_NAME;
                    BASE.OnChangeCenter_Year();
                    BASE._open_Year_Sdt = Convert.ToDateTime(center_Ins_Model.COD_YEAR_SDT);
                    BASE._open_Year_Edt = Convert.ToDateTime(center_Ins_Model.COD_YEAR_EDT);
                    BASE._open_Trans_DB = "COD_" + center_Ins_Model.INS_ID + "_" + center_Ins_Model.CEN_ID + "_" + center_Ins_Model.COD_YEAR_ID + ".COT";
                    BASE._open_UID_No = center_Ins_Model.CEN_UID;
                    BASE._open_Zone_ID = center_Ins_Model.CEN_ZONE_ID;
                    BASE._open_Zone_SUB_ID = center_Ins_Model.CEN_ZONE_SUB_ID;
                    BASE._open_Year_Acc_Type = center_Ins_Model.CEN_ACC_TYPE_ID;

                    if (center_Ins_Model.DEVICE_TOKEN != null && center_Ins_Model.DEVICE_TOKEN.Length > 0 && center_Ins_Model.ANDROID_ID != null && center_Ins_Model.ANDROID_ID.Length > 0)
                    {
                        BASE._AndroidDBOps.insertUsersDeviceToken(center_Ins_Model.DEVICE_TOKEN, UserName, center_Ins_Model.ANDROID_ID);
                    }

                    bool IsMobileCheck = System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;

                    DataSet DS = BASE._UserPreferences_DBOps.GetSelectedScreens_DataView();
                    DataTable DT_SelectedScreensDesktop = DS.Tables[1];
                    DataTable DT_SelectedScreensMobile = DS.Tables[2];

                    if (IsMobileCheck)
                    {
                        if (DT_SelectedScreensMobile != null && DT_SelectedScreensMobile.Rows.Count > 0)
                        {
                            BASE._List_Of_FullData_Screen = DT_SelectedScreensMobile.AsEnumerable().Select(p => p.Field<string>("SELECTED_SCREEN_ID")).ToList();
                        }
                    }
                    else
                    {
                        if (DT_SelectedScreensDesktop != null && DT_SelectedScreensDesktop.Rows.Count > 0)
                        {
                            BASE._List_Of_FullData_Screen = DT_SelectedScreensDesktop.AsEnumerable().Select(p => p.Field<string>("SELECTED_SCREEN_ID")).ToList();
                        }
                    }

                    BASE._Completed_Year_Count = 0;
                    BASE._IsVolumeCenter = BASE._CODDBOps.CheckVolumeCenter();
                    DataSet _EventID_DS = BASE._CenterDBOps.Get_Base_OpenEventId();
                    if ((_EventID_DS.Tables[1].Rows.Count > 0))
                    {
                        BASE._IsUnderAudit = true;
                    }
                    else
                    {
                        BASE._IsUnderAudit = false;
                    }
                    DataTable UnAuditedYear = BASE._CODDBOps.GetUnAuditedFinalYears();
                    if (!(UnAuditedYear == null))
                    {
                        if ((UnAuditedYear.Rows.Count > 0))
                        {
                            if ((Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]) > BASE._open_Year_ID))
                            {
                                BASE._next_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);
                            }
                            else
                            {
                                BASE._prev_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);
                            }

                        }

                        if ((UnAuditedYear.Rows.Count > 1))
                        {
                            if ((Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]) > BASE._open_Year_ID))
                            {
                                BASE._next_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);
                            }
                            else
                            {
                                BASE._prev_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);
                            }

                        }

                    }

                    BASE._Completed_Year_Count = Convert.ToInt32(BASE._CODDBOps.GetCompletedtYearCount());
                    DataTable SelCenters = BASE._CenterDBOps.GetChildCenterList(center_Ins_Model.CEN_PAD_NO_MAIN);
                    if ((SelCenters == null))
                    {
                        return Json(new { result = false, message = "Error!" }, JsonRequestBehavior.AllowGet);
                    }

                    string SelectedCentre = "";
                    foreach (DataRow xRow in SelCenters.Rows)
                    {
                        SelectedCentre = (SelectedCentre + ("\'"
                                    + (xRow["CEN_ID"].ToString() + "\',")));
                    }

                    if ((SelectedCentre.Trim().Length > 0))
                    {
                        SelectedCentre = (SelectedCentre.Trim().EndsWith(",") ? SelectedCentre.Trim().ToString().Substring(0, (SelectedCentre.Trim().Length - 1)) : SelectedCentre.Trim().ToString());
                    }

                    BASE._open_Cen_ID_Child = SelectedCentre;

                }
                catch (System.Exception e)
                {
                    return Json(new { result = false, message = e.Message }, JsonRequestBehavior.AllowGet);
                    BASE._open_User_ID = "";
                }



                string actualPassword = UserInfo.Rows[0]["USER_PWD"].ToString();
                string UserType = UserInfo.Rows[0]["USER_ROLE_ID"].ToString();
                string Txt_User = UserInfo.Rows[0]["USER_ID"].ToString();
                BASE._open_User_ID = Txt_User.Trim();
                if ((actualPassword == null))
                {
                    return Json(new { result = false, message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                else if ((BASE.Encrypt(Password.ToString().ToUpper()) == actualPassword.ToString().ToUpper()))
                {
                    return Json(new { result = true, message = "success", Guid = MyGuid, deviceToken = center_Ins_Model.DEVICE_TOKEN, androidID = center_Ins_Model.ANDROID_ID, RedirectToAndroid = center_Ins_Model.RedirectToAndroid }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = "Incorrect Password . . . !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Frm_Login(string INS_NAME, string CEN_NAME_X, string CEN_NAME, string CEN_PAD_NO_Main, string CEN_ID_MAIN, string CEN_ZONE_ID, string CEN_ZONE_SUB_ID, string CEN_UID, string CEN_PAD_NO, string CEN_ACC_TYPE_ID, string CEN_ID, string COD_YEAR_EDT, string COD_YEAR_ID, string COD_YEAR_NAME, string CEN_REC_ID, string COD_YEAR_SDT, string INS_ID, string INS_SHORT, string IS_VOLUME)
        {
            try
            {
                Center_Ins_List SelectedCenter = new Center_Ins_List();
                try
                {
                    TempData["IschKRememberMe"] = false;
                    if (Request.Cookies["Frm_Login_Cookies"] != null)
                    {
                        HttpCookie getCookie = Request.Cookies.Get("Frm_Login_Cookies");
                        SelectedCenter.UserName = getCookie.Values["UserName"].ToString();
                        SelectedCenter.Password = getCookie.Values["Password"].ToString();
                        TempData["IschKRememberMe"] = true;
                    }
                    DateTime COD_YEAR_EDT_Dt = Convert.ToDateTime(COD_YEAR_EDT.Split(' ')[0].ToString());
                    DateTime COD_YEAR_SDT_Dt = Convert.ToDateTime(COD_YEAR_SDT.Split(' ')[0].ToString());

                    string EDT = COD_YEAR_EDT_Dt.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string SDT = COD_YEAR_SDT_Dt.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

                    SelectedCenter.CEN_NAME_X = CEN_NAME_X;
                    SelectedCenter.CEN_UID = CEN_UID;
                    SelectedCenter.CEN_PAD_NO = CEN_PAD_NO;
                    SelectedCenter.CEN_ACC_TYPE_ID = CEN_ACC_TYPE_ID;
                    SelectedCenter.CEN_ID = CEN_ID;
                    SelectedCenter.COD_YEAR_EDT = EDT;
                    SelectedCenter.COD_YEAR_ID = COD_YEAR_ID;
                    SelectedCenter.COD_YEAR_NAME = COD_YEAR_NAME;
                    SelectedCenter.CEN_REC_ID = CEN_REC_ID;
                    SelectedCenter.COD_YEAR_SDT = SDT;
                    SelectedCenter.INS_ID = INS_ID;
                    SelectedCenter.INS_NAME = INS_NAME;
                    SelectedCenter.INS_SHORT = INS_SHORT;
                    SelectedCenter.CEN_PAD_NO_MAIN = CEN_PAD_NO_Main;
                    SelectedCenter.CEN_NAME = CEN_NAME;
                    SelectedCenter.CEN_ID_MAIN = CEN_ID_MAIN;
                    SelectedCenter.CEN_ZONE_ID = CEN_ZONE_ID;
                    SelectedCenter.CEN_ZONE_SUB_ID = CEN_ZONE_SUB_ID;
                    SelectedCenter.IS_VOLUME = IS_VOLUME;
                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        message = message,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                return View(SelectedCenter);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Auditor Center Selection
        public ActionResult Frm_COD_Selection_Auditor(string xIns_ID = null, string xINS_NAME = null, string xINS_SHORT = null, string PopupID = "")
        {
            try
            {
                ViewBag.OpenUserType = BASE._open_User_Type.ToUpper();
                if (xIns_ID == null)
                {
                    xIns_ID = BASE._open_Ins_ID;
                    xINS_NAME = BASE._open_Ins_Name;
                    xINS_SHORT = BASE._open_Ins_Short;
                }
                ViewBag.lbl_Instt_ID = xINS_NAME;
                ViewBag.xIns_ID = xIns_ID;
                ViewBag.xINS_NAME = xINS_NAME;
                ViewBag.xINS_SHORT = xINS_SHORT;
                ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "popup_frm_COD_Selection_Auditor";
                var Final_Data = BASE._CenterDBOps.GetCenterListByAuditor_Instt(BASE._open_User_ID, xIns_ID);
                if (Final_Data == null)
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                CenterData = Final_Data;
                return PartialView(CenterData);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_COD_Selection_Auditor_Grid(string command, string xIns_ID = null, string txtCenter = null, bool SearchClick = false)
        {
            var Final_Data = new DataTable();
            if (command == "REFRESH" || CenterData == null)
            {
                if (SearchClick == true)
                {
                    Final_Data = BASE._CenterDBOps.GetCenterListByPAD_Name(txtCenter, xIns_ID);
                }
                else
                {
                    Final_Data = BASE._CenterDBOps.GetCenterListByAuditor_Instt(BASE._open_User_ID, xIns_ID);
                }
                CenterData = Final_Data;
            }
            return View(CenterData);
        }
        public ActionResult Frm_COD_Selection_Auditor_Submit(int CEN_ID, string CEN_NAME, int COD_YEAR_ID, string COD_YEAR_NAME, string COD_YEAR_SDT, string COD_YEAR_EDT, string CEN_UID, string REC_ID, string CEN_ID_MAIN, string CEN_PAD_NO_MAIN, string CEN_PAD_NO, string CEN_ZONE_ID, string CEN_ZONE_SUB_ID, string CEN_ACC_TYPE_ID, string IS_VOLUME, string AuditStatus, bool IsConfirmAuditStatus = false)
        {
            try
            {
                if (AuditStatus == "Returned")
                {
                    // Offer Resumtion of Audit                
                    if (IsConfirmAuditStatus)
                    {
                        if (BASE._CenterDBOps.ResumeAudit(CEN_ID, COD_YEAR_ID))
                        {
                            return Json(new { result = false, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { result = false, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if ((BASE._open_User_Type != "SUPERUSER"))
                    {
                        return Json(new { result = false, message = "Sorry !  Not allowed to Login Center without Resuming Audit....!" }, JsonRequestBehavior.AllowGet);
                    }
                }

                BASE._CenterDBOps.LogOut();
                ChangeCenter(CEN_ID, CEN_NAME, COD_YEAR_ID, COD_YEAR_NAME, COD_YEAR_SDT, COD_YEAR_EDT, CEN_UID, REC_ID, CEN_ID_MAIN, CEN_PAD_NO_MAIN, CEN_PAD_NO, CEN_ZONE_ID, CEN_ZONE_SUB_ID, CEN_ACC_TYPE_ID, IS_VOLUME);
                // BASE._prev_Unaudited_YearID = "" : BASE._next_Unaudited_YearID = "" ': BASE._Completed_Year_Count = 0
                return Json(new { result = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangeCenter(int CenID, string CenName, int YearID, string YearName, string YrStDate, string YrEndDate, string UID, string CenRecID, string CenIDMain, string PadMain, string PADNo, string ZoneID, string SubZoneID, string AccTypeID, string isVolume, string XINS_ID = null, string XINS_NAME = null, string xINS_SHORT = null)
        {
            try
            {
                cenID = BASE._open_Cen_ID;
                yearID = BASE._open_Year_ID;
                CompletedYearCount = BASE._Completed_Year_Count;
                BASE._CenterDBOps.LogOut();
                BASE._open_Cen_ID = CenID;
                BASE._open_Year_ID = YearID;
                BASE.OnChangeCenter_Year();
                BASE._Completed_Year_Count = 0;
                DataTable UnAuditedYear = BASE._CODDBOps.GetUnAuditedFinalYears();
                BASE._Completed_Year_Count = Convert.ToInt32(BASE._CODDBOps.GetCompletedtYearCount());
                DataSet _EventID_DS = BASE._CenterDBOps.Get_Base_OpenEventId();
                DataTable EventId = _EventID_DS.Tables[0];
                BASE._open_Cen_Name = CenName;
                BASE._open_Year_Name = YearName;
                BASE._open_Year_Sdt = Convert.ToDateTime(YrStDate);//need to do type casting 
                BASE._open_Year_Edt = Convert.ToDateTime(YrEndDate);//need to do type casting
                BASE._open_UID_No = UID;
                BASE._open_Cen_Rec_ID = Convert.ToString(CenRecID);//need to do type casting 
                BASE._open_Cen_ID_Main = Convert.ToInt32(CenIDMain);
                BASE._open_PAD_No_Main = PadMain;
                BASE._open_PAD_No = PADNo;
                BASE._open_Zone_ID = ZoneID;
                BASE._open_Zone_SUB_ID = SubZoneID;
                BASE._open_Year_Acc_Type = AccTypeID;
                BASE._prev_Unaudited_YearID = 0;
                BASE._next_Unaudited_YearID = 0;
                BASE._IsVolumeCenter = isVolume == null ? false : Convert.ToBoolean(Convert.ToInt32(isVolume));
                if (XINS_ID != null)
                {
                    BASE._open_Ins_ID = XINS_ID;
                }
                if (XINS_NAME != null)
                {
                    BASE._open_Ins_Name = XINS_NAME;
                }
                if (xINS_SHORT != null)//redmine Bug #133307 fixed
                {
                    BASE._open_Ins_Short = xINS_SHORT;
                }

                if (UnAuditedYear.Rows.Count > 0)
                {
                    if (Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        BASE._next_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);//need to do type casting
                    }
                    else
                    {
                        BASE._prev_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);//need to do type casting
                    }
                }

                if (UnAuditedYear.Rows.Count > 1)
                {
                    //need to do type casting
                    if (Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        BASE._next_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);//need to do type casting
                    }
                    else
                    {
                        BASE._prev_Unaudited_YearID = Convert.ToInt32(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);
                    }
                }


                if ((EventId.Rows.Count > 0))
                {
                    BASE._open_Event_ID = Convert.ToInt32(EventId.Rows[0][0]);
                }
                else
                {
                    BASE._open_Event_ID = 0;
                }
                if ((_EventID_DS.Tables[1].Rows.Count > 0))
                {
                    BASE._IsUnderAudit = true;
                }
                else
                {
                    BASE._IsUnderAudit = false;
                }
                BASE._SessionDictionary.Clear();
                BASE._RightsDictionary.Clear();
                clearSessionVariable();
                if(BASE._open_User_Type == "SUPERUSER")                    
                {
                    Session.Timeout = 4320;
                }
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BASE._open_Cen_ID = (int)cenID;
                BASE._open_Year_ID = (int)yearID;
                BASE._Completed_Year_Count = (int)CompletedYearCount;
                clearSessionVariable();
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public void clearSessionVariable()
        {
            BASE._SessionDictionary.Remove("CenID_ChangeInstitute");
            BASE._SessionDictionary.Remove("YearID_ChangeInstitute");
            BASE._SessionDictionary.Remove("CompletedYearCount_ChangeInstitute");
        }
        public bool CheckResumeAudit(int CEN_ID, int COD_YEAR_ID)
        {
            return BASE._CenterDBOps.ResumeAudit(CEN_ID, COD_YEAR_ID);
        }
        #region Logout
        public ActionResult Logout()
        {
            SessionGUID = Request.QueryString["SessionGUID"];
            string ReferrerPath = "";
            if (Request.UrlReferrer != null)
            {
               ReferrerPath = Request.UrlReferrer.AbsolutePath;
            } 
            string urlPath = Request.Url.AbsolutePath;
            string LoginPath = ConfigurationManager.AppSettings["DefaultPath"];
            if (ReferrerPath == "/Start/Home/Page" || urlPath== "/Start/Home/Page")  
            {
                string cenID = "";
                if (BASE._open_Cen_ID == 0)
                {
                    if (ReferrerPath == "/Start/Home/Page")
                    {
                        cenID = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["CenID"];
                    }
                    else
                    {
                        cenID = HttpUtility.ParseQueryString(Request.Url.Query)["CenID"];
                    }
                }
                else 
                {
                    cenID=BASE._open_Cen_ID.ToString();
                }
               // LoginPath= ConfigurationManager.AppSettings["GenericPath"]+ "?InsID="+BASE._open_Ins_ID+"&CenID="+BASE._open_Cen_ID;                
                LoginPath= ConfigurationManager.AppSettings["GenericPath"]+ "?CenID="+ cenID;                
            }
            if (!string.IsNullOrEmpty(SessionGUID))
            {
                List<BaseModel> ListOfBase = new List<BaseModel>();

                if (HttpContext.Session["BASEClass"] == null)
                {
                    BASE = new Common_Lib.Common();
                    BASE.Get_Configure_Setting();
                }
                else
                {
                    try
                    {
                        ListOfBase = HttpContext.Session["BASEClass"] as List<BaseModel>;
                        var basedata = ListOfBase.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                        if (basedata == null)
                        {
                            Session.RemoveAll(); Session.Clear();
                            Response.Redirect(LoginPath);
                        }
                        else
                        {
                            BASE._CenterDBOps.LogOut();
                            ListOfBase.Remove(basedata);
                            Session.RemoveAll(); Session.Clear();
                            HttpContext.Session["BASEClass"] = ListOfBase;
                        }
                    }
                    catch (Exception e)
                    {
                        BASE._CenterDBOps.LogOut();
                        Response.Redirect(LoginPath);
                    }
                }
            }

            Response.Redirect(LoginPath);
            return null;
        }
        #endregion
        #region Generic Login
        [HttpGet]
        public ActionResult Frm_Generic_Login(string CenID = "", string InsID = "")        
        {
            ViewData["InsID"] = InsID;
            ViewData["CenID"] = CenID;
            return View();
        }
        [HttpPost]
        public ActionResult Frm_Generic_Login(string cenid, string pwd, string username)
        {
            try 
            {
                Center_Ins_List center_details = new Center_Ins_List();
                DataTable details = BASE._CenterDBOps.GetCenterDetails_Login(cenid);
                if (details == null || details.Rows.Count == 0)
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                center_details.CEN_ID = cenid;
                center_details.CEN_REC_ID = details.Rows[0]["CEN_REC_ID"].ToString();
                center_details.CEN_ID_MAIN = details.Rows[0]["main_cen_id"].ToString();
                center_details.CEN_NAME_X = details.Rows[0]["CEN_NAME"].ToString();
                center_details.CEN_PAD_NO_MAIN = details.Rows[0]["main_cen_pad_no"].ToString();
                center_details.CEN_PAD_NO = details.Rows[0]["CEN_PAD_NO"].ToString();
                center_details.INS_ID = details.Rows[0]["INS_ID"].ToString();
                center_details.INS_NAME = details.Rows[0]["INS_NAME"].ToString();
                center_details.INS_SHORT = details.Rows[0]["INS_SHORT"].ToString();
                center_details.COD_YEAR_ID = details.Rows[0]["COD_YEAR_ID"].ToString();
                center_details.COD_YEAR_NAME = details.Rows[0]["COD_YEAR_NAME"].ToString();
                center_details.COD_YEAR_SDT = details.Rows[0]["COD_YEAR_SDT"].ToString();
                center_details.COD_YEAR_EDT = details.Rows[0]["COD_YEAR_EDT"].ToString();
                center_details.CEN_UID = details.Rows[0]["CEN_UID"].ToString();
                center_details.CEN_ZONE_ID = details.Rows[0]["MAIN_CEN_ZONE_ID"].ToString();
                center_details.CEN_ZONE_SUB_ID = details.Rows[0]["MAIN_CEN_ZONE_SUB_ID"].ToString();
                center_details.CEN_ACC_TYPE_ID = details.Rows[0]["CEN_ACC_TYPE_ID"].ToString();

                return Frm_Login_Post(center_details, username, pwd, null);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetInstitutes(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(BASE._CenterDBOps.GetIns_List(Common_Lib.RealTimeService.ClientScreen.Start_Auditor_Login)), "application/json");
        }
        public ActionResult GetCentres(DataSourceLoadOptions loadOptions,string InsID)
        {
            return Content(JsonConvert.SerializeObject(BASE._CenterDBOps.GetCenter_ByInstitute(InsID)), "application/json");
        }
        #endregion
    }
}