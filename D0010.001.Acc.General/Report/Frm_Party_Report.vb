Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering

Public Class Frm_Party_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Public PartyID As String = ""
    Dim xid As String = ""
    Public Opening As Double = 0
    Public xFr_Date As Date = Nothing : Public xTo_Date As Date = Nothing
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
        xPleaseWait.Show("Party Ledger" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        ''/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        ''\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub


#End Region

#Region "Start--> Button Events"
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Shift Or Keys.F6)) Then ' CALCULATOR
            Try
                Dim xfrm As New D0006.Frm_Calculator
                xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                xfrm.Show()
                '                MsgBox(Val(xfrm.txtInput.Text))
            Catch ex As Exception
            End Try
            Return (True)
        End If

        If (keyData = (Keys.Alt Or Keys.F2)) Then 'CHANGE PERIOD
            Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = 20
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
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
            If UCase(btn.Name) = "BUT_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub BUT_SHOW_Click(sender As Object, e As EventArgs) Handles BUT_SHOW.Click
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
        Me.DataNavigation("DETAIL")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("DETAIL")
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
    Private Sub GridView1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.GotFocus
        CreationDetail(Me.GridView1.FocusedRowHandle)
    End Sub
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
      
    End Sub
    Private Sub CreationDetail(ByVal Xrow As Integer)
      
    End Sub
    Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
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
        ' Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        GLookUp_PartyList.Tag = "" : LookUp_GetPartyList()
        PartyBind()
        Fill_Change_Period_Items()
        If xFr_Date = Nothing Then
            Dim xLastDate As Date = Now.Date
            Dim xMM As Integer = xLastDate.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
        Else
            Me.Cmb_View.SelectedIndex = 20
        End If
        
        If PartyID.Length > 0 Then Grid_Display()
        xPleaseWait.Hide()
    End Sub
    Public Sub Grid_Display()
        Dim FrDate As Date = xFr_Date
        Dim ToDate As Date = xTo_Date
        xPleaseWait.Show("Party Ledger" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        ' Me.Pic_Status.Visible = False : Lbl_Status.Text = "" : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_Separator1.Visible = False
        RowFlag1 = False

        Dim param As Common_Lib.RealTimeService.Param_GetPartyReport = New Common_Lib.RealTimeService.Param_GetPartyReport
        param.FromDate = FrDate
        param.ToDate = ToDate
        param.PartyId = GLookUp_PartyList.Tag
        param.YearID = Base._open_Year_ID
        Dim _Party_Table As DataTable = Base._Reports_Ledgers_DBOps.GetPartyReport(param)
        If _Party_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        _Party_Table.Columns.Add(New DataColumn("Balance", Txt_TitleX.Text.GetType()))
        Dim Total As Double = Opening
        Dim TotCredit As Double = 0
        Dim TotDebit As Double = 0
        For Each cRow As DataRow In _Party_Table.Rows
            If Not IsDBNull(cRow("Debit")) Then
                If cRow("Debit").ToString().Length > 0 Then Total = Math.Round(Total + Convert.ToDouble(cRow("Debit")), 2) : TotDebit += Convert.ToDouble(cRow("Debit"))
            End If
            If Not IsDBNull(cRow("Credit")) Then
                If cRow("Credit").ToString().Length > 0 Then Total = Math.Round(Total - Convert.ToDouble(cRow("Credit")), 2) : TotCredit += Convert.ToDouble(cRow("Credit"))
            End If
            If Total > 0 Then
                cRow("Balance") = Total.ToString() + " Dr"
            ElseIf Total < 0 Then
                cRow("Balance") = (Total * -1).ToString() + " Cr"
            End If
        Next

        Dim OpeningRow As DataRow = _Party_Table.NewRow
        OpeningRow("Date") = FrDate.ToString("MMM-dd-yyyy")
        OpeningRow("Particulars") = "Opening Balance"
        If Opening > 0 Then OpeningRow("Balance") = Opening.ToString + " Dr."
        If Opening < 0 Then OpeningRow("Balance") = (-1 * Opening).ToString + " Cr."
        If Opening = 0 Then OpeningRow("Balance") = Opening.ToString
        _Party_Table.Rows.InsertAt(OpeningRow, 0)

        If _Party_Table.Rows.Count > 0 Then
            lblNetBalance.Text = "Rs. " + Val(_Party_Table.Rows(_Party_Table.Rows.Count - 1)("Balance").ToString).ToString + " " + IIf(_Party_Table.Rows(_Party_Table.Rows.Count - 1)("Balance").ToString.Contains("Cr"), "Cr", "Dr")
        Else
            lblNetBalance.Text = "Rs. 0"
        End If

        Me.GridControl1.DataSource = _Party_Table
        xPleaseWait.Hide()
    End Sub
    Private Sub PartyBind()
        Me.GLookUp_PartyList.Properties.ReadOnly = False
        If Me.PartyID Is Nothing Then Exit Sub
        If Me.PartyID.Length > 0 Then
            Me.GLookUp_PartyList.ShowPopup() : Me.GLookUp_PartyList.ClosePopup()
            Me.GLookUp_PartyListView.FocusedRowHandle = Me.GLookUp_PartyListView.LocateByValue("ID", PartyID)
            Me.GLookUp_PartyList.EditValue = PartyID
            Me.GLookUp_PartyList.Properties.Tag = "SHOW"
            Me.GLookUp_PartyList.Tag = PartyID
        Else
            Me.GLookUp_PartyListView.FocusedRowHandle = -1
            Me.GLookUp_PartyList.Text = ""
        End If
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then Me.GLookUp_PartyList.Properties.ReadOnly = True
    End Sub
    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "REFRESH"
                Grid_Display()
            Case "DETAIL"
                If IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TR_M_ID")) Then Exit Sub
                Dim xFrm As Frm_Txn_Detail = New Frm_Txn_Detail
                xFrm.TxnMID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TR_M_ID")
                xFrm.MainBase = Base
                xFrm.ShowDialog()
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

    Private Sub GLookUp_PartyList1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList.CancelPopup()
            ' Hide_Properties()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            '  Hide_Properties()
        End If
    End Sub
    Private Sub GLookUp_PartyList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList.EditValueChanged
        If Me.GLookUp_PartyList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_PartyListView.RowCount > 0 And Val(Me.GLookUp_PartyListView.FocusedRowHandle) > 0 Then
                Me.GLookUp_PartyList.Tag = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "ID").ToString
            End If
            If Me.GLookUp_PartyListView.RowCount > 0 And Val(Me.GLookUp_PartyListView.FocusedRowHandle) = 0 Then
                Me.GLookUp_PartyList.Tag = Nothing
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_PartyList.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_PartyList_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_PartyList_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("Name", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._Journal_voucher_DBOps.GetParties("Name", "ID")
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("Name") = "" : d1.Rows.Add(ROW)
        Dim dview As New DataView(d1) : dview.Sort = "Name"
        If dview.Count > 0 Then
            Me.GLookUp_PartyList.Properties.ValueMember = "ID"
            Me.GLookUp_PartyList.Properties.DisplayMember = "Name"
            Me.GLookUp_PartyList.Properties.DataSource = dview
            Me.GLookUp_PartyListView.RefreshData()
            Me.GLookUp_PartyList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PartyList.Properties.Tag = "NONE"
        End If
    End Sub

    Private Sub Cmb_View_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_View.SelectedIndexChanged
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
    Private Sub Cmb_View_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles Cmb_View.ButtonClick
        If e.Button.Index = 1 Then
            Change_Period()
            Exit Sub
        End If
    End Sub
    Private Sub Change_Period()
        Dim xfrm As New Frm_Change_Period : xfrm.MainBase = Base
        xfrm.Text = Me.Text : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
            xFr_Date = xfrm.xFr_Date : xTo_Date = xfrm.xTo_Date
            xfrm.Dispose()
        Else
            xfrm.Dispose()
            Exit Sub
        End If
        Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        Grid_Display()
    End Sub
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

#End Region

   
End Class
