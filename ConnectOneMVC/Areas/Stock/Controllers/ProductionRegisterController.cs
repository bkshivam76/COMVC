using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Areas.Help.Models;
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
using static Common_Lib.DbOperations.StockDeptStores;
using static Common_Lib.DbOperations.StockProduction;
using static Common_Lib.DbOperations.StockProfile;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class ProductionRegisterController : BaseController
    {
        // GET: Stock/ProductionRegister
        #region Global Variables
        public DateTime? ProductionFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("ProductionFromDate_ProdInfo");
            }
            set
            {
                SetBaseSession("ProductionFromDate_ProdInfo", value);
            }
        }
        public DateTime? ProductionToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("ProductionToDate_ProdInfo");
            }
            set
            {
                SetBaseSession("ProductionToDate_ProdInfo", value);
            }
        }
        public List<Return_GetRegister_MainGrid> ProductionRegister_Data_Glob
        {
            get
            {
                return (List<Return_GetRegister_MainGrid>)GetBaseSession("ProductionRegister_Data_Glob_ProdInfo");
            }
            set
            {
                SetBaseSession("ProductionRegister_Data_Glob_ProdInfo", value);
            }
        }
        public List<Return_GetRegister_NestedGrid> ItemProduce_ExportData
        {
            get
            {
                return (List<Return_GetRegister_NestedGrid>)GetBaseSession("ItemProduce_ExportData_ProdInfo");
            }
            set
            {
                SetBaseSession("ItemProduce_ExportData_ProdInfo", value);
            }
        }
        public List<Return_GetProdItemsConsumed> Production_StockConsumedData
        {
            get
            {
                return (List<Return_GetProdItemsConsumed>)GetBaseSession("Production_StockConsumedData_Prod");
            }
            set
            {
                SetBaseSession("Production_StockConsumedData_Prod", value);
            }
        }
        public List<Return_GetDocumentsGridData> Production_Documents_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("Production_Documents_Window_Grid_Data_Prod");
            }
            set
            {
                SetBaseSession("Production_Documents_Window_Grid_Data_Prod", value);
            }
        }
        public ArrayList ProductionEdit_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_Document_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_Document_ID_Prod", value);
            }
        }
        public ArrayList ProductionDelete_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionDelete_Document_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionDelete_Document_ID_Prod", value);
            }
        }
        public ArrayList ProductionUnlink_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionUnlink_Document_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionUnlink_Document_ID_Prod", value);
            }
        }
        public List<Return_GetProdExpensesIncurred> Grid_Data_Actual_Expenses_Data_Production
        {
            get
            {
                return (List<Return_GetProdExpensesIncurred>)GetBaseSession("Grid_Data_Actual_Expenses_Data_Production_Prod");
            }
            set
            {
                SetBaseSession("Grid_Data_Actual_Expenses_Data_Production_Prod", value);
            }
        }
        public ArrayList Delete_Expense_prod
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Expense_prod_Prod");
            }
            set
            {
                SetBaseSession("Delete_Expense_prod_Prod", value);
            }
        }
        public ArrayList ProductionEdit_StockCon_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_StockCon_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_StockCon_ID_Prod", value);
            }
        }
        public ArrayList Delete_StockCon_Prod
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_StockCon_Prod_Prod");
            }
            set
            {
                SetBaseSession("Delete_StockCon_Prod_Prod", value);
            }
        }
        public List<Return_GetProdItemProduced> Production_StockProducedData
        {
            get
            {
                return (List<Return_GetProdItemProduced>)GetBaseSession("Production_StockProducedData_Prod");
            }
            set
            {
                SetBaseSession("Production_StockProducedData_Prod", value);
            }
        }
        public ArrayList ProductionEdit_StockProd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_StockProd_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_StockProd_ID_Prod", value);
            }
        }
        public ArrayList Delete_Stock_Prod
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Stock_Prod_Prod");
            }
            set
            {
                SetBaseSession("Delete_Stock_Prod_Prod", value);
            }
        }
        public List<Return_GetProdMachineUsage> Production_Machine_Usage_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetProdMachineUsage>)GetBaseSession("Production_Machine_Usage_Window_Grid_Data_Prod");
            }
            set
            {
                SetBaseSession("Production_Machine_Usage_Window_Grid_Data_Prod", value);
            }
        }
        public ArrayList ProductionEdit_Machine_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_Machine_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_Machine_ID_Prod", value);
            }
        }
        public ArrayList ProductionDelete_MachineUsage_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionDelete_MachineUsage_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionDelete_MachineUsage_ID_Prod", value);
            }
        }
        public List<Return_GetProdManpowerUsage> Grid_Data_Actual_Manpower_Usage_Data_Production
        {
            get
            {
                return (List<Return_GetProdManpowerUsage>)GetBaseSession("Grid_Data_Actual_Manpower_Usage_Data_Production_Prod");
            }
            set
            {
                SetBaseSession("Grid_Data_Actual_Manpower_Usage_Data_Production_Prod", value);
            }
        }
        public ArrayList ProductionEdit_Manpower_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_Manpower_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_Manpower_ID_Prod", value);
            }
        }
        public ArrayList Delete_Actual_Manpower_Usage_Data_Production
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Actual_Manpower_Usage_Data_Production_Prod");
            }
            set
            {
                SetBaseSession("Delete_Actual_Manpower_Usage_Data_Production_Prod", value);
            }
        }
        public List<Return_GetProdScrapProduced> Production_ScrapCreatedData
        {
            get
            {
                return (List<Return_GetProdScrapProduced>)GetBaseSession("Production_ScrapCreatedData_Prod");
            }
            set
            {
                SetBaseSession("Production_ScrapCreatedData_Prod", value);
            }
        }
        public ArrayList ProductionEdit_Scrap_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("ProductionEdit_Scrap_ID_Prod");
            }
            set
            {
                SetBaseSession("ProductionEdit_Scrap_ID_Prod", value);
            }
        }
        public ArrayList Delete_Scrap_Prod
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Scrap_Prod_Prod");
            }
            set
            {
                SetBaseSession("Delete_Scrap_Prod_Prod", value);
            }
        }
        public List<int> Delete_ProductionExisting_Remarks_ID
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_ProductionExisting_Remarks_ID_Prod");
            }
            set
            {
                SetBaseSession("Delete_ProductionExisting_Remarks_ID_Prod", value);
            }
        }
        public List<Return_Get_Prod_Expenses_For_Mapping> ProductionActuallExpensions
        {
            get
            {
                return (List<Return_Get_Prod_Expenses_For_Mapping>)GetBaseSession("ProductionActuallExpensions_Prod");
            }
            set
            {
                SetBaseSession("ProductionActuallExpensions_Prod", value);
            }
        }
        public List<Return_GetProdRemarks> ProductionExisting_Remarks_Grid_Data
        {
            get
            {
                return (List<Return_GetProdRemarks>)GetBaseSession("ProductionExisting_Remarks_Grid_Data_Prod");
            }
            set
            {
                SetBaseSession("ProductionExisting_Remarks_Grid_Data_Prod", value);
            }
        }

        
        #endregion


        #region "Grid"
        public ActionResult Frm_ProductionRegister_Info(DateTime? FromDate, DateTime? ToDate, bool ChangeStatus = true)
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_Production, "List"))
            {
                Production_user_rights();
                String PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;
                ViewData["ProductionExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ProductionExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ProductionExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                              
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Production').hide();</script>");
            }
        }
        public ActionResult Frm_ProductionRegister_Grid(string command, DateTime? FromDate, DateTime? ToDate, int ShowHorizontalBar = 0 )
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            Production_user_rights();
            ViewData["ProductionExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ProductionExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ProductionExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            if (ProductionRegister_Data_Glob == null || command == "REFRESH")
            {
                var ProductionRegister_Data = BASE._Stock_Production_DBOps.GetRegister(Convert.ToDateTime(ProductionFromDate), Convert.ToDateTime(ProductionToDate));
                if (ProductionRegister_Data != null)
                {
                    var Mastergrid = ProductionRegister_Data.main_Register;
                    var Nestedgrid = ProductionRegister_Data.nested_Register;
                    ProductionRegister_Data_Glob = Mastergrid;
                    ItemProduce_ExportData = Nestedgrid;
                    Session["ItemProduce_ExportData"] = ItemProduce_ExportData;
                }
            }
            List<Return_GetRegister_MainGrid> Mastergrid_data = ProductionRegister_Data_Glob as List<Return_GetRegister_MainGrid>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }
        public PartialViewResult Frm_ProductionRegister_ItemProduce_Grid(int Prod_ID, string Command, DateTime? FromDate , DateTime? ToDate)
        {
            ViewData["Stock_Prod_ID"] = Prod_ID;
            ViewData["FromDate"] = FromDate;
            ViewData["ToDate"] = ToDate;
            if (ItemProduce_ExportData == null || Command == "REFRESH")
            {
                var ProductionRegister_Data = BASE._Stock_Production_DBOps.GetRegister(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                var ItemProduce_Data = ProductionRegister_Data.nested_Register;
                ItemProduce_ExportData = ItemProduce_Data;
            }
            var data = ItemProduce_ExportData as List<Return_GetRegister_NestedGrid>;
            List<Return_GetRegister_NestedGrid> itemproduce = data.FindAll(x => x.Prod_ID == Prod_ID);
            return PartialView(itemproduce);
        }
        public ActionResult ProductionRegisterCustomDataAction(int key = 0)
        {
            var Data = ProductionRegister_Data_Glob as List<Return_GetRegister_MainGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.ID + "![" + it.EditedBy + "![" + it.EditedOn + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.Prod_Date;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult ItemProduceCustomDataAction(int key = 0)
        {
            var Data = ItemProduce_ExportData as List<Return_GetRegister_NestedGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Prod_ID + "![" + it.EditedBy + "![" + it.EditedOn + "![" + it.AddedBy + "![" + it.AddedOn;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult CreationDetail(string Xrow, string AddOn, string AddBy, string EditOn, string EditBy)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;

                if (IsDate(AddOn))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(AddOn) ? "" : Convert.ToDateTime(AddOn).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(AddBy) ? "" : AddBy.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(AddBy) ? "" : AddBy.Trim().ToUpper());
                }
                if (IsDate(EditOn))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(EditOn) ? "" : Convert.ToDateTime(EditOn).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(EditBy) ? "" : EditBy.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(EditBy) ? "" : EditBy.Trim().ToUpper());
                }

                return Json(new
                {
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Create = "",
                    Lbl_Modify = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public static GridViewSettings ProductionRegisterNestedGridSettings(int Prod_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ProductionRegister" + Prod_ID;
            settings.SettingsDetail.MasterGridName = "ProductionRegisterListGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Item_Produced");
            settings.Columns.Add("Head");
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Remarks").Visible = true;
            settings.Columns.Add("Produced_Qty").Visible = true;
            settings.Columns.Add("Accepted_Qty").Visible = true;
            settings.Columns.Add("Rejected_Qty").Visible = true;   
            settings.Columns.Add("Cost_Price").Visible = true;
            settings.Columns.Add("Market_Price").Visible = true;
            settings.Columns.Add("AddedBy").Visible = false;
            settings.Columns.Add("AddedOn").Visible = false;
            settings.Columns.Add("EditedOn").Visible = false;
            settings.Columns.Add("EditedBy").Visible = false;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("Prod_ID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "onitemproducefocusedrowchange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for exporting nested grid 
        public static IEnumerable GetItemProduce(int Prod_ID)
        {
            List<Return_GetRegister_NestedGrid> data = (List<Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["ItemProduce_ExportData"];
            List<Return_GetRegister_NestedGrid> itemproducelist = data.FindAll(x => x.Prod_ID == Prod_ID);
            return itemproducelist;
        }//binding data to nested grid
        #endregion
        #region "Period Selection"

        public ActionResult LookUp_Get_ViewType_List_Production(bool? IsVisible, DataSourceLoadOptions loadOptions)
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
        public ActionResult LookUp_ViewType_ChangeEvent_Production(string Chaval)
        {
            ProductionRegister_Period model = GetPeriod(Chaval);
            ProductionFromDate = model.Production_Fromdate;
            ProductionToDate = model.Production_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ProductionRegister_Period GetPeriod(string Chaval)
        {
            ProductionRegister_Period model = new ProductionRegister_Period();
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
            model.Production_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");           
            model.Production_Fromdate = xFr_Date;
            model.Production_Todate = xTo_Date;
            return model;            
        }
        [HttpGet]
        public ActionResult Frm_Change_Period_Screen_Production()
        {
            ProductionRegister_Period model = new ProductionRegister_Period();
            model.Production_PeriodSelection = "Specific Period";
            model.Production_Todate = (DateTime)ProductionToDate;
            model.Production_Fromdate = (DateTime)ProductionFromDate;
            model.Production_BE_View_Period = "";
            model.Production_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.Production_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_Production(ProductionRegister_Period model)
        {
            if (model.Production_Todate < model.Production_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductionToDate = model.Production_Todate;
                ProductionFromDate = model.Production_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }//0000117 bug fixed
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
        #endregion
        #region "NEVD"
        public ActionResult DataNavigation(string ActionMethod, int ID, DateTime ProdDate)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Production, "Update") && ActionMethod == "Edit")
            {
                return Json(new { result = "NoUpdateRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if (!CheckRights(BASE, ClientScreen.Stock_Production, "Delete") && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if (ActionMethod == "Delete" || ActionMethod == "Edit")
            {
                var Production_Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
                var Production_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (Production_Audited_Date != null)
                    {
                        if (Production_Audited_Date.Rows.Count > 0)
                        {
                            if (ProdDate >= Convert.ToDateTime(Production_Audited_Date.Rows[0]["FROMDATE"]) && ProdDate <= Convert.ToDateTime(Production_Audited_Date.Rows[0]["TODATE"]))
                            {
                                return Json(new
                                {
                                    Message = "No Changes Are Allowed In Audited Period.Production Date should not be in Audited period...!",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Production_Submitted_Date != null)
                    {
                        if (Production_Submitted_Date.Rows.Count > 0)
                        {
                            if (ProdDate >= Convert.ToDateTime(Production_Submitted_Date.Rows[0]["FROMDATE"]) && ProdDate <= Convert.ToDateTime(Production_Submitted_Date.Rows[0]["TODATE"]))
                            {
                                return Json(new
                                {
                                    Message = "No Changes Are Allowed In Accounts Submitted Period.Production Date Should Not Be In Account Submission Period...!",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
               

            }
            if (ActionMethod == "Delete")
            {
                var manpowerleftcount = BASE._Stock_Production_DBOps.GetUsedLeftManpowerCount(ID);
                if (manpowerleftcount > 0)
                {
                    return Json(new
                    {
                        Message = "Any Production Using A Manpower Which Has “Left” Cannot Be Deleted",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);

                }    
                var scrapcreatedgriddata = BASE._Stock_Production_DBOps.GetProdScrapProduced(ID);
                if (scrapcreatedgriddata != null)
                {
                    for (int i = 0; i < scrapcreatedgriddata.Count; i++)
                    {
                        var scrapcreatedcheck = BASE._Stock_Profile_DBOps.GetStockUsage(scrapcreatedgriddata[i].CreatedStockID, ClientScreen.Stock_Production);
                        if (scrapcreatedcheck.Count > 0)
                        {
                            return Json(new
                            {
                                Message = "Production Cannot Be Deleted Because " + scrapcreatedgriddata[i].Item_Name + " Is Used In " + scrapcreatedcheck[0].Screen,
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new
            {
                Message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_NEVD_Production(string ActionMethod, int ID = 0)
        {
            Production_user_rights();
            if (!CheckRights(BASE, ClientScreen.Stock_Production, "Add") && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#ProductionNew').hide();</script>");
            }
            if (!CheckRights(BASE, ClientScreen.Stock_Production, "View") && ActionMethod == "View")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#ProductionView').hide();</script>");
            }

            Model_NEVD_Production model = new Model_NEVD_Production();
            model.ActionMethod = ActionMethod;
            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var selectedrowdata = BASE._Stock_Production_DBOps.GetProdDetails(ID);
                if (selectedrowdata != null)
                {
                    model.Prod_ID = ID;
                    model.ProductionNumber = selectedrowdata.Prod_No;
                    model.ProductionDate = selectedrowdata.Prod_Date;
                    model.ProductionFromDate = selectedrowdata.Prod_FromDate;
                    model.ProductionToDate = selectedrowdata.Prod_ToDate;
                    model.ProductionLocationID = selectedrowdata.LocationID;
                    model.ProductionLocation = selectedrowdata.Location;
                    model.ProductionLotNumber = selectedrowdata.Lot_No;
                    model.ProductionProjectNameID = selectedrowdata.ProjID;                    
                    model.ProductionProjectName = selectedrowdata.Proj_Name;
                    model.ProductionSanctionNo = selectedrowdata.Sanction_No;
                    model.ProductionDoneBy = selectedrowdata.ProdDoneBy;
                    model.ProductionStore = selectedrowdata.Prod_Store;
                    model.ProductionStoreID = selectedrowdata.Prod_Store_ID;                      
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_NEVD_Production(Model_NEVD_Production model)
        {
            string actionmethod = model.ActionMethod;
            try
            {
                #region "Checking Restriction"
                if (actionmethod == "New" || actionmethod == "Edit")
                {                    
                    if (model.ProductionDate < BASE._open_Year_Sdt || model.ProductionDate > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            Message = "Production Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //if (Production_StockProducedData == null || (Production_StockProducedData as List<Return_GetProdItemProduced>).Count == 0)
                    //{
                    //    return Json(new
                    //    {
                    //        Message = "You Must Have An Entry For Stock Produced...!",
                    //        result = "falsestockproduce",
                    //    }, JsonRequestBehavior.AllowGet);
                    //}//Mantis bug 0000944 fixed
                    if (Production_StockConsumedData == null || (Production_StockConsumedData as List<Return_GetProdItemsConsumed>).Count == 0)//0000059 bug number fixed.
                    {
                        return Json(new
                        {
                            Message = "You Must Have An Entry For Stock Consumed...!",
                            result = "falsestockconsume",
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._open_User_Type == "CLIENT ROLE")
                    {
                        var Production_Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
                        var Production_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                        if (Production_Audited_Date != null)
                        {
                            if (Production_Audited_Date.Rows.Count > 0)
                            {
                                if (model.ProductionDate >= Convert.ToDateTime(Production_Audited_Date.Rows[0]["FROMDATE"]) && model.ProductionDate <= Convert.ToDateTime(Production_Audited_Date.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        Message = "No Changes Are Allowed In Audited Period.Production Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (Production_Submitted_Date != null)
                        {
                            if (Production_Submitted_Date.Rows.Count > 0)
                            {
                                if (model.ProductionDate >= Convert.ToDateTime(Production_Submitted_Date.Rows[0]["FROMDATE"]) && model.ProductionDate <= Convert.ToDateTime(Production_Submitted_Date.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        Message = "No Changes Are Allowed In Accounts Submitted Period.Production Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (actionmethod == "Edit")
                    { 
                        var stockproducepercentage = calculatetotalpercentage();
                        if (stockproducepercentage != Convert.ToDecimal(100.00))
                        {
                            return Json(new
                            {
                                Message = "Total of “Percentage of Total Value” Of All Items Produced Should Be 100 ...!",
                                result = "falsestockproduce",
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }//Mantis bug 0000944 fixed
                }
                #endregion
                #region "IUD"
                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    Param_Insert_Production_Txn InParam = new Param_Insert_Production_Txn();
                    Param_Update_Production_Txn UpParam = new Param_Update_Production_Txn();

                    IUDDocuments(ref InParam, ref UpParam, actionmethod, model);
                    IUDExpenses(ref InParam, ref UpParam, actionmethod);
                    IUDItemConsumed(ref InParam, ref UpParam, actionmethod);
                    IUDItemProduce(ref InParam, ref UpParam, model);
                    IUDManpowerUsage(ref InParam, ref UpParam, actionmethod);
                    IUDMachineUsage(ref InParam, ref UpParam, actionmethod);
                    IUDScrapProduced(ref InParam, ref UpParam, model);
                    IUDRemainingData(ref InParam, ref UpParam, model);
                    if (actionmethod == "New")
                    {
                        if (BASE._Stock_Production_DBOps.InsertProduction_Txn(InParam))
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
                        if (BASE._Stock_Production_DBOps.UpdateProduction_Txn(UpParam))
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
                if (actionmethod == "Delete")
                {
                    if (BASE._Stock_Production_DBOps.DeleteProduction_Txn(model.Prod_ID))
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
                #endregion
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
        public ActionResult Frm_Export_Options()// list export
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Production, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#ProductionRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#ProductionListPreview').hide();</script>");
            }
            return View();
        }      
        #region "IUD Functions"
        public void IUDDocuments(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, string actionmethod, Model_NEVD_Production model)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Production_Documents_Window_Grid_Data;
            var index = 0;
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
                        InEInfo.Ref_Screen = "Production";
                        InEInfo.Ref_Rec_ID = model.Prod_ID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments[index] = InEInfo;
                        index++;

                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Added_Attachments = insertAttachments;
                }
                if (actionmethod == "Edit")
                {
                    if (ProductionEdit_Document_ID != null)
                    {
                        string[] doceditid = (string[])(ProductionEdit_Document_ID as ArrayList).ToArray(typeof(string));
                        if (doceditid != null)
                        {
                            var updateattachment = new Parameter_Update_Attachment[doceditid.Count()];
                            for (int j = 0; j < doceditid.Count(); j++)
                            {
                                for (int i = 0; i < DocumentsData.Count; i++)
                                {
                                    if (doceditid[j] != null)
                                    {
                                        if (DocumentsData[i].ID == doceditid[j].ToString())
                                        {
                                            var InEInfo = new Parameter_Update_Attachment();
                                            InEInfo.FileName = DocumentsData[i].File_Name;
                                            InEInfo.Description = DocumentsData[i].Remarks;
                                            InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                                            InEInfo.Ref_Screen = "Production";
                                            InEInfo.Ref_Rec_ID = model.Prod_ID.ToString();
                                            InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                                            InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                                            InEInfo.File = DocumentsData[i].File_Array;
                                            InEInfo.RecID = DocumentsData[i].ID;
                                            updateattachment[j] = InEInfo;
                                        }
                                    }
                                }
                            }
                            UpParam.Updated_Attachments = updateattachment;
                        }
                    }
                    UpParam.Added_Attachments = insertAttachments;
                   
                }
            }
            if (actionmethod == "Edit")
            {
                if (ProductionDelete_Document_ID != null)
                {
                    string[] res = (string[])(ProductionDelete_Document_ID as ArrayList).ToArray(typeof(string));
                    UpParam.Deleted_Attachment_IDs = res;
                }
                if (ProductionUnlink_Document_ID != null)
                {
                    string[] res = (string[])(ProductionUnlink_Document_ID as ArrayList).ToArray(typeof(string));
                    UpParam.Unlinked_Attachment_IDs = res;
                }
            }
            }
        public void IUDExpenses(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, string actionmethod)
        {
            var index = 0;
            var ExpensesData = (List<Return_GetProdExpensesIncurred>)Grid_Data_Actual_Expenses_Data_Production;
            if (ExpensesData != null)
            {
                var InsertExpenses = new List<Param_Insert_Production_Expenses>();//[ExpensesData.Count()];
                for (int i = 0; i < ExpensesData.Count; i++)
                {
                    if (ExpensesData[i].ID == 0)
                    {
                        var InEInfo = new Param_Insert_Production_Expenses();
                        InEInfo.Exp_Tr_Sr_No = ExpensesData[i].Exp_Tr_Sr_No;
                        InEInfo.Exp_Tr_ID = ExpensesData[i].Exp_Tr_ID;
                        InsertExpenses.Add(InEInfo);
                        index++;
                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Expenses_Incurred = InsertExpenses.ToArray();
                }
                if (actionmethod == "Edit")
                {
                    UpParam.Added_Expenses_Incurred = InsertExpenses.ToArray();
                }
            }
            if (actionmethod == "Edit")
            {
                if (Delete_Expense_prod != null)
                    {
                        int[] res = (int[])(Delete_Expense_prod as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Expenses_Incurred_IDs = res;
                    }
                }
            }    
        public void IUDItemConsumed(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, string actionmethod)
        {
            var StkConData = (List<Return_GetProdItemsConsumed>)Production_StockConsumedData;
            var insertindex = 0;
            if (StkConData != null)
            {
                var insertStkcon = new Param_Insert_Production_ItemsConsumed[StkConData.Count()];
                for (int i = 0; i < StkConData.Count; i++)
                {
                    if (StkConData[i].consumptionID == 0)
                    {
                        var InEInfo = new Param_Insert_Production_ItemsConsumed();
                        InEInfo.Stock_ID = StkConData[i].StockID;
                        InEInfo.Item_Qty = StkConData[i].Consumed_Qty;
                        InEInfo.Item_Amount = StkConData[i].Amount;
                        InEInfo.Remarks = StkConData[i].Remarks;
                        insertStkcon[insertindex] = InEInfo;
                        insertindex++;
                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Items_Consumed = insertStkcon;
                }
                if (actionmethod == "Edit")
                {
                    if (ProductionEdit_StockCon_ID != null)
                    {
                        int[] StkConEditID = (int[])(ProductionEdit_StockCon_ID as ArrayList).ToArray(typeof(int));
                        if (StkConEditID != null)
                        {
                            var updateStkcon = new Param_Update_Production_ItemsConsumed[StkConEditID.Count()];
                            for (int j = 0; j < StkConEditID.Count(); j++)
                            {
                                for (int i = 0; i < StkConData.Count; i++)
                                {
                                    if (StkConData[i].consumptionID == StkConEditID[j])
                                    {
                                        var InEInfo = new Param_Update_Production_ItemsConsumed();
                                        InEInfo.Stock_ID = StkConData[i].StockID;
                                        InEInfo.Item_Qty = StkConData[i].Consumed_Qty;
                                        InEInfo.Item_Amount = StkConData[i].Amount;
                                        InEInfo.Remarks = StkConData[i].Remarks;
                                        InEInfo.ID = StkConData[i].consumptionID;
                                        updateStkcon[j] = InEInfo;
                                    }
                                }
                            }
                            UpParam.Updated_Items_Consumed = updateStkcon;
                        }
                    }
                    UpParam.Added_Items_Consumed = insertStkcon;
                }
            }
            if (actionmethod == "Edit")
            {
                if (Delete_StockCon_Prod != null)
                    {
                        int[] res = (int[])(Delete_StockCon_Prod as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Items_Consumed_IDs = res;
                    }
                }
            }      
        public void IUDItemProduce(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, Model_NEVD_Production model)
        {
            var StkProddata = (List<Return_GetProdItemProduced>)Production_StockProducedData;
            var insertindex = 0;
            if (StkProddata != null)
            {
                var insertStkProd = new Param_Insert_Production_ItemsProduced[StkProddata.Count()];
                for (int i = 0; i < StkProddata.Count; i++)
                {
                    if (StkProddata[i].ID == 0)
                    {
                        var InEInfo = new Param_Insert_Production_ItemsProduced();
                        InEInfo.sub_Item_ID = StkProddata[i].ProducedItemID;
                        InEInfo.stock_ID = StkProddata[i].StockID;
                        InEInfo.Item_Qty_Produced = StkProddata[i].Qty_Produced;
                        InEInfo.Item_Qty_Accepted = Convert.ToDecimal(StkProddata[i].Qty_Accepted);
                        InEInfo.Item_Market_Rate = StkProddata[i].MarketRate;
                        InEInfo.Item_Market_Price = StkProddata[i].MarketPrice;
                        InEInfo.Remarks = StkProddata[i].Remarks;
                        InEInfo.TotalValue_Perc = StkProddata[i].TotalValue_Perc;
                        InEInfo.Store_ID = Convert.ToInt32(model.ProductionStoreID);
                        InEInfo.Lot_Serial_no = model.ProductionLotNumber;
                        InEInfo.Make = "Production-item";
                        InEInfo.Model = "Production-item";
                        InEInfo.Warranty = "";
                        InEInfo.Unit_ID = StkProddata[i].UnitID;
                        InEInfo.Value = StkProddata[i].TotalValue;
                        insertStkProd[insertindex] = InEInfo;
                        insertindex++;
                    }
                }
                if (model.ActionMethod == "New")
                {
                    InParam.Items_Produced = insertStkProd;
                }
                if (model.ActionMethod == "Edit")
                {
                    if (ProductionEdit_StockProd_ID != null)
                    {
                        int[] StkProdEditID = (int[])(ProductionEdit_StockProd_ID as ArrayList).ToArray(typeof(int));

                        if (StkProdEditID != null)
                        {
                            var editStkProd = new Param_Update_Production_ItemsProduced[StkProdEditID.Count()];
                            for (int j = 0; j < StkProdEditID.Count(); j++)
                            {
                                for (int i = 0; i < StkProddata.Count; i++)
                                {
                                    if (StkProddata[i].ID == StkProdEditID[j])
                                    {
                                        var InEInfo = new Param_Update_Production_ItemsProduced();
                                        InEInfo.sub_Item_ID = StkProddata[i].ProducedItemID;
                                        InEInfo.stock_ID = StkProddata[i].StockID;
                                        InEInfo.Item_Qty_Produced = StkProddata[i].Qty_Produced;
                                        InEInfo.Item_Qty_Accepted = Convert.ToDecimal(StkProddata[i].Qty_Accepted);
                                        InEInfo.Item_Market_Rate = StkProddata[i].MarketRate;
                                        InEInfo.Item_Market_Price = StkProddata[i].MarketPrice;
                                        InEInfo.Remarks = StkProddata[i].Remarks;
                                        InEInfo.TotalValue_Perc = StkProddata[i].TotalValue_Perc;
                                        InEInfo.Store_ID = Convert.ToInt32(model.ProductionStoreID);
                                        InEInfo.Lot_Serial_no = model.ProductionLotNumber;
                                        InEInfo.Make = "Production-item";
                                        InEInfo.Model = "Production-item";
                                        InEInfo.Warranty = "";
                                        InEInfo.Unit_ID = StkProddata[i].UnitID;
                                        InEInfo.Value = StkProddata[i].TotalValue;
                                        InEInfo.ID = StkProddata[i].ID;
                                        editStkProd[j] = InEInfo;
                                    }
                                }
                            }
                            UpParam.Updated_Items_Produced = editStkProd;
                        }
                    }
                    UpParam.Added_Items_Produced = insertStkProd;
                }
            }
            if (model.ActionMethod == "Edit")
            {
                if (Delete_Stock_Prod != null)
                    {
                        int[] res = (int[])(Delete_Stock_Prod as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Items_Produced_IDs = res;
                    }
                }
            }      
        public void IUDMachineUsage(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, string actionmethod)
        {
            var all_data_Of_MachineUsage_Grid = (List<Return_GetProdMachineUsage>)Production_Machine_Usage_Window_Grid_Data;
            var index = 0;
            if (all_data_Of_MachineUsage_Grid != null)
            {
                var insertMachineUsage = new Param_Insert_Production_MachineUsage[all_data_Of_MachineUsage_Grid.Count()];
                for (int i = 0; i < all_data_Of_MachineUsage_Grid.Count; i++)
                {
                    if (all_data_Of_MachineUsage_Grid[i].REC_ID == 0)
                    {
                        var InMachineUsageInfo = new Param_Insert_Production_MachineUsage();
                        InMachineUsageInfo.Machine_ID = all_data_Of_MachineUsage_Grid[i].Machine_ID;
                        InMachineUsageInfo.Machine_Count = all_data_Of_MachineUsage_Grid[i].Mch_Count;
                        InMachineUsageInfo.Machine_Usage = Convert.ToDecimal(all_data_Of_MachineUsage_Grid[i].Usage_in_Hrs);
                        InMachineUsageInfo.Machine_Remarks = all_data_Of_MachineUsage_Grid[i].Remarks;
                        insertMachineUsage[index] = InMachineUsageInfo;
                        index++;
                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Machine_Usage = insertMachineUsage;
                }
                if (actionmethod == "Edit")
                {
                    if (ProductionEdit_Machine_ID != null)
                    {
                        int[] machineeditID = (int[])(ProductionEdit_Machine_ID as ArrayList).ToArray(typeof(int));

                        if (machineeditID != null)
                        {
                            var updateMachineUsage = new Param_Update_Production_MachineUsage[machineeditID.Count()];
                            for (int j = 0; j < machineeditID.Count(); j++)
                            {
                                for (int i = 0; i < all_data_Of_MachineUsage_Grid.Count; i++)
                                {
                                    if (machineeditID[j] == all_data_Of_MachineUsage_Grid[i].REC_ID)
                                    {
                                        var InMachineUsageInfo = new Param_Update_Production_MachineUsage();
                                        InMachineUsageInfo.Machine_ID = all_data_Of_MachineUsage_Grid[i].Machine_ID;
                                        InMachineUsageInfo.Machine_Count = all_data_Of_MachineUsage_Grid[i].Mch_Count;
                                        InMachineUsageInfo.Machine_Usage = Convert.ToDecimal(all_data_Of_MachineUsage_Grid[i].Usage_in_Hrs);
                                        InMachineUsageInfo.Machine_Remarks = all_data_Of_MachineUsage_Grid[i].Remarks;
                                        InMachineUsageInfo.ID = all_data_Of_MachineUsage_Grid[i].REC_ID;
                                        updateMachineUsage[j] = InMachineUsageInfo;
                                    }
                                }
                            }
                            UpParam.Updated_Machine_Usage = updateMachineUsage;
                        }
                    }
                    UpParam.Added_Machine_Usage = insertMachineUsage;
                }
            }
            if (actionmethod == "Edit")
            {
                if (ProductionDelete_MachineUsage_ID != null)
                    {
                        int[] res = (int[])(ProductionDelete_MachineUsage_ID as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Machine_Usage_IDs = res;
                    }
                }
            }     
        public void IUDManpowerUsage(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, string actionmethod)
        {
            var all_data_Of_ManpowerUsage = (List<Return_GetProdManpowerUsage>)Grid_Data_Actual_Manpower_Usage_Data_Production;
            var index = 0;
            if (all_data_Of_ManpowerUsage != null)
            {
                var insertManpowerUsage = new Param_Insert_Production_ManpowerUsage[all_data_Of_ManpowerUsage.Count()];
                for (int I = 0; I < all_data_Of_ManpowerUsage.Count; I++)
                {
                    if (all_data_Of_ManpowerUsage[I].ID == 0)
                    {
                        var InManpowerUsageInfo = new Param_Insert_Production_ManpowerUsage();
                        InManpowerUsageInfo.Person_ID = Convert.ToInt32(all_data_Of_ManpowerUsage[I].ManpowerID);
                        InManpowerUsageInfo.Period_From = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].W_PeriodFrom);
                        InManpowerUsageInfo.Period_To = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].W_PeriodTo);
                        InManpowerUsageInfo.Units_Worked = all_data_Of_ManpowerUsage[I].Units_Worked;
                        InManpowerUsageInfo.Total_Amount = all_data_Of_ManpowerUsage[I].TotalCost;
                        InManpowerUsageInfo.Remarks = all_data_Of_ManpowerUsage[I].Remarks;
                        InManpowerUsageInfo.Charge_ID = all_data_Of_ManpowerUsage[I].ManpowerChargesID;
                        insertManpowerUsage[index] = InManpowerUsageInfo;
                        index++;
                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Manpower_Usage = insertManpowerUsage;
                }
                if (actionmethod == "Edit")
                {
                    if (ProductionEdit_Manpower_ID != null)
                    {
                        int[] manpowereditid = (int[])(ProductionEdit_Manpower_ID as ArrayList).ToArray(typeof(int));

                        if (manpowereditid != null)
                        {
                            var editmanpower = new Param_Update_Production_ManpowerUsage[manpowereditid.Count()];
                            for (int j = 0; j < manpowereditid.Count(); j++)
                            {
                                for (int I = 0; I < all_data_Of_ManpowerUsage.Count; I++)
                                {
                                    if (all_data_Of_ManpowerUsage[I].ID == manpowereditid[j])
                                    {
                                        var InManpowerUsageInfo = new Param_Update_Production_ManpowerUsage();
                                        InManpowerUsageInfo.Person_ID = Convert.ToInt32(all_data_Of_ManpowerUsage[I].ManpowerID);
                                        InManpowerUsageInfo.Period_From = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].W_PeriodFrom);
                                        InManpowerUsageInfo.Period_To = Convert.ToDateTime(all_data_Of_ManpowerUsage[I].W_PeriodTo);
                                        InManpowerUsageInfo.Units_Worked = all_data_Of_ManpowerUsage[I].Units_Worked;
                                        InManpowerUsageInfo.Total_Amount = all_data_Of_ManpowerUsage[I].TotalCost;
                                        InManpowerUsageInfo.Remarks = all_data_Of_ManpowerUsage[I].Remarks;
                                        InManpowerUsageInfo.ID = all_data_Of_ManpowerUsage[I].ID;
                                        editmanpower[j] = InManpowerUsageInfo;
                                    }
                                }
                            }
                            UpParam.Updated_Manpower_Usage = editmanpower;
                        }
                    }
                    UpParam.Added_Manpower_Usage = insertManpowerUsage;
                }
            }
            if (actionmethod == "Edit")
            {
                if (Delete_Actual_Manpower_Usage_Data_Production != null)
                    {
                        int[] res = (int[])(Delete_Actual_Manpower_Usage_Data_Production as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Manpower_Usage_IDs = res;
                    }
                }
            }
        public void IUDScrapProduced(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, Model_NEVD_Production model)
        {
            var scrapcreatedData = (List<Return_GetProdScrapProduced>)Production_ScrapCreatedData;
            var index = 0;
            if (scrapcreatedData != null)
            {
                var insertscrapcreated = new Param_Insert_Production_ScrapProduced[scrapcreatedData.Count()];
                for (int i = 0; i < scrapcreatedData.Count; i++)
                {
                    if (scrapcreatedData[i].ID == 0)
                    {
                        var InEInfo = new Param_Insert_Production_ScrapProduced();
                        InEInfo.sub_Item_ID = scrapcreatedData[i].ItemID;
                        InEInfo.Store_ID = Convert.ToInt32(model.ProductionStoreID);
                        InEInfo.Lot_Serial_no = model.ProductionLotNumber;
                        InEInfo.Make = "Production-scrap";
                        InEInfo.Model = "Production-scrap";
                        InEInfo.Warranty = "";
                        InEInfo.Unit_ID = scrapcreatedData[i].UnitID;
                        InEInfo.Value = scrapcreatedData[i].Amount;
                        InEInfo.Qty = scrapcreatedData[i].Qty;
                        InEInfo.Remarks = scrapcreatedData[i].Remarks;
                        insertscrapcreated[index] = InEInfo;
                        index++;
                    }
                }
                if (model.ActionMethod == "New")
                {
                    InParam.Scrap_Produced = insertscrapcreated;
                }
                if (model.ActionMethod == "Edit")
                {
                    if (ProductionEdit_Scrap_ID != null)
                    {
                        int[] scrapeditid = (int[])(ProductionEdit_Scrap_ID as ArrayList).ToArray(typeof(int));
                        if (scrapeditid != null)
                        {
                            var updatescrapcreated = new Param_Update_Production_ScrapProduced[scrapeditid.Count()];
                            for (int j = 0; j < scrapeditid.Count(); j++)
                            {
                                for (int i = 0; i < scrapcreatedData.Count; i++)
                                {
                                    if (scrapcreatedData[i].ID == scrapeditid[j])
                                    {
                                        var InEInfo = new Param_Update_Production_ScrapProduced();
                                        InEInfo.sub_Item_ID = scrapcreatedData[i].ItemID;
                                        InEInfo.Store_ID = Convert.ToInt32(model.ProductionStoreID);
                                        InEInfo.Lot_Serial_no = model.ProductionLotNumber;
                                        InEInfo.Make = "Production-scrap";
                                        InEInfo.Model = "Production-scrap";
                                        InEInfo.Warranty = "";
                                        InEInfo.Unit_ID = scrapcreatedData[i].UnitID;
                                        InEInfo.Value = scrapcreatedData[i].Amount;
                                        InEInfo.Qty = scrapcreatedData[i].Qty;
                                        InEInfo.Remarks = scrapcreatedData[i].Remarks;
                                        InEInfo.ID = scrapcreatedData[i].ID;
                                        updatescrapcreated[j] = InEInfo;
                                    }
                                }
                            }
                            UpParam.Updated_Scrap_Produced = updatescrapcreated;
                        }
                    }
           
         
                UpParam.Added_Scrap_Produced = insertscrapcreated;
                    }
                }
            if (model.ActionMethod == "Edit")
            {
                if (Delete_Scrap_Prod != null)
                    {
                        int[] res = (int[])(Delete_Scrap_Prod as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Scrap_Produced_IDs = res;
                    }
                }
            }
        public void IUDRemainingData(ref Param_Insert_Production_Txn InParam, ref Param_Update_Production_Txn UpParam, Model_NEVD_Production model)
        {
            if (model != null && model.ActionMethod == "New")
            {
                InParam.Prod_Date = Convert.ToDateTime(model.ProductionDate);
                InParam.Location_ID = model.ProductionLocationID;
                InParam.Lot_no = model.ProductionLotNumber;
                InParam.Project_ID = model.ProductionProjectNameID;
                InParam.Worked_By = model.ProductionDoneBy;
                InParam.FromDate = model.ProductionFromDate;
                InParam.ToDate = model.ProductionToDate;
                InParam.Remarks = model.ProductionRemarks;
                InParam.StoreID = (Int32)model.ProductionStoreID ;
            }
            else
            {
                UpParam.ProdID = model.Prod_ID;
                UpParam.Prod_Date = Convert.ToDateTime(model.ProductionDate);
                UpParam.Location_ID = model.ProductionLocationID;
                UpParam.Lot_no = model.ProductionLotNumber;
                UpParam.Project_ID = model.ProductionProjectNameID;
                UpParam.Worked_By = model.ProductionDoneBy;
                UpParam.FromDate = model.ProductionFromDate;
                UpParam.ToDate = model.ProductionToDate;
                UpParam.Remarks = model.ProductionRemarks;
                UpParam.StoreID = (Int32)model.ProductionStoreID;
                if (Delete_ProductionExisting_Remarks_ID != null)
                {
                    //int[] res = (int[])(Delete_ProductionExisting_Remarks_ID as ArrayList).ToArray(typeof(int));
                    int[] res = ((List<int>)Delete_ProductionExisting_Remarks_ID).ToArray();//0000076 bug fixed
                    UpParam.Deleted_Remarks_IDs = res;
                }
            }
        }
        #endregion
        public ActionResult CheckDocumentsLinked(int prodid)
        {
            var docdata = Production_Documents_Window_Grid_Data as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._Stock_Production_DBOps.GetAttachmentLinkScreen(prodid, docdata[i].ID);
                    if (!string.IsNullOrEmpty(screen))
                    {
                        return Json(new
                        {
                            result = true,
                            message = "There Are Documents That Cannot Be Deleted Because They Have Been Linked To Other Screens</br> Do You Want To Unlink It From Production..? "
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
        #endregion
        #region "DropDown"
        public ActionResult Get_ProductionStore_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetStoreList> Store_List = BASE._StockDeptStores_dbops.GetStoreList();
            Store_List.Insert(0, new Return_GetStoreList { Store_Name = "Please Select Store Name...", Sub_Dept_Name = "", Dept_Name = "", Dept_Incharge_Name = "", StoreID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Store_List, loadOptions)));
        }
        public ActionResult Get_ProductionLocation_List(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockProfile.Return_GetLocations>(), loadOptions)));
            }
            List<DbOperations.StockProfile.Return_GetLocations> Location_List = BASE._Stock_Profile_DBOps.GetLocations(Convert.ToInt32(storeid));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Location_List, loadOptions)));
        }
        public ActionResult Get_ProductionProject_List(DataSourceLoadOptions loadOptions)
        {
            List<Projects.Return_GetOpenProjectsList> Project_List = BASE._Projects_Dbops.GetOpenProjectsList();            
            Project_List.Insert(0, new DbOperations.Projects.Return_GetOpenProjectsList { Project_Name = "Please Select Project...", Project_Id = 0, Complex_Name = "", Complex_Id = "", Sanction_Date = null, Sanction_no = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Project_List, loadOptions)));
        }
        public ActionResult Get_ProductionDoneBy_List(DataSourceLoadOptions loadOptions)
        {
            var data = new List<SelectListItem>();
            data.AddRange(new List<SelectListItem>() { new SelectListItem { Value = "Department", Text = "Department" },
                                                       new SelectListItem { Value = "Contractor", Text = "Contractor" },
                                                       new SelectListItem { Value = "Both", Text = "Both" },
                                                       new SelectListItem { Value="",Text=""} });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)));
        }
        #endregion
        #region "Inside Grids"
        #region "Documents"
        public ActionResult ProductionDocumentsGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var docList = new List<Return_GetDocumentsGridData>();
            if (ActionMethodName == "New")
            {
                return PartialView(docList);
            }
            if (Production_Documents_Window_Grid_Data == null)
            {
                var docData = BASE._Stock_Production_DBOps.GetProdDocuments(Prod_ID);
                if (docData != null)
                {
                    docList = docData;
                }
                Production_Documents_Window_Grid_Data = docList;
            }

            return PartialView(Production_Documents_Window_Grid_Data);
        }
        public ActionResult Production_Documents_Attachment()
        {
            Model_Attachment_Window model = (Model_Attachment_Window)GetBaseSession("Production_Documents_Attachment_AttachmentData");
            List<Return_GetDocumentsGridData> gridRows = new List<Return_GetDocumentsGridData>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Production_Documents_Window_Grid_Data != null)
            {
                gridRows = (List<Return_GetDocumentsGridData>)Production_Documents_Window_Grid_Data;
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
                    var editDocument = ProductionEdit_Document_ID as ArrayList;
                    if (editDocument != null)
                    {
                        editDocument.Add(model.ID);
                        ProductionEdit_Document_ID = editDocument;
                    }
                    else
                    {
                        editDocumentID.Add(model.ID);
                        ProductionEdit_Document_ID = editDocumentID;
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
            Production_Documents_Window_Grid_Data = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Production_Documents_Attachment_LinkCheck(string DocId, int ProdId)
        {
            var screen = BASE._Stock_Production_DBOps.GetAttachmentLinkScreen(ProdId, DocId);
            if (!string.IsNullOrEmpty(screen))
            {
                return Json(new
                {
                    result = false,
                    message = "This Document Cannot Be Deleted Because It Has been Attached To "+screen+".Do You Want To Unlink It From Production..? "
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
        public ActionResult Frm_DocumentsDetail_Window_Delete_Grid_Record(string ActionMethod, string SrID = null, string Doc_ID = null)
        {
            var srid = Convert.ToInt32(SrID);
            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)Production_Documents_Window_Grid_Data;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (dataToDelete != null)
            {
                allDocData.Remove(dataToDelete);
            }
            Production_Documents_Window_Grid_Data = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    var deleteDocumentID = new ArrayList();
                    var deleteDocument = ProductionDelete_Document_ID as ArrayList;
                    if (deleteDocument != null)
                    {
                        deleteDocument.Add(Doc_ID);
                        ProductionDelete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID.Add(Doc_ID);
                        ProductionDelete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {                
               
                    var unlinkDocumentID = new ArrayList();
                    var unlinkDocument = ProductionUnlink_Document_ID as ArrayList;
                    if (unlinkDocument != null)
                    {
                        unlinkDocument.Add(Doc_ID);
                        ProductionUnlink_Document_ID = unlinkDocument;
                    }
                    else
                    {
                        unlinkDocumentID.Add(Doc_ID);
                        ProductionUnlink_Document_ID = unlinkDocumentID;
                    }
                }
            
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Expenses"
        public ActionResult ProductionExpensesGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var Actual_Expenses_List = new List<Return_GetProdExpensesIncurred>();
            if (ActionMethodName == "New")
            {
                return PartialView(Actual_Expenses_List);
            }
            if (Grid_Data_Actual_Expenses_Data_Production == null)
            {
                var Actual_Expenses_Data = BASE._Stock_Production_DBOps.GetProdExpensesIncurred(Prod_ID);
                if (Actual_Expenses_Data != null)
                {
                    Actual_Expenses_List = Actual_Expenses_Data;
                }
                Grid_Data_Actual_Expenses_Data_Production = Actual_Expenses_List;
            }
            return PartialView(Grid_Data_Actual_Expenses_Data_Production);
        }
        public ActionResult Frm_Production_Expense_Details(int Prod_ID)
        {
            ViewData["Production_AddAccVouPaymentRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            var expensemappinglist = new List<Return_Get_Prod_Expenses_For_Mapping>();
            var data = BASE._Stock_Production_DBOps.Get_Prod_Expenses_For_Mapping(Prod_ID);
            if (data != null)
            {
                expensemappinglist = data;
            }
            ProductionActuallExpensions = expensemappinglist;
            return View(ProductionActuallExpensions);
        }
        public ActionResult Prod_Actual_Estimation_Grid(string command, int Prod_ID)
        {
            if (command == "REFRESH")
            {
                var data = BASE._Stock_Production_DBOps.Get_Prod_Expenses_For_Mapping(Prod_ID);
                ProductionActuallExpensions = data;
            }
            var finalData = ProductionActuallExpensions as List<Return_Get_Prod_Expenses_For_Mapping>;

            return PartialView(finalData);
        }
        public ActionResult FindGridKeyValue()
        {
            var griddata = Grid_Data_Actual_Expenses_Data_Production as List<Return_GetProdExpensesIncurred>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = griddata[i].Exp_Tr_ID;

                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult Addingnewexpenses(string[] selectedrowarr)
        {
            if (selectedrowarr != null)
            {
                var Final_Data = Grid_Data_Actual_Expenses_Data_Production as List<Return_GetProdExpensesIncurred>;
                for (int i = 0; i < Final_Data.Count; i++)
                {

                    if (Final_Data[i].ID != 0)
                    {
                        var deleteItemEstID = new ArrayList();
                        var deleteItemEst = Delete_Expense_prod as ArrayList;
                        if (deleteItemEst != null)
                        {
                            deleteItemEst.Add(Final_Data[i].ID);
                            Delete_Expense_prod = deleteItemEst;
                        }
                        else
                        {
                            deleteItemEstID.Add(Final_Data[i].ID);
                            Delete_Expense_prod = deleteItemEstID;
                        }
                    }
                }//Mantis bug 0000975 fixed

                var mappinggriddata = ProductionActuallExpensions as List<Return_Get_Prod_Expenses_For_Mapping>;
                List<Return_GetProdExpensesIncurred> newrow = new List<Return_GetProdExpensesIncurred>();
                for (int i = 0; i < selectedrowarr.Length; i++)
                {
                    var NewSr = i + 1;
                    Return_Get_Prod_Expenses_For_Mapping mappedrow = mappinggriddata.Find(x => x.Txn_ID == selectedrowarr[i].ToString());
                    Return_GetProdExpensesIncurred data = new Return_GetProdExpensesIncurred();
                    data.ExpDate = mappedrow._Date;
                    data.ItemName = mappedrow.ItemName;
                    data.Head = mappedrow.Head;
                    data.Party = mappedrow.Party;
                    data.Amount = mappedrow.Amount;
                    data.Exp_Tr_ID = mappedrow.Txn_ID;
                    data.Exp_Tr_Sr_No = mappedrow.Txn_Sr_No;
                    data.AddedBy = BASE._open_User_ID;
                    data.AddedOn = DateTime.Now;
                    data.Sr = NewSr;
                    newrow.Add(data);
                }
                Grid_Data_Actual_Expenses_Data_Production = newrow;
                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Grid_Data_Actual_Expenses_Data_Production = null;
                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult IncurredDelete(int SR, int id = 0)
        {
            var Final_Data = Grid_Data_Actual_Expenses_Data_Production as List<Return_GetProdExpensesIncurred>;
            var onlyMatch = Final_Data.Single(s => s.Sr == SR);
            Final_Data.Remove(onlyMatch);
            Grid_Data_Actual_Expenses_Data_Production = Final_Data;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_Expense_prod as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_Expense_prod = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_Expense_prod = deleteItemEstID;
                }
            }
            return Json(new { result = true, message = "Deleted Successfully", }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Machine Usage"
        public ActionResult ProductionMachineUsageGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var MachineList = new List<Return_GetProdMachineUsage>();
            if (ActionMethodName == "New")
            {
                return PartialView(MachineList);
            }
            if (Production_Machine_Usage_Window_Grid_Data == null)
            {
                var MachineUsageData = BASE._Stock_Production_DBOps.GetProdMachineUsage(Prod_ID);
                if (MachineUsageData != null)
                {
                    MachineList = MachineUsageData;
                }
                Production_Machine_Usage_Window_Grid_Data = MachineList;
            }
            return PartialView(Production_Machine_Usage_Window_Grid_Data);
        }
        public ActionResult Frm_Stock_Production_Machinery_Used(string ActionMethod = null, string SrID = null)
        {
            ProductionMachineUsageDetail model = new ProductionMachineUsageDetail();

            model.ActionMethod = ActionMethod;
            if (ActionMethod == "New")
            {
                model.Production_Machine_Count = 1;

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Return_GetProdMachineUsage>)Production_Machine_Usage_Window_Grid_Data;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdMachineUsage();
                model.Sr_No = Sr;
                model.Production_Machine_Name = dataToEdit != null ? dataToEdit.Machine_Name.ToString() : "";
                model.Production_Machine_Count = dataToEdit.Mch_Count;
                model.Production_Machine_Usage_Hours = Convert.ToDouble(dataToEdit.Usage_in_Hrs);
                model.Production_Machine_Remarks = dataToEdit != null ? dataToEdit.Remarks : "";
                model.Production_Machine_NameID = dataToEdit.Machine_ID;
                model.Production_Machine_No = dataToEdit.Machine_No;
                model.ID = dataToEdit.REC_ID;
            }
            return View(model);
        }
        public ActionResult Frm_Machinery_Used(ProductionMachineUsageDetail model)
        {
            var actionmethod = model.ActionMethod;
            if (actionmethod == "New" || actionmethod == "Edit")
            {
                if (Convert.ToDouble(model.Production_Machine_Count) > Convert.ToDouble(model.Curr_Qty))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Machine Count Cannot Be Greater Than Total Qty of Machine selected..."
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            List<Return_GetProdMachineUsage> gridRows = new List<Return_GetProdMachineUsage>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Production_Machine_Usage_Window_Grid_Data != null)
            {
                gridRows = (List<Return_GetProdMachineUsage>)Production_Machine_Usage_Window_Grid_Data;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetProdMachineUsage grid = new Return_GetProdMachineUsage();

                grid.Sr = NewSr;
                grid.Machine_Name = model.Production_Machine_Name;
                grid.Machine_No = model.Production_Machine_No;
                grid.Mch_Count = Convert.ToInt32(model.Production_Machine_Count);
                grid.Usage_in_Hrs = Convert.ToDecimal(model.Production_Machine_Usage_Hours);
                grid.Remarks = model.Production_Machine_Remarks;
                grid.Machine_ID = Convert.ToInt32(model.Production_Machine_NameID);
                grid.REC_ADD_BY = BASE._open_User_ID;
                grid.REC_ADD_ON = DateTime.Now;
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr_No);
                dataToEdit.Machine_Name = model.Production_Machine_Name;
                dataToEdit.Machine_No = model.Production_Machine_No;
                dataToEdit.Mch_Count = Convert.ToInt32(model.Production_Machine_Count);
                dataToEdit.Usage_in_Hrs = Convert.ToDecimal(model.Production_Machine_Usage_Hours);
                dataToEdit.Remarks = model.Production_Machine_Remarks;
                dataToEdit.Machine_ID = Convert.ToInt32(model.Production_Machine_NameID);
                if (model.ID != 0)
                {
                    var editStockConID = new ArrayList();
                    var editStockCon = ProductionEdit_Machine_ID as ArrayList;
                    if (editStockCon != null)
                    {
                        editStockCon.Add(model.ID);
                        ProductionEdit_Machine_ID = editStockCon;
                    }
                    else
                    {
                        editStockConID.Add(model.ID);
                        ProductionEdit_Machine_ID = editStockConID;
                    }
                }
            }
            Production_Machine_Usage_Window_Grid_Data = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_MachineUsageDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null, int Rec_Id = 0)
        {
            var Sr = Convert.ToInt16(SrID);
            var allMachineData = (List<Return_GetProdMachineUsage>)Production_Machine_Usage_Window_Grid_Data;
            var dataToDelete = allMachineData != null ? allMachineData.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdMachineUsage();
            if (allMachineData != null)
            {
                allMachineData.Remove(dataToDelete);
            }
            Production_Machine_Usage_Window_Grid_Data = allMachineData;
            var deleteMachineID = new ArrayList();
            if (Rec_Id != 0)
            {
                var deleteMachine = ProductionDelete_MachineUsage_ID as ArrayList;
                if (deleteMachine != null)
                {
                    deleteMachine.Add(Rec_Id);
                    ProductionDelete_MachineUsage_ID = deleteMachine;
                }
                else
                {
                    deleteMachineID.Add(Rec_Id);
                    ProductionDelete_MachineUsage_ID = deleteMachineID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetMachine_Name(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_Get_Stocks_Listing>(), loadOptions)), "application/json");
            }
            List<Return_Get_Stocks_Listing> MachineData = BASE._Stock_Production_DBOps.GetCurrMachines(Convert.ToInt32(storeid));
           
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MachineData, loadOptions)), "application/json");
        }
        #endregion
        #region "Manpower Usage"
        public ActionResult ProductionActual_Manpower_Usage_GridData(string ActionMethodName, int Prod_ID = 0)
        {
            var Actual_Manpower_List = new List<Return_GetProdManpowerUsage>();
            if (ActionMethodName == "New")
            {
                return PartialView(Actual_Manpower_List);
            }
            if (Grid_Data_Actual_Manpower_Usage_Data_Production == null)
            {
                var Actual_Manpower_List_Data = BASE._Stock_Production_DBOps.GetProdManpowerUsage(Prod_ID);
                if (Actual_Manpower_List_Data != null)
                {
                    Actual_Manpower_List = Actual_Manpower_List_Data;
                }
                Grid_Data_Actual_Manpower_Usage_Data_Production = Actual_Manpower_List;
            }

            return PartialView(Grid_Data_Actual_Manpower_Usage_Data_Production);
        }
        public ActionResult Frm_Add_Manpower_Usage(int SR = 0, string ActionMethod = null)
        {
            ViewData["Production_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ProductionActualManpowerUsage model = new ProductionActualManpowerUsage();
            model.ActionMethod = ActionMethod;           
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetProdManpowerUsage>)Grid_Data_Actual_Manpower_Usage_Data_Production;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdManpowerUsage();
                model.sr = Sr;
                model.ID = dataToEdit.ID;
                model.Production_PersonName = dataToEdit.PersonName != null ? dataToEdit.PersonName : "";
                model.Production_W_PeriodFrom = dataToEdit.W_PeriodFrom;
                model.Production_W_PeriodTo = dataToEdit.W_PeriodTo;
                model.Production_ManpowerUnits = dataToEdit.Units;
                model.Production_RatePerUnit = Convert.ToDouble(dataToEdit.RatePerUnit);
                model.Production_Units_Worked = Convert.ToDouble(dataToEdit.Units_Worked);
                model.Production_TotalCost = Convert.ToDouble(dataToEdit.TotalCost);
                model.Production_ManpowerRemarks = dataToEdit.Remarks != null ? dataToEdit.Remarks : "";
                model.Production_ManpowerID = dataToEdit.ManpowerID;
                model.Production_Manpower_ChargesID = dataToEdit.ManpowerChargesID;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Add_Manpower_Usage(ProductionActualManpowerUsage model)
        {
            var actionmethod = model.ActionMethod;    
            if (actionmethod == "Edit" || actionmethod == "New")
            {
                DateTime WorkFrom_date = Convert.ToDateTime(model.Production_W_PeriodFrom);
                DateTime WorkTo_date = Convert.ToDateTime(model.Production_W_PeriodTo);
                var CheckRates = BASE._Personnels_Dbops.GetPersonnelCharges(Convert.ToInt32(model.Production_ManpowerID), WorkFrom_date, WorkTo_date);

                if (model.Production_Units_Worked <= 0)
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
                if (model.PM_Joined_Date > WorkFrom_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Work Period From Date can not be Less than Joined Date " + Convert.ToDateTime(model.PM_Joined_Date).ToString("dd/MM/yyyy") + " of Person </br> Please Select Greater than this " + Convert.ToDateTime(model.PM_Joined_Date).ToString("dd/MM/yyyy")
                    }, JsonRequestBehavior.AllowGet);
                }//Mantis bug 0001045 resolved
                if (model.PM_Leave_Date <= WorkTo_date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Work Period To Date can not be greater than Leaving Date " + Convert.ToDateTime(model.PM_Leave_Date).ToString("dd/MM/yyyy") + " of Person...!"
                    }, JsonRequestBehavior.AllowGet);
                }//Mantis bug 0001045 resolved             
                if (CheckRates.Count > 1)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Not Allowed To Post Entry For Period Which Contains 2 Different Rates / Units...If Rate Or Unit Of Work For A User Was Updated In Between Period Of Work, User Need To Make Separate Entries For Both!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            List<Return_GetProdManpowerUsage> gridRows = new List<Return_GetProdManpowerUsage>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Grid_Data_Actual_Manpower_Usage_Data_Production != null)
            {
                gridRows = (List<Return_GetProdManpowerUsage>)Grid_Data_Actual_Manpower_Usage_Data_Production;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetProdManpowerUsage grid = new Return_GetProdManpowerUsage();

                grid.Sr = NewSr;
                grid.ManpowerID = Convert.ToInt32(model.Production_ManpowerID);
                grid.ManpowerChargesID = Convert.ToInt32(model.Production_Manpower_ChargesID);
                grid.PersonName = model.Production_PersonName;
                grid.PersonType = model.Production_PersonType;
                grid.Units_Worked = Convert.ToDecimal(model.Production_Units_Worked);
                grid.RatePerUnit = Convert.ToDecimal(model.Production_RatePerUnit);
                grid.TotalCost = Convert.ToDecimal(model.Production_TotalCost);
                grid.Units = model.Production_ManpowerUnits;
                grid.Remarks = model.Production_ManpowerRemarks;
                grid.W_PeriodFrom = Convert.ToDateTime(model.Production_W_PeriodFrom);
                grid.W_PeriodTo = Convert.ToDateTime(model.Production_W_PeriodTo);
                grid.AddedBy = BASE._open_User_ID;
                grid.AddedOn = DateTime.Now;
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.sr);

                dataToEdit.PersonName = model.Production_PersonName;
                dataToEdit.PersonType = model.Production_PersonType;
                dataToEdit.Units_Worked = Convert.ToDecimal(model.Production_Units_Worked);
                dataToEdit.RatePerUnit = Convert.ToDecimal(model.Production_RatePerUnit);
                dataToEdit.TotalCost = Convert.ToDecimal(model.Production_TotalCost);
                dataToEdit.Remarks = model.Production_ManpowerRemarks;
                dataToEdit.W_PeriodFrom = Convert.ToDateTime(model.Production_W_PeriodFrom);
                dataToEdit.W_PeriodTo = Convert.ToDateTime(model.Production_W_PeriodTo);
                dataToEdit.ManpowerID = Convert.ToInt32(model.Production_ManpowerID);
                dataToEdit.ManpowerChargesID = Convert.ToInt32(model.Production_Manpower_ChargesID);
                var editManpowerID = new ArrayList();
                if (model.ID != 0)
                {
                    var editManpower = ProductionEdit_Manpower_ID as ArrayList;
                    if (editManpower != null)
                    {
                        editManpower.Add(model.ID);
                        ProductionEdit_Manpower_ID = editManpower;
                    }
                    else
                    {
                        editManpowerID.Add(model.ID);
                        ProductionEdit_Manpower_ID = editManpowerID;
                    }
                }
            }
            Grid_Data_Actual_Manpower_Usage_Data_Production = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Manpower_Usage_Window_Delete_Grid_Record(string ActionMethod, int SR, int? Manpower_Usage_Rec_Id = 0)
        {
           
            var allData = (List<Return_GetProdManpowerUsage>)Grid_Data_Actual_Manpower_Usage_Data_Production;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetProdManpowerUsage();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Grid_Data_Actual_Manpower_Usage_Data_Production = allData;
            var deleteItemEstID = new ArrayList();
            if (Manpower_Usage_Rec_Id != 0)
            {
                var deleteItemEst = Delete_Actual_Manpower_Usage_Data_Production as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(Manpower_Usage_Rec_Id);
                    Delete_Actual_Manpower_Usage_Data_Production = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(Manpower_Usage_Rec_Id);
                    Delete_Actual_Manpower_Usage_Data_Production = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_Person_Names(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_GetPersonnels>(), loadOptions)), "application/json");
            }
            List<Return_GetPersonnels> data = BASE._Stock_Production_DBOps.GetPersonnels(Convert.ToInt32(storeid));
          
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        public ActionResult Autocalculate_Rate_per_Unit(int PersonId, DateTime? WorkFrom = null, DateTime? WorkTo = null)
        {
            DateTime WorkFrom_date = Convert.ToDateTime(WorkFrom) == null ? DateTime.Now : Convert.ToDateTime(WorkFrom);
            DateTime WorkTo_date = Convert.ToDateTime(WorkTo) == null ? DateTime.Now : Convert.ToDateTime(WorkTo);
            var list = BASE._Personnels_Dbops.GetPersonnelCharges(PersonId, WorkFrom_date, WorkTo_date);
            if (list.Count() > 0)
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

        #endregion
        #region "Remarks"
        public ActionResult ProductionExistingRemarksGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var RemarksList = new List<Return_GetProdRemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(RemarksList);
            }
            if (ProductionExisting_Remarks_Grid_Data == null)
            {
                var RemarksData = BASE._Stock_Production_DBOps.GetProdRemarks(Prod_ID);
                if (RemarksData != null)
                {
                    RemarksList = RemarksData;
                }
                ProductionExisting_Remarks_Grid_Data = RemarksList;
            }
            return PartialView(ProductionExisting_Remarks_Grid_Data);
        }
        public ActionResult Frm_ExistingRemarks_Window_Delete_Grid_Record(string ActionMethod, int? ID = 0)
        {
            var id = Convert.ToInt16(ID);
            var allData = (List<Return_GetProdRemarks>)ProductionExisting_Remarks_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == id).FirstOrDefault() : new Return_GetProdRemarks();
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
            ProductionExisting_Remarks_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new List<int>();
                var deleteExistingRemarks = Delete_ProductionExisting_Remarks_ID as List<int>;
                if (deleteExistingRemarks != null)
                {
                    deleteExistingRemarks.Add(id);
                    Delete_ProductionExisting_Remarks_ID = deleteExistingRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_ProductionExisting_Remarks_ID = deleteRemarksID;
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
            var all_data = (List<Return_GetProdRemarks>)ProductionExisting_Remarks_Grid_Data;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetProdRemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("Frm_ViewRemarks", ViewBag.ViewRemarks);
        }

        #endregion
        #region "Stock Consumed"
        public ActionResult ProductionStockConsumedGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var StockList = new List<Return_GetProdItemsConsumed>();
            if (ActionMethodName == "New")
            {
                return PartialView(StockList);
            }
            if (Production_StockConsumedData == null)
            {
                var StockConData = BASE._Stock_Production_DBOps.GetProdItemsConsumed(Prod_ID);
                if (StockConData != null)
                {
                    StockList = StockConData;
                }
                Production_StockConsumedData = StockList;
            }
            return PartialView(Production_StockConsumedData);
        }
        public ActionResult Frm_Stock_Production_Item_Consumed(int SR = 0, string ActionMethod = null)
        {

            ProductionStockConsumedDetails model = new ProductionStockConsumedDetails();
            model.ActionMethod = ActionMethod;
            if (ActionMethod == "New")
            {

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetProdItemsConsumed>)Production_StockConsumedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdItemsConsumed();
                model.sr = Sr;
                model.Production_Item_Name = dataToEdit.Item_Name != null ? dataToEdit.Item_Name : "";
                model.Production_Item_ID = dataToEdit.StockID;
                model.Production_Remarks = dataToEdit.Remarks != null ? dataToEdit.Remarks : "";
                model.Production_Consumed_Qty = Convert.ToDouble(dataToEdit.Consumed_Qty);
                model.Production_Head = dataToEdit.Head;
                model.Production_Make = dataToEdit.Make;
                model.Production_Model = dataToEdit.Model;
                model.Production_Unit = dataToEdit.Unit;
                model.consumptionID = dataToEdit.consumptionID;
                model.Production_Amount = Convert.ToDouble(dataToEdit.Amount);
            }
            return PartialView(model);
        }
        public ActionResult Frm_Add_Stock_Consumed(ProductionStockConsumedDetails model)
        {
            var actionmethod = model.ActionMethod;
            List<Return_GetProdItemsConsumed> gridRows = new List<Return_GetProdItemsConsumed>();
            if (model.Production_Consumed_Qty > model.Production_Available_Qty)
            {
                return Json(new
                {
                    result = false,
                    message = "Consumed quantity  should be smaller than Available quantity..!"
                }, JsonRequestBehavior.AllowGet);
            }
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Production_StockConsumedData != null)
            {
                gridRows = (List<Return_GetProdItemsConsumed>)Production_StockConsumedData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetProdItemsConsumed grid = new Return_GetProdItemsConsumed();
                grid.Sr = NewSr;
                grid.AddedOn = DateTime.Now;
                grid.AddedBy = BASE._open_User_ID;
                grid.StockID = Convert.ToInt32(model.Production_Item_ID);
                grid.Item_Name = model.Production_Item_Name;
                grid.Head = model.Production_Head;
                grid.Make = model.Production_Make;
                grid.Model = model.Production_Model;
                grid.Unit = model.Production_Unit;
                grid.Consumed_Qty = Convert.ToDecimal(model.Production_Consumed_Qty);
                grid.Remarks = model.Production_Remarks;
                grid.Amount = Convert.ToDecimal(model.Production_Amount);
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.sr);
                if (model.consumptionID != 0)
                {
                    var editStockConID = new ArrayList();
                    var editStockCon = ProductionEdit_StockCon_ID as ArrayList;
                    if (editStockCon != null)
                    {
                        editStockCon.Add(model.consumptionID);
                        ProductionEdit_StockCon_ID = editStockCon;
                    }
                    else
                    {
                        editStockConID.Add(model.consumptionID);
                        ProductionEdit_StockCon_ID = editStockConID;
                    }
                }
                dataToEdit.StockID = Convert.ToInt32(model.Production_Item_ID);
                dataToEdit.Item_Name = model.Production_Item_Name;
                dataToEdit.Head = model.Production_Head;
                dataToEdit.Make = model.Production_Make;
                dataToEdit.Model = model.Production_Model;
                dataToEdit.Unit = model.Production_Unit;
                dataToEdit.Consumed_Qty = Convert.ToDecimal(model.Production_Consumed_Qty);
                dataToEdit.Remarks = model.Production_Remarks;
                dataToEdit.Amount = Convert.ToDecimal(model.Production_Amount);
                dataToEdit.AddedOn = DateTime.Now;
                dataToEdit.AddedBy = BASE._open_User_ID;
            }
            Production_StockConsumedData = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Frm_StockCon_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
        {
            var SR = Convert.ToInt16(sr);
            var allData = (List<Return_GetProdItemsConsumed>)Production_StockConsumedData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetProdItemsConsumed();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Production_StockConsumedData = allData;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_StockCon_Prod as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_StockCon_Prod = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_StockCon_Prod = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetStock_Consumed(DataSourceLoadOptions loadOptions, int? storeid, int? projectID)
        {
            var StockConData = new List<Return_Get_Stocks_Listing>();            
            var project_ID = projectID;               
            if (storeid == null)
            {                
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StockConData, loadOptions)), "application/json");
            }
            if (project_ID == null || (int)project_ID == 0)
            {
                StockConData = BASE._Stock_Production_DBOps.GetConsumableStock(Convert.ToInt32(storeid));
            }
            else
            {
                StockConData = BASE._Stock_Production_DBOps.GetConsumableStock(Convert.ToInt32(storeid), (int)project_ID);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StockConData, loadOptions)), "application/json");
        }
        #endregion
        #region "Scrap Created"
        public ActionResult ProductionScrapCreatedGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var ScrapList = new List<Return_GetProdScrapProduced>();
            if (ActionMethodName == "New")
            {
                return PartialView(ScrapList);
            }
            if (Production_ScrapCreatedData == null)
            {
                var ScrapCreateData = BASE._Stock_Production_DBOps.GetProdScrapProduced(Prod_ID);
                if (ScrapCreateData != null)
                {
                    ScrapList = ScrapCreateData;
                }
                Production_ScrapCreatedData = ScrapList;
            }

            return PartialView(Production_ScrapCreatedData);
        }
        public ActionResult Frm_ScrapCreated_Production(int SR = 0, string ActionMethod = null)
        {
            ViewData["Production_AddItemMasterRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");
            ProductionScrapCreatedDetails model = new ProductionScrapCreatedDetails();
            model.ActionMethod = ActionMethod;
            
            if (ActionMethod == "New")
            {

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetProdScrapProduced>)Production_ScrapCreatedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdScrapProduced();
                model.sr = Sr;
                model.Production_ScrapItem_Name = dataToEdit.Item_Name != null ? dataToEdit.Item_Name : "";
                model.Production_ScrapItem_ID = dataToEdit.ItemID;
                model.Production_ScrapRemarks = dataToEdit.Remarks != null ? dataToEdit.Remarks : "";
                model.Production_ScrapQty = Convert.ToDouble(dataToEdit.Qty);
                model.Production_ScrapRate = dataToEdit.Rate;
                model.Production_ScrapAmount = Convert.ToDouble(dataToEdit.Amount);
                model.Production_ScrapUnit = dataToEdit.Unit;
                model.Production_ScrapUnitID = dataToEdit.UnitID;
                model.ID = dataToEdit.ID;
            }
            return PartialView(model);
        }
        public ActionResult Frm_Add_Scrap_Created(ProductionScrapCreatedDetails model)
        {
            var actionmethod = model.ActionMethod;
            List<Return_GetProdScrapProduced> gridRows = new List<Return_GetProdScrapProduced>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Production_ScrapCreatedData != null)
            {
                gridRows = (List<Return_GetProdScrapProduced>)Production_ScrapCreatedData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetProdScrapProduced grid = new Return_GetProdScrapProduced();
                grid.Sr = NewSr;
                grid.Item_Name = model.Production_ScrapItem_Name != null ? model.Production_ScrapItem_Name : "";
                grid.ItemID = Convert.ToInt32(model.Production_ScrapItem_ID);
                grid.Remarks = model.Production_ScrapRemarks;
                grid.Qty = Convert.ToDecimal(model.Production_ScrapQty);
                grid.Rate = Convert.ToDecimal(model.Production_ScrapRate);
                grid.Amount = Convert.ToDecimal(model.Production_ScrapAmount);
                grid.Unit = model.Production_ScrapUnit;
                grid.UnitID = model.Production_ScrapUnitID;
                grid.ID = model.ID;
                grid.Added_On = DateTime.Now;
                grid.Added_By = BASE._open_User_ID;
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.sr);
                if (model.ID != 0)
                {
                    var editScrapID = new ArrayList();
                    var editScrap = ProductionEdit_Scrap_ID as ArrayList;
                    if (editScrap != null)
                    {
                        editScrap.Add(model.ID);
                        ProductionEdit_Scrap_ID = editScrap;
                    }
                    else
                    {
                        editScrapID.Add(model.ID);
                        ProductionEdit_Scrap_ID = editScrapID;
                    }
                }
                dataToEdit.Item_Name = model.Production_ScrapItem_Name != null ? model.Production_ScrapItem_Name : "";
                dataToEdit.ItemID = Convert.ToInt32(model.Production_ScrapItem_ID);
                dataToEdit.Remarks = model.Production_ScrapRemarks;
                dataToEdit.Qty = Convert.ToDecimal(model.Production_ScrapQty);
                dataToEdit.Rate = Convert.ToDecimal(model.Production_ScrapRate);
                dataToEdit.Amount = Convert.ToDecimal(model.Production_ScrapAmount);
                dataToEdit.Unit = model.Production_ScrapUnit;
                dataToEdit.UnitID = model.Production_ScrapUnitID;
                dataToEdit.ID = model.ID;
                dataToEdit.Added_On = DateTime.Now;
                dataToEdit.Added_By = BASE._open_User_ID;
            }
            Production_ScrapCreatedData = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Frm_ScrapCreate_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
        {
            var SR = Convert.ToInt16(sr);
            var allData = (List<Return_GetProdScrapProduced>)Production_ScrapCreatedData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetProdScrapProduced();
            if (dataToDelete.CreatedStockID != 0)
            {
                var scrapproducecheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.CreatedStockID, ClientScreen.Stock_Production);
                if (scrapproducecheck.Count > 0)
                {
                    var inusedscreen = scrapproducecheck[0].Screen;
                    return Json(new
                    {
                        result = false,
                        message = "Scrap Produced Cannot Be Deleted Because It has Been Used In" + inusedscreen + "...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Production_ScrapCreatedData = allData;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_Scrap_Prod as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_Scrap_Prod = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_Scrap_Prod = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetScrap_Created(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockProduction.Return_GetStockItems>(), loadOptions)), "application/json");
            }
            var ScrapData = BASE._Stock_Production_DBOps.GetScrapItems(Convert.ToInt32(storeid));

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ScrapData, loadOptions)), "application/json");
        }
        #endregion
        #region "Stock Produced"
        public ActionResult ProductionStockProduceGridData(string ActionMethodName, int Prod_ID = 0)
        {
            var StockList = new List<Return_GetProdItemProduced>();
            if (ActionMethodName == "New")
            {
                return PartialView(StockList);
            }
            if (Production_StockProducedData == null)
            {
                var StockProdData = BASE._Stock_Production_DBOps.GetProdItemProduced(Prod_ID);
                if (StockProdData != null)
                {
                    StockList = StockProdData;
                }
                Production_StockProducedData = StockList;
            }
            return PartialView(Production_StockProducedData);
        }
        public ActionResult Frm_StkProduce_Production(int SR = 0, string ActionMethod = null)
        {
            ProductionStockProducedDetails model = new ProductionStockProducedDetails();
            model.ActionMethod = ActionMethod;
            if (ActionMethod == "New")
            {

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetProdItemProduced>)Production_StockProducedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetProdItemProduced();
                model.sr = Sr;
                model.Production_StkPrd_Item_Name = dataToEdit.ItemName != null ? dataToEdit.ItemName : "";
                model.Production_StkPrd_Item_ID = dataToEdit.ProducedItemID;
                model.Production_StkPrd_Remarks = dataToEdit.Remarks != null ? dataToEdit.Remarks.ToString() : "";
                model.Production_StkPrd_Produced_Qty = Convert.ToDouble(dataToEdit.Qty_Produced);
                model.Production_StkPrd_Accepted_Qty = Convert.ToDouble(dataToEdit.Qty_Accepted);
                model.Production_StkPrd_Rejected_Qty = Convert.ToDouble(dataToEdit.Qty_Rejected);
                model.Production_StkPrd_TotalValue = Convert.ToDouble(dataToEdit.TotalValue);
                model.Production_StkPrd_MarketPrice = Convert.ToDouble(dataToEdit.MarketPrice);
                model.Production_StkPrd_MarketRate = Convert.ToDouble(dataToEdit.MarketRate);
                model.Production_StkPrd_Head = dataToEdit.Head;
                model.Production_StkPrd_Percentage = Convert.ToDouble(dataToEdit.TotalValue_Perc);
                model.StockID = dataToEdit.StockID;
                model.ID = dataToEdit.ID;
                model.StkPrd_UnitID = dataToEdit.UnitID;
            }
            return PartialView(model);
        }
        public ActionResult Frm_Add_StockProduce(ProductionStockProducedDetails model)
        {
            var actionmethod = model.ActionMethod;

            List<Return_GetProdItemProduced> gridRows = new List<Return_GetProdItemProduced>();
            if (actionmethod == "Edit" || actionmethod == "New")
            {
                if (model.Production_StkPrd_Accepted_Qty > model.Production_StkPrd_Produced_Qty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Accepted Quantity can not be greater than Produced Quantity...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.ActionMethod == "Edit")
            {
                if (model.ID != 0)
                {
                    var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(model.StockID, ClientScreen.Stock_Production);
                    if (itemcheck.Count > 0)
                    {
                        var inusescreen = itemcheck[0].Screen;
                        return Json(new
                        {
                            result = false,
                            message = "Item produced Cannot Be Edited Because It has Been Used In" + inusescreen + "...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Production_StockProducedData != null)
            {
                gridRows = (List<Return_GetProdItemProduced>)Production_StockProducedData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (actionmethod == "New")
            {
                Return_GetProdItemProduced grid = new Return_GetProdItemProduced();
                grid.Sr = NewSr;
                grid.AddedOn = DateTime.Now;
                grid.AddedBy = BASE._open_User_ID;
                grid.ProducedItemID = Convert.ToInt32(model.Production_StkPrd_Item_ID);
                grid.ItemName = model.Production_StkPrd_Item_Name;
                grid.Head = model.Production_StkPrd_Head;
                grid.Qty_Accepted = Convert.ToDecimal(model.Production_StkPrd_Accepted_Qty);
                grid.Qty_Produced = Convert.ToDecimal(model.Production_StkPrd_Produced_Qty);
                grid.Qty_Rejected = Convert.ToDecimal(model.Production_StkPrd_Rejected_Qty);
                grid.MarketPrice = Convert.ToDecimal(model.Production_StkPrd_MarketPrice);
                grid.MarketRate = Convert.ToDecimal(model.Production_StkPrd_MarketRate);
                grid.TotalValue = Convert.ToDecimal(model.Production_StkPrd_TotalValue);
                grid.Remarks = model.Production_StkPrd_Remarks;
                grid.StockID = model.StockID;
                grid.UnitID = model.StkPrd_UnitID;
                grid.TotalValue_Perc = Convert.ToDecimal(model.Production_StkPrd_Percentage);
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.sr);
                if (model.ID != 0)
                {
                    var editStockConID = new ArrayList();
                    var editStockCon = ProductionEdit_StockProd_ID as ArrayList;
                    if (editStockCon != null)
                    {
                        editStockCon.Add(model.ID);
                        ProductionEdit_StockProd_ID = editStockCon;
                    }
                    else
                    {
                        editStockConID.Add(model.ID);
                        ProductionEdit_StockProd_ID = editStockConID;
                    }
                }
                dataToEdit.AddedOn = DateTime.Now;
                dataToEdit.AddedBy = BASE._open_User_ID;
                dataToEdit.ProducedItemID = Convert.ToInt32(model.Production_StkPrd_Item_ID);
                dataToEdit.ItemName = model.Production_StkPrd_Item_Name;
                dataToEdit.Head = model.Production_StkPrd_Head;
                dataToEdit.Qty_Accepted = Convert.ToDecimal(model.Production_StkPrd_Accepted_Qty);
                dataToEdit.Qty_Produced = Convert.ToDecimal(model.Production_StkPrd_Produced_Qty);
                dataToEdit.Qty_Rejected = Convert.ToDecimal(model.Production_StkPrd_Rejected_Qty);
                dataToEdit.MarketPrice = Convert.ToDecimal(model.Production_StkPrd_MarketPrice);
                dataToEdit.MarketRate = Convert.ToDecimal(model.Production_StkPrd_MarketRate);
                dataToEdit.TotalValue = Convert.ToDecimal(model.Production_StkPrd_TotalValue);
                dataToEdit.Remarks = model.Production_StkPrd_Remarks;
                dataToEdit.StockID = model.StockID;
                dataToEdit.UnitID = model.StkPrd_UnitID;
                dataToEdit.TotalValue_Perc = Convert.ToDecimal(model.Production_StkPrd_Percentage);
            }
            Production_StockProducedData = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Frm_StkProduce_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
        {
            var SR = Convert.ToInt16(sr);
            var allData = (List<Return_GetProdItemProduced>)Production_StockProducedData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetProdItemProduced();
            if (dataToDelete.StockID != 0)
            {
                var itemproducecheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.StockID, ClientScreen.Stock_Production);
                if (itemproducecheck.Count > 0)
                {
                    var inusedscreen = itemproducecheck[0].Screen;
                    return Json(new
                    {
                        result = false,
                        message = "Item Produced Cannot Be Deleted Because It has Been Used In" + inusedscreen + "...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Production_StockProducedData = allData;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_Stock_Prod as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_Stock_Prod = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_Stock_Prod = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetStock_Produced(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockProduction.Return_GetStockItems>(), loadOptions)), "application/json");
            }
            var StockProdData = BASE._Stock_Production_DBOps.GetStockItems(Convert.ToInt32(storeid));

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StockProdData, loadOptions)), "application/json");
        }
        public decimal calculatetotalpercentage()
        {
            var stockproducegriddata = Production_StockProducedData as List<Return_GetProdItemProduced>;
            decimal totalpercentage = 0;
            if (stockproducegriddata != null)
            {
                for (int i = 0; i < stockproducegriddata.Count; i++)
                {
                    totalpercentage = totalpercentage + stockproducegriddata[i].TotalValue_Perc;
                }
            }
            return totalpercentage;
        }
        #endregion
        #endregion
        #region"MISC"
        public void Sessionclear()
        {
            Session.Remove("ProductionActionMethod");          
            Session.Remove("ProductionDocument");
            Session.Remove("ItemProduce_ExportData");
            BASE._SessionDictionary.Remove("Production_Documents_Attachment_AttachmentData");
            BASE._SessionDictionary.Remove("ProductionDocument_AttachmentData");
            ClearBaseSession("_Prod");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_ProdInfo");
        }

        public void Production_user_rights()
        {
            ViewData["Production_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Production, "Add");
            ViewData["Production_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Production, "Update");
            ViewData["Production_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Production, "View");
            ViewData["Production_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Production, "Delete");
            ViewData["Production_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Production, "Export");
            ViewData["Production_AddAccVouPaymentRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            ViewData["Production_AddItemMasterRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");            
            ViewData["Production_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["Production_AddProjectRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Add");
        }
        #endregion
    }
}