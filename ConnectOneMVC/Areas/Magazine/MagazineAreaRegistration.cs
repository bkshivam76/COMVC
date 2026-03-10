using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Magazine
{
    public class MagazineAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Magazine";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Magazine_default",
                "Magazine/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}