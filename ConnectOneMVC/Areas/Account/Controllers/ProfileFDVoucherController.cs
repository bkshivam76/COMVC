using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class ProfileFDVoucherController : BaseController
    {
        Common_Lib.RealTimeService.Parameter_InsertFD_VoucherFD InFD;
        Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD InFDHty;
        Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD InRenFDHis;
        Common_Lib.RealTimeService.Parameter_UpdateFD_VoucherFD UpFD;
        Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD UpFDHis;
        Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD UpRenFDHty;
        public Common_Lib.Common.FDAction? iAction = 0;
        string _action;
        string Status = "";
        // GET: Account/ProfileFDVoucher
        public ActionResult Frm_FD_Window(FdVoucher profilemodel)
        {
            foreach (var key in ModelState.Keys.ToList().Where(key => ModelState.ContainsKey(key)))
            {
                ModelState[key].Errors.Clear(); //This is my new solution. Thanks bbak
            }

            return View(profilemodel);
        }

        [HttpPost]
        public ActionResult Frm_FD_Window_Post(FdVoucher model)
        {
            Common_Lib.Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + model.TempActionMethod);
            iAction = model.iAction;

            if (iAction == Common_Lib.Common.FDAction.Renew_FD)
            {
                _action = Common_Lib.Common.FDAction.Renew_FD.ToString();
            }
            if (iAction == Common_Lib.Common.FDAction.New_FD)
            {
                _action = Common_Lib.Common.FDAction.New_FD.ToString();
            }
            if (iAction == Common_Lib.Common.FDAction.Close_FD)
            {
                _action = Common_Lib.Common.FDAction.Close_FD.ToString();
            }

            if ((((model.ActionMethod) == Common_Lib.Common.Navigation_Mode._New)
                        || ((model.ActionMethod) == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                        || (model.ActionMethod) == Common_Lib.Common.Navigation_Mode._Edit))
            {
                if ((Convert.ToDouble(model.TXT_INTEREST) >= 15))
                {
                    return Json(new
                    {
                        message = "Please check the Rate of Interest...!" + ("\r\n" + ("\r\n" + "Do you want to Continue...? ")),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Bank as blank
                if (model.FD_List_BA_ID.ToString().Trim().Length <= 0)
                {
                    return Json(new
                    {
                        message = "Bank Name Not Selected...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD NO as blank
                if ((model.FD_List_FD_NO.Length == 0))
                {
                    return Json(new
                    {
                        message = "Please Enter F.D. Account No....!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD date as blank
                if ((IsDate(model.FD_List_FD_DATE.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect/Blank...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if (Convert.ToDateTime(model.FD_List_FD_DATE) < Convert.ToDateTime(BASE._open_Year_Sdt)
                    || Convert.ToDateTime(model.FD_List_FD_DATE) > Convert.ToDateTime(BASE._open_Year_Edt))

                {
                    return Json(new
                    {
                        message = "Date not as per Financial Year...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD As date as blank
                if ((IsDate(model.FD_AS_DATE.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect/Blank...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((model.FD_AS_DATE > model.FD_List_FD_DATE))
                {
                    return Json(new
                    {
                        message = "As of Date must be Equal/Lower than to F.D. Date..",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // commented to allow premature renewal
                // If IsDate(Me.Txt_As_Date.Text) = True And IsDate(Me.MatDate) = True And Convert.ToDateTime(Me.Txt_As_Date.Text) < Me.MatDate Then
                //     Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                //     Me.ToolTip1.Show("A s   o f   D a t e   m u s t   b e   E q u a l   /   G r e a t e r  t h a n   M a t u r i t y  D a t e  o f  R e n e w e d  F D. . . !", Me.Txt_As_Date, 0, Me.Txt_As_Date.Height, 5000)
                //     Me.Txt_As_Date.Focus()
                //     Me.DialogResult = Windows.Forms.DialogResult.None
                //     Exit Sub
                // Else
                //     Me.ToolTip1.Hide(Me.Txt_As_Date)
                // End If
                // check FD Amount as blank
                if ((Convert.ToDouble(model.Txt_Amount) <= 0))
                {
                    return Json(new
                    {
                        message = "FD Amount cannot be Zero/Negative...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Rate as blank
                if (((Convert.ToDouble(model.TXT_INTEREST) <= 0)
                            || (Convert.ToDouble(model.TXT_INTEREST) > 100)))
                {
                    return Json(new
                    {
                        message = "FD Rate Incorrect...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Maturity Amount as blank
                if ((Convert.ToDouble(model.FD_List_MATURITY_AMOUNT) <= 0))
                {
                    return Json(new
                    {
                        message = "Maturity Amount cannot be Zero/Negative...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((Convert.ToDouble(model.FD_List_MATURITY_AMOUNT) < Convert.ToDouble(model.Txt_Amount)))
                {
                    return Json(new
                    {
                        message = "Maturity Amount Not Less than to F.D. Amount...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Maturity date as blank
                if ((IsDate(model.FD_List_MATURITY_DATE.ToString()) == false))
                {
                    return Json(new
                    {
                        message = "Date Incorrect/Blank...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                // check FD Maturity date > FD Date
                if ((model.FD_List_MATURITY_DATE <= model.FD_List_FD_DATE))
                {
                    return Json(new
                    {
                        message = "Maturity Date Must Be Greater Than F.D. Date...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            // CHECKING LOCK STATUS
            if (model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
            {
                int? MaxValue = 0;
                MaxValue = Convert.ToInt32(BASE._FD_Voucher_DBOps.GetStatusByID(model.xID));
                if ((MaxValue == null))
                {
                    return Json(new
                    {
                        message = "Entry Not Found...!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if ((MaxValue == (int)Common_Lib.Common.Record_Status._Locked))
                {
                    return Json(new
                    {
                        message = "L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" + ("\r\n" + ("\r\n" + ("Note:" + ("\r\n" + ("-------" + ("\r\n" + ("Drop your Request to Madhuban for Unlock this Entry," + ("\r\n" + "If you really want to do some action...!")))))))),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            // CHECKING duplicate Acc no
            if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
                        || model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit)
            {
                int? MaxValue = 0;
                MaxValue = Convert.ToInt32(BASE._FD_Voucher_DBOps.GetAccountNoCount(model.xID, model.FD_List_FD_NO, model.FD_List_BA_ID));
                if ((MaxValue == null))
                {
                    return View();
                }

                if (((MaxValue > 0)
                            && !model.FD_List_FD_NO.Trim().ToUpper().Equals(model.FD_List_FD_NO)))
                {
                    return Json(new
                    {
                        message = "Same Account No. already exists...!" + ("\r\n" + "Do you want to Continue...?"),
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            // '-------------------------- // Start Dependencies // ----------------------------
            // If Base.AllowMultiuser() Then
            //     If Look_BankList.Tag.ToString.Length > 0 Then
            //         Dim cnt As Object = Base._FD_Voucher_DBOps.GetFDBankAccounts(Look_BankList.Tag).Rows.Count
            //         If cnt Is Nothing Then
            //             Base.HandleDBError_OnNothingReturned()
            //             Exit Sub
            //         End If
            //         If cnt <= 0 Then
            //             DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //             Exit Sub
            //         End If
            //     End If
            // End If

            //string Status_Action = "";
            if (model.Chk_Incompleted)
            {
                model.Status_Action = ((int)Common_Lib.Common.Record_Status._Incomplete).ToString();
            }
            else
            {
                model.Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
            }

            if ((model.ActionMethod == Common.Navigation_Mode._Delete))
            {
                model.Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
            }

            try
            {
                if (((model.ActionMethod == Common_Lib.Common.Navigation_Mode._New)
                            || (model.ActionMethod == Common_Lib.Common.Navigation_Mode._New_From_Selection)))
                {
                    // new
                    model.xID = System.Guid.NewGuid().ToString();
                    string xRenewFrom = "NULL";
                    if ((model.iAction == Common_Lib.Common.FDAction.Renew_FD))
                    {
                        xRenewFrom = ("\'"
                                    + (model.FD_List + "\'"));
                    }

                    InFD = new Common_Lib.RealTimeService.Parameter_InsertFD_VoucherFD();
                    InFD.BankAccID = model.FD_List_BA_ID;
                    InFD.FDNo = model.FD_List_FD_NO;
                    if (IsDate(model.FD_List_FD_DATE.ToString()))
                    {
                        InFD.FDDate = Convert.ToDateTime(model.FD_List_FD_DATE).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDDate = model.FD_List_FD_DATE.ToString();
                    }

                    // InFD.FDDate = Me.Txt_Date.Text.Trim
                    if (IsDate(model.FD_AS_DATE.ToString()))
                    {
                        InFD.FDAsDate = Convert.ToDateTime(model.FD_AS_DATE.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDAsDate = model.FD_AS_DATE.ToString();
                    }

                    // InFD.FDAsDate = Me.Txt_As_Date.Text
                    InFD.FDAmount = Convert.ToDouble(model.Txt_Amount);
                    InFD.FDIntRate = Convert.ToDouble(model.TXT_INTEREST);
                    InFD.PaymentCondition = model.Cmd_Mode;
                    if (IsDate(model.FD_List_MATURITY_DATE.ToString()))
                    {
                        InFD.FDMaturityDate = Convert.ToDateTime(model.FD_List_MATURITY_DATE.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InFD.FDMaturityDate = model.FD_List_MATURITY_DATE.ToString();
                    }

                    // InFD.FDMaturityDate = Me.Txt_Mat_Date.Text
                    InFD.FDMaturityAmount = double.Parse(model.FD_List_MATURITY_AMOUNT.ToString());
                    InFD.Remarks = model.Txt_Remarks;
                    InFD.TxnID = model.xMID;
                    InFD.RenewFrom_ID = xRenewFrom;
                    // InFD.FDStatus = Common_Lib.Common.FDStatus.New_FD 'Bug #4353 Fix
                    InFD.Status_Action = model.Status_Action;
                    InFD.RecID = model.xID;
                    // Shifted to FD Voucher
                    // If Not Base._FD_Voucher_DBOps.InsertFD(InFD) Then
                    //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //     Exit Sub
                    // End If
                    InFDHty = new Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD();
                    InFDHty.FDID = model.xID;
                    InFDHty.FDAction = _action;
                    InFDHty.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString();
                    InFDHty.TxnID = model.xMID;
                    InFDHty.Status_Action = model.Status_Action;
                    InFDHty.RecID = Guid.NewGuid().ToString();
                    // Shifted to FD Voucher
                    // If Not Base._FD_Voucher_DBOps.InsertFDHistory(InFDHty) Then
                    //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //     Exit Sub
                    // End If
                    if ((iAction == Common_Lib.Common.FDAction.Renew_FD))
                    {
                        InRenFDHis = new Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD();
                        InRenFDHis.FDID = model.FD_List;
                        InRenFDHis.FDAction = Common_Lib.Common.FDAction.Renew_FD.ToString();
                        InRenFDHis.FDStatus = Status;
                        InRenFDHis.TxnID = model.xMID;
                        InRenFDHis.Status_Action = model.Status_Action;
                        InRenFDHis.RecID = Guid.NewGuid().ToString();
                        // Shifted to FD Voucher
                        // If Not Base._FD_Voucher_DBOps.InsertFDHistory(InRenFDHis) Then
                        //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //     Exit Sub
                        // End If
                    }
                }

                if ((model.ActionMethod == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    // edit
                    UpFD = new Common_Lib.RealTimeService.Parameter_UpdateFD_VoucherFD();
                    UpFD.BankAccID = model.FD_List_BA_ID;
                    UpFD.FDNo = model.FD_List_FD_NO;
                    if (IsDate(model.FD_List_FD_DATE.ToString()))
                    {
                        UpFD.FDDate = Convert.ToDateTime(model.FD_List_FD_DATE.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDDate = model.FD_List_FD_DATE.ToString();
                    }

                    // UpFD.FDDate = Me.Txt_Date.Text.Trim
                    if (IsDate(model.FD_AS_DATE.ToString()))
                    {
                        UpFD.FDAsDate = Convert.ToDateTime(model.FD_AS_DATE.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDAsDate = model.FD_AS_DATE.ToString();
                    }

                    // UpFD.FDAsDate = Me.Txt_As_Date.Text
                    UpFD.FDAmount = Convert.ToDouble(model.Txt_Amount);
                    UpFD.FDIntRate = Convert.ToDouble(model.TXT_INTEREST);
                    UpFD.PaymentCondition = model.Cmd_Mode;
                    if (IsDate(model.FD_List_MATURITY_DATE.ToString()))
                    {
                        UpFD.FDMaturityDate = Convert.ToDateTime(model.FD_List_MATURITY_DATE.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpFD.FDMaturityDate = model.FD_List_MATURITY_DATE.ToString();
                    }

                    // UpFD.FDMaturityDate = Me.Txt_Mat_Date.Text
                    UpFD.FDMaturityAmount = Convert.ToDouble(model.FD_List_MATURITY_AMOUNT);
                    UpFD.Remarks = model.Txt_Remarks;
                    UpFD.TxnID = model.xMID;
                    UpFD.RenewFrom_ID = model.FD_List;
                    // UpFD.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString  'Bug #4353 Fix
                    // UpFD.Status_Action = Status_Action
                    // Shifted to FD Voucher
                    // If Not Base._FD_Voucher_DBOps.UpdateFD(UpFD) Then
                    //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //     Exit Sub
                    // End If
                    UpFDHis = new Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD();
                    UpFDHis.FDAction = _action;
                    UpFDHis.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString();
                    UpFDHis.TxnId = model.xMID;
                    UpFDHis.FDID = model.xID;
                    // Shifted to FD Voucher
                    // If Not Base._FD_Voucher_DBOps.UpdateFDHistory(UpFDHis) Then
                    //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //     Exit Sub
                    // End If
                    if ((iAction == Common_Lib.Common.FDAction.Renew_FD))
                    {
                        UpRenFDHty = new Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD();
                        UpRenFDHty.FDAction = _action;
                        UpRenFDHty.FDStatus = Status;
                        UpRenFDHty.TxnId = model.xMID;
                        UpRenFDHty.FDID = model.FD_List;
                        // Shifted to FD Voucher
                        // If Not Base._FD_Voucher_DBOps.UpdateFDHistory(UpRenFDHty) Then
                        //     DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //     Exit Sub
                        // End If
                    }
                }

                string FDRecID = "";
                //Common_Lib.Common.Navigation_Mode Tag = model.ActionMethod;
                if (Tag == Common_Lib.Common.Navigation_Mode._New | Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    Common_Lib.RealTimeService.Param_Txn_NewFD_InsertVoucherFD InNewParam = new Common_Lib.RealTimeService.Param_Txn_NewFD_InsertVoucherFD();
                    model.xID = System.Guid.NewGuid().ToString();
                    model.xMID = System.Guid.NewGuid().ToString();

                    FDRecID = model.xID;
                    if (BASE.AllowMultiuser())
                    {
                        //Closed Bank Acc Check #g35
                        if (model.BankList_Enabled)
                        {
                            string AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FDVoucher_BankList, "").ToString();
                            if (string.IsNullOrEmpty(AccNo))
                                AccNo = "";
                            if (AccNo.Length > 0)
                            {
                                return Json(new
                                {
                                    message = "Entry cannot be Added....!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        //FD Account dependency check #Ref H35
                        if (model.FD_List_BA_ID.ToString().Length > 0)
                        {
                            DataTable FDAccount = BASE._FD_Voucher_DBOps.GetFDBankAccounts(model.FD_List_BA_ID);
                            if (FDAccount == null)
                            {
                                return View();
                            }
                            DateTime? EditTime = model.Bank_List_REC_EDIT_ON;
                            //A/D,E/D
                            if (FDAccount.Rows.Count <= 0)
                            {
                                return Json(new
                                {
                                    message = Common_Lib.Messages.DependencyChanged("Referred FD Bank Account") + " Referred Record Already Deleted!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(FDAccount.Rows[0]["REC_EDIT_ON"]);
                                //A/E,E/E
                                if (EditTime != NewEditOn)
                                {
                                    return Json(new
                                    {
                                        message = Common_Lib.Messages.DependencyChanged("Referred FD Bank Account") + " Referred Record Already Changed!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }



                    //----------------------// Start Dependencies //----------------------
                    if (BASE.AllowMultiuser())
                    {
                        DateTime oldEditOn = default(DateTime);
                        //Bank A/c dependency check #Ref G35
                        if (!string.IsNullOrEmpty(model.FDVoucher_BankList) && model.FDVoucher_BankList.Length > 0)
                        {
                            DataTable d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FDVoucher_BankList);
                            oldEditOn = Convert.ToDateTime(model.Bank_List_REC_EDIT_ON);
                            //A/D,E/D
                            if (d1.Rows.Count <= 0)
                            {
                                return Json(new
                                {
                                    message = Common_Lib.Messages.DependencyChanged("Bank Account") + " Referred Record Already Deleted!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                //A/E,E/E
                                if (oldEditOn != NewEditOn)
                                {
                                    return Json(new
                                    {
                                        message = Common_Lib.Messages.DependencyChanged("Bank Account") + " Referred Record Already Changed!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }

                    //-----------------------// End Dependencies //-------------------------

                    try
                    {
                        //Master Record 
                        Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD InMInfo = new Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD();
                        InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits;
                        InMInfo.VNo = model.Txt_V_NO;
                        if (IsDate(model.Txt_V_Date.ToString()))
                            InMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        else
                            InMInfo.TDate = model.Txt_V_Date.ToString();
                        //InMInfo.TDate = Txt_V_Date.Text
                        InMInfo.SubTotal = Convert.ToDouble(model.Txt_Amount);
                        InMInfo.Cash = 0;
                        InMInfo.Bank = 0;
                        InMInfo.TDS = 0;
                        InMInfo.Status_Action = model.Status_Action;
                        InMInfo.RecID = model.xMID;

                        //If Not Base._FD_Voucher_DBOps.InsertMasterInfo(InMInfo) Then
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //    Exit Sub
                        //End If

                        if(string.IsNullOrEmpty(InMInfo.RecID) || InMInfo.RecID.ToString().ToLower() == "null")
                        {
                            InMInfo.RecID = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InMInfo.Status_Action) || InMInfo.Status_Action.ToString().ToLower() == "null")
                        {
                            InMInfo.Status_Action = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InMInfo.TDate) || InMInfo.TDate.ToString().ToLower() == "null")
                        {
                            InMInfo.TDate = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InMInfo.VNo) || InMInfo.VNo.ToString().ToLower() == "null")
                        {
                            InMInfo.VNo = string.Empty;
                        }

                        InNewParam.param_InsertMaster = InMInfo;

                        //'---start: Fd Insertion shifted from frm_fd_winfow--''
                        //Insert FD
                        //If Not Base._FD_Voucher_DBOps.InsertFD(xfrm.InFD) Then
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //    Exit Sub
                        //End If

                        if (string.IsNullOrEmpty(InFD.BankAccID) || InFD.BankAccID.ToString().ToLower() == "null")
                        {
                            InFD.RecID = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.FDAsDate) || InFD.FDAsDate.ToString() == "null")
                        {
                            InFD.FDAsDate = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.FDDate) || InFD.FDDate.ToString().ToLower() == "null")
                        {
                            InFD.FDDate = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.FDMaturityDate) || InFD.FDMaturityDate.ToString().ToLower() == "null")
                        {
                            InFD.FDMaturityDate = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.FDNo) || InFD.FDNo.ToString().ToLower() == "null")
                        {
                            InFD.FDNo = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.PaymentCondition) || InFD.PaymentCondition.ToString().ToLower() == "null")
                        {
                            InFD.PaymentCondition = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.RecID) || InFD.RecID.ToString().ToLower() == "null")
                        {
                            InFD.RecID = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.Remarks) || InFD.Remarks.ToString().ToLower() == "null")
                        {
                            InFD.Remarks = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.RenewFrom_ID) || InFD.RenewFrom_ID.ToString().ToLower() == "null")
                        {
                            InFD.RenewFrom_ID = null;
                        }
                        if (string.IsNullOrEmpty(InFD.Status_Action) || InFD.Status_Action.ToString().ToLower() == "null")
                        {
                            InFD.Status_Action = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFD.TxnID) || InFD.TxnID.ToString().ToLower() == "null")
                        {
                            InFD.TxnID = string.Empty;
                        }
                        InNewParam.param_InsertFD = InFD;
                        //insert FD history
                        //If Not Base._FD_Voucher_DBOps.InsertFDHistory(xfrm.InFDHty) Then
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //    Exit Sub
                        //End If
                        if (string.IsNullOrEmpty(InFDHty.FDAction) || InFDHty.FDAction.ToString().ToLower() == "null")
                        {
                            InFDHty.FDAction = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFDHty.FDID) || InFDHty.FDID.ToString().ToLower() == "null")
                        {
                            InFDHty.FDID = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFDHty.FDStatus) || InFDHty.FDStatus.ToString().ToLower() == "null")
                        {
                            InFDHty.FDStatus = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFDHty.RecID) || InFDHty.RecID.ToString().ToLower() == "null")
                        {
                            InFDHty.RecID = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFDHty.Status_Action) || InFDHty.Status_Action.ToString().ToLower() == "null")
                        {
                            InFDHty.Status_Action = string.Empty;
                        }
                        if (string.IsNullOrEmpty(InFDHty.TxnID) || InFDHty.TxnID.ToString().ToLower() == "null")
                        {
                            InFDHty.TxnID = string.Empty;
                        }
                        InNewParam.param_InsertFDHistory = InFDHty;
                        //'---end: Fd Insertion shifted from frm_fd_winfow--''

                        //Txn
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherFD InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherFD();
                        InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits;
                        InParam.VNo = string.IsNullOrEmpty(model.Txt_V_NO) ? string.Empty : model.Txt_V_NO;
                        if (IsDate(model.Txt_V_Date.ToString()))
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                        else
                            InParam.TDate = string.IsNullOrEmpty(model.Txt_V_Date.ToString()) ? string.Empty : model.Txt_V_Date.ToString();
                        //InParam.TDate = Me.Txt_V_Date.Text.Trim()
                        InParam.ItemID = string.IsNullOrEmpty(model.ItemList) ? string.Empty : model.ItemList;
                        InParam.Type = string.IsNullOrEmpty(model.iTrans_Type) ? string.Empty : model.iTrans_Type;
                        InParam.Cr_Led_ID = string.IsNullOrEmpty(model.Cr_Led_id) ? string.Empty : model.Cr_Led_id;
                        InParam.Dr_Led_ID = string.IsNullOrEmpty(model.Dr_Led_id) ? string.Empty : model.Dr_Led_id;
                        InParam.SUB_Cr_Led_ID = string.IsNullOrEmpty(model.Sub_Cr_Led_ID) ? string.Empty : model.Sub_Cr_Led_ID;
                        InParam.SUB_Dr_Led_ID = string.IsNullOrEmpty(model.Sub_Dr_Led_ID) ? string.Empty : model.Sub_Dr_Led_ID;
                        InParam.Amount = Convert.ToDouble(model.Txt_Amount);
                        InParam.Mode = string.IsNullOrEmpty(model.Cmd_Mode) ? string.Empty : model.Cmd_Mode;
                        InParam.Ref_BANK_ID = "";
                        InParam.Ref_Branch = "";
                        InParam.Ref_No = string.IsNullOrEmpty(model.Txt_Ref_No) ? string.Empty : model.Txt_Ref_No;
                        if (IsDate(model.Txt_Ref_Date.ToString()))
                            InParam.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                        else
                            InParam.Ref_Date = string.IsNullOrEmpty(model.Txt_Ref_Date.ToString()) ? string.Empty : model.Txt_Ref_Date.ToString();
                        if (IsDate(model.Txt_Ref_CDate.ToString()))
                            InParam.Ref_CDate = Convert.ToDateTime(model.Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                        else
                            InParam.Ref_CDate = string.IsNullOrEmpty(model.Txt_Ref_CDate.ToString()) ? string.Empty : model.Txt_Ref_CDate.ToString();
                        //InParam.Ref_Date = Me.Txt_Ref_Date.Text.Trim
                        //InParam.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
                        InParam.Narration = string.IsNullOrEmpty(model.Txt_Narration) ? string.Empty : model.Txt_Narration;
                        InParam.Remarks = string.IsNullOrEmpty(model.Txt_Remarks) ? string.Empty : model.Txt_Remarks;
                        InParam.Reference = string.IsNullOrEmpty(model.Txt_Reference) ? string.Empty : model.Txt_Reference;
                        InParam.FDID = string.IsNullOrEmpty(FDRecID) ? string.Empty : FDRecID;
                        InParam.MasterTxnID = string.IsNullOrEmpty(model.xMID) ? string.Empty : model.xMID;
                        InParam.Status_Action = string.IsNullOrEmpty(model.Status_Action) ? string.Empty : model.Status_Action;
                        InParam.RecID = string.IsNullOrEmpty(model.xID) ? string.Empty : model.xID;

                        //If Not Base._FD_Voucher_DBOps.Insert(InParam) Then
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        //    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                        //    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                        //    Exit Sub
                        //End If
                        InNewParam.param_Insert = InParam;
                    }
                    catch (Exception ex)
                    {
                        return Json(new
                        {
                            message = "An Error Occoured while FD operation!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (!BASE._FD_Voucher_DBOps.InsertNewFD_Txn(InNewParam))
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.SomeError + " Error!!",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        message = Common_Lib.Messages.SaveSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_NewFD_UpdateVoucherFD EditParam = new Common_Lib.RealTimeService.Param_Txn_NewFD_UpdateVoucherFD();
                if (Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    FDRecID = model.xID;
                    if (BASE.AllowMultiuser())
                    {
                        //FD Account dependency check #Ref H35
                        if (model.FD_List_BA_ID.ToString().Length > 0)
                        {
                            DataTable FDAccount = BASE._FD_Voucher_DBOps.GetFDBankAccounts(model.FD_List_BA_ID);
                            if (FDAccount == null)
                            {
                                return View();
                            }

                            DateTime? EditTime = model.FD_List_REC_EDIT_ON;
                            //A/D,E/D
                            if (FDAccount.Rows.Count <= 0)
                            {
                                return Json(new
                                {
                                    message = Common_Lib.Messages.DependencyChanged("Referred FD Bank Account") + " Referred Record Already Deleted!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(FDAccount.Rows[0]["REC_EDIT_ON"]);
                                //A/E,E/E
                                if (EditTime != NewEditOn)
                                {
                                    return Json(new
                                    {
                                        message = Common_Lib.Messages.DependencyChanged("Referred FD Bank Account") + " Referred Record Already Changed!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }


                    if (BASE.AllowMultiuser())
                    {
                        object MaxValue = 0;
                        MaxValue = BASE._FD_Voucher_DBOps.GetTxnStatus(model.xMID);
                        //bug 5353 fix
                        if (MaxValue == null)
                        {
                            return Json(new
                            {
                                message = "Entry Not Found...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                        {
                            return Json(new
                            {
                                message = "Locked Entry cannot be Edited/Deleted...!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                        //Closed Bank Acc Check #g35
                        if (!string.IsNullOrEmpty(model.FDVoucher_BankList) && model.FDVoucher_BankList.Length > 0)
                        {
                            string AccNo = BASE._Voucher_DBOps.GetBankAccount(model.FDVoucher_BankList, "").ToString();
                            if (string.IsNullOrEmpty(AccNo))
                                AccNo = "";
                            if (AccNo.Length > 0)
                            {
                                return Json(new
                                {
                                    message = "Entry cannot be Edited...!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        DateTime oldEditOn = default(DateTime);
                        //Bank A/c dependency check #Ref G35
                        if (!string.IsNullOrEmpty(model.FDVoucher_BankList) && model.FDVoucher_BankList.Length > 0)
                        {
                            DataTable d1 = BASE._FD_Voucher_DBOps.GetBankAccounts(model.FDVoucher_BankList);
                            oldEditOn = Convert.ToDateTime(model.Bank_List_REC_EDIT_ON);
                            //A/D,E/D
                            if (d1.Rows.Count <= 0)
                            {
                                return Json(new
                                {
                                    message = Common_Lib.Messages.DependencyChanged("Bank Account") + " Referred Record Already Deleted!!",
                                    result = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                DateTime NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                //A/E,E/E
                                if (oldEditOn != NewEditOn)
                                {
                                    return Json(new
                                    {
                                        message = Common_Lib.Messages.DependencyChanged("Bank Account") + " Referred Record Already Changed!!",
                                        result = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }


                        DateTime CloseDate = Convert.ToDateTime(BASE._FD_Voucher_DBOps.GetFDCloseDate(model.xMID));
                        if (IsDate(CloseDate.ToString()))
                        {
                            return Json(new
                            {
                                message = "Current FD has already been Renewed/Closed.Referred Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);

                        }

                        if ((int)BASE._FD_Voucher_DBOps.GetCount(model.xMID, FDRecID, 1) > 0)
                        {
                            return Json(new
                            {
                                message = "Interest / TDS Posted against Current FD.Referred Record Already Changed!!",
                                result = false
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    //Maaster Record 
                    Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD UpMInfo = new Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD();
                    UpMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits;
                    UpMInfo.VNo = model.Txt_V_NO;
                    if (IsDate(model.Txt_V_Date.ToString()))
                        UpMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    else
                        UpMInfo.TDate = Convert.ToString(model.Txt_V_Date);
                    //UpMInfo.TDate = Txt_V_Date.Text
                    UpMInfo.SubTotal = Convert.ToDouble(model.Txt_Amount);
                    UpMInfo.Cash = 0;
                    UpMInfo.Bank = 0;
                    UpMInfo.TDS = 0;
                    UpMInfo.RecID = model.xMID;

                    //If Not Base._FD_Voucher_DBOps.UpdateMasterInfo(UpMInfo) Then
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //    Exit Sub
                    //End If
                    EditParam.param_UpdateMaster = UpMInfo;

                    //'---start: Fd updation shifted from frm_fd_winfow--''
                    //If Not Base._FD_Voucher_DBOps.UpdateFD(xfrm.UpFD) Then
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //    Exit Sub
                    //End If
                    EditParam.param_UpdateFD = UpFD;

                    //If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpFDHis) Then
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //    Exit Sub
                    //End If
                    EditParam.param_UpdateFDHistory = UpFDHis;

                    //If iAction = Common_Lib.Common.FDAction.Renew_FD Then
                    //    If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpRenFDHty) Then
                    //        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //        Exit Sub
                    //    End If
                    //End If
                    //'---end: Fd updation shifted from frm_fd_winfow--''
                    EditParam.param_DeleteVoucher_Txn_MID = model.xMID;

                    //If Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
                    Common_Lib.RealTimeService.Parameter_Insert_VoucherFD InParam1 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherFD();
                    InParam1.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits;
                    InParam1.VNo = model.Txt_V_NO;
                    if (IsDate(model.Txt_V_Date.ToString()))
                        InParam1.TDate = Convert.ToDateTime(model.Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                    else
                        InParam1.TDate = model.Txt_V_Date.ToString();
                    //InParam1.TDate = Me.Txt_V_Date.Text.Trim()
                    InParam1.ItemID = model.ItemList;
                    InParam1.Type = model.iTrans_Type;
                    InParam1.Cr_Led_ID = model.Cr_Led_id;
                    InParam1.Dr_Led_ID = model.Dr_Led_id;
                    InParam1.SUB_Cr_Led_ID = model.Sub_Cr_Led_ID;
                    InParam1.SUB_Dr_Led_ID = model.Sub_Dr_Led_ID;
                    InParam1.Amount = Convert.ToDouble(model.Txt_Amount);
                    InParam1.Mode = model.Cmd_Mode;
                    InParam1.Ref_BANK_ID = "";
                    InParam1.Ref_Branch = "";
                    InParam1.Ref_No = model.Txt_Ref_No;
                    if (IsDate(model.Txt_Ref_Date.ToString()))
                        InParam1.Ref_Date = Convert.ToDateTime(model.Txt_Ref_Date).ToString(BASE._Server_Date_Format_Short);
                    else
                        InParam1.Ref_Date = Convert.ToString(model.Txt_Ref_Date);
                    if (IsDate(model.Txt_Ref_CDate.ToString()))
                        InParam1.Ref_CDate = Convert.ToDateTime(model.Txt_Ref_CDate).ToString(BASE._Server_Date_Format_Short);
                    else
                        InParam1.Ref_CDate = model.Txt_Ref_CDate.ToString();
                    //InParam1.Ref_Date = Me.Txt_Ref_Date.Text.Trim
                    //InParam1.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
                    InParam1.Narration = model.Txt_Narration;
                    InParam1.Remarks = model.Txt_Remarks;
                    InParam1.Reference = model.Txt_Reference;
                    InParam1.FDID = FDRecID;
                    InParam1.MasterTxnID = model.xMID;
                    InParam1.Status_Action = model.Status_Action;
                    InParam1.RecID = model.xID;


                    //If Not Base._FD_Voucher_DBOps.Insert(InParam1) Then
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //    Exit Sub
                    //End If
                    EditParam.param_Insert = InParam1;
                    //Else
                    //   DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    //    Exit Sub
                    //End If
                    //STR1 = " UPDATE TRANSACTION_INFO SET " & _
                    //                        " TR_VNO         ='" & Me.Txt_V_NO.Text & "', " & _
                    //                        " TR_DATE        =#" & xDate.ToString(Base._Date_Format_Short) & "#, " & _
                    //                        " TR_ITEM_ID     ='" & Me.GLookUp_ItemList.Tag & "', " & _
                    //                        " TR_TYPE        ='" & iTrans_Type & "', " & _
                    //                        " TR_CR_LED_ID   ='" & Cr_Led_id & "', " & _
                    //                        " TR_DR_LED_ID   ='" & Dr_Led_id & "', " & _
                    //                        " TR_SUB_CR_LED_ID  ='" & Me.GLookUp_BankList.Tag & "', " & _
                    //                        " TR_MODE        ='" & Me.Cmd_Mode.Text & "', " & _
                    //                        " TR_REF_NO      ='" & Me.Txt_Ref_No.Text & "', " & _
                    //                        " TR_REF_DATE    = " & Me.Txt_Ref_Date.Tag & ", " & _
                    //                        " TR_REF_CDATE   = " & Me.Txt_Ref_CDate.Tag & ", " & _
                    //                        " TR_AMOUNT      = " & Val(Me.Txt_Amount.Text) & ", " & _
                    //                        " TR_NARRATION   ='" & Me.Txt_Narration.Text & "', " & _
                    //                        " TR_REMARKS     ='" & Me.Txt_Remarks.Text & "', " & _
                    //                        " TR_REFERENCE   ='" & Me.Txt_Reference.Text & "', " & _
                    //                        " " & _
                    //                        " REC_EDIT_ON    =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
                    //                        " REC_EDIT_BY    ='" & Base._open_User_ID & "', " & _
                    //                        " REC_STATUS     = " & Status_Action & "," & _
                    //                        " REC_STATUS_ON  =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
                    //                        " REC_STATUS_BY  ='" & Base._open_User_ID & "'  " & _
                    //                        " WHERE TR_M_ID   ='" & Me.xMID.Text & "'"
                    //Command.CommandText = STR1 : Command.ExecuteNonQuery() 'FD REC ID in Cross Ref ID is not getting Updated 
                    //trans.Commit()

                    if (!BASE._FD_Voucher_DBOps.UpdateNewFD_Txn(EditParam))
                    {
                        return Json(new
                        {
                            message = Common_Lib.Messages.SomeError,
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        message = Common_Lib.Messages.UpdateSuccess,
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return Json(new
                {
                    message = "An error has occured!!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return View();
            // Close Prev FD
            // If iAction = Common_Lib.Common.FDAction.Renew_FD Then
            //     Dim ClsFd As Common_Lib.DbOperations.Parameter_CloseFD_VoucherFD = New Common_Lib.DbOperations.Parameter_CloseFD_VoucherFD()
            //     ClsFd.FDCloseDate = Txt_Date.Text.Trim()
            //     ClsFd.FDStatus = Status
            //     ClsFd.Status_Action = Status_Action
            //     ClsFd.RecID = iRenewFrom
            //     If Not Base._FD_Voucher_DBOps.CloseFD(ClsFd) Then
            //         DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //         Exit Sub
            //     End If
            // End If
        }

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }

        public ActionResult FDWindow_LookUp_GetBankList(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            DataTable BA_Table = BASE._FD_Voucher_DBOps.GetFDBankAccounts();
            if ((BA_Table == null))
            {
                return null;
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

            DataTable BB_Table = BASE._FD_Voucher_DBOps.GetBranches(Branch_IDs);
            if ((BB_Table == null))
            {
                return null;
            }

            // BUILD DATA

            var BuildData = (from B in BB_Table.AsEnumerable()
                             join A in BA_Table.AsEnumerable()
                             on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                             select new FDBankList
                             {
                                 BANK_NAME = (string)B["Name"],
                                 BI_SHORT_NAME = (string)B["BI_SHORT_NAME"],
                                 BANK_BRANCH = (string)B["Branch"],
                                 BA_ID = (string)A["ID"],
                                 BANK_ACC_NO = (string)A["BA_CUST_NO"],
                                 REC_EDIT_ON = (DateTime?)A["REC_EDIT_ON"]
                             });

            var Final_Data = BuildData.ToList();


            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Final_Data, loadOptions)), "application/json");
        }
    }
}