Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq

Public Class Frm_Existing_References

#Region "Start--> Default Variables"

    'Dim _db_Connection As New OleDbConnection
    'Dim _db_DataAdapter As OleDbDataAdapter = Nothing
    Dim _db_Table As New DataTable()


    Public Led_ID As String
    
    Public REC_EDIT_ON As DateTime
    Public Txn_M_ID As String
    Public Ref_Rec_ID As String
    Public Reference As String = ""
    Public Opening As Decimal = 0
    Public Closing As Decimal = 0
    Public NextYearClosing As Decimal = 0
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

#End Region

    '#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Reference List" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Set_Default() ' Prepare Status-bar help text of all objects
        '   If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then GridControl1.Enabled = False
        Me.Focus()
    End Sub

    '#End Region

    '#Region "Start--> Button Events"

    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
            Closing = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Closing")
            If Base._next_Unaudited_YearID <> Nothing Then
                NextYearClosing = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Next Year Closing Value")
            End If
            Opening = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Opening")
            Reference = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Reference")
            Ref_Rec_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID")
            '; REC_EDIT_ON = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_EDIT_ON") 'Bug #5202 fixed
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
            DevExpress.XtraEditors.XtraMessageBox.Show("D a t a   N o t   F o u n d . . . !", Me.Txt_TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.GridView1.Focus()
        End If
    End Sub

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.GotFocus, BUT_CANCEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.LostFocus, BUT_CANCEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_OK.KeyDown, BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click ', T_Edit.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            ' Dim btn As SimpleButton
            '    btn = CType(sender, SimpleButton)
            '    If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            '    If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            'Else
            '    Dim T_btn As ToolStripMenuItem
            '    T_btn = CType(sender, ToolStripMenuItem)
            '    If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            'If UCase(T_btn.Name) = "T_TXNREPORT" Then Me.DataNavigation("Txn. Report")
            'If UCase(T_btn.Name) = "T_VIEW" Then Me.DataNavigation("VIEW")
        End If

    End Sub


    '#End Region

    '#Region "Start--> Procedures"

    Private Sub Set_Default()
        Base.Get_Configure_Setting()
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Grid_Display() ' Prepare Grid Data
        xPleaseWait.Hide()
    End Sub
    'Private Sub DataNavigation(ByVal Action As String)
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
    '        Select Case Action
    '            Case "NEW"
    '                Dim xfrm As New Frm_WIP_Window
    '                xfrm.GLookUp_WIP_LedgerList.Text = Ledger
    '                xfrm.Txt_Amount.Text = Amount
    '                xfrm.ShowDialog(Me)
    '                If xfrm.DialogResult = DialogResult.OK Then

    '                Else
    '                    xfrm.Dispose()
    '                End If
    '                If Not xfrm Is Nothing Then xfrm.Dispose()
    '            Case "EDIT"
    '                Dim xfrm As New Frm_WIP_Window
    '                xfrm.GLookUp_WIP_LedgerList.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP Ledger").ToString
    '                xfrm.Txt_Reference.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Reference").ToString
    '                xfrm.ShowDialog(Me)
    '                If xfrm.DialogResult = DialogResult.OK Then
    '                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Reference", xfrm.Txt_Reference.Text)
    '                Else
    '                    xfrm.Dispose()
    '                End If
    '        End Select
    '    End If
    'End Sub

    Private Sub Grid_Display()

        Dim WIPProfile As Common_Lib.RealTimeService.Param_GetProfileListing_WIP = New Common_Lib.RealTimeService.Param_GetProfileListing_WIP
        WIPProfile.Prev_YearId = Base._prev_Unaudited_YearID
        WIPProfile.Next_YearID = Base._next_Unaudited_YearID
        WIPProfile.WIP_LED_ID = Led_ID
        WIPProfile.TR_M_ID = Txn_M_ID
        _db_Table = Base._WIPDBOps.GetProfileListing_WIP(WIPProfile) 'Base._AssetDBOps.GetList(Voucher_Entry, Profile_Entry)
        If _db_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        If _db_Table.Rows.Count <= 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("No Reference Present for selected Ledger. Please add New Reference, if you want to proceed", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Me.Close()
            Exit Sub
        End If

        Me.GridControl1.DataSource = _db_Table
        Me.GridView1.Columns("LED_ID").Visible = False
        'Me.GridView1.Columns("YearID").Visible = False
        Me.GridView1.Columns("Add By").Visible = False
        Me.GridView1.Columns("Add Date").Visible = False
        Me.GridView1.Columns("Edit By").Visible = False
        Me.GridView1.Columns("Edit Date").Visible = False
        Me.GridView1.Columns("Action Status").Visible = False
        Me.GridView1.Columns("Action By").Visible = False
        Me.GridView1.Columns("Action Date").Visible = False
        Me.GridView1.Columns("ID").Visible = False
        Me.GridView1.Columns("YearID").Visible = False
        Me.GridView1.Columns("WIP Ledger").Visible = False
        Me.GridView1.Columns("RemarkCount").Visible = False
        Me.GridView1.Columns("RemarkStatus").Visible = False
        Me.GridView1.Columns("Entry Type").Visible = False
        Me.GridView1.Columns("CrossedTimeLimit").Visible = False
        Me.GridView1.Columns("TR_ID").Visible = False
        Me.GridView1.Columns("OpenActions").Visible = False
        Me.GridView1.Columns("Reference").MinWidth = 233
        Me.GridView1.Columns("Reference").BestFit()
        Me.GridView1.Columns("SNo.").MinWidth = 30         'Me.GridView1.Columns("Sr.").BestFit()
        Me.GridView1.Columns("Opening").MinWidth = 85
        Me.GridView1.Columns("Addition").MinWidth = 85
        Me.GridView1.Columns("Deduction").MinWidth = 85
        Me.GridView1.Columns("Closing").MinWidth = 85
        Me.GridView1.Columns("Next Year Closing Value").Visible = False
        ' Me.GridView1.Columns("Model").MinWidth = 60
        'Me.GridView1.Columns("WIP_Amount").MinWidth = 100
        'Me.GridView1.Columns("Curr Value").MinWidth = 100
        'Me.GridView1.Columns("Model").BestFit()

        ' Me.GridView1.Columns("Address").Width = 300
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()

        If Not Ref_Rec_ID Is Nothing Then
            For ctr As Integer = 0 To _db_Table.Rows.Count - 1
                If Me.GridView1.GetRowCellValue(ctr, "ID").ToString = Ref_Rec_ID Then
                    Me.GridView1.FocusedRowHandle = ctr
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub GridControl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            BUT_OK_Click(Nothing, Nothing)
        End If
    End Sub

    '#End Region


End Class
