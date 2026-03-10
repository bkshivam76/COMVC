
Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Deposit_Slip_All

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public xTo_Date As Date = New Date(2013, 3, 31), xFr_Date As Date = New Date(2012, 4, 1)
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    '   Public xBankid As String = Nothing, xBankName As String = Nothing
    '  Public Slipno As Integer = 0

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
        'xPleaseWait.Show("Deposit Slip Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        ''/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        ''\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Filter.GotFocus, But_Find.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Filter.LostFocus, But_Find.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, T_PrintSlip.Click
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
            If UCase(T_btn.Name) = "T_PRINTSLIP" Then Me.DataNavigation("PRINTSLIP")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub BUT_SAVE_COM_Click(sender As Object, e As EventArgs) Handles BUT_SAVE_COM.Click
        Grid_Display()
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
        ' Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()

        Fill_Change_Period_Items()
        'Default View Setting..........................................
        Dim xMM As Integer = Now.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        Me.GLookUp_BankList.Focus()
        'xPleaseWait.Hide()
    End Sub

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("TRIAL_BALANCE")
    Private Sub Hide_Properties()
        ' Me.ToolTip1.Hide(Me.GLookUp_BankList)
    End Sub

    Public Sub Grid_Display()
        'xPleaseWait.Show("Deposit Slip Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        RowFlag1 = False
        Dim TBList As DataTable = Base._DepositSlipsDBOps.GetSlipAllReport(IIf(GLookUp_BankList.Text.Length > 0, GLookUp_BankList.Tag, Nothing), xFr_Date, xTo_Date, Val(BE_Slip_No.Text)).Tables(0)
        If TBList Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = TBList
        Me.GridView1.Columns("Slip No").Group()
        Me.GridView1.Columns("Amount").Width = 110
        Me.GridView1.Columns("Date").Width = 110
        Me.GridView1.Columns("Branch Name").Width = 150
        Me.GridView1.Columns("Drawee Bank").Width = 110
        Me.GridView1.Columns("Institute").Visible = False
        Me.GridView1.Columns("Dep_BA_ID").Visible = False
        Me.GridView1.OptionsView.ShowGroupedColumns = True
        Me.GridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", Me.GridView1.Columns("Amount"), "Total: {0:#,0.00}")
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView1.ExpandAllGroups()
        'xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "PRINTSLIP"
                If Me.GridView1.FocusedRowHandle >= 0 Then
                    'Dim xPromptWindow As New Common_Lib.Prompt_Window
                    Dim dTable As DataTable = Base._DepositSlipsDBOps.GetRecord(Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Slip No").ToString), Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Dep_BA_ID").ToString)
                    'If Not IsDBNull(dTable.Rows(0)("SL_PRINT_DATE")) Then
                    '    If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Slip already printed. Do you want to re-print deposit slip ...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    '        Exit Sub
                    '    End If
                    'End If
                    Dim DepostDate As DateTime = DateTime.Today

                    Dim xFrm As New Dialog_Deposit_Slips : xFrm.MainBase = Base
                    xFrm.BE_Bank_Acc.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Deposited Bank").ToString : xFrm.BE_Bank_Acc.Enabled = False
                    xFrm.BE_Slip_No.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Slip No").ToString : xFrm.BE_Slip_No.Enabled = False
                    xFrm.ShowDialog()

                    If xFrm.DialogResult <> DialogResult.OK Then Exit Sub

                    DepostDate = xFrm.Txt_Dep_Date.DateTime

                    Dim xRep As New Report_Deposit_slips : xRep.MainBase = Base
                    xRep.SlipID = dTable.Rows(0)("REC_ID").ToString
                    xRep.XrDate.Text = DepostDate.ToString(Base._Date_Format_DD_MMM_YYYY)
                    'Base.Show_ReportPreview(xRep, "Deposit Slip", Me, True)
                    xRep.Dispose()

                    If IsDBNull(dTable.Rows(0)("SL_PRINT_DATE")) Then
                        'If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "Do you want to mark deposit slip as Printed...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                        '    'If xFrm.DialogResult =DialogResult.OK Then
                        '    Base._DepositSlipsDBOps.MarkAsPrinted(dTable.Rows(0)("ID").ToString, DepostDate)
                        '    DevExpress.XtraEditors.XtraMessageBox.Show("Deposit Slip No. " & xFrm.BE_Slip_No.Text & " marked as Printed..", "Deposit Slip", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'End If
                        ' xPromptWindow.Hide()
                        'End If
                    End If
                End If
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    Dim _table As DataView = GridView1.DataSource
                    'Base.Show_ListPreview(GridControl1, "", Me, False, Printing.PaperKind.A4, _table.ToTable().Rows(0)("Institute").ToString & "," & Base._open_Cen_Name & vbNewLine & "Slip Wise Total of : " & GLookUp_BankList.Text & vbNewLine & "From : " & xFr_Date.ToShortDateString & " to " & xTo_Date.ToShortDateString, "", "", True, 100, 11)
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

#Region "Start--> LookupEdit Events"

    '1.GLookUp_BankList
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
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.BE_Bank_Branch.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.BE_Bank_Acc_No.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
                Me.BE_Bank_Acc_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BI_SHORT_NAME").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._Voucher_DBOps.GetBankAccountsList()
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Payment_DBOps.GetBranches(Branch_IDs)
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
                                        .BANK_ID = B.Field(Of String)("BANK_ID")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        'If Final_Data.Count = 1 Then
        '    Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
        '    Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False

        '    Me.GLookUp_BankList.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        '    Me.BE_Bank_Branch.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        '    Me.BE_Bank_Acc_No.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray

        'Else
        Me.GLookUp_BankList.Properties.ReadOnly = False
        'End If

    End Sub
    Private Sub GLookUp_FilterCriteria_GLookUp_BankList(sender As Object, e As ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BI_SHORT_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
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
        If xfrm.DialogResult = DialogResult.OK Then
            xFr_Date = xfrm.xFr_Date : xTo_Date = xfrm.xTo_Date
            xfrm.Dispose()
        Else
            xfrm.Dispose()
            Exit Sub
        End If
        Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
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