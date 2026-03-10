Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_Asset_Window

#Region "Start--> Default Variables"
    Public Tr_M_ID As String = ""
    Public xLeft As Integer
    Public xTop As Integer
    Public IsGift As Boolean = False
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

            If Val(Trim(Me.Txt_Warranty.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("W a r r a n t y   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Warranty, 0, Me.Txt_Warranty.Height, 5000)
                Me.Txt_Warranty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Warranty)
            End If

            If Val(Trim(Me.Txt_Amt.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o   /   N e g a t i v e . . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                Me.Txt_Amt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amt)
            End If
            '#6312 fix
            If Val(Trim(Me.Txt_Qty.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Q u a n t i t y   c a n n o t   b e   Z e r o   /   N e g a t i v e . . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amt)
            End If

            If IsDate(Me.Txt_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                Me.Txt_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Date)
            End If

            If IsDate(Me.Txt_Date.Text) = True And Not IsGift Then
                If IsDate(Me.Txt_Date.Text) = True Then
                    '1
                    Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_Date.Text))
                    If diff < 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                        Me.Txt_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Date)
                    End If
                    '2
                    diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_Date.Text))
                    If diff > 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_Date, 0, Me.Txt_Date.Height, 5000)
                        Me.Txt_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Date)
                    End If
                End If
            End If


            If Len(Trim(Me.Look_LocList.Tag)) = 0 Or Len(Trim(Me.Look_LocList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("L o c a t i o n   N o t   S e l e c t e d . . . !", Me.Look_LocList, 0, Me.Look_LocList.Height, 5000)
                Me.Look_LocList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_LocList)
            End If

            If Len(Me.Txt_Make.Text.Trim) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("M a k e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Make, 0, Me.Txt_Make.Height, 5000)
                Me.Txt_Make.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Make)
            End If

            If Len(Me.Txt_Model.Text.Trim) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("M o d e l   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Model, 0, Me.Txt_Model.Height, 5000)
                Me.Txt_Model.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Model)
            End If

            If Val(Me.Txt_Qty.Text) <= 0 Then Me.Txt_Qty.Text = 1
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

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Others.GotFocus, Txt_Model.GotFocus, Txt_Make.GotFocus, Txt_Serial.GotFocus, Txt_Qty.GotFocus, Txt_Warranty.GotFocus, Txt_Date.GotFocus, Txt_Amt.GotFocus, Txt_Amt.Click, Txt_Warranty.Click, Txt_Qty.Click, Txt_Rate.GotFocus, Txt_Rate.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amt.Name Or txt.Name = Txt_Qty.Name Or txt.Name = Txt_Warranty.Name Or txt.Name = Txt_Rate.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Others.KeyPress, Txt_Make.KeyPress, Txt_Model.KeyPress, Txt_Serial.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Make.KeyDown, Txt_Model.KeyDown, Txt_Serial.KeyDown, Txt_Qty.KeyDown, Txt_Warranty.KeyDown, Txt_Date.KeyDown, Txt_Others.KeyDown, Txt_Amt.KeyDown, Txt_Rate.KeyDown
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

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Make.Validated, Txt_Model.Validated, Txt_Serial.Validated, Txt_Others.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TitleX.Text = "Movable Asset"

        Me.Look_LocList.Tag = "" : LookUp_GetLocList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        If IsGift Then
            Me.LayoutControlItem2.Text = "Insurance Amount :"
        End If
        Me.Txt_Amt.Focus()
    End Sub
    Public AI_LOC_AL_ID, AI_PUR_DATE As String
    Private Sub Data_Binding()
        Look_LocList.EditValue = AI_LOC_AL_ID
        Look_LocList.Tag = Look_LocList.EditValue
        Look_LocList.Properties.Tag = "SHOW"

        If IsDate(AI_PUR_DATE) Then
            Dim xDate As DateTime = Nothing
            xDate = AI_PUR_DATE : Txt_Date.DateTime = xDate
        End If
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.BE_ItemName.Enabled = False : Me.BE_ItemName.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Make.Enabled = False : Me.Txt_Make.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Model.Enabled = False : Me.Txt_Model.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Rate.Enabled = False : Me.Txt_Rate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amt.Enabled = False : Me.Txt_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Warranty.Enabled = False : Me.Txt_Warranty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Serial.Enabled = False : Me.Txt_Serial.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Date.Enabled = False : Me.Txt_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Others.Enabled = False : Me.Txt_Others.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_LocList.Enabled = False : Me.Look_LocList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_Loc.Enabled = False
    End Sub

    
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Txt_Amt)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

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
            Me.Txt_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Date.Focus()
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
            LayoutControlItem1.AppearanceItemCaption.ForeColor = Color.Red
        Else
            LayoutControlItem1.AppearanceItemCaption.ForeColor = Color.Black
        End If
    End Sub

#End Region

End Class