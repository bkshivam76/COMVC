Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraReports.UI
Public Class Frm_Daily_Balances_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public xFr_Date As Date = Nothing : Public xTo_Date As Date = Nothing : Public xStatus_Choice As String = ""
    Public DisplayType As String = "BANK" : Private Led_ID As String = "" : Public Bank_Acc_ID As String = ""
    Public xView_Sel_Id As Integer = -1
    Private Open_Cash_Bal, Open_Bank_Bal As Double
    Private Close_Cash_Bal, Close_Bank_Bal As Double
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Public bankName As String = ""
    Private ValuesUpdated As New ArrayList
    Private FormClosingEnable As Boolean = True
    Dim UpdatingGrid As Boolean = False
    Dim SaveDataCalled As Boolean = False
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

    Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not SaveDataCalled Then
            SaveDataCalled = True : Me.DataNavigation("CLOSE")
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        'xPleaseWait.Show("Daily Balances Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        ''/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        ''\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
        AddUnboundColumn()
        ' gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
    End Sub

    Private Sub Frm_Daily_Balances_Report_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FormClosingEnable Then
            Me.DataNavigation("CLOSE")
        End If
    End Sub

#End Region

#Region "Start--> Button Events"
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                Me.DataNavigation("SAVE")
            End If
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus, BUT_OPTIONS.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus, BUT_OPTIONS.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, BUT_OPTIONS.Click, T_REC_TXN.Click, T_REC_CLEARING.Click, BUT_SAVE.Click, T_PRINT_CHEQUE.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_OPTIONS" Then Me.DataNavigation("OPTIONS")
            If UCase(btn.Name) = "BUT_SAVE" Then Me.DataNavigation("SAVE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
            If UCase(T_btn.Name) = "T_REC_TXN" Then Me.DataNavigation("RECONSILE-TXN")
            If UCase(T_btn.Name) = "T_REC_CLEARING" Then Me.DataNavigation("RECONSILE-CLEAR")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(T_btn.Name) = "T_PRINT_CHEQUE" Then Me.DataNavigation("CHEQUE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown, GridView1.KeyDown, GridControl1.KeyDown, BUT_OPTIONS.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.Enter Then
            DataNavigation("LEDGER")
        End If
    End Sub

#End Region

#Region "Start--> Grid Events"
    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub GridDefaultProperty()
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsBehavior.Editable = False

        Me.GridView1.OptionsDetail.AllowZoomDetail = True
        Me.GridView1.OptionsDetail.AutoZoomDetail = True

        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True

        Me.GridView1.OptionsSelection.InvertSelection = True

        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = False
    End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        Me.DataNavigation("LEDGER")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

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
    Private Sub GridView1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.GotFocus
        CreationDetail(Me.GridView1.FocusedRowHandle)
    End Sub
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag1 Then
                    CreationDetail(e.FocusedRowHandle)
                End If
            Else
                Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
            End If
        Catch ex As Exception
            Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        End Try
    End Sub
    Private Sub CreationDetail(ByVal Xrow As Integer)
        If Xrow >= 0 Then
            Me.Pic_Status.Visible = True : Me.Lbl_Separator1.Visible = True : Dim Status As String = ""
            Try
                Status = Me.GridView1.GetRowCellValue(Xrow, "Action Status").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Lbl_Status.Text = "Completed" : Me.Pic_Status.Image = My.Resources.lock : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Lbl_Status.Text = Status : Me.Pic_Status.Image = My.Resources.unlock : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If

            Dim Add_Date As String = "" : Dim Add_By As String = ""
            Try
                Add_Date = Me.GridView1.GetRowCellValue(Xrow, "Add Date").ToString
                Add_By = Me.GridView1.GetRowCellValue(Xrow, "Add By").ToString()
            Catch ex As Exception
            End Try
            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.GridView1.GetRowCellValue(Xrow, "Edit Date").ToString
                Edit_By = Me.GridView1.GetRowCellValue(Xrow, "Edit By").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
        End If
    End Sub
    Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
    End Sub
    Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.ShowCustomizationForm
        Try
            Me.ColumnFormVisibleFlag1 = True
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.HideCustomizationForm
        Try
            Me.ColumnFormVisibleFlag1 = False
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridControl_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.buttonclick
        Select Case e.Button.Tag.ToString.Trim.ToUpper
            Case "OPEN_COL"
                If Me.ColumnFormVisibleFlag1 Then
                    Me.GridView1.DestroyCustomization()
                    e.Button.Hint = "Show Column Chooser"
                    e.Button.ImageIndex = 6
                Else
                    Me.GridView1.ColumnsCustomization()
                    e.Button.Hint = "Hide Column Chooser"
                    e.Button.ImageIndex = 10
                End If

            Case "GROUP_BOX"
                If Me.GridView1.OptionsView.ShowGroupPanel Then
                    Me.GridView1.OptionsView.ShowGroupPanel = False
                    e.Button.Hint = "Show Group Box"
                    e.Button.ImageIndex = 8
                Else
                    Me.GridView1.OptionsView.ShowGroupPanel = True
                    e.Button.Hint = "Hide Group Box"
                    e.Button.ImageIndex = 16
                End If
            Case "GROUPED_COL"
                If Me.GridView1.OptionsView.ShowGroupedColumns Then
                    Me.GridView1.OptionsView.ShowGroupedColumns = False
                    e.Button.Hint = "Show Group Column"
                    e.Button.ImageIndex = 14
                Else
                    Me.GridView1.OptionsView.ShowGroupedColumns = True
                    e.Button.Hint = "Hide Grouped Column"
                    e.Button.ImageIndex = 17
                End If
            Case "FOOTER_BAR"
                If Me.GridView1.OptionsView.ShowFooter Then
                    Me.GridView1.OptionsView.ShowFooter = False
                    e.Button.Hint = "Show Footer Bar"
                    e.Button.ImageIndex = 18
                Else
                    Me.GridView1.OptionsView.ShowFooter = True
                    e.Button.Hint = "Hide Footer Bar"
                    e.Button.ImageIndex = 13
                End If
            Case "GROUP_FOOTER"
                If Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
                    e.Button.Hint = "Show Group Footer Bar"
                Else
                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    e.Button.Hint = "Hide Group Footer Bar"
                End If

            Case "FILTER"
                Me.GridView1.ShowFilterEditor(Me.GridView1.FocusedColumn)
        End Select
    End Sub
    Private Sub GridView1_StartSorting(sender As Object, e As EventArgs) Handles GridView1.StartSorting
        ' GridView1_CustomUnboundColumnData(sender, e)
    End Sub
    Private Sub GridView1_CustomColumnSort(sender As Object, e As CustomColumnSortEventArgs) Handles GridView1.CustomColumnSort
        If e.Column.FieldName = "iTR_REF_CDATE" Then
            e.Handled = True

            Dim d1 As Date = e.Value1 : Dim d2 As Date = e.Value2
            If d1 = DateTime.MinValue And d2 = DateTime.MinValue Then
                e.Result = 0 : Exit Sub
            End If
            If d1 = DateTime.MinValue Then
                'If e.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending Then
                e.Result = 1
                'Else
                '    e.Result = -1
                '    ' e.Result = System.Collections.Comparer.Default.Compare(d1, d2)
                'End If
            Else
                If d2 = DateTime.MinValue Then
                    '    If e.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending Then
                    e.Result = -1
                    '        Else
                    '    e.Result = 1
                    'End If
                Else
                    e.Result = System.Collections.Comparer.Default.Compare(d1, d2)
                End If
            End If
        End If
    End Sub
    Private Sub GridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        Dim view As GridView = TryCast(sender, GridView)
        If e.Column.FieldName = "Balance" AndAlso e.IsGetData Then
            e.Value = getTotalValue(view, e.ListSourceRowIndex)
        End If
    End Sub
    Private Sub AddUnboundColumn()
        GridControl1.ForceInitialize()
        Dim unbColumn As GridColumn = GridView1.Columns.AddField("Balance")
        unbColumn.VisibleIndex = GridView1.Columns.Count
        unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
        unbColumn.OptionsColumn.AllowEdit = False
        unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        unbColumn.DisplayFormat.FormatString = "c"
        unbColumn.Width = 150
        unbColumn.AppearanceCell.BackColor = Color.LemonChiffon
    End Sub
    Private Function getTotalValue(view As GridView, listSourceRowIndex As Integer) As Decimal
        Dim Balance As Decimal = 0
        Dim index As Int16 = GridView1.GetRowHandle(listSourceRowIndex)
        For i As Integer = -1 To index
            Dim Debit As Decimal = 0
            If Not IsDBNull(view.GetRowCellValue(i, "iTR_RECEIPT")) Then Debit = Convert.ToDecimal(view.GetRowCellValue(i, "iTR_RECEIPT"))
            Dim Credit As Decimal = 0
            If Not IsDBNull(view.GetRowCellValue(i, "iTR_PAYMENT")) Then Credit = Val(view.GetRowCellValue(i, "iTR_PAYMENT"))
            Balance += Debit - Credit
        Next
        Return Balance
    End Function
    Private Sub GridView1_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If GridView1.FocusedRowHandle < 0 Then Exit Sub
        If UpdatingGrid Then Exit Sub
        'Dim xPleaseWait As New Common_Lib.PleaseWait
        'Dim DataRowCount As Integer = GridView1.DataRowCount
        'Dim I As Integer : UpdatingGrid = True
        'For I = 0 To DataRowCount - 1
        '    If I = GridView1.FocusedRowHandle Then Continue For
        '    If Not GridView1.IsRowVisible(I) Then Continue For
        '    xPleaseWait.Show("Checking other records within same transaction...")
        '    Dim Mode As String = GridView1.GetRowCellValue(I, "iTR_MODE").ToString()
        '    Dim RefNo As String = ""
        '    If Not IsDBNull(GridView1.GetRowCellValue(I, "Ref")) Then RefNo = GridView1.GetRowCellValue(I, "Ref")
        '    Dim Tr_M_ID As String = ""
        '    If Not IsDBNull(GridView1.GetRowCellValue(I, "iTR_M_ID")) Then Tr_M_ID = GridView1.GetRowCellValue(I, "iTR_M_ID")
        '    If Not IsDBNull(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iTR_M_ID")) Then
        '        If Mode = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iTR_MODE").ToString() And RefNo = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Ref").ToString() And Tr_M_ID = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iTR_M_ID") Then
        '            GridView1.SetRowCellValue(I, GridView1.Columns("iTR_REF_CDATE"), GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iTR_REF_CDATE"))
        '        End If
        '    End If
        'Next
        'If Not e Is Nothing Then
        '    If Not ValuesUpdated.Contains(GridView1.GetRowCellValue(e.RowHandle, "iREC_ID")) Then ValuesUpdated.Add(GridView1.GetRowCellValue(e.RowHandle, "iREC_ID"))
        'Else
        '    If Not ValuesUpdated.Contains(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iREC_ID")) Then ValuesUpdated.Add(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "iREC_ID"))
        'End If

        'xPleaseWait.Hide() : UpdatingGrid = False
        SetBalances()
    End Sub
    Private Sub GridView1_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridView1.ShowingEditor
        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_ITEM") = "OPENING BALANCE" Then
            e.Cancel = True
        End If
    End Sub
    Private Sub GridView1_ValidatingEditor(sender As Object, e As Controls.BaseContainerValidateEditorEventArgs) Handles GridView1.ValidatingEditor
        Dim View As GridView = sender
        If View.FocusedColumn.FieldName = "iTR_REF_CDATE" Then
            If Not e.Value Is Nothing And Me.GridView1.FocusedRowHandle > 0 Then
                'Get the currently edited value 
                If IsDate(e.Value) Then
                    Dim clearingDate As DateTime = Convert.ToDateTime(e.Value)
                    'Specify validation criteria 
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_REF_DATE")) Then
                        If clearingDate < Convert.ToDateTime(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_REF_DATE")) Then
                            e.Valid = False
                            e.ErrorText = "Clearing Date cannot be less than Instrument Date!!"
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub GridControl1_Enter(sender As Object, e As EventArgs) Handles GridControl1.Enter
        GridView1.FocusedColumn = GridView1.Columns("iTR_REF_CDATE")
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        Grid_Display()
        Me.But_Find.Image = My.Resources.greenaccept
        ' Me.GridView1.ShowFindPanel()
        GridView1.Focus()
        '   xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        'xPleaseWait.Show("Daily Balances" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Dim OtherCondition As String = ""

        If DisplayType.ToUpper.Trim = "BANK" Then Me.Txt_TitleX.Text = "Daily Bank Balances" : Led_ID = "00079" : OtherCondition = " AND ( TR_SUB_CR_LED_ID ='" & Bank_Acc_ID & "' OR TR_SUB_DR_LED_ID ='" & Bank_Acc_ID & "' ) "
        If DisplayType.ToUpper.Trim = "CASH" Then Me.Txt_TitleX.Text = "Daily Cash Balances" : Led_ID = "00080" : Bank_Acc_ID = "" : OtherCondition = ""
        Me.Txt_TitleX.Text += "(Fr. " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy") + ")"
        If bankName.Length > 0 Then
            Me.Txt_TitleX.Text += " for " + bankName
        End If
        Dim xFrm As Report_Daily_Balances = New Report_Daily_Balances
        xFrm.DisplayType = DisplayType

        If xStatus_Choice = "unreconciled" Then
            OtherCondition += " AND TI.TR_REF_CDATE IS NULL "
            Txt_TitleX.Text += " (Un-Reconciled) "
        End If

        Dim XTABLE As DataTable = xFrm.CreateData(Bank_Acc_ID, xFr_Date, xTo_Date, OtherCondition, Led_ID)
        Me.GridControl1.DataSource = XTABLE
        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView1.OptionsBehavior.EditingMode = GridEditingMode.Inplace
        Me.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        If DisplayType.ToUpper.Trim = "CASH" Then
            Me.GridView1.Columns("iTR_REF_CDATE").Visible = False
            Me.GridView1.Columns("iTR_MODE").Visible = False
            Me.GridView1.Columns("Ref").Visible = False
        Else
            Me.GridView1.Columns("iTR_REF_CDATE").Visible = True
            Me.GridView1.Columns("iTR_REF_CDATE").Width = 120
            Me.GridView1.Columns("iTR_REF_DATE").Width = 100
            Me.GridView1.Columns("iTR_MODE").Visible = True
            Me.GridView1.Columns("Ref").Visible = True
        End If

        SetBalances()

        'xPleaseWait.Hide()
    End Sub

    Public Sub SetBalances()
        lblTxnBalance.Text = "0" : lblNetBalance.Text = "0" : lblClearingBalance.Text = "0"
        Dim XTABLE As DataTable = GridControl1.DataSource
        For Each cRow As DataRow In XTABLE.Rows
            If Not IsDBNull(cRow("iTR_RECEIPT")) Then
                If Val(cRow("iTR_RECEIPT")) > 0 Then
                    lblTxnBalance.EditValue = (Val(lblTxnBalance.EditValue) + cRow("iTR_RECEIPT")).ToString
                    If Not IsDBNull(cRow("iTR_REF_CDATE")) Then lblClearingBalance.EditValue = (Val(lblClearingBalance.EditValue) + cRow("iTR_RECEIPT")).ToString
                End If
            End If

            If Not IsDBNull(cRow("iTR_PAYMENT")) Then
                If Val(cRow("iTR_PAYMENT")) > 0 Then
                    lblTxnBalance.EditValue = (Val(lblTxnBalance.EditValue) - cRow("iTR_PAYMENT")).ToString
                    If Not IsDBNull(cRow("iTR_REF_CDATE")) Then lblClearingBalance.EditValue = (Val(lblClearingBalance.EditValue) - cRow("iTR_PAYMENT")).ToString
                End If
            End If
        Next
        lblNetBalance.Text = (Val(lblTxnBalance.EditValue) - Val(lblClearingBalance.EditValue)).ToString
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "RECONSILE-TXN"
                If Not GridView1.ValidateEditor Then Exit Sub
                If DisplayType = "CASH" Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Reconciliation can be done in Bank Report only !!", "Cash Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If IsDate(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_DATE")) Then
                    Dim xDate As Date = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_DATE")
                    Dim xFrm As Frm_Bank_Reconcile = New Frm_Bank_Reconcile()
                    xFrm.lblTxnBalance.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Balance")
                    xFrm.Txt_TitleX.Text = "Reconciliation as on " + xDate.ToString("dd-MMM-yyyy")
                    xFrm.Text = bankName : xFrm.xBankAccID = Bank_Acc_ID
                    xFrm.MainBase = Base : xFrm.xdate = xDate : xFrm.ShowDialog()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("T x n   D a t e   i s   I n c o r r e c t !!", "Invalid Date!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Case "RECONSILE-CLEAR"
                If Not GridView1.ValidateEditor Then Exit Sub
                If DisplayType = "CASH" Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Reconciliation can be done in Bank Report only !!", "Cash Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If IsDate(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_REF_CDATE")) Then
                    Dim xDate As Date = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_REF_CDATE")
                    Dim xFrm As Frm_Bank_Reconcile = New Frm_Bank_Reconcile()
                    xFrm.lblTxnBalance.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Balance")
                    xFrm.Txt_TitleX.Text = "Reconciliation as on " + xDate.ToString("dd-MMM-yyyy")
                    xFrm.Text = bankName : xFrm.xBankAccID = Bank_Acc_ID
                    xFrm.MainBase = Base : xFrm.xdate = xDate : xFrm.ShowDialog()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("C l e a r i n g   D a t e   i s   I n c o r r e c t !!", "Invalid Date!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Case "CHEQUE"
                'xPleaseWait.Show("G e n e r a t i n g    C h e q u e")
                Dim Party As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_PARTY_1").ToString()
                Dim Amount As Decimal = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_PAYMENT").ToString())
                Dim VchDate As Date = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "iTR_REF_DATE").ToString()
                Dim xRep As New Cheque_Printing(Party, Amount, VchDate)
                xRep.ShowPreviewDialog()
            Case "SAVE"
                GridView1.ClearColumnsFilter()
                If Not GridView1.ValidateEditor Then Exit Sub
                GridView1_CellValueChanged(Nothing, Nothing)
               ' Dim xPleaseWait As New Common_Lib.PleaseWait
                ' Dim DataRowCount As Integer = GridView1.DataRowCount
                ' Traverse data rows and change the Price field values. 
                'Dim xPromptWindow As New Common_Lib.Prompt_Window
                'If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Do you want to save changes...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                '    'Dim I As Integer
                '    For Each ID As String In ValuesUpdated.ToArray
                '        xPleaseWait.Show("Updating Records")
                '        Dim I As Integer = Me.GridView1.LocateByValue("iREC_ID", ID)
                '        If I < 0 Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show("Please Remove Filter and Retry Saving!!", "Invalid Filter!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            xPleaseWait.Hide()
                '            Exit Sub
                '        End If
                '        Dim Mode As String = GridView1.GetRowCellValue(I, "iTR_MODE").ToString()
                '        'If Mode = "CBS" Or Mode = "RTGS" Or Mode = "NEFT" Or Mode = "CHEQUE" Or Mode = "DD" Then
                '        Dim upParam As Common_Lib.RealTimeService.Param_UpdateClearingDate = New Common_Lib.RealTimeService.Param_UpdateClearingDate
                '        upParam.iRecID = GridView1.GetRowCellValue(I, "iREC_ID")
                '        If IsDate(GridView1.GetRowCellValue(I, "iTR_REF_CDATE")) Then upParam.ClearingDate = GridView1.GetRowCellValue(I, "iTR_REF_CDATE")
                '        upParam.TxnDate = GridView1.GetRowCellValue(I, "iTR_DATE")
                '        If Not IsDBNull(GridView1.GetRowCellValue(I, "Ref")) Then upParam.iRefNo = GridView1.GetRowCellValue(I, "Ref")
                '        upParam.Mode = Mode
                '        If Not IsDBNull(GridView1.GetRowCellValue(I, "iTR_M_ID")) Then upParam.iTrMID = GridView1.GetRowCellValue(I, "iTR_M_ID")
                '        Base._Reports_Common_DBOps.UpdateClearingDate(upParam)
                '        ' End If
                '    Next
                '    ValuesUpdated.Clear()
                '    xPleaseWait.Hide()
                'End If
            Case "OPTIONS"
                Dim xfrm1 As New D0010._001.Dialog_DailyBalances : xfrm1.MainBase = Base
                xfrm1.xSelModeIndex = IIf(DisplayType = "CASH", 0, 1)
                If xfrm1.xSelModeIndex = 1 Then
                    xfrm1.GLookUp_BankList.Tag = Me.Bank_Acc_ID
                Else
                    xfrm1.GLookUp_BankList.Tag = ""
                End If
                If xView_Sel_Id > 0 Then xfrm1.xSelViewIndex = xView_Sel_Id
                xfrm1.xFr_Date = xFr_Date : xfrm1.xTo_Date = xTo_Date : xfrm1.RadioGroup3.SelectedIndex = IIf(Me.xStatus_Choice = "unreconciled", 0, 1)
                xfrm1.RadioGroup2.Enabled = False : xfrm1.ShowDialog(Me)
                If xfrm1.DialogResult = DialogResult.OK Then
                    DisplayType = IIf(xfrm1.rdo_Balances_Mode.SelectedIndex = 0, "CASH", "BANK")
                    If xfrm1.rdo_Balances_Mode.SelectedIndex = 1 Then
                        Bank_Acc_ID = xfrm1.GLookUp_BankList.Tag
                        bankName = xfrm1.GLookUp_BankList.Text & ", A/c.No.: " & xfrm1.BE_Bank_Acc_No.Text
                    Else
                        Bank_Acc_ID = ""
                        bankName = ""
                    End If
                    xStatus_Choice = IIf(xfrm1.RadioGroup3.SelectedIndex = 0, "unreconciled", "all")
                    xFr_Date = xfrm1.xFr_Date : xTo_Date = xfrm1.xTo_Date : xView_Sel_Id = xfrm1.Cmb_View.SelectedIndex
                    Grid_Display()
                End If
            Case "REFRESH"
                If Not GridView1.ValidateEditor Then Exit Sub
                If ValuesUpdated.Count > 0 Then
                    GridView1_CellValueChanged(Nothing, Nothing)
                    DataNavigation("SAVE")
                    ValuesUpdated.Clear()
                End If
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    'Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "", "", True)
                End If
                Me.GridView1.Focus()
            Case "FILTER"
                If Me.GridView1.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = My.Resources.bluesearch
                    Me.GridView1.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = My.Resources.blueaccept
                    Me.GridView1.OptionsView.ShowAutoFilterRow = True
                End If
            Case "FIND"
                If Me.GridView1.IsFindPanelVisible Then
                    Me.But_Find.Image = My.Resources.greensearch
                    Me.GridView1.HideFindPanel()
                Else
                    Me.But_Find.Image = My.Resources.greenaccept
                    Me.GridView1.ShowFindPanel()
                End If
            Case "CLOSE"
                If ValuesUpdated.Count > 0 Then
                    SaveDataCalled = True : DataNavigation("SAVE")
                End If
                FormClosingEnable = False : Me.Close()
        End Select

    End Sub

#End Region
 
End Class
