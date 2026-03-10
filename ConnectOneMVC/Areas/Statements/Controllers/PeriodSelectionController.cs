using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace ConnectOneMVC.Areas.Statements.Controllers
{
    public class PeriodSelectionController : BaseController
    {
        public JsonResult Check_Period_Selection(string _FrDate, string _ToDate)
        {
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate ;
            DateTime toDate ;
            String Msg = "";

            if ((DateTime.TryParse(_FrDate, out frDate) == false))
            {
                Msg = "Date Incorrect / Blank...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            if ((DateTime.TryParse(_ToDate, out toDate) == false))
            {
                Msg = "Date Incorrect / Blank...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            double diff = toDate.Subtract(frDate).TotalDays;
                if ((diff < 0))
                {
                    Msg = "From Date cannot be Higher Than To Date...!";
                    return Json(new
                    {
                        message = Msg,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
            }

            diff = frDate.Subtract(BASE._open_Year_Sdt).TotalDays;
            if ((diff < 0))
            {
                Msg = "From Date not as per Financial Year...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

                diff = toDate.Subtract(BASE._open_Year_Edt).TotalDays;
            if ((diff > 0))
            {
                Msg = "To Date not as per Financial Year...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = Msg,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}