
Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting

Public Class Frm_Utilization_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public xTo_Date, xFr_Date As Date
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Dim xid As String = ""
    Public UtilizePercent As Decimal = 0
    Dim dtExpenseDet As DataTable
    Dim dtIncomeDet As DataTable
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
        'xPleaseWait.Show("Trial Balance" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        ''/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        ''\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Grid Events"
    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.grdRptView.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRptView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRptView.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRptView.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRptView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub GridDefaultProperty()
        Me.grdRptView.OptionsBehavior.AllowIncrementalSearch = True
        Me.grdRptView.OptionsBehavior.Editable = False

        Me.grdRptView.OptionsDetail.AllowZoomDetail = True
        Me.grdRptView.OptionsDetail.AutoZoomDetail = True

        Me.grdRptView.OptionsNavigation.EnterMoveNextColumn = True

        Me.grdRptView.OptionsSelection.InvertSelection = True

        Me.grdRptView.OptionsView.ColumnAutoWidth = False
        Me.grdRptView.OptionsView.EnableAppearanceEvenRow = True
        Me.grdRptView.OptionsView.EnableAppearanceOddRow = True
        Me.grdRptView.OptionsView.ShowChildrenInGroupPanel = True
        Me.grdRptView.OptionsView.ShowGroupPanel = False
        Me.grdRptView.OptionsView.ShowAutoFilterRow = False
    End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRptView.DoubleClick
        Me.DataNavigation("EDIT")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdRptView.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("EDIT")
        End If
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True
            Me.DataNavigation("DELETE")
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            Exit Sub
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If

        '  Me.Txt_Search.Text = Me.Txt_Search.Text & Chr(e.KeyCode)

    End Sub
    Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grdRptView.CustomDrawCell
        'If Me.GridView1.RowCount > 0 And Val(e.RowHandle) >= 0 Then
        '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Action Status").ToString.ToUpper.Trim = "INCOMPLETE" Then
        '        e.Appearance.BackColor2 = Color.LightSalmon
        '    End If
        'End If
    End Sub
    Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRptView.ShowCustomizationForm
        Me.ColumnFormVisibleFlag1 = True
        Me.grdRPT.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
        Me.grdRPT.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
    End Sub
    Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRptView.HideCustomizationForm
        Me.ColumnFormVisibleFlag1 = False
        Me.grdRPT.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
        Me.grdRPT.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
    End Sub
    Private Sub GridControl_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles grdRPT.EmbeddedNavigator.buttonclick
        Select Case e.Button.Tag.ToString.Trim.ToUpper
            Case "OPEN_COL"
                If Me.ColumnFormVisibleFlag1 Then
                    Me.grdRptView.DestroyCustomization()
                    e.Button.Hint = "Show Column Chooser"
                    e.Button.ImageIndex = 6
                Else
                    Me.grdRptView.ColumnsCustomization()
                    e.Button.Hint = "Hide Column Chooser"
                    e.Button.ImageIndex = 10
                End If

            Case "GROUP_BOX"
                If Me.grdRptView.OptionsView.ShowGroupPanel Then
                    Me.grdRptView.OptionsView.ShowGroupPanel = False
                    e.Button.Hint = "Show Group Box"
                    e.Button.ImageIndex = 8
                Else
                    Me.grdRptView.OptionsView.ShowGroupPanel = True
                    e.Button.Hint = "Hide Group Box"
                    e.Button.ImageIndex = 16
                End If
            Case "GROUPED_COL"
                If Me.grdRptView.OptionsView.ShowGroupedColumns Then
                    Me.grdRptView.OptionsView.ShowGroupedColumns = False
                    e.Button.Hint = "Show Group Column"
                    e.Button.ImageIndex = 14
                Else
                    Me.grdRptView.OptionsView.ShowGroupedColumns = True
                    e.Button.Hint = "Hide Grouped Column"
                    e.Button.ImageIndex = 17
                End If
            Case "FOOTER_BAR"
                If Me.grdRptView.OptionsView.ShowFooter Then
                    Me.grdRptView.OptionsView.ShowFooter = False
                    e.Button.Hint = "Show Footer Bar"
                    e.Button.ImageIndex = 18
                Else
                    Me.grdRptView.OptionsView.ShowFooter = True
                    e.Button.Hint = "Hide Footer Bar"
                    e.Button.ImageIndex = 13
                End If
            Case "GROUP_FOOTER"
                If Me.grdRptView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
                    Me.grdRptView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
                    e.Button.Hint = "Show Group Footer Bar"
                Else
                    Me.grdRptView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    e.Button.Hint = "Hide Group Footer Bar"
                End If

            Case "FILTER"
                Me.grdRptView.ShowFilterEditor(Me.grdRptView.FocusedColumn)
        End Select
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        Grid_Display()
        'xPleaseWait.Hide()
    End Sub

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("TRIAL_BALANCE")
   
    Private Function getTransactionTable(ByVal IsReceipt As Boolean, ByVal FrDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable = Base._Reports_Common_DBOps.GetTSummaryList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, IsReceipt, FrDate, ToDate)
        If dt Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
            Exit Function
        End If
        Dim CashBankTotalAmt As String = "0.00"
        If dt.Rows.Count > 0 Then
            CashBankTotalAmt = dt.Rows(0)("IGroupSum")
            'remove 1st row from the table-- this represents the total sum. This will be added as last row
            dt.Rows.RemoveAt(0)
            'make GroupSum display adjustment. 
            'Table obtained from the database will have GroupSum displayed corresponding to its groupname
            'Hence a small adjustment is made to show GroupSum at the end of subgroups
            Dim totalAmount As String = ""
            Dim tempAmount As String = ""
            For i = 0 To dt.Rows.Count - 1
                If Not String.IsNullOrEmpty(dt.Rows(i).Item("IGroupSum")) Then
                    If Not String.IsNullOrEmpty(totalAmount) Then
                        tempAmount = totalAmount
                    End If
                    totalAmount = dt.Rows(i).Item("IGroupSum")
                    dt.Rows(i).Item("IGroupSum") = ""
                    ' dt.Rows(i).Item("ITEM") = ""
                End If

                If (String.IsNullOrEmpty(dt.Rows(i).Item("IAmount")) And i > 0) Then
                    dt.Rows(i - 1).Item("IGroupSum") = tempAmount
                End If
            Next
            dt.Rows(dt.Rows.Count - 1).Item("IGroupSum") = totalAmount
            dt.AcceptChanges()

            Dim rows = dt.[Select]("ITEM = 'FD With Banks'")
            For Each row As DataRow In rows
                row.Delete()
            Next
            dt.AcceptChanges()

            Dim nTable As DataTable = dt.Clone
            For Each dRow As DataRow In dt.Rows
                If ((Not String.IsNullOrEmpty(dRow.Item("ITEM")))) Then
                    Dim newRow As DataRow = nTable.NewRow()
                    newRow.ItemArray = dRow.ItemArray
                    nTable.Rows.Add(newRow)
                End If
            Next
            dt = nTable
        End If
        'add Opening and closing balances for cash and bank
        'Opening Balances in Receipts table
        'If (IsReceipt) Then

        '    'insert a dummy row after opening balances
        '    Dim dtReceiptsRow As DataRow = dt.NewRow()
        '    dt.Rows.InsertAt(dtReceiptsRow, 0)

        '    Dim Bankobj As DataRow = dt.NewRow()
        '    Bankobj("Item") = "Opening Bank Balance"
        '    Bankobj("IType") = "RECEIPTS"
        '    Bankobj("IAmount") = ""
        '    Bankobj("IGroupSum") = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00")
        '    CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetBankDetails(FrDate, ToDate).OpeningBalance)
        '    dt.Rows.InsertAt(Bankobj, 0)

        '    Dim CashRow As DataRow = dt.NewRow()
        '    CashRow("ITEM") = "Opening Cash Balance"
        '    CashRow("IType") = "RECEIPTS"
        '    CashRow("IAmount") = ""
        '    CashRow("IGroupSum") = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00")
        '    CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetCashDetails(FrDate, ToDate).OpeningBalance)
        '    dt.Rows.InsertAt(CashRow, 0)

        'Else
        '    'insert a dummy row @ first
        '    Dim dtPaymentRow As DataRow = dt.NewRow()
        '    dt.Rows.InsertAt(dtPaymentRow, dt.Rows.Count)

        '    Dim Cashobj As DataRow = dt.NewRow()
        '    Cashobj("Item") = "Closing Cash Balance"
        '    Cashobj("IType") = "PAYMENTS"
        '    Cashobj("IAmount") = ""
        '    Cashobj("IGroupSum") = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00")
        '    CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetCashDetails(FrDate, ToDate).ClosingBalance)
        '    dt.Rows.InsertAt(Cashobj, dt.Rows.Count)

        '    Dim Bankobj As DataRow = dt.NewRow()
        '    Bankobj("ITEM") = "Closing Bank Balance"
        '    Bankobj("IType") = "PAYMENTS"
        '    Bankobj("IAmount") = ""
        '    Bankobj("IGroupSum") = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00")
        '    CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetBankDetails(FrDate, ToDate).ClosingBalance)
        '    dt.Rows.InsertAt(Bankobj, dt.Rows.Count)
        'End If
        'Dim dr1 As DataRow = dt.NewRow()
        'dr1("IGroupSum") = Convert.ToDecimal(CashBankTotalAmt).ToString("#0.00")
        'dt.Rows.Add(dr1) 'add row that corresponds to total 
        dt.AcceptChanges()
        Return dt
    End Function

    Public Sub Grid_Display()
        'xPleaseWait.Show("Utilization Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False
        Dim receiptsDt As New DataTable
        Dim paymentsDt As New DataTable

        receiptsDt = getTransactionTable(True, Base._open_Year_Sdt, Base._open_Year_Edt) ' Gets datatable for receipts
        paymentsDt = getTransactionTable(False, Base._open_Year_Sdt, Base._open_Year_Edt) ' Gets datatable for Payments

        Dim Utilization_Table As New DataTable : Dim ROW As DataRow
        With Utilization_Table
            .Columns.Add("ITEM", Type.GetType("System.String"))
            .Columns.Add("IAmount", Type.GetType("System.String"))
            .Columns.Add("IGroupSum", Type.GetType("System.String"))
            .Columns.Add("PITEM", Type.GetType("System.String"))
            .Columns.Add("IPAmount", Type.GetType("System.String"))
            .Columns.Add("IPGroupSum", Type.GetType("System.String"))
        End With
        Dim totalRows As Integer = IIf(receiptsDt.Rows.Count > paymentsDt.Rows.Count, receiptsDt.Rows.Count, paymentsDt.Rows.Count)
        Dim testDouble As Double = 0
        For i = 0 To totalRows - 1
            ROW = Utilization_Table.NewRow()
            If (receiptsDt.Rows.Count > i) Then
                ROW("ITEM") = receiptsDt.Rows(i)("ITEM")
                ROW("IAmount") = IIf(Double.TryParse(receiptsDt.Rows(i)("IAmount").ToString(), testDouble), receiptsDt.Rows(i)("IAmount"), "")
                ROW("IGroupSum") = IIf(Double.TryParse(receiptsDt.Rows(i)("IGroupSum").ToString(), testDouble), receiptsDt.Rows(i)("IGroupSum"), "")
            Else
                ROW("ITEM") = ""
                ROW("IAmount") = ""
                ROW("IGroupSum") = ""
            End If

            If (paymentsDt.Rows.Count > i) Then
                ROW("PITEM") = paymentsDt.Rows(i)("ITEM")
                ROW("IPAmount") = IIf(Double.TryParse(paymentsDt.Rows(i)("IAmount").ToString(), testDouble), paymentsDt.Rows(i)("IAmount"), "")
                ROW("IPGroupSum") = IIf(Double.TryParse(paymentsDt.Rows(i)("IGroupSum").ToString(), testDouble), paymentsDt.Rows(i)("IGroupSum"), "")
            Else
                ROW("PITEM") = ""
                ROW("IPAmount") = ""
                ROW("IPGroupSum") = ""
            End If
            If Not (IsDBNull(ROW("ITEM")) And IsDBNull(ROW("PITEM"))) Then Utilization_Table.Rows.Add(ROW)
        Next
        ''insert a dummy row to distinguish total from others
        'ROW = Utilization_Table.NewRow()

        'Utilization_Table.Rows.Add(ROW)
        '' add last row of each table

        'ROW = Utilization_Table.NewRow()
        'ROW("ITEM") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("ITEM").ToString
        'ROW("IAmount") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("IAmount").ToString
        'ROW("IGroupSum") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("IGroupSum").ToString
        'ROW("PITEM") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("ITEM").ToString
        'ROW("IPAmount") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("IAmount").ToString
        'ROW("IPGroupSum") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("IGroupSum").ToString


        'Utilization_Table.Rows.Add(ROW)

        ''Replace Headers
        'Utilization_Table.Columns("IAmount").ColumnName = "AMOUNT"
        'Utilization_Table.Columns("IGroupSum").ColumnName = "TOTAL"
        'Utilization_Table.Columns("IPAmount").ColumnName = " AMOUNT"
        'Utilization_Table.Columns("IPGroupSum").ColumnName = " TOTAL"
        'Utilization_Table.Columns("ITEM").ColumnName = "RECEIPTS"
        'Utilization_Table.Columns("PITEM").ColumnName = "PAYMENTS"
        Dim TotReceipts As Decimal = 0
        For Each cRow As DataRow In receiptsDt.Rows
            TotReceipts = TotReceipts + CDec(cRow("IAmount"))
        Next
        Dim TotPayments As Decimal = 0
        For Each cRow As DataRow In paymentsDt.Rows
            TotPayments = TotPayments + CDec(cRow("IAmount"))
        Next
        UtilizePercent = Decimal.Round(Convert.ToDecimal(100 * (TotPayments / TotReceipts)), 2)
        lblUtilizePercent.Text = "Total Utilization : " + Convert.ToString(UtilizePercent) + "% approx."

        Me.grdRPT.DataSource = Utilization_Table

        Me.grdRptView.Columns("IAmount").Caption = "Amount(in Rs.)"
        Me.grdRptView.Columns("IAmount").Width = 100
        Me.grdRptView.Columns("IAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.grdRptView.Columns("IAmount").DisplayFormat.FormatString = "{0:#,0.00}"
        Me.grdRptView.Columns("IAmount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.grdRptView.Columns("ITEM").Width = 280
        Me.grdRptView.Columns("ITEM").Caption = "Receipts"
        Me.grdRptView.Columns("IPAmount").Caption = "Amount(in Rs.)"
        Me.grdRptView.Columns("IPAmount").Width = 100
        Me.grdRptView.Columns("IPAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.grdRptView.Columns("IPAmount").DisplayFormat.FormatString = "{0:#,0.00}"
        Me.grdRptView.Columns("IPAmount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.grdRptView.Columns("PITEM").Width = 280
        Me.grdRptView.Columns("PITEM").Caption = "Payments"
        Me.grdRptView.Columns("IGroupSum").Visible = False
        Me.grdRptView.Columns("IPGroupSum").Visible = False
        Me.grdRptView.OptionsView.ShowFooter = True
        'xPleaseWait.Hide()
    End Sub

    Private Function ExcessOfExpOverIncome() As Double
        Dim sumObjectExpense As Object, sumObjectIncome As Object
        sumObjectExpense = dtExpenseDet.Compute("Sum([Amount in Rs.])", "PGName <>'' and Led_Type='EXPENSE'")
        sumObjectIncome = dtIncomeDet.Compute("Sum([Amount in Rs.])", "PGName <>'' and Led_Type='INCOME'")
        Dim TotExpense As Double = 0
        If sumObjectExpense.ToString().Length > 0 Then TotExpense = Convert.ToDouble(sumObjectExpense.ToString())
        Dim TotIncome As Double = 0
        If sumObjectIncome.ToString().Length > 0 Then TotIncome = Convert.ToDouble(sumObjectIncome.ToString())

        Dim Diff As Double = TotExpense - TotIncome
        Return Diff
    End Function

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action

            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.grdRptView.RowCount > 0 Then
                    'MsgBox("coming soon!")
                    'Base.Show_ListPreview(grdRPT, Me.Text, Me, True, Printing.PaperKind.A4, "FY " + Base._open_Year_ID.ToString + "- Utilization for " + Base._open_Cen_Name + "(" + Base._open_UID_No.ToString + "-" + Base._open_Zone_ID + ") :- " + UtilizePercent.ToString + "%(approx.)", "", "", True)
                End If
                Me.grdRptView.Focus()
            Case "FILTER"
                If Me.grdRptView.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = My.Resources.bluesearch
                    Me.grdRptView.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = My.Resources.blueaccept
                    Me.grdRptView.OptionsView.ShowAutoFilterRow = True
                End If
            Case "FIND"
                If Me.grdRptView.IsFindPanelVisible Then
                    Me.But_Find.Image = My.Resources.greensearch
                    Me.grdRptView.HideFindPanel()
                Else
                    Me.But_Find.Image = My.Resources.greenaccept
                    Me.grdRptView.ShowFindPanel()
                End If

            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

#End Region

End Class
