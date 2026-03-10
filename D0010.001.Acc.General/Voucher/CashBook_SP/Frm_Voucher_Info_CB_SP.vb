Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraPrinting
Imports System.Reflection
Imports System.IO
Imports DevExpress.XtraReports.UI
Public Class Frm_Voucher_Info_CB_SP

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common

    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Private xID As String = "" : Private xMID As String = ""
    Private xFr_Date As Date = Nothing : Private xTo_Date As Date = Nothing
    Private Advanced_Filter_Category As String = "" : Private Advanced_Filter_RefID As String = ""
    Private Negative_MsgStr As String = ""

    Private REC_BANK01 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK02 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK03 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK04 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK05 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK06 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK07 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK08 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK09 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private REC_BANK10 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()

    Private PAY_BANK01 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK02 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK03 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK04 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK05 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK06 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK07 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK08 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK09 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Private PAY_BANK10 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
    Dim FType As String = ""

    Private Open_Cash_Bal, Open_Bank_Bal, Open_Bank_Bal01, Open_Bank_Bal02, Open_Bank_Bal03, Open_Bank_Bal04, Open_Bank_Bal05, Open_Bank_Bal06, Open_Bank_Bal07, Open_Bank_Bal08, Open_Bank_Bal09, Open_Bank_Bal10 As Double
    Private Close_Cash_Bal, Close_Bank_Bal, Close_Bank_Bal01, Close_Bank_Bal02, Close_Bank_Bal03, Close_Bank_Bal04, Close_Bank_Bal05, Close_Bank_Bal06, Close_Bank_Bal07, Close_Bank_Bal08, Close_Bank_Bal09, Close_Bank_Bal10 As Double

    Private Summary_Column_Status As Boolean = False
    Public Sub New()


        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
        OpenCashBook = True
    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        OpenCashBook = False
        Me.Dispose()
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Cash Book" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../
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

        If (keyData = (Keys.F10)) Then ' add action
            ButtonClick(T_ADD_REMARKS, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.F10 Or Keys.Control)) Then ' view action
            ButtonClick(T_REMARKS, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.F11)) Then ' lock
            ButtonClick(T_LOCKED, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.F11 Or Keys.Control)) Then ' unlock
            ButtonClick(T_UNLOCKED, Nothing)
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_NEW.GotFocus, BUT_EDIT.GotFocus, BUT_DELETE.GotFocus, BUT_PRINT.GotFocus, BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus, BUT_LOCKED.GotFocus, BUT_UNLOCKED.GotFocus, BUT_VIEW.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_NEW.LostFocus, BUT_EDIT.LostFocus, BUT_DELETE.LostFocus, BUT_PRINT.LostFocus, BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus, BUT_LOCKED.LostFocus, BUT_UNLOCKED.LostFocus, BUT_VIEW.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click, BUT_DELETE.Click, BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Delete.Click, T_Edit.Click, T_Refresh.Click, T_New.Click, T_Print.Click, But_Filter.Click, But_Find.Click, T_Completed.Click, T_Voucher.Click, T_LOCKED.Click, T_UNLOCKED.Click, T_REMARKS.Click, T_ADD_REMARKS.Click, BUT_UNLOCKED.Click, BUT_LOCKED.Click, T_MATCH_TRANSFERS.Click, T_UNMATCH_TRANSFERS.Click, BUT_VIEW.Click, T_VIEW.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(btn.Name) = "BUT_VIEW" Then Me.DataNavigation("VIEW")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
            If UCase(btn.Name) = "BUT_LOCKED" Then Me.DataNavigation("LOCKED")
            If UCase(btn.Name) = "BUT_UNLOCKED" Then Me.DataNavigation("UNLOCKED")
            If UCase(btn.Name) = "BUT_LOCKED_ALL" Then Me.DataNavigation("LOCKED_ALL")
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
            If UCase(T_btn.Name) = "T_VOUCHER" Then Me.DataNavigation("VOUCHER")
            If UCase(T_btn.Name) = "T_LOCKED" Then Me.DataNavigation("LOCKED")
            If UCase(T_btn.Name) = "T_UNLOCKED" Then Me.DataNavigation("UNLOCKED")
            If UCase(T_btn.Name) = "T_REMARKS" Then Me.DataNavigation("REMARKS")
            If UCase(T_btn.Name) = "T_ADD_REMARKS" Then Me.DataNavigation("ADD REMARKS")
            If UCase(T_btn.Name) = "T_MATCH_TRANSFERS" Then Me.DataNavigation("MATCH TRANSFERS")
            If UCase(T_btn.Name) = "T_UNMATCH_TRANSFERS" Then Me.DataNavigation("UNMATCH TRANSFERS")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_NEW.KeyDown, BUT_EDIT.KeyDown, BUT_DELETE.KeyDown, BUT_PRINT.KeyDown, BUT_CLOSE.KeyDown, But_Filter.KeyDown, But_Find.KeyDown, BUT_LOCKED.KeyDown, BUT_UNLOCKED.KeyDown, BUT_VIEW.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub But_HideBank_Click(sender As System.Object, e As System.EventArgs) Handles But_HideBank.Click
        Dim _CheckFlag As Boolean = False
        If But_HideBank.Text.ToUpper = "HIDE BANK" Then
            If REC_BANK01.tag = "YES" Then REC_BANK01.VISIBLE = False : _CheckFlag = True
            If REC_BANK02.tag = "YES" Then REC_BANK02.VISIBLE = False : _CheckFlag = True
            If REC_BANK03.tag = "YES" Then REC_BANK03.VISIBLE = False : _CheckFlag = True
            If REC_BANK04.tag = "YES" Then REC_BANK04.VISIBLE = False : _CheckFlag = True
            If REC_BANK05.tag = "YES" Then REC_BANK05.VISIBLE = False : _CheckFlag = True
            If REC_BANK06.tag = "YES" Then REC_BANK06.VISIBLE = False : _CheckFlag = True
            If REC_BANK07.tag = "YES" Then REC_BANK07.VISIBLE = False : _CheckFlag = True
            If REC_BANK08.tag = "YES" Then REC_BANK08.VISIBLE = False : _CheckFlag = True
            If REC_BANK09.tag = "YES" Then REC_BANK09.VISIBLE = False : _CheckFlag = True
            If REC_BANK10.tag = "YES" Then REC_BANK10.VISIBLE = False : _CheckFlag = True

            If PAY_BANK01.tag = "YES" Then PAY_BANK01.VISIBLE = False : _CheckFlag = True
            If PAY_BANK02.tag = "YES" Then PAY_BANK02.VISIBLE = False : _CheckFlag = True
            If PAY_BANK03.tag = "YES" Then PAY_BANK03.VISIBLE = False : _CheckFlag = True
            If PAY_BANK04.tag = "YES" Then PAY_BANK04.VISIBLE = False : _CheckFlag = True
            If PAY_BANK05.tag = "YES" Then PAY_BANK05.VISIBLE = False : _CheckFlag = True
            If PAY_BANK06.tag = "YES" Then PAY_BANK06.VISIBLE = False : _CheckFlag = True
            If PAY_BANK07.tag = "YES" Then PAY_BANK07.VISIBLE = False : _CheckFlag = True
            If PAY_BANK08.tag = "YES" Then PAY_BANK08.VISIBLE = False : _CheckFlag = True
            If PAY_BANK09.tag = "YES" Then PAY_BANK09.VISIBLE = False : _CheckFlag = True
            If PAY_BANK10.tag = "YES" Then PAY_BANK10.VISIBLE = False : _CheckFlag = True

            If _CheckFlag Then
                But_HideBank.Text = "Show Bank"
                Me.iTR_REC_BANK.Visible = True : Me.iTR_PAY_BANK.Visible = True
            End If
        Else
            If REC_BANK01.tag = "YES" Then REC_BANK01.VISIBLE = True : _CheckFlag = True
            If REC_BANK02.tag = "YES" Then REC_BANK02.VISIBLE = True : _CheckFlag = True
            If REC_BANK03.tag = "YES" Then REC_BANK03.VISIBLE = True : _CheckFlag = True
            If REC_BANK04.tag = "YES" Then REC_BANK04.VISIBLE = True : _CheckFlag = True
            If REC_BANK05.tag = "YES" Then REC_BANK05.VISIBLE = True : _CheckFlag = True
            If REC_BANK06.tag = "YES" Then REC_BANK06.VISIBLE = True : _CheckFlag = True
            If REC_BANK07.tag = "YES" Then REC_BANK07.VISIBLE = True : _CheckFlag = True
            If REC_BANK08.tag = "YES" Then REC_BANK08.VISIBLE = True : _CheckFlag = True
            If REC_BANK09.tag = "YES" Then REC_BANK09.VISIBLE = True : _CheckFlag = True
            If REC_BANK10.tag = "YES" Then REC_BANK10.VISIBLE = True : _CheckFlag = True

            If PAY_BANK01.tag = "YES" Then PAY_BANK01.VISIBLE = True : _CheckFlag = True
            If PAY_BANK02.tag = "YES" Then PAY_BANK02.VISIBLE = True : _CheckFlag = True
            If PAY_BANK03.tag = "YES" Then PAY_BANK03.VISIBLE = True : _CheckFlag = True
            If PAY_BANK04.tag = "YES" Then PAY_BANK04.VISIBLE = True : _CheckFlag = True
            If PAY_BANK05.tag = "YES" Then PAY_BANK05.VISIBLE = True : _CheckFlag = True
            If PAY_BANK06.tag = "YES" Then PAY_BANK06.VISIBLE = True : _CheckFlag = True
            If PAY_BANK07.tag = "YES" Then PAY_BANK07.VISIBLE = True : _CheckFlag = True
            If PAY_BANK08.tag = "YES" Then PAY_BANK08.VISIBLE = True : _CheckFlag = True
            If PAY_BANK09.tag = "YES" Then PAY_BANK09.VISIBLE = True : _CheckFlag = True
            If PAY_BANK10.tag = "YES" Then PAY_BANK10.VISIBLE = True : _CheckFlag = True

            If _CheckFlag Then
                But_HideBank.Text = "Hide Bank"
                Me.iTR_REC_BANK.Visible = False : Me.iTR_PAY_BANK.Visible = False
            End If
        End If
    End Sub
    Private Sub But_ShowTotal_Click(sender As System.Object, e As System.EventArgs) Handles But_ShowTotal.Click
        If But_ShowTotal.Text.ToUpper = "SHOW TOTAL BAL." Then
            But_ShowTotal.Text = "Hide Total Bal."
            Summary_Column_Status = True
        Else
            But_ShowTotal.Text = "Show Total Bal."
            Summary_Column_Status = False
        End If
        Show_Summary_Balances()
    End Sub
    Private Sub But_Narration_Click(sender As System.Object, e As System.EventArgs) Handles But_Narration.Click
        If But_Narration.Text.ToUpper = "SHOW NARRATION" Then
            Me.BandedGridView1.OptionsView.ShowPreview = True
            But_Narration.Text = "Hide Narration"
        Else
            Me.BandedGridView1.OptionsView.ShowPreview = False
            But_Narration.Text = "Show Narration"
        End If
    End Sub
    Private Sub But_ShowColumn_Click(sender As System.Object, e As System.EventArgs) Handles But_ShowColumn.Click
        Me.BandedGridView1.ColumnsCustomization()
    End Sub
    Private Sub But_ShowFilter_Click(sender As System.Object, e As System.EventArgs) Handles But_ShowFilter.Click
        Me.BandedGridView1.ShowFilterEditor(Me.BandedGridView1.FocusedColumn)
    End Sub
    Private Sub But_AdvFilters_Click(sender As System.Object, e As System.EventArgs) Handles But_AdvFilters.Click
        Me.DataNavigation("ADV. FILTERS")
    End Sub

#End Region

