Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_Voucher_Type

#Region "Start--> Default Variables"

    Private Get_Voucher_Type As String = ""
    Public Voucher_Type As String
    Public Selection_By_Item As Boolean = False
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
        SetDefault()
    End Sub
    Private Sub Frm_Login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Me.GLookUp_ItemList.Focus()
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        '
        Me.GLookUp_ItemList.Focus()
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
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
        Me.DialogResult = Windows.Forms.DialogResult.None
        If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("I t e m   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
            Me.GLookUp_ItemList.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        End If
        Select Case Get_Voucher_Type.ToUpper
            Case "CASH DEPOSITED", "CASH WITHDRAWN"
                Voucher_Type = "CASH" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "BANK TRANSFER"
                Voucher_Type = "BANK" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "DONATION"
                Voucher_Type = "DONATION - REGULAR" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "DONATION - FOREIGN"
                Voucher_Type = "DONATION - FOREIGN" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "COLLECTION BOX"
                Voucher_Type = "COLLECTION BOX" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "PAYMENT", "PAYMENT - INSTITUTE", "LAND & BUILDING / GIFT", "LAND & BUILDING"
                Voucher_Type = "PAYMENT" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "RECEIPTS", "RECEIPTS - INSTITUTE"
                Voucher_Type = "RECEIPTS" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "DONATION - GIFT"
                Voucher_Type = "DONATION - GIFT" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "INTERNAL TRANSFER", "INTERNAL TRANSFER - INSTITUTE", "INTERNAL TRANSFER WITH H.Q."
                Voucher_Type = "INTERNAL TRANSFER" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "FD"
                Voucher_Type = "FD" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "SALE OF ASSET"
                Voucher_Type = "SALE OF ASSET" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "MEMBERSHIP"
                Voucher_Type = "MEMBERSHIP" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "MEMBERSHIP RENEWAL"
                Voucher_Type = "MEMBERSHIP RENEWAL" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "ASSET TRANSFER"
                Voucher_Type = "ASSET TRANSFER" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_WIP_FINALIZATION.Name
                Voucher_Type = "WIP FINALIZATION" : Selection_By_Item = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case Else
                DevExpress.XtraEditors.XtraMessageBox.Show("C o m i n g   S o o n . . . !", "Voucher Entry...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
        End Select

    End Sub

    Private Sub All_Voucher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CASH.Click, BUT_BANK.Click, BUT_PAYMENT.Click, BUT_RECEIPT.Click, BUT_DONATION.Click, BUT_FD.Click, BUT_INT_TRANSFER.Click, BUT_C_BOX.Click, BUT_J_Entry.Click, BUT_ASSET_TFER.Click, BUT_GIFT.Click, BUT_S_ASSET.Click, BUT_WIP_FINALIZATION.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        Selection_By_Item = False
        Select Case btn.Name
            Case BUT_CASH.Name
                Voucher_Type = "CASH"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_BANK.Name
                Voucher_Type = "BANK"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_DONATION.Name
                If Base.Allow_Foreign_Donation Then
                    Me.Hide()
                    Dim xfrm As New Frm_Voucher_Win_D_Type : xfrm.Text = "Donation"
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim xKind As String = xfrm.RadioGroup1.Text : xfrm.Dispose()
                        If xKind.ToString.ToUpper = "DONATION - REGULAR" Then Voucher_Type = "DONATION - REGULAR" : Me.DialogResult = Windows.Forms.DialogResult.OK
                        If xKind.ToString.ToUpper = "DONATION - FOREIGN" Then Voucher_Type = "DONATION - FOREIGN" : Me.DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        Me.Show() : xfrm.Dispose()
                    End If
                Else
                    Voucher_Type = "DONATION - REGULAR"
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            Case BUT_C_BOX.Name
                Voucher_Type = "COLLECTION BOX"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_PAYMENT.Name
                Voucher_Type = "PAYMENT"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_RECEIPT.Name
                Voucher_Type = "RECEIPTS"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_GIFT.Name
                Voucher_Type = "DONATION - GIFT"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_INT_TRANSFER.Name
                Voucher_Type = "INTERNAL TRANSFER"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_FD.Name
                Voucher_Type = "FD"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_S_ASSET.Name
                Voucher_Type = "SALE OF ASSET"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_J_Entry.Name
                Voucher_Type = "JOURNAL"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_ASSET_TFER.Name
                Voucher_Type = "ASSET TRANSFER"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case BUT_WIP_FINALIZATION.Name
                Voucher_Type = "WIP FINALIZATION"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case Else
                DevExpress.XtraEditors.XtraMessageBox.Show("C o m i n g   S o o n . . . !", "Voucher Entry...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
        End Select

    End Sub
#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_ItemList
    Private Sub GLookUp_ItemList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_ItemList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_ItemList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_ItemList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_ItemList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_ItemList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_ItemList.CancelPopup()
            Hide_Properties()
            'Me.Txt_No.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.List_Lang.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Hide_Properties()
            If GLookUp_ItemList.Text.Length > 0 And GLookUp_ItemList.Tag.Length > 0 Then
                Me.BUT_OK_Click(Nothing, Nothing)
            End If
        End If

    End Sub
    Private Sub GLookUp_ItemList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.EditValueChanged
        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" Then

            If (Me.GLookUp_ItemListView.RowCount > 0 And Val(Me.GLookUp_ItemListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_ID").ToString
                Get_Voucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                Me.BUT_OK.Enabled = True
            Else
                Me.BUT_OK.Enabled = False
            End If

        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim ITEM_APPLICABLE As String = "" : If Base.Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
        Dim d1 As DataTable = Base._Voucher_DBOps.GetItem_LedgerListMain(Base.Allow_Foreign_Donation, Base.Allow_Membership, ITEM_APPLICABLE)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "ITEM_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_ItemList.Properties.ValueMember = "ITEM_ID"
            Me.GLookUp_ItemList.Properties.DisplayMember = "ITEM_NAME"
            Me.GLookUp_ItemList.Properties.DataSource = dview
            Me.GLookUp_ItemListView.RefreshData()
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.GLookUp_ItemList.EditValue = 0
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
        End If
    End Sub

#End Region

    

    
End Class
