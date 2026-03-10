Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_Live_Stock_Window

#Region "Start--> Default Variables"
    Public Tr_M_ID As String = ""
    Public xLeft As Integer
    Public xTop As Integer
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
        Me.Left = xLeft : Me.Top = xTop
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub

#End Region

#Region "Start--> Button Events"
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
            End If
            Return (True)
        End If
        
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click
        Hide_Properties()

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If


            If Len(Trim(Me.Txt_Name.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N a m e   c a n n o t   b e   B l a n k . . . !", Me.Txt_Name, 0, Me.Txt_Name.Height, 5000)
                Me.Txt_Name.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Name)
            End If

            If Me.Cmd_Year.Text.Length > 0 Then
                If Val(Cmd_Year.Text) < 1950 Then
                    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                    Me.ToolTip1.Show("E n t e r   P r o p e r   B i r t h   Y e a r . . . !", Me.Cmd_Year, 0, Me.Cmd_Year.Height, 5000)
                    Me.Cmd_Year.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_Year)
                End If
                'If Val(Cmd_Year.Text) > Base._open_Year_Sdt.Year Then
                '    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                '    Me.ToolTip1.Show("B i r t h   Y e a r   m u s t   b e   E a r l i e r   t h a n   S t a r t   F i n a n c i a l   Y e a r . . . !", Me.Cmd_Year, 0, Me.Cmd_Year.Height, 5000)
                '    Me.Cmd_Year.Focus()
                '    Me.DialogResult = Windows.Forms.DialogResult.None
                '    Exit Sub
                'Else
                '    Me.ToolTip1.Hide(Me.Cmd_Year)
                'End If
            End If

            If Rad_Insurance.SelectedIndex = 0 Then
                If Me.Look_InsList.Tag.Trim.Length <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("I n s u r a n c e   C o m p a n y   N o t   S e l e c t e d . . . !", Me.Look_InsList, 0, Me.Look_InsList.Height, 5000)
                    Me.Look_InsList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_InsList)
                End If
                If Len(Trim(Me.Txt_PolicyNo.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("P o l i c y   N o .   c a n n o t   b e   B l a n k . . . !", Me.Txt_PolicyNo, 0, Me.Txt_PolicyNo.Height, 5000)
                    Me.Txt_PolicyNo.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_PolicyNo)
                End If
                If Val(Trim(Me.Txt_InsAmt.EditValue)) < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("I n s u r a n c e   A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_InsAmt, 0, Me.Txt_InsAmt.Height, 5000)
                    Me.Txt_InsAmt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_InsAmt)
                End If
                If IsDate(Me.Txt_I_Date.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_I_Date, 0, Me.Txt_I_Date.Height, 5000)
                    Me.Txt_I_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_I_Date)
                End If
                If IsDate(Me.Txt_I_Date.Text) = True Then
                    Dim diff2 As Double = DateDiff(DateInterval.Day, Now.Date, DateValue(Me.Txt_I_Date.Text))
                    If diff2 <= 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                        Me.ToolTip1.Show("D a t e   m u s t   b e   h i g h e r   t h a n   T o d a y ' s . . . !", Me.Txt_I_Date, 0, Me.Txt_I_Date.Height, 5000)
                        Me.Txt_I_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_I_Date)
                    End If


                End If
            End If

            If Len(Trim(Me.Look_LocList.Tag)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("L o c a t i o n   N o t   S e l e c t e d . . . !", Me.Look_LocList, 0, Me.Look_LocList.Height, 5000)
                Me.Look_LocList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_LocList)
            End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()

       
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
       
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
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

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Others.GotFocus, Txt_Name.GotFocus, Txt_PolicyNo.GotFocus, Txt_InsAmt.GotFocus, Txt_I_Date.GotFocus, Txt_InsAmt.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_InsAmt.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If

    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Others.KeyPress, Txt_Name.KeyPress, Txt_PolicyNo.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Name.KeyDown, Txt_PolicyNo.KeyDown, Txt_InsAmt.KeyDown, Txt_I_Date.KeyDown, Txt_Others.KeyDown
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

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Name.Validated, Txt_PolicyNo.Validated, Txt_Others.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Year.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_Year.KeyDown
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

    Private Sub Rad_Insurance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Rad_Insurance.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Rad_Insurance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rad_Insurance.SelectedIndexChanged
        If Rad_Insurance.SelectedIndex = 0 Then
            Me.Look_InsList.Enabled = True
            Me.Look_InsList.EditValue = ""
            Me.Txt_PolicyNo.Enabled = True : Me.Txt_PolicyNo.Text = ""
            Me.Txt_InsAmt.Enabled = True : Me.Txt_InsAmt.Text = ""
            Me.Txt_I_Date.Enabled = True : Me.Txt_I_Date.EditValue = ""
        Else
            Me.Look_InsList.Enabled = False
            Me.Look_InsList.EditValue = ""
            Me.Txt_PolicyNo.Enabled = False : Me.Txt_PolicyNo.Text = ""
            Me.Txt_InsAmt.Enabled = False : Me.Txt_InsAmt.Text = ""
            Me.Txt_I_Date.Enabled = False : Me.Txt_I_Date.EditValue = ""
        End If
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_I_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TitleX.Text = "Livestock"
        For i As Integer = Base._open_Year_Sdt.Year To 1950 Step -1 : Me.Cmd_Year.Properties.Items.Add(i) : Next : Me.Cmd_Year.Properties.Items.Add(" ")
       
        Me.Look_InsList.Tag = "" : Look_LocList.Tag = ""
        LookUp_GetInsList() : LookUp_GetLocList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.Txt_Name.Focus()
    End Sub
    Public LS_LOC_AL_ID As String : Public LS_INS_ID As String
    Public LS_INSURANCE, LS_INSURANCE_ID, LS_INS_POLICY_NO, LS_BIRTH_YEAR As String : Public LS_INS_DATE As String : Public LS_INS_AMT As Double
    Private Sub Data_Binding()

        Rad_Insurance.EditValue = LS_INSURANCE
        Cmd_Year.EditValue = LS_BIRTH_YEAR

        Look_InsList.EditValue = LS_INS_ID
        Look_InsList.Tag = Look_InsList.EditValue
        Look_InsList.Properties.Tag = "SHOW"

        Txt_PolicyNo.Text = LS_INS_POLICY_NO
        Txt_InsAmt.Text = LS_INS_AMT


        Dim xDate As DateTime = Nothing
        If IsDate(LS_INS_DATE) Then
            xDate = LS_INS_DATE : Txt_I_Date.DateTime = xDate
        End If

        If LS_LOC_AL_ID.Length > 0 Then
            Look_LocList.EditValue = LS_LOC_AL_ID
            Look_LocList.Tag = Look_LocList.EditValue
            Look_LocList.Properties.Tag = "SHOW"
        End If

    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.BE_ItemName.Enabled = False : Me.BE_ItemName.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Name.Enabled = False : Me.Txt_Name.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Year.Enabled = False : Me.Cmd_Year.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Rad_Insurance.Enabled = False : Me.Rad_Insurance.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_InsList.Enabled = False : Me.Look_InsList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_PolicyNo.Enabled = False : Me.Txt_PolicyNo.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_InsAmt.Enabled = False : Me.Txt_InsAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_I_Date.Enabled = False : Me.Txt_I_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Others.Enabled = False : Me.Txt_Others.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_LocList.Enabled = False : Me.Look_LocList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_Loc.Enabled = False
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Txt_Name)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '2.Insurance Company List
    Private Sub Look_InsList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_InsList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_InsList.CancelPopup()
            Hide_Properties()
            Me.Txt_Others.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Others.Focus()
        End If
    End Sub
    Private Sub Look_InsList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_InsList.EditValueChanged
        If Me.Look_InsList.Properties.Tag = "SHOW" Then
            Me.Look_InsList.Tag = Me.Look_InsList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetInsList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetInsuranceItems("Name", "ID")
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
            'Me.Look_InsList.Properties.BestFit()
            Me.Look_InsList.Properties.PopupWidth = 400
            Me.Look_InsList.Properties.Columns(1).Visible = False
            Me.Look_InsList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_InsList.EditValue = 0
            Me.Look_InsList.Properties.Tag = "SHOW"
        Else
            Me.Look_InsList.Properties.Tag = "NONE"
        End If
    End Sub

    Private Sub But_Loc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Loc.Click
        Dim xfrm As New D0009.Frm_Location_Window
        xfrm.Text = "New ~ Location" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm._Base = Base
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
            xfrm.Dispose()
            LookUp_GetLocList()
            Me.Look_LocList.EditValue = "" : Me.Look_LocList.Tag = "" : Look_LocList.Properties.Tag = "SHOW"
        Else
            xfrm.Dispose()
        End If
    End Sub
    Private Sub Look_LocList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_LocList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_LocList.CancelPopup()
            Hide_Properties()
            If Rad_Insurance.SelectedIndex = 0 Then Me.Txt_I_Date.Focus() Else Rad_Insurance.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            If Rad_Insurance.SelectedIndex = 0 Then Me.Txt_I_Date.Focus() Else Rad_Insurance.Focus()
        End If

    End Sub
    Private Sub Look_LocList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_LocList.EditValueChanged
        If Me.Look_LocList.Properties.Tag = "SHOW" Then
            Me.Look_LocList.Tag = Me.Look_LocList.GetColumnValue("AL_ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetLocList()

        Dim d2 As DataTable = Base._Gift_DBOps.GetAssetLocations(Tr_M_ID)
        If d2 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d2)
        If dview.Count > 0 Then
            Me.Look_LocList.Properties.ValueMember = "AL_ID"
            Me.Look_LocList.Properties.DisplayMember = "Location Name"
            Me.Look_LocList.Properties.DataSource = dview
            Me.Look_LocList.Properties.PopulateColumns()
            'Me.Look_LocList.Properties.BestFit()
            Me.Look_LocList.Properties.PopupWidth = Me.Look_LocList.Width
            Me.Look_LocList.Properties.Columns(1).Visible = False
            Me.Look_LocList.Properties.Columns(2).Visible = False
            Me.Look_LocList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_LocList.EditValue = 0
            Me.Look_LocList.Properties.Tag = "SHOW"
        Else
            Me.Look_LocList.Properties.Tag = "NONE"
        End If
        If dview.Count = 1 Then
            Me.Look_LocList.EditValue = dview.Item(0).Row("AL_ID").ToString
            Me.Look_LocList.Enabled = False
        Else
            Me.Look_LocList.Enabled = True
        End If
        If dview.Count > 1 Then
            LayoutControlItem2.AppearanceItemCaption.ForeColor = Color.Red
        Else
            LayoutControlItem2.AppearanceItemCaption.ForeColor = Color.Black
        End If
    End Sub
#End Region

  
End Class