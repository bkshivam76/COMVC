using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Magazine.Models;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Magazine.Controllers
{
    [CheckLogin]
    public class MagazineDispatchTypeController : BaseController
    {
        public ActionResult Frm_Magazine_DispatchType_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Magazine_Dispatch_Type_Master, "List"))
            {
                ButtonHide();//Checking new edit rights
                var DispatchType_Data = BASE._Magazine_DBOps.GetList_Magazine_DispatchTypeList("Voucher_Entry", "Profile_Entry", "");
                if ((DispatchType_Data == null))
                {
                    return View();
                }
                else
                {
                    Session["DispatchType_ExportData"] = DispatchType_Data;
                    Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                    Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                    Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                    ViewBag.ShowHorizontalBar = 0;
                    return View(DispatchType_Data);
                }
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Magazine_Dispatch_Type_Master').hide();</script>");//Code written for User Authorization do not remove
            }
        }

        public PartialViewResult Frm_Magazine_DispatchType_Info_Grid(string command = null, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Session["DispatchType_ExportData"] == null || command == "REFRESH")
            {
                var DispatchType_Data = BASE._Magazine_DBOps.GetList_Magazine_DispatchTypeList("Voucher_Entry", "Profile_Entry", "");
                Session["DispatchType_ExportData"] = DispatchType_Data;
               var DispatchType_Data_Charges = BASE._Magazine_DBOps.GetList_DispatchTypeChargesList("Voucher_Entry", "Profile_Entry", "");
                Session["DispatchTypeCharges_ExportData"] = DispatchType_Data_Charges;
            }
            var _DispatchType_Data = Session["DispatchType_ExportData"] as List<DbOperations.Magazine.Return_MagazineDispatchType>;
            if ((_DispatchType_Data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_DispatchType_Data);
        }

        public PartialViewResult Frm_Magazine_DispatchType_DispatchCharges_Grid(string command, string DispatchType_ID)
        {
            ViewData["DispatchType_ID"] = DispatchType_ID;

            if (Session["DispatchTypeCharges_ExportData"] == null || command == "REFRESH")
            {
                var DispatchType_Data_Charges = BASE._Magazine_DBOps.GetList_DispatchTypeChargesList("Voucher_Entry", "Profile_Entry", "");
                Session["DispatchTypeCharges_ExportData"] = DispatchType_Data_Charges;
            }
            var _DispatchType_Data_Charges = Session["DispatchTypeCharges_ExportData"] as List<DbOperations.Magazine.Return_MagazineDispatchType_Charges>;
            List<DbOperations.Magazine.Return_MagazineDispatchType_Charges> dispatchcharges = _DispatchType_Data_Charges.FindAll(x => x.DispatchType_ID == DispatchType_ID);
            var ret_list = dispatchcharges;
            return PartialView(ret_list);
        }

        public ActionResult CreationDetail(string Xrow, string Check)
        {
            string Action_Status = null;
            string Add_Date = null;
            string Add_By = null;
            string Action_Date = null;
            string Action_By = null;
            string Edit_Date = null;
            string Edit_By = null;
            if (Check == "type")
            {
                if (string.IsNullOrEmpty(Xrow))
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
                else
                {
                    var tempdata = Session["DispatchType_ExportData"] as List<DbOperations.Magazine.Return_MagazineDispatchType>;
                    List<DbOperations.Magazine.Return_MagazineDispatchType> item = new List<DbOperations.Magazine.Return_MagazineDispatchType>();
                    item = (from x in tempdata where x.DispatchType_ID == Xrow select x).ToList<DbOperations.Magazine.Return_MagazineDispatchType>();
                    Action_Status = item[0].ActionStatus.ToString();
                    Add_Date = item[0].AddDate.ToString();
                    Add_By = item[0].AddBy.ToString();
                    Action_Date = item[0].ActionDate.ToString();
                    Action_By = item[0].ActionBy.ToString();
                    Edit_Date = item[0].EditDate.ToString();
                    Edit_By = item[0].EditBy.ToString();
                }
            }
            else if (Check == "charge")
            {
                if (string.IsNullOrEmpty(Xrow))
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
                else
                {
                    var tempdata = Session["DispatchTypeCharges_ExportData"] as List<DbOperations.Magazine.Return_MagazineDispatchType_Charges>; ;
                    List<DbOperations.Magazine.Return_MagazineDispatchType_Charges> item = new List<DbOperations.Magazine.Return_MagazineDispatchType_Charges>();
                    item = (from x in tempdata where x.DispatchTypeCharges_ID == Xrow select x).ToList<DbOperations.Magazine.Return_MagazineDispatchType_Charges>();
                    Action_Status = item[0].ActionStatus.ToString();
                    Add_Date = item[0].AddDate.ToString();
                    Add_By = item[0].AddBy.ToString();
                    Action_Date = item[0].ActionDate.ToString();
                    Action_By = item[0].ActionBy.ToString();
                    Edit_Date = item[0].EditDate.ToString();
                    Edit_By = item[0].EditBy.ToString();
                }
            }
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
        } // to calculate edit on add on add by etc.

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }// checking if date

        public JsonResult ButtonHide()
        {
            if (BASE._open_User_Type == "CLIENT ROLE")
            {
                var isnewright = false;
                var iseditright = false;
                foreach (DataRow cRow in BASE._Auth_Rights_Listing.Rows)
                {
                    if (cRow["Task"].ToString() == Common_Lib.RealTimeService.ClientScreen.Profile_Magazine.ToString())
                    {
                        if (cRow["Permission"].ToString().ToLower() == "add")
                        {
                            isnewright = true;
                        }
                        if (cRow["Permission"].ToString().ToLower() == "update")
                        {
                            iseditright = true;
                        }
                    }
                }
                Session["editright"] = iseditright;
                Session["newright"] = isnewright;
                return Json(new { datanew = isnewright, dataedit = iseditright }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["newright"] = true;
                Session["editright"] = true;
                return Json(new { datanew = true, dataedit = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DataNavigation(string ActionMethod, string ID, DateTime? Edit_Date, string Name = null)
        {
            string xTemp_ID = ID;
            DataTable d1 = BASE._Magazine_DBOps.GetRecord_Dispatch_Type(xTemp_ID);
            Magazine_DispatchType_Type model = new Magazine_DispatchType_Type();
            var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if ((ActionMethod == "PRINT-LIST"))
                    {
                        if ((d1 == null))
                        {
                            return Json(new { Message = "ERROR!", result = false }, JsonRequestBehavior.AllowGet);
                        }
                        if ((d1.Rows.Count == 0))
                        {
                            return Json(new { Message = Common_Lib.Messages.RecordChanged("Current DispatchType") + " " + "Record Changed / Removed in Background!!", result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (ActionMethod == "New")
                {
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "Edit" || ActionMethod == "Delete")
                {
                    if ((d1.Rows.Count == 0))
                    {
                        return Json(new { Message = Common_Lib.Messages.RecordChanged("Current DispatchType") + " " + "Record Changed / Removed in Background!!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    else if (ActionMethod == "Edit")
                    {
                        Edit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                        model.ID = ID;
                        model.EditDate = Convert.ToDateTime(Edit_Date);
                        return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "Delete")
                {
                    Edit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    model.ID = ID;
                    model.EditDate = Convert.ToDateTime(Edit_Date);
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                }
                else if (ActionMethod == "View")
                {
                    Edit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    model.ID = ID;
                    model.EditDate = Convert.ToDateTime(Edit_Date);
                    return Json(new { Message = "", result = true, data = model }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Frm_Magazine_DispatchType_Window(string ActionMethod = null, string TypeID = null)
        {
            string EditedOn = null;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Magazine_DispatchType_Type model = new Models.Magazine_DispatchType_Type();
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();

            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                DataTable _dtabledisData = BASE._Magazine_DBOps.GetRecord_Dispatch_Type(TypeID);

                if (BASE.AllowMultiuser())
                {
                    if ((_dtabledisData == null))
                    {
                    }
                    else
                    {
                        EditedOn = (_dtabledisData.Rows[0]["REC_EDIT_ON"]).ToString();
                        DateTime? EditDate = Convert.ToDateTime(EditedOn);
                        model.ID = TypeID;
                        model.EditDate = Convert.ToDateTime(EditDate);
                        model.DispatchType_Name = (_dtabledisData.Rows[0]["MDT_NAME"]).ToString();
                    }

                    string viewstr = "";
                    if ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))
                    {
                        viewstr = "view";
                    }
                    if (Convert.ToDateTime(EditedOn) != null)
                    {
                        if ((Convert.ToDateTime(EditedOn) != Convert.ToDateTime(_dtabledisData.Rows[0]["REC_EDIT_ON"])))
                        {
                            ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current Dispatch Type", viewstr));
                        }
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Magazine_DispatchType_Window(Magazine_DispatchType_Type model)
        {
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (BASE.AllowMultiuser())
                {
                    if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                                || (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)))
                    {
                        DataTable dispatch_DbOps = BASE._Magazine_DBOps.GetRecord_Dispatch_Type(model.ID);
                        if ((dispatch_DbOps == null))
                        {
                            return Json(new
                            {
                                Message = "something went wrong!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((dispatch_DbOps.Rows.Count == 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Dispatch Type") + " Record Already Changed !!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((model.EditDate != Convert.ToDateTime(dispatch_DbOps.Rows[0]["REC_EDIT_ON"])))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Dispatch Type") + " Record Already Changed !!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    // Checking Duplicate Record....

                    int checkname = Convert.ToInt32(BASE._Magazine_DBOps.GetMagazineDispatchCountByName(model.DispatchType_Name));

                    if (checkname > 0)
                    {
                        return Json(new
                        {
                            Message = "Duplicate..(" + model.DispatchType_Name + ") Same Dispatch Type Already Available...!!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        Parameter_Insert_Magazine_Dispatch_Type InParam = new Parameter_Insert_Magazine_Dispatch_Type();

                        InParam.Name = model.DispatchType_Name;
                        InParam.Status_Action = "1";

                        if (BASE._Magazine_DBOps.Insert_Magazine_Dispatch_Type(InParam))
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
                    else if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                    {
                        Parameter_Update_Magazine_Dispatch_Type UpParam = new Parameter_Update_Magazine_Dispatch_Type();
                        UpParam.Name = model.DispatchType_Name;
                        UpParam.Rec_ID = model.ID;
                        if (BASE._Magazine_DBOps.Update_Dispatch_Type(UpParam))
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
                }
                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._View))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    // DELETE
                   // Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();

                    if (BASE._Magazine_DBOps.Delete_Dispatch_Type(model.ID))
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
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Frm_Magazine_DispatchCharge_Window(string ActionMethod, string TypeID = null, string ChargeID = null)
        {
            string EditedOn = null;
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Magazine_DispatchType_Charge model = new Models.Magazine_DispatchType_Charge();
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            model.DispatchType_ID = TypeID;
            DataTable item = BASE._Magazine_DBOps.GetRecord_Dispatch_Type(TypeID);
            model.DispatchType_Name = item.Rows[0]["MDT_NAME"].ToString().ToUpper();

          
            if (((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit)
                            || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
                            || (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                DataTable _dtabledisData = BASE._Magazine_DBOps.GetRecord_Dispatch_Type_Charges(ChargeID);

                if (BASE.AllowMultiuser())
                {
                    if ((_dtabledisData == null))
                    {
                    }
                    else
                    {
                        model.DispatchCharge_ID = ChargeID;
                        EditedOn = (_dtabledisData.Rows[0]["REC_EDIT_ON"]).ToString();
                        DateTime? EditDate = Convert.ToDateTime(EditedOn);
                        model.DispatchType_ID = TypeID;
                        model.EditDate = Convert.ToDateTime(EditDate);
                        model.EffectiveDate = Convert.ToDateTime((_dtabledisData.Rows[0]["MDTC_EFF_DATE"]).ToString());
                        model.Charges = Convert.ToDouble(_dtabledisData.Rows[0]["MDTC_CHARGES"]);
                    }

                    string viewstr = "";
                    if ((Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View))
                    {
                        viewstr = "view";
                    }
                    if (Convert.ToDateTime(EditedOn) != null)
                    {
                        if ((Convert.ToDateTime(EditedOn) != Convert.ToDateTime(_dtabledisData.Rows[0]["REC_EDIT_ON"])))
                        {
                            ModelState.AddModelError("data", Common_Lib.Messages.RecordChanged("Current Dispatch Type Charges", viewstr));
                        }
                    }
                }
            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Magazine_DispatchCharge_Window(Magazine_DispatchType_Charge model)
        {
            try
            {
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (BASE.AllowMultiuser())
                {
                   
                    if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                                || (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete)))
                    {
                        DataTable dispatch_DbOps = BASE._Magazine_DBOps.GetRecord_Dispatch_Type_Charges(model.DispatchCharge_ID);
                        if ((dispatch_DbOps == null))
                        {
                            return Json(new
                            {
                                Message = "something went wrong!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((dispatch_DbOps.Rows.Count == 0))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Dispatch Type Charges") + " Record Already Changed !!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((model.EditDate != Convert.ToDateTime(dispatch_DbOps.Rows[0]["REC_EDIT_ON"])))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.RecordChanged("Current Dispatch Type Charges") + " Record Already Changed !!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    int checkcharge_effdate = 0;

                    if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
                    {
                        checkcharge_effdate = Convert.ToInt32(BASE._Magazine_DBOps.GetMagazineDispatchFeeCountByEffDate(model.EffectiveDate.ToString(), model.DispatchType_ID));
                    }
                    if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        checkcharge_effdate = Convert.ToInt32(BASE._Magazine_DBOps.GetMagazineDispatchFeeCountByEffDate(model.EffectiveDate.ToString(), model.DispatchType_ID, model.DispatchCharge_ID));
                    }

                    if (checkcharge_effdate > 0)
                    {
                        return Json(new
                        {
                            Message = "Dispatch Type Charges with Same Effective Date already created...!!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New))
                    {
                        Parameter_Insert_Magazine_Dispatch_Type_Charges InParam = new Parameter_Insert_Magazine_Dispatch_Type_Charges();
                        InParam.DT_ID = model.DispatchType_ID;
                        InParam.Eff_Date = (Convert.ToDateTime(model.EffectiveDate)).ToString(BASE._Server_Date_Format_Long);
                        InParam.Charges = Convert.ToDouble(model.Charges);
                        InParam.Status_Action = "1";
                        if (BASE._Magazine_DBOps.Insert_Magazine_Dispatch_Type_Charges(InParam))
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
                    else if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                    {
                        Parameter_Update_Magazine_Dispatch_Type_Charges UpParam = new Parameter_Update_Magazine_Dispatch_Type_Charges();
                        UpParam.Eff_Date = (Convert.ToDateTime(model.EffectiveDate)).ToString(BASE._Server_Date_Format_Long);
                        UpParam.Charges = Convert.ToDouble(model.Charges);
                        UpParam.Rec_ID = model.DispatchCharge_ID;
                        ;
                        if (BASE._Magazine_DBOps.Update_Dispatch_Type_Charges(UpParam))
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
                }
                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._View))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    // DELETE
                   // Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();

                    if (BASE._Magazine_DBOps.Delete_Dispatch_Type_Charges(model.DispatchCharge_ID))
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
                // message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                // message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ModelState.AddModelError(string.Empty, message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }// list preview or export

        public ActionResult Frm_Choose_New()
        {
            return PartialView();
        }//radio button to choose for new

        public JsonResult Magazine_DispatchType_Set_Remove_Default(string ActionMethod, string ID = null)
        {
            if (ActionMethod == "Set_Default")
            {
                if (BASE._Magazine_DBOps.Set_Default_Magazine_Dispatch(ID))
                {
                    return Json(new { Message = "Selected Dispatch set as Default...", result = true, data = "Set Dispatch as Default" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = "Error!!!", result = false, data = "Set Dispatch as Default" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (BASE._Magazine_DBOps.Remove_Default_Magazine_Dispatch())
                {
                    return Json(new { Message = "Removed Default Dispatch ...", result = true, data = "Remove Default" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = "Error!!!", result = false, data = "Remove Default" }, JsonRequestBehavior.AllowGet);
                }
            }
        } //set  or remove default

        public static GridViewSettings CreateDispatchTypeChargesSettings(string ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "DispatchCharges" + ID;
            settings.SettingsDetail.MasterGridName = "DispatchTypeInfoGrid";
            settings.CallbackRouteValues = new { Controller = "MagazineDispatchType", Action = "Frm_Magazine_DispatchType_DispatchCharges_Grid", DispatchType_ID = ID };
            settings.KeyFieldName = "DispatchTypeCharges_ID";
            settings.Columns.Add("EffectiveDate").Caption = "Effective Date";
            settings.Columns.Add("Charges");
            settings.Columns.Add("AddDate").Visible = false;
            settings.Columns.Add("AddBy").Visible = false;
            settings.Columns.Add("EditDate").Visible = false;
            settings.Columns.Add("EditBy").Visible = false;
            settings.Columns.Add("ActionBy").Visible = false;
            settings.Columns.Add("ActionDate").Visible = false;
            settings.Columns.Add("DispatchType_ID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "ondispatchchargefocusedrowchange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
           

            return settings;
        }//settings for nested grid 

        public static IEnumerable GetDispatchTypeCharges(string DispatchType_ID)
        {
            List<DbOperations.Magazine.Return_MagazineDispatchType_Charges> _DispatchType_Data_Charges = (List<DbOperations.Magazine.Return_MagazineDispatchType_Charges>)System.Web.HttpContext.Current.Session["DispatchTypeCharges_ExportData"];
            List<DbOperations.Magazine.Return_MagazineDispatchType_Charges> dispatchcharges = _DispatchType_Data_Charges.FindAll(x => x.DispatchType_ID == DispatchType_ID);
            var ret_list = dispatchcharges;
            return ret_list;
        }//binding data to nested grid
    }
}