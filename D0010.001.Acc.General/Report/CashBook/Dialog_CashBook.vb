Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Dialog_CashBook

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
        Me.Cmb_Type.SelectedIndex = Val(Set_Type)
        If Val(Set_Type) = 0 Then 'Monthly
            Me.Cmb_Monthly.Text = Set_Text
        End If
        If Val(Set_Type) = 1 Then 'Quarterly
            Me.Cmb_Quarterly.Text = Set_Text
        End If
        If Val(Set_Type) = 2 Then 'Period
            Me.Txt_Fr_Date.DateTime = xFr_Date : Me.Txt_To_Date.DateTime = xTo_Date
        End If
        If Val(Set_Type) = 3 Then 'yearly
            xFr_Date = Base._open_Year_Sdt : xTo_Date = Base._open_Year_Edt
            Fdate.Text = Format(xFr_Date, "dd/MM/yyyy") : Tdate.Text = Format(xTo_Date, "dd/MM/yyyy")
        End If
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
        If Cmb_Type.SelectedIndex = 0 Then 'Monthly
            xView = Me.Cmb_Monthly.Text
        End If
        If Cmb_Type.SelectedIndex = 1 Then 'Quarterly
            xView = Me.Cmb_Quarterly.Text
        End If
        If Me.Cmb_Type.SelectedIndex = 2 Then 'Period
            xView = Cmb_Type.Text
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
        End If
        If Me.Cmb_Type.SelectedIndex = 3 Then 'yearly
            xView = Cmb_Type.Text
            xFr_Date = Base._open_Year_Sdt : xTo_Date = Base._open_Year_Edt
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

#End Region

#Region "Start--> View Date Setting"

    Public xView As String = Nothing : Public xFr_Date As DateTime = Nothing : Public xTo_Date As DateTime = Nothing

    Private Sub Cmb_Type_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmb_Type.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Me.Cmb_Type.SelectedIndex = 0 Then 'Monthly
                Me.Type_TabControl.SelectedTabPage = Page_Monthly : Me.Cmb_Monthly.Focus()
            End If
            If Me.Cmb_Type.SelectedIndex = 1 Then 'Quarterly
                Me.Type_TabControl.SelectedTabPage = Page_Quarterly : Me.Cmb_Quarterly.Focus()
            End If
            If Me.Cmb_Type.SelectedIndex = 2 Then 'Period
                Me.Type_TabControl.SelectedTabPage = Page_Period : Me.Txt_Fr_Date.Focus()
            End If
            If Me.Cmb_Type.SelectedIndex = 3 Then 'yearly
                Me.Type_TabControl.SelectedTabPage = Page_Year : Me.Fdate.Focus()
            End If

        End If
    End Sub
    Private Sub Cmb_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Type.SelectedIndexChanged
        If Me.Cmb_Type.SelectedIndex = 0 Then 'Monthly
            Me.Type_TabControl.Visible = True
            Me.Cmb_Monthly.Properties.Items.Clear()
            For I As Integer = Base._open_Year_Sdt.Month To 12 : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_Monthly.Properties.Items.Add(xMonth & "-" & Base._open_Year_Sdt.Year) : Next
            For I As Integer = 1 To Base._open_Year_Edt.Month : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_Monthly.Properties.Items.Add(xMonth & "-" & Base._open_Year_Edt.Year) : Next
            Dim xMM As Integer = Now.Month
            Me.Cmb_Monthly.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
            Cmb_Monthly_SelectedIndexChanged(Nothing, Nothing)
            Me.Type_TabControl.SelectedTabPage = Page_Monthly : Me.Cmb_Monthly.Focus()
        End If

        If Me.Cmb_Type.SelectedIndex = 1 Then 'Quarterly
            Me.Type_TabControl.Visible = True
            Me.Cmb_Quarterly.SelectedIndex = 0
            Cmb_Quarterly_SelectedIndexChanged(Nothing, Nothing)
            Me.Type_TabControl.SelectedTabPage = Page_Quarterly : Me.Cmb_Quarterly.Focus()
        End If

        If Me.Cmb_Type.SelectedIndex = 2 Then 'Period
            Me.Type_TabControl.Visible = True
            Me.Type_TabControl.SelectedTabPage = Page_Period : Me.Txt_Fr_Date.Focus()
        End If

        If Me.Cmb_Type.SelectedIndex = 3 Then 'yearly
            Me.Type_TabControl.Visible = True
            xFr_Date = Base._open_Year_Sdt : xTo_Date = Base._open_Year_Edt
            Fdate.Text = Format(xFr_Date, "dd/MM/yyyy") : Tdate.Text = Format(xTo_Date, "dd/MM/yyyy")
            Me.Type_TabControl.SelectedTabPage = Page_Year
        End If
    End Sub

    Private Sub Cmb_Monthly_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmb_Monthly.KeyDown
        If e.KeyCode = Keys.PageUp Then Me.Cmb_Type.Focus()
    End Sub
    Private Sub Cmb_Monthly_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Monthly.SelectedIndexChanged
        Dim Sel_Mon As String = Me.Cmb_Monthly.Text.Substring(0, 3).ToUpper
        Dim SEL_MM As Integer = IIf(Sel_Mon = "JAN", 1, IIf(Sel_Mon = "FEB", 2, IIf(Sel_Mon = "MAR", 3, IIf(Sel_Mon = "APR", 4, IIf(Sel_Mon = "MAY", 5, IIf(Sel_Mon = "JUN", 6, IIf(Sel_Mon = "JUL", 7, IIf(Sel_Mon = "AUG", 8, IIf(Sel_Mon = "SEP", 9, IIf(Sel_Mon = "OCT", 10, IIf(Sel_Mon = "NOV", 11, IIf(Sel_Mon = "DEC", 12, 4))))))))))))
        xFr_Date = New Date(Me.Cmb_Monthly.Text.Substring(4, 4), SEL_MM, 1)
        xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, xFr_Date))
    End Sub

    Private Sub Cmb_Quarterly_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmb_Quarterly.KeyDown
        If e.KeyCode = Keys.PageUp Then Me.Cmb_Type.Focus()
    End Sub
    Private Sub Cmb_Quarterly_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Quarterly.SelectedIndexChanged
        Dim xMM As Integer = 0
        If Cmb_Quarterly.SelectedIndex < 3 Then
            If Cmb_Quarterly.SelectedIndex = 0 Then xMM = 4
            If Cmb_Quarterly.SelectedIndex = 1 Then xMM = 7
            If Cmb_Quarterly.SelectedIndex = 2 Then xMM = 10
            xFr_Date = New Date(Base._open_Year_Sdt.Year, xMM, 1)
            xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        Else
            xMM = 1
            xFr_Date = New Date(Base._open_Year_Edt.Year, xMM, 1)
            xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        End If
    End Sub


    Private Sub Txt_Fr_Date_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Fr_Date.KeyDown
        If e.KeyCode = Keys.PageUp Then Me.Cmb_Type.Focus()
    End Sub
    Private Sub Txt_To_Date_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_To_Date.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Fdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Fdate.KeyDown
        If e.KeyCode = Keys.PageUp Then Me.Cmb_Type.Focus()
    End Sub
    Private Sub Tdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Tdate.KeyDown
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
