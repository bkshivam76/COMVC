Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_FD_Window

#Region "Start--> Default Variables"

    Public TxnID As String = ""
    Public Status As String = ""
    Public MatDate As DateTime
    Public iAction As Common_Lib.Common.FDAction = -1
    Public iRenewFrom As String = ""
    Public iRenewFDNo As String = ""
    Dim _action As String = ""
    Public xFdID As String = "" 'Used only when FD is 
    Public InFD As Common_Lib.RealTimeService.Parameter_InsertFD_VoucherFD = Nothing
    Public InFDHty As Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD = Nothing
    Public InRenFDHis As Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD = Nothing
    Public UpFD As Common_Lib.RealTimeService.Parameter_UpdateFD_VoucherFD = Nothing
    Public UpFDHis As Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD = Nothing
    Public UpRenFDHty As Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD = Nothing
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
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub

#End Region

#Region "Start--> Button Events"
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.O)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
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
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Select Case iAction
            Case Common_Lib.Common.FDAction.New_FD
                _action = Common_Lib.Common.FDAction.New_FD.ToString
            Case Common_Lib.Common.FDAction.Renew_FD
                _action = Common_Lib.Common.FDAction.Renew_FD.ToString
            Case Common_Lib.Common.FDAction.Close_FD
                _action = Common_Lib.Common.FDAction.Close_FD.ToString
        End Select
        Hide_Properties()
       
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then


            If Val(txt_Rate.Text) >= 15 Then
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Message", "Please check the Rate of Interest...!" & vbNewLine & vbNewLine & "Do you want to Continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.txt_Rate.Focus()
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
            End If

            'check FD Bank as blank
            If Me.Look_BankList.Tag.ToString.Trim.Length <= 0 Or Me.Look_BankList.Text.Trim.Length <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N a m e   N o t   S e l e c t e d . . . !", Me.Look_BankList, 0, Me.Look_BankList.Height, 5000)
                Me.Look_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_BankList)
            End If

            'check FD NO as blank
            If Me.Txt_No.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P l e a s e   E n t e r   F. D.  A c c o u n t  N o. . . . !", Me.Txt_No, 0, Me.Txt_No.Height, 5000)
                Me.Txt_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_No)
            End If

            'check FD date as blank
            If IsDate(Me.Txt_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                Me.Txt_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Date)
            End If
            '1
            Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_Date.Text))
            If diff < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                Me.Txt_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Date)
            End If
            '2
            diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_Date.Text))
            If diff > 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                Me.Txt_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Date)
            End If
            'check FD As date as blank
            If IsDate(Me.Txt_As_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_As_Date, 0, Me.Txt_As_Date.Height, 5000)
                Me.Txt_As_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_As_Date)
            End If
            If Me.Txt_As_Date.DateTime > Me.Txt_Date.DateTime Then
                Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                Me.ToolTip1.Show("A s   o f   D a t e   m u s t   b e   E q u a l   /   L o w e r  t h a n   t o   F. D.   D a t e . . . !", Me.Txt_As_Date, 0, Me.Txt_As_Date.Height, 5000)
                Me.Txt_As_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_As_Date)
            End If
            'commented to allow premature renewal
            'If IsDate(Me.Txt_As_Date.Text) = True And IsDate(Me.MatDate) = True And Convert.ToDateTime(Me.Txt_As_Date.Text) < Me.MatDate Then
            '    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
            '    Me.ToolTip1.Show("A s   o f   D a t e   m u s t   b e   E q u a l   /   G r e a t e r  t h a n   M a t u r i t y  D a t e  o f  R e n e w e d  F D. . . !", Me.Txt_As_Date, 0, Me.Txt_As_Date.Height, 5000)
            '    Me.Txt_As_Date.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'Else
            '    Me.ToolTip1.Hide(Me.Txt_As_Date)
            'End If

            'check FD Amount as blank
            If Val(Me.Txt_Amount.EditValue) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F D   A m o u n t   c a n n o t   b e   Z e r o   /   N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            'check FD Rate as blank

            If Val(Me.txt_Rate.EditValue) <= 0 Or Val(Me.txt_Rate.EditValue) > 100 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F D   R a t e   I n c o r r e c t . . . !", Me.txt_Rate, 0, Me.txt_Rate.Height, 5000)
                Me.txt_Rate.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.txt_Rate)
            End If

            'check FD Maturity Amount as blank
            If Val(Me.Txt_Mat_Amount.EditValue) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("M a t u r i t y   A m o u n t   c a n n o t   b e   Z e r o   /   N e g a t i v e . . . !", Me.Txt_Mat_Amount, 0, Me.Txt_Mat_Amount.Height, 5000)
                Me.Txt_Mat_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Mat_Amount)
            End If

            If Val(Me.Txt_Mat_Amount.EditValue) < Val(Me.Txt_Amount.EditValue) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("M a t u r i t y   A m o u n t   N o t   L e s s   t h a n   t o   F. D.   A m o u n t . . . !", Me.Txt_Mat_Amount, 0, Me.Txt_Mat_Amount.Height, 5000)
                Me.Txt_Mat_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Mat_Amount)
            End If

            'check FD Maturity date as blank
            If IsDate(Me.Txt_Mat_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Mat_Date, 0, Me.Txt_Mat_Date.Height, 5000)
                Me.Txt_Mat_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Mat_Date)
            End If

            'check FD Maturity date > FD Date
            If Txt_Mat_Date.DateTime <= Txt_Date.DateTime Then
                Me.ToolTip1.ToolTipTitle = "Invalid Information . . ."
                Me.ToolTip1.Show("M a t u r i t y   D a t e    M u s t   B e   G r e a t e r   T h a n    F . D .   D a t e . . . !", Me.Txt_Mat_Date, 0, Me.Txt_Mat_Date.Height, 5000)
                Me.Txt_Mat_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Mat_Date)
            End If
        End If


        'CHECKING LOCK STATUS
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim MaxValue As Object = 0
            MaxValue = Base._FD_Voucher_DBOps.GetStatusByID(Me.xID.Text)
            If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        End If

        'CHECKING duplicate Acc no
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim MaxValue As Object = 0
            MaxValue = Base._FD_Voucher_DBOps.GetAccountNoCount(xID.Text, Txt_No.Text.Trim(), Me.Look_BankList.Tag)
            If MaxValue Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If MaxValue > 0 And Not Txt_No.Text.Trim().ToUpper.Equals(iRenewFDNo.ToUpper) Then
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Message", "Same Account No. already exists...!" & vbNewLine & "Do you want to Continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.Txt_No.Focus()
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
            End If
        End If

        ''-------------------------- // Start Dependencies // ----------------------------
        'If Base.AllowMultiuser() Then
        '    If Look_BankList.Tag.ToString.Length > 0 Then
        '        Dim cnt As Object = Base._FD_Voucher_DBOps.GetFDBankAccounts(Look_BankList.Tag).Rows.Count
        '        If cnt Is Nothing Then
        '            Base.HandleDBError_OnNothingReturned()
        '            Exit Sub
        '        End If
        '        If cnt <= 0 Then
        '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred FD Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End If
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        Try
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
                Me.xID.Text = System.Guid.NewGuid().ToString()
                Dim xRenewFrom As String = "NULL" : If iAction = Common_Lib.Common.FDAction.Renew_FD Then xRenewFrom = "'" & iRenewFrom & "'"
                InFD = New Common_Lib.RealTimeService.Parameter_InsertFD_VoucherFD()
                InFD.BankAccID = Me.Look_BankList.Tag
                InFD.FDNo = Me.Txt_No.Text
                If IsDate(Txt_Date.Text) Then InFD.FDDate = Convert.ToDateTime(Txt_Date.Text).ToString(Base._Server_Date_Format_Short) Else InFD.FDDate = Txt_Date.Text
                'InFD.FDDate = Me.Txt_Date.Text.Trim
                If IsDate(Txt_As_Date.Text) Then InFD.FDAsDate = Convert.ToDateTime(Txt_As_Date.Text).ToString(Base._Server_Date_Format_Short) Else InFD.FDAsDate = Txt_As_Date.Text
                'InFD.FDAsDate = Me.Txt_As_Date.Text
                InFD.FDAmount = Val(Me.Txt_Amount.EditValue)
                InFD.FDIntRate = Val(Me.txt_Rate.EditValue)
                InFD.PaymentCondition = Me.Cmd_Type.Text
                If IsDate(Txt_Mat_Date.Text) Then InFD.FDMaturityDate = Convert.ToDateTime(Txt_Mat_Date.Text).ToString(Base._Server_Date_Format_Short) Else InFD.FDMaturityDate = Txt_Mat_Date.Text
                'InFD.FDMaturityDate = Me.Txt_Mat_Date.Text
                InFD.FDMaturityAmount = Val(Me.Txt_Mat_Amount.EditValue)
                InFD.Remarks = Me.Txt_Remarks.Text
                InFD.TxnID = TxnID
                InFD.RenewFrom_ID = xRenewFrom
                'InFD.FDStatus = Common_Lib.Common.FDStatus.New_FD 'Bug #4353 Fix
                InFD.Status_Action = Status_Action
                InFD.RecID = Me.xID.Text

                'Shifted to FD Voucher
                'If Not Base._FD_Voucher_DBOps.InsertFD(InFD) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                InFDHty = New Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD()
                InFDHty.FDID = Me.xID.Text
                InFDHty.FDAction = _action
                InFDHty.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString
                InFDHty.TxnID = TxnID
                InFDHty.Status_Action = Status_Action
                InFDHty.RecID = Guid.NewGuid.ToString

                'Shifted to FD Voucher
                'If Not Base._FD_Voucher_DBOps.InsertFDHistory(InFDHty) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If


                If iAction = Common_Lib.Common.FDAction.Renew_FD Then
                    InRenFDHis = New Common_Lib.RealTimeService.Parameter_InsertFDHistory_VoucherFD()
                    InRenFDHis.FDID = iRenewFrom
                    InRenFDHis.FDAction = Common_Lib.Common.FDAction.Renew_FD.ToString
                    InRenFDHis.FDStatus = Status
                    InRenFDHis.TxnID = TxnID
                    InRenFDHis.Status_Action = Status_Action
                    InRenFDHis.RecID = Guid.NewGuid.ToString

                    'Shifted to FD Voucher
                    'If Not Base._FD_Voucher_DBOps.InsertFDHistory(InRenFDHis) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                End If
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If

            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
                UpFD = New Common_Lib.RealTimeService.Parameter_UpdateFD_VoucherFD()
                UpFD.BankAccID = Me.Look_BankList.Tag
                UpFD.FDNo = Me.Txt_No.Text
                If IsDate(Txt_Date.Text) Then UpFD.FDDate = Convert.ToDateTime(Txt_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpFD.FDDate = Txt_Date.Text
                'UpFD.FDDate = Me.Txt_Date.Text.Trim
                If IsDate(Txt_As_Date.Text) Then UpFD.FDAsDate = Convert.ToDateTime(Txt_As_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpFD.FDAsDate = Txt_As_Date.Text
                'UpFD.FDAsDate = Me.Txt_As_Date.Text
                UpFD.FDAmount = Val(Me.Txt_Amount.EditValue)
                UpFD.FDIntRate = Val(Me.txt_Rate.EditValue)
                UpFD.PaymentCondition = Me.Cmd_Type.Text
                If IsDate(Txt_Mat_Date.Text) Then UpFD.FDMaturityDate = Convert.ToDateTime(Txt_Mat_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpFD.FDMaturityDate = Txt_Mat_Date.Text
                'UpFD.FDMaturityDate = Me.Txt_Mat_Date.Text
                UpFD.FDMaturityAmount = Val(Me.Txt_Mat_Amount.EditValue)
                UpFD.Remarks = Me.Txt_Remarks.Text
                UpFD.TxnID = TxnID
                UpFD.RenewFrom_ID = iRenewFrom
                'UpFD.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString  'Bug #4353 Fix
                'UpFD.Status_Action = Status_Action

                'Shifted to FD Voucher
                'If Not Base._FD_Voucher_DBOps.UpdateFD(UpFD) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                UpFDHis = New Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD()
                UpFDHis.FDAction = _action
                UpFDHis.FDStatus = Common_Lib.Common.FDStatus.New_FD.ToString
                UpFDHis.TxnId = TxnID
                UpFDHis.FDID = Me.xID.Text

                'Shifted to FD Voucher
                'If Not Base._FD_Voucher_DBOps.UpdateFDHistory(UpFDHis) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                If iAction = Common_Lib.Common.FDAction.Renew_FD Then
                    UpRenFDHty = New Common_Lib.RealTimeService.Parameter_UpdateFDHistory_VoucherFD()
                    UpRenFDHty.FDAction = _action
                    UpRenFDHty.FDStatus = Status
                    UpRenFDHty.TxnId = TxnID
                    UpRenFDHty.FDID = iRenewFrom
                    'Shifted to FD Voucher
                    'If Not Base._FD_Voucher_DBOps.UpdateFDHistory(UpRenFDHty) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                End If
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("An error has occoured!! " & vbNewLine & ex.Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        'Close Prev FD
        'If iAction = Common_Lib.Common.FDAction.Renew_FD Then
        '    Dim ClsFd As Common_Lib.DbOperations.Parameter_CloseFD_VoucherFD = New Common_Lib.DbOperations.Parameter_CloseFD_VoucherFD()
        '    ClsFd.FDCloseDate = Txt_Date.Text.Trim()
        '    ClsFd.FDStatus = Status
        '    ClsFd.Status_Action = Status_Action
        '    ClsFd.RecID = iRenewFrom

        '    If Not Base._FD_Voucher_DBOps.CloseFD(ClsFd) Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        'End If

    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
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
    Private Sub Txt_Date_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Date.EditValueChanged, Txt_As_Date.EditValueChanged
        'If Not IsDate(Txt_As_Date.Text) Then
        '    If Txt_Date.Text.Length > 0 Then
        '        Dim AsDate As DateTime = New DateTime(Txt_Date.DateTime.Year, Txt_Date.DateTime.Month, Txt_Date.DateTime.Day)
        '        Txt_As_Date.DateTime = AsDate
        '    End If
        'End If
        ShowPeriod()
    End Sub
    Private Sub Txt_Mat_Date_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Mat_Date.EditValueChanged
        ShowPeriod()
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_No.GotFocus, Txt_Date.GotFocus, Txt_As_Date.GotFocus, Txt_Mat_Date.GotFocus, Txt_Amount.GotFocus, Txt_Mat_Amount.GotFocus, txt_Rate.GotFocus, Txt_Amount.Click, Txt_Mat_Amount.Click, txt_Rate.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Or txt.Name = Txt_Mat_Amount.Name Or txt.Name = txt_Rate.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_No.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_No.KeyDown, Txt_Date.KeyDown, Txt_As_Date.KeyDown, Txt_Mat_Date.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_No.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Fixed Deposit (F.D.)" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None

        Me.Txt_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_As_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Mat_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TitleX.Text = "Fixed Deposit (F.D.)"
        ' Me.SubTitleX.Text = Space(5) & "As on " & Format(DateAdd(DateInterval.Day, -1, Base._open_Year_Sdt), "dd MMMM, yyyy")
        'Me.SubTitleX.Left = Me.TitleX.Left + Me.TitleX.Width + 4 : Me.SubTitleX.Top = 2

        If Not iAction = Common_Lib.Common.FDAction.Renew_FD Then Look_BankList.Tag = ""
        LookUp_GetBankList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.Txt_No.Focus()
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()
        Dim d1 As DataTable
        If xFdID.Length = 0 Then
            d1 = Base._FD_Voucher_DBOps.GetFDRecordByTxnID(TxnID)
        Else
            d1 = Base._FD_Voucher_DBOps.GetFDRecordByID(xFdID)
        End If
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.xID.Text = d1.Rows(0)("REC_ID")
        Look_BankList.EditValue = d1.Rows(0)("FD_BA_ID")
        Look_BankList.Tag = Look_BankList.EditValue
        Look_BankList.Properties.Tag = "SHOW"

        Txt_No.DataBindings.Add("text", d1, "FD_NO")
        If Txt_Amount.Text.Length = 0 Then Txt_Amount.DataBindings.Add("editvalue", d1, "FD_AMT") 'Altered Amount shall be provided from voucher screen 
        txt_Rate.DataBindings.Add("editvalue", d1, "FD_INT_RATE")
        Txt_Mat_Amount.DataBindings.Add("editvalue", d1, "FD_MAT_AMT")

        Cmd_Type.DataBindings.Add("editvalue", d1, "FD_INT_PAY_COND")
        Txt_Remarks.DataBindings.Add("text", d1, "FD_OTHER_DETAIL")

        Dim xDate As DateTime = Nothing
        If Not IsDate(Txt_Date.Text) Then xDate = d1.Rows(0)("FD_DATE") : Txt_Date.DateTime = xDate
        If Not IsDate(Txt_As_Date.Text) Then xDate = d1.Rows(0)("FD_AS_DATE") : Txt_As_Date.DateTime = xDate
        xDate = d1.Rows(0)("FD_MAT_DATE") : Txt_Mat_Date.DateTime = xDate
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Look_BankList.Enabled = False : Me.Look_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Lbl_Branch.Enabled = False : Me.Lbl_Branch.Appearance.ForeColor = SetColor
        Me.Lbl_AccountNo.Enabled = False : Me.Lbl_AccountNo.Appearance.ForeColor = SetColor
        Me.Txt_No.Enabled = False : Me.Txt_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Date.Enabled = False : Me.Txt_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_As_Date.Enabled = False : Me.Txt_As_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.txt_Rate.Enabled = False : Me.txt_Rate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Type.Enabled = False : Me.Cmd_Type.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Mat_Amount.Enabled = False : Me.Txt_Mat_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Mat_Date.Enabled = False : Me.Txt_Mat_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Lbl_Period.Enabled = False : Me.Lbl_Period.Appearance.ForeColor = SetColor
        Me.But_Bank.Enabled = False
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Look_BankList)
    End Sub
    'Public Shared Sub UpdateBankBalance(ByVal BankAccountID As String)
    '    Using T As New OleDbConnection(Base._data_ConStr_Data)
    '        T.Open() : Dim MaxValue As Object = 0 : Dim command As OleDbCommand = T.CreateCommand()
    '        'Close Prev FD
    '        'FD BALANCE UPDATE FOR BANK A/C.
    '        command.CommandText = "SELECT SUM(FD_AMT) FROM FD_INFO  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND FD_CEN_ID='" & Base._open_Cen_ID & "' AND FD_BA_ID  = '" & BankAccountID & "' AND FD_CLOSE_DATE IS NULL "
    '        MaxValue = command.ExecuteScalar()
    '        If IsDBNull(MaxValue) Then MaxValue = 0
    '        Dim STR1 As String = " UPDATE OPENING_BALANCES_INFO SET " & _
    '                                "OP_AMOUNT        =" & MaxValue & "" & _
    '                                "  WHERE REC_ID    ='" & BankAccountID & "'"
    '        command.CommandText = STR1 : command.ExecuteNonQuery()

    '    End Using
    'End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.BANK LIST
    Private Sub But_Bank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Bank.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim SaveID As String = Me.Look_BankList.Tag
            Dim xFrm As New D0009.Frm_Bank_Info
            xFrm.Text = "Bank Information (F.D.)..."
            xFrm.MainBase = Base
            xFrm.ShowDialog(Me)
            xFrm.Dispose()
            LookUp_GetBankList()
            Me.Look_BankList.EditValue = SaveID : Me.Look_BankList.Tag = SaveID : Look_BankList.Properties.Tag = "SHOW"
        End If
    End Sub
    
    Private Sub Look_BankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_BankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_BankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_BankList.CancelPopup()
            Hide_Properties()
            'Me.Txt_No.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.List_Lang.Focus()
        End If

    End Sub
    Private Sub Look_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_BankList.EditValueChanged
        If Me.Look_BankList.Properties.Tag = "SHOW" Then
            Me.Look_BankList.Tag = Me.Look_BankList.GetColumnValue("ID").ToString
            Try
                Me.Lbl_Branch.Text = "Branch: " & Me.Look_BankList.GetColumnValue("Branch").ToString
                Me.Lbl_AccountNo.Text = "Customer No.: " & Me.Look_BankList.GetColumnValue("BA_CUST_NO").ToString
            Catch ex As Exception

            End Try
        Else
        End If
    End Sub
    Private Sub LookUp_GetBankList()
        Dim BA_Table As DataTable = Base._FD_Voucher_DBOps.GetFDBankAccounts()
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
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        'BUILD DATA
        Dim BuildData = From B In BB_Table, A In BA_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
                        Select New With { _
                                        .Name = B.Field(Of String)("Name"), _
                                        .Branch = B.Field(Of String)("Branch"), _
                                        .BA_CUST_NO = A.Field(Of String)("BA_CUST_NO"), _
                                        .ID = A.Field(Of String)("ID"), _
                                        .REC_EDIT_ON = A.Field(Of DateTime)("REC_EDIT_ON")
                                        } : Dim Final_Data = BuildData.ToList
 
        If Final_Data.Count > 0 Then
            Me.Look_BankList.Properties.ValueMember = "ID"
            Me.Look_BankList.Properties.DisplayMember = "Name"
            Me.Look_BankList.Properties.DataSource = Final_Data
            Me.Look_BankList.Properties.PopulateColumns()
            Me.Look_BankList.Properties.Columns(0).Caption = "Bank Name"
            Me.Look_BankList.Properties.Columns(1).Caption = "Branch"
            Me.Look_BankList.Properties.Columns(2).Caption = "Customer No."
            Me.Look_BankList.Properties.Columns(3).Visible = False
            Me.Look_BankList.Properties.Columns(4).Visible = False
            Me.Look_BankList.Properties.BestFit()
            Me.Look_BankList.Properties.PopupWidth = 600
            Me.Look_BankList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_BankList.EditValue = 0
            Me.Look_BankList.Properties.Tag = "SHOW"
        Else
            Me.Look_BankList.Properties.Tag = "NONE"
        End If
    End Sub
    Private Sub ShowPeriod()
        If Txt_As_Date.Text.Length > 0 Then
            If Txt_Mat_Date.Text.Length > 0 Then
                Lbl_Period.Text = ((Txt_Mat_Date.DateTime - Txt_As_Date.DateTime).Days).ToString & " Day(s)."
            End If
        End If
    End Sub

#End Region

End Class