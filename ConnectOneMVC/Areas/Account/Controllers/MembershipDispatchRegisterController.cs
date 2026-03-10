using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class MembershipDispatchRegisterController : BaseController
    {
        // GET: Account/MembershipDispatchRegister
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Frm_Membership_Dispatch_Register_Info(string ActionMethod = null)
        {
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.Title = "Magazine Dispatch Register";
            var searchOptions = new List<string>
            {
                "DISPATCH MEMBER ID",
                "MEMBER ID.",
                "MEMBER OLD NO.",
                "MAGAZINE ISSUE DATE",
                "ALL RECORDS"
            };
            ViewBag.SearchOptions = searchOptions;
            List<ConnectOneMVC.Areas.Account.Models.Magazine> listMagazine = DatatableToModel.DataTabletoMagazineLookUp(BASE._Magazine_DBOps.GetList("", "", ""));
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Magazine_Dispatch_Register).ToString()) ? 1 : 0;
            return View();
        }        
        public ActionResult Frm_Magazine_Dispatch_Register_Info_Grid_dx(string ActionMethod = null)
        {
            //MM_DS = Base._Magazine_DBOps.GetList_MagazineDispatchRegister(Membership_ID, Membership_Old_ID, Issue_Date, Magazine, Disp_Membership_ID, IIf(DtCutOff.Text.Length > 0, CutOff, Nothing))
            return null;
        }
    }
}