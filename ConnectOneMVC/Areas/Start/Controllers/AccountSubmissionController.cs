using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using ConnectOneMVC.Areas.Start.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using Newtonsoft.Json;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    public class AccountSubmissionController : BaseController
    {
        // GET: Start/AccountSubmission
        public ActionResult Index()
        {

            return View();
        }

        #region Submit/Unsubmit Accounts
        public ActionResult Frm_Submit_Accounts()
        {
            ViewBag.YearName = BASE._open_Year_Name;
            ViewBag.OpenYearOfFY = Convert.ToString(BASE._open_Year_Sdt.Year);
            ViewBag.CloseYearofFY = Convert.ToString(BASE._open_Year_Edt.Year);
            ViewBag.EndDate = Convert.ToString(BASE._open_Year_Edt);
            AccountSubmissionModel Model = new AccountSubmissionModel();

            //DataTable inData = new DataTable();
            DataTable inData = BASE._CenterDBOps.GetAccountsSubmittedPeriod();
            if (inData.Rows.Count > 0)
            {
                if (IsDate(inData.Rows[0]["CSA_SUBMIT_ON"].ToString()))
                {
                    DateTime _cDate = Convert.ToDateTime(inData.Rows[0]["CSA_SUBMIT_ON"]);
                    ViewBag.lblLastCompletionOn = "Accounts for current year was last submitted on " + _cDate.ToString(BASE._Date_Format_DD_MMM_YYYY);
                }
                if (IsDate(inData.Rows[0]["CSA_SUBMIT_TO"].ToString()))
                {
                    Model.PrevSubmittedTill_SubmitAccounts = Convert.ToDateTime(inData.Rows[0]["CSA_SUBMIT_TO"]);
                    //SetCheckBoxesForSubmittedPeriod(Model.PrevSubmittedTill_SubmitAccounts);
                }
            }
            return View(Model);
        }
        public ActionResult Pending_Accounts_Submission()
        {
            string ReturnMessage = "";
            DataTable inData = BASE._CenterDBOps.GetLatestAccountsSubmittedPeriod();
            if (inData.Rows.Count > 0)
            {
                if (IsDate(inData.Rows[0]["CSA_SUBMIT_TO"].ToString()))
                {
                    DateTime PrevSubmittedTill_SubmitAccounts = Convert.ToDateTime(inData.Rows[0]["CSA_SUBMIT_TO"]);
                    DateTime LastSubmissionTillDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                    if (LastSubmissionTillDate > PrevSubmittedTill_SubmitAccounts && BASE._open_User_Type == "CLIENT ROLE")
                    {
                        ReturnMessage = "Alert !!<br/><br/>You have not submitted your books of Accounts after " + PrevSubmittedTill_SubmitAccounts.ToString("dd-MMM-yyyy");
                    }
                }
            }
            else
            {
                ReturnMessage = "Alert !!<br/><br/>You have not submitted your books of Accounts";
            }
            return Json(new
            {
                ReturnMessage
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Submit_Success(string ToDate, string PrevSubmittedTill)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                DateTime _ToDate = string.IsNullOrEmpty(ToDate) ? new DateTime(0001, 1, 1) : Convert.ToDateTime(ToDate);
                String NagtivecheckMsg = "";

                if (_ToDate > new DateTime(0001, 1, 1))
                {
                    NagtivecheckMsg = NegativeBalanceCheck(BASE._open_Year_Sdt, _ToDate);

                }

                if (NagtivecheckMsg.Length > 0)
                {
                    jsonParam.message = NagtivecheckMsg;
                    jsonParam.title = "Error..";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_AddAccountsSubmissionPeriod InParam = new Common_Lib.RealTimeService.Param_AddAccountsSubmissionPeriod();
                InParam.FromDate = BASE._open_Year_Sdt;
                InParam.ToDate = _ToDate;
                InParam.PrevSubmittedTill = string.IsNullOrEmpty(PrevSubmittedTill) ? new DateTime(0001, 1, 1) : Convert.ToDateTime(PrevSubmittedTill);
                BASE._CenterDBOps.AddAccountsSubmissionPeriod(InParam);

                jsonParam.message = "Accounts Submitted Successfully";
                jsonParam.title = "Success!!";
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
        private bool IsDate(string text)
        {
            //DateTime temp;
            if (DateTime.TryParse(text, out DateTime temp))
                return true;
            else
                return false;
        }
        private string NegativeBalanceCheck(DateTime FromDate, DateTime ToDate)
        {
            String Msg = "";
            // Check Negative Cash Balance for Audit Period Only
            Double Opening_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(FromDate, ToDate, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Common_Lib.Messages.SomeError;
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Opening_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Opening_Bal = 0;

                }
            }
            else
            {
                Opening_Bal = 0;
            }
            if (Opening_Bal < 0)
            {
                return "Accounts cannot be submitted as Cash Balance is NEGATIVE. <br> ========================================== <br> Opening Cash Balance <br> Amount : " + Opening_Bal.ToString("#,0.##") + "<br> ========================================= <br> For more details: Check Daily Balances Report";
            }
            DataTable TR_Table = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");
            if (TR_Table == null)
            {
                return Common_Lib.Messages.SomeError;
            }
            // (1.3) Cash Opening Balance Insert..........

            DataRow ROW = TR_Table.NewRow();
            ROW["iTR_SORT_REC"] = "A";
            ROW["iTR_DATE"] = BASE._open_Year_Sdt.ToString(BASE._Date_Format_Current);
            ROW["iTR_REC_CASH"] = Opening_Bal;
            ROW["iTR_PAY_CASH"] = 0;
            TR_Table.Rows.Add(ROW);

            // (1.4) Cash Data Sorting

            DataView DV1 = new DataView(TR_Table);
            DV1.Sort = "iTR_DATE,iTR_SORT_REC";
            DataTable XTABLE = DV1.ToTable();
            Double _Temp_Balance = 0;
            Double _Temp_Receipt = 0;
            Double _Temp_Payment = 0;

            // (1.5) Check Negative Cash

            foreach (DataRow XROW in XTABLE.Rows)
            {
                if (!Convert.IsDBNull(XROW["iTR_REC_CASH"]))
                {
                    _Temp_Receipt = Convert.ToDouble(XROW["iTR_REC_CASH"]);
                }
                else
                {
                    _Temp_Receipt = 0;
                }
                if (!Convert.IsDBNull(XROW["iTR_PAY_CASH"]))
                {
                    _Temp_Payment = Convert.ToDouble(XROW["iTR_PAY_CASH"]);
                }
                else
                {
                    _Temp_Payment = 0;
                }
                if (_Temp_Receipt <= 0 && _Temp_Payment <= 0)
                {

                }
                else
                {
                    _Temp_Balance = Math.Round((_Temp_Balance + _Temp_Receipt) - _Temp_Payment, 2);
                }
                if (_Temp_Balance < 0 && Convert.ToDateTime(XROW["iTR_DATE"]) >= FromDate && Convert.ToDateTime(XROW["iTR_DATE"]) <= ToDate)
                {
                    Msg = "Accounts cannot be submitted as Cash Balance is NEGATIVE.<br>==========================================<br>Date      : " + Convert.ToDateTime(XROW["iTR_DATE"]).ToString("dd-MMM, yyyy") + " <br> Amount : " + _Temp_Balance.ToString("#,0.##") + " <br> ========================================== <br> For more details: Check Daily Balances Report";
                }

            }

            return Msg;
        }

        #endregion

        #region SubmitReport, ReturnForCorrection, VerifyAudit
        [HttpGet]
        public ActionResult Frm_Submit_Report()
        {
            try
            {
                ViewBag.Txt_Center_Name = BASE._open_Cen_Name;
                string Grade = BASE._CenterDBOps.GetlatestCenterGrade();
                ViewBag.Txt_Grade = "Grade : " + Grade;
                if (Grade == "A" || Grade == "B")
                {
                    ViewBag.chk_Verify = false;
                    ViewBag.Disabled = true;
                }
                else
                {
                    ViewBag.chk_Verify = true;
                    ViewBag.Disabled = false;
                }
                return View();
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Frm_Submit_Report(bool chk_Verify_ReportSubmit)
        {
            try
            {
                if (!BASE._CenterDBOps.SubmitReport())
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError,
                        logout = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false,
                    logout = false
                }, JsonRequestBehavior.AllowGet);
            }
            if (chk_Verify_ReportSubmit == true)
            {
                int eventID;
                try
                {
                    eventID = (int)BASE._CenterDBOps.VerifyAudit();
                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        message,
                        result = false,
                        logout = false
                    }, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    if (eventID != 0)
                    {
                        string ResultString = "";
                        BASE._Notifications_DBOps.SendSMS(eventID, "AUDIT_VERIFICATION", ref ResultString);
                        string Message = "Audit Verified Successfully..!<br><br>" + ResultString + "<br>Om Shanti...!";
                        return Json(new
                        {
                            result = true,
                            message = Message,
                            logout = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string ResultString = "";
                        BASE._Notifications_DBOps.SendSMS(null, "REPORT_SUBMISSION", ref ResultString);
                        string Message = Messages.SomeError + "<br>!! PLEASE NOTE !!<br><br> Only Report is Submitted Successfully..!<br> Some Error Happened in Verification<br>" + ResultString + "<br>Om Shanti...!";
                        return Json(new
                        {
                            result = true,
                            message = Message,
                            logout = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        message,
                        result = false,
                        logout = true
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                try
                {
                    string ResultString = "";
                    BASE._Notifications_DBOps.SendSMS(null, "REPORT_SUBMISSION", ref ResultString);
                    string Message = "Report Submitted Successfully..!<br><br>" + ResultString + "<br>Om Shanti...!";
                    return Json(new
                    {
                        result = true,
                        message = Message,
                        logout = true
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        message,
                        result = false,
                        logout = true
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Check_Submission_Restrictions()
        {
            if (BASE._CenterDBOps.GetAuditTxnPeriod().Rows.Count == 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Sorry! Current Center / Year is not allotted for audit to current user..!"
                }, JsonRequestBehavior.AllowGet);
            }
            DataTable AuditPeriod = BASE._CenterDBOps.GetAuditPeriod();
            if (AuditPeriod.Rows.Count == 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Invalid Allocation!!"
                }, JsonRequestBehavior.AllowGet);
            }
            Decimal Opening_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary((DateTime)AuditPeriod.Rows[0]["HE_TNXS_FROM"], (DateTime)AuditPeriod.Rows[0]["HE_TO"], BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Opening_Bal = (Decimal)Cash_Bal.Rows[0]["OPENING"];
                }
                else { Opening_Bal = 0; }
            }
            else { Opening_Bal = 0; }
            DataTable TR_Table = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");
            if (TR_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            DataRow ROW = TR_Table.NewRow();
            ROW["iTR_SORT_REC"] = "A";
            ROW["iTR_DATE"] = BASE._open_Year_Sdt.ToString(BASE._Date_Format_Current);
            ROW["iTR_REC_CASH"] = Opening_Bal;
            ROW["iTR_PAY_CASH"] = 0;
            TR_Table.Rows.Add(ROW);
            DataView DV1 = new DataView(TR_Table);
            DV1.Sort = "iTR_DATE,iTR_SORT_REC";
            DataTable XTABLE = DV1.ToTable();
            Decimal _Temp_Balance = 0;
            Decimal _Temp_Receipt = 0;
            Decimal _Temp_Payment = 0;
            foreach (DataRow XROW in XTABLE.Rows)
            {
                if (!Convert.IsDBNull(XROW["iTR_REC_CASH"]))
                {
                    _Temp_Receipt = (Decimal)XROW["iTR_REC_CASH"];
                }
                else { _Temp_Receipt = 0; }
                if (!Convert.IsDBNull(XROW["iTR_PAY_CASH"]))
                {
                    _Temp_Payment = (Decimal)XROW["iTR_PAY_CASH"];
                }
                else { _Temp_Payment = 0; }
                if (_Temp_Receipt <= 0 && _Temp_Payment <= 0)
                {

                }
                else
                {
                    _Temp_Balance = Math.Round((_Temp_Balance + _Temp_Receipt) - _Temp_Payment, 2);
                }
                if (_Temp_Balance < 0 && (DateTime)XROW["iTR_DATE"] >= (DateTime)AuditPeriod.Rows[0]["HE_TNXS_FROM"] && (DateTime)XROW["iTR_DATE"] <= (DateTime)AuditPeriod.Rows[0]["HE_TO"])
                {
                    return Json(new
                    {
                        result = false,
                        message = "Print out cannot be taken as Cash Balance is NEGATIVE.<br>==========================================<br>Date : " + Convert.ToDateTime(XROW["iTR_DATE"]).ToString("dd-MMM, yyyy") + "<br> Amount : " + _Temp_Balance.ToString("#,0.##") + "<br>====================================<br>For more details: Check Daily Balances Report"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (BASE._AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer((DateTime)AuditPeriod.Rows[0]["HE_TNXS_FROM"], (DateTime)AuditPeriod.Rows[0]["HE_TO"]) > 0)
            {
                return Json(new
                {
                    result = false,
                    message = "Some of the Asset Transfer Entries with other centres are not matched yet !!<br>Match all the pending asset transfer entries. Then prints can be taken."
                }, JsonRequestBehavior.AllowGet);
            }
            if (BASE._Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount((DateTime)AuditPeriod.Rows[0]["HE_TNXS_FROM"], (DateTime)AuditPeriod.Rows[0]["HE_TO"]) > 0)
            {
                string Msg = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts-> Internal Transfer Register<br>Prints can be taken if 1) All Transfers with centers 2) TDS Transfers with H.Q. 3)Non-Cash Transfers with H.Q. are matched.";
                if (BASE._open_Ins_ID != "00001")
                {
                    Msg = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts->Internal Transfer Register for unmatched Entries.";
                }
                return Json(new
                {
                    result = false,
                    message = Msg
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnForCorrection()
        {
            try
            {
                if (BASE._CenterDBOps.ReturnedForCorrection())
                {
                    return Json(new
                    {
                        result = true,
                        message = "Returned For Correction Successfully..!<br><br>Om Shanti...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VerifyAudit()
        {
            try
            {
                int eventID = (int)BASE._CenterDBOps.VerifyAudit();
                if (eventID != 0)
                {
                    string ResultString = "";
                    BASE._Notifications_DBOps.SendSMS(eventID, "AUDIT_VERIFICATION", ref ResultString);
                    string Message = "Audit Verified Successfully..!<br><br>" + ResultString + "<br>Om Shanti...!";
                    return Json(new
                    {
                        result = true,
                        message = Message
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Accounts Submission History


        //public ActionResult Frm_Report_Submit_Accounts()
        //{
        //    BASE._SessionDictionary.Add("GridData_SubmissionHistory", BASE._CenterDBOps.GetAccountsSubmissionReport());
        //    BASE._SessionDictionary.TryGetValue("GridData_SubmissionHistory", out object value);

        //    return View(value);
        //}
        //public ActionResult Frm_Report_Submit_Accounts_Grid()
        //{
        //    BASE._SessionDictionary.TryGetValue("GridData_SubmissionHistory", out object value);
        //    return View(value);
        //}

        public void SessionClear()
        {
            BASE._SessionDictionary.Remove("GridData_SubmissionHistory");
        }

        public ActionResult Frm_Report_Submit_Accounts()
        {
            return View();
        }
        public ActionResult Frm_Report_Submit_Accounts_Grid()
        {
            return Content(JsonConvert.SerializeObject(BASE._CenterDBOps.GetAccountsSubmissionReport()), "application/json");
        }

        #endregion
    }
}