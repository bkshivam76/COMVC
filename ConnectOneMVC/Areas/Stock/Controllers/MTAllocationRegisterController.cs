using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using DevExpress.Web.Mvc;
using Newtonsoft.Json;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Areas.Stock.Models;
using static Common_Lib.DbOperations.StockMachineToolAllocation;
using static Common_Lib.DbOperations.StockDeptStores;
using static Common_Lib.DbOperations.Jobs;
using static Common_Lib.DbOperations.StockProfile;
using Common_Lib.RealTimeService;
using Common_Lib;
using System.Data;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    public class MTAllocationRegisterController : BaseController
    {
        // GET: Stock/MTAllocationRegister
        #region Global Variables
        public List<Return_GetRegister_MainGrid> MTAllocationRegister_Data_Glob
        {
            get
            {
                return (List<Return_GetRegister_MainGrid>)GetBaseSession("MTAllocationRegister_Data_Glob_MTARInfo");
            }
            set
            {
                SetBaseSession("MTAllocationRegister_Data_Glob_MTARInfo", value);
            }
        }
        public List<Return_GetRegister_NestedGrid> MT_IssueDetail_Data
        {
            get
            {
                return (List<Return_GetRegister_NestedGrid>)GetBaseSession("MT_IssueDetail_Data_MTARInfo");
            }
            set
            {
                SetBaseSession("MT_IssueDetail_Data_MTARInfo", value);
            }
        }
        public List<Return_GetRegister_SubNestedGrid> MT_ReturnDetail_Data
        {
            get
            {
                return (List<Return_GetRegister_SubNestedGrid>)GetBaseSession("MT_ReturnDetail_Data_MTARInfo");
            }
            set
            {
                SetBaseSession("MT_ReturnDetail_Data_MTARInfo", value);
            }
        }
        public DateTime? MTAllocationFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("MTAllocationFromDate_MTARInfo");
            }
            set
            {
                SetBaseSession("MTAllocationFromDate_MTARInfo", value);
            }
        }
        public DateTime? MTAllocationToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("MTAllocationToDate_MTARInfo");
            }
            set
            {
                SetBaseSession("MTAllocationToDate_MTARInfo", value);
            }
        }
        public int MTMainGridID
        {
            get
            {
                return (int)GetBaseSession("MTMainGridID_MTAR");
            }
            set
            {
                SetBaseSession("MTMainGridID_MTAR", value);
            }
        }
        public int MT_ShowHorizontalBar
        {
            get
            {
                return (int)GetBaseSession("MT_ShowHorizontalBar_MTAR");
            }
            set
            {
                SetBaseSession("MT_ShowHorizontalBar_MTAR", value);
            }
        }
        public int MTNestedGridID
        {
            get
            {
                return (int)GetBaseSession("MTNestedGridID_MTAR");
            }
            set
            {
                SetBaseSession("MTNestedGridID_MTAR", value);
            }
        }
        public List<MT_Issued_Tools> MT_Issued_List
        {
            get
            {
                return (List<MT_Issued_Tools>)GetBaseSession("MT_Issued_List_MTAR");
            }
            set
            {
                SetBaseSession("MT_Issued_List_MTAR", value);
            }
        }
        public List<Return_GetMachToolIssueRemarks> Existing_Remarks_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetMachToolIssueRemarks>)GetBaseSession("Existing_Remarks_Window_Grid_Data_MTAR");
            }
            set
            {
                SetBaseSession("Existing_Remarks_Window_Grid_Data_MTAR", value);
            }
        }
        public List<int> Edit_IssueID
        {
            get
            {
                return (List<int>)GetBaseSession("Edit_IssueID_MTAR");
            }
            set
            {
                SetBaseSession("Edit_IssueID_MTAR", value);
            }
        }
        public List<int> Delete_Issue_ID
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_Issue_ID_MTAR");
            }
            set
            {
                SetBaseSession("Delete_Issue_ID_MTAR", value);
            }
        }
        public List<int> Delete_ExistingRemarks
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_ExistingRemarks_MTAR");
            }
            set
            {
                SetBaseSession("Delete_ExistingRemarks_MTAR", value);
            }
        }
        public List<Return_GetMachToolReturnRemarks> Return_Remarks_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetMachToolReturnRemarks>)GetBaseSession("Return_Remarks_Window_Grid_Data_MTAR");
            }
            set
            {
                SetBaseSession("Return_Remarks_Window_Grid_Data_MTAR", value);
            }
        }
        public List<Return_Get_Stocks_Listing> MachineToolName_List
        {
            get
            {
                return (List<Return_Get_Stocks_Listing>)GetBaseSession("MachineToolName_List_MTAR");
            }
            set
            {
                SetBaseSession("MachineToolName_List_MTAR", value);
            }
        }
        public List<Return_get_store_Pending_Returns> MachineToolName_Return_List
        {
            get
            {
                return (List<Return_get_store_Pending_Returns>)GetBaseSession("MachineToolName_Return_List_MTAR");
            }
            set
            {
                SetBaseSession("MachineToolName_Return_List_MTAR", value);
            }
        }
        public List<int> Delete_ReturnRemarks_MTAR
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_ReturnRemarks_MTAR");
            }
            set
            {
                SetBaseSession("Delete_ReturnRemarks_MTAR", value);
            }
        }
        public List<FullReturnGrid> MTAllocation_FullReturnGridData
        {
            get
            {
                return (List<FullReturnGrid>)GetBaseSession("MTAllocation_FullReturnGridData_MTAR");
            }
            set
            {
                SetBaseSession("Delete_ReturnRemarks_MTAR", value);
            }
        }
        public string MT_ActionMethod
        {
            get
            {
                return (string)GetBaseSession("MT_ActionMethod_MTAR");
            }
            set
            {
                SetBaseSession("MT_ActionMethod_MTAR", value);
            }
        }
        
        #endregion
        public ActionResult Frm_MT_AllocationRegister_Info()
        {
            var ctrl = new Start.Controllers.HomeController();
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_Machine_Tool_Issue, "List"))//Code written for User Authorization do not remove
            {
                string PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;

                ViewData["MTAExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["MTAExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["MTAExportGridHeaderRight"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                MT_user_rights();
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Machine_Tool_Issue').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        #region Grid
        public ActionResult Frm_MT_AllocationRegister_Grid(string command, int ShowHorizontalBar = 0)
        {
            MT_user_rights();
            ViewData["MTAExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["MTAExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["MTAExportGridHeaderRight"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            MT_ShowHorizontalBar = ShowHorizontalBar;
            ViewData["MT_ShowHorizontalBar"] = MT_ShowHorizontalBar;
            if (MTAllocationRegister_Data_Glob == null || command == "REFRESH")
            {
                var MTAllocationRegister_Data = BASE._Stock_MachineTools_DBOps.GetRegister(Convert.ToDateTime(MTAllocationFromDate), Convert.ToDateTime(MTAllocationToDate));
                if (MTAllocationRegister_Data != null)
                {
                    var Mastergrid = MTAllocationRegister_Data.main_Register;
                    var Nestedgrid = MTAllocationRegister_Data.nested_Register;
                    var SubnestedGrid = MTAllocationRegister_Data.sub_nested_Register;
                    if (Mastergrid.Count > 0)
                    {
                        Mastergrid = Mastergrid.OrderBy(o => o.ID).ToList();
                        for (int i = 0; i < Mastergrid.Count; i++)
                        {
                            Mastergrid[i].Sr = i + 1;
                        }
                    }
                    MTAllocationRegister_Data_Glob = Mastergrid;
                    MT_IssueDetail_Data = Nestedgrid;
                    Session["MT_IssueDetail_Data"] = MT_IssueDetail_Data;
                    MT_ReturnDetail_Data = SubnestedGrid;
                    Session["MT_ReturnDetail_Data"] = MT_ReturnDetail_Data;
                }
            }
            List<Return_GetRegister_MainGrid> Mastergrid_data = MTAllocationRegister_Data_Glob as List<Return_GetRegister_MainGrid>;
          
            if (Mastergrid_data == null)
            {
                return PartialView();
            }

            return PartialView(Mastergrid_data);
        }
        public PartialViewResult Frm_MTAllocationRegister_IssueDetail_Grid(int MT_ID, string Command)
        {
            ViewData["MT_ShowHorizontalBar"] = MT_ShowHorizontalBar;
            MTMainGridID = MT_ID;
            ViewData["MTMainGridID"] = MTMainGridID;
            if (MT_IssueDetail_Data == null || Command == "REFRESH")
            {
                var MTAllocationRegister_Data = BASE._Stock_MachineTools_DBOps.GetRegister(Convert.ToDateTime(MTAllocationFromDate), Convert.ToDateTime(MTAllocationToDate));
                var MTItemProduce_Data = MTAllocationRegister_Data.nested_Register;
                MT_IssueDetail_Data = MTItemProduce_Data;
            }
            var data = MT_IssueDetail_Data as List<Return_GetRegister_NestedGrid>;
            List<Return_GetRegister_NestedGrid> mtitemproduce = data.FindAll(x => x.IssueMainRecID == MT_ID);
            return PartialView(mtitemproduce);
        }
        public PartialViewResult Frm_MTAllocationRegister_ReturnDetail_Grid(int ITEM_ID, string Command)
        {
            ViewData["MT_ShowHorizontalBar"] = MT_ShowHorizontalBar;
            ViewData["MTMainGridID"] = MTMainGridID;
            MTNestedGridID = ITEM_ID;
            ViewData["MTNestedGridID"] = MTNestedGridID;
            if (MT_ReturnDetail_Data == null || Command == "REFRESH")
            {
                var MTAllocationRegister_Data = BASE._Stock_MachineTools_DBOps.GetRegister(Convert.ToDateTime(MTAllocationFromDate), Convert.ToDateTime(MTAllocationToDate));
                var MTSubItemProduce_Data = MTAllocationRegister_Data.sub_nested_Register;
                MT_ReturnDetail_Data = MTSubItemProduce_Data;
            }
            var subdata = MT_ReturnDetail_Data as List<Return_GetRegister_SubNestedGrid>;
            List<Return_GetRegister_SubNestedGrid> mtsubitemproduce = subdata.FindAll(x => x.IssueItemRecID == ITEM_ID);

            return PartialView(mtsubitemproduce);
        }
        public static GridViewSettings MTAllocationRegister_IssueDetail_Export(int MT_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MTAllocationRegister" + MT_ID;
            settings.SettingsDetail.MasterGridName = "MTAllocationRegisterListGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Mach_Tool_Name").Visible = true;
            settings.Columns.Add("Mach_Tool_Code").Visible = true;
            settings.Columns.Add("Qty_Allocated").Visible = true;
            settings.Columns.Add("Qty_Returned").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("MachineToolID").Visible = false;
            settings.Columns.Add("IssueMainRecID").Visible = false;
            settings.Columns.Add("IssueDate").Visible = false;
            settings.Columns.Add("IssuingStore").Visible = false;
            return settings;
        }//settings for exporting nested grid 
        public static IEnumerable GetIssueDetail(int MT_ID)
        {
            List<Return_GetRegister_NestedGrid> data = (List<Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["MT_IssueDetail_Data"];
            List<Return_GetRegister_NestedGrid> itemproducelist = data.FindAll(x => x.IssueMainRecID == MT_ID);
            return itemproducelist;
        }//binding data to nested grid
        public static GridViewSettings MTAllocationRegister_ReturnDetail_Export(int ITEM_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "MTAllocationRegister" + ITEM_ID;
            settings.SettingsDetail.MasterGridName = "MTAllocationRegisterItemProduce";
            settings.KeyFieldName = "IssueMainRecID";
            settings.Columns.Add("Return_Qty").Visible = true;
            settings.Columns.Add("Return_Date").Visible = true;
            settings.Columns.Add("Penalty").Visible = true;
            settings.Columns.Add("Remarks").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("IssueItemRecID").Visible = false;
            settings.Columns.Add("MachineToolID").Visible = false;
            return settings;
        }//settings for exporting sub nested grid     
        public static IEnumerable GetReturnDetail(int ITEM_ID)
        {
            List<Return_GetRegister_SubNestedGrid> data = (List<Return_GetRegister_SubNestedGrid>)System.Web.HttpContext.Current.Session["MT_ReturnDetail_Data"];
            List<Return_GetRegister_SubNestedGrid> subitemproducelist = data.FindAll(x => x.IssueItemRecID == ITEM_ID);
            return subitemproducelist;
        }//binding data to sub nested grid     
        public ActionResult MTAllocationRegisterCustomDataAction(int key = 0)
        {
            var Data = MTAllocationRegister_Data_Glob as List<Return_GetRegister_MainGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.EditedBy + "![" + it.EditedOn + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.Issue_Date + "![" + it.CurrUserRole;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        #endregion
        #region Period Selection
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
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
        public ActionResult LookUp_Get_ViewType_List_MTAllocation(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var bankdata = new List<SelectListItem>();
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
        public ActionResult LookUp_ViewType_ChangeEvent_MTAllocation(string Chaval)
        {
            MTAllocationRegister_Period model = GetPeriod(Chaval);
            MTAllocationFromDate = model.MTAllocation_Fromdate;
            MTAllocationToDate = model.MTAllocation_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);

        }
        public MTAllocationRegister_Period GetPeriod(string Chaval)
        {
            MTAllocationRegister_Period model = new MTAllocationRegister_Period();
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
            model.MTAllocation_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.MTAllocation_Fromdate = xFr_Date;
            model.MTAllocation_Todate = xTo_Date;
            return model;
        }
        public ActionResult Frm_Change_Period_Screen_MTAllocation()
        {
            MTAllocationRegister_Period model = new MTAllocationRegister_Period();
            model.MTAllocation_PeriodSelection = "Specific Period";
            model.MTAllocation_Todate = (DateTime)MTAllocationToDate;
            model.MTAllocation_Fromdate = (DateTime)MTAllocationFromDate;
            model.MTAllocation_BE_View_Period = "";
            model.MTAllocation_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.MTAllocation_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_MTAllocation(MTAllocationRegister_Period model)
        {
            if (model.MTAllocation_Todate < model.MTAllocation_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MTAllocationToDate = model.MTAllocation_Todate;
                MTAllocationFromDate = model.MTAllocation_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Issue
        [HttpGet]
        public ActionResult Frm_NEVD_MTAllocation(string ActionMethod,string IssueDate,int?IssuedBy,int?IssuedTo,int?JobID,int?UsageSiteID,int? StoreID, int ID = 0)
        {
            var j = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (j = 0; j < Rights.Length; j++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, Rights[j]) && ActionMethod == AM[j])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            
            MT_user_rights();
            Model_NEVD_MTAllocation model = new Model_NEVD_MTAllocation();
            model.ActionMethod = ActionMethod;
            model.LoginUserID = BASE._open_User_PersonnelID;
            MT_Issued_List = new List<MT_Issued_Tools>();
            Existing_Remarks_Window_Grid_Data = new List<Return_GetMachToolIssueRemarks>();
            MT_ActionMethod = ActionMethod;
            


            if (IssueDate != null)
            {
                model.MT_IssueDate = Convert.ToDateTime(IssueDate);
                model.MT_IssuedBy = IssuedBy;
                model.MT_IssuedTo = IssuedTo;
                model.MT_UsageSiteID = UsageSiteID;
                model.MT_JobID = JobID;
                model.MT_IssuingStoreID = StoreID;
            }
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var selectedrowdata = BASE._Stock_MachineTools_DBOps.GetRecord(ID);
                if (selectedrowdata != null)
                {
                    model.MT_ID = selectedrowdata.ID;
                    model.MT_IssueDate = selectedrowdata.Issue_Date;
                    model.MT_IssuedBy = selectedrowdata.Issued_By_ID;
                    model.MT_IssuedTo = selectedrowdata.Issued_To_ID;
                    model.MT_IssuingStoreID = selectedrowdata.Issuing_Store_ID;
                    model.MT_UsageSiteID = selectedrowdata.Usage_Site_ID;
                    model.MT_JobID = selectedrowdata.Job_ID;
                    if (selectedrowdata.IssuedItemGridData.Count > 0)
                    {
                        var IssueData = new List<MT_Issued_Tools>();
                        for (int i = 0; i < selectedrowdata.IssuedItemGridData.Count; i++)
                        {
                            MT_Issued_Tools row = new MT_Issued_Tools();
                            row.ID = selectedrowdata.IssuedItemGridData[i].ID;
                            row.Machine_Tool_Name = selectedrowdata.IssuedItemGridData[i].Machine_Tool_Name;
                            row.Qty_Issued = selectedrowdata.IssuedItemGridData[i].Qty_Issued;
                            row.ToolStockID = selectedrowdata.IssuedItemGridData[i].ToolStockID;
                            row.Sr = i + 1;
                            IssueData.Add(row);
                        }
                        MT_Issued_List = IssueData;
                    }

                    var remarksData = BASE._Stock_MachineTools_DBOps.GetMachToolIssueRemarks(model.MT_ID);
                    Existing_Remarks_Window_Grid_Data = remarksData;
                }
            }

            model.Existing_RemarksGrid_data = Existing_Remarks_Window_Grid_Data;
            model.AllocatedTools_Grid_data = MT_Issued_List;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_NEVD_MTAllocation(Model_NEVD_MTAllocation model)
        {
            try
            {
                var actionmethod = model.ActionMethod;

                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    if (model.MT_IssueDate < BASE._open_Year_Sdt || model.MT_IssueDate > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            message = "Machine/Tool allocation Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                    if (actionmethod == "New" || actionmethod == "Edit")
                    {
                        if (BASE._open_User_Type == "CLIENT ROLE")
                        {
                            if (AuditedPeriod != null)
                            {
                                if (AuditedPeriod.Rows.Count > 0)
                                {
                                    if (model.MT_IssueDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.MT_IssueDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
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
                                    if (model.MT_IssueDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.MT_IssueDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
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
                        if (model.MT_IssueDate < BASE._open_Year_Sdt || model.MT_IssueDate > BASE._open_Year_Edt)
                        {
                            return Json(new
                            {
                                message = "Issue Date Should Be Within Open Financial Year ...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (MT_Issued_List == null || ((List<MT_Issued_Tools>)MT_Issued_List).Count < 1)
                        {
                            return Json(new
                            {
                                message = "Atleast One Machine/Tool Must Be Issued ...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                var IssuedList = (List<MT_Issued_Tools>)MT_Issued_List;
                if (actionmethod == "New")
                {
                    Param_Insert_MachineTool_Issue InParam = new Param_Insert_MachineTool_Issue();
                    InParam.Issued_by_ID = Convert.ToInt32(model.MT_IssuedBy);
                    InParam.Issued_to_ID = Convert.ToInt32(model.MT_IssuedTo);
                    InParam.Issue_Date = Convert.ToDateTime(model.MT_IssueDate) == null ? DateTime.Now : Convert.ToDateTime(model.MT_IssueDate);
                    InParam.Issuing_Store_ID = Convert.ToInt32(model.MT_IssuingStoreID);
                    InParam.Usage_Site_ID = Convert.ToInt32(model.MT_UsageSiteID);
                    InParam.Remarks = model.MT_Remarks ?? ""; //1137 bug fixed
                    InParam.Job_ID =model.MT_JobID;
                    if (IssuedList != null && IssuedList.Count > 0)
                    {
                        var InsertTool = new List<Param_Insert_MachineTool_Item>();
                        foreach (var item in IssuedList)
                        {
                            var insertItem = new Param_Insert_MachineTool_Item();
                            insertItem.Qty = item.Qty_Issued;
                            insertItem.Stock_ID = item.ToolStockID;
                            InsertTool.Add(insertItem);
                        }
                        InParam._Items_Issued = InsertTool.ToArray();
                    }
                    if (BASE._Stock_MachineTools_DBOps.InsertMachineToolIssue(InParam))
                    {
                        return Json(new
                        {
                            result = true,
                            message = Messages.SaveSuccess,
                            issuedate = model.MT_IssueDate.ToString(),
                            issuedby=model.MT_IssuedBy,
                            issuedto=model.MT_IssuedTo,
                            jobid=model.MT_JobID,
                            usagesiteid=model.MT_UsageSiteID,
                            storeid=model.MT_IssuingStoreID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Edit")
                {
                    Param_Update_MachineTool_Issue UpParam = new Param_Update_MachineTool_Issue();
                    UpParam.Issued_by_ID = Convert.ToInt32(model.MT_IssuedBy);
                    UpParam.Issued_to_ID = Convert.ToInt32(model.MT_IssuedTo);
                    UpParam.Issue_Date = Convert.ToDateTime(model.MT_IssueDate) == null ? DateTime.Now : Convert.ToDateTime(model.MT_IssueDate);
                    UpParam.Issuing_Store_ID = Convert.ToInt32(model.MT_IssuingStoreID);
                    UpParam.Usage_Site_ID = Convert.ToInt32(model.MT_UsageSiteID);
                    UpParam.Remarks = model.MT_Remarks != null ? model.MT_Remarks : "";
                    UpParam.Job_ID = model.MT_JobID;
                    UpParam.ID = model.MT_ID;
                    if (IssuedList != null && IssuedList.Count > 0)
                    {
                        var InsertTool = new List<Param_Insert_MachineTool_Item>();
                        var UpdateTool = new List<Param_update_MachineTool_Item>();
                        var IssueEditIDs = Edit_IssueID != null ? ((List<int>)Edit_IssueID).ToArray() : null;
                        foreach (var item in IssuedList)
                        {
                            if (item.ID == 0)
                            {
                                var insertItem = new Param_Insert_MachineTool_Item();
                                insertItem.Qty = item.Qty_Issued;
                                insertItem.Stock_ID = item.ToolStockID;
                                InsertTool.Add(insertItem);
                            }
                            else if (IssueEditIDs != null && IssueEditIDs.Length > 0)
                            {
                                if (IssueEditIDs.Contains(item.ID))
                                {
                                    var updateItem = new Param_update_MachineTool_Item();
                                    updateItem.Qty = item.Qty_Issued;
                                    updateItem.Stock_ID = item.ToolStockID;
                                    updateItem.ID = item.ID;
                                    UpdateTool.Add(updateItem);
                                }
                            }
                        }
                        UpParam._Insert_Items_Issued = InsertTool.Count > 0 ? InsertTool.ToArray() : null;
                        UpParam._updated_Items_Issued = UpdateTool.Count > 0 ? UpdateTool.ToArray() : null;
                    }
                    if (Delete_Issue_ID != null)
                    {
                        var deleteID = ((List<int>)Delete_Issue_ID).ToArray();//Mantis bug 0001230 fixed
                        UpParam._deleted_Items_Issued_IDs = deleteID;
                    }
                    if (Delete_ExistingRemarks != null)
                    {
                        UpParam._deleted_Remarks_IDs = Delete_ExistingRemarks == null ? null : ((List<int>)Delete_ExistingRemarks).Count > 0 ? ((List<int>)Delete_ExistingRemarks).ToArray() : null;
                    }
                    if (BASE._Stock_MachineTools_DBOps.UpdateMachineToolIssue(UpParam))
                    {
                        return Json(new
                        {
                            result = true,
                            message = Messages.UpdateSuccess
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (actionmethod == "Delete")
                {
                    if (BASE._Stock_MachineTools_DBOps.DeleteMachineToolIssue(model.MT_ID))
                    {
                        return Json(new
                        {
                            message = Messages.DeleteSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            message = Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AllocatedTools_Grid()
        {
            ViewData["MT_ActionMethod"] = MT_ActionMethod;
            ViewData["MachineToolName_List"] = MachineToolName_List;
            return View(MT_Issued_List);
        }
        public ActionResult AllocatedTools_Grid_AddNewRow(MT_Issued_Tools Newrow)
        {
            List<MT_Issued_Tools> gridRows = new List<MT_Issued_Tools>();
            if (ModelState.IsValid)
            {
                var MachineToolList = (List<Return_Get_Stocks_Listing>)MachineToolName_List;
                var SelectedTool = MachineToolList.Where(x => x.ID == Convert.ToInt32(Newrow.Machine_Tool_Name)).FirstOrDefault();
                if (MT_Issued_List != null)
                {
                    gridRows = (List<MT_Issued_Tools>)MT_Issued_List;
                }
                for (int i = 0; i < gridRows.Count; i++)
                {
                    if (gridRows[i].ToolStockID == SelectedTool.ID)
                    {
                        ViewData["EditError"] = "Entry Of The Selected Tool Is Already Present In the Grid</br>Kindly Edit That Row To Make Changes";
                        return View("AllocatedTools_Grid", MT_Issued_List);
                    }
                }
                if (Newrow.Qty_Issued > SelectedTool.Curr_Qty)
                {
                    ViewData["EditError"] = "Quantity Issued Cannot Be Greater Than Available Quantity( " + SelectedTool.Curr_Qty + " )";
                    return View("AllocatedTools_Grid", MT_Issued_List);
                }
                MT_Issued_Tools grid = new MT_Issued_Tools();
                grid.ToolStockID = SelectedTool.ID;
                grid.Qty_Issued = Newrow.Qty_Issued;
                grid.Machine_Tool_Name = SelectedTool.Item_Name;
                gridRows.Add(grid);
                for (int i = 0; i < gridRows.Count(); i++)
                {
                    gridRows[i].Sr = i + 1;
                }
                MT_Issued_List = gridRows;
            }
            else
            {
                ViewData["EditError"] = "Please Correct All Errors..";
            }
            ViewData["EditError"] = "";
            return View("AllocatedTools_Grid", MT_Issued_List);
        }
        public ActionResult AllocatedTools_Grid_EditRow(MT_Issued_Tools Newrow, int Sr = 0, int ToolStockID = 0)
        {
            List<MT_Issued_Tools> gridRows = new List<MT_Issued_Tools>();
            if (ModelState.IsValid)
            {
                var MachineToolList = (List<Return_Get_Stocks_Listing>)MachineToolName_List;
                if (MT_Issued_List != null)
                {
                    gridRows = (List<MT_Issued_Tools>)MT_Issued_List;
                }
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == Sr);
                var ID = dataToEdit.ID;
                var IsToolID = int.TryParse(Newrow.Machine_Tool_Name, out int test);
                var SelectedTool = MachineToolList.Where(x => x.ID == (IsToolID ? Convert.ToInt32(Newrow.Machine_Tool_Name) : dataToEdit.ToolStockID)).FirstOrDefault();

                if (Newrow.Qty_Issued > SelectedTool.Curr_Qty)
                {
                    ViewData["EditError"] = "Quantity Issued Cannot Be Greater Than Available Quantity( " + SelectedTool.Curr_Qty + " )";
                    return View("AllocatedTools_Grid", MT_Issued_List);
                }
                if (ID != 0)
                {
                    var ReturnToolData = MT_ReturnDetail_Data as List<Return_GetRegister_SubNestedGrid>;
                    var ItemReturnData = ReturnToolData.Where(x => x.IssueItemRecID == ID).ToList();
                    if (ItemReturnData.Count() > 0)
                    {
                        if (dataToEdit.ToolStockID != SelectedTool.ID)
                        {
                            ViewData["EditError"] = "Issued Tool Against Which Return Has Been Posted Cannot Be Changed..";
                            return View("AllocatedTools_Grid", MT_Issued_List);
                        }
                        decimal retqty = 0;
                        for (int i = 0; i < ItemReturnData.Count(); i++)
                        {
                            retqty = retqty + ItemReturnData[i].Return_Qty;
                        }
                        if (Newrow.Qty_Issued < retqty)
                        {
                            ViewData["EditError"] = "Issued Quantity Cannot Be Below Return Quantity( " + retqty + " )..";
                            return View("AllocatedTools_Grid", MT_Issued_List);
                        }
                    }
                }
                dataToEdit.Machine_Tool_Name = SelectedTool.Item_Name;
                dataToEdit.ToolStockID = SelectedTool.ID;
                dataToEdit.Qty_Issued = Newrow.Qty_Issued;
                if (ID != 0)
                {
                    var editIssueID = new List<int>();
                    var editedIssueID = Edit_IssueID as List<int>;
                    if (editedIssueID != null)
                    {
                        editedIssueID.Add(ID);
                        Edit_IssueID = editedIssueID;
                    }
                    else
                    {
                        editIssueID.Add(ID);
                        Edit_IssueID = editIssueID;
                    }
                }
            }
            else
            {
                ViewData["EditError"] = "Please Correct All Errors..";
            }
            MT_Issued_List = gridRows;
            ViewData["EditError"] = "";
            return View("AllocatedTools_Grid", MT_Issued_List);
        }
        public ActionResult AllocatedTools_Grid_DeleteRow(int Sr)
        {
            var allData = (List<MT_Issued_Tools>)MT_Issued_List;
            var DataToDelete = allData != null ? allData.Where(x => x.Sr == Sr).FirstOrDefault() : new MT_Issued_Tools();
            var ID = DataToDelete.ID;
            if (ID != 0)
            {
                var ReturnToolData = MT_ReturnDetail_Data as List<Return_GetRegister_SubNestedGrid>;
                var ItemReturnData = ReturnToolData.Where(x => x.IssueItemRecID == ID);
                if (ItemReturnData.Count() > 0)
                {
                    ViewData["EditError"] = "Issued Tool Against Which Return Has Been Posted Cannot Be Deleted..";
                    return View("AllocatedTools_Grid", MT_Issued_List);
                }
            }
            if (allData != null)
            {
                var deleteIssueID = new List<int>();
                if (ID != 0)
                {
                    var deletedIDs = (List<int>)Delete_Issue_ID;
                    if (deletedIDs != null)
                    {
                        deletedIDs.Add(ID);
                        Delete_Issue_ID = deletedIDs;
                    }
                    else
                    {
                        deleteIssueID.Add(ID);
                        Delete_Issue_ID = deleteIssueID;
                    }
                }
                allData.Remove(DataToDelete);
            }
            MT_Issued_List = allData;
            ViewData["EditError"] = "";
            return View("AllocatedTools_Grid", MT_Issued_List);
        }
        #endregion
        #region partial return
        public ActionResult Frm_New_Mach_Tool_Return(string ActionMethod, int ISSUEROWID = 0, int ReturnRowID = 0)
        {
            if ((!CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }            
            Model_Return_MTAllocation model = new Model_Return_MTAllocation();
            model.ActionMethod = ActionMethod;
            model.MTReturn_ReturnDate = DateTime.Now;
            model.LoginUserID = BASE._open_User_PersonnelID;
            Return_Remarks_Window_Grid_Data = new List<Return_GetMachToolReturnRemarks>();
            if (ISSUEROWID != 0)
            {
                var IssueData = ((List<Return_GetRegister_NestedGrid>)MT_IssueDetail_Data).Where(x => x.ID == ISSUEROWID).FirstOrDefault();
                var MainRecData = BASE._Stock_MachineTools_DBOps.GetRecord(IssueData.IssueMainRecID);
                var Role = ((List<Return_GetRegister_MainGrid>)MTAllocationRegister_Data_Glob).Where(X => X.ID == IssueData.IssueMainRecID).FirstOrDefault().CurrUserRole;
                string[] CurrUserRole = (Role != null && Role.Length > 0) ?Role.Split(',').Select(x=>x.Trim()).ToArray() :null;//Mantis bug 0001141 fixed
                if (CurrUserRole == null || !CurrUserRole.Contains("Issuing Store User"))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('User Is Not From Issuing Store Of Selected Row..','Inform'); $('#Dynamic_Content_popup').dxPopup('hide');</script>");
                }
                model.MTReturn_IssuingStoreID = MainRecData.Issuing_Store_ID;
                model.MTReturn_IssueDate = IssueData.IssueDate;
                model.MTReturn_Tool_ID = IssueData.MachineToolID;
                model.MTReturn_MainIssueID = IssueData.IssueMainRecID;
                model.MTReturn_IssueRecID = IssueData.ID;
               
            }
            if (ActionMethod != "New")
            {
                var remarksData = BASE._Stock_MachineTools_DBOps.GetMachToolReturnRemarks(ReturnRowID);
                if (remarksData != null)
                {
                    Return_Remarks_Window_Grid_Data = remarksData;
                }
                var ReturnRowData = ((List<Return_GetRegister_SubNestedGrid>)MT_ReturnDetail_Data).Where(x => x.ID == ReturnRowID).FirstOrDefault();
                var IssueRowID = ReturnRowData.IssueItemRecID;
                var IssueRowData = ((List<Return_GetRegister_NestedGrid>)MT_IssueDetail_Data).Where(x => x.ID == IssueRowID).FirstOrDefault();
                var IssueMainRecID= IssueRowData.IssueMainRecID;
                var MainRecData = BASE._Stock_MachineTools_DBOps.GetRecord(IssueMainRecID);
                model.MTReturn_ReturnDate = ReturnRowData.Return_Date;
                model.MTReturn_IssuingStoreID = MainRecData.Issuing_Store_ID;
                model.MTReturn_IssueDate = MainRecData.Issue_Date;
                model.MTReturn_Tool_ID = IssueRowData.MachineToolID;
                model.MTReturn_ReturnQuantity = (int)ReturnRowData.Return_Qty;
                model.MTReturn_ReturnedTo = MainRecData.Issued_By_ID;
                model.MTReturn_MainIssueID = IssueMainRecID;
                model.MTReturn_ID = ReturnRowID;
                model.MTReturn_IssueRecID = IssueRowID;
                if (ActionMethod == "Edit" || ActionMethod == "Delete")
                {
                    var Role = ((List<Return_GetRegister_MainGrid>)MTAllocationRegister_Data_Glob).Where(X => X.ID == IssueMainRecID).FirstOrDefault().CurrUserRole;
                    string[] CurrUserRole = (Role != null && Role.Length > 0) ? Role.Split(',') : null;
                    if (CurrUserRole == null || !CurrUserRole.Contains("Issuing Store User"))
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('User Is Not From Issuing Store Of Selected Row..','Inform'); $('#Dynamic_Content_popup').dxPopup('hide');</script>");
                    }
                }                
            }
            model.Return_RemarksGrid_data = Return_Remarks_Window_Grid_Data;
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_New_Mach_Tool_Return(Model_Return_MTAllocation model)
        {
            try
            {
                model.MTReturn_Tool_ID = ((List<Return_get_store_Pending_Returns>)MachineToolName_Return_List).Where(x => x.Sr == model.MTReturn_Tool_ID).FirstOrDefault().MachineToolID;
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();

                    if (BASE._open_User_Type == "CLIENT ROLE")
                    {
                        if (AuditedPeriod != null)
                        {
                            if (AuditedPeriod.Rows.Count > 0)
                            {
                                if (model.MTReturn_ReturnDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.MTReturn_ReturnDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Return Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (SubmittedPeriod != null)
                        {
                            if (SubmittedPeriod.Rows.Count > 0)
                            {
                                if (model.MTReturn_ReturnDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.MTReturn_ReturnDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Return Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (model.MTReturn_ReturnQuantity < 1)
                    {
                        return Json(new
                        {
                            message = "Quantity Returned Should Be Greater Than 0...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.MTReturn_ReturnQuantity > model.MTReturn_PendingQuantity)
                    {
                        return Json(new
                        {
                            message = "Quantity Returned Cannot Be Greater Than Pending Quantity...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.MTReturn_ReturnDate < model.MTReturn_IssueDate)
                    {
                        return Json(new
                        {
                            message = "Return Date Cannot Be Less Than Issue Date...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.ActionMethod == "New")
                {
                    var IssueData = ((List<Return_GetRegister_NestedGrid>)MT_IssueDetail_Data).Where(x => x.IssueMainRecID == model.MTReturn_MainIssueID);
                    model.MTReturn_IssueRecID = IssueData.Where(x => x.MachineToolID == model.MTReturn_Tool_ID).FirstOrDefault().ID;
                    Param_Insert_MachineTool_Return Inparam = new Param_Insert_MachineTool_Return();
                    Inparam.Issue_Item_ID = (int)model.MTReturn_IssueRecID;
                    Inparam.Qty_Returned = Convert.ToDecimal(model.MTReturn_ReturnQuantity);
                    Inparam.Remarks = model.MTReturn_Remarks == null ? "" : model.MTReturn_Remarks;
                    Inparam.Return_Date = (DateTime)model.MTReturn_ReturnDate;

                    if (BASE._Stock_MachineTools_DBOps.InsertMachineToolReturn(Inparam))
                    {

                        return Json(new
                        {
                            result = true,
                            message = Messages.SaveSuccess
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
                if (model.ActionMethod == "Edit")
                {
                    Param_Update_MachineTool_Return UpParam = new Param_Update_MachineTool_Return();
                    UpParam.ID = model.MTReturn_ID;
                    UpParam.Qty_Returned = Convert.ToDecimal(model.MTReturn_ReturnQuantity);
                    UpParam.Remarks = model.MTReturn_Remarks == null ? "" : model.MTReturn_Remarks;
                    UpParam.Return_Date = (DateTime)model.MTReturn_ReturnDate;
                    UpParam._deleted_Remarks_IDs = Delete_ReturnRemarks_MTAR == null ? null : ((List<int>)Delete_ReturnRemarks_MTAR).Count > 0 ? ((List<int>)Delete_ReturnRemarks_MTAR).ToArray() : null;
                    if (BASE._Stock_MachineTools_DBOps.UpdateMachineToolReturn(UpParam))
                    {
                        return Json(new
                        {
                            result = true,
                            message = Messages.UpdateSuccess
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
                if (model.ActionMethod == "Delete")
                {
                    if (BASE._Stock_MachineTools_DBOps.DeleteMachineToolReturn(model.MTReturn_ID))
                    {
                        return Json(new
                        {
                            result = true,
                            message = Messages.DeleteSuccess
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
        #endregion
        #region full return
        [HttpPost]
        public ActionResult Frm_Mach_Tool_Full_Return(int[] ISSUEROWID)
        {
            if ((!CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }

            Model_Full_Return_MTAllocation model = new Model_Full_Return_MTAllocation();
            List<FullReturnGrid> ReturnGridData = new List<FullReturnGrid>();
            var IssueGridData = (List<Return_GetRegister_NestedGrid>)MT_IssueDetail_Data;
            var Role = ((List<Return_GetRegister_MainGrid>)MTAllocationRegister_Data_Glob).Where(X => X.ID == IssueGridData[0].IssueMainRecID).FirstOrDefault().CurrUserRole;
            string[] CurrUserRole = (Role != null && Role.Length > 0) ? Role.Split(',').Select(x=>x.Trim()).ToArray() : null;//Mantis bug 0001141 fixed            
            if (CurrUserRole == null || !CurrUserRole.Contains("Issuing Store User"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('User Is Not From Issuing Store Of Selected Rows..','Inform'); $('#Dynamic_Content_popup').dxPopup('hide');</script>");
            }
            for (int i = 0; i < ISSUEROWID.Length; i++)
            {
                var IssueRow = IssueGridData.Where(x => x.ID == ISSUEROWID[i]).FirstOrDefault();
                FullReturnGrid ReturnNewRow = new FullReturnGrid();
                ReturnNewRow.MachineName = IssueRow.Mach_Tool_Name;
                ReturnNewRow.IssueDate = IssueRow.IssueDate;
                ReturnNewRow.QtyIssued = (int)IssueRow.Qty_Allocated;
                ReturnNewRow.QtyPending = (int)(IssueRow.Qty_Allocated - IssueRow.Qty_Returned);
                ReturnNewRow.QtyReturned = (int)(IssueRow.Qty_Allocated - IssueRow.Qty_Returned);
                ReturnNewRow.IssueRowID = IssueRow.ID;
                ReturnNewRow.IssuingStore = IssueRow.IssuingStore;
                ReturnNewRow.MachineToolID = IssueRow.MachineToolID;
                ReturnNewRow.Sr = i + 1;
                ReturnNewRow.ID = 0;
                ReturnGridData.Add(ReturnNewRow);
            }
            MTAllocation_FullReturnGridData = ReturnGridData;
            model.FullReturn_Grid_data = MTAllocation_FullReturnGridData;
            model.MTFullReturn_ReturnDate = DateTime.Now;
            model.MTFullReturn_ReturnedTo = BASE._open_User_ID;
            return View(model);
        }
        public ActionResult FullReturn_Grid()
        {
            return View(MTAllocation_FullReturnGridData);
        }
        public ActionResult UpdateFullReturnGridData(int key, string field, int? value)
        {
            var data = (List<FullReturnGrid>)MTAllocation_FullReturnGridData;
            var DataToEdit = data.FirstOrDefault(x => x.Sr == key);
            if (DataToEdit.QtyPending < value)
            {
                return Json(new
                {
                    message = "Returned Quantity Cannot Be Greater Than Pending Quantity...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
            DataToEdit.QtyReturned = (int)value;
            MTAllocation_FullReturnGridData = data;
            return Json(new
            {
                message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Frm_Mach_Tool_Full_Return_Save(Model_Full_Return_MTAllocation model)
        {
            try
            {
                var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();

                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (AuditedPeriod != null)
                    {
                        if (AuditedPeriod.Rows.Count > 0)
                        {
                            if (model.MTFullReturn_ReturnDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.MTFullReturn_ReturnDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
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
                            if (model.MTFullReturn_ReturnDate >= Convert.ToDateTime(SubmittedPeriod.Rows[0]["FROMDATE"]) && model.MTFullReturn_ReturnDate <= Convert.ToDateTime(SubmittedPeriod.Rows[0]["TODATE"]))
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
                var ReturnGridData = (List<FullReturnGrid>)MTAllocation_FullReturnGridData;
                DateTime MaxIssueDate = ReturnGridData.Max(X => X.IssueDate);
                if (model.MTFullReturn_ReturnDate < MaxIssueDate)
                {
                    return Json(new
                    {
                        message = "Return Date Cannot Be Less Than Issue Date( " + MaxIssueDate.ToString("dd/MM/yyyy") + " )...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }


                for (int i = 0; i < ReturnGridData.Count(); i++)
                {
                    Param_Insert_MachineTool_Return InParam = new Param_Insert_MachineTool_Return();
                    InParam.Issue_Item_ID = ReturnGridData[i].IssueRowID;
                    InParam.Qty_Returned = ReturnGridData[i].QtyReturned;
                    InParam.Remarks = model.MTFullReturn_Remarks ?? "";
                    InParam.Return_Date = model.MTFullReturn_ReturnDate;
                    if (!BASE._Stock_MachineTools_DBOps.InsertMachineToolReturn(InParam))
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
                    message = Messages.SaveSuccess
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
        #endregion
        public ActionResult CheckToolIssued(int ReturnRowID)
        {
            var IssueRowID = ((List<Return_GetRegister_SubNestedGrid>)MT_ReturnDetail_Data).Where(x => x.ID == ReturnRowID).FirstOrDefault().IssueItemRecID;
            var MachineID = ((List<Return_GetRegister_NestedGrid>)MT_IssueDetail_Data).Where(x => x.ID == IssueRowID).FirstOrDefault().MachineToolID;
            int count=BASE._Stock_MachineTools_DBOps.GetMachineToolIssueCount(IssueRowID, MachineID);
            if (count > 0)
            {
                return Json(new
                {                   
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {                   
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DataNavigation(int IssueMainRowID = 0,int ReturnRowID=0)
        {
            DateTime? IssueDate = null;
            DateTime? ReturnDate = null;
            if (ReturnRowID == 0)
            {           
                IssueDate = ((List<Return_GetRegister_MainGrid>)MTAllocationRegister_Data_Glob).Where(x => x.ID == IssueMainRowID).FirstOrDefault().Issue_Date;
            }
            else
            {
                ReturnDate = ((List<Return_GetRegister_SubNestedGrid>)MT_ReturnDetail_Data).Where(x => x.ID == ReturnRowID).FirstOrDefault().Return_Date;
            }
            DateTime? Date = IssueDate == null ? ReturnDate : IssueDate;
            var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
            var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
            if (BASE._open_User_Type == "CLIENT ROLE")
            {
                if (AuditedPeriod != null)
                {
                    if (AuditedPeriod.Rows.Count > 0)
                    {
                        if (Date >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && Date <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "No Changes Are Allowed In Account Audited Period...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (SubmittedPeriod != null)
                {
                    if (SubmittedPeriod.Rows.Count > 0)
                    {
                        if (Date >= Convert.ToDateTime(SubmittedPeriod.Rows[0]["FROMDATE"]) && Date <= Convert.ToDateTime(SubmittedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "No Changes Are Allowed In Account Submission Period...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#MTAllocation_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#MTAllocationListPreview').hide();</script>");
            }
            return View();
        }
        #region "DropDown"
        public ActionResult Get_IssuingStore_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetStoreList> Store_List = BASE._StockDeptStores_dbops.GetStoreList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Store_List, loadOptions)));
        }
        public ActionResult Get_IssuedBy_List(DataSourceLoadOptions loadOptions, int? storeid = 0)
        {
            List<Return_GetPersonnels> IssuedBy_List = new List<Return_GetPersonnels>();
            IssuedBy_List = BASE._Stock_MachineTools_DBOps.GetPersonnels((int)storeid);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(IssuedBy_List, loadOptions)));
        }
        public ActionResult Get_IssuedTo_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetStockPersonnels> IssuedTo_List = BASE._Jobs_Dbops.GetStockPersonnels();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(IssuedTo_List, loadOptions)));
        }
        public ActionResult Get_JobName_List(DataSourceLoadOptions loadOptions)
        {
            List<Common_Lib.DbOperations.Jobs.Return_GetList> JobName_List = BASE._Jobs_Dbops.GetOpenJobs();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(JobName_List, loadOptions)));
        }
        public ActionResult Get_UsageSite_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetStoreDept> UsageSite_List = BASE._StockDeptStores_dbops.GetAllDeptList(ClientScreen.Stock_Machine_Tool_Issue);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UsageSite_List, loadOptions)));
        }
        public void Get_MachineToolName_List(int storeid)
        {
            List<Return_Get_Stocks_Listing> MTName_List = BASE._Stock_MachineTools_DBOps.Get_MachineToolStock(storeid);
            MachineToolName_List = MTName_List;
        }
        public ActionResult Get_MachineToolName_Return_List(DataSourceLoadOptions loadOptions, int storeid)
        {
            var MTName_List = BASE._Stock_MachineTools_DBOps.GetPendingReturns(storeid);
            MachineToolName_Return_List = MTName_List;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MTName_List, loadOptions)));
        }
        #endregion
        #region Issue Tool Remarks
        public ActionResult Existing_RemarksGrid(string ActionMethodName, int MToolIssueID = 0)
        {
            var remarksList = new List<Return_GetMachToolIssueRemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(remarksList);
            }
            if (Existing_Remarks_Window_Grid_Data == null)
            {
                var remarkscData = BASE._Stock_MachineTools_DBOps.GetMachToolIssueRemarks(MToolIssueID);
                if (remarkscData != null)
                {
                    remarksList = remarkscData;
                }
                Existing_Remarks_Window_Grid_Data = remarksList;
            }
            return PartialView(Existing_Remarks_Window_Grid_Data);
        }
        [HttpGet]
        public ActionResult Frm_Existing_RemarksWindow(string ActionMethod = null, int SR_No = 0)
        {
            var model = new ReturnExisintRemark();
            model.ActionMethod = ActionMethod;

            var Data = Existing_Remarks_Window_Grid_Data as List<Return_GetMachToolIssueRemarks>;
            string itstr = "";
            if (Data != null)
            {
                var res = Data.Where(f => f.ID == SR_No).FirstOrDefault();
                model.Remark = res.Remarks;
            }

            return PartialView(model);
        }
        public ActionResult Frm_Remarks_Window_Delete_Grid_Record(string ActionMethod, int SR_No = 0, int id = 0)
        {
            var SR = Convert.ToInt16(SR_No);
            var allData = (List<Return_GetMachToolIssueRemarks>)Existing_Remarks_Window_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetMachToolIssueRemarks();
            var RemarksBy = dataToDelete.Remarks_By;
            if (RemarksBy != BASE._open_User_ID)
            {
                return Json(new
                {
                    result = false,
                    message = "User Is Allowed To Delete His Own Remarks Only.."
                }, JsonRequestBehavior.AllowGet);
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Existing_Remarks_Window_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new List<int>();
                var deleteRemarks = Delete_ExistingRemarks as List<int>;
                if (deleteRemarks != null)
                {
                    deleteRemarks.Add(id);
                    Delete_ExistingRemarks = deleteRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_ExistingRemarks = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Return Tool Remarks
        public ActionResult Return_RemarksGrid(string ActionMethodName, int MToolReturnID = 0)
        {
            var remarksList = new List<Return_GetMachToolReturnRemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(remarksList);
            }
            if (Return_Remarks_Window_Grid_Data == null && MToolReturnID > 0)
            {

                var remarkscData = BASE._Stock_MachineTools_DBOps.GetMachToolReturnRemarks(MToolReturnID);
                if (remarkscData != null)
                {
                    remarksList = remarkscData;
                }

                Return_Remarks_Window_Grid_Data = remarksList;
            }          
            return PartialView(Return_Remarks_Window_Grid_Data);
        }
        [HttpGet]
        public ActionResult Frm_Return_RemarksWindow(string ActionMethod = null, int SR_No = 0)
        {
            var model = new ReturnExisintRemark();
            model.ActionMethod = ActionMethod;

            var Data = Return_Remarks_Window_Grid_Data as List<Return_GetMachToolReturnRemarks>;
            string itstr = "";
            if (Data != null)
            {
                var res = Data.Where(f => f.Sr_No == SR_No).FirstOrDefault();
                model.Remark = res.Remarks;
            }

            return PartialView("Frm_Existing_RemarksWindow",model);
        }
        public ActionResult Frm_Return_Remarks_Window_Delete_Grid_Record(string ActionMethod, int SR_No = 0, int id = 0)
        {
            var SR = Convert.ToInt16(SR_No);
            var allData = (List<Return_GetMachToolReturnRemarks>)Return_Remarks_Window_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetMachToolReturnRemarks();
            var RemarksBy = dataToDelete.Remarks_By;
            if (RemarksBy != BASE._open_User_ID)
            {
                return Json(new
                {
                    result = false,
                    message = "User Is Allowed To Delete His Own Remarks Only.."
                }, JsonRequestBehavior.AllowGet);
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Return_Remarks_Window_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new List<int>();
                var deleteRemarks = Delete_ReturnRemarks_MTAR as List<int>;
                if (deleteRemarks != null)
                {
                    deleteRemarks.Add(id);
                    Delete_ReturnRemarks_MTAR = deleteRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_ReturnRemarks_MTAR = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region"MISC"

        public void Sessionclear()
        {
            Session.Remove("MT_IssueDetail_Data");
            Session.Remove("MT_ReturnDetail_Data");
            ClearBaseSession("_MTAR");
        }
        public void InfoSessionclear()
        {
            ClearBaseSession("_MTARInfo");
        }
        public void Sessionclear_return()
        {            
        }
        public void Sessionclear_FullReturn()
        {            
        }
        public void MT_user_rights()
        {
            ViewData["MT_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Add");
            ViewData["MT_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Update");
            ViewData["MT_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "View");
            ViewData["MT_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Delete");
            ViewData["MT_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Export");
            ViewData["MT_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Report");
            ViewData["MT_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_Machine_Tool_Issue, "Approve");
            ViewData["MT_AddJobRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Add");
        }
        #endregion
    }
}