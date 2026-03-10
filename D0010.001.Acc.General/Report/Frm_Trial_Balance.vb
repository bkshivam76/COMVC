
Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting

Public Class Frm_Trial_Balance

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public xTo_Date, xFr_Date As Date
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Dim xid As String = ""


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
        xPleaseWait.Show("Trial Balance" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
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
        Me.DataNavigation("EDIT")
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
    Private Sub GridView1_GotFocus(sender As Object, e As System.EventArgs) Handles GridView1.GotFocus
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
        'If Me.GridView1.RowCount > 0 And Val(e.RowHandle) >= 0 Then
        '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Action Status").ToString.ToUpper.Trim = "INCOMPLETE" Then
        '        e.Appearance.BackColor2 = Color.LightSalmon
        '    End If
        'End If
    End Sub
    Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.ShowCustomizationForm
        Me.ColumnFormVisibleFlag1 = True
        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
    End Sub
    Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.HideCustomizationForm
        Me.ColumnFormVisibleFlag1 = False
        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
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
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        Grid_Display()
        xPleaseWait.Hide()
    End Sub

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("TRIAL_BALANCE")

    Public Sub Grid_Display()
        xPleaseWait.Show("Trial Balance" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        RowFlag1 = False
        Dim TBList As DataTable = Base._Reports_Common_DBOps.GetReportTrialBalance(Common_Lib.RealTimeService.ClientScreen.Report_Potamel, Base._open_Year_Sdt, Base._open_Year_Edt)
        Dim dview As New DataView(TBList)
        dview.Sort = "NATURE"
        If TBList Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = dview
        Me.GridView1.Columns("PRIMARY_TYPE").Visible = False
        Me.GridView1.Columns("SECONDARY_TYPE").Visible = False
        'If Me.GridView1.Columns.Contains("LED_ID") Then Me.GridView1.Columns("LED_ID").Visible = False
        Me.GridView1.Columns("LEDGER").Width = 200

        Me.GridView1.Columns("DR").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.GridView1.Columns("CR").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        'Me.GridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DR", Me.GridView1.Columns("DR"), "{0:#,0.00}")
        'Me.GridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CR", Me.GridView1.Columns("CR"), "{0:#,0.00}")
        Me.GridView1.OptionsView.ShowFooter = True

        xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action

            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "", "", True)
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
                Me.Close()
        End Select

    End Sub

#End Region

End Class

#Region "old"
'Public Class Frm_Trial_Balance_Old

'#Region "Start--> Default Variables"
'    Public MainBase As New Common_Lib.Common
'    Public xTo_Date, xFr_Date As Date
'    Private RowFlag1 As Boolean
'    Private ColumnFormVisibleFlag1 As Boolean = False
'    Dim xid As String = ""

'    Public Sub New()

'        ' This call is required by the Windows Form Designer.
'        InitializeComponent()

'        DevExpress.Skins.SkinManager.EnableFormSkins()
'        'DevExpress.UserSkins.OfficeSkins.Register()
'        DevExpress.UserSkins.BonusSkins.Register()
'    End Sub

'#End Region

'#Region "Start--> Form Events"

'    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
'        Me.Dispose()
'    End Sub

'    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 
''Automation Related Call  
'Common_Lib.TestSupport.StoreControlDetail(Me) 
''End : Automation Related Call
'        xPleaseWait.Show("Trial Balance" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
'        Base = MainBase
'        ''/...FOR PROGRAMMING MODE ONLY......\
'        Programming_Testing()
'        ''\................................../
'        Set_Default() ' Prepare Status-bar help text of all objects
'        Me.Focus()
'    End Sub


'#End Region

'#Region "Start--> Button Events"

'    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus
'        Dim btn As SimpleButton
'        btn = CType(sender, SimpleButton)
'        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
'    End Sub
'    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus
'        Dim btn As SimpleButton
'        btn = CType(sender, SimpleButton)
'        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
'    End Sub
'    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click
'        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
'            Dim btn As SimpleButton
'            btn = CType(sender, SimpleButton)
'            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
'            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
'            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
'            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
'            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
'            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
'            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
'            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
'        Else
'            Dim T_btn As ToolStripMenuItem
'            T_btn = CType(sender, ToolStripMenuItem)
'            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
'            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
'            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
'            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
'            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
'            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
'            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
'        End If

'    End Sub
'    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown
'        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
'    End Sub

'#End Region

'#Region "Start--> Grid Events"
'    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
'        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.GridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'    End Sub
'    Private Sub GridDefaultProperty()
'        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
'        Me.GridView1.OptionsBehavior.Editable = False

'        Me.GridView1.OptionsDetail.AllowZoomDetail = True
'        Me.GridView1.OptionsDetail.AutoZoomDetail = True

'        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True

'        Me.GridView1.OptionsSelection.InvertSelection = True

'        Me.GridView1.OptionsView.ColumnAutoWidth = False
'        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
'        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
'        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
'        Me.GridView1.OptionsView.ShowGroupPanel = False
'        Me.GridView1.OptionsView.ShowAutoFilterRow = False
'    End Sub
'    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
'        Me.DataNavigation("EDIT")
'    End Sub
'    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

'        If e.KeyCode = Keys.Enter Then
'            e.SuppressKeyPress = True
'            Me.DataNavigation("EDIT")
'        End If
'        If e.KeyCode = Keys.Delete Then
'            e.SuppressKeyPress = True
'            Me.DataNavigation("DELETE")
'        End If
'        If e.KeyCode = Keys.PageUp Then
'            e.SuppressKeyPress = True
'            SendKeys.Send("+{TAB}")
'            Exit Sub
'        End If
'        If e.KeyCode = Keys.PageDown Then
'            e.SuppressKeyPress = True
'            SendKeys.Send("{TAB}")
'            Exit Sub
'        End If

'        '  Me.Txt_Search.Text = Me.Txt_Search.Text & Chr(e.KeyCode)

'    End Sub
'    Private Sub GridView1_GotFocus(sender As Object, e As System.EventArgs) Handles GridView1.GotFocus
'        CreationDetail(Me.GridView1.FocusedRowHandle)
'    End Sub
'    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
'        Try
'            If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
'                If RowFlag1 Then
'                    CreationDetail(e.FocusedRowHandle)
'                End If
'            Else
'                Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
'            End If
'        Catch ex As Exception
'            Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
'        End Try
'    End Sub
'    Private Sub CreationDetail(ByVal Xrow As Integer)
'        If Xrow >= 0 Then
'            Me.Pic_Status.Visible = True : Me.Lbl_Separator1.Visible = True : Dim Status As String = ""
'            Try
'                Status = Me.GridView1.GetRowCellValue(Xrow, "Action Status").ToString
'            Catch ex As Exception
'            End Try
'            If Status.ToUpper.Trim.ToString = "LOCKED" Then
'                Me.Lbl_Status.Text = "Completed" : Me.Pic_Status.Image = My.Resources.lock : Me.Lbl_Status.ForeColor = Color.Blue
'            Else
'                Me.Lbl_Status.Text = Status : Me.Pic_Status.Image = My.Resources.unlock : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
'            End If

'            Dim Add_Date As String = "" : Dim Add_By As String = ""
'            Try
'                Add_Date = Me.GridView1.GetRowCellValue(Xrow, "Add Date").ToString
'                Add_By = Me.GridView1.GetRowCellValue(Xrow, "Add By").ToString()
'            Catch ex As Exception
'            End Try
'            If IsDate(Add_Date) Then
'                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
'            Else
'                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
'            End If

'            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
'            Try
'                Edit_Date = Me.GridView1.GetRowCellValue(Xrow, "Edit Date").ToString
'                Edit_By = Me.GridView1.GetRowCellValue(Xrow, "Edit By").ToString
'            Catch ex As Exception
'            End Try
'            If IsDate(Edit_Date) Then
'                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
'            Else
'                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
'            End If
'        End If
'    End Sub
'    Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
'        'If Me.GridView1.RowCount > 0 And Val(e.RowHandle) >= 0 Then
'        '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Action Status").ToString.ToUpper.Trim = "INCOMPLETE" Then
'        '        e.Appearance.BackColor2 = Color.LightSalmon
'        '    End If
'        'End If
'    End Sub
'    Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.ShowCustomizationForm
'        Me.ColumnFormVisibleFlag1 = True
'        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
'        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
'    End Sub
'    Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.HideCustomizationForm
'        Me.ColumnFormVisibleFlag1 = False
'        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
'        Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
'    End Sub
'    Private Sub GridControl_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.buttonclick
'        Select Case e.Button.Tag.ToString.Trim.ToUpper
'            Case "OPEN_COL"
'                If Me.ColumnFormVisibleFlag1 Then
'                    Me.GridView1.DestroyCustomization()
'                    e.Button.Hint = "Show Column Chooser"
'                    e.Button.ImageIndex = 6
'                Else
'                    Me.GridView1.ColumnsCustomization()
'                    e.Button.Hint = "Hide Column Chooser"
'                    e.Button.ImageIndex = 10
'                End If

'            Case "GROUP_BOX"
'                If Me.GridView1.OptionsView.ShowGroupPanel Then
'                    Me.GridView1.OptionsView.ShowGroupPanel = False
'                    e.Button.Hint = "Show Group Box"
'                    e.Button.ImageIndex = 8
'                Else
'                    Me.GridView1.OptionsView.ShowGroupPanel = True
'                    e.Button.Hint = "Hide Group Box"
'                    e.Button.ImageIndex = 16
'                End If
'            Case "GROUPED_COL"
'                If Me.GridView1.OptionsView.ShowGroupedColumns Then
'                    Me.GridView1.OptionsView.ShowGroupedColumns = False
'                    e.Button.Hint = "Show Group Column"
'                    e.Button.ImageIndex = 14
'                Else
'                    Me.GridView1.OptionsView.ShowGroupedColumns = True
'                    e.Button.Hint = "Hide Grouped Column"
'                    e.Button.ImageIndex = 17
'                End If
'            Case "FOOTER_BAR"
'                If Me.GridView1.OptionsView.ShowFooter Then
'                    Me.GridView1.OptionsView.ShowFooter = False
'                    e.Button.Hint = "Show Footer Bar"
'                    e.Button.ImageIndex = 18
'                Else
'                    Me.GridView1.OptionsView.ShowFooter = True
'                    e.Button.Hint = "Hide Footer Bar"
'                    e.Button.ImageIndex = 13
'                End If
'            Case "GROUP_FOOTER"
'                If Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
'                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
'                    e.Button.Hint = "Show Group Footer Bar"
'                Else
'                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
'                    e.Button.Hint = "Hide Group Footer Bar"
'                End If

'            Case "FILTER"
'                Me.GridView1.ShowFilterEditor(Me.GridView1.FocusedColumn)
'        End Select
'    End Sub
'#End Region

'#Region "Start--> Procedures"

'    Private Sub Set_Default()
'        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
'        Me.CancelButton = Me.BUT_CLOSE
'        GridDefaultProperty()
'        Grid_Display()
'        xPleaseWait.Hide()
'    End Sub

'    Friend ROW As DataRow
'    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("TRIAL_BALANCE")

'    Public Sub Grid_Display()
'        xPleaseWait.Show("Trial Balance" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
'        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
'        RowFlag1 = False


'        With DT
'            .Columns.Add("Particulars", Type.GetType("System.String"))
'            .Columns.Add("P_ID", Type.GetType("System.String"))
'            .Columns.Add("Opening", Type.GetType("System.Double"))
'            .Columns.Add("Debit", Type.GetType("System.Double"))
'            .Columns.Add("Credit", Type.GetType("System.Double"))
'            .Columns.Add("Closing", Type.GetType("System.Double"))
'        End With


'        Dim _TB_Data As DataTable = Base._Reports_Common_DBOps.GetTrialBalance(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, xFr_Date, xTo_Date)
'        If _TB_Data Is Nothing Then
'            Base.HandleDBError_OnNothingReturned()
'            Exit Sub
'        End If
'        If _TB_Data.Rows.Count > 0 Then
'            Dim _PG_ID As String = _TB_Data.Rows(0)("PG_ID") : Dim _SG_ID As String = _TB_Data.Rows(0)("SG_ID") : Dim _LED_ID As String = _TB_Data.Rows(0)("LED_ID") : Dim _ITEM_ID As String = _TB_Data.Rows(0)("TR_ITEM_ID")
'            Dim _DR_Amt As Double = 0 : Dim _CR_Amt As Double = 0
'            For Each _Row In _TB_Data.Rows
'                If _Row("PG_ID") = _PG_ID And _Row("SG_ID") = _SG_ID And _Row("LED_ID") = _LED_ID And _Row("TR_ITEM_ID") = _ITEM_ID Then
'                    If Not IsDBNull(_Row("TR_AMOUNT_DR")) Then _DR_Amt = _DR_Amt + _Row("TR_AMOUNT_DR")
'                    If Not IsDBNull(_Row("TR_AMOUNT_CR")) Then _CR_Amt = _CR_Amt + _Row("TR_AMOUNT_CR")
'                Else
'                    _PG_ID = _Row("PG_ID")
'                    If Not IsDBNull(_Row("TR_AMOUNT_DR")) Then _DR_Amt = _Row("TR_AMOUNT_DR")
'                    If Not IsDBNull(_Row("TR_AMOUNT_CR")) Then _CR_Amt = _Row("TR_AMOUNT_CR")
'                End If
'                Dim _DTROW As DataRow
'                _DTROW = DT.NewRow
'                _DTROW("Particulars") = _Row("LED_NAME")
'                _DTROW("P_ID") = _Row("PG_ID")
'                _DTROW("Opening") = _Row("OPENING_BAL")
'                _DTROW("Debit") = _Row("TR_AMOUNT_DR")
'                _DTROW("Credit") = _Row("TR_AMOUNT_CR")
'                Dim dblOpening As Double = 0
'                If Not IsDBNull(_Row("OPENING_BAL")) Then
'                    dblOpening = Convert.ToDouble(_Row("OPENING_BAL"))
'                End If
'                _DTROW("Closing") = _Row("CLOSING_BAL") + dblOpening
'                DT.Rows.Add(_DTROW)
'            Next
'        End If
'        Me.GridControl1.DataSource = DT
'        'Me.GridControl1.DataSource = _TB_Data

'        'Me.GridView1.Columns("PG_ID").Group()
'        'Me.GridView1.Columns("SG_ID").Group()
'        'Me.GridView1.Columns("LED_ID").Group()
'        'Me.GridView1.Columns("TR_ITEM_ID").Group()
'        'Me.GridView1.ExpandAllGroups()

'        'Me.GridView1.Columns("LED_TYPE").Visible = False
'        Me.GridView1.Columns("P_ID").Visible = False
'        'Me.GridView1.Columns("PG_NAME").Visible = False
'        'Me.GridView1.Columns("SG_ID").Visible = False
'        'Me.GridView1.Columns("SG_NAME").Visible = False
'        'Me.GridView1.Columns("LED_ID").Visible = False
'        'Me.GridView1.Columns("TR_ITEM_ID").Visible = False
'        'Me.GridView1.Columns("ITEM_NAME").Visible = False
'        'Me.GridView1.Columns("TR_TYPE").Visible = False
'        'Me.GridView1.Columns("TR_AMOUNT").Visible = False
'        'Me.GridView1.BestFitMaxRowCount = 5
'        Me.GridView1.BestFitColumns()


'        '--------------------------------------
'        If Me.GridView1.RowCount <= 0 Then
'            RowFlag1 = False
'        Else
'            Try
'                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Item Name")
'                If xid.Length > 0 Then
'                    Me.GridView1.FocusedRowHandle = Me.GridView1.LocateByValue("ID", xid)
'                    Me.GridView1.SelectRow(Me.GridView1.FocusedRowHandle)
'                Else
'                    Me.GridView1.FocusedRowHandle = 0 : Me.GridView1.SelectRow(0)
'                End If
'            Catch ex As Exception
'                Me.GridView1.FocusedColumn = Me.GridView1.Columns("ID")
'                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
'            End Try
'            RowFlag1 = True
'            Me.GridView1.Focus()
'        End If
'        '--------------------------------------
'        xPleaseWait.Hide()
'    End Sub

'    Public Sub DataNavigation(ByVal Action As String)
'        Select Case Action

'            Case "REFRESH"
'                Grid_Display()
'            Case "PRINT-LIST"
'                If Me.GridView1.RowCount > 0 Then
'                    Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "", "", True)
'                End If
'                Me.GridView1.Focus()
'            Case "FILTER"
'                If Me.GridView1.OptionsView.ShowAutoFilterRow Then
'                    Me.But_Filter.Image = My.Resources.bluesearch
'                    Me.GridView1.OptionsView.ShowAutoFilterRow = False
'                Else
'                    Me.But_Filter.Image = My.Resources.blueaccept
'                    Me.GridView1.OptionsView.ShowAutoFilterRow = True
'                End If
'            Case "FIND"
'                If Me.GridView1.IsFindPanelVisible Then
'                    Me.But_Find.Image = My.Resources.greensearch
'                    Me.GridView1.HideFindPanel()
'                Else
'                    Me.But_Find.Image = My.Resources.greenaccept
'                    Me.GridView1.ShowFindPanel()
'                End If

'            Case "CLOSE"
'                Me.Close()
'        End Select

'    End Sub

'#End Region

'End Class
#End Region