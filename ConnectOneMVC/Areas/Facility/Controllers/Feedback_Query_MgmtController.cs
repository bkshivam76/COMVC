using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Areas.Help.Controllers;
using ConnectOneMVC.Areas.Help.Models;
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
    public class Feedback_Query_MgmtController : BaseController
    {
        public DataSet DS_FQ
        {
            get { return (DataSet)GetBaseSession("DS_FQ_FQmgt"); }
            set { SetBaseSession("DS_FQ_FQmgt", value); }
        }
        public DataSet DS_Contr
        {
            get { return (DataSet)GetBaseSession("DS_Contr_FQmgt"); }
            set { SetBaseSession("DS_Contr_FQmgt", value); }
        }
        // GET: Facility/Feedback_Query_Mgmt
        public ActionResult Frm_Feedback_Query_Mgmt()
        {
            return View();
        }
        public ActionResult Frm_FeedQuery_EventsCardView(int? centerid = null, String SessionGUID = null, string command = null)
        {
            centerid = BASE._open_Cen_ID;
            string attachmentpath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"];
            DS_FQ = BASE._SMGS_DBOps.Get_Feedback_Query_EventsList(centerid, attachmentpath);
            DataTable dt = DS_FQ.Tables[0];
            ViewBag.rowcount_evnt = dt.Rows.Count;
            ViewBag.firsteventrecid = "";
            ViewBag.referenceName ="";
            ViewBag.referenceRecid = "";
            if (dt.Rows.Count > 0)
            {
                ViewBag.firsteventrecid = dt.Rows[0]["REC_ID"];
                ViewBag.referenceName = dt.Rows[0]["FQI_REFERENCE"];
                ViewBag.referenceRecid = dt.Rows[0]["FQI_REFERENCE_REC_ID"];
            }
            return View(dt);
        }

        public ActionResult Frm_FeedQuery_ContributorsCardView(string eventrecid, string referenceName = null, string referenceRecid = null, string SessionGUID = null, 
            string command = null)
        {
            int rwcnt = DS_FQ.Tables[0].Rows.Count;
            if (command == null)
            {
                if(rwcnt > 0)
                {
                    eventrecid = DS_FQ.Tables[0].Rows[0]["REC_ID"].ToString();
                    referenceName = DS_FQ.Tables[0].Rows[0]["FQI_REFERENCE"].ToString();
                    referenceRecid = DS_FQ.Tables[0].Rows[0]["FQI_REFERENCE_REC_ID"].ToString();
                }
                
            }
            else if(command == "REFRESH")
            {
                //eventrecid = eventrecid;
            }
            DataTable dt = null;
            if(rwcnt > 0)
            {
                string attachmentpath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"];
                DS_Contr = BASE._SMGS_DBOps.Get_feed_Query_Contributions(eventrecid, attachmentpath, referenceName, referenceRecid);
                dt = DS_Contr.Tables[0];
            }
            ViewBag.rowcount_contr = rwcnt;
            return View(dt);
        }
        public ActionResult Frm_FeedQuery_DetailsCardView(string eventrecid, int? userid = null, string feed_qry_recid = null, string username = null, bool isanonymous = true, 
            string SessionGUID = null, string command = null,string user_AB_ID=null, string referenceName = null, string referenceRecid = null)
        {
            //DataTable d1 = BASE._CoreDBOps.GetCenterDetails();
            //ViewBag.inchargeImg = (byte[])d1.Rows[0]["CEN_INCHARGE_IMAGE"];
            DataTable dt = null;
            if (DS_Contr != null)
            {            
                int rwcnt = DS_Contr.Tables[0].Rows.Count;
                if (command == null)
                {
                    if(rwcnt > 0)
                    {
                        eventrecid = "sample"; //DS_Contr.Tables[0].Rows[0]["EventRECID"].ToString();
                        DataRow dataRow = DS_Contr.Tables[0].Rows[0];
                        userid = 0; // (int)dataRow["USERID"];
                        feed_qry_recid = "sampleb"; //dataRow["FQI_REC_ID"].ToString();
                        username = "a"; // dataRow["USERNAME"].ToString();
                        if (dataRow["IS_ANONYMOUS"].ToString() == "1")
                        { isanonymous = true; }
                        else { isanonymous = false; }
                        referenceName = "";
                        referenceRecid = "";
                        //isanonymous = (int)dataRow["IS_ANONYMOUS"];
                    }

                }
                else if(command == "REFRESH")
                {
                    //eventrecid = eventrecid;
                }
            
                string readby = BASE._open_User_ID;
                if(rwcnt > 0)
                {
                    string attachmentpath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"];
                    DataSet DS = BASE._SMGS_DBOps.Get_All_Responses_FeedQuery(eventrecid, userid, feed_qry_recid, username, isanonymous, readby, attachmentpath, user_AB_ID,
                         referenceName, referenceRecid); //[sp_get_sm_AllFeedsQuerysResps]
                    dt = DS.Tables[0];
                }
            }
            else
            {
                ViewBag.rowcount_det = 0;
            }
            return View(dt);
        }

        [HttpPost]
        public ActionResult SubmitResponse(FormCollection collection)
            //string feed_Query_Response_RecId, string responseText, bool isanonymous, string eventrecid, int userid,
            //                            string User_Ab_ID, string referenceName = null, string referenceRecid = null)
        {
            #region
            //string User = null;
            ////User = user;
            //if (HttpContext.Session["LoginUser"] == null)
            //{
            //    User = user;
            //}
            //else
            //{
            //    User = HttpContext.Session["LoginUser"].ToString();
            //}
            //DataTable dt = BASE._SM_DBOps.GetUserid(User);
            //Int32 UserId = (int)dt.Rows[0]["Userid"];
            //string eventRecId = received_recid;
            ////string feedbackRating = null;
            /////if (System.Web.HttpContext.Current.Session["LoginUserId"] != null)
            //{
            //    UserId = (int)HttpContext.Session["LoginUserId"];
            //}
            //Boolean read_status = false;
            //DataTable cendt = BASE._SM_DBOps.geteventCenterID(eventRecId);
            //Int32 centerid = (int)cendt.Rows[0]["SR_CEN_ID"];
            //string recid = Guid.NewGuid().ToString();
            #endregion
            //var f = Request.Files;
            DateTime addon = DateTime.Now;
            string status = "";
            string feed_Query_Response_RecId = collection["feed_Query_Response_RecId"].ToString();
            string responseText = collection["responseText"].ToString();
            bool isanonymous = Convert.ToBoolean(collection["isanonymous"]);
            string eventrecid = collection["eventrecid"].ToString();
            int userid = Convert.ToInt32(collection["userid"]);
            string User_Ab_ID = collection["User_Ab_ID"].ToString();
            string referenceName = collection["referenceName"].ToString();
            string referenceRecid = collection["referenceRecid"].ToString();
            string cuserid = BASE._open_User_ID;
            string fqr_recid = Guid.NewGuid().ToString();
            string file_name = collection["Help_Document_FileName"].ToString();
            string responseText2 = collection["responseText"].ToString().Replace("\n", "<br />\r\n");
            bool insert_status = BASE._SMGS_DBOps.InsertFQRespopnse(feed_Query_Response_RecId, responseText2, addon, cuserid, isanonymous, fqr_recid);
            string reslt = "";
            string[] attachmentIdsList = { };
            string[] FilesList = { };
            string[] captionsList = { };
            string attachmentId = "";
            //Files Upload part starts from here onwards
            if (collection["Help_Document_FileName"] == "null" || collection["Help_Document_FileName"] == "undefined" ||
                    collection["Help_Document_FileName"] == "")
            {
                reslt = " result = True";
            }
            else
            {
                //here we need to call attatchment action method and add CEN_ID and Year_ID parameters to collection
                //Redirect.ToAction("Frm_Attachment_Window", "Attachment", new { param = parameter });
                Model_Attachment_Window model = new Model_Attachment_Window();
                model.Help_Document_FileName = collection["Help_Document_FileName"];
                DataTable dt = BASE._SMGS_DBOps.GetAttachmentDocID();
                model.Help_Document_NameID = (string)dt.Rows[0][0];
                model.Help_Byte_SessionName = collection["Help_Byte_SessionName"];
                model.Help_Post_Area_Name = collection["Help_Post_Area_Name"];
                model.Help_Post_Controller_Name = collection["Help_Post_Controller_Name"];
                model.Help_REF_REC_ID = fqr_recid; // collection["Help_REF_REC_ID"];
                model.Help_REF_SCREEN = collection["Help_REF_SCREEN"];
                model.Help_Document_Description = collection["Help_Document_Description"];
                model.Help_Checked = Convert.ToBoolean(collection["Help_Checked"]);
                model.Help_uploadControlName = collection["Help_uploadControlName"];
                model.Help_uploadControlActionMethod = collection["Help_uploadControlActionMethod"];
                //model.CEN_ID = Convert.ToString(cuserid);
                //model.Year_ID = DateTime.Today.Year;
                model.Help_uploadMethod = collection["Help_uploadMethod"]; // This is to specify that we are appending the files manually not by form submit.
                List<string> captions = new List<string>();
                //string[] captions_arr = collection["file_captions[]"][];
                for (int i = 0; i < collection["file_captions[]"].Split(',').Length; i += 1)
                {
                    captions.Add(collection["file_captions[]"].Split(',')[i].ToString());
                }
                model.Help_File_caption = captions;
                //AttachmentController attachmentController = new AttachmentController();
                //var x = attachmentController.Frm_Attachment_Window(model);
                var x = Attachment_Save(model);

                string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)x).Data);
                dynamic DynamicData = JsonConvert.DeserializeObject(jsonString);
                reslt = DynamicData.result.ToString();
                string Str_attachmentIdslist = DynamicData.AttachmentIdsList.ToString();
                string Str_FileList = DynamicData.FileList.ToString();
                string Str_captions = DynamicData.captionsList.ToString();
                attachmentIdsList = Str_attachmentIdslist.Split(',');
                FilesList = Str_FileList.Split(',');
                captionsList = Str_captions.Split('~');
                attachmentId = DynamicData.AttachmentID.ToString();
                //    attachmentIdsList = DynamicData.AttachmentIdsList.ToString().Split(',');
                //reslt = ((System.Web.Mvc.JsonResult)x).Data.ToString().Split(',')[1];
                //attachmentId = ((System.Web.Mvc.JsonResult)x).Data.ToString().Split(',')[2];
                //attachmentId = attachmentId.Replace(" AttachmentID = ", "") + "." + model.Help_Document_FileName.Split('.')[1];

            }

            //Files upload part ends here
            if (!insert_status)
            {
                if(userid > 0)
                {
                    DataTable dt_emailSMSDetails1 = BASE._SMGS_DBOps.Get_ResponseEmail_FeedbackQuery(eventrecid, feed_Query_Response_RecId).Tables[0]; //[sp_get_sm_AllFeedsQuerysResps]
                    DataTable dt_emailSMSDetails2 = BASE._SMGS_DBOps.Get_ResponseEmail_FeedbackQuery(eventrecid, feed_Query_Response_RecId).Tables[1]; //[sp_get_sm_AllFeedsQuerysResps]
                    sendEmailSMS_ForResponse(dt_emailSMSDetails1, dt_emailSMSDetails2, responseText, eventrecid, userid, User_Ab_ID, referenceName, referenceRecid, attachmentIdsList, FilesList, captionsList, file_name);
                }                
                status = "success";
                return Json(new { message = status });
            }

            else
            {
                status = "error";
                return Json(new { message = status });
            }
            //string status = "success";
            //return Json(new { message = status });

        }

        public void sendEmailSMS_ForResponse(DataTable dt_emailSMSDetails1, DataTable dt_emailSMSDetails2, string responseText, string eventrecid, int? enduserid,
            string USer_ab_id, string referenceName, string referenceRecid, string[] attachmentIdsList, string[] FilesList,  string[] captionsList, string file_name)
        {
            string source = dt_emailSMSDetails1.Rows[0]["SOURCE"].ToString();
            string eventName = "";
            string subject = "";
            if(referenceName != "EVENT")
            {
                eventName = "";
                subject = "Response to your " + source + " on Event: " + eventName;
            }
            else if (referenceName == "EVENT")
            {
                eventName = dt_emailSMSDetails2.Rows[0]["EVENTNAME"].ToString();
                subject = "Response to your " + source;
            }
            
            string endUserName = dt_emailSMSDetails1.Rows[0]["ENDUSERNAME"].ToString();
            string centerName = dt_emailSMSDetails1.Rows[0]["CENTERNAME"].ToString();
            string MobileNo = dt_emailSMSDetails1.Rows[0]["PhoneNumber"].ToString();
            string To_EmailID = dt_emailSMSDetails1.Rows[0]["Email"].ToString();
            string fileLinkText = "";
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"];
            if (file_name != "null" && file_name != "undefined" && file_name != "")
            {
                string file_caption = "";
                if (attachmentIdsList.Length == 1)
                {
                    
                    if(captionsList[0].Trim() != "") { file_caption = captionsList[0]; }
                    else { file_caption = FilesList[0]; }
                    fileLinkText = "<br>Attachment<br> " +
                    "<a href = "+ path + attachmentIdsList[0].Trim() + "'>"+captionsList[0]+"</a>";
                }
                else if (attachmentIdsList.Length > 1)
                {
                    fileLinkText = "<br>Attachments: <br> ";
                    for (int cnt = 0; cnt < attachmentIdsList.Length; cnt++)
                    {
                        int filecount = cnt + 1;                        
                        if (captionsList[cnt].Trim() != "") { file_caption = captionsList[cnt]; }
                        else { file_caption = FilesList[cnt]; }
                        fileLinkText += "<br> <a href =" + path + attachmentIdsList[cnt].Trim() + "'>"+ file_caption + "</a>";
                    }
                }

            }
            string body = "<br>Dear User, <br> Thank you for posting the " + source + ". Here is your response. <br><ul>Response:- '" + responseText +".'<br>" + fileLinkText;
            
            string EmailMessage = ""; string smsMessageToUser = "";
            //To_EmailID = "ramakrishna.boyapati@bkconnect.net";
            bool sent = SendHTMLEmail(ref EmailMessage, To_EmailID, subject, "~/Areas/Facility/Views/Feedback_Query_Mgmt/ResponseToFeedbackQueryEmailer.cshtml", body);
            if(referenceName == "EVENT")
            {
                if (source.ToUpper() == "FEEDBACK")
                {
                    if (MobileNo != "")
                    {
                        BASE._Notifications_DBOps.SendSMS(enduserid, "ResponseForFeedbackToUser", ref smsMessageToUser, eventrecid, null, MobileNo, USer_ab_id);
                    }
                }
                if (source.ToUpper() == "QUERY")
                {
                    if (MobileNo != "")
                    {
                        BASE._Notifications_DBOps.SendSMS(enduserid, "ResponseForQueryToUser", ref smsMessageToUser, eventrecid, null, MobileNo, USer_ab_id);
                    }
                }
            }
            else if(referenceName != "EVENT")
            {
                //we need to set the SMS structure
            }

        }
        public ActionResult getAttachments(string fqr_recid)
        {
            string filenames = ""; string aliattachmentids = ""; string captions = "";
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["attachmentpath"];
            DataTable dt = BASE._SMGS_DBOps.GetAttachments_FeedQuery(path, fqr_recid);
            
            if(dt != null || dt.Rows.Count > 0)
            {
                int rwcount = dt.Rows.Count;
                for (int i = 0; i < rwcount; i++)
                {
                    if (i == 0)
                    {
                        filenames = dt.Rows[i]["File_Name"].ToString();
                        aliattachmentids = dt.Rows[i]["ALI_ATTACHMENT_ID"].ToString();
                        captions = dt.Rows[i]["AI_DESCRIPTION"].ToString();
                    }
                    else
                    {
                        filenames = filenames + "," + dt.Rows[i]["File_Name"].ToString();
                        aliattachmentids = aliattachmentids + "," + dt.Rows[i]["ALI_ATTACHMENT_ID"].ToString();
                        captions = captions + "!_(" + dt.Rows[i]["AI_DESCRIPTION"].ToString();
                    }
                }
                return Json(new { filenames = filenames, filerecids = aliattachmentids, filecaptions = captions ,message = "success" });
            }
            else
            {
                return Json(new { message = "error" });
            }
                
        }

        //public ActionResult Frm_FeedQuery_EventsCardView(String viewmode = "default", String searchmode = "default", String searchtext = "", String startdate = null, String enddate = null, String insid = null,
        //String SessionGUID = null, int? centerid = null, String speaker = null, String themeid = null, int? ratingfrom = null, int? ratingto = null, String centername = null, String wingshort = null,
        //String cityid = null, String stateid = null, String attachmentRootPath = null, String wingid = null, String topicname = null, String tabchange = "default", String wingname = null,
        //Boolean webview = false, Boolean needimages = false, Boolean needvideos = false)
        //{
        //    //FilterParameters filterParams = new FilterParameters();
        //    //string CenID = "";
        //    //CenID = "2210";
        //    //string FY = "1516";

        //    //var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
        //    //var MyGuid = string.IsNullOrEmpty(SessionGUID) || SessionGUID == "null" ? Guid.NewGuid() : Guid.Parse(SessionGUID);
        //    //AllBASEs.Add(new BaseModel
        //    //{
        //    //    CenterGuid = MyGuid,
        //    //    BASE = BASE
        //    //});
        //    //SessionGUID = MyGuid.ToString();
        //    //ViewData["SessionGUID"] = SessionGUID;
        //    //Session["BASEClass"] = AllBASEs;
        //    //BASE._open_Cen_ID = Convert.ToInt32(CenID);
        //    //BASE._open_User_ID = CenID;
        //    //BASE._open_Year_ID = Convert.ToInt32(FY);
        //    //BASE._open_UID_No = "02284/BK/001";
        //    //BASE._open_Year_Sdt = Convert.ToDateTime("01-04-2015");
        //    //BASE._open_User_Type = "CLIENT ROLE";
        //    centerid = BASE._open_Cen_ID;
        //    if (startdate == "") { startdate = null; }
        //    if (enddate == "") { enddate = null; }
        //    DateTime? fromdate = null;
        //    DateTime? todate = null;
        //    if (startdate != null) { fromdate = Convert.ToDateTime(startdate); };
        //    if (todate != null) { todate = Convert.ToDateTime(enddate); };

        //    attachmentRootPath = "http://Connectonedev.bkinfo.in/Attachments/";
        //    attachmentRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["thumbnailpath"];
        //    viewmode = "snapshot";
        //    DT_El = BASE._SMGS_DBOps.Get_SM_EventsList((DateTime?)fromdate, (DateTime?)todate, (int?)centerid, speaker, themeid,
        //                    (int?)ratingfrom, (int?)ratingto, cityid, stateid, attachmentRootPath, wingid, topicname, viewmode, 
        //                    searchmode, searchtext, tabchange, insid, webview, needimages, needvideos);

        //    return View(DT_El);
        //}

        
        public ActionResult updateShowinPublic(string fqrrecid, bool trueorfalse)
        {
            bool status = BASE._SMGS_DBOps.UpdateShowinPublic(fqrrecid, trueorfalse);

            return Json(status);
        }
    }
}