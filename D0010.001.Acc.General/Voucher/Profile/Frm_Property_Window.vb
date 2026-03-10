Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_Property_Window

#Region "Start--> Default Variables"

    Public IsGift As Boolean = False
    Public IsJV As Boolean = False
    Private Enum LB_Category
        PURCHASED
        PURCHASED_CONSTRUCTED
        GIFTED
        GIFTED_CONSTRUCTED
        LEASED_Long_Term
        LEASED_CONSTRUCTED_Long_Term
        LEASED
    End Enum

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
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.O)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.D)) Then 'delete
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                BUT_SAVE_Click(BUT_DEL, Nothing)
            End If
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
            Me.xID.Text = Guid.NewGuid().ToString()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            'If Base.IsInsuranceAudited() Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
            '    Exit Sub
            'End If

            If Len(Trim(Me.Cmd_PCategory.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P r o p e r t y   C a t e g o r y   N o t   S e l e c t e d . . . !", Me.Cmd_PCategory, 0, Me.Cmd_PCategory.Height, 5000)
                Me.Cmd_PCategory.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_PCategory)
            End If

            If Len(Trim(Me.Cmd_PType.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P r o p e r t y   T y p e   N o t   S e l e c t e d . . . !", Me.Cmd_PType, 0, Me.Cmd_PType.Height, 5000)
                Me.Cmd_PType.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_PType)
            End If

            If Len(Trim(Me.Cmd_PUse.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("U s e   o f   P r o p e r t y   N o t   S e l e c t e d . . . !", Me.Cmd_PUse, 0, Me.Cmd_PUse.Height, 5000)
                Me.Cmd_PUse.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_PUse)
            End If

            If Len(Trim(Me.Txt_PName.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P r o p e r t y   /  B u i l d i n g   N a m e   c a n n o t   b e   B l a n k . . . !", Me.Txt_PName, 0, Me.Txt_PName.Height, 5000)
                Me.Txt_PName.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_PName)
            End If

            If Len(Trim(Me.Txt_Add.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P r o p e r t y   /  B u i l d i n g   A d d r e s s   c a n n o t   b e   B l a n k . . . !", Me.Txt_Add, 0, Me.Txt_Add.Height, 5000)
                Me.Txt_Add.Focus()
                Me.Txt_Add.ClosePopup()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Add)
            End If
            If Not AddressChecks() Then '#6029 fix
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Len(Trim(Me.Cmd_Ownership.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("O w n e r s h i p   N o t   S e l e c t e d . . . !", Me.Cmd_Ownership, 0, Me.Cmd_Ownership.Height, 5000)
                Me.Cmd_Ownership.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_Ownership)
            End If

            If Me.Cmd_Ownership.SelectedIndex = 1 Then 'free
                If Len(Trim(Me.Look_OwnList.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("O w n e r   N a m e   N o t   S e l e c t e d . . . !", Me.Look_OwnList, 0, Me.Look_OwnList.Height, 5000)
                    Me.Look_OwnList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_OwnList)
                End If
            End If

            If Len(Trim(Me.Txt_SNo.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S u r v e y  N o   c a n n o t   b e   B l a n k . . . !", Me.Txt_SNo, 0, Me.Txt_SNo.Height, 5000)
                Me.Txt_SNo.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_SNo)
            End If

            If Val(Trim(Me.Txt_Tot_Area.Text)) <= 0 And Not Cmd_PType.Text.ToUpper.Contains("FLAT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T o t a l   P l o t   A r e a   c a n n o t   b e   Z e r o  o r  N e g a t i v e. . . !", Me.Txt_Tot_Area, 0, Me.Txt_Tot_Area.Height, 5000)
                Me.Txt_Tot_Area.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Tot_Area)
            End If

            If Val(Trim(Me.Txt_Tot_Area.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T o t a l   P l o t   A r e a   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Tot_Area, 0, Me.Txt_Tot_Area.Height, 5000)
                Me.Txt_Tot_Area.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Tot_Area)
            End If

            If Val(Trim(Me.Txt_Con_Area.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("C o n s t r u c t i o n   A r e a   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Con_Area, 0, Me.Txt_Con_Area.Height, 5000)
                Me.Txt_Con_Area.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Con_Area)
            End If

            If Cmd_PType.Text.ToUpper <> "LAND" Then
                If Val(Trim(Me.Txt_Con_Area.Text)) <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("T o t a l   C o n s t r u c t e d   A r e a   c a n n o t   b e   Z e r o  o r  N e g a t i v e. . . !", Me.Txt_Con_Area, 0, Me.Txt_Con_Area.Height, 5000)
                    Me.Txt_Con_Area.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Con_Area)
                End If

                If Me.Cmd_Con_Year.Text.Trim.Length <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("C o n s t r u c t i o n   Y e a r   n o t   S e l e c t e d. . . !", Me.Cmd_Con_Year, 0, Me.Cmd_Con_Year.Height, 5000)
                    Me.Cmd_Con_Year.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_Con_Year)
                End If

                If Me.Cmd_RccType.Text.Trim.Length <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("R C C   R o o f   C o n s t r u c t i o n   N o t   S e l e c t e d . . . !", Me.Cmd_RccType, 0, Me.Cmd_RccType.Height, 5000)
                    Me.Cmd_RccType.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_RccType)
                End If
            End If

            If Val(Trim(Me.Txt_Dep_Amt.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Dep_Amt, 0, Me.Txt_Dep_Amt.Height, 5000)
                Me.Txt_Dep_Amt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Dep_Amt)
            End If
            If Val(Trim(Me.Txt_Mon_Rent.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Mon_Rent, 0, Me.Txt_Mon_Rent.Height, 5000)
                Me.Txt_Mon_Rent.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Mon_Rent)
            End If
            If Val(Trim(Me.Txt_Other_Payments.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Other_Payments, 0, Me.Txt_Other_Payments.Height, 5000)
                Me.Txt_Other_Payments.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Other_Payments)
            End If
            If Me.Cmd_Con_Year.Text.Length > 0 Then
                If Val(Cmd_Con_Year.Text) > Base._open_Year_Edt.Year Then
                    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                    Me.ToolTip1.Show("C o n s t r u c t i o n   Y e a r   m u s t   b e   L e s s   t h a n   /   E q u a l   t o   C u r r e n t   F i n a n c i a l   Y e a r . . . !", Me.Cmd_Con_Year, 0, Me.Cmd_Con_Year.Height, 5000)
                    Me.Cmd_Con_Year.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_Con_Year)
                End If
            End If

            If (Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString)) And Not Cmd_PType.Text.ToUpper.Contains("LAND") Then
                If Len(Trim(Me.Cmd_RccType.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("R C C   C o n s t r u c t e d   R o o f   N o t   S e l e c t e d . . . !", Me.Cmd_RccType, 0, Me.Cmd_RccType.Height, 5000)
                    Me.Cmd_RccType.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Cmd_RccType)
                End If
            End If
            'If IsDate(Me.Txt_PaidDate.Text) = True Then
            '    If Me.Txt_PaidDate.DateTime >= Base._open_Year_Sdt Then
            '        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
            '        Me.ToolTip1.Show("D a t e   m u s t   b e   E a r l i e r   t h a n   S t a r t   F i n a n c i a l   Y e a r . . . !", Me.Txt_PaidDate, 0, Me.Txt_PaidDate.Height, 5000)
            '        XtraTabControl1.SelectedTabPage = TPage_Rent
            '        Me.Txt_PaidDate.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.Txt_PaidDate)
            '    End If
            'End If
            'If IsDate(Me.Txt_F_Date.Text) = True Then
            '    If Me.Txt_F_Date.DateTime >= Base._open_Year_Sdt Then
            '        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
            '        Me.ToolTip1.Show("D a t e   m u s t   b e   E a r l i e r   t h a n   S t a r t   F i n a n c i a l   Y e a r . . . !", Me.Txt_F_Date, 0, Me.Txt_F_Date.Height, 5000)
            '        XtraTabControl1.SelectedTabPage = TPage_Rent
            '        Me.Txt_F_Date.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.Txt_F_Date)
            '    End If
            'End If
            If IsDate(Me.Txt_F_Date.Text) = True And IsDate(Me.Txt_T_Date.Text) = True Then
                If Me.Txt_F_Date.DateTime >= Me.Txt_T_Date.DateTime Then
                    Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                    Me.ToolTip1.Show("D a t e   m u s t   b e   H i g h e r   t h a n   F r o m   D a t e . . . !", Me.Txt_T_Date, 0, Me.Txt_T_Date.Height, 5000)
                    XtraTabControl1.SelectedTabPage = TPage_Rent
                    Me.Txt_T_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_T_Date)
                End If

            End If
            If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) And Cmd_PCategory.Text.ToUpper.Contains("LONG") Then 'lease longterm
                If IsDate(Me.Txt_F_Date.Text) = True And IsDate(Me.Txt_T_Date.Text) = True Then
                    Dim diff As Double = DateDiff(DateInterval.Day, Txt_F_Date.DateTime, Txt_T_Date.DateTime)
                    If diff < 3650 Then
                        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                        Me.ToolTip1.Show("L e a s e d   ( L o n g   T e r m )   P e r i o d   c a n n o t   b e   L e s s   t h a n   1 0   Y e a r s . . . !", Me.Txt_T_Date, 0, Me.Txt_T_Date.Height, 5000)
                        XtraTabControl1.SelectedTabPage = TPage_Rent
                        Me.Txt_T_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    End If

                End If
            End If

            Dim query As String = ""
            Dim MainCenters As DataTable = Nothing
            'check duplicate as main center
            If Me.Cmd_PUse.SelectedIndex = 0 Then
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then MainCenters = Base._L_B_DBOps.GetMainCenters()
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then MainCenters = Base._L_B_DBOps.GetMainCenters(Me.xID.Text)
                If MainCenters Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If

                If MainCenters.Rows.Count > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Duplicate Information . . ."
                    Me.ToolTip1.Show("M a i n   C e n t r e (" & MainCenters(0)("LB_PRO_NAME").ToString() & ")  a l r e a d y   C r e a t e d   i n  " & MainCenters(0)("CEN_UID").ToString() & "   i n   y e a r  " & MainCenters(0)("YEAR_ID").ToString() & ". . . ! ", Me.Cmd_PUse, 0, Me.Cmd_PUse.Height, 5000)
                    Me.Cmd_PUse.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            End If

            'check duplicates in name 

            Dim MaxValue As Object = 0
            MaxValue = Base._L_B_Voucher_DBOps.CheckDuplicatePropertyName(Me.Tag, Me.xID.Text, Txt_PName.Text)
            If MaxValue Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If MaxValue > 0 Then
                Me.ToolTip1.ToolTipTitle = "Duplicate Information . . ."
                Me.ToolTip1.Show("P r o p e r t y   w i t h   s a m e   n a m e   a l r e a d y   C r e a t e d . . . ! ", Me.Txt_PName, 0, Me.Txt_PName.Height, 5000)
                Me.Txt_PName.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            'Checking Duplicate Location Name ....
            Dim MaxValue_Loc As Object = 0
            MaxValue_Loc = Base._AssetLocDBOps.GetRecordCountByName(Me.Txt_PName.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment, Base._open_PAD_No_Main)
            If MaxValue_Loc Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If MaxValue_Loc <> 0 And Me.Tag = Common_Lib.Common.Navigation_Mode._New Then
                Me.ToolTip1.ToolTipTitle = "Duplicate. . . (" & Me.Txt_PName.Text & ")"
                Me.ToolTip1.Show("L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e . . . !", Me.Txt_PName, 0, Me.Txt_PName.Height, 5000)
                Me.Txt_PName.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
                'ElseIf MaxValue_Loc <> 0 And Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                '    If (Trim(UCase(Me.Txt_PName.Text)) <> Trim(UCase(Me.Txt_PName.Tag))) Then
                '        Me.ToolTip1.ToolTipTitle = "Duplicate. . . (" & Me.Txt_PName.Text & ")"
                '        Me.ToolTip1.Show("L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e . . . !" & vbNewLine & vbNewLine & "--> Edit Property: " & Me.Txt_PName.Tag, Me.Txt_PName, 0, Me.Txt_PName.Height, 5000)
                '        Me.Txt_PName.Focus()
                '        Me.DialogResult = Windows.Forms.DialogResult.None
                '        Exit Sub
                '    End If
            Else
                Me.ToolTip1.Hide(Me.Txt_PName)
            End If

            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim LocNames As DataTable = Base._L_B_DBOps.GetPendingTfs_LocNames(Base._open_Cen_Rec_ID)
                If Not LocNames Is Nothing Then
                    If LocNames.Rows.Count > 0 Then
                        If Me.Txt_PName.Text.ToString.Length > 0 Then
                            For I = 0 To LocNames.Rows.Count - 1
                                If Me.Txt_PName.Text.ToUpper = LocNames.Rows(I)(0).ToString.ToUpper Then
                                    Me.ToolTip1.ToolTipTitle = "Duplicate. . . (" & Me.Txt_PName.Text & ")"
                                    Me.ToolTip1.Show("L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   E x i s t s   I n   P e n d i n g   T r a n s f e r s  . . . !", Me.Txt_PName, 0, Me.Txt_PName.Height, 5000)
                                    Me.Txt_PName.Focus()
                                    Me.DialogResult = Windows.Forms.DialogResult.None
                                    Exit Sub
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

        If Len(Trim(Me.Cmd_RccType.Text)) = 0 Then Me.Cmd_RccType.Text = "NO"

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, BUT_NEW.GotFocus, BUT_EDIT.GotFocus, BUT_DELETE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, BUT_NEW.LostFocus, BUT_EDIT.LostFocus, BUT_DELETE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, BUT_NEW.KeyDown, BUT_EDIT.KeyDown, BUT_DELETE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click, BUT_DELETE.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
        End If

    End Sub
#End Region

#Region "Start--> TextBox Events"

    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    '0-PURCHASED
    '1-GIFTED
    '2-RENTED
    '3-LEASED (Short Term)
    '4-LEASED (Long Term)
    '5-MORTGAGE (Short Term)
    '6-MORTGAGE (Long Term)
    '7-FREE USE

    Private Sub XtraTabControl1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabControl1.GotFocus
        If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Then
            XtraTabControl1.SelectedTabPage = TPage_ExtPro : Me.GridView1.Focus() : Me.GridView1.FocusedRowHandle = 0
        ElseIf Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) Then
            XtraTabControl1.SelectedTabPage = TPage_Rent : Me.Txt_Dep_Amt.Focus()
        Else
            XtraTabControl1.SelectedTabPage = TPage_Doc : Me.Chk_DocList.Focus()
        End If
    End Sub
    Private Sub Txt_PName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_PName.Leave
        Me.Txt_PName.Text = StrConv(Me.Txt_PName.Text, vbProperCase)
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Other_Payments.GotFocus, Txt_Dep_Amt.GotFocus, Txt_Mon_Rent.GotFocus, Txt_Con_Area.GotFocus, Txt_Tot_Area.GotFocus, Txt_SNo.GotFocus, Txt_Dep_Amt.Click, Txt_Mon_Rent.Click, Txt_Other_Payments.Click, Txt_Tot_Area.Click, Txt_Con_Area.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Dep_Amt.Name Or txt.Name = Txt_Mon_Rent.Name Or txt.Name = Txt_Other_Payments.Name Or txt.Name = Txt_Tot_Area.Name Or txt.Name = Txt_Con_Area.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If

    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_OtherDoc.KeyPress, Txt_PName.KeyPress, Txt_Remarks.KeyPress, Txt_SNo.KeyPress, Txt_R_Add1.KeyPress, Txt_R_Add2.KeyPress, Txt_R_Add3.KeyPress, Txt_R_Add4.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Other_Payments.KeyDown, Txt_Dep_Amt.KeyDown, Txt_Tot_Area.KeyDown, Txt_Con_Area.KeyDown, Txt_SNo.KeyDown, Txt_Mon_Rent.KeyDown, Txt_PaidDate.KeyDown, Txt_T_Date.KeyDown, Txt_F_Date.KeyDown, Txt_Remarks.KeyDown, Txt_OtherDoc.KeyDown, Txt_PName.KeyDown, Txt_Address.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()
        If txt.Name = Txt_Tot_Area.Name Then
            'If e.KeyCode = Keys.Enter Then DGrid1.Focus()
            'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
            If e.KeyCode = Keys.Enter And Cmd_PType.Text.ToUpper = "LAND" Then
                e.SuppressKeyPress = True
                If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Then
                    XtraTabControl1.SelectedTabPage = TPage_ExtPro : Me.GridView1.Focus() : Me.GridView1.FocusedRowHandle = 0
                ElseIf Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) Then
                    XtraTabControl1.SelectedTabPage = TPage_Rent : Me.Txt_Dep_Amt.Focus()
                End If
            End If
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")

        ElseIf txt.Name = Txt_T_Date.Name Then
            If e.KeyCode = Keys.Enter And Cmd_PType.Text.ToUpper = "LAND" Then
                e.SuppressKeyPress = True
                XtraTabControl1.SelectedTabPage = TPage_Doc : Me.Chk_DocList.Focus()
            End If
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        ElseIf txt.Name = Txt_Dep_Amt.Name Then
            If e.KeyCode = Keys.PageUp Then
                e.SuppressKeyPress = True
                If Cmd_PType.Text.ToUpper = "LAND" Then
                    Me.Txt_Tot_Area.Focus()
                Else
                    Me.Cmd_RccType.Focus()
                End If
            End If
        ElseIf txt.Name = Txt_Remarks.Name Then
            If e.KeyCode = Keys.PageUp Then
                e.SuppressKeyPress = True
                XtraTabControl1.SelectedTabPage = TPage_Doc
                If Chk_OtherDoc.Checked Then
                    Txt_OtherDoc.Focus()
                Else
                    Chk_OtherDoc.Focus()
                End If
            End If
        ElseIf txt.Name = Txt_OtherDoc.Name Then
            If e.KeyCode = Keys.PageUp Then
                e.SuppressKeyPress = True
                Me.Chk_OtherDoc.Focus()
            End If

        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_OtherDoc.Validated, Txt_Remarks.Validated, Txt_PName.Validated, Txt_Address.Validated, Txt_SNo.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub Cmd_PUse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_PUse.SelectedIndexChanged
        If Cmd_PUse.SelectedIndex = 0 Then
            Dim CenterData As DataTable = Base._L_B_Voucher_DBOps.GetMainCenterAdd()
            If CenterData Is Nothing Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If CenterData.Rows.Count > 0 Then  ': Txt_Address.Text = CenterData.Rows(0)("CEN_ADD1").ToString & vbCrLf & CenterData.Rows(0)("CEN_ADD2").ToString & vbCrLf & CenterData.Rows(0)("CEN_ADD3").ToString & vbCrLf & CenterData.Rows(0)("CEN_ADD4").ToString & vbCrLf & CenterData.Rows(0)("CEN_CITY").ToString & ", " & CenterData.Rows(0)("CEN_STATE").ToString & " - " & CenterData.Rows(0)("CEN_COUNTRY").ToString
                'Me.Txt_Add.Text = Me.Txt_Address.Text
                Me.Txt_PName.Text = CenterData.Rows(0)("CEN_B_NAME").ToString
                Txt_R_Add1.Text = CenterData.Rows(0)("CEN_ADD1").ToString() : Txt_R_Add1.Properties.ReadOnly = True
                Txt_R_Add2.Text = CenterData.Rows(0)("CEN_ADD2").ToString() : Txt_R_Add2.Properties.ReadOnly = True
                Txt_R_Add3.Text = CenterData.Rows(0)("CEN_ADD3").ToString() : Txt_R_Add3.Properties.ReadOnly = True
                Txt_R_Add4.Text = CenterData.Rows(0)("CEN_ADD4").ToString() : Txt_R_Add4.Properties.ReadOnly = True
                If Not IsDBNull(CenterData.Rows(0)("CEN_STATE_ID")) Then
                    If CenterData.Rows(0)("CEN_STATE_ID").ToString.Length > 0 Then
                        Me.GLookUp_StateList.ShowPopup() : Me.GLookUp_StateList.ClosePopup()
                        Me.GLookUp_StateListView.FocusedRowHandle = Me.GLookUp_StateListView.LocateByValue("R_ST_REC_ID", CenterData.Rows(0)("CEN_STATE_ID"))
                        Me.GLookUp_StateList.EditValue = CenterData.Rows(0)("CEN_STATE_ID")
                        Me.GLookUp_StateList.Tag = Me.GLookUp_StateList.EditValue
                        Me.GLookUp_StateList.Properties.Tag = "SHOW"
                    End If
                End If : GLookUp_StateList.Properties.ReadOnly = True
                If Not IsDBNull(CenterData.Rows(0)("CEN_DISTRICT_ID")) Then
                    If CenterData.Rows(0)("CEN_DISTRICT_ID").ToString.Length > 0 Then
                        Me.GLookUp_DistrictList.ShowPopup() : Me.GLookUp_DistrictList.ClosePopup()
                        Me.GLookUp_DistrictListView.FocusedRowHandle = Me.GLookUp_DistrictListView.LocateByValue("R_DI_REC_ID", CenterData.Rows(0)("CEN_DISTRICT_ID"))
                        Me.GLookUp_DistrictList.EditValue = CenterData.Rows(0)("CEN_DISTRICT_ID")
                        Me.GLookUp_DistrictList.Tag = Me.GLookUp_DistrictList.EditValue
                        Me.GLookUp_DistrictList.Properties.Tag = "SHOW"
                    End If
                End If : GLookUp_DistrictList.Properties.ReadOnly = True
                If Not IsDBNull(CenterData.Rows(0)("CEN_CITY_ID")) Then
                    If CenterData.Rows(0)("CEN_CITY_ID").ToString.Length > 0 Then
                        Me.GLookUp_CityList.ShowPopup() : Me.GLookUp_CityList.ClosePopup()
                        Me.GLookUp_CityListView.FocusedRowHandle = Me.GLookUp_CityListView.LocateByValue("R_CI_REC_ID", CenterData.Rows(0)("CEN_CITY_ID"))
                        Me.GLookUp_CityList.EditValue = CenterData.Rows(0)("CEN_CITY_ID")
                        Me.GLookUp_CityList.Tag = Me.GLookUp_CityList.EditValue
                        Me.GLookUp_CityList.Properties.Tag = "SHOW"
                    End If
                End If : GLookUp_CityList.Properties.ReadOnly = True
                Txt_R_Pincode.Text = CenterData.Rows(0)("CEN_PINCODE").ToString() : Txt_R_Pincode.Properties.ReadOnly = True
            End If
            ' Me.PopupContainerEdit1.Text = Me.Txt_Address.Text
            If Txt_R_Add1.Text.Length > 0 Then
                Me.Txt_Add.Text = Me.Txt_R_Add1.Text +
                    IIf(Len(Trim(Me.Txt_R_Add2.Text)) > 0, ", " + Me.Txt_R_Add2.Text, "") +
                    IIf(Len(Trim(Me.Txt_R_Add3.Text)) > 0, ", " + Me.Txt_R_Add3.Text, "") +
                    IIf(Len(Trim(Me.Txt_R_Add4.Text)) > 0, ", " + Me.Txt_R_Add4.Text, "") + ", " + Me.GLookUp_CityList.Text.ToUpper + ", Dist. " + Me.GLookUp_DistrictList.Text + ", " + Me.GLookUp_StateList.Text + "-" + Me.Txt_R_Pincode.Text
            End If
        Else
            Me.Txt_Add.Text = ""
            Me.Txt_PName.Text = ""
            Me.Txt_Address.Text = ""
            Txt_R_Add1.Text = "" : Txt_R_Add1.Properties.ReadOnly = False
            Txt_R_Add2.Text = "" : Txt_R_Add2.Properties.ReadOnly = False
            Txt_R_Add3.Text = "" : Txt_R_Add3.Properties.ReadOnly = False
            Txt_R_Add4.Text = "" : Txt_R_Add4.Properties.ReadOnly = False
            GLookUp_StateList.EditValue = "" : GLookUp_StateList.Properties.ReadOnly = False
            GLookUp_DistrictList.EditValue = "" : GLookUp_DistrictList.Properties.ReadOnly = False
            GLookUp_CityList.EditValue = "" : GLookUp_CityList.Properties.ReadOnly = False
            Txt_R_Pincode.Text = "" : Txt_R_Pincode.Properties.ReadOnly = False
        End If
    End Sub

    Private Sub Cmd_EditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_PCategory.GotFocus, Cmd_Ownership.GotFocus, Cmd_PType.GotFocus, Cmd_PUse.GotFocus, Cmd_RccType.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub Cmd_EditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_PCategory.KeyDown, Cmd_Ownership.KeyDown, Cmd_PType.KeyDown, Cmd_PUse.KeyDown, Cmd_RccType.KeyDown, Cmd_Con_Year.KeyDown
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

        If txt.Name = Cmd_RccType.Name And e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Then
                XtraTabControl1.SelectedTabPage = TPage_ExtPro : Me.GridView1.Focus() : Me.GridView1.FocusedRowHandle = 0
            ElseIf Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) Then
                XtraTabControl1.SelectedTabPage = TPage_Rent : Me.Txt_Dep_Amt.Focus()
            End If
        End If

    End Sub
    Private Sub Cmd_PType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_PType.SelectedIndexChanged
        If Cmd_PType.Text.ToUpper = "LAND" Then
            Me.Txt_Con_Area.EditValue = "" : Me.Txt_Con_Area.Enabled = False
            Me.Cmd_Con_Year.EditValue = "" : Me.Cmd_Con_Year.Enabled = False
            Me.Cmd_RccType.EditValue = "NO" : Me.Cmd_RccType.Enabled = False
        Else
            Me.Cmd_RccType.EditValue = "" : Me.Cmd_RccType.Enabled = True
            Me.Txt_Con_Area.EditValue = "" : Me.Txt_Con_Area.Enabled = True
            Me.Cmd_Con_Year.EditValue = "" : Me.Cmd_Con_Year.Enabled = True
        End If
        If (Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString)) And Cmd_PType.Text.ToUpper <> "LAND" Then
            Me.LayoutControlItem13.AppearanceItemCaption.ForeColor = Color.Red
        Else
            Me.LayoutControlItem13.AppearanceItemCaption.ForeColor = Color.Black
        End If

        If Cmd_PType.Text.ToUpper = "LAND" Then
            LayoutControlItem8.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem20.AppearanceItemCaption.ForeColor = Color.Black
            LayoutControlItem10.AppearanceItemCaption.ForeColor = Color.Black
            LayoutControlItem13.AppearanceItemCaption.ForeColor = Color.Black
        ElseIf Cmd_PType.Text.ToUpper = "BUILDING" Then
            LayoutControlItem8.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem20.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem10.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem13.AppearanceItemCaption.ForeColor = Color.Red
        Else
            LayoutControlItem8.AppearanceItemCaption.ForeColor = Color.Black
            LayoutControlItem20.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem10.AppearanceItemCaption.ForeColor = Color.Red
            LayoutControlItem13.AppearanceItemCaption.ForeColor = Color.Red
        End If
    End Sub
    Private Sub Cmd_PCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_PCategory.SelectedIndexChanged
        If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Then
            TPage_ExtPro.PageVisible = True : TPage_Rent.PageVisible = False
        ElseIf Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) Then
            TPage_ExtPro.PageVisible = True : TPage_Rent.PageVisible = True
            TPage_Rent.Text = StrConv(Cmd_PCategory.Text, vbProperCase)
        End If

        If Cmd_PCategory.Text.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.Contains(LB_Category.GIFTED.ToString) Then
            Me.Cmd_Ownership.SelectedIndex = 0
        Else
            Me.Cmd_Ownership.SelectedIndex = 1
        End If
        Cmd_PType_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub Cmd_Ownership_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Ownership.SelectedIndexChanged
        If Cmd_Ownership.SelectedIndex = 0 Then
            Me.Look_OwnList.Visible = False : Me.Look_OwnList.Enabled = False : Me.But_Owner.Enabled = False : Me.Look_OwnList.EditValue = ""
        Else
            Me.Look_OwnList.Visible = True : Me.Look_OwnList.Enabled = True : Me.But_Owner.Enabled = True : Me.Look_OwnList.EditValue = ""
        End If
    End Sub

    Private Sub Chk_DocList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_DocList.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Chk_OtherDoc.Focus()
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            If Cmd_PCategory.Text.ToUpper.Contains(LB_Category.PURCHASED.ToString) Or Cmd_PCategory.Text.ToUpper.Contains(LB_Category.GIFTED.ToString) Then
                XtraTabControl1.SelectedTabPage = TPage_ExtPro : Me.GridView1.Focus() : Me.GridView1.FocusedRowHandle = 0
            ElseIf Cmd_PCategory.Text.ToUpper.Contains(LB_Category.LEASED.ToString) Then
                XtraTabControl1.SelectedTabPage = TPage_Rent : Me.Txt_Dep_Amt.Focus()
            End If
        End If

    End Sub
    Private Sub Chk_OtherDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_OtherDoc.CheckedChanged
        If Chk_OtherDoc.Checked Then
            Chk_OtherDoc.Tag = "YES" : Txt_OtherDoc.Enabled = True : Txt_OtherDoc.Text = "" : Txt_OtherDoc.Focus()
        Else
            Chk_OtherDoc.Tag = "NO" : Txt_OtherDoc.Enabled = False : Txt_OtherDoc.Text = ""
        End If
    End Sub
    Private Sub Chk_OtherDoc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_OtherDoc.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Chk_OtherDoc.Checked Then
                Txt_OtherDoc.Focus()
            Else
                Txt_Remarks.Focus()
            End If
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Chk_DocList.Focus()
        End If
    End Sub

    Private Sub PopupContainerEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Add.Click
        Txt_Add.ShowPopup()
    End Sub
    Private Sub PopupContainerEdit1_Closed(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles Txt_Add.Closed
        Me.Txt_Add.Text = Me.Txt_Address.Text
        If Txt_R_Add1.Text.Length > 0 Then
            Me.Txt_Add.Text = Me.Txt_R_Add1.Text +
                IIf(Len(Trim(Me.Txt_R_Add2.Text)) > 0, ", " + Me.Txt_R_Add2.Text, "") +
                IIf(Len(Trim(Me.Txt_R_Add3.Text)) > 0, ", " + Me.Txt_R_Add3.Text, "") +
                IIf(Len(Trim(Me.Txt_R_Add4.Text)) > 0, ", " + Me.Txt_R_Add4.Text, "") + ", " + Me.GLookUp_CityList.Text.ToUpper + ", Dist. " + Me.GLookUp_DistrictList.Text + ", " + Me.GLookUp_StateList.Text + "-" + Me.Txt_R_Pincode.Text
        End If
    End Sub
    Private Sub PopupContainerEdit1_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Add.Popup
        Me.Txt_Add.Text = ""
    End Sub
    Private Sub PopupContainerEdit1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Add.GotFocus
        If Txt_Add.IsPopupOpen = False Then Txt_Add.ShowPopup()
    End Sub
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Me.Txt_Add.ClosePopup()
        If Not AddressChecks() Then
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If
    End Sub
#End Region

#Region "Start--> Procedures"
    Public LB_PRO_TYPE, LB_PRO_CATEGORY, LB_PRO_USE, LB_PRO_NAME, LB_PRO_ADDRESS, LB_ADDRESS1, LB_ADDRESS2, LB_ADDRESS3, LB_ADDRESS4, LB_COUNTRY_ID, LB_STATE_ID, LB_DISTRICT_ID, LB_CITY_ID, LB_PINCODE, LB_OWNERSHIP, LB_OWNERSHIP_PARTY_ID, LB_SURVEY_NO, LB_CON_YEAR, LB_RCC_ROOF, LB_PAID_DATE, LB_PERIOD_FROM, LB_PERIOD_TO, LB_DOC_OTHERS, LB_DOC_NAME, LB_OTHER_DETAIL, LB_REC_ID, ITEM_ID As String
    Public LB_TOT_P_AREA, LB_CON_AREA, LB_DEPOSIT_AMT, LB_MONTH_RENT, LB_MONTH_O_PAYMENTS As Double
    Public LB_DOCS_ARRAY, LB_EXTENDED_PROPERTY_TABLE As DataTable
    Private Sub SetDefault()
        xPleaseWait.Show("Land && Building" & vbNewLine & vbNewLine & "L o a d i n g . . . !")

        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_F_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_T_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_PaidDate.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TitleX.Text = "Land && Building" : Me.TextEdit1.Text = Base._open_Ins_Name
        Me.SubTitleX.Text = ""
        GLookUp_StateList.Tag = "" : LookUp_Get_StateList()
        GLookUp_CityList.Tag = ""
        GLookUp_DistrictList.Tag = ""
        Me.Txt_Add.ShowPopup() : Me.Txt_Add.ClosePopup()

        'Space(5) & "As on " & Format(DateAdd(DateInterval.Day, -1, Base._open_Year_Sdt), "dd MMMM, yyyy")
        ''Me.SubTitleX.Left = Me.TitleX.Left + Me.TitleX.Width + 4
        For i As Integer = Base._open_Year_Edt.Year To 1900 Step -1 : Me.Cmd_Con_Year.Properties.Items.Add(i) : Next : Me.Cmd_Con_Year.Properties.Items.Add(" ")
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
            'Dim d1 As New Common_Lib.Get_Data(Base, "CORE", "CENTRE_INFO", "SELECT CEN_B_NAME  from CENTRE_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = '" & Base._open_Cen_ID_Main & "'") : d1._dc_Connection.Close()
            'If Not IsDBNull(d1._dc_DataTable.Rows(0)("CEN_B_NAME")) Then Me.Txt_PName.Text = d1._dc_DataTable.Rows(0)("CEN_B_NAME") Else Me.Txt_PName.Text = ""
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Properties(False, True)
        End If
        If IsGift Then
            Cmd_PCategory.Properties.Items.RemoveAt(0)
            Cmd_PCategory.Properties.Items.RemoveAt(0)
            Cmd_PCategory.Properties.Items.RemoveAt(0)
            Cmd_PCategory.Properties.Items.RemoveAt(0)
        ElseIf Not IsJV Then
            Cmd_PCategory.Properties.Items.RemoveAt(4)
            Cmd_PCategory.Properties.Items.RemoveAt(4)
        End If
        LookUp_GetOwnList() : Look_OwnList.Tag = ""
        Get_Documents_List()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If

        '----For Prepare Grid Columns for Extended Property
        SetGridData()
        Me.Cmd_PCategory.Focus()
        If ITEM_ID = "f8f4b6c3-f340-4e99-892b-16ebf33c8c28" Then 'Land Purchase
            Cmd_PType.Properties.Items.RemoveAt(1)
            Cmd_PType.Properties.Items.RemoveAt(1)
            Cmd_PType.Properties.Items.RemoveAt(1)
            Cmd_PType.SelectedItem = Cmd_PType.Properties.Items(0)
        ElseIf ITEM_ID = "c4d9b556-6a36-41f5-8f56-245d2c6e0be4" Then 'Building Purchase
            Cmd_PType.Properties.Items.RemoveAt(0)
            Cmd_PType.Properties.Items.RemoveAt(1)
            Cmd_PType.Properties.Items.RemoveAt(1)
            Cmd_PType.SelectedItem = Cmd_PType.Properties.Items(0)
        ElseIf ITEM_ID = "e769d245-0299-4737-9b74-55cc2147f389" Then 'Flat Purchase
            Cmd_PType.Properties.Items.RemoveAt(0)
            Cmd_PType.Properties.Items.RemoveAt(0)
            Cmd_PType.Properties.Items.RemoveAt(1)
        ElseIf ITEM_ID = "f90dfc01-7c18-447e-8331-3eae64288087" Then 'Flat (in Multiple Floor)
            Cmd_PType.Properties.Items.RemoveAt(0)
            Cmd_PType.Properties.Items.RemoveAt(0)
            Cmd_PType.Properties.Items.RemoveAt(0)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        Dim d1_Ext As DataTable = LB_EXTENDED_PROPERTY_TABLE
        Dim d1_Doc As DataTable = LB_DOCS_ARRAY
        Cmd_PType.Text = LB_PRO_TYPE
        Cmd_PCategory.Text = LB_PRO_CATEGORY
        Cmd_PUse.Text = LB_PRO_USE

        Txt_PName.Text = LB_PRO_NAME
        Txt_PName.Tag = LB_PRO_NAME
        If Not IsDBNull(LB_PRO_ADDRESS) Then
            Txt_Address.Text = LB_PRO_ADDRESS
            Txt_Add.Text = LB_PRO_ADDRESS
        End If

        ' If Not Address = "" Then Me.Txt_Add.Text = Address
        'If Not IsDBNull(d1.Rows(0)("LB_ADDRESS1")) Then
        If LB_ADDRESS1.Length > 0 Then
            Txt_R_Add1.Text = LB_ADDRESS1
            Txt_R_Add2.Text = LB_ADDRESS2
            Txt_R_Add3.Text = LB_ADDRESS3
            Txt_R_Add4.Text = LB_ADDRESS4
            'If Not IsDBNull(d1.Rows(0)("LB_STATE_ID")) Then
            If LB_STATE_ID.Length > 0 Then
                Me.GLookUp_StateList.ShowPopup() : Me.GLookUp_StateList.ClosePopup()
                Me.GLookUp_StateListView.FocusedRowHandle = Me.GLookUp_StateListView.LocateByValue("R_ST_REC_ID", LB_STATE_ID)
                Me.GLookUp_StateList.EditValue = LB_STATE_ID
                Me.GLookUp_StateList.Tag = Me.GLookUp_StateList.EditValue
                Me.GLookUp_StateList.Properties.Tag = "SHOW"
            End If
            'End If :
            Me.GLookUp_StateList.Properties.ReadOnly = False
            'If Not IsDBNull(d1.Rows(0)("LB_DISTRICT_ID")) Then
            If LB_DISTRICT_ID.Length > 0 Then
                Me.GLookUp_DistrictList.ShowPopup() : Me.GLookUp_DistrictList.ClosePopup()
                Me.GLookUp_DistrictListView.FocusedRowHandle = Me.GLookUp_DistrictListView.LocateByValue("R_DI_REC_ID", LB_DISTRICT_ID)
                Me.GLookUp_DistrictList.EditValue = LB_DISTRICT_ID
                Me.GLookUp_DistrictList.Tag = Me.GLookUp_DistrictList.EditValue
                Me.GLookUp_DistrictList.Properties.Tag = "SHOW"
            End If
            'End If : 
            Me.GLookUp_DistrictList.Properties.ReadOnly = False
            'If Not IsDBNull(d1.Rows(0)("LB_CITY_ID")) Then
            If LB_CITY_ID.Length > 0 Then
                Me.GLookUp_CityList.ShowPopup() : Me.GLookUp_CityList.ClosePopup()
                Me.GLookUp_CityListView.FocusedRowHandle = Me.GLookUp_CityListView.LocateByValue("R_CI_REC_ID", LB_CITY_ID)
                Me.GLookUp_CityList.EditValue = LB_CITY_ID
                Me.GLookUp_CityList.Tag = Me.GLookUp_CityList.EditValue
                Me.GLookUp_CityList.Properties.Tag = "SHOW"
            End If
            'End If : 
            Me.GLookUp_CityList.Properties.ReadOnly = False
            'If Not IsDBNull(d1.Rows(0)("LB_PINCODE")) Then
            Txt_R_Pincode.Text = LB_PINCODE
            'End If

            Txt_Add.Text = Me.Txt_R_Add1.Text +
                        IIf(Len(Trim(Me.Txt_R_Add2.Text)) > 0, ", " + Me.Txt_R_Add2.Text, "") +
                        IIf(Len(Trim(Me.Txt_R_Add3.Text)) > 0, ", " + Me.Txt_R_Add3.Text, "") +
                        IIf(Len(Trim(Me.Txt_R_Add4.Text)) > 0, ", " + Me.Txt_R_Add4.Text, "") + ", " + Me.GLookUp_CityList.Text.ToUpper + ", Dist. " + Me.GLookUp_DistrictList.Text + ", " + Me.GLookUp_StateList.Text + "-" + Me.Txt_R_Pincode.Text
        End If

        Cmd_Ownership.Text = LB_OWNERSHIP
        If Not IsDBNull(LB_OWNERSHIP_PARTY_ID) Then
            Look_OwnList.EditValue = LB_OWNERSHIP_PARTY_ID
            Look_OwnList.Tag = Look_OwnList.EditValue
            Look_OwnList.Properties.Tag = "SHOW"
        End If
        Txt_SNo.Text = LB_SURVEY_NO

        Txt_Tot_Area.EditValue = LB_TOT_P_AREA
        Txt_Con_Area.EditValue = LB_CON_AREA
        Cmd_Con_Year.EditValue = LB_CON_YEAR
        Cmd_RccType.EditValue = LB_RCC_ROOF

        XtraTabControl1.SelectedTabPage = TPage_Rent
        Txt_Dep_Amt.Text = LB_DEPOSIT_AMT
        Txt_Mon_Rent.Text = LB_MONTH_RENT
        Txt_Other_Payments.Text = LB_MONTH_O_PAYMENTS
        Dim xDate As DateTime = Nothing
        If Not IsDBNull(LB_PAID_DATE) And IsDate(LB_PAID_DATE) Then
            xDate = LB_PAID_DATE : Txt_PaidDate.DateTime = xDate
        End If
        If Not IsDBNull(LB_PERIOD_FROM) And IsDate(LB_PERIOD_FROM) Then
            xDate = LB_PERIOD_FROM : Txt_F_Date.DateTime = xDate
        End If
        If Not IsDBNull(LB_PERIOD_TO) And IsDate(LB_PERIOD_TO) Then
            xDate = LB_PERIOD_TO : Txt_T_Date.DateTime = xDate
        End If

        XtraTabControl1.SelectedTabPage = TPage_Doc
        If LB_DOC_OTHERS.ToUpper.Trim = "YES" Then Chk_OtherDoc.Checked = True Else Chk_OtherDoc.Checked = False
        Txt_OtherDoc.Text = LB_DOC_NAME
        Dim DocTable As DataView = CType(Chk_DocList.DataSource, DataView)
        For counter As Integer = 0 To Chk_DocList.ItemCount - 1
            For Each currRow In d1_Doc.Rows
                If DocTable(counter)("ID").ToString().Equals(currRow("LB_MISC_ID").ToString()) Then
                    Chk_DocList.SetItemChecked(counter, True)
                End If
            Next
        Next counter

        Txt_Remarks.Text = LB_OTHER_DETAIL
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Cmd_PCategory.Enabled = False : Me.Cmd_PCategory.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_PType.Enabled = False : Me.Cmd_PType.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_PUse.Enabled = False : Me.Cmd_PUse.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_PName.Enabled = False : Me.Txt_PName.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Add.Enabled = False : Me.Txt_Add.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Ownership.Enabled = False : Me.Cmd_Ownership.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_OwnList.Enabled = False : Me.Look_OwnList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SNo.Enabled = False : Me.Txt_SNo.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Con_Area.Enabled = False : Me.Txt_Con_Area.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Tot_Area.Enabled = False : Me.Txt_Tot_Area.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Con_Year.Enabled = False : Me.Cmd_Con_Year.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_RccType.Enabled = False : Me.Cmd_RccType.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Dep_Amt.Enabled = False : Me.Txt_Dep_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_PaidDate.Enabled = False : Me.Txt_PaidDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Mon_Rent.Enabled = False : Me.Txt_PaidDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Other_Payments.Enabled = False : Me.Txt_Other_Payments.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_F_Date.Enabled = False : Me.Txt_PaidDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_T_Date.Enabled = False : Me.Txt_PaidDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_DocList.Enabled = False : Me.Chk_DocList.Appearance.ForeColor = SetColor
        Me.Chk_OtherDoc.Enabled = False : Me.Chk_OtherDoc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_OtherDoc.Enabled = False : Me.Txt_OtherDoc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_Owner.Enabled = False : Me.BUT_NEW.Enabled = False : Me.BUT_EDIT.Enabled = False : Me.BUT_DELETE.Enabled = False
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Set_Properties(ByVal Clear As Boolean, ByVal Set_ReadOnly As Boolean)
        Me.Txt_Other_Payments.Properties.Tag = 0
        Me.Txt_Dep_Amt.Properties.Tag = 0
        Me.Txt_Mon_Rent.Properties.Tag = 0
        If Clear Then
            Me.Txt_Other_Payments.Text = ""
            Me.Txt_Dep_Amt.Text = ""
            Me.Txt_Mon_Rent.Text = ""

        End If
        If Set_ReadOnly Then
            Me.Txt_Add.Properties.ReadOnly = True
            Me.Cmd_PCategory.Properties.ReadOnly = True
            Me.Cmd_PType.Properties.ReadOnly = True
            Me.Txt_PName.Properties.ReadOnly = True
            Me.Cmd_PUse.Properties.ReadOnly = True
            Me.Txt_Tot_Area.Properties.ReadOnly = True
            Me.Txt_Con_Area.Properties.ReadOnly = True
            Me.Cmd_Con_Year.Properties.ReadOnly = True
            Me.Cmd_RccType.Properties.ReadOnly = True
            Me.Txt_Remarks.Properties.ReadOnly = True
            Me.Chk_DocList.Enabled = False
            Me.Chk_OtherDoc.Properties.ReadOnly = True
            Me.Txt_SNo.Properties.ReadOnly = True
            Me.Txt_Dep_Amt.Properties.ReadOnly = True
            Me.Txt_PaidDate.Properties.ReadOnly = True
            Me.Txt_Mon_Rent.Properties.ReadOnly = True
            Me.Txt_Other_Payments.Properties.ReadOnly = True
            Me.Txt_F_Date.Properties.ReadOnly = True
            Me.Txt_T_Date.Properties.ReadOnly = True

            Me.BUT_NEW.Enabled = False
            Me.BUT_EDIT.Enabled = False
            Me.BUT_DELETE.Enabled = False
        Else
            Me.Txt_Add.Properties.ReadOnly = False
            Me.Cmd_PCategory.Properties.ReadOnly = False
            Me.Cmd_PType.Properties.ReadOnly = False
            Me.Txt_PName.Properties.ReadOnly = False
            Me.Cmd_PUse.Properties.ReadOnly = False
            Me.Txt_Tot_Area.Properties.ReadOnly = False
            Me.Txt_Con_Area.Properties.ReadOnly = False
            Me.Cmd_Con_Year.Properties.ReadOnly = False
            Me.Cmd_RccType.Properties.ReadOnly = False
            Me.Txt_Remarks.Properties.ReadOnly = False
            Me.Chk_DocList.Enabled = True
            Me.Chk_OtherDoc.Properties.ReadOnly = False
            Me.Txt_SNo.Properties.ReadOnly = False
            Me.Txt_Dep_Amt.Properties.ReadOnly = False
            Me.Txt_PaidDate.Properties.ReadOnly = False
            Me.Txt_Mon_Rent.Properties.ReadOnly = False
            Me.Txt_Other_Payments.Properties.ReadOnly = False
            Me.Txt_F_Date.Properties.ReadOnly = False
            Me.Txt_T_Date.Properties.ReadOnly = False

            Me.BUT_NEW.Enabled = True
            Me.BUT_EDIT.Enabled = True
            Me.BUT_DELETE.Enabled = True
        End If
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Look_OwnList)
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Dim xRowPos As Object = Me.GridView1.FocusedRowHandle
        Dim xColPos As Object = Me.GridView1.FocusedColumn
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Select Case Action
                Case "NEW"

                    Dim xfrm As New Frm_Property_Window_Ext
                    xfrm.Text = "New ~ Extended Property..." : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        Me.GridView1.ClearSorting()
                        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
                        Dim XSR As Integer = 0
                        If Me.GridView1.RowCount = 1 And Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) = 0 Then
                            DT.Rows(Me.GridView1.FocusedRowHandle).Delete()
                            ROW = DT.NewRow : ROW("Sr.") = 1 : XSR = 1
                        Else
                            Me.GridView1.MoveLast()
                            ROW = DT.NewRow
                            ROW("Sr.") = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) + 1 : XSR = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) + 1
                        End If
                        ROW("Institution") = xfrm.Look_InsList.Text
                        ROW("Ins_ID") = xfrm.Look_InsList.Tag
                        ROW("Total Plot Area (Sq.Ft.)") = Val(xfrm.Txt_Ext_Tot_Area.Text)
                        ROW("Constructed Area (Sq.Ft.)") = Val(xfrm.Txt_Ext_Con_Area.Text)
                        ROW("Construction Year") = xfrm.Cmd_Ext_Con_Year.Text
                        ROW("M.O.U. Date") = xfrm.Txt_MOU_Date.Text
                        ROW("Value") = Val(xfrm.xAmt.Text)
                        ROW("Other Detail") = xfrm.Txt_Others.Text
                        DT.Rows.Add(ROW)
                        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                    End If
                    Me.GridView1.Focus()
                    xfrm.Dispose()
                Case "EDIT"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 And Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) > 0 Then

                        Dim xfrm As New Frm_Property_Window_Ext
                        xfrm.Text = "Edit ~ Extended Property..." : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Dim XSR As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()
                        xfrm.Look_InsList.EditValue = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Ins_ID").ToString()
                        xfrm.Look_InsList.Tag = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Ins_ID").ToString()
                        xfrm.Txt_Ext_Tot_Area.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Total Plot Area (Sq.Ft.)").ToString()
                        xfrm.Txt_Ext_Con_Area.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Constructed Area (Sq.Ft.)").ToString()
                        xfrm.Cmd_Ext_Con_Year.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Construction Year").ToString()
                        xfrm.xAmt.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Value").ToString()
                        xfrm.Txt_Others.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Other Detail").ToString()
                        Dim xDate As DateTime = Nothing : xDate = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "M.O.U. Date").ToString() : xfrm.Txt_MOU_Date.DateTime = xDate
                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Ins_ID", xfrm.Look_InsList.Tag)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Institution", xfrm.Look_InsList.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Total Plot Area (Sq.Ft.)", xfrm.Txt_Ext_Tot_Area.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Constructed Area (Sq.Ft.)", xfrm.Txt_Ext_Con_Area.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Construction Year", xfrm.Cmd_Ext_Con_Year.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "M.O.U. Date", xfrm.Txt_MOU_Date.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Value", xfrm.xAmt.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Other Detail", xfrm.Txt_Others.Text)
                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                        End If
                        Me.GridView1.Focus()
                        xfrm.Dispose()
                    End If
                Case "DELETE"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 And Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) > 0 Then

                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                        If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                            Dim XSR As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()
                            DT.Rows(Me.GridView1.FocusedRowHandle).Delete()
                            If Me.GridView1.RowCount = 0 Then DT.NewRow() : DT.Rows.Add(ROW)
                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                            Me.GridView1.Focus()
                        End If
                        xPromptWindow.Dispose()
                    End If
            End Select

            For I As Integer = 0 To Me.GridView1.RowCount - 1
                If Val(Me.GridView1.GetRowCellValue(I, "Sr.").ToString()) > 0 Then
                    Me.GridView1.SetRowCellValue(I, "Sr.", I + 1)
                End If
            Next
            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        End If
        Me.GridView1.Focus()
    End Sub

    Private Sub Get_Documents_List()
        Dim d1 As DataTable = Base._L_B_Voucher_DBOps.GetDocuments()
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim DV5 As New DataView(d1)
        If DV5.Count > 0 Then
            Me.Chk_DocList.ValueMember = "ID" : Me.Chk_DocList.DisplayMember = "Name" : Me.Chk_DocList.DataSource = DV5
        End If
    End Sub
    Private Function AddressChecks()
        If Trim(Txt_R_Add1.Text.Length) <= 0 Then
            Me.Txt_Add.ShowPopup()
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("P r o p e r t y   A d d r e s s  C a n n o t   B e   B l a n k . . . !", Me.Txt_R_Add1, 0, Me.Txt_R_Add1.Height, 5000)
            Me.Txt_R_Add1.Focus()
            Return False
        Else
            Me.ToolTip1.Hide(Me.Txt_R_Add1)
        End If

        If Len(Trim(GLookUp_StateList.Tag)) <= 0 Or Len(Trim(GLookUp_StateList.Text)) <= 0 Then
            Me.Txt_Add.ShowPopup()
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("S t a t e   C a n n o t   B e   B l a n k . . . !", Me.GLookUp_StateList, 0, Me.GLookUp_StateList.Height, 5000)
            Me.GLookUp_StateList.Focus()
            Return False
        Else
            Me.ToolTip1.Hide(Me.GLookUp_StateList)
        End If

        If Len(Trim(GLookUp_DistrictList.Tag)) <= 0 Or Len(Trim(GLookUp_DistrictList.Text)) <= 0 Then
            Me.Txt_Add.ShowPopup()
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("D i s t r i c t   C a n n o t   B e   B l a n k . . . !", Me.GLookUp_DistrictList, 0, Me.GLookUp_DistrictList.Height, 5000)
            Me.GLookUp_DistrictList.Focus()
            Return False
        Else
            Me.ToolTip1.Hide(Me.GLookUp_DistrictList)
        End If

        If Len(Trim(GLookUp_CityList.Tag)) <= 0 Or Len(Trim(GLookUp_CityList.Text)) <= 0 Then
            Me.Txt_Add.ShowPopup()
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("C i t y   C a n n o t   B e   B l a n k . . . !", Me.GLookUp_CityList, 0, Me.GLookUp_CityList.Height, 5000)
            Me.GLookUp_CityList.Focus()
            Return False
        Else
            Me.ToolTip1.Hide(Me.GLookUp_CityList)
        End If
        If Trim(Txt_R_Pincode.Text.Length) <= 0 Then
            Me.Txt_Add.ShowPopup()
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("P i n c o d e   C a n n o t   B e   B l a n k . . . !", Me.Txt_R_Pincode, 0, Me.Txt_R_Pincode.Height, 5000)
            Me.Txt_R_Pincode.Focus()
            Return False
        Else
            Me.ToolTip1.Hide(Me.Txt_R_Pincode)
        End If
        Return True
    End Function
