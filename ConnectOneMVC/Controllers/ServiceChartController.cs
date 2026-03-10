using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Areas.Facility.Controllers;
using ConnectOneMVC.Models;
using System.IO;
using System.Data;


namespace ConnectOneMVC.Controllers
{
    public class ServiceChartController : BaseController
    {

        // GET: ServiceChart
        public ActionResult KarunaSankalpChart(string AB_ID)
        {
            KarunaSankalpChartModel model = new KarunaSankalpChartModel();
            DataTable d1 = BASE._Chart_DBOps.get_karunaSankalpChart_ParticipantDetails(AB_ID);
            string Root_Path = "https://services.brahmakumaris.com/KarunaSankalpChart/";
            //string Root_Path = "http://localhost:51486/ServiceChart/KarunaSankalpChart_SubmitResponse?AB_ID=";
            //string Root_Path = "http://localhost:51486/KarunaSankalpChart/";
            model.ParticipantName = d1.Rows[0]["PARTICIPANTNAME"].ToString();
            model.Participant_AB_ID = d1.Rows[0]["PARTICIPANT_AB_ID"].ToString();
            model.ChartSrNo = d1.Rows[0]["CHARTSRNO"].ToString();
            model.ChartFrom = Convert.ToDateTime(d1.Rows[0]["CHART_FROM"]).ToString("dd-MMM-yyyy");
            model.ChartTo = Convert.ToDateTime(d1.Rows[0]["CHART_TO"]).ToString("dd-MMM-yyyy");
            model.BenevolentPledge = (d1.Rows[0]["BENEVOLENT"].ToString() == "True") ?  "block" :  "none";
            model.EquanimityPledge = (d1.Rows[0]["EQUANIMITY"].ToString() == "True") ?  "block" :  "none";
            model.PeacePledge = (d1.Rows[0]["PEACE"].ToString() == "True") ?  "block" :  "none";
            model.HappinessPledge = (d1.Rows[0]["HAPPINESS"].ToString() == "True") ?  "block" :  "none";
            model.RespectPledge = (d1.Rows[0]["RESPECT"].ToString() == "True") ?  "block" :  "none";
            model.BrotherhoodPledge = (d1.Rows[0]["BROTHERHOOD"].ToString() == "True") ?  "block" :  "none";
            model.href_Yes = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/Yes/" + model.ParticipantName.ToTitleCase();
            model.href_No = Root_Path + model.Participant_AB_ID + "/" + model.ChartSrNo + "/No/" + model.ParticipantName.ToTitleCase();

            return View(model);

        }

        public ActionResult KarunaSankalpChart_SubmitResponse(string AB_ID, string ChartSrNo, string Response, string ParticipantName = "")
        {
            KarunaSankalpChartModel model = new KarunaSankalpChartModel();
            model.ParticipantName = ParticipantName;
            model.UserResponse = Response;
            model.ExceptionMessage = "";
            try
            {
                bool ResponseSaved = BASE._Chart_DBOps.insert_karunaSankalpChart_Responses(AB_ID, Convert.ToInt32(ChartSrNo), Response);

                return View(model);
            }

            catch (Exception ex)
            {
                model.ExceptionMessage = ex.Message;
                return View(model);
            }

        }

        public void KarunaSankalpChart_Mail(string AB_ID)
        {

            // ServiceReportController ServiceReport = new ServiceReportController();

            string ContentPath = "~/Views/ServiceChart/KarunaSankalpChart.cshtml";
            string html = RenderViewToString(ControllerContext, ContentPath);
            string emailID = "shailender.chaudhary@bkconnect.net";
            string Subject = "Javascript Testing for the Karuna Sankalp Chart";
            bool sent = BASE._Notifications_DBOps.SendEmail(emailID, Subject, html);



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
    }
}