#Region "Start--> Grid Events"

    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.BandedGridView1.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular)
        Me.Band_Voucher.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.Band_Particulars.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.Band_Receipt.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)
        Me.Band_Payment.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold)

        Me.BandedGridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.BandedGridView1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", Zoom1.Value - 1, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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

    Private Sub BandedGridView1_CalcPreviewText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs) Handles BandedGridView1.CalcPreviewText
        'Dim View As GridView = sender
        'Dim Str As String = ""
        'If View.GetRowCellValue(e.RowHandle, BandedGridColumn2).ToString.Length > 0 Then
        '    Str = "Cheque No. " + View.GetRowCellValue(e.RowHandle, BandedGridColumn2).ToString()
        'End If
        'If View.GetRowCellValue(e.RowHandle, BandedGridColumn11).ToString.Length > 0 Then
        '    If Str.Length > 0 Then
        '        Str += ", " & View.GetRowCellValue(e.RowHandle, BandedGridColumn11).ToString()
        '    Else
        '        Str += View.GetRowCellValue(e.RowHandle, BandedGridColumn11).ToString()
        '    End If
        'End If
        'If Str.Length > 0 Then e.PreviewText = Str
    End Sub
    Private Sub BandedGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BandedGridView1.DoubleClick
        Me.DataNavigation("EDIT")
    End Sub
    Private Sub BandedGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BandedGridView1.KeyDown
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
    Private Sub BandedGridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged
        Try
            If Me.BandedGridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag1 Then
                    CreationDetail(e.FocusedRowHandle)
                End If
            Else
                Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = ""  ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = ""
            End If
        Catch ex As Exception
            Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = ""  ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = ""
        End Try
    End Sub
    Private Sub CreationDetail(ByVal Xrow As Integer)
        If Me.BandedGridView1.GetRowCellValue(0, "iREC_ID").ToString.Length > 0 Then
            If Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_ID").ToString = "OPENING BALANCE" Then
                Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = ""  ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = "" 
                Exit Sub
            End If
            Me.Pic_Status.Visible = True : Dim Status As String = "" ': Me.Lbl_Seprator1.Visible = True
            Try
                Status = Me.BandedGridView1.GetRowCellValue(Xrow, "iACTION_STATUS").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Pic_Status.Image = My.Resources.lock 'Me.Lbl_Status.Text = "Completed" : : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Pic_Status.Image = My.Resources.unlock 'Me.Lbl_Status.Text = Status : : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If

            Dim Add_Date As String = "" : Dim Add_By As String = "" : Dim Status_On As String = "" : Dim Status_By As String = "" : Dim StatusStr As String = "Completed"
            Try
                Add_Date = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_ADD_ON").ToString
                Add_By = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_ADD_BY").ToString()
                Status_On = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_STATUS_ON").ToString
                Status_By = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_STATUS_BY").ToString()
            Catch ex As Exception
            End Try
            If Status = "Locked" Then
                StatusStr = "Locked"
            Else
                StatusStr = "UnLocked"
            End If
            If IsDate(Status_On) Then
                Lbl_StatusOn.Text = StatusStr & " On: " & IIf(IsDBNull(Status_On), "", Status_On) : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            Else
                Lbl_StatusOn.Text = StatusStr & " On: " & "?" : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            End If

            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_EDIT_ON").ToString
                Edit_By = Me.BandedGridView1.GetRowCellValue(Xrow, "iREC_EDIT_BY").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
        Else
            Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = ""
        End If
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" : BE_Cash_Bank.Text = "" ': Lbl_Status.Text = "" : Lbl_Seprator1.Visible = False
        If BandedGridView1.FocusedRowHandle >= 0 Then CreationDetail(BandedGridView1.FocusedRowHandle)
        Me.CancelButton = Me.BUT_CLOSE

        Fill_Change_Period_Items()

        Create_Bank_Columns()
        GridDefaultProperty()

        Dim MaxValue As Object = 0 : Dim xLastDate As Date = Now.Date
        MaxValue = Base._Voucher_DBOps.GetMaxTransactionDate()
        If MaxValue Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close() : Exit Sub
        End If
        If IsDBNull(MaxValue) Then xLastDate = Base._open_Year_Sdt Else xLastDate = MaxValue

        Dim xMM As Integer = xLastDate.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
        Me.BandedGridView1.Focus()
        xPleaseWait.Hide()
    End Sub

    Private Sub BE_Cash_Bank_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BE_Cash_Bank.ButtonClick
        Dim xfrm As New Frm_View_Summary : xfrm.MainBase = Base
        xfrm.Text = "Summary..." : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        xfrm.Dispose()
    End Sub

    Private Sub Create_Bank_Columns()
        REC_BANK01.Name = "REC_BANK01" : REC_BANK01.AppearanceCell.Options.UseTextOptions = True : REC_BANK01.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK01.AppearanceHeader.Options.UseTextOptions = True : REC_BANK01.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK01.Caption = "REC_BANK01" : REC_BANK01.DisplayFormat.FormatString = "#,0.00" : REC_BANK01.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK01.Visible = False : REC_BANK01.Width = 80 : REC_BANK01.Tag = "NO"
        REC_BANK02.Name = "REC_BANK02" : REC_BANK02.AppearanceCell.Options.UseTextOptions = True : REC_BANK02.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK02.AppearanceHeader.Options.UseTextOptions = True : REC_BANK02.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK02.Caption = "REC_BANK02" : REC_BANK02.DisplayFormat.FormatString = "#,0.00" : REC_BANK02.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK02.Visible = False : REC_BANK02.Width = 80 : REC_BANK02.Tag = "NO"
        REC_BANK03.Name = "REC_BANK03" : REC_BANK03.AppearanceCell.Options.UseTextOptions = True : REC_BANK03.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK03.AppearanceHeader.Options.UseTextOptions = True : REC_BANK03.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK03.Caption = "REC_BANK03" : REC_BANK03.DisplayFormat.FormatString = "#,0.00" : REC_BANK03.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK03.Visible = False : REC_BANK03.Width = 80 : REC_BANK03.Tag = "NO"
        REC_BANK04.Name = "REC_BANK04" : REC_BANK04.AppearanceCell.Options.UseTextOptions = True : REC_BANK04.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK04.AppearanceHeader.Options.UseTextOptions = True : REC_BANK04.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK04.Caption = "REC_BANK04" : REC_BANK04.DisplayFormat.FormatString = "#,0.00" : REC_BANK04.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK04.Visible = False : REC_BANK04.Width = 80 : REC_BANK04.Tag = "NO"
        REC_BANK05.Name = "REC_BANK05" : REC_BANK05.AppearanceCell.Options.UseTextOptions = True : REC_BANK05.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK05.AppearanceHeader.Options.UseTextOptions = True : REC_BANK05.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK05.Caption = "REC_BANK05" : REC_BANK05.DisplayFormat.FormatString = "#,0.00" : REC_BANK05.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK05.Visible = False : REC_BANK05.Width = 80 : REC_BANK05.Tag = "NO"
        REC_BANK06.Name = "REC_BANK06" : REC_BANK06.AppearanceCell.Options.UseTextOptions = True : REC_BANK06.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK06.AppearanceHeader.Options.UseTextOptions = True : REC_BANK06.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK06.Caption = "REC_BANK06" : REC_BANK06.DisplayFormat.FormatString = "#,0.00" : REC_BANK06.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK06.Visible = False : REC_BANK06.Width = 80 : REC_BANK06.Tag = "NO"
        REC_BANK07.Name = "REC_BANK07" : REC_BANK07.AppearanceCell.Options.UseTextOptions = True : REC_BANK07.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK07.AppearanceHeader.Options.UseTextOptions = True : REC_BANK07.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK07.Caption = "REC_BANK07" : REC_BANK07.DisplayFormat.FormatString = "#,0.00" : REC_BANK07.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK07.Visible = False : REC_BANK07.Width = 80 : REC_BANK07.Tag = "NO"
        REC_BANK08.Name = "REC_BANK08" : REC_BANK08.AppearanceCell.Options.UseTextOptions = True : REC_BANK08.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK08.AppearanceHeader.Options.UseTextOptions = True : REC_BANK08.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK08.Caption = "REC_BANK08" : REC_BANK08.DisplayFormat.FormatString = "#,0.00" : REC_BANK08.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK08.Visible = False : REC_BANK08.Width = 80 : REC_BANK08.Tag = "NO"
        REC_BANK09.Name = "REC_BANK09" : REC_BANK09.AppearanceCell.Options.UseTextOptions = True : REC_BANK09.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK09.AppearanceHeader.Options.UseTextOptions = True : REC_BANK09.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK09.Caption = "REC_BANK09" : REC_BANK09.DisplayFormat.FormatString = "#,0.00" : REC_BANK09.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK09.Visible = False : REC_BANK09.Width = 80 : REC_BANK09.Tag = "NO"
        REC_BANK10.Name = "REC_BANK10" : REC_BANK10.AppearanceCell.Options.UseTextOptions = True : REC_BANK10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK10.AppearanceHeader.Options.UseTextOptions = True : REC_BANK10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : REC_BANK10.Caption = "REC_BANK10" : REC_BANK10.DisplayFormat.FormatString = "#,0.00" : REC_BANK10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : REC_BANK10.Visible = False : REC_BANK10.Width = 80 : REC_BANK10.Tag = "NO"

        PAY_BANK01.Name = "PAY_BANK01" : PAY_BANK01.AppearanceCell.Options.UseTextOptions = True : PAY_BANK01.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK01.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK01.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK01.Caption = "PAY_BANK01" : PAY_BANK01.DisplayFormat.FormatString = "#,0.00" : PAY_BANK01.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK01.Visible = False : PAY_BANK01.Width = 80 : PAY_BANK01.Tag = "NO"
        PAY_BANK02.Name = "PAY_BANK02" : PAY_BANK02.AppearanceCell.Options.UseTextOptions = True : PAY_BANK02.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK02.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK02.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK02.Caption = "PAY_BANK02" : PAY_BANK02.DisplayFormat.FormatString = "#,0.00" : PAY_BANK02.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK02.Visible = False : PAY_BANK02.Width = 80 : PAY_BANK02.Tag = "NO"
        PAY_BANK03.Name = "PAY_BANK03" : PAY_BANK03.AppearanceCell.Options.UseTextOptions = True : PAY_BANK03.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK03.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK03.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK03.Caption = "PAY_BANK03" : PAY_BANK03.DisplayFormat.FormatString = "#,0.00" : PAY_BANK03.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK03.Visible = False : PAY_BANK03.Width = 80 : PAY_BANK03.Tag = "NO"
        PAY_BANK04.Name = "PAY_BANK04" : PAY_BANK04.AppearanceCell.Options.UseTextOptions = True : PAY_BANK04.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK04.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK04.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK04.Caption = "PAY_BANK04" : PAY_BANK04.DisplayFormat.FormatString = "#,0.00" : PAY_BANK04.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK04.Visible = False : PAY_BANK04.Width = 80 : PAY_BANK04.Tag = "NO"
        PAY_BANK05.Name = "PAY_BANK05" : PAY_BANK05.AppearanceCell.Options.UseTextOptions = True : PAY_BANK05.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK05.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK05.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK05.Caption = "PAY_BANK05" : PAY_BANK05.DisplayFormat.FormatString = "#,0.00" : PAY_BANK05.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK05.Visible = False : PAY_BANK05.Width = 80 : PAY_BANK05.Tag = "NO"
        PAY_BANK06.Name = "PAY_BANK06" : PAY_BANK06.AppearanceCell.Options.UseTextOptions = True : PAY_BANK06.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK06.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK06.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK06.Caption = "PAY_BANK06" : PAY_BANK06.DisplayFormat.FormatString = "#,0.00" : PAY_BANK06.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK06.Visible = False : PAY_BANK06.Width = 80 : PAY_BANK06.Tag = "NO"
        PAY_BANK07.Name = "PAY_BANK07" : PAY_BANK07.AppearanceCell.Options.UseTextOptions = True : PAY_BANK07.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK07.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK07.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK07.Caption = "PAY_BANK07" : PAY_BANK07.DisplayFormat.FormatString = "#,0.00" : PAY_BANK07.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK07.Visible = False : PAY_BANK07.Width = 80 : PAY_BANK07.Tag = "NO"
        PAY_BANK08.Name = "PAY_BANK08" : PAY_BANK08.AppearanceCell.Options.UseTextOptions = True : PAY_BANK08.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK08.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK08.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK08.Caption = "PAY_BANK08" : PAY_BANK08.DisplayFormat.FormatString = "#,0.00" : PAY_BANK08.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK08.Visible = False : PAY_BANK08.Width = 80 : PAY_BANK08.Tag = "NO"
        PAY_BANK09.Name = "PAY_BANK09" : PAY_BANK09.AppearanceCell.Options.UseTextOptions = True : PAY_BANK09.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK09.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK09.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK09.Caption = "PAY_BANK09" : PAY_BANK09.DisplayFormat.FormatString = "#,0.00" : PAY_BANK09.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK09.Visible = False : PAY_BANK09.Width = 80 : PAY_BANK09.Tag = "NO"
        PAY_BANK10.Name = "PAY_BANK10" : PAY_BANK10.AppearanceCell.Options.UseTextOptions = True : PAY_BANK10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK10.AppearanceHeader.Options.UseTextOptions = True : PAY_BANK10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far : PAY_BANK10.Caption = "PAY_BANK10" : PAY_BANK10.DisplayFormat.FormatString = "#,0.00" : PAY_BANK10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : PAY_BANK10.Visible = False : PAY_BANK10.Width = 80 : PAY_BANK10.Tag = "NO"
    End Sub

    Public Sub Grid_Display()
        xPleaseWait.Show("Cash Book" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : RowFlag1 = False : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Status.Text = "": Lbl_Seprator1.Visible = False
        Me.BandedGridView1.OptionsView.AllowCellMerge = False

        '-----------------If Advance filter is applied
        Dim final_str As String = ""
        If Me.BandedGridView1.ActiveFilterString.Length > 0 And Me.BandedGridView1.ActiveFilterString.Contains("[Advanced_Filter]") Then  'Bug #5754 fix
            Dim Ind As Integer = Me.BandedGridView1.ActiveFilterString.IndexOf("[Advanced_Filter]")
            Dim sub_str1 As String = ""
            If Ind > 0 Then sub_str1 = BandedGridView1.ActiveFilterString.Substring(0, Ind - 1)
            Dim sub_str2 As String = BandedGridView1.ActiveFilterString.Substring(Ind)
            If sub_str2.Split("=").Count > 1 Then
                Advanced_Filter_Category = sub_str2.Split("=")(1).Replace("'", "").Trim
                Advanced_Filter_RefID = sub_str2.Split("=")(2).Replace("'", "").Trim
            End If
            final_str = sub_str1 + " [Advanced_Filter] ='" & Advanced_Filter_Category & " = " & Advanced_Filter_RefID & "'"
        End If



        '---------------Get transaction from SP
        Dim CashTR_DataSet As DataSet = Base._Voucher_DBOps.GetListBySP(xFr_Date, xTo_Date, Base._open_Ins_ID, Base._open_Year_Edt, Base._open_Year_Sdt, Advanced_Filter_Category, Advanced_Filter_RefID)
        Dim TR_Table As DataTable = CashTR_DataSet.Tables(0)
        Dim Bank_Table As DataTable = CashTR_DataSet.Tables(1)
        Dim OpenCloseBal_Table As DataTable = CashTR_DataSet.Tables(2)

        If Advanced_Filter_Category.Length > 0 And final_str.Length > 0 Then
            Me.BandedGridView1.ActiveFilterString = final_str
            Advanced_Filter_Category = "" : Advanced_Filter_RefID = ""
        End If

        If TR_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim bankCount As Integer
        'Try
        'get total number of banks
        bankCount = Convert.ToInt32(Bank_Table.Rows.Count().ToString()) 'to do Dad

        '-------------------(1) Get Data--------------------------------------------------------------------------------------------
        '
        ' Get Cash Balance..............
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        If OpenCloseBal_Table.Rows.Count > 0 Then
            Open_Cash_Bal = Convert.ToDouble(OpenCloseBal_Table.Rows(0)("Open_Cash_Bal").ToString())
            Close_Cash_Bal = Convert.ToDouble(OpenCloseBal_Table.Rows(0)("Close_Cash_Bal").ToString())
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        Dim _bankCnt As Integer = 1 : Dim _colWidth As Integer = 0 : Dim _colSetWidth As Integer = 90
        Dim _online_BANK_COL_TR_REC As String = "" : Dim _online_BANK_COL_NB_REC As String = "" : Dim _online_BANK_COL_TR_PAY As String = "" : Dim _online_BANK_COL_NB_PAY As String = ""
        Dim _local__BANK_COL_TR_REC As String = "" : Dim _local__BANK_COL_NB_REC As String = "" : Dim _local__BANK_COL_TR_PAY As String = "" : Dim _local__BANK_COL_NB_PAY As String = ""
        '
        Me.Band_Receipt.Columns.Remove(Me.iTR_REC_JOURNAL) : Me.Band_Receipt.Columns.Remove(Me.iTR_REC_TOTAL)
        If Bank_Table.Rows.Count > 0 Then Me.iTR_REC_BANK.Visible = False : Me.iTR_PAY_BANK.Visible = False : _colWidth -= _colSetWidth : But_HideBank.Text = "Hide Bank" Else But_HideBank.Text = "Show Bank"
        Me.Band_Payment.Columns.Remove(Me.iTR_PAY_JOURNAL) : Me.Band_Payment.Columns.Remove(Me.iTR_PAY_TOTAL)
        Me.Band_Receipt.Columns.Remove(Me.REC_BANK01) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK02) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK03) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK04) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK05) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK06) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK07) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK08) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK09) : Me.Band_Receipt.Columns.Remove(Me.REC_BANK10)
        Me.Band_Payment.Columns.Remove(Me.PAY_BANK01) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK02) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK03) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK04) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK05) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK06) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK07) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK08) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK09) : Me.Band_Payment.Columns.Remove(Me.PAY_BANK10)
        REC_BANK01.Tag = "NO" : REC_BANK02.Tag = "NO" : REC_BANK03.Tag = "NO" : REC_BANK04.Tag = "NO" : REC_BANK05.Tag = "NO" : REC_BANK06.Tag = "NO" : REC_BANK07.Tag = "NO" : REC_BANK08.Tag = "NO" : REC_BANK09.Tag = "NO" : REC_BANK10.Tag = "NO"
        '
        If bankCount > 0 Then
            Open_Bank_Bal = Convert.ToDouble(OpenCloseBal_Table.Rows(0)("Open_Bank_Bal").ToString())
            Close_Bank_Bal = Convert.ToDouble(OpenCloseBal_Table.Rows(0)("Close_Bank_Bal").ToString())
            For Each XROW In Bank_Table.Rows
                Select Case _bankCnt
                    Case 1
                        Open_Bank_Bal01 = XROW("OPENING")
                        Close_Bank_Bal01 = XROW("CLOSING")

                        REC_BANK01.FieldName = XROW("RECFIELDNAME") : REC_BANK01.Caption = XROW("CAPTION")
                        REC_BANK01.ToolTip = XROW("TOOLTIP") : REC_BANK01.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK01.Visible = True : REC_BANK01.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK01}) : Me.Band_Receipt.Columns.Add(REC_BANK01)

                        PAY_BANK01.FieldName = XROW("PAYFIELDNAME") : PAY_BANK01.Caption = XROW("CAPTION")
                        PAY_BANK01.ToolTip = XROW("TOOLTIP") : PAY_BANK01.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK01.Visible = True : PAY_BANK01.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK01}) : Me.Band_Payment.Columns.Add(PAY_BANK01)
                        _colWidth += _colSetWidth
                    Case 2
                        Open_Bank_Bal02 = XROW("OPENING")
                        Close_Bank_Bal02 = XROW("CLOSING")

                        REC_BANK02.FieldName = XROW("RECFIELDNAME") : REC_BANK02.Caption = XROW("CAPTION")
                        REC_BANK02.ToolTip = XROW("TOOLTIP") : REC_BANK02.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK02.Visible = True : REC_BANK02.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK02}) : Me.Band_Receipt.Columns.Add(REC_BANK02)

                        PAY_BANK02.FieldName = XROW("PAYFIELDNAME") : PAY_BANK02.Caption = XROW("CAPTION")
                        PAY_BANK02.ToolTip = XROW("TOOLTIP") : PAY_BANK02.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK02.Visible = True : PAY_BANK02.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK02}) : Me.Band_Payment.Columns.Add(PAY_BANK02)
                        _colWidth += _colSetWidth
                    Case 3
                        Open_Bank_Bal03 = XROW("OPENING")
                        Close_Bank_Bal03 = XROW("CLOSING")

                        REC_BANK03.FieldName = XROW("RECFIELDNAME") : REC_BANK03.Caption = XROW("CAPTION")
                        REC_BANK03.ToolTip = XROW("TOOLTIP") : REC_BANK03.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK03.Visible = True : REC_BANK03.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK03}) : Me.Band_Receipt.Columns.Add(REC_BANK03)

                        PAY_BANK03.FieldName = XROW("PAYFIELDNAME") : PAY_BANK03.Caption = XROW("CAPTION")
                        PAY_BANK03.ToolTip = XROW("TOOLTIP") : PAY_BANK03.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK03.Visible = True : PAY_BANK03.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK03}) : Me.Band_Payment.Columns.Add(PAY_BANK03)
                        _colWidth += _colSetWidth
                    Case 4
                        Open_Bank_Bal04 = XROW("OPENING")
                        Close_Bank_Bal04 = XROW("CLOSING")

                        REC_BANK04.FieldName = XROW("RECFIELDNAME") : REC_BANK04.Caption = XROW("CAPTION")
                        REC_BANK04.ToolTip = XROW("TOOLTIP") : REC_BANK04.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK04.Visible = True : REC_BANK04.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK04}) : Me.Band_Receipt.Columns.Add(REC_BANK04)

                        PAY_BANK04.FieldName = XROW("PAYFIELDNAME") : PAY_BANK04.Caption = XROW("CAPTION")
                        PAY_BANK04.ToolTip = XROW("TOOLTIP") : PAY_BANK04.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK04.Visible = True : PAY_BANK04.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK04}) : Me.Band_Payment.Columns.Add(PAY_BANK04)
                        _colWidth += _colSetWidth
                    Case 5
                        Open_Bank_Bal05 = XROW("OPENING")
                        Close_Bank_Bal05 = XROW("CLOSING")

                        REC_BANK05.FieldName = XROW("RECFIELDNAME") : REC_BANK05.Caption = XROW("CAPTION")
                        REC_BANK05.ToolTip = XROW("TOOLTIP") : REC_BANK05.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK05.Visible = True : REC_BANK05.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK05}) : Me.Band_Receipt.Columns.Add(REC_BANK05)

                        PAY_BANK05.FieldName = XROW("PAYFIELDNAME") : PAY_BANK05.Caption = XROW("CAPTION")
                        PAY_BANK05.ToolTip = XROW("TOOLTIP") : PAY_BANK05.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK05.Visible = True : PAY_BANK05.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK05}) : Me.Band_Payment.Columns.Add(PAY_BANK05)
                        _colWidth += _colSetWidth
                    Case 6
                        Open_Bank_Bal06 = XROW("OPENING")
                        Close_Bank_Bal06 = XROW("CLOSING")

                        REC_BANK06.FieldName = XROW("RECFIELDNAME") : REC_BANK06.Caption = XROW("CAPTION")
                        REC_BANK06.ToolTip = XROW("TOOLTIP") : REC_BANK06.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK06.Visible = True : REC_BANK06.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK06}) : Me.Band_Receipt.Columns.Add(REC_BANK06)

                        PAY_BANK06.FieldName = XROW("PAYFIELDNAME") : PAY_BANK06.Caption = XROW("CAPTION")
                        PAY_BANK06.ToolTip = XROW("TOOLTIP") : PAY_BANK06.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK06.Visible = True : PAY_BANK06.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK06}) : Me.Band_Payment.Columns.Add(PAY_BANK06)
                        _colWidth += _colSetWidth
                    Case 7
                        Open_Bank_Bal07 = XROW("OPENING")
                        Close_Bank_Bal07 = XROW("CLOSING")

                        REC_BANK07.FieldName = XROW("RECFIELDNAME") : REC_BANK07.Caption = XROW("CAPTION")
                        REC_BANK07.ToolTip = XROW("TOOLTIP") : REC_BANK07.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK07.Visible = True : REC_BANK07.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK07}) : Me.Band_Receipt.Columns.Add(REC_BANK07)

                        PAY_BANK07.FieldName = XROW("PAYFIELDNAME") : PAY_BANK07.Caption = XROW("CAPTION")
                        PAY_BANK07.ToolTip = XROW("TOOLTIP") : PAY_BANK07.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK07.Visible = True : PAY_BANK07.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK07}) : Me.Band_Payment.Columns.Add(PAY_BANK07)
                        _colWidth += _colSetWidth
                    Case 8
                        Open_Bank_Bal08 = XROW("OPENING")
                        Close_Bank_Bal08 = XROW("CLOSING")

                        REC_BANK08.FieldName = XROW("RECFIELDNAME") : REC_BANK08.Caption = XROW("CAPTION")
                        REC_BANK08.ToolTip = XROW("TOOLTIP") : REC_BANK08.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK08.Visible = True : REC_BANK08.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK08}) : Me.Band_Receipt.Columns.Add(REC_BANK08)

                        PAY_BANK08.FieldName = XROW("PAYFIELDNAME") : PAY_BANK08.Caption = XROW("CAPTION")
                        PAY_BANK08.ToolTip = XROW("TOOLTIP") : PAY_BANK08.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK08.Visible = True : PAY_BANK08.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK08}) : Me.Band_Payment.Columns.Add(PAY_BANK08)
                        _colWidth += _colSetWidth
                    Case 9
                        Open_Bank_Bal09 = XROW("OPENING")
                        Close_Bank_Bal09 = XROW("CLOSING")

                        REC_BANK09.FieldName = XROW("RECFIELDNAME") : REC_BANK09.Caption = XROW("CAPTION")
                        REC_BANK09.ToolTip = XROW("TOOLTIP") : REC_BANK09.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK09.Visible = True : REC_BANK09.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK09}) : Me.Band_Receipt.Columns.Add(REC_BANK09)

                        PAY_BANK09.FieldName = XROW("PAYFIELDNAME") : PAY_BANK09.Caption = XROW("CAPTION")
                        PAY_BANK09.ToolTip = XROW("TOOLTIP") : PAY_BANK09.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK09.Visible = True : PAY_BANK09.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK09}) : Me.Band_Payment.Columns.Add(PAY_BANK09)
                        _colWidth += _colSetWidth
                    Case 10
                        Open_Bank_Bal10 = XROW("OPENING")
                        Close_Bank_Bal10 = XROW("CLOSING")

                        REC_BANK10.FieldName = XROW("RECFIELDNAME") : REC_BANK10.Caption = XROW("CAPTION")
                        REC_BANK10.ToolTip = XROW("TOOLTIP") : REC_BANK10.CustomizationCaption = XROW("RECCUSTOMIZATIONCAPTION") : REC_BANK10.Visible = True : REC_BANK10.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {REC_BANK10}) : Me.Band_Receipt.Columns.Add(REC_BANK10)

                        PAY_BANK10.FieldName = XROW("PAYFIELDNAME") : PAY_BANK10.Caption = XROW("CAPTION")
                        PAY_BANK10.ToolTip = XROW("TOOLTIP") : PAY_BANK10.CustomizationCaption = XROW("PAYCUSTOMIZATIONCAPTION") : PAY_BANK10.Visible = True : PAY_BANK10.Tag = "YES" : Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {PAY_BANK10}) : Me.Band_Payment.Columns.Add(PAY_BANK10)
                        _colWidth += _colSetWidth
                End Select
                _bankCnt += 1
            Next
        Else
            Open_Bank_Bal = 0 : Open_Bank_Bal01 = 0 : Open_Bank_Bal02 = 0 : Open_Bank_Bal03 = 0 : Open_Bank_Bal04 = 0 : Open_Bank_Bal05 = 0 : Open_Bank_Bal06 = 0 : Open_Bank_Bal07 = 0 : Open_Bank_Bal08 = 0 : Open_Bank_Bal09 = 0 : Open_Bank_Bal10 = 0
            Close_Bank_Bal = 0 : Close_Bank_Bal01 = 0 : Close_Bank_Bal02 = 0 : Close_Bank_Bal03 = 0 : Close_Bank_Bal04 = 0 : Close_Bank_Bal05 = 0 : Close_Bank_Bal06 = 0 : Close_Bank_Bal07 = 0 : Close_Bank_Bal08 = 0 : Close_Bank_Bal09 = 0 : Close_Bank_Bal10 = 0
        End If
        Me.Band_Receipt.Columns.Add(Me.iTR_REC_JOURNAL) : Me.Band_Receipt.Columns.Add(Me.iTR_REC_TOTAL)
        Me.Band_Payment.Columns.Add(Me.iTR_PAY_JOURNAL) : Me.Band_Payment.Columns.Add(Me.iTR_PAY_TOTAL)
        BE_Cash_Bank.Text = "Cash: " & Format(Close_Cash_Bal, "#,0.00") & "    Bank: " & Format(Close_Bank_Bal, "#,0.00")

       
        Me.GridControl1.DataSource = TR_Table ': TR_Table.Dispose()
        '-------------------------------------------------------------------------------------------------------------------------------------
        Me.BandedGridView1.Columns("iTR_VNO").Visible = False
        Me.BandedGridView1.Columns("iTR_REF_NO").Visible = False
        Me.BandedGridView1.Columns("iTR_NARRATION").Visible = False
        '-------------------------------------------------------------------------------------------------------------------------------------
        Show_Summary_Balances()
        '-------------------------------------------------------------------------------------------------------------------------------------
        ' Me.BandedGridView1.BestFitMaxRowCount = 10 : Me.BandedGridView1.BestFitColumns()' Bug #5271 fix
        '-------------------------------------------------------------------------------------------------------------------------------------
        Me.Band_Receipt.Width = 400 + _colWidth
        Me.Band_Particulars.Width = 500
        Me.Band_Payment.Width = 400 + _colWidth
        Me.BandedGridView1.PreviewFieldName = "iTR_NARRATION"
        Me.BandedGridView1.OptionsView.AllowCellMerge = True
        '-------------------------------------------------------------------------------------------------------------------------------------
        Dim condition1 As StyleFormatCondition = New StyleFormatCondition() : condition1.Appearance.BackColor = Color.Linen : condition1.Appearance.Options.UseBackColor = True 'Color.OldLace
        condition1.Condition = FormatConditionEnum.Expression : condition1.Expression = "[iTR_REF_NO]%2=0" : condition1.ApplyToRow = True
        BandedGridView1.FormatConditions.Add(condition1)

        Dim condition2 As StyleFormatCondition = New StyleFormatCondition() : condition2.Appearance.BackColor = Color.AliceBlue : condition2.Appearance.Options.UseBackColor = True
        condition2.Condition = FormatConditionEnum.Expression : condition2.Expression = "[iTR_REF_NO]%2<>0" : condition2.ApplyToRow = True
        BandedGridView1.FormatConditions.Add(condition2)
        '--------------------------------------------------------------------------------------------------------------------------------------
        If Not Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Manage_Remarks) Then
            Me.T_ADD_REMARKS.Visible = False
        End If
        If Not Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Then
            Me.T_LOCKED.Visible = False : Me.T_UNLOCKED.Visible = False : Me.T_MATCH_TRANSFERS.Visible = False
            Me.BUT_UNLOCKED.Visible = False : Me.BUT_LOCKED.Visible = False : Me.T_UNMATCH_TRANSFERS.Visible = False
        End If
        HighlightRemarks(BandedGridView1)
        '-------------------------------------------------------------------------------------------------------------------------------------
        If Me.BandedGridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("iTR_DATE")
                Me.BandedGridView1.MoveBy(Me.BandedGridView1.LocateByValue("iREC_ID", xID))
            Catch ex As Exception
                Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("iTR_DATE")
                Me.BandedGridView1.FocusedRowHandle = BandedGridView1.RowCount - 1
            End Try
            If BandedGridView1.FocusedRowHandle = 0 Then
                Try
                    Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("iTR_DATE")
                    Me.BandedGridView1.MoveBy(Me.BandedGridView1.LocateByValue("iTR_M_ID", xMID))
                Catch ex As Exception
                    Me.BandedGridView1.FocusedColumn = Me.BandedGridView1.Columns("iTR_DATE")
                    Me.BandedGridView1.FocusedRowHandle = BandedGridView1.RowCount - 1
                End Try
            End If
            Me.BandedGridView1.Focus()
            RowFlag1 = True
            CreationDetail(Me.BandedGridView1.FocusedRowHandle)
            Me.BandedGridView1.Focus()
        End If

        Check_Negative_Balance()
        xPleaseWait.Hide()

        If Negative_MsgStr.Trim.Length > 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Negative_MsgStr, "Alert...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Check_Negative_Balance()
        xPleaseWait.Show("Cash Book" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        xPleaseWait.Activate()
        xPleaseWait.Focus()
        Dim TR_Table As DataTable = Nothing : Dim DV1 As DataView = Nothing : Dim XTABLE As DataTable = Nothing : Dim _Temp_Balance, _Temp_Receipt, _Temp_Payment As Double
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(1) Cash Negative Balance
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(1.1) Cash Transaction......................
        TR_Table = Base._Voucher_DBOps.GetNegativeBalance("00080", "", "")
        If TR_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '(1.2) Get Cash Opening Balance..............
        Dim Opening_Bal As Double = 0
        If xFr_Date <> Base._open_Year_Sdt Then
            Opening_Bal = 0
            Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(Base._open_Year_Sdt, Base._open_Year_Edt, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
            If Cash_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
            If Cash_Bal.Rows.Count > 0 Then
                If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Opening_Bal = Cash_Bal.Rows(0)("OPENING") Else Opening_Bal = 0
            Else : Opening_Bal = 0 : End If
        Else
            Opening_Bal = Open_Cash_Bal
        End If
        '(1.3) Cash Opening Balance Insert..........
        Dim ROW As DataRow : ROW = TR_Table.NewRow : ROW("iTR_SORT_REC") = "A" : ROW("iTR_DATE") = Format(Base._open_Year_Sdt, Base._Date_Format_Current) : ROW("iTR_REC_CASH") = Opening_Bal : ROW("iTR_PAY_CASH") = 0 : TR_Table.Rows.Add(ROW)
        '(1.4) Cash Data Sorting
        DV1 = New DataView(TR_Table) : DV1.Sort = "iTR_DATE,iTR_SORT_REC" : XTABLE = DV1.ToTable
        Negative_MsgStr = "" : _Temp_Balance = 0 : _Temp_Receipt = 0 : _Temp_Payment = 0
        '(1.5) Check Negative Cash
        For Each XROW In XTABLE.Rows
            If Not IsDBNull(XROW("iTR_REC_CASH")) Then _Temp_Receipt = XROW("iTR_REC_CASH") Else _Temp_Receipt = 0
            If Not IsDBNull(XROW("iTR_PAY_CASH")) Then _Temp_Payment = XROW("iTR_PAY_CASH") Else _Temp_Payment = 0
            If _Temp_Receipt <= 0 And _Temp_Payment <= 0 Then : Else
                _Temp_Balance = Math.Round((_Temp_Balance + _Temp_Receipt) - _Temp_Payment, 2)
            End If
            If _Temp_Balance < 0 Then
                Negative_MsgStr = "N E G A T I V E   B A L A N C E" & vbNewLine & "==========================================" & vbNewLine & "In Cash..." & vbNewLine & "Date      : " & Format(XROW("iTR_DATE"), "dd-MMM, yyyy") & vbNewLine & "Amount : " & Format(_Temp_Balance, "#,0.##") & vbNewLine & "==========================================" & vbNewLine & "For more details: Check Daily Balances Report" : Exit For
            End If
        Next
        '
        If Negative_MsgStr.Trim.Length > 0 Then Exit Sub
        '
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(2) Bank Negative Balance
        '-------------------------------------------------------------------------------------------------------------------------------------
        '(2.0) Get Bank Detail..............
        Dim Bank_Det As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(Base._open_Year_Sdt, Base._open_Year_Edt, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Det Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '
        Dim OtherCondition As String = "" : Opening_Bal = 0
        If Bank_Det.Rows.Count > 0 Then
            For Each B_Row In Bank_Det.Rows
                '(2.1) Get Bank Opening Balance & ID.........
                OtherCondition = " AND ( TR_SUB_CR_LED_ID ='" & B_Row("ID") & "' OR TR_SUB_DR_LED_ID ='" & B_Row("ID") & "' ) "
                If Not IsDBNull(B_Row("OPENING")) Then Opening_Bal = B_Row("OPENING") Else Opening_Bal = 0
                '(2.2) Bank Transaction......................
                TR_Table = Base._Voucher_DBOps.GetNegativeBalance("00079", B_Row("ID"), OtherCondition)
                If TR_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                '(2.3) Cash Opening Balance Insert..........
                ROW = TR_Table.NewRow : ROW("iTR_SORT_REC") = "A" : ROW("iTR_DATE") = Format(Base._open_Year_Sdt, Base._Date_Format_Current) : ROW("iTR_REC_BANK") = Opening_Bal : ROW("iTR_PAY_BANK") = 0 : TR_Table.Rows.Add(ROW)
                '(2.4) Cash Data Sorting
                DV1 = New DataView(TR_Table) : DV1.Sort = "iTR_DATE,iTR_SORT_REC" : XTABLE = DV1.ToTable
                Negative_MsgStr = "" : _Temp_Balance = 0 : _Temp_Receipt = 0 : _Temp_Payment = 0
                '(2.5) Check Negative Cash
                For Each XROW In XTABLE.Rows
                    If Not IsDBNull(XROW("iTR_REC_BANK")) Then _Temp_Receipt = XROW("iTR_REC_BANK") Else _Temp_Receipt = 0
                    If Not IsDBNull(XROW("iTR_PAY_BANK")) Then _Temp_Payment = XROW("iTR_PAY_BANK") Else _Temp_Payment = 0
                    If _Temp_Receipt <= 0 And _Temp_Payment <= 0 Then : Else
                        _Temp_Balance = Math.Round((_Temp_Balance + _Temp_Receipt) - _Temp_Payment, 2)
                    End If
                    If _Temp_Balance < 0 Then
                        Negative_MsgStr = "N E G A T I V E   B A L A N C E" & vbNewLine & "==========================================" & vbNewLine & "In Bank  : " & B_Row("BI_SHORT_NAME") & ", A/c. No.: " & B_Row("BA_ACCOUNT_NO") & vbNewLine & "Date      : " & Format(XROW("iTR_DATE"), "dd-MMM, yyyy") & vbNewLine & "Amount : " & Format(_Temp_Balance, "#,0.##") & vbNewLine & "==========================================" & vbNewLine & "For more details: Check Daily Balances Report"
                        Exit For
                    End If
                Next
                If Negative_MsgStr.Trim.Length > 0 Then Exit Sub
            Next
        End If
        xPleaseWait.Hide()
    End Sub

    Private Sub Show_Summary_Balances()
        Me.BandedGridView1.OptionsView.ShowFooter = True

        Me.BandedGridView1.Columns("iTR_REC_CASH").Summary.Clear() : Me.BandedGridView1.Columns("iTR_REC_CASH").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.BandedGridView1.Columns("iTR_REC_BANK").Summary.Clear() : Me.BandedGridView1.Columns("iTR_REC_BANK").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.BandedGridView1.Columns("iTR_REC_JOURNAL").Summary.Clear() : Me.BandedGridView1.Columns("iTR_REC_JOURNAL").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        Me.BandedGridView1.Columns("iTR_REC_TOTAL").Summary.Clear() : Me.BandedGridView1.Columns("iTR_REC_TOTAL").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")

        If REC_BANK01.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK01").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK01").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK02.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK02").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK02").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK03.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK03").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK03").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK04.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK04").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK04").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK05.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK05").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK05").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK06.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK06").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK06").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK07.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK07").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK07").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK08.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK08").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK08").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK09.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK09").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK09").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If REC_BANK10.TAG = "YES" Then Me.BandedGridView1.Columns("REC_BANK10").Summary.Clear() : Me.BandedGridView1.Columns("REC_BANK10").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")

        If PAY_BANK01.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK01").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK01").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK01").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal01, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK01").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK01", "{0:#,0.00}")
            End If
        End If

        If PAY_BANK02.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK02").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK02").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK02").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal02, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK02").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK02", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK03.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK03").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK03").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK03").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal03, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK03").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK03", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK04.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK04").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK04").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK04").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal04, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK04").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK04", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK05.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK05").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK05").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK05").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal05, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK05").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK05", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK06.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK06").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK06").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK06").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal06, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK06").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK06", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK07.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK07").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK07").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK07").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal07, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK07").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK07", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK08.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK08").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK08").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK08").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal08, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK08").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK08", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK09.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK09").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK09").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK09").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal09, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK09").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK09", "{0:#,0.00}")
            End If
        End If
        If PAY_BANK10.TAG = "YES" Then
            Me.BandedGridView1.Columns("PAY_BANK10").Summary.Clear()
            Me.BandedGridView1.Columns("PAY_BANK10").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
            If Summary_Column_Status Then
                Me.BandedGridView1.Columns("PAY_BANK10").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal10, "#,0.00"))
                Me.BandedGridView1.Columns("PAY_BANK10").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REC_BANK10", "{0:#,0.00}")
            End If
        End If
        Me.BandedGridView1.Columns("iTR_DATE").Summary.Clear()
        Me.BandedGridView1.Columns("iTR_DATE").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Total :{0:0}")

        Me.BandedGridView1.Columns("iTR_PARTY_1").Summary.Clear()
        If Summary_Column_Status Then
            Me.BandedGridView1.Columns("iTR_PARTY_1").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Total Payments :{0:0}")
            Me.BandedGridView1.Columns("iTR_PARTY_1").Summary.Add.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Closing Balance :{0:0}")
            Me.BandedGridView1.Columns("iTR_PARTY_1").Summary.Add.SetSummary(DevExpress.Data.SummaryItemType.Custom, "Total Receipts :{0:0}")
        End If

        Me.BandedGridView1.Columns("iTR_PAY_CASH").Summary.Clear()
        Me.BandedGridView1.Columns("iTR_PAY_CASH").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If Summary_Column_Status Then
            Me.BandedGridView1.Columns("iTR_PAY_CASH").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Cash_Bal, "#,0.00"))
            Me.BandedGridView1.Columns("iTR_PAY_CASH").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "iTR_REC_CASH", "{0:#,0.00}")
        End If

        Me.BandedGridView1.Columns("iTR_PAY_BANK").Summary.Clear()
        Me.BandedGridView1.Columns("iTR_PAY_BANK").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If Summary_Column_Status Then
            Me.BandedGridView1.Columns("iTR_PAY_BANK").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", Format(Close_Bank_Bal, "#,0.00"))
            Me.BandedGridView1.Columns("iTR_PAY_BANK").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "iTR_REC_BANK", "{0:#,0.00}")
        End If

        Me.BandedGridView1.Columns("iTR_PAY_JOURNAL").Summary.Clear()
        Me.BandedGridView1.Columns("iTR_PAY_JOURNAL").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If Summary_Column_Status Then
            Me.BandedGridView1.Columns("iTR_PAY_JOURNAL").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", "0.00")
            Me.BandedGridView1.Columns("iTR_PAY_JOURNAL").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "iTR_REC_JOURNAL", "{0:#,0.00}")
        End If

        Me.BandedGridView1.Columns("iTR_PAY_TOTAL").Summary.Clear()
        Me.BandedGridView1.Columns("iTR_PAY_TOTAL").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, "{0:#,0.00}")
        If Summary_Column_Status Then
            Me.BandedGridView1.Columns("iTR_PAY_TOTAL").Summary.Add(DevExpress.Data.SummaryItemType.Custom, "", "")
            Me.BandedGridView1.Columns("iTR_PAY_TOTAL").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "iTR_REC_TOTAL", "{0:#,0.00}")
        End If
        '
        'Get Summary Total Value
        'BandedGridView1.Columns("iTR_REC_BANK").SummaryItem.SummaryValue


    End Sub

    Private Sub BandedGridView1_CellMerge(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles BandedGridView1.CellMerge
        If Not e.Column Is BandedGridView1.Columns("iTR_DATE") And _
            Not e.Column Is BandedGridView1.Columns("iTR_VNO") And _
            Not e.Column Is BandedGridView1.Columns("iTR_REF_NO") And _
            Not e.Column Is BandedGridView1.Columns("iTR_PARTY_1") Then
            e.Merge = False : e.Handled = True : Exit Sub
        End If

        Dim _Ref_No1 As Double = Val(BandedGridView1.GetRowCellValue(e.RowHandle1, BandedGridView1.Columns("iTR_REF_NO")))
        Dim _Ref_No2 As Double = Val(BandedGridView1.GetRowCellValue(e.RowHandle2, BandedGridView1.Columns("iTR_REF_NO")))

        If e.Column Is BandedGridView1.Columns("iTR_DATE") Then
            Dim _Date1 As Date = Convert.ToDateTime(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column))
            Dim _Date2 As Date = Convert.ToDateTime(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column))
            If _Date1 = _Date2 And _Ref_No1 = _Ref_No2 Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
        End If
        If e.Column Is BandedGridView1.Columns("iTR_VNO") Then
            Dim _Vno1 As String = "" : If Not IsDBNull(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column)) Then _Vno1 = BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column)
            Dim _Vno2 As String = "" : If Not IsDBNull(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column)) Then _Vno2 = BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column)
            If _Vno1 = _Vno2 And _Ref_No1 = _Ref_No2 Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
        End If
        If e.Column Is BandedGridView1.Columns("iTR_PARTY_1") Then
            Dim _Party1 As String = "" : If Not IsDBNull(BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column)) Then _Party1 = BandedGridView1.GetRowCellValue(e.RowHandle1, e.Column)
            Dim _Party2 As String = "" : If Not IsDBNull(BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column)) Then _Party2 = BandedGridView1.GetRowCellValue(e.RowHandle2, e.Column)
            If _Party1 = _Party2 And _Ref_No1 = _Ref_No2 Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
        End If
        If e.Column Is BandedGridView1.Columns("iTR_REF_NO") Then
            If _Ref_No1 = _Ref_No2 Then : e.Merge = True : e.Handled = True : Else : e.Merge = False : e.Handled = True : End If
        End If
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        '----------------------------------------
        If Base.AllowMultiuser() Then
            If Action = "MATCH TRANSFERS" Or Action = "UNMATCH TRANSFERS" Or Action = "VOUCHER" Or Action = "PRINT-LIST" Or Action = "LOCKED" Or Action = "UNLOCKED" Then
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    If Not xTemp_ID.ToString.ToLower = "opening balance" And Not xTemp_ID.ToString.ToLower = "note-book" Then
                        Dim RecEdit_Date As Object = Base._Voucher_DBOps.GetEditOnByRecID(xTemp_ID)
                        If IsDBNull(RecEdit_Date) Or RecEdit_Date Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Grid_Display()
                            Exit Sub
                        End If
                        If RecEdit_Date <> Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Grid_Display()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
        '----------------------------------------
        Select Case Action
            Case "NEW"
                '
                Dim ExitFlag As Integer = 1
                While ExitFlag <> 0
                    Dim xfrm As New Frm_Voucher_Type
                    xfrm.Text = "New ~ " & Me.Text : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        Dim Voucher_Type As String = xfrm.Voucher_Type
                        Dim Selection_By_Item As Boolean = xfrm.Selection_By_Item
                        Dim Selected_Item_ID As String = xfrm.GLookUp_ItemList.Tag
                        Dim Flag As Boolean = False : Dim xEntryDate As Date = Nothing
                        xfrm.Dispose()
                        If Voucher_Type = "CASH" Then
                            Dim zfrm As New Frm_Voucher_Win_Cash : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "BANK" Then
                            Dim zfrm As New Frm_Voucher_Win_B2B : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "DONATION - REGULAR" Then
                            Dim zfrm As New Frm_Voucher_Win_Donation_R : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "DONATION - FOREIGN" Then
                            Dim zfrm As New Frm_Voucher_Win_Donation_F : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "COLLECTION BOX" Then
                            If Base._open_Ins_ID = "00001" Or Base._open_Ins_ID = "00005" Then 'bk, trst 
                                Dim zfrm As New Frm_Voucher_Win_C_Box : zfrm.MainBase = Base
                                If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                                zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                                If Not zfrm Is Nothing Then zfrm.Dispose()
                            Else
                                DevExpress.XtraEditors.XtraMessageBox.Show("In " & Base._open_Ins_Name & vbNewLine & vbNewLine & "C o l l e c t i o n   B o x   E n t r y   N o t   A p p l i c a b l e . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        If Voucher_Type = "PAYMENT" Or Voucher_Type = "PAYMENT - INSTITUTE" Then
                            Dim zfrm As New Frm_Voucher_Win_Gen_Pay : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "RECEIPTS" Or Voucher_Type = "RECEIPTS - INSTITUTE" Then
                            Dim zfrm As New Frm_Voucher_Win_Gen_Rec : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "DONATION - GIFT" Then
                            Dim zfrm As New Frm_Voucher_Win_Gift : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "INTERNAL TRANSFER" Then
                            Dim zfrm As New Frm_Voucher_Win_I_Transfer : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID_1.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "FD" Then
                            Dim zfrm As New Frm_Voucher_Win_FD : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            If Not Selection_By_Item Then
                                Dim yFrm As New Frm_Voucher_Win_FD_Type : yFrm.MainBase = Base
                                yFrm.ShowDialog(Me)
                                If yFrm.DialogResult = Windows.Forms.DialogResult.OK Then
                                    zfrm.iSpecific_ItemID = yFrm.SelectedActivityID
                                    Select Case yFrm.SelectedActivityID
                                        Case "f6e4da62-821f-4961-9f93-f5177fca2a77" : zfrm.iAction = Common_Lib.Common.FDAction.New_FD : zfrm.TitleX.Text = "FD Creation"
                                        Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" : zfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : zfrm.TitleX.Text = "FD Renewal"
                                        Case "65730a27-e365-4195-853e-2f59225fe8f4" : zfrm.iAction = Common_Lib.Common.FDAction.Close_FD : zfrm.TitleX.Text = "FD Closure"
                                    End Select
                                Else
                                    Exit Sub
                                End If
                            Else
                                Select Case Selected_Item_ID
                                    Case "f6e4da62-821f-4961-9f93-f5177fca2a77"
                                        zfrm.iAction = Common_Lib.Common.FDAction.New_FD : zfrm.TitleX.Text = "FD Creation"
                                    Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b"
                                        zfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : zfrm.TitleX.Text = "FD Renewal"
                                    Case "1ed5cbe4-c8aa-4583-af44-eba3db08e117", "65730a27-e365-4195-853e-2f59225fe8f4"
                                        zfrm.iAction = Common_Lib.Common.FDAction.Close_FD : zfrm.TitleX.Text = "FD Closure" : zfrm.iSpecific_ItemID = "65730a27-e365-4195-853e-2f59225fe8f4"
                                End Select
                            End If
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "SALE OF ASSET" Then
                            Dim zfrm As New Frm_Voucher_Win_Sale_Asset : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "MEMBERSHIP" Then
                            Dim zfrm As New Frm_Voucher_Membership
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "MEMBERSHIP RENEWAL" Then
                            Dim zfrm As New Frm_Voucher_Membership_Renewal
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "JOURNAL" Then
                            Dim zfrm As New Frm_Voucher_Win_Journal : zfrm.MainBase = Base : zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Voucher_Type = "ASSET TRANSFER" Then
                            Dim zfrm As New Frm_Voucher_Win_Asset_Transfer : zfrm.MainBase = Base
                            If Selection_By_Item Then zfrm.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection : zfrm.iSpecific_ItemID = Selected_Item_ID Else zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                            zfrm.ShowDialog(Me) : If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then xID = zfrm.xID1.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                            If Not zfrm Is Nothing Then zfrm.Dispose()
                        End If
                        If Flag Then
                            If (xFr_Date <= xEntryDate.Date And xTo_Date >= xEntryDate.Date) Then
                                Grid_Display()
                            Else
                                Dim xMM As Integer = xEntryDate.Month : Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
                            End If

                            Dim xPromptWindow As New Common_Lib.Prompt_Window
                            If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "A d d   M o r e   E n t r y . . . ?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then ExitFlag = 0
                            xPromptWindow.Dispose()
                        Else
                            ExitFlag = 0
                        End If
                    Else
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                        ExitFlag = 0
                    End If
                End While

            Case "EDIT"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    Dim xTemp_MID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                    Dim xMaster_ID As String = "" : If xTemp_MID.Length > 0 Then xMaster_ID = xTemp_MID Else xMaster_ID = xTemp_ID
                    Dim isRecChanged As Boolean = False '#5518 fix
                    Dim Flag As Boolean = False : Dim xEntryDate As Date = Nothing
                    If xTemp_ID = "OPENING BALANCE" Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Opening Balance cannot Edit / Delete from Voucher Entry...!" & vbNewLine & vbNewLine & "Note: Use Profile - Cash A/c. Information for CASH" & vbNewLine & Space(31) & "or Bank A/c. Information for BANK", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If xTemp_ID = "NOTE-BOOK" Then
                        Dim xFrm As New Frm_NoteBook_Info : xFrm.MainBase = Base : xFrm.Text = "Note-Book Entry (" & Me.Text & ")"
                        xFrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xFrm.xEntryDate = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_DATE").ToString()
                        xFrm.ShowDialog(Me) : xFrm.Dispose()
                        xID = xTemp_ID : Flag = True : xEntryDate = xFrm.xEntryDate
                        If (xFr_Date <= xEntryDate.Date And xTo_Date >= xEntryDate.Date) Then
                        Else
                            Dim xMM As Integer = xEntryDate.Month : Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
                        End If
                        Grid_Display()
                        Exit Sub
                    End If

                    If xTemp_ID.Length > 0 Then
                        '
                        Dim Entry_Found As Boolean = False
                        Dim xRec_Status As String = "" : Dim xTR_CODE As String = "" : Dim xTemp_D_Status As String = "" : Dim xCross_Ref_Id As String = ""
                        Dim multiUserMsg As String = ""
                        Dim AllowUser As Boolean = False
                        Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID)
                        If Status Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Status.Rows.Count > 0 Then ' checks for record existence here 
                            Entry_Found = True
                            xRec_Status = Status.Rows(0)("REC_STATUS")
                            xTR_CODE = Status.Rows(0)("TR_CODE") 'fills trcode here , if record is present
                            For Each cRow As DataRow In Status.Rows
                                If Not IsDBNull(cRow("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = cRow("TR_TRF_CROSS_REF_ID")
                                If xCross_Ref_Id.Length > 0 Then Exit For
                            Next
                            'Status check for multiusers
                            Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iACTION_STATUS").ToString()
                            Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                            If value <> xRec_Status Then
                                If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                    multiUserMsg = vbNewLine & vbNewLine & "The Record has been locked in the background by another user."
                                ElseIf xRec_Status = Common_Lib.Common.Record_Status._Completed Then
                                    multiUserMsg = vbNewLine & vbNewLine & "T h e   R e c o r d   h a s   b e e n   u n l o c k e d   i n   t h e   b a c k g r o u n d   b y   a n o t h e r   u s e r."
                                    AllowUser = True
                                Else
                                    multiUserMsg = vbNewLine & vbNewLine & "Record Status has been changed in the background by another user"
                                    AllowUser = True
                                End If
                                If AllowUser Then
                                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                                    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", multiUserMsg & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                        xPromptWindow.Dispose()
                                    Else
                                        xPromptWindow.Dispose()
                                        Grid_Display()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                        'takes action if there is no record 
                        If Entry_Found = False Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   i n   b a c k g r o u n d  . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub

                        If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & multiUserMsg & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If

                        If Get_Closed_Bank_Status(xTemp_ID) Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & Closed_Bank_Account_No & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub

                        If xTR_CODE = 5 Or xTR_CODE = 6 Then 'uses trcode here 
                            Dim Status2 As DataTable = Base._Voucher_DBOps.GetDonationStatus(xTemp_ID)
                            If Status2 Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            If Status2.Rows.Count > 0 Then
                                xTemp_D_Status = Status2.Rows(0)("DS_STATUS")
                            End If
                        End If

                        Dim xVCode As Integer = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString()
                        Dim xItemID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_ITEM_ID").ToString()
                        Select Case xVCode
                            Case Common_Lib.Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn
                                Dim xfrm As New Frm_Voucher_Win_Cash : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Bank_To_Bank_Transfer
                                Dim xfrm As New Frm_Voucher_Win_B2B : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Regular
                                If (xTemp_D_Status <> "") AndAlso (xTemp_D_Status <> "42189485-9b6b-430a-8112-0e8882596f3c") AndAlso (xTemp_D_Status <> "3a99fadc-b336-480d-8116-fbd144bd7671") AndAlso (xTemp_D_Status <> "6a7c38ba-5779-4e21-acc7-c1829b7ec933") Then '--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   c a n n o t   b e   E d i t   /   D e l e t e . . . !" & vbNewLine & vbNewLine & "Note: Donation Status Changed, Check Donation Register..!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                Dim xfrm As New Frm_Voucher_Win_Donation_R : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Foreign
                                If (xTemp_D_Status <> "") AndAlso (xTemp_D_Status <> "42189485-9b6b-430a-8112-0e8882596f3c") AndAlso (xTemp_D_Status <> "3a99fadc-b336-480d-8116-fbd144bd7671") AndAlso (xTemp_D_Status <> "6a7c38ba-5779-4e21-acc7-c1829b7ec933") Then '--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   c a n n o t   b e   E d i t   /   D e l e t e . . . !" & vbNewLine & vbNewLine & "Note: Donation Status Changed, Check Donation Register..!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                Dim xfrm As New Frm_Voucher_Win_Donation_F : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()

                            Case Common_Lib.Common.Voucher_Screen_Code.Collection_Box
                                Dim xfrm As New Frm_Voucher_Win_C_Box : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Payment
                                'Liabilities Raised
                                Dim xTemp_LiabID As String = Base._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID) 'Get Liab creted by current Txn
                                If xTemp_LiabID.Length > 0 Then
                                    Dim txnReferLiab As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID) 'Payment has been made againt the liability raised
                                    If Not txnReferLiab Is Nothing Then
                                        If txnReferLiab.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some payment has been made against the liability raised in current transaction on " & Convert.ToDateTime(txnReferLiab.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferLiab.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_LiabID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID 'Gets references in next live year too 
                                    txnReferLiab = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferLiab Is Nothing Then
                                        If txnReferLiab.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   L i a b i l i t y   c r e a t e d   b y   c u r r e n t   p a y m e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                'Deposits Raised
                                Dim xTemp_DepID As String = Base._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID) 'Get Liab creted by current Txn
                                If xTemp_DepID.Length > 0 Then
                                    Dim txnReferDeposits As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID) 'Adjustments/ Refund has been made againt the deposit raised
                                    If Not txnReferDeposits Is Nothing Then
                                        If txnReferDeposits.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " & Convert.ToDateTime(txnReferDeposits.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferDeposits.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_DepID
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID 'Gets references in next live year too 
                                    txnReferDeposits = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferDeposits Is Nothing Then
                                        If txnReferDeposits.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   D e p o s i t   c r e a t e d   b y   c u r r e n t   p a y m e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                'Advance Raised
                                Dim xTemp_AdvID As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID) 'Get Advances creted by current Txn
                                If xTemp_AdvID.Length > 0 Then
                                    Dim txnReferAdvance As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID) 'Adjustments/ Refund has been made againt the Advance raised
                                    If Not txnReferAdvance Is Nothing Then
                                        If txnReferAdvance.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " & Convert.ToDateTime(txnReferAdvance.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferAdvance.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_AdvID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID 'Gets references in next live year too 
                                    txnReferAdvance = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferAdvance Is Nothing Then
                                        If txnReferAdvance.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   A d v a n c e   c r e a t e d   b y   c u r r e n t   p a y m e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(xTemp_MID).Rows ' Get Actual Item IDs from Selected Transaction
                                    Dim xTemp_ItemID As String = cRow(0).ToString()
                                    Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                    Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                                    If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                                        Dim xTemp_AssetID As String = ""
                                        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                            Case "GOLD", "SILVER"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_MID)
                                            Case "OTHER ASSETS"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, xTemp_MID)
                                            Case "LIVESTOCK"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, xTemp_MID)
                                            Case "VEHICLES"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_MID)
                                            Case "LAND & BUILDING"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, xTemp_MID)
                                        End Select
                                        If xTemp_AssetID.Length > 0 Then
                                            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                            If Not SaleRecord Is Nothing Then
                                                If SaleRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                            Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                            If Not ReferenceRecord Is Nothing Then
                                                If ReferenceRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'Bug #5339 fix
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Else 'Non Profile Entries 
                                        Dim RefID As String = Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)
                                        If Not RefID Is Nothing Then
                                            If RefID.Length > 0 Then
                                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(RefID) 'checks if the referred property for constt items has been sold 
                                                If Not SaleRecord Is Nothing Then
                                                    If SaleRecord.Rows.Count > 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If

                                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, RefID) 'checks if the referred property for constt items has been transferred 'Bug #5339 fix
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    If AssetTrfRecord.Rows.Count > 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next

                                Dim xfrm As New Frm_Voucher_Win_Gen_Pay : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xMID = xfrm.xMID.Text : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Receipt
                                Dim FinalPayDate As Object = Base._DepositsDBOps.GetFinalPaymentDate(xCross_Ref_Id, xTemp_MID)
                                If IsDate(FinalPayDate) Then
                                    If FinalPayDate <> DateTime.MinValue Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " & FinalPayDate.ToLongDateString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Gen_Rec : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
                                For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(xTemp_MID).Rows ' Get Actual Asset IDs from Selected Transaction
                                    Dim xTemp_ItemID As String = cRow(0).ToString()
                                    Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                    Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                                    If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                                        Dim xTemp_AssetID As String = ""
                                        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                            Case "GOLD", "SILVER"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_MID)
                                            Case "OTHER ASSETS"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, xTemp_MID)
                                            Case "LIVESTOCK"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, xTemp_MID)
                                            Case "VEHICLES"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_MID)
                                            Case "LAND & BUILDING"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, xTemp_MID)
                                        End Select
                                        If xTemp_AssetID.Length > 0 Then
                                            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                            If Not SaleRecord Is Nothing Then
                                                If SaleRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If

                                            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'Bug #5339 fix
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                            Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID)
                                            If Not ReferenceRecord Is Nothing Then
                                                If ReferenceRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Else 'Non Profile Entries 
                                        Dim RefID As String = Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)
                                        If Not RefID Is Nothing Then
                                            If RefID.Length > 0 Then
                                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(RefID) 'checks if the referred property for constt items has been sold 
                                                If Not SaleRecord Is Nothing Then
                                                    If SaleRecord.Rows.Count > 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, RefID) 'checks if the referred property for constt items has been sold 'Bug #5339 fix
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    If AssetTrfRecord.Rows.Count > 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next

                                Dim xfrm As New Frm_Voucher_Win_Gift : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
                                Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                                If Not d1 Is Nothing Then
                                    If d1.Rows.Count > 0 Then
                                        If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> d1.Rows(0)("REC_EDIT_ON") Then
                                            isRecChanged = True
                                        End If
                                        If Not IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                            If d1.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                                multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                                DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   I n t e r n a l   T r a n s f e r   c a n n o t   b e   E d i t e d . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                If isRecChanged Then Grid_Display()
                                                Exit Sub
                                            End If
                                        Else
                                            If isRecChanged Then '#5518 fix
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display()
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_I_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID_1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID_1.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If xfrm.DialogResult = Windows.Forms.DialogResult.Cancel And isRecChanged Then Grid_Display() '#5518 fix
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                                Dim xfrm As New Frm_Voucher_Win_FD : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                xfrm.xMID.Text = xTemp_MID
                                Dim IsClosed As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(xTemp_MID) ' checks if the fd is closed
                                'CHECK IF THE CURREMT FD HAS ANY TDS OR INTEREST AGAINST IT, OTHER THAN IN CURRENT TRANSACTION 
                                Dim Has_Int_TDS As Object = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, xCross_Ref_Id, 1) 'checks if any interest or tds has been posted against the FD 

                                'CHECKS IF CURRENT TRANSACTIONS IS A RENEWAL ONE, IF YES THEN GET THE ID OF NEWLY CREATED FD
                                Dim IsRenew As Object = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 2)
                                Dim New_FD As Object = Nothing
                                If IsRenew > 0 Then ' And xItemID <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" 'Bug #5547 fix
                                    New_FD = Base._FD_Voucher_DBOps.GetNewFDIdFromClosed(xCross_Ref_Id)
                                    If Not IsDBNull(New_FD) Then Has_Int_TDS = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, New_FD, 1)
                                End If

                                Select Case xItemID
                                    Case "f6e4da62-821f-4961-9f93-f5177fca2a77" 'FD New
                                        If IsDate(IsClosed) Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("F D  A l r e a d y  C l o s e d / R e n ew e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf Has_Int_TDS > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t / T D S  a l r e a d y  e n t e r e d  a g a i n s t  F D!" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.New_FD : xfrm.TitleX.Text = "FD Creation" : xfrm.CreatedFDID = xCross_Ref_Id
                                        End If
                                    Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" 'FD Renewed
                                        If IsDate(IsClosed) Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n ew e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf Has_Int_TDS > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = xCross_Ref_Id ' New_FD ' Bug #5315
                                        End If
                                    Case "1ed5cbe4-c8aa-4583-af44-eba3db08e117", "65730a27-e365-4195-853e-2f59225fe8f4" 'FD Close - Interest, 65730a27-e365-4195-853e-2f59225fe8f4
                                        If IsRenew > 0 Then
                                            If IsDate(IsClosed) Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n e w e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            ElseIf Has_Int_TDS > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = New_FD
                                            End If
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.Close_FD : xfrm.TitleX.Text = "FD Closure"
                                        End If

                                    Case Else
                                        If IsRenew > 0 Then
                                            If IsDate(IsClosed) Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n e w e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            ElseIf Has_Int_TDS > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = New_FD
                                            End If
                                        Else
                                            If Base._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 3) > 0 Then ' fdclose principle
                                                xfrm.iAction = Common_Lib.Common.FDAction.Close_FD : xfrm.TitleX.Text = "FD Closure"
                                            Else
                                                ' If xItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4" Then 'Excess interest recovered by bank on FD
                                                xfrm.iSpecific_ItemID = xItemID
                                                '    Else
                                                '    If IsDate(Base._FD_Voucher_DBOps.GetFDCloseDateByFdID(xCross_Ref_Id)) Then
                                                '        DevExpress.XtraEditors.XtraMessageBox.Show("C o n c e r n e d  F D  h a s  b e e n  C l o s e d / R e n e w e d !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this Entry.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                '        Exit Sub
                                                '    End If
                                                '    xfrm.iSpecific_ItemID = xItemID
                                                'End If
                                            End If
                                        End If
                                End Select
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                                Dim xTemp_AdvID As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID) 'Get advance cretaed by current Txn
                                If xTemp_AdvID.Length > 0 Then
                                    Dim xTemp_RefRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AdvID) 'Advance is referred in a transaction
                                    If Not xTemp_RefRecord Is Nothing Then
                                        If xTemp_RefRecord.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some Refunds were made on " & Convert.ToDateTime(xTemp_RefRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & xTemp_RefRecord.Rows(0)("TR_AMOUNT").ToString() & " for the Sale amount Receivable raised by current entry." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Sale_Asset : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True

                                Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id)
                                If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5608 point2 fix
                                    Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                    Param.CrossRefId = xCross_Ref_Id
                                    Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count
                                    If Adj_NextYear > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Some adjustments have already been made on this asset" & vbNewLine & vbNewLine & " Please delete those entries for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    xfrm.Info_MaxEditedOn = MaxEditOn
                                End If


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If


                                Dim xfrm As New Frm_Voucher_Membership : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                                Dim xfrm As New Frm_Voucher_Membership_Renewal : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me)
                                If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If


                                Dim xfrm As New Frm_Voucher_Membership_Conversion : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Journal
                                Dim UseCount As Object = 0
                                Dim RefId As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._AdvanceDBOps.GetAdvancePaymentCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                RefId = Base._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._DepositsDBOps.GetTransactionCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                RefId = Base._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._LiabilityDBOps.GetTransactionCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Journal : xfrm.MainBase = Base
                                xfrm.xMID.Text = xMaster_ID : xfrm.xID.Text = xTemp_ID : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit

                                'RefId = Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)
                                'Bug #5085 fixed

                                For Each Row As DataRow In Base._Voucher_DBOps.GetRefRecordIDS(xTemp_MID).Rows
                                    If Row("ITEM_PROFILE") = "GOLD" Or Row("ITEM_PROFILE") = "SILVER" Or Row("ITEM_PROFILE") = "OTHER ASSETS" Or Row("ITEM_PROFILE") = "LIVESTOCK" Or Row("ITEM_PROFILE") = "LAND & BUILDING" Or Row("ITEM_PROFILE") = "VEHICLES" Then
                                        If Not IsDBNull(Row("TR_TRF_CROSS_REF_ID")) Then
                                            RefId = Row("TR_TRF_CROSS_REF_ID")
                                            If Not RefId Is Nothing Then
                                                If RefId.Length > 0 Then
                                                    Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(RefId)
                                                    If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5608 point2 fix
                                                        Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(RefId, True) 'bug #5566 fix, exclude_prev_year made true to allow editing jv if the asset was partially sold in previous year
                                                        If Not SaleRecord Is Nothing Then
                                                            If SaleRecord.Rows.Count > 0 Then
                                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End If
                                                        'checks if the referred property for constt items has been transferred 'Bug #5339 fix 
                                                        Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Base._open_Year_ID, RefId, True) 'exclude_prev_year made true to allow deleting jv if the asset was partially transferred in previous year
                                                        If AssetTrfRecord.Rows.Count > 0 Then
                                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End If
                                                        '#5589 fix
                                                        'If Base._next_Unaudited_YearID.Length > 0 Then
                                                        Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                                        Param.CrossRefId = RefId
                                                        Param.YearID = Base._open_Year_ID 'Bug #5687
                                                        Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                                        Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                                        Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal).Rows.Count
                                                        If Adj_NextYear > 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Selected Entry contains an asset against which adjustments have been made in current / next year" & vbNewLine & vbNewLine & " Please delete those entries for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                        'End If
                                                    Else
                                                        xfrm.Info_MaxEditedOn.Add(RefId, MaxEditOn) 'Bug #5695 fix
                                                    End If
                                                End If
                                            End If
                                        End If
                                    ElseIf Row("ITEM_PROFILE") = "OTHER DEPOSITS" Then 'Bug #5683
                                        If Not IsDBNull(Row("TR_TRF_CROSS_REF_ID")) Then
                                            RefId = Row("TR_TRF_CROSS_REF_ID")
                                            If Not RefId Is Nothing Then
                                                If RefId.Length > 0 Then
                                                    Dim FinalPayDate As Object = Base._DepositsDBOps.GetFinalPaymentDate(RefId)
                                                    If IsDate(FinalPayDate) And FinalPayDate <> DateTime.MinValue Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " & FinalPayDate.ToLongDateString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If Not xfrm Is Nothing Then xfrm.Dispose()

                            Case Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                                Dim Rec As DataTable = Base._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1)
                                If Not Rec Is Nothing Then
                                    If Rec.Rows.Count > 0 Then
                                        If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> Rec.Rows(0)("REC_EDIT_ON") Then
                                            isRecChanged = True
                                        End If
                                        If Not IsDBNull(Rec.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                            If Rec.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                                multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                                DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   A s s e t   T r a n s f e r   c a n n o t   b e   E d i t e d. . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                If isRecChanged Then Grid_Display()
                                                Exit Sub
                                            End If
                                        Else
                                            If isRecChanged Then '#5518 fix
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                Grid_Display() : Exit Sub
                                            End If
                                        End If
                                    End If
                                End If


                                Dim _RowHandle As Integer = 0
                                If Val(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_SR_NO").ToString()) = 2 Then
                                    _RowHandle = 1
                                End If
                                If (Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle - _RowHandle, "iTR_TYPE").ToString()) = "CREDIT" Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("A s s e t   T r a n s f e r   F r o m   E n t r y   c a n n o t   b e   E d i t e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Asset_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True

                                'Checks for any jv adjustments made on the asset 
                                If (Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle - _RowHandle, "iTR_TYPE").ToString()) = "DEBIT" Then
                                    Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetRecord(xTemp_MID, 2)
                                    Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(d1.Rows(0)("TR_REF_OTHERS"))
                                    If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5610 point2 fix
                                        Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                        Param.CrossRefId = d1.Rows(0)("TR_REF_OTHERS")
                                        Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                        Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                        Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count
                                        If Adj_NextYear > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Some adjustments have already been made on this asset" & vbNewLine & vbNewLine & " Please delete those entries for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        xfrm.Info_MaxEditedOn = MaxEditOn
                                    End If
                                End If


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID1.Text : xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                                If xfrm.DialogResult = Windows.Forms.DialogResult.Cancel And isRecChanged Then Grid_Display() '#5518 fix
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                        End Select
                        If Flag Then
                            If (xFr_Date <= xEntryDate.Date And xTo_Date >= xEntryDate.Date) Then
                                Grid_Display() 'standard function to refresh grid everywhere
                            Else
                                Dim xMM As Integer = xEntryDate.Month : Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
                            End If
                        End If

                    End If
                End If

            Case "DELETE"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    Dim xTemp_MID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                    Dim xMaster_ID As String = "" : If xTemp_MID.Length > 0 Then xMaster_ID = xTemp_MID Else xMaster_ID = xTemp_ID
                    Dim isRecChanged As Boolean = False '#5518 fix
                    If xTemp_ID = "OPENING BALANCE" Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Opening Balance cannot Edit / Delete from Voucher Entry...!" & vbNewLine & vbNewLine & "Note: Use Profile - Cash A/c. Information for CASH" & vbNewLine & Space(31) & "or Bank A/c. Information for BANK", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If xTemp_ID = "NOTE-BOOK" Then
                        Dim xFrm As New Frm_NoteBook_Info : xFrm.MainBase = Base : xFrm.Text = "Note-Book Entry (" & Me.Text & ")"
                        xFrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xFrm.xEntryDate = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_DATE").ToString()
                        xFrm.ShowDialog(Me) : xFrm.Dispose()
                        xID = xTemp_ID : Grid_Display()
                        Exit Sub
                    End If

                    If xTemp_ID.Length > 0 Then
                        ' 
                        Dim Entry_Found As Boolean = False
                        Dim xRec_Status As String = "" : Dim xTR_CODE As String = "" : Dim xTemp_D_Status As String = "" : Dim xCross_Ref_Id As String = ""
                        Dim multiUserMsg As String = ""
                        Dim AllowUser As Boolean = False
                        Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID)
                        If Status Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Status.Rows.Count > 0 Then
                            Entry_Found = True
                            xRec_Status = Status.Rows(0)("REC_STATUS")
                            xTR_CODE = Status.Rows(0)("TR_CODE")
                            For Each cRow As DataRow In Status.Rows
                                If Not IsDBNull(cRow("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = cRow("TR_TRF_CROSS_REF_ID")
                                If xCross_Ref_Id.Length > 0 Then Exit For
                            Next

                            'Status check for multiusers
                            Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iACTION_STATUS").ToString()
                            Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                            If value <> xRec_Status Then
                                If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                    multiUserMsg = vbNewLine & vbNewLine & "The Record has been locked in the background by another user."
                                ElseIf xRec_Status = Common_Lib.Common.Record_Status._Completed Then
                                    multiUserMsg = vbNewLine & vbNewLine & "T h e   R e c o r d   h a s   b e e n   u n l o c k e d   i n   t h e   b a c k g r o u n d   b y   a n o t h e r   u s e r."
                                    AllowUser = True
                                Else
                                    multiUserMsg = vbNewLine & vbNewLine & "Record Status has been changed in the background by another user"
                                    AllowUser = True
                                End If
                                If AllowUser Then
                                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                                    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", multiUserMsg & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                        xPromptWindow.Dispose()
                                    Else
                                        xPromptWindow.Dispose()
                                        Grid_Display()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If

                        If Entry_Found = False Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   i n   b a c k g r o u n d  . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                        If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & multiUserMsg & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If

                        If Get_Closed_Bank_Status(xTemp_ID) Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & Closed_Bank_Account_No & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub

                        If xTR_CODE = 5 Or xTR_CODE = 6 Then
                            Dim Status2 As DataTable = Base._Voucher_DBOps.GetDonationStatus(xTemp_ID)
                            If Status2 Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            If Status2.Rows.Count > 0 Then
                                xTemp_D_Status = Status2.Rows(0)("DS_STATUS")
                            End If
                        End If


                        Dim xVCode As Integer = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString()
                        Dim xItemID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_ITEM_ID").ToString()
                        Select Case xVCode
                            Case Common_Lib.Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn
                                Dim xfrm As New Frm_Voucher_Win_Cash : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Bank_To_Bank_Transfer
                                Dim xfrm As New Frm_Voucher_Win_B2B : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Regular
                                If (xTemp_D_Status <> "") AndAlso (xTemp_D_Status <> "42189485-9b6b-430a-8112-0e8882596f3c") AndAlso (xTemp_D_Status <> "3a99fadc-b336-480d-8116-fbd144bd7671") AndAlso (xTemp_D_Status <> "6a7c38ba-5779-4e21-acc7-c1829b7ec933") Then '--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   c a n n o t   b e   E d i t e d /   D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note: Donation Status Changed, Check Donation Register..!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                Dim xfrm As New Frm_Voucher_Win_Donation_R : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Foreign
                                If (xTemp_D_Status <> "") AndAlso (xTemp_D_Status <> "42189485-9b6b-430a-8112-0e8882596f3c") AndAlso (xTemp_D_Status <> "3a99fadc-b336-480d-8116-fbd144bd7671") AndAlso (xTemp_D_Status <> "6a7c38ba-5779-4e21-acc7-c1829b7ec933") Then '--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   c a n n o t   b e   E d i t e d  /   D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note: Donation Status Changed, Check Donation Register..!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                Dim xfrm As New Frm_Voucher_Win_Donation_F : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Collection_Box
                                Dim xfrm As New Frm_Voucher_Win_C_Box : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Payment
                                'Liabilities Raised
                                Dim xTemp_LiabID As String = Base._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID) 'Get Liab creted by current Txn
                                If xTemp_LiabID.Length > 0 Then
                                    Dim txnReferLiab As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID) 'Payment has been made againt the liability raised
                                    If Not txnReferLiab Is Nothing Then
                                        If txnReferLiab.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some payment has been made against the liability raised in current transaction on " & Convert.ToDateTime(txnReferLiab.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferLiab.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_LiabID
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    txnReferLiab = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferLiab Is Nothing Then
                                        If txnReferLiab.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   L i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to delete this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                'Deposits Raised
                                Dim xTemp_DepID As String = Base._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID) 'Get Liab creted by current Txn
                                If xTemp_DepID.Length > 0 Then
                                    Dim txnReferDeposits As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID) 'Adjustments/ Refund has been made againt the deposit raised
                                    If Not txnReferDeposits Is Nothing Then
                                        If txnReferDeposits.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " & Convert.ToDateTime(txnReferDeposits.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferDeposits.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_DepID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    txnReferDeposits = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferDeposits Is Nothing Then
                                        If txnReferDeposits.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   D e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to delete this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                'Advance Raised
                                Dim xTemp_AdvID As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID) 'Get Advances creted by current Txn
                                If xTemp_AdvID.Length > 0 Then
                                    Dim txnReferAdvance As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID) 'Adjustments/ Refund has been made againt the Advance raised
                                    If Not txnReferAdvance Is Nothing Then
                                        If txnReferAdvance.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " & Convert.ToDateTime(txnReferAdvance.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferAdvance.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = xTemp_AdvID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    txnReferAdvance = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                                    If Not txnReferAdvance Is Nothing Then
                                        If txnReferAdvance.Rows.Count > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!   A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to delete this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If
                                Dim IsInsuaranceApplicable As Boolean = False
                                For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(xTemp_MID).Rows ' Get Actual Item IDs from Selected Transaction
                                    Dim xTemp_ItemID As String = cRow(0).ToString()
                                    Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                    Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                                    If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                                        Dim xTemp_AssetID As String = ""
                                        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                            Case "GOLD", "SILVER"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_MID)
                                            Case "OTHER ASSETS"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, xTemp_MID) : IsInsuaranceApplicable = True
                                            Case "LIVESTOCK"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, xTemp_MID)
                                            Case "VEHICLES"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_MID)
                                            Case "LAND & BUILDING"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, xTemp_MID) : IsInsuaranceApplicable = True
                                        End Select
                                        If xTemp_AssetID.Length > 0 Then

                                            If IsInsuaranceApplicable Then
                                                If Base.IsInsuranceAudited() Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                                                End If
                                            End If

                                            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                            If Not SaleRecord Is Nothing Then
                                                If SaleRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'Bug #5339 Fix
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                            Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                            If Not ReferenceRecord Is Nothing Then
                                                If ReferenceRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Else 'Non Profile Entries 
                                        Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)) 'checks if the referred property for constt items has been sold 
                                        If Not SaleRecord Is Nothing Then
                                            If SaleRecord.Rows.Count > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                        Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)) 'checks if the referred property for constt items has been transferred 'Bug #5339Fix
                                        If AssetTrfRecord.Rows.Count > 0 Then
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Next
                                Dim xfrm As New Frm_Voucher_Win_Gen_Pay : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Receipt
                                Dim FinalPayDate As Object = Base._DepositsDBOps.GetFinalPaymentDate(xCross_Ref_Id, xTemp_MID)
                                If IsDate(FinalPayDate) Then
                                    If FinalPayDate <> DateTime.MinValue Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " & FinalPayDate.ToLongDateString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Gen_Rec : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
                                Dim IsInsuaranceApplicable As Boolean = False
                                For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(xTemp_MID).Rows ' Get Actual Item IDs from Selected Transaction
                                    Dim xTemp_ItemID As String = cRow(0).ToString()
                                    Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                    Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                                    If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                                        Dim xTemp_AssetID As String = ""
                                        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                            Case "GOLD", "SILVER"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_MID)
                                            Case "OTHER ASSETS"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, xTemp_MID) : IsInsuaranceApplicable = True
                                            Case "LIVESTOCK"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, xTemp_MID)
                                            Case "VEHICLES"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_MID)
                                            Case "LAND & BUILDING"
                                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, xTemp_MID) : IsInsuaranceApplicable = True
                                        End Select
                                        If xTemp_AssetID.Length > 0 Then
                                            If IsInsuaranceApplicable Then
                                                If Base.IsInsuranceAudited() Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                                                End If
                                            End If

                                            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                            If Not SaleRecord Is Nothing Then
                                                If SaleRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                            Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID)
                                            If Not ReferenceRecord Is Nothing Then
                                                If ReferenceRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'Bug #5339 fix
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                If AssetTrfRecord.Rows.Count > 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Else 'Non Profile Entries 
                                        Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)) 'checks if the referred property for constt items has been sold 
                                        If Not SaleRecord Is Nothing Then
                                            If SaleRecord.Rows.Count > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                        Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, Base._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)) 'checks if the referred property for constt items has been transferred 'Bug #5339 fix
                                        If AssetTrfRecord.Rows.Count > 0 Then
                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Next
                                Dim xfrm As New Frm_Voucher_Win_Gift : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
                                Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                                If Not d1 Is Nothing Then
                                    If d1.Rows.Count > 0 Then
                                        If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> d1.Rows(0)("REC_EDIT_ON") Then
                                            isRecChanged = True
                                        End If
                                        If Not IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                            If d1.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                                multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                                DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   I n t e r n a l   T r a n s f e r   c a n n o t   b e   D e l e t e d . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                If isRecChanged Then Grid_Display()
                                                Exit Sub
                                            End If
                                        Else
                                            If isRecChanged Then '#5518 fix
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display()
                                                Grid_Display()
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_I_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID_1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID_1.Text : Grid_Display()
                                If xfrm.DialogResult = DialogResult.Cancel And isRecChanged Then Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                                Dim xfrm As New Frm_Voucher_Win_FD : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xMID.Text = xTemp_MID
                                Dim IsClosed As Object = Base._FD_Voucher_DBOps.GetFDCloseDate(xTemp_MID)
                                'CHECK IF THE CURREMT FD HAS ANY TDS OR INTEREST AGAINST IT, OTHER THAN IN CURRENT TRANSACTION 
                                Dim Has_Int_TDS As Object = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, xCross_Ref_Id, 1)

                                'CHECKS IF CURRENT TRANSACTIONS IS A RENEWAL ONE, IF YES THEN GET THE ID OF NEWLY CREATED FD
                                Dim IsRenew As Object = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 2)
                                Dim New_FD As Object = Nothing
                                If IsRenew > 0 Then ' And xItemID <> "4eb60d78-ce90-4a9f-891b-7a82d79dc84b" 'Bug #5547 fix
                                    New_FD = Base._FD_Voucher_DBOps.GetNewFDIdFromClosed(xCross_Ref_Id)
                                    If New_FD Is Nothing Then
                                        Base.HandleDBError_OnNothingReturned()
                                        Exit Sub
                                    End If
                                    Has_Int_TDS = Base._FD_Voucher_DBOps.GetCount(xTemp_MID, New_FD, 1)
                                End If

                                Select Case xItemID
                                    Case "f6e4da62-821f-4961-9f93-f5177fca2a77"
                                        If IsDate(IsClosed) Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n e w e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf Has_Int_TDS > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S    a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.New_FD : xfrm.TitleX.Text = "FD Creation" : xfrm.CreatedFDID = xCross_Ref_Id
                                        End If
                                    Case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b"
                                        If IsDate(IsClosed) Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n e w e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf Has_Int_TDS > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t  F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = xCross_Ref_Id 'New_FD ' Bug #5315
                                        End If
                                    Case "1ed5cbe4-c8aa-4583-af44-eba3db08e117", "65730a27-e365-4195-853e-2f59225fe8f4"
                                        If IsRenew > 0 Then
                                            If IsDate(IsClosed) Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n ew e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            ElseIf Has_Int_TDS > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = New_FD
                                            End If
                                        Else
                                            xfrm.iAction = Common_Lib.Common.FDAction.Close_FD : xfrm.TitleX.Text = "FD Closure"
                                        End If
                                    Case Else
                                        If IsRenew > 0 Then
                                            If IsDate(IsClosed) Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("F D   A l r e a d y   C l o s e d   /   R e n e w e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            ElseIf Has_Int_TDS > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r e s t   /   T D S   a l r e a d y   e n t e r e d   a g a i n s t   F D . . . !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this FD.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                xfrm.iAction = Common_Lib.Common.FDAction.Renew_FD : xfrm.TitleX.Text = "FD Renewal" : xfrm.CreatedFDID = New_FD
                                            End If
                                        Else
                                            If Base._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 3) > 0 Then ' fdclose principle
                                                xfrm.iAction = Common_Lib.Common.FDAction.Close_FD : xfrm.TitleX.Text = "FD Closure"
                                            Else
                                                'If xItemID = "290063bc-a1a1-43af-bedb-f51b7a30c4f4" Then 'Excess interest recovered by bank on FD
                                                xfrm.iSpecific_ItemID = xItemID
                                                '    Else
                                                '    If IsDate(Base._FD_Voucher_DBOps.GetFDCloseDateByFdID(xCross_Ref_Id)) Then
                                                '        DevExpress.XtraEditors.XtraMessageBox.Show("C o n c e r n e d  F D  h a s  b e e n  c l o s e d / R e n e w e d !" & vbNewLine & vbNewLine & "Please remove such transactions to edit/delete this Entry.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                '        Exit Sub
                                                '    End If
                                                '    xfrm.iSpecific_ItemID = xItemID
                                                'End If
                                            End If
                                        End If
                                End Select
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : xMID = xfrm.xMID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                                Dim xTemp_AdvID As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID) 'Get advance cretaed by current Txn
                                If xTemp_AdvID.Length > 0 Then
                                    Dim xTemp_RefRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AdvID) 'Advance is referred in a transaction
                                    If Not xTemp_RefRecord Is Nothing Then
                                        If xTemp_RefRecord.Rows.Count > 0 Then
                                            If xTemp_RefRecord.Rows.Count > 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some Refunds were made on " & Convert.ToDateTime(xTemp_RefRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & xTemp_RefRecord.Rows(0)("TR_AMOUNT").ToString() & " for the Sale amount Receivable raised by current entry." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Sale_Asset : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID

                                Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id)
                                If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5609 point2 fix
                                    Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                    Param.CrossRefId = xCross_Ref_Id
                                    Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count
                                    If Adj_NextYear > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Some adjustments have already been made on this asset" & vbNewLine & vbNewLine & " Please delete those entries for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    xfrm.Info_MaxEditedOn = MaxEditOn
                                End If


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim xfrm As New Frm_Voucher_Membership : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim xfrm As New Frm_Voucher_Membership_Renewal : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion
                                '1 Check for Discontinued
                                Dim GetValue As Object = ""
                                GetValue = Base._Membership_DBOps.GetDiscontinued(True, xMaster_ID)
                                If GetValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.Close()
                                If GetValue.ToString.ToUpper = "DISCONTINUED" Then DevExpress.XtraEditors.XtraMessageBox.Show("D i s c o n t i u n e d   M e m b e r   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

                                '2 Check for Another Transacation
                                Dim xDate As DateTime = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ADD_ON").ToString() : Dim TrFound As Boolean = False
                                Dim CurRecordAddOn As String = Format(xDate, "yyyy-MM-dd HH:mm:ss")
                                Dim T1 As DataTable = Base._Membership_DBOps.GetMasterTransactionList(True, xMaster_ID)
                                If T1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                If T1.Rows.Count > 0 Then
                                    For Each XRow In T1.Rows
                                        If XRow("REC_ID") = xTemp_ID Then : Continue For
                                        Else
                                            xDate = XRow("REC_ADD_ON")
                                            If Format(xDate, "yyyy-MM-dd HH:mm:ss") > CurRecordAddOn Then TrFound = True : Exit For
                                        End If
                                    Next
                                    If TrFound Then DevExpress.XtraEditors.XtraMessageBox.Show("S o m e   a n o t h e r   E n t r y   a v a i l a b l e   a f t e r   t h i s   E n t r y   o f   s a m e   M e m b e r . . . . !", "Cannot Edited / Delete...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                                End If
                                '3. Check Receipt Generated.
                                If Base._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("M e m b e r s h i p   R e c e i p t   g e n e r a t e d   v o u c h e r   c a n n o t   b e   E d i t e d   /  D e l e t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim xfrm As New Frm_Voucher_Membership_Conversion : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID : xfrm.Add_Time = CurRecordAddOn
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Journal
                                Dim UseCount As Object = 0
                                Dim RefId As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._AdvanceDBOps.GetAdvancePaymentCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                RefId = Base._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._DepositsDBOps.GetTransactionCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                RefId = Base._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID)
                                If RefId.Length > 0 Then
                                    UseCount = Base._LiabilityDBOps.GetTransactionCount(RefId)
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                                    param.CrossRefId = RefId
                                    param.Excluded_Rec_M_ID = xTemp_MID
                                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                                    If UseCount > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Journal : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID

                                'Bug #5087 fixed
                                For Each Row As DataRow In Base._Voucher_DBOps.GetRefRecordIDS(xTemp_MID).Rows
                                    If Row("ITEM_PROFILE") = "OTHER ASSETS" Or Row("ITEM_PROFILE") = "LAND & BUILDING" Then
                                        If Base.IsInsuranceAudited() Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                                        End If
                                    End If
                                    If Row("ITEM_PROFILE") = "GOLD" Or Row("ITEM_PROFILE") = "SILVER" Or Row("ITEM_PROFILE") = "OTHER ASSETS" Or Row("ITEM_PROFILE") = "LIVESTOCK" Or Row("ITEM_PROFILE") = "LAND & BUILDING" Or Row("ITEM_PROFILE") = "VEHICLES" Then
                                        If Not IsDBNull(Row("TR_TRF_CROSS_REF_ID")) Then
                                            RefId = Row("TR_TRF_CROSS_REF_ID")
                                            If Not RefId Is Nothing Then
                                                If RefId.Length > 0 Then
                                                    Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(RefId)
                                                    If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5608 point2 fix
                                                        Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(RefId, True) 'bug #5566 fix, exclude_prev_year made true to allow deleting jv if the asset was partially sold in previous year
                                                        If Not SaleRecord Is Nothing Then
                                                            If SaleRecord.Rows.Count > 0 Then
                                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End If
                                                        'checks if the referred property for constt items has been transferred 'Bug #5339 fix
                                                        Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Base._open_Year_ID, RefId, True) 'exclude_prev_year made true to allow deleting jv if the asset was partially transferred in previous year
                                                        If AssetTrfRecord.Rows.Count > 0 Then
                                                            If AssetTrfRecord.Rows.Count > 0 Then
                                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End If
                                                        '#5589 fix
                                                        'If Base._next_Unaudited_YearID.Length > 0 Then
                                                        Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                                        Param.CrossRefId = RefId
                                                        Param.YearID = Base._open_Year_ID 'Bug #5687
                                                        Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                                        Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                                        Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal).Rows.Count
                                                        If Adj_NextYear > 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Selected Entry contains an asset against which adjustments have been made in current / next year" & vbNewLine & vbNewLine & " Please delete those entries for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                        'End If
                                                    Else
                                                        xfrm.Info_MaxEditedOn.Add(RefId, MaxEditOn) 'Bug #5695 fix
                                                    End If
                                                End If
                                            End If
                                        End If
                                    ElseIf Row("ITEM_PROFILE") = "ADVANCES" Or Row("ITEM_PROFILE") = "OTHER LIABILITIES" Or Row("ITEM_PROFILE") = "OTHER DEPOSITS" Then 'Bug #5608 pt.3
                                        If Not IsDBNull(Row("TR_TRF_CROSS_REF_ID")) Then
                                            RefId = Row("TR_TRF_CROSS_REF_ID")
                                            If Not RefId Is Nothing Then
                                                If RefId.Length > 0 Then
                                                    If Row("ITEM_PROFILE") = "OTHER DEPOSITS" Then
                                                        Dim FinalPayDate As Object = Base._DepositsDBOps.GetFinalPaymentDate(RefId)
                                                        If IsDate(FinalPayDate) And FinalPayDate <> DateTime.MinValue Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " & FinalPayDate.ToLongDateString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End If

                                                    Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                                                    Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData(Row("ITEM_PROFILE"), Row("TR_ITEM_ID"), Row("TR_AB_ID_1"), xTemp_MID, Common_Lib.Common.Navigation_Mode._Delete, RefId)
                                                    If Base._next_Unaudited_YearID <> Nothing Then
                                                        If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Journal Entry creates a Negative Closing Balance in Next Year for " & Row("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End If

                                                    If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Journal Entry creates a Negative Closing Balance in Current Year for " & Row("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID.Text : Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()

                            Case Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                                Dim _RowHandle As Integer = 0
                                If Val(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_SR_NO").ToString()) = 2 Then
                                    _RowHandle = 1
                                End If
                                If (Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle - _RowHandle, "iTR_TYPE").ToString()) = "CREDIT" And Not IsDBNull(xCross_Ref_Id) Then
                                    If xCross_Ref_Id.Length > 0 Then
                                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                                        If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to <color=red><u>Delete</u></color> Matched Asset Transfer...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                                            xPromptWindow.Dispose()
                                            For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(xTemp_MID).Rows ' Get Actual Item IDs from Selected Transaction
                                                Dim xTemp_ItemID As String = cRow(0).ToString()
                                                Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                                Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                                                Dim xTemp_AssetID As String = ""
                                                Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                                    Case "GOLD", "SILVER"
                                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, xTemp_MID)
                                                    Case "OTHER ASSETS"
                                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, xTemp_MID)
                                                    Case "LIVESTOCK"
                                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, xTemp_MID)
                                                    Case "VEHICLES"
                                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, xTemp_MID)
                                                    Case "LAND & BUILDING"
                                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, xTemp_MID)
                                                End Select
                                                If xTemp_AssetID.Length > 0 Then
                                                    Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                                    If Not SaleRecord Is Nothing Then
                                                        If SaleRecord.Rows.Count > 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                    Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'checks if the referred property for constt items has been transferred 'Bug #5339 fix
                                                    If AssetTrfRecord.Rows.Count > 0 Then
                                                        If AssetTrfRecord.Rows.Count > 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                    'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                                    Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                                    If Not ReferenceRecord Is Nothing Then
                                                        If ReferenceRecord.Rows.Count > 0 Then
                                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        Else
                                            xPromptWindow.Dispose()
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    Dim Rec As DataTable = Base._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1)
                                    If Not Rec Is Nothing Then
                                        If Rec.Rows.Count > 0 Then
                                            If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> Rec.Rows(0)("REC_EDIT_ON") Then
                                                isRecChanged = True
                                            End If
                                            If Not IsDBNull(Rec.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                                If Rec.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                                    multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   A s s e t   T r a n s f e r   c a n n o t   b e   D e l e t e d . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                    If isRecChanged Then Grid_Display()
                                                    Exit Sub
                                                End If
                                            Else
                                                If isRecChanged Then '#5518 fix
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display()
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                Dim xfrm As New Frm_Voucher_Win_Asset_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID

                                'Checks for any jv adjustments made on the asset
                                If (Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle - _RowHandle, "iTR_TYPE").ToString()) = "DEBIT" Then
                                    Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetRecord(xTemp_MID, 2)
                                    Dim MaxEditOn As DateTime = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(d1.Rows(0)("TR_REF_OTHERS"))
                                    If DateDiff(DateInterval.Second, MaxEditOn, Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")) < 0 Then 'bug #5610 point2 fix
                                        Dim Param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
                                        Param.CrossRefId = d1.Rows(0)("TR_REF_OTHERS")
                                        Param.NextUnauditedYearID = Base._next_Unaudited_YearID
                                        Param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                                        Dim Adj_NextYear As Integer = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count
                                        If Adj_NextYear > 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Some adjustments have already been made on this asset" & vbNewLine & vbNewLine & " Please delete those entries for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        xfrm.Info_MaxEditedOn = MaxEditOn
                                    End If
                                End If


                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID1.Text : Grid_Display()
                                If xfrm.DialogResult = DialogResult.Cancel And isRecChanged Then Grid_Display()
                                If Not xfrm Is Nothing Then xfrm.Dispose()
                        End Select
                    End If
                End If
            Case "VIEW"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    Dim xTemp_MID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                    Dim xMaster_ID As String = "" : If xTemp_MID.Length > 0 Then xMaster_ID = xTemp_MID Else xMaster_ID = xTemp_ID
                    If xTemp_ID = "OPENING BALANCE" Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Opening Balance cannot view from Voucher Entry...!" & vbNewLine & vbNewLine & "Note: Use Profile - Cash A/c. Information for CASH" & vbNewLine & Space(31) & "or Bank A/c. Information for BANK", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If xTemp_ID = "NOTE-BOOK" Then
                        Dim xFrm As New Frm_NoteBook_Info : xFrm.MainBase = Base : xFrm.Text = "Note-Book Entry (" & Me.Text & ")"
                        xFrm.Tag = Common_Lib.Common.Navigation_Mode._View : xFrm.xEntryDate = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_DATE").ToString()
                        xFrm.ShowDialog(Me) : xFrm.Dispose()
                        xID = xTemp_ID
                        Exit Sub
                    End If

                    '
                    If xTemp_ID.Length > 0 Then
                        Dim Entry_Found As Boolean = False
                        Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID)
                        If Status Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Status.Rows.Count > 0 Then ' checks for record existence here 
                            Entry_Found = True
                        End If

                        'takes action if there is no record 
                        If Entry_Found = False Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   i n   b a c k g r o u n d  . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                        Dim xVCode As Integer = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString()
                        Select Case xVCode
                            Case Common_Lib.Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn
                                Dim xfrm As New Frm_Voucher_Win_Cash : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Bank_To_Bank_Transfer
                                Dim xfrm As New Frm_Voucher_Win_B2B : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID
                                xfrm.ShowDialog(Me) : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Regular
                                Dim xfrm As New Frm_Voucher_Win_Donation_R : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Foreign
                                Dim xfrm As New Frm_Voucher_Win_Donation_F : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Collection_Box
                                Dim xfrm As New Frm_Voucher_Win_C_Box : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Payment
                                Dim xfrm As New Frm_Voucher_Win_Gen_Pay : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Receipt
                                Dim xfrm As New Frm_Voucher_Win_Gen_Rec : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
                                Dim xfrm As New Frm_Voucher_Win_Gift : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
                                Dim xfrm As New Frm_Voucher_Win_I_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID_1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Fixed_Deposits
                                Dim xfrm As New Frm_Voucher_Win_FD : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xMID.Text = xTemp_MID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                                Dim xfrm As New Frm_Voucher_Win_Sale_Asset : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership
                                Dim xfrm As New Frm_Voucher_Membership : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal
                                Dim xfrm As New Frm_Voucher_Membership_Renewal : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion
                                Dim xfrm As New Frm_Voucher_Membership_Conversion : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case (Common_Lib.Common.Voucher_Screen_Code.Journal)
                                Dim xfrm As New Frm_Voucher_Win_Journal : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()
                            Case Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                                Dim xfrm As New Frm_Voucher_Win_Asset_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._View : xfrm.xID1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                                '-----------------------------+
                                'Start : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.Info_LastEditedOn = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON")
                                '-----------------------------+
                                'End : Edit date sent to Check if entry already changed 
                                '-----------------------------+
                                xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.Retry Then Grid_Display() : If Not xfrm Is Nothing Then xfrm.Dispose()

                        End Select
                    End If
                End If
            Case "MATCH TRANSFERS"
                Dim xVCode As Integer = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString()
                Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                'Dim xCross_Ref_ID As Object = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iCross_Ref_ID")
                Dim xParty_ID As Object = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_AB_ID_1")
                Dim xTemp_MID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()

                If Not xVCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("P l e a s e   s e l e c t   a   I n t e r n a l   T r a n s f e r   V o u c h e r . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                End If
                Dim isRecChanged As Boolean = False
                Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID)
                If Status Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Status.Rows.Count > 0 Then
                    If Common_Lib.Common.Record_Status._Locked = Status.Rows(0)("REC_STATUS") Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   M a t c h e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                    End If
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                End If

                If xVCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer Then
                    Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                    If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> d1.Rows(0)("REC_EDIT_ON") Then
                        isRecChanged = True
                    End If
                    If Not IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                        If isRecChanged Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been matched in the background.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Grid_Display()
                            Exit Sub
                        End If

                        If DialogResult.No = xPromptWindow.ShowDialog("Confirmation...", "Transfer Voucher already Matched...!" & vbNewLine & "Do you want to rematch the transfer...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                            xPromptWindow.Dispose()
                            If isRecChanged Then Grid_Display() '#5518 fix
                            Exit Sub
                        Else
                            xPromptWindow.Dispose()
                        End If
                    Else
                        If isRecChanged Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Record has been unmatched in the background.", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Grid_Display()
                            Exit Sub
                        End If
                    End If
                    Dim xTf As New Frm_I_Transfer_Matching()
                    xTf.to_Match_Txn_ID = xTemp_ID : xTf.to_Cen_Rec_ID = Base._Internal_Tf_Voucher_DBOps.GetCenterID(xParty_ID.ToString()).ToString() : xTf.MainBase = Base
                    xTf.ShowDialog(Me)
                    If Not xTf Is Nothing Then xTf.Dispose()
                End If
            Case "UNMATCH TRANSFERS"
                Dim xVCode As Integer = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString()
                Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                'Dim xCross_Ref_ID As Object = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iCross_Ref_ID")
                Dim xTemp_MID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                Dim xTr_Date As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_DATE").ToString()

                If Not xVCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("P l e a s e   s e l e c t   a   I n t e r n a l   T r a n s f e r   V o u c h e r . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                End If

                'check status of original rec
                Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID)
                If Status Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Status.Rows.Count > 0 Then
                    If Common_Lib.Common.Record_Status._Locked = Status.Rows(0)("REC_STATUS") Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   U n m a t c h e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                    End If
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                End If
                Dim multiUserMsg As String = "" : Dim isRecChanged As Boolean = False
                Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                If Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_EDIT_ON") <> d1.Rows(0)("REC_EDIT_ON") Then
                    isRecChanged = True
                End If
                If IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Or d1.Rows(0)("TR_TRF_CROSS_REF_ID") Is Nothing Then
                    multiUserMsg = vbNewLine & vbNewLine & "Record has already been unmatched in the background"
                    DevExpress.XtraEditors.XtraMessageBox.Show("S e l e c t e d   r e c o r d   i s   a l r e a d y   u n m a t c h e d  . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                    If isRecChanged Then Grid_Display() '#5518 fix
                    Exit Sub
                Else
                    If isRecChanged Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Transfer Voucher matched in the background...!" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                        Exit Sub
                    End If
                End If

                'check status of matched rec
                Status = Base._Voucher_DBOps.GetStatus_TrCode_OtherCentre(d1.Rows(0)("TR_TRF_CROSS_REF_ID"))
                If Status Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Status.Rows.Count > 0 Then
                    If Common_Lib.Common.Record_Status._Locked = Status.Rows(0)("REC_STATUS") Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   M a t c h e d   w i t h   t h i s   r e c o r d   i s   a l r e a d y   l o c k e d  . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock that Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                    End If
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                End If

                If xVCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer Then
                    If IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("T r a n s f e r   V o u c h e r   a l r e a d y   u n m a t c h e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Exit Sub
                    End If
                    'Unmatch here
                    If Base._Internal_Tf_Voucher_DBOps.UnMatchTransfers(xTemp_ID, d1.Rows(0)("TR_TRF_CROSS_REF_ID"), xTr_Date) Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("T r a n s f e r   E n t r y   u n m a t c h e d   s u c c e s s f u l l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y !   T r a n s f e r   E n t r y   C o u l d   n o t   b e   u n m a t c h e d   s u c c e s s f u l l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                End If
            Case "VOUCHER"
                Dim xVCode As Integer = Val(Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_CODE").ToString())
                Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                If xVCode = Common_Lib.Common.Voucher_Screen_Code.Collection_Box Then
                    xPleaseWait.Show("G e n e r a t i n g    C o l l e c t i o n   B o x    V o u c h e r")
                    Dim xRep As New CBVoucher(xTemp_ID)
                    xRep.ShowPreviewDialog()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("C o m i n g   S o o n . . . !", "Voucher Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.None
                End If
            Case "LOCKED_ALL"
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "Sure you want to <color=red>Lock all</color> entries in Selected period...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
                If Me.BandedGridView1.RowCount > 0 And Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                    xPleaseWait.Show("Locking All Entries...")
                    Dim NotLocked As Integer = 0
                    For Ctr As Integer = 0 To BandedGridView1.RowCount - 1
                        If Not Me.BandedGridView1.IsDataRow(Ctr) Then Continue For
                        Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Ctr, "iACTION_STATUS").ToString()
                        Dim xRemarks As Object = Me.BandedGridView1.GetRowCellValue(Ctr, "RemarkStatus")
                        If xStatus.ToUpper = "LOCKED" Then
                            Continue For
                        End If
                        If xStatus.ToUpper = "INCOMPLETE" Then
                            NotLocked += 1 : Continue For
                        End If
                        If Not xRemarks Is Nothing And Not IsDBNull(xRemarks) Then
                            NotLocked += 1 : Continue For
                        End If
                        Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Ctr, "iTR_M_ID").ToString()
                        If xTemp_ID.Length = 36 Then
                            If Not Base._Voucher_DBOps.MarkAsLockedByMasterID(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                xPleaseWait.Hide() : Exit Sub
                            End If
                        Else
                            xTemp_ID = Me.BandedGridView1.GetRowCellValue(Ctr, "iREC_ID").ToString()
                            If xTemp_ID.Length = 36 Then
                                If Not Base._Voucher_DBOps.MarkAsLocked(xTemp_ID) Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    xPleaseWait.Hide() : Exit Sub
                                End If
                            Else
                                Continue For
                            End If
                        End If
                        '  xID = Me.BandedGridView1.GetRowCellValue(Ctr, "iREC_ID").ToString()
                    Next
                    xPleaseWait.Hide()
                    If NotLocked > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(NotLocked & " R e c o r d s   n o t  l o c k e d  s u c c e s s f u l l y . . . !" & vbNewLine & vbNewLine & "Incomplete Records, Records with open Queries, Notebook Expenses and Opening Balances are skipped.", "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                        Exit Sub
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show("A l l   R e c o r d s   L o c k e d   S u c c e s s f u l l y . . . !" & vbNewLine & vbNewLine & "Notebook Expenses and Opening Balances are skipped.", "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                        Exit Sub
                    End If
                End If
            Case "LOCKED"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.SelectedRowsCount) > 0 And Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                    Dim Ctr As Integer = 0
                    For Each CurrRowHandle As Integer In Me.BandedGridView1.GetSelectedRows
                        If Not Me.BandedGridView1.IsDataRow(CurrRowHandle) Then Continue For
                        Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iTR_M_ID").ToString()
                        If xTemp_ID.Trim.Length < 36 Then xTemp_ID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()
                        If xTemp_ID.Length < 36 Then
                            If xTemp_ID.ToString.ToLower = "opening balance" Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("O p e n i n g   B a l a n c e s   c a n   b e   u n l o c k e d   i n   p r o f i l e   s c r e e n s   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect opening balance Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                            End If
                            If xTemp_ID.ToString.ToLower = "note-book" Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("N o t e b o o k   e n t r i e s   c a n   b e   u n l o c k e d   i n   N o t e b o o k   s c r e e n   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect notebook Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                            End If
                        End If
                        Dim MaxValue As Object = 0
                        Dim Msg As String = ""
                        Dim AllowUser As Boolean = False
                        Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString())
                        If Status Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Status.Rows.Count > 0 Then ' checks for record existence here 
                            MaxValue = Status.Rows(0)("REC_STATUS")

                            Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iACTION_STATUS").ToString()
                            Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                            If value <> MaxValue Then
                                Msg = "Record Status has been changed in the background by another user"
                                If MaxValue = Common_Lib.Common.Record_Status._Completed Then
                                    AllowUser = True
                                End If
                                If AllowUser Then
                                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                                    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "Record has been Unlocked in the background by another user" & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                        xPromptWindow.Dispose()
                                    Else
                                        xPromptWindow.Dispose()
                                        Grid_Display()
                                        Exit Sub
                                    End If
                                End If
                            Else
                                Msg = "Information..."
                            End If
                        End If

                        Dim xRemarks As Object = Base._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, xTemp_ID)
                        If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("A l r e a d y   L o c k e d   E n t r i e s   c a n n o t   b e   R e - L o c k e d . . . !" & vbNewLine & vbNewLine & "Please unselect already locked Entries ...!", Msg, MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                        If MaxValue = Common_Lib.Common.Record_Status._Incomplete Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("I n c o m p l e t e   E n t r i e s   c a n n o t   b e   L o c k e d . . . !" & vbNewLine & vbNewLine & "Please unselect incomplete Entries or ask Center to Complete it...!", Msg, MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                        If Not xRemarks Is Nothing And Not IsDBNull(xRemarks) Then
                            If MaxValue > 0 Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r i e s   w i t h   p e n d i n g   q u e r i e s   c a n ' t   b e   L o c k e d. . . !" & vbNewLine & vbNewLine & "Please unselect such Entries...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If

                    Next
                    For Each CurrRowHandle As Integer In Me.BandedGridView1.GetSelectedRows
                        If Not Me.BandedGridView1.IsDataRow(CurrRowHandle) Then Continue For
                        Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iTR_M_ID").ToString()
                        Ctr += 1
                        If xTemp_ID.Length = 36 Then
                            If Not Base._Voucher_DBOps.MarkAsLockedByMasterID(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            xTemp_ID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()
                            If Not Base._Voucher_DBOps.MarkAsLocked(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                        xID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()
                    Next
                    If Ctr > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.LockedSuccess(Ctr), "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                        Exit Sub
                    End If
                End If
            Case "UNLOCKED"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.SelectedRowsCount) > 0 And Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Then
                    Dim Ctr As Integer = 0
                    For Each CurrRowHandle As Integer In Me.BandedGridView1.GetSelectedRows
                        If Not Me.BandedGridView1.IsDataRow(CurrRowHandle) Then Continue For
                        Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iTR_M_ID").ToString()
                        If xTemp_ID.Trim.Length < 36 Then xTemp_ID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()

                        If xTemp_ID.Length = 36 Then
                            If xTemp_ID.ToString.ToLower = "opening balance" Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("O p e n i n g   B a l a n c e s   c a n   b e   u n l o c k e d   i n   p r o f i l e   s c r e e n s   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect opening balance Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                            End If
                            If xTemp_ID.ToString.ToLower = "note-book" Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("N o t e b o o k   e n t r i e s   c a n   b e   u n l o c k e d   i n   N o t e b o o k   s c r e e n   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect notebook Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                            End If
                        End If
                        Dim MaxValue As Object = 0
                        Dim Msg As String = ""
                        Dim AllowUser As Boolean = False
                        Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString())
                        If Status Is Nothing Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Status.Rows.Count > 0 Then ' checks for record existence here 
                            MaxValue = Status.Rows(0)("REC_STATUS")

                            Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iACTION_STATUS").ToString()
                            Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                            If value <> MaxValue Then
                                Msg = "Record Status has been changed in the background by another user"
                                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                                    AllowUser = True
                                End If
                                If AllowUser Then
                                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                                    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "The Record has been Locked in the background by another user" & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                        xPromptWindow.Dispose()
                                    Else
                                        xPromptWindow.Dispose()
                                        Grid_Display()
                                        Exit Sub
                                    End If
                                End If
                            Else
                                Msg = "Information..."
                            End If
                        End If
                        If MaxValue = Common_Lib.Common.Record_Status._Completed Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("A l r e a d y   U n l o c k e d   E n t r i e s   c a n n o t   b e   R e - U n l o c k e d . . . !" & vbNewLine & vbNewLine & "Please unselect already unlocked Entries ...!", Msg, MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                        If MaxValue = Common_Lib.Common.Record_Status._Incomplete Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("I n c o m p l e t e   E n t r i e s   c a n n o t   b e   U n l o c k e d . . . !" & vbNewLine & vbNewLine & "Please unselect incomplete Entries or ask Center to Complete it...!", Msg, MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If

                    Next

                    For Each CurrRowHandle As Integer In Me.BandedGridView1.GetSelectedRows
                        If Not Me.BandedGridView1.IsDataRow(CurrRowHandle) Then Continue For
                        Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iTR_M_ID").ToString()
                        Ctr += 1
                        If xTemp_ID.Length = 36 Then
                            If Not Base._Voucher_DBOps.MarkAsCompleteByMasterID(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            xTemp_ID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()
                            If Not Base._Voucher_DBOps.MarkAsComplete(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                        xID = Me.BandedGridView1.GetRowCellValue(CurrRowHandle, "iREC_ID").ToString()
                    Next
                    If Ctr > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UnlockedSuccess(Ctr), "Locked...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Grid_Display()
                        Exit Sub
                    End If
                End If
            Case "REMARKS"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 And Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.View_Remarks) Then
                    If Not Me.BandedGridView1.IsDataRow(BandedGridView1.FocusedRowHandle) Then Exit Sub
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                    If xTemp_ID.Trim.Length < 36 Then xTemp_ID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    If xTemp_ID.Length < 36 Then
                        If xTemp_ID.ToString.ToLower = "opening balance" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("R e m a r k s   f o r   O p e n i n g   B a l a n c e s   c a n   b e   v i e w e d   i n   p r o f i l e   s c r e e n s   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect opening balance Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                        If xTemp_ID.ToString.ToLower = "note-book" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("R e m a r k s   f o r   N o t e b o o k   e n t r i e s   c a n   b e   v i e w e d   i n   N o t e b o o k   s c r e e n   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect notebook Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                    End If
                    Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iACTION_STATUS").ToString()
                    xID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    View_Actions(xTemp_ID, Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, xStatus, Me)
                    Grid_Display()
                End If
            Case "ADD REMARKS"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 And Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Manage_Remarks) Then
                    If Not Me.BandedGridView1.IsDataRow(BandedGridView1.FocusedRowHandle) Then Exit Sub
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iTR_M_ID").ToString()
                    If xTemp_ID.Trim.Length < 36 Then xTemp_ID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    If xTemp_ID.Length < 36 Then
                        If xTemp_ID.ToString.ToLower = "opening balance" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("R e m a r k s   f o r   O p e n i n g   B a l a n c e s   c a n   b e   a d d e d   i n   p r o f i l e   s c r e e n s   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect opening balance Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                        If xTemp_ID.ToString.ToLower = "note-book" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("R e m a r k s   f o r   N o t e b o o k   e n t r i e s   c a n   b e   a d d e d   i n   N o t e b o o k   s c r e e n   o n l y . . . !" & vbNewLine & vbNewLine & "Please unselect notebook Entries ...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
                        End If
                    End If
                    xID = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iACTION_STATUS").ToString()
                    If xStatus.ToUpper <> "LOCKED" Then
                        Add_Actions(xTemp_ID, Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Me)
                        Grid_Display()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show("Q u e r i e s   c a n n o t   b e   a d d e d   t o   F r e e z e d   E n t r y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            Case "COMPLETED1"
                If Me.BandedGridView1.RowCount > 0 And Val(Me.BandedGridView1.FocusedRowHandle) >= 0 Then
                    Dim xTemp_ID As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "iREC_ID").ToString()
                    If xTemp_ID.Length > 0 Then
                        Dim xTemp_1 As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "ITEM_NAME").ToString()
                        Dim xTemp_2 As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Make").ToString()

                        Dim xEdit_By As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Edit_By").ToString()
                        Dim xStatus As String = Me.BandedGridView1.GetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Action_Status").ToString()
                        Dim xEntry As String = "Name: " & xTemp_1 & vbNewLine & _
                                                "Make: " & xTemp_2
                        If xStatus.Trim.ToUpper.ToString = "COMPLETED" Or xStatus.Trim.ToUpper.ToString = "LOCKED" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   A l r e a d y   C o m p l e t e d . . . !", "Entry...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                        If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "<u>Sure you want to<color=red> Completed</color> this Entry...?</u>" & vbNewLine & vbNewLine & "<size=12>" & xEntry & "</size>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                            xPromptWindow.Dispose()
                            If Base._open_User_ID.Trim.ToUpper.ToString <> xEdit_By.Trim.ToUpper.ToString Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("I n c o r r e c t   U s e r . . . !" & vbNewLine & _
                                                                            "=================" & vbNewLine & vbNewLine & _
                                                                            "Following Entry Control By " & xEdit_By.ToString & " User...!" & vbNewLine & vbNewLine & _
                                                                            xEntry, "Cannot Completed.....", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            If Base._Voucher_DBOps.MarkAsComplete(xTemp_ID) Then
                                Grid_Display()
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, "Action...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            xPromptWindow.Dispose()
                        End If
                    End If
                End If
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If Me.BandedGridView1.RowCount > 0 Then
                    Base.Show_ListPreview(GridControl1, "Voucher Entry  (UID: " & Base._open_UID_No & ")", Me, True, System.Drawing.Printing.PaperKind.A4, Me.Text, "UID: " & Base._open_UID_No, BE_View_Period.Text, True)
                End If
                Me.BandedGridView1.Focus()
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
            Case "PRINT-LIST"
            Case "ADV. FILTERS"
                Dim xFrm As New Frm_AdvancedFilters
                If Me.BandedGridView1.ActiveFilterString.Length > 0 And Me.BandedGridView1.ActiveFilterString.Contains("Advanced_Filter") Then
                    Dim Ind As Integer = Me.BandedGridView1.ActiveFilterString.IndexOf("[Advanced_Filter]")
                    Dim sub_str1 As String = ""
                    If Ind > 0 Then sub_str1 = Me.BandedGridView1.ActiveFilterString.Substring(0, Ind - 1)
                    Dim sub_str2 As String = Me.BandedGridView1.ActiveFilterString.Substring(Ind)
                    If sub_str2.Split("=").Count > 1 Then
                        xFrm.Advanced_Filter_Category = sub_str2.Split("=")(1).Replace("'", "").Trim
                        xFrm.Advanced_Filter_RefId = sub_str2.Split("=")(2).Replace("'", "").Trim
                        xFrm.FilterType = FType
                    End If
                    Me.BandedGridView1.ActiveFilterString = sub_str1
                End If
                xFrm.ShowDialog(Me)
                If xFrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    Advanced_Filter_Category = xFrm.Advanced_Filter_Category
                    Advanced_Filter_RefID = xFrm.Advanced_Filter_RefId
                    FType = xFrm.FilterType
                    If Advanced_Filter_Category.Length > 0 Then
                        If Me.BandedGridView1.ActiveFilterString.Length > 0 Then
                            Me.BandedGridView1.ActiveFilterString = Me.BandedGridView1.ActiveFilterString + "OR" + " [Advanced_Filter] ='" & Advanced_Filter_Category & " = " & Advanced_Filter_RefID & "'"
                        Else
                            Me.BandedGridView1.ActiveFilterString = "[Advanced_Filter] ='" & Advanced_Filter_Category & " = " & Advanced_Filter_RefID & "'"
                        End If
                    End If
                    Grid_Display()
                Else
                    xFrm.Dispose()
                End If
            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

    Dim Closed_Bank_Account_No As String = ""
    Public Function Get_Closed_Bank_Status(ByVal xRecID As String) As Boolean
        Dim Flag As Boolean = False : Dim CR_LED_ID As String = "" : Dim DR_LED_ID As String = "" : Dim xTR_MODE As String = "" : Dim xTR_CODE As Integer = 0

        Dim d4 As DataTable = Base._Voucher_DBOps.GetTransactionDetail(xRecID)
        If d4.Rows.Count > 0 Then
            If Not IsDBNull(d4.Rows(0)("TR_SUB_CR_LED_ID")) Then CR_LED_ID = d4.Rows(0)("TR_SUB_CR_LED_ID") Else CR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_SUB_DR_LED_ID")) Then DR_LED_ID = d4.Rows(0)("TR_SUB_DR_LED_ID") Else DR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_CODE")) Then xTR_CODE = d4.Rows(0)("TR_CODE") Else xTR_CODE = 0
            If Not IsDBNull(d4.Rows(0)("TR_MODE")) Then xTR_MODE = d4.Rows(0)("TR_MODE") Else xTR_MODE = ""
        End If
        If xTR_CODE = 6 Or xTR_CODE = 1 Or UCase(xTR_MODE) <> "CASH" Then 'count(rec_id) 'Bug #4728 fix
            Dim MaxValue As Object = Nothing
            MaxValue = Base._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID)
            If IsDBNull(MaxValue) Then
                Flag = False : Closed_Bank_Account_No = ""
            Else
                Flag = True : Closed_Bank_Account_No = MaxValue
            End If
        End If
        Return Flag
    End Function

    Private Function FindLocationUsage(PropertyID As String, Optional Exclude_Sold_TF As Boolean = True) As String
        Dim MaxValue As Object = 0
        Dim Message As String = ""
        Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID)
        For Each cRow As DataRow In Locations.Rows
            Dim LocationID As String = cRow(0).ToString()
            Dim UsedPage As String = Base._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF)
            Dim DeleteAllow As Boolean = True
            If UsedPage.Length > 0 Then DeleteAllow = False
            If Not DeleteAllow Then
                Message = "P r o p e r t y   b e i n g   s o l d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                Exit For
            End If
        Next
        Return Message
    End Function

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
            Grid_Display()
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

#End Region

End Class



