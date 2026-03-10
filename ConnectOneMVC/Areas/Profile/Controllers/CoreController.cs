using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;


namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class CoreController : BaseController
    {
        #region "Start--> Default Variables"
        public List<ResponsePerson> ResponsiblePartyList
        {
            get { return (List<ResponsePerson>)GetBaseSession("ResponsiblePartyList_CoreWindow"); }
            set { SetBaseSession("ResponsiblePartyList_CoreWindow", value); }
        }
        public DateTime Support_Edit_On
        {
            get
            {
                return (DateTime)GetBaseSession("Support_Edit_On_Core");
            }
            set
            {
                SetBaseSession("Support_Edit_On_Core", value);
            }
        }

        public DateTime? LastEditedOn
        {
            get
            {
                return (DateTime?)GetBaseSession("LastEditedOn_Core");
            }
            set
            {
                SetBaseSession("LastEditedOn_Core", value);
            }
        }
        public LookUp_GetPersonsList_Info lpg
        {
            get
            {
                return (LookUp_GetPersonsList_Info)GetBaseSession("lpg_Core");
            }
            set
            {
                SetBaseSession("lpg_Core", value);
            }
        }

        public List<CoreInfoInstitute> CoreInstitute
        {
            get
            {
                return (List<CoreInfoInstitute>)GetBaseSession("CoreInstitute_Core");
            }
            set
            {
                SetBaseSession("CoreInstitute_Core", value);
            }
        }
        public List<CoreInfoLocation> CoreLocation
        {
            get
            {
                return (List<CoreInfoLocation>)GetBaseSession("CoreLocation_Core");
            }
            set
            {
                SetBaseSession("CoreLocation_Core", value);
            }
        }
        public void SetDefaultValues()
        {
            LastEditedOn = DateTime.Now;
            Support_Edit_On = DateTime.MinValue;
            lpg = new LookUp_GetPersonsList_Info();
        }

        #endregion

        #region Page Constructor
        public CoreController()
        {
        }
        #endregion 

        #region Add/Edit Bank Account Details for popup
        public ActionResult NewPop(string ActionMethod)
        {
            CoreProfile model = new CoreProfile();
            return View(model);
        }
        public JsonResult DataNavigation(string ActionMethod, string ID, DateTime? Edit_Date, DateTime? LastEditedOn)
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Update")) && ActionMethod == "Edit")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "View")) && ActionMethod == "View")
            {
                return Json(new { result = "NoViewRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Delete")) && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            CoreProfile Cp = new CoreProfile();
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (ActionMethod == "Edit")
                    {
                        if (Common_Lib.Common.Navigation_Mode._Edit == (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_Edit"))
                        {
                            DataTable dt = BASE._CoreDBOps.GetCenSupportInfo();
                            DateTime Support_Edit_On = Convert.ToDateTime(dt.Rows[0]["REC_EDIT_ON"]);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Start--> Procedures" (Default Grid Page Action Method GET: Profile/Core)
        public ActionResult Frm_Core_Info()
        {
            SetDefaultValues();
            Core_User_Rights();
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Core_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Core, "List"))//Code written for User Authorization do not remove
            {
                CoreInfo coreInfo = new CoreInfo();
                DataTable d1 = BASE._CoreDBOps.GetCenterDetails();
                if ((d1 == null))
                {
                    return View("Frm_Core_Info", null);
                }

                DataTable d3 = BASE._CoreDBOps.GetCenSupportInfo();
                if ((d3 == null))
                {
                    return View("Frm_Core_Info", null);
                }

                // 1 General

                coreInfo.xCen_Name = Convert.ToString(d1.Rows[0]["CEN_NAME"]);
                coreInfo.xCen_Pad_No = BASE._open_PAD_No_Main;
                coreInfo.xCen_UID = BASE._open_UID_No;
                coreInfo.xCen_AccType = BASE._open_Year_Acc_Type;
                coreInfo.xCen_SubZone = BASE._open_Zone_SUB_ID;
                coreInfo.xCen_Zone = BASE._open_Zone_ID;
                if (d1.Columns.Contains("CEN_INCHARGE"))
                {
                    if (d1.Rows[0]["CEN_INCHARGE"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Incharge = Convert.ToString(d1.Rows[0]["CEN_INCHARGE"]);
                    }
                    else
                    {
                        coreInfo.xCen_Incharge = "";
                    }

                }

                if (d1.Rows[0]["CEN_IN_PAD_NO"] != System.DBNull.Value)
                {
                    coreInfo.xCen_In_Padno = Convert.ToString(d1.Rows[0]["CEN_IN_PAD_NO"]);
                }
                else
                {
                    coreInfo.xCen_In_Padno = "";
                }

                if ((d3.Rows.Count > 0))
                {
                    if (d3.Rows[0]["CEN_OPEN_DATE"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_DOS = Convert.ToString(d3.Rows[0]["CEN_OPEN_DATE"]);
                    }
                    else
                    {
                        coreInfo.xCen_DOS = "";
                    }

                    if (d3.Rows[0]["RES_PERSON"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Res_Person = Convert.ToString(d3.Rows[0]["RES_PERSON"]);
                    }
                    else
                    {
                        coreInfo.xCen_Res_Person = "";
                    }

                    if (d3.Rows[0]["C_MOB_NO_1"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Res_TelNo = Convert.ToString(d3.Rows[0]["C_MOB_NO_1"]);
                    }
                    else
                    {
                        coreInfo.xCen_Res_TelNo = "";
                    }

                    if (d3.Rows[0]["REC_EDIT_ON"] != System.DBNull.Value)
                    {
                        Support_Edit_On = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
                        CoreProfile cp = new CoreProfile();
                        ViewBag.EditDate = Support_Edit_On;
                    }
                }
                else
                {
                    // 
                    coreInfo.xCen_Res_Person = "";
                    coreInfo.xCen_Res_TelNo = "";
                    coreInfo.xCen_DOS = "";
                }

                if (((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                            || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
                {
                    if (d1.Columns.Contains("CEN_INCHARGE_IMAGE"))
                    {
                        if (d1.Rows[0]["CEN_INCHARGE_IMAGE"] != System.DBNull.Value)
                        {

                            coreInfo.ImgIncharge = (byte[])d1.Rows[0]["CEN_INCHARGE_IMAGE"];

                        }
                        else
                        {
                            coreInfo.ImgIncharge = null;
                        }

                    }


                    if (d1.Columns.Contains("CENTRE_IMAGE"))
                    {
                        if (d1.Rows[0]["CENTRE_IMAGE"] != System.DBNull.Value)
                        {
                            coreInfo.ImgCentre = (byte[])d1.Rows[0]["CENTRE_IMAGE"];
                        }
                        else
                        {
                            coreInfo.ImgCentre = null;
                        }

                    }

                }
                else
                {
                    coreInfo.ImgIncharge = null;
                    coreInfo.ImgCentre = null;
                }

                // 2 contact detail
                if (d1.Rows[0]["CEN_B_NAME"] != System.DBNull.Value)
                {
                    coreInfo.xCen_B_Name = Convert.ToString(d1.Rows[0]["CEN_B_NAME"]);
                }
                else
                {
                    coreInfo.xCen_B_Name = "";
                }

                if (d1.Rows[0]["CEN_ADD1"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add1 = Convert.ToString(d1.Rows[0]["CEN_ADD1"]);
                }
                else
                {
                    coreInfo.xCen_Add1 = "";
                }

                if (d1.Rows[0]["CEN_ADD2"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add2 = Convert.ToString(d1.Rows[0]["CEN_ADD2"]);
                }
                else
                {
                    coreInfo.xCen_Add2 = "";
                }

                if (d1.Rows[0]["CEN_ADD3"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add3 = Convert.ToString(d1.Rows[0]["CEN_ADD3"]);
                }
                else
                {
                    coreInfo.xCen_Add3 = "";
                }

                if (d1.Rows[0]["CEN_ADD4"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add4 = Convert.ToString(d1.Rows[0]["CEN_ADD4"]);
                }
                else
                {
                    coreInfo.xCen_Add4 = "";
                }

                if (d1.Rows[0]["CEN_CITY"] != System.DBNull.Value)
                {
                    coreInfo.xCen_City = Convert.ToString(d1.Rows[0]["CEN_CITY"]);
                }
                else
                {
                    coreInfo.xCen_City = "";
                }

                if (d1.Rows[0]["CEN_PINCODE"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Pin = Convert.ToString(d1.Rows[0]["CEN_PINCODE"]);
                }
                else
                {
                    coreInfo.xCen_Pin = "";
                }

                if (d1.Rows[0]["CEN_DISTRICT"] != System.DBNull.Value)
                {
                    coreInfo.xCen_District = Convert.ToString(d1.Rows[0]["CEN_DISTRICT"]);
                }
                else
                {
                    coreInfo.xCen_District = "";
                }

                if (d1.Rows[0]["CEN_STATE"] != System.DBNull.Value)
                {
                    coreInfo.xCen_State = Convert.ToString(d1.Rows[0]["CEN_STATE"]);
                }
                else
                {
                    coreInfo.xCen_State = "";
                }

                if (d1.Rows[0]["CEN_COUNTRY"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Country = Convert.ToString(d1.Rows[0]["CEN_COUNTRY"]);
                }
                else
                {
                    coreInfo.xCen_Country = "";
                }

                string t1;
                string t2;
                if (d1.Rows[0]["CEN_TEL_NO_1"] != System.DBNull.Value)
                {
                    t1 = Convert.ToString(d1.Rows[0]["CEN_TEL_NO_1"]);
                }
                else
                {
                    t1 = "";
                }

                if (d1.Rows[0]["CEN_TEL_NO_2"] != System.DBNull.Value)
                {
                    t2 = Convert.ToString(d1.Rows[0]["CEN_TEL_NO_2"]);
                }
                else
                {
                    t2 = "";
                }

                if ((t2.Length > 0))
                {
                    coreInfo.xCen_Tel = (t1 + (", " + t2));
                }
                else
                {
                    coreInfo.xCen_Tel = t1;
                }

                string f1;
                string f2;
                if (d1.Rows[0]["CEN_FAX_NO_1"] != System.DBNull.Value)
                {
                    f1 = Convert.ToString(d1.Rows[0]["CEN_FAX_NO_1"]);
                }
                else
                {
                    f1 = "";
                }

                if (d1.Rows[0]["CEN_FAX_NO_2"] != System.DBNull.Value)
                {
                    f2 = Convert.ToString(d1.Rows[0]["CEN_FAX_NO_2"]);
                }
                else
                {
                    f2 = "";
                }

                if ((f2.Length > 0))
                {
                    coreInfo.xCen_Fax = (f1 + (", " + f2));
                }
                else
                {
                    coreInfo.xCen_Fax = f1;
                }

                string m1;
                string m2;
                if (d1.Rows[0]["CEN_MOB_NO_1"] != System.DBNull.Value)
                {
                    m1 = Convert.ToString(d1.Rows[0]["CEN_MOB_NO_1"]);
                }
                else
                {
                    m1 = "";
                }

                if (d1.Rows[0]["CEN_MOB_NO_2"] != System.DBNull.Value)
                {
                    m2 = Convert.ToString(d1.Rows[0]["CEN_MOB_NO_2"]);
                }
                else
                {
                    m2 = "";
                }

                if ((m2.Length > 0))
                {
                    coreInfo.xCen_Mob = (m1 + (", " + m2));
                }
                else
                {
                    coreInfo.xCen_Mob = m1;
                }

                string e1;
                string e2;
                if (d1.Rows[0]["CEN_EMAIL_ID_1"] != System.DBNull.Value)
                {
                    e1 = Convert.ToString(d1.Rows[0]["CEN_EMAIL_ID_1"]);
                }
                else
                {
                    e1 = "";
                }

                if (d1.Rows[0]["CEN_EMAIL_ID_2"] != System.DBNull.Value)
                {
                    e2 = Convert.ToString(d1.Rows[0]["CEN_EMAIL_ID_2"]);
                }
                else
                {
                    e2 = "";
                }

                if ((e2.Length > 0))
                {
                    coreInfo.xCen_Email = (e1 + (", " + e2));
                }
                else
                {
                    coreInfo.xCen_Email = e1;
                }

                // 
                if (d1.Rows[0]["CEN_WEBSITE_URL"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Website = Convert.ToString(d1.Rows[0]["CEN_WEBSITE_URL"]);
                }
                else
                {
                    coreInfo.xCen_Website = "";
                }
                lpg.REC_EDIT_ON = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);

                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(coreInfo);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Core').hide();</script>");//Code written for User Authorization do not remove
            }
        }

        public ActionResult Frm_Core_Info_InstituteAccount_Grid(string command, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Core_User_Rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (CoreInstitute == null || command == "REFRESH")
            {
                DataTable dInst = BASE._CoreDBOps.GetInstitutes();
                if ((dInst == null))
                {
                    return PartialView("Frm_Core_Info_InstituteAccount_Grid", null);
                }

                DataTable dCENTRE = BASE._CoreDBOps.GetCenterDetailsByBKPAD();
                if ((dCENTRE == null))
                {
                    return PartialView("Frm_Core_Info_InstituteAccount_Grid", null);
                }

                var INSTData = from INST in dInst.AsEnumerable()
                               join CEN in dCENTRE.AsEnumerable()

                       on INST["INS_ID"] equals CEN["CEN_INS_ID"]
                               select new CoreInfoInstitute
                               {
                                   INS_NAME = Convert.ToString(INST["INS_NAME"]),
                                   CEN_NAME = Convert.ToString(CEN["CEN_NAME"]),
                                   CEN_UID = Convert.ToString(CEN["CEN_UID"]),
                                   CEN_INCHARGE = Convert.ToString(CEN["CEN_INCHARGE"]),
                                   ID = Convert.ToString(INST["INS_ID"])
                               };
                var Final_Data = INSTData.ToList();
                CoreInstitute = Final_Data;
            }
            return PartialView("Frm_Core_Info_InstituteAccount_Grid", CoreInstitute);
        }
        public ActionResult Frm_Core_Info_Location_Grid(string command, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            Core_User_Rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (CoreLocation == null || command == "REFRESH")
            {
                DataTable locTable = BASE._CoreDBOps.GetLocations();
                if ((locTable.Rows.Count == 0))
                {
                    return PartialView("Frm_Core_Info_Location_Grid", null);
                }

                var Final_Data = DatatableToModel.DataTabletoCoreLocationList(locTable);
                CoreLocation = Final_Data;
            }
            return PartialView("Frm_Core_Info_Location_Grid", CoreLocation);
        }

        public ActionResult CoreLocationCustomDataAction(string key)
        {
            string itstr = "";
            if (CoreLocation != null)
            {
                if (key != null)
                {
                    var Final_Data = CoreLocation as List<CoreInfoLocation>;
                    var it = (CoreInfoLocation)Final_Data.Where(f => f.ID == key).FirstOrDefault();

                    if (it != null)
                    {
                        itstr = it.ID + "![" + it.Edit_Date + "![" + it.Add_Date + "![" + it.Add_By + "![" + it.Edit_By + "![" + it.Action_Status + "![" + it.Action_Date + "![" + it.Action_By + "![" + it.YEARID;
                    }
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        //Edit General information --start
        public ActionResult frm_Core_window(string EditedOn, string ActionMethod = null, string ID = null, string LastEditedOn = "", string PopupID = "", string CalingScreen = "")
        {
            Core_User_Rights();
            var Popup = string.IsNullOrWhiteSpace(PopupID) ? "popup_frm_Core_Window" : PopupID;
            ViewBag.PopupID = Popup;
            ViewBag.CalingScreen = CalingScreen;
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Add")) && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + Popup + "','Not Allowed','No Rights')</script>");
            }
            CoreProfile model = new Models.CoreProfile();
            BASE._CoreDBOps.GetCenSupportInfo();
            DataTable d5 = BASE._CoreDBOps.GetList();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            refreshPartyList();
            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                CoreInfo coreinfo = new CoreInfo();
                //if (System.DBNull.Value != d5.Rows[0]["CEN_OPEN_DATE"])
                //{ model.CenterStartDate = (DateTime)d5.Rows[0]["CEN_OPEN_DATE"]; }
                if (d5.Rows.Count > 0) model.typeField = d5.Rows[0]["CEN_ACC_RES_PERSON_AB_ID"].ToString();

                DataTable _dtableTelData = BASE._CoreDBOps.GetCenterDetails();
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult frm_Core_window(CoreProfile model)
        {
            var message = "";

            var PresentDate = model.EditDate;

            if (BASE.AllowMultiuser() == true)
            {
                if (Common_Lib.Common.Navigation_Mode._Edit.ToString() == model.TempActionMethod.ToString())
                {
                    DataTable general_DbOps = BASE._CoreDBOps.GetList();
                    if (general_DbOps.Rows.Count.ToString() == "0")
                    {
                        model.TempActionMethod = Common_Lib.Common.Navigation_Mode._New.ToString();//if no support info then change mode as new
                        //return Json(new { Message = "Record Already Changed!!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (LastEditedOn != null)//skip check if lastediton is null
                    {
                        if (general_DbOps.Rows.Count > 0)
                        {
                            DateTime LastEditedOnB = Convert.ToDateTime(String.Format("{0:g}", LastEditedOn));
                            DateTime RecEditBS = Convert.ToDateTime(String.Format("{0:g}", general_DbOps.Rows[0]["REC_EDIT_ON"]));
                            DateTime LastEditedOnSB = Convert.ToDateTime(String.Format("{0:g}", general_DbOps.Rows[0]["REC_EDIT_ON"]));
                            if ((LastEditedOnSB != RecEditBS))
                            {
                                return Json(new { Message = "Record Already Changed!!", result = false }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                if (Common_Lib.Common.Navigation_Mode._New.ToString() == model.TempActionMethod)
                {
                    if (model.Locations != null)
                    {
                        if (model.Locations.Length <= 0)
                        {
                            return Json(new { Message = "L o c a t i o n   c a n n o t   b e   B l a n k . . . !", result = false }, JsonRequestBehavior.AllowGet);
                        }
                        //Checking Duplicate Record....
                        object MaxValue = 0;
                        int xID = 0;
                        MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(model.Locations.Trim(), Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                        if ((MaxValue == null))
                        {
                            BASE.HandleDBError_OnNothingReturned();
                        }
                        if ((Convert.ToInt32(MaxValue) != 0) && (model.TempActionMethod.Trim() == Common_Lib.Common.Navigation_Mode._New.ToString()))
                        {
                            return Json(new { Message = "S a m e   L o c a t i o n   A l r e a d y   A v a i l a b l e . . . !", result = false }, JsonRequestBehavior.AllowGet);
                        }
                        else if (((Convert.ToUInt32(MaxValue) != 0) && (model.TempActionMethod.ToString() == Common_Lib.Common.Navigation_Mode._New.ToString())))
                        {
                            if ((model.Locations.ToUpper().Trim() != model.TempActionMethod.ToUpper().Trim()))
                            {
                                return Json(new { Message = "S a m e   L o c a t i o n   A l r e a d y   A v a i l a b l e . . . !", result = false }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        if (((model.TempActionMethod.ToString() == Common_Lib.Common.Navigation_Mode._New.ToString()) || (model.TempActionMethod.ToString() == Common_Lib.Common.Navigation_Mode._Edit.ToString())))
                        {
                            // Bug #5824 fix
                            DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                            if (!(LocNames == null))
                            {
                                if ((LocNames.Rows.Count > 0))
                                {
                                    for (int i = 0; i < LocNames.Rows.Count - 1; i++)
                                    {
                                        if ((model.Locations.ToUpper().Trim() == LocNames.Rows[i][0].ToString().ToUpper()))
                                        {
                                            return Json(new { Message = model.Locations.Trim() + "  " + "Referred Record Already Changed, Location with same name already exists in pending Transfers!!", result = false }, JsonRequestBehavior.AllowGet);
                                        }
                                    }

                                }

                            }
                            if (model.TempActionMethod == Common_Lib.Common.Navigation_Mode._New.ToString())
                            {
                                return Json(new
                                {
                                    Message = "SaveLocation",
                                    result = true,
                                    Test = model.Locations,
                                }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }
            }
            //--------------------------------------
            //End : Check if entry already changed 
            //--------------------------------------
            if (model.TempActionMethod.ToString() == "_New" || model.TempActionMethod.ToString() == "_Edit")
            {
                //if (model.CenterStartDate == null)
                //{
                //    return Json(new { Message = "Incomplete Information. . .", result = false }, JsonRequestBehavior.AllowGet);
                //}
                //DateTime openDt = Convert.ToDateTime(String.Format("{0:g}", BASE._open_Year_Edt));
                //DateTime CurDate = Convert.ToDateTime(String.Format("{0:g}", model.CenterStartDate));
                //if ((CurDate > openDt))
                //{
                //    return Json(new { Message = "Date must be Earlier than End Date of Financial Year . . . ", result = false }, JsonRequestBehavior.AllowGet);
                //}


                if ((model.typeField.Trim().Length == 0))
                {
                    return Json(new { Message = "Person Not Selected . . . !", result = false }, JsonRequestBehavior.AllowGet);
                }
                //-------------------------------------// Dependencies //-----------------------------------------------------
                if (BASE.AllowMultiuser() == true)
                {
                    if (model.TempActionMethod.ToString() == "_Edit" || model.TempActionMethod.ToString() == "Edit")
                    {
                        if (model.typeField.Length > 0)
                        {
                            DataTable d1 = BASE._CoreDBOps.GetPartyList(model.typeField);
                            DateTime oldEditOnB = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                            DateTime oldEditOnS = Convert.ToDateTime(String.Format("{0:g}", oldEditOnB));
                            if (d1.Rows.Count <= 0)
                            {
                                return Json(new { Message = "Referred Record Already Deleted...!", result = false }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                // Record has not been deleted
                                DateTime NewEditOnB = Convert.ToDateTime(d1.Rows[0][2]);
                                DateTime NewEditOnS = Convert.ToDateTime(String.Format("{0:g}", NewEditOnB));
                                if ((oldEditOnS != NewEditOnS))
                                {
                                    return Json(new { Message = "Referred Record Already Changed!!", result = false }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                    }
                }
                //--------------------------------------------------------------------------------------------------------------
                string Status_Action = "";
                Status_Action = Common_Lib.Common.Record_Status._Completed.ToString();
                if (model.TempActionMethod.ToString() == "_Edit" || model.TempActionMethod.ToString() == "Edit" || model.TempActionMethod.ToString() == "New" || model.TempActionMethod.ToString() == "_New")
                {
                    Common_Lib.RealTimeService.Parameter_Update_Core UpParam = new Common_Lib.RealTimeService.Parameter_Update_Core();
                    //UpParam.OpeningDate = Convert.ToDateTime(model.CenterStartDate).ToString(BASE._Server_Date_Format_Long);
                    UpParam.ResponsiblePersonID = model.typeField;
                    if (BASE._CoreDBOps.Update(UpParam))
                    {
                        return Json(new { Message = "Save Successfully", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "Error";
                    }
                }
            }
            return Json(new
            {
                Message = message,
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetResponsePersonsList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (ResponsiblePartyList == null || DDRefresh == true)
            {
                refreshPartyList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ResponsiblePartyList, loadOptions)), "application/json");
        }
        public void refreshPartyList()
        {
            DataTable prop = BASE._CoreDBOps.GetPartyList();
            DataView dview = new DataView(prop);
            dview.Sort = "Name";
            ResponsiblePartyList = DatatableToModel.DataTabletoLookUp_GetResponsePersonsList_INFO(dview.ToTable());
        }
        //Edit General information --End
        #endregion

        #region Export
        public ActionResult Frm_Institute_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('CoreInstitute_report_modal','Not Allowed','No Rights');$('#CoreModelListPreview').hide();</script>");
            }
            return PartialView();
        }

        public ActionResult Frm_Location_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('CoreLocation_report_modal','Not Allowed','No Rights');$('#CoreModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion

        #region MISC
        public void Core_User_Rights()
        {
            ViewData["Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            ViewData["Core_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Update");
            ViewData["Core_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "View");
            ViewData["Core_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Delete");
            ViewData["Core_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Export");
            ViewData["Core_ListRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "List");

            if (!(BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)))
            {
                ViewData["Profile_CoreSuperuser_auditor_Lock_UnLock"] = false;
            }
            else
            {
                ViewData["Profile_CoreSuperuser_auditor_Lock_UnLock"] = true;
            }
        }
        public void SessionClear()
        {
            ClearBaseSession("_Core");
        }
        public void SessionClear_Window()
        {
            ClearBaseSession("_CoreWindow");
        }
        #endregion

        #region Devextreme
        public ActionResult Frm_Core_Info_dx()
        {
            SetDefaultValues();
            Core_User_Rights();
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Core).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Core_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((
                BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Core, "List"))//Code written for User Authorization do not remove
            {
                CoreInfo coreInfo = new CoreInfo();
                DataTable d1 = BASE._CoreDBOps.GetCenterDetails();
                if ((d1 == null))
                {
                    return View("Frm_Core_Info", null);
                }

                DataTable d3 = BASE._CoreDBOps.GetCenSupportInfo();
                if ((d3 == null))
                {
                    return View("Frm_Core_Info", null);
                }

                // 1 General

                coreInfo.xCen_Name = Convert.ToString(d1.Rows[0]["CEN_NAME"]);
                coreInfo.xCen_Pad_No = BASE._open_PAD_No_Main;
                coreInfo.xCen_UID = BASE._open_UID_No;
                coreInfo.xCen_AccType = BASE._open_Year_Acc_Type;
                coreInfo.xCen_SubZone = BASE._open_Zone_SUB_ID;
                coreInfo.xCen_Zone = BASE._open_Zone_ID;
                if (d1.Columns.Contains("CEN_INCHARGE"))
                {
                    if (d1.Rows[0]["CEN_INCHARGE"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Incharge = Convert.ToString(d1.Rows[0]["CEN_INCHARGE"]);
                    }
                    else
                    {
                        coreInfo.xCen_Incharge = "";
                    }

                }

                if (d1.Rows[0]["CEN_IN_PAD_NO"] != System.DBNull.Value)
                {
                    coreInfo.xCen_In_Padno = Convert.ToString(d1.Rows[0]["CEN_IN_PAD_NO"]);
                }
                else
                {
                    coreInfo.xCen_In_Padno = "";
                }

                if ((d3.Rows.Count > 0))
                {
                    if (d3.Rows[0]["CEN_OPEN_DATE"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_DOS = Convert.ToString(d3.Rows[0]["CEN_OPEN_DATE"]);
                    }
                    else
                    {
                        coreInfo.xCen_DOS = "";
                    }

                    if (d3.Rows[0]["RES_PERSON"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Res_Person = Convert.ToString(d3.Rows[0]["RES_PERSON"]);
                    }
                    else
                    {
                        coreInfo.xCen_Res_Person = "";
                    }

                    if (d3.Rows[0]["C_MOB_NO_1"] != System.DBNull.Value)
                    {
                        coreInfo.xCen_Res_TelNo = Convert.ToString(d3.Rows[0]["C_MOB_NO_1"]);
                    }
                    else
                    {
                        coreInfo.xCen_Res_TelNo = "";
                    }

                    if (d3.Rows[0]["REC_EDIT_ON"] != System.DBNull.Value)
                    {
                        Support_Edit_On = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
                        CoreProfile cp = new CoreProfile();
                        ViewBag.EditDate = Support_Edit_On;
                    }
                }
                else
                {
                    // 
                    coreInfo.xCen_Res_Person = "";
                    coreInfo.xCen_Res_TelNo = "";
                    coreInfo.xCen_DOS = "";
                }

                if (((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                            || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
                {
                    if (d1.Columns.Contains("CEN_INCHARGE_IMAGE"))
                    {
                        if (d1.Rows[0]["CEN_INCHARGE_IMAGE"] != System.DBNull.Value)
                        {

                            coreInfo.ImgIncharge = (byte[])d1.Rows[0]["CEN_INCHARGE_IMAGE"];

                        }
                        else
                        {
                            coreInfo.ImgIncharge = null;
                        }

                    }


                    if (d1.Columns.Contains("CENTRE_IMAGE"))
                    {
                        if (d1.Rows[0]["CENTRE_IMAGE"] != System.DBNull.Value)
                        {
                            coreInfo.ImgCentre = (byte[])d1.Rows[0]["CENTRE_IMAGE"];
                        }
                        else
                        {
                            coreInfo.ImgCentre = null;
                        }

                    }

                }
                else
                {
                    coreInfo.ImgIncharge = null;
                    coreInfo.ImgCentre = null;
                }

                // 2 contact detail
                if (d1.Rows[0]["CEN_B_NAME"] != System.DBNull.Value)
                {
                    coreInfo.xCen_B_Name = Convert.ToString(d1.Rows[0]["CEN_B_NAME"]);
                }
                else
                {
                    coreInfo.xCen_B_Name = "";
                }

                if (d1.Rows[0]["CEN_ADD1"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add1 = Convert.ToString(d1.Rows[0]["CEN_ADD1"]);
                }
                else
                {
                    coreInfo.xCen_Add1 = "";
                }

                if (d1.Rows[0]["CEN_ADD2"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add2 = Convert.ToString(d1.Rows[0]["CEN_ADD2"]);
                }
                else
                {
                    coreInfo.xCen_Add2 = "";
                }

                if (d1.Rows[0]["CEN_ADD3"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add3 = Convert.ToString(d1.Rows[0]["CEN_ADD3"]);
                }
                else
                {
                    coreInfo.xCen_Add3 = "";
                }

                if (d1.Rows[0]["CEN_ADD4"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Add4 = Convert.ToString(d1.Rows[0]["CEN_ADD4"]);
                }
                else
                {
                    coreInfo.xCen_Add4 = "";
                }

                if (d1.Rows[0]["CEN_CITY"] != System.DBNull.Value)
                {
                    coreInfo.xCen_City = Convert.ToString(d1.Rows[0]["CEN_CITY"]);
                }
                else
                {
                    coreInfo.xCen_City = "";
                }

                if (d1.Rows[0]["CEN_PINCODE"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Pin = Convert.ToString(d1.Rows[0]["CEN_PINCODE"]);
                }
                else
                {
                    coreInfo.xCen_Pin = "";
                }

                if (d1.Rows[0]["CEN_DISTRICT"] != System.DBNull.Value)
                {
                    coreInfo.xCen_District = Convert.ToString(d1.Rows[0]["CEN_DISTRICT"]);
                }
                else
                {
                    coreInfo.xCen_District = "";
                }

                if (d1.Rows[0]["CEN_STATE"] != System.DBNull.Value)
                {
                    coreInfo.xCen_State = Convert.ToString(d1.Rows[0]["CEN_STATE"]);
                }
                else
                {
                    coreInfo.xCen_State = "";
                }

                if (d1.Rows[0]["CEN_COUNTRY"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Country = Convert.ToString(d1.Rows[0]["CEN_COUNTRY"]);
                }
                else
                {
                    coreInfo.xCen_Country = "";
                }

                string t1;
                string t2;
                if (d1.Rows[0]["CEN_TEL_NO_1"] != System.DBNull.Value)
                {
                    t1 = Convert.ToString(d1.Rows[0]["CEN_TEL_NO_1"]);
                }
                else
                {
                    t1 = "";
                }

                if (d1.Rows[0]["CEN_TEL_NO_2"] != System.DBNull.Value)
                {
                    t2 = Convert.ToString(d1.Rows[0]["CEN_TEL_NO_2"]);
                }
                else
                {
                    t2 = "";
                }

                if ((t2.Length > 0))
                {
                    coreInfo.xCen_Tel = (t1 + (", " + t2));
                }
                else
                {
                    coreInfo.xCen_Tel = t1;
                }

                string f1;
                string f2;
                if (d1.Rows[0]["CEN_FAX_NO_1"] != System.DBNull.Value)
                {
                    f1 = Convert.ToString(d1.Rows[0]["CEN_FAX_NO_1"]);
                }
                else
                {
                    f1 = "";
                }

                if (d1.Rows[0]["CEN_FAX_NO_2"] != System.DBNull.Value)
                {
                    f2 = Convert.ToString(d1.Rows[0]["CEN_FAX_NO_2"]);
                }
                else
                {
                    f2 = "";
                }

                if ((f2.Length > 0))
                {
                    coreInfo.xCen_Fax = (f1 + (", " + f2));
                }
                else
                {
                    coreInfo.xCen_Fax = f1;
                }

                string m1;
                string m2;
                if (d1.Rows[0]["CEN_MOB_NO_1"] != System.DBNull.Value)
                {
                    m1 = Convert.ToString(d1.Rows[0]["CEN_MOB_NO_1"]);
                }
                else
                {
                    m1 = "";
                }

                if (d1.Rows[0]["CEN_MOB_NO_2"] != System.DBNull.Value)
                {
                    m2 = Convert.ToString(d1.Rows[0]["CEN_MOB_NO_2"]);
                }
                else
                {
                    m2 = "";
                }

                if ((m2.Length > 0))
                {
                    coreInfo.xCen_Mob = (m1 + (", " + m2));
                }
                else
                {
                    coreInfo.xCen_Mob = m1;
                }

                string e1;
                string e2;
                if (d1.Rows[0]["CEN_EMAIL_ID_1"] != System.DBNull.Value)
                {
                    e1 = Convert.ToString(d1.Rows[0]["CEN_EMAIL_ID_1"]);
                }
                else
                {
                    e1 = "";
                }

                if (d1.Rows[0]["CEN_EMAIL_ID_2"] != System.DBNull.Value)
                {
                    e2 = Convert.ToString(d1.Rows[0]["CEN_EMAIL_ID_2"]);
                }
                else
                {
                    e2 = "";
                }

                if ((e2.Length > 0))
                {
                    coreInfo.xCen_Email = (e1 + (", " + e2));
                }
                else
                {
                    coreInfo.xCen_Email = e1;
                }

                // 
                if (d1.Rows[0]["CEN_WEBSITE_URL"] != System.DBNull.Value)
                {
                    coreInfo.xCen_Website = Convert.ToString(d1.Rows[0]["CEN_WEBSITE_URL"]);
                }
                else
                {
                    coreInfo.xCen_Website = "";
                }
                lpg.REC_EDIT_ON = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);

                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(coreInfo);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Core').hide();</script>");
            }
        }

        public ActionResult Frm_Core_Info_InstituteAccount_Grid_dx(bool VouchingMode = false, string ViewMode = "Default")
        {
            Core_User_Rights();

            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;


            {
                DataTable dInst = BASE._CoreDBOps.GetInstitutes();

                DataTable dCENTRE = BASE._CoreDBOps.GetCenterDetailsByBKPAD();

                var INSTData = from INST in dInst.AsEnumerable()
                               join CEN in dCENTRE.AsEnumerable()

                       on INST["INS_ID"] equals CEN["CEN_INS_ID"]
                               select new CoreInfoInstitute
                               {
                                   INS_NAME = Convert.ToString(INST["INS_NAME"]),
                                   CEN_NAME = Convert.ToString(CEN["CEN_NAME"]),
                                   CEN_UID = Convert.ToString(CEN["CEN_UID"]),
                                   CEN_INCHARGE = Convert.ToString(CEN["CEN_INCHARGE"]),
                                   ID = Convert.ToString(INST["INS_ID"])
                               };
                var Final_Data = INSTData.ToList();
                CoreInstitute = Final_Data;
            }

            return Content(JsonConvert.SerializeObject(CoreInstitute), "application/json");
        }

        public ActionResult Frm_Core_Info_Location_Grid_dx(bool VouchingMode = false, string ViewMode = "Default")
        {
            Core_User_Rights();

            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;


            DataTable locTable = BASE._CoreDBOps.GetLocations();

            var Final_Data = DatatableToModel.DataTabletoCoreLocationList(locTable);
            CoreLocation = Final_Data;

            return Content(JsonConvert.SerializeObject(CoreLocation), "application/json");
        }

        public ActionResult Frm_Institute_Export_Options_dx()
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('CoreInstitute_report_modal','Not Allowed','No Rights');$('#CoreModelListPreview').hide();</script>");
            }
            return PartialView();
        }

        public ActionResult Frm_Location_Export_Options_dx()
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('CoreLocation_report_modal','Not Allowed','No Rights');$('#CoreModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion

    }
}