Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraPrinting
Imports System.Reflection
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class Frm_Voucher_Info

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    '
    Private Get_Voucher_Type As String = ""
    Public Voucher_Type As String
    Public Sel_Led_Name As String
    Public Sel_Led_ID As String
    Private Sel_v_Date As DateTime
    Private Sel_Pay_Bank_ID As String
    Public Selection_By_Item As Boolean = False

    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Private xID As String = "" : Private xMID As String = ""
    Private xFr_Date As Date = Nothing : Private xTo_Date As Date = Nothing
    Private Negative_MsgStr As String = ""

    Private Open_Cash_Bal, Open_Bank_Bal As Double
    Private Close_Cash_Bal, Close_Bank_Bal As Double

    Private Summary_Column_Status As Boolean = False
    Public Sub New()


        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Voucher Entry" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub
    Private Sub Form_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        Button_Panel.Left = ((Me.Width / 2) - (Me.Button_Panel.Width / 2)) + 2
        Button_Panel.Top = ((Me.Height / 2) - (Me.Button_Panel.Height / 2)) + 15
    End Sub
    Private Sub Form__Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        Me.GLookUp_ItemList.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Shift Or Keys.F6)) Then ' CALCULATOR
            Try
                Dim xfrm As New D0006.Frm_Calculator
                xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                xfrm.Show()
                '                MsgBox(Val(xfrm.txtInput.Text))
            Catch ex As Exception
            End Try
            Return (True)
        End If

        If (keyData = (Keys.Alt Or Keys.F2)) Then 'CHANGE PERIOD
            Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = 20
            Return (True)
        End If


        'If (keyData = (Keys.Shift Or Keys.C)) Then Voucher_Type = "CASH" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.B)) Then Voucher_Type = "BANK" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.D)) Then
        '    If Base.Allow_Foreign_Donation Then
        '        Dim xfrm As New Frm_Voucher_Win_D_Type : xfrm.Text = "Donation" : xfrm.ShowDialog(Me)
        '        If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
        '            Dim xKind As String = xfrm.RadioGroup1.Text : xfrm.Dispose()
        '            If xKind.ToString.ToUpper = "DONATION - REGULAR" Then Voucher_Type = "DONATION - REGULAR"
        '            If xKind.ToString.ToUpper = "DONATION - FOREIGN" Then Voucher_Type = "DONATION - FOREIGN"
        '        Else
        '            xfrm.Dispose()
        '        End If
        '    Else
        '        Voucher_Type = "DONATION - REGULAR"
        '    End If
        '    Me.DataNavigation("NEW") : Return (True)
        'End If
        'If (keyData = (Keys.Shift Or Keys.X)) Then Voucher_Type = "COLLECTION BOX" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.P)) Then Voucher_Type = "PAYMENT" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.R)) Then Voucher_Type = "RECEIPTS" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.K)) Then Voucher_Type = "DONATION - GIFT" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.T)) Then Voucher_Type = "INTERNAL TRANSFER" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.F)) Then Voucher_Type = "FD" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.S)) Then Voucher_Type = "SALE OF ASSET" : Me.DataNavigation("NEW") : Return (True)
        'If (keyData = (Keys.Shift Or Keys.N)) Then BUT_NB_Entry_Click(Nothing, Nothing) : Return (True)
        'If (keyData = (Keys.Shift Or Keys.O)) Then BUT_CashBook_Click(Nothing, Nothing) : Return (True)


        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_CLOSE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("I t e m   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
            Me.GLookUp_ItemList.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        End If
        Selection_By_Item = True : Voucher_Type = ""
        Select Case Get_Voucher_Type.ToUpper
            Case "CASH DEPOSITED", "CASH WITHDRAWN"
                Voucher_Type = "CASH"
            Case "BANK TRANSFER"
                Voucher_Type = "BANK"
            Case "DONATION"
                Voucher_Type = "DONATION - REGULAR"
            Case "DONATION - FOREIGN"
                Voucher_Type = "DONATION - FOREIGN"
            Case "COLLECTION BOX"
                Voucher_Type = "COLLECTION BOX"
            Case "PAYMENT", "PAYMENT - INSTITUTE", "LAND & BUILDING / GIFT", "LAND & BUILDING"
                Voucher_Type = "PAYMENT"
            Case "RECEIPTS", "RECEIPTS - INSTITUTE"
                Voucher_Type = "RECEIPTS"
            Case "DONATION - GIFT"
                Voucher_Type = "DONATION - GIFT"
            Case "INTERNAL TRANSFER", "INTERNAL TRANSFER - INSTITUTE", "INTERNAL TRANSFER WITH H.Q."
                Voucher_Type = "INTERNAL TRANSFER"
            Case "FD"
                Voucher_Type = "FD"
            Case "SALE OF ASSET"
                Voucher_Type = "SALE OF ASSET"
            Case "MEMBERSHIP"
                Voucher_Type = "MEMBERSHIP"
            Case "MEMBERSHIP RENEWAL"
                Voucher_Type = "MEMBERSHIP RENEWAL"
            Case "ASSET TRANSFER"
                Voucher_Type = "ASSET TRANSFER"
            Case Else
                DevExpress.XtraEditors.XtraMessageBox.Show("C o m i n g   S o o n . . . !", "Voucher Entry...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
        If Voucher_Type.Length > 0 Then DataNavigation("NEW")
    End Sub
    Private Sub All_Voucher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CASH.Click, BUT_BANK.Click, BUT_PAYMENT.Click, BUT_RECEIPT.Click, BUT_DONATION.Click, BUT_FD.Click, BUT_INT_TRANSFER.Click, BUT_C_BOX.Click, BUT_J_Entry.Click, BUT_ASSET_TFER.Click, BUT_GIFT.Click, BUT_S_ASSET.Click, BUT_WIP_FINAL.Click, BUT_CASH_PAY_VOLUME.Click, BUT_BANK_PAY_VOLUME.Click, BUT_CONTRA_VOLUME.Click, BUT_JV_VOLUME.Click
        Dim btn As SimpleButton : btn = CType(sender, SimpleButton)
        Selection_By_Item = False : Voucher_Type = ""
        Dim AdditionalParam As String = ""
        Select Case btn.Name
            'Volume Center Specific
            Case BUT_CASH_PAY_VOLUME.Name
                Voucher_Type = "PAYMENT" : AdditionalParam = "CASH"
            Case BUT_BANK_PAY_VOLUME.Name
                Voucher_Type = "PAYMENT" : AdditionalParam = "BANK"
            Case BUT_CONTRA_VOLUME.Name
                Voucher_Type = "JOURNAL"
            Case BUT_JV_VOLUME.Name
                Voucher_Type = "PAYMENT" : AdditionalParam = "JV"
                'END:Volume Center Specific
            Case BUT_CASH.Name
                Voucher_Type = "CASH"
            Case BUT_BANK.Name
                Voucher_Type = "BANK"
            Case BUT_DONATION.Name
                If Base.Allow_Foreign_Donation Then
                    Dim xfrm As New Frm_Voucher_Win_D_Type : xfrm.Text = "Donation" : xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim xKind As String = xfrm.RadioGroup1.Text : xfrm.Dispose()
                        If xKind.ToString.ToUpper = "DONATION - REGULAR" Then Voucher_Type = "DONATION - REGULAR"
                        If xKind.ToString.ToUpper = "DONATION - FOREIGN" Then Voucher_Type = "DONATION - FOREIGN"
                    Else
                        xfrm.Dispose()
                    End If
                Else
                    Voucher_Type = "DONATION - REGULAR"
                End If
            Case BUT_C_BOX.Name
                Voucher_Type = "COLLECTION BOX"
            Case BUT_PAYMENT.Name
                Voucher_Type = "PAYMENT"
            Case BUT_RECEIPT.Name
                Voucher_Type = "RECEIPTS"
            Case BUT_GIFT.Name
                Voucher_Type = "DONATION - GIFT"
            Case BUT_INT_TRANSFER.Name
                Voucher_Type = "INTERNAL TRANSFER"
            Case BUT_FD.Name
                Voucher_Type = "FD"
            Case BUT_S_ASSET.Name
                Voucher_Type = "SALE OF ASSET"
            Case BUT_J_Entry.Name
                Voucher_Type = "JOURNAL"
            Case BUT_ASSET_TFER.Name
                Voucher_Type = "ASSET TRANSFER"
            Case BUT_WIP_FINAL.Name
                Voucher_Type = "WIP FINALIZATION"
            Case Else
                DevExpress.XtraEditors.XtraMessageBox.Show("C o m i n g   S o o n . . . !", "Voucher Entry...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
        If Voucher_Type.Length > 0 Then DataNavigation("NEW", AdditionalParam)
    End Sub
    Private Sub BUT_CashBook_Click(sender As System.Object, e As System.EventArgs) Handles BUT_CashBook.Click
        If OpenCashBook = False Then
            Dim xfrm As New Frm_Voucher_Info_CB : xfrm.MainBase = Base : xfrm.MdiParent = Me.MdiParent : xfrm.Show()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show("C a s h   B o o k   a l r e a d y   o p e n e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.GLookUp_ItemList.EditValue = "" : Me.GLookUp_ItemList.Text = "" : Me.GLookUp_ItemList.Focus()
    End Sub
    Private Sub BUT_NB_Entry_Click(sender As System.Object, e As System.EventArgs) Handles BUT_NB_Entry.Click
        Dim xFrm As New Frm_NoteBook_Info : xFrm.MainBase = Base : xFrm.Tag = Common_Lib.Common.Navigation_Mode._New : xFrm.ShowDialog(Me) : xFrm.Dispose()
        Me.GLookUp_ItemList.EditValue = "" : Me.GLookUp_ItemList.Text = "" : Me.GLookUp_ItemList.Focus()
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CLOSE
        Fill_Change_Period_Items()

        Dim MaxValue As Object = 0 : Dim xLastDate As Date = Now.Date
        MaxValue = Base._Voucher_DBOps.GetMaxTransactionDate()
        If IsDBNull(MaxValue) Then xLastDate = Base._open_Year_Sdt Else xLastDate = MaxValue

        Dim xMM As Integer = xLastDate.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))

        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()

        If Base._IsVolumeCenter Then
            BUT_CASH.Visible = False : BUT_ASSET_TFER.Visible = False : BUT_BANK.Visible = False : BUT_C_BOX.Visible = False : BUT_CASH.Visible = False : BUT_DONATION.Visible = False : BUT_FD.Visible = False : BUT_GIFT.Visible = False : BUT_INT_TRANSFER.Visible = False : BUT_J_Entry.Visible = False : BUT_NB_Entry.Visible = False : BUT_PAYMENT.Visible = False : BUT_RECEIPT.Visible = False : BUT_S_ASSET.Visible = False : BUT_WIP_FINAL.Visible = False : BUT_CashBook.Visible = False
            BUT_CASH_PAY_VOLUME.Visible = True : BUT_BANK_PAY_VOLUME.Visible = True : BUT_CONTRA_VOLUME.Visible = True : BUT_JV_VOLUME.Visible = True
        Else
            BUT_CASH.Visible = True : BUT_ASSET_TFER.Visible = True : BUT_BANK.Visible = True : BUT_C_BOX.Visible = True : BUT_CASH.Visible = True : BUT_DONATION.Visible = True : BUT_FD.Visible = True : BUT_GIFT.Visible = True : BUT_INT_TRANSFER.Visible = True : BUT_J_Entry.Visible = True : BUT_NB_Entry.Visible = True : BUT_PAYMENT.Visible = True : BUT_RECEIPT.Visible = True : BUT_S_ASSET.Visible = True : BUT_WIP_FINAL.Visible = True : BUT_CashBook.Visible = True
            BUT_CASH_PAY_VOLUME.Visible = False : BUT_BANK_PAY_VOLUME.Visible = False : BUT_CONTRA_VOLUME.Visible = False : BUT_JV_VOLUME.Visible = False
        End If

        '
        Me.GLookUp_ItemList.Focus()

        xPleaseWait.Hide()
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
    End Sub

    Private Sub BE_Cash_Bank_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BE_Cash_Bank.ButtonClick
        Dim xfrm As New Frm_View_Summary : xfrm.MainBase = Base
        xfrm.Text = "Summary..." : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        xfrm.Dispose()
    End Sub

    Public Sub Grid_Display()
        xPleaseWait.Show("Voucher Entry" & vbNewLine & vbNewLine & "L o a d i n g . . . !")

        '-------------------(1) Get Data--------------------------------------------------------------------------------------------
        '
        'Get Cash Balance..............
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
        If Cash_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        If Cash_Bal.Rows.Count > 0 Then
            If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Open_Cash_Bal = Cash_Bal.Rows(0)("OPENING") Else Open_Cash_Bal = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("CLOSING")) Then Close_Cash_Bal = Cash_Bal.Rows(0)("CLOSING") Else Close_Cash_Bal = 0
        Else : Open_Cash_Bal = 0 : Close_Cash_Bal = 0 : End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Bank Balance..............
        Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        Dim Bank_Bal As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '
        Dim _bankCnt As Integer = 1 : Dim _colWidth As Integer = 0 : Dim _colSetWidth As Integer = 90
        Dim _online_BANK_COL_TR_REC As String = "" : Dim _online_BANK_COL_NB_REC As String = "" : Dim _online_BANK_COL_TR_PAY As String = "" : Dim _online_BANK_COL_NB_PAY As String = ""
        Dim _local__BANK_COL_TR_REC As String = "" : Dim _local__BANK_COL_NB_REC As String = "" : Dim _local__BANK_COL_TR_PAY As String = "" : Dim _local__BANK_COL_NB_PAY As String = ""
        '
        '
        If Bank_Bal.Rows.Count > 0 Then
            For Each XROW In Bank_Bal.Rows
                If Not IsDBNull(XROW("OPENING")) Then Open_Bank_Bal += XROW("OPENING") Else Open_Bank_Bal += 0
                If Not IsDBNull(XROW("CLOSING")) Then Close_Bank_Bal += XROW("CLOSING") Else Close_Bank_Bal += 0
                _bankCnt += 1
            Next
        Else
            Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        End If
        BE_Cash_Bank.Text = "Cash: " & Format(Close_Cash_Bal, "#,0.00") & "    Bank: " & Format(Close_Bank_Bal, "#,0.00")

        xPleaseWait.Hide()
    End Sub

    Private Sub Check_Negative_Balance()
        Dim TR_Table As DataTable = Nothing : Dim DV1 As DataView = Nothing : Dim XTABLE As DataTable = Nothing : Dim _Temp_Balance, _Temp_Receipt, _Temp_Payment As Double
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(1) Cash Negative Balance
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(1.1) Cash Transaction......................
        TR_Table = Base._Voucher_DBOps.GetNegativeBalance("00080", "", "")
        If TR_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '(1.2) Get Cash Opening Balance..............
        Dim Opening_Bal As Double = 0
        If xFr_Date <> Base._open_Year_Sdt Then
            Opening_Bal = 0
            Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(Base._open_Year_Sdt, Base._open_Year_Edt, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
            If Cash_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
            If Cash_Bal.Rows.Count > 0 Then
                If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Opening_Bal = Cash_Bal.Rows(0)("OPENING") Else Opening_Bal = 0
            Else : Opening_Bal = 0 : End If
        Else
            Opening_Bal = Open_Cash_Bal
        End If
        '(1.3) Cash Opening Balance Insert..........
        Dim ROW As DataRow : ROW = TR_Table.NewRow : ROW("iTR_SORT_REC") = "A" : ROW("iTR_DATE") = Format(Base._open_Year_Sdt, Base._Date_Format_Current) : ROW("iTR_REC_CASH") = Opening_Bal : ROW("iTR_PAY_CASH") = 0 : TR_Table.Rows.Add(ROW)
        '(1.4) Cash Data Sorting
        DV1 = New DataView(TR_Table) : DV1.Sort = "iTR_DATE,iTR_SORT_REC" : XTABLE = DV1.ToTable
        Negative_MsgStr = "" : _Temp_Balance = 0 : _Temp_Receipt = 0 : _Temp_Payment = 0
        '(1.5) Check Negative Cash
        For Each XROW In XTABLE.Rows
            If Not IsDBNull(XROW("iTR_REC_CASH")) Then _Temp_Receipt = XROW("iTR_REC_CASH") Else _Temp_Receipt = 0
            If Not IsDBNull(XROW("iTR_PAY_CASH")) Then _Temp_Payment = XROW("iTR_PAY_CASH") Else _Temp_Payment = 0
            If _Temp_Receipt <= 0 And _Temp_Payment <= 0 Then : Else
                _Temp_Balance = (_Temp_Balance + _Temp_Receipt) - _Temp_Payment
            End If
            If _Temp_Balance < 0 Then
                Negative_MsgStr = "N E G A T I V E   B A L A N C E" & vbNewLine & "==========================================" & vbNewLine & "In Cash..." & vbNewLine & "Date      : " & Format(XROW("iTR_DATE"), "dd-MMM, yyyy") & vbNewLine & "Amount : " & Format(_Temp_Balance, "#,0.##") & vbNewLine & "==========================================" & vbNewLine & "For more details: Check Daily Balances Report" : Exit For
            End If
        Next
        '
        If Negative_MsgStr.Trim.Length > 0 Then Exit Sub
        '
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(2) Bank Negative Balance
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(2.0) Get Bank Detail..............
        Dim Bank_Det As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(Base._open_Year_Sdt, Base._open_Year_Edt, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Det Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '
        Dim OtherCondition As String = "" : Opening_Bal = 0
        If Bank_Det.Rows.Count > 0 Then
            For Each B_Row In Bank_Det.Rows
                '(2.1) Get Bank Opening Balance & ID.........
                OtherCondition = " AND ( TR_SUB_CR_LED_ID ='" & B_Row("ID") & "' OR TR_SUB_DR_LED_ID ='" & B_Row("ID") & "' ) "
                If Not IsDBNull(B_Row("OPENING")) Then Opening_Bal = B_Row("OPENING") Else Opening_Bal = 0
                '(2.2) Bank Transaction......................
                TR_Table = Base._Voucher_DBOps.GetNegativeBalance("00079", B_Row("ID"), OtherCondition)
                If TR_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                '(2.3) Cash Opening Balance Insert..........
                ROW = TR_Table.NewRow : ROW("iTR_SORT_REC") = "A" : ROW("iTR_DATE") = Format(Base._open_Year_Sdt, Base._Date_Format_Current) : ROW("iTR_REC_BANK") = Opening_Bal : ROW("iTR_PAY_BANK") = 0 : TR_Table.Rows.Add(ROW)
                '(2.4) Cash Data Sorting
                DV1 = New DataView(TR_Table) : DV1.Sort = "iTR_DATE,iTR_SORT_REC" : XTABLE = DV1.ToTable
                Negative_MsgStr = "" : _Temp_Balance = 0 : _Temp_Receipt = 0 : _Temp_Payment = 0
                '(2.5) Check Negative Cash
                For Each XROW In XTABLE.Rows
                    If Not IsDBNull(XROW("iTR_REC_BANK")) Then _Temp_Receipt = XROW("iTR_REC_BANK") Else _Temp_Receipt = 0
                    If Not IsDBNull(XROW("iTR_PAY_BANK")) Then _Temp_Payment = XROW("iTR_PAY_BANK") Else _Temp_Payment = 0
                    If _Temp_Receipt <= 0 And _Temp_Payment <= 0 Then : Else
                        _Temp_Balance = (_Temp_Balance + _Temp_Receipt) - _Temp_Payment
                    End If
                    If _Temp_Balance < 0 Then
                        Negative_MsgStr = "N E G A T I V E   B A L A N C E" & vbNewLine & "==========================================" & vbNewLine & "In Bank  : " & B_Row("BI_SHORT_NAME") & ", A/c. No.: " & B_Row("BA_ACCOUNT_NO") & vbNewLine & "Date      : " & Format(XROW("iTR_DATE"), "dd-MMM, yyyy") & vbNewLine & "Amount : " & Format(_Temp_Balance, "#,0.##") & vbNewLine & "==========================================" & vbNewLine & "For more details: Check Daily Balances Report"
                        Exit For
                    End If
                Next
                If Negative_MsgStr.Trim.Length > 0 Then Exit Sub
            Next
        End If

    End Sub

    Public Sub DataNavigation(ByVal Action As String, Optional AdditionalParam As String = "")

        Select Case Action
            Case "NEW"
                'Dim Selection_By_Item As Boolean = xfrm.Selection_By_Item
                If Voucher_Type = "CASH" Then
                    Dim zfrm As New Frm_Voucher_Win_Cash : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "BANK" Then
                    Dim zfrm As New Frm_Voucher_Win_B2B : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "DONATION - REGULAR" Then
                    Dim zfrm As New Frm_Voucher_Win_Donation_R : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me)
                    If Base._IsVolumeCenter And zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim xfrm1 As New Frm_Receipt_Options()
                        xfrm1.chosen_Voucher = Frm_Receipt_Options.Voucher_Choice.Donation
                        xfrm1.Tr_Date = zfrm.Txt_V_Date.DateTime : xfrm1.Mode = zfrm.Cmd_Mode.Text : xfrm1.Selected_Bank_ID = zfrm.GLookUp_BankList.Tag : xfrm1.SelectedRefBankID = zfrm.GLookUp_RefBankList.Tag
                        xfrm1.ShowDialog()
                    End If
                    : zfrm.Dispose()
                End If
                If Voucher_Type = "DONATION - FOREIGN" Then
                    Dim zfrm As New Frm_Voucher_Win_Donation_F : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "COLLECTION BOX" Then
                    If Base._open_Ins_ID = "00001" Or Base._open_Ins_ID = "00005" Then 'bk, trst 
                        Dim zfrm As New Frm_Voucher_Win_C_Box : zfrm.MainBase = Base
                        If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                        zfrm.ShowDialog(Me) : zfrm.Dispose()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show("In " & Base._open_Ins_Name & vbNewLine & vbNewLine & "C o l l e c t i o n   B o x   E n t r y   N o t   A p p l i c a b l e . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                If Voucher_Type = "PAYMENT" Or Voucher_Type = "PAYMENT - INSTITUTE" Then
                    Dim zfrm As New Frm_Voucher_Win_Gen_Pay : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    If Not Sel_Led_Name = Nothing Then
                        If Sel_Led_Name.ToUpper = "DEBTORS" Or Sel_Led_Name.ToUpper = "CREDITORS" Or GLookUp_ItemList.Text.ToUpper.StartsWith("PAYMENT") Then zfrm.SelectedPaymentType = Frm_Voucher_Win_Gen_Pay.PaymentType.Cash
                        If Sel_v_Date > DateTime.MinValue Then zfrm.Txt_V_Date.DateTime = Sel_v_Date
                        zfrm.Sel_Bank_ID = Sel_Pay_Bank_ID
                    End If
                    If AdditionalParam.Length > 0 Then
                        If Sel_v_Date > DateTime.MinValue Then zfrm.Txt_V_Date.DateTime = Sel_v_Date
                        zfrm.Sel_Bank_ID = Sel_Pay_Bank_ID
                        zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection
                        If AdditionalParam = "BANK" Then zfrm.SelectedPaymentType = Frm_Voucher_Win_Gen_Pay.PaymentType.Bank
                        If AdditionalParam = "CASH" Then zfrm.SelectedPaymentType = Frm_Voucher_Win_Gen_Pay.PaymentType.Cash
                        If AdditionalParam = "JV" Then zfrm.SelectedPaymentType = Frm_Voucher_Win_Gen_Pay.PaymentType.Credit
                    End If
                    zfrm.ShowDialog(Me)
                    Sel_v_Date = zfrm.Txt_V_Date.DateTime : Sel_Pay_Bank_ID = zfrm.Sel_Bank_ID
                    : zfrm.Dispose()
                End If
                If Voucher_Type = "RECEIPTS" Or Voucher_Type = "RECEIPTS - INSTITUTE" Then
                    Dim zfrm As New Frm_Voucher_Win_Gen_Rec : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me)
                    If Base._IsVolumeCenter And zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim xfrm1 As New Frm_Receipt_Options()
                        xfrm1.chosen_Voucher = Frm_Receipt_Options.Voucher_Choice.Receipt
                        xfrm1.Tr_Date = zfrm.Txt_V_Date.DateTime : xfrm1.Mode = zfrm.Cmd_Mode.Text : xfrm1.Selected_Bank_ID = zfrm.GLookUp_BankList.Tag : xfrm1.SelectedRefBankID = zfrm.GLookUp_RefBankList.Tag
                        xfrm1.ShowDialog()
                    End If
                    : zfrm.Dispose()
                End If
                If Voucher_Type = "DONATION - GIFT" Then
                    Dim zfrm As New Frm_Voucher_Win_Gift : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "INTERNAL TRANSFER" Then
                    Dim zfrm As New Frm_Voucher_Win_I_Transfer : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me)
                    If Base._IsVolumeCenter And zfrm.DialogResult = Windows.Forms.DialogResult.OK And zfrm.iTrans_Type = "CREDIT" Then
                        Dim xfrm1 As New Frm_Receipt_Options()
                        xfrm1.chosen_Voucher = Frm_Receipt_Options.Voucher_Choice.Internal_Transfer
                        xfrm1.Selected_Bank_ID = zfrm.Ref_Bank_ID
                        xfrm1.Tr_Date = zfrm.Txt_V_Date.DateTime : xfrm1.Mode = zfrm.Cmd_Mode.Text : xfrm1.Selected_Bank_ID = zfrm.GLookUp_BankList.Tag
                        xfrm1.Selected_ItemID = zfrm.GLookUp_ItemList.Tag : xfrm1.Selected_Trans_Type = zfrm.iTrans_Type : xfrm1.Selected_Purpose_ID = zfrm.GLookUp_PurList.EditValue
                        xfrm1.ShowDialog()
                    End If
                    : zfrm.Dispose()
                End If
                If Voucher_Type = "FD" Then
                    Dim zfrm As New Frm_Voucher_Win_FD : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    If Not Selection_By_Item Then
                        Dim yFrm As New Frm_Voucher_Win_FD_Type : yFrm.MainBase = Base
                        yFrm.ShowDialog(Me)
                        If yFrm.DialogResult = Windows.Forms.DialogResult.OK Then
                            zfrm.iSpecific_ItemID = yFrm.SelectedActivityID
                            Select Case yFrm.SelectedActivityID
                                Case "f6e4da62-821f-4961-9f93-f5177fca2a77" : zfrm.iAction = Common_Lib.Common.FDAction.New_FD : zfrm.TitleX.Text = "FD Creation"
                                Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" : zfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : zfrm.TitleX.Text = "FD Renewal"
                                Case "65730a27-e365-4195-853e-2f59225fe8f4" : zfrm.iAction = Common_Lib.Common.FDAction.Close_FD : zfrm.TitleX.Text = "FD Closure"
                            End Select
                        Else
                            Exit Sub
                        End If
                    Else
                        Select Case Me.GLookUp_ItemList.Tag
                            Case "f6e4da62-821f-4961-9f93-f5177fca2a77"
                                zfrm.iAction = Common_Lib.Common.FDAction.New_FD : zfrm.TitleX.Text = "FD Creation"
                            Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b"
                                zfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : zfrm.TitleX.Text = "FD Renewal"
                            Case "1ed5cbe4-c8aa-4583-af44-eba3db08e117", "65730a27-e365-4195-853e-2f59225fe8f4"
                                zfrm.iAction = Common_Lib.Common.FDAction.Close_FD : zfrm.TitleX.Text = "FD Closure" : zfrm.iSpecific_ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                        End Select
                    End If
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "SALE OF ASSET" Then
                    Dim zfrm As New Frm_Voucher_Win_Sale_Asset : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "MEMBERSHIP" Then
                    Dim zfrm As New Frm_Voucher_Membership
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "MEMBERSHIP RENEWAL" Then
                    Dim zfrm As New Frm_Voucher_Membership_Renewal
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "JOURNAL" Then
                    Dim zfrm As New Frm_Voucher_Win_Journal : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "ASSET TRANSFER" Then
                    Dim zfrm As New Frm_Voucher_Win_Asset_Transfer : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                If Voucher_Type = "WIP FINALIZATION" Then
                    Dim zfrm As New Frm_Voucher_Win_WIP_Finalization : zfrm.MainBase = Base
                    If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Me.GLookUp_ItemList.Tag Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me) : zfrm.Dispose()
                End If
                Me.GLookUp_ItemList.EditValue = "" : Me.GLookUp_ItemList.Text = "" : Me.GLookUp_ItemList.Focus()
            Case "CLOSE"
                Me.Close() : Exit Sub
        End Select
        If Base._IsVolumeCenter = True Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Post same entry again...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                DataNavigation(Action, AdditionalParam)
            Else
                Exit Sub
            End If
            xPromptWindow.Dispose()
        End If
    End Sub

    Dim Closed_Bank_Account_No As String = ""
    Function Get_Closed_Bank_Status(ByVal xRecID As String) As Boolean
        Dim Flag As Boolean = False : Dim CR_LED_ID As String = "" : Dim DR_LED_ID As String = "" : Dim xTR_MODE As String = "" : Dim xTR_CODE As Integer = 0

        Dim d4 As DataTable = Base._Voucher_DBOps.GetTransactionDetail(xRecID)
        If d4.Rows.Count > 0 Then
            If Not IsDBNull(d4.Rows(0)("TR_SUB_CR_LED_ID")) Then CR_LED_ID = d4.Rows(0)("TR_SUB_CR_LED_ID") Else CR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_SUB_DR_LED_ID")) Then DR_LED_ID = d4.Rows(0)("TR_SUB_DR_LED_ID") Else DR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_CODE")) Then xTR_CODE = d4.Rows(0)("TR_CODE") Else xTR_CODE = 0
            If Not IsDBNull(d4.Rows(0)("TR_MODE")) Then xTR_MODE = d4.Rows(0)("TR_MODE") Else xTR_MODE = ""
        End If
        If xTR_CODE = 6 Or UCase(xTR_MODE) <> "CASH" Then 'count(rec_id)
            Dim MaxValue As Object = Nothing
            MaxValue = Base._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID)
            If IsDBNull(MaxValue) Then
                Flag = False : Closed_Bank_Account_No = ""
            Else
                Flag = True : Closed_Bank_Account_No = MaxValue
            End If
        End If
        Return Flag
    End Function

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
        ElseIf e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Hide_Properties()
            If GLookUp_ItemList.Text.Length > 0 And GLookUp_ItemList.Tag.Length > 0 Then
                Me.BUT_OK_Click(Nothing, Nothing)
            End If
        End If

    End Sub
    Private Sub GLookUp_ItemList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.EditValueChanged
        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" Then

            If (Me.GLookUp_ItemListView.RowCount > 0 And Val(Me.GLookUp_ItemListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_ID").ToString
                Get_Voucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                Sel_Led_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LED_ID").ToString
                Sel_Led_Name = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_NAME").ToString
                Me.BUT_OK.Enabled = True
            Else
                Me.BUT_OK.Enabled = False
            End If

        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim ITEM_APPLICABLE As String = "" : If Base.Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
        Dim d1 As DataTable = Base._Voucher_DBOps.GetItem_LedgerListMain(Base.Allow_Foreign_Donation, Base.Allow_Membership, ITEM_APPLICABLE)
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
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.GLookUp_ItemList.EditValue = 0
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
        End If
    End Sub
    Private Sub GLookUp_ItemList_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GLookUp_ItemList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_ItemList(sender)))
    End Sub
    Private Sub FilterLookup_ItemList(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        ' Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("LED_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("ITEM_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
#End Region

#Region "Start--> Change Period"

    Private Sub Fill_Change_Period_Items()
        Me.Cmb_View.Properties.Items.Clear()
        For I As Integer = Base._open_Year_Sdt.Month To 12 : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Sdt.Year) : Next
        For I As Integer = 1 To Base._open_Year_Edt.Month : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Edt.Year) : Next
        Me.Cmb_View.Properties.Items.Add("1st Quarter") ' : APR to JUN
        Me.Cmb_View.Properties.Items.Add("2nd Quarter") ' : JUL to SEP
        Me.Cmb_View.Properties.Items.Add("3rd Quarter") ' : OCT to DEC
        Me.Cmb_View.Properties.Items.Add("4th Quarter") ' : JAN to MAR
        Me.Cmb_View.Properties.Items.Add("1st Half Yearly") ' : APR to SEP
        Me.Cmb_View.Properties.Items.Add("2nd Half Yearly") ' : OCT to MAR
        Me.Cmb_View.Properties.Items.Add("Nine Months") ' : APR to DEC
        Me.Cmb_View.Properties.Items.Add("Financial Year")
        Me.Cmb_View.Properties.Items.Add("Specific Period")
    End Sub
    Private Sub Cmb_View_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Cmb_View.SelectedIndexChanged
        Me.Cmb_View.Properties.Buttons(1).Enabled = False

        'INPUT
        If Cmb_View.SelectedIndex >= 0 And Cmb_View.SelectedIndex <= 11 Then '12 MONTHS
            Dim Sel_Mon As String = Me.Cmb_View.Text.Substring(0, 3).ToUpper
            Dim SEL_MM As Integer = IIf(Sel_Mon = "JAN", 1, IIf(Sel_Mon = "FEB", 2, IIf(Sel_Mon = "MAR", 3, IIf(Sel_Mon = "APR", 4, IIf(Sel_Mon = "MAY", 5, IIf(Sel_Mon = "JUN", 6, IIf(Sel_Mon = "JUL", 7, IIf(Sel_Mon = "AUG", 8, IIf(Sel_Mon = "SEP", 9, IIf(Sel_Mon = "OCT", 10, IIf(Sel_Mon = "NOV", 11, IIf(Sel_Mon = "DEC", 12, 4))))))))))))
            xFr_Date = New Date(Me.Cmb_View.Text.Substring(4, 4), SEL_MM, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 12 Then 'Q1
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 13 Then 'Q2
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 7, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 14 Then 'Q3
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 10, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 15 Then 'Q4
            xFr_Date = New Date(Base._open_Year_Edt.Year, 1, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 16 Then 'H1
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 17 Then 'H2
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 10, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 18 Then 'NINE MONTHS
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 9, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 19 Then 'FINANCIAL YEAR
            xFr_Date = Base._open_Year_Sdt : xTo_Date = Base._open_Year_Edt
        ElseIf Cmb_View.SelectedIndex = 20 Then 'SPECIFIC PERIOD
            Me.Cmb_View.Properties.Buttons(1).Enabled = True : Change_Period() : Exit Sub
        End If

        'OUTPUT
        If Cmb_View.SelectedIndex >= 0 Then
            Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
            Grid_Display()
        End If
    End Sub
    Private Sub Cmb_View_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles Cmb_View.ButtonClick
        If e.Button.Index = 1 Then
            Change_Period()
            Exit Sub
        End If
    End Sub
    Private Sub Change_Period()
        Dim xfrm As New Frm_Change_Period : xfrm.MainBase = Base
        xfrm.Text = Me.Text : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
            xFr_Date = xfrm.xFr_Date : xTo_Date = xfrm.xTo_Date
            xfrm.Dispose()
        Else
            xfrm.Dispose()
            Exit Sub
        End If
        Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        Grid_Display()
    End Sub

#End Region

End Class



