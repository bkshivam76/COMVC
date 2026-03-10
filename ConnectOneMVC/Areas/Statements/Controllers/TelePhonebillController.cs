using Common_Lib;
using ConnectOneMVC.Areas.Statements.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOne.D0010._001;
namespace ConnectOneMVC.Areas.Statements.Controllers
{
    public class TelePhonebillController : BaseController
    {
        // GET: Statements/TelePhone        
        public ActionResult Dialog_Telephone_Bill()
        {
            TelePhonebill model = new TelePhonebill();
            model.PeriodFrom = BASE._open_Year_Sdt;
            model.PeriodTo = BASE._open_Year_Edt;
            ViewData["IsMobile"] = isMobileBrowser();
            return View(model);
        }
        #region "Start--> LookupEdit Events"

        public ActionResult LookUp_GetTelephonebillList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var Telephonebilllist = BASE._telephoneDBOps.GetListByCondition(Common_Lib.RealTimeService.ClientScreen.Profile_Telephone, "") ;
           // var bankdata = DatatableToModel.DataTabletoTelePhoneInfo_INFO(Telephonebilllist);
           // bankdata = bankdata != null ? bankdata : new List<TelePhonebillinfo>();
            Telephonebilllist.Sort((x, y) => x.TP_NO.CompareTo(y.TP_NO));
            Telephonebilllist.Insert(0, new DbOperations.Return_Payment_Telephone_Select { TP_NO = "--All Tel. No(s)---", TP_ID = "--ALL--" });

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Telephonebilllist, loadOptions)), "application/json");
        }
        #endregion "end--> LookupEdit Events"

        public ActionResult TelephoneStatement(string _FrDate, string _ToDate, string TelephoneID)
        {
            ViewData["IsMobile"] = isMobileBrowser();
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            return View(new ConnectOne.D0010._001.Report_Telephone_Bill(frDate, toDate,"", TelephoneID,BASE));
        }
        private bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = System.Web.HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "android","midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}