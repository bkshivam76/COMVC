using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Areas.Membership.Models;
using System.Data;
namespace ConnectOneMVC.Areas.Membership.Controllers
{
    public class WingMembershipController : BaseController
    { 
        // GET: Membership/WingMembership
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult wing_membership_Register()
        {
            return View();
        }
        public ActionResult wing_membership_Register_new()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            return View();
        }
        public ActionResult new_wing_membership_window()
        {
            return View();
        }
        public ActionResult Renew_wing_membership_window()
        {
            return View();
        }
        public ActionResult Conversion_wing_membership_window()
        {
            return View();
        }
        public ActionResult MembershipConfirmation(String ResponseID, String Approver, string ActionTaken)
        {
            //add confirmation in remarks of Request form
            BASE._Membership_DBOps.UpdateWingMembershipConfirmation(ResponseID, Approver, ActionTaken);
            
            string ToEmail1 = ""; string ToEmail2 = ""; string Subject = ""; string ReplyTo = "";
            string ContentPath = "~/Areas/Membership/Views/WingMembership/MembershipConfirmationNotification.cshtml";
            //send Notification to Wings 
            DataTable _table= BASE._SM_DBOps.Get_Membership_Confirmation_Email(ResponseID);
            MembershipConfirmationEmail model_wing = new MembershipConfirmationEmail();
            model_wing.Status = ActionTaken;
            model_wing.DateOfConfirmation = DateTime.Now.ToString("dd-MMMM-yyyy HH:mm");
            model_wing.Wing = _table.Rows[0]["WING_NAME"].ToString();
            model_wing.Approver = Approver;
            model_wing.MemberName = _table.Rows[0]["APPLICANT"].ToString();
            model_wing.FormViewLink = _table.Rows[0]["VIEW_FORM_LINK"].ToString();
            ToEmail1 = _table.Rows[0]["WING_EMAIL_1"].ToString();
            ToEmail2 = _table.Rows[0]["WING_EMAIL_2"].ToString();
            Subject = model_wing.Wing + " Membership Request by "+ model_wing.MemberName  + " has been "+ ActionTaken + " by "+ Approver;
            ReplyTo = _table.Rows[0]["REPLY_TO"].ToString();
            string html = RenderViewToString(ControllerContext, ContentPath, model_wing, false);
            bool sent = BASE._Notifications_DBOps.SendEmail(ToEmail1, Subject, html, "Wing Membership Request Process Notification", ToEmail2, "",ReplyTo);
            
            //send Notification to Center
            MembershipConfirmationEmail model_centre = new MembershipConfirmationEmail();
            model_centre.Status = ActionTaken;
            model_centre.DateOfConfirmation = model_wing.DateOfConfirmation;
            model_centre.Wing = _table.Rows[0]["WING_NAME"].ToString();
            model_centre.Approver = Approver;
            model_centre.MemberName = _table.Rows[0]["APPLICANT"].ToString();
            model_centre.FormViewLink = _table.Rows[0]["VIEW_FORM_LINK"].ToString();
            ToEmail1 = _table.Rows[0]["CEN_EMAIL_ID_1"].ToString();
            ToEmail2 = _table.Rows[0]["CEN_EMAIL_ID_2"].ToString();
            html = RenderViewToString(ControllerContext, ContentPath, model_centre, false);
            sent = BASE._Notifications_DBOps.SendEmail(ToEmail1, Subject, html, "Wing Membership Request Process Notification", ToEmail2, "", ReplyTo);
            
            //send Notification to Applicant
            MembershipConfirmationEmail model_applicant = new MembershipConfirmationEmail();
            model_applicant.Status = ActionTaken;
            model_applicant.DateOfConfirmation = model_wing.DateOfConfirmation;
            model_applicant.Wing = _table.Rows[0]["WING_NAME"].ToString();
            model_applicant.Approver = Approver;
            model_applicant.MemberName = _table.Rows[0]["APPLICANT"].ToString();
            model_applicant.FormViewLink = _table.Rows[0]["VIEW_FORM_LINK"].ToString();
            ToEmail1 = _table.Rows[0]["APPLICANT_EMAIL_1"].ToString();
            ToEmail2 = _table.Rows[0]["APPLICANT_EMAIL_2"].ToString();
            html = RenderViewToString(ControllerContext, ContentPath, model_applicant, false);
            sent = BASE._Notifications_DBOps.SendEmail(ToEmail1, Subject, html, "Wing Membership Request Process Notification", ToEmail2, "", ReplyTo);

            //send Notification to RERF office 
            MembershipConfirmationEmail model_rerf = new MembershipConfirmationEmail();
            model_rerf.Status = ActionTaken;
            model_rerf.DateOfConfirmation = model_wing.DateOfConfirmation;
            model_rerf.Wing = _table.Rows[0]["WING_NAME"].ToString();
            model_rerf.Approver = Approver;
            model_rerf.MemberName = _table.Rows[0]["APPLICANT"].ToString();
            model_rerf.FormViewLink = _table.Rows[0]["VIEW_FORM_LINK"].ToString();
            ToEmail1 = "rerf@bkivv.org";
            ToEmail2 = "";
            html = RenderViewToString(ControllerContext, ContentPath, model_rerf, false);
            sent = BASE._Notifications_DBOps.SendEmail(ToEmail1, Subject, html, "Wing Membership Request Process Notification", ToEmail2, "", ReplyTo);
            return View();
        }
        public ActionResult MembershipConfirmationNotification(MembershipConfirmationEmail model)
        {
            return View(model);
        }
    }
}