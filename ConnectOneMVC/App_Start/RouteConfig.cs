using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ConnectOneMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            string[] defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"].Split('/');
            string sparc_cenid = System.Configuration.ConfigurationManager.AppSettings["SpARC_Project_Cen_ID"].ToString();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
    name: "sparc",
    url: "sparc/{CenID}",
    defaults: new { controller = "Login", action = "Frm_Generic_Login", CenID = sparc_cenid, InsID= "00010" }).DataTokens["area"] = "Start"; 
    //namespaces: new[] { "ConnectOneMVC.Start.Controllers" });


            routes.MapRoute(
                name: "KarunaSankalpChart",
                url: "KarunaSankalpChart/{AB_ID}/{ChartSrNo}/{Response}/{ParticipantName}",
                new
                {
                    controller = "ServiceChart",
                    action = "KarunaSankalpChart_SubmitResponse",
                    AB_ID = UrlParameter.Optional,
                    ChartSrNo = UrlParameter.Optional,
                    Response = UrlParameter.Optional,
                    ParticipantName = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "MainDefault", // Route name
                url: "{controller}/{action}/{id}", // URL with parameters
                defaults: new { controller = "Pages", action = "Home", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
            name: "Default", // Route name     
            url: "{controller}/{action}/{id}", // URL with parameters      
            defaults: new { controller = defaultpath[2], action = defaultpath[3], id = UrlParameter.Optional } // Parameter defaults   
            ).DataTokens.Add("area", "Start");
            routes.MapRoute(
                name: "ViewFile", // Route name
                url: "viewfiles", // URL with parameters
                defaults: new { controller = "ViewFolder", action = "ViewFiles", id = UrlParameter.Optional } // Parameter defaults
                ).DataTokens.Add("area", "Help");
        }
    }
}