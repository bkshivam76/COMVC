Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_Voucher_Win_C_Box_D

#Region "Start--> Default Variables"
   
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
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
    End Sub

#End Region

#Region "Start--> Button Events"

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
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "Start--> Text Events"

    Private Sub Text_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_1000.GotFocus, Txt_500.GotFocus, Txt_100.GotFocus, Txt_50.GotFocus, Txt_20.GotFocus, Txt_10.GotFocus, Txt_5.GotFocus, Txt_2.GotFocus, Txt_1.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        txt.SelectAll()
    End Sub

    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_1000.KeyDown, Txt_500.KeyDown, Txt_100.KeyDown, Txt_50.KeyDown, Txt_20.KeyDown, Txt_10.KeyDown, Txt_5.KeyDown, Txt_2.KeyDown, Txt_1.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)

        If UCase(txt.Name) = "TXT_SEARCH" Then
            'If e.KeyCode = Keys.Enter Then DGrid1.Focus()
            'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub Txt_1000_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_1000.EditValueChanged, Txt_500.EditValueChanged, Txt_100.EditValueChanged, Txt_50.EditValueChanged, Txt_20.EditValueChanged, Txt_10.EditValueChanged, Txt_5.EditValueChanged, Txt_2.EditValueChanged, Txt_1.EditValueChanged
        Me.BE_1000.Text = Format((Val(Me.Txt_1000.Text) * 1000), "#,0.00")
        Me.BE_500.Text = Format((Val(Me.Txt_500.Text) * 500), "#,0.00")
        Me.BE_100.Text = Format((Val(Me.Txt_100.Text) * 100), "#,0.00")
        Me.BE_50.Text = Format((Val(Me.Txt_50.Text) * 50), "#,0.00")
        Me.BE_20.Text = Format((Val(Me.Txt_20.Text) * 20), "#,0.00")
        Me.BE_10.Text = Format((Val(Me.Txt_10.Text) * 10), "#,0.00")
        Me.BE_5.Text = Format((Val(Me.Txt_5.Text) * 5), "#,0.00")
        Me.BE_2.Text = Format((Val(Me.Txt_2.Text) * 2), "#,0.00")
        Me.BE_1.Text = Format((Val(Me.Txt_1.Text) * 1), "#,0.00")
    End Sub

    Private Sub Amount_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BE_1000.EditValueChanged, BE_500.EditValueChanged, BE_100.EditValueChanged, BE_50.EditValueChanged, BE_20.EditValueChanged, BE_10.EditValueChanged, BE_5.EditValueChanged, BE_2.EditValueChanged, BE_1.EditValueChanged
        Me.Txt_Total.Text = Format((Val(Me.Txt_1000.Text) * 1000) + (Val(Me.Txt_500.Text) * 500) + (Val(Me.Txt_100.Text) * 100) + (Val(Me.Txt_50.Text) * 50) + (Val(Me.Txt_20.Text) * 20) + (Val(Me.Txt_10.Text) * 10) + (Val(Me.Txt_5.Text) * 5) + (Val(Me.Txt_2.Text) * 2) + (Val(Me.Txt_1.Text) * 1), "#,0.00")
        Me.Txt_Total.Tag = Format((Val(Me.Txt_1000.Text) * 1000) + (Val(Me.Txt_500.Text) * 500) + (Val(Me.Txt_100.Text) * 100) + (Val(Me.Txt_50.Text) * 50) + (Val(Me.Txt_20.Text) * 20) + (Val(Me.Txt_10.Text) * 10) + (Val(Me.Txt_5.Text) * 5) + (Val(Me.Txt_2.Text) * 2) + (Val(Me.Txt_1.Text) * 1), "#0.00")
    End Sub

#End Region

End Class
