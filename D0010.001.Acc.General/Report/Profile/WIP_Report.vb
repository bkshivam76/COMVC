Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class WIP_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False

    Dim xID As String = ""

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
        'xPleaseWait.Show("Work in Progress Details" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        Set_Default()
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
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
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T_Close.Click, T_Print.Click, BUT_CLOSE.Click, T_CHANGE.Click, T_Refresh.Click, BUT_PRINT.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_CHANGE" Then Me.DataNavigation("CHANGE")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_CHANGE" Then Me.DataNavigation("CHANGE")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub BUT_SHOW_Click(sender As System.Object, e As System.EventArgs)
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Grid Events"

    'Private Sub GridDefaultProperty()
    '    Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
    '    Me.GridView1.OptionsBehavior.Editable = False

    '    Me.GridView1.OptionsDetail.AllowZoomDetail = True
    '    Me.GridView1.OptionsDetail.AutoZoomDetail = True

    '    Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True

    '    Me.GridView1.OptionsSelection.InvertSelection = True

    '    Me.GridView1.OptionsView.ColumnAutoWidth = False
    '    Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
    '    Me.GridView1.OptionsView.EnableAppearanceOddRow = True
    '    Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
    '    Me.GridView1.OptionsView.ShowGroupPanel = False
    '    Me.GridView1.OptionsView.ShowAutoFilterRow = True
    'End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.DataNavigation("CHANGE")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            'Me.DataNavigation("CHANGE")
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
    'Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
    '    Try
    '        If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
    '            If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 And Me.GridView1.IsFocusedView Then
    '                Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
    '            Else
    '                Me.GridView1.Tag = -1
    '            End If
    '        Else
    '            Me.GridView1.Tag = -1
    '        End If
    '    Catch ex As Exception
    '        Me.GridView1.Tag = -1
    '    End Try
    'End Sub
    'Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.ColumnFormVisibleFlag1 = True
    '        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
    '        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.ColumnFormVisibleFlag1 = False
    '        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
    '        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub GridControl_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
    '    Select Case e.Button.Tag.ToString.Trim.ToUpper
    '        Case "OPEN_COL"
    '            If Me.ColumnFormVisibleFlag1 Then
    '                Me.GridView1.DestroyCustomization()
    '                e.Button.Hint = "Show Column Chooser"
    '                e.Button.ImageIndex = 6
    '            Else
    '                Me.GridView1.ColumnsCustomization()
    '                e.Button.Hint = "Hide Column Chooser"
    '                e.Button.ImageIndex = 10
    '            End If

    '        Case "GROUP_BOX"
    '            If Me.GridView1.OptionsView.ShowGroupPanel Then
    '                Me.GridView1.OptionsView.ShowGroupPanel = False
    '                e.Button.Hint = "Show Group Box"
    '                e.Button.ImageIndex = 8
    '            Else
    '                Me.GridView1.OptionsView.ShowGroupPanel = True
    '                e.Button.Hint = "Hide Group Box"
    '                e.Button.ImageIndex = 16
    '            End If
    '        Case "GROUPED_COL"
    '            If Me.GridView1.OptionsView.ShowGroupedColumns Then
    '                Me.GridView1.OptionsView.ShowGroupedColumns = False
    '                e.Button.Hint = "Show Group Column"
    '                e.Button.ImageIndex = 14
    '            Else
    '                Me.GridView1.OptionsView.ShowGroupedColumns = True
    '                e.Button.Hint = "Hide Grouped Column"
    '                e.Button.ImageIndex = 17
    '            End If
    '        Case "FOOTER_BAR"
    '            If Me.GridView1.OptionsView.ShowFooter Then
    '                Me.GridView1.OptionsView.ShowFooter = False
    '                e.Button.Hint = "Show Footer Bar"
    '                e.Button.ImageIndex = 18
    '            Else
    '                Me.GridView1.OptionsView.ShowFooter = True
    '                e.Button.Hint = "Hide Footer Bar"
    '                e.Button.ImageIndex = 13
    '            End If
    '        Case "GROUP_FOOTER"
    '            If Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
    '                Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    '                e.Button.Hint = "Show Group Footer Bar"
    '            Else
    '                Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    '                e.Button.Hint = "Hide Group Footer Bar"
    '            End If
    '        Case "FIND_WINDOW"
    '            If Me.GridView1.IsFindPanelVisible Then
    '                Me.GridView1.HideFindPanel()
    '                e.Button.Hint = "Show Find Window"
    '            Else
    '                Me.GridView1.ShowFindPanel()
    '                e.Button.Hint = "Hide Find Window"
    '            End If
    '        Case "FILTER"
    '            Me.GridView1.ShowFilterEditor(Me.GridView1.FocusedColumn)
    '    End Select
    'End Sub

    Private Sub PivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs) Handles PivotGridControl1.CustomFieldSort
        If e.Field.FieldName = "Month" Then
            'Custom sorting w.r.t MonthNo
            Dim orderValue1 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MonthNo"), orderValue2 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MonthNo")
            e.Result = Comparer.Default.Compare(orderValue1, orderValue2)
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CANCEL
        '   GridDefaultProperty()
        Grid_Display()
        'BUT_CHANGE.Visible = False : BUT_CHANGE.Enabled = False
        ' xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        'xPleaseWait.Show("Work in Progress Details" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False

        Dim _data As DataTable ' = Base._Reports_Common_DBOps.GetWIPReport()
        If _data Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.PivotGridControl1.DataSource = _data
        '--------------------------------
        'Me.PivotGridControl1.BestFitColumns()

        'If Me.PivotGridControl1.RowCount <= 0 Then
        '    RowFlag1 = False
        'Else
        '    Try
        '        Me.GridView1.FocusedColumn = Me.GridView1.Columns("Location Name")
        '        'Me.GridView1.MoveBy(Me.GridView1.LocateByValue("Amount", xID))
        '    Catch ex As Exception
        '        Me.GridView1.FocusedColumn = Me.GridView1.Columns("Location Name")
        '        Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
        '    End Try
        '    Me.GridView1.Focus()
        '    RowFlag1 = True
        '    Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
        '    Me.GridView1.Focus()
        'End If

        'xPleaseWait.Hide()
    End Sub

    Public _Date, _Item_ID, _Mode, _CEN_ID, _BI_ID, _REF_BI_ID, _BI_BRANCH, _REF_BRANCH, _REF_OTHERS, _BI_ACC_NO, _BI_REF_NO, _BI_REF_DT, _PUR_ID, _ID As String
    Public _Amount As Double
    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                '  If Me.PivotGridControl1.RowCount > 0 Then
                'Base.Show_ListPreview(PivotGridControl1, Me.Text, Me, True, Printing.PaperKind.A4, "WIP Report", "", "Year: " & Base._open_Year_Name, True)
                ' End If
                'Me.GridView1.Focus()
            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

#End Region

End Class
