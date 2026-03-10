using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web;
namespace ConnectOneMVC.Reports
{
    public partial class DonationRecpt : DevExpress.XtraReports.UI.XtraReport
    {
        public class Details
        {
            public const string Center = "CENTER";
            public const string ReceiptNo = "RECEIPTNO";
            public const string ReceiptID = "ReceiptID";
            public const string RDate = "VDATE";
            public const string DonorPanNo = "PANNO";
            public const string Amount = "AMOUNT";
            public const string DonorName = "DONOR";
            public const string DonorAddress = "Address";
            public const string DonorArea = "DONORAREA";
            public const string DonationMode = "MODE";
            public const string DonorMobile = "MOB";
            public const string DonorEmail = "EMAIL";
            public const string DonorPIN = "PIN";
            public const string DonationInstrumentInfo = "INSTRUMENT_INFO";
            public const string DonationInstrumentDate = "INSTRUMENT_DATE";
            public const string DonationModeRefNo = "RefNo";
            public const string InstituteName = "Institute";
            public const string InstituteID = "InsID";
            public const string InstHeader1 = "Header1";
            public const string InstHeader2 = "Header2";
            public const string InstHeader3 = "Header3";
            public const string InstHeader4 = "Header4";
            public const string InstPAN = "InsPAN";
            public const string DonationDisclaimer = "DonationInfo";
            public const string DonationBy_Suffix = "DONATIONFOR";
            public const string HOAdd1 = "HOAdd1";
            public const string HOAdd2 = "HOAdd2";
            public const string HOAdd3 = "HOAdd3";
            public const string HOAdd4 = "HOAdd4";
            public const string HOCity = "HOCity";
            public const string HODist = "HODist";
            public const string HOState = "HOState";
            public const string HOCountry = "HOCountry";
            public const string HOPin = "HOPin";
            public const string HOTel1 = "HOTel1";
            public const string HOTel2 = "HOTel2";
            public const string HOMob1 = "HOMob1";
            public const string HOMob2 = "HOMob2";
            public const string HOFax1 = "HOFax1";
            public const string HOFax2 = "HOFax2";
            public const string HOEmail1 = "HOEmail1";
            public const string HOEmail2 = "HOEmail2";
            public const string IsAOApplicable = "IsAOApplicable";
            public const string AOAdd1 = "AOAdd1";
            public const string AOAdd2 = "AOAdd2";
            public const string AOAdd3 = "AOAdd3";
            public const string AOAdd4 = "AOAdd4";
            public const string AOCity = "AOCity";
            public const string AODist = "AODist";
            public const string AOState = "AOState";
            public const string AOCountry = "AOCountry";
            public const string AOPin = "AOPin";
            public const string AOTel1 = "AOTel1";
            public const string AOTel2 = "AOTel2";
            public const string AOMob1 = "AOMob1";
            public const string AOMob2 = "AOMob2";
            public const string AOFax1 = "AOFax1";
            public const string AOFax2 = "AOFax2";
            public const string AOEmail1 = "AOEmail1";
            public const string AOEmail2 = "AOEmail2";
            public const string ExemptionFooter = "EXMPFOOTER";
            public const string AuthorizedSignatory = "AUTHORITY";
            public const string ForeignAmount = "ForeignAmount";
            public const string ConversionRate = "ConversionRate";
            public const string Currency = "CUR_CODE";
        }

