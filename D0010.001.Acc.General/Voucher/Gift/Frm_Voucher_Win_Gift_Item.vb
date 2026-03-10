Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_Voucher_Win_Gift_Item

#Region "Start--> Default Variables"

    Public iVoucher_Type As String = ""
    Public iTrans_Type As String = ""
    Public iLed_ID As String = ""
    Public iLed_Type As String = ""
    Public iCon_Led_Type As String = ""
    Public iSpecific_ItemID As String = ""
    Public iParty_Req As String = ""
    Public iProfile As String = ""
    Public iProfile_OLD As String = ""
    Public iSpecific_Allow As Boolean = False

    Public iPur_ID As String = ""
    Public Calc_Allow As Boolean = True
    Public iCond_Ledger_ID As String = ""
    Public iMinValue As Double = 0
    Public iMaxValue As Double = 0
    Dim Cnt_BankAccount As Integer
    Dim Vno As String = ""
    Public Vdt As String = ""
    Public iTxnM_ID As String = ""
    Public iReference As String = ""
    Public iRefType As String = ""
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
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.GLookUp_ItemList.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.O)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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
    Private Sub BUT_DEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_DEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public X_LOC_ID As String
    Public GS_DESC_MISC_ID As String : Public GS_ITEM_WEIGHT As Double
    Public AI_TYPE As String : Public AI_MAKE, AI_MODEL, AI_SERIAL_NO, AI_PUR_DATE As String : Public AI_WARRANTY As Double : Public AI_IMAGE As Byte() : Public AI_INS_AMT As Double
    Public LS_NAME, LS_BIRTH_YEAR, LS_INSURANCE, LS_INSURANCE_ID, LS_INS_POLICY_NO, LS_INS_DATE As String : Public LS_INS_AMT As Double
    Public VI_MAKE, VI_MODEL, VI_REG_NO_PATTERN, VI_REG_NO, VI_REG_DATE, VI_OWNERSHIP, VI_OWNERSHIP_AB_ID, VI_DOC_RC_BOOK, VI_DOC_AFFIDAVIT, VI_DOC_WILL, VI_DOC_TRF_LETTER, VI_DOC_FU_LETTER, VI_DOC_OTHERS, VI_DOC_NAME, VI_INSURANCE_ID, VI_INS_POLICY_NO, VI_INS_EXPIRY_DATE As String
    Public LB_PRO_TYPE, LB_PRO_CATEGORY, LB_PRO_USE, LB_PRO_NAME, LB_PRO_ADDRESS, LB_ADDRESS1, LB_ADDRESS2, LB_ADDRESS3, LB_ADDRESS4, LB_STATE_ID, LB_DISTRICT_ID, LB_CITY_ID, LB_PINCODE, LB_OWNERSHIP, LB_OWNERSHIP_PARTY_ID, LB_SURVEY_NO, LB_CON_YEAR, LB_RCC_ROOF, LB_PAID_DATE, LB_PERIOD_FROM, LB_PERIOD_TO, LB_DOC_OTHERS, LB_DOC_NAME, LB_OTHER_DETAIL, LB_REC_ID As String

    Public LB_TOT_P_AREA, LB_CON_AREA, LB_DEPOSIT_AMT, LB_MONTH_RENT, LB_MONTH_O_PAYMENTS As Double : Public LB_DOCS_ARRAY, LB_EXTENDED_PROPERTY_TABLE As DataTable
    Public Ref_RecID As String

    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click
        Hide_Properties()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then


            If Me.GLookUp_ItemListView.FocusedRowHandle <= 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If Trim(Me.Txt_Qty.Text).Contains(".") And iProfile.ToUpper = "OTHER ASSETS" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Q u a n t i t y   c a n n o t   b e   p a r t i a l   i n   c a s e   o f   M o v a b l e   A s s e t s . . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If

            If Me.GLookUp_PurListView.FocusedRowHandle < 1 Or Me.GLookUp_PurList.Text.Trim.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P u r p o s e   N o t   S e l e c t e d . . . !", Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                Me.GLookUp_PurList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PurList)
            End If

        End If
        '
        '
        Dim NewProfile As Boolean = False
        If iProfile_OLD.Length > 0 Then
            If iProfile <> iProfile_OLD Then
                NewProfile = True
            End If
        End If

        If iVoucher_Type.Trim.ToUpper = "LAND & BUILDING" And Not iProfile.ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
            Dim db_Table As DataTable = Base._Payment_DBOps.GetLandBuildingNames(iTxnM_ID)
            If db_Table Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If db_Table.Rows.Count <= 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("N o   S e l e c t a b l e   P r o p e r t y   E x i s t s !" & vbNewLine & vbNewLine & "Please create a Land and Building entry before adding expenses for construction " & vbNewLine & "or add a different voucher for expenses if you are using same voucher for property purchase and expenses!", "Add Property First...", MessageBoxButtons.OK)
                Exit Sub
            End If
            Dim xfrm As New Frm_Property_Select
            'xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
            xfrm.LB_REC_ID = LB_REC_ID
            xfrm.Txn_M_ID = Me.iTxnM_ID
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                LB_REC_ID = xfrm.LB_REC_ID
                If Not xfrm Is Nothing Then xfrm.Dispose()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                If Not xfrm Is Nothing Then xfrm.Dispose()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
            End If
        End If

        If iProfile = "WIP" Then
            Dim xFrm As New Frm_Reference_Type
            xFrm.RadioGroup1.SelectedIndex = IIf(iRefType = "NEW", 1, 0)
            xFrm.Led_Id = iLed_ID
            xFrm.Tag = Me.Tag
            xFrm.ShowDialog(Me)
            If xFrm.DialogResult = Windows.Forms.DialogResult.OK Then
                If xFrm.RadioGroup1.SelectedIndex = 0 Then
                    Dim xFrm1 As New Frm_Existing_References
                    'xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                    'xfrm.Txn_M_ID = Me.iTxnM_ID
                    xFrm1.Led_ID = iLed_ID
                    xFrm1.Ref_Rec_ID = Ref_RecID
                    xFrm1.Txn_M_ID = iTxnM_ID
                    xFrm1.Tag = Me.Tag
                    xFrm1.ShowDialog(Me)
                    If xFrm1.DialogResult = Windows.Forms.DialogResult.OK Then
                        If xFrm1.Closing + Val(Txt_Amount.Text) < 0 Then
                            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                            Me.ToolTip1.Show("Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Current Year for WIP( " & xFrm1.Reference & " ) with Original Value " & xFrm1.Opening.ToString, Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                            Me.GLookUp_PurList.Focus()
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        End If

                        If Base._next_Unaudited_YearID <> Nothing Then
                            If xFrm1.NextYearClosing + Val(Txt_Amount.Text) < 0 Then
                                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                Me.ToolTip1.Show("Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Next Year for WIP( " & xFrm1.Reference & " ) with Original Value " & xFrm1.Opening.ToString, Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                                Me.GLookUp_PurList.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            End If
                        End If

                        If iRefType = "EXISTING" Then
                            If Not Ref_RecID Is Nothing Then
                                If Ref_RecID.Length > 0 And xFrm1.Ref_Rec_ID <> Ref_RecID Then
                                    Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                                    Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", iSpecific_ItemID, Nothing, iTxnM_ID, Common_Lib.Common.Navigation_Mode._Edit, Ref_RecID)
                                    If Base._next_Unaudited_YearID <> Nothing Then
                                        If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Next Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Current Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If

                        Ref_RecID = xFrm1.Ref_Rec_ID
                        iRefType = "EXISTING"
                        iReference = ""
                        xFrm1.Dispose()
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        'Me.Close()
                    Else
                        xFrm1.Dispose()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        ' Me.Close()
                        Exit Sub
                    End If
                Else
                    Dim xFrm2 As New Frm_WIP_Window
                    xFrm2.LedID = iLed_ID
                    xFrm2.Amount = Val(Txt_Amount.Text)
                    xFrm2.Tag = Me.Tag
                    xFrm2.Reference = iReference
                    xFrm2.xID.Text = iTxnM_ID
                    xFrm2.iTxnM_ID = iTxnM_ID
                    xFrm2.ShowDialog(Me)
                    If xFrm2.DialogResult = Windows.Forms.DialogResult.OK Then

                        If iRefType = "EXISTING" Then
                            If Not Ref_RecID Is Nothing Then
                                If Ref_RecID.Length > 0 Then
                                    Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                                    Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", iSpecific_ItemID, Nothing, iTxnM_ID, Common_Lib.Common.Navigation_Mode._Edit, Ref_RecID)
                                    If Base._next_Unaudited_YearID <> Nothing Then
                                        If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Next Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If

                                    If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Current Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If

                        iReference = xFrm2.Txt_Reference.Text
                        iRefType = "NEW"
                        Ref_RecID = ""
                        xFrm2.Dispose()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        'Me.Close()
                    Else
                        xFrm2.Dispose()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        'Me.Close()
                        Exit Sub
                    End If
                End If
            Else
                xFrm.Dispose()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

        End If

        Select Case iProfile.ToUpper
            Case "GOLD", "SILVER"
                Calc_Allow = False
                Dim xfrm As New Frm_GoldSilver_Window
                xfrm.Text = Me.Text & " (Gold / Silver Detail)..."
                xfrm.Cmd_Type.Text = iProfile : xfrm.BE_ItemName.Text = Me.GLookUp_ItemList.Text
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If NewProfile Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    Else
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Else
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        End If
                        xfrm.GS_DESC_MISC_ID = GS_DESC_MISC_ID
                        xfrm.GS_LOC_AL_ID = X_LOC_ID
                        xfrm.Txt_Weight.EditValue = GS_ITEM_WEIGHT
                        xfrm.Txt_Others.Text = Me.Txt_Remarks.Text
                    End If
                End If

                xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    GS_DESC_MISC_ID = xfrm.Look_MiscList.Tag
                    X_LOC_ID = xfrm.Look_LocList.Tag
                    Me.Txt_Qty.Text = xfrm.Txt_Weight.Text
                    GS_ITEM_WEIGHT = Val(xfrm.Txt_Weight.Text)
                    Me.Txt_Rate.Text = Me.Txt_Amount.Text
                    Me.Txt_Remarks.Text = xfrm.Txt_Others.Text
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                End If
            Case "OTHER ASSETS"
                Calc_Allow = False
                Dim xfrm As New Frm_Asset_Window
                xfrm.Text = Me.Text & " (Movable Asset Detail)..."
                xfrm.BE_ItemName.Text = Me.GLookUp_ItemList.Text
                'xfrm.Txt_Amt.Enabled = False
                xfrm.IsGift = True
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    'xfrm.Txt_Amt.Text = Me.Txt_Amount.Text
                    xfrm.Txt_Rate.Text = Me.Txt_Rate.Text
                    xfrm.Txt_Qty.Text = Me.Txt_Qty.Text
                    Dim xDate As DateTime = Nothing : If IsDate(Vdt) Then xDate = Vdt : xfrm.Txt_Date.DateTime = xDate
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If NewProfile Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    Else
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Else
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        End If
                        xfrm.Txt_Make.Text = AI_MAKE
                        xfrm.Txt_Model.Text = AI_MODEL
                        xfrm.Txt_Serial.Text = AI_SERIAL_NO
                        xfrm.Txt_Warranty.Text = AI_WARRANTY
                        If Not AI_IMAGE Is Nothing Then
                            Dim myimage As Image
                            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(DirectCast(AI_IMAGE, Byte()))
                            If ms.Length > 0 Then
                                myimage = System.Drawing.Image.FromStream(ms)
                                xfrm.ImgAsset.Image = myimage
                            End If
                        End If
                        If IsDate(AI_PUR_DATE) Then
                            xfrm.Txt_Date.DateTime = DateValue(AI_PUR_DATE)
                        End If
                        xfrm.Txt_Amt.Text = Me.AI_INS_AMT 'Me.Txt_Amount.Text
                        xfrm.Txt_Rate.Text = Me.Txt_Rate.Text
                        xfrm.Txt_Qty.Text = Me.Txt_Qty.Text
                        xfrm.Txt_Others.Text = Me.Txt_Remarks.Text
                        xfrm.AI_LOC_AL_ID = X_LOC_ID
                    End If
                End If
                xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    AI_MAKE = xfrm.Txt_Make.Text
                    AI_MODEL = xfrm.Txt_Model.Text
                    AI_SERIAL_NO = xfrm.Txt_Serial.Text
                    AI_WARRANTY = Val(xfrm.Txt_Warranty.Text)
                    Dim converter As New ImageConverter
                    AI_IMAGE = converter.ConvertTo(xfrm.ImgAsset.Image, GetType(Byte()))
                    If IsDate(xfrm.Txt_Date.Text) Then
                        AI_PUR_DATE = xfrm.Txt_Date.DateTime
                    End If
                    Me.AI_INS_AMT = Val(xfrm.Txt_Amt.Text)
                    Me.Txt_Qty.Text = xfrm.Txt_Qty.Text
                    Me.Txt_Rate.Text = xfrm.Txt_Rate.Text
                    Me.Txt_Remarks.Text = xfrm.Txt_Others.Text
                    X_LOC_ID = xfrm.Look_LocList.Tag

                    If iCond_Ledger_ID <> "00000" Then
                        If Val(Txt_Amount.Text) > Val(iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(iMaxValue) Then
                            If iCon_Led_Type = "ASSET" Then AI_TYPE = "ASSET" Else AI_TYPE = "EXPENSE"
                        Else
                            If iLed_Type = "ASSET" Then AI_TYPE = "ASSET" Else AI_TYPE = "EXPENSE"
                        End If
                    Else
                        If iLed_Type = "ASSET" Then AI_TYPE = "ASSET" Else AI_TYPE = "EXPENSE"
                    End If
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                End If
            Case "LIVESTOCK"
                Calc_Allow = False
                Dim xfrm As New Frm_Live_Stock_Window
                xfrm.Text = Me.Text & " (Livestock Detail)..."
                xfrm.BE_ItemName.Text = Me.GLookUp_ItemList.Text
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If NewProfile Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    Else
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Else
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        End If
                        xfrm.Txt_Name.Text = LS_NAME
                        xfrm.LS_BIRTH_YEAR = LS_BIRTH_YEAR
                        xfrm.LS_INSURANCE = LS_INSURANCE
                        xfrm.LS_INS_ID = LS_INSURANCE_ID
                        xfrm.LS_INS_POLICY_NO = LS_INS_POLICY_NO
                        xfrm.LS_INS_AMT = LS_INS_AMT
                        If IsDate(LS_INS_DATE) Then
                            xfrm.LS_INS_DATE = DateValue(LS_INS_DATE)
                        End If
                        xfrm.Txt_Others.Text = Me.Txt_Remarks.Text
                        xfrm.LS_LOC_AL_ID = X_LOC_ID
                    End If
                End If
                xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    LS_NAME = xfrm.Txt_Name.Text
                    LS_BIRTH_YEAR = xfrm.Cmd_Year.Text
                    LS_INSURANCE = xfrm.Rad_Insurance.Text
                    LS_INSURANCE_ID = xfrm.Look_InsList.Tag
                    LS_INS_POLICY_NO = xfrm.Txt_PolicyNo.Text
                    LS_INS_AMT = Val(xfrm.Txt_InsAmt.EditValue)
                    If IsDate(xfrm.Txt_I_Date.Text) Then
                        LS_INS_DATE = xfrm.Txt_I_Date.DateTime
                    End If
                    Me.Txt_Rate.Text = Me.Txt_Amount.Text
                    Me.Txt_Remarks.Text = xfrm.Txt_Others.Text
                    X_LOC_ID = xfrm.Look_LocList.Tag
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                End If
            Case "VEHICLES"
                Calc_Allow = False
                Dim xfrm As New Frm_Vehicles_Window
                xfrm.Text = Me.Text & " (Vehicle Detail)..."
                xfrm.BE_ItemName.Text = Me.GLookUp_ItemList.Text
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If NewProfile Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    Else
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Else
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        End If
                        xfrm.Cmd_Make.Text = VI_MAKE
                        xfrm.VI_MODEL = VI_MODEL
                        xfrm.VI_REG_NO_PATTERN = VI_REG_NO_PATTERN
                        xfrm.VI_REG_NO = VI_REG_NO
                        If IsDate(VI_REG_DATE) Then
                            xfrm.Txt_RegDate.DateTime = DateValue(VI_REG_DATE)
                        End If
                        xfrm.VI_OWNERSHIP = VI_OWNERSHIP
                        xfrm.VI_OWNERSHIP_AB_ID = VI_OWNERSHIP_AB_ID
                        xfrm.VI_DOC_RC_BOOK = VI_DOC_RC_BOOK
                        xfrm.VI_DOC_AFFIDAVIT = VI_DOC_AFFIDAVIT
                        xfrm.VI_DOC_WILL = VI_DOC_WILL
                        xfrm.VI_DOC_TRF_LETTER = VI_DOC_TRF_LETTER
                        xfrm.VI_DOC_FU_LETTER = VI_DOC_FU_LETTER
                        xfrm.VI_DOC_OTHERS = VI_DOC_OTHERS
                        xfrm.VI_DOC_NAME = VI_DOC_NAME

                        xfrm.VI_INSURANCE_ID = VI_INSURANCE_ID
                        xfrm.Txt_PolicyNo.Text = VI_INS_POLICY_NO
                        If IsDate(VI_INS_EXPIRY_DATE) Then
                            xfrm.Txt_E_Date.DateTime = DateValue(VI_INS_EXPIRY_DATE)
                        End If

                        xfrm.Txt_Others.Text = Me.Txt_Remarks.Text
                        xfrm.VI_LOC_AL_ID = X_LOC_ID
                    End If

                End If
                xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    VI_MAKE = xfrm.Cmd_Make.Text
                    VI_MODEL = xfrm.Cmd_Model.Text
                    VI_REG_NO_PATTERN = xfrm.RAD_RegPattern.Text
                    VI_REG_NO = xfrm.Txt_RegNo.Text
                    If IsDate(xfrm.Txt_RegDate.Text) Then
                        VI_REG_DATE = xfrm.Txt_RegDate.DateTime
                    End If
                    VI_OWNERSHIP = xfrm.Cmd_Ownership.Text
                    Dim _Owner_Free As String = ""
                    If xfrm.Cmd_Ownership.SelectedIndex = 1 Then _Owner_Free = xfrm.Look_OwnList.Tag
                    If xfrm.Cmd_Ownership.SelectedIndex = 2 Then _Owner_Free = xfrm.Look_OwnList.Tag
                    VI_OWNERSHIP = xfrm.Cmd_Ownership.Text
                    VI_OWNERSHIP_AB_ID = _Owner_Free
                    VI_DOC_RC_BOOK = xfrm.Chk_RC_Book.Tag
                    VI_DOC_AFFIDAVIT = xfrm.Chk_Affidavit.Tag
                    VI_DOC_WILL = xfrm.Chk_Will.Tag
                    VI_DOC_TRF_LETTER = xfrm.Chk_Trf_Letter.Tag
                    VI_DOC_FU_LETTER = xfrm.Chk_FU_Letter.Tag
                    VI_DOC_OTHERS = xfrm.Chk_OtherDoc.Tag
                    VI_DOC_NAME = xfrm.Txt_OtherDoc.Text
                    VI_INSURANCE_ID = xfrm.Look_InsList.Tag
                    VI_INS_POLICY_NO = xfrm.Txt_PolicyNo.Text
                    If IsDate(xfrm.Txt_E_Date.Text) Then
                        VI_INS_EXPIRY_DATE = xfrm.Txt_E_Date.DateTime
                    End If

                    Me.Txt_Rate.Text = Me.Txt_Amount.Text
                    Me.Txt_Remarks.Text = xfrm.Txt_Others.Text
                    X_LOC_ID = xfrm.Look_LocList.Tag
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                End If
            Case "LAND & BUILDING"
                Calc_Allow = False
                Dim xfrm As New Frm_Property_Window
                xfrm.Text = Me.Text & " (Land & Building Detail)..."
                xfrm.IsGift = True
                xfrm.ITEM_ID = Me.GLookUp_ItemList.Tag

                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If NewProfile Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    Else
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        Else
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        End If
                        xfrm.LB_PRO_TYPE = LB_PRO_TYPE
                        xfrm.LB_PRO_CATEGORY = LB_PRO_CATEGORY
                        xfrm.LB_PRO_USE = LB_PRO_USE
                        xfrm.LB_PRO_NAME = LB_PRO_NAME
                        xfrm.LB_PRO_ADDRESS = LB_PRO_ADDRESS
                        'xfrm.Address = LB_ADDRESS
                        xfrm.LB_ADDRESS1 = LB_ADDRESS1
                        xfrm.LB_ADDRESS2 = LB_ADDRESS2
                        xfrm.LB_ADDRESS3 = LB_ADDRESS3
                        xfrm.LB_ADDRESS4 = LB_ADDRESS4
                        xfrm.LB_CITY_ID = LB_CITY_ID
                        xfrm.LB_DISTRICT_ID = LB_DISTRICT_ID
                        xfrm.LB_STATE_ID = LB_STATE_ID
                        xfrm.LB_PINCODE = LB_PINCODE
                        xfrm.LB_OWNERSHIP = LB_OWNERSHIP
                        xfrm.LB_OWNERSHIP_PARTY_ID = LB_OWNERSHIP_PARTY_ID
                        xfrm.LB_SURVEY_NO = LB_SURVEY_NO
                        xfrm.LB_CON_YEAR = LB_CON_YEAR
                        xfrm.LB_RCC_ROOF = LB_RCC_ROOF
                        xfrm.LB_PAID_DATE = LB_PAID_DATE
                        xfrm.LB_PERIOD_FROM = LB_PERIOD_FROM
                        xfrm.LB_PERIOD_TO = LB_PERIOD_TO
                        xfrm.LB_DOC_OTHERS = LB_DOC_OTHERS
                        xfrm.LB_DOC_NAME = LB_DOC_NAME
                        xfrm.LB_OTHER_DETAIL = LB_OTHER_DETAIL
                        xfrm.LB_TOT_P_AREA = LB_TOT_P_AREA
                        xfrm.LB_CON_AREA = LB_CON_AREA
                        xfrm.LB_DEPOSIT_AMT = LB_DEPOSIT_AMT
                        xfrm.LB_MONTH_RENT = LB_MONTH_RENT
                        xfrm.LB_MONTH_O_PAYMENTS = LB_MONTH_O_PAYMENTS
                        xfrm.LB_REC_ID = LB_REC_ID
                        xfrm.xID.Text = LB_REC_ID
                        If LB_DOCS_ARRAY Is Nothing Then FetchLBDocuments()
                        xfrm.LB_DOCS_ARRAY = LB_DOCS_ARRAY
                        If LB_EXTENDED_PROPERTY_TABLE Is Nothing Then FetchExtensionData()
                        xfrm.LB_EXTENDED_PROPERTY_TABLE = LB_EXTENDED_PROPERTY_TABLE
                    End If

                End If
                ' xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    LB_PRO_TYPE = xfrm.Cmd_PType.Text
                    LB_PRO_CATEGORY = xfrm.Cmd_PCategory.Text
                    LB_PRO_USE = xfrm.Cmd_PUse.Text
                    LB_PRO_NAME = xfrm.Txt_PName.Text
                    LB_PRO_ADDRESS = xfrm.Txt_Address.Text
                    'LB_ADDRESS = xfrm.Txt_Add.Text
                    LB_ADDRESS1 = xfrm.Txt_R_Add1.Text
                    LB_ADDRESS2 = xfrm.Txt_R_Add2.Text
                    LB_ADDRESS3 = xfrm.Txt_R_Add3.Text
                    LB_ADDRESS4 = xfrm.Txt_R_Add4.Text
                    LB_STATE_ID = xfrm.GLookUp_StateList.Tag
                    LB_CITY_ID = xfrm.GLookUp_CityList.Tag
                    LB_DISTRICT_ID = xfrm.GLookUp_DistrictList.Tag
                    LB_PINCODE = xfrm.Txt_R_Pincode.Text
                    LB_OWNERSHIP = xfrm.Cmd_Ownership.Text
                    If xfrm.Cmd_Ownership.SelectedIndex = 1 Then
                        LB_OWNERSHIP_PARTY_ID = xfrm.Look_OwnList.Tag
                    Else
                        LB_OWNERSHIP_PARTY_ID = "NULL"
                    End If
                    LB_SURVEY_NO = xfrm.Txt_SNo.Text
                    LB_CON_YEAR = xfrm.Cmd_Con_Year.Text
                    LB_RCC_ROOF = xfrm.Cmd_RccType.Text
                    If IsDate(xfrm.Txt_PaidDate.Text) Then
                        LB_PAID_DATE = xfrm.Txt_PaidDate.DateTime
                    End If
                    If IsDate(xfrm.Txt_F_Date.Text) Then
                        LB_PERIOD_FROM = xfrm.Txt_F_Date.DateTime
                    End If
                    If IsDate(xfrm.Txt_T_Date.Text) Then
                        LB_PERIOD_TO = xfrm.Txt_T_Date.DateTime
                    End If
                    LB_DOC_OTHERS = xfrm.Chk_OtherDoc.Tag
                    LB_DOC_NAME = xfrm.Txt_OtherDoc.Text
                    LB_OTHER_DETAIL = Me.Txt_Remarks.Text
                    LB_TOT_P_AREA = Val(xfrm.Txt_Tot_Area.Text)
                    LB_CON_AREA = Val(xfrm.Txt_Con_Area.Text)
                    LB_DEPOSIT_AMT = Val(xfrm.Txt_Dep_Amt.Text)
                    LB_MONTH_RENT = Val(xfrm.Txt_Mon_Rent.Text)
                    LB_MONTH_O_PAYMENTS = Val(xfrm.Txt_Other_Payments.Text)
                    LB_REC_ID = xfrm.xID.Text

                    'Extended Property
                    FillExtensionTable()
                    For I As Integer = 0 To xfrm.GridView1.RowCount - 1
                        If Val(xfrm.GridView1.GetRowCellValue(I, "Sr.").ToString) > 0 Then
                            Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                            Row("LB_MOU_DATE") = xfrm.GridView1.GetRowCellValue(I, "M.O.U. Date").ToString
                            Row("LB_SR_NO") = Val(xfrm.GridView1.GetRowCellValue(I, "Sr.").ToString)
                            Row("LB_INS_ID") = xfrm.GridView1.GetRowCellValue(I, "Ins_ID").ToString
                            Row("LB_TOT_P_AREA") = Val(xfrm.GridView1.GetRowCellValue(I, "Total Plot Area (Sq.Ft.)").ToString())
                            Row("LB_CON_AREA") = Val(xfrm.GridView1.GetRowCellValue(I, "Constructed Area (Sq.Ft.)").ToString())
                            Row("LB_CON_YEAR") = xfrm.GridView1.GetRowCellValue(I, "Construction Year").ToString()
                            Row("LB_VALUE") = Val(xfrm.GridView1.GetRowCellValue(I, "Value").ToString())
                            Row("LB_OTHER_DETAIL") = xfrm.GridView1.GetRowCellValue(I, "Other Detail").ToString()
                            Row("LB_REC_ID") = xfrm.xID.Text
                            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                        End If
                    Next

                    'Documents 
                    LB_DOCS_ARRAY = New DataTable
                    With LB_DOCS_ARRAY
                        .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                        .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                    End With
                    For Each currSelection As Object In xfrm.Chk_DocList.CheckedItems
                        Dim checkedRow As DataRowView = CType(currSelection, DataRowView)
                        Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                        Row("LB_MISC_ID") = checkedRow("ID").ToString()
                        Row("LB_REC_ID") = xfrm.xID.Text
                        LB_DOCS_ARRAY.Rows.Add(Row)
                    Next

                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    Me.Close()
                End If
            Case Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
        End Select
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
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Remarks.GotFocus, Txt_Qty.GotFocus, Txt_Qty.Click, Txt_Rate.GotFocus, Txt_Rate.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Or txt.Name = Txt_Qty.Name Or txt.Name = Txt_Rate.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amount.KeyPress, Txt_Remarks.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_Remarks.KeyDown, Txt_Qty.KeyDown, Txt_Rate.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Remarks.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
    Private Sub Txt_Qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Qty.EditValueChanged, Txt_Rate.EditValueChanged
        'If Calc_Allow = True Then
        '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Txt_Amount.Text = Val(Txt_Qty.Text) * Val(Txt_Rate.Text)
        'End If
        Txt_Amount.Text = IIf(Txt_Qty.Text.Length = 0, 1, Val(Txt_Qty.Text))
    End Sub

    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_UOM.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_UOM.KeyDown
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
        Me.TitleX.Text = "Item Detail"
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
            Select Case iProfile.ToUpper
                Case "GOLD", "SILVER", "OTHER ASSETS", "LIVESTOCK", "VEHICLES", "LAND & BUILDING"
                    Me.BUT_SAVE_COM.Text = "View"
                Case Else
                    If iVoucher_Type.Trim.ToUpper = "LAND & BUILDING" And Not iProfile.ToUpper = "LAND & BUILDING" Then
                        Me.BUT_SAVE_COM.Text = "View"
                    Else
                        Me.BUT_SAVE_COM.Visible = False
                    End If
            End Select
        End If
        If iSpecific_Allow Then 'By Item-wise Selection
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
            Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            Me.GLookUp_ItemList.Properties.ReadOnly = False
            iSpecific_Allow = False
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.Txt_Amount.Text = "1"
        Me.GLookUp_ItemList.Focus()
    End Sub

    Private Sub Data_Binding()
        Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
        Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
        Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
        Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Me.GLookUp_ItemList.Properties.ReadOnly = False
        Me.GLookUp_ItemList.Tag = iSpecific_ItemID

        Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
        Me.GLookUp_PurListView.FocusedRowHandle = Me.GLookUp_PurListView.LocateByValue("PUR_ID", iPur_ID)
        Me.GLookUp_PurList.EditValue = iPur_ID
        Me.GLookUp_PurList.Properties.Tag = "SHOW"
        Me.GLookUp_PurList.Properties.ReadOnly = False
        Me.GLookUp_PurList.Tag = iPur_ID

    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_UOM.Enabled = False : Me.Cmd_UOM.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Rate.Enabled = False : Me.Txt_Rate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.Txt_Amount)
    End Sub
    Private Sub FillExtensionTable()
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
    End Sub
    Private Sub FetchExtensionData()
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
        Dim LB_Ext As New Common_Lib.Get_Data(Base, "SYS", "LAND_BUILDING_EXTENDED_INFO", "SELECT LB_SR_NO, LB_INS_ID, LB_TOT_P_AREA, LB_CON_AREA,LB_CON_YEAR, LB_MOU_DATE, LB_VALUE, LB_OTHER_DETAIL FROM LAND_BUILDING_EXTENDED_INFO where LB_REC_ID ='" & LB_REC_ID & "'")

        For Each XROW As DataRow In LB_Ext._dc_DataTable.Rows
            Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
            Row("LB_MOU_DATE") = XROW("LB_MOU_DATE")
            Row("LB_SR_NO") = XROW("LB_SR_NO")
            Row("LB_INS_ID") = XROW("LB_INS_ID")
            Row("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
            Row("LB_CON_AREA") = XROW("LB_CON_AREA")
            Row("LB_CON_YEAR") = XROW("LB_CON_YEAR")
            Row("LB_VALUE") = XROW("LB_VALUE")
            Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
            Row("LB_REC_ID") = LB_REC_ID
            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
        Next
    End Sub
    Public Sub FetchLBDocuments()
        LB_DOCS_ARRAY = New DataTable
        With LB_DOCS_ARRAY
            .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
        End With
        Dim LB_DOC As New Common_Lib.Get_Data(Base, "SYS", "LAND_BUILDING_EXTENDED_INFO", "SELECT LB_MISC_ID FROM LAND_BUILDING_DOCUMENTS_INFO where LB_REC_ID ='" & LB_REC_ID & "'")
        For Each XROW As DataRow In LB_DOC._dc_DataTable.Rows
            Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
            Row("LB_MISC_ID") = XROW("LB_MISC_ID")
            Row("LB_REC_ID") = LB_REC_ID
            LB_DOCS_ARRAY.Rows.Add(Row)
        Next
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
        End If

    End Sub
    Private Sub GLookUp_ItemList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.EditValueChanged
        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_ItemListView.RowCount > 0 And Val(Me.GLookUp_ItemListView.FocusedRowHandle) > 0 Then
                If Me.GLookUp_ItemList.Text.Trim.Length > 0 Then
                    Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_ID").ToString
                    BE_Item_Head.Text = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_NAME").ToString
                    iVoucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                    iLed_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LED_ID").ToString
                    iLed_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_TYPE").ToString
                    iCon_Led_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "CON_LED_TYPE").ToString
                    iTrans_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TRANS_TYPE").ToString
                    iParty_Req = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PARTY_REQ").ToString
                    iProfile = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PROFILE").ToString
                    iCond_Ledger_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_LED_ID").ToString
                    iMinValue = Val(Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_MIN_VALUE").ToString)
                    iMaxValue = Val(Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_CON_MAX_VALUE").ToString)

                    If iProfile = "VEHICLES" Or iProfile = "LIVESTOCK" Or iProfile = "LAND & BUILDING" Then
                        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Text = 1
                        Me.Cmd_UOM.Enabled = False : Me.Cmd_UOM.Text = "NO"
                        Me.Txt_Rate.Enabled = False
                    ElseIf iProfile = "ADVANCES" Or iProfile = "OTHER DEPOSITS" Or iProfile = "OTHER LIABILITIES" Or iProfile = "TELEPHONE BILL" Then
                        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Text = ""
                        Me.Cmd_UOM.Enabled = False : Me.Cmd_UOM.Text = "NO"
                        Me.Txt_Rate.Enabled = False
                    ElseIf iProfile = "GOLD" Or iProfile = "SILVER" Or iProfile = "OTHER ASSETS" Then
                        Me.Txt_Qty.Enabled = True ': Me.Txt_Qty.Text = "" '#6312 fix
                        Me.Cmd_UOM.Enabled = True : Me.Cmd_UOM.Text = "NO"
                        Me.Txt_Rate.Enabled = True
                    Else ' OTHER
                        Me.Txt_Qty.Enabled = True : Me.Txt_Qty.Text = ""
                        Me.Cmd_UOM.Enabled = True
                        Me.Txt_Rate.Enabled = True
                    End If
                Else
                    Me.GLookUp_ItemList.Tag = Nothing : Me.GLookUp_ItemList.Text = ""
                End If
            Else
                Me.GLookUp_ItemList.Tag = Nothing
                ' Me.GLookUp_ItemList.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetItemLedger()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)

        dview.Sort = "ITEM_NAME"
        If dview.Count > 0 Then
            Dim ROW As DataRow : ROW = d1.NewRow() : ROW("ITEM_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_ItemList.Properties.ValueMember = "ITEM_ID"
            Me.GLookUp_ItemList.Properties.DisplayMember = "ITEM_NAME"
            Me.GLookUp_ItemList.Properties.DataSource = dview
            Me.GLookUp_ItemListView.RefreshData()
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup() : Me.GLookUp_ItemList.EditValue = dview.Item(0).Row("ITEM_ID").ToString : Me.GLookUp_ItemList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.Properties.ReadOnly = False
        End If
    End Sub


    '2.GLookUp_PurList
    Private Sub GLookUp_PurList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PurList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PurList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PurList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PurList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PurList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PurList.CancelPopup()
            Hide_Properties()
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
        End If

    End Sub
    Private Sub GLookUp_PurList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PurList.EditValueChanged
        If Me.GLookUp_PurList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_PurListView.RowCount > 0 And Val(Me.GLookUp_PurListView.FocusedRowHandle) > 0 Then
                If Me.GLookUp_PurList.Text.Trim.Length > 0 Then
                    Me.GLookUp_PurList.Tag = Me.GLookUp_PurListView.GetRowCellValue(Me.GLookUp_PurListView.FocusedRowHandle, "PUR_ID").ToString
                Else
                    Me.GLookUp_PurList.Tag = Nothing : Me.GLookUp_PurListView.FocusedRowHandle = 0
                End If
            Else
                Me.GLookUp_PurList.Tag = Nothing : Me.GLookUp_PurList.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetPurposeList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetProjects("PUR_NAME", "PUR_ID")
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)

        If dview.Count > 0 Then
            Dim ROW As DataRow : ROW = d1.NewRow() : ROW("PUR_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_PurList.Properties.ValueMember = "PUR_ID"
            Me.GLookUp_PurList.Properties.DisplayMember = "PUR_NAME"
            Me.GLookUp_PurList.Properties.DataSource = dview
            Me.GLookUp_PurListView.RefreshData()
            Me.GLookUp_PurList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PurList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            ''1
            'Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
            'Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
            'Me.GLookUp_PurList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            'Me.GLookUp_PurList.Properties.Tag = "SHOW"
            Me.GLookUp_PurList.Properties.ReadOnly = False
        End If

    End Sub
#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class