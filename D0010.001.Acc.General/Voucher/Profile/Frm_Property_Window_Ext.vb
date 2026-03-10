Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_Property_Window_Ext


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

    Private Sub Form_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Hide_Properties()
    End Sub
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub

#End Region

#Region "Start--> Button Events"
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then

            If Len(Trim(Me.Look_InsList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I n s t i t u t e   N o t   S e l e c t e d . . . !", Me.Look_InsList, 0, Me.Look_InsList.Height, 5000)
                Me.Look_InsList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_InsList)
            End If

            If Val(Trim(Me.Txt_Ext_Tot_Area.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T o t a l   P l o t   A r e a   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Ext_Tot_Area, 0, Me.Txt_Ext_Tot_Area.Height, 5000)
                Me.Txt_Ext_Tot_Area.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ext_Tot_Area)
            End If

            If Val(Trim(Me.Txt_Ext_Con_Area.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("C o n s t r u c t i o n   A r e a   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Ext_Con_Area, 0, Me.Txt_Ext_Con_Area.Height, 5000)
                Me.Txt_Ext_Con_Area.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ext_Con_Area)
            End If
            If Me.Cmd_Ext_Con_Year.Text.Length > 0 Then
                If Val(Cmd_Ext_Con_Year.Text) > Base._open_Year_Sdt.Year Then
                    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                    Me.ToolTip1.Show("C o n s t r u c t i o n   Y e a r   m u s t   b e   L e s s   t h a n   /   E q u a l   t o   S t a r t   F i n a n c i a l   Y e a r . . . !", Me.Cmd_Ext_Con_Year, 0, Me.Cmd_Ext_Con_Year.Height, 5000)
                    Me.Cmd_Ext_Con_Year.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_Ext_Con_Year)
                End If
            End If
            If IsDate(Me.Txt_MOU_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_MOU_Date, 0, Me.Txt_MOU_Date.Height, 5000)
                Me.Txt_MOU_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_MOU_Date)
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Hide_Properties()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> TextBox Events"

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Others.GotFocus, Txt_Ext_Tot_Area.GotFocus, Txt_Ext_Con_Area.GotFocus, Txt_Ext_Con_Area.Click, Txt_Ext_Tot_Area.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Ext_Con_Area.Name Or txt.Name = Txt_Ext_Tot_Area.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If

    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Others.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Others.KeyDown, Txt_Ext_Tot_Area.KeyDown, Txt_Ext_Con_Area.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()
        If UCase(txt.Name) = "TXT_SEARCH" Then
            'If e.KeyCode = Keys.Enter Then DGrid1.Focus()
            'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Others.Validated, Txt_Ext_Tot_Area.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        If e.KeyCode = Keys.Escape And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.PageUp And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageUp And txt.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

#End Region

#Region "Start--> Procedures"
    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Extended Property"
        For i As Integer = Base._open_Year_Sdt.Year To 1900 Step -1 : Me.Cmd_Ext_Con_Year.Properties.Items.Add(i) : Next : Me.Cmd_Ext_Con_Year.Properties.Items.Add(" ")
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.BUT_SAVE_COM.Visible = False
            Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Properties(False, True)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Look_InsList.Tag = ""
        LookUp_GetInsList()
        Me.Look_InsList.Focus()
    End Sub
    
    Private Sub Set_Properties(ByVal Clear As Boolean, ByVal Set_ReadOnly As Boolean)
        Me.Txt_Others.Properties.Tag = 0
        Me.Txt_Ext_Tot_Area.Properties.Tag = 0
        If Clear Then
            Me.Txt_Ext_Tot_Area.Text = ""
            Me.Txt_Others.Text = ""
        End If
        If Set_ReadOnly Then
            Me.Look_InsList.Properties.ReadOnly = True
            Me.Txt_Others.Properties.ReadOnly = True
            Me.Txt_Ext_Tot_Area.Properties.ReadOnly = True
        Else
            Me.Look_InsList.Properties.ReadOnly = False
            Me.Txt_Others.Properties.ReadOnly = False
            Me.Txt_Ext_Tot_Area.Properties.ReadOnly = False
        End If
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Look_InsList)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.Look_InsList
    Private Sub Look_InsList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_InsList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_InsList.CancelPopup()
            Hide_Properties()
            'Me.Txt_No.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.List_Lang.Focus()
        End If

    End Sub
    Private Sub Look_InsList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_InsList.EditValueChanged
        If Me.Look_InsList.Properties.Tag = "SHOW" Then
            Me.Look_InsList.Tag = Me.Look_InsList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetInsList()
        Dim d1 As DataTable = Base._L_B_Voucher_DBOps.GetInstt()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.Look_InsList.Properties.ValueMember = "ID"
            Me.Look_InsList.Properties.DisplayMember = "Name"
            Me.Look_InsList.Properties.DataSource = dview
            Me.Look_InsList.Properties.PopulateColumns()
            'Me.Look_ItemList.Properties.BestFit()
            'Me.Look_ItemList.Properties.PopupWidth = 280
            Me.Look_InsList.Properties.Columns(2).Visible = False
            Me.Look_InsList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_InsList.EditValue = 0
            Me.Look_InsList.Properties.Tag = "SHOW"
        Else
            Me.Look_InsList.Properties.Tag = "NONE"
        End If
    End Sub

#End Region


End Class