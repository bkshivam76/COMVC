using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.Jobs;
using static Common_Lib.DbOperations.StockDeptStores;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]

    public class JobRequestRegisterController : BaseController
    {

        // GET: Stock/JobRequestRegister
        #region Global Variables
        public DateTime? JobRRFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("JobRRFromDate_JobInfo");
            }
            set
            {
                SetBaseSession("JobRRFromDate_JobInfo", value);
            }
        }
        public DateTime? JobRRToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("JobRRToDate_JobInfo");
            }
            set
            {
                SetBaseSession("JobRRToDate_JobInfo", value);
            }
        }
        public List<DbOperations.Jobs.Return_GetRegister> JobRR_Data_Glob
        {
            get
            {
                return (List<DbOperations.Jobs.Return_GetRegister>)GetBaseSession("JobRR_Data_Glob_JobInfo");
            }
            set
            {
                SetBaseSession("JobRR_Data_Glob_JobInfo", value);
            }
        }
        public List<Return_GetDocumentsGridData> Job_Documents_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("Job_Documents_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Documents_Window_Grid_Data_JobRR", value);
            }
        }
        public List<Return_GetJobItemEstimates> Job_Item_Usage_Est_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobItemEstimates>)GetBaseSession("Job_Item_Usage_Est_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Item_Usage_Est_Window_Grid_Data_JobRR", value);
            }
        }
        public List<Return_GetJobManpowerEstimates> Job_ManpowerEstimation_Grid_Data
        {
            get
            {
                return (List<Return_GetJobManpowerEstimates>)GetBaseSession("Job_ManpowerEstimation_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_ManpowerEstimation_Grid_Data_JobRR", value);
            }
        }
        public List<Return_GetJobManpowerUsage> Job_Actual_Manpower_Usage_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobManpowerUsage>)GetBaseSession("Job_Actual_Manpower_Usage_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Actual_Manpower_Usage_Window_Grid_Data_JobRR", value);
            }
        }
        public List<Return_GetJobExpensesIncurred> Job_Actual_Expenses_Grid_Data
        {
            get
            {
                return (List<Return_GetJobExpensesIncurred>)GetBaseSession("Job_Actual_Expenses_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Actual_Expenses_Grid_Data_JobRR", value);
            }
        }
        public List<Return_GetJobMachineUsage> Job_Machine_Usage_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobMachineUsage>)GetBaseSession("Job_Machine_Usage_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Machine_Usage_Window_Grid_Data_JobRR", value);
            }
        }
        public ArrayList JobEdit_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobEdit_Document_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobEdit_Document_ID_JobRR", value);
            }
        }
        public ArrayList JobDelete_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobDelete_Document_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobDelete_Document_ID_JobRR", value);
            }
        }
        public ArrayList JobUnlink_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobUnlink_Document_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobUnlink_Document_ID_JobRR", value);
            }
        }
        public ArrayList JobEdit_itemusage_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobEdit_itemusage_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobEdit_itemusage_ID_JobRR", value);
            }
        }
        public ArrayList Delete_ItemEstimation_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_ItemEstimation_ID_JobRR");
            }
            set
            {
                SetBaseSession("Delete_ItemEstimation_ID_JobRR", value);
            }
        }
        public ArrayList JobEdit_Manpowerest_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobEdit_Manpowerest_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobEdit_Manpowerest_ID_JobRR", value);
            }
        }
        public ArrayList Delete_Manpower_Estimation_Data_Session
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Manpower_Estimation_Data_Session_JobRR");
            }
            set
            {
                SetBaseSession("Delete_Manpower_Estimation_Data_Session_JobRR", value);
            }
        }
        public ArrayList JobEdit_Manpower_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobEdit_Manpower_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobEdit_Manpower_ID_JobRR", value);
            }
        }
        public ArrayList Delete_Actual_JobManpower_Usage_Data_Session
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Actual_JobManpower_Usage_Data_Session_JobRR");
            }
            set
            {
                SetBaseSession("Delete_Actual_JobManpower_Usage_Data_Session_JobRR", value);
            }
        }
        public ArrayList Delete_Expense_job
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Expense_job_JobRR");
            }
            set
            {
                SetBaseSession("Delete_Expense_job_JobRR", value);
            }
        }
        public ArrayList JobEdit_Machine_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobEdit_Machine_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobEdit_Machine_ID_JobRR", value);
            }
        }
        public ArrayList JobDelete_MachineUsage_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("JobDelete_MachineUsage_ID_JobRR");
            }
            set
            {
                SetBaseSession("JobDelete_MachineUsage_ID_JobRR", value);
            }
        }
        public ArrayList Delete_JObExisting_Remarks_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_JObExisting_Remarks_ID_JobRR");
            }
            set
            {
                SetBaseSession("Delete_JObExisting_Remarks_ID_JobRR", value);
            }
        }
        public List<Return_GetJobItemEstimates> Temp_Job_Item_Usage_Est_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobItemEstimates>)GetBaseSession("Temp_Job_Item_Usage_Est_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Job_Item_Usage_Est_Window_Grid_Data_JobRR", value);
            }
        }
        public ArrayList Temp_JobEdit_itemusage_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_JobEdit_itemusage_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_JobEdit_itemusage_ID_JobRR", value);
            }
        }
        public ArrayList Temp_Delete_ItemEstimation_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_Delete_ItemEstimation_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Delete_ItemEstimation_ID_JobRR", value);
            }
        }
        public List<Return_GetJobExpensesIncurred> Temp_Job_Actual_Expenses_Grid_Data
        {
            get
            {
                return (List<Return_GetJobExpensesIncurred>)GetBaseSession("Temp_Job_Actual_Expenses_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Job_Actual_Expenses_Grid_Data_JobRR", value);
            }
        }
        public ArrayList Temp_Delete_Expense_prod_job
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_Delete_Expense_prod_job_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Delete_Expense_prod_job_JobRR", value);
            }
        }
        public List<Return_GetJobMachineUsage> Temp_Job_Machine_Usage_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobMachineUsage>)GetBaseSession("Temp_Job_Machine_Usage_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Job_Machine_Usage_Window_Grid_Data_JobRR", value);
            }
        }
        public ArrayList Temp_JobDelete_MachineUsage_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_JobDelete_MachineUsage_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_JobDelete_MachineUsage_ID_JobRR", value);
            }
        }
        public ArrayList Temp_JobEdit_Machine_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_JobEdit_Machine_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_JobEdit_Machine_ID_JobRR", value);
            }
        }
        public List<Return_GetJobManpowerUsage> Temp_Job_Actual_Manpower_Usage_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetJobManpowerUsage>)GetBaseSession("Temp_Job_Actual_Manpower_Usage_Window_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Job_Actual_Manpower_Usage_Window_Grid_Data_JobRR", value);
            }
        }
        public ArrayList Temp_JobEdit_Manpower_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_JobEdit_Manpower_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_JobEdit_Manpower_ID_JobRR", value);
            }
        }
        public ArrayList Temp_Delete_Actual_JobManpower_Usage_Data_Session
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_Delete_Actual_JobManpower_Usage_Data_Session_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Delete_Actual_JobManpower_Usage_Data_Session_JobRR", value);
            }
        }
        public List<Return_GetJobManpowerEstimates> Temp_Job_ManpowerEstimation_Grid_Data
        {
            get
            {
                return (List<Return_GetJobManpowerEstimates>)GetBaseSession("Temp_Job_ManpowerEstimation_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Job_ManpowerEstimation_Grid_Data_JobRR", value);
            }
        }
        public ArrayList Temp_JobEdit_Manpowerest_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_JobEdit_Manpowerest_ID_JobRR");
            }
            set
            {
                SetBaseSession("Temp_JobEdit_Manpowerest_ID_JobRR", value);
            }
        }
        public ArrayList Temp_Delete_Manpower_Estimation_Data_Session
        {
            get
            {
                return (ArrayList)GetBaseSession("Temp_Delete_Manpower_Estimation_Data_Session_JobRR");
            }
            set
            {
                SetBaseSession("Temp_Delete_Manpower_Estimation_Data_Session_JobRR", value);
            }
        }
        public List<Return_GetJobRemarks> Job_Existing_Remarks_Grid_Data
        {
            get
            {
                return (List<Return_GetJobRemarks>)GetBaseSession("Job_Existing_Remarks_Grid_Data_JobRR");
            }
            set
            {
                SetBaseSession("Job_Existing_Remarks_Grid_Data_JobRR", value);
            }
        }
        public int Current_JobID
        {
            get
            {
                return (int)GetBaseSession("Current_JobID_JobRR");
            }
            set
            {
                SetBaseSession("Current_JobID_JobRR", value);
            }
        }
        public List<Return_Get_Job_Expenses_For_Mapping> JobActuallExpensions
        {
            get
            {
                return (List<Return_Get_Job_Expenses_For_Mapping>)GetBaseSession("JobActuallExpensions_JobRR");
            }
            set
            {
                SetBaseSession("JobActuallExpensions_JobRR", value);
            }
        }
        
        #endregion
        #region Grid

        public ActionResult Frm_Job_Request_Register_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_Job, "List"))
            {
                Job_NEVD_Rights();
                String PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;
                ViewData["JobRRExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["JobRRExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["JobRRExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();                

                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Job').hide();</script>");
            }
        }
        

        public ActionResult Frm_Job_Request_Register_Grid(string command, int ShowHorizontalBar = 0)
        {
            Job_NEVD_Rights();
            ViewData["JobRRExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["JobRRExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["JobRRExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (JobRR_Data_Glob == null || command == "REFRESH")
            {
                List<DbOperations.Jobs.Return_GetRegister> JobRR_Data = BASE._Jobs_Dbops.GetRegister(Convert.ToDateTime(JobRRFromDate), Convert.ToDateTime(JobRRToDate));
                JobRR_Data_Glob = JobRR_Data;
            }
            List<DbOperations.Jobs.Return_GetRegister> JobRR_Data_Final = JobRR_Data_Glob as List<DbOperations.Jobs.Return_GetRegister>;
            return PartialView(JobRR_Data_Final);
        }

        public ActionResult JobRequestRegisterRegisterCustomDataAction(int key = 0)
        {
            var Final_Data = JobRR_Data_Glob as List<DbOperations.Jobs.Return_GetRegister>;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Rec_Edit_By + "![" + it.Rec_Edit_On + "![" + it.Rec_Add_By + "![" + it.Rec_Add_On + "![" + it.Job_Status + "![" + it.CurrUserRole + "![" + it.Requestor;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        #endregion Grid
        #region Period Selection

        public string SetDate()
        {
            string xmonth;
            int year;
            if ((DateTime.Now >= new DateTime(BASE._open_Year_Sdt.Year, 4, 1)) && (DateTime.Now <= new DateTime(BASE._open_Year_Edt.Year, 3, 31)))
            {
                xmonth = (DateTime.Now.ToString("MMM")).ToUpper();
                if (xmonth == "JAN" || xmonth == "FEB" || xmonth == "MAR")
                {
                    year = BASE._open_Year_Edt.Year;
                }
                else
                {
                    year = BASE._open_Year_Sdt.Year;
                }
                return xmonth + "-" + year;
            }
            else
            {
                xmonth = "MAR";
                year = BASE._open_Year_Edt.Year;
                return xmonth + "-" + year;
            }
        }

        public ActionResult LookUp_Get_ViewType_List_JRR(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var bankdata = new List<SelectListItem>() /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/;
            string xMonth = string.Empty;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                switch (I)
                {
                    case 1:
                        xMonth = "JAN";
                        break;

                    case 2:
                        xMonth = "FEB";
                        break;

                    case 3:
                        xMonth = "MAR";
                        break;

                    case 4:
                        xMonth = "APR";
                        break;

                    case 5:
                        xMonth = "MAY";
                        break;

                    case 6:
                        xMonth = "JUN";
                        break;

                    case 7:
                        xMonth = "JUL";
                        break;

                    case 8:
                        xMonth = "AUG";
                        break;

                    case 9:
                        xMonth = "SEP";
                        break;

                    case 10:
                        xMonth = "OCT";
                        break;

                    case 11:
                        xMonth = "NOV";
                        break;

                    case 12:
                        xMonth = "DEC";
                        break;

                    default:
                        xMonth = "";
                        break;
                }
                bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Sdt.Year, Text = xMonth + "-" + BASE._open_Year_Sdt.Year });
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                switch (I)
                {
                    case 1:
                        xMonth = "JAN";
                        break;

                    case 2:
                        xMonth = "FEB";
                        break;

                    case 3:
                        xMonth = "MAR";
                        break;

                    case 4:
                        xMonth = "APR";
                        break;

                    case 5:
                        xMonth = "MAY";
                        break;

                    case 6:
                        xMonth = "JUN";
                        break;

                    case 7:
                        xMonth = "JUL";
                        break;

                    case 8:
                        xMonth = "AUG";
                        break;

                    case 9:
                        xMonth = "SEP";
                        break;

                    case 10:
                        xMonth = "OCT";
                        break;

                    case 11:
                        xMonth = "NOV";
                        break;

                    case 12:
                        xMonth = "DEC";
                        break;

                    default:
                        xMonth = "";
                        break;
                }
                bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Edt.Year, Text = xMonth + "-" + BASE._open_Year_Edt.Year });
            }

            bankdata.AddRange(new List<SelectListItem>() { new SelectListItem { Value = "1st Quarter", Text = "1st Quarter" },
                                                           new SelectListItem { Value = "2rd Quarter", Text = "2rd Quarter" },
                                                           new SelectListItem { Value = "3th Quarter", Text = "3th Quarter" },
                                                           new SelectListItem { Value = "4th Quarter", Text = "4th Quarter" } ,
                                                           new SelectListItem { Value = "1st Half Yearly", Text = "1st Half Yearly" } ,
                                                           new SelectListItem { Value = "2nd Half Yearly", Text = "2nd Half Yearly" } ,
                                                           new SelectListItem { Value = "Nine Months", Text = "Nine Months" } ,
                                                           new SelectListItem { Value = "Financial Year", Text = "Financial Year" } ,
                                                           new SelectListItem { Value = "Specific Period", Text = "Specific Period" },
          });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(bankdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_ViewType_ChangeEvent_JRR(string Chaval)
        {
            JobRequestRegister_Period model = GetPeriod(Chaval);
            JobRRFromDate = model.JobRR_Fromdate;
            JobRRToDate = model.JobRR_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JobRequestRegister_Period GetPeriod(string Chaval)
        {
            JobRequestRegister_Period model = new JobRequestRegister_Period();
            DateTime xFr_Date = DateTime.Now;
            DateTime xTo_Date = DateTime.Now;
            if (Chaval == "1st Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "2rd Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "3th Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "4th Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "1st Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(6).AddDays(-1);
            }
            else if (Chaval == "2nd Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(6).AddDays(-1);
            }
            else if (Chaval == "Nine Months")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(9).AddDays(-1);
            }
            else if (Chaval == "Financial Year")
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            else
            {
                var Sel_Month = Chaval.Substring(0, 3).ToUpper();
                var SEL_MM = 0;
                switch (Sel_Month)
                {
                    case "JAN":
                        SEL_MM = 1;
                        break;

                    case "FEB":
                        SEL_MM = 2;
                        break;

                    case "MAR":
                        SEL_MM = 3;
                        break;

                    case "APR":
                        SEL_MM = 4;
                        break;

                    case "MAY":
                        SEL_MM = 5;
                        break;

                    case "JUN":
                        SEL_MM = 6;
                        break;

                    case "JUL":
                        SEL_MM = 7;
                        break;

                    case "AUG":
                        SEL_MM = 8;
                        break;

                    case "SEP":
                        SEL_MM = 9;
                        break;

                    case "OCT":
                        SEL_MM = 10;
                        break;

                    case "NOV":
                        SEL_MM = 11;
                        break;

                    case "DEC":
                        SEL_MM = 12;
                        break;

                    default:
                        SEL_MM = 0;
                        break;
                }

                xFr_Date = new DateTime(Convert.ToInt32(Chaval.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = xFr_Date.AddMonths(1).AddDays(-1);
            }
            model.JobRR_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.JobRR_Fromdate = xFr_Date;
            model.JobRR_Todate = xTo_Date;
            return model;
        }
        [HttpGet]
        public ActionResult Frm_Change_Period_Screen_JRR()
        {
            JobRequestRegister_Period model = new JobRequestRegister_Period();
            model.JobRR_PeriodSelection = "Specific Period";
            model.JobRR_Todate = (DateTime)JobRRToDate;
            model.JobRR_Fromdate = (DateTime)JobRRFromDate;
            model.JobRR_BE_View_Period = "";
            model.JobRR_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.JobRR_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_JRR(JobRequestRegister_Period model)
        {
            if (model.JobRR_Todate < model.JobRR_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JobRRToDate = model.JobRR_Todate;
                JobRRFromDate = model.JobRR_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Period Selection
        #region NEVD

        public ActionResult DataNavigation(int jobid, string actionmethod = null)
        {
            var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
            var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
            var jobdata = BASE._Jobs_Dbops.GetRecord(jobid);
            var requesteddate = jobdata.Job_RequestDate;
            if (BASE._open_User_Type == "CLIENT ROLE")
            {
                if (AuditedPeriod != null)
                {
                    if (AuditedPeriod.Rows.Count > 0)
                    {
                        if (requesteddate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && requesteddate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Audited Period.Request Date should not be in Audited period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (SubmittedPeriod != null)
                {
                    if (SubmittedPeriod.Rows.Count > 0)
                    {
                        if (requesteddate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && requesteddate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Accounts Submitted Period.Request Date Should Not Be In Account Submission Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

            if (actionmethod == "Delete")
            {
                return RedirectToAction("getUORRCount");
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Frm_NEVD_Job_Request(string ActionMethod, int ID = 0, string PostSuccessFunction = null, string PopupID = "Dynamic_Content_popup", string CallingScreen = "", int? projectID = null)
        {
            Job_NEVD_Rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Job, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupID + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            if (CallingScreen.Length > 0)
            {
                Sessionclear();
            }            
            JobActualManpowerUsage model_ManPowUsa = new JobActualManpowerUsage();
            Model_NEVD_JobRequest model = new Model_NEVD_JobRequest();
            model.Job_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "OnJobRequestRegisterAjaxSuccessForm";
            model.Job_PopUPId = PopupID != null ? PopupID : "Dynamic_Content_popup";
            model.Job_CallingScreen = CallingScreen;
            model.ActionMethod = ActionMethod;
            model.Job_Id = ID;
            model.Job_RequestDate = DateTime.Now;
            //  model.Job_StartDate = DateTime.Now; //Mantis bug 0000623 fixed
            model.Job_CurrUser_MainDeptID = BASE._open_User_MainDeptID;
            model.Job_CurrUser_PersonnelID = BASE._open_User_PersonnelID;
            model.Job_CurrUser_SubDeptID = BASE._open_User_SubDeptID;
            if (BASE._open_PAD_No_Main == "9000")
            {
                ViewBag.SelectAssigneeMainDept = "Construction";
            }
            if (CallingScreen == "Project")
            {
                DateTime? nullDateTime = null;
                var projdata = BASE._Projects_Dbops.GetRecord(Convert.ToInt32(projectID));
                //var ProjMainAssigneeDept = projdata[0].AssigneeMainDept;//Mantis bug 0000376 fixed
                //var ProjSubAssigneeDept = projdata[0].AssigneeSubDept;//Mantis bug 0000376 fixed
                //model.Job_AssigneeMainDeptID = ProjMainAssigneeDept;
                //model.Job_AssigneeSubDeptID = ProjSubAssigneeDept;
                model.Job_ProjectId = projectID;//Mantis bug 0000466 fixed
                model.Job_ComplexId = projdata[0].Proj_Complex_Id;//Mantis bug 0000466 fixed
                if (projdata[0].Proj_Sanction_Date == nullDateTime)//Mantis bug 0000466 fixed
                {
                    model.Job_SanctionDate = DateTime.MinValue;
                }
                else
                {
                    model.Job_SanctionDate = (DateTime)projdata[0].Proj_Sanction_Date;
                }//Mantis bug 0000466 fixed            
                model.Job_SanctionNo = projdata[0].Proj_Sanction_No;//Mantis bug 0000466 fixed
            }//Mantis bug 0000388 fixed

            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var data = BASE._Jobs_Dbops.GetRecord(ID);

                if (ID != 0 && ActionMethod != "View")//Mantis bug 1309 fixed
                {

                    var Grid_Data = JobRR_Data_Glob;//Mantis bug 0000447 fixed
                    var Job_Record = Grid_Data.Where(f => f.ID == ID).FirstOrDefault();//Mantis bug 0000447 fixed
                    model.Job_CurrUserRole = Job_Record.CurrUserRole == null ? Job_Record.CurrUserRole : Job_Record.CurrUserRole.Trim();//Mantis bug 0000447 fixed//Mantis bug 0001174 fixed
                }
                model_ManPowUsa.JobStartDate = data.Job_StartDate;//Mantis bug 0000541 fixed
                model_ManPowUsa.JobRequestDate = data.Job_RequestDate;//Mantis bug 0000541 fixed
                model.Job_Name = data.Job_Name;
                model.Job_RequestDate = data.Job_RequestDate;
                model.Job_No = data.Job_No;
                model.Job_RequestorId = data.Job_RequestorId;
                model.Job_RequestorSubDeptId = data.Job_RequestorSubDeptId;
                model.Job_RequestorMainDeptId = data.Job_RequestorMainDeptId;
                model.Job_Description = data.Job_Description;
                model.Job_EndDate = data.Job_EndDate;
                model.Job_StartDate = data.Job_StartDate;
                model.Job_RequestedFinishDate = data.Job_RequestedFinishDate;
                model.Job_RequestedStartDate = data.Job_RequestedStartDate;
                model.Job_AssigneeId = data.Job_AssigneeId;
                model.Job_Assignee_Sub_Dept_ID = data.Job_AssigneeSubDeptID;//Mantis bug 0000374 fixed
                model.Job_AssigneeMainDeptID = data.Job_AssigneeMainDeptID;
                model.Job_ComplexId = data.Job_ComplexId;
                model.Job_ProjectId = data.Job_ProjectId;
                model.Job_Type = data.Job_Type;
                model.Job_Status = data.Job_Status;
                model.Job_BudgetLimit = (double?)data.Job_BudgetLimit;
                model.Job_EstRequired = data.Job_EstRequired;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_NEVD_Job_Request(Model_NEVD_JobRequest model)
        {
            try
            {
                var actionmethod = model.ActionMethod;

                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.Job_Name))
                    {
                        return Json(new
                        {
                            message = "Job Name cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Job_Name = model.Job_Name.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    if (string.IsNullOrWhiteSpace(model.Job_Description))
                    {
                        return Json(new
                        {
                            message = "Job Description cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Job_Description = model.Job_Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    if (model.Job_Remarks != null)
                    {
                        model.Job_Remarks = model.Job_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    if (model.Job_RequestDate < BASE._open_Year_Sdt || model.Job_RequestDate > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            message = "Job Request Date Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                    if (actionmethod == "New")
                    {
                        if (BASE._open_User_Type == "CLIENT ROLE")
                        {
                            if (AuditedPeriod != null)
                            {
                                if (AuditedPeriod.Rows.Count > 0)
                                {
                                    if (model.Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                    {
                                        return Json(new
                                        {
                                            message = "Request Date should not be in Audited period...!",
                                            result = false,
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            if (SubmittedPeriod != null)
                            {
                                if (SubmittedPeriod.Rows.Count > 0)
                                {
                                    if (model.Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                    {
                                        return Json(new
                                        {
                                            message = "Request Date Should Not Be In Account Submission Period...!",
                                            result = false,
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                    if (actionmethod == "Edit")
                    {
                        if (AuditedPeriod != null)
                        {
                            if (AuditedPeriod.Rows.Count > 0)//0000143 bug fixed
                            {
                                if (model.Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Request Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (SubmittedPeriod != null)
                        {
                            if (SubmittedPeriod.Rows.Count > 0)//0000143 bug fixed
                            {
                                if (model.Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Request Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (model.Job_AssigneeId != null)
                    {
                        if (model.Job_AssigneeMainDeptID != model.Job_AssigneeNameMainDeptID)
                        {
                            return Json(new
                            {
                                message = "Assignee Do Not Belong To The Assignee Main Department Selected...!!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if(model.Job_Assignee_Sub_Dept_ID != null)//Mantis bug 0000370 fixed
                        {
                            if (model.Job_Assignee_Sub_Dept_PersonnelID != null)//Mantis bug 0000370 fixed
                            {
                                //     if (!model.Job_Assignee_Sub_Dept_ID.Contains(model.Job_AssigneeNameSubDeptID.ToString()))//Mantis bug 0000370 fixed
                                if (!model.Job_Assignee_Sub_Dept_PersonnelID.Contains(model.Job_AssigneeId.ToString()))//Mantis bug 0000370 fixed
                                {
                                    return Json(new
                                    {
                                        message = "Assignee Do Not Belong To The Assignee Sub Department Selected...!!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    message = "Assignee Do Not Belong To The Assignee Sub Department Selected...!!",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (model.Job_ProjectId > 0)
                    {
                        var projdata = BASE._Projects_Dbops.GetRecord(Convert.ToInt32(model.Job_ProjectId));
                        var ProjMainAssigneeDept = (projdata[0].AssigneeMainDept).Split(',').Select(p => p.Trim()).ToList();
                        if (!(ProjMainAssigneeDept.Contains(model.Job_AssigneeMainDeptID.ToString())))
                        {
                            return Json(new
                            {
                                message = "Job Assignee Main Dept Should Not Be Outside The Main Assignee Dept Selected For Project...!!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (actionmethod == "New")
                {
                    Param_Insert_Job_Txn InParam = new Param_Insert_Job_Txn();
                    NewInsertDocuments(ref InParam, model);
                    NewInsertExpenses(ref InParam);
                    NewInsertItemEstimate(ref InParam);
                    NewInsertManpowerEstimate(ref InParam);
                    NewInsertManpowerUsage(ref InParam);
                    NewInsertMachineUsage(ref InParam);
                    NewInsertRemainingData(ref InParam, model);

                    if (BASE._Jobs_Dbops.InsertJob(InParam))
                    {
                        return Json(new
                        {
                            result = true,
                            message = Messages.SaveSuccess
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Edit")
                {
                    Param_Update_Job_Txn InParam = new Param_Update_Job_Txn();

                    EditIUDDocuments(ref InParam, model);
                    EditIUDExpenses(ref InParam);
                    EditIUDItemEstimate(ref InParam);
                    EditIUDManpowerEstimate(ref InParam);
                    EditIUDManpowerUsage(ref InParam);
                    EditIUDMachineUsage(ref InParam);
                    EditIUDRemainingData(ref InParam, model);

                    if (BASE._Jobs_Dbops.UpdateJob(InParam))
                    {
                        if (!string.IsNullOrEmpty(model.Job_SatusButtonClick))
                        {
                            string msg = "";
                            Param_Update_Job_Status param = new Param_Update_Job_Status();
                            param.JobID = model.Job_Id;
                            switch (model.Job_SatusButtonClick)
                            {
                                case "Estimation_Submission":
                                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Assigned_for_Estimation_Creation", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To 'Assigned For Estimation Creation'..!!";
                                    break;

                                case "Estimate_Approval":
                                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Submitted_for_Estimate_Approval", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To 'Submitted for Estimate Approval'..!!";
                                    break;

                                case "Reject":
                                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Rejected", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Rejected..!!";
                                    break;

                                case "Changes Recommended":
                                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Changes_Recommended", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Changes Recommended...!!";
                                    break;

                                case "Cancel":
                                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Cancelled", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Cancelled..!!";
                                    break;

                                case "Approve":
                                    if (model.Job_AssigneeMainDeptInchargeID == BASE._open_User_PersonnelID)
                                    {
                                        param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Approved", true);
                                        msg = "Status Changed To 'Approved'..!!";
                                    }
                                    else if (model.Job_RequestorMainDeptInchargeID == BASE._open_User_PersonnelID)
                                    {
                                        param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Requested", true);
                                        msg = "Status Changed To 'Requested'..!!";
                                    }
                                    break;
                            }
                            if (BASE._Jobs_Dbops.UpdateJobStatus(param))
                            {
                                return Json(new
                                {
                                    result = true,
                                    message = msg
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = Messages.SomeError
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        return Json(new
                        {
                            result = true,
                            message = Messages.UpdateSuccess,
                            isRequestor = model.Job_RequestorId == model.Job_CurrUser_PersonnelID ? true : false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Delete")
                {
                    if (BASE._Jobs_Dbops.DeleteJob(model.Job_Id))
                    {
                        return Json(new { result = true, message = Messages.DeleteSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #region New InParam Functions
        public void NewInsertDocuments(ref Param_Insert_Job_Txn InParam, Model_NEVD_JobRequest model)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Job_Documents_Window_Grid_Data;
            if (DocumentsData != null)
            {
                var insertAttachments = new Parameter_Insert_Attachment[DocumentsData.Count()];
                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "Jobs";
                        InEInfo.Ref_Rec_ID = model.Job_Id.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments[i] = InEInfo;
                    }
                }
                InParam.InAttachment = insertAttachments;
            }
        }
        public void NewInsertItemEstimate(ref Param_Insert_Job_Txn InParam)
        {
            var all_data_Of_Est_Grid = (List<Return_GetJobItemEstimates>)Job_Item_Usage_Est_Window_Grid_Data;
            if (all_data_Of_Est_Grid != null)
            {
                var insertItemEstimations = new Param_Insert_Job_ItemEstimation[all_data_Of_Est_Grid.Count()];
                for (int I = 0; I <= all_data_Of_Est_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Est_Grid[I].REC_ID == 0)
                    {
                        var InItemEstimaInfo = new Param_Insert_Job_ItemEstimation();
                        InItemEstimaInfo.sub_Item_ID = all_data_Of_Est_Grid[I].Item_ID;
                        InItemEstimaInfo.Est_Qty = all_data_Of_Est_Grid[I].Quantity;
                        InItemEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Rate);
                        InItemEstimaInfo.Est_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Amount);
                        InItemEstimaInfo.Est_Tolerance = Convert.ToDecimal(all_data_Of_Est_Grid[I].Tolerance);
                        InItemEstimaInfo.Remarks = all_data_Of_Est_Grid[I].Remarks;
                        insertItemEstimations[I] = InItemEstimaInfo;
                    }
                }
                InParam.InJobItemEstimation = insertItemEstimations;
            }
        }
        public void NewInsertManpowerEstimate(ref Param_Insert_Job_Txn InParam)
        {
            var all_data_Of_Manpower_Est_Grid = (List<Return_GetJobManpowerEstimates>)Job_ManpowerEstimation_Grid_Data;
            if (all_data_Of_Manpower_Est_Grid != null)
            {
                var insertManPowerEstimations = new Param_Insert_Job_ManpowerEstimation[all_data_Of_Manpower_Est_Grid.Count()];
                for (int I = 0; I <= all_data_Of_Manpower_Est_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Manpower_Est_Grid[I].REC_ID == 0)
                    {
                        var InManpowerEstimaInfo = new Param_Insert_Job_ManpowerEstimation();
                        InManpowerEstimaInfo.Manpower_Skill_ID = all_data_Of_Manpower_Est_Grid[I].SkillID.ToString();
                        InManpowerEstimaInfo.Unit_ID = all_data_Of_Manpower_Est_Grid[I].UnitID;
                        InManpowerEstimaInfo.Estimated_Qty = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Consumption);
                        InManpowerEstimaInfo.Est_Amount = all_data_Of_Manpower_Est_Grid[I].Est_Cost;
                        InManpowerEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Rate_per_Unit);
                        InManpowerEstimaInfo.Remarks = all_data_Of_Manpower_Est_Grid[I].Remarks;
                        insertManPowerEstimations[I] = InManpowerEstimaInfo;
                    }
                }
                InParam.InJobManpowerEstimation = insertManPowerEstimations;
            }
        }
        public void NewInsertManpowerUsage(ref Param_Insert_Job_Txn InParam)
        {
            var all_data_Of_ManpowerUsage = (List<Return_GetJobManpowerUsage>)Job_Actual_Manpower_Usage_Window_Grid_Data;
            if (all_data_Of_ManpowerUsage != null)
            {
                var insertManpowerUsageUsage = new Param_Insert_Job_ManpowerUsage[all_data_Of_ManpowerUsage.Count()];
                for (int I = 0; I <= all_data_Of_ManpowerUsage.Count() - 1; I++)
                {
                    if (all_data_Of_ManpowerUsage[I].REC_ID == 0)
                    {
                        var InManpowerUsageInfo = new Param_Insert_Job_ManpowerUsage();
                        InManpowerUsageInfo.Person_ID = Convert.ToInt32(all_data_Of_ManpowerUsage[I].Manpower_ID);
                        InManpowerUsageInfo.Period_From = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_From);
                        InManpowerUsageInfo.Period_To = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_To);
                        InManpowerUsageInfo.Charge_ID = (Int32)all_data_Of_ManpowerUsage[I].Job_Manpower_ChargeID;//Mantis bug 0000362 fixed
                        InManpowerUsageInfo.Units_Worked = all_data_Of_ManpowerUsage[I].Units_Worked;
                        InManpowerUsageInfo.Total_Amount = all_data_Of_ManpowerUsage[I].Total_Cost;
                        InManpowerUsageInfo.Remarks = all_data_Of_ManpowerUsage[I].Remarks;
                        InManpowerUsageInfo.Job_ID = 0;
                        insertManpowerUsageUsage[I] = InManpowerUsageInfo;
                    }
                }
                InParam.InJobManpowerUsage = insertManpowerUsageUsage;
            }
        }
        public void NewInsertExpenses(ref Param_Insert_Job_Txn InParam)
        {
            var all_data_Of_Expensesincurred_Grid = (List<Return_GetJobExpensesIncurred>)Job_Actual_Expenses_Grid_Data;
            if (all_data_Of_Expensesincurred_Grid != null)
            {
                var insertExpensesincurred = new Param_Insert_Job_ExpensesIncurred[all_data_Of_Expensesincurred_Grid.Count()];
                for (int I = 0; I <= all_data_Of_Expensesincurred_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Expensesincurred_Grid[I].REC_ID == 0)
                    {
                        var InExpensesincurred = new Param_Insert_Job_ExpensesIncurred();
                        InExpensesincurred.Job_ID = 0;
                        InExpensesincurred.Exp_Tr_ID = all_data_Of_Expensesincurred_Grid[I].Txn_ID;//Mantis bug 0000393 fixed
                        InExpensesincurred.Exp_Tr_Sr_No = Convert.ToInt32(all_data_Of_Expensesincurred_Grid[I].Txn_Sr_No);//Mantis bug 0000393 fixed
                        insertExpensesincurred[I] = InExpensesincurred;
                    }
                }
                InParam.InJobExpensesIncurred = insertExpensesincurred;
            }
        }
        public void NewInsertMachineUsage(ref Param_Insert_Job_Txn InParam)
        {
            var all_data_Of_MachineUsage_Grid = (List<Return_GetJobMachineUsage>)Job_Machine_Usage_Window_Grid_Data;
            if (all_data_Of_MachineUsage_Grid != null)
            {
                var insertMachineUsage = new Param_Insert_Job_MachineUsage[all_data_Of_MachineUsage_Grid.Count()];
                for (int I = 0; I <= all_data_Of_MachineUsage_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_MachineUsage_Grid[I].REC_ID == 0)
                    {
                        var InMachineUsageInfo = new Param_Insert_Job_MachineUsage();
                        InMachineUsageInfo.Machine_ID = all_data_Of_MachineUsage_Grid[I].Machine_ID;
                        InMachineUsageInfo.Machine_Count = all_data_Of_MachineUsage_Grid[I].Mch_Count;
                        InMachineUsageInfo.Machine_Usage = Convert.ToDecimal(all_data_Of_MachineUsage_Grid[I].Usage_in_Hrs);
                        InMachineUsageInfo.Machine_Remarks = all_data_Of_MachineUsage_Grid[I].Remarks;
                        insertMachineUsage[I] = InMachineUsageInfo;
                    }
                }
                InParam.InJobMachineUsage = insertMachineUsage;
            }
        }
        public void NewInsertRemainingData(ref Param_Insert_Job_Txn InParam, Model_NEVD_JobRequest model)
        {
            InParam.Job_Name = model.Job_Name == null ? "" : model.Job_Name;
            InParam.Job_Request_Date = model.Job_RequestDate;
            InParam.Job_Type = model.Job_Type == null ? "" : model.Job_Type;
            InParam.Job_Project_Id = model.Job_ProjectId;
            InParam.Job_Complex_Id = model.Job_ComplexId;
            InParam.Job_Requestor_Id = Convert.ToInt32(model.Job_RequestorId);
            InParam.Job_Assignee_Main_Dept_ID = Convert.ToInt32(model.Job_AssigneeMainDeptID);

            if (model.Job_Assignee_Sub_Dept_ID != null)//Mantis bug 0000374 fixed
            {
                var job_AssigneeSubDeptID = model.Job_Assignee_Sub_Dept_ID.Split(',');
                List<int?> AssignSubDeptID = new List<int?>();
                for (int i = 0; i < job_AssigneeSubDeptID.Length; i++)
                {
                    AssignSubDeptID.Add(Convert.ToInt32(job_AssigneeSubDeptID[i]));
                }
                InParam.Job_Assignee_Sub_Dept_ID = AssignSubDeptID.ToArray();
            }//Mantis bug 0000374 fixed
            else
            {
                InParam.Job_Assignee_Sub_Dept_ID = null;
            }//Mantis bug 0000374 fixed

            InParam.Job_Assignee_Id = model.Job_AssigneeId;
            InParam.Job_Requested_Start_Date = model.Job_RequestedStartDate;
            InParam.Job_Requested_finish_Date = model.Job_RequestedFinishDate;
            InParam.Job_Start_Date = model.Job_StartDate;
            InParam.Job_Completion_Date = model.Job_EndDate;
            InParam.Job_Description = model.Job_Description;
            InParam.Job_Estimate_Required = model.Job_EstRequired;
            InParam.Job_Budget_Limit = Convert.ToDecimal(model.Job_BudgetLimit);
            InParam.Job_Requestor_Main_Dept_Id = Convert.ToInt32(model.Job_RequestorMainDeptId);
            InParam.Job_Requestor_Sub_Dept_Id = model.Job_RequestorSubDeptId;//Mantis bug 0000426 fixed
            InParam.Remarks = model.Job_Remarks;
        }

        #endregion New InParam Functions
        #region Edit InParam Functions
        public void EditIUDDocuments(ref Param_Update_Job_Txn InParam, Model_NEVD_JobRequest model)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Job_Documents_Window_Grid_Data;
            if (DocumentsData != null)
            {
                var insertAttachments = new List<Parameter_Insert_Attachment>();//Mantis bug 0000404 fixed
                var updateattachment = new List<Parameter_Update_Attachment>();//Mantis bug 0000404 fixed
                string[] doceditid = JobEdit_Document_ID != null ? (string[])(JobEdit_Document_ID as ArrayList).ToArray(typeof(string)) : null;
                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "Jobs";
                        InEInfo.Ref_Rec_ID = model.Job_Id.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments.Add(InEInfo);//Mantis bug 0000404 fixed
                    }
                    else if (doceditid != null)
                    {
                        if (doceditid.Contains(DocumentsData[i].ID))
                        {
                            var InEInfo = new Parameter_Update_Attachment();
                            InEInfo.FileName = DocumentsData[i].File_Name;
                            InEInfo.Description = DocumentsData[i].Remarks;
                            InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                            InEInfo.Ref_Screen = "Jobs";
                            InEInfo.Ref_Rec_ID = model.Job_Id.ToString();
                            InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                            InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                            InEInfo.File = DocumentsData[i].File_Array;
                            InEInfo.RecID = DocumentsData[i].ID;
                            updateattachment.Add(InEInfo);//Mantis bug 0000404 fixed
                        }
                    }
                }
                if (insertAttachments.Count > 0) { InParam.Added_Attachments = insertAttachments[0] == null ? null : insertAttachments.ToArray(); }
                else { InParam.Added_Attachments = insertAttachments.ToArray(); }//Mantis bug 0000404 fixed
                if (updateattachment.Count > 0) { InParam.Updated_Attachments = updateattachment[0] == null ? null : updateattachment.ToArray(); }
                else { InParam.Updated_Attachments = updateattachment.ToArray(); }//Mantis bug 0000404 fixed
            }//Mantis bug 0000404 fixed
            var DocAllDeleteIds = JobDelete_Document_ID as ArrayList;
            if (DocAllDeleteIds != null)
            {
                var deleteDocID = new Param_Deleted_Attachments[DocAllDeleteIds.Count];
                for (int i = 0; i <= DocAllDeleteIds.Count - 1; i++)
                {
                    var deleteDocIDInfo = new Param_Deleted_Attachments();
                    deleteDocIDInfo.Rec_ID = DocAllDeleteIds[i].ToString();
                    deleteDocID[i] = deleteDocIDInfo;
                }
                InParam.Deleted_Attachments = deleteDocID;
            }
            var DocAllunlinkIds = JobUnlink_Document_ID as ArrayList;
            if (DocAllunlinkIds != null)
            {
                var unlinkDocID = new Param_Unlinked_Attachments[DocAllunlinkIds.Count];
                for (int i = 0; i <= DocAllunlinkIds.Count - 1; i++)
                {
                    var unlinkDocIDInfo = new Param_Unlinked_Attachments();
                    unlinkDocIDInfo.Rec_ID = DocAllunlinkIds[i].ToString();
                    unlinkDocID[i] = unlinkDocIDInfo;
                }
                InParam.Unlinked_Attachments = unlinkDocID;
            }
        }
        public void EditIUDItemEstimate(ref Param_Update_Job_Txn InParam)
        {            
            var all_data_Of_Est_Grid = (List<Return_GetJobItemEstimates>)Job_Item_Usage_Est_Window_Grid_Data;
            if (all_data_Of_Est_Grid != null)
            {
                var insertItemEstimations = new List<Param_Insert_Job_ItemEstimation>();//Mantis bug 0000576 fixed
                var UpdateItemEstimations = new List<Param_Update_Job_ItemEstimation>();//Mantis bug 0000576 fixed
                int[] itemeditid = JobEdit_itemusage_ID != null ? (int[])(JobEdit_itemusage_ID as ArrayList).ToArray(typeof(int)) : null;
                for (int I = 0; I <= all_data_Of_Est_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Est_Grid[I].REC_ID == 0)
                    {
                        var InItemEstimaInfo = new Param_Insert_Job_ItemEstimation();
                        InItemEstimaInfo.sub_Item_ID = all_data_Of_Est_Grid[I].Item_ID;
                        InItemEstimaInfo.Est_Qty = all_data_Of_Est_Grid[I].Quantity;
                        InItemEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Rate);
                        InItemEstimaInfo.Est_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Amount);
                        InItemEstimaInfo.Est_Tolerance = Convert.ToDecimal(all_data_Of_Est_Grid[I].Tolerance);
                        InItemEstimaInfo.Remarks = all_data_Of_Est_Grid[I].Remarks;
                        insertItemEstimations.Add(InItemEstimaInfo);//Mantis bug 0000576 fixed                        
                    }
                    else if (itemeditid != null)
                    {
                        if (itemeditid.Contains(all_data_Of_Est_Grid[I].REC_ID))
                        {
                            var InItemEstimaInfo = new Param_Update_Job_ItemEstimation();
                            InItemEstimaInfo.sub_Item_ID = all_data_Of_Est_Grid[I].Item_ID;
                            InItemEstimaInfo.Est_Qty = all_data_Of_Est_Grid[I].Quantity;
                            InItemEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Rate);
                            InItemEstimaInfo.Est_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[I].Est_Amount);
                            InItemEstimaInfo.Est_Tolerance = Convert.ToDecimal(all_data_Of_Est_Grid[I].Tolerance);
                            InItemEstimaInfo.Remarks = all_data_Of_Est_Grid[I].Remarks;
                            InItemEstimaInfo.ID = all_data_Of_Est_Grid[I].REC_ID;
                            UpdateItemEstimations.Add(InItemEstimaInfo);//Mantis bug 0000576 fixed                            
                        }
                    }
                }
                if (insertItemEstimations.Count > 0) { InParam.Added_JobItemEstimations = insertItemEstimations == null ? null : insertItemEstimations.ToArray(); }
                else { InParam.Added_JobItemEstimations = insertItemEstimations.ToArray(); }//Mantis bug 0000576 fixed
                if (UpdateItemEstimations.Count > 0) { InParam.Updated_JobItemEstimations = UpdateItemEstimations == null ? null : UpdateItemEstimations.ToArray(); }
                else { InParam.Updated_JobItemEstimations = UpdateItemEstimations.ToArray(); }//Mantis bug 0000576 fixed
            }
            var ItemEstAllDeleteIds = Delete_ItemEstimation_ID as ArrayList;
            if (ItemEstAllDeleteIds != null)
            {
                var deleteItemEstID = new Param_Deleted_Job_ItemEstimation[ItemEstAllDeleteIds.Count];
                for (int i = 0; i <= ItemEstAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemEstIDInfo = new Param_Deleted_Job_ItemEstimation();
                    deleteItemEstIDInfo.Rec_ID = Convert.ToInt32(ItemEstAllDeleteIds[i]);
                    deleteItemEstID[i] = deleteItemEstIDInfo;
                }
                InParam.Deleted_JobItemEstimations = deleteItemEstID;
            }
        }
        public void EditIUDManpowerEstimate(ref Param_Update_Job_Txn InParam)
        {
            var all_data_Of_Manpower_Est_Grid = (List<Return_GetJobManpowerEstimates>)Job_ManpowerEstimation_Grid_Data;
            if (all_data_Of_Manpower_Est_Grid != null)
            {
                var insertManPowerEstimations = new List<Param_Insert_Job_ManpowerEstimation>();//Mantis bug 0000360 fixed
                var UpdateManpowerEstimations = new List<Param_Update_Job_ManpowerEstimation>();//Mantis bug 0000360 fixed
                int[] manpowereditid = JobEdit_Manpowerest_ID != null ? (int[])(JobEdit_Manpowerest_ID as ArrayList).ToArray(typeof(int)) : null;
                for (int I = 0; I <= all_data_Of_Manpower_Est_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Manpower_Est_Grid[I].REC_ID == 0)
                    {
                        var InManpowerEstimaInfo = new Param_Insert_Job_ManpowerEstimation();
                        InManpowerEstimaInfo.Manpower_Skill_ID = all_data_Of_Manpower_Est_Grid[I].SkillID.ToString();
                        InManpowerEstimaInfo.Unit_ID = all_data_Of_Manpower_Est_Grid[I].UnitID;
                        InManpowerEstimaInfo.Estimated_Qty = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Consumption);
                        InManpowerEstimaInfo.Est_Amount = all_data_Of_Manpower_Est_Grid[I].Est_Cost;
                        InManpowerEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Rate_per_Unit);
                        InManpowerEstimaInfo.Remarks = all_data_Of_Manpower_Est_Grid[I].Remarks;
                        insertManPowerEstimations.Add(InManpowerEstimaInfo);
                    }//Mantis bug 0000360 fixed
                    else if (manpowereditid != null)
                    {
                        if (manpowereditid.Contains(all_data_Of_Manpower_Est_Grid[I].REC_ID))
                        {
                            var InManpowerEstimaInfo = new Param_Update_Job_ManpowerEstimation();
                            InManpowerEstimaInfo.Manpower_Skill_ID = all_data_Of_Manpower_Est_Grid[I].SkillID.ToString();
                            InManpowerEstimaInfo.Unit_ID = all_data_Of_Manpower_Est_Grid[I].UnitID;
                            InManpowerEstimaInfo.Estimated_Qty = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Consumption);
                            InManpowerEstimaInfo.Est_Amount = all_data_Of_Manpower_Est_Grid[I].Est_Cost;
                            InManpowerEstimaInfo.Est_Rate = Convert.ToDecimal(all_data_Of_Manpower_Est_Grid[I].Estimated_Rate_per_Unit);
                            InManpowerEstimaInfo.Remarks = all_data_Of_Manpower_Est_Grid[I].Remarks;
                            InManpowerEstimaInfo.ID = all_data_Of_Manpower_Est_Grid[I].REC_ID;
                            UpdateManpowerEstimations.Add(InManpowerEstimaInfo);
                        }//Mantis bug 0000360 fixed
                    }
                }
                if (insertManPowerEstimations.Count > 0) { InParam.Added_JobManpowerEstimations = insertManPowerEstimations == null ? null : insertManPowerEstimations.ToArray(); }
                else { InParam.Added_JobManpowerEstimations = insertManPowerEstimations.ToArray(); }
                if (UpdateManpowerEstimations.Count > 0) { InParam.Updated_JobManpowerEstimations = UpdateManpowerEstimations[0] == null ? null : UpdateManpowerEstimations.ToArray(); }
                else { InParam.Updated_JobManpowerEstimations = UpdateManpowerEstimations.ToArray(); }
            }//Mantis bug 0000360 fixed
            if (Delete_Manpower_Estimation_Data_Session != null)
            {
                var all_data_Of_Manpower_Est_Grid_Deleted = Delete_Manpower_Estimation_Data_Session as ArrayList;
                if (all_data_Of_Manpower_Est_Grid_Deleted != null)
                {
                    var deleteItemEstID = new Param_Deleted_Job_ManpowerEstimation[all_data_Of_Manpower_Est_Grid_Deleted.Count];
                    for (int i = 0; i <= all_data_Of_Manpower_Est_Grid_Deleted.Count - 1; i++)
                    {
                        var deleteItemEstIDInfo = new Param_Deleted_Job_ManpowerEstimation();
                        deleteItemEstIDInfo.Rec_ID = Convert.ToInt32(all_data_Of_Manpower_Est_Grid_Deleted[i]);
                        deleteItemEstID[i] = deleteItemEstIDInfo;
                    }
                    InParam.Deleted_JobManpowerEstimations = deleteItemEstID;
                }
            }
        }
        public void EditIUDManpowerUsage(ref Param_Update_Job_Txn InParam)
        {
            var all_data_Of_ManpowerUsage = (List<Return_GetJobManpowerUsage>)Job_Actual_Manpower_Usage_Window_Grid_Data;
            if (all_data_Of_ManpowerUsage != null)
            {
                var insertManpowerUsage = new List<Param_Insert_Job_ManpowerUsage>();
                var UpdateManpowerUsage = new List<Param_Update_Job_ManpowerUsage>();
                int[] manpowereditid = JobEdit_Manpower_ID != null ? (int[])(JobEdit_Manpower_ID as ArrayList).ToArray(typeof(int)) : null;
                for (int I = 0; I <= all_data_Of_ManpowerUsage.Count() - 1; I++)
                {
                    if (all_data_Of_ManpowerUsage[I].REC_ID == 0)
                    {
                        var InManpowerUsageInfo = new Param_Insert_Job_ManpowerUsage();
                        InManpowerUsageInfo.Charge_ID = (Int32)all_data_Of_ManpowerUsage[I].Job_Manpower_ChargeID;//Mantis bug 0000362 fixed
                        InManpowerUsageInfo.Person_ID = Convert.ToInt32(all_data_Of_ManpowerUsage[I].Manpower_ID);
                        InManpowerUsageInfo.Period_From = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_From);
                        InManpowerUsageInfo.Period_To = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_To);
                        InManpowerUsageInfo.Units_Worked = all_data_Of_ManpowerUsage[I].Units_Worked;
                        InManpowerUsageInfo.Total_Amount = all_data_Of_ManpowerUsage[I].Total_Cost;
                        InManpowerUsageInfo.Remarks = all_data_Of_ManpowerUsage[I].Remarks;
                        InManpowerUsageInfo.Job_ID = 0;
                        insertManpowerUsage.Add(InManpowerUsageInfo);
                    }//Mantis bug 0000404 fixed
                    else if (manpowereditid != null)
                    {
                        if (manpowereditid.Contains(all_data_Of_ManpowerUsage[I].REC_ID))
                        {
                            var InManpowerUsageInfo = new Param_Update_Job_ManpowerUsage();
                            InManpowerUsageInfo.Person_ID = Convert.ToInt32(all_data_Of_ManpowerUsage[I].Manpower_ID);
                            InManpowerUsageInfo.Period_From = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_From);
                            InManpowerUsageInfo.Period_To = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].Work_Period_To);
                            InManpowerUsageInfo.Units_Worked = all_data_Of_ManpowerUsage[I].Units_Worked;
                            InManpowerUsageInfo.Total_Amount = all_data_Of_ManpowerUsage[I].Total_Cost;
                            InManpowerUsageInfo.Remarks = all_data_Of_ManpowerUsage[I].Remarks;
                            InManpowerUsageInfo.ID = all_data_Of_ManpowerUsage[I].REC_ID; //mantis #1421
                            UpdateManpowerUsage.Add(InManpowerUsageInfo);
                        }//Mantis bug 0000404 fixed
                    }
                }
                if (insertManpowerUsage.Count > 0) { InParam.Added_JobManpowerUsage = insertManpowerUsage[0] == null ? null : insertManpowerUsage.ToArray(); }
                else { InParam.Added_JobManpowerUsage = insertManpowerUsage.ToArray(); }//Mantis bug 0000404 fixed
                if (UpdateManpowerUsage.Count > 0) { InParam.Updated_JobManpowerUsage = UpdateManpowerUsage[0] == null ? null : UpdateManpowerUsage.ToArray(); }
                else { InParam.Updated_JobManpowerUsage = UpdateManpowerUsage.ToArray(); }//Mantis bug 0000404 fixed

            }
            var all_data_Of_ManpowerUsage_deleted = Delete_Actual_JobManpower_Usage_Data_Session as ArrayList;
            if (all_data_Of_ManpowerUsage_deleted != null)
            {
                var deleteManpowerUsageEstID = new Param_Deleted_Job_ManpowerUsage[all_data_Of_ManpowerUsage_deleted.Count];
                for (int i = 0; i <= all_data_Of_ManpowerUsage_deleted.Count - 1; i++)
                {
                    var deleteManpowerUsageIDInfo = new Param_Deleted_Job_ManpowerUsage();
                    deleteManpowerUsageIDInfo.Rec_ID = Convert.ToInt32(all_data_Of_ManpowerUsage_deleted[i]);
                    deleteManpowerUsageEstID[i] = deleteManpowerUsageIDInfo;
                }
                InParam.Deleted_JobManpowerUsage = deleteManpowerUsageEstID;
            }
        }
        public void EditIUDExpenses(ref Param_Update_Job_Txn InParam)
        {
            //var insertindex = 0;
            var all_data_Of_Expensesincurred_Grid = (List<Return_GetJobExpensesIncurred>)Job_Actual_Expenses_Grid_Data;
            if (all_data_Of_Expensesincurred_Grid != null)
            {
                var insertExpensesincurred = new List<Param_Insert_Job_ExpensesIncurred>();//Mantis bug 0000393 fixed
                for (int I = 0; I <= all_data_Of_Expensesincurred_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_Expensesincurred_Grid[I].REC_ID == 0)
                    {
                        var InExpensesincurred = new Param_Insert_Job_ExpensesIncurred();
                        InExpensesincurred.Job_ID = all_data_Of_Expensesincurred_Grid[I].REC_ID;
                        InExpensesincurred.Exp_Tr_ID = all_data_Of_Expensesincurred_Grid[I].Txn_ID;//Mantis bug 0000393 fixed
                        InExpensesincurred.Exp_Tr_Sr_No = Convert.ToInt32(all_data_Of_Expensesincurred_Grid[I].Txn_Sr_No);//Mantis bug 0000393 fixed
                        insertExpensesincurred.Add(InExpensesincurred);//Mantis bug 0000393 fixed                       
                    }
                }
                InParam.Added_JobExpensesIncurred = insertExpensesincurred.ToArray();
            }
            if (Delete_Expense_job != null)
            {
                var all_data_Of_Expensesincurred_Grid_deleted = Delete_Expense_job as ArrayList;
                var DeleteExpensesincurred = new Param_Deleted_Job_ExpensesIncurred[all_data_Of_Expensesincurred_Grid_deleted.Count];
                for (int I = 0; I <= all_data_Of_Expensesincurred_Grid_deleted.Count - 1; I++)
                {
                    var InExpensesincurred_deleted = new Param_Deleted_Job_ExpensesIncurred();
                    InExpensesincurred_deleted.Rec_ID = Convert.ToInt32(all_data_Of_Expensesincurred_Grid_deleted[I]);
                    DeleteExpensesincurred[I] = InExpensesincurred_deleted;
                }
                InParam.Deleted_JobExpensesIncurred = DeleteExpensesincurred;
            }
        }
        public void EditIUDMachineUsage(ref Param_Update_Job_Txn InParam)
        {
            var all_data_Of_MachineUsage_Grid = (List<Return_GetJobMachineUsage>)Job_Machine_Usage_Window_Grid_Data;
            if (all_data_Of_MachineUsage_Grid != null)
            {
                var insertMachineUsage = new List<Param_Insert_Job_MachineUsage>();//Mantis bug 0000404 fixed
                var updateMachineUsage = new List<Param_Update_Job_MachineUsage>();//Mantis bug 0000404 fixed
                int[] machineeditid = JobEdit_Machine_ID != null ? (int[])(JobEdit_Machine_ID as ArrayList).ToArray(typeof(int)) : null;
                for (int I = 0; I <= all_data_Of_MachineUsage_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_MachineUsage_Grid[I].REC_ID == 0)
                    {
                        var InMachineUsageInfo = new Param_Insert_Job_MachineUsage();
                        InMachineUsageInfo.Machine_ID = all_data_Of_MachineUsage_Grid[I].Machine_ID;
                        InMachineUsageInfo.Machine_Count = all_data_Of_MachineUsage_Grid[I].Mch_Count;
                        InMachineUsageInfo.Machine_Usage = Convert.ToDecimal(all_data_Of_MachineUsage_Grid[I].Usage_in_Hrs);
                        InMachineUsageInfo.Machine_Remarks = all_data_Of_MachineUsage_Grid[I].Remarks;
                        insertMachineUsage.Add(InMachineUsageInfo);//Mantis bug 0000404 fixed
                    }
                    else if (machineeditid != null)
                    {
                        if (machineeditid.Contains(all_data_Of_MachineUsage_Grid[I].REC_ID))
                        {
                            var InMachineUsageInfo = new Param_Update_Job_MachineUsage();
                            InMachineUsageInfo.Machine_ID = all_data_Of_MachineUsage_Grid[I].Machine_ID;
                            InMachineUsageInfo.Machine_Count = all_data_Of_MachineUsage_Grid[I].Mch_Count;
                            InMachineUsageInfo.Machine_Usage = Convert.ToDecimal(all_data_Of_MachineUsage_Grid[I].Usage_in_Hrs);
                            InMachineUsageInfo.Machine_Remarks = all_data_Of_MachineUsage_Grid[I].Remarks;
                            updateMachineUsage.Add(InMachineUsageInfo);
                        }
                    }
                }
                if (insertMachineUsage.Count > 0) { InParam.Added_JobMachineUsage = insertMachineUsage[0] == null ? null : insertMachineUsage.ToArray(); }
                else { InParam.Added_JobMachineUsage = insertMachineUsage.ToArray(); }
                if (updateMachineUsage.Count > 0) { InParam.Updated_JobMachineUsage = updateMachineUsage[0] == null ? null : updateMachineUsage.ToArray(); }
                else { InParam.Updated_JobMachineUsage = updateMachineUsage.ToArray(); }
            }//Mantis bug 0000404 fixed
            var MachineAllDeleteIds = JobDelete_MachineUsage_ID as ArrayList;
            if (MachineAllDeleteIds != null)
            {
                var deleteMachineID = new Param_Deleted_Job_MachineUsage[MachineAllDeleteIds.Count];
                for (int i = 0; i <= MachineAllDeleteIds.Count - 1; i++)
                {
                    var deleteMachineIDInfo = new Param_Deleted_Job_MachineUsage();
                    deleteMachineIDInfo.Rec_ID = Convert.ToInt32(MachineAllDeleteIds[i]);
                    deleteMachineID[i] = deleteMachineIDInfo;
                }
                InParam.Deleted_JobMachineUsage = deleteMachineID;
            }
        }
        public void EditIUDRemainingData(ref Param_Update_Job_Txn InParam, Model_NEVD_JobRequest model)
        {
            InParam.Job_Name = model.Job_Name == null ? "" : model.Job_Name;
            InParam.Job_Request_Date = model.Job_RequestDate;
            InParam.Job_Type = model.Job_Type == null ? "" : model.Job_Type;
            InParam.Job_Project_Id = model.Job_ProjectId;
            InParam.Job_Complex_Id = model.Job_ComplexId;
            InParam.Job_Requestor_Id = model.Job_RequestorId;
            InParam.Job_Assignee_Main_Dept_ID = model.Job_AssigneeMainDeptID;

            if (model.Job_Assignee_Sub_Dept_ID != null)//Mantis bug 0000374 fixed
            {
                var AssigneeSubDeptID = model.Job_Assignee_Sub_Dept_ID.Split(',');
                List<int?> AssignSubDeptID = new List<int?>();
                for (int i = 0; i < AssigneeSubDeptID.Count(); i++)
                {
                    AssignSubDeptID.Add(Convert.ToInt32(AssigneeSubDeptID[i]));
                }
                InParam.Job_Assignee_Sub_Dept_ID = AssignSubDeptID.ToArray();
            }//Mantis bug 0000374 fixed
            else
            {
                InParam.Job_Assignee_Sub_Dept_ID = null;
            }//Mantis bug 0000374 fixed


            InParam.Job_Assignee_Id = model.Job_AssigneeId;
            InParam.Job_Requested_Start_Date = model.Job_RequestedStartDate;
            InParam.Job_Requested_finish_Date = model.Job_RequestedFinishDate;
            InParam.Job_Start_Date = model.Job_StartDate;
            InParam.Job_Completion_Date = model.Job_EndDate;
            InParam.Job_Description = model.Job_Description;
            InParam.Job_Estimate_Required = model.Job_EstRequired;
            InParam.Job_Budget_Limit = Convert.ToDecimal(model.Job_BudgetLimit);
            InParam.Job_Requestor_Main_Dept_Id = model.Job_RequestorMainDeptId;
            InParam.Job_Requestor_Sub_Dept_Id = model.Job_RequestorSubDeptId;//Mantis bug 0000426 fixed
            InParam.Job_ID = model.Job_Id;
            InParam.Remarks = model.Job_Remarks;
            var allData = Delete_JObExisting_Remarks_ID as ArrayList;
            if (allData != null)
            {
                var deleteExist_RemarksID = new Param_Deleted_Remarks[allData.Count];
                for (int i = 0; i <= allData.Count - 1; i++)
                {
                    var deleteRemarksInfo = new Param_Deleted_Remarks();
                    deleteRemarksInfo.Rec_ID = Convert.ToInt32(allData[i]);
                    deleteExist_RemarksID[i] = deleteRemarksInfo;
                }
                InParam.Deleted_Remarks = deleteExist_RemarksID;
            }
        }

        #endregion Edit InParam Functions
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE,ClientScreen.Stock_Job,"Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#JobRequestRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Job_Request_Requestor_Quick(string PostSuccessFunction = null, string PopupID = "popup_frm_quick_JRR", string CallingScreen = "", int? projectID = null)
        {
            Job_NEVD_Rights();
            if ((!CheckRights(BASE,ClientScreen.Stock_Job,"Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#"+ PopupID + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");                
            }
            Model_Job_QuickJob model = new Model_Job_QuickJob();
            model.Quick_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "OnJobRequestRegisterAjaxSuccessForm";
            model.Quick_popupID = PopupID != null ? PopupID : "popup_frm_quick_JRR";
            model.Quick_Job_RequestDate = DateTime.Now;
            model.Quick_CallingScreen = CallingScreen;
            if (CallingScreen == "Project")
            {
                var projdata = BASE._Projects_Dbops.GetRecord(Convert.ToInt32(projectID));
                //var ProjMainAssigneeDept = Convert.ToInt32(projdata[0].Proj_Assignee_Main_Dept_Id);
                //var ProjSubAssigneeDept = Convert.ToInt32(projdata[0].Proj_Assignee_Sub_Dept_Id);                
                //model.Quick_Job_AssigneeMainDeptID = ProjMainAssigneeDept;
                //model.Quick_Job_AssigneeSubDeptID = ProjSubAssigneeDept;
                model.Quick_Job_ProjectId = projectID;//Mantis bug 0000466 fixed
                model.Quick_Job_ComplexId = projdata[0].Proj_Complex_Id;//Mantis bug 0000466 fixed
            }
            return View(model);
        }
        public ActionResult Frm_NEVD_Job_Request_Quick(Model_Job_QuickJob model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Quick_Job_Name))
                {
                    return Json(new
                    {
                        message = "Quick Job Name cannot be Blank . . . !",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Quick_Job_Name = model.Quick_Job_Name.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                if (string.IsNullOrWhiteSpace(model.Quick_Job_Description))
                {
                    return Json(new
                    {
                        message = "Job Description cannot be Blank . . . !",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Quick_Job_Description = model.Quick_Job_Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }

                var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                if (model.Quick_Job_RequestDate < BASE._open_Year_Sdt || model.Quick_Job_RequestDate > BASE._open_Year_Edt)
                {
                    return Json(new
                    {
                        message = "Job Request Date Date Should Be Within Open Financial Year ...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (AuditedPeriod.Rows.Count > 0)//Mantis bug 0000401 fixed
                    {
                        if (model.Quick_Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Quick_Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "Request Date should not be in Audited period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (SubmittedPeriod.Rows.Count > 0)//Mantis bug 0000401 fixed
                    {
                        if (model.Quick_Job_RequestDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Quick_Job_RequestDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "Request Date Should Not Be In Account Submission Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Quick_Job_ProjectId > 0)
                {
                    var projdata = BASE._Projects_Dbops.GetRecord(Convert.ToInt32(model.Quick_Job_ProjectId));
                    var ProjMainAssigneeDept = projdata[0].AssigneeMainDept.Split(',').Select(x => x.Trim()).ToArray();//Mantis bug 0000401 fixed//Mantis bug 0001209 fixed

                    if (!ProjMainAssigneeDept.Contains(model.Quick_Job_AssigneeMainDeptID.ToString()))//Mantis bug 0001209 fixed
                    {
                        return Json(new
                        {
                            message = "Job Assignee Main Dept Should Not Be Outside The Main Assignee Dept Selected For Project...!!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Insert_Job_Txn InParam = new Param_Insert_Job_Txn();

                InParam.Job_Name = model.Quick_Job_Name == null ? "" : model.Quick_Job_Name;
                InParam.Job_Request_Date = model.Quick_Job_RequestDate;
                InParam.Job_Type = model.Quick_Job_Type == null ? "" : model.Quick_Job_Type;
                InParam.Job_Project_Id = model.Quick_Job_ProjectId;
                InParam.Job_Complex_Id = model.Quick_Job_ComplexId;
                InParam.Job_Assignee_Main_Dept_ID = Convert.ToInt32(model.Quick_Job_AssigneeMainDeptID);


                if (model.Job_QJ_Assignee_Sub_Dept_ID != null)//Mantis bug 0000374 fixed
                {
                    var QJ_AssigneeSubDeptID = model.Job_QJ_Assignee_Sub_Dept_ID.Split(',');
                    List<int?> QJ_AssignSubDeptID = new List<int?>();
                    for (int j = 0; j < QJ_AssigneeSubDeptID.Count(); j++)
                    {
                        QJ_AssignSubDeptID.Add(Convert.ToInt32(QJ_AssigneeSubDeptID[j]));
                    }
                    InParam.Job_Assignee_Sub_Dept_ID = QJ_AssignSubDeptID.ToArray();
                }//Mantis bug 0000374 fixed
                else
                {
                    InParam.Job_Assignee_Sub_Dept_ID = null;
                }//Mantis bug 0000374 fixed              
                InParam.Job_Description = model.Quick_Job_Description;
                InParam.Job_Requestor_Id = (int)BASE._open_User_PersonnelID;
                InParam.Job_Requestor_Main_Dept_Id = (int)BASE._open_User_MainDeptID;
                InParam.Job_Requestor_Sub_Dept_Id = BASE._open_User_SubDeptID; //Mantis bug 0000617 fixed

                if (BASE._Jobs_Dbops.InsertJob(InParam))
                {
                    return Json(new { result = true, message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ContextMenuChecks(int jobid)
        {
            var jobdata = BASE._Jobs_Dbops.GetRecord(jobid);
            int AssignMainDeptID = jobdata.Job_AssigneeMainDeptID;
            string AssignSubDeptID = jobdata.Job_AssigneeSubDeptID;//Mantis bug 0000374 fixed
            if (BASE._open_User_MainDeptID == AssignMainDeptID || AssignSubDeptID.Contains(BASE._open_User_SubDeptID.ToString()))//Mantis bug 0000374 fixed
            {
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "Not Allowed!!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getUORRCount(int jobid)
        {
            var uocount = BASE._Jobs_Dbops.GetJob_UO_Count(jobid);
            var rrcount = BASE._Jobs_Dbops.GetJob_RR_Count(jobid);
            if (uocount > 0)
            {
                return Json(new
                {
                    Message = "A Job Cannot Be Deleted If UO Is Posted Against It..!!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else if (rrcount > 0)
            {
                return Json(new
                {
                    Message = "A Job Cannot Be Deleted If RR Is Posted Against It..!!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void InnerGridDataClear()
        {
            Temp_Job_Item_Usage_Est_Window_Grid_Data = Job_Item_Usage_Est_Window_Grid_Data;
            Temp_JobEdit_itemusage_ID = JobEdit_itemusage_ID;
            Temp_Delete_ItemEstimation_ID = Delete_ItemEstimation_ID;
            Temp_Job_Actual_Expenses_Grid_Data = Job_Actual_Expenses_Grid_Data;
            Temp_Delete_Expense_prod_job = Delete_Expense_job;//Mantis bug 0000393 fixed
            Temp_Job_Machine_Usage_Window_Grid_Data = Job_Machine_Usage_Window_Grid_Data;
            Temp_JobDelete_MachineUsage_ID = JobDelete_MachineUsage_ID;
            Temp_JobEdit_Machine_ID = JobEdit_Machine_ID;
            Temp_Job_Actual_Manpower_Usage_Window_Grid_Data = Job_Actual_Manpower_Usage_Window_Grid_Data;
            Temp_JobEdit_Manpower_ID = JobEdit_Manpower_ID;
            Temp_Delete_Actual_JobManpower_Usage_Data_Session = Delete_Actual_JobManpower_Usage_Data_Session;
            Temp_Job_ManpowerEstimation_Grid_Data = Job_ManpowerEstimation_Grid_Data;
            Temp_JobEdit_Manpowerest_ID = JobEdit_Manpowerest_ID;
            Temp_Delete_Manpower_Estimation_Data_Session = Delete_Manpower_Estimation_Data_Session;
        }
        public void InnerGridDataRestore()
        {
            Job_Item_Usage_Est_Window_Grid_Data = Temp_Job_Item_Usage_Est_Window_Grid_Data;
            JobEdit_itemusage_ID = Temp_JobEdit_itemusage_ID;
            Delete_ItemEstimation_ID = Temp_Delete_ItemEstimation_ID;
            Job_Actual_Expenses_Grid_Data = Temp_Job_Actual_Expenses_Grid_Data;
            Delete_Expense_job = Temp_Delete_Expense_prod_job;//Mantis bug 0000393 fixed
            Job_Machine_Usage_Window_Grid_Data = Temp_Job_Machine_Usage_Window_Grid_Data;
            JobDelete_MachineUsage_ID = Temp_JobDelete_MachineUsage_ID;
            JobEdit_Machine_ID = Temp_JobEdit_Machine_ID;
            Job_Actual_Manpower_Usage_Window_Grid_Data = Temp_Job_Actual_Manpower_Usage_Window_Grid_Data;
            JobEdit_Manpower_ID = Temp_JobEdit_Manpower_ID;
            Delete_Actual_JobManpower_Usage_Data_Session = Temp_Delete_Actual_JobManpower_Usage_Data_Session;
            Job_ManpowerEstimation_Grid_Data = Temp_Job_ManpowerEstimation_Grid_Data;
            JobEdit_Manpowerest_ID = Temp_JobEdit_Manpowerest_ID;
            Delete_Manpower_Estimation_Data_Session = Temp_Delete_Manpower_Estimation_Data_Session;
        }
        public ActionResult JobStatusToNew(int jobid)
        {
            Param_Update_Job_Status param = new Param_Update_Job_Status();
            param.JobID = jobid;
            param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "_New", true);
            if (BASE._Jobs_Dbops.UpdateJobStatus(param))
            {
                return Json(new
                {
                    result = true,
                    message = "Status Changed To New..!!"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteEstimations(int itemest, int manest)
        {
            if (itemest > 0)
            {
                var itemestdata = Job_Item_Usage_Est_Window_Grid_Data as List<Return_GetJobItemEstimates>;
                var deleteItemEst = Delete_ItemEstimation_ID as ArrayList;
                for (int i = 0; i < itemestdata.Count; i++)
                {
                    if (itemestdata[i].REC_ID > 0)
                    {
                        deleteItemEst.Add(itemestdata[i].REC_ID);
                    }
                }
                Delete_ItemEstimation_ID = deleteItemEst;
                Job_Item_Usage_Est_Window_Grid_Data = null;
            }
            if (manest > 0)
            {
                var manestdata = Job_ManpowerEstimation_Grid_Data as List<Return_GetJobManpowerEstimates>;
                var deletemanEst = Delete_Manpower_Estimation_Data_Session as ArrayList;
                for (int i = 0; i < manestdata.Count; i++)
                {
                    if (manestdata[i].REC_ID > 0)
                    {
                        deletemanEst.Add(manestdata[i].REC_ID);
                    }
                }
                Delete_Manpower_Estimation_Data_Session = deletemanEst;
                Job_ManpowerEstimation_Grid_Data = null;
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckDocumentsLinked(int jobid)
        {
            var docdata = Job_Documents_Window_Grid_Data as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._Jobs_Dbops.GetAttachmentLinkScreen(jobid, docdata[i].ID);
                    if (!string.IsNullOrEmpty(screen))
                    {
                        return Json(new
                        {
                            result = true,
                            message = "There Are Documents That Cannot Be Deleted Because They Have Been Linked To Other Screens</br> Do You Want To Unlink It From Job..? "
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new
            {
                result = false,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion NEVD
        #region DropDowns

        #region Project and Complex Name DropDowns

        public ActionResult Complexes_GetList(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._Complexes_DBOps.GetInstList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        public ActionResult Get_Project_Name_List(DataSourceLoadOptions loadOptions)
        {
            var Get_Project_Name_List = BASE._Projects_Dbops.GetList();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Project_Name_List, loadOptions)));
        }

        public ActionResult Get_Project_Name_List_QJ(DataSourceLoadOptions loadOptions)
        {
            var Get_Project_Name_List_QJ = BASE._Projects_Dbops.GetList();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Project_Name_List_QJ, loadOptions)));
        }

        public ActionResult Get_JobType_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetJobTypes> JobType_List = BASE._Jobs_Dbops.GetJobTypes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(JobType_List, loadOptions)));
        }

        #endregion Project and Complex Name DropDowns

        #region Department Requestors DropDowns

        //Requestor's Main Department:
        public ActionResult GetStockMainDept(DataSourceLoadOptions loadOptions)
        {
            var requesterMainDeptList = BASE._StockDeptStores_dbops.GetMainDeptList(Common_Lib.RealTimeService.ClientScreen.Stock_Dept_Store_Master);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(requesterMainDeptList, loadOptions)));
        }

        //Requestor's Sub-Department:
        public ActionResult GetStockSubDept(DataSourceLoadOptions loadOptions, int MainDeptID = 0)
        {
            List<Return_GetStoreDept> requesterSubDeptList = new List<Return_GetStoreDept>();
            if (MainDeptID == 0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(requesterSubDeptList, loadOptions)));
            }
            requesterSubDeptList = BASE._StockDeptStores_dbops.GetSubDeptList(ClientScreen.Stock_Job, MainDeptID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(requesterSubDeptList, loadOptions)));
        }

        //Assign's Main-Department:
        public ActionResult Get_Stock_Assigns_MainDept(DataSourceLoadOptions loadOptions)
        {
            var assignsMainDeptData = BASE._StockDeptStores_dbops.GetMainDeptList(ClientScreen.Stock_Job);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignsMainDeptData, loadOptions)));
        }

        public ActionResult Get_Stock_Assigns_MainDept_QJ(DataSourceLoadOptions loadOptions)
        {
            var assignsMainDeptData = BASE._StockDeptStores_dbops.GetMainDeptList(ClientScreen.Stock_Job);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignsMainDeptData, loadOptions)));
        }

        //Assign's Sub-Department:
        public ActionResult Get_Stock_Assign_SubDept(DataSourceLoadOptions loadOptions, int AssigMainDeptID = 0, int? AssigneepersonnelID= null)
        {
            List<Return_GetStoreDept> assignSubDeptList = new List<Return_GetStoreDept>();
            if (AssigMainDeptID == 0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignSubDeptList, loadOptions)));
            }
            assignSubDeptList = BASE._StockDeptStores_dbops.GetSubDeptList(ClientScreen.Stock_Job, AssigMainDeptID, Convert.ToInt32(AssigneepersonnelID));

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignSubDeptList, loadOptions)));
        }

        public ActionResult Get_Stock_Assign_SubDept_QJ(DataSourceLoadOptions loadOptions, int AssigMainDeptID = 0, int? AssigneepersonnelID = null)
        {
            List<Return_GetStoreDept> assignSubDeptList = new List<Return_GetStoreDept>();
            if (AssigMainDeptID == 0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignSubDeptList, loadOptions)));
            }
            assignSubDeptList = BASE._StockDeptStores_dbops.GetSubDeptList(ClientScreen.Stock_Job, AssigMainDeptID, Convert.ToInt32(AssigneepersonnelID));

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(assignSubDeptList, loadOptions)));
        }

        public ActionResult Get_Requestor_Names(DataSourceLoadOptions loadOption)
        {
            var jobRequestorNames = BASE._Jobs_Dbops.GetStockPersonnels();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(jobRequestorNames, loadOption)));
        }

        public JsonResult GetContact_No(int assignId = 0)
        {
            string assignContactNo = "";
            var jobRequestorContact_No = BASE._Jobs_Dbops.GetStockPersonnels();
            foreach (var item in jobRequestorContact_No)
            {
                if (item.ID == assignId)
                {
                    assignContactNo = item.Mobile_No;
                }
            }
            return Json(new { AssignerContactNo = assignContactNo, result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Stock_Assign_Names(DataSourceLoadOptions loadOption)
        {
            var jobAssignersName = BASE._Jobs_Dbops.GetStockPersonnels();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(jobAssignersName, loadOption)));
        }

        #endregion Department Requestors DropDowns

        #endregion DropDowns
        #region Inside Grids
        #region Documents

        public ActionResult InnerGrid_DocumentsGrid(string ActionMethodName, int JobId = 0)
        {
            var docList = new List<Return_GetDocumentsGridData>();
            if (ActionMethodName == "New")
            {
                return PartialView(docList);
            }
            if (Job_Documents_Window_Grid_Data == null)
            {
                var docData = BASE._Jobs_Dbops.GetJobDocuments(JobId);
                if (docData != null)
                {
                    docList = docData;
                }
                Job_Documents_Window_Grid_Data = docList;
            }
            return PartialView(Job_Documents_Window_Grid_Data);
        }
        public ActionResult Job_Documents_Attachment()
        {
            Model_Attachment_Window model = (Model_Attachment_Window) GetBaseSession("Job_Documents_Attachment_AttachmentData");

            if (model.Help_REF_REC_ID != null)
            {
                try
                {
                    Parameter_Insert_Attachment InEInfo = new Parameter_Insert_Attachment();

                    InEInfo.FileName = model.Help_Document_FileName;
                    InEInfo.Description = model.Help_Document_Description;
                    InEInfo.NameID = model.Help_Document_NameID;
                    InEInfo.Ref_Screen = "Jobs";
                    InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                    InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                    InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                    InEInfo.File = model.Help_filefield;
                    InEInfo.RecID = System.Guid.NewGuid().ToString();
                    if (BASE._Attachments_DBOps.Insert(InEInfo).Length>0)
                    {
                        return Json(new { result = true, message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
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
            else
            {
                List<Return_GetDocumentsGridData> gridRows = new List<Return_GetDocumentsGridData>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Job_Documents_Window_Grid_Data != null)
                {
                    gridRows = (List<Return_GetDocumentsGridData>)Job_Documents_Window_Grid_Data;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr_No) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.ActionMethod == "New")
                {
                    Return_GetDocumentsGridData grid = new Return_GetDocumentsGridData();
                    grid.Sr_No = NewSr;
                    grid.Document_Type = model.Help_Document_CategoryID;
                    grid.Document_Name = model.Help_Document_Name;
                    grid.File_Name = model.Help_Document_FileName;
                    grid.Remarks = model.Help_Document_Description;
                    grid.File_Array = model.Help_filefield;
                    grid.Applicable_From = model.Help_Doc_From_Date;
                    grid.Applicable_To = model.Help_Doc_To_Date;
                    grid.Document_Name_ID = model.Help_Document_NameID;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);
                }
                if (model.ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr_No == model.Sr_no);
                    var editDocumentID = new ArrayList();
                    if (model.ID != null || model.ID != "0" || model.ID != "")
                    {
                        var editDocument = JobEdit_Document_ID as ArrayList;
                        if (editDocument != null)
                        {
                            editDocument.Add(model.ID);
                            JobEdit_Document_ID = editDocument;
                        }
                        else
                        {
                            editDocumentID.Add(model.ID);
                            JobEdit_Document_ID = editDocumentID;
                        }
                    }
                    dataToEdit.File_Name = model.Help_Document_FileName;
                    dataToEdit.Document_Type = model.Help_Document_CategoryID;
                    dataToEdit.Document_Name = model.Help_Document_Name;
                    dataToEdit.Remarks = model.Help_Document_Description;
                    dataToEdit.File_Array = model.Help_filefield;
                    dataToEdit.Applicable_From = model.Help_Doc_From_Date;
                    dataToEdit.Applicable_To = model.Help_Doc_To_Date;
                    dataToEdit.Document_Name_ID = model.Help_Document_NameID;
                }
                Job_Documents_Window_Grid_Data = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Job_Documents_Attachment_LinkCheck(string DocId, int JobId)
        {
            var screen = BASE._Jobs_Dbops.GetAttachmentLinkScreen(JobId, DocId);
            if (!string.IsNullOrEmpty(screen))
            {      
                if (screen != "Jobs")
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From Job..?"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To Some Other Entry In " + screen + ".Do You Want To Unlink It From This Entry..?"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_DocumentsDetail_Window_Delete_Grid_Record(string ActionMethod, string SrID = null, string Doc_ID = null)
        {
            var srid = Convert.ToInt32(SrID);
            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)Job_Documents_Window_Grid_Data;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (allDocData != null)
            {
                allDocData.Remove(dataToDelete);
            }
            Job_Documents_Window_Grid_Data = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    var deleteDocumentID = new ArrayList();
                    var deleteDocument = JobDelete_Document_ID as ArrayList;
                    if (deleteDocument != null)
                    {
                        deleteDocument.Add(Doc_ID);
                        JobDelete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID.Add(Doc_ID);
                        JobDelete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {
                var unlinkDocumentID = new ArrayList();
                var unlinkDocument = JobUnlink_Document_ID as ArrayList;
                if (unlinkDocument != null)
                {
                    unlinkDocument.Add(Doc_ID);
                    JobUnlink_Document_ID = unlinkDocument;
                }
                else
                {
                    unlinkDocumentID.Add(Doc_ID);
                    JobUnlink_Document_ID = unlinkDocumentID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion Documents
        #region Item Usage

        public ActionResult InnerGrid_ItemUsagesEstimationGrid(string ActionMethodName, int JobId = 0)
        {
            var ItemEstList = new List<Return_GetJobItemEstimates>();
            if (ActionMethodName == "New")
            {
                return PartialView(ItemEstList);
            }
            if (Job_Item_Usage_Est_Window_Grid_Data == null)
            {
                var itemUsageEstdata = BASE._Jobs_Dbops.GetJobItemEstimates(JobId);
                if (itemUsageEstdata != null)
                {
                    ItemEstList = itemUsageEstdata;
                }
                Job_Item_Usage_Est_Window_Grid_Data = ItemEstList;
            }
            return PartialView(Job_Item_Usage_Est_Window_Grid_Data);
        }

        public ActionResult Frm_Job_Request_Add_Item_Estimate(string ActionMethod = null, int SrID = 0)
        {
            JobItemUsageEstimation model = new JobItemUsageEstimation();
            model.Job_Item_ActionMethod = ActionMethod;
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Return_GetJobItemEstimates>)Job_Item_Usage_Est_Window_Grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetJobItemEstimates();
                model.Job_Item_Sr_No = Sr;
                model.Job_ItemName = dataToEdit.Item;
                model.Job_Item_Type = dataToEdit.ItemType;
                model.Job_Item_Code = dataToEdit.Item_Code;
                model.Job_Unit = dataToEdit.Unit;
                model.Job_Estimated_Quantity_Usage = Convert.ToDouble(dataToEdit.Quantity);
                model.Job_Estimated_Rate = Convert.ToDouble(dataToEdit.Est_Rate);
                model.Job_Estimated_Amount = Convert.ToDouble(dataToEdit.Est_Amount);
                model.Job_UnitID = dataToEdit.UnitID;
                model.Job_ItemID = dataToEdit.Item_ID;
                model.Job_Item_REC_ID = dataToEdit.REC_ID;// mantis bug 613 solved
                if (dataToEdit.Tolerance != null) { model.Job_Tolerance = Convert.ToDouble(dataToEdit.Tolerance); }
                model.Job_ItemUsage_Remarks = dataToEdit != null ? dataToEdit.Remarks : "";
            }
            return View(model);
        }

        public ActionResult Frm_Item_Usage_Estimate(JobItemUsageEstimation model)
        {
            var actionmethod = model.Job_Item_ActionMethod;
            if (actionmethod == "New" || actionmethod == "Edit")
            {
                if (model.Job_Tolerance > 100)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Tolerance Cannot Be Greater Than 100"
                    }, JsonRequestBehavior.AllowGet);
                }

                if (model.Job_ItemUsage_Remarks != null)
                {
                    model.Job_ItemUsage_Remarks = model.Job_ItemUsage_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }

            }
            List<Return_GetJobItemEstimates> gridRows = new List<Return_GetJobItemEstimates>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Job_Item_Usage_Est_Window_Grid_Data != null)
            {
                gridRows = (List<Return_GetJobItemEstimates>)Job_Item_Usage_Est_Window_Grid_Data;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetJobItemEstimates grid = new Return_GetJobItemEstimates();
                grid.Sr = NewSr;
                grid.Item = model.Job_ItemName;
                grid.ItemType = model.Job_Item_Type;
                grid.Item_Code = model.Job_Item_Code;
                grid.Quantity = Convert.ToDecimal(model.Job_Estimated_Quantity_Usage);
                grid.Unit = model.Job_Unit;
                grid.Est_Rate = Convert.ToDecimal(model.Job_Estimated_Rate);
                grid.Est_Amount = Convert.ToDecimal(model.Job_Estimated_Amount);
                if (model.Job_Tolerance != null) { grid.Tolerance = Convert.ToDecimal(model.Job_Tolerance); }
                grid.Remarks = model.Job_ItemUsage_Remarks;
                grid.UnitID = model.Job_UnitID;
                grid.REC_ADD_BY = BASE._open_User_ID;
                grid.REC_ADD_ON = DateTime.Now;
                grid.Item_ID = Convert.ToInt32(model.Job_ItemID);
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Job_Item_Sr_No);
                dataToEdit.Item = model.Job_ItemName;
                dataToEdit.ItemType = model.Job_Item_Type;
                dataToEdit.Item_Code = model.Job_Item_Code;
                dataToEdit.Quantity = Convert.ToDecimal(model.Job_Estimated_Quantity_Usage);
                dataToEdit.Unit = model.Job_Unit;
                dataToEdit.Est_Rate = Convert.ToDecimal(model.Job_Estimated_Rate);
                dataToEdit.Est_Amount = Convert.ToDecimal(model.Job_Estimated_Amount);
                if (model.Job_Tolerance != null)
                {
                    dataToEdit.Tolerance = Convert.ToDecimal(model.Job_Tolerance);
                }
                dataToEdit.Remarks = model.Job_ItemUsage_Remarks;
                dataToEdit.UnitID = model.Job_UnitID;
                dataToEdit.Item_ID = Convert.ToInt32(model.Job_ItemID);
                var edititemusageID = new ArrayList();
                if (model.Job_Item_REC_ID != 0)
                {
                    var edititem = JobEdit_itemusage_ID as ArrayList;
                    if (edititem != null)
                    {
                        edititem.Add(model.Job_Item_REC_ID);
                        JobEdit_itemusage_ID = edititem;
                    }
                    else
                    {
                        edititemusageID.Add(model.Job_Item_REC_ID);
                        JobEdit_itemusage_ID = edititemusageID;
                    }
                }
            }
            Job_Item_Usage_Est_Window_Grid_Data = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_ItemEstimation_Window_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Rec_Id = 0)
        {
            var allItemEstData = (List<Return_GetJobItemEstimates>)Job_Item_Usage_Est_Window_Grid_Data;
            var dataToDelete = allItemEstData != null ? allItemEstData.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetJobItemEstimates();
            if (allItemEstData != null)
            {
                allItemEstData.Remove(dataToDelete);
            }
            Job_Item_Usage_Est_Window_Grid_Data = allItemEstData;
            var deleteItemEstID = new ArrayList();
            if (Rec_Id != 0)
            {
                var deleteItemEst = Delete_ItemEstimation_ID as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(Rec_Id);
                    Delete_ItemEstimation_ID = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(Rec_Id);
                    Delete_ItemEstimation_ID = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LookUp_GetItem_Names(bool? IsVisible, DataSourceLoadOptions loadOptions, int? StoreID)//Mantis bug 0000763 fixed
        {
            if (StoreID == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockProduction.Return_GetStockItems>(), loadOptions)), "application/json");
            }//Mantis bug 0000763 fixed
            var itemData = BASE._Stock_Production_DBOps.GetStockItems((int)StoreID);//Mantis bug 0000763 fixed            
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(itemData, loadOptions)), "application/json");
        }

        #endregion Item Usage
        #region Machine

        public ActionResult InnerGrid_ActualMachineUsageGrid(string ActionMethodName, int JobId = 0)
        {
            var MachineList = new List<Return_GetJobMachineUsage>();
            if (ActionMethodName == "New")
            {
                return PartialView(MachineList);
            }

            if (Job_Machine_Usage_Window_Grid_Data == null)
            {
                var MachineUsageData = BASE._Jobs_Dbops.GetJobMachineUsage(JobId);
                if (MachineUsageData != null)
                {
                    MachineList = MachineUsageData;
                }
                Job_Machine_Usage_Window_Grid_Data = MachineList;
            }
            return PartialView(Job_Machine_Usage_Window_Grid_Data);
        }

        public ActionResult Frm_Job_Machinery_Used(string ActionMethod = null, int SrID = 0, int jobid = 0)
        {
            JobMachineUsageDetail model = new JobMachineUsageDetail();
            model.Job_Machine_ActionMethod = ActionMethod;
            model.jobID = jobid;
            if (ActionMethod == "New")
            {
                model.Job_Machine_Count = 1;
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Return_GetJobMachineUsage>)Job_Machine_Usage_Window_Grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetJobMachineUsage();
                model.Job_Machine_Sr_No = Sr;
                model.Job_Machine_Name = dataToEdit != null ? dataToEdit.Machine_Name.ToString() : "";
                model.Job_Machine_Count = dataToEdit.Mch_Count;
                model.Job_Machine_Usage_Hours = Convert.ToDouble(dataToEdit.Usage_in_Hrs);
                model.Job_Machine_Remarks = dataToEdit != null ? dataToEdit.Remarks : "";
                model.Job_Machine_NameID = dataToEdit.Machine_ID;
                model.Job_Machine_No = dataToEdit.Machine_No;
                model.Job_Machine_REC_ID = dataToEdit.REC_ID;
            }
            return View(model);
        }

        public ActionResult Frm_Machinery_Used(JobMachineUsageDetail model)
        {
            var actionmethod = model.Job_Machine_ActionMethod;
            if (actionmethod == "New" || actionmethod == "Edit")
            {
                if (Convert.ToDouble(model.Job_Machine_Count) > Convert.ToDouble(model.Job_Machine_Curr_Qty))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Machine Count Cannot Be Greater Than Total Qty of Machine selected..."
                    }, JsonRequestBehavior.AllowGet);
                }

                if (model.Job_Machine_Remarks != null)
                {
                    model.Job_Machine_Remarks = model.Job_Machine_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
            }
            if (model.jobID != 0)
            {
                try
                {
                    Param_Insert_Job_MachineUsage inparam = new Param_Insert_Job_MachineUsage();
                    inparam.Job_ID = Convert.ToInt32(model.jobID);
                    inparam.Machine_Count = Convert.ToInt32(model.Job_Machine_Count);
                    inparam.Machine_ID = Convert.ToInt32(model.Job_Machine_NameID);
                    inparam.Machine_Remarks = model.Job_Machine_Remarks;
                    inparam.Machine_Usage = Convert.ToDecimal(model.Job_Machine_Usage_Hours);
                    if (BASE._Jobs_Dbops.InsertJobMachineUsage(inparam))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
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
            else
            {
                List<Return_GetJobMachineUsage> gridRows = new List<Return_GetJobMachineUsage>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Job_Machine_Usage_Window_Grid_Data != null)
                {
                    gridRows = (List<Return_GetJobMachineUsage>)Job_Machine_Usage_Window_Grid_Data;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (actionmethod == "New")
                {
                    Return_GetJobMachineUsage grid = new Return_GetJobMachineUsage();
                    grid.Sr = NewSr;
                    grid.Machine_Name = model.Job_Machine_Name;
                    grid.Machine_No = model.Job_Machine_No;
                    grid.Mch_Count = Convert.ToInt32(model.Job_Machine_Count);
                    grid.Usage_in_Hrs = Convert.ToDecimal(model.Job_Machine_Usage_Hours);
                    grid.Remarks = model.Job_Machine_Remarks;
                    grid.Machine_ID = Convert.ToInt32(model.Job_Machine_NameID);
                    grid.REC_ADD_BY = BASE._open_User_ID;
                    grid.REC_ADD_ON = DateTime.Now;
                    gridRows.Add(grid);
                }
                else if (actionmethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Job_Machine_Sr_No);
                    dataToEdit.Machine_Name = model.Job_Machine_Name;
                    dataToEdit.Machine_No = model.Job_Machine_No;
                    dataToEdit.Mch_Count = Convert.ToInt32(model.Job_Machine_Count);
                    dataToEdit.Usage_in_Hrs = Convert.ToDecimal(model.Job_Machine_Usage_Hours);
                    dataToEdit.Remarks = model.Job_Machine_Remarks;
                    dataToEdit.Machine_ID = Convert.ToInt32(model.Job_Machine_NameID);
                    if (model.Job_Machine_REC_ID != 0)
                    {
                        var editStockConID = new ArrayList();
                        var editStockCon = JobEdit_Machine_ID as ArrayList;
                        if (editStockCon != null)
                        {
                            editStockCon.Add(model.Job_Machine_REC_ID);
                            JobEdit_Machine_ID = editStockCon;
                        }
                        else
                        {
                            editStockConID.Add(model.Job_Machine_REC_ID);
                            JobEdit_Machine_ID = editStockConID;
                        }
                    }
                }
                Job_Machine_Usage_Window_Grid_Data = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_MachineUsageDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null, int Rec_Id = 0)
        {
            var allMachineData = (List<Return_GetJobMachineUsage>)Job_Machine_Usage_Window_Grid_Data;
            var dataToDelete = allMachineData != null ? allMachineData.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetJobMachineUsage();//0000252 bug fixed
            if (allMachineData != null)
            {
                allMachineData.Remove(dataToDelete);
            }
            Job_Machine_Usage_Window_Grid_Data = allMachineData;
            var deleteMachineID = new ArrayList();
            if (Rec_Id != 0)
            {
                var deleteMachine = JobDelete_MachineUsage_ID as ArrayList;
                if (deleteMachine != null)
                {
                    deleteMachine.Add(Rec_Id);
                    JobDelete_MachineUsage_ID = deleteMachine;
                }
                else
                {
                    deleteMachineID.Add(Rec_Id);
                    JobDelete_MachineUsage_ID = deleteMachineID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LookUp_GetMachine_Name(DataSourceLoadOptions loadOptions)
        {
            var MachineData = BASE._Jobs_Dbops.Get_Machines();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MachineData, loadOptions)), "application/json");
        }

        #endregion Machine
        #region Remarks

        public ActionResult InnerGrid_RemarksGrid(string ActionMethodName, int JobId = 0)
        {
            var RemarksList = new List<Return_GetJobRemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(RemarksList);
            }
            if (Job_Existing_Remarks_Grid_Data == null)
            {
                var RemarksData = BASE._Jobs_Dbops.GetJobRemarks(JobId);
                if (RemarksData != null)
                {
                    RemarksList = RemarksData;
                }
                Job_Existing_Remarks_Grid_Data = RemarksList;
            }
            return PartialView(Job_Existing_Remarks_Grid_Data);
        }

        public ActionResult Frm_ExistingRemarks_Window_Delete_Grid_Record(string ActionMethod, int? ID = 0)
        {
            var id = Convert.ToInt16(ID);
            var allData = (List<Return_GetJobRemarks>)Job_Existing_Remarks_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == id).FirstOrDefault() : new Return_GetJobRemarks();
            if (dataToDelete.Remarks_By != BASE._open_User_ID)
            {
                return Json(new
                {
                    result = false,
                    message = "A User Is Allowed To Delete His Own Remarks Only..!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Job_Existing_Remarks_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new ArrayList();
                var deleteExistingRemarks = Delete_JObExisting_Remarks_ID as ArrayList;
                if (deleteExistingRemarks != null)
                {
                    deleteExistingRemarks.Add(id);
                    Delete_JObExisting_Remarks_ID = deleteExistingRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_JObExisting_Remarks_ID = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_ViewRemarks(int SR = 0)
        {
            var all_data = (List<Return_GetJobRemarks>)Job_Existing_Remarks_Grid_Data;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetJobRemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("Frm_ViewRemarks", ViewBag.ViewRemarks);
        }

        #endregion Remarks
        #region Manpower Usage

        public ActionResult InnerGrid_ActualManpowerUsageGrid(string ActionMethodName, int JobID = 0)
        {
            var ActualManpowerList = new List<Return_GetJobManpowerUsage>();
            if (ActionMethodName == "New")
            {
                return PartialView(ActualManpowerList);
            }
            if (Job_Actual_Manpower_Usage_Window_Grid_Data == null)
            {
                var ActualManpowerData = BASE._Jobs_Dbops.GetJobManpowerUsage(JobID);
                if (ActualManpowerData != null)
                {
                    ActualManpowerList = ActualManpowerData;
                }
                Job_Actual_Manpower_Usage_Window_Grid_Data = ActualManpowerList;
            }
            return View(Job_Actual_Manpower_Usage_Window_Grid_Data);
        }

        public ActionResult Frm_Add_Manpower_Usage(string ActionMethod, int SR = 0, int jobid = 0)
        {
            ViewData["Job_personnelNewRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["Job_personnelViewRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "View");
            JobActualManpowerUsage model = new JobActualManpowerUsage();
            model.Job_ManpowerUsage_ActionMethod = ActionMethod;
            model.jobID = jobid;
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetJobManpowerUsage>)Job_Actual_Manpower_Usage_Window_Grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetJobManpowerUsage();
                model.Job_ManpowerUsage_sr = Sr;
                model.Job_PersonName = dataToEdit.Person_Name != null ? dataToEdit.Person_Name : "";
                model.Job_W_PeriodFrom = dataToEdit.Work_Period_From;
                model.Job_W_PeriodTo = dataToEdit.Work_Period_To;
                model.Job_ManpowerUnitsID = dataToEdit.UnitID;
                model.Job_RatePerUnit = Convert.ToDouble(dataToEdit.Rate_per_Unit);
                model.Job_Units_Worked = Convert.ToDouble(dataToEdit.Units_Worked);
                model.Job_TotalCost = Convert.ToDouble(dataToEdit.Total_Cost);
                model.Job_ManpowerRemarks = dataToEdit.Remarks != null ? dataToEdit.Remarks : "";
                model.Job_ManpowerID = dataToEdit.Manpower_ID;
                model.Job_Manpower_ChargeID = Convert.ToInt32(dataToEdit.Job_Manpower_ChargeID);
                model.Job_ManpowerUsage_REC_ID = dataToEdit.REC_ID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Add_Manpower_Usage(JobActualManpowerUsage model)
        {
            var actionmethod = model.Job_ManpowerUsage_ActionMethod;
            var jobid = 0;
            if (Current_JobID == 0 || Current_JobID == null) { } else { jobid = (int)Current_JobID; }
            var data = new Return_GetRecord();
            if (jobid == 0) { }else { data = BASE._Jobs_Dbops.GetRecord((int)jobid); }
            DateTime? datenull = null;
            var JobStartDate= datenull;
            var JobRequestDate = datenull;
            if (!(data.Job_StartDate == datenull || data.Job_StartDate == DateTime.MinValue))
            {
                JobStartDate = data.Job_StartDate;
            }
            else if (!(model.JobStartDate== datenull || model.JobStartDate == DateTime.MinValue))
            {
                JobStartDate = model.JobStartDate;
            }

            if (!(data.Job_RequestDate == datenull || data.Job_RequestDate==DateTime.MinValue))
            {
                JobRequestDate = data.Job_RequestDate;
            }
            else if (!(model.JobRequestDate == datenull || model.JobRequestDate == DateTime.MinValue))
            {
                JobRequestDate = model.JobRequestDate;
            }//Mantis bug 0000541 fixed
            if (actionmethod == "Edit" || actionmethod == "New")
            {

                if (model.Job_ManpowerRemarks != null)
                {
                    model.Job_ManpowerRemarks = model.Job_ManpowerRemarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                DateTime WorkFrom_date = Convert.ToDateTime(model.Job_W_PeriodFrom);
                DateTime WorkTo_date = Convert.ToDateTime(model.Job_W_PeriodTo);                

                if (model.Joined_Date > WorkFrom_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Period From Date can not be Less than Joined Date "+ Convert.ToDateTime(model.Joined_Date).ToString("dd/MM/yyyy") + " of Person </br> Please Select Greater than this "+ Convert.ToDateTime(model.Joined_Date).ToString("dd/MM/yyyy") 
                    }, JsonRequestBehavior.AllowGet);
                }//Mantis bug 0001045 resolved
                if (model.Leave_Date < WorkFrom_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Period From Date can not be Greater than Leave Date " + Convert.ToDateTime(model.Leave_Date).ToString("dd/MM/yyyy") + " of Person </br> Please Select Less than this " + Convert.ToDateTime(model.Leave_Date).ToString("dd/MM/yyyy")
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Leave_Date <= WorkTo_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Period To Date can not be greater than Leaving Date "+Convert.ToDateTime(model.Leave_Date).ToString("dd/MM/yyyy") + " of Person...!"
                    }, JsonRequestBehavior.AllowGet);
                }//Mantis bug 0001045 resolved
                
                if (!(JobStartDate == datenull || JobStartDate==DateTime.MinValue))
                {
                    if (WorkFrom_date < JobStartDate)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Period From Date can not be Less than Job Start Date " + Convert.ToDateTime(JobStartDate).ToString("dd/MM/yyyy") + " of Person...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }//Mantis bug 0000541 fixed
                if (!(JobRequestDate==datenull || JobRequestDate == DateTime.MinValue))
                {
                    if (WorkFrom_date < JobRequestDate)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Period From Date can not be Less than Job Request Date " + Convert.ToDateTime(JobRequestDate).ToString("dd/MM/yyyy") + " of Person...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }//Mantis bug 0000541 fixed
                var CheckRates = BASE._Personnels_Dbops.GetPersonnelCharges(Convert.ToInt32(model.Job_ManpowerID), WorkFrom_date, WorkTo_date);

                if (model.Job_Units_Worked <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Actual units hours can not be Negative or Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (WorkTo_date < WorkFrom_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Work Period to Date Should Be More Than Work Period From...! "
                    }, JsonRequestBehavior.AllowGet);
                }


                if (CheckRates.Count > 1)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Not Allowed To Post Entry For Period Which Contains 2 Different Rates / Units...</br>If Rate Or Unit Of Work For A User Was Updated In Between Period Of Work, User Need To Make Separate Entries For Both!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.jobID != 0)
            {
                try
                {
                    Param_Insert_Job_ManpowerUsage inparam = new Param_Insert_Job_ManpowerUsage();
                    inparam.Job_ID = Convert.ToInt32(model.jobID);
                    inparam.Period_From = Convert.ToDateTime(model.Job_W_PeriodFrom);
                    inparam.Period_To = Convert.ToDateTime(model.Job_W_PeriodTo);
                    inparam.Person_ID = Convert.ToInt32(model.Job_ManpowerID);
                    inparam.Remarks = model.Job_ManpowerRemarks;
                    inparam.Charge_ID = model.Job_Manpower_ChargeID;//Mantis bug 0000362 fixed
                    inparam.Total_Amount = Convert.ToInt32(model.Job_TotalCost);
                    inparam.Units_Worked = Convert.ToInt32(model.Job_Units_Worked);
                    if (BASE._Jobs_Dbops.InsertJobManpowerUsage(inparam))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
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
            else
            {
                List<Return_GetJobManpowerUsage> gridRows = new List<Return_GetJobManpowerUsage>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Job_Actual_Manpower_Usage_Window_Grid_Data != null)
                {
                    gridRows = (List<Return_GetJobManpowerUsage>)Job_Actual_Manpower_Usage_Window_Grid_Data;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (actionmethod == "New")
                {
                    Return_GetJobManpowerUsage grid = new Return_GetJobManpowerUsage();

                    grid.Sr = NewSr;
                    grid.Manpower_ID = Convert.ToInt32(model.Job_ManpowerID);
                    grid.Person_Name = model.Job_PersonName;
                    grid.Units_Worked = Convert.ToDecimal(model.Job_Units_Worked);
                    grid.Rate_per_Unit = Convert.ToDecimal(model.Job_RatePerUnit);
                    grid.Total_Cost = Convert.ToDecimal(model.Job_TotalCost);
                    grid.Job_Manpower_ChargeID = model.Job_Manpower_ChargeID;//Mantis bug 0000362 fixed
                    grid.UnitID = model.Job_ManpowerUnits;
                    grid.Unit = model.Job_ManpowerUnits;
                    grid.Remarks = model.Job_ManpowerRemarks;
                    grid.Work_Period_From = Convert.ToDateTime(model.Job_W_PeriodFrom);
                    grid.Work_Period_To = Convert.ToDateTime(model.Job_W_PeriodTo);
                    grid.REC_ADD_BY = BASE._open_User_ID;
                    grid.REC_ADD_ON = DateTime.Now;
                    gridRows.Add(grid);
                }
                else if (actionmethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Job_ManpowerUsage_sr);

                    dataToEdit.Person_Name = model.Job_PersonName;
                    dataToEdit.Units_Worked = Convert.ToDecimal(model.Job_Units_Worked);
                    dataToEdit.Rate_per_Unit = Convert.ToDecimal(model.Job_RatePerUnit);
                    dataToEdit.Total_Cost = Convert.ToDecimal(model.Job_TotalCost);
                    dataToEdit.Job_Manpower_ChargeID = model.Job_Manpower_ChargeID;//Mantis bug 0000362 fixed
                    dataToEdit.Remarks = model.Job_ManpowerRemarks;
                    dataToEdit.UnitID = model.Job_ManpowerUnits;
                    dataToEdit.Unit = model.Job_ManpowerUnits;
                    dataToEdit.Work_Period_From = Convert.ToDateTime(model.Job_W_PeriodFrom);
                    dataToEdit.Work_Period_To = Convert.ToDateTime(model.Job_W_PeriodTo);
                    dataToEdit.Manpower_ID = Convert.ToInt32(model.Job_ManpowerID);
                    var editManpowerID = new ArrayList();
                    if (model.Job_ManpowerUsage_REC_ID != 0)
                    {
                        var editManpower = JobEdit_Manpower_ID as ArrayList;
                        if (editManpower != null)
                        {
                            editManpower.Add(model.Job_ManpowerUsage_REC_ID);
                            JobEdit_Manpower_ID = editManpower;
                        }
                        else
                        {
                            editManpowerID.Add(model.Job_ManpowerUsage_REC_ID);
                            JobEdit_Manpower_ID = editManpowerID;
                        }
                    }
                }
                Job_Actual_Manpower_Usage_Window_Grid_Data = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_Manpower_Usage_Window_Delete_Grid_Record(string ActionMethod, int SR, int? Manpower_Usage_Rec_Id = 0)
        {
            var allData = (List<Return_GetJobManpowerUsage>)Job_Actual_Manpower_Usage_Window_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetJobManpowerUsage();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Job_Actual_Manpower_Usage_Window_Grid_Data = allData;
            var deleteItemEstID = new ArrayList();
            if (Manpower_Usage_Rec_Id != 0)
            {
                var deleteItemEst = Delete_Actual_JobManpower_Usage_Data_Session as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(Manpower_Usage_Rec_Id);
                    Delete_Actual_JobManpower_Usage_Data_Session = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(Manpower_Usage_Rec_Id);
                    Delete_Actual_JobManpower_Usage_Data_Session = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Autocalculate_Rate_per_Unit(int PersonId, DateTime? WorkFrom = null, DateTime? WorkTo = null)
        {
            DateTime WorkFrom_date = Convert.ToDateTime(WorkFrom) == null ? DateTime.Now : Convert.ToDateTime(WorkFrom);
            DateTime WorkTo_date = Convert.ToDateTime(WorkTo) == null ? DateTime.Now : Convert.ToDateTime(WorkTo);
            var list = BASE._Personnels_Dbops.GetPersonnelCharges(PersonId, WorkFrom_date, WorkTo_date);
            if (list.Count > 0)
            {
                return Json(new
                {
                    result = true,
                    message = list
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = false,
                message = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LookUp_Get_Person_Names(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._Jobs_Dbops.GetPaidPersonnels();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        #endregion Manpower Usage
        #region ManPower Estimation

        public ActionResult InnerGrid_ManPowerEstimationGrid(string ActionMethodName, int JobID = 0)
        {
            var ManpowerEstimList = new List<Return_GetJobManpowerEstimates>();
            if (ActionMethodName == "New")
            {
                return PartialView(ManpowerEstimList);
            }
            if (Job_ManpowerEstimation_Grid_Data == null)
            {
                var ManpowerEstimationData = BASE._Jobs_Dbops.GetJobManpowerEstimates(JobID);
                if (ManpowerEstimationData != null)
                {
                    ManpowerEstimList = ManpowerEstimationData;
                }
                Job_ManpowerEstimation_Grid_Data = ManpowerEstimList;
            }
            return PartialView(Job_ManpowerEstimation_Grid_Data);
        }

        public ActionResult Frm_Add_Manpower_Estimate(string ActionMethod = null, int SR = 0)
        {
            JobManpowerEstimates model = new JobManpowerEstimates();
            model.Job_Manpower_ActionMethod = ActionMethod;
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var all_data = (List<Return_GetJobManpowerEstimates>)Job_ManpowerEstimation_Grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetJobManpowerEstimates();
                model.Job_Manpower_REC_ID = dataToEdit.REC_ID;
                model.Job_Manpower_skilltype = dataToEdit.Manpower_Type;
                model.Job_Estimated_Unit = dataToEdit.Unit;
                model.Job_Estimated_UnitID = dataToEdit.UnitID;
                model.Job_Estimated_Consumption = Convert.ToDouble(dataToEdit.Estimated_Consumption);
                model.Job_Estimated_Rate_per_Unit = Convert.ToDouble(dataToEdit.Estimated_Rate_per_Unit);
                model.Job_Est_Cost = dataToEdit != null ? dataToEdit.Est_Cost : 0;
                model.Job_ManEstRemarks = dataToEdit.Remarks;
                model.Job_Manpower_Sr = SR;
                model.Job_Manpower_skilltypeID = dataToEdit.SkillID;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Add_Manpower_Estimate(JobManpowerEstimates model)
        {
            var actionmethod = model.Job_Manpower_ActionMethod;

            if ((actionmethod == "New") || (actionmethod == "Edit"))
            {
                if (model.Job_ManEstRemarks != null)
                {
                    model.Job_ManEstRemarks = model.Job_ManEstRemarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
            }
            
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            List<Return_GetJobManpowerEstimates> gridRows = new List<Return_GetJobManpowerEstimates>();
            if (Job_ManpowerEstimation_Grid_Data != null)
            {
                gridRows = (List<Return_GetJobManpowerEstimates>)Job_ManpowerEstimation_Grid_Data;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetJobManpowerEstimates grid = new Return_GetJobManpowerEstimates();
                grid.Sr = NewSr;
                grid.Manpower_Type = model.Job_Manpower_skilltype;
                grid.Estimated_Consumption = Convert.ToDecimal(model.Job_Estimated_Consumption);
                grid.Estimated_Rate_per_Unit = Convert.ToDecimal(model.Job_Estimated_Rate_per_Unit);
                grid.Est_Cost = model.Job_Est_Cost;
                grid.Unit = model.Job_Estimated_Unit;
                grid.UnitID = model.Job_Estimated_UnitID;
                grid.REC_ADD_BY = BASE._open_User_ID;
                grid.REC_ADD_ON = DateTime.Now;
                grid.Remarks = model.Job_ManEstRemarks;
                grid.SkillID = model.Job_Manpower_skilltypeID;
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Job_Manpower_Sr);
                dataToEdit.Manpower_Type = model.Job_Manpower_skilltype;
                dataToEdit.Estimated_Consumption = Convert.ToDecimal(model.Job_Estimated_Consumption);
                dataToEdit.Estimated_Rate_per_Unit = Convert.ToDecimal(model.Job_Estimated_Rate_per_Unit);
                dataToEdit.Unit = model.Job_Estimated_Unit;
                dataToEdit.UnitID = model.Job_Estimated_UnitID;
                dataToEdit.Est_Cost = model.Job_Est_Cost;
                dataToEdit.Remarks = model.Job_ManEstRemarks;
                dataToEdit.SkillID = model.Job_Manpower_skilltypeID;
                var editManpowerID = new ArrayList();
                if (model.Job_Manpower_REC_ID != 0)
                {
                    var editManpower = JobEdit_Manpowerest_ID as ArrayList;
                    if (editManpower != null)
                    {
                        editManpower.Add(model.Job_Manpower_REC_ID);
                        JobEdit_Manpowerest_ID = editManpower;
                    }
                    else
                    {
                        editManpowerID.Add(model.Job_Manpower_REC_ID);
                        JobEdit_Manpowerest_ID = editManpowerID;
                    }
                }
            }
            Job_ManpowerEstimation_Grid_Data = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_ManpowerEstimation_Window_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Rec_Id = 0)
        {
            var allData = (List<Return_GetJobManpowerEstimates>)Job_ManpowerEstimation_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetJobManpowerEstimates();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Job_ManpowerEstimation_Grid_Data = allData;
            var deleteItemEstID = new ArrayList();
            if (Rec_Id != 0)
            {
                var deleteItemEst = Delete_Manpower_Estimation_Data_Session as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(Rec_Id);
                    Delete_Manpower_Estimation_Data_Session = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(Rec_Id);
                    Delete_Manpower_Estimation_Data_Session = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStockUnits(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        public ActionResult LookUp_Get_skill_types(DataSourceLoadOptions loadOptions)
        {
            var data = BASE._Personnels_Dbops.GetSkillTypes();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        #endregion ManPower Estimation
        #region Expenses

        public ActionResult InnerGrid_ActualExpensesIncurredGrid(string ActionMethodName, int ID = 0)//Mantis bug 0000393 fixed
        {
            var ActualExpensesList = new List<Return_GetJobExpensesIncurred>();
            if (ActionMethodName == "New")
            {
                return PartialView(ActualExpensesList);
            }
            if (Job_Actual_Expenses_Grid_Data == null)
            {
                var ActualExpensesData = BASE._Jobs_Dbops.GetJobExpensesIncurred(ID);//Mantis bug 0000393 fixed
                if (ActualExpensesData != null)
                {
                    ActualExpensesList = ActualExpensesData;
                }
                Job_Actual_Expenses_Grid_Data = ActualExpensesList;
            }
            return PartialView(Job_Actual_Expenses_Grid_Data);
        }

        public ActionResult Frm_Job_Expense_Details(int Job_ID, string CallingPage = null)
        {
            ViewData["Job_AccVouPayNewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            var expensescallingpage = CallingPage == null ? "InnerGrid" : CallingPage;
            ViewBag.ExpCallingPage = expensescallingpage;
            ViewBag.ExpJobID = Job_ID;
            var expensemappinglist = new List<Return_Get_Job_Expenses_For_Mapping>();
            var data = BASE._Jobs_Dbops.Get_Job_Expenses_For_Mapping(Job_ID);
            if (data != null)
            {
                expensemappinglist = data;
            }
            JobActuallExpensions = expensemappinglist;
            return View(JobActuallExpensions);
        }

        public ActionResult Job_Actual_Estimation_Grid()
        {
            var finalData = JobActuallExpensions as List<Return_Get_Job_Expenses_For_Mapping>;

            return PartialView(finalData);
        }

        public ActionResult FindGridKeyValue()
        {
            var griddata = Job_Actual_Expenses_Grid_Data as List<Return_GetJobExpensesIncurred>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = griddata[i].Txn_ID;//Mantis bug 0000393 fixed
                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Addingnewexpenses(string[] selectedrowarr, string callingscreen = null, int jobid = 0)
        {
            var mappinggriddata = new List<Return_Get_Job_Expenses_For_Mapping>();
            if (selectedrowarr != null)
            {
                var Final_Data = new List<Return_GetJobExpensesIncurred>();
                if (Job_Actual_Expenses_Grid_Data == null)
                {
                    Final_Data = BASE._Jobs_Dbops.GetJobExpensesIncurred(jobid);
                    Job_Actual_Expenses_Grid_Data = Final_Data;
                }//Mantis bug 0000604 fixed

                Final_Data = Job_Actual_Expenses_Grid_Data as List<Return_GetJobExpensesIncurred>;//Mantis bug 0000393 fixed

                    for (int i = 0; i < Final_Data.Count; i++)
                    {
                        if (Final_Data[i].REC_ID != 0)
                        {
                            var deleteItemEstID = new ArrayList();
                            var deleteItemEst = Delete_Expense_job as ArrayList;
                            if (deleteItemEst != null)
                            {
                                deleteItemEst.Add(Final_Data[i].REC_ID);
                                Delete_Expense_job = deleteItemEst;
                            }
                            else
                            {
                                deleteItemEstID.Add(Final_Data[i].REC_ID);
                                Delete_Expense_job = deleteItemEstID;
                            }
                        }
                    }//Mantis bug 0000393 fixed
                    if (callingscreen != null)
                    {
                        try
                        {
                            Param_Insert_Job_ExpensesIncurred inparam = new Param_Insert_Job_ExpensesIncurred();
                            mappinggriddata = JobActuallExpensions as List<Return_Get_Job_Expenses_For_Mapping>;
                            List<Return_GetJobExpensesIncurred> newrow = new List<Return_GetJobExpensesIncurred>();
                            for (int i = 0; i < selectedrowarr.Length; i++)
                            {
                                Return_Get_Job_Expenses_For_Mapping mappedrow = mappinggriddata.Find(x => x.Txn_ID == selectedrowarr[i].ToString());
                                Param_Insert_Job_ExpensesIncurred Inparam = new Param_Insert_Job_ExpensesIncurred();
                                Inparam.Job_ID = jobid;
                                Inparam.Exp_Tr_ID = mappedrow.Txn_ID;
                                Inparam.Exp_Tr_Sr_No = Convert.ToInt32(mappedrow.Txn_Sr_No);
                                BASE._Jobs_Dbops.InsertJobExpensesIncurred(Inparam);
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
                        return Json(new
                        {
                            Message = Messages.SaveSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mappinggriddata = JobActuallExpensions as List<Return_Get_Job_Expenses_For_Mapping>;
                        List<Return_GetJobExpensesIncurred> newrow = new List<Return_GetJobExpensesIncurred>();
                        for (int i = 0; i < selectedrowarr.Length; i++)
                        {
                            var NewSr = i + 1;
                            Return_Get_Job_Expenses_For_Mapping mappedrow = mappinggriddata.Find(x => x.Txn_ID == selectedrowarr[i].ToString());
                            Return_GetJobExpensesIncurred data = new Return_GetJobExpensesIncurred();
                            data._Date = mappedrow._Date;
                            data.ItemName = mappedrow.ItemName;
                            data.Head = mappedrow.Head;
                            data.Party = mappedrow.Party;
                            data.Amount = mappedrow.Amount;
                            data.Txn_ID = mappedrow.Txn_ID;//Mantis bug 0000393 fixed
                            data.Txn_Sr_No = mappedrow.Txn_Sr_No;//Mantis bug 0000393 fixed
                            data.REC_ADD_BY = BASE._open_User_ID;
                            data.REC_ADD_ON = DateTime.Now;
                            data.Sr = NewSr;
                            newrow.Add(data);
                        }
                        Job_Actual_Expenses_Grid_Data = newrow;
                        return Json(new { result = true, Message = "" }, JsonRequestBehavior.AllowGet);//Mantis bug 0000604 fixed
                }                
            }
            else
            {
                var Final_Data = Job_Actual_Expenses_Grid_Data as List<Return_GetJobExpensesIncurred>;

                for (int i = 0; i < Final_Data.Count; i++)
                {
                    if (Final_Data[i].REC_ID != 0)
                    {
                        var deleteItemEstID = new ArrayList();
                        var deleteItemEst = Delete_Expense_job as ArrayList;
                        if (deleteItemEst != null)
                        {
                            deleteItemEst.Add(Final_Data[i].REC_ID);
                            Delete_Expense_job = deleteItemEst;
                        }
                        else
                        {
                            deleteItemEstID.Add(Final_Data[i].REC_ID);
                            Delete_Expense_job = deleteItemEstID;
                        }
                    }
                }
                Job_Actual_Expenses_Grid_Data = null;
                return Json(new { result = true, Message = "" }, JsonRequestBehavior.AllowGet);//Mantis bug 0000604 fixed
            }//Mantis bug 0000393 fixed
        }

        public ActionResult IncurredDelete(int SR, int id = 0)
        {
            var Final_Data = Job_Actual_Expenses_Grid_Data as List<Return_GetJobExpensesIncurred>;
            var onlyMatch = Final_Data.Single(s => s.Sr == SR);
            Final_Data.Remove(onlyMatch);
            Job_Actual_Expenses_Grid_Data = Final_Data;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_Expense_job as ArrayList;//Mantis bug 0000393 fixed
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_Expense_job = deleteItemEst;//Mantis bug 0000393 fixed
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_Expense_job = deleteItemEstID;//Mantis bug 0000393 fixed
                }
            }
            return Json(new { result = true, message = "Deleted Successfully", }, JsonRequestBehavior.AllowGet);
        }

        #endregion Expenses
        #endregion Inside Grids
        #region Job Request register Bottom Buttons

        [HttpGet]
        public ActionResult Job_StatusChange_Window(int JobId, string StatusButton)
        {
            JobUpDateStatus model = new JobUpDateStatus();
            model.JobID = JobId;
            model.Job_StatusType = StatusButton;
            model.Job_Completion_Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Job_StatusChange_Window(JobUpDateStatus model)
        {
            try
            {
                string msg = "";
                Param_Update_Job_Status param = new Param_Update_Job_Status();
                param.JobID = model.JobID;
                if (model.Job_StatusType == "Complete")
                {
                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Completed", true);
                    param.JobCompletionDate = model.Job_Completion_Date;
                    msg = "Status Changed To Completed..!!";
                    if (BASE._Jobs_Dbops.UpdateJobStatus(param))
                    {
                        return Json(new
                        {
                            result = true,
                            message = msg
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.Job_StatusType == "Cancel")
                {
                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Cancelled", true);
                    msg = "Status Changed To Cancelled..!!";
                }
                else if (model.Job_StatusType == "Reject")
                {
                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Rejected", true);
                    msg = "Status Changed To Rejected..!!";
                }
                else
                {
                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Changes_Recommended", true);
                    msg = "Status Changed To Changes Recommended...!!";
                }
                if (BASE._Jobs_Dbops.UpdateJobStatus(param))
                {
                    Param_InsertJobRemarks inparam = new Param_InsertJobRemarks();
                    inparam.JobID = model.JobID;
                    inparam.Remarks = model.Job_Status_Remark;
                    if (BASE._Jobs_Dbops.InsertJobRemarks(inparam))
                    {
                        return Json(new
                        {
                            result = true,
                            message = msg
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Job_StatusChange_Check(int JobId, string StatusButton)
        {
            if (StatusButton != "Changes Recommended")
            {
                string msg = "";
                var uocount = BASE._Jobs_Dbops.GetJob_UO_Count(JobId);
                var rrcount = BASE._Jobs_Dbops.GetJob_RR_Count(JobId);
                if (StatusButton == "Complete")
                {
                    var uopendingcount = BASE._Jobs_Dbops.GetJob_UO_Pending_Count(JobId);
                    var rrpendingcount = BASE._Jobs_Dbops.GetJob_RR_Pending_Count(JobId);
                    var totalusagecount = BASE._Jobs_Dbops.GetJob_Total_Usage_Count(JobId);
                    var data = BASE._Jobs_Dbops.GetRecord(JobId);
                    int AssignMainDeptID = data.Job_AssigneeMainDeptID;
                    string AssignSubDeptID = data.Job_AssigneeSubDeptID;
                    if (AssignMainDeptID != BASE._open_User_MainDeptID && AssignSubDeptID.Contains(BASE._open_User_SubDeptID.ToString()))//Mantis bug 0000374 fixed
                    {
                        msg = "Not Allowed";
                    }
                    if (uopendingcount > 0)
                    {
                        msg = "A Job Cannot Be Marked As Completed, If  UO Against It Are In Pending Status..!!";
                    }
                    else if (rrpendingcount > 0)
                    {
                        msg = "A Job Cannot Be Marked As Completed, If  RR Against It Are In Pending Status..!!";
                    }
                    else if (uocount == 0)
                    {
                        msg = "A Job Cannot Be Marked As Completed, If No UO Is Posted Against It..!!";
                    }
                    else if (totalusagecount == 0)
                    {
                        msg = "A Job Cannot Be Marked As Completed, If No Machine Usage/Manpower Usage/Expenses Is Posted Against It...!!";
                    }
                }
                else if (uocount > 0)
                {
                    msg = "A Job Cannot Be " + StatusButton + "ed " + "If UO Is Posted Against It..!!";
                }
                else if (rrcount > 0)
                {
                    msg = "A Job Cannot Be " + StatusButton + "ed " + "If RR Is Posted Against It..!!";
                }
                if (msg != "")
                {
                    return Json(new
                    {
                        result = false,
                        message = msg
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = true,
                        message = msg
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Job_StatusChange(int JobId, string StatusButton, string[] userRole)
        {
            try
            {
                var data = BASE._Jobs_Dbops.GetRecord(JobId);
                var JobStatus = data.Job_Status;
                int AssignMainDeptID = data.Job_AssigneeMainDeptID;
                string AssignSubDeptID = data.Job_AssigneeSubDeptID==null?"": data.Job_AssigneeSubDeptID;//Mantis bug 0000374 fixed//Mantis bug 0000477 fixed
                int jobrequestorid = data.Job_RequestorId;
                string msg = "";
                Param_Update_Job_Status param = new Param_Update_Job_Status();
                param.JobID = JobId;
                if (StatusButton == "Estimation_Submission")
                {
                    param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Assigned_for_Estimation_Creation", true);
                    msg = "Status Changed To 'Assigned For Estimation Creation'..!!";
                }
                else if (StatusButton == "Reopen_Job")
                {
                    if (AssignMainDeptID == BASE._open_User_MainDeptID)
                    {
                        if (JobStatus == "Completed")
                        {
                            param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Approved", true);
                            msg = "Status Changed To 'Approved'..!!";
                        }
                    }
                    if (AssignSubDeptID.Contains(BASE._open_User_SubDeptID.ToString()))//Mantis bug 0000374 fixed
                    {
                        if (JobStatus == "Completed")//Mantis bug 0000599 fixed
                        {
                            param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "In_Progress", true);
                            msg = "Status Changed To 'In-Progress'..!!";
                        }
                    }
                    if (BASE._open_User_PersonnelID == jobrequestorid || userRole.Contains("Requestor Main Dept In-Charge"))//Mantis bug 0000422 fixed
                    {
                        param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "_New", true);
                        msg = "Status Changed To 'New'..!!";
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Not Allowed!!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (StatusButton == "Estimate_Approval")
                {
                    if (AssignSubDeptID.Contains(BASE._open_User_SubDeptID.ToString()))//Mantis bug 0000374 fixed
                    {
                        param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Submitted_for_Estimate_Approval", true);
                        msg = "Status Changed To 'Submitted for Estimate Approval'..!!";
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Not Allowed!!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (StatusButton == "Approve")
                {
                    if (userRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "New" || JobStatus == "Rejected" || JobStatus == "Changes Recommended")
                        {
                            param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Requested", true);
                            msg = "Status Changed To 'Requested'..!!";
                        }//Mantis bug 0000706 fixed
                    }
                    if (userRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Rejected" || JobStatus == "Changes Recommended" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval")
                        {
                            param.UpdatedStatus = (Job_Status)Enum.Parse(typeof(Job_Status), "Approved", true);
                            msg = "Status Changed To 'Approved'..!!";
                        }//Mantis bug 0000706 fixed
                    }
                }
                if (BASE._Jobs_Dbops.UpdateJobStatus(param))
                {
                    return Json(new
                    {
                        result = true,
                        message = msg
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
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

        public ActionResult JobRequestStatusFlow(int JobId, string StatusButton, string[] CurrUserRole)
        {
            Current_JobID = JobId;//Mantis bug 0000541 fixed
            var data = BASE._Jobs_Dbops.GetRecord(JobId);
            var JobStatus = data.Job_Status;
            var JobAssigneeMainDeptID = data.Job_AssigneeMainDeptID;
            var JobRequestorMainDeptID = data.Job_RequestorMainDeptId;
            var JobProjectID = data.Job_ProjectId;
            string msg = "";
            if (StatusButton == "Documents")
            {
                if (JobStatus == "Completed")
                {
                    msg = "You Are Not Allowed To Post Documents If Job Status IS " + JobStatus + " ..!!";
                }
                else
                {
                    return Json(new
                    {
                        result = true,
                        message = ""
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else if (StatusButton == "Complete")
            {
                if (BASE._open_User_MainDeptID == JobAssigneeMainDeptID || BASE._open_User_MainDeptID == JobRequestorMainDeptID)
                {
                    if (JobStatus == "Approved" || JobStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Users From Assignee Dept Or Requestor Dept Are Not Allowed To Mark Job As Completed If Job Status IS " + JobStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only Users From Assignee Dept Or Requestor Dept Can Mark Job As Completed..!!";
                }
            }
            else if (StatusButton == "Post Manpower Hour" || StatusButton == "Generate Job Card" || StatusButton == "Generate Job Cost Compare Report")
            {
                if (BASE._open_User_MainDeptID == JobAssigneeMainDeptID || BASE._open_User_MainDeptID == JobRequestorMainDeptID)
                {
                    return Json(new
                    {
                        result = true,
                        message = ""
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Only Users From Assignee Dept Or Requestor Dept Can " + StatusButton + " ..!!";
                }
            }
            else if (StatusButton == "Post UO")
            {
                if (JobStatus == "Approved" || JobStatus == "In-Progress")
                {
                    return Json(new
                    {
                        result = true,
                        message = ""
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "You Are Not Allowed To Post UO If Job Status IS " + JobStatus + " ..!!";
                }
            }
            else if (CurrUserRole.Length > 0)
            {
                if (StatusButton == "Cancel")
                {
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        if (JobStatus == "New" || JobStatus == "Changes Recommended" || JobStatus == "Requested" || JobStatus == "Approved")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Is Not Allowed To Cancel Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Requestor' Can Cancel The Job...!!";
                    }
                }
                else if (StatusButton == "Reopen_Job")
                {
                    if (JobProjectID > 0)
                    {
                        var projdata = BASE._Projects_Dbops.GetRecord((int)JobProjectID);
                        var projstatus = projdata[0].Proj_Status.ToString();
                        if (projstatus == "Completed")
                        {
                            return Json(new
                            {
                                result = false,
                                message = "Job Whose Project Has Been Completed Cannot be Reopened...!!"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "Cancelled")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Is Not Allowed To Re-open Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "Rejected" || JobStatus == "Cancelled")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Main Dept In-Charge Is Not Allowed To Re-open Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }//Mantis bug 0000422 fixed
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge") || CurrUserRole.Contains("Assignee Sub-Dept Main Incharge"))
                    {
                        if (JobStatus == "Completed")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Or Assignee Sub-Dept Incharge Cannot Re-open Job If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    else if( (! CurrUserRole.Contains("Requestor"))&&(!CurrUserRole.Contains("Requestor Main Dept In-Charge")))
                    {
                        msg = "Only 'Requestor','Requestor Main Dept In-Charge','Assignee Main Dept Incharge','Assignee Sub-Dept Incharge' Can Re-open The Job...!!";
                    }//Mantis bug 0000422 fixed
                }
                else if (StatusButton == "Reject")
                {
                    if (CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "New" || JobStatus == "Requested")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Main Dept Incharge Cannot Reject Job If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "Approved" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Cannot Reject Job If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    else if(!CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        msg = "Only 'Requestor Main Dept Incharge','Assignee Main Dept Incharge' Can Reject The Job...!!";
                    }
                }
                else if (StatusButton == "Changes Recommended")
                {
                    if (CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "New" || JobStatus == "Rejected" || JobStatus == "Requested")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Main Dept Incharge Cannot Recommend Changes If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Approved" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Cannot Recommend Changes If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    else if(! CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        msg = "Only 'Requestor Main Dept Incharge','Assignee Main Dept Incharge' Can Recommend Changes In The Job...!!";
                    }
                }
                else if (StatusButton == "Approve")
                {
                    if (CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "New" || JobStatus == "Rejected" || JobStatus == "Changes Recommended")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Main Dept Incharge Cannot Approve The Job If Status Is " + JobStatus + "...!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Rejected" || JobStatus == "Changes Recommended" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Cannot Approve The Job If Job Status Is " + JobStatus + "...!!";
                        }
                    }
                    else if(! CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        msg = "Only 'Requestor Main Dept Incharge','Assignee Main Dept Incharge' Can Approve The Job...!!";
                    }
                }
                else if (StatusButton == "Estimation_Submission")
                {
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "Requested" || JobStatus == "Rejected" || JobStatus == "Approved" || JobStatus == "Submitted for Estimate Approval")//Mantis bug 0000475 fixed
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Is Not Allowed To Assign For Estimate Creation If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Assignee Main Dept Incharge' Can Assign For Estimate Creation..!!";
                    }
                }
                else if (StatusButton == "Estimate_Approval")
                {
                    if (CurrUserRole.Contains("Assignee Sub-Dept Main Incharge"))
                    {
                        if (JobStatus == "Assigned for Estimation Creation")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Sub-Dept Incharge Is Not Allowed To Submit For Estimate Approval If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Assignee Sub-Dept Incharge' Can Submit For Estimate Approval..!!";
                    }
                }
                else if (StatusButton == "Edit")
                {
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "New")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Is Not Allowed To Edit Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Requestor Main Dept In-Charge"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "New" || JobStatus == "Requested")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Requestor Main Dept In-Charge Is Not Allowed To Edit Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Changes Recommended" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval" || JobStatus == "Approved")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Dept Main Incharge Is Not Allowed To Edit Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Sub-Dept Main Incharge"))
                    {
                        if (JobStatus == "Assigned for Estimation Creation")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Sub-Dept Main Incharge Is Not Allowed To Edit Job If Job Status IS " + JobStatus + " ..!!";
                        }
                    }//Mantis bug 0000385 fixed
                    else if( (! CurrUserRole.Contains("Requestor"))&&(! CurrUserRole.Contains("Requestor Main Dept In-Charge"))&&(!CurrUserRole.Contains("Assignee Dept Main Incharge")))
                    {
                        msg = "Only 'Requestor','Requestor Main Dept In-Charge','Assignee Dept Main Incharge','Assignee Sub-Dept Main Incharge' Can Edit This Job..!!";
                    }//Mantis bug 0000385 fixed
                }
                else if (StatusButton == "Delete")
                {
                    if (JobStatus == "New")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Are Not Allowed To Delete If Job Status IS " + JobStatus + " ..!!";
                    }
                }
                else if (StatusButton == "Manpower Estimation" || StatusButton == "Item Usage Estimation")
                {
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (JobStatus == "Approved" || JobStatus == "Requested" || JobStatus == "Assigned for Estimation Creation" || JobStatus == "Submitted for Estimate Approval" || JobStatus == "In-Progress")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Dept Main Incharge Cannot Add/Edit/Delete " + StatusButton + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Sub-Dept Main Incharge"))
                    {
                        if (JobStatus == "Assigned for Estimation Creation")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee  Sub Dept  Incharge Cannot Add/edit/Delete " + StatusButton + " ..!!";
                        }
                    }
                    else if(! CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        msg = "Only 'Assignee Main Dept Incharge','Assignee Sub Dept Incharge' Have Rights To Add/edit/Delete " + StatusButton + " ..!!";
                    }
                }
                else
                {
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge") || CurrUserRole.Contains("Assignee Sub-Dept Main Incharge"))
                    {
                        if (JobStatus == "Approved" || JobStatus == "In-Progress")
                        {
                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "Assignee Main Dept Incharge Or Assignee Sub Dept Incharge Is Not Allowed To Add " + StatusButton + " If Job Status IS " + JobStatus + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Assignee Main Dept Incharge','Assignee Sub Dept Incharge' Have Rights To Add " + StatusButton + " To The Job...!!";
                    }
                }
            }
            else
            {
                msg = "Not Alowed..!!";
            }
            if (msg != "")
            {
                return Json(new
                {
                    result = false,
                    message = msg
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Job Request register Bottom Buttons
        #region"MISC"
        public void Sessionclear()
        {
            Session.Remove("JobDocument");
            ClearBaseSession("_JobRR");
            BASE._SessionDictionary.Remove("JobDocument_HelpAttachment");
            BASE._SessionDictionary.Remove("Job_Documents_Attachment_AttachmentData");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_JobInfo");
        }
        
        public void Job_NEVD_Rights()
        {
            ViewData["Job_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Add");
            ViewData["Job_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Update");
            ViewData["Job_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "View");
            ViewData["Job_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Delete");
            ViewData["Job_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Export");
            ViewData["Job_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Report");
            ViewData["Job_personnelNewRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["Job_storemasterNewRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Add");
            ViewData["Job_ProjectScreenNewright"] = CheckRights(BASE, ClientScreen.Stock_Project, "Add");
            ViewData["Job_personnelViewRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "View");
            ViewData["Job_AccVouPayNewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
        }
        #endregion
    }
}