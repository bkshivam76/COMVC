Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection

Public Class Frm_Voucher_Win_FD

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public CreatedFDID As String = ""
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer

    Public iAction As Common_Lib.Common.FDAction = -1
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
    Private Sub Form_Closing_Window_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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
        If iAction = Common_Lib.Common.FDAction.Renew_FD Then
            RenewFD_Functonality(sender) : Exit Sub
        ElseIf iAction = Common_Lib.Common.FDAction.Close_FD Then
            CloseFD_Functonality(sender) : Exit Sub
        ElseIf iAction = Common_Lib.Common.FDAction.New_FD Then
            NewFD_Functionality(sender) : Exit Sub
        End If
        Hide_Properties()
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            'Closed Bank Acc Check #g38
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If AccNo Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim FDvoucher_DbOps As DataTable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text)
                If FDvoucher_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(FDvoucher_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'CHECKING LOCK STATUS
                Dim MaxValue As Object = 0
                MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
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

            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If Val(Trim(Me.GLookUp_FD_List.Text.Length)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S e l e c t  F D. . . !", Me.GLookUp_FD_List, 0, Me.GLookUp_FD_List.Height, 5000)
                Me.GLookUp_FD_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FD_List)
            End If

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If Me.Cmd_Mode.Text.Length <= 0 And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S e l e c t  M o d e . . . !", Me.Cmd_Mode, 0, Me.Cmd_Mode.Height, 5000)
                Me.Cmd_Mode.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_Mode)
            End If

            If Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) <= 0 Then ' And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" 
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("E n t e r  A m o u n t  R e c e i v e d . . . !", Me.TXT_RENEWAL_MATURITY_AMOUNT, 0, Me.TXT_RENEWAL_MATURITY_AMOUNT.Height, 5000)
                Me.TXT_RENEWAL_MATURITY_AMOUNT.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_RENEWAL_MATURITY_AMOUNT)
            End If

            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If

            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""
            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If

            If GLookUp_BankList.Text.Trim.Length = 0 And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S p e c i f i e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If

            If IsDate(Me.Txt_Ref_Date.Text) = False And GLookUp_ItemList.Tag <> "d0219173-45ff-4284-ae0e-89ba0e8d76b4" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If

            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
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

        End If

        '-----------------------------// Start Dependencies //--------------------------

        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim d1 As DataTable
                Dim oldEditOn, NewEditOn As DateTime
                'Bank Account dependency check #Ref G38
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    d1 = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then 'A/D, E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                'FDs dependency check #Ref I38, AH38
                If GLookUp_FD_List.Tag.ToString.Length > 0 Then
                    '  d1 = Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag)
                    If (GLookUp_ItemListView.FocusedRowHandle >= 0 And Me.GLookUp_ItemList.Tag <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" And Me.GLookUp_ItemList.Tag <> "f6e4da62-821f-4961-9f93-f5177fca2a77" And Me.GLookUp_ItemList.Tag <> "65730a27-e365-4195-853e-2f59225fe8f4") Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                        d1 = Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag, True)
                    Else
                        d1 = Base._FD_Voucher_DBOps.GetFDs(False, GLookUp_FD_List.Tag)
                    End If
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    oldEditOn = GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then 'A/D, E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If

        '---------------------------// End Dependencies //-----------------------------

        ''CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = "" : Dim Sub_Cr_Led_id As String = "" : Dim Sub_Dr_Led_id As String = ""

        If iTrans_Type.ToUpper = "DEBIT" Then
            Dr_Led_id = iLed_ID
            If Cmd_Mode.Text.ToUpper <> "CASH" Then
                Cr_Led_id = "00079"
                If GLookUp_BankList.Text.Length > 0 Then Sub_Cr_Led_id = "'" & GLookUp_BankList.Tag & "'"
            Else
                Cr_Led_id = "00080"
            End If
        Else
            Cr_Led_id = iLed_ID
            If Cmd_Mode.Text.ToUpper <> "CASH" Then
                Dr_Led_id = "00079"
                If GLookUp_BankList.Text.Length > 0 Then Sub_Dr_Led_id = "'" & GLookUp_BankList.Tag & "'"
            Else
                Dr_Led_id = "00080"
            End If
        End If

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_InsertVoucherFD = New Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_InsertVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Me.xMID.Text = System.Guid.NewGuid().ToString()

            'Try
            'Master Record 
            Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text '#5410 fix
            InMInfo.SubTotal = 0
            InMInfo.Cash = 0
            InMInfo.Bank = 0
            InMInfo.TDS = 0
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.InsertMasterInfo(InMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMInfo

            If GLookUp_ItemList.Tag = "d0219173-45ff-4284-ae0e-89ba0e8d76b4" Then
                'TDS by bank
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                InParam.ItemID = Me.GLookUp_ItemList.Tag
                InParam.Type = iTrans_Type
                InParam.Cr_Led_ID = ""
                InParam.Dr_Led_ID = Dr_Led_id
                InParam.SUB_Cr_Led_ID = ""
                InParam.SUB_Dr_Led_ID = ""
                InParam.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                InParam.Mode = ""
                InParam.Ref_BANK_ID = ""
                InParam.Ref_Branch = ""
                InParam.Ref_No = ""
                InParam.Ref_Date = ""
                InParam.Ref_CDate = ""
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.Txt_Remarks.Text
                InParam.Reference = Me.Txt_Reference.Text
                InParam.FDID = GLookUp_FD_List.Tag
                InParam.MasterTxnID = Me.xMID.Text
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text

                'If Not Base._FD_Voucher_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_TDSbyBank = InParam
                'iNTEREST RECEIVED
                Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParam1.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                'InParam1.TDate = Me.Txt_V_Date.Text.Trim()
                InParam1.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                InParam1.Type = "CREDIT"
                InParam1.Cr_Led_ID = "00069"
                InParam1.Dr_Led_ID = ""
                InParam1.SUB_Cr_Led_ID = ""
                InParam1.SUB_Dr_Led_ID = ""
                InParam1.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                InParam1.Mode = ""
                InParam1.Ref_BANK_ID = ""
                InParam1.Ref_Branch = ""
                InParam1.Ref_No = ""
                InParam1.Ref_Date = ""
                InParam1.Ref_CDate = ""
                InParam1.Narration = Me.Txt_Narration.Text
                InParam1.Remarks = Me.Txt_Remarks.Text
                InParam1.Reference = Me.Txt_Reference.Text
                InParam1.FDID = GLookUp_FD_List.Tag
                InParam1.MasterTxnID = Me.xMID.Text
                InParam1.Status_Action = Status_Action
                InParam1.RecID = Guid.NewGuid.ToString

                'If Not Base._FD_Voucher_DBOps.Insert(InParam1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertIntRec = InParam1
            Else

                Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                InParams.ItemID = Me.GLookUp_ItemList.Tag
                InParams.Type = iTrans_Type
                InParams.Cr_Led_ID = Cr_Led_id
                InParams.Dr_Led_ID = Dr_Led_id
                InParams.SUB_Cr_Led_ID = Sub_Cr_Led_id
                InParams.SUB_Dr_Led_ID = Sub_Dr_Led_id
                InParams.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                InParams.Mode = Cmd_Mode.Text
                InParams.Ref_BANK_ID = GLookUp_BankList.Tag
                InParams.Ref_Branch = Txt_Branch.Text
                InParams.Ref_No = Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParams.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParams.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_CDate = Txt_Ref_CDate.Text
                'InParams.Ref_Date = Me.Txt_Ref_Date.Text.Trim
                'InParams.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
                InParams.Narration = Me.Txt_Narration.Text
                InParams.Remarks = Me.Txt_Remarks.Text
                InParams.Reference = Me.Txt_Reference.Text
                InParams.FDID = GLookUp_FD_List.Tag
                InParams.MasterTxnID = Me.xMID.Text
                InParams.Status_Action = Status_Action
                InParams.RecID = Me.xID.Text

                'If Not Base._FD_Voucher_DBOps.Insert(InParams) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_Insert = InParams
            End If
            'Catch ex As Exception
            '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End Try

            If Not Base._FD_Voucher_DBOps.InsertIncomeAndExpenses_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_UpdateVoucherFD = New Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_UpdateVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            'Try
            'Maaster Record 
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD()
            UpMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            'UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.SubTotal = 0
            UpMInfo.Cash = 0
            UpMInfo.Bank = 0
            UpMInfo.TDS = 0
            UpMInfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.UpdateMasterInfo(UpMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMInfo

            'Delete Transactions
            'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteTxns = Me.xMID.Text

            Me.xID.Text = System.Guid.NewGuid.ToString

            If GLookUp_ItemList.Tag = "d0219173-45ff-4284-ae0e-89ba0e8d76b4" Then
                'TDS by bank
                Dim InPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPms.TDate = Txt_V_Date.Text
                'InPms.TDate = Me.Txt_V_Date.Text.Trim()
                InPms.ItemID = Me.GLookUp_ItemList.Tag
                InPms.Type = iTrans_Type
                InPms.Cr_Led_ID = ""
                InPms.Dr_Led_ID = Dr_Led_id
                InPms.SUB_Cr_Led_ID = ""
                InPms.SUB_Dr_Led_ID = ""
                InPms.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                InPms.Mode = ""
                InPms.Ref_BANK_ID = ""
                InPms.Ref_Branch = ""
                InPms.Ref_No = ""
                InPms.Ref_Date = ""
                InPms.Ref_CDate = ""
                InPms.Narration = Me.Txt_Narration.Text
                InPms.Remarks = Me.Txt_Remarks.Text
                InPms.Reference = Me.Txt_Reference.Text
                InPms.FDID = GLookUp_FD_List.Tag
                InPms.MasterTxnID = Me.xMID.Text
                InPms.Status_Action = Status_Action
                InPms.RecID = Me.xID.Text


                'If Not Base._FD_Voucher_DBOps.Insert(InPms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_TDSbyBank = InPms

                'iNTEREST RECEIVED
                Dim inParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                inParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                inParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then inParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else inParam.TDate = Txt_V_Date.Text
                'inParam.TDate = Me.Txt_V_Date.Text.Trim()
                inParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                inParam.Type = "CREDIT"
                inParam.Cr_Led_ID = "00069"
                inParam.Dr_Led_ID = ""
                inParam.SUB_Cr_Led_ID = ""
                inParam.SUB_Dr_Led_ID = ""
                inParam.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                inParam.Mode = ""
                inParam.Ref_BANK_ID = ""
                inParam.Ref_Branch = ""
                inParam.Ref_No = ""
                inParam.Ref_Date = ""
                inParam.Ref_CDate = ""
                inParam.Narration = Me.Txt_Narration.Text
                inParam.Remarks = Me.Txt_Remarks.Text
                inParam.Reference = Me.Txt_Reference.Text
                inParam.FDID = GLookUp_FD_List.Tag
                inParam.MasterTxnID = Me.xMID.Text
                inParam.Status_Action = Status_Action
                inParam.RecID = Guid.NewGuid.ToString

                'If Not Base._FD_Voucher_DBOps.Insert(inParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InsertIntRec = inParam
            Else

                Dim inParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                inParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                inParams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then inParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else inParams.TDate = Txt_V_Date.Text
                'inParams.TDate = Me.Txt_V_Date.Text.Trim()
                inParams.ItemID = Me.GLookUp_ItemList.Tag
                inParams.Type = iTrans_Type
                inParams.Cr_Led_ID = Cr_Led_id
                inParams.Dr_Led_ID = Dr_Led_id
                inParams.SUB_Cr_Led_ID = Sub_Cr_Led_id
                inParams.SUB_Dr_Led_ID = Sub_Dr_Led_id
                inParams.Amount = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                inParams.Mode = Cmd_Mode.Text
                inParams.Ref_BANK_ID = GLookUp_BankList.Tag
                inParams.Ref_Branch = Txt_Branch.Text
                inParams.Ref_No = Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then inParams.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else inParams.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then inParams.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else inParams.Ref_CDate = Txt_Ref_CDate.Text
                'inParams.Ref_Date = Me.Txt_Ref_Date.Text.Trim
                'inParams.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
                inParams.Narration = Me.Txt_Narration.Text
                inParams.Remarks = Me.Txt_Remarks.Text
                inParams.Reference = Me.Txt_Reference.Text
                inParams.FDID = GLookUp_FD_List.Tag
                inParams.MasterTxnID = Me.xMID.Text
                inParams.Status_Action = Status_Action
                inParams.RecID = Me.xID.Text

                EditParam.param_Insert = inParams
                'If Not Base._FD_Voucher_DBOps.Insert(inParams) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
            End If
            'Catch ex As Exception
            '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!", Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End Try

            If Not Base._FD_Voucher_DBOps.UpdateIncomeAndExpenses_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_DeleteVoucherFD = New Common_Lib.RealTimeService.Param_Txn_IncomeExpenses_DeleteVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                'Delete Transactions
                'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_Delete = Me.xMID.Text
                ''Delete Master
                'If Not Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text

                If Not Base._FD_Voucher_DBOps.DeleteIncomeAndExpenses_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
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
    Private Sub BTN_FD_DET_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_FD_DET.Click
        If GLookUp_FD_List.Tag.ToString.Length > 0 Then
            Dim xfrm As New Frm_FD_Window
            xfrm.xFdID = Me.GLookUp_FD_List.Tag
            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
            xfrm.BUT_DEL.Visible = False
            xfrm.BUT_SAVE_COM.Visible = False
            xfrm.BUT_CANCEL.Text = "Close"
            xfrm.ShowDialog(Me)
        Else
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("F D   N o t   S e l e c t e d . . . !", Me.GLookUp_FD_List, 0, Me.GLookUp_FD_List.Height, 5000)
            Me.GLookUp_FD_List.Focus()
        End If
    End Sub
    Private Sub Cmd_Mode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.SelectedIndexChanged, Cmb_Rec_Mode.SelectedIndexChanged
        Dim CMB As ComboBoxEdit = sender
        Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None : Me.Txt_Ref_No.Properties.Mask.EditMask = ""
        If CMB.Text.ToUpper = "CASH" Then
            Lay_PayDetail.Control.Enabled = False
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" ': BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            End If
            Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = "" : Txt_AccountNo.Text = "" : Txt_Branch.Text = ""
            lbl_Branch.ForeColor = Color.Gray : lbl_Account_No.ForeColor = Color.Gray : lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Cdate.ForeColor = Color.Gray
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            Me.GLookUp_BankList.Enabled = False
        Else
            Lay_PayDetail.Control.Enabled = True
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            End If

            Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = "" : Txt_AccountNo.Text = "" : Txt_Branch.Text = ""
            lbl_Branch.ForeColor = Color.Black : lbl_Account_No.ForeColor = Color.Black : lbl_Ref_Bank.ForeColor = Color.Red : lbl_Ref_Title.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red : lbl_Ref_Cdate.ForeColor = Color.Black
            Txt_Ref_No.Enabled = True : Txt_Ref_Date.Enabled = True : Txt_Ref_CDate.Enabled = True
            If Val(TXT_REC_AMOUNT.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Or iAction = Common_Lib.Common.FDAction.Close_FD Then
                If CMB.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Saving Bank Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : lbl_Ref_Cdate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]"
                If CMB.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "Saving Bank Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_Cdate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]"
                If CMB.Text.ToUpper = "BANK ACCOUNT" Then lbl_Ref_Title.Text = "Saving Bank Details:" : Txt_Ref_No.Enabled = True : Txt_Ref_Date.Enabled = False : Txt_Ref_CDate.Enabled = False : lbl_Ref_No.ForeColor = Color.Black : lbl_Ref_No.Text = "Ref. No.:" : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.ForeColor = Color.Gray : lbl_Ref_Cdate.Text = "Clearing Date:"
            Else
                If CMB.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Saving Bank Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : lbl_Ref_Cdate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]"
                If CMB.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "Saving Bank Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_Cdate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]"
                If CMB.Text.ToUpper = "BANK ACCOUNT" Then lbl_Ref_Title.Text = "Saving Bank Details:" : Txt_Ref_No.Enabled = True : Txt_Ref_Date.Enabled = False : Txt_Ref_CDate.Enabled = False : lbl_Ref_No.ForeColor = Color.Black : lbl_Ref_No.Text = "Ref. No.:" : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.ForeColor = Color.Gray : lbl_Ref_Cdate.Text = "Clearing Date:"
            End If
            LookUp_GetBankList()
            Me.GLookUp_BankList.Enabled = True : Me.GLookUp_BankList.Properties.ReadOnly = False
            If GLookUp_BankListView.RowCount = 1 Then
                Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                Me.GLookUp_BankListView.MoveBy(0)
                Me.GLookUp_BankListView.FocusedRowHandle = 0
                Me.GLookUp_BankList.EditValue = Me.GLookUp_BankListView.GetRowCellValue(0, "BA_ID").ToString
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                Me.GLookUp_BankList.Properties.Tag = "SHOW"
            End If
        End If
    End Sub
    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.GotFocus, Cmb_Rec_Mode.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_Mode.KeyDown, Cmb_Rec_Mode.KeyDown
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        If e.KeyCode = Keys.Escape And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.PageUp And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageUp And txt.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub TXT_NRC_DATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXT_NRC_DATE.Click
        If TXT_NRC_DATE.Properties.ReadOnly = True Then
            Me.TXT_NRC_DATE.ClosePopup()
            Me.ToolTip1.ToolTipTitle = "Direct date change not allowed . . ."
            Me.ToolTip1.Show("Please change voucher date to change this date !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
            Me.TXT_NRC_DATE.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If
    End Sub


#End Region

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Branch.GotFocus, Txt_AccountNo.GotFocus, TXT_TDS.GotFocus, TXT_REC_AMOUNT.GotFocus, TXT_INTEREST.GotFocus, TXT_Fd_Bank.GotFocus, TXT_TDS_PREV.GotFocus, TXT_RENEWAL_MATURITY_AMOUNT.GotFocus, TXT_Fd_Bank.GotFocus, TXT_FD_ACT.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amount.KeyPress, Txt_Ref_No.KeyPress, Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress, Txt_Branch.KeyPress, Txt_AccountNo.KeyPress, TXT_TDS.KeyPress, TXT_REC_AMOUNT.KeyPress, TXT_INTEREST.KeyPress, TXT_Fd_Bank.KeyPress, TXT_TDS_PREV.KeyPress, TXT_RENEWAL_MATURITY_AMOUNT.KeyPress, TXT_Fd_Bank.KeyPress, TXT_FD_ACT.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_Branch.KeyDown, Txt_AccountNo.KeyDown, TXT_TDS.KeyDown, TXT_REC_AMOUNT.KeyDown, TXT_INTEREST.KeyDown, TXT_NRC_DATE.KeyDown, TXT_TDS_PREV.KeyDown, TXT_RENEWAL_MATURITY_AMOUNT.KeyDown, TXT_Fd_Bank.KeyDown, TXT_FD_ACT.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()
        If UCase(txt.Name) = "" Then

        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Ref_No.Validated, Txt_Narration.Validated, Txt_Reference.Validated, Txt_Remarks.Validated, Txt_Branch.Validated, Txt_AccountNo.Validated, TXT_TDS.Validated, TXT_REC_AMOUNT.Validated, TXT_INTEREST.Validated, TXT_Fd_Bank.Validated, TXT_TDS_PREV.Validated, TXT_RENEWAL_MATURITY_AMOUNT.Validated, TXT_Fd_Bank.Validated, TXT_FD_ACT.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
    Private Sub TXT_REC_AMOUNT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXT_REC_AMOUNT.EditValueChanged, TXT_RENEWAL_MATURITY_AMOUNT.EditValueChanged
        If GLookUp_FD_List.Tag.ToString.Length > 0 And TXT_REC_AMOUNT.Text.Length > 0 Then
            TXT_INTEREST.Text = Math.Round(Math.Round(Val(TXT_REC_AMOUNT.Text), 2) - Math.Round(Val(Txt_Amount.Text), 2), 2).ToString
        End If
        If Not iAction = Common_Lib.Common.FDAction.Renew_FD Then Exit Sub
        If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
            Lay_PayDetail.Control.Enabled = False : lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray
        End If
        If Val(TXT_REC_AMOUNT.Text) <> Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
            If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                GLookUp_BankList.Properties.ReadOnly = False : GLookUp_BankList.Enabled = True : Lay_PayDetail.Control.Enabled = True
                lbl_Ref_Bank.ForeColor = Color.Red

                If Not Cmb_Rec_Mode.Text.ToUpper = "BANK ACCOUNT" Then
                    lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red
                Else
                    lbl_Ref_No.ForeColor = Color.Black : lbl_Ref_Date.ForeColor = Color.Gray
                End If
            End If
            lblReceiptMode.ForeColor = Color.Red : Cmb_Rec_Mode.Properties.ReadOnly = False
            If Val(TXT_REC_AMOUNT.Text) < Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                lblReceiptMode.Text = "Pay Mode: " : lbl_Ref_Title.Text = "Saving Bank Details:"
            Else
                lblReceiptMode.Text = "Receipt Mode: "
                If Cmb_Rec_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Saving Bank Detail:"
                If Cmb_Rec_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "Saving Bank Detail:"
                If Cmb_Rec_Mode.Text.ToUpper = "BANK ACCOUNT" Then lbl_Ref_Title.Text = "Saving Bank Details:"
            End If
        Else
            lblReceiptMode.ForeColor = Color.Gray : Cmb_Rec_Mode.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub TXT_NRC_DATE_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXT_NRC_DATE.EditValueChanged, GLookUp_FD_List.EditValueChanged
        If IsDate(TXT_NRC_DATE.DateTime) And Me.GLookUp_FD_ListView.FocusedRowHandle >= 0 Then
            If iAction = Common_Lib.Common.FDAction.Renew_FD Then
                If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) > TXT_NRC_DATE.DateTime Then
                    TXT_FD_ACT.Text = "Pre-Mature Renewal"
                Else
                    TXT_FD_ACT.Text = "Renewal On Maturity"
                End If
            ElseIf iAction = Common_Lib.Common.FDAction.Close_FD Then
                If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) > TXT_NRC_DATE.DateTime Then
                    TXT_FD_ACT.Text = "Pre-Mature Closure"
                Else
                    TXT_FD_ACT.Text = "Closure On Maturity"
                End If
            End If
        End If
    End Sub
    Private Sub Txt_V_Date_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_V_Date.EditValueChanged
        If iAction = Common_Lib.Common.FDAction.Close_FD Or iAction = Common_Lib.Common.FDAction.New_FD Or iAction = Common_Lib.Common.FDAction.Renew_FD Then
            TXT_NRC_DATE.DateTime = Txt_V_Date.DateTime
            TXT_NRC_DATE.Properties.ReadOnly = True
        End If
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Fixed Deposits (F.D.) Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        If Me.TitleX.Text.StartsWith("New") Then Me.TitleX.Text = "FD related Income / Expenses"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()

        'Set Item
        Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
        Me.GLookUp_FD_List.ShowPopup() : Me.GLookUp_FD_List.ClosePopup()
        Select Case iAction
            Case Common_Lib.Common.FDAction.New_FD
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", "f6e4da62-821f-4961-9f93-f5177fca2a77")
                Me.GLookUp_ItemList.EditValue = "f6e4da62-821f-4961-9f93-f5177fca2a77"
                C_R_Panel.Enabled = False : lblFDAct.ForeColor = Color.Gray : lblReceiptMode.ForeColor = Color.Gray : lblReceivedAmount.ForeColor = Color.Gray : lblInterest.ForeColor = Color.Gray : lblTDS.ForeColor = Color.Gray : lblTDS_Prev.ForeColor = Color.Gray : Lay_NRC_Date.Text = "FD Start Date: "
                Lay_Select_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never : Lay_View_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never : Lay_FD_Bank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_Renewal_Amount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Txt_Amount.Properties.ReadOnly = False : PanelControl3.Enabled = True : Cmd_Mode.Properties.ReadOnly = False
                Lay_Renewal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                GLookUp_BankList.Width = 537 : Txt_Branch.Width = 537 : Txt_AccountNo.Width = 537 : Txt_Ref_No.Width = 537 : Txt_Ref_Date.Width = 537 : Txt_Ref_CDate.Width = 537
            Case Common_Lib.Common.FDAction.Renew_FD
                C_R_Panel.Enabled = True : lblFDAct.ForeColor = Color.Black : lblReceiptMode.ForeColor = Color.Red : lblReceivedAmount.ForeColor = Color.Red : lblInterest.ForeColor = Color.Red : lblTDS.ForeColor = Color.Black : lblTDS_Prev.ForeColor = Color.Black : Lay_NRC_Date.Text = "Renewal Date:"
                Lay_Select_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_View_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_FD_Bank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never : Lay_Renewal_Amount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_Renewal_Amount.Text = "Renewal Amt:"
                Txt_Amount.Properties.ReadOnly = True
                Lay_Renewal_Amount.Text = "Renewal Amt" : TXT_RENEWAL_MATURITY_AMOUNT.Properties.NullValuePrompt = "Renewal Amount..." : TXT_RENEWAL_MATURITY_AMOUNT.Properties.ReadOnly = False
                'Cmd_Mode.Properties.Items.Remove(Cmd_Mode.Properties.Items(0)) : Cmd_Mode.Text = Cmd_Mode.Properties.Items(0).ToString
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", "4eb60d78-ce90-4a9f-891b-7a82d79dc84b")
                Me.GLookUp_ItemList.EditValue = "4eb60d78-ce90-4a9f-891b-7a82d79dc84b"
                'Lay_Renewal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Common_Lib.Common.FDAction.Close_FD
                lblRenewalTitle.Text = "FD Closure Detail:"
                C_R_Panel.Enabled = True : lblFDAct.ForeColor = Color.Black : lblReceiptMode.ForeColor = Color.Red : lblReceivedAmount.ForeColor = Color.Red : lblInterest.ForeColor = Color.Red : lblTDS.ForeColor = Color.Black : lblTDS_Prev.ForeColor = Color.Black : Lay_NRC_Date.Text = "FD Close Date:"
                Lay_Select_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_View_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_FD_Bank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never : Lay_Renewal_Amount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Lay_Renewal_Amount.Text = "Maturity Amt" : TXT_RENEWAL_MATURITY_AMOUNT.Properties.NullValuePrompt = "Maturity Amount..." : TXT_RENEWAL_MATURITY_AMOUNT.Properties.ReadOnly = True
                Txt_Amount.Properties.ReadOnly = True : Cmb_Rec_Mode.Properties.ReadOnly = False : Cmd_Mode_SelectedIndexChanged(Cmb_Rec_Mode, New System.EventArgs)
                'Cmd_Mode.Properties.Items.Remove(Cmd_Mode.Properties.Items(0)) : Cmd_Mode.Text = Cmd_Mode.Properties.Items(0).ToString
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", "65730a27-e365-4195-853e-2f59225fe8f4")
                Me.GLookUp_ItemList.EditValue = "65730a27-e365-4195-853e-2f59225fe8f4"
                'Lay_Renewal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case Else
                C_R_Panel.Enabled = False : lblFDAct.ForeColor = Color.Gray : lblReceiptMode.ForeColor = Color.Gray : lblReceivedAmount.ForeColor = Color.Gray : lblInterest.ForeColor = Color.Gray : lblTDS.ForeColor = Color.Gray : lblTDS_Prev.ForeColor = Color.Gray
                Lay_NRC_Date.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never : Lay_Select_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_View_Fd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Lay_FD_Bank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Lay_Renewal_Amount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_Renewal_Amount.Text = "Amount:" : TXT_RENEWAL_MATURITY_AMOUNT.Properties.NullValuePrompt = "Amount ..."
                Txt_Amount.Properties.ReadOnly = True
                Lay_FD_Bank.Text = "Bank's Name : "
                Dim _ItemID As String = iSpecific_ItemID
                If _ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715" And (Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete) Then ' FD Interest Received
                    Dim Value As Object = Base._FD_Voucher_DBOps.GetItemCountInSameMaster(Me.xMID.Text, "d0219173-45ff-4284-ae0e-89ba0e8d76b4") ' tds entry in same entry of interest
                    If Value Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    If Value > 0 Then _ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                End If
                If _ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4" Then 'TDS Deducted by bank
                    Lay_PayDetail.Control.Enabled = False : Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    Lay_PayDetail.Control.Enabled = True : Lay_Payment_Mode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always : Lay_Payment_Mode.Text = "Mode: " : Cmd_Mode.Properties.NullValuePrompt = "Select Mode..."
                End If

                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
                Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
                Lay_Renewal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                GLookUp_BankList.Width = 537
                Txt_Branch.Width = 537
                Txt_AccountNo.Width = 537
                Txt_Ref_No.Width = 537
                Txt_Ref_Date.Width = 537
                Txt_Ref_CDate.Width = 537
        End Select
        If Not iAction = Common_Lib.Common.FDAction.New_FD Then
            GLookUp_FD_List.Tag = "" : LookUp_GetFDList()
        End If

        Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Me.GLookUp_ItemList.Properties.ReadOnly = True
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
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            Me.Txt_V_Date.Focus()
        Else
            Me.GLookUp_ItemList.Focus()
        End If
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()
        xPleaseWait.Show("Fixed Deposits (F.D.) Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Dim NewFDTable As DataTable = Nothing, CloseFDTable As DataTable = Nothing, Closetable As DataTable = Nothing, Renewtable As DataTable = Nothing, MasterTable As DataTable = Nothing, PaymentMode As DataTable = Nothing, InterestTable As DataTable = Nothing
        Dim Query As String = ""
        Dim xDate As DateTime = Nothing
        Select Case iAction
            Case Common_Lib.Common.FDAction.New_FD
                Closetable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text)
                If Closetable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                xDate = Closetable.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
                '-----------------------------+
                'Start : Check if entry already changed 
                '-----------------------------+
                If Base.AllowMultiuser() Then
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                        Dim viewstr As String = ""
                        If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                        If Info_LastEditedOn <> Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON")) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                '-----------------------------+
                'End : Check if entry already changed 
                '-----------------------------+

                LastEditedOn = Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON"))

                'New Fd
                CloseFDTable = Base._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString)
                If CloseFDTable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Case Common_Lib.Common.FDAction.Renew_FD
                ' fd close item 
                Closetable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text, "65730a27-e365-4195-853e-2f59225fe8f4")
                If Closetable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                xDate = Closetable.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
                '-----------------------------+
                'Start : Check if entry already changed 
                '-----------------------------+
                If Base.AllowMultiuser() Then
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                        If Info_LastEditedOn <> Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON")) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                '-----------------------------+
                'End : Check if entry already changed 
                '-----------------------------+

                LastEditedOn = Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON"))
                ' fd RENEW item 
                Renewtable = Base._FD_Voucher_DBOps.GetSelectedColumns(Me.xMID.Text, "4eb60d78-ce90-4a9f-891b-7a82d79dc84b")
                If Renewtable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                'Old Fd
                CloseFDTable = Base._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString)
                'New FD
                NewFDTable = Base._FD_Voucher_DBOps.GetFDRecordByID(Renewtable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString)
                'Master
                MasterTable = Base._FD_Voucher_DBOps.GetMasterRecord(Me.xMID.Text)
                'PayMode
                PaymentMode = Base._FD_Voucher_DBOps.GetPaymentRecords(Me.xMID.Text)
                'Interest
                InterestTable = Base._FD_Voucher_DBOps.GetInterestRecords(Me.xMID.Text, "c92da5ab-082d-45d9-b6b7-78752625c715")
                If CloseFDTable Is Nothing Or NewFDTable Is Nothing Or MasterTable Is Nothing Or PaymentMode Is Nothing Or InterestTable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Case Common_Lib.Common.FDAction.Close_FD
                ' fd close principle 
                Closetable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text, "65730a27-e365-4195-853e-2f59225fe8f4")
                If Closetable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Closetable.DefaultView.Sort = "TR_REF_BANK_ID desc" ' Fixed FD close issues , where bank account dropdown did not get filled on edit/delete 
                Dim closeTableSort As DataTable = Closetable.DefaultView.ToTable
                Closetable = closeTableSort
                xDate = Closetable.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
                '-----------------------------+
                'Start : Check if entry already changed 
                '-----------------------------+
                If Base.AllowMultiuser() Then
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                        If Info_LastEditedOn <> Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON")) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                '-----------------------------+
                'End : Check if entry already changed 
                '-----------------------------+

                LastEditedOn = Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON"))

                'Old Fd
                CloseFDTable = Base._FD_Voucher_DBOps.GetFDRecordByID(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString)
                'Master
                MasterTable = Base._FD_Voucher_DBOps.GetMasterRecord(Me.xMID.Text)
                'Interest
                InterestTable = Base._FD_Voucher_DBOps.GetInterestRecords(Me.xMID.Text, "1ed5cbe4-c8aa-4583-af44-eba3db08e117")
                If CloseFDTable Is Nothing Or MasterTable Is Nothing Or InterestTable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Case Else
                ' Txn
                Closetable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text)
                If Closetable Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                xDate = Closetable.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
                '-----------------------------+
                'Start : Check if entry already changed 
                '-----------------------------+
                If Base.AllowMultiuser() Then
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                        If Info_LastEditedOn <> Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON")) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                '-----------------------------+
                'End : Check if entry already changed 
                '-----------------------------+

                LastEditedOn = Convert.ToDateTime(Closetable.Rows(0)("REC_EDIT_ON"))

        End Select

        Txt_V_NO.DataBindings.Add("TEXT", Closetable, "TR_VNO")
        Cmd_Mode.DataBindings.Add("text", Closetable, "TR_MODE")
        Txt_Amount.DataBindings.Add("EditValue", Closetable, "TR_AMOUNT")
        Txt_Narration.DataBindings.Add("text", Closetable, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", Closetable, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", Closetable, "TR_REFERENCE")

        Me.xID.Text = Closetable.Rows(0)("REC_ID")
        If iAction = Common_Lib.Common.FDAction.New_FD Then

            If Not IsDBNull(Closetable.Rows(0)("TR_SUB_CR_LED_ID")) Then
                If Closetable.Rows(0)("TR_SUB_CR_LED_ID").ToString.Length > 0 Then
                    Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                    Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_SUB_CR_LED_ID")))
                    Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_SUB_CR_LED_ID"))
                    Me.GLookUp_BankList.EditValue = Closetable.Rows(0)("TR_SUB_CR_LED_ID")
                    Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                    Me.GLookUp_BankList.Properties.Tag = "SHOW"
                End If
            End If : Me.GLookUp_BankList.Properties.ReadOnly = False

            Txt_Ref_No.DataBindings.Add("text", Closetable, "TR_REF_NO")
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_DATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
            End If
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_CDATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
            End If
            If Not IsDBNull(CloseFDTable.Rows(0)("FD_DATE")) Then
                xDate = CloseFDTable.Rows(0)("FD_DATE") : TXT_NRC_DATE.DateTime = xDate
            End If

            If Not IsDBNull(Closetable.Rows(0)("TR_ITEM_ID")) Then
                If Closetable.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                    Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                    'Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Closetable.Rows(0)("TR_ITEM_ID")))
                    Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Closetable.Rows(0)("TR_ITEM_ID"))
                    Me.GLookUp_ItemList.EditValue = Closetable.Rows(0)("TR_ITEM_ID")
                    Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                    Me.GLookUp_ItemList.Properties.Tag = "SHOW"
                End If
            End If
        End If

        If iAction = Common_Lib.Common.FDAction.Renew_FD Then
            If Not IsDBNull(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                If Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                    Me.GLookUp_FD_List.ShowPopup() : Me.GLookUp_FD_List.ClosePopup()
                    Me.GLookUp_FD_ListView.FocusedRowHandle = Me.GLookUp_FD_ListView.LocateByValue("FD_ID", Closetable.Rows(0)("TR_TRF_CROSS_REF_ID"))
                    Me.GLookUp_FD_List.EditValue = Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")
                    Me.GLookUp_FD_List.Tag = Me.GLookUp_FD_List.EditValue
                    Me.GLookUp_FD_List.Properties.Tag = "SHOW"
                    GLookUp_FD_List_EditValueChanged(Me, New System.EventArgs)
                    Me.GLookUp_FD_List.Properties.ReadOnly = True
                End If
            End If

            If PaymentMode.Rows.Count > 0 Then
                If Not IsDBNull(PaymentMode.Rows(0)("TR_MODE")) Then
                    Cmb_Rec_Mode.EditValue = PaymentMode.Rows(0)("TR_MODE")
                End If
                Txt_Ref_No.DataBindings.Add("text", PaymentMode, "TR_REF_NO")
                If Not IsDBNull(PaymentMode.Rows(0)("TR_REF_DATE")) Then
                    xDate = PaymentMode.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
                End If
                If Not IsDBNull(PaymentMode.Rows(0)("TR_REF_CDATE")) Then
                    xDate = PaymentMode.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
                End If
                Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", PaymentMode.Rows(0)("TR_REF_BANK_ID")))
                Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", PaymentMode.Rows(0)("TR_REF_BANK_ID"))
                Me.GLookUp_BankList.EditValue = PaymentMode.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                Me.GLookUp_BankList.Properties.Tag = "SHOW"
                Me.GLookUp_BankList.Properties.ReadOnly = False
            End If

            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            'Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Closetable.Rows(0)("TR_ITEM_ID")))
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", "4eb60d78-ce90-4a9f-891b-7a82d79dc84b")
            Me.GLookUp_ItemList.EditValue = "4eb60d78-ce90-4a9f-891b-7a82d79dc84b"
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"

            Dim fdDate As DateTime = Nothing
            If IsDate(CloseFDTable.Rows(0)("FD_CLOSE_DATE")) Then fdDate = CloseFDTable.Rows(0)("FD_CLOSE_DATE") : TXT_NRC_DATE.DateTime = fdDate

            TXT_RENEWAL_MATURITY_AMOUNT.EditValue = 0
            For Each CurrRow As DataRow In Renewtable.Rows
                TXT_RENEWAL_MATURITY_AMOUNT.EditValue += Val(CurrRow("TR_AMOUNT").ToString)
            Next
            TXT_REC_AMOUNT.DataBindings.Add("EditValue", MasterTable, "TR_SUB_AMT")
            If Val(TXT_REC_AMOUNT.Text) <> Val(TXT_RENEWAL_MATURITY_AMOUNT.EditValue) And PaymentMode.Rows.Count = 0 Then
                Cmb_Rec_Mode.EditValue = "CASH"
            End If
            TXT_TDS.DataBindings.Add("EditValue", MasterTable, "TR_TDS_AMT")
            TXT_NRC_DATE_EditValueChanged(Me, New System.EventArgs)
            TXT_REC_AMOUNT_EditValueChanged(Me, New System.EventArgs)
        End If

        If iAction = Common_Lib.Common.FDAction.Close_FD Then
            If Not IsDBNull(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                If Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                    Me.GLookUp_FD_List.ShowPopup() : Me.GLookUp_FD_List.ClosePopup()
                    Me.GLookUp_FD_ListView.FocusedRowHandle = Me.GLookUp_FD_ListView.LocateByValue("FD_ID", Closetable.Rows(0)("TR_TRF_CROSS_REF_ID"))
                    Me.GLookUp_FD_List.EditValue = Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")
                    Me.GLookUp_FD_List.Tag = Me.GLookUp_FD_List.EditValue
                    Me.GLookUp_FD_List.Properties.Tag = "SHOW"
                    GLookUp_FD_List_EditValueChanged(Me, New System.EventArgs)
                    Me.GLookUp_FD_List.Properties.ReadOnly = True
                End If
            End If

            If Not IsDBNull(Closetable.Rows(0)("TR_MODE")) Then
                Cmb_Rec_Mode.EditValue = Closetable.Rows(0)("TR_MODE")
            End If
            Txt_Ref_No.DataBindings.Add("text", Closetable, "TR_REF_NO")
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_DATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
            End If
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_CDATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
            End If
            TXT_TDS.DataBindings.Add("EditValue", MasterTable, "TR_TDS_AMT")
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            'Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Closetable.Rows(0)("TR_ITEM_ID")))
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", "65730a27-e365-4195-853e-2f59225fe8f4")
            Me.GLookUp_ItemList.EditValue = "65730a27-e365-4195-853e-2f59225fe8f4"
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"

            Dim fdDate As DateTime = Nothing
            If IsDate(CloseFDTable.Rows(0)("FD_CLOSE_DATE")) Then fdDate = CloseFDTable.Rows(0)("FD_CLOSE_DATE") : TXT_NRC_DATE.DateTime = fdDate

            TXT_REC_AMOUNT.DataBindings.Add("EditValue", MasterTable, "TR_SUB_AMT")
            TXT_NRC_DATE_EditValueChanged(Me, New System.EventArgs)
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_REF_BANK_ID")))
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_REF_BANK_ID"))
            Me.GLookUp_BankList.EditValue = Closetable.Rows(0)("TR_REF_BANK_ID")
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
            Me.GLookUp_BankList.Properties.ReadOnly = False
        End If

        If iAction = -1 Then
            If Not IsDBNull(Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                If Closetable.Rows(0)("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                    Me.GLookUp_FD_List.ShowPopup() : Me.GLookUp_FD_List.ClosePopup()
                    Me.GLookUp_FD_ListView.FocusedRowHandle = Me.GLookUp_FD_ListView.LocateByValue("FD_ID", Closetable.Rows(0)("TR_TRF_CROSS_REF_ID"))
                    Me.GLookUp_FD_List.EditValue = Closetable.Rows(0)("TR_TRF_CROSS_REF_ID")
                    Me.GLookUp_FD_List.Tag = Me.GLookUp_FD_List.EditValue
                    Me.GLookUp_FD_List.Properties.Tag = "SHOW"
                    GLookUp_FD_List_EditValueChanged(Me, New System.EventArgs)
                    Me.GLookUp_FD_List.Properties.ReadOnly = True
                End If
            End If

            If Not IsDBNull(Closetable.Rows(0)("TR_MODE")) Then
                Cmb_Rec_Mode.EditValue = Closetable.Rows(0)("TR_MODE")
            End If
            Txt_Ref_No.DataBindings.Add("text", Closetable, "TR_REF_NO")
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_DATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
            End If
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_CDATE")) Then
                xDate = Closetable.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
            End If

            Dim _ItemID As String = Closetable.Rows(0)("TR_ITEM_ID")
            If _ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715" Then ' FD Interest Received
                If Base._FD_Voucher_DBOps.GetItemCountInSameMaster(xMID.Text, "d0219173-45ff-4284-ae0e-89ba0e8d76b4") > 0 Then _ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4" ' tds entry in same entry of interest
            End If
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            'Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Closetable.Rows(0)("TR_ITEM_ID")))
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", _ItemID)
            Me.GLookUp_ItemList.EditValue = _ItemID
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"

            TXT_RENEWAL_MATURITY_AMOUNT.DataBindings.Add("EditValue", Closetable, "TR_AMOUNT")
            If Not IsDBNull(Closetable.Rows(0)("TR_REF_BANK_ID")) Then
                Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_REF_BANK_ID")))
                Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Closetable.Rows(0)("TR_REF_BANK_ID"))
                Me.GLookUp_BankList.EditValue = Closetable.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                Me.GLookUp_BankList.Properties.Tag = "SHOW"
                Me.GLookUp_BankList.Properties.ReadOnly = False
            End If
        End If

        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_FD_List.Enabled = False : Me.GLookUp_FD_List.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_NRC_DATE.Enabled = False : Me.TXT_NRC_DATE.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_RENEWAL_MATURITY_AMOUNT.Enabled = False : Me.TXT_RENEWAL_MATURITY_AMOUNT.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_Fd_Bank.Enabled = False : Me.TXT_Fd_Bank.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.TXT_FD_ACT.Enabled = False : Me.TXT_FD_ACT.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_REC_AMOUNT.Enabled = False : Me.TXT_REC_AMOUNT.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_INTEREST.Enabled = False : Me.TXT_INTEREST.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmb_Rec_Mode.Enabled = False : Me.Cmb_Rec_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_TDS.Enabled = False : Me.TXT_TDS.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.TXT_TDS_PREV.Enabled = False : Me.TXT_TDS_PREV.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Branch.Enabled = False : Me.Txt_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_AccountNo.Enabled = False : Me.Txt_AccountNo.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor

    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.GLookUp_BankList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
        Me.ToolTip1.Hide(Me.Txt_Amount)
    End Sub
    Private Sub NewFD_Functionality(ByVal sender As System.Object)
        Hide_Properties()

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then

            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim FDvoucher_DbOps As DataTable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text)
                If FDvoucher_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(FDvoucher_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If

            'Special Checks
            'Dim xCross_Ref_Id As String = ""
            'Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.xMID.Text)
            'If Status Is Nothing Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            'If Not IsDBNull(Status.Rows(0)("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = Status.Rows(0)("TR_TRF_CROSS_REF_ID")
            'Dim IsClosed As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text) ' checks if the fd is closed
            ''CHECK IF THE CURREMT FD HAS ANY TDS OR INTEREST AGAINST IT, OTHER THAN IN CURRENT TRANSACTION 
            'Dim Has_Int_TDS As Object = Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, xCross_Ref_Id, 1) 'checks if any interest or tds has been posted against the FD 

            'If IsDate(IsClosed) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("F D  A l r e a d y  C l o s e d / R e n ew e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf Has_Int_TDS > 0 Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t / T D S  a l r e a d y  e n t e r e d  a g a i n s t  F D!" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
              Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
              Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If Me.Cmd_Mode.Text.Length <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S e l e c t  P a y m e n t  M o d e . . . !", Me.Cmd_Mode, 0, Me.Cmd_Mode.Height, 5000)
                Me.Cmd_Mode.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_Mode)
            End If

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If

            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
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

        End If


        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            Dr_Led_id = iLed_ID
            If Cmd_Mode.Text.ToUpper = "CASH" Then
                Cr_Led_id = "00080" 'Cash A/c.
            Else
                Cr_Led_id = "00079" 'Bank A/c.
                Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
            End If
        Else
            Cr_Led_id = iLed_ID
            If Cmd_Mode.Text.ToUpper = "CASH" Then
                Dr_Led_id = "00080" 'Cash A/c.
            Else
                Dr_Led_id = "00079" 'Bank A/c.
                Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag
            End If
        End If

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_NewFD_InsertVoucherFD = New Common_Lib.RealTimeService.Param_Txn_NewFD_InsertVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Dim FDRecID As String = ""

            Dim xfrm As New Frm_FD_Window
            xfrm.Txt_Amount.Text = Txt_Amount.Text
            xfrm.Txt_Amount.Properties.ReadOnly = True
            xfrm.Tag = Me.Tag
            xfrm.Status = Common_Lib.Common.FDStatus.New_FD.ToString
            xfrm.iAction = Common_Lib.Common.FDAction.New_FD
            xfrm.TxnID = Me.xMID.Text
            xfrm.Txt_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.Txt_As_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.Txt_Date.Properties.ReadOnly = True
            xfrm.Txt_As_Date.Properties.ReadOnly = True
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                FDRecID = xfrm.xID.Text
                If Base.AllowMultiuser() Then
                    'Closed Bank Acc Check #g35
                    If Me.GLookUp_BankList.Enabled Then
                        Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                        If IsDBNull(AccNo) Then AccNo = ""
                        If AccNo.Length > 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d . . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                    'FD Account dependency check #Ref H35
                    If xfrm.Look_BankList.Tag.ToString.Length > 0 Then
                        Dim FDAccount As DataTable = Base._FD_Voucher_DBOps.GetFDBankAccounts(xfrm.Look_BankList.Tag)
                        If FDAccount Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        Dim EditTime As DateTime = xfrm.Look_BankList.GetColumnValue("REC_EDIT_ON")
                        If FDAccount.Rows.Count <= 0 Then 'A/D,E/D
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        Else
                            Dim NewEditOn As DateTime = FDAccount.Rows(0)("REC_EDIT_ON")
                            If EditTime <> NewEditOn Then 'A/E,E/E
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                xfrm.Dispose()
            Else
                xfrm.Dispose()
                Exit Sub
            End If

            '----------------------// Start Dependencies //----------------------
            If Base.AllowMultiuser() Then
                Dim oldEditOn As DateTime
                'Bank A/c dependency check #Ref G35
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then 'A/D,E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If

            '-----------------------// End Dependencies //-------------------------

            Try
                'Master Record 
                Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD()
                InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InMInfo.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
                'InMInfo.TDate = Txt_V_Date.Text
                InMInfo.SubTotal = Val(Txt_Amount.Text)
                InMInfo.Cash = 0
                InMInfo.Bank = 0
                InMInfo.TDS = 0
                InMInfo.Status_Action = Status_Action
                InMInfo.RecID = Me.xMID.Text

                'If Not Base._FD_Voucher_DBOps.InsertMasterInfo(InMInfo) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertMaster = InMInfo

                ''---start: Fd Insertion shifted from frm_fd_winfow--''
                'Insert FD
                'If Not Base._FD_Voucher_DBOps.InsertFD(xfrm.InFD) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertFD = xfrm.InFD

                'insert FD history
                'If Not Base._FD_Voucher_DBOps.InsertFDHistory(xfrm.InFDHty) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertFDHistory = xfrm.InFDHty
                ''---end: Fd Insertion shifted from frm_fd_winfow--''

                'Txn
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                InParam.ItemID = Me.GLookUp_ItemList.Tag
                InParam.Type = iTrans_Type
                InParam.Cr_Led_ID = Cr_Led_id
                InParam.Dr_Led_ID = Dr_Led_id
                InParam.SUB_Cr_Led_ID = Sub_Cr_Led_ID
                InParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID
                InParam.Amount = Val(Me.Txt_Amount.Text)
                InParam.Mode = Cmd_Mode.Text
                InParam.Ref_BANK_ID = ""
                InParam.Ref_Branch = ""
                InParam.Ref_No = Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_CDate = Txt_Ref_CDate.Text
                'InParam.Ref_Date = Me.Txt_Ref_Date.Text.Trim
                'InParam.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.Txt_Remarks.Text
                InParam.Reference = Me.Txt_Reference.Text
                InParam.FDID = FDRecID
                InParam.MasterTxnID = Me.xMID.Text
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text

                'If Not Base._FD_Voucher_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_Insert = InParam
            Catch ex As Exception
                'delete Entered FD Entry in case of exception in entering TXN
                ' Base._FD_Voucher_DBOps.DeleteFD(Me.xMID.Text)
                ' Base._FD_Voucher_DBOps.DeleteFDHistory(Me.xMID.Text)
                DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!", Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try

            If Not Base._FD_Voucher_DBOps.InsertNewFD_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_NewFD_UpdateVoucherFD = New Common_Lib.RealTimeService.Param_Txn_NewFD_UpdateVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim FDRecID As String = ""
            Dim xfrm As New Frm_FD_Window
            xfrm.Tag = Me.Tag
            xfrm.Status = Common_Lib.Common.FDStatus.New_FD.ToString
            xfrm.iAction = Common_Lib.Common.FDAction.New_FD
            xfrm.Txt_Amount.Text = Me.Txt_Amount.Text
            xfrm.Txt_Amount.Properties.ReadOnly = True
            xfrm.Txt_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.Txt_As_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.Txt_Date.Properties.ReadOnly = True
            xfrm.Txt_As_Date.Properties.ReadOnly = True
            xfrm.TxnID = Me.xMID.Text
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                FDRecID = xfrm.xID.Text
                If Base.AllowMultiuser() Then
                    'FD Account dependency check #Ref H35
                    If xfrm.Look_BankList.Tag.ToString.Length > 0 Then
                        Dim FDAccount As DataTable = Base._FD_Voucher_DBOps.GetFDBankAccounts(xfrm.Look_BankList.Tag)
                        If FDAccount Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If

                        Dim EditTime As DateTime = xfrm.Look_BankList.GetColumnValue("REC_EDIT_ON")
                        If FDAccount.Rows.Count <= 0 Then 'A/D,E/D
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        Else
                            Dim NewEditOn As DateTime = FDAccount.Rows(0)("REC_EDIT_ON")
                            If EditTime <> NewEditOn Then 'A/E,E/E
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                xfrm.Dispose()
            Else
                xfrm.Dispose()
                Exit Sub
            End If

            If Base.AllowMultiuser() Then
                Dim MaxValue As Object = 0
                MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text) 'bug 5353 fix
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'Closed Bank Acc Check #g35
                If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                    If IsDBNull(AccNo) Then AccNo = ""
                    If AccNo.Length > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t e d . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If

                Dim oldEditOn As DateTime
                'Bank A/c dependency check #Ref G35
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then 'A/D,E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If


                Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text)
                If IsDate(CloseDate) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, FDRecID, 1) > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Interest / TDS Posted against Current FD.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

            End If

            'Maaster Record 
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD()
            UpMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            'UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.SubTotal = Val(Txt_Amount.Text)
            UpMInfo.Cash = 0
            UpMInfo.Bank = 0
            UpMInfo.TDS = 0
            UpMInfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.UpdateMasterInfo(UpMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMInfo

            ''---start: Fd updation shifted from frm_fd_winfow--''
            'If Not Base._FD_Voucher_DBOps.UpdateFD(xfrm.UpFD) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateFD = xfrm.UpFD

            'If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpFDHis) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateFDHistory = xfrm.UpFDHis

            'If iAction = Common_Lib.Common.FDAction.Renew_FD Then
            '    If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpRenFDHty) Then
            '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If
            ''---end: Fd updation shifted from frm_fd_winfow--''
            EditParam.param_DeleteVoucher_Txn_MID = Me.xMID.Text

            'If Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
            Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
            InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InParam1.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
            'InParam1.TDate = Me.Txt_V_Date.Text.Trim()
            InParam1.ItemID = Me.GLookUp_ItemList.Tag
            InParam1.Type = iTrans_Type
            InParam1.Cr_Led_ID = Cr_Led_id
            InParam1.Dr_Led_ID = Dr_Led_id
            InParam1.SUB_Cr_Led_ID = Sub_Cr_Led_ID
            InParam1.SUB_Dr_Led_ID = Sub_Dr_Led_ID
            InParam1.Amount = Val(Me.Txt_Amount.Text)
            InParam1.Mode = Cmd_Mode.Text
            InParam1.Ref_BANK_ID = ""
            InParam1.Ref_Branch = ""
            InParam1.Ref_No = Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam1.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam1.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.Ref_CDate = Txt_Ref_CDate.Text
            'InParam1.Ref_Date = Me.Txt_Ref_Date.Text.Trim
            'InParam1.Ref_CDate = Me.Txt_Ref_CDate.Text.Trim
            InParam1.Narration = Me.Txt_Narration.Text
            InParam1.Remarks = Me.Txt_Remarks.Text
            InParam1.Reference = Me.Txt_Reference.Text
            InParam1.FDID = FDRecID
            InParam1.MasterTxnID = Me.xMID.Text
            InParam1.Status_Action = Status_Action
            InParam1.RecID = Me.xID.Text


            'If Not Base._FD_Voucher_DBOps.Insert(InParam1) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_Insert = InParam1
            'Else
            '   DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            'STR1 = " UPDATE TRANSACTION_INFO SET " & _
            '                        " TR_VNO         ='" & Me.Txt_V_NO.Text & "', " & _
            '                        " TR_DATE        =#" & xDate.ToString(Base._Date_Format_Short) & "#, " & _
            '                        " TR_ITEM_ID     ='" & Me.GLookUp_ItemList.Tag & "', " & _
            '                        " TR_TYPE        ='" & iTrans_Type & "', " & _
            '                        " TR_CR_LED_ID   ='" & Cr_Led_id & "', " & _
            '                        " TR_DR_LED_ID   ='" & Dr_Led_id & "', " & _
            '                        " TR_SUB_CR_LED_ID  ='" & Me.GLookUp_BankList.Tag & "', " & _
            '                        " TR_MODE        ='" & Me.Cmd_Mode.Text & "', " & _
            '                        " TR_REF_NO      ='" & Me.Txt_Ref_No.Text & "', " & _
            '                        " TR_REF_DATE    = " & Me.Txt_Ref_Date.Tag & ", " & _
            '                        " TR_REF_CDATE   = " & Me.Txt_Ref_CDate.Tag & ", " & _
            '                        " TR_AMOUNT      = " & Val(Me.Txt_Amount.Text) & ", " & _
            '                        " TR_NARRATION   ='" & Me.Txt_Narration.Text & "', " & _
            '                        " TR_REMARKS     ='" & Me.Txt_Remarks.Text & "', " & _
            '                        " TR_REFERENCE   ='" & Me.Txt_Reference.Text & "', " & _
            '                        " " & _
            '                        " REC_EDIT_ON    =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
            '                        " REC_EDIT_BY    ='" & Base._open_User_ID & "', " & _
            '                        " REC_STATUS     = " & Status_Action & "," & _
            '                        " REC_STATUS_ON  =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
            '                        " REC_STATUS_BY  ='" & Base._open_User_ID & "'  " & _
            '                        " WHERE TR_M_ID   ='" & Me.xMID.Text & "'"
            'Command.CommandText = STR1 : Command.ExecuteNonQuery() 'FD REC ID in Cross Ref ID is not getting Updated 
            'trans.Commit()

            If Not Base._FD_Voucher_DBOps.UpdateNewFD_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_NewFD_DeleteVoucherFD = New Common_Lib.RealTimeService.Param_Txn_NewFD_DeleteVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            If Base.AllowMultiuser() Then
                Dim MaxValue As Object = 0
                MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text) 'bug 5353 fix
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'Closed Bank Acc Check #g35
                If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                    If IsDBNull(AccNo) Then AccNo = ""
                    If AccNo.Length > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If

                Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text)
                If IsDate(CloseDate) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, CreatedFDID, 1) > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Interest / TDS Posted against Current FD.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

            End If

            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then

                'REMOVE FD HISTORY
                'If Not Base._FD_Voucher_DBOps.DeleteFDHistory(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteFDHistory = Me.xMID.Text
                ''REMOVE FD
                'If Not Base._FD_Voucher_DBOps.DeleteFD(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteFD = Me.xMID.Text
                'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_Delete = Me.xMID.Text
                'If Not Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text

                If Not Base._FD_Voucher_DBOps.DeleteNewFD_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub RenewFD_Functonality(ByVal sender As System.Object)
        Hide_Properties()

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then

            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim FDvoucher_DbOps As DataTable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text, "65730a27-e365-4195-853e-2f59225fe8f4")
                If FDvoucher_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(FDvoucher_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If

            'Special Checks
            'Dim xCross_Ref_Id As String = ""
            'Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.xMID.Text)
            'If Status Is Nothing Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            'If Not IsDBNull(Status.Rows(0)("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = Status.Rows(0)("TR_TRF_CROSS_REF_ID")
            'Dim IsClosed As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text) ' checks if the fd is closed
            ''CHECK IF THE CURREMT FD HAS ANY TDS OR INTEREST AGAINST IT, OTHER THAN IN CURRENT TRANSACTION 
            'Dim Has_Int_TDS As Object = Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, xCross_Ref_Id, 1) 'checks if any interest or tds has been posted against the FD 

            'If IsDate(IsClosed) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("F D  A l r e a d y  C l o s e d / R e n ew e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf Has_Int_TDS > 0 Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t / T D S  a l r e a d y  e n t e r e d  a g a i n s t  F D!" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
              Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
              Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window


            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If (Len(Trim(Me.GLookUp_FD_List.Tag)) = 0 Or Len(Trim(Me.GLookUp_FD_List.Text)) = 0) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F D   N o t   S e l e c t e d . . . !", Me.GLookUp_FD_List, 0, Me.GLookUp_FD_List.Height, 5000)
                Me.GLookUp_FD_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FD_List)
            End If
            If Len(Trim(Me.GLookUp_FD_List.Text)) = 0 Then Me.GLookUp_FD_List.Tag = ""

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If IsDate(Me.TXT_NRC_DATE.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                Me.TXT_NRC_DATE.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
            End If

            If IsDate(Me.TXT_NRC_DATE.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.TXT_NRC_DATE.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.TXT_NRC_DATE.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If

                If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_AS_DATE").ToString) >= TXT_NRC_DATE.DateTime Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("R e n e w a l  D a t e  m u s t  b e  G r e a t e r  t h a n  F D  A s  O f  D a t e  . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If
            End If

            If TXT_RENEWAL_MATURITY_AMOUNT.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("R e n e w a l  A m o u n t  N o t  E n t e r e d . . . !", Me.TXT_RENEWAL_MATURITY_AMOUNT, 0, Me.TXT_RENEWAL_MATURITY_AMOUNT.Height, 5000)
                Me.TXT_RENEWAL_MATURITY_AMOUNT.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_RENEWAL_MATURITY_AMOUNT)
            End If

            If Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) < Val(Txt_Amount.Text) And Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) <= TXT_NRC_DATE.DateTime Then
                xPromptWindow = New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Renewal amount is less than F.D. Amount...!" & vbNewLine & vbNewLine & "Are you sure you want to Save...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.TXT_RENEWAL_MATURITY_AMOUNT.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
            End If

            If Val(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_AMOUNT").ToString) < Val(TXT_REC_AMOUNT.Text) And Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) < TXT_NRC_DATE.DateTime Then
                xPromptWindow = New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Received amount should not be greater than Maturity Amount...!" & vbNewLine & vbNewLine & "Are you sure you want to Save...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.TXT_RENEWAL_MATURITY_AMOUNT.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
            End If

            If TXT_REC_AMOUNT.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("R e c e i v e d  A m o u n t  N o t  E n t e r e d . . . !", Me.TXT_REC_AMOUNT, 0, Me.TXT_REC_AMOUNT.Height, 5000)
                Me.TXT_REC_AMOUNT.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_REC_AMOUNT)
            End If

            'If Val(TXT_REC_AMOUNT.Text) > Val(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_AMOUNT").ToString) Then
            '    xPromptWindow = New Common_Lib.Prompt_Window
            '    If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Received amount should not be greater than Maturity Amount...!" & vbNewLine & vbNewLine & "Are you sure you want to Save...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
            '        xPromptWindow.Dispose()
            '        Me.TXT_REC_AMOUNT.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        xPromptWindow.Dispose()
            '    End If
            'End If

            If TXT_INTEREST.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I n t e r e s t  N o t  E n t e r e d . . . !", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
                Me.TXT_INTEREST.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_INTEREST)
            End If

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And Val(TXT_REC_AMOUNT.Text) <> Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) And Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And Cmb_Rec_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmb_Rec_Mode.Text.ToUpper <> "CASH" And Val(TXT_REC_AMOUNT.Text) <> Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And Cmb_Rec_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmb_Rec_Mode.Text.ToUpper <> "CASH" And Val(TXT_REC_AMOUNT.Text) <> Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If

            If Cmb_Rec_Mode.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("R e c e i p t  M o d e  N o t  S e l e c t e d . . . !", Me.Cmb_Rec_Mode, 0, Me.Cmb_Rec_Mode.Height, 5000)
                Me.Cmb_Rec_Mode.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmb_Rec_Mode)
            End If

            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
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

            'If Val(TXT_TDS.Text) = 0 Then
            '    If (Val(TXT_REC_AMOUNT.Text) <> Val(Txt_Amount.Text) + Val(TXT_INTEREST.Text)) Then
            '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '        Me.ToolTip1.Show("I n t e r e s t  O r  R e c e i v e d  A m o u n t  I n c o r r e c t . . . !" & vbNewLine & vbNewLine & "Received  Amount  Includes  FD  Amount + Interest", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
            '        Me.TXT_INTEREST.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.TXT_INTEREST)
            '    End If
            'End If

            'If Val(TXT_TDS.Text) > 0 Then
            '    If Val(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_AMOUNT").ToString) > (Val(TXT_TDS.Text) + (Val(TXT_TDS_PREV.Text) + Val(Txt_Amount.Text) + Val(TXT_INTEREST.Text)) Then
            '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '        Me.ToolTip1.Show("I n t e r e s t  O r  T D S  I n c o r r e c t . . . !" & vbNewLine & vbNewLine & "TDS + FD Amount + Interest Can't be less than Maturity Amount.", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
            '        Me.TXT_INTEREST.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.TXT_INTEREST)
            '    End If
            'End If
        End If

        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_RenewFD_InsertVoucherFD = New Common_Lib.RealTimeService.Param_Txn_RenewFD_InsertVoucherFD

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim FDRecID As String = ""
            Dim xfrm As New Frm_FD_Window
            xfrm.Look_BankList.EditValue = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BA_ID").ToString
            xfrm.Look_BankList.Properties.ReadOnly = True
            xfrm.Lbl_Branch.Text = "Branch: " & Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_BRANCH").ToString
            xfrm.Lbl_AccountNo.Text = "Customer No.: " & Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_CUST_NO").ToString
            xfrm.Look_BankList.Tag = xfrm.Look_BankList.EditValue
            xfrm.Look_BankList.Properties.Tag = "SHOW"
            xfrm.Txt_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.Txt_Date.Properties.ReadOnly = True
            xfrm.Txt_Amount.EditValue = TXT_RENEWAL_MATURITY_AMOUNT.Text
            xfrm.Txt_Amount.Properties.ReadOnly = True
            xfrm.iRenewFrom = Me.GLookUp_FD_List.Tag
            xfrm.iRenewFDNo = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_NO").ToString
            xfrm.MatDate = Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString)
            xfrm.Tag = Me.Tag
            xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD
            If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) <= TXT_NRC_DATE.DateTime Then
                xfrm.Status = Common_Lib.Common.FDStatus.Matured_Renewed_FD.ToString
            Else
                xfrm.Status = Common_Lib.Common.FDStatus.Premature_Renewed_FD.ToString
            End If
            'xfrm.Txt_Date.Text = TXT_NRC_DATE.Text
            xfrm.TxnID = Me.xMID.Text
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                FDRecID = xfrm.xID.Text
                xfrm.Dispose()
            Else
                xfrm.Dispose()
                Exit Sub
            End If


            If Base.AllowMultiuser() Then
                'Closed Bank Acc Check #g36
                If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                    If IsDBNull(AccNo) Then AccNo = ""
                    If AccNo.Length > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user.... ", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
                Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDateByFdID(Me.GLookUp_FD_List.Tag)
                If IsDate(CloseDate) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                Dim oldEditOn As DateTime
                'Bank Account dependency check #Ref G36
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim BankAcc As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If BankAcc.Rows.Count <= 0 Then 'A/D, E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = BankAcc.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

                'FDs dependency check #Ref AH36
                If GLookUp_FD_List.Tag.ToString.Length > 0 Then
                    Dim FDs As DataTable '= Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag)
                    If (GLookUp_ItemListView.FocusedRowHandle >= 0 And Me.GLookUp_ItemList.Tag <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" And Me.GLookUp_ItemList.Tag <> "f6e4da62-821f-4961-9f93-f5177fca2a77" And Me.GLookUp_ItemList.Tag <> "65730a27-e365-4195-853e-2f59225fe8f4") Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                        FDs = Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag, True)
                    Else
                        FDs = Base._FD_Voucher_DBOps.GetFDs(False, GLookUp_FD_List.Tag)
                    End If
                    oldEditOn = GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "REC_EDIT_ON")
                    If FDs.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = FDs.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

            End If


            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID ' NEW FD
                Cr_Led_id = iLed_ID 'OLD FD
            Else
                Cr_Led_id = iLed_ID
                Dr_Led_id = iLed_ID
            End If
            Try
                'Master Record 
                Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD()
                InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InMInfo.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
                'InMInfo.TDate = Txt_V_Date.Text
                InMInfo.SubTotal = Val(TXT_REC_AMOUNT.Text)
                InMInfo.Cash = 0
                InMInfo.Bank = 0
                InMInfo.TDS = Val(TXT_TDS.Text)
                InMInfo.Status_Action = Status_Action
                InMInfo.RecID = Me.xMID.Text

                InNewParam.param_InsertMaster = InMInfo
                'If Not Base._FD_Voucher_DBOps.InsertMasterInfo(InMInfo) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                '--Start:FD Save shifted here--'
                'Insert FD
                'If Not Base._FD_Voucher_DBOps.InsertFD(xfrm.InFD) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertFD = xfrm.InFD

                'insert FD history
                'If Not Base._FD_Voucher_DBOps.InsertFDHistory(xfrm.InFDHty) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InFDHistory = xfrm.InFDHty

                ''Renew FD History
                'If Not Base._FD_Voucher_DBOps.InsertFDHistory(xfrm.InRenFDHis) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InRenewFDHistory = xfrm.InRenFDHis

                'Close Prev FD
                'Dim ClsFd As Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD = New Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD()
                'ClsFd.FDCloseDate = xfrm.InFD.FDDate
                'ClsFd.FDStatus = xfrm.Status
                'ClsFd.Status_Action = Status_Action
                'ClsFd.RecID = Me.GLookUp_FD_List.Tag

                'If Not Base._FD_Voucher_DBOps.CloseFD(ClsFd) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If  ' Bug #4353 fix 
                '--End:FD Save shifted here--'

                'NEW FD
                Dim DrAmount As Double = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                If Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) > Val(Me.TXT_REC_AMOUNT.Text) Then DrAmount = Val(Me.TXT_REC_AMOUNT.Text)
                Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                InParams.ItemID = Me.GLookUp_ItemList.Tag
                InParams.Type = "DEBIT"
                InParams.Cr_Led_ID = ""
                InParams.Dr_Led_ID = Dr_Led_id
                InParams.SUB_Cr_Led_ID = ""
                InParams.SUB_Dr_Led_ID = ""
                InParams.Amount = DrAmount
                InParams.Mode = ""
                InParams.Ref_BANK_ID = ""
                InParams.Ref_Branch = ""
                InParams.Ref_No = ""
                InParams.Ref_Date = ""
                InParams.Ref_CDate = ""
                InParams.Narration = Me.Txt_Narration.Text
                InParams.Remarks = Me.Txt_Remarks.Text
                InParams.Reference = Me.Txt_Reference.Text
                InParams.FDID = FDRecID
                InParams.MasterTxnID = Me.xMID.Text
                InParams.Status_Action = Status_Action
                InParams.RecID = Me.xID.Text

                'If Not Base._FD_Voucher_DBOps.Insert(InParams) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertNewFD = InParams

                'old FD : fd close principal
                'Entry for Amount Received in Journal Col
                Dim CloseFDJournal As Double = Val(Me.Txt_Amount.Text)
                If Val(Me.TXT_REC_AMOUNT.Text) < CloseFDJournal Then CloseFDJournal = Val(Me.TXT_REC_AMOUNT.Text)
                If Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) < CloseFDJournal Then CloseFDJournal = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
                If CloseFDJournal < Val(Me.Txt_Amount.Text) And Val(Me.TXT_REC_AMOUNT.Text) <= Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) Then CloseFDJournal = Val(Me.Txt_Amount.Text)
                Dim InPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPms.TDate = Txt_V_Date.Text
                'InPms.TDate = Me.Txt_V_Date.Text.Trim()
                InPms.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                InPms.Type = "CREDIT"
                InPms.Cr_Led_ID = Cr_Led_id
                InPms.Dr_Led_ID = ""
                InPms.SUB_Cr_Led_ID = ""
                InPms.SUB_Dr_Led_ID = ""
                InPms.Amount = CloseFDJournal
                InPms.Mode = ""
                InPms.Ref_BANK_ID = ""
                InPms.Ref_Branch = ""
                InPms.Ref_No = ""
                InPms.Ref_Date = ""
                InPms.Ref_CDate = ""
                InPms.Narration = Me.Txt_Narration.Text
                InPms.Remarks = Me.Txt_Remarks.Text
                InPms.Reference = Me.Txt_Reference.Text
                InPms.FDID = Me.GLookUp_FD_List.Tag
                InPms.MasterTxnID = Me.xMID.Text
                InPms.Status_Action = Status_Action
                InPms.RecID = Guid.NewGuid.ToString()

                InNewParam.param_InCloseFDJournal = InPms
                'If Not Base._FD_Voucher_DBOps.Insert(InPms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                'Adjust Excess Amount received than renewal but less than Principal in Closure principle
                If Val(Me.TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) And Val(Me.TXT_REC_AMOUNT.Text) > Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                    Dim inParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    inParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    inParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then inParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else inParam.TDate = Txt_V_Date.Text
                    'inParam.TDate = Me.Txt_V_Date.Text.Trim()
                    inParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                    inParam.Type = "CREDIT"
                    inParam.Cr_Led_ID = Cr_Led_id
                    inParam.Dr_Led_ID = ""
                    inParam.SUB_Cr_Led_ID = ""
                    inParam.SUB_Dr_Led_ID = ""
                    inParam.Amount = Val(Me.Txt_Amount.Text) - Val(Me.TXT_REC_AMOUNT.Text)
                    inParam.Mode = ""
                    inParam.Ref_BANK_ID = ""
                    inParam.Ref_Branch = ""
                    inParam.Ref_No = ""
                    inParam.Ref_Date = ""
                    inParam.Ref_CDate = ""
                    inParam.Narration = Me.Txt_Narration.Text
                    inParam.Remarks = Me.Txt_Remarks.Text
                    inParam.Reference = Me.Txt_Reference.Text
                    inParam.FDID = Me.GLookUp_FD_List.Tag
                    inParam.MasterTxnID = Me.xMID.Text
                    inParam.Status_Action = Status_Action
                    inParam.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(inParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InAdjExcessAmtRec = inParam
                End If

                If Val(TXT_REC_AMOUNT.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) And Val(Txt_Amount.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                    'Balance Amount entry as per mode of Receipt
                    Dim BalanceReceived As Double = Val(TXT_REC_AMOUNT.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                    If Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) < Val(Txt_Amount.Text) And Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then BalanceReceived = Val(Txt_Amount.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                    If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                        Dim InPmts As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InPmts.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InPmts.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InPmts.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPmts.TDate = Txt_V_Date.Text
                        'InPmts.TDate = Me.Txt_V_Date.Text.Trim()
                        InPmts.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                        InPmts.Type = "CREDIT"
                        InPmts.Cr_Led_ID = Cr_Led_id
                        InPmts.Dr_Led_ID = "00079"
                        InPmts.SUB_Cr_Led_ID = ""
                        InPmts.SUB_Dr_Led_ID = Me.GLookUp_BankList.Tag
                        InPmts.Amount = BalanceReceived
                        InPmts.Mode = Me.Cmb_Rec_Mode.Text
                        InPmts.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                        InPmts.Ref_Branch = Me.Txt_Branch.Text
                        InPmts.Ref_No = Me.Txt_Ref_No.Text
                        If IsDate(Txt_Ref_Date.Text) Then InPmts.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPmts.Ref_Date = Txt_Ref_Date.Text
                        If IsDate(Txt_Ref_CDate.Text) Then InPmts.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InPmts.Ref_CDate = Txt_Ref_CDate.Text
                        'InPmts.Ref_Date = Me.Txt_Ref_Date.Text
                        'InPmts.Ref_CDate = Me.Txt_Ref_CDate.Text
                        InPmts.Narration = Me.Txt_Narration.Text
                        InPmts.Remarks = Me.Txt_Remarks.Text
                        InPmts.Reference = Me.Txt_Reference.Text
                        InPmts.FDID = Me.GLookUp_FD_List.Tag 'FDRecID Bug #5153
                        InPmts.MasterTxnID = Me.xMID.Text
                        InPmts.Status_Action = Status_Action
                        InPmts.RecID = Guid.NewGuid.ToString()

                        'If Not Base._FD_Voucher_DBOps.Insert(InPmts) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InNewParam.param_InBalRec_Notcash = InPmts
                    Else 'Cash Mode
                        Dim InPams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InPams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InPams.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InPams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPams.TDate = Txt_V_Date.Text
                        'InPams.TDate = Me.Txt_V_Date.Text.Trim()
                        InPams.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                        InPams.Type = "CREDIT"
                        InPams.Cr_Led_ID = Cr_Led_id
                        InPams.Dr_Led_ID = "00080"
                        InPams.SUB_Cr_Led_ID = ""
                        InPams.SUB_Dr_Led_ID = ""
                        InPams.Amount = BalanceReceived
                        InPams.Mode = Me.Cmb_Rec_Mode.Text
                        InPams.Ref_BANK_ID = ""
                        InPams.Ref_Branch = ""
                        InPams.Ref_No = ""
                        InPams.Ref_Date = ""
                        InPams.Ref_CDate = ""
                        InPams.Narration = Me.Txt_Narration.Text
                        InPams.Remarks = Me.Txt_Remarks.Text
                        InPams.Reference = Me.Txt_Reference.Text
                        InPams.FDID = Me.GLookUp_FD_List.Tag 'FDRecID Bug #5153
                        InPams.MasterTxnID = Me.xMID.Text
                        InPams.Status_Action = Status_Action
                        InPams.RecID = Guid.NewGuid.ToString()

                        InNewParam.param_InBalRec_Cash = InPams

                        'If Not Base._FD_Voucher_DBOps.Insert(InPams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                    End If
                End If

                'Interest : FD Interest Received  (Separate)
                Dim Journal_Interest As Double = 0
                Dim Credited_Interest As Double = 0
                Dim Payment As Double = 0
                If Val(TXT_REC_AMOUNT.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then ' interest is being used instead of renewal of same 
                    If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then
                        If Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) > Val(Txt_Amount.Text) Then
                            Journal_Interest = Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) - Val(Txt_Amount.Text)
                            Credited_Interest = Val(TXT_REC_AMOUNT.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                        Else
                            Credited_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text) '+Amt Red = principle - renewal
                        End If
                        'Else + amt recd = Recd-renewal
                    End If
                ElseIf Val(TXT_REC_AMOUNT.Text) < Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then ' interest is being credited to bank account
                    Payment = Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) - Val(TXT_REC_AMOUNT.Text)
                    If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then Journal_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text)
                Else
                    If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then Journal_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text)
                End If

                If Journal_Interest > 0 Then
                    Dim inPams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    inPams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    inPams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then inPams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else inPams.TDate = Txt_V_Date.Text
                    'inPams.TDate = Me.Txt_V_Date.Text.Trim()
                    inPams.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                    inPams.Type = "CREDIT"
                    inPams.Cr_Led_ID = "00069"
                    inPams.Dr_Led_ID = ""
                    inPams.SUB_Cr_Led_ID = ""
                    inPams.SUB_Dr_Led_ID = ""
                    inPams.Amount = Journal_Interest
                    inPams.Mode = ""
                    inPams.Ref_BANK_ID = ""
                    inPams.Ref_Branch = ""
                    inPams.Ref_No = ""
                    inPams.Ref_Date = ""
                    inPams.Ref_CDate = ""
                    inPams.Narration = Me.Txt_Narration.Text
                    inPams.Remarks = Me.Txt_Remarks.Text
                    inPams.Reference = Me.Txt_Reference.Text
                    inPams.FDID = Me.GLookUp_FD_List.Tag
                    inPams.MasterTxnID = Me.xMID.Text
                    inPams.Status_Action = Status_Action
                    inPams.RecID = Guid.NewGuid.ToString()

                    InNewParam.param_InJournalInterest = inPams
                    'If Not Base._FD_Voucher_DBOps.Insert(inPams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                    '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                End If

                If Credited_Interest > 0 Then
                    If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                        Dim InParameter As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InParameter.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InParameter.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InParameter.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParameter.TDate = Txt_V_Date.Text
                        'InParameter.TDate = Me.Txt_V_Date.Text.Trim()
                        InParameter.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                        InParameter.Type = "CREDIT"
                        InParameter.Cr_Led_ID = "00069"
                        InParameter.Dr_Led_ID = "00079"
                        InParameter.SUB_Cr_Led_ID = ""
                        InParameter.SUB_Dr_Led_ID = Me.GLookUp_BankList.Tag
                        InParameter.Amount = Credited_Interest
                        InParameter.Mode = Me.Cmb_Rec_Mode.Text
                        InParameter.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                        InParameter.Ref_Branch = Me.Txt_Branch.Text
                        InParameter.Ref_No = Me.Txt_Ref_No.Text
                        If IsDate(Txt_Ref_Date.Text) Then InParameter.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParameter.Ref_Date = Txt_Ref_Date.Text
                        If IsDate(Txt_Ref_CDate.Text) Then InParameter.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParameter.Ref_CDate = Txt_Ref_CDate.Text
                        'InParameter.Ref_Date = Me.Txt_Ref_Date.Text
                        'InParameter.Ref_CDate = Me.Txt_Ref_CDate.Text
                        InParameter.Narration = Me.Txt_Narration.Text
                        InParameter.Remarks = Me.Txt_Remarks.Text
                        InParameter.Reference = Me.Txt_Reference.Text
                        InParameter.FDID = Me.GLookUp_FD_List.Tag
                        InParameter.MasterTxnID = Me.xMID.Text
                        InParameter.Status_Action = Status_Action
                        InParameter.RecID = Guid.NewGuid.ToString()

                        'If Not Base._FD_Voucher_DBOps.Insert(InParameter) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InNewParam.param_InCreditedInterest_Notcash = InParameter
                    Else
                        Dim InParameters As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InParameters.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InParameters.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InParameters.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParameters.TDate = Txt_V_Date.Text
                        'InParameters.TDate = Me.Txt_V_Date.Text.Trim()
                        InParameters.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                        InParameters.Type = "CREDIT"
                        InParameters.Cr_Led_ID = "00069"
                        InParameters.Dr_Led_ID = "00080"
                        InParameters.SUB_Cr_Led_ID = ""
                        InParameters.SUB_Dr_Led_ID = ""
                        InParameters.Amount = Credited_Interest
                        InParameters.Mode = Me.Cmb_Rec_Mode.Text
                        InParameters.Ref_BANK_ID = ""
                        InParameters.Ref_Branch = ""
                        InParameters.Ref_No = ""
                        InParameters.Ref_Date = ""
                        InParameters.Ref_CDate = ""
                        InParameters.Narration = Me.Txt_Narration.Text
                        InParameters.Remarks = Me.Txt_Remarks.Text
                        InParameters.Reference = Me.Txt_Reference.Text
                        InParameters.FDID = Me.GLookUp_FD_List.Tag
                        InParameters.MasterTxnID = Me.xMID.Text
                        InParameters.Status_Action = Status_Action
                        InParameters.RecID = Guid.NewGuid.ToString()

                        InNewParam.param_InCreditedInterest_Cash = InParameters

                        'If Not Base._FD_Voucher_DBOps.Insert(InParameters) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                    End If
                End If

                If Payment > 0 Then
                    If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                        Dim InPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InPrms.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.TDate = Txt_V_Date.Text
                        'InPrms.TDate = Me.Txt_V_Date.Text.Trim()
                        InPrms.ItemID = Me.GLookUp_ItemList.Tag
                        InPrms.Type = "DEBIT"
                        InPrms.Cr_Led_ID = "00079"
                        InPrms.Dr_Led_ID = Dr_Led_id
                        InPrms.SUB_Cr_Led_ID = Me.GLookUp_BankList.Tag
                        InPrms.SUB_Dr_Led_ID = ""
                        InPrms.Amount = Payment
                        InPrms.Mode = Me.Cmb_Rec_Mode.Text
                        InPrms.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                        InPrms.Ref_Branch = Me.Txt_Branch.Text
                        InPrms.Ref_No = Me.Txt_Ref_No.Text
                        If IsDate(Txt_Ref_Date.Text) Then InPrms.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_Date = Txt_Ref_Date.Text
                        If IsDate(Txt_Ref_CDate.Text) Then InPrms.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_CDate = Txt_Ref_CDate.Text
                        'InPrms.Ref_Date = Me.Txt_Ref_Date.Text
                        'InPrms.Ref_CDate = Me.Txt_Ref_CDate.Text
                        InPrms.Narration = Me.Txt_Narration.Text
                        InPrms.Remarks = Me.Txt_Remarks.Text
                        InPrms.Reference = Me.Txt_Reference.Text
                        InPrms.FDID = FDRecID
                        InPrms.MasterTxnID = Me.xMID.Text
                        InPrms.Status_Action = Status_Action
                        InPrms.RecID = Guid.NewGuid.ToString()

                        InNewParam.param_InPmt_NotCash = InPrms
                        'If Not Base._FD_Voucher_DBOps.Insert(InPrms) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                    Else 'Cash Mode
                        Dim InParm As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                        InParm.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                        InParm.VNo = Me.Txt_V_NO.Text
                        If IsDate(Txt_V_Date.Text) Then InParm.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParm.TDate = Txt_V_Date.Text
                        'InParm.TDate = Me.Txt_V_Date.Text.Trim()
                        InParm.ItemID = Me.GLookUp_ItemList.Tag
                        InParm.Type = "DEBIT"
                        InParm.Cr_Led_ID = "00080"
                        InParm.Dr_Led_ID = Dr_Led_id
                        InParm.SUB_Cr_Led_ID = ""
                        InParm.SUB_Dr_Led_ID = ""
                        InParm.Amount = Payment
                        InParm.Mode = Me.Cmb_Rec_Mode.Text
                        InParm.Ref_BANK_ID = ""
                        InParm.Ref_Branch = ""
                        InParm.Ref_No = ""
                        InParm.Ref_Date = ""
                        InParm.Ref_CDate = ""
                        InParm.Narration = Me.Txt_Narration.Text
                        InParm.Remarks = Me.Txt_Remarks.Text
                        InParm.Reference = Me.Txt_Reference.Text
                        InParm.FDID = FDRecID
                        InParm.MasterTxnID = Me.xMID.Text
                        InParm.Status_Action = Status_Action
                        InParm.RecID = Guid.NewGuid.ToString()

                        InNewParam.param_InPmt_Cash = InParm
                        'If Not Base._FD_Voucher_DBOps.Insert(InParm) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                    End If
                End If

                Dim InterestOverhead As Double = 0
                If Val(TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) Then
                    InterestOverhead = Val(Me.Txt_Amount.Text) - Val(TXT_REC_AMOUNT.Text)
                End If

                If InterestOverhead > 0 Then 'Excess Interest recovered by bank
                    Dim BankCharges As DataTable = Base._FD_Voucher_DBOps.GetBankChargesItemDetail()
                    Dim IntParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    IntParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    IntParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then IntParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IntParam.TDate = Txt_V_Date.Text
                    'IntParam.TDate = Me.Txt_V_Date.Text.Trim()
                    IntParam.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4"
                    IntParam.Type = BankCharges.Rows(0)("ITEM_TRANS_TYPE").ToString
                    IntParam.Cr_Led_ID = ""
                    IntParam.Dr_Led_ID = BankCharges.Rows(0)("ITEM_LED_ID").ToString
                    IntParam.SUB_Cr_Led_ID = ""
                    IntParam.SUB_Dr_Led_ID = ""
                    IntParam.Amount = InterestOverhead
                    IntParam.Mode = ""
                    IntParam.Ref_BANK_ID = ""
                    IntParam.Ref_Branch = ""
                    IntParam.Ref_No = ""
                    IntParam.Ref_Date = ""
                    IntParam.Ref_CDate = ""
                    IntParam.Narration = Me.Txt_Narration.Text
                    IntParam.Remarks = Me.Txt_Remarks.Text
                    IntParam.Reference = Me.Txt_Reference.Text
                    IntParam.FDID = Me.GLookUp_FD_List.Tag
                    IntParam.MasterTxnID = Me.xMID.Text
                    IntParam.Status_Action = Status_Action
                    IntParam.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(IntParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                    '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InInterestOverhead = IntParam
                End If

                If Val(Val(TXT_TDS.Text) > 0) Then 'TDS DEDUCTED 
                    Dim IntParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    IntParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    IntParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then IntParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IntParams.TDate = Txt_V_Date.Text
                    'IntParams.TDate = Me.Txt_V_Date.Text.Trim()
                    IntParams.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                    IntParams.Type = "DEBIT"
                    IntParams.Cr_Led_ID = ""
                    IntParams.Dr_Led_ID = "00008"
                    IntParams.SUB_Cr_Led_ID = ""
                    IntParams.SUB_Dr_Led_ID = ""
                    IntParams.Amount = Val(TXT_TDS.Text)
                    IntParams.Mode = ""
                    IntParams.Ref_BANK_ID = ""
                    IntParams.Ref_Branch = ""
                    IntParams.Ref_No = ""
                    IntParams.Ref_Date = ""
                    IntParams.Ref_CDate = ""
                    IntParams.Narration = Me.Txt_Narration.Text
                    IntParams.Remarks = Me.Txt_Remarks.Text
                    IntParams.Reference = Me.Txt_Reference.Text
                    IntParams.FDID = Me.GLookUp_FD_List.Tag
                    IntParams.MasterTxnID = Me.xMID.Text
                    IntParams.Status_Action = Status_Action
                    IntParams.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(IntParams) Then 'InTDS_Deducted1- txn scope
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                    '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InTDS_Deducted1 = IntParams

                    Dim InsParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    InsParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    InsParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InsParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsParam.TDate = Txt_V_Date.Text
                    'InsParam.TDate = Me.Txt_V_Date.Text.Trim()
                    InsParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                    InsParam.Type = "CREDIT"
                    InsParam.Cr_Led_ID = "00069"
                    InsParam.Dr_Led_ID = ""
                    InsParam.SUB_Cr_Led_ID = ""
                    InsParam.SUB_Dr_Led_ID = ""
                    InsParam.Amount = Val(TXT_TDS.Text)
                    InsParam.Mode = ""
                    InsParam.Ref_BANK_ID = ""
                    InsParam.Ref_Branch = ""
                    InsParam.Ref_No = ""
                    InsParam.Ref_Date = ""
                    InsParam.Ref_CDate = ""
                    InsParam.Narration = Me.Txt_Narration.Text
                    InsParam.Remarks = Me.Txt_Remarks.Text
                    InsParam.Reference = Me.Txt_Reference.Text
                    InsParam.FDID = Me.GLookUp_FD_List.Tag
                    InsParam.MasterTxnID = Me.xMID.Text
                    InsParam.Status_Action = Status_Action
                    InsParam.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(InsParam) Then 'InTDS_Deducted2 - txn scope
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                    '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InTDS_Deducted2 = InsParam
                End If
            Catch ex As Exception
                'delete Entered FD Entry in case of exception in entering TXN
                Base._FD_Voucher_DBOps.DeleteFDHistory(Me.xMID.Text)
                Base._FD_Voucher_DBOps.DeleteFD(Me.xMID.Text)
                DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try

            If Not Base._FD_Voucher_DBOps.InsertRenewFD_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_RenewFD_UpdateVoucherFD = New Common_Lib.RealTimeService.Param_Txn_RenewFD_UpdateVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim FDRecID As String = ""
            Dim xfrm As New Frm_FD_Window
            xfrm.Look_BankList.EditValue = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BA_ID").ToString
            xfrm.Look_BankList.Properties.ReadOnly = True
            xfrm.Lbl_Branch.Text = "Branch: " & Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_BRANCH").ToString
            xfrm.Lbl_AccountNo.Text = "Customer No.: " & Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_CUST_NO").ToString
            xfrm.iRenewFDNo = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_NO").ToString
            xfrm.Look_BankList.Tag = xfrm.Look_BankList.EditValue
            xfrm.Look_BankList.Properties.Tag = "SHOW"
            xfrm.Txt_Date.DateTime = TXT_NRC_DATE.DateTime
            xfrm.MatDate = Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString)
            xfrm.Txt_Date.Properties.ReadOnly = True
            xfrm.Txt_Amount.EditValue = TXT_RENEWAL_MATURITY_AMOUNT.Text
            xfrm.Txt_Amount.Properties.ReadOnly = True
            xfrm.iRenewFrom = Me.GLookUp_FD_List.Tag
            xfrm.Tag = Me.Tag
            xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD
            If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) <= TXT_NRC_DATE.DateTime Then
                xfrm.Status = Common_Lib.Common.FDStatus.Matured_Renewed_FD.ToString
            Else
                xfrm.Status = Common_Lib.Common.FDStatus.Premature_Renewed_FD.ToString
            End If
            'xfrm.Txt_Date.Text = TXT_NRC_DATE.Text
            xfrm.TxnID = Me.xMID.Text
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                FDRecID = xfrm.xID.Text
                xfrm.Dispose()
            Else
                xfrm.Dispose()
                Exit Sub
            End If

            If Base.AllowMultiuser() Then
                Dim MaxValue As Object = 0
                MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'Closed Bank Acc Check #g36
                If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                    If IsDBNull(AccNo) Then AccNo = ""
                    If AccNo.Length > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t e d . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user.... ", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If

                Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text)
                If IsDate(CloseDate) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                Dim oldEditOn As DateTime
                'Bank Account dependency check #Ref G36
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim BankAcc As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If BankAcc.Rows.Count <= 0 Then 'A/D, E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = BankAcc.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

                If Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, FDRecID, 1) > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Interest / TDS Posted against Current FD.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

            End If

            'Dim xDate As DateTime = Nothing : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
            'Dim R_Date As DateTime = Nothing : If IsDate(Me.Txt_Ref_Date.Text) Then : R_Date = New Date(Me.Txt_Ref_Date.DateTime.Year, Me.Txt_Ref_Date.DateTime.Month, Me.Txt_Ref_Date.DateTime.Day) : Me.Txt_Ref_Date.Tag = "#" & R_Date.ToString(Base._Date_Format_Short) & "#" : Else : Me.Txt_Ref_Date.Tag = " NULL " : End If
            'Dim C_Date As DateTime = Nothing : If IsDate(Me.Txt_Ref_CDate.Text) Then : C_Date = New Date(Me.Txt_Ref_CDate.DateTime.Year, Me.Txt_Ref_CDate.DateTime.Month, Me.Txt_Ref_CDate.DateTime.Day) : Me.Txt_Ref_CDate.Tag = "#" & C_Date.ToString(Base._Date_Format_Short) & "#" : Else : Me.Txt_Ref_CDate.Tag = " NULL " : End If
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID ' NEW FD
                Cr_Led_id = iLed_ID 'OLD FD
            Else
                Cr_Led_id = iLed_ID
                Dr_Led_id = iLed_ID
            End If
            'Try
            'Master Record 
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD()
            UpMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            'UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.SubTotal = Val(TXT_REC_AMOUNT.Text)
            UpMInfo.Cash = 0
            UpMInfo.Bank = 0
            UpMInfo.TDS = Val(TXT_TDS.Text)
            UpMInfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.UpdateMasterInfo(UpMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMInfo

            ''---start: Fd updation shifted from frm_fd_winfow--''
            'update FD
            'If Not Base._FD_Voucher_DBOps.UpdateFD(xfrm.UpFD) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateFD = xfrm.UpFD
            ''FD hist
            'If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpFDHis) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpFDHistory = xfrm.UpFDHis
            ''RenFD History
            'If Not Base._FD_Voucher_DBOps.UpdateFDHistory(xfrm.UpRenFDHty) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpRenFDHistory = xfrm.UpRenFDHty

            'Close Prev FD
            'Dim ClsFd As Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD = New Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD()
            'ClsFd.FDCloseDate = xfrm.UpFD.FDDate
            'ClsFd.FDStatus = xfrm.Status
            'ClsFd.Status_Action = Status_Action
            'ClsFd.RecID = Me.GLookUp_FD_List.Tag

            'If Not Base._FD_Voucher_DBOps.CloseFD(ClsFd) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If ' Bug #4353 fix 
            ''---end: Fd updation shifted from frm_fd_winfow--''

            'DELETE TRANSACTIONS 
            'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteTxns = Me.xMID.Text

            Me.xID.Text = Guid.NewGuid.ToString

            'NEW FD
            Dim DrAmount As Double = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
            If Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) > Val(Me.TXT_REC_AMOUNT.Text) Then DrAmount = Val(Me.TXT_REC_AMOUNT.Text)
            Dim InsPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
            InsPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InsPms.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InsPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsPms.TDate = Txt_V_Date.Text
            'InsPms.TDate = Me.Txt_V_Date.Text.Trim()
            InsPms.ItemID = Me.GLookUp_ItemList.Tag
            InsPms.Type = "DEBIT"
            InsPms.Cr_Led_ID = ""
            InsPms.Dr_Led_ID = Dr_Led_id
            InsPms.SUB_Cr_Led_ID = ""
            InsPms.SUB_Dr_Led_ID = ""
            InsPms.Amount = DrAmount
            InsPms.Mode = ""
            InsPms.Ref_BANK_ID = ""
            InsPms.Ref_Branch = ""
            InsPms.Ref_No = ""
            InsPms.Ref_Date = ""
            InsPms.Ref_CDate = ""
            InsPms.Narration = Me.Txt_Narration.Text
            InsPms.Remarks = Me.Txt_Remarks.Text
            InsPms.Reference = Me.Txt_Reference.Text
            InsPms.FDID = FDRecID
            InsPms.MasterTxnID = Me.xMID.Text
            InsPms.Status_Action = Status_Action
            InsPms.RecID = Me.xID.Text

            'If Not Base._FD_Voucher_DBOps.Insert(InsPms) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_InsertNewFD = InsPms

            'old FD : fd close principal
            'Entry for Amount Received in Journal Col
            Dim CloseFDJournal As Double = Val(Me.Txt_Amount.Text)
            If Val(Me.TXT_REC_AMOUNT.Text) < CloseFDJournal Then CloseFDJournal = Val(Me.TXT_REC_AMOUNT.Text)
            If Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) < CloseFDJournal Then CloseFDJournal = Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text)
            If CloseFDJournal < Val(Me.Txt_Amount.Text) And Val(Me.TXT_REC_AMOUNT.Text) <= Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) Then CloseFDJournal = Val(Me.Txt_Amount.Text)
            Dim InsPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
            InsPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InsPrms.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InsPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsPrms.TDate = Txt_V_Date.Text
            'InsPrms.TDate = Me.Txt_V_Date.Text.Trim()
            InsPrms.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
            InsPrms.Type = "CREDIT"
            InsPrms.Cr_Led_ID = Cr_Led_id
            InsPrms.Dr_Led_ID = ""
            InsPrms.SUB_Cr_Led_ID = ""
            InsPrms.SUB_Dr_Led_ID = ""
            InsPrms.Amount = CloseFDJournal
            InsPrms.Mode = ""
            InsPrms.Ref_BANK_ID = ""
            InsPrms.Ref_Branch = ""
            InsPrms.Ref_No = ""
            InsPrms.Ref_Date = ""
            InsPrms.Ref_CDate = ""
            InsPrms.Narration = Me.Txt_Narration.Text
            InsPrms.Remarks = Me.Txt_Remarks.Text
            InsPrms.Reference = Me.Txt_Reference.Text
            InsPrms.FDID = Me.GLookUp_FD_List.Tag
            InsPrms.MasterTxnID = Me.xMID.Text
            InsPrms.Status_Action = Status_Action
            InsPrms.RecID = Guid.NewGuid.ToString()

            'If Not Base._FD_Voucher_DBOps.Insert(InsPrms) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_InCloseFDJournal = InsPrms

            'Adjust Excess Amount received than renewal but less than Principal in Closure principle
            If Val(Me.TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) And Val(Me.TXT_REC_AMOUNT.Text) > Val(Me.TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                Dim InstParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InstParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InstParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InstParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InstParam.TDate = Txt_V_Date.Text
                'InstParam.TDate = Me.Txt_V_Date.Text.Trim()
                InstParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                InstParam.Type = "CREDIT"
                InstParam.Cr_Led_ID = Cr_Led_id
                InstParam.Dr_Led_ID = ""
                InstParam.SUB_Cr_Led_ID = ""
                InstParam.SUB_Dr_Led_ID = ""
                InstParam.Amount = Val(Me.Txt_Amount.Text) - Val(Me.TXT_REC_AMOUNT.Text)
                InstParam.Mode = ""
                InstParam.Ref_BANK_ID = ""
                InstParam.Ref_Branch = ""
                InstParam.Ref_No = ""
                InstParam.Ref_Date = ""
                InstParam.Ref_CDate = ""
                InstParam.Narration = Me.Txt_Narration.Text
                InstParam.Remarks = Me.Txt_Remarks.Text
                InstParam.Reference = Me.Txt_Reference.Text
                InstParam.FDID = Me.GLookUp_FD_List.Tag
                InstParam.MasterTxnID = Me.xMID.Text
                InstParam.Status_Action = Status_Action
                InstParam.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InstParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InAdjExcessAmtRec = InstParam
            End If

            If Val(TXT_REC_AMOUNT.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) And Val(Txt_Amount.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then
                'Balance Amount entry as per mode of Receipt
                Dim BalanceReceived As Double = Val(TXT_REC_AMOUNT.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                If Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) < Val(Txt_Amount.Text) And Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then BalanceReceived = Val(Txt_Amount.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                    Dim IParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    IParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    IParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then IParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IParam.TDate = Txt_V_Date.Text
                    'IParam.TDate = Me.Txt_V_Date.Text.Trim()
                    IParam.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                    IParam.Type = "CREDIT"
                    IParam.Cr_Led_ID = Cr_Led_id
                    IParam.Dr_Led_ID = "00079"
                    IParam.SUB_Cr_Led_ID = ""
                    IParam.SUB_Dr_Led_ID = Me.GLookUp_BankList.Tag
                    IParam.Amount = BalanceReceived
                    IParam.Mode = Me.Cmb_Rec_Mode.Text
                    IParam.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                    IParam.Ref_Branch = Me.Txt_Branch.Text
                    IParam.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then IParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else IParam.Ref_Date = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then IParam.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else IParam.Ref_CDate = Txt_Ref_CDate.Text
                    'IParam.Ref_Date = Me.Txt_Ref_Date.Text
                    'IParam.Ref_CDate = Me.Txt_Ref_CDate.Text
                    IParam.Narration = Me.Txt_Narration.Text
                    IParam.Remarks = Me.Txt_Remarks.Text
                    IParam.Reference = Me.Txt_Reference.Text
                    IParam.FDID = Me.GLookUp_FD_List.Tag 'FDRecID Bug #5153
                    IParam.MasterTxnID = Me.xMID.Text
                    IParam.Status_Action = Status_Action
                    IParam.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(IParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InBalRec_Notcash = IParam
                Else 'Cash Mode
                    Dim IParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    IParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    IParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then IParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IParams.TDate = Txt_V_Date.Text
                    'IParams.TDate = Me.Txt_V_Date.Text.Trim()
                    IParams.ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                    IParams.Type = "CREDIT"
                    IParams.Cr_Led_ID = Cr_Led_id
                    IParams.Dr_Led_ID = "00080"
                    IParams.SUB_Cr_Led_ID = ""
                    IParams.SUB_Dr_Led_ID = ""
                    IParams.Amount = BalanceReceived
                    IParams.Mode = Me.Cmb_Rec_Mode.Text
                    IParams.Ref_BANK_ID = ""
                    IParams.Ref_Branch = ""
                    IParams.Ref_No = ""
                    IParams.Ref_Date = ""
                    IParams.Ref_CDate = ""
                    IParams.Narration = Me.Txt_Narration.Text
                    IParams.Remarks = Me.Txt_Remarks.Text
                    IParams.Reference = Me.Txt_Reference.Text
                    IParams.FDID = Me.GLookUp_FD_List.Tag 'FDRecID Bug #5153
                    IParams.MasterTxnID = Me.xMID.Text
                    IParams.Status_Action = Status_Action
                    IParams.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(IParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InBalRec_Cash = IParams
                End If
            End If

            'Interest : FD Interest Received  (Separate)
            Dim Journal_Interest As Double = 0
            Dim Credited_Interest As Double = 0
            Dim Payment As Double = 0
            If Val(TXT_REC_AMOUNT.Text) > Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then ' interest is being used instead of renewal of same 
                If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then
                    If Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) > Val(Txt_Amount.Text) Then
                        Journal_Interest = Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) - Val(Txt_Amount.Text)
                        Credited_Interest = Val(TXT_REC_AMOUNT.Text) - Val(TXT_RENEWAL_MATURITY_AMOUNT.Text)
                    Else
                        Credited_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text) '+Amt Red = principle - renewal
                    End If
                    'Else + amt recd = Recd-renewal
                End If
            ElseIf Val(TXT_REC_AMOUNT.Text) < Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) Then ' interest is being credited to bank account
                Payment = Val(TXT_RENEWAL_MATURITY_AMOUNT.Text) - Val(TXT_REC_AMOUNT.Text)
                If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then Journal_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text)
            Else
                If Val(TXT_REC_AMOUNT.Text) > Val(Txt_Amount.Text) Then Journal_Interest = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text)
            End If

            If Journal_Interest > 0 Then
                Dim IPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                IPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                IPrms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then IPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IPrms.TDate = Txt_V_Date.Text
                'IPrms.TDate = Me.Txt_V_Date.Text.Trim()
                IPrms.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                IPrms.Type = "CREDIT"
                IPrms.Cr_Led_ID = "00069"
                IPrms.Dr_Led_ID = ""
                IPrms.SUB_Cr_Led_ID = ""
                IPrms.SUB_Dr_Led_ID = ""
                IPrms.Amount = Journal_Interest
                IPrms.Mode = ""
                IPrms.Ref_BANK_ID = ""
                IPrms.Ref_Branch = ""
                IPrms.Ref_No = ""
                IPrms.Ref_Date = ""
                IPrms.Ref_CDate = ""
                IPrms.Narration = Me.Txt_Narration.Text
                IPrms.Remarks = Me.Txt_Remarks.Text
                IPrms.Reference = Me.Txt_Reference.Text
                IPrms.FDID = Me.GLookUp_FD_List.Tag
                IPrms.MasterTxnID = Me.xMID.Text
                IPrms.Status_Action = Status_Action
                IPrms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(IPrms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InJournalInterest = IPrms
            End If

            If Credited_Interest > 0 Then
                If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    InParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                    'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                    InParam.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                    InParam.Type = "CREDIT"
                    InParam.Cr_Led_ID = "00069"
                    InParam.Dr_Led_ID = "00079"
                    InParam.SUB_Cr_Led_ID = ""
                    InParam.SUB_Dr_Led_ID = Me.GLookUp_BankList.Tag
                    InParam.Amount = Credited_Interest
                    InParam.Mode = Me.Cmb_Rec_Mode.Text
                    InParam.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                    InParam.Ref_Branch = Me.Txt_Branch.Text
                    InParam.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_CDate = Txt_Ref_CDate.Text
                    'InParam.Ref_Date = Me.Txt_Ref_Date.Text
                    'InParam.Ref_CDate = Me.Txt_Ref_CDate.Text
                    InParam.Narration = Me.Txt_Narration.Text
                    InParam.Remarks = Me.Txt_Remarks.Text
                    InParam.Reference = Me.Txt_Reference.Text
                    InParam.FDID = Me.GLookUp_FD_List.Tag
                    InParam.MasterTxnID = Me.xMID.Text
                    InParam.Status_Action = Status_Action
                    InParam.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InCreditedInterest_Notcash = InParam
                Else
                    Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    InParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                    'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                    InParams.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                    InParams.Type = "CREDIT"
                    InParams.Cr_Led_ID = "00069"
                    InParams.Dr_Led_ID = "00080"
                    InParams.SUB_Cr_Led_ID = ""
                    InParams.SUB_Dr_Led_ID = ""
                    InParams.Amount = Credited_Interest
                    InParams.Mode = Me.Cmb_Rec_Mode.Text
                    InParams.Ref_BANK_ID = ""
                    InParams.Ref_Branch = ""
                    InParams.Ref_No = ""
                    InParams.Ref_Date = ""
                    InParams.Ref_CDate = ""
                    InParams.Narration = Me.Txt_Narration.Text
                    InParams.Remarks = Me.Txt_Remarks.Text
                    InParams.Reference = Me.Txt_Reference.Text
                    InParams.FDID = Me.GLookUp_FD_List.Tag
                    InParams.MasterTxnID = Me.xMID.Text
                    InParams.Status_Action = Status_Action
                    InParams.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(InParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InCreditedInterest_Cash = InParams
                End If
            End If

            If Payment > 0 Then
                If Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                    Dim InPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    InPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    InPrms.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.TDate = Txt_V_Date.Text
                    'InPrms.TDate = Me.Txt_V_Date.Text.Trim()
                    InPrms.ItemID = Me.GLookUp_ItemList.Tag
                    InPrms.Type = "DEBIT"
                    InPrms.Cr_Led_ID = "00079"
                    InPrms.Dr_Led_ID = Dr_Led_id
                    InPrms.SUB_Cr_Led_ID = Me.GLookUp_BankList.Tag
                    InPrms.SUB_Dr_Led_ID = ""
                    InPrms.Amount = Payment
                    InPrms.Mode = Me.Cmb_Rec_Mode.Text
                    InPrms.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                    InPrms.Ref_Branch = Me.Txt_Branch.Text
                    InPrms.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InPrms.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_Date = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InPrms.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_CDate = Txt_Ref_CDate.Text
                    'InPrms.Ref_Date = Me.Txt_Ref_Date.Text
                    'InPrms.Ref_CDate = Me.Txt_Ref_CDate.Text
                    InPrms.Narration = Me.Txt_Narration.Text
                    InPrms.Remarks = Me.Txt_Remarks.Text
                    InPrms.Reference = Me.Txt_Reference.Text
                    InPrms.FDID = FDRecID
                    InPrms.MasterTxnID = Me.xMID.Text
                    InPrms.Status_Action = Status_Action
                    InPrms.RecID = Guid.NewGuid.ToString()
                    'If Not Base._FD_Voucher_DBOps.Insert(InPrms) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InPmt_NotCash = InPrms
                Else 'Cash Mode
                    Dim InPrmts As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                    InPrmts.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                    InPrmts.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InPrmts.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrmts.TDate = Txt_V_Date.Text
                    'InPrmts.TDate = Me.Txt_V_Date.Text.Trim()
                    InPrmts.ItemID = Me.GLookUp_ItemList.Tag
                    InPrmts.Type = "DEBIT"
                    InPrmts.Cr_Led_ID = "00080"
                    InPrmts.Dr_Led_ID = Dr_Led_id
                    InPrmts.SUB_Cr_Led_ID = ""
                    InPrmts.SUB_Dr_Led_ID = ""
                    InPrmts.Amount = Payment
                    InPrmts.Mode = Me.Cmb_Rec_Mode.Text
                    InPrmts.Ref_BANK_ID = ""
                    InPrmts.Ref_Branch = ""
                    InPrmts.Ref_No = ""
                    InPrmts.Ref_Date = ""
                    InPrmts.Ref_CDate = ""
                    InPrmts.Narration = Me.Txt_Narration.Text
                    InPrmts.Remarks = Me.Txt_Remarks.Text
                    InPrmts.Reference = Me.Txt_Reference.Text
                    InPrmts.FDID = FDRecID
                    InPrmts.MasterTxnID = Me.xMID.Text
                    InPrmts.Status_Action = Status_Action
                    InPrmts.RecID = Guid.NewGuid.ToString()

                    'If Not Base._FD_Voucher_DBOps.Insert(InPrmts) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InPmt_Cash = InPrmts
                End If
            End If

            Dim InterestOverhead As Double = 0
            If Val(TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) Then
                InterestOverhead = Val(Me.Txt_Amount.Text) - Val(TXT_REC_AMOUNT.Text)
            End If

            If InterestOverhead > 0 Then 'Excess Interest recovered by bank
                Dim BankCharges As DataTable = Base._FD_Voucher_DBOps.GetBankChargesItemDetail()
                Dim InPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPms.TDate = Txt_V_Date.Text
                'InPms.TDate = Me.Txt_V_Date.Text.Trim()
                InPms.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4"
                InPms.Type = BankCharges.Rows(0)("ITEM_TRANS_TYPE").ToString
                InPms.Cr_Led_ID = ""
                InPms.Dr_Led_ID = BankCharges.Rows(0)("ITEM_LED_ID").ToString
                InPms.SUB_Cr_Led_ID = ""
                InPms.SUB_Dr_Led_ID = ""
                InPms.Amount = InterestOverhead
                InPms.Mode = ""
                InPms.Ref_BANK_ID = ""
                InPms.Ref_Branch = ""
                InPms.Ref_No = ""
                InPms.Ref_Date = ""
                InPms.Ref_CDate = ""
                InPms.Narration = Me.Txt_Narration.Text
                InPms.Remarks = Me.Txt_Remarks.Text
                InPms.Reference = Me.Txt_Reference.Text
                InPms.FDID = Me.GLookUp_FD_List.Tag
                InPms.MasterTxnID = Me.xMID.Text
                InPms.Status_Action = Status_Action
                InPms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InPms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    'Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    'Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InInterestOverhead = InPms
            End If

            If Val(Val(TXT_TDS.Text) > 0) Then 'TDS DEDUCTED 
                Dim InParameter As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParameter.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParameter.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParameter.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParameter.TDate = Txt_V_Date.Text
                'InParameter.TDate = Me.Txt_V_Date.Text.Trim()
                InParameter.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                InParameter.Type = "DEBIT"
                InParameter.Cr_Led_ID = ""
                InParameter.Dr_Led_ID = "00008"
                InParameter.SUB_Cr_Led_ID = ""
                InParameter.SUB_Dr_Led_ID = ""
                InParameter.Amount = Val(TXT_TDS.Text)
                InParameter.Mode = ""
                InParameter.Ref_BANK_ID = ""
                InParameter.Ref_Branch = ""
                InParameter.Ref_No = ""
                InParameter.Ref_Date = ""
                InParameter.Ref_CDate = ""
                InParameter.Narration = Me.Txt_Narration.Text
                InParameter.Remarks = Me.Txt_Remarks.Text
                InParameter.Reference = Me.Txt_Reference.Text
                InParameter.FDID = Me.GLookUp_FD_List.Tag
                InParameter.MasterTxnID = Me.xMID.Text
                InParameter.Status_Action = Status_Action
                InParameter.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParameter) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InTDS_Deducted1 = InParameter

                Dim InParameters As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParameters.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParameters.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParameters.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParameters.TDate = Txt_V_Date.Text
                'InParameters.TDate = Me.Txt_V_Date.Text.Trim()
                InParameters.ItemID = "c92da5ab-082d-45d9-b6b7-78752625c715"
                InParameters.Type = "CREDIT"
                InParameters.Cr_Led_ID = "00069"
                InParameters.Dr_Led_ID = ""
                InParameters.SUB_Cr_Led_ID = ""
                InParameters.SUB_Dr_Led_ID = ""
                InParameters.Amount = Val(TXT_TDS.Text)
                InParameters.Mode = ""
                InParameters.Ref_BANK_ID = ""
                InParameters.Ref_Branch = ""
                InParameters.Ref_No = ""
                InParameters.Ref_Date = ""
                InParameters.Ref_CDate = ""
                InParameters.Narration = Me.Txt_Narration.Text
                InParameters.Remarks = Me.Txt_Remarks.Text
                InParameters.Reference = Me.Txt_Reference.Text
                InParameters.FDID = Me.GLookUp_FD_List.Tag
                InParameters.MasterTxnID = Me.xMID.Text
                InParameters.Status_Action = Status_Action
                InParameters.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParameters) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InTDS_Deducted2 = InParameters
            End If

            'Catch ex As Exception
            '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End Try
            If Not Base._FD_Voucher_DBOps.UpdateRenewFD_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_RenewFD_DeleteVoucherFD = New Common_Lib.RealTimeService.Param_Txn_RenewFD_DeleteVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then

                If Base.AllowMultiuser() Then
                    Dim MaxValue As Object = 0
                    MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
                    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                    If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                    'Closed Bank Acc Check #g36
                    If Not IsDBNull(Me.GLookUp_BankList.Tag) Then
                        If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                            Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                            If IsDBNull(AccNo) Then AccNo = ""
                            If AccNo.Length > 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user.... ", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(Me.xMID.Text)
                    If IsDate(CloseDate) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If

                    Dim New_FD As Object = Nothing
                    New_FD = CreatedFDID ' Base._FD_Voucher_DBOps.GetNewFDIdFromClosed(CreatedFDID)
                    If Base._FD_Voucher_DBOps.GetCount(Me.xMID.Text, New_FD, 1) > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Interest / TDS Posted against Current FD.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
                'Dim FDHistory As Object = Nothing
                'FDHistory = Base._FD_Voucher_DBOps.GetPrevFDStatus(Me.xMID.Text, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString)

                ''Revert Old FD
                'If IsDBNull(FDHistory) Then FDHistory = Common_Lib.Common.FDStatus.New_FD.ToString
                'If Not Base._FD_Voucher_DBOps.ReOpenFD(FDHistory, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If  ' Bug #4353 fix 

                'Delete FD History
                'If Not Base._FD_Voucher_DBOps.DeleteFDHistory(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteFDHistory = Me.xMID.Text

                'Delete New FD
                'If Not Base._FD_Voucher_DBOps.DeleteFD(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteFD = Me.xMID.Text

                'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_Delete = Me.xMID.Text

                'If Not Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text

                If Not Base._FD_Voucher_DBOps.DeleteRenewFD_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub CloseFD_Functonality(ByVal sender As System.Object)
        Hide_Properties()
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            'Closed Bank Acc Check #g37
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Referred record already changed by some other user...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim FDvoucher_DbOps As DataTable = Base._FD_Voucher_DBOps.GetRecord(Me.xMID.Text, "65730a27-e365-4195-853e-2f59225fe8f4")
                If FDvoucher_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(FDvoucher_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current FD"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'CHECKING LOCK STATUS

                Dim MaxValue As Object = 0
                MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
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

            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
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
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If (Len(Trim(Me.GLookUp_FD_List.Tag)) = 0 Or Len(Trim(Me.GLookUp_FD_List.Text)) = 0) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F D   N o t   S e l e c t e d . . . !", Me.GLookUp_FD_List, 0, Me.GLookUp_FD_List.Height, 5000)
                Me.GLookUp_FD_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FD_List)
            End If
            If Len(Trim(Me.GLookUp_FD_List.Text)) = 0 Then Me.GLookUp_FD_List.Tag = ""

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If IsDate(Me.TXT_NRC_DATE.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                Me.TXT_NRC_DATE.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
            End If

            If IsDate(Me.TXT_NRC_DATE.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.TXT_NRC_DATE.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.TXT_NRC_DATE.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If

                If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_DATE").ToString) >= TXT_NRC_DATE.DateTime Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("C l o s u r e    D a t e   m u s t   b e   G r e a t e r   t h a n   F D   S t a r t   D a t e  . . . !", Me.TXT_NRC_DATE, 0, Me.TXT_NRC_DATE.Height, 5000)
                    Me.TXT_NRC_DATE.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.TXT_NRC_DATE)
                End If
            End If

            If TXT_REC_AMOUNT.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("R e c e i v e d   A m o u n t   N o t   E n t e r e d . . . !", Me.TXT_REC_AMOUNT, 0, Me.TXT_REC_AMOUNT.Height, 5000)
                Me.TXT_REC_AMOUNT.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_REC_AMOUNT)
            End If

            If Val(Txt_Amount.Text) > Val(Me.TXT_REC_AMOUNT.Text) And TXT_NRC_DATE.DateTime >= Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) Then
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Received Amount cannot be less than F.D. Amount on Maturity...!" & vbNewLine & vbNewLine & "Sure you want to Continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.TXT_REC_AMOUNT.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
                Me.ToolTip1.Hide(Me.TXT_REC_AMOUNT)
            Else
                Me.ToolTip1.Hide(Me.TXT_REC_AMOUNT)
            End If

            If TXT_INTEREST.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I n t e r e s t  N o t  E n t e r e d . . . !", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
                Me.TXT_INTEREST.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.TXT_INTEREST)
            End If

            If IsDBNull(Me.GLookUp_BankList.Tag) Then Me.GLookUp_BankList.Tag = ""
            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And Cmb_Rec_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And Cmb_Rec_Mode.Text.ToUpper <> "BANK ACCOUNT" And Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If

            If Cmb_Rec_Mode.Text.Length = 0 And Val(TXT_INTEREST.Text) > 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("R e c e i p t  M o d e  N o t  S e l e c t e d . . . !", Me.Cmb_Rec_Mode, 0, Me.Cmb_Rec_Mode.Height, 5000)
                Me.Cmb_Rec_Mode.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmb_Rec_Mode)
            End If

            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
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

            'If Val(TXT_TDS.Text) = 0 Then
            '    If (Val(TXT_REC_AMOUNT.Text) <> Val(Txt_Amount.Text) + Val(TXT_INTEREST.Text)) And Val(TXT_INTEREST.Text) > 0 Then
            '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '        Me.ToolTip1.Show("I n t e r e s t  O r  R e c e i v e d  A m o u n t  I n c o r r e c t . . . !" & vbNewLine & vbNewLine & "Received  Amount  Includes  FD  Amount + Interest", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
            '        Me.TXT_INTEREST.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.TXT_INTEREST)
            '    End If
            'End If

            'If Val(TXT_TDS.Text) > 0 Then
            '    If Val(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_AMOUNT").ToString) > (Val(TXT_TDS.Text) + Val(Txt_Amount.Text) + Val(TXT_INTEREST.Text)) Then
            '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '        Me.ToolTip1.Show("I n t e r e s t  O r  T D S  I n c o r r e c t . . . !" & vbNewLine & vbNewLine & "TDS + FD Amount + Interest Can't be less than Maturity Amount.", Me.TXT_INTEREST, 0, Me.TXT_INTEREST.Height, 5000)
            '        Me.TXT_INTEREST.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.TXT_INTEREST)
            '    End If
            'End If
        End If

        '-----------------// Start Dependencies //---------------------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim oldEditOn As DateTime
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim closeFD As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If closeFD.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = closeFD.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

                If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                    If GLookUp_FD_List.Tag.ToString.Length > 0 Then
                        Dim FDs As DataTable ' = Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag)
                        If (GLookUp_ItemListView.FocusedRowHandle >= 0 And Me.GLookUp_ItemList.Tag <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" And Me.GLookUp_ItemList.Tag <> "f6e4da62-821f-4961-9f93-f5177fca2a77" And Me.GLookUp_ItemList.Tag <> "65730a27-e365-4195-853e-2f59225fe8f4") Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                            FDs = Base._FD_Voucher_DBOps.GetFDs(True, GLookUp_FD_List.Tag, True)
                        Else
                            FDs = Base._FD_Voucher_DBOps.GetFDs(False, GLookUp_FD_List.Tag)
                        End If
                        oldEditOn = GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "REC_EDIT_ON")
                        If FDs.Rows.Count <= 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        Else
                            Dim NewEditOn As DateTime = FDs.Rows(0)("REC_EDIT_ON")
                            If oldEditOn <> NewEditOn Then 'A/E,E/E
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("FD"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                    Dim CloseDate As Object = Base._FD_Voucher_DBOps.GetFDCloseDateByFdID(Me.GLookUp_FD_List.Tag)
                    If IsDate(CloseDate) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Current FD has already been Renewed/Closed.", "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
            End If
        End If

        '------------------// End Dependencies //--------------------------


        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._FD_Voucher_DBOps.GetTxnStatus(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            Dr_Led_id = iLed_ID ' NEW FD
            If Me.Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Cr_Led_id = "00079"
                Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
            Else
                Cr_Led_id = "00080"
            End If
        Else
            If Me.Cmb_Rec_Mode.Text.ToUpper <> "CASH" Then
                Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag
                Dr_Led_id = "00079"
            Else
                Dr_Led_id = "00080"
            End If
            Cr_Led_id = iLed_ID
        End If

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_CloseFD_InsertVoucherFD = New Common_Lib.RealTimeService.Param_Txn_CloseFD_InsertVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Me.xID.Text = System.Guid.NewGuid().ToString()
            'Try
            'Master Record 
            Dim InMinfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherFD()
            InMinfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InMinfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMinfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMinfo.TDate = Txt_V_Date.Text
            'InMinfo.TDate = Txt_V_Date.Text
            InMinfo.SubTotal = Val(TXT_REC_AMOUNT.Text)
            InMinfo.Cash = 0
            InMinfo.Bank = 0
            InMinfo.TDS = Val(TXT_TDS.Text)
            InMinfo.Status_Action = Status_Action
            InMinfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.InsertMasterInfo(InMinfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMinfo

            'old FD : fd close principal
            'Dim Int_R_Date As DateTime = Nothing : Dim R_Date_Str As String = "" : If IsDate(Me.Txt_Ref_Date.Text) Then : R_Date = Me.Txt_Ref_Date.Text : R_Date_Str = "#" & R_Date.ToString(Base._Date_Format_Short) & "#" : Else : R_Date_Str = " NULL " : End If
            'Dim Int_C_Date As DateTime = Nothing : Dim C_Date_Str As String = "" : If IsDate(Me.Txt_Ref_CDate.Text) Then : C_Date = Me.Txt_Ref_CDate.Text : C_Date_Str = "#" & C_Date.ToString(Base._Date_Format_Short) & "#" : Else : C_Date_Str = " NULL " : End If
            Dim Close_Amount As Double = Val(Me.Txt_Amount.Text) : Dim InterestOverhead As Double = 0
            If Val(TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) Then
                Close_Amount = Val(TXT_REC_AMOUNT.Text) : InterestOverhead = Val(Me.Txt_Amount.Text) - Val(TXT_REC_AMOUNT.Text)
            End If

            Dim InsPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
            InsPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InsPrms.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InsPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsPrms.TDate = Txt_V_Date.Text
            'InsPrms.TDate = Me.Txt_V_Date.Text.Trim()
            InsPrms.ItemID = Me.GLookUp_ItemList.Tag
            InsPrms.Type = iTrans_Type
            InsPrms.Cr_Led_ID = Cr_Led_id
            InsPrms.Dr_Led_ID = Dr_Led_id
            InsPrms.SUB_Cr_Led_ID = Sub_Cr_Led_ID
            InsPrms.SUB_Dr_Led_ID = Sub_Dr_Led_ID
            InsPrms.Amount = Close_Amount
            InsPrms.Mode = Me.Cmb_Rec_Mode.Text
            InsPrms.Ref_BANK_ID = Me.GLookUp_BankList.Tag
            InsPrms.Ref_Branch = Me.Txt_Branch.Text
            InsPrms.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InsPrms.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsPrms.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InsPrms.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InsPrms.Ref_CDate = Txt_Ref_CDate.Text
            'InsPrms.Ref_Date = Me.Txt_Ref_Date.Text
            'InsPrms.Ref_CDate = Me.Txt_Ref_CDate.Text
            InsPrms.Narration = Me.Txt_Narration.Text
            InsPrms.Remarks = Me.Txt_Remarks.Text
            InsPrms.Reference = Me.Txt_Reference.Text
            InsPrms.FDID = Me.GLookUp_FD_List.Tag
            InsPrms.MasterTxnID = Me.xMID.Text
            InsPrms.Status_Action = Status_Action
            InsPrms.RecID = Me.xID.Text

            'If Not Base._FD_Voucher_DBOps.Insert(InsPrms) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
            '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
            '    Exit Sub
            'End If
            InNewParam.param_Insert = InsPrms

            If InterestOverhead > 0 Then
                Dim InParamts As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParamts.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParamts.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParamts.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamts.TDate = Txt_V_Date.Text
                'InParamts.TDate = Me.Txt_V_Date.Text.Trim()
                InParamts.ItemID = Me.GLookUp_ItemList.Tag
                InParamts.Type = iTrans_Type
                InParamts.Cr_Led_ID = Cr_Led_id
                InParamts.Dr_Led_ID = ""
                InParamts.SUB_Cr_Led_ID = ""
                InParamts.SUB_Dr_Led_ID = ""
                InParamts.Amount = InterestOverhead
                InParamts.Mode = ""
                InParamts.Ref_BANK_ID = ""
                InParamts.Ref_Branch = ""
                InParamts.Ref_No = ""
                InParamts.Ref_Date = ""
                InParamts.Ref_CDate = ""
                InParamts.Narration = Me.Txt_Narration.Text
                InParamts.Remarks = Me.Txt_Remarks.Text
                InParamts.Reference = Me.Txt_Reference.Text
                InParamts.FDID = Me.GLookUp_FD_List.Tag
                InParamts.MasterTxnID = Me.xMID.Text
                InParamts.Status_Action = Status_Action
                InParamts.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParamts) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InterestOverhead = InParamts

                Dim BankCharges As DataTable = Base._FD_Voucher_DBOps.GetBankChargesItemDetail()
                Dim InPams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPams.TDate = Txt_V_Date.Text
                'InPams.TDate = Me.Txt_V_Date.Text.Trim()
                InPams.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4"
                InPams.Type = BankCharges.Rows(0)("ITEM_TRANS_TYPE").ToString
                InPams.Cr_Led_ID = ""
                InPams.Dr_Led_ID = BankCharges.Rows(0)("ITEM_LED_ID").ToString
                InPams.SUB_Cr_Led_ID = ""
                InPams.SUB_Dr_Led_ID = ""
                InPams.Amount = InterestOverhead
                InPams.Mode = ""
                InPams.Ref_BANK_ID = ""
                InPams.Ref_Branch = ""
                InPams.Ref_No = ""
                InPams.Ref_Date = ""
                InPams.Ref_CDate = ""
                InPams.Narration = Me.Txt_Narration.Text
                InPams.Remarks = Me.Txt_Remarks.Text
                InPams.Reference = Me.Txt_Reference.Text
                InPams.FDID = Me.GLookUp_FD_List.Tag
                InPams.MasterTxnID = Me.xMID.Text
                InPams.Status_Action = Status_Action
                InPams.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InPams) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InterestOverhead_BankCharges = InPams
            End If

            'Close FD
            Dim _Status As String = Common_Lib.Common.FDStatus.Matured_Closed_FD.ToString
            'Dim clsFD As Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD = New Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD()
            'If IsDate(TXT_NRC_DATE.Text) Then clsFD.FDCloseDate = Convert.ToDateTime(TXT_NRC_DATE.Text).ToString(Base._Server_Date_Format_Short) Else clsFD.FDCloseDate = TXT_NRC_DATE.Text
            'clsFD.FDCloseDate = TXT_NRC_DATE.Text
            'clsFD.Status_Action = Status_Action
            'clsFD.RecID = Me.GLookUp_FD_List.Tag
            If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) > TXT_NRC_DATE.DateTime Then _Status = Common_Lib.Common.FDStatus.Premature_Closed_FD.ToString
            'clsFD.FDStatus = _Status
            'Base._FD_Voucher_DBOps.CloseFD(clsFD) 'Bug #4353 Fix

            'FD History
            Dim InFDHis As Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD()
            InFDHis.FDID = Me.GLookUp_FD_List.Tag
            InFDHis.FDAction = Common_Lib.Common.FDAction.Close_FD.ToString
            InFDHis.FDStatus = _Status
            InFDHis.TxnID = Me.xMID.Text
            InFDHis.Status_Action = Status_Action
            InFDHis.RecID = Guid.NewGuid.ToString

            'Base._FD_Voucher_DBOps.InsertFDHistory(InFDHis)
            InNewParam.param_InsertFDHistory = InFDHis

            'Interest : FD Close Interest
            If Val(TXT_INTEREST.Text) > 0 Then
                Dim IntParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                IntParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                IntParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then IntParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IntParam.TDate = Txt_V_Date.Text
                'IntParam.TDate = Me.Txt_V_Date.Text.Trim()
                IntParam.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117"
                IntParam.Type = "CREDIT"
                IntParam.Cr_Led_ID = "00069"
                IntParam.Dr_Led_ID = Dr_Led_id
                IntParam.SUB_Cr_Led_ID = Sub_Cr_Led_ID
                IntParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID
                IntParam.Amount = Val(TXT_INTEREST.Text)
                IntParam.Mode = Me.Cmb_Rec_Mode.Text
                IntParam.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                IntParam.Ref_Branch = Me.Txt_Branch.Text
                IntParam.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then IntParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else IntParam.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then IntParam.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else IntParam.Ref_CDate = Txt_Ref_CDate.Text
                'IntParam.Ref_Date = Me.Txt_Ref_Date.Text
                'IntParam.Ref_CDate = Me.Txt_Ref_CDate.Text
                IntParam.Narration = Me.Txt_Narration.Text
                IntParam.Remarks = Me.Txt_Remarks.Text
                IntParam.Reference = Me.Txt_Reference.Text
                IntParam.FDID = Me.GLookUp_FD_List.Tag
                IntParam.MasterTxnID = Me.xMID.Text
                IntParam.Status_Action = Status_Action
                IntParam.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(IntParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InFDCloseInterest = IntParam
            End If

            If Val(Val(TXT_TDS.Text) > 0) Then 'TDS DEDUCTED 
                Dim IntParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                IntParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                IntParams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then IntParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else IntParams.TDate = Txt_V_Date.Text
                'IntParams.TDate = Me.Txt_V_Date.Text.Trim()
                IntParams.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                IntParams.Type = "DEBIT"
                IntParams.Cr_Led_ID = ""
                IntParams.Dr_Led_ID = "00008"
                IntParams.SUB_Cr_Led_ID = ""
                IntParams.SUB_Dr_Led_ID = ""
                IntParams.Amount = Val(TXT_TDS.Text)
                IntParams.Mode = ""
                IntParams.Ref_BANK_ID = ""
                IntParams.Ref_Branch = ""
                IntParams.Ref_No = ""
                IntParams.Ref_Date = ""
                IntParams.Ref_CDate = ""
                IntParams.Narration = Me.Txt_Narration.Text
                IntParams.Remarks = Me.Txt_Remarks.Text
                IntParams.Reference = Me.Txt_Reference.Text
                IntParams.FDID = Me.GLookUp_FD_List.Tag
                IntParams.MasterTxnID = Me.xMID.Text
                IntParams.Status_Action = Status_Action
                IntParams.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(IntParams) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_TDSDeducted1 = IntParams
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                InParam.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117"
                InParam.Type = "CREDIT"
                InParam.Cr_Led_ID = "00069"
                InParam.Dr_Led_ID = ""
                InParam.SUB_Cr_Led_ID = ""
                InParam.SUB_Dr_Led_ID = ""
                InParam.Amount = Val(TXT_TDS.Text)
                InParam.Mode = ""
                InParam.Ref_BANK_ID = ""
                InParam.Ref_Branch = ""
                InParam.Ref_No = ""
                InParam.Ref_Date = ""
                InParam.Ref_CDate = ""
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.Txt_Remarks.Text
                InParam.Reference = Me.Txt_Reference.Text
                InParam.FDID = Me.GLookUp_FD_List.Tag
                InParam.MasterTxnID = Me.xMID.Text
                InParam.Status_Action = Status_Action
                InParam.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._FD_Voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_TDSDeducted2 = InParam
            End If

            'Catch ex As Exception
            '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End Try
            If Not Base._FD_Voucher_DBOps.InsertCloseFD_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_CloseFD_UpdateVoucherFD = New Common_Lib.RealTimeService.Param_Txn_CloseFD_UpdateVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            ' Try
            'Maaster Record 
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherFD()
            UpMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            'UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.SubTotal = Val(TXT_REC_AMOUNT.Text)
            UpMInfo.Cash = 0
            UpMInfo.Bank = 0
            UpMInfo.TDS = Val(TXT_TDS.Text)
            UpMInfo.RecID = Me.xMID.Text

            'If Not Base._FD_Voucher_DBOps.UpdateMasterInfo(UpMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMInfo

            'DELETE TRANSACTIONS 
            'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteTxns = Me.xMID.Text

            'old FD : fd close principal
            'Dim Int_R_Date As DateTime = Nothing : Dim R_Date_Str As String = "" : If IsDate(Me.Txt_Ref_Date.Text) Then : R_Date = Me.Txt_Ref_Date.Text : R_Date_Str = "#" & R_Date.ToString(Base._Date_Format_Short) & "#" : Else : R_Date_Str = " NULL " : End If
            'Dim Int_C_Date As DateTime = Nothing : Dim C_Date_Str As String = "" : If IsDate(Me.Txt_Ref_CDate.Text) Then : C_Date = Me.Txt_Ref_CDate.Text : C_Date_Str = "#" & C_Date.ToString(Base._Date_Format_Short) & "#" : Else : C_Date_Str = " NULL " : End If
            Dim Close_Amount As Double = Val(Me.Txt_Amount.Text) : Dim InterestOverhead As Double = 0
            If Val(TXT_REC_AMOUNT.Text) < Val(Me.Txt_Amount.Text) Then
                Close_Amount = Val(TXT_REC_AMOUNT.Text) : InterestOverhead = Val(Me.Txt_Amount.Text) - Val(TXT_REC_AMOUNT.Text)
            End If
            Me.xID.Text = Guid.NewGuid.ToString()
            Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
            InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
            InParams.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
            'InParams.TDate = Me.Txt_V_Date.Text.Trim()
            InParams.ItemID = Me.GLookUp_ItemList.Tag
            InParams.Type = iTrans_Type
            InParams.Cr_Led_ID = Cr_Led_id
            InParams.Dr_Led_ID = Dr_Led_id
            InParams.SUB_Cr_Led_ID = Sub_Cr_Led_ID
            InParams.SUB_Dr_Led_ID = Sub_Dr_Led_ID
            InParams.Amount = Close_Amount
            InParams.Mode = Me.Cmb_Rec_Mode.Text
            InParams.Ref_BANK_ID = Me.GLookUp_BankList.Tag
            InParams.Ref_Branch = Me.Txt_Branch.Text
            InParams.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParams.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParams.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_CDate = Txt_Ref_CDate.Text
            'InParams.Ref_Date = Me.Txt_Ref_Date.Text
            'InParams.Ref_CDate = Me.Txt_Ref_CDate.Text
            InParams.Narration = Me.Txt_Narration.Text
            InParams.Remarks = Me.Txt_Remarks.Text
            InParams.Reference = Me.Txt_Reference.Text
            InParams.FDID = Me.GLookUp_FD_List.Tag
            InParams.MasterTxnID = Me.xMID.Text
            InParams.Status_Action = Status_Action
            InParams.RecID = Me.xID.Text

            'If Not Base._FD_Voucher_DBOps.Insert(InParams) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_InsertCloseAmt = InParams

            If InterestOverhead > 0 Then
                Dim InPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPms.TDate = Txt_V_Date.Text
                'InPms.TDate = Me.Txt_V_Date.Text.Trim()
                InPms.ItemID = Me.GLookUp_ItemList.Tag
                InPms.Type = iTrans_Type
                InPms.Cr_Led_ID = Cr_Led_id
                InPms.Dr_Led_ID = ""
                InPms.SUB_Cr_Led_ID = ""
                InPms.SUB_Dr_Led_ID = ""
                InPms.Amount = InterestOverhead
                InPms.Mode = ""
                InPms.Ref_BANK_ID = ""
                InPms.Ref_Branch = ""
                InPms.Ref_No = ""
                InPms.Ref_Date = ""
                InPms.Ref_CDate = ""
                InPms.Narration = Me.Txt_Narration.Text
                InPms.Remarks = Me.Txt_Remarks.Text
                InPms.Reference = Me.Txt_Reference.Text
                InPms.FDID = Me.GLookUp_FD_List.Tag
                InPms.MasterTxnID = Me.xMID.Text
                InPms.Status_Action = Status_Action
                InPms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InPms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InterestOverhead = InPms

                Dim BankCharges As DataTable = Base._FD_Voucher_DBOps.GetBankChargesItemDetail()
                Dim InsPms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InsPms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InsPms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InsPms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsPms.TDate = Txt_V_Date.Text
                'InsPms.TDate = Me.Txt_V_Date.Text.Trim()
                InsPms.ItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4"
                InsPms.Type = BankCharges.Rows(0)("ITEM_TRANS_TYPE").ToString
                InsPms.Cr_Led_ID = ""
                InsPms.Dr_Led_ID = BankCharges.Rows(0)("ITEM_LED_ID").ToString
                InsPms.SUB_Cr_Led_ID = ""
                InsPms.SUB_Dr_Led_ID = ""
                InsPms.Amount = InterestOverhead
                InsPms.Mode = ""
                InsPms.Ref_BANK_ID = ""
                InsPms.Ref_Branch = ""
                InsPms.Ref_No = ""
                InsPms.Ref_Date = ""
                InsPms.Ref_CDate = ""
                InsPms.Narration = Me.Txt_Narration.Text
                InsPms.Remarks = Me.Txt_Remarks.Text
                InsPms.Reference = Me.Txt_Reference.Text
                InsPms.FDID = Me.GLookUp_FD_List.Tag
                InsPms.MasterTxnID = Me.xMID.Text
                InsPms.Status_Action = Status_Action
                InsPms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InsPms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InterestOverhead_BankCharges = InsPms
                'STR1 = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_DR_LED_ID,TR_MODE,TR_REF_BANK_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_TRF_CROSS_REF_ID,TR_M_ID," & _
                '                            "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                '                            ") VALUES(" & _
                '                            "'" & Base._open_Cen_ID & "'," & _
                '                            "'" & Base._open_Year_ID & "'," & _
                '                            " " & Voucher_Screen_Code.Fixed_Deposits & "," & _
                '                            "'" & Me.Txt_V_NO.Text & "', " & _
                '                            "#" & xDate.ToString(Base._Date_Format_Short) & "#, " & _
                '                             "'c92da5ab-082d-45d9-b6b7-78752625c715', " & _
                '                            "'DEBIT', " & _
                '                            "NULL, " & _
                '                            "'00069'," & _
                '                            "NULL, " & _
                '                            "NULL, " & _
                '                            "NULL, " & _
                '                            "NULL, " & _
                '                           "NULL, " & _
                '                            "NULL, " & _
                '                            "NULL, " & _
                '                            " " & InterestOverhead & ", " & _
                '                            "'" & Me.Txt_Narration.Text & "', " & _
                '                            "'" & Me.Txt_Remarks.Text & "', " & _
                '                            "'" & Me.Txt_Reference.Text & "', " & _
                '                            "'" & Me.GLookUp_FD_List.Tag & "', " & _
                '                            "'" & Me.xMID.Text & "', " & _
                '                            "#" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', #" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', " & Status_Action & ", #" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', '" & Guid.NewGuid.ToString() & "'" & _
                '                            ")"
                'Command.CommandText = STR1 : Command.ExecuteNonQuery()
            End If

            'Close FD
            Dim _Status As String = Common_Lib.Common.FDStatus.Matured_Closed_FD.ToString
            'Dim ClsFD As Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD = New Common_Lib.RealTimeService.Parameter_CloseFD_VoucherFD()
            'If IsDate(TXT_NRC_DATE.Text) Then ClsFD.FDCloseDate = Convert.ToDateTime(TXT_NRC_DATE.Text).ToString(Base._Server_Date_Format_Short) Else ClsFD.FDCloseDate = TXT_NRC_DATE.Text
            ''ClsFD.FDCloseDate = TXT_NRC_DATE.Text
            'ClsFD.FDStatus = _Status
            'ClsFD.Status_Action = Status_Action
            'ClsFD.RecID = Me.GLookUp_FD_List.Tag
            'MATURITY_DATE

            If Convert.ToDateTime(Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_DATE").ToString) > TXT_NRC_DATE.DateTime Then _Status = Common_Lib.Common.FDStatus.Premature_Closed_FD.ToString
            'Base._FD_Voucher_DBOps.CloseFD(ClsFD) 'Bug #4353 Fix

            'FD History
            Dim InFDHty As Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD = New Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD()
            InFDHty.FDID = Me.GLookUp_FD_List.Tag
            InFDHty.FDAction = Common_Lib.Common.FDAction.Close_FD.ToString
            InFDHty.FDStatus = _Status
            InFDHty.TxnID = Me.xMID.Text
            InFDHty.Status_Action = Status_Action
            InFDHty.RecID = Guid.NewGuid.ToString
            'Base._FD_Voucher_DBOps.InsertFDHistory(InFDHty)
            EditParam.param_InsertFDHistory = InFDHty

            'Interest : FD Close Interest
            If Val(TXT_INTEREST.Text) > 0 Then
                Dim InPrms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InPrms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InPrms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InPrms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.TDate = Txt_V_Date.Text
                'InPrms.TDate = Me.Txt_V_Date.Text.Trim()
                InPrms.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117"
                InPrms.Type = "CREDIT"
                InPrms.Cr_Led_ID = "00069"
                InPrms.Dr_Led_ID = Dr_Led_id
                InPrms.SUB_Cr_Led_ID = Sub_Cr_Led_ID
                InPrms.SUB_Dr_Led_ID = Sub_Dr_Led_ID
                InPrms.Amount = Val(TXT_INTEREST.Text)
                InPrms.Mode = Me.Cmb_Rec_Mode.Text
                InPrms.Ref_BANK_ID = Me.GLookUp_BankList.Tag
                InPrms.Ref_Branch = Me.Txt_Branch.Text
                InPrms.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InPrms.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InPrms.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InPrms.Ref_CDate = Txt_Ref_CDate.Text
                'InPrms.Ref_Date = Me.Txt_Ref_Date.Text
                'InPrms.Ref_CDate = Me.Txt_Ref_CDate.Text
                InPrms.Narration = Me.Txt_Narration.Text
                InPrms.Remarks = Me.Txt_Remarks.Text
                InPrms.Reference = Me.Txt_Reference.Text
                InPrms.FDID = Me.GLookUp_FD_List.Tag
                InPrms.MasterTxnID = Me.xMID.Text
                InPrms.Status_Action = Status_Action
                InPrms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InPrms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InFDCloseInterest = InPrms
            End If

            If Val(Val(TXT_TDS.Text) > 0) Then 'TDS DEDUCTED 
                Dim InParms As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParms.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParms.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParms.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParms.TDate = Txt_V_Date.Text
                'InParms.TDate = Me.Txt_V_Date.Text.Trim()
                InParms.ItemID = "d0219173-45ff-4284-ae0e-89ba0e8d76b4"
                InParms.Type = "DEBIT"
                InParms.Cr_Led_ID = ""
                InParms.Dr_Led_ID = "00008"
                InParms.SUB_Cr_Led_ID = ""
                InParms.SUB_Dr_Led_ID = ""
                InParms.Amount = Val(TXT_TDS.Text)
                InParms.Mode = ""
                InParms.Ref_BANK_ID = ""
                InParms.Ref_Branch = ""
                InParms.Ref_No = ""
                InParms.Ref_Date = ""
                InParms.Ref_CDate = ""
                InParms.Narration = Me.Txt_Narration.Text
                InParms.Remarks = Me.Txt_Remarks.Text
                InParms.Reference = Me.Txt_Reference.Text
                InParms.FDID = Me.GLookUp_FD_List.Tag
                InParms.MasterTxnID = Me.xMID.Text
                InParms.Status_Action = Status_Action
                InParms.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParms) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_TDSDeducted1 = InParms
                Dim InParamts As Common_Lib.RealTimeService.Parameter_Insert_VoucherFD = New Common_Lib.RealTimeService.Parameter_Insert_VoucherFD()
                InParamts.TransCode = Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                InParamts.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParamts.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamts.TDate = Txt_V_Date.Text
                'InParamts.TDate = Me.Txt_V_Date.Text.Trim()
                InParamts.ItemID = "1ed5cbe4-c8aa-4583-af44-eba3db08e117"
                InParamts.Type = "CREDIT"
                InParamts.Cr_Led_ID = "00069"
                InParamts.Dr_Led_ID = ""
                InParamts.SUB_Cr_Led_ID = ""
                InParamts.SUB_Dr_Led_ID = ""
                InParamts.Amount = Val(TXT_TDS.Text)
                InParamts.Mode = ""
                InParamts.Ref_BANK_ID = ""
                InParamts.Ref_Branch = ""
                InParamts.Ref_No = ""
                InParamts.Ref_Date = ""
                InParamts.Ref_CDate = ""
                InParamts.Narration = Me.Txt_Narration.Text
                InParamts.Remarks = Me.Txt_Remarks.Text
                InParamts.Reference = Me.Txt_Reference.Text
                InParamts.FDID = Me.GLookUp_FD_List.Tag
                InParamts.MasterTxnID = Me.xMID.Text
                InParamts.Status_Action = Status_Action
                InParamts.RecID = Guid.NewGuid.ToString()

                'If Not Base._FD_Voucher_DBOps.Insert(InParamts) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_TDSDeducted2 = InParamts
            End If
            'Catch ex As Exception
            '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End Try
            If Not Base._FD_Voucher_DBOps.UpdateCloseFD_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_CloseFD_DeleteVoucherFD = New Common_Lib.RealTimeService.Param_Txn_CloseFD_DeleteVoucherFD
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                'Try

                DelParam.MID_Delete = Me.xMID.Text
                'If Not Base._FD_Voucher_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text
                'If Not Base._FD_Voucher_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                'FETCH FD STATUS BEFORE CURRENT FD CLOSE OPERATION FROM HISTORY 
                'Dim FDHistory As Object = Nothing
                'FDHistory = Base._FD_Voucher_DBOps.GetPrevFDStatus(Me.xMID.Text, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString)

                ''Revert Old FD
                'If IsDBNull(FDHistory) Then FDHistory = Common_Lib.Common.FDStatus.New_FD.ToString
                'If Not Base._FD_Voucher_DBOps.ReOpenFD(FDHistory, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If 'Bug #4353 Fix

                'Delete FD History
                'If Not Base._FD_Voucher_DBOps.DeleteFDHistory(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteFDHistory = Me.xMID.Text
                'Catch ex As Exception
                '    DevExpress.XtraEditors.XtraMessageBox.Show("An Error Occoured while FD operation!!" & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End Try
                If Not Base._FD_Voucher_DBOps.DeleteCloseFD_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If
            xPromptWindow.Dispose()
        End If
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
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._FD_Voucher_DBOps.GetItemList
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
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.Txt_Branch.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.Txt_AccountNo.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetBankList()
        Dim BA_Table As DataTable = Base._FD_Voucher_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._FD_Voucher_DBOps.GetBranches(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'BUILD DATA
        Dim BuildData = From B In BB_Table, A In BA_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID"))
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
            Cnt_BankAccount = 1
            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            ' Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            ' Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray

            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False
        Else
            Cnt_BankAccount = Final_Data.Count
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
    End Sub
    Private Sub GLookUp_BankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_Bank(sender)))
    End Sub
    Private Sub FilterLookup_Bank(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        ' Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub

    Private Sub GLookUp_FD_List_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_FD_List.EditValueChanged
        If Me.GLookUp_FD_List.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_FD_ListView.RowCount > 0 And Val(Me.GLookUp_FD_ListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_FD_List.Tag = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString
                Me.TXT_Fd_Bank.Text = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_NAME").ToString
                'Me.TXT_FD_Branch.Text = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.Txt_Amount.Text = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_AMOUNT").ToString
                If iAction = Common_Lib.Common.FDAction.Close_FD Then Me.TXT_RENEWAL_MATURITY_AMOUNT.Text = Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "MATURITY_AMOUNT").ToString
                Me.TXT_TDS_PREV.Text = (Val(Base._FD_Voucher_DBOps.GetTDS(Me.xMID.Text, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString)) - Val(Base._FD_Voucher_DBOps.GetTDSReversal(Me.xMID.Text, Me.GLookUp_FD_ListView.GetRowCellValue(Me.GLookUp_FD_ListView.FocusedRowHandle, "FD_ID").ToString))).ToString
                If GLookUp_FD_List.Tag.ToString.Length > 0 And TXT_REC_AMOUNT.Text.Length > 0 Then
                    TXT_INTEREST.Text = Val(TXT_REC_AMOUNT.Text) - Val(Txt_Amount.Text)
                End If
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_FD_List_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_FD_List.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_FD_ListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_FD_ListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_FD_List.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_FD_ListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_FD_ListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_FD_List.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_FD_List_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_FD_List.KeyDown
        If e.KeyCode = Keys.PageUp And GLookUp_FD_List.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_BankList.CancelPopup()
            Hide_Properties()
            Me.TXT_REC_AMOUNT.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_FD_List.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.TXT_REC_AMOUNT.Focus()
        End If
    End Sub
    Private Sub LookUp_GetFDList(Optional ByVal xFDId As String = "")
        'Dim BA_Table As DataTable = Base._FD_Voucher_DBOps.GetFDBankAccounts()
        'If BA_Table Is Nothing Then
        '    Base.HandleDBError_OnNothingReturned()
        '    Exit Sub
        'End If

        'Dim Branch_IDs As String = ""
        'For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        'If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        'If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        'Dim BB_Table As DataTable = Base._FD_Voucher_DBOps.GetBranches(Branch_IDs)
        'If BB_Table Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim FD_Table As DataTable = Nothing
        ''show both open and closed FDs in case of FD Expense/income
        Dim Final_Data As DataTable = Nothing
        If (GLookUp_ItemListView.FocusedRowHandle >= 0 And Me.GLookUp_ItemList.Tag <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" And Me.GLookUp_ItemList.Tag <> "f6e4da62-821f-4961-9f93-f5177fca2a77" And Me.GLookUp_ItemList.Tag <> "65730a27-e365-4195-853e-2f59225fe8f4") Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            Final_Data = Base._FD_Voucher_DBOps.GetFDs(True, Nothing, True)
        Else
            Final_Data = Base._FD_Voucher_DBOps.GetFDs(False)
        End If
        'If FD_Table Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If
        ''BUILD DATA
        'Dim BuildData = From B In BB_Table, A In BA_Table, F In FD_Table _
        '                Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
        '                And (A.Field(Of String)("ID") = F.Field(Of String)("FD_BA_ID")) _
        '                Select New With { _
        '                                .BANK_NAME = B.Field(Of String)("Name"), _
        '                                .BI_SHORT_NAME = B.Field(Of String)("BI_SHORT_NAME"), _
        '                                .BANK_BRANCH = B.Field(Of String)("Branch"), _
        '                                .BANK_CUST_NO = A.Field(Of String)("BA_CUST_NO"), _
        '                                .BA_ID = A.Field(Of String)("ID"), _
        '                                .FD_ID = F.Field(Of String)("REC_ID"), _
        '                                .FD_NO = F.Field(Of String)("FD_NO"), _
        '                                .FD_AMOUNT = F.Field(Of Decimal)("FD_AMT"), _
        '                                .MATURITY_AMOUNT = F.Field(Of Decimal)("FD_MAT_AMT"), _
        '                                .MATURITY_DATE = F.Field(Of DateTime?)("FD_MAT_DATE"), _
        '                                .FD_DATE = F.Field(Of DateTime?)("FD_DATE"), _
        '                                .FD_AS_DATE = F.Field(Of DateTime?)("FD_AS_DATE"), _
        '                                .FD_STATUS = F.Field(Of String)("FD_STATUS"), _
        '                                .ROI = F.Field(Of Decimal)("FD_INT_RATE"), _
        '                                .REC_EDIT_ON = F.Field(Of DateTime)("REC_EDIT_ON")
        '                                  }
        'Dim OrderedData = From Selection In BuildData
        '                  Order By Selection.FD_NO Ascending, FD_AS_DATE Ascending
        '                  Select Selection
        'Dim Final_Data = OrderedData.ToList
        If Final_Data.Rows.Count > 0 Then
            Me.GLookUp_FD_List.Properties.ValueMember = "FD_ID"
            Me.GLookUp_FD_List.Properties.DisplayMember = "FD_NO"
            Me.GLookUp_FD_List.Properties.DataSource = Final_Data
            Me.GLookUp_FD_ListView.RefreshData()
            Me.GLookUp_FD_List.Properties.Tag = "SHOW"
        Else
            '  Me.Cmd_Mode.Properties.ReadOnly = True
            Me.GLookUp_FD_List.Properties.Tag = "NONE"
        End If
    End Sub
    Private Sub GLookUp_FD_List_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_FD_List.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_FD(sender)))
    End Sub
    Private Sub FilterLookup_FD(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        '  Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("FD_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("FD_Amount", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("Bank_Name", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
#End Region

#Region "Start--> Private Events"
    Public Sub CreateReminders(ByVal RenewalDate As DateTime, ByVal FDAccNo As String, ByVal FDBank As String)
        Dim xDate As Date = RenewalDate.AddDays(-7)
        Dim xTime As DateTime = FormatDateTime("09:00", DateFormat.ShortTime)
        Dim trans As OleDbTransaction = Nothing
        Using Con As New OleDbConnection(Base._data_ConStr_Sys)
            Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
            Dim STR1 As String = "INSERT INTO REMINDER_INFO(REM_CEN_ID,REM_TYPE,REM_TITLE,REM_DESCRIPTION,REM_SDATE,REM_STIME,REM_RECUR,REM_MUSIC,REM_TR_ID," & _
                                                           "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                  ") VALUES(" & _
                                 "'" & Base._open_Cen_ID & "', " & _
                                 "'DAILY'" & _
                                 "'F.D. RENEWAL'" & _
                                 "' FD Account No " & FDAccNo & " in " & FDBank & " needs to be renewed on " & RenewalDate.ToLongDateString & "', " & _
                                 "#" & xDate.ToString(Base._Date_Format_Short) & "#, " & _
                                 "'" & xTime.ToString(Base._Time_Format_AM_PM, Base.fi) & "', " & _
                                 "7," & _
                                 "1, " & _
                                 "'" & Me.xMID.Text & "'," & _
                                 "#" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', #" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', " & Common_Lib.Common.Record_Status._Completed & ", #" & Now.ToString(Base._Date_Format_Long) & "#, '" & Base._open_User_ID & "', '" & System.Guid.NewGuid().ToString() & "'" & _
                                 ")"
            command.CommandText = STR1 : command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DeleteReminder()
        Using Con As New OleDbConnection(Base._data_ConStr_Sys)
            Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
            Dim STRdelete As String = " UPDATE REMINDER_INFO SET " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Deleted & "," & _
                                        "REC_EDIT_ON     =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
                                        "REC_STATUS_ON     =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
                                        "REC_STATUS_BY     ='" & Base._open_User_ID & "'  " & _
                                        "  WHERE REM_TR_ID    ='" & Me.xMID.Text & "'"
            command.CommandText = STRdelete : command.ExecuteNonQuery()
        End Using
    End Sub
#End Region

   
  
End Class