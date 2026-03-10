Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_Change_Period

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public Set_Type, Set_Text As String
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
        Base = MainBase
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Me.Txt_Fr_Date.DateTime = xFr_Date
        Me.Txt_To_Date.DateTime = xTo_Date
    End Sub
    Private Sub Frm_Login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

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
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
       
        If IsDate(Me.Txt_Fr_Date.Text) = False Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Fr_Date, 0, Me.Txt_Fr_Date.Height, 5000)
            Me.Txt_Fr_Date.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Txt_Fr_Date)
        End If
        If IsDate(Me.Txt_To_Date.Text) = False Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_To_Date, 0, Me.Txt_To_Date.Height, 5000)
            Me.Txt_To_Date.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Txt_To_Date)
        End If
        If IsDate(Me.Txt_Fr_Date.Text) = True And IsDate(Me.Txt_To_Date.Text) = True Then
            Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Fr_Date.Text), DateValue(Me.Txt_To_Date.Text))
            If diff < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F r o m   D a t e   c a n n o t   b e   H i g h e r   T h a n   T o   D a t e . . . !", Me.Txt_Fr_Date, 0, Me.Txt_Fr_Date.Height, 5000)
                Me.Txt_Fr_Date.Focus()
                Me.DialogResult = DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Fr_Date)
            End If
        End If
        If IsDate(Me.Txt_Fr_Date.Text) = True Then
            Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_Fr_Date.Text))
            If diff < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F r o m   D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_Fr_Date, 0, Me.Txt_Fr_Date.Height, 5000)
                Me.Txt_Fr_Date.Focus()
                Me.DialogResult = DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Fr_Date)
            End If
        End If
        If IsDate(Me.Txt_To_Date.Text) = True Then
            Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_To_Date.Text))
            If diff > 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T o   D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_To_Date, 0, Me.Txt_To_Date.Height, 5000)
                Me.Txt_To_Date.Focus()
                Me.DialogResult = DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_To_Date)
            End If
        End If

        xFr_Date = Me.Txt_Fr_Date.DateTime : xTo_Date = Me.Txt_To_Date.DateTime

        Me.DialogResult = DialogResult.OK
    End Sub

#End Region

#Region "Start--> Text Events"

    Public xFr_Date As DateTime = Nothing : Public xTo_Date As DateTime = Nothing

    Private Sub Txt_Fr_Date_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Fr_Date.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Txt_To_Date_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_To_Date.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Fdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Tdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Txt_To_Date_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_To_Date.Leave
        If Not IsDate(Me.Txt_To_Date.Text) Then
            If IsDate(Me.Txt_Fr_Date.Text) Then
                Dim XDATE As Date = New Date(Me.Txt_Fr_Date.DateTime.Year, Me.Txt_Fr_Date.DateTime.Month, 1) : XDATE = DateAdd(DateInterval.Month, 1, XDATE) : XDATE = DateAdd(DateInterval.Day, -1, XDATE)
                Me.Txt_To_Date.DateTime = XDATE
            End If
        End If
        'Dim i As Integer : i = DateAdd(DateInterval.Day, -1, DateSerial(2011, 4 + 1, 1)).Day
    End Sub
    Private Sub Txt_Fr_Date_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fr_Date.Leave
        If Not IsDate(Me.Txt_Fr_Date.Text) Then
            Dim XDATE As Date = New Date(Now.Year, Now.Month, 1)
            Me.Txt_Fr_Date.DateTime = XDATE
        End If
    End Sub

#End Region

End Class
