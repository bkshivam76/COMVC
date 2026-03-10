Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Receipt_Options

#Region "Start--> Default Variables"
    Public Selected_Bank_ID As String = ""
    Public SelectedRefBankID As String = ""
    Public Tr_Date As DateTime
    Public Mode As String
    Public Selected_ItemID As String
    Public Selected_Trans_Type As String
    Public Selected_Purpose_ID As String

    Public Enum Voucher_Choice
        Donation
        Internal_Transfer
        Receipt
    End Enum
    Public chosen_Voucher As Voucher_Choice

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
        SetDefault() : DataBinding()
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
        'If Len(Trim(Me.Cmb_FilterTypes.Text)) = 0 Then
        '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
        '    Me.ToolTip1.Show("F i l t e r  T y p e   N o t   S e l e c t e d . . . !", Me.Cmb_FilterTypes, 0, Me.Cmb_FilterTypes.Height, 5000)
        '    Me.Cmb_FilterTypes.Focus()
        '    Me.DialogResult = Windows.Forms.DialogResult.None
        '    Exit Sub
        'Else
        '    Me.ToolTip1.Hide(Me.Cmb_FilterTypes)
        'End If


        'If Len(Trim(Me.GLookUp_FilterCriteria.Text)) = 0 Then
        '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
        '    Me.ToolTip1.Show("F i l t e r  C r i t e r i a   N o t   S e l e c t e d . . . !", Me.GLookUp_FilterCriteria, 0, Me.GLookUp_FilterCriteria.Height, 5000)
        '    Me.GLookUp_FilterCriteria.Focus()
        '    Me.DialogResult = Windows.Forms.DialogResult.None
        '    Exit Sub
        'Else
        '    Me.ToolTip1.Hide(Me.GLookUp_FilterCriteria)
        'End If

        'FilterType = Cmb_FilterTypes.Text
        'Advanced_Filter_Category = AssetProfile
        'Advanced_Filter_RefId = GLookUp_FilterCriteria.Tag
        'Dim KeepOpen As Boolean = True
        'Do While KeepOpen
        '    KeepOpen = False
        If Cmb_VoucherTypes.Text = "Donation" Then ' "DONATION - REGULAR"
            Dim zfrm As New Frm_Voucher_Win_Donation_R : zfrm.MainBase = Base
            zfrm.Tag = Common_Lib.Common.Navigation_Mode._New : zfrm.Txt_V_Date.DateTime = Tr_Date : zfrm.Cmd_Mode.Text = IIf(zfrm.Cmd_Mode.Properties.Items.Contains(Mode), Mode, "CHEQUE")
            : zfrm.SelectedBankID = Selected_Bank_ID : zfrm.SelectedRefBankID = SelectedRefBankID
            zfrm.ShowDialog(Me)
            If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                Tr_Date = zfrm.Txt_V_Date.DateTime : Mode = zfrm.Cmd_Mode.Text : Selected_Bank_ID = zfrm.GLookUp_BankList.Tag : SelectedRefBankID = zfrm.GLookUp_RefBankList.Tag
            End If
            'xID = zfrm.xID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
            'If Not zfrm Is Nothing Then zfrm.Dispose()
        End If
        If Cmb_VoucherTypes.Text = "Receipt" Then
            Dim zfrm As New Frm_Voucher_Win_Gen_Rec : zfrm.MainBase = Base
            zfrm.Tag = Common_Lib.Common.Navigation_Mode._New : zfrm.Txt_V_Date.DateTime = Tr_Date : zfrm.Cmd_Mode.Text = IIf(zfrm.Cmd_Mode.Properties.Items.Contains(Mode), Mode, "CHEQUE")
            : zfrm.SelectedBankID = Selected_Bank_ID : zfrm.SelectedRefBankID = SelectedRefBankID
            zfrm.ShowDialog(Me)
            If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                Tr_Date = zfrm.Txt_V_Date.DateTime : Mode = zfrm.Cmd_Mode.Text : Selected_Bank_ID = zfrm.GLookUp_BankList.Tag
            End If
            ': xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
            'If Not zfrm Is Nothing Then zfrm.Dispose()
        End If
        If Cmb_VoucherTypes.Text = "Internal Transfer" Then
            Dim zfrm As New Frm_Voucher_Win_I_Transfer : zfrm.MainBase = Base
            If Selected_ItemID Is Nothing Then Selected_ItemID = IIf(Base.Is_HQ_Centre, "4dde250e-d1ab-4015-a692-54bf1b65df9f", "5ed74f26-5229-457c-bb3f-65e8165b9bf9")
            If Selected_ItemID.Length = 0 Then Selected_ItemID = IIf(Base.Is_HQ_Centre, "4dde250e-d1ab-4015-a692-54bf1b65df9f", "5ed74f26-5229-457c-bb3f-65e8165b9bf9")
            zfrm.Tag = Common_Lib.Common.Navigation_Mode._New : zfrm.Selected_V_Date = Tr_Date : zfrm.Selected_Mode = IIf(zfrm.Cmd_Mode.Properties.Items.Contains(Mode), Mode, "CHEQUE") : zfrm.Selected_Bank_ID = Selected_Bank_ID
            zfrm.Selected_Item_ID = Selected_ItemID : zfrm.USE_CROSS_REF = False : zfrm.Selected_Trans_Type = Selected_Trans_Type
            zfrm.ShowDialog(Me)
            If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                If zfrm.iTrans_Type = "CREDIT" Then
                    Tr_Date = zfrm.Txt_V_Date.DateTime : Mode = zfrm.Cmd_Mode.Text : Selected_Bank_ID = zfrm.GLookUp_BankList.Tag : Selected_ItemID = zfrm.GLookUp_ItemList.Tag
                    : Selected_Trans_Type = zfrm.iTrans_Type
                Else
                    Me.Close()
                End If
            End If
            ': xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
            'If Not zfrm Is Nothing Then zfrm.Dispose()
        End If
        ' Loop

        ' Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

#End Region

#Region "Start--> Events"
    Private Sub DataBinding()
        Select Case chosen_Voucher
            Case Voucher_Choice.Donation
                Cmb_VoucherTypes.Text = "Donation"
            Case Voucher_Choice.Internal_Transfer
                Cmb_VoucherTypes.Text = "Internal Transfer"
            Case Voucher_Choice.Receipt
                Cmb_VoucherTypes.Text = "Receipt"
        End Select
    End Sub
    Private Sub SetDefault()
        'LookUp_GetBankList()
    End Sub
#End Region

End Class
