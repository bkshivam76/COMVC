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
    public class CashBookController : BaseController
    {
        // GET: Statements/CashBook
        public ActionResult Statement()
        {
            TempData["BASE"] = BASE;
            TempData["_FrDate"] = BASE._open_Year_Sdt;
            TempData["_ToDate"] = BASE._open_Year_Edt;
            return View();
        }
    }
}