Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Voucher_Win_Gen_Rec

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer
    Public iParty_Req As String = ""
    Public iProfile As String = ""
    Public iCond_Ledger_ID As String = ""
    Public iMinValue As Double = 0
    Public iMaxValue As Double = 0
    Public iTDS_CODE As String = ""

    Public iLink_ID As String = ""
    Public iOffSet_ID As String = ""
    Public iOffSet_Item As String = ""
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime

    Public SelectedBankID As String = ""
    Public SelectedRefBankID As String = ""
    Public SelectedDepositSlipNo As Integer = 0
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
        If (keyData = (Keys.Control Or Keys.A)) Then ' Add Party / Person
            But_PersAdd_Click(Nothing, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.M)) Then ' Manage Party / Person
            But_PersManage_Click(Nothing, Nothing)
            Return (True)
        End If
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
        Hide_Properties()
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                'Closed Bank Acc Check #g31
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If AccNo Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim receipt_DbOps As DataTable = Base._Rect_DBOps.GetMasterRecord(Me.xMID.Text)
                If receipt_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If receipt_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Receipt"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(receipt_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Receipt"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                Dim MaxValue As Object = 0
                MaxValue = Base._Rect_DBOps.GetStatus(Me.xID.Text)
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
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If
            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If
            If iVoucher_Type.Trim.ToUpper = "RECEIPTS - INSTITUTE" Then
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Message...", "Received from other Institute not allowed <color=red>without Head-Quarter Permission</color>...!</b></size>" & vbNewLine & vbNewLine & "Do you want to Continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.GLookUp_ItemList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
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
            If iParty_Req.ToString.Trim.ToUpper = "YES" Then
                If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("P a r t y   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
                    Me.GLookUp_PartyList1.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
                End If
            End If
            If iLink_ID.ToString.Length > 0 Then
                If Len(Trim(Me.GLookUp_Adjustment.Tag)) = 0 Or Len(Trim(Me.GLookUp_Adjustment.Text)) = 0 Or Val(Trim(Me.GLookUp_Adjustment.Text)) <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("A d j u s t m e n t   R e f e r e n c e   N o t   S e l e c t e d . . . !", Me.GLookUp_Adjustment, 0, Me.GLookUp_Adjustment.Height, 5000)
                    Me.GLookUp_Adjustment.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_Adjustment)
                End If
            End If
            If Val(Trim(Me.Txt_Diff.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t  c a n n o t  b e  g r e a t e r  t h a n " & Val(Me.Txt_Out_Standing.Text) & "...!", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If
            If RAD_Receipt.SelectedIndex <> 2 Then 'NILL
                If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                    Me.Txt_Amount.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amount)
                End If
            End If

            If iLink_ID.ToString.Length > 0 And RAD_Receipt.SelectedIndex = 0 Then ' Partial 
                If Val(Trim(Me.Txt_Amount.Text)) >= Val(Txt_Out_Standing.Text) Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("A m o u n t   c a n n o t    b e    g r e a t e r   t h a n   o r   e q u a l   t o   O u t - S t a n d i n g   A m o u n t . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                    Me.Txt_Amount.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amount)
                End If
                'Bug #5612
                If Base._next_Unaudited_YearID <> Nothing Then
                    If Val(Trim(Me.Txt_Amount.Text)) >= Val(Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_OUTSTAND_NEXT_YEAR").ToString) Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("A m o u n t   c a n n o t    b e    g r e a t e r   t h a n   o r   e q u a l   t o   O u t - S t a n d i n g   A m o u n t   a t   e n d   o f   N e x t   y e a r (" & Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_OUTSTAND_NEXT_YEAR").ToString & ") . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                        Me.Txt_Amount.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Amount)
                    End If
                End If
            End If
            If iLink_ID.ToString.Length > 0 And RAD_Receipt.SelectedIndex = 1 Then 'Final
                If Val(Trim(Me.Txt_Amount.Text)) > Val(Txt_Out_Standing.Text) Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("A m o u n t   c a n n o t    b e    g r e a t e r   t h a n   O u t - S t a n d i n g   A m o u n t . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                    Me.Txt_Amount.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amount)
                End If
                'Bug #5612
                If Base._next_Unaudited_YearID <> Nothing Then
                    If Val(Trim(Me.Txt_Amount.Text)) > Val(Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_OUTSTAND_NEXT_YEAR").ToString) Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("A m o u n t   c a n n o t    b e    g r e a t e r   t h a n   O u t - S t a n d i n g   A m o u n t   a t   e n d   o f   N e x t   y e a r (" & Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_OUTSTAND_NEXT_YEAR").ToString & ") . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                        Me.Txt_Amount.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Amount)
                    End If
                End If
            End If
            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And (Cmd_Mode.Text.ToUpper <> "CASH") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If (Len(Trim(Me.GLookUp_RefBankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_RefBankList.Text)) = 0) And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") And (Cmd_Mode.Text.ToUpper <> "CASH TO BANK") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_RefBankList, 0, Me.GLookUp_RefBankList.Height, 5000)
                Me.GLookUp_RefBankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_RefBankList)
            End If
            If Len(Trim(Me.GLookUp_RefBankList.Text)) = 0 Then Me.GLookUp_RefBankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_Branch.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") And (Cmd_Mode.Text.ToUpper <> "CASH TO BANK") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   B r a n c h   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_Branch, 0, Me.Txt_Ref_Branch.Height, 5000)
                Me.Txt_Ref_Branch.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Branch)
            End If
            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If


            If Len(Trim(Me.GLookUp_PurList.Tag)) = 0 Or Len(Trim(Me.GLookUp_PurList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P u r p o s e   N o t   S e l e c t e d . . . !", Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                Me.GLookUp_PurList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PurList)
            End If

            If Val(Txt_Slip_No.Text) > 0 Then
                Dim BankAccIDTable As DataTable = Base._DepositSlipsDBOps.GetList(Val(Txt_Slip_No.Text), GLookUp_BankList.Tag)
                If BankAccIDTable.Rows.Count > 0 Then '--Slip Exists
                    If Not IsDBNull(BankAccIDTable.Rows(0)("BA_REC_ID")) Then
                        If BankAccIDTable.Rows(0)("BA_REC_ID") <> GLookUp_BankList.Tag Then
                            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                            Me.ToolTip1.Show("S e l e c t e d   s l i p   h a s   t r a n s a c t i o n s   o f   o t h e r   b a n k . . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                            Me.Txt_Slip_No.Focus()
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            Me.ToolTip1.Hide(Me.Txt_Slip_No)
                        End If
                    End If

                    If Not IsDBNull(BankAccIDTable.Rows(0)("Date of Print")) Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("S e l e c t e d   s l i p   i s    a l r e a d y   p r i n t e d . . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                        Me.Txt_Slip_No.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Slip_No)
                    End If
                End If
            ElseIf Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S l i p   n o.  c a n n o t   b e   n e g a t i v e   o r  Z e r o. . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                Me.Txt_Slip_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
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

            If Val(Txt_Slip_Count.Text) > 0 Then
                If Val(Txt_Slip_Count.Text) < Base._DepositSlipsDBOps.GetSlipTxnCount(Val(Me.Txt_Slip_No.Text), Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, Me.xMID.Text) Then
                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                    If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Some transaction(s) have been posted in deposit slip being used by you" & vbNewLine & vbNewLine & "Continue posting voucher using same deposit slip...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    Else
                        Exit Sub
                    End If
                End If
            End If
        End If

        '-------------------------- // Start Dependencies // ----------------------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim oldEditOn As DateTime
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    'Bank Account dependency check #Ref G31
                    Dim d1 As DataTable = Base._Rect_DBOps.GetBankAccounts(False, GLookUp_BankList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                'Address Book dependency check #Ref Z31
                If GLookUp_PartyList1.Tag.ToString.Length > 0 Then
                    Dim Address As DataTable = Base._Rect_DBOps.GetParties(GLookUp_PartyList1.Tag)
                    If Address Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    oldEditOn = GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
                    If Address.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Reffered Party Record"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = Address.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Party Record"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If


                'Advance Table...
                Dim RecptParam As Common_Lib.RealTimeService.Param_GetReceiptAdvances = New Common_Lib.RealTimeService.Param_GetReceiptAdvances()
                RecptParam.Advance_ItemID = iLink_ID
                RecptParam.Advance_PartyID = GLookUp_PartyList1.Tag
                RecptParam.Next_YearID = Base._next_Unaudited_YearID
                RecptParam.Prev_YearId = Base._prev_Unaudited_YearID
                RecptParam.Advance_RecID = GLookUp_Adjustment.Tag
                RecptParam.Tr_M_Id = Me.xMID.Text
                Dim OS_TABLE As DataTable = Base._Rect_DBOps.GetAdvances(RecptParam)  'Base._Rect_DBOps.GetPendingAdvances( GLookUp_PartyList1.Tag, iLink_ID, GLookUp_Adjustment.Tag)
                If OS_TABLE Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If OS_TABLE.Rows.Count > 0 Then
                    'Dim JointData As DataSet = GetAdvanceAdjustments(GLookUp_PartyList1.Tag, OS_TABLE)
                    If Convert.ToDecimal(OS_TABLE.Rows(0)("REF_OUTSTAND")) < Val(Me.Txt_Amount.Text) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Advance's Closing Balance"), "Referred Record value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                    'Bug #5612
                    If Base._next_Unaudited_YearID <> Nothing Then
                        If Convert.ToDecimal(OS_TABLE.Rows(0)("REF_OUTSTAND_NEXT_YEAR")) < Val(Me.Txt_Amount.Text) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Advance's Closing Balance in next year"), "Referred Record value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                    Dim CreationDate_Adv As Date = DateValue(IIf(IsDBNull(OS_TABLE.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, OS_TABLE.Rows(0)("REF_CREATION_DATE")))
                    If DateValue(Me.Txt_V_Date.Text) < CreationDate_Adv Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("C u r r e n t   R e f e r e n c e   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   A d v a n c e   C r e a t i o n   V o u c h e r   d a t e d  " & CreationDate_Adv.ToLongDateString() & " . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                        Me.Txt_V_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_V_Date)
                    End If
                End If

                Dim RecDeposits As Common_Lib.RealTimeService.Param_GetReceiptDeposits = New Common_Lib.RealTimeService.Param_GetReceiptDeposits
                RecDeposits.Dep_ItemID = iLink_ID
                RecDeposits.Dep_PartyID = GLookUp_PartyList1.Tag
                RecDeposits.Dep_RecID = GLookUp_Adjustment.Tag
                RecDeposits.Next_YearID = Base._next_Unaudited_YearID
                RecDeposits.Prev_YearId = Base._prev_Unaudited_YearID
                RecDeposits.TR_M_ID = Me.xMID.Text
                Dim OS_TABLE1 As DataTable = Base._Rect_DBOps.GetPendingDeposits(RecDeposits) 'Base._Rect_DBOps.GetPendingDeposits(GLookUp_PartyList1.Tag, iLink_ID, GLookUp_Adjustment.Tag)
                If OS_TABLE1 Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If OS_TABLE1.Rows.Count > 0 Then
                    If Convert.ToDecimal(OS_TABLE1.Rows(0)("REF_OUTSTAND")) < Val(Me.Txt_Amount.Text) + Val(Txt_Diff.Text) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Deposit's Closing Balance"), "Referred Record value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If

                    'Bug 5612
                    If Base._next_Unaudited_YearID <> Nothing Then
                        If Convert.ToDecimal(OS_TABLE1.Rows(0)("REF_OUTSTAND_NEXT_YEAR")) < Val(Me.Txt_Amount.Text) + Val(Txt_Diff.Text) Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Deposit's Closing Balance in Next Year"), "Referred Record value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                    Dim CreationDate_dep As Date = DateValue(IIf(IsDBNull(OS_TABLE1.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, OS_TABLE1.Rows(0)("REF_CREATION_DATE")))
                    If DateValue(Me.Txt_V_Date.Text) < CreationDate_dep Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("C u r r e n t   R e f e r e n c e   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   D e p o s i t   C r e a t i o n   V o u c h e r   d a t e d  " & CreationDate_dep.ToLongDateString() & " . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                        Me.Txt_V_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_V_Date)
                    End If

                    If DateValue(Me.Txt_V_Date.Text) >= CreationDate_dep And RAD_Receipt.SelectedIndex = 1 Then ' Voucher Date is not Less than Any Ref Voucher Date for final payment 
                        Dim inparam As Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate = New Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate()
                        inparam.Creation_Date = CreationDate_dep
                        inparam.Asset_RecID = GLookUp_Adjustment.Tag
                        inparam.YearID = Base._open_Year_ID
                        inparam.Tr_M_ID = Me.xMID.Text
                        Dim MxDate As Date = DateValue(Base._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam))
                        If MxDate = Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        If DateValue(Me.Txt_V_Date.Text) < MxDate Then
                            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                            Me.ToolTip1.Show("F i n a l   A d j u s t m e n t   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   p r e v i o u s    t r a n s a c t i o n    o n   s a m e   d e p o s i t   d a t e d  " & MxDate.ToLongDateString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                            Me.Txt_V_Date.Focus()
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            Me.ToolTip1.Hide(Me.Txt_V_Date)
                        End If
                    End If
                End If

                If OS_TABLE.Rows.Count = 0 And OS_TABLE1.Rows.Count = 0 And GLookUp_Adjustment.Enabled = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Referred Advance/Deposit Record"), "Referred Record value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '----------------------- // End Dependencies //-------------------------------

        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._Rect_DBOps.GetStatus(Me.xID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If
        If Cmd_Mode.Text.ToUpper = "CASH" Then
            GLookUp_BankList.EditValue = "" : Me.GLookUp_BankList.Tag = ""
            GLookUp_RefBankList.EditValue = "" : Me.GLookUp_RefBankList.Tag = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
        End If
        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        '+----JV LEDGER DETAIL----+
        Dim JV_TRANS_TYPE As String = "" : Dim JV_Cr_Led_id As String = "" : Dim JV_Dr_Led_id As String = ""
        If iLink_ID.ToString.Length > 0 And iOffSet_ID.ToString.Length > 0 Then
            'Dim JV_SQL_1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & iOffSet_ID & "'   "
            Dim JV_DT As DataTable = Base._Rect_DBOps.GetItemsListByID(iOffSet_ID)
            If JV_DT.Rows.Count > 0 Then
                JV_TRANS_TYPE = JV_DT.Rows(0)("Item_Trans_Type")
                If JV_DT.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                    JV_Dr_Led_id = JV_DT.Rows(0)("ITEM_LED_ID")
                Else
                    JV_Cr_Led_id = JV_DT.Rows(0)("ITEM_LED_ID")
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("Adjustment Item Not Found..!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If
        '+----------END-----------+
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

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherReceipt
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim STR2 As String = "" : Dim XCASH As Double = 0.0 : Dim XBANK As Double = 0.0
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Then
                XCASH = Val(Me.Txt_Amount.Text)
            Else
                XBANK = Val(Me.Txt_Amount.Text)
            End If
            'MASTER ENTRY
            Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherReceipt()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
            'InMInfo.TDate = Me.Txt_V_Date.Text
            InMInfo.PartyID = Me.GLookUp_PartyList1.Tag
            InMInfo.SubTotal = Val(Me.Txt_Amount.Text)
            InMInfo.Cash = XCASH
            InMInfo.Bank = XBANK
            InMInfo.Advance = 0
            InMInfo.Liability = 0
            InMInfo.Credit = 0
            InMInfo.TDS = 0
            InMInfo.ReceiptType = Me.RAD_Receipt.SelectedIndex
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            'If Not Base._Rect_DBOps.InsertMasterInfo(InMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMInfo


            If Val(Me.Txt_Amount.Text) > 0 Then
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text
                InParam.ItemID = Me.GLookUp_ItemList.Tag
                InParam.Type = iTrans_Type
                InParam.Cr_Led_ID = Cr_Led_id
                InParam.Dr_Led_ID = Dr_Led_id
                InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
                InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
                InParam.Mode = Me.Cmd_Mode.Text
                InParam.Ref_Bank = Me.GLookUp_RefBankList.Tag
                InParam.Ref_Branch = Me.Txt_Ref_Branch.Text
                InParam.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefDate = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefCDate = Txt_Ref_CDate.Text
                'InParam.RefDate = Me.Txt_Ref_Date.Text
                'InParam.RefCDate = Me.Txt_Ref_CDate.Text
                InParam.Amount = Val(Me.Txt_Amount.Text)
                InParam.PartyID = Me.GLookUp_PartyList1.Tag
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.Txt_Remarks.Text
                InParam.Reference = Me.Txt_Reference.Text
                InParam.Tr_M_ID = Me.xMID.Text
                InParam.TxnSrNo = 1
                InParam.Cross_Ref_ID = Me.GLookUp_Adjustment.Tag
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text

                'If Not Base._Rect_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_Insert = InParam
            End If

            If iLink_ID.ToString.Length > 0 Then

                'JV ENTRY IF FINAL ADJUSTMENT
                If (RAD_Receipt.SelectedIndex = 1 Or RAD_Receipt.SelectedIndex = 2) And Val(Me.Txt_Diff.Text) > 0 And iOffSet_ID.ToString.Length > 0 Then
                    'CREDIT ENTRY
                    Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                    InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                    InParam1.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                    'InParam1.TDate = Me.Txt_V_Date.Text
                    InParam1.ItemID = Me.GLookUp_ItemList.Tag
                    InParam1.Type = iTrans_Type
                    InParam1.Cr_Led_ID = Cr_Led_id
                    InParam1.Dr_Led_ID = ""
                    InParam1.Sub_Cr_Led_ID = ""
                    InParam1.Sub_Dr_Led_ID = ""
                    InParam1.Mode = Me.Cmd_Mode.Text
                    InParam1.Ref_Bank = ""
                    InParam1.Ref_Branch = ""
                    InParam1.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParam1.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefDate = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParam1.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefCDate = Txt_Ref_CDate.Text
                    'InParam1.RefDate = Me.Txt_Ref_Date.Text
                    'InParam1.RefCDate = Me.Txt_Ref_CDate.Text
                    InParam1.Amount = Val(Me.Txt_Diff.Text)
                    InParam1.PartyID = Me.GLookUp_PartyList1.Tag
                    InParam1.Narration = Me.Txt_Narration.Text
                    InParam1.Remarks = Me.Txt_Remarks.Text
                    InParam1.Reference = Me.Txt_Reference.Text
                    InParam1.Tr_M_ID = Me.xMID.Text
                    InParam1.TxnSrNo = 1
                    InParam1.Cross_Ref_ID = Me.GLookUp_Adjustment.Tag
                    InParam1.Status_Action = Status_Action
                    InParam1.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.Insert(InParam1) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                    '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InsertCreditJV = InParam1

                    'DEBIT ENTRY
                    Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                    InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                    InParam.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                    'InParam.TDate = Me.Txt_V_Date.Text
                    InParam.ItemID = iOffSet_ID
                    InParam.Type = JV_TRANS_TYPE
                    InParam.Cr_Led_ID = ""
                    InParam.Dr_Led_ID = JV_Dr_Led_id
                    InParam.Sub_Cr_Led_ID = ""
                    InParam.Sub_Dr_Led_ID = ""
                    InParam.Mode = Me.Cmd_Mode.Text
                    InParam.Ref_Bank = ""
                    InParam.Ref_Branch = ""
                    InParam.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefDate = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefCDate = Txt_Ref_CDate.Text
                    'InParam.RefDate = Me.Txt_Ref_Date.Text
                    'InParam.RefCDate = Me.Txt_Ref_CDate.Text
                    InParam.Amount = Val(Me.Txt_Diff.Text)
                    InParam.PartyID = Me.GLookUp_PartyList1.Tag
                    InParam.Narration = Me.Txt_Narration.Text
                    InParam.Remarks = Me.Txt_Remarks.Text
                    InParam.Reference = Me.Txt_Reference.Text
                    InParam.Tr_M_ID = Me.xMID.Text
                    InParam.TxnSrNo = 2
                    InParam.Cross_Ref_ID = "" 'Me.GLookUp_Adjustment.Tag
                    InParam.Status_Action = Status_Action
                    InParam.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                    '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InsertDebitJV = InParam

                    'purpose
                    Dim InPurpose_dr As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt()
                    InPurpose_dr.TxnID = Me.xMID.Text
                    InPurpose_dr.PurposeID = Me.GLookUp_PurList.Tag
                    InPurpose_dr.Amount = Val(Me.Txt_Diff.Text)
                    InPurpose_dr.Status_Action = Status_Action
                    InPurpose_dr.RecID = System.Guid.NewGuid().ToString()
                    InPurpose_dr.SrNo = 2
                    'If Not Base._Rect_DBOps.InsertPurpose(InPurpose_dr) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Rect_DBOps.DeletePayment(Me.xMID.Text)
                    '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                    '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InsertPurposedr = InPurpose_dr

                    'PAYMENT - ADJUSTMENT
                    Dim InPmt As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt()
                    InPmt.TxnMID = Me.xMID.Text
                    InPmt.Type = "ADJUSTMENT"
                    InPmt.SrNo = "2"
                    InPmt.RefID = Me.GLookUp_Adjustment.Tag
                    InPmt.RefAmount = Val(Me.Txt_Diff.Text)
                    InPmt.Status_Action = Status_Action
                    InPmt.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.InsertPayment(InPmt) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Rect_DBOps.DeletePayment(Me.xMID.Text)
                    '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                    '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InPmtAdjusted = InPmt
                End If

                'PAYMENT - REFUND
                Dim InPmt1 As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt()
                InPmt1.TxnMID = Me.xMID.Text
                InPmt1.Type = "REFUND"
                InPmt1.SrNo = "1"
                InPmt1.RefID = Me.GLookUp_Adjustment.Tag
                InPmt1.RefAmount = Val(Me.Txt_Amount.Text)
                InPmt1.Status_Action = Status_Action
                InPmt1.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Rect_DBOps.InsertPayment(InPmt1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._Rect_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InPmtRefund = InPmt1
            End If
            'purpose
            Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt()
            InPurpose.TxnID = Me.xMID.Text
            InPurpose.PurposeID = Me.GLookUp_PurList.Tag
            InPurpose.Amount = Val(Me.Txt_Amount.Text)
            InPurpose.Status_Action = Status_Action
            InPurpose.RecID = System.Guid.NewGuid().ToString()
            InPurpose.SrNo = 1
            'If Not Base._Rect_DBOps.InsertPurpose(InPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._Rect_DBOps.DeletePayment(Me.xMID.Text)
            '    Base._Rect_DBOps.Delete(Me.xMID.Text)
            '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            InNewParam.param_InsertPurpose = InPurpose
            If Val(Txt_Slip_No.Text) > 0 Then
                Dim inSlip As New Common_Lib.RealTimeService.Parameter_InsertSlip_VoucherReceipt
                inSlip.RecID = Guid.NewGuid.ToString
                inSlip.SlipNo = Val(Txt_Slip_No.Text)
                inSlip.TxnID = Me.xMID.Text
                inSlip.Dep_BA_ID = Sub_Dr_Led_ID
                InNewParam.param_InsertSlip = inSlip
            End If


            If Not Base._Rect_DBOps.InsertReceipt_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherReceipt = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherReceipt
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim STR2 As String = "" : Dim XCASH As Double = 0.0 : Dim XBANK As Double = 0.0
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Then
                XCASH = Val(Me.Txt_Amount.Text)
            Else
                XBANK = Val(Me.Txt_Amount.Text)
            End If
            'MASTER ENTRY
            Dim UpMaster As Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherReceipt()
            UpMaster.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMaster.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.TDate = Txt_V_Date.Text
            'UpMaster.TDate = Me.Txt_V_Date.Text
            UpMaster.PartyID = Me.GLookUp_PartyList1.Tag
            UpMaster.SubTotal = Val(Me.Txt_Amount.Text)
            UpMaster.Cash = XCASH
            UpMaster.Bank = XBANK
            UpMaster.ReceiptType = Me.RAD_Receipt.SelectedIndex
            'UpMaster.Status_Action = Status_Action
            UpMaster.RecID = Me.xMID.Text

            'If Not Base._Rect_DBOps.UpdateMaster(UpMaster) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMaster

            'If Not Base._Rect_DBOps.DeletePurpose(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeletePurpose = Me.xMID.Text
            EditParam.MID_DeleteSlip = Me.xMID.Text
            'If Not Base._Rect_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_Delete = Me.xMID.Text
            'If Not Base._Rect_DBOps.DeletePayment(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeletePayment = Me.xMID.Text

            If Val(Me.Txt_Amount.Text) > 0 Then
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text
                InParam.ItemID = Me.GLookUp_ItemList.Tag
                InParam.Type = iTrans_Type
                InParam.Cr_Led_ID = Cr_Led_id
                InParam.Dr_Led_ID = Dr_Led_id
                InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
                InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
                InParam.Mode = Me.Cmd_Mode.Text
                InParam.Ref_Bank = Me.GLookUp_RefBankList.Tag
                InParam.Ref_Branch = Me.Txt_Ref_Branch.Text
                InParam.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefDate = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefCDate = Txt_Ref_CDate.Text
                'InParam.RefDate = Me.Txt_Ref_Date.Text
                'InParam.RefCDate = Me.Txt_Ref_CDate.Text
                InParam.Amount = Val(Me.Txt_Amount.Text)
                InParam.PartyID = Me.GLookUp_PartyList1.Tag
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.Txt_Remarks.Text
                InParam.Reference = Me.Txt_Reference.Text
                InParam.Tr_M_ID = Me.xMID.Text
                InParam.TxnSrNo = 1
                InParam.Cross_Ref_ID = Me.GLookUp_Adjustment.Tag
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text

                'If Not Base._Rect_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_Insert = InParam
            End If

            If iLink_ID.ToString.Length > 0 Then

                'JV ENTRY IF FINAL ADJUSTMENT
                If (RAD_Receipt.SelectedIndex = 1 Or RAD_Receipt.SelectedIndex = 2) And Val(Me.Txt_Diff.Text) > 0 And iOffSet_ID.ToString.Length > 0 Then
                    'CREDIT ENTRY
                    Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                    InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                    InParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                    'InParams.TDate = Me.Txt_V_Date.Text
                    InParams.ItemID = Me.GLookUp_ItemList.Tag
                    InParams.Type = iTrans_Type
                    InParams.Cr_Led_ID = Cr_Led_id
                    InParams.Dr_Led_ID = ""
                    InParams.Sub_Cr_Led_ID = ""
                    InParams.Sub_Dr_Led_ID = ""
                    InParams.Mode = Me.Cmd_Mode.Text
                    InParams.Ref_Bank = ""
                    InParams.Ref_Branch = ""
                    InParams.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParams.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.RefDate = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParams.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParams.RefCDate = Txt_Ref_CDate.Text
                    'InParams.RefDate = Me.Txt_Ref_Date.Text
                    'InParams.RefCDate = Me.Txt_Ref_CDate.Text
                    InParams.Amount = Val(Me.Txt_Diff.Text)
                    InParams.PartyID = Me.GLookUp_PartyList1.Tag
                    InParams.Narration = Me.Txt_Narration.Text
                    InParams.Remarks = Me.Txt_Remarks.Text
                    InParams.Reference = Me.Txt_Reference.Text
                    InParams.Tr_M_ID = Me.xMID.Text
                    InParams.TxnSrNo = 1
                    InParams.Cross_Ref_ID = Me.GLookUp_Adjustment.Tag
                    InParams.Status_Action = Status_Action
                    InParams.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.Insert(InParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InsertCreditJV = InParams

                    'DEBIT ENTRY
                    Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_Insert_VoucherReceipt()
                    InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Receipt
                    InParam1.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                    'InParam1.TDate = Me.Txt_V_Date.Text
                    InParam1.ItemID = iOffSet_ID
                    InParam1.Type = JV_TRANS_TYPE
                    InParam1.Cr_Led_ID = ""
                    InParam1.Dr_Led_ID = JV_Dr_Led_id
                    InParam1.Sub_Cr_Led_ID = ""
                    InParam1.Sub_Dr_Led_ID = ""
                    InParam1.Mode = Me.Cmd_Mode.Text
                    InParam1.Ref_Bank = ""
                    InParam1.Ref_Branch = ""
                    InParam1.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParam1.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefDate = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParam1.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefCDate = Txt_Ref_CDate.Text
                    'InParam1.RefDate = Me.Txt_Ref_Date.Text
                    'InParam1.RefCDate = Me.Txt_Ref_CDate.Text
                    InParam1.Amount = Val(Me.Txt_Diff.Text)
                    InParam1.PartyID = Me.GLookUp_PartyList1.Tag
                    InParam1.Narration = Me.Txt_Narration.Text
                    InParam1.Remarks = Me.Txt_Remarks.Text
                    InParam1.Reference = Me.Txt_Reference.Text
                    InParam1.Tr_M_ID = Me.xMID.Text
                    InParam1.TxnSrNo = 2
                    InParam1.Cross_Ref_ID = "" 'Me.GLookUp_Adjustment.Tag
                    InParam1.Status_Action = Status_Action
                    InParam1.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.Insert(InParam1) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InsertDebitJV = InParam1

                    'PAYMENT - ADJUSTMENT
                    Dim InPmt As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt()
                    InPmt.TxnMID = Me.xMID.Text
                    InPmt.Type = "ADJUSTMENT"
                    InPmt.SrNo = "2"
                    InPmt.RefID = Me.GLookUp_Adjustment.Tag
                    InPmt.RefAmount = Val(Me.Txt_Diff.Text)
                    InPmt.Status_Action = Status_Action
                    InPmt.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Rect_DBOps.InsertPayment(InPmt) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InPmtAdjusted = InPmt

                    'purpose
                    Dim InPurpose_dr As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt()
                    InPurpose_dr.TxnID = Me.xMID.Text
                    InPurpose_dr.PurposeID = Me.GLookUp_PurList.Tag
                    InPurpose_dr.Amount = Val(Me.Txt_Diff.Text)
                    InPurpose_dr.Status_Action = Status_Action
                    InPurpose_dr.RecID = System.Guid.NewGuid().ToString()
                    InPurpose_dr.SrNo = 2
                    'If Not Base._Rect_DBOps.InsertPurpose(InPurpose_dr) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Rect_DBOps.DeletePayment(Me.xMID.Text)
                    '    Base._Rect_DBOps.Delete(Me.xMID.Text)
                    '    Base._Rect_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    EditParam.param_InsertPurposedr = InPurpose_dr
                End If
                'PAYMENT - REFUND
                Dim InPmt1 As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherReceipt()
                InPmt1.TxnMID = Me.xMID.Text
                InPmt1.Type = "REFUND"
                InPmt1.SrNo = "1"
                InPmt1.RefID = Me.GLookUp_Adjustment.Tag
                InPmt1.RefAmount = Val(Me.Txt_Amount.Text)
                InPmt1.Status_Action = Status_Action
                InPmt1.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Rect_DBOps.InsertPayment(InPmt1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InPmtRefund = InPmt1
            End If

            'purpose
            Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherReceipt()
            InPurpose.TxnID = Me.xMID.Text
            InPurpose.PurposeID = Me.GLookUp_PurList.Tag
            InPurpose.Amount = Val(Me.Txt_Amount.Text)
            InPurpose.Status_Action = Status_Action
            InPurpose.RecID = System.Guid.NewGuid().ToString()
            InPurpose.SrNo = 1 'Bug #6469 fix

            'If Not Base._Rect_DBOps.InsertPurpose(InPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_InsertPurpose = InPurpose

            If Val(Txt_Slip_No.Text) > 0 Then
                Dim inSlip As New Common_Lib.RealTimeService.Parameter_InsertSlip_VoucherReceipt
                inSlip.RecID = Guid.NewGuid.ToString
                inSlip.SlipNo = Val(Txt_Slip_No.Text)
                inSlip.TxnID = Me.xMID.Text
                inSlip.Dep_BA_ID = Sub_Dr_Led_ID
                EditParam.param_InsertSlip = inSlip
            End If

            If Not Base._Rect_DBOps.UpdateReceipt_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherReceipt = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherReceipt
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then  'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then

                'If Not Base._Rect_DBOps.DeleteItems(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteItems = Me.xMID.Text

                'If Not Base._Rect_DBOps.DeletePayment(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePayment = Me.xMID.Text

                'If Not Base._Rect_DBOps.DeletePurpose(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePurpose = Me.xMID.Text

                DelParam.MID_DeleteSlip = Me.xMID.Text

                'If Not Base._Rect_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_Delete = Me.xMID.Text

                'If Not Base._Rect_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text

                If Not Base._Rect_DBOps.DeleteReceipt_Txn(DelParam) Then
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
    Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Party)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Party Detail)..." : xfrm.TitleX.Text = "Party Detail"
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, But_PersManage.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, But_PersManage.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, But_PersManage.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Cmd_Mode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.SelectedIndexChanged
        Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None : Me.Txt_Ref_No.Properties.Mask.EditMask = ""
        If Cmd_Mode.Text.ToUpper = "CASH" Then
            LayoutControlItem1.Control.Enabled = False
            LayoutControlItem3.Control.Enabled = False
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            End If
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Gray : lbl_Bank_Name.ForeColor = Color.Gray : lbl_Branch_Name.ForeColor = Color.Gray : lbl_Acc_No.ForeColor = Color.Gray
            lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_Branch.ForeColor = Color.Gray : lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Cdate.ForeColor = Color.Gray
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            Me.Txt_Slip_No.Enabled = False : Me.Txt_Slip_Count.Enabled = False
        ElseIf Cmd_Mode.Text.ToUpper = "BANK ACCOUNT" Then
            LayoutControlItem1.Control.Enabled = True
            LayoutControlItem3.Control.Enabled = False
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            End If
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Black : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Black : lbl_Acc_No.ForeColor = Color.Black
            lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_Branch.ForeColor = Color.Gray : lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Cdate.ForeColor = Color.Gray
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            Me.Txt_Slip_No.Enabled = True : Me.Txt_Slip_Count.Enabled = True
        ElseIf Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
            LayoutControlItem1.Control.Enabled = True : lbl_Bank_Name.ForeColor = Color.Red : lbl_Acc_No.ForeColor = Color.Red

            LayoutControlItem3.Control.Enabled = True
            GLookUp_RefBankList.Enabled = False : Txt_Ref_Branch.Enabled = False : Txt_Ref_No.Enabled = False
            lbl_Ref_Bank.ForeColor = Color.Gray : Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = ""
            lbl_Ref_No.Text = "Ref. No.:" : lbl_Ref_No.ForeColor = Color.Red : Txt_Ref_No.Enabled = True : Txt_Ref_No.Text = "" : lbl_Ref_Title.Text = "Detail:"
            lbl_Ref_Date.Text = "Date:" : lbl_Ref_Date.ForeColor = Color.Red : Txt_Ref_Date.Text = "" : Txt_Ref_Date.Enabled = True
            lbl_Ref_Cdate.Text = "Clearing Date:" : lbl_Ref_Cdate.ForeColor = Color.Black : Txt_Ref_CDate.Text = "" : Txt_Ref_CDate.Enabled = True
            lbl_Ref_Branch.ForeColor = Color.Gray : Txt_Ref_Branch.Text = ""
        Else
            LayoutControlItem1.Control.Enabled = True
            LayoutControlItem3.Control.Enabled = True
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            End If
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Black : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Black : lbl_Acc_No.ForeColor = Color.Black
            lbl_Ref_Bank.ForeColor = Color.Red : lbl_Ref_Branch.ForeColor = Color.Red : lbl_Ref_Title.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red : lbl_Ref_Cdate.ForeColor = Color.Black
            Me.Txt_Slip_No.Enabled = True : Me.Txt_Slip_Count.Enabled = True
        End If
        Txt_Slip_No.Text = "" : Txt_Slip_Count.Text = ""
        If Cmd_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Cheque Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]"
        If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]"

        If Cmd_Mode.Text.ToUpper = "BANK ACCOUNT" Then
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:"
        End If
        If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "ECS" Then
            lbl_Ref_Title.Text = Cmd_Mode.Text.ToUpper & " Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Ref. No..." : lbl_Ref_Date.Text = "Date:"
        End If
        If Cmd_Mode.Text = "CBS" And (Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection) Then
            Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
            Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", Me.lbl_Ref_No.Tag)
            Me.GLookUp_RefBankList.EditValue = Me.lbl_Ref_No.Tag
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
            Me.Cmd_Mode.Focus()
        End If
        GLookUp_BankList_EditValueChanged(Nothing, Nothing)
    End Sub
    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_Mode.KeyDown
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
    Private Sub Txt_Slip_No_EditValueChanged(sender As Object, e As EventArgs) Handles Txt_Slip_No.EditValueChanged
        If Val(Me.Txt_Slip_No.Text) > 0 Then
            Me.Txt_Slip_Count.Text = Base._DepositSlipsDBOps.GetSlipTxnCount(Val(Me.Txt_Slip_No.Text), Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, Me.xMID.Text)
        Else
            Me.Txt_Slip_Count.Text = ""
        End If
    End Sub
#End Region

#Region "Start--> TextBox Events"

    Private Sub RAD_Receipt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RAD_Receipt.SelectedIndexChanged
        If RAD_Receipt.SelectedIndex = 2 Then
            Me.Txt_Amount.Text = "" : Me.Txt_Amount.Enabled = False
        Else
            Me.Txt_Amount.Enabled = True
        End If
        Calc_Diff()
    End Sub
    Private Sub RAD_Receipt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RAD_Receipt.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Txt_Amount_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.EditValueChanged, Txt_Slip_Count.EditValueChanged
        Calc_Diff()
    End Sub

    Private Sub Calc_Diff()
        If iLink_ID.ToString.Length > 0 And Val(Txt_Out_Standing.Text) > 0 Then
            If RAD_Receipt.SelectedIndex = 1 Or RAD_Receipt.SelectedIndex = 2 Then
                Me.Txt_Diff.Text = Val(Txt_Out_Standing.Text) - Val(Txt_Amount.Text)
            Else
                Me.Txt_Diff.Text = ""
            End If
        Else
            Me.Txt_Diff.Text = ""
        End If

    End Sub
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Ref_Branch.GotFocus, Txt_Slip_No.GotFocus, Txt_Slip_No.Click, Txt_Slip_Count.GotFocus, Txt_Slip_Count.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amount.KeyPress, Txt_Ref_No.KeyPress, Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress, Txt_Ref_Branch.KeyPress, Txt_Slip_No.KeyPress, Txt_Slip_Count.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_Ref_Branch.KeyDown, Txt_Slip_No.KeyDown, Txt_Slip_Count.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Ref_No.Validated, Txt_Narration.Validated, Txt_Reference.Validated, Txt_Remarks.Validated, Txt_Ref_Branch.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Receipt Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Receipt"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        GLookUp_RefBankList.Tag = "" : LookUp_GetRefBankList()
        GLookUp_PartyList1.Tag = "" : LookUp_GetPartyList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()
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
        Me.xMID.Text = Base._Rect_DBOps.GetMasterID(Me.xID.Text)
        Dim d1 As DataTable = Base._Rect_DBOps.GetMasterRecord(Me.xMID.Text)
        Dim d2 As DataTable = Base._Rect_DBOps.GetPurposeRecord(Me.xMID.Text)
        Dim d3 As DataTable = Base._Rect_DBOps.GetRecord(Me.xMID.Text)
        Dim d4 As DataTable = Base._Rect_DBOps.GetSlipRecord(Me.xMID.Text)
        Dim d5 As DataTable = Nothing
        If Not d4 Is Nothing Then
            If d4.Rows.Count > 0 Then
                d5 = Base._Voucher_DBOps.GetSlipMAsterRecord(d4.Rows(0)("TR_SLIP_ID").ToString)
            End If
        End If
       
        If d1 Is Nothing Or d2 Is Nothing Or d4 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If d3.Rows.Count = 0 Then d3 = Base._Rect_DBOps.GetJournalRecord(Me.xMID.Text)
        Dim xDate As DateTime = Nothing
        xDate = d3.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Receipt", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Txt_V_NO.DataBindings.Add("TEXT", d3, "TR_VNO")
    
        Cmd_Mode.DataBindings.Add("text", d3, "TR_MODE")

        If Not IsDBNull(d3.Rows(0)("TR_ITEM_ID")) Then
            If d3.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d3.Rows(0)("TR_ITEM_ID"))
                Me.GLookUp_ItemList.EditValue = d3.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_ItemList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_AB_ID_1")) Then
            If d3.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", d3.Rows(0)("TR_AB_ID_1")))
                Me.GLookUp_PartyList1.EditValue = d3.Rows(0)("TR_AB_ID_1")
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PartyList1.Properties.ReadOnly = False


        Txt_Amount.DataBindings.Add("EditValue", d1, "TR_SUB_AMT")

        If Not IsDBNull(d3.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
            If d3.Rows(0)("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                Me.GLookUp_Adjustment.ShowPopup() : Me.GLookUp_Adjustment.ClosePopup()
                Me.GLookUp_AdjustmentView.MoveBy(Me.GLookUp_AdjustmentView.LocateByValue("REF_ID", d3.Rows(0)("TR_TRF_CROSS_REF_ID")))
                Me.GLookUp_Adjustment.EditValue = d3.Rows(0)("TR_TRF_CROSS_REF_ID")
                Me.GLookUp_Adjustment.Tag = Me.GLookUp_Adjustment.EditValue
                Me.GLookUp_Adjustment.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_Adjustment.Properties.ReadOnly = False

        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If (Not IsDBNull(d3.Rows(0)("TR_SUB_CR_LED_ID")) And IIf(IsDBNull(d3.Rows(0)("TR_CR_LED_ID")), "", d3.Rows(0)("TR_CR_LED_ID")) = "00079") Then Bank_ID = d3.Rows(0)("TR_SUB_CR_LED_ID")
        Else
            If Not IsDBNull(d3.Rows(0)("TR_SUB_DR_LED_ID")) And IIf(IsDBNull(d3.Rows(0)("TR_DR_LED_ID")), "", d3.Rows(0)("TR_DR_LED_ID")) = "00079" Then Bank_ID = d3.Rows(0)("TR_SUB_DR_LED_ID")
        End If

        If Bank_ID.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID)
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_REF_BANK_ID")) Then
            If d3.Rows(0)("TR_REF_BANK_ID").ToString.Length > 0 Then
                Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
                Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", d3.Rows(0)("TR_REF_BANK_ID"))
                Me.GLookUp_RefBankList.EditValue = d3.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankList.EditValue
                Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_RefBankList.Properties.ReadOnly = False

        Txt_Ref_Branch.DataBindings.Add("text", d3, "TR_REF_BRANCH")
        Txt_Ref_No.DataBindings.Add("text", d3, "TR_REF_NO")
        If Not d5 Is Nothing Then Txt_Slip_No.DataBindings.Add("text", d5, "SL_NO")
        If Not IsDBNull(d3.Rows(0)("TR_REF_DATE")) Then
            xDate = d3.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d3.Rows(0)("TR_REF_CDATE")) Then
            xDate = d3.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If


        If Not IsDBNull(d2.Rows(0)("TR_PURPOSE_MISC_ID")) Then
            If d2.Rows(0)("TR_PURPOSE_MISC_ID").ToString.Length > 0 Then
                Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", d2.Rows(0)("TR_PURPOSE_MISC_ID")))
                Me.GLookUp_PurList.EditValue = d2.Rows(0)("TR_PURPOSE_MISC_ID")
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                Me.GLookUp_PurList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PurList.Properties.ReadOnly = False


        Txt_Narration.DataBindings.Add("text", d3, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d3, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d3, "TR_REFERENCE")
        RAD_Receipt.DataBindings.Add("SelectedIndex", d1, "TR_RECEIPT_TYPE")
        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList1.Enabled = False : Me.GLookUp_PartyList1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_City.Enabled = False : Me.BE_City.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_PAN_No.Enabled = False : Me.BE_PAN_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_Adjustment.Enabled = False : Me.GLookUp_Adjustment.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.RAD_Receipt.Enabled = False : Me.RAD_Receipt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Out_Standing.Enabled = False : Me.Txt_Out_Standing.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Diff.Enabled = False : Me.Txt_Diff.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_RefBankList.Enabled = False : Me.GLookUp_RefBankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Branch.Enabled = False : Me.Txt_Ref_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor

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
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Slip_No.Enabled = False : Me.Txt_Slip_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_PersAdd.Enabled = False
        Me.But_PersManage.Enabled = False
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

                iParty_Req = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PARTY_REQ").ToString
                If iParty_Req.ToString.ToUpper = "YES" Then lbl_Party.ForeColor = Color.Red Else lbl_Party.ForeColor = Color.Black

                iProfile = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PROFILE").ToString

                iCond_Ledger_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_LED_ID").ToString
                iMinValue = Val(Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_MIN_VALUE").ToString)
                iMaxValue = Val(Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_MAX_VALUE").ToString)
                iMaxValue = Val(Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_MAX_VALUE").ToString)
                iTDS_CODE = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TDS_CODE").ToString

                iLink_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LINK_REC_ID").ToString
                iOffSet_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_OFFSET_REC_ID").ToString
                iOffSet_Item = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_OFFSET_NAME").ToString

                RAD_Receipt.SelectedIndex = 0 : Me.RAD_Receipt.Enabled = True
                GLookUp_PartyList1.Tag = "" : GLookUp_PartyList1.EditValue = ""
                GLookUp_Adjustment.Tag = "" : GLookUp_Adjustment.EditValue = ""
                LayoutControlItem11.Control.Enabled = False : Txt_Out_Standing.Text = "" : Me.Txt_Diff.Text = "" : Me.lbl_Diff.Text = "Difference:"
                Me.Txt_Amount.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._Rect_DBOps.GetLedgerItems(Base.Is_HQ_Centre)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    '2.GLookUp_BankList
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
            Me.Cmd_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Mode.Focus()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0 And Cmd_Mode.Text.ToUpper <> "CASH") Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.BE_Bank_Branch.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.BE_Bank_Acc_No.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
                Me.BE_Bank_Acc_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BI_SHORT_NAME").ToString
                Me.lbl_Ref_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ID").ToString
                Me.Txt_Slip_No.Text = Base._DepositSlipsDBOps.GetMaxOpenSlipNo(Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt)
                Me.Txt_Slip_No.Enabled = True
                Me.Txt_Slip_Count.Text = Base._DepositSlipsDBOps.GetSlipTxnCount(Val(Me.Txt_Slip_No.Text), Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Receipt, Me.xMID.Text)
            Else
                Me.Txt_Slip_No.Enabled = False
            End If
        Else
            Me.Txt_Slip_No.Enabled = False
        End If
    End Sub
    Private Sub GLookUp_BankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_BankList_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_BankList_FilterLookup(sender As Object)
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
        Dim BA_Table As DataTable = Base._Rect_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Rect_DBOps.GetBranchDetails(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                                        .BANK_ID = B.Field(Of String)("BANK_ID"), _
                                        .REC_EDIT_ON = A.Field(Of DateTime)("REC_EDIT_ON")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            Me.Cmd_Mode.Enabled = False
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        If Final_Data.Count = 1 Then
            Cnt_BankAccount = 1
            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray

            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False
        Else
            Cnt_BankAccount = Final_Data.Count
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
        End If

        If SelectedBankID.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", SelectedBankID)
            Me.GLookUp_BankList.EditValue = SelectedBankID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        If SelectedDepositSlipNo > 0 Then
            Me.Txt_Slip_No.Text = SelectedDepositSlipNo
        End If
    End Sub

    '3.GLookUp_RefBankList
    Private Sub GLookUp_RefBankList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_RefBankList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_RefBankListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_RefBankListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_RefBankList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_RefBankListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_RefBankListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_RefBankList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_RefBankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_RefBankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_RefBankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_RefBankList.CancelPopup()
            Hide_Properties()
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_RefBankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
        End If

    End Sub
    Private Sub GLookUp_RefBankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_RefBankList.EditValueChanged
        If Me.GLookUp_RefBankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_RefBankListView.RowCount > 0 And Val(Me.GLookUp_RefBankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankListView.GetRowCellValue(Me.GLookUp_RefBankListView.FocusedRowHandle, "BI_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_RefBankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_RefBankList.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_RefBankList_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_RefBankList_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BI_BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BI_SHORT_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetRefBankList()
        'bank
        Dim B2 As DataTable = Base._Rect_DBOps.GetBanks()
        If B2 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(B2)
        If dview.Count > 0 Then
            Me.GLookUp_RefBankList.Properties.ValueMember = "BI_ID"
            Me.GLookUp_RefBankList.Properties.DisplayMember = "BI_BANK_NAME"
            Me.GLookUp_RefBankList.Properties.DataSource = dview
            Me.GLookUp_RefBankListView.RefreshData()
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_RefBankList.Properties.Tag = "NONE"
        End If

        If SelectedRefBankID.Length > 0 Then
            Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
            Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", SelectedRefBankID)
            Me.GLookUp_RefBankList.EditValue = SelectedRefBankID
            Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankList.EditValue
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_RefBankList.Properties.ReadOnly = False
    End Sub

    '4.GLookUp_PartyList1
    Private Sub GLookUp_PartyList1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList1.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList1.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList1.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList1.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList1.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_PartyList1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList1View.RowCount > 0 And Val(Me.GLookUp_PartyList1View.FocusedRowHandle) >= 0) Then
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_ID").ToString
                Me.BE_PAN_No.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_PAN_NO").ToString
                Me.BE_City.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_CITY").ToString

                RAD_Receipt.SelectedIndex = 0 : Me.RAD_Receipt.Enabled = True
                LayoutControlItem11.Control.Enabled = False
                Me.GLookUp_Adjustment.Tag = "" : GLookUp_Adjustment.EditValue = "" : Txt_Out_Standing.Text = "" : Me.Txt_Diff.Text = "" : Me.lbl_Diff.Text = "Difference:"
                Me.Txt_Amount.Text = ""
                If iLink_ID.ToString.Length > 0 Then
                    If iProfile = "OTHER DEPOSITS" Then
                        Party_Outstanding_Deposits(Me.GLookUp_PartyList1.Tag)
                    End If
                    If iProfile = "ADVANCES" Then
                        RAD_Receipt.SelectedIndex = 1 : Me.RAD_Receipt.Enabled = False
                        Party_Outstanding_Advances(Me.GLookUp_PartyList1.Tag)

                    End If
                End If
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_PartyList1.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_PartyList_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_PartyList_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator(CriteriaOperator.Parse("Replace(Replace(Replace(Replace([C_NAME],'.',''),' ',''),',',''),'-','')", Nothing), "%" + edit.AutoSearchText.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("-", "") + "%", BinaryOperatorType.Like)
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._Rect_DBOps.GetParties()
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("C_NAME") = "" : d1.Rows.Add(ROW)

        Dim dview As New DataView(d1) : dview.Sort = "C_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_PartyList1.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList1.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList1.Properties.DataSource = dview
            Me.GLookUp_PartyList1View.RefreshData()
            Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            Me.GLookUp_PartyList1.Text = ""
        Else
            Me.GLookUp_PartyList1.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList1.Properties.ReadOnly = False
    End Sub

    Private Sub Party_Outstanding_Deposits(ByVal xParty_ID As String)
        'Deposit Table...
        Dim RecDeposits As Common_Lib.RealTimeService.Param_GetReceiptDeposits = New Common_Lib.RealTimeService.Param_GetReceiptDeposits
        RecDeposits.Dep_ItemID = iLink_ID
        RecDeposits.Dep_PartyID = xParty_ID
        RecDeposits.Next_YearID = Base._next_Unaudited_YearID
        RecDeposits.Prev_YearId = Base._prev_Unaudited_YearID
        RecDeposits.TR_M_ID = Me.xMID.Text
        Dim OS_TABLE As DataTable = Base._Rect_DBOps.GetPendingDeposits(RecDeposits) ' Base._Rect_DBOps.GetPendingDeposits(xParty_ID, iLink_ID)
        If OS_TABLE Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'Dim AdjustedDeposits As DataTable = New DataTable
        'If OS_TABLE.Rows.Count > 0 Then
        '    AdjustedDeposits = GetDepositAdjustments(xParty_ID, OS_TABLE)
        'End If

        ''Delete Out-Standing Zero......................................
        'Dim _Temp As DataTable = New DataTable : Dim xNrow As DataRow
        'With _Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
        'For Each XRow In AdjustedDeposits.Rows
        '    If XRow("REF_OUTSTAND") = 0 Then xNrow = _Temp.NewRow : xNrow("_Rid") = AdjustedDeposits.Rows.IndexOf(XRow) : _Temp.Rows.Add(xNrow)
        'Next
        'For Each XRow In _Temp.Rows : AdjustedDeposits.Rows(XRow("_Rid")).Delete() : Next
        Dim dview As New DataView(OS_TABLE) : If dview.Table.Rows.Count > 0 Then dview.Sort = "REF_DATE"
        GLookUp_Adjustment_List(dview)
    End Sub

    'Private Function GetDepositAdjustments(xParty_ID As String, OS_TABLE As DataTable) As DataTable

    '    'Transaction Table...
    '    Dim _TR_TABLE As DataTable = Nothing
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, False, Base._prev_Unaudited_YearID, xMID.Text) 'gets prev year refunds 
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, False, Base._open_Year_ID, xMID.Text, Base._next_Unaudited_YearID)
    '        End If
    '    Else
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, True, Base._prev_Unaudited_YearID)
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, True, Base._open_Year_ID, Nothing, Base._next_Unaudited_YearID)
    '        End If
    '    End If

    '    If _TR_TABLE Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If

    '    'Journal Adjustments 
    '    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only
    '    param.NextUnauditedYearID = Base._next_Unaudited_YearID
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADJ_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '    'Journal Additions 
    '    param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADDITION_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '    'Item Table...
    '    Dim ITEM_Table As DataTable = Base._Rect_DBOps.GetItemsListByID(iLink_ID)
    '    If ITEM_Table Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If
    '    'Relationship...
    '    '1. 
    '    Dim JointData As DataSet = New DataSet()
    '    JointData.Tables.Add(OS_TABLE.Copy)
    '    JointData.Tables.Add(ITEM_Table.Copy)
    '    Dim Item_Relation1 As DataRelation = JointData.Relations.Add("Item_1", JointData.Tables("DEPOSITS_INFO").Columns("REF_ITEM_ID"), JointData.Tables("ITEM_INFO").Columns("ITEM_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each Item_Row In XRow.GetChildRows(Item_Relation1) : XRow("REF_ITEM") = Item_Row("ITEM_NAME") : Next : Next
    '    '2
    '    JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation) : XRow("REF_REFUND") = _Row("Refund") : Next : Next
    '    '3
    '    JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation As DataRelation = JointData.Relations.Add("Journal_Adjustment", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation) : XRow("REF_ADJUSTED") = Val(XRow("REF_ADJUSTED")) + Val(_Row("AMOUNT")) : Next : Next
    '    '4
    '    JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation) : XRow("REF_ADDITION") = _Row("AMOUNT") : Next : Next


    '    'Clear Relations
    '    JointData.Relations.Clear()

    '    JointData.Tables.RemoveAt(4) 'Remove Previously added Additions table 
    '    JointData.Tables.RemoveAt(3) 'Remove Previously added Adjustments table 
    '    JointData.Tables.RemoveAt(2) 'Remove Previously added Deposit Refund table

    '    'Updating Out-Standing...
    '    Dim xCnt As Integer = 1
    '    For Each XRow In JointData.Tables(0).Rows
    '        XRow("REF_OUTSTAND") = XRow("REF_AMT") + XRow("REF_ADDITION") - (XRow("REF_REFUND") + XRow("REF_ADJUSTED")) : XRow("Sr") = xCnt : xCnt += 1
    '    Next

    '    'Changes for Year Ending Process
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        'Make Prev Year Closing as Current Year Opening
    '        For Each XRow In JointData.Tables(0).Rows
    '            XRow("REF_ADDITION") = 0
    '            XRow("REF_ADJUSTED") = 0
    '            XRow("REF_REFUND") = 0
    '            XRow("REF_AMT") = XRow("REF_OUTSTAND")
    '        Next

    '        'Current Year Payments of Deposits 
    '        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, False, Base._open_Year_ID, xMID.Text)
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetDepositsRefundList(xParty_ID, True, Base._open_Year_ID)
    '        End If

    '        If _TR_TABLE Is Nothing Then
    '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Return Nothing
    '        End If

    '        Dim param1 As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param1.CrossRefId = Nothing
    '        param1.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only
    '        param1.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADJ_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param1, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '        'Journal Additions 
    '        param1 = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param1.CrossRefId = Nothing
    '        param1.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only
    '        param1.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADDITION_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param1, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)


    '        'Show payments 
    '        JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation1) : XRow("REF_REFUND") = _Row("Refund") : Next : Next
    '        'Adj
    '        JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation1 As DataRelation = JointData.Relations.Add("Journal_Adjustment", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation1) : XRow("REF_ADJUSTED") = Val(XRow("REF_ADJUSTED")) + Val(_Row("AMOUNT")) : Next : Next
    '        'Add
    '        JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation1 As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("DEPOSITS_INFO").Columns("REF_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation1) : XRow("REF_ADDITION") = _Row("AMOUNT") : Next : Next

    '        'Update Outstanding Amounts
    '        For Each XRow In JointData.Tables(0).Rows
    '            XRow("REF_OUTSTAND") = XRow("REF_AMT") + XRow("REF_ADDITION") - (XRow("REF_REFUND") + XRow("REF_ADJUSTED"))
    '        Next
    '    End If
    '    Return JointData.Tables(0)
    'End Function

    'PARTY OUTSTANDING - ADVANCES
    Private Sub Party_Outstanding_Advances(ByVal xParty_ID As String)
        'Advance Table...
        Dim RecptParam As Common_Lib.RealTimeService.Param_GetReceiptAdvances = New Common_Lib.RealTimeService.Param_GetReceiptAdvances()
        RecptParam.Advance_ItemID = iLink_ID
        RecptParam.Advance_PartyID = xParty_ID
        RecptParam.Next_YearID = Base._next_Unaudited_YearID
        RecptParam.Prev_YearId = Base._prev_Unaudited_YearID
        RecptParam.Tr_M_Id = Me.xMID.Text
        Dim OS_TABLE As DataTable = Base._Rect_DBOps.GetAdvances(RecptParam) 'Base._Rect_DBOps.GetPendingAdvances(xParty_ID, iLink_ID)
        'If OS_TABLE Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If
        'Dim JointData As DataSet = GetAdvanceAdjustments(xParty_ID, OS_TABLE)
        ''Delete Out-Standing Zero......................................
        'Dim _Temp As DataTable = New DataTable : Dim xNrow As DataRow
        'With _Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
        'For Each XRow In JointData.Tables(0).Rows
        '    If XRow("REF_OUTSTAND") = 0 Then xNrow = _Temp.NewRow : xNrow("_Rid") = JointData.Tables(0).Rows.IndexOf(XRow) : _Temp.Rows.Add(xNrow)
        'Next
        'For Each XRow In _Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next
        Dim dview As New DataView(OS_TABLE) : dview.Sort = "REF_DATE"
        GLookUp_Adjustment_List(dview)

    End Sub

    'Private Function GetAdvanceAdjustments(xParty_ID As String, OS_TABLE As DataTable) As DataSet

    '    'Journal Adjustments 
    '    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    param.NextUnauditedYearID = Base._next_Unaudited_YearID
    '    Dim JOURNAL_ADJ_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '    'Journal Additions 
    '    param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADDITION_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '    'Transaction Table...
    '    Dim _TR_TABLE As DataTable = Nothing
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, False, Base._prev_Unaudited_YearID, xMID.Text) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, False, Base._open_Year_ID, xMID.Text, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    Else
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, True, Base._prev_Unaudited_YearID) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, True, Base._open_Year_ID, Nothing, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    End If

    '    If _TR_TABLE Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If
    '    'Item Table...
    '    Dim ITEM_Table As DataTable = Base._Rect_DBOps.GetItemsListByID(iLink_ID)
    '    If ITEM_Table Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If

    '    'Relationship...
    '    '1. 
    '    Dim JointData As DataSet = New DataSet()
    '    JointData.Tables.Add(OS_TABLE.Copy)
    '    JointData.Tables.Add(ITEM_Table.Copy)
    '    Dim Item_Relation1 As DataRelation = JointData.Relations.Add("Item_1", JointData.Tables("ADVANCES_INFO").Columns("REF_ITEM_ID"), JointData.Tables("ITEM_INFO").Columns("ITEM_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each Item_Row In XRow.GetChildRows(Item_Relation1) : XRow("REF_ITEM") = Item_Row("ITEM_NAME") : Next : Next
    '    '2
    '    JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation) : XRow("REF_REFUND") = _Row("Refund") : XRow("REF_ADJUSTED") = _Row("Adjusted") : Next : Next
    '    '3
    '    JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation As DataRelation = JointData.Relations.Add("journal_Adjustment", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation) : XRow("REF_ADJUSTED") = Val(XRow("REF_ADJUSTED")) + Val(_Row("AMOUNT")) : Next : Next
    '    '4
    '    JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation) : XRow("REF_ADDITION") = _Row("AMOUNT") : Next : Next

    '    'Clear Relations
    '    JointData.Relations.Clear()

    '    'Updating Out-Standing...
    '    Dim xCnt As Integer = 1
    '    For Each XRow In JointData.Tables(0).Rows : XRow("REF_OUTSTAND") = XRow("REF_AMT") + XRow("REF_ADDITION") - (XRow("REF_REFUND") + XRow("REF_ADJUSTED")) : XRow("Sr") = xCnt : xCnt += 1 : Next

    '    JointData.Tables.RemoveAt(4) 'Remove Previously added Additions table 
    '    JointData.Tables.RemoveAt(3) 'Remove Previously added Adjustments table 
    '    JointData.Tables.RemoveAt(2) 'Remove Previously added Deposit Payment table 

    '    'Changes for Year Ending Process
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        'Make Prev Year Closing as Current Year Opening
    '        For Each XRow In JointData.Tables(0).Rows
    '            XRow("REF_ADDITION") = 0 : XRow("REF_ADJUSTED") = 0 : XRow("REF_REFUND") = 0 : XRow("REF_AMT") = XRow("REF_OUTSTAND")
    '        Next

    '        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, False, Base._open_Year_ID, xMID.Text) 'Get Current Year Payments 
    '        Else
    '            _TR_TABLE = Base._Rect_DBOps.GetAdvancesRefundList(xParty_ID, True, Base._open_Year_ID) 'Get Current Year Payments 
    '        End If

    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADJ_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '        'Journal Additions 
    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADDITION_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '        'payment
    '        JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation1) : XRow("REF_REFUND") = _Row("Refund") : XRow("REF_ADJUSTED") = _Row("Adjusted") : Next : Next
    '        'adjustment
    '        JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation1 As DataRelation = JointData.Relations.Add("journal_Adjustment", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation1) : XRow("REF_ADJUSTED") = Val(XRow("REF_ADJUSTED")) + Val(_Row("AMOUNT")) : Next : Next
    '        'addition
    '        JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation1 As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("ADVANCES_INFO").Columns("REF_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation1) : XRow("REF_ADDITION") = _Row("AMOUNT") : Next : Next
    '        JointData.Relations.Clear()

    '        'Updating Out-Standing...
    '        For Each XRow In JointData.Tables(0).Rows : XRow("REF_OUTSTAND") = XRow("REF_AMT") + XRow("REF_ADDITION") - (XRow("REF_REFUND") + XRow("REF_ADJUSTED")) : Next
    '    End If
    '    Return JointData
    'End Function

    '5.GLookUp_Adjustment
    Private Sub GLookUp_Adjustment_List(ByVal xDView As DataView)
        If xDView.Count > 0 Then
            Me.GLookUp_Adjustment.Properties.ValueMember = "REF_ID"
            Me.GLookUp_Adjustment.Properties.DisplayMember = "REF_AMT"
            Me.GLookUp_Adjustment.Properties.DataSource = xDView
            Me.GLookUp_AdjustmentView.RefreshData()
            Me.GLookUp_Adjustment.Properties.Tag = "SHOW"

            LayoutControlItem11.Control.Enabled = True
            lbl_Adjust_Title.ForeColor = Color.Black
            lbl_Payment.ForeColor = Color.Red

            lbl_Out_Standing.ForeColor = Color.Black
            lbl_Diff.ForeColor = Color.Blue
            If RAD_Receipt.Enabled Then
                lbl_Receipt.ForeColor = Color.Black
            Else
                lbl_Receipt.ForeColor = Color.DimGray
            End If
            Me.GLookUp_Adjustment.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy

            Me.Txt_Out_Standing.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Else
            LayoutControlItem11.Control.Enabled = False
            lbl_Adjust_Title.ForeColor = Color.DimGray
            lbl_Payment.ForeColor = Color.DimGray

            lbl_Out_Standing.ForeColor = Color.DimGray
            lbl_Diff.ForeColor = Color.DimGray
            lbl_Receipt.ForeColor = Color.DimGray

            Me.GLookUp_Adjustment.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray

            Me.Txt_Out_Standing.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.GLookUp_Adjustment.Tag = "" : GLookUp_Adjustment.EditValue = "" : Txt_Out_Standing.Text = ""

            Me.GLookUp_Adjustment.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_Adjustment.Properties.ReadOnly = False
    End Sub
    Private Sub GLookUp_Adjustment_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_Adjustment.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_AdjustmentView.RowCount > 0 And Val(Me.GLookUp_AdjustmentView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_Adjustment.Tag = Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_ID").ToString
                Me.Txt_Out_Standing.Text = Val(Me.GLookUp_AdjustmentView.GetRowCellValue(Me.GLookUp_AdjustmentView.FocusedRowHandle, "REF_OUTSTAND").ToString)

                If iOffSet_ID.ToString.Length > 0 Then
                    lbl_Diff.Text = iOffSet_Item & ":"
                Else
                    lbl_Diff.Text = "Balance:"
                End If

                Calc_Diff()
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_FilterCriteria_GLookUp_Adjustment(sender As Object, e As ChangingEventArgs) Handles GLookUp_Adjustment.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("REF_ITEM", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("REF_AMT", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("REF_PURPOSE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("REF_OTHER_DETAIL", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub

    '6.GLookUp_PurList
    Private Sub GLookUp_PurList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PurList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PurList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PurList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PurList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PurList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PurList.CancelPopup()
            Hide_Properties()
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
        End If

    End Sub
    Private Sub GLookUp_PurList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PurList.EditValueChanged
        If Me.GLookUp_PurList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_PurListView.RowCount > 0 And Val(Me.GLookUp_PurListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurListView.GetRowCellValue(Me.GLookUp_PurListView.FocusedRowHandle, "PUR_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetPurposeList()
        Dim d1 As DataTable = Base._Rect_DBOps.GetPurposes()
        Dim dview As New DataView(d1)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dview.Count > 0 Then
            Me.GLookUp_PurList.Properties.ValueMember = "PUR_ID"
            Me.GLookUp_PurList.Properties.DisplayMember = "PUR_NAME"
            Me.GLookUp_PurList.Properties.DataSource = dview
            Me.GLookUp_PurListView.RefreshData()
            Me.GLookUp_PurList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PurList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            ''1
            'Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
            'Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
            'Me.GLookUp_PurList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            'Me.GLookUp_PurList.Properties.Tag = "SHOW"
            Me.GLookUp_PurList.Properties.ReadOnly = False
        End If

    End Sub

#End Region

   
End Class