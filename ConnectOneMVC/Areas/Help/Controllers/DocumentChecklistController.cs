using ConnectOneMVC.Controllers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations.SharedVariables;

namespace ConnectOneMVC.Areas.Help.Controllers
{
    public class DocumentChecklistController : BaseController
    {
        // GET: Help/DocumentChecklist
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LookUp_Get_Item(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetAllItems> data = BASE._CoreDBOps.GetAllItemsMappedToDocuments();
          
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Document(DataSourceLoadOptions loadOptions,string ID)
        {
            List<Return_GetItemDocuments> data=BASE._CoreDBOps.GetItemDocuments(ID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
    }
}