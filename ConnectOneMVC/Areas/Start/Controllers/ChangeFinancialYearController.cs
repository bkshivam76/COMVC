using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    [CheckLogin]
    public class ChangeFinancialYearController : BaseController
    {
        public List<COD_Change_YearModel> CFY_Data
        {
            get { return (List<COD_Change_YearModel>)GetBaseSession("CFY_Data_ChangeFinancialYear"); }
            set { SetBaseSession("CFY_Data_ChangeFinancialYear", value); }
        }
        // GET: Start/ChangeFinancialYear
        public ActionResult Frm_COD_Change_Year()
        {
            Frm_COD_Change_Year_Data();
            return View(CFY_Data);
        }
        public ActionResult Frm_COD_Change_Year_Grid_CustomData(string COD_YEAR_ID)//COD_YEAR_ID; ACC_TYPE;Lock;To;From;Financial_Year)
        {
            string itstr = "";
            var it = CFY_Data.FirstOrDefault(x => x.COD_YEAR_ID == COD_YEAR_ID);
            if (it != null)
            {
                itstr = it.COD_YEAR_ID + "![" + it.ACC_TYPE + "![" + it.Lock + "![" + it.To + "![" +
                            it.From + "![" + it.Financial_Year+"!["+ it.Default;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        [HttpPost]
        public ActionResult Frm_COD_Change_Year(COD_Change_YearModel xfrm)
        {
            try
            {
                if (xfrm.SetDefaultPressed_CFY == true)
                {
                    if (BASE._CODDBOps.UpdateDefaultFinancialYear(xfrm.XCOD_YEAR_ID) == false)
                    {
                        return Json(new { result = false, Message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                BASE._open_Year_ID = xfrm.XCOD_YEAR_ID;
                BASE._open_Year_Name = xfrm.XCOD_YEAR_NAME;
                BASE.OnChangeCenter_Year();
                BASE._open_Year_Sdt = (xfrm.XCOD_YEAR_SDT);
                BASE._open_Year_Edt = (xfrm.XCOD_YEAR_EDT);
                BASE._prev_Unaudited_YearID = 0;
                BASE._next_Unaudited_YearID = 0;
                BASE._Completed_Year_Count = 0;
                BASE._open_Year_Acc_Type = xfrm.XCOD_ACC_TYPE;
                DataTable UnAuditedYear = BASE._CODDBOps.GetUnAuditedFinalYears();
                if ((UnAuditedYear.Rows.Count > 0))
                {
                    if (Convert.ToInt16(UnAuditedYear.Rows[0]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        BASE._next_Unaudited_YearID = Convert.ToInt16(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);
                    }
                    else
                    {
                        BASE._prev_Unaudited_YearID = Convert.ToInt16(UnAuditedYear.Rows[0]["COD_YEAR_ID"]);
                    }

                }

                if ((UnAuditedYear.Rows.Count > 1))
                {
                    if (Convert.ToInt16(UnAuditedYear.Rows[1]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        BASE._next_Unaudited_YearID = Convert.ToInt16(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);
                    }
                    else
                    {
                        BASE._prev_Unaudited_YearID = Convert.ToInt16(UnAuditedYear.Rows[1]["COD_YEAR_ID"]);
                    }
                }
                DataSet _EventID_DS = BASE._CenterDBOps.Get_Base_OpenEventId();
                if ((_EventID_DS.Tables[1].Rows.Count > 0))
                {
                    BASE._IsUnderAudit = true;
                }
                else
                {
                    BASE._IsUnderAudit = false;
                }
                //Sub_Title();
                //Show_Messages();
                //If Base._prefer_open_verification_windows_on_login Then
                //        OpenStartupWindows(sender)
                //    End If
                //If Base._prev_Unaudited_YearID<> Nothing Then
                //    Me.oPrevYearStatus.Text = "NOT SPLIT"
                //Else
                //    Me.oPrevYearStatus.Text = ""
                //End If
                //Me.oCenID.Text = "CenID:" & Base._open_Cen_ID
                BASE._Completed_Year_Count = Convert.ToInt32(BASE._CODDBOps.GetCompletedtYearCount());
                //If Base._Completed_Year_Count > 0 Then Me.oYearNo.Text = "Year No." & CStr(Base._Completed_Year_Count + 1) Else Me.oYearNo.Text = ""


                //Set_Default(True) Code
                if (true)
                {
                    // File Menu.............
                    //All Menu visiblility code has been skipped
                    // ...................................
                    BASE._open_ClientUser = "NO";
                    BASE.Refresh_Notes_List = true;
                    //try
                    //{
                    //    //DateTime ServerDate = Convert.ToDateTime( BASE._Action_Items_DBOps.GetServerDateTime();
                    //    //if (File.Exists(BASE._dbname_Sys))
                    //    //{
                    //    //    BASE._Sync_Last_DateTime = ConnectOne.Sync.BLL.DBOperations.GetLeastSuccessfulSyncTime(BASE._dbname_Sys, BASE._open_Cen_ID, BASE._open_Ins_ID, Common_Lib.Log.LogSuffix.ClientApplication, ServerDate);
                    //    //}
                    //}
                    //catch (Exception ex)
                    //{
                    //}

                    // 'CHECKING CENTRE TASK INFO.......................
                    BASE.Allow_Foreign_Donation = false;
                    BASE.Allow_Bank_In_C_Box = false;
                    BASE.Is_HQ_Centre = false;
                    BASE.Allow_Membership = false;
                    DataTable PER_DB = BASE._ClientUserDBOps.GetCenterTasks();
                    if ((PER_DB == null))
                    {
                        return Json(new { result = false, Message = Common_Lib.Messages.SomeError }, JsonRequestBehavior.AllowGet);
                        //DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //return;
                    }

                    DataView DV1 = new DataView(PER_DB);
                    DV1.Sort = "TASK_NAME";
                    if ((DV1.Count > 0))
                    {
                        int index = DV1.Find("FOREIGN DONATION");
                        //  Me.Text = DV1(index)("PERMISSION").ToString()
                        if ((index >= 0))
                        {
                            if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                        != 0))
                            {
                                BASE.Allow_Foreign_Donation = true;
                            }
                            else
                            {
                                BASE.Allow_Foreign_Donation = false;
                            }

                        }

                        index = DV1.Find("COLLECTION BOX - BANK");
                        if ((index >= 0))
                        {
                            if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                        != 0))
                            {
                                BASE.Allow_Bank_In_C_Box = true;
                            }
                            else
                            {
                                BASE.Allow_Bank_In_C_Box = false;
                            }

                        }

                        index = DV1.Find("H.Q. CENTRE");
                        if ((index >= 0))
                        {
                            if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                        != 0))
                            {
                                BASE.Is_HQ_Centre = true;
                            }
                            else
                            {
                                BASE.Is_HQ_Centre = false;
                            }

                        }

                        index = DV1.Find("PRINT STATEMENTS");
                        if ((index >= 0))
                        {
                            if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                        != 0))
                            {
                                BASE.Allow_Statements_Without_Restrictions = true;
                            }
                            else
                            {
                                BASE.Allow_Statements_Without_Restrictions = false;
                            }

                        }
                    }
                    else
                    {
                        BASE.Allow_Foreign_Donation = false;
                        BASE.Allow_Bank_In_C_Box = false;
                        BASE.Is_HQ_Centre = false;
                        BASE.Allow_Membership = false;
                        BASE.Allow_Statements_Without_Restrictions = false;
                    }

                    if ((BASE._open_Year_Acc_Type == "MEMBERSHIP"))
                    {
                        BASE.Allow_Membership = true;
                    }
                    else
                    {
                        BASE.Allow_Membership = false;
                    }

                    if ((BASE._open_Year_Acc_Type == "MAGAZINE"))
                    {
                        BASE.Allow_Magazine = true;
                    }
                    else
                    {
                        BASE.Allow_Magazine = false;
                    }

                    object ReportsToBePrinted = "";
                    ReportsToBePrinted = BASE._CenterDBOps.GetReportsToBePrintedInfo(BASE._open_Year_ID);
                    BASE._ReportsToBePrinted = ReportsToBePrinted.ToString();
                    // BASE._ReportsToBePrinted = IIf((Not ReportsToBePrinted Is Nothing) AndAlso ReportsToBePrinted.Length > 0, ReportsToBePrinted, "")
                    // .................................................

                }
                //else -- As we have hardcoded condition, so this code shall never be reached 
                //{
                //    // File Menu.............
                //    // ....................................
                //    // Clear/Set Default Values................
                //    BASE._IsVolumeCenter = false;
                //    BASE._open_Cen_ID = 0;
                //    BASE._open_Cen_Name = "";
                //    BASE._open_PAD_No = "";
                //    BASE._open_Ins_ID = "";
                //    BASE._open_Ins_Name = "";
                //    BASE._open_Year_ID = 0;
                //    BASE._open_Year_Acc_Type = "";
                //    BASE._prev_Unaudited_YearID = 0;
                //    BASE._Completed_Year_Count = 0;
                //    BASE._open_Year_Name = "";
                //    BASE._open_Year_Sdt = DateTime.Now;
                //    BASE._open_Year_Edt = DateTime.Now;
                //    BASE._open_Trans_DB = "";
                //    BASE._open_User_Type = "USER";
                //    BASE.Refresh_Notes_List = false;
                //    BASE._Sync_Last_DateTime = DateTime.Now;
                //    BASE.Allow_Foreign_Donation = false;
                //    BASE.Allow_Bank_In_C_Box = false;
                //    BASE.Is_HQ_Centre = false;
                //    BASE.Allow_Membership = false;
                //    BASE.Allow_Statements_Without_Restrictions = false;
                //    BASE._ReportsToBePrinted = "";
                //}
                Session["Frm_COD_Change_Year_Data_Grid"] = null;
                if (xfrm.SetDefaultPressed_CFY)
                {
                    return Json(new { result = true, Message = BASE._open_Year_Name + " Year Set As Default Successfull" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_COD_Change_Year_Grid(string command)
        {
            if (command == "REFRESH" || CFY_Data == null)
            {
                Frm_COD_Change_Year_Data();
            }
            return View(CFY_Data);
        }
        public void Frm_COD_Change_Year_Data()
        {
            DataTable Year_Table = BASE._CODDBOps.GetFinancialYearList();
            var data = (from a in Year_Table.AsEnumerable()
                        select new COD_Change_YearModel
                        {
                            COD_YEAR_ID = a["COD_YEAR_ID"].ToString(),
                            ACC_TYPE = a["ACC_TYPE"].ToString(),
                            Financial_Year = a["Financial Year"].ToString(),
                            From = (Convert.ToDateTime(a["From"]).ToString("dd/MM/yyyy")),
                            To = (Convert.ToDateTime(a["To"]).ToString("dd/MM/yyyy")),
                            Lock = a["Lock"].ToString(),
                            Default= a["Lock"].ToString()=="No"?true:false
                        }).ToList();
            CFY_Data = data;           
        }
        public void SessionClear()
        {
            ClearBaseSession("_ChangeFinancialYear");
        }
    }
    [Serializable]
    public class COD_Change_YearModel
    {
        public string ACC_TYPE { get; set; }
        public string COD_YEAR_ID { get; set; }
        public string Financial_Year { get; set; }
        public string From { get; set; }
        public string Lock { get; set; }
        public string To { get; set; }
        public bool Default { get; set; }
        public string XCOD_ACC_TYPE { get; set; }
        public DateTime XCOD_YEAR_EDT { get; set; }
        public int XCOD_YEAR_ID { get; set; }
        public string XCOD_YEAR_NAME { get; set; }
        public DateTime XCOD_YEAR_SDT { get; set; }
        public bool SetDefaultPressed_CFY { get; set; }      
    }
}