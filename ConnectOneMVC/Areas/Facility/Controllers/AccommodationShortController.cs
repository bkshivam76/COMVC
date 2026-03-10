using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectOneMVC.Controllers;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Helper;
using DevExtreme.AspNet.Mvc;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Facility.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class AccommodationShortController : BaseController
    {
        // GET: Facility/AccommodationShort
        public ActionResult Frm_AccommodationShort_Info()
        {
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            return View();
        }
        public ActionResult getNamesByRegNos_Or_PhoneNos(string ph_reg = "REGISTRATION_NUMS", string reg_Ph_Numbers = null, string EVENTID = null, string FORMID = null)
        {
            if(FORMID == "") { FORMID = null; }
            Int64 formid = Convert.ToInt64(FORMID);

            DataTable dt = BASE._Form_dbops.get_personNamesByRegNumbers(ph_reg, reg_Ph_Numbers, EVENTID, formid, BASE._open_Cen_ID);
            string regNumbers = "";
            string chartids = "";
            string respids = "";
            string names = "";
            string genders = "";
            string names_genders_str = "";
            string arr_dates = "";
            string dep_dates = "";
            string selection = "";
            var lenth = dt.Rows.Count;

            if (lenth > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        regNumbers = dt.Rows[i]["CR_UNIQUE_REG_NO"].ToString();
                        chartids = dt.Rows[i]["CR_CHART_ID"].ToString();
                        respids = dt.Rows[i]["CR_RESPONSE_ID"].ToString();
                        names = dt.Rows[i]["C_NAME"].ToString();
                        genders = dt.Rows[i]["C_GENDER"].ToString();
                        names_genders_str = dt.Rows[i]["C_NAME"].ToString() + "(" + dt.Rows[i]["C_GENDER"].ToString().Substring(0, 1) + ")";
                        arr_dates = (Convert.ToDateTime(dt.Rows[i]["arrival_date"]).AddTicks(Convert.ToDateTime(dt.Rows[i]["arrival_time"]).TimeOfDay.Ticks)).ToString();
                        dep_dates = (Convert.ToDateTime(dt.Rows[i]["departure_date"]).AddTicks(Convert.ToDateTime(dt.Rows[i]["departure_time"]).TimeOfDay.Ticks)).ToString();
                        selection = dt.Rows[i]["selection"].ToString();
                    }
                    else
                    {
                        regNumbers = regNumbers + "|" + dt.Rows[i]["CR_UNIQUE_REG_NO"].ToString();
                        chartids = chartids + "|" + dt.Rows[i]["CR_CHART_ID"].ToString();
                        respids = respids + "|" + dt.Rows[i]["CR_RESPONSE_ID"].ToString();
                        names = names + "|" + dt.Rows[i]["C_NAME"].ToString();
                        genders = genders + "|" + dt.Rows[i]["C_GENDER"].ToString();
                        names_genders_str = names_genders_str + "|" + dt.Rows[i]["C_NAME"].ToString() + "(" + dt.Rows[i]["C_GENDER"].ToString().Substring(0, 1) + ")";
                        arr_dates = arr_dates + "|" + (Convert.ToDateTime(dt.Rows[i]["arrival_date"]).AddTicks(Convert.ToDateTime(dt.Rows[i]["arrival_time"]).TimeOfDay.Ticks)).ToString();
                        dep_dates = dep_dates + "|" + (Convert.ToDateTime(dt.Rows[i]["departure_date"]).AddTicks(Convert.ToDateTime(dt.Rows[i]["departure_time"]).TimeOfDay.Ticks)).ToString();
                        selection =selection + "|" + dt.Rows[i]["selection"].ToString();
                    }
                }
                
                DateTime min_Arr_Date = Convert.ToDateTime(null);
                DateTime max_Dep_Date = Convert.ToDateTime(null);
                //startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                if (dt.Rows.OfType<DataRow>().Where(k => k["arrival_date"] != DBNull.Value).Select(k => Convert.ToDateTime(k["arrival_date"])).Count() > 0)
                {
                    min_Arr_Date = dt.Rows.OfType<DataRow>().Where(k => k["arrival_date"] != DBNull.Value).Select(k => Convert.ToDateTime(k["arrival_date"]).AddTicks(Convert.ToDateTime(k["arrival_time"]).TimeOfDay.Ticks)).Min();
                }
                if (dt.Rows.OfType<DataRow>().Where(k => k["departure_date"] != DBNull.Value).Select(k => Convert.ToDateTime(k["departure_date"])).Count() > 0)
                {
                     max_Dep_Date = dt.Rows.OfType<DataRow>().Where(k => k["departure_date"] != DBNull.Value).Select(k => Convert.ToDateTime(k["departure_date"]).AddTicks(Convert.ToDateTime(k["departure_time"]).TimeOfDay.Ticks)).Max();
                }
                var arrivaldate_min = min_Arr_Date.ToString();
                var departuredate_max = max_Dep_Date.ToString();
                return Json(new
                {
                    Message = "success",
                    result = true,
                    regNumbers,
                    chartids,
                    respids,
                    names,
                    genders,
                    names_genders_str,
                    arrivaldate_min,
                    departuredate_max,
                    arr_dates,
                    dep_dates,
                    selection,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "nodata",
                    result = false,
                    title = "No Data Available",
                }, JsonRequestBehavior.AllowGet);
            }
        
        }

        public ActionResult updateAccomShort(string ChartResponseID, string Accommodation, string BedCount, string remarksText, string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string selectedRoomsData = null, string editedData = null, string Registrations = null, string Reg_Names = "", string performAction = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
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


            List<AccomShortSelectedRoomsData> selectedRowsData = JsonConvert.DeserializeObject<List<AccomShortSelectedRoomsData>>(selectedRoomsData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            List<AccommodationEditedData_Short> updatedData = JsonConvert.DeserializeObject<List<AccommodationEditedData_Short>>(editedData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            string[] roomids_arr = new string[selectedRowsData.Count];
            //string[] roomcapacity_arr = new string[selectedRowsData.Count];
            //string[] roomAlreadyAllotted_arr = new string[selectedRowsData.Count];
            //string[] roomAvailability_arr = new string[selectedRowsData.Count];
            string[] roomCurr_Allotment_arr = new string[selectedRowsData.Count];
            var d = 0;
            for (var i= 0; i< updatedData.Count; i++)
            {
                if(Convert.ToInt16(updatedData[i].data.CURRENT_ALLOTMENT) > 0)
                {
                    for (var j = 0; j < selectedRowsData.Count; j++)
                    {
                        if(selectedRowsData[j].REC_ID == updatedData[i].key)
                        {
                            roomCurr_Allotment_arr[d] = updatedData[i].data.CURRENT_ALLOTMENT;
                            roomids_arr[d] = updatedData[i].key;
                            d = d + 1;
                            goto nxt;
                        }
                    }
                
                }
            nxt:;
            }


            DataSet ds = BASE._Form_dbops.GetAccomodationList(BASE._open_Cen_ID, startdate, enddate, ChartResponseID.Replace('|', ','));


            DataTable BedsAvailability_tbl = ds.Tables[0]; //Building, roomid, allowed, allotted, available...
            DataTable respids_groupRegCounts_tbl = ds.Tables[2]; //responseid group registration count. respid, count_respids
            bool groupregCount = false;
            if (respids_groupRegCounts_tbl.Rows.Count == 0)
            {
                groupregCount = false;
            }
            else
            {
                groupregCount = true;
                jsonParam.message = "As of now we can not give accommodation for group registrations through this screen. That facility is not implemented yet.";
                jsonParam.title = "Information";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

            //In this loop, we are checking the availability in the selected rooms for each responseid. We are looping through all the responseids.
            string[] respids_arr = ChartResponseID.Split('|');            
            
            Int32[] CurrentAvailableBeds_arr = new Int32[selectedRowsData.Count];
            Int32[] registrations_arr = new Int32[respids_arr.Length];
            int allowed; int allotted; 
            int available;
            int requiredbedsCount = 0;
            int availableCount = 0;

            //This loop is to count the required beds
            for(var i = 0; i<respids_arr.Length; i++)
            {
                if(groupregCount == false)
                {
                    requiredbedsCount = requiredbedsCount + 1;
                    registrations_arr[i] = 1;
                }
            }

            for (int j = 0; j < selectedRowsData.Count; j++)//Here we are counting total number of beds available within the selected rooms. And also filling available beds in each room in an array.
            {
                DataRow[] roomdetail = BedsAvailability_tbl.Select("REC_ID = '" + roomids_arr[j] + "'");
                if (roomdetail != null && roomdetail.Length>0)
                {
                    allowed = Convert.ToInt32(roomdetail[0]["Allowed"]);
                    allotted = Convert.ToInt32(roomdetail[0]["AlreadyAllotted"]);
                    if(Convert.ToInt16(roomCurr_Allotment_arr[j])> Convert.ToInt16(roomdetail[0]["AVAILABLE"]))
                    {
                        jsonParam.message = "You have allotted more than the available beds in the room: " + roomdetail[0]["ROOM"] + ". Please correct and allot again.";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    CurrentAvailableBeds_arr[j] = Convert.ToInt32(roomCurr_Allotment_arr[j]); //Filling current allotted beds in an array of selected room.

                    //roomids_arr[j] = roomdetail[0]["REC_ID"].ToString();
                    available = Convert.ToInt32(roomCurr_Allotment_arr[j]); // Convert.ToInt32(roomdetail[0]["AVAILABLE"]);
                    availableCount = available + availableCount;
                }
            }

            if (requiredbedsCount > availableCount) { //Here we are checking the requirement is fulfilled or not
                jsonParam.message = "You need " + (requiredbedsCount - availableCount) + " more beds to fulfill the requirement. ";
                jsonParam.title = "Information";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            else  {//Here we are saving the accommodation
                //Delete: here before save, we need to delete the existing respid's allottment from database.
                for (int respidsIndex = 0; respidsIndex < respids_arr.Length; respidsIndex++) //This loop is loop through the selected response ids and delete the old accommodation.
                {
                    BASE._Form_dbops.DeleteAccommodationByRespID(respids_arr[respidsIndex]);
                }
                //saving accommodation
                for (int respidsIndex = 0; respidsIndex < respids_arr.Length; respidsIndex++) //This loop is loop through the selected response ids
                {
                    for (int currentalotmentsIndex = 0; currentalotmentsIndex < CurrentAvailableBeds_arr.Length; currentalotmentsIndex++) //This loop is loop through the currently allotted rooms.
                    {
                        int currentIndexNoofBeds = Convert.ToInt32(CurrentAvailableBeds_arr[currentalotmentsIndex]);
                        int currentRespIdrequirement = Convert.ToInt32(registrations_arr[respidsIndex]);
                        if (currentIndexNoofBeds == 0) { goto nextAllottment; } //This is to skip if the allottment to the room has zero beds.
                        try
                        {
                            if (currentRespIdrequirement - currentIndexNoofBeds == 0)
                            {
                                //BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], roomids_arr[currentalotmentsIndex],
                                //                                                currentIndexNoofBeds, remarksText, startdate, enddate);
                                BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], roomids_arr[currentalotmentsIndex],
                                                                                currentIndexNoofBeds, remarksText, startdate, enddate);
                                registrations_arr[respidsIndex] = 0;
                                CurrentAvailableBeds_arr[currentalotmentsIndex] = 0;
                                goto nextResp_id;
                            }
                            else if (currentRespIdrequirement - currentIndexNoofBeds > 0)
                            {
                                BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], roomids_arr[currentalotmentsIndex],
                                                                                currentIndexNoofBeds, remarksText, startdate, enddate);
                                registrations_arr[respidsIndex] = (currentRespIdrequirement - currentIndexNoofBeds);
                                CurrentAvailableBeds_arr[currentalotmentsIndex] = 0;
                                //for(int ind = 0; ind < currentAllotments_arr.Length - 1; ind++)
                                //{
                                //    currentAllotments_arr[ind] = currentAllotments_arr[ind + 1];
                                //}
                                //Array.Resize(ref currentAllotments_arr, currentAllotments_arr.Length - 1);

                                goto nextAllottment;
                            }
                            else if (currentRespIdrequirement - currentIndexNoofBeds < 0)
                            {
                                BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], roomids_arr[currentalotmentsIndex],
                                                                                currentRespIdrequirement, remarksText, startdate, enddate);
                                registrations_arr[respidsIndex] = 0;
                                CurrentAvailableBeds_arr[currentalotmentsIndex] = (currentIndexNoofBeds - currentRespIdrequirement);
                                goto nextResp_id;
                            }

                        }
                        catch (Exception e)
                        {
                            jsonParam.message = e.Message;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    nextAllottment:;
                    }
                nextResp_id:;
                }
            }
            if (performAction == "UpdateAndPrint")
            {
                if (respids_arr.Length > 1)
                {
                    return RedirectToAction("Frm_PrintSlipOptions", "AccommodationRegister", new { Chart_Inst_ID = "", Chart_Resp_ID = ChartResponseID, NamesList = Reg_Names, 
                        PrintFromscreen = "AccommodationShort" });
                }
                else
                {
                    return RedirectToAction("Frm_PrintSlip_AccomRegstr", "AccommodationRegister", new { PrintOption = "Individual", Chart_Inst_ID = "", Chart_Resp_ID = ChartResponseID,
                        PrintFromscreen = "AccommodationShort" });
                }
            }
            else
            {
                jsonParam.message = "Accomodation Has Been Allotted/Updated Successfully";
                jsonParam.title = "Success!!";
                jsonParam.result = true;                
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult LookUp_Get_EventsList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.Get_ServiceReports_And_Events();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Event(d1), loadOptions)), "application/json");
        }

        public ActionResult LookUp_Get_LiveFormsList(DataSourceLoadOptions loadOptions, string eventid = null)
        {
            DataTable dt = BASE._Form_dbops.get_LiveFormsList(BASE._open_Cen_ID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_RoomsList(DataSourceLoadOptions loadOptions)
        {
            DataTable dt = BASE._Form_dbops.GetRoomsList_AccomShort(BASE._open_Cen_ID);
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }

        public void SessionClearAccomShort()
        {
            ClearBaseSession("_AccomShort");
        }
    }
}