#End Region

#Region "Start--> LookupEdit Events"


    '2.Address Book
    Private Sub But_Owner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Owner.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim SaveID1 As String = Me.Look_OwnList.Tag
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Ownership)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetOwnList()
            Me.Look_OwnList.EditValue = SaveID1 : Me.Look_OwnList.Tag = SaveID1 : Look_OwnList.Properties.Tag = "SHOW"
        End If
    End Sub
    Private Sub Look_OwnList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_OwnList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_OwnList.CancelPopup()
            Hide_Properties()
            Me.Cmd_PUse.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_PUse.Focus()
        End If

    End Sub
    Private Sub Look_OwnList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_OwnList.EditValueChanged
        If Me.Look_OwnList.Properties.Tag = "SHOW" Then
            Me.Look_OwnList.Tag = Me.Look_OwnList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetOwnList()
        Dim d1 As DataTable = Base._L_B_Voucher_DBOps.GetOwners()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        dview.Sort = "ID"
        If dview.Count > 0 Then
            Me.Look_OwnList.Properties.ValueMember = "ID"
            Me.Look_OwnList.Properties.DisplayMember = "Name"
            Me.Look_OwnList.Properties.DataSource = dview
            Me.Look_OwnList.Properties.PopulateColumns()
            'Me.Look_OwnList.Properties.BestFit()
            Me.Look_OwnList.Properties.PopupWidth = 400
            Me.Look_OwnList.Properties.Columns(3).Visible = False
            Me.Look_OwnList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_OwnList.EditValue = 0
            Me.Look_OwnList.Properties.Tag = "SHOW"
        Else
            Me.Look_OwnList.Properties.Tag = "NONE"
        End If
    End Sub

    Private Sub GLookUp_StateList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_StateList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_StateListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_StateListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_StateList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_StateListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_StateListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_StateList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_StateList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_StateList.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_StateList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_StateList.CancelPopup()
            Hide_Properties()
            Me.Txt_R_Add4.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_StateList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_R_Add4.Focus()
        End If
    End Sub
    Private Sub GLookUp_StateList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_StateList.EditValueChanged
        If Me.GLookUp_StateList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_StateListView.RowCount > 0 And Val(Me.GLookUp_StateListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_StateList.Tag = Me.GLookUp_StateListView.GetRowCellValue(Me.GLookUp_StateListView.FocusedRowHandle, "R_ST_REC_ID").ToString
                Me.GLookUp_StateList.Properties.AccessibleDescription = Me.GLookUp_StateListView.GetRowCellValue(Me.GLookUp_StateListView.FocusedRowHandle, "R_ST_CODE").ToString
                Me.GLookUp_DistrictList.Tag = "" : Me.GLookUp_DistrictList.EditValue = "" : LookUp_Get_DistrictList() '#6032 fix
                Me.GLookUp_CityList.Tag = "" : Me.GLookUp_CityList.EditValue = "" : LookUp_Get_CityList()
            Else
                Me.GLookUp_StateList.Properties.AccessibleDescription = ""
                Me.GLookUp_StateList.Tag = "" : Me.GLookUp_StateList.EditValue = ""
                Me.GLookUp_DistrictList.Tag = "" : Me.GLookUp_DistrictList.EditValue = ""
                Me.GLookUp_CityList.Tag = "" : Me.GLookUp_CityList.EditValue = ""
                LookUp_Get_DistrictList()
                LookUp_Get_CityList()
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_StateList()
        Dim d1 As DataTable = Base._Address_DBOps.GetStates("IN", "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID")
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("R_ST_NAME") = "" : ROW("R_ST_CODE") = 0 : d1.Rows.Add(ROW)
        Dim dview As New DataView(d1) : dview.Sort = "R_ST_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_StateList.Properties.ValueMember = "R_ST_REC_ID"
            Me.GLookUp_StateList.Properties.DisplayMember = "R_ST_NAME"
            Me.GLookUp_StateList.Properties.DataSource = dview
            Me.GLookUp_StateListView.RefreshData()
            Me.GLookUp_StateList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_StateList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.GLookUp_StateList.Properties.ReadOnly = False
    End Sub

    '5a R.District List
    Private Sub GLookUp_DistrictList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_DistrictList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_DistrictListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_DistrictListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_DistrictList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_DistrictListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_DistrictListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_DistrictList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_DistrictList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_DistrictList.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_DistrictList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_DistrictList.CancelPopup()
            Hide_Properties()
            Me.GLookUp_StateList.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_DistrictList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_StateList.Focus()
        End If
    End Sub
    Private Sub GLookUp_DistrictList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_DistrictList.EditValueChanged
        If Me.GLookUp_DistrictList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_DistrictListView.RowCount > 0 And Val(Me.GLookUp_DistrictListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_DistrictList.Tag = Me.GLookUp_DistrictListView.GetRowCellValue(Me.GLookUp_DistrictListView.FocusedRowHandle, "R_DI_REC_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_DistrictList()
        Dim d1 As DataTable = Base._Address_DBOps.GetDistricts("IN", Val(Me.GLookUp_StateList.Properties.AccessibleDescription), "R_DI_NAME", "R_DI_REC_ID")
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("R_DI_NAME") = "" : ROW("R_DI_REC_ID") = "" : d1.Rows.Add(ROW)
        Dim dview As New DataView(d1) : dview.Sort = "R_DI_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_DistrictList.Properties.ValueMember = "R_DI_REC_ID"
            Me.GLookUp_DistrictList.Properties.DisplayMember = "R_DI_NAME"
            Me.GLookUp_DistrictList.Properties.DataSource = dview
            Me.GLookUp_DistrictListView.RefreshData()
            Me.GLookUp_DistrictList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_DistrictList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.GLookUp_DistrictList.Properties.ReadOnly = False
    End Sub

    '6a R.City List
    Private Sub GLookUp_CityList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_CityList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_CityListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_CityListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_CityList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_CityListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_CityListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_CityList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_CityList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_CityList.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_CityList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_CityList.CancelPopup()
            Hide_Properties()
            Me.GLookUp_DistrictList.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_CityList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_DistrictList.Focus()
        End If
    End Sub
    Private Sub GLookUp_CityList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_CityList.EditValueChanged
        If Me.GLookUp_CityList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_CityListView.RowCount > 0 And Val(Me.GLookUp_CityListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_CityList.Tag = Me.GLookUp_CityListView.GetRowCellValue(Me.GLookUp_CityListView.FocusedRowHandle, "R_CI_REC_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_CityList()
        Dim d1 As DataTable = Nothing
        'If GLookUp_CountryList.Tag = "f9970249-121c-4b8f-86f9-2b53e850809e" Then 'india
        d1 = Base._Address_DBOps.GetCitiesBySt_Co_Code("IN", Val(Me.GLookUp_StateList.Properties.AccessibleDescription), "R_CI_NAME", "R_CI_REC_ID")
        'Else
        'd1 = Base._Address_DBOps.GetCitiesByCO_Code(Me.GLookUp_CountryList.Properties.AccessibleDescription, "R_CI_NAME", "R_CI_REC_ID")
        'End If

        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("R_CI_NAME") = "" : ROW("R_CI_REC_ID") = "" : d1.Rows.Add(ROW)
        Dim dview As New DataView(d1) : dview.Sort = "R_CI_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_CityList.Properties.ValueMember = "R_CI_REC_ID"
            Me.GLookUp_CityList.Properties.DisplayMember = "R_CI_NAME"
            Me.GLookUp_CityList.Properties.DataSource = dview
            Me.GLookUp_CityListView.RefreshData()
            Me.GLookUp_CityList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_CityList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.GLookUp_CityList.Properties.ReadOnly = False
    End Sub


#End Region

#Region "Start--> Custom Grid Setting"

    Friend DS As New DataSet()
    Friend ROW As DataRow
    Friend DT As DataTable = DS.Tables.Add("TableName")
    Private Sub SetGridData()
        With DT
            .Columns.Add("Sr.", Type.GetType("System.Int32"))
            .Columns.Add("Institution", Type.GetType("System.String"))
            .Columns.Add("Ins_ID", Type.GetType("System.String"))
            .Columns.Add("Total Plot Area (Sq.Ft.)", Type.GetType("System.Double"))
            .Columns.Add("Constructed Area (Sq.Ft.)", Type.GetType("System.Double"))
            .Columns.Add("Construction Year", Type.GetType("System.String"))
            .Columns.Add("M.O.U. Date", Type.GetType("System.String"))
            .Columns.Add("Value", Type.GetType("System.Double"))
            .Columns.Add("Other Detail", Type.GetType("System.String"))
        End With
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then ROW = DT.NewRow : DT.Rows.Add(ROW)
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Dim INS_Table As DataTable = Base._L_B_Voucher_DBOps.GetInstt("INS_NAME", "INS_ID", "SNAME")
            If INS_Table Is Nothing Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            'Dim SQL_STR2 As String = " SELECT LB_SR_NO,LB_INS_ID,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_MOU_DATE,LB_VALUE,LB_OTHER_DETAIL " & _
            '                         " FROM Land_Building_Extended_Info " & _
            '                         " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LB_CEN_ID='" & Base._open_Cen_ID & "' AND LB_REC_ID='" & Me.xID.Text & "' ; "
            'Dim d2 As New Common_Lib.Get_Data(Base, "SYS", "Land_Building_Extended_Info", SQL_STR2)
            Dim EXT_Table As DataTable = LB_EXTENDED_PROPERTY_TABLE

            'BUILD DATA
            Dim BuildData = From EXT In EXT_Table, I In INS_Table _
                            Where (EXT.Field(Of String)("LB_INS_ID") = I.Field(Of String)("INS_ID")) _
                            Select New With { _
                                            .LB_SR_NO = EXT.Field(Of Double)("LB_SR_NO"), _
                                            .INS_NAME = I.Field(Of String)("INS_NAME"), _
                                            .INS_ID = I.Field(Of String)("INS_ID"), _
                                            .LB_TOT_P_AREA = EXT.Field(Of Double)("LB_TOT_P_AREA"),
                                            .LB_CON_AREA = EXT.Field(Of Double)("LB_CON_AREA"),
                                            .LB_CON_YEAR = EXT.Field(Of String)("LB_CON_YEAR"),
                                            .LB_MOU_DATE = EXT.Field(Of String)("LB_MOU_DATE"), _
                                            .LB_VALUE = EXT.Field(Of Double)("LB_VALUE"),
                                            .LB_OTHER_DETAIL = EXT.Field(Of String)("LB_OTHER_DETAIL") _
                                      } : Dim Final_Data = BuildData.ToList

            For Each FieldName In Final_Data
                ROW = DT.NewRow
                ROW("Sr.") = FieldName.LB_SR_NO
                ROW("Institution") = FieldName.INS_NAME
                ROW("Ins_ID") = FieldName.INS_ID
                ROW("Total Plot Area (Sq.Ft.)") = FieldName.LB_TOT_P_AREA
                ROW("Constructed Area (Sq.Ft.)") = FieldName.LB_CON_AREA
                ROW("Construction Year") = FieldName.LB_CON_YEAR
                ROW("M.O.U. Date") = Format(Convert.ToDateTime(FieldName.LB_MOU_DATE), Base._Date_Format_Current)
                ROW("Value") = FieldName.LB_VALUE
                ROW("Other Detail") = FieldName.LB_OTHER_DETAIL
                DT.Rows.Add(ROW)
            Next
            If DT.Rows.Count = 0 Then ROW = DT.NewRow : DT.Rows.Add(ROW)
        End If

        ' Bind the DataGrids to the DataSet's tables.
        Me.GridControl1.DataSource = DS
        Me.GridControl1.DataMember = "TableName"
        Me.GridView1.Columns("Sr.").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        Me.GridView1.Columns("Sr.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Sr.").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridView1.Columns("Institution").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridView1.Columns("Total Plot Area (Sq.Ft.)").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Constructed Area (Sq.Ft.)").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Construction Year").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Ins_ID").Visible = False : Me.GridView1.Columns("Value").Visible = False
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
    End Sub
    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        Me.DataNavigation("EDIT")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        'If e.KeyCode = Keys.Insert Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("NEW")
        'End If
        'If e.KeyCode = Keys.Enter Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("EDIT")
        'End If
        'If e.KeyCode = Keys.Delete Then
        '    e.SuppressKeyPress = True
        '    Me.DataNavigation("DELETE")
        'End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            If Cmd_PType.Text.ToUpper = "LAND" Then
                Me.Txt_Tot_Area.Focus()
            Else
                Me.Cmd_RccType.Focus()
            End If
        End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            XtraTabControl1.SelectedTabPage = TPage_Doc : Me.Chk_DocList.Focus()
        End If

    End Sub
#End Region

End Class