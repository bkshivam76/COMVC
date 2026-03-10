Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Dialog_Deposit_Slips

#Region "Start--> Default Variables"

    Public MainBase As New Common_Lib.Common
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()

    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Hide_Properties()
    End Sub
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.BE_Slip_No.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click
        Hide_Properties()
        If Len(Trim(Me.BE_Slip_No.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("S l i p   N o.   N o t   E n t e r e d . . . !", Me.BE_Slip_No, 0, Me.BE_Slip_No.Height, 5000)
            Me.BE_Slip_No.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.BE_Slip_No)
        End If

        'Dim dTable As DataTable = Base._DepositSlipsDBOps.GetRecord(Val(BE_Slip_No.Text))
        'If dTable.Rows.Count = 0 Then
        '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
        '    Me.ToolTip1.Show("S l i p   N o.   d o e s   N o t   E x i s t   i n   C u r r .  Y e a r . . . !", Me.BE_Slip_No, 0, Me.BE_Slip_No.Height, 5000)
        '    Me.BE_Slip_No.Focus()
        '    Me.DialogResult =DialogResult.None
        '    Exit Sub
        'End If

        If Not IsDate(Trim(Me.Txt_Dep_Date.Text)) Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("D e p o s i t   D a t e   I n c o r r e c t . . . !", Me.Txt_Dep_Date, 0, Me.Txt_Dep_Date.Height, 5000)
            Me.Txt_Dep_Date.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Txt_Dep_Date)
        End If

        'Dim xRep As New Report_Deposit_slips : xRep.MainBase = Base
        'xRep.SlipID = dTable.Rows(0)("REC_ID").ToString
        'Base.Show_ReportPreview(xRep, "Deposit Slip", Me, True)
        'xRep.Dispose()

        'Dim xPromptWindow As New Common_Lib.Prompt_Window
        'If IsDBNull(dTable.Rows(0)("SL_PRINT_DATE")) Then
        '    If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", "Do you want to mark deposit slip as Printed...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
        '        Base._DepositSlipsDBOps.MarkAsPrinted(dTable.Rows(0)("REC_ID").ToString)
        '        xPromptWindow.Hide()
        '    End If
        'End If
        Me.DialogResult = DialogResult.OK
        Me.Hide()

    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Hide_Properties()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Other Events"

    Private Sub RadioGroup1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Txt_Dep_Date.DateTime = DateTime.Now
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = DialogResult.None
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.BE_Slip_No)
    End Sub

#End Region

End Class