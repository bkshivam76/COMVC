Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Localization


Public Class Frm_NoteBook_Info


#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Dim xid As String = ""
    Public xEntryDate As Date = Nothing

    Private xFr_Date As DateTime = Nothing : Private xTo_Date As DateTime = Nothing
    Private Open_Cash_Bal, Open_Bank_Bal As Double
    Private Close_Cash_Bal, Close_Bank_Bal As Double
    Private Check_UnSave_Data As Boolean = False


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Dim FormClosingEnable As Boolean = True
    Private Sub Form_Closing_Window_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then Me.DataNavigation("CLOSE")
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        'xPleaseWait.Show("Note-Book Entry" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        '  Me.Icon = xPleaseWait.Icon
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()

    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

        If (keyData = (Keys.Control Or Keys.F3)) Then ' CALCULATOR
            Try
                Dim startInfo As New ProcessStartInfo : startInfo.FileName = "CALC.EXE" : Process.Start(startInfo)
            Catch ex As Exception
            End Try
            Return (True)
        End If

        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                Me.DataNavigation("SAVE")
            End If
            Return (True)
        End If
        'If (keyData = (Keys.Alt Or Keys.F2)) Then 'VIEW SELECTION
        '    Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = 20
        '    Return (True)
        'End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, BUT_LOCKED.GotFocus, BUT_LOCKED_ALL.GotFocus, BUT_UNLOCKED.GotFocus, BUT_SAVE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, BUT_LOCKED.LostFocus, BUT_LOCKED_ALL.LostFocus, BUT_UNLOCKED.LostFocus, BUT_SAVE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, BUT_UNLOCKED.Click, BUT_LOCKED_ALL.Click, BUT_LOCKED.Click, BUT_SAVE.Click, But_Filter.Click, But_Find.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_SAVE" Then Me.DataNavigation("SAVE")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_LOCKED" Then Me.DataNavigation("LOCKED")
            If UCase(btn.Name) = "BUT_UNLOCKED" Then Me.DataNavigation("UNLOCKED")
            If UCase(btn.Name) = "BUT_LOCKED_ALL" Then Me.DataNavigation("LOCKED_ALL")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown, BUT_LOCKED.KeyDown, BUT_LOCKED_ALL.KeyDown, BUT_UNLOCKED.KeyDown, BUT_SAVE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Grid Events"

    Private Sub BandedGridView1_ShowGridMenu(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles BandedGridView1.PopupMenuShowing
        If e.MenuType = GridMenuType.Column Then

            Dim RemoveColumnMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnRemoveColumn)
            If RemoveColumnMenu IsNot Nothing Then RemoveColumnMenu.Visible = False

            Dim SortAscendingMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnSortAscending)
            If SortAscendingMenu IsNot Nothing Then SortAscendingMenu.Visible = False

            Dim SortDescendingMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnSortDescending)
            If SortDescendingMenu IsNot Nothing Then SortDescendingMenu.Visible = False

            Dim ClearSortingMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnClearSorting)
            If ClearSortingMenu IsNot Nothing Then ClearSortingMenu.Visible = False

            Dim GroupMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            If GroupMenu IsNot Nothing Then GroupMenu.Visible = False

            Dim GroupBoxMenu As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuGroupPanelShow)
            If GroupBoxMenu IsNot Nothing Then GroupBoxMenu.Visible = False

            'Dim miGroup As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            'If miGroup IsNot Nothing Then miGroup.Enabled = False
        End If
    End Sub
    Private Function GetItemByStringId(ByVal menu As DXPopupMenu, ByVal id As GridStringId) As DXMenuItem
        For Each item As DXMenuItem In menu.Items
            If item.Caption = GridLocalizer.Active.GetLocalizedString(id) Then
                Return item
            End If
        Next item
        Return Nothing
    End Function
    Private Sub BandedGridView1_ValidatingEditor(ByVal sender As Object, ByVal e As BaseContainerValidateEditorEventArgs) Handles BandedGridView1.ValidatingEditor
        Try
            Dim View As GridView = sender
            If Not View.FocusedColumn.Name.StartsWith("AMT_DT_") Or IsDBNull(e.Value) Then Exit Sub
            'If View.FocusedColumn.FieldName = "AMT_DT_01" Then
            Dim _Value As Double = Convert.ToDouble(e.Value)
            If _Value < 0 Then
                e.Valid = False : e.ErrorText = "A m o u n t   c a n n o t   b e   N e g a t i v e . . . !"
            Else
                e.Value = Int(Convert.ToDouble(e.Value))
            End If
            'End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub BandedGridView1_InvalidValueException(ByVal sender As Object, ByVal e As InvalidValueExceptionEventArgs) Handles BandedGridView1.InvalidValueException
        Dim View As GridView = sender
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
        DevExpress.XtraEditors.XtraMessageBox.Show(e.ErrorText, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Try
            View.CloseEditor()
            View.CancelUpdateCurrentRow()
            View.ShowEditor()
        Catch ex As Exception
        End Try
        'BandedGridView1.FocusedColumn = View.FocusedColumn
        'BandedGridView1.FocusedRowHandle = View.FocusedRowHandle
        'TryCast(View.ActiveEditor, TextEdit).SelectAll()
    End Sub
    Private Sub BandedGridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles BandedGridView1.ShownEditor
        Dim view As GridView = TryCast(sender, GridView)
        TryCast(view.ActiveEditor, TextEdit).SelectAll()
    End Sub
    Private Sub BandedGridView1_HiddenEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles BandedGridView1.HiddenEditor
        UpdateTotal()
    End Sub
    Private Sub BandedGridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged
        UpdateCellData(sender)
        ShowSelectedItem()
    End Sub
    Private Sub BandedGridView1_FocusedColumnChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles BandedGridView1.FocusedColumnChanged
        UpdateCellData(sender)
        ShowEntryDateMonth()
        Try
            ShowSelectedItem()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ShowSelectedItem()
        Me.lbl_SelectedItem.Text = BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, ITEM_NAME).ToString() & "<color=red> Total Amt: " & Format(Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_TOTAL).ToString()), "#,0.00") & "</color>"
    End Sub
    Private Sub ShowEntryDateMonth()
        If BandedGridView1.FocusedColumn.Name.StartsWith("AMT_DT_") Then
            Me.lbl_EntryDateMonth.Text = "Entry Date: <color=red>" & Base.GetDayNumberWithSuffix(BandedGridView1.FocusedColumn.Caption) & " " & StrConv(Cmb_View.Text.Replace("-", ","), VbStrConv.ProperCase) & "</color>"
        Else
            Me.lbl_EntryDateMonth.Text = "Entry Month: <color=red>" & StrConv(Cmb_View.Text, VbStrConv.ProperCase) & "</color>"
        End If
    End Sub
    Private Sub UpdateCellData(ByVal sender As Object)
        Dim View As GridView = sender
        If Not View.FocusedColumn.Name.StartsWith("AMT_DT_") Then Exit Sub
        If View.IsEditing = False Then View.ShowEditor()
        Me.BandedGridView1.RefreshData() : Me.BandedGridView1.UpdateCurrentRow()
        UpdateTotal()
    End Sub
    Private Sub UpdateTotal()
        Try
            Dim Total_Value As Double = Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_01).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_02).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_03).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_04).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_05).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_06).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_07).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_08).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_09).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_10).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_11).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_12).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_13).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_14).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_15).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_16).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_17).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_18).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_19).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_20).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_21).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_22).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_23).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_24).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_25).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_26).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_27).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_28).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_29).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_30).ToString()) + _
                                        Val(BandedGridView1.GetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_DT_31).ToString())
            BandedGridView1.SetRowCellValue(BandedGridView1.FocusedRowHandle, AMT_TOTAL, Total_Value)
        Catch ex As Exception

        End Try

        
    End Sub
    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.BandedGridView1.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular)
        Me.GridBand1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.GridBand2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.GridBand3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)

        'Me.RepositoryItemTextEdit1.AppearanceFocused.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.BandedGridView1.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.BandedGridView1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", Zoom1.Value - 1, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Try
            Me.BandedGridView1.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryItemTextEdit1_Spin(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepositoryItemTextEdit1.Spin
        e.Handled = True
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CLOSE : Check_UnSave_Data = False
        Me.DialogResult = DialogResult.None
        Me.TitleX.Text = "Note-Book Entry"
        Fill_Change_Period_Items()
        'Check Last Entry Date.........................................
        Dim xLastDate As Date = Now.Date

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Cmb_View.Properties.ReadOnly = True
            xLastDate = xEntryDate
        Else
            Dim MaxValue As Object = 0
            MaxValue = Base._NoteBook_DBOps.GetMaxTransactionDate()
            If MaxValue Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If IsDBNull(MaxValue) Then xLastDate = Base._open_Year_Sdt Else xLastDate = MaxValue
        End If

        'Default View Setting..........................................
        Dim xMM As Integer = xLastDate.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "View ~ " & Me.TitleX.Text
            Me.BUT_SAVE.Enabled = False
            Set_Disable()
        End If
        Check_UnSave_Data = True
        'xPleaseWait.Hide()
    End Sub

    Private Sub Get_Cash_Bank_Balance()
        '
        'Get Cash Balance..............
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
        If Cash_Bal Is Nothing Then Base.HandleDBError_OnNothingReturned() : Exit Sub
        If Cash_Bal.Rows.Count > 0 Then
            If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Open_Cash_Bal = Cash_Bal.Rows(0)("OPENING") Else Open_Cash_Bal = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("CLOSING")) Then Close_Cash_Bal = Cash_Bal.Rows(0)("CLOSING") Else Close_Cash_Bal = 0
        Else : Open_Cash_Bal = 0 : Close_Cash_Bal = 0 : End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Bank Balance..............
        Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        Dim Bank_Bal As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(xFr_Date, xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Bal Is Nothing Then Base.HandleDBError_OnNothingReturned() : Exit Sub
        '
        Dim _bankCnt As Integer = 1 : Dim _colWidth As Integer = 0 : Dim _colSetWidth As Integer = 90
        Dim _online_BANK_COL_TR_REC As String = "" : Dim _online_BANK_COL_NB_REC As String = "" : Dim _online_BANK_COL_TR_PAY As String = "" : Dim _online_BANK_COL_NB_PAY As String = ""
        Dim _local__BANK_COL_TR_REC As String = "" : Dim _local__BANK_COL_NB_REC As String = "" : Dim _local__BANK_COL_TR_PAY As String = "" : Dim _local__BANK_COL_NB_PAY As String = ""
        '
        '
        If Bank_Bal.Rows.Count > 0 Then
            For Each XROW In Bank_Bal.Rows
                If Not IsDBNull(XROW("OPENING")) Then Open_Bank_Bal += XROW("OPENING") Else Open_Bank_Bal += 0
                If Not IsDBNull(XROW("CLOSING")) Then Close_Bank_Bal += XROW("CLOSING") Else Close_Bank_Bal += 0
                _bankCnt += 1
            Next
        Else
            Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        End If
        BE_Cash_Bank.Text = "Cash: " & Format(Close_Cash_Bal, "#,0.00") & "    Bank: " & Format(Close_Bank_Bal, "#,0.00")

    End Sub

    Private Sub BE_Cash_Bank_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BE_Cash_Bank.ButtonClick
        Dim xfrm As New Frm_View_Summary : xfrm.MainBase = Base
        xfrm.Text = "Summary..." : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        xfrm.Dispose()
    End Sub
    Dim SaveData As DataTable = Nothing : Dim TR_Table As DataTable = Nothing
    Public Sub Grid_Display()
        'xPleaseWait.Show("Note-Book Entry" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        '-----------------------
        Get_Cash_Bank_Balance()
        '-----------------------

        TR_Table = Base._NoteBook_DBOps.GetList(xFr_Date.Month)
        If TR_Table Is Nothing Then Base.HandleDBError_OnNothingReturned() : Exit Sub

        SaveData = TR_Table.Copy


        Me.GridControl1.DataSource = TR_Table
        For I As Integer = 0 To TR_Table.Rows.Count - 1
            If Not IsDBNull(TR_Table(I)("AMT_DT_01")) Then TR_Table(I)("AMT_DT_01") = Int(Val(TR_Table(I)("AMT_DT_01")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_02")) Then TR_Table(I)("AMT_DT_02") = Int(Val(TR_Table(I)("AMT_DT_02")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_03")) Then TR_Table(I)("AMT_DT_03") = Int(Val(TR_Table(I)("AMT_DT_03")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_04")) Then TR_Table(I)("AMT_DT_04") = Int(Val(TR_Table(I)("AMT_DT_04")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_05")) Then TR_Table(I)("AMT_DT_05") = Int(Val(TR_Table(I)("AMT_DT_05")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_06")) Then TR_Table(I)("AMT_DT_06") = Int(Val(TR_Table(I)("AMT_DT_06")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_07")) Then TR_Table(I)("AMT_DT_07") = Int(Val(TR_Table(I)("AMT_DT_07")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_08")) Then TR_Table(I)("AMT_DT_08") = Int(Val(TR_Table(I)("AMT_DT_08")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_09")) Then TR_Table(I)("AMT_DT_09") = Int(Val(TR_Table(I)("AMT_DT_09")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_10")) Then TR_Table(I)("AMT_DT_10") = Int(Val(TR_Table(I)("AMT_DT_10")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_11")) Then TR_Table(I)("AMT_DT_11") = Int(Val(TR_Table(I)("AMT_DT_11")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_12")) Then TR_Table(I)("AMT_DT_12") = Int(Val(TR_Table(I)("AMT_DT_12")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_13")) Then TR_Table(I)("AMT_DT_13") = Int(Val(TR_Table(I)("AMT_DT_13")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_14")) Then TR_Table(I)("AMT_DT_14") = Int(Val(TR_Table(I)("AMT_DT_14")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_15")) Then TR_Table(I)("AMT_DT_15") = Int(Val(TR_Table(I)("AMT_DT_15")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_16")) Then TR_Table(I)("AMT_DT_16") = Int(Val(TR_Table(I)("AMT_DT_16")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_17")) Then TR_Table(I)("AMT_DT_17") = Int(Val(TR_Table(I)("AMT_DT_17")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_18")) Then TR_Table(I)("AMT_DT_18") = Int(Val(TR_Table(I)("AMT_DT_18")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_19")) Then TR_Table(I)("AMT_DT_19") = Int(Val(TR_Table(I)("AMT_DT_19")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_20")) Then TR_Table(I)("AMT_DT_20") = Int(Val(TR_Table(I)("AMT_DT_20")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_21")) Then TR_Table(I)("AMT_DT_21") = Int(Val(TR_Table(I)("AMT_DT_21")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_22")) Then TR_Table(I)("AMT_DT_22") = Int(Val(TR_Table(I)("AMT_DT_22")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_23")) Then TR_Table(I)("AMT_DT_23") = Int(Val(TR_Table(I)("AMT_DT_23")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_24")) Then TR_Table(I)("AMT_DT_24") = Int(Val(TR_Table(I)("AMT_DT_24")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_25")) Then TR_Table(I)("AMT_DT_25") = Int(Val(TR_Table(I)("AMT_DT_25")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_26")) Then TR_Table(I)("AMT_DT_26") = Int(Val(TR_Table(I)("AMT_DT_26")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_27")) Then TR_Table(I)("AMT_DT_27") = Int(Val(TR_Table(I)("AMT_DT_27")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_28")) Then TR_Table(I)("AMT_DT_28") = Int(Val(TR_Table(I)("AMT_DT_28")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_29")) Then TR_Table(I)("AMT_DT_29") = Int(Val(TR_Table(I)("AMT_DT_29")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_30")) Then TR_Table(I)("AMT_DT_30") = Int(Val(TR_Table(I)("AMT_DT_30")))
            If Not IsDBNull(TR_Table(I)("AMT_DT_31")) Then TR_Table(I)("AMT_DT_31") = Int(Val(TR_Table(I)("AMT_DT_31")))
            If Not IsDBNull(TR_Table(I)("AMT_TOTAL")) Then TR_Table(I)("AMT_TOTAL") = Int(Val(TR_Table(I)("AMT_TOTAL")))

        Next


        Me.BandedGridView1.Columns("ITEM_NAME").Summary.Clear()
        Me.BandedGridView1.Columns("ITEM_NAME").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Total :{0:0}")

        If Not Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Notebook, Common_Lib.Common.ClientAction.Lock_Unlock) Then
            Me.BUT_UNLOCKED.Visible = False : Me.BUT_LOCKED.Visible = False : Me.BUT_LOCKED_ALL.Visible = False
        End If

        'xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "SAVE"
                _Entries.Clear()
                If BandedGridView1.RowCount > 0 Then
                    For i As Integer = 0 To BandedGridView1.RowCount - 1
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_01).ToString())) > 0 Then PrepareNoteBookEntry(i, 1, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_01).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_02).ToString())) > 0 Then PrepareNoteBookEntry(i, 2, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_02).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_03).ToString())) > 0 Then PrepareNoteBookEntry(i, 3, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_03).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_04).ToString())) > 0 Then PrepareNoteBookEntry(i, 4, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_04).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_05).ToString())) > 0 Then PrepareNoteBookEntry(i, 5, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_05).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_06).ToString())) > 0 Then PrepareNoteBookEntry(i, 6, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_06).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_07).ToString())) > 0 Then PrepareNoteBookEntry(i, 7, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_07).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_08).ToString())) > 0 Then PrepareNoteBookEntry(i, 8, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_08).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_09).ToString())) > 0 Then PrepareNoteBookEntry(i, 9, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_09).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_10).ToString())) > 0 Then PrepareNoteBookEntry(i, 10, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_10).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_11).ToString())) > 0 Then PrepareNoteBookEntry(i, 11, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_11).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_12).ToString())) > 0 Then PrepareNoteBookEntry(i, 12, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_12).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_13).ToString())) > 0 Then PrepareNoteBookEntry(i, 13, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_13).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_14).ToString())) > 0 Then PrepareNoteBookEntry(i, 14, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_14).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_15).ToString())) > 0 Then PrepareNoteBookEntry(i, 15, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_15).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_16).ToString())) > 0 Then PrepareNoteBookEntry(i, 16, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_16).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_17).ToString())) > 0 Then PrepareNoteBookEntry(i, 17, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_17).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_18).ToString())) > 0 Then PrepareNoteBookEntry(i, 18, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_18).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_19).ToString())) > 0 Then PrepareNoteBookEntry(i, 19, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_19).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_20).ToString())) > 0 Then PrepareNoteBookEntry(i, 20, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_20).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_21).ToString())) > 0 Then PrepareNoteBookEntry(i, 21, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_21).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_22).ToString())) > 0 Then PrepareNoteBookEntry(i, 22, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_22).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_23).ToString())) > 0 Then PrepareNoteBookEntry(i, 23, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_23).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_24).ToString())) > 0 Then PrepareNoteBookEntry(i, 24, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_24).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_25).ToString())) > 0 Then PrepareNoteBookEntry(i, 25, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_25).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_26).ToString())) > 0 Then PrepareNoteBookEntry(i, 26, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_26).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_27).ToString())) > 0 Then PrepareNoteBookEntry(i, 27, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_27).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_28).ToString())) > 0 Then PrepareNoteBookEntry(i, 28, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_28).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_29).ToString())) > 0 Then PrepareNoteBookEntry(i, 29, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_29).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_30).ToString())) > 0 Then PrepareNoteBookEntry(i, 30, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_30).ToString()))
                        If Int(Val(BandedGridView1.GetRowCellValue(i, AMT_DT_31).ToString())) > 0 Then PrepareNoteBookEntry(i, 31, Val(BandedGridView1.GetRowCellValue(i, AMT_DT_31).ToString()))
                    Next


                    Dim xFlag As Boolean = True
                    If Base._NoteBook_DBOps.Delete(xFr_Date.Month, xFr_Date.Year) Then
                        xFlag = True
                    Else
                        xFlag = False
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    If _Entries.Count > 0 Then
                        If Base._NoteBook_DBOps.Insert(_Entries) And xFlag = True Then
                        Else
                            xFlag = False
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    If xFlag Then
                        Me.DialogResult = DialogResult.None
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                'Case "LOCKED_ALL"
                '    Dim xPromptWindow As New Common_Lib.Prompt_Window
                '    If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "Sure you want to <color=red>Lock all</color> entries in Selected period...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                '        xPromptWindow.Dispose()
                '        Exit Sub
                '    Else
                '        xPromptWindow.Dispose()
                '    End If
                '    If PivotGridControl1.Cells.RowCount > 0 And Base.CheckActionRights(ClientScreen.Accounts_Notebook, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                '        xPleaseWait.Show("Locking Shown Notebook Entries...")
                '        If Base._NoteBook_DBOps.MarkAsLocked(xFr_Date, xTo_Date) Then
                '            xPleaseWait.Hide()
                '            DevExpress.XtraEditors.XtraMessageBox.Show("A l l   R e c o r d s   L o c k e d   S u c c e s s f u l l y!!", "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '            Grid_Display()
                '            Exit Sub
                '        Else
                '            xPleaseWait.Hide()
                '            DevExpress.XtraEditors.XtraMessageBox.Show("R e c o r d s   n o t  l o c k e d  s u c c e s s f u l l y!!", "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '            Grid_Display()
                '            Exit Sub
                '        End If

                '    End If
                'Case "LOCKED"
                '    If PivotGridControl1.Cells.RowCount > 0 And Base.CheckActionRights(ClientScreen.Accounts_Notebook, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                '        If Not PivotGridControl1.Cells.GetFocusedCellInfo.IsFieldValueRetrievable(PV_REC_ID) Then Exit Sub
                '        Dim xStatus As Object = Base._NoteBook_DBOps.GetStatus(PivotGridControl1.Cells.GetFocusedCellInfo.GetFieldValue(PV_REC_ID).ToString())
                '        If xStatus = Common_Lib.Common.Record_Status._Locked Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show("A l r e a d y  L o c k e d  E n t r i e s  c a n 't  b e  R e -L o c k e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                '        End If
                '        If xStatus = Common_Lib.Common.Record_Status._Incomplete Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show("I n c o m p l e t e   E n t r i e s   c a n ' t   b e   L o c k e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                '        End If
                '        Dim xTemp_ID As String = PivotGridControl1.Cells.GetFocusedCellInfo.GetFieldValue(PV_REC_ID).ToString() : xid = xTemp_ID
                '        If Not Base._NoteBook_DBOps.MarkAsLocked(xTemp_ID) Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If

                '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.LockedSuccess, "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Grid_Display()
                '        Exit Sub
                '    End If
                'Case "UNLOCKED"
                '    If Me.PivotGridControl1.Cells.RowCount > 0 And Base.CheckActionRights(ClientScreen.Accounts_Notebook, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                '        If Not PivotGridControl1.Cells.GetFocusedCellInfo.IsFieldValueRetrievable(PV_REC_ID) Then Exit Sub
                '        Dim xStatus As Object = Base._NoteBook_DBOps.GetStatus(PivotGridControl1.Cells.GetFocusedCellInfo.GetFieldValue(PV_REC_ID).ToString())
                '        If xStatus = Common_Lib.Common.Record_Status._Completed Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show("A l r e a d y   U n l o c k e d   E n t r i e s   c a n 't   b e   R e -U n l o c k e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                '        End If
                '        If xStatus = Common_Lib.Common.Record_Status._Incomplete Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show("I n c o m p l e t e   E n t r i e s   c a n ' t   b e   U n l o c k e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                '        End If
                '        Dim xTemp_ID As String = PivotGridControl1.Cells.GetFocusedCellInfo.GetFieldValue(PV_REC_ID).ToString() : xid = xTemp_ID
                '        If Not Base._NoteBook_DBOps.MarkAsComplete(xTemp_ID) Then
                '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UnlockedSuccess(), "UnLocked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Grid_Display()
                '        Exit Sub
                '    End If

            Case "FILTER"
                If Me.BandedGridView1.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = My.Resources.bluesearch
                    Me.BandedGridView1.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = My.Resources.blueaccept
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
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.BandedGridView1.RowCount > 0 Then
                    Dim Incharge As String = ""
                    Dim Centre_Inc As DataTable = Base._Report_DBOps.GetCenterDetails()
                    If Centre_Inc Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Me.Dispose()
                        Exit Sub
                    End If
                    If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Incharge = Centre_Inc.Rows(0)("CEN_INCHARGE")
                    'Base.Show_ListPreview(GridControl1, "Note Book  (UID: " & Base._open_UID_No & ")", Me, True, Printing.PaperKind.A4, Me.Text, "UID: " & Base._open_UID_No, BE_View_Period.Text, True, 50, 75, 14, "Date:[Date Printed]" & vbNewLine & vbNewLine & "Place:" + Base._open_Cen_Name, "ver " + Base._Current_Version, "_________________________" & vbNewLine & "Signature of Centre In - charge" & vbNewLine & Incharge)
                End If
                Me.BandedGridView1.Focus()
            Case "CLOSE"
                Data_Change()
                If Flag_Data_Change Then
                    ' Dim xPromptWindow As New Common_Lib.Prompt_Window
                    'If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Do you want to Save Notebook Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    '    Me.DataNavigation("SAVE")
                    '    Me.DialogResult = DialogResult.OK
                    '    FormClosingEnable = False : Me.Close()
                    'Else
                    '    Me.DialogResult = DialogResult.Cancel
                    '    FormClosingEnable = False : Me.Close()
                    'End If
                    'xPromptWindow.Dispose()
                Else
                    Me.DialogResult = DialogResult.Cancel
                    FormClosingEnable = False : Me.Close()
                End If
        End Select

    End Sub

    Dim _Entries As New ArrayList
    Private Sub PrepareNoteBookEntry(ByVal _Row As Integer, ByVal _Day As Integer, ByVal _Amount As Double)
        Dim VDate As Date = New Date(xFr_Date.Year, xFr_Date.Month, _Day) : Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        If BandedGridView1.GetRowCellValue(_Row, ITEM_TRANS_TYPE).ToString.ToUpper = "DEBIT" Then
            Dr_Led_id = BandedGridView1.GetRowCellValue(_Row, ITEM_LED_ID).ToString : Cr_Led_id = "00080" 'Cash A/c.
        Else
            Cr_Led_id = BandedGridView1.GetRowCellValue(_Row, ITEM_LED_ID).ToString : Dr_Led_id = "00080" 'Cash A/c.
        End If
        Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_NoteBook = New Common_Lib.RealTimeService.Parameter_Insert_NoteBook()
        InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Payment
        InParam.VNo = ""
        InParam.TDate = VDate.ToString(Base._Server_Date_Format_Short)
        InParam.ItemID = BandedGridView1.GetRowCellValue(_Row, REC_ID).ToString
        InParam.Type = BandedGridView1.GetRowCellValue(_Row, ITEM_TRANS_TYPE).ToString
        InParam.Cr_Led_ID = Cr_Led_id
        InParam.Dr_Led_ID = Dr_Led_id
        InParam.Amount = Int(_Amount)
        InParam.Narration = ""
        InParam.Status_Action = Common_Lib.Common.Record_Status._Completed
        InParam.RecID = System.Guid.NewGuid().ToString()
        _Entries.Add(InParam)
    End Sub

    Private Sub Set_Disable()
        AMT_DT_01.OptionsColumn.AllowEdit = False
        AMT_DT_02.OptionsColumn.AllowEdit = False
        AMT_DT_03.OptionsColumn.AllowEdit = False
        AMT_DT_04.OptionsColumn.AllowEdit = False
        AMT_DT_05.OptionsColumn.AllowEdit = False
        AMT_DT_06.OptionsColumn.AllowEdit = False
        AMT_DT_07.OptionsColumn.AllowEdit = False
        AMT_DT_08.OptionsColumn.AllowEdit = False
        AMT_DT_09.OptionsColumn.AllowEdit = False
        AMT_DT_10.OptionsColumn.AllowEdit = False
        AMT_DT_11.OptionsColumn.AllowEdit = False
        AMT_DT_12.OptionsColumn.AllowEdit = False
        AMT_DT_13.OptionsColumn.AllowEdit = False
        AMT_DT_14.OptionsColumn.AllowEdit = False
        AMT_DT_15.OptionsColumn.AllowEdit = False
        AMT_DT_16.OptionsColumn.AllowEdit = False
        AMT_DT_17.OptionsColumn.AllowEdit = False
        AMT_DT_18.OptionsColumn.AllowEdit = False
        AMT_DT_19.OptionsColumn.AllowEdit = False
        AMT_DT_20.OptionsColumn.AllowEdit = False
        AMT_DT_21.OptionsColumn.AllowEdit = False
        AMT_DT_22.OptionsColumn.AllowEdit = False
        AMT_DT_23.OptionsColumn.AllowEdit = False
        AMT_DT_24.OptionsColumn.AllowEdit = False
        AMT_DT_25.OptionsColumn.AllowEdit = False
        AMT_DT_26.OptionsColumn.AllowEdit = False
        AMT_DT_27.OptionsColumn.AllowEdit = False
        AMT_DT_28.OptionsColumn.AllowEdit = False
        AMT_DT_29.OptionsColumn.AllowEdit = False
        AMT_DT_30.OptionsColumn.AllowEdit = False
        AMT_DT_31.OptionsColumn.AllowEdit = False
    End Sub

    Dim Flag_Data_Change As Boolean = False
    Private Sub Data_Change()
        For I As Integer = 0 To TR_Table.Rows.Count - 1
            Dim TR_Amt As Integer = 0 : Dim Save_Amt As Integer = 0

            If Not IsDBNull(TR_Table(I)("AMT_DT_01")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_01"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_01")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_01"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_02")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_02"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_02")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_02"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_03")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_03"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_03")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_03"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_04")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_04"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_04")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_04"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_05")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_05"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_05")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_05"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_06")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_06"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_06")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_06"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_07")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_07"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_07")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_07"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_08")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_08"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_08")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_08"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_09")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_09"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_09")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_09"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_10")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_10"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_10")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_10"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_11")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_11"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_11")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_11"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_12")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_12"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_12")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_12"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_13")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_13"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_13")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_13"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_14")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_14"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_14")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_14"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_15")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_15"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_15")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_15"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_16")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_16"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_16")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_16"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_17")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_17"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_17")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_17"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_18")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_18"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_18")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_18"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_19")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_19"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_19")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_19"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_20")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_20"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_20")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_20"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_21")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_21"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_21")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_21"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_22")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_22"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_22")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_22"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_23")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_23"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_23")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_23"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_24")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_24"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_24")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_24"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_25")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_25"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_25")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_25"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_26")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_26"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_26")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_26"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_27")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_27"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_27")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_27"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_28")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_28"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_28")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_28"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_29")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_29"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_29")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_29"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_30")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_30"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_30")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_30"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False

            If Not IsDBNull(TR_Table(I)("AMT_DT_31")) Then TR_Amt = Int(Val(TR_Table(I)("AMT_DT_31"))) Else TR_Amt = 0
            If Not IsDBNull(SaveData(I)("AMT_DT_31")) Then Save_Amt = Int(Val(SaveData(I)("AMT_DT_31"))) Else Save_Amt = 0
            If TR_Amt <> Save_Amt Then Flag_Data_Change = True : Exit Sub Else Flag_Data_Change = False
        Next
    End Sub
