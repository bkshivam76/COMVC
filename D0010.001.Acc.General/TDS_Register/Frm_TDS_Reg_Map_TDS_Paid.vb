Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid.Views.Base
Public Class Frm_TDS_Reg_Map_TDS_Paid

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public Cen_ID As String
    Public Cen_Name As String
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Public xMID As String = ""
    Public TDS_Deduction_List As New ArrayList

    Public Class Out_TDS
        Public RefMID As String
        Public RefSrNo As Integer
        Public TDS_Ded As Double
        Public Declared_Ded_date As DateTime
    End Class

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
        '  Me.DialogResult =DialogResult.Retry
        Me.Close()
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        'xPleaseWait.Show("TDS Deduction Entries for TDS paid to Govt." & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        Set_Default()
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_ACCEPT.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_ACCEPT.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T_Close.Click, T_Print.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_ACCEPT.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub BUT_ACCEPT_Click(sender As Object, e As EventArgs) Handles BUT_ACCEPT.Click
        If Val(lblNetBalance.Text) < 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("Total Mapped TDS deduction amount can not be greater than amount paid to Govt. in voucher!!!", "Invalid Selection!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim DataRowCount As Integer = GridView1.DataRowCount
        For I = 0 To DataRowCount - 1
            If Val(GridView1.GetRowCellValue(I, "TDS_Paid").ToString()) > 0 Then
                Dim inParam As New Out_TDS()
                inParam.TDS_Ded = Val(GridView1.GetRowCellValue(I, "TDS_Paid").ToString())
                inParam.RefMID = GridView1.GetRowCellValue(I, "REC_ID").ToString()
                inParam.RefSrNo = Val(GridView1.GetRowCellValue(I, "SR_NO").ToString())
                inParam.Declared_Ded_date = GridView1.GetRowCellValue(I, "declared_date")
                TDS_Deduction_List.Add(inParam)
            End If
        Next

        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub
    Private Sub BTN_BACK_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Retry
        Me.Hide()
    End Sub
    Private Sub BTN_CANCEL_Click(sender As Object, e As EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub
    Private Sub Txt_Fr_Date_DateTimeChanged(sender As Object, e As EventArgs) Handles Txt_Fr_Date.DateTimeChanged
       
    End Sub
    Private Sub BUT_OK_Click(sender As Object, e As EventArgs) Handles BUT_OK.Click
        If Me.Txt_Fr_Date.Text.Length = 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("Please enter Valid From Date !!!", "Invalid From Date!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Not IsDate(Me.Txt_Fr_Date.Text) Then
            DevExpress.XtraEditors.XtraMessageBox.Show("Please enter Valid From Date !!!", "Invalid From Date!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Me.Txt_TitleX.Text = "Unmapped TDS Deduction Entries for TDS Paid to Govt. since " & Txt_Fr_Date.DateTime.ToShortDateString()
        Grid_Display()
    End Sub
#End Region

#Region "Start--> Grid Events"

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
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
    End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        ' Me.DataNavigation("ACCEPT")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        'If e.KeyCode = Keys.Insert Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("NEW")
        'End If
        'If e.KeyCode = Keys.Enter Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("ACCEPT")
        'End If
        'If e.KeyCode = Keys.Delete Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("DELETE")
        'End If
        'If e.KeyCode = Keys.Space Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("VIEW")
        'End If
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
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 And Me.GridView1.IsFocusedView Then
                    Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
                Else
                    Me.GridView1.Tag = -1
                End If
            Else
                Me.GridView1.Tag = -1
            End If
            If GridView1.FocusedColumn.Caption = "TDS Being Sent" Then
                SendKeys.Send("{F2}")
            End If
        Catch ex As Exception
            Me.GridView1.Tag = -1
        End Try
    End Sub
    Private Sub GridView1_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs) Handles GridView1.FocusedColumnChanged
        If e.FocusedColumn.Caption = "TDS Being Sent" Then
            SendKeys.Send("{F2}")
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
    Private Sub GridView1_ValidatingEditor(sender As Object, e As Controls.BaseContainerValidateEditorEventArgs) Handles GridView1.ValidatingEditor
        Dim View As GridView = sender
        If View.FocusedColumn.FieldName = "TDS_Paid" Then
            If Not e.Value Is Nothing Then
                'Get the currently edited value 
                Dim TDS_being_sent As Decimal = Val(e.Value)
                'Specify validation criteria 
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS_Deducted")) Then
                    If TDS_being_sent > Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS_Deducted")) - Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS_Paid_Already")) Then
                        e.Valid = False
                        e.ErrorText = "TDS being sent can not be greater than TDS pending for this entry!!"
                    End If
                End If
                If TDS_being_sent < 0 Then
                    e.Valid = False
                    e.ErrorText = "Please enter Valid TDS Sent amount!!"
                End If
            End If
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        SetBalances()
    End Sub
#End Region

#Region "Start--> Procedures"
    Private Sub SetBalances()
        lblOnScreenSelection.Text = "0"
        Dim DataRowCount As Integer = GridView1.DataRowCount
        Dim I As Integer
        For I = 0 To DataRowCount - 1
            lblOnScreenSelection.Text = CStr(Val(lblOnScreenSelection.Text) + Val(GridView1.GetRowCellValue(I, "TDS_Paid").ToString()))
        Next
        lblNetBalance.Text = CStr(Val(lblMentionedInvoucher.Text) - Val(lblOnScreenSelection.Text))
        If Val(lblNetBalance.Text) >= 0 Then
            lblNetBalance.BackColor = Color.White
        Else
            lblNetBalance.BackColor = Color.LightPink
        End If
    End Sub

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CANCEL
        GridDefaultProperty()
        Txt_Fr_Date.DateTime = Date.Now.AddMonths(-4)
        Me.Txt_TitleX.Text = "Unmapped TDS Deduction Entries for TDS Paid to Govt. since " & Txt_Fr_Date.DateTime.ToShortDateString()
        Grid_Display()
        SetBalances()
        'xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        ' xPleaseWait.Show("TDS Deduction Entries for TDS paid to Govt." & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False
        Dim p1 As DataTable = Base._TDS_DBOps.GetTDS_Deducted_Not_Paid(xMID, Cen_ID, Txt_Fr_Date.DateTime)

        If p1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = p1
        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView1.OptionsBehavior.EditingMode = GridEditingMode.Inplace
        Me.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click

        If Me.GridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("TDS_Paid")
                Me.GridView1.FocusedRowHandle = 0
                SendKeys.Send("{F2}")
            Catch ex As Exception
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("TDS_Paid")
                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
            End Try
            Me.GridView1.Focus()
            RowFlag1 = True
            Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
            Me.GridView1.Focus()
        End If
        'xPleaseWait.Hide()
    End Sub

    Public _Amount As Double
    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    'Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, "Pending Internal Transfer Entries", "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                End If
                Me.GridView1.Focus()
            Case "CLOSE"
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
        End Select

    End Sub
#End Region


End Class
