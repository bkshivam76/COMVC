using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Facility.Models;
using Common_Lib;
using ConnectOneMVC.Controllers;
using static Common_Lib.DbOperations.Center_Purpose_Info;
using Newtonsoft.Json;
using System.Globalization;


namespace ConnectOneMVC.Areas.Facility.Controllers
{
    [CheckLogin]
    public class CenterPurposeController : BaseController
    {
        public ActionResult Frm_Center_Purpose_Info()
        {
            return View("Frm_Center_Purpose_Info");
        }

        public ActionResult GetCenterPurposeDetail()
        {
            return Content(JsonConvert.SerializeObject(BASE._CenterPurposeDBOps.GetList()), "application/json");
        }

        public ActionResult Frm_Center_Purpose_Window(string ActionMethod, string ID, string REC_EDIT_ON_client)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            var model = new CenterPurpose();
            model.TempActionMethod = ActionMethod;

            try
            {
                if (ActionMethod == "New")
                {
                    return View("Frm_Center_Purpose_Window", model);
                }

                var dt = BASE._CenterPurposeDBOps.GetRecord(ID);

                if (dt == null || dt.Rows.Count == 0)
                {
                    jsonParam.result = false;
                    jsonParam.message = Common_Lib.Messages.RecordChanged("Current Purpose") + "Record Already Changed!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                model.ID = dt.Rows[0]["REC_ID"].ToString();
                model.Purpose = dt.Rows[0]["CPR_NAME"].ToString();
                model.Active = Convert.ToBoolean(dt.Rows[0]["CPR_ACTIVE"]);
                model.REC_EDIT_ON = dt.Rows[0]["REC_EDIT_ON"] as DateTime?;

                if (BASE.AllowMultiuser() && (ActionMethod == "Edit" || ActionMethod == "Delete") && model.REC_EDIT_ON.HasValue)
                {
                    if (!string.IsNullOrEmpty(REC_EDIT_ON_client))
                    {
                        
                            var clientRECEditOn = Convert.ToDateTime(REC_EDIT_ON_client);
                            var dbRECEditOn = model.REC_EDIT_ON.Value;

                            if (clientRECEditOn != dbRECEditOn)
                            {
                                jsonParam.result = false;
                                jsonParam.message = Common_Lib.Messages.RecordChanged("Current Purpose") + "Record Already Changed!!";
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                    }
                }

                return View("Frm_Center_Purpose_Window", model);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = $"An error occurred: {ex.Message}";
                jsonParam.refreshgrid = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Frm_Center_Purpose_Window(CenterPurpose model)
        {

            Return_Json_Param jsonParam = new Return_Json_Param();

            try
            {
                if (model.TempActionMethod != "Delete" && !ModelState.IsValid)
                {

                    jsonParam.result = false;
                    jsonParam.message = "Validation failed.";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                if (BASE.AllowMultiuser() && (model.TempActionMethod == "Edit" || model.TempActionMethod == "Delete"))
                {

                    var purposeRecord = BASE._CenterPurposeDBOps.GetRecord(model.ID);

                    if (purposeRecord == null || purposeRecord.Rows.Count == 0)
                    {

                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.RecordChanged("Current Purpose") + "Record Changed / Removed in Background!!";
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((model.TempActionMethod == "Edit" || model.TempActionMethod == "Delete"))
                    {
                        DateTime dbValue = Convert.ToDateTime(purposeRecord.Rows[0]["REC_EDIT_ON"]);

                        if (!model.REC_EDIT_ON.HasValue)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Purpose") + "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        DateTime clientValue = model.REC_EDIT_ON.Value;

                        if (dbValue != clientValue)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Purpose") + "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }

                switch (model.TempActionMethod)
                {
                    case "New":
                        var insertResult = BASE._CenterPurposeDBOps.Insert(model.Purpose);
                        if (insertResult)
                        {
                            jsonParam.result = true;
                            jsonParam.message = "Saved Successfully!!";
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        break;

                    case "Edit":
                        if (BASE._CenterPurposeDBOps.Update(model.Purpose, model.ID))
                        {
                            jsonParam.result = true;
                            jsonParam.message = "Updated Successfully!!";
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        break;

                    case "Delete":
                        if (BASE._CenterPurposeDBOps.Delete(model.ID))
                        {
                            jsonParam.result = true;
                            jsonParam.message = "Deleted  Successfully!!";
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        break;
                }

                jsonParam.result = false;
                jsonParam.message = "Operation failed.";
                jsonParam.refreshgrid = true;

                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = $"An error occurred: {ex.Message}";
                jsonParam.refreshgrid = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_Center_Purpose, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('ExportOptionsPopup','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeStatus(String ID, string ActionType)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            try
            {
                if (ActionType == "ACTIVATE")
                {
                    BASE._CenterPurposeDBOps.Activate(ID);

                    jsonParam.result = true;
                    jsonParam.message = "Record activated successfully.";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                if (ActionType == "DEACTIVATE")
                {
                    BASE._CenterPurposeDBOps.DeActivate(ID);

                    jsonParam.result = true;
                    jsonParam.message = "Record deactivated successfully.";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = $"An error occurred: {ex.Message} ";
                jsonParam.refreshgrid = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}