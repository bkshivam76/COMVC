Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering

Public Class Frm_Voucher_Win_Gen_Pay

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Public Enum PaymentType As Integer
        Bank
        Cash
        Credit
    End Enum
    Public SelectedPaymentType As PaymentType = PaymentType.Cash

    Dim Cnt_BankAccount As Integer
    Private iParty_Req As Boolean = False
    Private LB_DOCS_ARRAY As DataTable
    Private LB_EXTENDED_PROPERTY_TABLE As DataTable
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Public Sel_Bank_ID As String = ""
    Public IsProfileCreationVoucher As Boolean = False
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
    Dim FormClosingEnable As Boolean = True
    Private Sub Form_Closing_Window_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then BUT_CANCEL_Click(New Object, New EventArgs)
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'By Item-wise Selection
            Me.DataNavigation("NEW")
        End If
        Me.GLookUp_PartyList1.Focus()
        'If Me.GridView1.RowCount > 0 Then
        '    If Base._IsVolumeCenter And Me.GridView1.GetRowCellValue(Me.GridView1.RowCount - 1, "Item_Led_ID").ToString() = "00083" Then
        '        GridView4.Focus() : Exit Sub
        '    End If
        'End If
        If Base._IsVolumeCenter And GLookUp_PartyList1.Text.Length > 0 Then Me.Txt_V_Date.Focus()
        If Base._IsVolumeCenter And GLookUp_PartyList1.Text.Length > 0 And Me.Txt_V_Date.Text.Length > 0 Then BUT_SAVE_COM.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If (keyData = (Keys.Control Or Keys.A)) Then ' Add Party / Person
                But_PersAdd_Click(Nothing, Nothing)
                Return (True)
            End If
            If (keyData = (Keys.Control Or Keys.M)) Then ' Manage Party / Person
                But_PersManage_Click(Nothing, Nothing)
                Return (True)
            End If
            If (keyData = (Keys.Control Or Keys.S)) Then ' save
                If Me.BUT_SAVE_COM.Enabled Then
                    BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
                    Return (True)
                End If
            End If
            If (keyData = Keys.Insert) Then
                Me.DataNavigation("NEW")
                Return (True)
            End If
            If GridControl1.Focused Then
                If (keyData = Keys.Enter) Then
                    Me.DataNavigation("EDIT")
                    Return (True)
                End If
                If (keyData = Keys.Delete) Then
                    Me.DataNavigation("DELETE")
                    Return (True)
                End If
                If (keyData = Keys.Space) Then
                    Me.DataNavigation("VIEW")
                    Return (True)
                End If
            End If
            If (keyData = (Keys.Alt Or Keys.B)) Then
                Me.Tab_Page_Bank.Show()
                GridView2.Focus()
                If GridView2.RowCount > 0 Then
                    GridView2.FocusedRowHandle = 0
                    GridView2.FocusedColumn = GridView2.Columns(0)
                End If
                Return (True)
            End If
            If (keyData = (Keys.Alt Or Keys.A)) Then
                Me.Tab_Page_Advance.Show()
                GridView3.Focus()
                If GridView3.RowCount > 0 Then
                    GridView3.FocusedRowHandle = 0
                    GridView3.FocusedColumn = GridView3.Columns(0)
                End If
                Return (True)
            End If
            If (keyData = (Keys.Alt Or Keys.L)) Then
                Me.Tab_Page_Liabilities.Show()
                GridView4.Focus()
                If GridView4.RowCount > 0 Then
                    GridView4.FocusedRowHandle = 0
                    GridView4.FocusedColumn = GridView4.Columns(0)
                End If
                Return (True)
            End If
            If GridControl2.Focused Then
                If (keyData = (Keys.Alt Or Keys.N)) Then
                    Me.Tab_Page_Bank.Show()
                    Me.DataNavigation_Bank("NEW")
                    Return (True)
                End If
                If (keyData = Keys.Enter) Then
                    Me.DataNavigation_Bank("EDIT")
                    Return (True)
                End If
                If (keyData = Keys.Delete) Then
                    Me.DataNavigation_Bank("DELETE")
                    Return (True)
                End If
                If (keyData = Keys.Space) Then
                    Me.DataNavigation_LB("VIEW")
                    Return (True)
                End If
            End If
            If GridControl3.Focused Then
                If (keyData = Keys.Enter) Then
                    Me.DataNavigation_Adv("EDIT")
                    Return (True)
                End If
                If (keyData = Keys.Delete) Then
                    Me.DataNavigation_Adv("DELETE")
                    Return (True)
                End If
                If (keyData = Keys.Space) Then
                    Me.DataNavigation_Adv("VIEW")
                    Return (True)
                End If
            End If
            If GridControl4.Focused Then
                If (keyData = Keys.Enter) Then
                    Me.DataNavigation_LB("EDIT")
                    Return (True)
                End If
                If (keyData = Keys.Delete) Then
                    Me.DataNavigation_LB("DELETE")
                    Return (True)
                End If
                If (keyData = Keys.Space) Then
                    Me.DataNavigation_LB("VIEW")
                    Return (True)
                End If
            End If
            If (keyData = (Keys.Alt Or Keys.B Or Keys.N)) Then
                Me.Tab_Page_Bank.Show()
                Me.DataNavigation_Bank("NEW")
                Return (True)
            End If
            
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            If (keyData = (Keys.Control Or Keys.D)) Then 'delete
                BUT_SAVE_Click(BUT_DEL, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.F2)) Then
            Txt_V_Date.Focus()
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub

    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        'call using  Base.AllowMultiuser(), Bank_Detail- dtable,Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
        'DirectCast(GridView2.DataSource, DataTable)
        'DirectCast(GridView3.DataSource,DataTable),'DirectCast(GridView4.DataSource,DataTable),

        Dim IsDeleteConfirmed As Boolean = False
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If Val(Txt_CashAmt.Text) > 10000 Then
                Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog("Message...", "<size=13><b>Cash Payment more than <color=red>Rs. 10,000</color> is not allowed...!</b></size>" & vbNewLine & vbNewLine & "Do you still want to Save...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    xPromptWindow.Dispose()
                    Me.Txt_CashAmt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    xPromptWindow.Dispose()
                End If
            End If
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                IsDeleteConfirmed = True
            Else
                Exit Sub
            End If
            xPromptWindow.Dispose()
        End If



        'START - FOLLOWING IS NOT IN SERVICE
        'Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        'If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        'If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
        'END - FOLLOWING IS NOT IN SERVICE

        'need to check if xID,Rec_ID's datasource is changed. If so we need to convey from server

        Dim paymentVoucherDetails As New Common_Lib.RealTimeService.Param_paymentVoucherDetails()
        Dim paymentVoucherDetailBasicData As New Common_Lib.RealTimeService.Param_BasicData()
        Dim paymentVoucherDetailFormData As New Common_Lib.RealTimeService.Param_paymentVoucherFormData()
        Dim paymentVoucherDetailGlobalData As New Common_Lib.RealTimeService.Param_paymentVoucherGlobalData()

        'Set values
        paymentVoucherDetailBasicData.open_Ins_ID = Base._open_Ins_ID
        paymentVoucherDetailBasicData.open_UID_No = Base._open_UID_No
        paymentVoucherDetailBasicData.open_PAD_No_Main = Base._open_PAD_No_Main
        paymentVoucherDetailBasicData.open_Cen_Rec_ID = Base._open_Cen_Rec_ID
        paymentVoucherDetailBasicData.open_Year_Sdt = Base._open_Year_Sdt
        paymentVoucherDetailBasicData.open_Year_Edt = Base._open_Year_Edt
        paymentVoucherDetailBasicData.prev_Unaudited_YearID = Base._prev_Unaudited_YearID
        paymentVoucherDetailBasicData.next_Unaudited_YearID = Base._next_Unaudited_YearID
        paymentVoucherDetailBasicData.IsMultiUserAllowed = Base.AllowMultiuser() 'Base.AllowMultiuser()
        paymentVoucherDetailBasicData.IsInsuranceAudited = Base.IsInsuranceAudited() 'Base.IsInsuranceAudited()

        'Filled Form Values
        paymentVoucherDetailFormData.NavMode = Me.Tag
        paymentVoucherDetailFormData.Rec_ID = Me.xMID.Text
        paymentVoucherDetailFormData.Txt_AdvAmt = Me.Txt_AdvAmt.Text
        paymentVoucherDetailFormData.Txt_CreditAmt = Me.Txt_CreditAmt.Text
        paymentVoucherDetailFormData.Txt_LB_Amt = Me.Txt_LB_Amt.Text
        paymentVoucherDetailFormData.Txt_CashAmt = Me.Txt_CashAmt.Text
        If IsDate(Txt_V_Date.Text) Then paymentVoucherDetailFormData.Txt_V_Date = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else paymentVoucherDetailFormData.Txt_V_Date = Txt_V_Date.Text
        paymentVoucherDetailFormData.Txt_V_NO = Me.Txt_V_NO.Text
        paymentVoucherDetailFormData.Txt_Inv_No = Me.Txt_Inv_No.Text
        If IsDate(Txt_Inv_Date.Text) Then paymentVoucherDetailFormData.Txt_Inv_Date = Convert.ToDateTime(Txt_Inv_Date.Text).ToString(Base._Server_Date_Format_Short) Else paymentVoucherDetailFormData.Txt_Inv_Date = Txt_Inv_Date.Text
        paymentVoucherDetailFormData.Txt_SubTotal = Me.Txt_SubTotal.Text
        paymentVoucherDetailFormData.Txt_BankAmt = Me.Txt_BankAmt.Text
        paymentVoucherDetailFormData.Txt_TDS_Amt = Me.Txt_TDS_Amt.Text
        If IsDate(Txt_DueDate.Text) Then paymentVoucherDetailFormData.Txt_DueDate = Convert.ToDateTime(Txt_DueDate.Text).ToString(Base._Server_Date_Format_Short) Else paymentVoucherDetailFormData.Txt_DueDate = Txt_DueDate.Text
        paymentVoucherDetailFormData.Txt_Narration = Me.Txt_Narration.Text
        paymentVoucherDetailFormData.Txt_Reference = Me.Txt_Reference.Text
        paymentVoucherDetailFormData.GLookUp_PartyList1_Txt = Me.GLookUp_PartyList1.Text
        paymentVoucherDetailFormData.GLookUp_PartyList1_Tag = Me.GLookUp_PartyList1.Tag
        If Not IsDBNull(Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")) Then
            paymentVoucherDetailFormData.oldREC_EDIT_ON = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
        End If
        If Not GridView1.DataSource Is Nothing Then
            paymentVoucherDetailFormData.GridView1 = DirectCast(GridView1.DataSource, DataView).ToTable()
            For Each dRow As DataRow In paymentVoucherDetailFormData.GridView1.Rows
                If IsDate(dRow("AI_PUR_DATE")) Then dRow("AI_PUR_DATE") = Convert.ToDateTime(dRow("AI_PUR_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LS_INS_DATE")) Then dRow("LS_INS_DATE") = Convert.ToDateTime(dRow("LS_INS_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("VI_REG_DATE")) Then dRow("VI_REG_DATE") = Convert.ToDateTime(dRow("VI_REG_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("VI_INS_EXPIRY_DATE")) Then dRow("VI_INS_EXPIRY_DATE") = Convert.ToDateTime(dRow("VI_INS_EXPIRY_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PAID_DATE")) Then dRow("LB_PAID_DATE") = Convert.ToDateTime(dRow("LB_PAID_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PERIOD_FROM")) Then dRow("LB_PERIOD_FROM") = Convert.ToDateTime(dRow("LB_PERIOD_FROM")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PERIOD_TO")) Then dRow("LB_PERIOD_TO") = Convert.ToDateTime(dRow("LB_PERIOD_TO")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_BILL_DATE")) Then dRow("TP_BILL_DATE") = Convert.ToDateTime(dRow("TP_BILL_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_PERIOD_FROM")) Then dRow("TP_PERIOD_FROM") = Convert.ToDateTime(dRow("TP_PERIOD_FROM")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_PERIOD_TO")) Then dRow("TP_PERIOD_TO") = Convert.ToDateTime(dRow("TP_PERIOD_TO")).ToString(Base._Server_Date_Format_Long)
            Next
            If Not paymentVoucherDetailFormData.GridView1 Is Nothing Then paymentVoucherDetailFormData.GridView1.TableName = "VoucherDetailFormDataGrid1"
        End If
        If Not GridView2.DataSource Is Nothing Then
            paymentVoucherDetailFormData.DTGridView2 = DirectCast(GridView2.DataSource, DataView).ToTable()
            For Each dRow As DataRow In paymentVoucherDetailFormData.DTGridView2.Rows
                If IsDate(dRow("Date")) Then dRow("Date") = Convert.ToDateTime(dRow("Date")).ToString(Base._Server_Date_Format_Short)
                If IsDate(dRow("Clearing Date")) Then dRow("Clearing Date") = Convert.ToDateTime(dRow("Clearing Date")).ToString(Base._Server_Date_Format_Short)
            Next
            If Not paymentVoucherDetailFormData.DTGridView2 Is Nothing Then paymentVoucherDetailFormData.DTGridView2.TableName = "VoucherDetailFormDataGrid2"
        End If
        If Not GridView3.DataSource Is Nothing Then
            paymentVoucherDetailFormData.GridView3 = DirectCast(GridView3.DataSource, DataView).ToTable()
            For Each dRow As DataRow In paymentVoucherDetailFormData.GridView3.Rows
                If IsDate(dRow("Given Date")) Then dRow("Given Date") = Convert.ToDateTime(dRow("Given Date")).ToString(Base._Server_Date_Format_Short)
            Next
            If Not paymentVoucherDetailFormData.GridView3 Is Nothing Then paymentVoucherDetailFormData.GridView3.TableName = "VoucherDetailFormDataGrid3"
        End If
        If Not GridView4.DataSource Is Nothing Then
            paymentVoucherDetailFormData.GridView4 = DirectCast(GridView4.DataSource, DataView).ToTable()
            'For Each drow As DataRow In paymentVoucherDetailFormData.GridView4.Rows
            '    If IsDate(drow("given date")) Then drow("given date") = Convert.ToDateTime(drow("given date")).ToString(Base._Server_Date_Format_Long)
            'Next
            If Not paymentVoucherDetailFormData.GridView4 Is Nothing Then paymentVoucherDetailFormData.GridView4.TableName = "voucherdetailformdatagrid4"
        End If
        paymentVoucherDetailFormData.IsChk_Incompleted = Chk_Incompleted.Checked
        paymentVoucherDetailFormData.xID = Me.xID.Text
        paymentVoucherDetailFormData.TitleX = Me.TitleX.Text
        paymentVoucherDetailFormData.WindowText = Me.Text

        'Form's global variables
        paymentVoucherDetailGlobalData.Bank_Detail = Me.Bank_Detail
        If Not paymentVoucherDetailGlobalData.Bank_Detail Is Nothing Then
            For Each dRow As DataRow In paymentVoucherDetailGlobalData.Bank_Detail.Rows
                If IsDate(dRow("Date")) Then dRow("Date") = Convert.ToDateTime(dRow("Date")).ToString(Base._Server_Date_Format_Short)
                If IsDate(dRow("Clearing Date")) Then dRow("Clearing Date") = Convert.ToDateTime(dRow("Clearing Date")).ToString(Base._Server_Date_Format_Short)
            Next
        End If
        paymentVoucherDetailGlobalData.LastEditedOn = Me.LastEditedOn
        paymentVoucherDetailGlobalData.DT = Me.DT
        If Not paymentVoucherDetailGlobalData.DT Is Nothing Then
            For Each dRow As DataRow In paymentVoucherDetailGlobalData.DT.Rows
                If IsDate(dRow("AI_PUR_DATE")) Then dRow("AI_PUR_DATE") = Convert.ToDateTime(dRow("AI_PUR_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LS_INS_DATE")) Then dRow("LS_INS_DATE") = Convert.ToDateTime(dRow("LS_INS_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("VI_REG_DATE")) Then dRow("VI_REG_DATE") = Convert.ToDateTime(dRow("VI_REG_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("VI_INS_EXPIRY_DATE")) Then dRow("VI_INS_EXPIRY_DATE") = Convert.ToDateTime(dRow("VI_INS_EXPIRY_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PAID_DATE")) Then dRow("LB_PAID_DATE") = Convert.ToDateTime(dRow("LB_PAID_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PERIOD_FROM")) Then dRow("LB_PERIOD_FROM") = Convert.ToDateTime(dRow("LB_PERIOD_FROM")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("LB_PERIOD_TO")) Then dRow("LB_PERIOD_TO") = Convert.ToDateTime(dRow("LB_PERIOD_TO")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_BILL_DATE")) Then dRow("TP_BILL_DATE") = Convert.ToDateTime(dRow("TP_BILL_DATE")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_PERIOD_FROM")) Then dRow("TP_PERIOD_FROM") = Convert.ToDateTime(dRow("TP_PERIOD_FROM")).ToString(Base._Server_Date_Format_Long)
                If IsDate(dRow("TP_PERIOD_TO")) Then dRow("TP_PERIOD_TO") = Convert.ToDateTime(dRow("TP_PERIOD_TO")).ToString(Base._Server_Date_Format_Long)
            Next
        End If
        If Not paymentVoucherDetailGlobalData.DT Is Nothing Then paymentVoucherDetailGlobalData.DT.TableName = "DT"
        paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE = Me.LB_EXTENDED_PROPERTY_TABLE
        If Not paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE Is Nothing Then paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE.TableName = "LB_EXTENDED_PROPERTY_TABLE"
        paymentVoucherDetailGlobalData.LB_DOCS_ARRAY = Me.LB_DOCS_ARRAY
        If Not paymentVoucherDetailGlobalData.LB_DOCS_ARRAY Is Nothing Then paymentVoucherDetailGlobalData.LB_DOCS_ARRAY.TableName = "LB_DOCS_ARRAY"

        paymentVoucherDetailGlobalData.iParty_Req = Me.iParty_Req

        paymentVoucherDetails.BasicData = paymentVoucherDetailBasicData
        paymentVoucherDetails.FormData = paymentVoucherDetailFormData
        paymentVoucherDetails.GlobalData = paymentVoucherDetailGlobalData

        'Service Call
        Dim retPaymentSaveChecks As Common_Lib.RealTimeService.Param_SaveButtonChecks = Base._Voucher_DBOps.GetPaymentSaveChecks(paymentVoucherDetails)
        Dim myControl1 As Control
        'To find component
        Dim _DEPfrm As New Common_Lib.FindComponent(Me)
        If Not retPaymentSaveChecks Is Nothing Then
            If (retPaymentSaveChecks.ToolTipText.Length > 0) Then
                Me.ToolTip1.ToolTipTitle = retPaymentSaveChecks.ToolTipTitle
                'Get component
                Dim foundControl As Object = Nothing
                foundControl = _DEPfrm.FindInAllObjects(retPaymentSaveChecks.focusControlId)
                If TypeOf foundControl Is DevExpress.XtraGrid.Views.Grid.GridView Then
                    Dim grid As DevExpress.XtraGrid.Views.Grid.GridView = DirectCast(foundControl, DevExpress.XtraGrid.Views.Grid.GridView)
                    Me.ToolTip1.Show(retPaymentSaveChecks.ToolTipText, grid.GridControl, 0, grid.GridControl.Height, 5000)
                    grid.Focus()
                Else
                    myControl1 = foundControl
                    Me.ToolTip1.Show(retPaymentSaveChecks.ToolTipText, myControl1, 0, myControl1.Height, 5000)
                    myControl1.Focus()
                End If
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If (retPaymentSaveChecks.messageBoxText.Length > 0) Then
                If (retPaymentSaveChecks.focusControlId.Length > 0) Then
                    Dim foundControl As Object = Nothing
                    foundControl = _DEPfrm.FindInAllObjects(retPaymentSaveChecks.focusControlId)
                    If TypeOf foundControl Is DevExpress.XtraGrid.Views.Grid.GridView Then
                        Dim grid As DevExpress.XtraGrid.Views.Grid.GridView = DirectCast(foundControl, DevExpress.XtraGrid.Views.Grid.GridView)
                        grid.Focus()
                    Else
                        myControl1 = foundControl
                        myControl1.Focus()
                    End If
                End If
                If (retPaymentSaveChecks.SelectedTabPage.Length > 0) Then
                    XtraTabControl1.SelectedTabPage = _DEPfrm.FindInAllObjects(retPaymentSaveChecks.focusControlId)
                End If
                Dim msgIcon As MessageBoxIcon
                If retPaymentSaveChecks.messageIcon.ToLower = "asterisk" Then
                    msgIcon = MessageBoxIcon.Asterisk
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "error" Then
                    msgIcon = MessageBoxIcon.Error
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "exclamation" Then
                    msgIcon = MessageBoxIcon.Exclamation
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "hand" Then
                    msgIcon = MessageBoxIcon.Hand
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "information" Then
                    msgIcon = MessageBoxIcon.Information
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "none" Then
                    msgIcon = MessageBoxIcon.None
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "question" Then
                    msgIcon = MessageBoxIcon.Question
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "stop" Then
                    msgIcon = MessageBoxIcon.Stop
                ElseIf retPaymentSaveChecks.messageIcon.ToLower = "warning" Then
                    msgIcon = MessageBoxIcon.Warning
                End If
                If (retPaymentSaveChecks.dialogResult.Length > 0) Then
                    'We have None, retry and ok dialogresult
                    Me.DialogResult = IIf(retPaymentSaveChecks.dialogResult = Windows.Forms.DialogResult.Retry.ToString(), Windows.Forms.DialogResult.Retry, IIf(retPaymentSaveChecks.dialogResult = Windows.Forms.DialogResult.None.ToString(), Windows.Forms.DialogResult.None, Windows.Forms.DialogResult.OK))
                    DevExpress.XtraEditors.XtraMessageBox.Show(retPaymentSaveChecks.messageBoxText, retPaymentSaveChecks.messageCaption, MessageBoxButtons.OK, msgIcon) ', retPaymentSaveChecks.messageIcon As MessageBoxIcon)
                    'Close the popup for retry and ok messages
                    If (retPaymentSaveChecks.dialogResult <> Windows.Forms.DialogResult.None.ToString()) Then
                        FormClosingEnable = False : Me.Close()
                    End If
                End If
                Exit Sub
            End If
        Else
            FormClosingEnable = False : Me.Close()
        End If
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Cancel this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                Hide_Properties()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                FormClosingEnable = False : Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.None
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Party)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Party Detail)..." : xfrm.TitleX.Text = "Party Detail"
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, But_PersManage.GotFocus, But_PersAdd.GotFocus, BUT_NEW.GotFocus, BUT_EDIT.GotFocus, BUT_DELETE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, But_PersManage.LostFocus, But_PersAdd.LostFocus, BUT_NEW.LostFocus, BUT_EDIT.LostFocus, BUT_DELETE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, But_PersManage.KeyDown, But_PersAdd.KeyDown, BUT_NEW.KeyDown, BUT_EDIT.KeyDown, BUT_DELETE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click, BUT_DELETE.Click, T_New.Click, T_Edit.Click, T_Delete.Click, BUT_NEW_B.Click, BUT_EDIT_B.Click, BUT_DELETE_B.Click, T_NEW_B.Click, T_EDIT_B.Click, T_DELETE_B.Click, BUT_EDIT_A.Click, T_EDIT_A.Click, T_DELETE_A.Click, BUT_DELETE_A.Click, T_EDIT_L.Click, T_DELETE_L.Click, BUT_EDIT_L.Click, BUT_DELETE_L.Click, BUT_VIEW.Click, T_VIEW.Click, BUT_VIEW_B.Click, T_VIEW_B.Click, BUT_VIEW_A.Click, T_VIEW_A.Click, BUT_VIEW_L.Click, T_VIEW_L.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(btn.Name) = "BUT_VIEW" Then Me.DataNavigation("VIEW")

            If UCase(btn.Name) = "BUT_NEW_B" Then Me.DataNavigation_Bank("NEW")
            If UCase(btn.Name) = "BUT_EDIT_B" Then Me.DataNavigation_Bank("EDIT")
            If UCase(btn.Name) = "BUT_DELETE_B" Then Me.DataNavigation_Bank("DELETE")
            If UCase(btn.Name) = "BUT_VIEW_B" Then Me.DataNavigation_Bank("VIEW")

            If UCase(btn.Name) = "BUT_EDIT_A" Then Me.DataNavigation_Adv("EDIT")
            If UCase(btn.Name) = "BUT_DELETE_A" Then Me.DataNavigation_Adv("DELETE")
            If UCase(btn.Name) = "BUT_VIEW_A" Then Me.DataNavigation_Adv("VIEW")

            If UCase(btn.Name) = "BUT_EDIT_L" Then Me.DataNavigation_LB("EDIT")
            If UCase(btn.Name) = "BUT_DELETE_L" Then Me.DataNavigation_LB("DELETE")
            If UCase(btn.Name) = "BUT_VIEW_L" Then Me.DataNavigation_LB("VIEW")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(T_btn.Name) = "T_VIEW" Then Me.DataNavigation("VIEW")

            If UCase(T_btn.Name) = "T_NEW_B" Then Me.DataNavigation_Bank("NEW")
            If UCase(T_btn.Name) = "T_EDIT_B" Then Me.DataNavigation_Bank("EDIT")
            If UCase(T_btn.Name) = "T_DELETE_B" Then Me.DataNavigation_Bank("DELETE")
            If UCase(T_btn.Name) = "T_VIEW_B" Then Me.DataNavigation_Bank("VIEW")

            If UCase(T_btn.Name) = "T_EDIT_A" Then Me.DataNavigation_Adv("EDIT")
            If UCase(T_btn.Name) = "T_DELETE_A" Then Me.DataNavigation_Adv("DELETE")
            If UCase(T_btn.Name) = "T_VIEW_A" Then Me.DataNavigation_Adv("VIEW")

            If UCase(T_btn.Name) = "T_EDIT_L" Then Me.DataNavigation_LB("EDIT")
            If UCase(T_btn.Name) = "T_DELETE_L" Then Me.DataNavigation_LB("DELETE")
            If UCase(T_btn.Name) = "T_VIEW_L" Then Me.DataNavigation_LB("VIEW")
        End If

    End Sub

#End Region

#Region "Start--> TextBox Events"
    Private Sub Item_Msg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Item_Msg.Click
        Me.GridView1.Focus()
    End Sub
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown, CheckEdit2.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_CashAmt.GotFocus, Txt_CashAmt.Click, Txt_CreditAmt.GotFocus, Txt_CreditAmt.Click, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Inv_No.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_CashAmt.Name Or txt.Name = Txt_CreditAmt.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub

    Private Sub TxtLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_CashAmt.LostFocus, Txt_CreditAmt.LostFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_CashAmt.Name Or txt.Name = Txt_CreditAmt.Name Then
            txt.Text = Format(ConvertAsDecimal(txt.Text), "#0.00")
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Narration.KeyPress, Txt_Reference.KeyPress, Txt_Inv_No.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_CashAmt.KeyDown, Txt_CreditAmt.KeyDown, Txt_Inv_Date.KeyDown, Txt_Inv_No.KeyDown, Txt_V_Date.KeyDown, Txt_Reference.KeyDown, Txt_Narration.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Narration.Validated, Txt_Reference.Validated, Txt_Inv_No.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If Me.GridView1.OptionsView.ShowPreview Then
            Me.GridView1.OptionsView.ShowPreview = False
        Else
            Me.GridView1.OptionsView.ShowPreview = True
        End If
    End Sub
    'Private Sub Txt_Inv_Date_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Inv_Date.LostFocus
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
    '        If IsDate(Me.Txt_Inv_Date.Text) And IsDate(Me.Txt_V_Date.Text) = False Then
    '            Me.Txt_V_Date.DateTime = Me.Txt_Inv_Date.DateTime
    '        End If
    '    End If
    'End Sub
    Private Sub Txt_V_Date_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles Txt_V_Date.EditValueChanging
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            If Not e.OldValue Is Nothing Then
                If e.OldValue.ToString.Length > 0 Then
                    If IsProfileCreationVoucher Then
                        Dim TxnDates As DataTable = Base._Payment_DBOps.Get_CreatedAssets_MinTxnDate(Me.xMID.Text, e.NewValue)
                        If TxnDates.Rows.Count > 0 Then
                            Dim Message As String = "A Transaction has been posted againt " & TxnDates(0)("ProfName") & " created in current voucher , for Rs." & TxnDates(0)("Amount").ToString & " on date(" & TxnDates(0)("MinTxnDate").ToString() & ") which is less than voucher date specified by you." & vbNewLine & "Please specify a lower Voucher Date!!"
                            DevExpress.XtraEditors.XtraMessageBox.Show("Specified Voucher Date not allowed!!" & vbNewLine & vbNewLine & Message, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            e.Cancel = True
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Amount_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_CashAmt.EditValueChanged, Txt_BankAmt.EditValueChanged, Txt_AdvAmt.EditValueChanged, Txt_CreditAmt.EditValueChanged
        Me.Txt_DiffAmt.Text = ConvertAsDecimal(Txt_SubTotal.Text) - (ConvertAsDecimal(Me.Txt_CashAmt.Text) + ConvertAsDecimal(Me.Txt_BankAmt.Text) + ConvertAsDecimal(Me.Txt_AdvAmt.Text) + ConvertAsDecimal(Me.Txt_CreditAmt.Text))
        Calculation_Check()
        If ConvertAsDecimal(Me.Txt_CreditAmt.Text) > 0 Then
            Me.Txt_DueDate.Enabled = True : Me.Txt_DueDate.EditValue = ""
        Else
            Me.Txt_DueDate.Enabled = False : Me.Txt_DueDate.EditValue = ""
        End If
    End Sub
    Private Sub Difference_Calculation()
        If (ConvertAsDecimal(Me.Txt_BankAmt.Text) + ConvertAsDecimal(Me.Txt_AdvAmt.Text) + ConvertAsDecimal(Me.Txt_CreditAmt.Text)) > 0 Then
            Me.Txt_CashAmt.Text = "0.00"
            Me.Txt_DiffAmt.Text = ConvertAsDecimal(Txt_SubTotal.Text) - (ConvertAsDecimal(Me.Txt_CashAmt.Text) + ConvertAsDecimal(Me.Txt_BankAmt.Text) + ConvertAsDecimal(Me.Txt_AdvAmt.Text) + ConvertAsDecimal(Me.Txt_CreditAmt.Text))
        Else
            Me.Txt_DiffAmt.Text = 0
            If Base._IsVolumeCenter And SelectedPaymentType = PaymentType.Credit Then
                Me.Txt_CreditAmt.Text = Format(ConvertAsDecimal(Txt_SubTotal.Text), "#0.00")
            Else
                Me.Txt_CashAmt.Text = Format(ConvertAsDecimal(Txt_SubTotal.Text), "#0.00")
            End If
        End If
    End Sub
    Private Sub Calculation_Check()
        If Val(Txt_DiffAmt.Text) <> 0 Then
            Me.BUT_SAVE_COM.Enabled = False
            Txt_DiffAmt.Properties.AppearanceDisabled.BackColor = Color.LightSalmon
        Else
            Me.BUT_SAVE_COM.Enabled = True
            Txt_DiffAmt.Properties.AppearanceDisabled.BackColor = Color.AliceBlue
        End If

    End Sub

    Private Sub Sub_Amt_Calculation(ByVal Delete_Action As Boolean)
        Me.GridView1.ClearSorting()
        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Decimal = 0 : Dim xTDS As Decimal = 0
        For I As Integer = 0 To Me.GridView1.RowCount - 1
            If Delete_Action Then Me.GridView1.SetRowCellValue(I, "Sr.", I + 1)
            xAmt += ConvertAsDecimal(Me.GridView1.GetRowCellValue(I, "Amount").ToString())
            xTDS += ConvertAsDecimal(Me.GridView1.GetRowCellValue(I, "TDS").ToString())
            If Me.GridView1.GetRowCellValue(I, "Item_Party_Req").ToString.ToUpper.Trim = "YES" Then
                iParty_Req = True
            End If
        Next
        Txt_SubTotal.Text = Format(xAmt, "#0.00")
        Txt_TDS_Amt.Text = Format(xTDS, "#0.00")
        Me.GridView1.RefreshData()
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        '---------------
        If (ConvertAsDecimal(Me.Txt_BankAmt.Text) + ConvertAsDecimal(Me.Txt_AdvAmt.Text) + ConvertAsDecimal(Me.Txt_CreditAmt.Text)) > 0 Then
            Me.Txt_DiffAmt.Text = ConvertAsDecimal(Txt_SubTotal.Text) - (ConvertAsDecimal(Me.Txt_CashAmt.Text) + ConvertAsDecimal(Me.Txt_BankAmt.Text) + ConvertAsDecimal(Me.Txt_AdvAmt.Text) + ConvertAsDecimal(Me.Txt_CreditAmt.Text))
        Else
            Me.Txt_DiffAmt.Text = 0
            If Base._IsVolumeCenter And SelectedPaymentType = PaymentType.Credit Then 'And Me.GridView1.GetRowCellValue(Me.GridView1.RowCount - 1, "Item_Led_ID").ToString() <> "00083" 
                Me.Txt_CreditAmt.Text = Format(ConvertAsDecimal(Txt_SubTotal.Text), "#0.00")
            Else
                Me.Txt_CashAmt.Text = Format(ConvertAsDecimal(Txt_SubTotal.Text), "#0.00")
            End If
        End If
        Difference_Calculation()
        Calculation_Check()
        If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
    End Sub
    Private Sub Bank_Amt_Calculation(ByVal Delete_Action As Boolean)
        Me.GridView2.ClearSorting()
        Me.GridView2.SortInfo.Add(Me.GridView2.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Decimal = 0
        For I As Integer = 0 To Me.GridView2.RowCount - 1
            If Delete_Action Then Me.GridView2.SetRowCellValue(I, "Sr.", I + 1)
            xAmt += ConvertAsDecimal(Me.GridView2.GetRowCellValue(I, "Amount").ToString())
        Next
        Txt_BankAmt.EditValue = Format(xAmt, "#0.00")
        Me.GridView2.RefreshData()
        Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
        '---------------
        Difference_Calculation()
        Calculation_Check()
    End Sub
    Private Sub Advance_Amt_Calculation()
        Me.GridView3.ClearSorting()
        If Me.GridView3.RowCount > 0 Then Me.GridView3.SortInfo.Add(Me.GridView3.Columns("Sr"), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Decimal = 0
        For I As Integer = 0 To Me.GridView3.RowCount - 1
            xAmt += ConvertAsDecimal(Me.GridView3.GetRowCellValue(I, "Payment").ToString())
        Next
        Txt_AdvAmt.Text = Format(xAmt, "#0.00")
        Me.GridView3.RefreshData()
        Me.GridView3.BestFitMaxRowCount = 10 : Me.GridView3.BestFitColumns()
        '---------------
        Difference_Calculation()
        Calculation_Check()
    End Sub
    Private Sub LB_Amt_Calculation()
        Me.GridView4.ClearSorting()
        If Me.GridView4.RowCount > 0 Then Me.GridView4.SortInfo.Add(Me.GridView4.Columns("Sr"), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Decimal = 0
        For I As Integer = 0 To Me.GridView4.RowCount - 1
            xAmt += ConvertAsDecimal(Me.GridView4.GetRowCellValue(I, "Payment").ToString())
        Next
        Txt_LB_Amt.Text = Format(xAmt, "#0.00")
        Me.GridView4.RefreshData()
        Me.GridView4.BestFitMaxRowCount = 10 : Me.GridView4.BestFitColumns()
        '---------------
        'Difference_Calculation()
        Calculation_Check()
    End Sub

#End Region

#Region "Start--> Custom Grid Setting"

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("Item_Detail")
    Friend Bank As New DataSet() : Friend Bank_Detail As DataTable = Bank.Tables.Add("Bank_Detail")
    Private Sub SetGridData()

        '--------------------ITEM DETAIL
        With DT
            .Columns.Add("Sr.", Type.GetType("System.Int32"))
            .Columns.Add("Item_ID", Type.GetType("System.String"))
            .Columns.Add("Item_Led_ID", Type.GetType("System.String"))
            .Columns.Add("Item_Trans_Type", Type.GetType("System.String"))
            .Columns.Add("Item_Party_Req", Type.GetType("System.String"))
            .Columns.Add("Item_Profile", Type.GetType("System.String"))
            .Columns.Add("Item Name", Type.GetType("System.String"))
            .Columns.Add("ITEM_VOUCHER_TYPE", Type.GetType("System.String"))
            .Columns.Add("Head", Type.GetType("System.String"))
            .Columns.Add("Qty.", Type.GetType("System.Double"))
            .Columns.Add("Unit", Type.GetType("System.String"))
            .Columns.Add("Rate", Type.GetType("System.Double"))
            .Columns.Add("TDS", Type.GetType("System.Double"))
            .Columns.Add("Amount", Type.GetType("System.Decimal"))
            .Columns.Add("Remarks", Type.GetType("System.String"))
            .Columns.Add("Pur_ID", Type.GetType("System.String")) 'Purpose ID
            .Columns.Add("LOC_ID", Type.GetType("System.String"))
            .Columns.Add("CREATION_PROF_REC_ID", Type.GetType("System.String"))
            '---Gold/Silver-----
            .Columns.Add("GS_DESC_MISC_ID", Type.GetType("System.String"))
            .Columns.Add("GS_ITEM_WEIGHT", Type.GetType("System.Decimal"))
            '---Other Asset-----
            .Columns.Add("AI_TYPE", Type.GetType("System.String"))
            .Columns.Add("AI_MAKE", Type.GetType("System.String"))
            .Columns.Add("AI_MODEL", Type.GetType("System.String"))
            .Columns.Add("AI_SERIAL_NO", Type.GetType("System.String"))
            .Columns.Add("AI_PUR_DATE", Type.GetType("System.String"))
            .Columns.Add("AI_WARRANTY", Type.GetType("System.Double"))
            .Columns.Add("AI_IMAGE", Type.GetType("System.Byte[]"))
            '-LIVE STOCK-----
            .Columns.Add("LS_NAME", Type.GetType("System.String"))
            .Columns.Add("LS_BIRTH_YEAR", Type.GetType("System.String"))
            .Columns.Add("LS_INSURANCE", Type.GetType("System.String"))
            .Columns.Add("LS_INSURANCE_ID", Type.GetType("System.String"))
            .Columns.Add("LS_INS_POLICY_NO", Type.GetType("System.String"))
            .Columns.Add("LS_INS_AMT", Type.GetType("System.Double"))
            .Columns.Add("LS_INS_DATE", Type.GetType("System.String"))
            '---Vehicles------
            .Columns.Add("VI_MAKE", Type.GetType("System.String"))
            .Columns.Add("VI_MODEL", Type.GetType("System.String"))
            .Columns.Add("VI_REG_NO_PATTERN", Type.GetType("System.String"))
            .Columns.Add("VI_REG_NO", Type.GetType("System.String"))
            .Columns.Add("VI_REG_DATE", Type.GetType("System.String"))
            .Columns.Add("VI_OWNERSHIP", Type.GetType("System.String"))
            .Columns.Add("VI_OWNERSHIP_AB_ID", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_RC_BOOK", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_AFFIDAVIT", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_WILL", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_TRF_LETTER", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_FU_LETTER", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_OTHERS", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_NAME", Type.GetType("System.String"))
            .Columns.Add("VI_INSURANCE_ID", Type.GetType("System.String"))
            .Columns.Add("VI_INS_POLICY_NO", Type.GetType("System.String"))
            .Columns.Add("VI_INS_EXPIRY_DATE", Type.GetType("System.String"))
            '-----Land&Building-----
            .Columns.Add("LB_PRO_TYPE", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_CATEGORY", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_USE", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_NAME", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_ADDRESS", Type.GetType("System.String"))
            .Columns.Add("LB_OWNERSHIP", Type.GetType("System.String"))
            .Columns.Add("LB_OWNERSHIP_PARTY_ID", Type.GetType("System.String"))
            .Columns.Add("LB_SURVEY_NO", Type.GetType("System.String"))
            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
            .Columns.Add("LB_RCC_ROOF", Type.GetType("System.String"))
            .Columns.Add("LB_DEPOSIT_AMT", Type.GetType("System.Double"))
            .Columns.Add("LB_PAID_DATE", Type.GetType("System.String"))
            .Columns.Add("LB_MONTH_RENT", Type.GetType("System.Double"))
            .Columns.Add("LB_MONTH_O_PAYMENTS", Type.GetType("System.Double"))
            .Columns.Add("LB_PERIOD_FROM", Type.GetType("System.String"))
            .Columns.Add("LB_PERIOD_TO", Type.GetType("System.String"))
            .Columns.Add("LB_DOC_OTHERS", Type.GetType("System.String"))
            .Columns.Add("LB_DOC_NAME", Type.GetType("System.String"))
            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
            .Columns.Add("LB_REC_EDIT_ON", Type.GetType("System.DateTime"))
            .Columns.Add("LB_ADDRESS1", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS2", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS3", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS4", Type.GetType("System.String"))
            .Columns.Add("LB_STATE_ID", Type.GetType("System.String"))
            .Columns.Add("LB_DISTRICT_ID", Type.GetType("System.String"))
            .Columns.Add("LB_CITY_ID", Type.GetType("System.String"))
            .Columns.Add("LB_PINCODE", Type.GetType("System.String"))
            '---Telephone-----
            .Columns.Add("TP_ID", Type.GetType("System.String"))
            .Columns.Add("TP_BILL_NO", Type.GetType("System.String"))
            .Columns.Add("TP_BILL_DATE", Type.GetType("System.String"))
            .Columns.Add("TP_PERIOD_FROM", Type.GetType("System.String"))
            .Columns.Add("TP_PERIOD_TO", Type.GetType("System.String"))
            '--WIP--
            .Columns.Add("REF_REC_ID", Type.GetType("System.String"))
            .Columns.Add("REFERENCE", Type.GetType("System.String"))
            .Columns.Add("WIP_REF_TYPE", Type.GetType("System.String"))
            '--TDS--
            .Columns.Add("REF_TDS_DED", Type.GetType("System.String"))
        End With
        Me.GridControl1.DataSource = DS : Me.GridControl1.DataMember = "Item_Detail"
        Me.GridView1.Columns("Sr.").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        Me.GridView1.Columns("Sr.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Sr.").Width = 40
        Me.GridView1.Columns("Item Name").Width = 200
        Me.GridView1.Columns("Head").Width = 164
        Me.GridView1.Columns("Qty.").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Qty.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Qty.").Width = 40
        Me.GridView1.Columns("Unit").Width = 40
        Me.GridView1.Columns("Rate").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Rate").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Rate").Width = 70
        Me.GridView1.Columns("Amount").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Amount").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Amount").Width = 100
        Me.GridView1.Columns("TDS").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("TDS").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("TDS").Width = 70
        Me.GridView1.Columns("Item_ID").Visible = False
        Me.GridView1.Columns("Item_Led_ID").Visible = False
        Me.GridView1.Columns("Item_Trans_Type").Visible = False
        Me.GridView1.Columns("Item_Party_Req").Visible = False
        Me.GridView1.Columns("Item_Profile").Visible = False
        Me.GridView1.Columns("ITEM_VOUCHER_TYPE").Visible = False
        Me.GridView1.Columns("Remarks").Visible = False
        Me.GridView1.Columns("Pur_ID").Visible = False
        Me.GridView1.Columns("LOC_ID").Visible = False
        Me.GridView1.Columns("CREATION_PROF_REC_ID").Visible = False
        '---Gold/Silver-----
        Me.GridView1.Columns("GS_DESC_MISC_ID").Visible = False
        Me.GridView1.Columns("GS_ITEM_WEIGHT").Visible = False
        '---Other Asset-----
        Me.GridView1.Columns("AI_TYPE").Visible = False
        Me.GridView1.Columns("AI_MAKE").Visible = False
        Me.GridView1.Columns("AI_MODEL").Visible = False
        Me.GridView1.Columns("AI_SERIAL_NO").Visible = False
        Me.GridView1.Columns("AI_PUR_DATE").Visible = False
        Me.GridView1.Columns("AI_WARRANTY").Visible = False
        Me.GridView1.Columns("AI_IMAGE").Visible = False
        '---Livestock-----
        Me.GridView1.Columns("LS_NAME").Visible = False
        Me.GridView1.Columns("LS_BIRTH_YEAR").Visible = False
        Me.GridView1.Columns("LS_INSURANCE").Visible = False
        Me.GridView1.Columns("LS_INSURANCE_ID").Visible = False
        Me.GridView1.Columns("LS_INS_POLICY_NO").Visible = False
        Me.GridView1.Columns("LS_INS_AMT").Visible = False
        Me.GridView1.Columns("LS_INS_DATE").Visible = False
        '---Vehicles------
        Me.GridView1.Columns("VI_MAKE").Visible = False
        Me.GridView1.Columns("VI_MODEL").Visible = False
        Me.GridView1.Columns("VI_REG_NO_PATTERN").Visible = False
        Me.GridView1.Columns("VI_REG_NO").Visible = False
        Me.GridView1.Columns("VI_REG_DATE").Visible = False
        Me.GridView1.Columns("VI_OWNERSHIP").Visible = False
        Me.GridView1.Columns("VI_OWNERSHIP_AB_ID").Visible = False
        Me.GridView1.Columns("VI_DOC_RC_BOOK").Visible = False
        Me.GridView1.Columns("VI_DOC_AFFIDAVIT").Visible = False
        Me.GridView1.Columns("VI_DOC_WILL").Visible = False
        Me.GridView1.Columns("VI_DOC_TRF_LETTER").Visible = False
        Me.GridView1.Columns("VI_DOC_FU_LETTER").Visible = False
        Me.GridView1.Columns("VI_DOC_OTHERS").Visible = False
        Me.GridView1.Columns("VI_DOC_NAME").Visible = False
        Me.GridView1.Columns("VI_INSURANCE_ID").Visible = False
        Me.GridView1.Columns("VI_INS_POLICY_NO").Visible = False
        Me.GridView1.Columns("VI_INS_EXPIRY_DATE").Visible = False
        '----Land&Building-----
        Me.GridView1.Columns("LB_PRO_TYPE").Visible = False
        Me.GridView1.Columns("LB_PRO_CATEGORY").Visible = False
        Me.GridView1.Columns("LB_PRO_USE").Visible = False
        Me.GridView1.Columns("LB_PRO_NAME").Visible = False
        Me.GridView1.Columns("LB_PRO_ADDRESS").Visible = False
        Me.GridView1.Columns("LB_OWNERSHIP").Visible = False
        Me.GridView1.Columns("LB_OWNERSHIP_PARTY_ID").Visible = False
        Me.GridView1.Columns("LB_SURVEY_NO").Visible = False
        Me.GridView1.Columns("LB_TOT_P_AREA").Visible = False
        Me.GridView1.Columns("LB_CON_AREA").Visible = False
        Me.GridView1.Columns("LB_CON_YEAR").Visible = False
        Me.GridView1.Columns("LB_RCC_ROOF").Visible = False
        Me.GridView1.Columns("LB_DEPOSIT_AMT").Visible = False
        Me.GridView1.Columns("LB_PAID_DATE").Visible = False
        Me.GridView1.Columns("LB_MONTH_RENT").Visible = False
        Me.GridView1.Columns("LB_MONTH_O_PAYMENTS").Visible = False
        Me.GridView1.Columns("LB_PERIOD_FROM").Visible = False
        Me.GridView1.Columns("LB_PERIOD_TO").Visible = False
        Me.GridView1.Columns("LB_DOC_OTHERS").Visible = False
        Me.GridView1.Columns("LB_DOC_NAME").Visible = False
        Me.GridView1.Columns("LB_OTHER_DETAIL").Visible = False
        Me.GridView1.Columns("LB_REC_ID").Visible = False
        Me.GridView1.Columns("LB_REC_EDIT_ON").Visible = False
        Me.GridView1.Columns("LB_ADDRESS1").Visible = False
        Me.GridView1.Columns("LB_ADDRESS2").Visible = False
        Me.GridView1.Columns("LB_ADDRESS3").Visible = False
        Me.GridView1.Columns("LB_ADDRESS4").Visible = False
        Me.GridView1.Columns("LB_STATE_ID").Visible = False
        Me.GridView1.Columns("LB_DISTRICT_ID").Visible = False
        Me.GridView1.Columns("LB_CITY_ID").Visible = False
        Me.GridView1.Columns("LB_PINCODE").Visible = False
        '----Telephone-----
        Me.GridView1.Columns("TP_ID").Visible = False
        Me.GridView1.Columns("TP_BILL_NO").Visible = False
        Me.GridView1.Columns("TP_BILL_DATE").Visible = False
        Me.GridView1.Columns("TP_PERIOD_FROM").Visible = False
        Me.GridView1.Columns("TP_PERIOD_TO").Visible = False
        '--WIP--
        Me.GridView1.Columns("REF_REC_ID").Visible = False
        Me.GridView1.Columns("REFERENCE").Visible = False
        Me.GridView1.Columns("WIP_REF_TYPE").Visible = False
        '--TDS--
        Me.GridView1.Columns("REF_TDS_DED").Visible = False


        Me.GridView1.PreviewFieldName = "Remarks"

        Me.GridView1.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Amount").DisplayFormat.FormatString = "#0.00"
        Me.GridView1.Columns("TDS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("TDS").DisplayFormat.FormatString = "#0.00"
        Me.GridView1.Columns("Rate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Rate").DisplayFormat.FormatString = "#0.00"

        '--------------------BANK DETAIL
        With Bank_Detail
            .Columns.Add("Sr.", Type.GetType("System.Int32"))
            .Columns.Add("Amount", Type.GetType("System.Decimal"))
            .Columns.Add("Mode", Type.GetType("System.String"))
            .Columns.Add("No.", Type.GetType("System.String"))
            .Columns.Add("Date", Type.GetType("System.String"))
            .Columns.Add("Clearing Date", Type.GetType("System.String"))
            .Columns.Add("Bank Name", Type.GetType("System.String"))
            .Columns.Add("Branch", Type.GetType("System.String"))
            .Columns.Add("A/c. No.", Type.GetType("System.String"))
            .Columns.Add("ID", Type.GetType("System.String"))
            'BANK-CBS/NEFT/RTGS................................
            .Columns.Add("MT_BANK_ID", Type.GetType("System.String"))
            .Columns.Add("Money Transfer Bank", Type.GetType("System.String"))
            .Columns.Add("Ref. A/c. No.", Type.GetType("System.String"))
            .Columns.Add("Edit Time", Type.GetType("System.DateTime")) 'Added for Multi User check for bank accounts
        End With
        Me.GridControl2.DataSource = Bank
        Me.GridControl2.DataMember = "Bank_Detail"
        Me.GridView2.Columns("MT_BANK_ID").Visible = False
        Me.GridView2.Columns("ID").Visible = False
        Me.GridView2.Columns("Edit Time").Visible = False
        Me.GridView2.Columns("Sr.").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        Me.GridView2.Columns("Sr.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView2.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView2.Columns("Amount").DisplayFormat.FormatString = "#0.00"

        Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()

        ''--------------------ADVANCE DETAIL
        'With Adv_Detail
        '    .Columns.Add("Sr.", Type.GetType("System.Int32"))
        '    .Columns.Add("Amount", Type.GetType("System.Double"))
        '    .Columns.Add("Given Date", Type.GetType("System.String"))
        '    .Columns.Add("Purpose", Type.GetType("System.String"))
        '    .Columns.Add("Other Details", Type.GetType("System.String"))
        '    .Columns.Add("ID", Type.GetType("System.String"))
        'End With
        'Me.GridControl3.DataSource = Advances
        'Me.GridControl3.DataMember = "Adv_Detail"
        'Me.GridView3.Columns("ID").Visible = False
        'Me.GridView3.Columns("Sr.").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'Me.GridView3.Columns("Sr.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        'Me.GridView3.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        'Me.GridView3.Columns("Amount").DisplayFormat.FormatString = "#0.00"
        'Me.GridView3.BestFitMaxRowCount = 10 :  Me.GridView3.BestFitColumns
        '
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation("EDIT")
        End If
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If e.KeyCode = Keys.Insert Then
                e.SuppressKeyPress = True
                Me.DataNavigation("NEW")
            End If
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                Me.DataNavigation("EDIT")
            End If
            If e.KeyCode = Keys.Delete Then
                e.SuppressKeyPress = True
                Me.DataNavigation("DELETE")
            End If
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation("VIEW")
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Me.Txt_V_Date.Focus()
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = False
            Me.Txt_CashAmt.Focus()
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation_Bank("EDIT")
        End If
    End Sub
    Private Sub GridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = False
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation_Bank("VIEW")
        End If
    End Sub

    Private Sub GridView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation_Adv("EDIT")
        End If
    End Sub
    Private Sub GridView3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView3.KeyDown
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = False
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation_Adv("VIEW")
        End If
    End Sub
    Private Sub GridView3_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView3.RowCellStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If e.Column.FieldName = "Out-Standing" Then
            Dim xValue As Double = Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns(e.Column.FieldName)))
            If xValue > 0 Then
                e.Appearance.BackColor = Color.LightSalmon
                e.Appearance.BackColor2 = Color.LightSalmon
                'e.Appearance.ForeColor = Color.B
            End If
            If xValue < 0 Then
                e.Appearance.BackColor = Color.RosyBrown
                e.Appearance.BackColor2 = Color.RosyBrown
                'e.Appearance.ForeColor = Color.B
            End If
        End If
        If e.Column.FieldName = "Payment" Then
            Dim xValue As Double = Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns(e.Column.FieldName)))
            If xValue > 0 Then
                e.Appearance.BackColor = Color.LimeGreen
                e.Appearance.BackColor2 = Color.LimeGreen
            End If
        End If

    End Sub

    Private Sub GridView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView4.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation_LB("EDIT")
        End If
    End Sub
    Private Sub GridView4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = False
            Me.Txt_CashAmt.Focus()
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation_LB("VIEW")
        End If
    End Sub
    Private Sub GridView4_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView4.RowCellStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If e.Column.FieldName = "Out-Standing" Then
            Dim xValue As Double = Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns(e.Column.FieldName)))
            If xValue > 0 Then
                e.Appearance.BackColor = Color.LightSalmon
                e.Appearance.BackColor2 = Color.LightSalmon
                'e.Appearance.ForeColor = Color.B
            End If
            If xValue < 0 Then
                e.Appearance.BackColor = Color.RosyBrown
                e.Appearance.BackColor2 = Color.RosyBrown
                'e.Appearance.ForeColor = Color.B
            End If

        End If
        If e.Column.FieldName = "Payment" Then
            Dim xValue As Double = Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns(e.Column.FieldName)))
            If xValue > 0 Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.LightGreen
            End If
        End If
    End Sub

    ''' <summary>
    ''' Returns Message text if References are present against the item created being edited
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreatedItemReferenceChecks(Optional GridRowNo As Integer = Nothing, Optional checkAdvDepOnly As Boolean = False) As String
        If GridRowNo = Nothing Then GridRowNo = Me.GridView1.FocusedRowHandle
        Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(Me.GridView1.GetRowCellValue(GridRowNo, "Item_ID").ToString()) 'Gets Asset Profile
        Dim Sr As Integer = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.")
        Dim ReturnMessage As String = ""
        Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
        'If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
        Dim xTemp_AssetID As String = ""
        Dim isProperty As Boolean = False
        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
            Case "GOLD", "SILVER"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "OTHER ASSETS"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "LIVESTOCK"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "VEHICLES"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "WIP"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "OTHER DEPOSITS"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "ADVANCES"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
            Case "LAND & BUILDING"
                xTemp_AssetID = Me.GridView1.GetRowCellValue(GridRowNo, "CREATION_PROF_REC_ID").ToString
                isProperty = True
                'Case "OTHER LIABILITIES"
                '    xTemp_AssetID = Base._Voucher_DBOps.GetRaisedLiabilityRecID(Me.xMID.Text)
            Case "NOT APPLICABLE" 'Pick Property Reference for L&B
                Dim value As Object = Base._Voucher_DBOps.GetReferenceRecordID(Me.xMID.Text)
                If Not value Is Nothing Then
                    xTemp_AssetID = value.ToString()
                End If
        End Select
        If Not checkAdvDepOnly Then
            ReturnMessage = ItemReferenceCheck(xTemp_AssetProfile, xTemp_AssetID)
        ElseIf xTemp_AssetProfile = "OTHER DEPOSITS" Or xTemp_AssetProfile = "ADVANCES" Then
            ReturnMessage = ItemReferenceCheck(xTemp_AssetProfile, xTemp_AssetID)
        End If

        Return ReturnMessage
    End Function
    Public Function ItemReferenceCheck(xTemp_AssetProfile As String, xTemp_AssetID As String) As String
        Dim ReturnMessage As String = ""
        If xTemp_AssetID.Length > 0 And (xTemp_AssetProfile = "GOLD" Or xTemp_AssetProfile = "SILVER" Or xTemp_AssetProfile = "OTHER ASSETS" Or xTemp_AssetProfile = "LIVESTOCK" Or xTemp_AssetProfile = "VEHICLES" Or xTemp_AssetProfile = "WIP" Or xTemp_AssetProfile = "LAND & BUILDING" Or xTemp_AssetProfile = "NOT APPLICABLE") Then
            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID, False)
            If Not SaleRecord Is Nothing Then
                If SaleRecord.Rows.Count > 0 Then
                    'Throw New Exception("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.")
                    'Exit Function
                    ReturnMessage = "Sorry ! Selected Entry creates/refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." ''& vbNewLine & vbNewLine & " Please delete the record for editing this Entry."
                    Return ReturnMessage
                End If
            End If

            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment, Nothing, xTemp_AssetID, False) 'Bug #5339 fix
            If (Not AssetTrfRecord Is Nothing) AndAlso AssetTrfRecord.Rows.Count > 0 Then
                If AssetTrfRecord.Rows.Count > 0 Then
                    'Throw New Exception("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.")
                    'Exit Function
                    ReturnMessage = "Sorry ! Selected Entry creates/refers asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." ' & vbNewLine & vbNewLine & " Please delete the record for editing this Entry."
                    Return ReturnMessage
                End If
            End If

            If xTemp_AssetProfile <> "NOT APPLICABLE" Then 'Reference need not be checked in case of Construction entries
                Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                If Not ReferenceRecord Is Nothing Then
                    If ReferenceRecord.Rows.Count > 0 Then
                        'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for completing this Entry.", "Item created here has been used in some other entry....", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Sub
                        ReturnMessage = "Sorry ! Selected Entry creates/refers asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." '& vbNewLine & vbNewLine & " Please delete the record for completing this Entry."
                        Return ReturnMessage
                    End If
                End If
            End If
        End If

        If xTemp_AssetProfile = "OTHER DEPOSITS" Then
            ''Check If created deposit is used anywhere 
            If Not xTemp_AssetID Is Nothing Then
                Dim xTemp_DepID As String = xTemp_AssetID
                If xTemp_DepID.Length > 0 Then
                    'Adjustments/ Refund has been made againt the deposit raised
                    Dim txnReferDeposits As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID)
                    If Not txnReferDeposits Is Nothing Then
                        If txnReferDeposits.Rows.Count > 0 Then
                            'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " & Convert.ToDateTime(txnReferDeposits.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferDeposits.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Deposit created here has been used in some other entry....", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Sub
                            ReturnMessage = "Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " & Convert.ToDateTime(txnReferDeposits.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferDeposits.Rows(0)("TR_AMOUNT").ToString() & "." '& vbNewLine & vbNewLine & " Please delete the record for editing this Entry."
                        End If
                    End If

                    Dim paramJornalEntry As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    paramJornalEntry.CrossRefId = xTemp_DepID
                    paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    paramJornalEntry.Excluded_Rec_M_ID = Me.xMID.Text
                    paramJornalEntry.NextUnauditedYearID = Base._next_Unaudited_YearID
                    txnReferDeposits = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                    If Not txnReferDeposits Is Nothing Then
                        If txnReferDeposits.Rows.Count > 0 Then
                            'Throw New Exception("S o r r y!   D e p o s i t   c r e a t e d   b y   c u r r e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   j o u r n a l   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.")
                            'Exit Sub
                            ReturnMessage = "S o r r y!   D e p o s i t   c r e a t e d   b y   c u r r e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   j o u r n a l   e n t r y. . . !" '& vbNewLine & vbNewLine & "Please delete that dependency to edit this entry."
                        End If
                    End If
                End If
            End If
        End If

        If xTemp_AssetProfile = "ADVANCES" Then
            ''Check If created deposit is used anywhere 
            If Not xTemp_AssetID Is Nothing Then
                Dim xTemp_AdvID As String = xTemp_AssetID
                If xTemp_AdvID.Length > 0 Then
                    'Adjustments/ Refund has been made againt the deposit raised
                    Dim txnReferAdvances As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID)
                    If Not txnReferAdvances Is Nothing Then
                        If txnReferAdvances.Rows.Count > 0 Then
                            'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " & Convert.ToDateTime(txnReferAdvances.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferAdvances.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Advance created here has been used in some other entry....", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Sub
                            ReturnMessage = "Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " & Convert.ToDateTime(txnReferAdvances.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferAdvances.Rows(0)("TR_AMOUNT").ToString() & "." ' & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry."
                        End If
                    End If

                    Dim paramJornalEntry As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    paramJornalEntry.CrossRefId = xTemp_AdvID
                    paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    paramJornalEntry.Excluded_Rec_M_ID = Me.xMID.Text
                    paramJornalEntry.NextUnauditedYearID = Base._next_Unaudited_YearID
                    txnReferAdvances = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                    If Not txnReferAdvances Is Nothing Then
                        If txnReferAdvances.Rows.Count > 0 Then
                            'Throw New Exception("S o r r y!   A d v a n c e   c r e a t e d   b y   c u r r e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   j o u r n a l   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to delete this entry.")
                            'Exit Sub
                            ReturnMessage = "S o r r y!   A d v a n c e   c r e a t e d   b y   c u r r e n t   e n t r y   i s   u s e d   i n   s o m e   o t h e r   j o u r n a l   e n t r y. . . !" '& vbNewLine & vbNewLine & "Please delete that dependency to delete this entry."
                        End If
                    End If
                End If
            End If
        End If
        Return ReturnMessage
    End Function
    Public Sub DataNavigation(ByVal Action As String)
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Dim Delete_Action As Boolean = False
            Select Case Action
                Case "NEW"
                    Dim xfrm As New Frm_Voucher_Win_Gen_Pay_Item
                    xfrm.Text = "New ~ Item Detail" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm.iSpecific_Allow = True : xfrm.iSpecific_ItemID = iSpecific_ItemID
                    If IsDate(Me.Txt_V_Date.Text) Then xfrm.Vdt = Format(Me.Txt_V_Date.DateTime, Base._Date_Format_Current)
                    If Me.GLookUp_PartyList1.Tag.ToString.Length > 0 Then
                        xfrm.iPartyID = Me.GLookUp_PartyList1.Tag.ToString
                    End If
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then xfrm.iTxnM_ID = Me.xMID.Text
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        If xfrm.GLookUp_PartyListView.FocusedRowHandle > 0 Then
                            Dim PartyID As String = xfrm.GLookUp_PartyListView.GetRowCellValue(xfrm.GLookUp_PartyListView.FocusedRowHandle, "ID").ToString
                            Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                            Me.GLookUp_PartyList1View.FocusedRowHandle = Me.GLookUp_PartyList1View.LocateByValue("C_ID", PartyID)
                            If Me.GLookUp_PartyList1View.FocusedRowHandle < 0 Then
                                LookUp_GetPartyList()
                                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                                Me.GLookUp_PartyList1View.FocusedRowHandle = Me.GLookUp_PartyList1View.LocateByValue("C_ID", PartyID)
                            End If
                            Me.GLookUp_PartyList1.EditValue = PartyID
                            'Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
                            'Me.GLookUp_PartyList1.Properties.ReadOnly = False
                            Me.GLookUp_PartyList1.Tag = PartyID
                        End If
                        Me.GridView1.ClearSorting()
                        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
                        ROW = DT.NewRow
                        If GridView1.RowCount <= 0 Then
                            ROW("Sr.") = 1
                        Else
                            Me.GridView1.MoveLast()
                            ROW("Sr.") = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) + 1
                        End If
                        ROW("Item Name") = xfrm.GLookUp_ItemList.Text
                        ROW("Item_ID") = xfrm.GLookUp_ItemList.Tag
                        If xfrm.iCond_Ledger_ID <> "00000" Then
                            If Val(xfrm.Txt_Amt.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(xfrm.iMaxValue) Then
                                ROW("Item_Led_ID") = xfrm.iCond_Ledger_ID
                            Else
                                ROW("Item_Led_ID") = xfrm.iLed_ID
                            End If
                        Else
                            ROW("Item_Led_ID") = xfrm.iLed_ID
                        End If
                        ROW("ITEM_VOUCHER_TYPE") = xfrm.iVoucher_Type
                        ROW("Item_Trans_Type") = xfrm.iTrans_Type
                        ROW("Item_Profile") = xfrm.iProfile
                        ROW("Item_Party_Req") = xfrm.iParty_Req
                        ROW("Head") = xfrm.BE_Item_Head.Text
                        ROW("Qty.") = Val(xfrm.Txt_Qty.Text)
                        ROW("Unit") = xfrm.Cmb_Unit.Text
                        ROW("Rate") = Val(xfrm.Txt_Rate.Text)
                        ROW("Amount") = Convert.ToDecimal(xfrm.Txt_Amt.Text) - Convert.ToDecimal(IIf(xfrm.TXT_TDS.Text.Length > 0, xfrm.TXT_TDS.Text, 0))
                        ROW("TDS") = Val(xfrm.TXT_TDS.Text)
                        ROW("Remarks") = xfrm.Txt_Remarks.Text
                        ROW("PUR_ID") = xfrm.GLookUp_PurList.Tag
                        ROW("LB_REC_ID") = xfrm.LB_REC_ID
                        ROW("LB_REC_EDIT_ON") = xfrm.LB_REC_EDIT_ON

                        If xfrm.iProfile = "GOLD" Or xfrm.iProfile = "SILVER" Then
                            ROW("GS_DESC_MISC_ID") = xfrm.GS_DESC_MISC_ID
                            ROW("GS_ITEM_WEIGHT") = xfrm.GS_ITEM_WEIGHT
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "OTHER ASSETS" Then
                            ROW("AI_TYPE") = xfrm.AI_TYPE
                            ROW("AI_MAKE") = xfrm.AI_MAKE
                            ROW("AI_MODEL") = xfrm.AI_MODEL
                            ROW("AI_SERIAL_NO") = xfrm.AI_SERIAL_NO
                            ROW("AI_PUR_DATE") = xfrm.AI_PUR_DATE
                            ROW("AI_WARRANTY") = xfrm.AI_WARRANTY
                            If Not IsDBNull(xfrm.AI_IMAGE) Then ROW("AI_IMAGE") = xfrm.AI_IMAGE
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "LIVESTOCK" Then
                            ROW("LS_NAME") = xfrm.LS_NAME
                            ROW("LS_BIRTH_YEAR") = xfrm.LS_BIRTH_YEAR
                            ROW("LS_INSURANCE") = xfrm.LS_INSURANCE
                            ROW("LS_INSURANCE_ID") = xfrm.LS_INSURANCE_ID
                            ROW("LS_INS_POLICY_NO") = xfrm.LS_INS_POLICY_NO
                            ROW("LS_INS_AMT") = xfrm.LS_INS_AMT
                            ROW("LS_INS_DATE") = xfrm.LS_INS_DATE
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "VEHICLES" Then
                            ROW("VI_MAKE") = xfrm.VI_MAKE
                            ROW("VI_MODEL") = xfrm.VI_MODEL
                            ROW("VI_REG_NO_PATTERN") = xfrm.VI_REG_NO_PATTERN
                            ROW("VI_REG_NO") = xfrm.VI_REG_NO
                            ROW("VI_REG_DATE") = xfrm.VI_REG_DATE
                            ROW("VI_OWNERSHIP") = xfrm.VI_OWNERSHIP
                            ROW("VI_OWNERSHIP_AB_ID") = xfrm.VI_OWNERSHIP_AB_ID
                            ROW("VI_DOC_RC_BOOK") = xfrm.VI_DOC_RC_BOOK
                            ROW("VI_DOC_AFFIDAVIT") = xfrm.VI_DOC_AFFIDAVIT
                            ROW("VI_DOC_WILL") = xfrm.VI_DOC_WILL
                            ROW("VI_DOC_TRF_LETTER") = xfrm.VI_DOC_TRF_LETTER
                            ROW("VI_DOC_FU_LETTER") = xfrm.VI_DOC_FU_LETTER
                            ROW("VI_DOC_OTHERS") = xfrm.VI_DOC_OTHERS
                            ROW("VI_DOC_NAME") = xfrm.VI_DOC_NAME
                            ROW("VI_INSURANCE_ID") = xfrm.VI_INSURANCE_ID
                            ROW("VI_INS_POLICY_NO") = xfrm.VI_INS_POLICY_NO
                            ROW("VI_INS_EXPIRY_DATE") = xfrm.VI_INS_EXPIRY_DATE
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "LAND & BUILDING" Then
                            ROW("LB_PRO_TYPE") = xfrm.LB_PRO_TYPE
                            ROW("LB_PRO_CATEGORY") = xfrm.LB_PRO_CATEGORY
                            ROW("LB_PRO_USE") = xfrm.LB_PRO_USE
                            ROW("LB_PRO_NAME") = xfrm.LB_PRO_NAME
                            ROW("LB_PRO_ADDRESS") = xfrm.LB_PRO_ADDRESS
                            ROW("LB_ADDRESS1") = xfrm.LB_ADDRESS1
                            ROW("LB_ADDRESS2") = xfrm.LB_ADDRESS2
                            ROW("LB_ADDRESS3") = xfrm.LB_ADDRESS3
                            ROW("LB_ADDRESS4") = xfrm.LB_ADDRESS4
                            ROW("LB_CITY_ID") = xfrm.LB_CITY_ID
                            ROW("LB_DISTRICT_ID") = xfrm.LB_DISTRICT_ID
                            ROW("LB_STATE_ID") = xfrm.LB_STATE_ID
                            ROW("LB_PINCODE") = xfrm.LB_PINCODE
                            ROW("LB_OWNERSHIP") = xfrm.LB_OWNERSHIP
                            ROW("LB_OWNERSHIP_PARTY_ID") = xfrm.LB_OWNERSHIP_PARTY_ID
                            ROW("LB_SURVEY_NO") = xfrm.LB_SURVEY_NO
                            ROW("LB_CON_YEAR") = xfrm.LB_CON_YEAR
                            ROW("LB_RCC_ROOF") = xfrm.LB_RCC_ROOF
                            ROW("LB_PAID_DATE") = xfrm.LB_PAID_DATE
                            ROW("LB_PERIOD_FROM") = xfrm.LB_PERIOD_FROM
                            ROW("LB_PERIOD_TO") = xfrm.LB_PERIOD_TO
                            ROW("LB_DOC_OTHERS") = xfrm.LB_DOC_OTHERS
                            ROW("LB_DOC_NAME") = xfrm.LB_DOC_NAME
                            ROW("LB_OTHER_DETAIL") = xfrm.LB_OTHER_DETAIL
                            ROW("LB_TOT_P_AREA") = xfrm.LB_TOT_P_AREA
                            ROW("LB_CON_AREA") = xfrm.LB_CON_AREA
                            ROW("LB_DEPOSIT_AMT") = xfrm.LB_DEPOSIT_AMT
                            ROW("LB_MONTH_RENT") = xfrm.LB_MONTH_RENT
                            ROW("LB_MONTH_O_PAYMENTS") = xfrm.LB_MONTH_O_PAYMENTS

                            If LB_DOCS_ARRAY Is Nothing Then
                                LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY
                            Else
                                If LB_DOCS_ARRAY.Rows.Count <= 0 Then
                                    LB_DOCS_ARRAY = New DataTable
                                    With LB_DOCS_ARRAY
                                        .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                        .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                    End With
                                End If
                                For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                    Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                    Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                    Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                    LB_DOCS_ARRAY.Rows.Add(Row)
                                Next
                            End If
                            If LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE
                            Else
                                If LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0 Then
                                    LB_EXTENDED_PROPERTY_TABLE = New DataTable
                                    With LB_EXTENDED_PROPERTY_TABLE
                                        .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                        .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                        .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                        .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                        .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                        .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                        .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                        .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                        .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                    End With
                                End If
                                For Each XROW As DataRow In xfrm.LB_EXTENDED_PROPERTY_TABLE.Rows
                                    Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                    Row("LB_MOU_DATE") = XROW("LB_MOU_DATE").ToString
                                    Row("LB_SR_NO") = XROW("LB_SR_NO").ToString
                                    Row("LB_INS_ID") = XROW("LB_INS_ID").ToString
                                    Row("LB_TOT_P_AREA") = Val(XROW("LB_TOT_P_AREA").ToString())
                                    Row("LB_CON_AREA") = Val(XROW("LB_CON_AREA").ToString())
                                    Row("LB_CON_YEAR") = XROW("LB_CON_YEAR").ToString()
                                    Row("LB_VALUE") = Val(XROW("LB_VALUE"))
                                    Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL").ToString()
                                    Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                    LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                Next
                            End If
                        End If
                        If xfrm.iProfile = "TELEPHONE BILL" Then
                            ROW("TP_ID") = xfrm.X_TP_ID
                            ROW("TP_BILL_NO") = xfrm.TP_BILL_NO
                            ROW("TP_BILL_DATE") = xfrm.TP_BILL_DATE
                            ROW("TP_PERIOD_FROM") = xfrm.TP_PERIOD_FROM
                            ROW("TP_PERIOD_TO") = xfrm.TP_PERIOD_TO
                        End If
                        If xfrm.iProfile.Equals("WIP") Then
                            If xfrm.iRefType = "NEW" Then
                                ROW("REFERENCE") = xfrm.iReference
                                ROW("WIP_REF_TYPE") = xfrm.iRefType
                            Else
                                ROW("REF_REC_ID") = xfrm.Ref_RecID
                                ROW("WIP_REF_TYPE") = xfrm.iRefType
                            End If
                        End If

                        If Not xfrm.TDS_Deduction_List Is Nothing Then
                            If xfrm.TDS_Deduction_List.Count > 0 Then
                                For Each cParam As Frm_Voucher_Win_Gen_Pay_Tds_Ded.Out_TDS In xfrm.TDS_Deduction_List
                                    If ROW("REF_TDS_DED").ToString.Length > 0 Then
                                        ROW("REF_TDS_DED") = ROW("REF_TDS_DED") + ";" + cParam.RefMID + ":" + cParam.TDS_Ded.ToString
                                    Else
                                        ROW("REF_TDS_DED") = cParam.RefMID + ":" + cParam.TDS_Ded.ToString
                                    End If
                                Next
                            End If
                        End If

                        DT.Rows.Add(ROW)
                        'xID = xfrm.xID.Text

                        If Not xfrm Is Nothing Then xfrm.Dispose()
                        If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
                    Else
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If
                Case "EDIT", "VIEW"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        Dim xfrm As New Frm_Voucher_Win_Gen_Pay_Item
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If
                        Dim AllowAmountEdit As Boolean = True
                        ''#####################################################################################
                        If Action = "EDIT" Then
                            AllowAmountEdit = IIf(CreatedItemReferenceChecks().Length > 0, False, True)
                        End If
                        ''##################################################################################################################################

                        xfrm.iSpecific_ItemID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID").ToString()
                        xfrm.iProfile_OLD = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString()

                        xfrm.iPur_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID").ToString()
                        xfrm.Txt_Qty.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.").ToString()
                        xfrm.Txt_Rate.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Rate").ToString()
                        xfrm.Txt_Amt.Text = Convert.ToDecimal(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Amount").ToString()) + IIf(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS").ToString().Length > 0, Convert.ToDecimal(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS").ToString()), 0)
                        If Not AllowAmountEdit Then
                            xfrm.Txt_Amt.Properties.ReadOnly = True
                            xfrm.Txt_Amt.ToolTip = "Amount has been disabled , as there are references posted against created item"
                            xfrm.Txt_Qty.Properties.ReadOnly = True
                            xfrm.Txt_Qty.ToolTip = "Amount has been disabled , as there are references posted against created item"
                        End If
                        xfrm.TXT_TDS.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS").ToString()
                        xfrm.Txt_Remarks.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks").ToString()
                        xfrm.LB_REC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID").ToString()
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_EDIT_ON").ToString().Length > 0 Then
                            xfrm.LB_REC_EDIT_ON = Convert.ToDateTime(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_EDIT_ON").ToString())
                        End If
                        xfrm.iTxnM_ID = Me.xMID.Text
                        If Me.GLookUp_PartyList1.Tag.ToString.Length > 0 Then
                            xfrm.iPartyID = Me.GLookUp_PartyList1.Tag.ToString
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "GOLD" Or _
                            Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "SILVER" Then
                            xfrm.GS_DESC_MISC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_DESC_MISC_ID").ToString()
                            xfrm.GS_ITEM_WEIGHT = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_ITEM_WEIGHT").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "OTHER ASSETS" Then
                            xfrm.AI_TYPE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_TYPE").ToString()
                            xfrm.AI_MAKE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MAKE").ToString()
                            xfrm.AI_MODEL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MODEL").ToString()
                            xfrm.AI_SERIAL_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_SERIAL_NO").ToString()
                            xfrm.AI_PUR_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_PUR_DATE").ToString()
                            xfrm.AI_WARRANTY = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_WARRANTY").ToString())
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE")) Then xfrm.AI_IMAGE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE")
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LIVESTOCK" Then
                            xfrm.LS_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_NAME").ToString()
                            xfrm.LS_BIRTH_YEAR = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_BIRTH_YEAR").ToString()
                            xfrm.LS_INSURANCE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE").ToString()
                            xfrm.LS_INSURANCE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE_ID").ToString()
                            xfrm.LS_INS_POLICY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_POLICY_NO").ToString()
                            xfrm.LS_INS_AMT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_AMT").ToString())
                            xfrm.LS_INS_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_DATE").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "VEHICLES" Then
                            xfrm.VI_MAKE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MAKE").ToString()
                            xfrm.VI_MODEL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MODEL").ToString()
                            xfrm.VI_REG_NO_PATTERN = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO_PATTERN").ToString()
                            xfrm.VI_REG_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO").ToString()
                            xfrm.VI_REG_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_DATE").ToString()
                            xfrm.VI_OWNERSHIP = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP").ToString()
                            xfrm.VI_OWNERSHIP_AB_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP_AB_ID").ToString()
                            xfrm.VI_DOC_RC_BOOK = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_RC_BOOK").ToString()
                            xfrm.VI_DOC_AFFIDAVIT = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_AFFIDAVIT").ToString()
                            xfrm.VI_DOC_WILL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_WILL").ToString()
                            xfrm.VI_DOC_TRF_LETTER = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_TRF_LETTER").ToString()
                            xfrm.VI_DOC_FU_LETTER = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_FU_LETTER").ToString()
                            xfrm.VI_DOC_OTHERS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_OTHERS").ToString()
                            xfrm.VI_DOC_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_NAME").ToString()
                            xfrm.VI_INSURANCE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INSURANCE_ID").ToString()
                            xfrm.VI_INS_POLICY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_POLICY_NO").ToString()
                            xfrm.VI_INS_EXPIRY_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_EXPIRY_DATE").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LAND & BUILDING" Then
                            xfrm.LB_PRO_TYPE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_TYPE").ToString()
                            xfrm.LB_PRO_CATEGORY = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_CATEGORY").ToString()
                            xfrm.LB_PRO_USE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_USE").ToString()
                            xfrm.LB_PRO_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_NAME").ToString()
                            xfrm.LB_PRO_ADDRESS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_ADDRESS").ToString()
                            xfrm.LB_ADDRESS1 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS1").ToString()
                            xfrm.LB_ADDRESS2 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS2").ToString()
                            xfrm.LB_ADDRESS3 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS3").ToString()
                            xfrm.LB_ADDRESS4 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS4").ToString()
                            xfrm.LB_CITY_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CITY_ID").ToString()
                            xfrm.LB_DISTRICT_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DISTRICT_ID").ToString()
                            xfrm.LB_STATE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_STATE_ID").ToString()
                            xfrm.LB_PINCODE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PINCODE").ToString()
                            xfrm.LB_OWNERSHIP = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP").ToString()
                            xfrm.LB_OWNERSHIP_PARTY_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP_PARTY_ID").ToString()
                            xfrm.LB_SURVEY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_SURVEY_NO").ToString()
                            xfrm.LB_CON_YEAR = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_YEAR").ToString()
                            xfrm.LB_RCC_ROOF = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_RCC_ROOF").ToString()
                            xfrm.LB_PAID_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PAID_DATE").ToString()
                            xfrm.LB_PERIOD_FROM = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_FROM").ToString()
                            xfrm.LB_PERIOD_TO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_TO").ToString()
                            xfrm.LB_DOC_OTHERS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_OTHERS").ToString()
                            xfrm.LB_DOC_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_NAME").ToString()
                            xfrm.LB_OTHER_DETAIL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OTHER_DETAIL").ToString()
                            xfrm.LB_TOT_P_AREA = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_TOT_P_AREA").ToString())
                            xfrm.LB_CON_AREA = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_AREA").ToString())
                            xfrm.LB_DEPOSIT_AMT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DEPOSIT_AMT").ToString())
                            xfrm.LB_MONTH_RENT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_RENT").ToString())
                            xfrm.LB_MONTH_O_PAYMENTS = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_O_PAYMENTS").ToString())


                            Dim EDIT_LB_EXTENDED_PROPERTY_TABLE As DataTable = New DataTable
                            With EDIT_LB_EXTENDED_PROPERTY_TABLE
                                .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                            End With
                            If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then 'LB Item screen already opened in same instance 
                                For Each XROW As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                    If XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                        Dim Row As DataRow = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                        Row("LB_MOU_DATE") = XROW("LB_MOU_DATE")
                                        Row("LB_SR_NO") = XROW("LB_SR_NO")
                                        Row("LB_INS_ID") = XROW("LB_INS_ID")
                                        Row("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
                                        Row("LB_CON_AREA") = XROW("LB_CON_AREA")
                                        Row("LB_CON_YEAR") = XROW("LB_CON_YEAR")
                                        Row("LB_VALUE") = XROW("LB_VALUE")
                                        Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
                                        Row("LB_REC_ID") = xfrm.LB_REC_ID
                                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                    End If
                                Next
                            Else
                                Dim LB_Ext As DataTable = Base._L_B_DBOps.GetExtensionDetails(xfrm.LB_REC_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
                                For Each XROW As DataRow In LB_Ext.Rows
                                    Dim Row As DataRow = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                    Row("LB_MOU_DATE") = XROW("LB_MOU_DATE")
                                    Row("LB_SR_NO") = XROW("LB_SR_NO")
                                    Row("LB_INS_ID") = XROW("LB_INS_ID")
                                    Row("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
                                    Row("LB_CON_AREA") = XROW("LB_CON_AREA")
                                    Row("LB_CON_YEAR") = XROW("LB_CON_YEAR")
                                    Row("LB_VALUE") = XROW("LB_VALUE")
                                    Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
                                    Row("LB_REC_ID") = xfrm.LB_REC_ID
                                    EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                Next
                            End If
                            xfrm.LB_EXTENDED_PROPERTY_TABLE = EDIT_LB_EXTENDED_PROPERTY_TABLE

                            Dim EDIT_LB_DOCS_ARRAY As DataTable = New DataTable
                            With EDIT_LB_DOCS_ARRAY
                                .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                            End With
                            If Not LB_DOCS_ARRAY Is Nothing Then
                                For Each XROW As DataRow In LB_DOCS_ARRAY.Rows
                                    If XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                        Dim Row As DataRow = EDIT_LB_DOCS_ARRAY.NewRow()
                                        Row("LB_MISC_ID") = XROW("LB_MISC_ID")
                                        Row("LB_REC_ID") = xfrm.LB_REC_ID
                                        EDIT_LB_DOCS_ARRAY.Rows.Add(Row)
                                    End If
                                Next
                            Else
                                Dim LB_DOC As DataTable = Base._L_B_DBOps.GetDocsDetails(xfrm.LB_REC_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
                                For Each XROW As DataRow In LB_DOC.Rows
                                    Dim Row As DataRow = EDIT_LB_DOCS_ARRAY.NewRow()
                                    Row("LB_MISC_ID") = XROW("LB_MISC_ID")
                                    Row("LB_REC_ID") = xfrm.LB_REC_ID
                                    EDIT_LB_DOCS_ARRAY.Rows.Add(Row)
                                Next
                            End If
                            xfrm.LB_DOCS_ARRAY = EDIT_LB_DOCS_ARRAY

                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "TELEPHONE BILL" Then
                            xfrm.X_TP_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_ID").ToString()
                            xfrm.TP_BILL_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_BILL_NO").ToString()
                            xfrm.TP_BILL_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_BILL_DATE").ToString()
                            xfrm.TP_PERIOD_FROM = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_PERIOD_FROM").ToString()
                            xfrm.TP_PERIOD_TO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_PERIOD_TO").ToString()
                        End If

                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "WIP" Then
                            xfrm.iReference = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REFERENCE").ToString()
                            xfrm.Ref_RecID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID").ToString()
                            xfrm.iRefType = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE").ToString()
                        End If


                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK And xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item Name", xfrm.GLookUp_ItemList.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID", xfrm.GLookUp_ItemList.Tag)
                            If xfrm.iCond_Ledger_ID <> "00000" Then
                                If Val(xfrm.Txt_Amt.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(xfrm.iMaxValue) Then
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iCond_Ledger_ID)
                                Else
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                                End If
                            Else
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                            End If
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Trans_Type", xfrm.iTrans_Type)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile", xfrm.iProfile)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ITEM_VOUCHER_TYPE", xfrm.iVoucher_Type)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Party_Req", xfrm.iParty_Req)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Head", xfrm.BE_Item_Head.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.", Val(xfrm.Txt_Qty.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Unit", xfrm.Cmb_Unit.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rate", Val(xfrm.Txt_Rate.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Amount", Convert.ToDecimal(xfrm.Txt_Amt.Text) - IIf(xfrm.TXT_TDS.Text.Length > 0, Convert.ToDecimal(xfrm.TXT_TDS.Text), 0))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TDS", Val(xfrm.TXT_TDS.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks", xfrm.Txt_Remarks.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID", xfrm.GLookUp_PurList.Tag)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID", xfrm.LB_REC_ID)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_EDIT_ON", xfrm.LB_REC_EDIT_ON)
                            If xfrm.GLookUp_PartyListView.FocusedRowHandle > 0 Then
                                Dim PartyID As String = xfrm.GLookUp_PartyListView.GetRowCellValue(xfrm.GLookUp_PartyListView.FocusedRowHandle, "ID").ToString
                                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                                Me.GLookUp_PartyList1View.FocusedRowHandle = Me.GLookUp_PartyList1View.LocateByValue("C_ID", PartyID)
                                Me.GLookUp_PartyList1.EditValue = PartyID
                                'Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
                                'Me.GLookUp_PartyList1.Properties.ReadOnly = False
                                Me.GLookUp_PartyList1.Tag = PartyID
                            End If

                            If xfrm.iProfile = "GOLD" Or xfrm.iProfile = "SILVER" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_DESC_MISC_ID", xfrm.GS_DESC_MISC_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_ITEM_WEIGHT", xfrm.GS_ITEM_WEIGHT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "OTHER ASSETS" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_TYPE", xfrm.AI_TYPE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MAKE", xfrm.AI_MAKE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MODEL", xfrm.AI_MODEL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_SERIAL_NO", xfrm.AI_SERIAL_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_PUR_DATE", xfrm.AI_PUR_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_WARRANTY", xfrm.AI_WARRANTY)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE", xfrm.AI_IMAGE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "LIVESTOCK" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_NAME", xfrm.LS_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_BIRTH_YEAR", xfrm.LS_BIRTH_YEAR)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE", xfrm.LS_INSURANCE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE_ID", xfrm.LS_INSURANCE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_POLICY_NO", xfrm.LS_INS_POLICY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_AMT", Val(xfrm.LS_INS_AMT))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_DATE", xfrm.LS_INS_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID) '#4864 fix
                            End If
                            If xfrm.iProfile = "VEHICLES" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MAKE", xfrm.VI_MAKE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MODEL", xfrm.VI_MODEL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO_PATTERN", xfrm.VI_REG_NO_PATTERN)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO", xfrm.VI_REG_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_DATE", xfrm.VI_REG_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP", xfrm.VI_OWNERSHIP)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP_AB_ID", xfrm.VI_OWNERSHIP_AB_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_RC_BOOK", xfrm.VI_DOC_RC_BOOK)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_AFFIDAVIT", xfrm.VI_DOC_AFFIDAVIT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_WILL", xfrm.VI_DOC_WILL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_TRF_LETTER", xfrm.VI_DOC_TRF_LETTER)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_FU_LETTER", xfrm.VI_DOC_FU_LETTER)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_OTHERS", xfrm.VI_DOC_OTHERS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_NAME", xfrm.VI_DOC_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INSURANCE_ID", xfrm.VI_INSURANCE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_POLICY_NO", xfrm.VI_INS_POLICY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_EXPIRY_DATE", xfrm.VI_INS_EXPIRY_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "LAND & BUILDING" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_TYPE", xfrm.LB_PRO_TYPE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_CATEGORY", xfrm.LB_PRO_CATEGORY)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_USE", xfrm.LB_PRO_USE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_NAME", xfrm.LB_PRO_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_ADDRESS", xfrm.LB_PRO_ADDRESS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS1", xfrm.LB_ADDRESS1)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS2", xfrm.LB_ADDRESS2)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS3", xfrm.LB_ADDRESS3)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS4", xfrm.LB_ADDRESS4)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CITY_ID", xfrm.LB_CITY_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DISTRICT_ID", xfrm.LB_DISTRICT_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_STATE_ID", xfrm.LB_STATE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PINCODE", xfrm.LB_PINCODE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP", xfrm.LB_OWNERSHIP)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP_PARTY_ID", xfrm.LB_OWNERSHIP_PARTY_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_SURVEY_NO", xfrm.LB_SURVEY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_YEAR", xfrm.LB_CON_YEAR)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_RCC_ROOF", xfrm.LB_RCC_ROOF)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PAID_DATE", xfrm.LB_PAID_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_FROM", xfrm.LB_PERIOD_FROM)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_TO", xfrm.LB_PERIOD_TO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_OTHERS", xfrm.LB_DOC_OTHERS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_NAME", xfrm.LB_DOC_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OTHER_DETAIL", xfrm.LB_OTHER_DETAIL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_TOT_P_AREA", xfrm.LB_TOT_P_AREA)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_AREA", xfrm.LB_CON_AREA)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DEPOSIT_AMT", xfrm.LB_DEPOSIT_AMT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_RENT", xfrm.LB_MONTH_RENT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_O_PAYMENTS", xfrm.LB_MONTH_O_PAYMENTS)


                                If LB_DOCS_ARRAY Is Nothing Then
                                    LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY
                                Else
                                    If LB_DOCS_ARRAY.Rows.Count <= 0 Then
                                        LB_DOCS_ARRAY = New DataTable
                                        With LB_DOCS_ARRAY
                                            .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                        End With
                                    End If
                                    'delete any previously added docs for same l&b
                                    Dim New_LB_DOCS_ARRAY As DataTable = LB_DOCS_ARRAY.Clone()
                                    For Each XROW As DataRow In LB_DOCS_ARRAY.Rows
                                        If Not XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                            New_LB_DOCS_ARRAY.ImportRow(XROW)
                                        End If
                                    Next
                                    LB_DOCS_ARRAY = New_LB_DOCS_ARRAY
                                    For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                        Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                        Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                        Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                        LB_DOCS_ARRAY.Rows.Add(Row) 'add docs checked in current selection
                                    Next
                                End If

                                If LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                    LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE
                                Else
                                    If LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0 Then
                                        LB_EXTENDED_PROPERTY_TABLE = New DataTable
                                        With LB_EXTENDED_PROPERTY_TABLE
                                            .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                            .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                            .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                            .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                        End With
                                    End If
                                    'delete any previously added extensions for same l&b
                                    Dim New_LB_EXTENDED_PROPERTY_TABLE As DataTable = LB_EXTENDED_PROPERTY_TABLE.Clone()
                                    For Each XROW As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                        If Not XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                            New_LB_EXTENDED_PROPERTY_TABLE.ImportRow(XROW)
                                        End If
                                    Next
                                    LB_EXTENDED_PROPERTY_TABLE = New_LB_EXTENDED_PROPERTY_TABLE
                                    New_LB_EXTENDED_PROPERTY_TABLE.Dispose()
                                    For Each XROW As DataRow In xfrm.LB_EXTENDED_PROPERTY_TABLE.Rows
                                        Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                        Row("LB_MOU_DATE") = XROW("LB_MOU_DATE").ToString
                                        Row("LB_SR_NO") = XROW("LB_SR_NO").ToString
                                        Row("LB_INS_ID") = XROW("LB_INS_ID").ToString
                                        Row("LB_TOT_P_AREA") = Val(XROW("LB_TOT_P_AREA").ToString())
                                        Row("LB_CON_AREA") = Val(XROW("LB_CON_AREA").ToString())
                                        Row("LB_CON_YEAR") = XROW("LB_CON_YEAR").ToString()
                                        Row("LB_VALUE") = Val(XROW("LB_VALUE"))
                                        Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL").ToString()
                                        Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                        LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                    Next
                                End If
                            End If
                            If xfrm.iProfile = "TELEPHONE BILL" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_ID", xfrm.X_TP_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_BILL_NO", xfrm.TP_BILL_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_BILL_DATE", xfrm.TP_BILL_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_PERIOD_FROM", xfrm.TP_PERIOD_FROM)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TP_PERIOD_TO", xfrm.TP_PERIOD_TO)
                            End If
                            If xfrm.iProfile.Equals("WIP") Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID", xfrm.Ref_RecID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "REFERENCE", xfrm.iReference)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE", xfrm.iRefType)
                            End If
                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                        End If
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If
                Case "DELETE"
                        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "OTHER ASSETS" Then
                                If Base.IsInsuranceAudited() Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s   C a n n o t   b e   D e l e t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End If

                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString().Trim.ToUpper = "LAND & BUILDING" And Not Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
                                If Base.IsInsuranceAudited() Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y   R e l a t e d   E x p e n s e s   C a n n o t   b e   D e l e t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End If
                            ''#####################################################################################
                            Dim RefMessage As String = CreatedItemReferenceChecks()
                            If RefMessage.Length > 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(RefMessage, "Item Can not be deleted...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                            ''##################################################################################################################################

                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "WIP" And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE").ToString() = "EXISTING" Then
                                Dim RefId As Object = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID").ToString()
                                If Not RefId Is Nothing Then
                                    If RefId.Length > 0 Then
                                        Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                                        Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID").ToString(), Nothing, Me.xMID.Text, Common_Lib.Common.Navigation_Mode._Delete, RefId)
                                        If Base._next_Unaudited_YearID <> Nothing Then
                                            If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " & ROW("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If

                                        If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for " & ROW("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                        End If

                            DT.Rows(Me.GridView1.FocusedRowHandle).Delete()
                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                            Delete_Action = True
                        End If
            End Select

            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                GLookUp_PartyList1_EditValueChanged(New Object, New System.EventArgs)
                Sub_Amt_Calculation(Delete_Action)
            End If

            If Base._IsVolumeCenter And Action = "NEW" Then
                If GridView1.RowCount > 0 Then
                    If SelectedPaymentType = PaymentType.Bank Then
                        DataNavigation_Bank("NEW", Val(Me.GridView1.GetRowCellValue(Me.GridView1.RowCount - 1, "Amount").ToString()))
                    End If
                    If Me.GridView1.GetRowCellValue(Me.GridView1.RowCount - 1, "Item_Led_ID").ToString().Equals("00083") Then 'Creditors
                        Me.Tab_Page_Liabilities.Show()
                        If GridView4.RowCount = 1 Then
                            GridView4.FocusedRowHandle = 0 : GridView4.FocusedColumn = GridView4.Columns("Out-Standing")
                            DataNavigation_LB("EDIT", Val(Txt_BankAmt.Text) - Val(Txt_LB_Amt.Text))
                        ElseIf GridView4.RowCount > 1 Then
                            For ctr As Integer = 0 To GridView4.RowCount - 1
                                If Val(Me.GridView4.GetRowCellValue(ctr, "Out-Standing").ToString()) > 0 And Val(Txt_BankAmt.Text) - Val(Txt_LB_Amt.Text) > 0 Then
                                    GridView4.FocusedRowHandle = ctr : GridView4.FocusedColumn = GridView4.Columns("Out-Standing")
                                    DataNavigation_LB("EDIT", Val(Txt_BankAmt.Text) - Val(Txt_LB_Amt.Text))
                                Else
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If
        If Base._IsVolumeCenter Then
            Me.Txt_V_Date.Focus()
        Else
            Me.GridView1.Focus()
        End If
    End Sub
    Public Sub DataNavigation_Bank(ByVal Action As String, Optional Amount As Double = 0.0)
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Dim Delete_Action As Boolean = False
            Select Case Action
                Case "NEW"
                    Dim xfrm As New Frm_Voucher_Win_Gen_Pay_Bank
                    xfrm.Text = "New ~ Bank Payment" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    If Base._IsVolumeCenter Then
                        xfrm.Txt_Amount.Text = Txt_CashAmt.Text : xfrm.Cmd_Mode.Text = "CHEQUE" : xfrm.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : xfrm.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]"
                        xfrm.iBank_ID = Sel_Bank_ID
                    End If
                    If Amount > 0 Then xfrm.Txt_Amount.Text = CStr(Amount)
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        Me.GridView2.ClearSorting()
                        Me.GridView2.SortInfo.Add(Me.GridView2.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
                        ROW = Bank_Detail.NewRow
                        If GridView2.RowCount <= 0 Then
                            ROW("Sr.") = 1
                        Else
                            Me.GridView2.MoveLast()
                            ROW("Sr.") = Val(Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Sr.").ToString()) + 1
                        End If
                        ROW("Amount") = Val(xfrm.Txt_Amount.Text)
                        ROW("Mode") = xfrm.Cmd_Mode.Text
                        ROW("No.") = xfrm.Txt_Ref_No.Text
                        ROW("Date") = xfrm.Txt_Ref_Date.Text
                        ROW("Clearing Date") = xfrm.Txt_Ref_CDate.Text
                        ROW("Bank Name") = xfrm.GLookUp_BankList.Text
                        ROW("Branch") = xfrm.BE_Bank_Branch.Text
                        ROW("A/c. No.") = xfrm.BE_Bank_Acc_No.Text
                        ROW("ID") = xfrm.GLookUp_BankList.Tag

                        ROW("MT_BANK_ID") = xfrm.GLookUp_RefBankList.Tag
                        ROW("Money Transfer Bank") = xfrm.GLookUp_RefBankList.Text
                        ROW("Ref. A/c. No.") = xfrm.Txt_Trf_ANo.Text
                        ROW("Edit Time") = xfrm.GLookUp_BankListView.GetRowCellValue(xfrm.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON").ToString
                        Sel_Bank_ID = xfrm.GLookUp_BankList.Tag
                        Bank_Detail.Rows.Add(ROW)
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    Else
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If

                Case "EDIT", "VIEW"
                        If Me.GridView2.RowCount > 0 And Val(Me.GridView2.FocusedRowHandle) >= 0 Then
                            Dim xfrm As New Frm_Voucher_Win_Gen_Pay_Bank
                            If Action = "VIEW" Then
                                xfrm.Text = "View ~ Bank Payment"
                                xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                            Else
                                xfrm.Text = "Edit ~ Bank Payment"
                                xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                            End If

                            xfrm.iBank_ID = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "ID").ToString()
                            xfrm.iMT_BANK_ID = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "MT_BANK_ID").ToString()
                            xfrm.Cmd_Mode.Text = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Mode").ToString()
                            xfrm.Txt_Ref_No.Text = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "No.").ToString()
                            xfrm.Txt_Trf_ANo.Text = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Ref. A/c. No.").ToString()
                            Dim xDate As DateTime = Nothing
                            If IsDate(Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Date")) Then
                                xDate = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Date").ToString() : xfrm.Txt_Ref_Date.DateTime = xDate
                            End If
                            If IsDate(Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Clearing Date")) Then
                                xDate = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Clearing Date").ToString() : xfrm.Txt_Ref_CDate.DateTime = xDate
                            End If
                            xfrm.Txt_Amount.Text = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Amount").ToString()
                            xfrm.ShowDialog(Me)
                            If xfrm.DialogResult = DialogResult.OK Then
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Amount", Val(xfrm.Txt_Amount.Text))
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Mode", xfrm.Cmd_Mode.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "No.", xfrm.Txt_Ref_No.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Date", xfrm.Txt_Ref_Date.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Clearing Date", xfrm.Txt_Ref_CDate.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Bank Name", xfrm.GLookUp_BankList.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Branch", xfrm.BE_Bank_Branch.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "A/c. No.", xfrm.BE_Bank_Acc_No.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "ID", xfrm.GLookUp_BankList.Tag)

                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "MT_BANK_ID", xfrm.GLookUp_RefBankList.Tag)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Money Transfer Bank", xfrm.GLookUp_RefBankList.Text)
                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Ref. A/c. No.", xfrm.Txt_Trf_ANo.Text)

                                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Edit Time", xfrm.GLookUp_BankListView.GetRowCellValue(xfrm.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON").ToString)
                                Me.GridView2.RefreshData() : Me.GridView2.UpdateCurrentRow()
                                Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
                                Sel_Bank_ID = xfrm.GLookUp_BankList.Tag
                            End If
                            If Not xfrm Is Nothing Then xfrm.Dispose()
                        End If
                Case "DELETE"
                        If Me.GridView2.RowCount > 0 And Val(Me.GridView2.FocusedRowHandle) >= 0 Then
                            Bank_Detail.Rows(Me.GridView2.FocusedRowHandle).Delete()
                            Me.GridView2.RefreshData() : Me.GridView2.UpdateCurrentRow()
                            Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
                            Delete_Action = True
                        End If
            End Select
            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                Bank_Amt_Calculation(Delete_Action)
            End If

        End If
        Me.GridView2.Focus()
    End Sub
    Public Sub DataNavigation_Adv(ByVal Action As String)
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("P a r t y   N o t   S e l e c t e d . . . !", "Advance Payment Detail", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.GLookUp_PartyList1.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
            Select Case Action
                Case "EDIT", "VIEW"
                    If Me.GridView3.RowCount > 0 And Val(Me.GridView3.FocusedRowHandle) >= 0 Then
                        Dim xfrm As New Frm_Voucher_Win_Gen_Pay_Adv
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Advance Ajustment"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Advance Ajustment"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If

                        xfrm.i_PartyID = Me.GridControl3.Tag
                        xfrm.BE_ItemName.Text = Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Item").ToString()
                        xfrm.BE_Purpose.Text = Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Purpose").ToString()
                        xfrm.BE_Other_Detail.Text = Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Other Details").ToString()
                        xfrm.BE_Adv_Amt.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Advance").ToString())
                        xfrm.BE_Adjust_Amt.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Adjusted").ToString())
                        xfrm.BE_Refund_Amt.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Refund").ToString())
                        xfrm.BE_OS_Amt.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Out-Standing").ToString())
                        xfrm.Next_Year_Balance = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Next Year Out-Standing").ToString())
                        If IsDate(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Given Date")) Then
                            xfrm.BE_Given_Date.Text = Format(DateValue(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Given Date").ToString()), Base._Date_Format_Current)
                        End If
                        If Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Payment").ToString()) <= 0 Then
                            If xfrm.Tag = Common_Lib.Common.Navigation_Mode._New Or xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                                xfrm.Txt_Amount.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Out-Standing").ToString())
                            Else
                                xfrm.Txt_Amount.Text = ""
                            End If
                        Else
                            xfrm.Txt_Amount.Text = Val(Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "Payment").ToString())
                        End If

                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK Then
                            Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Payment", Val(xfrm.Txt_Amount.Text))
                            Me.GridView3.RefreshData() : Me.GridView3.UpdateCurrentRow()
                            Me.GridView3.BestFitMaxRowCount = 10 : Me.GridView3.BestFitColumns()
                        End If
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If
                Case "DELETE"
                    If Me.GridView3.RowCount > 0 And Val(Me.GridView3.FocusedRowHandle) >= 0 Then
                        Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Payment", 0)
                        Me.GridView3.RefreshData() : Me.GridView3.UpdateCurrentRow()
                        Me.GridView3.BestFitMaxRowCount = 10 : Me.GridView3.BestFitColumns()
                    End If
            End Select
            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                Advance_Amt_Calculation()
            End If
        End If
        Me.GridView3.Focus()
    End Sub
    Public Sub DataNavigation_LB(ByVal Action As String, Optional AdjustedAmount As Double = 0)
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("P a r t y   N o t   S e l e c t e d . . . !", "Advance Payment Detail", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.GLookUp_PartyList1.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
            Select Case Action
                Case "EDIT", "VIEW"
                    If Me.GridView4.RowCount > 0 And Val(Me.GridView4.FocusedRowHandle) >= 0 Then
                        Dim xfrm As New Frm_Voucher_Win_Gen_Pay_LB
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Payable Ajustment"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Payable Ajustment"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If

                        xfrm.i_PartyID = Me.GridControl3.Tag
                        xfrm.BE_ItemName.Text = Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Item").ToString()
                        xfrm.BE_Purpose.Text = Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Purpose").ToString()
                        xfrm.BE_Other_Detail.Text = Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Other Details").ToString()
                        xfrm.BE_Adv_Amt.Text = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Amount").ToString())
                        xfrm.BE_Paid_Amt.Text = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Paid").ToString())
                        xfrm.BE_OS_Amt.Text = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Out-Standing").ToString())
                        xfrm.Next_Year_Balance = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Next Year Out-Standing").ToString())
                        If IsDate(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Given Date")) Then
                            xfrm.BE_Given_Date.Text = Format(DateValue(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Given Date").ToString()), Base._Date_Format_Current)
                        End If
                        If Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Payment").ToString()) <= 0 Then
                            If xfrm.Tag = Common_Lib.Common.Navigation_Mode._New Or xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                                xfrm.Txt_Amount.Text = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Out-Standing").ToString())
                            Else
                                xfrm.Txt_Amount.Text = ""
                            End If
                        Else
                            xfrm.Txt_Amount.Text = Val(Me.GridView4.GetRowCellValue(Me.GridView4.FocusedRowHandle, "Payment").ToString())
                        End If
                        If AdjustedAmount > 0 Then xfrm.Txt_Amount.Text = IIf(AdjustedAmount <= Val(xfrm.BE_OS_Amt.Text), AdjustedAmount, Val(xfrm.BE_OS_Amt.Text))
                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK Then
                            Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Payment", Val(xfrm.Txt_Amount.Text))
                            Me.GridView4.RefreshData() : Me.GridView4.UpdateCurrentRow()
                            Me.GridView4.BestFitMaxRowCount = 10 : Me.GridView4.BestFitColumns()
                        End If
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If
                Case "DELETE"
                    If Me.GridView4.RowCount > 0 And Val(Me.GridView4.FocusedRowHandle) >= 0 Then
                        Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Payment", 0)
                        Me.GridView4.RefreshData() : Me.GridView4.UpdateCurrentRow()
                        Me.GridView4.BestFitMaxRowCount = 10 : Me.GridView4.BestFitColumns()
                    End If
            End Select
            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                LB_Amt_Calculation()
            End If
        End If
        Me.GridView4.Focus()
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        If TitleX.Text = "Land and Building Payment" Then
            xPleaseWait.Show("Land and Building Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Else
            xPleaseWait.Show("Payment Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
            Me.TitleX.Text = "Payment"
        End If
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Inv_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_DueDate.Properties.NullValuePrompt = Base._Date_Format_Current

        GLookUp_PartyList1.Tag = "" : LookUp_GetPartyList()
        Me.GridControl3.Tag = ""
        SetGridData()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.Text = "New ~ " & Me.TitleX.Text
            Me.Txt_V_NO.Text = ""
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "Edit ~ " & Me.TitleX.Text
            Data_Binding()

        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.Text = "Delete ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "View ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
            Me.T_VIEW.Enabled = True : Me.BUT_VIEW.Enabled = True
            Me.T_VIEW_B.Enabled = True : Me.BUT_VIEW_B.Enabled = True
            Me.T_VIEW_A.Enabled = True : Me.BUT_VIEW_A.Enabled = True
            Me.T_VIEW_L.Enabled = True : Me.BUT_VIEW_L.Enabled = True
        End If
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        Dim dsPayment As New Common_Lib.RealTimeService.Param_PaymentData
        dsPayment = Base._Voucher_DBOps.GetPaymentDetails(Me.xMID.Text)

        Dim d1 As New DataTable
        Dim d3 As New DataTable
        Dim d4 As New DataTable
        Dim d5 As New DataTable
        Dim d6 As New DataTable
        Dim d7 As New DataTable

        d1 = dsPayment.param_MasterInfo
        d3 = dsPayment.param_TransactionInfo
        d4 = dsPayment.param_TransactionItem
        d5 = dsPayment.param_BankPayment
        d6 = dsPayment.param_TxnAdvancePayment
        d7 = dsPayment.param_TxnLiabPayment
        LB_DOCS_ARRAY = dsPayment.param_LB_DOCS_ARRAY
        LB_EXTENDED_PROPERTY_TABLE = dsPayment.param_LB_EXTENDED_PROPERTY

        Dim xDate As DateTime = Nothing
        xDate = d1.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Payment", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+
        LastEditedOn = Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON"))

        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        Txt_Inv_No.DataBindings.Add("TEXT", d1, "TR_INV_NO")
        If Not IsDBNull(d1.Rows(0)("TR_INV_DATE")) Then
            xDate = d1.Rows(0)("TR_INV_DATE") : Txt_Inv_Date.DateTime = xDate
        End If

        If Not IsDBNull(d1.Rows(0)("TR_AB_ID_1")) Then
            If d1.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", d1.Rows(0)("TR_AB_ID_1")))
                Me.GLookUp_PartyList1.EditValue = d1.Rows(0)("TR_AB_ID_1")
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            End If
        End If
        Me.GLookUp_PartyList1.Properties.ReadOnly = False
        'Party_Outstanding_Advances(Me.GLookUp_PartyList1.Tag)
        'Party_Outstanding_Liabilities(Me.GLookUp_PartyList1.Tag)
        Sub_Amt_Calculation(False)


        'ITEM DETAIL
        For Each XRow In d4.Rows
            ROW = DT.NewRow
            ROW("Sr.") = XRow("TR_SR_NO")
            ROW("Item_ID") = XRow("TR_ITEM_ID")
            ROW("Item_Led_ID") = XRow("TR_LED_ID")
            ROW("Item_Trans_Type") = XRow("TR_TRANS_TYPE")
            ROW("Item_Party_Req") = XRow("TR_PARTY_REQ")
            ROW("Item_Profile") = XRow("TR_PROFILE")
            ROW("ITEM_VOUCHER_TYPE") = XRow("ITEM_VOUCHER_TYPE")
            ROW("Item Name") = XRow("TR_ITEM_NAME")
            ROW("Head") = XRow("TR_ITEM_HEAD")
            ROW("Qty.") = XRow("TR_QTY")
            ROW("Unit") = XRow("TR_UNIT")
            ROW("Rate") = XRow("TR_RATE")
            ROW("Amount") = XRow("TR_AMOUNT")
            ROW("Remarks") = XRow("TR_REMARKS")
            ROW("TDS") = XRow("TDS")
            'Purpose
            ROW("Pur_ID") = XRow("Pur_ID")
            ROW("LOC_ID") = XRow("LOC_ID")
            ROW("CREATION_PROF_REC_ID") = XRow("CREATION_PROF_REC_ID")
            If Not IsDBNull(ROW("CREATION_PROF_REC_ID")) Then If ROW("CREATION_PROF_REC_ID").ToString.Length > 0 Then IsProfileCreationVoucher = True
            'Gold/Silver
            ROW("GS_DESC_MISC_ID") = XRow("GS_DESC_MISC_ID")
            ROW("GS_ITEM_WEIGHT") = XRow("GS_ITEM_WEIGHT")
            'OTHER ASSET
            ROW("AI_TYPE") = XRow("AI_TYPE")
            ROW("AI_MAKE") = XRow("AI_MAKE")
            ROW("AI_MODEL") = XRow("AI_MODEL")
            ROW("AI_SERIAL_NO") = XRow("AI_SERIAL_NO")
            ROW("AI_WARRANTY") = XRow("AI_WARRANTY")
            ROW("AI_IMAGE") = XRow("AI_IMAGE")
            If (Len(XRow("AI_PUR_DATE")) > 0) Then ROW("AI_PUR_DATE") = DateTime.ParseExact(XRow("AI_PUR_DATE"), Base._Server_Date_Format_Short, Nothing)
            'LIVE STOCK
            ROW("LS_NAME") = XRow("LS_NAME")
            ROW("LS_BIRTH_YEAR") = XRow("LS_BIRTH_YEAR")
            ROW("LS_INSURANCE") = XRow("LS_INSURANCE")
            ROW("LS_INSURANCE_ID") = XRow("LS_INSURANCE_ID")
            ROW("LS_INS_POLICY_NO") = XRow("LS_INS_POLICY_NO")
            ROW("LS_INS_AMT") = XRow("LS_INS_AMT")
            If (Len(XRow("LS_INS_DATE")) > 0) Then ROW("LS_INS_DATE") = DateTime.ParseExact(XRow("LS_INS_DATE"), Base._Server_Date_Format_Short, Nothing)
            'VEHICLES
            ROW("VI_MAKE") = XRow("VI_MAKE")
            ROW("VI_MODEL") = XRow("VI_MODEL")
            ROW("VI_REG_NO_PATTERN") = XRow("VI_REG_NO_PATTERN")
            ROW("VI_REG_NO") = XRow("VI_REG_NO")
            If (Len(XRow("VI_REG_DATE")) > 0) Then ROW("VI_REG_DATE") = DateTime.ParseExact(XRow("VI_REG_DATE"), Base._Server_Date_Format_Short, Nothing)
            ROW("VI_OWNERSHIP") = XRow("VI_OWNERSHIP")
            ROW("VI_OWNERSHIP_AB_ID") = XRow("VI_OWNERSHIP_AB_ID")
            ROW("VI_DOC_RC_BOOK") = XRow("VI_DOC_RC_BOOK")
            ROW("VI_DOC_AFFIDAVIT") = XRow("VI_DOC_AFFIDAVIT")
            ROW("VI_DOC_WILL") = XRow("VI_DOC_WILL")
            ROW("VI_DOC_TRF_LETTER") = XRow("VI_DOC_TRF_LETTER")
            ROW("VI_DOC_FU_LETTER") = XRow("VI_DOC_FU_LETTER")
            ROW("VI_DOC_OTHERS") = XRow("VI_DOC_OTHERS")
            ROW("VI_DOC_NAME") = XRow("VI_DOC_NAME")
            ROW("VI_INSURANCE_ID") = XRow("VI_INSURANCE_ID")
            ROW("VI_INS_POLICY_NO") = XRow("VI_INS_POLICY_NO")
            If (Len(XRow("VI_INS_EXPIRY_DATE")) > 0) Then ROW("VI_INS_EXPIRY_DATE") = DateTime.ParseExact(XRow("VI_INS_EXPIRY_DATE"), Base._Server_Date_Format_Short, Nothing)
            'Land & Building
            ROW("LB_PRO_TYPE") = XRow("LB_PRO_TYPE")
            ROW("LB_PRO_CATEGORY") = XRow("LB_PRO_CATEGORY")
            ROW("LB_PRO_USE") = XRow("LB_PRO_USE")
            ROW("LB_PRO_NAME") = XRow("LB_PRO_NAME")
            ROW("LB_PRO_ADDRESS") = XRow("LB_PRO_ADDRESS")
            ROW("LB_OWNERSHIP") = XRow("LB_OWNERSHIP")
            ROW("LB_OWNERSHIP_PARTY_ID") = XRow("LB_OWNERSHIP_PARTY_ID")
            ROW("LB_SURVEY_NO") = XRow("LB_SURVEY_NO")
            ROW("LB_CON_YEAR") = XRow("LB_CON_YEAR")
            ROW("LB_RCC_ROOF") = XRow("LB_RCC_ROOF")
            If (Len(XRow("LB_PAID_DATE")) > 0) Then ROW("LB_PAID_DATE") = DateTime.ParseExact(XRow("LB_PAID_DATE"), Base._Server_Date_Format_Short, Nothing)
            If (Len(XRow("LB_PERIOD_FROM")) > 0) Then ROW("LB_PERIOD_FROM") = DateTime.ParseExact(XRow("LB_PERIOD_FROM"), Base._Server_Date_Format_Short, Nothing)
            If (Len(XRow("LB_PERIOD_TO")) > 0) Then ROW("LB_PERIOD_TO") = DateTime.ParseExact(XRow("LB_PERIOD_TO"), Base._Server_Date_Format_Short, Nothing)
            ROW("LB_DOC_OTHERS") = XRow("LB_DOC_OTHERS")
            ROW("LB_DOC_NAME") = XRow("LB_DOC_NAME")
            ROW("LB_OTHER_DETAIL") = XRow("LB_OTHER_DETAIL")
            ROW("LB_TOT_P_AREA") = XRow("LB_TOT_P_AREA")
            ROW("LB_CON_AREA") = XRow("LB_CON_AREA")
            ROW("LB_DEPOSIT_AMT") = XRow("LB_DEPOSIT_AMT")
            ROW("LB_MONTH_RENT") = XRow("LB_MONTH_RENT")
            ROW("LB_MONTH_O_PAYMENTS") = XRow("LB_MONTH_O_PAYMENTS")
            ROW("LB_REC_ID") = XRow("LB_REC_ID")
            If XRow("LB_REC_EDIT_ON") <> DateTime.MinValue Then
                ROW("LB_REC_EDIT_ON") = XRow("LB_REC_EDIT_ON")
            End If
            ROW("LB_ADDRESS1") = XRow("LB_ADDRESS1")
            ROW("LB_ADDRESS2") = XRow("LB_ADDRESS2")
            ROW("LB_ADDRESS3") = XRow("LB_ADDRESS3")
            ROW("LB_ADDRESS4") = XRow("LB_ADDRESS4")
            ROW("LB_STATE_ID") = XRow("LB_STATE_ID")
            ROW("LB_DISTRICT_ID") = XRow("LB_DISTRICT_ID")
            ROW("LB_CITY_ID") = XRow("LB_CITY_ID")
            ROW("LB_PINCODE") = XRow("LB_PINCODE")
            'TELEPHONE BILL
            ROW("TP_ID") = XRow("TP_ID")
            ROW("TP_BILL_NO") = XRow("TP_BILL_NO")
            If (Len(XRow("TP_BILL_DATE")) > 0) Then ROW("TP_BILL_DATE") = DateTime.ParseExact(XRow("TP_BILL_DATE"), Base._Server_Date_Format_Short, Nothing)
            If (Len(XRow("TP_PERIOD_FROM")) > 0) Then ROW("TP_PERIOD_FROM") = DateTime.ParseExact(XRow("TP_PERIOD_FROM"), Base._Server_Date_Format_Short, Nothing)
            If (Len(XRow("TP_PERIOD_TO")) > 0) Then ROW("TP_PERIOD_TO") = DateTime.ParseExact(XRow("TP_PERIOD_TO"), Base._Server_Date_Format_Short, Nothing)
            'WIP
            ROW("REF_REC_ID") = IIf(XRow("WIP_REF_TYPE") = "NEW", "", XRow("WIP_REC_ID"))
            ROW("REFERENCE") = XRow("WIP_REF")
            ROW("WIP_REF_TYPE") = XRow("WIP_REF_TYPE")

            DT.Rows.Add(ROW)
        Next
        Sub_Amt_Calculation(False)

        'BANK DETAIL..........................
        Dim BA_Table As DataTable = Base._Payment_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        BA_Table.Columns.Add("Name") : BA_Table.Columns.Add("Branch")
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"
        '
        Dim BB_Table As DataTable = Base._Payment_DBOps.GetBranches(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim BankJointdata As DataSet = New DataSet
        BankJointdata.Tables.Add(BA_Table)
        '
        BankJointdata.Tables.Add(BB_Table.Copy)
        Dim BA_Relation As DataRelation = BankJointdata.Relations.Add("BANK", BankJointdata.Tables("BANK_ACCOUNT_INFO").Columns("BA_BRANCH_ID"), BankJointdata.Tables("BANK_BRANCH_INFO").Columns("BB_BRANCH_ID"), False)
        For Each XROW In BankJointdata.Tables(0).Rows : For Each _Row In XROW.GetChildRows(BA_Relation) : XROW("Name") = _Row("NAME") : XROW("Branch") = _Row("Branch") : Next : Next
        BankJointdata.Relations.Clear()
        BA_Table = BankJointdata.Tables(0)
        BankJointdata.Tables.Clear()

        BankJointdata.Tables.Add(d5.Copy)
        '
        BankJointdata.Tables.Add(BA_Table.Copy)
        Dim BANK_Relation As DataRelation = BankJointdata.Relations.Add("BANK_ACC", BankJointdata.Tables("TRANSACTION_D_PAYMENT_INFO").Columns("TR_REF_ID"), BankJointdata.Tables("BANK_ACCOUNT_INFO").Columns("ID"), False)
        Dim MT_Bank_IDs As String = ""
        For Each XROW In BankJointdata.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(BANK_Relation)
                XROW("BANK_NAME") = _Row("Name") : XROW("BRANCH_NAME") = _Row("Branch") : XROW("ACC_NO") = _Row("BA_ACCOUNT_NO") : XROW("REC_EDIT_ON") = _Row("REC_EDIT_ON") ' added to show edit time of bank account instead of bank payment in grid  
            Next
            If XROW("TR_MT_BANK_ID").ToString.Length > 0 Then
                MT_Bank_IDs += "'" & XROW("TR_MT_BANK_ID").ToString & "',"
            End If
        Next
        If MT_Bank_IDs.Trim.Length > 0 Then MT_Bank_IDs = IIf(MT_Bank_IDs.Trim.EndsWith(","), Mid(MT_Bank_IDs.Trim.ToString, 1, MT_Bank_IDs.Trim.Length - 1), MT_Bank_IDs.Trim.ToString)
        If MT_Bank_IDs.Trim.Length = 0 Then MT_Bank_IDs = "''"

        Dim MT_BANK_Table As DataTable = Base._Payment_DBOps.GetBanks(MT_Bank_IDs)
        If MT_BANK_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        '
        BankJointdata.Tables.Add(MT_BANK_Table.Copy)
        Dim MT_BANK_Relation As DataRelation = BankJointdata.Relations.Add("MT_BANK_ACC", BankJointdata.Tables("TRANSACTION_D_PAYMENT_INFO").Columns("TR_MT_BANK_ID"), BankJointdata.Tables("BANK_INFO").Columns("REC_ID"), False)
        For Each XROW In BankJointdata.Tables(0).Rows : For Each _Row In XROW.GetChildRows(MT_BANK_Relation) : XROW("MT_BANK_NAME") = _Row("BI_BANK_NAME") : Next : Next
        d5 = BankJointdata.Tables(0)
        '
        For Each XRow In d5.Rows
            ROW = Bank_Detail.NewRow
            ROW("Sr.") = XRow("TR_SR_NO")
            ROW("Amount") = XRow("TR_REF_AMT")
            ROW("Mode") = XRow("TR_MODE")
            ROW("No.") = XRow("TR_REF_NO")
            If Not IsDBNull(XRow("TR_REF_DATE")) Then
                xDate = XRow("TR_REF_DATE") : ROW("Date") = Format(xDate, Base._Date_Format_Current)
            End If
            If Not IsDBNull(XRow("TR_REF_CDATE")) Then
                xDate = XRow("TR_REF_CDATE") : ROW("Clearing Date") = Format(xDate, Base._Date_Format_Current)
            End If
            ROW("Bank Name") = XRow("BANK_NAME")
            ROW("Branch") = XRow("BRANCH_NAME")
            ROW("A/c. No.") = XRow("ACC_NO")
            ROW("ID") = XRow("TR_REF_ID")

            ROW("Money Transfer Bank") = XRow("MT_BANK_NAME")
            ROW("Ref. A/c. No.") = XRow("TR_MT_ACC_NO")
            ROW("MT_BANK_ID") = XRow("TR_MT_BANK_ID")
            ROW("Edit Time") = XRow("REC_EDIT_ON") ' added for fixing reopening of #4995, #5033
            Bank_Detail.Rows.Add(ROW)
        Next
        Bank_Amt_Calculation(False)


        'ADVANCE DETAIL....................................................
        If GridView3.RowCount > 0 Then
            For I As Integer = 0 To Me.GridView3.RowCount - 1
                For Each XRow In d6.Rows
                    If Me.GridView3.GetRowCellValue(I, "AI_ID").ToString() = XRow("TR_REF_ID") Then
                        Me.GridView3.SetRowCellValue(I, "Payment", XRow("TR_REF_AMT"))
                    End If
                Next
            Next
            Me.GridView3.RefreshData() : Me.GridView3.UpdateCurrentRow()
            Advance_Amt_Calculation()
        End If


        'LIABILITIES DETAIL....................................................
        If GridView4.RowCount > 0 Then
            For I As Integer = 0 To Me.GridView4.RowCount - 1
                For Each XRow In d7.Rows
                    If Me.GridView4.GetRowCellValue(I, "LI_ID").ToString() = XRow("TR_REF_ID") Then
                        Me.GridView4.SetRowCellValue(I, "Payment", XRow("TR_REF_AMT"))
                    End If
                Next
            Next
            Me.GridView4.RefreshData() : Me.GridView4.UpdateCurrentRow()
            LB_Amt_Calculation()
        End If

        Difference_Calculation()

        Me.Txt_CreditAmt.EditValue = Format(d1.Rows(0)("TR_CREDIT_AMT"), "#0.00")
        If Val(Me.Txt_CreditAmt.EditValue) > 0 Then IsProfileCreationVoucher = True

        Me.Txt_CashAmt.EditValue = Format(d1.Rows(0)("TR_CASH_AMT"), "#0.00")

        Me.Txt_TDS_Amt.EditValue = Format(d1.Rows(0)("TR_TDS_AMT"), "#0.00")

        If Val(Me.Txt_TDS_Amt.EditValue) > 0 Then GLookUp_PartyList1.Properties.ReadOnly = True

        Txt_Narration.Text = d3.Rows(0)("TR_NARRATION")
        Txt_Reference.Text = d3.Rows(0)("TR_REFERENCE")
        Dim xCrDate As DateTime = Nothing
        If Not IsDBNull(d1.Rows(0)("TR_CR_DUE_DATE")) Then xCrDate = d1.Rows(0)("TR_CR_DUE_DATE") : Txt_DueDate.DateTime = xCrDate

        Calculation_Check()
        'Saurabh : making party uneditable if there is some adjustment 
        If Val(Txt_AdvAmt.Text) > 0 Then GLookUp_PartyList1.Properties.ReadOnly = True
        If Val(Txt_CreditAmt.Text) > 0 Then GLookUp_PartyList1.Properties.ReadOnly = True
        If Val(Txt_LB_Amt.Text) > 0 Then GLookUp_PartyList1.Properties.ReadOnly = True
        xPleaseWait.Hide()

        ''Liabilities Raised
        'Get Liab creted by current Txn
        If Val(Txt_CreditAmt.Text) > 0 Then
            Dim xTemp_LiabID As String = Base._Voucher_DBOps.GetRaisedLiabilityRecID(Me.xMID.Text) 'Get Liab creted by current Txn
            If Not xTemp_LiabID Is Nothing Then
                If xTemp_LiabID.Length > 0 Then
                    'Payment has been made againt the liability raised
                    Dim txnReferLiab As DataTable = Base._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID)
                    If Not txnReferLiab Is Nothing Then
                        If txnReferLiab.Rows.Count > 0 Then
                            Txt_CreditAmt.Properties.ReadOnly = True
                            Txt_CreditAmt.ToolTip = "Liability is readonly, as it has been adjusted in other entries"
                            Txt_CreditAmt.Tag = Txt_CreditAmt.Text

                            GLookUp_PartyList1.Properties.ReadOnly = True
                            Dim tip As New DevExpress.Utils.SuperToolTip()
                            tip.Items.Add("Party is readonly, as liability created in this voucher has been adjusted in other entries")
                            GLookUp_PartyList1.SuperTip = tip
                        End If
                    End If

                    'Liability Raised
                    Dim paramJornalEntry As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    paramJornalEntry.CrossRefId = xTemp_LiabID
                    paramJornalEntry.Excluded_Rec_M_ID = Me.xMID.Text
                    paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    paramJornalEntry.NextUnauditedYearID = Base._next_Unaudited_YearID

                    txnReferLiab = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                    If Not txnReferLiab Is Nothing Then
                        If txnReferLiab.Rows.Count > 0 Then
                            Txt_CreditAmt.Properties.ReadOnly = True
                            Txt_CreditAmt.ToolTip = "Liability is readonly, as it has been adjusted in other entries"
                            Txt_CreditAmt.Tag = Txt_CreditAmt.Text

                            GLookUp_PartyList1.Properties.ReadOnly = True
                            Dim tip As New DevExpress.Utils.SuperToolTip()
                            tip.Items.Add("Party is readonly, as liability created in this voucher has been adjusted in other entries")
                            GLookUp_PartyList1.SuperTip = tip
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Inv_Date.Enabled = False : Me.Txt_Inv_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Inv_No.Enabled = False : Me.Txt_Inv_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList1.Enabled = False : Me.GLookUp_PartyList1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_City.Enabled = False : Me.BE_City.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_PAN_No.Enabled = False : Me.BE_PAN_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_DiffAmt.Enabled = False : Me.Txt_DiffAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SubTotal.Enabled = False : Me.Txt_SubTotal.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_CashAmt.Enabled = False : Me.Txt_CashAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_BankAmt.Enabled = False : Me.Txt_BankAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_CreditAmt.Enabled = False : Me.Txt_CreditAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_TDS_Amt.Enabled = False : Me.Txt_TDS_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_AdvAmt.Enabled = False : Me.Txt_AdvAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_LB_Amt.Enabled = False : Me.Txt_LB_Amt.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_DueDate.Enabled = False : Me.Txt_DueDate.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.But_PersAdd.Enabled = False : Me.But_PersManage.Enabled = False
        Me.BUT_NEW.Enabled = False : Me.BUT_EDIT.Enabled = False : Me.BUT_DELETE.Enabled = False : Me.BUT_VIEW.Enabled = False
        Me.T_New.Enabled = False : Me.T_Edit.Enabled = False : Me.T_Delete.Enabled = False : Me.T_VIEW.Enabled = False

        Me.BUT_NEW_B.Enabled = False : Me.BUT_EDIT_B.Enabled = False : Me.BUT_DELETE_B.Enabled = False : Me.BUT_VIEW_B.Enabled = False
        Me.T_NEW_B.Enabled = False : Me.T_EDIT_B.Enabled = False : Me.T_DELETE_B.Enabled = False : Me.T_VIEW_B.Enabled = False

        Me.BUT_EDIT_A.Enabled = False : Me.BUT_DELETE_A.Enabled = False : Me.BUT_VIEW_A.Enabled = False
        Me.T_EDIT_A.Enabled = False : Me.T_DELETE_A.Enabled = False : Me.T_VIEW_A.Enabled = False

        Me.BUT_EDIT_L.Enabled = False : Me.BUT_DELETE_L.Enabled = False : Me.BUT_VIEW_L.Enabled = False
        Me.T_EDIT_L.Enabled = False : Me.T_DELETE_L.Enabled = False : Me.T_VIEW_L.Enabled = False
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
        Me.ToolTip1.Hide(Me.Txt_CashAmt)
    End Sub
    Private Function FindLocationUsage(PropertyID As String, Optional Exclude_Sold_TF As Boolean = True) As String
        Dim Message As String = ""
        Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID)
        For Each cRow As DataRow In Locations.Rows
            Dim LocationID As String = cRow(0).ToString()
            Dim UsedPage As String = Base._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF)
            Dim DeleteAllow As Boolean = True
            If UsedPage.Length > 0 Then DeleteAllow = False
            If Not DeleteAllow Then
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                    Message = "C a n ' t   D e l e t e . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                Else
                    Message = "C a n ' t   E d i t . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                End If
                Exit For
            End If
        Next
        Return Message
    End Function
    Private Function GetClosedBankAccNo(BankID) As String
        Dim MaxValue As Object = Nothing
        MaxValue = Base._Voucher_DBOps.GetBankAccount(BankID, "")
        If MaxValue Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Return ""
        End If
        If IsDBNull(MaxValue) Then
            Return ""
        Else
            Return MaxValue
        End If
    End Function
    Private Function ConvertAsDecimal(Val As String)
        If Val.Length = 0 Then Return 0
        Return Convert.ToDecimal(Val)
    End Function

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_PartyList1
    Private Sub GLookUp_PartyList1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList1.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList1.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList1.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList1.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList1.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_PartyList1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList1View.RowCount > 0 And Val(Me.GLookUp_PartyList1View.FocusedRowHandle) >= 0) Then
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_ID").ToString
                Me.BE_PAN_No.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_PAN_NO").ToString
                Me.BE_City.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_CITY").ToString
                Me.Txt_AdvAmt.Text = "0.00" : Me.Txt_LB_Amt.Text = "0.00"
                Party_Outstanding_Advances(Me.GLookUp_PartyList1.Tag)
                Party_Outstanding_Liabilities(Me.GLookUp_PartyList1.Tag)
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_PartyList1.EditValueChanging
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            If Not e.OldValue Is Nothing Then
                If e.OldValue.ToString.Length > 0 Then
                    For ctr As Integer = 0 To GridView1.RowCount - 1
                        Dim Message As String = CreatedItemReferenceChecks(ctr, True) 'checks if for curr advance/deposit created , any reference has been posted ??
                        If Message.Length > 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Party Change is not allowed!!" & vbNewLine & vbNewLine & Message, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            e.Cancel = True
                        End If
                    Next
                End If
            End If
        End If
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_PartyList1_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_PartyList1_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator(CriteriaOperator.Parse("Replace(Replace(Replace(Replace([C_NAME],'.',''),' ',''),',',''),'-','')", Nothing), "%" + edit.AutoSearchText.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("-", "") + "%", BinaryOperatorType.Like)
        filterCondition = New GroupOperator(GroupOperatorType.[Or], {op1}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub GLookUp_PartyList1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.Click
        If GLookUp_PartyList1.Properties.ReadOnly = True Then
            Me.ToolTip1.ToolTipTitle = "Party change not allowed . . ."
            Me.ToolTip1.Show("P a r t y   C a n ' t  b e   c h a n g e d   i f   t h e r e   i s   s o m e   A d v a n c e , D e p o s i t  a d j u s t m e n t  o r  T D S  i n c l u d e d. . . !" & vbNewLine & vbNewLine & "Please delete this entry and enter a fresh one to change party.", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
            Me.GLookUp_PartyList1.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._Payment_DBOps.GetParties()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("C_NAME") = "" : d1.Rows.Add(ROW)

        Dim dview As New DataView(d1) : dview.Sort = "C_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_PartyList1.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList1.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList1.Properties.DataSource = dview
            Me.GLookUp_PartyList1View.RefreshData()
            Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            Me.GLookUp_PartyList1.Text = ""
        Else
            Me.GLookUp_PartyList1.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList1.Properties.ReadOnly = False
    End Sub
    '2
    Private Sub Party_Outstanding_Advances(ByVal xParty_ID As String)
        Dim PmtAdvances As Common_Lib.RealTimeService.Param_GetPaymentAdvances = New Common_Lib.RealTimeService.Param_GetPaymentAdvances
        PmtAdvances.Adv_Party_ID = xParty_ID
        PmtAdvances.Next_YearID = Base._next_Unaudited_YearID
        PmtAdvances.Prev_YearId = Base._prev_Unaudited_YearID
        PmtAdvances.Tr_M_Id = Me.xMID.Text
        Dim ADV_TABLE As DataTable = Base._Payment_DBOps.GetPendingAdvances(PmtAdvances)
        If ADV_TABLE Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        'Dim ADV_TABLE As DataTable = Base._Payment_DBOps.GetPendingAdvances(xParty_ID)
        'If ADV_TABLE Is Nothing Then
        '    Base.HandleDBError_OnNothingReturned()
        '    Exit Sub
        'End If
        '   If ADV_TABLE.Rows.Count > 0 Then
        'Dim JointData As DataSet = GetAdvanceAdjustments(xParty_ID, ADV_TABLE)

        '..............................................................
        Me.GridControl3.DataSource = ADV_TABLE
        'Me.GridControl3.DataMember = "ADVANCES_INFO"
        Me.GridView3.Columns("AI_ITEM_ID").Visible = False
        Me.GridView3.Columns("AI_ID").Visible = False
        Me.GridView3.Columns("OFFSET_ID").Visible = False
        Me.GridView3.Columns("Sr").Visible = False
        Me.GridView3.Columns("Next Year Out-Standing").Visible = False
        Me.GridView3.Columns("REF_CREATION_DATE").Visible = False
        Me.GridView3.Columns("Advance").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView3.Columns("Advance").DisplayFormat.FormatString = "#0.00"
        Me.GridView3.Columns("Out-Standing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView3.Columns("Out-Standing").DisplayFormat.FormatString = "#0.00"
        Me.GridView3.Columns("Adjusted").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView3.Columns("Adjusted").DisplayFormat.FormatString = "#0.00"
        Me.GridView3.Columns("Refund").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView3.Columns("Refund").DisplayFormat.FormatString = "#0.00"
        Me.GridView3.Columns("Payment").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView3.Columns("Payment").DisplayFormat.FormatString = "#0.00"
        Me.GridView3.BestFitMaxRowCount = 10 : Me.GridView3.BestFitColumns()
        '     End If
    End Sub

    'Private Function GetAdvanceAdjustments(xParty_ID As String, ADV_TABLE As DataTable) As DataSet
    '    'Payment Table...
    '    Dim ADV_PAY_TABLE As DataTable = Nothing
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, True, Base._prev_Unaudited_YearID) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, True, Base._open_Year_ID, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    Else
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, False, Base._prev_Unaudited_YearID) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, False, Base._open_Year_ID, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    End If

    '    If ADV_PAY_TABLE Is Nothing Then
    '        Base.HandleDBError_OnNothingReturned()
    '        Exit Function
    '    End If

    '    'Item Table...
    '    Dim ITEM_Table As DataTable = Base._Payment_DBOps.GetAdvanceItems()
    '    If ITEM_Table Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If

    '    'Journal Adjustments 
    '    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only 'Adjustments of Advances
    '    param.NextUnauditedYearID = Base._next_Unaudited_YearID
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADJ_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '    'Journal Additions 
    '    param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only 'Adjustments of Advances
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADDITION_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)


    '    Dim JointData As DataSet = New DataSet
    '    JointData.Tables.Add(ADV_TABLE)
    '    'Relationship...
    '    '1
    '    JointData.Tables.Add(ITEM_Table.Copy) : Dim Item_Relation As DataRelation = JointData.Relations.Add("Item", JointData.Tables("ADVANCES_INFO").Columns("AI_ITEM_ID"), JointData.Tables("ITEM_INFO").Columns("ITEM_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each Item_Row In XRow.GetChildRows(Item_Relation) : XRow("Item") = Item_Row("ITEM_NAME") : XRow("OFFSET_ID") = Item_Row("ITEM_OFFSET_REC_ID") : Next : Next
    '    '2
    '    JointData.Tables.Add(ADV_PAY_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointData.Relations.Add("Advance", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation) : XRow("Adjusted") = _Row("Adjusted") : XRow("Refund") = _Row("Refund") : Next : Next
    '    '3
    '    JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation) : XRow("Adjusted") = Val(XRow("Adjusted")) + Val(_Row("AMOUNT")) : Next : Next
    '    '4
    '    JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation) : XRow("Addition") = _Row("AMOUNT") : Next : Next


    '    'Clear Relations
    '    JointData.Relations.Clear()

    '    'Updating Out-Standing...
    '    Dim xCnt As Integer = 1
    '    For Each XRow In JointData.Tables(0).Rows : XRow("Out-Standing") = XRow("Advance") + Val(XRow("Addition")) - (XRow("Refund") + Val(XRow("Adjusted"))) : XRow("Sr") = xCnt : xCnt += 1 : Next

    '    JointData.Tables.RemoveAt(4) 'Remove Previously added Additions table 
    '    JointData.Tables.RemoveAt(3) 'Remove Previously added Adjustments table 
    '    JointData.Tables.RemoveAt(2) 'Remove Previously added Deposit Payment table 

    '    'Changes for Year Ending Process
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        'Make Prev Year Closing as Current Year Opening
    '        For Each XRow In JointData.Tables(0).Rows
    '            XRow("Addition") = 0 : XRow("Adjusted") = 0 : XRow("Refund") = 0 : XRow("Advance") = XRow("Out-Standing")
    '        Next

    '        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, True, Base._open_Year_ID) 'Get Current Year Payments 
    '        Else
    '            ADV_PAY_TABLE = Base._Payment_DBOps.GetAdvancesPaid(xMID.Text, xParty_ID, False, Base._open_Year_ID) 'Get Current Year Payments 
    '        End If

    '        'Journal Adjustments 
    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only 'Adjustments of Advances
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADJ_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '        'Journal Additions 
    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only 'Adjustments of Advances
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADDITION_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Profile_Advances)

    '        'payment
    '        JointData.Tables.Add(ADV_PAY_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointData.Relations.Add("Advance", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation1) : XRow("Adjusted") = _Row("Adjusted") : XRow("Refund") = _Row("Refund") : Next : Next
    '        'Adj
    '        JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation1 As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation1) : XRow("Adjusted") = Val(XRow("Adjusted")) + Val(_Row("AMOUNT")) : Next : Next
    '        'Add
    '        JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation1 As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("ADVANCES_INFO").Columns("AI_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation1) : XRow("Addition") = _Row("AMOUNT") : Next : Next

    '        JointData.Relations.Clear()

    '        'Updating Out-Standing...
    '        For Each XRow In JointData.Tables(0).Rows : XRow("Out-Standing") = XRow("Advance") + Val(XRow("Addition")) - (XRow("Refund") + Val(XRow("Adjusted"))) : Next
    '    End If

    '    'Delete Out-Standing Zero......................................
    '    Dim AI_Temp As DataTable = New DataTable : Dim xNrow As DataRow
    '    With AI_Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
    '    For Each XRow In JointData.Tables(0).Rows
    '        If XRow("Out-Standing") = 0 Then xNrow = AI_Temp.NewRow : xNrow("_Rid") = JointData.Tables(0).Rows.IndexOf(XRow) : AI_Temp.Rows.Add(xNrow)
    '    Next
    '    For Each XRow In AI_Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next
    '    JointData.Tables(0).AcceptChanges()
    '    Return JointData
    'End Function

    '3
    Private Sub Party_Outstanding_Liabilities(ByVal xParty_ID As String)
        Dim PmtLiabilities As Common_Lib.RealTimeService.Param_GetPaymentLiabilities = New Common_Lib.RealTimeService.Param_GetPaymentLiabilities
        PmtLiabilities.LI_Party_ID = xParty_ID
        PmtLiabilities.Next_YearID = Base._next_Unaudited_YearID
        PmtLiabilities.Prev_YearId = Base._prev_Unaudited_YearID
        PmtLiabilities.Tr_M_Id = Me.xMID.Text

        Dim LI_TABLE As DataTable = Base._Payment_DBOps.GetPendingLiabilities(PmtLiabilities)
        'If LI_TABLE Is Nothing Then
        '    Base.HandleDBError_OnNothingReturned()
        '    Exit Sub
        'End If
        'Dim AdjustedLiabilities As DataSet = New DataSet
        '   If LI_TABLE.Rows.Count > 0 Then
        'AdjustedLiabilities = GetLiabilityAdjustments(xParty_ID, LI_TABLE)
        Me.GridControl4.DataSource = LI_TABLE
        'Me.GridControl4.DataMember = "Liabilities_Info"
        Me.GridView4.Columns("LI_ITEM_ID").Visible = False
        Me.GridView4.Columns("LI_ID").Visible = False
        Me.GridView4.Columns("OFFSET_ID").Visible = False
        Me.GridView4.Columns("Sr").Visible = False
        Me.GridView4.Columns("Next Year Out-Standing").Visible = False
        Me.GridView4.Columns("REF_CREATION_DATE").Visible = False
        Me.GridView4.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView4.Columns("Amount").DisplayFormat.FormatString = "#0.00"
        Me.GridView4.Columns("Out-Standing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView4.Columns("Out-Standing").DisplayFormat.FormatString = "#0.00"
        Me.GridView4.Columns("Paid").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView4.Columns("Paid").DisplayFormat.FormatString = "#0.00"
        Me.GridView4.Columns("Payment").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric : Me.GridView4.Columns("Payment").DisplayFormat.FormatString = "#0.00"
        Me.GridView4.BestFitMaxRowCount = 10 : Me.GridView4.BestFitColumns()
        '  End If
    End Sub

    'Private Function GetLiabilityAdjustments(xParty_ID As String, LI_TABLE As DataTable) As DataSet

    '    'Payment Table...
    '    Dim LI_PAY_TABLE As DataTable = Nothing
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, True, Base._prev_Unaudited_YearID) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, True, Base._open_Year_ID, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    Else
    '        If Base._prev_Unaudited_YearID.Length > 0 Then
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, False, Base._prev_Unaudited_YearID) 'Get Previous Year Payments to calculate closing Values 
    '        Else
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, False, Base._open_Year_ID, Base._next_Unaudited_YearID) 'Get Current Year Payments 
    '        End If
    '    End If
    '    If LI_PAY_TABLE Is Nothing Then
    '        Base.HandleDBError_OnNothingReturned()
    '        Exit Function
    '    End If

    '    'Item Table...
    '    Dim ITEM_Table As DataTable = Base._Payment_DBOps.GetLiabilitiesItems()
    '    If ITEM_Table Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return Nothing
    '    End If

    '    'Journal Additions 
    '    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only 'Additions of Liab
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADDITION_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)

    '    'Journal Adjustments 
    '    param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '    param.CrossRefId = Nothing
    '    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only 'Adjustments of Liab
    '    param.NextUnauditedYearID = Base._next_Unaudited_YearID
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        param.YearID = Base._prev_Unaudited_YearID 'Get Previous Year Adjustments to calculate closing Values 
    '    Else
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '    End If
    '    Dim JOURNAL_ADJ_TABLE As DataTable = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)


    '    Dim JointData As DataSet = New DataSet
    '    JointData.Tables.Add(LI_TABLE)
    '    'Relationship...
    '    '1
    '    JointData.Tables.Add(ITEM_Table.Copy) : Dim Item_Relation As DataRelation = JointData.Relations.Add("Item", JointData.Tables("Liabilities_Info").Columns("LI_ITEM_ID"), JointData.Tables("ITEM_INFO").Columns("ITEM_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each Item_Row In XRow.GetChildRows(Item_Relation) : XRow("Item") = Item_Row("ITEM_NAME") : XRow("OFFSET_ID") = Item_Row("ITEM_OFFSET_REC_ID") : Next : Next
    '    '2
    '    JointData.Tables.Add(LI_PAY_TABLE.Copy) : Dim LB_Relation As DataRelation = JointData.Relations.Add("Liabilities", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(LB_Relation) : XRow("Paid") = _Row("Paid") : Next : Next
    '    'Clear Relations
    '    JointData.Relations.Clear()
    '    '3
    '    JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation) : XRow("Adjusted") = _Row("AMOUNT") : Next : Next
    '    '4
    '    JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation) : XRow("Addition") = _Row("AMOUNT") : Next : Next

    '    JointData.Relations.Clear()
    '    JointData.Tables.RemoveAt(4) 'Remove Previously added Additions table 
    '    JointData.Tables.RemoveAt(3) 'Remove Previously added Adjustments table 
    '    JointData.Tables.RemoveAt(2) 'Remove Previously added Liab Payment table 


    '    'Updating Out-Standing..............................
    '    Dim xCnt As Integer = 1
    '    For Each XRow In JointData.Tables(0).Rows : XRow("Out-Standing") = Convert.ToInt32(XRow("Amount") + Val(XRow("Addition")) - (Val(XRow("Paid")) + Val(XRow("Adjusted")))) : XRow("Sr") = xCnt : xCnt += 1 : Next

    '    'Changes for Year Ending Process
    '    If Base._prev_Unaudited_YearID.Length > 0 Then
    '        'Make Prev Year Closing as Current Year Opening
    '        For Each XRow In JointData.Tables(0).Rows
    '            XRow("Addition") = 0 : XRow("Adjusted") = 0 : XRow("Paid") = 0 : XRow("Amount") = XRow("Out-Standing")
    '        Next

    '        'Payment Table...
    '        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, True, Base._open_Year_ID) 'Get Current Year Payments 
    '        Else
    '            LI_PAY_TABLE = Base._Payment_DBOps.GetLiabilitiesPaid(xMID.Text, xParty_ID, False, Base._open_Year_ID) 'Get Current Year Payments 
    '        End If
    '        If LI_PAY_TABLE Is Nothing Then
    '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Return Nothing
    '        End If

    '        'Journal Additions 
    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Credit_Only 'Additions of Liab
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADDITION_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)

    '        'Journal Adjustments 
    '        param = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments
    '        param.CrossRefId = Nothing
    '        param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Debit_Only 'Adjustments of Liab
    '        param.YearID = Base._open_Year_ID 'Get Current Year Adjustments  
    '        JOURNAL_ADJ_TABLE = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)

    '        'payment
    '        JointData.Tables.Add(LI_PAY_TABLE.Copy) : Dim LB_Relation1 As DataRelation = JointData.Relations.Add("Liabilities", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(LB_Relation1) : XRow("Paid") = _Row("Paid") : Next : Next
    '        'Clear Relations
    '        JointData.Relations.Clear()
    '        'adjustments
    '        JointData.Tables.Add(JOURNAL_ADJ_TABLE.Copy) : Dim Jou_Adj_Relation1 As DataRelation = JointData.Relations.Add("Adjustment", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Debit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Adj_Relation1) : XRow("Adjusted") = _Row("AMOUNT") : Next : Next
    '        'additions
    '        JointData.Tables.Add(JOURNAL_ADDITION_TABLE.Copy) : Dim Jou_Addi_Relation1 As DataRelation = JointData.Relations.Add("Addition", JointData.Tables("Liabilities_Info").Columns("LI_ID"), JointData.Tables("Credit_Only").Columns("TR_TRF_CROSS_REF_ID"), False)
    '        For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Jou_Addi_Relation1) : XRow("Addition") = _Row("AMOUNT") : Next : Next

    '        For Each XRow In JointData.Tables(0).Rows : XRow("Out-Standing") = Convert.ToInt32(XRow("Amount") + Val(XRow("Addition")) - (Val(XRow("Paid")) + Val(XRow("Adjusted")))) : Next
    '        JointData.Relations.Clear()
    '    End If

    '    'Delete Out-Standing Zero......................................
    '    Dim LI_Temp As DataTable = New DataTable : Dim xNrow As DataRow
    '    With LI_Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
    '    For Each XRow In JointData.Tables(0).Rows
    '        If XRow("Out-Standing") = 0 Then xNrow = LI_Temp.NewRow : xNrow("_Rid") = JointData.Tables(0).Rows.IndexOf(XRow) : LI_Temp.Rows.Add(xNrow)
    '    Next
    '    For Each XRow In LI_Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next
    '    '..............................................................
    '    JointData.Tables(0).AcceptChanges()
    '    Return JointData
    'End Function

#End Region

End Class


