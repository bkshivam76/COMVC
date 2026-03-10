Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports System.Drawing
Imports System.Windows.Forms

Public Class Frm_Bank_Accounts_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private ColumnFormVisibleFlag1 As Boolean = False
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
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
        Base = MainBase
        'xPleaseWait.Show(" Existing Bank Account Listing" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
        ''Automation Related Call
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
    End Sub

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.Click, But_Find.Click, But_Filter.Click, T_Refresh.Click, T_Print.Click, T_Close.Click, BUT_PRINT.Click, BUT_SHOW.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_SHOW" Then Me.DataNavigation("SHOW")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub

    Private Sub Set_Default()
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CLOSE
        ' Me.Txt_TitleX.Text = Me.Txt_TitleX.Text & " for " & StrConv(Base._open_Ins_Name, vbProperCase)
        Grid_Display() ' Prepare Grid Data
        'xPleaseWait.Hide()
    End Sub

    Private Sub Grid_Display()
        Dim _db_Table As DataTable '= Base._Reports_Ledgers_DBOps.GetBankAccountsList()
        If _db_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        Me.GridControl1.DataSource = _db_Table
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
    End Sub
   
    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "SHOW"
                Dim xFrm As Frm_Bank_Center_Detail = New Frm_Bank_Center_Detail
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Center")) Then xFrm.xCen_Name.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Center") Else xFrm.xCen_Name.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "UID")) Then xFrm.xCen_UID.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "UID") Else xFrm.xCen_UID.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add1")) Then xFrm.xCen_Add1.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add1") Else xFrm.xCen_Add1.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add2")) Then xFrm.xCen_Add2.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add2") Else xFrm.xCen_Add2.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add3")) Then xFrm.xCen_Add3.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add3") Else xFrm.xCen_Add3.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add4")) Then xFrm.xCen_Add4.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Add4") Else xFrm.xCen_Add4.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "City")) Then xFrm.xCen_City.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "City") Else xFrm.xCen_City.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "State")) Then xFrm.xCen_State.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "State") Else xFrm.xCen_State.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Connected Center")) Then xFrm.xCen_ConnectedCenter.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Connected Center") Else xFrm.xCen_ConnectedCenter.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Zone")) Then xFrm.xCen_Zone.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Zone") Else xFrm.xCen_Zone.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Bank")) Then xFrm.xBank_Name.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Bank") Else xFrm.xBank_Name.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Branch")) Then xFrm.xBank_Branch.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Branch") Else xFrm.xBank_Branch.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "IFSC")) Then xFrm.xBank_IFSC.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "IFSC") Else xFrm.xBank_IFSC.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Acc No.")) Then xFrm.xBank_AccNo.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Acc No.") Else xFrm.xBank_AccNo.Text = ""
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Status")) Then xFrm.xBank_Status.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Status") Else xFrm.xBank_Status.Text = ""
                xFrm.MainBase = Base
                xFrm.ShowDialog()
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
                If Me.GridView1.RowCount > 0 Then
                    'Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "", "", True)
                End If
                Me.GridView1.Focus()
            Case "REFRESH"
                Grid_Display()
            Case "CLOSE"
                Me.Close()
        End Select

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

        Me.GridView1.OptionsDetail.AllowZoomDetail = False
        Me.GridView1.OptionsDetail.AutoZoomDetail = True

        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True

        Me.GridView1.OptionsSelection.InvertSelection = True

        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True


        Me.GridView2.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView2.OptionsBehavior.Editable = False

        Me.GridView2.OptionsDetail.AllowZoomDetail = False
        Me.GridView2.OptionsDetail.AutoZoomDetail = True

        Me.GridView2.OptionsNavigation.EnterMoveNextColumn = True

        Me.GridView2.OptionsSelection.InvertSelection = True


        ' Me.GridView2.OptionsView.ColumnAutoWidth = False
        'Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        'Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = False

    End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DataNavigation("SHOW")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown, GridControl1.KeyDown
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
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("SHOW")
        End If
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
#End Region

End Class
