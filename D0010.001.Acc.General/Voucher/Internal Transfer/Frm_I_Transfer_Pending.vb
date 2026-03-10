Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_I_Transfer_Pending

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
        xPleaseWait.Show("Pending Internal Transfer Entries" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        Set_Default()
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_ACCEPT.GotFocus, BUT_CANCEL.GotFocus, BUT_REJECT.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_ACCEPT.LostFocus, BUT_CANCEL.LostFocus, BUT_REJECT.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T_Close.Click, T_Print.Click, BUT_ACCEPT.Click, BUT_CANCEL.Click, BUT_REJECT.Click, T_ACCEPT.Click, T_REJECT.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_ACCEPT" Then Me.DataNavigation("ACCEPT")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_ACCEPT" Then Me.DataNavigation("ACCEPT")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_ACCEPT.KeyDown, BUT_CANCEL.KeyDown, BUT_REJECT.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
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
        Me.DataNavigation("ACCEPT")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        'If e.KeyCode = Keys.Insert Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("NEW")
        'End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("ACCEPT")
        End If
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
        Catch ex As Exception
            Me.GridView1.Tag = -1
        End Try
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
        Me.CancelButton = Me.BUT_CANCEL
        GridDefaultProperty()
        If Base._open_Cen_ID = 4216 Then
            FillCentres()
        Else
            Grid_Display() : GLookUp_Cen_List.Visible = False : LabelControl1.Visible = False : Me.SimpleButton1.Visible = False
        End If

        'Grid_Display()
        xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        xPleaseWait.Show("Pending Internal Transfer Entries" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False
        Dim p1 As DataSet
        If Base._open_Cen_ID = 4216 Then
            Dim CenID As Integer
            If Me.GLookUp_Cen_ListView.GetRowCellValue(Me.GLookUp_Cen_ListView.FocusedRowHandle, "TO_CEN_NAME") <> "All Centres" Then
                CenID = Me.GLookUp_Cen_ListView.GetRowCellValue(Me.GLookUp_Cen_ListView.FocusedRowHandle, "TO_CEN_ID")
            End If
            p1 = Base._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, CenID)
        Else
            p1 = Base._Internal_Tf_Voucher_DBOps.GetUnMatchedList(0, Nothing)
        End If
        If p1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = p1.Tables(0)
        Me.GridView1.Columns("ITEM_ID").Visible = False
        Me.GridView1.Columns("CEN_ID").Visible = False
        Me.GridView1.Columns("BI_ID").Visible = False
        Me.GridView1.Columns("BI_ID").Visible = False
        Me.GridView1.Columns("PUR_ID").Visible = False
        Me.GridView1.Columns("ID").Visible = False
        Me.GridView1.Columns("M_ID").Visible = False
        Me.GridView1.Columns("Status").Visible = False

        Me.GridView1.Columns("REF_BI_ID").Visible = False
        Me.GridView1.Columns("Ref.Branch").Visible = False
        Me.GridView1.Columns("Ref.Others").Visible = False
        Me.GridView1.Columns("Ref_Bank_AccNo").Visible = False 'used in DD credit case.  #bug 5539 fix

        Me.GridView1.Columns("Add By").Visible = False
        Me.GridView1.Columns("Add Date").Visible = False
        Me.GridView1.Columns("Edit By").Visible = False
        Me.GridView1.Columns("Edit Date").Visible = False
        Me.GridView1.Columns("Action Status").Visible = False
        Me.GridView1.Columns("Action By").Visible = False
        Me.GridView1.Columns("Action Date").Visible = False

        Me.GridView1.Columns("Description").Group()
        Me.GridView1.ExpandAllGroups()
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        Me.GridView1.Columns("Centre Name").Width = 200

        If Me.GridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Centre Name")
                'Me.GridView1.MoveBy(Me.GridView1.LocateByValue("Amount", xID))
            Catch ex As Exception
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Centre Name")
                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
            End Try
            Me.GridView1.Focus()
            RowFlag1 = True
            Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
            Me.GridView1.Focus()
        End If
        xPleaseWait.Hide()
    End Sub

    Public _Date, _Item_ID, _Mode, _CEN_ID, _BI_ID, _REF_BI_ID, _BI_BRANCH, _REF_BRANCH, _REF_OTHERS, _BI_ACC_NO, _BI_REF_NO, _BI_REF_DT, _PUR_ID, _ID, _M_ID, _REF_BANK_ACC_NO, _EDIT_DATE As String
    Public _Amount As Double
    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "ACCEPT"
                If Val(Me.GridView1.Tag) >= 0 Then
                    _Date = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Date").ToString()
                    _Item_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "ITEM_ID").ToString()
                    _Mode = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Mode").ToString()
                    _CEN_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "CEN_ID").ToString()
                    _BI_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "BI_ID").ToString()
                    _BI_BRANCH = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Branch Name").ToString()
                    _BI_ACC_NO = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Bank A/c. No.").ToString()

                    _REF_BI_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "REF_BI_ID").ToString()
                    _REF_BRANCH = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Ref.Branch").ToString()
                    _REF_OTHERS = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Ref.Others").ToString()

                    _BI_REF_NO = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Ref.No.").ToString()
                    _BI_REF_DT = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Ref.Date").ToString()
                    _Amount = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Amount").ToString()
                    _PUR_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "PUR_ID").ToString()
                    _ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "ID").ToString()
                    _M_ID = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "M_ID").ToString()
                    _EDIT_DATE = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Edit Date").ToString()
                    _REF_BANK_ACC_NO = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "Ref_Bank_AccNo").ToString()  'used in DD credit case. #bug 5539 fix
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r n a l   T r a n s f e r   E n t r y   N o t   S e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, "Pending Internal Transfer Entries", "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                End If
                Me.GridView1.Focus()
            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

    Public Sub FillCentres()
        Dim Centres As DataTable = Base._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "", "")
        If Centres Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(Centres) ': dview.Sort = "TO_CEN_NAME,TO_UID"
        Dim ROW As DataRow : ROW = Centres.NewRow() : ROW("TO_CEN_NAME") = "All Centres" : dview.Table.Rows.InsertAt(ROW, 0)
        dview.Sort = "TO_CEN_NAME"
        Me.GLookUp_Cen_List.Properties.ValueMember = "TO_CEN_ID"
        Me.GLookUp_Cen_List.Properties.DisplayMember = "TO_CEN_NAME"
        Me.GLookUp_Cen_List.Properties.DataSource = dview
        Me.GLookUp_Cen_ListView.RefreshData()
        Me.GLookUp_Cen_List.Properties.Tag = "SHOW"
        'If dview.Count <= 0 Then
        'Me.GLookUp_Cen_List.Properties.Tag = "NONE"
        'End If
    End Sub

    Private Sub GLookUp_Cen_List_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_Cen_List.KeyDown
        If e.KeyCode = Keys.Enter Then
            Grid_Display()
        End If
    End Sub
#End Region

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Grid_Display()
    End Sub
End Class
