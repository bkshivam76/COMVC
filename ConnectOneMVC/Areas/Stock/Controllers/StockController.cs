using Common_Lib;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Common_Lib.RealTimeService;
using static Common_Lib.DbOperations.StockProfile;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    public class StockController : BaseController
    {
        // GET: Stock/Profile


        #region Global Variables

        public List<StockProfile> StockprofileData 
        {
            get
            {
                return (List<StockProfile>)GetBaseSession("StockprofileData_ProfInfo");
            }
            set
            {
                SetBaseSession("StockprofileData_ProfInfo", value);
            }
        }

        
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Stock_Info()
        {
            Stock_user_rights();         
            if (CheckRights(BASE, ClientScreen.Profile_Stock, "List"))
            {
                ViewBag.ShowHorizontalBar = 0;
                ViewBag.IsSuperUserOrAuditor = BASE._open_User_Type.ToUpper() == "SUPERUSER" || BASE._open_User_Type.ToUpper() == "AUDITOR" ? true : false;
                var Stockdata = BASE._Stock_Profile_DBOps.GetProfiledata();
                DateTime? dt = null;//Mantis bug 0000340 fixed
                var list = Stockdata.Select(q => new StockProfile
                {
                    Store_Name = q.Store_Name,
                    Item_Name = q.Item_Name,
                    Serial_Lot_No = q.Serial_Lot_No,
                    Item_Type = q.Item_Type,
                    Item_Code = q.Item_Code,
                    Make = q.Make,
                    Model = q.Model,
                    Warranty = q.Warranty,
                    Unit = q.Unit,
                    Opening_Qty = q.Opening_Qty,
                    Opening_Value = Convert.ToDecimal(q.Opening_Value),
                    Date_of_Purchase = string.IsNullOrEmpty(Convert.ToString(q.Date_of_Purchase)) ? dt : Convert.ToDateTime(q.Date_of_Purchase),
                    Location = q.Location,
                    Current_Qty = Convert.ToDecimal(q.Current_Qty),
                    Current_Value = Convert.ToDecimal(q.Current_Value),
                    Proj_Name = q.Proj_Name,
                    Total_Value = Convert.ToDecimal(q.Current_Value),
                    Remarks = q.Remarks,
                    REC_ADD_ON = Convert.ToDateTime(q.REC_ADD_ON),
                    REC_ADD_BY = q.REC_ADD_BY,
                    REC_EDIT_ON = Convert.ToDateTime(q.REC_EDIT_ON),
                    REC_EDIT_BY = q.REC_EDIT_BY,
                    REC_STATUS = q.REC_STATUS,
                    REC_STATUS_ON = Convert.ToDateTime(q.REC_STATUS_ON),
                    REC_STATUS_BY = q.REC_STATUS_BY,
                    REC_ID = q.REC_ID,
                    YEAR_ID = q.YEAR_ID,
                    Stock_TR_ID = q.Stock_TR_ID.ToString(),
                    Store_ID = q.Store_ID,
            }).ToList();//Mantis bug 0000337 fixed//Mantis bug 0000340 fixed         

                StockprofileData = list;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(StockprofileData);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Stock').hide();</script>");
            }
        }//Mantis bug 0000367 fixed

        public ActionResult Frm_Stock_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            Stock_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (StockprofileData == null || command == "REFRESH")
            {
                var Stockdata = BASE._Stock_Profile_DBOps.GetProfiledata();
                DateTime? dt = null;//Mantis bug 0000340 fixed
                var list = Stockdata.Select(q => new StockProfile
                {
                    Store_Name = q.Store_Name,
                    Item_Name = q.Item_Name,
                    Serial_Lot_No = q.Serial_Lot_No,
                    Item_Type = q.Item_Type,
                    Item_Code = q.Item_Code,
                    Make = q.Make,
                    Model = q.Model,
                    Warranty = q.Warranty,
                    Unit = q.Unit,
                    Opening_Qty = q.Opening_Qty,
                    Opening_Value = Convert.ToDecimal(q.Opening_Value),
                    Proj_Name = q.Proj_Name,
                    Date_of_Purchase = string.IsNullOrEmpty(Convert.ToString(q.Date_of_Purchase)) ? dt : Convert.ToDateTime(q.Date_of_Purchase),
                    Location = q.Location,
                    Current_Qty =Convert.ToDecimal( q.Current_Qty),
                    Current_Value = Convert.ToDecimal(q.Current_Value),
                    Total_Value = Convert.ToDecimal(q.Current_Value),
                    Remarks = q.Remarks,
                    REC_ADD_ON = Convert.ToDateTime(q.REC_ADD_ON),
                    REC_ADD_BY = q.REC_ADD_BY,
                    REC_EDIT_ON = Convert.ToDateTime(q.REC_EDIT_ON),
                    REC_EDIT_BY = q.REC_EDIT_BY,
                    REC_STATUS = q.REC_STATUS,
                    REC_STATUS_ON = Convert.ToDateTime(q.REC_STATUS_ON),
                    REC_STATUS_BY = q.REC_STATUS_BY,
                    REC_ID = q.REC_ID,
                    YEAR_ID = q.YEAR_ID,
                    Stock_TR_ID = q.Stock_TR_ID.ToString(),
                    Store_ID = q.Store_ID,
                }).ToList();//Mantis bug 0000337 fixed//Mantis bug 0000340 fixed
                StockprofileData = list;            
            }
            return PartialView("Frm_Stock_Info_Grid", StockprofileData);
        }
        #region Grid Row binding
        public ActionResult StockRowCustomDataAction(int? key)
        {
            var FinalRegData = StockprofileData as List<StockProfile>;
            var FDData = FinalRegData != null ? (StockProfile)FinalRegData.Where(f => f.REC_ID == key).FirstOrDefault() : null;
            string StockReg = "";
            if (FDData != null)
            {
                StockReg = FDData.REC_ID + "![" + FDData.Store_Name + "![" + FDData.Item_Name + "![" + FDData.Serial_Lot_No + "![" + FDData.Item_Type + "![" +
                           FDData.Item_Code + "![" + FDData.Make + "![" + FDData.Model + "![" + FDData.Warranty + "![" + FDData.Unit + "![" + FDData.Opening_Qty + "![" + FDData.Opening_Value
                           + "![" + FDData.Date_of_Purchase + "![" + FDData.Location + "![" + FDData.Current_Qty + "![" + FDData.Current_Value + "![" + FDData.Remarks + "![" + FDData.REC_ADD_ON + "![" + FDData.REC_ADD_BY + "![" +
                           FDData.REC_EDIT_ON + "![" + FDData.REC_EDIT_BY + "![" + FDData.REC_STATUS + "![" + FDData.REC_STATUS_ON + "![" + FDData.REC_STATUS_BY + "![" + FDData.Store_Id
                           + "![" + FDData.Stock_TR_ID + "![" + FDData.YEAR_ID + "![" + FDData.Store_ID;

            }
            return GridViewExtension.GetCustomDataCallbackResult(StockReg);
        }
        #endregion
        public ActionResult Frm_Stock_Window(string ActionMethod = null,int recId=0)  
        {
            Stock_user_rights();
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_Stock, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#popup_frm_StockProfile_Window').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }            

            ADDStockProfile model = new ADDStockProfile();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            if (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._New)
            {
                model.ActionMethod = Navigation_Mode_tag;
                model.TempActionMethod = ActionMethod;
                //Store Auto Fill Values
                var Storelist = BASE._StockDeptStores_dbops.GetStoreList();
                if (Storelist.Count == 1)
                {
                    model.Store_Name = Storelist[0].Store_Name;
                    model.Store_Id = Storelist[0].StoreID;
                }
                //Location Auto Fill Values
                var Locationlist = BASE._Stock_Profile_DBOps.GetLocations();
                if (Locationlist.Count == 1)
                {
                    model.Location = Locationlist[0].Location_Name;
                }
            }
            if (Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Edit
                || Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._View
                || Navigation_Mode_tag == Common_Lib.Common.Navigation_Mode._Delete)
            {
                model.ActionMethod = Navigation_Mode_tag;
                model.TempActionMethod = ActionMethod;
                model.Rec_ID = recId;
                DataTable Stockdata = BASE._Stock_Profile_DBOps.GetRecord(recId);
                model.Item_Name = (Stockdata.Rows[0]["Stock_sub_Item_ID"]).ToString();//Mantis bug 0000338 fixed
                model.Make = Stockdata.Rows[0]["Stock_Make"].ToString();
                model.Model = Stockdata.Rows[0]["Stock_Model"].ToString();
                model.Serial_Lot_No = Stockdata.Rows[0]["Stock_Lot_Serial_no"].ToString();
                model.Store_Name = Stockdata.Rows[0]["Stock_Store_ID"].ToString();
                model.Store_Id = (Int32)Stockdata.Rows[0]["Stock_Store_ID"];
                model.UnitID = Stockdata.Rows[0]["Stock_Unit_ID"].ToString();
                model.Total_Value = Convert.ToDouble(Stockdata.Rows[0]["Stock_Value"]);
                model.Opening_Qty = Convert.ToDouble(Stockdata.Rows[0]["Stock_Quantity"]);
                model.Stock_Remarks = Stockdata.Rows[0]["Stock_Remarks"].ToString();
                if(!(Stockdata.Rows[0]["Stock_Date_of_Purchase"] == System.DBNull.Value))
                {
                   model.Date_of_Purchase = Convert.ToDateTime(Stockdata.Rows[0]["Stock_Date_of_Purchase"]);
                }
                model.Location = Stockdata.Rows[0]["Stock_Location_ID"].ToString();
                model.Warranty = Stockdata.Rows[0]["Stock_Warranty"].ToString();
                if (!(Stockdata.Rows[0]["Stock_Project_ID"]== System.DBNull.Value))
                {
                    model.Project = Convert.ToInt32(Stockdata.Rows[0]["Stock_Project_ID"]);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Stock_Window(ADDStockProfile Addmodel)
        {
            Addmodel.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Addmodel.TempActionMethod);            
            try
            {
                if (((Addmodel.ActionMethod == Common_Lib.Common.Navigation_Mode._New) || Addmodel.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (IsDate(Addmodel.Date_of_Purchase.ToString()) == true)
                    {
                        DateTime firstDate = BASE._open_Year_Sdt;
                        DateTime secondDate = Addmodel.Date_of_Purchase == null ? DateTime.Now : Convert.ToDateTime(Addmodel.Date_of_Purchase);
                        TimeSpan diff = secondDate.Subtract(firstDate);
                        TimeSpan diff1 = secondDate - firstDate;
                        double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());

                        if (diff2 < 0)
                        {
                            return Json(new
                            {
                                result = false,
                                Message = "Purchase date should be earlier to Current Financial year ...!"
                            }, JsonRequestBehavior.AllowGet);
                        }

                        DateTime firstDate1 = BASE._open_Year_Edt;
                        DateTime secondDate1 = Addmodel.Date_of_Purchase == null ? DateTime.Now : Convert.ToDateTime(Addmodel.Date_of_Purchase);
                        TimeSpan diff3 = secondDate1.Subtract(firstDate1);
                        TimeSpan diff4 = secondDate1 - firstDate1;
                        double diff5 = Convert.ToDouble((secondDate1 - firstDate1).TotalDays.ToString());

                        if (diff5 > 0)
                        {
                            return Json(new
                            {
                                result = false,
                                Message = "Purchase date should be earlier to Current Financial year ...!"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Addmodel.Opening_Qty == 0 || Addmodel.Opening_Qty < 0)
                    {
                        return Json(new
                        {
                            Message = "Opening Quantity cannot be Negative or Zero ...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Addmodel.Total_Value == 0 || Addmodel.Total_Value < 0)
                    {
                        return Json(new
                        {
                            Message = "Total Value cannot be Negative or Zero ...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    bool Value = false;
                    Value = BASE._Stock_Profile_DBOps.GetStockDuplication(Addmodel.Item_Name.ToString(), Addmodel.Make, Addmodel.Model, Addmodel.Serial_Lot_No);
                    if (Value == true)
                    {
                        return Json(new
                        {
                            Message = "Same lot no & make & model with same item name should not have been used elsewhere ...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Addmodel.TempActionMethod) == Common_Lib.Common.Navigation_Mode._New))
                {
                    bool Result = true;
                   Param_Add_StockProfile InParam = new Param_Add_StockProfile();
                    //InParam.Project_ID = Addmodel.Project              
                    InParam.Store_Dept_ID = Addmodel.Store_Id;
                    InParam.item_id = int.Parse(Addmodel.Item_Name);//Mantis bug 0000291 fixed
                    InParam.make = Addmodel.Make;
                    InParam.model = Addmodel.Model;
                    InParam.serial_no = Addmodel.Serial_Lot_No;
                    InParam.Quantity = Addmodel.Opening_Qty;
                    InParam.Unit_Id = Addmodel.UnitID;
                    if (Addmodel.Date_of_Purchase != null)
                    {
                        InParam.Date_Of_Purchase = Convert.ToDateTime(Addmodel.Date_of_Purchase);
                    }
                    InParam.total_value = Addmodel.Total_Value;
                    InParam.Location_Id = Addmodel.Location;
                    InParam.Project_ID = Addmodel.Project;
                    InParam.Warranty = Addmodel.Warranty;
                    InParam.Remarks = Addmodel.Stock_Remarks??"";

                    if (!BASE._Stock_Profile_DBOps.AddStockProfile(InParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Saved Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Addmodel.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    //Edit
                    bool Result = true;
                    Common_Lib.RealTimeService.Param_Update_StockProfile UpParam = new Common_Lib.RealTimeService.Param_Update_StockProfile();
                    UpParam.Store_Dept_ID = Addmodel.Store_Id;
                    UpParam.sub_Item_ID = Convert.ToInt32(Addmodel.Item_Name);//Mantis bug 0000338 fixed
                    UpParam.make = Addmodel.Make;
                    UpParam.model = Addmodel.Model;
                    UpParam.serial_no = Addmodel.Serial_Lot_No;
                    UpParam.Quantity = Addmodel.Opening_Qty;
                    UpParam.Unit_Id = Addmodel.UnitID;
                    if (Addmodel.Date_of_Purchase != null)
                    {
                        UpParam.Date_Of_Purchase = Convert.ToDateTime(Addmodel.Date_of_Purchase);
                    }
                    UpParam.total_value = Addmodel.Total_Value;
                    UpParam.Location_Id = Addmodel.Location;
                    UpParam.Project_ID = Addmodel.Project;
                    UpParam.Warranty = Addmodel.Warranty;
                    UpParam.Remarks = Addmodel.Stock_Remarks??"";
                    UpParam.StockID = Addmodel.Rec_ID.ToString();
                    UpParam.Rec_ID = Addmodel.Rec_ID;
                    if (!BASE._Stock_Profile_DBOps.UpdateStockProfile(UpParam))
                    {
                        Result = false;
                    }

                    if (Result)
                    {
                        return Json(new
                        {
                            Message = "Updated Successfully!!",
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                if (((Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + Addmodel.TempActionMethod) == Common_Lib.Common.Navigation_Mode._Delete))
                {
                    //bool Result = true;

                    if (!BASE._Stock_Profile_DBOps.DeleteStockProfile(Addmodel.Rec_ID))
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    Message = "Deleted Successfully!!",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    Message =ex.Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DataNavigation(string ActionMethod, int REC_ID,int REC_STATUS,string Stock_TR_ID,int recYearID=0)
        {
            try
            {
                ADDStockProfile model = new ADDStockProfile();
                  if (ActionMethod == "Edit")
                {
                    string XREC_ID = REC_ID.ToString();
                    var AuditCompleted = BASE._CenterDBOps.IsFinalAuditCompleted();
                    if (AuditCompleted == true)
                    {
                        return Json(new { Message = "No entries are allowed in Split year ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var IsStockCarried = BASE._Stock_Profile_DBOps.IsStockCarriedForward(REC_ID, recYearID);
                    if (IsStockCarried == true)
                    {
                        return Json(new { Message = "Entry c/f from previous years cannot be edited if previous year is not split yet ", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (REC_STATUS==2)
                    {
                        return Json(new { Message = "Locked entry cannot be updated / deleted ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable Asset = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Stock, 0, XREC_ID, false);
                    DataTable Order = BASE._Transfer_Orders_DBOps.GetTransferOrders(REC_ID);
                    if(Asset.Rows.Count>0 && Order.Rows.Count>0)
                    {
                        return Json(new { Message = "Stock Entry against which Asset Transfer / Transfer Order has been posted can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable SaleRef= BASE._Voucher_DBOps.GetSaleReferenceRecord(REC_ID.ToString());
                    if(SaleRef.Rows.Count>0)
                    {
                        return Json(new { Message = "Stock Entry against which Sale Entry has been posted can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    //DataTable JornalEntry = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(REC_ID, Common_Lib.RealTimeService.ClientScreen.Stock_Profile);
                    //if (JornalEntry.Rows.Count > 0)
                    //{
                    //    return Json(new { Message = "Stock Entry against which JV/Discard Entry has been posted can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    //}
                    DataTable Deliveries= BASE._user_order_DBOps.GetDeliveries(REC_ID);
                    if (Deliveries.Rows.Count>0)
                    {
                        return Json(new { Message = "Stock Entry against which User Order has been posted and delivered can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable GetProduction= BASE._Stock_Production_DBOps.GetProductions(REC_ID);
                    if(GetProduction.Rows.Count>0)
                    {
                        return Json(new { Message = "Stock Entry against which Production Entry has been posted can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if(Stock_TR_ID!=null)
                    {
                        return Json(new { Message = "Stock Entry created from Voucher Screen (PO/Payment/Donation in Kind/Asset Trasnfer/Transfer Order) cannot be edited in Profile ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "Delete")
                {
                    string XREC_ID = REC_ID.ToString();
                    var AuditCompleted = BASE._CenterDBOps.IsFinalAuditCompleted();
                    if (AuditCompleted == true)
                    {
                        return Json(new { Message = "No entries are allowed in Split year ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var IsStockCarried = BASE._Stock_Profile_DBOps.IsStockCarriedForward(REC_ID, recYearID);
                    if (IsStockCarried == true)
                    {
                        return Json(new { Message = "Entry c/f from previous years cannot be edited if previous year is not split yet ", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (REC_STATUS == 2)
                    {
                        return Json(new { Message = "Locked entry cannot be updated / deleted ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable Asset = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_Stock, 0, XREC_ID, false);
                    DataTable Order = BASE._Transfer_Orders_DBOps.GetTransferOrders(REC_ID);
                    if (Asset.Rows.Count > 0 && Order.Rows.Count > 0)
                    {
                        return Json(new { Message = "Stock Entry against which Asset Transfer / Transfer Order has been posted can not be deleted ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    //DataTable JornalEntry = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(REC_ID, Common_Lib.RealTimeService.ClientScreen.Stock_Profile);
                    //if (JornalEntry.Rows.Count > 0)
                    //{
                    //    return Json(new { Message = "Stock Entry against which JV/Discard Entry has been posted can not be updated ..!", result = true }, JsonRequestBehavior.AllowGet);
                    //}
                    DataTable SaleRef = BASE._Voucher_DBOps.GetSaleReferenceRecord(REC_ID.ToString());
                    if (SaleRef.Rows.Count > 0)
                    {
                        return Json(new { Message = "Stock Entry against which Sale Entry has been posted can not be deleted ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable GetDeliveries = BASE._user_order_DBOps.GetDeliveries(REC_ID);
                    if(GetDeliveries.Rows.Count > 0)
                    {
                        return Json(new { Message = "Stock Entry against which User Order has been posted and delivered can not be deleted ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable Production= BASE._Stock_Production_DBOps.GetProductions(REC_ID);
                    if(Production.Rows.Count > 0)
                    {
                        return Json(new { Message = "Stock Entry against which Production Entry has been posted can not be deleted ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (Stock_TR_ID != null)
                    {
                        return Json(new { Message = "Stock Entry created from Voucher Screen (PO/Payment/Donation in Kind/Asset Trasnfer/Transfer Order) cannot be deleted in Profile ..!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "View")
                {
                    
                }
                  else if(ActionMethod== "LOCKED")
                {
                    if (REC_STATUS == 2)
                    {
                        return Json(new { Message = "Already locked entry cannot be locked ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable Status = BASE._Stock_Profile_DBOps.GetRecord(REC_ID);
                    if (Convert.ToInt32(Status.Rows[0]["REC_STATUS"]) != REC_STATUS)
                    {
                        return Json(new { Message = "Entry which has been updated or locked in background, can not be locked ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var AuditCompleted = BASE._CenterDBOps.IsFinalAuditCompleted();
                    if (AuditCompleted == true)
                    {
                        return Json(new { Message = "No entries are allowed in Split year ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var Lock = BASE._Stock_Profile_DBOps.MarkAsLocked(REC_ID.ToString());
                    if (Lock == true)
                    {
                        return Json(new { Message = "Locks the selected record.", result = true }, JsonRequestBehavior.AllowGet);
                    }


                }
                else if(ActionMethod == "UNLOCKED")
                {
                    if (REC_STATUS == 1)
                    {
                        return Json(new { Message = "Already unlocked entry cannot be unlocked ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable Status = BASE._Stock_Profile_DBOps.GetRecord(REC_ID);
                    if (Convert.ToInt32(Status.Rows[0]["REC_STATUS"]) != REC_STATUS)
                    {
                        return Json(new { Message = "Entry which has been updated or locked/unlocked in background, cannot be unlocked ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var AuditCompleted = BASE._CenterDBOps.IsFinalAuditCompleted();
                    if (AuditCompleted == true)
                    {
                        return Json(new { Message = "No entries are allowed in Split year ...!", result = true }, JsonRequestBehavior.AllowGet);
                    }
                    var IsUnlock= BASE._Stock_Profile_DBOps.MarkAsUnLocked(REC_ID.ToString());
                    if (IsUnlock == true)
                    {
                        return Json(new { Message = "Unlocks the selected record.", result = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_Stock_Change_Location(string ActionMethod = null,string FData="")
        {
            if ((!CheckRights(BASE, ClientScreen.Profile_Stock, "Update")))//Code Used for User Authorization please do not alter or delete.//Mantis bug 0001028 fixed
            {
                return Content("<script language='javascript' type='text/javascript'>$('#popup_frm_Stock_Change_Location').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>"); //$('#StockProfileListPreview').hide(); once got the id for button will update it.//Mantis bug 0001028 fixed
            }
            ViewData["Stock_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            ChangeLocationInfo model = new ChangeLocationInfo();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;
            model.Change_Date = DateTime.Now;
            model.Change_Time = DateTime.Now;
            var Data = FData.Split(',');
            model.Item_Name = Data[2];
            model.Item_Type = Data[4];
            model.Item_Code = Data[5];
            model.Exist_Location = Data[13];
            model.REC_ID = Convert.ToInt32(Data[0]);
            model.Store_Id = Data[27];
            model.Rec_Edit_On = Data[19];
            return View(model);
        }
        public ActionResult Frm_Stock_Change_Location_Save(ChangeLocationInfo model)
        {
            if (model.REC_ID != 0)
            {
                DataTable DTstatus= BASE._Stock_Profile_DBOps.GetRecord(model.REC_ID);
                var temp = BASE._Stock_Profile_DBOps.GetProfiledata(model.REC_ID);
                if (DTstatus.Rows.Count > 0)//Mantis bug 0001186 fixed
                { 
                    if(DTstatus.Rows[0]["Rec_Status"].ToString() == "2")
                    {
                        return Json(
                         new
                         {
                             result = false,
                             Message = "Location cannot be changed if profile entry has been Locked in background..!"
                         }, JsonRequestBehavior.AllowGet);
                    }
                    if(model.Rec_Edit_On != DTstatus.Rows[0]["REC_EDIT_ON"].ToString())
                    {
                        return Json(
                        new
                        {
                            result = false,
                            Message = "Location cannot be changed if profile entry has been Updated/Deleted in background..!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                
                if(Convert.ToDouble(temp[0].Current_Qty) == 0)
                {
                    return Json(
                    new
                    {
                        result = false,
                        Message = "Location cannot be changed if profile entry's Current Quantity has become zero..!"
                    }, JsonRequestBehavior.AllowGet);
                }//Mantis bug 0001186 fixed
            }            

            Param_Update_StockLocation UpParam = new Param_Update_StockLocation();//Mantis bug 0001186 fixed
            UpParam.LocationID = model.Stk_Profile_Location;
            UpParam.Location_Change_Date = model.Change_Date;
            UpParam.StockID = model.REC_ID;
            UpParam.UpdationRemarks = model.Remarks;
            UpParam.RequestorID = (int)BASE._open_User_PersonnelID;

            if (BASE._Stock_Profile_DBOps.UpdateStockLocation(UpParam))
            {
                return Json(
                    new
                    {
                        result = true,
                        Message = "Location Changed Successfully"
                    }, JsonRequestBehavior.AllowGet);
            }//Mantis bug 0001186 fixed
            return null;
        }
        public ActionResult Frm_Stock_Change_Project()
        {
            return View();
        }

        public ActionResult IsFinalAuditCompleted()
        {
            var AuditCompleted = BASE._CenterDBOps.IsFinalAuditCompleted();
            if (AuditCompleted == true)
            {
                return Json(new { Message = "No entries are allowed in Split year ...!", result = true }, JsonRequestBehavior.AllowGet);
            }
            if (BASE._Completed_Year_Count > 0)
            {
                return Json(new { Message = "New Entries are allowed only in Existing Center 1st year ...!", result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "", result = false }, JsonRequestBehavior.AllowGet);
        }
        

        #region  Look_Up Events
        public ActionResult LookUp_GetStoreList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Storelist = BASE._StockDeptStores_dbops.GetStoreList();
            var list = Storelist.Select(q => new StoreInfo
            {
                StoreName = q.Store_Name,
                StoreId = q.StoreID,
                DeptName=q.Dept_Name,
                SubDeptName=q.Sub_Dept_Name,
                StoreInchargeName=q.Store_Incharge_Name,
                DeptInchargeName=q.Dept_Incharge_Name
            }).ToList();          
            DataTable dt = ToDataTable(list);
            var Storelistdata = DatatableToModel.LookUp_StoreList(dt);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Storelistdata, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetStockItems(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Itemlist = BASE._Stock_Profile_DBOps.GetStockItems();
            var list = Itemlist.Select(q => new ItemInfo
            {
                ItemId = q.Item_ID,
                ItemName = q.Stock_Item_Name,
                ItemCategory = q.Item_Category,
                ItemType = q.Item_Type,
                ItemCode = q.Item_Code,
                Unit = q.Unit,
                UnitID=q.UnitID
            }).ToList();
            DataTable dt =ToDataTable(list);
            var itemlistdata = DatatableToModel.LookUp_GetStockItemsList(dt);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(itemlistdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_GetLocations(bool? IsVisible, DataSourceLoadOptions loadOptions, int? stockstoreid)
        {
            if (stockstoreid == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.StockProfile.Return_GetLocations>(), loadOptions)));
            }
            var Locationlist = BASE._Stock_Profile_DBOps.GetLocations(Convert.ToInt32(stockstoreid));
            //var _location = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Stock_Profile, "","");
            var list = Locationlist.Select(q => new LocationInfo
            {
                LocId = q.Loc_Id,
                LocationName = q.Location_Name,
                MatchedName = q.Matched_Name,
                MatchedType = q.Matched_Type,
                MatchedInstt = q.Matched_Instt,             
            }).ToList();
            DataTable dt = ToDataTable(list);
            var locationlistdata = DatatableToModel.LookUp_GetLocations(dt);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(locationlistdata, loadOptions)), "application/json");
        }

        public ActionResult LookUp_GetProjects(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Projectlist = BASE._Projects_Dbops.GetList();
            var list = Projectlist.Select(q => new ProjectInfo
            {
                ProjectId = q.Project_Id,
                ProjectName = q.Project_Name,
                Sanctionno = q.Sanction_no,
                ComplexName = q.Complex_Name,
            }).ToList();
            DataTable dt = ToDataTable(list);
            var projectlistdata = DatatableToModel.LookUp_GetProjects(dt);
            projectlistdata = projectlistdata != null ? projectlistdata : new List<ProjectInfo>();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(projectlistdata, loadOptions)), "application/json");
        }
        //public ActionResult LookUp_GetChangeLocations(bool? IsVisible, DataSourceLoadOptions loadOptions)
        //{
        //    var Locationlist = BASE._Stock_Profile_DBOps.GetLocations();
        //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(projectlistdata, loadOptions)), "application/json");
        //}
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable();
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE,ClientScreen.Profile_Stock, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#StockProfile_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#StockProfileListPreview').hide();</script>");
            }

            return PartialView();
        }
        #endregion
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void SessionClear()
        {
            ClearBaseSession("_Prof");
        }

        public void InfoSessionclear()
        {
            ClearBaseSession("_ProfInfo");
        }
        public void Stock_user_rights()
        {
            ViewData["Stock_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "Add");
            ViewData["Stock_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "Update");
            ViewData["Stock_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "View");
            ViewData["Stock_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "Delete");
            ViewData["Stock_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "Export");
            ViewData["Stock_ReportRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "Report");
            ViewData["Stock_ListRight"] = CheckRights(BASE, ClientScreen.Profile_Stock, "List");
            ViewData["Stock_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
        }       
    }
}