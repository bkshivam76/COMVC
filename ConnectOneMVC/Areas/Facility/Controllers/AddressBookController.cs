using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
using System.Globalization;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using System.Net;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    [CheckLogin]
    public class AddressBookController : BaseController
    {

        #region "Start--> Default Variables"
        public List<Address_Book> Addressbook_ExportData
        {
            get
            {
                return (List<Address_Book>)GetBaseSession("Addressbook_ExportData_AdresBuk");
            }
            set
            {
                SetBaseSession("Addressbook_ExportData_AdresBuk", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> AddressBookInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("AddressBookInfo_DetailGrid_Data_AdresBuk");
            }
            set
            {
                SetBaseSession("AddressBookInfo_DetailGrid_Data_AdresBuk", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> AddressBookInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("AddressBookInfo_AdditionalInfoGrid_AdresBuk");
            }
            set
            {
                SetBaseSession("AddressBookInfo_AdditionalInfoGrid_AdresBuk", value);
            }
        }        
        public byte[] AB_Image
        {
            get
            {
                return (byte[])GetBaseSession("AB_Image_ABWindow");
            }
            set
            {
                SetBaseSession("AB_Image_ABWindow", value);
            }
        }      
        public List<Party_Info> PartyList
        {
            get { return (List<Party_Info>)GetBaseSession("PartyList_ABWindow"); }
            set { SetBaseSession("PartyList_ABWindow", value); }
        }      
        public List<DbOperations.Return_StateList> StateList_AB
        {
            get { return (List<DbOperations.Return_StateList>)GetBaseSession("StateList_ABWindow"); }
            set { SetBaseSession("StateList_ABWindow", value); }
        }      
        public List<Country_INFO> CountryList_ABSmall
        {
            get { return (List<Country_INFO>)GetBaseSession("CountryList_ABSmall"); }
            set { SetBaseSession("CountryList_ABSmall", value); }
        }
        public List<DbOperations.Return_StateList> StateList_ABSmall
        {
            get { return (List<DbOperations.Return_StateList>)GetBaseSession("StateList_ABSmall"); }
            set { SetBaseSession("StateList_ABSmall", value); }
        }
        public List<DbOperations.Return_DistrictList> DistrictList_ABSmall
        {
            get { return (List<DbOperations.Return_DistrictList>)GetBaseSession("DistrictList_ABSmall"); }
            set { SetBaseSession("DistrictList_ABSmall", value); }
        }
        public List<DbOperations.Return_CityList> CityList_ABSmall
        {
            get { return (List<DbOperations.Return_CityList>)GetBaseSession("CityList_ABSmall"); }
            set { SetBaseSession("CityList_ABSmall", value); }
        }
        #endregion
        public ActionResult Frm_Address_Info(string PopUpId = null, string PartyRefreshFunction = "")
        {
            AdresBook_user_rights();
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            if (CheckRights(BASE, ClientScreen.Facility_AddressBook, "List"))
            {
                ViewBag.PopUpId = PopUpId != null ? PopUpId : null;
                ViewBag.PartyRefreshFunction = PartyRefreshFunction;
                //ViewBag.ShowHorizontalBar = 0;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_AddressBook).ToString()) ? 1 : 0;
              
               bool AddressBook_Auto_Vouching_Mode = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                bool ShowAttachmentIndicator = AddressBook_Auto_Vouching_Mode;
                bool ShowVouchingIndicator = AddressBook_Auto_Vouching_Mode;

                //DataTable TP_Table = BASE._Address_DBOps.GetList();
                DataTable TP_Table = BASE._Address_DBOps.GetAddressBookListing(ShowAttachmentIndicator, ShowVouchingIndicator);
                List<DataRow> list = TP_Table.AsEnumerable().ToList();

                List<Address_Book> addressBook = new List<Address_Book>();
                foreach (DataRow row in TP_Table.Rows)
                {
                    Address_Book newdata = new Address_Book();

                    newdata.ID = row["ID"].ToString();
                    newdata.Title = row["Title"].ToString();
                    newdata.Name = row["Name"].ToString();
                    newdata.Organization = row["Organization"].ToString();
                    newdata.Designation = row["Designation"].ToString();//Redmine Bug #131387 fixed
                    newdata.Occupation = row["Occupation"].ToString();
                    newdata.Education = row["Education"].ToString();
                    newdata.PinCode = row["PinCode"].ToString();
                    newdata.City = row["City"].ToString();
                    newdata.State = row["State"].ToString();
                    newdata.Country = row["Country"].ToString();
                    newdata.Passport_No = row["Passport No."].ToString();
                    newdata.PAN_No = row["PAN No."].ToString();
                    newdata.VAT_TIN_No = row["VAT TIN No."].ToString();
                    newdata.CST_TIN_No = row["CST TIN No."].ToString();
                    newdata.GST_TIN_No = row["GSTIN No."].ToString();
                    newdata.TAN_No = row["TAN No."].ToString();
                    newdata.UID_No = row["UID No."].ToString();
                    newdata.Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString();
                    newdata.Voter_ID = row["VOTER ID No."].ToString();
                    newdata.Ration_Card_No = row["Ration Card No."].ToString();
                    newdata.DL_No = row["DL No."].ToString();
                    newdata.Taxpayer_ID = row["Tax ID No."].ToString();
                    newdata.Address_Line1 = row["Address Line.1"].ToString();
                    newdata.Address_Line2 = row["Address Line.2"].ToString();
                    newdata.Address_Line3 = row["Address Line.3"].ToString();
                    newdata.Address_Line4 = row["Address Line.4"].ToString();
                    newdata.District = row["District"].ToString();
                    newdata.Resi_Tel_No = row["Resi.Tel.No(s)"].ToString();
                    newdata.Office_Tel_No = row["Office Tel.No(s)"].ToString();
                    newdata.Office_Fax_No = row["Office Fax No(s)"].ToString();
                    newdata.Resi_Fax_No = row["Resi.Fax No(s)"].ToString();
                    newdata.Mobile_No = row["Mobile No(s)"].ToString();
                    newdata.Email = row["Email"].ToString();
                    newdata.Website = row["Website"].ToString();
                    newdata.Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]);
                    newdata.Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]);
                    newdata.Blood_Group = row["Blood Group"].ToString();
                    newdata.Status = row["Status"].ToString();
                    newdata.BK_Title = row["BK Title"].ToString();
                    newdata.BK_PAD_No = row["BK PAD No."].ToString();
                    newdata.Class_At = row["Class At"].ToString();
                    newdata.Centre_Category = row["Centre Category"].ToString();
                    newdata.Centre_Name = row["Centre Name"].ToString();
                    newdata.Category = row["Category"].ToString();
                    newdata.Referene = row["Referene"].ToString();
                    newdata.Remarks = row["Remarks"].ToString();
                    newdata.Events = row["Events"].ToString();
                    newdata.Add_By = row["Add By"].ToString();
                    newdata.Add_Date = row["Add Date"].ToString();
                    newdata.Edit_By = row["Edit By"].ToString();
                    newdata.Edit_Date = row["Edit Date"].ToString();
                    newdata.Action_Status = row["Action Status"].ToString();
                    newdata.Action_By = row["Action By"].ToString();
                    newdata.Action_Date = row["Action Date"].ToString();
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                    newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                    newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    addressBook.Add(newdata);
                }                
                //addressBook = (from row in TP_Table.AsEnumerable()
                //               select new Address_Book
                //               {
                //                   ID = row["ID"].ToString(),
                //                   Title = row["Title"].ToString(),
                //                   Name = row["Name"].ToString(),
                //                   Organization = row["Organization"].ToString(),
                //                   Designation = row["Designation"].ToString(),//Redmine Bug #131387 fixed
                //                   Education = row["Education"].ToString(),
                //                   PinCode = row["PinCode"].ToString(),
                //                   City = row["City"].ToString(),
                //                   State = row["State"].ToString(),
                //                   Country = row["Country"].ToString(),
                //                   Passport_No = row["Passport No."].ToString(),
                //                   PAN_No = row["PAN No."].ToString(),
                //                   VAT_TIN_No = row["VAT TIN No."].ToString(),
                //                   CST_TIN_No = row["CST TIN No."].ToString(),
                //                   GST_TIN_No = row["GSTIN No."].ToString(),
                //                   TAN_No = row["TAN No."].ToString(),
                //                   UID_No = row["UID No."].ToString(),
                //                   Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString(),
                //                   Address_Line1 = row["Address Line.1"].ToString(),
                //                   Address_Line2 = row["Address Line.2"].ToString(),
                //                   Address_Line3 = row["Address Line.3"].ToString(),
                //                   Address_Line4 = row["Address Line.4"].ToString(),
                //                   District = row["District"].ToString(),
                //                   Resi_Tel_No = row["Resi.Tel.No(s)"].ToString(),
                //                   Office_Tel_No = row["Office Tel.No(s)"].ToString(),
                //                   Office_Fax_No = row["Office Fax No(s)"].ToString(),
                //                   Resi_Fax_No = row["Resi.Fax No(s)"].ToString(),
                //                   Mobile_No = row["Mobile No(s)"].ToString(),
                //                   Email = row["Email"].ToString(),
                //                   Website = row["Website"].ToString(),
                //                   Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]),
                //                   Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]),
                //                   Blood_Group = row["Blood Group"].ToString(),
                //                   Status = row["Status"].ToString(),
                //                   BK_Title = row["BK Title"].ToString(),
                //                   BK_PAD_No = row["BK PAD No."].ToString(),
                //                   Class_At = row["Class At"].ToString(),
                //                   Centre_Category = row["Centre Category"].ToString(),
                //                   Centre_Name = row["Centre Name"].ToString(),
                //                   Category = row["Category"].ToString(),
                //                   Referene = row["Referene"].ToString(),
                //                   Remarks = row["Remarks"].ToString(),
                //                   Events = row["Events"].ToString(),
                //                   Add_By = row["Add By"].ToString(),
                //                   Add_Date = row["Add Date"].ToString(),
                //                   Edit_By = row["Edit By"].ToString(),
                //                   Edit_Date = row["Edit Date"].ToString(),
                //                   Action_Status = row["Action Status"].ToString(),
                //                   Action_By = row["Action By"].ToString(),
                //                   Action_Date = row["Action Date"].ToString(),
                //                   REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]),
                //                   COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]),
                //                   RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]),
                //                   REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]),
                //                   OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]),
                //                   ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]),

                //               }).ToList();
                var Final_Data = addressBook.ToList();
                Addressbook_ExportData = Final_Data;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["AddressBook_Auto_Vouching_Mode"] = AddressBook_Auto_Vouching_Mode;
                return View(Final_Data);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopUpId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopUpId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
        }
        public PartialViewResult Frm_Address_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",string RowKeyToFocus="", bool ShowAttachmentIndicator = false, bool ShowVouchingIndicator = false)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            AdresBook_user_rights();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (Addressbook_ExportData == null || command == "REFRESH")
            {
                //DataTable TP_Table = BASE._Address_DBOps.GetList();
                DataTable TP_Table = BASE._Address_DBOps.GetAddressBookListing(ShowAttachmentIndicator, ShowVouchingIndicator);
                List<Address_Book> addressBook = new List<Address_Book>();

                foreach (DataRow row in TP_Table.Rows)
                {
                    Address_Book newdata = new Address_Book();

                    newdata.ID = row["ID"].ToString();
                    newdata.Title = row["Title"].ToString();
                    newdata.Name = row["Name"].ToString();
                    newdata.Organization = row["Organization"].ToString();
                    newdata.Designation = row["Designation"].ToString();//Redmine Bug #131387 fixed
                    newdata.Education = row["Education"].ToString();
                    newdata.PinCode = row["PinCode"].ToString();
                    newdata.City = row["City"].ToString();
                    newdata.State = row["State"].ToString();
                    newdata.Country = row["Country"].ToString();
                    newdata.Passport_No = row["Passport No."].ToString();
                    newdata.PAN_No = row["PAN No."].ToString();
                    newdata.VAT_TIN_No = row["VAT TIN No."].ToString();
                    newdata.CST_TIN_No = row["CST TIN No."].ToString();
                    newdata.GST_TIN_No = row["GSTIN No."].ToString();
                    newdata.TAN_No = row["TAN No."].ToString();
                    newdata.UID_No = row["UID No."].ToString();
                    newdata.Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString();
                    newdata.Voter_ID = row["VOTER ID No."].ToString();
                    newdata.Ration_Card_No = row["Ration Card No."].ToString();
                    newdata.DL_No = row["DL No."].ToString();
                    newdata.Taxpayer_ID = row["Tax ID No."].ToString();
                    newdata.Address_Line1 = row["Address Line.1"].ToString();
                    newdata.Address_Line2 = row["Address Line.2"].ToString();
                    newdata.Address_Line3 = row["Address Line.3"].ToString();
                    newdata.Address_Line4 = row["Address Line.4"].ToString();
                    newdata.District = row["District"].ToString();
                    newdata.Resi_Tel_No = row["Resi.Tel.No(s)"].ToString();
                    newdata.Office_Tel_No = row["Office Tel.No(s)"].ToString();
                    newdata.Office_Fax_No = row["Office Fax No(s)"].ToString();
                    newdata.Resi_Fax_No = row["Resi.Fax No(s)"].ToString();
                    newdata.Mobile_No = row["Mobile No(s)"].ToString();
                    newdata.Email = row["Email"].ToString();
                    newdata.Website = row["Website"].ToString();
                    newdata.Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]);
                    newdata.Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]);
                    newdata.Blood_Group = row["Blood Group"].ToString();
                    newdata.Status = row["Status"].ToString();
                    newdata.BK_Title = row["BK Title"].ToString();
                    newdata.BK_PAD_No = row["BK PAD No."].ToString();
                    newdata.Class_At = row["Class At"].ToString();
                    newdata.Centre_Category = row["Centre Category"].ToString();
                    newdata.Centre_Name = row["Centre Name"].ToString();
                    newdata.Category = row["Category"].ToString();
                    newdata.Referene = row["Referene"].ToString();
                    newdata.Remarks = row["Remarks"].ToString();
                    newdata.Events = row["Events"].ToString();
                    newdata.Add_By = row["Add By"].ToString();
                    newdata.Add_Date = row["Add Date"].ToString();
                    newdata.Edit_By = row["Edit By"].ToString();
                    newdata.Edit_Date = row["Edit Date"].ToString();
                    newdata.Action_Status = row["Action Status"].ToString();
                    newdata.Action_By = row["Action By"].ToString();
                    newdata.Action_Date = row["Action Date"].ToString();
                    newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                    newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                    newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                    newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                    newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                    newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");

                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    addressBook.Add(newdata);
                }
                //addressBook = (from DataRow row in TP_Table.Rows

                //               select new Address_Book
                //               {
                //                   ID = row["ID"].ToString(),
                //                   Title = row["Title"].ToString(),
                //                   Name = row["Name"].ToString(),
                //                   Organization = row["Organization"].ToString(),
                //                   Designation = row["Designation"].ToString(),//Redmine Bug #131387 fixed
                //                   Education = row["Education"].ToString(),
                //                   PinCode = row["PinCode"].ToString(),
                //                   City = row["City"].ToString(),
                //                   State = row["State"].ToString(),
                //                   Country = row["Country"].ToString(),
                //                   Passport_No = row["Passport No."].ToString(),
                //                   PAN_No = row["PAN No."].ToString(),
                //                   VAT_TIN_No = row["VAT TIN No."].ToString(),
                //                   GST_TIN_No = row["GSTIN No."].ToString(),
                //                   CST_TIN_No = row["CST TIN No."].ToString(),
                //                   TAN_No = row["TAN No."].ToString(),
                //                   UID_No = row["UID No."].ToString(),
                //                   Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString(),
                //                   Address_Line1 = row["Address Line.1"].ToString(),
                //                   Address_Line2 = row["Address Line.2"].ToString(),
                //                   Address_Line3 = row["Address Line.3"].ToString(),
                //                   Address_Line4 = row["Address Line.4"].ToString(),
                //                   District = row["District"].ToString(),
                //                   Resi_Tel_No = row["Resi.Tel.No(s)"].ToString(),
                //                   Office_Tel_No = row["Office Tel.No(s)"].ToString(),
                //                   Office_Fax_No = row["Office Fax No(s)"].ToString(),
                //                   Resi_Fax_No = row["Resi.Fax No(s)"].ToString(),
                //                   Mobile_No = row["Mobile No(s)"].ToString(),
                //                   Email = row["Email"].ToString(),
                //                   Website = row["Website"].ToString(),
                //                   Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]),
                //                   Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]),
                //                   Blood_Group = row["Blood Group"].ToString(),
                //                   Status = row["Status"].ToString(),
                //                   BK_Title = row["BK Title"].ToString(),
                //                   BK_PAD_No = row["BK PAD No."].ToString(),
                //                   Class_At = row["Class At"].ToString(),
                //                   Centre_Category = row["Centre Category"].ToString(),
                //                   Centre_Name = row["Centre Name"].ToString(),
                //                   Category = row["Category"].ToString(),
                //                   Referene = row["Referene"].ToString(),
                //                   Remarks = row["Remarks"].ToString(),
                //                   Events = row["Events"].ToString(),
                //                   Add_By = row["Add By"].ToString(),
                //                   Add_Date = row["Add Date"].ToString(),
                //                   Edit_By = row["Edit By"].ToString(),
                //                   Edit_Date = row["Edit Date"].ToString(),
                //                   Action_Status = row["Action Status"].ToString(),
                //                   Action_By = row["Action By"].ToString(),
                //                   Action_Date = row["Action Date"].ToString(),
                //                   REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]),
                //                   COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]),
                //                   RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]),
                //                   REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]),
                //                   OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]),
                //                   ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]),

                //               }).ToList();

                var FinalData = addressBook.ToList();
                Addressbook_ExportData = FinalData;
            }
            return PartialView(Addressbook_ExportData);
        }
        public ActionResult AddressBook_GetGridData(string ID)
        {
            string itstr = "";
            if (ID != null)
            {
                var actionItems = Addressbook_ExportData.AsEnumerable().Where(f => f.ID == ID).FirstOrDefault();
                if (actionItems != null)
                {
                    itstr = actionItems.ID + "![" + actionItems.Add_By + "![" + actionItems.Add_Date + "![" + actionItems.Edit_By + "![" + actionItems.Edit_Date + "![" + actionItems.Action_Status + "![" + actionItems.Action_By + "![" + actionItems.Action_Date;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_AddressBook, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('AddressBook_report_modal','Not Allowed','No Rights');$('#AddressModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #region <--NestedGrid-->
        public ActionResult Frm_Address_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.AddressBookInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.AddressBookInfo_RecID = RecID;
            ViewBag.AddressBookInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_AddressBook);
                    AddressBookInfo_DetailGrid_Data = _docList;
                    Session["AddressBookInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Facility_AddressBook);
                    AddressBookInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["AddressBookInfo_detailGrid_Data"] = data.DocumentMapping;
                    AddressBookInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(AddressBookInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(AddressBookInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "AddressListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "AddressListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["AddressBookInfo_detailGrid_Data"];
        }
        #endregion // <--NestedGrid-->
        #region "devextreme"
        public ActionResult Frm_Address_Info_dx(string PopUpId = null, string PartyRefreshFunction = "")
        {
            AdresBook_user_rights();
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            if (CheckRights(BASE, ClientScreen.Facility_AddressBook, "List"))
            {
                ViewBag.PopUpId = PopUpId != null ? PopUpId : null;
                ViewBag.PartyRefreshFunction = PartyRefreshFunction;               
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_AddressBook).ToString()) ? 1 : 0;
                bool AddressBook_Auto_Vouching_Mode = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;                    
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["AddressBook_Auto_Vouching_Mode"] = AddressBook_Auto_Vouching_Mode;
               // Addressbook_ExportData = new List<Address_Book>();
                return View();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(PopUpId))
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');HidePopup('" + PopUpId + "')</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
                }
            }
        }
        public ActionResult Frm_Address_Info_GridData_dx(bool ShowAttachmentIndicator,bool ShowVouchingIndicator)
        {
            DataTable TP_Table = BASE._Address_DBOps.GetAddressBookListing(ShowAttachmentIndicator, ShowVouchingIndicator);     
            List<Address_Book> addressBook = new List<Address_Book>();

            foreach (DataRow row in TP_Table.Rows)
            {
                Address_Book newdata = new Address_Book();

                newdata.ID = row["ID"].ToString();
                newdata.Title = row["Title"].ToString();
                newdata.Name = row["Name"].ToString();
                newdata.Organization = row["Organization"].ToString();
                newdata.Designation = row["Designation"].ToString();//Redmine Bug #131387 fixed
                newdata.Occupation = row["Occupation"].ToString();
                newdata.Education = row["Education"].ToString();
                newdata.PinCode = row["PinCode"].ToString();
                newdata.City = row["City"].ToString();
                newdata.State = row["State"].ToString();
                newdata.Country = row["Country"].ToString();
                newdata.Passport_No = row["Passport No."].ToString();
                newdata.PAN_No = row["PAN No."].ToString();
                newdata.VAT_TIN_No = row["VAT TIN No."].ToString();
                newdata.CST_TIN_No = row["CST TIN No."].ToString();
                newdata.GST_TIN_No = row["GSTIN No."].ToString();
                newdata.TAN_No = row["TAN No."].ToString();
                newdata.UID_No = row["UID No."].ToString();
                newdata.Service_Tax_Reg_No = row["Service Tax Reg. No."].ToString();
                newdata.Voter_ID = row["VOTER ID No."].ToString();
                newdata.Ration_Card_No = row["Ration Card No."].ToString();
                newdata.DL_No = row["DL No."].ToString();
                newdata.Taxpayer_ID = row["Tax ID No."].ToString();
                newdata.Address_Line1 = row["Address Line.1"].ToString();
                newdata.Address_Line2 = row["Address Line.2"].ToString();
                newdata.Address_Line3 = row["Address Line.3"].ToString();
                newdata.Address_Line4 = row["Address Line.4"].ToString();
                newdata.District = row["District"].ToString();
                newdata.Resi_Tel_No = row["Resi.Tel.No(s)"].ToString();
                newdata.Office_Tel_No = row["Office Tel.No(s)"].ToString();
                newdata.Office_Fax_No = row["Office Fax No(s)"].ToString();
                newdata.Resi_Fax_No = row["Resi.Fax No(s)"].ToString();
                newdata.Mobile_No = row["Mobile No(s)"].ToString();
                newdata.Email = row["Email"].ToString();
                newdata.Website = row["Website"].ToString();
                newdata.Date_of_Birth_Lokik = Convert.IsDBNull(row["Date of Birth (Lokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birth (Lokik)"]);
                newdata.Date_of_Birth_Alokik = Convert.IsDBNull(row["Date of Birht (Alokik)"]) ? (DateTime?)null : Convert.ToDateTime(row["Date of Birht (Alokik)"]);
                newdata.Blood_Group = row["Blood Group"].ToString();
                newdata.Status = row["Status"].ToString();
                newdata.BK_Title = row["BK Title"].ToString();
                newdata.BK_PAD_No = row["BK PAD No."].ToString();
                newdata.Class_At = row["Class At"].ToString();
                newdata.Centre_Category = row["Centre Category"].ToString();
                newdata.Centre_Name = row["Centre Name"].ToString();
                newdata.Category = row["Category"].ToString();
                newdata.Referene = row["Referene"].ToString();
                newdata.Remarks = row["Remarks"].ToString();
                newdata.Events = row["Events"].ToString();
                newdata.Add_By = row["Add By"].ToString();
                newdata.Add_Date = row["Add Date"].ToString();
                newdata.Edit_By = row["Edit By"].ToString();
                newdata.Edit_Date = row["Edit Date"].ToString();
                newdata.Action_Status = row["Action Status"].ToString();
                newdata.Action_By = row["Action By"].ToString();
                newdata.Action_Date = row["Action Date"].ToString();
                newdata.REQ_ATTACH_COUNT = Convert.IsDBNull(row["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["REQ_ATTACH_COUNT"]);
                newdata.COMPLETE_ATTACH_COUNT = Convert.IsDBNull(row["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(row["COMPLETE_ATTACH_COUNT"]);
                newdata.RESPONDED_COUNT = Convert.IsDBNull(row["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(row["RESPONDED_COUNT"]);
                newdata.REJECTED_COUNT = Convert.IsDBNull(row["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(row["REJECTED_COUNT"]);
                newdata.OTHER_ATTACH_CNT = Convert.IsDBNull(row["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["OTHER_ATTACH_CNT"]);
                newdata.ALL_ATTACH_CNT = Convert.IsDBNull(row["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(row["ALL_ATTACH_CNT"]);
                newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                newdata.iIcon = "";

                if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newdata.iIcon += "GreenShield|";
                }
                else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newdata.iIcon += "YellowShield|";
                }
                else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newdata.iIcon += "BlueShield|";
                }
                if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newdata.iIcon += "RedFlag|";
                }
                if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newdata.iIcon += "RequiredAttachment|";
                }
                else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newdata.iIcon += "AdditionalAttachment|";
                }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                addressBook.Add(newdata);
            }
            //Addressbook_ExportData = addressBook;
            return Content(JsonConvert.SerializeObject(addressBook), "application/json");
        }
        public ActionResult Frm_Address_Info_GridData_detail_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Facility_AddressBook, !VouchingMode)), "application/json");
        }
        public FormDataCollection ConvertJsonToFormDataCollection(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);            
            var formData = new FormDataCollection(jsonObject.ToObject<Dictionary<string, string>>());
            return formData;
        }

        public ActionResult Frm_Address_Info_SaveEdited_GridData_dx(string value, string key=null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            var jsonObject = JObject.Parse(value);
            var formData = ConvertJsonToFormDataCollection(value);
            string ID = formData.Get("ID");
            Address_Book_ViewModel model = new Address_Book_ViewModel();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_Edit");
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            Get_Master_Data(ref model);
            model.Organization_DS = Get_Org_List();
            LookUp_GetCountryList(ref model);
            model.Rad_Gender_AB = "MALE";
            model.Com_BloodGroup_AB = "None";
            model.Rad_Status_AB = "Wellwisher";
            model.Cmb_Cen_Cagetory_AB = 0;
            model.Rad_Acc_Party_AB = "0";// redmine Bug #133476 fixed       
            if (Tag == Common.Navigation_Mode._Edit)
            {
                #region Data_Binding
                DataTable d1 = BASE._Address_DBOps.GetRecord(ID);                 
                DataTable dMag = BASE._Address_DBOps.GetMagazineRecords(ID);
                DataTable dWing = BASE._Address_DBOps.GetWingRecords(ID);
                DataTable dSpecial = BASE._Address_DBOps.GetSpecialityRecords(ID);
                DataTable dEvents = BASE._Address_DBOps.GetEventRecords(ID);
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Org_RecID = Convert.ToString(d1.Rows[0]["C_ORG_REC_ID"]);

                model.Look_TitleList_AB = Convert.ToString(d1.Rows[0]["C_TITLE"]);
                model.Txt_Name_AB = Convert.ToString(d1.Rows[0]["C_NAME"]);
                model.Rad_Gender_AB = Convert.ToString(d1.Rows[0]["C_GENDER"]);
                model.Txt_OrgName_AB = Convert.ToString(d1.Rows[0]["C_ORG_NAME"]);
                model.Txt_Desig_AB = Convert.ToString(d1.Rows[0]["C_DESIGNATION"]);
                model.Txt_Education_AB = Convert.ToString(d1.Rows[0]["C_EDUCATION"]);
                if (!Convert.IsDBNull(d1.Rows[0]["C_OCCUPATION_ID"]))
                {
                    model.Look_OccupationList_AB = Convert.ToString(d1.Rows[0]["C_OCCUPATION_ID"]);
                }
                model.Txt_Ref_AB = Convert.ToString(d1.Rows[0]["C_REF"]);
                model.Txt_Remark1_AB = Convert.ToString(d1.Rows[0]["C_REMARKS_1"]);
                model.Txt_Remark2_AB = Convert.ToString(d1.Rows[0]["C_REMARKS_2"]);
                byte[] byteBLOBData;
                if (!Convert.IsDBNull(d1.Rows[0]["C_PHOTO"]))
                {
                    byteBLOBData = ((byte[])(d1.Rows[0]["C_PHOTO"]));
                    AB_Image = byteBLOBData;
                }
                // 2
                model.Txt_R_Add1_AB = Convert.ToString(d1.Rows[0]["C_R_ADD1"]);
                model.Txt_R_Add2_AB = Convert.ToString(d1.Rows[0]["C_R_ADD2"]);
                model.Txt_R_Add3_AB = Convert.ToString(d1.Rows[0]["C_R_ADD3"]);
                model.Txt_R_Add4_AB = Convert.ToString(d1.Rows[0]["C_R_ADD4"]);
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_COUNTRY_ID"]))
                {
                    if (d1.Rows[0]["C_R_COUNTRY_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RCountryList_AB = Convert.ToString(d1.Rows[0]["C_R_COUNTRY_ID"]);
                        //if (model.GLookUp_RCountryList_AB != "f9970249-121c-4b8f-86f9-2b53e850809e")
                        //{
                        //    RefreshCityList_AB(model.GLookUp_RCountryList_AB, CountrycodeRes, "");
                        //}
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_STATE_ID"]))
                {
                    if (d1.Rows[0]["C_R_STATE_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RStateList_AB = Convert.ToString(d1.Rows[0]["C_R_STATE_ID"]);                
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_DISTRICT_ID"]))
                {
                    if ((d1.Rows[0]["C_R_DISTRICT_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_RDistrictList_AB = Convert.ToString(d1.Rows[0]["C_R_DISTRICT_ID"]);
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_CITY_ID"]))
                {
                    if ((d1.Rows[0]["C_R_CITY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_RCityList_AB = Convert.ToString(d1.Rows[0]["C_R_CITY_ID"]);
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_PINCODE"]))
                {
                    model.Txt_R_Pincode_AB = Convert.ToString(d1.Rows[0]["C_R_PINCODE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_ACC_PARTY"]))
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(d1.Rows[0]["C_ACC_PARTY"])))
                    {
                        if ((bool)d1.Rows[0]["C_ACC_PARTY"])
                        {
                            model.Rad_Acc_Party_AB = "1";
                        }
                        else
                        {
                            model.Rad_Acc_Party_AB = "0";
                        }
                    }
                    else
                    {
                        model.Rad_Acc_Party_AB = "0";
                    }
                }
                else
                {
                    model.Rad_Acc_Party_AB = "0";
                }

                // 
                model.Txt_O_Add1_AB = Convert.ToString(d1.Rows[0]["C_O_ADD1"]);
                model.Txt_O_Add2_AB = Convert.ToString(d1.Rows[0]["C_O_ADD2"]);
                model.Txt_O_Add3_AB = Convert.ToString(d1.Rows[0]["C_O_ADD3"]);
                model.Txt_O_Add4_AB = Convert.ToString(d1.Rows[0]["C_O_ADD4"]);

                if (!Convert.IsDBNull(d1.Rows[0]["C_O_COUNTRY_ID"]))
                {
                    if ((d1.Rows[0]["C_O_COUNTRY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OCountryList_AB = Convert.ToString(d1.Rows[0]["C_O_COUNTRY_ID"]);
                        //if (d1.Rows[0]["C_O_COUNTRY_ID"].ToString() != "f9970249-121c-4b8f-86f9-2b53e850809e")
                        //{
                        //    RefreshCityList_AB(model.GLookUp_OCountryList_AB, CountrycodeRes, "");
                        //}
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_STATE_ID"]))
                {
                    if ((d1.Rows[0]["C_O_STATE_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OStateList_AB = Convert.ToString(d1.Rows[0]["C_O_STATE_ID"]);                        
                    }
                }

                if (!Convert.IsDBNull(d1.Rows[0]["C_O_DISTRICT_ID"]))
                {
                    if ((d1.Rows[0]["C_O_DISTRICT_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_ODistrictList_AB = Convert.ToString(d1.Rows[0]["C_O_DISTRICT_ID"]);

                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_CITY_ID"]))
                {
                    if ((d1.Rows[0]["C_O_CITY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OCityList_AB = Convert.ToString(d1.Rows[0]["C_O_CITY_ID"]);

                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_PINCODE"]))
                {
                    model.Txt_O_Pincode_AB = Convert.ToString(d1.Rows[0]["C_O_PINCODE"]);
                }

                // 3
                model.Txt_R_Tel_1_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_R_1"]);
                model.Txt_R_Tel_2_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_R_2"]);
                model.Txt_R_Fax_1_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_R_1"]);
                model.Txt_R_Fax_2_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_R_2"]);

                model.Txt_O_Tel_1_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_O_1"]);
                model.Txt_O_Tel_2_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_O_2"]);
                model.Txt_O_Fax_1_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_O_1"]);
                model.Txt_O_Fax_2_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_O_2"]);

                model.Txt_Mob_1_AB = Convert.ToString(d1.Rows[0]["C_MOB_NO_1"]);
                model.Txt_Mob_2_AB = Convert.ToString(d1.Rows[0]["C_MOB_NO_2"]);

                model.Txt_Email1_AB = Convert.ToString(d1.Rows[0]["C_EMAIL_ID_1"]);
                model.Txt_Email2_AB = Convert.ToString(d1.Rows[0]["C_EMAIL_ID_2"]);

                model.Txt_Website_AB = Convert.ToString(d1.Rows[0]["C_WEBSITE"]);
                model.Txt_Skype_AB = Convert.ToString(d1.Rows[0]["C_SKYPE_ID"]);
                model.Txt_Facebook_AB = Convert.ToString(d1.Rows[0]["C_FACEBOOK_ID"]);
                model.Txt_Twitter_AB = Convert.ToString(d1.Rows[0]["C_TWITTER_ID"]);
                model.Txt_GTalk_AB = Convert.ToString(d1.Rows[0]["C_GTALK_ID"]);

                // 4              
                if (!Convert.IsDBNull(d1.Rows[0]["C_DOB_L"]))
                {
                    model.DE_DOB_L_AB = Convert.ToDateTime(d1.Rows[0]["C_DOB_L"]);
                }
                else
                {
                    model.DE_DOB_L_AB = null;
                }

                model.Com_BloodGroup_AB = Convert.ToString(d1.Rows[0]["C_BLOODGROUP"]);

                if (!Convert.IsDBNull(d1.Rows[0]["C_CONTACT_MODE_ID"]))
                {
                    model.Look_ConModeList_AB = Convert.ToString(d1.Rows[0]["C_CONTACT_MODE_ID"]);
                }

                model.Txt_PAN_No_AB = Convert.ToString(d1.Rows[0]["C_PAN_NO"]);
                model.Txt_VAT_TIN_AB = d1.Rows[0]["C_VAT_TIN_NO"].ToString();
                model.GST_TIN = d1.Rows[0]["C_GST_TIN_NO"].ToString();
                model.Txt_CST_TIN_AB = d1.Rows[0]["C_CST_TIN_NO"].ToString();
                model.Txt_TAN_AB = Convert.ToString(d1.Rows[0]["C_TAN_NO"]);
                model.Txt_UID_AB = Convert.ToString(d1.Rows[0]["C_UID_NO"]);
                model.Txt_STR_AB = Convert.ToString(d1.Rows[0]["C_STR_NO"]);
                model.Txt_VoterID_AB = Convert.ToString(d1.Rows[0]["C_VOTER_ID_NO"]);
                model.Txt_RationCardNo_AB = Convert.ToString(d1.Rows[0]["C_RATION_NO"]);
                model.Txt_DLNo_AB = Convert.ToString(d1.Rows[0]["C_DL_NO"]);
                model.Txt_TaxpayerID_AB = Convert.ToString(d1.Rows[0]["C_TAX_ID_NO"]);
                if (!Convert.IsDBNull(d1.Rows[0]["C_PASSPORT_NO"]))
                {
                    model.Txt_Passport_No_AB = Convert.ToString(d1.Rows[0]["C_PASSPORT_NO"]);
                }
                model.Magazine = new List<Models.Magazine>();
                foreach (DataRow currRow in dMag.Rows)
                {
                    model.Magazine.Add(new Models.Magazine() { Magazine_Misc_ID = currRow["C_MISC_REC_ID"].ToString(), Selected = true });
                }
                // 5
                //XtraTabControl1.SelectedTabPage = Page_Status;
                // 
                model.Rad_Status_AB = Convert.ToString(d1.Rows[0]["C_STATUS"]);
                // Status
                if ((model.Rad_Status_AB.ToLower() == "wellwisher"))
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CON_DT"]))
                    {
                        model.DE_DOC_AB = Convert.ToDateTime(d1.Rows[0]["C_CON_DT"]);
                    }
                    else
                    {
                        model.DE_DOC_AB = null;
                    }
                }
                else
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["C_DOB_A"]))
                    {
                        // Alokik DoB
                        model.DE_DOB_A_AB = Convert.ToDateTime(d1.Rows[0]["C_DOB_A"]);
                    }
                    else
                    {
                        model.DE_DOB_A_AB = null;
                    }
                    // BK Title
                    model.Com_BK_Title_AB = Convert.ToString(d1.Rows[0]["C_BK_TITLE"]);
                    // BK PAd NO :text
                    model.Txt_PAD_No_AB = Convert.ToString(d1.Rows[0]["C_BK_PAD_NO"]);
                    model.Com_Class_At_AB = Convert.ToString(d1.Rows[0]["C_CLASS_AT"]);
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CEN_CATEGORY"]))
                    {
                        model.Cmb_Cen_Cagetory_AB = Convert.ToInt32(d1.Rows[0]["C_CEN_CATEGORY"]);
                    }
                    else
                    {
                        model.Cmb_Cen_Cagetory_AB = 0;
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CLASS_CEN_ID"]))
                    {
                        model.GLookUp_Cen_List_AB = Convert.ToString(d1.Rows[0]["C_CLASS_CEN_ID"]);
                    }
                    model.Txt_Class_Add_AB = Convert.ToString(d1.Rows[0]["C_CLASS_ADD1"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_CATEGORY"]))
                {
                    model.Look_CategoryList_AB = Convert.ToString(d1.Rows[0]["C_CATEGORY"]);
                }
                // 6
                //XtraTabControl1.SelectedTabPage = Page_Special;
                // Wings
                model.WingsMember = new List<WingsMember>();
                foreach (DataRow currRow in dWing.Rows)
                {
                    model.WingsMember.Add(new WingsMember() { Wings_Misc_ID = currRow["C_WING_ID"].ToString(), Selected = true });
                }
                // Specialities
                model.Specialities = new List<Specialitites>();
                foreach (DataRow currRow in dSpecial.Rows)
                {
                    model.Specialities.Add(new Specialitites() { Specialities_Misc_ID = currRow["C_MISC_REC_ID"].ToString(), Selected = true });
                }
                // Events
                model.Events = new List<Events>();
                foreach (DataRow currRow in dEvents.Rows)
                {
                    model.Events.Add(new Events() { Events_Misc_ID = currRow["C_MISC_REC_ID"].ToString(), Selected = true });
                }
                #endregion
            }
            if (model.Rad_Status_AB.ToLower() == "wellwisher")
            {
                model.IsBkReadOnly = true;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Replicate_Confirm"))==false)
            {
                model.Replicate_Confirm = formData.Get("Replicate_Confirm").Equals("False")?false:true;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Occupation"))==false)
            {
                model.Look_OccupationList_AB = formData.Get("Occupation");
            }
            else if(value.Contains("Occupation") && string.IsNullOrWhiteSpace(formData.Get("Occupation")))
            {
                model.Look_OccupationList_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Education"))==false)
            {
                model.Txt_Education_AB = formData.Get("Education");
            }
            else if (value.Contains("Education") && string.IsNullOrWhiteSpace(formData.Get("Education")))
            {
                model.Txt_Education_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Name"))==false)
            {
                model.Txt_Name_AB = formData.Get("Name");
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Country"))==false)
            {
                model.GLookUp_RCountryList_AB = formData.Get("Country");
            }
            else if (value.Contains("Country") && string.IsNullOrWhiteSpace(formData.Get("Country")))
            {
                model.GLookUp_RCountryList_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("City"))==false)
            {
                model.GLookUp_RCityList_AB = formData.Get("City");
            }
            else if (value.Contains("City") && string.IsNullOrWhiteSpace(formData.Get("City")))
            {
                model.GLookUp_RCityList_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("State"))==false)
            {
                model.GLookUp_RStateList_AB = formData.Get("State");
            }
            else if (value.Contains("State") && string.IsNullOrWhiteSpace(formData.Get("State")))
            {
                model.GLookUp_RStateList_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Centre_Name"))==false)
            {
                model.GLookUp_Cen_List_AB = formData.Get("Centre_Name");
            }
            else if (value.Contains("Centre_Name") && string.IsNullOrWhiteSpace(formData.Get("Centre_Name")))
            {
                model.GLookUp_Cen_List_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Centre_Category"))==false)
            {
                model.Cmb_Cen_Cagetory_AB = formData.Get("Centre_Category").Equals("Indian Centre")?0:1;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Status"))==false)
            {
                model.Rad_Status_AB = formData.Get("Status");
            }
            else if (value.Contains("Status") && string.IsNullOrWhiteSpace(formData.Get("Status")))
            {
                model.Rad_Status_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Date_of_Birth_Alokik"))==false)
            {
                DateTime dob;
                if (DateTime.TryParseExact(formData.Get("Date_of_Birth_Alokik"), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    // Format the date string back to ISO 8601 format
                    model.DE_DOB_A_AB = dob;
                    //string isoFormattedLDOBString = dob.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//already in ISO format so no need to isoFormattedLDOBString.ToString(BASE._Server_Date_Format_Long);
                }
                //model.DE_DOB_A_AB = formData.Get("Date_of_Birth_Alokik");
            }
            else if (value.Contains("Date_of_Birth_Alokik") && string.IsNullOrWhiteSpace(formData.Get("Date_of_Birth_Alokik")))
            {
                model.DE_DOB_A_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Date_of_Birth_Lokik"))==false)
            {
                DateTime dob;
                if (DateTime.TryParseExact(formData.Get("Date_of_Birth_Lokik"), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    // Format the date string back to ISO 8601 format
                    model.DE_DOB_L_AB = dob;
                    //string isoFormattedLDOBString = dob.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//already in ISO format so no need to isoFormattedLDOBString.ToString(BASE._Server_Date_Format_Long);
                }
                //model.DE_DOB_A_AB = formData.Get("Date_of_Birth_Alokik");
            }
            else if (value.Contains("Date_of_Birth_Lokik") && string.IsNullOrWhiteSpace(formData.Get("Date_of_Birth_Lokik")))
            {
                model.DE_DOB_L_AB = null;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("PinCode"))==false)
            {
                model.Txt_R_Pincode_AB = formData.Get("PinCode");
            }
            else if (value.Contains("PinCode") && string.IsNullOrWhiteSpace(formData.Get("PinCode")))
            {
                model.Txt_R_Pincode_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("PAN_No"))==false)
            {
                model.Txt_PAN_No_AB = formData.Get("PAN_No");
            }
            else if (value.Contains("PAN_No") && string.IsNullOrWhiteSpace(formData.Get("PAN_No")))
            {
                model.Txt_PAN_No_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Passport_No"))==false)
            {
                model.Txt_Passport_No_AB = formData.Get("Passport_No");
            }
            else if (value.Contains("Passport_No") && string.IsNullOrWhiteSpace(formData.Get("Passport_No")))
            {
                model.Txt_Passport_No_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("UID_No"))==false)
            {
                model.Txt_UID_AB = formData.Get("UID_No");
            }
            else if (value.Contains("UID_No") && string.IsNullOrWhiteSpace(formData.Get("UID_No")))
            {
                model.Txt_UID_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Voter_ID"))==false)
            {
                model.Txt_VoterID_AB = formData.Get("Voter_ID");
            }
            else if (value.Contains("Voter_ID") && string.IsNullOrWhiteSpace(formData.Get("Voter_ID")))
            {
                model.Txt_VoterID_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("DL_No"))==false)
            {
                model.Txt_DLNo_AB = formData.Get("DL_No");
            }
            else if (value.Contains("DL_No") && string.IsNullOrWhiteSpace(formData.Get("DL_No")))
            {
                model.Txt_DLNo_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Address_Line1"))==false)
            {
                model.Txt_R_Add1_AB = formData.Get("Address_Line1");
            }
            else if (value.Contains("Address_Line1") && string.IsNullOrWhiteSpace(formData.Get("Address_Line1")))
            {
                model.Txt_R_Add1_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Address_Line2"))==false)
            {
                model.Txt_R_Add2_AB = formData.Get("Address_Line2");
            }
            else if (value.Contains("Address_Line2") && string.IsNullOrWhiteSpace(formData.Get("Address_Line2")))
            {
                model.Txt_R_Add2_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Address_Line3"))==false)
            {
                model.Txt_R_Add3_AB = formData.Get("Address_Line3");
            }
            else if (value.Contains("Address_Line3") && string.IsNullOrWhiteSpace(formData.Get("Address_Line3")))
            {
                model.Txt_R_Add3_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Address_Line4"))==false)
            {
                model.Txt_R_Add4_AB = formData.Get("Address_Line4");
            }
            else if (value.Contains("Address_Line4") && string.IsNullOrWhiteSpace(formData.Get("Address_Line4")))
            {
                model.Txt_R_Add4_AB = "";
            }
            if (string.IsNullOrWhiteSpace(formData.Get("Edit_Confirm"))== false)
            {
                model.Edit_Confirm = formData.Get("Edit_Confirm").Equals("True") ? true : false;
            }
            if (string.IsNullOrWhiteSpace(formData.Get("BK_Title"))==false)
            {
                model.Com_BK_Title_AB = formData.Get("BK_Title");
            }
            string remarksValue = formData.Get("Remarks");
            if (!string.IsNullOrWhiteSpace(remarksValue))
            {
                string[] parts = remarksValue.Split(new[] { '|' }, 2);
                model.Txt_Remark1_AB = parts.Length >= 1 ? parts[0].Trim() : string.Empty;
                model.Txt_Remark2_AB = parts.Length == 2 ? parts[1].Trim() : string.Empty;
            }                            
            else if(value.Contains("Remarks") &&  string.IsNullOrWhiteSpace(formData.Get("Remarks")))
            {
                model.Txt_Remark1_AB = string.Empty;
                model.Txt_Remark2_AB = string.Empty;
            }
            model.Rec_ID = formData.Get("ID");
            DataTable ServiceUser = BASE._Address_DBOps.GetAddressBookServiceUserDetails(model.Rec_ID);
            if (ServiceUser != null && ServiceUser.Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(model.Txt_Mob_1_AB) == false && model.Txt_Mob_1_AB.Length <= 10)
                {
                    model.Txt_Mob_1_AB = "0" + model.Txt_Mob_1_AB;
                }
                if (string.IsNullOrWhiteSpace(model.Txt_Mob_2_AB) == false && model.Txt_Mob_2_AB.Length <= 10)
                {
                    model.Txt_Mob_2_AB = "0" + model.Txt_Mob_2_AB;
                }
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mobile_No")))
            {
                string[] mobile = formData.Get("Mobile_No").Split(',');
                if (mobile.Length>0)
                {
                    model.Txt_Mob_1_AB = mobile[0];
                    if (mobile.Length == 2) model.Txt_Mob_2_AB = mobile[1];
                    else if (mobile.Length == 1) model.Txt_Mob_2_AB = null;
                }
            }
            else if (value.Contains("Mobile_No") && string.IsNullOrWhiteSpace(formData.Get("Mobile_No")))
            {
                model.Txt_Mob_1_AB = "";
                model.Txt_Mob_2_AB = "";
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email")))
            {
                string []emails = formData.Get("Email").Split(',');
                if (emails.Length > 0)
                {
                    model.Txt_Email1_AB = emails[0];
                    if (emails.Length == 2) model.Txt_Email2_AB = emails[1];
                    if (emails.Length == 1) model.Txt_Email2_AB = null;
                }                
            }
            else if (value.Contains("Email") && string.IsNullOrWhiteSpace(formData.Get("Email")))
            {
                model.Txt_Email1_AB = "";
                model.Txt_Email2_AB = "";
            }
            return Frm_Address_Info_Window(model, null);            
        }
            public ActionResult Frm_Address_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Facility_AddressBook)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if ((!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_AddressBook, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('AddressBook_report_modal','Not Allowed','No Rights');$('#AddressModelListPreview_dx').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        [HttpGet]
        public ActionResult Frm_Address_Info_Window(string PopUpId = "popup_frm_Address_Window", string ButtonID = "AddressModelNew", string ActionMethod = null, string ID = null, string PostSuccessFunction = null, string Party_DataGridID = null, string NewFunctionName = null, string DropDownFunctionName = null, string EditedOn = null)
        {
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add")) && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Update")) && ActionMethod == "Edit")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Delete")) && ActionMethod == "Delete")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if (ActionMethod == "New")
            {
                if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                {
                    if (BASE._CenterDBOps.IsFinalAuditCompleted())
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Party Cannot be added...!<br><br>Final Audit has been Completed for this year.','Information...');</script>");
                    }
                }
            }
            Address_Book_ViewModel model = new Address_Book_ViewModel();
            model.PostSuccessFunction = PostSuccessFunction ?? "AjaxSuccessAddressBook";
            model.PopUpId = PopUpId ?? "popup_frm_Address_Window";
            model.NewFunctionName = NewFunctionName ?? "OnAddressNewClick";
            model.Party_DataGridID = Party_DataGridID;
            model.DropDownFunctionName = DropDownFunctionName;
            model.Rec_ID = ID;
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            if (string.IsNullOrWhiteSpace(EditedOn) == false)
            {
                model.Info_LastEditedOn = Convert.ToDateTime(EditedOn);
            }
            Get_Master_Data(ref model);
            model.Organization_DS = Get_Org_List();
            LookUp_GetCountryList(ref model);
            model.Rad_Gender_AB = "MALE";
            model.Com_BloodGroup_AB = "None";
            model.Rad_Status_AB = "Wellwisher";
            model.Cmb_Cen_Cagetory_AB = 0;
            model.Rad_Acc_Party_AB = "0";// redmine Bug #133476 fixed       
            if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
            {
                #region Data_Binding
                Return_Json_Param jsonParam = new Return_Json_Param();
                DataTable d1 = BASE._Address_DBOps.GetRecord(ID);
                if (d1 == null)
                {
                    jsonParam.message = Messages.RecordChanged("Current Contact");
                    jsonParam.title = "Record Changed / Removed in Background!!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (d1.Rows.Count == 0)
                {
                    jsonParam.message = Messages.RecordChanged("Current Contact");
                    jsonParam.title = "Record Changed / Removed in Background!!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (BASE.AllowMultiuser())
                {
                    if (Tag == Common.Navigation_Mode._Edit)
                    {
                        if (model.Info_LastEditedOn > DateTime.MinValue)
                        {
                            if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Contact");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                DataTable dMag = BASE._Address_DBOps.GetMagazineRecords(ID);
                DataTable dWing = BASE._Address_DBOps.GetWingRecords(ID);
                DataTable dSpecial = BASE._Address_DBOps.GetSpecialityRecords(ID);
                DataTable dEvents = BASE._Address_DBOps.GetEventRecords(ID);
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Org_RecID = Convert.ToString(d1.Rows[0]["C_ORG_REC_ID"]);
                // 1
                model.Look_TitleList_AB = Convert.ToString(d1.Rows[0]["C_TITLE"]);
                model.Txt_Name_AB = Convert.ToString(d1.Rows[0]["C_NAME"]);
                model.Rad_Gender_AB = Convert.ToString(d1.Rows[0]["C_GENDER"]);
                model.Txt_OrgName_AB = Convert.ToString(d1.Rows[0]["C_ORG_NAME"]);
                model.Txt_Desig_AB = Convert.ToString(d1.Rows[0]["C_DESIGNATION"]);
                model.Txt_Education_AB = Convert.ToString(d1.Rows[0]["C_EDUCATION"]);
                if (!Convert.IsDBNull(d1.Rows[0]["C_OCCUPATION_ID"]))
                {
                    model.Look_OccupationList_AB = Convert.ToString(d1.Rows[0]["C_OCCUPATION_ID"]);
                }
                model.Txt_Ref_AB = Convert.ToString(d1.Rows[0]["C_REF"]);
                model.Txt_Remark1_AB = Convert.ToString(d1.Rows[0]["C_REMARKS_1"]);
                model.Txt_Remark2_AB = Convert.ToString(d1.Rows[0]["C_REMARKS_2"]);
                byte[] byteBLOBData;
                if (!Convert.IsDBNull(d1.Rows[0]["C_PHOTO"]))
                {
                    byteBLOBData = ((byte[])(d1.Rows[0]["C_PHOTO"]));
                    AB_Image = byteBLOBData;
                }
                // 2
                model.Txt_R_Add1_AB = Convert.ToString(d1.Rows[0]["C_R_ADD1"]);
                model.Txt_R_Add2_AB = Convert.ToString(d1.Rows[0]["C_R_ADD2"]);
                model.Txt_R_Add3_AB = Convert.ToString(d1.Rows[0]["C_R_ADD3"]);
                model.Txt_R_Add4_AB = Convert.ToString(d1.Rows[0]["C_R_ADD4"]);
                string CountrycodeRes = "";
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_COUNTRY_ID"]))
                {
                    if (d1.Rows[0]["C_R_COUNTRY_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RCountryList_AB = Convert.ToString(d1.Rows[0]["C_R_COUNTRY_ID"]);
                        CountrycodeRes = model.Country_R_DS.Where(x => x.R_CO_REC_ID == model.GLookUp_RCountryList_AB).First().R_CO_CODE;
                        model.State_R_DS = GetStateList(CountrycodeRes);
                        //if (model.GLookUp_RCountryList_AB != "f9970249-121c-4b8f-86f9-2b53e850809e")
                        //{
                        //    RefreshCityList_AB(model.GLookUp_RCountryList_AB, CountrycodeRes, "");
                        //}
                    }
                }
                string StatecodeRes = "";
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_STATE_ID"]))
                {
                    if (d1.Rows[0]["C_R_STATE_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RStateList_AB = Convert.ToString(d1.Rows[0]["C_R_STATE_ID"]);
                        if (model.State_R_DS != null && model.State_R_DS.Count > 0)//Redmine Bug #133705 fixed
                        {
                            StatecodeRes = model.State_R_DS.Where(x => x.R_ST_REC_ID == model.GLookUp_RStateList_AB).First().R_ST_CODE;
                            model.District_R_DS = GetDistrictList(CountrycodeRes, StatecodeRes);
                            model.City_R_DS = GetCityList(model.GLookUp_RCountryList_AB, CountrycodeRes, StatecodeRes);
                        }
                    }
                }
                if (model.City_R_DS == null || model.City_R_DS.Count == 0)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB) == false && model.GLookUp_RCountryList_AB != "f9970249-121c-4b8f-86f9-2b53e850809e")
                    {
                        model.City_R_DS = GetCityList(model.GLookUp_RCountryList_AB, CountrycodeRes, "");
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_DISTRICT_ID"]))
                {
                    if ((d1.Rows[0]["C_R_DISTRICT_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_RDistrictList_AB = Convert.ToString(d1.Rows[0]["C_R_DISTRICT_ID"]);
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_CITY_ID"]))
                {
                    if ((d1.Rows[0]["C_R_CITY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_RCityList_AB = Convert.ToString(d1.Rows[0]["C_R_CITY_ID"]);
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_PINCODE"]))
                {
                    model.Txt_R_Pincode_AB = Convert.ToString(d1.Rows[0]["C_R_PINCODE"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_ACC_PARTY"]))
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(d1.Rows[0]["C_ACC_PARTY"])))
                    {
                        if ((bool)d1.Rows[0]["C_ACC_PARTY"])
                        {
                            model.Rad_Acc_Party_AB = "1";
                        }
                        else
                        {
                            model.Rad_Acc_Party_AB = "0";
                        }
                    }
                    else
                    {
                        model.Rad_Acc_Party_AB = "0";
                    }
                }
                else
                {
                    model.Rad_Acc_Party_AB = "0";
                }

                // 
                model.Txt_O_Add1_AB = Convert.ToString(d1.Rows[0]["C_O_ADD1"]);
                model.Txt_O_Add2_AB = Convert.ToString(d1.Rows[0]["C_O_ADD2"]);
                model.Txt_O_Add3_AB = Convert.ToString(d1.Rows[0]["C_O_ADD3"]);
                model.Txt_O_Add4_AB = Convert.ToString(d1.Rows[0]["C_O_ADD4"]);

                if (!Convert.IsDBNull(d1.Rows[0]["C_O_COUNTRY_ID"]))
                {
                    if ((d1.Rows[0]["C_O_COUNTRY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OCountryList_AB = Convert.ToString(d1.Rows[0]["C_O_COUNTRY_ID"]);
                        CountrycodeRes = model.Country_O_DS.Where(x => x.R_CO_REC_ID == model.GLookUp_OCountryList_AB).First().R_CO_CODE;
                        model.State_O_DS = GetStateList(CountrycodeRes);
                        //if (d1.Rows[0]["C_O_COUNTRY_ID"].ToString() != "f9970249-121c-4b8f-86f9-2b53e850809e")
                        //{
                        //    RefreshCityList_AB(model.GLookUp_OCountryList_AB, CountrycodeRes, "");
                        //}
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_STATE_ID"]))
                {
                    if ((d1.Rows[0]["C_O_STATE_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OStateList_AB = Convert.ToString(d1.Rows[0]["C_O_STATE_ID"]);
                        if (model.State_O_DS != null && model.State_O_DS.Count > 0)//Redmine Bug #133705 fixed
                        {
                            StatecodeRes = model.State_O_DS.Where(x => x.R_ST_REC_ID == model.GLookUp_OStateList_AB).First().R_ST_CODE;
                            model.District_O_DS = GetDistrictList(CountrycodeRes, StatecodeRes);
                            model.City_O_DS = GetCityList(model.GLookUp_OCountryList_AB, CountrycodeRes, StatecodeRes);
                        }
                    }
                }
                if (model.City_O_DS == null || model.City_O_DS.Count == 0)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_OCountryList_AB) == false && model.GLookUp_OCountryList_AB != "f9970249-121c-4b8f-86f9-2b53e850809e")
                    {
                        model.City_O_DS = GetCityList(model.GLookUp_OCountryList_AB, CountrycodeRes, "");
                    }
                }

                if (!Convert.IsDBNull(d1.Rows[0]["C_O_DISTRICT_ID"]))
                {
                    if ((d1.Rows[0]["C_O_DISTRICT_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_ODistrictList_AB = Convert.ToString(d1.Rows[0]["C_O_DISTRICT_ID"]);

                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_CITY_ID"]))
                {
                    if ((d1.Rows[0]["C_O_CITY_ID"].ToString().Length > 0))
                    {
                        model.GLookUp_OCityList_AB = Convert.ToString(d1.Rows[0]["C_O_CITY_ID"]);

                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_O_PINCODE"]))
                {
                    model.Txt_O_Pincode_AB = Convert.ToString(d1.Rows[0]["C_O_PINCODE"]);
                }

                // 3
                model.Txt_R_Tel_1_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_R_1"]);
                model.Txt_R_Tel_2_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_R_2"]);
                model.Txt_R_Fax_1_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_R_1"]);
                model.Txt_R_Fax_2_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_R_2"]);

                model.Txt_O_Tel_1_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_O_1"]);
                model.Txt_O_Tel_2_AB = Convert.ToString(d1.Rows[0]["C_TEL_NO_O_2"]);
                model.Txt_O_Fax_1_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_O_1"]);
                model.Txt_O_Fax_2_AB = Convert.ToString(d1.Rows[0]["C_FAX_NO_O_2"]);

                model.Txt_Mob_1_AB = Convert.ToString(d1.Rows[0]["C_MOB_NO_1"]);
                model.Txt_Mob_2_AB = Convert.ToString(d1.Rows[0]["C_MOB_NO_2"]);

                model.Txt_Email1_AB = Convert.ToString(d1.Rows[0]["C_EMAIL_ID_1"]);
                model.Txt_Email2_AB = Convert.ToString(d1.Rows[0]["C_EMAIL_ID_2"]);

                model.Txt_Website_AB = Convert.ToString(d1.Rows[0]["C_WEBSITE"]);
                model.Txt_Skype_AB = Convert.ToString(d1.Rows[0]["C_SKYPE_ID"]);
                model.Txt_Facebook_AB = Convert.ToString(d1.Rows[0]["C_FACEBOOK_ID"]);
                model.Txt_Twitter_AB = Convert.ToString(d1.Rows[0]["C_TWITTER_ID"]);
                model.Txt_GTalk_AB = Convert.ToString(d1.Rows[0]["C_GTALK_ID"]);

                // 4              
                if (!Convert.IsDBNull(d1.Rows[0]["C_DOB_L"]))
                {
                    model.DE_DOB_L_AB = Convert.ToDateTime(d1.Rows[0]["C_DOB_L"]);
                }
                else
                {
                    model.DE_DOB_L_AB = null;
                }

                model.Com_BloodGroup_AB = Convert.ToString(d1.Rows[0]["C_BLOODGROUP"]);

                if (!Convert.IsDBNull(d1.Rows[0]["C_CONTACT_MODE_ID"]))
                {
                    model.Look_ConModeList_AB = Convert.ToString(d1.Rows[0]["C_CONTACT_MODE_ID"]);
                }

                model.Txt_PAN_No_AB = Convert.ToString(d1.Rows[0]["C_PAN_NO"]);
                model.Txt_VAT_TIN_AB = d1.Rows[0]["C_VAT_TIN_NO"].ToString();
                model.GST_TIN = d1.Rows[0]["C_GST_TIN_NO"].ToString();
                model.Txt_CST_TIN_AB = d1.Rows[0]["C_CST_TIN_NO"].ToString();
                model.Txt_TAN_AB = Convert.ToString(d1.Rows[0]["C_TAN_NO"]);
                model.Txt_UID_AB = Convert.ToString(d1.Rows[0]["C_UID_NO"]);
                model.Txt_STR_AB = Convert.ToString(d1.Rows[0]["C_STR_NO"]);
                model.Txt_VoterID_AB = Convert.ToString(d1.Rows[0]["C_VOTER_ID_NO"]);
                model.Txt_RationCardNo_AB = Convert.ToString(d1.Rows[0]["C_RATION_NO"]);
                model.Txt_DLNo_AB = Convert.ToString(d1.Rows[0]["C_DL_NO"]);
                model.Txt_TaxpayerID_AB = Convert.ToString(d1.Rows[0]["C_TAX_ID_NO"]);
                if (!Convert.IsDBNull(d1.Rows[0]["C_PASSPORT_NO"]))
                {
                    model.Txt_Passport_No_AB = Convert.ToString(d1.Rows[0]["C_PASSPORT_NO"]);
                }
                model.Magazine = new List<Models.Magazine>();
                foreach (DataRow currRow in dMag.Rows)
                {
                    model.Magazine.Add(new Models.Magazine() { Magazine_Misc_ID = currRow["C_MISC_REC_ID"].ToString() });
                }
                // 5
                //XtraTabControl1.SelectedTabPage = Page_Status;
                // 
                model.Rad_Status_AB = Convert.ToString(d1.Rows[0]["C_STATUS"]);
                // Status
                if ((model.Rad_Status_AB.ToLower() == "wellwisher"))
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CON_DT"]))
                    {
                        model.DE_DOC_AB = Convert.ToDateTime(d1.Rows[0]["C_CON_DT"]);
                    }
                    else
                    {
                        model.DE_DOC_AB = null;
                    }
                }
                else
                {
                    if (!Convert.IsDBNull(d1.Rows[0]["C_DOB_A"]))
                    {
                        // Alokik DoB
                        model.DE_DOB_A_AB = Convert.ToDateTime(d1.Rows[0]["C_DOB_A"]);
                    }
                    else
                    {
                        model.DE_DOB_A_AB = null;
                    }
                    // BK Title
                    model.Com_BK_Title_AB = Convert.ToString(d1.Rows[0]["C_BK_TITLE"]);
                    // BK PAd NO :text
                    model.Txt_PAD_No_AB = Convert.ToString(d1.Rows[0]["C_BK_PAD_NO"]);
                    model.Com_Class_At_AB = Convert.ToString(d1.Rows[0]["C_CLASS_AT"]);
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CEN_CATEGORY"]))
                    {
                        model.Cmb_Cen_Cagetory_AB = Convert.ToInt32(d1.Rows[0]["C_CEN_CATEGORY"]);
                    }
                    else
                    {
                        model.Cmb_Cen_Cagetory_AB = 0;
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["C_CLASS_CEN_ID"]))
                    {
                        model.GLookUp_Cen_List_AB = Convert.ToString(d1.Rows[0]["C_CLASS_CEN_ID"]);
                    }
                    model.Txt_Class_Add_AB = Convert.ToString(d1.Rows[0]["C_CLASS_ADD1"]);
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_CATEGORY"]))
                {
                    model.Look_CategoryList_AB = Convert.ToString(d1.Rows[0]["C_CATEGORY"]);
                }
                // 6
                //XtraTabControl1.SelectedTabPage = Page_Special;
                // Wings
                model.WingsMember = new List<WingsMember>();
                foreach (DataRow currRow in dWing.Rows)
                {
                    model.WingsMember.Add(new WingsMember() { Wings_Misc_ID = currRow["C_WING_ID"].ToString() });
                }
                // Specialities
                model.Specialities = new List<Specialitites>();
                foreach (DataRow currRow in dSpecial.Rows)
                {
                    model.Specialities.Add(new Specialitites() { Specialities_Misc_ID = currRow["C_MISC_REC_ID"].ToString() });
                }
                // Events
                model.Events = new List<Events>();
                foreach (DataRow currRow in dEvents.Rows)
                {
                    model.Events.Add(new Events() { Events_Misc_ID = currRow["C_MISC_REC_ID"].ToString() });
                }
                #endregion
            }
            if (model.Rad_Status_AB.ToLower() == "wellwisher")
            {
                model.IsBkReadOnly = true;
            }
            ViewBag.ABImage = AB_Image;
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            DataTable ServiceUser = BASE._Address_DBOps.GetAddressBookServiceUserDetails(model.Rec_ID);
            if (ServiceUser != null && ServiceUser.Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(model.Txt_Mob_1_AB) == false && model.Txt_Mob_1_AB.Length <= 10)
                {
                    model.Txt_Mob_1_AB = "0" + model.Txt_Mob_1_AB;
                }
                if (string.IsNullOrWhiteSpace(model.Txt_Mob_2_AB) == false && model.Txt_Mob_2_AB.Length <= 10)
                {
                    model.Txt_Mob_2_AB = "0" + model.Txt_Mob_2_AB;
                }
            }
            return View("Frm_Address_Info_Window", model);
        }
        [HttpPost]
        public ActionResult Frm_Address_Info_Window(Address_Book_ViewModel model, string resultflag)
        {
            var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.New_Confirm == false && model.Edit_Confirm == false)
                {
                    if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            jsonParam.message = "Party Cannot Be Added/Updated/Deleted...!<br><br>Final Audit Has Been Completed For This Year.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (BASE.AllowMultiuser())
                    {
                        if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                        {
                            DataTable address_DbOps = BASE._Address_DBOps.GetRecord(model.Rec_ID);
                            if (address_DbOps == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (address_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Contact");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            DateTime EditOn1 = (DateTime)model.LastEditedOn;
                            DateTime EditOn2 = (DateTime)address_DbOps.Rows[0]["REC_EDIT_ON"];
                            TimeSpan t = EditOn2 - EditOn1;
                            if (t.TotalSeconds >= 1 || t.TotalSeconds <= -1)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Contact");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (Tag == Common.Navigation_Mode._Delete)
                        {
                            object MaxValue = 0;
                            MaxValue = BASE._Address_DBOps.GetStatus(model.Rec_ID);
                            if (MaxValue == null)
                            {
                                jsonParam.message = "Entry Not Found...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                            {
                                jsonParam.message = "Locked Entry can not be Changed...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            bool DeleteAllow = false;
                            string UsedPage = "";
                            DataTable MaxCount = null;
                            DeleteAllow = CheckAddressUsage(model.Rec_ID, ref UsedPage);
                            if (!DeleteAllow)
                            {
                                jsonParam.message = "Can't Delete...!<br><br>This Contact Is Being Refered In Another Record in Current Or Other Years...!<br><br>Name: " + UsedPage;
                                jsonParam.title = "Warning...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Tag == Common.Navigation_Mode._Edit)
                    {
                        // Bug #5056 fix
                        DataTable d1 = BASE._Membership_DBOps.GetUsageAsPastMember(model.Org_RecID);
                        if (model.Rad_Status_AB == "0" && d1.Rows.Count > 0)
                        {
                            jsonParam.message = "Wing/BKES Members should be BKs...!<br><br>Current Contact is a Wing/BKES member and hence status must remain as B.K.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (string.IsNullOrWhiteSpace(model.GLookUp_Cen_List_AB) && (d1.Rows.Count > 0))
                        {
                            // Bug #4069
                            jsonParam.message = "Centre Name should be selected for Wing/BKES Members...!<br><br>Current Contact is a Wing/BKES member and hence Indian/Overseas Center must be selected.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }

                        d1 = BASE._CollectionBox_DBOps.GetUsageAsPastWitness(model.Rec_ID);
                        if (model.Com_BK_Title_AB != "Teacher" && model.Com_BK_Title_AB != "Surrender Kumar" && model.Com_BK_Title_AB != "Surrender Sister" && model.Com_BK_Title_AB != "Trialkumar" && model.Com_BK_Title_AB != "Trialkumari" && d1.Rows.Count > 0)
                        {
                            jsonParam.message = "Wrong BK Title Selected for a CollectionBox Voucher Witness...!<br><br>Selected Title must be Teacher/Surrender Kumar/ Surrender Sister/Trialkumar/Trialkumari";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        d1 = BASE._Donation_DBOps.GetUsageAsPastDonor(model.Rec_ID);
                        if (d1.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                            {
                                jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.GLookUp_RCountryList_AB == "f9970249-121c-4b8f-86f9-2b53e850809e")
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCityList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RDistrictList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RStateList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                                {

                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1 , City, District, State, Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                                {
                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1,Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        d1 = BASE._Donation_DBOps.GetUsageAsPastForeignDonor(model.Rec_ID);
                        if (d1.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                            {
                                jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Country must be mentioned for a Party used in Foreign Donation Voucher.";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.GLookUp_RCountryList_AB == "f9970249-121c-4b8f-86f9-2b53e850809e")
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCityList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RDistrictList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RStateList_AB)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                                {

                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1 , City, District, State, Country must be mentioned for a Party used in Foreign Donation Voucher.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB))
                                {
                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1,Country must be mentioned for a Party used in Foreign Donation Voucher.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        d1 = BASE._Address_DBOps.GetAddressBookServiceUserDetails(model.Rec_ID);
                        if (d1 != null && d1.Rows.Count > 0)
                        {
                            string ServiceUSerID = d1.Rows[0]["userId"].ToString();
                            if (string.IsNullOrWhiteSpace(model.Txt_Mob_1_AB) && string.IsNullOrWhiteSpace(model.Txt_Mob_2_AB) && string.IsNullOrWhiteSpace(model.Txt_Email1_AB) && string.IsNullOrWhiteSpace(model.Txt_Email2_AB))
                            {
                                jsonParam.message = "One Among Mobile Nos and Email Ids Must Be Mentioned For Service Users";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Mob_1_AB";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (string.IsNullOrWhiteSpace(model.Txt_Mob_1_AB) == false && model.Txt_Mob_1_AB.Length>10) 
                            {
                                if (model.Txt_Mob_1_AB.StartsWith("0")) 
                                {
                                    model.Txt_Mob_1_AB = model.Txt_Mob_1_AB.Remove(0, 1);
                                }
                            }
                            if (string.IsNullOrWhiteSpace(model.Txt_Mob_2_AB) == false && model.Txt_Mob_2_AB.Length > 10)
                            {
                                if (model.Txt_Mob_2_AB.StartsWith("0"))
                                {
                                    model.Txt_Mob_2_AB = model.Txt_Mob_2_AB.Remove(0, 1);
                                }
                            }
                            DataTable UserInfo = BASE._Address_DBOps.Get_service_users_From_Contact_Details(model.Txt_Mob_1_AB, model.Txt_Mob_2_AB, model.Txt_Email1_AB, model.Txt_Email2_AB);
                            DataRow[] result = UserInfo.Select("UserId <> " + ServiceUSerID);
                            if (result != null && result.Length > 0)
                            {
                                string Contact_data = result[0]["PhoneOrEmail"].ToString();
                                string MobOREmail = Contact_data.Contains("@") ? "Email ID: " : "Mobile No: ";
                                jsonParam.message = MobOREmail + "<b>" + Contact_data + "</b> Is Already Linked With Another Service User";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.focusid= getContactFieldFocusID(Contact_data, model);
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        //if (d1.Rows.Count > 0 && (string.IsNullOrWhiteSpace(model.Txt_R_Add1_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCityList_AB) || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_AB)))
                        //{
                        //    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1 , City, Country must be mentioned for a Party used in Foreign Donation Voucher.";
                        //    jsonParam.title = "Information...";
                        //    jsonParam.result = false;
                        //    return Json(new
                        //    {
                        //        jsonParam
                        //    }, JsonRequestBehavior.AllowGet);
                        //}
                    }
                }
                int I = 0;
                string SelectedItems = "";
                if (model.WingsMember != null)
                {
                    for (; I < model.WingsMember.Count(); I++)
                    {
                        if (model.WingsMember[I].Selected == true)
                        {
                            SelectedItems += model.WingsMember[I].Wings_Misc_ID + " | ";
                        }
                    }
                }
                if (SelectedItems.Trim().Length > 0)
                {
                    SelectedItems = SelectedItems.Trim().EndsWith("|") ? SelectedItems.Trim().Substring(0, SelectedItems.Trim().Length - 1) : SelectedItems.Trim();
                }
                var Chk_WingsList_Tag = SelectedItems;
                I = 0; SelectedItems = "";
                if (model.Specialities != null)
                {
                    for (; I < model.Specialities.Count(); I++)
                    {
                        if (model.Specialities[I].Selected == true)
                        {
                            SelectedItems += model.Specialities[I].Specialities_Misc_ID + " | ";
                        }
                    }
                }
                if (SelectedItems.Trim().Length > 0)
                {
                    SelectedItems = SelectedItems.Trim().EndsWith("|") ? SelectedItems.Trim().Substring(0, SelectedItems.Trim().Length - 1) : SelectedItems.Trim();
                }
                var Chk_SpecialList_Tag = SelectedItems;
                I = 0; SelectedItems = "";
                if (model.Events != null)
                {
                    for (; I < model.Events.Count(); I++)
                    {
                        if (model.Events[I].Selected == true)
                        {
                            SelectedItems += model.Events[I].Events_Misc_ID + " | ";
                        }
                    }
                }
                if (SelectedItems.Trim().Length > 0)
                {
                    SelectedItems = SelectedItems.Trim().EndsWith("|") ? SelectedItems.Trim().Substring(0, SelectedItems.Trim().Length - 1) : SelectedItems.Trim();
                }
                var Chk_EventsList_Tag = SelectedItems;

                if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New)
                {
                    if (string.IsNullOrWhiteSpace(model.Txt_Name_AB))
                    {
                        jsonParam.message = "Name Cannot Be Blank...";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Name_AB";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrWhiteSpace(model.Txt_Email1_AB))
                    {
                        if (BASE.IsEmail(model.Txt_Email1_AB) == false)
                        {
                            jsonParam.message = "Email ID Incorrect...";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Email1_AB";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(model.Txt_Email2_AB))
                    {
                        if (BASE.IsEmail(model.Txt_Email2_AB) == false)
                        {
                            jsonParam.message = "Email ID Incorrect...";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Email2_AB";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(model.Txt_Website_AB))
                    {
                        if (BASE.IsUrl(model.Txt_Website_AB) == false)
                        {
                            jsonParam.message = "Website Address Incorrect...";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Website_AB";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Name_AB))
                    {
                        model.Txt_Name_AB = BASE.CleanTextForDB(model.Txt_Name_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_OrgName_AB))
                    {
                        model.Txt_OrgName_AB = BASE.CleanTextForDB(model.Txt_OrgName_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Desig_AB))
                    {
                        model.Txt_Desig_AB = BASE.CleanTextForDB(model.Txt_Desig_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Education_AB))
                    {
                        model.Txt_Education_AB = BASE.CleanTextForDB(model.Txt_Education_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Ref_AB))
                    {
                        model.Txt_Ref_AB = BASE.CleanTextForDB(model.Txt_Ref_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Remark1_AB))
                    {
                        model.Txt_Remark1_AB = BASE.CleanTextForDB(model.Txt_Remark1_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Remark2_AB))
                    {
                        model.Txt_Remark2_AB = BASE.CleanTextForDB(model.Txt_Remark2_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_R_Add1_AB))
                    {
                        model.Txt_R_Add1_AB = BASE.CleanTextForDB(model.Txt_R_Add1_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_R_Add2_AB))
                    {
                        model.Txt_R_Add2_AB = BASE.CleanTextForDB(model.Txt_R_Add2_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_R_Add3_AB))
                    {
                        model.Txt_R_Add3_AB = BASE.CleanTextForDB(model.Txt_R_Add3_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_R_Add4_AB))
                    {
                        model.Txt_R_Add4_AB = BASE.CleanTextForDB(model.Txt_R_Add4_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_O_Add1_AB))
                    {
                        model.Txt_O_Add1_AB = BASE.CleanTextForDB(model.Txt_O_Add1_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_O_Add2_AB))
                    {
                        model.Txt_O_Add2_AB = BASE.CleanTextForDB(model.Txt_O_Add2_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_O_Add3_AB))
                    {
                        model.Txt_O_Add3_AB = BASE.CleanTextForDB(model.Txt_O_Add3_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_O_Add4_AB))
                    {
                        model.Txt_O_Add4_AB = BASE.CleanTextForDB(model.Txt_O_Add4_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Email1_AB))
                    {
                        model.Txt_Email1_AB = BASE.CleanTextForDB(model.Txt_Email1_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Email2_AB))
                    {
                        model.Txt_Email2_AB = BASE.CleanTextForDB(model.Txt_Email2_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Website_AB))
                    {
                        model.Txt_Website_AB = BASE.CleanTextForDB(model.Txt_Website_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Skype_AB))
                    {
                        model.Txt_Skype_AB = BASE.CleanTextForDB(model.Txt_Skype_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Facebook_AB))
                    {
                        model.Txt_Facebook_AB = BASE.CleanTextForDB(model.Txt_Facebook_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Twitter_AB))
                    {
                        model.Txt_Twitter_AB = BASE.CleanTextForDB(model.Txt_Twitter_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_GTalk_AB))
                    {
                        model.Txt_GTalk_AB = BASE.CleanTextForDB(model.Txt_GTalk_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_PAD_No_AB))
                    {
                        model.Txt_PAD_No_AB = BASE.CleanTextForDB(model.Txt_PAD_No_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Class_Add_AB))
                    {
                        model.Txt_Class_Add_AB = BASE.CleanTextForDB(model.Txt_Class_Add_AB);
                    }
                    if (!string.IsNullOrEmpty(model.Txt_Passport_No_AB))
                    {
                        model.Txt_Passport_No_AB = BASE.CleanTextForDB(model.Txt_Passport_No_AB);//redmine Bug #133486 fix
                    }
                }
                Param_Txn_Insert_Addresses InNewParam = new Param_Txn_Insert_Addresses();
                if (Tag == Common.Navigation_Mode._New)
                {
                    model.Rec_ID = System.Guid.NewGuid().ToString();
                    string ReplicationID = System.Guid.NewGuid().ToString();
                    string STR1 = "";
                    bool Result = true;
                    Parameter_Insert_Addresses InParam = new Parameter_Insert_Addresses();
                    InParam.Title = model.Look_TitleList_AB ?? "";
                    InParam.Name = model.Txt_Name_AB ?? "";
                    InParam.Gender = model.Rad_Gender_AB ?? "";
                    InParam.OrgName = model.Txt_OrgName_AB ?? "";
                    InParam.Designation = model.Txt_Desig_AB ?? "";
                    InParam.Education = model.Txt_Education_AB ?? "";
                    if (!(model.Look_OccupationList_AB == null))
                    {
                        InParam.OccupationID = model.Look_OccupationList_AB;
                    }
                    InParam.Reference = model.Txt_Ref_AB ?? "";
                    InParam.Remarks1 = model.Txt_Remark1_AB ?? "";
                    InParam.Remarks2 = model.Txt_Remark2_AB ?? "";
                    if (model.DE_DOB_L_AB != null)
                    {
                        InParam.LokikDob = Convert.ToDateTime(model.DE_DOB_L_AB).ToString(BASE._Server_Date_Format_Long);
                    }
                    InParam.BloodGroup = model.Com_BloodGroup_AB ?? "";
                    InParam.ContactModeID = model.Look_ConModeList_AB;
                    InParam.PANNo = model.Txt_PAN_No_AB ?? "";
                    InParam.VAT_TIN = model.Txt_VAT_TIN_AB ?? "";
                    if (string.IsNullOrEmpty(model.GST_TIN))
                    {
                        InParam.GST_TIN = model.Txt_VAT_TIN_AB ?? "";
                    }
                    else
                    {
                        InParam.GST_TIN = model.GST_TIN ?? "";
                    }
                    InParam.CST_TIN = model.Txt_CST_TIN_AB ?? "";
                    InParam.TAN = model.Txt_TAN_AB ?? "";
                    InParam.UID = model.Txt_UID_AB ?? "";
                    InParam.STRNo = model.Txt_STR_AB ?? "";
                    InParam.PassportNo = model.Txt_Passport_No_AB ?? "";
                    InParam.VoterID = model.Txt_VoterID_AB ?? "";
                    InParam.RationCardNo = model.Txt_RationCardNo_AB ?? "";
                    InParam.DLNo = model.Txt_DLNo_AB ?? "";
                    InParam.TaxpayerID = model.Txt_TaxpayerID_AB ?? "";
                    InParam.Magazine = null;
                    InParam.Res_Add1 = (model != null ? model.Txt_R_Add1_AB : "");
                    InParam.Res_Add2 = (model != null ? model.Txt_R_Add2_AB : "");
                    InParam.Res_Add3 = (model != null ? model.Txt_R_Add3_AB : "");
                    InParam.Res_Add4 = (model != null ? model.Txt_R_Add4_AB : "");
                    InParam.Res_cityID = model.GLookUp_RCityList_AB;
                    InParam.Res_StateID = model.GLookUp_RStateList_AB;
                    InParam.Res_DisttID = model.GLookUp_RDistrictList_AB;
                    InParam.Res_CountryID = model.GLookUp_RCountryList_AB;
                    InParam.Res_PinCode = model.Txt_R_Pincode_AB ?? "";
                    InParam.Off_Add1 = model.Txt_O_Add1_AB ?? "";
                    InParam.Off_Add2 = model.Txt_O_Add2_AB ?? "";
                    InParam.Off_Add3 = model.Txt_O_Add3_AB ?? "";
                    InParam.Off_Add4 = model.Txt_O_Add4_AB ?? "";

                    InParam.Off_CityID = model.GLookUp_OCityList_AB;

                    InParam.Off_StateID = model.GLookUp_OStateList_AB;

                    InParam.Off_DistID = model.GLookUp_ODistrictList_AB;

                    InParam.Off_CountryID = model.GLookUp_OCountryList_AB;
                    InParam.Off_PinCode = (model.Txt_O_Pincode_AB != null ? model.Txt_O_Pincode_AB : "");
                    InParam.ResTel1 = (model.Txt_R_Tel_1_AB != null ? model.Txt_R_Tel_1_AB : "");
                    InParam.ResTel2 = (model.Txt_R_Tel_2_AB != null ? model.Txt_R_Tel_2_AB : "");
                    InParam.ResFax1 = model.Txt_R_Fax_1_AB ?? "";
                    InParam.ResFax2 = model.Txt_R_Fax_2_AB ?? "";
                    InParam.OffTel1 = model.Txt_O_Tel_1_AB ?? "";
                    InParam.OffTel2 = model.Txt_O_Tel_2_AB ?? "";
                    InParam.OffFax1 = model.Txt_O_Fax_1_AB ?? "";
                    InParam.OffFax2 = model.Txt_O_Fax_2_AB ?? "";
                    InParam.Mob1 = model.Txt_Mob_1_AB ?? "";
                    InParam.Mob2 = model.Txt_Mob_2_AB ?? "";
                    InParam.Email1 = model.Txt_Email1_AB ?? "";
                    InParam.Email2 = model.Txt_Email2_AB ?? "";
                    InParam.Website = model.Txt_Website_AB ?? "";
                    InParam.SkypeID = model.Txt_Skype_AB ?? "";
                    InParam.FaceBookID = model.Txt_Facebook_AB ?? "";
                    InParam.TwitterID = model.Txt_Twitter_AB ?? "";
                    InParam.GtalkID = model.Txt_GTalk_AB ?? "";
                    InParam.Status = model.Rad_Status_AB ?? "";
                    if (model.DE_DOC_AB != null)
                    {
                        InParam.ContactDate = Convert.ToDateTime(model.DE_DOC_AB).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (model.DE_DOB_A_AB != null)
                    {
                        InParam.AlokikDOB = Convert.ToDateTime(model.DE_DOB_A_AB).ToString(BASE._Server_Date_Format_Long);
                    }

                    InParam.BKTitle = model.Com_BK_Title_AB ?? "";
                    InParam.PADNo = model.Txt_PAD_No_AB ?? "";
                    InParam.ClassAt = model.Com_Class_At_AB ?? "";
                    InParam.CenCategory = Convert.ToInt32(model.Cmb_Cen_Cagetory_AB);
                    InParam.ClassCID = model.GLookUp_Cen_List_AB;

                    InParam.ClassAdd1 = model.Txt_Class_Add_AB ?? "";
                    InParam.Category = model.Look_CategoryList_AB ?? "";
                    InParam.Category_Other = "";

                    InParam.WingsMember = Chk_WingsList_Tag;
                    InParam.Specialities = Chk_SpecialList_Tag;
                    InParam.Special_Other = model.Special_Other ?? "";
                    InParam.Events = Chk_EventsList_Tag;
                    InParam.EventsOther = "";
                    InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    InParam.Rec_ID = model.Rec_ID ?? "";
                    InParam.OrgAB_RecId = model.Rec_ID ?? "";
                    InParam.YearID = BASE._open_Year_ID;
                    InParam.AccountingParty = model.Rad_Acc_Party_AB == "0" ? false : true;
                    InNewParam.param_InsertAddresses = InParam;

                    if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                    {
                        Parameter_Insert_Addresses InParam_NextYear = new Parameter_Insert_Addresses();
                        CopyObject(InParam, InParam_NextYear);
                        InParam_NextYear.Rec_ID = (ReplicationID != null ? ReplicationID : "");
                        InParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        InNewParam.param_InsertAddresses_NextYear = InParam_NextYear;
                    }
                    // START :Add Magazine Info 
                    int ctr = 0;
                    List<Parameter_InsertMagazine_Addresses> InMagInfo = new List<Parameter_InsertMagazine_Addresses>();
                    List<Parameter_InsertMagazine_Addresses> InMagInfo_NextYear = new List<Parameter_InsertMagazine_Addresses>();
                    if (model.Magazine != null)
                    {
                        foreach (var currSelection in model.Magazine.Where(x => x.Selected == true))
                        {
                            Parameter_InsertMagazine_Addresses InMag = new Parameter_InsertMagazine_Addresses();
                            InMag.AB_Rec_ID = model.Rec_ID;
                            InMag.Magazine_Misc_ID = currSelection.Magazine_Misc_ID.ToString();
                            InMag.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InMag.Rec_ID = Guid.NewGuid().ToString();
                            InMagInfo.Add(InMag);
                            if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                            {
                                Parameter_InsertMagazine_Addresses InMag_NextYear = new Parameter_InsertMagazine_Addresses();
                                CopyObject(InMag, InMag_NextYear);
                                InMag_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InMag_NextYear.AB_Rec_ID = ReplicationID;
                                InMagInfo_NextYear.Add(InMag_NextYear);
                            }
                            ctr++;
                        }
                    }
                    InNewParam.InsertMagazine = InMagInfo.ToArray();
                    InNewParam.InsertMagazine_NextYear = InMagInfo_NextYear.ToArray();
                    // END MAGAZINE Addition
                    // START :ADD WINGS Info 
                    ctr = 0;
                    List<Parameter_InsertWings_Addresses> InWingInfo = new List<Parameter_InsertWings_Addresses>();
                    List<Parameter_InsertWings_Addresses> InWingInfo_NextYear = new List<Parameter_InsertWings_Addresses>();

                    if (model.WingsMember != null)
                    {
                        foreach (var currSelection in model.WingsMember.Where(x => x.Selected == true))
                        {
                            Parameter_InsertWings_Addresses InWing = new Parameter_InsertWings_Addresses();
                            InWing.AB_Rec_ID = model.Rec_ID;
                            InWing.Wings_Misc_ID = currSelection.Wings_Misc_ID.ToString();
                            InWing.Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                            InWing.Rec_ID = System.Guid.NewGuid().ToString();
                            InWingInfo.Add(InWing);
                            if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                            {
                                Parameter_InsertWings_Addresses InWing_NextYear = new Parameter_InsertWings_Addresses();
                                CopyObject(InWing, InWing_NextYear);
                                InWing_NextYear.AB_Rec_ID = ReplicationID;
                                InWing_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InWingInfo_NextYear.Add(InWing_NextYear);
                            }
                            ctr++;
                        }
                    }
                    InNewParam.InsertWings = InWingInfo.ToArray();
                    InNewParam.InsertWings_NextYear = InWingInfo_NextYear.ToArray();
                    // END WINGS Addition
                    // START :ADD SPECIALITIES Info 
                    List<Parameter_InsertSpecialities_Addresses> InSpecInfo = new List<Parameter_InsertSpecialities_Addresses>();
                    List<Parameter_InsertSpecialities_Addresses> InSpecInfo_NextYear = new List<Parameter_InsertSpecialities_Addresses>();
                    ctr = 0;
                    if (model.Specialities != null)
                    {
                        foreach (var currSelection in model.Specialities.Where(x => x.Selected == true))
                        {
                            Parameter_InsertSpecialities_Addresses InSpec = new Parameter_InsertSpecialities_Addresses();
                            InSpec.AB_Rec_ID = model.Rec_ID;
                            InSpec.Specialities_Misc_ID = currSelection.Specialities_Misc_ID;
                            InSpec.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InSpec.Rec_ID = Guid.NewGuid().ToString();
                            InSpecInfo.Add(InSpec);
                            if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                            {
                                Parameter_InsertSpecialities_Addresses InSpec_NextYear = new Parameter_InsertSpecialities_Addresses();
                                CopyObject(InSpec, InSpec_NextYear);
                                InSpec_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InSpec_NextYear.AB_Rec_ID = ReplicationID;
                                InSpecInfo_NextYear.Add(InSpec_NextYear);
                            }
                            ctr++;
                        }
                    }
                    InNewParam.InsertSpecialities = InSpecInfo.ToArray();
                    InNewParam.InsertSpecialities_NextYear = InSpecInfo_NextYear.ToArray();

                    List<Parameter_InsertEvents_Addresses> InEventInfo = new List<Parameter_InsertEvents_Addresses>();
                    List<Parameter_InsertEvents_Addresses> InEventInfo_NextYear = new List<Parameter_InsertEvents_Addresses>();
                    ctr = 0;
                    if (model.Events != null)
                    {
                        foreach (var currSelection in model.Events.Where(x => x.Selected == true))
                        {
                            Parameter_InsertEvents_Addresses InEvents = new Parameter_InsertEvents_Addresses();
                            InEvents.AB_Rec_ID = model.Rec_ID;
                            InEvents.Events_Misc_ID = currSelection.Events_Misc_ID;
                            InEvents.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InEvents.Rec_ID = Guid.NewGuid().ToString();
                            InEventInfo.Add(InEvents);
                            if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                            {
                                Parameter_InsertEvents_Addresses InEvents_NextYear = new Parameter_InsertEvents_Addresses();
                                CopyObject(InEvents, InEvents_NextYear);
                                InEvents_NextYear.AB_Rec_ID = ReplicationID;
                                InEvents_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InEvents_NextYear.Events_Misc_ID = currSelection.Events_Misc_ID;
                                InEventInfo_NextYear.Add(InEvents_NextYear);
                            }
                            ctr++;
                        }
                    }

                    InNewParam.InsertEvents = InEventInfo.ToArray();
                    InNewParam.InsertEvents_NextYear = InEventInfo_NextYear.ToArray();
                    // END EVENTS Addition
                    if (model.New_Confirm == false)
                    {
                        Param_Get_Duplicates param = new Param_Get_Duplicates();
                        param.insertPAram = InNewParam.param_InsertAddresses;
                        param.Rec_ID = model.Rec_ID;
                        var Message = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                        if (Message == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Message.ToString().Length > 0)
                        {
                            jsonParam.message = Message + "<br><br>Do you still want to insert the Record?";// redmine Bug #133523 fix
                            jsonParam.title = "Some Possible Duplicates!";
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (BASE._Address_DBOps.InsertAddresses_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "New ~ Contact";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.Rec_ID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                Param_Txn_Update_Addresses EditParam = new Param_Txn_Update_Addresses();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    bool ReplicateChange = false;
                    string ReplicationRecID = "";
                    if (model.Replicate_Confirm == true)
                    {
                        ReplicateChange = true;
                        ReplicationRecID = BASE._Address_DBOps.GetAddressRecID(model.Rec_ID, BASE._next_Unaudited_YearID).ToString();
                    }
                    bool Result = true;
                    Parameter_Update_Addresses UpParam = new Parameter_Update_Addresses();
                    UpParam.Title = (model.Look_TitleList_AB != null ? model.Look_TitleList_AB : "");
                    UpParam.Name = (model.Txt_Name_AB != null ? model.Txt_Name_AB : "");
                    UpParam.Gender = (model.Rad_Gender_AB != null ? model.Rad_Gender_AB : "");
                    UpParam.OrgName = (model.Txt_OrgName_AB != null ? model.Txt_OrgName_AB : "");
                    UpParam.Designation = (model.Txt_Desig_AB != null ? model.Txt_Desig_AB : "");
                    UpParam.Education = (model.Txt_Education_AB != null ? model.Txt_Education_AB : "");
                    if (!(model.Look_OccupationList_AB == null))
                    {
                        UpParam.OccupationID = model.Look_OccupationList_AB;
                    }

                    UpParam.Reference = (model.Txt_Ref_AB != null ? model.Txt_Ref_AB : "");
                    UpParam.Remarks1 = (model.Txt_Remark1_AB != null ? model.Txt_Remark1_AB : "");
                    UpParam.Remarks2 = (model.Txt_Remark2_AB != null ? model.Txt_Remark2_AB : "");
                    if (IsDate(model.DE_DOB_L_AB.ToString()))
                    {
                        UpParam.LokikDob = Convert.ToDateTime(model.DE_DOB_L_AB).ToString(BASE._Server_Date_Format_Long);
                    }
                    UpParam.BloodGroup = (model.Com_BloodGroup_AB != null ? model.Com_BloodGroup_AB : "");
                    if (!(model.Look_ConModeList_AB == null))
                    {
                        UpParam.ContactModeID = model.Look_ConModeList_AB;
                    }
                    UpParam.PANNo = (model.Txt_PAN_No_AB != null ? model.Txt_PAN_No_AB : "");
                    UpParam.VAT_TIN = (model != null ? (string.IsNullOrEmpty(model.Txt_VAT_TIN_AB) ? "" : model.Txt_VAT_TIN_AB.ToString()) : "");
                    UpParam.GST_TIN = (model != null ? (string.IsNullOrEmpty(model.GST_TIN) ? "" : model.GST_TIN.ToString()) : "");
                    UpParam.CST_TIN = (model != null ? (string.IsNullOrEmpty(model.Txt_CST_TIN_AB) ? "" : model.Txt_CST_TIN_AB.ToString()) : "");
                    UpParam.TAN = (model != null ? (string.IsNullOrEmpty(model.Txt_TAN_AB) ? "" : model.Txt_TAN_AB.ToString()) : "");
                    UpParam.UID = (model != null ? (string.IsNullOrEmpty(model.Txt_UID_AB) ? "" : model.Txt_UID_AB.ToString()) : "");
                    UpParam.VoterID = (model != null ? (string.IsNullOrEmpty(model.Txt_VoterID_AB) ? "" : model.Txt_VoterID_AB.ToString()) : "");
                    UpParam.RationCardNo = (model != null ? (string.IsNullOrEmpty(model.Txt_RationCardNo_AB) ? "" : model.Txt_RationCardNo_AB.ToString()) : "");
                    UpParam.DLNo = (model != null ? (string.IsNullOrEmpty(model.Txt_DLNo_AB) ? "" : model.Txt_DLNo_AB.ToString()) : "");
                    UpParam.TaxpayerID = (model != null ? (string.IsNullOrEmpty(model.Txt_TaxpayerID_AB) ? "" : model.Txt_TaxpayerID_AB.ToString()) : "");
                    UpParam.STRNo = (model.Txt_STR_AB != null ? model.Txt_STR_AB : "");
                    UpParam.PassportNo = (model.Txt_Passport_No_AB != null ? model.Txt_Passport_No_AB : "");

                    //if (model.Magazine != null)
                    //{
                    //    UpParam.Magazine = model.Magazine.Where(x => x.Selected == true).Select(a => a.Magazine_Misc_ID).ToList().ToString();
                    //}
                    UpParam.Magazine = null;

                    UpParam.Res_Add1 = (model.Txt_R_Add1_AB != null ? model.Txt_R_Add1_AB : "");
                    UpParam.Res_Add2 = (model.Txt_R_Add2_AB != null ? model.Txt_R_Add2_AB : "");
                    UpParam.Res_Add3 = (model.Txt_R_Add3_AB != null ? model.Txt_R_Add3_AB : "");
                    UpParam.Res_Add4 = (model.Txt_R_Add4_AB != null ? model.Txt_R_Add4_AB : "");
                    //if (!(model.GLookUp_RCityList_AB == null))
                    //{
                        UpParam.Res_cityID = model.GLookUp_RCityList_AB;
                    //}

                    //if (!(model.GLookUp_RStateList_AB == null))
                    //{
                        UpParam.Res_StateID = model.GLookUp_RStateList_AB;
                    //}

                    //if (!(model.GLookUp_RDistrictList_AB == null))
                    //{
                        UpParam.Res_DisttID = model.GLookUp_RDistrictList_AB;
                    //}

                    //if (!(model.GLookUp_RCountryList_AB == null))
                    //{
                        UpParam.Res_CountryID = model.GLookUp_RCountryList_AB;
                   // }

                    UpParam.Res_PinCode = (model.Txt_R_Pincode_AB != null ? model.Txt_R_Pincode_AB : "");
                    UpParam.Off_Add1 = (model.Txt_O_Add1_AB != null ? model.Txt_O_Add1_AB : "");
                    UpParam.Off_Add2 = (model.Txt_O_Add2_AB != null ? model.Txt_O_Add2_AB : "");
                    UpParam.Off_Add3 = (model.Txt_O_Add3_AB != null ? model.Txt_O_Add3_AB : "");
                    UpParam.Off_Add4 = (model.Txt_O_Add4_AB != null ? model.Txt_O_Add4_AB : "");
                    //if (!(model.GLookUp_OCityList_AB == null))
                    //{
                        UpParam.Off_CityID = model.GLookUp_OCityList_AB;
                    //}

                    //if (!(model.GLookUp_OStateList_AB == null))
                    //{
                        UpParam.Off_StateID = model.GLookUp_OStateList_AB;
                    //}

                    //if (!(model.GLookUp_ODistrictList_AB == null))
                    //{
                        UpParam.Off_DistID = model.GLookUp_ODistrictList_AB;
                    //}

                    //if (!(model.GLookUp_OCountryList_AB == null))
                    //{
                        UpParam.Off_CountryID = model.GLookUp_OCountryList_AB;
                    //}

                    UpParam.Off_PinCode = (model.Txt_O_Pincode_AB != null ? model.Txt_O_Pincode_AB : "");
                    UpParam.ResTel1 = (model.Txt_R_Tel_1_AB != null ? model.Txt_R_Tel_1_AB : "");
                    UpParam.ResTel2 = (model.Txt_R_Tel_2_AB != null ? model.Txt_R_Tel_2_AB : "");
                    UpParam.ResFax1 = (model.Txt_R_Fax_1_AB != null ? model.Txt_R_Fax_1_AB : "");
                    UpParam.ResFax2 = (model.Txt_R_Fax_2_AB != null ? model.Txt_R_Fax_2_AB : "");
                    UpParam.OffTel1 = (model.Txt_O_Tel_1_AB != null ? model.Txt_O_Tel_1_AB : "");
                    UpParam.OffTel2 = (model.Txt_O_Tel_2_AB != null ? model.Txt_O_Tel_2_AB : "");
                    UpParam.OffFax1 = (model.Txt_O_Fax_1_AB != null ? model.Txt_O_Fax_1_AB : "");
                    UpParam.OffFax2 = (model.Txt_O_Fax_2_AB != null ? model.Txt_O_Fax_2_AB : "");
                    UpParam.Mob1 = (model.Txt_Mob_1_AB != null ? model.Txt_Mob_1_AB : "");
                    UpParam.Mob2 = (model.Txt_Mob_2_AB != null ? model.Txt_Mob_2_AB : "");
                    UpParam.Email1 = (model.Txt_Email1_AB != null ? model.Txt_Email1_AB : "");
                    UpParam.Email2 = (model.Txt_Email2_AB != null ? model.Txt_Email2_AB : "");
                    UpParam.Website = (model.Txt_Website_AB != null ? model.Txt_Website_AB : "");
                    UpParam.SkypeID = (model.Txt_Skype_AB != null ? model.Txt_Skype_AB : "");
                    UpParam.FaceBookID = (model.Txt_Facebook_AB != null ? model.Txt_Facebook_AB : "");
                    UpParam.TwitterID = (model.Txt_Twitter_AB != null ? model.Txt_Twitter_AB : "");
                    UpParam.GtalkID = (model.Txt_GTalk_AB != null ? model.Txt_GTalk_AB : "");
                    UpParam.Status = (model.Rad_Status_AB != null ? model.Rad_Status_AB : "");
                    if (IsDate(model.DE_DOC_AB.ToString()))
                    {
                        UpParam.ContactDate = Convert.ToDateTime(model.DE_DOC_AB).ToString(BASE._Server_Date_Format_Long);
                    }

                    if (IsDate(model.DE_DOB_A_AB.ToString()))
                    {
                        UpParam.AlokikDOB = Convert.ToDateTime(model.DE_DOB_A_AB).ToString(BASE._Server_Date_Format_Long);
                    }

                    UpParam.BKTitle = (model.Com_BK_Title_AB != null ? model.Com_BK_Title_AB : "");
                    UpParam.PADNo = (model.Txt_PAD_No_AB != null ? model.Txt_PAD_No_AB : "");
                    UpParam.ClassAt = (model.Com_Class_At_AB != null ? model.Com_Class_At_AB : "");
                    UpParam.CenCategory = Convert.ToInt32(model.Cmb_Cen_Cagetory_AB);
                    UpParam.ClassCID = model.GLookUp_Cen_List_AB;
                    UpParam.ClassAdd1 = (model.Txt_Class_Add_AB != null ? model.Txt_Class_Add_AB : "");
                    UpParam.Category = (model.Look_CategoryList_AB != null ? model.Look_CategoryList_AB : "");
                    UpParam.Category_Other = "";
                    if (model.WingsMember != null)
                    {
                        UpParam.WingsMember = Chk_WingsList_Tag;
                    }
                    if (model.Specialities != null)
                    {
                        UpParam.Specialities = Chk_SpecialList_Tag;
                    }

                    UpParam.Special_Other = model.Special_Other;
                    if (model.Events != null)
                    {
                        UpParam.Events = Chk_EventsList_Tag;
                    }
                    UpParam.EventsOther = "";
                    UpParam.AccountingParty = model.Rad_Acc_Party_AB == "0" ? false : true;
                    UpParam.Rec_ID = model.Rec_ID;
                    EditParam.param_UpdateAddresses = UpParam;
                    if (ReplicateChange)
                    {
                        Parameter_Update_Addresses UpParam_NextYear = new Parameter_Update_Addresses();
                        CopyObject(UpParam, UpParam_NextYear);
                        UpParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        UpParam_NextYear.ReplicationUpdate = true;
                        EditParam.param_UpdateAddresses_NextYear = UpParam_NextYear;
                    }
                    EditParam.RecID_DeleteMagazine = model.Rec_ID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteMagazine_NextYear = ReplicationRecID;
                    }
                    //int ctr = 0;
                    List<Parameter_InsertMagazine_Addresses> InMagInfo = new List<Parameter_InsertMagazine_Addresses>();
                    List<Parameter_InsertMagazine_Addresses> InMagInfo_NextYear = new List<Parameter_InsertMagazine_Addresses>();
                    if (model.Magazine != null && model.Magazine.Count > 0)
                    {
                        foreach (var currSelection in model.Magazine.Where(x => x.Selected == true))
                        {
                            Parameter_InsertMagazine_Addresses InMag = new Parameter_InsertMagazine_Addresses();
                            InMag.AB_Rec_ID = model.Rec_ID;
                            InMag.Magazine_Misc_ID = currSelection.Magazine_Misc_ID;
                            InMag.Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                            InMag.Rec_ID = System.Guid.NewGuid().ToString();
                            InMagInfo.Add(InMag);
                            if (ReplicateChange)
                            {
                                Parameter_InsertMagazine_Addresses InMag_NextYear = new Parameter_InsertMagazine_Addresses();
                                CopyObject(InMag, InMag_NextYear);
                                InMag_NextYear.AB_Rec_ID = ReplicationRecID;
                                InMag_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InMagInfo_NextYear.Add(InMag_NextYear);
                            }
                        }
                    }
                    EditParam.InsertMagazine = InMagInfo.ToArray();
                    if (ReplicateChange)
                    {
                        EditParam.InsertMagazine_NextYear = InMagInfo_NextYear.ToArray();
                    }
                    EditParam.RecID_DelteWings = model.Rec_ID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DelteWings_NextYear = ReplicationRecID;
                    }

                    //ctr = 0;
                    List<Parameter_InsertWings_Addresses> InWingInfo = new List<Parameter_InsertWings_Addresses>();
                    List<Parameter_InsertWings_Addresses> InWingInfo_NextYear = new List<Parameter_InsertWings_Addresses>();

                    if (model.WingsMember != null && model.WingsMember.Count > 0)
                    {
                        foreach (var currSelection in model.WingsMember.Where(x => x.Selected == true))
                        {
                            Parameter_InsertWings_Addresses InWing = new Parameter_InsertWings_Addresses();
                            InWing.AB_Rec_ID = model.Rec_ID;
                            InWing.Wings_Misc_ID = currSelection.Wings_Misc_ID;
                            InWing.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InWing.Rec_ID = System.Guid.NewGuid().ToString();
                            InWingInfo.Add(InWing);
                            if (ReplicateChange)
                            {
                                Parameter_InsertWings_Addresses InWing_NextYear = new Parameter_InsertWings_Addresses();
                                CopyObject(InWing, InWing_NextYear);
                                InWing_NextYear.AB_Rec_ID = ReplicationRecID;
                                InWing_NextYear.Rec_ID = Guid.NewGuid().ToString();
                                InWingInfo_NextYear.Add(InWing_NextYear);
                            }
                            //ctr++;
                        }
                    }
                    EditParam.InsertWings = InWingInfo.ToArray();
                    if (ReplicateChange)
                    {
                        EditParam.InsertWings_NextYear = InWingInfo_NextYear.ToArray();
                    }

                    EditParam.RecID_DeleteSpeciality = model.Rec_ID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteSpeciality_NextYear = ReplicationRecID;
                    }
                    //ctr = 0;
                    List<Parameter_InsertSpecialities_Addresses> InSpecInfo = new List<Parameter_InsertSpecialities_Addresses>();
                    List<Parameter_InsertSpecialities_Addresses> InSpecInfo_NextYear = new List<Parameter_InsertSpecialities_Addresses>();

                    if (model.Specialities != null && model.Specialities.Count > 0)
                    {
                        foreach (var currSelection in model.Specialities.Where(x => x.Selected == true))
                        {
                            Parameter_InsertSpecialities_Addresses InSpec = new Parameter_InsertSpecialities_Addresses();
                            InSpec.AB_Rec_ID = model.Rec_ID;
                            InSpec.Specialities_Misc_ID = currSelection.Specialities_Misc_ID;
                            InSpec.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                            InSpec.Rec_ID = System.Guid.NewGuid().ToString();
                            InSpecInfo.Add(InSpec);
                            if (ReplicateChange)
                            {
                                Parameter_InsertSpecialities_Addresses InSpec_NextYear = new Parameter_InsertSpecialities_Addresses();
                                CopyObject(InSpec, InSpec_NextYear);
                                InSpec_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InSpec_NextYear.AB_Rec_ID = ReplicationRecID;
                                InSpecInfo_NextYear.Add(InSpec_NextYear);
                            }
                            //ctr++;
                        }
                    }
                    EditParam.InsertSpecialities = InSpecInfo.ToArray();
                    if (ReplicateChange)
                    {
                        EditParam.InsertSpecialities_NextYear = InSpecInfo_NextYear.ToArray();
                    }
                    EditParam.RecID_DeleteEvents = model.Rec_ID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteEvents_NextYear = ReplicationRecID;
                    }
                    //ctr = 0;
                    List<Parameter_InsertEvents_Addresses> InEventsInfo = new List<Parameter_InsertEvents_Addresses>();
                    List<Parameter_InsertEvents_Addresses> InEventInfo_NextYear = new List<Parameter_InsertEvents_Addresses>();

                    if (model.Events != null && model.Events.Count > 0)
                    {
                        foreach (var currSelection in model.Events.Where(x => x.Selected == true))
                        {
                            Parameter_InsertEvents_Addresses InEvents = new Parameter_InsertEvents_Addresses();
                            InEvents.AB_Rec_ID = model.Rec_ID;
                            InEvents.Events_Misc_ID = currSelection.Events_Misc_ID;
                            InEvents.Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                            InEvents.Rec_ID = System.Guid.NewGuid().ToString();
                            InEventsInfo.Add(InEvents);
                            if (ReplicateChange)
                            {
                                Parameter_InsertEvents_Addresses InEvents_NextYear = new Parameter_InsertEvents_Addresses();
                                InEvents_NextYear.AB_Rec_ID = ReplicationRecID;
                                InEvents_NextYear.Rec_ID = System.Guid.NewGuid().ToString();
                                InEvents_NextYear.Events_Misc_ID = currSelection.Events_Misc_ID;
                                InEventInfo_NextYear.Add(InEvents_NextYear);
                            }
                            //ctr++;
                        }
                    }
                    EditParam.InsertEvents = InEventsInfo.ToArray();
                    if (ReplicateChange)
                    {
                        EditParam.InsertEvents_NextYear = InEventInfo_NextYear.ToArray();
                    }
                    // END EVENTS UPDATE
                    if (model.Edit_Confirm == false)
                    {
                        Param_Get_Duplicates param = new Param_Get_Duplicates();
                        param.updatePAram = EditParam.param_UpdateAddresses;
                        param.Rec_ID = model.Rec_ID;
                        var Message = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                        if (Message == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Message.ToString().Length > 0)
                        {
                            jsonParam.message = Message + "<br><br>Do you still want to Update the Record?";
                            jsonParam.title = "Some Possible Duplicates!";
                            jsonParam.result = false;
                            jsonParam.isconfirm = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (BASE._Address_DBOps.UpdateAddresses_Txn(EditParam))
                    {

                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Address Book";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.Rec_ID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                Param_Txn_Delete_Addresses DelParam = new Param_Txn_Delete_Addresses();
                if (Tag == Common.Navigation_Mode._Delete)
                {
                    bool Result = true;
                    int ctr = 0;
                    DataTable Table = BASE._Address_DBOps.GetAddressRecIDs_ForAllYears(model.Rec_ID);
                    List<Param_Txn_Delete_AddressSet> DelAddressSetsAll = new List<Param_Txn_Delete_AddressSet>();
                    foreach (DataRow cRow in Table.Rows)
                    {
                        Param_Txn_Delete_AddressSet DelAddressSet = new Param_Txn_Delete_AddressSet();
                        DelAddressSet.RecID_DeleteMagazine = cRow[0].ToString();
                        DelAddressSet.RecID_DelteWings = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteSpeciality = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteEvents = cRow[0].ToString();
                        DelAddressSet.RecID_Delete = cRow[0].ToString();
                        DelAddressSetsAll.Add(DelAddressSet);
                        ctr++;
                        //  Bug #5018 fix
                    }
                    DelParam.DeleteAddressSets = DelAddressSetsAll.ToArray();
                    if (BASE._Address_DBOps.DeleteAddresses_Txn(DelParam))
                    {

                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Address Book";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.Rec_ID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult DataNavigation(string ActionMethod, string ID, DateTime? Edit_Date)
        {
            try
            {
                if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Delete")) && ActionMethod == "Delete")
                {
                    return Json(new { Message = "Not Allowed", result = "NoDeleteRights" }, JsonRequestBehavior.AllowGet);
                }
                if (ActionMethod == "Edit")
                {
                    string xTemp_ID = ID;
                    DateTime Edit_date = Convert.ToDateTime(Edit_Date);
                    if ((!(BASE._open_User_Type.ToUpper() == Common.ClientUserType.SuperUser.ToUpper())
                    && !(BASE._open_User_Type.ToUpper() == Common.ClientUserType.Auditor.ToUpper())))
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            return Json(new { Message = "Party Cannot be updated...!<br><br>Final Audit has been Completed for this year.", result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    object MaxValue = 0;
                    MaxValue = BASE._Address_DBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        return Json(new { Message = "Entry Not Found...!", result = false }, JsonRequestBehavior.AllowGet);
                    }

                    if ((int)MaxValue == (int)Common.Record_Status._Locked)
                    {
                        return Json(new { Message = "Locked Entry cannot be Edited/Deleted...!<br><br>Note:<br>-------Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (ActionMethod == "Delete")
                {
                    bool Result = false;
                    if ((!(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                                                && !(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            return Json(new { Message = "Party Cannot be deleted . . . !<br><br>Final Audit has been Completed for this year.", result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    string xTemp_ID = ID;
                    object MaxValue = 0;
                    MaxValue = BASE._Address_DBOps.GetStatus(xTemp_ID);
                    if (MaxValue == null)
                    {
                        return Json(new { Message = "Entry Not Found...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                    {
                        return Json(new { Message = "Locked Entry cannot be Edit/Delete...!<br><br>Note:<br>-------<br><br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    bool DeleteAllow = false;
                    string UsedPage = "";
                    DeleteAllow = CheckAddressUsage(xTemp_ID, ref UsedPage);
                    if (DeleteAllow)
                    {
                        Param_Txn_Delete_Addresses DelParam = new Param_Txn_Delete_Addresses();
                        int ctr = 0;
                        DataTable Table = BASE._Address_DBOps.GetAddressRecIDs_ForAllYears(xTemp_ID);
                        List<Param_Txn_Delete_AddressSet> DelAddressSetsAll = new List<Param_Txn_Delete_AddressSet>();
                        foreach (DataRow cRow in Table.Rows)
                        {
                            Param_Txn_Delete_AddressSet DelAddressSet = new Param_Txn_Delete_AddressSet();
                            DelAddressSet.RecID_DeleteMagazine = Convert.ToString(cRow[0]);
                            DelAddressSet.RecID_DelteWings = Convert.ToString(cRow[0]);
                            DelAddressSet.RecID_DeleteSpeciality = Convert.ToString(cRow[0]);
                            DelAddressSet.RecID_DeleteEvents = Convert.ToString(cRow[0]);
                            DelAddressSet.RecID_Delete = Convert.ToString(cRow[0]);
                            DelAddressSetsAll.Add(DelAddressSet);
                            ctr += 1;
                        }

                        DelParam.DeleteAddressSets = DelAddressSetsAll.ToArray();
                        if (!BASE._Address_DBOps.DeleteAddresses_Txn(DelParam))
                        {
                            Result = false;
                        }
                        else
                        {
                            Result = true;
                        }
                        // End If
                        if (Result)
                        {
                            return Json(new { Message = Messages.DeleteSuccess, result = true }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { Message = Messages.SomeError, result = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Message = "Can't Delete Record <br><br>This information is being used in Another Page...!<br><br>Name : " + UsedPage, result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Message = "", result = true }, JsonRequestBehavior.AllowGet);
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
        public bool CheckAddressUsage(string AB_ID, ref string UsedPage)
        {
            bool DeleteAllow = false;
            UsedPage = "";
            DataTable MaxCount = null;
            MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, AB_ID);
            if (MaxCount.Rows.Count > 0)
            {
                if ((int)MaxCount.Rows[0]["CNT"] > 0)
                {
                    DeleteAllow = false;
                    UsedPage = "Voucher Entry...";
                    if ((int)MaxCount.Rows[0]["YEARID"] != BASE._open_Year_ID)
                    {
                        UsedPage = UsedPage + "(Year:" + MaxCount.Rows[0]["YEARID"] + ")";
                    }
                }
            }
            else
            {
                DeleteAllow = true;
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.ADVANCES_INFO, AB_ID);
                if (MaxCount.Rows.Count > 0)
                {
                    if ((int)MaxCount.Rows[0]["CNT"] > 0)
                    {
                        DeleteAllow = false;
                        UsedPage = "Advances Information...";
                        if ((int)MaxCount.Rows[0]["YEARID"] != BASE._open_Year_ID)
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.DEPOSITS_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Other Deposits Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LIABILITIES_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Liabilities Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Vehicles Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.BANK_ACCOUNT_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Bank Account Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.CENTRE_SUPPORT_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Core Information...";
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if (Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0)
                    {
                        DeleteAllow = false;
                        UsedPage = "Land and Building Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }

            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.SERVICE_PLACE_INFO, AB_ID);
                if (MaxCount.Rows.Count > 0)
                {
                    if (Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0)
                    {
                        DeleteAllow = false;
                        UsedPage = "Service Places Information...";
                        if ((Convert.ToInt32(MaxCount.Rows[0]["YEARID"]) != BASE._open_Year_ID))
                        {
                            UsedPage = (UsedPage + ("(Year:"
                                        + (MaxCount.Rows[0]["YEARID"] + ")")));
                        }
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MEMBERSHIP_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Membership Information...";
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            if (DeleteAllow)
            {
                MaxCount = BASE._Address_DBOps.GetAddressUsageCount(Common_Lib.RealTimeService.Tables.MAGAZINE_MEMBERSHIP_INFO, AB_ID);
                if ((MaxCount.Rows.Count > 0))
                {
                    if ((Convert.ToInt32(MaxCount.Rows[0]["CNT"]) > 0))
                    {
                        DeleteAllow = false;
                        UsedPage = "Magazine Membership Register...";
                    }
                }
                else
                {
                    DeleteAllow = true;
                }
            }
            return DeleteAllow;
        }
        private void CopyObject(object SourceObj, object DestObj)
        {
            PropertyInfo[] destinationProperties = DestObj.GetType().GetProperties();
            foreach (PropertyInfo destinationPI in destinationProperties)
            {
                PropertyInfo sourcePI = SourceObj.GetType().GetProperty(destinationPI.Name);
                destinationPI.SetValue(DestObj, sourcePI.GetValue(SourceObj, null), null);
            }
        }
        #region Merge party
        [HttpGet]
        public ActionResult Frm_Party_Merge_Window(string ID,bool NextYearMerge,string Name="",string Edit_Date= "")
        {
            PartyMerge model = new PartyMerge();
            //GetPartyList();
            //var Party_Data = Addressbook_ExportData.Where(x => x.ID == ID).FirstOrDefault();
            model.Info_LastEditedOn = Convert.ToDateTime(Edit_Date);
            model.Merged_Party_AB = Name;
            model.Merged_Party_ID_AB = ID;
            model.ReplicateParty_PartyMerge = NextYearMerge;
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Party_Merge_Window(PartyMerge model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(model.GLookUp_Party_AB))
                {
                    jsonParam.message = "Please Select Party To Which " + model.Merged_Party_AB + " Is To Be Merged...!";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    jsonParam.focusid = "GLookUp_Party_AB";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                Param_MergeParties Inparam = new Param_MergeParties();
                Inparam.Merged_AB_ID = model.Merged_Party_ID_AB;
                Inparam.Target_AB_ID = model.GLookUp_Party_AB;
                Inparam.MergeInNextYearToo = model.ReplicateParty_PartyMerge;
                string _Result = BASE._Address_DBOps.MergeParties(Inparam);
                if (_Result.Length > 0)
                {
                    jsonParam.message = _Result + "<br><br>Party Could Not Be Merged!";
                    jsonParam.title = "Error...";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = model.Merged_Party_AB + " Merged Successfully!";
                jsonParam.title = "Merge Party";
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Get_Party_List(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTabelToPartyList(BASE._ServPlacesDBOps.GetAddresses()), loadOptions)), "application/json");
        }
        #endregion
        #region Start--> Procedures
        public void Get_Master_Data(ref Address_Book_ViewModel model)
        {
            DataTable d1 = BASE._Address_DBOps.GetAllMasters("Name", "ID");
            DataView DV1 = new DataView(d1);
            DV1.RowFilter = " [MASTERID]='TITLE' OR [MASTERID]='BLANK' ";
            DV1.Sort = "Name";
            model.Title_DS = DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable());
            DataView DV2 = new DataView(d1);
            DV2.RowFilter = " [MASTERID]='OCCUPATION' OR [MASTERID]='BLANK' ";
            DV2.Sort = "Name";
            model.Occupation_DS = DatatableToModel.DataTabletoTitle_INFO(DV2.ToTable());
            DataView DV3 = new DataView(d1);
            DV3.RowFilter = " [MASTERID]=\'STATUS CATEGORY\' OR [MASTERID]=\'BLANK\' ";
            DV3.Sort = "Sr";
            model.Category_DS = DatatableToModel.DataTabletoTitle_INFO(DV3.ToTable());
            DataView DV4 = new DataView(d1);
            DV4.RowFilter = " [MASTERID]='CONTACT MODE' OR [MASTERID]='BLANK' ";
            DV4.Sort = "Sr";
            model.ContactMode_DS = DatatableToModel.DataTabletoTitle_INFO(DV4.ToTable());
            DataView DV5 = new DataView(d1);
            DV5.RowFilter = " [MASTERID]='EVENTS' ";
            DV5.Sort = "Sr";
            model.EventsList_DS = DatatableToModel.DataTabletoTitle_INFO(DV5.ToTable());
            DataView DV6 = new DataView(d1);
            DV6.RowFilter = " [MASTERID]='MAGAZINE' ";
            DV6.Sort = "Sr";
            model.MagazineList_DS = DatatableToModel.DataTabletoTitle_INFO(DV6.ToTable());
            DataView DV7 = new DataView(d1);
            DV7.RowFilter = " [MASTERID]='SPECIALTIES' ";
            model.SpecialitiesList_DS = DatatableToModel.DataTabletoTitle_INFO(DV7.ToTable());
            DataView DV8 = new DataView(d1);
            DV8.RowFilter = " [MASTERID]='DESGINATIONS' OR [MASTERID]='BLANK'   ";
            DV8.Sort = "Name";
            model.Designation_DS = DatatableToModel.DataTabletoTitle_INFO(DV8.ToTable());
            DataView DV9 = new DataView(d1);
            DV9.RowFilter = " [MASTERID]='QUALIFICATIONS' OR [MASTERID]='BLANK'   ";
            DV9.Sort = "Name";
            model.Education_DS = DatatableToModel.DataTabletoTitle_INFO(DV9.ToTable());
            DataView DV10 = new DataView(BASE._Address_DBOps.GetWings());
            DV10.Sort = "Name";
            model.WingsList_DS = DatatableToModel.DataTabletoTitle_INFO(DV10.ToTable());
        }
        public List<Organization_INFO> Get_Org_List()
        {
            return DatatableToModel.DataTabletoOrganization_INFO(BASE._Address_DBOps.GetOrgList());
        }
        public void LookUp_GetCountryList(ref Address_Book_ViewModel model)
        {
            DataTable d1 = BASE._Address_DBOps.GetCountries("R_CO_NAME", "R_CO_CODE", "R_CO_REC_ID");
            DataView dview = new DataView(d1);
            dview.Sort = "R_CO_NAME";
            model.Country_R_DS = DatatableToModel.DataTabletoCountry_INFO(dview.ToTable());
            model.Country_O_DS = model.Country_R_DS;
            if (model.ActionMethod == Common.Navigation_Mode._New)
            {
                model.GLookUp_RCountryList_AB = "f9970249-121c-4b8f-86f9-2b53e850809e";
                model.GLookUp_OCountryList_AB = "f9970249-121c-4b8f-86f9-2b53e850809e";
                model.State_R_DS = GetStateList("IN");
                model.State_O_DS = model.State_R_DS;
            }
        }
        public List<DbOperations.Return_StateList> GetStateList(string CountryCode)
        {
            List<DbOperations.Return_StateList> d1 = BASE._Address_DBOps.GetStates(CountryCode, "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            return d1.OrderBy(x => x.R_ST_NAME).ToList();
        }
        public ActionResult GetStateListOnRefresh(string CountryCode)
        {
            return Content(JsonConvert.SerializeObject(GetStateList(CountryCode)), "application/json");
        }
        public List<DbOperations.Return_DistrictList> GetDistrictList(string CountryCode, string StateCode)
        {
            StateCode = StateCode == "" ? null : StateCode;
            List<DbOperations.Return_DistrictList> d1 = BASE._Address_DBOps.GetDistricts(CountryCode, StateCode, "R_DI_NAME", "R_DI_REC_ID");
            return d1.OrderBy(x => x.R_DI_NAME).ToList();
        }
        public ActionResult GetDistrictListOnRefresh(string CountryCode, string StateCode)
        {
            return Content(JsonConvert.SerializeObject(GetDistrictList(CountryCode, StateCode)), "application/json");
        }
        public List<DbOperations.Return_CityList> GetCityList(string CountryID, string CountryCode, string StateCode)
        {
            StateCode = StateCode == "" ? null : StateCode;
            List<DbOperations.Return_CityList> d1 = new List<DbOperations.Return_CityList>();
            if (CountryID == "f9970249-121c-4b8f-86f9-2b53e850809e")
            {
                d1 = BASE._Address_DBOps.GetCitiesBySt_Co_Code(CountryCode, StateCode, "R_CI_NAME", "R_CI_REC_ID");
            }
            else
            {
                d1 = BASE._Address_DBOps.GetCitiesByCO_Code(CountryCode, "R_CI_NAME", "R_CI_REC_ID");
            }
            return d1.OrderBy(x => x.R_CI_NAME).ToList();
        }
        public ActionResult GetCityListOnRefresh(string CountryID, string CountryCode, string StateCode)
        {
            return Content(JsonConvert.SerializeObject(GetCityList(CountryID, CountryCode, StateCode)), "application/json");
        }
        public ActionResult GetCentreList(int? CenterCategory)
        {
            DataTable D1;
            if (CenterCategory == 0)
            {
                D1 = BASE._Address_DBOps.GetCenterList();
            }
            else
            {
                D1 = BASE._Address_DBOps.GetOverseasCenterList();
            }
            DataView dview = new DataView(D1);
            dview.Sort = "CEN_NAME";
            return Content(JsonConvert.SerializeObject(dview.ToTable()), "application/json");
        }          
        #endregion
        [HttpPost]
        public ActionResult Upload()
        {
            var myFile = Request.Files["AB_file-uploader"];
            string[] imageExtensions = { ".jpg", ".jpeg", ".png" };
            var fileName = myFile.FileName.ToLower();
            var isValidExtenstion = imageExtensions.Any(ext =>
            {
                return fileName.LastIndexOf(ext) > -1;
            });
            if (isValidExtenstion)
            {
                BinaryReader reader = new BinaryReader(myFile.InputStream);
                byte[] imageBytes = reader.ReadBytes((int)myFile.ContentLength);
                AB_Image = imageBytes;
            }
            return new EmptyResult();
        }
        public void RemoveABImage()
        {
            BASE._SessionDictionary.Remove("AB_Image_ABWindow");
        }
        public ActionResult AB_PreviewImageControl()
        {
            ViewBag.AB_Asset_Image = AB_Image;
            return View();
        }
        #region "Small"
        [HttpGet]
        public ActionResult Frm_Address_Info_Window_Small(string PopUpId = "", string ButtonID = "", string ActionMethod = "", string ID = "", string PostSuccessFunction = "AddressBookSmallSuccessForm", string Party_DataGridID = "", string NewFunctionName = "", string DropDownFunctionName = "", string EditedOn = null, string CountryID = "", string StateID = "", string DistrictID = "", string CityID = "", string CallingScreen = "")
        {
            AddressBookSmall model = new AddressBookSmall();
            ViewBag.CallingScreen = CallingScreen;
            model.PopUpId = PopUpId;
            model.PostSuccessFunction = PostSuccessFunction;
            model.DropDownFunctionName = DropDownFunctionName;
            model.NewFunctionName = NewFunctionName;
            model.Party_DataGridID = Party_DataGridID;
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.TempActionMethod = model.ActionMethod.ToString();
            model.xID = ID;
            model.Titlex_ABSmall = "Contact";
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            string message = "";
            if ((ActionMethod == "New" || ActionMethod == "New_From_Selection") && model.Txt_Name_ABSmall != null)
            {
                model.Lbl_ContactName_ABSmall = model.Txt_Name_ABSmall;
            }
            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                DataTable d1 = BASE._Address_DBOps.GetRecord(model.xID);
                if (d1 == null)
                {
                    message = Messages.RecordChanged("Current Contact");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','" + message + "','Record Changed / Removed in Background!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    message = Messages.RecordChanged("Current Contact");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','" + message + "','Record Changed / Removed in Background!!');</script>");
                }
                if (EditedOn != null)
                {
                    if (BASE.AllowMultiuser())
                    {
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._Delete)
                        {
                            if (Convert.ToDateTime(EditedOn) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                            {
                                message = Messages.RecordChanged("Current Contact");
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','" + message + "','Record Already Changed!!');</script>");
                            }
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Org_RecID = d1.Rows[0]["C_ORG_REC_ID"].ToString();
                model.Txt_Name_ABSmall = d1.Rows[0]["C_NAME"].ToString();
                model.Lbl_ContactName_ABSmall = model.Txt_Name_ABSmall;
                model.Txt_Remark1_ABSmall = d1.Rows[0]["C_REMARKS_1"].ToString();
                model.Txt_Remark2_ABSmall = d1.Rows[0]["C_REMARKS_2"].ToString();
                model.Txt_R_Add1_ABSmall = d1.Rows[0]["C_R_ADD1"].ToString();
                model.Txt_R_Add2_ABSmall = d1.Rows[0]["C_R_ADD2"].ToString();
                model.Txt_R_Add3_ABSmall = d1.Rows[0]["C_R_ADD3"].ToString();
                model.Txt_R_Add4_ABSmall = d1.Rows[0]["C_R_ADD4"].ToString();
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_COUNTRY_ID"]))
                {
                    if (d1.Rows[0]["C_R_COUNTRY_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RCountryList_ABSmall = d1.Rows[0]["C_R_COUNTRY_ID"].ToString();
                    }
                }
                else
                {
                    model.GLookUp_RCountryList_ABSmall = "f9970249-121c-4b8f-86f9-2b53e850809e";
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_STATE_ID"]))
                {
                    if (d1.Rows[0]["C_R_STATE_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RStateList_ABSmall = d1.Rows[0]["C_R_STATE_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_DISTRICT_ID"]))
                {
                    if (d1.Rows[0]["C_R_DISTRICT_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_RDistrictList_ABSmall = d1.Rows[0]["C_R_DISTRICT_ID"].ToString();
                    }
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_SUB_CITY_ID"]))
                {
                    model.PC_City_Name_ABSmall = d1.Rows[0]["C_R_SUB_CITY_ID"].ToString();
                }
                else
                {
                    model.PC_City_Name_ABSmall = d1.Rows[0]["C_R_CITY_ID"].ToString();
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_R_PINCODE"]))
                {
                    model.Txt_R_Pincode_ABSmall = d1.Rows[0]["C_R_PINCODE"].ToString();
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_TEL_NO_R_1"]))
                {
                    model.Txt_R_Tel_1_ABSmall = d1.Rows[0]["C_TEL_NO_R_1"].ToString();
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_MOB_NO_1"]))
                {
                    model.Txt_Mob_1_ABSmall = d1.Rows[0]["C_MOB_NO_1"].ToString();
                }
                if (!Convert.IsDBNull(d1.Rows[0]["C_EMAIL_ID_1"]))
                {
                    model.Txt_Email1_ABSmall = d1.Rows[0]["C_EMAIL_ID_1"].ToString();
                }
            }
            if (ActionMethod == "New")
            {
                if (!string.IsNullOrWhiteSpace(CountryID))
                {
                    model.GLookUp_RCountryList_ABSmall = CountryID;
                }
                else
                {
                    model.GLookUp_RCountryList_ABSmall = "f9970249-121c-4b8f-86f9-2b53e850809e";
                }
                if (!string.IsNullOrWhiteSpace(StateID))
                {
                    model.GLookUp_RStateList_ABSmall = StateID;
                }
                if (!string.IsNullOrWhiteSpace(DistrictID))
                {
                    model.GLookUp_RDistrictList_ABSmall = DistrictID;
                }
                if (!string.IsNullOrWhiteSpace(CityID))
                {
                    model.PC_City_Name_ABSmall = CityID;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Address_Info_Window_Small(AddressBookSmall model)
        {
            var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.New_Confirm == false && model.Edit_Confirm == false)
                {
                    if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                    {
                        if (BASE._CenterDBOps.IsFinalAuditCompleted())
                        {
                            jsonParam.message = "Party Cannot Be Added/Updated/Deleted...!<br><br>Final Audit Has Been Completed For This Year.";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (BASE.AllowMultiuser())
                    {
                        if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._Delete)
                        {
                            DataTable address_DbOps = BASE._Address_DBOps.GetRecord(model.xID);
                            if (address_DbOps == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (address_DbOps.Rows.Count == 0)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Contact");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }

                            DateTime EditOn1 = (DateTime)model.LastEditedOn;
                            DateTime EditOn2 = (DateTime)address_DbOps.Rows[0]["REC_EDIT_ON"];
                            TimeSpan t = EditOn2 - EditOn1;
                            if (t.TotalSeconds >= 1 || t.TotalSeconds <= -1)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Contact");
                                jsonParam.title = "Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (Tag == Common.Navigation_Mode._Delete)
                        {
                            object MaxValue = 0;
                            MaxValue = BASE._Address_DBOps.GetStatus(model.xID);
                            if (MaxValue == null)
                            {
                                jsonParam.message = "Entry Not Found...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if ((Common.Record_Status)MaxValue == Common.Record_Status._Locked)
                            {
                                jsonParam.message = "Locked Entry can not be Changed...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            bool DeleteAllow = false;
                            string UsedPage = "";
                            DataTable MaxCount = null;
                            DeleteAllow = CheckAddressUsage(model.xID, ref UsedPage);
                            if (!DeleteAllow)
                            {
                                jsonParam.message = "Can't Delete...!<br><br>This Contact Is Being Refered In Another Record in Current Or Other Years...!<br><br>Name: " + UsedPage;
                                jsonParam.title = "Warning...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    if (Tag == Common.Navigation_Mode._Edit)
                    {
                        DataTable d1 = BASE._Membership_DBOps.GetUsageAsPastMember(model.Org_RecID);
                        d1 = BASE._Donation_DBOps.GetUsageAsPastDonor(model.xID);
                        if (d1.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_ABSmall))
                            {
                                jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            if (model.GLookUp_RCountryList_ABSmall == "f9970249-121c-4b8f-86f9-2b53e850809e")
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_ABSmall) || string.IsNullOrWhiteSpace(model.PC_City_Name_ABSmall)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RDistrictList_ABSmall)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RStateList_ABSmall)
                           || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_ABSmall))
                                {

                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1 , City, District, State, Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_ABSmall) || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_ABSmall))
                                {
                                    jsonParam.message = "Address Incomplete for a Existing Donor...!<br><br>Residence Address Line1,Country must be mentioned for a Party used in Donation Voucher/ Gift Voucher / Journal Adjustment of Gift.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }         
                        d1 = BASE._Donation_DBOps.GetUsageAsPastForeignDonor(model.xID);
                        if (d1.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(model.Txt_R_Add1_ABSmall) || string.IsNullOrWhiteSpace(model.PC_City_Name_ABSmall) || string.IsNullOrWhiteSpace(model.GLookUp_RCountryList_ABSmall))
                            {
                                jsonParam.message = "Address Incomplete For A Existing Donor...!<br><br>Residence Address Line1,City,Country must Be Mentioned For Foreign Donation Voucher";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    int i = 0;
                    string SelectedItems = "";
                    if (Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New)
                    {
                        if (string.IsNullOrWhiteSpace(model.Txt_Name_ABSmall) || model.Txt_Name_ABSmall.Trim() == "-- Not Specified --")
                        {
                            jsonParam.message = "Name Cannot Be Blank...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Name_ABSmall";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (!string.IsNullOrWhiteSpace(model.Txt_Email1_ABSmall))
                        {
                            if (BASE.IsEmail(model.Txt_Email1_ABSmall) == false)
                            {
                                jsonParam.message = "Email ID Incorrect...!";
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Email1_ABSmall";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (string.IsNullOrWhiteSpace(model.Txt_R_Tel_1_ABSmall) && string.IsNullOrWhiteSpace(model.Txt_Mob_1_ABSmall))
                        {
                            jsonParam.message = "Either Mobile No. Or LandLine No. Is Required";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Mob_1_ABSmall";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                Param_Txn_Insert_Addresses InNewParam = new Param_Txn_Insert_Addresses();
                if (Tag == Common.Navigation_Mode._New)
                {
                    model.xID = System.Guid.NewGuid().ToString();
                    string ReplicationID = System.Guid.NewGuid().ToString();
                    string STR1 = "";
                    Parameter_Insert_Addresses InParam = new Parameter_Insert_Addresses();
                    InParam.Title = "";
                    InParam.Name = model.Txt_Name_ABSmall.Replace("'", "`");
                    InParam.Gender = "";
                    InParam.OrgName = "";
                    InParam.Designation = "";
                    InParam.Education = "";
                    InParam.Reference = "";
                    InParam.Remarks1 = model.Txt_Remark1_ABSmall == null ? "" : model.Txt_Remark1_ABSmall.Replace("'", "`");
                    InParam.Remarks2 = model.Txt_Remark2_ABSmall == null ? "" : model.Txt_Remark2_ABSmall.Replace("'", "`");
                    InParam.BloodGroup = "";
                    InParam.PANNo = "";
                    InParam.VAT_TIN = "";
                    InParam.CST_TIN = "";
                    InParam.TAN = "";
                    InParam.UID = "";
                    InParam.STRNo = "";
                    InParam.PassportNo = "";
                    InParam.Magazine = null;
                    InParam.Res_Add1 = model.Txt_R_Add1_ABSmall == null ? "" : model.Txt_R_Add1_ABSmall.Replace("'", "`");
                    InParam.Res_Add2 = model.Txt_R_Add2_ABSmall == null ? "" : model.Txt_R_Add2_ABSmall.Replace("'", "`");
                    InParam.Res_Add3 = model.Txt_R_Add3_ABSmall == null ? "" : model.Txt_R_Add3_ABSmall.Replace("'", "`");
                    InParam.Res_Add4 = model.Txt_R_Add4_ABSmall == null ? "" : model.Txt_R_Add4_ABSmall.Replace("'", "`");
                    InParam.Res_cityID = model.PC_City_Name_ABSmall;
                    InParam.Res_city = model.PC_City_Name_Text_ABSmall == null ? null : model.PC_City_Name_Text_ABSmall.Replace("'", "`");
                    InParam.Res_StateID = model.GLookUp_RStateList_ABSmall;
                    InParam.Res_DisttID = model.GLookUp_RDistrictList_ABSmall;
                    InParam.Res_CountryID = model.GLookUp_RCountryList_ABSmall;
                    InParam.SubCityID = null;
                    InParam.Res_PinCode = model.Txt_R_Pincode_ABSmall == null ? "" : model.Txt_R_Pincode_ABSmall.Replace("'", "`");
                    InParam.ResTel1 = model.Txt_R_Tel_1_ABSmall == null ? "" : model.Txt_R_Tel_1_ABSmall.Replace("'", "`");
                    InParam.Mob1 = model.Txt_Mob_1_ABSmall == null ? "" : model.Txt_Mob_1_ABSmall.Replace("'", "`");
                    InParam.Email1 = model.Txt_Email1_ABSmall == null ? "" : model.Txt_Email1_ABSmall.Replace("'", "`");
                    InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    InParam.Rec_ID = model.xID;
                    InParam.OrgAB_RecId = model.xID;
                    InParam.YearID = BASE._open_Year_ID;
                    InNewParam.param_InsertAddresses = InParam;
                    if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                    {
                        Parameter_Insert_Addresses InParam_NextYear = new Parameter_Insert_Addresses();
                        CopyObject(InParam, InParam_NextYear);
                        InParam_NextYear.Rec_ID = ReplicationID;
                        InParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        InNewParam.param_InsertAddresses_NextYear = InParam_NextYear;
                    }
                    int ctr = 0;
                    Parameter_InsertMagazine_Addresses[] InMagInfo = new Parameter_InsertMagazine_Addresses[0];
                    Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Parameter_InsertMagazine_Addresses[0];
                    InNewParam.InsertMagazine = InMagInfo;
                    InNewParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                    Parameter_InsertWings_Addresses[] InWingInfo = new Parameter_InsertWings_Addresses[0];
                    Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Parameter_InsertWings_Addresses[0];
                    InNewParam.InsertWings = InWingInfo;
                    InNewParam.InsertWings_NextYear = InWingInfo_NextYear;
                    Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Parameter_InsertSpecialities_Addresses[0];
                    Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Parameter_InsertSpecialities_Addresses[0];
                    InNewParam.InsertSpecialities = InSpecInfo;
                    InNewParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                    Parameter_InsertEvents_Addresses[] InEventInfo = new Parameter_InsertEvents_Addresses[0];
                    Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Parameter_InsertEvents_Addresses[0];
                    InNewParam.InsertEvents = InEventInfo;
                    InNewParam.InsertEvents_NextYear = InEventInfo_NextYear;
                    //if (model.New_Confirm == false)     pritam:removed it for audit registration process
                    //{
                    //    Param_Get_Duplicates param = new Param_Get_Duplicates();
                    //    param.insertPAram = InNewParam.param_InsertAddresses;
                    //    param.Rec_ID = model.xID;

                    //    object Message = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                    //    if (Message == null)
                    //    {
                    //        jsonParam.message = Messages.SomeError;
                    //        jsonParam.title = "Error!!";
                    //        jsonParam.result = false;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //    if (Message.ToString().Length > 0)
                    //    {
                    //        jsonParam.message = "Some Possible Duplicates!<br><br>Do you still want to insert the Record?";
                    //        jsonParam.title = "Confirm..";
                    //        jsonParam.result = false;
                    //        jsonParam.isconfirm = true;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    if (BASE._Address_DBOps.InsertAddresses_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = model.Titlex_ABSmall;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Txn_Update_Addresses EditParam = new Param_Txn_Update_Addresses();
                if (Tag == Common.Navigation_Mode._Edit)
                {
                    bool ReplicateChange = false;
                    string ReplicationRecID = "";
                    if (model.Replicate_Confirm == true)
                    {
                        ReplicateChange = true;
                        ReplicationRecID = BASE._Address_DBOps.GetAddressRecID(model.xID, BASE._next_Unaudited_YearID).ToString();
                    }
                    Parameter_Update_Addresses UpParam = new Parameter_Update_Addresses();
                    UpParam.Name = model.Txt_Name_ABSmall.Replace("'", "`");
                    UpParam.Remarks1 = model.Txt_Remark1_ABSmall == null ? "" : model.Txt_Remark1_ABSmall.Replace("'", "`");
                    UpParam.Remarks2 = model.Txt_Remark2_ABSmall == null ? "" : model.Txt_Remark2_ABSmall.Replace("'", "`");
                    UpParam.Res_Add1 = model.Txt_R_Add1_ABSmall == null ? "" : model.Txt_R_Add1_ABSmall.Replace("'", "`");
                    UpParam.Res_Add2 = model.Txt_R_Add2_ABSmall == null ? "" : model.Txt_R_Add2_ABSmall.Replace("'", "`");
                    UpParam.Res_Add3 = model.Txt_R_Add3_ABSmall == null ? "" : model.Txt_R_Add3_ABSmall.Replace("'", "`");
                    UpParam.Res_Add4 = model.Txt_R_Add4_ABSmall == null ? "" : model.Txt_R_Add4_ABSmall.Replace("'", "`");
                    UpParam.Res_cityID = model.PC_City_Name_ABSmall;
                    UpParam.Res_city = model.PC_City_Name_Text_ABSmall == null ? null : model.PC_City_Name_Text_ABSmall.Replace("'", "`");
                    UpParam.Res_StateID = model.GLookUp_RStateList_ABSmall;
                    UpParam.Res_DisttID = model.GLookUp_RDistrictList_ABSmall;
                    UpParam.Res_CountryID = model.GLookUp_RCountryList_ABSmall;
                    UpParam.SubCityID = null;
                    UpParam.Res_PinCode = model.Txt_R_Pincode_ABSmall == null ? "" : model.Txt_R_Pincode_ABSmall.Replace("'", "`");
                    UpParam.ResTel1 = model.Txt_R_Tel_1_ABSmall == null ? "" : model.Txt_R_Tel_1_ABSmall.Replace("'", "`");
                    UpParam.Mob1 = model.Txt_Mob_1_ABSmall == null ? "" : model.Txt_Mob_1_ABSmall.Replace("'", "`");
                    UpParam.Email1 = model.Txt_Email1_ABSmall == null ? "" : model.Txt_Email1_ABSmall.Replace("'", "`");
                    UpParam.Status = "";
                    UpParam.Rec_ID = model.xID;
                    EditParam.param_UpdateAddresses = UpParam;
                    if (ReplicateChange)
                    {
                        Parameter_Update_Addresses UpParam_NextYear = new Parameter_Update_Addresses();
                        CopyObject(UpParam, UpParam_NextYear);
                        UpParam_NextYear.YearID = BASE._next_Unaudited_YearID;
                        UpParam_NextYear.ReplicationUpdate = true;
                        EditParam.param_UpdateAddresses_NextYear = UpParam_NextYear;
                    }
                    EditParam.RecID_DeleteMagazine = model.xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteMagazine_NextYear = ReplicationRecID;
                    }
                    Parameter_InsertMagazine_Addresses[] InMagInfo = new Parameter_InsertMagazine_Addresses[0];
                    Parameter_InsertMagazine_Addresses[] InMagInfo_NextYear = new Parameter_InsertMagazine_Addresses[0];
                    EditParam.InsertMagazine = InMagInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertMagazine_NextYear = InMagInfo_NextYear;
                    }
                    EditParam.RecID_DelteWings = model.xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DelteWings_NextYear = ReplicationRecID;
                    }
                    Parameter_InsertWings_Addresses[] InWingInfo = new Parameter_InsertWings_Addresses[0];
                    Parameter_InsertWings_Addresses[] InWingInfo_NextYear = new Parameter_InsertWings_Addresses[0];
                    EditParam.InsertWings = InWingInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertWings_NextYear = InWingInfo_NextYear;
                    }
                    EditParam.RecID_DeleteSpeciality = model.xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteSpeciality_NextYear = ReplicationRecID;
                    }
                    Parameter_InsertSpecialities_Addresses[] InSpecInfo = new Parameter_InsertSpecialities_Addresses[0];
                    Parameter_InsertSpecialities_Addresses[] InSpecInfo_NextYear = new Parameter_InsertSpecialities_Addresses[0];
                    EditParam.InsertSpecialities = InSpecInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertSpecialities_NextYear = InSpecInfo_NextYear;
                    }
                    EditParam.RecID_DeleteEvents = model.xID;
                    if (ReplicateChange)
                    {
                        EditParam.RecID_DeleteEvents_NextYear = ReplicationRecID;
                    }
                    Parameter_InsertEvents_Addresses[] InEventsInfo = new Parameter_InsertEvents_Addresses[0];
                    Parameter_InsertEvents_Addresses[] InEventInfo_NextYear = new Parameter_InsertEvents_Addresses[0];
                    EditParam.InsertEvents = InEventsInfo;
                    if (ReplicateChange)
                    {
                        EditParam.InsertEvents_NextYear = InEventInfo_NextYear;
                    }
                    //if (model.Edit_Confirm == false) pritam:removed it for audit registration process
                    //{
                    //    Param_Get_Duplicates param = new Param_Get_Duplicates();
                    //    param.updatePAram = EditParam.param_UpdateAddresses;
                    //    param.Rec_ID = model.xID;
                    //    object Message = BASE._Address_DBOps.GetDuplicateColumnMsg(param);
                    //    if (Message == null)
                    //    {
                    //        jsonParam.message = Messages.SomeError;
                    //        jsonParam.title = "Error!!";
                    //        jsonParam.result = false;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //    if (Message.ToString().Length > 0)
                    //    {
                    //        jsonParam.message = "Some Possible Duplicates!<br><br>Do you still want to Update the Record?";
                    //        jsonParam.title = "Confirm..";
                    //        jsonParam.result = false;
                    //        jsonParam.isconfirm = true;
                    //        return Json(new
                    //        {
                    //            jsonParam
                    //        }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    if (BASE._Address_DBOps.UpdateAddresses_Txn(EditParam))
                    {

                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = model.Titlex_ABSmall;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Param_Txn_Delete_Addresses DelParam = new Param_Txn_Delete_Addresses();
                if (Tag.ToString() == "_Delete")
                {
                    int ctr = 0;
                    DataTable Table = BASE._Address_DBOps.GetAddressRecIDs_ForAllYears(model.xID);
                    Param_Txn_Delete_AddressSet[] DelAddressSetsAll = new Param_Txn_Delete_AddressSet[Table.Rows.Count];
                    foreach (DataRow cRow in Table.Rows)
                    {
                        Param_Txn_Delete_AddressSet DelAddressSet = new Param_Txn_Delete_AddressSet();
                        DelAddressSet.RecID_DeleteMagazine = cRow[0].ToString();
                        DelAddressSet.RecID_DelteWings = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteSpeciality = cRow[0].ToString();
                        DelAddressSet.RecID_DeleteEvents = cRow[0].ToString();
                        DelAddressSet.RecID_Delete = cRow[0].ToString();
                        DelAddressSetsAll[ctr] = DelAddressSet;
                        ctr += 1;
                    }
                    DelParam.DeleteAddressSets = DelAddressSetsAll;
                    if (BASE._Address_DBOps.DeleteAddresses_Txn(DelParam))
                    {

                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = model.Titlex_ABSmall;
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam,
                            xid = model.xID
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RefreshCountryList_ABSmall()
        {
            DataTable d1 = BASE._Address_DBOps.GetCountries("R_CO_NAME", "R_CO_CODE", "R_CO_REC_ID");
            DataView dview = new DataView(d1);
            dview.Sort = "R_CO_NAME";
            CountryList_ABSmall = DatatableToModel.DataTabletoCountry_INFO(dview.ToTable());
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetCountryList_Res_ABSmall(DataSourceLoadOptions loadOptions)
        {
            if (CountryList_ABSmall == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Country_INFO>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(CountryList_ABSmall, loadOptions)), "application/json");
        }
        public ActionResult RefreshStateList_ABSmall(string CountryAccessibleDescription)
        {
            var d1 = BASE._Address_DBOps.GetStates(CountryAccessibleDescription, "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
            d1 = d1.OrderBy(x => x.R_ST_NAME).ToList();
            StateList_ABSmall = d1;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetStateList_Res_ABSmall(DataSourceLoadOptions loadOptions)
        {
            if (StateList_ABSmall == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Return_StateList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(StateList_ABSmall, loadOptions)), "application/json");
        }
        public ActionResult RefreshDistrictList_ABSmall(string CountryAccessibleDescription, string StateAccessibleDescription)
        {
            StateAccessibleDescription = StateAccessibleDescription == "" ? null : StateAccessibleDescription;
            var d1 = BASE._Address_DBOps.GetDistricts(CountryAccessibleDescription, StateAccessibleDescription, "R_DI_NAME", "R_DI_REC_ID");
            d1 = d1.OrderBy(x => x.R_DI_NAME).ToList();
            DistrictList_ABSmall = d1;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetDistrictList_Res_ABSmall(DataSourceLoadOptions loadOptions)
        {
            if (DistrictList_ABSmall == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Return_DistrictList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DistrictList_ABSmall, loadOptions)), "application/json");
        }
        public ActionResult RefreshCityList_ABSmall(string CountryID, string CountryAccessibleDescription, string StateAccessibleDescription)
        {
            StateAccessibleDescription = StateAccessibleDescription=="" ? null : StateAccessibleDescription;
            if (CountryID == "f9970249-121c-4b8f-86f9-2b53e850809e")
            {
                CityList_ABSmall = BASE._Address_DBOps.GetCitiesBySt_Co_Code(CountryAccessibleDescription, StateAccessibleDescription, "R_CI_NAME", "R_CI_REC_ID");
            }
            else
            {
                CityList_ABSmall = BASE._Address_DBOps.GetCitiesByCO_Code(CountryAccessibleDescription, "R_CI_NAME", "R_CI_REC_ID");
            }
            CityList_ABSmall = CityList_ABSmall.OrderBy(x => x.R_CI_NAME).ToList();
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetCityList_Res_ABSmall(DataSourceLoadOptions loadOptions)
        {
            if (CityList_ABSmall == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DbOperations.Return_CityList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(CityList_ABSmall, loadOptions)), "application/json");
        }
        #endregion
        #region "Mini"
        [HttpGet]
        public ActionResult Frm_Address_Info_Window_Mini(string PopUpId = "popup_frm_Address_Window", string ButtonID = "AddressModelNew", string ActionMethod = null, string ID = null, string PostSuccessFunction = null, string Party_DataGridID = null, string NewFunctionName = null, string DropDownFunctionName = null, string EditedOn = null)
        {
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add")) && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Update")) && ActionMethod == "Edit")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if ((!CheckRights(BASE, ClientScreen.Facility_AddressBook, "Delete")) && ActionMethod == "Delete")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Not Allowed','No Rights');</script>");
            }
            if (ActionMethod == "New")
            {
                if (BASE._open_User_Type.ToUpper() != "SUPERUSER" && BASE._open_User_Type.ToUpper() != "AUDITOR")
                {
                    if (BASE._CenterDBOps.IsFinalAuditCompleted())
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopUpId + "','Party Cannot be added...!<br><br>Final Audit has been Completed for this year.','Information...');</script>");
                    }
                }
            }
            Address_Book_ViewModel model = new Address_Book_ViewModel();
            model.PostSuccessFunction = PostSuccessFunction ?? "AjaxSuccessAddressBook_Mini";
            model.PopUpId = PopUpId ?? "popup_frm_Address_Window";
            model.NewFunctionName = NewFunctionName ?? "OnAddressNewClick";
            model.Party_DataGridID = Party_DataGridID;
            model.DropDownFunctionName = DropDownFunctionName;
            if (string.IsNullOrWhiteSpace(EditedOn) == false)
            {
                model.Info_LastEditedOn = Convert.ToDateTime(EditedOn);
            }
            model.Rec_ID = ID;
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            ViewBag.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
            return View("Frm_Address_Info_Window_Mini", model);
        }
        #endregion
        public void SessionClear_ABSmall()
        {
            ClearBaseSession("_ABSmall");
        }
        public void SessionClear()
        {
            ClearBaseSession("_AdresBuk");
            Session.Remove("AddressBookInfo_detailGrid_Data");

        }
        public void SessionClear_Window()
        {
            ClearBaseSession("_ABWindow");
        }
        public void AdresBook_user_rights()
        {
            ViewData["AdresBook_AddRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["AdresBook_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Update");
            ViewData["AdresBook_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Delete");
            ViewData["AdresBook_ExportRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Export");
            ViewData["AdresBook_ReportRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Report");
            ViewData["AdresBook_ListRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        string getContactFieldFocusID(string Contact_data, Address_Book_ViewModel model)
        {
            if (Contact_data.Contains("@"))
            {
                if (model.Txt_Email1_AB == Contact_data) return "Txt_Email1_AB";
                if (model.Txt_Email2_AB == Contact_data) return "Txt_Email2_AB";       
            }
            else
            {
                if (model.Txt_Mob_1_AB == Contact_data) return "Txt_Mob_1_AB";
                if (model.Txt_Mob_2_AB == Contact_data) return "Txt_Mob_2_AB";
           
            }
            return "";
        }
        
    }
}

