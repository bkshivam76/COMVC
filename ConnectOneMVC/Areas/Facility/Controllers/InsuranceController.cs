using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class InsuranceController : BaseController
    {
        // GET: Facility/Insurance
        public ActionResult InsuranceCalculator()
        {            
            return View();
        }
    }
}