        private string Rec_Id = "";
        private DataTable Data = null;
        public string DonationReceipt_RecID = "";
        public Common_Lib.Common BASE;
        public DonationRecpt(string _RecID, string type, string PaperType,Common_Lib.Common _BASE)
        {
            BASE = _BASE;
            Rec_Id = _RecID;
            InitializeComponent();
            //emptySheet();
            FillData(type);
            //if (PaperType.ToLower().Equals(common.ReceiptPaper_PrePrinted.ToLower()))
            if (PaperType.Split(',')[0].ToLower().Equals("'pre-printed(blank)'") || PaperType.Split(',')[0].ToLower().Equals("'blank'"))
                PrintOnlyData();
            if (PaperType.Split(',')[0].ToLower().Equals("'pre-printed(half-blank)'"))
            { PrintOnlyData(); NewPRePrinted(); }
        }
        private void FillData(string type)
        {
            Data = BASE._DonationRegister_DBOps.GetReceiptDetails(Rec_Id);
            // this.DataSource = Data;
            if (Data == null) return;
            if (Data.Rows.Count < 1) return;
            //Header 
            if (type.ToLower().Equals("duplicate"))
                xrDuplicateLabel.Visible = true;
            //if (type.ToLower().Equals("sample"))
            //   xrSample.Visible = true;
            DonationReceipt_RecID = Data.Rows[0][Details.ReceiptID].ToString();
            xrInstName.Text = Data.Rows[0][Details.InstituteName].ToString();
            xrHeader1.Text = Data.Rows[0][Details.InstHeader1].ToString();
            xrHeader2.Text = Data.Rows[0][Details.InstHeader2].ToString();
            xrHeader3.Text = Data.Rows[0][Details.InstHeader3].ToString();
            xrHeader4.Text = Data.Rows[0][Details.InstHeader4].ToString();
            xrHODetail_single.Text = "Head Office: "+ GetHOAddress();
             if (Data.Rows[0]["CenTel1"].ToString().Length > 0)
                xrTableCell4.Text += "Centre   Tel: " + Data.Rows[0]["CenTel1"].ToString();
            if (Data.Rows[0]["CenEmail1"].ToString().Length > 0)
                xrTableCell4.Text += "   Email : " + Data.Rows[0]["CenEmail1"].ToString();
            xrHODetail_single2.Text = GetHOAddress2();
            if (Data.Rows[0][Details.InstPAN].ToString().Length > 0)
                xrHODetail_Single3.Text = "Permanent Account Number : " + Data.Rows[0][Details.InstPAN].ToString();
            string RelativePAth = "~/Content/Images/Logos/INS_" + Data.Rows[0][Details.InstituteID].ToString() + ".jpg";
            string AbsolutePAth = HttpContext.Current.Server.MapPath(RelativePAth);

            xrLogo.ImageUrl = RelativePAth;
            try { xrLogo.ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(AbsolutePAth); }
            catch (Exception ex) { }

            //Middle Section
            xrRecNo.Text = Data.Rows[0][Details.ReceiptNo].ToString();
            xrRecNoDetails.Text = Data.Rows[0][Details.Center].ToString();
            xrDate.Text = Convert.ToDateTime(Data.Rows[0][Details.RDate]).ToString("dd/MMM/yyyy");
            xrAmountInNum.Text = " " + Convert.ToDouble(Data.Rows[0][Details.Amount].ToString()).ToString("N", new CultureInfo("hi-IN"));
            //xrItemName.Text = Data.Rows[0]["ReceiptItem"].ToString().ToLower();
            if (Data.Rows[0][Details.Currency].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " " + Data.Rows[0][Details.Currency].ToString();
            if (Data.Rows[0][Details.ForeignAmount].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " " + Data.Rows[0][Details.ForeignAmount].ToString();
            if (Data.Rows[0][Details.ConversionRate].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " @"+ Data.Rows[0][Details.ConversionRate].ToString();
            Common_Lib.Common Base = new Common_Lib.Common();

            //http://pm.bkinfo.in/issues/5946#note-4
            string strAmount =Data.Rows[0][Details.Amount].ToString();
            decimal amount = Convert.ToDecimal(strAmount);
            xrAmountInAlpha.Text = "(" + Base.ConvertNumToAlphaValue(amount) + ")"; 
 
            //if ((amount-Math.Floor(amount)) >0)
            //    xrAmountInAlpha.Text += " and " + (Base.ConvertNumToAlphaValue(Convert.ToInt64(100 * (amount - Math.Floor(amount))))).ToString() + " paise";
            //xrAmountInAlpha.Text +=      " only )";
            xrFrom.Text = Data.Rows[0][Details.DonorName].ToString();
            xrRealisationLabel.Text = "(" + Data.Rows[0][Details.DonorName].ToString() + ")";
            xrFrom2.Text = Data.Rows[0][Details.DonorAddress].ToString() ;
            xrFrom3.Text = Data.Rows[0][Details.DonorArea].ToString() + (Data.Rows[0][Details.DonorPIN].ToString().Length > 0 ? " - " + Data.Rows[0][Details.DonorPIN].ToString() : "") + (Data.Rows[0][Details.DonorMobile].ToString().Length > 0 ? " ( M: " + Data.Rows[0][Details.DonorMobile].ToString() + ")" : "") + (Data.Rows[0][Details.DonorEmail].ToString().Length > 0 ? " (Email: " + Data.Rows[0][Details.DonorEmail].ToString() + ")" : "");
            xrMode.Text =  Data.Rows[0][Details.DonationMode].ToString();//ToTitleCase()
            if (Data.Rows[0][Details.DonationInstrumentInfo].ToString().Length > 0)
                xrMode.Text += " No. " + Data.Rows[0][Details.DonationInstrumentInfo].ToString();
            //if (Data.Rows[0][Details.DonationInstrumentDate].ToString().Length > 0)
            //    xrMode.Text += " Dt." + Convert.ToDateTime(Data.Rows[0][Details.DonationInstrumentDate].ToString()).ToString("dd/MM/yyyy");
            xrModeSuffix.Text = Data.Rows[0][Details.DonationBy_Suffix].ToString();

            if (Data.Rows[0][Details.DonorPanNo].ToString().Length > 0)
            { xrLabel.Visible = true; xrPAN.Text = Data.Rows[0][Details.DonorPanNo].ToString(); }

            //Footer Section
            xrDonationInfo.Text = Data.Rows[0][Details.DonationDisclaimer].ToString();
            //xrForInst.Text = "For " + Data.Rows[0][Details.InstituteName].ToString();
            xrAuthSign.Text = Data.Rows[0][Details.AuthorizedSignatory].ToString();
            xrFooterNoteLabel.Text = Data.Rows[0][Details.ExemptionFooter].ToString();
        }
        public static string ToTitleCase(string input)
        {
            TextInfo ti = Thread.CurrentThread.CurrentCulture.TextInfo;
            //if a word is all in upper case, ToTitleCase method is not able to convert to title case. So we would make the input string all lower case.
            return ti.ToTitleCase(input.ToLower());
        }
        private string GetHOAddress()
        {
            string Address = "";
            Address = Data.Rows[0]["HOAdd1"].ToString();
            if (Data.Rows[0]["HOAdd2"].ToString().Length > 0)
                Address += ", " + Data.Rows[0]["HOAdd2"].ToString();
            if (Data.Rows[0]["HOAdd3"].ToString().Length > 0)
                Address += ", " + Data.Rows[0]["HOAdd3"].ToString();
            if (Data.Rows[0]["HOAdd4"].ToString().Length > 0)
                Address += ", " + Data.Rows[0]["HOAdd4"].ToString();
            if (Data.Rows[0]["HOCity"].ToString().Length > 0)
            {
                if(Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0]["HOCity"].ToString();
                else
                    Address += ", " + Data.Rows[0]["HOCity"].ToString();
            }
            if (Data.Rows[0]["HOPin"].ToString().Length > 0)
                Address += " - " + Data.Rows[0]["HOPin"].ToString();
            if (Data.Rows[0]["HOState"].ToString().Length > 0)
            {
                if (Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0]["HOState"].ToString();
                else
                    Address += ", " + Data.Rows[0]["HOState"].ToString();
            }
            return Address;
        }
        private string GetHOAddress2()
        {
            string Address = "";
            if (Data.Rows[0]["HOMob1"].ToString().Length > 0)
                Address += Environment.NewLine + " Mob : " + Data.Rows[0]["HOMob1"].ToString();
            {
                if (Data.Rows[0]["HOMob2"].ToString().Length > 0)
                {
                    if (!Data.Rows[0]["HOMob2"].ToString().Trim().StartsWith("to")) Address += ",";
                    Address += " " + Data.Rows[0]["HOMob2"].ToString();
                }
            }
            if (Data.Rows[0]["HOTel1"].ToString().Length > 0)
                Address += " Tel : " + Data.Rows[0]["HOTel1"].ToString() ;
            if (Data.Rows[0]["HOTel2"].ToString().Length > 0)
                Address += " " + Data.Rows[0]["HOTel2"].ToString();
            if (Data.Rows[0]["HOEmail1"].ToString().Length > 0)
                Address += "   Email : " +  Data.Rows[0]["HOEmail1"].ToString();

           

            return Address;
        }
        //private string GetAOAddress()
        //{
        //    string Address = "";
        //    Address = Data.Rows[0][Details.AOAdd1].ToString();
        //    if (Data.Rows[0][Details.AOAdd2].ToString().Length > 0)
        //        Address += " " + Data.Rows[0][Details.AOAdd2].ToString();
        //    if (Data.Rows[0][Details.AOAdd3].ToString().Length > 0)
        //        Address += " " + Data.Rows[0][Details.AOAdd3].ToString();
        //    if (Data.Rows[0][Details.AOAdd4].ToString().Length > 0)
        //        Address += " " + Data.Rows[0][Details.AOAdd4].ToString();
        //    return Address;
        //}
        private void emptySheet()
        {
            xrReceiptLabel.Text = "";
            xrDateLabel.Text = "";
            xrReceivedLabel.Text = "";
            xrRupee.Visible = false;
            xrFromLabel.Text = "";
            xrByLabel.Text = "";
            xrRealisationLabel.Text = "";
            xrLine2.Visible = false;
        }

        private void PrintOnlyData()
        {
            xrCrossBandBox1.Visible = false;
            xrInstName.Visible = false;
            xrHeader1.Visible = false;
            xrHeader4.Visible = false;
            xrHeader3.Visible = false;
            xrHeader2.Visible = false;
            xrHODetail_single.Visible = false;
            xrHODetail_single2.Visible = false;
            xrHODetail_Single3.Visible = false;
            xrLine.Visible = false;
            //xrLogo.Visible  =false;
        }
        private void NewPRePrinted()
        {
            xrDonationInfo.Visible = false;
            xrRealisationLabel.Visible = false;
            xrAuthSign.Visible = false;
            xrForInst.Visible = false;
            xrFooterNoteLabel.Visible = false;
            xrLine2.Visible = false;
        }
        private void DonationRecpt_PrintProgress(object sender, DevExpress.XtraPrinting.PrintProgressEventArgs e)
        {
           
        }

        private void DonationRecpt_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void DonationRecpt_BeforePrint(object sender, PrintEventArgs e)
        {
            
        }
    }
}
