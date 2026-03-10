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
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class ServicePlacesController : BaseController
    {
        // GET: Profile/ServicePlaces
        #region Global Variables
        public List<DbOperations.ServicePlaces.Return_ServicePlace> ServicePlace_ExportData
        {
            get
            {
                return (List<DbOperations.ServicePlaces.Return_ServicePlace>)GetBaseSession("ServicePlace_ExportData_ServicePlaces");
            }
            set
            {
                SetBaseSession("ServicePlace_ExportData_ServicePlaces", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> ServicePlaceInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("ServicePlaceInfo_DetailGrid_Data_ServicePlaces");
            }
            set
            {
                SetBaseSession("ServicePlaceInfo_DetailGrid_Data_ServicePlaces", value);
            }
        }

        public List<PlaceOwners> PlaceOwner_DD_Data
        {
            get { return (List<PlaceOwners>)GetBaseSession("PlaceOwner_DD_Data_ServicePlacesWindow"); }
            set { SetBaseSession("PlaceOwner_DD_Data_ServicePlacesWindow", value); }
        }

        public List<ResponsiblePerson> ResponsiblePerson_DD_Data
        {
            get { return (List<ResponsiblePerson>)GetBaseSession("ResponsiblePerson_DD_Data_ServicePlacesWindow"); }
            set { SetBaseSession("ResponsiblePerson_DD_Data_ServicePlacesWindow", value); }
        }

        #endregion
        public ActionResult Frm_ServicePlace_Info()
        {
            ViewData["Open_Ins_Id_Check"] = BASE._open_Ins_ID;
            ViewData["Open_Ins_Name"] = BASE._open_Ins_Name;
            ServicePlaces_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_ServicePlaces').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_ServicePlaces).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ServicePlaceInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            var SP_Data = BASE._ServPlacesDBOps.GetList();
            if ((SP_Data == null))
            {
                return View();
            }
            else
            {
                ServicePlace_ExportData = SP_Data;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(SP_Data);
            }
        }
        public PartialViewResult Frm_ServicePlace_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ServicePlaces_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            if (ServicePlace_ExportData == null || command == "REFRESH")
            {
                var SP_Data = BASE._ServPlacesDBOps.GetList();
                ServicePlace_ExportData = SP_Data;
            }
            return PartialView("Frm_ServicePlace_Info_Grid", ServicePlace_ExportData);
        }
        public ActionResult Frm_ServicePlace_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.ServicePlaceInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ServicePlaceInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_ServicePlaces);
            ServicePlaceInfo_DetailGrid_Data = _docList;
            Session["ServicePlaceInfo_detailGrid_Data"] = _docList;
            return PartialView(_docList);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ServicePlacesListGrid" + RecID;
            settings.Width = Unit.Percentage(100);
            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "ServicePlacesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["ServicePlaceInfo_detailGrid_Data"];
        }

        [HttpGet]
        public ActionResult Frm_ServicePlace_Window(string Tag = null, string xId = null, string Info_LastEditedOn = null)
        {
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };

            //It returns true or false by checking the rights of "List" in the address book.
            ViewData["ServicePlace_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_ServicePlaces, Rights[i]) && Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }
            ServicePlaces model = new ServicePlaces();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Tag);
            model.Tag = Navigation_Mode_tag;
            model.ActionMethod = model.Tag.ToString();
            model.xID = xId;
            //xId = model.xID;
            model.TitleX_ServicePlace = "Service Place";
            model.Chk_Mon = false;
            model.Chk_Tue = false;
            model.Chk_Wed = false;
            model.Chk_Thu = false;
            model.Chk_Fri = false;
            model.Chk_Sat = false;
            model.Chk_Sun = false;

            Return_Json_Param jsonParam = new Return_Json_Param();
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                model.Text_ServicePlace = "Edit ~ " + model.TitleX_ServicePlace;
                DataTable d1 = BASE._ServPlacesDBOps.GetRecord(model.xID);
                if (d1 == null || d1.Rows.Count == 0)
                {

                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error');HidePopup('Dynamic_Content_popup')</script>");
                }

                // This if condition is to check if entry already changed
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.Tag == Common.Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])))
                        {
                        }
                        else
                        {
                            string message = Messages.RecordChanged("Current ServicePlace", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','ServicePlacesListGrid');</script>");
                        }
                    }
                } // If condition to check if entry already changed or not is completed.

                model.LastEditedOn_ServicePlace = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Txt_Name_ServicePlaces = d1.Rows[0]["SP_SERVICE_PLACE_NAME"].ToString();
                model.Txt_Name_Tag_ServicePlaces = model.Txt_Name_ServicePlaces;
                model.Look_PlaceOwner_ServicePlaces = d1.Rows[0]["SP_PLACEAT_AB_ID"].ToString();

                model.Look_ResponsiblePerson_ServicePlaces = d1.Rows[0]["SP_RESPONSIBLE_PERSON_AB_ID"].ToString();

                model.Txt_Timings_ServicePlaces = Convert.ToDateTime(d1.Rows[0]["SP_TIMING"]);
                model.Txt_OtherDetails_ServicePlaces = d1.Rows[0]["SP_OTHER_DETAIL"].ToString();
                model.Cmb_PlaceType_ServicePlaces = d1.Rows[0]["SP_SERVICE_PLACE_TYPE"].ToString();
                model.cmb_Status_ServicePlaces = d1.Rows[0]["SP_STATUS"].ToString();

                DateTime xDate_ServicePlace;
                xDate_ServicePlace = Convert.ToDateTime(d1.Rows[0]["SP_STARTDATE"]);
                model.Txt_St_Date_ServicePlaces = xDate_ServicePlace;
                if (Convert.IsDBNull(d1.Rows[0]["SP_MAXCAPACITY"]) == false)
                {
                    model.Txt_MaxCapacity_ServicePlaces = Convert.ToInt32(d1.Rows[0]["SP_MAXCAPACITY"]);
                }
                //string WeekDay_ServicePlace = "";
                string[] WeekDay_ServicePlace = d1.Rows[0]["SP_WEEKDAYS"].ToString().Split(',');
                foreach (string day in WeekDay_ServicePlace)
                {
                    if (day == "Mon") { model.Chk_Mon = true; }
                    if (day == "Tue") { model.Chk_Tue = true; }
                    if (day == "Wed") { model.Chk_Wed = true; }
                    if (day == "Thu") { model.Chk_Thu = true; }
                    if (day == "Fri") { model.Chk_Fri = true; }
                    if (day == "Sat") { model.Chk_Sat = true; }
                    if (day == "Sun") { model.Chk_Sun = true; }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_ServicePlace_Window(ServicePlaces model)
        {

            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
                if (BASE.AllowMultiuser())
                {
                    //This is starting of checking if the entry is  already changed or not
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable serviceplaces_DbOps = BASE._ServPlacesDBOps.GetRecord(model.xID);
                        if (serviceplaces_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error..";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (serviceplaces_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current ServicePlace");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.LastEditedOn_ServicePlace != Convert.ToDateTime(serviceplaces_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current ServicePlace");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        object MaxValue = 0;
                        MaxValue = BASE._ServPlacesDBOps.GetStatus(model.xID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry Can not be Edited/Deleted...! <br> <br> Note: <br> ------- <br> Drop your Request to Madhuban for Unlock this Entry, <br> If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.Tag == Common.Navigation_Mode._Delete)
                        {
                            DataTable Locations = BASE._AssetLocDBOps.GetListBySPID(Common_Lib.RealTimeService.ClientScreen.Profile_ServicePlaces, model.xID);
                            foreach (DataRow cRow in Locations.Rows)
                            {
                                MaxValue = 0;
                                string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(cRow["AL_ID"].ToString(), false);
                                bool DeleteAllow = true;
                                if (UsedPage.Length > 0)
                                {
                                    DeleteAllow = false;
                                }
                                if (!DeleteAllow)
                                {
                                    jsonParam.message = Messages.RecordChanged("Asset Location");
                                    jsonParam.title = "Location Attached to current Service Place is being referred in " + UsedPage + " !";
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
                } //This is ending of checking if entry is  already changed or not.

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    //Check Place Name
                    if (string.IsNullOrWhiteSpace(model.Txt_Name_ServicePlaces))
                    {
                        jsonParam.message = "Service Place Name Cannot be Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Name_ServicePlaces";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Place Type Blank or not
                    if (string.IsNullOrWhiteSpace(model.Cmb_PlaceType_ServicePlaces))
                    {
                        jsonParam.message = "Type Not Specified...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmb_PlaceType_ServicePlaces";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Start Date is Blank or not
                    if (!IsDate(model.Txt_St_Date_ServicePlaces.ToString()))
                    {
                        jsonParam.message = "Start Date Not Specified";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_St_Date_ServicePlaces";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Date Can not be greater than today.
                    if (IsDate(model.Txt_St_Date_ServicePlaces.ToString()))
                    {
                        Double diff2 = ((TimeSpan)(model.Txt_St_Date_ServicePlaces - DateTime.Today)).Days;// need to check the logic
                        if (diff2 > 0)
                        {
                            jsonParam.title = "Incorrect Information...";
                            jsonParam.message = "Date Cannot be higher than todays...!";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_St_Date_ServicePlaces";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    //Check Weekdays blank                    
                    if (model.chk_Weekdays_CheckedItems() <= 0)
                    {
                        jsonParam.title = "Incorrect Information...";
                        jsonParam.message = "Weekdays Not Specified...!";
                        jsonParam.result = false;
                        jsonParam.focusid = "Chk_Mon";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    //Check timings blank
                    if (!IsDate(model.Txt_Timings_ServicePlaces.ToString()))
                    {
                        jsonParam.message = "Timings Not Specified";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Timings_ServicePlaces";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Place Status as blank
                    if (string.IsNullOrWhiteSpace(model.cmb_Status_ServicePlaces))
                    {
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.message = "Status Not Specified...!";
                        jsonParam.focusid = "cmb_Status_ServicePlaces";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Place Owner is blank or not
                    if (string.IsNullOrWhiteSpace(model.Look_PlaceOwner_ServicePlaces))
                    {
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.message = "Owner Not Selected...!";
                        jsonParam.focusid = "Look_PlaceOwner_ServicePlaces";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //Check Responsible Person is blank or not
                    if (string.IsNullOrWhiteSpace(model.Look_ResponsiblePerson_ServicePlaces))
                    {
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.message = "Responsible Person Not Selected...!";
                        jsonParam.focusid = "Look_ResponsiblePerson_ServicePlaces";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    //Checking Duplicate Record
                    Object MaxValue = 0;
                    int xID = 0;
                    MaxValue = BASE._ServPlacesDBOps.GetCountByPlaceName(model.Txt_Name_ServicePlaces.Trim());
                    if (MaxValue == null)
                    {
                        jsonParam.title = "Error!!";
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)MaxValue != 0 && model.Tag == Common_Lib.Common.Navigation_Mode._New)
                    {
                        jsonParam.title = "Duplicate...(" + model.Txt_Name_ServicePlaces + ")";
                        jsonParam.message = "Same Name Already Available...!" + model.Txt_Name_ServicePlaces;
                        jsonParam.focusid = "Txt_Name_ServicePlaces";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    else if ((int)MaxValue != 0 && model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        if (model.Txt_Name_ServicePlaces.ToUpper().Trim() != model.Txt_Name_Tag_ServicePlaces.ToUpper().Trim())
                        {
                            jsonParam.title = "Duplicate...(" + model.Txt_Name_ServicePlaces + ")";
                            jsonParam.message = "Same Name Already Available...! <br> <br> --> Edit Name: " + model.Txt_Name_Tag_ServicePlaces;
                            jsonParam.focusid = "Txt_Name_ServicePlaces";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    //Checking duplicate Location..
                    object MaxValue_Loc = 0;
                    MaxValue_Loc = BASE._AssetLocDBOps.GetRecordCountByName(model.Txt_Name_ServicePlaces.Trim(), Common_Lib.RealTimeService.ClientScreen.Profile_ServicePlaces, BASE._open_PAD_No_Main);
                    if (MaxValue_Loc == null)
                    {
                        jsonParam.title = "Error!!";
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)MaxValue_Loc != 0 && model.Tag == Common_Lib.Common.Navigation_Mode._New)
                    {
                        jsonParam.title = "Duplicate...(" + model.Txt_Name_ServicePlaces + ")";
                        jsonParam.message = "Same Location Already Available...!";
                        jsonParam.focusid = "Txt_Name_ServicePlaces";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                }

                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Look_PlaceOwner_ServicePlaces))
                        {
                            DateTime? oldEditOn = PlaceOwner_DD_Data.Where(x => x.ID == model.Look_PlaceOwner_ServicePlaces).FirstOrDefault().REC_EDIT_ON;
                            DataTable d1 = BASE._ServPlacesDBOps.GetAddresses(model.Look_PlaceOwner_ServicePlaces);
                            if (d1 == null)
                            {
                                jsonParam.title = "Error!!";
                                jsonParam.message = Common_Lib.Messages.SomeError;
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Common_Lib.Messages.DependencyChanged("Owner");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book(Owner)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.Look_ResponsiblePerson_ServicePlaces))
                        {
                            DateTime? oldEditOn = ResponsiblePerson_DD_Data.Where(x => x.ID == model.Look_ResponsiblePerson_ServicePlaces).FirstOrDefault().REC_EDIT_ON;
                            DataTable d2 = BASE._ServPlacesDBOps.GetAddresses(model.Look_ResponsiblePerson_ServicePlaces);
                            if (d2 == null)
                            {
                                jsonParam.title = "Error!!";
                                jsonParam.message = Common_Lib.Messages.SomeError;
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if (d2.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Responsible Person");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d2.Rows[0]["REC_EDIT_ON"]);
                                if (oldEditOn != NewEditOn)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book(Responsible Person)");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        //Check for duplicate location if the name of the location is already transferred from another center
                        //and this center is not accepted and it is in the pending list
                        DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                        if (!(LocNames == null))
                        {
                            if (LocNames.Rows.Count > 0)
                            {
                                for (int i = 0; i < LocNames.Rows.Count; i++)
                                {
                                    if (model.Txt_Name_ServicePlaces.ToString().ToUpper().Trim() == LocNames.Rows[i][0].ToString().ToUpper())
                                    {
                                        jsonParam.message = Messages.DependencyChanged("Location Name");
                                        jsonParam.title = "Referred Record Already Changed, Location with same name already exists in pending Transfers!!";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.closeform = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                }

                //Checking Lock Status
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    object MaxValue = 0;
                    MaxValue = BASE._ServPlacesDBOps.GetStatus(model.xID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = Messages.DependencyChanged("Entry Not Found...!");
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if ((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                    {
                        jsonParam.message = Messages.DependencyChanged("Locked Entry Cannot be Edit/Delete...! <br><br> Note:<br> ------- <br> Drop your Request to Madhuban to Unlock this Entry <br> If you really want to do some action...");
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                //***********************
                string Status_Action = "";
                Status_Action = ((int)Common.Record_Status._Completed).ToString();
                if (model.ActionMethod.ToString() == "_Delete")
                {
                    Status_Action = ((int)Common.Record_Status._Deleted).ToString();
                }

                //This part is to combine weekdays into a string with comma seperate.                
                string weekDays = "";
                if (model.Chk_Mon == true) { weekDays += "Mon,"; }
                if (model.Chk_Tue == true) { weekDays += "Tue,"; }
                if (model.Chk_Wed == true) { weekDays += "Wed,"; }
                if (model.Chk_Thu == true) { weekDays += "Thu,"; }
                if (model.Chk_Fri == true) { weekDays += "Fri,"; }
                if (model.Chk_Sat == true) { weekDays += "Sat,"; }
                if (model.Chk_Sun == true) { weekDays += "Sun,"; }
                if (weekDays.Length > 0) { weekDays = weekDays.Remove(weekDays.Length - 1); }

                Param_Txn_InsertServicePlaces InNewParam = new Param_Txn_InsertServicePlaces();
                if (model.Tag == Common.Navigation_Mode._New)
                {
                    model.xID = Guid.NewGuid().ToString();
                    // Insert bank and balance 
                    Parameter_Insert_ServicePlaces InParam = new Parameter_Insert_ServicePlaces();
                    InParam.PlaceType = model.Cmb_PlaceType_ServicePlaces;
                    InParam.Name = model.Txt_Name_ServicePlaces;
                    if (IsDate(model.Txt_St_Date_ServicePlaces.ToString()))
                    {
                        InParam.StartDate = Convert.ToDateTime(model.Txt_St_Date_ServicePlaces).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.PlaceAtABID = model.Look_PlaceOwner_ServicePlaces ?? "";
                    InParam.Weekdays = weekDays;
                    InParam.Timing = Convert.ToDateTime(model.Txt_Timings_ServicePlaces).ToString("h:mm tt") ?? "";
                    InParam.ResponsiblePersonABID = model.Look_ResponsiblePerson_ServicePlaces;
                    InParam.OtherDetails = model.Txt_OtherDetails_ServicePlaces ?? "";
                    InParam.Status = model.cmb_Status_ServicePlaces ?? "";
                    InParam.Status_Action = Status_Action;
                    InParam.Max_Capacity = model.Txt_MaxCapacity_ServicePlaces;
                    InParam.RecID = model.xID;

                    InNewParam.param_Insert = InParam;
                    Param_AssetLoc_Insert InAssetLoc = new Param_AssetLoc_Insert();
                    InAssetLoc.name = model.Txt_Name_ServicePlaces.Trim();
                    InAssetLoc.OtherDetails = "Managed By " + model.Look_ResponsiblePerson_ServicePlaces;
                    InAssetLoc.Status_Action = Status_Action;
                    InAssetLoc.Match_LB_ID = "";
                    InAssetLoc.Match_SP_ID = model.xID;

                    InNewParam.param_InsertAssetLoc = InAssetLoc;

                    if (BASE._ServPlacesDBOps.InsertServicePlaces_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.TitleX_ServicePlace;
                        jsonParam.closeform = true;
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
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

                Param_Txn_UpdateServicePlaces EditParam = new Param_Txn_UpdateServicePlaces();
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    Parameter_Update_ServicePlaces UpParam = new Parameter_Update_ServicePlaces();
                    UpParam.PlaceType = model.Cmb_PlaceType_ServicePlaces;
                    UpParam.Name = model.Txt_Name_ServicePlaces.Trim();
                    if (IsDate(model.Txt_St_Date_ServicePlaces.ToString()))
                    {
                        UpParam.StartDate = Convert.ToDateTime(model.Txt_St_Date_ServicePlaces).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.PlaceAtABID = model.Look_PlaceOwner_ServicePlaces;
                    //int Chk_Wkdays = model.chk_Weekdays_CheckedItems();
                    UpParam.Weekdays = weekDays;
                    UpParam.Timing = Convert.ToDateTime(model.Txt_Timings_ServicePlaces).ToString("h:mm tt");
                    UpParam.ResponsiblePersonABID = model.Look_ResponsiblePerson_ServicePlaces;
                    UpParam.OtherDetails = model.Txt_OtherDetails_ServicePlaces;
                    UpParam.Status = model.cmb_Status_ServicePlaces.ToString();
                    UpParam.Max_Capacity = model.Txt_MaxCapacity_ServicePlaces;
                    UpParam.RecID = model.xID;

                    EditParam.param_Update = UpParam;

                    Param_AssetLoc_Update UpByRef = new Param_AssetLoc_Update();
                    UpByRef.name = model.Txt_Name_ServicePlaces;
                    UpByRef.OtherDetails = "Managed By " + model.Look_ResponsiblePerson_ServicePlaces;
                    UpByRef.Match_LB_ID = "";
                    UpByRef.Match_SP_ID = model.xID;

                    string Message = "";
                    DataTable Locations = BASE._AssetLocDBOps.GetListBySPID(ClientScreen.Accounts_Voucher_Gift, model.xID);
                    if (Locations == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Locations.Rows.Count > 0)
                    {
                        Message = "<br> <br> No Subsequent Changes have been made in Location mapped to this Service Place. <br> User may make the required changes manually from profile -> Core -> Locations.";
                    }
                    if (BASE._ServPlacesDBOps.UpdateServicePlaces_Txn(EditParam))
                    {
                        jsonParam.message = Messages.SaveSuccess + Message;
                        jsonParam.title = model.TitleX_ServicePlace;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                Param_Txn_DeleteServicePlaces DelParam = new Param_Txn_DeleteServicePlaces();
                if (model.ActionMethod == Common.Navigation_Mode._Delete.ToString())
                {
                    DelParam.RecID_DelAssetLoc = model.xID;
                    DelParam.RecID_Delete = model.xID;
                    if (BASE._ServPlacesDBOps.DeleteServicePlaces_Txn(DelParam))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = model.TitleX_ServicePlace;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        jsonParam.refreshgrid = true;

                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        } /*This code is for Frm_ServicePlaces_Window HTTP Post*/

        public JsonResult DataNavigation(string ActionMethod, string id, string[] Selectedid)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            var ServicePlaceData = ServicePlace_ExportData.Where(x => x.ID == id).FirstOrDefault();
            var xTemp_Year = ServicePlaceData.YearID;
            var xTemp_ID = ServicePlaceData.ID;
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod.ToUpper() == "PRINT-LIST")
                    {
                        DataTable d1 = BASE._BankAccountDBOps.GetRecord(id);
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
                        if (d1.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current ServicePlace");
                            jsonParam.title = "Record Changed/Removed In Background!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        if (RecEdit_Date != Convert.ToDateTime(ServicePlaceData.Edit_Date))
                        {
                            jsonParam.message = Messages.RecordChanged("Current ServicePlace");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (ActionMethod.ToUpper() == "EDIT")
                {
                    object MaxValue = 0;
                    //below line returns the status of the id wether it is locked or completed etc in the form of 0, 1, 2, etc.
                    MaxValue = BASE._ServPlacesDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry Cannot be Edit/Delete...! <br><br> Note: <br> ------- <br> Drop your Request to Madhuban for Unlock this Entry, <br> If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    bool? IsServicePlaceCarriedFwd = BASE._ServPlacesDBOps.IsServicePlaceCarriedForward(xTemp_ID, xTemp_Year);
                    if (IsServicePlaceCarriedFwd == null)
                    {
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsServicePlaceCarriedFwd == true)
                    {
                        if (BASE._prev_Unaudited_YearID != 0)
                        {
                            jsonParam.message = "Entry Cannot be edited . . !<br><br>This entry has been carried forward from previous year(s). Updation(Partial) can be done only after finalization of previous year accounts...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (ActionMethod.ToUpper() == "DELETE")
                {
                    object MaxValue = 0;
                    MaxValue = BASE._ServPlacesDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.refreshgrid = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry cannot be Edit / Delete...! <br><br> Note: <br> ------ <br>Drop your request to Madhuban for Unlock this entry, <br> If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.refreshgrid = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    bool? IsServicePlaceCarriedFwd = BASE._ServPlacesDBOps.IsServicePlaceCarriedForward(xTemp_ID, xTemp_Year);
                    if (IsServicePlaceCarriedFwd == null)
                    {
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsServicePlaceCarriedFwd == true)
                    {
                        if (BASE._prev_Unaudited_YearID != 0) //Restricted in case of unsplit only
                        {
                            jsonParam.message = "Entry Cannot be edited . . !<br><br>This entry has been carried forward from previous year(s). Deletion can be done only after finalization of previous year accounts...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    DataTable Locations = BASE._AssetLocDBOps.GetListBySPID(Common_Lib.RealTimeService.ClientScreen.Profile_ServicePlaces, xTemp_ID);

                    foreach (DataRow cRow in Locations.Rows)
                    {
                        string Usage = CheckLocationUsage(cRow["AL_ID"].ToString(), false);
                        if (Usage.Length > 0)
                        {
                            jsonParam.message = "Location Attached to current Service Place is being referred in " + Usage + " !";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (ActionMethod.ToUpper() == "VIEW")
                {
                    object MaxValue = 0;
                    MaxValue = BASE._ServPlacesDBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found / Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                //This code is after all the if conditions checked
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
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        } //This is DataNavigation json Action code.
        //DataNavigation complete here.

        public void RefreshOwnerList()
        {
            DataTable d1 = BASE._ServPlacesDBOps.GetAddresses();
            DataView dview = new DataView(d1);
            dview.Sort = "NAME";
            PlaceOwner_DD_Data = DatatableToModel.DataTabletoServicePlacesLookUp_GetOwnerList(dview.ToTable());
        }
        public ActionResult LookUp_GetOwnerList_ServicePlaces(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (PlaceOwner_DD_Data == null || DDRefresh == true)
            {
                RefreshOwnerList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PlaceOwner_DD_Data, loadOptions)), "application/json");
        }

        public void RefreshResponsibleList()
        {
            DataTable d1 = BASE._ServPlacesDBOps.GetAddresses();
            DataView dview = new DataView(d1);
            dview.Sort = "NAME";
            ResponsiblePerson_DD_Data = DatatableToModel.DataTabletoServicePlacesLookUp_GetResponsibleList(dview.ToTable());
        }
        public ActionResult LookUp_GetResponsibleList_ServicePlaces(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (ResponsiblePerson_DD_Data == null || DDRefresh == true)
            {
                RefreshResponsibleList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ResponsiblePerson_DD_Data, loadOptions)), "application/json");
        }

        public void SessionClear()
        {
            ClearBaseSession("_ServicePlaces");
            Session.Remove("ServicePlaceInfo_detailGrid_Data");
        }    //This is info page session clear function.Window page session clear function is seperately written

        public void SessionClear_Window()
        {
            ClearBaseSession("_ServicePlacesWindow");
        }    // This is window page session clear function.

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
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('ServicePlaces_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion

        public void ServicePlaces_user_rights()
        {
            ViewData["ServicePlaces_AddRight"] = CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Add");
            ViewData["ServicePlaces_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Update");
            ViewData["ServicePlaces_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "View");
            ViewData["ServicePlaces_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Delete");
            ViewData["ServicePlaces_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }


        public string CheckLocationUsage(string LocationID, bool Exclude_Sold_IF = true)
        {
            string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(LocationID);
            bool DeleteAllow = true;
            if (UsedPage.Length > 0)
            {
                DeleteAllow = false;
            }
            return UsedPage;
        }

        #region Dev Extreme

        public ActionResult Frm_ServicePlace_Info_dx()
        {
            ViewData["Open_Ins_Id_Check"] = BASE._open_Ins_ID;
            ViewData["Open_Ins_Name"] = BASE._open_Ins_Name;
            ServicePlaces_user_rights();
            if (!CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_ServicePlaces').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_ServicePlaces).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ServicePlaceInfo_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View();
        }
        [HttpGet]
        public ActionResult Frm_ServicePlace_Info_Grid_dx()
        {
            var SP_Data = BASE._ServPlacesDBOps.GetList();
            ServicePlace_ExportData = SP_Data;

            return Content(JsonConvert.SerializeObject(SP_Data), "application/json");

        }
        public ActionResult Frm_ServicePlace_Info_DetailGrid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_ServicePlaces, !VouchingMode)), "application/json");
        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_ServicePlaces, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('ServicePlaces_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }


        #endregion 
    }
}