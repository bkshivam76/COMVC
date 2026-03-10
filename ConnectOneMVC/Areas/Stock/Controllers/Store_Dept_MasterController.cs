using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common_Lib;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System.Data;
using static Common_Lib.DbOperations.StockDeptStores;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class Store_Dept_MasterController : BaseController
    {
        // GET: Stock/Store_Dept_Master
        #region Global Variables
        public List<Common_Lib.DbOperations.StockDeptStores.Return_GetRegister> StoreDeptMasterdata
        {
            get
            {
                return (List<Common_Lib.DbOperations.StockDeptStores.Return_GetRegister>)GetBaseSession("StoreDeptMasterdata_InfoStoreM");
            }
            set
            {
                SetBaseSession("StoreDeptMasterdata_InfoStoreM", value);
            }
        }
        public List<Return_Get_MainSubDept_Personnels> StoreDeptMaster_Incharge_DD_data
        {
            get
            {
                return (List<Return_Get_MainSubDept_Personnels>)GetBaseSession("StoreDeptMaster_Incharge_DD_data_StoreM");
            }
            set
            {
                SetBaseSession("StoreDeptMaster_Incharge_DD_data_StoreM", value);
            }
        }        
        #endregion

        #region Grid
        public ActionResult Frm_Store_Dept_Master_Info()
        {
            Dept_Store_user_rights();            
            if (CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "List"))//0000260 bug fixed
            {
                ViewBag.ShowHorizontalBar = 0;
                var StoreDeptMaster = BASE._StockDeptStores_dbops.GetRegister();
                if ((StoreDeptMaster == null))
                {
                    return PartialView("Frm_Store_Dept_Master_Info_Grid", null);
                }
                StoreDeptMasterdata = StoreDeptMaster;
                ViewData["StoreMasterExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["StoreMasterExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["StoreMasterExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(StoreDeptMaster);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Dept_Store_Master').hide();</script>");
            }
        }
        public PartialViewResult Frm_Store_Dept_Master_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            Dept_Store_user_rights();
            ViewData["StoreMasterExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["StoreMasterExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["StoreMasterExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (StoreDeptMasterdata == null || command == "REFRESH")
            {
                var StoreDeptMaster = BASE._StockDeptStores_dbops.GetRegister();
                if (StoreDeptMaster == null)
                {
                    return PartialView("Frm_Store_Dept_Master_Info_Grid", null);
                }
                StoreDeptMasterdata = StoreDeptMaster;
            }
            return PartialView("Frm_Store_Dept_Master_Info_Grid", StoreDeptMasterdata);
        }
        public ActionResult Store_Dept_MasterCustomDataAction(int key)
        {
            var FinalStore_Data = StoreDeptMasterdata as List<Common_Lib.DbOperations.StockDeptStores.Return_GetRegister>;
            var it = FinalStore_Data != null ? (Common_Lib.DbOperations.StockDeptStores.Return_GetRegister)FinalStore_Data.Where(f => f.ID == key).FirstOrDefault() : null;
            string StoreReg = "";
            if (it != null)
            {

                StoreReg = it.Add_By + "![" + it.Add_Date + "![" + it.Edit_By + "![" + it.Edit_Date + "![" + it.Store_Dept_Name + "![" + it.Close_Date + "![" + it.Mapped_Locations;
            }
            return GridViewExtension.GetCustomDataCallbackResult(StoreReg);
        }
        #endregion
        #region NEVD
        public JsonResult DataNavigation(string ActionMethod, int ID = 0, string Edit_By = null)
        {
            Model_Store_Dept_Master model = new Model_Store_Dept_Master();
            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                if (BASE._open_User_Is_Central_Store == false)
                {
                    if (BASE._open_User_ID.ToUpper() != Edit_By.ToUpper())
                    {
                        return Json(new { Message = "User Can Edit/Delete Store Or Dept Created By Self Only", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                int count = BASE._StockDeptStores_dbops.GetStoreDeptUsageCount(ID);
                if (count > 0)
                {
                    if (ActionMethod == "Edit")
                    {
                        return Json(new { Message = "Store/Dept Referred In Any Screen Cannot Be " + ActionMethod + "ed", result = false }, JsonRequestBehavior.AllowGet);
                    }//Mantis bug 0001195 fixed
                    else
                    {
                        return Json(new { Message = "Store/Dept Referred In Any Screen Cannot Be " + ActionMethod + "d", result = false }, JsonRequestBehavior.AllowGet);
                    }//Mantis bug 0001195 fixed
                }
            }
            if (ActionMethod == "AddIncharge")
            {
                if (BASE._open_User_Is_Central_Store == false)
                {
                    if (BASE._open_User_ID.ToUpper() != Edit_By.ToUpper())
                    {
                        return Json(new { Message = "User Can Change Inchage/Location of Store Or Dept Created By Self Only", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
                return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_Store_Dept_Master_Window(string ActionMethod, int ID = 0,string PostSucessFunction = null, string PopupName = "Dynamic_Content_popup", string ButtonID= "Store_Dept_MasterNew")
        {
            Dept_Store_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            
            Model_Store_Dept_Master model = new Model_Store_Dept_Master();
            model.ActionMethod = ActionMethod;
            model.PostSuccessFunction = PostSucessFunction != null ? PostSucessFunction : "Store_Dept_MasterAjaxSuccessForm";
            model.PopupName = PopupName != null ? PopupName : "Dynamic_Content_popup";
            if ((ActionMethod == "Edit") || (ActionMethod == "Delete") || (ActionMethod == "View"))
            {
                Return_Get_StoreDept_Detail storedept = BASE._StockDeptStores_dbops.Get_StoreDept_Detail(ID);
                model.Store_Dept_Name = storedept.Store_Dept_Name;
                model.Store_Category = storedept.Category;
                model.Store_Main_DeptID = storedept.Main_DeptID;
                model.Store_Sub_DeptID = storedept.Sub_DeptID;
                model.Store_Registeration_No = storedept.Registeration_No;
                model.Store_InchargeID = storedept.InchargeID;
                model.Store_ContactPersonID = storedept.ContactPersonID;
                model.Store_PremesisType = storedept.PremesisType;
                model.Store_PremesisNameID = storedept.PremesisID;
                model.Store_IsCentralStore = storedept.IsCentralStore;
                model.Store_MappedLocations = storedept.MappedLocations;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Store_Dept_Master_Window(Model_Store_Dept_Master model)
        {
            var ActionMethod = model.ActionMethod;
            try
            {
                if ((ActionMethod == "New") || (ActionMethod == "Edit"))
                {
                    if (string.IsNullOrWhiteSpace(model.Store_Dept_Name))
                    {
                        return Json(new
                        {
                            message = "Store/Dept Name cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Store_Dept_Name = model.Store_Dept_Name.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    if (model.Store_Remarks != null)
                    {
                        model.Store_Remarks = model.Store_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }

                    if (model.Store_Category == "Store" || model.Store_Category == "Sub Dept.")
                    {
                        if (model.Store_Main_DeptID == null)
                        {
                            return Json(new
                            {
                                Message = "Connecting Main Department Cannot Be Empty",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        

                        if (model.Store_InchargeID == null) // mantis bug #1243
                        {
                            var InchargeList = (List<Return_Get_MainSubDept_Personnels>)StoreDeptMaster_Incharge_DD_data;
                            if (InchargeList != null)
                            {
                                if (InchargeList.Count != 0)
                                {

                                    return Json(new
                                    {
                                        Message = "Incharge value should be filled..!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        // Checking Duplicate Record....
                        int duplicateCheck = BASE._StockDeptStores_dbops.GetStoreDeptNumber_UsageCountInstt(model.Store_Registeration_No,model.ID);
                        if (duplicateCheck > 0)
                        {
                            return Json(new
                            {
                                Message = "Duplicate. . . (" + model.Store_Registeration_No + ") Registration No./Store No. Already Available in this institute...!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (ActionMethod == "Edit")
                    {
                        //A Central Store can not be ticked “No” if it has been used in some condition where a normal Store cannot be used
                        if (model.Store_IsCentralStore == false)
                        {
                            int cen = BASE._StockDeptStores_dbops.Get_CentralStorespecific_usage_count(model.ID);
                            if (cen > 0)
                            {
                                return Json(new
                                {
                                    Message = "Central Store value can not be ticked “No” !!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);

                            }

                        }
                    }
                }
                if (ActionMethod == "New")
                {
                    Param_InsertStoreDept inparam = new Param_InsertStoreDept();
                    inparam.SD_Name = model.Store_Dept_Name;
                    inparam.Dept_Type = model.Store_Category;
                    if (model.Store_Sub_DeptID != null)
                    {
                        inparam.Sub_Dept_ID = model.Store_Sub_DeptID;
                    }
                   
                    inparam.Incharge_ID = model.Store_InchargeID;
                    inparam.Reg_No = model.Store_Registeration_No;
                    inparam.Contact_Person_ID = model.Store_ContactPersonID;
                    inparam.Remarks = model.Store_Remarks;
                    if (model.Store_Main_DeptID != null)
                    {
                        inparam.Dept_ID = model.Store_Main_DeptID;
                    }                 
                    inparam.Is_Central_Store = Convert.ToBoolean(model.Store_IsCentralStore);
                    inparam.Premesis_Type = model.Store_PremesisType == "Property" ? Premesis_Type.Service_Property : Premesis_Type.Service_Place;
                    inparam.Premesis_ID = model.Store_PremesisNameID;
                    if (model.Store_MappedLocations != null)
                    {
                        string[] LocationIDs = model.Store_MappedLocations.Split(',');
                        Param_InsertStoreDept_Mapping[] User_Selected_Mapped_Locations = new Param_InsertStoreDept_Mapping[LocationIDs.Length];
                        for (int i = 0; i < LocationIDs.Length; i++)
                        {
                            Param_InsertStoreDept_Mapping location = new Param_InsertStoreDept_Mapping();
                            location.Location_ID = LocationIDs[i].Trim();
                            location.Dept_ID = Convert.ToInt32(model.Store_Main_DeptID);
                            User_Selected_Mapped_Locations[i] = location;
                        }
                        inparam.mapped_Locations = User_Selected_Mapped_Locations;
                    }

                    if (BASE._StockDeptStores_dbops.InsertStoreDept(inparam))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SaveSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "Edit")
                {
                    Param_UpdateStoreDept inparam = new Param_UpdateStoreDept();
                    inparam.SD_Name = model.Store_Dept_Name;
                    inparam.Dept_Type = model.Store_Category;
                    if (model.Store_Sub_DeptID != null)
                    {
                        inparam.Sub_Dept_ID = model.Store_Sub_DeptID;
                    }
                    inparam.Incharge_ID = model.Store_InchargeID;
                    inparam.Reg_No = model.Store_Registeration_No;
                    inparam.Contact_Person_ID = model.Store_ContactPersonID;
                    inparam.Remarks = model.Store_Remarks;
                    if (model.Store_Main_DeptID != null)
                    {
                        inparam.Dept_ID = model.Store_Main_DeptID;
                    }
                    inparam.Is_Central_Store = Convert.ToBoolean(model.Store_IsCentralStore);
                    inparam.Premesis_Type = model.Store_PremesisType == "Property" ? Premesis_Type.Service_Property : Premesis_Type.Service_Place;
                    inparam.Premesis_ID = model.Store_PremesisNameID;
                    inparam.ID = model.ID;
                    if (model.Store_MappedLocations != null)
                    {
                        string[] LocationIDs = model.Store_MappedLocations.Split(',');
                        Param_InsertStoreDept_Mapping[] User_Selected_Mapped_Locations = new Param_InsertStoreDept_Mapping[LocationIDs.Length];
                        for (int i = 0; i < LocationIDs.Length; i++)
                        {
                            Param_InsertStoreDept_Mapping location = new Param_InsertStoreDept_Mapping();
                            location.Location_ID = LocationIDs[i].Trim();
                            location.Dept_ID = Convert.ToInt32(model.Store_Main_DeptID);
                            User_Selected_Mapped_Locations[i] = location;
                        }
                        inparam.mapped_Locations = User_Selected_Mapped_Locations;
                    }
                    if (BASE._StockDeptStores_dbops.UpdateStoreDept(inparam))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.UpdateSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "Delete")
                {                 

                    if (BASE._StockDeptStores_dbops.DeleteStoreDept(model.ID))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.DeleteSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);

                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_Store_Dept_Master_Closure_Window(int ID, string Store_Dept_Name)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Update"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            Store_Dept_Master_Closure model = new Models.Store_Dept_Master_Closure();
            model.Store_Dept_Name = Store_Dept_Name;
            model.SD_ID = ID;
            model.SD_Close_Date = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Store_Dept_Master_Closure_Window(Store_Dept_Master_Closure model)
        {         
            try
            {
                if (model.SD_Close_Remarks != null)
                {
                    model.SD_Close_Remarks = model.SD_Close_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                Param_CloseStoreDept Upparam = new Param_CloseStoreDept();
                Upparam.Store_Dept_ID = model.SD_ID;
                Upparam.Close_Date = Convert.ToDateTime(model.SD_Close_Date);
                Upparam.Close_Remarks = model.SD_Close_Remarks;
                if (BASE._StockDeptStores_dbops.CloseStoreDept(Upparam))
                {
                    return Json(new
                    {
                        Message ="Closed Successfully!!",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);

                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Store_ReOpen(int ID)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Update"))
            {
                return Json(new
                {
                    result = "NoUpdateRights",
                    Message = "Not Allowed..No Rights"
                }, JsonRequestBehavior.AllowGet);                
            }
            if (BASE._StockDeptStores_dbops.ReopenStoreDept(ID))
            {
                return Json(new
                {
                    Message = "Reopened Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }    
        public JsonResult CheckMappedLocationsCount(int DEPTID)
        {
            var locationlist = BASE._StockDeptStores_dbops.GetMappedLocations(DEPTID);
            if (locationlist.Count > 0)
            {
                return Json(new
                {
                    Message = "",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckLocationStockUseCount(string locationid)
        {
            var locationlist = BASE._StockDeptStores_dbops.GetStockCountForLocation(locationid);
            if (locationlist > 0)
            {
                return Json(new
                {
                    Message = "",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Frm_SDM_Add_Incharge(string ActionMethod, int ID = 0, string PostSucessFunction = null, string PopupName = "Dynamic_Content_popup")
        {
            Dept_Store_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }

            Model_Store_Dept_Master model = new Model_Store_Dept_Master();
            model.ActionMethod = ActionMethod;
            model.PostSuccessFunction = PostSucessFunction != null ? PostSucessFunction : "Store_Dept_MasterAjaxSuccessForm";
            model.PopupName = PopupName != null ? PopupName : "Dynamic_Content_popup";
            if (ActionMethod == "Edit") 
            {
                Return_Get_StoreDept_Detail storedept = BASE._StockDeptStores_dbops.Get_StoreDept_Detail(ID);
                model.Store_Dept_Name = storedept.Store_Dept_Name;
                model.Store_Category = storedept.Category;
                model.Store_Main_DeptID = storedept.Main_DeptID;
                model.Store_Sub_DeptID = storedept.Sub_DeptID;
                model.Store_Registeration_No = storedept.Registeration_No;
                model.Store_InchargeID = storedept.InchargeID;
                model.Store_ContactPersonID = storedept.ContactPersonID;
                model.Store_PremesisType = storedept.PremesisType;
                model.Store_PremesisNameID = storedept.PremesisID;
                model.Store_IsCentralStore = storedept.IsCentralStore;
                model.Store_MappedLocations = storedept.MappedLocations;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_SDM_Add_Incharge(Model_Store_Dept_Master model)
        {
            var ActionMethod = model.ActionMethod;
            try
            {
                if (ActionMethod == "Edit")
                {


                    if (model.Store_InchargeID == null) // mantis bug #1243
                    {
                        var InchargeList = (List<Return_Get_MainSubDept_Personnels>)StoreDeptMaster_Incharge_DD_data;
                        if (InchargeList != null)
                        {
                            if (InchargeList.Count != 0)
                            {

                                return Json(new
                                {
                                    Message = "Incharge value should be filled..!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }



                    }

                   
                }
                
                if (ActionMethod == "Edit")
                {
                    Param_UpdateStoreDept inparam = new Param_UpdateStoreDept();
                    inparam.SD_Name = model.Store_Dept_Name;
                    inparam.Dept_Type = model.Store_Category;
                    if (model.Store_Sub_DeptID != null)
                    {
                        inparam.Sub_Dept_ID = model.Store_Sub_DeptID;
                    }
                    inparam.Incharge_ID = model.Store_InchargeID;
                    inparam.Reg_No = model.Store_Registeration_No;
                    inparam.Contact_Person_ID = model.Store_ContactPersonID;
                    inparam.Remarks = model.Store_Remarks;
                    if (model.Store_Main_DeptID != null)
                    {
                        inparam.Dept_ID = model.Store_Main_DeptID;
                    }
                    inparam.Is_Central_Store = Convert.ToBoolean(model.Store_IsCentralStore);
                    inparam.Premesis_Type = model.Store_PremesisType == "Property" ? Premesis_Type.Service_Property : Premesis_Type.Service_Place;
                    inparam.Premesis_ID = model.Store_PremesisNameID;
                    inparam.ID = model.ID;
                    if (model.Store_MappedLocations != null)
                    {
                        string[] LocationIDs = model.Store_MappedLocations.Split(',');
                        Param_InsertStoreDept_Mapping[] User_Selected_Mapped_Locations = new Param_InsertStoreDept_Mapping[LocationIDs.Length];
                        for (int i = 0; i < LocationIDs.Length; i++)
                        {
                            Param_InsertStoreDept_Mapping location = new Param_InsertStoreDept_Mapping();
                            location.Location_ID = LocationIDs[i].Trim();
                            location.Dept_ID = Convert.ToInt32(model.Store_Main_DeptID);
                            User_Selected_Mapped_Locations[i] = location;
                        }
                        inparam.mapped_Locations = User_Selected_Mapped_Locations;
                    }
                    if (BASE._StockDeptStores_dbops.UpdateStoreDept(inparam))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.UpdateSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);

                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Store_Dept_Master_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
     
        #endregion
        #region DropDown
        public ActionResult LookUp_GetConnMain_Depts(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var deptlist = BASE._StockDeptStores_dbops.GetMainDeptList(ClientScreen.Stock_Dept_Store_Master,true);//Mantis bug 0000427 fixed
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(deptlist, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetConnSub_Depts(bool? IsVisible, DataSourceLoadOptions loadOptions, int? MainDeptID)
        {
            if (MainDeptID != null)
            {
                var ConnSubDept = BASE._StockDeptStores_dbops.GetSubDeptList(Common_Lib.RealTimeService.ClientScreen.Stock_Dept_Store_Master, MainDeptID);
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ConnSubDept, loadOptions)), "application/json");
            }
            else
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_GetStoreDept>(), loadOptions)), "application/json");
            }
        }
        public ActionResult LookUp_Dept_Incharge(bool? IsVisible, DataSourceLoadOptions loadOptions, int? MainDeptID = null , bool DD_Refresh = false)
        {//here 2 functions were used previously based on main dept. value, we modified Get_MainSubDept_Personnels function to serve the purpose instead of using 2 functions.
            if (MainDeptID == 0)
            {
                MainDeptID = null;
            }

            if (StoreDeptMaster_Incharge_DD_data == null ||  MainDeptID != null || DD_Refresh) 
            {
              
                var Depincharge = BASE._StockDeptStores_dbops.Get_MainSubDept_Personnels(MainDeptID);
                if (Depincharge.Count == 0)
                {
                    StoreDeptMaster_Incharge_DD_data = null;
                }
                else
                {
                    StoreDeptMaster_Incharge_DD_data = Depincharge;
                }
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Depincharge, loadOptions)), "application/json");
                
            }
            else
            {
                var InchargeList = (List<Return_Get_MainSubDept_Personnels>)StoreDeptMaster_Incharge_DD_data;
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(InchargeList, loadOptions)), "application/json");

            }

        }//Mantis bug 0000298 fixed

        
        public ActionResult LookUp_Dept_Contact(bool? IsVisible, DataSourceLoadOptions loadOptions, int? MainDeptID)
        {
            if (MainDeptID == null || MainDeptID == 0)//Mantis bug 0000309 fixed
            {
                var incharge = BASE._user_order_DBOps.GetPersonnels();
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(incharge, loadOptions)), "application/json");
            }
            else
            {                
                var Depincharge = BASE._StockDeptStores_dbops.Get_MainSubDept_Personnels(Convert.ToInt32(MainDeptID));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Depincharge, loadOptions)), "application/json");                
            }
        }
        public ActionResult LookUp_Getpremises(bool? IsVisible, DataSourceLoadOptions loadOptions, string PremisisType)
        {
            if (PremisisType == null)
            {
                var premisisList = new List<Return_GetStoreDeptPremesis>();
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(premisisList, loadOptions)), "application/json");
            }
            var premisestypes = BASE._StockDeptStores_dbops.GetStoreDeptPremesis(PremisisType);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(premisestypes, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetLocationList(DataSourceLoadOptions loadOptions, string Premesis)
        {
            string PremesisType = null;
            string PremesisName = null;
            if (Premesis != null)
            {
                if (Premesis.Length > 0)
                {
                    if (Premesis.Split('|').Length > 0)
                    {
                        PremesisType = Premesis.Split('|')[0];
                        PremesisName = Premesis.Split('|')[1];
                    }
                }
            }
            List<Return_GetLocations> _RetList = new List<Return_GetLocations>();
            if (PremesisType == "Property" && PremesisName != null)
            {
                if (PremesisName.Length > 0) _RetList = BASE._StockDeptStores_dbops.GetPropertyLocations(PremesisName);
            }
            if (PremesisType == "Service Place" && PremesisName != null)
            {
                if (PremesisName.Length > 0) _RetList = BASE._StockDeptStores_dbops.GetServicePlaceLocations(PremesisName);
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(_RetList, loadOptions)), "application/json");

        }

        #endregion
        #region MISC
        public void Sessionclear()
        {
            //Session.Remove("StoreDeptMaster_Incharge_DD_data");
            ClearBaseSession("_StoreM");
        } //clears session variable on popup close    
        public void InfoSessionclear()
        {
            ClearBaseSession("_InfoStoreM");
        }
        public void Dept_Store_user_rights()
        {
            ViewData["Dept_Store_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Add");
            ViewData["Dept_Store_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Update");
            ViewData["Dept_Store_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "View");
            ViewData["Dept_Store_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Delete");
            ViewData["Dept_Store_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Export");
            ViewData["Dept_Store_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Report");
            ViewData["Dept_Store_ListRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "List");
            ViewData["Dept_Store_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
        }
        #endregion

    }
}
