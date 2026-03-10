using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using ConnectOneMVC.Areas.Profile.Models;
using DevExtreme.AspNet.Data;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class CoreLocationController : BaseController
    {
        // GET: Profile/CoreLocation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataNavigation(string ActionMethod, string ID, string YearID, string ActionStatus, DateTime? EditDate)
        {
            if (!(BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock))  && (ActionMethod == "LOCKED" || ActionMethod == "UNLOCKED"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights')</script>");//Code written for User Authorization do not remove
            }
            if ((!CheckRights(BASE,ClientScreen.Profile_Core,"Update")) && ActionMethod == "Edit")
            {
                return Json(new { result = "NoEditRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Delete")) && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "View")) && ActionMethod == "View")
            {
                return Json(new { result = "NoViewRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (BASE.AllowMultiuser())
                {

                    if ((ActionMethod == "Edit"))
                    {
                        string xTemp_ID = ID;
                        DataTable d1 = BASE._AssetLocDBOps.GetRecord(xTemp_ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                        if ((d1 == null))
                        {
                            return Json(new { Message = "ERROR!", result = false }, JsonRequestBehavior.AllowGet);
                        }

                        if ((d1.Rows.Count == 0))
                        {
                            return Json(new { Message = Common_Lib.Messages.RecordChanged("Current Location") + " " + "Record Changed / Removed in Background!!", result = false }, JsonRequestBehavior.AllowGet);
                        }

                        var RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    }
                }

                CoreProfile model = new CoreProfile();
                var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                model.ActionMethod = Tag;
                // ---------------------------------------------------------
                if (ActionMethod == "New")
                {
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "Edit")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._AssetLocDBOps.GetStatus(xTemp_ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background... !", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new { Message = ("Locked Entry can not be Edited / Deleted... !" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))), result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if ((BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) && (BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.Auditor.ToUpper()))
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            return Json(new { Message = "Location  Cannot be Edited After The Completion  Of Final Audit.. !!", result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                    {
                        return Json(new { Message = "Locations Cannot be Edited . . ! \r\n \r\n Updation/Rematching of Locations in Current year can be done only after finalization of previous year accounts....! ", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    model.ID = ID;
                    model.EditDate = Convert.ToDateTime(EditDate);

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed))
                    {
                        model.Chk_Incompleted = false;
                    }
                    else
                    {
                        model.Chk_Incompleted = true;
                    }
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);

                }
                else if (ActionMethod == "Delete")
                {

                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._AssetLocDBOps.GetStatus(xTemp_ID, Common_Lib.RealTimeService.ClientScreen.Profile_Core);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background...!", result = false }, JsonRequestBehavior.AllowGet);
                    }

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new { Message = ("Locked Entry can not be  Edit/ Delete...!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))), result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if ((BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) && (BASE._open_User_Type.ToUpper() != Common_Lib.Common.ClientUserType.Auditor.ToUpper()))
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            return Json(new { Message = ("Location  Cannot be Deleted After The Completion  Of Final Audit.. !!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))), result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                    {
                        return Json(new { Message = ("Locations Cannot be Deleted..!/n Deletions of Locations in Current year can be done only after finalization of previous year accounts....! "), result = false }, JsonRequestBehavior.AllowGet);
                    }

                    bool DeleteAllow = true;
                    string UsedPage = "";
                    UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(xTemp_ID, false);
                    if (UsedPage.Length > 0)
                    {
                        if (DeleteAllow)
                        {
                            return Json(new { Message = "Can't Delete...!  " + ("<br/>" + ("<br/>" + ("This information is  beingused in Another Page...!" + ("<br/>" + ("<br/>" + ("Name : " + UsedPage)))))), result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Message = "Delete", result = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "View")
                {
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._telephoneDBOps.GetStatus(xTemp_ID);
                    if ((MaxValue == null))
                    {
                        return Json(new { Message = "Entry Not Found / Changed In Background...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult frm_CoreLocation_window(string ActionMethod = null, string ID = null, string EditedOn = null, string locationDropdownRefreshFunction = null)
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Core, "Add")) && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_CoreLocation_Window','Not Allowed','No Rights');$('#LocationModelNew').hide();</script>");
            }

            CoreProfile model = new Models.CoreProfile();
            DateTime? EditDate = Convert.ToDateTime(EditedOn);
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            model.locationDropdownRefreshFunction = locationDropdownRefreshFunction;
            model.Max_Capacity_core = 0;
            if (Navigation_Mode_tag.ToString() == "_New")
            {
                if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_CoreLocation_Window','Locations Cannot be Added<br>Addition of Locations in Current year can be done only after finalization of previous year accounts....!','Information...');</script>");
                }
                if ((!(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) && !(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
                {
                    if (BASE._CenterDBOps.IsFinalAuditCompleted())
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_CoreLocation_Window','Locations Cannot be Added After The Completion Of Final Audit. . !','Alert');</script>");
                        //Details: Below JSON will be received by pop-up but it will displayed as simple JSON string. So replaced by content (as above).
                        //return Json(new
                        //{
                        //    Message = "Locations Cannot be Added After The Completion Of Final Audit. . !",
                        //    State = "1",
                        //    result = false
                        //}, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {

                LOCATIONINFO locationInfo = new Models.LOCATIONINFO();
                model.EditDate = Convert.ToDateTime(EditedOn);

                DataTable _dtableLocData = BASE._AssetLocDBOps.GetRecord(ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if ((_dtableLocData == null))
                {

                }
                else
                {
                    locationInfo = DatatableToModel.DataTabletoLOCATIONINFO(_dtableLocData).FirstOrDefault();
                }

                // -----------------------------+
                // Start : Check if entry already changed 
                // -----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                                || ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                                || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))))
                    {
                        string viewstr = "";
                        if ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))
                        {
                            viewstr = "view";
                        }

                        if (EditDate != null)
                        {
                            if ((EditDate != Convert.ToDateTime(_dtableLocData.Rows[0]["REC_EDIT_ON"])))
                            {
                                ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current LocationInfo", viewstr));
                            }
                        }

                    }

                }

                // -----------------------------+
                // End : Check if entry already changed 
                //-----------------------------+
                model.EditDate = Convert.ToDateTime(locationInfo.REC_EDIT_ON);
                model.LocationName = locationInfo.LocationName;                                
                model.otherDetails = locationInfo.otherDetails;
                model.TempActionMethod = Navigation_Mode_tag.ToString();
                model.ActionMethod = Navigation_Mode_tag;
                model.OldLocationName = locationInfo.LocationName;
                model.AC_or_NONAC = locationInfo.AC_or_NonAC;
                model.Category = locationInfo.Category;
                model.floor = locationInfo.roomfloor;
                model.Max_Capacity_core = locationInfo.max_Capacity;
                model.ID = ID;
                model.ServicePlace_ID = _dtableLocData.Rows[0]["SP_REC_ID"].ToString();
                model.Property_ID = _dtableLocData.Rows[0]["LB_REC_ID"].ToString();
                model.MatchedLocationName = model.LocationName;
                model.MatchedOtherDetails = model.otherDetails;

            }

            if (((Navigation_Mode_tag) == Common_Lib.Common.Navigation_Mode._Delete))
            {
                model.Chk_Incompleted = false;
            }

            if (((Navigation_Mode_tag) == Common_Lib.Common.Navigation_Mode._View))
            {
                model.Chk_Incompleted = false;
            }
            return PartialView("~/Areas/Profile/Views/Core/frm_CoreLocation_window.cshtml", model);
        }
        [HttpPost]
        public ActionResult frm_CoreLocation_window(CoreProfile model)
        {
            var ServiceId = model.ServicePlace_ID == null ? "" : model.ServicePlace_ID;
            var PropertyId = model.Property_ID == null ? "" : model.Property_ID;
            bool Status;
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            if (BASE.AllowMultiuser())
            {
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                            || (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)))
                {
                    DataTable location_Dbop = BASE._AssetLocDBOps.GetRecord(model.ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if ((location_Dbop == null))
                    {
                        return Json(new
                        {
                            Message = "something went wrong!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if ((location_Dbop.Rows.Count == 0))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.RecordChanged("Current Location") + " Record Already Changed !!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }
                    model.LastEditedOn = Convert.ToDateTime(location_Dbop.Rows[0]["REC_EDIT_ON"]);
                    if ((model.LastEditedOn != Convert.ToDateTime(location_Dbop.Rows[0]["REC_EDIT_ON"])))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.RecordChanged("Current Location Info") + " Record Already Changed !!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    object MaxValue = 0;
                    MaxValue = BASE._AssetLocDBOps.GetStatus(model.ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Entry Not Found...!" + " Record Already Changed !!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }


                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = ("Locked Entry can not be Edited / Deleted...!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete))
                    {
                        MaxValue = 0;
                        // check if Telephone info is used in any txn(payment voucher) #Ref AC23
                        MaxValue = BASE._telephoneDBOps.GetCountInTxn(Common_Lib.RealTimeService.ClientScreen.Profile_Telephone, model.ID);
                        if ((MaxValue == null))
                        {
                            return Json(new
                            {
                                Message = ("Locked Entry can not be Edited / Deleted...!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (((int)MaxValue > 0))
                        {
                            // D/AE
                            return Json(new
                            {
                                Message = ("Can't Delete ...!" + ("<br/>" + ("<br/>" + "This information is being used in Another Page...!"))),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {

                    // Checking Duplicate Record....
                    int? MaxValue = null;
                    int xID = 0;
                    var MaxValuestring = (BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(model.LocationName, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation));
                    if (!string.IsNullOrEmpty(MaxValuestring.ToString()))
                    {
                        MaxValue = (int)MaxValuestring;
                    }
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Something went wrong!!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((int)MaxValue != 0) && (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        return Json(new
                        {
                            Message = "Duplicate. . . (" + model.LocationName + ")Same Location Name. Already Available...!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((((int)MaxValue != 0) && (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                    {
                        //need to change
                        if (model.OldLocationName != model.LocationName)
                        {
                            return Json(new
                            {
                                Message = "Duplicate. . . (" + model.LocationName + ")Same Location Name Already Available...!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                else
                {

                }
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)))
                {
                    DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                    if (!(LocNames == null))
                    {
                        if ((LocNames.Rows.Count > 0))
                        {
                            for (int I = 0; I <= (LocNames.Rows.Count - 1); I++)
                            {
                                if ((model.LocationName.ToUpper().Trim() == LocNames.Rows[I][0].ToString().ToUpper()))
                                {
                                    return Json(new
                                    {
                                        Message = "Location Name. . . (" + model.LocationName + ") Referred Record Already Changed, Location with same name already exists in pending Transfers!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                    }
                }

                // CHECKING LOCK STATUS
                if (((model.ActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (BASE.IsInsuranceAudited())
                    {
                        return Json(new { Message = "Location  Re Matching Cannot Be Done After Completion of Insurance Audit", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    int? MaxValue = 0;
                    var MaxValuestring = BASE._AssetLocDBOps.GetStatus(model.ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    if (!string.IsNullOrEmpty(MaxValuestring.ToString()))
                    {
                        MaxValue = (int)MaxValue;
                    }
                    if ((MaxValue == null))
                    {
                        return Json(new
                        {
                            Message = "Entry Not Found...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = ("Locked Entry can not be Edit / Delete...!" + ("<br/>" + ("<br/>" + ("Note:" + ("<br/>" + ("-------" + ("<br/>" + ("Drop your Request to Madhuban for Unlock this Entry," + ("<br/>" + "If you really want to do some action...!"))))))))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                int Status_Action;
                if (model.Chk_Incompleted)
                {
                    Status_Action = Convert.ToInt16(Common_Lib.Common.Record_Status._Incomplete);
                }
                else
                {
                    Status_Action = Convert.ToInt16(Common_Lib.Common.Record_Status._Completed);
                }
                if ((model.ActionMethod == Common.Navigation_Mode._Delete))
                {
                    Status_Action = Convert.ToInt16(Common_Lib.Common.Record_Status._Deleted);
                }
                //To Save New Method
                if (((model.ActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    // new

                    if (model.RAD_PropertyService == "Property")
                    {
                        Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
                        PropParam.Next_YearID = BASE._next_Unaudited_YearID;
                        PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
                        PropParam.YearID = BASE._open_Year_ID;
                        PropParam.Asset_RecID = PropertyId;
                        var PropertyList = BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);
                        if ((PropertyList == null))
                        {
                            BASE.HandleDBError_OnNothingReturned();
                            return Json(new
                            {
                                Message = "Error ",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((PropertyList.Count <= 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.DependencyChanged("Property") + "Referred Record Already Sold/Transferred/Deleted!!",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //else if ((Mapping_Window.Look_ProList.GetColumnValue("REC_EDIT_ON") != PropertyList.Rows[0]["REC_EDIT_ON"]))
                        //{
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Property"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    this.DialogResult = Windows.Forms.DialogResult.Retry;
                        //    FormClosingEnable = false;
                        //    this.Close();
                        //    return;
                        //}
                    }
                    if (model.RAD_PropertyService=="Service Place")
                    {
                        // Service Place
                        DataTable d1 = BASE._ServPlacesDBOps.GetRecord(ServiceId);
                        if ((d1 == null))
                        {
                            BASE.HandleDBError_OnNothingReturned();
                            return Json(new
                            {
                                Message = "Error ",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((d1.Rows.Count <= 0))
                        {
                            // SP is deleted in the background

                            return Json(new
                            {
                                Message = Common_Lib.Messages.DependencyChanged("Service Place") + "Referred Record Already Sold/Transferred/Deleted!!",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            // SP has been edited
                            //if ((Mapping_Window.Look_SerList.GetColumnValue("REC_EDIT_ON") != d1.Rows[0]["REC_EDIT_ON"]))
                            //{
                            //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Service Place"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    this.DialogResult = Windows.Forms.DialogResult.Retry;
                            //    FormClosingEnable = false;
                            //    this.Close();
                            //    return;
                            //}

                        }

                    }
                    var locationNameInsert = model.LocationName == null ? "" : model.LocationName.Trim();
                    var otherdetailsInsert = model.otherDetails == null ? "" : model.otherDetails.Trim();
                    var maxCapacity = model.Max_Capacity_core;
                    var ac_nonac = model.AC_or_NONAC;
                    var category = model.Category;
                    var floor = model.floor;
                    Status = BASE._AssetLocDBOps.Insert(Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation, locationNameInsert, otherdetailsInsert, "1", ServiceId, PropertyId, maxCapacity
                        , ac_nonac, category, floor);
                    model.Result = 1;
                    return Json(new
                    {
                        Message = "Saved successfully. . . ",
                        EventType = 1,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                    //return PartialView("~/Areas/Profile/Views/Core/frm_CoreLocation_window.cshtml", model);
                }
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (model.RAD_PropertyService == "Property")
                    {
                        Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
                        PropParam.Next_YearID = BASE._next_Unaudited_YearID;
                        PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
                        PropParam.YearID = BASE._open_Year_ID;
                        PropParam.Asset_RecID = PropertyId;
                        var PropertyList = BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);
                        if ((PropertyList == null))
                        {
                            BASE.HandleDBError_OnNothingReturned();
                            return Json(new
                            {
                                Message = "Error ",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((PropertyList.Count <= 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.DependencyChanged("Property") + "Referred Record Already Sold/Transferred/Deleted!!",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //else if ((Mapping_Window.Look_ProList.GetColumnValue("REC_EDIT_ON") != PropertyList.Rows[0]["REC_EDIT_ON"]))
                        //{
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Property"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    this.DialogResult = Windows.Forms.DialogResult.Retry;
                        //    FormClosingEnable = false;
                        //    this.Close();
                        //    return;
                        //}
                    }
                    if (model.RAD_PropertyService == "Service Place")
                    {
                        // Service Place
                        DataTable d1 = BASE._ServPlacesDBOps.GetRecord(ServiceId);
                        if ((d1 == null))
                        {
                            BASE.HandleDBError_OnNothingReturned();
                            return Json(new
                            {
                                Message = "Error ",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((d1.Rows.Count <= 0))
                        {
                            // SP is deleted in the background

                            return Json(new
                            {
                                Message = Common_Lib.Messages.DependencyChanged("Service Place") + "Referred Record Already Sold/Transferred/Deleted!!",
                                EventType = 1,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            // SP has been edited
                            //if ((Mapping_Window.Look_SerList.GetColumnValue("REC_EDIT_ON") != d1.Rows[0]["REC_EDIT_ON"]))
                            //{
                            //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Service Place"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    this.DialogResult = Windows.Forms.DialogResult.Retry;
                            //    FormClosingEnable = false;
                            //    this.Close();
                            //    return;
                            //}

                        }

                    }
                    var locationNameUpdate = model.LocationName.Trim() == null ? "" : model.LocationName.Trim();
                    var otherdetailsUpdate = model.otherDetails == null ? "" : model.otherDetails.Trim();
                    var ac_nonac = model.AC_or_NONAC;
                    var category = model.Category;
                    var floor = model.floor;
                    var maxCapacity = model.Max_Capacity_core;
                    Status = BASE._AssetLocDBOps.Update_Global(Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation, locationNameUpdate, otherdetailsUpdate, model.RAD_PropertyService.ToUpper(), PropertyId, ServiceId, model.ID,
                        maxCapacity,ac_nonac,category,floor);
                    model.Result = 2;
                    return Json(new
                    {
                        Message = "Updated Successfully",
                        EventType = 2,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    Status = BASE._AssetLocDBOps.Delete(model.ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                    model.Result = 3;
                    return Json(new
                    {
                        Message = "Deleted successfully. . . ",
                        EventType = 3,
                        State = 1,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            return PartialView("~/Areas/Profile/Views/Core/frm_CoreLocation_window.cshtml", model);
        }
        public ActionResult LookUp_GetProList(DataSourceLoadOptions loadOptions)
        {
            Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP PropParam = new Common_Lib.RealTimeService.Param_LandAndBuilding_Get_Location_Property_ListingBySP();
            PropParam.Next_YearID = BASE._next_Unaudited_YearID;
            PropParam.Prev_YearId = BASE._prev_Unaudited_YearID;
            PropParam.CEN_BK_PAD_NO = BASE._open_PAD_No_Main;
            PropParam.YearID = BASE._open_Year_ID;
            var prop = BASE._L_B_DBOps.Get_Location_Property_ListingBySP(PropParam);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(prop, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetSerList(DataSourceLoadOptions loadOptions)
        {
            var spList = BASE._ServPlacesDBOps.GetAllServicePlaceList(Common_Lib.RealTimeService.ClientScreen.Profile_Core);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(spList, loadOptions)), "application/json");
        }
        public ActionResult frm_CoreMatchedLocation_window(string ID = "", string ActionMethod = "", DateTime? LastEditOn = null, string LocationName = "", 
            string OtherDetails = "", Int32 maxCapacity = 0, string old_LocationName="", string AC_or_NONAC = "", string Category = "", string Floor = "")
        {
            CoreProfile model = new CoreProfile();
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            if (ID != "")
            {
                DataTable _dtableLocData = BASE._AssetLocDBOps.GetRecord(ID, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                model.ID = ID;
                model.LastEditedOn = LastEditOn;

                model.ActionMethod = Tag;
                model.TempActionMethod = ActionMethod;
                model.LocationName = LocationName;
                model.OldLocationName = old_LocationName;
                model.otherDetails = OtherDetails;
                model.Max_Capacity_core = maxCapacity;
                model.AC_or_NONAC = AC_or_NONAC;
                model.Category = Category;
                model.floor = Floor;
                model.Property_ID = _dtableLocData.Rows[0]["LB_REC_ID"].ToString() == "" ? "" : _dtableLocData.Rows[0]["LB_REC_ID"].ToString();
                model.ServicePlace_ID = _dtableLocData.Rows[0]["SP_REC_ID"].ToString() == "" ? "" : _dtableLocData.Rows[0]["SP_REC_ID"].ToString();
                if (_dtableLocData.Rows[0]["SP_REC_ID"].ToString() != "")
                {
                    model.RAD_PropertyService = "Service Place";
                    model.IsDisabledProp = true;
                    model.IsDisabledServ = false;
                }
                else
                {
                    model.RAD_PropertyService = "Property";
                    model.IsDisabledServ = true;
                    model.IsDisabledProp = false;
                }
            }
            else
            {
                model.ID = "";
                model.ActionMethod = Tag;
                model.TempActionMethod = ActionMethod;
                model.LocationName = LocationName;
                model.OldLocationName = old_LocationName;
                model.otherDetails = OtherDetails;
                model.ServicePlace_ID = "";
                model.RAD_PropertyService = "Property";
                model.Max_Capacity_core = maxCapacity;
                model.AC_or_NONAC = AC_or_NONAC;
                model.Category = Category;
                model.floor = Floor;
                //model.SP_ID = SP_IDDTML == null ? "" : SP_IDDTML;
                //model.Property_ID = "Property";
                if (model.RAD_PropertyService == "Property")
                {
                    model.IsDisabledServ = true;
                    model.IsDisabledProp = false;
                }
            }

            //}


            return PartialView("~/Areas/Profile/Views/Core/frm_CoreMatchedLocation_window.cshtml", model);
        }
        public JsonResult DataNavigationTLocationMatching(Common_Lib.Common.Navigation_Mode ActionMethod, string Id = "", string ActionStatus = "", string EditDate = "", string LocationName = "",
            string OtherDetails = "", string RAD_PropertyService = "", string PropertyDDL = "", string ServiceDDL = "", string oldLocationName = "", string ac_nonac = "", string category = "",
            string Floor = "")
        {
            CoreProfile model = new CoreProfile();

            var tag = ActionMethod;
            object MaxValue = 0;
            int xID = 0;
            if (tag.ToString() == "_Edit")
            {
                MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(LocationName, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (MaxValue.ToString() != "0" && tag.ToString() == "_Edit")
                {
                    if (LocationName.ToUpper().Trim() != oldLocationName.ToUpper().Trim())
                    {
                        return Json(new { Message = "Same Location Already Available . . . --> Edit Location..", LocationMache = "1", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (tag.ToString() == "_New")
            {
                MaxValue = BASE._AssetLocDBOps.GetRecordCountByName_CurrentUID(LocationName, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (MaxValue.ToString() != "0" && tag.ToString() == "_New")
                {
                    return Json(new { Message = "Same Location Already Available . . . !", LocationMache = "1", result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            if (MaxValue == null)
            {
                return Json(new { Message = "E n t r y   N o t   F o u n d . . . !", LocationMache = "1", result = false }, JsonRequestBehavior.AllowGet);
            }
            if (MaxValue.ToString() == Common_Lib.Common.Record_Status._Locked.ToString())
            {
                return Json(new { Message = "L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !/n/n Note:Drop your Request to Madhuban for Unlock this Entry, If you really want to do some action...!", LocationMache = "1", result = false }, JsonRequestBehavior.AllowGet);
            }
            if (Id != "")
            {
                DataTable _dtableLocData = BASE._AssetLocDBOps.GetRecord(Id, Common_Lib.RealTimeService.ClientScreen.Core_Add_AssetLocation);
                if (ActionMethod.ToString() != "_New")
                {
                    string ServicePlace_ID = _dtableLocData.Rows[0]["SP_REC_ID"].ToString() == "" ? "" : _dtableLocData.Rows[0]["SP_REC_ID"].ToString();
                    string Property_ID = _dtableLocData.Rows[0]["LB_REC_ID"].ToString() == "" ? "" : _dtableLocData.Rows[0]["LB_REC_ID"].ToString();
                    string RecEditDate = _dtableLocData.Rows[0]["REC_EDIT_ON"].ToString() == "" ? "" : _dtableLocData.Rows[0]["REC_EDIT_ON"].ToString();
                    DateTime? LastEditedOn = Convert.ToDateTime(RecEditDate);
                    var radiopropEdit = "";
                    if (RAD_PropertyService == "")
                    {

                        if (Property_ID != "")
                        {
                            radiopropEdit = "Property";
                        }
                        else
                        {
                            radiopropEdit = "Service Place";
                        }
                    }
                    else
                    {
                        radiopropEdit = RAD_PropertyService;
                    }
                    var matchedValuues = new
                    {
                        locationNameDTLM = LocationName,
                        oldLocationNameDTLM = oldLocationName,
                        otherDetailsDTLM = OtherDetails,
                        actionMethodDTLM = tag,
                        sp_IdDTLM = ServicePlace_ID.ToString(),
                        lb_IdDTLM = Property_ID.ToString(),
                        IDDTML = Id,
                        LastEditOnDDTML = LastEditedOn,
                        RAD_PropertyService = radiopropEdit,
                        ac_nonacDTLM = ac_nonac,
                        categoryDTLM = category,
                        FloorDTLM = Floor,
                    };
                    return Json(new { Message = "", LocationMache = matchedValuues, result = true }, JsonRequestBehavior.AllowGet);
                }
            }

            var radioprop = RAD_PropertyService == "" ? "Property" : RAD_PropertyService;
            var DDLValPropOrSer = "";
            if (radioprop == "Property")
            {
                DDLValPropOrSer = PropertyDDL;
            }
            else
            {
                DDLValPropOrSer = ServiceDDL;
            }
            var matchedValuues1 = new
            {
                locationNameDTLM = LocationName,
                otherDetailsDTLM = OtherDetails,
                actionMethodDTLM = tag,
                sp_IdDTLM = "",
                lb_IdDTLM = "",
                IDDTML = "",
                LastEditOn = "",
                RAD_PropertyService = radioprop,
                ac_nonacDTLM = ac_nonac,
                categoryDTLM = category,
                FloorDTLM = Floor,
            };
            return Json(new { Message = "", LocationMache = matchedValuues1, result = true }, JsonRequestBehavior.AllowGet);

        }
  
    }
}