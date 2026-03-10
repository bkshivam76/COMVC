using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Start
{
    public class StartAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Start";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Start_default",
                "Start/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}