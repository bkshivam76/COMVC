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
using Microsoft.Ajax.Utilities;
using static Common_Lib.DbOperations;
using static Common_Lib.Common;
using static Common_Lib.DbOperations.StockUserOrder;
using static Common_Lib.DbOperations.StockProfile;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class UserOrderController : BaseController
    {
        // GET: Stock/UserOrder
        #region Global variables
        public string UO_RequisitionID
        {
            get
            {
                return (string)GetBaseSession("UO_RequisitionID_UO");
            }
            set
            {
                SetBaseSession("UO_RequisitionID_UO", value);
            }
        }
        public List<DbOperations.StockUserOrder.Return_GetRegister_MainGrid> UserOrder_Data_Glob
        {
            get
            {
                return (List<DbOperations.StockUserOrder.Return_GetRegister_MainGrid>)GetBaseSession("UserOrder_Data_Glob_InfoUO");
            }
            set
            {
                SetBaseSession("UserOrder_Data_Glob_InfoUO", value);
            }
        }
        public DateTime? UOFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("UOFromDate_InfoUO");
            }
            set
            {
                SetBaseSession("UOFromDate_InfoUO", value);
            }
        }
        public DateTime? UOToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("UOToDate_InfoUO");
            }
            set
            {
                SetBaseSession("UOToDate_InfoUO", value);
            }
        }
        public List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid> ItemUser_ExportData
        {
            get
            {
                return (List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid>)GetBaseSession("ItemUser_ExportData_InfoUO");
            }
            set
            {
                SetBaseSession("ItemUser_ExportData_InfoUO", value);
            }
        }
        public int UOID_Glob
        {
            get
            {
                return (int)GetBaseSession("UOID_Glob_UO");
            }
            set
            {
                SetBaseSession("UOID_Glob_UO", value);
            }
        }
        public List<Return_GetUO_Items> UO_ItemOrderedData
        {
            get
            {
                return (List<Return_GetUO_Items>)GetBaseSession("UO_ItemOrderedData_UO");
            }
            set
            {
                SetBaseSession("UO_ItemOrderedData_UO", value);
            }
        }
        public List<Return_GetDocumentsGridData> UserOrder_Documents_Window_Grid_Data
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("UserOrder_Documents_Window_Grid_Data_UO");
            }
            set
            {
                SetBaseSession("UserOrder_Documents_Window_Grid_Data_UO", value);
            }
        }
        public ArrayList UserOrderEdit_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UserOrderEdit_Document_ID_UO");
            }
            set
            {
                SetBaseSession("UserOrderEdit_Document_ID_UO", value);
            }
        }
        public ArrayList UserOrderDelete_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UserOrderDelete_Document_ID_UO");
            }
            set
            {
                SetBaseSession("UserOrderDelete_Document_ID_UO", value);
            }
        }
        public ArrayList UO_Unlink_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UO_Unlink_Document_ID_UO");
            }
            set
            {
                SetBaseSession("UO_Unlink_Document_ID_UO", value);
            }
        }
        public ArrayList UOEdit_ItemOrd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_ItemOrd_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_ItemOrd_ID_UO", value);
            }
        }
        public ArrayList Delete_User_OrderItem_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_OrderItem_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_OrderItem_ID_UO", value);
            }
        }
        public List<Return_GetUOGoodsDelivered_MainGrid> UO_GoodsDeliveredMainData
        {
            get
            {
                return (List<Return_GetUOGoodsDelivered_MainGrid>)GetBaseSession("UO_GoodsDeliveredMainData_UO");
            }
            set
            {
                SetBaseSession("UO_GoodsDeliveredMainData_UO", value);
            }
        }
        public List<Return_GetUOGoodsDelivered_NestedGrid> UO_GoodsDeliveredNestedData
        {
            get
            {
                return (List<Return_GetUOGoodsDelivered_NestedGrid>)GetBaseSession("UO_GoodsDeliveredNestedData_UO");
            }
            set
            {
                SetBaseSession("UO_GoodsDeliveredNestedData_UO", value);
            }
        }
        public ArrayList UOEdit_GoodsDelivered_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_GoodsDelivered_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_GoodsDelivered_ID_UO", value);
            }
        }
        public ArrayList Delete_User_GoodsDelivered_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_GoodsDelivered_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_GoodsDelivered_ID_UO", value);
            }
        }
        public List<Return_GetUOGoodsReceived> UO_GoodsRecdData
        {
            get
            {
                return (List<Return_GetUOGoodsReceived>)GetBaseSession("UO_GoodsRecdData_UO");
            }
            set
            {
                SetBaseSession("UO_GoodsRecdData_UO", value);
            }
        }
        public ArrayList UOEdit_GoodsRec_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_GoodsRec_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_GoodsRec_ID_UO", value);
            }
        }
        public ArrayList Delete_User_GoodsRecd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_GoodsRecd_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_GoodsRecd_ID_UO", value);
            }
        }
        public List<Return_GetUOGoodsReturned> UO_GoodsRetnData
        {
            get
            {
                return (List<Return_GetUOGoodsReturned>)GetBaseSession("UO_GoodsRetnData_UO");
            }
            set
            {
                SetBaseSession("UO_GoodsRetnData_UO", value);
            }
        }
        public ArrayList UOEdit_GoodsRet_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_GoodsRet_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_GoodsRet_ID_UO", value);
            }
        }
        public ArrayList Delete_User_GoodsRetn_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_GoodsRetn_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_GoodsRetn_ID_UO", value);
            }
        }
        public List<Return_GetUOGoodsReturnReceived> UO_GoodsRetRecdData
        {
            get
            {
                return (List<Return_GetUOGoodsReturnReceived>)GetBaseSession("UO_GoodsRetRecdData_UO");
            }
            set
            {
                SetBaseSession("UO_GoodsRetRecdData_UO", value);
            }
        }
        public ArrayList UOEdit_GoodsRetRec_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_GoodsRetRec_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_GoodsRetRec_ID_UO", value);
            }
        }
        public ArrayList Delete_User_GoodsRetRecd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_GoodsRetRecd_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_GoodsRetRecd_ID_UO", value);
            }
        }
        public List<Return_GetUOScrapCreated> UO_ScrapData
        {
            get
            {
                return (List<Return_GetUOScrapCreated>)GetBaseSession("UO_ScrapData_UO");
            }
            set
            {
                SetBaseSession("UO_ScrapData_UO", value);
            }
        }
        public ArrayList UOEdit_Scrap_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("UOEdit_Scrap_ID_UO");
            }
            set
            {
                SetBaseSession("UOEdit_Scrap_ID_UO", value);
            }
        }
        public ArrayList Delete_User_Scrap_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_User_Scrap_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_User_Scrap_ID_UO", value);
            }
        }
        public ArrayList Delete_UOExisting_Remarks_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_UOExisting_Remarks_ID_UO");
            }
            set
            {
                SetBaseSession("Delete_UOExisting_Remarks_ID_UO", value);
            }
        }
        public List<Return_Get_UO_Goods_Delivery_Stocks> UODeliveredStockGridData
        {
            get
            {
                return (List<Return_Get_UO_Goods_Delivery_Stocks>)GetBaseSession("UODeliveredStockGridData_UO_Stock");
            }
            set
            {
                SetBaseSession("UODeliveredStockGridData_UO_Stock", value);
            }
        }
        public ArrayList DeletedItemRequested_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedItemRequested_ID_UO");
            }
            set
            {
                SetBaseSession("DeletedItemRequested_ID_UO", value);
            }
        }
        public ArrayList DeletedUDS_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedUDS_ID_UO");
            }
            set
            {
                SetBaseSession("DeletedUDS_ID_UO", value);
            }
        }
        public ArrayList DeletedRecd_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedRecd_ID_UO");
            }
            set
            {
                SetBaseSession("DeletedRecd_ID_UO", value);
            }
        }
        public ArrayList DeletedUDSRetRec_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("DeletedUDSRetRec_ID_UO");
            }
            set
            {
                SetBaseSession("DeletedUDSRetRec_ID_UO", value);
            }
        }
        public List<Return_GetUORemarks> UO_ExisitngRemarksData
        {
            get
            {
                return (List<Return_GetUORemarks>)GetBaseSession("UO_ExisitngRemarksData_UO");
            }
            set
            {
                SetBaseSession("UO_ExisitngRemarksData_UO", value);
            }
        }
        public List<Return_Get_RR_Details_ForUOmapping> UO_MapUserOrderData
        {
            get
            {
                return (List<Return_Get_RR_Details_ForUOmapping>)GetBaseSession("UO_MapUserOrderData_UO");
            }
            set
            {
                SetBaseSession("UO_MapUserOrderData_UO", value);
            }
        }
        public List<DbOperations.StockUserOrder.Return_Get_Stock_Availability> Avl_Stock_Item_Grid_Data
        {
            get
            {
                return (List<DbOperations.StockUserOrder.Return_Get_Stock_Availability>)GetBaseSession("Avl_Stock_Item_Grid_Data_UO");
            }
            set
            {
                SetBaseSession("Avl_Stock_Item_Grid_Data_UO", value);
            }
        }




        #endregion
        #region Grid/Nested Grid
        public ActionResult Frm_User_Order_Info(string PopupID = "", string RequisitionID = "")
        {
            ViewBag.UO_PopupID = PopupID;
            UO_RequisitionID = RequisitionID;
            ViewData["UO_RequisitionID"] = UO_RequisitionID;
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_UO, "List"))
            {
                String PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;
                UO_Basic_Info();
                UO_user_rights();
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_UO').hide();</script>");
            }
        }
        public void UO_Basic_Info()
        {
            ViewData["UserOrderExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["UserOrderExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["UserOrderExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
        }

        public ActionResult Frm_User_Order_Main_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewData["UO_RequisitionID"] = UO_RequisitionID;
            UO_Basic_Info();
            UO_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (UserOrder_Data_Glob == null || command == "REFRESH")
            {
                var UserOrder_Data = BASE._user_order_DBOps.GetRegister(Convert.ToDateTime(UOFromDate), Convert.ToDateTime(UOToDate));

                if (UserOrder_Data != null)
                {
                    var Mastergrid = UserOrder_Data.main_Register;
                    var Nestedgrid = UserOrder_Data.nested_Register;
                    UserOrder_Data_Glob = Mastergrid;
                    ItemUser_ExportData = Nestedgrid;
                    Session["ItemUser_ExportData"] = ItemUser_ExportData;
                }
            }
            List<DbOperations.StockUserOrder.Return_GetRegister_MainGrid> Mastergrid_data = UserOrder_Data_Glob as List<DbOperations.StockUserOrder.Return_GetRegister_MainGrid>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }

        public PartialViewResult Frm_User_Order_Nested_Grid(int UOID, string command)
        {
            UOID_Glob = UOID;
            ViewData["UOID"] = UOID_Glob;
            if (ItemUser_ExportData == null || command == "REFRESH")
            {
                var UserOrder_Data = BASE._user_order_DBOps.GetRegister(Convert.ToDateTime(UOFromDate), Convert.ToDateTime(UOToDate));
                var ItemUser_Data = UserOrder_Data.nested_Register;
                ItemUser_ExportData = ItemUser_Data;
            }
            var data = ItemUser_ExportData as List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid>;
            List<Return_GetRegister_NestedGrid> itemuser = data.FindAll(x => x.UOID == UOID);
            return PartialView(itemuser);
        }

        public ActionResult UserOrderCustomDataAction(int key = 0)
        {
            var Data = UserOrder_Data_Glob as List<DbOperations.StockUserOrder.Return_GetRegister_MainGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.UO_number + "![" + it.ID + "![" + it.UO_Date + "![" + it.UO_Status + "![" + it.Delivery_Status + "![" + it.Edit_Date + "![" + it.Edit_By + "![" +
                           it.Add_Date + "![" + it.Add_by + "![" + it.CurrUserRole + "![" + it.Approval_Required + "![" + it.Requestee_Store + "![" + it.Complex + "![" + it.Job + "![" + it.Project + "!["
                           + it.Initiation_Mode + "![" + it.Requestor_Name + "![" + it.Req_MainDeptID + "![" + it.StoreID + "![" + it.JobID + "![" + it.UORR_Mapped + "![" + it.ProjID + "![" + it.Req_SubDeptID;

                    ViewData["currentuserrole"] = it.CurrUserRole;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        public ActionResult ItemUserorderCustomDataAction(int key = 0)
        {
            var Data = ItemUser_ExportData as List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Item_Name + "![" + it.Make + "#," + it.Model + "![" + it.Serial_No_Lot_No + "![" + it.Unit + "![" + it.Requested_Qty + "![" + it.Approved_Qty
                        + "![" + it.Delivered_Qty + "![" + it.Returned_Qty + "![" + it.Penalty_Charged + "![" + it.Shipping_Location + "![" + it.Add_by + "![" + it.Add_Date + "![" +
                        it.Edit_By + "![" + it.Edit_Date + "![" + it.ID + "![" + it.UOID + "![" + it.SubItemID;
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
        public static GridViewSettings CreateGeneralDetailGridSettings(int UOID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "UserOrderRegister" + UOID;
            settings.SettingsDetail.MasterGridName = "UserOrderListGrid";
            settings.KeyFieldName = "ID";
            //settings.KeyFieldName = "pKey";
            settings.Columns.Add("Item_Name").Visible = true;
            settings.Columns.Add("Make").Visible = true;
            settings.Columns.Add("Model").Visible = true;
            settings.Columns.Add("Serial_No_Lot_No").Visible = true;
            settings.Columns.Add("Unit").Visible = true;
            settings.Columns.Add("Requested_Qty").Visible = true;
            settings.Columns.Add("Approved_Qty").Visible = true;
            settings.Columns.Add("Delivered_Qty").Visible = true;
            settings.Columns.Add("Returned_Qty").Visible = true;
            settings.Columns.Add("Penalty_Charged").Visible = true;
            settings.Columns.Add("Shipping_Location").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("UOID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "OnItemUserOrderFocusedRowChange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;

            return settings;
        }// setting for exporting nestedgrid
        public static IEnumerable GetItemUser(int UOID)
        {

            List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid> data = (List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["ItemUser_ExportData"];
            List<DbOperations.StockUserOrder.Return_GetRegister_NestedGrid> itemuserlist = data.FindAll(x => x.UOID == UOID);
            return itemuserlist;
        }//binding data to nestedgrid

        #endregion Grid/Nested Grid

        #region Period Selection
        public ActionResult LookUp_Get_ViewType_List_UO(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {

            var bankdata = new List<SelectListItem>()
                /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/;
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
        public ActionResult LookUp_ViewType_ChangeEvent_UO(string Chaval)
        {
            UserOrder_Period model = GetPeriod(Chaval);
            UOFromDate = model.UO_Fromdate;
            UOToDate = model.UO_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public UserOrder_Period GetPeriod(string Chaval)
        {
            UserOrder_Period model = new UserOrder_Period();
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
            model.UO_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.UO_Fromdate = xFr_Date;
            model.UO_Todate = xTo_Date;
            return model;
        }
        [HttpGet]
        public ActionResult Frm_Change_Period_Screen_UO()
        {
            UserOrder_Period model = new UserOrder_Period();
            model.UO_PeriodSelection = "Specific Period";
            model.UO_Todate = (DateTime)UOToDate;
            model.UO_Fromdate = (DateTime)UOFromDate;
            model.UO_BE_View_Period = "";
            model.UO_OpenDate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.UO_CloseDate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen_UO(UserOrder_Period model)
        {
            if (model.UO_Todate < model.UO_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                UOToDate = model.UO_Todate;
                UOFromDate = model.UO_Fromdate;
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

        #endregion Period Selection

        public JsonResult Check_Unmapped_UO_Items_for_CreatingRR(int UO_ID = 0)
        {
            Param_Get_UOItem_ForRR inparam = new Param_Get_UOItem_ForRR();

            inparam.UO_ID = UO_ID;

            var rr_Items = BASE._user_order_DBOps.Get_UO_Items_Detail_For_RR(inparam);

            List<Return_Get_UO_Item_Details_ForNewRR> RequestedItemDataUO = rr_Items;

            if (RequestedItemDataUO.Count() == 0)
            {

                return Json(new
                {
                    result = false,
                    message = "RR is already created for this UO."
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


       
        #region NEVD
    public ActionResult DataNavigation(string ActionMethod, int ID, DateTime UO_Date)
        {


            var UO_Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
            var UO_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
            if (BASE._open_User_Type == "CLIENT ROLE")
            {
                if (UO_Audited_Date != null)
                {
                    if (UO_Audited_Date.Rows.Count > 0)
                    {
                        if (UO_Date >= Convert.ToDateTime(UO_Audited_Date.Rows[0]["FROMDATE"]) && UO_Date <= Convert.ToDateTime(UO_Audited_Date.Rows[0]["TODATE"]))
                        {

                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Audited Period.User Order Date should not be in Audited period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (UO_Submitted_Date != null)
                {
                    if (UO_Submitted_Date.Rows.Count > 0)
                    {

                        if (UO_Date >= Convert.ToDateTime(UO_Submitted_Date.Rows[0]["FROMDATE"]) && UO_Date <= Convert.ToDateTime(UO_Submitted_Date.Rows[0]["TODATE"]))
                        {

                            return Json(new
                            {
                                Message = "No Changes Are Allowed In Accounts Submitted Period.User Order Date Should Not Be In Account Submission Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }


            //if (ActionMethod == "New" || ActionMethod == "Delete")
            //{
            //    DataTable dtnew = BASE._user_order_DBOps.GetRecord(ID);
            //    if (dtedit.Rows[0]["UO_Status"].ToString() == "Completed" || dtedit.Rows[0]["UO_Status"].ToString() == "Rejected" || dtedit.Rows[0]["UO_Status"].ToString() == "Cancelled")
            //    {
            //        return Json(new { result = false, Message = "Completed/Rejected/Cancelled User Orders can not be Edited ..." }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            if (ActionMethod == "Delete")

            {

                int UO_Pending = BASE._user_order_DBOps.GetUO_RR_Count(ID);
                if (UO_Pending > 0)
                {
                    return Json(new { result = false, Message = "User Orders against which Requisition Requests/Transfer Orders have been posted, cannot be deleted..." }, JsonRequestBehavior.AllowGet);
                }

                DataTable UO_dt = BASE._user_order_DBOps.GetRecord(ID);
                if (UO_dt.Rows[0]["UO_Status"].ToString() != "New")
                {
                    return Json(new { result = false, Message = "User orders whose status is not “New” can not be deleted ..." }, JsonRequestBehavior.AllowGet);
                }

                int UO_Depend = BASE._user_order_DBOps.GetUO_DependentEntry_Count(ID);
                if (UO_Depend > 0)
                {
                    return Json(new { result = false, Message = "User Order against which Deliveries/Returns have been posted can not be deleted..." }, JsonRequestBehavior.AllowGet);
                }

                int UO_Scrap = BASE._user_order_DBOps.GetUO_Scrap_Creation_Count(ID);
                if (UO_Scrap > 0)
                {
                    return Json(new { result = false, Message = "User Order against which Scrap has been created, can not be deleted..." }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt1 = BASE._user_order_DBOps.GetRecord(ID);
                if (dt1.Rows[0]["UO_Status"].ToString() == "Completed" || dt1.Rows[0]["UO_Status"].ToString() == "Rejected" || dt1.Rows[0]["UO_Status"].ToString() == "Cancelled")
                {
                    return Json(new { result = false, Message = "Completed/Rejected/Cancelled User Orders can not be Deleted ..." }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "Cancel" || ActionMethod == "Reject")
            {
                int Count_Pending = BASE._user_order_DBOps.GetUO_RR_Count(ID);
                if (Count_Pending > 0)
                {
                    return Json(new { result = false, Message = "User Order against which Requisition Requests/ Transfer Orders have been posted, cannot be " + ActionMethod + "ed" + "..." }, JsonRequestBehavior.AllowGet);
                }
            }

            if (ActionMethod == "Complete")
            {
                int CountDeliveries = BASE._user_order_DBOps.GetUO_Deliveries_notReturned_Count(ID);
                if (CountDeliveries == 0)
                {
                    return Json(new { result = false, Message = "User Order which does not contain any delivery or contains full Returns, cannot be marked as completed..." }, JsonRequestBehavior.AllowGet);
                }

                int Counttransit = BASE._user_order_DBOps.GetUO_Goods_In_Transit_Count(ID);
                if (Counttransit != 0)
                {
                    return Json(new
                    {
                        result = false,
                        Message = "User Order cannot be marked as completed, if there are some goods in transit..."
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "Edit")
            {
                DataTable dtedit = BASE._user_order_DBOps.GetRecord(ID);
                if (dtedit.Rows[0]["UO_Status"].ToString() == "Completed" || dtedit.Rows[0]["UO_Status"].ToString() == "Rejected" || dtedit.Rows[0]["UO_Status"].ToString() == "Cancelled")
                {
                    return Json(new { result = false, Message = "Completed/Rejected/Cancelled User Orders can not be Edited ..." }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "MapUO")
            {
                Param_Get_Approval_Required param = new Param_Get_Approval_Required();

                var approval = BASE._StockApprovalReqd_dbops.Get_Approval_Required(param, ClientScreen.Stock_UO);
                var approvalvalue = "No";
                if (approval == false)
                {
                    approvalvalue = "No";
                }
                else
                {
                    approvalvalue = "Yes";
                }
                DataTable dtedit = BASE._user_order_DBOps.GetRecord(ID);
                if (!(dtedit.Rows[0]["UO_Status"].ToString() == "Approved" || dtedit.Rows[0]["UO_Status"].ToString() == "Progress"))
                {
                    if (approvalvalue == "Yes")
                    {

                        return Json(new { result = false, Message = "Only “Approved”/”In-Progress” Status UO can be merged into a Existing RR or UOs that do not require Approval  ..." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (ActionMethod == "UnmapUO")
            {
                var RRStatus = BASE._user_order_DBOps.GetUO_RR_Status(ID);
                if (RRStatus != "New")
                {
                    return Json(new { result = false, Message = "UO can not be unmapped from RR if the mapped RR is not in “New” Status..." }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_NEVD_User_Order(string ActionMethod = null, int ID = 0, string PostSuccessFunction = null, string PopUpID = "Dynamic_Content_popup", string CallingScreen = "")
        {
            UO_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_UO, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopUpID + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }

            UOSessionclear();
            NEVD_UserOrder model = new NEVD_UserOrder();

            model.UO_PostSuccessFunction = PostSuccessFunction == null ? "Frm_NEVD_User_Order_OnSuccess" : PostSuccessFunction;
            model.UO_PopUPId = PopUpID == null ? "Dynamic_Content_popup" : PopUpID;
            if (ID != 0 && ActionMethod != "View")//Mantis bug 0000114 fixed
            {

                var Role = ((List<Return_GetRegister_MainGrid>)UserOrder_Data_Glob).Where(x => x.ID == ID).FirstOrDefault().CurrUserRole;

                string[] CurrUserRole = (Role != null && Role.Length > 0) ? Role.Split(',').Select(x => x.Trim()).ToArray() : null;


                if (CurrUserRole != null)
                {
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        model.is_Requestor = true;
                    }
                    if (CurrUserRole.Contains("Requestor Incharge"))
                    {
                        model.is_RequestorIncharge = true;
                    }
                    if (CurrUserRole.Contains("Store Keeper"))
                    {
                        model.is_Store_Keeper = true;
                    }
                    if (CurrUserRole.Contains("Store Keeper in-charge"))
                    {
                        model.is_Store_KeeperIncharge = true;
                    }
                }
            }
            model.UO_ActionMethod = ActionMethod;
            model.UOID = ID;

            model.UO_openuserID = BASE._open_User_ID;
            model.UO_Curr_User_Personnel_ID = BASE._open_User_PersonnelID;

            model.UO_Curr_User_Store_ID = BASE._open_User_SubDeptID;
            model.UO_Curr_User_Dept_ID = BASE._open_User_MainDeptID;


            //model.is_Approver = CheckApproverRights() ? 1 : 0;            


            Param_Get_Approval_Required param = new Param_Get_Approval_Required();

            var approval = BASE._StockApprovalReqd_dbops.Get_Approval_Required(param, ClientScreen.Stock_UO);
            var approvalvalue = "NO";
            if (approval == false)
            {
                approvalvalue = "NO";
            }
            else
            {
                approvalvalue = "YES";
            }
            if (ActionMethod == "New")
            {
                model.UOApproval_Required = approvalvalue;
                model.UO_Initiate_Mode = "Connectone";
                model.Uo_Status = "New";
                model.User_Order_Date = DateTime.Now;
                model.UORequest_Name_Id = BASE._open_User_PersonnelID;
                //model.UORequestor_Department_Name_Id = BASE._open_User_MainDeptID;

                //if (BASE._open_User_MainDeptID == BASE._open_User_SubDeptID)
                //{
                //    model.UORequestor_Sub_Department_Name_Id = null;
                //}
                //else
                //{
                //    model.UORequestor_Sub_Department_Name_Id = BASE._open_User_SubDeptID;
                //}


            }


            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                var selectedrow = BASE._user_order_DBOps.GetUODetails(ID);
                if (selectedrow != null)
                {
                    UOID_Glob = ID;
                    //  Session["UORequest_Store_Id"] = selectedrow.StoreID;
                    //  Session["UORequestor_Department_Name_Id"] = selectedrow.Req_MainDeptID;
                    model.UO_number = selectedrow.UO_number;
                    model.User_Order_Date = selectedrow.UO_Date;
                    model.UO_Initiate_Mode = selectedrow.InitiationMode;
                    model.UOJob_Name_Id = selectedrow.JobID;
                    model.UORequest_Store_Id = selectedrow.StoreID;
                    model.UORequest_Name_Id = selectedrow.RequestorID;
                    //model.is_Requestor = selectedrow.is
                    model.UORequestor_Department_Name_Id = selectedrow.Req_MainDeptID;
                    model.UORequestor_Sub_Department_Name_Id = selectedrow.Req_SubDeptID;
                    model.UOApproval_Required = approvalvalue.ToString();
                    model.UORR_Mapped = selectedrow.UORR_Mapped;
                    // model. = selectedrow.ReceivalStatus;
                    model.Uo_Status = selectedrow.UO_Status;
                    model.UOID = ID;
                    model.uoprojectid = selectedrow.ProjID;

                }

            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_NEVD_User_Order(NEVD_UserOrder model)
        {
            try
            {
                string actionmethod = model.UO_ActionMethod;
                #region "Checking Restriction"

                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    if (model.User_Order_Date < BASE._open_Year_Sdt || model.User_Order_Date > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            message = "User Order Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var UO_Audited_Date = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var UO_Submitted_Date = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();

                    if (BASE._open_User_Type == "CLIENT ROLE")
                    {
                        if (UO_Audited_Date != null)
                        {
                            if (UO_Audited_Date.Rows.Count > 0)
                            {
                                if (model.User_Order_Date >= Convert.ToDateTime(UO_Audited_Date.Rows[0]["FROMDATE"]) && model.User_Order_Date <= Convert.ToDateTime(UO_Audited_Date.Rows[0]["TODATE"]))
                                {

                                    return Json(new
                                    {
                                        message = "No Changes Are Allowed In Audited Period.User Order Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (UO_Submitted_Date != null)
                        {
                            if (UO_Submitted_Date.Rows.Count > 0)
                            {
                                if (model.User_Order_Date >= Convert.ToDateTime(UO_Submitted_Date.Rows[0]["FROMDATE"]) && model.User_Order_Date <= Convert.ToDateTime(UO_Submitted_Date.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "No Changes Are Allowed In Accounts Submitted Period.User Order Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }

                    if (UO_ItemOrderedData == null || ((List<Return_GetUO_Items>)UO_ItemOrderedData).Count == 0)
                    {
                        return Json(new
                        {
                            message = "User Order should have atleast one Item Order, Please add requested item in item grid..!!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                #endregion
                if (actionmethod == "New")
                {
                    Param_Insert_UO_Txn param_Insert_UO_Txn = new Param_Insert_UO_Txn();

                    param_Insert_UO_Txn.UO_Initiated_Mode = model.UO_Initiate_Mode;
                    param_Insert_UO_Txn.UO_Date = model.User_Order_Date;
                    param_Insert_UO_Txn.UO_Job_ID = Convert.ToInt32(model.UOJob_Name_Id);
                    param_Insert_UO_Txn.UO_store_ID = Convert.ToInt32(model.UORequest_Store_Id);
                    param_Insert_UO_Txn.UO_Requestor_ID = Convert.ToInt32(model.UORequest_Name_Id);
                    param_Insert_UO_Txn.UO_Requestor_Main_Dept_Id = model.UORequestor_Department_Name_Id;
                    param_Insert_UO_Txn.UO_Requestor_Sub_Dept_Id = model.UORequestor_Sub_Department_Name_Id;
                    param_Insert_UO_Txn.Remarks = model.Uo_Remarks;


                    //Order Item

                    var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
                    if (all_data_Of_OrderItem_Grid != null)
                    {
                        var InsertOrderItemData = new Param_Insert_UO_Item[all_data_Of_OrderItem_Grid.Count()];
                        for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                        {
                            if (all_data_Of_OrderItem_Grid[I].ID == 0)
                            {
                                var InOrderItemInfo = new Param_Insert_UO_Item();

                                InOrderItemInfo.UO_ID = model.UOID;
                                InOrderItemInfo.Sub_Item_ID = all_data_Of_OrderItem_Grid[I].SubItemID;
                                InOrderItemInfo.Make = all_data_Of_OrderItem_Grid[I].Make;
                                InOrderItemInfo.Model = all_data_Of_OrderItem_Grid[I].Model;
                                InOrderItemInfo.Unit_ID = all_data_Of_OrderItem_Grid[I].UnitID;
                                InOrderItemInfo.Requested_Qty = all_data_Of_OrderItem_Grid[I].Requested_Qty;
                                InOrderItemInfo.Required_Date = all_data_Of_OrderItem_Grid[I].Requested_Delivery_Date;
                                InOrderItemInfo.Scheduled_Delivery_Date = all_data_Of_OrderItem_Grid[I].Scheduled_Delivery_Date;
                                InOrderItemInfo.Priority = all_data_Of_OrderItem_Grid[I].UOI_Priority;
                                InOrderItemInfo.Part_Delivery_Allowed = all_data_Of_OrderItem_Grid[I].Partial_Delivery_Allowed;
                                InOrderItemInfo.Remarks = all_data_Of_OrderItem_Grid[I].Remarks;
                                InOrderItemInfo.Delivery_Location_Id = all_data_Of_OrderItem_Grid[I].Delivery_Location_ID;
                                InOrderItemInfo.Approved_Qty = all_data_Of_OrderItem_Grid[I].Approved_Qty;
                                InsertOrderItemData[I] = InOrderItemInfo;
                            }
                        }
                        param_Insert_UO_Txn.InUOItems = InsertOrderItemData;
                    }

                    //Document Attachment 
                    var all_data_Of_DocAttach_Grid = (List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data;
                    if (all_data_Of_DocAttach_Grid != null)
                    {
                        var InsertDocAttachData = new Common_Lib.RealTimeService.Parameter_Insert_Attachment[all_data_Of_DocAttach_Grid.Count()];
                        for (int I = 0; I <= all_data_Of_DocAttach_Grid.Count() - 1; I++)
                        {
                            if (all_data_Of_DocAttach_Grid[I].ID == null)
                            {
                                var InDocAttachInfo = new Common_Lib.RealTimeService.Parameter_Insert_Attachment();
                                InDocAttachInfo.FileName = all_data_Of_DocAttach_Grid[I].File_Name;
                                InDocAttachInfo.Description = all_data_Of_DocAttach_Grid[I].Remarks;
                                InDocAttachInfo.NameID = all_data_Of_DocAttach_Grid[I].Document_Name_ID;
                                InDocAttachInfo.Ref_Screen = "UO";
                                InDocAttachInfo.Ref_Rec_ID = model.UOID.ToString();
                                InDocAttachInfo.RecID = System.Guid.NewGuid().ToString();
                                InDocAttachInfo.File = all_data_Of_DocAttach_Grid[I].File_Array;
                                InDocAttachInfo.Applicable_From = Convert.ToDateTime(all_data_Of_DocAttach_Grid[I].Applicable_From);
                                InDocAttachInfo.Applicable_To = Convert.ToDateTime(all_data_Of_DocAttach_Grid[I].Applicable_To);
                                InsertDocAttachData[I] = InDocAttachInfo;
                            }
                        }
                        param_Insert_UO_Txn.InAttachment = InsertDocAttachData;
                    }



                    if (BASE._user_order_DBOps.InsertUO(param_Insert_UO_Txn))
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
                    Param_Update_UO_Txn param_Update_UO_Txn = new Param_Update_UO_Txn();

                    EditIUDDocuments(ref param_Update_UO_Txn, model);
                    EditIUDItemOrdered(ref param_Update_UO_Txn, model);

                    EditIUDGoodsDelivery(ref param_Update_UO_Txn, model);
                    EditIUDGoodsReceived(ref param_Update_UO_Txn, model);
                    EditIUDGoodsReturned(ref param_Update_UO_Txn, model);
                    EditIUDGoodsReturnedReceived(ref param_Update_UO_Txn, model);
                    EditIUDScrap(ref param_Update_UO_Txn, model);
                    EditIUDRemainingData(ref param_Update_UO_Txn, model);

                    if (BASE._user_order_DBOps.UpdateUO(param_Update_UO_Txn))
                    {

                        if (!string.IsNullOrEmpty(model.UO_StatusButtonClick))
                        {
                            string msg = "";
                            Param_Update_UO_Status param = new Param_Update_UO_Status();
                            param.UOID = model.UOID;

                            var _CurrUserRole = BASE._user_order_DBOps.GetRegister(Convert.ToDateTime(UOFromDate), Convert.ToDateTime(UOToDate)).main_Register.FindAll(x => x.ID == model.UOID).FirstOrDefault().CurrUserRole;
                            string[] CurrUserRole = new string[] { "" };
                            if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }

                            var data = BASE._user_order_DBOps.GetUODetails(model.UOID);
                            var Status = data.UO_Status;
                            switch (model.UO_StatusButtonClick)
                            {

                                case "Reject":
                                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Rejected", true);
                                    msg = "Status Changed To Rejected..!!";
                                    break;

                                case "Changes Recommended":
                                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Changes_Recommended", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Changes Recommended...!!";
                                    break;

                                case "Cancel":
                                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Cancelled", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Cancelled..!!";
                                    break;

                                case "Reopen":

                                    if (CurrUserRole.Contains("Requestor"))
                                    {
                                        if (model.UOApproval_Required == "No")
                                        {
                                            param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Requested ", true);
                                            msg = "Status Changed To 'Requested'..!!";
                                        }
                                        else if (model.UOApproval_Required == "Yes")
                                        {
                                            param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "_New", true);
                                            msg = "Status Changed To 'New'..!!";
                                        }
                                    }

                                    if (Status == "Completed")
                                    {
                                        param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "In_Progress", true);
                                        msg = "Status Changed To 'In_Progress'..!!";


                                    }
                                    break;

                                case "Complete":
                                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Completed", true);
                                    msg = "Updated Successfully..!!</br>Status Changed To Completed..!!";
                                    break;

                                case "Approve":
                                    if (CurrUserRole.Contains("Requestor Incharge"))
                                    {
                                        param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Requested", true);
                                        msg = "Status Changed To 'Requested'..!!";
                                    }
                                    if (CurrUserRole.Contains("Store Keeper"))
                                    {
                                        if (model.UOApproval_Required.ToUpper() == "No".ToUpper())
                                        {
                                            param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Approved", true);
                                            msg = "Status Changed To 'Approved'..!!";
                                        }
                                        else if (model.UOApproval_Required.ToUpper() == "Yes".ToUpper())
                                        {

                                            msg = "Store Keeper Cant Approve UO if Approval is required for UO";
                                        }

                                    }

                                    if (CurrUserRole.Contains("Store Keeper in-charge"))
                                    {

                                        param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Approved", true);
                                        msg = "Status Changed To 'Approved'..!!";
                                    }
                                    break;
                            }
                            if (BASE._user_order_DBOps.UpdateUOStatus(param))

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
                        return Json(new { result = true, message = Messages.UpdateSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (actionmethod == "Delete")
                {
                    if (BASE._user_order_DBOps.DeleteUO(model.UOID))
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

        #region Edit InParam Functions
        public void EditIUDDocuments(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            // Attach Document in update mode

            var DocumentsData = (List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data;
            if (DocumentsData != null)
            {
                var insertAttachments = new List<Parameter_Insert_Attachment>();
                var updateattachment = new List<Parameter_Update_Attachment>();
                string[] doceditid = UserOrderEdit_Document_ID != null ? (string[])(UserOrderEdit_Document_ID as ArrayList).ToArray(typeof(string)) : null;
                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "UO";
                        InEInfo.Ref_Rec_ID = model.UOID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments.Add(InEInfo);
                    }
                    else if (doceditid != null)
                    {
                        if (doceditid.Contains(DocumentsData[i].ID))
                        {
                            var InEInfo = new Parameter_Update_Attachment();
                            InEInfo.FileName = DocumentsData[i].File_Name;
                            InEInfo.Description = DocumentsData[i].Remarks;
                            InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                            InEInfo.Ref_Screen = "UO";
                            InEInfo.Ref_Rec_ID = model.UOID.ToString();
                            InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                            InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                            InEInfo.File = DocumentsData[i].File_Array;
                            InEInfo.RecID = DocumentsData[i].ID;
                            updateattachment.Add(InEInfo);
                        }
                    }
                }
                if (insertAttachments.Count > 0) { param_Update_UO_Txn.Added_Attachments = insertAttachments[0] == null ? null : insertAttachments.ToArray(); }
                else { param_Update_UO_Txn.Added_Attachments = insertAttachments.ToArray(); }
                if (updateattachment.Count > 0) { param_Update_UO_Txn.Updated_Attachments = updateattachment[0] == null ? null : updateattachment.ToArray(); }
                else { param_Update_UO_Txn.Updated_Attachments = updateattachment.ToArray(); }

                var DocAllDeleteIds = UserOrderDelete_Document_ID as ArrayList;
                if (DocAllDeleteIds != null)
                {
                    var deleteDocID = new Param_Deleted_UO_Attachments[DocAllDeleteIds.Count];
                    for (int i = 0; i <= DocAllDeleteIds.Count - 1; i++)
                    {
                        var deleteDocIDInfo = new Param_Deleted_UO_Attachments();
                        deleteDocIDInfo.Rec_ID = DocAllDeleteIds[i].ToString();
                        deleteDocID[i] = deleteDocIDInfo;
                    }
                    param_Update_UO_Txn.Deleted_UOAttachments = deleteDocID;
                }
                var DocAllunlinkIds = UO_Unlink_Document_ID as ArrayList;
                if (DocAllunlinkIds != null)
                {
                    var unlinkDocID = new Param_Unlinked_UO_Attachments[DocAllunlinkIds.Count];
                    for (int i = 0; i <= DocAllunlinkIds.Count - 1; i++)
                    {
                        var unlinkDocIDInfo = new Param_Unlinked_UO_Attachments();
                        unlinkDocIDInfo.Rec_ID = DocAllunlinkIds[i].ToString();
                        unlinkDocID[i] = unlinkDocIDInfo;
                    }
                    param_Update_UO_Txn.Unlinked_UOAttachments = unlinkDocID;
                }

            }

        }
        public void EditIUDItemOrdered(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {

            var insertindex = 0;
            var updateindex = 0;
            var all_data_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;


            if (all_data_OrderItem_Grid != null)
            {
                var InsertOrderItems = new Param_Insert_UO_Item[all_data_OrderItem_Grid.Count()];
                var UpdateOrderItems = new Param_Update_UO_Item[all_data_OrderItem_Grid.Count()];
                int[] UOItemsOrdEditID = UOEdit_ItemOrd_ID != null ? (int[])(UOEdit_ItemOrd_ID as ArrayList).ToArray(typeof(int)) : null;


                for (int I = 0; I <= all_data_OrderItem_Grid.Count() - 1; I++)
                {
                    if (all_data_OrderItem_Grid[I].ID == 0)
                    {
                        var InOrderItemInfo = new Param_Insert_UO_Item();
                        InOrderItemInfo.UO_ID = model.UOID;
                        InOrderItemInfo.Sub_Item_ID = all_data_OrderItem_Grid[I].SubItemID;
                        InOrderItemInfo.Make = all_data_OrderItem_Grid[I].Make;
                        InOrderItemInfo.Model = all_data_OrderItem_Grid[I].Model;
                        InOrderItemInfo.Unit_ID = all_data_OrderItem_Grid[I].UnitID;
                        InOrderItemInfo.Requested_Qty = all_data_OrderItem_Grid[I].Requested_Qty;
                        InOrderItemInfo.Required_Date = all_data_OrderItem_Grid[I].Requested_Delivery_Date;
                        InOrderItemInfo.Scheduled_Delivery_Date = all_data_OrderItem_Grid[I].Scheduled_Delivery_Date;
                        InOrderItemInfo.Priority = all_data_OrderItem_Grid[I].UOI_Priority;
                        InOrderItemInfo.Part_Delivery_Allowed = all_data_OrderItem_Grid[I].Partial_Delivery_Allowed;
                        InOrderItemInfo.Remarks = all_data_OrderItem_Grid[I].Remarks;
                        InOrderItemInfo.Delivery_Location_Id = all_data_OrderItem_Grid[I].Delivery_Location_ID;
                        InOrderItemInfo.Approved_Qty = all_data_OrderItem_Grid[I].Approved_Qty;
                        InsertOrderItems[insertindex] = InOrderItemInfo;
                        insertindex++;
                    }
                    else if (UOItemsOrdEditID != null)
                    {
                        if (UOItemsOrdEditID.Contains(all_data_OrderItem_Grid[I].ID))
                        {
                            var UpdateOrderItemInfo = new Param_Update_UO_Item();
                            UpdateOrderItemInfo.ID = all_data_OrderItem_Grid[I].ID;
                            UpdateOrderItemInfo.UO_ID = model.UOID;
                            UpdateOrderItemInfo.Sub_Item_ID = all_data_OrderItem_Grid[I].SubItemID;
                            UpdateOrderItemInfo.Make = all_data_OrderItem_Grid[I].Make;
                            UpdateOrderItemInfo.Model = all_data_OrderItem_Grid[I].Model;
                            UpdateOrderItemInfo.Unit_ID = all_data_OrderItem_Grid[I].UnitID;
                            UpdateOrderItemInfo.Requested_Qty = all_data_OrderItem_Grid[I].Requested_Qty;
                            UpdateOrderItemInfo.Required_Date = all_data_OrderItem_Grid[I].Requested_Delivery_Date;
                            UpdateOrderItemInfo.Scheduled_Delivery_Date = all_data_OrderItem_Grid[I].Scheduled_Delivery_Date;
                            UpdateOrderItemInfo.Priority = all_data_OrderItem_Grid[I].UOI_Priority;
                            UpdateOrderItemInfo.Part_Delivery_Allowed = all_data_OrderItem_Grid[I].Partial_Delivery_Allowed;
                            UpdateOrderItemInfo.Delivery_Location_Id = all_data_OrderItem_Grid[I].Delivery_Location_ID;
                            UpdateOrderItemInfo.Remarks = all_data_OrderItem_Grid[I].Remarks;
                            UpdateOrderItemInfo.Approved_Qty = all_data_OrderItem_Grid[I].Approved_Qty;
                            UpdateOrderItems[updateindex] = UpdateOrderItemInfo;
                            updateindex++;
                        }
                    }
                }

                if (InsertOrderItems.Length > 0)
                // { param_Update_UO_Txn.InUOItems = InsertOrderItems[0] == null ? null : InsertOrderItems; }
                //else
                {
                    param_Update_UO_Txn.InUOItems = InsertOrderItems;
                }
                if (UpdateOrderItems.Length > 0)
                //{ param_Update_UO_Txn.UpdateUOItems = UpdateOrderItems[0] == null ? null : UpdateOrderItems; }
                //else
                {

                    param_Update_UO_Txn.UpdateUOItems = UpdateOrderItems;
                }
            }

            //delete Item in update mode
            var ItemAllDeleteIds = Delete_User_OrderItem_ID as ArrayList;
            if (ItemAllDeleteIds != null)
            {
                var deleteItemID = new Common_Lib.RealTimeService.Param_Deleted_UO_Items[ItemAllDeleteIds.Count];
                for (int i = 0; i <= ItemAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemIDInfo = new Common_Lib.RealTimeService.Param_Deleted_UO_Items();
                    deleteItemIDInfo.Rec_ID = Convert.ToInt32(ItemAllDeleteIds[i]);
                    deleteItemID[i] = deleteItemIDInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Items = deleteItemID;

            }


        }
        public void EditIUDGoodsDelivery(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            var insertindex = 0;
            var updateindex = 0;

            var all_data_Of_Delivery = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;

            var all_data_Of_NestedDelivery = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
            Session["UO_GoodsDeliveredNestedData"] = UO_GoodsDeliveredNestedData;
            if (all_data_Of_Delivery != null)
            {
                var insertDelivery = new Param_Insert_UO_Item_Delivered[all_data_Of_Delivery.Count()];
                var UpdateDelivery = new Param_Update_UO_Delivery[all_data_Of_Delivery.Count()];
                int[] all_data_Of_UpdateDelivery = UOEdit_GoodsDelivered_ID != null ? (int[])(UOEdit_GoodsDelivered_ID as ArrayList).ToArray(typeof(int)) : null;

                for (int I = 0; I <= all_data_Of_Delivery.Count() - 1; I++)
                {
                    if (all_data_Of_Delivery[I].ID == 0)
                    {
                        var DeliveryinsertData = new Param_Insert_UO_Item_Delivered();

                        DeliveryinsertData.UO_ID = model.UOID;

                        DeliveryinsertData.UO_Item_ID = all_data_Of_Delivery[I].ItemRequestedID;
                        DeliveryinsertData.Delivered_Qty = all_data_Of_Delivery[I].Delivered_Qty;
                        DeliveryinsertData.Delivery_Date = all_data_Of_Delivery[I].Delivery_Date;
                        DeliveryinsertData.Delivery_Mode_ID = all_data_Of_Delivery[I].ModeID;
                        DeliveryinsertData.Delivery_Carrier = all_data_Of_Delivery[I].Carrier;
                        DeliveryinsertData.Delivery_Location_ID = all_data_Of_Delivery[I].DeliveryLocationId;
                        DeliveryinsertData.Delivered_By_ID = all_data_Of_Delivery[I].DeliveredByID;
                        DeliveryinsertData.Delivery_Driver_ID = all_data_Of_Delivery[I].DriverID;
                        DeliveryinsertData.Delivery_Receiver_Name = all_data_Of_Delivery[I].Receiver_Name;
                        DeliveryinsertData.Delivery_Remarks = all_data_Of_Delivery[I].Remarks;
                        DeliveryinsertData.VehicleNo = all_data_Of_Delivery[I].Vehicle_No;

                        //Insert nested Delivery in Update Mode 


                        if (all_data_Of_NestedDelivery != null)
                        {

                            var insertNestedDeli = new Param_Insert_UO_Item_Delivered_Stocks[all_data_Of_NestedDelivery.Count()];
                            for (int J = 0; J <= all_data_Of_NestedDelivery.Count - 1; J++)
                            {
                                if (all_data_Of_NestedDelivery[J].UD_ID == 0)
                                {

                                    if (all_data_Of_Delivery[I].Sr == all_data_Of_NestedDelivery[J].MainSr)
                                    {

                                        var DeliveryinsertNestedData = new Param_Insert_UO_Item_Delivered_Stocks();

                                        DeliveryinsertNestedData.UO_ID = model.UOID;
                                        DeliveryinsertNestedData.UD_ID = all_data_Of_Delivery[I].ID;
                                        DeliveryinsertNestedData.UDS_Qty = all_data_Of_NestedDelivery[J].UDS_Qty;
                                        DeliveryinsertNestedData.Stock_ID = all_data_Of_NestedDelivery[J].StockRecordID;
                                        insertNestedDeli[J] = DeliveryinsertNestedData;
                                    }
                                }
                            }
                            //param_Insert_UO_Delivery._Delivered_Stock = insertNestedDeli;
                            DeliveryinsertData._Delivered_Stock = insertNestedDeli;
                        }

                        insertDelivery[insertindex] = DeliveryinsertData;
                        insertindex++;
                    }




                    else if (all_data_Of_UpdateDelivery != null)
                    {
                        if (all_data_Of_UpdateDelivery.Contains(all_data_Of_Delivery[I].ID))
                        {

                            var DeliveredUpdateData = new Param_Update_UO_Delivery();

                            DeliveredUpdateData.UO_ID = model.UOID;

                            DeliveredUpdateData.UO_Item_ID = all_data_Of_Delivery[I].ItemRequestedID;
                            DeliveredUpdateData.Delivered_Qty = all_data_Of_Delivery[I].Delivered_Qty;
                            DeliveredUpdateData.Delivery_Date = all_data_Of_Delivery[I].Delivery_Date;
                            DeliveredUpdateData.Delivery_Mode_ID = all_data_Of_Delivery[I].ModeID;
                            DeliveredUpdateData.Delivery_Carrier = all_data_Of_Delivery[I].Carrier;
                            DeliveredUpdateData.Delivery_Location_ID = all_data_Of_Delivery[I].DeliveryLocationId;
                            DeliveredUpdateData.Delivered_By_ID = all_data_Of_Delivery[I].DeliveredByID;
                            DeliveredUpdateData.Delivery_Driver_ID = all_data_Of_Delivery[I].DriverID;
                            DeliveredUpdateData.Delivery_Receiver_Name = all_data_Of_Delivery[I].Receiver_Name;
                            DeliveredUpdateData.Delivery_Remarks = all_data_Of_Delivery[I].Remarks;
                            //   DeliveredUpdateData.s = all_data_Of_Delivery[I].StockRecordID;
                            //DeliveredUpdateData.Stock_ID = 7003;
                            DeliveredUpdateData.VehicleNo = all_data_Of_Delivery[I].Vehicle_No;

                            DeliveredUpdateData.ID = all_data_Of_Delivery[I].ID;

                            //Update nested Delivery in Update Mode 


                            if (all_data_Of_NestedDelivery != null)
                            {

                                var insertNestedDeli = new Param_Insert_UO_Item_Delivered_Stocks[all_data_Of_NestedDelivery.Count()];
                                for (int J = 0; J <= all_data_Of_NestedDelivery.Count - 1; J++)
                                {
                                    if (all_data_Of_NestedDelivery[J].UD_ID == 0 || all_data_Of_NestedDelivery[J].UD_ID == all_data_Of_Delivery[I].ID)
                                    {

                                        if (all_data_Of_Delivery[I].Sr == all_data_Of_NestedDelivery[J].MainSr)
                                        {

                                            var DeliveryinsertNestedData = new Param_Insert_UO_Item_Delivered_Stocks();

                                            DeliveryinsertNestedData.UO_ID = model.UOID;
                                            DeliveryinsertNestedData.UD_ID = all_data_Of_Delivery[I].ID;
                                            DeliveryinsertNestedData.UDS_Qty = all_data_Of_NestedDelivery[J].UDS_Qty;
                                            DeliveryinsertNestedData.Stock_ID = all_data_Of_NestedDelivery[J].StockRecordID;
                                            insertNestedDeli[J] = DeliveryinsertNestedData;
                                        }
                                    }
                                }
                                //param_Insert_UO_Delivery._Delivered_Stock = insertNestedDeli;
                                DeliveredUpdateData._Added_Delivered_Stock = insertNestedDeli;
                            }

                            UpdateDelivery[updateindex] = DeliveredUpdateData;
                            updateindex++;
                        }
                    }
                }

                if (insertDelivery.Length > 0) {
                    //    param_Update_UO_Txn.InUODeliveries = insertDelivery[0] == null ? null : insertDelivery; }
                    //else
                    //{
                    param_Update_UO_Txn.InUODeliveries = insertDelivery;
                }
                if (UpdateDelivery.Length > 0) {
                    //    param_Update_UO_Txn.UpdateUODeliveries = UpdateDelivery[0] == null ? null : UpdateDelivery; }
                    //else
                    //{

                    param_Update_UO_Txn.UpdateUODeliveries = UpdateDelivery;
                }
            }


            //Delete Delivery in Update Mode 
            var ItemDeliveredAllDeleteIds = Delete_User_GoodsDelivered_ID as ArrayList;
            if (ItemDeliveredAllDeleteIds != null)
            {
                var deleteItemDeliveredID = new Param_Deleted_UO_Items_Delivered[ItemDeliveredAllDeleteIds.Count];
                for (int i = 0; i <= ItemDeliveredAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemDeliIDInfo = new Param_Deleted_UO_Items_Delivered();
                    deleteItemDeliIDInfo.Rec_ID = Convert.ToInt32(ItemDeliveredAllDeleteIds[i]);
                    deleteItemDeliveredID[i] = deleteItemDeliIDInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Items_Delivered = deleteItemDeliveredID;

            }
        }


        public void EditIUDGoodsReceived(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            var insertindex = 0;
            var updateindex = 0;

            var all_data_Of_Receivedgrid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            if (all_data_Of_Receivedgrid != null)
            {
                var insertReceived = new Param_Insert_UO_Item_Received[all_data_Of_Receivedgrid.Count()];
                var UpdateReceived = new Param_Update_UO_Received[all_data_Of_Receivedgrid.Count()];
                int[] all_data_Of_UpdateReCeived = UOEdit_GoodsRec_ID != null ? (int[])(UOEdit_GoodsRec_ID as ArrayList).ToArray(typeof(int)) : null;


                // string[] receivedid = UOEdit_GoodsRec_ID != null ? (string[])(UOEdit_GoodsRec_ID as ArrayList).ToArray(typeof(string)) : null;

                for (int I = 0; I <= all_data_Of_Receivedgrid.Count() - 1; I++)
                {
                    if (all_data_Of_Receivedgrid[I].ID == 0)
                    {
                        var ReceivedinsertData = new Param_Insert_UO_Item_Received();

                        ReceivedinsertData.UO_ID = model.UOID;

                        ReceivedinsertData.UO_Delivered_ID = all_data_Of_Receivedgrid[I].DeliveryEntryID;

                        ReceivedinsertData.Recd_Qty = all_data_Of_Receivedgrid[I].Received_Qty;

                        ReceivedinsertData.Recd_Date = all_data_Of_Receivedgrid[I].Received_Date;

                        ReceivedinsertData.Delivery_Mode_ID = all_data_Of_Receivedgrid[I].ModeID;
                        ReceivedinsertData.Delivery_Carrier = all_data_Of_Receivedgrid[I].Carrier;
                        ReceivedinsertData.Received_Location_ID = all_data_Of_Receivedgrid[I].DeliveryLocationId;
                        ReceivedinsertData.Bill_No = all_data_Of_Receivedgrid[I].Bill_No;
                        ReceivedinsertData.Challan_No = all_data_Of_Receivedgrid[I].Challan_No;

                        ReceivedinsertData.Received_By_ID = all_data_Of_Receivedgrid[I].ReceivedByID;
                        ReceivedinsertData.UDS_ID = all_data_Of_Receivedgrid[I].UDS_ID;
                        ReceivedinsertData.Receiver_Remarks = all_data_Of_Receivedgrid[I].Remarks;
                        ReceivedinsertData.UO_Returned_ID = all_data_Of_Receivedgrid[I].ReturnedEntryID;
                        insertReceived[insertindex] = ReceivedinsertData;
                        insertindex++;
                    }
                    //Update Returned in Update Mode 
                    else if (all_data_Of_UpdateReCeived != null)
                    {
                        if (all_data_Of_UpdateReCeived.Contains(all_data_Of_Receivedgrid[I].ID))
                        {

                            var ReceivedUpdateData = new Param_Update_UO_Received();

                            ReceivedUpdateData.UO_ID = model.UOID;

                            ReceivedUpdateData.UO_Delivered_ID = all_data_Of_Receivedgrid[I].DeliveryEntryID;

                            ReceivedUpdateData.Recd_Qty = all_data_Of_Receivedgrid[I].Received_Qty;

                            ReceivedUpdateData.Recd_Date = Convert.ToDateTime(all_data_Of_Receivedgrid[I].Received_Date);

                            ReceivedUpdateData.Delivery_Mode_ID = all_data_Of_Receivedgrid[I].ModeID;
                            ReceivedUpdateData.Delivery_Carrier = all_data_Of_Receivedgrid[I].Carrier;
                            ReceivedUpdateData.Received_Location_ID = all_data_Of_Receivedgrid[I].DeliveryLocationId;
                            ReceivedUpdateData.Bill_No = all_data_Of_Receivedgrid[I].Bill_No;
                            ReceivedUpdateData.Challan_No = all_data_Of_Receivedgrid[I].Challan_No;

                            ReceivedUpdateData.UDS_ID = all_data_Of_Receivedgrid[I].UDS_ID;
                            ReceivedUpdateData.Received_By_ID = all_data_Of_Receivedgrid[I].ReceivedByID;

                            ReceivedUpdateData.Receiver_Remarks = all_data_Of_Receivedgrid[I].Remarks;
                            ReceivedUpdateData.UO_Returned_ID = all_data_Of_Receivedgrid[I].ReturnedEntryID;
                            ReceivedUpdateData.ID = Convert.ToInt32(all_data_Of_Receivedgrid[I].ID);
                            UpdateReceived[updateindex] = ReceivedUpdateData;
                            updateindex++;
                        }
                    }
                }
                if (insertReceived.Length > 0)
                {
                    //    param_Update_UO_Txn.InUOReceipts = insertReceived[0] == null ? null : insertReceived; }
                    //else
                    //{

                    param_Update_UO_Txn.InUOReceipts = insertReceived;
                }

                if (UpdateReceived.Length > 0)
                {
                    //    param_Update_UO_Txn.UpdateUOReceipts = UpdateReceived[0] == null ? null : UpdateReceived; }
                    //else
                    //{

                    param_Update_UO_Txn.UpdateUOReceipts = UpdateReceived;
                }
            }


            //Delete Received in Update Mode 
            var ItemReceivedAllDeleteIds = Delete_User_GoodsRecd_ID as ArrayList;
            if (ItemReceivedAllDeleteIds != null)
            {
                var deleteItemReceivedID = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_Received[ItemReceivedAllDeleteIds.Count];
                for (int i = 0; i <= ItemReceivedAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemRcvdIDInfo = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_Received();
                    deleteItemRcvdIDInfo.Rec_ID = Convert.ToInt32(ItemReceivedAllDeleteIds[i]);
                    deleteItemReceivedID[i] = deleteItemRcvdIDInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Items_Receipts = deleteItemReceivedID;
                //Delete_User_GoodsRecd_ID = null;
            }

        }
        public void EditIUDGoodsReturned(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            var insertindex = 0;
            var updateindex = 0;
            var all_data_Of_Returned = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
            if (all_data_Of_Returned != null)
            {
                var insertReturned = new Param_Insert_UO_Item_Returned[all_data_Of_Returned.Count()];
                var updateReturned = new Param_Update_UO_Returned[all_data_Of_Returned.Count()];
                int[] all_data_Of_UpdateReturned = UOEdit_GoodsRet_ID != null ? (int[])(UOEdit_GoodsRet_ID as ArrayList).ToArray(typeof(int)) : null;

                for (int I = 0; I <= all_data_Of_Returned.Count() - 1; I++)
                {
                    if (all_data_Of_Returned[I].ID == 0)
                    {
                        var ReturnedInsertData = new Param_Insert_UO_Item_Returned();
                        ReturnedInsertData.UO_ID = model.UOID;

                        ReturnedInsertData.Returned_Qty = all_data_Of_Returned[I].Returned_Qty;
                        if (all_data_Of_Returned[I].Return_Date != null)
                        {
                            ReturnedInsertData.Returned_Date = all_data_Of_Returned[I].Return_Date;
                        }
                        ReturnedInsertData.Delivery_Mode_ID = all_data_Of_Returned[I].ModeID;
                        ReturnedInsertData.Delivery_Carrier = all_data_Of_Returned[I].Carrier;
                        ReturnedInsertData.Returned_Location_ID = all_data_Of_Returned[I].ReturnLocationId;
                        if (all_data_Of_Returned[I].ReturnedByID == 0)
                        {
                            ReturnedInsertData.Returned_By_ID = null;
                        }
                        else
                        {
                            ReturnedInsertData.Returned_By_ID = all_data_Of_Returned[I].ReturnedByID;
                        }
                        ReturnedInsertData.Returned_by_Remarks = all_data_Of_Returned[I].Remarks;
                        ReturnedInsertData.Recd_Item_ID = all_data_Of_Returned[I].ItemReceivedID;
                        insertReturned[insertindex] = ReturnedInsertData;
                        insertindex++;
                    }



                    //Update Returned in Update Mode 
                    else if (all_data_Of_UpdateReturned != null)
                    {
                        if (all_data_Of_UpdateReturned.Contains(all_data_Of_Returned[I].ID))
                        {

                            var ReturnedUpdateData = new Param_Update_UO_Returned();
                            ReturnedUpdateData.UO_ID = model.UOID;
                            ReturnedUpdateData.Returned_Qty = all_data_Of_Returned[I].Returned_Qty;
                            if (all_data_Of_Returned[I].Return_Date != null)
                            {
                                ReturnedUpdateData.Returned_Date = all_data_Of_Returned[I].Return_Date;
                            }
                            ReturnedUpdateData.Delivery_Mode_ID = all_data_Of_Returned[I].ModeID;
                            ReturnedUpdateData.Delivery_Carrier = all_data_Of_Returned[I].Carrier;
                            ReturnedUpdateData.Returned_Location_ID = all_data_Of_Returned[I].ReturnLocationId;
                            if (all_data_Of_Returned[I].ReturnedByID != null)
                            {
                                ReturnedUpdateData.Returned_By_ID = all_data_Of_Returned[I].ReturnedByID;
                            }
                            ReturnedUpdateData.Returned_by_Remarks = all_data_Of_Returned[I].Remarks;
                            ReturnedUpdateData.Recd_Item_ID = all_data_Of_Returned[I].ItemReceivedID;
                            ReturnedUpdateData.ID = all_data_Of_Returned[I].ID;
                            updateReturned[updateindex] = ReturnedUpdateData;
                            updateindex++;
                        }
                    }
                }

                if (insertReturned.Length > 0) {
                    //    param_Update_UO_Txn.InUOReturns = insertReturned[0] == null ? null : insertReturned; }
                    //else
                    //{
                    param_Update_UO_Txn.InUOReturns = insertReturned;
                }
                if (updateReturned.Length > 0) {
                    //    param_Update_UO_Txn.UpdateUOReturns = updateReturned[0] == null ? null : updateReturned; }
                    //else
                    //{

                    param_Update_UO_Txn.UpdateUOReturns = updateReturned;
                }
            }

            //Delete Returned in Update Mode 
            var ItemReturnedAllDeleteIds = Delete_User_GoodsRetn_ID as ArrayList;
            if (ItemReturnedAllDeleteIds != null)
            {
                var deleteItemReturnedID = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_Returned[ItemReturnedAllDeleteIds.Count];
                for (int i = 0; i <= ItemReturnedAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemRetnIDInfo = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_Returned();
                    deleteItemRetnIDInfo.Rec_ID = Convert.ToInt32(ItemReturnedAllDeleteIds[i]);
                    deleteItemReturnedID[i] = deleteItemRetnIDInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Items_Returns = deleteItemReturnedID;
                //Delete_User_GoodsRetn_ID = null;
            }
        }



        public void EditIUDGoodsReturnedReceived(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            var insertindex = 0;
            var updateindex = 0;

            var all_data_Of_ReturnReceived = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
            if (all_data_Of_ReturnReceived != null)
            {
                var insertReturnReceived = new Param_Insert_UO_Item_Return_Received[all_data_Of_ReturnReceived.Count()];
                var UpdateRetReceived = new Param_Update_UO_Return_Received[all_data_Of_ReturnReceived.Count()];
                int[] all_data_Of_UpdateRetReceived = UOEdit_GoodsRetRec_ID != null ? (int[])(UOEdit_GoodsRetRec_ID as ArrayList).ToArray(typeof(int)) : null;

                for (int I = 0; I <= all_data_Of_ReturnReceived.Count() - 1; I++)
                {
                    if (all_data_Of_ReturnReceived[I].ID == 0)
                    {
                        var RetReceivedinsertData = new Param_Insert_UO_Item_Return_Received();

                        RetReceivedinsertData.UO_ID = model.UOID;


                        RetReceivedinsertData.UO_Delivered_ID = all_data_Of_ReturnReceived[I].DeliveryEntryID;

                        RetReceivedinsertData.Received_Qty = all_data_Of_ReturnReceived[I].Received_Qty;

                        RetReceivedinsertData.Received_Date = all_data_Of_ReturnReceived[I].Received_Date;

                        RetReceivedinsertData.Received_Mode_ID = all_data_Of_ReturnReceived[I].ModeID;
                        RetReceivedinsertData.Delivery_Carrier = all_data_Of_ReturnReceived[I].Carrier;
                        RetReceivedinsertData.Ret_Rec_Location_ID = all_data_Of_ReturnReceived[I].RecdLocationId;

                        RetReceivedinsertData.Received_By_ID = all_data_Of_ReturnReceived[I].RecdByID;

                        RetReceivedinsertData.URR_UDS_ID = all_data_Of_ReturnReceived[I].UDS_ID;

                        RetReceivedinsertData.Receiver_Remarks = all_data_Of_ReturnReceived[I].Remarks;
                        RetReceivedinsertData.UO_Returned_ID = all_data_Of_ReturnReceived[I].ReturnEntryID;
                        insertReturnReceived[insertindex] = RetReceivedinsertData;
                        insertindex++;
                    }

                    else if (all_data_Of_UpdateRetReceived != null)
                    {
                        if (all_data_Of_UpdateRetReceived.Contains(all_data_Of_ReturnReceived[I].ID))
                        {
                            var RetReceivedUpdateData = new Param_Update_UO_Return_Received();

                            RetReceivedUpdateData.UO_ID = model.UOID;

                            RetReceivedUpdateData.UO_Delivered_ID = all_data_Of_ReturnReceived[I].DeliveryEntryID;

                            RetReceivedUpdateData.Received_Qty = all_data_Of_ReturnReceived[I].Received_Qty;

                            RetReceivedUpdateData.Received_Date = all_data_Of_ReturnReceived[I].Received_Date;

                            RetReceivedUpdateData.Received_Mode_ID = all_data_Of_ReturnReceived[I].ModeID;
                            RetReceivedUpdateData.Delivery_Carrier = all_data_Of_ReturnReceived[I].Carrier;
                            RetReceivedUpdateData.Ret_Rec_Location_ID = all_data_Of_ReturnReceived[I].RecdLocationId;


                            RetReceivedUpdateData.Received_By_ID = all_data_Of_ReturnReceived[I].RecdByID;
                            RetReceivedUpdateData.URR_UDS_ID = all_data_Of_ReturnReceived[I].UDS_ID;
                            RetReceivedUpdateData.Receiver_Remarks = all_data_Of_ReturnReceived[I].Remarks;
                            RetReceivedUpdateData.UO_Returned_ID = all_data_Of_ReturnReceived[I].ReturnEntryID;
                            RetReceivedUpdateData.ID = all_data_Of_ReturnReceived[I].ID;
                            UpdateRetReceived[updateindex] = RetReceivedUpdateData;
                            updateindex++;
                        }
                    }
                }

                if (insertReturnReceived.Length > 0) {
                    //    param_Update_UO_Txn.UpdateUORerturnReceipts = UpdateRetReceived[0] == null ? null : UpdateRetReceived; }
                    //else
                    //{
                    param_Update_UO_Txn.InUORerturnReceipts = insertReturnReceived;
                }
                if (UpdateRetReceived.Length > 0) {
                    //    param_Update_UO_Txn.UpdateUORerturnReceipts = UpdateRetReceived[0] == null ? null : UpdateRetReceived; }
                    //else
                    //{

                    param_Update_UO_Txn.UpdateUORerturnReceipts = UpdateRetReceived;
                }
            }

            //Delete Return Received in Update Mode 
            var ItemReturnReceivedAllDeleteIds = Delete_User_GoodsRetRecd_ID as ArrayList;
            if (ItemReturnReceivedAllDeleteIds != null)
            {
                var deleteItemReturnReceivedID = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_ReturnReceived[ItemReturnReceivedAllDeleteIds.Count];
                for (int i = 0; i <= ItemReturnReceivedAllDeleteIds.Count - 1; i++)
                {
                    var deleteItemRetRcvdIDInfo = new Common_Lib.RealTimeService.Param_Deleted_UO_Items_ReturnReceived();
                    deleteItemRetRcvdIDInfo.Rec_ID = Convert.ToInt32(ItemReturnReceivedAllDeleteIds[i]);
                    deleteItemReturnReceivedID[i] = deleteItemRetRcvdIDInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Items_Return_Receipts = deleteItemReturnReceivedID;
                //Delete_User_GoodsRetRecd_ID = null;
            }
        }


        public void EditIUDScrap(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            var insertindex = 0;
            var updateindex = 0;

            var all_data_Scrap_Grid = (List<Return_GetUOScrapCreated>)UO_ScrapData;


            if (all_data_Scrap_Grid != null)
            {

                var InsertScrap = new Param_Add_Stock_Addition[all_data_Scrap_Grid.Count()];
                var UpdateScrap = new Param_Update_StockProfile[all_data_Scrap_Grid.Count()];
                int[] all_data_UpdateScrap_Grid = UOEdit_Scrap_ID != null ? (int[])(UOEdit_Scrap_ID as ArrayList).ToArray(typeof(int)) : null;

                for (int I = 0; I <= all_data_Scrap_Grid.Count() - 1; I++)
                {
                    if (all_data_Scrap_Grid[I].ID == 0)
                    {
                        var InScrapInfo = new Param_Add_Stock_Addition();

                        InScrapInfo.Store_Dept_ID = Convert.ToInt32(model.UORequest_Store_Id);
                        InScrapInfo.item_id = all_data_Scrap_Grid[I].ItemID;
                        InScrapInfo.make = "UO - Scrap";
                        InScrapInfo.model = "UO - Scrap";
                        InScrapInfo.serial_no = "UO - Scrap" + model.UO_number;
                        InScrapInfo.Quantity = Convert.ToDouble(all_data_Scrap_Grid[I].Qty);
                        InScrapInfo.Unit_Id = all_data_Scrap_Grid[I].UnitID;
                        InScrapInfo.Date_Of_Purchase = DateTime.Now;
                        InScrapInfo.total_value = Convert.ToDouble(all_data_Scrap_Grid[I].Amount);
                        InScrapInfo.Location_Id = all_data_Scrap_Grid[I].LocationID;
                        //InScrapInfo.Project_ID = ;
                        // InScrapInfo.Warranty = " "; 
                        InScrapInfo.Remarks = all_data_Scrap_Grid[I].Remarks;
                        // InScrapInfo.Stock_TR_ID = 
                        // InScrapInfo.Stock_Tr_Sr_No =
                        InScrapInfo.Stock_Ref_ID = model.UOID;
                        InScrapInfo.Stock_Ref_Entry_Source = Stock_Addition_Source.User_Order_Scrap_Screen;
                        // InScrapInfo.Stock_Ref_Sub_ID =                         

                        InsertScrap[insertindex] = InScrapInfo;
                        insertindex++;
                    }


                    if (all_data_UpdateScrap_Grid != null)
                    {
                        if (all_data_UpdateScrap_Grid.Contains(all_data_Scrap_Grid[I].ID))
                        {

                            var UpdateScrapInfo = new Param_Update_StockProfile();

                            UpdateScrapInfo.Rec_ID = all_data_Scrap_Grid[I].ID;
                            UpdateScrapInfo.Store_Dept_ID = Convert.ToInt32(model.UORequest_Store_Id);
                            UpdateScrapInfo.sub_Item_ID = all_data_Scrap_Grid[I].ItemID;
                            UpdateScrapInfo.make = "UO - Scrap";
                            UpdateScrapInfo.model = "UO - Scrap";
                            UpdateScrapInfo.serial_no = "UO - Scrap" + model.UO_number;
                            UpdateScrapInfo.Quantity = Convert.ToDouble(all_data_Scrap_Grid[I].Qty);
                            UpdateScrapInfo.Unit_Id = all_data_Scrap_Grid[I].UnitID;
                            UpdateScrapInfo.Date_Of_Purchase = DateTime.Now;
                            UpdateScrapInfo.total_value = Convert.ToDouble(all_data_Scrap_Grid[I].Amount);
                            UpdateScrapInfo.Location_Id = all_data_Scrap_Grid[I].LocationID;
                            // InScrapInfo.Project_ID = ;
                            // InScrapInfo.Warranty = " "; 
                            UpdateScrapInfo.Remarks = all_data_Scrap_Grid[I].Remarks;

                            UpdateScrapInfo.StockID = all_data_Scrap_Grid[I].ID.ToString();
                            // InScrapInfo.Stock_TR_ID = 
                            // InScrapInfo.Stock_Tr_Sr_No =
                            //UpdateScrapInfo.Stock_Ref_ID = model.UOID;
                            //UpdateScrapInfo.Stock_Ref_Entry_Source = Stock_Addition_Source.User_Order_Scrap_Screen;
                            // InScrapInfo.Stock_Ref_Sub_ID =                         

                            //UpdateScrapInfo.Stock_Ref_Entry_Source = Stock_Addition_Source.User_Order_Scrap_Screen;
                            //UpdateScrapInfo.sto =  
                            //UpdateScrapInfo.Applicable_To = all_data_AttachDoc_Grid[I].Applicable_To;

                            UpdateScrap[updateindex] = UpdateScrapInfo;
                            updateindex++;
                        }
                    }
                }
                if (InsertScrap.Length > 0) {
                    //    param_Update_UO_Txn.InUOScrapCreated = InsertScrap[0] == null ? null : InsertScrap; }
                    //else
                    //{
                    param_Update_UO_Txn.InUOScrapCreated = InsertScrap;
                }
                if (UpdateScrap.Length > 0) {
                    //    param_Update_UO_Txn.UpdateUOScrapCreated = UpdateScrap[0] == null ? null : UpdateScrap; }
                    //else
                    //{

                    param_Update_UO_Txn.UpdateUOScrapCreated = UpdateScrap;
                }
            }


            //Delete Scrap Created
            var allScrapData = Delete_User_Scrap_ID as ArrayList;
            if (allScrapData != null)
            {
                var deleteScrapIDs = new Param_Deleted_UO_Scrap[allScrapData.Count];
                for (int i = 0; i <= allScrapData.Count - 1; i++)
                {
                    var deleteScrapInfo = new Param_Deleted_UO_Scrap();
                    deleteScrapInfo.Rec_ID = Convert.ToInt32(allScrapData[i]);
                    deleteScrapIDs[i] = deleteScrapInfo;
                }
                param_Update_UO_Txn.Deleted_UO_Scrap = deleteScrapIDs;
                //Delete_User_Scrap_ID = null;
            }

        }

        public void EditIUDRemainingData(ref Param_Update_UO_Txn param_Update_UO_Txn, NEVD_UserOrder model)
        {
            param_Update_UO_Txn.UO_Initiated_Mode = model.UO_Initiate_Mode;
            param_Update_UO_Txn.UO_Date = model.User_Order_Date;
            param_Update_UO_Txn.UO_Job_ID = Convert.ToInt32(model.UOJob_Name_Id);
            param_Update_UO_Txn.UO_store_ID = Convert.ToInt32(model.UORequest_Store_Id);
            param_Update_UO_Txn.UO_Requestor_ID = Convert.ToInt32(model.UORequest_Name_Id);
            param_Update_UO_Txn.UO_Requestor_Main_Dept_Id = model.UORequestor_Department_Name_Id;
            param_Update_UO_Txn.UO_Requestor_Sub_Dept_Id = model.UORequestor_Sub_Department_Name_Id;
            param_Update_UO_Txn.Remarks = model.Uo_Remarks;
            param_Update_UO_Txn.UOID = model.UOID;

            //delete Remarks
            var allData = Delete_UOExisting_Remarks_ID as ArrayList;
            if (allData != null)
            {
                var deleteExist_RemarksID = new Common_Lib.RealTimeService.Param_Deleted_UO_Remarks[allData.Count];
                for (int i = 0; i <= allData.Count - 1; i++)
                {
                    var deleteRemarksInfo = new Common_Lib.RealTimeService.Param_Deleted_UO_Remarks();
                    deleteRemarksInfo.Rec_ID = Convert.ToInt32(allData[i]);
                    deleteExist_RemarksID[i] = deleteRemarksInfo;
                }
                param_Update_UO_Txn.Deleted_UORemarks = deleteExist_RemarksID;
                //Delete_UOExisting_Remarks_ID = null;
            }
        }

        #endregion Edit InParam Functions
        public ActionResult LookUp_Get_Job_Name(DataSourceLoadOptions loadOptions)
        {
            var UONEVDjob = BASE._Jobs_Dbops.GetOpenJobs();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UONEVDjob, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Request_Store(DataSourceLoadOptions loadOptions)
        {
            var UONEVDRequeststore = BASE._StockDeptStores_dbops.GetAllStoreList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UONEVDRequeststore, loadOptions)), "application/json");
        }

        public JsonResult NewRequesteeStoreItemCheck(int? NewStoreID)
        {
            Param_GetStoreItems inparam = new Param_GetStoreItems();

            inparam.StoreID = NewStoreID;

            var storeitemlist = BASE._Sub_Item_DBOps.GetStoreItems(inparam, ClientScreen.Stock_UO);
            var flag = 0;
            var ItemName = "";
            if (NewStoreID != 0)
            {
                var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
                if (all_data_Of_OrderItem_Grid != null)
                {

                    for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                    {
                        var item = storeitemlist.FindAll(x => x.ItemID == all_data_Of_OrderItem_Grid[I].SubItemID);
                        if (item.Count <= 0)
                        {
                            flag = 1;
                            ItemName = all_data_Of_OrderItem_Grid[I].Item_Name;
                            break;

                        }

                    }


                    if (flag == 1)
                    {
                        return Json(new
                        {
                            Message = "Item (" + ItemName + ") is not available in Newly slected store,Store change is not allowed....!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    else if (flag == 0)
                    {
                        return Json(new
                        {
                            Message = "",
                            result = true
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

        public ActionResult LookUp_Get_Request_Name(DataSourceLoadOptions loadOptions)
        {
            var UONEVDRequestName = BASE._Jobs_Dbops.GetStockPersonnels();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UONEVDRequestName, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Department_Name(DataSourceLoadOptions loadOptions)
        {
            var UONEVDDept = BASE._StockDeptStores_dbops.GetMainDeptList(Common_Lib.RealTimeService.ClientScreen.Stock_UO);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UONEVDDept, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Sub_Department_Name(DataSourceLoadOptions loadOptions, int? MainDeptID)
        {
            if (MainDeptID != null)
            {

                var UONEVDSubDept = BASE._StockDeptStores_dbops.GetSubDeptList(Common_Lib.RealTimeService.ClientScreen.Stock_UO, MainDeptID);
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UONEVDSubDept, loadOptions)), "application/json");
            }
            else
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<StockDeptStores.Return_GetStoreDept>(), loadOptions)), "application/json");
            }
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_UO, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#UserOrderRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return View();
        }
        public ActionResult CheckDocumentsLinked(int ID)
        {
            var docdata = UserOrder_Documents_Window_Grid_Data as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._user_order_DBOps.GetAttachmentLinkScreen(ID, docdata[i].ID); //used project class function instead of UO function
                    if (!string.IsNullOrEmpty(screen))
                    {
                        if (screen != "UO")
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

        //public ActionResult UOContextMenuChecks(int UOID)
        //{
        //    var userorder   data = BASE._user_order_DBOps.GetRecord(UOID);

        //    BASE._StockApprovalReqd_dbops.Get_Approval_Required()
        //    int AssignMainDeptID = jobdata.Job_AssigneeMainDeptID;
        //    int? AssignSubDeptID = jobdata.Job_AssigneeSubDeptID;
        //    if (BASE._open_User_MainDeptID == AssignMainDeptID || BASE._open_User_SubDeptID == AssignSubDeptID)
        //    {
        //        return Json(new
        //        {
        //            Message = "",
        //            result = true,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            Message = "Not Allowed!!",
        //            result = false,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        #endregion NEVD


        #region UserOrder_Delivery

        public ActionResult LookUp_Get_Item_DL_Requested(DataSourceLoadOptions loadOptions, int? Del_ID)
        {
            if (Del_ID != 0) {
                var UODLItem = BASE._user_order_DBOps.GetUO_Items(Convert.ToInt32(UOID_Glob), true, Convert.ToInt32(Del_ID));

                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UODLItem, loadOptions)), "application/json");

            }
            else
            {
                var UODLItem = BASE._user_order_DBOps.GetUO_Items(Convert.ToInt32(UOID_Glob), true);

                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UODLItem, loadOptions)), "application/json");
            }
        }
        public ActionResult LookUp_Get_DL_Delivery_Mode(DataSourceLoadOptions loadOptions)
        {
            var UODLDELMODE = BASE._user_order_DBOps.GetDeliveryModes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UODLDELMODE, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_DLShipment_Location(DataSourceLoadOptions loadOptions, int UODelMainDeptID = 0, int? UODelSubDeptID = null)
        {
            List<Return_GetLocations> AllLocations = new List<Return_GetLocations>();

            List<Return_GetLocations> UODLLoc = BASE._Stock_Profile_DBOps.GetLocations(UODelMainDeptID);

            AllLocations = UODLLoc;

            if (UODelSubDeptID != null)
            {
                List<Return_GetLocations> Loclistsubdept = BASE._Stock_Profile_DBOps.GetLocations(Convert.ToInt32(UODelSubDeptID));
                List<Return_GetLocations> AllLocationsmainsub = UODLLoc.Union(Loclistsubdept).ToList();
                AllLocations = AllLocationsmainsub.DistinctBy(x => x.Loc_Id).ToList();
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AllLocations, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_DL_Delivered_By(DataSourceLoadOptions loadOptions, int RequestDLStoreID = 0)
        {
            var UODL_Del_By = BASE._user_order_DBOps.GetPersonnels(RequestDLStoreID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UODL_Del_By, loadOptions)), "application/json");
        }

        public ActionResult LookUp_Get_DL_Driver(DataSourceLoadOptions loadOptions)
        {
            var UODL_Driver = BASE._user_order_DBOps.GetPersonnels(0, "Driver");
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UODL_Driver, loadOptions)), "application/json");
        }

        public ActionResult UO_GoodsDeliveredMainGridData(string ActionMethodName, int UOID = 0)
        {
            var UODeliveryList = new List<Return_GetUOGoodsDelivered_MainGrid>();
            var UODeliveryNestedList = new List<Return_GetUOGoodsDelivered_NestedGrid>();
            if (ActionMethodName == "New")
            {
                return PartialView(UODeliveryList);
            }

            if (UO_GoodsDeliveredMainData == null)
            {
                var UOGoodsDeliveredData = BASE._user_order_DBOps.GetUOGoodsDelivered(UOID);

                if (UOGoodsDeliveredData != null)
                {
                    List<Return_GetUOGoodsDelivered_MainGrid> UOGoodsDeliveredMainData = UOGoodsDeliveredData.main_Register;
                    List<Return_GetUOGoodsDelivered_NestedGrid> UOGoodsDeliveredNestedData = UOGoodsDeliveredData.nested_Register;
                    if (UOGoodsDeliveredMainData != null)
                    {
                        UODeliveryList = UOGoodsDeliveredMainData;
                    }
                    if (UOGoodsDeliveredNestedData != null)
                    {
                        UODeliveryNestedList = UOGoodsDeliveredNestedData;
                    }
                    UO_GoodsDeliveredMainData = UODeliveryList;
                    UO_GoodsDeliveredNestedData = UODeliveryNestedList;
                    Session["UO_GoodsDeliveredNestedData"] = UO_GoodsDeliveredNestedData;
                }
            }
            //List<Return_GetUOGoodsDelivered_MainGrid> MastergridUODel_data = UO_GoodsDeliveredMainData as List<Return_GetUOGoodsDelivered_MainGrid>;
            //if (MastergridUODel_data == null)
            //{
            //    return PartialView();
            //}


            return PartialView(UO_GoodsDeliveredMainData);

        }

        public ActionResult UO_GoodsDeliveredNestedGridData(string ActionMethodName, int UOID = 0, int UD_ID = 0, int MainSr = 0)
        {

            List<Return_GetUOGoodsDelivered_NestedGrid> UODeliverynestedList = new List<Return_GetUOGoodsDelivered_NestedGrid>();

            if (UO_GoodsDeliveredNestedData == null)
            {
                var UOGoodsDeliveredData = BASE._user_order_DBOps.GetUOGoodsDelivered(UOID);
                if (UOGoodsDeliveredData != null)
                {
                    List<Return_GetUOGoodsDelivered_MainGrid> UOGoodsDeliveredMainData = UOGoodsDeliveredData.main_Register;
                    List<Return_GetUOGoodsDelivered_NestedGrid> UOGoodsDeliveredNestedData = UOGoodsDeliveredData.nested_Register;
                    UO_GoodsDeliveredMainData = UOGoodsDeliveredMainData;
                    UO_GoodsDeliveredNestedData = UOGoodsDeliveredNestedData;
                  
                }

            }

            List<Return_GetUOGoodsDelivered_NestedGrid> nestedgriddata = UO_GoodsDeliveredNestedData as List<Return_GetUOGoodsDelivered_NestedGrid>;

            List<Return_GetUOGoodsDelivered_NestedGrid> itemlist = new List<Return_GetUOGoodsDelivered_NestedGrid>();
            if (UD_ID == 0)
            {
                itemlist = nestedgriddata.FindAll(x => x.MainSr == MainSr && x.UD_ID == 0);
            }
            else
            {
                itemlist = nestedgriddata.FindAll(x => x.UD_ID == UD_ID);
            }


            return PartialView(itemlist);
        }




        public static GridViewSettings UO_GoodsDeliveredNestedGridSettings(int UD_ID = 0, int Sr = 0)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "UO_Goods_Delivered_Nested_Grid" + UD_ID + Sr;
            settings.SettingsDetail.MasterGridName = "UO_Goods_Delivered_Main_Grid";
            settings.KeyFieldName = "ID";

            settings.Columns.Add("Sr").Visible = true;
            settings.Columns.Add("Make").Visible = true;
            settings.Columns.Add("Model").Visible = true;
            settings.Columns.Add("UDS_Qty").Visible = true;
            settings.Columns.Add("Lot_No").Visible = true;

            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("UD_ID").Visible = false;
            settings.Columns.Add("MainSr").Visible = false;

            // settings.ClientSideEvents.FocusedRowChanged = "OnPurchaseLinkedUserOrderNestedGridFocusedRowChanged";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            //settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for exporting nested grid 

        public static IEnumerable GetDelUO(int UD_ID = 0, int Sr = 0)
        {
            List<Return_GetUOGoodsDelivered_NestedGrid> data = (List<Return_GetUOGoodsDelivered_NestedGrid>)System.Web.HttpContext.Current.Session["UO_GoodsDeliveredNestedData"];
            List<Return_GetUOGoodsDelivered_NestedGrid> itemuodellist = new List<Return_GetUOGoodsDelivered_NestedGrid>();
            if (UD_ID == 0)
            {
                itemuodellist = data.FindAll(x => x.MainSr == Sr && x.UD_ID == 0);
            }
            else
            {
                itemuodellist = data.FindAll(x => x.UD_ID == UD_ID);
            }



            //List<Return_GetUOGoodsDelivered_NestedGrid> itemuodellist = data.FindAll(x => x.UD_ID == UD_ID);
            return itemuodellist;
        }//binding data to nested grid


        public ActionResult UO_GoodsDelivery_Form_Grid(int ItemID = 0, int Rec_ID = 0, int DeliveryID = 0, string ActionMethod = null)
        {
            //UserOrder_Delivery model = new UserOrder_Delivery();

            List<Return_Get_UO_Goods_Delivery_Stocks> Deliveredstocklist = new List<Return_Get_UO_Goods_Delivery_Stocks>();

            if (ItemID != 0)
            {
                var actionmethod = ActionMethod;                
                    Param_Get_UO_Goods_Delivery_Stocks inparam = new Param_Get_UO_Goods_Delivery_Stocks();
                    inparam.UO_ID = Convert.ToInt32(UOID_Glob);
                    inparam.Sub_Item_ID = ItemID;
                if (DeliveryID == 0)
                {
                    inparam.DeliveredRecID = null;
            }
            else
            {
                inparam.DeliveredRecID = DeliveryID;
            }
            inparam.Rec_ID = Rec_ID; //item requested id
                    ViewData["Rec_ID"] = Rec_ID;

                    ViewData["ItemID"] = ItemID;
                


                    List<Return_Get_UO_Goods_Delivery_Stocks> UODeliveredStockData = BASE._user_order_DBOps.Get_UO_Goods_Delivery_Stocks(inparam);
                    if (UODeliveredStockData != null)
                    {
                        Deliveredstocklist = UODeliveredStockData;
                    }

                if (actionmethod == "Edit" || actionmethod == "View")
                {
                    //if (DeliveryID == 0)
                    //{
                        List<Return_GetUOGoodsDelivered_NestedGrid> nestedgriddata = UO_GoodsDeliveredNestedData as List<Return_GetUOGoodsDelivered_NestedGrid>;

                        List<Return_GetUOGoodsDelivered_NestedGrid> itemlist = new List<Return_GetUOGoodsDelivered_NestedGrid>();

                    if (DeliveryID == 0)
                    {
                        itemlist = nestedgriddata.FindAll(x => x.UD_ID == 0);
                    }

                    else
                    {
                        itemlist = nestedgriddata.FindAll(x => x.UD_ID == DeliveryID);
                    }


                        for (int I = 0; I <= UODeliveredStockData.Count() - 1; I++)
                        {
                            for (int j = 0; j <= itemlist.Count() - 1; j++)
                            {
                                if (UODeliveredStockData[I].ID == itemlist[j].StockRecordID)
                                {
                                    UODeliveredStockData[I].Del_qty = itemlist[j].UDS_Qty;
                                }
                            }
                        }


                    //}
                    

                }
                UODeliveredStockGridData = Deliveredstocklist;               
            }
            else
            {
                List<Return_Get_UO_Goods_Delivery_Stocks> grid_data = UODeliveredStockGridData as List<Return_Get_UO_Goods_Delivery_Stocks>;
                if (grid_data == null)
                {
                    return PartialView();
                }

            }
            //var finalData = UODeliveredStockGridData as List<Return_Get_UO_Goods_Delivery_Stocks>;



            return PartialView(UODeliveredStockGridData);


        }

        //public ActionResult Delivery_inner_grid_min_purchasedate()
        //{
        //    var all_data_Of_inner_Delivery_Grid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;

        //    DateTime? date = null;

        //    for (int I = 0; I <= all_data_Of_inner_Delivery_Grid.Count() - 1; I++)
        //    {
        //        if (all_data_Of_inner_Delivery_Grid[I].Del_qty > 0)
        //        {
        //            if (date == null)
        //            {
        //                date = all_data_Of_inner_Delivery_Grid[I].PurchaseDate;
        //            }
        //            else
        //            {
        //                   int value = DateTime.Compare(all_data_Of_inner_Delivery_Grid[I].PurchaseDate, Convert.ToDateTime(date));
        //                if (value < 0)
        //                {
                         
        //                    date = all_data_Of_inner_Delivery_Grid[I].PurchaseDate;
        //                }
        //            }
        //        }
        //    }
        //    var mindate = date;


        //    return Json(new { mindate = date, result = true }, JsonRequestBehavior.AllowGet);
        //}

              

     
        public ActionResult DataNavigationDelivery(string ActionMethod = null, int ID = 0)
        {

            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                //var Sr = Convert.ToInt16(SR);
                //var all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                //var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();

                if (ID != 0)
                {
                    var receiptcount = BASE._user_order_DBOps.GetUODelivery_Received_EntryCount(Convert.ToInt32(ID));
                    if (receiptcount > 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "Delivery against which receipt has been posted can not be Edited/ Deleted"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "success", result = true }, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }
            }

            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delivery_grid_oldest_item(string[] UO_ItemID, int UOid = 0)
        {
            var uoid = UOid;
            if (uoid != 0)
            {
                UOID_Glob = uoid;
            }
            List<Return_GetUO_Items> UOGRItem = BASE._user_order_DBOps.GetUO_Items(Convert.ToInt32(UOID_Glob), true);


            if (UO_ItemID != null)
            {
                if (UO_ItemID.Length == 1)
                {
                    List<Return_GetUO_Items> selecteditem = UOGRItem.FindAll(x => x.ID.ToString() == UO_ItemID[0].ToString());


                    if (selecteditem.Count == 0)
                    {
                        return Json(new
                        {
                            message = "Item not saved/Approved Quantity of Item is zero/ Full Delivery for Selected Item is already posted...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        var mindate = selecteditem.Min(x => x.Added_On);
                        var itemidj = selecteditem.FindAll(x => x.Added_On.Equals(mindate)).FirstOrDefault();
                        var itemid = itemidj.ID;
                        return Json(new { selID = itemid, result = true }, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }

            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Deliver ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Frm_User_Order_Delivery(string ActionMethod = null, int SR = 0, int UOid = 0, string PostSuccessFunction = null, string PopupID = null, int Item_Rec_Id = 0)
        {
            ViewData["UO_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            //session clear
            UODeliveryStockGridSessionclear();
              UserOrder_Delivery model = new UserOrder_Delivery();
            model.DL_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "Frm_User_Order_Delivered_OnSuccess";
            model.DL_PopUPId = PopupID != null ? PopupID : "UserOrderinnergrid";

            if (UOid != 0)
            {
                model.UOid = UOid;
            }
            if (model.UOid != 0)
            {
                UOID_Glob = UOid;
            }
            model.UO_DL_ActionMethod = ActionMethod;

            if (ActionMethod == "New")
            {
                
                model.UO_DL_Delivery_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm")); //mantis bug 997
                //model.UO_DL_Item_RequestedId = ItemID;
                model.UO_DL_Delivered_By_Id = BASE._open_User_PersonnelID;
                //  model.Grid_GoodsDeliveryData = UODeliveredStockGridData;
                //  return PartialView(Deliveredstocklist);

                if (Item_Rec_Id != 0)
                {
                    model.UO_DL_Item_RequestedId = Item_Rec_Id;
                }



            }

            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();
                var subitem = dataToEdit.SubItem_ID;
                model.UO_DL_Sr = Sr;
                model.UO_DL_Item_Name = dataToEdit.Item_Name;
                //model.UO_DL_Item_Code = dataToEdit.Item_Code;
                // model.UO_DL_Item_Type = dataToEdit.Item_Type;
                //model.UO_DL_Head = dataToEdit.Head;
                //model.UO_DL_Make = dataToEdit.Make;
                //model.UO_DL_ModelName = dataToEdit.Model;
                model.UO_DL_Delivered_Qty = dataToEdit.Delivered_Qty;

                //model.UO_DL_Lot_No = dataToEdit.Lot_No;
                model.UO_DL_Delivery_Date = dataToEdit.Delivery_Date;
                model.UO_DL_Delivery_Mode = dataToEdit.Delivery_Mode;
                model.UO_DL_Carrier = dataToEdit.Carrier;
                model.UO_DL_Shipment_To_Location = dataToEdit.Delivery_Location;
                model.UO_DL_Remarks = dataToEdit.Remarks;
                model.UO_DL_Driver = dataToEdit.Driver;
                model.UO_DL_Vehicle_no = dataToEdit.Vehicle_No;
                model.UO_DL_Delivery_ModeId = dataToEdit.ModeID;
                model.UO_DL_Shipment_To_LocationId = dataToEdit.DeliveryLocationId;
                model.UO_DL_Item_RequestedId = dataToEdit.ItemRequestedID;
                model.UO_DL_Delivered_By_Id = dataToEdit.DeliveredByID;
                model.UO_DL_DriverID = dataToEdit.DriverID;
                //model.UO_DL_StockRecordID = dataToEdit.StockRecordID;
                model.UO_DL_Received_By = dataToEdit.Receiver_Name;
                model.UO_DL_ID = dataToEdit.ID;
                model.UO_DL_Required_Delivery_Date = dataToEdit.Required_Delivery_Date;
               model.UO_DL_Scheduled_Delivery_Date = dataToEdit.Scheduled_Delivery_Date;
                //Session["DeliveryID"] = dataToEdit.ID;-for edit 
                //Session["UOItemID"] = 5;
                // model.Grid_GoodsDeliveryData = UODeliveredStockGridData;
                // model.UO_DL_StockRecordID = dataToEdit.StockRecordID;
                var MainGridID = dataToEdit.ID;


                List<Return_Get_UO_Goods_Delivery_Stocks> Deliveredstockgriddata = new List<Return_Get_UO_Goods_Delivery_Stocks>();



                var all_data_nested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
                List<Return_GetUOGoodsDelivered_NestedGrid> dataToEditnested = new List<Return_GetUOGoodsDelivered_NestedGrid>();
                if (all_data_nested != null)
                {
                    dataToEditnested = all_data_nested.FindAll(x => x.MainSr == Sr);
                }
                if (dataToEditnested != null)
                {

                    for (int I = 0; I <= dataToEditnested.Count() - 1; I++)
                    {
                        Return_Get_UO_Goods_Delivery_Stocks delstockgrid = new Return_Get_UO_Goods_Delivery_Stocks();


                        // delstockgrid.Avlb_Qty = dataToEditnested[I].av;
                        delstockgrid.Del_qty = dataToEditnested[I].UDS_Qty;
                        delstockgrid.Sub_Item_ID = dataToEditnested[I].SubItem_ID;
                        delstockgrid.Stock_Lot_Serial_no = dataToEditnested[I].Lot_No;
                        delstockgrid.Make = dataToEditnested[I].Make;
                        delstockgrid.Model = dataToEditnested[I].Model;
                        delstockgrid.Stk_Location = dataToEditnested[I].Stk_Location;
                        delstockgrid.StockUnit = dataToEditnested[I].Unit;
                        delstockgrid.ID = dataToEditnested[I].StockRecordID;
                        delstockgrid.UD_ID = dataToEditnested[I].UD_ID;
                        // delstockgrid.UDS_ID = dataToEditnested[I].Tax_Amount;
                        Deliveredstockgriddata.Add(delstockgrid);
                        UODeliveredStockGridData = Deliveredstockgriddata;



                    }


                }


            }
            return PartialView(model);
        }


        public ActionResult OnChangeDelivery(Decimal DelQty = 0, int ID = 0)
        {

            decimal sum = 0;
            string lotno = null;
            string location = null;
            var all_data_Of_delivery_form_Grid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;
            if (all_data_Of_delivery_form_Grid != null)
            {

                for (int I = 0; I <= all_data_Of_delivery_form_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_delivery_form_Grid[I].ID == ID)
                    {
                        all_data_Of_delivery_form_Grid[I].Del_qty = DelQty;
                    }
                }
            }
            var all_data_Of_delivery_Int_Grid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;
            if (all_data_Of_delivery_Int_Grid != null)
            {
                for (int I = 0; I <= all_data_Of_delivery_Int_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_delivery_Int_Grid[I].Del_qty > 0)
                    {
                        sum = sum + all_data_Of_delivery_Int_Grid[I].Del_qty;
                        if (all_data_Of_delivery_Int_Grid[I].Stock_Lot_Serial_no != null)
                        {
                            lotno = lotno + all_data_Of_delivery_Int_Grid[I].Stock_Lot_Serial_no + ',';
                        }
                        if (all_data_Of_delivery_Int_Grid[I].Stk_Location != null)
                        {
                            location = location + all_data_Of_delivery_Int_Grid[I].Stk_Location + ',';
                        }
                    }
                }

            }

            return Json(new
            {
                sum = sum,
                lotno = lotno,
                location = location,

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnDeliveryLoad()
        {

            decimal sum = 0;
            string lotno = null;
            string location = null;
            var all_data_Of_delivery_form_Grid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;
            if (all_data_Of_delivery_form_Grid != null)
            {

                for (int I = 0; I <= all_data_Of_delivery_form_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_delivery_form_Grid[I].Del_qty > 0)
                    {
                        sum = sum + all_data_Of_delivery_form_Grid[I].Del_qty;
                        if (all_data_Of_delivery_form_Grid[I].Stock_Lot_Serial_no != null)
                        {
                            lotno = lotno + all_data_Of_delivery_form_Grid[I].Stock_Lot_Serial_no + ',';
                        }
                        if (all_data_Of_delivery_form_Grid[I].Stk_Location != null)
                        {
                            location = location + all_data_Of_delivery_form_Grid[I].Stk_Location + ',';
                            //mantis bug 639
                        }

                    }
                }

            }

            return Json(new
            {
                sum = sum,
                lotno = lotno,
                location = location,

            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Frm_User_Order_Delivery(UserOrder_Delivery model)
        {
            if (model.UO_DL_ActionMethod == "New" | model.UO_DL_ActionMethod == "Edit")
            {
                var all_data_Of_inner_Delivery_Grid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;

                DateTime? date = null;
                var resultvalue = 0;
                for (int I = 0; I <= all_data_Of_inner_Delivery_Grid.Count() - 1; I++)
                {
                    if (all_data_Of_inner_Delivery_Grid[I].Del_qty > 0)
                    {
                        if (date == null)
                        {
                            date = all_data_Of_inner_Delivery_Grid[I].PurchaseDate;
                        }
                        else
                        {
                            resultvalue  = DateTime.Compare(all_data_Of_inner_Delivery_Grid[I].PurchaseDate, Convert.ToDateTime(date));
                            if (resultvalue < 0)
                            {

                                date = all_data_Of_inner_Delivery_Grid[I].PurchaseDate;
                            }
                        }
                    }
                }
                model.UO_DL_StockPurchaseDate = Convert.ToDateTime(date);


                if (model.UO_DL_Delivered_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivered Qty can not be Negative or Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UO_DL_Delivered_Qty > model.UO_DL_Pending_Qty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivered Qty can not be greater than Pending Qty ...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UO_DL_partialdelivery == "false")
                {

                    if (model.UO_DL_Approved_Qty != model.UO_DL_Delivered_Qty) //mantis #1577
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Partial Delivery is Not allowed for the selected item ...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                int value = DateTime.Compare(model.UO_DL_Delivery_Date, model.UO_DL_StockPurchaseDate);
                if (value < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivery Date should not be earlier then the Date of Purchase of delivered Stock ...!"
                    }, JsonRequestBehavior.AllowGet);
                } //mantis 1546,issue 3


                if (model.UO_DL_Delivery_ModeId == null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Please Select Delivery mode..!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.UOid != 0)
            {
                try
                {
                    Param_Insert_UO_Item_Delivered inparam = new Param_Insert_UO_Item_Delivered();


                    inparam.UO_ID = model.UOid;
                    inparam.UO_Item_ID = Convert.ToInt32(model.UO_DL_Item_RequestedId);
                    inparam.Delivered_Qty = model.UO_DL_Delivered_Qty;
                    inparam.Delivery_Date = model.UO_DL_Delivery_Date;
                    inparam.Delivery_Mode_ID = model.UO_DL_Delivery_ModeId;
                    inparam.Delivery_Carrier = model.UO_DL_Carrier;
                    inparam.Delivery_Location_ID = model.UO_DL_Shipment_To_LocationId;
                    inparam.Delivered_By_ID = model.UO_DL_Delivered_By_Id;
                    inparam.Delivery_Driver_ID = model.UO_DL_DriverID;
                    inparam.Delivery_Receiver_Name = model.UO_DL_Received_By;
                    inparam.Delivery_Remarks = model.UO_DL_Remarks;
                    inparam.VehicleNo = model.UO_DL_Vehicle_no;

                    List<Return_Get_UO_Goods_Delivery_Stocks> alldeliveryinnergriddata = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;
                    var insertNestedDeli = new Param_Insert_UO_Item_Delivered_Stocks[alldeliveryinnergriddata.Count()];
                    if (alldeliveryinnergriddata != null)
                    {

                        for (int I = 0; I <= alldeliveryinnergriddata.Count() - 1; I++)
                        {
                            Param_Insert_UO_Item_Delivered_Stocks upparam = new Param_Insert_UO_Item_Delivered_Stocks();

                            if (alldeliveryinnergriddata[I].Del_qty > 0)
                            {
                                upparam.UD_ID = 0;
                                upparam.UO_ID = model.UOid;
                                upparam.UDS_Qty = alldeliveryinnergriddata[I].Del_qty;
                                upparam.Stock_ID = alldeliveryinnergriddata[I].ID;
                                insertNestedDeli[I] = upparam;

                            }
                        }
                    }

                    inparam._Delivered_Stock = insertNestedDeli;

                    if (BASE._user_order_DBOps.Insert_UO_Item_Delivered(inparam))
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
                List<Return_GetUOGoodsDelivered_MainGrid> gridRows = new List<Return_GetUOGoodsDelivered_MainGrid>();
                List<Return_GetUOGoodsDelivered_NestedGrid> gridRowsNested = new List<Return_GetUOGoodsDelivered_NestedGrid>();

                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (UO_GoodsDeliveredMainData != null)
                {
                    gridRows = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;

                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;

                }
                if (UO_GoodsDeliveredNestedData != null)
                {
                    gridRowsNested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
                }

                if (model.UO_DL_ActionMethod == "New")
                {
                    Return_GetUOGoodsDelivered_MainGrid grid = new Return_GetUOGoodsDelivered_MainGrid();
                    grid.Sr = NewSr;
                    grid.Item_Name = model.UO_DL_Item_Name;
                    grid.Item_Code = model.UO_DL_Item_Code;
                    grid.Item_Type = model.UO_DL_Item_Type;
                    grid.Head = model.UO_DL_Head;
                    //grid.Make = model.UO_DL_Make;
                    //grid.Model = model.UO_DL_ModelName;
                    grid.Delivered_Qty = model.UO_DL_Delivered_Qty;
                    grid.SubItem_ID = model.UO_DL_SubItemID;
                    grid.Required_Delivery_Date = model.UO_DL_Required_Delivery_Date;
                    grid.Scheduled_Delivery_Date = model.UO_DL_Scheduled_Delivery_Date;
                    //grid.Lot_No = model.UO_DL_Lot_No;
                    grid.Delivery_Date = model.UO_DL_Delivery_Date;
                    grid.Delivery_Mode = model.UO_DL_Delivery_Mode;
                    grid.ModeID = model.UO_DL_Delivery_ModeId;
                    grid.Carrier = model.UO_DL_Carrier;
                    grid.Delivery_Location = model.UO_DL_Shipment_To_Location;
                    grid.DeliveryLocationId = model.UO_DL_Shipment_To_LocationId;
                    grid.Remarks = model.UO_DL_Remarks;
                    grid.Driver = model.UO_DL_Driver;
                    grid.Vehicle_No = model.UO_DL_Vehicle_no;
                    //grid.StockRecordID = model.UO_DL_StockRecordID;
                    grid.Receiver_Name = model.UO_DL_Received_By;
                    grid.ItemRequestedID = Convert.ToInt32(model.UO_DL_Item_RequestedId);
                    grid.DeliveredByID = model.UO_DL_Delivered_By_Id;
                    grid.DriverID = model.UO_DL_DriverID;
                    grid.ID = 0;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;

                    gridRows.Add(grid);



                    //var gridRowsNestedCount = 0;
                    //var LastRowNSr = 0;
                    //var NNewSr = LastRowNSr + 1;
                    //if (UO_GoodsDeliveredNestedData != null)
                    //{

                    //    gridRowsNested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
                    //    gridRowsNestedCount = gridRowsNested.Count;
                    //    LastRowNSr = gridRowsNestedCount > 0 ? Convert.ToInt32(gridRows[gridRowsNestedCount - 1].Sr) : 0;
                    //    NNewSr = LastRowNSr + 1;


                    //    List<Return_GetUOGoodsDelivered_NestedGrid> nestedgriddata = new List<Return_GetUOGoodsDelivered_NestedGrid>();
                    // Return_Get_UO_Goods_Delivery_Stocks GridView1 = new Return_Get_UO_Goods_Delivery_Stocks();
                    List<Return_Get_UO_Goods_Delivery_Stocks> alldatadeliverygrid = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;



                    if (alldatadeliverygrid != null)
                    {

                        for (int I = 0; I <= alldatadeliverygrid.Count() - 1; I++)
                        {
                            Return_GetUOGoodsDelivered_NestedGrid nestedgrid = new Return_GetUOGoodsDelivered_NestedGrid();

                            if (alldatadeliverygrid[I].Del_qty > 0)
                            {
                                nestedgrid.UD_ID = 0;
                                nestedgrid.Sr = NewSr;  // it should be alldatadeliverygrid[I].sr; 
                                nestedgrid.UDS_Qty = alldatadeliverygrid[I].Del_qty;
                                nestedgrid.StockRecordID = alldatadeliverygrid[I].ID;
                                nestedgrid.Make = alldatadeliverygrid[I].Make;
                                nestedgrid.Model = alldatadeliverygrid[I].Model;
                                nestedgrid.Lot_No = alldatadeliverygrid[I].Stock_Lot_Serial_no;
                                nestedgrid.Unit = alldatadeliverygrid[I].StockUnit;
                                nestedgrid.Stk_Location = alldatadeliverygrid[I].Stk_Location;
                                nestedgrid.MainSr = NewSr;
                                nestedgrid.SubItem_ID = alldatadeliverygrid[I].Sub_Item_ID; //itshould be subitem id
                                nestedgrid.Unit = alldatadeliverygrid[I].StockUnit;                            //    nestedgrid.Unit = GridViewitem.Unit;
                              

                                gridRowsNested.Add(nestedgrid);
                            }


                        }


                    }
                }


                else if (model.UO_DL_ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.UO_DL_Sr);
                    if (dataToEdit.ID != 0)
                    {
                        var receiptcount = BASE._user_order_DBOps.GetUODelivery_Received_EntryCount(dataToEdit.ID);
                        if (receiptcount > 0)
                        {

                            return Json(new
                            {
                                result = false,
                                message = "Delivery against which receipt has been posted can not be edited"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.UO_DL_ID != 0)
                    {
                        var editUOGoodsDelID = new ArrayList();
                        var editGoodsDel = UOEdit_GoodsDelivered_ID as ArrayList;
                        if (editGoodsDel != null)
                        {
                            editGoodsDel.Add(model.UO_DL_ID);
                            UOEdit_GoodsDelivered_ID = editGoodsDel;
                        }
                        else
                        {
                            editUOGoodsDelID.Add(model.UO_DL_ID);
                            UOEdit_GoodsDelivered_ID = editUOGoodsDelID;
                        }
                    }

                    dataToEdit.Item_Name = model.UO_DL_Item_Name;
                    dataToEdit.Item_Code = model.UO_DL_Item_Code;
                    dataToEdit.Item_Type = model.UO_DL_Item_Type;
                    dataToEdit.Head = model.UO_DL_Head;
                    //dataToEdit.Make = model.UO_DL_Make;
                    //dataToEdit.Model = model.UO_DL_ModelName;
                    dataToEdit.Delivered_Qty = model.UO_DL_Delivered_Qty;
                    //dataToEdit.Unit = model.UO_DL_Unit;
                    //dataToEdit.Lot_No = model.UO_DL_Lot_No;
                    dataToEdit.SubItem_ID = model.UO_DL_SubItemID;
                    dataToEdit.Delivery_Date = model.UO_DL_Delivery_Date;
                    dataToEdit.Delivery_Mode = model.UO_DL_Delivery_Mode;
                    dataToEdit.ModeID = model.UO_DL_Delivery_ModeId;
                    dataToEdit.Carrier = model.UO_DL_Carrier;
                    dataToEdit.Delivery_Location = model.UO_DL_Shipment_To_Location;
                    dataToEdit.DeliveryLocationId = model.UO_DL_Shipment_To_LocationId;
                    dataToEdit.Remarks = model.UO_DL_Remarks;
                    dataToEdit.Required_Delivery_Date = model.UO_DL_Required_Delivery_Date;
                    dataToEdit.Scheduled_Delivery_Date = model.UO_DL_Scheduled_Delivery_Date;
                    dataToEdit.Driver = model.UO_DL_Driver;
                    dataToEdit.Vehicle_No = model.UO_DL_Vehicle_no;

                    dataToEdit.ItemRequestedID = Convert.ToInt32(model.UO_DL_Item_RequestedId);
                    dataToEdit.DeliveredByID = model.UO_DL_Delivered_By_Id;
                    dataToEdit.DriverID = model.UO_DL_DriverID;

                    //  dataToEdit.StockRecordID = model.UO_DL_StockRecordID;
                    dataToEdit.Receiver_Name = model.UO_DL_Received_By;
                    //var gridRowsNestedCount = 0;
                    //var LastRowNSr = 0;
                    //var NNewSr = LastRowNSr + 1;
                    //if (UO_GoodsDeliveredNestedData != null)
                    //{

                    //    gridRowsNested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
                    //    gridRowsNestedCount = gridRowsNested.Count;
                    //    LastRowNSr = gridRowsNestedCount > 0 ? Convert.ToInt32(gridRows[gridRowsNestedCount - 1].Sr) : 0;
                    //    NNewSr = LastRowNSr + 1;
                    //}
                    //     var datatoeditnested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData ;
                    //var datatoeditnested = gridRowsNested.FindAll(x => x.Sr == model.UO_DL_Sr);
                    //List<Return_GetUOGoodsDelivered_NestedGrid> nestedgriddata = UO_GoodsDeliveredNestedData as List<Return_GetUOGoodsDelivered_NestedGrid>;


                    List<Return_GetUOGoodsDelivered_NestedGrid> datatoeditnested = new List<Return_GetUOGoodsDelivered_NestedGrid>();
                    if (gridRowsNested != null)
                    {
                        datatoeditnested = gridRowsNested.FindAll(x => x.MainSr == model.UO_DL_Sr);
                    }

                    if (datatoeditnested != null)
                    {

                        for (int I = 0; I <= datatoeditnested.Count() - 1; I++)
                        {

                            gridRowsNested.Remove(datatoeditnested[I]);


                        }



                        List<Return_Get_UO_Goods_Delivery_Stocks> formgriddataedited = (List<Return_Get_UO_Goods_Delivery_Stocks>)UODeliveredStockGridData;


                        //
                        //        int flag = 0;
                        //        for (int j = 0; j <= itemlist.Count() - 1; j++)
                        //        {
                        //            if (formgriddataedited[I].ID == itemlist[j].StockRecordID)
                        //            {
                        //                if (formgriddataedited[I].Del_qty > 0)
                        //                {
                        //                    itemlist[j].UDS_Qty = formgriddataedited[I].Del_qty;
                        //                }
                        //                else
                        //                {
                        //                    gridRowsNested.Remove(itemlist[j]);

                        //                }
                        //                flag = 1;
                        //                break;
                        //            }
                        //        }
                        //if (flag == 0)
                        //{

                        if (formgriddataedited != null)
                        {
                            for (int I = 0; I <= formgriddataedited.Count() - 1; I++)
                            {
                                Return_GetUOGoodsDelivered_NestedGrid nestedgrid = new Return_GetUOGoodsDelivered_NestedGrid();

                                if (formgriddataedited[I].Del_qty > 0)
                                {
                                nestedgrid.UD_ID = model.UO_DL_ID;
                                nestedgrid.Sr = model.UO_DL_Sr;
                                nestedgrid.UDS_Qty = formgriddataedited[I].Del_qty;
                                nestedgrid.StockRecordID = formgriddataedited[I].ID;
                                nestedgrid.Make = formgriddataedited[I].Make;
                                nestedgrid.Model = formgriddataedited[I].Model;
                                nestedgrid.Lot_No = formgriddataedited[I].Stock_Lot_Serial_no;
                                nestedgrid.Unit = formgriddataedited[I].StockUnit;
                                nestedgrid.MainSr = model.UO_DL_Sr;
                                nestedgrid.SubItem_ID = formgriddataedited[I].Sub_Item_ID; //itshould be subitem id
                                                                                           // nestedgrid.ID = formgriddataedited[I].ID;     
                                    nestedgrid.Stk_Location = formgriddataedited[I].Stk_Location;
                                    //    nestedgrid.Unit = GridViewitem.Unit;
                                    gridRowsNested.Add(nestedgrid);
                                }

                            }



                        }
                    }

                }





                UO_GoodsDeliveredMainData = gridRows;
                UO_GoodsDeliveredNestedData = gridRowsNested;
                return Json(new
                {
                    result = true,
                    message = "Updated Successfully"
                }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult All_Saved_Delivered_Item_Exists_in_Grid(int Rec_ID = 0, int uoid = 0, int srno = 0) //for saved rows
        {
            var ID = uoid;
            var SrNO = srno;
            if (uoid != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var all_data_Of_Delivery_Grid = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;

                if (all_data_Of_Delivery_Grid != null)
                {
                    if (all_data_Of_Delivery_Grid.Count != 0)
                    {

                        List<Return_GetUOGoodsDelivered_MainGrid> datatocheck = all_data_Of_Delivery_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);
                        List<Return_GetUOGoodsDelivered_MainGrid> datatocheckedit = all_data_Of_Delivery_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);

                        List<Return_GetUOGoodsDelivered_MainGrid> datatocheckdelete = all_data_Of_Delivery_Grid.FindAll(x => x.ID != 0);

                        if (datatocheck.Count != 0)
                        {
                            for (int I = 0; I <= datatocheck.Count() - 1; I++)
                            {

                                if (datatocheck[I].ItemRequestedID == Rec_ID)
                                {

                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit existing row." //mantis bug 943
                                    }, JsonRequestBehavior.AllowGet);

                                }
                            }
                        }

                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].ItemRequestedID == Rec_ID)
                                {

                                    if (UOEdit_GoodsDelivered_ID != null)
                                    {
                                        return Json(new
                                        {
                                            result = false,
                                            message = "Edited saved row for this item is already in Grid, please save that row first to edit this row."
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].ItemRequestedID == Rec_ID)
                                {

                                    if (Delete_User_GoodsDelivered_ID != null)
                                    {
                                        var deletedID = DeletedItemRequested_ID as ArrayList;//mantis bug 1225
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].ItemRequestedID == Convert.ToInt32(deletedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save UO first for any Addition/Updation."
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
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Check_Unsaved_Item_Entry_in_ItemOrdered_Grid_Delivery_Grid(int Rec_ID = 0) //delivery id to be changed
        {


            var all_data_Of_ItemOrd_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
            if (all_data_Of_ItemOrd_Grid != null)
            {

                if (all_data_Of_ItemOrd_Grid.Count != 0)
                {
                    List<Return_GetUO_Items> datatocheckItem = all_data_Of_ItemOrd_Grid.FindAll(x => x.ID == Rec_ID && x.ID != 0); //to be tested


                    if (datatocheckItem.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckItem.Count() - 1; I++)
                        {

                            if (UOEdit_ItemOrd_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Item Ordered Grid, please save UO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }

                }


            }
            if (Delete_User_OrderItem_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in Item Ordered Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }


            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_Unsaved_Item_Entry_in_Recd_and_RetRecd_Grid(int ID_Delivery = 0)
        {


            var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReceived> datatocheckrecd = all_data_Of_Received_Grid.FindAll(x => x.DeliveryEntryID == ID_Delivery && x.ID == 0);


                    if (datatocheckrecd.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in  Received Grid, please save it to Edit/Delete this row."
                        }, JsonRequestBehavior.AllowGet);


                    }
                }


            }



            var all_data_Of_Returnedrecd_Grid = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
            if (all_data_Of_Returnedrecd_Grid != null)
            {

                if (all_data_Of_Returnedrecd_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReturnReceived> datatocheckretrecd = all_data_Of_Returnedrecd_Grid.FindAll(x => x.DeliveryEntryID == ID_Delivery && x.ID == 0);


                    if (datatocheckretrecd.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in Return Received Grid, please save it to Edit/Delete this row."
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

        public ActionResult Frm_GoodsDel_DetailWin_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Goods_Del_Id = 0)
        {


            var allSrcDataGR = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
            List<Return_GetUOGoodsDelivered_NestedGrid> all_data_Of_Nested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
            var dataToDeletenested = new List<Return_GetUOGoodsDelivered_NestedGrid>();

            var dataToDelete = allSrcDataGR != null ? allSrcDataGR.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();

            if (all_data_Of_Nested != null)
            {
                if (dataToDelete.ID != 0)
                {
                    dataToDeletenested = all_data_Of_Nested.FindAll(x => x.UD_ID == Goods_Del_Id);
                }
                else
                {
                    dataToDeletenested = all_data_Of_Nested.FindAll(x => x.Sr == SrID);
                }
            }


            var del_ItemReq_id = dataToDelete.ItemRequestedID;
            if (dataToDelete.ID != 0)
            {
                var receiptcount = BASE._user_order_DBOps.GetUODelivery_Received_EntryCount(dataToDelete.ID);
                if (receiptcount > 0)
                {

                    return Json(new
                    {
                        result = false,
                        message = "Delivery against which receipt has been posted can not be deleted"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allSrcDataGR != null)
            {
                allSrcDataGR.Remove(dataToDelete);
            }

            for (int i = 0; i <= dataToDeletenested.Count() - 1; i++)
            {
                all_data_Of_Nested.Remove(dataToDeletenested[i]);
            }


            ViewData["UO_GoodsDeliveredData"] = allSrcDataGR;
            UO_GoodsDeliveredNestedData = all_data_Of_Nested;
            var deleteDeliveryID = new ArrayList();
            var deleteItemRequestedID = new ArrayList();

            if (Goods_Del_Id != 0) {

                var deleteItemRequested = DeletedItemRequested_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item
                var deleteDelivery = Delete_User_GoodsDelivered_ID as ArrayList;
                if (deleteDelivery != null)
                {
                    deleteDelivery.Add(Goods_Del_Id);
                    Delete_User_GoodsDelivered_ID = deleteDelivery;

                    deleteItemRequested.Add(del_ItemReq_id);
                    DeletedItemRequested_ID = deleteItemRequested;
                }
                else
                {
                    deleteDeliveryID.Add(Goods_Del_Id);
                    Delete_User_GoodsDelivered_ID = deleteDeliveryID;

                    deleteItemRequestedID.Add(del_ItemReq_id);
                    DeletedItemRequested_ID = deleteItemRequestedID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UserOrder_DeliveryAll


        [HttpGet]
        public ActionResult Frm_User_Order_Delivery_All(string ActionMethod = null, int SR = 0, int UOid = 0, string[] UO_ItemID = null)
        {
            ViewData["UO_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            UserOrder_Delivery_All model = new UserOrder_Delivery_All();
            model.UOid = UOid;

            model.UO_DLA_ActionMethod = "New";

            if (ActionMethod == "New")
            {
                model.UO_DLA_Delivery_Date = DateTime.Now;
                model.UO_DLA_Delivered_By_Id = BASE._open_User_PersonnelID;
                String uoitemid = null;
                if (UO_ItemID != null)
                {
                    for (int i = 0; i < UO_ItemID.Length; i++)
                    {
                        if (uoitemid == null)
                        {
                            uoitemid = UO_ItemID[i].ToString();
                        }
                        else
                        {
                            uoitemid = uoitemid + ',' + UO_ItemID[i].ToString();
                        }

                    }
                }
                model.UO_DeliverySelected_Item_ID = uoitemid;

            }

            return View(model);

        }
        //public ActionResult UpdateValue(int ke)


        [HttpPost]
        public ActionResult Frm_User_Order_Delivery_All(UserOrder_Delivery_All model)
        {


            if (model.UOid != 0)
            {
                List<Return_GetUO_Items> UOGRItem = BASE._user_order_DBOps.GetUO_Items(model.UOid, true);
                if (UOGRItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivery for all the items are already made/Items not Approved...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    // Param_Insert_UO_Item_Delivered inparam = new Param_Insert_UO_Item_Delivered();

                    // Param_Insert_UO_Item_Delivered_Stocks upparam = new Param_Insert_UO_Item_Delivered_Stocks();


                    Param_GetUOGoodsDeliverAllPending param = new Param_GetUOGoodsDeliverAllPending();

                    param.Delivery_Date = model.UO_DLA_Delivery_Date;
                    param.Delivery_Mode_ID = model.UO_DLA_Delivery_ModeId;
                    param.Delivery_Carrier = model.UO_DLA_Carrier;
                    param.Delivery_Location_ID = model.UO_DLA_Shipment_To_LocationId;
                    param.Delivered_By_ID = model.UO_DLA_Delivered_By_Id;
                    param.Delivery_Driver_ID = model.UO_DLA_DriverID;
                    param.Delivery_Receiver_Name = model.UO_DLA_Received_By;
                    param.Delivery_Remarks = model.UO_DLA_Remarks;
                    param.VehicleNo = model.UO_DLA_Vehicle_no;
                    param.UO_ID = model.UOid;


                    if (BASE._user_order_DBOps.InsertUOGoodsDeliverAllPending(param))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = "No More Pending Ordered Item to be Delivered/ Stocks not available" }, JsonRequestBehavior.AllowGet);
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
                List<Return_GetUO_Items> UOGRItem = BASE._user_order_DBOps.GetUO_Items(Convert.ToInt32(UOID_Glob), true);
                if (UOGRItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivery for all the items are already made/ Items not Approved...!"
                    }, JsonRequestBehavior.AllowGet);
                }

                List<Return_GetUOGoodsDelivered_MainGrid> all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                Return_GetUOGoodsDelivered_MainGrid dataToEdit = new Return_GetUOGoodsDelivered_MainGrid();
                if (all_data != null)
                {
                    dataToEdit = all_data.Where(x => x.ID == 0).FirstOrDefault();
                }

                if (dataToEdit != null || UOEdit_GoodsDelivered_ID != null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Pls. save the existing delivered enteries in the grid first...!"
                    }, JsonRequestBehavior.AllowGet);

                }
                else if (dataToEdit == null && UOEdit_GoodsDelivered_ID == null)
                {
                    List<Return_GetUOGoodsDelivered_NestedGrid> all_datanested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;
                    Return_GetUOGoodsDelivered_NestedGrid dataToEditnested = new Return_GetUOGoodsDelivered_NestedGrid();
                    if (all_datanested != null)
                    {
                        dataToEditnested = all_datanested.Where(x => x.ID == 0).FirstOrDefault();
                    }

                    if (dataToEditnested != null)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Pls. save the existing delivered enteries first...!"
                        }, JsonRequestBehavior.AllowGet);

                    }
                }



                if (model.UO_DLA_ActionMethod == "New")
                {

                    if (model.UO_DeliverySelected_Item_ID != null)
                    {
                        try
                        {
                            Param_GetUOGoodsDeliverSelected inparam = new Param_GetUOGoodsDeliverSelected();

                            inparam.Delivery_Date = model.UO_DLA_Delivery_Date;
                            inparam.Delivery_Mode_ID = model.UO_DLA_Delivery_ModeId;
                            inparam.Delivery_Carrier = model.UO_DLA_Carrier;
                            inparam.Delivery_Location_ID = model.UO_DLA_Shipment_To_LocationId;
                            inparam.Delivered_By_ID = model.UO_DLA_Delivered_By_Id;
                            inparam.Delivery_Driver_ID = model.UO_DLA_DriverID;
                            inparam.Delivery_Receiver_Name = model.UO_DLA_Received_By;
                            inparam.Delivery_Remarks = model.UO_DLA_Remarks;
                            inparam.VehicleNo = model.UO_DLA_Vehicle_no;
                            inparam.UO_ID = Convert.ToInt32(UOID_Glob);
                            inparam.UO_Item_ID = model.UO_DeliverySelected_Item_ID;

                            var del_all_selected = BASE._user_order_DBOps.GetUOGoodsDeliverSelectedItems(inparam);

                            List<Return_GetUOGoodsDeliveredSelected_MainGrid> del_all_selected_main = del_all_selected.main_Register;
                            List<Return_GetUOGoodsDeliveredSelected_NestedGrid> del_all_selected_nested = del_all_selected.nested_Register;

                            var gridRowsCount = 0;
                            var LastRowSr = 0;
                            var NewSr = LastRowSr;


                            List<Return_GetUOGoodsDelivered_MainGrid> gridRows = new List<Return_GetUOGoodsDelivered_MainGrid>();
                            List<Return_GetUOGoodsDelivered_NestedGrid> gridRowsNested = new List<Return_GetUOGoodsDelivered_NestedGrid>();


                            if (UO_GoodsDeliveredMainData != null)
                            {
                                gridRows = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;

                                gridRowsCount = gridRows.Count;
                                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                                NewSr = LastRowSr;

                            }
                            if (UO_GoodsDeliveredNestedData != null)
                            {
                                gridRowsNested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;


                            }

                            if (del_all_selected_main != null)
                            {
                                for (int I = 0; I <= del_all_selected_main.Count() - 1; I++)
                                {
                                    Return_GetUOGoodsDelivered_MainGrid gridmain = new Return_GetUOGoodsDelivered_MainGrid();

                                    NewSr = NewSr + 1;
                                    gridmain.Sr = NewSr;
                                    gridmain.Item_Name = del_all_selected_main[I].Item_Name;
                                    gridmain.Item_Code = del_all_selected_main[I].Item_Code;
                                    gridmain.Item_Type = del_all_selected_main[I].Item_Type;
                                    gridmain.Head = del_all_selected_main[I].Head;
                                    gridmain.Delivered_Qty = del_all_selected_main[I].Delivered_Qty;
                                    gridmain.SubItem_ID = del_all_selected_main[I].SubItem_ID;
                                    //grid.Lot_No = model.UO_DL_Lot_No;
                                    gridmain.Delivery_Date = del_all_selected_main[I].Delivery_Date;
                                    gridmain.Delivery_Mode = del_all_selected_main[I].Delivery_Mode;
                                    gridmain.ModeID = del_all_selected_main[I].ModeID;
                                    gridmain.Carrier = del_all_selected_main[I].Carrier;
                                    gridmain.Delivery_Location = del_all_selected_main[I].Delivery_Location;
                                    gridmain.DeliveryLocationId = del_all_selected_main[I].DeliveryLocationId;
                                    gridmain.Remarks = del_all_selected_main[I].Remarks;
                                    gridmain.Driver = del_all_selected_main[I].Driver;
                                    gridmain.Vehicle_No = del_all_selected_main[I].Vehicle_No;
                                    //grid.StockRecordID = model.UO_DL_StockRecordID;
                                    gridmain.Receiver_Name = del_all_selected_main[I].Receiver_Name;
                                    gridmain.ItemRequestedID = Convert.ToInt32(del_all_selected_main[I].ItemRequestedID);
                                    gridmain.DeliveredByID = del_all_selected_main[I].DeliveredByID;
                                    gridmain.DriverID = del_all_selected_main[I].DriverID;
                                    gridmain.ID = 0;
                                    gridmain.Added_On = DateTime.Now;
                                    gridmain.Added_By = BASE._open_User_ID;

                                    gridRows.Add(gridmain);


                                    if (del_all_selected_nested != null)
                                    {

                                        for (int J = 0; J <= del_all_selected_nested.Count() - 1; J++)
                                        {
                                            if (del_all_selected_nested[J].MainSr == del_all_selected_main[I].Sr)
                                            {

                                                Return_GetUOGoodsDelivered_NestedGrid nestedgrid = new Return_GetUOGoodsDelivered_NestedGrid();


                                                nestedgrid.UD_ID = 0;
                                                nestedgrid.Sr = del_all_selected_nested[J].Sr;
                                                nestedgrid.ID = 0;
                                                nestedgrid.UDS_Qty = del_all_selected_nested[J].UDS_Qty;
                                                nestedgrid.StockRecordID = del_all_selected_nested[J].StockRecordID;
                                                nestedgrid.Make = del_all_selected_nested[J].Make;
                                                nestedgrid.Model = del_all_selected_nested[J].Model;
                                                nestedgrid.Lot_No = del_all_selected_nested[J].Lot_No;
                                                nestedgrid.Unit = del_all_selected_nested[J].Unit;
                                                nestedgrid.SubItem_ID = del_all_selected_nested[J].SubItem_ID;
                                                nestedgrid.MainSr = NewSr;
                                                gridRowsNested.Add(nestedgrid);
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "No More Pending Ordered Item to be Delivered"
                                }, JsonRequestBehavior.AllowGet);

                            }
                            UO_GoodsDeliveredMainData = gridRows;
                            UO_GoodsDeliveredNestedData = gridRowsNested;
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
                        try
                        {
                            Param_GetUOGoodsDeliverAllPending inparam = new Param_GetUOGoodsDeliverAllPending();

                            inparam.Delivery_Date = model.UO_DLA_Delivery_Date;
                            inparam.Delivery_Mode_ID = model.UO_DLA_Delivery_ModeId;
                            inparam.Delivery_Carrier = model.UO_DLA_Carrier;
                            inparam.Delivery_Location_ID = model.UO_DLA_Shipment_To_LocationId;
                            inparam.Delivered_By_ID = model.UO_DLA_Delivered_By_Id;
                            inparam.Delivery_Driver_ID = model.UO_DLA_DriverID;
                            inparam.Delivery_Receiver_Name = model.UO_DLA_Received_By;
                            inparam.Delivery_Remarks = model.UO_DLA_Remarks;
                            inparam.VehicleNo = model.UO_DLA_Vehicle_no;
                            inparam.UO_ID = Convert.ToInt32(UOID_Glob);

                            var del_all_pending = BASE._user_order_DBOps.GetUOGoodsDeliverAllPending(inparam);

                            List<Return_GetUOGoodsDeliveredAllPending_MainGrid> del_all_pending_main = del_all_pending.main_Register;
                            List<Return_GetUOGoodsDeliveredAllPending_NestedGrid> del_all_pending_nested = del_all_pending.nested_Register;

                            var gridRowsCount = 0;
                            var LastRowSr = 0;
                            var NewSr = LastRowSr;


                            List<Return_GetUOGoodsDelivered_MainGrid> gridRows = new List<Return_GetUOGoodsDelivered_MainGrid>();
                            List<Return_GetUOGoodsDelivered_NestedGrid> gridRowsNested = new List<Return_GetUOGoodsDelivered_NestedGrid>();


                            if (UO_GoodsDeliveredMainData != null)
                            {
                                gridRows = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;

                                gridRowsCount = gridRows.Count;
                                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                                NewSr = LastRowSr;

                            }
                            if (UO_GoodsDeliveredNestedData != null)
                            {
                                gridRowsNested = (List<Return_GetUOGoodsDelivered_NestedGrid>)UO_GoodsDeliveredNestedData;


                            }

                            if (del_all_pending_main != null)
                            {
                                for (int I = 0; I <= del_all_pending_main.Count() - 1; I++)
                                {
                                    Return_GetUOGoodsDelivered_MainGrid gridmain = new Return_GetUOGoodsDelivered_MainGrid();
                                    NewSr = NewSr + 1;
                                    gridmain.Sr = NewSr;
                                    gridmain.Item_Name = del_all_pending_main[I].Item_Name;
                                    gridmain.Item_Code = del_all_pending_main[I].Item_Code;
                                    gridmain.Item_Type = del_all_pending_main[I].Item_Type;
                                    gridmain.Head = del_all_pending_main[I].Head;
                                    gridmain.Delivered_Qty = del_all_pending_main[I].Delivered_Qty;
                                    gridmain.SubItem_ID = del_all_pending_main[I].SubItem_ID;
                                    gridmain.Delivery_Date = del_all_pending_main[I].Delivery_Date;
                                    gridmain.Delivery_Mode = del_all_pending_main[I].Delivery_Mode;
                                    gridmain.ModeID = del_all_pending_main[I].ModeID;
                                    gridmain.Carrier = del_all_pending_main[I].Carrier;
                                    gridmain.Delivery_Location = del_all_pending_main[I].Delivery_Location;
                                    gridmain.DeliveryLocationId = del_all_pending_main[I].DeliveryLocationId;
                                    gridmain.Remarks = del_all_pending_main[I].Remarks;
                                    gridmain.Driver = del_all_pending_main[I].Driver;
                                    gridmain.Vehicle_No = del_all_pending_main[I].Vehicle_No;
                                    gridmain.Receiver_Name = del_all_pending_main[I].Receiver_Name;
                                    gridmain.ItemRequestedID = Convert.ToInt32(del_all_pending_main[I].ItemRequestedID);
                                    gridmain.DeliveredByID = del_all_pending_main[I].DeliveredByID;
                                    gridmain.DriverID = del_all_pending_main[I].DriverID;
                                    gridmain.ID = 0;
                                    gridmain.Added_On = DateTime.Now;
                                    gridmain.Added_By = BASE._open_User_ID;

                                    gridRows.Add(gridmain);


                                    if (del_all_pending_nested != null)
                                    {

                                        for (int J = 0; J <= del_all_pending_nested.Count() - 1; J++)
                                        {
                                            if (del_all_pending_nested[J].MainSr == del_all_pending_main[I].Sr)
                                            {
                                                Return_GetUOGoodsDelivered_NestedGrid nestedgrid = new Return_GetUOGoodsDelivered_NestedGrid();


                                                nestedgrid.UD_ID = 0;
                                                nestedgrid.Sr = del_all_pending_nested[J].Sr;
                                                nestedgrid.ID = 0;
                                                nestedgrid.UDS_Qty = del_all_pending_nested[J].UDS_Qty;
                                                nestedgrid.StockRecordID = del_all_pending_nested[J].StockRecordID;
                                                nestedgrid.Make = del_all_pending_nested[J].Make;
                                                nestedgrid.Model = del_all_pending_nested[J].Model;
                                                nestedgrid.Lot_No = del_all_pending_nested[J].Lot_No;
                                                nestedgrid.Unit = del_all_pending_nested[J].Unit;
                                                nestedgrid.SubItem_ID = del_all_pending_nested[J].SubItem_ID;
                                                nestedgrid.MainSr = NewSr;
                                                gridRowsNested.Add(nestedgrid);
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "No More Pending Ordered Item to be Delivered"
                                }, JsonRequestBehavior.AllowGet);

                            }
                            UO_GoodsDeliveredMainData = gridRows;
                            UO_GoodsDeliveredNestedData = gridRowsNested;
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

                }
                return Json(new
                {
                    result = true,
                    message = "Added Successfully"
                }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region Order_Received
        public ActionResult LookUp_Get_Item_Received(DataSourceLoadOptions loadOptions, int? Recd_ID)

        {
            if (Recd_ID != 0)
            {
                List<Return_GetUODeliveredItems> UOGRItem = BASE._user_order_DBOps.GetUODeliveredItems(Convert.ToInt32(UOID_Glob), Convert.ToInt32(Recd_ID));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRItem, loadOptions)), "application/json");
            }
            else
            {
                List<Return_GetUODeliveredItems> UOGRItem = BASE._user_order_DBOps.GetUODeliveredItems(Convert.ToInt32(UOID_Glob));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRItem, loadOptions)), "application/json");

            }
        }

        public ActionResult LookUp_Get_Received_Mode(DataSourceLoadOptions loadOptions)
        {
            var UOGRRECMODE = BASE._user_order_DBOps.GetDeliveryModes();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRRECMODE, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Received_Location(DataSourceLoadOptions loadOptions, int UOGRReqMainDeptID = 0, int? UOReqSubDeptID = null)
        {
            List<Return_GetLocations> AllLocations = new List<Return_GetLocations>();

            List<Return_GetLocations> UORecdLoc = BASE._Stock_Profile_DBOps.GetLocations(UOGRReqMainDeptID);

            AllLocations = UORecdLoc;

            if (UOReqSubDeptID != null)
            {
                List<Return_GetLocations> Loclistsubdept = BASE._Stock_Profile_DBOps.GetLocations(Convert.ToInt32(UOReqSubDeptID));
                List<Return_GetLocations> AllLocationsmainsub = UORecdLoc.Union(Loclistsubdept).ToList();
                AllLocations = AllLocationsmainsub.DistinctBy(x => x.Loc_Id).ToList();
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AllLocations, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Received_By(DataSourceLoadOptions loadOptions, int UOGRRecReqMainDeptID = 0, int? UOGRRecReqSubDeptID = null)
        {
            List<Return_GetPersonnels> AllPersonnels = new List<Return_GetPersonnels>();

            List<Return_GetPersonnels> UORecd_RecdBy = BASE._user_order_DBOps.GetPersonnels(UOGRRecReqMainDeptID);

            AllPersonnels = UORecd_RecdBy;

            if (UOGRRecReqSubDeptID != null)
            {
                List<Return_GetPersonnels> Perlistsubdept = BASE._user_order_DBOps.GetPersonnels(Convert.ToInt32(UOGRRecReqSubDeptID));
                List<Return_GetPersonnels> AllPer_main_sub= UORecd_RecdBy.Union(Perlistsubdept).ToList();
                AllPersonnels = AllPer_main_sub.DistinctBy(x => x.ID).ToList();
            }




            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AllPersonnels, loadOptions)), "application/json");
        }
        public ActionResult UO_GoodsReceivedGridData(string ActionMethodName, int UOID = 0)
        {
            var UORecdList = new List<Return_GetUOGoodsReceived>();
            if (ActionMethodName == "New")
            {
                return PartialView(UORecdList);
            }
            if (UO_GoodsRecdData == null)
            {
                List<Return_GetUOGoodsReceived> UOGoodsRecData = BASE._user_order_DBOps.GetUOGoodsReceived(UOID);
                if (UOGoodsRecData != null)
                {
                    UORecdList = UOGoodsRecData;
                }
                UO_GoodsRecdData = UORecdList;
            }
            return PartialView(UO_GoodsRecdData);
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
                    var retcount = BASE._user_order_DBOps.GetUOReceipt_Return_EntryCount(ID);
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

        public ActionResult Received_grid_oldest_delivery(string[] UO_ItemID, int UOid = 0)
        {
            var uoid = UOid;
            if (uoid != 0)
            {
                UOID_Glob = uoid;
            }
            List<Return_GetUODeliveredItems> UOGRItem = BASE._user_order_DBOps.GetUODeliveredItems(Convert.ToInt32(UOID_Glob));


            if (UO_ItemID != null)
            {
                if (UO_ItemID.Length == 1)
                {
                    List<Return_GetUODeliveredItems> selecteditem = UOGRItem.FindAll(x => x.ItemRequestedID.ToString() == UO_ItemID[0].ToString());


                    if (selecteditem.Count == 0)
                    {
                        return Json(new
                        {
                            message = "Item not saved/ Selected Item has no pending Deliveries to be Received...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        var mindate = selecteditem.Min(x => x.Added_On);
                        var itemidj = selecteditem.FindAll(x => x.Added_On.Equals(mindate)).FirstOrDefault();
                        var itemid = itemidj.ID;
                        return Json(new { selID = itemid, result = true }, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }

            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Receive ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Frm_User_Order_Received(string ActionMethod = null, int SR = 0, int UOid = 0, string PostSuccessFunction = null, string PopupID = null, int Item_Rec_Id = 0)
        {
            UOAddLocation_user_rights();
            UserOrder_Received model = new UserOrder_Received();
            model.GR_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "Frm_User_Order_Received_OnSuccess";
            model.GR_PopUPId = PopupID != null ? PopupID : "UserOrderinnergrid";
            model.UO_GR_ActionMethod = ActionMethod;
            if (UOid != 0)
            {
                model.UOid = UOid;
            }
            if (model.UOid != 0)
            {
                UOID_Glob = UOid;
            }


            if (ActionMethod == "New")
            {
                model.UO_GR_Received_Date = DateTime.Now;
                model.UO_GR_Received_By_Id = BASE._open_User_PersonnelID;


                if (Item_Rec_Id != 0)
                {
                    model.UO_GR_Item_ReceivedId = Item_Rec_Id;
                }


            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsReceived();

                model.UO_GR_Sr = Sr;

                // model.UO_GR_Item_Name = dataToEdit.Item_Name;
                // model.UO_GR_Item_Code = dataToEdit.Item_Code;
                //model.UO_GR_Item_Type = dataToEdit.Item_Type;
                //model.UO_GR_Head = dataToEdit.Head;
                //model.UO_GR_Make = dataToEdit.Make;
                //model.UO_GR_ModelName = dataToEdit.Model;
                model.UO_GR_Received_Qty = dataToEdit.Received_Qty;
                //model.UO_GR_Unit = dataToEdit.Unit;
                //model.UO_GR_Lot_No = dataToEdit.Lot_No;
                model.UO_GR_Received_Date = dataToEdit.Received_Date;
                model.UO_GR_Delivery_LocationId = dataToEdit.DeliveryLocationId;
                model.UO_GR_Carrier = dataToEdit.Carrier;
                model.UO_GR_Remarks = dataToEdit.Remarks;
                model.UO_GR_Received_ModeId = dataToEdit.ModeID;
                model.UO_GR_Received_By_Id = dataToEdit.ReceivedByID;
                model.UO_GR_BillNo = dataToEdit.Bill_No;
                model.UO_GR_ChallanNo = dataToEdit.Challan_No;
                model.UO_GR_ID = dataToEdit.ID;
                model.UO_GR_DeliveryEntry_ID = dataToEdit.DeliveryEntryID;
                model.UO_GR_ReturnedEntry_ID = dataToEdit.ReturnedEntryID;

                if (dataToEdit.DeliveryEntryID != null)
                {
                    model.UO_GR_Item_ReceivedId = Convert.ToInt32(dataToEdit.DeliveryEntryID);
                }
                else if (dataToEdit.ReturnedEntryID != null)
                {
                    model.UO_GR_Item_ReceivedId = Convert.ToInt32(dataToEdit.ReturnedEntryID);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_User_Order_Received(UserOrder_Received model)
        {
            if (model.UO_GR_ActionMethod == "New" | model.UO_GR_ActionMethod == "Edit")
            {

                if (model.UO_GR_Received_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Received Qty can not be Negative or Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UO_GR_Received_Qty > model.UO_GR_Unreceived_Delivered_Qty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Received Qty can not be greater than Pending Delivered Qty ...!"
                    }, JsonRequestBehavior.AllowGet);
                }

                int value = DateTime.Compare(model.UO_GR_Received_Date, model.UO_GR_Delivery_Date);
                if (value < 0) 
                {
                    return Json(new
                    {
                        result = false,
                        message = "Received Date can not be earlier then delivery date ...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }




            //if (model.UO_GR_ActionMethod == "New")
            //{
            //    var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;

            //    if (all_data_Of_Received_Grid != null)
            //    {

            //        List<Return_GetUOGoodsReceived> datatocheck = all_data_Of_Received_Grid.FindAll(x => x.ID == 0);


            //        for (int I = 0; I <= datatocheck.Count() - 1; I++)
            //        {

            //            if (datatocheck[I].UDS_ID == model.UO_GR_UDS_ID)
            //            {
            //                return Json(new
            //                {
            //                    result = false,
            //                    message = "New Unsaved Row for same item is already in Grid, please edit existing row."
            //                }, JsonRequestBehavior.AllowGet);

            //            }
            //        }
            //    }
            //}




            if (model.UOid != 0)
            {
                try
                {
                    Param_Insert_UO_Item_Received inparam = new Param_Insert_UO_Item_Received();
                    inparam.UO_ID = model.UOid;
                    inparam.UO_Delivered_ID = model.UO_GR_DeliveryEntry_ID;
                    inparam.Recd_Qty = model.UO_GR_Received_Qty;
                    inparam.Recd_Date = model.UO_GR_Received_Date;
                    inparam.Delivery_Mode_ID = model.UO_GR_Received_ModeId;
                    inparam.Delivery_Carrier = model.UO_GR_Carrier;
                    inparam.Received_Location_ID = model.UO_GR_Delivery_LocationId;
                    inparam.Bill_No = model.UO_GR_BillNo;
                    inparam.Challan_No = model.UO_GR_ChallanNo;
                    inparam.Received_By_ID = model.UO_GR_Received_By_Id;
                    inparam.Receiver_Remarks = model.UO_GR_Remarks;
                    inparam.UO_Returned_ID = model.UO_GR_ReturnedEntry_ID;
                    inparam.UDS_ID = model.UO_GR_UDS_ID;
                    if (BASE._user_order_DBOps.Insert_UO_Item_Received(inparam))
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
                List<Return_GetUOGoodsReceived> gridRows = new List<Return_GetUOGoodsReceived>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (UO_GoodsRecdData != null)
                {
                    gridRows = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.UO_GR_ActionMethod == "New")
                {
                    Return_GetUOGoodsReceived grid = new Return_GetUOGoodsReceived();
                    grid.Sr = NewSr;

                    //  grid.Item_ReceivedId = model.UO_GR_Item_ReceivedId;
                    grid.Item_Name = model.UO_GR_Item_Name;
                    grid.Item_Code = model.UO_GR_Item_Code;
                    grid.Item_Type = model.UO_GR_Item_Type;
                    grid.Head = model.UO_GR_Head;
                    grid.Make = model.UO_GR_Make;
                    grid.Model = model.UO_GR_ModelName;
                    grid.Received_Qty = model.UO_GR_Received_Qty;
                    grid.Unit = model.UO_GR_Unit;
                    grid.Lot_No = model.UO_GR_Lot_No;
                    grid.Received_Date = model.UO_GR_Received_Date;
                    grid.Received_Mode = model.UO_GR_Received_Mode;
                    grid.ModeID = model.UO_GR_Received_ModeId;
                    grid.Carrier = model.UO_GR_Carrier;
                    grid.Received_Location = model.UO_GR_Delivery_Location;
                    grid.DeliveryLocationId = model.UO_GR_Delivery_LocationId;
                    grid.Remarks = model.UO_GR_Remarks;

                    grid.Bill_No = model.UO_GR_BillNo;
                    grid.Challan_No = model.UO_GR_ChallanNo;

                    grid.ReceivedByID = model.UO_GR_Received_By_Id;
                    grid.DeliveryEntryID = model.UO_GR_DeliveryEntry_ID;
                    grid.ReturnedEntryID = model.UO_GR_ReturnedEntry_ID;

                    grid.ID = 0;
                    grid.UDS_ID = model.UO_GR_UDS_ID;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);

                }

                else if (model.UO_GR_ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.UO_GR_Sr);
                    if (dataToEdit.ID != 0)
                    {
                        var retcount = BASE._user_order_DBOps.GetUOReceipt_Return_EntryCount(dataToEdit.ID);
                        if (retcount > 0)
                        {

                            return Json(new
                            {
                                result = false,
                                message = "Receipt Entry against which Returns has been posted can not be edited "
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.UO_GR_ID != 0)
                    {
                        var editeditUOGoodsRecID = new ArrayList();
                        var editGoodsRec = UOEdit_GoodsRec_ID as ArrayList;
                        if (editGoodsRec != null)
                        {
                            editGoodsRec.Add(model.UO_GR_ID);
                            UOEdit_GoodsRec_ID = editGoodsRec;
                        }
                        else
                        {
                            editeditUOGoodsRecID.Add(model.UO_GR_ID);
                            UOEdit_GoodsRec_ID = editeditUOGoodsRecID;
                        }
                    }
                    //dataToEdit.Item_ReceivedId = model.UO_GR_Item_ReceivedId;
                    dataToEdit.Item_Name = model.UO_GR_Item_Name;
                    dataToEdit.Item_Code = model.UO_GR_Item_Code;
                    dataToEdit.Item_Type = model.UO_GR_Item_Type;
                    dataToEdit.Head = model.UO_GR_Head;
                    dataToEdit.Make = model.UO_GR_Make;
                    dataToEdit.Model = model.UO_GR_ModelName;
                    dataToEdit.Received_Qty = model.UO_GR_Received_Qty;
                    dataToEdit.Unit = model.UO_GR_Unit;
                    dataToEdit.Lot_No = model.UO_GR_Lot_No;
                    dataToEdit.Received_Date = model.UO_GR_Received_Date;
                    dataToEdit.Received_Mode = model.UO_GR_Received_Mode;
                    dataToEdit.ModeID = model.UO_GR_Received_ModeId;
                    dataToEdit.Carrier = model.UO_GR_Carrier;
                    dataToEdit.Received_Location = model.UO_GR_Delivery_Location;
                    dataToEdit.DeliveryLocationId = model.UO_GR_Delivery_LocationId;
                    dataToEdit.Remarks = model.UO_GR_Remarks;


                    dataToEdit.Bill_No = model.UO_GR_BillNo;
                    dataToEdit.Challan_No = model.UO_GR_ChallanNo;

                    dataToEdit.ReceivedByID = model.UO_GR_Received_By_Id;
                    dataToEdit.DeliveryEntryID = model.UO_GR_DeliveryEntry_ID;
                    dataToEdit.ReturnedEntryID = model.UO_GR_ReturnedEntry_ID;
                    dataToEdit.UDS_ID = model.UO_GR_UDS_ID;
                }
                UO_GoodsRecdData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult Received_Item_Exists_in_Grid(int UDSID = 0, int uoid = 0, int srno = 0)
        //{

        //    var ID = uoid;
        //    var SrNO = srno;
        //    if (uoid != 0)
        //    {
        //        return Json(new
        //        {
        //            Message = "",
        //            result = true
        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
        //        if (all_data_Of_Received_Grid != null)
        //        {

        //            if (all_data_Of_Received_Grid.Count != 0)
        //            {

        //                List<Return_GetUOGoodsReceived> datatocheck = all_data_Of_Received_Grid.FindAll(x => x.ID == 0 );


        //                for (int I = 0; I <= datatocheck.Count() - 1; I++)
        //                {

        //                    if (datatocheck[I].UDS_ID == UDSID)
        //                    {
        //                        if (datatocheck[I].Sr != SrNO)
        //                        {
        //                            return Json(new
        //                            {
        //                                result = false,
        //                                message = "New unsaved Row for same item is already in Grid, please edit existing row."
        //                            }, JsonRequestBehavior.AllowGet);
        //                        }
        //                    }
        //                }
        //            }


        //        }
        //        return Json(new
        //        {
        //            Message = "",
        //            result = true
        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //}

        public JsonResult All_Saved_Received_Item_Exists_in_Grid(int ReceivedID = 0, int uoid = 0, int srno = 0)
        {
            //received ID is UDS_ID
            var ID = uoid;
            var SrNO = srno;
            if (uoid != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                if (all_data_Of_Received_Grid != null)
                {

                    if (all_data_Of_Received_Grid.Count != 0)
                    {

                        List<Return_GetUOGoodsReceived> datatocheck = all_data_Of_Received_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);

                        List<Return_GetUOGoodsReceived> datatocheckedit = all_data_Of_Received_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);

                        List<Return_GetUOGoodsReceived> datatocheckdelete = all_data_Of_Received_Grid.FindAll(x => x.ID != 0);

                        if (datatocheck.Count != 0)
                        {
                            for (int I = 0; I <= datatocheck.Count() - 1; I++)
                            {

                                if (datatocheck[I].UDS_ID == ReceivedID)
                                {

                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit existing row."
                                    }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].UDS_ID == ReceivedID)
                                {

                                    if (UOEdit_GoodsRec_ID != null)
                                    {
                                        return Json(new
                                        {
                                            result = false,
                                            message = "Edited saved row for this item is already in Grid, please save UO first to edit this row."
                                        }, JsonRequestBehavior.AllowGet);


                                    }


                                }
                            }
                        }

                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].UDS_ID == ReceivedID)
                                {

                                    if (Delete_User_GoodsRecd_ID != null)
                                    {
                                        var deletedID = DeletedUDS_ID as ArrayList; //mantis bug 1225
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].UDS_ID == Convert.ToInt32(deletedID[i]))

                                                {
                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save UO first for any Addition/Updation."
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

        public JsonResult Check_Unsaved_Item_Entry_in_Returned_Grid(int recid = 0)
        {


            var all_data_Of_Returned_Grid = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
            if (all_data_Of_Returned_Grid != null)
            {

                if (all_data_Of_Returned_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReturned> datatocheckret = all_data_Of_Returned_Grid.FindAll(x => x.ItemReceivedID == recid && x.ID == 0);


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


        public JsonResult Check_Unsaved_Item_Entry_in_ReturnedReceived_Grid(int UDSid = 0)
        {


            var all_data_Of_Returnedrecd_Grid = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
            if (all_data_Of_Returnedrecd_Grid != null)
            {

                if (all_data_Of_Returnedrecd_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReturnReceived> datatocheckretrec = all_data_Of_Returnedrecd_Grid.FindAll(x => x.UDS_ID == UDSid && x.ID == 0);
                    List<Return_GetUOGoodsReturnReceived> datatocheckretrecedit = all_data_Of_Returnedrecd_Grid.FindAll(x => x.UDS_ID == UDSid && x.ID != 0);

                    if (datatocheckretrec.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in Returned Received Grid, please save it to Add/Edit/Delete this item."
                        }, JsonRequestBehavior.AllowGet);


                    }

                    if (datatocheckretrecedit.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckretrecedit.Count() - 1; I++)
                        {

                            if (UOEdit_GoodsRetRec_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Returned Received Grid, please save UO first to Add/Edit this item."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }
            if (Delete_User_GoodsRetRecd_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.No items should be deleted
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in Returned Received Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Check_Unsaved_Item_Entry_in_Delivery_Grid(int deliveryID = 0)
        {


            var all_data_Of_Delivery_Grid = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
            if (all_data_Of_Delivery_Grid != null)
            {

                if (all_data_Of_Delivery_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsDelivered_MainGrid> datatocheckdel = all_data_Of_Delivery_Grid.FindAll(x => x.ID == deliveryID && x.ID != 0); //to be tested


                    if (datatocheckdel.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckdel.Count() - 1; I++)
                        {

                            if (UOEdit_GoodsDelivered_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Delivery Grid, please save UO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }
            if (Delete_User_GoodsDelivered_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.No items should be deleted
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in Delivery Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }


            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Frm_GoodsRecd_DetailWin_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Goods_Rec_Id = 0)
        {


            var allSrcDataGR = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            var dataToDelete = allSrcDataGR != null ? allSrcDataGR.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUOGoodsReceived();
            var del_udsid = dataToDelete.UDS_ID;
            //if (dataToDelete.ID != 0)
            //{
            //    var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.StockID, ClientScreen.Stock_UO);
            //    if (itemcheck.Count > 0)
            //    {
            //        var inusescreen = itemcheck[0].Screen;
            //        return Json(new
            //        {
            //            result = false,
            //            message = "Goods Received Cannot Be Deleted Because It has Been Used In" + inusescreen + "...!"
            //        }, JsonRequestBehavior.AllowGet);
            //    }
            //}

            if (dataToDelete.ID != 0)
            {
                var retcount = BASE._user_order_DBOps.GetUOReceipt_Return_EntryCount(dataToDelete.ID);
                if (retcount > 0)
                {

                    return Json(new
                    {
                        result = false,
                        message = "Receipt Entry against which Returns has been posted can not be deleted "
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allSrcDataGR != null)
            {
                allSrcDataGR.Remove(dataToDelete);
            }
            UO_GoodsRecdData = allSrcDataGR;
            var deleteScrapID = new ArrayList();
            var deleteUDSID = new ArrayList();

            if (Goods_Rec_Id != 0)
            {
                var deleteUDS = DeletedUDS_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item
                var deleteScrap = Delete_User_GoodsRecd_ID as ArrayList;
                if (deleteScrap != null)
                {
                    deleteScrap.Add(Goods_Rec_Id);
                    Delete_User_GoodsRecd_ID = deleteScrap;

                    deleteUDS.Add(del_udsid);
                    DeletedUDS_ID = deleteUDS;


                }
                else
                {
                    deleteScrapID.Add(Goods_Rec_Id);
                    Delete_User_GoodsRecd_ID = deleteScrapID;

                    deleteUDSID.Add(del_udsid);
                    DeletedUDS_ID = deleteUDSID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Order_Received All


        [HttpGet]
        public ActionResult Frm_User_Order_Received_All(string ActionMethod = null, int SR = 0, int UOid = 0, string[] UO_ItemID = null)
        {
            UOAddLocation_user_rights();
            UserOrder_Received_All model = new UserOrder_Received_All();
            model.UOid = UOid;
            model.UO_GRA_ActionMethod = "New";

            if (ActionMethod == "New")
            {
                model.UO_GRA_Received_Date = DateTime.Now;
                model.UO_GRA_Received_By_Id = BASE._open_User_PersonnelID;
                String uoitemid = null;
                if (UO_ItemID != null)
                {
                    for (int i = 0; i < UO_ItemID.Length; i++)
                    {
                        if (uoitemid == null)
                        {
                            uoitemid = UO_ItemID[i].ToString();
                        }
                        else
                        {
                            uoitemid = uoitemid + ',' + UO_ItemID[i].ToString();
                        }

                    }
                }
                model.UO_Selected_Item_ID = uoitemid;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_User_Order_Received_All(UserOrder_Received_All model)
        {


            if (model.UOid != 0)
            {
                List<Return_GetUODeliveredItems> UOGRItem = BASE._user_order_DBOps.GetUODeliveredItems(model.UOid);

                if (UOGRItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "No More Pending Deliveries to be Received"
                    }, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    //Param_Insert_UO_Item_Received inparam = new Param_Insert_UO_Item_Received();

                    Param_GetUOGoodsReceiveAllPending param = new Param_GetUOGoodsReceiveAllPending();
                    param.UO_ID = model.UOid;
                    param.Recd_Date = model.UO_GRA_Received_Date;
                    param.Delivery_Mode_ID = model.UO_GRA_Received_ModeId;
                    param.Delivery_Carrier = model.UO_GRA_Carrier;
                    param.Received_Location_ID = model.UO_GRA_Delivery_LocationId;
                    param.Bill_No = model.UO_GRA_BillNo;
                    param.Challan_No = model.UO_GRA_ChallanNo;
                    param.Received_By_ID = model.UO_GRA_Received_By_Id;
                    param.Receiver_Remarks = model.UO_GRA_Remarks;



                    if (BASE._user_order_DBOps.InsertUOGoodsReceiveAllPending(param))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = "No More Pending Deliveries to be Received" }, JsonRequestBehavior.AllowGet);
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

                List<Return_GetUODeliveredItems> UOGRItem = BASE._user_order_DBOps.GetUODeliveredItems(Convert.ToInt32(UOID_Glob));
                if (UOGRItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "No More Pending Deliveries to be Received"
                    }, JsonRequestBehavior.AllowGet);
                }

                List<Return_GetUOGoodsReceived> all_data = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                Return_GetUOGoodsReceived dataToEdit = new Return_GetUOGoodsReceived();
                if (all_data != null)
                {
                    dataToEdit = all_data.Where(x => x.ID == 0).FirstOrDefault();

                }

                if (dataToEdit != null || UOEdit_GoodsRec_ID != null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Pls. save the existing New/ Edited rows in received grid first...!"
                    }, JsonRequestBehavior.AllowGet);

                }


                if (model.UO_GRA_ActionMethod == "New")
                {

                    if (model.UO_Selected_Item_ID != null)
                    {
                        Param_GetUOGoodsReceiveSelected inparam = new Param_GetUOGoodsReceiveSelected();
                        inparam.UO_ID = Convert.ToInt32(UOID_Glob);
                        inparam.Recd_Date = model.UO_GRA_Received_Date;
                        inparam.Delivery_Mode_ID = model.UO_GRA_Received_ModeId;
                        inparam.Delivery_Carrier = model.UO_GRA_Carrier;
                        inparam.Received_Location_ID = model.UO_GRA_Delivery_LocationId;
                        inparam.Bill_No = model.UO_GRA_BillNo;
                        inparam.Challan_No = model.UO_GRA_ChallanNo;
                        inparam.Received_By_ID = model.UO_GRA_Received_By_Id;
                        inparam.Receiver_Remarks = model.UO_GRA_Remarks;
                        inparam.UO_Item_ID = model.UO_Selected_Item_ID;

                        List<Return_GetUOGoodsReceivedAllPending> recd_all_selected = BASE._user_order_DBOps.GetUOGoodsReceiveSelectedItems(inparam);

                        var gridRowsCount = 0;
                        var LastRowSr = 0;
                        var NewSr = LastRowSr + 1;

                        List<Return_GetUOGoodsReceived> gridRows = new List<Return_GetUOGoodsReceived>();
                        if (UO_GoodsRecdData != null)
                        {
                            gridRows = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                            gridRowsCount = gridRows.Count;
                            LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                            NewSr = LastRowSr + 1;
                        }
                        if (recd_all_selected != null)
                        {

                            for (int I = 0; I <= recd_all_selected.Count() - 1; I++)
                            {
                                Return_GetUOGoodsReceived grid = new Return_GetUOGoodsReceived();
                                grid.Sr = NewSr;
                                NewSr = NewSr + 1;
                                grid.Item_Name = recd_all_selected[I].Item_Name;
                                grid.Item_Code = recd_all_selected[I].Item_Code;
                                grid.Item_Type = recd_all_selected[I].Item_Type;
                                grid.Head = recd_all_selected[I].Head;
                                grid.Make = recd_all_selected[I].Make;
                                grid.Model = recd_all_selected[I].Model;
                                grid.Received_Qty = recd_all_selected[I].Received_Qty;
                                grid.Unit = recd_all_selected[I].Unit;
                                grid.Lot_No = recd_all_selected[I].Lot_No;
                                grid.Received_Date = recd_all_selected[I].Received_Date;
                                grid.Received_Mode = recd_all_selected[I].Received_Mode;
                                grid.ModeID = recd_all_selected[I].ModeID;
                                grid.Carrier = recd_all_selected[I].Carrier;
                                grid.Received_Location = recd_all_selected[I].Received_Location;
                                grid.DeliveryLocationId = recd_all_selected[I].DeliveryLocationId;
                                grid.Remarks = recd_all_selected[I].Remarks;

                                grid.Bill_No = recd_all_selected[I].Bill_No;
                                grid.Challan_No = recd_all_selected[I].Challan_No;

                                grid.ReceivedByID = recd_all_selected[I].ReceivedByID;
                                grid.DeliveryEntryID = recd_all_selected[I].DeliveryEntryID;
                                grid.ReturnedEntryID = recd_all_selected[I].ReturnedEntryID;

                                grid.ID = 0;
                                grid.UDS_ID = recd_all_selected[I].UDS_ID;
                                grid.Added_On = DateTime.Now;
                                grid.Added_By = BASE._open_User_ID;
                                gridRows.Add(grid);

                            }


                        }
                        else
                        {
                            return Json(new
                            {
                                result = true,
                                message = "No More Pending Deliveries to be Received"
                            }, JsonRequestBehavior.AllowGet);

                        }

                        UO_GoodsRecdData = gridRows;
                    }

                    else
                    {
                        Param_GetUOGoodsReceiveAllPending inparam = new Param_GetUOGoodsReceiveAllPending();
                        inparam.UO_ID = Convert.ToInt32(UOID_Glob);
                        inparam.Recd_Date = model.UO_GRA_Received_Date;
                        inparam.Delivery_Mode_ID = model.UO_GRA_Received_ModeId;
                        inparam.Delivery_Carrier = model.UO_GRA_Carrier;
                        inparam.Received_Location_ID = model.UO_GRA_Delivery_LocationId;
                        inparam.Bill_No = model.UO_GRA_BillNo;
                        inparam.Challan_No = model.UO_GRA_ChallanNo;
                        inparam.Received_By_ID = model.UO_GRA_Received_By_Id;
                        inparam.Receiver_Remarks = model.UO_GRA_Remarks;

                        List<Return_GetUOGoodsReceivedAllPending> recd_all_pending = BASE._user_order_DBOps.GetUOGoodsReceiveAllPending(inparam);

                        var gridRowsCount = 0;
                        var LastRowSr = 0;
                        var NewSr = LastRowSr + 1;

                        List<Return_GetUOGoodsReceived> gridRows = new List<Return_GetUOGoodsReceived>();
                        if (UO_GoodsRecdData != null)
                        {
                            gridRows = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
                            gridRowsCount = gridRows.Count;
                            LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                            NewSr = LastRowSr + 1;
                        }
                        if (recd_all_pending != null)
                        {

                            for (int I = 0; I <= recd_all_pending.Count() - 1; I++)
                            {
                                Return_GetUOGoodsReceived grid = new Return_GetUOGoodsReceived();
                                grid.Sr = NewSr;
                                NewSr = NewSr + 1;
                                grid.Item_Name = recd_all_pending[I].Item_Name;
                                grid.Item_Code = recd_all_pending[I].Item_Code;
                                grid.Item_Type = recd_all_pending[I].Item_Type;
                                grid.Head = recd_all_pending[I].Head;
                                grid.Make = recd_all_pending[I].Make;
                                grid.Model = recd_all_pending[I].Model;
                                grid.Received_Qty = recd_all_pending[I].Received_Qty;
                                grid.Unit = recd_all_pending[I].Unit;
                                grid.Lot_No = recd_all_pending[I].Lot_No;
                                grid.Received_Date = recd_all_pending[I].Received_Date;
                                grid.Received_Mode = recd_all_pending[I].Received_Mode;
                                grid.ModeID = recd_all_pending[I].ModeID;
                                grid.Carrier = recd_all_pending[I].Carrier;
                                grid.Received_Location = recd_all_pending[I].Received_Location;
                                grid.DeliveryLocationId = recd_all_pending[I].DeliveryLocationId;
                                grid.Remarks = recd_all_pending[I].Remarks;

                                grid.Bill_No = recd_all_pending[I].Bill_No;
                                grid.Challan_No = recd_all_pending[I].Challan_No;

                                grid.ReceivedByID = recd_all_pending[I].ReceivedByID;
                                grid.DeliveryEntryID = recd_all_pending[I].DeliveryEntryID;
                                grid.ReturnedEntryID = recd_all_pending[I].ReturnedEntryID;

                                grid.ID = 0;
                                grid.UDS_ID = recd_all_pending[I].UDS_ID;
                                grid.Added_On = DateTime.Now;
                                grid.Added_By = BASE._open_User_ID;
                                gridRows.Add(grid);

                            }


                        }
                        else
                        {
                            return Json(new
                            {
                                result = true,
                                message = "No More Pending Deliveries to be Received"
                            }, JsonRequestBehavior.AllowGet);

                        }

                        UO_GoodsRecdData = gridRows;
                    }
                }

                return Json(new
                {
                    result = true,
                    message = "Added Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion

        #region Order_Returned
        public ActionResult LookUp_Get_Item_Returned(DataSourceLoadOptions loadOptions, int? Ret_ID)
        {
            if (Ret_ID != 0)
            {
                List<Return_GetUOGoodsReceived> UOGRetItem = BASE._user_order_DBOps.GetUOGoodsReceived(Convert.ToInt32(UOID_Glob), true, Convert.ToInt32(Ret_ID));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRetItem, loadOptions)), "application/json");
            }

            else
            {
                List<Return_GetUOGoodsReceived> UOGRetItem = BASE._user_order_DBOps.GetUOGoodsReceived(Convert.ToInt32(UOID_Glob), true);
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRetItem, loadOptions)), "application/json");

            }

        }
        public ActionResult LookUp_Get_Returned_Mode(DataSourceLoadOptions loadOptions)
        {
            var UORetMode = BASE._user_order_DBOps.GetDeliveryModes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UORetMode, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Returned_Location(DataSourceLoadOptions loadOptions, int retuoStoreID = 0)
        {
            var UO_Ret_Loc = BASE._Stock_Profile_DBOps.GetLocations(retuoStoreID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UO_Ret_Loc, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Returned_By(DataSourceLoadOptions loadOptions, int UOretMainDeptID = 0, int? UOretSubDeptID = null)
        {

            List<Return_GetPersonnels> AllPersonnels = new List<Return_GetPersonnels>();
            List<Return_GetPersonnels>  UO_Ret_By = BASE._user_order_DBOps.GetPersonnels(UOretMainDeptID);

            AllPersonnels = UO_Ret_By;

            if (UOretSubDeptID != null)
            {
                List<Return_GetPersonnels> Perlistsubdept = BASE._user_order_DBOps.GetPersonnels(Convert.ToInt32(UOretSubDeptID));
                List<Return_GetPersonnels> AllPer_main_sub = UO_Ret_By.Union(Perlistsubdept).ToList();
                AllPersonnels = AllPer_main_sub.DistinctBy(x => x.ID).ToList();
            }

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AllPersonnels, loadOptions)), "application/json");
        }
        public ActionResult UO_GoodsReturnedGridData(string ActionMethodName, int UOID = 0)
        {
            NEVD_UserOrder model = new NEVD_UserOrder();

            var UORetnList = new List<Return_GetUOGoodsReturned>();
            if (ActionMethodName == "New")
            {
                return PartialView(UORetnList);
            }
            if (UO_GoodsRetnData == null)
            {
                var UOGoodsRetndata = BASE._user_order_DBOps.GetUOGoodsReturned(UOID);

                if (UOGoodsRetndata != null)
                {
                    UORetnList = UOGoodsRetndata;
                }
                UO_GoodsRetnData = UORetnList;
            }
            return PartialView(UO_GoodsRetnData);
        }

        public ActionResult DataNavigationReturned(string ActionMethod = null, int ID = 0)
        {

            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                //var Sr = Convert.ToInt16(SR);
                //var all_data = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
                //var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsDelivered_MainGrid();
                if (ID != 0)
                {
                    var recdcount = BASE._user_order_DBOps.GetUOReturn_Received_EntryCount(ID);
                    if (recdcount > 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "Return Entry against which Return Receipt has been posted can not be edited/deleted"
                        }, JsonRequestBehavior.AllowGet);
                    }

                }


            }

            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Returned_grid_oldest_received(string[] UO_ItemID, int UOid = 0)
        {
            var uoid = UOid;
            if (uoid != 0)
            {
                UOID_Glob = uoid;
            }
            List<Return_GetUOGoodsReceived> UOGRItem = BASE._user_order_DBOps.GetUOGoodsReceived(Convert.ToInt32(UOID_Glob), true);


            if (UO_ItemID != null)
            {
                if (UO_ItemID.Length == 1)
                {
                    List<Return_GetUOGoodsReceived> selecteditem = UOGRItem.FindAll(x => x.UOI_ID.ToString() == UO_ItemID[0].ToString());


                    if (selecteditem.Count == 0)
                    {
                        return Json(new
                        {
                            message = "Item not saved/ Item Not Received/ All Received Items are already Returned...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        var mindate = selecteditem.Min(x => x.Added_On);
                        var itemidj = selecteditem.FindAll(x => x.Added_On.Equals(mindate)).FirstOrDefault();
                        var itemid = itemidj.ID;
                        return Json(new { selID = itemid, result = true }, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }

            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Return ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_User_Order_Returned(string ActionMethod = null, int SR = 0, int UOid = 0, string PostSuccessFunction = null, string PopupID = null, int Item_Rec_Id = 0)
        {
            UOAddLocation_user_rights();
            UserOrder_Returned model = new UserOrder_Returned();

            model.UO_GRet_ActionMethod = ActionMethod;
            model.GRet_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "Frm_User_Order_Returned_OnSuccess";
            model.GRet_PopUPId = PopupID != null ? PopupID : "UserOrderinnergrid";
            if (UOid != 0)
            {
                model.UOid = UOid;
            }
            if (model.UOid != 0)
            {
                UOID_Glob = UOid;
            }
            if (ActionMethod == "New")
            {
                model.UO_GRet_Returned_Date = DateTime.Now;
                model.UO_GRet_Returned_By_Id = BASE._open_User_PersonnelID;
                if (Item_Rec_Id != 0)
                {
                    model.UO_GRet_Item_ReturnedId = Item_Rec_Id;
                }
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsReturned();
                model.UO_GRet_Sr = Sr;



                model.UO_GRet_Item_ReturnedId = dataToEdit.ItemReceivedID;
                model.UO_GRet_RnItem_Name = dataToEdit.Item_Name;
                // model.UO_GRet_RnHead = dataToEdit.Head;
                //   model.UO_GRet_RnMake = dataToEdit.Make;
                //   model.UO_GRet_RnModelName = dataToEdit.Model;
                //     model.UO_GRet_RnItem_Code = dataToEdit.Item_Code;
                model.UO_GRet_Returned_Qty = dataToEdit.Returned_Qty;
                //   model.UO_GRet_RnUnit = dataToEdit.Unit;
                //    model.UO_GRet_RnItem_Type = dataToEdit.Item_Type;
                model.UO_GRet_Returned_Date = dataToEdit.Return_Date;
                //  model.UO_GRet_RnLot_No = dataToEdit.Lot_No;
                model.UO_GRet_Returned_ModeId = dataToEdit.ModeID;
                model.UO_GRet_Returned_Mode = dataToEdit.Return_Mode;
                model.UO_GRet_RnCarrier = dataToEdit.Carrier;
                model.UO_GRet_Returned_LocationId = dataToEdit.ReturnLocationId;
                model.UO_GRet_Returned_Location = dataToEdit.Received_Location;
                model.UO_GRet_Returned_By_Id = dataToEdit.ReturnedByID;

                model.UO_GRet_RnRemarks = dataToEdit.Remarks;
                model.UO_GRet_ID = dataToEdit.ID;
            }

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_User_Order_Returned(UserOrder_Returned model)
        {
            if (model.UO_GRet_ActionMethod == "New" | model.UO_GRet_ActionMethod == "Edit")
            {
                if (model.UO_GRet_Returned_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Returned Qty can not be Negative or Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UO_GRet_Returned_Qty > model.UO_GRet_pendingQty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Returned Qty can not be greater than Received Qty ...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                int value = DateTime.Compare(model.UO_GRet_Returned_Date, model.UO_GRet_Received_RnDate);
                if (value < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Returned Date can not be earlier then Received Date ...!"
                    }, JsonRequestBehavior.AllowGet);
                }//mantis #1567
            }

            if (model.UOid != 0)
            {
                try
                {
                    Param_Insert_UO_Item_Returned inparam = new Param_Insert_UO_Item_Returned();
                    inparam.UO_ID = model.UOid;
                    inparam.Returned_Qty = model.UO_GRet_Returned_Qty;
                    inparam.Returned_Date = model.UO_GRet_Returned_Date;
                    inparam.Delivery_Mode_ID = model.UO_GRet_Returned_ModeId;
                    inparam.Delivery_Carrier = model.UO_GRet_RnCarrier;
                    inparam.Returned_Location_ID = model.UO_GRet_Returned_LocationId;
                    inparam.Returned_By_ID = model.UO_GRet_Returned_By_Id;
                    inparam.Returned_by_Remarks = model.UO_GRet_RnRemarks;
                    inparam.Recd_Item_ID = Convert.ToInt32(model.UO_GRet_Item_ReturnedId);


                    if (BASE._user_order_DBOps.Insert_UO_Item_Returned(inparam))
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
            else {
                List<Return_GetUOGoodsReturned> gridRows = new List<Return_GetUOGoodsReturned>();

                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (UO_GoodsRetnData != null)
                {
                    gridRows = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.UO_GRet_ActionMethod == "New")
                {
                    Return_GetUOGoodsReturned grid = new Return_GetUOGoodsReturned();
                    grid.Sr = NewSr;

                    grid.ItemReceivedID = Convert.ToInt32(model.UO_GRet_Item_ReturnedId);
                    grid.Item_Name = model.UO_GRet_RnItem_Name;
                    grid.Item_Code = model.UO_GRet_RnItem_Code;
                    grid.Item_Type = model.UO_GRet_RnItem_Type;
                    grid.Head = model.UO_GRet_RnHead;
                    grid.Make = model.UO_GRet_RnMake;
                    grid.Model = model.UO_GRet_RnModelName;
                    grid.Returned_Qty = model.UO_GRet_Returned_Qty;
                    grid.Return_Date = model.UO_GRet_Returned_Date;
                    grid.Unit = model.UO_GRet_RnUnit;
                    grid.Lot_No = model.UO_GRet_RnLot_No;
                    grid.Return_Mode = model.UO_GRet_Returned_Mode;
                    grid.Received_Location = model.UO_GRet_Returned_Location;
                    grid.Carrier = model.UO_GRet_RnCarrier;
                    grid.ReturnLocationId = model.UO_GRet_Returned_LocationId;
                    grid.Remarks = model.UO_GRet_RnRemarks;
                    grid.ModeID = model.UO_GRet_Returned_ModeId;
                    grid.ReturnedByID = model.UO_GRet_Returned_By_Id;

                    grid.ID = 0;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);
                }

                else if (model.UO_GRet_ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.UO_GRet_Sr);
                    if (dataToEdit.ID != 0)
                    {
                        var recdcount = BASE._user_order_DBOps.GetUOReturn_Received_EntryCount(dataToEdit.ID);
                        if (recdcount > 0)
                        {

                            return Json(new
                            {
                                result = false,
                                message = "Return Entry against which receipt has been posted can not be edited"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    dataToEdit.ItemReceivedID = Convert.ToInt32(model.UO_GRet_Item_ReturnedId);
                    dataToEdit.Item_Name = model.UO_GRet_RnItem_Name;
                    dataToEdit.Item_Code = model.UO_GRet_RnItem_Code;
                    dataToEdit.Item_Type = model.UO_GRet_RnItem_Type;
                    dataToEdit.Head = model.UO_GRet_RnHead;
                    dataToEdit.Make = model.UO_GRet_RnMake;
                    dataToEdit.Model = model.UO_GRet_RnModelName;
                    dataToEdit.Returned_Qty = model.UO_GRet_Returned_Qty;
                    dataToEdit.Return_Date = model.UO_GRet_Returned_Date;
                    dataToEdit.Unit = model.UO_GRet_RnUnit;
                    dataToEdit.Lot_No = model.UO_GRet_RnLot_No;
                    dataToEdit.Return_Mode = model.UO_GRet_Returned_Mode;
                    dataToEdit.Received_Location = model.UO_GRet_Returned_Location;
                    dataToEdit.Carrier = model.UO_GRet_RnCarrier;
                    dataToEdit.ReturnLocationId = model.UO_GRet_Returned_LocationId;
                    dataToEdit.Remarks = model.UO_GRet_RnRemarks;
                    dataToEdit.ModeID = model.UO_GRet_Returned_ModeId;
                    dataToEdit.ReturnedByID = model.UO_GRet_Returned_By_Id;
                    if (model.UO_GRet_ID != 0)
                    {
                        var editeditUOGoodsRetnID = new ArrayList();
                        var editGoodsRet = UOEdit_GoodsRet_ID as ArrayList;
                        if (editGoodsRet != null)
                        {
                            editGoodsRet.Add(model.UO_GRet_ID);
                            UOEdit_GoodsRet_ID = editGoodsRet;
                        }
                        else
                        {
                            editeditUOGoodsRetnID.Add(model.UO_GRet_ID);
                            UOEdit_GoodsRet_ID = editeditUOGoodsRetnID;
                        }
                    }
                }
                UO_GoodsRetnData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult All_Saved_Returned_Item_Exists_in_Grid(int RetID = 0, int uoid = 0, int srno = 0)
        {
            var ID = uoid;
            var SrNO = srno;
            if (uoid != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {


                var all_data_Of_Returned_Grid = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
                if (all_data_Of_Returned_Grid != null)
                {

                    if (all_data_Of_Returned_Grid.Count != 0)
                    {
                        List<Return_GetUOGoodsReturned> datatocheckret = all_data_Of_Returned_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);
                        List<Return_GetUOGoodsReturned> datatocheckedit = all_data_Of_Returned_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);
                        List<Return_GetUOGoodsReturned> datatocheckdelete = all_data_Of_Returned_Grid.FindAll(x => x.ID != 0);

                        if (datatocheckret.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckret.Count() - 1; I++)
                            {

                                if (datatocheckret[I].ItemReceivedID == RetID)
                                {


                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit existing row."
                                    }, JsonRequestBehavior.AllowGet);


                                }
                            }
                        }
                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].ItemReceivedID == RetID)
                                {

                                    if (UOEdit_GoodsRet_ID != null)
                                    {
                                        return Json(new
                                        {
                                            result = false,
                                            message = "Edited saved row for this item is already in Grid, please save UO first to edit this row."
                                        }, JsonRequestBehavior.AllowGet);

                                    }



                                }
                            }
                        }

                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].ItemReceivedID == RetID)
                                {

                                    if (Delete_User_GoodsRetn_ID != null)
                                    {
                                        var deletedID = DeletedRecd_ID as ArrayList; //mantis bug 1225
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].ItemReceivedID == Convert.ToInt32(deletedID[i]))
                                                {

                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save UO first for any Addition/Updation."
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
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Check_Unsaved_Item_Entry_in_Received_Grid_Return_Grid(int UDSID = 0) //delivery id to be changed
        {


            var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReceived> datatocheckrec = all_data_Of_Received_Grid.FindAll(x => x.UDS_ID == UDSID && x.ID != 0); //to be tested


                    if (datatocheckrec.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckrec.Count() - 1; I++)
                        {

                            if (UOEdit_GoodsRec_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Received Grid, please save UO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }

            if (Delete_User_GoodsRecd_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.No items should be deleted
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in  Received Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_Unsaved_Item_Entry_in_RetRecd_and_Recd_Grid(int RecID = 0)
        {


            var all_data_Of_Returnedrecd_Grid = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
            if (all_data_Of_Returnedrecd_Grid != null)
            {

                if (all_data_Of_Returnedrecd_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReturnReceived> datatocheckret = all_data_Of_Returnedrecd_Grid.FindAll(x => x.ReturnEntryID == RecID && x.ID == 0);


                    if (datatocheckret.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in Returned Received Grid, please save it to Edit/Delete this row."
                        }, JsonRequestBehavior.AllowGet);


                    }
                }


            }

            var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReceived> datatocheckrecd = all_data_Of_Received_Grid.FindAll(x => x.ReturnedEntryID == RecID && x.ID == 0);


                    if (datatocheckrecd.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in Received Grid, please save it to Edit/Delete this row."
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
        public ActionResult Frm_GoodsRetn_DetailWin_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Goods_Ret_Id = 0)
        {


            var allSrcDataGRet = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
            var dataToDelete = allSrcDataGRet != null ? allSrcDataGRet.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUOGoodsReturned();
            var ID = dataToDelete.ID;
            var del_recdid = dataToDelete.ItemReceivedID;
            if (ID != 0)
            {
                var recdcount = BASE._user_order_DBOps.GetUOReturn_Received_EntryCount(dataToDelete.ID);
                if (recdcount > 0)
                {

                    return Json(new
                    {
                        result = false,
                        message = "Return Entry against which Return Receipt has been posted can not be deleted"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allSrcDataGRet != null)
            {
                allSrcDataGRet.Remove(dataToDelete);
            }
            UO_GoodsRetnData = allSrcDataGRet;

            var deleteScrapID = new ArrayList();

            var deleteRecdID = new ArrayList();

            if (Goods_Ret_Id != 0)
            {
                var deleteRecd = DeletedRecd_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item

                var deleteScrap = Delete_User_GoodsRetn_ID as ArrayList;
                if (deleteScrap != null)
                {
                    deleteScrap.Add(Goods_Ret_Id);
                    Delete_User_GoodsRetn_ID = deleteScrap;

                    deleteRecd.Add(del_recdid);
                    DeletedRecd_ID = deleteRecd;
                }
                else
                {
                    deleteScrapID.Add(Goods_Ret_Id);
                    Delete_User_GoodsRetn_ID = deleteScrapID;

                    deleteRecdID.Add(del_recdid);
                    DeletedRecd_ID = deleteRecdID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Order_Returned All

        [HttpGet]
        public ActionResult Frm_User_Order_Returned_All(string ActionMethod = null, int SR = 0, int UOid = 0)
        {
            UOAddLocation_user_rights();
            UserOrder_Returned_All model = new UserOrder_Returned_All();
            model.UOid = UOid;
            model.UO_GRetA_ActionMethod = "New";
            if (ActionMethod == "New")
            {
                model.UO_GRetA_Returned_Date = DateTime.Now;
                model.UO_GRetA_Returned_By_Id = BASE._open_User_PersonnelID;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_User_Order_Returned_All(UserOrder_Returned_All model)
        {
            if (model.UOid != 0)
            {
                List<Return_GetUOGoodsReceived> UOGRetItem = BASE._user_order_DBOps.GetUOGoodsReceived(model.UOid);
                if (UOGRetItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "No More Pending Received to be returned"
                    }, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    //  Param_Insert_UO_Item_Returned inparam = new Param_Insert_UO_Item_Returned();

                    Param_GetUOGoodsReturnAllPending param = new Param_GetUOGoodsReturnAllPending();
                    param.UO_ID = model.UOid;
                    param.Returned_Date = model.UO_GRetA_Returned_Date;
                    param.Delivery_Mode_ID = model.UO_GRetA_Returned_ModeId;
                    param.Delivery_Carrier = model.UO_GRetA_RnCarrier;
                    param.Returned_Location_ID = model.UO_GRetA_Returned_LocationId;
                    param.Returned_By_ID = model.UO_GRetA_Returned_By_Id;
                    param.Returned_by_Remarks = model.UO_GRetA_RnRemarks;


                    if (BASE._user_order_DBOps.InsertUOGoodsReturnAllPending(param))
                    {
                        return Json(new { result = "RightClickTrue", message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = "No More Pending Received to be returned" }, JsonRequestBehavior.AllowGet);
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
                List<Return_GetUOGoodsReceived> UOGRetItem = BASE._user_order_DBOps.GetUOGoodsReceived(Convert.ToInt32(UOID_Glob));

                if (UOGRetItem.Count == 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "No More Pending Received to be returned"
                    }, JsonRequestBehavior.AllowGet);
                }
                List<Return_GetUOGoodsReturned> all_data = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
                Return_GetUOGoodsReturned dataToEdit = new Return_GetUOGoodsReturned();
                if (all_data != null)
                {
                    dataToEdit = all_data.Where(x => x.ID == 0).FirstOrDefault();
                }

                if (dataToEdit != null || UOEdit_GoodsRet_ID != null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Pls. save the existing New/ Edited rows in returned grid first...!"
                    }, JsonRequestBehavior.AllowGet);

                }

                if (model.UO_GRetA_ActionMethod == "New")
                {


                    Param_GetUOGoodsReturnAllPending inparam = new Param_GetUOGoodsReturnAllPending();
                    inparam.UO_ID = Convert.ToInt32(UOID_Glob);
                    inparam.Returned_Date = model.UO_GRetA_Returned_Date;
                    inparam.Delivery_Mode_ID = model.UO_GRetA_Returned_ModeId;
                    inparam.Delivery_Carrier = model.UO_GRetA_RnCarrier;
                    inparam.Returned_Location_ID = model.UO_GRetA_Returned_LocationId;
                    inparam.Returned_By_ID = model.UO_GRetA_Returned_By_Id;
                    inparam.Returned_by_Remarks = model.UO_GRetA_RnRemarks;

                    List<Return_GetUOGoodsReturnedAllPending> retn_all_pending = BASE._user_order_DBOps.GetUOGoodsReturnAllPending(inparam);



                    var gridRowsCount = 0;
                    var LastRowSr = 0;
                    var NewSr = LastRowSr;
                    List<Return_GetUOGoodsReturned> gridRows = new List<Return_GetUOGoodsReturned>();
                    if (UO_GoodsRetnData != null)
                    {
                        gridRows = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
                        gridRowsCount = gridRows.Count;
                        LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                        NewSr = LastRowSr;
                    }


                    if (retn_all_pending != null)
                    {
                        for (int I = 0; I <= retn_all_pending.Count() - 1; I++)
                        {
                            Return_GetUOGoodsReturned grid = new Return_GetUOGoodsReturned();
                            NewSr = NewSr + 1;
                            grid.Sr = NewSr;

                            grid.ItemReceivedID = retn_all_pending[I].ItemReceivedID;
                            grid.Item_Name = retn_all_pending[I].Item_Name;
                            grid.Item_Code = retn_all_pending[I].Item_Code;
                            grid.Item_Type = retn_all_pending[I].Item_Type;
                            grid.Head = retn_all_pending[I].Head;
                            grid.Make = retn_all_pending[I].Make;
                            grid.Model = retn_all_pending[I].Model;
                            grid.Returned_Qty = retn_all_pending[I].Returned_Qty;
                            grid.Return_Date = retn_all_pending[I].Return_Date;
                            grid.Unit = retn_all_pending[I].Unit;
                            grid.Lot_No = retn_all_pending[I].Lot_No;
                            grid.Return_Mode = retn_all_pending[I].Return_Mode;
                            grid.Received_Location = retn_all_pending[I].Received_Location;
                            grid.Carrier = retn_all_pending[I].Carrier;
                            grid.ReturnLocationId = retn_all_pending[I].ReturnLocationId;
                            grid.Remarks = retn_all_pending[I].Remarks;
                            grid.ModeID = retn_all_pending[I].ModeID;
                            grid.ReturnedByID = retn_all_pending[I].ReturnedByID;
                            grid.ID = 0;
                            grid.Added_On = DateTime.Now;
                            grid.Added_By = BASE._open_User_ID;
                            gridRows.Add(grid);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            result = true,
                            message = "No More Pending Received to be Returned"
                        }, JsonRequestBehavior.AllowGet);

                    }
                    UO_GoodsRetnData = gridRows;
                }


                return Json(new
                {
                    result = true,
                    message = "Added Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Order_Return_Received
        public ActionResult LookUp_Get_Item_Return_Received(DataSourceLoadOptions loadOptions, int? RetRecd_ID)
        {
            if (RetRecd_ID != 0)
            {
                var UOGRetrecItem = BASE._user_order_DBOps.GetUORetReceivableItems(Convert.ToInt32(UOID_Glob), Convert.ToInt32(RetRecd_ID));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRetrecItem, loadOptions)), "application/json");
            }
            else
            {
                var UOGRetrecItem = BASE._user_order_DBOps.GetUORetReceivableItems(Convert.ToInt32(UOID_Glob));
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRetrecItem, loadOptions)), "application/json");
            }
        }
        public ActionResult LookUp_Get_Return_Received_Mode(DataSourceLoadOptions loadOptions)
        {
            var UOGRetrecMODE = BASE._user_order_DBOps.GetDeliveryModes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UOGRetrecMODE, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Return_Received_Location(DataSourceLoadOptions loadOptions, int RetRecReqStoreID = 0)
        {
            var UORetRecdLoc = BASE._Stock_Profile_DBOps.GetLocations(RetRecReqStoreID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UORetRecdLoc, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Return_Received_By(DataSourceLoadOptions loadOptions, int RetRecStoreID = 0)
        {
            var UORet_rec_RecdBy = BASE._user_order_DBOps.GetPersonnels(RetRecStoreID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(UORet_rec_RecdBy, loadOptions)), "application/json");
        }
        public ActionResult UO_GoodsReturned_ReceivedGridData(string ActionMethodName, int UOID = 0)
        {
            var UORetRecdList = new List<Return_GetUOGoodsReturnReceived>();
            if (ActionMethodName == "New")
            {
                return PartialView(UORetRecdList);
            }
            if (UO_GoodsRetRecdData == null)
            {
                var UOGoodsRetRecData = BASE._user_order_DBOps.GetUOGoodsReturnReceived(UOID);
                if (UOGoodsRetRecData != null)
                {
                    UORetRecdList = UOGoodsRetRecData;
                }
                UO_GoodsRetRecdData = UORetRecdList;
            }
            return PartialView(UO_GoodsRetRecdData);
        }


        [HttpGet]
        public ActionResult Frm_User_Order_Return_Received(string ActionMethod = null, int SR = 0, int UOid = 0, string PostSuccessFunction = null, string PopupID = null, int Item_Rec_Id = 0)
        {
            UOAddLocation_user_rights();
            UserOrder_Return_Received model = new UserOrder_Return_Received();

            model.GRetRec_PostSuccessFunction = PostSuccessFunction != null ? PostSuccessFunction : "Frm_User_Order_ReturnedReceived_OnSuccess";
            model.GRetRec_PopUPId = PopupID != null ? PopupID : "UserOrderinnergrid";
            model.UO_GRetRec_ActionMethod = ActionMethod;
            if (UOid != 0)
            {
                model.UOid = UOid;
            }

            if (model.UOid != 0)
            {
                UOID_Glob = UOid;
            }
            if (ActionMethod == "New")
            {
                // model.UO_GRetRec_Received_By_Id = BASE._open_User_PersonnelID;
                model.UO_GRetRec_ReturnReceived_Date = DateTime.Now;
                if (Item_Rec_Id != 0)
                {
                    model.UO_GRetRec_Item_Return_ReceiveId = Item_Rec_Id;
                }
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOGoodsReturnReceived();

                model.UO_GRetRec_Sr = Sr;

                model.UO_GRetRec_Item_Name = dataToEdit.Item_Name;
                //  model.UO_GRetRec_Item_Code = dataToEdit.Item_Code;
                //  model.UO_GRetRec_Item_Type = dataToEdit.Item_Type;              
                //    model.UO_GRetRec_Make = dataToEdit.Make;
                //     model.UO_GRetRec_ModelName = dataToEdit.Model;
                model.UO_GRetRec_Return_Received_Qty = dataToEdit.Received_Qty;
                //   model.UO_GRetRec_Unit = dataToEdit.Unit;
                //    model.UO_GRetRec_Lot_No = dataToEdit.Lot_No;
                model.UO_GRetRec_ReturnReceived_Date = dataToEdit.Received_Date;
                model.UO_GRetRec_Received_ModeId = dataToEdit.ModeID;
                model.UO_GRetRec_Received_Mode = dataToEdit.Received_Mode;
                model.UO_GRetRec_Carrier = dataToEdit.Carrier;
                model.UO_GRetRec_Received_LocationId = dataToEdit.RecdLocationId;
                model.UO_GRetRec_Received_Location = dataToEdit.Received_Location;
                model.UO_GRetRec_Remarks = dataToEdit.Remarks;
                model.UO_GRetRec_ID = dataToEdit.ID;
                model.UO_GRetRec_Received_By_Id = dataToEdit.RecdByID;

                model.UO_GRetRec_DeliveryEntry_ID = dataToEdit.DeliveryEntryID;
                model.UO_GRetRec_ReturnedEntry_ID = dataToEdit.ReturnEntryID;
                if (dataToEdit.DeliveryEntryID != null)
                {
                    model.UO_GRetRec_Item_Return_ReceiveId = Convert.ToInt32(dataToEdit.DeliveryEntryID);
                }
                else if (dataToEdit.ReturnEntryID != null)
                {
                    model.UO_GRetRec_Item_Return_ReceiveId = Convert.ToInt32(dataToEdit.ReturnEntryID);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_User_Order_Return_Received(UserOrder_Return_Received model)
        {
            if (model.UO_GRetRec_ActionMethod == "New" | model.UO_GRetRec_ActionMethod == "Edit")
            {

                if (model.UO_GRetRec_Return_Received_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Return Received Qty can not be Negative or Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UO_GRetRec_Return_Received_Qty > model.UO_GRetRec_pendingQty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Return Received Qty can not be greater than Returned Qty ...!"
                    }, JsonRequestBehavior.AllowGet);
                }

                int value = DateTime.Compare(model.UO_GRetRec_ReturnReceived_Date, Convert.ToDateTime(model.UO_GRetRec_Return_Date));
                if (value < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Return Recd Date should not be earlier than Return Date ...!"
                    }, JsonRequestBehavior.AllowGet);
                }//mantis 1546 issue 8
            }
            if (model.UOid != 0)
            {
                try
                {
                    Param_Insert_UO_Item_Return_Received inparam = new Param_Insert_UO_Item_Return_Received();
                    inparam.UO_ID = model.UOid;
                    inparam.UO_Delivered_ID = model.UO_GRetRec_DeliveryEntry_ID;
                    inparam.Received_Qty = model.UO_GRetRec_Return_Received_Qty;
                    inparam.Received_Date = model.UO_GRetRec_ReturnReceived_Date;
                    inparam.Received_Mode_ID = model.UO_GRetRec_Received_ModeId;
                    inparam.Delivery_Carrier = model.UO_GRetRec_Carrier;
                    inparam.Ret_Rec_Location_ID = model.UO_GRetRec_Received_LocationId;
                    inparam.Received_By_ID = model.UO_GRetRec_Received_By_Id;
                    inparam.Receiver_Remarks = model.UO_GRetRec_Remarks;
                    inparam.UO_Returned_ID = model.UO_GRetRec_ReturnedEntry_ID;
                    inparam.URR_UDS_ID = model.UO_GRetRec_UDS_ID;


                    if (BASE._user_order_DBOps.Insert_UO_Item_Return_Received(inparam))
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
                List<Return_GetUOGoodsReturnReceived> gridRows = new List<Return_GetUOGoodsReturnReceived>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (UO_GoodsRetRecdData != null)
                {
                    gridRows = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.UO_GRetRec_ActionMethod == "New")
                {
                    Return_GetUOGoodsReturnReceived grid = new Return_GetUOGoodsReturnReceived();
                    grid.Sr = NewSr;

                    //  grid.Item_ReceivedId = model.UO_GR_Item_ReceivedId;
                    grid.Item_Name = model.UO_GRetRec_Item_Name;
                    grid.Item_Code = model.UO_GRetRec_Item_Code;
                    grid.Item_Type = model.UO_GRetRec_Item_Type;
                    grid.Make = model.UO_GRetRec_Make;
                    grid.Model = model.UO_GRetRec_ModelName;
                    grid.Received_Qty = model.UO_GRetRec_Return_Received_Qty;
                    grid.Unit = model.UO_GRetRec_Unit;
                    grid.Lot_No = model.UO_GRetRec_Lot_No;
                    grid.Received_Date = model.UO_GRetRec_ReturnReceived_Date;
                    grid.Received_Mode = model.UO_GRetRec_Received_Mode;
                    grid.ModeID = model.UO_GRetRec_Received_ModeId;
                    grid.Carrier = model.UO_GRetRec_Carrier;
                    grid.Received_Location = model.UO_GRetRec_Received_Location;
                    grid.RecdLocationId = model.UO_GRetRec_Received_LocationId;
                    grid.Remarks = model.UO_GRetRec_Remarks;

                    grid.RecdByID = model.UO_GRetRec_Received_By_Id;
                    grid.DeliveryEntryID = model.UO_GRetRec_DeliveryEntry_ID;
                    grid.ReturnEntryID = model.UO_GRetRec_ReturnedEntry_ID;
                    grid.UDS_ID = model.UO_GRetRec_UDS_ID;
                    grid.ID = 0;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);

                }

                else if (model.UO_GRetRec_ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.UO_GRetRec_Sr);

                    //dataToEdit.Item_ReceivedId = model.UO_GR_Item_ReceivedId;
                    dataToEdit.Item_Name = model.UO_GRetRec_Item_Name;
                    dataToEdit.Item_Code = model.UO_GRetRec_Item_Code;
                    dataToEdit.Item_Type = model.UO_GRetRec_Item_Type;
                    dataToEdit.Make = model.UO_GRetRec_Make;
                    dataToEdit.Model = model.UO_GRetRec_ModelName;
                    dataToEdit.Received_Qty = model.UO_GRetRec_Return_Received_Qty;
                    dataToEdit.Unit = model.UO_GRetRec_Unit;
                    dataToEdit.Lot_No = model.UO_GRetRec_Lot_No;
                    dataToEdit.Received_Date = model.UO_GRetRec_ReturnReceived_Date;
                    dataToEdit.Received_Mode = model.UO_GRetRec_Received_Mode;
                    dataToEdit.ModeID = model.UO_GRetRec_Received_ModeId;
                    dataToEdit.Carrier = model.UO_GRetRec_Carrier;
                    dataToEdit.Received_Location = model.UO_GRetRec_Received_Location;
                    dataToEdit.RecdLocationId = model.UO_GRetRec_Received_LocationId;
                    dataToEdit.Remarks = model.UO_GRetRec_Remarks;
                    dataToEdit.RecdByID = model.UO_GRetRec_Received_By_Id;
                    dataToEdit.DeliveryEntryID = model.UO_GRetRec_DeliveryEntry_ID;
                    dataToEdit.ReturnEntryID = model.UO_GRetRec_ReturnedEntry_ID;
                    dataToEdit.UDS_ID = model.UO_GRetRec_UDS_ID;
                    if (model.UO_GRetRec_ID != 0)
                    {
                        var editUOGoodsRetRecID = new ArrayList();
                        var editGoodsRetRec = UOEdit_GoodsRetRec_ID as ArrayList;
                        if (editGoodsRetRec != null)
                        {
                            editGoodsRetRec.Add(model.UO_GRetRec_ID);
                            UOEdit_GoodsRetRec_ID = editGoodsRetRec;
                        }
                        else
                        {
                            editUOGoodsRetRecID.Add(model.UO_GRetRec_ID);
                            UOEdit_GoodsRetRec_ID = editUOGoodsRetRecID;
                        }
                    }
                }
                UO_GoodsRetRecdData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Updated Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult All_Saved_ReturnReceived_Item_Exists_in_Grid(int retrecid = 0, int uoid = 0, int srno = 0)
        {

            var ID = uoid;
            var SrNO = srno;
            if (uoid != 0)
            {
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var all_data_Of_ReturnedRec_Grid = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
                if (all_data_Of_ReturnedRec_Grid != null)
                {
                    if (all_data_Of_ReturnedRec_Grid.Count != 0)
                    {
                        List<Return_GetUOGoodsReturnReceived> datatocheckretrec = all_data_Of_ReturnedRec_Grid.FindAll(x => x.ID == 0 && x.Sr != SrNO);
                        List<Return_GetUOGoodsReturnReceived> datatocheckedit = all_data_Of_ReturnedRec_Grid.FindAll(x => x.ID != 0 && x.Sr != SrNO);
                        List<Return_GetUOGoodsReturnReceived> datatocheckdelete = all_data_Of_ReturnedRec_Grid.FindAll(x => x.ID != 0);

                        if (datatocheckretrec.Count != 0)
                        {

                            for (int I = 0; I <= datatocheckretrec.Count() - 1; I++)
                            {

                                if (datatocheckretrec[I].UDS_ID == retrecid)
                                {

                                    return Json(new
                                    {
                                        result = false,
                                        message = "New unsaved Row for same item is already in Grid, please edit existing row."
                                    }, JsonRequestBehavior.AllowGet);


                                }
                            }
                        }
                        if (datatocheckedit.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckedit.Count() - 1; I++)
                            {

                                if (datatocheckedit[I].UDS_ID == retrecid)
                                {

                                    if (UOEdit_GoodsRetRec_ID != null)
                                    {
                                        return Json(new
                                        {
                                            result = false,
                                            message = "Edited saved row for this item is already in Grid, please save UO first to edit this row."
                                        }, JsonRequestBehavior.AllowGet);

                                    }



                                }
                            }
                        }

                        if (datatocheckdelete.Count != 0)
                        {
                            for (int I = 0; I <= datatocheckdelete.Count() - 1; I++)
                            {

                                if (datatocheckdelete[I].UDS_ID == retrecid)
                                {

                                    if (Delete_User_GoodsRetRecd_ID != null)
                                    {
                                        var deletedID = DeletedUDSRetRec_ID as ArrayList; //mantis bug 1225
                                        if (deletedID != null)
                                        {
                                            for (int i = 0; i <= deletedID.Count - 1; i++)
                                            {
                                                if (datatocheckdelete[I].UDS_ID == Convert.ToInt32(deletedID[i]))

                                                {

                                                    return Json(new
                                                    {
                                                        result = false,
                                                        message = "A row has been Deleted for this item, please save UO first for any Addition/Updation."
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
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Check_Unsaved_Item_Entry_in_Return_Grid_for_RetRecd(int returnID = 0)
        {


            var all_data_Of_Returned_Grid = (List<Return_GetUOGoodsReturned>)UO_GoodsRetnData;
            if (all_data_Of_Returned_Grid != null)
            {

                if (all_data_Of_Returned_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReturned> datatocheckret = all_data_Of_Returned_Grid.FindAll(x => x.ID == returnID && x.ID != 0); //to be tested


                    if (datatocheckret.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckret.Count() - 1; I++)
                        {

                            if (UOEdit_GoodsRet_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Returned Grid, please save UO first to Add new row."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }
            if (Delete_User_GoodsRetn_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.No items should be deleted
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in  Returned Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Check_Unsaved_Item_Entry_in_Received_Grid_for_RetRecd(int UDSid = 0)
        {


            var all_data_Of_Received_Grid = (List<Return_GetUOGoodsReceived>)UO_GoodsRecdData;
            if (all_data_Of_Received_Grid != null)
            {

                if (all_data_Of_Received_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsReceived> datatocheckrec = all_data_Of_Received_Grid.FindAll(x => x.UDS_ID == UDSid && x.ID == 0);
                    List<Return_GetUOGoodsReceived> datatocheckrecedit = all_data_Of_Received_Grid.FindAll(x => x.UDS_ID == UDSid && x.ID != 0);

                    if (datatocheckrec.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in  Received Grid, please save it to Add/Edit this item."
                        }, JsonRequestBehavior.AllowGet);


                    }

                    if (datatocheckrecedit.Count != 0)
                    {
                        for (int I = 0; I <= datatocheckrecedit.Count() - 1; I++)
                        {

                            if (UOEdit_GoodsRec_ID != null)
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = "Edited saved row for this item is already in Received Grid, please save UO first to Add/Edit this item."
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }


                    }
                }


            }
            if (Delete_User_GoodsRecd_ID != null) //this delete checks for even if all the rows are deleted and no items exist in grid.No items should be deleted
            {
                return Json(new
                {
                    result = false,
                    message = "Deleted row is already in  Received Grid, please save UO first to Add new row."
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult PendingQtyCalculate(int ID = 0, decimal PendingQty = 0)
        //{
        //    var all_data_Of_ReturnedRec_Grid = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
        //    if (all_data_Of_ReturnedRec_Grid != null)
        //    {
        //        List<Return_GetUOGoodsReturnReceived> RetEntry = all_data_Of_ReturnedRec_Grid.FindAll(x => x.ID == ID);

        //         RetEntry.re

        //        for (int I = 0; I <= datatocheckretrec.Count() - 1; I++)
        //        {

        //            if (datatocheckretrec[I].ReturnEntryID == retrecid || datatocheckretrec[I].DeliveryEntryID == retrecid)
        //            {
        //                return Json(new
        //                {
        //                    result = false,
        //                    message = "New unsaved Row for same item is already in Grid, please edit existing row."
        //                }, JsonRequestBehavior.AllowGet);

        //            }
        //        }
        //    }


        //    return Json(new
        //    {
        //        Message = "",
        //        result = true
        //    }, JsonRequestBehavior.AllowGet);


        //}
        public ActionResult ReturnedReceived_grid_oldest_returned(string[] UO_ItemID, int UOid = 0)
        {
            var uoid = UOid;
            if (uoid != 0)
            {
                UOID_Glob = uoid;
            }
            List<Return_GetUORetReceivableItems> UOGRetrecItem = BASE._user_order_DBOps.GetUORetReceivableItems(Convert.ToInt32(UOID_Glob));


            if (UO_ItemID != null)
            {
                if (UO_ItemID.Length == 1)
                {
                    List<Return_GetUORetReceivableItems> selecteditem = UOGRetrecItem.FindAll(x => x.UOI_ID.ToString() == UO_ItemID[0].ToString());


                    if (selecteditem.Count == 0)
                    {
                        return Json(new
                        {
                            message = "Item not saved/ Selected Item has no more items to be return-received...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        var mindate = selecteditem.Min(x => x.Added_On);
                        var itemidj = selecteditem.FindAll(x => x.Added_On.Equals(mindate)).FirstOrDefault();
                        var itemid = itemidj.ID;
                        return Json(new { selID = itemid, result = true }, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }

            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Return-Receive ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_GoodsRetRecd_DetailWin_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Goods_RetRec_Id = 0)
        {


            var allSrcDataGRetRec = (List<Return_GetUOGoodsReturnReceived>)UO_GoodsRetRecdData;
            var dataToDelete = allSrcDataGRetRec != null ? allSrcDataGRetRec.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUOGoodsReturnReceived();
            var del_udsidretrec = dataToDelete.UDS_ID;
            //if (dataToDelete.ID != 0)
            //{
            //    var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.StockID, ClientScreen.Stock_UO);
            //    if (itemcheck.Count > 0)
            //    {
            //        var inusescreen = itemcheck[0].Screen;
            //        return Json(new
            //        {
            //            result = false,
            //            message = "Entry for Return Received Cannot Be Deleted Because It has Been Used In" + inusescreen + "...!"
            //        }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            if (allSrcDataGRetRec != null)
            {
                allSrcDataGRetRec.Remove(dataToDelete);
            }
            UO_GoodsRetRecdData = allSrcDataGRetRec;

            var deleteScrapretrecID = new ArrayList();
            var deleteUDSRetRecID = new ArrayList();

            if (Goods_RetRec_Id != 0)
            {
                var deleteUDSRetRec = DeletedUDSRetRec_ID as ArrayList; //for new restriction of DELETE to check whether partial entry exists of deleted item

                var deleteGoodsRetRec = Delete_User_GoodsRetRecd_ID as ArrayList;
                if (deleteGoodsRetRec != null)
                {
                    deleteGoodsRetRec.Add(Goods_RetRec_Id);
                    Delete_User_GoodsRetRecd_ID = deleteGoodsRetRec;

                    deleteUDSRetRec.Add(del_udsidretrec);
                    DeletedUDSRetRec_ID = deleteUDSRetRec;
                }
                else
                {
                    deleteScrapretrecID.Add(Goods_RetRec_Id);
                    Delete_User_GoodsRetRecd_ID = deleteScrapretrecID;

                    deleteUDSRetRecID.Add(del_udsidretrec);
                    DeletedUDSRetRec_ID = deleteUDSRetRecID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Document
        public ActionResult UO_DocumentsGridData(string ActionMethodName, int UOID = 0)

        {
            var docList = new List<Return_GetDocumentsGridData>();
            if (ActionMethodName == "New")
            {
                return PartialView(docList);
            }
            if (UserOrder_Documents_Window_Grid_Data == null)
            {
                var docData = BASE._user_order_DBOps.GetUODocuments(UOID);
                if (docData != null)
                {
                    docList = docData;
                }
                UserOrder_Documents_Window_Grid_Data = docList;
            }
            if (((List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data).Count > 0)
            {
                for (int i = 0; i < ((List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data).Count; i++)
                {
                    ((List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data)[i].Sr_No = i + 1;
                }
            }
            return PartialView(UserOrder_Documents_Window_Grid_Data);
        }

        public ActionResult UserOrder_Documents_Attachment()
        {
            Model_Attachment_Window model = (Model_Attachment_Window)GetBaseSession("UserOrder_Documents_Attachment_AttachmentData");



            if (model.Help_REF_REC_ID != null)
            {
                try
                {
                    Parameter_Insert_Attachment InEInfo = new Parameter_Insert_Attachment();

                    InEInfo.FileName = model.Help_Document_FileName;
                    InEInfo.Description = model.Help_Document_Description;
                    InEInfo.NameID = model.Help_Document_NameID;
                    InEInfo.Ref_Screen = "UO";
                    InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                    InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                    InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                    InEInfo.File = model.Help_filefield;
                    InEInfo.RecID = System.Guid.NewGuid().ToString();
                    if (BASE._Attachments_DBOps.Insert(InEInfo).Length > 0)
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

                if (UserOrder_Documents_Window_Grid_Data != null)
                {
                    gridRows = (List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data;
                }

                if (model.Help_Doc_From_Date > model.Help_Doc_To_Date)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Applicable From Date should be smaller than Applicable To Date"
                    }, JsonRequestBehavior.AllowGet);
                }

                //var gridRowsCount = 0;
                //var LastRowSr = 0;
                //var NewSr = LastRowSr + 1;
                //if (UserOrder_Documents_Window_Grid_Data != null)
                //{
                //    gridRows = (List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data;
                //    gridRowsCount = gridRows.Count;
                //    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr_No) : 0;
                //    NewSr = LastRowSr + 1;
                //}
                if (model.ActionMethod == "New")
                {
                    Return_GetDocumentsGridData grid = new Return_GetDocumentsGridData();
                    //grid.Sr_No = NewSr;
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
                        var editDocument = UserOrderEdit_Document_ID as ArrayList;
                        if (editDocument != null)
                        {
                            editDocument.Add(model.ID);
                            UserOrderEdit_Document_ID = editDocument;
                        }
                        else
                        {
                            editDocumentID.Add(model.ID);
                            UserOrderEdit_Document_ID = editDocumentID;
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
                UserOrder_Documents_Window_Grid_Data = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult UserOrder_Documents_Attachment_LinkCheck(string DocId, int UOId)
        {
            var screen = BASE._user_order_DBOps.GetAttachmentLinkScreen(UOId, DocId);
            // var screen = BASE._Projects_Dbops.GetAttachmentLinkScreen(UOId, DocId); 
            if (!string.IsNullOrEmpty(screen))
            {
                //    if (screen.Length > 0)
                //{
                if (screen != "UO")
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From UO..?"
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
            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)UserOrder_Documents_Window_Grid_Data;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (dataToDelete != null)
            {
                allDocData.Remove(dataToDelete);
            }
            UserOrder_Documents_Window_Grid_Data = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    var deleteDocumentID = new ArrayList();
                    var deleteDocument = UserOrderDelete_Document_ID as ArrayList;
                    if (deleteDocument != null)
                    {
                        deleteDocument.Add(Doc_ID);
                        UserOrderDelete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID.Add(Doc_ID);
                        UserOrderDelete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {

                var unlinkDocumentID = new ArrayList();
                var unlinkDocument = UO_Unlink_Document_ID as ArrayList;
                if (unlinkDocument != null)
                {
                    unlinkDocument.Add(Doc_ID);
                    UO_Unlink_Document_ID = unlinkDocument;
                }
                else
                {
                    unlinkDocumentID.Add(Doc_ID);
                    UO_Unlink_Document_ID = unlinkDocumentID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion Documents

        #region Existing Remarks
        public ActionResult UOExistingRemarksGridData(string ActionMethodName, int UOID = 0)
        {
            NEVD_UserOrder model = new NEVD_UserOrder();

            var UORemarksList = new List<Return_GetUORemarks>();
            if (ActionMethodName == "New")
            {
                return PartialView(UORemarksList);
            }
            if (UO_ExisitngRemarksData == null)
            {
                var RemarksData = BASE._user_order_DBOps.GetUORemarks(UOID);
                if (RemarksData != null)
                {
                    UORemarksList = RemarksData;
                }
                UO_ExisitngRemarksData = UORemarksList;
            }
            return PartialView(UO_ExisitngRemarksData);
        }
        public ActionResult Frm_UserOrderExistingRemarks_Delete_Grid_Record(string ActionMethod, int? ID = 0)
        {
            var id = Convert.ToInt16(ID);
            var allData = (List<Return_GetUORemarks>)UO_ExisitngRemarksData;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == id).FirstOrDefault() : new Return_GetUORemarks();
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
            UO_ExisitngRemarksData = allData;

            if (id != 0)
            {
                var deleteRemarksID = new ArrayList();
                var deleteExistingRemarks = Delete_UOExisting_Remarks_ID as ArrayList;
                if (deleteExistingRemarks != null)
                {
                    deleteExistingRemarks.Add(id);
                    Delete_UOExisting_Remarks_ID = deleteExistingRemarks;
                }
                else
                {
                    deleteRemarksID.Add(id);
                    Delete_UOExisting_Remarks_ID = deleteRemarksID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_UOViewRemarks(int SR = 0)
        {
            var all_data = (List<Return_GetUORemarks>)UO_ExisitngRemarksData;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetUORemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("Frm_UOViewRemarks", ViewBag.ViewRemarks);
        }
        #endregion

        #region Scrap Create
        public ActionResult Scrap_Item_Name_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            Common_Lib.RealTimeService.Param_GetStockItems inparam = new Common_Lib.RealTimeService.Param_GetStockItems();
            inparam.Main_Category = "Scrap";
            var ScrapItemlist = BASE._Sub_Item_DBOps.GetStockItems(inparam);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ScrapItemlist, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Scrap_Location_List(bool? IsVisible, DataSourceLoadOptions loadOptions, int uoscrapStoreID = 0)
        {

            var ScrapLocationlist = BASE._Stock_Profile_DBOps.GetLocations(uoscrapStoreID);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ScrapLocationlist, loadOptions)), "application/json");
        }
        public ActionResult UO_ScrapCreatedGridData(string ActionMethodName, int UOID = 0)
        {


            var UOScrapList = new List<Return_GetUOScrapCreated>();
            if (ActionMethodName == "New")
            {
                return PartialView(UOScrapList);
            }
            if (UO_ScrapData == null)
            {
                var UOScrapdata = BASE._user_order_DBOps.GetUOScrapCreated(UOID);

                if (UOScrapdata != null)
                {
                    UOScrapList = UOScrapdata;
                }
                UO_ScrapData = UOScrapList;
            }
            return PartialView(UO_ScrapData);
        }

        public ActionResult Frm_User_Order_Scrap(string ActionMethod = null, int SR = 0, int UOid = 0, string UO_number = null, int Requestee_Store_ID = 0)
        {
            ViewData["UO_AddStockItemRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");
            ViewData["UO_AddHelpRequestRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Add");

            UOScrapDetail model = new UOScrapDetail();
            model.UOScrapActionMethod = ActionMethod;
            if (UOid != 0)
            {
                model.UOid = UOid;
            }
            if (model.UOid != 0)
            {
                UOID_Glob = UOid;
            }
            model.UO_number = UO_number;
            model.UORequestee_Store_Id = Requestee_Store_ID;
            if (ActionMethod == "New")
            {

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUOScrapCreated>)UO_ScrapData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUOScrapCreated();
                model.UOScrap_Sr = Sr;
                model.UOScrap_ItemId = dataToEdit.ItemID;
                model.UOScrap_LocationID = dataToEdit.LocationID;
                model.UOScrap_Qty = Convert.ToInt32(dataToEdit.Qty);
                model.UOScrap_Amount = Convert.ToDouble(dataToEdit.Amount);
                model.UOScrap_Remarks = dataToEdit.Remarks;
                model.UOScrapID = dataToEdit.ID;
            }
            return View(model);
        }

        public ActionResult User_Order_Scrap(UOScrapDetail model)
        {
            if (model.UOScrapActionMethod == "New" | model.UOScrapActionMethod == "Edit")
            {
                if (string.IsNullOrEmpty(model.UOScrap_ItemId.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Item Name Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.UOScrap_Unit))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Unit Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.UOScrap_LocationID.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Location  Name Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.UOScrap_Qty.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Qty Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.UOScrap_Amount.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Amount Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UOScrap_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Qty should be greater than Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.UOScrap_Amount <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Amount should be greater than Zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.UOScrapActionMethod == "Edit")
            {
                if (model.UOScrapID != 0)
                {
                    var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(model.UOScrapID, ClientScreen.Stock_UO);
                    if (itemcheck.Count > 0)
                    {
                        var inusescreen = itemcheck[0].Screen;
                        return Json(new
                        {
                            result = false,
                            message = "Scrap Cannot Be Edited Because It has Been Used In" + inusescreen + "...!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            if (model.UOid != 0)
            {
                try
                {
                    Param_Add_Stock_Addition inparam = new Param_Add_Stock_Addition();
                    inparam.Store_Dept_ID = Convert.ToInt32(model.UORequestee_Store_Id);
                    inparam.item_id = Convert.ToInt32(model.UOScrap_ItemId);
                    inparam.make = "UO - Scrap";
                    inparam.model = "UO - Scrap";
                    inparam.serial_no = "UO - Scrap" + model.UO_number;
                    inparam.Quantity = model.UOScrap_Qty;
                    inparam.Unit_Id = model.UOScrapUnitID;
                    inparam.Date_Of_Purchase = DateTime.Now;
                    inparam.total_value = model.UOScrap_Amount;
                    inparam.Remarks = model.UOScrap_Remarks;
                    inparam.Stock_Ref_ID = model.UOid;
                    inparam.Location_Id = model.UOScrap_LocationID;
                    inparam.Stock_Ref_Entry_Source = Stock_Addition_Source.User_Order_Scrap_Screen;


                    if (BASE._Stock_Profile_DBOps.AddStockAddition(inparam, ClientScreen.Stock_UO))
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
                List<Return_GetUOScrapCreated> gridRows = new List<Return_GetUOScrapCreated>();

                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;

                if (UO_ScrapData != null)
                {
                    gridRows = (List<Return_GetUOScrapCreated>)UO_ScrapData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.UOScrapActionMethod == "New")
                {
                    Return_GetUOScrapCreated grid = new Return_GetUOScrapCreated();

                    grid.Sr = NewSr;
                    grid.Item_Name = model.UOScrapItemName;
                    grid.Qty = Convert.ToDecimal(model.UOScrap_Qty);
                    grid.Unit = model.UOScrap_Unit;
                    grid.UnitID = model.UOScrapUnitID;
                    grid.Rate = Convert.ToDecimal(model.UOScrap_Rate);
                    grid.Amount = Convert.ToDecimal(model.UOScrap_Amount);
                    grid.Remarks = model.UOScrap_Remarks;
                    grid.ItemID = Convert.ToInt32(model.UOScrap_ItemId);
                    grid.LocationID = model.UOScrap_LocationID;
                    grid.ID = 0;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);
                }
                else if (model.UOScrapActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.UOScrap_Sr);
                    dataToEdit.Item_Name = model.UOScrapItemName;
                    dataToEdit.Qty = Convert.ToDecimal(model.UOScrap_Qty);
                    dataToEdit.Unit = model.UOScrap_Unit;
                    dataToEdit.UnitID = model.UOScrapUnitID;
                    dataToEdit.Rate = Convert.ToDecimal(model.UOScrap_Rate);
                    dataToEdit.Amount = Convert.ToDecimal(model.UOScrap_Amount);
                    dataToEdit.Remarks = model.UOScrap_Remarks;
                    dataToEdit.ItemID = Convert.ToInt32(model.UOScrap_ItemId);
                    dataToEdit.LocationID = model.UOScrap_LocationID;

                    if (model.UOScrapID != 0)
                    {
                        var editUOScrapID = new ArrayList();
                        var editScrap = UOEdit_Scrap_ID as ArrayList;
                        if (editScrap != null)
                        {
                            editScrap.Add(model.UOScrapID);
                            UOEdit_Scrap_ID = editScrap;
                        }
                        else
                        {
                            editUOScrapID.Add(model.UOScrapID);
                            UOEdit_Scrap_ID = editUOScrapID;
                        }
                    }

                }
                UO_ScrapData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Scrap_Window_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Scrap_ID = 0)
        {


            var allSrcDatascrap = (List<Return_GetUOScrapCreated>)UO_ScrapData;
            var dataToDelete = allSrcDatascrap != null ? allSrcDatascrap.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUOScrapCreated();

            if (dataToDelete.ID != 0)
            {
                var itemcheck = BASE._Stock_Profile_DBOps.GetStockUsage(dataToDelete.ID, ClientScreen.Stock_UO);
                if (itemcheck.Count > 0)
                {
                    var inusescreen = itemcheck[0].Screen;
                    return Json(new
                    {
                        result = false,
                        message = "Scrap Cannot Be Deleted Because It has Been Used In" + inusescreen + "...!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (allSrcDatascrap != null)
            {
                allSrcDatascrap.Remove(dataToDelete);
            }
            UO_ScrapData = allSrcDatascrap;

            if (Scrap_ID != 0)
            {
                var deleteScrapID = new ArrayList();
                var deleteScrap = Delete_User_Scrap_ID as ArrayList;
                if (deleteScrap != null)
                {
                    deleteScrap.Add(Scrap_ID);
                    Delete_User_Scrap_ID = deleteScrap;
                }
                else
                {
                    deleteScrapID.Add(Scrap_ID);
                    Delete_User_Scrap_ID = deleteScrapID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Order Item
        public ActionResult Store_Items_List(bool? IsVisible, DataSourceLoadOptions loadOptions, int? ReqStoreID = null)
        {
            Param_GetStoreItems inparam = new Param_GetStoreItems();

            inparam.StoreID = ReqStoreID;
            var Itemlist = BASE._Sub_Item_DBOps.GetStoreItems(inparam, ClientScreen.Stock_UO);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Itemlist, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Units_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Unitlist = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Unitlist, loadOptions)), "application/json");
        }

        public ActionResult LookUp_OT_GetLocations(bool? IsVisible, DataSourceLoadOptions loadOptions, int UOReqMainDeptID = 0, int? UOReqSubDeptID = null)
        {
            List<Return_GetLocations> AllLocations = new List<Return_GetLocations>();

            List<Return_GetLocations> Loclist = BASE._Stock_Profile_DBOps.GetLocations(UOReqMainDeptID,false);

            AllLocations = Loclist;
          
            if (UOReqSubDeptID != null)
            {
                List<Return_GetLocations> Loclistsubdept = BASE._Stock_Profile_DBOps.GetLocations(Convert.ToInt32(UOReqSubDeptID),false);             
                List<Return_GetLocations> AllLocationsmainsub = Loclist.Union(Loclistsubdept).ToList();
                AllLocations = AllLocationsmainsub.DistinctBy(x => x.Loc_Id).ToList();
            }


            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AllLocations, loadOptions)), "application/json");


        }
        public ActionResult UO_ItemGridData(string ActionMethodName, int UOID = 0)
        {
            var UOItemList = new List<Return_GetUO_Items>();
            if (ActionMethodName == "New")
            {
                return PartialView(UOItemList);
            }
            if (UO_ItemOrderedData == null)
            {
                var UOItemOrdData = BASE._user_order_DBOps.GetUO_Items(UOID);
                if (UOItemOrdData != null)
                {
                    UOItemList = UOItemOrdData;
                }
                UO_ItemOrderedData = UOItemList;
            }
            return PartialView(UO_ItemOrderedData);
        }

        public ActionResult Check_Approved_Item_Status(int UOID = 0, string[] UO_ItemID = null)
        {

        
            var UOItemList = new List<Return_GetUO_Items>();
            UOOrderItemDetails model = new UOOrderItemDetails();
            if (UO_ItemOrderedData == null)
            {

                var UOItemOrdData = BASE._user_order_DBOps.GetUO_Items(UOID);

                if (UOItemOrdData != null)
                {
                    UOItemList = UOItemOrdData;
                }
                UO_ItemOrderedData = UOItemList;
            }
                var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;

            if (all_data_Of_OrderItem_Grid != null)
            {

                if (UO_ItemID != null)
                {
                    for (int i = 0; i < UO_ItemID.Length; i++)
                    {
                        Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_ItemID[i].ToString());

                        if (selectedrow.Approved_Qty == 0)
                        {
                            return Json(new
                            {
                                message = "Selected items not contain approved quantity, please enter Approved Quantity to create RR!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }

                else
                {
                    for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                    {

                        if (all_data_Of_OrderItem_Grid[I].Approved_Qty == 0)
                        {
                            return Json(new
                            {
                                message = "UO Items are not approved, please enter Approved Quantity for all Items to create RR",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                }


            }
            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApproveAllItems()
        {
            var flag = 0;
            UOOrderItemDetails model = new UOOrderItemDetails();

            var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
            if (all_data_Of_OrderItem_Grid != null)
            {
                for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                {

                    if (all_data_Of_OrderItem_Grid[I].Approved_Qty == 0)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    return Json(new
                    {
                        message = "All Items are already Approved.",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }


                for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                {

                    if (all_data_Of_OrderItem_Grid[I].Approved_Qty == 0)
                    {

                        all_data_Of_OrderItem_Grid[I].Approved_Qty = all_data_Of_OrderItem_Grid[I].Requested_Qty;

                        if (all_data_Of_OrderItem_Grid[I].ID != 0)
                        {
                            var editItemOrdID = new ArrayList();
                            var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                            if (editItemOrd != null)
                            {
                                editItemOrd.Add(all_data_Of_OrderItem_Grid[I].ID);
                                UOEdit_ItemOrd_ID = editItemOrd;
                            }
                            else
                            {
                                editItemOrdID.Add(all_data_Of_OrderItem_Grid[I].ID);
                                UOEdit_ItemOrd_ID = editItemOrdID;
                            }
                        }
                    }
                }


            }
            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApproveItems(string[] uoselectedrowarr)
        {

            UOOrderItemDetails model = new UOOrderItemDetails();

            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

            if (uoselectedrowarr != null)
            {
                for (int i = 0; i < uoselectedrowarr.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == uoselectedrowarr[i].ToString());
                    if (selectedrow.Approved_Qty != 0)
                    {
                        return Json(new
                        {
                            message = "Selected Item is already Approved.",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                        selectedrow.Approved_Qty = selectedrow.Requested_Qty;

                    if (selectedrow.ID != 0)
                    {
                        var editItemOrdID = new ArrayList();
                        var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                        if (editItemOrd != null)
                        {
                            editItemOrd.Add(selectedrow.ID);
                            UOEdit_ItemOrd_ID = editItemOrd;
                        }
                        else
                        {
                            editItemOrdID.Add(selectedrow.ID);
                            UOEdit_ItemOrd_ID = editItemOrdID;
                        }
                    }

                }
            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Approve ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true, message = "Selected Item(s) Approved Successfully" }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ApproveItemsPartially(string[] uoselectedrowarr)
        {
            UOOrderItemDetails model = new UOOrderItemDetails();
            String uoitemid = null;
            if (uoselectedrowarr != null)
            {
                for (int i = 0; i < uoselectedrowarr.Length; i++)
                {
                    if (uoitemid == null)
                    {
                        uoitemid = uoselectedrowarr[i].ToString();
                    }
                    else
                    {
                        uoitemid = uoitemid + ',' + uoselectedrowarr[i].ToString();
                    }

                }
            }

            model.UO_Schedule_Item_ID = uoitemid;
            return View(model);

        }
        public ActionResult ApproveItemsPartially_Save(Decimal ApprovedValue, string[] UO_Item_ID = null)
        {
            UOOrderItemDetails model = new UOOrderItemDetails();

            var Item_ID = UO_Item_ID;


            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;
            if (ApprovedValue <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Please Enter Approved Quantity. "
                }, JsonRequestBehavior.AllowGet);
            }


            Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == Item_ID[0].ToString());
            if (selectedrow.Approved_Qty != 0)
            {
                return Json(new
                {
                    message = "Selected Item is already Approved.",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }

            if (ApprovedValue > selectedrow.Requested_Qty)
            {
                return Json(new
                {
                    message = "Approved Quantity cannot be greater than requested Quantity.",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }

            selectedrow.Approved_Qty = ApprovedValue;
            if (selectedrow.ID != 0)
            {
                var editItemOrdID = new ArrayList();
                var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                if (editItemOrd != null)
                {
                    editItemOrd.Add(selectedrow.ID);
                    UOEdit_ItemOrd_ID = editItemOrd;
                }
                else
                {
                    editItemOrdID.Add(selectedrow.ID);
                    UOEdit_ItemOrd_ID = editItemOrdID;
                }
            }



            return Json(new { result = true, message = "Approved Quantity Added in Selected Item" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RejectAllItems()
        {
            var flag = 0;
            UOOrderItemDetails model = new UOOrderItemDetails();

            var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
            if (all_data_Of_OrderItem_Grid != null)
            {
                for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                {

                    if (all_data_Of_OrderItem_Grid[I].Approved_Qty != 0)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    return Json(new
                    {
                        message = "All Items are already Rejected.",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }

                for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                {


                    all_data_Of_OrderItem_Grid[I].Approved_Qty = 0;

                    if (all_data_Of_OrderItem_Grid[I].ID != 0)
                    {
                        var editItemOrdID = new ArrayList();
                        var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                        if (editItemOrd != null)
                        {
                            editItemOrd.Add(all_data_Of_OrderItem_Grid[I].ID);
                            UOEdit_ItemOrd_ID = editItemOrd;
                        }
                        else
                        {
                            editItemOrdID.Add(all_data_Of_OrderItem_Grid[I].ID);
                            UOEdit_ItemOrd_ID = editItemOrdID;
                        }
                    }
                }
            }



            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RejectItems(string[] uoselectedrowarr)
        {

            UOOrderItemDetails model = new UOOrderItemDetails();

            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

            if (uoselectedrowarr != null)
            {
                for (int i = 0; i < uoselectedrowarr.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == uoselectedrowarr[i].ToString());
                    if (selectedrow.Approved_Qty == 0)
                    {
                        return Json(new
                        {
                            message = "Selected Item is already Rejected.",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                        selectedrow.Approved_Qty = 0;

                    if (selectedrow.ID != 0)
                    {
                        var editItemOrdID = new ArrayList();
                        var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                        if (editItemOrd != null)
                        {
                            editItemOrd.Add(selectedrow.ID);
                            UOEdit_ItemOrd_ID = editItemOrd;
                        }
                        else
                        {
                            editItemOrdID.Add(selectedrow.ID);
                            UOEdit_ItemOrd_ID = editItemOrdID;
                        }
                    }

                }
            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Reject ...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true, message = "Selected Item(s) Rejected Successfully" }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult UO_Scheduledelivery(int UO_ID = 0, string[] uoselectedrowarr = null)
        {
            UOOrderItemDetails model = new UOOrderItemDetails();
            if (UO_ID != 0) {
                model.UOid = UO_ID;
            }
            String uoitemid = null;
            if (uoselectedrowarr != null)
            {
                for (int i = 0; i < uoselectedrowarr.Length; i++)
                {
                    if (uoitemid == null)
                    {
                        uoitemid = uoselectedrowarr[i].ToString();
                    }
                    else
                    {
                        uoitemid = uoitemid + ',' + uoselectedrowarr[i].ToString();
                    }

                }
            }

            model.UO_Schedule_Item_ID = uoitemid;
            return View(model);
        }

        public ActionResult UO_Scheduledelivery_Save(string schedule_Date, string[] UO_Item_ID = null, int UO_ID = 0)
        {
            UOOrderItemDetails model = new UOOrderItemDetails();

            if (UO_ID != 0)
            {
                var id = UO_ID;
                var UOItemList = new List<Return_GetUO_Items>();
                var UOItemOrdData = BASE._user_order_DBOps.GetUO_Items(id);
                if (UOItemOrdData != null)
                {
                    UOItemList = UOItemOrdData;
                }
                Param_Update_UO_Scheduled_Delivery inparam = new Param_Update_UO_Scheduled_Delivery();
                var all_data = (List<Return_GetUO_Items>)UOItemList;
                if (all_data != null)
                {

                    inparam.UO_ID = UO_ID;
                    inparam.Scheduled_Delivery_Date = Convert.ToDateTime(schedule_Date);

                }

                if (BASE._user_order_DBOps.Update_Scheduled_Delivery(inparam))
                {

                    return Json(new { result = "RightClickTrue", message = "Schedule Delivery Date Added in All Items." }, JsonRequestBehavior.AllowGet);
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
                var schedule_Item_ID = UO_Item_ID;


                var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;
                if (schedule_Date == null)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Please Enter Schedule Delivery Date"
                    }, JsonRequestBehavior.AllowGet);
                }

                else if (schedule_Date != null)
                {
                    for (int i = 0; i < schedule_Item_ID.Length; i++)
                    {
                        Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == schedule_Item_ID[i].ToString());
                        selectedrow.Scheduled_Delivery_Date = Convert.ToDateTime(schedule_Date);
                        if (selectedrow.ID != 0)
                        {
                            var editItemOrdID = new ArrayList();
                            var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                            if (editItemOrd != null)
                            {
                                editItemOrd.Add(selectedrow.ID);
                                UOEdit_ItemOrd_ID = editItemOrd;
                            }
                            else
                            {
                                editItemOrdID.Add(selectedrow.ID);
                                UOEdit_ItemOrd_ID = editItemOrdID;
                            }
                        }
                    }
                }
            }
            return Json(new { result = true, message = "Schedule Delivery Date Added in selected Items." }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult UOScheduleDeliveryItemGrid(UOOrderItemDetails model, string[] uoselectedrowarr)
        //{

        //    var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;


        //    if (uoselectedrowarr != null)
        //    {
        //        for (int i = 0; i < uoselectedrowarr.Length; i++)
        //        {
        //            Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == uoselectedrowarr[i].ToString());

        //            selectedrow.Scheduled_Delivery_Date = model.OT_Sch_Deliver_Date;

        //            if (selectedrow.ID != 0)
        //            {
        //                var editItemOrdID = new ArrayList();
        //                var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
        //                if (editItemOrd != null)
        //                {
        //                    editItemOrd.Add(selectedrow.ID);
        //                    UOEdit_ItemOrd_ID = editItemOrd;
        //                }
        //                else
        //                {
        //                    editItemOrdID.Add(selectedrow.ID);
        //                    UOEdit_ItemOrd_ID = editItemOrdID;
        //                }
        //            }


        //        }
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            message = "Please select atleast one Item to Schedule Delivery...!",
        //            result = false,
        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //    return Json(new { result = true, message = "Schedule Delivery Date for Selected Item Added Successfully" }, JsonRequestBehavior.AllowGet);
        //    //foreach (var item in selectedKeys.Split('|'))
        //    //{
        //    //    int key;
        //    //    if (int.TryParse(item, out key))
        //    //    {
        //    //        if (model.OT_Approved_Qty <= 0)
        //    //        {
        //    //            all_data_Of_OrderItem_Grid[key].Approved_Qty = all_data_Of_OrderItem_Grid[key].Requested_Qty;
        //    //        }
        //    //    }
        //    //}
        //    //return Json(new
        //    //{


        //    //}, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult FindItemGridKeyValue()
        {
            var griddata = UO_ItemOrderedData as List<Return_GetUO_Items>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = Convert.ToString(griddata[i].ID);
                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_User_Order_Item(int SR = 0, string ActionMethod = null, int UOid = 0, int itemid = 0, string UOItemPopupName = null, string PostSuccessFunction = "", int? subdeptid = null)
        {
            ViewData["UO_AddStockItemRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");
            ViewData["UO_AddHelpRequestRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Add");
            UOOrderItemDetails model = new UOOrderItemDetails();
            if (UOItemPopupName != null)
            {
                model.UOItemPopupName = UOItemPopupName;
            }
            else
                model.UOItemPopupName = "UserOrderinnergrid";
            model.UOItemPostSuccessFunction = PostSuccessFunction.Length > 0 ? PostSuccessFunction : "Frm_User_Order_Item_Popup_OnSuccess";

            model.UO_OT_ActionMethod = ActionMethod;
            if (ActionMethod == "New")
            {
                model.OT_Delivery_Allowed = true;
                model.OT_Priority = "Normal";
                model.OT_Req_Deliver_Date = DateTime.Now;
                if (itemid != 0)
                {
                    model.OT_Item_ID = itemid;
                }
            }
            if (UOid != 0)
            {

                model.UOid = UOid;
                model.UOItemSubDept = subdeptid;
            }


            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr = Convert.ToInt16(SR);
                var all_data = (List<Return_GetUO_Items>)UO_ItemOrderedData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr == Sr).FirstOrDefault() : new Return_GetUO_Items();
                model.Sr = Sr;
                model.OT_Item_ID = dataToEdit.SubItemID;
                model.OT_Item_Name = dataToEdit.Item_Name;
                model.OT_Head = dataToEdit.Head;
                model.OT_Item_Type = dataToEdit.Item_Type;
                model.OT_Item_Code = dataToEdit.Item_Code;
                model.OT_Make = dataToEdit.Make;
                model.OT_Model = dataToEdit.Model;
                model.OT_DeliveryLocID = dataToEdit.Delivery_Location_ID;
                model.OT_Delivery_Location = dataToEdit.Delivery_Location;
                model.OT_UnitID = dataToEdit.UnitID;
                model.OT_Requested_Qty = Convert.ToDouble(dataToEdit.Requested_Qty);
                model.OT_Approved_Qty = Convert.ToDouble(dataToEdit.Approved_Qty);
                model.OT_Req_Deliver_Date = dataToEdit.Requested_Delivery_Date;
                model.OT_Sch_Deliver_Date = dataToEdit.Scheduled_Delivery_Date;
                model.OT_Priority = dataToEdit.UOI_Priority;
                model.OT_Delivery_Allowed = dataToEdit.Partial_Delivery_Allowed;
                model.OT_Remarks = dataToEdit.Remarks != null ? dataToEdit.Remarks.ToString() : "";
                model.UO_OT_ID = dataToEdit.ID;


            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_User_Order_Item_Popup(UOOrderItemDetails model)
        {
            if (model.UO_OT_ActionMethod == "New" || model.UO_OT_ActionMethod == "Edit")
            {
                if (string.IsNullOrEmpty(model.OT_Item_Name) || string.IsNullOrEmpty(model.OT_Item_ID.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Item Name Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.OT_DeliveryLocID))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Delivery Location Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.OT_Requested_Qty <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Requested Quantity should be greater than zero...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.OT_Approved_Qty > model.OT_Requested_Qty)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Approved Qty can not be greater than Requested Qty ...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.OT_UnitID))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Unit Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.OT_Req_Deliver_Date.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Required Delivery Date Can Not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                var UOItemList = new List<Return_GetUO_Items>();

                if (UO_ItemOrderedData == null)
                {
                    if (model.UOid != 0)
                    {
                        var UOItemOrdData = BASE._user_order_DBOps.GetUO_Items(model.UOid);
                        if (UOItemOrdData != null)
                        {
                            UOItemList = UOItemOrdData;
                        }
                        UO_ItemOrderedData = UOItemList;
                    }
                }

                var all_data_Of_OrderItem_Grid = (List<Return_GetUO_Items>)UO_ItemOrderedData;
                if (all_data_Of_OrderItem_Grid != null)
                {
                    for (int I = 0; I <= all_data_Of_OrderItem_Grid.Count() - 1; I++)
                    {

                        if (all_data_Of_OrderItem_Grid[I].SubItemID == model.OT_Item_ID)
                        {
                            if (all_data_Of_OrderItem_Grid[I].Sr != model.Sr) //mantis issue 1297 resolved
                            {
                                return Json(new
                                {
                                    message = "This Item already exists.Please Edit Existing Item",
                                    result = false,
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            if (model.UOid != 0)
            {
                try
                {
                    Param_Insert_UO_Item inparam = new Param_Insert_UO_Item();
                    inparam.UO_ID = model.UOid;
                    inparam.Sub_Item_ID = Convert.ToInt32(model.OT_Item_ID);
                    inparam.Make = model.OT_Make;
                    inparam.Model = model.OT_Model;
                    inparam.Unit_ID = model.OT_UnitID;
                    inparam.Requested_Qty = Convert.ToDecimal(model.OT_Requested_Qty);
                    inparam.Required_Date = model.OT_Req_Deliver_Date;
                    inparam.Scheduled_Delivery_Date = model.OT_Sch_Deliver_Date;
                    inparam.Priority = model.OT_Priority;
                    inparam.Part_Delivery_Allowed = model.OT_Delivery_Allowed;
                    inparam.Remarks = model.OT_Remarks;
                    inparam.Delivery_Location_Id = model.OT_DeliveryLocID;
                    inparam.Approved_Qty = Convert.ToDecimal(model.OT_Approved_Qty);
                    if (BASE._user_order_DBOps.Insert_UO_Item(inparam))
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
                List<Return_GetUO_Items> gridRows = new List<Return_GetUO_Items>();
                var gridRowsCount = 0;
                var LastRowSr = 0;
                var NewSr = LastRowSr + 1;
                if (UO_ItemOrderedData != null)
                {
                    gridRows = (List<Return_GetUO_Items>)UO_ItemOrderedData;
                    gridRowsCount = gridRows.Count;
                    LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr) : 0;
                    NewSr = LastRowSr + 1;
                }
                if (model.UO_OT_ActionMethod == "New")
                {
                    Return_GetUO_Items grid = new Return_GetUO_Items();
                    grid.Sr = NewSr;
                    grid.Item_Name = model.OT_Item_Name;
                    grid.SubItemID = Convert.ToInt32(model.OT_Item_ID);
                    grid.Head = model.OT_Head;
                    grid.Item_Type = model.OT_Item_Type;
                    grid.Item_Code = model.OT_Item_Code;
                    grid.Make = model.OT_Make;
                    grid.Model = model.OT_Model;
                    grid.Delivery_Location = model.OT_Delivery_Location;
                    grid.Delivery_Location_ID = model.OT_DeliveryLocID;
                    grid.Unit = model.OT_UnitName;
                    grid.UnitID = model.OT_UnitID;
                    grid.Requested_Qty = Convert.ToDecimal(model.OT_Requested_Qty);
                    grid.Approved_Qty = Convert.ToDecimal(model.OT_Approved_Qty);
                    grid.Requested_Delivery_Date = model.OT_Req_Deliver_Date;
                    grid.Scheduled_Delivery_Date = model.OT_Sch_Deliver_Date;
                    grid.UOI_Priority = model.OT_Priority;
                    grid.Partial_Delivery_Allowed = model.OT_Delivery_Allowed;
                    grid.ID = 0;
                    grid.Remarks = model.OT_Remarks;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);
                }
                else if (model.UO_OT_ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                    if (dataToEdit.ID != 0)
                    {
                        var delcount = BASE._user_order_DBOps.GetUOReqItem_Delivered_EntryCount(dataToEdit.ID);
                        if (delcount > 0)
                        {

                            return Json(new
                            {
                                result = false,
                                message = "Requested Item against which delivery has been posted can not be edited"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.UO_OT_ID != 0)
                    {
                        var editItemOrdID = new ArrayList();
                        var editItemOrd = UOEdit_ItemOrd_ID as ArrayList;
                        if (editItemOrd != null)
                        {
                            editItemOrd.Add(model.UO_OT_ID);
                            UOEdit_ItemOrd_ID = editItemOrd;
                        }
                        else
                        {
                            editItemOrdID.Add(model.UO_OT_ID);
                            UOEdit_ItemOrd_ID = editItemOrdID;
                        }
                    }
                    dataToEdit.Item_Name = model.OT_Item_Name;
                    dataToEdit.SubItemID = Convert.ToInt32(model.OT_Item_ID);
                    dataToEdit.Head = model.OT_Head;
                    dataToEdit.Item_Type = model.OT_Item_Type;
                    dataToEdit.Item_Code = model.OT_Item_Code;
                    dataToEdit.Make = model.OT_Make;
                    dataToEdit.Model = model.OT_Model;
                    dataToEdit.Delivery_Location = model.OT_Delivery_Location;
                    dataToEdit.Delivery_Location_ID = model.OT_DeliveryLocID;
                    dataToEdit.Unit = model.OT_UnitName;
                    dataToEdit.UnitID = model.OT_UnitID;
                    dataToEdit.Requested_Qty = Convert.ToDecimal(model.OT_Requested_Qty);
                    dataToEdit.Approved_Qty = Convert.ToDecimal(model.OT_Approved_Qty);
                    dataToEdit.Requested_Delivery_Date = model.OT_Req_Deliver_Date;
                    dataToEdit.Scheduled_Delivery_Date = model.OT_Sch_Deliver_Date;
                    dataToEdit.UOI_Priority = model.OT_Priority;
                    dataToEdit.Partial_Delivery_Allowed = model.OT_Delivery_Allowed;
                    dataToEdit.Remarks = model.OT_Remarks;

                }
                UO_ItemOrderedData = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Item_DetailWin_Delete_Grid_Record(string ActionMethod, int SrID = 0, int Item_Rec_Id = 0)
        {

            var allSrcData = (List<Return_GetUO_Items>)UO_ItemOrderedData;
            var dataToDelete = allSrcData != null ? allSrcData.Where(x => x.Sr == SrID).FirstOrDefault() : new Return_GetUO_Items();
            if (dataToDelete.ID != 0)
            {
                var delcount = BASE._user_order_DBOps.GetUOReqItem_Delivered_EntryCount(dataToDelete.ID);
                if (delcount > 0)
                {

                    return Json(new
                    {
                        result = false,
                        message = "Requested Item against which delivery has been posted can not be deleted"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (allSrcData != null)
            {
                allSrcData.Remove(dataToDelete);
            }
            UO_ItemOrderedData = allSrcData;
            var deleteItemOrdID = new ArrayList();
            if (Item_Rec_Id != 0)
            {
                var deleteItemOrd = Delete_User_OrderItem_ID as ArrayList;
                if (deleteItemOrd != null)
                {
                    deleteItemOrd.Add(Item_Rec_Id);
                    Delete_User_OrderItem_ID = deleteItemOrd;
                }
                else
                {
                    deleteItemOrdID.Add(Item_Rec_Id);
                    Delete_User_OrderItem_ID = deleteItemOrdID;
                }

            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CurrentUserRoleGrids(int ID)
        {
            var Req = false;
            var ReqI = false;
            var Sk = false;
            var SkI = false;

            if (ID != 0)
            {
                var _CurrUserRole = BASE._user_order_DBOps.GetRegister(Convert.ToDateTime(UOFromDate), Convert.ToDateTime(UOToDate)).main_Register.FindAll(x => x.ID == ID).FirstOrDefault().CurrUserRole;
                string[] CurrUserRolea = new string[] { "" };
                if (_CurrUserRole != null) { CurrUserRolea = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
                if (CurrUserRolea != null)
                {
                    if (CurrUserRolea.Contains("Requestor"))
                    {
                        Req = true;
                    }
                    if (CurrUserRolea.Contains("Requestor Incharge"))
                    {
                        ReqI = true;
                    }
                    if (CurrUserRolea.Contains("Store Keeper"))
                    {
                        Sk = true;
                    }
                    if (CurrUserRolea.Contains("Store Keeper in-charge"))
                    {
                        SkI = true;
                    }
                }
            }

            return Json(new
            {
                Req = Req,
                ReqI = ReqI,
                Sk = Sk,
                SkI = SkI


            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_Unsaved_Item_Entry_in_Delivery_Grid_Item_Grid(int ID_Item = 0) //mantis bug #929 
        {


            var all_data_Of_Delivery_Grid = (List<Return_GetUOGoodsDelivered_MainGrid>)UO_GoodsDeliveredMainData;
            if (all_data_Of_Delivery_Grid != null)
            {

                if (all_data_Of_Delivery_Grid.Count != 0)
                {
                    List<Return_GetUOGoodsDelivered_MainGrid> datatocheckdelivery = all_data_Of_Delivery_Grid.FindAll(x => x.ItemRequestedID == ID_Item && x.ID == 0);


                    if (datatocheckdelivery.Count != 0)
                    {

                        return Json(new
                        {
                            result = false,
                            message = "New unsaved Row for same item is already in  Delivery Grid, please save it to Edit/Delete this row."
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



        public ActionResult CheckForsameRRIDS(string[] UO_ItemID)
        {
            string RRID = null;
            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;


            if (UO_ItemID != null)
            {

                for (int i = 0; i < UO_ItemID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_ItemID[i].ToString());

                    if (selectedrow.Mapped_RRID != null)
                    {
                        if (RRID == null)
                        {
                            RRID = selectedrow.Mapped_RRID + ",";
                        }
                        else
                        {
                            RRID = RRID + selectedrow.Mapped_RRID + ",";
                        }
                    }

                }
                if (RRID != null)
                {


                    var RRIDsplited = RRID.Split(',');

                    RRIDsplited = RRIDsplited.Where(color => !string.IsNullOrEmpty(color)).ToArray();
                    for (int i = 0; i < RRIDsplited.Length - 1; i++)
                    {
                        if (RRIDsplited[i] != RRIDsplited[i + 1])
                        {

                            return Json(new
                            {
                                message = "Items selected are mapped to different RR's.Please select Unmapped Items for mapping or Items mapped with same RR for Changing the Mapping already done..!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Merge UO in RR...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true, message = "All RRID's are same or null." }, JsonRequestBehavior.AllowGet);
            //foreach (var item in selectedKeys.Split('|'))
            //{
            //    int key;
            //    if (int.TryParse(item, out key))
            //    {
            //        if (model.OT_Approved_Qty <= 0)
            //        {
            //            all_data_Of_OrderItem_Grid[key].Approved_Qty = all_data_Of_OrderItem_Grid[key].Requested_Qty;
            //        }
            //    }
            //}
            //return Json(new
            //{


            //}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Findsubitemidforitem(string[] UO_ItemID)
        {
            string Subitemid = null;
            string ItemRecid = null;
            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;


            if (UO_ItemID != null)
            {
                for (int i = 0; i < UO_ItemID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_ItemID[i].ToString());

                    if (selectedrow.Mapped_RRID != null)
                    {
                        return Json(new
                        {
                            message = "Selected items contain already mapped items.Please select Unmapped Items only for creating RR!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                  
                }

                for (int i = 0; i < UO_ItemID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_ItemID[i].ToString());

                    if (Subitemid == null)
                    {
                        Subitemid = selectedrow.SubItemID + ",";
                    }
                    else
                    {
                        Subitemid = Subitemid + selectedrow.SubItemID + ",";
                    }
                }
                for (int i = 0; i < UO_ItemID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_ItemID[i].ToString());

                    if (ItemRecid == null)
                    {
                        ItemRecid = selectedrow.ID + ",";
                    }
                    else
                    {
                        ItemRecid = ItemRecid + selectedrow.ID + ",";
                    }
                }


            }
            return Json(new { result = true, message = "", data = Subitemid , itemid = ItemRecid }, JsonRequestBehavior.AllowGet);

        }
    
       
           
          
          
        
        #endregion

        #region Map UO to RR

        public ActionResult UO_MapExistingRRGridData(string command, int UOID = 0, string[] UO_Item_ID = null)
        {
            String uoitemid = null;

            var MapUserOrderList = new List<Return_Get_RR_Details_ForUOmapping>();
            if (UO_MapUserOrderData == null || command == "REFRESH")
            {
                UOOrderItemDetails model = new UOOrderItemDetails();

                var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

                if (UO_Item_ID != null)
                {
                    for (int i = 0; i < UO_Item_ID.Length; i++)
                    {
                        if (uoitemid == null)
                        {
                            uoitemid = UO_Item_ID[i].ToString() ;
                        }
                        else
                        {
                            uoitemid = uoitemid + ',' + UO_Item_ID[i].ToString();
                        }

                    }
                }

                Param_Get_RR_Details_ForUOmapping inparam = new Param_Get_RR_Details_ForUOmapping();
                inparam.UO_ID = UOID;
                if (uoitemid != null)
                {
                    inparam.UO_Item_ID = uoitemid;
                }
                List<Return_Get_RR_Details_ForUOmapping> MapUserOrderData = BASE._user_order_DBOps.Get_RR_Details_ForUOmapping(inparam);

                if (MapUserOrderData != null)
                {
                    MapUserOrderList = MapUserOrderData;
                }
                UO_MapUserOrderData = MapUserOrderList;

            }
            return PartialView(UO_MapUserOrderData);

        }

        //public ActionResult UO_MapExistingRRGridData(string command, int UOID = 0, string[] UO_Item_ID = null)
        //{
        //    var MapUserOrderList = new List<Return_Get_RR_Details_ForUOmapping>();
        //    if (UO_MapUserOrderData == null || command == "REFRESH")
        //    {
        //        Param_Get_RR_Details_ForUOmapping inparam = new Param_Get_RR_Details_ForUOmapping();
        //        inparam.UO_ID = UOID;
        //        var MapUserOrderData = BASE._user_order_DBOps.Get_RR_Details_ForUOmapping(inparam);

        //        if (MapUserOrderData != null)
        //        {
        //            MapUserOrderList = MapUserOrderData;
        //        }
        //        UO_MapUserOrderData = MapUserOrderList;

        //    }
        //    return PartialView(UO_MapUserOrderData);

        //}
        //public ActionResult UO_MapExistingRRGridDataItemGrid(string command, int UOID = 0, string[] UO_Item_ID = null)
        //{
        //    String uoitemid = null;

        //    var MapUserOrderList = new List<Return_Get_RR_Details_ForUOmapping>();
        //    if (UO_MapUserOrderData == null || command == "REFRESH")
        //    {
        //        UOOrderItemDetails model = new UOOrderItemDetails();

        //        var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

        //        if (UO_Item_ID != null)
        //        {
        //            for (int i = 0; i < UO_Item_ID.Length; i++)
        //            {

        //                uoitemid = UO_Item_ID[i].ToString() + ',';

        //            }
        //        }
            
        //        Param_Get_RR_Details_ForUOmapping inparam = new Param_Get_RR_Details_ForUOmapping();
        //        inparam.UO_ID = UOID;
        //        inparam.UO_Item_ID = uoitemid;
        //        var MapUserOrderData = BASE._user_order_DBOps.Get_RR_Details_ForUOmapping(inparam);

        //        if (MapUserOrderData != null)
        //        {
        //            MapUserOrderList = MapUserOrderData;
        //        }
        //        UO_MapUserOrderData = MapUserOrderList;

        //    }
        //    return PartialView(UO_MapUserOrderData);

        //}
        public JsonResult CheckPreviousMapping()
        {
            var RRID = 0;

            var all_Mapped_Grid_data = (List<Return_Get_RR_Details_ForUOmapping>)UO_MapUserOrderData;
            if (all_Mapped_Grid_data != null)
            {

                for (int I = 0; I <= all_Mapped_Grid_data.Count() - 1; I++)
                {
                    if (all_Mapped_Grid_data[I].UOIDs_Mapped == true)

                    {
                        RRID = all_Mapped_Grid_data[I].RR_ID;

                    }
                }
            }
            return Json(new
            {
                result = true,
                Message = "",
                value = RRID
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult FindItemGridKeyValueUORR()
        {
            var griddata = UO_MapUserOrderData as List<Return_Get_RR_Details_ForUOmapping>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = Convert.ToString(griddata[i].RR_ID);
                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UO_MapExistingRR_Register(int UO_ID = 0)
        {
            UORRMap model = new UORRMap();
            model.UO_ID = UO_ID;
          

            return PartialView(model);
        }

        public ActionResult UO_MapExistingRR(string[] UO_Items_ID, int UO_ID = 0 )
        {
            UORRMap model = new UORRMap();
            model.UO_ID = UO_ID;
            String uoitemid = null;
            if (UO_Items_ID != null)
            {
                for (int i = 0; i < UO_Items_ID.Length; i++)
                {
                    if (uoitemid == null)
                    {
                        uoitemid = UO_Items_ID[i].ToString();
                    }
                    else
                    {
                        uoitemid = uoitemid + ',' + UO_Items_ID[i].ToString();
                    }

                }
            }

            model.UO_Map_Item_ID = uoitemid;

            return PartialView(model);
        }



        public ActionResult UO_MapExistingRRSaveRegister(int UO_ID, int RR_ID)
        {

            try
            {
                var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

                Param_UO_RR_Unmapping unparam = new Param_UO_RR_Unmapping();
                unparam.UO_ID = UO_ID;


                if (!BASE._user_order_DBOps.UORRUnmapping(unparam))
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }



                Param_UO_Insert_UO_RR_Mapping inparam = new Param_UO_Insert_UO_RR_Mapping();
                inparam.UO_ID = UO_ID;
                inparam.RR_ID = RR_ID;


                if (!BASE._user_order_DBOps.InsertUORRMapping(inparam))
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
            return Json(new { result = true, message = "Merging Done Sucessfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UO_MapExistingRRSave(int UO_ID, int RR_ID, string[] UO_Item_ID)
        {

            try
            {
                var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;
                for (int i = 0; i < UO_Item_ID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_Item_ID[i].ToString());

                    Param_UO_RR_Unmapping unparam = new Param_UO_RR_Unmapping();
                    unparam.UO_ID = UO_ID;
                    unparam.UO_Item_ID = selectedrow.ID;

                    if (!BASE._user_order_DBOps.UORRUnmapping(unparam))
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }


                for (int i = 0; i < UO_Item_ID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_Item_ID[i].ToString());

                    Param_UO_Insert_UO_RR_Mapping inparam = new Param_UO_Insert_UO_RR_Mapping();
                    inparam.UO_ID = UO_ID;
                    inparam.RR_ID = RR_ID;
                    inparam.UO_Item_ID = selectedrow.ID;

                    if (!BASE._user_order_DBOps.InsertUORRMapping(inparam))
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
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
            return Json(new { result = true, message = "Merging Done Sucessfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnmapUORR(int UO_ID, int RR_ID = 0)
        { 
            try
            {
        
                Param_UO_RR_Unmapping delparam = new Param_UO_RR_Unmapping();
                delparam.UO_ID = UO_ID;
                if(RR_ID != 0){
                    delparam.Mapped_RR_ID = RR_ID;
                  }
                if (BASE._user_order_DBOps.UORRUnmapping(delparam))                  
                {
                    return Json(new { result = true, message = "Unmapping Done Successfully" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult UnmapUORRItemGrid(string[] UO_Item_ID, int UO_ID)
        {
           
            var all_data_Of_OrderItem_Grid = UO_ItemOrderedData as List<Return_GetUO_Items>;

            if (UO_Item_ID != null)
            {
                for (int i = 0; i < UO_Item_ID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_Item_ID[i].ToString());
                    if (selectedrow.ID == 0)
                    {
                        return Json(new
                        {
                            message = "One of the Selected Item is not saved. Please save UO to save all the items.",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (selectedrow.Approved_Qty == 0)
                    {
                        return Json(new
                        {
                            message = "Selected Item is Not Approved.",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if(selectedrow.Mapped_RRID == null)
                    {
                        return Json(new
                        {
                            message = selectedrow.Item_Name + " is Already unmapped.",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                }

                for (int i = 0; i < UO_Item_ID.Length; i++)
                {
                    Return_GetUO_Items selectedrow = all_data_Of_OrderItem_Grid.Find(x => x.ID.ToString() == UO_Item_ID[i].ToString());

                    Param_UO_RR_Unmapping unparam = new Param_UO_RR_Unmapping();
                    unparam.UO_ID = UO_ID;
                    unparam.UO_Item_ID = selectedrow.ID;

                    if (!BASE._user_order_DBOps.UORRUnmapping(unparam))
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new
                {
                    message = "Please select atleast one Item to Unmap...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = true, message = "Unmapping of selected items done successfully" }, JsonRequestBehavior.AllowGet);


        }




        #endregion

        #region Check Availability

        
        public ActionResult CA_Item_Grid_Custom_Data(int key = 0)
        {
            var Data = Avl_Stock_Item_Grid_Data as List<DbOperations.StockUserOrder.Return_Get_Stock_Availability>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.StockID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Make + "," + it.ItemID;             }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        public ActionResult LookUp_CA_ItemList(bool? IsVisible, DataSourceLoadOptions loadOptions, int? StoreID, int? CenterID)
        {
            Param_GetStoreItems inparam = new Param_GetStoreItems();
            if (StoreID == null || StoreID == 0)
            {
                inparam.StoreID = null;
            }
            else
            {
                inparam.StoreID = StoreID;
            }
            if (CenterID == null)
            {
                inparam.CEN_ID = null;
            }
            else
            {
                inparam.CEN_ID = CenterID;
            }

          
            var itemdata = BASE._Sub_Item_DBOps.GetStoreItems(inparam, ClientScreen.Stock_UO);
         
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(itemdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_CA_ItemCategory(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var categorydata = BASE._Sub_Item_DBOps.GetMainCategoriesMaster();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(categorydata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_CA_Center(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var centerdata = BASE._user_order_DBOps.Get_CenterDetails_StockAvailability();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(centerdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_CA_Store(bool? IsVisible, DataSourceLoadOptions loadOptions, int? CenterID)
        {            
            Param_GetStoreList_StockAvailability inparam = new Param_GetStoreList_StockAvailability();
            if (CenterID == null)
            {
                inparam.CEN_ID = null;
            }
            else
            {
                inparam.CEN_ID = CenterID;
            }

            var storedata = BASE._user_order_DBOps.GetStoreList_StockAvailability(inparam);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(storedata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_CA_Location(bool? IsVisible, DataSourceLoadOptions loadOptions, int? CenterID, int? StoreID)
        {
            Param_GetStoreLocations_StockAvailability inparam = new Param_GetStoreLocations_StockAvailability();
            if (StoreID == null)
            {
                inparam.StoreID = null;
            }
            else
            {
                inparam.StoreID = StoreID;
            }
            if (CenterID == null)
            {
                inparam.CEN_ID = null;
            }
            else
            {
                inparam.CEN_ID = CenterID;
            }


            var Locationdata = BASE._user_order_DBOps.Get_Store_Locations_StockAvailability(inparam);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Locationdata, loadOptions)), "application/json");
        }
        public ActionResult Frm_User_Order_Check_Availability( int UOid = 0, int? Requestee_Store_ID = null, int? ItemID = null, string Screenname = null)
        {
            UOCheckAvail model = new UOCheckAvail();

            if (ItemID != null && ItemID != null)
            {
                model.UO_CA_ItemID = ItemID;
            }
            else
            {
                model.UO_CA_ItemID = null;
            }
            if (Requestee_Store_ID != null)
            {
                model.UO_CA_Store_id = Requestee_Store_ID;
            }
            else
            {
                model.UO_CA_Store_id = null;
            }
                model.UO_CA_Center = BASE._open_Cen_ID;
                
            if(UOid != 0)
            {
                model.UOid = UOid;
            }

            if(Screenname == null)
            {
                model.screenname = null;
            }
            else
            {
                model.screenname = Screenname;
            }
            return View(model);
        }

        public ActionResult Check_StockAvailability(int? Item_ID = null, string Item_Category = null, int Center = 0, int Store = 0, string Location = null)

        {
            if (Item_ID == null) 
            {
                return Json(new
                {
                    result = false,
                    message = "Item Name Can Not be Empty...!"
                }, JsonRequestBehavior.AllowGet);
            }
            Common_Lib.RealTimeService.Param_Get_Stock_Availability Param = new Common_Lib.RealTimeService.Param_Get_Stock_Availability();
            Param.ItemType = object.ReferenceEquals(Item_Category, "") ? null : Item_Category;
            Param.LocationID = object.ReferenceEquals(Location, "") ? null : Location;
            Param.ItemID = Convert.ToInt32(Item_ID);
            Param.StoreDeptID = object.ReferenceEquals(Store, 0) ? 0 : Store;
            Param.Center = object.ReferenceEquals(Center, 0) ? 0 : Center;
            if(Param.Center == 0)
            {
                Param.Center = null;
            }
            if (Param.StoreDeptID == 0)
            {
                Param.StoreDeptID = null;
            }
            var AvlItems = BASE._user_order_DBOps.Get_Stock_Availability(Param);
           

            Avl_Stock_Item_Grid_Data = AvlItems;
            return PartialView("AvailableStockItemGridData", AvlItems);
        }
        public ActionResult AvailableStockItemGridData()
        {
           
           
                return PartialView(Avl_Stock_Item_Grid_Data);
         
          
        }

        #endregion

        #region User Order Bottom Buttons
        //public ActionResult Job_Status_Get(JobUpDateStatus model)
        //{
        //    switch (model.Job_StatusType)
        //    {
        //        case "Rejected":
        //            model.PopupHeader = "Job Reject..";
        //            break;
        //        case "Changes_Recommended":
        //            model.PopupHeader = "Job Changes..";
        //            break;
        //        case "Requested":
        //            model.PopupHeader = "Job Requested..";
        //            break;
        //        case "Assigned_for_Estimation_Creation":
        //            model.PopupHeader = "Assigned for Estimation...";
        //            break;
        //        case "Submitted_for_Estimate_Approval":
        //            model.PopupHeader = "Submitted for Estimate Approval...";
        //            break;
        //        case "Approved":
        //            model.PopupHeader = "Job Approve";
        //            break;
        //        case "Assigned":
        //            model.PopupHeader = "Job Assign..";
        //            break;
        //        case "In_Progress":
        //            model.PopupHeader = "Job Re-Open...";
        //            break;
        //        case "Completed":
        //            model.PopupHeader = "Completed the Job";
        //            break;
        //        case "Cancelled":
        //            model.PopupHeader = "Job Cancelled";
        //            break;
        //        default:
        //            model.PopupHeader = "";
        //            break;
        //    }
        //    return View(model);




        public ActionResult User_StatusChange(int ID, string StatusButton)
        {

                string msg = "";
                var flag = 1;
                Param_Get_Approval_Required param1 = new Param_Get_Approval_Required();

                var approval = BASE._StockApprovalReqd_dbops.Get_Approval_Required(param1, ClientScreen.Stock_UO);
                var approvalvalue = "No";
                if (approval == false)
                {
                    approvalvalue = "No";
                }
                else
                {
                    approvalvalue = "Yes";
                }

                Param_Update_UO_Status param = new Param_Update_UO_Status();
                param.UOID = ID;
                var _CurrUserRole = BASE._user_order_DBOps.GetRegister(Convert.ToDateTime(UOFromDate), Convert.ToDateTime(UOToDate)).main_Register.FindAll(x => x.ID == ID).FirstOrDefault().CurrUserRole;
                string[] CurrUserRole = new string[] { "" };
                if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }


                var UOdata = BASE._user_order_DBOps.GetUODetails(ID);
                var UOStatus = UOdata.UO_Status;

                    try
                    {
                switch (StatusButton)
                {
                    case "Reopen":
                        if (CurrUserRole.Contains("Requestor"))
                        {
                            if (approvalvalue == "No")
                            {
                                param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Requested ", true);
                                msg = "Status Changed To 'Requested'..!!";
                            }
                            else if (approvalvalue == "Yes")
                            {
                                param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "_New", true);
                                msg = "Status Changed To 'New'..!!";
                            }

                        }

                        if (UOStatus == "Completed")
                        {
                            param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "In_Progress", true);
                            msg = "Status Changed To 'In_Progress'..!!";
                        }
                        break;

                    case "Approve":

                        if (CurrUserRole.Contains("Requestor Incharge"))
                        {
                            if (approvalvalue == "Yes")
                            {
                                param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Requested", true);
                                msg = "Status Changed To 'Requested'..!!";
                            }
                            else if (approvalvalue == "No")
                            {
                                
                                 msg = "Requestor Incharge Cant Approve UO if Approval is not required for UO";
                                flag = 0;
                            }
                        }
                        if (CurrUserRole.Contains("Store Keeper"))
                        {
                            if (approvalvalue == "No")
                            {
                                param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Approved", true);
                                msg = "Status Changed To 'Approved'..!!";
                            }
                            else if (approvalvalue == "Yes")
                            {

                                msg = "Store Keeper Cant Approve UO if Approval is required for UO";
                                flag = 0;
                            }

                        }

                        if (CurrUserRole.Contains("Store Keeper in-charge"))
                        {

                            param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Approved", true);
                            msg = "Status Changed To 'Approved'..!!";

                        }
                        break;


                    case "Complete":


                        param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Completed", true);
                        msg = "Status Changed To 'Completed' for selected UO..!!";
                        break;
                }


                if (flag == 1)
                {
                    if (BASE._user_order_DBOps.UpdateUOStatus(param))
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
                        message = msg
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
        public ActionResult UserOrderStatusFlow(int ID, string StatusButton, string[] CurrUserRole)
        {
         
           
            var data = BASE._user_order_DBOps.GetUODetails(ID);
            var UOStatus = data.UO_Status;
            var UOReqMainDeptID = data.Req_MainDeptID;
            var UoStoreID = data.StoreID;
            NEVD_UserOrder model = new NEVD_UserOrder();
            Param_Get_Approval_Required param = new Param_Get_Approval_Required();

            var approval = BASE._StockApprovalReqd_dbops.Get_Approval_Required(param, ClientScreen.Stock_UO);
            var approvalvalue = "No";
            if (approval == false)
            {
                approvalvalue = "No";
            }
            else
            {
                approvalvalue = "Yes";
            }
            string msg = "";

           
            if (StatusButton.ToUpper() == "COMPLETE")
            {
               if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge") || CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Are Not Allowed To Mark User Order As Completed If User Order Status Is " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Requestor','Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Mark User Order As Completed ..!!";
                }

            }
          
            else if (StatusButton.ToUpper() == "CANCEL")
            {
                if (CurrUserRole.Contains("Requestor"))
                {
                    if (UOStatus == "New" || UOStatus == "Changes Recommended" || UOStatus == "Requested")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor' Is Not Allowed To Cancel User Order If UO Status Is " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Requestor' Can Cancel The User Order...!!";
                }
            }

            else if (StatusButton.ToUpper() == "REOPEN")
            {
                if (CurrUserRole.Contains("Requestor"))
                {
                    if (UOStatus == "Cancelled" || UOStatus == "Changes Recommended")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor' Is Not Allowed To Re-open Order Order If User Order Status Is " + UOStatus + " ..!!";
                    }
                }

                if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge") || CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Completed")
                    {

                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else

                    {
                        msg = " User Orders which are " + UOStatus + " cannot be reopened ..!!";
                    }

                }
                else
                {
                    msg = "Only 'Requestor','Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Re-open The User Order..!!";
                }
            }

            else if (StatusButton.ToUpper() == "REJECT")
            {
                if (CurrUserRole.Contains("Requestor Incharge"))
                {

                    if (approvalvalue == "No")
                    {
                        msg = "'Requestor Incharge' Cannot Reject UO If Approval is not required for UO";

                    }
                    else if (approvalvalue == "Yes")
                    {
                        if (UOStatus == "New" || UOStatus == "Changes Recommended" || UOStatus == "Requested")
                        {

                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Department Incharge' cannot Reject UO If UO Status Is " + UOStatus + "...!!";
                        }
                    }
                }
                if (CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    //if (approvalvalue == "No")
                    //{
                    //    msg = "'Store Keeper Incharge' Cannot Reject UO If Approval is required for UO";

                    //}

                    //else if (approvalvalue == "Yes")

                    //{
                    if (UOStatus == "Changes Recommended" || UOStatus == "Requested" || UOStatus == "Approved")
                    {


                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper Incharge' Cannot Reject UO If UO Status Is " + UOStatus + "...!!";
                    }



                }
                if (CurrUserRole.Contains("Store Keeper"))
                {
                    if (approvalvalue == "Yes")
                    {
                        msg = "'Store Keeper' Cannot Reject UO If Approval is required for UO";

                    }

                    else if (approvalvalue == "No")

                    {
                        if (UOStatus == "Changes Recommended" || UOStatus == "Requested" || UOStatus == "Approved")
                        {


                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Store Keeper' Cannot Reject UO If UO Status Is " + UOStatus + "...!!";
                        }
                    }


                }
                else if(!(CurrUserRole.Contains("Requestor Incharge") || CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge")))
                {
                    msg = "Only 'Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Reject The User Order...!!";
                }

                if (UOStatus == "Rejected" || UOStatus == "In-Progress" || UOStatus == "Cancelled" || UOStatus == "Completed")
                {
                    msg = "UO Cannot Be Rejected If UO Status Is " + UOStatus + "...!!";
                }
            }


            else if (StatusButton.ToUpper() == "CHANGES RECOMMENDED")
            {
                if (CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (approvalvalue == "No")
                    {
                        msg = "'Requestor Incharge' Cannot RECOMMENDED CHANGES in UO If Approval is not required for UO";

                    }
                    else if (approvalvalue == "Yes")
                    {
                        if (UOStatus == "New" || UOStatus == "Rejected" || UOStatus == "Requested")
                        {

                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Department Incharge' cannot Recommend Changes for UO If UO Status Is " + UOStatus + "...!!";
                        }
                    }
                }
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Rejected" || UOStatus == "Requested" || UOStatus == "Approved")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge' Cannot Recommend Changes for UO If UO Status Is " + UOStatus + "...!!";
                    }
                }                
                else if (!(CurrUserRole.Contains("Requestor Incharge")))
                {
                    msg = "Only 'Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Recommend Changes For The User Order...!!";
                }
                
            }


            else if (StatusButton.ToUpper() == "APPROVE")
            {
                                               
                if (CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (approvalvalue == "No")
                    {
                        msg = "'Requestor Incharge' Cannot Approve  UO If Approval is not required for UO";

                    }
                    else if (approvalvalue == "Yes")
                    {
                        if (UOStatus == "New" || UOStatus == "Rejected" || UOStatus == "Changes Recommended")
                        {

                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Department Incharge' Cannot Approve UO If UO Status Is " + UOStatus + "...!!";
                        }
                    }
                }
                if (CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    //if (approvalvalue == "No")
                    //{
                    //    msg = "'Store Keeper Incharge' Cannot Approve UO If Approval is not required for UO";

                    //}
                    //else if (approvalvalue == "Yes")

                    //{
                        if (UOStatus == "Rejected" || UOStatus == "Changes Recommended" || UOStatus == "Requested")
                        {


                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = " 'Store Keeper Incharge' Cannot Approve UO If UO Status Is " + UOStatus + "...!!";
                        }
               

                }

                if (CurrUserRole.Contains("Store Keeper") )
                {
                    if (approvalvalue == "Yes")
                    {
                        msg = "'Store Keeper' Cannot Approve UO If Approval is required for UO";

                    }

                    else
                    {
                        if (UOStatus == "Rejected" || UOStatus == "Changes Recommended" || UOStatus == "Requested")
                        {


                            return Json(new
                            {
                                result = true,
                                message = ""
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Store Keeper' Cannot Approve UO If UO Status Is " + UOStatus + "...!!";
                        }
                    }


                }
                else if(! (CurrUserRole.Contains("Requestor Incharge") || CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge")))
                {
                    msg = "Only 'Requestor Department Incharge','Store Keeper','Store Keeper Incharge' can Approve The UO...!!";
                }

                if (UOStatus == "Approved" || UOStatus == "In-Progress" || UOStatus == "Completed")
                {
                    msg = "UO Cannot Be Approved If UO Status Is " + UOStatus + "...!!";
                }

            }

            else if (StatusButton.ToUpper() == "EDIT")
            {
                if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (UOStatus == "New" || UOStatus == "Changes Recommended" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if(UOStatus == "Cancelled")
                    {
                        msg = "You cannot Edit User Order If UO Status IS " + UOStatus + " ..!!";
                    }
                    else
                    {
                        msg = "You cannot Edit User Order If UO Status IS " + UOStatus ;
                    }
                }

                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Requested" || UOStatus == "Approved" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (UOStatus == "Cancelled")
                    {
                        msg = "You cannot Edit User Order If UO Status IS " + UOStatus + " .., please Re-open the User Order to Edit.";
                    }
                    else
                    {
                        msg = "'You cannot Edit User Order If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else if (!(CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge")))
                {

                    msg = "Only 'Requestor','Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Edit The User Order..!!";

                }


            }

            else if (StatusButton.ToUpper() == "DELETE")
            {
                if (UOStatus == "New")
                {
                    return Json(new
                    {
                        result = true,
                        message = ""
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "You cannot Delete User Order If UO Status IS " + UOStatus + " ..!!";
                }
            }

            else if (StatusButton.ToUpper() == "ADD-ITEMS")
            {
                if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (UOStatus == "New" || UOStatus == "Changes Recommended")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor','Requestor Department Incharge' Are Not Allowed To Add Items If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Requested" || UOStatus == "Approved" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge' Are Not Allowed To Add Items If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else if (!(CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge")))
                {
                    msg = "Only 'Requestor','Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Add Items in User Order..!!";
                }

            }

            else if (StatusButton.ToUpper() == "UPDATE-ITEMS" || StatusButton.ToUpper() == "DELETE-ITEMS")
            {
                if (CurrUserRole.Contains("Requestor"))
                {
                    if (UOStatus == "New" || UOStatus == "Changes Recommended")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor' is Not Allowed To Update/Delete Items If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                if (CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (UOStatus == "New" || UOStatus == "Changes Recommended" || UOStatus == "Requested")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor Department Incharge' is Not Allowed To Update/Delete Items If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Requested" || UOStatus == "Approved" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge' Are Not Allowed To Update/Delete  Items If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else if (!(CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge")))
                {
                    msg = "Only 'Requestor','Requestor Department Incharge','Store Keeper','Store Keeper Incharge' Can Update/Delete Items In User Order..!!";
                }

            }

            else if (StatusButton.ToUpper() == "ADD-GOODS-RETURNED" || StatusButton.ToUpper() == "UPDATE-GOODS-RETURNED" || StatusButton.ToUpper() == "DELETE-GOODS-RETURNED" || StatusButton.ToUpper() == "ADD-GOODS-RECEIVED" || StatusButton.ToUpper() == "UPDATE-GOODS-RECEIVED" || StatusButton.ToUpper() == "DELETE-GOODS-RECEIVED")
            {
                if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor','Requestor Department Incharge' Are Not Allowed To Add/Edit/Delete Goods Received/Returned If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Requestor','Requestor Department Incharge' Can Add/Edit/Delete Goods Received/Returned for The User Order..!!";
                }
            }

            else if (StatusButton.ToUpper() == "ADD-GOODS-DELIVERY-RETURNED" || StatusButton.ToUpper() == "UPDATE-GOODS-DELIVERY-RETURNED" || StatusButton.ToUpper() == "DELETE-GOODS-DELIVERY-RETURNED" || StatusButton.ToUpper() == "ADD-GOODS-DELIVERY" || StatusButton.ToUpper() == "UPDATE-GOODS-DELIVERY" || StatusButton.ToUpper() == "DELETE-GOODS-DELIVERY")
            {
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Requested" || UOStatus == "Approved" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge'Are Not Allowed To Add/Edit/Delete Goods Delivered/ Goods Delivered Returned If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Store Keeper','Store Keeper Incharge' Can Add/Edit/Delete - Goods Delivered/ Goods Delivered Returned for The User Order..!!";
                }
            }

            else if (StatusButton.ToUpper() == "DOCUMENTS")
            {
                if (UOStatus == "Completed")
                {
                    msg = "You Are Not Allowed To Post Documents If UO Status IS " + UOStatus + " ..!!";
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

            else if (StatusButton.ToUpper() == "POST SCRAP CREATED" )
            {
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "Approved" || UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge' Are Not Allowed To Post Scrap Created If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Store Keeper','Store Keeper Incharge' Post Scrap Created for The User Order..!!";
                }
            }


            else if (StatusButton.ToUpper() == "PRINT RECOVERY SLIP" || StatusButton.ToUpper() == "PRINT GATE PASS")
            {
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                    if (UOStatus == "In-Progress") 
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Store Keeper','Store Keeper Incharge' Are Not Allowed To Print Recovery Slip / Gate Pass If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Store Keeper','Store Keeper Incharge' Can Print Recovery Slip / Gate Pass for The User Order..!!";
                }
            }

            else if (StatusButton.ToUpper() == "SCHEDULE DELIVERY")
            {
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
                  
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
               
               
                }
                else
                {
                    msg = "Only 'Store Keeper','Store Keeper Incharge' Can Schedule Delivery for The User Order..!!";
                }
            }
            else if (StatusButton.ToUpper() == "PRINT GOODS RECEIPT")
            {
                if (CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Incharge"))
                {
                    if (UOStatus == "In-Progress")
                    {
                        return Json(new
                        {
                            result = true,
                            message = ""
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "'Requestor','Requestor Department Incharge' Are Not Allowed To Print Goods Receipt If UO Status IS " + UOStatus + " ..!!";
                    }
                }
                else
                {
                    msg = "Only 'Requestor','Requestor Department Incharge' Can Print Goods Receipt For The User Order..!!";
                }
            }

            else if (StatusButton.ToUpper() == "MAP-UNMAP")
            {
                if (CurrUserRole.Contains("Store Keeper") || CurrUserRole.Contains("Store Keeper in-charge"))
                {
           
                }

                else
                {
                    msg = "Only 'Store Keeper','Store Keeper Incharge' Can Map/Unmap User Order ..!!";
                }
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
       
        [HttpGet]
        public ActionResult UO_StatusChange_Window(int ID, string StatusButton)
        {
            UserUpDateStatus model = new UserUpDateStatus();
            model.ID = ID;
            model.UO_StatusType = StatusButton;
      
            return View(model);
        }
        [HttpPost]
        public ActionResult UO_StatusChange_Window(UserUpDateStatus model)
        {

            try
            {

                string msg = "";

                Param_Update_UO_Status param = new Param_Update_UO_Status();
                param.UOID = model.ID;

                if (model.UO_StatusType.ToUpper() == "CANCEL")
                {

                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Cancelled", true);
                    msg = "Status Changed To 'Cancelled'..!!";
                }

                else if (model.UO_StatusType.ToUpper() == "CHANGES RECOMMENDED")
                {
                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Changes_Recommended", true);
                    msg = "Status Changed To 'Changes_Recommended' for selected UO..!!";
                }

                else if (model.UO_StatusType.ToUpper() == "REJECT")
                {

                    param.UpdatedStatus = (UO_Status)Enum.Parse(typeof(UO_Status), "Rejected", true);
                    msg = "Status Changed To 'Rejected' for selected UO..!!";
                }


                if (BASE._user_order_DBOps.UpdateUOStatus(param))
                {
                    Param_InsertUORemarks Inparam = new Param_InsertUORemarks();
                    Inparam.UO_ID = model.ID;
                    Inparam.Remarks = model.UO_Status_Remark;
                 
                    if (BASE._user_order_DBOps.InsertUORemarks(Inparam))
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
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    result = true,
                    message = ""
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


        #endregion
        //  public ActionResult Document_grid(string ActionMethodName, string ID)
        //{
        //    var model = new DocPartial();
        //    model.Id = Convert.ToInt32(ID);
        //    model.ActionMethod = ActionMethodName;
        // return PartialView(model);
        //}
        // public ActionResult Scrap_Created_Grid(string ActionMethodName, string ID)
        // {
        //var model = new DocPartial();
        //model.Id = Convert.ToInt32(ID);
        //model.ActionMethod = ActionMethodName;
        //  return PartialView(model);
        //  }
        //  public ActionResult Existin_Remarks_Grid(string ActionMethodName, string ID)
        // {
        //var model = new DocPartial();
        //model.Id = Convert.ToInt32(ID);
        //model.ActionMethod = ActionMethodName;
        // return PartialView(model);
        // }

        //public ActionResult Add_Item_Checks(int UO_ID)
        //{
        //    var temp = BASE._user_order_DBOps.GetRecord(UO_ID);
        //    var Status = "";

        //    return Json(new
        //    {
        //        result = true,
        //        message = "",
        //        Uo_Status = Status
        //    }, JsonRequestBehavior.AllowGet);
        //}

        #region"MISC"
        public void UOSessionclear()
        {
            Session.Remove("UserOrderDocument");            
            BASE._SessionDictionary.Remove("UserOrder_Documents_Attachment_AttachmentData");
            BASE._SessionDictionary.Remove("UserOrderDocument_AttachmentData");
            ClearBaseSession("_UO");
        } //clears session variable on popup close

        public void InfoSessionclear()
        {
            ClearBaseSession("_InfoUO");
        }
        public void UODeliveryStockGridSessionclear()
        {

            ClearBaseSession("_UO_Stock");
        }
        
        public void UO_user_rights()
        {
            ViewData["UO_AddRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Add");
            ViewData["UO_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Update");
            ViewData["UO_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "View");
            ViewData["UO_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Delete");
            ViewData["UO_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Export");
            ViewData["UO_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Report");
            ViewData["UO_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_UO, "Approve");
            ViewData["UO_AddJobRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Add");
            ViewData["UO_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["UO_ViewJobRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "View");
            ViewData["UO_AddStockItemRight"] = CheckRights(BASE, ClientScreen.Stock_Sub_Item, "Add");
            ViewData["UO_AddRRRight"] = CheckRights(BASE, ClientScreen.Stock_RR, "Add");
            ViewData["UO_AddHelpRequestRight"] = CheckRights(BASE, ClientScreen.Help_Request_Box, "Add");
        }
        public void UOAddLocation_user_rights()
        {
            ViewData["UO_ProfileCore__AddRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Profile_Core, "Add");
        }
        #endregion
    }
}