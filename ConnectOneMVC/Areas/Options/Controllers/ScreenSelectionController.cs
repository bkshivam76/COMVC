using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using ConnectOneMVC.Areas.Options.Models;

namespace ConnectOneMVC.Areas.Options.Controllers
{
    [CheckLogin]
    public class ScreenSelectionController : BaseController
    {
        public ActionResult Frm_User_Preferences()
        {
            ScreenSelection model = new ScreenSelection();

            int Rad_Acc_Party = 2;            
            if (BASE._prefer_show_acc_party_only.HasValue)
            {
                if (BASE._prefer_show_acc_party_only == true)
                {
                    Rad_Acc_Party = 0;
                }
                else
                {
                    Rad_Acc_Party = 1;
                }
            }
            else
            {
                Rad_Acc_Party = 2;
            }
            ViewBag.Rad_Acc_Party = Rad_Acc_Party;
            ViewBag.Rad_VouchingIndicator = BASE._prefer_show_vouching_indicator;               
            ViewBag.Rad_AttachmentIndicator= BASE._prefer_show_attachment_indicator;

            ViewData["AutoOpenScreenIDs"] = (string[])GetBaseSession("AutoOpenScreenIDs_AutoOpenScreen");

            // Initializing the lists
            model.AllVisibleScreensList = new List<AllVisibleScreens>();
            model.SelectedScreensList_Desktop = new List<SelectedScreens_Desktop>();
            model.SelectedScreensList_Mobile = new List<SelectedScreens_Mobile>();

            //Fetching DataTables
            DataTable DT_VisibleScreens = BASE._Menu_vibilities_Listing;            
            DataSet DS = BASE._UserPreferences_DBOps.GetSelectedScreens_DataView();
            DataTable DT_AllListingScreens = DS.Tables[0];
            DataTable DT_SelectedScreensDesktop = DS.Tables[1];
            DataTable DT_SelectedScreensMobile = DS.Tables[2];

            // Filling the lists
            var JoinResult = (from a in DT_VisibleScreens.AsEnumerable()
                              join b in DT_AllListingScreens.AsEnumerable()
                              on a.Field<string>("MENU_ID") equals b.Field<string>("SCREEN_ID")
                              select new AllVisibleScreens()
                              {
                                  DisplayName = b.Field<string>("DISPLAY_NAME"),
                                  ScreenID = a.Field<string>("MENU_ID")
                              }).ToList();
            model.AllVisibleScreensList.AddRange(JoinResult);

            
            foreach (DataRow row in DT_SelectedScreensDesktop.Rows)
            {
                SelectedScreens_Desktop newdata2 = new SelectedScreens_Desktop();
                newdata2.DisplayName = row[0].ToString();
                newdata2.ScreenID = row[1].ToString();
                model.SelectedScreensList_Desktop.Add(newdata2);
            }

            
            
            foreach (DataRow row in DT_SelectedScreensMobile.Rows)
            {
                SelectedScreens_Mobile newdata3 = new SelectedScreens_Mobile();
                newdata3.DisplayName = row[0].ToString();
                newdata3.ScreenID = row[1].ToString();
                model.SelectedScreensList_Mobile.Add(newdata3);
            }       

            return View(model);
        }
        [HttpPost]
        public ActionResult But_Save_Click(int Rad_Acc_Party,bool VouchingIndicator,bool AttachmentIndicator, string[] ScreenIDs, string DeviceType, SelectedScreens_Desktop[] ScreenIDs_Desktop, SelectedScreens_Mobile[] ScreenIDs_Mobile)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (Rad_Acc_Party == 0)
                {
                    BASE._prefer_show_acc_party_only = true;
                }
                else if (Rad_Acc_Party == 1)
                {
                    BASE._prefer_show_acc_party_only = false;
                }
                else if (Rad_Acc_Party == 2)
                {
                    BASE._prefer_show_acc_party_only = null;
                }
                BASE._prefer_show_vouching_indicator = VouchingIndicator;
                BASE._prefer_show_attachment_indicator = AttachmentIndicator;

                // Details: Database call for inserting the user preferences related to Data View.
                if (BASE._UserPreferences_DBOps.Delete_DataView())
                {
                    if (ScreenIDs_Desktop != null && ScreenIDs_Desktop.Length > 0)
                    {
                        for (int i = 0; i < ScreenIDs_Desktop.Length; i++)
                        {
                            BASE._UserPreferences_DBOps.InsertDataViewOption(ScreenIDs_Desktop[i].ScreenID, "FULL_DATA_DESKTOP", "DESKTOP");
                        }
                    }

                    if (ScreenIDs_Mobile != null && ScreenIDs_Mobile.Length > 0)
                    {
                        for (int i = 0; i < ScreenIDs_Mobile.Length; i++)
                        {
                            BASE._UserPreferences_DBOps.InsertDataViewOption(ScreenIDs_Mobile[i].ScreenID, "FULL_DATA_MOBILE", "MOBILE");
                        }
                    }
                }

                // Details: Refilling the Session variable with fresh preferences.

                BASE._List_Of_FullData_Screen.Clear();

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

                if (BASE._UserPreferences_DBOps.Delete())
                {
                    if (ScreenIDs != null && ScreenIDs.Length > 0)
                    {
                        for (int i = 0; i < ScreenIDs.Length; i++)
                        {
                            BASE._UserPreferences_DBOps.InsertAutoOpenScreen(ScreenIDs[i]);
                        }
                    }
                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = "Success..";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error..";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}