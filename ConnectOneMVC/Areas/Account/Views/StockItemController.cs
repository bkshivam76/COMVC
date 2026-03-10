using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Views
{
    public class StockItemController : Controller
    {
        // GET: Account/StockItem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult frm_Stock_Item_New()
        {
            return View();
        }
        public ActionResult frm_Stock_Item_Close()
        {
            return View();
        }
    }
}