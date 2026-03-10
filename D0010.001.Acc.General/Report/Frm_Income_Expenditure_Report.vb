
Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting

Public Class Frm_Income_Expenditure_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public xTo_Date, xFr_Date As Date
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Dim xid As String = ""

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

    Public Sub Grid_Display()
        'xPleaseWait.Show("Income & Expenditure Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False
        'Dim dtGridTable As DataTable = Base._Reports_Common_DBOps.GetReportIncomeExpenditure(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, Base._open_Year_Sdt, Base._open_Year_Edt)

        'dtExpenseDet = dtGridTable.Clone()
        'Dim expression As String = "PGName <>'' and Led_Type='EXPENSE'"
        'Dim foundRows() As DataRow
        'foundRows = dtGridTable.Select(expression)
        'foundRows.CopyToDataTable(dtExpenseDet, LoadOption.OverwriteChanges)

        'dtIncomeDet = dtGridTable.Clone()
        'expression = "PGName <>'' and Led_Type='INCOME'"
        'foundRows = dtGridTable.Select(expression)
        'foundRows.CopyToDataTable(dtIncomeDet, LoadOption.OverwriteChanges)
        'Dim ExcessOfExpOverIncomeAmt As Double = ExcessOfExpOverIncome()
        'If ExcessOfExpOverIncomeAmt > 0 Then
        '    Dim Dr As DataRow = dtIncomeDet.NewRow()
        '    Dr("PGName") = ""
        '    Dr("SGName") = ""
        '    Dr("Led_Name") = "Excess of exp. over income"
        '    Dr("Led_Type") = "INCOME"
        '    Dr("Amount in Rs.") = ExcessOfExpOverIncomeAmt.ToString("0.00")
        '    dtIncomeDet.Rows.Add(Dr)
        'Else
        '    Dim Dr As DataRow = dtExpenseDet.NewRow()
        '    Dr("PGName") = ""
        '    Dr("SGName") = ""
        '    Dr("Led_Name") = "Excess of income over exp."
        '    Dr("Led_Type") = "EXPENSE"
        '    Dr("Amount in Rs.") = (-ExcessOfExpOverIncomeAmt).ToString("0.00")
        '    dtExpenseDet.Rows.Add(Dr)
        'End If

        'Dim dview As New DataView(TBList)
        'dview.Sort = "NATURE"
        'If TBList Is Nothing Then
        '    Base.HandleDBError_OnNothingReturned()
        '    Exit Sub
        'End If
        Me.grdRPT.DataSource = Base._Reports_Common_DBOps.GetReportIncomeExpenditure(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, Base._open_Year_Sdt, Base._open_Year_Edt)

        Me.grdRptView.Columns("ExpAmount").Caption = "Amount(in Rs.)"
        Me.grdRptView.Columns("ExpAmount").Width = 100
        Me.grdRptView.Columns("ExpAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.grdRptView.Columns("ExpAmount").DisplayFormat.FormatString = "{0:#,0.00}"
        Me.grdRptView.Columns("ExpAmount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.grdRptView.Columns("Expense Particulars").Width = 280
        Me.grdRptView.Columns("IncAmount").Caption = "Amount(in Rs.)"
        Me.grdRptView.Columns("IncAmount").Width = 100
        Me.grdRptView.Columns("IncAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.grdRptView.Columns("IncAmount").DisplayFormat.FormatString = "{0:#,0.00}"
        Me.grdRptView.Columns("IncAmount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.grdRptView.Columns("Income Particulars").Width = 280
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
                    'Base.Show_ListPreview(grdRPT, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "", "", True)
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
