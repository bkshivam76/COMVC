using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using Common_Lib.RealTimeService;


namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class MagazineRequestController : BaseController
    {
        // GET: Facility/MagazineRequest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Magazine_Request_Window(string ActionMethod = null)
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_Magazine_Request, "List"))
            {                
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights')$('#Facility_Magazine_Request').hide();</script>");//Code written for User Authorization do not remove                   
            }
        }
    }
}