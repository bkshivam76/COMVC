Imports System.Runtime.InteropServices

Public Class Prompt_Window

    'For Enable Hotkey -------------------------|
    <DllImport("user32.dll")> Private Shared Sub SystemParametersInfo(ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByRef pvParam As Integer, ByVal fWinIni As UInteger)
    End Sub
    'Constants used for User32 calls. 
    Const SPI_SETKEYBOARDCUES As UInteger = &H100B
    Private Shared Sub Enable_Alt_HotKey()
        Dim pv As Integer = 1
        ' Call to systemparametersinfo to set true of pv variable.
        SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, pv, 0)
        'Set pvParam to TRUE to always underline menu access keys, 
    End Sub
    '-------------------------------------------|

    Public Enum WindowSize
        _399x140
        _499x180
        _599x220
        _699x260
        _799x300
        _899x340
        _999x380
    End Enum
    Public Enum ButtonType
        _Information
        _Exclamation
        _Question
        _Critical
        _Error
    End Enum
    Public SetButtonType As ButtonType
    Public Enum FocusButton
        _Button1
        _Button2
    End Enum
    Public SetFocusButton As FocusButton
    Public FirstButtonText As String = ""
    Public SecondtButtonText As String = ""
    Public isDisposed As Boolean = False
    Private Sub Frm_Prompt_Window_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Me.DialogResult = System.Windows.Forms.DialogResult.None
    End Sub
    Private Sub BUT_SAVE_Click(sender As System.Object, e As System.EventArgs) Handles BUT_SAVE.Click
        If SetButtonType = ButtonType._Question Then Me.DialogResult = System.Windows.Forms.DialogResult.Yes Else Me.DialogResult = DialogResult.OK
    End Sub
    Private Sub BUT_CANCEL_Click(sender As System.Object, e As System.EventArgs) Handles BUT_CANCEL.Click
        If SetButtonType = ButtonType._Question Then Me.DialogResult = System.Windows.Forms.DialogResult.No Else Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub
    Public Overloads Function ShowDialog(ByVal SetTitle As String, ByVal SetMessage As String, ByVal SetButton As ButtonType, ByVal SetWindowSize As WindowSize, ByVal SetFocusOn As FocusButton, ByVal SetBackColor As System.Drawing.Color, Optional FirstButtonText As String = "", Optional SecondButtonText As String = "") As DialogResult
        MdiParent = Me.ParentForm
        Me.CancelButton = Me.BUT_CANCEL
        Me.Text = SetTitle
        Me.lbl_Message.Text = SetMessage.Trim
        SetButtonType = SetButton
        SetFocusButton = SetFocusOn
        Me.Appearance.BackColor = SetBackColor
        Me.FirstButtonText = FirstButtonText
        Me.SecondtButtonText = SecondButtonText
        Define_Button(SetButton)
        Define_WindowSize(SetWindowSize)
        Me.ShowDialog()
        Me.Close()
        Return Me.DialogResult
    End Function
    Private Sub Define_Button(ByVal SetButton As ButtonType)
        Select Case SetButton
            Case ButtonType._Information
                Me.PictureBox1.Image = My.Resources.Info : Me.BUT_SAVE.Visible = False : Me.BUT_SAVE.Text = "&Close"
                If FirstButtonText.Length > 0 Then Me.BUT_SAVE.Text = FirstButtonText
                If SecondtButtonText.Length > 0 Then Me.BUT_CANCEL.Text = SecondtButtonText
            Case ButtonType._Exclamation
                Me.PictureBox1.Image = My.Resources.Exclamation : Me.BUT_SAVE.Visible = False : Me.BUT_SAVE.Text = "&Close" : Me.BUT_CANCEL.Text = "&OK"
                If FirstButtonText.Length > 0 Then Me.BUT_SAVE.Text = FirstButtonText
                If SecondtButtonText.Length > 0 Then Me.BUT_CANCEL.Text = SecondtButtonText
            Case ButtonType._Question
                Me.PictureBox1.Image = My.Resources.Question : Me.BUT_SAVE.Text = "&Yes" : Me.BUT_CANCEL.Text = "&No"
                If FirstButtonText.Length > 0 Then Me.BUT_SAVE.Text = FirstButtonText
                If SecondtButtonText.Length > 0 Then Me.BUT_CANCEL.Text = SecondtButtonText
            Case ButtonType._Critical
                Me.PictureBox1.Image = My.Resources.Critical : Me.BUT_SAVE.Visible = False : Me.BUT_SAVE.Text = "&Close"
                If FirstButtonText.Length > 0 Then Me.BUT_SAVE.Text = FirstButtonText
                If SecondtButtonText.Length > 0 Then Me.BUT_CANCEL.Text = SecondtButtonText
            Case ButtonType._Error
                Me.PictureBox1.Image = My.Resources._Error : Me.BUT_SAVE.Visible = False : Me.BUT_SAVE.Text = "&Close"
                If FirstButtonText.Length > 0 Then Me.BUT_SAVE.Text = FirstButtonText
                If SecondtButtonText.Length > 0 Then Me.BUT_CANCEL.Text = SecondtButtonText
        End Select
    End Sub
    Private Sub Define_WindowSize(ByVal SetWindowSize As WindowSize)
        Select Case SetWindowSize
            Case WindowSize._399x140
                Me.Size = New Size(399, 140)
            Case WindowSize._499x180
                Me.Size = New Size(499, 180)
            Case WindowSize._599x220
                Me.Size = New Size(599, 220)
            Case WindowSize._699x260
                Me.Size = New Size(699, 260)
            Case WindowSize._799x300
                Me.Size = New Size(799, 300)
            Case WindowSize._899x340
                Me.Size = New Size(899, 340)
            Case WindowSize._999x380
                Me.Size = New Size(999, 380)
        End Select
    End Sub
    Private Sub Prompt_Window_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If SetFocusButton = FocusButton._Button1 Then BUT_SAVE.Focus() : BUT_SAVE.TabIndex = 0
        If SetFocusButton = FocusButton._Button2 Then BUT_CANCEL.Focus() : BUT_CANCEL.TabIndex = 0
        If lbl_Message.Height < 530 Then Me.Height = lbl_Message.Height + 70
        If lbl_Message.Height < 750 Then Me.Width = lbl_Message.Width + 50
    End Sub

End Class