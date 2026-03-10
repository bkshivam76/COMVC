Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.UI

Public Class Frm_Donation_Register

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
        'xPleaseWait.Show("Donation Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub



#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus, BUT_DON_PRINT.GotFocus, BUT_DON_REQ.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus, BUT_DON_PRINT.LostFocus, BUT_DON_REQ.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, T_Request.Click, T_Cancel.Click, BUT_DON_REQ.Click, T_FORM_PRINT.Click, BUT_DON_PRINT.Click, T_Manage_Attachment.Click, T_Link_Attachment.Click, T_Add_Attachment.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_DON_REQ" Then Me.DataNavigation("REQUEST - RECEIPT")
            If UCase(btn.Name) = "BUT_DON_PRINT" Then Me.DataNavigation("PRINT-FORM")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_REQUEST" Then Me.DataNavigation("REQUEST - RECEIPT")
            If UCase(T_btn.Name) = "T_CANCEL" Then Me.DataNavigation("REQUEST - CANCELLATION")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(T_btn.Name) = "T_FORM_PRINT" Then Me.DataNavigation("PRINT-FORM")
            If UCase(T_btn.Name) = "T_ADD_ATTACHMENT" Then Me.DataNavigation("ADD-ATTACHMENT")
            If UCase(T_btn.Name) = "T_MANAGE_ATTACHMENT" Then Me.DataNavigation("MANAGE-ATTACHMENT")
            If UCase(T_btn.Name) = "T_LINK_ATTACHMENT" Then Me.DataNavigation("LINK-ATTACHMENT")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown, But_Filter.KeyDown, But_Find.KeyDown, BUT_DON_PRINT.KeyDown, BUT_DON_REQ.KeyDown
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
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        'If e.KeyCode = Keys.Insert Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("NEW")
        'End If
        'If e.KeyCode = Keys.Enter Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("EDIT")
        'End If
        'If e.KeyCode = Keys.Delete Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("DELETE")
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
    Private Sub GridView1_GotFocus(sender As Object, e As System.EventArgs) Handles GridView1.GotFocus
        CreationDetail(Me.GridView1.FocusedRowHandle)
    End Sub
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        Try
            If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag1 Then
                    CreationDetail(e.FocusedRowHandle)
                End If
                If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 And Me.GridView1.IsFocusedView Then
                    Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
                Else
                    Me.GridView1.Tag = -1
                End If
            Else
                Me.GridView1.Tag = -1
                Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
            End If
        Catch ex As Exception
            Me.GridView1.Tag = -1
            Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        End Try
    End Sub
    Private Sub CreationDetail(ByVal Xrow As Integer)
        If Xrow >= 0 Then
            Me.Pic_Status.Visible = True : Me.Lbl_Seprator1.Visible = True : Dim Status As String = ""
            Try
                Status = Me.GridView1.GetRowCellValue(Xrow, "ACTION_STATUS").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Lbl_Status.Text = "Completed" : Me.Pic_Status.Image = My.Resources.lock : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Lbl_Status.Text = Status : Me.Pic_Status.Image = My.Resources.unlock : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If
            Dim Add_Date As String = "" : Dim Add_By As String = ""
            Try
                Add_Date = Me.GridView1.GetRowCellValue(Xrow, "REC_ADD_ON").ToString
                Add_By = Me.GridView1.GetRowCellValue(Xrow, "REC_ADD_BY").ToString()
            Catch ex As Exception
            End Try
            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.GridView1.GetRowCellValue(Xrow, "REC_EDIT_ON").ToString
                Edit_By = Me.GridView1.GetRowCellValue(Xrow, "REC_EDIT_BY").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
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

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        Me.CancelButton = Me.BUT_CLOSE
        ' GridDefaultProperty()
        GridView1.Tag = -1
        Grid_Display()
        'xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        'xPleaseWait.Show("Donation Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        RowFlag1 = False

        'A
        Dim Donation_Reg As DataTable '= Base._DonationRegister_DBOps.GetList()
        If Donation_Reg Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'B
        Dim Receipt_Print As DataTable ' = Base._DonationRegister_DBOps.GetDonationPrints()
        If Receipt_Print Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'C
        Dim Receipt_Dispatch As DataTable ' = Base._DonationRegister_DBOps.GetDonationDispatches()
        If Receipt_Dispatch Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'D
        Dim Receipt_Rejection As DataTable ' = Base._DonationRegister_DBOps.GetDonationRejections()
        If Receipt_Rejection Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'FINAL DATA
        Dim Donor_DS As DataSet = New DataSet
        Donor_DS.Tables.Add(Donation_Reg.Copy)
        Donor_DS.Tables.Add(Receipt_Print.Copy)
        Donor_DS.Tables.Add(Receipt_Dispatch.Copy)
        Donor_DS.Tables.Add(Receipt_Rejection.Copy)
        Dim Receipt_Print_Detail As DataRelation = Donor_DS.Relations.Add("Receipt Print Detail", Donor_DS.Tables("Transaction_Info").Columns("ID"), Donor_DS.Tables("Donation_Receipt_Print_Info").Columns("DR_TR_ID"), False)
        Dim Receipt_Dispatch_Detail As DataRelation = Donor_DS.Relations.Add("Receipt Dispatch Detail", Donor_DS.Tables("Transaction_Info").Columns("ID"), Donor_DS.Tables("Donation_Receipt_Dispatch_Info").Columns("DR_TR_ID"), False)
        Dim Receipt_Rejection_Detail As DataRelation = Donor_DS.Relations.Add("Receipt Rejection Detail", Donor_DS.Tables("Transaction_Info").Columns("ID"), Donor_DS.Tables("Donation_Status_info").Columns("DS_TR_ID"), False)

        Donor_DS.Tables("Donation_Receipt_Print_Info").Columns("DR_TR_ID").ColumnMapping = MappingType.Hidden
        Donor_DS.Tables("Donation_Receipt_Dispatch_Info").Columns("DR_TR_ID").ColumnMapping = MappingType.Hidden
        Donor_DS.Tables("Donation_Status_info").Columns("DS_TR_ID").ColumnMapping = MappingType.Hidden

        Me.GridControl1.DataSource = Donor_DS
        Me.GridControl1.DataMember = "Transaction_Info"

        Me.GridView1.Columns("REC_ADD_BY").Visible = False
        Me.GridView1.Columns("REC_ADD_ON").Visible = False
        Me.GridView1.Columns("REC_EDIT_BY").Visible = False
        Me.GridView1.Columns("REC_EDIT_ON").Visible = False
        Me.GridView1.Columns("ACTION_STATUS").Visible = False
        Me.GridView1.Columns("REC_STATUS_BY").Visible = False
        Me.GridView1.Columns("REC_STATUS_ON").Visible = False
        Me.GridView1.Columns("TR_AB_ID_1").Visible = False
        Me.GridView1.Columns("ID").Visible = False
        Me.GridView1.Columns("DS_STATUS_MISC_ID").Visible = False
        Me.GridView1.Columns("Date").Width = 100
        Me.GridView1.Columns("Date").FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.GridView1.Columns("Donor Name").Width = 200

        Me.GridView1.Columns("Status").Group()
        Me.GridView1.ExpandAllGroups()
        Me.GridView1.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Amount").DisplayFormat.FormatString = "#,0.00"

        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.Columns("Amount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "Grand Total: {0:#,0.00}")

        GridView1.BestFitMaxRowCount = 10 : GridView1.BestFitColumns()

        ' GridView1.SetMasterRowExpanded(GridView1.FocusedRowHandle, True)
        '--------------------------------------
        If Me.GridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Donor Name")
                If xID.Length > 0 Then
                    Me.GridView1.FocusedRowHandle = Me.GridView1.LocateByValue("ID", xID)
                    Me.GridView1.SelectRow(Me.GridView1.FocusedRowHandle)
                Else
                    Me.GridView1.FocusedRowHandle = 0 : Me.GridView1.SelectRow(0)
                End If
            Catch ex As Exception
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("ID")
                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
            End Try
            RowFlag1 = True
            Me.GridView1.Focus()
            Me.GridView1.Tag = Me.GridView1.FocusedRowHandle
        End If
        '--------------------------------------
        'xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        '----------------------------------------------------------------
        If Base.AllowMultiuser() Then
            If Action = "REQUEST - RECEIPT" Or Action = "PRINT-FORM" Or Action = "PRINT-LIST" Then
                'If Val(Me.GridView1.Tag) >= 0 Then
                For Each CurrRowHandle As Integer In Me.GridView1.GetSelectedRows
                    If Not Me.GridView1.IsDataRow(CurrRowHandle) Then Continue For
                    Dim xTemp_ID As Object = Me.GridView1.GetRowCellValue(CurrRowHandle, "ID")
                    If xTemp_ID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned(Me)
                        Exit Sub
                    End If
                    Dim Donation_Reg As DataTable = Base._DonationRegister_DBOps.GetRecDetail(xTemp_ID.ToString())
                    If Donation_Reg Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Grid_Display()
                        Exit Sub
                    End If
                    If Donation_Reg.Rows.Count = 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Grid_Display()
                        Exit Sub
                    End If
                    Dim RecEdit_Date = Convert.ToDateTime(Donation_Reg.Rows(0)("REC_EDIT_ON"))
                    If RecEdit_Date <> Me.GridView1.GetRowCellValue(CurrRowHandle, "REC_EDIT_ON") Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Grid_Display()
                        Exit Sub
                    End If
                    Dim Misc_ID = Donation_Reg.Rows(0)("DS_STATUS_MISC_ID")
                    If Misc_ID <> Me.GridView1.GetRowCellValue(CurrRowHandle, "DS_STATUS_MISC_ID").ToString() Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Donation Status Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Grid_Display()
                        Exit Sub
                    End If
                    'End If
                Next
            End If
        End If
        '----------------------------------------------------------------
        Select Case Action
            Case "REQUEST - RECEIPT"
                For Each CurrRowHandle As Integer In Me.GridView1.GetSelectedRows
                    If Not Me.GridView1.IsDataRow(CurrRowHandle) Then Continue For
                    ' If Val(Me.GridView1.Tag) >= 0 Then
                    Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "ID").ToString() : xID = xTemp_ID
                    Dim xTemp_0 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "TR_AB_ID_1").ToString()
                    Dim xTemp_1 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "Donor Name").ToString()
                    Dim xTemp_2 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "City").ToString()
                    Dim xTemp_3 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "State").ToString()
                    Dim xTemp_4 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "Country").ToString()
                    Dim xTemp_5 As String = Format(Val(Me.GridView1.GetRowCellValue(CurrRowHandle, "Amount").ToString()), "#,0.00")
                    Dim xTemp_6 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "DS_STATUS_MISC_ID").ToString()
                    Dim xTemp_7 As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "Status").ToString()


                    Dim xStatus As String = Me.GridView1.GetRowCellValue(CurrRowHandle, "ACTION_STATUS").ToString()
                    Dim xEntry As String = "Name: " & xTemp_1 & ", " & xTemp_2 & ", " & xTemp_3 & " - " & xTemp_4 & vbNewLine & _
                                           "Amount: " & xTemp_5
                    'If xStatus.Trim.ToUpper.ToString = "LOCKED" Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   a p p l y   f o r   R e c e i p t   R e q u e s t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Exit Sub
                    'End If
                    If xTemp_6.Trim.ToString <> "42189485-9b6b-430a-8112-0e8882596f3c" AndAlso xTemp_6.Trim.ToString <> "3a99fadc-b336-480d-8116-fbd144bd7671" AndAlso xTemp_6.Trim.ToString.Length > 0 Then ' Donation Accepted & " REJECT
                        DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   R e q u e s t   c a n n o t   b e   A p p l i e d . . . !" & vbNewLine & vbNewLine & "Current Status: " & xTemp_7, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    Dim Address_Book As DataTable = Base._DonationRegister_DBOps.GetAddressDetail(xTemp_0)
                    If Address_Book Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    'Dim xPromptWindow As New Common_Lib.Prompt_Window
                    'Dim ReqParam As Common_Lib.RealTimeService.Parameter_Request_Receipt = New Common_Lib.RealTimeService.Parameter_Request_Receipt
                    'If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "<u>Sure you want to apply<color=red> Receipt Request</color> of this Entry...?</u>" & vbNewLine & vbNewLine & "<size=12>" & xEntry & "</size>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._599x220, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    '    xPromptWindow.Dispose()
                    '    If xTemp_6.Trim.ToString.Length <= 0 Or xTemp_6.Trim.ToString = "3a99fadc-b336-480d-8116-fbd144bd7671" Then
                    '        ReqParam.param_InsertReceiptRequest = New Common_Lib.RealTimeService.Param_DonationRegister_InsertReceiptRequest()
                    '        ReqParam.param_InsertReceiptRequest.openYearID = Base._open_Year_ID
                    '        ReqParam.param_InsertReceiptRequest.TransactionID = xTemp_ID
                    '        'If Not Base._DonationRegister_DBOps.InsertReceiptRequest(xTemp_ID) Then
                    '        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        '    Exit Sub
                    '        'End If
                    '    Else
                    '        ReqParam.TxnID_UpdateReceiptRequest = xTemp_ID
                    '        'If Not Base._DonationRegister_DBOps.UpdateReceiptRequest(xTemp_ID) Then
                    '        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        '    Exit Sub
                    '        'End If
                    '    End If

                    '    ReqParam.TxnID_DeleteAddressBook = xTemp_ID
                    '    'If Not Base._DonationRegister_DBOps.DeleteAddressBook(xTemp_ID) Then
                    '    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    '    Exit Sub
                    '    'End If
                    '    Dim AddParam(Address_Book.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertDonationAddress_DonationRegister
                    '    Dim ctr As Integer = 0
                    '    For Each XRow In Address_Book.Rows
                    '        Dim InParam As Common_Lib.RealTimeService.Parameter_InsertDonationAddress_DonationRegister = New Common_Lib.RealTimeService.Parameter_InsertDonationAddress_DonationRegister()
                    '        InParam.TransactionID = xTemp_ID
                    '        InParam.AB_ID = XRow("REC_ID")
                    '        InParam.Name = XRow("C_NAME")
                    '        InParam.PAN = XRow("C_PAN_NO")
                    '        InParam.PassportNo = XRow("C_PASSPORT_NO")
                    '        InParam.Add1 = XRow("C_R_ADD1")
                    '        InParam.Add2 = XRow("C_R_ADD2")
                    '        InParam.Add3 = XRow("C_R_ADD3")
                    '        InParam.Add4 = XRow("C_R_ADD4")
                    '        InParam.CityID = IIf(IsDBNull(XRow("C_R_CITY_ID")), "", XRow("C_R_CITY_ID"))
                    '        InParam.DistrictID = IIf(IsDBNull(XRow("C_R_DISTRICT_ID")), "", XRow("C_R_DISTRICT_ID"))
                    '        InParam.StateID = IIf(IsDBNull(XRow("C_R_STATE_ID")), "", XRow("C_R_STATE_ID"))
                    '        InParam.CountryID = XRow("C_R_COUNTRY_ID")
                    '        InParam.PinCode = XRow("C_R_PINCODE")
                    '        'If Not Base._DonationRegister_DBOps.InsertDonationAddress(InParam) Then
                    '        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        '    Exit Sub
                    '        'End If
                    '        AddParam(ctr) = InParam
                    '        ctr += 1
                    '    Next
                    '    ReqParam.InAddress = AddParam
                    '    If Not Base._DonationRegister_DBOps.RequestAReceipt(ReqParam) Then
                    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    '    DevExpress.XtraEditors.XtraMessageBox.Show("R e q u e s t   A p p l i e d   S u c c e s s f u l l y" & vbNewLine & vbNewLine & xEntry, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    '    xPromptWindow.Dispose()
                    'End If
                    ''Else
                    ''DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   n o t   s e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ''End If
                Next
                Grid_Display()
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-FORM"
                If Me.GridView1.GetSelectedRows.Count > 1 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Please select Single Record to Print donation Form!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Val(Me.GridView1.Tag) >= 0 Then
                    Dim country As String = ""
                    Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Val(Me.GridView1.Tag), "ID").ToString() : xID = xTemp_ID
                    Dim isPan As New Boolean() : isPan = False
                    Dim SQL_STR1 As String = "" : Dim CENID As String = "" : Dim Ins As String = ""
                    Dim TI_Table As DataTable = Base._DonationRegister_DBOps.GetRecDetail(xTemp_ID)
                    If TI_Table Is Nothing Then
                        If TI_Table Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        Exit Sub
                    End If
                    Dim data As New Donation()
                    Dim ListDataSource As New ArrayList()
                    Dim TransactionInfo As DataRow = TI_Table.Rows(0)
                    data.Finacial_Year = Base._open_Year_Name
                    Dim DonationDate As Date = TransactionInfo("TR_DATE")
                    data.DateOf_Donation = DonationDate.ToString(Base._Date_Format_Current)
                    data.Transaction_Mode = TransactionInfo("TR_MODE").ToString()
                    data.Check_No = TransactionInfo("TR_REF_NO").ToString()
                    data.Total_Amount = TransactionInfo("TR_AMOUNT").ToString()
                    Dim InWords As New Common_Lib.Common
                    'data.Amount_InWord = InWords.ConvertNumToAlphaValue(TransactionInfo("TR_AMOUNT").ToString()).ToUpper() & " ONLY."
                    Dim Amount As Double = Convert.ToDouble(TransactionInfo("TR_AMOUNT").ToString())
                    data.Amount_InWord = Base.ConvertNumToAlphaValue(Convert.ToDecimal(Amount))
                    'If ((Amount - Math.Floor(Amount)) > 0) Then
                    '    data.Amount_InWord += " and " + (Base.ConvertNumToAlphaValue(Convert.ToInt64(100 * (Amount - Math.Floor(Amount))))).ToString() + " paise"
                    'End If
                    'data.Amount_InWord += " only"
                    data.Branch_Name = TransactionInfo("TR_REF_BRANCH").ToString()
                    CENID = TransactionInfo("TR_CEN_ID").ToString()

                    TI_Table = Base._DonationRegister_DBOps.GetCenterDetails(CENID)
                    If TI_Table Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    Dim centreInfo As DataRow = TI_Table.Rows(0)
                    data.Centre_Name = centreInfo("CEN_NAME").ToString()
                    data.Centre_BKPad = "{ " & centreInfo("CEN_UID").ToString() & " }" & " / ( " & centreInfo("CEN_PAD_NO").ToString() & " )"
                    Ins = centreInfo("CEN_INS_ID").ToString()

                    Dim miscId As Object = Base._DonationRegister_DBOps.GetMiscNameByID(TransactionInfo("DS_STATUS_MISC_ID").ToString())
                    If miscId Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim bank As Object = Base._DonationRegister_DBOps.GetBankNameByID(TransactionInfo("TR_REF_BANK_ID").ToString())
                    If (Not bank Is Nothing) Then
                        ' bank = TI_Table.Rows(0)(0).ToString()
                        data.Bank_Name = bank
                    End If

                    If (Not (miscId.ToString.ToLower.Equals("donation accepted") Or miscId.ToString.ToLower.Equals("receipt request rejected"))) Then
                        TI_Table = Base._DonationRegister_DBOps.GetAddressDetail_Form(False, xTemp_ID)
                    Else
                        TI_Table = Base._DonationRegister_DBOps.GetAddressDetail_Form(True, Nothing, TransactionInfo("TR_AB_ID_1").ToString())
                    End If
                    If TI_Table Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    Dim addressInfo As DataRow
                    If (TI_Table.Rows.Count > 0) Then
                        addressInfo = TI_Table.Rows(0)
                        data.Full_Name = addressInfo("C_NAME").ToString()
                        data.Address_1 = data.AddComma(addressInfo("C_R_ADD1").ToString()) & data.AddComma(addressInfo("C_R_ADD2").ToString()) & data.AddComma(addressInfo("C_R_ADD3").ToString()) & data.AddComma(addressInfo("C_R_ADD4").ToString())
                        data.Pan_No = addressInfo("C_PAN_NO").ToString()
                        If (Not String.IsNullOrEmpty(data.Pan_No)) Then isPan = True
                        If (Not String.IsNullOrEmpty(addressInfo("C_R_CITY_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetCityList("'" & addressInfo("C_R_CITY_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim cityInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & " City:" & data.AddComma(cityInfo("CI_NAME").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_R_DISTRICT_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetDistrictList("'" & addressInfo("C_R_DISTRICT_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim districtInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & " Dist:" & data.AddComma(districtInfo("DI_NAME").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_R_STATE_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetStateList("'" & addressInfo("C_R_STATE_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim stateInfo As DataRow = TI_Table.Rows(0)
                            Dim PinCode As String = IIf(addressInfo("C_R_PINCODE").ToString().Length > 0, "-" & addressInfo("C_R_PINCODE").ToString(), "")
                            data.Address_1 = data.Address_1 & " State:" & data.AddComma(stateInfo("ST_NAME").ToString() & PinCode)
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_R_COUNTRY_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetCountryList("'" & addressInfo("C_R_COUNTRY_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim countryInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & countryInfo("CO_NAME").ToString()
                            country = countryInfo(0).ToString()
                        End If
                        Try 'the office address need to be taken
                            If (Not String.IsNullOrEmpty(addressInfo("C_AB_ID").ToString())) Then
                                TI_Table = Base._DonationRegister_DBOps.GetOfficeAddressDetail(addressInfo("C_AB_ID").ToString())
                                If TI_Table Is Nothing Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                                addressInfo = TI_Table.Rows(0)
                            End If
                        Catch ex As Exception
                        End Try

                        If (Not String.IsNullOrEmpty(addressInfo("C_O_ADD1").ToString())) Then
                            data.Office_Address = data.AddComma(addressInfo("C_O_ADD1").ToString()) & data.AddComma(addressInfo("C_O_ADD2").ToString()) & data.AddComma(addressInfo("C_O_ADD3").ToString()) & data.AddComma(addressInfo("C_O_ADD4").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_O_DISTRICT_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetDistrictList("'" & addressInfo("C_O_DISTRICT_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim districtInfo As DataRow = TI_Table.Rows(0)
                            data.Office_Address = data.Office_Address & data.AddComma(districtInfo("DI_NAME").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_O_CITY_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetCityList("'" & addressInfo("C_O_CITY_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim cityInfo As DataRow = TI_Table.Rows(0)
                            data.Office_Address = data.Office_Address & data.AddComma(cityInfo("CI_NAME").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_O_STATE_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetStateList("'" & addressInfo("C_O_STATE_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim stateInfo As DataRow = TI_Table.Rows(0)
                            data.Office_Address = data.Office_Address & data.AddComma(stateInfo("ST_NAME").ToString())
                        End If
                        If (Not String.IsNullOrEmpty(addressInfo("C_O_COUNTRY_ID").ToString())) Then
                            TI_Table = Base._DonationRegister_DBOps.GetCountryList("'" & addressInfo("C_O_COUNTRY_ID").ToString() & "'")
                            If TI_Table Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim countryInfo As DataRow = TI_Table.Rows(0)
                            data.Office_Address = data.Office_Address & countryInfo("CO_NAME").ToString()
                        End If
                    End If
                    Select Case Ins
                        Case "00001"
                            Dim DonationForm As New BKDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                        Case "00002"
                            If (String.Equals(TransactionInfo("TR_CODE").ToString(), "6")) Then
                                TI_Table = Base._DonationRegister_DBOps.GetForeignDonationDetail(xTemp_ID)
                                If TI_Table Is Nothing Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                                Dim foreignDonationInfo As DataRow = TI_Table.Rows(0)
                                Dim DonationForm As New WRSTDonationNRIForm()
                                DonationForm.XrCheck.Text = data.Transaction_Mode
                                DonationForm.XrCheck1.Text = data.Transaction_Mode
                                DonationForm.XrCheck.CheckState = CheckState.Checked
                                DonationForm.XrCheck1.CheckState = CheckState.Checked
                                DonationForm.XrFCurrency.Text = foreignDonationInfo("TR_FOREIGN_AMT").ToString()
                                DonationForm.XrFCurrency1.Text = foreignDonationInfo("TR_FOREIGN_AMT").ToString()
                                data.Total_Amount = foreignDonationInfo("TR_INR_AMT")
                                TI_Table = Base._DonationRegister_DBOps.GetcurrencyByID(foreignDonationInfo("TR_CUR_ID").ToString())
                                If TI_Table Is Nothing Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                                Dim currencyInfo As DataRow = TI_Table.Rows(0)
                                DonationForm.XrCurrencyName.Text = currencyInfo("CUR_NAME").ToString()
                                DonationForm.XrCurrencyName1.Text = currencyInfo("CUR_NAME").ToString()
                                data.Address_1 = country
                                ListDataSource.Add(data)
                                DonationForm.DataSource = ListDataSource
                                DonationForm.ShowPreview()
                            Else
                                Dim DonationForm As New WRSTDonationIndianForm()
                                DonationForm.XrCheck.Text = data.Transaction_Mode
                                DonationForm.XrCheck1.Text = data.Transaction_Mode
                                DonationForm.XrCheck.CheckState = CheckState.Checked
                                DonationForm.XrCheck1.CheckState = CheckState.Checked
                                If (isPan) Then
                                    DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                    DonationForm.XrYesPan.CheckState = CheckState.Checked
                                End If
                                ListDataSource.Add(data)
                                DonationForm.DataSource = ListDataSource
                                DonationForm.ShowPreview()
                            End If
                        Case "00003"
                            Dim DonationForm As New RERFDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                        Case "00004"
                            Dim DonationForm As New BKESDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                        Case "00005"
                            Dim DonationForm As New TSRTDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                        Case "00006"
                            Dim DonationForm As New SSAFDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                        Case "00008"
                            Dim DonationForm As New RMCSDonationForm()
                            DonationForm.XrCheck.Text = data.Transaction_Mode
                            DonationForm.XrCheck1.Text = data.Transaction_Mode
                            DonationForm.XrCheck.CheckState = CheckState.Checked
                            DonationForm.XrCheck1.CheckState = CheckState.Checked
                            If (isPan) Then
                                DonationForm.XrYesPan1.CheckState = CheckState.Checked
                                DonationForm.XrYesPan.CheckState = CheckState.Checked
                            End If
                            ListDataSource.Add(data)
                            DonationForm.DataSource = ListDataSource
                            DonationForm.ShowPreview()
                    End Select
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   n o t   s e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "ADD-ATTACHMENT"
                If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                    Dim xSource As String = IIf(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Kind of Donation").ToString() = "Regular", "Donation_Regular", "Donation_Foreign")
                    Add_Attachment(Me.Txt_TitleX.Text, xSource, Me, xTemp_ID)
                End If
            Case "MANAGE-ATTACHMENT"
                If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                    Dim xSource As String = IIf(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Kind of Donation").ToString() = "Regular", "Donation_Regular", "Donation_Foreign")
                    Manage_Attachment(Me.Txt_TitleX.Text, xSource, Me, xTemp_ID)
                End If
            Case "LINK-ATTACHMENT"
                If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                    Dim xSource As String = IIf(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Kind of Donation").ToString() = "Regular", "Donation_Regular", "Donation_Foreign")
                    Link_Attachment(Me.Txt_TitleX.Text, xSource, Me, xTemp_ID)
                End If
            Case "PRINT-LIST"
                If Me.GridView1.RowCount > 0 Then
                    'Base.Show_ListPreview(GridControl1, Me.Text, Me, True, Printing.PaperKind.A4, Me.Text, "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                End If
                Me.GridView1.Focus()
            Case "FILTER"
                If Me.GridView1.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = ConnectOne.D0010._001.My.Resources.bluesearch
                    Me.GridView1.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = Global.ConnectOne.D0010._001.My.Resources.blueaccept
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
