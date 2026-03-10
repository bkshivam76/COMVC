using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class StudentListController : BaseController
    {
        // GET: Profile/StudentList

        public List<DbOperations.Students.Return_StudentList> StudentList_ExportData
        {
            get
            {
                return (List<DbOperations.Students.Return_StudentList>)GetBaseSession("StudentList_ExportData_StudentList");
            }
            set
            {
                SetBaseSession("StudentList_ExportData_StudentList", value);
            }
        }
        public ActionResult Frm_Students_Info()
        {
            StudentList_user_rights();
            if (!CheckRights(BASE,ClientScreen.Profile_Students,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_Students').hide();</script>");
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_Students).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            List<Common_Lib.DbOperations.Students.Return_StudentList> Student_List = BASE._Students_DBOps.GetListOfStudents();
            if ((Student_List.Count==0))
            {
                return View();
            }
            else
            {
                StudentList_ExportData = Student_List;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(Student_List);
            }
        }

        public PartialViewResult Frm_Students_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            StudentList_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (StudentList_ExportData == null || command == "REFRESH")
            {
                List<Common_Lib.DbOperations.Students.Return_StudentList> Student_List = BASE._Students_DBOps.GetListOfStudents();
                StudentList_ExportData = Student_List;
            }
            return PartialView("Frm_Students_Info_Grid", StudentList_ExportData);
        }
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_Students, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Students_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_StudentList");
        }
        public void StudentList_user_rights()
        {
            ViewData["StudentList_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Students, "Add");
            ViewData["StudentList_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_Students, "Update");
            ViewData["StudentList_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_Students, "View");
            ViewData["StudentList_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_Students, "Delete");
            ViewData["StudentList_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_Students, "Export");
        }
    }
}