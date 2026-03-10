using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
//using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
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
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations.Attachments;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class HTMLEmailerController : BaseController
    {
        // GET: Facility/HTMLEmailer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Android_HomePage(string AndroidID= "")
        {
            return View(model: AndroidID);
        }

        public void Donation_80G_ReceiptEmailer_Old()
        {
            //try
            //{
            //    DataTable dt_donorsFor80G_Receipts = BASE._Donation_DBOps.GetDonorsFor80GReceipts();

            //    if (dt_donorsFor80G_Receipts != null && dt_donorsFor80G_Receipts.Rows.Count > 0)
            //    {
            //        foreach (DataRow _cRow in dt_donorsFor80G_Receipts.Rows)
            //        {
            //            string emailId_To = _cRow["TO_EMAIL"].ToString();
            //            string emailId_Cc = _cRow["CC_EMAIL"].ToString();
            //            string emailId_ReplyTo = _cRow["REPLY_TO_EMAIL"].ToString();
            //            string AB_ID = _cRow["AB_ID"].ToString();

            //            DataTable dt_DonorsDonationDataFor80G = BASE._Donation_DBOps.GetDonorsDonationDataFor80G(AB_ID);

            //            string subject = dt_DonorsDonationDataFor80G.Rows[0]["SUBJECT"].ToString();
            //            string instituteName = dt_DonorsDonationDataFor80G.Rows[0]["INSTITUTE_NAME"].ToString();
            //            string instituteID = dt_DonorsDonationDataFor80G.Rows[0]["INSTITUTE_ID"].ToString();
            //            string cenID = dt_DonorsDonationDataFor80G.Rows[0]["CEN_ID"].ToString();

            //            string htmlHeaderRow = "<tr><td>Sr</td><td>Donation Date</td><td>Donation Amount</td><td>Donation Receipt Path </td></tr>";
            //            string htmlForDonationData = null;

            //            foreach (DataRow _Row in dt_DonorsDonationDataFor80G.Rows)
            //            {
            //                string serialNo = _Row["SR"].ToString();
            //                string donationdate = _Row["DONATION_DATE"].ToString();
            //                string donationAmount = (Convert.ToInt32(_Row["DONATION_AMOUNT"])).ToString();
            //                string receiptPath = _Row["80G_RECEIPT_PATH"].ToString();

            //                htmlForDonationData = htmlForDonationData + "<tr><td>" + serialNo + "</td><td>" + donationdate + "</td><td>" + donationAmount + "</td><td>" + receiptPath + "</td></tr>";
            //            }

            //            DonationReceipt80G model = new DonationReceipt80G();
            //            model.donationDataHeader = HttpUtility.HtmlEncode(htmlHeaderRow);
            //            model.donationDataRows = HttpUtility.HtmlEncode(htmlForDonationData);
            //            model.instituteName = instituteName;
            //            model.instituteLogoURL = "https://connectone.app/Content/Images/Logos/INS_" + instituteID + ".jpg";
            //            model.cenID = cenID;

            //            string html = RenderViewToString(ControllerContext, "~/Areas/Facility/Views/HTMLEmailer/Donation_80G_ReceiptEmailer.cshtml", model, false);
            //            bool sent = BASE._Notifications_DBOps.SendEmail(emailId_To, subject, html, "80G_Donation_Receipt", emailId_Cc, "", emailId_ReplyTo);

            //            if (sent)
            //            {
            //                BASE._Donation_DBOps.Insert80GReceiptsDataSentByEmail(AB_ID, "Connectone_Emailer", "EMAIL", emailId_Cc, emailId_To, "Shantivan");
            //            }
            //            //return View(model);
            //        }
            //    }
            //    //return new EmptyResult();

            //}
            //catch (Exception e)
            //{
            //    //return new EmptyResult();
            //}
        }

        public ActionResult Donation_80G_ReceiptEmailer(string AB_ID)
        {
            try
            { 
                DataTable dt_DonorsDonationDataFor80G = BASE._Donation_DBOps.GetDonorsDonationDataFor80G(AB_ID);

                string subject = dt_DonorsDonationDataFor80G.Rows[0]["SUBJECT"].ToString();
                string instituteName = dt_DonorsDonationDataFor80G.Rows[0]["INSTITUTE_NAME"].ToString();
                string instituteID = dt_DonorsDonationDataFor80G.Rows[0]["INSTITUTE_ID"].ToString();
                string cenID = dt_DonorsDonationDataFor80G.Rows[0]["CEN_ID"].ToString();

                string htmlHeaderRow = "<tr><td>Sr</td><td>Donation Date</td><td>Donation Amount</td><td>Donation Receipt Path </td></tr>";
                string htmlForDonationData = null;

                foreach (DataRow _Row in dt_DonorsDonationDataFor80G.Rows)
                {
                    string serialNo = _Row["SR"].ToString();
                    string donationdate = _Row["DONATION_DATE"].ToString();
                    string donationAmount = (Convert.ToInt32(_Row["DONATION_AMOUNT"])).ToString();
                    string receiptPath = _Row["80G_RECEIPT_PATH"].ToString();

                    htmlForDonationData = htmlForDonationData + "<tr><td>" + serialNo + "</td><td>" + donationdate + "</td><td>" + donationAmount + "</td><td>" + receiptPath + "</td></tr>";
                }

                DonationReceipt80G model = new DonationReceipt80G();
                model.donationDataHeader = HttpUtility.HtmlEncode(htmlHeaderRow);
                model.donationDataRows = HttpUtility.HtmlEncode(htmlForDonationData);
                model.instituteName = instituteName;
                model.instituteLogoURL = "https://connectone.app/Content/Images/Logos/INS_" + instituteID + ".jpg";
                model.cenID = cenID;

                return View(model);
            }
            catch (Exception e)
            {
                return new EmptyResult();
            }
        }

        public ActionResult SendMedicalWingMembersEmails()
        {
            string html = RenderViewToString(ControllerContext, "~/Areas/Facility/Views/htmlemailer/MedicalWingEmailer.cshtml", null, true);
            ViewBag.RenderedHtml = html;


            String Message = "";
            bool sent;
            DataTable _Table = BASE._Membership_DBOps.GetMemberEmails("med");
            foreach (DataRow _cRow in _Table.Rows)
            {
                string emailID = _cRow[0].ToString();
                sent = BASE._Notifications_DBOps.SendEmail(emailID, "Regarding 46th Mind Body Medicine Conference!", html);
                if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
                else { Message += emailID + " : sending failed.<br/>"; }
                System.Threading.Thread.Sleep(250);
                break;
            }

            ViewBag.Message = Message;
            return View();
        }
        public ActionResult SendEmailsByReference()
        {
            string html = RenderViewToString(ControllerContext, "~/Areas/Facility/Views/htmlemailer/MedicalWingEmailer.cshtml", null, true);
            ViewBag.RenderedHtml = html;

            String Message = "";
            bool sent;

            DataTable _Table = BASE._Address_DBOps.GetEmailIDs("45MBM","1", "Z");
            foreach (DataRow _cRow in _Table.Rows)
            {
                string emailID = _cRow[0].ToString();
                sent = BASE._Notifications_DBOps.SendEmail(emailID, "Thanks for Joining Mind-Body medicine Conference from 10th to 14th June, 2022", html);
                if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
                else { Message += emailID + " : sending failed.<br/>"; }
                System.Threading.Thread.Sleep(250);
                 //break;
            }

            ViewBag.Message = Message;
            return View();
        }

        public ActionResult RenderViewToString()
        {
            string html = RenderViewToString(ControllerContext, "~/Areas/Facility/Views/htmlemailer/KarunaEmailer.cshtml", null, true);

            //--chart--start
            //KarunaSankalpChartModel model = new KarunaSankalpChartModel();
            //DataTable d1 = BASE._Chart_DBOps.get_karunaSankalpChart_ParticipantDetails("84283ac4-379f-412e-9b42-f132999f8880");
            //model.ParticipantName = d1.Rows[0]["PARTICIPANTNAME"].ToString();
            //model.Participant_AB_ID = d1.Rows[0]["PARTICIPANT_AB_ID"].ToString();
            //model.ChartSrNo = d1.Rows[0]["CHARTSRNO"].ToString();
            //model.ChartFrom = Convert.ToDateTime(d1.Rows[0]["CHART_FROM"]).ToString("dd-MMM-yyyy");
            //model.ChartTo = Convert.ToDateTime(d1.Rows[0]["CHART_TO"]).ToString("dd-MMM-yyyy");
            //model.BenevolentPledge = (d1.Rows[0]["BENEVOLENT"].ToString() == "True") ? "block" : "none";
            //model.EquanimityPledge = (d1.Rows[0]["EQUANIMITY"].ToString() == "True") ? "block" : "none";
            //model.PeacePledge = (d1.Rows[0]["PEACE"].ToString() == "True") ? "block" : "none";
            //model.HappinessPledge = (d1.Rows[0]["HAPPINESS"].ToString() == "True") ? "block" : "none";
            //model.RespectPledge = (d1.Rows[0]["RESPECT"].ToString() == "True") ? "block" : "none";
            //model.BrotherhoodPledge = (d1.Rows[0]["BROTHERHOOD"].ToString() == "True") ? "block" : "none";
            ////model.href_Yes = "http://localhost:51486/ServiceChart/KarunaSankalpChart_SubmitResponse?AB_ID="+ model.Participant_AB_ID + "&ChartSrNo=" + model.ChartSrNo + "&Response=Yes";
            //model.href_Yes = "http://localhost:51486/KarunaSankalpChart/" + model.Participant_AB_ID + "/" + model.ChartSrNo + "/Yes/" + model.ParticipantName;
            ////model.href_No = "http://localhost:51486/ServiceChart/KarunaSankalpChart_SubmitResponse?AB_ID=" + model.Participant_AB_ID + "&ChartSrNo=" + model.ChartSrNo + "&Response=No";
            //model.href_No = "http://localhost:51486/KarunaSankalpChart/" + model.Participant_AB_ID + "/" + model.ChartSrNo + "/No/" + model.ParticipantName;
            //string html = RenderViewToString(ControllerContext, "~/Views/ServiceChart/KarunaSankalpChart.cshtml", model, false);
            //--chart_end

            ViewBag.RenderedHtml = html;
            

            String Message = "";
            bool sent;
            //DataTable _Table = BASE._Membership_DBOps.GetMemberEmails("med");
            //foreach (DataRow _cRow in _Table.Rows)
            //{
            //    string emailID = _cRow[0].ToString();
            //    sent = BASE._Notifications_DBOps.SendEmail(emailID, "Brahmakuamris :: Spiritual Empowerment for Kindness and compassion Year 2022", html);
            //    if (sent) {Message += emailID+" : sent Successfully.<br/>";}
            //    else { Message += emailID + " : sending failed.<br/>"; }
            //    System.Threading.Thread.Sleep(250);
            //    break;
            //}

            DataTable _Table = BASE._Address_DBOps.GetEmailIDs("S","Z");
            foreach (DataRow _cRow in _Table.Rows)
            {
                string emailID = _cRow[0].ToString();
                sent = BASE._Notifications_DBOps.SendEmail(emailID, "Brahmakuamris :: Spiritual Empowerment for Kindness and compassion Year 2022", html);
                if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
                else { Message += emailID + " : sending failed.<br/>"; }
                System.Threading.Thread.Sleep(250);
               // break;
            }

            //string emailID = "saurabh@bkivv.org";
            //sent = BASE._Notifications_DBOps.SendEmail("saurabh@bkivv.org", "Brahmakumaris :: Spiritual Empowerment for Kindness and Compassion - Year 2022", html);
            //if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
            //else { Message += emailID + " : sending failed.<br/>"; }

            ViewBag.Message = Message;
            return View();
        }

        public ActionResult SendKarunCharts()
        {
            String Message = "";
            bool sent;
            DataTable _Table = BASE._Chart_DBOps.GetKarunaChartEmailIDs();
            foreach (DataRow _cRow in _Table.Rows)
            {
                string AB_ID = _cRow["ab_id"].ToString();
                string emailID = _cRow["C_EMAIL_ID_1"].ToString();

                KarunaSankalpChartModel model = new KarunaSankalpChartModel();
                DataTable d1 = BASE._Chart_DBOps.get_karunaSankalpChart_ParticipantDetails(AB_ID);
                string Root_Path = "https://services.brahmakumaris.com/KarunaSankalpChart/";
                model.ParticipantName = d1.Rows[0]["PARTICIPANTNAME"].ToString();
                model.Participant_AB_ID = d1.Rows[0]["PARTICIPANT_AB_ID"].ToString();
                model.ChartSrNo = d1.Rows[0]["CHARTSRNO"].ToString();
                model.ChartFrom = Convert.ToDateTime(d1.Rows[0]["CHART_FROM"]).ToString("dd-MMM-yyyy");
                model.ChartTo = Convert.ToDateTime(d1.Rows[0]["CHART_TO"]).ToString("dd-MMM-yyyy");
                model.BenevolentPledge = (d1.Rows[0]["BENEVOLENT"].ToString() == "True") ? "block" : "none";
                model.EquanimityPledge = (d1.Rows[0]["EQUANIMITY"].ToString() == "True") ? "block" : "none";
                model.PeacePledge = (d1.Rows[0]["PEACE"].ToString() == "True") ? "block" : "none";
                model.HappinessPledge = (d1.Rows[0]["HAPPINESS"].ToString() == "True") ? "block" : "none";
                model.RespectPledge = (d1.Rows[0]["RESPECT"].ToString() == "True") ? "block" : "none";
                model.BrotherhoodPledge = (d1.Rows[0]["BROTHERHOOD"].ToString() == "True") ? "block" : "none";
                model.href_Yes = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/Yes/" + model.ParticipantName.ToTitleCase();
                model.href_No = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/No/" + model.ParticipantName.ToTitleCase();

                string html = RenderViewToString(ControllerContext, "~/Views/ServiceChart/KarunaSankalpChart.cshtml", model, false);
                ViewBag.RenderedHtml = html;
                
                sent = BASE._Notifications_DBOps.SendEmail(emailID, "Pledge Reminder :: Spiritual Empowerment for Kindness and Compassion Year 2022", html,
                    "Pledge Reminder:5","","", "karunasankalp@brahmakumaris.com");
                if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
                else { Message += emailID + " : sending failed.<br/>"; }
                System.Threading.Thread.Sleep(250);
               //  break;
            }

            ViewBag.Message = Message;
            return View();
        }

        public ActionResult KarunaSankalpChart(string AB_ID, string id = null)
        {
            //String Message = "";
            //bool sent;
            //DataTable _Table = BASE._Chart_DBOps.GetKarunaChartEmailIDs();
            //foreach (DataRow _cRow in _Table.Rows)
            //{
            //    string AB_ID = _cRow["ab_id"].ToString();
            //string emailID = _cRow["C_EMAIL_ID_1"].ToString();

            KarunaSankalpChartModel model = new KarunaSankalpChartModel();
            DataTable d1 = BASE._Chart_DBOps.get_karunaSankalpChart_ParticipantDetails(AB_ID);
            string Root_Path = "https://services.brahmakumaris.com/KarunaSankalpChart/";
            model.ParticipantName = d1.Rows[0]["PARTICIPANTNAME"].ToString();
            model.Participant_AB_ID = d1.Rows[0]["PARTICIPANT_AB_ID"].ToString();
            model.ChartSrNo = d1.Rows[0]["CHARTSRNO"].ToString();
            model.ChartFrom = Convert.ToDateTime(d1.Rows[0]["CHART_FROM"]).ToString("dd-MMM-yyyy");
            model.ChartTo = Convert.ToDateTime(d1.Rows[0]["CHART_TO"]).ToString("dd-MMM-yyyy");
            model.BenevolentPledge = (d1.Rows[0]["BENEVOLENT"].ToString() == "True") ? "block" : "none";
            model.EquanimityPledge = (d1.Rows[0]["EQUANIMITY"].ToString() == "True") ? "block" : "none";
            model.PeacePledge = (d1.Rows[0]["PEACE"].ToString() == "True") ? "block" : "none";
            model.HappinessPledge = (d1.Rows[0]["HAPPINESS"].ToString() == "True") ? "block" : "none";
            model.RespectPledge = (d1.Rows[0]["RESPECT"].ToString() == "True") ? "block" : "none";
            model.BrotherhoodPledge = (d1.Rows[0]["BROTHERHOOD"].ToString() == "True") ? "block" : "none";
            model.href_Yes = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/Yes/" + model.ParticipantName.ToTitleCase().Replace(" ", "")+"/email";
            model.href_No = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/No/" + model.ParticipantName.ToTitleCase().Replace(" ", "")+"/email";
            model.id = id;
            //string html = RenderViewToString(ControllerContext, "~/Views/ServiceChart/KarunaSankalpChart.cshtml", model, false);
            //ViewBag.RenderedHtml = html;

            //sent = BASE._Notifications_DBOps.SendEmail(emailID, "Pledge Reminder :: Spiritual Empowerment for Kindness and Compassion Year 2022", html, "Pledge Reminder:5", "", "", "karunasankalp@brahmakumaris.com");
            //if (sent) { Message += emailID + " : sent Successfully.<br/>"; }
            //else { Message += emailID + " : sending failed.<br/>"; }
            //System.Threading.Thread.Sleep(250);
            //  break;
            //}

            //ViewBag.Message = Message;
            return View("~/Views/ServiceChart/KarunaSankalpChart.cshtml",model);
        }

        public ActionResult TestEmailer()
        {
            return View();
        }
        public ActionResult MedicalWingEmailer()
        {
            return View();
        }
        public ActionResult KarunaEmailer()
        {
            return View();
        }
        public ActionResult KarunaSocial()
        {
            return View();
        }
        public ActionResult bkEmailer()
        {
            return View();
        }
        public ActionResult MembershipConfirmation()
        {
            return View();
        }
        public ActionResult GeneralEmailer()
        {
            return View();
        }
        public ActionResult SewanjaliReportEmailer(string reportFor = "", String startdate = null, String enddate = null, String insid = null,
    String SessionGUID = null, int? centerid = null, String speaker = null, String themeid = null, int? ratingfrom = null, int? ratingto = null, String centername = null, String wingshort = null,
    String cityid = null, String stateid = null, String attachmentRootPath = null, String wingid = null, String topicname = null, String tabchange = "default", String wingname = null,
    Boolean webview = false, Boolean needimages = false, Boolean needvideos = false, string cityname = null, string statename = null, string zoneID = null, string SubzoneID = null)
        {
            if (attachmentRootPath == null) { attachmentRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"].ToString(); }
            if (centerid == 0) { centerid = null; }
            if (cityid == "" || cityid == "null") { cityid = null; }
            if (stateid == "" || stateid == "null") { stateid = null; }
            if (wingid == "" || wingid == "null") { wingid = null; }
            if (topicname == "" || topicname == "null") { topicname = null; }
            if (startdate == "" || startdate == "null") { startdate = null; }
            if (enddate == "" || enddate == "null") { enddate = null; }
            if (themeid == "" || themeid == "null") { themeid = null; }
            if (speaker == "" || speaker == "null") { speaker = null; }
            if (zoneID == "" || zoneID == "null") { zoneID = null; }
            if (SubzoneID == "" || SubzoneID == "null") { SubzoneID = null; }
            DateTime? fromdate = null;
            DateTime? todate = null;
            if (startdate != null) { fromdate = Convert.ToDateTime(startdate); }
            if (enddate != null) { todate = Convert.ToDateTime(enddate); }
            DataSet _ds = BASE._SR_DBOps.GetSewanjaliData(reportFor, (DateTime?)fromdate, (DateTime?)todate, (int?)centerid, speaker, themeid, ratingfrom, ratingto, cityid, stateid, attachmentRootPath, wingid, insid, zoneID, SubzoneID);
            return View(_ds);
        }
        static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
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
        public ActionResult CorpusFDEmailer(string cenId, string fyId)
        {
            DataSet ds =  BASE._Notifications_DBOps.GenericEmailer(cenId, fyId, "corpus fd");
            return View(ds);
        }

    }
}