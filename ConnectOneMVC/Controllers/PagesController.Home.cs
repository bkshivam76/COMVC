using System.Web.Mvc;

namespace ConnectOneMVC.Controllers
{
    public partial class PagesController : Controller
    {
        public ActionResult Home()
        {
            string defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"];
            return RedirectToAction(defaultpath.Split('/')[3], defaultpath.Split('/')[2], new { area = defaultpath.Split('/')[1] });
        }
    }
}