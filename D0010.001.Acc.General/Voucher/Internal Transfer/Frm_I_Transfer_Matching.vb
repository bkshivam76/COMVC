Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_I_Transfer_Matching

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False

    Public to_Match_Txn_ID As String = ""
    Public to_Cen_Rec_ID As String = ""

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
        xPleaseWait.Show("Transfer Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        Me.Txt_TitleX.Text = "Match Internal Transfer(" & Base._open_UID_No & ")"
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../

        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub



#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, BUT_PRINT.Click, BUT_MATCH.Click, T_Match.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(btn.Name) = "BUT_VIEW" Then Me.DataNavigation("VIEW")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_MATCH" Then Me.DataNavigation("MATCH")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(T_btn.Name) = "T_VIEW" Then Me.DataNavigation("VIEW")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(T_btn.Name) = "T_MATCH" Then Me.DataNavigation("MATCH")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_CLOSE.KeyDown
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
        If e.KeyCode = Keys.Insert Then
            e.SuppressKeyPress = True
            Me.DataNavigation("NEW")
        End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("EDIT")
        End If
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True
            Me.DataNavigation("DELETE")
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation("VIEW")
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
            Case "FIND_WINDOW"
                If Me.GridView1.IsFindPanelVisible Then
                    Me.GridView1.HideFindPanel()
                    e.Button.Hint = "Show Find Window"
                Else
                    Me.GridView1.ShowFindPanel()
                    e.Button.Hint = "Hide Find Window"
                End If
            Case "FILTER"
                Me.GridView1.ShowFilterEditor(Me.GridView1.FocusedColumn)
        End Select
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        Grid_Display()
        xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        xPleaseWait.Show("Transfer Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False

        Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetList(to_Match_Txn_ID, to_Cen_Rec_ID)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = d1
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        Me.GridView1.Columns("REC_ID").Visible = False
        Me.GridView1.Columns("Matched").Width = 10
        If Me.GridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Request Detail")
                Me.GridView1.MoveBy(Me.GridView1.LocateByValue("Amount", to_Match_Txn_ID))
            Catch ex As Exception
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Amount")
                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
            End Try
            Me.GridView1.Focus()
            RowFlag1 = True
            Me.GridView1.Focus()
        End If
        xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)

        Select Case Action
            Case "MATCH"
                Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_ID").ToString()
                If Base._Internal_Tf_Voucher_DBOps.MatchTransfers(to_Match_Txn_ID, xTemp_ID) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("T r a n s f e r   E n t r y   m a t c h e d   s u c c e s s f u l l y...", "Internal Transfer Matching", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y !  T r a n s f e r   E n t r y   C o u l d   n o t   b e   m a t c h e d   s u c c e s s f u l l y...", "Internal Transfer Matching", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
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
            Case "PRINT-LIST"
            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

#End Region

End Class