#End Region

#Region "Start--> Change Period"

    Private Sub Fill_Change_Period_Items()
        Me.Cmb_View.Properties.Items.Clear()
        For I As Integer = Base._open_Year_Sdt.Month To 12 : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Sdt.Year) : Next
        For I As Integer = 1 To Base._open_Year_Edt.Month : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Edt.Year) : Next
        'Me.Cmb_View.Properties.Items.Add("1st Quarter") ' : APR to JUN
        'Me.Cmb_View.Properties.Items.Add("2nd Quarter") ' : JUL to SEP
        'Me.Cmb_View.Properties.Items.Add("3rd Quarter") ' : OCT to DEC
        'Me.Cmb_View.Properties.Items.Add("4th Quarter") ' : JAN to MAR
        'Me.Cmb_View.Properties.Items.Add("1st Half Yearly") ' : APR to SEP
        'Me.Cmb_View.Properties.Items.Add("2nd Half Yearly") ' : OCT to MAR
        'Me.Cmb_View.Properties.Items.Add("Nine Months") ' : APR to DEC
        'Me.Cmb_View.Properties.Items.Add("Financial Year")
        'Me.Cmb_View.Properties.Items.Add("Specific Period")
    End Sub
    Private Sub Cmb_View_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Cmb_View.SelectedIndexChanged
        Me.Cmb_View.Properties.Buttons(1).Enabled = False

        If Check_UnSave_Data Then
            Data_Change()
            If Flag_Data_Change Then
                'Dim xPromptWindow As New Common_Lib.Prompt_Window
                'If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Do you want to Save Notebook Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                '    Me.DialogResult = DialogResult.None
                '    Me.DataNavigation("SAVE")
                'Else
                '    Me.DialogResult = DialogResult.None
                'End If
                'xPromptWindow.Dispose()
            End If
        End If

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

        Dim TotalDays As Integer = System.DateTime.DaysInMonth(xFr_Date.Year, xFr_Date.Month)
        AMT_DT_29.Visible = True : AMT_DT_30.Visible = True : AMT_DT_31.Visible = True
        If TotalDays = 30 Then : AMT_DT_31.Visible = False
        ElseIf TotalDays = 29 Then : AMT_DT_30.Visible = False : AMT_DT_31.Visible = False
        ElseIf TotalDays = 28 Then : AMT_DT_29.Visible = False : AMT_DT_30.Visible = False : AMT_DT_31.Visible = False
        End If

        'OUTPUT
        If Cmb_View.SelectedIndex >= 0 Then
            Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
            GridBand2.Caption = Format(xFr_Date, "MMMM, yyyy")
            Grid_Display()
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
        Grid_Display()
    End Sub

#End Region


End Class
