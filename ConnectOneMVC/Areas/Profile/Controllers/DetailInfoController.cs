using ConnectOneMVC.Controllers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.Audit;
using static Common_Lib.DbOperations.Center;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class DetailInfoController : BaseController
    {
        [CheckLogin]
        public ActionResult ContactInformation()
        {
            ViewBag.Padno = BASE._open_PAD_No;
            return View();
        }
        public ActionResult DetailInformation()
        {
            return new EmptyResult();
            //return View();
        }

        public ActionResult LookUp_Get_Details(DataSourceLoadOptions loadOptions, int padno = 0)
        {
            if (padno == 0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_Get_Contact_Info>(), loadOptions)), "application/json");
            }
            List<Return_Get_Contact_Info> data = BASE._CenterDBOps.Get_Contact_Info(padno);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        public ActionResult CheckAuditStatusPage()
        {            
            return View();
        }

        public ActionResult LookUp_Get_AuditStatus(DataSourceLoadOptions loadOptions, int CertificateNo = 0)
        {           
            if (CertificateNo == 0)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_AuditStatusPage_Public>(), loadOptions)), "application/json");
            }
            List<Return_AuditStatusPage_Public> data = BASE._Audit_DBOps.AuditStatusPage_Public(CertificateNo);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
    }
}