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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using static Common_Lib.DbOperations.StockRequisitionRequest;
using static Common_Lib.DbOperations.StockUserOrder;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    public class RequestRegisterController : BaseController
    {
        // GET: Stock/RequestRegister
        #region Global Variables
        public DateTime? RRFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("RRFromDate_RRInfo");
            }
            set
            {
                SetBaseSession("RRFromDate_RRInfo", value);
            }
        }
        public DateTime? RRToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("RRToDate_RRInfo");
            }
            set
            {
                SetBaseSession("RRToDate_RRInfo", value);
            }
        }
        public List<StockRequisitionRequest.Return_GetRegister_MainGrid> RequestRegister_Data
        {
            get
            {
                return (List<StockRequisitionRequest.Return_GetRegister_MainGrid>)GetBaseSession("RequestRegister_Data_RRInfo");
            }
            set
            {
                SetBaseSession("RequestRegister_Data_RRInfo", value);
            }
        }
        public List<StockRequisitionRequest.Return_GetRegister_NestedGrid> Return_GetRegister_NestedGrid
        {
            get
            {
                return (List<StockRequisitionRequest.Return_GetRegister_NestedGrid>)GetBaseSession("Return_GetRegister_NestedGrid_RRInfo");
            }
            set
            {
                SetBaseSession("Return_GetRegister_NestedGrid_RRInfo", value);
            }
        }
        public int RR_ID
        {
            get
            {
                return (int)GetBaseSession("RR_ID_RR");
            }
            set
            {
                SetBaseSession("RR_ID_RR", value);
            }
        }
        public List<StockPurchaseOrder.Return_GetRegister_MainGrid> RR_Linked_PO_Data
        {
            get
            {
                return (List<StockPurchaseOrder.Return_GetRegister_MainGrid>)GetBaseSession("RR_Linked_PO_Data_RR");
            }
            set
            {
                SetBaseSession("RR_Linked_PO_Data_RR", value);
            }
        }
        public List<StockPurchaseOrder.Return_GetRegister_NestedGrid> RR_Linked_PO_Nested_Data
        {
            get
            {
                return (List<StockPurchaseOrder.Return_GetRegister_NestedGrid>)GetBaseSession("RR_Linked_PO_Nested_Data_RR");
            }
            set
            {
                SetBaseSession("RR_Linked_PO_Nested_Data_RR", value);
            }
        }
        public List<DbOperations.StockDeptStores.Return_GetStoreDept> RR_All_Stores_Dept_List
        {
            get
            {
                return (List<DbOperations.StockDeptStores.Return_GetStoreDept>)GetBaseSession("RR_All_Stores_Dept_List_RR");
            }
            set
            {
                SetBaseSession("RR_All_Stores_Dept_List_RR", value);
            }
        }
        public List<Return_GetDocumentsGridData> Requisition_Documents_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("Requisition_Documents_Window_Grid_Data_RR");
            }
            set
            {
                SetBaseSession("Requisition_Documents_Window_Grid_Data_RR", value);
            }
        }
        public List<Return_Get_RR_Items_MainGrid> Requisition_ItemRequestedData
        {
            get
            {
                return (List<Return_Get_RR_Items_MainGrid>)GetBaseSession("Requisition_ItemRequestedData_RR");
            }
            set
            {
                SetBaseSession("Requisition_ItemRequestedData_RR", value);
            }
        }

        
        public List<Return_Get_RR_Items_NestedGrid> Requisition_ItemRequestedTaxData
        {
            get
            {
                return (List<Return_Get_RR_Items_NestedGrid>)GetBaseSession("Requisition_ItemRequestedTaxData_RR");
            }
            set
            {
                SetBaseSession("Requisition_ItemRequestedTaxData_RR", value);
            }
        }
        public int[] Delete_RequisitionExisting_ItemRequested_ID
        {
            get
            {
                return (int[])GetBaseSession("Delete_RequisitionExisting_ItemRequested_ID_RR");
            }
            set
            {
                SetBaseSession("Delete_RequisitionExisting_ItemRequested_ID_RR", value);
            }
        }
        public ArrayList RequisitionEdit_ItemRequested_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("RequisitionEdit_ItemRequested_ID_RR");
            }
            set
            {
                SetBaseSession("RequisitionEdit_ItemRequested_ID_RR", value);
            }
        }
        public List<Return_Get_RR_Linked_UO> Requisition_LinkedUserOrderData
        {
            get
            {
                return (List<Return_Get_RR_Linked_UO>)GetBaseSession("Requisition_LinkedUserOrderData_RR");
            }
            set
            {
                SetBaseSession("Requisition_LinkedUserOrderData_RR", value);
            }
        }
        public List<Return_GetRequisitionRemarks> RequisitionExisting_Remarks_Grid_Data
        {
            get
            {
                return (List<Return_GetRequisitionRemarks>)GetBaseSession("RequisitionExisting_Remarks_Grid_Data_RR");
            }
            set
            {
                SetBaseSession("RequisitionExisting_Remarks_Grid_Data_RR", value);
            }
        }
        public List<int> Delete_RequisitionExisting_Remarks_ID
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_RequisitionExisting_Remarks_ID_RR");
            }
            set
            {
                SetBaseSession("Delete_RequisitionExisting_Remarks_ID_RR", value);
            }
        }
        public string[] RequisitionEdit_Document_ID
        {
            get
            {
                return (string[])GetBaseSession("RequisitionEdit_Document_ID_RR");
            }
            set
            {
                SetBaseSession("RequisitionEdit_Document_ID_RR", value);
            }
        }
        public string[] RequisitionDelete_Document_ID
        {
            get
            {
                return (string[])GetBaseSession("RequisitionDelete_Document_ID_RR");
            }
            set
            {
                SetBaseSession("RequisitionDelete_Document_ID_RR", value);
            }
        }
        public string[] RequisitionUnlink_Document_ID
        {
            get
            {
                return (string[])GetBaseSession("RequisitionUnlink_Document_ID_RR");
            }
            set
            {
                SetBaseSession("RequisitionUnlink_Document_ID_RR", value);
            }
        }

        public List<Return_GetUO_Items> UO_ItemOrderedData
        {
            get
            {
                return (List<Return_GetUO_Items>)GetBaseSession("UO_ItemOrderedData_RR");
            }
            set
            {
                SetBaseSession("UO_ItemOrderedData_RR", value);
            }
        }

        public List<Return_Get_RR_Tax_Detail> Requisition_ItemRequestedTaxDetailData
        {
            get
            {
                return (List<Return_Get_RR_Tax_Detail>)GetBaseSession("Requisition_ItemRequestedTaxDetailData_RR_Tax");
            }
            set
            {
                SetBaseSession("Requisition_ItemRequestedTaxDetailData_RR_Tax", value);
            }
        }
        #endregion

        #region "Grid/Nested Grid"    
        public ActionResult Frm_RequestRegister_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_RR, "List"))//Code written for User Authorization do not remove
            {
                string PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewBag.ShowHorizontalBar = 0;
                RR_user_rights();
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_RR').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public PartialViewResult Frm_RequestRegister_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            RR_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (RequestRegister_Data == null || command == "REFRESH")
            {
                var RRMaster = BASE._RR_DBOps.GetRegister(Convert.ToDateTime(RRFromDate), Convert.ToDateTime(RRToDate));
                var MasterGrid = RRMaster.main_Register;
                var Nestedgrid = RRMaster.nested_Register;
                RequestRegister_Data = MasterGrid;
                Return_GetRegister_NestedGrid = Nestedgrid;
                Session["Return_GetRegister_NestedGrid"] = Return_GetRegister_NestedGrid;
            }
            List<StockRequisitionRequest.Return_GetRegister_MainGrid> Mastergrid_data = RequestRegister_Data as List<StockRequisitionRequest.Return_GetRegister_MainGrid>;

            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView("Frm_RequestRegister_Info_Grid", Mastergrid_data);

        }
        public ActionResult RequestRegisterCustomDataAction(int key = 0)
        {
            var Data = RequestRegister_Data as List<StockRequisitionRequest.Return_GetRegister_MainGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Requisition_ID + "![" + it.Requisition_Raised + "![" + it.Requisition_Status + "![" + it.Requisition_Type + "![" + it.Tot_Req_Amount + "![" + it.Purchased_by + "![" + it.Requestor + "![" + it.Req_Dept + "![" +
                          it.Remarks + "![" + it.Added_On + "![" + it.Added_By + "![" + it.Edited_On + "![" + it.Edited_By + "![" + it.ID + "![" + it.CurrUserRole + "![" + it.Latest_UOID; 
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public PartialViewResult Frm_RequestRegister_ItemRequest_Grid(int RequestID, string Command, string FromDate = "", string ToDate = "", int ShowHorizontalBar = 0)
        {
            ViewData["FromDate"] = FromDate;
            ViewData["ToDate"] = ToDate;
            ViewBag.RRItemGrid_ShowHorizontalBar = ShowHorizontalBar;
            ViewData["RequestID"] = RequestID;

            if (Return_GetRegister_NestedGrid == null || Command == "REFRESH")
            {
                var RequestRegister_Data = BASE._RR_DBOps.GetRegister(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                var ItemRequest_Data = RequestRegister_Data.nested_Register;
                Return_GetRegister_NestedGrid = ItemRequest_Data;
            }
            var data = Return_GetRegister_NestedGrid as List<StockRequisitionRequest.Return_GetRegister_NestedGrid>;
            List<StockRequisitionRequest.Return_GetRegister_NestedGrid> itemRequest = data.FindAll(x => x.RR_ID == RequestID);
            return PartialView(itemRequest);
        }
        public ActionResult ItemRequestCustomDataAction(int key = 0)
        {
            var Data = Return_GetRegister_NestedGrid as List<StockRequisitionRequest.Return_GetRegister_NestedGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.ID + "![" + it.Added_On + "![" + it.Added_By + "![" + it.Edited_On + "![" + it.Edited_By;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public static GridViewSettings RequestRegisterNestedGridSettings(int request_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "RequestRegister" + request_ID;
            settings.SettingsDetail.MasterGridName = "RequestRegisterListGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Destination_Location").Visible = true;
            settings.Columns.Add("Requested_Quantity").Visible = true;
            settings.Columns.Add("Approved_Quantity").Visible = true;
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Rate").Visible = true;
            settings.Columns.Add("Discount_Promised").Visible = true;
            settings.Columns.Add("Amount").Visible = true;
            settings.Columns.Add("Supplier_Dept").Visible = true;
            settings.Columns.Add("RR_Priority").Visible = true;
            settings.Columns.Add("Req_Delivery_Date").Visible = true;
            settings.Columns.Add("RequestedItemID").Visible = false;
            settings.Columns.Add("ID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "onitemrequestfocusedrowchange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;

        }//settings for nested grid
        public static IEnumerable GetItemRequest(int request_ID)
        {
            List<StockRequisitionRequest.Return_GetRegister_NestedGrid> data = (List<StockRequisitionRequest.Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["Return_GetRegister_NestedGrid"];
            List<StockRequisitionRequest.Return_GetRegister_NestedGrid> itemrequestlist = data.FindAll(x => x.RR_ID == request_ID);
            return itemrequestlist;
        }//binding data to nested grid
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
        public ActionResult Frm_Export_Options()// list export
        {
            if (!CheckRights(BASE, ClientScreen.Stock_RR, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#RequestRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return View();
        }
        #endregion
        #region "Period Selection"
        public ActionResult LookUp_Get_ViewType_List_Request(bool? IsVisible, DataSourceLoadOptions loadOptions)
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
        public ActionResult LookUp_ViewType_ChangeEvent_Request(string Chaval)
        {
            RequestRegister_Period model = GetPeriod(Chaval);
            RRFromDate = model.RR_Fromdate;
            RRToDate = model.RR_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public RequestRegister_Period GetPeriod(string Chaval)
        {
            RequestRegister_Period model = new RequestRegister_Period();
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

            model.RR_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.RR_Fromdate = xFr_Date;
            model.RR_Todate = xTo_Date;
            return model;
        }
        public ActionResult Frm_Change_Period_Screen_Requisition()
        {
            RequestRegister_Period model = new RequestRegister_Period();
            model.RR_PeriodSelection = "Specific Period";
            model.RR_Todate = Convert.ToDateTime(RRToDate);
            model.RR_Fromdate = Convert.ToDateTime(RRFromDate);
            model.RR_BE_View_Period = "";
            model.RR_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.RR_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_RR(RequestRegister_Period model)
        {
            if (model.RR_Fromdate > model.RR_Todate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RRToDate = model.RR_Todate;
                RRFromDate = model.RR_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
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
        #endregion
        #region "NEVD"
        /// <summary>
        /// Called in case of Edit/Delete only
        /// </summary>
        /// <param name="ActionMethod"></param>
        /// <param name="ID"></param>
        /// <param name="RR_Date"></param>
        /// <returns></returns>
        public ActionResult DataNavigation(string ActionMethod, int ID, DateTime RR_Date, string ReqNo)
        {
            Return_Get_RR_Detail RR_Details;
            string RR_Status = "";
            Return_Get_RR_Usage_Count RR_Usage = BASE._RR_DBOps.Get_RR_Usage_Count(ID);
            RR_Details = BASE._RR_DBOps.Get_RR_Detail(ID);
            RR_Status = RR_Details.Status;
            if (ActionMethod == "Delete" || ActionMethod == "Edit")
            {
                DataTable Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
                DataTable Accounts_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                if (RR_Status.ToLower() == "completed")
                {
                    return Json(new
                    {
                        Message = "Completed Requisition Request cannot be Edited/Deleted...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (Audited_Date != null && Audited_Date.Rows.Count > 0)
                    {
                        if (RR_Date >= Convert.ToDateTime(Audited_Date.Rows[0]["FROMDATE"]) && RR_Date <= Convert.ToDateTime(Audited_Date.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Audited Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Accounts_Submitted_Date != null && Accounts_Submitted_Date.Rows.Count > 0)
                    {
                        if (RR_Date >= Convert.ToDateTime(Accounts_Submitted_Date.Rows[0]["FROMDATE"]) && RR_Date <= Convert.ToDateTime(Accounts_Submitted_Date.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Accounts Submitted Period..!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            if (ActionMethod == "Delete")
            {

                if (RR_Usage.PO_Count > 0)
                {
                    return Json(new
                    {
                        Message = "Requisition Request referred in Purchase Order cannot be Deleted...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (RR_Usage.UO_Count > 0)
                {
                    return Json(new
                    {
                        Message = "Requisition Request referred in User Order cannot be Deleted...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (RR_Usage.TO_Count > 0)
                {
                    return Json(new
                    {
                        Message = "Requisition Request referred in Transfer Order cannot be Deleted...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (RR_Status.ToLower() == "rejected" || RR_Status.ToLower() == "cancelled" || RR_Status.ToLower() == "completed")
                {
                    return Json(new
                    {
                        Message = RR_Status + " Requisition Request cannot be Deleted...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "DeliverUO")
            {
                if (RR_Usage.PO_Count == 0)
                {
                    return Json(new
                    {
                        Message = "No Purchase order is linked to selected Requisition Request...",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "OpenTO" || ActionMethod == "AcceptTO")
            {
                if (RR_Usage.TO_Count == 0)
                {
                    return Json(new
                    {
                        Message = "No Transfer order is linked to selected Requisition Request...",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "OpenPO")
            {
                if (RR_Usage.PO_Count == 0)
                {
                    return Json(new
                    {
                        Message = "No Purchase order is linked to selected Requisition Request...",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "OpenUO")
            {
                if (RR_Usage.UO_Count == 0)
                {
                    return Json(new
                    {
                        Message = "No User order is linked to selected Requisition Request...",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "AcceptTO")
            {
                if (RR_Details.RR_Type == "Purchase Request")
                {
                    return Json(new
                    {
                        Message = "RR Type is not Transfer Request...",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                var jobStatus = "";
                var prjStatus = "";
                if (RR_Details.Job_ID != null)
                {
                    jobStatus = BASE._Jobs_Dbops.GetRecord((int)RR_Details.Job_ID).Job_Status;
                }
                if (RR_Details.Project_ID != null)
                {
                    prjStatus = BASE._Projects_Dbops.GetRecord((int)RR_Details.Project_ID).FirstOrDefault().Proj_Status;
                }
                if (jobStatus == "Completed" || prjStatus == "Completed")
                {
                    return Json(new
                    {
                        Message = "RR against completed Job/Project cannot be Reopened/Edited/Deleted",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                var StoreCloseDate = BASE._StockDeptStores_dbops.GetRegister().Where(x => x.ID == RR_Details.Dept_Store_ID).FirstOrDefault().Close_Date;
                if (StoreCloseDate != null && StoreCloseDate.ToString().Length > 0)
                {
                    return Json(new
                    {
                        Message = "RR by closed store/Dept cannot be Reopened/Edited/Deleted",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            //Default Case with no Validations active
            return Json(new
            {
                Message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_NEVD_Requisition(string ActionMethod, int ID = 0, string PopupName = "Dynamic_Content_popup", string PostSuccessFunction = "", string CurUserRole = "", int UO_ID = 0, string UO_SubItem_ID = null,string UO_ItemRecID = null, int? Jobid = null, int? projectid = null)
        {            
            RR_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_RR, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }
            
            Sessionclear();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            RR_ID = ID;
            ViewData["RR_ID"] = RR_ID;
            ViewData["RRActionMethod"] = Navigation_Mode_tag;
            Model_NEVD_Requisition model = new Model_NEVD_Requisition();
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            model.RequisitionDate = DateTime.Today;
            model.RequisitionStatus = "New";
            model.RR_Curr_User_Personnel_ID = BASE._open_User_PersonnelID;
            model.RR_Curr_User_Store_ID = BASE._open_User_SubDeptID;
            model.is_Approver = CheckApproverRights() ? 1 : 0;
            model.RequisitionRequestorStoreDeptID = GetUserStoreID();
            model.RRPopupName = PopupName.Length > 0 ? PopupName : "Dynamic_Content_popup";
            model.RRPostSuccessFunction = PostSuccessFunction.Length > 0 ? PostSuccessFunction : "OnRequisitionRegisterAjaxSuccessForm";
            model.RequisitionRequestorID = BASE._open_User_PersonnelID;
            model.RequisitionType = "Purchase Request";

            if (Jobid != null)
            {
                model.RequisitionJobID = Jobid;
                    }

            if (projectid != null)
            {
                model.RequisitionProjectID = projectid;
            }
            if (UO_ID != 0)
                {
                ViewData["UO_ID"] = UO_ID;
                model.RR_UOID = UO_ID;
            }
            if (UO_SubItem_ID != null)
            {
                ViewData["UO_SubItem_ID"] = UO_SubItem_ID;
                model.RR_UOSubitemID = UO_SubItem_ID;
                model.RR_UO_ItemRecID = UO_ItemRecID;

            }

            //for (int i = 0; i < UO_Item_ID.Length; i++)
            //{
            //    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_Item_ID[i].ToString());
            //    unparam.UO_Item_ID = selectedrow.ID;
            //}

            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                Return_Get_RR_Detail selectedrowdata = BASE._RR_DBOps.Get_RR_Detail(ID);
                if (selectedrowdata != null)
                {
                    model.RR_ID = ID;
                    model.RequisitionNumber = selectedrowdata.ID_No;
                    model.RequisitionDate = selectedrowdata.RR_Date;
                    model.RequisitionStatus = selectedrowdata.Status;
                    model.RequisitionProjectID = selectedrowdata.Project_ID;
                    model.RequisitionJobID = selectedrowdata.Job_ID;
                    model.RequisitionRequestorStoreDeptID = selectedrowdata.Dept_Store_ID;
                    model.RequisitionRequestorID = selectedrowdata.Requestor_ID;
                    model.RequisitionType = selectedrowdata.RR_Type;
                    model.RequisitionTransferFromDeptID = selectedrowdata.Trf_From_Dept_ID;
                    model.RequisitionPurchasedByID = selectedrowdata.Purchased_by_ID;
                    model.RequisitionSpecialDiscount = selectedrowdata.Special_Discount;
                    model.RR_CurrUserRole = CurUserRole;
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_NEVD_Requisition(Model_NEVD_Requisition model)
        {
            string ReturnMessage = "";
            try
            {
                var tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                var actionmethod = model.TempActionMethod;

                if (model.RRSubmitMode.ToUpper() == "APPROVE_RR_FULL_REQUESTED_QTY")
                {
                    //Marks all Approved Qty to Requested Qty, as user has inst}ructed to do so 
                    Requisition_Approve_Items_qty("Approve All", "");
                }

                #region "Checking Restriction"
                if (actionmethod == "_New" || actionmethod == "_Edit")
                {
                    if (model.RequisitionDate < BASE._open_Year_Sdt || model.RequisitionDate > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            Message = "Requisition Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    var Audited_Period = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var Accounts_Submitted_Period = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                    if (Audited_Period != null && Audited_Period.Rows.Count > 0)
                    {
                        if (model.RequisitionDate >= Convert.ToDateTime(Audited_Period.Rows[0]["FROMDATE"]) && model.RequisitionDate <= Convert.ToDateTime(Audited_Period.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Audited Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Accounts_Submitted_Period != null && Accounts_Submitted_Period.Rows.Count > 0)
                    {
                        if (model.RequisitionDate >= Convert.ToDateTime(Accounts_Submitted_Period.Rows[0]["FROMDATE"]) && model.RequisitionDate <= Convert.ToDateTime(Accounts_Submitted_Period.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Accounts Submitted Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if(model.RequisitionType.ToUpper() == "TRANSFER REQUEST") //mantis bug #695,#687 resolved
                    {
                        if (model.RequisitionTransferFromDeptID == null)
                        {
                            return Json(new
                            {
                                Message = "Transfer From Dept Cannot Be Blank..!!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (model.RequisitionType.ToUpper() == "PURCHASE REQUEST") //mantis bug 687 resolved
                    {
                        if (model.RequisitionPurchasedByID == null)
                        {
                            return Json(new
                            {
                                Message = "Purchased By Cannot Be Blank..!!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }


                }
                #endregion
                if (model.RRSubmitMode.ToUpper() == "CANCEL_RR" && actionmethod == "_New")
                {
                    return Json(new
                    {
                        Message = "New Requisition Request can not be can cancelled before creation..!!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                    #region "IUD"
                    if (actionmethod == "_New" || actionmethod == "_Edit")
                {
                    Param_Insert_RequisitionRequest_Txn InParam = new Param_Insert_RequisitionRequest_Txn();
                    Param_Update_RequisitionRequest_Txn UpParam = new Param_Update_RequisitionRequest_Txn();

                    IUDDocuments(ref InParam, ref UpParam, actionmethod);
                    IUDItemRequested(ref InParam, ref UpParam, actionmethod);
                    IUDRemainingData(ref InParam, ref UpParam, model);
                    if (actionmethod == "_New")
                    {
                        model.RR_ID = BASE._RR_DBOps.InsertRequisitionRequest_Txn(InParam);
                        if (model.RR_ID > 0)
                        {
                            if(model.RR_UOID != 0)
                            {
                                Param_UO_Insert_UO_RR_Mapping inparam = new Param_UO_Insert_UO_RR_Mapping();
                                inparam.UO_ID = model.RR_UOID;
                                inparam.RR_ID = model.RR_ID;
                                if (model.RR_UO_ItemRecID != null)
                                {
                                    var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;
                                    var itemid = model.RR_UO_ItemRecID;
                                    var itemRecid = itemid.Split(',').Select(t => t.Trim()).ToArray();
                                    for (int i = 0; i < itemRecid.Length - 1; i++)
                                    {
                                        //   Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == subitemid[i].ToString());
                                        inparam.UO_Item_ID = Convert.ToInt32(itemRecid[i]);
                                        if (!BASE._user_order_DBOps.InsertUORRMapping(inparam))
                                        {
                                            return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!BASE._user_order_DBOps.InsertUORRMapping(inparam))
                                    {
                                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            ReturnMessage = Common_Lib.Messages.SaveSuccess;
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
                    if (actionmethod == "_Edit")
                    {
                        if (BASE._RR_DBOps.UpdateRequisitionRequest_Txn(UpParam))
                        {
                            ReturnMessage = Common_Lib.Messages.UpdateSuccess;
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
                if (actionmethod == "_Delete")
                {
                    if (BASE._RR_DBOps.DeleteRequisitionRequest_Txn(model.RR_ID))
                    {
                        ReturnMessage = Common_Lib.Messages.DeleteSuccess;
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
                if (model.RRSubmitMode.ToUpper() == "SUBMIT_FOR_APPROVAL")
                {
                    return Requisition_Update_Status("Submit for Approval", model.RR_ID, true);
                }
                else if (model.RRSubmitMode.ToUpper() == "CANCEL_RR")
                {
                    return Requisition_Update_Status("Cancel", model.RR_ID, true);
                }
                else if (model.RRSubmitMode.ToUpper() == "APPROVE_RR" || model.RRSubmitMode.ToUpper() == "APPROVE_RR_FULL_REQUESTED_QTY")
                {
                    return Requisition_Update_Status("Approve", model.RR_ID, true);
                }
                else if (model.RRSubmitMode.ToUpper() == "REJECT_RR")
                {
                    return Requisition_Update_Status("Reject", model.RR_ID, true);
                }
                else if (model.RRSubmitMode.ToUpper() == "RECOMMEND_CHANGES_RR")
                {
                    return Requisition_Update_Status("Recommend Changes", model.RR_ID, true);
                }
                else
                {
                    return Json(new
                    {
                        Message = ReturnMessage,
                        result = true,
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
        public JsonResult Requisition_Update_Status(string UpdatedStatus, int ID, bool fromPostFunction = false)
        {
            try
            {

                #region "Check Validations"
                Return_Get_RR_Usage_Count RR_Usage = BASE._RR_DBOps.Get_RR_Usage_Count(ID);
                if (UpdatedStatus == "Approve" && fromPostFunction == false)
                {
                    List<StockRequisitionRequest.Return_GetRegister_NestedGrid> Nestedgrid = (List<StockRequisitionRequest.Return_GetRegister_NestedGrid>)Return_GetRegister_NestedGrid;
                    List<StockRequisitionRequest.Return_GetRegister_NestedGrid> _RR_Items_ApprovedQty_0 = Nestedgrid.Where(x => x.RR_ID == ID).ToList().Where(x => x.Approved_Quantity == 0).ToList();
                    if (_RR_Items_ApprovedQty_0.Count > 0)
                    {
                        return Json(new
                        {
                            Message = "You have not Approved Requested Quantity for any of the Requested Item in Selected Requisition!!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (UpdatedStatus == "Recommend Changes")
                {
                    Return_Get_RR_Detail RR_Details;
                    RR_Details = BASE._RR_DBOps.Get_RR_Detail(ID);
                    if (RR_Details.Status=="Changes Recommended")
                    {
                        return Json(new
                        {
                            Message = "Recommending Changes not allowed for Requisition in Changes Recommended Status!!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (UpdatedStatus == "Reopen")
                {
                    Return_Get_RR_Detail RR_Details;
                    RR_Details = BASE._RR_DBOps.Get_RR_Detail(ID);
                    if (RR_Details.Job_ID != null) {
                        var jobStatus = BASE._Jobs_Dbops.GetRecord((int)RR_Details.Job_ID).Job_Status;
                        if (jobStatus == "Completed")
                        {
                            return Json(new
                            {
                                Message = "RR against completed Job/Project cannot be Reopened",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (RR_Details.Project_ID != null)
                    {
                        var prjStatus = BASE._Projects_Dbops.GetRecord((int)RR_Details.Project_ID).FirstOrDefault().Proj_Status;
                        if (prjStatus == "Completed")
                        {
                            return Json(new
                            {
                                Message = "RR against completed Job/Project cannot be Reopened",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                  
                    
                    var StoreCloseDate = BASE._StockDeptStores_dbops.GetRegister().Where(x => x.ID == RR_Details.Dept_Store_ID).FirstOrDefault().Close_Date;
                    if (StoreCloseDate != null && StoreCloseDate.ToString().Length > 0)
                    {
                        return Json(new
                        {
                            Message = "RR by closed store/Dept cannot be Reopened",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (UpdatedStatus == "Cancel")
                {
                    if (RR_Usage.PO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in Purchase Order cannot be Cancelled...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (RR_Usage.UO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in User Order cannot be Cancelled...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (RR_Usage.TO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in Transfer Order cannot be Cancelled...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (UpdatedStatus == "Reject")
                {
                    if (RR_Usage.PO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in Purchase Order cannot be Rejected...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (RR_Usage.UO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in User Order cannot be Rejected...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (RR_Usage.TO_Count > 0)
                    {
                        return Json(new
                        {
                            Message = "Requisition Request referred in Transfer Order cannot be Rejected...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (UpdatedStatus == "Complete")
                {
                    var PO_TO_Incomplete_Count = BASE._RR_DBOps.Get_PO_TO_Incomplete_Count(ID);
                    if (PO_TO_Incomplete_Count > 0)
                    { 
                        return Json(new
                        {
                            Message = "Requisition cannot be marked as Completed, When the related PO/TO is not marked as Completed....!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }//Mantis bug 0000715 fixed

                #endregion
                //All Validations are Ok, so updating status 
                Param_Update_RR_Status StatusParam = new Param_Update_RR_Status();
                StatusParam.RRID = ID;
                string SuccessMessage = "";
                switch (UpdatedStatus)
                {
                    case "Cancel":
                        StatusParam.UpdatedStatus = RR_Status.Cancelled;
                        SuccessMessage = "Requisition Cancelled!!";
                        break;
                    case "Approve":
                        StatusParam.UpdatedStatus = RR_Status.Approved;
                        var Item_Requested_Count = ViewData["Item_Requested_Count"];//Mantis bug 0000884 fixed
                        SuccessMessage = "Requisition Approved for "+ Item_Requested_Count + " Item(s)!!";//Mantis bug 0000884 fixed
                        break;
                    case "Recommend Changes":
                        StatusParam.UpdatedStatus = RR_Status.Changes_Recommended;
                        SuccessMessage = "Changes Recommended Successfully in Current Requisition !!";
                        break;
                    case "Complete":
                        StatusParam.UpdatedStatus = RR_Status.Completed;
                        SuccessMessage = "Requisition Completed !!";
                        break;
                    case "Reject":
                        StatusParam.UpdatedStatus = RR_Status.Rejected;
                        SuccessMessage = "Requisition Rejected!!";
                        break;
                    case "Re-Requisition":
                        StatusParam.UpdatedStatus = RR_Status.Re_Requisition_Requested;
                        SuccessMessage = "Requisition sent for Re-Requisition!!";
                        break;
                    case "Submit for Approval":
                        StatusParam.UpdatedStatus = RR_Status.Submitted_for_Approval;
                        SuccessMessage = "Requisition Submitted for Approval!!";
                        break;
                    case "Reopen":
                    case "New":
                        StatusParam.UpdatedStatus = RR_Status._New;
                        SuccessMessage = "Requisition Reopened!!";
                        break;
                }
                BASE._RR_DBOps.UpdateRRStatus(StatusParam);
                return Json(new { Message = SuccessMessage, result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RR_Linked_PO(string ReqID = "")
        {
            if (RR_Linked_PO_Data == null)
            {
                var PurchaseRegister_Data = BASE._PO_DBOps.GetRegister(BASE._open_Year_Sdt, BASE._open_Year_Edt);
                if (PurchaseRegister_Data != null)
                {
                    var MasterGrid = PurchaseRegister_Data.main_Register;
                    var Nestedgrid = PurchaseRegister_Data.nested_Register;
                    var RR_Linked_PO = MasterGrid.FindAll(x => x.RequisitionID == ReqID);
                    RR_Linked_PO_Data = RR_Linked_PO;
                    RR_Linked_PO_Nested_Data = Nestedgrid;
                }
            }
            List<StockPurchaseOrder.Return_GetRegister_MainGrid> Mastergrid_data = RR_Linked_PO_Data as List<StockPurchaseOrder.Return_GetRegister_MainGrid>;

            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }
        public PartialViewResult RR_Linked_PO_ItemPurchase_Grid(int PO_ID)
        {
            ViewData["Linked_POID"] = PO_ID;
            if (RR_Linked_PO_Nested_Data == null)
            {
                var PurchaseRegister_Data = BASE._PO_DBOps.GetRegister(BASE._open_Year_Sdt, BASE._open_Year_Edt);
                var ItemPurchase_Data = PurchaseRegister_Data.nested_Register;
                RR_Linked_PO_Nested_Data = ItemPurchase_Data;
            }
            var data = RR_Linked_PO_Nested_Data as List<StockPurchaseOrder.Return_GetRegister_NestedGrid>;
            List<StockPurchaseOrder.Return_GetRegister_NestedGrid> ItemPurchase = data.FindAll(x => x.PO_ID == PO_ID);
            return PartialView(ItemPurchase);
        }

        public int? GetUserStoreID()
        {
            int? ReturnStoreID = null;
            if (BASE._open_User_SubDeptID != null)
            {
                List<DbOperations.StockDeptStores.Return_GetStoreDept> Get_Store_List = null;
                if (RR_All_Stores_Dept_List == null)
                {
                    Get_Store_List = BASE._StockDeptStores_dbops.GetAllStoreDeptList();
                    RR_All_Stores_Dept_List = Get_Store_List;
                }
                else
                {
                    Get_Store_List = (List<DbOperations.StockDeptStores.Return_GetStoreDept>)RR_All_Stores_Dept_List;
                }
                var it = Get_Store_List.Where(f => f.ID == BASE._open_User_SubDeptID).FirstOrDefault();
                if (it != null) ReturnStoreID = BASE._open_User_SubDeptID;
            }
            return ReturnStoreID;
        }
        public ActionResult CheckDocumentsLinked(int RRID)
        {
            var docdata = Requisition_Documents_Window_Grid_Data as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._RR_DBOps.GetAttachmentLinkScreen(RRID, docdata[i].ID);
                    if (!string.IsNullOrEmpty(screen))
                    {
                        if (screen != "Requisition")
                        {
                            return Json(new
                            {
                                result = false,
                                message = "There Are Documents That Cannot Be Deleted Because They Have Been Linked To Other Screens </ br > Do You Want To Unlink It From Project..?"
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
                }
            }
            return Json(new
            {
                result = false,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Dropdown Data Binding"
        public ActionResult Get_Project_List(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.Projects.Return_GetList> Get_Project_Name_List = BASE._Projects_Dbops.GetList();
            //Get_Project_Name_List.Insert(0, new DbOperations.Projects.Return_GetList { Project_Name = "Please Select Project Name...", Sanction_no = "", Sanction_Date = DateTime.Now, Complex_Name = "", Project_Id = 0, Complex_Id = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Project_Name_List, loadOptions)));
        }
        public ActionResult Get_Job_List(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.Jobs.Return_GetList> Get_Job_Name_List = BASE._Jobs_Dbops.GetOpenJobs();
            //Get_Job_Name_List.Insert(0, new DbOperations.Jobs.Return_GetList { Job_Name = "Please Select Job Name...", Job_No = 0, Job_Type = "", Proj_Name = "", Complex = "", Sanction_No ="", ID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Job_Name_List, loadOptions)), "application/json");
        }
        public ActionResult Get_Req_Store_Dept_List(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockDeptStores.Return_GetStoreDept> Get_Store_Dept_List = null;
            if (RR_All_Stores_Dept_List == null)
            {
                Get_Store_Dept_List = BASE._StockDeptStores_dbops.GetAllStoreDeptList();
            }
            else
            {
                Get_Store_Dept_List = (List<DbOperations.StockDeptStores.Return_GetStoreDept>)RR_All_Stores_Dept_List;
            }
            //Get_Store_Dept_List.Insert(0, new DbOperations.StockDeptStores.Return_GetStoreList { Store_Name = "Please Select Requestor Store/Dept....",StoreID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Store_Dept_List, loadOptions)), "application/json");
        }
        public ActionResult Get_Requestor_List(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.Jobs.Return_GetStockPersonnels> Get_Req_List = BASE._Jobs_Dbops.GetStockPersonnels();
            //Get_Req_List.Insert(0, new DbOperations.Jobs.Return_GetStockPersonnels { Name = "Please Select Requestor....", ID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Req_List, loadOptions)), "application/json");
        }
        public ActionResult Get_Trf_From_Dept_List(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockDeptStores.Return_GetStoreDept> Get_Dept_List = BASE._StockDeptStores_dbops.GetAllStoreDeptList();
            //Get_Dept_List.Insert(0, new DbOperations.StockDeptStores.Return_GetStoreDept { Name = "Please Select Transfer From Dept....", ID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_Dept_List, loadOptions)), "application/json");
        }
        public ActionResult Get_Purchaser_List(DataSourceLoadOptions loadOptions, int? storedeptid)
        {
            if (storedeptid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockRequisitionRequest.Return_GetPurchasedBy>(), loadOptions)));
            }
            List<DbOperations.StockRequisitionRequest.Return_GetPurchasedBy> Get_pers_List = BASE._RR_DBOps.GetPurchasedBy(Convert.ToInt32(storedeptid));
            //Get_pers_List.Insert(0, new DbOperations.StockRequisitionRequest.Return_GetPurchasedBy { Name = "Please Select Transfer From Dept....", ID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Get_pers_List, loadOptions)), "application/json");
        }

        #endregion
        #region "Inside Grids"
        #region "Items Requested"
        public ActionResult RequisitionItemRequestedGridData(string ActionMethodName, int RR_ID = 0, int UO_ID = 0, string UO_SubItem_ID = null)
        {
            ViewData["RR_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Approve");
            ViewData["RRActionMethod"] = ActionMethodName;
            ViewData["RR_ID"] = RR_ID;
            ViewData["UO_ID"] = UO_ID;
            var ItemRequestedList = new List<Return_Get_RR_Items_MainGrid>();
            var ItemRequestedTaxList = new List<Return_Get_RR_Items_NestedGrid>();

            if (ActionMethodName == "_New")
            {
                if (UO_ID != 0)
                {

                    Param_Get_RR_Items inparam = new Param_Get_RR_Items();
                    inparam.RR_ID = RR_ID;
                    inparam.UO_ID = UO_ID;

                    inparam.SubItemID = UO_SubItem_ID;


                    var rr_Items = BASE._RR_DBOps.Get_RR_Items(inparam);
                    List<Return_Get_RR_Items_MainGrid> RequestedItemData = rr_Items.Main_grid_Data;
                    List<Return_Get_RR_Items_NestedGrid> RequestedItemTaxData = rr_Items.Nested_grid_Data;
                    if (RequestedItemData != null)
                    {
                        ItemRequestedList = RequestedItemData;
                    }
                    if (RequestedItemTaxData != null)
                    {
                        ItemRequestedTaxList = RequestedItemTaxData;
                    }
                    Requisition_ItemRequestedData = ItemRequestedList;
                    Requisition_ItemRequestedTaxData = ItemRequestedTaxList;
                    Session["Requisition_ItemRequestedTaxData"] = Requisition_ItemRequestedTaxData;
                }
                else
                {
                    return PartialView(ItemRequestedList);
                }
                
            }


            if (Requisition_ItemRequestedData == null)
            {
                Param_Get_RR_Items inparam = new Param_Get_RR_Items();
                inparam.RR_ID = RR_ID;
                if (UO_ID != 0)
                {

                    inparam.UO_ID = UO_ID;
                
                    inparam.SubItemID = UO_SubItem_ID;
                }
                else
                {
                    inparam.UO_ID = null;
                }
                var rr_Items = BASE._RR_DBOps.Get_RR_Items(inparam);
                List<Return_Get_RR_Items_MainGrid> RequestedItemData = rr_Items.Main_grid_Data;
                List<Return_Get_RR_Items_NestedGrid> RequestedItemTaxData = rr_Items.Nested_grid_Data;
                if (RequestedItemData != null)
                {
                    ItemRequestedList = RequestedItemData;
                }
                if (RequestedItemTaxData != null)
                {
                    ItemRequestedTaxList = RequestedItemTaxData;
                }
                Requisition_ItemRequestedData = ItemRequestedList;
                Requisition_ItemRequestedTaxData = ItemRequestedTaxList;
                Session["Requisition_ItemRequestedTaxData"] = Requisition_ItemRequestedTaxData;
            }

            return PartialView(Requisition_ItemRequestedData);
        }

        public PartialViewResult Frm_RequestRegister_ItemRequestedTax_Grid(string Command,int Requested_Item_ID = 0, int MainSr = 0)
        {
            ViewData["Requested_Item_ID"] = Requested_Item_ID;
            ViewData["MainSr"] = MainSr;

            if (Requisition_ItemRequestedData == null || Command == "REFRESH")
            {
                Param_Get_RR_Items inparam = new Param_Get_RR_Items();
                inparam.RR_ID = (Int32)RR_ID;
                var rr_Items = BASE._RR_DBOps.Get_RR_Items(inparam);
                List<Return_Get_RR_Items_MainGrid> RequestedItemData = rr_Items.Main_grid_Data;
                List<Return_Get_RR_Items_NestedGrid> RequestedItemTaxData = rr_Items.Nested_grid_Data;
                Requisition_ItemRequestedData = RequestedItemData;
                Requisition_ItemRequestedTaxData = RequestedItemTaxData;
            }
            var data = Requisition_ItemRequestedTaxData as List<Return_Get_RR_Items_NestedGrid>;
            List<Return_Get_RR_Items_NestedGrid> itemReqTax = new List<Return_Get_RR_Items_NestedGrid>();

            if (Requested_Item_ID == 0)
            {
                itemReqTax = data.FindAll(x => x.MainSr == MainSr && x.RRI_ID == 0);
            }
            else
            {
              itemReqTax = data.FindAll(x => x.RRI_ID == Requested_Item_ID);
            }
            return PartialView(itemReqTax);
        }

        public static GridViewSettings RequestRegisterItemTaxNestedGridSettings(int Requested_Item_ID = 0, int Sr = 0)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ItemRequestedTax" + Requested_Item_ID + Sr;
            settings.SettingsDetail.MasterGridName = "RequisitionItemRequestedGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Sr").Visible = true;
            settings.Columns.Add("Tax_Type");
            settings.Columns.Add("TaxPercent").Visible = true;
            settings.Columns.Add("Requested_Quantity").Visible = true;

            settings.Columns.Add("RR_ID").Visible = false;
            settings.Columns.Add("Tax_TypeID").Visible = false;
            settings.Columns.Add("ID").Visible = true;
            settings.Columns.Add("MainSr").Visible = false;
            // settings.ClientSideEvents.FocusedRowChanged = "onitemrequestfocusedrowchange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            // settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;

        }//settings for nested grid
        public static IEnumerable GetItemRequestTax(int Requested_Item_ID = 0,  int Sr = 0)
        {
            List<Return_Get_RR_Items_NestedGrid> data = (List<Return_Get_RR_Items_NestedGrid>)System.Web.HttpContext.Current.Session["Requisition_ItemRequestedTaxData"];
            List<Return_Get_RR_Items_NestedGrid> itemrequestTaxlist = new List<Return_Get_RR_Items_NestedGrid>();

            if (Requested_Item_ID == 0)
            {
             itemrequestTaxlist = data.FindAll(x => x.MainSr == Sr && x.RRI_ID == 0);
            }
            else
            {
             itemrequestTaxlist = data.FindAll(x => x.RRI_ID == Requested_Item_ID);
            }
            return itemrequestTaxlist;
        }//binding data to nested grid


        [HttpGet]
        public ActionResult Frm_Stock_Requisition_Item_Requested(int SR = 0, string ActionMethod = null)
        {
            ViewData["RR_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Approve");
            ViewData["RR_Supplier_Add_Right"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Add");
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);

            TaxGridSessionclear();
            Model_Requisition_Item_Requested model = new Model_Requisition_Item_Requested();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            
            if (ActionMethod == "_New")
            {
                model.Requisition_RequestedItem_Priority = "Normal";
                model.Requisition_RequestedItem_Reqd_Delivery_date = DateTime.Today.AddDays(1);
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                model.ActionMethod = Tag;
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_Get_RR_Items_MainGrid>)Requisition_ItemRequestedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_Get_RR_Items_MainGrid();
                model.Sr = Sr;
                model.Requisition_RequestedItem_Item_ID = dataToEdit.SubItemID;
                model.Requisition_RequestedItem_Item_Name = dataToEdit.Item_Name;
                model.Requisition_RequestedItem_Item_Type = dataToEdit.Item_Type;
                model.Requisition_RequestedItem_Item_Code = dataToEdit.Item_Code;
                model.Requisition_RequestedItem_Head = dataToEdit.Head;
                model.Requisition_RequestedItem_Make = dataToEdit.Make;
                model.Requisition_RequestedItem_Model = dataToEdit.Model;
                model.Requisition_RequestedItem_Supplier_ID = dataToEdit.SupplierID;
                model.Requisition_RequestedItem_Supplier = dataToEdit.Supplier;
                model.Requisition_RequestedItem_Delivery_Location_ID = dataToEdit.LocationID;
                model.Requisition_RequestedItem_Delivery_Location = dataToEdit.Destination_Location;
                model.Requisition_RequestedItem_Requested_Qty = Convert.ToDouble(dataToEdit.Requested_Qty);
                model.Requisition_RequestedItem_Approved_Qty = dataToEdit.Approved_Qty == 0 ? (double?)null : Convert.ToDouble(dataToEdit.Approved_Qty);
                model.Requisition_RequestedItem_Unit_ID = dataToEdit.Unit_ID;
                model.Requisition_RequestedItem_Unit = dataToEdit.Unit;
                model.Requisition_RequestedItem_Rate = dataToEdit.Rate == 0 ? (double?)null : Convert.ToDouble(dataToEdit.Rate);
                model.Requisition_RequestedItem_Rate_After_Discount = dataToEdit.Rate_after_Discount == 0 ? (double?)null : Convert.ToDouble(dataToEdit.Rate_after_Discount);
                model.Requisition_RequestedItem_Discount = dataToEdit.Discount == 0 ? (double?)null : Convert.ToDouble(dataToEdit.Discount);
                model.Requisition_RequestedItem_Taxes = Convert.ToDouble(dataToEdit.Tax);
                model.Requisition_RequestedItem_Tax_Percent = Convert.ToDouble(dataToEdit.TotalTaxPercent);
                model.Requisition_RequestedItem_Priority = dataToEdit.RRI_Priority;
                model.Requisition_RequestedItem_Reqd_Delivery_date = dataToEdit.Required_Delivery_Date;
                model.Requisition_RequestedItem_Amount = Convert.ToDouble(dataToEdit.Amount);
                model.AddedOn = dataToEdit.Added_On;
                model.AddedBy = dataToEdit.Added_By;
                model.Requisition_RequestedItem_ID = dataToEdit.ID;
                model.UODeptID = dataToEdit.UO_Dept_ID;
                model.UOSubDeptID = dataToEdit.UO_Sub_Dept_ID;
                model.Requisition_RequestedItem_Remarks = dataToEdit.Remarks;

                List<Return_Get_RR_Tax_Detail> taxgriddata = new List<Return_Get_RR_Tax_Detail>(); 


                var all_data_nested = (List<Return_Get_RR_Items_NestedGrid>)Requisition_ItemRequestedTaxData;
                List<Return_Get_RR_Items_NestedGrid> dataToEditnested = new List<Return_Get_RR_Items_NestedGrid>();
                if (all_data_nested != null)
                {
                    dataToEditnested = all_data_nested.FindAll(x => x.MainSr == Sr);
                }
                if (dataToEditnested != null)
                {

                    for (int I = 0; I <= dataToEditnested.Count() - 1; I++)
                    {
                        Return_Get_RR_Tax_Detail taxgrid = new Return_Get_RR_Tax_Detail();

                                           
                        taxgrid.Sr = dataToEditnested[I].Sr;
                        taxgrid.TaxPercent = dataToEditnested[I].TaxPercent;
                        taxgrid.TaxType = dataToEditnested[I].Tax_Type;
                        taxgrid.TaxTypeID = dataToEditnested[I].Tax_TypeID;
                        taxgrid.Tax_Amount = dataToEditnested[I].Tax_Amount;
                        taxgrid.Remarks = dataToEditnested[I].TaxRemarks;
                        taxgrid.ID = dataToEditnested[I].ID;
                        taxgriddata.Add(taxgrid);
                        Requisition_ItemRequestedTaxDetailData = taxgriddata;



                    }


                }

            
            }            
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Stock_Requisition_Item_Requested(Model_Requisition_Item_Requested model)
        {
            model.ActionMethod = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());
            List<Return_Get_RR_Items_MainGrid> gridRows = new List<Return_Get_RR_Items_MainGrid>();
            List<Return_Get_RR_Items_NestedGrid> gridRowsNested = new List<Return_Get_RR_Items_NestedGrid>();

            if (model.Requisition_RequestedItem_Discount != null && (model.Requisition_RequestedItem_Discount <= 0 || model.Requisition_RequestedItem_Discount > 100))
            {
                return Json(new
                {
                    result = false,
                    message = "Discount Promised Should Be Greater Than 0 and Less than 100...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_RequestedItem_Rate != null && model.Requisition_RequestedItem_Rate <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Rate Should Be Greater Than 0...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_RequestedItem_Approved_Qty != null && model.Requisition_RequestedItem_Approved_Qty < 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Approved Quantity Cannot Be Negative...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_RequestedItem_Approved_Qty != null && model.Requisition_RequestedItem_Approved_Qty > model.Requisition_RequestedItem_Requested_Qty)
            {
                return Json(new
                {
                    result = false,
                    message = "Requested Qty cannot be less than Approved Qty !!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_RequestedItem_Requested_Qty <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Requested Quantity Should Be Greater Than 0...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_RequestedItem_Taxes < 0 && model.Requisition_RequestedItem_Taxes > 100)
            {
                return Json(new
                {
                    result = false,
                    message = "Tax Cannot Be less than 0 and more than 100...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Requisition_ItemRequestedData != null)
            {
                gridRows = (List<Return_Get_RR_Items_MainGrid>)Requisition_ItemRequestedData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }

            if (Requisition_ItemRequestedTaxData != null)
            {
                gridRowsNested = (List<Return_Get_RR_Items_NestedGrid>)Requisition_ItemRequestedTaxData;
            }


            if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
            {
                Return_Get_RR_Items_MainGrid grid = new Return_Get_RR_Items_MainGrid();
                grid.Sr = NewSr;
                grid.Added_On = DateTime.Now;
                grid.Added_By = BASE._open_User_ID;
                grid.Item_Name = model.Requisition_RequestedItem_Item_Name;
                grid.Item_Code = model.Requisition_RequestedItem_Item_Code;
                grid.Item_Type = model.Requisition_RequestedItem_Item_Type;
                grid.Head = model.Requisition_RequestedItem_Head;
                grid.Make = model.Requisition_RequestedItem_Make;
                grid.Model = model.Requisition_RequestedItem_Model;
                grid.Requested_Qty = Convert.ToDecimal(model.Requisition_RequestedItem_Requested_Qty);
                grid.Approved_Qty = Convert.ToDecimal(model.Requisition_RequestedItem_Approved_Qty);
                grid.Supplier = model.Requisition_RequestedItem_Supplier;
                grid.Unit = model.Requisition_RequestedItem_Unit;
                grid.Destination_Location = model.Requisition_RequestedItem_Delivery_Location;
                grid.RRI_Priority = model.Requisition_RequestedItem_Priority;
                grid.Rate = Convert.ToDecimal(model.Requisition_RequestedItem_Rate);
                grid.Rate_after_Discount = Convert.ToDecimal(model.Requisition_RequestedItem_Rate_After_Discount);
                grid.Tax = Convert.ToDecimal(model.Requisition_RequestedItem_Taxes);
                grid.TotalTaxPercent = Convert.ToDecimal(model.Requisition_RequestedItem_Tax_Percent);
                grid.Discount = Convert.ToDecimal(model.Requisition_RequestedItem_Discount);
                grid.Amount = Convert.ToDecimal(model.Requisition_RequestedItem_Amount);
                grid.Required_Delivery_Date = Convert.ToDateTime(model.Requisition_RequestedItem_Reqd_Delivery_date);
                grid.ID = 0;
                grid.Unit_ID = model.Requisition_RequestedItem_Unit_ID;
                grid.SubItemID = Convert.ToInt32(model.Requisition_RequestedItem_Item_ID);
                grid.LocationID = model.Requisition_RequestedItem_Delivery_Location_ID;
                grid.SupplierID = model.Requisition_RequestedItem_Supplier_ID;
                grid.Remarks = model.Requisition_RequestedItem_Remarks;
                gridRows.Add(grid);


                List<Return_Get_RR_Tax_Detail> taxgriddata = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;

                if (taxgriddata != null)
                {

                    for (int I = 0; I <= taxgriddata.Count() - 1; I++)
                    {
                        Return_Get_RR_Items_NestedGrid nestedgrid = new Return_Get_RR_Items_NestedGrid();


                        nestedgrid.MainSr = NewSr;
                        nestedgrid.Sr = taxgriddata[I].Sr;
                        nestedgrid.TaxPercent = taxgriddata[I].TaxPercent;
                        nestedgrid.Tax_Type = taxgriddata[I].TaxType;
                        nestedgrid.Tax_TypeID = taxgriddata[I].TaxTypeID;
                        nestedgrid.Tax_Amount = taxgriddata[I].Tax_Amount;
                        nestedgrid.TaxRemarks = taxgriddata[I].Remarks;
                        nestedgrid.ID = taxgriddata[I].ID;

                        gridRowsNested.Add(nestedgrid);



                    }


                }
            }
            else if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                SaveUpdatedItemIDinSession(model.Requisition_RequestedItem_ID);
                dataToEdit.Item_Name = model.Requisition_RequestedItem_Item_Name;
                dataToEdit.Item_Code = model.Requisition_RequestedItem_Item_Code;
                dataToEdit.Item_Type = model.Requisition_RequestedItem_Item_Type;
                dataToEdit.Head = model.Requisition_RequestedItem_Head;
                dataToEdit.Make = model.Requisition_RequestedItem_Make;
                dataToEdit.Model = model.Requisition_RequestedItem_Model;
                dataToEdit.Requested_Qty = Convert.ToDecimal(model.Requisition_RequestedItem_Requested_Qty);
                dataToEdit.Approved_Qty = Convert.ToDecimal(model.Requisition_RequestedItem_Approved_Qty);
                dataToEdit.Supplier = model.Requisition_RequestedItem_Supplier;
                dataToEdit.Unit = model.Requisition_RequestedItem_Unit;
                dataToEdit.Destination_Location = model.Requisition_RequestedItem_Delivery_Location;
                dataToEdit.RRI_Priority = model.Requisition_RequestedItem_Priority;
                dataToEdit.Rate = Convert.ToDecimal(model.Requisition_RequestedItem_Rate);
                dataToEdit.Rate_after_Discount = Convert.ToDecimal(model.Requisition_RequestedItem_Rate_After_Discount);
                dataToEdit.Tax = Convert.ToDecimal(model.Requisition_RequestedItem_Taxes);
                dataToEdit.TotalTaxPercent = Convert.ToDecimal(model.Requisition_RequestedItem_Tax_Percent);
                dataToEdit.Discount = Convert.ToDecimal(model.Requisition_RequestedItem_Discount);
                dataToEdit.Amount = Convert.ToDecimal(model.Requisition_RequestedItem_Amount);
                dataToEdit.Required_Delivery_Date = Convert.ToDateTime(model.Requisition_RequestedItem_Reqd_Delivery_date);
                dataToEdit.Unit_ID = model.Requisition_RequestedItem_Unit_ID;
                dataToEdit.SubItemID = Convert.ToInt32(model.Requisition_RequestedItem_Item_ID);
                dataToEdit.LocationID = model.Requisition_RequestedItem_Delivery_Location_ID;
                dataToEdit.SupplierID = model.Requisition_RequestedItem_Supplier_ID;
                dataToEdit.Remarks = model.Requisition_RequestedItem_Remarks;


                //for nested grid edit

                List<Return_Get_RR_Items_NestedGrid> dataToEditnested = new List<Return_Get_RR_Items_NestedGrid>();
                if (gridRowsNested != null)
                {
                    dataToEditnested = gridRowsNested.FindAll(x => x.MainSr == model.Sr);
                }
                if (dataToEditnested != null)
                {

                    for (int I = 0; I <= dataToEditnested.Count() - 1; I++)
                    {

                        gridRowsNested.Remove(dataToEditnested[I]);


                    }




                    List<Return_Get_RR_Tax_Detail> taxgriddata = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;







                    if (taxgriddata != null)
                    {

                        for (int I = 0; I <= taxgriddata.Count() - 1; I++)
                        {
                            Return_Get_RR_Items_NestedGrid nestedgrid = new Return_Get_RR_Items_NestedGrid();


                            nestedgrid.MainSr = model.Sr;
                            nestedgrid.Sr = taxgriddata[I].Sr;
                            nestedgrid.TaxPercent = taxgriddata[I].TaxPercent;
                            nestedgrid.Tax_Type = taxgriddata[I].TaxType;
                            nestedgrid.Tax_TypeID = taxgriddata[I].TaxTypeID;
                            nestedgrid.Tax_Amount = taxgriddata[I].Tax_Amount;
                            nestedgrid.TaxRemarks = taxgriddata[I].Remarks;
                            nestedgrid.ID = taxgriddata[I].ID;

                            gridRowsNested.Add(nestedgrid);



                        }
                    }


                }


            }

            Requisition_ItemRequestedData = gridRows;

                Requisition_ItemRequestedTaxData = gridRowsNested;

                return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Frm_ItemRequested_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
        {
            var SR = Convert.ToInt16(sr);
            var allData = (List<Return_Get_RR_Items_MainGrid>)Requisition_ItemRequestedData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_Get_RR_Items_MainGrid();

            List<Return_Get_RR_Items_NestedGrid> all_data_Of_Nested = (List<Return_Get_RR_Items_NestedGrid>)Requisition_ItemRequestedTaxData;
            var dataToDeletenested = new List<Return_Get_RR_Items_NestedGrid>();


            if (all_data_Of_Nested != null)
            {
               
                    dataToDeletenested = all_data_Of_Nested.FindAll(x => x.MainSr == SR);
                
            }

            


            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }

            for (int i = 0; i <= dataToDeletenested.Count() - 1; i++)
            {
                all_data_Of_Nested.Remove(dataToDeletenested[i]);
            }

            Requisition_ItemRequestedData = allData;

            Requisition_ItemRequestedTaxData = all_data_Of_Nested;

            if (id != 0)
            {
                var deleteItemEstID = new int[1];
                var deleteItemEst = Delete_RequisitionExisting_ItemRequested_ID as int[];
                if (deleteItemEst != null)
                {
                    Array.Resize(ref deleteItemEst, deleteItemEst.Length + 1);
                    deleteItemEst[deleteItemEst.Length - 1] = id;
                    Delete_RequisitionExisting_ItemRequested_ID = deleteItemEst;
                }
                else
                {
                    deleteItemEstID[0] = id;
                    Delete_RequisitionExisting_ItemRequested_ID = deleteItemEstID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
       
    
        public JsonResult Requisition_Approve_Items_qty(string ButtonClicked, string SelectedSrNo)
        {
            try
            {

                if (Requisition_ItemRequestedData == null)
                {
                    return Json(new { Message = "Grid Data Incorrect!!", result = false }, JsonRequestBehavior.AllowGet);
                }
                List<Return_Get_RR_Items_MainGrid> RequestedItemData = (List<Return_Get_RR_Items_MainGrid>)Requisition_ItemRequestedData;
                if (RequestedItemData.Count == 0)
                {
                    return Json(new { Message = "No Items present to be Approved !!", result = false }, JsonRequestBehavior.AllowGet);
                }
                switch (ButtonClicked)
                {
                    case "Approve":
                        if (SelectedSrNo.Trim().Length == 0)
                        {
                            return Json(new { Message = "No Items Selected !!", result = false }, JsonRequestBehavior.AllowGet);
                        }
                        List<long> SelectedSrNoArray = SelectedSrNo.Split(',').Select(long.Parse).ToList();
                        foreach (Return_Get_RR_Items_MainGrid cItem in RequestedItemData)
                        {
                            if (SelectedSrNoArray.Contains(cItem.Sr))
                            {
                                cItem.Approved_Qty = cItem.Requested_Qty;
                                SaveUpdatedItemIDinSession(cItem.ID);

                                //calculate taxes
                                
                               cItem.Tax = ((cItem.Approved_Qty * cItem.Rate_after_Discount) * cItem.TotalTaxPercent) / 100;


                                //calculate amount

                                cItem.Amount = (cItem.Approved_Qty * cItem.Rate_after_Discount ) + cItem.Tax;
                            }
                        }
                        break;
                    case "Approve All":
                        foreach (Return_Get_RR_Items_MainGrid cItem in RequestedItemData)
                        {
                            if (cItem.Approved_Qty == 0 || cItem.Approved_Qty == null || cItem.Approved_Qty != cItem.Requested_Qty)//Mantis bug 0000884 fixed
                            {
                                cItem.Approved_Qty = cItem.Requested_Qty;
                                SaveUpdatedItemIDinSession(cItem.ID);

                                //calculate taxes

                                cItem.Tax = ((cItem.Approved_Qty * cItem.Rate_after_Discount) * cItem.TotalTaxPercent) / 100;


                                //calculate amount

                                cItem.Amount = (cItem.Approved_Qty * cItem.Rate_after_Discount) + cItem.Tax;

                            }
                        }
                        break;
                }
                Requisition_ItemRequestedData = RequestedItemData;
                ViewData["Item_Requested_Count"] = RequestedItemData.Count();//Mantis bug 0000884 fixed                
                return Json(new { Message = "Items Approved!!", result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Get_Item_Requested(DataSourceLoadOptions loadOptions, int? storeid)
        {
            if (storeid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.SubItems.Return_GetStoreItems>(), loadOptions)), "application/json");
            }
            Param_GetStoreItems inparam = new Param_GetStoreItems();

            inparam.StoreID = storeid;
          
            List<DbOperations.SubItems.Return_GetStoreItems> StockConData = BASE._Sub_Item_DBOps.GetStoreItems(inparam, Common_Lib.RealTimeService.ClientScreen.Stock_RR);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StockConData, loadOptions)), "application/json");
        }
        public ActionResult Get_Suppliers(DataSourceLoadOptions loadOptions,int? Param_ItemID=0)//Mantis bug 0001194 fixed
        {
            List<DbOperations.Suppliers.Return_GetItemMappedSuppliers> SupplierData = BASE._StockSupplier_dbops.GetItemMappedSuppliers(Param_ItemID);//Mantis bug 0001194 fixed
            if (SupplierData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Suppliers.Return_GetItemMappedSuppliers>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SupplierData, loadOptions)), "application/json");
        }
        public ActionResult Get_Locations(DataSourceLoadOptions loadOptions, string StoreID_UO_DeptID)
        {
            if (StoreID_UO_DeptID.Length == 0)
            { return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockDeptStores.Return_GetLocations>(), loadOptions)), "application/json"); }
            Int32 StoreID = Convert.ToInt32(StoreID_UO_DeptID.Split(',')[0]);
            Int32 UO_DeptID = 0;
            Int32 UO_SubdeptID = 0;
            if (StoreID_UO_DeptID.Split(',').Length > 1)
            {
                if (StoreID_UO_DeptID.Split(',')[1].Length > 0)
                {
                    UO_DeptID = Convert.ToInt32(StoreID_UO_DeptID.Split(',')[1]);
                    
                }
                if (StoreID_UO_DeptID.Split(',')[2].Length > 0)
                {
                    UO_SubdeptID = Convert.ToInt32(StoreID_UO_DeptID.Split(',')[2]);

                }
            }
            List<DbOperations.StockDeptStores.Return_GetLocations> LocationData = BASE._RR_DBOps.Get_RR_Locations(StoreID, UO_DeptID, UO_SubdeptID);
            if (LocationData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockDeptStores.Return_GetLocations>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(LocationData, loadOptions)), "application/json");
        }
        public ActionResult Get_Units(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockProfile.Return_GetUnits> UnitData = BASE._Stock_Profile_DBOps.GetUnits();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UnitData, loadOptions)), "application/json");
        }

        public ActionResult TaxGridData( int subitemID = 0, int RR_ItemID = 0)
        {

            var RRTaxList = new List<Return_Get_RR_Tax_Detail>();
            var RRID = ViewData["RR_ID"];
            int RRItemID = RR_ItemID;
            int subitem = subitemID;
           
            if (Requisition_ItemRequestedTaxDetailData == null)
            {
                List<Return_Get_RR_Tax_Detail> RRtaxData = BASE._RR_DBOps.Get_RR_Tax_Detail(Convert.ToInt32(RRID), RRItemID, subitem);
                if (RRtaxData != null)
                {
                    RRTaxList = RRtaxData;
                }
                Requisition_ItemRequestedTaxDetailData = RRTaxList;
            }
            return PartialView(Requisition_ItemRequestedTaxDetailData);
        }

        public ActionResult Get_TaxType(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockRequisitionRequest.Return_GetTaxType> Taxdata = BASE._RR_DBOps.GetTaxType();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Taxdata, loadOptions)), "application/json");
        }
        public void SaveUpdatedItemIDinSession(int Updated_RequestedItem_ID)
        {
            if (Updated_RequestedItem_ID != 0)
            {
                var editItemRequestedID = new ArrayList();
                var editItemRequested = RequisitionEdit_ItemRequested_ID as ArrayList;
                if (editItemRequested != null)
                {
                    editItemRequested.Add(Updated_RequestedItem_ID);
                    RequisitionEdit_ItemRequested_ID = editItemRequested;
                }
                else
                {
                    editItemRequestedID.Add(Updated_RequestedItem_ID);
                    RequisitionEdit_ItemRequested_ID = editItemRequestedID;
                }
            }

        }
      
        [HttpGet]
        public ActionResult Frm_Requisition_Request_Add_Taxes(string ActionMethod = null, int SR = 0)
        {
            Model_Requisition_Item_Requested model = new Model_Requisition_Item_Requested();

           model.RR_Taxes_ActionMethod =  ActionMethod;

            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_Get_RR_Tax_Detail();
                model.Sr = Sr;
                model.Requisition_Addtax_TaxPercent = dataToEdit.TaxPercent;
                model.Requisition_Addtax_TaxType = dataToEdit.TaxType;
                model.Requisition_Addtax_TaxTypeID = dataToEdit.TaxTypeID;
                model.Requisition_Addtax_TaxAmount = Convert.ToDouble(dataToEdit.Tax_Amount);
                model.Requisition_Addtax_Remarks = dataToEdit.Remarks;              
                model.Requisition_Addtax_RecID  = dataToEdit.ID;
               
            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Requisition_Request_Add_Taxes(Model_Requisition_Item_Requested model)
        {
          
            List<Return_Get_RR_Tax_Detail> gridRows = new List<Return_Get_RR_Tax_Detail>();

            if (model.Requisition_Addtax_TaxPercent != 0 && (model.Requisition_Addtax_TaxPercent <= 0 || model.Requisition_Addtax_TaxPercent > 100))
            {
                return Json(new
                {
                    result = false,
                    message = "Tax Percentage Should Be Greater Than 0 and Less than 100...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Requisition_Addtax_TaxTypeID == null )
            {
                return Json(new
                {
                    result = false,
                    message = "Tax Type is a Mandatory Field."
                }, JsonRequestBehavior.AllowGet);
            }
            
         
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Requisition_ItemRequestedTaxDetailData != null)
            {
                gridRows = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (model.RR_Taxes_ActionMethod == "_New"  || model.RR_Taxes_ActionMethod == "_NewAddTax")
            {
                Return_Get_RR_Tax_Detail grid = new Return_Get_RR_Tax_Detail();
                grid.Sr = NewSr;
                grid.TaxPercent = model.Requisition_Addtax_TaxPercent;
                grid.TaxType = model.Requisition_Addtax_TaxType;
                grid.TaxTypeID = model.Requisition_Addtax_TaxTypeID;
                grid.Tax_Amount = Convert.ToDecimal(model.Requisition_Addtax_TaxAmount);
                grid.Remarks = model.Requisition_Addtax_Remarks;
                grid.ID = 0;
              
                gridRows.Add(grid);
            }
            else if (model.RR_Taxes_ActionMethod == "_Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);

                dataToEdit.TaxPercent = model.Requisition_Addtax_TaxPercent;
                dataToEdit.TaxType = model.Requisition_Addtax_TaxType;
                dataToEdit.TaxTypeID = model.Requisition_Addtax_TaxTypeID;
                dataToEdit.Tax_Amount = Convert.ToDecimal(model.Requisition_Addtax_TaxAmount);
                dataToEdit.Remarks = model.Requisition_Addtax_Remarks;
               
            }
            Requisition_ItemRequestedTaxDetailData = gridRows;

            var actionmethodvalue = model.RR_Taxes_ActionMethod;
            return Json(new
            {
                result = true,
                message = "Saved Successfully",
                action = actionmethodvalue
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Frm_ItemRequested_Tax_Window_Delete_Grid_Record(string ActionMethod, string Sr = null, int ID = 0)
        {
            var SR = Convert.ToInt16(Sr);
            var allData = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_Get_RR_Tax_Detail();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Requisition_ItemRequestedTaxDetailData = allData;
         
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Requisition_Request_Manage_Taxes(int SubitemID)
        {
            Model_Requisition_Item_Requested model = new Model_Requisition_Item_Requested();
            model.Requisition_RequestedItem_Item_ID = SubitemID;
            return PartialView(model);
        }

        public ActionResult CalculateTotalTaxAmount()
        {
            decimal? sum = 0;
            decimal? sum_tax_percent = 0;
            var all_data_Of_tax_Grid = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;
            if (all_data_Of_tax_Grid != null)
            {
                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {
                    
                        sum = sum + all_data_Of_tax_Grid[I].Tax_Amount;
                    sum_tax_percent = sum_tax_percent + all_data_Of_tax_Grid[I].TaxPercent;

                }

            }

            return Json(new
            {
                Amount = sum,
                TaxPercent = sum_tax_percent,

            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CalculateTaxes(Decimal Quantity, Decimal? Rate, Decimal? RateAfterDiscount)
        {
            decimal? sum = 0;
            decimal? sum_tax_percent = 0;
            var all_data_Of_tax_Grid = (List<Return_Get_RR_Tax_Detail>)Requisition_ItemRequestedTaxDetailData;
            if (all_data_Of_tax_Grid != null)
            {


                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {
                   var taxpercent = all_data_Of_tax_Grid[I].TaxPercent;

                 
                     var  TaxAmt = ((Quantity * RateAfterDiscount) * taxpercent) / 100;
                    

                    all_data_Of_tax_Grid[I].Tax_Amount = TaxAmt;
                }

                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {

                    sum = sum + all_data_Of_tax_Grid[I].Tax_Amount;
                    sum_tax_percent = sum_tax_percent + all_data_Of_tax_Grid[I].TaxPercent;

                }

               

            }

            return Json(new
            {
                Amount = sum,
                TaxPercent = sum_tax_percent,
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Linked User Orders"
        public ActionResult RequisitionLinkedUserOrderGridData(string ActionMethodName, int RR_ID = 0)
        {
            var LinkedUserOrderList = new List<Return_Get_RR_Linked_UO>();
            if (ActionMethodName == "_New")
            {
                return PartialView(LinkedUserOrderList);
            }
            if (Requisition_LinkedUserOrderData == null)
            {
                LinkedUserOrderList = BASE._RR_DBOps.Get_RR_Linked_UO(RR_ID);
                Requisition_LinkedUserOrderData = LinkedUserOrderList;
            }
            var data = (List<Return_Get_RR_Linked_UO>)Requisition_LinkedUserOrderData;
            for (int i = 0; i < data.Count(); i++)
            {
                data[i].Sr = i + 1;
            }
            Requisition_LinkedUserOrderData = data;
            return PartialView(Requisition_LinkedUserOrderData);
        }
        #endregion
        #region "Remarks"
        public ActionResult RequisitionExistingRemarksGridData(string ActionMethodName, int RR_ID = 0)
        {
            var RemarksList = new List<Return_GetRequisitionRemarks>();
            if (ActionMethodName == "_New")
            {
                return PartialView(RemarksList);
            }
            if (RequisitionExisting_Remarks_Grid_Data == null)
            {
                var RemarksData = BASE._RR_DBOps.GetRequisitionRemarks(RR_ID);
                if (RemarksData != null)
                {
                    RemarksList = RemarksData;
                }
                RequisitionExisting_Remarks_Grid_Data = RemarksList;
            }
            return PartialView(RequisitionExisting_Remarks_Grid_Data);
        }
        public ActionResult FrmRequisition_ExistingRemarks_Window_Delete_Grid_Record(string ActionMethod, int? ID = 0)
        {
            var id = Convert.ToInt16(ID);
            var allData = (List<Return_GetRequisitionRemarks>)RequisitionExisting_Remarks_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == id).FirstOrDefault() : new Return_GetRequisitionRemarks();
            if (dataToDelete.Remarks_By != BASE._open_User_ID)
            {
                return Json(new
                {
                    result = false,
                    message = "A user is allowed to delete his own remarks only..!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            RequisitionExisting_Remarks_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new List<int>();
                var deleteExistingRemarks = Delete_RequisitionExisting_Remarks_ID as List<int>;
                if (deleteExistingRemarks != null)
                {
                    deleteExistingRemarks.Add(id);
                    Delete_RequisitionExisting_Remarks_ID = deleteExistingRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_RequisitionExisting_Remarks_ID = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FrmRequisition_ViewRemarks(int sr = 0)
        {
            var all_data = (List<Return_GetRequisitionRemarks>)RequisitionExisting_Remarks_Grid_Data;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == sr).FirstOrDefault() : new Return_GetRequisitionRemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("FrmRequisition_ViewRemarks", ViewBag.ViewRemarks);
        }

        #endregion
        #region "Documents"
        public ActionResult RequisitionDocumentsGridData(string ActionMethodName, int RR_ID = 0)
        {
            var docList = new List<DbOperations.Return_GetDocumentsGridData>();
            if (ActionMethodName == "_New")
            {
                return PartialView(docList);
            }
            if (Requisition_Documents_Window_Grid_Data == null)
            {
                var docData = BASE._RR_DBOps.GetRequisitionDocuments(RR_ID);
                if (docData != null)
                {
                    docList = docData;
                }
                Requisition_Documents_Window_Grid_Data = docList;
            }

            return PartialView(Requisition_Documents_Window_Grid_Data);
        }
        public ActionResult Requisition_Documents_Attachment(string ActionMethod = null, string ID = null)
        {
            Model_Attachment_Window model = (Model_Attachment_Window)GetBaseSession("Requisition_Documents_Attachment_AttachmentData");
            List<Return_GetDocumentsGridData> gridRows = new List<Return_GetDocumentsGridData>();
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Requisition_Documents_Window_Grid_Data != null)
            {
                gridRows = (List<Return_GetDocumentsGridData>)Requisition_Documents_Window_Grid_Data;
                gridRowsCount = gridRows.Count;
                LastRowSr = LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr_No) : 0;
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
            else if (model.ActionMethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr_No == model.Sr_no);
                var editDocumentID = new string[1];
                if (ID != null || ID != "0")
                {
                    var editDocument = RequisitionEdit_Document_ID as string[];
                    if (editDocument != null)
                    {
                        Array.Resize(ref editDocument, editDocument.Length + 1);
                        editDocument[editDocument.Length - 1] = model.ID;
                        RequisitionEdit_Document_ID = editDocument;
                    }
                    else
                    {
                        editDocumentID[0] = (model.ID);
                        RequisitionEdit_Document_ID = editDocumentID;
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
            Requisition_Documents_Window_Grid_Data = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_DocumentsDetail_Window_Delete_Grid_Record(string ActionMethod, string SrID = null, string Doc_ID = null)
        {
            var srid = Convert.ToInt32(SrID);

            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)Requisition_Documents_Window_Grid_Data;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (dataToDelete != null)
            {
                allDocData.Remove(dataToDelete);
            }
            Requisition_Documents_Window_Grid_Data = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    string[] deleteDocumentID = new string[1];
                    var deleteDocument = RequisitionDelete_Document_ID as string[];
                    if (deleteDocument != null)
                    {
                        Array.Resize(ref deleteDocument, deleteDocument.Length + 1);
                        deleteDocument[deleteDocument.Length - 1] = Doc_ID;
                        RequisitionDelete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID[0] = Doc_ID;
                        RequisitionDelete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {
                string[] UnlinkDocumentID = new string[1];
                var UnlinkDocument = RequisitionUnlink_Document_ID as string[];
                if (UnlinkDocument != null)
                {
                    Array.Resize(ref UnlinkDocument, UnlinkDocument.Length + 1);
                    UnlinkDocument[UnlinkDocument.Length - 1] = Doc_ID;
                    RequisitionUnlink_Document_ID = UnlinkDocument;
                }
                else
                {
                    UnlinkDocumentID[0] = Doc_ID;
                    RequisitionUnlink_Document_ID = UnlinkDocumentID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RR_Documents_Attachment_LinkCheck(string DocId, int RRId)
        {
            var screen = BASE._RR_DBOps.GetAttachmentLinkScreen(RRId, DocId);
            if (!string.IsNullOrEmpty(screen))
            {
                if (screen != "Requisition")
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From Requisition..?"
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
        #endregion
        #endregion
        #region "IUD Functions"
        public void IUDDocuments(ref Param_Insert_RequisitionRequest_Txn InParam, ref Param_Update_RequisitionRequest_Txn UpParam, string actionmethod)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Requisition_Documents_Window_Grid_Data;
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
                        InEInfo.Ref_Screen = "Requisition";
                        if (RR_ID != 0) InEInfo.Ref_Rec_ID = RR_ID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments[index] = InEInfo;
                        index++;

                    }
                }
                if (actionmethod == "_New")
                {
                    InParam.Added_Attachments = insertAttachments;
                }
                if (actionmethod == "_Edit")
                {
                    var doceditid = RequisitionEdit_Document_ID as string[];
                    if (doceditid != null)
                    {
                        var updateattachment = new Parameter_Update_Attachment[doceditid.Count()];
                        for (int j = 0; j < doceditid.Count(); j++)
                        {
                            for (int i = 0; i < DocumentsData.Count; i++)
                            {
                                if (DocumentsData[i].ID == doceditid[j])
                                {
                                    var InEInfo = new Parameter_Update_Attachment();
                                    InEInfo.FileName = DocumentsData[i].File_Name;
                                    InEInfo.Description = DocumentsData[i].Remarks;
                                    InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                                    InEInfo.Ref_Screen = "Requisition";
                                    if (RR_ID != 0) InEInfo.Ref_Rec_ID = RR_ID.ToString();
                                    InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                                    InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                                    InEInfo.File = DocumentsData[i].File_Array;
                                    InEInfo.RecID = DocumentsData[i].ID;
                                    updateattachment[j] = InEInfo;
                                }
                            }
                        }
                        UpParam.Updated_Attachments = updateattachment;
                    }
                    UpParam.Added_Attachments = insertAttachments;
                    if (RequisitionDelete_Document_ID != null)
                    {
                        UpParam.Deleted_Attachment_IDs = RequisitionDelete_Document_ID as string[];
                    }
                    if(RequisitionUnlink_Document_ID!=null)
                    {
                        UpParam.Unlinked_Attachment_IDs= RequisitionUnlink_Document_ID as string[];
                    }
                }
            }
        }
        public void IUDItemRequested(ref Param_Insert_RequisitionRequest_Txn InParam, ref Param_Update_RequisitionRequest_Txn UpParam, string actionmethod)
        {
            var index = 0;

            var ItemRequestedData = (List<Return_Get_RR_Items_MainGrid>)Requisition_ItemRequestedData;

            var all_data_Of_NestedItemGrid = (List<Return_Get_RR_Items_NestedGrid>)Requisition_ItemRequestedTaxData;
            Session["UO_GoodsDeliveredNestedData"] = Requisition_ItemRequestedTaxData;

            if (ItemRequestedData != null)
            {
                var insertItemRequested = new Param_InsertRequisitionRequestItem[ItemRequestedData.Count()];
                for (int i = 0; i < ItemRequestedData.Count; i++)
                {
                    if (ItemRequestedData[i].ID == 0)
                    {
                        var InEInfo = new Param_InsertRequisitionRequestItem();
                        InEInfo.supplier_ID = ItemRequestedData[i].SupplierID;
                        InEInfo.Make = ItemRequestedData[i].Make;
                        InEInfo.Model = ItemRequestedData[i].Model;
                        InEInfo.Qty_Requested = ItemRequestedData[i].Requested_Qty;
                        InEInfo.Qty_Approved = ItemRequestedData[i].Approved_Qty;
                        InEInfo.Unit_ID = ItemRequestedData[i].Unit_ID;
                        InEInfo.Rate = ItemRequestedData[i].Rate;
                        InEInfo.Discount_Promised = ItemRequestedData[i].Discount;
                        InEInfo.Taxes = ItemRequestedData[i].Tax;
                        InEInfo.Amount = ItemRequestedData[i].Amount;
                        InEInfo.Priority = ItemRequestedData[i].RRI_Priority;
                        InEInfo.Reqd_Delivery_Date = ItemRequestedData[i].Required_Delivery_Date;
                        InEInfo.Remarks = ItemRequestedData[i].Remarks;
                        InEInfo.Sub_Item_ID = ItemRequestedData[i].SubItemID;
                        InEInfo.Dest_Location_ID = ItemRequestedData[i].LocationID;


                        //nested
                        if (all_data_Of_NestedItemGrid != null)
                        {

                            var insertNestedItem = new Param_Insert_Tax_Details[all_data_Of_NestedItemGrid.Count()];
                            for (int J = 0; J <= all_data_Of_NestedItemGrid.Count - 1; J++)
                            {
                                if (all_data_Of_NestedItemGrid[J].RRI_ID == 0)
                                {

                                    if (ItemRequestedData[i].Sr == all_data_Of_NestedItemGrid[J].MainSr)
                                    {

                                        var IteminsertNestedData = new Param_Insert_Tax_Details();

                                        IteminsertNestedData.RequestedItem_ID = all_data_Of_NestedItemGrid[J].RRI_ID;
                                        IteminsertNestedData.TaxPercent = all_data_Of_NestedItemGrid[J].TaxPercent;
                                        IteminsertNestedData.TaxRemarks = all_data_Of_NestedItemGrid[J].TaxRemarks;
                                        IteminsertNestedData.TaxTypeID = all_data_Of_NestedItemGrid[J].Tax_TypeID;
                                        insertNestedItem[J] = IteminsertNestedData;
                                    }
                                }
                            }
                            //param_Insert_UO_Delivery._Delivered_Stock = insertNestedDeli;
                            InEInfo._Item_Taxes = insertNestedItem;
                        }



                        //



                        insertItemRequested[index] = InEInfo;
                        index++;
                    }
                }
                if (actionmethod == "_New")
                {
                    InParam.Added_Items_Requested = insertItemRequested;
                }
                if (actionmethod == "_Edit")
                {
                    ArrayList ItemRequestededitid = RequisitionEdit_ItemRequested_ID as ArrayList;
                    if (ItemRequestededitid != null)
                    {
                        var updateRequisitionRequested = new Param_UpdateRequisitionRequestItem[ItemRequestededitid.Count];
                        for (int j = 0; j < ItemRequestededitid.Count; j++)
                        {
                            for (int i = 0; i < ItemRequestedData.Count; i++)
                            {
                                if (ItemRequestedData[i].ID == (int)ItemRequestededitid[j])
                                {
                                    var InEInfo = new Param_UpdateRequisitionRequestItem();
                                    InEInfo.RRI_ID = ItemRequestedData[i].ID;
                                    InEInfo.supplier_ID = ItemRequestedData[i].SupplierID;
                                    InEInfo.Make = ItemRequestedData[i].Make;
                                    InEInfo.Model = ItemRequestedData[i].Model;
                                    InEInfo.Qty_Requested = ItemRequestedData[i].Requested_Qty;
                                    InEInfo.Qty_Approved = ItemRequestedData[i].Approved_Qty;
                                    InEInfo.Unit_ID = ItemRequestedData[i].Unit_ID;
                                    InEInfo.Rate = ItemRequestedData[i].Rate;
                                    InEInfo.Discount_Promised = ItemRequestedData[i].Discount;
                                    InEInfo.Taxes = ItemRequestedData[i].Tax;
                                    InEInfo.Amount = ItemRequestedData[i].Amount;
                                    InEInfo.Priority = ItemRequestedData[i].RRI_Priority;
                                    InEInfo.Reqd_Delivery_Date = ItemRequestedData[i].Required_Delivery_Date;
                                    InEInfo.Remarks = ItemRequestedData[i].Remarks;
                                    InEInfo.Sub_Item_ID = ItemRequestedData[i].SubItemID;
                                    InEInfo.Dest_Location_ID = ItemRequestedData[i].LocationID;

                                    //nested
                                    if (all_data_Of_NestedItemGrid != null)
                                    {

                                        var insertNested = new Param_Insert_Tax_Details[all_data_Of_NestedItemGrid.Count()];
                                        for (int J = 0; J <= all_data_Of_NestedItemGrid.Count - 1; J++)
                                        {
                                            if (all_data_Of_NestedItemGrid[J].RRI_ID == 0 || all_data_Of_NestedItemGrid[J].RRI_ID == ItemRequestedData[i].ID)
                                            {

                                                if (ItemRequestedData[i].Sr == all_data_Of_NestedItemGrid[J].MainSr)
                                                {

                                                    var IteminsertNestedData = new Param_Insert_Tax_Details();

                                                    IteminsertNestedData.RequestedItem_ID = all_data_Of_NestedItemGrid[J].RRI_ID;
                                                    IteminsertNestedData.TaxPercent = all_data_Of_NestedItemGrid[J].TaxPercent;
                                                    IteminsertNestedData.TaxTypeID = all_data_Of_NestedItemGrid[J].Tax_TypeID;
                                                    IteminsertNestedData.TaxRemarks = all_data_Of_NestedItemGrid[J].TaxRemarks;
                                                    insertNested[J] = IteminsertNestedData;
                                                }
                                            }
                                        }

                                        InEInfo._Added_Item_Taxes = insertNested;
                                    }

                                    

                                    updateRequisitionRequested[j] = InEInfo;
                                }
                            }
                        }
                        UpParam.Updated_Items_Requested = updateRequisitionRequested;
                    }
                    UpParam.Added_Items_Requested = insertItemRequested;
                    if (Delete_RequisitionExisting_ItemRequested_ID != null)
                    {
                        UpParam.Deleted_Items_Requested_IDs = Delete_RequisitionExisting_ItemRequested_ID as int[];
                    }
                }
            }
        }
        public void IUDRemainingData(ref Param_Insert_RequisitionRequest_Txn InParam, ref Param_Update_RequisitionRequest_Txn UpParam, Model_NEVD_Requisition model)
        {
            if (model != null && model.TempActionMethod == "_New")
            {
                InParam.RR_Date = model.RequisitionDate;
                InParam.Project_ID = model.RequisitionProjectID;
                InParam.Job_ID = model.RequisitionJobID;
                InParam.Requestor_ID = (int)model.RequisitionRequestorID;
                InParam.RR_Type = model.RequisitionType;
                InParam.Purchased_by_ID = (int?)model.RequisitionPurchasedByID;
                InParam.Trf_From_Dept_ID = model.RequisitionTransferFromDeptID;
                InParam.Requesting_Dept_ID = (int)model.RequisitionRequestorStoreDeptID;
                InParam.Remarks = model.RequisitionRemarks;
                InParam.Special_Discount = model.RequisitionSpecialDiscount;
            }
            else
            {
                UpParam.RR_ID = model.RR_ID;
                UpParam.RR_Date = model.RequisitionDate;
                UpParam.Project_ID = model.RequisitionProjectID;
                UpParam.Job_ID = model.RequisitionJobID;
                UpParam.Requestor_ID = (int)model.RequisitionRequestorID;
                UpParam.RR_Type = model.RequisitionType;
                UpParam.Purchased_by_ID = (int?)model.RequisitionPurchasedByID;
                UpParam.Trf_From_Dept_ID = model.RequisitionTransferFromDeptID;
                UpParam.Requesting_Dept_ID = (int)model.RequisitionRequestorStoreDeptID;
                UpParam.Remarks = model.RequisitionRemarks;
                UpParam.Special_Discount = model.RequisitionSpecialDiscount;
                if (Delete_RequisitionExisting_Remarks_ID != null)
                {
                    UpParam.Deleted_Remarks_IDs = ((List<int>)Delete_RequisitionExisting_Remarks_ID).ToArray();
                }
            }
        }
        #endregion
        #region "Misc Functions"
        public void Sessionclear()
        {
            Session.Remove("RR_Documents");
            Session.Remove("RRisApprovalRight");
            BASE._SessionDictionary.Remove("Requisition_Documents_Attachment_AttachmentData");
            BASE._SessionDictionary.Remove("RR_Documents_AttachmentData");
            ClearBaseSession("_RR");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_RRInfo");
        }
        public void TaxGridSessionclear()
        {
            
            ClearBaseSession("_RR_Tax");
        } //clears session variable on popup close

        public void RR_user_rights()
        {
            ViewData["RR_AddRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Add");
            ViewData["RR_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Update");
            ViewData["RR_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "View");
            ViewData["RR_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Delete");
            ViewData["RR_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Export");
            ViewData["RR_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Report");
            ViewData["RR_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Approve");
        }
        public bool CheckApproverRights()
        {
            return CheckRights(BASE, ClientScreen.Stock_RR, "Approve");
        }
        #endregion

    }
}