using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using System;
using System.Data;
using System.Web.Mvc;
namespace ConnectOneMVC.Areas.Start.Controllers
{
    public class AuditRegistrationController : BaseController
    {
        // GET: Start/AuditRegistration
        public ActionResult Index()
        {
            return View();
        }
        [CheckLogin]
        public ActionResult RegisterForAudit()
        {
            return View();
        }

        public JsonResult SupportInfoExists()
        {
            DataTable general_DbOps = BASE._CoreDBOps.GetList();
            if (general_DbOps.Rows.Count == 0)
            { return Json(new { result = false, Message = "" }, JsonRequestBehavior.AllowGet); }
            return Json(new { result = true, Message = "" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckDocumentsPending()
        {
            DataSet AllPendingDS = BASE._Audit_DBOps.GetAllPendingAttachments();
            //if (d5.Rows[0]["CEN_ACC_RES_PERSON_AB_ID"] is null)
            //{ return Json(new { result = "", Message = "" }, JsonRequestBehavior.AllowGet); }
            return Json(new { result = true, Message = "" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MarkAsRegisteredForAudit(bool confirmationTaken = false)
        {
            try
            {
                DataTable d1 = BASE._Audit_DBOps.RegisterForAudit(confirmationTaken);
                if (d1 == null || d1.Rows.Count == 0)
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.IsDBNull(d1.Rows[0][1]))
                {
                    return Json(new { result = false, message = d1.Rows[0][0].ToString() }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToBoolean(d1.Rows[0][1]) == false)
                {
                    return Json(new { result = false, message = d1.Rows[0][0].ToString(), takeConfirmation = true }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToBoolean(d1.Rows[0][1]) == true)
                {
                    string ResultString = "";
                    BASE._Notifications_DBOps.SendSMS(Convert.ToInt32(d1.Rows[0][2]), "AUDIT_REGISTRATION", ref ResultString);
                    string Message = d1.Rows[0][0].ToString() + "<br><br>" + ResultString;
                    return Json(new { result = true, message = Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new { result = false, message = Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CancelAuditRegistration(bool confirmationTaken = false)
        {
            try
            {
                DataTable d1 = BASE._Audit_DBOps.CancelAuditRegistration(confirmationTaken);
                if (d1 == null || d1.Rows.Count == 0)
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.IsDBNull(d1.Rows[0][1]))
                {
                    return Json(new { result = false, message = d1.Rows[0][0].ToString() }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToBoolean(d1.Rows[0][1]) == false)
                {
                    return Json(new { result = false, message = d1.Rows[0][0].ToString(), takeConfirmation = true }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToBoolean(d1.Rows[0][1]) == true)
                {
                    string ResultString = "";
                    BASE._Notifications_DBOps.SendSMS(Convert.ToInt32(d1.Rows[0][2]), "AUDIT_REGISTRATION_CANCELLED", ref ResultString);
                    string Message = d1.Rows[0][0].ToString() + "<br><br>" + ResultString;
                    return Json(new { result = true, message = Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new { result = false, message = Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Check_Restrictions(string Msg)
        {
            var User_Type = BASE._open_User_Type.ToUpper();
            Return_Json_Param jsonParam = new Return_Json_Param();
            double Opening_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Edt, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.result = false;
                jsonParam.title = "Error!!";
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
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

            DataTable TR_Table = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");
            if (TR_Table == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.result = false;
                jsonParam.title = "Error!!";
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            //DataTable dTABLE = BASE._Voucher_DBOps.GetBankEntriesCountInNextEvent();
            //if (dTABLE.Rows.Count > 0)
            //{
            //    DateTime AssignedEventTxnToDate = (Convert.ToDateTime(dTABLE.Rows[0]["Txn_To_Date"])).AddDays(1);
            //    jsonParam.message = "There is no entry in saving bank A/c after " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + ". So please deposit cash in bank or withdraw from bank,update your bank Passbook and enter in CONNECTONE after " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + " !!<br>==========================================<br> Acc. No.       : " + dTABLE.Rows[0]["BA_ACCOUNT_NO"].ToString() + "<br>Bank             : " + dTABLE.Rows[0]["BI_SHORT_NAME"].ToString() + "<br>==========================================<br>Please post atleast one entry in this bank after " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + " to Register for Audit. <br> <br> सेविंग बैंक अकाउंट में " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + " के बाद कोई एंट्री नहीं है . कृपया बैंक में कैश निकासी अथवा जमा करें, आपकी पासबुक अपडेट करें और CONNECTONE में " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + " के बाद की एंट्री करें !!<br>==========================================<br> अकाउंट नंबर : " + dTABLE.Rows[0]["BA_ACCOUNT_NO"].ToString() + "<br>बैंक : " + dTABLE.Rows[0]["BI_SHORT_NAME"].ToString() + "<br>==========================================<br>ऑडिट के लिए रजिस्टर करवाने के लिए  " + AssignedEventTxnToDate.ToString("dd MMM yyyy") + " के बाद की इस बैंक में कम से कम एक एंट्री करें.";
            //    jsonParam.result = false;
            //    return Json(new
            //    {
            //        jsonParam,
            //        User_Type
            //    }, JsonRequestBehavior.AllowGet);
            //}

            DataTable Bank_Entry = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");

            // (1.3) Cash Opening Balance Insert..........
            DataRow ROW;
            ROW = TR_Table.NewRow();
            ROW["iTR_SORT_REC"] = "A";
            ROW["iTR_DATE"] = BASE._open_Year_Sdt.ToString(BASE._Server_Date_Format_Short);
            ROW["iTR_REC_CASH"] = Opening_Bal;
            ROW["iTR_PAY_CASH"] = 0;
            TR_Table.Rows.Add(ROW);

            // (1.4) Cash Data Sorting
            DataView DV1 = new DataView(TR_Table);
            DV1.Sort = "iTR_DATE,iTR_SORT_REC";
            DataTable XTABLE = DV1.ToTable();
            double _Temp_Balance = 0;
            double _Temp_Receipt = 0;
            double _Temp_Payment = 0;

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
                if (_Temp_Balance < 0)
                {
                    jsonParam.message = "Can't Register for Audit as Cash Balance is NEGATIVE.<br>==========================================<br>Date      : " + Convert.ToDateTime(XROW["iTR_DATE"]).ToString("dd-MMM, yyyy") + "<br>Amount : " + _Temp_Balance.ToString("#,0.##") + "<br>==========================================<br>For more details: Check Daily Balances Report <br> <br> कैश बैलेंस Negative होने से ऑडिट के लिए रजिस्टर नहीं कर सकते:<br>==========================================<br>तरीक : " + Convert.ToDateTime(XROW["iTR_DATE"]).ToString("dd-MMM, yyyy") + "<br>रकम : " + _Temp_Balance.ToString("#,0.##") + "<br>==========================================<br>कृपया Daily Balances Report में चेक करके ठीक करना जी.";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                        User_Type
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            // Centre Remarks Count 
            if ((int)BASE._Action_Items_DBOps.GetPendingCentreRemarkCount(ClientScreen.Home_StartUp) > 0)
            {
                jsonParam.message = "Some of the Actions posted by Auditors in Madhuban require your attention!!<br><br>Please click Help -> Audit Actions.<br>Select an Audit Action and Click on Centre Remarks.<br>Enter your remarks and Save, Repeat this for all audit actions which are not closed,Then Register for Audit..<br>मधुबन से कुछ Audit action पोस्ट की है जिसका आपने अभी तक जवाब नहीं दिया है.<br> कृपया Help -> Audit Actions में जाकर Centre Remark में जवाब लिखना जी. सभी खुले हुए->Audit Actions का जवाब देने पर आप ऑडिट के लिए रजिस्टर कर सकते हैं";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            // Asset Tf
            if (BASE._AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer(BASE._open_Year_Sdt, BASE._open_Year_Edt) > 0)
            {
                jsonParam.message = "Some of the Asset Transfer Entries with other centres/HQ are not matched yet !!<br>Match all the pending asset transfer entries. Then Register for Audit. <br> <br> अन्य सेंटर्स अथवा हेडक्वाटर के साथ कुछ Asset Transfer एंट्री मैच नहीं हुई हैं. सभी एंट्री मैच होने पर ही ऑडिट के लिए रजिस्टर कर सकते है. ";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            // Internal Tf
            if (BASE._Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount(BASE._open_Year_Sdt, BASE._open_Year_Edt) > 0)
            {
                jsonParam.message = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts-> Internal Transfer Register<br>Can Register for Audit if 1) All Transfers with centers 2) TDS Transfers with H.Q. 3)Non-Cash Transfers with H.Q. are matched. <br> अन्य सेंटर्स के साथ कुछ Transfer एंट्री मैच नहीं हुई हैं. कृपया Accounts -> Internal Transfer Register में देखना जी: <br> 1) अन्य सेंटर्स के साथ सभी प्रकार के Transfer 2) मुख्यालय के साथ सभी प्रकार के ट्रान्सफर(केवल Sent to Abu नकद को छोडकर) उपरोक्त सभी एंट्री मैच होने पर ही ऑडिट के लिए रजिस्टर कर सकते है. ";
                jsonParam.result = false;
                if (BASE._open_Ins_ID != "00001")
                {
                    jsonParam.message = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts->Internal Transfer Register for unmatched Entries. <br> अन्य सेंटर्स के साथ कुछ Transfer एंट्री मैच नहीं हुई हैं. कृपया Accounts -> Internal Transfer Register में देखना जी. उपरोक्त सभी एंट्री मैच होने पर ही ऑडिट के लिए रजिस्टर कर सकते है.";
                }
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            bool AllowAccountAnalysisBlocking = true;
            DataTable Cen_Task = BASE._ClientUserDBOps.GetCenterTasks();
            if (Cen_Task != null)
            {
                DataView _dataview = new DataView(Cen_Task);
                _dataview.Sort = "TASK_NAME";
                if (_dataview.Count > 0)
                {
                    int index = _dataview.Find("Bypass_Account_Analysis_Audit_Registration_Block");
                    if (index >= 0)
                    {
                        AllowAccountAnalysisBlocking = false;
                    }
                }
            }

            //Account Analysis Checks where Blocking of Audit Registration has been marked 
            if (AllowAccountAnalysisBlocking)
            {
                DataSet dSet = (DataSet)BASE._Action_Items_DBOps.GetAuditExceptions(true);
                if (dSet.Tables["MainTable"].Rows.Count > 0)
                {
                    jsonParam.message = "One of the Necessary Account Analysis is not Corrected yet!!<br>Please refer Start-> Account Analysis and check Account Analysis namely '" + dSet.Tables["MainTable"].Rows[0]["Name"].ToString() + "' <br> आपने एक आवश्यक अकाउंट एनालिसिस को ठीक नहीं किया गया है।  कृपया Start-> Account Analysis खोलें और '" + dSet.Tables["MainTable"].Rows[0]["Name"].ToString() + "' नाम का अकाउंट एनालिसिस चेक करें एवं आवश्यक करेक्शन करें।";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                        User_Type
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            string DynamicRestriction = "";
            DbOperations.SharedVariables Dbops = new DbOperations.SharedVariables(BASE);
            DynamicRestriction = Dbops.GetDynaicClientRestriction();
            if (DynamicRestriction.Length > 0)
            {
                jsonParam.message = DynamicRestriction;
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            DataSet PendingAttachments = BASE._Audit_DBOps.GetAllPendingAttachments();
            if (PendingAttachments.Tables[0].Rows.Count > 0)
            {
                string Pending_Screen = PendingAttachments.Tables[1].Rows[0]["SCREEN"].ToString();
                string Pending_Entries = PendingAttachments.Tables[1].Rows[0]["PENDING_ENTRIES"].ToString();
                Int32 Rejected_count = (Int32)PendingAttachments.Tables[1].Rows[0]["REJECT_CNT"];
                if (Rejected_count > 0)
                {
                    jsonParam.message = "There are " + Rejected_count + " Rejected Documents  in " + Pending_Screen + " Screen.. <br> Please re-attach document / mention remarks in entries against which red flag is shown in " + Pending_Screen + " Screen<br> " + Pending_Screen + " स्क्रीन में " + Rejected_count + " डॉक्यूमेंट हैं जिन्हें ऑडिटर ने रिजेक्ट किया है. <br> कृपया " + Pending_Screen + " में जहां <img src='https://connectone.app/content/images/Rejected_Attachment.png'/>(Red फ्लैग) मार्क हो, वहां बताये गए डोक्युमेन्ट्स दुबारा अपलोड करना जी / कारण लिखना जी उसके बाद ऑडिट के लिए रजिस्टर कर सकते हैं.";
                }
                else
                {
                    jsonParam.message = "There are " + Pending_Entries + " Entries in " + Pending_Screen + " Screen where neither Required document is attached, nor Reason for not attaching Document is mentioned..<br>Please open " + Pending_Screen + " screen and upload required documents or mention the reason for not uploading them where ever <img src='https://connectone.app/content/images/attach_red_2.png'/>(Red Shield) or <img src='https://connectone.app/content/images/attach_yellow_2.png'/>(Yellow Shield) symbols are shown.  <br> " + Pending_Screen + " स्क्रीन में " + Pending_Entries + " एंट्रीज़ हैं जिसमें ना तो डोक्युमेन्ट्स अपलोड किये हैं ना ही कारण लिखा गया है. <br> कृपया " + Pending_Screen + " में जहां <img src='https://connectone.app/content/images/attach_red_2.png'/>(Red Shield) मार्क या <img src='https://connectone.app/content/images/attach_yellow_2.png'/>(Yellow Shield) मार्क है वहां बताये गए डोक्युमेन्ट्स अपलोड करना जी / कारण लिखना जी उसके बाद ऑडिट के लिए रजिस्टर कर सकते हैं.";
                }
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            jsonParam.result = true;
            return Json(new
            {
                jsonParam,
                User_Type
            }, JsonRequestBehavior.AllowGet);
        }
    }
}