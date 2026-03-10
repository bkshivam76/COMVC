using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using ConnectOneMVC.Models;
using System;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using Common_Lib.RealTimeService;
using System.Data;
using Common_Lib;
using DevExtreme.AspNet.Mvc.Builders;
using DevExtreme.AspNet.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using ConnectOneMVC.Helper;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Web.Script.Serialization;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Areas.Help.Models;
using System.Web.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Collections.Specialized;

namespace ConnectOneMVC.Controllers
{

    public class BaseController : Controller
    {
        public Common_Lib.Common BASE;
        public string SessionGUID = "";
        public string Voucher_Entry = "Voucher Entry";

        [ValidateInput(false), AllowAnonymous]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            CultureInfo newCulture = new CultureInfo("", true);
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy HH:mm:ss";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;


            SessionGUID = Request.QueryString["SessionGUID"];
            if (!string.IsNullOrEmpty(SessionGUID))
            {
                List<BaseModel> ListOfBase = new List<BaseModel>();

                if (HttpContext.Session["BASEClass"] == null)
                {
                    BASE = new Common_Lib.Common();
                    BASE.Get_Configure_Setting();
                }
                else
                {
                    try
                    {
                        ListOfBase = HttpContext.Session["BASEClass"] as List<BaseModel>;
                        var basedata = ListOfBase.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                        if (basedata == null)
                        {
                            //HttpContext.Session["BASEClass"] = null; //check this why setting null there can be multiple logins..setting this null will remove all logged in session
                            BASE = new Common_Lib.Common();
                            BASE.Get_Configure_Setting();
                        }
                        else
                        {
                            BASE = basedata.BASE;
                        }
                    }
                    catch (Exception e)
                    {
                        BASE = new Common_Lib.Common();
                        BASE.Get_Configure_Setting();
                    }
                }
            }
            else
            {
                BASE = new Common_Lib.Common();
                BASE.Get_Configure_Setting();
            }
            ViewBag.Base_Date_Format_Current = BASE._Date_Format_Current;
            System.Web.HttpContext.Current.Session["Open_Year_Sdt_Year"] = BASE._open_Year_Sdt.Year;
            System.Web.HttpContext.Current.Session["Open_Year_Edt_Year"] = BASE._open_Year_Edt.Year;
        }
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (HttpContext.Session["BASEClass"] != null)
            {
                var ListBASE = HttpContext.Session["BASEClass"] as List<BaseModel>;
                try
                {
                    if (!string.IsNullOrWhiteSpace(SessionGUID))
                    {
                        var CurrentSession = ListBASE.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                        if (CurrentSession != null)
                            CurrentSession.BASE = BASE;
                    }
                    HttpContext.Session["BASEClass"] = ListBASE;
                }
                catch (Exception e)
                {
                }
            }
        }
        public bool CheckRights(Common _base, ClientScreen screenName, string RightsReqd)
        {
            //Superuser/Admin/Auditor get all rights on screens in accounts Modules(including masters)
            if (((_base._open_User_Type.ToUpper() == Common.ClientUserType.SuperUser.ToUpper())
                          || (_base._open_User_Type.ToUpper() == Common.ClientUserType.Auditor.ToUpper()) || (_base._open_User_User_Is_Admin))
                          && (GetScreenModuleName(screenName).ToLower() == "accounts" || GetScreenModuleName(screenName).ToLower() == "magazine" || GetScreenModuleName(screenName).ToLower() == "membership"))
            {
                return true;
            }
            //All Other users & Screens Admins checked for Rights 
            else
            {
                return CheckRightsByAllocation(_base, screenName, RightsReqd);
            }
        }
        public bool CheckRightsByAllocation(Common _base, ClientScreen screenName, string RightsReqd)
        {
            if (GetRightSession(screenName.ToString() + "_" + RightsReqd) == true)
            {
                return true;
            }
            if (GetRightSession(screenName.ToString() + "_Full") == true)
            {
                return true;
            }
            return false;
        }
        public string GetScreenModuleName(ClientScreen screenName)
        {
            switch (screenName)
            {
                case ClientScreen.Profile_Magazine:
                case ClientScreen.Magazine_Receipt_Register:
                case ClientScreen.Membership_Receipt_Register:
                case ClientScreen.Report_MagSubDispatch:
                case ClientScreen.Magazine_Membership:
                case ClientScreen.Magazine_SubCity:
                case ClientScreen.Magazine_Dispatch_Register:
                case ClientScreen.Magazine_Dispatch_Type_Master:
                    return "Magazine";
                case ClientScreen.Profile_Stock:
                case ClientScreen.Stock_Sub_Item:
                case ClientScreen.Stock_Project:
                case ClientScreen.Stock_Job:
                case ClientScreen.Stock_UO:
                case ClientScreen.Stock_RR:
                case ClientScreen.Stock_PO:
                case ClientScreen.Stock_Production:
                case ClientScreen.Stock_Machine_Tool_Issue:
                    return "Stock";
                case ClientScreen.Accounts_Voucher_Membership:
                case ClientScreen.Accounts_Voucher_Membership_Renewal:
                case ClientScreen.Accounts_Voucher_Membership_Conversion:
                    return "Membership";

                case ClientScreen.Start_Login:
                case ClientScreen.Start_Auditor_Login:
                case ClientScreen.Start_Auditor_Submit_Report:
                case ClientScreen.Start_Auditor_Audit_Verification:
                case ClientScreen.Start_Auditor_Notify:
                case ClientScreen.Profile_Telephone:
                case ClientScreen.Profile_LiveStock:
                case ClientScreen.Profile_Vehicles:
                case ClientScreen.Profile_GoldSilver:
                case ClientScreen.Profile_StockOfConsumables:
                case ClientScreen.Profile_Assets:
                case ClientScreen.Profile_Advances:
                case ClientScreen.Profile_BankAccounts:
                case ClientScreen.Profile_Cash:
                case ClientScreen.Profile_Core:
                case ClientScreen.Profile_Deposit:
                case ClientScreen.Profile_Deposit_Slips:
                case ClientScreen.Profile_Liabilities:
                case ClientScreen.Profile_ServicePlaces:
                case ClientScreen.Profile_FD:
                case ClientScreen.Profile_LandAndBuilding:
                case ClientScreen.Profile_ServicableSouls:
                case ClientScreen.Profile_Students:
                case ClientScreen.Profile_Report:
                case ClientScreen.Profile_Membership:
                case ClientScreen.Profile_OpeningBalances:
                case ClientScreen.Profile_Insurance_ChangeDetail:
                case ClientScreen.Profile_Insurance_ChangeValue:
                case ClientScreen.Profile_ChangeLocation:
                case ClientScreen.Profile_Complexes:
                case ClientScreen.Profile_WIP:
                case ClientScreen.Facility_AddressBook:
                case ClientScreen.Facility_Letter:
                case ClientScreen.Facility_Notes:
                case ClientScreen.Facility_ServiceReport:
                case ClientScreen.Facility_ServiceProject:
                case ClientScreen.Facility_Reminder:
                case ClientScreen.Facility_Magazine_Request:
                case ClientScreen.Facility_Center_Purpose:
                case ClientScreen.Facility_GodlyServiceMaterial:
                case ClientScreen.Facility_ChartInfo:
                case ClientScreen.Facility_ChartResponsesInfo:
                case ClientScreen.Facility_Accommodation_Register:
                case ClientScreen.Options_ChangePassword:
                case ClientScreen.Options_ResetPassword:
                case ClientScreen.Options_Maintenance:
                case ClientScreen.Options_DocLibrary:
                case ClientScreen.Options_SyncData:
                case ClientScreen.Options_GroupMaster:
                case ClientScreen.Options_UserMaster:
                case ClientScreen.Options_UserRegister:
                case ClientScreen.Options_Manage_Privileges:
                case ClientScreen.Options_createForm:
                case ClientScreen.Options_FormResponse:
                case ClientScreen.Stock_Personnel_Master:
                case ClientScreen.Stock_Dept_Store_Master:
                case ClientScreen.Stock_Supplier_Master:
                case ClientScreen.Accounts_Vouchers:
                case ClientScreen.Accounts_DonationRegister:
                case ClientScreen.Accounts_DraftEntryRegister:
                case ClientScreen.Account_TDS_Register:
                case ClientScreen.Accounts_Notebook:
                case ClientScreen.Accounts_Voucher_CollectionBox:
                case ClientScreen.Accounts_Voucher_BankToBank:
                case ClientScreen.Accounts_Voucher_CashBank:
                case ClientScreen.Accounts_Voucher_Donation:
                case ClientScreen.Accounts_Voucher_Gift:
                case ClientScreen.Accounts_Voucher_Receipt:
                case ClientScreen.Accounts_Voucher_Payment:
                case ClientScreen.Accounts_Voucher_FD:
                case ClientScreen.Accounts_Voucher_Property:
                case ClientScreen.Accounts_Voucher_Internal_Transfer:
                case ClientScreen.Accounts_Voucher_SaleOfAsset:
                case ClientScreen.Accounts_Voucher_Journal:
                case ClientScreen.Accounts_Voucher_AssetTransfer:
                case ClientScreen.Accounts_Voucher_WIP_Finalization:
                case ClientScreen.Account_CashbookAuditor:
                case ClientScreen.Accounts_CashBook:
                case ClientScreen.Report_Items:
                case ClientScreen.Report_Items_Documents:
                case ClientScreen.Report_Landscape:
                case ClientScreen.Report_Potrait:
                case ClientScreen.Report_Transaction_Summary:
                case ClientScreen.Report_Construction_Statement:
                case ClientScreen.Report_Collection_Box:
                case ClientScreen.Report_Transaction_Statement:
                case ClientScreen.Report_Collection_Box_Voucher:
                case ClientScreen.Report_Payment_Voucher:
                case ClientScreen.Report_Gift:
                case ClientScreen.Report_PartyListing:
                case ClientScreen.Report_PartyReport:
                case ClientScreen.Report_ItemReport:
                case ClientScreen.Report_LedgerReport:
                case ClientScreen.Report_SecondaryGroupReport:
                case ClientScreen.Report_PrimaryGroupReport:
                case ClientScreen.Report_FDReport:
                case ClientScreen.Report_Vehicles:
                case ClientScreen.Report_Assets:
                case ClientScreen.Report_GS:
                case ClientScreen.Report_LB:
                case ClientScreen.Report_Purpose:
                case ClientScreen.Report_Potamail:
                case ClientScreen.Report_InsuranceLetter:
                case ClientScreen.Report_AssetInsurance_Breakup:
                case ClientScreen.Report_ConsumableValue_Breakup:
                case ClientScreen.Report_WIPInfo:
                case ClientScreen.Report_Construction_List:
                case ClientScreen.Report_Asset_Movement_Logs:
                case ClientScreen.Report_Daily_Balances:
                case ClientScreen.Report_Deposit_Slips:
                case ClientScreen.Report_Bank_Account_List:
                case ClientScreen.Report_Income_Expenditure:
                case ClientScreen.Report_VoucherReference:
                case ClientScreen.Help_News:
                case ClientScreen.Help_Request_Box:
                case ClientScreen.Help_Action_Items:
                case ClientScreen.Help_Attachments:
                case ClientScreen.Help_Scheduler:
                case ClientScreen.Home_Murli:
                case ClientScreen.Home_StartUp:
                case ClientScreen.Options_ClientUser:
                case ClientScreen.Global_Set:
                case ClientScreen.Core_Add_AssetLocation:
                case ClientScreen.CommonFunctions:
                case ClientScreen.Profile_Existing_Mag_member:
                case ClientScreen.Account_InternalTrf_Register:
                case ClientScreen.Report_Location:
                case ClientScreen.Report_BS:
                case ClientScreen.Report_TB:
                case ClientScreen.Report_Utilization:
                case ClientScreen.Statement_Main:
                case ClientScreen.Statement_CashBook:
                case ClientScreen.Statement_Potamel:
                case ClientScreen.Statement_CollectionBox:
                case ClientScreen.Statement_Donation:
                case ClientScreen.Statement_Property:
                case ClientScreen.Statement_Movable:
                case ClientScreen.Statement_Vehicle:
                case ClientScreen.Statement_GS:
                case ClientScreen.Statement_FD:
                case ClientScreen.Statement_Telaphonebill:
                case ClientScreen.Account_Document_Library:
                case ClientScreen.Service_Module:
                case ClientScreen.Report_AddressBook_Search:
                case ClientScreen.Report_User_Rights:
                //case ClientScreen.Account_CashbookAuditor:
                case ClientScreen.Auditor_DataRestriction:
                case ClientScreen.Report_SpecialVoucherReference:
                case ClientScreen.Start_BankChecking:
                    // case ClientScreen.Report_TDS_Applicability:
                case ClientScreen.Report_MultiBank_CashBook:
                    return "Accounts";
                default:
                    return "No Defined";
            }
        }
        public object GetBaseSession(string Key)
        {
            if (BASE._SessionDictionary.TryGetValue(Key, out object value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
        public void SetBaseSession(string Key, object value)
        {
            if (BASE._SessionDictionary.ContainsKey(Key))
            {
                BASE._SessionDictionary[Key] = value;
            }
            else
            {
                BASE._SessionDictionary.Add(Key, value);
            }
        }
        public void ClearBaseSession(string EndWith)
        {
            foreach (var item in BASE._SessionDictionary.Where(x => x.Key.EndsWith(EndWith)).ToList())
            {
                BASE._SessionDictionary.Remove(item.Key);
            }
        }
        public bool GetRightSession(string Key)
        {
            Key = Key.Trim().ToUpper();
            if (BASE._RightsDictionary.TryGetValue(Key, out bool value))
            {
                return value;
            }
            else
            {
                return false;
            }
        }
        public void SetRightSession(string Key, bool value)
        {
            Key = Key.Trim().ToUpper();
            if (BASE._RightsDictionary.ContainsKey(Key))
            {
                BASE._RightsDictionary[Key] = value;
            }
            else
            {
                BASE._RightsDictionary.Add(Key, value);
            }
        }
        public void Check_Defaulf_Cash_Account()
        {
            string Cash_Acc_ID = BASE._open_Ins_ID + "-" + BASE._open_Cen_ID.ToString() + "-" + BASE._open_Year_ID.ToString() + "-CASH-A/C-OP-BALANCE";
            object MaxValue = 0;
            MaxValue = BASE._CashDBOps.CheckCashOpeningBalanceRowCount(Cash_Acc_ID);
            if ((int)MaxValue <= 0)
            {
                BASE._CashDBOps.AddDefault(Cash_Acc_ID);
            }
        }
        public ActionResult SendSms(string SMSKey, string MobileNoInput, string GenericID, string cenid = null)
        {
            string ResultString = "";
            BASE._Notifications_DBOps.SendSMS(null, SMSKey, ref ResultString, GenericID, cenid, MobileNoInput);
            return Json(new { ResultString }, JsonRequestBehavior.AllowGet);
        }
        //Excel Upload Functions
        public ActionResult ImportTableAsExcelFile(string table_name)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            DataSet columnsAndFks_ds = BASE._Form_dbops.get_ColumnNamesOfTable(table_name);
            DataTable columnNames_dt = columnsAndFks_ds.Tables[0];
            DataTable ForeignKeyColumns_dt = columnsAndFks_ds.Tables[1];
            DataTable SkipCols_dt = columnsAndFks_ds.Tables[2];

            //string filepath = @"C:\Users\admin\Downloads\sampleTemplate.xlsx";
            string filepath = Server.MapPath("~/App_Data/TempExcelFiles/sampleTemplate.xlsx");
            FileInfo file = new FileInfo(Server.MapPath("~/App_Data/TempExcelFiles/sampleTemplate.xlsx"));
            if (file.Exists)
            {
                file.Delete();
            }
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Sample Data"
            };

            sheets.Append(sheet);

            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();

            Row r = new Row();
            foreach (DataRow itm in columnNames_dt.Rows)
            {
                string ClName = itm[0].ToString();
                bool Fk_contains = ForeignKeyColumns_dt.AsEnumerable().Any(row => itm[0].ToString() == row.Field<String>("Referencing_Column"));
                bool Skip_contains = SkipCols_dt.AsEnumerable().Any(row => itm[0].ToString() == row.Field<String>("ColNames"));
                if (Skip_contains)
                {

                }
                else if (Fk_contains)//This if condition is to change the column name for the foreign key columns
                {
                    switch (table_name.ToUpper())
                    {
                        case "ACTION_ITEM_INFO":
                        case "ADDRESS_BOOK":
                            ClName = ForeignKeyColumns_dt.AsEnumerable().Where(x => x.Field<String>("Referencing_Column") == ClName)
                                .Select(x => x.Field<String>("Custom_Column")).First<string>();
                            break;
                    }
                    //breks:
                    Cell c = new Cell()
                    {
                        CellValue = new CellValue(ClName),
                        DataType = CellValues.String
                    };
                    r.Append(c);
                }
                else
                {
                    Cell c = new Cell()
                    {
                        CellValue = new CellValue(ClName),
                        DataType = CellValues.String
                    };
                    r.Append(c);
                }
            }
            sheetData.Append(r);

            worksheetPart.Worksheet.Save();
            spreadsheetDocument.Close();
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            return File(bytes, "Application/x-msexcel", "sampleTemplate.xlsx");

        }

        [HttpPost]
        public ActionResult ReadUploadedExcel(ExcelUploadVariables model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string file_Path = "";
            string table_name = model.table_name;
            string file_name = "";
            var targetLocation = Server.MapPath("~/App_Data/TempExcelFiles/");
            if (table_name.ToUpper() == "ADDRESS_BOOK")
            {
                file_name = model.ExcelUpload_Address.FileName;
                try
                {
                    file_Path = Path.Combine(targetLocation, model.ExcelUpload_Address.FileName);
                    model.ExcelUpload_Address.SaveAs(file_Path);
                }
                catch { Response.StatusCode = 400; }
            }
            else if (table_name.ToUpper() == "ACTION_ITEM_INFO")
            {
                file_name = model.ExcelUpload_Action_items.FileName;
                try
                {
                    file_Path = Path.Combine(targetLocation, model.ExcelUpload_Action_items.FileName);
                    model.ExcelUpload_Action_items.SaveAs(file_Path);
                }
                catch { Response.StatusCode = 400; }
            }
            DataSet columnNamesDS = BASE._Form_dbops.get_ColumnNamesOfTable(table_name);
            DataTable columnNamesDt = columnNamesDS.Tables[0];
            DataTable ForeignKeyColumns_dt = columnNamesDS.Tables[1];
            DataTable SkipCols_dt = columnNamesDS.Tables[2];
            string[] colnames = new[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
                        "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV",
                        "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS",
                        "BT", "BU", "BV", "BW", "BX", "BY", "BZ", "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP",
                        "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ", "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM",
                        "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ", "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ",
                        "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ", "FA", "FB", "FC", "FD", "FE", "FF", "FG",
                        "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ", "GA", "GB", "GC", "GD",
                        "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ", "HA",
                        "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ", "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV", "HW", "HX",
                        "HY", "HZ", "IA", "IB", "IC", "ID", "IE", "IF", "IG", "IH", "II", "IJ", "IK", "IL", "IM", "IN", "IO", "IP", "IQ", "IR", "IS", "IT", "IU",
                        "IV", "IW", "IX", "IY", "IZ", "JA", "JB", "JC", "JD", "JE", "JF", "JG", "JH", "JI", "JJ", "JK", "JL", "JM", "JN", "JO", "JP", "JQ", "JR",
                        "JS", "JT", "JU", "JV", "JW", "JX", "JY", "JZ", "KA", "KB", "KC", "KD", "KE", "KF", "KG", "KH", "KI", "KJ", "KK", "KL", "KM", "KN", "KO",
                        "KP", "KQ", "KR", "KS", "KT", "KU", "KV", "KW", "KX", "KY", "KZ", "LA", "LB", "LC", "LD", "LE", "LF", "LG", "LH", "LI", "LJ", "LK", "LL",
                        "LM", "LN", "LO", "LP", "LQ", "LR", "LS", "LT", "LU", "LV", "LW", "LX", "LY", "LZ", "MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI",
                        "MJ", "MK", "ML"};
            string FailedRowsMessage = "";
            string ReturnMessage = "";
            string allColumnNamesStr = "";
            string allValuesStr = "";
            using (FileStream fs = new FileStream(file_Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    Worksheet sheet = worksheetPart.Worksheet;

                    var cells = sheet.Descendants<Cell>();
                    var rows = sheet.Descendants<Row>();
                    //var cols = sheet.Descendants<Columns>();
                    int colsCount = 0;
                    //Console.WriteLine("Row count = {0}", rows.LongCount());
                    //Console.WriteLine("Cell count = {0}", cells.LongCount());
                    DataTable dt = new DataTable();
                    var recid = "";

                    Int32 RowNumber = 0;
                    //Creating the insert query for the mandatory columns data. (These columns are defined in the 3rd table of the sp)
                    if (table_name.ToUpper() == "ADDRESS_BOOK")
                    {
                        allColumnNamesStr = "INSERT INTO " + table_name + " (C_CEN_ID, REC_ADD_ON, REC_ADD_BY, REC_EDIT_ON, REC_EDIT_BY, REC_STATUS, " +
                        "REC_STATUS_ON, REC_STATUS_BY, REC_ID";
                    }
                    else if (table_name.ToUpper() == "ACTION_ITEM_INFO")
                    {
                        allColumnNamesStr = "INSERT INTO " + table_name + " (AI_CEN_ID, REC_ADD_ON, REC_ADD_BY, REC_EDIT_ON, REC_EDIT_BY, REC_STATUS, " +
                        "REC_STATUS_ON, REC_STATUS_BY, REC_ID";
                    }

                    foreach (Row rw in rows)
                    {
                        RowNumber = RowNumber + 1;
                        if (RowNumber == 1) //This is to check for the column headings
                        {
                            colsCount = rw.Elements<Cell>().Count();
                            foreach (Cell cel in rw.Elements<Cell>())
                            {
                                int ssid = int.Parse(cel.CellValue.Text);
                                string str = sst.ChildElements[ssid].InnerText;
                                if (cel != null)
                                {
                                    bool contains = columnNamesDt.AsEnumerable().Any(row => str == row.Field<String>("COLUMN_NAME"));
                                    //string fk_col_name = str.Replace("_NAME", "_ID");
                                    string fk_col_name = str;
                                    bool contains_fk = ForeignKeyColumns_dt.AsEnumerable().Any(row => fk_col_name == row.Field<String>("Custom_Column"));
                                    if (contains == true && contains_fk == false)
                                    {
                                        dt.Columns.Add(str);
                                        allColumnNamesStr += ", " + str;
                                    }
                                    else if (contains == false && contains_fk == true)
                                    { // This is to add columns with custom columns replacing original column for foreign key column
                                        var temprw = ForeignKeyColumns_dt.AsEnumerable().Where(x => x.Field<String>("Custom_Column") == str);
                                        foreach (var e in temprw)
                                        {
                                            if (e.Field<String>("Custom_Column").ToString() == str)
                                            {
                                                fk_col_name = e.Field<String>("Referencing_Column").ToString();
                                                goto breks;
                                            }
                                        }
                                        breks:
                                        dt.Columns.Add(fk_col_name);
                                        allColumnNamesStr += ", " + fk_col_name;
                                    }
                                    else
                                    {
                                        jsonParam.message = "Uploaded File has Invalid Column Names...";
                                        jsonParam.title = "Excel File..";
                                        jsonParam.result = false;
                                        jsonParam.popup_form_path = file_Path;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            allColumnNamesStr = allColumnNamesStr + ")";
                        }
                        else if (RowNumber > 1)// preparing values for the insert query
                                               //And looping from the 2nd row onwards.
                        {
                            Int32 c = 0;
                            recid = Guid.NewGuid().ToString();
                            allValuesStr = "";
                            allValuesStr = "(" + BASE._open_Cen_ID + ", '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "', " + BASE._open_User_ID + ",  null, null, 1, '" +
                                DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "', " + BASE._open_User_ID + ", '" + recid + "'";
                            for (int clm = 1; clm <= colsCount; clm++)
                            {
                                //int ssid = int.Parse(cel.CellValue.Text);
                                //string str = sst.ChildElements[ssid].InnerText;
                                string cellAddress = colnames[clm - 1] + RowNumber;
                                string cellColNameAddress = colnames[clm - 1] + "1";
                                string shtname = "Sample Data";
                                var celValue = GetCellValue(file_Path, shtname, cellAddress); //D2, E2
                                var colName = GetCellValue(file_Path, shtname, cellColNameAddress);
                                bool contains = columnNamesDt.AsEnumerable().Any(row => colName == row.Field<String>("COLUMN_NAME"));

                                //string fk_col_name = colName.Replace("_NAME", "_ID");
                                //bool contains_fk = ForeignKeyColumns_dt.AsEnumerable().Any(row => fk_col_name == row.Field<String>("Referencing_Column"));
                                string fk_col_name = colName;
                                bool contains_fk = ForeignKeyColumns_dt.AsEnumerable().Any(row => fk_col_name == row.Field<String>("Custom_Column"));
                                if (contains == true && contains_fk == false)
                                {
                                    var data_type = columnNamesDt.AsEnumerable().Where(row => row.Field<String>("COLUMN_NAME") == colName)
                                                .Select(row => row.Field<string>("DATA_TYPE")).First<string>();

                                    if (celValue != null && celValue != "" && celValue != " ")
                                    {
                                        if ((data_type == "varchar" || data_type == "nvarchar" || data_type == "image"))
                                        {
                                            allValuesStr += ", '" + celValue + "'";
                                        }
                                        else if (data_type == "datetime2")
                                        {
                                            allValuesStr += ", '" + DateTime.FromOADate(Convert.ToInt32(celValue)).ToString("yyyy-MM-dd") + "'";
                                        }
                                        else if ((data_type == "int" || data_type == "bit"))
                                        {
                                            allValuesStr += ", " + celValue;
                                        }
                                    }
                                    else if (celValue == null || celValue == "" || celValue == " ")
                                    {
                                        allValuesStr += ", null";
                                    }
                                }
                                else if (contains == false && contains_fk == true)
                                {
                                    //string ReplacedWithIDcolumnName = colName.Replace("_NAME", "_ID");
                                    string ReplacedWithIDcolumnName = "";
                                    ReplacedWithIDcolumnName = ForeignKeyColumns_dt.AsEnumerable().Where(x => x.Field<String>("Custom_Column") == colName)
                                        .Select(x => x.Field<String>("Referencing_Column")).First<string>();

                                    var data_type = columnNamesDt.AsEnumerable().Where(row => row.Field<String>("COLUMN_NAME") == ReplacedWithIDcolumnName)
                                                .Select(row => row.Field<string>("DATA_TYPE")).First<string>();

                                    if (celValue != null && celValue != "" && celValue != " ")
                                    {
                                        foreach (DataRow fkrw in ForeignKeyColumns_dt.Rows)
                                        {
                                            if (fkrw["Referencing_Column"].ToString() == ReplacedWithIDcolumnName)
                                            {
                                                string ref_table = fkrw["Referenced_Table"].ToString();
                                                string ref_col = fkrw["Referenced_Column"].ToString();
                                                DataTable cntRecid_Dt = BASE._Form_dbops.GetidFromFk(table_name, celValue, ReplacedWithIDcolumnName, ref_table, ref_col);
                                                int cnt = Convert.ToInt32(cntRecid_Dt.Rows[0]["cnt"].ToString());
                                                if (cnt == 0)
                                                {
                                                    if (FailedRowsMessage != "")
                                                    {
                                                        FailedRowsMessage = FailedRowsMessage + ", " + RowNumber.ToString();
                                                    }
                                                    else
                                                    {
                                                        FailedRowsMessage = RowNumber.ToString();
                                                    }
                                                    goto nxtRow;
                                                }
                                                else
                                                {
                                                    if ((data_type == "varchar" || data_type == "nvarchar" || data_type == "image"))
                                                    {
                                                        allValuesStr += ", '" + cntRecid_Dt.Rows[0][1].ToString() + "'";
                                                    }
                                                    else if (data_type == "datetime2")
                                                    {
                                                        allValuesStr += ", '" + DateTime.FromOADate(Convert.ToInt32(cntRecid_Dt.Rows[0][1].ToString()))
                                                            .ToString("yyyy-MM-dd") + "'";
                                                    }
                                                    else if ((data_type == "int" || data_type == "bit"))
                                                    {
                                                        allValuesStr += ", " + cntRecid_Dt.Rows[0][1];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (celValue == null || celValue == "" || celValue == " ")
                                    {
                                        allValuesStr += ", null";
                                    }
                                }
                                c += 1;
                            }

                            allValuesStr = allValuesStr + ")";
                            string query = allColumnNamesStr + " VALUES " + allValuesStr;
                            //Here we need to write the code to send the above query to insert the values.
                            Parameter_InsertQuery_ExcelRawDataUpload InParams = new Parameter_InsertQuery_ExcelRawDataUpload();
                            InParams.query = query;
                            InParams.RecID = recid.ToString();
                            InParams.table_name = table_name;
                            Boolean result = BASE._Address_DBOps.Excel_Insert_Query(InParams);
                        } // End of elseif condition - To fill values in the query string.
                        nxtRow:;
                        //allColumnNamesStr = "";
                        //allValuesStr = "";
                    } //End of for loop going through each row

                }
            }

            if (FailedRowsMessage == "")
            {
                ReturnMessage = "Uploaded Successfully..";
            }
            else if (FailedRowsMessage.Length > 0)
            {
                ReturnMessage = "Excel file is Partially Uploaded.. Below Rows are not uploaded. Because the data is not correct in those rows.<br>" +
                    "Failed Row Numbers are: " + FailedRowsMessage;
                FileInfo file = new FileInfo(file_Path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            jsonParam.message = ReturnMessage;
            jsonParam.title = "Excel File..";
            jsonParam.result = true;
            //jsonParam.popup_form_path = file_Path;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }


        // Example GetCellValue(@"c:\test.xlsx", "Sheet1", "A1");
        // Retrieve the value of a cell, given a file name, sheet name, and address name.
        public static string GetCellValue(string fileName, string sheetName, string addressName)
        {
            string value = null;
            // Open the spreadsheet document for read-only access.
            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false))
            {
                // Retrieve a reference to the workbook part.
                WorkbookPart wbPart = document.WorkbookPart;

                // Find the sheet with the supplied name, and then use that 
                // Sheet object to retrieve a reference to the first worksheet.
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
                    Where(s => s.Name == sheetName).FirstOrDefault();

                // Throw an exception if there is no sheet.
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                // Retrieve a reference to the worksheet part.
                WorksheetPart wsPart =
                    (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

                // Use its Worksheet property to get a reference to the cell 
                // whose address matches the address you supplied.
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().
                    Where(c => c.CellReference == addressName).FirstOrDefault();

                // If the cell does not exist, return an empty string.
                if (theCell == null)
                {
                    value = null;
                }
                else if (theCell.InnerText.Length > 0)
                {
                    value = theCell.InnerText;

                    // If the cell represents an integer number, you are done. 
                    // For dates, this code returns the serialized value that 
                    // represents the date. The code handles strings and 
                    // Booleans individually. For shared strings, the code 
                    // looks up the corresponding value in the shared string 
                    // table. For Booleans, the code converts the value into 
                    // the words TRUE or FALSE.
                    if (theCell.DataType != null)
                    {
                        switch (theCell.DataType.Value)
                        {
                            case CellValues.SharedString:
                                // For shared strings, look up the value in the
                                // shared strings table.
                                var stringTable =
                                    wbPart.GetPartsOfType<SharedStringTablePart>()
                                    .FirstOrDefault();

                                // If the shared string table is missing, something 
                                // is wrong. Return the index that is in
                                // the cell. Otherwise, look up the correct text in 
                                // the table.
                                if (stringTable != null)
                                {
                                    value =
                                        stringTable.SharedStringTable
                                        .ElementAt(int.Parse(value)).InnerText;
                                }
                                break;

                            case CellValues.Boolean:
                                switch (value)
                                {
                                    case "0":
                                        value = "FALSE";
                                        break;
                                    default:
                                        value = "TRUE";
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return value;
        }
        public Boolean SendHTMLEmail(ref string Message, string ToEmailId, string Subject, string ContentPath = "~/Views/Base/ConnectoneEmailer.cshtml", object model = null, bool isPartial = false, string EmailerName = "", string CcEmailId = "", string BccEmailId = "", string ReplyToEmailId = "", string senderIDtoBeUsed = "", string attachment_filename = "")
        {
            //string html = RenderViewToString(ControllerContext, "~/Areas/Facility/Views/ServiceReport/MedicalWingEmailer.cshtml", null, true);
            string html = RenderViewToString(ControllerContext, ContentPath, model, isPartial);
            bool sent = BASE._Notifications_DBOps.SendEmail(ToEmailId, Subject, html, EmailerName, CcEmailId, BccEmailId, ReplyToEmailId, senderIDtoBeUsed, attachment_filename);
            System.Threading.Thread.Sleep(100);
            if (sent) { Message += ToEmailId + " :Email sent Successfully.<br/>"; return true; }
            else { Message += ToEmailId + " :Email sending failed.<br/>"; return false; }
        }

        public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data    
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;
            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
        public string GetAttachmentPath(string Rec_ID, string FileName, bool GuidAsFileName = false)
        {
            string AttachmentPath = WebConfigurationManager.AppSettings["attachmentpath"].ToString();
            return AttachmentPath + CommonFunctions.GetAttachment_DiskFileName(Rec_ID, FileName, GuidAsFileName) + "?" + DateTime.Now.Millisecond;
        }

        #region old_sendPushNotification
        //public void sendPushNotification(string deviceToken = "", string body_URL_To_Open = "11", string title = "CONNECTONE", string subtitle = "", string ImageURL = "")
        //{
        //    body_URL_To_Open = string.IsNullOrWhiteSpace(body_URL_To_Open) ? "11" : body_URL_To_Open;
        //    deviceToken = string.IsNullOrWhiteSpace(deviceToken) ? "" : deviceToken;
        //    ImageURL = string.IsNullOrWhiteSpace(ImageURL) ? "" : ImageURL;
        //    title = string.IsNullOrWhiteSpace(title) ? "CONNECTONE" : title;
        //    subtitle = string.IsNullOrWhiteSpace(subtitle) ? "" : subtitle;

        //    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //    tRequest.Method = "post";
        //    //serverKey - Key from Firebase cloud messaging server  
        //    tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA8tArSfU:APA91bFJM0kUqwbPjx8fh-gTo6S8hqxhThRf5NDwSShgyVsHU67Pdx7yG8DkmAVCv8rf2YzbmajjG7UPtzA3x9Y9yUWsf7Il-c5D87bJZGkuh57SIbb8L1R5N7GWzXxtJBQsGH2kdlwD"));
        //    //Sender Id - From firebase project setting  
        //    tRequest.Headers.Add(string.Format("Sender: id={0}", "1042874583541"));
        //    tRequest.ContentType = "application/json";
        //    var payload = new
        //    {
        //        to = deviceToken,
        //        priority = "high",
        //        content_available = true,
        //        notification = new
        //        {

        //        },
        //        data = new
        //        {
        //            body = body_URL_To_Open,
        //            title = title,
        //            subtitle = subtitle,
        //            imageURL = ImageURL
        //        }

        //    };

        //    string postbody = JsonConvert.SerializeObject(payload).ToString();
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
        //    tRequest.ContentLength = byteArray.Length;
        //    using (Stream dataStream = tRequest.GetRequestStream())
        //    {
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        using (WebResponse tResponse = tRequest.GetResponse())
        //        {
        //            using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //            {
        //                if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        //result.Response = sResponseFromServer;
        //                    }
        //            }
        //        }
        //    }

        //}
        #endregion 

        public Response_AndroidNotificationAPI sendPushNotification(string deviceToken = "", string body_URL_To_Open = "11", string title = "CONNECTONE", string subtitle = "", string ImageURL = "")
        {

            body_URL_To_Open = string.IsNullOrWhiteSpace(body_URL_To_Open) ? "" : body_URL_To_Open;
            deviceToken = string.IsNullOrWhiteSpace(deviceToken) ? "" : deviceToken;
            ImageURL = string.IsNullOrWhiteSpace(ImageURL) ? "" : ImageURL;
            title = string.IsNullOrWhiteSpace(title) ? "CONNECTONE" : title;
            subtitle = string.IsNullOrWhiteSpace(subtitle) ? "" : subtitle;

            int successCount = 0;
            int failureCount = 0;
            string remarks = "";

            Response_AndroidNotificationAPI response = new Response_AndroidNotificationAPI();

            try
            {
                System.Net.WebRequest tRequest = System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA8tArSfU:APA91bFJM0kUqwbPjx8fh-gTo6S8hqxhThRf5NDwSShgyVsHU67Pdx7yG8DkmAVCv8rf2YzbmajjG7UPtzA3x9Y9yUWsf7Il-c5D87bJZGkuh57SIbb8L1R5N7GWzXxtJBQsGH2kdlwD"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "1042874583541"));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = deviceToken,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        title = title,
                        body = subtitle,
                        image = ImageURL // Details: URL of Image which should come in notification.
                    },
                    data = new
                    {
                        subtitle = "", // Details: To be Kept Blank
                        imageURL = body_URL_To_Open //Details: URL which should open on click of Notification.
                    },
                    android = new
                    {
                        notification = new
                        {
                            imageUrl = ImageURL // Details: To be Kept Blank
                        }
                    }

                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String ResponseFromServer = tReader.ReadToEnd();

                                    var data = (JObject)JsonConvert.DeserializeObject(ResponseFromServer);
                                    successCount = Convert.ToInt32(data.SelectToken("success"));
                                    failureCount = Convert.ToInt32(data.SelectToken("failure"));
                                    remarks = failureCount == 1 ? data.SelectToken("results[0].error").ToString() : "NULL";

                                    response.status = successCount == 1 ? "SUCCESS" : "FAILURE";
                                    response.remarks = remarks;
                                    return response;
                                }
                        }
                    }
                }

                response.status = successCount == 1 ? "SUCCESS" : "FAILURE";
                response.remarks = remarks;
                return response;

            }
            catch (Exception e)
            {
                response.status = "UNKNOWN";
                response.remarks = e.Message;
                return response;
            }


        }

        public string TranslateText(string input, string FromLang = "en", string ToLang = "hi")
        {
            // Set the language from/to in the url (or pass it into this function)
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             FromLang, ToLang, Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;

            // Get all json data
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);

            // Extract just the first array element (This is the only data we are interested in)
            var translationItems = jsonData[0];

            // Translation Data
            string translation = "";

            // Loop through the collection extracting the translated objects
            foreach (object item in translationItems)
            {
                // Convert the item array to IEnumerable
                IEnumerable translationLineObject = item as IEnumerable;

                // Convert the IEnumerable translationLineObject to a IEnumerator
                IEnumerator translationLineString = translationLineObject.GetEnumerator();

                // Get first object in IEnumerator
                translationLineString.MoveNext();

                // Save its value (translated text)
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }

            // Remove first blank character
            if (translation.Length > 1) { translation = translation.Substring(1); };

            // Return translation
            return translation;
        }

        public ActionResult Attachment_Save(Model_Attachment_Window model, ClientScreen clientScreen = ClientScreen.Help_Attachments)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Help_uploadControlActionMethod) == false)
                {
                    model.ActionMethod = model.Help_uploadControlActionMethod;
                }
                model.Help_FileList = new List<Byte[]>();
                List<string> DocumentFileNameList = new List<string>();               
                if (string.IsNullOrWhiteSpace(model.Help_uploadControlName) == false)
                {
                    var AllFiles = Request.Files;
                    if (AllFiles.Count == 1)
                    {
                        if (model.Help_uploadMethod == "fileCollection")
                        {
                            HttpFileCollectionBase myFile = Request.Files;
                            HttpPostedFileBase File = myFile[0];
                            if (File.ContentLength > 0)
                            {
                                BinaryReader reader = new BinaryReader(File.InputStream);
                                byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                model.Help_filefield = imageBytes;
                                model.Help_FileList.Add(imageBytes);
                                DocumentFileNameList.Add(File.FileName);
                                imageBytes = null;
                                myFile = null;
                                reader.Close();
                                reader.Dispose();
                            }
                        }
                        else
                        {
                            HttpPostedFileBase myFile = Request.Files[model.Help_uploadControlName];
                            if (myFile.ContentLength > 0)
                            {
                                BinaryReader reader = new BinaryReader(myFile.InputStream);
                                byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);
                                model.Help_filefield = imageBytes;
                                model.Help_FileList.Add(imageBytes);
                                DocumentFileNameList.Add(myFile.FileName);
                                imageBytes = null;
                                myFile = null;
                                reader.Close();
                                reader.Dispose();
                            }
                        }

                    }
                    else
                    {
                        if (model.Help_uploadMethod == "fileCollection")
                        {
                            HttpFileCollectionBase myFile = Request.Files;//.GetMultiple(model.Help_uploadControlName); //This is working for file collection.
                            for (int i = 0; i < myFile.Count; i++)
                            {
                                HttpPostedFileBase File = myFile[i];
                                if (File.ContentLength > 0)
                                {
                                    BinaryReader reader = new BinaryReader(File.InputStream);
                                    byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                    model.Help_FileList.Add(imageBytes);
                                    DocumentFileNameList.Add(File.FileName);
                                    imageBytes = null;
                                    File = null;
                                    reader.Close();
                                    reader.Dispose();
                                }
                            }
                            myFile = null;
                        }
                        else
                        {
                            IList<HttpPostedFileBase> myFile = Request.Files.GetMultiple(model.Help_uploadControlName);
                            for (int i = 0; i < myFile.Count; i++)
                            {
                                HttpPostedFileBase File = myFile[i];
                                if (File.ContentLength > 0)
                                {
                                    BinaryReader reader = new BinaryReader(File.InputStream);
                                    byte[] imageBytes = reader.ReadBytes((int)File.ContentLength);
                                    model.Help_FileList.Add(imageBytes);
                                    DocumentFileNameList.Add(File.FileName);
                                    imageBytes = null;
                                    File = null;
                                    reader.Close();
                                    reader.Dispose();
                                }
                            }
                            myFile = null;
                        }
                    }
                    AllFiles = null;
                }
                else
                {
                    object FileField;
                    object DocumentFileName;
                    FileField = GetBaseSession(model.Help_Byte_SessionName + "_HelpAttachment");
                    DocumentFileName = GetBaseSession(model.Help_Byte_SessionName + "_DocumentFileName" + "_HelpAttachment");
                    if (FileField != null)
                    {
                        if (FileField.GetType() == typeof(Byte[]))
                        {
                            model.Help_filefield = FileField as byte[];
                        }
                        else
                        {
                            model.Help_FileList = FileField as List<byte[]>;
                        }
                        if (model.Help_FileList == null || model.Help_FileList.Count == 0)
                        {
                            model.Help_FileList.Add(model.Help_filefield);
                        }
                    }

                    if (DocumentFileName != null)
                    {
                        if (DocumentFileName.GetType() == typeof(String))
                        {
                            model.Help_Document_FileName = (string)DocumentFileName;
                        }
                        else
                        {
                            DocumentFileNameList = DocumentFileName as List<string>;
                        }
                        if (DocumentFileNameList == null || DocumentFileNameList.Count == 0)
                        {
                            DocumentFileNameList.Add(model.Help_Document_FileName);
                        }
                    }
                }
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    for (int i = 0; i < DocumentFileNameList.Count; i++)
                    {
                        DocumentFileNameList[i] = CommonFunctions.TransformFileName(DocumentFileNameList[i], model.Help_FileList.Count > 0 ? model.Help_FileList[i] : model.Help_filefield);
                        var regex = new Regex(@"^[A-Za-z0-9. _]{1,255}$");
                        if (!regex.IsMatch(DocumentFileNameList[i]))
                        {
                            return Json(new
                            {
                                result = false,
                                Message = "Only alphaNumeric . _ is allowed in File Name<br> Kindly Rename the File: " + DocumentFileNameList[i]
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    model.Help_Document_FileName = DocumentFileNameList.Count == 0 ? model.Help_Document_FileName : DocumentFileNameList[0];
                    //if (model.Help_Document_FileName.Split('.').Length <= 1)
                    //{
                    //    return Json(new
                    //    {
                    //        result = false,
                    //        Message = "File Without Extension Not Allowed...!"
                    //    }, JsonRequestBehavior.AllowGet);
                    //}

                    if (string.IsNullOrEmpty(model.Help_Document_NameID))
                    {
                        return Json(new
                        {
                            result = false,
                            Message = "Document Name Is Not Selected..!!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Help_ParamMandatory == true)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Help_FromDate_Label))
                        {
                            if (IsDate(model.Help_Doc_From_Date) == false)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_FromDate_Label + " Is Blank Or Invalid..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.Help_ToDate_Label))
                        {
                            if (IsDate(model.Help_Doc_To_Date) == false)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_ToDate_Label + " Is Blank Or Invalid..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(model.Help_Description_Label))
                        {
                            if (string.IsNullOrWhiteSpace(model.Help_Document_Description))
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = model.Help_Description_Label + " Is Not Specefied..!!"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(model.Help_Document_Description))
                    {
                        model.Help_Document_Description = model.Help_Document_Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                    }
                    if (model.ActionMethod == "New")
                    {
                        if (model.Help_File_caption != null && model.Help_File_caption.Count > 0)
                        {
                            if (model.Help_FileList.Count != model.Help_File_caption.Count)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = "Caption And File Count Different, Please Attach Again"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (model.Help_File_Ratings != null && model.Help_File_Ratings.Count > 0)
                        {
                            if (model.Help_FileList.Count != model.Help_File_Ratings.Count)
                            {
                                return Json(new
                                {
                                    result = false,
                                    Message = "Rating And File Count Different, Please Attach Again"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                if (model.Help_Post_Area_Name == "Help" && model.Help_Post_Controller_Name == "Attachment")
                {
                    if (model.ActionMethod == "New")
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.NameID = model.Help_Document_NameID;
                        InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                        InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                        InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                        InEInfo.Ref_Screen = model.Help_REF_SCREEN;
                        InEInfo.Checked = model.Help_Checked;
                        InEInfo.Vouching_Category = CommonFunctions.GetVouchingCategoryForCallingScreen(model.Help_REF_SCREEN ?? "");
                        var AttachmentID = "";
                        string msg = "";
                        string FileList = "";
                        string captionsList = "";
                        string ratingsList = "";
                        string AttachmentIdsList = "";                   
                        for (int i = 0; i < model.Help_FileList.Count; i++)
                        {
                            InEInfo.Description = model.Help_Document_Description == null ? "" : model.Help_Document_Description;                         
                            string ErrMsg = PreventFileUpload(DocumentFileNameList[i]);
                            if (string.IsNullOrWhiteSpace(ErrMsg) == false) 
                            {
                                msg = msg + ErrMsg;
                            }                 
                            else
                            {
                                InEInfo.RecID = System.Guid.NewGuid().ToString();
                                AttachmentID = InEInfo.RecID;
                                InEInfo.File = model.Help_FileList[i];
                                InEInfo.FileName = DocumentFileNameList[i];
                                if (model.Help_File_caption != null && model.Help_File_caption.Count > 0 && string.IsNullOrWhiteSpace(model.Help_File_caption[i]) == false)
                                {
                                    InEInfo.Description = model.Help_File_caption[i].Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                                }
                                if ((model.Help_File_Ratings != null && model.Help_File_Ratings.Count > 0))
                                {
                                    InEInfo.AI_CEN_RATING = model.Help_File_Ratings[i];
                                }
                                var AttachmentSrNo = BASE._Attachments_DBOps.Insert(InEInfo, clientScreen);
                                if (AttachmentSrNo.Length > 0)
                                {
                                    FileList += DocumentFileNameList[i] + ", ";
                                    AttachmentIdsList += InEInfo.RecID + ", ";
                                    captionsList += model.Help_File_caption[i] + "~ ";
                                    //ratingsList += model.Help_File_Ratings[i] + "~ ";
                                    msg = msg + DocumentFileNameList[i] + " Uploaded...<b>(" + AttachmentSrNo + ")</b><br>";
                                }
                                else
                                {
                                    msg = msg + DocumentFileNameList[i] + " <b>Upload Failed...</b><br>";
                                }
                            }
                        }
                        if (FileList.Length > 0)
                        {
                            FileList = FileList.Remove(FileList.Length - 2);
                            AttachmentIdsList = AttachmentIdsList.Remove(AttachmentIdsList.Length - 2);
                            if (string.IsNullOrWhiteSpace(captionsList) == false)
                            {
                                captionsList = captionsList.Remove(captionsList.Length - 2);
                            }
                            if (string.IsNullOrWhiteSpace(ratingsList) == false)
                            {
                                ratingsList = ratingsList.Remove(ratingsList.Length - 2);
                            }
                        }
                        if (msg.Length > 0)
                        {
                            return Json(new
                            {
                                Message = msg,
                                result = true,
                                AttachmentID,
                                FileList,
                                AttachmentIdsList,
                                captionsList,
                                //ratingsList
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Messages.SomeError,
                                result = false,
                                AttachmentID,
                                FileList,
                                AttachmentIdsList,
                                captionsList,
                                ratingsList
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ActionMethod == "Edit")
                    {
                        var InEInfo = new Parameter_Update_Attachment();
                        InEInfo.FileName = model.Help_Document_FileName;
                        InEInfo.Description = model.Help_Document_Description == null ? "" : model.Help_Document_Description;
                        InEInfo.CategoryID = model.Help_Document_NameID;
                        InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                        InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                        InEInfo.File = model.Help_filefield;
                        InEInfo.RecID = model.ID;
                        InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                        InEInfo.Ref_Screen = model.Help_REF_SCREEN;
                        InEInfo.Checked = model.Help_Checked;
                        InEInfo.Vouching_Category = CommonFunctions.GetVouchingCategoryForCallingScreen(model.Help_REF_SCREEN ?? "");
                        var AttachmentID = InEInfo.RecID;
                        if (BASE._Attachments_DBOps.Update(InEInfo, clientScreen))
                        {
                            return Json(new
                            {
                                Message = "Attachment Updated Successfully...",
                                result = true,
                                AttachmentID
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SomeError,
                                result = false,
                                AttachmentID
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ActionMethod == "Delete")
                    {
                        string Actual_File_Name = model.Help_Document_FileName;
                        string Delete_File_Name = "";
                        var FileParts = Actual_File_Name.Split('.');
                        if (FileParts.Length > 1)
                        {
                            Delete_File_Name = model.ID + "." + FileParts[FileParts.Length - 1];
                        }
                        else
                        {
                            Delete_File_Name = model.ID;
                        }
                        if (BASE._Attachments_DBOps.Unlink_aLL(model.ID))
                        {
                            if (BASE._Attachments_DBOps.Delete_attachment(Delete_File_Name))
                            {
                                return Json(new
                                {
                                    Message = "Attachment Deleted Successfully...",
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
                else if (model.Help_Post_Controller_Name.Length > 0)
                {
                    SetBaseSession(model.Help_Post_Action_Name + "_AttachmentData", model);
                    TempData["ModelData"] = model;
                    //return RedirectToAction("Production_Documents_Attachment", "ProductionRegister",new {Area="Stock" });
                    return RedirectToAction(model.Help_Post_Action_Name, model.Help_Post_Controller_Name, new { Area = model.Help_Post_Area_Name, SessionGUID = Request.QueryString["SessionGUID"] });

                }

                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />{1}", ex.Message,ex.StackTrace);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public string PreventFileUpload(string FileName) 
        {
            string FileType = MimeMapping.GetMimeMapping(FileName);
            if (FileType.Contains("html") == true)
            {
                return FileName + " <b>Upload Failed.<br>Html Files Are Not Allowed<br></b>";
            }
            else if (FileType.Contains("x-msdownload") || FileType.Contains("x-ms-installer"))
            {
                return  FileName + " <b>Upload Failed.<br>Executable Files Are Not Allowed<br></b>";
            }
            else if (FileType.Contains("xspf+xml"))
            {
                return  FileName + " <b>Upload Failed.<br>xspf Files Are Not Allowed<br></b>";
            }
            else if (FileType.Contains("tiff"))
            {
                return FileName + " <b>Upload Failed.<br>tiff Files Are Not Allowed<br></b>";
            }
            else if (FileType.Contains("sct") || FileType.Contains("scriplet"))
            {
                return FileName + " <b>Upload Failed.<br>sct Files Are Not Allowed<br></b>";
            }
            return "";
        }
        public ActionResult Unlink_OR_Delete_Attachment(string AttachmentID, string RefRecID, string file_name, string RefScreen = "")
        {
            try
            {
                Parameter_GetAttachmentLinkCount inparam = new Parameter_GetAttachmentLinkCount();
                inparam.AttachmentID = AttachmentID;
                inparam.RefScreen = RefScreen;
                inparam.RefRecordID = RefRecID;
                var screen = BASE._Attachments_DBOps.GetAttachmentLinkScreen(inparam);
                if (!string.IsNullOrEmpty(screen))
                {
                    if (screen != RefScreen)
                    {
                        return UnLinkAttachment(RefRecID, AttachmentID);
                    }
                    else
                    {
                        return UnLinkAttachment(RefRecID, AttachmentID);
                    }
                }
                else
                {
                    return DeleteAttachment(file_name, AttachmentID);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UnLinkAttachment(string RefId, string ID)
        {
            try
            {
                if (BASE._Attachments_DBOps.Unlink_attachment(RefId, ID))
                {
                    return Json(new
                    {
                        result = true,
                        Message = "Attachment Unlinked Successfully..!!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        Message = Common_Lib.Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteAttachment(string FileName, string ID)
        {
            try
            {
                string Delete_File_Name = "";
                var FileParts = FileName.Split('.');
                if (FileParts.Length > 1)
                {
                    Delete_File_Name = ID + "." + FileParts[FileParts.Length - 1];
                }
                else
                {
                    Delete_File_Name = ID;
                }
                if (BASE._Attachments_DBOps.Unlink_aLL(ID))
                {
                    if (BASE._Attachments_DBOps.Delete_attachment(Delete_File_Name))
                    {
                        return Json(new
                        {
                            Message = "Attachment Deleted Successfully...",
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
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        private bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }



        public string GetFileType(byte[] FileField)
        {
            string filetype = "";
            byte[] first16Bytes = new byte[16];
            Array.Copy(FileField, 0, first16Bytes, 0, 16);
            string data_as_hex = BitConverter.ToString(first16Bytes);
            string MagicNumber = data_as_hex.Substring(0, 11);
            if (MagicNumber.StartsWith("42-4D"))
            {
                filetype = "bmp";
                return filetype;
            }
            else if (MagicNumber.StartsWith("FF-FB") || MagicNumber.StartsWith("49-44-33"))
            {
                filetype = "mp3";
                return filetype;
            }
            switch (MagicNumber)
            {
                case "25-50-44-46":
                    filetype = "pdf";
                    break;
                case "FF-D8-FF-DB":
                case "FF-D8-FF-EE":
                case "FF-D8-FF-E0":
                case "FF-D8-FF-E1":
                    filetype = "jpg";
                    break;
                case "89-50-4E-47":
                    filetype = "png";
                    break;
                case "47-49-46-38":
                    filetype = "gif";
                    break;
                case "7B-5C-72-74":
                    filetype = "rtf";
                    break;
                case "50-4B-03-04":
                    filetype = "docx";
                    break;
                case "D0-CF-11-E0":
                    filetype = "doc";
                    break;
                case "49-49-2A-00":
                case "4D-4D-00-2A":
                    filetype = "tiff";
                    break;
                case "38-42-50-53":
                    filetype = "psd";
                    break;
                case "52-49-46-46":
                    filetype = "webp";
                    break;

            }
            if (filetype.Length == 0)
            {
                string MagicNumber_4byteOffset = data_as_hex.Substring(12, 11);
                switch (MagicNumber_4byteOffset)
                {
                    case "66-74-79-70":
                        filetype = "mp4";
                        break;
                }
            }
            return filetype;
        }
    }
    public class AllowDifferentOrigin : ActionFilterAttribute, IActionFilter
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Remove("X-Frame-Options");
            base.OnResultExecuted(filterContext);
        }
    }
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool UserLoggedIn = true;
            HttpContext hContext = HttpContext.Current;
            string Referer = "";
            if (hContext.Request.UrlReferrer != null)
            {
                Referer = hContext.Request.UrlReferrer.AbsolutePath;
            }
            string url = hContext.Request.Url.AbsolutePath;
            string SessionGUID = hContext.Request.QueryString["SessionGUID"];
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                UserLoggedIn = false;
            }
            else 
            {
                if (hContext.Session["BASEClass"] == null)
                {
                    UserLoggedIn = false;
                }
                else                 
                {
                    List<BaseModel> ListOfBase = hContext.Session["BASEClass"] as List<BaseModel>;
                    var basedata = ListOfBase.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID));
                    if (basedata == null)
                    {
                        UserLoggedIn = false;
                    }                  
                }
            }          
            if (UserLoggedIn==false)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    if (Referer == "/Start/Home/Page" || url == "/Start/Home/Page")
                    {
                        filterContext.Result = new HttpStatusCodeResult(999, "GenericLogin");
                    }
                    else if (Referer.Contains("Facility/ServiceReport") || url.Contains("Facility/ServiceReport")) 
                    {
                        string QueryString = "";
                        if (Referer.Contains("Facility/ServiceReport"))
                        {
                            QueryString = hContext.Request.UrlReferrer.Query;
                        }
                        else
                        {
                            QueryString = hContext.Request.Url.Query;
                        }
                        if (string.IsNullOrWhiteSpace(QueryString) == false) 
                        {
                            QueryString = QueryString.Replace("ReporterEmail", "Email");
                            QueryString = QueryString.Replace("ReporterMobile", "Mobile");
                        }
                        filterContext.Result = new HttpStatusCodeResult(999, "ServiceReport|"+ QueryString);
                    }
                    else
                    {
                        filterContext.Result = new HttpStatusCodeResult(999, "Default");
                    }                   
                }
                else
                {
                    if (Referer == "/Start/Home/Page" || url == "/Start/Home/Page")
                    {
                        string cenID = "";
                        if (Referer == "/Start/Home/Page")
                        {
                            cenID = HttpUtility.ParseQueryString(hContext.Request.UrlReferrer.Query)["CenID"];
                        }
                        else
                        {
                            cenID = HttpUtility.ParseQueryString(hContext.Request.Url.Query)["CenID"];
                        }
                        string defaultpath = System.Configuration.ConfigurationManager.AppSettings["GenericPath"];
                        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        redirectTargetDictionary.Add("area", defaultpath.Split('/')[1]);
                        redirectTargetDictionary.Add("action", defaultpath.Split('/')[3]);
                        redirectTargetDictionary.Add("controller", defaultpath.Split('/')[2]);
                        redirectTargetDictionary.Add("CenID", cenID);
                        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    }
                    else if (Referer.Contains("Facility/ServiceReport") || url.Contains("Facility/ServiceReport"))
                    {
                        string QueryString = "";
                        if (Referer.Contains("Facility/ServiceReport"))
                        {
                            QueryString = hContext.Request.UrlReferrer.Query;
                        }
                        else
                        {
                            QueryString = hContext.Request.Url.Query;
                        }
                        if (string.IsNullOrWhiteSpace(QueryString) == false)
                        {
                            QueryString = QueryString.Replace("ReporterEmail", "Email");
                            QueryString = QueryString.Replace("ReporterMobile", "Mobile");                            
                        }
                        NameValueCollection queryParams = HttpUtility.ParseQueryString(QueryString);
                        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        redirectTargetDictionary.Add("area", "Facility");
                        redirectTargetDictionary.Add("action", "Frm_Reporter_Window");
                        redirectTargetDictionary.Add("controller", "ServiceReport");
                        foreach (string key in queryParams)
                        {
                            redirectTargetDictionary[key] = queryParams[key];
                        }
                        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    }
                    else
                    {
                        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        string defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"];

                        redirectTargetDictionary.Add("area", defaultpath.Split('/')[1]);
                        redirectTargetDictionary.Add("action", defaultpath.Split('/')[3]);
                        redirectTargetDictionary.Add("controller", defaultpath.Split('/')[2]);
                        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public static class MyExtensions
    {
        public static class GridViewHelper
        {
            public static void Custom_HeaderFilterFillItems_All(GridViewSettings settings)
            {
                settings.HeaderFilterFillItems = (s, e) =>
                {
                    if (e.Column.FieldName != "iIcon")
                    {
                        e.Values.Insert(1, FilterValue.CreateShowNonBlanksValue(e.Column, "(Non Blanks)"));
                        return;
                    }
                    e.Values.Clear();
                    e.AddShowAll();
                    e.AddValue("<img src='../../../../Content/Images/attach_red_2.png' width='20' height='20'</img>", "", "Contains([iIcon], 'RedShield')");//red
                    e.AddValue("<img src='../../../../Content/Images/attach_yellow_2.png' width='20' height='20'</img>", "", "Contains([iIcon], 'YellowShield')");//Yellow
                    e.AddValue("<img src='../../../../Content/Images/attach_green_2.png' width='20' height='20'</img>", "", "Contains([iIcon], 'GreenShield')");//green
                    e.AddValue("<img src='../../../../Content/Images/Completed_With_Remarks.png' width='20' height='20'</img>", "", "Contains([iIcon], 'BlueShield')");//blue
                    e.AddValue("<img src='../../../../Content/Images/Rejected_Attachment.png' width='20' height='20'</img>", "", "Contains([iIcon], 'RedFlag')");//reject
                    e.AddValue("<img src='../../../../Content/Images/attach_normal.png' width='20' height='20'</img>", "", "Contains([iIcon], 'RequiredAttachment')");//other
                    e.AddValue("<img src='../../../../Content/Images/attach_other_plus.png' width='20' height='20'</img>", "", "Contains([iIcon], 'AdditionalAttachment')");//other plus
                    e.AddValue("<img src='../../../../Content/Images/vAccept.png' width='15' height='15'</img>", "", "Contains([iIcon], 'VouchingAccepted')");//v accept
                    e.AddValue("<img src='../../../../Content/Images/vReject.png' width='15' height='15'</img>", "", "Contains([iIcon], 'VouchingReject')");//v reject
                    e.AddValue("<img src='../../../../Content/Images/vAcceptRemark.png' width='15' height='15'</img>", "", "Contains([iIcon], 'VouchingAcceptWithRemarks')");//v remark
                    e.AddValue("<img src='../../../../Content/Images/vPartial.png' width='15' height='15'</img>", "", "Contains([iIcon], 'VouchingPartial')");//v partial
                    e.AddValue("<img src='../../../../Content/Images/aAccept.png' width='15' height='15'</img>", "", "Contains([iIcon], 'AuditAccepted')");//a accept
                    e.AddValue("<img src='../../../../Content/Images/aReject.png' width='15' height='15'</img>", "", "Contains([iIcon], 'AuditReject')");//a reject
                    e.AddValue("<img src='../../../../Content/Images/aAcceptRemark.png' width='15' height='15'</img>", "", "Contains([iIcon], 'AuditAcceptWithRemarks')");//a remark
                    e.AddValue("<img src='../../../../Content/Images/aPartial.png' width='15' height='15'</img>", "", "Contains([iIcon], 'AuditPartial')");//a partial
                    e.AddValue("<img src='../../../../Content/Images/AutoVouching.png' width='15' height='15'</img>", "", "Contains([iIcon], 'AutoVouching')");//Auto_Vouching
                    e.AddValue("<img src='../../../../Content/Images/CorrectedEntry.png' width='15' height='15'</img>", "", "Contains([iIcon], 'CorrectedEntry')");//Corrected_Entry
                };
            }
            public static void CustomGridView(GridViewSettings settings)
            {
                if (HttpContext.Current.Request.Browser.IsMobileDevice)
                { 
                    settings.SettingsBehavior.AllowDragDrop = false;
                    //set paging gestures to false for all screens in mobile
                    settings.EnablePagingGestures = AutoBoolean.False;
                }
                settings.ControlStyle.CssClass = "DevexpressComponents";
            }
            public static void GridViewExportSettings(GridViewSettings settings, string xTitle, bool xLandScape, System.Drawing.Printing.PaperKind xPaperKind, string xTopLeft_Header, string xTopCentre_Header, string xTopRight_Header, int xSetTopMargin = 50, int xSetBottomMargin = 40, int xSetHeaderFontSize = 14, string xBottomLeft_Footer = "", string xBottomCentre_Footer = "", string xBottomRight_Footer = "", int xCellFontSize = 9)
            {
                settings.ControlStyle.CssClass = "DevexpressComponents";
                if (string.IsNullOrWhiteSpace(xBottomLeft_Footer))
                {
                    xBottomLeft_Footer = "Page [Page # of Pages #]";
                }
                if (string.IsNullOrWhiteSpace(xBottomRight_Footer))
                {
                    xBottomRight_Footer = "Print On: [Date Printed], [Time Printed]";
                }
                settings.SettingsExport.EnableClientSideExportAPI = true;
                settings.SettingsExport.ExcelExportMode = DevExpress.Export.ExportType.DataAware;

                settings.SettingsExport.FileName = xTitle;
                settings.SettingsExport.Landscape = xLandScape;
                settings.SettingsExport.PaperKind = xPaperKind;
                settings.SettingsExport.TopMargin = xSetTopMargin;
                settings.SettingsExport.BottomMargin = xSetBottomMargin;
                settings.SettingsExport.LeftMargin = 40;
                settings.SettingsExport.RightMargin = 40;

                settings.SettingsExport.PageHeader.Left = xTopLeft_Header;
                settings.SettingsExport.PageHeader.Center = xTopCentre_Header;
                settings.SettingsExport.PageHeader.Right = xTopRight_Header;
                settings.SettingsExport.PageHeader.Font.Name = "Arial";
                settings.SettingsExport.PageHeader.Font.Size = xSetHeaderFontSize;
                settings.SettingsExport.PageHeader.VerticalAlignment = DevExpress.XtraPrinting.BrickAlignment.None;

                settings.SettingsExport.PageFooter.Left = xBottomLeft_Footer;
                settings.SettingsExport.PageFooter.Center = xBottomCentre_Footer;
                settings.SettingsExport.PageFooter.Right = xBottomRight_Footer;
                settings.SettingsExport.PageFooter.Font.Name = "Verdana";
                settings.SettingsExport.PageFooter.Font.Size = 8;
                settings.SettingsExport.PageFooter.VerticalAlignment = DevExpress.XtraPrinting.BrickAlignment.Far;

                settings.SettingsExport.Styles.Footer.Font.Size = xCellFontSize;
                settings.SettingsExport.Styles.Cell.Font.Size = xCellFontSize;
                settings.SettingsExport.Styles.Cell.Font.Name = "Tahoma";
                settings.SettingsExport.Styles.Header.BackColor = System.Drawing.Color.FromArgb(100, 211, 211, 211);
                settings.SettingsExport.Styles.Header.ForeColor = System.Drawing.Color.Black;
                settings.SettingsExport.Styles.Footer.BackColor = System.Drawing.Color.FromArgb(100, 169, 169, 169);
                settings.SettingsExport.Styles.Footer.ForeColor = System.Drawing.Color.Black;
            }
        }
        public static GridViewSettings CustomGridView(this WebViewPage view)
        {
            GridViewSettings settings = new GridViewSettings();
            return settings;
        }
        public static SpinEditSettings NotebookSpinEdit(GridViewDataItemTemplateContainer c, bool Set_Disable)
        {
            SpinEditSettings x = new SpinEditSettings();
            x.Name = c.Column.VisibleIndex + "_" + c.VisibleIndex + "_Editor";
            x.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            x.Properties.SpinButtons.ShowIncrementButtons = false;
            x.Properties.AllowMouseWheel = false;
            x.ControlStyle.BackColor = System.Drawing.Color.Transparent;
            x.ControlStyle.Border.BorderStyle = BorderStyle.None;
            x.ControlStyle.HorizontalAlign = HorizontalAlign.Right;
            x.Properties.NumberType = SpinEditNumberType.Integer;
            x.Properties.AllowNull = true;
            x.Properties.DisplayFormatString = "#";
            x.Properties.ShowOutOfRangeWarning = false;
            x.Properties.MaxLength = 9;
            x.Properties.InvalidStyle.BackColor = System.Drawing.Color.Red;
            x.Properties.FocusedStyle.BackColor = System.Drawing.Color.LightCyan;
            x.Enabled = !Set_Disable;
            x.DisabledStyle.ForeColor = System.Drawing.Color.Black;
            x.Properties.ClientSideEvents.LostFocus = "function (s, e) {{ NB_EditorLostFocus(); }}";
            x.Properties.ClientSideEvents.GotFocus = String.Format("function (s, e) {{ ShowEntryDateMonth(s, '{0}'); }}", c.Column.Caption);
            x.Properties.ClientSideEvents.ValueChanged = String.Format("function (s, e) {{ NoteBookEntry_ValueChanged(s, '{0}', '{1}'); }}", c.Column.FieldName, c.KeyValue);
            x.Properties.ClientSideEvents.KeyDown = "function (s, e) {{ NB_EditorKeyDown(s,e); }}";
            return x;
        }
        public static DataGridBuilder<object> CustomDataGridListing(this DataGridBuilder<object> grid, int AllColumns = 1)
        {
            grid.ElementAttr(new { @class = "DataGridListing" });
            grid.AllowColumnReordering(true);
            grid.ColumnAutoWidth(true);          
            grid.HoverStateEnabled(true);
            grid.RowAlternationEnabled(true);
            grid.ShowBorders(true);
            grid.ShowColumnLines(true);
            grid.ShowRowLines(true);
            grid.WordWrapEnabled(true);
            grid.Width("100%");
            //selection settings
            grid.Selection(s => s.Mode(SelectionMode.Single));
            //searchPanel settings
            grid.SearchPanel(s => s.HighlightSearchText(true).SearchVisibleColumnsOnly(true));
            //scrolling settings
            grid.Scrolling(s => s.Mode(GridScrollingMode.Standard).ScrollByThumb(true).ScrollByContent(true).ShowScrollbar(ShowScrollbarMode.Always).UseNative(true));
            //Pager Settings
            grid.Paging(p => p.PageSize(25));
            grid.Pager(p => p.ShowPageSizeSelector(true).ShowNavigationButtons(true).ShowInfo(true).AllowedPageSizes(new JS("[10,20,25,50,100,200]")).Visible(true)); //need to configure all
            //header filter
            grid.HeaderFilter(s => s.Visible(true).AllowSearch(true));
            //filter row 
            grid.FilterRow(s => s.Visible(true));
            //filter panel
            grid.FilterPanel(s => s.Visible(true));
            // responsive settings
            if (AllColumns == 1)
            {
                grid.ColumnHidingEnabled(false);
                grid.AllowColumnResizing(true);             
            }
            else
            {
                grid.ColumnHidingEnabled(true);
                grid.AllowColumnResizing(false);               
            }
            grid.ColumnResizingMode(ColumnResizingMode.Widget);          
            grid.Grouping(g => g.AutoExpandAll(true).ContextMenuEnabled(true).ExpandMode(GridGroupingExpandMode.ButtonClick));
            grid.Sorting(sorting => sorting.Mode(GridSortingMode.Multiple));
            grid.FilterSyncEnabled(true);
            grid.OnAdaptiveDetailRowPreparing("function(e) { { DxGrid_AdaptiveDetailRowPreparing(e); }}");
            grid.ColumnChooser(s => s.Mode(HttpContext.Current.Request.Browser.IsMobileDevice?GridColumnChooserMode.Select: GridColumnChooserMode.DragAndDrop));
            grid.CustomizeColumns("DxGrid_CustomizeColumns");
            grid.OnEditorPreparing("DxGrid_onEditorPreparing");
            grid.OnOptionChanged("DxGrid_OnOnOptionChanged");          
            grid.OnCellClick("DxGrid_OnCellClick");          
            return grid;
        }
        public static DataGridBuilder<object> CustomDataGrid(this DataGridBuilder<object> grid)
        {
            grid.ElementAttr(new { @class = "DropDownDataGrid" });
            grid.DataSource(new JS(@"component.option(""dataSource"")"));
            grid.Paging(p => p.PageSize(HttpContext.Current.Request.Browser.IsMobileDevice ? 10:100));
            grid.HoverStateEnabled(true);
            grid.OnKeyDown("function (e) {{ DataGrid_keyDown(e,component); }}");
            grid.OnEditorPreparing("function (e) {{ DataGrid_OnEditorPreparing(e,component); }}");
            grid.OnRowClick("function (e) {{ DataGrid_OnRowClick(e,component); }}");
            grid.SearchPanel(s => s
                  .Visible(true)
                  .HighlightSearchText(true)
                  .SearchVisibleColumnsOnly(true)
                  .Placeholder(" ")
                  );
            grid.FilterRow(f => f.Visible(true));
            if (HttpContext.Current.Request.Browser.IsMobileDevice == false)
            {
                grid.FilterPanel(f => f.Visible(true));
            }
            grid.Scrolling(s => s.Mode(GridScrollingMode.Infinite)
            .UseNative(false)
            .ScrollByThumb(true)
            .ScrollByContent(true)
            .ShowScrollbar(ShowScrollbarMode.Always)
            );
            grid.Grouping(g => g.AllowCollapsing(false));
            grid.AllowColumnResizing(true);
            grid.ColumnResizingMode(ColumnResizingMode.Widget);
            grid.ColumnAutoWidth(true);
            grid.Height(300); //Redmine Bug #133565 fix  Redmine Bug #134774 fix
            grid.OnSelectionChanged("function(selectedItems){ var keys = selectedItems.selectedRowKeys;component.option('value', keys);}");
            grid.ShowColumnLines(true);
            grid.ShowRowLines(true);
            grid.RowAlternationEnabled(true);
            grid.ShowBorders(true);
            grid.WordWrapEnabled(true);
            return grid;
        }
        public static DateBoxBuilder CustomDateBox(this DateBoxBuilder DateBox, bool SetMaxDate = false)
        {
            DateBox.PickerType(HttpContext.Current.Request.Browser.IsMobileDevice?DateBoxPickerType.Native: DateBoxPickerType.Calendar);
            DateBox.DisplayFormat("dd-MM-yyyy");
            DateBox.Type(DateBoxType.Date);
            DateBox.ShowClearButton(true);
            DateBox.Min(new DateTime(Convert.ToInt32(HttpContext.Current.Session["Open_Year_Sdt_Year"]), 4, 1));
            DateBox.InvalidDateMessage("Date Is Invalid..!!");
            //DateBox.OnClosed("function(e){ e.component.focus();}");
            DateBox.Max(new DateTime(2999, 12, 31));
            DateBox.UseMaskBehavior(true);
            DateBox.ValidationMessageMode(ValidationMessageMode.Always);
            DateBox.Placeholder("dd-MM-yyyy");
            DateBox.DateOutOfRangeMessage("Date Not As Per Financial Year...!");
            //DateBox.DateOutOfRangeMessage("Cannot Be Less Than 01-04-" + HttpContext.Current.Session["Open_Year_Sdt_Year"].ToString());
            DateBox.AdaptivityEnabled(true);
            DateBox.CalendarOptions(s => s.ShowTodayButton(true));
            if (SetMaxDate == true)
            {
                DateBox.Max(new DateTime(Convert.ToInt32(HttpContext.Current.Session["Open_Year_Edt_Year"]), 3, 31));
            }
            return DateBox;
        }
        public static DropDownBoxBuilder CustomDropDown(this DropDownBoxBuilder DD)
        {
            DD.DeferRendering(false);
            DD.ShowClearButton(true);
            DD.OnKeyDown("DropDown_KeyPress");
            DD.ElementAttr("OnClick", "DropDown_Click(this)");
            DD.OnClosed("DropDown_Close");
            DD.DropDownOptions(s => s
            .ResizeEnabled(true)
            .Width("auto")
            .DeferRendering(false)
            .OnResizeEnd("DDPopup_ResizeEnd")
             //.Height(300)
             //.OnShowing("PopupOnShowing")         
             //.OnShown("function (e) {{ e.component.content().on('dxmousewheel',function(evt){evt.stopPropagation();})}}") //Redmine Bug #134774 fix
             );
            return DD;
        }
        public static SelectBoxBuilder CustomSelectBox(this SelectBoxBuilder SelectBox)
        {
            SelectBox.DeferRendering(false);
            SelectBox.ShowClearButton(true);
            SelectBox.SearchEnabled(true);
            SelectBox.WrapItemText(true);
            SelectBox.SearchTimeout(150);
            SelectBox.SearchMode(DropDownSearchMode.StartsWith);
            SelectBox.OnFocusIn("function (e) {{ TextEditor_OnFocusIn(e); }}");
           // SelectBox.DataSourceOptions(a => a.Paginate(true).PageSize(HttpContext.Current.Request.Browser.IsMobileDevice ? 10 : 100));
            return SelectBox;
        }
        public static TextBoxBuilder CustomTextBox(this TextBoxBuilder TextBox, bool CleanTextForDB = false, bool ToupperCase = false)
        {
            TextBox.ShowClearButton(true);
            TextBox.InputAttr(new { @class = (CleanTextForDB == true ? "CleanTextForDB" : "") + (ToupperCase == true ? " TextBoxUpperCase" : "") });
            TextBox.OnContentReady("function (e) {{ TextBox_OnContentReady(e); }}");
            return TextBox;
        }
        public static NumberBoxBuilder CustomNumberBox(this NumberBoxBuilder NumberBox, bool IsMobile = false)
        {
            bool IsMobileCheck = HttpContext.Current.Request.Browser.IsMobileDevice;

            if (IsMobileCheck)
            {
                NumberBox.ShowSpinButtons(false);
                NumberBox.ShowClearButton(false);
            }
            else
            {
                NumberBox.ShowSpinButtons(true);
            }

            NumberBox.InputAttr(new { @style = "text-align:left" });
            return NumberBox;
        }
        public static ButtonBuilder CustomSaveButton(this ButtonBuilder Button, string ActionMethod = "")
        {
            if (ActionMethod == "_Delete" || ActionMethod == "Delete")
            {
                Button.Icon("fas fa-check-circle btn-icons deleteicon icon-rgt-space5");
            }
            else
            {
                Button.Icon("fas fa-check-circle btn-icons checkgreen icon-rgt-space5");
            }
            Button.ElementAttr(new { @class = "FormActionButton" });
            return Button;
        }
        public static ButtonBuilder CustomCloseButton(this ButtonBuilder Button)
        {
            Button.Icon("fa fa-times-circle fa-w-16 btn-icons crossgrey");
            // Button.OnClick("function(e){HidePopup();}");
            Button.ElementAttr(new { @class = "FormActionButton" });
            return Button;
        }
        public static ListBuilder CustomList(this ListBuilder List)
        {
            List.SearchEnabled(true);
            List.PageLoadMode(ListPageLoadMode.ScrollBottom);
            return List;
        }
        public static PopupBuilder CustomPopup(this PopupBuilder popup)
        {
            popup.OnHidden("function(e) { PopupOnHidden(e); }");
            popup.OnShown("function(e) { PopupOnShown(e); }");
            popup.OnShowing("function(e){PopupOnShowing(e);}");
            popup.ShowCloseButton(true);
            popup.MaxHeight("100%");
            return popup;
        }
        public static HtmlEditorBuilder CustomHtmlEditor(this HtmlEditorBuilder html,bool variable=false)
        {
            List<string> sizes = new List<string>();
            for (int i = 6; i < 34; i+=2)
            {
                sizes.Add(i + "px");
            }
            html.ValueType(HtmlEditorValueType.Html);
            html.Toolbar(toolbar => toolbar.Items(
        items =>
        {
            //items.Add().FormatName(HtmlEditorToolbarItem.Undo);
            //items.Add().FormatName(HtmlEditorToolbarItem.Redo);
            items.Add().FormatName(HtmlEditorToolbarItem.Separator).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Background);
            items.Add().FormatName(HtmlEditorToolbarItem.Color);
            items.Add().FormatName(HtmlEditorToolbarItem.Separator);
            items.Add().FormatName("header").FormatValues(new JS("[false, 1, 2, 3, 4, 5]"));
            items.Add().FormatName(HtmlEditorToolbarItem.Separator);
            items.Add().FormatName(HtmlEditorToolbarItem.Bold);
            items.Add().FormatName(HtmlEditorToolbarItem.Italic);
            items.Add().FormatName(HtmlEditorToolbarItem.Strike).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Underline);
            items.Add().FormatName(HtmlEditorToolbarItem.Separator);
            items.Add().FormatName(HtmlEditorToolbarItem.AlignLeft);
            items.Add().FormatName(HtmlEditorToolbarItem.AlignCenter);
            items.Add().FormatName(HtmlEditorToolbarItem.AlignRight);
            items.Add().FormatName(HtmlEditorToolbarItem.AlignJustify).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Separator);
            items.Add().FormatName(HtmlEditorToolbarItem.Blockquote).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.BulletList);
            //items.Add().FormatName(HtmlEditorToolbarItem.Clear);
            //items.Add().FormatName(HtmlEditorToolbarItem.CodeBlock);
            items.Add().FormatName(HtmlEditorToolbarItem.DecreaseIndent).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Font);
            items.Add().FormatName(HtmlEditorToolbarItem.Header);
            items.Add().FormatName(HtmlEditorToolbarItem.Image);
            items.Add().FormatName(HtmlEditorToolbarItem.IncreaseIndent).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Link);
            items.Add().FormatName(HtmlEditorToolbarItem.OrderedList).LocateInMenu(ToolbarItemLocateInMenuMode.Auto);
            items.Add().FormatName(HtmlEditorToolbarItem.Size).FormatValues(sizes);
            items.Add().FormatName(HtmlEditorToolbarItem.Variable).Visible(variable).CssClass("HtmlEditorVariable");
            // items.Add().FormatName(HtmlEditorToolbarItem.Variable);
        }).Multiline(true));
            return html;
        }
        public static string HandleEscapeCharacters(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                str = str.Replace("\\", "\\\\");
                str = str.Replace("\"", "\\\"");
                return str;
            }
        }
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
            }
        }
        public static string FillNullOrEmptyString(this string str, string fillString = "--")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return fillString;
            }
            else
            {
                return str;
            }
        }
        public static string ReplacePwithDivTags(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) == false)
            {
                str = System.Net.WebUtility.HtmlDecode(HttpUtility.HtmlDecode(str));
                // str = Regex.Replace(str, @"^[<p]$", "<div");
                //  str = Regex.Replace(str, @"^[p>]$", "div>");
                str = str.Replace("<p", "<div");
                str = str.Replace("p>", "div>");
            }
            return str;
        }
        public static string ConvertHtmltoPlainString(this string originalHtml)
        {
            #region remove_space start
            //string pattern = "(<strong>|<b>|<i>|<em>)\\s+";
            //string replacement = "$1";
            //Regex rgx = new Regex(pattern);
            //string result = rgx.Replace(originalHtml, replacement);

            //pattern = "\\s+(</strong>|</b>|</i>|</em>)";
            //rgx = new Regex(pattern);
            //originalHtml = rgx.Replace(result, replacement);
            var doc = new HtmlDocument();
            if (string.IsNullOrWhiteSpace(originalHtml))
            {
                return null;
            }
            doc.LoadHtml(originalHtml);

            var tags = new[] { "strong", "em" };

            foreach (var tag in tags)
            {
                var nodes = doc.DocumentNode.Descendants(tag);

                foreach (var node in nodes)
                {
                    if (node.InnerHtml != null && node.InnerHtml.StartsWith(" "))
                    {
                        node.InnerHtml = node.InnerHtml.TrimStart();
                        if (node.PreviousSibling != null && node.PreviousSibling.InnerHtml != null)
                        {
                            node.PreviousSibling.InnerHtml += " ";
                        }
                    }

                    if (node.InnerHtml != null && node.InnerHtml.EndsWith(" "))
                    {
                        node.InnerHtml = node.InnerHtml.TrimEnd();
                        if (node.NextSibling != null && node.NextSibling.InnerHtml != null)
                        {
                            node.NextSibling.InnerHtml = " " + node.NextSibling.InnerHtml;
                        }
                    }
                }
            }
            originalHtml = doc.DocumentNode.OuterHtml;
            #endregion remove-space end
            string html = originalHtml;
            string finalString = null;
            if (string.IsNullOrWhiteSpace(html) == false)
            {
                //html = Regex.Replace(html, @"<br>", "<br>");
                //if (!html.EndsWith("</p>"))
                //{
                //    if (html.EndsWith("<br>"))
                //    {
                //        html = html.Remove(html.Length - 4); // Remove the last <br>
                //    }
                //}
                if (html.StartsWith("<p>") && html.EndsWith("</p>"))
                {
                    int startTagIndex = html.IndexOf("<p>");
                    int endTagIndex = html.LastIndexOf("</p>");
                    html = html.Remove(endTagIndex, 4).Remove(startTagIndex, 3);
                }
                html = Regex.Replace(html, @"<br>", "%0a");
                html = Regex.Replace(html, @"<p>", "%0a");
                html = Regex.Replace(html, @"</p>", "");
                html = Regex.Replace(html, @"<strong>", "*");
                html = Regex.Replace(html, @"</strong>", "*");
                html = Regex.Replace(html, @"<em>", "_");
                html = Regex.Replace(html, @"</em>", "_");
                html = System.Net.WebUtility.HtmlDecode(System.Web.HttpUtility.HtmlDecode(html));
                //html = Regex.Replace(html, @"<\/p>", "");
                //html = Regex.Replace(html, @"<p>", "");
                //string underscoredText = Regex.Replace(html, "<em>(.*?)</em>", "_$1_");
                //underscoredText = Regex.Replace(underscoredText, "<i>(.*?)</i>", "_$1_");
                //string asteriskedText = Regex.Replace(underscoredText, "<strong>(.*?)</strong>", "*$1*");
                //asteriskedText = Regex.Replace(asteriskedText, "<b>(.*?)</b>", "*$1*");
                //finalString = asteriskedText.Replace("<p style=\"text - align: right; \">", "");
                //finalString = asteriskedText.Replace("<p style=\"text - align: left; \">", "");
                //finalString = asteriskedText.Replace("<p style=\"text - align: centre; \">", "");
            }
            //return finalString;
            return html;
        }
        public static string ConvertWhatsApptoHtml(this string whatsappText)
        {
            whatsappText = whatsappText ?? "";
            whatsappText = System.Net.WebUtility.HtmlDecode(HttpUtility.HtmlDecode(whatsappText));
            StringBuilder htmlBuilder = new StringBuilder();
            bool isBold = false;
            bool isItalic = false;
            bool isStrikethrough = false;

            for (int i = 0; i < whatsappText.Length; i++)
            {
                char currentChar = whatsappText[i];

                if (currentChar == '*')
                {
                    isBold = !isBold;
                    if (isBold)
                    {
                        htmlBuilder.Append("<strong>");
                    }
                    else
                    {
                        htmlBuilder.Append("</strong>");
                    }
                }
                else if (currentChar == '_')
                {
                    isItalic = !isItalic;
                    if (isItalic)
                    {
                        htmlBuilder.Append("<em>");
                    }
                    else
                    {
                        htmlBuilder.Append("</em>");
                    }
                }
                else if (currentChar == '~')
                {
                    isStrikethrough = !isStrikethrough;
                    if (isStrikethrough)
                    {
                        htmlBuilder.Append("<s>");
                    }
                    else
                    {
                        htmlBuilder.Append("</s>");
                    }
                }
                else
                {
                    htmlBuilder.Append(currentChar);
                }
            }

            // Close any open tags
            if (isBold)
            {
                htmlBuilder.Append("</strong>");
            }
            if (isItalic)
            {
                htmlBuilder.Append("</em>");
            }
            if (isStrikethrough)
            {
                htmlBuilder.Append("</s>");
            }

            return htmlBuilder.ToString().Replace("%0a", "<br>");
           
        }
        public static string ConvertHtmlToWhatsappText(this string htmlString)
        {
            htmlString = htmlString ?? "";
            var doc = new HtmlDocument();
            doc.LoadHtml(System.Net.WebUtility.HtmlDecode(HttpUtility.HtmlDecode(htmlString)));
            //string output=ConvertNodeToWhatsappText(doc.DocumentNode);
            List<HtmlNodeInfo> NodesList = new List<HtmlNodeInfo>();
            ExtractNodesRecursively(doc.DocumentNode, NodesList);
            int StartTagCount = 0;
            int EndTagCount = 0;
            for (int i = 0; i < NodesList.Count; i++) 
            {
                string tag = "";
                if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false || string.IsNullOrWhiteSpace(NodesList[i].EndTag) == false) 
                {
                    if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                    {
                        tag = NodesList[i].StartTag.ToLower();
                        StartTagCount++;
                    }
                    else if (string.IsNullOrWhiteSpace(NodesList[i].EndTag) == false) 
                    {
                        tag = NodesList[i].EndTag.ToLower();
                        EndTagCount++;
                    }                       
                    switch (tag)
                    {
                        case "<b>":
                        case "<strong>":
                        case "</strong>":
                        case "</b>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "*";
                            }
                            else 
                            {
                                NodesList[i].EndTag = "*";
                            }                                
                            break;
                        case "<i>":
                        case "<em>":    
                        case "</i>":
                        case "</em>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "_";
                            }
                            else
                            {
                                NodesList[i].EndTag = "_";
                            }
                            break;
                        case "<strike>":
                        case "<s>":
                        case "</strike>":
                        case "</s>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "~";
                            }
                            else
                            {
                                NodesList[i].EndTag = "~";
                            }
                            break;                    
                        case "<h1>":
                        case "<h2>":
                        case "<h3>":
                        case "<h4>":
                        case "<h5>":
                        case "<h6>":
                        case "</h1>":
                        case "</h2>":
                        case "</h3>":
                        case "</h4>":
                        case "</h5>":
                        case "</h6>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "%0a*";
                            }
                            else
                            {
                                NodesList[i].EndTag = "*%0a%0a";
                            }
                            break;                         
                        case "<p>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "";
                            }
                            else
                            {
                                NodesList[i].EndTag = "";
                            }
                            break;
                        case "</p>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "%0a";
                            }
                            else
                            {
                                if (i < NodesList.Count - 1)
                                {
                                    NodesList[i].EndTag = "%0a";
                                }
                                else 
                                {
                                    NodesList[i].EndTag = "";
                                }
                            }
                            break;                        
                        case "<br>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "";
                            }
                            else
                            {
                                NodesList[i].EndTag = "";
                            }
                            break;
                        case "</br>":
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = "%0a";
                            }
                            else
                            {
                                if (i < NodesList.Count - 1 && string.IsNullOrWhiteSpace(NodesList[i + 1].EndTag)==false && NodesList[i + 1].EndTag == "</p>")
                                {
                                    NodesList[i].EndTag = "";
                                }
                                else 
                                {
                                    NodesList[i].EndTag = "%0a";
                                }
                            }
                            break;    
                        default:
                            if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                            {
                                NodesList[i].StartTag = NodesList[i].StartTag;
                            }
                            else
                            {
                                NodesList[i].EndTag = NodesList[i].EndTag;
                            }
                            break;                           
                    }
                }
                if (i > 0) 
                {
                    if (string.IsNullOrWhiteSpace(NodesList[i].Text) == false)
                    {
                        string BeforeSpaces = GetEmptySpacesBefore(NodesList[i].Text);
                        string AfterSpaces = GetEmptySpacesAfter(NodesList[i].Text);
                        if (string.IsNullOrWhiteSpace(NodesList[i - 1].StartTag) == false)
                        {
                            if (BeforeSpaces != null && BeforeSpaces.Length > 0) //no space after start tag
                            {
                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (string.IsNullOrWhiteSpace(NodesList[j].EndTag) == false)
                                    {
                                        NodesList[j].EndTag = NodesList[j].EndTag + BeforeSpaces;
                                        NodesList[i].Text = NodesList[i].Text.TrimStart();
                                        break;
                                    }
                                    else if (string.IsNullOrWhiteSpace(NodesList[j].Text) == false)
                                    {
                                        NodesList[j].Text = NodesList[j].Text + BeforeSpaces;
                                        NodesList[i].Text = NodesList[i].Text.TrimStart();
                                        break;
                                    }
                                }                               
                            }
                        }
                        if (i!= (NodesList.Count-1) && string.IsNullOrWhiteSpace(NodesList[i + 1].EndTag) == false)
                        {
                            if (AfterSpaces != null && AfterSpaces.Length > 0) //no space before end tag
                            {
                                for (int j = i + 1; j < NodesList.Count; j++)
                                {
                                    if (string.IsNullOrWhiteSpace(NodesList[j].Text) == false)
                                    {
                                        NodesList[j].Text = AfterSpaces + NodesList[j].Text;
                                        NodesList[i].Text = NodesList[i].Text.TrimEnd();
                                        break;
                                    }
                                }
                            }
                        }
                        //if (StartTagCount != EndTagCount)
                        //{
                        //    NodesList[i].Text = NodesList[i].Text.Trim();
                        //}
                    }
                }
            }
            StringBuilder WhatsAppText = new StringBuilder();
            for (int i = 0; i < NodesList.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(NodesList[i].StartTag) == false)
                {
                    WhatsAppText.Append(NodesList[i].StartTag);
                }
                else if (NodesList[i].Text!=null && NodesList[i].Text.Length>0) 
                {
                    WhatsAppText.Append(NodesList[i].Text);
                }
                else if (string.IsNullOrWhiteSpace(NodesList[i].EndTag) == false)
                {
                    WhatsAppText.Append(NodesList[i].EndTag);
                }
            }
            return WhatsAppText.ToString();
        }
        public static void ExtractNodesRecursively(HtmlNode node, List<HtmlNodeInfo> NodesList)
        {
            foreach (var childNode in node.ChildNodes)
            {
                if (childNode.NodeType == HtmlNodeType.Text)
                {
                    // Text node
                    NodesList.Add(new HtmlNodeInfo
                    {
                        StartTag = "",               
                        Text = childNode.InnerText,               
                        EndTag = ""
                    });
                    
                }
                else if (childNode.NodeType == HtmlNodeType.Element)
                {
                    NodesList.Add(new HtmlNodeInfo
                    {
                        StartTag = childNode.OuterHtml.Substring(0, childNode.Name.Length + 2),
                        Text = "",
                        EndTag = ""
                    });                     
                    ExtractNodesRecursively(childNode, NodesList);
                    NodesList.Add(new HtmlNodeInfo
                    {
                        StartTag = "",
                        Text = "",
                        EndTag = "</" + childNode.Name + ">"
                    });               
                }
            }           
        }     
        public static string GetEmptySpacesBefore(this string text) 
        {
            var spaces = "";
            if (text.StartsWith(" "))           
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        spaces += " ";
                    }
                    else { break; }
                }
            }
            return spaces;
        }
        public static string GetEmptySpacesAfter(this string text)
        {
            var spaces = "";
            if (text.EndsWith(" "))
            {
                for (int i = text.Length-1; i >=0; i--)
                {
                    if (text[i] == ' ')
                    {
                        spaces += " ";
                    }
                    else { break; }
                }
            }
            return spaces;
        }
        public static string RemoveFirstPTag(this string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection pTags = doc.DocumentNode.SelectNodes("//p");
            if (pTags != null && pTags.Count > 0)
            {
                // Find the first <p> tag
                HtmlNode firstPTag = pTags[0];

                // If the first <p> tag has parent, replace it with its children
                if (firstPTag.ParentNode != null)
                {
                    foreach (HtmlNode childNode in firstPTag.ChildNodes)
                    {
                        firstPTag.ParentNode.InsertBefore(childNode, firstPTag);
                    }
                    firstPTag.Remove();
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
        public static string ExtractTextFromHTML(this string html)
        {
            if (string.IsNullOrWhiteSpace(html)) { return ""; }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var chunks = new List<string>();

            foreach (var item in doc.DocumentNode.DescendantsAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            return String.Join(" ", chunks);
        }
        public static int GetFinancialYear(this DateTime dateTime)
        {
            string beginYear = "";
            string endYear = "";
            if (dateTime.Month >= 4)
            {
                beginYear = dateTime.ToString("yy");
                endYear = dateTime.AddYears(1).ToString("yy");
            }
            else
            {
                beginYear = dateTime.AddYears(-1).ToString("yy");
                endYear = dateTime.ToString("yy");
            }
            return Convert.ToInt32(string.Concat(beginYear, endYear));
        }
        public static string GetFinancialYearName(this DateTime dateTime)
        {
            string beginYear = "";
            string endYear = "";
            if (dateTime.Month >= 4)
            {
                beginYear = dateTime.ToString("yyyy");
                endYear = dateTime.AddYears(1).ToString("yyyy");
            }
            else
            {
                beginYear = dateTime.AddYears(-1).ToString("yyyy");
                endYear = dateTime.ToString("yyyy");
            }
            return string.Concat(beginYear, " - ", endYear);
        }
        public class HtmlNodeInfo
        {
            public string StartTag { get; set; }
            public string Text { get; set; }
            public string EndTag { get; set; }
        }
    }
}    