//using Stimulsoft.Report;
using Common_Lib;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using ConnectOneMVC.Controllers;
using DevExtreme.AspNet.Mvc;
using DevExpress.Web.Mvc;
using ConnectOneMVC.Helper;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Areas.Magazine.Models;
using System.Web.UI.WebControls;
using System.Reflection;
//using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Magazine.Controllers
{
    public class MagazineDispatchRegisterController : BaseController
    {
        //'Membership List
        string Disp_Membership_ID = null;
        string Membership_ID = null;
        string Membership_Old_ID = null;
        string Issue_Date = null;
        string Magazine = null;
        public static string xID = "";
        public static string Issue = "";
        public static string Member = "";
        string IssueID = null;
        string MemberID = null;
        string ModeID = null;
        public static DataSet MM_DS = new DataSet();
        string DtCutOff = null;
        public static int Total_disp_copies = 0;
        public static int Total_subs_copies = 0;
        public static int Txt_Returned_Count = 0;
        public static int Txt_Net_Dispatched = 0;
        DataTable Dispatch_Detail = new DataTable();
        DataTable Deleted_Vouchers = new DataTable();
        // GET: Magazine/MagazineDispatchRegister
        public ActionResult Index()
        {
            ViewBag.ShowHorizontalBar = 0;
            return View();
        }
        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }

        public ActionResult Frm_Magazine_Dispatch_Reg_Info(string Cmb_ListBy = "", string Txt_Str = "", string Txt_Issue_Date = "",
            string GLookUp_MagList = "", string DtCutOff = "", string TimeCutOff = "")
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Magazine_Dispatch_Register, "List"))
            {
                if (DtCutOff != "" && DtCutOff != null)
                {
                    string[] date = DtCutOff.Split('/');
                    DtCutOff = date[1].ToString() + "-" + date[0].ToString() + "-" + date[2].ToString();
                }
                if (Cmb_ListBy != "" && Cmb_ListBy != null)
                {
                    string CutOff = null;
                    Magazine = GLookUp_MagList == "" ? null : GLookUp_MagList;
                    Disp_Membership_ID = Txt_Str == "" ? null : Txt_Str;
                    if (Cmb_ListBy == "MEMBER ID.")
                    {
                        Membership_ID = Txt_Str == "" ? null : Txt_Str;
                    }
                    if (Cmb_ListBy == "DISPATCH MEMBER ID")
                    {
                        Disp_Membership_ID = Txt_Str == "" ? null : Txt_Str;
                    }
                    if (Cmb_ListBy == "DISPATCH MEMBER ID")
                    {
                        Membership_Old_ID = Txt_Str == "" ? null : Txt_Str;
                    }
                    if (Cmb_ListBy == "MAGAZINE ISSUE DATE")
                    {
                        Issue_Date = Txt_Issue_Date == "" ? null : Txt_Issue_Date;
                    }
                    ViewBag.ShowHorizontalBar = 0;
                    if (GLookUp_MagList.Length > 0)
                    {
                        if (DtCutOff.Length > 0)
                        {
                            //CutOff = DtCutOff.DateTime.Date.Add(TimeCutOff.Time.TimeOfDay)
                            CutOff = DtCutOff;
                        }
                    }
                    DtCutOff = DtCutOff.Length > 0 ? CutOff : null;
                    MM_DS = BASE._Magazine_DBOps.GetList_MagazineDispatchRegister(Membership_ID, Membership_Old_ID, Issue_Date, Magazine, Disp_Membership_ID, Convert.ToDateTime(DtCutOff));
                    if (MM_DS == null)
                    {
                        return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["MM_DS"] = MM_DS;
                    }
                    DataRelation Dispatch_Detail = MM_DS.Relations.Add("Dispatch Detail", MM_DS.Tables[0].Columns["ISSUE_MEMBER"], MM_DS.Tables[1].Columns["ISSUE_MEMBER"], false);
                    List<MagazineDispatch_Info> Magazine_Dispatch_Info = new List<MagazineDispatch_Info>();
                    List<MagazineDispatch_Info> Magazine_Dispatch_Info_Details = new List<MagazineDispatch_Info>();
                    MM_DS.Tables[0].Columns["ISSUE_MEMBER"].ColumnMapping = MappingType.Hidden;
                    MM_DS.Tables[1].Columns["ISSUE_MEMBER"].ColumnMapping = MappingType.Hidden;
                    Magazine_Dispatch_Info = (from DataRow T in MM_DS.Tables[0].AsEnumerable()
                                              select new MagazineDispatch_Info
                                              {
                                                  Dispatch_Member_ID = T["Dispatch Member ID"].ToString(),
                                                  Member_ID = T["Member ID"].ToString(),
                                                  Member_Old_ID = T["Member Old ID"].ToString(),
                                                  Member = T["Member"].ToString(),
                                                  Magazine = T["Magazine"].ToString(),
                                                  Issue = T["Issue"].ToString(),
                                                  TotalCopies = T["Total Copies"].ToString(),
                                                  DispatchedCopies = T["Dispatched Copies"].ToString(),
                                                  Status = T["Status"].ToString(),
                                                  Address = T["Address"].ToString(),
                                                  Member_Status = T["Member Status"].ToString(),
                                                  MII_RPC_SEED = T["MII_RPC_SEED"].ToString(),
                                                  MII_COPY_WT = T["MII_COPY_WT"].ToString(),
                                                  MEM_CATEGORY = T["MEM_CATEGORY"].ToString(),
                                                  City = T["City"].ToString(),
                                                  State = T["State"].ToString(),
                                                  PinCode = T["PinCode"].ToString(),
                                                  Country = T["Country"].ToString(),
                                                  BUNDLE_WEIGHT = T["BUNDLE_WEIGHT"].ToString(),
                                                  MII_ISSUE_DATE = T["MII_ISSUE_DATE"].ToString(),
                                                  DISP_DONE_MODE = T["DISP_DONE_MODE"].ToString(),
                                                  Expiry_Status = T["Expiry Status"].ToString(),
                                                  MAG_REC_ID = T["MAG_REC_ID"].ToString(),
                                                  REGION = T["REGION"].ToString(),
                                                  RMS = T["RMS"].ToString(),
                                                  PSO = T["PSO"].ToString(),
                                                  CONTACT_NO = T["CONTACT_NO"].ToString(),
                                                  CURRTIME = T["CURRTIME"].ToString(),
                                                  MD_ID = T["MD_ID"].ToString(),
                                                  ISSUE_ID = T["ISSUE_ID"].ToString(),
                                                  MEM_ID = T["MEM_ID"].ToString(),
                                                  TR_ID = T["TR_ID"].ToString(),
                                                  NAME = T["NAME"].ToString(),
                                                  ADD1 = T["ADD1"].ToString(),
                                                  ADD2 = T["ADD2"].ToString(),
                                                  ADD3 = T["ADD3"].ToString(),
                                                  ADD4 = T["ADD4"].ToString(),
                                                  ADD5 = T["ADD5"].ToString(),
                                                  Mem_Issue_Info = T["Mem_Issue_Info"].ToString(),
                                                  Copies = T["Copies"].ToString(),
                                                  MODE = T["MODE"].ToString(),
                                                  MODE_ID = T["MODE_ID"].ToString(),
                                                  REG_MODE = T["REG_MODE"].ToString(),
                                                  MII_REG_SIZE = T["MII_REG_SIZE"].ToString(),
                                                  MII_MAX_BUNDLE_COPY = T["MII_MAX_BUNDLE_COPY"].ToString(),
                                                  MEM_STATUS = T["MEM_STATUS"].ToString(),
                                                  MEM_CLOSE_DATE = T["MEM_CLOSE_DATE"].ToString(),
                                                  DISP_ADDED_ON = T["DISP_ADDED_ON"].ToString(),
                                                  ISSUE_MEMBER = T["ISSUE_MEMBER"].ToString(),
                                              }).ToList();
                    var Final_Data = Magazine_Dispatch_Info.ToList();
                    Session["MM_DS"] = Final_Data;
                    //Table1
                    Magazine_Dispatch_Info_Details = (from DataRow T in MM_DS.Tables[1].AsEnumerable()
                                                      select new MagazineDispatch_Info
                                                      {
                                                          ISSUE_MEMBER = T["ISSUE_MEMBER"].ToString(),
                                                          Date = T["Date"].ToString(),
                                                          Dispatch_Mode = T["Dispatch Mode"].ToString(),
                                                          Dispatch_Status = T["Dispatch Status"].ToString(),
                                                          Copies = T["Copies"].ToString(),
                                                          Remarks = T["Remarks"].ToString(),
                                                      }).ToList();
                    var Final_DataDetails = Magazine_Dispatch_Info_Details.ToList();
                    Session["Magazine_Dispatch_Info_Details"] = Final_DataDetails;
                    //Table1
                    Session["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                    Session["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                    Session["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                    return View(Final_Data);
                }
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Magazine_Dispatch_Register').hide();</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult Magazine_Dispatch_Custom_Data(string key = "64317b49-2554-484f-8c6c-34859a8b981e")
        {
            var FinalRegData = Session["MM_DS"] as List<MagazineDispatch_Info>;
            var FDData = FinalRegData != null ? (MagazineDispatch_Info)FinalRegData.Where(f => f.MEM_ID == key).FirstOrDefault() : null;
            string MagReg = "";
            if (FDData != null)
            {
                MagReg = FDData.ID + "![" + FDData.Issue + "![" + FDData.Magazine + "![" + FDData.Member_ID + "![" + FDData.TotalCopies + "![" + FDData.ISSUE_ID + "![" + FDData.MEM_ID
                    + "![" + FDData.MODE_ID + "![" + FDData.TR_ID;

            }
            return GridViewExtension.GetCustomDataCallbackResult(MagReg);
        }

        public PartialViewResult Frm_Magazine_Dispatch_Reg_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Session["MM_DS"] == null || command == "REFRESH")
            {
                var Fina_Data = Session["MM_DS"] as List<MagazineDispatch_Info>;
            }
            return PartialView("Frm_Magazine_Dispatch_Reg_Info_Grid", Session["MM_DS"]);
        }

        public PartialViewResult Frm_Magazine_Dispatch_Reg_Info_Grid_Details(string command, string ISSUE_MEMBER, int ShowHorizontalBar = 0)
        {

            ViewData["ISSUE_MEMBER"] = ISSUE_MEMBER;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            var Final_DataDetail = Session["Magazine_Dispatch_Info_Details"] as List<MagazineDispatch_Info>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ISSUE_MEMBER == ISSUE_MEMBER);
            var ret_list = new List<MagazineDispatch_Info> { sel_bal_info };

            return PartialView("Frm_Magazine_Dispatch_Reg_Info_Grid_Details", ret_list);
        }
        public static GridViewSettings CreateGeneralDetailGridSettings(string ISSUE_MEMBER)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "detailGrid_" + ISSUE_MEMBER;
            settings.SettingsDetail.MasterGridName = "DispatchListGrid";
            settings.Width = Unit.Percentage(100);

            //settings.KeyFieldName = "pKey";
            settings.Columns.Add("ISSUE_MEMBER").Visible = false;
            settings.Columns.Add("Date").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Dispatch_Mode");
            settings.Columns.Add("Dispatch_Status");
            settings.Columns.Add("Remarks");
            settings.Columns.Add("Copies");
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            return settings;
        }

        public static IEnumerable GetDispatchdetails(string ISSUE_MEMBER = "")
        {
            var Final_DataDetail = System.Web.HttpContext.Current.Session["Magazine_Dispatch_Info_Details"] as List<MagazineDispatch_Info>;
            var sel_bal_info = Final_DataDetail.Find(x => x.ISSUE_MEMBER == ISSUE_MEMBER);
            var ret_list = new List<MagazineDispatch_Info> { sel_bal_info };
            return ret_list;
        }
        [HttpGet]
        public ActionResult LookUp_GetMagazineList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Magazine_DBOps.GetList("", "", "") as DataTable;
            DataView d1View = new DataView(d1);
            d1View.Sort = "Name";
            var data = DatatableToModel.GetMagazineLis_Info(d1View.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        #region export
        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }
        #endregion
        public ActionResult Frm_Magazine_Dispatch_Reg_Window(string ActionMethod = "", string FDData = "")
        {
            if (ActionMethod != null)
            {
                xID = Guid.NewGuid().ToString();
                MagazineDispatchRegisterInfo model = new MagazineDispatchRegisterInfo();
                model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                var actionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
                model.ActionMethodName = actionMethod.ToString();
                if (ActionMethod == "New")
                {

                }

                if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    var _Data = FDData.Split(',');
                    model.BE_Issue = _Data[1];
                    Issue = model.BE_Issue;
                    model.BE_Magazine = _Data[2];
                    model.BE_Member = _Data[3];
                    Member = model.BE_Member;
                    model.Issue_ID = _Data[5];
                    IssueID = model.Issue_ID;
                    model.MEM_ID = _Data[6];
                    MemberID = model.MEM_ID;
                    ModeID = _Data[7];
                    model.Txt_Subscriped_count = Convert.ToInt32(_Data[4]);
                    Total_subs_copies = Convert.ToInt32(model.Txt_Subscriped_count);
                    model.Tr_M_ID = _Data[8];
                    DataTable Dispatch_Detail = BASE._Magazine_DBOps.Get_Dispatch_Details(model.Issue_ID, model.MEM_ID);
                    Session["Dispatch_Detail"] = Dispatch_Detail;
                    if (Dispatch_Detail.Rows.Count > 0)
                    {
                        model.Txt_Dispatched_Count = 0;
                        model.Txt_Returned_Count = 0;
                        foreach (DataRow cRow in Dispatch_Detail.Rows)
                        {
                            if (cRow["Status"].ToString() == "DISCONTINUED")
                            {
                                model.Txt_Subscriped_count = 0;
                            }
                            if (Convert.ToInt32(cRow["Disp. Count"]) > 0)
                            {
                                int Txt_Dispatched_Count = 0;
                                Txt_Dispatched_Count = Convert.ToInt32(cRow["Disp. Count"]);
                                model.Txt_Dispatched_Count = model.Txt_Dispatched_Count + Txt_Dispatched_Count;
                            }
                            if (Convert.ToInt32(cRow["Disp. Count"]) < 0)
                            {
                                int Txt_Returned_Count = 0;
                                Txt_Returned_Count = +((1 * Convert.ToInt32(cRow["Disp. Count"])) * -1);
                                model.Txt_Returned_Count = model.Txt_Returned_Count + Txt_Returned_Count;
                            }
                            model.Txt_Net_Dispatched = model.Txt_Dispatched_Count - model.Txt_Returned_Count;
                        }
                        Total_disp_copies = Convert.ToInt32(model.Txt_Dispatched_Count);
                    }
                    else
                    {
                        model.Txt_Dispatched_Count = 0;
                        model.Txt_Returned_Count = 0;
                        model.Txt_Net_Dispatched = 0;
                    }
                }
                return PartialView(model);
            }
            else
            {
                return null;
            }

        }
        [HttpPost]
        public ActionResult Frm_Magazine_Dispatch_Reg_Window(MagazineDispatchRegisterInfo model)
        {
            int xCnt = 0;
            DateTime dDate;
            DataTable dt = new DataTable();
            dt = Session["Dispatch_Detail"] as DataTable;
            Dispatch_Detail = dt;
            var all_data_Of_Grid = (List<Magazine_Dispatch_Detail_Grid>)Session["Grid_Data_Dispatch_Window_Data_Session"];
            for (int I = 0; I <= all_data_Of_Grid.Count() - 1; I++)
            {
                if (all_data_Of_Grid[I].Tr_M_ID.ToString() == xID)
                {
                    Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch inparam = new Common_Lib.RealTimeService.Parameter_Insert_Magazine_Dispatch();
                    inparam.Dispatch_ID = all_data_Of_Grid[I].Mode_ID.ToString();
                    dDate = Convert.ToDateTime(all_data_Of_Grid[I].Date.ToString());
                    inparam.DispatchDate = dDate.ToString(BASE._Server_Date_Format_Short);
                    inparam.Issue_ID = model.Issue_ID;
                    inparam.Membership_ID = model.MEM_ID;
                    inparam.Status = all_data_Of_Grid[I].Status.ToString();
                    inparam.Copies = Convert.ToInt32(all_data_Of_Grid[I].Copies.ToString());
                    inparam.Remarks = all_data_Of_Grid[I].Remarks.ToString();
                    inparam.Tr_ID = xID;
                    if (!BASE._Magazine_DBOps.Insert_Magazine_Dispatch(inparam))
                    {
                        return Json(new { result = false, Message = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //--------            
            for (int I = 0; I <= all_data_Of_Grid.Count() - 1; I++)
            {
                if (all_data_Of_Grid[I].UPDATED.ToString() == "1")
                {
                    Common_Lib.RealTimeService.Parameter_Update_Magazine_Dispatch inparam = new Common_Lib.RealTimeService.Parameter_Update_Magazine_Dispatch();
                    inparam.Dispatch_ID = all_data_Of_Grid[I].Mode_ID.ToString();
                    dDate = Convert.ToDateTime(all_data_Of_Grid[I].Date.ToString());
                    inparam.DispatchDate = dDate.ToString(BASE._Server_Date_Format_Long);
                    inparam.Issue_ID = model.Issue_ID;
                    inparam.Membership_ID = model.MEM_ID;
                    inparam.Status = all_data_Of_Grid[I].Status.ToString();
                    inparam.Copies = Convert.ToInt32(all_data_Of_Grid[I].Copies.ToString());
                    inparam.Remarks = all_data_Of_Grid[I].Remarks.ToString();
                    inparam.Tr_ID = all_data_Of_Grid[I].Tr_M_ID.ToString();
                    if (!BASE._Magazine_DBOps.Update_Magazine_Dispatch(inparam))
                    {
                        return Json(new { result = false, Message = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var Deleted_Vouchers = (List<Deleted_Dispatch_Vouchers>)Session["Deleted_Vouchers"];
            if (Deleted_Vouchers != null)
            {
                if (Deleted_Vouchers.Count > 0)
                {
                    for (int I = 0; I <= Deleted_Vouchers.Count() - 1; I++)
                    {
                        if (!BASE._Magazine_DBOps.Delete_Magazine_dispatch(Deleted_Vouchers[I].ToString()))
                        {
                            return Json(new { result = false, Message = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new { result = true, Message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DispatchGridData(string ActionMethodName, string IssueID = null, string MemberID = null)
        {
            DataTable DT = new DataTable();
            DataRow ROW;
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethodName);
            DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT.Columns.Add("Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Copies", Type.GetType("System.Int32"));
            DT.Columns.Add("Disp. Count", Type.GetType("System.Int32"));
            DT.Columns.Add("Mode", Type.GetType("System.String"));
            DT.Columns.Add("Mode_ID", Type.GetType("System.String"));
            DT.Columns.Add("Status", Type.GetType("System.String"));
            DT.Columns.Add("Remarks", Type.GetType("System.String"));
            DT.Columns.Add("Tr_M_ID", Type.GetType("System.String"));
            DT.Columns.Add("UPDATED", Type.GetType("System.Boolean"));
            if (Tag == Common_Lib.Common.Navigation_Mode._New)
            {
                //ROW = DT.NewRow();
                //DT.Rows.Add(ROW);
            }
            if ((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                        || ((Tag == Common_Lib.Common.Navigation_Mode._Delete)
                        || (Tag == Common_Lib.Common.Navigation_Mode._View)))
            {
                // DataTable Dispatch_Detail = BASE._Magazine_DBOps.Get_Dispatch_Details(IssueID, MemberID);
                if (Session["Dispatch_Detail"] != null)
                {
                    DT = Session["Dispatch_Detail"] as DataTable;
                }
                else { }
            }
            List<Magazine_Dispatch_Detail_Grid> data = DatatableToModel.DataTabletoDispatch_Window_Grid_INFO(DT);
            if (Session["Grid_Data_Dispatch_Window_Data_Session"] != null)
            {
                ViewBag.Grid_Data_Dispatch_Window_Data = Session["Grid_Data_Dispatch_Window_Data_Session"];
            }
            else
            {
                Session["DispatchEDN"] = data;
                ViewBag.Grid_Data_Dispatch_Window_Data = data;
                Session["Grid_Data_Dispatch_Window_Data_Session"] = data;
            }
            return PartialView(ViewBag.Grid_Data_Dispatch_Window_Data);
        }
        public ActionResult Frm_DispatchEntryWindow(string ActionMethod = "", string SrID = null)
        {
            Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);
            Magazine_Dispatch_Info_Details model = new Magazine_Dispatch_Info_Details();
            if (ActionMethod == "_New")
            {
                model.ActionMethod = Tag;
                model.Txt_Dispatched_count = 0;
                model.Txt_Date = DateTime.Now;
                model.Issue_Member = Member + " " + Issue;
            }
            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                model.ActionMethod = Tag;
                model.Issue_Member = Member + " " + Issue;
                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Magazine_Dispatch_Detail_Grid>)Session["Grid_Data_Dispatch_Window_Data_Session"];
                var dataToEdit = all_data.FirstOrDefault(x => x.Sr == Sr);
                model.Sr = Sr;
                model.Txt_Date = dataToEdit.Date;
                model.Txt_Dispatched_count = dataToEdit.Copies;
                model.GLookUp_DTypeList = dataToEdit.Mode_ID;
                model.Cmb_Status = dataToEdit.Status;
                model.Me_Remarks = dataToEdit.Remarks;
                model.Tr_M_ID = dataToEdit.Tr_M_ID;
            }
            return PartialView(model);
        }
        public ActionResult Frm_Magazine_Dispatch_Detail_Window(ConnectOneMVC.Areas.Magazine.Models.Magazine_Dispatch_Info_Details model)
        {
            model.Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());

            if (model.Tag == Common_Lib.Common.Navigation_Mode._New | model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (IsDate(model.Txt_Date.ToString()) == true)
                {
                    DateTime firstDate = BASE._open_Year_Sdt;
                    DateTime secondDate = model.Txt_Date ?? DateTime.Now;
                    TimeSpan diff = secondDate.Subtract(firstDate);
                    TimeSpan diff1 = secondDate - firstDate;
                    double diff2 = Convert.ToDouble((secondDate - firstDate).TotalDays.ToString());
                    if (diff2 < 0)
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Date   not   as   per   Financial   Year . . . !"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime firstDate1 = BASE._open_Year_Sdt;
                    DateTime secondDate1 = model.Txt_Date ?? DateTime.Now;
                    TimeSpan diffr = secondDate1.Subtract(firstDate1);
                    TimeSpan diff3 = secondDate1 - firstDate1;
                    double diff4 = Convert.ToDouble((secondDate1 - firstDate1).TotalDays.ToString());
                    //if(diff4 > 0)
                    //{
                    //    return Json(new
                    //    {
                    //        result = false,
                    //        message = "D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !"
                    //    }, JsonRequestBehavior.AllowGet);
                    //}
                }
                if (model.GLookUp_DTypeList == null || string.IsNullOrEmpty(model.GLookUp_DTypeList))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Dispatch   Type   not   specified. . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Cmb_Status == null || string.IsNullOrEmpty(model.Cmb_Status))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Status   not   specified. . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                int tt = (((model.Cmb_Status.ToUpper() == "DELIVERED")
                || (model.Cmb_Status.ToUpper() == "DISCONTINUED")) ? model.Txt_Dispatched_count : ((1 * model.Txt_Dispatched_count)
                * -1));
                if (Total_disp_copies + tt > Total_subs_copies)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Dispatched Count can not be greater than Total subscription(" + Total_subs_copies + "). . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                int tt_dispatch = ((model.Cmb_Status.ToUpper() == "DELIVERED") || (model.Cmb_Status.ToUpper() == "DISCONTINUED")) ? model.Txt_Dispatched_count : -1 * model.Txt_Dispatched_count;
                if (Total_disp_copies + tt_dispatch < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Total Dispatched Copies after Adjusting Returns can not be Negative . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Dispatched_count <= 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Please   enter   Valid   Dispatch   Count . . . !"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            List<Magazine_Dispatch_Detail_Grid> gridRows = new List<Magazine_Dispatch_Detail_Grid>();
            if (Session["Grid_Data_Dispatch_Window_Data_Session"] != null)
            {
                gridRows = (List<Magazine_Dispatch_Detail_Grid>)Session["Grid_Data_Dispatch_Window_Data_Session"];
            }
            if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
            {
                Magazine_Dispatch_Detail_Grid grid = new Magazine_Dispatch_Detail_Grid();
                if ((gridRows.Count <= 0))
                {
                    grid.Sr = 1;
                }
                else
                {
                    if (string.IsNullOrEmpty(gridRows.FirstOrDefault().Sr.ToString()))
                    { grid.Sr = 1; }
                    else
                    { grid.Sr = gridRows.Count + 1; }
                }
                grid.Date = model.Txt_Date;
                grid.Copies = model.Txt_Dispatched_count;
                grid.Disp_Count = model.Cmb_Status.ToUpper() == "DELIVERED" || model.Cmb_Status.ToUpper() == "DISCONTINUED" ? model.Txt_Dispatched_count : 1 * model.Txt_Dispatched_count * -1;
                grid.Mode = model.Mode_Name;
                grid.Mode_ID = model.GLookUp_DTypeList;
                grid.Status = model.Cmb_Status;
                grid.Remarks = model.Me_Remarks;
                grid.Tr_M_ID = xID;
                grid.UPDATED = 0;
                if (Convert.ToInt32(grid.Disp_Count) > 0)
                {
                    Total_disp_copies = Total_disp_copies + Convert.ToInt32(grid.Disp_Count);
                }
                if (Convert.ToInt32(grid.Disp_Count) < 0)
                {
                    int _retCount = ((1 * Convert.ToInt32(grid.Disp_Count)) * -1);
                    Txt_Returned_Count = Txt_Returned_Count + _retCount;
                }
                Txt_Net_Dispatched = Total_disp_copies - Txt_Returned_Count;
                gridRows.Add(grid);
            }
            else if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Date = model.Txt_Date;
                dataToEdit.Copies = model.Txt_Dispatched_count;
                dataToEdit.Disp_Count = model.Cmb_Status.ToUpper() == "DELIVERED" || model.Cmb_Status.ToUpper() == "DISCONTINUED" ? model.Txt_Dispatched_count : 1 * model.Txt_Dispatched_count * -1;
                dataToEdit.Mode = model.Mode_Name;
                dataToEdit.Mode_ID = model.GLookUp_DTypeList; ;
                dataToEdit.Status = model.Cmb_Status;
                dataToEdit.Remarks = model.Me_Remarks;
                dataToEdit.Tr_M_ID = model.Tr_M_ID == xID ? xID : model.Tr_M_ID;
                dataToEdit.UPDATED = model.Tr_M_ID == xID ? 0 : 1;
                if (Convert.ToInt32(dataToEdit.Disp_Count) > 0)
                {
                    Total_disp_copies = Total_disp_copies + Convert.ToInt32(dataToEdit.Disp_Count);
                }
                if (Convert.ToInt32(dataToEdit.Disp_Count) < 0)
                {
                    int _retCount = ((1 * Convert.ToInt32(dataToEdit.Disp_Count)) * -1);
                    Txt_Returned_Count = Txt_Returned_Count + _retCount;
                }
                Txt_Net_Dispatched = Total_disp_copies - Txt_Returned_Count;
                //dataToEdit.Ref_Clearing_Date = model.Txt_Ref_CDate;
            }
            Session["Grid_Data_Dispatch_Window_Data_Session"] = gridRows;
            return Json(new
            {
                result = true,
                Total_disp_copies = Total_disp_copies,
                Txt_Returned_Count = Txt_Returned_Count,
                Txt_Net_Dispatched = Txt_Net_Dispatched,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_DispatchDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null, string TxnMID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            if (TxnMID != xID)
            {
                if (Session["Deleted_Vouchers"] == null)
                {
                    var Deleted_Vouchers = new List<Deleted_Dispatch_Vouchers>();
                    Deleted_Dispatch_Vouchers ddv = new Deleted_Dispatch_Vouchers();
                    ddv.Tr_M_ID = TxnMID;
                    Deleted_Vouchers.Add(ddv);
                    Session["Deleted_Vouchers"] = Deleted_Vouchers;
                }
                else
                {
                    var Deleted_Vouchers = (List<Deleted_Dispatch_Vouchers>)Session["Deleted_Vouchers"];
                    Deleted_Dispatch_Vouchers ddv = new Deleted_Dispatch_Vouchers();
                    ddv.Tr_M_ID = TxnMID;
                    Deleted_Vouchers.Add(ddv);
                    Session["Deleted_Vouchers"] = Deleted_Vouchers;
                }
            }
            var allData = (List<Magazine_Dispatch_Detail_Grid>)Session["Grid_Data_Dispatch_Window_Data_Session"];
            var dataToDelete = allData.FirstOrDefault(x => x.Sr == Sr);
            if (Convert.ToInt32(dataToDelete.Disp_Count) > 0)
            {
                Total_disp_copies = Total_disp_copies - Convert.ToInt32(dataToDelete.Disp_Count);
            }
            //if (Convert.ToInt32(dataToDelete.Disp_Count) > 0)
            //{
            //    int _retCount = Convert.ToInt32(dataToDelete.Disp_Count);
            //    Txt_Returned_Count = Txt_Returned_Count - _retCount;
            //}
            Txt_Net_Dispatched = Total_disp_copies - Txt_Returned_Count;
            allData.Remove(dataToDelete);
            Session["Grid_Data_Dispatch_Window_Data_Session"] = allData;
            return Json(new
            {
                result = true,
                Total_disp_copies = Total_disp_copies,
                Txt_Returned_Count = Txt_Returned_Count,
                Txt_Net_Dispatched = Txt_Net_Dispatched,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetDispatchTypeList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Magazine_DBOps.GetList_DispatchTypeList("", "", "");
            DataView dview = new DataView(d1);
            //dview.Sort = "ID";
            var data = DatatableToModel.DataTabletoDispatchTypeList_INFO(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        public ActionResult DataNavigation(string selectedRows, string ActionMethod = "", string Cmb_ListBy = "", string Txt_Issue_Date = "", string GLookUp_MagList = "")
        {
            string[] records = selectedRows.Split('#');

            DataTable selectedData = new DataTable();
            selectedData.Columns.Add("Dispatch Member ID");
            selectedData.Columns.Add("Member ID");
            selectedData.Columns.Add("Member Old ID");
            selectedData.Columns.Add("Member");
            selectedData.Columns.Add("Magazine");
            selectedData.Columns.Add("Issue");
            selectedData.Columns.Add("Total Copies");
            selectedData.Columns.Add("Dispatched Copies");
            selectedData.Columns.Add("Status");
            selectedData.Columns.Add("Member Status");
            selectedData.Columns.Add("MEM_ID");
            selectedData.Columns.Add("MD_ID");
            selectedData.Columns.Add("Address");
            selectedData.Columns.Add("NAME");
            selectedData.Columns.Add("ADD1");
            selectedData.Columns.Add("ADD2");
            selectedData.Columns.Add("ADD3");
            selectedData.Columns.Add("ADD4");
            selectedData.Columns.Add("ADD5");
            selectedData.Columns.Add("Mem_Issue_Info");
            selectedData.Columns.Add("Copies");
            selectedData.Columns.Add("ISSUE_ID");
            selectedData.Columns.Add("TR_ID");
            selectedData.Columns.Add("ISSUE_MEMBER");
            selectedData.Columns.Add("MODE");
            selectedData.Columns.Add("MODE_ID");
            selectedData.Columns.Add("REG_MODE");
            selectedData.Columns.Add("MII_REG_SIZE");
            selectedData.Columns.Add("MII_MAX_BUNDLE_COPY");
            selectedData.Columns.Add("MII_RPC_SEED");
            selectedData.Columns.Add("MII_COPY_WT");
            selectedData.Columns.Add("MEM_STATUS");
            selectedData.Columns.Add("MEM_CLOSE_DATE");
            selectedData.Columns.Add("MEM_CATEGORY");
            selectedData.Columns.Add("DISP_ADDED_ON");
            selectedData.Columns.Add("City");
            selectedData.Columns.Add("State");
            selectedData.Columns.Add("PinCode");
            selectedData.Columns.Add("Country");
            selectedData.Columns.Add("BUNDLE_WEIGHT");
            selectedData.Columns.Add("MII_ISSUE_DATE");
            selectedData.Columns.Add("DISP_DONE_MODE");
            selectedData.Columns.Add("MAG_REC_ID");
            selectedData.Columns.Add("Expiry Status");
            selectedData.Columns.Add("REGION");
            selectedData.Columns.Add("RMS");
            selectedData.Columns.Add("PSO");
            selectedData.Columns.Add("CONTACT_NO");
            selectedData.Columns.Add("CURRTIME");
            for (int i = 0; i < records.Length - 1; i++)
            {
                string[] record = records[i].Split(',');
                for (int j = 0; j < records.Length - 1; j++)
                {
                    selectedData.Rows.Add(record[0], "", "", "", "", "", record[6], "", "", "", record[10], "", "", record[21], record[13], record[16], record[17],
                        record[30], "", record[31], record[32], record[33], "", "", record[36], record[37], record[38], record[39], record[40], record[41],
                        record[42], record[43], "", record[45], record[46], record[18], record[49], record[50], record[51], record[52], record[53], "", record[55], "", "",
                        record[58], "", record[60], "").ToString();
                }
            }
            //var facebookFriends = new JavaScriptSerializer().Deserialize<JObject>(selectedRows.ToString());
            if (ActionMethod == "EDIT")
            {

            }
            if (ActionMethod == "DELETE-ALL")
            {
                if (!BASE._Voucher_Magazine_DBOps.Delete_All_Issue_dispatches(Convert.ToDateTime(Txt_Issue_Date), GLookUp_MagList))
                {
                    return Json(new { Message = "Error", result = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = "Dispatches Deleted Successfully!!", result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "BY HAND")
            {
                string ErrMsg = "";
                foreach (DataRow cRow in selectedData.Rows)
                {
                    int C1 = cRow["Dispatched Copies"].ToString() == "" ? 0 : Convert.ToInt32(cRow["Dispatched Copies"].ToString());
                    int C2 = cRow["Total Copies"].ToString() == "" ? 0 : Convert.ToInt32(cRow["Total Copies"].ToString());
                    int Copies = C2 - C1;
                    if (Copies > 0)
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_dispatch_New_Voucher inDispatch = new Common_Lib.RealTimeService.Parameter_Insert_dispatch_New_Voucher();
                        inDispatch.FromDate = Convert.ToDateTime(cRow["MII_ISSUE_DATE"].ToString());
                        inDispatch.ToDate = Convert.ToDateTime(cRow["MII_ISSUE_DATE"].ToString());
                        inDispatch.MagID = cRow["MAG_REC_ID"].ToString();
                        inDispatch.MembershipID = cRow["MEM_ID"].ToString();
                        inDispatch.subDate = DateTime.Now;
                        inDispatch.subsCopies = Copies;
                        if (!BASE._Magazine_DBOps.Insert_Magazine_Dispatch_New_Voucher(inDispatch))
                        {
                            return Json(new { result = false, Message = "Sorry !! Copies already dispatched for" + cRow["Member"].ToString() + "(" + cRow["Member ID"].ToString() + ") for Issue dated " }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { result = true, Message = "" + cRow["Member"].ToString() + "(" + cRow["Member ID"].ToString() + ") for Issue dated " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        DateTime IssueDate = Convert.ToDateTime(cRow["MII_ISSUE_DATE"].ToString());

                    }
                }
            }
            if (ActionMethod == "REGISTRY REPORT")
            {
                bool ForRemainingCopiesOnly = true;
                return RedirectToAction("Frm_Magazine_Dispatch_Sort");
            }
            if (ActionMethod == "LABEL-REGISTRY")
            {
                return Json(new { Message = "LABEL", result = true }, JsonRequestBehavior.AllowGet);
            }
            if (ActionMethod == "LABEL-REGULAR")
            {

            }
            if (ActionMethod == "REPORT")
            {
                bool ForRemainingCopiesOnly = true;
                DataTable FinalData = UpdateCopies(FinalizeBundles(selectedData, ForRemainingCopiesOnly));
                var query = from row in FinalData.AsEnumerable()
                            group row by new
                            {
                                Copies = row.Field<string>("Copies"),
                                MEM_CATEGORY = row.Field<string>("MEM_CATEGORY"),
                                MODE = row.Field<string>("MODE"),
                                BUNDLE_WEIGHT = row.Field<string>("BUNDLE_WEIGHT"),
                                dISP_DONE_MODE = row.Field<string>("DISP_DONE_MODE"),
                                ISSUE_ID = row.Field<string>("ISSUE_ID").Max()

                            } into MonthGroup
                            select new
                            {
                                MonthGroup.Key.Copies,
                                Count = MonthGroup.Key.Copies.Count(),
                                Weight = MonthGroup.Key.BUNDLE_WEIGHT.Max(),
                                DISP_DONE_MODE = MonthGroup.Key.dISP_DONE_MODE,
                                Total = Convert.ToInt32(MonthGroup.Key.Copies) * MonthGroup.Key.Copies.Count(),
                                Category = MonthGroup.Key.MEM_CATEGORY,
                                MODE = MonthGroup.Key.MODE,
                                ISSUE_ID = MonthGroup.Key.ISSUE_ID
                            };
                DataTable DataTbl = DataTableHelper.ToDataTable(query.ToList());
                //DataTable DataTbl = query.CopyToDataTable;
                var FinalQuery = from Main in DataTbl.AsEnumerable()
                                 from Rates in MM_DS.Tables[2].AsEnumerable()
                                 where (Rates.Field<string>("MDE_CATEGORY") == Main.Field<string>("Category"))
                                   && (Convert.ToInt32(Main.Field<char>("Weight")) >= Convert.ToInt32(Rates.Field<decimal>("FROM_WT")))
                                   && (Convert.ToInt32(Main.Field<char>("Weight")) <= Convert.ToInt32(Rates.Field<decimal>("TO_WT")))
                                 //&& (Main.Field<string>("Issue_ID")== Rates.Field<string>("Issue_ID"))
                                 select new
                                 {
                                     Copies = Main.Field<string>("Copies"),
                                     Weight = Main.Field<char>("Weight"),
                                     StampValue = Main.Field<string>("Mode").ToString().ToUpper().Contains("REGIST") ? Rates.Field<decimal>("REG_RATE") : Rates.Field<decimal>("RATE"),
                                     Count = Main.Field<int>("Count"),
                                     Total = Main.Field<int>("Total"),
                                     Category = Main.Field<string>("Category"),
                                     DISP_DONE_MODE = Main.Field<string>("DISP_DONE_MODE"),
                                     Mode = Main.Field<string>("Mode"),
                                     TotalValue = Main.Field<string>("Mode").ToString().ToUpper().Contains("REGIST") ? Rates.Field<decimal>("REG_RATE") : Rates.Field<decimal>("RATE") * Main.Field<int>("Count"),
                                 };
                DataTable Final_Data = DataTableHelper.ToDataTable(FinalQuery.ToList());

                DataView dataView = new DataView(Final_Data);
                dataView.Sort = " Copies ASC ";
                dataView.RowFilter = "DISP_DONE_MODE <> 'BY HAND'";
                //switch (OptedMode)
                //{
                //    case Mode.Registry_Post:
                //        dataView.RowFilter += " AND MODE like \'REGIST%\'";
                //        break;
                //    case Mode.Regular_Post:
                //        dataView.RowFilter += " AND MODE not like \'REGIST%\'";
                //        break;
                //}
                DataTable ReportTable = dataView.ToTable();
                ReportTable.Columns.Remove("DISP_DONE_MODE");
                if (ReportTable.Rows.Count > 0)
                {

                }


                //var FinalQuery = from Main in DataTbl.AsEnumerable() join Rates in MM_DS.Tables[2].AsEnumerable()
                //                 on  Main.Field<string>("MDE_CATEGORY") equals Rates.Field<string>("MDE_CATEGORY")
                //                 where (Main.Field<int>("Weight") >= Rates.Field<decimal>("FROM_WT") &&
                //                 Main.Field<int>("Weight") <= Rates.Field<decimal>("TO_WT") as data


                //var query = from row in FinalData.AsEnumerable()
                //            group row by new MagazineDispatch_Info
                //            {
                //                Copies = Convert.ToString(row.Field<int>("Copies")),
                //                MEM_CATEGORY = row.Field<string>("MEM_CATEGORY"),
                //                MODE = row.Field<string>("MODE")
                //            } into MonthGroup
                //            select new MagazineDispatch_Info()
                //            {
                //                Count = MonthGroup.Key.Copies.Count(),
                //                //Weight = MonthGroup.Key.BUNDLE_WEIGHT.Max(),
                //                //DISP_DONE_MODE = MonthGroup.Key.DISP_DONE_MODE.Max().ToString(),
                //                //Total = Convert.ToInt32(MonthGroup.Key.Copies) * MonthGroup.Key.Copies.Count(),
                //                MEM_CATEGORY = MonthGroup.Key.MEM_CATEGORY,
                //                MODE = MonthGroup.Key.MODE
                //                //ISSUE_ID = MonthGroup.Key.ISSUE_ID.Count().ToString()
                //            };
                //DataTable DataTbl = new DataTable();
                //var FinalQuery = (from Main in DataTbl.AsEnumerable()
                //                  join Rates in MM_DS.Tables[2].AsEnumerable()
                //                  on Main.Field<string>("MDE_CATEGORY") equals Rates.Field<string>("Category")
                //                  where Main.Field<int>("Weight") >= Rates.Field<decimal>("FROM_WT")
                //                  && Main.Field<int>("Weight") <= Rates.Field<decimal>("TO_WT");
                //&& Main.Field<string>("Issue_ID") = Rates.Field<string>("Issue_ID")


            }
            if (ActionMethod == "LABEL")
            {

                //'Finalize Bundles
                DataTable FinalData = FinalizeBundles(selectedData);
                //'Sort
                DataView dview = new DataView(FinalData);
                //dview.Sort;
                DataTable finalSortedTable = dview.ToTable();
                //Add Sr no
                finalSortedTable = Add_RPC_Codes(finalSortedTable);
                if (finalSortedTable.Rows.Count == 0)
                {
                    return Json(new { result = false, Message = "Sorry !! No Valid Label with outstanding dispatches selected" }, JsonRequestBehavior.AllowGet);
                }
                //StiReport Report = new StiReport();
                //Report.RegData(finalSortedTable);
                //if (BASE._open_Cen_ID == 4427)
                //{
                //    Report.Load("LF_D.mrt");
                //}
                //else
                //{
                //    Report.Load("LF_E.mrt");
                //}
                //Report.Compile();
                //Report.Show(Me.ParentForm, True)
                //Me.Cursor = Cursors.Default
                //Report.Dispose()
                return Json(new { result = true, Message = "Mark displayed records as 'Dispatched'...?" }, JsonRequestBehavior.AllowGet);
                //IReport Report = new StiReport(); 
            }
            return Json(new { Message = "All Dispatches for Selected Issue shall be deleted, Continue...?", result = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Frm_Magazine_Dispatch(string actionMethod = "")
        {
            MagazineDispatchRegisterInfo magazineDispatch_Info = new MagazineDispatchRegisterInfo();
            magazineDispatch_Info.Actiontype = actionMethod;
            return View(magazineDispatch_Info);
        }
        [HttpGet]
        public ActionResult Frm_Magazine_Dispatch_Sort(string ActionMethod = "")
        {
            MagazineDispatchRegisterInfo magazineDispatch_Info = new MagazineDispatchRegisterInfo();
            magazineDispatch_Info.Actiontype = ActionMethod;
            return View(magazineDispatch_Info);
        }

        private DataTable UpdateCopies(DataTable OldCopies)
        {
            foreach (DataRow cRow in OldCopies.Rows)
                cRow["Copies"] = cRow["Total Copies"].ToString();
            return OldCopies;
        }

        private DataTable FinalizeBundles(DataTable selectedData, bool ForRemainingCopiesOnly = true)
        {
            Refine_Address(selectedData);
            return CreateBundles(selectedData, ForRemainingCopiesOnly);
        }
        private void Refine_Address(DataTable _table)
        {
            int _xCnt = 0;
            foreach (DataRow _xrow in _table.Rows)
            {
                string OrgAdd1 = _xrow["ADD1"].ToString();
                string OrgAdd2 = _xrow["ADD2"].ToString();
                string OrgAdd3 = _xrow["ADD3"].ToString();
                string OrgAdd4 = _xrow["ADD4"].ToString();
                string OrgAdd5 = _xrow["ADD5"].ToString();

                string TempAdd1 = ""; string TempAdd2 = ""; string TempAdd3 = ""; string TempAdd4 = ""; string TempAdd5 = "";

                // ------------start----------------
                if (OrgAdd1.Trim().Length > 0)
                {
                    TempAdd1 = OrgAdd1;
                    if (OrgAdd2.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd2;
                        if (OrgAdd3.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd3;
                            if (OrgAdd4.Trim().Length > 0)
                            {
                                TempAdd4 = OrgAdd4;
                                if (OrgAdd5.Trim().Length > 0)
                                    TempAdd5 = OrgAdd5;
                                else
                                    TempAdd5 = ""; // OrgAdd5
                            }
                            else if (OrgAdd5.Trim().Length > 0)
                            {
                                TempAdd4 = OrgAdd5; TempAdd5 = "";
                            }
                            else
                            {
                                TempAdd4 = ""; TempAdd5 = "";
                            }// OrgAdd5 // OrgAdd4
                        }
                        else if (OrgAdd4.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd4;
                            if (OrgAdd5.Trim().Length > 0)
                            {
                                TempAdd4 = OrgAdd5; TempAdd5 = "";
                            }
                            else
                            {
                                TempAdd4 = ""; TempAdd5 = "";
                            } // OrgAdd5
                        }
                        else if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        }// OrgAdd5// OrgAdd4 // OrgAdd3
                    }
                    else if (OrgAdd3.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd3;
                        if (OrgAdd4.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd4;
                            if (OrgAdd5.Trim().Length > 0)
                            {
                                TempAdd4 = OrgAdd5; TempAdd5 = "";
                            }
                            else
                            {
                                TempAdd4 = ""; TempAdd5 = "";
                            } // OrgAdd5
                        }
                        else if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        }// OrgAdd5 // OrgAdd4
                    }
                    else if (OrgAdd4.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd4;
                        if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        } // OrgAdd5
                    }
                    else if (OrgAdd5.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd5; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }
                    else
                    {
                        TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }// OrgAdd5// OrgAdd4// OrgAdd3 // OrgAdd2
                }
                else if (OrgAdd2.Trim().Length > 0)
                {
                    TempAdd1 = OrgAdd2;
                    if (OrgAdd3.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd3;
                        if (OrgAdd4.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd4;
                            if (OrgAdd5.Trim().Length > 0)
                            {
                                TempAdd4 = OrgAdd5; TempAdd5 = "";
                            }
                            else
                            {
                                TempAdd4 = ""; TempAdd5 = "";
                            } // OrgAdd5
                        }
                        else if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        }// OrgAdd5 // OrgAdd4
                    }
                    else if (OrgAdd4.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd4;
                        if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        } // OrgAdd5
                    }
                    else if (OrgAdd5.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd5; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }
                    else
                    {
                        TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }// OrgAdd5// OrgAdd4 // OrgAdd3
                }
                else if (OrgAdd3.Trim().Length > 0)
                {
                    TempAdd1 = OrgAdd3;
                    if (OrgAdd4.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd4;
                        if (OrgAdd5.Trim().Length > 0)
                        {
                            TempAdd3 = OrgAdd5; TempAdd4 = ""; TempAdd5 = "";
                        }
                        else
                        {
                            TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                        } // OrgAdd5
                    }
                    else if (OrgAdd5.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd5; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }
                    else
                    {
                        TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }// OrgAdd5 // OrgAdd4
                }
                else if (OrgAdd4.Trim().Length > 0)
                {
                    TempAdd1 = OrgAdd4;
                    if (OrgAdd5.Trim().Length > 0)
                    {
                        TempAdd2 = OrgAdd5; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    }
                    else
                    {
                        TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                    } // OrgAdd5
                }
                else if (OrgAdd5.Trim().Length > 0)
                {
                    TempAdd1 = OrgAdd5; TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                }
                else
                {
                    TempAdd1 = ""; TempAdd2 = ""; TempAdd3 = ""; TempAdd4 = ""; TempAdd5 = "";
                }// OrgAdd5// OrgAdd4// OrgAdd3// OrgAdd2 // OrgAdd1
                 // ------------over----------------

                // _table.Rows(_xCnt)("NO") = _xCnt + 1
                _table.Rows[_xCnt]["ADD1"] = TempAdd1;
                _table.Rows[_xCnt]["ADD2"] = TempAdd2;
                _table.Rows[_xCnt]["ADD3"] = TempAdd3;
                _table.Rows[_xCnt]["ADD4"] = TempAdd4;
                _table.Rows[_xCnt]["ADD5"] = TempAdd5;

                _xCnt = _xCnt + 1;
            } // For Each 
        }
        private DataTable CreateBundles(DataTable dispatchdata, bool ForRemainingCopiesOnly = true)
        {
            string CopyCol = ForRemainingCopiesOnly ? "Copies" : "Total Copies";
            DataTable bundledata = dispatchdata;
            int ctr = 1;
            foreach (DataRow cRow in dispatchdata.Rows)
            {
                //xPleaseWait.Show("Creating Bundles ..(" + System.Convert.ToString(ctr * 100 / (double)dispatchdata.Rows.Count) + " % Completed)");
                int CopyCount = Convert.ToInt32(cRow[CopyCol].ToString());
                if (CopyCount >= Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString()) && cRow["MEM_STATUS"].ToString() == "CONTINUE")
                {
                    // Create new Bundle as Max bundle Size has 
                    while (CopyCount >= Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString()))
                    {
                        var row = bundledata.Rows.Add();
                        //row.ItemArray = cRow.ItemArray;
                        int CurrBundleSize;
                        if (Convert.ToInt32(cRow["MII_REG_SIZE"].ToString()) > 0)
                        {
                            // Defined Min Reg size
                            int CopiesAfterMaxSize = (CopyCount - Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString()));
                            int CopiesModMaxSize = (CopyCount % Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString()));
                            int CopiesModMinRegSize = (CopyCount % Convert.ToInt32(cRow["MII_REG_SIZE"].ToString()));
                            int DiffofMinRegAndMaxSize = (Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString()) - Convert.ToInt32(cRow["MII_REG_SIZE"].ToString()));
                            // 40
                            CurrBundleSize = Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString());
                            if (((((CopiesAfterMaxSize / Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())) >= 2) ||
                                (((CopyCount % Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())) > Convert.ToInt32(cRow["MII_REG_SIZE"].ToString()) ||
                                (((CopyCount % Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())) == 0) ||
                                (((CopyCount % Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())) < DiffofMinRegAndMaxSize) &&
                                ((CopyCount % Convert.ToInt32(cRow["MII_REG_SIZE"].ToString())) > DiffofMinRegAndMaxSize)))))
                                        && (CopyCount >= Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())))))
                            {
                                CurrBundleSize = Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString());
                            }
                            else if ((CopyCount <= Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString())))
                            {
                                CurrBundleSize = CopyCount;
                            }
                            else
                            {
                                CurrBundleSize = Convert.ToInt32(cRow["MII_REG_SIZE"].ToString());
                            }

                        }
                        else
                        {
                            CurrBundleSize = Convert.ToInt32(cRow["MII_MAX_BUNDLE_COPY"].ToString());
                        }

                        row["Copies"] = CurrBundleSize;
                        row["Total Copies"] = CurrBundleSize;
                        CopyCount = (CopyCount - CurrBundleSize);
                        row["Mem_Issue_Info"] = row["Mem_Issue_Info"];
                        //  + "[" + ctr.ToString + "]"
                        if ((Convert.ToInt32(cRow["MII_REG_SIZE"].ToString()) > 0))
                        {
                            row["MODE"] = row["REG_MODE"];
                        }

                        row["BUNDLE_WEIGHT"] = Convert.ToInt32(row[CopyCol].ToString()) * Convert.ToInt32(row["MII_COPY_WT"].ToString());
                        ctr++;
                    }

                }
                // Add row having copies within max limit
                if (CopyCount > 0 & cRow["MEM_STATUS"].ToString() == "CONTINUE")
                {
                    var NewRow = bundledata.Rows.Add();
                    NewRow.ItemArray = cRow.ItemArray;
                    NewRow["Copies"] = CopyCount;
                    NewRow["Total Copies"] = CopyCount;
                    //NewRow("BUNDLE_WEIGHT") = CInt(Val(NewRow(CopyCol)) * Val(NewRow("MII_COPY_WT")))
                    if (ctr > 1)
                        NewRow["Mem_Issue_Info"] = NewRow["Mem_Issue_Info"]; // + "[" + ctr.ToString + "]"
                    if (Convert.ToInt32(NewRow[CopyCol].ToString()) >= (NewRow["MII_REG_SIZE"].ToString() + NewRow.ToString()).ToString().Length)
                        NewRow["MODE"] = NewRow["REG_MODE"];
                }
            }
            ctr = 1;
            foreach (DataRow cRow in bundledata.Rows)
            {
                //xPleaseWait.Show("Finalizing Bundles ..(" + System.Convert.ToString(ctr * 100 / (double)dispatchdata.Rows.Count) + " % Completed)");
                DataRow[] bundleCount = bundledata.Select("[Dispatch Member ID] = '" + cRow["Dispatch Member ID"].ToString() + "'");
                if (bundleCount.Length > 0)
                {
                    if (cRow["Mem_Issue_Info"] != bundleCount[0]["Dispatch Member ID"])
                        cRow["Mem_Issue_Info"] = cRow["Mem_Issue_Info"].ToString().Replace("]", "/" + bundleCount.Length.ToString() + "]");
                }
                ctr += 1;
            }
            //xPleaseWait.Hide();
            return bundledata;
        }
        private DataTable Add_RPC_Codes(DataTable BundledData)
        {
            Int64 RPC_NO = 0;
            foreach (DataRow cRow in BundledData.Rows)
            {
                if (cRow["MODE"].ToString().ToUpper().Contains("REGIST"))
                {
                    // If cRow("MEM_STATUS") <> "DISPATCHED" Then
                    RPC_NO = RPC_NO == 0 ? Convert.ToInt32(cRow["MII_RPC_SEED"].ToString()) : (RPC_NO + 1);
                    // cRow("MODE") = RPC_NO.ToString.PadLeft(4, "0") ' + " / 23-07-2014" ' Dispatch Date
                    cRow["MII_RPC_SEED"] = RPC_NO.ToString().PadLeft(4);
                    // End If
                }
                else
                {
                    cRow["MII_RPC_SEED"] = DBNull.Value;
                }

            }

            return BundledData;
        }

    }

    public static class DataTableHelper
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }
}