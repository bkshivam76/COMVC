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
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    [CheckLogin]
    public class GoldSilverController : BaseController
    {
        // GET: Profile/GoldSilver
        #region Global Variables

        public string Voucher_Entry
        {
            get
            {
                return (string)GetBaseSession("Voucher_Entry_GoldSilver");
            }
            set
            {
                SetBaseSession("Voucher_Entry_GoldSilver", value);
            }
        }
        public string Profile_Entry
        {
            get
            {
                return (string)GetBaseSession("Profile_Entry_GoldSilver");
            }
            set
            {
                SetBaseSession("Profile_Entry_GoldSilver", value);
            }
        }
        public List<GoldSilverInfo> GoldSilverExportData
        {
            get
            {
                return (List<GoldSilverInfo>)GetBaseSession("GoldSilverExportData_GoldSilver");
            }
            set
            {
                SetBaseSession("GoldSilverExportData_GoldSilver", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> GoldSilverInfo_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("GoldSilverInfo_DetailGrid_Data_GoldSilver");
            }
            set
            {
                SetBaseSession("GoldSilverInfo_DetailGrid_Data_GoldSilver", value);
            }
        }

        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> GoldSilverInfo_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("GoldSilverInfo_AdditionalInfoGrid_GoldSilver");
            }
            set
            {
                SetBaseSession("GoldSilverInfo_AdditionalInfoGrid_GoldSilver", value);
            }
        }
        public void SetDefaultValues()
        {
            Voucher_Entry = "Voucher Entry";
            Profile_Entry = "Profile Entry";
        }
        #endregion
        public ActionResult Frm_GoldSilver_Info()
        {
            SetDefaultValues();
            GoldSilver_User_Rights();
            if (!CheckRights(BASE, ClientScreen.Profile_GoldSilver, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_GoldSilver').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_GoldSilver).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing GSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            GSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
            GSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            GSProfile.Next_YearID = BASE._next_Unaudited_YearID;
            GSProfile.TableName = Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO;
            DataTable GS_Table = BASE._GoldSilverDBOps.GetProfileListing(GSProfile);
            ViewBag.ActionRights = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Common_Lib.Common.ClientAction.Special_Groupings);
            // Base._GoldSilverDBOps.GetList(Voucher_Entry, Profile_Entry)
            ViewData["GSProfile_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;


            if ((GS_Table == null))
            {
                return View();
            }

            else
            {
                var goldsilverdata = DatatableToModel.DataTabletoGoldSilver_INFO(GS_Table);
                GoldSilverExportData = goldsilverdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(goldsilverdata);
            }
        }

        public ActionResult Frm_GoldSilver_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            GoldSilver_User_Rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (ViewBag.ActionRights == null)
            { ViewBag.ActionRights = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Common_Lib.Common.ClientAction.Special_Groupings); }

            if (GoldSilverExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GetProfileListing GSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
                GSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
                GSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
                GSProfile.Next_YearID = BASE._next_Unaudited_YearID;
                GSProfile.TableName = Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO;
                DataTable GS_Table = BASE._GoldSilverDBOps.GetProfileListing(GSProfile);

                //  Base._VehicleDBOps.GetList(Voucher_Entry, Profile_Entry)
                if ((GS_Table == null))
                {
                }
                else
                {
                    var goldsilverdata = DatatableToModel.DataTabletoGoldSilver_INFO(GS_Table);
                    GoldSilverExportData = goldsilverdata;
                }
            }
            return PartialView("Frm_GoldSilver_Info_Grid", GoldSilverExportData);
        }
        #region <--Nested Grid-->
        public ActionResult Frm_GoldSilver_Info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false, string MID = "")
        {
            ViewBag.GoldSilverInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.GoldSilverInfo_RecID = RecID;
            ViewBag.GoldSilverInfo_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    GoldSilverInfo_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Profile_GoldSilver);
                    Session["GoldSilverInfo_detailGrid_Data"] = GoldSilverInfo_DetailGrid_Data;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Profile_GoldSilver);
                    GoldSilverInfo_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    GoldSilverInfo_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(GoldSilverInfo_DetailGrid_Data);
        }

        public ActionResult AdditionalInfo_Grid()
        {
            return View(GoldSilverInfo_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GoldSilverListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "GoldSilverListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["GoldSilverInfo_detailGrid_Data"];
        }
        #endregion // <--Nested Grid-->

        #region Create detail

        public ActionResult CreationDetail(string Xrow, string Action_Status, string Add_Date, string Add_By,
            string Action_Date, string Action_By, string Edit_Date, string Edit_By)
        {
            if (!string.IsNullOrEmpty(Xrow))
            {
                string Status = "";
                string Lbl_Status = string.Empty;
                string Lbl_StatusOn = string.Empty;
                string Lbl_StatusBy = string.Empty;
                string Pic_Status = string.Empty;
                string Lbl_Create = string.Empty;
                string Lbl_Modify = string.Empty;
                string Lbl_Status_Color = string.Empty;
                string Lbl_By = string.Empty;
                try
                {
                    Status = Action_Status;
                }
                catch (Exception ex)
                {
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    Lbl_Status = "Locked On : " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    Lbl_By = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "" : Action_By.Trim().ToUpper());
                    //Lbl_Status_Color = "blue";
                    Pic_Status = "fa-lock";
                }
                else
                {
                    Pic_Status = "fa-unlock";
                    //Lbl_Status = Status;
                    Lbl_Status = "UnLocked On : " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    Lbl_By = "UnLocked By: " + (string.IsNullOrEmpty(Action_By) ? "" : Action_By.Trim().ToUpper());
                }
                if (IsDate(Add_Date))
                {
                    Lbl_Create = "Add On: " + (string.IsNullOrEmpty(Add_Date) ? "" : Convert.ToDateTime(Add_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Create = "Add On: " + "?, By: " + (string.IsNullOrEmpty(Add_By) ? "" : Add_By.Trim().ToUpper());
                }
                if (IsDate(Edit_Date))
                {
                    Lbl_Modify = "Edit On: " + (string.IsNullOrEmpty(Edit_Date) ? "" : Convert.ToDateTime(Edit_Date).ToString("dd-MM-yyyy hh:mm:ss")) + ", By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                else
                {
                    Lbl_Modify = "Edit On: " + "?, By: " + (string.IsNullOrEmpty(Edit_By) ? "" : Edit_By.Trim().ToUpper());
                }
                if (Status.ToUpper().Trim().ToString() == "LOCKED")
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Locked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Locked On: " + "?";
                    }
                    Lbl_StatusBy = "Locked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                else
                {
                    if (IsDate(Action_Date))
                    {
                        Lbl_StatusOn = "Unlocked On: " + (string.IsNullOrEmpty(Action_Date) ? "" : Convert.ToDateTime(Action_Date).ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Lbl_StatusOn = "Unlocked On: " + "?";
                    }
                    Lbl_StatusBy = "Unlocked By: " + (string.IsNullOrEmpty(Action_By) ? "?" : Action_By.Trim().ToUpper());
                }
                return Json(new
                {
                    Lbl_Status = Lbl_Status,
                    Lbl_Create = Lbl_Create,
                    Lbl_Modify = Lbl_Modify,
                    Lbl_Status_Color = Pic_Status,
                    Lbl_StatusBy = Lbl_StatusBy,
                    Lbl_StatusOn = Lbl_StatusOn,
                    Lbl_By = Lbl_By
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Lbl_Status = "",
                    Lbl_Create = "",
                    Lbl_Modify = "",
                    Lbl_Status_Color = "",
                    Lbl_StatusBy = "",
                    Lbl_StatusOn = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        [HttpGet]
        public ActionResult GetWeight(string weight)
        {
            string Result = "";
            if ((double.Parse(weight) > 0))
            {
                double goldweight = Convert.ToDouble(weight);
                string kg_part;
                string gm_part;
                string mg_part;
                kg_part = "kg";
                gm_part = "gm";
                mg_part = "mg";
                string[] gm_and_mg = string.Format(double.Parse(weight).ToString(), "0.000").ToString().Split('.');
                if ((double.Parse(gm_and_mg[0]) > 0))
                {
                    double gm_and_mgpart = (double.Parse(gm_and_mg[0]) / 1000);
                    string[] kg_and_gm = string.Format(gm_and_mgpart.ToString(), "0.000").ToString().Split('.');
                    if ((double.Parse(kg_and_gm[0]) > 0))
                    {
                        Result = (string.Format(double.Parse(kg_and_gm[0]).ToString(), "#,0")
                                    + (kg_part
                                    + (((double.Parse(kg_and_gm[1].ToString())) > 0) ? (", "
                                    + (double.Parse(kg_and_gm[1]) + gm_part)) : "") + ((double.Parse(gm_and_mg[1]) > 0) ? (" && "
                                    + (double.Parse(gm_and_mg[1]) + mg_part)) : "")));
                    }
                    else
                    {
                        Result = (((double.Parse(kg_and_gm[1]) > 0) ? (double.Parse(kg_and_gm[1]) + gm_part) : "") + ((double.Parse(gm_and_mg[1]) > 0) ? (" && "
                                    + (double.Parse(gm_and_mg[1]) + mg_part)) : ""));
                    }

                }
                else
                {
                    Result = (double.Parse(gm_and_mg[1]) + (" " + mg_part));
                }

            }



            return Json(new
            {
                Result = Result,
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DataNavigation(string ActionMethod = null, string id = null, string YearID = null, string Action_Status = null, string TR_ID = null, string OpenActions = null, DateTime? Edit_Date = null, string Entry_Type = null)
        {

            if (ActionMethod == "New")
            {
                if ((BASE._Completed_Year_Count > 0))
                {
                    return Json(new
                    {
                        Message = "E n t r y   C a n n o t   b e   c r e a t e d. . !" + ("\r\n" + ("\r\n" + "Required Profile Entries have already been created for this centre...!")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                    //"Information...", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else if (!BASE._GoldSilverDBOps.IsTBImportedCentre())
                {
                    return Json(new
                    {
                        Message = "Profile Entries not allowed for a newly opened center",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (BASE.AllowMultiuser())
            {
                if (((ActionMethod == "LOCKED")
                            || ((ActionMethod == "UNLOCKED")
                            || (ActionMethod == "PRINT-LIST"))))
                {

                    string xTemp_ID = id;
                    DataTable d1 = BASE._GoldSilverDBOps.GetRecord(xTemp_ID);
                    if ((d1 == null))
                    {
                        return Json(new
                        {
                            Message = "No Record Found",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((d1.Rows.Count == 0))
                    {
                        return Json(new
                        {
                            Message = "Record Changed / Removed in Background!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    DateTime? RecEdit_Date = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    if ((RecEdit_Date != Edit_Date))
                    {
                        return Json(new
                        {
                            Message = "Record Already Changed!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }
            #region Edit
            if (ActionMethod == "Edit")
            {
                string xTemp_ID = id;
                bool? IsGSCarriedForward = BASE._GoldSilverDBOps.IsGSCarriedForward(xTemp_ID, YearID);
                if ((IsGSCarriedForward == null))
                {
                    // BASE.HandleDBError_OnNothingReturned();
                    //   return;
                }

                if ((IsGSCarriedForward == true
                            && BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID)))
                {
                    return Json(new
                    {
                        Message = ("Entry cannot be edited...!" + ("\r\n" + ("\r\n" + "This entry has been carried forward from previous year(s).Updation(Partial) can be done only after fi" +
                        "nalization of previous year accounts...! "))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, 0, xTemp_ID).Rows.Count > 0))
                {
                    // Bug #5339 fix
                    return Json(new
                    {
                        Message = ("Entry cannot be edited...!" + ("\r\n" + ("\r\n" + "This Asset has already been transferred to another centre."
                        ))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                string xTr_ID = TR_ID;
                if (xTr_ID.Trim() == null)
                {
                    return Json(new
                    {
                        Message = ("Entry cannot be edited / Deleted...!" + ("\r\n" + ("\r\n" + "This Entry Managed by Voucher Entry...!"
                         ))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }

                // #5340 fix
                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                if (!(SaleRecord == null))
                {
                    if ((SaleRecord.Rows.Count > 0))
                    {
                        return Json(new
                        {
                            Message = ("Entry cannot be edited / Deleted...!" + ("\r\n" + ("\r\n" + "This item has already been sold...!"
                            ))),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                string xStatus = Action_Status;
                object value = Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                object MaxValue = 0;
                bool AllowUser = false;
                MaxValue = BASE._GoldSilverDBOps.GetStatus(xTemp_ID);
                if ((MaxValue == null))
                {
                    return Json(new
                    {
                        Message = ("Entry Not Found ?chnged In Background...!"
                        ),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                string multiUserMsg = "";
                if ((value != MaxValue))
                {
                    if ((MaxValue == (object)Common_Lib.Common.Record_Status._Locked))
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "The Record has been locked in the background by another user."));
                    }
                    else if ((MaxValue == (object)Common_Lib.Common.Record_Status._Completed))
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "The Record hase been unlocked in   the  background by aother user"));
                        AllowUser = true;
                    }
                    else
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "Record Status has been changed in the background by another user"));
                        AllowUser = true;
                    }

                    if (AllowUser)
                    {
                        //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                        //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", (multiUserMsg + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                        //{
                        //    xPromptWindow.Dispose();
                        //}
                        //else
                        //{
                        //    xPromptWindow.Dispose();
                        //    Grid_Display();
                        //    return;
                        //}

                    }

                }

                if ((MaxValue == (object)Common_Lib.Common.Record_Status._Locked))
                {
                    return Json(new
                    {
                        Message = ("Locked entry cannot be   Edited ? Deleted... !"
                                    + (multiUserMsg + ("\r\n" + ("\r\n" + ("Note:" + ("\r\n" + ("-------" + ("\r\n" + ("Drop your Request to Madhuban for Unlock this Entry," + ("\r\n" + "If you really want to do some action...!"
                                    ))))))))))
                                    ,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                param.CrossRefId = xTemp_ID;
                param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                DataTable jAdj = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver);
                // #4845 fix
                if ((jAdj == null))
                {
                    // BASE.HandleDBError_OnNothingReturned();
                    //return;
                }

                MaxValue = jAdj.Rows.Count;
                if ((int)MaxValue > 0)
                {
                    return Json(new
                    {
                        Message = ("Entry Cannot be Edited...!" + ("\r\n" + ("\r\n" + "There are journal voucher references posted against it...!")))
                           ,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            #endregion
            #region DELETE
            if (ActionMethod == "Delete")
            {

                string xTemp_ID = id.ToString();
                // If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "YearID").ToString() <> Base._open_Year_ID Then  'NOT Current Year Entry (add check for non-carried fwd record in split data) 
                bool? IsGSCarriedForward;
                IsGSCarriedForward = BASE._GoldSilverDBOps.IsGSCarriedForward(xTemp_ID, YearID);
                if ((IsGSCarriedForward == null))
                {
                    //Base.HandleDBError_OnNothingReturned();
                    //return;
                }

                if ((IsGSCarriedForward == true))
                {

                    return Json(new
                    {
                        Message = "Entry Cannot be deleted..!" + ("\r\n" + ("\r\n" + "This entry has been carried forward from previous year(s)...! ")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, 0, xTemp_ID).Rows.Count > 0))
                {
                    // Bug #5339 fix
                    return Json(new
                    {
                        Message = "Entry Cannot be deleted..!" + ("\r\n" + ("\r\n" + "This Asset has already been transferred to another centre.")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                string xTr_ID = TR_ID;
                if (TR_ID != "")
                {
                    return Json(new
                    {
                        Message = "Entry Cannot be Edited / deleted..!" + ("\r\n" + ("\r\n" + "This Entry Managed by Voucher Entry...!")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // Dim xSale_Status As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sale Status").ToString
                // If xSale_Status.ToUpper <> "UN-SOLD" Then
                //     DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   C a n n  o t   b e   E d i t e d  /   D e l e t e d . . !" & vbNewLine & vbNewLine & "This item has already been sold...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                // End If
                // #5340 fix
                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_ID);
                if (!(SaleRecord == null))
                {
                    if ((SaleRecord.Rows.Count > 0))
                    {
                        return Json(new
                        {
                            Message = "Entry Cannot be Edited / deleted..!" + ("\r\n" + ("\r\n" + "This item has already been sold...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

                int xOpenActions = Convert.ToInt32(OpenActions);
                if ((xOpenActions > 0))
                {
                    return Json(new
                    {
                        Message = "Entry Cannot be deleted..!" + ("\r\n" + ("\r\n" + "There are open actions / queries posted against it...!")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                string xStatus = Action_Status.ToString();
                object value = Convert.ToInt32(Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus)));
                object MaxValue = 0;
                bool AllowUser = false;
                MaxValue = BASE._GoldSilverDBOps.GetStatus(xTemp_ID);
                if ((MaxValue == null))
                {
                    return Json(new
                    {
                        Message = "Entry Cannot Found / Changed In Backround..!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                string multiUserMsg = "";
                if ((value != MaxValue))
                {
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "The Record has been locked in the background by another user."));
                    }
                    else if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed))
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "The record has been unlocked in the background by another user"));
                        AllowUser = true;
                    }
                    else
                    {
                        multiUserMsg = ("\r\n" + ("\r\n" + "Record Status has been changed in the background by another user"));
                        AllowUser = true;
                    }

                    if (AllowUser)
                    {
                        //Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                        //if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", (multiUserMsg + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                        //{
                        //    xPromptWindow.Dispose();
                        //}
                        //else
                        //{
                        //    xPromptWindow.Dispose();
                        //    Grid_Display();
                        //    return;
                        //}

                    }

                }

                if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                {

                    return Json(new
                    {
                        Message = "Locked Entry cannot be Edited / Deleted..!" + (multiUserMsg + ("\r\n" + ("\r\n" + ("Note:" + ("\r\n" + ("-------" + ("\r\n" + ("Drop your Request to Madhuban for Unlock this Entry," + ("\r\n" + "If you really want to do some action...!"))))))))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                param.CrossRefId = xTemp_ID;
                param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                // #5340 fix
                DataTable jAdj = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver);
                if ((jAdj == null))
                {
                }

                MaxValue = jAdj.Rows.Count;
                if (((int)MaxValue > 0))
                {
                    return Json(new
                    {
                        Message = "Entry Cannot be deleted..!" + ("\r\n" + ("\r\n" + "There are journal voucher references posted against it...!")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                //Frm_GoldSilver_Window_Profile xfrm = new Frm_GoldSilver_Window_Profile();
                //xfrm.Text = ("Delete ~ " + this.Text);
                //xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete;
                //xfrm.xID.Text = xTemp_ID;
                //// -----------------------------+
                //// Start : Edit date sent to Check if entry already changed 
                //// -----------------------------+
                //xfrm.Info_LastEditedOn = this.GridView1.GetRowCellValue(this.GridView1.FocusedRowHandle, "Edit Date");
                //// -----------------------------+
                //// End : Edit date sent to Check if entry already changed 
                //// -----------------------------+
                //xfrm.ShowDialog(this);
                //if (((xfrm.DialogResult == DialogResult.OK)
                //            || (xfrm.DialogResult == DialogResult.Retry)))
                //{
                //    xid = xfrm.xID.Text;
                //    Grid_Display();
                //}

                //if (!(xfrm == null))
                //{
                //    xfrm.Dispose();
                //}

                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region Locked
            if (ActionMethod == "Locked")
            {
                if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Common_Lib.Common.ClientAction.Lock_Unlock))
                {
                    int Ctr = 0;
                    string xTemp_ID = id;
                    string xType = Entry_Type;
                    object xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_ID);
                    // Please Note that this is a dependency , in case gs creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                    if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                    {

                        return Json(new
                        {
                            Message = "Entry Created from Vouchers can be Audited from vouchers only...!" + ("\r\n" + ("\r\n" + "Please unselect Entries Created from Voucher ...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xStatus = Action_Status;
                    object value = Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                    object MaxValue = 0;
                    bool AllowUser = false;
                    MaxValue = BASE._GoldSilverDBOps.GetStatus(xTemp_ID);
                    string Msg = "";
                    if ((value != MaxValue))
                    {
                        Msg = "Record Status has been changed in the background by another user";
                        if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed))
                        {
                            AllowUser = true;
                        }

                        //if (AllowUser)
                        //{
                        //    Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();
                        //    if ((DialogResult.Yes == xPromptWindow.ShowDialog("Confirmation...", ("Record has been Unlocked in the background by another user" + ("\r\n" + ("\r\n" + "Do you want to continue...?"))), Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)))
                        //    {
                        //        xPromptWindow.Dispose();
                        //    }
                        //    else
                        //    {
                        //        xPromptWindow.Dispose();
                        //        Grid_Display();
                        //        return;
                        //    }

                        //}

                    }


                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                    {
                        return Json(new
                        {
                            Message = "Already Locked Entries can't be Re-Locked...!" + ("\r\n" + ("\r\n" + "Please unselect already locked Entries ...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                    {

                        return Json(new
                        {
                            Message = "Incomplete Entries can't be Locked...!" + ("\r\n" + ("\r\n" + "Please unselect incomplete Entries or ask Center to Complete it...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((!(xRemarks == null)
                                && !Convert.IsDBNull(xRemarks)))
                    {
                        if (((int)MaxValue > 0))
                        {
                            return Json(new
                            {
                                Message = "Entries with pending queries can't be Locked...!" + ("\r\n" + ("\r\n" + "Please unselect such Entries...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }


                    }

                    xTemp_ID = id;
                    string xid = xTemp_ID;
                    Ctr++;
                    if (!BASE._GoldSilverDBOps.MarkAsLocked(xTemp_ID))
                    {

                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }


                    if ((Ctr > 0))
                    {

                        return Json(new
                        {
                            Message = Common_Lib.Messages.LockedSuccess(Ctr),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        Message = ("Not Allowed..No Rights"),
                        result = "NoLockRights"
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            #endregion
            #region UNLocked
            if (ActionMethod == "UnLocked")
            {
                if (BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Common_Lib.Common.ClientAction.Lock_Unlock))
                {
                    int Ctr = 0;
                    string xTemp_ID = id;
                    string xType = Entry_Type;
                    // Please Note that this is a dependency , in case gs creation voucher is updated, then the status is used as same as voucher only. If this check is to be removed , we must pick fresh rec_status and rec_status_on from database 
                    if ((xType.ToUpper() == Voucher_Entry.ToUpper()))
                    {

                        return Json(new
                        {
                            Message = "Entry Created from Vouchers can be Audited from Vouchers only...!" + ("\r\n" + ("\r\n" + "Please unselect Entries Created from Voucher ...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    string xStatus = Action_Status;
                    object value = Enum.Parse(typeof(Common_Lib.Common.Record_Status), ("_" + xStatus));
                    object MaxValue = 0;
                    bool AllowUser = false;
                    MaxValue = BASE._GoldSilverDBOps.GetStatus(xTemp_ID);
                    string Msg = "";
                    if ((value != MaxValue))
                    {
                        Msg = "Record Status has been changed in the background by another user";
                        if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            AllowUser = true;
                        }

                    }
                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Completed))
                    {
                        return Json(new
                        {
                            Message = "Already Unlocked Entries can't be Re- Unlocked...!" + ("\r\n" + ("\r\n" + "Please unselect already unlocked Entries...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Incomplete))
                    {
                        return Json(new
                        {
                            Message = "Incomplete Entries can't be Unlocked...!" + ("\r\n" + ("\r\n" + "Please unselect incomplete Entries or ask Center to Complete it...!")),
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    // If Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
                    //     If Not Me.GridView1.GetRowCellValue(CurrRowHandle, "Action By").ToString().ToUpper.Equals(Base._open_User_ID.ToUpper) Then
                    //         DevExpress.XtraEditors.XtraMessageBox.Show("R e c o r d s    l o c k e d    b y    o t h e r    u s e r s   c a n   b e   u n l o c k e d   b y   S u p e r U s e r s   O n l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                    //     End If
                    // End If
                    xTemp_ID = id;
                    string xid = xTemp_ID;
                    Ctr++;
                    if (!BASE._GoldSilverDBOps.MarkAsComplete(xTemp_ID))
                    {
                        return Json(new
                        {
                            Message = "Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }


                    if ((Ctr > 0))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.UnlockedSuccess(Ctr),
                            result = true
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new
                    {
                        Message = ("Not Allowed..No Rights"),
                        result = "NoUnLockRights"
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            #endregion
            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Frm_GoldSilver_window(string ActionMethod = null, string id = null, string YearID = null, string Action_Status = null, string Edit_Date = null, string TR_ID = null, string PopupID = "popup_frm_GoldSilver_Window")
        {
            ViewData["GoldS_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            var i = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Profile_GoldSilver, Rights[i]) && ActionMethod == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupID + "','Not Allowed','No Rights');</script>");
                }
            }

            GoldSilverDto model = new GoldSilverDto();
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            Common_Lib.Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = ActionMethod;
            DataTable d1 = BASE._GoldSilverDBOps.GetRecord(id);
            if ((d1 == null))
            {
                return Json(new
                {
                    message = "No Data Found . . . !",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            // -----------------------------+
            // Start : Check if entry already changed 
            // -----------------------------+
            if (BASE.AllowMultiuser())
            {
                if ((Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            || (Tag == Common_Lib.Common.Navigation_Mode._Delete)
                            || (Tag == Common_Lib.Common.Navigation_Mode._View))
                {
                    string viewstr = "";
                    if (Tag == Common_Lib.Common.Navigation_Mode._View)
                    {
                        viewstr = "view";
                    }

                    //if ((Convert.ToDateTime(Edit_Date) != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])))
                    //{

                    //    return Json(new
                    //    {
                    //        message = Common_Lib.Messages.RecordChanged("Current Item", viewstr) +
                    //        "Record Already Changed!!",
                    //        result = false
                    //    }, JsonRequestBehavior.AllowGet);
                    //}
                    // -----------------------------+
                    // End : Check if entry already changed 
                    // -----------------------------+
                    model.LastEditedOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                    model.YearID = d1.Rows[0]["GS_COD_YEAR_ID"].ToString();
                    model.GS_TYPE = d1.Rows[0]["GS_TYPE"].ToString();

                    model.Txt_Others = d1.Rows[0]["GS_OTHER_DETAIL"].ToString();
                    if (Tag != Common_Lib.Common.Navigation_Mode._View)
                    {
                    }
                    model.Txt_Weight = d1.Rows[0]["GS_ITEM_WEIGHT"].ToString();

                    model.ID = id;
                    model.Txt_Amount = d1.Rows[0]["GS_AMT"].ToString();
                    model.Look_ItemList = d1.Rows[0]["GS_ITEM_ID"].ToString();
                    model.Look_MiscList = d1.Rows[0]["GS_DESC_MISC_ID"].ToString();
                    if (!Convert.IsDBNull(d1.Rows[0]["GS_LOC_AL_ID"]))
                    {
                        model.GS_LOC_AL_ID = d1.Rows[0]["GS_LOC_AL_ID"].ToString();
                    }
                }

            }


            return PartialView(model);


        }
        [HttpPost]
        public ActionResult Frm_GoldSilver_window(GoldSilverDto model)
        {// -----------------------------+
         // Start : Check if entry already changed 
         // -----------------------------+
            var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + model.TempActionMethod);
            Common_Lib.Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + model.TempActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            if (BASE.AllowMultiuser())
            {
                if ((Tag) == Common_Lib.Common.Navigation_Mode._New
                            || (Tag) == Common_Lib.Common.Navigation_Mode._Edit
                            || (Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    // Gets PropertyID for the selected location
                    object LB_ID = BASE._AssetLocDBOps.GetPropertyID(model.GS_LOC_AL_ID);
                    if ((LB_ID == null))
                    {
                        return Json(new
                        {
                            Message = "No Record Found",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    // Transfer of location property check #Ref AM13
                    if (!Convert.IsDBNull(LB_ID))
                    {
                        DataTable tblAssetTf = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_LandAndBuilding, 0, LB_ID.ToString());
                        if ((tblAssetTf == null))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((tblAssetTf == null))
                        {

                            return Json(new
                            {
                                Message = "Error!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((tblAssetTf.Rows.Count > 0))
                        {

                            return Json(new
                            {
                                Message = "Entry Cannot be Added/Edited/Deleted...!" + ("\r\n" + ("\r\n" + "This item location Property has already been transferred to another centre.")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    if ((Tag) == Common_Lib.Common.Navigation_Mode._Edit
                                || (Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        DataTable goldsilver_DbOps = BASE._GoldSilverDBOps.GetRecord(model.ID);
                        if ((goldsilver_DbOps == null))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((goldsilver_DbOps.Rows.Count == 0))
                        {

                            return Json(new
                            {
                                Message = "Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((model.LastEditedOn != Convert.ToDateTime(goldsilver_DbOps.Rows[0]["REC_EDIT_ON"])))
                        {

                            return Json(new
                            {
                                Message = "Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        DataTable TR_TABLE = null;
                        if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                        {
                            TR_TABLE = BASE._GoldSilverDBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), Convert.ToString(BASE._prev_Unaudited_YearID));
                        }
                        else
                        {
                            TR_TABLE = BASE._GoldSilverDBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), Convert.ToString(BASE._open_Year_ID));
                        }

                        if ((TR_TABLE == null))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Check for sale of gold/silver item
                        int sale_qty = 0;
                        if ((TR_TABLE.Rows.Count > 0))
                        {
                            sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Weight"]);
                        }

                        if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                        {
                            TR_TABLE = BASE._GoldSilverDBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), Convert.ToString(BASE._open_Year_ID));
                            if ((TR_TABLE == null))
                            {
                                return Json(new
                                {
                                    Message = "No Record Found",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }

                            if ((TR_TABLE.Rows.Count > 0))
                            {
                                sale_qty = sale_qty + Convert.ToInt32(TR_TABLE.Rows[0]["Sale Weight"]);
                            }

                        }

                        if ((sale_qty != 0))
                        {

                            return Json(new
                            {
                                Message = "Entry Cannot be Edited / Deleted...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        int MaxValue = 0;
                        MaxValue = Convert.ToInt32(BASE._GoldSilverDBOps.GetStatus(model.ID));
                        if ((MaxValue == 0))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if (((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked))
                        {
                            return Json(new
                            {
                                Message = "Record Status Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Journal ref dependency check #Ref AP13
                        Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                        param.CrossRefId = model.ID;
                        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                        param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                        // #5340 fix
                        DataTable dAdj = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver);
                        if ((dAdj == null))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        MaxValue = dAdj.Rows.Count;
                        if ((MaxValue > 0))
                        {
                            // there are some journal references

                            return Json(new
                            {
                                Message = "Entry CAnnot be Edited / Deleted...!" + ("\r\n" + ("\r\n" + "There are journal voucher references posted against it...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        // Asset Transfer(To) dependency check #Ref AM13
                        DataTable Tfs = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, 0, model.ID);
                        if ((Tfs == null))
                        {
                            return Json(new
                            {
                                Message = "No Record Found",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        if ((Tfs.Rows.Count > 0))
                        {
                            return Json(new
                            {
                                Message = "Entry CAnnot be Edited / Deleted...!" + ("\r\n" + ("\r\n" + "This Asset has already been transferred to another centre.")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);

                        }

                        // sale of location property check #Ref AO13
                        if (!Convert.IsDBNull(LB_ID))
                        {
                            if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                            {
                                TR_TABLE = BASE._L_B_DBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), BASE._prev_Unaudited_YearID);
                            }
                            else
                            {
                                TR_TABLE = BASE._L_B_DBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), BASE._open_Year_ID);
                            }

                            sale_qty = 0;
                            if ((TR_TABLE.Rows.Count > 0))
                            {
                                sale_qty = Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                            }

                            if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
                            {
                                TR_TABLE = BASE._L_B_DBOps.GetTransactions(Convert.ToString("'" + model.ID + "'"), BASE._open_Year_ID);
                                if ((TR_TABLE.Rows.Count > 0))
                                {
                                    sale_qty = sale_qty + Convert.ToInt32(TR_TABLE.Rows[0]["Sale Quantity"]);
                                }

                            }

                            if ((sale_qty != 0))
                            {
                                return Json(new
                                {
                                    Message = "Entry CAnnot be Edited / Deleted...!" + ("\r\n" + ("\r\n" + "This item location property has already been sold...!")),
                                    result = false
                                }, JsonRequestBehavior.AllowGet);

                            }

                        }

                    }

                    if ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        int openActions = Convert.ToInt32(BASE._Action_Items_DBOps.GetOpenActions(model.ID, "GOLD_SILVER_INFO"));
                        if ((openActions == 0))
                        {
                            //Base.HandleDBError_OnNothingReturned();
                            //return;
                        }

                        if ((openActions > 0))
                        {

                            return Json(new
                            {
                                Message = "Entry CAnnot be  Deleted...!" + ("\r\n" + ("\r\n" + "There are open actions / queries posted against it...!")),
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }

            }

            // -----------------------------+
            // End : Check if entry already changed 
            // -----------------------------+
            if ((Tag) == Common_Lib.Common.Navigation_Mode._New
                        || (Tag) == Common_Lib.Common.Navigation_Mode._Edit)
            {
                // If Base.DateTime_Mismatch Then
                //     Me.DialogResult = Windows.Forms.DialogResult.None
                //     Exit Sub
                // End If
                if ((model.GS_TYPE.Trim().Length == 0))
                {
                    return Json(new
                    {
                        Message = "Type Not Selected",
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }


                if (model.Look_ItemList == null
                            && (Tag) == Common_Lib.Common.Navigation_Mode._New)
                {

                    return Json(new
                    {
                        Message = "Item Name Not Selected",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.Look_MiscList == null))
                {
                    return Json(new
                    {
                        Message = "Description Not Selected",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.Txt_Weight.Trim() == null))
                {
                    return Json(new
                    {
                        Message = "Item Weight cannot be Zero / Negative...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }
                if (Convert.ToDouble(model.Txt_Amount) < 0)
                {

                    return Json(new
                    {
                        Message = "Amount cannot be Negative...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);

                }


                if ((model.GS_LOC_AL_ID.Trim() == null))
                {

                    return Json(new
                    {
                        Message = "Location Not Selected...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


                if ((model.Look_MiscList.ToUpper().Trim() == "OTHERS"))
                {
                    if ((model.Txt_Others.Trim().Length <= 0))
                    {

                        return Json(new
                        {
                            Message = "Other Detail Not Selected...!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                }

            }

            // -----------------------------------// Dependencies //------------------------------------------
            if (BASE.AllowMultiuser())
            {
                if ((Tag == Common_Lib.Common.Navigation_Mode._New)
                            || (Tag == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    // Location Dependency Check #Ref K13, U13
                    DataTable isLocChanged = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, model.GS_LOC_AL_ID, null);
                    if ((isLocChanged == null))
                    {
                        //Base.HandleDBError_OnNothingReturned();
                        //return;
                    }

                    if ((isLocChanged.Rows.Count <= 0))
                    {
                        return Json(new
                        {
                            Message = "Referred Record Already Deleted!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);

                        // Else 'Ref E13
                        //     If Look_LocList.GetColumnValue("REC_EDIT_ON") <> isLocChanged.Rows(0)("REC_EDIT_ON") Then
                        //         DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //         Me.DialogResult = Windows.Forms.DialogResult.Retry
                        //         FormClosingEnable = False : Me.Close()
                        //         Exit Sub
                        //     End If
                    }

                }

            }


            int Status_Action = 0;
            if (model.Chk_Incompleted)
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Incomplete);
            }
            else
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Completed);
            }

            if (model.TempActionMethod == "_Deleted")
            {
                Status_Action = Convert.ToInt32(Common_Lib.Common.Record_Status._Deleted);
            }

            if ((Tag) == Common_Lib.Common.Navigation_Mode._New)
            {
                // new
                Common_Lib.RealTimeService.Parameter_Insert_GoldSilver InParam = new Common_Lib.RealTimeService.Parameter_Insert_GoldSilver();
                InParam.Type = model.GS_TYPE;
                InParam.ItemID = model.Look_ItemList;
                InParam.DescMiscID = model.Look_MiscList;
                InParam.Weight = double.Parse(model.Txt_Weight);
                InParam.Amount = double.Parse(model.Txt_Amount);
                InParam.LocationID = model.GS_LOC_AL_ID;
                InParam.OtherDetails = model.Txt_Others;
                InParam.Status_Action = Status_Action.ToString();
                if (BASE._GoldSilverDBOps.Insert(InParam))
                {

                    return Json(new
                    {
                        Message = "Record Successfully Save",
                        result = true
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        Message = "Error!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            #region Edit
            if (Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                // edit
                // Dim MaxValue As Object = 0 : Dim xID As Integer = 0
                // command.CommandText = "SELECT MAX(CEN_ID) FROM CENTRE_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " cen_ins_id='00001' "
                // MaxValue = command.ExecuteScalar() : If IsDBNull(MaxValue) Then xID = 1 Else xID = Val(MaxValue) + 1
                Common_Lib.RealTimeService.Parameter_Update_GoldSilver UpParam = new Common_Lib.RealTimeService.Parameter_Update_GoldSilver();
                UpParam.Type = model.GS_TYPE;
                UpParam.ItemID = model.Look_ItemList;
                UpParam.DescMiscID = model.Look_MiscList;
                UpParam.Weight = double.Parse(model.Txt_Weight);
                UpParam.Amount = double.Parse(model.Txt_Amount);
                UpParam.LocationID = model.GS_LOC_AL_ID;
                UpParam.OtherDetails = model.Txt_Others;
                // UpParam.Status_Action = Status_Action;
                UpParam.Rec_ID = model.ID;
                if (BASE._GoldSilverDBOps.Update(UpParam))
                {

                    return Json(new
                    {
                        Message = "Record Updated Successfully ",
                        result = true
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    return Json(new
                    {
                        Message = "Error...!",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            #endregion
            #region Delete
            if (Tag == Common_Lib.Common.Navigation_Mode._Delete)
            {
                // DELETE
                // Common_Lib.Prompt_Window xPromptWindow = new Common_Lib.Prompt_Window();

                if (BASE._GoldSilverDBOps.Delete(model.ID))
                {
                    return Json(new
                    {
                        Message = "Record Deleted Successfully!",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Error!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }


            }
            #endregion

            return Json(new
            {
                Message = "",
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('GoldSilver_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        #region DropDown Method
        [HttpGet]
        public ActionResult LookUp_GetItemList(DataSourceLoadOptions loadOptions, string Cmd_Type = null)
        {

            DataTable d1 = BASE._GoldSilverDBOps.GetGoldSilverOpeningProfileItems(Cmd_Type.ToUpper().Trim(), "ID", "Name");
            DataView dview = new DataView(d1);
            var data = DatatableToModel.DataTabletoGSItem(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");

        }
        [HttpGet]
        public ActionResult LookUp_GetMiscList(DataSourceLoadOptions loadOptions)
        {

            DataTable d1 = BASE._GoldSilverDBOps.GetGoldSilverMisc("Name", "ID");
            DataView dview = new DataView(d1);
            var data = DatatableToModel.DataTabletoGSItem(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");

        }
        [HttpGet]
        public ActionResult LookUp_GetLocList(DataSourceLoadOptions loadOptions)
        {

            DataTable d1 = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, null, null);
            DataView dview = new DataView(d1);
            var data = DatatableToModel.DataTabletoASSET_LOCATION_INFO(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");

        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_GoldSilver");
            Session.Remove("GoldSilverInfo_detailGrid_Data");
        }
        public void GoldSilver_User_Rights()
        {
            ViewData["GoldS_Profile_Core_AddRight"] = CheckRights(BASE, ClientScreen.Profile_Core, "Add");
            ViewData["GoldS_AddRight"] = CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Add");
            ViewData["GoldS_UpdateRight"] = CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Update");
            ViewData["GoldS_ViewRight"] = CheckRights(BASE, ClientScreen.Profile_GoldSilver, "View");
            ViewData["GoldS_DeleteRight"] = CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Delete");
            ViewData["GoldS_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Export");
            ViewData["GoldS_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Profile_GoldSilver, Common.ClientAction.Lock_Unlock);
            ViewData["GoldS_AddHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "Add");
            ViewData["GoldS_ListHelp_ActionItemsRight"] = CheckRights(BASE, ClientScreen.Help_Action_Items, "List");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["GS_SpecialGrouping"] = BASE.CheckActionRights(ClientScreen.Profile_GoldSilver, Common.ClientAction.Special_Groupings);
        }

        #region Dev Extreme
        public ActionResult Frm_GoldSilver_Info_dx(bool VouchingMode = false)
        {
            SetDefaultValues();
            GoldSilver_User_Rights();
            if (!CheckRights(BASE, ClientScreen.Profile_GoldSilver, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Profile_GoldSilver').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Profile_GoldSilver).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            Common_Lib.RealTimeService.Param_GetProfileListing GSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            GSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
            GSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            GSProfile.Next_YearID = BASE._next_Unaudited_YearID;
            GSProfile.TableName = Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO;
            DataTable GS_Table = BASE._GoldSilverDBOps.GetProfileListing(GSProfile);
            ViewBag.ActionRights = BASE.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Common_Lib.Common.ClientAction.Special_Groupings);
            ViewData["GSProfile_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;


            if ((GS_Table == null))
            {
                return View();
            }

            else
            {
                var goldsilverdata = DatatableToModel.DataTabletoGoldSilver_INFO(GS_Table);
                GoldSilverExportData = goldsilverdata;
                ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                return View(goldsilverdata);
            }
        }

        public ActionResult Frm_GoldSilver_Info_Grid_dx()
        {
            Common_Lib.RealTimeService.Param_GetProfileListing GSProfile = new Common_Lib.RealTimeService.Param_GetProfileListing();
            GSProfile.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
            GSProfile.Prev_YearId = BASE._prev_Unaudited_YearID;
            GSProfile.Next_YearID = BASE._next_Unaudited_YearID;
            GSProfile.TableName = Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO;
            var GS_Table = BASE._GoldSilverDBOps.GetProfileListing(GSProfile);
            var goldsilverdata = DatatableToModel.DataTabletoGoldSilver_INFO(GS_Table);
            GoldSilverExportData = goldsilverdata;
            System.Diagnostics.Debug.WriteLine("*******************************");
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(GoldSilverExportData));
            return Content(JsonConvert.SerializeObject(GoldSilverExportData), "application/json");
        }

        public ActionResult Frm_GoldSilver_Info_DetailGrid_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, "", ClientScreen.Profile_GoldSilver, !VouchingMode)), "application/json");
        }

        public ActionResult Frm_GoldSilver_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, "", BASE._open_Cen_ID, ClientScreen.Profile_GoldSilver)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx()
        {
            if (!CheckRights(BASE, ClientScreen.Profile_GoldSilver, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Frm_GoldSilver_report_modal_result','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
    }
}