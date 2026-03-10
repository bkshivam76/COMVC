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
    public partial class DonationRecpt_DualHeaderAddress : DevExpress.XtraReports.UI.XtraReport
    {
        private string Txn_Rec_Id = "";
        private DataTable Data = null;
        public string DonationReceipt_RecID = "";
        public Common_Lib.Common BASE;
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
        public DonationRecpt_DualHeaderAddress(string _Txn_RecID, string Type, string PaperType, Common_Lib.Common _BASE)
        {
            BASE = _BASE;
            Txn_Rec_Id = _Txn_RecID;
            InitializeComponent();
            FillData(Type);
           // if (PaperType.ToLower().Equals(common.ReceiptPaper_PrePrinted.ToLower()))
            if (PaperType.Split(',')[0].ToLower().Equals("'pre-printed(blank)'"))
                PrintOnlyData();
            if (PaperType.Split(',')[0].ToLower().Equals("'pre-printed(half-blank)'"))
            { PrintOnlyData(); NewPRePrinted(); }
        }

        private void FillData(string Type)
        {
            Data = BASE._DonationRegister_DBOps.GetReceiptDetails(Txn_Rec_Id);
            // this.DataSource = Data;
            if (Data == null) return;
            if (Data.Rows.Count < 1) return;
            //Header 
            if (Type.ToLower().Equals("duplicate"))
                xrDuplicateLabel.Visible = true;
            DonationReceipt_RecID = Data.Rows[0][Details.ReceiptID].ToString();
            xrInstName.Text = Data.Rows[0][Details.InstituteName].ToString();
            xrHeader1.Text = Data.Rows[0][Details.InstHeader1].ToString();
            xrHeader2.Text = Data.Rows[0][Details.InstHeader2].ToString();
            xrHeader3.Text = Data.Rows[0][Details.InstHeader3].ToString();
            if (Data.Rows[0][Details.InstHeader4].ToString().Length>0)
                xrHeader4.Text = Data.Rows[0][Details.InstHeader4].ToString();
            else
                xrHeader4.Text = "Permanent Account Number : "+Data.Rows[0][Details.InstPAN].ToString();
            
            xrHODetail.Text = "Regd. Office : ";
            xrHODetail2.Text = Data.Rows[0][Details.HOAdd1].ToString() +" "+ Data.Rows[0][Details.HOAdd2].ToString();
            xrHODetail3.Text = Data.Rows[0][Details.HOAdd3].ToString() + " " + Data.Rows[0][Details.HOAdd4].ToString(); 
            if (Data.Rows[0][Details.HOCity].ToString().Length > 0)
                    xrHODetail3.Text += " " +Data.Rows[0][Details.HOCity].ToString();
            if (Data.Rows[0][Details.HOPin].ToString().Length > 0)
                xrHODetail3.Text += " - " + Data.Rows[0][Details.HOPin].ToString();
            if (Data.Rows[0][Details.HOState].ToString().Length > 0)
            {xrHODetail3.Text += " " + Data.Rows[0][Details.HOState].ToString();}
             if (Data.Rows[0][Details.HOTel1].ToString().Length > 0)
                xrHODetail4.Text = "Tel:" + Data.Rows[0][Details.HOTel1].ToString();
            if (Data.Rows[0][Details.HOTel2].ToString().Length > 0)
                xrHODetail4.Text += " " + Data.Rows[0][Details.HOTel2].ToString();
            if (Data.Rows[0][Details.HOFax1].ToString().Length > 0)
                xrHODetail5.Text += "Fax " + Data.Rows[0][Details.HOFax1].ToString();
            if (Data.Rows[0][Details.HOEmail1].ToString().Length > 0)
                xrHODetail5.Text += " Email: " + Data.Rows[0][Details.HOEmail1].ToString();


            xrAODetail.Text = "Administrative Office : ";
            xrAODetail2.Text = Data.Rows[0][Details.AOAdd1].ToString() + " " + Data.Rows[0][Details.AOAdd2].ToString();
            xrAODetail3.Text = Data.Rows[0][Details.AOAdd3].ToString() + " " + Data.Rows[0][Details.AOAdd4].ToString();
            if (Data.Rows[0][Details.AOCity].ToString().Length > 0)
                if(xrAODetail3.Text.Trim().Length>0)
                    xrAODetail3.Text += " " + Data.Rows[0][Details.AOCity].ToString();
                else
                    xrAODetail3.Text = Data.Rows[0][Details.AOCity].ToString();
            if (Data.Rows[0][Details.AOPin].ToString().Length > 0)
                xrAODetail3.Text += " - " + Data.Rows[0][Details.AOPin].ToString();
            if (Data.Rows[0][Details.AOState].ToString().Length > 0)
                xrAODetail3.Text += " " + Data.Rows[0][Details.AOState].ToString(); 
            if (Data.Rows[0][Details.AOTel1].ToString().Length > 0)
                xrAODetail4.Text = "Tel:" + Data.Rows[0][Details.AOTel1].ToString();
            if (Data.Rows[0][Details.AOTel2].ToString().Length > 0)
                xrAODetail4.Text += " " + Data.Rows[0][Details.AOTel2].ToString();
            if (Data.Rows[0][Details.AOFax1].ToString().Length > 0)
                xrAODetail5.Text += "Fax " + Data.Rows[0][Details.AOFax1].ToString();
            if (Data.Rows[0][Details.AOEmail1].ToString().Length > 0)
                xrAODetail5.Text += " Email: " + Data.Rows[0][Details.AOEmail1].ToString();
            string RelativePAth = "~/App_Themes/Standard/images/Logos/INS_" + Data.Rows[0][Details.InstituteID].ToString() + ".jpg";
            string AbsolutePAth =  HttpContext.Current.Server.MapPath(RelativePAth);

            //xrLogo.ImageUrl = "~/App_Themes/Standard/images/Logos/INS_" + Data.Rows[0][Details.InstituteID].ToString() + ".jpg";
            xrLogo.ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(AbsolutePAth);
            //Middle Section
            xrRecNo.Text = Data.Rows[0][Details.ReceiptNo].ToString();
            xrRecNoDetails.Text = Data.Rows[0][Details.Center].ToString();
            xrDate.Text = Convert.ToDateTime(Data.Rows[0][Details.RDate]).ToString("dd/MM/yyyy");
            xrAmountInNum.Text = " " + Convert.ToDouble(Data.Rows[0][Details.Amount].ToString()).ToString("N", new CultureInfo("hi-IN"));
            if (Data.Rows[0][Details.Currency].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " " + Data.Rows[0][Details.Currency].ToString();
            if (Data.Rows[0][Details.ForeignAmount].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " "+Data.Rows[0][Details.ForeignAmount].ToString();
            if (Data.Rows[0][Details.ConversionRate].ToString().Length > 0 && Data.Rows[0][Details.Currency].ToString().ToUpper() != "INR")
                xrAmountInNum.Text += " @" + Data.Rows[0][Details.ConversionRate].ToString();
            Common_Lib.Common Base = new Common_Lib.Common();

            // http://pm.bkinfo.in/issues/5946#note-13
            string strAmount = Data.Rows[0][Details.Amount].ToString();
            decimal amount = Convert.ToDecimal(strAmount);
            xrAmountInAlpha.Text = "(" + Base.ConvertNumToAlphaValue(amount) + ")"; 
      
            //double amount = Convert.ToDouble(Data.Rows[0][Details.Amount].ToString());
            //xrAmountInAlpha.Text = "( Rupees " + Base.ConvertNumToAlphaValue(Convert.ToInt64(amount));
            //if ((amount - Math.Floor(amount)) > 0)
            //    xrAmountInAlpha.Text += " and "+(Base.ConvertNumToAlphaValue(Convert.ToInt64(100 * (amount - Math.Floor(amount))))).ToString() + " paise";
            //xrAmountInAlpha.Text += " only )";
            
            xrFrom.Text = Data.Rows[0][Details.DonorName].ToString();
            xrFrom2.Text = Data.Rows[0][Details.DonorAddress].ToString() ;
            xrFrom3.Text = Data.Rows[0][Details.DonorArea].ToString() + (Data.Rows[0][Details.DonorPIN].ToString().Length>0 ?  " - " + Data.Rows[0][Details.DonorPIN].ToString() : "") + (Data.Rows[0][Details.DonorMobile].ToString().Length > 0 ? " (Mob: " + Data.Rows[0][Details.DonorMobile].ToString() + ")" : "") + (Data.Rows[0][Details.DonorEmail].ToString().Length > 0 ? " (Email: " + Data.Rows[0][Details.DonorEmail].ToString() + ")" : "");
            xrMode.Text = ToTitleCase(Data.Rows[0][Details.DonationMode].ToString());
            if (Data.Rows[0][Details.DonationInstrumentInfo].ToString().Length > 0)
                xrMode.Text += " No. " + Data.Rows[0][Details.DonationInstrumentInfo].ToString();
            if (Data.Rows[0][Details.DonationInstrumentDate].ToString().Length > 0)
                xrMode.Text += " Dt." + Convert.ToDateTime(Data.Rows[0][Details.DonationInstrumentDate].ToString()).ToString("dd/MM/yyyy");
            xrModeSuffix.Text = Data.Rows[0][Details.DonationBy_Suffix].ToString();

            if (Data.Rows[0][Details.DonorPanNo].ToString().Length > 0)
            { xrPANLabel.Visible = true; xrPAN.Text = Data.Rows[0][Details.DonorPanNo].ToString(); }

            //Footer Section
            xrDonationInfo.Text = Data.Rows[0][Details.DonationDisclaimer].ToString();
            xrForInst.Text = "For " + Data.Rows[0][Details.InstituteName].ToString();
            xrAuthSign.Text = Data.Rows[0][Details.AuthorizedSignatory].ToString();
            xrFooterNoteLabel.Text = Data.Rows[0][Details.ExemptionFooter].ToString();
        }
        public static string ToTitleCase(string input)
        {
            TextInfo ti = Thread.CurrentThread.CurrentCulture.TextInfo;
            //if a word is all in upper case, ToTitleCase method is not able to convert to title case. So we would make the input string all lower case.
            return ti.ToTitleCase(input.ToLower());
        }
        private string GetHoAddress()
        {
            string Address = "";
            Address = "Regd. Office: " + Data.Rows[0][Details.HOAdd1].ToString();
            if (Data.Rows[0][Details.HOAdd2].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.HOAdd2].ToString();
            if (Data.Rows[0][Details.HOAdd3].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.HOAdd3].ToString();
            if (Data.Rows[0][Details.HOAdd4].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.HOAdd4].ToString();
            if (Data.Rows[0][Details.HOCity].ToString().Length > 0)
            {
                if(Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0][Details.HOCity].ToString();
                else
                    Address += ", " + Data.Rows[0][Details.HOCity].ToString();
            }
            if (Data.Rows[0][Details.HOPin].ToString().Length > 0)
                Address += " - " + Data.Rows[0][Details.HOPin].ToString();
            if (Data.Rows[0][Details.HOState].ToString().Length > 0)
            {
                if (Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0][Details.HOState].ToString();
                else
                    Address += ", " + Data.Rows[0][Details.HOState].ToString();
            }
            return Address;
        }
        private string GetHoAddress2()
        {
            string Address = "";
            if (Data.Rows[0][Details.HOTel1].ToString().Length > 0)
                Address += Environment.NewLine + " Tel:" + Data.Rows[0][Details.HOTel1].ToString();
            if (Data.Rows[0][Details.HOTel2].ToString().Length > 0)
            {
                if (!Data.Rows[0][Details.HOTel2].ToString().Trim().StartsWith("to")) Address += ",";
                Address += " " + Data.Rows[0][Details.HOTel2].ToString();
            }
            if (Data.Rows[0][Details.HOFax1].ToString().Length > 0)
                Address += " Fax " + Data.Rows[0][Details.HOFax1].ToString();
            if (Data.Rows[0][Details.HOEmail1].ToString().Length > 0)
                Address += " Email: " + Data.Rows[0][Details.HOEmail1].ToString();
            
            return Address;
        }
        private string GetAOAddress()
        {
            string Address = "";
            Address = "Administrative Office: " + Data.Rows[0][Details.AOAdd1].ToString();
            if (Data.Rows[0][Details.AOAdd2].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.AOAdd2].ToString();
            if (Data.Rows[0][Details.AOAdd3].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.AOAdd3].ToString();
            if (Data.Rows[0][Details.AOAdd4].ToString().Length > 0)
                Address += " " + Data.Rows[0][Details.AOAdd4].ToString();
            if (Data.Rows[0][Details.AOCity].ToString().Length > 0)
            {
                if (Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0][Details.AOCity].ToString();
                else
                    Address += ", " + Data.Rows[0][Details.AOCity].ToString();
            }
            if (Data.Rows[0][Details.AOPin].ToString().Length > 0)
                Address += " - " + Data.Rows[0][Details.AOPin].ToString();
            if (Data.Rows[0][Details.AOState].ToString().Length > 0)
            {
                if (Address.Trim().EndsWith(","))
                    Address += " " + Data.Rows[0][Details.AOState].ToString();
                else
                    Address += ", " + Data.Rows[0][Details.AOState].ToString();
            }
            return Address;
        }
        private string GetAoAddress2()
        {
            string Address = "";
            if (Data.Rows[0][Details.AOTel1].ToString().Length > 0)
                Address += Environment.NewLine + " Tel:" + Data.Rows[0][Details.AOTel1].ToString();
            if (Data.Rows[0][Details.AOTel2].ToString().Length > 0)
            {
                if (!Data.Rows[0][Details.AOTel2].ToString().Trim().StartsWith("to")) Address += ",";
                    Address += " " + Data.Rows[0][Details.AOTel2].ToString();
            }
            if (Data.Rows[0][Details.AOFax1].ToString().Length > 0)
                Address += " Fax " + Data.Rows[0][Details.AOFax1].ToString();
            if (Data.Rows[0][Details.AOEmail1].ToString().Length > 0)
                Address += " Email: " + Data.Rows[0][Details.AOEmail1].ToString();

            return Address;
        }
        private void PrintOnlyData()
        {
            xrCrossBandBox1.Visible = false;
            xrInstName.Visible = false;
            xrHeader1.Visible = false;
            xrHeader2.Visible = false;
            xrHeader3.Visible = false;
            xrHeader4.Visible = false;
            xrHODetail.Visible = false;
            xrHODetail2.Visible = false;
            xrHODetail3.Visible = false;
            xrHODetail4.Visible = false;
            xrHODetail5.Visible = false;
            xrAODetail.Visible = false;
            xrAODetail2.Visible = false;
            xrAODetail3.Visible = false;
            xrAODetail4.Visible = false;
            xrAODetail5.Visible = false;
            xrLine.Visible = false;
            xrLogo.Visible = false;
        }
        private void NewPRePrinted()
        {
            //xrDonationInfo.ForeColor = Color.White;
            xrDonationInfo.Visible = false;
            xrRealisationLabel.Visible = false;
            xrAuthSign.Visible = false;
            xrForInst.Visible = false;
            xrFooterNoteLabel.Visible = false;
            xrLine2.Visible = false;
        }

    }
}
