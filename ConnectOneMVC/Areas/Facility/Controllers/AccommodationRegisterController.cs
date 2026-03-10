using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Options.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;


namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class AccommodationRegisterController : BaseController
    {
        // GET: Facility/AccommoadtionRegister
        public DataTable dt_AccommodationRegisterGrid
        {
            get { return (DataTable)GetBaseSession("dt_AccommodationRegisterGrid_AccomRegstr"); }
            set { SetBaseSession("dt_AccommodationRegisterGrid_AccomRegstr", value); }
        }
        public DataTable dt_chartResponseGuestsummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponseGuestsummaryGrid_AccomRegstr"); }
            set { SetBaseSession("dt_chartResponseGuestsummaryGrid_AccomRegstr", value); }
        }
        public DataTable dt_recommendationSummaryDataGrid
        {
            get { return (DataTable)GetBaseSession("dt_recommendationSummaryDataGrid_AccomRegstr"); }
            set { SetBaseSession("dt_recommendationSummaryDataGrid_AccomRegstr", value); }
        }
        public List<AccommodationDetailedList> GetAccommodationDetailedList
        {
            get { return (List<AccommodationDetailedList>)GetBaseSession("GetAccommodationDetailedList_ChartAccommodation"); }
            set { SetBaseSession("GetAccommodationDetailedList_ChartAccommodation", value); }
        }
        public ActionResult Frm_AccommodationRegister_Info()
        {
            //Project_ID = serviceProject_ID;
            ////ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartInfo).ToString()) ? 1 : 0;

            ViewBag.AccomRegstr_ShowHorizontalBar = 0;
            //ViewBag.serviceProject_ID = serviceProject_ID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            //chartInfoGridData(Project_ID);
            dt_AccommodationRegisterGrid = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, DateTime.Today, DateTime.Today.AddDays(1), null, null, null, "Event Start And End Dates");
            //return View(dt_chartInfoGrid);
            //if(BASE._open_Cen_ID == 5329) //This number should be 7036. because this change is only for ORC case.
            //{
            //    dt_AccommodationRegisterGrid.Columns.Cast<DataColumn>().Where(col => col.ColumnName == "Accom From" || col.ColumnName == "Accom To").ToList().ForEach(col => dt_AccommodationRegisterGrid.Columns.Remove(col));
            //}
            ViewBag.cenid = BASE._open_Cen_ID; // this is for only orc to keep arrival & departure dates same with accommodation from and to dates.
            return View(dt_AccommodationRegisterGrid);
        }

        public ActionResult Frm_PrintSlipOptions(string Chart_Inst_ID = "", string Chart_Resp_ID = "", string NamesList = "", string MaleCountStr = "", string FemaleCountStr = "",
             string PrintFromscreen = "AccommodationRegister")
        {
            ViewBag.inst_ID = Chart_Inst_ID;
            ViewBag.Resp_ID = Chart_Resp_ID;
            ViewBag.NamesList = NamesList;
            ViewBag.Printfromscreen = PrintFromscreen;
            printparams model = new printparams();

            return View("Frm_PrintSlipOptions",model);
        }
        public ActionResult Frm_PrintSlip_AccomRegstr(string PrintOption = "Individual", string Chart_Inst_ID = "", string Chart_Resp_ID = "", string SelectedName = "",
            string SelectedRespID = "", string MaleCntStr = "", string FemaleCntStr = "", string PrintFromscreen = "AccommodationRegister")
        {
            printparams model = new printparams();
            //ViewBag.chartids = Chart_Inst_ID;
            //ViewBag.respids = Chart_Resp_ID;
            //ViewBag.printoption = PrintOption;
            //string[] instids = Chart_Inst_ID.Split('|');
            //string[] respids = Chart_Resp_ID.Split('|');
            //string[] malecount_arr = MaleCntStr.Split('|');
            //string[] femalecount_arr = FemaleCntStr.Split('|');
            //var chart_instanceid = Chart_Inst_ID.Replace("|", "','");
            //var chart_responseids = Chart_Resp_ID.Replace("|", "','");            
            var chart_responseids = Chart_Resp_ID.Replace("|", ",");
            //DataTable dt = dt_AccommodationRegisterGrid;
            model.printOption = PrintOption;
            //model.slipCount = instids.Length;
            model.centerName = BASE._open_Cen_Name;
            model.PersonName = SelectedName;
            model.selectedRespID = SelectedRespID;
            //model.maleCount = MaleCntStr;
            //model.femaleCount = FemaleCntStr;
            model.dt_SelectedSlipsData = BASE._Form_dbops.get_AccommodationSlipDetails(BASE._open_Cen_ID, chart_responseids);
            if(PrintFromscreen == "AccommodationShort")
            {
                model.PrintFromscreen = PrintFromscreen;
            }else if(PrintFromscreen == "AccommodationRegister") { model.PrintFromscreen = PrintFromscreen; }
            return View("Frm_PrintSlip_AccomRegstr",model);
            //if (instids.Length == 1)
            //{                
            //    DataTable AccomData= BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, null, null, null, null, null, "Event Start And End Dates",null,false, chart_responseids);
            //    //DataView dv = new DataView(dt_AccommodationRegisterGrid);
            //    //dv.RowFilter = "RESP_ID = '"+ Chart_Resp_ID +"'"; // query example = "id = 10"
            //    //model.dt_SelectedSlipsData = dv.ToTable();
            //    model.dt_SelectedSlipsData = AccomData;
            //    return View(model);
            //}
            //else
            //{
            //    DataTable AccomData = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, null, null, null, null, null, "Event Start And End Dates", null, false, chart_responseids);
            //    //DataView dv = new DataView(dt_AccommodationRegisterGrid);
            //    //dv.RowFilter = "RESP_ID in ('" + chart_responseids + "')"; // query example = "id = 10"
            //    //model.dt_SelectedSlipsData = dv.ToTable();
            //    model.dt_SelectedSlipsData = AccomData;
            //    return View(model);
            //}
        }
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_AccommodationRegister_Info_Grid(string command, 
            int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false,
            string ViewMode = "Default", string ColumnToBeHidddenIndex = "", 
            string ColumnToBeShownIndex = "", string RowKeyToFocus = "", 
            string Filter_By = "Event Start And End Dates",
            string fromdate = null, string todate = null, 
            string fromtime = null, string totime = null, string eventid = null, 
            string buildingid = null, string reg_Number = null, int? Chart_Inst_ID = null, 
            bool ConciseMode = true)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DateTime? startdate;
            DateTime? enddate;
            if (fromdate.Split('|')[0] == "null") { startdate = null; }
            else { startdate = string.IsNullOrWhiteSpace(fromdate.Split('|')[0]) ? (DateTime?)null : Convert.ToDateTime(fromdate.Split('|')[0]).Date; }
            if (todate.Split('|')[0] == "null") { enddate = null; }
            else { enddate = string.IsNullOrWhiteSpace(todate.Split('|')[0]) ? (DateTime?)null : Convert.ToDateTime(todate.Split('|')[0]).Date; }

            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            buildingid = string.IsNullOrWhiteSpace(buildingid) ? null : buildingid;
            reg_Number = string.IsNullOrWhiteSpace(reg_Number) ? null : reg_Number;

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

            if (command == "REFRESH" || dt_AccommodationRegisterGrid == null)
            {
                dt_AccommodationRegisterGrid = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, startdate, enddate, eventid, buildingid, reg_Number, Filter_By, Chart_Inst_ID, ConciseMode);
                //if (BASE._open_Cen_ID == 5329) //This number should be 7036. because this change is only for ORC case.
                //{
                //    dt_AccommodationRegisterGrid.Columns.Cast<DataColumn>().Where(col => col.ColumnName == "Accom From" || col.ColumnName == "Accom To").ToList().ForEach(col => dt_AccommodationRegisterGrid.Columns.Remove(col));
                //}
            }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            //if(ViewMode == "Default")
            //{
            //    ShowHorizontalBar = 0;
            //}
            //else { ShowHorizontalBar = 1; }
            ViewBag.AccomRegstr_ShowHorizontalBar = ShowHorizontalBar;

            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.ConciseMode = ConciseMode;
            ViewBag.cenid = BASE._open_Cen_ID; // this is for only orc to keep arrival & departure dates same with accommodation from and to dates.
            return View(dt_AccommodationRegisterGrid);
            //return View();
        }

        public ActionResult Frm_LiveFormsList_AccRegstr(string eventid = null)
        {
            //DataTable dt = BASE._Form_dbops.get_LiveFormsList(BASE._open_Cen_ID, eventid);
            ViewBag.eventid = eventid;
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            return View();
        }
        public ActionResult LookUp_Get_LiveFormsList(DataSourceLoadOptions loadOptions, string eventid = null)
        {

            DataTable dt = BASE._Form_dbops.get_LiveFormsList(BASE._open_Cen_ID);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }
        public ActionResult Frm_AccomodationSettings_Multi(string _ChartResponseID, string _ChartInstanceID, string arrivalDate, string departureDate, string Registrations, string Names,
            string bedCounts, string gridToRefresh = "AccommodationRegister_InfoGrid", string ContinueEvenDatesNotEqual = "false")
        {
            //bool a = departureDate.Split('|').GroupBy(s => s).Count() == 1;
            //string item = departureDate.FirstOrDefault().ToString();
            //StringArray.Skip(1).All(i => i.ToLower() == item.ToLower())
            //bool match = departureDate.Split('|').All(i => i.ToString() == "null");
            ResponseMiscDetails model = new ResponseMiscDetails();
            Return_Json_Param jsonParam = new Return_Json_Param();
            model.resp_ids = _ChartResponseID;//.Replace('|', ',');
            model.bedcounts = bedCounts;//.Replace('|', ',');
            model.registrations = Registrations;//.Replace('|', ',');
            model.namesOfRegisteredPersons = Names;//.Replace('|', ',');
            string[] noOfBeds = bedCounts.Split('|');
            model.BedCount = 0;
            for (int i = 0; i < noOfBeds.Length; i++)
            {
                model.BedCount = model.BedCount + Convert.ToInt32(noOfBeds[i]);
            }
            string[] noOfRegistrations = Registrations.Split('|');
            model.TotalRegistrations = 0;
            for (int i = 0; i < noOfRegistrations.Length; i++)
            {
                if (noOfRegistrations[i] == "null" || noOfRegistrations[i] == "" || noOfRegistrations[i] == " ")
                {
                    model.TotalRegistrations = model.TotalRegistrations + 0;
                }
                else
                {
                    model.TotalRegistrations = model.TotalRegistrations + Convert.ToInt32(noOfRegistrations[i]);
                }

            }
            arrivalDate = arrivalDate.Replace("GMT+0530 (India Standard Time)", "");
            departureDate = departureDate.Replace("GMT+0530 (India Standard Time)", "");
            string[] arrivaldates = arrivalDate.Split('|');
            string[] departuredates = departureDate.Split('|');


            DateTime? startdate = (DateTime?)null;
            DateTime? enddate = (DateTime?)null;
            if (ContinueEvenDatesNotEqual == "true")
            {
                for (int i = 0; i < arrivaldates.Length; i++)
                {
                    if (arrivaldates[i] == "null" || arrivaldates[i] == "")
                    {
                        List<string> arrDtsList = new List<string>(arrivaldates);
                        arrDtsList.RemoveAt(arrDtsList.IndexOf("null"));
                        arrivaldates = arrDtsList.ToArray();
                        i = i - 1;
                    }
                }
                for (int i = 0; i < departuredates.Length; i++)
                {
                    if (departuredates[i] == "null" || departuredates[i] == "")
                    {
                        List<string> depDtsList = new List<string>(departuredates);
                        depDtsList.RemoveAt(depDtsList.IndexOf("null"));
                        departuredates = depDtsList.ToArray();
                        i = i - 1;
                    }

                }

                DateTime[] arrivaldates_dt = new DateTime[arrivaldates.Length];
                DateTime[] departuredates_dt = new DateTime[departuredates.Length];
                for (int i = 0; i < arrivaldates.Length; i++)
                {
                    if (arrivaldates[i] == "null" || arrivaldates[i] == "") { arrivaldates_dt[i] = DateTime.Now.Date; } else { arrivaldates_dt[i] = Convert.ToDateTime(arrivaldates[i]); }
                    if (departuredates[i] == "null" || departuredates[i] == "") { departuredates_dt[i] = DateTime.Now.AddDays(1).Date; } else { departuredates_dt[i] = Convert.ToDateTime(departuredates[i]); }
                }
                Array.Sort(arrivaldates_dt);
                Array.Sort(departuredates_dt);
                startdate = arrivaldates_dt[0];
                enddate = departuredates_dt[departuredates.Length - 1];
            }
            else
            {
                bool AllareNullOrNot = arrivalDate.Split('|').All(i => i.ToString() == "null");
                if (AllareNullOrNot)
                {
                    startdate = DateTime.Now.Date;
                    enddate = DateTime.Today.AddDays(1);
                }
                else
                {
                    if (Array.Exists(arrivaldates, e => e == "null"))
                    {
                        jsonParam.result = false;
                        jsonParam.isconfirm = true;
                        jsonParam.message = "Arrival Dates of all the selected registrations are not same. Do you want to continue with it?";
                        jsonParam.title = "Information";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Array.Exists(departuredates, e => e == "null"))
                    {
                        jsonParam.result = false;
                        jsonParam.isconfirm = true;
                        jsonParam.message = "Departure Dates of all the selected registrations are not same. Do you want to continue with it?";
                        jsonParam.title = "Information";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    for (int i = 1; i < arrivaldates.Length; i++)
                    {
                        if (Convert.ToDateTime(arrivaldates[0].ToString()).Date != Convert.ToDateTime(arrivaldates[i].ToString()).Date)
                        {
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            jsonParam.message = "Arrival Dates of all the selected registrations are not same. Do you want to continue with it?";
                            jsonParam.title = "Information";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDateTime(departuredates[0].ToString()).Date != Convert.ToDateTime(departuredates[i].ToString()).Date)
                        {
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            jsonParam.message = "Departure Dates of all the selected registrations are not same. Do you want to continue with it?";
                            jsonParam.title = "Information";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DateTime[] arrivaldates_dt = new DateTime[arrivaldates.Length];
                    DateTime[] departuredates_dt = new DateTime[departuredates.Length];
                    for (int i = 0; i < arrivaldates.Length; i++)
                    {
                        if (arrivaldates[i] == "null" || arrivaldates[i] == "") { arrivaldates_dt[i] = DateTime.Now.Date; } else { arrivaldates_dt[i] = Convert.ToDateTime(arrivaldates[i]); }
                        if (departuredates[i] == "null" || departuredates[i] == "") { departuredates_dt[i] = DateTime.Now.AddDays(1).Date; } else { departuredates_dt[i] = Convert.ToDateTime(departuredates[i]); }
                    }
                    Array.Sort(arrivaldates_dt);
                    Array.Sort(departuredates_dt);
                    startdate = arrivaldates_dt[0];
                    enddate = departuredates_dt[departuredates.Length - 1];
                    //startdate = string.IsNullOrWhiteSpace(arrivaldates[0]) ? (DateTime?)null : Convert.ToDateTime(arrivaldates[0].ToString());
                    //enddate = string.IsNullOrWhiteSpace(departuredates[0]) ? (DateTime?)null : Convert.ToDateTime(departuredates[0].ToString());
                    if (startdate == null) { startdate = DateTime.Today; }
                    if (enddate == null) { enddate = DateTime.Today.AddDays(1); }
                }

            }


            model.fromtime = startdate; //string.IsNullOrWhiteSpace(arrivaldates[0]) ? (DateTime?)null : Convert.ToDateTime(arrivaldates[0].ToString());
            model.totime = enddate; // string.IsNullOrWhiteSpace(departuredates[0]) ? (DateTime?)null : Convert.ToDateTime(departuredates[0].ToString());
            model.fromdate = startdate;
            model.todate = enddate;

            ViewBag.ChartInstanceID = _ChartInstanceID.Replace('|', ',');
            ViewBag.gridToRefresh = gridToRefresh;
            dt_recommendationSummaryDataGrid = BASE._Form_dbops.get_recommendationSummaryForSelectedRespids(model.resp_ids.Replace("|", ","));
            model.recommendationSummaryTbl = dt_recommendationSummaryDataGrid;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateChartResponseAccomodation(string ChartResponseID, string Accommodation, string BedCount, string remarksText, string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string editedData = null, string gridToRefresh = "ChartResponsesInfoGrid", string Registrations = null, string originalData = "", string Reg_Names = "", string performAction = "")
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
                        jsonParam.focusid = "EndDate_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "End time Should be greater than Start time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "EndTime_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }


            //dynamic DynamicData = JsonConvert.DeserializeObject(editedData);
            List<AccommodationEditedData> updatedData = JsonConvert.DeserializeObject<List<AccommodationEditedData>>(editedData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            List<OldData> original_Data = JsonConvert.DeserializeObject<List<OldData>>(originalData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            int lenthOfUpdateData = updatedData.Count;
            Int32 lenthOfOriginalData = original_Data.Count;
            string[] respids_arr = ChartResponseID.Split('|');
            string[] registrations_arr = Registrations.Split('|');
            string[] RegistrationNames_arr = Reg_Names.Split('|');
            string[] roomids_arr = new string[lenthOfOriginalData];
            string[] roomidsUpdatedOrhasValue_arr = new string[lenthOfOriginalData];
            string[] currentAllotments_arr = new string[lenthOfOriginalData];
            string[,] currentAllotmentsAndIds_arr = new string[lenthOfOriginalData, 2];
            string[] roomNames_arr = new string[lenthOfOriginalData];
            string[] BldgNames_arr = new string[lenthOfOriginalData];
            string[] alreadyAllotted_arr = new string[lenthOfOriginalData];
            string[] allowed_arr = new string[lenthOfOriginalData];
            for (int i = 0; i < lenthOfOriginalData; i++) //This loop is to fill the updated data into currentAllottment array and keeping the non updated value as it is. And filling roomids array 
            {
                roomids_arr[i] = original_Data[i].REC_ID;
                currentAllotmentsAndIds_arr[i, 0] = original_Data[i].REC_ID;
                for (int updatedIndex = 0; updatedIndex < lenthOfUpdateData; updatedIndex++)
                {
                    if (updatedData[updatedIndex].key == original_Data[i].REC_ID && updatedData[updatedIndex].type == "update")
                    {
                        currentAllotments_arr[i] = updatedData[updatedIndex].data.CurrentAllottment.ToString();
                        //currentAllotmentsAndIds_arr[i, 0] = original_Data[i].REC_ID;
                        currentAllotmentsAndIds_arr[i, 1] = updatedData[updatedIndex].data.CurrentAllottment.ToString();
                        if (Convert.ToInt32(updatedData[updatedIndex].data.CurrentAllottment.ToString()) > 0)
                        { //Here we are filling roomidsUpdatedOrhasValue_arr with only greater than zero current allottment roomids. Means we are filtering out the zero current allottment from updated data.
                            roomidsUpdatedOrhasValue_arr[i] = original_Data[i].REC_ID;
                        }
                    }
                }
                //Here if the above for loop doesn't fill any value in currentAllotments_arr from updated data, then we are filling with original currentAllottment data.
                if (string.IsNullOrWhiteSpace(currentAllotments_arr[i])) 
                {
                    currentAllotments_arr[i] = original_Data[i].CurrentAllottment;
                    currentAllotmentsAndIds_arr[i, 1] = original_Data[i].CurrentAllottment;
                    //currentAllotmentsAndIds_arr[i, 0] = original_Data[i].REC_ID;
                    if (Convert.ToInt32(original_Data[i].CurrentAllottment) > 0)
                    {
                        roomidsUpdatedOrhasValue_arr[i] = original_Data[i].REC_ID;
                    }
                }
                alreadyAllotted_arr[i] = original_Data[i].Allotted.ToString();
                allowed_arr[i] = original_Data[i].Allowed.ToString(); //Capacity
                roomNames_arr[i] = original_Data[i].Room;
                BldgNames_arr[i] = original_Data[i].Building;

            }


            //Checking the false conditions first and exit from save/update process before deleting the existing data from database and saving/updating process
            DataTable d1 = BASE._Form_dbops.GetAccomodationList(BASE._open_Cen_ID, startdate, enddate, ChartResponseID.Replace('|', ',')).Tables[0];
            var CurrAllotWithoutZero_arr = currentAllotments_arr.Where(x => x != "0").ToArray(); //This line is important to filter which are greater than zero of currrent allottment column.
            var UpdatedData_roomids = roomidsUpdatedOrhasValue_arr.Where(x => x != null).ToArray();
            if (d1 != null && d1.Rows.Count > 0)
            {
                for (int currentalotmentsIndex = 0; currentalotmentsIndex < UpdatedData_roomids.Length; currentalotmentsIndex++) //This loop is loop through the currently allotted rooms.
                {
                    int currentIndexNoofBeds = Convert.ToInt32(CurrAllotWithoutZero_arr[currentalotmentsIndex]);
                    //int currentRespIdrequirement = Convert.ToInt32(registrations_arr[respidsIndex]);
                    //if (currentIndexNoofBeds == 0) { goto nextAllottment; } //This is to skip if the allottment to the room is zero beds.
                    Int32 allowed = 0;
                    Int32 allotted = 0;
                    Int32 curr_allot = 0;

                    DataRow[] roomdetail = d1.Select("REC_ID = '" + UpdatedData_roomids[currentalotmentsIndex] + "'");
                    if (roomdetail != null && d1.Rows.Count > 0)
                    {
                        allowed = Convert.ToInt32(roomdetail[0]["Allowed"]);
                        allotted = Convert.ToInt32(roomdetail[0]["AlreadyAllotted"]);
                    }
                    else
                    {
                        jsonParam.message = roomNames_arr[Array.IndexOf(roomids_arr, UpdatedData_roomids[currentalotmentsIndex])] + "- This room is deleted. Please re-allott in other available rooms";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        //jsonParam.focusid = "BedCount";
                        jsonParam.flag = "refeshDropdown";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //allowed = Convert.ToInt32(d1.AsEnumerable().Where(s => s.Field<string>("REC_ID") == roomids_arr[currentalotmentsIndex]).Select(s => s.Field<Int32>("ALLOWED")).ToArray()[0]);  //d1.Select(p => p.Allowed).Where(p => p.REC_ID == roomids_arr[currentalotmentsIndex]).ToArray();
                    //allotted = Convert.ToInt32(d1.AsEnumerable().Where(s => s.Field<string>("REC_ID") == roomids_arr[currentalotmentsIndex]).Select(s => s.Field<string>("ALREADYALLOTTED")).ToArray()[0]);  //d1.Select(p => p.Allowed).Where(p => p.REC_ID == roomids_arr[currentalotmentsIndex]).ToArray();
                    curr_allot = currentIndexNoofBeds;
                    if (allotted + curr_allot > allowed)
                    {
                        jsonParam.message = "You have allotted more than the available beds in the room Name: "
                                            + roomNames_arr[Array.IndexOf(roomids_arr, UpdatedData_roomids[currentalotmentsIndex])]
                                            + " in the Building :" + BldgNames_arr[Array.IndexOf(roomids_arr, UpdatedData_roomids[currentalotmentsIndex])];
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.flag = "refeshDropdown";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Information";
                jsonParam.result = false;
                jsonParam.flag = "refeshDropdown";
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

            //Deleting previous accommodation data of the selected Chart Response IDs
            for (int respidsIndex = 0; respidsIndex < respids_arr.Length; respidsIndex++) //This loop is loop through the selected response ids
            {
                BASE._Form_dbops.DeleteAccommodationByRespID(respids_arr[respidsIndex]);
            }

            //The Main Loop of allottment starts from here
            for (int respidsIndex = 0; respidsIndex < respids_arr.Length; respidsIndex++) //This loop is loop through the selected response ids
            {
                for (int currentalotmentsIndex = 0; currentalotmentsIndex < CurrAllotWithoutZero_arr.Length; currentalotmentsIndex++) //This loop is loop through the currently allotted rooms.
                {
                    int currentIndexNoofBeds = Convert.ToInt32(CurrAllotWithoutZero_arr[currentalotmentsIndex]);
                    int currentRespIdrequirement = Convert.ToInt32(registrations_arr[respidsIndex]);
                    if (currentIndexNoofBeds == 0) { goto nextAllottment; } //This is to skip if the allottment to the room has zero beds.

                    try
                    {
                        if (currentRespIdrequirement - currentIndexNoofBeds == 0)
                        {
                            //BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], roomids_arr[currentalotmentsIndex],
                            //                                                currentIndexNoofBeds, remarksText, startdate, enddate);
                            BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], UpdatedData_roomids[currentalotmentsIndex],
                                                                            currentIndexNoofBeds, remarksText, startdate, enddate);
                            registrations_arr[respidsIndex] = "0";
                            CurrAllotWithoutZero_arr[currentalotmentsIndex] = "0";
                            goto nextResp_id;
                        }
                        else if (currentRespIdrequirement - currentIndexNoofBeds > 0)
                        {
                            BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], UpdatedData_roomids[currentalotmentsIndex],
                                                                            currentIndexNoofBeds, remarksText, startdate, enddate);
                            registrations_arr[respidsIndex] = (currentRespIdrequirement - currentIndexNoofBeds).ToString();
                            CurrAllotWithoutZero_arr[currentalotmentsIndex] = "0";
                            //for(int ind = 0; ind < currentAllotments_arr.Length - 1; ind++)
                            //{
                            //    currentAllotments_arr[ind] = currentAllotments_arr[ind + 1];
                            //}
                            //Array.Resize(ref currentAllotments_arr, currentAllotments_arr.Length - 1);

                            goto nextAllottment;
                        }
                        else if (currentRespIdrequirement - currentIndexNoofBeds < 0)
                        {
                            BASE._Form_dbops.updateChartResponseAccomodation(respids_arr[respidsIndex], UpdatedData_roomids[currentalotmentsIndex],
                                                                            currentRespIdrequirement, remarksText, startdate, enddate);
                            registrations_arr[respidsIndex] = "0";
                            CurrAllotWithoutZero_arr[currentalotmentsIndex] = (currentIndexNoofBeds - currentRespIdrequirement).ToString();
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
            if (performAction == "UpdateAndPrint") 
            {
                if (respids_arr.Length > 1)
                {
                    return Frm_PrintSlipOptions("", ChartResponseID,Reg_Names,"","","AccommodationRegister");
                }
                else 
                {
                    return Frm_PrintSlip_AccomRegstr("Individual","", ChartResponseID,"","","","","AccommodationRegister");
                }
            }
            else
            {
                jsonParam.message = "Accomodation Has Been Allotted/Updated Successfully";
                jsonParam.title = "Success!!";
                jsonParam.result = true;
                jsonParam.flag = gridToRefresh;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LookUp_Get_AccomodationList(DataSourceLoadOptions loadOptions, string fromdate = null, string end_date = null, string fromtime = null, string end_time = null,
            string ChartResponseID = null, string RoomID = null)
        {
            DateTime? startdate = null;
            DateTime? enddate = null;
            ChartResponseID = ChartResponseID.Replace('|', ',');
            if (fromdate != null && fromdate != "")
            {
                if (fromtime != null && fromtime != "")
                {
                    startdate = (Convert.ToDateTime(fromdate).Date).AddTicks(Convert.ToDateTime(fromtime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(fromdate);
                }
            }
            if (end_date != null && end_date != "")
            {
                if (end_time != null && end_time != "")
                {
                    enddate = (Convert.ToDateTime(end_date).Date).AddTicks(Convert.ToDateTime(end_time).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(end_date);
                }
            }

            //var roomid = "";
            DataSet ds = BASE._Form_dbops.GetAccomodationList(BASE._open_Cen_ID, startdate, enddate, ChartResponseID, RoomID);
            DataTable d1 = ds.Tables[0];
            DataTable d2 = ds.Tables[1];
            if (d2 != null)
            {
                var data = DatatableToModel.DataTableTo_AccommodationDetailsList(d2);
                GetAccommodationDetailedList = data;
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_AccommodationPlaces(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_EventsList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.Get_ServiceReports_And_Events();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Event(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_BuildingsList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.GetServicePlacesByCenid(BASE._open_Cen_ID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_ServicePlaces(d1), loadOptions)), "application/json");
        }
        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }

        public ActionResult LookUp_Get_RecommendationSummaryOfRespIds(DataSourceLoadOptions loadOptions, string responseids = null)
        {
            //DataTable dt_recommendationSummaryDataGrid = BASE._Form_dbops.get_recommendationSummaryForSelectedRespids(responseids);
            return Content(JsonConvert.SerializeObject(dt_recommendationSummaryDataGrid), "application/json");
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }
        public void SessionClearAccomRegstr()
        {
            ClearBaseSession("_AccomRegstr");
        }

        #region Dev Extreme
        public ActionResult Frm_AccommodationRegister_Info_dx()
        {
            ViewBag.AccomRegstr_ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_Accommodation_Register).ToString()) ? 1 : 0;
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            dt_AccommodationRegisterGrid = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, DateTime.Today, DateTime.Today.AddDays(1), null, null, null, "Event Start And End Dates");
            return View();
        }

        //[HttpGet]
        //public ActionResult AccommodationRegister_Info_Grid_Load()
        //{
        //    dt_AccommodationRegisterGrid = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, DateTime.Today, DateTime.Today.AddDays(1), null, null, null, "Event Start And End Dates");
        //    return Content(JsonConvert.SerializeObject(dt_AccommodationRegisterGrid), "application/json");

        //}
        [HttpGet]
        public ActionResult AccommodationRegister_Info_Grid_Load( string Filter_By = "Event Start And End Dates",
           string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string eventid = null,
            string buildingid = null, string reg_Number = null, int? Chart_Inst_ID = null,
            bool ConciseMode = true, bool firtstLoad=true
            )
        {

            Return_Json_Param jsonParam = new Return_Json_Param();
            DateTime? startdate;
            DateTime? enddate;
            if (firtstLoad)
            {
                startdate = DateTime.Today;
                enddate = DateTime.Today.AddDays(1);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(fromdate)) { fromdate = ""; }
                if (fromdate.Split('|')[0] == "null") { startdate = null; }
                else { startdate = string.IsNullOrWhiteSpace(fromdate.Split('|')[0]) ? (DateTime?)null : Convert.ToDateTime(fromdate.Split('|')[0]).Date; }

                if (string.IsNullOrWhiteSpace(todate)) { todate = ""; }
                if (todate.Split('|')[0] == "null") { enddate = null; }
                else { enddate = string.IsNullOrWhiteSpace(todate.Split('|')[0]) ? (DateTime?)null : Convert.ToDateTime(todate.Split('|')[0]).Date; }

                DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
                DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
                eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
                buildingid = string.IsNullOrWhiteSpace(buildingid) ? null : buildingid;
                reg_Number = string.IsNullOrWhiteSpace(reg_Number) ? null : reg_Number;



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
            }
                dt_AccommodationRegisterGrid = BASE._Form_dbops.get_AccommodationRegister(BASE._open_Cen_ID, startdate, enddate, eventid, buildingid, reg_Number, Filter_By, Chart_Inst_ID, ConciseMode);
            return Content(JsonConvert.SerializeObject(dt_AccommodationRegisterGrid), "application/json");
        }

        public ActionResult Frm_Export_Options_dx()
        {
            return PartialView();
        }
        #endregion
    }
}