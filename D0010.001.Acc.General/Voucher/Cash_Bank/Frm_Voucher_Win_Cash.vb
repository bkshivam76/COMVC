Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Reflection
Imports DevExpress.Data.Filtering

Public Class Frm_Voucher_Win_Cash

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iMode As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()

    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Hide_Properties()
    End Sub
    Dim FormClosingEnable As Boolean = True
    Private Sub Form_Closing_Window_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then BUT_CANCEL_Click(New Object, New EventArgs)
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'By Item-wise Selection
            Me.Txt_V_Date.Focus()
        Else
            Me.GLookUp_ItemList.Focus()
        End If
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.D)) Then 'delete
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                BUT_SAVE_Click(BUT_DEL, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.F2)) Then
            Txt_V_Date.Focus()
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                'Closed Bank Acc Check #g28
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If AccNo Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CashDepositWithDrawn.UsedBankAccClosed(AccNo), "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox("Information....", Common_Lib.Messages.CashDepositWithDrawn.UsedBankAccClosed(AccNo), Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If

            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim cash_bank_DbOps As DataTable = Base._Cash_Bank_DBOps.GetRecord(Me.xID.Text)
                If cash_bank_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If cash_bank_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transaction"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Transaction"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(cash_bank_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transaction"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Transaction"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'CHECKING LOCK STATUS
                Dim MaxValue As Object = 0
                MaxValue = Base._Cash_Bank_DBOps.GetStatus(Me.xID.Text)
                If MaxValue Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox("Information...", "E n t r y   N o t   F o u n d . . . !", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                End If

                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CashDepositWithDrawn.EntryLockedNotEditDel, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox("Information...", Common_Lib.Messages.CashDepositWithDrawn.EntryLockedNotEditDel, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.ItemNotSelected, Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.VchDateIncorrect, Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                Me.Txt_V_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_V_Date)
            End If
            If IsDate(Me.Txt_V_Date.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_V_Date.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.VchDateNotfinYear, Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_V_Date.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.VchDateNotfinYear, Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.BankNotSelected, Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If

            If IsDate(Me.Txt_Ref_Date.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_Ref_Date.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.RefDateNotfinYear, Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                    Me.Txt_Ref_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_Date)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_Ref_Date.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.RefDateNotfinYear, Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                    Me.Txt_Ref_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_Date)
                End If
            End If

            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_Ref_CDate.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.ClearingDateNotfinYear, Me.Txt_Ref_CDate, 0, Me.Txt_Ref_CDate.Height, 5000)
                    Me.Txt_Ref_CDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_CDate)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_Ref_CDate.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.ClearingDateNotfinYear, Me.Txt_Ref_CDate, 0, Me.Txt_Ref_CDate.Height, 5000)
                    Me.Txt_Ref_CDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_CDate)
                End If
                diff = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("C l e a r i n g   D a t e  C a n n o t   b e   l e s s   t h a n   R e f e r e n c e   D a t e!!", Me.Txt_Ref_CDate, 0, Me.Txt_Ref_CDate.Height, 5000)
                    Me.Txt_Ref_CDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_CDate)
                End If
            End If
            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show(Common_Lib.Messages.CashDepositWithDrawn.AmtNotZero, Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

        End If

        '-------------------// Start Dependencies //-------------------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                'Bank Account dependency check #Ref G28
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim oldEditOn As DateTime = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    Dim d1 As DataTable = Base._Cash_Bank_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If d1.Rows.Count <= 0 Then 'A/D,E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Base.ShowMessagebox("Referred Record Already Deleted!!", Common_Lib.Messages.DependencyChanged("Bank Account"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            'Base.ShowMessagebox("Referred Record Already Changed!!", Common_Lib.Messages.DependencyChanged("Bank Account"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
        '---------------------// End Dependencies //-----------------------

        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._Cash_Bank_DBOps.GetStatus(Me.xID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        'Ledger Setting..................................................................\
        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            Dr_Led_id = iLed_ID : Cr_Led_id = "00080" 'Cash A/c.
            If iLed_ID = "00079" Then Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag 'Bank A/c.
        Else
            Cr_Led_id = iLed_ID : Dr_Led_id = "00080" 'Cash A/c.
            If iLed_ID = "00079" Then Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag 'Bank A/c.
        End If
        '................................................................................./

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_Voucher_CashBank = New Common_Lib.RealTimeService.Parameter_Insert_Voucher_CashBank()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn
            InParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
            'InParam.TDate = Txt_V_Date.Text
            InParam.ItemID = Me.GLookUp_ItemList.Tag
            InParam.Type = iTrans_Type
            InParam.Cr_Led_ID = Cr_Led_id
            InParam.Dr_Led_ID = Dr_Led_id
            InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            InParam.Mode = iMode
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'InParam.Ref_Date = Txt_Ref_Date.Text
            'InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            InParam.Amount = Val(Me.Txt_Amount.Text)
            InParam.Narration = Me.Txt_Narration.Text
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID.Text

            If Base._Cash_Bank_DBOps.Insert(InParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.SaveSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Exit Sub
            End If
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim UpParam As Common_Lib.RealTimeService.Parameter_Update_Voucher_CashBank = New Common_Lib.RealTimeService.Parameter_Update_Voucher_CashBank()
            UpParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.TDate = Txt_V_Date.Text
            'UpParam.TDate = Txt_V_Date.Text
            UpParam.ItemID = Me.GLookUp_ItemList.Tag
            UpParam.Type = iTrans_Type
            UpParam.Cr_Led_ID = Cr_Led_id
            UpParam.Dr_Led_ID = Dr_Led_id
            UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            UpParam.Mode = iMode
            UpParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then UpParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then UpParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'UpParam.Ref_Date = Txt_Ref_Date.Text
            'UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            UpParam.Amount = Val(Me.Txt_Amount.Text)
            UpParam.Narration = Me.Txt_Narration.Text
            UpParam.Remarks = Me.Txt_Remarks.Text
            UpParam.Reference = Me.Txt_Reference.Text
            'UpParam.Status_Action = Status_Action
            UpParam.RecID = Me.xID.Text

            If Base._Cash_Bank_DBOps.Update(UpParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.UpdateSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Exit Sub
            End If
        End If


        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                If Base._Cash_Bank_DBOps.Delete(Me.xID.Text) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.DeleteSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = DialogResult.OK
                    FormClosingEnable = False : Me.Close()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Exit Sub
                End If
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Cancel this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                Hide_Properties()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                FormClosingEnable = False : Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.None
            End If
            xPromptWindow.Dispose()
        End If
    End Sub

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amount.KeyPress, Txt_Ref_No.KeyPress, Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()
        If UCase(txt.Name) = "TXT_SEARCH" Then
            'If e.KeyCode = Keys.Enter Then DGrid1.Focus()
            'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Ref_No.Validated, Txt_Narration.Validated, Txt_Reference.Validated, Txt_Remarks.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Cash Deposit / Withdrawn Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Cash Deposit / Withdrawn"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.Text = "New ~ " & Me.TitleX.Text
            Me.Txt_V_NO.Text = ""
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "Edit ~ " & Me.TitleX.Text
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.Text = "Delete ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "View ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'By Item-wise Selection
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
            Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            Me.GLookUp_ItemList.Properties.ReadOnly = False
            Me.Txt_V_Date.Focus()
        Else
            Me.GLookUp_ItemList.Focus()
        End If
        xPleaseWait.Hide()
    End Sub

    Private Sub Data_Binding()
        Dim d1 As DataTable = Base._Cash_Bank_DBOps.GetRecord(Me.xID.Text)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
            Exit Sub
        End If

        Dim xDate As DateTime = Nothing
        xDate = d1.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transaction", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Transaction", viewstr), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        LastEditedOn = Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON"))
        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        If Not IsDBNull(d1.Rows(0)("TR_ITEM_ID")) Then
            If d1.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d1.Rows(0)("TR_ITEM_ID")))
                Me.GLookUp_ItemList.EditValue = d1.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If
        End If
        Me.GLookUp_ItemList.Properties.ReadOnly = False

        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If iLed_ID = "00079" Then Bank_ID = IIf(IsDBNull(d1.Rows(0)("TR_SUB_DR_LED_ID")), "", d1.Rows(0)("TR_SUB_DR_LED_ID"))
        Else
            If iLed_ID = "00079" Then Bank_ID = IIf(IsDBNull(d1.Rows(0)("TR_SUB_CR_LED_ID")), "", d1.Rows(0)("TR_SUB_CR_LED_ID"))
        End If
        If Bank_ID.ToString.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID))
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        Txt_Ref_No.DataBindings.Add("text", d1, "TR_REF_NO")
        If Not IsDBNull(d1.Rows(0)("TR_REF_DATE")) Then
            xDate = d1.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d1.Rows(0)("TR_REF_CDATE")) Then
            xDate = d1.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If
        Txt_Amount.DataBindings.Add("EditValue", d1, "TR_AMOUNT")
        Txt_Narration.DataBindings.Add("text", d1, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d1, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d1, "TR_REFERENCE")
        xPleaseWait.Hide()

    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Branch.Enabled = False : Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Acc_No.Enabled = False : Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.GLookUp_BankList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
        Me.ToolTip1.Hide(Me.Txt_Amount)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_ItemList
    Private Sub GLookUp_ItemList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_ItemList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_ItemList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_ItemList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_ItemList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_ItemList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_ItemList.CancelPopup()
            Hide_Properties()
            'Me.Txt_No.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.List_Lang.Focus()
        End If

    End Sub
    Private Sub GLookUp_ItemList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.EditValueChanged
        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_ItemListView.RowCount > 0 And Val(Me.GLookUp_ItemListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_ID").ToString
                BE_Item_Head.Text = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_NAME").ToString
                iVoucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                iLed_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LED_ID").ToString
                iTrans_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TRANS_TYPE").ToString

                If iVoucher_Type.ToUpper = "CASH DEPOSITED" Then
                    iMode = "CASH" : lbl_amt.Text = "Cash Deposited Amount:"
                    LayoutControlItem3.Control.Enabled = False
                    Me.Txt_Ref_No.Text = "" : Me.Txt_Ref_Date.EditValue = "" : Me.Txt_Ref_CDate.EditValue = ""
                    lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_CDate.ForeColor = Color.Gray
                Else
                    iMode = "CHEQUE" : lbl_amt.Text = "Cash Withdrawn Amount:"
                    LayoutControlItem3.Control.Enabled = True
                    Me.Txt_Ref_No.Text = "" : Me.Txt_Ref_Date.EditValue = "" : Me.Txt_Ref_CDate.EditValue = ""
                    lbl_Ref_Title.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red : lbl_Ref_CDate.ForeColor = Color.Black
                End If
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        'Dim SQL_STR1 As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS [ITEM_ID]  " & _
        '                         " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('CASH DEPOSITED','CASH WITHDRAWN')"
        Dim d1 As DataTable = Base._Cash_Bank_DBOps.GetItemList()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "ITEM_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_ItemList.Properties.ValueMember = "ITEM_ID"
            Me.GLookUp_ItemList.Properties.DisplayMember = "ITEM_NAME"
            Me.GLookUp_ItemList.Properties.DataSource = dview
            Me.GLookUp_ItemListView.RefreshData()
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup() : Me.GLookUp_ItemList.EditValue = dview.Item(0).Row("ITEM_ID").ToString : Me.GLookUp_ItemList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.Properties.ReadOnly = False
        End If
    End Sub

    '1.GLookUp_BankList
    Private Sub GLookUp_BankList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_BankList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_BankListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_BankListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_BankList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_BankListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_BankListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_BankList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_BankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_BankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_BankList.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.BE_Bank_Branch.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.BE_Bank_Acc_No.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
                Me.BE_Bank_Acc_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BI_SHORT_NAME").ToString


            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_BankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BI_SHORT_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        ' Dim SQL_1 As String = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS [ID] FROM BANK_ACCOUNT_INFO " & _
        '                      " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND BA_CEN_ID='" & Base._open_Cen_ID & "' AND BA_ACCOUNT_TYPE='SAVING' AND BA_CLOSE_DATE IS NULL  ; "
        Dim BA_Table As DataTable = Base._Cash_Bank_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        If BA_Table.Rows.Count < 1 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("V o u c h e r   E n t r y   N o t   A l l o w e d. . . !" & vbNewLine & vbNewLine & "Note: Cash Deposited / Withdrawn required minimum One Bank Accounts...!", Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Base.ShowMessagebox(Me.TitleX.Text, "V o u c h e r   E n t r y   N o t   A l l o w e d. . . !" & vbNewLine & vbNewLine & "Note: Cash Deposited / Withdrawn required minimum One Bank Accounts...!", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            FormClosingEnable = False : Me.Close()
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        'Dim SQL_2 As String = "SELECT B.BI_BANK_NAME AS [Name],B.BI_SHORT_NAME,A.BB_BRANCH_NAME as [Branch], A.REC_ID AS BB_BRANCH_ID " & _
        '                      " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " & _
        '                      " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID IN (" & Branch_IDs & ")  ;"
        Dim BB_Table As DataTable = Base._Cash_Bank_DBOps.GetBranchDetails(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
            Exit Sub
        End If
        '
        'BUILD DATA
        Dim BuildData = From B In BB_Table, A In BA_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
                        Select New With { _
                                        .BANK_NAME = B.Field(Of String)("Name"), _
                                        .BI_SHORT_NAME = B.Field(Of String)("BI_SHORT_NAME"), _
                                        .BANK_BRANCH = B.Field(Of String)("Branch"), _
                                        .BANK_ACC_NO = A.Field(Of String)("BA_ACCOUNT_NO"), _
                                        .BA_ID = A.Field(Of String)("ID"), _
                                        .REC_EDIT_ON = A.Field(Of DateTime)("REC_EDIT_ON")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        If Final_Data.Count = 1 Then
            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy

            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
        End If
    End Sub

#End Region


End Class