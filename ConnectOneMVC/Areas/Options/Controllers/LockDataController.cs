using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Options.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.DataRestriction;

namespace ConnectOneMVC.Areas.Options.Controllers
{
    [CheckLogin]
    public class LockDataController : BaseController
    {
        public List<Return_Party> PartyList
        {
            get
            {
                return (List<Return_Party>)GetBaseSession("PartyList_LockDataWindow");
            }
            set
            {
                SetBaseSession("PartyList_LockDataWindow", value);
            }
        }
        public List<BankList> BankList
        {
            get
            {
                return (List<BankList>)GetBaseSession("BankList_LockDataWindow");
            }
            set
            {
                SetBaseSession("BankList_LockDataWindow", value);
            }
        }
        public List<Report_Ledgers.LedgerList> LedgerList
        {
            get
            {
                return (List<Report_Ledgers.LedgerList>)GetBaseSession("LedgerList_LockDataWindow");
            }
            set
            {
                SetBaseSession("LedgerList_LockDataWindow", value);
            }
        }
        public DataTable ListData
        {
            get
            {
                return (DataTable)GetBaseSession("ListData_LockDataInfo");
            }
            set
            {
                SetBaseSession("ListData_LockDataInfo", value);
            }
        }
        public ActionResult Frm_LockData_Info()
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            var userid = BASE._open_User_ID;
            ListData = BASE._DataRestriction_DBOps.DataRestriction_GetList();
            ViewBag.ShowHorizontalBar = 0;
            return View(ListData);
        }
        public ActionResult Frm_LockData_Info_Grid(string command, int ShowHorizontalBar = 0,string Layout=null)
        { 
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["Layout"] = Layout;
            if (command == "REFRESH"|| ListData==null)
            {
                ListData = BASE._DataRestriction_DBOps.DataRestriction_GetList();
            }
            return View(ListData);
        }
        public ActionResult Frm_LockData_window(string ActionMethod,int ID=0)
        {
            Model_Lockdata model = new Model_Lockdata();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.TempActionMethod = model.ActionMethod.ToString();
            RefreshPartyList();
            RefreshBankList();
            RefreshLedgerList();
            ViewBag.MaxDate = BASE._open_Year_Edt;
            model.LockType_LockData = "READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION";
            if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_View" || model.TempActionMethod == "_Delete")
            {
                model.RecID_LockData = ID;
                var d1 = BASE._DataRestriction_DBOps.GetRecord(ID);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    model.LockType_LockData = d1.Rows[0]["CR_TYPE"].ToString();
                    if (!Convert.IsDBNull(d1.Rows[0]["CR_FROMDATE"]))
                    {
                        model.PeriodFrom_LockData = Convert.ToDateTime(d1.Rows[0]["CR_FROMDATE"]);
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["CR_TODATE"]))
                    {
                        model.PeriodTo_LockData = Convert.ToDateTime(d1.Rows[0]["CR_TODATE"]);
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["CR_RESTRICTION_REMARKS"]))
                    {
                        model.Remarks_LockData = d1.Rows[0]["CR_RESTRICTION_REMARKS"].ToString();
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["CR_RESTRICTION_REF_ID"]))
                    {
                        if (!Convert.IsDBNull(d1.Rows[0]["CR_RESTRICTION_TYPE"]))
                        {
                            var Type = d1.Rows[0]["CR_RESTRICTION_TYPE"].ToString().ToUpper();
                            if (Type == "PARTY")
                            {
                                model.PartyID_LockData = d1.Rows[0]["CR_RESTRICTION_REF_ID"].ToString();
                            }
                            else if (Type == "BANK")
                            {
                                model.BankID_LockData = d1.Rows[0]["CR_RESTRICTION_REF_ID"].ToString();
                            }
                            else if (Type == "LEDGER")
                            {
                                model.LedgerID_LockData= d1.Rows[0]["CR_RESTRICTION_REF_ID"].ToString();
                            }
                        }
                    }

                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_LockData_window(Model_Lockdata model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
             
                if (model.LockType_LockData == "READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION")
                {
                    if (model.PeriodFrom_LockData == null)
                    {
                        jsonParam.message = "Period From Is Required...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodFrom_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodTo_LockData == null)
                    {
                        jsonParam.message = "Period To Is Required...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodTo_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodFrom_LockData > BASE._open_Year_Edt)
                    {
                        jsonParam.message = "Period From Cannot Be Higher Than End Date Of Financial Year...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodFrom_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodFrom_LockData < BASE._open_Year_Sdt)
                    {
                        jsonParam.message = "Period From Must Not Be Earlier Than Start Financial Year...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodFrom_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodTo_LockData > BASE._open_Year_Edt)
                    {
                        jsonParam.message = "Period From Cannot Be Higher Than End Date Of Financial Year...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodTo_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodTo_LockData < BASE._open_Year_Sdt)
                    {
                        jsonParam.message = "Period From Must Not Be Earlier Than Start Financial Year...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodTo_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.PeriodTo_LockData < model.PeriodFrom_LockData)
                    {
                        jsonParam.message = "Period To Must Be Greater Than Period From...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodTo_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string RestrictedID = null;
                string RestrictionOn = null;
                if (!string.IsNullOrWhiteSpace(model.PartyID_LockData))
                {
                    RestrictedID = model.PartyID_LockData;
                    RestrictionOn = "PARTY";
                }
                else if (!string.IsNullOrWhiteSpace(model.BankID_LockData))
                {
                    RestrictedID = model.BankID_LockData;
                    RestrictionOn = "BANK";
                }
                else if (!string.IsNullOrWhiteSpace(model.LedgerID_LockData))
                {
                    RestrictedID = model.LedgerID_LockData;
                    RestrictionOn = "LEDGER";
                }
                model.Remarks_LockData = model.Remarks_LockData ?? "";
                if (model.TempActionMethod == "_New")
                {
                    var LockType = (LockType)Enum.Parse(typeof(LockType), model.LockType_LockData);
                    Parameter_Insert_DataRestriction Inparam = new Parameter_Insert_DataRestriction();
                    Inparam.PeriodFrom = model.PeriodFrom_LockData;
                    Inparam.PeriodTo = model.PeriodTo_LockData;
                    Inparam.RestrictedID = RestrictedID;
                    Inparam.RestrictionOn = RestrictionOn;
                    Inparam.Remarks = model.Remarks_LockData;
                    Inparam.LockType = LockType;

                    String RetMessage = BASE._DataRestriction_DBOps.InsertDataRestrictions(Inparam);
                    if (RetMessage.Length > 0)
                    {
                        jsonParam.message = RetMessage;
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "PeriodTo_LockData";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = "Success..";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (model.TempActionMethod == "_Edit")
                {
                    var LockType = (LockType)Enum.Parse(typeof(LockType), model.LockType_LockData);
                    Parameter_Update_DataRestriction UpParam = new Parameter_Update_DataRestriction();
                    UpParam.PeriodFrom = model.PeriodFrom_LockData;
                    UpParam.PeriodTo = model.PeriodTo_LockData;
                    UpParam.RestrictedID = RestrictedID;
                    UpParam.RestrictionOn = RestrictionOn;
                    UpParam.LockType = LockType;
                    UpParam.Remarks = model.Remarks_LockData;
                    UpParam.RECID = model.RecID_LockData;
                    String RetMessage = BASE._DataRestriction_DBOps.UpdateDataRestrictions(UpParam);
                    if (RetMessage.Length == 0)
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = RetMessage;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.TempActionMethod == "_Delete")
                {
                    if (BASE._DataRestriction_DBOps.Delete(model.RecID_LockData))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam                           
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Export_Options()
        {
            return View();
        }
        public ActionResult RefreshPartyList()
        {
            var d1 = BASE._Payment_DBOps.GetParties("Name", "ID");
            if (d1 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));
            PartyList = d1;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetPartyList(DataSourceLoadOptions loadOptions)
        {
            if (PartyList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_Party>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PartyList, loadOptions)), "application/json");
        }
        public ActionResult RefreshBankList()
        {
            DataTable BA_Table = BASE._Cash_Bank_DBOps.GetBankAccounts();
            if (BA_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if ((Branch_IDs.Trim().Length > 0))
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if ((Branch_IDs.Trim().Length == 0))
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._Cash_Bank_DBOps.GetBranchDetails(Branch_IDs);
            if ((BB_Table == null))
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            var BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new BankList
                             {
                                 BANK_NAME = B["Name"].ToString(),
                                 BI_SHORT_NAME = B["BI_SHORT_NAME"].ToString(),
                                 BANK_BRANCH = B["Branch"].ToString(),
                                 BANK_ACC_NO = A["BA_ACCOUNT_NO"].ToString(),
                                 BA_ID = A["ID"].ToString(),
                                 REC_EDIT_ON = Convert.ToDateTime(A["REC_EDIT_ON"])
                             });

            var Final_Data = BuildData.ToList();
            Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            BankList = Final_Data;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_GetBankList(DataSourceLoadOptions loadOptions)
        {
            if (BankList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<BankList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BankList, loadOptions)), "application/json");
        }
        public void RefreshLedgerList()
        {
            LedgerList = BASE._Reports_Ledgers_DBOps.GetLedgerList().OrderBy(o => o.Name).ToList();
        }
        public ActionResult Fill_Ledgers(DataSourceLoadOptions loadOptions)
        {
            if (LedgerList == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Report_Ledgers.LedgerList>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(LedgerList, loadOptions)), "application/json");
        }
        public void SessionClearWindow()
        {
            ClearBaseSession("_LockDataWindow");
        }
        public void SessionClear()
        {
            ClearBaseSession("_LockDataInfo");
        }
    }
}