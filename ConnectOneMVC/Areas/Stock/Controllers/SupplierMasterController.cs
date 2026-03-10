using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Controllers;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.SubItems;
using static Common_Lib.DbOperations.StockProfile;
using static Common_Lib.DbOperations.Suppliers;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    [CheckLogin]
    public class SupplierMasterController : BaseController
    {
        // GET: Stock/SupplierMaster
        #region Global Variables
        public List<Return_GetRegister_MainGrid> SupplierMaster_Data_Glob
        {
            get
            {
                return (List<Return_GetRegister_MainGrid>)GetBaseSession("SupplierMaster_Data_Glob_InfoSM");
            }
            set
            {
                SetBaseSession("SupplierMaster_Data_Glob_InfoSM", value);
            }
        }
        public List<Return_GetRegister_NestedGrid> SupplierBank_ExportData
        {
            get
            {
                return (List<Return_GetRegister_NestedGrid>)GetBaseSession("SupplierBank_ExportData_InfoSM");
            }
            set
            {
                SetBaseSession("SupplierBank_ExportData_InfoSM", value);
            }
        }
        public Int32 SupplierID_Glob
        {
            get
            {
                return (Int32)GetBaseSession("SupplierID_Glob_SM");
            }
            set
            {
                SetBaseSession("SupplierID_Glob_SM", value);
            }
        }
        public string SupplierActionMethod
        {
            get
            {
                return (string)GetBaseSession("SupplierActionMethod_SM");
            }
            set
            {
                SetBaseSession("SupplierActionMethod_SM", value);
            }
        }
        public List<Return_GetSupplierBanks> Supplier_BankAccountData
        {
            get
            {
                return (List<Return_GetSupplierBanks>)GetBaseSession("Supplier_BankAccountData_SM");
            }
            set
            {
                SetBaseSession("Supplier_BankAccountData_SM", value);
            }
        }
        public ArrayList Edit_SupplierBank_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Edit_SupplierBank_ID_SM");
            }
            set
            {
                SetBaseSession("Edit_SupplierBank_ID_SM", value);
            }
        }
        public ArrayList Delete_SupplierBank
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_SupplierBank_SM");
            }
            set
            {
                SetBaseSession("Delete_SupplierBank_SM", value);
            }
        }
        public List<Return_GetItemSupplierMapping> SupplierItemMap_Data
        {
            get
            {
                return (List<Return_GetItemSupplierMapping>)GetBaseSession("SupplierItemMap_Data_SMap");
            }
            set
            {
                SetBaseSession("SupplierItemMap_Data_SMap", value);
            }
        }

        public List<DbOperations.StockProfile.Return_GetStockItems> Item_Name_List_Grid_Data //to be checked for get base session
        {
            get
            {
                return (List<DbOperations.StockProfile.Return_GetStockItems>)GetBaseSession("Item_Name_List_Grid_Data_SM");
            }
            set
            {
                SetBaseSession("Item_Name_List_Grid_Data_SM", value);
            }
        }
        #endregion

        #region "Grid"
        public ActionResult Frm_SupplierMaster_Info()
        {
            Supplier_user_rights();           
            if (CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "List"))
            {
                ViewBag.ShowHorizontalBar = 0;
                var SupplierMaster_Data = BASE._StockSupplier_dbops.GetRegister();
                List<Return_GetRegister_MainGrid> Mastergrid = SupplierMaster_Data.main_Register;                
                if (SupplierMaster_Data == null)
                {
                    return View();
                }
                else
                {
                    SupplierMaster_Data_Glob = Mastergrid;
                    ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                    ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                    ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

                    return View(SupplierMaster_Data_Glob);
                }
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Supplier_Master').hide();</script>");
            }
        }


        public ActionResult Frm_SupplierMaster_Info_Grid(string command, int ShowHorizontalBar = 0)
        {
            Supplier_user_rights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            if (SupplierMaster_Data_Glob == null || command == "REFRESH")
            {
                var SupplierMaster_Data = BASE._StockSupplier_dbops.GetRegister();
                if (SupplierMaster_Data != null)
                {
                    var Mastergrid = SupplierMaster_Data.main_Register;
                    var Nestedgrid = SupplierMaster_Data.nested_Register;
                    SupplierMaster_Data_Glob = Mastergrid;
                    SupplierBank_ExportData = Nestedgrid;
                    Session["SupplierBank_ExportData"] = SupplierBank_ExportData;
                }
            }

            List<Return_GetRegister_MainGrid> Mastergrid_data = SupplierMaster_Data_Glob as List<Return_GetRegister_MainGrid>;

            if ((Mastergrid_data == null))
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }


        public PartialViewResult Frm_SupplierMaster_BankDetail_NestedGrid(int SupplierID, string Command)
        {
            SupplierID_Glob = SupplierID;
            ViewData["SupplierID"] = SupplierID_Glob;

            if (SupplierBank_ExportData == null || Command == "REFRESH")
            {
                var SupplierMaster_Data = BASE._StockSupplier_dbops.GetRegister();
                var SupplierBankDetail_Data = SupplierMaster_Data.nested_Register;
                SupplierBank_ExportData = SupplierBankDetail_Data;
            }
            var data = SupplierBank_ExportData as List<Return_GetRegister_NestedGrid>;
            List<Return_GetRegister_NestedGrid> bankdetails = data.FindAll(x => x.SupplierID == SupplierID);
            return PartialView(bankdetails);
        }

        public ActionResult SupplierMasterCustomDataAction(int key = 0)
        {
            var Data = SupplierMaster_Data_Glob as List<Return_GetRegister_MainGrid>;
            string SupplierReg = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();
                if (it != null)
                {
                    SupplierReg = it.Supplier + "![" + it.Company_Code + "![" + it.Registered_No + "![" + it.Contact_Person + "![" +
                            it.PAN_No + "![" + it.Supplier_Address + "![" + it.Country + "![" +
                            it.Suppl_State + "![" + it.City + "![" + it.Email + "![" + it.Other_Details + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.EditedBy + "![" + it.EditedOn + "![" + it.ID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(SupplierReg);
        }

        public ActionResult BankDetailsCustomDataAction(int key = 0)
        {
            var Data = SupplierBank_ExportData as List<Return_GetRegister_NestedGrid>;
            string itstr = "";
            if (Data != null)
            {
                var it = Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Bank + "![" + it.Branch + "![" + it.Account_No + "![" + it.IFSC + "![" + it.SupplierID + "![" + it.AddedBy + "![" + it.AddedOn + "![" + it.EditedBy + "![" + it.EditedOn;
                }
            }

            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }

        public static GridViewSettings SupplierMasterNestedGridSettings(int SupplierID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "SupplierMaster" + SupplierID;
            settings.SettingsDetail.MasterGridName = "SupplierMasterListGrid";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Bank").Visible = true; ;
            settings.Columns.Add("Branch").Visible = true; ;
            settings.Columns.Add("Account_No").Visible = true;
            settings.Columns.Add("IFSC").Visible = true;
            settings.Columns.Add("SupplierID").Visible = false;
            settings.Columns.Add("AddedBy").Visible = false;
            settings.Columns.Add("AddedOn").Visible = false;
            settings.Columns.Add("EditedBy").Visible = false;
            settings.Columns.Add("EditedOn").Visible = false;
            settings.Columns.Add("ID").Visible = false;
            settings.ClientSideEvents.FocusedRowChanged = "OnBankDetailFocusedRowChange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
          //  settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;
            return settings;
        }//settings for nested grid 

        public static IEnumerable GetBankDetail(int SupplierID)
        {
            List<Return_GetRegister_NestedGrid> data = (List<Return_GetRegister_NestedGrid>)System.Web.HttpContext.Current.Session["SupplierBank_ExportData"];
            List<Return_GetRegister_NestedGrid> banklist = data.FindAll(x => x.SupplierID == SupplierID);
            return banklist;
        }//binding data to nested grid
        #endregion
        #region "NEVD"
        public ActionResult Frm_Choose_New()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Add"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Supplier_Choose_New').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#SupplierMasterNew').hide();</script>");
            }
            return PartialView();
        }//radio button to choose for new
        public ActionResult DataNavigation(string ActionMethod, int ID)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Update") && ActionMethod == "Edit")
            {
                return Json(new { result = "NoUpdateRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Delete") && ActionMethod == "Delete")
            {
                return Json(new { result = "NoDeleteRights", Message = "Not Allowed..No Rights" }, JsonRequestBehavior.AllowGet);
            }

            if (ActionMethod == "Delete")
            {

                Return_GetSupplierUsage supp_ref = BASE._StockSupplier_dbops.GetSupplierUsage(ID);
                if (!(supp_ref.screen==null || supp_ref._Date == DateTime.MinValue))//Mantis bug 0001295 fixed
                {
                    return Json(new
                    {
                        Message = "Supplier can not be deleted as it is referred in (" + supp_ref.screen + " ) on (" + supp_ref._Date + " )",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                Message = "",
                result = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_SupplierMaster_Window(string ActionMethod, int ID = 0,string PostSucessFunction= "OnSupplierMasterAjaxSuccessForm", string PopupName= "Dynamic_Content_popup")
        {
            ViewData["Supplier_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Add") && ActionMethod == "New")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#"+ PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "View") && ActionMethod == "View")
            {
                return Content("<script language='javascript' type='text/javascript'>$('#" + PopupName + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }

            //var Navigation_Mode_tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            SupplierID_Glob = ID;
            SupplierActionMethod = ActionMethod;
            SupplierMasterNEVD model = new SupplierMasterNEVD();
            
            model.ActionMethod = ActionMethod;
            model.Supplier_PostSucessFunction = PostSucessFunction == null ? "OnSupplierMasterAjaxSuccessForm" : PostSucessFunction;

            if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                Return_GetSupplierRecord selectedrowdata = BASE._StockSupplier_dbops.GetSupplierRecord(ID);
                if (selectedrowdata != null)
                {
                    if (selectedrowdata.AB_ID == null)
                    {
                        return Json(new
                        {
                            Message = "Supplier record is not present in current year in Address book, Hence this record cannot be shown....!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }//Mantis bug 0000984 fixed
                    else
                    {
                        List<Return_GetPersons> Person_List = BASE._StockSupplier_dbops.GetPersons(selectedrowdata.AB_ID);
                        var New_Person_List = new List<Return_GetPersons>();
                        New_Person_List = Person_List.FindAll(x => x.ID == selectedrowdata.AB_ID);
                        model.Supplier_State = New_Person_List[0].State;
                        model.Supplier_Country = New_Person_List[0].Country;
                        model.Supplier_Address = New_Person_List[0].Address;
                        model.Supplier_City = New_Person_List[0].City;
                        model.Supplier_Pan_No = New_Person_List[0].Pan_No;
                        model.Supplier_EmailID = New_Person_List[0].Email;
                        model.Supplier_ContactNo = New_Person_List[0].ContactNo;
                    }//Mantis bug 0001200 fixed
                    model.SupplierID = ID;
                    model.Supplier_AB_ID = selectedrowdata.AB_ID;
                    model.Supplier_Company_Code = selectedrowdata.Company_Code;
                    model.Supplier_Reg_No = selectedrowdata.Reg_No;
                    model.Supplier_Contact_Person = selectedrowdata.Contact_Person;
                    model.Supplier_Other_Details = selectedrowdata.Other_Details;
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_SupplierMaster_Window(SupplierMasterNEVD model)
        {
            string actionmethod = model.ActionMethod;
            try
            {

                #region "Checking Restriction"
                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    if(BASE._StockSupplier_dbops.GetSupplier_Party_Duplication_Check(model.Supplier_AB_ID, model.SupplierID))
                    {
                        return Json(new
                        {
                            Message = "Supplier can not be added as it already exists." ,
                            result = false,
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                #endregion
                #region "IUD"
                if (actionmethod == "New" || actionmethod == "Edit")
                {
                    Param_InsertSupplier_Txn InParam = new Param_InsertSupplier_Txn();
                    Param_UpdateSupplier_Txn UpParam = new Param_UpdateSupplier_Txn();

                    IUDRemainingData(ref InParam, ref UpParam, model);
                    IUDBankAccount(ref InParam, ref UpParam, actionmethod, model);



                    if (actionmethod == "New")
                    {
                        if (BASE._StockSupplier_dbops.InsertSupplier(InParam))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SaveSuccess,
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SomeError,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (actionmethod == "Edit")
                    {
                        if (BASE._StockSupplier_dbops.UpdateSupplier(UpParam))
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SaveSuccess,
                                result = true
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                Message = Common_Lib.Messages.SomeError,
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (actionmethod == "Delete")
                {
                    if (BASE._StockSupplier_dbops.DeleteSupplier(model.SupplierID))//check it should be supplier id
                    {

                        return Json(new
                        {
                            Message = Common_Lib.Messages.DeleteSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                #endregion
                return Json(new
                {
                    Message = "",
                    result = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    Message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Frm_Export_Options()// list export
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#SupplierMaster_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#SupplierMasterListPreview').hide();</script>");
            }
            return View();
        }
        #region "IUD Functions"

        public void IUDBankAccount(ref Param_InsertSupplier_Txn InParam, ref Param_UpdateSupplier_Txn UpParam, string actionmethod, SupplierMasterNEVD model)
        {
            var bankData = (List<Return_GetSupplierBanks>)Supplier_BankAccountData;
            
            if (bankData != null)
            {
                var insertbank = new List<Param_InsertsupplierBank>();
                for (int i = 0; i < bankData.Count; i++)
                {
                    if (bankData[i].ID == 0)
                    {
                        var InBInfo = new Param_InsertsupplierBank();
                        InBInfo.Supplier_ID = model.SupplierID;
                        // InBInfo.Supplier_ID = Convert.ToInt32(SupplierID_Glob);
                        InBInfo.BANK_ID = bankData[i].Bank_Name_ID;//Mantis bug 0000986 fixed
                        InBInfo.Branch_Name = bankData[i].Branch_Name;
                        InBInfo.Acc_No = bankData[i].Account_No;
                        InBInfo.IFSC_Code = bankData[i].IFSC_Code;
                        insertbank.Add(InBInfo);                        
                    }
                }
                if (actionmethod == "New")
                {
                    InParam.Added_Banks = insertbank.ToArray();
                }
                if (actionmethod == "Edit")
                {
                    if (Edit_SupplierBank_ID != null)
                    {
                        int[] SuppBankEditID = (int[])(Edit_SupplierBank_ID as ArrayList).ToArray(typeof(int));
                        if (SuppBankEditID != null)
                        {
                            var updatebankdet = new List<Param_UpdatesupplierBank>();
                            for (int j = 0; j < SuppBankEditID.Count(); j++)
                            {
                                for (int i = 0; i < bankData.Count; i++)
                                {
                                    if (bankData[i].ID == SuppBankEditID[j])
                                    {

                                        var InEInfo = new Param_UpdatesupplierBank();
                                        InEInfo.ID = bankData[i].ID;
                                        InEInfo.BANK_ID = bankData[i].Bank_Name_ID;//Mantis bug 0000986 fixed
                                        InEInfo.Branch_Name = bankData[i].Branch_Name;
                                        InEInfo.Acc_No = bankData[i].Account_No;
                                        InEInfo.IFSC_Code = bankData[i].IFSC_Code;
                                        updatebankdet.Add(InEInfo);


                                    }
                                }
                            }
                            UpParam.Updated_Banks = updatebankdet.ToArray();
                        }
                    }
                    UpParam.Added_Banks = insertbank.ToArray();
                    if (Delete_SupplierBank != null)
                    {
                        int[] res = (int[])(Delete_SupplierBank as ArrayList).ToArray(typeof(int));
                        UpParam.Deleted_Banks_IDs = res;
                    }
                }
            }
        }

        public void IUDRemainingData(ref Param_InsertSupplier_Txn InParam, ref Param_UpdateSupplier_Txn UpParam, SupplierMasterNEVD model)
        {
            if (model != null && model.ActionMethod == "New")
            {
                InParam.AB_ID = model.Supplier_AB_ID;
                InParam.Company_Code = model.Supplier_Company_Code;
                InParam.Reg_No = model.Supplier_Reg_No;
                InParam.Contact_Person = model.Supplier_Contact_Person;
                InParam.Other_Details = model.Supplier_Other_Details;
            }
            else if (model != null && model.ActionMethod == "Edit")
            {
                UpParam.ID = model.SupplierID;
                UpParam.AB_ID = model.Supplier_AB_ID;
                UpParam.Company_Code = model.Supplier_Company_Code;
                UpParam.Reg_No = model.Supplier_Reg_No;
                UpParam.Contact_Person = model.Supplier_Contact_Person;
                UpParam.Other_Details = model.Supplier_Other_Details;
            }
        }
        #endregion
        #endregion
        #region "DropDown"
        public ActionResult Get_SupplierName_List(DataSourceLoadOptions loadOptions)
        {
            List<Return_GetPersons> Person_List = BASE._StockSupplier_dbops.GetPersons();
            Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", Pan_No = "", ContactNo = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Person_List, loadOptions)));
        }
        #endregion
        #region "Inside Grid"


        public ActionResult SupplierBankAccount_Info_Grid(string ActionMethodName, int SupplierID = 0)
        {
            var BankList = new List<Return_GetSupplierBanks>();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["SupplierID"] = SupplierID;
            ViewData["SupplierActionMethod"] = SupplierActionMethod;
            if (ActionMethodName == "New")
            {
                return PartialView(BankList);
            }            
            if (Supplier_BankAccountData == null)
            {

                List<Return_GetSupplierBanks> BankAccData = BASE._StockSupplier_dbops.GetSupplierBanks(SupplierID);
                if (BankAccData != null)
                {
                    BankList = BankAccData;
                }
                Supplier_BankAccountData = BankList;
            }            
            return PartialView(Supplier_BankAccountData);
        }

        [HttpGet]
        public ActionResult Frm_Supplier_BankAccountWindow(string ActionMethod = null, int SR_No = 0)
        {
            //Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);
            SupplierBankAccountDetails model = new SupplierBankAccountDetails();
            model.ActionMethod = ActionMethod;

            if (ActionMethod == "New")
            {

            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {

                var Sr_No = SR_No;
                var all_data = (List<Return_GetSupplierBanks>)Supplier_BankAccountData;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr_No == Sr_No).FirstOrDefault() : new Return_GetSupplierBanks();
                model.Sr_No = Sr_No;
                model.Supplier_Bank_Name_ID = dataToEdit.Bank_Name_ID != null ? dataToEdit.Bank_Name_ID : "";//Mantis bug 0000986 fixed
                model.SM_BankName = dataToEdit.SM_BankName != null ? dataToEdit.SM_BankName : "";//Mantis bug 0000986 fixed
                model.Supplier_Branch_Name = dataToEdit.Branch_Name != null ? dataToEdit.Branch_Name : "";
                model.Supplier_Account_No = dataToEdit.Account_No != null ? dataToEdit.Account_No : "";
                model.Supplier_IFSC_Code = dataToEdit.IFSC_Code;
                model.ID = dataToEdit.ID;

            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Supplier_BankAccountWindow(SupplierBankAccountDetails model)

        {
            //model.ActionMethod = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());
            var actionmethod = model.ActionMethod;
            List<Return_GetSupplierBanks> gridRows = new List<Return_GetSupplierBanks>();

            //int acc_no = BASE._StockSupplier_dbops.GetSupplierBankAccUsageCount(model.Supplier_Bank_Name, model.Supplier_Account_No);

            //if (acc_no > 0)
            //{
            //    return Json(new
            //    {
            //        Message = "Account number can not be duplicate for same bank",
            //        result = "false",
            //    }, JsonRequestBehavior.AllowGet);
            //}
            var gridRowsCount = 0;
            var LastRowSr = 0;
            var NewSr = LastRowSr + 1;
            if (Supplier_BankAccountData != null)
            {
                gridRows = (List<Return_GetSupplierBanks>)Supplier_BankAccountData;
                gridRowsCount = gridRows.Count;
                LastRowSr = gridRowsCount > 0 ? Convert.ToInt32(gridRows[gridRowsCount - 1].Sr_No) : 0;
                NewSr = LastRowSr + 1;
            }


            if (actionmethod == "New")
            {

                Return_GetSupplierBanks grid = new Return_GetSupplierBanks();
                grid.Sr_No = NewSr;
                grid.AddedOn = DateTime.Now;
                grid.AddedBy = BASE._open_User_ID;
                grid.SM_BankName = model.SM_BankName;
                grid.Bank_Name_ID = model.Supplier_Bank_Name_ID;//Mantis bug 0000986 fixed
                grid.Branch_Name = model.Supplier_Branch_Name;
                grid.Account_No = model.Supplier_Account_No;
                grid.IFSC_Code = model.Supplier_IFSC_Code;
                grid.ID = model.ID;
                gridRows.Add(grid);
            }
            else if (actionmethod == "Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr_No == model.Sr_No);
                if (model.ID != 0)
                {
                    var editSupplierBankID = new ArrayList();
                    var editSupplierBank = Edit_SupplierBank_ID as ArrayList;
                    if (editSupplierBank != null)
                    {
                        editSupplierBank.Add(model.ID);
                        Edit_SupplierBank_ID = editSupplierBank;
                    }
                    else
                    {
                        editSupplierBankID.Add(model.ID);
                        Edit_SupplierBank_ID = editSupplierBankID;
                    }
                }
                dataToEdit.ID = Convert.ToInt32(model.ID);
                dataToEdit.Sr_No = Convert.ToInt32(model.Sr_No);
                dataToEdit.Bank_Name_ID = model.Supplier_Bank_Name_ID;//Mantis bug 0000986 fixed
                dataToEdit.SM_BankName = model.SM_BankName;//Mantis bug 0000986 fixed
                dataToEdit.Branch_Name = model.Supplier_Branch_Name;
                dataToEdit.Account_No = model.Supplier_Account_No;
                dataToEdit.IFSC_Code = model.Supplier_IFSC_Code;

                dataToEdit.AddedOn = DateTime.Now;
                dataToEdit.AddedBy = BASE._open_User_ID;
            }
            Supplier_BankAccountData = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Frm_SupplierBank_Window_Delete_Grid_Record(string ActionMethod, int SR_No = 0, int id = 0)
        {
            var SR = Convert.ToInt16(SR_No);
            var allData = (List<Return_GetSupplierBanks>)Supplier_BankAccountData;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetSupplierBanks();
            if (allData != null)
            {
                allData.Remove(dataToDelete);
            }
            Supplier_BankAccountData = allData;
            if (id != 0)
            {
                var deleteSupplierBankID = new ArrayList();
                var deleteSupplierBank = Delete_SupplierBank as ArrayList;
                if (deleteSupplierBank != null)
                {
                    deleteSupplierBank.Add(id);
                    Delete_SupplierBank = deleteSupplierBank;
                }
                else
                {
                    deleteSupplierBankID.Add(id);
                    Delete_SupplierBank = deleteSupplierBankID;
                }
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options_Bank()// list export
        {
            return View();
        }
        public ActionResult LookUp_GetBankList(DataSourceLoadOptions loadOptions)
        {
            //if (storeid == null)
            //{
            //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<Return_Get_Stocks_Listing>(), loadOptions)), "application/json");
            //}
            var BankData = BASE._StockSupplier_dbops.GetBanks();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(BankData, loadOptions)), "application/json");
        }
        #endregion
        [HttpGet]
        public ActionResult Frm_Supplier_AddNewBankContextWindow(int SupplierID = 0)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Add"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            //Common_Lib.Common.Navigation_Mode Tag = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), ActionMethod);
            SupplierBankAccountDetails model = new SupplierBankAccountDetails();


            SupplierID_Glob = SupplierID;


            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Frm_Supplier_AddNewBankContextWindow(SupplierBankAccountDetails model)

        {
            //model.ActionMethod = (Common_Lib.Common.Navigation_Mode)Enum.Parse(typeof(Common_Lib.Common.Navigation_Mode), model.ActionMethod.ToString());
            var actionmethod = model.ActionMethod;


            //int acc_no = BASE._StockSupplier_dbops.GetSupplierBankAccUsageCount(model.Supplier_Bank_Name, model.Supplier_Account_No);

            //if (acc_no > 0)
            //{
            //    return Json(new
            //    {
            //        Message = "Account number can not be duplicate for same bank",
            //        result = "false",
            //    }, JsonRequestBehavior.AllowGet);
            //}



            int supplierid = Convert.ToInt32(SupplierID_Glob);

            //  var Sr_No = NewSr;
            var AddedOn = DateTime.Now;
            var AddedBy = BASE._open_User_ID;
            var Bank_Name = model.Supplier_Bank_Name_ID;//Mantis bug 0000986 fixed
            var Branch_Name = model.Supplier_Branch_Name;
            var Account_No = model.Supplier_Account_No;
            var IFSC_Code = model.Supplier_IFSC_Code;
            var ID = Convert.ToInt32(model.ID);

            if (BASE._StockSupplier_dbops.InsertSupplierBank(supplierid, Bank_Name, Branch_Name, Account_No, IFSC_Code))

            {
                return Json(new
                {
                    Message = Common_Lib.Messages.SaveSuccess,
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = Common_Lib.Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }



      

        #region"Item Supplier Mapping"
        public ActionResult Frm_Supplier_Item_Mapping_Info_Grid(string command, string ItemCategory = null, int? SupplierID = null, int? ItemID = null)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (SupplierItemMap_Data == null || command == "REFRESH")
            {
                  
                Param_GetItemSupplierMapping inparam = new Param_GetItemSupplierMapping();

                if (ItemID == 0)
                {
                    inparam.ItemID = null;
                }
                else
                {
                    inparam.ItemID = ItemID;
                }

                if (ItemCategory == "")
                {
                    inparam.ItemCategory = null;
                }
                else
                {
                    inparam.ItemCategory = ItemCategory;
                }
                if (SupplierID == 0)
                {
                    inparam.SupplierID = null;
                }
                else
                {
                    inparam.SupplierID = SupplierID;
                }
                var maplist = new List<Return_GetItemSupplierMapping>();
                //if (SupplierItemMap_Data == null)
                //{
                List<Return_GetItemSupplierMapping> SupplierItemMap = BASE._StockSupplier_dbops.GetItemSupplierMapping(inparam);
                if (SupplierItemMap != null)
                {
                    maplist = SupplierItemMap;


                }
                SupplierItemMap_Data = maplist;
            }
            List<Return_GetItemSupplierMapping> Mastergrid_data = SupplierItemMap_Data as List<Return_GetItemSupplierMapping>;
            if (Mastergrid_data == null)
            {
                return PartialView();
            }
            return PartialView(Mastergrid_data);
        }


        public ActionResult Frm_Supplier_Item_Mapping_Info(int? SupplierID = null)
        {
               
                SupplierID_Glob = (int)SupplierID;
       
            SupplierMasterMappingNEVD model = new SupplierMasterMappingNEVD();
            model.SupplierMapping_Supplier = Convert.ToInt32(SupplierID);
            return PartialView(model);
        }


        //public ActionResult Frm_Supplier_Item_Mapping_Info()
        //{
        //    List<Return_GetItemSupplierMapping> SupplierItemMap = BASE._StockSupplier_dbops.GetItemSupplierMapping();
        //    if (SupplierItemMap == null)
        //    {
        //        return PartialView("Frm_Supplier_Item_Mapping_Info_Grid", null);
        //    }
        //    else
        //    {
        //        SupplierItemMap_Data = SupplierItemMap;
        //        ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
        //        ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
        //        ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
        //        return View(SupplierItemMap_Data);
        //    }
        //}

        [HttpGet]
        public ActionResult Frm_Item_Supplier_Add_Mapping_Window(string ActionMethod = null, int itemid = 0, string PostSuccessFunction = null, string PopUpID = "Dynamic_Content_popup")
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Mapping"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            ItemSupplierMapping model = new ItemSupplierMapping();
            model.ActionMethod = ActionMethod;  
            model.IS_PostSuccessFunction = PostSuccessFunction == null ? "OnItemSupplierMappingAjaxSuccessForm" : PostSuccessFunction;
            model.IS_PopUPId = PopUpID == null ? "Dynamic_Content_popup" : PopUpID;
            if(itemid != 0)
            {
                model.IS_Mapping_Item_ID = itemid;
            }

            if (ActionMethod == "New")
            {

            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Item_Supplier_Add_Mapping_Window(ItemSupplierMapping model)
        {
            try
            {
                if (model.IS_Mapping_Remarks != null)
                {
                    model.IS_Mapping_Remarks = model.IS_Mapping_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                int[] supplierid = Array.ConvertAll(model.IS_Mapping_Supplier_ID.Split(','), Convert.ToInt32);
                int subitemid = Convert.ToInt32(model.IS_Mapping_Item_ID);
                string selecteditemid = subitemid.ToString();
                string remarks = model.IS_Mapping_Remarks;
                int flag = 0;
                for (int i = 0; i < supplierid.Count(); i++)
                {
                    if (BASE._StockSupplier_dbops.InsertItemSupplierMapping(supplierid[i], selecteditemid, remarks))
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                        break;
                    }
                }
                if (flag == 1)
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult Frm_Supplier_Item_Add_Mapping_Window(string ActionMethod = null)
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Mapping"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#Dynamic_Content_popup').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }

            SupplierItemMapping model = new SupplierItemMapping();
                model.ActionMethod = ActionMethod;
                if (ActionMethod == "New")
                {

                }
                return PartialView(model);
            
        }
        [HttpPost]
        public ActionResult Frm_Supplier_Item_Add_Mapping_Window(SupplierItemMapping model)
        {
            try
            {
                if (model.SI_Mapping_Remarks != null)
                {
                    model.SI_Mapping_Remarks = model.SI_Mapping_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                //= Array.ConvertAll(.Split(','), Convert.ToInt32);                
                //string[] itemid = model.SI_ItemID.Split(',').Select(x => x.Trim()).ToArray();
                //var alldata = Item_Name_List_Grid_Data as List<DbOperations.StockProfile.Return_GetStockItems>;
                string selecteditemid = model.SI_ItemID;
                int supplierid = Convert.ToInt32(model.SI_Mapping_Supplier_ID);
                string remarks = model.SI_Mapping_Remarks;
                //int flag = 0;
                //for (int i = 0; i < itemid.Length; i++)
                //{
                //    DbOperations.StockProfile.Return_GetStockItems selectedrow = alldata.Find(x => x.Item_ID.ToString() == itemid[i].ToString());

                //     var item = selectedrow.Item_ID;
                //    //check for insert only those who are checked
                if (BASE._StockSupplier_dbops.InsertItemSupplierMapping(supplierid, selecteditemid, remarks))
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = Common_Lib.Messages.SomeError,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
                
             
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpGet]
        public ActionResult Frm_Supplier_Item_Update_Mapping_Window(string ActionMethod = null, int Mappingid = 0)
        {

            UpdateItemSupplierMapping model = new UpdateItemSupplierMapping();
            model.ActionMethod = ActionMethod;
          
                if (ActionMethod == "Edit" || ActionMethod == "View")
                {

                    var ID = Convert.ToInt16(Mappingid);
                    var all_data = (List<Return_GetItemSupplierMapping>)SupplierItemMap_Data;
                    var dataToEdit = all_data != null ? all_data.Where(x => x.Mapping_ID == ID).FirstOrDefault() : new Return_GetItemSupplierMapping();

              model.MappingID = dataToEdit.Mapping_ID;
                    model.UIS_Mapping_Item_ID = dataToEdit.ItemID;
                    model.USI_Mapping_Supplier_ID = dataToEdit.SupplierID; 
                    model.UIS_Mapping_Remarks = dataToEdit.Remarks;
                   
                }

                return View(model);
        }
          
                         
        [HttpPost]
        public ActionResult Frm_Supplier_Item_Update_Mapping_Window(UpdateItemSupplierMapping model)
        {
            try
            {
                if (model.UIS_Mapping_Remarks != null)
                {
                    model.UIS_Mapping_Remarks = model.UIS_Mapping_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }
                int itemid = Convert.ToInt32(model.UIS_Mapping_Item_ID);
                int supplierid = Convert.ToInt32(model.USI_Mapping_Supplier_ID);
                string remarks = model.UIS_Mapping_Remarks;
                int mappingid = model.MappingID;
               
                    if (BASE._StockSupplier_dbops.UpdateItemSupplierMapping(supplierid, itemid, remarks,mappingid))
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SaveSuccess,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete_Supplier_Mapping(string ActionMethod, int Mappingid = 0)
        {

            if (BASE._StockSupplier_dbops.DeleteItemSupplierMapping(Mappingid))
            {

                return Json(new
                {
                    Message = Common_Lib.Messages.DeleteSuccess,
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Message = Common_Lib.Messages.SomeError,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    
        public ActionResult Get_SupplierMappingItemCategory(DataSourceLoadOptions loadOptions)
        {

            List<Return_GetMainCategoriesMaster> Item_category = BASE._Sub_Item_DBOps.GetMainCategoriesMaster();
            // Item_category.Insert(0, new Return_GetAllSuppliers { Supplier = "Please Select Supplier Name...", ContactPerson = "", Contact_No = "", CompanyCode = "", PAN = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Item_category, loadOptions)));
        }
        public ActionResult Get_SupplierMappingSupplier(DataSourceLoadOptions loadOptions)
        {

            List<Return_GetAllSuppliers> Map_Supplier = BASE._StockSupplier_dbops.GetAllSuppliers(null);
            // Item_category.Insert(0, new Return_GetAllSuppliers { Supplier = "Please Select Supplier Name...", ContactPerson = "", Contact_No = "", CompanyCode = "", PAN = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Map_Supplier, loadOptions)));
        }

        public ActionResult Get_Update_SI_Supplier(DataSourceLoadOptions loadOptions)
        {

            List<Return_GetAllSuppliers> Map_Supplier = BASE._StockSupplier_dbops.GetAllSuppliers(null);
            // Item_category.Insert(0, new Return_GetAllSuppliers { Supplier = "Please Select Supplier Name...", ContactPerson = "", Contact_No = "", CompanyCode = "", PAN = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Map_Supplier, loadOptions)));
        }
        public ActionResult Get_SI_SupplierDetails(DataSourceLoadOptions loadOptions)
        {

            List<Return_GetAllSuppliers> Map_Supplier = BASE._StockSupplier_dbops.GetAllSuppliers(null);
            // Item_category.Insert(0, new Return_GetAllSuppliers { Supplier = "Please Select Supplier Name...", ContactPerson = "", Contact_No = "", CompanyCode = "", PAN = "" });
            // Person_List.Insert(0, new Return_GetPersons { Name = "Please Select Person Name...", City = "", State = "", Country = "", Pan_No = "", ContactNo = "", ID = "" });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Map_Supplier, loadOptions)));
        }

        public ActionResult Get_SupplierMappingItemName(DataSourceLoadOptions loadOptions, string itemcategoryID)//Mantis bug 0000992 fixed
        {
            Param_GetStockItems inparam = new Param_GetStockItems();
            inparam.Main_Category = itemcategoryID;
            List<SubItems.Return_GetStockItems> Item_name = BASE._Sub_Item_DBOps.GetStockItems(inparam);//Mantis bug 0000992 fixed
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Item_name, loadOptions)));
        }


        public ActionResult Get_Update_Supp_Map_ItemName(DataSourceLoadOptions loadOptions)
        {
            List<Common_Lib.DbOperations.StockProfile.Return_GetStockItems> Item_name = BASE._Stock_Profile_DBOps.GetStockItems();
          
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Item_name, loadOptions)));
        }
        public ActionResult Get_IS_ItemDetails(DataSourceLoadOptions loadOptions, string itemcategoryID)//Mantis bug 0000992 fixed
        {
            Param_GetStockItems inparam = new Param_GetStockItems();
            inparam.Main_Category = itemcategoryID;
            List<SubItems.Return_GetStockItems> Item_name = BASE._Sub_Item_DBOps.GetStockItems(inparam);//Mantis bug 0000992 fixed
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Item_name, loadOptions)));
        }
        public ActionResult Lookup_AddItemSupplierMapping_Supplier(DataSourceLoadOptions loadOptions)
        {
            int? NullVariable=null;
            List<Return_GetAllSuppliers> Map_Supplier = BASE._StockSupplier_dbops.GetAllSuppliers(NullVariable);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Map_Supplier, loadOptions)));
        }

        public ActionResult Lookup_AddSupplierItemMapping_ItemName(DataSourceLoadOptions loadOptions)
        {

            List<Common_Lib.DbOperations.StockProfile.Return_GetStockItems> Item_name = BASE._Stock_Profile_DBOps.GetStockItems();
            //Item_name.Insert(0, new Common_Lib.DbOperations.StockProfile.Return_GetStockItems { Stock_Item_Name = "Please Select Item Name...", Item_Category = "", Item_Code = "", Item_ID = 0 });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Item_name, loadOptions)));
        }


        public ActionResult FindItemGridKeyValue()
        {
            var griddata = Item_Name_List_Grid_Data as List<DbOperations.StockProfile.Return_GetStockItems>;
            if (griddata != null)
            {
                string[] gridkey = new string[griddata.Count];
                for (int i = 0; i < griddata.Count; i++)
                {
                    gridkey[i] = Convert.ToString(griddata[i].Item_ID);
                }
                return Json(new { result = true, data = gridkey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ItemNameListGrid()

        {
            List<Common_Lib.DbOperations.StockProfile.Return_GetStockItems> Item_name = BASE._Stock_Profile_DBOps.GetStockItems();
            GridViewSelectAllCheckBoxMode selectAllMode = GridViewSelectAllCheckBoxMode.AllPages;
            Item_Name_List_Grid_Data = Item_name;
            ViewBag.SelectAllCheckBoxMode = selectAllMode;
            return PartialView("ItemNameListGrid", Item_name);
        }
 
        #endregion

        #region"MISC"
        public void SMSessionclear()
        {
            Session.Remove("SupplierBank_ExportData");
            ClearBaseSession("_SM");
        } //clears session variable on popup close  

        public void SMapSessionclear()
        {
             ClearBaseSession("_SMap");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_InfoSM");
        }
        public void Supplier_user_rights()
        {
            ViewData["Supplier_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Add");
            ViewData["Supplier_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Update");
            ViewData["Supplier_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "View");
            ViewData["Supplier_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Delete");
            ViewData["Supplier_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Export");
            ViewData["Supplier_ReportRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Report");
            ViewData["Supplier_MappingRight"] = CheckRights(BASE, ClientScreen.Stock_Supplier_Master, "Mapping");
            ViewData["Supplier_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
        }
        #endregion
    }
}