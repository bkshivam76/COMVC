using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOne.D0010._001;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Utils.Extensions;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WhatsappBusiness.CloudApi;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Exceptions;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.Media.Requests;
using WhatsappBusiness.CloudApi.Messages.Requests;
using WhatsappBusiness.CloudApi.Response;
using System.Net.Http;
using static Common_Lib.DbOperations.DonationRegister;
using System.Configuration;

namespace ConnectOneMVC.Areas.Account.Controllers
{
  
    public class DonationRegisterController : BaseController
    {
        // GET: Account/DonationRegister
        #region Global Variables
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> Donation_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("Donation_AdditionalInfoGrid_DonRegInfo");
            }
            set
            {
                SetBaseSession("Donation_AdditionalInfoGrid_DonRegInfo", value);
            }
        }
        public List<Return_DonationRegister> DonationRegister_ExportData
        {
            get
            {
                return (List<Return_DonationRegister>)GetBaseSession("DonationRegister_ExportData_DonRegInfo");
            }
            set
            {
                SetBaseSession("DonationRegister_ExportData_DonRegInfo", value);
            }
        }
        public DateTime FromDate
        {
            get { return (DateTime)GetBaseSession("FromDate_DonRegInfo"); }
            set { SetBaseSession("FromDate_DonRegInfo", value); }
        }
        public DateTime ToDate
        {
            get { return (DateTime)GetBaseSession("ToDate_DonRegInfo"); }
            set { SetBaseSession("ToDate_DonRegInfo", value); }
        }
        public List<Return_DonationRegister_Prints> DonationRegister_Print_ExportData
        {
            get
            {
                return (List<Return_DonationRegister_Prints>)GetBaseSession("DonationRegister_Print_ExportData_DonRegInfo");
            }
            set
            {
                SetBaseSession("DonationRegister_Print_ExportData_DonRegInfo", value);
            }
        }
        public List<Return_DonationRegister_Dispatches> DonationRegister_Dispatches_ExportData
        {
            get
            {
                return (List<Return_DonationRegister_Dispatches>)GetBaseSession("DonationRegister_Dispatches_ExportData_DonRegInfo");
            }
            set
            {
                SetBaseSession("DonationRegister_Dispatches_ExportData_DonRegInfo", value);
            }
        }
        public List<Return_DonationRegister_Rejections> DonationRegister_Rejection_ExportData
        {
            get
            {
                return (List<Return_DonationRegister_Rejections>)GetBaseSession("DonationRegister_Rejection_ExportData_DonRegInfo");
            }
            set
            {
                SetBaseSession("DonationRegister_Rejection_ExportData_DonRegInfo", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> DonationRegisterInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("DonationRegisterInfo_DetailGrid_Data_DonRegInfo");
            }
            set
            {
                SetBaseSession("DonationRegisterInfo_DetailGrid_Data_DonRegInfo", value);
            }
        }
        string WhatsAppBusinessPhoneNumberId = "104658105823288";
        string WhatsAppBusinessAccountId = "100277319602234";
        string WhatsAppBusinessId = "100277319602234";
        string AccessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW";
        #endregion
        [CheckLogin]
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
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
        [CheckLogin]
        public ActionResult Period_ChangeEvent(string Chaval)
        {
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
            string Period = "Period: Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            FromDate = xFr_Date;
            ToDate = xTo_Date;
            return Json(new
            {
                Period
            }, JsonRequestBehavior.AllowGet);

        }
        [CheckLogin]
        public ActionResult Frm_Change_Period_Screen()
        {
            DonationRegisterPeriod model = new DonationRegisterPeriod();
            model.DonReg_Fromdate = FromDate;
            model.DonReg_Todate = ToDate;
            ViewBag.MaxDate = BASE._open_Year_Edt;
            return View(model);
        }
        [CheckLogin]
        [HttpPost]
        public ActionResult Frm_Change_Period_Screen(DonationRegisterPeriod model)
        {
            if (model.DonReg_Fromdate == null)
            {
                return Json(new
                {
                    result = false,
                    message = "Enter From Date"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.DonReg_Todate == null)
            {
                return Json(new
                {
                    result = false,
                    message = "Enter To Date"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.DonReg_Todate < model.DonReg_Fromdate)
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Cannot Be Less Than From Date"
                }, JsonRequestBehavior.AllowGet);
            }
            FromDate = (DateTime)model.DonReg_Fromdate;
            ToDate = (DateTime)model.DonReg_Todate;
            string Period = "Period: Fr.: " + FromDate.ToString("dd-MMM, yyyy") + "  to  " + ToDate.ToString("dd-MMM, yyyy");
            return Json(new
            {
                result = true,
                Period
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Frm_Donation_Register_Info()
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.OpenPeriod = "APR-" + BASE._open_Year_Sdt.Year;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewBag.Filename = "DonationRegister_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            ViewData["DonationReg_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "Export");
            ViewData["DonationReg_HelpAttachmentAddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["DonationReg_HelpAttachmentListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
            ViewData["DonationReg_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            
            DonationRegister_ExportData = new List<Return_DonationRegister>();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Accounts_DonationRegister).ToString()) ? 1 : 0;

            Int32 Curr_Financial_Year = BASE._open_Year_ID;
            Int32 cenid = BASE._open_Cen_ID;
            DataTable dt = BASE._DonationRegister_DBOps.get_GatewayBankslist(cenid, Curr_Financial_Year);
            ViewBag.gatewayBanksCount = dt.Rows.Count;
            
            return View(DonationRegister_ExportData);
            
        }
        [CheckLogin]
        public PartialViewResult Frm_Donation_Register_Grid(string command = null, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["DonationReg_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "Export");
            ViewData["DonationReg_HelpAttachmentAddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["DonationReg_HelpAttachmentListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewBag.Filename = "DonationRegister_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            if (DonationRegister_ExportData == null || command == "REFRESH")
            {
                var Donation_Data = BASE._DonationRegister_DBOps.GetList(FromDate, ToDate);
                DonationRegister_ExportData = Donation_Data;
            }
            var _Donation_Data = DonationRegister_ExportData;
            if ((_Donation_Data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_Donation_Data);
        }
        [CheckLogin]
        public PartialViewResult Frm_Donation_Register_Prints_Grid(string command, string DonationID, int ShowHorizontalBar = 0)
        {

            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["DonationID"] = DonationID;
            if (DonationRegister_Print_ExportData == null || command == "REFRESH")
            {
                var DonationPrint_Data = BASE._DonationRegister_DBOps.GetDonationPrints();
                DonationRegister_Print_ExportData = DonationPrint_Data;
                Session["DonationRegister_Print_ExportData"] = DonationPrint_Data;
            }
            List<Return_DonationRegister_Prints> _DonationPrints = DonationRegister_Print_ExportData;
            var sel_bal_info = _DonationPrints.Find(x => x.DR_TR_ID == DonationID);
            var ret_list = new List<Return_DonationRegister_Prints> { sel_bal_info };
            return PartialView("Frm_Donation_Register_Prints_Grid", ret_list);
        }
        [CheckLogin]
        public PartialViewResult Frm_Donation_Register_Dispatches_Grid(string command, string DonationID, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["DonationID"] = DonationID;
            if (DonationRegister_Dispatches_ExportData == null || command == "REFRESH")
            {
                var DonationDispatches_Data = BASE._DonationRegister_DBOps.GetDonationDispatches(DonationID);
                Session["DonationRegister_Dispatches_ExportData"] = DonationDispatches_Data;
                DonationRegister_Dispatches_ExportData = DonationDispatches_Data;
            }
            //List<Return_DonationRegister_Dispatches> _DonationDispatches = DonationRegister_Dispatches_ExportData;
            //List<Return_DonationRegister_Dispatches> sel_bal_info = _DonationDispatches.FindAll(x => x.DR_TR_ID == DonationID);
            ////var ret_list = new List<Return_DonationRegister_Dispatches> { sel_bal_info };
            return PartialView("Frm_Donation_Register_Dispatches_Grid", DonationRegister_Dispatches_ExportData);
        }
        [CheckLogin]
        public PartialViewResult Frm_Donation_Register_Rejection_Grid(string command, string DonationID, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["DonationID"] = DonationID;
            if (DonationRegister_Rejection_ExportData == null || command == "REFRESH")
            {
                var DonationRejection_Data = BASE._DonationRegister_DBOps.GetDonationRejections();
                Session["DonationRegister_Rejection_ExportData"] = DonationRejection_Data;
                DonationRegister_Rejection_ExportData = DonationRejection_Data;
            }
            List<Return_DonationRegister_Rejections> _DonationRejections = DonationRegister_Rejection_ExportData;
            var sel_bal_info = _DonationRejections.Find(x => x.DS_TR_ID == DonationID);
            var ret_list = new List<Return_DonationRegister_Rejections> { sel_bal_info };
            return PartialView("Frm_Donation_Register_Rejection_Grid", ret_list);
        }
        [CheckLogin]
        public ActionResult Frm_Donation_Register_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.DonationRegisterInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.DonationRegisterInfo_RecID = RecID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Accounts_DonationRegister);
                    DonationRegisterInfo_DetailGrid_Data = _docList;
                    Session["DonationRegisterInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, "", BASE._open_Cen_ID, ClientScreen.Accounts_DonationRegister);
                    DonationRegisterInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["DonationRegisterInfo_detailGrid_Data"] = data.DocumentMapping;
                    Donation_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(DonationRegisterInfo_DetailGrid_Data);
        }
        [CheckLogin]
        public ActionResult AdditionalInfo_Grid()
        {
            return View(Donation_AdditionalInfoGrid);
        }
        [CheckLogin]
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        [CheckLogin]
        public static GridViewSettings NestedGridExportSettings(string DonationID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "DonRegisterListGrid" + DonationID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "DonRegisterListGrid";
            return settings;
        }
        [CheckLogin]
        public static IEnumerable NestedGridExportData(string DonationID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["DonationRegisterInfo_detailGrid_Data"];
        }
        [CheckLogin]
        public ActionResult Refresh_GridIcon_PreviewRow(string TempID, string NestedRowKeyValue)
        {
            DonationRegister_ExportData = BASE._DonationRegister_DBOps.GetList(FromDate, ToDate);
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(TempID, "", ClientScreen.Accounts_DonationRegister);
            DonationRegisterInfo_DetailGrid_Data = _docList;
            var AttachmentRow = DonationRegisterInfo_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History = AttachmentRow.Vouching_History;
            string Main_iIcon = DonationRegister_ExportData.Where(x => x.ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult DonRegNestedCustomDataAction(string key = null)
        {
            var Data = DonationRegisterInfo_DetailGrid_Data as List<DbOperations.Audit.Return_GetDocumentMapping>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.UniqueID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Doc_Status + "![" + it.Params_Mandatory + "![" + it.LABEL_FROM_DATE + "![" + it.LABEL_TO_DATE + "![" + it.LABEL_DESCRIPTION + "![" + it.Document_Category + "![" + it.Document_ID
                        + "![" + it.ATTACH_ID + "![" + it.TxnID + "![" + it.TxnMID + "![" + it.MAP_ID + "![" + it.Reason + "![" + it.ATTACH_FILE_NAME + "![" +
                        it.Attachment_Action_Status + "![" + it.UniqueID + "![" + it.ReasonID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        [CheckLogin]
        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
          string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
                string Pic_Status = string.Empty;
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;
                string Lbl_Status_Color = string.Empty;
                try
                {
                    Status = Action_Status;
                }
                catch (Exception ex)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Completed";
                    Lbl_Status_Color = "blue";
                    Pic_Status = "Fa Fa-Lock";
                }
                else
                {
                    Pic_Status = "Fa Fa-UnLock";
                    Lbl_Status = Status;
                    if (Status.ToUpper().Trim().ToString() == "COMPLETED")
                        Lbl_Status_Color = "green";
                    else
                        Lbl_Status_Color = "red";
                }
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Locked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Locked On: " + "?";
                    }
                    Lbl_StatusBy = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                else
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Unlocked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Unlocked On: " + "?";
                    }
                    Lbl_StatusBy = "Unlocked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Lbl_Status_Color,
                    Lbl_StatusBy = Lbl_StatusBy,
                    Lbl_StatusOn = Lbl_StatusOn
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = "",
                    Lbl_StatusBy = "",
                    Lbl_StatusOn = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [CheckLogin]
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        [CheckLogin]
        public ActionResult Request_Receipt(string IDs, string StatusIDs, string Statuses, string AB_IDs)
        {
            string[] Donation_Status = Statuses.Split(',');
            string[] Donation_Status_ID = StatusIDs.Split(',');
            string[] Donation_ID = IDs.Split(',');
            string[] Donation_AB_ID = AB_IDs.Split(',');
            for (int i = 0; i < Donation_ID.Length; i++)
            {
                string xTemp_ID = Donation_ID[i];
                string xTemp_0 = Donation_AB_ID[i];
                string xTemp_6 = Donation_Status_ID[i];
                string xTemp_7 = Donation_Status[i];
                if ((xTemp_6.Trim().ToString() ?? "") != "42189485-9b6b-430a-8112-0e8882596f3c" && (xTemp_6.Trim().ToString() ?? "") != "3a99fadc-b336-480d-8116-fbd144bd7671" && xTemp_6.Trim().ToString().Length > 0) // Donation Accepted & " REJECT
                {
                    return Json(new
                    {
                        result = false,
                        Message = "Donation Request cannot be Applied...! <br/> Current Status: " + xTemp_7,
                    }, JsonRequestBehavior.AllowGet);
                }

                var Address_Book = BASE._DonationRegister_DBOps.GetAddressDetail(xTemp_0);
                var ReqParam = new Common_Lib.RealTimeService.Parameter_Request_Receipt();
                if (xTemp_6.Trim().ToString().Length <= 0 | (xTemp_6.Trim().ToString() ?? "") == "3a99fadc-b336-480d-8116-fbd144bd7671")
                {
                    ReqParam.param_InsertReceiptRequest = new Common_Lib.RealTimeService.Param_DonationRegister_InsertReceiptRequest();
                    ReqParam.param_InsertReceiptRequest.openYearID = BASE._open_Year_ID;
                    ReqParam.param_InsertReceiptRequest.TransactionID = xTemp_ID;
                }
                else
                {
                    ReqParam.TxnID_UpdateReceiptRequest = xTemp_ID;
                }
                ReqParam.TxnID_DeleteAddressBook = xTemp_ID;
                var AddParam = new Common_Lib.RealTimeService.Parameter_InsertDonationAddress_DonationRegister[Address_Book.Rows.Count + 1];
                int ctr = 0;
                foreach (DataRow XRow in Address_Book.Rows)
                {
                    var InParam = new Common_Lib.RealTimeService.Parameter_InsertDonationAddress_DonationRegister();
                    InParam.TransactionID = xTemp_ID;
                    InParam.AB_ID = Convert.ToString(XRow["REC_ID"]);
                    InParam.Name = Convert.ToString(XRow["C_NAME"]);
                    InParam.PAN = Convert.ToString(XRow["C_PAN_NO"]);
                    InParam.PassportNo = Convert.ToString(XRow["C_PASSPORT_NO"]);
                    InParam.Add1 = Convert.ToString(XRow["C_R_ADD1"]);
                    InParam.Add2 = Convert.ToString(XRow["C_R_ADD2"]);
                    InParam.Add3 = Convert.ToString(XRow["C_R_ADD3"]);
                    InParam.Add4 = Convert.ToString(XRow["C_R_ADD4"]);
                    InParam.CityID = Convert.ToString(XRow["C_R_CITY_ID"] is System.DBNull ? "" : XRow["C_R_CITY_ID"]);
                    InParam.DistrictID = Convert.ToString(XRow["C_R_DISTRICT_ID"] is System.DBNull ? "" : XRow["C_R_DISTRICT_ID"]);
                    InParam.StateID = Convert.ToString(XRow["C_R_STATE_ID"] is System.DBNull ? "" : XRow["C_R_STATE_ID"]);
                    InParam.CountryID = Convert.ToString(XRow["C_R_COUNTRY_ID"]);
                    InParam.PinCode = Convert.ToString(XRow["C_R_PINCODE"]);
                    AddParam[ctr] = InParam;
                    ctr += 1;
                }

                ReqParam.InAddress = AddParam;
                if (!BASE._DonationRegister_DBOps.RequestAReceipt(ReqParam))
                {
                    return Json(new
                    {
                        result = false,
                        Message = Common_Lib.Messages.SomeError,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                result = true,
                Message = "Request Applied Successfully for Selected Entries !!",
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        [HttpPost]
        public ActionResult Generate_Receipt(string IDs, string StatusIDs, string Statuses, string AB_IDs, string Donation_Types, string Donation_Dates, string Form_Received_Dates)
        {
            string[] Donation_Status = Statuses.Split(',');
            string[] Donation_Status_ID = StatusIDs.Split(',');
            string[] Donation_ID = IDs.Split(',');
            string[] Donation_AB_ID = AB_IDs.Split(',');
            string[] Donation_Kind = Donation_Types.Split(',');
            string[] Donation_Voucher_Date = Donation_Dates.Split(',');
            string[] Donation_Form_Received_Date = Form_Received_Dates.Split(',');
            for (int i = 0; i < Donation_ID.Length; i++)
            {
                string DonationID = Donation_ID[i];
                string ABID = Donation_AB_ID[i];
                string StatusID = Donation_Status_ID[i];
                string Status = Donation_Status[i];
                string DonationKind = Donation_Kind[i];
                string VoucherDate = Donation_Voucher_Date[i];
                string FormReceivedDate = Donation_Form_Received_Date[i];
                if ((Status.Trim().ToLower() ?? "") != "donation accepted" && (Status.Trim().ToLower() ?? "") != "receipt request rejected" && (Status.Trim().ToLower() ?? "") != "receipt requested" && Status.Trim().Length > 0) // Donation Accepted & " REJECT
                {
                    return Json(new
                    {
                        result = false,
                        Message = "Acknowledgement cannot be Generated...! <br/> Current Status: " + Status,
                    }, JsonRequestBehavior.AllowGet);
                }
                //var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                //if (SubmittedPeriod != null)
                //{
                //    if (SubmittedPeriod.Rows.Count > 0)
                //    {
                //        if (Convert.ToDateTime(VoucherDate) >= Convert.ToDateTime(SubmittedPeriod.Rows[0]["FROMDATE"]) && Convert.ToDateTime(VoucherDate) <= Convert.ToDateTime(SubmittedPeriod.Rows[0]["TODATE"]))
                //        {
                //            return Json(new
                //            {
                //                Message = "No Changes Are Allowed In Accounts Submitted Period. Generated Donation Entry Date Should Not Be In Account Submitted Period...!<br> If you want to Generate this Receipt, Kindly Unsubmit Accounts for this Period. ",
                //                result = false,
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //}
                DateTime _FormReceiveDate = DateTime.MinValue;
                if (FormReceivedDate.Length > 0 && FormReceivedDate!= "null")
                    _FormReceiveDate = Convert.ToDateTime(FormReceivedDate);
                string ReceiptNo = GenerateReceipt(DonationID, BASE._open_Year_ID.ToString(), DonationKind, Convert.ToDateTime(VoucherDate), Status, ABID, _FormReceiveDate);
                if (ReceiptNo.Length>0)
                {
                    string SenderNumber= ConfigurationManager.AppSettings["DefaultWhatsAppSender"];
                    BASE._Notifications_DBOps.NotifyDonationGeneratebyWhatsapp(DonationID, SenderNumber);
                    /* e-acknowledgement section
                    DataTable _table = BASE._Address_DBOps.GetRecord(ABID);
                    //List<DbOperations.Voucher_Donation.Return_DonationGetRecord> _Donation = BASE._Donation_DBOps.GetRecord(DonationID);
                    List<DbOperations.DonationRegister.Return_DonationRegister> _Donation = BASE._DonationRegister_DBOps.GetList(BASE._open_Year_Sdt, BASE._open_Year_Edt,DonationID);
                    if (_table.Rows.Count > 0)
                    {
                        string MobNo = ""; string Email = "";
                        string CenterMobNo = BASE._CenterDBOps.Get_Contact_Info(Convert.ToInt32(BASE._open_PAD_No_Main))[1].Contact1;
                        CenterMobNo = CenterMobNo.Length > 0 ? CenterMobNo : "9414052546";
                        if (_table.Rows[0]["C_MOB_NO_1"] != System.DBNull.Value) MobNo = _table.Rows[0]["C_MOB_NO_1"].ToString();
                        if (_table.Rows[0]["C_EMAIL_ID_1"] != System.DBNull.Value) Email = _table.Rows[0]["C_EMAIL_ID_1"].ToString();
                        if (MobNo.Length > 0 & ReceiptNo.Length > 0)
                        {
                            //BASE._DonationRegister_DBOps.Send_E_Acknowledgement_Whatsapp(BASE._open_Ins_Name, AckNo As String, DonorName As String, DonorAddress As String, DonorIDName As String, DonorIDNo As String, DonorEmail As String, DonorWhatsappNo As String, DonationAmount As Decimal, DonationMode As String, DonationModeRefNo As String, DonationDate As DateTime, DonationType As String, CenterNAme As String, CenterContactNo As String, DonationID As String)
                            Send_E_Acknowledgement_Whatsapp(BASE._open_Ins_Short, ReceiptNo, _Donation[0].Donor_Name , _Donation[0].Address + ", " + _Donation[0].City + ", Dist. " + _Donation[0].District + ", " + _Donation[0].State + " - " + _Donation[0].PinCode, _Donation[0].PAN.Length > 0 ? "PAN" : "AADHAR", _Donation[0].PAN.Length > 0 ? _Donation[0].PAN : _Donation[0].AADHAR, Email, MobNo, (decimal)_Donation[0].Amount, _Donation[0].Mode, _Donation[0].Cheque_DD_Ref_No, (DateTime)_Donation[0].Cheque_DD_Ref_Date, DonationKind, BASE._open_Cen_Name, CenterMobNo, DonationID);
                        }
                    }
                    */

                }
                else {
                    return Json(new
                    {
                        result = false,
                        Message = Common_Lib.Messages.SomeError,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                result = true,
                Message = "Acknowledgement Generated Successfully for Selected Entries !!",
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Returns Receipt no if generated
        /// </summary>
        /// <param name="Rec_ID"></param>
        /// <param name="Year_ID"></param>
        /// <param name="DType"></param>
        /// <param name="VoucherDate"></param>
        /// <param name="currStatus"></param>
        /// <param name="Address_ID"></param>
        /// <param name="FormReceiptDate"></param>
        /// <returns></returns>
        [CheckLogin]
        private string GenerateReceipt(string Rec_ID, string Year_ID, string DType, DateTime VoucherDate, string currStatus, string Address_ID, DateTime FormReceiptDate)
        {
            try
            {
               

                string ReqDonationType = "";
                if ("FOREIGN" == DType.ToUpper())
                    ReqDonationType = "FOREIGN";
                else
                    ReqDonationType = "REGULAR";
              return  BASE._DonationRegister_DBOps.GenerateReceipt(Rec_ID, Year_ID, BASE._open_User_ID, FormReceiptDate, VoucherDate, BASE._open_Cen_ID.ToString(), BASE._open_Cen_ID.ToString(), ReqDonationType, currStatus, Address_ID);
                
            }
            catch (Exception ex)
            {
                throw ex;
                return "";
            }
           // return true;
        }
        [CheckLogin]
        public System.Threading.Tasks.Task<WhatsAppResponse> Send_E_Acknowledgement_Whatsapp(string InsttName, string AckNo, string DonorName, string DonorAddress, string DonorIDName, string DonorIDNo, string DonorEmail, string DonorWhatsappNo, decimal DonationAmount, string DonationMode, string DonationModeRefNo, DateTime DonationDate, string DonationType, string CenterNAme, string CenterContactNo, string DonationID)
        {
            IWhatsAppBusinessClient _whatsAppBusinessClient;
            // Dim _whatsAppConfig As WhatsAppBusinessCloudApiConfig
            WhatsAppBusinessCloudApiConfig whatsAppConfig;
            whatsAppConfig = new WhatsAppBusinessCloudApiConfig();
            whatsAppConfig.WhatsAppBusinessPhoneNumberId = WhatsAppBusinessPhoneNumberId;
            whatsAppConfig.WhatsAppBusinessAccountId = WhatsAppBusinessAccountId;
            whatsAppConfig.WhatsAppBusinessId = WhatsAppBusinessId;
            whatsAppConfig.AccessToken = AccessToken;
            _whatsAppBusinessClient = new WhatsAppBusinessClient(whatsAppConfig, true);

            DynamicURLTemplateMessageRequest textTemplateMessage = new DynamicURLTemplateMessageRequest();
            textTemplateMessage.To = DonorWhatsappNo;
            textTemplateMessage.Template = new DynamicMessageTemplate();
            textTemplateMessage.Template.Name = "donation_e_acknowledgement";
            textTemplateMessage.Template.Language = new DynamicMessageLanguage();
            textTemplateMessage.Template.Language.Code = LanguageCode.English_UK;
            textTemplateMessage.Template.Components = new List<DynamicMessageComponent>();
            DynamicMessageComponent bodyComponents = new DynamicMessageComponent();
            bodyComponents.Type = "body";
            bodyComponents.Parameters = new List<DynamicMessageParameter>()
            {
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = AckNo
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorName
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorAddress
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorIDName
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorIDNo
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorEmail
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonorWhatsappNo
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationAmount.ToString()
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationMode.ToTitleCase()
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationModeRefNo
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationDate.ToString("dd-MMM-yyyy")
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationType
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = CenterNAme.ToTitleCase()
                },
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = CenterContactNo
                }
            };
            textTemplateMessage.Template.Components.Add(bodyComponents);
            DynamicMessageComponent headerComponents = new DynamicMessageComponent();
            headerComponents.Type = "header";
            headerComponents.Parameters = new List<DynamicMessageParameter>()
            {
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = InsttName
                }
            };
            textTemplateMessage.Template.Components.Add(headerComponents);
            DynamicMessageComponent buttonComponents = new DynamicMessageComponent();
            buttonComponents.Type = "button";
            buttonComponents.sub_type = "URL";
            buttonComponents.Parameters = new List<DynamicMessageParameter>()
            {
                new DynamicMessageParameter()
                {
                    Type = "text",
                    text = DonationID
                }
            };
            textTemplateMessage.Template.Components.Add(buttonComponents);
            return _whatsAppBusinessClient.SendDynamicURLButtonMessageAsync(textTemplateMessage);
        }
        [CheckLogin]
        public ActionResult Donation_Register_AddRejectionReason(string DonationID = null, string RejectionRemarks = "", string ABID = "")
        {
            ViewBag.DonationID = DonationID;
            ViewBag.RejectionRemarks = RejectionRemarks;
            ViewBag.ABID = ABID;
            return View();
        }
        [CheckLogin]
        public ActionResult Donation_Register_UpdateRejectionReason(string DonationIDs = "", string RejectReason = "", string ABID = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string[] All_DonationIDs = DonationIDs.Split(','); 
                for (int i = 0; i < All_DonationIDs.Count(); i++)
                {
                    string EmailSendStatus = "";
                    if (BASE._DonationRegister_DBOps.RejectReceipt(All_DonationIDs[i], RejectReason,BASE._open_User_ID.ToString(), ABID))
                    {
                        EmailSendStatus = BASE._Notifications_DBOps.SendEmailOnNotificationEvent(All_DonationIDs[i], "DONATION ACKNOWLEDGEMENT REJECTION");
                        jsonParam.message = "The selected Donation Acknowledgement(s) have been marked as <b> Rejected </b>! <br/>"+ EmailSendStatus;
                            
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.title = "SUCCESS";
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult DonationReceipt(string RecIDs, string type)
        {
            XtraReport CombinedReport = null;
            int Counter = 0;
            foreach (string Txn_Rec_ID in RecIDs.Split(','))
            {
                //if (Status[Counter].ToString().ToLower().Equals(Donation.DonationStatus.DONATION_ACCEPTED) || Status[Counter].ToString().ToLower().Equals(Donation.DonationStatus.RECIEPT_REQUEST_REJECTED) || Status[Counter].ToString().ToLower().Equals(Donation.DonationStatus.RECIEPT_CANCELLED))
                //{ Counter += 1; continue; }
                XtraReport currReport = null;

                if (CombinedReport == null)
                {
                    currReport = new ConnectOneMVC.Reports.DonationRecpt(Txn_Rec_ID, type, "blank", BASE);
                    CombinedReport = currReport;
                    CombinedReport.CreateDocument();
                }
                else
                {
                    currReport = new ConnectOneMVC.Reports.DonationRecpt(Txn_Rec_ID, type, "blank", BASE);
                    currReport.CreateDocument();
                    ((XtraReport)CombinedReport).Pages.AddRange(currReport.Pages);
                }
                Counter += 1;
            }
            return View(CombinedReport);
        }
        [CheckLogin]
        public ActionResult Download80GReceipt(string IDs)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string[] Donation_ID = IDs.Split(',');
                string Receipt_Received_Links = "";

                int Total_Count = Donation_ID.Length;
                
                int Receipt_Received_Count = 0;

                foreach (string ID in Donation_ID)
                {
                    List<Return_DonationRegister_Dispatches> dispatchData = BASE._DonationRegister_DBOps.GetDonationDispatches(ID);
                    if (dispatchData == null || dispatchData.Count == 0)
                    {                       
                        continue;
                    }

                    var Receipt80G = dispatchData.Find(x => string.IsNullOrWhiteSpace(x.Reference_No) == false && string.IsNullOrWhiteSpace(x.Location) == false && x.Reference_No.Contains("80G|") && x.Location.Contains("</a>"));

                    if (Receipt80G != null)
                    {
                        Receipt_Received_Count++;
                        Receipt_Received_Links = Receipt_Received_Links + Receipt80G.Location.Split('|')[1] + " |";
                    }
                }                

                string Message = Convert.ToString(Receipt_Received_Count) + " Donation 80G Receipts Have Been Received out of " + Convert.ToString(Total_Count) + " selected Donations.";

                if (Receipt_Received_Links.Length > 0)
                {
                    jsonParam.flag = Receipt_Received_Links;
                    jsonParam.message = Message + "<br> <br> The available receipts have been downloaded.";
                    jsonParam.result = true;
                    jsonParam.title = "Information...";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = Message;
                    jsonParam.result = false;
                    jsonParam.title = "Information...";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [CheckLogin]
        public ActionResult PrintDonationForm(string RecID, DateTime Rec_Edit_ON_Grid, string DsStatusMiscID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (BASE.AllowMultiuser())
            {
                DataTable Donation_Reg = BASE._DonationRegister_DBOps.GetRecDetail(RecID.ToString());
               
                if (Donation_Reg == null)
                {
                    jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                    jsonParam.result = false;
                    jsonParam.title = "Record Changed / Removed in Background!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (Donation_Reg.Rows.Count == 0)
                {
                    jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                    jsonParam.result = false;
                    jsonParam.title = "Record Changed / Removed in Background!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (Donation_Reg.Rows[0]["ITEM_NAME"].ToString().ToLower()== "donation")
                {
                    jsonParam.message = "Donation Forms are not needed for General Donations. Please Generate Acknowledgement for getting Signature of Donor!!";
                    jsonParam.result = false;
                    jsonParam.title = "Not Needed!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                DateTime RecEdit_Date = Convert.ToDateTime(Donation_Reg.Rows[0]["REC_EDIT_ON"]);
                //DateTime Rec_Edit_ON_Grid = DonationRegister_ExportData.Find(x => x.ID == RecID).Edit_Date;
                if (RecEdit_Date != Rec_Edit_ON_Grid)
                {
                    jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                    jsonParam.result = false;
                    jsonParam.title = "Record Changed / Removed in Background!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                string Misc_ID = Donation_Reg.Rows[0]["DS_STATUS_MISC_ID"].ToString();
                if (Misc_ID != DsStatusMiscID)
                {
                    jsonParam.message = Common_Lib.Messages.RecordChanged("Current Donation");
                    jsonParam.result = false;
                    jsonParam.title = "Donation Status Changed!!";
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }

            string country = "";

            Boolean isPan = false;
            string SQL_STR1 = "";
            string CENID = "";
            string Ins = "";
            DataTable TI_Table = BASE._DonationRegister_DBOps.GetRecDetail(RecID);
            //TI_Table = BASE._DonationRegister_DBOps.GetRecDetail(RecID);
            if (TI_Table == null)
            {
                if (TI_Table == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                    "DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            Donation data = new Donation();
            ArrayList ListDataSource = new ArrayList();
            DataRow TransactionInfo = TI_Table.Rows[0];
            //DataRow TransactionInfo = TI_Table.Rows[0];
            data.Finacial_Year = BASE._open_Year_Name;
            DateTime DonationDate = Convert.ToDateTime(TransactionInfo["TR_DATE"]);
            data.DateOf_Donation = DonationDate.ToString(BASE._Date_Format_Current);
            data.Transaction_Mode = TransactionInfo["TR_MODE"].ToString();
            data.Check_No = TransactionInfo["TR_REF_NO"].ToString();
            data.Total_Amount = Convert.ToDouble(TransactionInfo["TR_AMOUNT"].ToString());
            Common_Lib.Common InWords = new Common_Lib.Common();
            //'data.Amount_InWord = InWords.ConvertNumToAlphaValue(TransactionInfo["TR_AMOUNT"].ToString()).ToUpper() & " ONLY."
            Double Amount = Convert.ToDouble(TransactionInfo["TR_AMOUNT"].ToString());
            data.Amount_InWord = BASE.ConvertNumToAlphaValue(Convert.ToDecimal(Amount));
            //'if(((Amount - Math.Floor(Amount)) > 0)) {
            //'    data.Amount_InWord += " and " + (BASE.ConvertNumToAlphaValue(Convert.ToInt64(100 * (Amount - Math.Floor(Amount))))).ToString() + " paise"
            //'}
            //'data.Amount_InWord += " only"
            data.Branch_Name = TransactionInfo["TR_REF_BRANCH"].ToString();
            CENID = TransactionInfo["TR_CEN_ID"].ToString();

            TI_Table = BASE._DonationRegister_DBOps.GetCenterDetails(CENID);
            if (TI_Table == null)
            {
                jsonParam.result = false;
                jsonParam.message = Common_Lib.Messages.SomeError;
                jsonParam.title = "Error!!";
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            DataRow centreInfo = TI_Table.Rows[0];
            data.Centre_Name = centreInfo["CEN_NAME"].ToString();
            data.Centre_BKPad = "{ " + centreInfo["CEN_UID"].ToString() + " }" + " / ( " + centreInfo["CEN_PAD_NO"].ToString() + " )";
            Ins = centreInfo["CEN_INS_ID"].ToString();

            object miscId = BASE._DonationRegister_DBOps.GetMiscNameByID(TransactionInfo["DS_STATUS_MISC_ID"].ToString());
            if (miscId == null)
            {
                jsonParam.result = false;
                jsonParam.message = Common_Lib.Messages.SomeError;
                jsonParam.title = "Error!!";
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

            object bank = BASE._DonationRegister_DBOps.GetBankNameByID(TransactionInfo["TR_REF_BANK_ID"].ToString());
            if (bank != null)
            {
                //' bank = TI_Table.Rows[0](0).ToString()
                data.Bank_Name = bank.ToString();
            }

            if (!(miscId.ToString().ToLower().Equals("donation accepted") || miscId.ToString().ToLower().Equals("receipt request rejected")))
            {
                TI_Table = BASE._DonationRegister_DBOps.GetAddressDetail_Form(false, RecID);
            }
            else
            {
                TI_Table = BASE._DonationRegister_DBOps.GetAddressDetail_Form(true, null, TransactionInfo["TR_AB_ID_1"].ToString());
            }


            if (TI_Table == null)
            {
                jsonParam.result = false;
                jsonParam.message = Common_Lib.Messages.SomeError;
                jsonParam.title = "Error!!";
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            DataRow addressInfo;
            if (TI_Table.Rows.Count > 0)
            {
                addressInfo = TI_Table.Rows[0];
                data.Full_Name = addressInfo["C_NAME"].ToString();
                data.Mobile_No = addressInfo["MobileNo"].ToString();
                data.Email_ID = addressInfo["EmailID"].ToString();
                data.Address_1 = data.AddComma(addressInfo["C_R_ADD1"].ToString()) + data.AddComma(addressInfo["C_R_ADD2"].ToString()) +
                    data.AddComma(addressInfo["C_R_ADD3"].ToString()) + data.AddComma(addressInfo["C_R_ADD4"].ToString());
                data.Pan_No = addressInfo["C_PAN_NO"].ToString();
                if (!(String.IsNullOrEmpty(data.Pan_No)))
                {
                    isPan = true;
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_R_CITY_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetCityList("'" + addressInfo["C_R_CITY_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow cityInfo = TI_Table.Rows[0];
                    data.Address_1 = data.Address_1 + " City:" + data.AddComma(cityInfo["CI_NAME"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_R_DISTRICT_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetDistrictList("'" + addressInfo["C_R_DISTRICT_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow districtInfo = TI_Table.Rows[0];
                    data.Address_1 = data.Address_1 + " Dist:" + data.AddComma(districtInfo["DI_NAME"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_R_STATE_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetStateList("'" + addressInfo["C_R_STATE_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow stateInfo = TI_Table.Rows[0];
                    string PinCode = addressInfo["C_R_PINCODE"].ToString().Length > 0 ? "-" + addressInfo["C_R_PINCODE"].ToString() : "";
                    data.Address_1 = data.Address_1 + " State:" + data.AddComma(stateInfo["ST_NAME"].ToString() + PinCode);
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_R_COUNTRY_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetCountryList("'" + addressInfo["C_R_COUNTRY_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow countryInfo = TI_Table.Rows[0];
                    data.Address_1 = data.Address_1 + countryInfo["CO_NAME"].ToString();
                    country = countryInfo[0].ToString();
                }
                try
                {//the office address need to be taken
                    if (!(String.IsNullOrEmpty(addressInfo["C_AB_ID"].ToString())))
                    {
                        TI_Table = BASE._DonationRegister_DBOps.GetOfficeAddressDetail(addressInfo["C_AB_ID"].ToString());
                        if (TI_Table == null)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        addressInfo = TI_Table.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    //string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    //jsonParam.message = msg;
                    //jsonParam.title = "Error!!";
                    //jsonParam.result = false;
                    //return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }


                if (!(String.IsNullOrEmpty(addressInfo["C_O_ADD1"].ToString())))
                {
                    data.Office_Address = data.AddComma(addressInfo["C_O_ADD1"].ToString()) +
                        data.AddComma(addressInfo["C_O_ADD2"].ToString()) + data.AddComma(addressInfo["C_O_ADD3"].ToString()) +
                        data.AddComma(addressInfo["C_O_ADD4"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_O_DISTRICT_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetDistrictList("'" + addressInfo["C_O_DISTRICT_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow districtInfo = TI_Table.Rows[0];
                    data.Office_Address = data.Office_Address + data.AddComma(districtInfo["DI_NAME"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_O_CITY_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetCityList("'" + addressInfo["C_O_CITY_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow cityInfo = TI_Table.Rows[0];
                    data.Office_Address = data.Office_Address + data.AddComma(cityInfo["CI_NAME"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_O_STATE_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetStateList("'" + addressInfo["C_O_STATE_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow stateInfo = TI_Table.Rows[0];
                    data.Office_Address = data.Office_Address + data.AddComma(stateInfo["ST_NAME"].ToString());
                }
                if (!(String.IsNullOrEmpty(addressInfo["C_O_COUNTRY_ID"].ToString())))
                {
                    TI_Table = BASE._DonationRegister_DBOps.GetCountryList("'" + addressInfo["C_O_COUNTRY_ID"].ToString() + "'");
                    if (TI_Table == null)
                    {
                        jsonParam.result = false;
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DataRow countryInfo = TI_Table.Rows[0];
                    data.Office_Address = data.Office_Address + countryInfo["CO_NAME"].ToString();
                }
            }

            switch (Ins)
            {
                case "00001":
                    BKDonationForm DonationForm = new BKDonationForm();
                    DonationForm.XrCheck.Text = data.Transaction_Mode;
                    DonationForm.XrCheck1.Text = data.Transaction_Mode;
                    DonationForm.XrCheck.Checked = true;
                    DonationForm.XrCheck1.Checked = true;
                    //DonationForm.XrCheck1.CheckBoxState = (CheckBoxState)System.Windows.Forms.CheckState.Checked; This is also working

                    if (isPan)
                    {
                        DonationForm.XrYesPan1.Checked = true;
                        DonationForm.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    DonationForm.DataSource = ListDataSource;
                    return View("BKDonationForm", DonationForm);

                case "00002":
                    if (String.Equals(TransactionInfo["TR_CODE"].ToString(), "6"))
                    {
                        TI_Table = BASE._DonationRegister_DBOps.GetForeignDonationDetail(RecID);
                        if (TI_Table == null)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataRow foreignDonationInfo = TI_Table.Rows[0];
                        WRSTDonationNRIForm WRSTNRIForm = new WRSTDonationNRIForm();
                        WRSTNRIForm.XrCheck.Text = data.Transaction_Mode;
                        WRSTNRIForm.XrCheck1.Text = data.Transaction_Mode;
                        WRSTNRIForm.XrCheck.Checked = true;
                        WRSTNRIForm.XrCheck1.Checked = true;
                        WRSTNRIForm.XrFCurrency.Text = foreignDonationInfo["TR_FOREIGN_AMT"].ToString();
                        WRSTNRIForm.XrFCurrency1.Text = foreignDonationInfo["TR_FOREIGN_AMT"].ToString();
                        data.Total_Amount = Convert.ToDouble(foreignDonationInfo["TR_INR_AMT"]);
                        TI_Table = BASE._DonationRegister_DBOps.GetcurrencyByID(foreignDonationInfo["TR_CUR_ID"].ToString());
                        if (TI_Table == null)
                        {
                            jsonParam.result = false;
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataRow currencyInfo = TI_Table.Rows[0];
                        WRSTNRIForm.XrCurrencyName.Text = currencyInfo["CUR_NAME"].ToString();
                        WRSTNRIForm.XrCurrencyName1.Text = currencyInfo["CUR_NAME"].ToString();
                        data.Address_1 = country;
                        ListDataSource.Add(data);
                        WRSTNRIForm.DataSource = ListDataSource;
                        return View("WRSTNriDonationForm", WRSTNRIForm);
                    }

                    else
                    {
                        WRSTDonationIndianForm WRSTINDForm = new WRSTDonationIndianForm();
                        WRSTINDForm.XrCheck.Text = data.Transaction_Mode;
                        WRSTINDForm.XrCheck1.Text = data.Transaction_Mode;
                        WRSTINDForm.XrCheck.Checked = true;
                        WRSTINDForm.XrCheck1.Checked = true;
                        if (isPan)
                        {
                            WRSTINDForm.XrYesPan1.Checked = true;
                            WRSTINDForm.XrYesPan.Checked = true;
                        }
                        ListDataSource.Add(data);
                        WRSTINDForm.DataSource = ListDataSource;
                        return View("WRSTIndDonationForm", WRSTINDForm);
                    }

                case "00003":
                    RERFDonationForm RerfForm = new RERFDonationForm();
                    RerfForm.XrCheck.Text = data.Transaction_Mode;
                    RerfForm.XrCheck1.Text = data.Transaction_Mode;
                    RerfForm.XrCheck.Checked = true;
                    RerfForm.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        RerfForm.XrYesPan1.Checked = true;
                        RerfForm.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    RerfForm.DataSource = ListDataSource;
                    return View("RERFDonationForm", RerfForm);
                case "00004":
                    BKESDonationForm BKESForm = new BKESDonationForm();
                    BKESForm.XrCheck.Text = data.Transaction_Mode;
                    BKESForm.XrCheck1.Text = data.Transaction_Mode;
                    BKESForm.XrCheck.Checked = true;
                    BKESForm.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        BKESForm.XrYesPan1.Checked = true;
                        BKESForm.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    BKESForm.DataSource = ListDataSource;
                    return View("BKESDonationForm", BKESForm);
                case "00005":
                    TSRTDonationForm TSRTForm = new TSRTDonationForm();
                    TSRTForm.XrCheck.Text = data.Transaction_Mode;
                    TSRTForm.XrCheck1.Text = data.Transaction_Mode;
                    TSRTForm.XrCheck.Checked = true;
                    TSRTForm.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        TSRTForm.XrYesPan1.Checked = true;
                        TSRTForm.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    TSRTForm.DataSource = ListDataSource;
                    return View("TSRTDonationForm", TSRTForm);
                case "00006":
                    SSAFDonationForm SSAFform = new SSAFDonationForm();
                    SSAFform.XrCheck.Text = data.Transaction_Mode;
                    SSAFform.XrCheck1.Text = data.Transaction_Mode;
                    SSAFform.XrCheck.Checked = true;
                    SSAFform.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        SSAFform.XrYesPan1.Checked = true;
                        SSAFform.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    SSAFform.DataSource = ListDataSource;
                    return View("SSAFDonationForm", SSAFform);
                case "00007":
                    MKSDonationForm MKSform = new MKSDonationForm();
                    MKSform.XrCheck.Text = data.Transaction_Mode;
                    MKSform.XrCheck1.Text = data.Transaction_Mode;
                    MKSform.XrCheck.Checked = true;
                    MKSform.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        MKSform.XrYesPan1.Checked = true;
                        MKSform.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    MKSform.DataSource = ListDataSource;
                    return View("MKSDonationForm", MKSform);
                case "00008":
                    RMCSDonationForm RMCSform = new RMCSDonationForm();
                    RMCSform.XrCheck.Text = data.Transaction_Mode;
                    RMCSform.XrCheck1.Text = data.Transaction_Mode;
                    RMCSform.XrCheck.Checked = true;
                    RMCSform.XrCheck1.Checked = true;
                    if (isPan)
                    {
                        RMCSform.XrYesPan1.Checked = true;
                        RMCSform.XrYesPan.Checked = true;
                    }
                    ListDataSource.Add(data);
                    RMCSform.DataSource = ListDataSource;
                    return View("RMCSDonationForm", RMCSform);
            }
            return new EmptyResult();

        }

        #region export       
        [CheckLogin]
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_DonationRegister, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('DonRegister_report_modal','Not Allowed','No Rights');$('#DonRegisterModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        [CheckLogin]
        public static GridViewSettings CreatePrintGridSettings(string DonationID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "PrintGrid_" + DonationID;
            settings.SettingsDetail.MasterGridName = "DonRegisterListGrid";
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "pKey";
            settings.Columns.Add("DR_TR_ID").Visible = false;
            settings.Columns.Add("Print");
            settings.Columns.Add("Print_Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Location");
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }
        [CheckLogin]
        public static IEnumerable GetDonationPrints(string DonationID)
        {
            List<DbOperations.DonationRegister.Return_DonationRegister_Prints> _DonationPrints = (List<DbOperations.DonationRegister.Return_DonationRegister_Prints>)System.Web.HttpContext.Current.Session["DonationRegister_Print_ExportData"];
            var sel_bal_info = _DonationPrints.Find(x => x.DR_TR_ID == DonationID);
            var ret_list = new List<DbOperations.DonationRegister.Return_DonationRegister_Prints> { sel_bal_info };
            return ret_list;
        }
        [CheckLogin]
        public static GridViewSettings CreateDispatchGridSettings(string DonationID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "DispatchGrid_" + DonationID;
            settings.SettingsDetail.MasterGridName = "DonRegisterListGrid";
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "pKey";
            settings.Columns.Add("DR_TR_ID").Visible = false;
            settings.Columns.Add("Mode");
            settings.Columns.Add("Disp_Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Reference");
            settings.Columns.Add("Reference_No").Caption = "Reference No.";
            settings.Columns.Add("Remarks");
            settings.Columns.Add("Location");
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }
        [CheckLogin]
        public static IEnumerable GetDonationDispatches(string DonationID)
        {
            List<DbOperations.DonationRegister.Return_DonationRegister_Dispatches> _DonationPrints = (List<DbOperations.DonationRegister.Return_DonationRegister_Dispatches>)System.Web.HttpContext.Current.Session["DonationRegister_Dispatches_ExportData"];
            var sel_bal_info = _DonationPrints.Find(x => x.DR_TR_ID == DonationID);
            var ret_list = new List<DbOperations.DonationRegister.Return_DonationRegister_Dispatches> { sel_bal_info };
            return ret_list;
        }
        [CheckLogin]
        public static GridViewSettings CreateRejectionGridSettings(string DonationID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "RejectionGrid_" + DonationID;
            settings.SettingsDetail.MasterGridName = "DonRegisterListGrid";
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "pKey";
            settings.Columns.Add("DS_TR_ID").Visible = false;
            settings.Columns.Add("Reason_Of_Rejection").Caption = "Reason Of Rejection";
            settings.Columns.Add(column =>
            {
                {
                    column.FieldName = "Rejected_On";
                    column.Caption = "Rejected On";
                    column.Visible = true;
                    column.ColumnType = MVCxGridViewColumnType.DateEdit;
                }
            });
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }
        [CheckLogin]
        public static IEnumerable GetDonationRejections(string DonationID)
        {
            List<DbOperations.DonationRegister.Return_DonationRegister_Rejections> _DonationPrints = (List<DbOperations.DonationRegister.Return_DonationRegister_Rejections>)System.Web.HttpContext.Current.Session["DonationRegister_Rejection_ExportData"];
            var sel_bal_info = _DonationPrints.Find(x => x.DS_TR_ID == DonationID);
            var ret_list = new List<DbOperations.DonationRegister.Return_DonationRegister_Rejections> { sel_bal_info };
            return ret_list;
        }
        #endregion
        [CheckLogin]
        public void SessionClear()
        {
            ClearBaseSession("_DonRegInfo");
            Session.Remove("DonationRegisterInfo_detailGrid_Data");
        }

        #region Dev Extreme
        [CheckLogin]
        public ActionResult Frm_Donation_Register_Info_dx()
        {
            DonRegister_user_rights();
            if (!(CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.OpenPeriod = "APR-" + BASE._open_Year_Sdt.Year;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["DonationReg_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "Export");
            ViewData["DonationReg_HelpAttachmentAddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["DonationReg_HelpAttachmentListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
            ViewData["DonationReg_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Accounts_DonationRegister).ToString()) ? 1 : 0;

            Int32 Curr_Financial_Year = BASE._open_Year_ID;
            Int32 cenid = BASE._open_Cen_ID;
            DataTable dt = BASE._DonationRegister_DBOps.get_GatewayBankslist(cenid, Curr_Financial_Year);
            ViewBag.gatewayBanksCount = dt.Rows.Count;


            return View();
        }
        [HttpGet]
        public ActionResult Donation_Register_Grid_Load(bool isShowButtonCLicked = false)
        {

            DonationRegister_ExportData = new List<Return_DonationRegister>();
            if (isShowButtonCLicked)
            {
                if (FromDate > DateTime.MinValue)
                {
                    DonationRegister_ExportData = BASE._DonationRegister_DBOps.GetList(FromDate, ToDate);
                }
            }
            return Content(JsonConvert.SerializeObject(DonationRegister_ExportData), "application/json");

        }
        [HttpGet]
        public ActionResult Donation_Payment_Gateway_Logs_Grid_Load()
        {
            Int32 Curr_Financial_Year = BASE._open_Year_ID;
            Int32 cenid = BASE._open_Cen_ID;
            DataTable dt = BASE._DonationRegister_DBOps.get_PaymentGateway_Logs(cenid, Curr_Financial_Year);            
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }

        [HttpGet]
        public ActionResult Donation_Register_DetailGrid_Load(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Accounts_DonationRegister, !VouchingMode)), "application/json");
        }
        public ActionResult Donation_Register_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            var data = BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Accounts_DonationRegister);
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Accounts_DonationRegister)), "application/json");
        }
        public ActionResult Donation_Register_PrintsGridData_dx(string DonationID = "")
        {
           
            var DonationPrint_Data = BASE._DonationRegister_DBOps.GetDonationPrints();
            List<Return_DonationRegister_Prints> _DonationPrints = DonationPrint_Data;
            var sel_bal_info = _DonationPrints.Find(x => x.DR_TR_ID == DonationID);
            var ret_list = new List<Return_DonationRegister_Prints> { sel_bal_info };
            return Content(JsonConvert.SerializeObject(ret_list), "application/json");
        }

        public ActionResult Donation_Register_DispatchesGridData_dx(string DonationID = "")
        {
            var DonationDispatches_Data = BASE._DonationRegister_DBOps.GetDonationDispatches(DonationID);
            return Content(JsonConvert.SerializeObject(DonationDispatches_Data), "application/json");

        }
        public ActionResult Donation_Register_RejectionGridData_dx(string RecID = "")
        {
            List<Return_DonationRegister_Rejections> _DonationRejections = BASE._DonationRegister_DBOps.GetDonationRejections();
            var sel_bal_info = _DonationRejections.Find(x => x.DS_TR_ID == RecID);
            var ret_list = new List<Return_DonationRegister_Rejections> { sel_bal_info };
            return Content(JsonConvert.SerializeObject(ret_list), "application/json");
        }

        [CheckLogin]
        public ActionResult Frm_Export_Options_dx()
        {
            ViewBag.Filename = "DonationRegister_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_DonationRegister, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('DonRegister_report_modal','Not Allowed','No Rights');$('#DonRegisterModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        [CheckLogin]
        public ActionResult Frm_Export_Options_logs_dx()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_DonationRegister, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('DonLogs_report_modal','Not Allowed','No Rights');$('#DonLogsModelListPreview').hide();</script>");
            }
            return PartialView();
        }

        public void DonRegister_user_rights() {

            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

            ViewData["DonationReg_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_DonationRegister, "Export");
            ViewData["DonationReg_HelpAttachmentAddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["DonationReg_HelpAttachmentListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");

        }
        #endregion
    }
}