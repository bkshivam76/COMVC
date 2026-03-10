using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using DevExpress.Pdf;
using System.Drawing;
using System.IO;
using System.Data;
namespace ConnectOneMVC.Areas.Start.Controllers
{
    public class VouchingAuditController : BaseController
    {
        // GET: Start/VouchingAudit
        public ActionResult VouchingScreen(string InsttID, string Vouching_Category)
        {
            DataSet _ReturnData = BASE._Voucher_DBOps.GetVouchingAuditData(InsttID, Vouching_Category.Replace("_"," "));
            if (_ReturnData.Tables.Count > 0)
            {
                if (_ReturnData.Tables[0].Rows.Count > 0)
                {
                    Models.VouchingAuditModel _Model = new Models.VouchingAuditModel();
                    _Model.VF_Instt_Code = InsttID;
                    _Model.VF_Category = Vouching_Category;
                    _Model.VF_ENTRY_ID = _ReturnData.Tables[0].Rows[0][0].ToString();
                    _Model.Curr_Attachment_File_Name = _ReturnData.Tables[1].Rows[0]["ATTACHMENT_FILE_NAME"].ToString();
                    _Model.VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party = _ReturnData.Tables[2].Rows[0]["VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party"].ToString(); ;
                    _Model.DonationData= BASE._DonationRegister_DBOps.GetList(); 
                    return View(_Model);
                }
            }
            Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(); 
        }
        [HttpPost]
        public ActionResult VouchingScreen(Models.VouchingAuditModel _Model)
        {
            Common_Lib.RealTimeService.Param_AddVouchingAudit InParam = new Common_Lib.RealTimeService.Param_AddVouchingAudit();
            InParam.Entry_ID = _Model.VF_ENTRY_ID;
            InParam.Vouching_Category = "CASH DONATIONS";
            if (_Model.VF_Vouching_Status.ToLower() == "accepted")
            {
                InParam.Vouching_Status = Common_Lib.RealTimeService.Vouching_audit_Status.Accepted;
            }
            if (_Model.VF_Vouching_Status.ToLower() == "rejected")
            {
                InParam.Vouching_Status = Common_Lib.RealTimeService.Vouching_audit_Status.Rejected;
            }
            Common_Lib.RealTimeService.Param_AddVouchingAuditResponse[] Responses = new Common_Lib.RealTimeService.Param_AddVouchingAuditResponse[3];

            Common_Lib.RealTimeService.Param_AddVouchingAuditResponse inParam_Response_1 = new Common_Lib.RealTimeService.Param_AddVouchingAuditResponse();
            inParam_Response_1.Question = "Donation Letter in correct name of Institute?";
            inParam_Response_1.Response = _Model.VF_Cash_Donation_Correct_Name_Instt ? "YES" : "NO";
            Responses[0]= inParam_Response_1;

            Common_Lib.RealTimeService.Param_AddVouchingAuditResponse inParam_Response_2 = new Common_Lib.RealTimeService.Param_AddVouchingAuditResponse();
            inParam_Response_2.Question = "Type of Donation?";
            inParam_Response_2.Response = _Model.VF_Cash_Donation_Donation_Type;
            Responses[1] = inParam_Response_2;

            Common_Lib.RealTimeService.Param_AddVouchingAuditResponse inParam_Response_3 = new Common_Lib.RealTimeService.Param_AddVouchingAuditResponse();
            inParam_Response_3.Question = "Signed?";
            inParam_Response_3.Response = _Model.VF_Cash_Donation_Signed ? "YES" : "NO";
            Responses[2] = inParam_Response_3;

            InParam.Responses = Responses;
            BASE._Voucher_DBOps.AddVouchingAudit(InParam);

            DataSet _ReturnData = BASE._Voucher_DBOps.GetVouchingAuditData(_Model.VF_Instt_Code, _Model.VF_Category);
            if (_ReturnData.Tables.Count > 0)
            {
                if (_ReturnData.Tables[0].Rows.Count > 0)
                {
                    string InsttID = _Model.VF_Instt_Code;
                    string Vouching_Category = _Model.VF_Category;
                    _Model = new Models.VouchingAuditModel();
                    _Model.VF_Instt_Code = InsttID;
                    _Model.VF_Category = Vouching_Category;
                    _Model.VF_ENTRY_ID = _ReturnData.Tables[0].Rows[0][0].ToString();
                    _Model.Curr_Attachment_File_Name = _ReturnData.Tables[1].Rows[0]["ATTACHMENT_FILE_NAME"].ToString();
                    _Model.VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party = _ReturnData.Tables[2].Rows[0]["VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party"].ToString(); ;
                }
            }
            return View(_Model);
        }
        public ActionResult VouchingLeftPanel()
        {
            return PartialView();
        }
        public ActionResult VouchingRightPanel()
        {
            return PartialView();
        }
        public ActionResult VouchingMidPanel()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult VouchingForm_CashDonations()
        {
            return PartialView();
        }

        public ActionResult VouchingAuditorDashboard()
        {
            List<Common_Lib.DbOperations.Vouchers.Return_GetVouchingAuditDashboard> _Data = BASE._Voucher_DBOps.GetVouchingAuditDashboard(BASE._open_User_ID);
            return View(_Data);
        }

        public ActionResult VouchingAuditorDashboardCardView()
        {
            
            return View();
        }
    }

    public static class WidgetsDemoHelper
    {
        public static List<string> GetWidgets()
        {
            return new List<string>() { "DateTime", "Mail", "Trading", "Weather", "Calendar" };
        }
    }
}