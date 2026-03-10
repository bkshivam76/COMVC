using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Areas.Help.Models;
//using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Utils.Extensions;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations.Attachments;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class ServiceReportController : BaseController
    {
        public List<ServiceReport> ServiceReport_ExportData
        {
            get { return (List<ServiceReport>)GetBaseSession("ServiceReport_ExportData_SerRpt");}
            set { SetBaseSession("ServiceReport_ExportData_SerRpt", value); }
        }
        public List<ProgramOrganiserNames> OrganiserNamesList
        {
            get { return (List<ProgramOrganiserNames>)GetBaseSession("OrganiserNamesList_SerRpt"); }
            set { SetBaseSession("OrganiserNamesList_SerRpt", value); }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> ServiceReportInfo_DetailGrid_Data
        {
            get { return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("ServiceReportInfo_DetailGrid_Data_SerRpt"); }
            set { SetBaseSession("ServiceReportInfo_DetailGrid_Data_SerRpt", value);}
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> ServiceReportInfo_AdditionalInfoGrid
        {
            get { return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("ServiceReportInfo_AdditionalInfoGrid_SerRpt"); }
            set { SetBaseSession("ServiceReportInfo_AdditionalInfoGrid_SerRpt", value); }
        }

        public DataTable DT_Draft
        {
            get { return (DataTable)GetBaseSession("DT_Draft"); }
            set { SetBaseSession("DT_Draft", value); }
        }
        public List<TreePlantationDetails> TreePlantationData 
        {
            get { return (List<TreePlantationDetails>)GetBaseSession("TreePlantationData_ServiceRpt_Window"); }
            set { SetBaseSession("TreePlantationData_ServiceRpt_Window", value); }
        }  
        #region Listing
        [CheckLogin]
        public ActionResult Frm_Service_Report_Info()
        {
            ServiceReport_UserRights();
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            if (CheckRights(BASE, ClientScreen.Facility_ServiceReport, "List"))
            {
                //ViewBag.ShowHorizontalBar = 0;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;

                Grid_Display();
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ServiceReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                    || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View(ServiceReport_ExportData);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
            }
        }
        public void Grid_Display()
        {
            DataTable Misc_Table = BASE._SR_DBOps.GetProjects("MISC_NAME", "ID");
            DataTable SR_Table = BASE._SR_DBOps.GetList();

            var BuildData = from SR in SR_Table.AsEnumerable()
                            join Misc in Misc_Table.AsEnumerable()
                            on SR["Project ID"] equals Misc["ID"]
                            select new ServiceReport
                            {
                                MISC_NAME = Misc.Field<string>("MISC_NAME"),
                                SR_PROG_NAME = SR.Field<string>("SR_PROG_NAME"),
                                SR_PROG_VENUE = SR.Field<string>("SR_PROG_VENUE"),
                                SR_From_Date = SR.Field<DateTime?>("SR_PROG_FR_DATE"),
                                SR_To_Date = SR.Field<DateTime?>("SR_PROG_TO_DATE"),
                                SR_DATE = SR.Field<string>("SR_DATE"),
                                SR_PERIOD = SR.Field<Int32>("SR_PERIOD"),
                                SR_TIME = SR.Field<string>("SR_TIME"),
                                SR_TIME_PER = SR.Field<string>("SR_TIME_PER"),
                                SR_BRIEF = SR.Field<string>("SR_BRIEF"),
                                SR_SPEAKER = SR.Field<string>("SR_SPEAKER"),
                                SR_SPL = SR.Field<string>("SR_SPL"),
                                SR_BENEFIT = SR.Field<Int32>("SR_BENEFIT"),
                                SR_FOLLOW = SR.Field<string>("SR_FOLLOW"),
                                SR_FEEDBACK = SR.Field<string>("SR_FEEDBACK"),
                                SR_NewsLink = SR.Field<string>("SR_PROG_NEWS_LINK"),
                                SR_Prog_Category = SR.Field<string>("SR_PROG_CATEGORY"),
                                SR_VVIP_Testimonial = SR.Field<string>("SR_PROG_VVIP_TESTIMONIAL"),
                                SR_Cultural = SR.Field<string>("SR_PROG_CULTURAL"),
                                Add_By = SR.Field<string>("Add By"),
                                Add_Date = SR.Field<DateTime>("Add Date"),
                                Edit_By = SR.Field<string>("Edit By"),
                                Edit_Date = SR.Field<DateTime>("Edit Date"),
                                Action_Status = SR.Field<string>("Action Status"),
                                Action_By = SR.Field<string>("Action By"),
                                Action_Date = SR.Field<DateTime>("Action Date"),
                                ID = SR.Field<string>("ID"),
                                REQ_ATTACH_COUNT = Convert.IsDBNull(SR["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(SR["REQ_ATTACH_COUNT"]),
                                COMPLETE_ATTACH_COUNT = Convert.IsDBNull(SR["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(SR["COMPLETE_ATTACH_COUNT"]),
                                RESPONDED_COUNT = Convert.IsDBNull(SR["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(SR["RESPONDED_COUNT"]),
                                REJECTED_COUNT = Convert.IsDBNull(SR["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(SR["REJECTED_COUNT"]),
                                OTHER_ATTACH_CNT = Convert.IsDBNull(SR["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(SR["OTHER_ATTACH_CNT"]),
                                ALL_ATTACH_CNT = Convert.IsDBNull(SR["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(SR["ALL_ATTACH_CNT"]),
                                VOUCHING_PENDING_COUNT = SR.Field<Int32?>("VOUCHING_PENDING_COUNT"),
                                VOUCHING_ACCEPTED_COUNT = SR.Field<Int32?>("VOUCHING_ACCEPTED_COUNT"),
                                VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = SR.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"),
                                VOUCHING_REJECTED_COUNT = SR.Field<Int32?>("VOUCHING_REJECTED_COUNT"),
                                VOUCHING_TOTAL_COUNT = SR.Field<Int32?>("VOUCHING_TOTAL_COUNT"),
                                AUDIT_PENDING_COUNT = SR.Field<Int32?>("AUDIT_PENDING_COUNT"),
                                AUDIT_ACCEPTED_COUNT = SR.Field<Int32?>("AUDIT_ACCEPTED_COUNT"),
                                AUDIT_ACCEPTED_WITH_REMARKS_COUNT = SR.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"),
                                AUDIT_REJECTED_COUNT = SR.Field<Int32?>("AUDIT_REJECTED_COUNT"),
                                AUDIT_TOTAL_COUNT = SR.Field<Int32?>("AUDIT_TOTAL_COUNT"),
                                IS_AUTOVOUCHING = SR.Field<Int32?>("IS_AUTOVOUCHING"),
                                IS_CORRECTED_ENTRY = SR.Field<Int32?>("IS_CORRECTED_ENTRY"),
                                SR_OrganizedFor= SR.Field<string>("OrganizedFor"),
                                SR_PRogCoordinator1 = SR.Field<string>("Coordinator 1"),
                                SR_PRogCoordinator2 = SR.Field<string>("Coordinator 2"),
                                SR_ParticipantCategory = SR.Field<string>("ParticipantCategory"),
                                SR_ProgramType = SR.Field<string>("AddlProgramType"),
                                SR_LocationType = SR.Field<string>("LocationType"),
                                SR_ParticipantSubCategory = SR.Field<string>("ParticipantSubCategory"),
                                ReportLink = SR.Field<string>("REPORT_LINK"),
                                FORM_INST_ID= SR.Field<Int32?>("FORM_INST_ID"),
                                SR_CHILD_BENEFIT = SR.Field<Int32?>("SR_CHILD_BENEFIT"),
                                SR_YOUTH_BENEFIT = SR.Field<Int32?>("SR_YOUTH_BENEFIT"),
                                SR_MALE_BENEFIT = SR.Field<Int32?>("SR_MALE_BENEFIT"),
                                SR_FEMALE_BENEFIT = SR.Field<Int32?>("SR_FEMALE_BENEFIT"),
                                SR_WOMEN_BENEFIT = SR.Field<Int32?>("SR_WOMEN_BENEFIT"),
                                SR_SENIOR_CITIZEN_BENEFIT = SR.Field<Int32?>("SR_SENIOR_CITIZEN_BENEFIT"),
                                VenueTypes= SR.Field<string>("VenueTypes"),
                                Reporter_Mobile = SR.Field<string>("REPORTER_MOBILE"),
                                Reporter_Email = SR.Field<string>("REPORTER_EMAIL")
                            };
            var Final_Data = BuildData.ToList();
            var count = Final_Data.Count;
            for (int i = 0; i < count; i++)
            {
                if (Final_Data[i].IS_CORRECTED_ENTRY > 0)
                {
                    Final_Data[i].iIcon += "CorrectedEntry|";
                }
                if (Final_Data[i].IS_AUTOVOUCHING > 0)
                {
                    Final_Data[i].iIcon += "AutoVouching|";
                }
                if ((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) == 0 && (Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0))
                {
                    Final_Data[i].iIcon += "RedShield|";
                }
                else if (((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) >= (Final_Data[i].REQ_ATTACH_COUNT ?? 0)) && ((Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0) && ((Final_Data[i].RESPONDED_COUNT ?? 0) == 0)))
                {
                    Final_Data[i].iIcon += "GreenShield|";
                }
                else if ((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) > 0 && (((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) < (Final_Data[i].REQ_ATTACH_COUNT ?? 0))))
                {
                    Final_Data[i].iIcon += "YellowShield|";
                }
                else if (((((Final_Data[i].COMPLETE_ATTACH_COUNT ?? 0) + (Final_Data[i].RESPONDED_COUNT ?? 0)) >= (Final_Data[i].REQ_ATTACH_COUNT ?? 0)) && ((Final_Data[i].REQ_ATTACH_COUNT ?? 0) > 0) && ((Final_Data[i].RESPONDED_COUNT ?? 0) > 0)))
                {
                    Final_Data[i].iIcon += "BlueShield|";
                }
                if (((Final_Data[i].REJECTED_COUNT ?? 0) > 0))
                {
                    Final_Data[i].iIcon += "RedFlag|";
                }
                if ((((Final_Data[i].ALL_ATTACH_CNT ?? 0) > 0) && (Final_Data[i].OTHER_ATTACH_CNT ?? 0) == 0))
                {
                    Final_Data[i].iIcon += "RequiredAttachment|";
                }
                else if ((((Final_Data[i].ALL_ATTACH_CNT ?? 0) > 0) && (Final_Data[i].OTHER_ATTACH_CNT ?? 0) != 0))
                {
                    Final_Data[i].iIcon += "AdditionalAttachment|";
                }
                if (Final_Data[i].VOUCHING_TOTAL_COUNT == Final_Data[i].VOUCHING_ACCEPTED_COUNT && Final_Data[i].VOUCHING_ACCEPTED_WITH_REMARKS_COUNT == 0 && Final_Data[i].VOUCHING_ACCEPTED_COUNT > 0) { Final_Data[i].iIcon += "VouchingAccepted|"; }
                if (Final_Data[i].VOUCHING_REJECTED_COUNT > 0) { Final_Data[i].iIcon += "VouchingReject|"; }
                if (Final_Data[i].VOUCHING_TOTAL_COUNT == Final_Data[i].VOUCHING_ACCEPTED_COUNT && Final_Data[i].VOUCHING_ACCEPTED_WITH_REMARKS_COUNT > 0) { Final_Data[i].iIcon += "VouchingAcceptWithRemarks|"; }
                if (Final_Data[i].VOUCHING_PENDING_COUNT > 0 && (Final_Data[i].VOUCHING_ACCEPTED_COUNT > 0 || Final_Data[i].VOUCHING_REJECTED_COUNT > 0)) { Final_Data[i].iIcon += "VouchingPartial|"; }
                if (Final_Data[i].AUDIT_TOTAL_COUNT == Final_Data[i].AUDIT_ACCEPTED_COUNT && Final_Data[i].AUDIT_ACCEPTED_WITH_REMARKS_COUNT == 0 && Final_Data[i].AUDIT_ACCEPTED_COUNT > 0) { Final_Data[i].iIcon += "AuditAccepted|"; }
                if (Final_Data[i].AUDIT_REJECTED_COUNT > 0) { Final_Data[i].iIcon += "AuditReject|"; }
                if (Final_Data[i].AUDIT_TOTAL_COUNT == Final_Data[i].AUDIT_ACCEPTED_COUNT && Final_Data[i].AUDIT_ACCEPTED_WITH_REMARKS_COUNT > 0) { Final_Data[i].iIcon += "AuditAcceptWithRemarks|"; }
                if (Final_Data[i].AUDIT_PENDING_COUNT > 0 && (Final_Data[i].AUDIT_ACCEPTED_COUNT > 0 || Final_Data[i].AUDIT_REJECTED_COUNT > 0)) { Final_Data[i].iIcon += "AuditPartial|"; }
            }
            ServiceReport_ExportData = Final_Data;
        }
        [HttpPost]
        public PartialViewResult Frm_Service_Report_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ServiceReport_UserRights();
            if (command == "REFRESH" || ServiceReport_ExportData == null)
            {
                Grid_Display();
            }
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            return PartialView(ServiceReport_ExportData);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public ActionResult Frm_Service_Report_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.ServiceReportInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ServiceReportInfo_RecID = RecID;
            ViewBag.ServiceReportInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_ServiceReport);
                    ServiceReportInfo_DetailGrid_Data = _docList;
                    Session["ServiceReportInfo_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Facility_ServiceReport);
                    ServiceReportInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["ServiceReportInfo_detailGrid_Data"] = data.DocumentMapping;
                    ServiceReportInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(ServiceReportInfo_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(ServiceReportInfo_AdditionalInfoGrid);
        }
        public ActionResult Frm_Export_Options()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceReport, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            }
            return PartialView();
        }  
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "ServiceReportListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            //settings.Columns.Add("Item_Name");
            //settings.Columns.Add("Document_Name");
            //settings.Columns.Add("Reason");
            //settings.Columns.Add("FromDate");
            //settings.Columns.Add("ToDate");
            //settings.Columns.Add("Description");
            //settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "ServiceReportListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["ServiceReportInfo_DetailGrid_Data"];
        }
        #endregion
        public ActionResult LookUp_Get_ServiceReport_Institutes(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._SR_DBOps.GetInstitutes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Institutes(d1), loadOptions)), "application/json");
        }
        [OutputCache(NoStore = true, Duration = 0)]
        [HttpGet]
        public ActionResult Frm_Service_Report_Window(string ActionMethod = "_New", string xID = "", string Info_LastEditedOn = null, string PopupName = "Dynamic_Content_popup", string GridToRefresh = "ServiceReportListGrid", string CenID = null, string ReporterEmail = null, string ReporterMobile = null, string SessionGUID = null, string FY = "",string sender="",string cen_name="", string insid = "",string projId="",string wingId="")
        {
            if (ActionMethod != "_New" && string.IsNullOrWhiteSpace(BASE._open_User_ID)) 
            {
                string defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"];
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", defaultpath.Split('/')[1]);
                redirectTargetDictionary.Add("action", defaultpath.Split('/')[3]);
                redirectTargetDictionary.Add("controller", defaultpath.Split('/')[2]);
               return new RedirectToRouteResult(redirectTargetDictionary);
            }        
            if (string.IsNullOrWhiteSpace(CenID) == false)
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == MyGuid);
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = MyGuid,
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                }           
                SessionGUID = MyGuid.ToString();                
                BASE._open_Cen_ID = Convert.ToInt32(CenID);
                BASE._open_User_ID = CenID;
                BASE._open_Year_ID = Convert.ToInt32(FY);
                //BASE._open_Year_ID = BASE._CenterDBOps.GetCentreMaxOpenYear(CenID);           
                //if (BASE._open_Year_ID == 0) 
                //{
                //    BASE._open_Year_ID = DateTime.Today.GetFinancialYear();
                //}
                BASE._open_Cen_Name = cen_name;
                BASE._open_Ins_ID = insid;
            }
            ViewBag.cen_name = BASE._open_Cen_Name;
            ViewBag.projId = projId;
            ViewBag.wingId = wingId;
            TreePlantationData = new List<TreePlantationDetails>();
            ServiceReport_ViewModel model = new ServiceReport_ViewModel();
            model.attachment_Window = new Model_Attachment_Window();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            model.ActionMethod = model.Tag.ToString();
            model.TitleX_GSR = "Godly Service Report";
            model.xID_GSR = xID ?? "";
            model.PopupName_GSR = PopupName != null ? PopupName : "Dynamic_Content_popup";
            model.Institutes_GSR = DatatableToModel.DataTableTo_Service_Report_Institutes(BASE._SR_DBOps.GetInstitutes());
            model.MasterProjectNameList = LookUp_Get_MasterProjectList();
            model.ProjectNameList = LookUp_Get_ProjectList(sender);
            model.WingListData = Get_WingsList();
            model.DocumentNameID = GetDocumentNameID();
            model.ProgType_Data = GetProgramType();
            model.ProgOccasion_Data = GetProgramOccasion();            
            model.ReporterCenID = CenID;
            model.ReporterEmail = ReporterEmail;
            model.ReporterMobile = ReporterMobile;
            model.Sender = sender;
            model.Txt_NoOfEvents_GSR = 1;
            model.Txt_Organiser_GSR = BASE._open_Cen_Name;
            model.organiserNames_GSR = GetOrganiserNamesList();
            model.VenueTypeData = GetProgramVenueTypes();
            if (string.IsNullOrWhiteSpace(CenID) == false)
            {
                if (model.MasterProjectNameList.Count > 0)
                {
                    if (string.Equals(model.Sender, "amritmahotsav", StringComparison.OrdinalIgnoreCase))
                    {
                        var obj = model.MasterProjectNameList.Find(x => x.Name == "Azadi Ka Amrit Mahotsav");
                        if (obj != null)
                        {
                            model.Look_MasterProject_GSR = obj.ID;
                        }
                    }
                    else
                    {
                        var obj = model.MasterProjectNameList.Find(x => x.Name == "General");
                        if (obj != null)
                        {
                            model.Look_MasterProject_GSR = obj.ID;
                        }
                    }
                }                
                if (model.Tag == Common.Navigation_Mode._New) 
                {
                    if (string.IsNullOrWhiteSpace(model.Sender) == false) 
                    {
                        int count=model.WingListData.Count;
                        int i;
                        for (i=0; i < count; i++) 
                        {
                            if (string.Equals(model.WingListData[i].Name.Trim().Replace(" ", ""), model.Sender.Trim().Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) 
                            {
                                model.PreSelectedWing = model.WingListData[i].ID;
                                model.Chk_WingBasedFlag_GSR = "1";
                                break;
                            }
                        }
                    }
                    DataTable d1 = BASE._SR_DBOps.GetCenterDetailsForCenID(Convert.ToInt32(CenID));
                    StringBuilder address = new StringBuilder("");
                    if (d1 != null && d1.Rows.Count > 0)
                    {
                        string[] fields = { "CEN_B_NAME", "CEN_ADD1", "CEN_ADD2", "CEN_ADD3", "CEN_ADD4", "CEN_CITY", "CEN_PINCODE" };
                        foreach (string field in fields)
                        {
                            if (d1.Rows[0][field] != System.DBNull.Value && string.IsNullOrWhiteSpace(d1.Rows[0][field].ToString()) == false)
                            {
                                address.Append(Convert.ToString(d1.Rows[0][field]));
                                if (field != "CEN_PINCODE")
                                {
                                    address.Append(", ");
                                }
                            }
                        }
                        model.Txt_ProgVenue_GSR = address.ToString();
                    }
                }
                if (string.IsNullOrWhiteSpace(projId) == false) 
                {
                    model.Look_ProjList_GSR = projId;
                }
                if (string.IsNullOrWhiteSpace(wingId) == false)
                {
                    if (string.IsNullOrWhiteSpace(model.PreSelectedWing))
                    {
                        model.PreSelectedWing = wingId;
                    }
                    else 
                    {
                        model.PreSelectedWing = model.PreSelectedWing + "|" + wingId;
                    }
                    model.Chk_WingBasedFlag_GSR = "1";
                }
            }
            if (model.Tag == Common.Navigation_Mode._New)
            {
                model.xID_GSR = Guid.NewGuid().ToString();
                if (BASE._open_Ins_ID == "00010")
                {
                    model.Is_InstituteName_GodlyService_GSR = true;
                }
                else
                {
                    model.Is_InstituteName_GodlyService_GSR = false;
                    model.Institute_GSR = BASE._open_Ins_ID;
                }
                //Set Master Project to default value as General. Because it will be hidden from User
                if (string.IsNullOrWhiteSpace(CenID))//Request is coming from loggedIn User instead of openurl. For openurl Look_MasterProject_GSR is set at if (string.IsNullOrWhiteSpace(CenID)==false)
                {
                    var obj = model.MasterProjectNameList.Find(x => x.Name == "General");
                    if (obj != null)
                    {
                        model.Look_MasterProject_GSR = obj.ID;
                    }
                    StringBuilder address = new StringBuilder("");
                    DataTable d1 = BASE._CoreDBOps.GetCenterDetails();
                    if (d1 != null && d1.Rows.Count > 0)
                    {
                        string[] fields = { "CEN_B_NAME", "CEN_ADD1", "CEN_ADD2", "CEN_ADD3", "CEN_ADD4", "CEN_CITY", "CEN_PINCODE" };
                        foreach (string field in fields)
                        {
                            if (d1.Rows[0][field] != System.DBNull.Value && string.IsNullOrWhiteSpace(d1.Rows[0][field].ToString())==false)
                            {
                                address.Append(Convert.ToString(d1.Rows[0][field]));
                                if (field != "CEN_PINCODE")
                                {
                                    address.Append(", ");
                                }
                            }
                        }
                        model.Txt_ProgVenue_GSR = address.ToString();
                    }
                }
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d1 = BASE._SR_DBOps.GetRecord(model.xID_GSR);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    string message = Messages.RecordChanged("Current Service Report");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Changed / Removed in Background!!','" + GridToRefresh + "');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn),Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))==false)
                        {
                            string message = Messages.RecordChanged("Current Service Report");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Look_ProjList_GSR = d1.Rows[0]["SR_PROJ_ID"].ToString();
                model.Look_MasterProject_GSR = d1.Rows[0]["SR_MASTER_PROJ_ID"].ToString();
                model.Chk_WingBasedFlag_GSR = d1.Rows[0]["SR_PROG_TYPE"].ToString();

                model.Txt_ProgName_GSR = d1.Rows[0]["SR_PROG_NAME"].ToString();
                model.Txt_ProgVenue_GSR = d1.Rows[0]["SR_PROG_VENUE"].ToString();
                model.Txt_Organiser_GSR = d1.Rows[0]["SR_PROG_ORGANISER"].ToString();               

                model.DE_FR_DT_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_DATE"]);
                model.DE_TO_DT_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_DATE"]);

                model.Txt_Fr_Time_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_TIME"]);
                model.Txt_To_Time_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_TIME"]);
                model.Txt_TotalProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_PROG_BENEFIT"]);

                if (Convert.IsDBNull(d1.Rows[0]["SR_CHILD_BENEFIT"])==false)
                {             
                    model.Txt_ChildBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_CHILD_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_YOUTH_BENEFIT"]) == false)
                {
                    model.Txt_YouthBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_YOUTH_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_MALE_BENEFIT"]) == false)
                {
                    model.Txt_MaleBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_MALE_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_FEMALE_BENEFIT"]) == false)
                {
                    model.Txt_FemaleBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_FEMALE_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_WOMEN_BENEFIT"]) == false)
                {
                    model.Txt_WomenBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_WOMEN_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_SENIOR_CITIZEN_BENEFIT"]) == false)
                {
                    model.Txt_SrCitizenBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_SENIOR_CITIZEN_BENEFIT"]);
                }

                if (BASE._open_Ins_ID == "00010")
                {
                    model.Is_InstituteName_GodlyService_GSR = true;
                    model.Institute_GSR = d1.Rows[0]["SR_INS_ID"].ToString();
                }
                else
                {
                    model.Is_InstituteName_GodlyService_GSR = false;
                    model.Institute_GSR = BASE._open_Ins_ID;
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_ONLINE_PROG_BENEFIT"]))
                {
                    model.Txt_OnlineProgBeneficiaries_GSR = 0;
                }
                else
                {
                    model.Txt_OnlineProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_ONLINE_PROG_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_OFFLINE_PROG_BENEFIT"]))
                {
                    model.Txt_ProgBeneficiaries_GSR = 0;
                }
                else
                {
                    model.Txt_ProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_OFFLINE_PROG_BENEFIT"]);
                }              
                if (model.Txt_ProgBeneficiaries_GSR == 0 && model.Txt_OnlineProgBeneficiaries_GSR == 0) 
                {
                    model.Txt_ProgBeneficiaries_GSR = model.Txt_TotalProgBeneficiaries_GSR;
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_NO_OF_EVENT"]))
                {
                    model.Txt_NoOfEvents_GSR = 1;
                }
                else
                {
                    model.Txt_NoOfEvents_GSR = Convert.ToInt32(d1.Rows[0]["SR_NO_OF_EVENT"]);
                }
                    model.Txt_ProgBrief_GSR = d1.Rows[0]["SR_PROG_BRIEF"].ToString() ?? "";
                model.Txt_ProgSpecialMom_GSR = d1.Rows[0]["SR_PROG_SPL"].ToString() ?? "";
                model.Txt_ProgFollowUp_GSR = d1.Rows[0]["SR_PROG_FOLLOWUP"].ToString() ?? "";
                model.Txt_ProgSpeaker_GSR = d1.Rows[0]["SR_PROG_SPEAKER"].ToString() ?? "";
                model.Txt_ProgFeedback_GSR = d1.Rows[0]["SR_PROG_FEEDBACK"].ToString() ?? "";

                model.Look_ProgType_GSR = d1.Rows[0]["SR_PROG_CATEGORY"].ToString();
                model.Txt_NewsLink_GSR = d1.Rows[0]["SR_PROG_NEWS_LINK"].ToString();
                model.Txt_VVIPTestimonial_GSR = d1.Rows[0]["SR_PROG_VVIP_TESTIMONIAL"].ToString();
                model.Txt_CulturalProg_GSR = d1.Rows[0]["SR_PROG_CULTURAL"].ToString();
                model.Txt_MediaLink_GSR = d1.Rows[0]["SR_MEDIA_LINK"].ToString();

                model.ReporterMobile= d1.Rows[0]["REPORTER_MOBILE"].ToString();

                model.Wings_List_GSR = new List<WingsList>();
                foreach (DataRow xRow in BASE._SR_DBOps.GetWingsRecord(model.xID_GSR).Rows)
                {
                    model.Wings_List_GSR.Add(new Models.WingsList()
                    {
                        ID = xRow["SR_WING_ID"].ToString()
                    });
                }

                DataTable d3 = BASE._SR_DBOps.GetGuestRecord(model.xID_GSR);
                if (d3.Rows.Count > 0)
                {
                    model.Txt_GuestName_1_GSR = d3.Rows[0]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_1_GSR = d3.Rows[0]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 1)
                {
                    model.Txt_GuestName_2_GSR = d3.Rows[1]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_2_GSR = d3.Rows[1]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 2)
                {
                    model.Txt_GuestName_3_GSR = d3.Rows[2]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_3_GSR = d3.Rows[2]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 3)
                {
                    model.Txt_GuestName_4_GSR = d3.Rows[3]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_4_GSR = d3.Rows[3]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 4)
                {
                    model.Txt_GuestName_5_GSR = d3.Rows[4]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_5_GSR = d3.Rows[4]["SR_GUEST_DESIG"].ToString();
                }
                DataTable d4 = BASE._SR_DBOps.GetActivityRecord(model.xID_GSR);
                if (d4 != null && d4.Rows.Count > 0) 
                {                   
                    int count = 1;
                    foreach (DataRow row in d4.Rows) 
                    {
                        if (row["SR_ACTIVITY_NAME"].ToString() == "Deaddiction")
                        {
                            model.Txt_DeaddictionPledgeCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else if (row["SR_ACTIVITY_NAME"].ToString() == "Blood Donation")
                        {
                            model.Txt_BloodDonationUnitsCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else if (row["SR_ACTIVITY_NAME"].ToString() == "Awards Received")
                        {
                            model.Txt_AwardsReceivedCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else 
                        {
                            TreePlantationDetails newTree = new TreePlantationDetails();
                            newTree.Srno = count;
                            newTree.TreeMiscID = row["SR_ACTIVITY_DETAIL_MISC_ID"].ToString();
                            newTree.Count= Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                            TreePlantationData.Add(newTree);
                            count++;
                            model.Txt_TreeLocation_GSR = string.Concat(row["SR_ACTIVITY_LATITUDE"].ToString(), ",", row["SR_ACTIVITY_LONGITUDE"].ToString());
                            if (model.Txt_TreeLocation_GSR.Length == 1)
                            {
                                model.Txt_TreeLocation_GSR = "";
                            }
                        }
                    }
                    var distinctDt = d4.DefaultView.ToTable(true, "SR_ACTIVITY_NAME");
                    model.SelectedActivities_GSR = String.Join("![", distinctDt.AsEnumerable().Select(r => r.Field<string>("SR_ACTIVITY_NAME")));                                 
                }
                DataTable d5 = BASE._SR_DBOps.GetProgramTypeRecord(model.xID_GSR);
                if (d5 != null && d5.Rows.Count > 0)
                {
                    model.ProgType_ID_GSR = String.Join("![", d5.AsEnumerable().Select(r => r.Field<string>("SR_PROGRAM_TYPE_ID")));
                }
                DataTable d6 = BASE._SR_DBOps.GetProgramOccasionRecord(model.xID_GSR);
                if (d6 != null && d6.Rows.Count > 0)
                {
                    model.Look_ProgOccasion_GSR = String.Join("![", d6.AsEnumerable().Select(r => r.Field<string>("SR_PROGRAM_OCCASION_ID")));
                }     
                DataTable d7 = BASE._SR_DBOps.GetProgramAdditionalInfoRecord(model.xID_GSR);
                if (d7 != null && d7.Rows.Count > 0)
                {
                  model.Look_OrganisedFor_GSR= d7.Rows[0]["SRA_ORGANIZED_FOR_AB_ID"].ToString();
                  model.Look_ProgramCoordinator1_GSR= d7.Rows[0]["SRA_PROGRAM_COORDINATOR1_AB_ID"].ToString();
                  model.Look_ProgramCoordinator2_GSR = d7.Rows[0]["SRA_PROGRAM_COORDINATOR2_AB_ID"].ToString();
                  model.Look_ProgramLocationType_GSR= d7.Rows[0]["SRA_PROGRAM_LOCATION_TYPE_ID"].ToString();
                  model.Look_ProgramType_GSR= d7.Rows[0]["SRA_PROGRAM_TYPE_ID"].ToString();
                  model.Look_ParticipantCategory_GSR= d7.Rows[0]["SRA_PARTICIPANT_CATEGORY_ID"].ToString();
                  model.Look_ParticipantSubCategory_GSR= d7.Rows[0]["SRA_PARTICIPANT_SUB_CATEGORY_ID"].ToString();
                  model.Look_ParticipantType_GSR= d7.Rows[0]["SRA_PARTICIPANT_TYPE"].ToString();
                }
                DataTable d8 = BASE._SR_DBOps.GetProgramCollaboratedCenters(model.xID_GSR);
                if (d8 != null && d8.Rows.Count > 0)
                {
                    model.Coll_CEN_ID_GSR = String.Join("![", d8.AsEnumerable().Select(r => r.Field<Int32>("SR_CEN_ID")));
                }
                DataTable d9 = BASE._SR_DBOps.GetProgramVenueTypeRecord(model.xID_GSR);
                if (d9 != null && d9.Rows.Count > 0)
                {
                    model.Look_ProgVenueType_GSR = String.Join("![", d9.AsEnumerable().Select(r => r.Field<string>("SRVT_VENUE_ID")));
                }
                List<Attachment_List> Attachments = BASE._Attachments_DBOps.GetList(model.xID_GSR);
                if (Attachments != null && Attachments.Count > 0) 
                {
                   model.Attachment_Details = new List<SR_AttachmentDetails>(Attachments.Count);             
                    for (int i = 0; i < Attachments.Count; i++) 
                    {
                        SR_AttachmentDetails row = new SR_AttachmentDetails();
                        row.Attachment_ID=Attachments[i].ID;
                        row.Attachment_FileName=Attachments[i].File_Name;
                        row.Attachment_Description=Attachments[i].Only_Description;
                        row.Attachment_Rating = Attachments[i].AI_CEN_RATING;
                        row.Attachment_Type=MimeMapping.GetMimeMapping(Attachments[i].File_Name);
                        row.Attachment_Path = GetAttachmentPath(Attachments[i].ID, Attachments[i].File_Name);
                        model.Attachment_Details.Add(row);
                    }
                    //List<Attachment_List> files =Attachments.FindAll(x => x.Description.Contains("Program Banner"));
                    //if (files != null && files.Count>0) 
                    //{
                    //    model.ProgramBanner_GSR = string.Join(", ", files.Select(x => x.File_Name));
                    //    Attachments.Remove(x => x.Description.Contains("Program Banner"));
                    //}
                    //files= Attachments.FindAll(x => x.Description.Contains("Program Promo"));
                    //if (files != null && files.Count > 0)
                    //{
                    //    model.ProgramPromo_GSR = string.Join(", ", files.Select(x => x.File_Name));
                    //    Attachments.Remove(x => x.Description.Contains("Program Promo"));
                    //}
                    //files= Attachments.FindAll(x => x.Description.Contains("Program Press Release"));
                    //if (files != null && files.Count > 0)
                    //{
                    //    model.ProgramPressRelease_GSR = string.Join(", ", files.Select(x => x.File_Name));
                    //    Attachments.Remove(x => x.Description.Contains("Program Press Release"));
                    //}
                   // files = Attachments.FindAll(x => x.Description.Contains("Program Pictures") || x.Description.Contains("Program Videos"));
                    if (Attachments != null && Attachments.Count > 0)
                    {
                        model.ProgramPictures_GSR = string.Join(", ", Attachments.Select(x => x.File_Name));
                    }
                }
            }
            model.SelectedActivities_GSR = model.SelectedActivities_GSR ?? "";
            model.ProgType_ID_GSR = model.ProgType_ID_GSR ?? "";
            model.Look_ProgOccasion_GSR = model.Look_ProgOccasion_GSR ?? "";
            model.Coll_CEN_ID_GSR = model.Coll_CEN_ID_GSR ?? "";
            DataTable Cen_data = BASE._SR_DBOps.GetCenterMobileAndLocation(BASE._open_Cen_ID);
            if (Cen_data != null && Cen_data.Rows.Count > 0) 
            {
                model.Cen_Mobile = Cen_data.Rows[0]["Cen_Mobile"].ToString();
                model.Cen_Location_Coordinates = string.Concat(Cen_data.Rows[0]["CEN_LOC_LAT"].ToString(), ",", Cen_data.Rows[0]["CEN_LOC_LONG"].ToString());
                if (model.Cen_Location_Coordinates.Length == 1) 
                {
                    model.Cen_Location_Coordinates = "";
                }
            }
            //model.Txt_TreeLocation_GSR = string.IsNullOrWhiteSpace(model.Txt_TreeLocation_GSR) ? model.Cen_Location_Coordinates : model.Txt_TreeLocation_GSR;
            model.Txt_MobNoVerification_GSR = string.IsNullOrWhiteSpace(model.ReporterMobile) ? model.Cen_Mobile : model.ReporterMobile;
            DataTable mob_verify = BASE._SR_DBOps.GetMobileVerificationStatus(model.Txt_MobNoVerification_GSR);
            if (mob_verify != null && mob_verify.Rows.Count > 0) 
            {
                model.IsReporterMobile_Verified = Convert.ToBoolean(mob_verify.Rows[0]["Is_Verified"]);
            }
            return View(model);
        }
        [CheckLogin]
        [HttpPost]
        public ActionResult Frm_Service_Report_Window(ServiceReport_ViewModel model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
            }
            try
            {
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        DataTable ServRep_DbOps = BASE._SR_DBOps.GetRecord(model.xID_GSR);
                        if (ServRep_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Service Report");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(ServRep_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Service Report");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    if (string.IsNullOrWhiteSpace(model.Look_MasterProject_GSR) == true) 
                    {
                        jsonParam.message = "Master Project Name not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Look_MasterProject_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //if (string.IsNullOrWhiteSpace(model.Look_ProjList_GSR) || model.Look_ProjList_GSR.Trim().Length == 0)
                    //{
                    //    jsonParam.message = "Project Name Not Selected. . . !";
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.focusid = "Look_ProjList_GSR";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}
                    if (string.IsNullOrWhiteSpace(model.Txt_ProgName_GSR) || model.Txt_ProgName_GSR.Trim().Length == 0)
                    {
                        jsonParam.message = "Programme Name cannot be Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_ProgName_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_ProgVenue_GSR) || model.Txt_ProgVenue_GSR.Trim().Length == 0)
                    {
                        jsonParam.message = "Programme Venue cannot be Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_ProgVenue_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._open_Ins_ID == "00010" && (string.IsNullOrWhiteSpace(model.Institute_GSR) || model.Institute_GSR.Trim().Length == 0))
                    {
                        jsonParam.message = "Institute cannot be Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Institute_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.DE_FR_DT_GSR) == false)
                    {
                        jsonParam.message = "From Date Incorrect / Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "DE_FR_DT_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.DE_TO_DT_GSR) == false)
                    {
                        jsonParam.message = "To Date Incorrect / Blank. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "DE_TO_DT_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Convert.ToDateTime(model.DE_TO_DT_GSR) < Convert.ToDateTime(model.DE_FR_DT_GSR))
                    {
                        jsonParam.message = "To Date Should be Greater Or Equal To From Date. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "DE_TO_DT_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_Fr_Time_GSR.ToString()) || model.Txt_Fr_Time_GSR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "From Time cannot be Blank / Incorrect. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Fr_Time_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Txt_To_Time_GSR.ToString()) || model.Txt_To_Time_GSR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "To Time cannot be Blank/Incorrect. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_To_Time_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    decimal TotalBreakup = Convert.ToDecimal(model.Txt_ChildBeneficiaries_GSR) + Convert.ToDecimal(model.Txt_FemaleBeneficiaries_GSR) + Convert.ToDecimal(model.Txt_MaleBeneficiaries_GSR) + Convert.ToDecimal(model.Txt_SrCitizenBeneficiaries_GSR) + Convert.ToDecimal(model.Txt_WomenBeneficiaries_GSR) + Convert.ToDecimal(model.Txt_YouthBeneficiaries_GSR);
                    if (TotalBreakup > 0)
                    {
                        if (TotalBreakup != Convert.ToDecimal(model.Txt_ProgBeneficiaries_GSR) && TotalBreakup != Convert.ToDecimal(model.Txt_TotalProgBeneficiaries_GSR))
                        {
                            jsonParam.message = "Beneficiary Breakup not equal to Offline or Total Count. . . !";
                            jsonParam.title = "Incorrect Information . . .";
                            jsonParam.focusid = "Txt_ProgBeneficiaries_GSR";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DateTime FromDateTime = Convert.ToDateTime(model.DE_FR_DT_GSR).Add(Convert.ToDateTime(model.Txt_Fr_Time_GSR).TimeOfDay);
                    DateTime ToDateTime = Convert.ToDateTime(model.DE_TO_DT_GSR).Add(Convert.ToDateTime(model.Txt_To_Time_GSR).TimeOfDay);
                    if (ToDateTime <= FromDateTime)
                    {
                        jsonParam.message = "To Time Should Be Greater Than From Time";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_To_Time_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_TotalProgBeneficiaries_GSR == null || model.Txt_TotalProgBeneficiaries_GSR <= 0)
                    {
                        jsonParam.message = "No. of Beneficiaries cannot be Zero/Negative. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_ProgBeneficiaries_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToBoolean(model.Chk_WingBasedFlag_GSR == "true") && model.Wings_List_GSR.Where(x => x.Selected == true).Count() <= 0)
                    {
                        jsonParam.message = "Wing Name Not Selected. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Wings_List_GSR";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._New) 
                    {
                        var files = Request.Files;
                        if (files == null || files.Count == 0) 
                        {
                            jsonParam.message = "Attach Program Pictures/Videos/Press Release/Banner/Promo. . . !";
                            jsonParam.title = "Incomplete Information . . .";                         
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (files[0].ContentLength == 0) 
                        {
                            jsonParam.message = "Invalid Attachment. . . !";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                string Latitude = "";
                string Longitude = "";
                if (string.IsNullOrWhiteSpace(model.FinalPlantationCoordinates_GSR)==false) 
                {
                    string[] coordinates = model.FinalPlantationCoordinates_GSR.Split(',');
                    Latitude = coordinates[0];
                    Longitude= coordinates[1];
                }
                model.SelectedActivities_GSR = model.SelectedActivities_GSR ?? "";
                var SelectedActivities = model.SelectedActivities_GSR.Split(new string[] { "![" }, StringSplitOptions.None);      
                if (SelectedActivities!=null && SelectedActivities.Length>0) 
                {
                    if (Array.IndexOf(SelectedActivities,"Deaddiction") > -1) 
                    {
                        if (model.Txt_DeaddictionPledgeCount_GSR == null || model.Txt_DeaddictionPledgeCount_GSR < 1) 
                        {
                            jsonParam.message = "Deaddiction Pledge Count Is Required And Cannot Be Less Than 1...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_DeaddictionPledgeCount_GSR";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Array.IndexOf(SelectedActivities,"Blood Donation") > -1) 
                    {
                        if (model.Txt_BloodDonationUnitsCount_GSR == null || model.Txt_BloodDonationUnitsCount_GSR < 1) 
                        {
                            jsonParam.message = "Blood Donated Units Count Is Required And Cannot Be Less Than 1...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_BloodDonationUnitsCount_GSR";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Array.IndexOf(SelectedActivities,"Awards Received") > -1) 
                    {
                        if (model.Txt_AwardsReceivedCount_GSR == null || model.Txt_AwardsReceivedCount_GSR < 1) 
                        {
                            jsonParam.message = "Awards Received Count Is Required And Cannot Be Less Than 1...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_AwardsReceivedCount_GSR";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Array.IndexOf(SelectedActivities, "Tree Plantation") > -1) 
                    {
                        if (TreePlantationData.Count == 0) 
                        {
                            jsonParam.message = "Tree Plantation Details Are Required...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "TreePlantation_DG";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                model.ProgType_ID_GSR = model.ProgType_ID_GSR ?? "";
                var ProgType= model.ProgType_ID_GSR.Split(new string[] { "![" }, StringSplitOptions.None);            
                if (ProgType.Length == 0) 
                {
                    jsonParam.message = "Program Type Not Selected. . . !";
                    jsonParam.title = "Incomplete Information . . .";
                    jsonParam.focusid = "Look_ProgType_GSR";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }                
                Common_Lib.RealTimeService.Param_Txn_Insert_ServiceReport InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_ServiceReport();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New)         //'new
                {
                    Common_Lib.RealTimeService.Parameter_Insert_ServiceReport InParam = new Common_Lib.RealTimeService.Parameter_Insert_ServiceReport();
                    InParam.ProjectID = model.Look_ProjList_GSR ?? "";
                    InParam.ProgType = Convert.ToBoolean(model.Chk_WingBasedFlag_GSR == "true") ? 1.ToString() : 0.ToString();
                    InParam.Name = model.Txt_ProgName_GSR ?? "";
                    InParam.Venue = model.Txt_ProgVenue_GSR ?? "";
                    InParam.FromDate = Convert.ToDateTime(model.DE_FR_DT_GSR).ToString(BASE._Server_Date_Format_Long);
                    InParam.ToDate = Convert.ToDateTime(model.DE_TO_DT_GSR).ToString(BASE._Server_Date_Format_Long);
                    InParam.FromTime = Convert.ToDateTime(model.Txt_Fr_Time_GSR).ToString(BASE._Time_Format_AM_PM, BASE.fi);
                    InParam.ToTime = Convert.ToDateTime(model.Txt_To_Time_GSR).ToString(BASE._Time_Format_AM_PM, BASE.fi);
                    InParam.Period = model.Txt_Period_Time_GSR ?? "";
                    InParam.Brief = model.Txt_ProgBrief_GSR ?? "";
                    InParam.SpecialMoments = model.Txt_ProgSpecialMom_GSR ?? "";
                    InParam.Beneficiaries = Convert.ToDouble(model.Txt_TotalProgBeneficiaries_GSR);
                    InParam.Followup = model.Txt_ProgFollowUp_GSR ?? "";
                    InParam.Speaker = model.Txt_ProgSpeaker_GSR ?? "";
                    InParam.Feedback = model.Txt_ProgFeedback_GSR ?? "";
                    InParam.Rec_ID = model.xID_GSR;
                    InParam.ProgCategory = model.ProgType_Text_GSR ?? "";
                    InParam.NewsLink = model.Txt_NewsLink_GSR ?? "";
                    InParam.VVIP_Testimonial = model.Txt_VVIPTestimonial_GSR ?? "";
                    InParam.Cultural = model.Txt_CulturalProg_GSR ?? "";
                    InParam.CenID = String.IsNullOrWhiteSpace(model.ReporterCenID) ? BASE._open_Cen_ID.ToString() : model.ReporterCenID;
                    InParam.ReporterEmail = model.ReporterEmail ?? "";
                    InParam.ReporterMobile = model.ReporterMobile ?? "";
                    InParam.MediaLink = model.Txt_MediaLink_GSR ?? "";
                    InParam.MasterProjID = model.Look_MasterProject_GSR ?? "";
                    InParam.Offline_Beneficiaries = Convert.ToDouble(model.Txt_ProgBeneficiaries_GSR);
                    InParam.Online_Beneficiaries = Convert.ToDouble(model.Txt_OnlineProgBeneficiaries_GSR);
                    InParam.NoOfEvent = model.Txt_NoOfEvents_GSR;
                    InParam.ProgramOccasion = model.ProgOccasion_Text_GSR ?? "";
                    InParam.ProgramOrganiser = model.Txt_Organiser_GSR ?? "";
                    InParam.Institute_ID = model.Institute_GSR;
                    InParam.Child_Beneficiaries = model.Txt_ChildBeneficiaries_GSR;
                    InParam.Youth_Beneficiaries = model.Txt_YouthBeneficiaries_GSR;
                    InParam.Male_Beneficiaries = model.Txt_MaleBeneficiaries_GSR;
                    InParam.Female_Beneficiaries = model.Txt_FemaleBeneficiaries_GSR;
                    InParam.Women_Beneficiaries = model.Txt_WomenBeneficiaries_GSR;
                    InParam.SeniorCitizen_Beneficiaries = model.Txt_SrCitizenBeneficiaries_GSR;
                    if (Array.IndexOf(SelectedActivities, "Tree Plantation") > -1)
                    {
                        InParam.ReporterMobile = model.FinalVerifiedMobNo_GSR;
                    }
                        InNewParam.param_Insert_ServiceReport = InParam;

                    List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport> InWingsInfo = new List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport>();
                    if (Convert.ToBoolean(model.Chk_WingBasedFlag_GSR == "true"))
                    {
                        if (model.Wings_List_GSR.Where(x => x.Selected == true).Count() > 0)
                        {
                            foreach (var CurrSelection in model.Wings_List_GSR.Where(x => x.Selected == true))
                            {
                                Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport InWings = new Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport();
                                InWings.Sr_ID = model.xID_GSR;
                                InWings.WingID = CurrSelection.ID;
                                InWings.Rec_ID = System.Guid.NewGuid().ToString();
                                InWingsInfo.Add(InWings);
                            }
                        }
                    }
                    InNewParam.InsertWings = InWingsInfo.ToArray();
                    List<Parameter_InsertAcivityDetail_ServiceReport> InActivityInfo = new List<Parameter_InsertAcivityDetail_ServiceReport>();
                    if (SelectedActivities != null && SelectedActivities.Length > 0)
                    {
                        if (Array.IndexOf(SelectedActivities, "Deaddiction") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Deaddiction";
                            row.ActivityCount = (int)model.Txt_DeaddictionPledgeCount_GSR;
                            row.Rec_ID= Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }
                        if (Array.IndexOf(SelectedActivities, "Blood Donation") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Blood Donation";
                            row.ActivityCount = (int)model.Txt_BloodDonationUnitsCount_GSR;
                            row.Rec_ID= Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }
                        if (Array.IndexOf(SelectedActivities, "Awards Received") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Awards Received";
                            row.ActivityCount = (int)model.Txt_AwardsReceivedCount_GSR;
                            row.Rec_ID= Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }
                        if (Array.IndexOf(SelectedActivities, "Tree Plantation") > -1)
                        {
                            for (int i = 0; i < TreePlantationData.Count; i++) 
                            {
                                Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                                row.Sr_ID = model.xID_GSR;
                                row.ActivityName = "Tree Plantation";
                                row.ActivitySpecialDetail = TreePlantationData[i].TreeMiscID;
                                row.ActivityCount = TreePlantationData[i].Count;
                                row.Rec_ID = Guid.NewGuid().ToString();
                                row.Latitude = Latitude;
                                row.Longitude = Longitude;
                                InActivityInfo.Add(row);
                            }                        
                        }
                    }
                    InNewParam.InsertActivityDetails = InActivityInfo.ToArray();

                    List<Parameter_InsertProgramType_ServiceReport> InProgramType = new List<Parameter_InsertProgramType_ServiceReport>();
                    for (int i = 0; i < ProgType.Length; i++) 
                    {
                        Parameter_InsertProgramType_ServiceReport row = new Parameter_InsertProgramType_ServiceReport();
                        row.Sr_ID = model.xID_GSR;
                        row.ProgTypeID = ProgType[i];                  
                        row.Rec_ID = Guid.NewGuid().ToString();
                        InProgramType.Add(row);
                    }
                    InNewParam.InsertProgType = InProgramType.ToArray();
                    if (string.IsNullOrWhiteSpace(model.Look_ProgOccasion_GSR)==false) 
                    {
                        List<Parameter_InsertProgramOccasion_ServiceReport> InProgramOccasion = new List<Parameter_InsertProgramOccasion_ServiceReport>();
                        var ProgOccasion = model.Look_ProgOccasion_GSR.Split(',');
                        for (int i = 0; i < ProgOccasion.Length; i++)
                        {
                            Parameter_InsertProgramOccasion_ServiceReport row = new Parameter_InsertProgramOccasion_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ProgOccasionID = ProgOccasion[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InProgramOccasion.Add(row);
                        }
                        InNewParam.InsertProgOccasion = InProgramOccasion.ToArray();
                    }                    
                    
                    if (string.IsNullOrWhiteSpace(model.Coll_CEN_ID_GSR)==false) 
                    {
                        List<Parameter_InsertCollaborateCenters_ServiceReport> InProgramCollaborateCenters = new List<Parameter_InsertCollaborateCenters_ServiceReport>();
                        var CollCenters = model.Coll_CEN_ID_GSR.Split(new string[] { "![" }, StringSplitOptions.None);
                        for (int i = 0; i < CollCenters.Length; i++)
                        {
                            Parameter_InsertCollaborateCenters_ServiceReport row = new Parameter_InsertCollaborateCenters_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.CEN_ID = CollCenters[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InProgramCollaborateCenters.Add(row);
                        }
                        InNewParam.InsertCollaborateCenters = InProgramCollaborateCenters.ToArray();
                    }

                    if (model.InsertAdditonalinfo_GSR == true) 
                    {
                        Common_Lib.RealTimeService.Parameter_InsertAdditionalInfo_ServiceReport Inaddtional = new Common_Lib.RealTimeService.Parameter_InsertAdditionalInfo_ServiceReport();
                        Inaddtional.OrganizedFor = model.Look_OrganisedFor_GSR;
                        Inaddtional.ParticipantCategory = model.Look_ParticipantCategory_GSR;
                        Inaddtional.ParticipantSubCategory = model.Look_ParticipantSubCategory_GSR;
                        Inaddtional.ParticipantType= model.Look_ParticipantType_GSR;
                        Inaddtional.ProgramCoordinator_1= model.Look_ProgramCoordinator1_GSR;
                        Inaddtional.ProgramCoordinator_2= model.Look_ProgramCoordinator2_GSR;
                        Inaddtional.ProgramLocationType= model.Look_ProgramLocationType_GSR;
                        Inaddtional.ProgramType= model.Look_ProgramType_GSR;
                        Inaddtional.Rec_ID = System.Guid.NewGuid().ToString();
                        Inaddtional.Sr_ID = model.xID_GSR;

                        InNewParam.param_InsertAdditonalInfo = Inaddtional;
                    }

                    if (string.IsNullOrWhiteSpace(model.Look_ProgVenueType_GSR) == false)
                    {
                        List<Parameter_InsertVenueType_ServiceReport> InProgramVenueType = new List<Parameter_InsertVenueType_ServiceReport>();
                        var venueType = model.Look_ProgVenueType_GSR.Split(',');
                        for (int i = 0; i < venueType.Length; i++)
                        {
                            Parameter_InsertVenueType_ServiceReport row = new Parameter_InsertVenueType_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.Venue_misc_id = venueType[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InProgramVenueType.Add(row);
                        }
                        InNewParam.InsertVenueType = InProgramVenueType.ToArray();
                    }

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGuest1 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGuest1.Sr_ID = model.xID_GSR;
                    InGuest1.Sr_No = 1;
                    InGuest1.GusetName = model.Txt_GuestName_1_GSR ?? "";
                    InGuest1.GuestDesignation = model.Txt_GuestDesig_1_GSR ?? "";
                    InGuest1.Rec_ID = System.Guid.NewGuid().ToString();


                    InNewParam.param_InsertGuest1 = InGuest1;

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGuest2 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGuest2.Sr_ID = model.xID_GSR;
                    InGuest2.Sr_No = 2;
                    InGuest2.GusetName = model.Txt_GuestName_2_GSR ?? "";
                    InGuest2.GuestDesignation = model.Txt_GuestDesig_2_GSR ?? "";
                    InGuest2.Rec_ID = System.Guid.NewGuid().ToString();


                    InNewParam.param_InsertGuest2 = InGuest2;

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGuest3 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGuest3.Sr_ID = model.xID_GSR;
                    InGuest3.Sr_No = 3;
                    InGuest3.GusetName = model.Txt_GuestName_3_GSR ?? "";
                    InGuest3.GuestDesignation = model.Txt_GuestDesig_3_GSR ?? "";
                    InGuest3.Rec_ID = System.Guid.NewGuid().ToString();


                    InNewParam.param_InsertGuest3 = InGuest3;

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGuest4 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGuest4.Sr_ID = model.xID_GSR;
                    InGuest4.Sr_No = 4;
                    InGuest4.GusetName = model.Txt_GuestName_4_GSR ?? "";
                    InGuest4.GuestDesignation = model.Txt_GuestDesig_4_GSR ?? "";
                    InGuest4.Rec_ID = System.Guid.NewGuid().ToString();


                    InNewParam.param_InsertGuest4 = InGuest4;

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGuest5 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGuest5.Sr_ID = model.xID_GSR;
                    InGuest5.Sr_No = 5;
                    InGuest5.GusetName = model.Txt_GuestName_5_GSR ?? "";
                    InGuest5.GuestDesignation = model.Txt_GuestDesig_5_GSR ?? "";
                    InGuest5.Rec_ID = System.Guid.NewGuid().ToString();


                    InNewParam.param_InsertGuest5 = InGuest5;

                    var files = Request.Files;
                    if (files != null && files.Count > 0)
                    {
                        string filesUploadMessage = "";
                        bool FileUploadResult = false;
                        string FileNames = "";
                        string AttachmentIds = "";
                        var x = Attachment_Save(model.attachment_Window, ClientScreen.Facility_ServiceReport);
                        string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)x).Data);
                        Return_Attachment_Post DynamicData = JsonConvert.DeserializeObject<Return_Attachment_Post>(jsonString);
                        filesUploadMessage = DynamicData.Message.ToString();
                        FileUploadResult = DynamicData.result;
                        FileNames = DynamicData.FileList;
                        AttachmentIds = DynamicData.AttachmentIdsList;
                        if (FileUploadResult == true && string.IsNullOrWhiteSpace(FileNames)==false && string.IsNullOrWhiteSpace(AttachmentIds) == false)
                        {
                            string AttachmentPath ="~/Attachments/"+ CommonFunctions.GetAttachment_DiskFileName(AttachmentIds.Split(',')[0], FileNames.Split(',')[0], false);
                            string absolutePath = HttpContext.Server.MapPath(AttachmentPath);
                            if (System.IO.File.Exists(absolutePath))
                            {
                                if (!BASE._SR_DBOps.InsertServiceReport_Txn(InNewParam))
                                {
                                    jsonParam.message = Common_Lib.Messages.SomeError + "<br> While Saving The Service Report";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    string ResultString = "";
                                    if (string.IsNullOrWhiteSpace(model.ReporterMobile) == false)
                                    {
                                        BASE._Notifications_DBOps.SendSMS(null, "SUBMIT_GodlyServiceReport", ref ResultString, model.xID_GSR);
                                    }
                                    DataTable Emailerdata = BASE._SR_DBOps.GetDataForServiceReportEmail(model.xID_GSR);
                                    string EmailMessage = "";
                                    if (Emailerdata != null && Emailerdata.Rows.Count > 0)
                                    {
                                        SendHTMLEmail(ref EmailMessage, Emailerdata.Rows[0]["CENTER_EMAIL"].ToString(), Emailerdata.Rows[0]["SUBJECT"].ToString(),
                                            "~/Areas/Facility/Views/ServiceReport/ServiceReportEmailer.cshtml", Emailerdata, true, "", Emailerdata.Rows[0]["CC"].ToString(), Emailerdata.Rows[0]["BCC"].ToString());
                                    }
                                    jsonParam.message = Common_Lib.Messages.SaveSuccess + "<br>" + ResultString + EmailMessage + filesUploadMessage;
                                    jsonParam.title = model.TitleX_GSR;
                                    jsonParam.result = true;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam, xID = model.xID_GSR }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else 
                            {
                                jsonParam.message = "Cannot Save Service Report<br>Attachments Not Found In Disk<br>" + filesUploadMessage;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            jsonParam.message = "Cannot Save Service Report<br>Attachments Could Not Be Uploaded<br>" + filesUploadMessage;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.focusid = "FileUpload";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.message = "Cannot Save Service Report<br>No Attachments Found";
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.focusid = "FileUpload";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }


                    //if (!BASE._SR_DBOps.InsertServiceReport_Txn(InNewParam))
                    //{
                    //    jsonParam.message = Common_Lib.Messages.SomeError;
                    //    jsonParam.title = "Error!!";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{                        
                    //    string filesUploadMessage = "";                
                    //    if (string.IsNullOrWhiteSpace(model.attachment_Window.Help_Document_FileName)==false)
                    //    {  
                    //        var x = Attachment_Save(model.attachment_Window,ClientScreen.Facility_ServiceReport);
                    //        string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)x).Data);
                    //        Return_Attachment_Post DynamicData=JsonConvert.DeserializeObject<Return_Attachment_Post>(jsonString);                     
                    //        filesUploadMessage = DynamicData.Message.ToString();       
                    //    }
                    //    //Attachments uploading part is ended here.
                    //    string ResultString = "";
                    //    if (string.IsNullOrWhiteSpace(model.ReporterMobile) == false)
                    //    {
                    //        BASE._Notifications_DBOps.SendSMS(null, "SUBMIT_GodlyServiceReport", ref ResultString, model.xID_GSR);
                    //    }
                    //    DataTable Emailerdata = BASE._SR_DBOps.GetDataForServiceReportEmail(model.xID_GSR);
                    //    string EmailMessage = "";
                    //    if (Emailerdata != null && Emailerdata.Rows.Count > 0)
                    //    {
                    //        SendHTMLEmail(ref EmailMessage, Emailerdata.Rows[0]["CENTER_EMAIL"].ToString(), Emailerdata.Rows[0]["SUBJECT"].ToString(), 
                    //            "~/Areas/Facility/Views/ServiceReport/ServiceReportEmailer.cshtml", Emailerdata, true, "", Emailerdata.Rows[0]["CC"].ToString(), Emailerdata.Rows[0]["BCC"].ToString());
                    //    }
                    //    jsonParam.message = Common_Lib.Messages.SaveSuccess + "<br>" + ResultString + EmailMessage + filesUploadMessage ;
                    //    jsonParam.title = model.TitleX_GSR;
                    //    jsonParam.result = true;
                    //    jsonParam.refreshgrid = true;
                    //    jsonParam.closeform = true;
                    //    return Json(new { jsonParam, xID = model.xID_GSR }, JsonRequestBehavior.AllowGet);
                    //}
                    
                }
                Common_Lib.RealTimeService.Param_Txn_Update_ServiceReport EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_ServiceReport();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)   //'edit
                {
                    Common_Lib.RealTimeService.Parameter_Update_ServiceReport UpParam = new Common_Lib.RealTimeService.Parameter_Update_ServiceReport();
                    UpParam.ProjectID = model.Look_ProjList_GSR ?? "";
                    UpParam.ProgType = Convert.ToBoolean(model.Chk_WingBasedFlag_GSR == "true") ? 1.ToString() : 0.ToString();
                    UpParam.Name = model.Txt_ProgName_GSR ?? "";
                    UpParam.Venue = model.Txt_ProgVenue_GSR ?? "";
                    UpParam.FromDate = Convert.ToDateTime(model.DE_FR_DT_GSR).ToString(BASE._Server_Date_Format_Long);
                    UpParam.ToDate = Convert.ToDateTime(model.DE_TO_DT_GSR).ToString(BASE._Server_Date_Format_Long);
                    UpParam.FromTime = Convert.ToDateTime(model.Txt_Fr_Time_GSR).ToString(BASE._Time_Format_AM_PM, BASE.fi);
                    UpParam.ToTime = Convert.ToDateTime(model.Txt_To_Time_GSR).ToString(BASE._Time_Format_AM_PM, BASE.fi);
                    UpParam.Period = model.Txt_Period_Time_GSR ?? "";
                    UpParam.Brief = model.Txt_ProgBrief_GSR ?? "";
                    UpParam.SpecialMoments = model.Txt_ProgSpecialMom_GSR ?? "";
                    UpParam.Beneficiaries = Convert.ToDouble(model.Txt_TotalProgBeneficiaries_GSR);
                    UpParam.Followup = model.Txt_ProgFollowUp_GSR ?? "";
                    UpParam.Speaker = model.Txt_ProgSpeaker_GSR ?? "";
                    UpParam.Feedback = model.Txt_ProgFeedback_GSR ?? "";
                    UpParam.Rec_ID = model.xID_GSR;
                    UpParam.ProgCategory = model.ProgType_Text_GSR ?? "";
                    UpParam.NewsLink = model.Txt_NewsLink_GSR ?? "";
                    UpParam.VVIP_Testimonial = model.Txt_VVIPTestimonial_GSR ?? "";
                    UpParam.Cultural = model.Txt_CulturalProg_GSR ?? "";
                    UpParam.CenID = String.IsNullOrWhiteSpace(model.ReporterCenID) ? BASE._open_Cen_ID.ToString() : model.ReporterCenID;
                    UpParam.ReporterEmail = model.ReporterEmail ?? "";
                    UpParam.ReporterMobile = model.ReporterMobile ?? "";
                    UpParam.ReporterMobile = model.ReporterMobile ?? "";
                    UpParam.MediaLink = model.Txt_MediaLink_GSR ?? "";
                    UpParam.MasterProjID = model.Look_MasterProject_GSR ?? "";
                    UpParam.Offline_Beneficiaries = Convert.ToDouble(model.Txt_ProgBeneficiaries_GSR);
                    UpParam.Online_Beneficiaries = Convert.ToDouble(model.Txt_OnlineProgBeneficiaries_GSR);
                    UpParam.NoOfEvent = model.Txt_NoOfEvents_GSR;
                    UpParam.ProgramOccasion = model.ProgOccasion_Text_GSR ?? "";
                    UpParam.ProgramOrganiser = model.Txt_Organiser_GSR ?? "";
                    UpParam.Institute_ID = model.Institute_GSR;
                    UpParam.Child_Beneficiaries = model.Txt_ChildBeneficiaries_GSR;
                    UpParam.Youth_Beneficiaries = model.Txt_YouthBeneficiaries_GSR;
                    UpParam.Male_Beneficiaries = model.Txt_MaleBeneficiaries_GSR;
                    UpParam.Female_Beneficiaries = model.Txt_FemaleBeneficiaries_GSR;
                    UpParam.Women_Beneficiaries = model.Txt_WomenBeneficiaries_GSR;
                    UpParam.SeniorCitizen_Beneficiaries = model.Txt_SrCitizenBeneficiaries_GSR;
                    if (Array.IndexOf(SelectedActivities, "Tree Plantation") > -1)
                    {
                        UpParam.ReporterMobile = model.FinalVerifiedMobNo_GSR;
                    }
                    EditParam.param_Update_ServiceReport = UpParam;


                    EditParam.RecID_DeleteWing = model.xID_GSR;
                    List<Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport> InWingsInfo = new List<Parameter_InsertWings_ServiceReport>();
                    if (Convert.ToBoolean(model.Chk_WingBasedFlag_GSR == "true"))
                    {
                        foreach (var currSelection in model.Wings_List_GSR.Where(x => x.Selected == true))
                        {
                            Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport InWing = new Common_Lib.RealTimeService.Parameter_InsertWings_ServiceReport();
                            InWing.Sr_ID = model.xID_GSR;
                            InWing.WingID = currSelection.ID;
                            InWing.Rec_ID = System.Guid.NewGuid().ToString();
                            InWingsInfo.Add(InWing);
                        }
                    }
                    EditParam.InsertWings = InWingsInfo.ToArray();

                    EditParam.RecID_DeleteAcitvity = model.xID_GSR;
                    List<Parameter_InsertAcivityDetail_ServiceReport> InActivityInfo = new List<Parameter_InsertAcivityDetail_ServiceReport>();
                    if (SelectedActivities != null && SelectedActivities.Length > 0)
                    {
                        if (Array.IndexOf(SelectedActivities, "Deaddiction") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Deaddiction";
                            row.ActivityCount = (int)model.Txt_DeaddictionPledgeCount_GSR;
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }
                        if (Array.IndexOf(SelectedActivities, "Blood Donation") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Blood Donation";
                            row.ActivityCount = (int)model.Txt_BloodDonationUnitsCount_GSR;
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }
                        if (Array.IndexOf(SelectedActivities, "Awards Received") > -1)
                        {
                            Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ActivityName = "Awards Received";
                            row.ActivityCount = (int)model.Txt_AwardsReceivedCount_GSR;
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InActivityInfo.Add(row);
                        }

                        if (Array.IndexOf(SelectedActivities, "Tree Plantation") > -1)
                        {
                            for (int i = 0; i < TreePlantationData.Count; i++)
                            {
                                Parameter_InsertAcivityDetail_ServiceReport row = new Parameter_InsertAcivityDetail_ServiceReport();
                                row.Sr_ID = model.xID_GSR;
                                row.ActivityName = "Tree Plantation";
                                row.ActivitySpecialDetail = TreePlantationData[i].TreeMiscID;
                                row.ActivityCount = TreePlantationData[i].Count;
                                row.Rec_ID = Guid.NewGuid().ToString();
                                row.Latitude = Latitude;
                                row.Longitude = Longitude;
                                InActivityInfo.Add(row);
                            }
                        }
                    }
                    EditParam.InsertActivityDetails = InActivityInfo.ToArray();

                    EditParam.RecID_DeleteProgType = model.xID_GSR;
                    List<Parameter_InsertProgramType_ServiceReport> InProgramType = new List<Parameter_InsertProgramType_ServiceReport>();
                    for (int i = 0; i < ProgType.Length; i++)
                    {
                        Parameter_InsertProgramType_ServiceReport row = new Parameter_InsertProgramType_ServiceReport();
                        row.Sr_ID = model.xID_GSR;
                        row.ProgTypeID = ProgType[i];
                        row.Rec_ID = Guid.NewGuid().ToString();
                        InProgramType.Add(row);
                    }
                    EditParam.InsertProgType = InProgramType.ToArray();

                    EditParam.RecID_DeleteProgOccasion = model.xID_GSR;
                    if (string.IsNullOrWhiteSpace(model.Look_ProgOccasion_GSR) == false)
                    {
                        List<Parameter_InsertProgramOccasion_ServiceReport> InProgramOccasion = new List<Parameter_InsertProgramOccasion_ServiceReport>();
                        var ProgOccasion = model.Look_ProgOccasion_GSR.Split(',');
                        for (int i = 0; i < ProgOccasion.Length; i++)
                        {
                            Parameter_InsertProgramOccasion_ServiceReport row = new Parameter_InsertProgramOccasion_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.ProgOccasionID = ProgOccasion[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InProgramOccasion.Add(row);
                        }
                        EditParam.InsertProgOccasion = InProgramOccasion.ToArray();
                    }
                    EditParam.RecID_DeleteCollaborateCenters = model.xID_GSR;
                    if (string.IsNullOrWhiteSpace(model.Coll_CEN_ID_GSR) == false)
                    {
                        List<Parameter_InsertCollaborateCenters_ServiceReport> InsertCollaCenters = new List<Parameter_InsertCollaborateCenters_ServiceReport>();
                        var CollaCenters = model.Coll_CEN_ID_GSR.Split(new string[] { "![" }, StringSplitOptions.None);
                        for (int i = 0; i < CollaCenters.Length; i++)
                        {
                            Parameter_InsertCollaborateCenters_ServiceReport row = new Parameter_InsertCollaborateCenters_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.CEN_ID = CollaCenters[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InsertCollaCenters.Add(row);
                        }
                        EditParam.InsertCollaborateCenters = InsertCollaCenters.ToArray();
                    }
                    EditParam.RecID_DeleteAdditonalInfo = model.xID_GSR;
                    if (model.InsertAdditonalinfo_GSR == true)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertAdditionalInfo_ServiceReport Inaddtional = new Common_Lib.RealTimeService.Parameter_InsertAdditionalInfo_ServiceReport();
                        Inaddtional.OrganizedFor = model.Look_OrganisedFor_GSR;
                        Inaddtional.ParticipantCategory = model.Look_ParticipantCategory_GSR;
                        Inaddtional.ParticipantSubCategory = model.Look_ParticipantSubCategory_GSR;
                        Inaddtional.ParticipantType = model.Look_ParticipantType_GSR;
                        Inaddtional.ProgramCoordinator_1 = model.Look_ProgramCoordinator1_GSR;
                        Inaddtional.ProgramCoordinator_2 = model.Look_ProgramCoordinator2_GSR;
                        Inaddtional.ProgramLocationType = model.Look_ProgramLocationType_GSR;
                        Inaddtional.ProgramType = model.Look_ProgramType_GSR;
                        Inaddtional.Rec_ID = System.Guid.NewGuid().ToString();
                        Inaddtional.Sr_ID = model.xID_GSR;

                        EditParam.param_InsertAdditonalInfo = Inaddtional;
                    }
                    EditParam.RecID_DeleteVenueType = model.xID_GSR;
                    if (string.IsNullOrWhiteSpace(model.Look_ProgVenueType_GSR) == false)
                    {
                        List<Parameter_InsertVenueType_ServiceReport> InProgramVenueType = new List<Parameter_InsertVenueType_ServiceReport>();
                        var venueType = model.Look_ProgVenueType_GSR.Split(',');
                        for (int i = 0; i < venueType.Length; i++)
                        {
                            Parameter_InsertVenueType_ServiceReport row = new Parameter_InsertVenueType_ServiceReport();
                            row.Sr_ID = model.xID_GSR;
                            row.Venue_misc_id = venueType[i];
                            row.Rec_ID = Guid.NewGuid().ToString();
                            InProgramVenueType.Add(row);
                        }
                        EditParam.InsertVenueType = InProgramVenueType.ToArray();
                    }

                    EditParam.RecID_DeleteGuest = model.xID_GSR;

                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGst1 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGst1.Sr_ID = model.xID_GSR;
                    InGst1.Sr_No = 1;
                    InGst1.GusetName = model.Txt_GuestName_1_GSR ?? "";
                    InGst1.GuestDesignation = model.Txt_GuestDesig_1_GSR ?? "";
                    InGst1.Rec_ID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertGuest1 = InGst1;
                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGst2 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGst2.Sr_ID = model.xID_GSR;
                    InGst2.Sr_No = 2;
                    InGst2.GusetName = model.Txt_GuestName_2_GSR ?? "";
                    InGst2.GuestDesignation = model.Txt_GuestDesig_2_GSR ?? "";
                    InGst2.Rec_ID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertGuest2 = InGst2;
                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGst3 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGst3.Sr_ID = model.xID_GSR;
                    InGst3.Sr_No = 3;
                    InGst3.GusetName = model.Txt_GuestName_3_GSR ?? "";
                    InGst3.GuestDesignation = model.Txt_GuestDesig_3_GSR ?? "";
                    InGst3.Rec_ID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertGuest3 = InGst3;
                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGst4 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGst4.Sr_ID = model.xID_GSR;
                    InGst4.Sr_No = 4;
                    InGst4.GusetName = model.Txt_GuestName_4_GSR ?? "";
                    InGst4.GuestDesignation = model.Txt_GuestDesig_4_GSR ?? "";
                    InGst4.Rec_ID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertGuest4 = InGst4;
                    Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport InGst5 = new Common_Lib.RealTimeService.Parameter_InsertGuest_ServiceReport();
                    InGst5.Sr_ID = model.xID_GSR;
                    InGst5.Sr_No = 5;
                    InGst5.GusetName = model.Txt_GuestName_5_GSR ?? "";
                    InGst5.GuestDesignation = model.Txt_GuestDesig_5_GSR ?? "";
                    InGst5.Rec_ID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertGuest5 = InGst5;

                    if (!BASE._SR_DBOps.UpdateServiceReport_Txn(EditParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //After successfully insertion of the service Report Details, we are now uploading attachments from here onwards.
                        //Files Upload part starts from here onwards
                        if (string.IsNullOrWhiteSpace(model.attachment_Window.Help_Document_FileName) == false)
                        {
                            // if (model.attachment_Window.Help_File_caption!=null && model.attachment_Window.Help_File_caption.Count > 0)//For NEWLY ENTERED CAPTIONS/maybe RATINGS will also be there
                            // {
                            Attachment_Save(model.attachment_Window,ClientScreen.Facility_ServiceReport);
                            //  }
                        }
                            if (model.attachment_Window.Help_File_Old_captions!=null && model.attachment_Window.Help_File_Old_captions.Count > 0 && model.attachment_Window.Help_File_Old_Ratings != null && model.attachment_Window.Help_File_Old_Ratings.Count > 0)//To update Old Ratings and Captions
                            {
                                for (int i = 0; i < model.attachment_Window.Help_File_Old_captions.Count; i++)
                                {
                                    if(!BASE._Attachments_DBOps.Update_Attachment_Caption_And_CentreRating
                                       (
                                        model.attachment_Window.Help_File_Old_Attachment_IDs[i], model.attachment_Window.Help_File_Old_captions[i], model.attachment_Window.Help_File_Old_Ratings[i])
                                      )
                                    {
                                        jsonParam.message = "Error while updating existing old Attachments Details!!";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                      //  }
                        // addding files to database ends here               
                        jsonParam.message = Common_Lib.Messages.UpdateSuccess;
                        jsonParam.title = model.TitleX_GSR;
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam, xID = model.xID_GSR }, JsonRequestBehavior.AllowGet);
                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Delete_ServiceReport DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_ServiceReport();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)  //'DELETE
                {
                    DelParam.RecID_DeleteProgOccasion = model.xID_GSR;
                    DelParam.RecID_DeleteProgType= model.xID_GSR;
                    DelParam.RecID_DeleteCollaborateCenters= model.xID_GSR;
                    DelParam.RecID_DeleteAcitvity = model.xID_GSR;
                    DelParam.RecID_DeleteWing = model.xID_GSR;
                    DelParam.RecID_DeleteGuest = model.xID_GSR;
                    DelParam.RecID_DeleteAttachments = model.xID_GSR;
                    DelParam.RecID_DeleteAdditonalInfo = model.xID_GSR;
                    DelParam.RecID_DeleteVenueType = model.xID_GSR;
                    DelParam.RecID_Delete = model.xID_GSR;

                    if (!BASE._SR_DBOps.DeleteServiceReport_Txn(DelParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX_GSR;
                    jsonParam.result = true;
                    jsonParam.refreshgrid = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_Service_Report_ActivityDetail(string Activity) 
        {
            return View("Frm_Service_Report_ActivityDetail", model:Activity);
        }
        public List<ProjectNameList> LookUp_Get_ProjectList(string Remark2Filter="")
        {
            if (string.Equals(Remark2Filter, "medicalwing", StringComparison.OrdinalIgnoreCase)==false) 
            {
                Remark2Filter = "";
            }           
            DataTable d1 = BASE._SR_DBOps.GetProjects("Name", "ID", Remark2Filter);
            return DatatableToModel.DataTabletoProjectNameList(d1);
        }
        public List<ProjectNameList> LookUp_Get_MasterProjectList(string Remark2Filter = "")
        {
            DataTable d1 = BASE._SR_DBOps.GetMasterProjects("Name", "ID", Remark2Filter);
            return DatatableToModel.DataTabletoProjectNameList(d1);
        }
        public List<WingsList> Get_WingsList()
        {
            DataTable d1 = BASE._SR_DBOps.GetWings();
            return DatatableToModel.DataTabletoGSR_GetWingsList(d1);
        }
        public ActionResult LookUp_Get_TreePlantList(DataSourceLoadOptions loadOptions)
        {        
            return Content(JsonConvert.SerializeObject(BASE._SR_DBOps.GetTreeDetails()), "application/json");
        }
        public List<ProgramTypeList> GetProgramType() 
        {
            return DatatableToModel.DataTabletoProgramTypeList(BASE._SR_DBOps.GetProgramType());
        }        
        public List<ProgramOccasionList> GetProgramOccasion()
        {
            return DatatableToModel.DataTabletoProgramOccasionList(BASE._SR_DBOps.GetProgramOccasion());
        }
        public List<ProgramOrganiserNames> GetOrganiserNamesList()
        {
            DataTable dt = BASE._SR_DBOps.GetOrganiserNames(BASE._open_Cen_ID);
            return DatatableToModel.DataTabletoProgramOrganisersList(dt);
        }
        public JArray GetProgramVenueTypes() 
        {
           return JArray.Parse(JsonConvert.SerializeObject(BASE._SR_DBOps.GetProgramVenueType()));
              //  return Content(JsonConvert.SerializeObject(BASE._SR_DBOps.GetProgramVenueType()), "application/json");
        }
        public ActionResult Frm_Reporter_Window(string sender="", string Mobile = "", string Email = "", string CenID = "", string cen_name = "",string projId = "", string wingId = "")
        {
            //ViewBag.CentreList = GetCentre();
            ViewBag.sender = sender;
            ViewBag.Mobile = Mobile;
            ViewBag.CenID = CenID;
            ViewBag.Email = Email;
            ViewBag.cen_name = cen_name;
            ViewBag.projId = projId;
            ViewBag.wingId = wingId;
            return View();
        }
        public ActionResult Get_Ins_List(DataSourceLoadOptions loadOptions, string cen_bk_pad_no, string cen_name)
        {
            if (string.IsNullOrWhiteSpace(cen_name) == false)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTabletoGSR_InsList(BASE._SR_DBOps.GetInstitutesOfCentre(cen_name, cen_bk_pad_no)), loadOptions)), "application/json");
            }
            else 
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<InsList>(), loadOptions)), "application/json");
            }
        }
        public ActionResult Get_Centre_List(DataSourceLoadOptions loadOptions)
        {      
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetCentre(), loadOptions)), "application/json");            
        }
        public ActionResult GetParticipantCategory(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(BASE._SR_DBOps.GetParticipantCategory()), loadOptions)), "application/json");
        }
        public ActionResult GetParticipantSubCategory(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(BASE._SR_DBOps.GetParticipantSubCategory()), loadOptions)), "application/json");
        }
        public ActionResult GetProgramType_Additional(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(BASE._SR_DBOps.GetProgramType_Additional()), loadOptions)), "application/json");
        }
        public ActionResult GetProgramLocationType(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(BASE._SR_DBOps.GetProgramLocationType()), loadOptions)), "application/json");
        }
        public ActionResult GetParties(DataSourceLoadOptions loadOptions,bool OnlyFaculty=false,bool OnlyEventOrganizer=false )
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_PartyList(BASE._SR_DBOps.GetParties(OnlyFaculty, OnlyEventOrganizer)), loadOptions)), "application/json");
        }       
        public ActionResult Frm_Service_Report_Response(string sender = "", string RecID ="",string Mobile="",string Email="",string CenID="",string cen_name = "", string projId = "", string wingId = "")
        {
            ViewBag.sender = sender;
            ViewBag.RecID = RecID;
            ViewBag.Mobile = Mobile;
            ViewBag.CenID = CenID;
            ViewBag.Email = Email;
            ViewBag.cen_name = cen_name;
            ViewBag.projId = projId;
            ViewBag.wingId = wingId;
            return View();
        }
        public string GetDocumentNameID()
        {
            DataTable d1 = BASE._SR_DBOps.GetDocumentNameID();
            return d1.Rows[0]["rec_id"].ToString();
        }
        public List<CentreList> GetCentre()
        {
            DataTable d1 = BASE._SR_DBOps.GetCentres();
            return DatatableToModel.DataTabletoGSR_CentreList(d1);
        }
        public ActionResult Frm_Service_Event_Details(string RecID,string sender="")
        {
            return new RedirectResult("https://services.brahmakumaris.com/Events/Events/Frm_Service_Event_Details?RecID="+RecID);

            //ServiceEventDetails model = new ServiceEventDetails();
            //DataTable d1 = BASE._SR_DBOps.GetServiceEventDetails(RecID);
            //model.Sender = sender;
            //model.RecID = RecID;
            //model.CenID= d1.Rows[0]["SR_CEN_ID"].ToString();
            //model.LastEditOn= Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            //model.ProjName = d1.Rows[0]["MISC_NAME"].ToString();
            //model.ProgName = d1.Rows[0]["SR_PROG_NAME"].ToString();
            //model.Title = model.ProgName + " ( " + model.ProjName + " )";
            //if (Convert.IsDBNull(d1.Rows[0]["SR_RATING"]) == false)
            //{
            //    model.StarRating = Convert.ToInt32(d1.Rows[0]["SR_RATING"]);
            //}           
            //DateTime FromDate = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_DATE"]);
            //DateTime ToDate = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_DATE"]);
            //string ProgFromDate = FromDate.ToString("MMM dd, yyyy");
            //string ProgToDate = ToDate.ToString("MMM dd, yyyy");
            //if (FromDate.Month == ToDate.Month)
            //{
            //    ProgToDate = ToDate.ToString("dd, yyyy");
            //}
            //if (FromDate.Year == ToDate.Year)
            //{
            //    ProgFromDate = FromDate.ToString("MMM dd");
            //}
            //model.FromToDate = ProgFromDate + " - " + ProgToDate;
            //model.FromToTime = d1.Rows[0]["SR_PROG_FR_TIME"].ToString() + " To " + d1.Rows[0]["SR_PROG_TO_TIME"].ToString();
            //model.Organizer = d1.Rows[0]["INS_NAME"].ToString();
            //if (d1.Rows[0]["SR_PROG_TYPE"].ToString() != "0")
            //{
            //    model.Organizer += " ( ";
            //    DataTable wingData = BASE._SR_DBOps.GetWingsRecord(RecID);
            //    int count = wingData.Rows.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (i == count - 1)
            //        {
            //            model.Wings += wingData.Rows[i]["WING_NAME"].ToString();
            //        }
            //        else
            //        {
            //            model.Wings += wingData.Rows[i]["WING_NAME"].ToString() + ", ";
            //        }
            //    }
            //    model.Organizer += model.Wings + " )";
            //}
            //model.Speaker = d1.Rows[0]["SR_PROG_SPEAKER"].ToString();
            //model.Speaker = model.Speaker.FillNullOrEmptyString();
            //model.SubjectTopicTheme = model.Title;
            //model.Category = d1.Rows[0]["SR_PROG_CATEGORY"].ToString();
            //model.Category = model.Category.FillNullOrEmptyString();
            //model.AudienceType = "--";
            //model.CenterPhone = d1.Rows[0]["CENTRE_PHONENO"].ToString();
            //model.CenterPhone = model.CenterPhone.FillNullOrEmptyString();
            //model.CenterEmail = d1.Rows[0]["CENTRE_EMAILID"].ToString();
            //model.CenterEmail = model.CenterEmail.FillNullOrEmptyString();
            //model.Links = d1.Rows[0]["SR_PROG_NEWS_LINK"].ToString();
            //model.Links = model.Links.FillNullOrEmptyString();
            //model.Venue = d1.Rows[0]["SR_PROG_VENUE"].ToString();
            //model.Venue = model.Venue.FillNullOrEmptyString();
            //model.CenterName = d1.Rows[0]["CENTRE_NAME"].ToString();
            //model.CenterName = model.CenterName.FillNullOrEmptyString();

            //List<Attachment_List> Attachments = BASE._Attachments_DBOps.GetList(RecID, d1.Rows[0]["SR_CEN_ID"].ToString());
            //if (Attachments.Count > 0)
            //{
            //    var Banner = Attachments.Find(x => x.Description.Contains("Program Banner"));
            //    if (Banner == null)
            //    {
            //        Banner = Attachments.Find(x => x.Description.Contains("Program Pictures"));
            //    }
            //    if (Banner != null)
            //    {
            //        model.BannerPath = GetAttachmentPath(Banner.ID, Banner.File_Name);                    
            //    }
            //    var count = Attachments.Count;
            //    model.AttachmentPaths = new List<string>();
            //    for (int i = 0; i < count; i++) 
            //    {
            //        //if (MimeMapping.GetMimeMapping(Attachments[i].File_Name).Contains("image"))
            //       // {
            //            model.AttachmentPaths.Add(GetAttachmentPath(Attachments[i].ID, Attachments[i].File_Name));
            //       // }
            //    }
            //}     
            ////model.Txt_ProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_PROG_BENEFIT"]);
            //model.Brief = d1.Rows[0]["SR_PROG_BRIEF"].ToString();
            //model.SplMoment = d1.Rows[0]["SR_PROG_SPL"].ToString();
            //model.FollowUp = d1.Rows[0]["SR_PROG_FOLLOWUP"].ToString();         
            //model.Feedback = d1.Rows[0]["SR_PROG_FEEDBACK"].ToString();        
            //model.Testimonials = d1.Rows[0]["SR_PROG_VVIP_TESTIMONIAL"].ToString();
            //model.Cultural = d1.Rows[0]["SR_PROG_CULTURAL"].ToString();
            ////model.Txt_MediaLink_GSR = d1.Rows[0]["SR_MEDIA_LINK"].ToString();
            //DataTable d3 = BASE._SR_DBOps.GetGuestRecord(RecID);
            //string guest;
            //string designation;
            //if (d3.Rows.Count > 0)
            //{
            //    guest = d3.Rows[0]["SR_GUEST_NAME"].ToString();
            //    designation = d3.Rows[0]["SR_GUEST_DESIG"].ToString();
            //    if (string.IsNullOrEmpty(guest) == false) 
            //    {
            //        model.Guests = guest;
            //    }
            //    if (string.IsNullOrEmpty(designation) == false)
            //    {
            //        model.Guests += " - " + designation;
            //    }             
            //}
            //if (d3.Rows.Count > 1)
            //{
            //    guest = d3.Rows[1]["SR_GUEST_NAME"].ToString();
            //    designation = d3.Rows[1]["SR_GUEST_DESIG"].ToString();
            //    if (string.IsNullOrEmpty(guest) == false)
            //    {
            //        model.Guests +="!["+ guest;
            //    }
            //    if (string.IsNullOrEmpty(designation) == false)
            //    {
            //        model.Guests += " - " + designation;
            //    }              
            //}
            //if (d3.Rows.Count > 2)
            //{
            //    guest = d3.Rows[2]["SR_GUEST_NAME"].ToString();
            //    designation = d3.Rows[2]["SR_GUEST_DESIG"].ToString();
            //    if (string.IsNullOrEmpty(guest) == false)
            //    {
            //        model.Guests += "![" + guest;
            //    }
            //    if (string.IsNullOrEmpty(designation) == false)
            //    {
            //        model.Guests += " - " + designation;
            //    }              
            //}
            //if (d3.Rows.Count > 3)
            //{
            //    guest = d3.Rows[3]["SR_GUEST_NAME"].ToString();
            //    designation = d3.Rows[3]["SR_GUEST_DESIG"].ToString();
            //    if (string.IsNullOrEmpty(guest) == false)
            //    {
            //        model.Guests +="!["+ guest;
            //    }
            //    if (string.IsNullOrEmpty(designation) == false) 
            //    {
            //        model.Guests += " - " + designation;
            //    }             
            //}
            //if (d3.Rows.Count > 4)
            //{
            //    guest = d3.Rows[4]["SR_GUEST_NAME"].ToString();
            //    designation = d3.Rows[4]["SR_GUEST_DESIG"].ToString();
            //    if (string.IsNullOrEmpty(guest) == false)
            //    {
            //        model.Guests +="!["+ guest;
            //    }
            //    if (string.IsNullOrEmpty(designation) == false)
            //    {
            //        model.Guests += " - " + designation;
            //    }              
            //}
            //return View(model);
        }
        //public string GetAttachmentPath(string AttachmentID, string FileName) 
        //{
        //    var CustomFileName = "";
        //    var filename = FileName.Split('.');
        //    for (var i = 0; i < filename.Length; i++)
        //    {
        //        if (i == 0)
        //        {
        //            CustomFileName = AttachmentID;
        //        }
        //        if ((i == (filename.Length - 1)) && i != 0)
        //        {
        //            CustomFileName = CustomFileName + "." + filename[i];
        //        }
        //    }
        //    return Request.Url.Scheme + "://" + Request.Url.Authority + "/Attachments/" + CustomFileName;
        //    //return "https://Connectonedev.bkivv.app/Attachments/" + CustomFileName;
        //}
        public void ServiceReport_UserRights()
        {
            ViewData["Service_Report_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceReport, "Export");
            ViewData["Service_Report_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceReport, "List");
            ViewData["Service_Report_AddRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceReport, "Add");
            ViewData["Service_Report_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceReport, "Update");
            ViewData["Service_Report_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceReport, "Delete");
            ViewData["Service_Report_ReportRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceReport, "Report");

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");//attachment
        }
        public void SessionClear()
        {
            ClearBaseSession("_SerRpt");
            Session.Remove("ServiceReportInfo_DetailGrid_Data");
        }
        public void SessionClear_Window() 
        {
            ClearBaseSession("_ServiceRpt_Window");
        }
        private bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public ActionResult GetTreePlantationDetails(DataSourceLoadOptions loadOptions)
        {
            if (TreePlantationData == null) 
            {
                TreePlantationData = new List<TreePlantationDetails>();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(TreePlantationData, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult InsertTreePlantationDetails(string values)
        {
            TreePlantationDetails newTree = JsonConvert.DeserializeObject<TreePlantationDetails>(values);
            newTree.Srno = TreePlantationData.Count + 1;
            TreePlantationData.Add(newTree);
            return new EmptyResult();
        }        
        [HttpPut]
        public ActionResult UpdateTreePlantationDetails(string values,int key)
        {
            TreePlantationDetails newTree = JsonConvert.DeserializeObject<TreePlantationDetails>(values);
            var Tree = TreePlantationData.FirstOrDefault(x => x.Srno == key);
            if (Tree != null)
            {
                if (string.IsNullOrWhiteSpace(newTree.TreeMiscID) == false)
                {
                    Tree.TreeMiscID = newTree.TreeMiscID;
                }
                if (newTree.Count > 0)
                {
                    Tree.Count = newTree.Count;
                }
            }
            return new EmptyResult();
        }
        [HttpDelete]
        public ActionResult DeleteTreePlantationDetails(int key)
        {
            var item = TreePlantationData.SingleOrDefault(x => x.Srno == key);
            if (item != null)
            {
                TreePlantationData.Remove(item);
            }
            for (int i = 0; i < TreePlantationData.Count; i++)
            {
                TreePlantationData[i].Srno = i + 1;
            }
            return new EmptyResult();
        }
        [HttpPost]
        public JsonResult CheckUniqueTreeType(string ID)
        {         
            var Tree = TreePlantationData.FirstOrDefault(x => x.TreeMiscID == ID);
            if (Tree == null)
            {
                return Json(true);
            }
            else 
            {
                return Json(false);
            }           
        }
        public ActionResult SendOtp(string Mobile) 
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string message = "";
                if (BASE._Notifications_DBOps.SendSMS(null, "MobileVerification", ref message, null, null, Mobile))
                {
                    jsonParam.message = message;
                    jsonParam.title = "Information..";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    jsonParam.message = Messages.SomeError+"<br>"+message;
                    jsonParam.title = "Information..";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VerifyOtp(string Mobile,string OTP) 
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try 
            {
                if (BASE._SR_DBOps.UpdateMobileVerificationStatus(Mobile, OTP))
                {
                    jsonParam.message = "Mobile Number Has Been Verified";
                    jsonParam.title = "Success..";
                    jsonParam.result = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information..";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e) 
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ServiceReportEmailer()
        {
            return View();
        }
        public ActionResult Attachments_Window(string ActionMethod = "_New", string xID = "")
        {
            return View();
        }
        public ActionResult ResendServiceReportEmailer(string GSR_ID = "")
        {
            DataTable Emailerdata = BASE._SR_DBOps.GetDataForServiceReportEmail(GSR_ID);
            string EmailMessage = "";
            if (Emailerdata != null && Emailerdata.Rows.Count > 0)
            {
                SendHTMLEmail(ref EmailMessage, Emailerdata.Rows[0]["CENTER_EMAIL"].ToString(), Emailerdata.Rows[0]["SUBJECT"].ToString(), "~/Areas/Facility/Views/ServiceReport/ServiceReportEmailer.cshtml", Emailerdata, true, "", Emailerdata.Rows[0]["CC"].ToString(), Emailerdata.Rows[0]["BCC"].ToString());
                ViewBag.Message = EmailMessage;
            }
            else { ViewBag.Message = "Data Not Found!!"; }
            return View();
        }
    #region Events Management

    [CheckLogin]
        public ActionResult Frm_Events_Mgt_Info()
        {
            ServiceReport_UserRights();
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            //string attachmentRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["thumbnailpath"];// Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            //string attachmentRootPath = Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            string attachmentRootPath = "https://Connectonedev.bkivv.app/Attachments/";
            string event_stataus = "DRAFT";
            if (CheckRights(BASE, ClientScreen.Facility_ServiceReport, "List"))
            {
                //ViewBag.ShowHorizontalBar = 0;
                //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;

                DT_Draft = BASE._SR_DBOps.Get_SM_EventsMgtList(event_stataus, attachmentRootPath);
                //ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                //ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                //ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                //ViewData["ServiceReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                //    || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                
                
                return View(DT_Draft);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
            }
        }

        public ActionResult Draft_Events()
        {

            return View();
        }
        public ActionResult Upcoming_Events()
        {

            return View();
        }
        public ActionResult Past_Events()
        {

            return View();
        }
        public ActionResult Cancelled_Events()
        {

            return View();
        }

        public ActionResult Frm_Draft_Events_Grid(string command)
        {
            string event_stataus = "Draft";
            //string attachmentRootPath = Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            string attachmentRootPath = "https://Connectonedev.bkivv.app/Attachments/";
            if (command == null)
            {

            }
            else if (command == "Refresh")
            {
                DT_Draft = BASE._SR_DBOps.Get_SM_EventsMgtList(event_stataus, attachmentRootPath);
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;
            return View(DT_Draft);
        }
        public ActionResult Frm_Upcoming_Events_Grid(string command)
        {
            string event_stataus = "Upcoming";
            //string attachmentRootPath = Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            string attachmentRootPath = "https://Connectonedev.bkivv.app/Attachments/";
            if (command == null){
                
            }
            else if(command == "Refresh"){
                DT_Draft = BASE._SR_DBOps.Get_SM_EventsMgtList(event_stataus, attachmentRootPath);
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;
            return View(DT_Draft);
        }
        public ActionResult Frm_Past_Events_Grid(string command)
        {
            string event_stataus = "Past";
            //string attachmentRootPath = Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            string attachmentRootPath = "https://Connectonedev.bkivv.app/Attachments/";
            if (command == null){
                
            }
            else if(command == "Refresh"){
                DT_Draft = BASE._SR_DBOps.Get_SM_EventsMgtList(event_stataus, attachmentRootPath);
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;
            return View(DT_Draft);
        }
        public ActionResult Frm_Cancelled_Events_Grid(string command)
        {
            string event_stataus = "Cancelled";
            //string attachmentRootPath = Request.Url.Scheme + "://" + Request.Url.Host + "/Attachments/";
            string attachmentRootPath = "https://Connectonedev.bkivv.app/Attachments/";
            if (command == null){
                
            }
            else if(command == "Refresh"){
                DT_Draft = BASE._SR_DBOps.Get_SM_EventsMgtList(event_stataus, attachmentRootPath);
            }
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;
            return View(DT_Draft);
        }

        #endregion
        public ActionResult EventCreation(string ActionMethod = "_New", string xID = "", string Info_LastEditedOn = null, string PopupName = "Dynamic_Content_popup", string GridToRefresh = "ServiceReportListGrid", string CenID = null, string ReporterEmail = null, string ReporterMobile = null, string SessionGUID = null, string FY = "", string sender = "")
        {
            if (ActionMethod != "_New" && string.IsNullOrWhiteSpace(BASE._open_User_ID))
            {
                string defaultpath = System.Configuration.ConfigurationManager.AppSettings["DefaultPath"];
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", defaultpath.Split('/')[1]);
                redirectTargetDictionary.Add("action", defaultpath.Split('/')[3]);
                redirectTargetDictionary.Add("controller", defaultpath.Split('/')[2]);
                return new RedirectToRouteResult(redirectTargetDictionary);
            }
            if (string.IsNullOrWhiteSpace(CenID) == false)
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(CenID);
                BASE._open_User_ID = CenID;
                BASE._open_Year_ID = Convert.ToInt32(FY);
            }
            TreePlantationData = new List<TreePlantationDetails>();
            ServiceReport_ViewModel model = new ServiceReport_ViewModel();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            model.ActionMethod = model.Tag.ToString();
            model.TitleX_GSR = "Godly Service Report";
            model.xID_GSR = xID ?? "";
            model.PopupName_GSR = PopupName != null ? PopupName : "Dynamic_Content_popup";
            model.MasterProjectNameList = LookUp_Get_MasterProjectList();
            model.ProjectNameList = LookUp_Get_ProjectList(string.Equals(sender, "amritmahotsav", StringComparison.OrdinalIgnoreCase) ? "" : sender);
            model.WingListData = Get_WingsList();
            model.DocumentNameID = GetDocumentNameID();
            model.ProgType_Data = GetProgramType();
            model.ProgOccasion_Data = GetProgramOccasion();
            model.ReporterCenID = CenID;
            model.ReporterEmail = ReporterEmail;
            model.ReporterMobile = ReporterMobile;
            model.Sender = sender;
            model.Txt_NoOfEvents_GSR = 1;
            if (string.IsNullOrWhiteSpace(CenID) == false)
            {
                if (model.MasterProjectNameList.Count > 0)
                {
                    if (string.Equals(model.Sender, "amritmahotsav", StringComparison.OrdinalIgnoreCase))
                    {
                        var obj = model.MasterProjectNameList.Find(x => x.Name == "Azadi Ka Amrit Mahotsav");
                        if (obj != null)
                        {
                            model.Look_MasterProject_GSR = obj.ID;
                        }
                    }
                    else
                    {
                        var obj = model.MasterProjectNameList.Find(x => x.Name == "General");
                        if (obj != null)
                        {
                            model.Look_MasterProject_GSR = obj.ID;
                        }
                    }
                }
                if (model.Tag == Common.Navigation_Mode._New)
                {
                    if (string.IsNullOrWhiteSpace(model.Sender) == false)
                    {
                        int count = model.WingListData.Count;
                        int i;
                        for (i = 0; i < count; i++)
                        {
                            if (string.Equals(model.WingListData[i].Name.Trim().Replace(" ", ""), model.Sender.Trim().Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
                            {
                                model.PreSelectedWing = model.WingListData[i].ID;
                                model.Chk_WingBasedFlag_GSR = "1";
                                break;
                            }
                        }
                    }
                }
            }
            if (model.Tag == Common.Navigation_Mode._New)
            {
                model.xID_GSR = Guid.NewGuid().ToString();
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d1 = BASE._SR_DBOps.GetRecord(model.xID_GSR);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    string message = Messages.RecordChanged("Current Service Report");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Changed / Removed in Background!!','" + GridToRefresh + "');</script>");
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                        {
                            string message = Messages.RecordChanged("Current Service Report");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                model.Look_ProjList_GSR = d1.Rows[0]["SR_PROJ_ID"].ToString();
                model.Look_MasterProject_GSR = d1.Rows[0]["SR_MASTER_PROJ_ID"].ToString();
                model.Chk_WingBasedFlag_GSR = d1.Rows[0]["SR_PROG_TYPE"].ToString();

                model.Txt_ProgName_GSR = d1.Rows[0]["SR_PROG_NAME"].ToString();
                model.Txt_ProgVenue_GSR = d1.Rows[0]["SR_PROG_VENUE"].ToString();

                model.DE_FR_DT_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_DATE"]);
                model.DE_TO_DT_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_DATE"]);

                model.Txt_Fr_Time_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_TIME"]);
                model.Txt_To_Time_GSR = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_TIME"]);
                model.Txt_TotalProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_PROG_BENEFIT"]);
                if (Convert.IsDBNull(d1.Rows[0]["SR_ONLINE_PROG_BENEFIT"]))
                {
                    model.Txt_OnlineProgBeneficiaries_GSR = 0;
                }
                else
                {
                    model.Txt_OnlineProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_ONLINE_PROG_BENEFIT"]);
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_OFFLINE_PROG_BENEFIT"]))
                {
                    model.Txt_ProgBeneficiaries_GSR = 0;
                }
                else
                {
                    model.Txt_ProgBeneficiaries_GSR = Convert.ToInt32(d1.Rows[0]["SR_OFFLINE_PROG_BENEFIT"]);
                }
                if (model.Txt_ProgBeneficiaries_GSR == 0 && model.Txt_OnlineProgBeneficiaries_GSR == 0)
                {
                    model.Txt_ProgBeneficiaries_GSR = model.Txt_TotalProgBeneficiaries_GSR;
                }
                if (Convert.IsDBNull(d1.Rows[0]["SR_NO_OF_EVENT"]))
                {
                    model.Txt_NoOfEvents_GSR = 1;
                }
                else
                {
                    model.Txt_NoOfEvents_GSR = Convert.ToInt32(d1.Rows[0]["SR_NO_OF_EVENT"]);
                }
                model.Txt_ProgBrief_GSR = d1.Rows[0]["SR_PROG_BRIEF"].ToString() ?? "";
                model.Txt_ProgSpecialMom_GSR = d1.Rows[0]["SR_PROG_SPL"].ToString() ?? "";
                model.Txt_ProgFollowUp_GSR = d1.Rows[0]["SR_PROG_FOLLOWUP"].ToString() ?? "";
                model.Txt_ProgSpeaker_GSR = d1.Rows[0]["SR_PROG_SPEAKER"].ToString() ?? "";
                model.Txt_ProgFeedback_GSR = d1.Rows[0]["SR_PROG_FEEDBACK"].ToString() ?? "";

                model.Look_ProgType_GSR = d1.Rows[0]["SR_PROG_CATEGORY"].ToString();
                model.Txt_NewsLink_GSR = d1.Rows[0]["SR_PROG_NEWS_LINK"].ToString();
                model.Txt_VVIPTestimonial_GSR = d1.Rows[0]["SR_PROG_VVIP_TESTIMONIAL"].ToString();
                model.Txt_CulturalProg_GSR = d1.Rows[0]["SR_PROG_CULTURAL"].ToString();
                model.Txt_MediaLink_GSR = d1.Rows[0]["SR_MEDIA_LINK"].ToString();

                model.Wings_List_GSR = new List<WingsList>();
                foreach (DataRow xRow in BASE._SR_DBOps.GetWingsRecord(model.xID_GSR).Rows)
                {
                    model.Wings_List_GSR.Add(new Models.WingsList()
                    {
                        ID = xRow["SR_WING_ID"].ToString()
                    });
                }

                DataTable d3 = BASE._SR_DBOps.GetGuestRecord(model.xID_GSR);
                if (d3.Rows.Count > 0)
                {
                    model.Txt_GuestName_1_GSR = d3.Rows[0]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_1_GSR = d3.Rows[0]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 1)
                {
                    model.Txt_GuestName_2_GSR = d3.Rows[1]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_2_GSR = d3.Rows[1]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 2)
                {
                    model.Txt_GuestName_3_GSR = d3.Rows[2]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_3_GSR = d3.Rows[2]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 3)
                {
                    model.Txt_GuestName_4_GSR = d3.Rows[3]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_4_GSR = d3.Rows[3]["SR_GUEST_DESIG"].ToString();
                }
                if (d3.Rows.Count > 4)
                {
                    model.Txt_GuestName_5_GSR = d3.Rows[4]["SR_GUEST_NAME"].ToString();
                    model.Txt_GuestDesig_5_GSR = d3.Rows[4]["SR_GUEST_DESIG"].ToString();
                }
                DataTable d4 = BASE._SR_DBOps.GetActivityRecord(model.xID_GSR);
                if (d4 != null && d4.Rows.Count > 0)
                {
                    int count = 1;
                    foreach (DataRow row in d4.Rows)
                    {
                        if (row["SR_ACTIVITY_NAME"].ToString() == "Deaddiction")
                        {
                            model.Txt_DeaddictionPledgeCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else if (row["SR_ACTIVITY_NAME"].ToString() == "Blood Donation")
                        {
                            model.Txt_BloodDonationUnitsCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else if (row["SR_ACTIVITY_NAME"].ToString() == "Awards Received")
                        {
                            model.Txt_AwardsReceivedCount_GSR = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                        }
                        else
                        {
                            TreePlantationDetails newTree = new TreePlantationDetails();
                            newTree.Srno = count;
                            newTree.TreeMiscID = row["SR_ACTIVITY_DETAIL_MISC_ID"].ToString();
                            newTree.Count = Convert.ToInt32(row["SR_ACTIVITY_COUNT"]);
                            TreePlantationData.Add(newTree);
                            count++;
                        }
                    }
                    var distinctDt = d4.DefaultView.ToTable(true, "SR_ACTIVITY_NAME");
                    model.SelectedActivities_GSR = String.Join("![", distinctDt.AsEnumerable().Select(r => r.Field<string>("SR_ACTIVITY_NAME")));
                }
                DataTable d5 = BASE._SR_DBOps.GetProgramTypeRecord(model.xID_GSR);
                if (d5 != null && d5.Rows.Count > 0)
                {
                    model.ProgType_ID_GSR = String.Join("![", d5.AsEnumerable().Select(r => r.Field<string>("SR_PROGRAM_TYPE_ID")));
                }
                DataTable d6 = BASE._SR_DBOps.GetProgramOccasionRecord(model.xID_GSR);
                if (d6 != null && d6.Rows.Count > 0)
                {
                    model.Look_ProgOccasion_GSR = String.Join("![", d6.AsEnumerable().Select(r => r.Field<string>("SR_PROGRAM_OCCASION_ID")));
                }
                List<Attachment_List> Attachments = BASE._Attachments_DBOps.GetList(model.xID_GSR);
                if (Attachments != null && Attachments.Count > 0)
                {
                    List<Attachment_List> files = Attachments.FindAll(x => x.Description.Contains("Program Banner"));
                    if (files != null && files.Count > 0)
                    {
                        model.ProgramBanner_GSR = string.Join(", ", files.Select(x => x.File_Name));
                        Attachments.Remove(x => x.Description.Contains("Program Banner"));
                    }
                    files = Attachments.FindAll(x => x.Description.Contains("Program Promo"));
                    if (files != null && files.Count > 0)
                    {
                        model.ProgramPromo_GSR = string.Join(", ", files.Select(x => x.File_Name));
                        Attachments.Remove(x => x.Description.Contains("Program Promo"));
                    }
                    files = Attachments.FindAll(x => x.Description.Contains("Program Press Release"));
                    if (files != null && files.Count > 0)
                    {
                        model.ProgramPressRelease_GSR = string.Join(", ", files.Select(x => x.File_Name));
                        Attachments.Remove(x => x.Description.Contains("Program Press Release"));
                    }
                    // files = Attachments.FindAll(x => x.Description.Contains("Program Pictures") || x.Description.Contains("Program Videos"));
                    if (Attachments != null && Attachments.Count > 0)
                    {
                        model.ProgramPictures_GSR = string.Join(", ", Attachments.Select(x => x.File_Name));
                    }
                }
            }
            model.SelectedActivities_GSR = model.SelectedActivities_GSR ?? "";
            model.ProgType_ID_GSR = model.ProgType_ID_GSR ?? "";
            model.Look_ProgOccasion_GSR = model.Look_ProgOccasion_GSR ?? "";
            return View(model);
        }

        public ActionResult LookUp_Get_Centres(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_CentresList(BASE._Form_dbops.GetCentres(BASE._open_Ins_ID)), loadOptions)), "application/json");

        }
        public ActionResult LookUp_Get_Collaborate_Centres(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(BASE._CenterDBOps.GetCentre_BKPadNo_Inst(BASE._open_Ins_ID)), "application/json");
        }





        #region DevExtreme
        [CheckLogin]
        public ActionResult Frm_Service_Report_Info_Dx()
        {
            ServiceReport_UserRights();
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            if (CheckRights(BASE, ClientScreen.Facility_ServiceReport, "List"))
            {
                //ViewBag.ShowHorizontalBar = 0;
                ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceReport).ToString()) ? 1 : 0;

                //Grid_Display();
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                ViewData["ServiceReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                    || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
            }
        }

        [HttpGet]
        public ActionResult Frm_Service_Report_Info_Grid_Load(int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default")
        {
            Grid_Display();
            return Content(JsonConvert.SerializeObject(ServiceReport_ExportData), "application/json");
        }
        [HttpGet]
        public ActionResult Frm_Service_Report_Info_DetailGrid_Load(string RecID = "", string MID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Facility_ServiceReport)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceReport, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            }
            return PartialView();
        }
        #endregion

        public ActionResult Frm_ServiceReport_Generator()
        {
            ViewBag.centerName = BASE._open_Cen_Name;
            return View();
        }
        public ActionResult Generate_ServiceReport_Period(string fromdate, string todate, string locationtype)
        {
            if(locationtype == null)
            {
                DataTable dt2 = null;
                return Content(JsonConvert.SerializeObject(dt2), "application/json");
            }
            DateTime? startdate;
            DateTime? enddate;
            startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate);
            enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate);

            DataTable dt = BASE._SR_DBOps.generateServiceReportByPeriod(BASE._open_Cen_ID, startdate, enddate, locationtype);
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }

    }
}