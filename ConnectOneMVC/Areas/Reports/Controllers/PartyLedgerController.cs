using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Reports.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations.Report_Ledgers;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    [CheckLogin]
    public class PartyLedgerController : BaseController
    {
        
        public ActionResult Frm_Party_Report(decimal _Opening = 0,string partyid = "",string PopUpId= null,string FromDate = "", string ToDate = "")
        {
            //SetDefaultValues();
            if (!CheckRights(BASE, ClientScreen.Report_PartyReport,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_PartyReport').hide();</script>");
            }
            ViewBag.OpenYearSdt = BASE._open_Year_Sdt;
            ViewBag.OpenYearEdt = BASE._open_Year_Edt;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_PartyReport).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.PopUpId = PopUpId != null ? PopUpId : null;       
            ViewBag.SelectedPartyValue = partyid;
            ViewBag.xFr_Date = FromDate;
            ViewBag.xTo_Date = ToDate;        
            ViewBag.OpeningValue = _Opening;
            if (string.IsNullOrWhiteSpace(FromDate))
            {
                DateTime xLastDate = DateTime.Now > BASE._open_Year_Sdt && DateTime.Now <= BASE._open_Year_Edt ? DateTime.Now : BASE._open_Year_Sdt;
                int xMM = xLastDate.Month;
                ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            }
            else 
            {
                ViewBag.PeriodSelectedIndex = 20;
            }

            ViewData["PartyLedger_ExportRight"] = CheckRights(BASE, ClientScreen.Report_PartyReport, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
            ViewData["PartyLedger_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
           || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();    

            return View();
        }
        public ActionResult PartyLedgerData(string Fr_Date, string To_Date, string PartyID, decimal Opening = 0)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            Param_GetPartyReport param = new Param_GetPartyReport();
            param.FromDate = Convert.ToDateTime(Fr_Date);
            param.ToDate = Convert.ToDateTime(To_Date);
            param.PartyId = PartyID;
            param.YearID = BASE._open_Year_ID;

            Param_GetPartyListing param_open = new Param_GetPartyListing();
            param_open.FromDate = BASE._open_Year_Sdt;
            param_open.ToDate = param.FromDate.AddDays(-1);
            param_open.PartyID = PartyID;
            List<PartyBalancesReport> _Party_opening = BASE._Reports_Ledgers_DBOps.GetPartyList(param_open);
            if (_Party_opening.Count > 0)
            {
                if (param.FromDate == BASE._open_Year_Sdt)
                {
                    Opening = _Party_opening[0].Opening_Value;
                }
                else
                {
                    Opening = _Party_opening[0].Closing_Value;
                }
            }
            else 
            {
                Opening = 0;
            }
            List<PartyLedgerReport> ledgerData = BASE._Reports_Ledgers_DBOps.GetPartyReport(param, Opening, param.FromDate);
            string lblNetBalance = "";
            if (ledgerData.Count > 0)
            {
                if (ledgerData[ledgerData.Count - 1].Balance != null)
                {
                    //lblNetBalance = "Rs. " + ledgerData[ledgerData.Count - 1].Num_Balance.ToString();
                    lblNetBalance = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "Rs. {0:N2} {1}", ledgerData[ledgerData.Count - 1].Num_Balance, ledgerData[ledgerData.Count - 1].Balance);
                }
                else
                {
                    lblNetBalance = "Rs. 0";
                }
            }
            else
            {
                lblNetBalance = "Rs. 0";
            }
            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            var result = new
            {
                jsonParam,
                Opening,
                lblNetBalance,
                GridData= ledgerData
            };
            return Content(JsonConvert.SerializeObject(result), "application/json");
            //return Json(new
            //{
            //    jsonParam,
            //    Opening,
            //    lblNetBalance,
            //    GridData = JsonConvert.SerializeObject(ledgerData)
            //}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Party_Report_DetailGrid_Data(string ID, string MID, bool VouchingMode = false)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(ID, MID, ClientScreen.Accounts_CashBook, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Party_Report_AdditionalGrid(bool VouchingMode = false, string ID = "", string MID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(ID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook)), "application/json");
        }
        public ActionResult Fill_Parties(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BASE._Reports_Ledgers_DBOps.GetParties().OrderBy(p => p.Name).ToList(), loadOptions)), "application/json");
        }
        public List<CB_Period> FillChangePeriod()
        {
            var period = new List<CB_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                CB_Period row1 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                CB_Period row2 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            CB_Period row = new CB_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            CB_Period row3 = new CB_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            CB_Period row4 = new CB_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            CB_Period row5 = new CB_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            CB_Period row6 = new CB_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            CB_Period row7 = new CB_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            CB_Period row8 = new CB_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            CB_Period row9 = new CB_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            CB_Period row10 = new CB_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            return period;
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(FillChangePeriod(), loadOptions)), "application/json");
        }
        public ActionResult Frm_PartyLedger_Specific_Period(string xFrDate = "", string xToDate = "")
        {
            CB_SpeceficPeriod model = new CB_SpeceficPeriod();
            model.CB_Fromdate = Convert.ToDateTime(xFrDate);
            model.CB_Todate = Convert.ToDateTime(xToDate);
            return View(model);
        }
        public ActionResult Frm_Export_Options(string GridName,string Name,string From,string To)
        {
            if ((!CheckRights(BASE, ClientScreen.Report_PartyReport, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_export_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridName = GridName;
            ViewBag.Filename = Name + "_" + BASE._open_UID_No + "_" + Convert.ToDateTime(From).ToString("dd-MMM-yyyy") + "_" + Convert.ToDateTime(To).ToString("dd-MMM-yyyy");
            return View("Common_Export");
        }
        #region PrintableReport
        public ActionResult PrintableReport(PartyLedgerPrintableReport model, string ReportData)
        {
            model.Data = JsonConvert.DeserializeObject<List<PartyLedgerReport>>(ReportData);
            List<PartyList> PartyData = BASE._Reports_Ledgers_DBOps.GetParties(model.PartyID);
            if (PartyData.Count > 0)
            {
                model.PAN = PartyData[0].PAN;
                model.GST = PartyData[0].GST;
                model.State = PartyData[0].State;
                model.City = PartyData[0].City;
                model.Mobile = PartyData[0].Mobile;
                model.Email = PartyData[0].Email;
            }
            model.CityState = (model.City ?? "") + ", " + (model.State ?? "");
            if (model.CityState.EndsWith(", "))
            {
                model.CityState = model.CityState.Substring(0, model.CityState.Length - 2);
            }
            model.MobileEmail = (model.Mobile ?? "") + ", " + (model.Email ?? "");
            if (model.MobileEmail.EndsWith(", "))
            {
                model.MobileEmail = model.MobileEmail.Substring(0, model.MobileEmail.Length - 2);
            }
            List<PrintableReportColumnCollection> _reportColumns = new List<PrintableReportColumnCollection>()
            {
                    new PrintableReportColumnCollection("_Date", "Vch Date",90),
                    new PrintableReportColumnCollection("InvDt", "Inv Date",90),
                    new PrintableReportColumnCollection("InvNo", "InvNo",90),
                    new PrintableReportColumnCollection("Particulars", "Particulars",150),
                   // new PrintableReportColumnCollection("Screen", "Voucher",90),
                    new PrintableReportColumnCollection("Payment_RefNo", "Payment RefNo",100),
                    new PrintableReportColumnCollection("Debit", "Debit",100,"Right"),
                    new PrintableReportColumnCollection("Credit", "Credit",100,"Right"),
                    new PrintableReportColumnCollection("Balance", "Balance",100,"Right")
            };

            XtraReport report = new XtraReport();

            //XlsxExportOptions xlsxOptions = report.ExportOptions.Xlsx;
            //xlsxOptions.ExportMode = XlsxExportMode.SingleFilePageByPage;

            report.Name = model.Name + "_" + BASE._open_UID_No + "_" + model.From.ToString("dd-MMM-yyyy") + "_" + model.To.ToString("dd-MMM-yyyy");
            report.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
            report.PaperKind = System.Drawing.Printing.PaperKind.A4;
            report.Landscape = false;
            report.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth);
            report.DataSource = model;
            report.DataMember = "Data";

            var reportWorkableWidth = report.PageWidth - report.Margins.Left - report.Margins.Right;
            float Width_75 = (reportWorkableWidth * 3) / 4;
            float Width_25 = reportWorkableWidth - Width_75;


            // Add a PageHeaderBand for institute details
            PageHeaderBand pageHeader = new PageHeaderBand();
            pageHeader.CanShrink = true;
            pageHeader.CanGrow = true;   

            XRLabel Institute_PageHeader = InstituteLabel_printableReport(); //institute
            Institute_PageHeader.WidthF = reportWorkableWidth;
            Institute_PageHeader.LocationF = new System.Drawing.PointF(0, 0);
            pageHeader.Controls.Add(Institute_PageHeader);

            XRLabel UId_PageHeader = UidLabel_printableReport(); //uid
            UId_PageHeader.WidthF = reportWorkableWidth;
            UId_PageHeader.LocationF = new System.Drawing.PointF(0, Institute_PageHeader.HeightF + 2);
            pageHeader.Controls.Add(UId_PageHeader);

            XRLine PageheaderHrLine = new XRLine(); // Horizontal Line
            PageheaderHrLine.WidthF = reportWorkableWidth;
            PageheaderHrLine.LocationF = new System.Drawing.PointF(0, UId_PageHeader.LocationF.Y + UId_PageHeader.HeightF + 2);
            pageHeader.Controls.Add(PageheaderHrLine);

            pageHeader.HeightF = PageheaderHrLine.HeightF + UId_PageHeader.HeightF + Institute_PageHeader.HeightF;
            report.Bands.Add(pageHeader);
            //----------PageHeader Ends-------------

            // Add a GroupHeaderBand for party details
            GroupHeaderBand partyGroupHeader = new GroupHeaderBand();
            partyGroupHeader.RepeatEveryPage = false;
            partyGroupHeader.CanShrink = true;
            partyGroupHeader.CanGrow = true;           

            XRPanel  partyHeaderPanel = new XRPanel();
            partyHeaderPanel.CanShrink = true;
            partyHeaderPanel.CanGrow = true;
            partyHeaderPanel.WidthF = reportWorkableWidth;            

             // Party Name (Left)
            XRLabel lblPartyName = new XRLabel();
            lblPartyName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[Name]"));
            lblPartyName.Font = new Font("Arial", 10, FontStyle.Bold);     
            lblPartyName.WidthF = Width_75;
            lblPartyName.HeightF = 20;
            lblPartyName.TextAlignment = TextAlignment.TopLeft;
            partyHeaderPanel.Controls.Add(lblPartyName);
            // Statement of Account (Right)
            XRLabel lblStatement = new XRLabel();
            lblStatement.Text = "Statement of Account";
            lblStatement.Font = new Font("Arial", 10, FontStyle.Bold);
            lblStatement.LocationF = new PointF(Width_75, 0);
            lblStatement.WidthF = Width_25;
            lblStatement.TextAlignment = TextAlignment.TopRight;
            lblStatement.HeightF = 20;
            partyHeaderPanel.Controls.Add(lblStatement);
            // City,State (Left)
            XRLabel lblCityState = new XRLabel();
            lblCityState.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[CityState]"));
            lblCityState.Font = new Font("Arial", 9, FontStyle.Regular);
            lblCityState.LocationF = new PointF(0, lblPartyName.LocationF.Y + lblPartyName.HeightF);
            lblCityState.WidthF = Width_75;
            lblCityState.TextAlignment = TextAlignment.TopLeft;
            lblCityState.CanShrink = true;
            lblCityState.CanGrow = true;
            lblCityState.HeightF = string.IsNullOrWhiteSpace(model.CityState) ? 0 : 20;
            lblCityState.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            partyHeaderPanel.Controls.Add(lblCityState);
            // Period From (Right)
            XRLabel lblPeriodFrom = new XRLabel();
            lblPeriodFrom.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'Period From: ' + FormatString('{0:dd-MMM-yy}', [From])"));
            lblPeriodFrom.Font = new Font("Arial", 9, FontStyle.Regular);
            lblPeriodFrom.LocationF = new PointF(Width_75, lblStatement.LocationF.Y + lblStatement.HeightF);
            lblPeriodFrom.WidthF = Width_25;
            lblPeriodFrom.HeightF = 20;
            lblPeriodFrom.TextAlignment = TextAlignment.TopRight;
            partyHeaderPanel.Controls.Add(lblPeriodFrom);
            // GST PAN (Left)
            XRLabel lblGSTPAN = new XRLabel();
            lblGSTPAN.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "Iif(IsNullOrEmpty([GST]), [PAN], [GST])"));
            lblGSTPAN.Font = new Font("Arial", 9, FontStyle.Regular);
            lblGSTPAN.LocationF = new PointF(0, lblCityState.LocationF.Y + lblCityState.HeightF);
            lblGSTPAN.WidthF = Width_75;
            lblGSTPAN.TextAlignment = TextAlignment.TopLeft;
            lblGSTPAN.CanShrink = true;
            lblGSTPAN.CanGrow = true;
            lblGSTPAN.HeightF = string.IsNullOrWhiteSpace(model.PAN) ? string.IsNullOrWhiteSpace(model.GST) ? 0 : 20 : 20;
            lblGSTPAN.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            partyHeaderPanel.Controls.Add(lblGSTPAN);
            // Period To (Right)
            XRLabel lblPeriodTo = new XRLabel();
            lblPeriodTo.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'Period To: ' + FormatString('{0:dd-MMM-yy}', [To])"));
            lblPeriodTo.Font = new Font("Arial", 9, FontStyle.Regular);
            lblPeriodTo.LocationF = new PointF(Width_75, lblPeriodFrom.LocationF.Y + lblPeriodFrom.HeightF);
            lblPeriodTo.WidthF = Width_25;
            lblPeriodTo.HeightF = 20;
            lblPeriodTo.TextAlignment = TextAlignment.TopRight;
            partyHeaderPanel.Controls.Add(lblPeriodTo);
            // Mobile,Email (Left)
            XRLabel lblMobilEmail = new XRLabel();
            lblMobilEmail.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[MobileEmail]"));
            lblMobilEmail.Font = new Font("Arial", 9, FontStyle.Regular);
            lblMobilEmail.LocationF = new PointF(0, lblGSTPAN.LocationF.Y + lblGSTPAN.HeightF);
            lblMobilEmail.WidthF = Width_75;
            lblMobilEmail.TextAlignment = TextAlignment.TopLeft;
            lblMobilEmail.CanShrink = true;
            lblMobilEmail.CanGrow = true;
            lblMobilEmail.HeightF = string.IsNullOrWhiteSpace(model.MobileEmail) ? 0 : 20;
            lblMobilEmail.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            partyHeaderPanel.Controls.Add(lblMobilEmail);

            partyGroupHeader.Controls.Add(partyHeaderPanel);

            XRTable partyGroupHeader_dataHeader = DataTableHeader_printableReport(_reportColumns); //data header row
            partyGroupHeader_dataHeader.WidthF = reportWorkableWidth;
            partyGroupHeader_dataHeader.LocationF = new System.Drawing.PointF(0, partyHeaderPanel.HeightF+10);
            partyGroupHeader.Controls.Add(partyGroupHeader_dataHeader);   

            report.Bands.Add(partyGroupHeader);
            //----------Party GroupHeader Ends-------------

            // Add a GroupHeaderBand for data header to show in next pages
            GroupHeaderBand dataTableHeaderGroup = new GroupHeaderBand();
            dataTableHeaderGroup.RepeatEveryPage = true;
            dataTableHeaderGroup.CanShrink = true;
            dataTableHeaderGroup.CanGrow = true;
            dataTableHeaderGroup.BeforePrint += (s, e) =>
            {
                XtraReport rep = (XtraReport)((Band)s).Report;
                GroupHeaderBand band= (GroupHeaderBand)s;
                if (rep.PrintingSystem != null)
                {
                    int currentPage = rep.PrintingSystem.Document.PageCount;
                    if (currentPage == 0) //not show in 1st page already added in party group header
                    {
                        e.Cancel = true;
                        band.Visible = false;
                        band.HeightF = 0;
                    }
                    else 
                    {
                        band.Visible = true;
                    }
                }
            };

            XRTable dataTableHeader_dataHeader = DataTableHeader_printableReport(_reportColumns); //data header row
            dataTableHeader_dataHeader.WidthF = reportWorkableWidth;
            dataTableHeader_dataHeader.LocationF = new System.Drawing.PointF(0, 0);

            dataTableHeaderGroup.Controls.Add(dataTableHeader_dataHeader);
            report.Bands.Add(dataTableHeaderGroup);
            //----------Data Header Group Ends-------------

            // Report Data
            DetailBand detailBand = new DetailBand();
            report.Bands.Add(detailBand);        

            XRTable dataTable = DataTableBody_printableReport(_reportColumns);
            dataTable.WidthF = reportWorkableWidth;
            detailBand.Controls.Add(dataTable);
            detailBand.HeightF = dataTable.HeightF;
            //----------Report Data Ends-------------

            // Report Footer
            ReportFooterBand reportFooter = new ReportFooterBand();
            report.Bands.Add(reportFooter);

            XRTable summaryTable = DataTableFooter_printableReport(_reportColumns);
            summaryTable.WidthF = reportWorkableWidth;
            reportFooter.Controls.Add(summaryTable);

            XRLabel lblEndOfStatement = new XRLabel();
            lblEndOfStatement.WidthF = reportWorkableWidth;
            lblEndOfStatement.LocationF = new System.Drawing.PointF(0, summaryTable.LocationF.Y + summaryTable.HeightF + 2);
            lblEndOfStatement.Text = "----------End of Statement----------";
            lblEndOfStatement.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblEndOfStatement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            reportFooter.Controls.Add(lblEndOfStatement);
            reportFooter.HeightF = summaryTable.HeightF + lblEndOfStatement.HeightF;
            //----------ReportFooter Ends-------------

            // Page Footer
            PageFooterBand pageFooter = new PageFooterBand();
            report.Bands.Add(pageFooter);
            pageFooter.HeightF = 40;
            XRLine footerHrLine = new XRLine(); //horizontal line
            footerHrLine.WidthF = reportWorkableWidth;
            footerHrLine.LocationF = new System.Drawing.PointF(0, 0);
            footerHrLine.BorderWidth = 1;
            footerHrLine.ForeColor = Color.Gray;
            pageFooter.Controls.Add(footerHrLine);

            XRPageInfo pageInfo = new XRPageInfo(); //page info
            pageInfo.LocationF = new System.Drawing.PointF(0, footerHrLine.LocationF.Y + footerHrLine.HeightF + 2);
            pageInfo.WidthF = reportWorkableWidth;
            pageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            pageInfo.Font = new System.Drawing.Font("Arial", 9);
            pageInfo.Format = "Page {0} of {1}";
            pageFooter.Controls.Add(pageInfo);
            //----------PageFooter Ends-------------

            return View("Frm_Export_Options", report);
        }
        public XRLabel InstituteLabel_printableReport() 
        {
            //Full name of the institute
            XRLabel lblInstituteName = new XRLabel();
            lblInstituteName.Text = BASE._open_Ins_Name;
            //lblInstituteName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "ToUpper([InstituteFullName])")); // Convert to upper case dynamically
            lblInstituteName.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold); // Adjust size as needed
            lblInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;            
            return lblInstituteName;
        }
        public XRLabel UidLabel_printableReport()
        {
            //UID Name and Number
            XRLabel lblUID = new XRLabel();
            //lblUID.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "ToUpper([UIDName]) + ' (' + [UIDNumber] + ')'")); // Concatenate dynamically
            lblUID.Text = BASE._open_Cen_Name+" ("+BASE._open_UID_No+")";
            lblUID.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular);
            lblUID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;        
            return lblUID;
        }
        public XRTable DataTableHeader_printableReport(List<PrintableReportColumnCollection> _reportColumns) 
        {
            XRTable dataheaderTable = new XRTable();
            dataheaderTable.BeginInit();
            //dataheaderTable.SizeF = new System.Drawing.SizeF(dataheaderTable.WidthF, 25); 
            dataheaderTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            dataheaderTable.BorderColor = System.Drawing.Color.Black; 
            dataheaderTable.BackColor = System.Drawing.Color.LightBlue;            

            XRTableRow headerRow = new XRTableRow();
            //headerRow.HeightF = 25;
            // Add header cells
            for (int i = 0; i < _reportColumns.Count; i++) 
            {
                XRTableCell headerTableCell = new XRTableCell();
                headerTableCell.Text = _reportColumns[i].Caption;
                headerTableCell.WidthF = _reportColumns[i].Width;
                headerTableCell.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
                headerTableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;             
                headerRow.Cells.Add(headerTableCell);
            }        

            dataheaderTable.Rows.Add(headerRow);
            dataheaderTable.AdjustSize();
            dataheaderTable.EndInit();
            return dataheaderTable;
        }
        public XRTable DataTableBody_printableReport(List<PrintableReportColumnCollection> _reportColumns)
        {
            XRTable dataTable = new XRTable();
            dataTable.BeginInit();
            dataTable.LocationF = new System.Drawing.PointF(0, 0);
            dataTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            dataTable.BorderColor = System.Drawing.Color.LightGray;

            XRTableRow dataRow = new XRTableRow();
            //dataRow.HeightF = 25;
            // Add data cells
            for (int i = 0; i < _reportColumns.Count; i++)
            {
                XRTableCell dataCell = new XRTableCell();               
                dataCell.WidthF = _reportColumns[i].Width;
                dataCell.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular);             
                dataCell.ExpressionBindings.Add(new ExpressionBinding("Text", "["+ _reportColumns[i].Name + "]"));
                if (_reportColumns[i].Name == "_Date" || _reportColumns[i].Name == "InvDt")
                {
                    dataCell.ExpressionBindings.Add(new ExpressionBinding("Text", "FormatDateString([" + _reportColumns[i].Name + "],'dd-MMM-yyyy')"));                    
                }
                else if (_reportColumns[i].Name == "Credit" || _reportColumns[i].Name == "Debit") 
                {
                    dataCell.ExpressionBindings.Add(new ExpressionBinding("Text", "FormatIndianNumber([" + _reportColumns[i].Name + "])"));
                }
                else if (_reportColumns[i].Name == "Balance")
                {
                    dataCell.ExpressionBindings.Add(new ExpressionBinding("Text", "FormatIndianNumber([Num_Balance]) + ' ' + [Balance]"));
                }
                if (_reportColumns[i].Align == "Left")
                {
                    dataCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                }
                else if (_reportColumns[i].Align == "Right")
                {
                    dataCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                else 
                {
                    dataCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                dataCell.Padding = new PaddingInfo(2, 2, 2, 2);
                dataRow.Cells.Add(dataCell);
            }  
            dataTable.Rows.Add(dataRow);
            dataTable.AdjustSize();
            dataTable.EndInit();
            return dataTable;
        }
        public XRTable DataTableFooter_printableReport(List<PrintableReportColumnCollection> _reportColumns)
        {
            XRTable summaryTable = new XRTable();
            summaryTable.BeginInit();          
            summaryTable.LocationF = new System.Drawing.PointF(0, 1);
            summaryTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            summaryTable.BorderColor = System.Drawing.Color.LightGray;

            XRTableRow summaryRow = new XRTableRow();       
            // Add data cells
            for (int i = 0; i < _reportColumns.Count; i++)
            {
                XRTableCell summaryCell = new XRTableCell();
                summaryCell.WidthF = _reportColumns[i].Width;
                summaryCell.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
                summaryCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;       
                if (_reportColumns[i].Name == "Credit" || _reportColumns[i].Name == "Debit")
                {
                    summaryCell.ExpressionBindings.Add(new ExpressionBinding("Text", "FormatIndianNumber(Sum([" + _reportColumns[i].Name + "]))"));
                    //summaryCell.Summary.Func = SummaryFunc.Sum;
                    //summaryCell.Summary.IgnoreNullValues = true;
                    //summaryCell.Summary.Running = SummaryRunning.Report;                 
                }  
                summaryRow.Cells.Add(summaryCell);             
            }
            summaryTable.Rows.Add(summaryRow);
            summaryTable.AdjustSize();
            summaryTable.EndInit();
            return summaryTable;
        }
        public void PageHeader_BeforePrint(object s, System.Drawing.Printing.PrintEventArgs e)
        {
            var rep = (XtraReport)((Band)s).Report;
            if (rep.PrintingSystem != null || rep.PrintingSystem.ExportOptions != null)
            {
                int currentPage = rep.PrintingSystem.Document.PageCount;
                if (currentPage == 0)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion PrintableReport
        public void SessionClear()
        {
            ClearBaseSession("_PartyLedger");
            Session.Remove("PartyLedger_DetailGrid_Data");
        }
    }
}