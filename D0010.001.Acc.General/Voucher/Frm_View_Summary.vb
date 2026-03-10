Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_View_Summary

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public Set_Type, Set_Text As String
    Public xFr_Date As Date = Nothing : Public xTo_Date As Date = Nothing
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
        Base = MainBase

        ''/...FOR PROGRAMMING MODE ONLY......\
        'Programming_Testing()
        ''\................................../
        Me.SubTitleX.Text = "Period  Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        'xPleaseWait.Show("S u m m a r y" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Get_Cash_Bank_Balance()
        'xPleaseWait.Hide()

    End Sub
 

    Private Open_Cash_Bal, Close_Cash_Bal, Open_Bank_Bal, Close_Bank_Bal, R_CASH, R_BANK, P_CASH, P_BANK As Double
    Private Sub Get_Cash_Bank_Balance()
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0 : Open_Bank_Bal = 0 : Close_Bank_Bal = 0 : R_CASH = 0 : R_BANK = 0 : P_CASH = 0 : P_BANK = 0

        ''PREPARE TABLE
        Dim CashBank_DS As New DataSet() : Dim CashBank_Table As DataTable = CashBank_DS.Tables.Add("Table") : Dim ROW As DataRow
        With CashBank_Table
            .Columns.Add("Title", Type.GetType("System.String"))
            .Columns.Add("Sr", Type.GetType("System.Double"))
            .Columns.Add("Description", Type.GetType("System.String"))
            .Columns.Add("O_BALANCE", Type.GetType("System.Double")) : .Columns("O_BALANCE").Caption = "Opening Balance"
            .Columns.Add("R_BALANCE", Type.GetType("System.Double")) : .Columns("R_BALANCE").Caption = "Total Receipt"
            .Columns.Add("P_BALANCE", Type.GetType("System.Double")) : .Columns("P_BALANCE").Caption = "Total Payment"
            .Columns.Add("C_BALANCE", Type.GetType("System.Double")) : .Columns("C_BALANCE").Caption = "Closing Balance"
        End With

        ''CASH................................
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
        If Cash_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        If Cash_Bal.Rows.Count > 0 Then
            If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Open_Cash_Bal = Cash_Bal.Rows(0)("OPENING") Else Open_Cash_Bal = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("RECEIPT")) Then R_CASH = Cash_Bal.Rows(0)("RECEIPT") Else R_CASH = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("PAYMENT")) Then P_CASH = Cash_Bal.Rows(0)("PAYMENT") Else P_CASH = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("CLOSING")) Then Close_Cash_Bal = Cash_Bal.Rows(0)("CLOSING") Else Close_Cash_Bal = 0
        Else : Open_Cash_Bal = 0 : R_CASH = 0 : P_CASH = 0 : Close_Cash_Bal = 0 : End If
        ROW = CashBank_Table.NewRow : ROW("Title") = "CASH" : ROW("Sr") = 1 : ROW("Description") = "CASH Summary" : ROW("O_BALANCE") = Open_Cash_Bal : ROW("R_BALANCE") = R_CASH : ROW("P_BALANCE") = P_CASH : ROW("C_BALANCE") = Close_Cash_Bal : CashBank_Table.Rows.Add(ROW)

        ''BANK................................
        Dim Bank_Bal As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim XCNT As Integer = 2
        If Bank_Bal.Rows.Count > 0 Then
            For Each XROW In Bank_Bal.Rows
                If Not IsDBNull(XROW("OPENING")) Then Open_Bank_Bal = XROW("OPENING") Else Open_Bank_Bal += 0
                If Not IsDBNull(XROW("RECEIPT")) Then R_BANK = XROW("RECEIPT") Else R_BANK += 0
                If Not IsDBNull(XROW("PAYMENT")) Then P_BANK = XROW("PAYMENT") Else P_BANK += 0
                If Not IsDBNull(XROW("CLOSING")) Then Close_Bank_Bal = XROW("CLOSING") Else Close_Bank_Bal += 0

                ROW = CashBank_Table.NewRow : ROW("Title") = "BANK"
                ROW("Sr") = XCNT : ROW("Description") = XROW("BI_SHORT_NAME") & ", A/c. No.: " & XROW("BA_ACCOUNT_NO") : ROW("O_BALANCE") = Open_Bank_Bal : ROW("R_BALANCE") = R_BANK : ROW("P_BALANCE") = P_BANK : ROW("C_BALANCE") = Close_Bank_Bal : CashBank_Table.Rows.Add(ROW)
                XCNT += 1
            Next
        End If

        Me.GridControl2.DataSource = CashBank_DS : Me.GridControl2.DataMember = "Table"
        Me.GridView2.Columns("Description").Width = 200
        Me.GridView2.Columns("O_BALANCE").Width = 120 : Me.GridView2.Columns("O_BALANCE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView2.Columns("O_BALANCE").DisplayFormat.FormatString = "#,0.00"
        Me.GridView2.Columns("R_BALANCE").Width = 120 : Me.GridView2.Columns("R_BALANCE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView2.Columns("R_BALANCE").DisplayFormat.FormatString = "#,0.00"
        Me.GridView2.Columns("P_BALANCE").Width = 120 : Me.GridView2.Columns("P_BALANCE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView2.Columns("P_BALANCE").DisplayFormat.FormatString = "#,0.00"
        Me.GridView2.Columns("C_BALANCE").Width = 120 : Me.GridView2.Columns("C_BALANCE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView2.Columns("C_BALANCE").DisplayFormat.FormatString = "#,0.00"
        Me.GridView2.Columns("Title").Group()
        Me.GridView2.Columns("Title").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        Me.GridView2.Columns("Sr").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending : Me.GridView2.Columns("Sr").Visible = False

        Me.GridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Description", Me.GridView2.Columns("Description"), "Total:")
        Me.GridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "O_BALANCE", Me.GridView2.Columns("O_BALANCE"), "{0:#,0.00}")
        Me.GridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_BALANCE", Me.GridView2.Columns("R_BALANCE"), "{0:#,0.00}")
        Me.GridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "P_BALANCE", Me.GridView2.Columns("P_BALANCE"), "{0:#,0.00}")
        Me.GridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "C_BALANCE", Me.GridView2.Columns("C_BALANCE"), "{0:#,0.00}")

        Me.GridView2.OptionsView.ShowFooter = True
        Me.GridView2.Columns("Description").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Grand Total : {0:0}")
        Me.GridView2.Columns("O_BALANCE").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.GridView2.Columns("R_BALANCE").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.GridView2.Columns("P_BALANCE").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.GridView2.Columns("C_BALANCE").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")

        Me.GridView2.ExpandAllGroups()

    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.DialogResult = DialogResult.OK
    End Sub

#End Region

End Class
