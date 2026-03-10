using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class RoomsVisualizationController : BaseController
    {
        // GET: Facility/RoomsVisualization
        public ActionResult Frm_RoomsOccupancyVisualization_Info()
        {
            RoomsVisualizationData roomsData = new RoomsVisualizationData();

            Int32 cenid = BASE._open_Cen_ID;
            DateTime fromDate = DateTime.Today;
            DateTime toDate = (DateTime.Today.AddDays(1)).AddDays(1).AddSeconds(-1);
            
            DataSet ds = BASE._SM_DBOps.get_roomsVisualizationData(BASE._open_Cen_ID, fromDate, toDate);
            DataTable d1 = ds.Tables[0];
            DataTable d2 = ds.Tables[1];
            //return Content(JsonConvert.SerializeObject(d1), "application/json");
            //var jsondata = JsonConvert.SerializeObject(d1, Formatting.Indented);
            
            //roomsData = JsonConvert.DeserializeObject<RoomsVisualizationData>(jsondata);
            return View("Frm_RoomsOccupancyVisualization_Info", ds);
            
        }

        public ActionResult LookUp_Get_BuildingsList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.GetServicePlacesByCenid(BASE._open_Cen_ID);            
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_ServicePlaces(d1), loadOptions)), "application/json");
        }

        
        public ActionResult RefreshData(string fromdate = null, string todate = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Int32 cenid = BASE._open_Cen_ID;
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            //DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            //DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {                
                startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));                
            }
            if (enddate != null)
            {                
                enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));                
            }
            if (enddate != null && startdate != null)
            {
                if (enddate < startdate)
                {
                    if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                    {
                        jsonParam.message = "End Date Should be greater than or equal to Start Date";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "totime_AccomShort";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "End time Should be greater than Start time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "totime_AccomShort";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            //DateTime fromDate = DateTime.Today;
            //DateTime toDate = (DateTime.Today.AddDays(1)).AddDays(1).AddSeconds(-1);

            DataSet ds = BASE._SM_DBOps.get_roomsVisualizationData(BASE._open_Cen_ID, startdate, enddate);            

            var jsondata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Content(jsondata);
            //return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult update_RoomRemarks(string room_recid, string remarks)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                BASE._SM_DBOps.UpdateRoomRemarks(room_recid, remarks);
                jsonParam.result = true;
                //jsonParam.isconfirm = true;
                jsonParam.message = "Updated Succssfully";
                jsonParam.title = "Information";
            }
            catch(Exception e)
            {
                jsonParam.result = false;
                //jsonParam.isconfirm = true;
                jsonParam.message = "Some error!";
                jsonParam.title = "Error";
            }
            
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }

    }
}