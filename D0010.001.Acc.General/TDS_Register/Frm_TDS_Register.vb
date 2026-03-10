Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Card

Public Class Frm_TDS_Register

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common

    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Dim xID As String = ""
    Dim xRecID As String = ""
    Dim xPaidID As String = ""
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
        'xPleaseWait.Show("TDS Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
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
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, T_MAP_RECD.Click, T_MAP_PAID.Click, T_UNMAP_RECD.Click, T_UNMAP_PAID.Click, T_UNMAP_DED.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_MAP_PAID" Then Me.DataNavigation("Map-TDS-Paid")
            If UCase(T_btn.Name) = "T_UNMAP_DED" Then Me.DataNavigation("UnMap-TDS-Ded")
            If UCase(T_btn.Name) = "T_UNMAP_PAID" Then Me.DataNavigation("UnMap-TDS-Paid")
            If UCase(T_btn.Name) = "T_MAP_RECD" Then Me.DataNavigation("Map-TDS-Recd")
            If UCase(T_btn.Name) = "T_UNMAP_RECD" Then Me.DataNavigation("UnMap-TDS-Recd")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown, But_Filter.KeyDown, But_Find.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Grid Events"
    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.BandedGridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub GridDefaultProperty()
        Me.BandedGridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.BandedGridView1.OptionsBehavior.Editable = False

        Me.BandedGridView1.OptionsDetail.AllowZoomDetail = True
        Me.BandedGridView1.OptionsDetail.AutoZoomDetail = True

        Me.BandedGridView1.OptionsNavigation.EnterMoveNextColumn = True

        Me.BandedGridView1.OptionsSelection.InvertSelection = True

        Me.BandedGridView1.OptionsView.ColumnAutoWidth = False
        Me.BandedGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.BandedGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.BandedGridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.BandedGridView1.OptionsView.ShowGroupPanel = False
        Me.BandedGridView1.OptionsView.ShowAutoFilterRow = False
    End Sub
    Private Sub BandedGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
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
    Private Sub BandedGridView1_GotFocus(sender As Object, e As System.EventArgs)
        CreationDetail(Me.BandedGridView1.FocusedRowHandle)
    End Sub
    Private Sub BandedGridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

        Try
            If Me.BandedGridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag1 Then
                    CreationDetail(e.FocusedRowHandle)
                End If
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 And Me.BandedGridView1.IsFocusedView Then
                    Me.BandedGridView1.Tag = Me.BandedGridView1.FocusedRowHandle
                Else
                    Me.BandedGridView1.Tag = -1
                End If
            Else
                Me.BandedGridView1.Tag = -1
                Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
            End If
        Catch ex As Exception
            Me.BandedGridView1.Tag = -1
            Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        End Try
    End Sub
    Private Sub CreationDetail(ByVal Xrow As Integer)
        If Xrow >= 0 Then
            Me.Pic_Status.Visible = True : Me.Lbl_Seprator1.Visible = True : Dim Status As String = ""
            Try
                Status = Me.BandedGridView1.GetRowCellValue(Xrow, "ACTION_STATUS").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Lbl_Status.Text = "Completed" : Me.Pic_Status.Image = My.Resources.lock : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Lbl_Status.Text = Status : Me.Pic_Status.Image = My.Resources.unlock : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If
            Dim Add_Date As String = "" : Dim Add_By As String = ""
            Try
                Add_Date = Me.BandedGridView1.GetRowCellValue(Xrow, "REC_ADD_ON").ToString
                Add_By = Me.BandedGridView1.GetRowCellValue(Xrow, "REC_ADD_BY").ToString()
            Catch ex As Exception
            End Try
            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.BandedGridView1.GetRowCellValue(Xrow, "REC_EDIT_ON").ToString
                Edit_By = Me.BandedGridView1.GetRowCellValue(Xrow, "REC_EDIT_BY").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
        End If
    End Sub

    Private Sub BandedGridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles BandedGridView1.ShowCustomizationForm
        Try
            Me.ColumnFormVisibleFlag1 = True
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
        Catch ex As Exception
        End Try
    End Sub
    Private Sub BandedGridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles BandedGridView1.HideCustomizationForm
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
                    Me.BandedGridView1.DestroyCustomization()
                    e.Button.Hint = "Show Column Chooser"
                    e.Button.ImageIndex = 6
                Else
                    Me.BandedGridView1.ColumnsCustomization()
                    e.Button.Hint = "Hide Column Chooser"
                    e.Button.ImageIndex = 10
                End If

            Case "GROUP_BOX"
                If Me.BandedGridView1.OptionsView.ShowGroupPanel Then
                    Me.BandedGridView1.OptionsView.ShowGroupPanel = False
                    e.Button.Hint = "Show Group Box"
                    e.Button.ImageIndex = 8
                Else
                    Me.BandedGridView1.OptionsView.ShowGroupPanel = True
                    e.Button.Hint = "Hide Group Box"
                    e.Button.ImageIndex = 16
                End If
            Case "GROUPED_COL"
                If Me.BandedGridView1.OptionsView.ShowGroupedColumns Then
                    Me.BandedGridView1.OptionsView.ShowGroupedColumns = False
                    e.Button.Hint = "Show Group Column"
                    e.Button.ImageIndex = 14
                Else
                    Me.BandedGridView1.OptionsView.ShowGroupedColumns = True
                    e.Button.Hint = "Hide Grouped Column"
                    e.Button.ImageIndex = 17
                End If
            Case "FOOTER_BAR"
                If Me.BandedGridView1.OptionsView.ShowFooter Then
                    Me.BandedGridView1.OptionsView.ShowFooter = False
                    e.Button.Hint = "Show Footer Bar"
                    e.Button.ImageIndex = 18
                Else
                    Me.BandedGridView1.OptionsView.ShowFooter = True
                    e.Button.Hint = "Hide Footer Bar"
                    e.Button.ImageIndex = 13
                End If
            Case "GROUP_FOOTER"
                If Me.BandedGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
                    Me.BandedGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
                    e.Button.Hint = "Show Group Footer Bar"
                Else
                    Me.BandedGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    e.Button.Hint = "Hide Group Footer Bar"
                End If

            Case "FILTER"
                Me.BandedGridView1.ShowFilterEditor(Me.BandedGridView1.FocusedColumn)
        End Select
    End Sub
    Private Sub BandedGridView1_CellMerge(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles BandedGridView1.CellMerge
        If Not e.Column Is BandedGridView1.Columns("TDS_DED") _
            And Not e.Column Is BandedGridView1.Columns("RECD_TDS") _
            And Not e.Column Is BandedGridView1.Columns("PAID_TDS") Then
            e.Merge = False : e.Handled = True : Exit Sub
        End If

        Dim _Ded_M_ID_1 As Object = BandedGridView1.GetRowCellValue(e.RowHandle1, BandedGridView1.Columns("DED_TR_M_ID"))
        Dim _Ded_M_ID_2 As Object = BandedGridView1.GetRowCellValue(e.RowHandle2, BandedGridView1.Columns("DED_TR_M_ID"))

        If e.Column Is BandedGridView1.Columns("TDS_DED") Then
            Dim _Ded1 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column))
            Dim _Ded2 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column))
            If _Ded1 = _Ded2 And _Ded_M_ID_1 = _Ded_M_ID_2 Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
            Exit Sub
        End If

        Dim _Recd_M_ID_1 As Object = BandedGridView1.GetRowCellValue(e.RowHandle1, BandedGridView1.Columns("RECD_TDS_M_ID"))
        Dim _Recd_M_ID_2 As Object = BandedGridView1.GetRowCellValue(e.RowHandle2, BandedGridView1.Columns("RECD_TDS_M_ID"))

        If e.Column Is BandedGridView1.Columns("RECD_TDS") Then
            If Not IsDBNull(_Recd_M_ID_1) And Not IsDBNull(_Recd_M_ID_2) Then
                Dim _Recd1 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column))
                Dim _Recd2 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column))
                If _Ded_M_ID_1 = _Ded_M_ID_2 And Not _Ded_M_ID_1.ToString.Contains("DEDUCTION NOT MAPPED") Then
                    e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True
                End If
                Exit Sub
            Else
                e.Merge = False : e.Handled = True
            End If
        End If

        Dim _Paid_M_ID_1 As Object = BandedGridView1.GetRowCellValue(e.RowHandle1, BandedGridView1.Columns("PAID_TDS_M_ID"))
        Dim _Paid_M_ID_2 As Object = BandedGridView1.GetRowCellValue(e.RowHandle2, BandedGridView1.Columns("PAID_TDS_M_ID"))

        If e.Column Is BandedGridView1.Columns("PAID_TDS") Then
            If Not IsDBNull(_Paid_M_ID_1) And Not IsDBNull(_Paid_M_ID_2) Then
                Dim _Paid1 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column))
                Dim _Paid2 As Decimal = CDec(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column))
                If _Ded_M_ID_1 = _Ded_M_ID_2 And Not _Ded_M_ID_1.ToString.Contains("DEDUCTION NOT MAPPED") Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
                Exit Sub
            Else
                e.Merge = False : e.Handled = True
            End If
        End If
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        Me.CancelButton = Me.BUT_CLOSE
        ' GridDefaultProperty()
        BandedGridView1.Tag = -1
        Grid_Display()
        'xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        '  xPleaseWait.Show("TDS Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Seprator1.Visible = False
        RowFlag1 = False

        'A
        Dim TDS_Reg As DataTable = Base._TDS_DBOps.GetTDSRegister()
        If TDS_Reg Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Me.GridControl1.DataSource = TDS_Reg
        Me.BandedGridView1.OptionsView.AllowCellMerge = True
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Dim condition1 As StyleFormatCondition = New StyleFormatCondition() : condition1.Appearance.BackColor = Color.Linen : condition1.Appearance.Options.UseBackColor = True 'Color.OldLace
        'condition1.Condition = FormatConditionEnum.Expression : condition1.Expression = "[iTR_REF_NO]%2=0" : condition1.ApplyToRow = True
        'BandedGridView1.FormatConditions.Add(condition1)

        'Dim condition2 As StyleFormatCondition = New StyleFormatCondition() : condition2.Appearance.BackColor = Color.AliceBlue : condition2.Appearance.Options.UseBackColor = True
        'condition2.Condition = FormatConditionEnum.Expression : condition2.Expression = "[iTR_REF_NO]%2<>0" : condition2.ApplyToRow = True
        'BandedGridView1.FormatConditions.Add(condition2)
        '--------------------------------------------------------------------------------------------------------------------------------------
        If Me.BandedGridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("Center")
                xID = xID.Replace("DEDUCTION NOT MAPPED% %", "")
                If xID.Length > 0 Or xRecID.Length > 0 Or xPaidID.Length > 0 Then
                    Dim Cols() As GridColumn = New GridColumn() {BandedGridView1.Columns("DED_TR_M_ID"), BandedGridView1.Columns("RECD_TDS_M_ID"), BandedGridView1.Columns("PAID_TDS_M_ID")}
                    Dim Values() As Object = New Object() {IIf(xID = "", DBNull.Value, xID), IIf(xRecID = "", DBNull.Value, xRecID), IIf(xPaidID = "", DBNull.Value, xPaidID)}
                    Me.BandedGridView1.FocusedRowHandle = LocateRowByMultipleValues(BandedGridView1, Cols, Values, 0)  ' Me.BandedGridView1.LocateByValue("ID", xID)

                    If Me.BandedGridView1.FocusedRowHandle <= 0 Then
                        If xPaidID.Length > 0 Then
                            Me.BandedGridView1.FocusedRowHandle = Me.BandedGridView1.LocateByValue("PAID_TDS_M_ID", xPaidID)
                        End If
                        If xRecID.Length > 0 Then
                            Me.BandedGridView1.FocusedRowHandle = Me.BandedGridView1.LocateByValue("RECD_TDS_M_ID", xRecID)
                        End If
                        If xID.Length > 0 Then
                            Me.BandedGridView1.FocusedRowHandle = Me.BandedGridView1.LocateByValue("DED_TR_M_ID", xID)
                        End If
                    End If
                    Me.BandedGridView1.SelectRow(Me.BandedGridView1.FocusedRowHandle)
                    xID = "" : xRecID = "" : xPaidID = ""
                Else
                    Me.BandedGridView1.FocusedRowHandle = 0 : Me.BandedGridView1.SelectRow(0)
                End If
            Catch ex As Exception
                Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("ID")
                Me.BandedGridView1.FocusedRowHandle = BandedGridView1.RowCount - 1
            End Try
            RowFlag1 = True
            Me.BandedGridView1.Focus()
        End If

        'xPleaseWait.Hide()
    End Sub

    Public Function LocateRowByMultipleValues(ByVal View As ColumnView, ByVal Columns As GridColumn(), ByVal Values As Object(), ByVal StartRowHandle As Integer) As Integer
        ' Check whether or not the arrays have the same length 
        If Columns.Length <> Values.Length Then
            Return DevExpress.XtraGrid.GridControl.InvalidRowHandle
        End If
        ' Obtain the number of data rows within the view 
        Dim DataRowCount As Integer
        If (TypeOf (View) Is CardView) Then
            DataRowCount = View.RowCount
        Else
            DataRowCount = CType(View, GridView).DataRowCount
        End If
        ' Traverse the data rows to find a match 
        Dim Match As Boolean
        Dim CurrValue As Object
        Dim CurrentRowHandle, I As Integer
        For CurrentRowHandle = StartRowHandle To DataRowCount - 1
            Match = True
            For I = 0 To Columns.Length - 1
                CurrValue = View.GetRowCellValue(CurrentRowHandle, Columns(I))
                If Columns(I).Caption = "PAID_TDS_M_ID" Then
                    CurrValue = CurrValue
                End If
                If Not CurrValue.Equals(Values(I)) Then Match = False
            Next
            If Match Then Return CurrentRowHandle
        Next
        ' Return the invalid row handle if no matches are found 
        Return DevExpress.XtraGrid.GridControl.InvalidRowHandle
    End Function

    Public Sub DataNavigation(ByVal Action As String)
        '----------------------------------------------------------------
        'If Base.AllowMultiuser() Then
        '    If Action = "REQUEST - RECEIPT" Or Action = "PRINT-FORM" Or Action = "PRINT-LIST" Then
        '        'If Val(Me.BandedGridView1.Tag) >= 0 Then
        '        For Each CurrRowHandle As Integer In Me.BandedGridView1.GetSelectedRows
        '            If Not Me.BandedGridView1.IsDataRow(CurrRowHandle) Then Continue For
        '            Dim xTemp_ID As Object = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "ID")
        '            If xTemp_ID Is Nothing Then
        '                Base.HandleDBError_OnNothingReturned(Me)
        '                Exit Sub
        '            End If
        '            Dim Donation_Reg As DataTable = Base._DonationRegister_DBOps.GetRecDetail(xTemp_ID.ToString())
        '            If Donation_Reg Is Nothing Then
        '                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Grid_Display()
        '                Exit Sub
        '            End If
        '            If Donation_Reg.Rows.Count = 0 Then
        '                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Grid_Display()
        '                Exit Sub
        '            End If
        '            Dim RecEdit_Date = Convert.ToDateTime(Donation_Reg.Rows(0)("REC_EDIT_ON"))
        '            If RecEdit_Date <> Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "REC_EDIT_ON") Then
        '                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Changed / Removed in Background!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Grid_Display()
        '                Exit Sub
        '            End If
        '            Dim Misc_ID = Donation_Reg.Rows(0)("DS_STATUS_MISC_ID")
        '            If Misc_ID <> Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "DS_STATUS_MISC_ID").ToString() Then
        '                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Donation Status Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Grid_Display()
        '                Exit Sub
        '            End If
        '            'End If
        '        Next
        '    End If
        'End If
        '----------------------------------------------------------------
        If Me.BandedGridView1.FocusedRowHandle >= 0 Then
            xID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DED_TR_M_ID").ToString()
            xRecID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID").ToString()
            xPaidID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID").ToString()
        End If
        Select Case Action
            Case "UnMap-TDS-Recd"
                If Me.BandedGridView1.FocusedRowHandle >= 0 Then
                    If IsDBNull(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Received from Center' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Base._TDS_DBOps.UnmapTDSRecd(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID").ToString())
                    xRecID = ""
                    Grid_Display()
                End If
            Case "Map-TDS-Recd"
                If Me.BandedGridView1.FocusedRowHandle >= 0 Then
                    If IsDBNull(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Received from Center' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim xfrom_TDs_ded As New Frm_TDS_Reg_Map_TDS_Recd
                    xfrom_TDs_ded.MainBase = Base : xfrom_TDs_ded.xMID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID").ToString()
                    xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS").ToString())
                    If xfrom_TDs_ded.lblMentionedInvoucher.EditValue = 0 Then xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "INTEREST_RECD_ON_TDS").ToString())
                    If xfrom_TDs_ded.lblMentionedInvoucher.EditValue = 0 Then xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PENALTY_RECD_ON_TDS").ToString())
                    xfrom_TDs_ded.Cen_ID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "CEN_ID").ToString()
                    xfrom_TDs_ded.Cen_Name = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Center").ToString()
                    xfrom_TDs_ded.ShowDialog()
                    If xfrom_TDs_ded.DialogResult = DialogResult.Retry Or xfrom_TDs_ded.DialogResult = DialogResult.Cancel Then
                        xfrom_TDs_ded.Dispose() : Exit Sub
                    End If

                    Dim inTDSParam(xfrom_TDs_ded.TDS_Deduction_List.Count) As Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer
                    Dim ctr As Integer = 0
                    For Each cParam As Frm_TDS_Reg_Map_TDS_Recd.Out_TDS In xfrom_TDs_ded.TDS_Deduction_List
                        Dim inTDSDeduct As New Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer()
                        inTDSDeduct.RecID = Guid.NewGuid.ToString
                        inTDSDeduct.RefTxnID = cParam.RefMID ' DEDUCTION MASTER RECORD ID 
                        'inTDSDeduct.RefTxnSrNo = cParam.RefSrNo
                        inTDSDeduct.TDS_Sent = cParam.TDS_Ded
                        inTDSDeduct.TxnMID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID").ToString()
                        'inTDSDeduct.TxnSrNo = Val(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TR_SR_NO").ToString())
                        inTDSParam(ctr) = inTDSDeduct
                        ctr += 1
                    Next
                    Base._TDS_DBOps.Insert_TDS_Deduction(inTDSParam)
                    xfrom_TDs_ded.Dispose()
                    xRecID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "RECD_TDS_M_ID").ToString()
                    xID = xfrom_TDs_ded.TDS_Deduction_List(0).RefMID
                    Grid_Display()
                End If
            Case "UnMap-TDS-Paid"
                If Me.BandedGridView1.FocusedRowHandle >= 0 Then
                    If IsDBNull(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Paid from Center' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Base._TDS_DBOps.UnmapTDSPaid(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID").ToString())
                    xPaidID = ""
                    Grid_Display()
                End If
            Case "UnMap-TDS-Ded"
                If Me.BandedGridView1.FocusedRowHandle >= 0 Then
                    If IsDBNull(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DED_TR_M_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Dedcution' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DED_TR_M_ID") Is Nothing Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Dedcution' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DED_TR_M_ID") = "DEDUCTION NOT MAPPED% %" Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Dedcution' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Base._TDS_DBOps.UnmapTDSDeducted(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DED_TR_M_ID").ToString())
                    Grid_Display()
                End If
            Case "Map-TDS-Paid"
                If Me.BandedGridView1.FocusedRowHandle >= 0 Then
                    If IsDBNull(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please select valid 'TDS Paid to Govt.' Entry. . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim xfrom_TDs_ded As New Frm_TDS_Reg_Map_TDS_Paid
                    xfrom_TDs_ded.MainBase = Base : xfrom_TDs_ded.xMID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID").ToString()
                    xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS").ToString())
                    If xfrom_TDs_ded.lblMentionedInvoucher.EditValue = 0 Then xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "INTEREST_PAID_ON_TDS").ToString())
                    If xfrom_TDs_ded.lblMentionedInvoucher.EditValue = 0 Then xfrom_TDs_ded.lblMentionedInvoucher.EditValue = CDec(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PENALTY_PAID_ON_TDS").ToString())
                    xfrom_TDs_ded.Cen_ID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "CEN_ID").ToString()
                    xfrom_TDs_ded.Cen_Name = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Center").ToString()
                    xfrom_TDs_ded.ShowDialog()
                    If xfrom_TDs_ded.DialogResult = DialogResult.Retry Or xfrom_TDs_ded.DialogResult = DialogResult.Cancel Then
                        xfrom_TDs_ded.Dispose() : Exit Sub
                    End If

                    Dim inTDSParam(xfrom_TDs_ded.TDS_Deduction_List.Count) As Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer
                    Dim ctr As Integer = 0
                    For Each cParam As Frm_TDS_Reg_Map_TDS_Paid.Out_TDS In xfrom_TDs_ded.TDS_Deduction_List
                        Dim inTDSDeduct As New Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer()
                        inTDSDeduct.RecID = Guid.NewGuid.ToString
                        inTDSDeduct.RefTxnID = cParam.RefMID ' DEDUCTION MASTER RECORD ID 
                        'inTDSDeduct.RefTxnSrNo = cParam.RefSrNo
                        inTDSDeduct.TDS_Paid_Govt = cParam.TDS_Ded
                        'inTDSDeduct.Declared_Ded_Date = cParam.Declared_Ded_date
                        inTDSDeduct.TxnMID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID").ToString()
                        'inTDSDeduct.TxnSrNo = Val(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TR_SR_NO").ToString())
                        inTDSParam(ctr) = inTDSDeduct
                        ctr += 1
                    Next
                    Base._TDS_DBOps.Insert_TDS_Deduction(inTDSParam)
                    xfrom_TDs_ded.Dispose()
                    xPaidID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PAID_TDS_M_ID").ToString()
                    xID = xfrom_TDs_ded.TDS_Deduction_List(0).RefMID
                    Grid_Display()
                End If
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.BandedGridView1.RowCount > 0 Then
                    'Base.Show_ListPreview(GridControl1, Me.Text, Me, True, System.Drawing.Printing.PaperKind.A4, Me.Text, "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                End If
                Me.BandedGridView1.Focus()
            Case "FILTER"
                If Me.BandedGridView1.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = ConnectOne.D0010._001.My.Resources.bluesearch
                    Me.BandedGridView1.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = Global.ConnectOne.D0010._001.My.Resources.blueaccept
                    Me.BandedGridView1.OptionsView.ShowAutoFilterRow = True
                End If
            Case "FIND"
                If Me.BandedGridView1.IsFindPanelVisible Then
                    Me.But_Find.Image = My.Resources.greensearch
                    Me.BandedGridView1.HideFindPanel()
                Else
                    Me.But_Find.Image = My.Resources.greenaccept
                    Me.BandedGridView1.ShowFindPanel()
                End If
            Case "CLOSE"
                Me.Close()
        End Select


    End Sub

#End Region

End Class
