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
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.StockPurchaseOrder;
using static Common_Lib.DbOperations.StockDeptStores;
using static Common_Lib.DbOperations.Suppliers;
using static Common_Lib.DbOperations.StockRequisitionRequest;
using static Common_Lib.DbOperations.StockUserOrder;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class PurchaseRegisterController : BaseController
    {
        // GET: Stock/PurchaseRegister
        #region Global Variables
        public string PO_RequisitionID
        {
            get
            {
                return (string)GetBaseSession("PO_RequisitionID_PO");
            }
            set
            {
                SetBaseSession("PO_RequisitionID_PO", value);
            }
        }
        public DateTime? PurchaseFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("PurchaseFromDate_POInfo");
            }
            set
            {
                SetBaseSession("PurchaseFromDate_POInfo", value);
            }
        }
        public DateTime? PurchaseToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("PurchaseToDate_POInfo");
            }
            set
            {
                SetBaseSession("PurchaseToDate_POInfo", value);
            }
        }
        public List<StockPurchaseOrder.Return_GetRegister_MainGrid> PurchaseRegister_Data_Glob
        {
            get
            {
                return (List<StockPurchaseOrder.Return_GetRegister_MainGrid>)GetBaseSession("PurchaseRegister_Data_Glob_POInfo");
            }
            set
            {
                SetBaseSession("PurchaseRegister_Data_Glob_POInfo", value);
            }
        }
        public List<StockPurchaseOrder.Return_GetRegister_NestedGrid> ItemPurchase_ExportData
        {
            get
            {
                return (List<StockPurchaseOrder.Return_GetRegister_NestedGrid>)GetBaseSession("ItemPurchase_ExportData_POInfo");
            }
            set
            {
                SetBaseSession("ItemPurchase_ExportData_POInfo", value);
            }
        }
        public string Currentuserrole
        {
            get
            {
                return (string)GetBaseSession("Currentuserrole_PO");
            }
            set
            {
                SetBaseSession("Currentuserrole_PO", value);
            }
        }
        public int PO_ID_Glob
        {
            get
            {
                return (int)GetBaseSession("PO_ID_Glob_PO");
            }
            set
            {
                SetBaseSession("PO_ID_Glob_PO", value);
            }
        }
        public List<int> Delete_PurchaseExisting_Remarks_ID
        {
            get
            {
                return (List<int>)GetBaseSession("Delete_PurchaseExisting_Remarks_ID_PO");
            }
            set
            {
                SetBaseSession("Delete_PurchaseExisting_Remarks_ID_PO", value);
            }
        }
        public List<Return_GetDocumentsGridData> Purchase_Documents_Window_Grid_Data_PO
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("Purchase_Documents_Window_Grid_Data_PO");
            }
            set
            {
                SetBaseSession("Purchase_Documents_Window_Grid_Data_PO", value);
            }
        }
        public ArrayList PurchaseEdit_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseEdit_Document_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseEdit_Document_ID_PO", value);
            }
        }
        public ArrayList PurchaseDelete_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseDelete_Document_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseDelete_Document_ID_PO", value);
            }
        }
        public ArrayList PurchaseUnlink_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseUnlink_Document_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseUnlink_Document_ID_PO", value);
            }
        }
        public List<Return_Get_PO_Items_MainGrid> Purchase_ItemOrderedData
        {
            get
            {
                return (List<Return_Get_PO_Items_MainGrid>)GetBaseSession("Purchase_ItemOrderedData_PO");
            }
            set
            {
                SetBaseSession("Purchase_ItemOrderedData_PO", value);
            }
        }

        public List<Return_Get_PO_Items_NestedGrid> PO_IO_NestedGridTaxData
        {
            get
            {
                return (List<Return_Get_PO_Items_NestedGrid>)GetBaseSession("PO_IO_NestedGridTaxData_PO");
            }
            set
            {
                SetBaseSession("PO_IO_NestedGridTaxData_PO", value);
            }
        }
        public ArrayList PurchaseEdit_ItemOrd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseEdit_ItemOrd_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseEdit_ItemOrd_ID_PO", value);
            }
        }
        public ArrayList Delete_ItemOrd_Pur
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_ItemOrd_Pur_PO");
            }
            set
            {
                SetBaseSession("Delete_ItemOrd_Pur_PO", value);
            }
        }
        public List<Return_GetPOGoodsReceived> Purchase_GoodsReceivedData
        {
            get
            {
                return (List<Return_GetPOGoodsReceived>)GetBaseSession("Purchase_GoodsReceivedData_PO");
            }
            set
            {
                SetBaseSession("Purchase_GoodsReceivedData_PO", value);
            }
        }
        public ArrayList PurchaseEdit_GoodsRec_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseEdit_GoodsRec_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseEdit_GoodsRec_ID_PO", value);
            }
        }
        public ArrayList Delete_GoodsRec_Pur
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_GoodsRec_Pur_PO");
            }
            set
            {
                SetBaseSession("Delete_GoodsRec_Pur_PO", value);
            }
        }

        public ArrayList DeletedItemRec_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedItemRec_ID_PO");
            }
            set
            {
                SetBaseSession("DeletedItemRec_ID_PO", value);
            }
        }

        public ArrayList DeletedReceivedEntry_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedReceivedEntry_ID_PO");
            }
            set
            {
                SetBaseSession("DeletedReceivedEntry_ID_PO", value);
            }
        }
        public List<Return_GetPOGoodsReturned> Purchase_GoodsReturnedData
        {
            get
            {
                return (List<Return_GetPOGoodsReturned>)GetBaseSession("Purchase_GoodsReturnedData_PO");
            }
            set
            {
                SetBaseSession("Purchase_GoodsReturnedData_PO", value);
            }
        }
        public ArrayList PurchaseEdit_GoodsRet_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("PurchaseEdit_GoodsRet_ID_PO");
            }
            set
            {
                SetBaseSession("PurchaseEdit_GoodsRet_ID_PO", value);
            }
        }
        public ArrayList Delete_GoodsRet_Pur
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_GoodsRet_Pur_PO");
            }
            set
            {
                SetBaseSession("Delete_GoodsRet_Pur_PO", value);
            }
        }
        public List<Return_GetPOPayments> Purchase_PaymentData
        {
            get
            {
                return (List<Return_GetPOPayments>)GetBaseSession("Purchase_PaymentData_PO");
            }
            set
            {
                SetBaseSession("Purchase_PaymentData_PO", value);
            }
        }
        public ArrayList Delete_Payment_pur
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Payment_pur_PO");
            }
            set
            {
                SetBaseSession("Delete_Payment_pur_PO", value);
            }
        }
        public List<Return_GetPORemarks> PurchaseExisting_Remarks_Grid_Data
        {
            get
            {
                return (List<Return_GetPORemarks>)GetBaseSession("PurchaseExisting_Remarks_Grid_Data_PO");
            }
            set
            {
                SetBaseSession("PurchaseExisting_Remarks_Grid_Data_PO", value);
            }
        }
        public List<Return_GetPOPaymentsForMapping> PurchasePaymentMappingData
        {
            get
            {
                return (List<Return_GetPOPaymentsForMapping>)GetBaseSession("PurchasePaymentMappingData_PO");
            }
            set
            {
                SetBaseSession("PurchasePaymentMappingData_PO", value);
            }
        }
        public List<Return_GetPOLinkedUserOrders_MainGrid> Purchase_LinkUserOrderMainData
        {
            get
            {
                return (List<Return_GetPOLinkedUserOrders_MainGrid>)GetBaseSession("Purchase_LinkUserOrderMainData_POInfo");
            }
            set
            {
                SetBaseSession("Purchase_LinkUserOrderMainData_POInfo", value);
            }
        }
        public List<Return_GetPOLinkedUserOrders_NestedGrid> Purchase_LinkUserOrderNestedData
        {
            get
            {
                return (List<Return_GetPOLinkedUserOrders_NestedGrid>)GetBaseSession("Purchase_LinkUserOrderNestedData_POInfo");
            }
            set
            {
                SetBaseSession("Purchase_LinkUserOrderNestedData_POInfo", value);
            }
        }
        public List<Return_GetPOLinkedRequisitions_MainGrid> Purchase_LinkRRMainData
        {
            get
            {
                return (List<Return_GetPOLinkedRequisitions_MainGrid>)GetBaseSession("Purchase_LinkRRMainData_POInfo");
            }
            set
            {
                SetBaseSession("Purchase_LinkRRMainData_POInfo", value);
            }
        }
        public List<Return_GetPOLinkedRequisitions_NestedGrid> Purchase_LinkRRNestedData
        {
            get
            {
                return (List<Return_GetPOLinkedRequisitions_NestedGrid>)GetBaseSession("Purchase_LinkRRNestedData_POInfo");
            }
            set
            {
                SetBaseSession("Purchase_LinkRRNestedData_POInfo", value);
            }
        }
        public List<DbOperations.StockPurchaseOrder.Return_Param_Get_PriceHistory> PriceHistory_Grid_Data
        {
            get
            {
                return (List<DbOperations.StockPurchaseOrder.Return_Param_Get_PriceHistory>)GetBaseSession("PriceHistory_Grid_Data_POPopup");
            }
            set
            {
                SetBaseSession("PriceHistory_Grid_Data_POPopup", value);
            }
        }
        public List<Return_Get_PO_Tax_Detail> PO_ItemRequestedTaxDetailData
        {
            get
            {
                return (List<Return_Get_PO_Tax_Detail>)GetBaseSession("PO_ItemRequestedTaxDetailData_PO_Tax");
            }
            set
            {
                SetBaseSession("PO_ItemRequestedTaxDetailData_PO_Tax", value);
            }
        }

        //PurchasePaymentMappingData as List<Return_GetPOPaymentsForMapping>;
        #endregion
        #region "Grid/Nested Grid"
        public ActionResult Frm_PurchaseRegister_Info(string PopupID = "",string RequisitionID = "")
        {
            PO_user_rights();
            ViewBag.PO_PopupID = PopupID;
            PO_RequisitionID = RequisitionID;
            ViewData["PO_RequisitionID"] = PO_RequisitionID;
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_PO, "List"))
            {
                String PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;
                ViewData["PurchaseExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["PurchaseExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["PurchaseExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_PO').hide();</script>");             
            }
        }

        public ActionResult Frm_PurchaseRegister_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            PO_user_rights();            
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["PurchaseExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["PurchaseExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["PurchaseExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            if (PurchaseRegister_Data_Glob == null || command == "REFRESH")
            {
                var PurchaseRegister_Data = BASE._PO_DBOps.GetRegister(Convert.ToDateTime(PurchaseFromDate), Convert.ToDateTime(PurchaseToDate));
                if (PurchaseRegister_Data != null)
                {
                    var MasterGrid = PurchaseRegister_Data.main_Register;
                    var Nestedgrid = PurchaseRegister_Data.nested_Register;                    
                    PurchaseRegister_Data_Glob = MasterGrid;
                    ItemPurchase_ExportData = Nestedgrid;
                    Session["ItemPurchase_ExportData"] = ItemPurchase_ExportData;
                }                
            }
            if(PO_RequisitionID == null || PO_RequisitionID == "")
            {
                ViewData["PO_RequisitionID"] = "";
            }
            else
            { 
                ViewData["PO_RequisitionID"] = PO_RequisitionID;
            }

            List<StockPurchaseOrder.Return_GetRegister_MainGrid> Mastergrid_data = PurchaseRegister_Data_Glob as List<StockPurchaseOrder.Return_GetRegister_MainGrid>;

            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }
        public PartialViewResult Frm_PurchaseRegister_ItemPurchase_Grid(int PO_ID, string Command)
        {
            //ViewData["FromDate"] = FromDate;
            //ViewData["ToDate"] = ToDate;

            if (ItemPurchase_ExportData == null || Command == "REFRESH")
            {
                var PurchaseRegister_Data = BASE._PO_DBOps.GetRegister(Convert.ToDateTime(PurchaseFromDate), Convert.ToDateTime(PurchaseToDate));
                var ItemPurchase_Data = PurchaseRegister_Data.nested_Register;
                ItemPurchase_ExportData = ItemPurchase_Data;
            }
            var data = ItemPurchase_ExportData as List<StockPurchaseOrder.Return_GetRegister_NestedGrid>;
            List<StockPurchaseOrder.Return_GetRegister_NestedGrid> ItemPurchase = data.FindAll(x => x.PO_ID == PO_ID);
            return PartialView(ItemPurchase);
        }
        public ActionResult PurchaseRegisterCustomDataAction(int key = 0)
        {
            var Data = PurchaseRegister_Data_Glob as List<StockPurchaseOrder.Return_GetRegister_MainGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.PO_No + "![" + it.ID + "![" + it.EditedBy + "![" + it.EditedOn + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.Requestor + "![" + it.CurrUserRole + "![" + it.Supplier + "![" + it.PO_Date + "![" + it.RequisitionID + "![" + it.SupplierID;
                }
                Currentuserrole = it.CurrUserRole;
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        public ActionResult ItemPurchaseCustomDataAction(int key = 0)
        {
            var Data = ItemPurchase_ExportData as List<StockPurchaseOrder.Return_GetRegister_NestedGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.PO_ID + "![" + it.ItemName + "![" + it.Ordered_Qty + "![" + it.Received_Qty + "![" + it.Unit + "![" + it.Rate + "![" + it.Amount + "![" + it.Item_Del_Status + "![" + it.Req_Del_Date + "![" + it.AddedOn + "![" + it.AddedBy + "![" + it.EditedOn + "![" + it.EditedBy + "![" + it.ID + "![" + it.SubitemID;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public static GridViewSettings PurchaseRegisterNestedGridSettings(int PO_ID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PurchaseRegister" + PO_ID;
            settings.SettingsDetail.MasterGridName = "PurchaseRegisterGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("PO_ID").Visible = false;
            settings.Columns.Add("ItemName");
            settings.Columns.Add("Ordered_Qty").Visible = true;
            settings.Columns.Add("Received_Qty").Visible = true;
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Rate").Visible = true;
            settings.Columns.Add("Amount").Visible = true;
            settings.Columns.Add("Item_Del_Status").Visible = true;
            settings.Columns.Add("Req_Del_Date").Visible = true;
            settings.Columns.Add("AddedOn").Visible = false;
            settings.Columns.Add("AddedBy").Visible = false;
            settings.Columns.Add("EditedOn").Visible = false;
            settings.Columns.Add("EditedBy").Visible = false;
            settings.Columns.Add("ID").Visible = false;

            settings.ClientSideEvents.FocusedRowChanged = "OnItemPurchaseFocusedRowChange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;

        }//settings for nested grid

        public static IEnumerable GetItemPurchase(int PO_ID)
        {
            List<StockPurchaseOrder.Return_GetRegister_NestedGrid> data = (List<StockPurchaseOrder.Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["ItemPurchase_ExportData"];
            List<StockPurchaseOrder.Return_GetRegister_NestedGrid> itempurchaselist = data.FindAll(x => x.PO_ID == PO_ID);
            return itempurchaselist;
        }//binding data to nested grid
        
        #endregion
        #region "Period Selection"
        public ActionResult LookUp_Get_ViewType_List_Purchase(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var bankdata = new List<SelectListItem>();
            /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/
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
        public ActionResult LookUp_ViewType_ChangeEvent_Purchase(string Chaval)
        {
            PurchaseRegister_Period model = GetPeriod(Chaval);
            PurchaseFromDate = model.PO_Fromdate;
            PurchaseToDate = model.PO_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public PurchaseRegister_Period GetPeriod(string Chaval)
        {
            PurchaseRegister_Period model = new PurchaseRegister_Period();
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
            model.PO_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.PO_Fromdate = xFr_Date;
            model.PO_Todate = xTo_Date;
            return model;            
        }
        [HttpGet]
        public ActionResult Frm_Change_Period_Screen_Purchase()
        {
            PurchaseRegister_Period model = new PurchaseRegister_Period();
            model.PO_PeriodSelection = "Specific Period";
            model.PO_Todate = (DateTime)PurchaseToDate;
            model.PO_Fromdate = (DateTime)PurchaseFromDate;
            model.PO_BE_View_Period = "";
            model.PO_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.PO_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_Purchase(PurchaseRegister_Period model)
        {
            if (model.PO_Todate < model.PO_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                PurchaseToDate = model.PO_Todate;
                PurchaseFromDate = model.PO_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public String SetDate()
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
        public ActionResult DataNavigation(string ActionMethod, int ID, DateTime PODate)
        {
            
                string msg = "";
                var PO_Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
                var PO_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (PO_Audited_Date != null)
                    {
                        if (PO_Audited_Date.Rows.Count > 0)//Mantis bug 0000105 fixed
                    {
                            if (PODate >= Convert.ToDateTime(PO_Audited_Date.Rows[0]["FROMDATE"]) && PODate <= Convert.ToDateTime(PO_Audited_Date.Rows[0]["TODATE"]))
                            {
                                msg = "false";
                                return Json(new
                                {
                                    Message = "No Changes Are Allowed In Audited Period.Purchase Date should not be in Audited period...!",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                }//Mantis bug 0000105 fixed
                if (PO_Submitted_Date != null)
                    {
                        if(PO_Submitted_Date.Rows.Count > 0)
                        { 
                            if (PODate >= Convert.ToDateTime(PO_Submitted_Date.Rows[0]["FROMDATE"]) && PODate <= Convert.ToDateTime(PO_Submitted_Date.Rows[0]["TODATE"]))
                            {
                                msg = "false";
                                return Json(new
                                {
                                    Message = "No Changes Are Allowed In Accounts Submitted Period.Purchase Date Should Not Be In Account Submission Period...!",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                    }//Mantis bug 0000105 fixed
                }
                }

                if (ActionMethod == "NewPay")
                {
                    var amount = BASE._PO_DBOps.Get_PO_Pending_Due(ID);
                    if (amount== 0)//Mantis bug 0000800 fixed
                {
                        msg = "false";
                        return Json(new
                        {
                            Message = "Payment can be made if there is some amount pending..!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            var uoid = 0;
            int uosr = 0;//Mantis bug 0000114 fixed
            var defaultvalue = 0;
            if (ActionMethod == "DeliveryLinkUO")
                {
                var x = BASE._PO_DBOps.Get_PO_Latest_UO_ID(ID);
                uoid = x.HasValue ? (int)x : defaultvalue;
                if (uoid == 0)
                {
                    msg = "false";
                    return Json(new
                    {
                        Message = "Linked RR and UO should be present to post Delivery to UO / View UO..!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                else { uosr = uoid; }
            }//Mantis bug 0000114 fixed

            if (ActionMethod == "NewItemRec")
                {
                    if (BASE._PO_DBOps.Get_PO_Non_Rate_Items(ID))
                    {
                        msg = "false";
                        return Json(new
                        {
                            Message = "Delivery can not be posted against PO until Rates have been mentioned for all Items Ordered...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
     

            if (ActionMethod == "Edit")
            {
                var comp = BASE._PO_DBOps.Get_PO_Job_Project_Completed(ID);
                var store = BASE._PO_DBOps.Get_PO_Related_ClosedDept_Count(ID);
                var datacomp = BASE._PO_DBOps.Get_PO_Detail(ID);
                var POStatus = datacomp.PO_Status;
                if (POStatus == "Completed")
                {
                    msg = "false";
                    return Json(new
                    {
                        Message = "Completed Purchase Orders cannot be edited",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);

                }
                if (comp)
                {
                    msg = "false";
                    return Json(new
                    {
                        Message = "PO against Completed Job/Project can not be  Edited",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }

                if (store != 0)
                {
                    msg = "false";
                    return Json(new
                    {
                        Message = "PO involving closed Store/Dept  can not be  Edited",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (msg == "")
            {
                return Json(new
                {
                    Message = "",
                    result = true,
                    UOSR = uosr,
                }, JsonRequestBehavior.AllowGet);
            }//Mantis bug 0000114 fixed
            else
            {
                return Json(new
                {
                    Message = "Error",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_NEVD_Purchase(string ActionMethod, int ID = 0)
        {
            PO_user_rights();
            if (!CheckRights(BASE, ClientScreen.Stock_PO, "Update") && ActionMethod == "Edit")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            if (!CheckRights(BASE, ClientScreen.Stock_PO, "View") && ActionMethod == "View")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            Sessionclear(); //bug 769 resolved
            Model_NEVD_Purchase model = new Model_NEVD_Purchase();
            model.ActionMethod = ActionMethod;
            
            
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var selectedrowdata = BASE._PO_DBOps.Get_PO_Detail(ID);

                if (selectedrowdata != null)
                {
                    PO_ID_Glob = ID;
                    
                    model.PO_ID = ID;
                    model.PO_Number = selectedrowdata.PO_Number;
                    model.PO_Status = selectedrowdata.PO_Status;
                    model.PO_Date = selectedrowdata.PO_Date;
                    model.PO_SupplierID = selectedrowdata.SupplierID;   
                    ViewData["EDIT_SupplierID"] = selectedrowdata.SupplierID;
                    model.PO_PurchasedBy = selectedrowdata.PurchasedBy;
                    model.PO_DeliveryStatus = selectedrowdata.DeliveryStatus;
                    model.PO_TotalAmount = selectedrowdata.TotalAmount;
                    model.PO_PaidAmount = selectedrowdata.PaidAmount;
                    model.PO_PendingAmount = selectedrowdata.PendingAmount;
                    model.PO_SpecialDiscount = selectedrowdata.Special_Discount;

                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_NEVD_Purchase(Model_NEVD_Purchase model)
        {
            
            try
            {
                string actionmethod = model.ActionMethod;
                if (actionmethod == "Edit")
                {
                    if (model.PO_Date < BASE._open_Year_Sdt || model.PO_Date > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            message = "PO Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();

                    if (BASE._open_User_Type == "CLIENT ROLE")
                    {
                        if (AuditedPeriod != null)
                        {
                            if(AuditedPeriod.Rows.Count > 0)//Mantis bug 0000105 fixed
                            { 
                                if (model.PO_Date >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.PO_Date <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Purchase Order Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }//Mantis bug 0000105 fixed
                        }
                        if (SubmittedPeriod != null)
                        {
                            if(SubmittedPeriod.Rows.Count > 0)//Mantis bug 0000105 fixed
                            { 
                                if (model.PO_Date >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.PO_Date <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Request Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }//Mantis bug 0000105 fixed
                        }
                    }

                  




                }
                

                if (actionmethod == "Edit")
                {


                    Param_Update_PurchaseOrder_Txn UpParam = new Param_Update_PurchaseOrder_Txn();

                    IUDDocuments(ref UpParam, actionmethod,model);
                    IUDGoodsReceived(ref UpParam, actionmethod, model);
                    IUDGoodsReturned(ref UpParam, actionmethod, model);
                    IUDPayments(ref UpParam, actionmethod, model);

                    IUDItemsOrdered(ref UpParam, actionmethod, model);

                    IUDRemainingData(ref UpParam, model);


                    if (BASE._PO_DBOps.UpdatePurchaseOrder_Txn(UpParam))
                    {

                        if (!string.IsNullOrEmpty(model.PO_StatusButtonClick))
                        {
                            string msg = "";
                            Param_UpdatePurchaseOrderStatus param = new Param_UpdatePurchaseOrderStatus();
                            param.PO_ID = model.PO_ID;
                            param.Logged_In_User = BASE._open_User_ID;//Mantis bug 0001239 fixed
                            switch (model.PO_StatusButtonClick)
                            {


                                case "Reject":
                                    param.UpdatedStatus = PO_Status.Rejected;
                                    msg = "Updated Successfully..!!</br>Status Changed To Rejected..!!";
                                    break;

                                case "Re_Requisition":
                                    param.UpdatedStatus = PO_Status.Re_Requisition_Requested;
                                    msg = "Updated Successfully..!!</br>Status Changed To 'Assigned for Re Requisition'..!!";
                                    break;

                                case "Complete":
                                    param.UpdatedStatus = PO_Status.Completed;

                                    msg = "Updated Successfully..!!</br>Status Changed To Completed..!!";
                                    break;

                                case "Approve":
                                    param.UpdatedStatus = PO_Status.Approved;
                                    msg = "Updated Successfully..!!</br>Status Changed To 'Approved'..!!";
                                    break;
                            }

                                    if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
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
        #region "IUD Functions"

        public void IUDRemainingData(ref Param_Update_PurchaseOrder_Txn UpParam, Model_NEVD_Purchase model)
        {

            UpParam.ID = model.PO_ID;
            UpParam.PO_Date = model.PO_Date;
            UpParam.Supplier_ID = model.PO_SupplierID;
            UpParam.Remarks = model.PO_Remarks;
            UpParam.Special_Discount = model.PO_SpecialDiscount;

            //var allData = Delete_PurchaseExisting_Remarks_ID as ArrayList;
            if (Delete_PurchaseExisting_Remarks_ID != null)
            {

                int[] res = ((List<int>)Delete_PurchaseExisting_Remarks_ID).ToArray();//mantis #1420 resolved 
                UpParam.Deleted_Remarks_IDs = res;
            }

        }

        public void IUDDocuments(ref Param_Update_PurchaseOrder_Txn UpParam, string actionmethod, Model_NEVD_Purchase model)
        {
            var insertindex = 0;
            var updateindex = 0;
            var DocumentsData = (List<Return_GetDocumentsGridData>)Purchase_Documents_Window_Grid_Data_PO;
           
            if (DocumentsData != null)
            {
                var insertAttachments = new Parameter_Insert_Attachment[DocumentsData.Count()];
                var updateattachment = new Parameter_Update_Attachment[DocumentsData.Count()];
                string[] doceditid = PurchaseEdit_Document_ID != null ? (string[])(PurchaseEdit_Document_ID as ArrayList).ToArray(typeof(string)) : null;

                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "PO";
                        InEInfo.Ref_Rec_ID = model.PO_ID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments[insertindex] = InEInfo;
                        insertindex++;

                    }
                    else if (doceditid != null)
                    {
                        if (doceditid.Contains(DocumentsData[i].ID))
                        {
                            var InEInfo = new Parameter_Update_Attachment();
                            InEInfo.FileName = DocumentsData[i].File_Name;
                            InEInfo.Description = DocumentsData[i].Remarks;
                            InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                            InEInfo.Ref_Screen = "PO";
                            InEInfo.Ref_Rec_ID = model.PO_ID.ToString();
                            InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                            InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                            InEInfo.File = DocumentsData[i].File_Array;
                            InEInfo.RecID = DocumentsData[i].ID;
                            updateattachment[updateindex] = InEInfo;
                            updateindex++;
                        }
                    }
                }
                UpParam.Added_Attachments = insertAttachments;
                UpParam.Updated_Attachments = updateattachment;
            }

            if (PurchaseDelete_Document_ID != null)
            {
                string[] res = (string[])(PurchaseDelete_Document_ID as ArrayList).ToArray(typeof(string));
                UpParam.Deleted_Attachment_IDs = res;
            }
            if (PurchaseUnlink_Document_ID != null)
            {
                string[] res = (string[])(PurchaseUnlink_Document_ID as ArrayList).ToArray(typeof(string));
                UpParam.Unlinked_Attachment_IDs = res;
            }
        }
                                       

        public void IUDItemsOrdered(ref Param_Update_PurchaseOrder_Txn UpParam, string actionmethod, Model_NEVD_Purchase model)
        {
            var ItemsordData = (List<Return_Get_PO_Items_MainGrid>)Purchase_ItemOrderedData;
            var all_data_Of_NestedItemGrid = (List<Return_Get_PO_Items_NestedGrid>)PO_IO_NestedGridTaxData;
            Session["UO_GoodsDeliveredNestedData"] = PO_IO_NestedGridTaxData;

            var insertindex = 0;
            var updateindex = 0;
            if (ItemsordData != null)
            {
                var insertItemsOrd = new Param_InsertPurchaseOrderItem[ItemsordData.Count()];
                var updateItemsOrd = new Param_UpdatePurchaseOrderItem[ItemsordData.Count()];
                int[] ItemsOrdEditID = PurchaseEdit_ItemOrd_ID != null ? (int[])(PurchaseEdit_ItemOrd_ID as ArrayList).ToArray(typeof(int)): null;

                for (int i = 0; i < ItemsordData.Count(); i++)
                {
                    if (ItemsordData[i].ID == 0)
                    {
                        var InEInfo = new Param_InsertPurchaseOrderItem();

                        InEInfo.PO_ID = model.PO_ID;
                        InEInfo.RR_Item_Sr_No = ItemsordData[i].RR_Item_Sr_No;
                        InEInfo.Make = ItemsordData[i].Make;
                        InEInfo.Model = ItemsordData[i].Model;
                        InEInfo.Purchase_Qty = ItemsordData[i].OrderedQty;
                        InEInfo.Unit_ID = ItemsordData[i].UnitID;
                        InEInfo.Rate = ItemsordData[i].Rate;
                        InEInfo.Discount_Promised = ItemsordData[i].Discount;
                        InEInfo.Taxes = ItemsordData[i].Taxes;
                        InEInfo.Amount = ItemsordData[i].Amount;
                        InEInfo.Priority = ItemsordData[i].POI_Priority;
                        InEInfo.Reqd_Delivery_Date = ItemsordData[i].Reqd_Del_Date;
                        InEInfo.Remarks = ItemsordData[i].Remarks;
                        InEInfo.Sub_Item_ID = ItemsordData[i].ItemID;
                        InEInfo.Add_Update_Reason = ItemsordData[i].AddUpdateReason;
                        InEInfo.Dest_Location_ID = ItemsordData[i].LocationID;
                        InEInfo.Requested_Qty = ItemsordData[i].Requested_Qty;

                        //nested
                        if (all_data_Of_NestedItemGrid != null)
                        {

                            var insertNestedItem = new PO_Param_Insert_Tax_Details[all_data_Of_NestedItemGrid.Count()];
                            for (int J = 0; J <= all_data_Of_NestedItemGrid.Count - 1; J++)
                            {
                                if (all_data_Of_NestedItemGrid[J].PO_Item_ID == 0)
                                {

                                    if (ItemsordData[i].Sr == all_data_Of_NestedItemGrid[J].MainSr)
                                    {

                                        var IteminsertNestedData = new PO_Param_Insert_Tax_Details();

                                        IteminsertNestedData.RequestedItem_ID = all_data_Of_NestedItemGrid[J].PO_Item_ID;
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

                        insertItemsOrd[insertindex] = InEInfo;
                        insertindex++;
                    }




                    else if (ItemsOrdEditID != null)
                    {
                        if (ItemsOrdEditID.Contains(ItemsordData[i].ID))
                        {

                            var InEInfo = new Param_UpdatePurchaseOrderItem();
                            InEInfo.ID = ItemsordData[i].ID;
                            InEInfo.Make = ItemsordData[i].Make;
                            InEInfo.Model = ItemsordData[i].Model;
                            InEInfo.Purchase_Qty = ItemsordData[i].OrderedQty;
                            InEInfo.Unit_ID = ItemsordData[i].UnitID;
                            InEInfo.Rate = ItemsordData[i].Rate;
                            InEInfo.Discount_Promised = ItemsordData[i].Discount;
                            InEInfo.Taxes = ItemsordData[i].Taxes;
                            InEInfo.Amount = ItemsordData[i].Amount;
                            InEInfo.Priority = ItemsordData[i].POI_Priority;
                            InEInfo.Reqd_Delivery_Date = ItemsordData[i].Reqd_Del_Date;
                            InEInfo.Remarks = ItemsordData[i].Remarks;
                            InEInfo.Sub_Item_ID = ItemsordData[i].ItemID;
                            InEInfo.Add_Update_Reason = ItemsordData[i].AddUpdateReason;
                            InEInfo.Dest_Location_ID = ItemsordData[i].LocationID;



                            //nested
                            if (all_data_Of_NestedItemGrid != null)
                            {

                                var insertNested = new PO_Param_Insert_Tax_Details[all_data_Of_NestedItemGrid.Count()];
                                for (int J = 0; J <= all_data_Of_NestedItemGrid.Count - 1; J++)
                                {
                                    if (all_data_Of_NestedItemGrid[J].PO_Item_ID == 0 || all_data_Of_NestedItemGrid[J].PO_Item_ID == ItemsordData[i].ID)
                                    {

                                        if (ItemsordData[i].Sr == all_data_Of_NestedItemGrid[J].MainSr)
                                        {

                                            var IteminsertNestedData = new PO_Param_Insert_Tax_Details();

                                            IteminsertNestedData.RequestedItem_ID = all_data_Of_NestedItemGrid[J].PO_Item_ID;
                                            IteminsertNestedData.TaxPercent = all_data_Of_NestedItemGrid[J].TaxPercent;
                                            IteminsertNestedData.TaxTypeID = all_data_Of_NestedItemGrid[J].Tax_TypeID;
                                            IteminsertNestedData.TaxRemarks = all_data_Of_NestedItemGrid[J].TaxRemarks;
                                            insertNested[J] = IteminsertNestedData;
                                        }
                                    }
                                }

                                InEInfo._Added_Item_Taxes = insertNested;
                            }







                            updateItemsOrd[updateindex] = InEInfo;



                            updateindex++;

                        }
                    }
                }

                if (insertItemsOrd.Length > 0)
                // { param_Update_UO_Txn.InUOItems = InsertOrderItems[0] == null ? null : InsertOrderItems; }
                //else
                {
                    UpParam.Added_Items_Ordered = insertItemsOrd;
                }
                if (updateItemsOrd.Length > 0)
                //{ param_Update_UO_Txn.UpdateUOItems = UpdateOrderItems[0] == null ? null : UpdateOrderItems; }
                //else
                {

                    UpParam.Updated_Items_Ordered = updateItemsOrd;
                }
            }
            
                if (Delete_ItemOrd_Pur != null)
                {
                    int[] res = (int[])(Delete_ItemOrd_Pur as ArrayList).ToArray(typeof(int));
                    UpParam.Deleted_Items_Ordered_IDs = res;
                }
            
        }

        public void IUDGoodsReceived(ref Param_Update_PurchaseOrder_Txn UpParam, string actionmethod, Model_NEVD_Purchase model)
        {

            var GoodsRecData = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
            var insertindex = 0;
            var updateindex = 0;
            if (GoodsRecData != null)
            {
                var insertGoodsRec = new Param_InsertPurchaseOrderGoodsReceived[GoodsRecData.Count()];
                var updateGoodsRec = new Param_UpdatePurchaseOrderGoodsReceived[GoodsRecData.Count()];
                int[] GoodsRecEditID = PurchaseEdit_GoodsRec_ID != null ? (int[])(PurchaseEdit_GoodsRec_ID as ArrayList).ToArray(typeof(int)) : null;
                for (int i = 0; i < GoodsRecData.Count(); i++)
                {
                    if (GoodsRecData[i].ID == 0)
                    {
                        var InEInfo = new Param_InsertPurchaseOrderGoodsReceived();
                        InEInfo.Stock_Value = GoodsRecData[i].Stock_Value;
                        InEInfo.Store_Dept_ID = GoodsRecData[i].Stock_Store_Dept_ID;
                        InEInfo.Lot_Serial_no = GoodsRecData[i].LotNo;
                        InEInfo.Project_ID = GoodsRecData[i].Stock_Proj_ID;
                        InEInfo.Model = GoodsRecData[i].Model;
                        InEInfo.Make = GoodsRecData[i].Make;
                        InEInfo.sub_Item_ID = GoodsRecData[i].ItemID;
                        InEInfo.Receiver_Remarks = GoodsRecData[i].Remarks;
                        InEInfo.Received_By_ID = GoodsRecData[i].ReceivedByID;
                        InEInfo.Challan_No = GoodsRecData[i].ChallanNo;
                        InEInfo.Bill_No = GoodsRecData[i].BillNo;
                        InEInfo.Delivery_Location_ID = GoodsRecData[i].LocationID;
                        InEInfo.Delivery_Carrier = GoodsRecData[i].Carrier;
                        InEInfo.FOB = GoodsRecData[i].FOB;
                        InEInfo.Delivery_Mode = GoodsRecData[i].ShipmentModeID;
                        InEInfo.Recd_Date = GoodsRecData[i].ReceivedDate;
                        InEInfo.Recd_Qty = GoodsRecData[i].ReceivedQty;
                        InEInfo.PO_Item_ID = GoodsRecData[i].PO_Item_Rec_ID;
                        InEInfo.PO_ID = model.PO_ID;
                        InEInfo.Unit_ID = GoodsRecData[i].UnitID;
                        InEInfo.Warranty = GoodsRecData[i].Warranty;
                 

                        insertGoodsRec[insertindex] = InEInfo;
                        insertindex++;
                    }


                    else if (GoodsRecEditID != null)
                    {

                        if (GoodsRecEditID.Contains(GoodsRecData[i].ID))
                        {
                            var InEInfo = new Param_UpdatePurchaseOrderGoodsReceived();
                            InEInfo.Stock_Value = GoodsRecData[i].Stock_Value;
                            InEInfo.Store_Dept_ID = GoodsRecData[i].Stock_Store_Dept_ID;
                            InEInfo.Lot_Serial_no = GoodsRecData[i].LotNo;
                            InEInfo.Project_ID = GoodsRecData[i].Stock_Proj_ID;
                            InEInfo.Model = GoodsRecData[i].Model;
                            InEInfo.Make = GoodsRecData[i].Make;
                            InEInfo.sub_Item_ID = GoodsRecData[i].ItemID;
                            InEInfo.Receiver_Remarks = GoodsRecData[i].Remarks;
                            InEInfo.Received_By_ID = GoodsRecData[i].ReceivedByID;
                            InEInfo.Challan_No = GoodsRecData[i].ChallanNo;
                            InEInfo.Bill_No = GoodsRecData[i].BillNo;
                            InEInfo.Delivery_Location_ID = GoodsRecData[i].LocationID;
                            InEInfo.Delivery_Carrier = GoodsRecData[i].Carrier;
                            InEInfo.FOB = GoodsRecData[i].FOB;
                            InEInfo.Delivery_Mode = GoodsRecData[i].ShipmentModeID;
                            InEInfo.Recd_Date = GoodsRecData[i].ReceivedDate;
                            InEInfo.Recd_Qty = GoodsRecData[i].ReceivedQty;
                            InEInfo.PO_Item_ID = GoodsRecData[i].PO_Item_Rec_ID;
                            InEInfo.ID = GoodsRecData[i].ID;
                            InEInfo.Unit_ID = GoodsRecData[i].UnitID;
                            InEInfo.Warranty = GoodsRecData[i].Warranty;
                            InEInfo.StockID = GoodsRecData[i].CreatedStockID;
                            updateGoodsRec[updateindex] = InEInfo;
                            updateindex++;


                        }
                    }
                }
                if (insertGoodsRec.Length > 0)
                {
                    //    param_Update_UO_Txn.InUOReturns = insertReturned[0] == null ? null : insertReturned; }
                    //else
                    //{
                    UpParam.Added_Items_Received = insertGoodsRec;

                }
                if (updateGoodsRec.Length > 0)
                {
                    //    param_Update_UO_Txn.UpdateUOReturns = updateReturned[0] == null ? null : updateReturned; }
                    //else
                    //{

                    UpParam.Updated_Items_Received = updateGoodsRec;
                }
            }




            if (Delete_GoodsRec_Pur != null)
            {
                int[] res = (int[])(Delete_GoodsRec_Pur as ArrayList).ToArray(typeof(int));
                UpParam.Deleted_Items_Received_IDs = res;
            }
        }
        

        public void IUDGoodsReturned(ref Param_Update_PurchaseOrder_Txn UpParam, string actionmethod, Model_NEVD_Purchase model)
        {
            var GoodsRetData = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
            var insertindex = 0;
            var updateindex = 0;
            if (GoodsRetData != null)
            {
                var insertGoodsRet = new List<Param_InsertPurchaseOrderGoodsReturned>();//[GoodsRetData.Count()];
                var updateGoodsRet = new List<Param_UpdatePurchaseOrderGoodsReturned>();//[GoodsRetData.Count()];
                int[] GoodsRetEditID = PurchaseEdit_GoodsRet_ID != null ? (int[])(PurchaseEdit_GoodsRet_ID as ArrayList).ToArray(typeof(int)) : null;


                for (int i = 0; i < GoodsRetData.Count(); i++)
                {
                    if (GoodsRetData[i].ID == 0)
                    {
                        var InEInfo = new Param_InsertPurchaseOrderGoodsReturned();
                        InEInfo.PO_ID = model.PO_ID;
                        InEInfo.Recd_Item_ID = GoodsRetData[i].RecdEntryID;
                        InEInfo.Returned_Qty = GoodsRetData[i].ReturnedQty;
                        InEInfo.Returned_Date = GoodsRetData[i].ReturnedDate;
                        InEInfo.Delivery_Mode = GoodsRetData[i].ShipmentModeID;
                        InEInfo.Delivery_Carrier = GoodsRetData[i].Carrier;
                        InEInfo.Returned_By_ID = GoodsRetData[i].ReturnedByID;
                        InEInfo.Returned_by_Remarks = GoodsRetData[i].Remarks;
                        InEInfo.Challan_No = GoodsRetData[i].ChallanNo;//Mantis bug 936 fixed


                        insertGoodsRet.Add(InEInfo);
                        insertindex++;
                    }



                    else if (GoodsRetEditID != null)
                    {
                        if (GoodsRetEditID.Contains(GoodsRetData[i].ID))
                        {


                            var InEInfo = new Param_UpdatePurchaseOrderGoodsReturned();
                            InEInfo.ID = GoodsRetData[i].ID;
                            InEInfo.Recd_Item_ID = GoodsRetData[i].RecdEntryID;
                            InEInfo.Returned_Qty = GoodsRetData[i].ReturnedQty;
                            InEInfo.Returned_Date = GoodsRetData[i].ReturnedDate;
                            InEInfo.Delivery_Mode = GoodsRetData[i].ShipmentModeID;
                            InEInfo.Delivery_Carrier = GoodsRetData[i].Carrier;
                           InEInfo.Returned_By_ID = GoodsRetData[i].ReturnedByID;
                            InEInfo.Returned_by_Remarks = GoodsRetData[i].Remarks;
                            InEInfo.Challan_No = GoodsRetData[i].ChallanNo;//Mantis bug 936 fixed
                            updateGoodsRet.Add(InEInfo);
                            updateindex++;


                        }
                    }
                }

                if (insertGoodsRet.Count > 0)
                {
                    //    param_Update_UO_Txn.InUOReturns = insertReturned[0] == null ? null : insertReturned; }
                    //else
                    //{
                    UpParam.Added_Items_Returned = insertGoodsRet.ToArray();
                }
                if (updateGoodsRet.Count > 0)
                {
                    //    param_Update_UO_Txn.UpdateUOReturns = updateReturned[0] == null ? null : updateReturned; }
                    //else
                    //{

                    UpParam.Updated_Items_Returned = updateGoodsRet.ToArray();
                }

            }


                if (Delete_GoodsRet_Pur != null)
                {
                    int[] res = (int[])(Delete_GoodsRet_Pur as ArrayList).ToArray(typeof(int));
                    UpParam.Deleted_Items_Returned_IDs = res;
                }
            
        }

        public void IUDPayments(ref Param_Update_PurchaseOrder_Txn UpParam, string actionmethod, Model_NEVD_Purchase model)
        {            
            var PaymentsData = (List<Return_GetPOPayments>)Purchase_PaymentData;
            if (PaymentsData != null)
            {                
                var InsertExpenses = new List<Param_InsertPurchaseOrderPayment>();
                for (int i = 0; i < PaymentsData.Count(); i++)
                {
                    if (PaymentsData[i].ID == null || String.IsNullOrEmpty(PaymentsData[i].ID))//Mantis bug 0000994 fixed
                    {
                        var recid = PaymentsData[i].ID;

                        var InEInfo = new Param_InsertPurchaseOrderPayment();
                        InEInfo.PO_id = model.PO_ID;
                        InEInfo.Exp_Tr_Sr_No = PaymentsData[i].Txn_Sr_No;
                        InEInfo.Exp_Tr_ID = PaymentsData[i].TxnMID;
                        InsertExpenses.Add(InEInfo);
                        

                    }

                }

                if (InsertExpenses.Count > 0)
                {
                    UpParam.Added_Payments_Mapped = InsertExpenses[0] == null ? null : InsertExpenses.ToArray();
                }
                else
                {
                    UpParam.Added_Payments_Mapped = InsertExpenses.ToArray();
                }
            }


            if (Delete_Payment_pur != null)
            {
                int[] res = (int[])(Delete_Payment_pur as ArrayList).ToArray(typeof(int));
                UpParam.Deleted_Payment_Mapped_IDs = res;
            }

        }

        #endregion
        public ActionResult Frm_Export_Options()// list export
        {
            if (!(CheckRights(BASE, ClientScreen.Stock_PO, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#PurchaseRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return View();
        }
       
       // public ActionResult ContextMenuChecks(int ID)
      //  {
            //var jobdata = BASE._Jobs_Dbops.GetRecord(jobid);
            //int AssignMainDeptID = jobdata.Job_AssigneeMainDeptID;
            //int? AssignSubDeptID = jobdata.Job_AssigneeSubDeptID;
            //if (BASE._open_User_MainDeptID == AssignMainDeptID || BASE._open_User_SubDeptID == AssignSubDeptID)
            //{
            //    return Json(new
            //    {
            //        Message = "",
            //        result = true,
            //    }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new
            //    {
            //        Message = "Not Allowed!!",
            //        result = false,
            //    }, JsonRequestBehavior.AllowGet);
            //}
     //   }
        public ActionResult POStatusToNew(int ID)
        {
            Param_UpdatePurchaseOrderStatus param = new Param_UpdatePurchaseOrderStatus();
            param.PO_ID = ID;
            param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "_New", true);
            if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
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
     

        public ActionResult CheckDocumentsLinked(int PO_ID)
        {
            var docdata = Purchase_Documents_Window_Grid_Data_PO as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._PO_DBOps.GetAttachmentLinkScreen(PO_ID, docdata[i].ID);
                    if (!string.IsNullOrEmpty(screen))
                    {
                        return Json(new
                        {
                            result = true,
                            message = "There Are Documents That Cannot Be Deleted Because They Have Been Linked To Other Screens</br> Do You Want To Unlink It From Purchase Order..? "
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
        #region "ContextMenuFunctions"

        public ActionResult Get_PO_Latest_RR_ID(int PO_ID)
        {

            var RR_ID = BASE._PO_DBOps.Get_PO_Latest_RR_ID(PO_ID);

            return Json(new
            {
                result = true,
                rrid = RR_ID,
                message = "success"
            }, JsonRequestBehavior.AllowGet);
        }

       
        
        #endregion
        #region "DropDown"



        public ActionResult Lookup_PO_Supplier(DataSourceLoadOptions loadOptions)
        {

            List<Return_GetAllSuppliers> Map_Supplier = BASE._StockSupplier_dbops.GetAllSuppliers(null);
            // Item_category.Insert(0, new Return_GetAllSuppliers { Supplier = "Please Select Supplier Name...", ContactPerson = "", Contact_No = "", CompanyCode = "", PAN = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Map_Supplier, loadOptions)));
        }

        #endregion
        #region "Inside Grids"

        #region "Documents"
        public ActionResult PurchaseDocumentsGridData(string ActionMethodName, int PO_ID = 0)
        {
            var docList = new List<Return_GetDocumentsGridData>();
            if (ActionMethodName == "New")
            {
                return PartialView(docList);
            }
            if (Purchase_Documents_Window_Grid_Data_PO == null)
            {
                var docData = BASE._PO_DBOps.GetPODocuments(PO_ID);
                if (docData != null)
                {
                    docList = docData;
                }
                Purchase_Documents_Window_Grid_Data_PO = docList;
            }

            return PartialView(Purchase_Documents_Window_Grid_Data_PO);
        }

        public ActionResult Purchase_Documents_Attachment()
        {
            Model_Attachment_Window model = (Model_Attachment_Window)GetBaseSession("Purchase_Documents_Attachment_AttachmentData");

            if (model.Help_REF_REC_ID != null)
            {
                try
                {
                    Parameter_Insert_Attachment PO_In_Info = new Parameter_Insert_Attachment();

                    PO_In_Info.FileName = model.Help_Document_FileName;
                    PO_In_Info.Description = model.Help_Document_Description;
                    PO_In_Info.NameID = model.Help_Document_NameID;
                    PO_In_Info.Ref_Screen = "PO";
                    PO_In_Info.Ref_Rec_ID = model.Help_REF_REC_ID;
                    PO_In_Info.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                    PO_In_Info.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                    PO_In_Info.File = model.Help_filefield;
                    PO_In_Info.RecID = System.Guid.NewGuid().ToString();
                    if (BASE._Attachments_DBOps.Insert(PO_In_Info).Length > 0)
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
            }//Mantis bug 0000896 fixed
            else
            {
                List<Return_GetDocumentsGridData> gridRows = new List<Return_GetDocumentsGridData>();
                if (model.Help_Doc_From_Date > model.Help_Doc_To_Date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Applicable From Date should be smaller than Applicable To Date"
                    }, JsonRequestBehavior.AllowGet);
                }
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Purchase_Documents_Window_Grid_Data_PO != null)
                {
                    gridRows = (List<Return_GetDocumentsGridData>)Purchase_Documents_Window_Grid_Data_PO;
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
                        var editDocument = PurchaseEdit_Document_ID as ArrayList;
                        if (editDocument != null)
                        {
                            editDocument.Add(model.ID);
                            PurchaseEdit_Document_ID = editDocument;
                        }
                        else
                        {
                            editDocumentID.Add(model.ID);
                            PurchaseEdit_Document_ID = editDocumentID;
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
                Purchase_Documents_Window_Grid_Data_PO = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Purchase_Documents_Attachment_LinkCheck(string DocId, int POId)
        {
            var screen = BASE._PO_DBOps.GetAttachmentLinkScreen(POId, DocId);
            if (!string.IsNullOrEmpty(screen))
            {
                return Json(new
                {
                    result = false,
                    message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From Purchase..? "
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
            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)Purchase_Documents_Window_Grid_Data_PO;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (dataToDelete != null)
            {
                allDocData.Remove(dataToDelete);
            }
            Purchase_Documents_Window_Grid_Data_PO = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    var deleteDocumentID = new ArrayList();
                    var deleteDocument = PurchaseDelete_Document_ID as ArrayList;
                    if (deleteDocument != null)
                    {
                        deleteDocument.Add(Doc_ID);
                        PurchaseDelete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID.Add(Doc_ID);
                        PurchaseDelete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {

                var unlinkDocumentID = new ArrayList();
                var unlinkDocument = PurchaseUnlink_Document_ID as ArrayList;
                if (unlinkDocument != null)
                {
                    unlinkDocument.Add(Doc_ID);
                    PurchaseUnlink_Document_ID = unlinkDocument;
                }
                else
                {
                    unlinkDocumentID.Add(Doc_ID);
                    PurchaseUnlink_Document_ID = unlinkDocumentID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion Documents

        #region "Item Ordered"
        public ActionResult PO_ItemOrderedGridData(string ActionMethodName, int PO_ID = 0)
        {
            var ItemList = new List<Return_Get_PO_Items_MainGrid>();
            var ItemRequestedTaxList = new List<Return_Get_PO_Items_NestedGrid>();
            if (ActionMethodName == "New")
            {
                return PartialView(ItemList);
            }
            if (Purchase_ItemOrderedData == null)
            {
                var ItemOrdData = BASE._PO_DBOps.GetPOItemsOrdered(PO_ID);
                
                    List<Return_Get_PO_Items_MainGrid> POItemData = ItemOrdData.Main_grid_Data;
                    List<Return_Get_PO_Items_NestedGrid> POItemTaxData = ItemOrdData.Nested_grid_Data;
                

                if (POItemData != null)
                {
                    ItemList = POItemData;
                }
                if (POItemTaxData != null)
                {
                    ItemRequestedTaxList = POItemTaxData;
                }

                Purchase_ItemOrderedData = ItemList;
                PO_IO_NestedGridTaxData = ItemRequestedTaxList;
                Session["PO_IO_NestedGridTaxData"] = PO_IO_NestedGridTaxData;
            }
           
                return PartialView(Purchase_ItemOrderedData);
            

        }

        public PartialViewResult PO_ItemOrderedNestedGridData(string Command, int PO_Item_ID = 0, int MainSr = 0)
        {
            ViewData["PO_Item_ID"] = PO_Item_ID;
            ViewData["MainSr"] = MainSr;

            if (PO_IO_NestedGridTaxData == null || Command == "REFRESH")
            {
                var ItemOrdData = BASE._PO_DBOps.GetPOItemsOrdered(PO_ID_Glob);

                List<Return_Get_PO_Items_MainGrid> POItemData = ItemOrdData.Main_grid_Data;
                List<Return_Get_PO_Items_NestedGrid> POItemTaxData = ItemOrdData.Nested_grid_Data;

                Purchase_ItemOrderedData = POItemData;
                PO_IO_NestedGridTaxData = POItemTaxData;
            
            }
            var data = PO_IO_NestedGridTaxData as List<Return_Get_PO_Items_NestedGrid>;
            List<Return_Get_PO_Items_NestedGrid> itemReqTax = new List<Return_Get_PO_Items_NestedGrid>();

            if (PO_Item_ID == 0)
            {
                itemReqTax = data.FindAll(x => x.MainSr == MainSr && x.PO_Item_ID == 0);
            }
            else
            {
                itemReqTax = data.FindAll(x => x.PO_Item_ID == PO_Item_ID);
            }
            return PartialView(itemReqTax);
        }

        public static GridViewSettings PurchaseRegisterItemTaxNestedGridSettings(int PO_Item_ID = 0, int Sr = 0)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "POItemRequestedTax" + PO_Item_ID + Sr;
            settings.SettingsDetail.MasterGridName = "PO_ItemOrderedGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Sr").Visible = true;
            settings.Columns.Add("Tax_Type");
            settings.Columns.Add("TaxPercent").Visible = true;
            settings.Columns.Add("TaxRemarks").Visible = true;
            settings.Columns.Add("Tax_Amount").Visible = true;
            settings.Columns.Add("PO_Item_ID").Visible = false;
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
        public static IEnumerable GetItemOrderedTax(int PO_Item_ID = 0, int Sr = 0)
        {
            List<Return_Get_PO_Items_NestedGrid> data = (List<Return_Get_PO_Items_NestedGrid>)System.Web.HttpContext.Current.Session["PO_IO_NestedGridTaxData"];
            List<Return_Get_PO_Items_NestedGrid> itemrequestTaxlist = new List<Return_Get_PO_Items_NestedGrid>();

            if (PO_Item_ID == 0)
            {
                itemrequestTaxlist = data.FindAll(x => x.MainSr == Sr && x.PO_Item_ID == 0);
            }
            else
            {
                itemrequestTaxlist = data.FindAll(x => x.PO_Item_ID == PO_Item_ID);
            }
            return itemrequestTaxlist;
        }//binding data to nested grid



        public ActionResult Frm_PO_Item_Ordered(int SR = 0, string ActionMethod = null)
        {
            POTaxGridSessionclear();
            PurchaseItemOrderedDetail model = new PurchaseItemOrderedDetail();
            model.ActionMethod = ActionMethod;
            if (ActionMethod == "New")
            {
                model.Purchase_IO_PostedBy = BASE._open_User_ID;
                model.Purchase_IO_POI_Priority = "Normal";
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_Get_PO_Items_MainGrid>)Purchase_ItemOrderedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_Get_PO_Items_MainGrid();
                model.Sr = Sr;
                model.Purchase_IO_ItemID = dataToEdit.ItemID;
                model.Purchase_IO_LocationID = dataToEdit.LocationID;
                model.Purchase_IO_Make = dataToEdit.Make;
                model.Purchase_IO_Model = dataToEdit.Model;
                model.Purchase_IO_RequestedQty = dataToEdit.Requested_Qty;//Mantis bug 0000883 fixed
                model.Purchase_IO_OrderedQty = dataToEdit.OrderedQty;
                model.Purchase_IO_UnitID = dataToEdit.UnitID;
                model.Purchase_IO_Rate = dataToEdit.Rate;
                model.Purchase_IO_Unit = dataToEdit.Unit;
                model.Purchase_IO_ItemName = dataToEdit.ItemName;
                model.Purchase_IO_RR_Item_Sr_No = dataToEdit.RR_Item_Sr_No;
                model.Purchase_IO_RR = dataToEdit.RR;
                model.Purchase_IO_AddUpdateReason = dataToEdit.AddUpdateReason;
                model.Purchase_IO_DestLocation = dataToEdit.DestLocation;
                model.Purchase_IO_PostedBy = dataToEdit.PostedBy;             
                model.Purchase_IO_Discount = dataToEdit.Discount;
                model.Purchase_IO_Taxes = dataToEdit.Taxes;
                model.Purchase_IO_POI_Priority= dataToEdit.POI_Priority;
                model.Purchase_IO_Reqd_Del_Date= dataToEdit.Reqd_Del_Date;
                model.Purchase_IO_Amount = dataToEdit.Amount;
                model.Purchase_IO_Remarks = dataToEdit.Remarks;
                model.Purchase_IO_RR = dataToEdit.RR;
                model.Purchase_IO_RequestID = dataToEdit.RequestID;
                model.Purchase_IO_Rate_After_Discount = dataToEdit.Rate_after_Discount == 0 ? (double?)null : Convert.ToDouble(dataToEdit.Rate_after_Discount);

                model.ID = dataToEdit.ID;

                List<Return_Get_PO_Tax_Detail> taxgriddata = new List<Return_Get_PO_Tax_Detail>();


                var all_data_nested = (List<Return_Get_PO_Items_NestedGrid>)PO_IO_NestedGridTaxData;
                List<Return_Get_PO_Items_NestedGrid> dataToEditnested = new List<Return_Get_PO_Items_NestedGrid>();
                if (all_data_nested != null)
                {
                    dataToEditnested = all_data_nested.FindAll(x => x.MainSr == Sr);
                }
                if (dataToEditnested != null)
                {

                    for (int I = 0; I <= dataToEditnested.Count() - 1; I++)
                    {
                        Return_Get_PO_Tax_Detail taxgrid = new Return_Get_PO_Tax_Detail();


                        taxgrid.Sr = dataToEditnested[I].Sr;
                        taxgrid.TaxPercent = dataToEditnested[I].TaxPercent;
                        taxgrid.TaxType = dataToEditnested[I].Tax_Type;
                        taxgrid.TaxTypeID = dataToEditnested[I].Tax_TypeID;
                        taxgrid.Tax_Amount = dataToEditnested[I].Tax_Amount;
                        taxgrid.Remarks = dataToEditnested[I].TaxRemarks;
                        taxgrid.ID = dataToEditnested[I].ID;
                        taxgriddata.Add(taxgrid);
                        PO_ItemRequestedTaxDetailData = taxgriddata;



                    }


                }


            }
            return PartialView(model);
        }

        public ActionResult Frm_PO_Item_Ordered_Save(PurchaseItemOrderedDetail model)
        {
            var actionmethod = model.ActionMethod;
            List<Return_Get_PO_Items_MainGrid> gridRows = new List<Return_Get_PO_Items_MainGrid>();
            List<Return_Get_PO_Items_NestedGrid> gridRowsNested = new List<Return_Get_PO_Items_NestedGrid>();

            //if (model.Purchase_IO_Reqd_Del_Date != null)
            //{
            //    if (model.Purchase_IO_Reqd_Del_Date < DateTime.Today)
            //    {
            //        return Json(new
            //        {
            //            result = false,
            //            message = "Required Delivery date can not be past date!"
            //        }, JsonRequestBehavior.AllowGet);
            //    }

            //}

            if (model.Purchase_IO_Discount != null)
            {
                if(model.Purchase_IO_Discount < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Discount Amount can not be negative !"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if(model.Purchase_IO_Rate != null)
            {
                if (model.Purchase_IO_Rate < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Rate Amount can not be negative !"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            
                if (model.Purchase_IO_OrderedQty < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Ordered Quantity can not be negative !"
                    }, JsonRequestBehavior.AllowGet);
                }
            
            if (model.Purchase_IO_RR == null)
            {
                if (model.Purchase_IO_AddUpdateReason == null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Reason for Add/Update is mandatory !"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

     

        var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Purchase_ItemOrderedData != null)
            {
                gridRows = (List<Return_Get_PO_Items_MainGrid>)Purchase_ItemOrderedData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }

            if (PO_IO_NestedGridTaxData != null)
            {
                gridRowsNested = (List<Return_Get_PO_Items_NestedGrid>)PO_IO_NestedGridTaxData;
            }
            if (actionmethod == "New")
            {
                Return_Get_PO_Items_MainGrid grid = new Return_Get_PO_Items_MainGrid();
                grid.Sr = NewSr;
                grid.AddedOn = DateTime.Now;
                grid.AddedBy = BASE._open_User_ID;
                grid.ItemID = model.Purchase_IO_ItemID;
                grid.ItemCode = model.Purchase_IO_ItemCode;
                grid.Head = model.Purchase_IO_Head;
                grid.ItemType = model.Purchase_IO_ItemType;                
                grid.LocationID = model.Purchase_IO_LocationID;
                grid.ItemName = model.Purchase_IO_ItemName;
                grid.Make = model.Purchase_IO_Make;
                grid.Model = model.Purchase_IO_Model;                
                grid.UnitID = model.Purchase_IO_UnitID;
                grid.Unit = model.Purchase_IO_Unit;
                grid.DestLocation = model.Purchase_IO_DestLocation;
                grid.Requested_Qty = model.Purchase_IO_RequestedQty;
                grid.OrderedQty = model.Purchase_IO_OrderedQty;
                grid.Rate = model.Purchase_IO_Rate;
                grid.Rate_after_Discount = Convert.ToDecimal(model.Purchase_IO_Rate_After_Discount);

                grid.Discount = model.Purchase_IO_Discount;
                grid.Taxes = model.Purchase_IO_Taxes;              
                grid.POI_Priority = model.Purchase_IO_POI_Priority;
                grid.Reqd_Del_Date = model.Purchase_IO_Reqd_Del_Date;//0000109 bug Fixed
                grid.Amount = model.Purchase_IO_Amount;
                grid.AddUpdateReason = model.Purchase_IO_AddUpdateReason;
                grid.PostedBy = model.Purchase_IO_PostedBy;
                grid.Remarks = model.Purchase_IO_Remarks;
                grid.RR = model.Purchase_IO_RR;
                grid.RR_Item_Sr_No = model.Purchase_IO_RR_Item_Sr_No;
                grid.ID = 0;
                grid.RR_Item_Sr_No = NewSr;
                grid.PendingQty = model.Purchase_IO_OrderedQty; 
                grid.RequestID = 0;

                gridRows.Add(grid);

                List<Return_Get_PO_Tax_Detail> taxgriddata = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;

                if (taxgriddata != null)
                {

                    for (int I = 0; I <= taxgriddata.Count() - 1; I++)
                    {
                        Return_Get_PO_Items_NestedGrid nestedgrid = new Return_Get_PO_Items_NestedGrid();


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
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                if (dataToEdit.ID != 0)
                {
                    var editItemOrdID = new ArrayList();
                    var editItemOrd = PurchaseEdit_ItemOrd_ID as ArrayList;
                    if (editItemOrd != null)
                    {
                        editItemOrd.Add(model.ID);
                        PurchaseEdit_ItemOrd_ID = editItemOrd;
                    }
                    else
                    {
                        editItemOrdID.Add(model.ID);
                        PurchaseEdit_ItemOrd_ID = editItemOrdID;
                    }
                }


                dataToEdit.RR_Item_Sr_No = model.Purchase_IO_RR_Item_Sr_No;
                dataToEdit.PendingQty = model.PendingQty;
                dataToEdit.ItemID = model.Purchase_IO_ItemID;
                dataToEdit.ItemCode = model.Purchase_IO_ItemCode;
                dataToEdit.Head = model.Purchase_IO_Head;
                dataToEdit.ItemType = model.Purchase_IO_ItemType;
                dataToEdit.LocationID = model.Purchase_IO_LocationID;
                dataToEdit.DestLocation = model.Purchase_IO_DestLocation;
                dataToEdit.Make = model.Purchase_IO_Make;
                dataToEdit.Model = model.Purchase_IO_Model;
                dataToEdit.UnitID = model.Purchase_IO_UnitID;
                dataToEdit.Unit = model.Purchase_IO_Unit;
                dataToEdit.Requested_Qty = model.Purchase_IO_RequestedQty;
                dataToEdit.ItemName = model.Purchase_IO_ItemName;
                dataToEdit.OrderedQty = model.Purchase_IO_OrderedQty;
                dataToEdit.Rate = model.Purchase_IO_Rate;
                dataToEdit.Rate_after_Discount = Convert.ToDecimal(model.Purchase_IO_Rate_After_Discount);

                dataToEdit.Discount = model.Purchase_IO_Discount;
                dataToEdit.Taxes = model.Purchase_IO_Taxes;
                dataToEdit.POI_Priority = model.Purchase_IO_POI_Priority;
                dataToEdit.Reqd_Del_Date = model.Purchase_IO_Reqd_Del_Date;//0000109 bug Fixed
                dataToEdit.Amount = model.Purchase_IO_Amount;
                dataToEdit.AddUpdateReason = model.Purchase_IO_AddUpdateReason;
                dataToEdit.PostedBy = model.Purchase_IO_PostedBy;
                dataToEdit.Remarks = model.Purchase_IO_Remarks;
                dataToEdit.RR = model.Purchase_IO_RR;
                dataToEdit.RR_Item_Sr_No = model.Purchase_IO_RR_Item_Sr_No;
                dataToEdit.RequestID = model.Purchase_IO_RequestID;

                //for nested grid edit

                List<Return_Get_PO_Items_NestedGrid> dataToEditnested = new List<Return_Get_PO_Items_NestedGrid>();
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


                    List<Return_Get_PO_Tax_Detail> taxgriddata = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;


                    if (taxgriddata != null)
                    {

                        for (int I = 0; I <= taxgriddata.Count() - 1; I++)
                        {
                            Return_Get_PO_Items_NestedGrid nestedgrid = new Return_Get_PO_Items_NestedGrid();


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

            Purchase_ItemOrderedData = gridRows;
            PO_IO_NestedGridTaxData = gridRowsNested;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
      

        public ActionResult Get_PurchaseStockitems(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            Param_GetStoreItems inparam = new Param_GetStoreItems();

            inparam.StoreID = null;

            var StockItemList = BASE._Sub_Item_DBOps.GetStoreItems(inparam, ClientScreen.Stock_PO);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StockItemList, loadOptions)), "application/json");
        }

        public ActionResult Get_Purchase_IO_DestLocation(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
          
            var DestLocList = BASE._PO_DBOps.Get_PO_Item_Dest_Locations(Convert.ToInt32(PO_ID_Glob));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DestLocList, loadOptions)), "application/json");
        }

        public ActionResult Lookup_IO_Unit(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {

            var UnitIOList = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UnitIOList, loadOptions)), "application/json");
        }


        //public ActionResult Frm_PO_Request_Add_Taxes()
        //{
        //    return View();
        //}

        public ActionResult DataNavigationItemOrdered(string ActionMethod = null, int ID = 0)
        {

            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                //var Sr = Convert.ToInt16(SR);
                //var all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                //var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();
                if (ID != 0)
                {
                    var retcount = BASE._PO_DBOps.GetPOItem_Received_EntryCount(ID);
                    if (retcount > 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "Item Ordered Entry against which Receipts has been posted can not be Edited/ Deleted "
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }

            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_Unsaved_Item_Entry_in_Received_Grid(int ID_Item = 0) 
        {


            var all_data_Of_Received_Grid = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetPOGoodsReceived> datatocheckdelivery = all_data_Of_Received_Grid.FindAll(x => x.PO_Item_Rec_ID == ID_Item && x.ID == 0);


                    if (datatocheckdelivery.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in  Received Grid, please save it to Edit/Delete this row."
                        }, JsonRequestBehavior.AllowGet);


                    }
                }


            }


            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PO_TaxGridData(int subitemID = 0, int PO_ItemID = 0)
        {

            var POTaxList = new List<Return_Get_PO_Tax_Detail>();
            var POID = PO_ID_Glob;
            int POItemID = PO_ItemID;
            int subitem = subitemID;

            if (PO_ItemRequestedTaxDetailData == null)
            {
                List<Return_Get_PO_Tax_Detail> POtaxData = BASE._PO_DBOps.Get_PO_Tax_Detail(POID, POItemID, subitem);
                if (POtaxData != null)
                {
                    POTaxList = POtaxData;
                }
                PO_ItemRequestedTaxDetailData = POTaxList;
            }
            return PartialView(PO_ItemRequestedTaxDetailData);
        }

        public ActionResult Get_TaxType(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockPurchaseOrder.Return_GetTaxType> Taxdata = BASE._PO_DBOps.GetTaxType();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Taxdata, loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult Frm_PO_Request_Add_Taxes(string ActionMethod = null, int SR = 0)
        {
            PurchaseItemOrderedDetail model = new PurchaseItemOrderedDetail();

            model.PO_Taxes_ActionMethod = ActionMethod;

            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_Get_PO_Tax_Detail();
                model.Sr = Sr;
                model.PO_Addtax_TaxPercent = dataToEdit.TaxPercent;
                model.PO_Addtax_TaxType = dataToEdit.TaxType;
                model.PO_Addtax_TaxTypeID = dataToEdit.TaxTypeID;
                model.PO_Addtax_TaxAmount = Convert.ToDouble(dataToEdit.Tax_Amount);
                model.PO_Addtax_Remarks = dataToEdit.Remarks;
                model.PO_Addtax_RecID = dataToEdit.ID;

            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_PO_Request_Add_Taxes(PurchaseItemOrderedDetail model)
        {

            List<Return_Get_PO_Tax_Detail> gridRows = new List<Return_Get_PO_Tax_Detail>();

            if (model.PO_Addtax_TaxPercent != 0 && (model.PO_Addtax_TaxPercent <= 0 || model.PO_Addtax_TaxPercent > 100))
            {
                return Json(new
                {
                    result = false,
                    message = "Tax Percentage Should Be Greater Than 0 and Less than 100...!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.PO_Addtax_TaxTypeID == null)
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
            if (PO_ItemRequestedTaxDetailData != null)
            {
                gridRows = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                NewSr = LastRowSr + 1;
            }
            if (model.PO_Taxes_ActionMethod == "_New" || model.PO_Taxes_ActionMethod == "_NewAddTax")
            {
                Return_Get_PO_Tax_Detail grid = new Return_Get_PO_Tax_Detail();
                grid.Sr = NewSr;
                grid.TaxPercent = model.PO_Addtax_TaxPercent;
                grid.TaxType = model.PO_Addtax_TaxType;
                grid.TaxTypeID = model.PO_Addtax_TaxTypeID;
                grid.Tax_Amount = Convert.ToDecimal(model.PO_Addtax_TaxAmount);
                grid.Remarks = model.PO_Addtax_Remarks;
                grid.ID = 0;

                gridRows.Add(grid);
            }
            else if (model.PO_Taxes_ActionMethod == "_Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);

                dataToEdit.TaxPercent = model.PO_Addtax_TaxPercent;
                dataToEdit.TaxType = model.PO_Addtax_TaxType;
                dataToEdit.TaxTypeID = model.PO_Addtax_TaxTypeID;
                dataToEdit.Tax_Amount = Convert.ToDecimal(model.PO_Addtax_TaxAmount);
                dataToEdit.Remarks = model.PO_Addtax_Remarks;

            }
            PO_ItemRequestedTaxDetailData = gridRows;

            var actionmethodvalue = model.PO_Taxes_ActionMethod;
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
            var allData = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_Get_PO_Tax_Detail();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            PO_ItemRequestedTaxDetailData = allData;

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_PO_Request_Manage_Taxes(int SubitemID)
        {
            PurchaseItemOrderedDetail model = new PurchaseItemOrderedDetail();
            model.Purchase_IO_ItemID = SubitemID;
            return PartialView(model);
        }

        public ActionResult CalculateTotalTaxAmount()
        {
            decimal? sum = 0;
            var all_data_Of_tax_Grid = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;
            if (all_data_Of_tax_Grid != null)
            {
                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {

                    sum = sum + all_data_Of_tax_Grid[I].Tax_Amount;


                }

            }

            return Json(new
            {
                Amount = sum,

            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CalculateTaxes(Decimal Quantity, Decimal? Rate, Decimal? RateAfterDiscount)
        {
            decimal? sum = 0;
            var all_data_Of_tax_Grid = (List<Return_Get_PO_Tax_Detail>)PO_ItemRequestedTaxDetailData;
            if (all_data_Of_tax_Grid != null)
            {


                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {
                    var taxpercent = all_data_Of_tax_Grid[I].TaxPercent;

                    var TaxAmt = ((Quantity * RateAfterDiscount) * taxpercent) / 100;


                    all_data_Of_tax_Grid[I].Tax_Amount = TaxAmt;
                }

                for (int I = 0; I <= all_data_Of_tax_Grid.Count() - 1; I++)
                {

                    sum = sum + all_data_Of_tax_Grid[I].Tax_Amount;


                }

            }

            return Json(new
            {
                Amount = sum,

            }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region "Goods Received"

        public ActionResult PO_GoodsReceivedGridData(string ActionMethodName, int PO_ID = 0)
        {
            if (PO_ID != 0)
            {
                PO_ID_Glob = PO_ID;
            
            }
            var GoodsRecdList = new List<Return_GetPOGoodsReceived>();
            if (ActionMethodName == "New")
            {
                return PartialView(GoodsRecdList); 
            }
            if (Purchase_GoodsReceivedData == null)
            {
                var GoodsRecdData = BASE._PO_DBOps.GetPOGoodsReceived(PO_ID);
                if (GoodsRecdData != null)
                {
                    GoodsRecdList = GoodsRecdData;
                }
                Purchase_GoodsReceivedData = GoodsRecdList;
            }
            return PartialView(Purchase_GoodsReceivedData);
        }

        public ActionResult Frm_PO_Goods_Received(string ActionMethod, int SR = 0, int POid = 0)
        {

            PurchaseGoodsReceivedDetail model = new PurchaseGoodsReceivedDetail();
            model.ActionMethod = ActionMethod;
            model.POid = POid;
            if (model.POid != 0)
            {
                PO_ID_Glob = POid;
            }
            if (ActionMethod == "New")
            {
                model.Purchase_GR_ReceivedDate = DateTime.Now;
                model.Purchase_GR_ReceivedByID = BASE._open_User_PersonnelID;
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetPOGoodsReceived();
                model.Sr = Sr;
                model.Purchase_GR_ItemID = dataToEdit.PO_Item_Rec_ID; //this was item id
                model.Purchase_GR_ReceivedQty= dataToEdit.ReceivedQty;
                model.Purchase_GR_ReceivedDate = dataToEdit.ReceivedDate;
                model.Purchase_GR_LotNo = dataToEdit.LotNo;
                model.Purchase_GR_ShipmentModeID= dataToEdit.ShipmentModeID;
                model.Purchase_GR_FOB = dataToEdit.FOB;
                model.Purchase_GR_Carrier = dataToEdit.Carrier;
                model.Purchase_GR_LocationID = dataToEdit.LocationID;
                model.Purchase_GR_BillNo = dataToEdit.BillNo;
                model.Purchase_GR_Warranty = dataToEdit.Warranty;
                model.Purchase_GR_ChallanNo = dataToEdit.ChallanNo;
                model.Purchase_GR_ReceivedByID = dataToEdit.ReceivedByID;
                model.Purchase_GR_FOB = dataToEdit.FOB;
                model.Purchase_GR_CreatedStockID = dataToEdit.CreatedStockID;
                model.Purchase_GR_Remarks = dataToEdit.Remarks;
                model.ID = dataToEdit.ID;
                model.Purchase_GR_SubitemID = dataToEdit.ItemID;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_PO_Goods_Received(PurchaseGoodsReceivedDetail model)
        {
            var actionmethod = model.ActionMethod;

            if (model.Purchase_GR_ReceivedQty <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Received Qty should be greater than 0!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Purchase_GR_FOB == null)
            {
                return Json(new
                {
                    result = false,
                    message = "Free on Board is a mandatory field"
                }, JsonRequestBehavior.AllowGet);
            }

            if (model.Purchase_GR_ReceivedQty > model.Purchase_GR_PendingQuantity)
            {
                return Json(new
                {
                    result = false,
                    message = "Received Qty should not be greater than Pending Qty!"
                }, JsonRequestBehavior.AllowGet);
            }

            if (model.Purchase_GR_ReceivedDate > DateTime.Today)
            {
                return Json(new
                {
                    result = false,
                    message = "Received Date cannot be future date!"
                }, JsonRequestBehavior.AllowGet);
            }

            //if (model.Purchase_GR_ReceivedDate < model.Purchase_GR_ReqDelDate)
            //{
            //    return Json(new
            //    {
            //        result = false,
            //        message = "Received Date cannot be less than Delivery Date!"
            //    }, JsonRequestBehavior.AllowGet);
            //} //mantis #1491
            int? lotnocount = 0;
            int? stockrefidgr = PO_ID_Glob;
                var subitemid = model.Purchase_GR_SubitemID;
            if (model.Purchase_GR_CreatedStockID != 0)
            {
              lotnocount = BASE._PO_DBOps.Get_PO_Duplicate_LotNo_Count(model.Purchase_GR_CreatedStockID, model.Purchase_GR_LotNo, stockrefidgr, subitemid);
            }
            else
            {
             lotnocount = BASE._PO_DBOps.Get_PO_Duplicate_LotNo_Count(null, model.Purchase_GR_LotNo, stockrefidgr, subitemid);
            }
            //lot number
   
            if(lotnocount > 0)

            {
                return Json(new
                {
                    result = false,
                    message = "Please Enter a unique value for Lot No."
                }, JsonRequestBehavior.AllowGet);
            }
            //


            var flag = 0;
            var currentuser = Convert.ToString(Currentuserrole);
            if (currentuser == "RR_Requestor" || currentuser == "RR_Requestor_Incharge")
            {
                Param_GetStoreItems inparam = new Param_GetStoreItems();

                inparam.StoreID = model.Purchase_GR_Stock_Store_Dept_ID;
                var items = BASE._Sub_Item_DBOps.GetStoreItems(inparam, ClientScreen.Stock_PO);
                for (var i = 0; i < items.Count; i++)
                {
                    if (model.Purchase_GR_ItemID == Convert.ToInt16(items[i].ItemID))
                    {
                        flag = 1;

                    }
                }
                if (flag == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "store keepers can receive only items mapped to their store"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.ActionMethod == "Edit")
            {
                if (model.ID != 0)
                {
                    var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(model.Purchase_GR_CreatedStockID, ClientScreen.Stock_PO);
                    if (itemcheck.Count > 0)
                    {
                        var inusescreen = itemcheck[0].Screen;
                        return Json(new
                        {
                            result = false,
                            message = "Goods Received Cannot Be Edited Because It has Been Used In" + inusescreen + "...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (model.POid != 0)
            {
                try
                {
                    Param_InsertPurchaseOrderGoodsReceived inparam = new Param_InsertPurchaseOrderGoodsReceived();
                    inparam.PO_ID = model.POid;
                    inparam.Stock_Value = model.Purchase_GR_Stock_Value;
                    inparam.Store_Dept_ID = model.Purchase_GR_Stock_Store_Dept_ID;
                    inparam.Lot_Serial_no = model.Purchase_GR_LotNo;
                    inparam.Project_ID = model.Purchase_GR_Stock_Proj_ID;
                    inparam.Model = model.Purchase_GR_Model;
                    inparam.Make = model.Purchase_GR_Make;
                    //inparam.sub_Item_ID = model.Purchase_GR_ItemID;
                    inparam.Receiver_Remarks = model.Purchase_GR_Remarks;
                    inparam.Received_By_ID = model.Purchase_GR_ReceivedByID;
                    inparam.Challan_No = model.Purchase_GR_ChallanNo;
                    inparam.Bill_No = model.Purchase_GR_BillNo;
                    inparam.Delivery_Location_ID = model.Purchase_GR_LocationID;
                    inparam.Delivery_Carrier = model.Purchase_GR_Carrier;
                    inparam.FOB = model.Purchase_GR_FOB;
                    inparam.Delivery_Mode = model.Purchase_GR_ShipmentModeID;
                    inparam.Recd_Date = model.Purchase_GR_ReceivedDate;
                    inparam.Recd_Qty = model.Purchase_GR_ReceivedQty;
                    inparam.PO_Item_ID = model.Purchase_GR_PO_Item_Rec_ID;
                    inparam.Unit_ID = model.Purchase_GR_UnitID;
                    inparam.Warranty = model.Purchase_GR_Warranty;
                    inparam.sub_Item_ID = model.Purchase_GR_SubitemID;
                  


                    if (BASE._PO_DBOps.InsertPurchaseOrderGoodsReceived(inparam))
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

                List<Return_GetPOGoodsReceived> gridRows = new List<Return_GetPOGoodsReceived>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Purchase_GoodsReceivedData != null)
                {
                    gridRows = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (actionmethod == "New")
                {
                    Return_GetPOGoodsReceived grid = new Return_GetPOGoodsReceived();
                    grid.Sr = NewSr;
                    grid.ItemID = model.Purchase_GR_SubitemID;
                    grid.ReceivedQty = model.Purchase_GR_ReceivedQty;
                    grid.ReceivedDate = model.Purchase_GR_ReceivedDate;
                    grid.LotNo = model.Purchase_GR_LotNo;
                    grid.ShipmentModeID = model.Purchase_GR_ShipmentModeID;
                    grid.FOB = model.Purchase_GR_FOB;
                    grid.Carrier = model.Purchase_GR_Carrier;
                    grid.LocationID = model.Purchase_GR_LocationID;
                    grid.BillNo = model.Purchase_GR_BillNo;
                    grid.Warranty = model.Purchase_GR_Warranty;
                    grid.ChallanNo = model.Purchase_GR_ChallanNo;
                    grid.ReceivedByID = model.Purchase_GR_ReceivedByID;
                    grid.Remarks = model.Purchase_GR_Remarks;
                    grid.ReceivedBy = model.Purchase_GR_ReceivedBy;

                    grid.ItemName = model.Purchase_GR_ItemName;
                    grid.ItemCode = model.Purchase_GR_ItemCode;
                    grid.Head = model.Purchase_GR_Head;
                    grid.Make = model.Purchase_GR_Make;
                    grid.Model = model.Purchase_GR_Model;
                    grid.Stock_Value = model.Purchase_GR_ReceivedQty * Convert.ToDecimal(model.Purchase_GR_Rate) ;
                    grid.Stock_Store_Dept_ID = model.Purchase_GR_Stock_Store_Dept_ID;
                    grid.Stock_Proj_ID = model.Purchase_GR_Stock_Proj_ID;
                    grid.UnitID = model.Purchase_GR_UnitID;
                    grid.PO_Item_Rec_ID = model.Purchase_GR_PO_Item_Rec_ID;
                    grid.POI_Priority = model.Purchase_GR_Priority;
                    grid.Unit = model.Purchase_GR_Unit;

                    grid.ShipmentMode = model.Purchase_GR_ShipmentMode;

                    grid.ID = 0;
                    grid.AddedOn = DateTime.Now;
                    grid.AddedBy = BASE._open_User_ID;
                    grid.ReqDelDate = Convert.ToDateTime(model.Purchase_GR_ReqDelDate);



                    gridRows.Add(grid);
                }
                else if (actionmethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                    if (dataToEdit.ID != 0)
                    {
                        var editItemRecID = new ArrayList();
                        var editItemRec = PurchaseEdit_GoodsRec_ID as ArrayList;
                        if (editItemRec != null)
                        {
                            editItemRec.Add(model.ID);
                            PurchaseEdit_GoodsRec_ID = editItemRec;
                        }
                        else
                        {
                            editItemRecID.Add(model.ID);
                            PurchaseEdit_GoodsRec_ID = editItemRecID;
                        }
                    }

                    dataToEdit.ItemName = model.Purchase_GR_ItemName;
                    dataToEdit.ItemID = model.Purchase_GR_SubitemID; // this was earlier item id
                    dataToEdit.ReceivedQty = model.Purchase_GR_ReceivedQty;
                    dataToEdit.ReceivedDate = model.Purchase_GR_ReceivedDate;
                    dataToEdit.LotNo = model.Purchase_GR_LotNo;
                    dataToEdit.ShipmentModeID = model.Purchase_GR_ShipmentModeID;
                    dataToEdit.FOB = model.Purchase_GR_FOB;
                    dataToEdit.Carrier = model.Purchase_GR_Carrier;
                    dataToEdit.LocationID = model.Purchase_GR_LocationID;
                    dataToEdit.BillNo = model.Purchase_GR_BillNo;
                    dataToEdit.Warranty = model.Purchase_GR_Warranty;
                    dataToEdit.ChallanNo = model.Purchase_GR_ChallanNo;
                    dataToEdit.ReceivedByID = model.Purchase_GR_ReceivedByID;
                    dataToEdit.Remarks = model.Purchase_GR_Remarks;
                    
                    dataToEdit.ItemName = model.Purchase_GR_ItemName;
                    dataToEdit.ItemCode = model.Purchase_GR_ItemCode;
                    dataToEdit.Head = model.Purchase_GR_Head;
                    dataToEdit.Make = model.Purchase_GR_Make;
                    dataToEdit.Model = model.Purchase_GR_Model;
                    dataToEdit.Stock_Value =  model.Purchase_GR_ReceivedQty * Convert.ToDecimal(model.Purchase_GR_Rate);
                    dataToEdit.Stock_Store_Dept_ID = model.Purchase_GR_Stock_Store_Dept_ID;
                    dataToEdit.Stock_Proj_ID = model.Purchase_GR_Stock_Proj_ID;
                    dataToEdit.UnitID = model.Purchase_GR_UnitID;
                    dataToEdit.PO_Item_Rec_ID = model.Purchase_GR_PO_Item_Rec_ID;
                    dataToEdit.POI_Priority = model.Purchase_GR_Priority;
                    dataToEdit.Unit = model.Purchase_GR_Unit;
                    dataToEdit.ReceivedBy = model.Purchase_GR_ReceivedBy;
                    dataToEdit.ShipmentMode = model.Purchase_GR_ShipmentMode;

                    dataToEdit.ReqDelDate = Convert.ToDateTime(model.Purchase_GR_ReqDelDate);

                    dataToEdit.EditedOn = DateTime.Now;
                    dataToEdit.EditedBy = BASE._open_User_ID;
                }
                Purchase_GoodsReceivedData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Frm_PO_GoodsRecd_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
        {
            var SR = Convert.ToInt16(sr);
            var allData = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetPOGoodsReceived();
            var ItemRecID = dataToDelete.PO_Item_Rec_ID;

            if (dataToDelete.CreatedStockID != 0)
            {
                var goodsrecdcheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.CreatedStockID, ClientScreen.Stock_PO);
                if (goodsrecdcheck.Count > 0)
                {
                    var inusedscreen = goodsrecdcheck[0].Screen;
                    return Json(new
                    {
                        result = false,
                        message = "Goods Received Cannot Be Deleted Because It has Been Used In" + inusedscreen + "...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }


            if (dataToDelete.ID != 0)
            {
                var retcount = BASE._PO_DBOps.GetPOReceipt_Return_EntryCount(dataToDelete.ID);
                if (retcount > 0)
                {

                    return Json(new
                    {
                        result = false,
                        message = "Receipt Entry against which Returns has been posted can not be deleted "
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Purchase_GoodsReceivedData = allData;
            var deleteItemRecID = new ArrayList();
            if (id != 0)
            {
                var deleteGRID = new ArrayList();
                var deleteItemGR = Delete_GoodsRec_Pur as ArrayList;

                var deleteItemID = DeletedItemRec_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item

                if (deleteItemGR != null)
                {
                    deleteItemGR.Add(id);
                    Delete_GoodsRec_Pur = deleteItemGR;

                    deleteItemID.Add(ItemRecID);
                    DeletedItemRec_ID = deleteItemID;
                }
                else
                {
                    deleteGRID.Add(id);
                    Delete_GoodsRec_Pur = deleteGRID;

                    deleteItemRecID.Add(ItemRecID);
                    DeletedItemRec_ID = deleteItemRecID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Lookup_GR_Req_StockItem(bool? IsVisible, DataSourceLoadOptions loadOptions, int? Recd_ID)
        {
            if (Recd_ID != 0)
            {
                var GRItemList = BASE._PO_DBOps.GetPOItemsOrdered_NotFullyReceived(Convert.ToInt32(PO_ID_Glob), Convert.ToInt32(Recd_ID));
                var result = GRItemList.Main_grid_Data;
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(result, loadOptions)), "application/json");
            }
            else
            {
                var GRItemList = BASE._PO_DBOps.GetPOItemsOrdered_NotFullyReceived(Convert.ToInt32(PO_ID_Glob));
                var result = GRItemList.Main_grid_Data;
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(result, loadOptions)), "application/json");

            }
         
        }

        public ActionResult Lookup_GR_Shipment(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var GRShipList = BASE._PO_DBOps.GetDeliveryModes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GRShipList, loadOptions)), "application/json");
        }

        public ActionResult Lookup_GR_RecdLocation(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            List <Return_Get_PO_Item_Dest_Locations> GRRecdLocList = BASE._PO_DBOps.Get_PO_Item_Dest_Locations(Convert.ToInt32(PO_ID_Glob));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GRRecdLocList, loadOptions)), "application/json");
        }

        public ActionResult Lookup_GR_RecdBy(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var GRRecdByList = BASE._PO_DBOps.Get_PO_Item_Received_By(Convert.ToInt32(PO_ID_Glob));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GRRecdByList, loadOptions)), "application/json");
        }

        public JsonResult Received_Item_Entry_Already_in_Same_Grid(int ReceivedID = 0, int poid = 0, int srno = 0) 
        {
           
            var POID = poid;
            var SrNO = srno;
            if (POID != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var all_data_Of_Received_Grid = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
                if (all_data_Of_Received_Grid != null)
                {

                    if (all_data_Of_Received_Grid.Count != 0)
                    {

                        List<Return_GetPOGoodsReceived> datatocheck = all_data_Of_Received_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);

                        List<Return_GetPOGoodsReceived> datatocheckedit = all_data_Of_Received_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);

                        List<Return_GetPOGoodsReceived> datatocheckdelete = all_data_Of_Received_Grid.FindAll(x => x.ID != 0);

                        if (datatocheck.Count != 0)
                        {
                            for (int I = 0; I <= datatocheck.Count() - 1; I++)
                            {

                                if (datatocheck[I].PO_Item_Rec_ID == ReceivedID)
                                {

                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit same row or save PO first for any New Addition/Updation."
                                    }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].PO_Item_Rec_ID == ReceivedID)
                                {

                                    if (PurchaseEdit_GoodsRec_ID != null)
                                    {
                                     
                                        var EditedID = PurchaseEdit_GoodsRec_ID as ArrayList; //for checking the edited ID in case the Item Rec ID is same for multiple rows
                                        if (EditedID != null)
                                        {
                                            for (int i = 0; i <= EditedID.Count - 1; i++)
                                            {
                                                if (datatocheckedit[I].ID == Convert.ToInt32(EditedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "Edited row for this item is already in Grid, please save PO first for any Addition/Updation."
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }

                                    }


                                }
                            }
                        }

                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].PO_Item_Rec_ID == ReceivedID)
                                {

                                    if (Delete_GoodsRec_Pur != null)
                                    {
                                        var deletedID = DeletedItemRec_ID as ArrayList; //mantis bug 1225
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].PO_Item_Rec_ID == Convert.ToInt32(deletedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save PO first for any Addition/Updation."
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }


                    }
                }

                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Received_Item_Entry_in_Returned_Grid(int recid = 0)
        {


            var all_data_Of_Returned_Grid = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
            if (all_data_Of_Returned_Grid != null)
            {

                if (all_data_Of_Returned_Grid.Count != 0)
                {
                    List<Return_GetPOGoodsReturned> datatocheckret = all_data_Of_Returned_Grid.FindAll(x => x.RecdEntryID == recid && x.ID == 0);


                    if (datatocheckret.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in Returned Grid, please save it to Edit/Delete this row."
                        }, JsonRequestBehavior.AllowGet);


                    }
                }


            }


            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Received_Item_Entry_in_ItemOrdered_Grid(int RecdItemID = 0)
        {


            var all_data_Of_ItemOrdered_Grid = (List<Return_Get_PO_Items_MainGrid>)Purchase_ItemOrderedData;
            if (all_data_Of_ItemOrdered_Grid != null)
            {

                if (all_data_Of_ItemOrdered_Grid.Count != 0)
                {
                    List<Return_Get_PO_Items_MainGrid> datatocheckedit = all_data_Of_ItemOrdered_Grid.FindAll(x => x.ID == RecdItemID && x.ID != 0); 


                    if (datatocheckedit.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                        {

                            if (PurchaseEdit_ItemOrd_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Item Ordered Grid, please save PO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }
         

            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DataNavigationReceived(string ActionMethod = null, int ID = 0)
        {

            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                //var Sr = Convert.ToInt16(SR);
                //var all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                //var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();
                if (ID != 0)
                {
                    var retcount = BASE._PO_DBOps.GetPOReceipt_Return_EntryCount(ID);
                    if (retcount > 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "Receipt Entry against which Returns has been posted can not be Edited/ Deleted "
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }

            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region "Goods Returned"
        public ActionResult PO_GoodsReturnedGridData(string ActionMethodName, int PO_ID = 0)
        {

            var GoodsRetList = new List<Return_GetPOGoodsReturned>();
            if (ActionMethodName == "New")
            {
                return PartialView(GoodsRetList);
            }
            if (Purchase_GoodsReturnedData == null)
            {
                var GoodsRetData = BASE._PO_DBOps.GetPOGoodsReturned(PO_ID);
                if (GoodsRetData != null)
                {
                    GoodsRetList = GoodsRetData;
                }
                Purchase_GoodsReturnedData = GoodsRetList;
            }
            return PartialView(Purchase_GoodsReturnedData);
        }
        public ActionResult Frm_PO_Goods_Returned(string ActionMethod, int SR = 0, int POid = 0)
            {

            PurchaseGoodsReturnedDetail model = new PurchaseGoodsReturnedDetail();
                model.ActionMethod = ActionMethod;
            model.POid = POid;
            if (model.POid != 0)
            {
                PO_ID_Glob = POid;
            }
            if (ActionMethod == "New")
                {
                model.Purchase_GRE_ReturnedDate = DateTime.Now;
                model.Purchase_GRE_ReturnedByID = BASE._open_User_PersonnelID;
            }
                if (ActionMethod == "Edit" || ActionMethod == "View")
                {
                    var Sr = Convert.ToInt16(SR);
                    var all_data = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
                    var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetPOGoodsReturned();
                    model.Sr = Sr;
                    model.Purchase_GRE_ItemID = dataToEdit.RecdEntryID;                  
                    model.Purchase_GRE_ReturnedQty = dataToEdit.ReturnedQty;                   
                    model.Purchase_GRE_ReturnedDate = dataToEdit.ReturnedDate;
                model.Purchase_GRE_ItemName = dataToEdit.ItemName;
                    model.Purchase_GRE_ChallanNo = dataToEdit.ChallanNo;
                    model.Purchase_GRE_ShipmentModeID = dataToEdit.ShipmentModeID;
                    model.Purchase_GRE_ShipmentMode = dataToEdit.ShipmentMode;
                    model.Purchase_GRE_Carrier = dataToEdit.Carrier;
                    model.Purchase_GRE_ReturnedBy = dataToEdit.ReturnedBy;
                model.Purchase_GRE_ReturnedByID = dataToEdit.ReturnedByID;
                model.Purchase_GRE_Remarks = dataToEdit.Remarks;
                model.Purchase_GRE_BillNo = dataToEdit.BillNo;
                model.ID = dataToEdit.ID;
                model.Purchase_GRE_SubitemID = dataToEdit.ItemID;
                }
                return PartialView(model);
            }

        [HttpPost]
        public ActionResult Frm_PO_Goods_Returned(PurchaseGoodsReturnedDetail model)
        {
            var actionmethod = model.ActionMethod;

            if (model.Purchase_GRE_ReturnedQty <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Returned Qty should be greater than 0!" //Mantis bug 1589 case 1-4 fixed
                }, JsonRequestBehavior.AllowGet);
            }

            if (model.Purchase_GRE_ReturnedQty > model.Purchase_GRE_PendingQuantity)
            {
                return Json(new
                {
                    result = false,
                    message = "Returned Qty should not be greater than Pending Qty to Return!"
                }, JsonRequestBehavior.AllowGet);
            }
            var stockqty  = BASE._PO_DBOps.Get_Stock_Current_Quantity_Count(model.Purchase_GRE_ReturnedStockID);
            if (stockqty < model.Purchase_GRE_ReturnedQty)//Mantis bug 1267 fixed
            {
              
                return Json(new
                {
                    message = "The Returned Qty shouldn’t be greater than the available qty of the Stock Created by Goods received..!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }

        
            if (model.Purchase_GRE_ReturnedDate > DateTime.Today)
            {
                return Json(new
                {
                    result = false,
                    message = "Returned Date cannot be future date!"
                }, JsonRequestBehavior.AllowGet);
            }

            if (model.Purchase_GRE_ReturnedDate < model.Purchase_GRE_ReceivedDate)
            {
                return Json(new
                {
                    result = false,
                    message = "Returned Date should be greater than or Equal to Received date!"
                }, JsonRequestBehavior.AllowGet);
            }

            var currentuser = Convert.ToString(Currentuserrole);
            if (currentuser != "Purchaser")
            {


            }

            if (model.POid != 0)
            {
                try
                {
                    Param_InsertPurchaseOrderGoodsReturned inparam = new Param_InsertPurchaseOrderGoodsReturned();
                    inparam.PO_ID = model.POid;
                    inparam.Recd_Item_ID = model.Purchase_GRE_PO_Item_Rec_ID;
                    inparam.Returned_Qty = model.Purchase_GRE_ReturnedQty;
                    inparam.Returned_Date = model.Purchase_GRE_ReturnedDate;
                    inparam.Delivery_Mode = model.Purchase_GRE_ShipmentModeID;
                    inparam.Delivery_Carrier = model.Purchase_GRE_Carrier;
                    inparam.Returned_By_ID = model.Purchase_GRE_ReturnedByID;
                    inparam.Returned_by_Remarks = model.Purchase_GRE_Remarks;
                    inparam.Challan_No = model.Purchase_GRE_ChallanNo;//Mantis bug 936 fixed



                    if (BASE._PO_DBOps.InsertPurchaseOrderGoodsReturned(inparam))
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
                List<Return_GetPOGoodsReturned> gridRows = new List<Return_GetPOGoodsReturned>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (Purchase_GoodsReturnedData != null)
                {
                    gridRows = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (actionmethod == "New")
                {
                    Return_GetPOGoodsReturned grid = new Return_GetPOGoodsReturned();
                    grid.Sr = NewSr;
                    grid.ItemID = model.Purchase_GRE_SubitemID;
                    grid.ItemName = model.Purchase_GRE_ItemName;
                    grid.ItemCode = model.Purchase_GRE_ItemCode;
                    grid.Make = model.Purchase_GRE_Make;
                    grid.Model = model.Purchase_GRE_Model;
                    grid.ReturnedQty = model.Purchase_GRE_ReturnedQty;
                    grid.Unit = model.Purchase_GRE_Unit;
                    grid.ReturnedDate = model.Purchase_GRE_ReturnedDate;
                    grid.LotNo = model.Purchase_GRE_LotNo;
                    grid.BillNo = model.Purchase_GRE_BillNo;
                    grid.ChallanNo = model.Purchase_GRE_ChallanNo;
                    grid.ShipmentMode = model.Purchase_GRE_ShipmentMode;
                    grid.Carrier = model.Purchase_GRE_Carrier;
                    grid.ReturnedByID = model.Purchase_GRE_ReturnedByID;
                    grid.ReturnedBy = model.Purchase_GRE_ReturnedBy;
                    grid.Remarks = model.Purchase_GRE_Remarks;
                    grid.ShipmentModeID = model.Purchase_GRE_ShipmentModeID;
                    grid.RecdEntryID = model.Purchase_GRE_PO_Item_Rec_ID;
                    grid.ShipmentMode = model.Purchase_GRE_ShipmentMode;
                    grid.AddedOn = DateTime.Now;
                    grid.AddedBy = BASE._open_User_ID;
                    grid.ID = 0;
                  
                  


                    gridRows.Add(grid);
                }
                else if (actionmethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                    if (model.ID != 0)
                    {
                        var editItemRetID = new ArrayList();
                        var editItemRet = PurchaseEdit_GoodsRet_ID as ArrayList;
                        if (editItemRet != null)
                        {
                            editItemRet.Add(model.ID);
                            PurchaseEdit_GoodsRet_ID = editItemRet;
                        }
                        else
                        {
                            editItemRetID.Add(model.ID);
                            PurchaseEdit_GoodsRet_ID = editItemRetID;
                        }
                    }

                    dataToEdit.ItemName = model.Purchase_GRE_ItemName;
                    dataToEdit.ItemCode = model.Purchase_GRE_ItemCode;

                    dataToEdit.Make = model.Purchase_GRE_Make;
                    dataToEdit.Model = model.Purchase_GRE_Model;
                    dataToEdit.ReturnedQty = model.Purchase_GRE_ReturnedQty;
                    dataToEdit.Unit = model.Purchase_GRE_Unit;
                    dataToEdit.ReturnedDate = model.Purchase_GRE_ReturnedDate;
                    dataToEdit.LotNo = model.Purchase_GRE_LotNo;
                    dataToEdit.BillNo = model.Purchase_GRE_BillNo;
                    dataToEdit.ChallanNo = model.Purchase_GRE_ChallanNo;
                    dataToEdit.ShipmentMode = model.Purchase_GRE_ShipmentMode;
                    dataToEdit.Carrier = model.Purchase_GRE_Carrier;
                    dataToEdit.ReturnedByID = model.Purchase_GRE_ReturnedByID;
                    dataToEdit.ReturnedBy = model.Purchase_GRE_ReturnedBy;
                    dataToEdit.Remarks = model.Purchase_GRE_Remarks;
              
                    dataToEdit.ShipmentModeID = model.Purchase_GRE_ShipmentModeID;
                    dataToEdit.ItemID = model.Purchase_GRE_SubitemID;
                    dataToEdit.RecdEntryID = model.Purchase_GRE_PO_Item_Rec_ID;
                    dataToEdit.EditedOn = DateTime.Now;
                    dataToEdit.EditedBy = BASE._open_User_ID;
                }
                Purchase_GoodsReturnedData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Frm_PO_GoodsRet_Window_Delete_Grid_Record(string ActionMethod, string sr = null, int id = 0)
            {
                var SR = Convert.ToInt16(sr);
                var allData = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
                var dataToDelete = allData != null ? allData.Where(x => x.Sr == SR).FirstOrDefault() : new Return_GetPOGoodsReturned();
                var RecdID = dataToDelete.RecdEntryID;

            if (allData != null)
                {
                    allData.Remove(dataToDelete);
                }
                Purchase_GoodsReturnedData = allData;
            var deleteReceivedID = new ArrayList();
            if (id != 0)
                {
                    var deleteGREID = new ArrayList();
                    var deleteItemGRE = Delete_GoodsRet_Pur as ArrayList;

                var deleteRecdEntryID = DeletedReceivedEntry_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item

                if (deleteItemGRE != null)
                    {
                        deleteItemGRE.Add(id);
                        Delete_GoodsRet_Pur = deleteItemGRE;

                    deleteRecdEntryID.Add(RecdID);
                    DeletedReceivedEntry_ID = deleteRecdEntryID;
                }
                    else
                    {
                        deleteGREID.Add(id);
                        Delete_GoodsRet_Pur = deleteGREID;

                    deleteReceivedID.Add(RecdID);
                    DeletedReceivedEntry_ID = deleteReceivedID;
                }
                }
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }

        public ActionResult Lookup_GRE_Ret_StockItem(bool? IsVisible, DataSourceLoadOptions loadOptions, int? Ret_ID)
        {
            if (Ret_ID != 0)
            {
                var GREItemList = BASE._PO_DBOps.GetPOGoodsReceived_not_FullyReturned_Only(Convert.ToInt32(PO_ID_Glob), Convert.ToInt32(Ret_ID));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GREItemList, loadOptions)), "application/json");
            }
            else
            {
                var GREItemList = BASE._PO_DBOps.GetPOGoodsReceived_not_FullyReturned_Only(Convert.ToInt32(PO_ID_Glob));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GREItemList, loadOptions)), "application/json");
            }
       
        }

        public ActionResult Lookup_GRE_Shipment(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var GRShipList = BASE._PO_DBOps.GetDeliveryModes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GRShipList, loadOptions)), "application/json");
        }

       

        public ActionResult Lookup_GRE_RetBy(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var GRERecdByList = BASE._PO_DBOps.Get_PO_Item_Received_By(Convert.ToInt32(PO_ID_Glob));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GRERecdByList, loadOptions)), "application/json");
        }

        public JsonResult Return_Item_Entry_Already_in_Same_Grid(int ReturnID = 0, int poid = 0, int srno = 0)
        {
           
            var POID = poid;
            var SrNO = srno;
            if (POID != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var all_data_Of_Returned_Grid = (List<Return_GetPOGoodsReturned>)Purchase_GoodsReturnedData;
                if (all_data_Of_Returned_Grid != null)
                {

                    if (all_data_Of_Returned_Grid.Count != 0)
                    {

                        List<Return_GetPOGoodsReturned> datatocheck = all_data_Of_Returned_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);

                        List<Return_GetPOGoodsReturned> datatocheckedit = all_data_Of_Returned_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);

                        List<Return_GetPOGoodsReturned> datatocheckdelete = all_data_Of_Returned_Grid.FindAll(x => x.ID != 0);

                        if (datatocheck.Count != 0)
                        {
                            for (int I = 0; I <= datatocheck.Count() - 1; I++)
                            {

                                if (datatocheck[I].RecdEntryID == ReturnID)
                                {

                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit same row or save PO first for any New Addition/Updation."
                                    }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].RecdEntryID == ReturnID)
                                {

                                    if (PurchaseEdit_GoodsRet_ID != null)
                                    {
                                        
                                        var EditedID = PurchaseEdit_GoodsRet_ID as ArrayList; //for checking the edited ID in case the Item Rec ID is same for multiple rows
                                        if (EditedID != null)
                                        {
                                            for (int i = 0; i <= EditedID.Count - 1; i++)
                                            {
                                                if (datatocheckedit[I].ID == Convert.ToInt32(EditedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "Edited row for this item is already in Grid, please save PO first for any Addition/Updation."
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                        }

                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].RecdEntryID == ReturnID)
                                {

                                    if (Delete_GoodsRet_Pur != null)
                                    {
                                        var deletedID = DeletedReceivedEntry_ID as ArrayList;
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].RecdEntryID == Convert.ToInt32(deletedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save PO first for any Addition/Updation."
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }

                                        }
                                    }

                                }
                            }
                        }


                    }
                }


                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult Return_Item_Entry_in_Received_Grid(int RetItemID = 0)
        {


            var all_data_Of_Received_Grid = (List<Return_GetPOGoodsReceived>)Purchase_GoodsReceivedData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetPOGoodsReceived> datatocheckedit = all_data_Of_Received_Grid.FindAll(x => x.ID == RetItemID && x.ID != 0);


                    if (datatocheckedit.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                        {

                            if (PurchaseEdit_GoodsRec_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Goods Received Grid, please save PO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }


            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region "Remarks"
        public ActionResult POExistingRemarksGridData(string ActionMethodName, int PO_ID = 0)
        {
            var RemarksList = new List<Return_GetPORemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(RemarksList);
            }
            
            if (PurchaseExisting_Remarks_Grid_Data == null)
            {
                var RemarksData = BASE._PO_DBOps.GetPORemarks(PO_ID); 
                if (RemarksData != null)
                {
                    RemarksList = RemarksData;
                }
                PurchaseExisting_Remarks_Grid_Data = RemarksList;
            }
            return PartialView(PurchaseExisting_Remarks_Grid_Data);
        }
        public ActionResult Frm_ExistingRemarks_Window_Delete_Grid_Record(string ActionMethod, int? ID = 0)
        {
            var id = Convert.ToInt16(ID);
            var allData = (List<Return_GetPORemarks>)PurchaseExisting_Remarks_Grid_Data;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == id).FirstOrDefault() : new Return_GetPORemarks();
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
            PurchaseExisting_Remarks_Grid_Data = allData;
            if (id != 0)
            {
                var deleteRemarksID = new List<int>();
                var deleteExistingRemarks = Delete_PurchaseExisting_Remarks_ID as List<int>;
                if (deleteExistingRemarks != null)
                {
                    deleteExistingRemarks.Add(id);
                    Delete_PurchaseExisting_Remarks_ID = deleteExistingRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_PurchaseExisting_Remarks_ID = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PO_Frm_ViewRemarks(int SR = 0)
        {
            var all_data = (List<Return_GetPORemarks>)PurchaseExisting_Remarks_Grid_Data;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetPORemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("PO_Frm_ViewRemarks", ViewBag.ViewRemarks);
        }

        #endregion

        #region Payment"
        public ActionResult PO_PaymentGridData(string ActionMethodName, int PO_ID = 0)
        {
            
            var PaymentList = new List<Return_GetPOPayments>();
            if (ActionMethodName == "New")
            {
                return PartialView(PaymentList);
            }
            if (Purchase_PaymentData == null)
            {
                var PaymentData = BASE._PO_DBOps.GetPOPayments(PO_ID);
                if (PaymentData != null)
                {
                    PaymentList = PaymentData;
                }
                Purchase_PaymentData = PaymentList;
            }
            return PartialView(Purchase_PaymentData);
        }

        public ActionResult Frm_PO_Payment(int PO_ID, string CallingPage = null, int SupplierID = 0,string PopupID= "Purchaseinnergrid")
        {
            ViewData["PO_AddAccVouPaymentRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            if (!(CheckRights(BASE, ClientScreen.Stock_PO, "Accounts Responsible")))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#"+ PopupID + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            var paymentscallingpage = CallingPage == null ? "InnerGrid" : CallingPage;
            ViewBag.PayCallingPage = paymentscallingpage;
            ViewBag.Paypo_id = PO_ID;
            var paymentmappinglist = new List<Return_GetPOPaymentsForMapping>();            
            var data = BASE._PO_DBOps.GetPOPaymentsForMapping(SupplierID);//pass supplier from session
            if (data != null)
            {
                paymentmappinglist = data;
            }
            PurchasePaymentMappingData = paymentmappinglist;
            return View(PurchasePaymentMappingData);
        }


        public ActionResult Purchase_Payment_Mapping_Grid()
        {
            var finalData = new List<Return_GetPOPaymentsForMapping>();
            if (PurchasePaymentMappingData == null)
            {
                var supplierID = (int)ViewData["EDIT_SupplierID"];
                finalData = BASE._PO_DBOps.GetPOPaymentsForMapping(supplierID);
            }
            else
            {
                finalData = PurchasePaymentMappingData as List<Return_GetPOPaymentsForMapping>;
            }

            return PartialView(finalData);
        }
        public ActionResult PurchaseFindGridKeyValue()
        {
            var griddata = Purchase_PaymentData as List<Return_GetPOPayments>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = griddata[i].TxnMID;

                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult Addingnewpayment(string[] selectedrowarr, string callingscreen = null, int po_id = 0)
        {
            var mappinggriddata = PurchasePaymentMappingData as List<Return_GetPOPaymentsForMapping>;
            if (selectedrowarr != null)
            {
                if (callingscreen != null)
                {
                    try
                    {
                        Param_InsertPurchaseOrderPayment inparam = new Param_InsertPurchaseOrderPayment();
                        mappinggriddata = PurchasePaymentMappingData as List<Return_GetPOPaymentsForMapping>;
                        List<Return_GetPOPayments> newrow = new List<Return_GetPOPayments>();
                        for (int i = 0; i < selectedrowarr.Length; i++)
                        {
                            Return_GetPOPaymentsForMapping mappedrow = mappinggriddata.Find(x => x.Txn_ID == selectedrowarr[i].ToString());
                            Param_InsertPurchaseOrderPayment Inparam = new Param_InsertPurchaseOrderPayment();
                            Inparam.PO_id = po_id;
                           
                            Inparam.Exp_Tr_ID = mappedrow.Txn_ID;
                            Inparam.Exp_Tr_Sr_No = Convert.ToInt32(mappedrow.Txn_Sr_No);
                            BASE._PO_DBOps.InsertPurchaseOrderPayment(Inparam);
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

                     mappinggriddata = PurchasePaymentMappingData as List<Return_GetPOPaymentsForMapping>;
                    List<Return_GetPOPayments> newrow = new List<Return_GetPOPayments>();
                    for (int i = 0; i < selectedrowarr.Length; i++)
                    {
                        var NewSr = i + 1;
                        Return_GetPOPaymentsForMapping mappedrow = mappinggriddata.Find(x => x.Txn_ID.ToString() == selectedrowarr[i]);
                        Return_GetPOPayments data = new Return_GetPOPayments();
                        data.Mode = mappedrow.Mode;
                        data.Payment_Amt = mappedrow.Payment_Amt;
                        data.Payment_Date = mappedrow.Payment_Date;
                        data.Deposited_BankBranch = mappedrow.Deposited_BankBranch;
                        data.Deposited_AcctNo = mappedrow.Deposited_AcctNo;
                        data.Ref_No = mappedrow.Ref_No;
                        data.Ref_Date = mappedrow.Ref_Date;
                        data.ClearingDate = mappedrow.ClearingDate;
                        data.Payment_Branch = mappedrow.Payment_Branch;
                        data.Payment_Bank = mappedrow.Payment_Bank;
                        data.Payment_AcctNo = mappedrow.Payment_AcctNo;
                        data.TxnMID = Convert.ToString(mappedrow.Txn_ID);
                        data.Txn_Sr_No = mappedrow.Txn_Sr_No;
                        //data.ID = mappedrow.ID;
                        data.AddedOn = DateTime.Now;
                        data.AddedBy = BASE._open_User_ID;
                        data.Sr = NewSr;
                        newrow.Add(data);
                    }
                    Purchase_PaymentData = newrow;
                    return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Purchase_PaymentData = null;
                return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PaymentDelete(int SR, int id = 0)
        {
            var Final_Data = Purchase_PaymentData as List<Return_GetPOPayments>;
            var onlyMatch = Final_Data.Single(s => s.Sr == SR);
            Final_Data.Remove(onlyMatch);
            Purchase_PaymentData = Final_Data;
            if (id != 0)
            {
                var deleteItemEstID = new ArrayList();
                var deleteItemEst = Delete_Payment_pur as ArrayList;
                if (deleteItemEst != null)
                {
                    deleteItemEst.Add(id);
                    Delete_Payment_pur = deleteItemEst;
                }
                else
                {
                    deleteItemEstID.Add(id);
                    Delete_Payment_pur = deleteItemEstID;
                }
            }
            return Json(new { result = true, message = "Deleted Successfully", }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Linked User Order"
        public ActionResult PO_LinkedUserOrderMainGridData(string ActionMethodName, int PO_ID = 0)
        {
            
             //var LinkUserOrderMainList = new List<DbOperations.StockPurchaseOrder.Return_GetPOLinkedUserOrders_MainGrid>();
           
            if (Purchase_LinkUserOrderMainData == null)
            {
                var LinkUserOrderData = BASE._PO_DBOps.GetPOLinkedUserOrders(PO_ID);
               

                if (LinkUserOrderData != null)
                {
                    //LinkUserOrderMainList = LinkUserOrderMainData;
                    var LinkUserOrderMainData = LinkUserOrderData.main_Register;
                    var LinkUserOrderNestedData = LinkUserOrderData.nested_Register;
                    Purchase_LinkUserOrderMainData = LinkUserOrderMainData;
                    Purchase_LinkUserOrderNestedData = LinkUserOrderNestedData;
                    Session["Purchase_LinkUserOrderNestedData"] = Purchase_LinkUserOrderNestedData;
                }
                
            }
            List<Return_GetPOLinkedUserOrders_MainGrid> Mastergrid_data = Purchase_LinkUserOrderMainData as List<Return_GetPOLinkedUserOrders_MainGrid>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }

        public ActionResult PO_LinkedUserOrderNestedGridData(string ActionMethodName, int PO_ID = 0, int UOID = 0)
        {

            //var LinkUserOrderNestedList = new List<DbOperations.StockPurchaseOrder.Return_GetPOLinkedUserOrders_NestedGrid>();
            //if (ActionMethodName == "New")
            //{
            //    return PartialView(LinkUserOrderNestedList);
            //}
            if (Purchase_LinkUserOrderNestedData == null)
            {
                var LinkUserOrderData = BASE._PO_DBOps.GetPOLinkedUserOrders(PO_ID);
                var LinkUserOrderNestedData = LinkUserOrderData.nested_Register;
                Purchase_LinkUserOrderNestedData = LinkUserOrderNestedData;
            }  

            var data = Purchase_LinkUserOrderNestedData as List<Return_GetPOLinkedUserOrders_NestedGrid>;
            List<Return_GetPOLinkedUserOrders_NestedGrid> itemuo = data.FindAll(x => x.UOID == UOID);
            return PartialView(itemuo);
        }
        
        public static GridViewSettings PurchaseRegisterUONestedGridSettings(int UOID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PO_LinkedUserOrderNestedGrid" + UOID;
            settings.SettingsDetail.MasterGridName = "PO_LinkedUserOrderMainGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Sr");
            settings.Columns.Add("ItemName");
            settings.Columns.Add("ItemCode").Visible = true;
            settings.Columns.Add("Head").Visible = true;
            settings.Columns.Add("Make").Visible = true;
            settings.Columns.Add("Model").Visible = true;
            settings.Columns.Add("Requested_Qty").Visible = true;
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Dest_Location").Visible = true;
           
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("UOID").Visible = false;
            //settings.ClientSideEvents.FocusedRowChanged = "OnPurchaseLinkedUserOrderNestedGridFocusedRowChanged";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            //settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for exporting nested grid 

        public static IEnumerable GetItemUO(int UOID)
        {
            List<Return_GetPOLinkedUserOrders_NestedGrid> data = (List<Return_GetPOLinkedUserOrders_NestedGrid>)System.Web.HttpContext.Current.Session["Purchase_LinkUserOrderNestedData"];
            List<Return_GetPOLinkedUserOrders_NestedGrid> itemuolist = data.FindAll(x => x.UOID == UOID);
            return itemuolist;
        }//binding data to nested grid


        public ActionResult Purchase_UO_Save()
        {
            //NEVD_UserOrder model = (NEVD_UserOrder)TempData["ModelData"];
         
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Linked RR"
        public ActionResult PO_LinkedRRMainGridData(string ActionMethodName, int PO_ID = 0)
        {
            
            if (PO_ID !=0) PO_ID_Glob = PO_ID;
            ViewData["PO_ID"] = PO_ID_Glob;
            if (Purchase_LinkRRMainData == null)
            {
                var LinkRRData = BASE._PO_DBOps.GetPOLinkedRequisitions(PO_ID);
                List <Return_GetPOLinkedRequisitions_MainGrid> LinkRRMainData = LinkRRData.main_Register;
                List <Return_GetPOLinkedRequisitions_NestedGrid> LinkRRNestedData = LinkRRData.nested_Register;

                if (LinkRRData != null)
                {
                    //var LinkRRMainData = LinkRRData.main_Register;
                    //var LinkRRNestedData = LinkRRData.nested_Register;
                    Purchase_LinkRRMainData = LinkRRMainData;
                    Purchase_LinkRRNestedData = LinkRRNestedData;
                }
               
            }
            List<Return_GetPOLinkedRequisitions_MainGrid> MastergridRR_data = Purchase_LinkRRMainData as List<Return_GetPOLinkedRequisitions_MainGrid>;
            if (MastergridRR_data == null)
            {
                return PartialView();
            }
            return PartialView(MastergridRR_data);
        }

        //public ActionResult PO_LinkedRRNestedGridData(string ActionMethodName, int PO_ID = 0, int RRID = 0)
        //{

        //    //var LinkRRNestedList = new List<DbOperations.StockPurchaseOrder.Return_GetPOLinkedRequisitions_NestedGrid>();
        //    //if (ActionMethodName == "New")
        //    //{
        //    //    return PartialView(LinkRRNestedList);
        //    //}
        //    if (Purchase_LinkRRNestedData == null)
        //    {
        //        var LinkRRData = BASE._PO_DBOps.GetPOLinkedRequisitions(PO_ID);
        //        var LinkRRNestedData = LinkRRData.nested_Register;
        //        Purchase_LinkRRNestedData = LinkRRNestedData;

        //    }
        //    var data = Purchase_LinkRRNestedData as List<Return_GetPOLinkedRequisitions_NestedGrid>;
        //    List<Return_GetPOLinkedRequisitions_NestedGrid> itemrr = data.FindAll(x => x.RRID == RRID);
        //    return PartialView(itemrr);
        //}

       
        public ActionResult PO_LinkedRRNestedGridData( int RRID = 0, int PO_ID = 0)
        {
            PO_ID_Glob = PO_ID;
            ViewData["PO_ID"] = PO_ID_Glob;
            var LinkRRNestedList = new List<DbOperations.StockPurchaseOrder.Return_GetPOLinkedRequisitions_NestedGrid>();
           
            ViewData["RRID"] = RRID;
            if (Purchase_LinkRRNestedData == null)
            {
                var LinkRRData = BASE._PO_DBOps.GetPOLinkedRequisitions(PO_ID);
                var LinkRRNestedData = LinkRRData.nested_Register;
                Purchase_LinkRRNestedData = LinkRRNestedData;
                Session["Purchase_LinkRRNestedData"] = Purchase_LinkRRNestedData;
            }

            var data = Purchase_LinkRRNestedData as List<Return_GetPOLinkedRequisitions_NestedGrid>;
            List<Return_GetPOLinkedRequisitions_NestedGrid> itemrr = data.FindAll(x => x.RRID == RRID);
            return PartialView(itemrr);
        }

        public static GridViewSettings PurchaseRegisterRRNestedGridSettings(int RRID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PO_LinkedRRNestedGrid" + RRID;
            settings.SettingsDetail.MasterGridName = "PO_LinkedRRMainGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Sr");
            settings.Columns.Add("ItemName");
            settings.Columns.Add("ItemCode").Visible = true;
            settings.Columns.Add("Head").Visible = true;
            settings.Columns.Add("Make").Visible = true;
            settings.Columns.Add("Model").Visible = true;
            settings.Columns.Add("Requested_Qty").Visible = true;
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Dest_Location").Visible = true;

            settings.Columns.Add("Rate").Visible = true;
            settings.Columns.Add("Taxes").Visible = true;
            settings.Columns.Add("Discount").Visible = true;
            settings.Columns.Add("Amount").Visible = true;
            settings.Columns.Add("RRI_Priority").Visible = true;
            settings.Columns.Add("Req_Del_Date").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("RRID").Visible = false;
           // settings.ClientSideEvents.FocusedRowChanged = "OnPurchaseLinkedUserOrderNestedGridFocusedRowChanged";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            //settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for exporting nested grid 

        public static IEnumerable GetItemRR(int RRID)
        {
            List<Return_GetPOLinkedRequisitions_NestedGrid> data = (List<Return_GetPOLinkedRequisitions_NestedGrid>)System.Web.HttpContext.Current.Session["Purchase_LinkRRNestedData"];
            List<Return_GetPOLinkedRequisitions_NestedGrid> itemrrlist = data.FindAll(x => x.RRID == RRID);
            return itemrrlist;
        }//binding data to nested grid

        //public ActionResult Purchase_RR_Save()
        //{
        //    Model_NEVD_Requisition model = (Model_NEVD_Requisition)TempData["ModelData"];
        //    List<Return_Get_RR_Detail> gridRows = new List<Return_Get_RR_Detail>();

        //    var LastRowSr = 0;
        //    var NewSr = LastRowSr + 1;
        //    if (Purchase_LinkRRMainData != null)
        //    {
        //        gridRows = (List<Return_Get_RR_Detail>)Purchase_LinkRRMainData;

        //    }         
        //    return Json(new
        //    {
        //        result = true,
        //        message = "Saved Successfully"
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #endregion




        #endregion

        #region Price History Window

        public ActionResult LookUp_PriceHistoryItem(bool? IsVisible, DataSourceLoadOptions loadOptions , int? SupplierID)
        {
            List<Return_GetItemsPriceHistory> itemdata = BASE._PO_DBOps.GetItemsPriceHistory(ClientScreen.Stock_PO,SupplierID);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(itemdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_PriceHistorySuppplier(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            List<Return_Param_GetAllSuppliers> supplierdata = BASE._PO_DBOps.GetSupplier_PriceHistory();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(supplierdata, loadOptions)), "application/json");
        }

        public ActionResult Frm_PO_Price_History(int ItemID = 0, int? SupplierID = null)
        {
            PriceHistory model = new PriceHistory();

            if (ItemID != 0)
            {
                model.PH_Item_ID = ItemID;
            }

            if (SupplierID != null)
            {
                model.PH_Supplier = Convert.ToInt32(SupplierID);
            }


            return View(model);
        }

        public ActionResult Check_PriceHistory(int Item_ID, string Item_Code = null, int? SupplierID = null)

        {
            if (Item_ID == 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Item Name Can Not be Empty...!"
                }, JsonRequestBehavior.AllowGet);
            }
            Common_Lib.RealTimeService.Param_Get_PriceHistory Param = new Common_Lib.RealTimeService.Param_Get_PriceHistory();
            Param.SupplierID = object.ReferenceEquals(SupplierID, "") ? null : SupplierID;
            Param.ItemCode = object.ReferenceEquals(Item_Code, "") ? null : Item_Code;
            Param.ItemID = Item_ID;
           
          
            var PriceHistory = BASE._PO_DBOps.Get_PriceHistory(Param);


            PriceHistory_Grid_Data = PriceHistory;
            return PartialView("PriceHistoryGrid", PriceHistory);
        }
        public ActionResult PriceHistoryGrid()
        {


            return PartialView(PriceHistory_Grid_Data);


        }

        #endregion
        #region Purchase Register Bottom Buttons

        [HttpGet]
        public ActionResult PO_StatusChange_Window(int ID, string StatusButton)
        {
            POUpDateStatus model = new POUpDateStatus();
            model.PO_ID = ID;
            model.PO_StatusType = StatusButton;

            return View(model);
        }

        [HttpPost]
        public ActionResult PO_StatusChange_Window(POUpDateStatus model)
        {
            try
            {
                string msg = "";
                Param_UpdatePurchaseOrderStatus param = new Param_UpdatePurchaseOrderStatus();
                param.PO_ID = model.PO_ID;
                if (model.PO_StatusType == "Complete")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Completed", true);

                    msg = "Status Changed To Completed..!!";
                }

                else if (model.PO_StatusType == "Reopen")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Reopen", true);
                    msg = "Status Changed To 'Reopen' for selected PO..!!";
                }

                if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
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
                
                //else if (model.PO_StatusType == "Cancel")
                //{
                //    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Cancelled", true);
                //    msg = "Status Changed To Cancelled..!!";
                //}
                //else if (model.PO_StatusType == "Reject")
                //{
                //    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Rejected", true);
                //    msg = "Status Changed To Rejected..!!";
                //}
                //else
                //{
                //    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Re_Requisition_Requested", true);
                //    msg = "Status Changed To Re_Requisition_Requested...!!";
                //}
                //if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
                //{
                //    if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
                //    {
                //        return Json(new
                //        {
                //            result = true,
                //            message = msg
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        return Json(new
                //        {
                //            result = false,
                //            message = Messages.SomeError
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //}
                //else
                //{
                //    return Json(new
                //    {
                //        result = false,
                //        message = Messages.SomeError
                //    }, JsonRequestBehavior.AllowGet);
                //}
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

        public ActionResult PO_StatusChange_Check(int ID, string StatusButton)
        {
            
               string msg = "";
                //    var uocount = BASE._Jobs_Dbops.GetJob_UO_Count(JobId);
                //    var rrcount = BASE._Jobs_Dbops.GetJob_RR_Count(JobId);
                
                if (StatusButton == "Complete")
                {
                    var delicount = BASE._PO_DBOps.GetPOGoodsReceived_not_FullyReturned_Only(ID);
                    if (delicount == null)
                    {
                        msg = "A PO  without deliveries can not be marked as" + StatusButton + "ed " + "..!!";

                    }
                }

            if (StatusButton == "Reopen")
            {

                var comp = BASE._PO_DBOps.Get_PO_Job_Project_Completed(ID);
                var store = BASE._PO_DBOps.Get_PO_Related_ClosedDept_Count(ID);

                if (comp)
                {

                    msg = "PO against Completed Job/Project can not be Reopened ";

                }

                if (store != 0)
                {

                    msg = "PO involving closed Store/Dept  can not be Reopened / Edited";

                }
            }



                
                //    else if (uocount > 0)
                //    {
                //        msg = "A Job Cannot Be " + StatusButton + "ed " + "If UO Is Posted Against It..!!";
                //    }
                //    else if (rrcount > 0)
                //    {
                //        msg = "A Job Cannot Be " + StatusButton + "ed " + "If RR Is Posted Against It..!!";
                //    }
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

        public ActionResult PO_StatusChange(int ID, string StatusButton, string[] userRole)
        {
            try
            {
                var data = BASE._PO_DBOps.Get_PO_Detail(ID);
                var POStatus = data.PO_Status;               
               
                string msg = "";
                Param_UpdatePurchaseOrderStatus param = new Param_UpdatePurchaseOrderStatus();
                param.PO_ID = ID;
                param.Logged_In_User = BASE._open_User_ID;//Mantis bug 0000924 fixed

                if (StatusButton == "Re_Requisition")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Re_Requisition_Requested", true);
                    msg = "Status Changed To 'Assigned for Re Requisition'..!!";                    
                }
                else if (StatusButton == "Approve")
                {
                        param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Approved", true);
                        msg = "Status Changed To 'Approved'..!!";                    
                }
                else if (StatusButton == "Cancel")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Cancelled", true);
                    msg = "Status Changed To 'Cancelled' for selected PO..!!";                    
                }
                else if (StatusButton == "Reject")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Rejected", true);
                    msg = "Status Changed To 'Rejected' for selected PO..!!";                    
                }
                else if(StatusButton == "Complete")
                {
                    param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "Completed", true);
                    msg = "Status Changed To Completed..!!";
                }
                else if (StatusButton == "Reopen")
                {
                    if (POStatus == "Cancelled" )
                    {

                        param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "_New", true);                        
                        msg = "Status Changed To 'New'.!!";
                    }
                    if (POStatus == "Completed")
                    {

                        param.UpdatedStatus = (PO_Status)Enum.Parse(typeof(PO_Status), "In_Progress", true);                        
                        msg = "Status Changed To 'In_Progress'.!!";
                    }
                }
                if (BASE._PO_DBOps.UpdatePurchaseOrderStatus(param))
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

        public ActionResult PurchaseOrderStatusFlow(int ID, string StatusButton, string[] CurrUserRole)
        {
            var data = BASE._PO_DBOps.Get_PO_Detail(ID);
            var POStatus = data.PO_Status;
           
            string msg = "";
            if (StatusButton.ToUpper() == "DOCUMENTS")
            {
                if (POStatus == "Completed")
                {
                    msg = "You Are Not Allowed To Post Documents If PO Status IS " + POStatus + " ..!!";
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
            else if (StatusButton.ToUpper() == "COMPLETE")
            {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge") || CurrUserRole.Contains("RR_Requestor"))
                {
                    if (POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Are Not Allowed To Mark Purchase Order As Completed If Purchase Order Status is " + POStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only Purchaser, Purchasing Dept. Incharge, RR Requestor have rights To Mark Purchase Order As Completed...!!";
                }

            }
            else if (StatusButton.ToUpper() == "RE_REQUISITION") //0000157 bug fixed
             {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge"))
                {

                    if (POStatus == "New" || POStatus == "Approved" || POStatus == "Rejected")//0000157 bug fixed
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Are not Allowed To mark PO for RE-Requisition if PO Status Is " + POStatus + "...!!";
                    }

                }
                else
                {
                    msg = "Only Purchaser, Purchasing Dept. Incharge Have Rights To Mark Purchase Order for RE-Requisition...!!";
                }
            }
            
            else if (StatusButton.ToUpper() == "CANCEL")
            {
                if (CurrUserRole.Contains("RR_Requestor"))
                {
                    if (POStatus == "New" || POStatus == "Re-Requisition Requested" || POStatus == "Approved")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Requestor Is Not Allowed To Cancel Purchase Order If PO Status IS " + POStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only Requestor Have Rights To Cancel The Purchase Order...!!";
                }
            }
            else if (StatusButton.ToUpper() == "REOPEN")
            {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge") || CurrUserRole.Contains("RR_Approver"))
                {
                    if (POStatus == "Completed")
                    {

                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else

                    {
                        msg = "Purchaser, Purchasing Dept. Incharge, RR Approver cannot reopen PO in " + POStatus + " status !!";
                    }

                }

                if (CurrUserRole.Contains("RR_Requestor"))
                {
                    if (POStatus == "Cancelled")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Requestor Is Not Allowed To Re-open Purchase Order If Purchase Order Status IS " + POStatus + " ..!!";
                    }
                }

                else
                {
                    msg = "You do not have Rights To Re-open The PO..!!";
                }
            }
            else if (StatusButton.ToUpper() == "REJECT")
            {
                if (CurrUserRole.Contains("RR_Approver"))
                {
                    if (POStatus == "Completed" || POStatus == "Re-Requisition Requested")
                    {

                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "RR Approver Cannot Reject PO If PO Status Is " + POStatus + "...!!";
                    }
                }
               if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge"))
                {
                    if (POStatus == "New" || POStatus == "Approved" || POStatus == "Completed" || POStatus == "Re-Requisition Requested")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Purchaser, Purchasing Dept. Incharge Cannot Reject PO If PO Status Is " + POStatus + "...!!";
                    }
                }
                else
                {
                    msg = "You Don't Have Rights To Reject The PO...!!";
                }
            }

            else if (StatusButton.ToUpper() == "APPROVE")
            {
                if (CurrUserRole.Contains("RR_Approver"))
                {
                    if (POStatus == "Re-Requisition Requested")
                    {

                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "RR Approver Cannot Approve PO If PO Status Is " + POStatus + "...!!";
                    }
                }
               if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge"))
                {
                    if (POStatus == "New" || POStatus == "Rejected")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Purchaser, Purchasing Dept. Incharge Cannot Approve PO If PO Status Is " + POStatus + "...!!";
                    }
                }
                else
                {
                    msg = "You Don't Have Rights To Approve The PO...!!";
                }
            }

            else if (StatusButton.ToUpper() == "EDIT")
            {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge"))
                {
                    if (POStatus == "New" || POStatus == "Approved" || POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Purchaser, Purchasing Dept. Incharge cannot Edit Purchase Order If PO Status IS " + POStatus + " ..!!";
                    }
                }
                if (CurrUserRole.Contains("RR_Approver"))
                {
                    if (POStatus == "Re-Requisition Requested")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "RR Approver cannot Edit Purchase Order If PO Status IS " + POStatus + " ..!!";
                    }
                }//Mantis bug 0000808 fixed

                else
                {
                    msg = "You Don't Have Rights To Edit This Purchase Order..!!";
                }
            }
            else if (StatusButton.ToUpper() == "ADD-ITEMS" || StatusButton.ToUpper() == "UPDATE-ITEMS" || StatusButton.ToUpper() == "DELETE-ITEMS")
            {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge"))
                {
                    if (POStatus == "New" || POStatus == "Approved" || POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Purchaser, Purchasing Dept. Incharge Are Not Allowed To Add/Update/Delete Items If PO Status IS " + POStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only Purchaser, Purchasing Dept. Incharge Have Rights To Add/Update/Delete Items for this Purchase Order..!!";
                }

            }
            else if (StatusButton.ToUpper() == "POSTING-PAYMENTS")
            {
                if (CurrUserRole.Contains("Accts_Responsible"))
                {
                    if (POStatus == "Approved" || POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Accounts Responsible Personnel are not Allowed to Post Payments if PO Status is " + POStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only Accounts Responsible Personnel Have Rights for" + StatusButton + " ..!!";
                }
            }

            else if (StatusButton.ToUpper() == "LINKED UO - POST DELIVERY")
            {
                if (CurrUserRole.Contains("RR_Requestor"))
                {
                    if (POStatus == "Completed" || POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "RR Requestor is not Allowed to Post Delivery if PO Status is " + POStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only RR Requestor Have Rights for" + StatusButton + " ..!!";
                }
            }
            else if (StatusButton.ToUpper() == "POSTING DELIVERY RECEIVED" || StatusButton.ToUpper() == "POSTING DELIVERY RETURNED")
            {
                if (CurrUserRole.Contains("Purchaser") || CurrUserRole.Contains("Purchaser_Incharge") || CurrUserRole.Contains("RR_Requestor"))
                {
                    if (POStatus == "Approved" || POStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "Purchaser, Purchasing Dept. Incharge, RR Requestor are not Allowed to Post Delivery Returned/ Received If PO Status is " + POStatus + " ..!!";
                    }
                }


                else
                {
                    msg = "Only Purchaser, Purchasing Dept. Incharge, RR Requestor Have Rights for" + StatusButton + " ..!!";
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

        #endregion Purchase register Bottom Buttons
        #region "Misc Functions"
        public void Sessionclear()
        {
            Session.Remove("Purchase_LinkRRNestedData");
            Session.Remove("Purchase_LinkUserOrderNestedData");
            Session.Remove("ItemPurchase_ExportData");
            BASE._SessionDictionary.Remove("Purchase_Documents_Attachment_AttachmentData");
            BASE._SessionDictionary.Remove("PurchaseDocument_AttachmentData");
            ClearBaseSession("_PO");
        } //clears session variable on popup close

        public void InfoSessionclear()
        {
            ClearBaseSession("_POInfo");
        }

        public void POTaxGridSessionclear()
        {

            ClearBaseSession("_PO_Tax");
        } //clears session variable on popup close

        public void POHistorySessionclear()
        {
            ClearBaseSession("_POPopup");
        }
        public void PO_user_rights()
        {
            ViewData["PO_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_PO, "Update");
            ViewData["PO_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_PO, "View");
            ViewData["PO_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_PO, "Export");
            ViewData["PO_AccResponsibleRight"] = CheckRights(BASE, ClientScreen.Stock_PO, "Accounts Responsible");            
            ViewData["PO_ViewRRRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "View");
            ViewData["PO_AddAccVouPaymentRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
        }        
        #endregion
    }
}