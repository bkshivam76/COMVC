using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using System.Web.Configuration;
using Common_Lib;
using ConnectOneMVC.Areas.Help.Models;
using DevExpress.XtraReports.Design;
using DevExpress.Utils.Extensions;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Helper;
using System.IO;
using System.Web.Hosting;


namespace ConnectOneMVC.Areas.Help.Controllers
{
    public class AndroidController : BaseController
    {
        #region <--Android HomePage Landing-->

        public ActionResult Android_HomePage(string androidID = "", string deviceToken= "")
        {
            refreshDeviceToken(androidID, deviceToken); //this is to ensure that if user uninstalls the app. and then reinstall or he clears the data we will capture his new device token & refresh it..
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            ViewBag.Connectone_Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.theme = "blue";
            DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
            if (d1.Rows.Count > 0) 
            {
                if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)                 
                {
                    ViewBag.theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                }
            }
            
            // MODULES VISIBILITY

            bool showSparcModule = true;
            bool showAccountsModule = true;
            bool showServiceModule = true;

            string Sparc_Project_Cen_ID = WebConfigurationManager.AppSettings["SpARC_Project_Cen_ID"].ToString();

            DataTable dt_registeredUsersSparc = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "SPARC", Sparc_Project_Cen_ID);
            DataTable dt_registeredUsersC1 = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "CONNECTONE");
            DataTable dt_registeredUsersServices = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "NON_SPARC_SERVICE_USERS", Sparc_Project_Cen_ID);

            if (dt_registeredUsersSparc == null || dt_registeredUsersSparc.Rows.Count == 0) { showSparcModule = false; } else { showSparcModule = true; }
            if (dt_registeredUsersC1 == null || dt_registeredUsersC1.Rows.Count == 0) { showAccountsModule = false; } else { showAccountsModule = true; }
            if (dt_registeredUsersServices == null || dt_registeredUsersServices.Rows.Count == 0) { showServiceModule = false; } else { showServiceModule = true; }

            if (showSparcModule == false && showAccountsModule == false && showServiceModule == false)
            {
                showSparcModule = true;
                showAccountsModule = true;
                showServiceModule = true;
            }

            if (showSparcModule == false && (showAccountsModule == true || showServiceModule == true))
            {
                showSparcModule = false;
                showAccountsModule = true;
                showServiceModule = true;
            }

            ViewBag.showSparcModule = showSparcModule;
            ViewBag.showAccountsModule = showAccountsModule;
            ViewBag.showServiceModule = showServiceModule;

            return View();
        }
        public ActionResult Android_ORC_HomePage(string androidID = "", string deviceToken = "")
        {
            refreshDeviceToken(androidID, deviceToken); //this is to ensure that if user uninstalls the app. and then reinstall or he clears the data we will capture his new device token & refresh it..
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            ViewBag.Connectone_Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.theme = "blue";
            DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
            if (d1.Rows.Count > 0)
            {
                if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                {
                    ViewBag.theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                }
            }

            return View();
        }
        public ActionResult Android_ShantiSarovar_HomePage(string androidID = "", string deviceToken = "")
        {
            refreshDeviceToken(androidID, deviceToken); //this is to ensure that if user uninstalls the app. and then reinstall or he clears the data we will capture his new device token & refresh it..
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            ViewBag.Connectone_Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.theme = "blue";
            DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
            if (d1.Rows.Count > 0)
            {
                if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                {
                    ViewBag.theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                }
            }

            return View();
        }

        public ActionResult Android_SPARC_App_HomePage(string androidID = "", string deviceToken = "")
        {
            refreshDeviceToken(androidID, deviceToken); //this is to ensure that if user uninstalls the app. and then reinstall or he clears the data we will capture his new device token & refresh it..
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            ViewBag.Connectone_Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.theme = "blue";
            DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
            if (d1.Rows.Count > 0)
            {
                if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                {
                    ViewBag.theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                }
            }

            return View();
        }
        public ActionResult Android_C1_HomePage(string androidID = "", string deviceToken = "",string user_id="",string theme="")
        {
            C1HomePAge model = new C1HomePAge();
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();         
            DataTable dt_registeredUsersC1 = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "CONNECTONE");
            //if (dt_registeredUsersC1.Rows.Count == 0)
            //{
            //    string Login_URL = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString() + "/Start/Login/Frm_COD_Selection?deviceToken=" + deviceToken + "&androidID=" + androidID;
            //    return new RedirectResult(Login_URL);
            //}            
            if (dt_registeredUsersC1 != null && dt_registeredUsersC1.Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(user_id))
                {
                    DataRow[] defaultUSer = dt_registeredUsersC1.Select("DEFAULT_USER=1");
                    if (defaultUSer != null && defaultUSer.Length > 0)
                    {
                        string default_user_ID = (dt_registeredUsersC1.Select("DEFAULT_USER=1"))[0]["SERVICE_USER_ID"].ToString();
                        //string default_user_ID = dt_registeredUsersC1.AsEnumerable().Where(s => s.Field<int>("DEFAULT_USER") == 1).Select(s => s.Field<string>("SERVICE_USER_ID"))[0];
                        user_id = default_user_ID;
                    }
                }
                model.ActiveUser = dt_registeredUsersC1.Select("SERVICE_USER_ID='" + user_id + "'");
                model.NonActiveUser = dt_registeredUsersC1.Select("SERVICE_USER_ID<>'" + user_id + "'");
                if (string.IsNullOrWhiteSpace(theme) == true) 
                {
                    // theme= dt_registeredUsersC1.Rows[0]["ANDROID_THEME"].ToString();
                    DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
                    if (d1.Rows.Count > 0)
                    {
                        if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                        {
                            theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                        }
                    }
                }
            }
            model.Projects=new DataTable();
            ViewBag.theme = string.IsNullOrWhiteSpace(theme)?"blue":theme;
            return View("Android_C1_HomePage",model);
        }  

        public ActionResult Android_ServiceModule(string androidID = "", string deviceToken = "")
        {
            string Service_Module = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString() + "?browser=true";

            return new RedirectResult(Service_Module);
        }
        public ActionResult Android_Services_HomePage(string androidID = "", string deviceToken = "", string user_id = "", string user_ab_id = "",string theme = "")
        {
            C1HomePAge model = new C1HomePAge();
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            DataTable dt_registeredUsersServices = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "SERVICE");
            if (dt_registeredUsersServices != null && dt_registeredUsersServices.Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(user_id))
                {
                    DataRow[] defaultUSer = dt_registeredUsersServices.Select("DEFAULT_USER=1");
                    if (defaultUSer != null && defaultUSer.Length > 0)
                    {
                        string default_user_ID = (dt_registeredUsersServices.Select("DEFAULT_USER=1"))[0]["SERVICE_USER_ID"].ToString();
                        //string default_user_ID = dt_registeredUsersC1.AsEnumerable().Where(s => s.Field<int>("DEFAULT_USER") == 1).Select(s => s.Field<string>("SERVICE_USER_ID"))[0];
                        user_id = default_user_ID;
                    }
                }
                model.ActiveUser = dt_registeredUsersServices.Select("SERVICE_USER_ID='" + user_id + "'");
                model.NonActiveUser = dt_registeredUsersServices.Select("SERVICE_USER_ID<>'" + user_id + "'");
                if (string.IsNullOrWhiteSpace(theme) == true)
                {
                    //theme = dt_registeredUsersServices.Rows[0]["ANDROID_THEME"].ToString();
                    DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
                    if (d1.Rows.Count > 0)
                    {
                        if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                        {
                            theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                        }
                    }
                }
            }
            model.Projects = new DataTable();
            ViewBag.theme = string.IsNullOrWhiteSpace(theme) ? "blue" : theme;
            return View("Android_Services_HomePage", model);
        }
        public ActionResult Android_Sparc_HomePage(string androidID = "", string deviceToken = "", string user_id = "",string user_ab_id="",string theme = "")
        {
            SparcHomePAge model = new SparcHomePAge();
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();     
            string Sparc_Project_Cen_ID = WebConfigurationManager.AppSettings["SpARC_Project_Cen_ID"].ToString();
            DataTable dt_registeredUsersSparc = BASE._AndroidDBOps.get_registeredUsersForModule(androidID, "SPARC", Sparc_Project_Cen_ID);

            //DataSet ds_subjectsAndProjects = BASE._AndroidDBOps.Get_sparcSubjectsAndProjects(androidID);
            //DataTable dt_registeredSubjects = ds_subjectsAndProjects.Tables[0];
            //DataTable dt_MappedProjects = ds_subjectsAndProjects.Tables[1];

            if (dt_registeredUsersSparc.Rows.Count == 0)
            {
                string Service_Module_Login_URL = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString() + "/Events/Login/loginpartialview?firebaseDeviceToken=" + deviceToken + "&androidID=" + androidID + "&androidModule=SPARC";
                return new RedirectResult(Service_Module_Login_URL);
            }

            if (string.IsNullOrWhiteSpace(user_id))
            {
                DataRow[] defaultUSer = dt_registeredUsersSparc.Select("DEFAULT_USER=1");
                if (defaultUSer != null && defaultUSer.Length > 0)
                {
                    user_id = (dt_registeredUsersSparc.Select("DEFAULT_USER=1"))[0]["SERVICE_USER_ID"].ToString();
                }
                else 
                {
                    user_id= dt_registeredUsersSparc.Rows[0]["SERVICE_USER_ID"].ToString();
                }
                if (string.IsNullOrWhiteSpace(theme) == true)
                {
                    // theme = dt_registeredUsersSparc.Rows[0]["ANDROID_THEME"].ToString();
                    DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
                    if (d1.Rows.Count > 0)
                    {
                        if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                        {
                            theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                        }
                    }
                }
            }

            
            model.Projects = BASE._AndroidDBOps.get_projectsMappedToSubject(user_id, Sparc_Project_Cen_ID);
            model.ActiveUser = dt_registeredUsersSparc.Select("SERVICE_USER_ID='" + user_id+"'");
            model.NonActiveUser = dt_registeredUsersSparc.Select("SERVICE_USER_ID<>'" + user_id+"'");
            ViewBag.theme = string.IsNullOrWhiteSpace(theme) ? "blue" : theme;

            return View("Android_Sparc_HomePage", model);
        }

        public ActionResult refreshDeviceTokenPublic(string androidID = "", string deviceToken = "")
        {
            string domainToRedirect = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString() + "/Help/Android/refreshDeviceToken?androidID=" + androidID + "&deviceToken=" + deviceToken;
            return new RedirectResult(domainToRedirect);
        }

        public ActionResult refreshDeviceToken(string androidID = "", string deviceToken = "")
        {
            BASE._AndroidDBOps.refreshDeviceToken(androidID, deviceToken);

            return new EmptyResult();
        }
        public ActionResult UnregisterAndroidUser(string REGISTRATION_TBL_Rec_ID) 
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try 
            {
                if (BASE._AndroidDBOps.unregisterAndroidUser(REGISTRATION_TBL_Rec_ID))          
                {
                    jsonParam.message = "User Unregistered Successfully";
                    jsonParam.title = "Information";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }       
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        
        }
        public ActionResult SetDefaultAndroidUser(string REGISTRATION_TBL_Rec_ID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._AndroidDBOps.update_AndroidDefaultUser(REGISTRATION_TBL_Rec_ID))
                {
                    jsonParam.message = "User Set As Default Successfully";
                    jsonParam.title = "Information";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateTheme(string theme, string androidID) 
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try 
            {
                if (BASE._AndroidDBOps.UpdateAndroidTheme(theme, androidID)) 
                {
                    jsonParam.message = "Theme Set Successfully";
                    jsonParam.title = "Information";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Android_Sewa_Report(string androidID = "", string deviceToken = "", string user_id = "", string user_ab_id = "", string theme = "",string type="") 
        {
            ViewBag.androidID = androidID;
            ViewBag.deviceToken = deviceToken;
            ViewBag.Domain = WebConfigurationManager.AppSettings["AndroidConnectOneDomain"].ToString();
            ViewBag.Service_Module_Domain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
            if (string.IsNullOrWhiteSpace(theme) == true)
            {
                // theme = dt_registeredUsersSparc.Rows[0]["ANDROID_THEME"].ToString();
                DataTable d1 = BASE._AndroidDBOps.GetAndroidTheme(androidID);
                if (d1.Rows.Count > 0)
                {
                    if (d1.Rows[0]["ANDROID_THEME"].ToString().Length > 0)
                    {
                        theme = d1.Rows[0]["ANDROID_THEME"].ToString();
                    }
                }
            }
            ViewBag.theme = string.IsNullOrWhiteSpace(theme) ? "blue" : theme;
            ViewBag.type = string.IsNullOrWhiteSpace(type) ? "Centre" : type;
            return View();
        }
        public ActionResult LookUp_Get_Centres(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._AndroidDBOps.GetCentres(true);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_CentresList(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Wings(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._SR_DBOps.GetWings();         
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTabletoGSR_GetWingsList(d1), loadOptions)), "application/json");
        }
        #endregion
        [HttpGet]

        public ActionResult GetNotificationJson()
        {
            string filePath = HostingEnvironment.MapPath("~/Scripts/sample.json");
            // Read the file content
            var jsonContent = System.IO.File.ReadAllText(filePath);
            // Return the content as a JSON response
            return Content(jsonContent, "application/json");
        }
    }
}