Imports DevExpress.XtraEditors

Public Class Dialog_Telephone_Bill

#Region "Start--> Default Variables"

    Public MainBase As New Common_Lib.Common
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
        Base = MainBase
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
        GLookUp_TeleList.Focus()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.GLookUp_TeleList.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"


    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        If Len(Trim(Me.GLookUp_TeleList.Tag)) = 0 Or Len(Trim(Me.GLookUp_TeleList.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("T e l e p h o n e   N o .   N o t   S e l e c t e d . . . !", Me.GLookUp_TeleList, 0, Me.GLookUp_TeleList.Height, 5000)
            Me.GLookUp_TeleList.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.GLookUp_TeleList)
        End If

        If Len(Trim(Me.Txt_Fr_Date.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("P e r i o d   F r o m   N o t   S e l e c t e d . . . !", Me.Txt_Fr_Date, 0, Me.Txt_Fr_Date.Height, 5000)
            Me.Txt_Fr_Date.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Txt_Fr_Date)
        End If

        If Len(Trim(Me.Txt_To_Date.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("P e r i o d   T o   N o t   S e l e c t e d . . . !", Me.Txt_To_Date, 0, Me.Txt_To_Date.Height, 5000)
            Me.Txt_To_Date.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Txt_To_Date)
        End If

        Dim xRep As New Report_Telephone_Bill : xRep.MainBase = Base : xRep.xTP_ID = Me.GLookUp_TeleList.Tag
        xRep.xFr_Date = Txt_Fr_Date.DateTime : xRep.xTo_Date = Txt_To_Date.DateTime
        'Base.Show_ReportPreview(xRep, "Telephone Bill Payment Details", Me, True)
        xRep.Dispose()

        Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Hide_Properties()
        Me.DialogResult = DialogResult.Cancel
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

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = DialogResult.None

        GLookUp_TeleList.Tag = ""  'LookUp_GetTeleList()
        Txt_Fr_Date.DateTime = Base._open_Year_Sdt
        Txt_To_Date.DateTime = Base._open_Year_Edt
        Me.GLookUp_TeleList.Focus()
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_TeleList)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_TeleList
    Private Sub GLookUp_TeleList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_TeleList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_TeleListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_TeleListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_TeleList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_TeleListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_TeleListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_TeleList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_TeleList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_TeleList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_TeleList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_TeleList.CancelPopup()
            Hide_Properties()
            Me.BUT_SAVE_COM.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_TeleList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.BUT_SAVE_COM.Focus()
        End If

    End Sub
    Private Sub GLookUp_TeleList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_TeleList.EditValueChanged
        If Me.GLookUp_TeleList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_TeleListView.RowCount > 0 And Val(Me.GLookUp_TeleListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_TeleList.Tag = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_ID").ToString
                Me.BE_Company.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_COMPANY").ToString
                Me.BE_Category.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_CATEGORY").ToString
                Me.BE_Plantype.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_TYPE").ToString

            End If
        Else
        End If
    End Sub
    ' Private Sub LookUp_GetTeleList()
    'Dim d1 As DataTable = Base._telephoneDBOps.GetListByCondition(Common_Lib.RealTimeService.ClientScreen.Profile_Telephone, "")
    'If d1 Is Nothing Then
    '       DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    'Exit Sub
    'End If
    'Dim ROW As DataRow : ROW = d1.NewRow : ROW("TP_NO") = " --All Tel. No(s)--- " : ROW("TP_ID") = "--ALL--" : d1.Rows.Add(ROW)
    'Dim dview As New DataView(d1) : dview.Sort = "TP_NO"
    'If dview.Count > 0 Then
    'Me.GLookUp_TeleList.Properties.ValueMember = "TP_ID"
    'Me.GLookUp_TeleList.Properties.DisplayMember = "TP_NO"
    'Me.GLookUp_TeleList.Properties.DataSource = dview
    'Me.GLookUp_TeleListView.RefreshData()
    'Me.GLookUp_TeleList.Properties.Tag = "SHOW"
    'Else
    'Me.GLookUp_TeleList.Properties.Tag = "NONE"
    'End If

    ' Me.GLookUp_TeleList.Properties.ReadOnly = False

    ' End Sub

#End Region

End Class