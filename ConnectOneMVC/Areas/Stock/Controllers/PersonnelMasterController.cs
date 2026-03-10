using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Personnels;
using ConnectOneMVC.Controllers;
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
using ConnectOneMVC.Areas.Stock.Models;
using Common_Lib;
using System.Collections;
using static Common_Lib.DbOperations.StockDeptStores;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class PersonnelMasterController : BaseController
    {
        // GET: Stock/PersonnelMaster
        #region Global Variables
        public List<Return_GetRegister_MainGrid> PersonnelMasterGridData
        {
            get
            {
                return (List<Return_GetRegister_MainGrid>)GetBaseSession("PersonnelMasterGridData_PMInfo");
            }
            set
            {
                SetBaseSession("PersonnelMasterGridData_PMInfo", value);
            }
        }
        public List<Return_GetRegister_NestedGrid> PersonnelNested_ExportData
        {
            get
            {
                return (List<Return_GetRegister_NestedGrid>)GetBaseSession("PersonnelNested_ExportData_PMInfo");
            }
            set
            {
                SetBaseSession("PersonnelNested_ExportData_PMInfo", value);
            }
        }
        public List<Return_GetStoreDept> PersonnelMaster_MainDepartment_DD_data
        {
            get
            {
                return (List<Return_GetStoreDept>)GetBaseSession("PersonnelMaster_MainDepartment_DD_data_PM");
            }
            set
            {
                SetBaseSession("PersonnelMaster_MainDepartment_DD_data_PM", value);
            }
        }
        public string Personnel_Charges_Unit
        {
            get
            {
                return (string)GetBaseSession("Personnel_Charges_Unit_PM");
            }
            set
            {
                SetBaseSession("Personnel_Charges_Unit_PM", value);
            }
        }
        
        #endregion


        #region "Grid"
        public static IEnumerable GetPersonnelCharges(int PersonnelID)
        {
            var Nested_Data = (List<Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["PersonnelNested_ExportData"];
            List<Return_GetRegister_NestedGrid> data = Nested_Data.FindAll(x => x.Personnel_ID == PersonnelID);
            return data;
        }

        public static GridViewSettings PersonnelCharges_GridSettings(int Personnel_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PersonnelCharges" + Personnel_ID;
            settings.SettingsDetail.MasterGridName = "PersonnelChargesListGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("Personnel_ID").Visible = false;
            settings.Columns.Add("Effective_Till").Visible = false;
            settings.Columns.Add("Charges");
            settings.Columns.Add("Unit");
            settings.Columns.Add("Remarks");
            settings.Columns.Add("Effective_Date").Caption = "Effective Date";
            return settings;
        }

        public ActionResult Frm_PersonnelMaster_Info()
        {
            Personnel_user_rights();   
            if (CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "List"))//000260 bug fixed
            {
                ViewBag.ShowHorizontalBar = 0;
                var personnelMastermaingriddata = new List<Return_GetRegister_MainGrid>();
                var PersonnelMasterItems = BASE._Personnels_Dbops.GetRegister();

                if (PersonnelMasterItems == null)
                {
                    return View(personnelMastermaingriddata);
                }
                personnelMastermaingriddata = PersonnelMasterItems.main_Register;
                PersonnelMasterGridData = personnelMastermaingriddata;
                PersonnelNested_ExportData = PersonnelMasterItems.nested_Register;
                Session["PersonnelNested_ExportData"] = PersonnelNested_ExportData;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();       
              
                return View(PersonnelMasterGridData);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Personnel_Master').hide();</script>");
            }
        }
        public ActionResult Frm_PersonnelMaster_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            Personnel_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (PersonnelMasterGridData == null || command == "REFRESH")
            {
                var PersonnelMasterItems = BASE._Personnels_Dbops.GetRegister();
                if (PersonnelMasterItems != null)
                {
                    var Mastergrid = PersonnelMasterItems.main_Register;
                    var Nestedgrid = PersonnelMasterItems.nested_Register;
                    PersonnelMasterGridData = Mastergrid;
                    PersonnelNested_ExportData = Nestedgrid;
                }
            }
            var Mastergrid_data = PersonnelMasterGridData as List<Return_GetRegister_MainGrid>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }
        public PartialViewResult Frm_PersonnelMaster_PersonnelCharges_Grid(string command, int PersonnelID)
        {
            ViewData["PersonnelID"] = PersonnelID;
            if (PersonnelNested_ExportData == null || command == "REFRESH")
            {
                var PersonnelNested_Data = BASE._Personnels_Dbops.GetRegister().nested_Register;
                PersonnelNested_ExportData = PersonnelNested_Data;
            }
            var Nested_Data = (List<Return_GetRegister_NestedGrid>)PersonnelNested_ExportData;
            List<Return_GetRegister_NestedGrid> data = Nested_Data.FindAll(x => x.Personnel_ID == PersonnelID);
            return PartialView(data);
        }
        public ActionResult PersonnelChargesCustomDataAction(int key)
        {
            var FinalPersonnel_Data = PersonnelNested_ExportData as List<Return_GetRegister_NestedGrid>;
            var it = FinalPersonnel_Data != null ? FinalPersonnel_Data.Where(f => f.ID == key).FirstOrDefault() : null;
            string PersonnelReg = "";
            if (it != null)
            {
                PersonnelReg = it.REC_ADD_ON + "![" + it.REC_ADD_BY + "!["
                        + it.REC_EDIT_ON + "![" + it.REC_EDIT_BY + "![" + it.Personnel_ID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(PersonnelReg);
        }
        public ActionResult PersonnelMasterCustomDataAction(int key)
        {
            var FinalPersonnel_Data = PersonnelMasterGridData as List<Return_GetRegister_MainGrid>;
            var it = FinalPersonnel_Data != null ? FinalPersonnel_Data.Where(f => f.REC_ID == key).FirstOrDefault() : null;
            string PersonnelReg = "";
            if (it != null)
            {
                PersonnelReg = it.REC_ADD_ON + "![" + it.REC_ADD_BY + "!["+ it.REC_EDIT_ON + "![" + it.REC_EDIT_BY + "![" + it.Name + "![" + it.Leaving_Date+"!["+it.Joining_Date;
            }
            return GridViewExtension.GetCustomDataCallbackResult(PersonnelReg);
        }
        #endregion      
        #region "NEVD" 
        public ActionResult Frm_Choose_New()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Personnel_Choose_New').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#Personnel_MasterNew').hide();</script>");
            }
            return PartialView();
        }//radio button to choose for new
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Personnel_Master_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#Personnel_MasterListPreview').hide();</script>");
            }
            return PartialView();
        }

        public ActionResult Frm_Personnel_Reopen(int personnelid, string name)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Update"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }

            if (BASE._Personnels_Dbops.Mark_Personnel_asReopen(personnelid))
            {
                return Json(new
                {
                    Message = name + " Has Been Reopened",
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

        [HttpGet]
        public ActionResult Frm_PersonnelMaster_Window(string ActionMethod = null, int ID = 0,string PostSucessFunction=null,string PopupName= "Dynamic_Content_popup", string ButtonID= "Personnel_MasterNew")
        {
            Personnel_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }

            Model_PersonnelMaster_Window model = new Model_PersonnelMaster_Window();
            model.ActionMethod = ActionMethod;
            model.Personnel_JoiningDate = DateTime.Now;//Mantis bug 0001043 resolved

            model.PostSuccessFunction = PostSucessFunction!=null? PostSucessFunction:"Personnel_MasterAjaxSuccessForm";          
            model.PopupName = PopupName != null? PopupName: "Dynamic_Content_popup";
           
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var personnel_Info = BASE._Personnels_Dbops.GetPersonnelRecord(ID);

                model.Personnel_PersonNameID = personnel_Info.AB_ID;
                model.Personnel_ContractorID = personnel_Info.Contractor_ID == null ? "" : personnel_Info.Contractor_ID.ToString();
                model.Personnel_DesignationID = personnel_Info.Designation_ID;
                model.REC_ID = personnel_Info.REC_ID;
                model.Personnel_DeptID = personnel_Info.MainDept_ID;
                model.Personnel_SubDeptID = personnel_Info.SubDept_ID;
                model.Personnel_LeavingDate = personnel_Info.Leaving_Date;
                model.Personnel_JoiningDate = personnel_Info.Joining_Date;
                model.Personnel_PFNO = personnel_Info.PF_NO;
                model.Personnel_PaymentMode = personnel_Info.Payment_Mode;
                model.Personnel_SkillTypeID = personnel_Info.Skill_Type_ID;
                model.Personnel_Type = personnel_Info.Type;
                model.Personnel_OtherDetails = personnel_Info.Other_Details == null ? "" : personnel_Info.Other_Details;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_PersonnelMaster_Window(Model_PersonnelMaster_Window model)
        {
            var actionmethod = model.ActionMethod;
            try
            {
                if (model.Personnel_SkillTypeID == null)
                {
                    return Json(new
                    {
                        Message = "Skill Type Cannot Be Empty...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    if (model.Personnel_DeptID == null) // mantis bug #1243
                    {
                        var DepartList  = (List<Return_GetStoreDept>)PersonnelMaster_MainDepartment_DD_data;
                        if (DepartList.Count != 0)
                        {
                            
                            return Json(new
                            {
                                Message = "Department value should be filled..!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (model.Personnel_JoiningDate > model.Personnel_LeavingDate)
                    {
                        return Json(new
                        {
                            Message = "Joining Date Cannot Be After Leaving Date...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //var StockPersonnel_Usage_Count = BASE._Personnels_Dbops.Get_StockPersonnel_Usage_Count(model.REC_ID);
                    //if (StockPersonnel_Usage_Count > 0)
                    //{
                    //    return Json(new
                    //    {
                    //        Message = "Person Name can be edited only if none of entry posted against item....!",
                    //        result = false
                    //    }, JsonRequestBehavior.AllowGet);//Mantis bug 0001170 fixed
                    //}
                    //code commented as Edit is allowed for some fields even if personnel is used...mantis#1441
                    var personnelcount = BASE._Personnels_Dbops.Get_Personnel_Count(model.Personnel_PersonNameID, model.Personnel_SubDeptID == null ? Convert.ToInt32(model.Personnel_DeptID) : Convert.ToInt32(model.Personnel_SubDeptID),model.Personnel_Type , model.REC_ID);//Mantis bug 0000044 fixed
                    if (personnelcount >0)
                    {
                        return Json(new
                        {
                            Message = "Two Personnels For Same Address Book Record In Same Dept and Same Personnel Type Cannot Be Created...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);//Mantis bug 0000044 fixed
                    }
                    if (model.Personnel_PFNO != null)
                    {
                        var pfcount = BASE._Personnels_Dbops.Get_PFNo_Count(model.Personnel_PFNO, model.REC_ID);
                        if (pfcount > 1)
                        {
                            return Json(new
                            {
                                Message = "PF Number Cannot Be Duplicate In Open Or Closed Personnels...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (actionmethod == "New")
                {

                    Common_Lib.RealTimeService.Param_InsertPersonnel inparam = new Common_Lib.RealTimeService.Param_InsertPersonnel();

                    inparam.AB_ID = model.Personnel_PersonNameID;
                    inparam.Type = model.Personnel_Type;
                    inparam.Skill_Type_IDs = model.Personnel_SkillTypeID;
                    inparam.Payment_Mode = model.Personnel_PaymentMode;
                    inparam.Dept_ID = model.Personnel_SubDeptID==null? Convert.ToInt32(model.Personnel_DeptID) : Convert.ToInt32(model.Personnel_SubDeptID);
                    if (inparam.Dept_ID == 0)
                    {
                        inparam.Dept_ID = null;
                    }
                    inparam.Contractor_ID = model.Personnel_ContractorID;
                    inparam.PF_No = model.Personnel_PFNO;
                    inparam.Joining_Date = model.Personnel_JoiningDate;
                    inparam.Designation_ID = model.Personnel_DesignationID;
                    inparam.Other_Details = model.Personnel_OtherDetails;

                    if (BASE._Personnels_Dbops.InsertPersonnel(inparam))
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
                if (actionmethod == "Edit")
                {
                    Common_Lib.RealTimeService.Param_UpdatePersonnel inparam = new Common_Lib.RealTimeService.Param_UpdatePersonnel();
                    inparam.AB_ID = model.Personnel_PersonNameID;
                    inparam.Type = model.Personnel_Type;
                    inparam.Skill_Type_IDs = model.Personnel_SkillTypeID;
                    inparam.Payment_Mode = model.Personnel_PaymentMode;
                    inparam.Dept_ID = model.Personnel_SubDeptID == null ? Convert.ToInt32(model.Personnel_DeptID) : Convert.ToInt32(model.Personnel_SubDeptID); 
                    inparam.Contractor_ID = model.Personnel_ContractorID;
                    inparam.PF_No = model.Personnel_PFNO;
                    inparam.Joining_Date = model.Personnel_JoiningDate;
                    inparam.Designation_ID = model.Personnel_DesignationID;
                    inparam.Other_Details = model.Personnel_OtherDetails;
                    inparam.PersonnelID = model.REC_ID;
                    if (BASE._Personnels_Dbops.UpdatePersonnel(inparam))
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
                if (actionmethod == "Delete")
                {
                    if (BASE._Personnels_Dbops.DeletePersonnel(model.REC_ID))
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
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_PersonnelMasterLeft_Window(int personnelid, string name, DateTime joiningdate)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Update"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            Model_PersonnelMasterLeft_Window model = new Model_PersonnelMasterLeft_Window();
            model.PersonnelLeft_Name = name;
            model.Personnelleft_JoiningDate = joiningdate;
            model.Personnelleft_PersonnelID = personnelid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_PersonnelMasterLeft_Window(Model_PersonnelMasterLeft_Window model)
        {
            if (model.Personnelleft_LeavingDate < model.Personnelleft_JoiningDate)
            {
                return Json(new
                {
                    Message = "Leaving Date Cannot Be Before Joining Date...!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            var Per_Usage_Period = BASE._Personnels_Dbops.Get_Personnel_Usage_Period(model.Personnelleft_PersonnelID);
            if (Per_Usage_Period.MaxUsageToDate != null && Per_Usage_Period.MinUsageFromDate != null)
            {
                if (Per_Usage_Period.MaxUsageToDate >= model.Personnelleft_LeavingDate)
                {
                    return Json(new
                    {
                        Message = "Personnel is being used till  "+Convert.ToDateTime(Per_Usage_Period.MaxUsageToDate).ToString("dd/MM/yyyy") + " Enter Leaving Date more than this...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }//Mantis bug 0001045 resolved            

            if (BASE._Personnels_Dbops.Mark_Personnel_asLeft(Convert.ToDateTime(model.Personnelleft_LeavingDate), model.Personnelleft_PersonnelID))
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

        [HttpGet]
        public ActionResult Frm_PersonnelRates_Window(int personnelid, string name, string ActionMethod = null, int ChargesID = 0, string ButtonID = "Personnel_MasterNew",string PopupName= "Dynamic_Content_popup")
        {
            Personnel_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Personnel_Master, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            
            Model_PersonnelRates_Window model = new Model_PersonnelRates_Window();
            model.ActionMethod = ActionMethod;
            model.PersonnelCharges_PersonnelName = name;
            model.PersonnelCharges_ChargeID = ChargesID;
            model.PersonnelCharges_PersonnelID = personnelid;
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var personnel_Info = BASE._Personnels_Dbops.GetPersonnelChargesRecord(ChargesID);
                model.PersonnelCharges_EffDate = personnel_Info.EffDate;
                model.PersonnelCharges_Charges = Convert.ToDouble(personnel_Info.Charges);
                model.PersonnelCharges_UnitID = personnel_Info.UnitID;
                model.PersonnelCharges_Remarks = personnel_Info.Remarks == null ? "" : personnel_Info.Remarks;              
                Personnel_Charges_Unit = personnel_Info.UnitID;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_PersonnelRates_Window(Model_PersonnelRates_Window model)
        {
            var actionmethod = model.ActionMethod;
            try
            {
                if (actionmethod == "New" || actionmethod == "Edit" || actionmethod == "Delete")
                {
                    var Nested_Data = (List<Return_GetRegister_NestedGrid>)PersonnelNested_ExportData;
                    List<Return_GetRegister_NestedGrid> data = Nested_Data.FindAll(x => x.Personnel_ID == model.PersonnelCharges_PersonnelID && x.Effective_Date == model.PersonnelCharges_EffDate); 
                    var usagecount = BASE._Personnels_Dbops.Get_PersonnelCharges_UsageCount(model.PersonnelCharges_PersonnelID, Convert.ToDateTime(model.PersonnelCharges_EffDate));
                    if (actionmethod == "New" || actionmethod == "Edit")
                    {
                        if (model.PersonnelCharges_Remarks != null)
                        {
                            model.PersonnelCharges_Remarks = model.PersonnelCharges_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                        }
                        if (data.Count > 0 && actionmethod == "New")
                        {
                            return Json(new
                            {
                                Message = "Charges Already Exist For The Effective Date...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (data.Count > 0 && actionmethod == "Edit" && data[0].ID != model.PersonnelCharges_ChargeID)//0000055 bug number fixed.
                        {
                            return Json(new
                            {
                                Message = "Charges Already Exist For The Effective Date...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (usagecount > 0)
                        {
                            return Json(new
                            {
                                Message = "Charges Cannot Be Added In Effective Period Where Job/Production Manpower Usage Has Already Been Posted...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (actionmethod == "Edit" || actionmethod == "Delete")
                    {                       
                        if (actionmethod == "Edit")
                        {
                            if (Personnel_Charges_Unit != model.PersonnelCharges_UnitID)
                            {
                                if (usagecount > 0)
                                {
                                    return Json(new
                                    {
                                        Message = "Unit Cannot Be Updated When Actual Manpower Usage Has Been Posted In Job/Production...!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (usagecount > 0)
                        {
                            return Json(new
                            {
                                Message = "Any Charges Against Which Job/Production Manpower Usage Has Been Posted, Cannot Be Deleted / Updated...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (actionmethod == "New")
                {

                    Common_Lib.RealTimeService.Param_InsertPersonnelCharges inparam = new Common_Lib.RealTimeService.Param_InsertPersonnelCharges();
                    inparam.Pers_ID = model.PersonnelCharges_PersonnelID;
                    inparam.Eff_From = Convert.ToDateTime(model.PersonnelCharges_EffDate);
                    inparam.Rate_Charges = Convert.ToDecimal(model.PersonnelCharges_Charges);
                    inparam.Rate_Unit_ID = model.PersonnelCharges_UnitID;
                    inparam.Eff_Remarks = model.PersonnelCharges_Remarks == null ? "" : model.PersonnelCharges_Remarks;
                    if (BASE._Personnels_Dbops.InsertPersonnelCharges(inparam))
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
                if (actionmethod == "Edit")
                {
                    Common_Lib.RealTimeService.Param_UpdatePersonnelCharges inparam = new Common_Lib.RealTimeService.Param_UpdatePersonnelCharges();
                    inparam.Pers_ID = model.PersonnelCharges_PersonnelID;
                    inparam.Eff_From = Convert.ToDateTime(model.PersonnelCharges_EffDate);
                    inparam.Rate_Charges = Convert.ToDecimal(model.PersonnelCharges_Charges);
                    inparam.Rate_Unit_ID = model.PersonnelCharges_UnitID;
                    inparam.Pers_ChargeID = model.PersonnelCharges_ChargeID;
                    inparam.Eff_Remarks = model.PersonnelCharges_Remarks == null ? "" : model.PersonnelCharges_Remarks;
                    if (BASE._Personnels_Dbops.updatePersonnelCharges(inparam))
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
                if (actionmethod == "Delete")
                {
                    if (BASE._Personnels_Dbops.DeletePersonnelCharges(model.PersonnelCharges_ChargeID))
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
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Get_StockPersonnel_Usage_Count(int id)
        {       
            var usagecount = BASE._Personnels_Dbops.Get_StockPersonnel_Usage_Count(id);
            if (usagecount > 0)
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
        #endregion
        #region "Misc"
        public void Personnel_user_rights()
        {
            ViewData["Personnel_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["Personnel_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Update");
            ViewData["Personnel_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "View");
            ViewData["Personnel_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Delete");
            ViewData["Personnel_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Export");
            ViewData["Personnel_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Report");
            ViewData["Personnel_ListRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "List");
            ViewData["Personnel_AddDeptRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Add");
        }

        public void Sessionclear()
        {
            Session.Remove("PersonnelNested_ExportData");
            ClearBaseSession("_PM");
        }
        public void InfoSessionclear()
        {
            ClearBaseSession("_PMInfo");
        }
        #endregion        
        #region "DropDown"
        public ActionResult LookUp_GetContractorList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var contraList = BASE._Personnels_Dbops.GetContractors();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(contraList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetDepartmentList(bool? IsVisible, DataSourceLoadOptions loadOptions, bool DD_Refresh = false)
        {
            if (PersonnelMaster_MainDepartment_DD_data == null || DD_Refresh)
            {
                var DepartList = BASE._StockDeptStores_dbops.GetMainDeptList(Common_Lib.RealTimeService.ClientScreen.Stock_Dept_Store_Master, true);
                PersonnelMaster_MainDepartment_DD_data = DepartList;
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DepartList, loadOptions)), "application/json");
            }
            else
            {
                var DepartmentList = (List<Return_GetStoreDept>)PersonnelMaster_MainDepartment_DD_data ;
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DepartmentList, loadOptions)), "application/json");
            }
        }
        public ActionResult LookUp_GetDesignationList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {

            var DesigList = BASE._Personnels_Dbops.GetDesignations();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DesigList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetPersonnelList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var PersonnelList = BASE._Personnels_Dbops.GetPersons();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PersonnelList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetSkillList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var SkillList = BASE._Personnels_Dbops.GetSkillTypes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SkillList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetSubDepartmentList(DataSourceLoadOptions loadOptions,int MainDeptID=0)
        {
            List<Return_GetStoreDept> DepartList = new List<Return_GetStoreDept>();
            if (MainDeptID==0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DepartList, loadOptions)), "application/json");
            }
            DepartList = BASE._StockDeptStores_dbops.GetAllStoreDeptList(MainDeptID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DepartList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetUnitList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var unitlist = BASE._Personnels_Dbops.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(unitlist, loadOptions)), "application/json");
        }
        #endregion
    }
}