Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Voucher_Win_Journal_Reference

#Region "Start--> Default Variables"
    Public iItemProfile As String
    Public SelectedRefID As String
    Public SelectedItemID As String
    Public ReferenceData As DataTable
    Public iTxnM_ID As String
    Public Ref_Rec_ID As String
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
        Me.GLookUp_ReferenceList.Focus()
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

    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click
        Hide_Properties()
        If Trim(Me.GLookUp_ReferenceList.Text).Length <= 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("N o   R e f e r e n c e   S e l e c t e d. . . !", Me.GLookUp_ReferenceList, 0, Me.GLookUp_ReferenceList.Height, 5000)
            Me.GLookUp_ReferenceList.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.GLookUp_ReferenceList)
        End If

        'If Trim(Me.Txt_Qty.Text).Length <= 0 And (iItemProfile = "OTHER ASSETS" Or iItemProfile = "GOLD" Or iItemProfile = "SILVER") Then
        '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
        '    Me.ToolTip1.Show("N o   Q u a n t i t y   E n t e r e d. . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
        '    Me.Txt_Qty.Focus()
        '    Me.DialogResult = Windows.Forms.DialogResult.None
        '    Exit Sub
        'Else
        '    Me.ToolTip1.Hide(Me.Txt_Qty)
        'End If

        'Dim param As Common_Lib.RealTimeService.Parameter_GetCurrentRecAdjustment = New Common_Lib.RealTimeService.Parameter_GetCurrentRecAdjustment
        'param.CrossRefId = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "REC_ID").ToString
        'param.Rec_M_ID = iTxnM_ID
        'Dim CurrentRecAdjustments As DataTable = Base._Journal_voucher_DBOps.GetGetCurrentRecAdjustment(param)
        'Dim AdjDebit As Double = 0 : Dim AdjCredit As Double = 0 : Dim AdjDebitQty As Double = 0 : Dim AdjCreditQty As Double = 0
        'For Each cRow As DataRow In CurrentRecAdjustments.Rows
        '    If cRow("TR_TYPE") = "DEBIT" Then
        '        AdjDebit = cRow("AMOUNT") : AdjDebitQty = cRow("QTY")
        '    Else
        '        AdjCredit = cRow("AMOUNT") : AdjCreditQty = cRow("QTY")
        '    End If
        'Next

        If (iItemProfile.ToUpper <> "OTHER LIABILITIES") And (iItemProfile.ToUpper <> "OPENING") Then
            If Not SelectedRefID Is Nothing Then
                If Me.GLookUp_ReferenceListView.LocateByValue("REC_ID", SelectedRefID) <> Me.GLookUp_ReferenceListView.FocusedRowHandle Then
                    If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.LocateByValue("REC_ID", SelectedRefID), "Curr Value").ToString)) < 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("V a l u e  o f   p r e v i o u s l y   r e f e r e n c e d   a s s e t   b e c o m e s   n e g a t i v e   i n   C u r r e n t   Y e a r . . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                        Me.Txt_Amt.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Amt)
                    End If

                    If iItemProfile.ToUpper.Equals("WIP") Or iItemProfile.ToUpper.Equals("ADVANCES") Or iItemProfile.ToUpper.Equals("OTHER DEPOSITS") Or iItemProfile.ToUpper.Equals("OTHER LIABILITIES") Then
                        If Base._next_Unaudited_YearID <> Nothing Then
                            If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.LocateByValue("REC_ID", SelectedRefID), "Next Year Closing Value").ToString)) < 0 Then
                                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                Me.ToolTip1.Show("V a l u e  o f   p r e v i o u s l y   r e f e r e n c e d   a s s e t   b e c o m e s   n e g a t i v e   i n   N e x t   Y e a r . . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                                Me.Txt_Amt.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            Else
                                Me.ToolTip1.Hide(Me.Txt_Amt)
                            End If
                        End If
                    End If
                End If
            End If
          
            If Txt_Action.Text.ToUpper = "CREDIT" Then
                If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString)) < (Val(Txt_Amt.Text)) Then ' "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                    Me.Txt_Amt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amt)
                End If

                If iItemProfile.ToUpper.Equals("WIP") Or iItemProfile.ToUpper.Equals("ADVANCES") Or iItemProfile.ToUpper.Equals("OTHER DEPOSITS") Or iItemProfile.ToUpper.Equals("OTHER LIABILITIES") Then
                    If Base._next_Unaudited_YearID <> Nothing Then
                        If Not Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value") Is Nothing And Not IsDBNull(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value")) Then
                            If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value").ToString)) < (Val(Txt_Amt.Text)) Then ' "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e   i n   n e x t    y e a r. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                                Me.Txt_Amt.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            Else
                                Me.ToolTip1.Hide(Me.Txt_Amt)
                            End If
                        End If
                    End If
                End If
            Else
                If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString)) + (Val(Txt_Amt.Text)) < 0 Then ' "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                    Me.Txt_Amt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amt)
                End If

                If Base._next_Unaudited_YearID <> Nothing Then
                    If Not Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value") Is Nothing And Not IsDBNull(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value")) Then
                        If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value").ToString)) + (Val(Txt_Amt.Text)) < 0 Then ' "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                            Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e   i n   n e x t    y e a r. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                            Me.Txt_Amt.Focus()
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            Me.ToolTip1.Hide(Me.Txt_Amt)
                        End If
                    End If
                End If
            End If
        Else
            Me.ToolTip1.Hide(Me.Txt_Amt)
        End If

        If (iItemProfile.ToUpper = "OTHER LIABILITIES") And (iItemProfile.ToUpper <> "OPENING") Then
            If Txt_Action.Text.ToUpper = "DEBIT" Then
                If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString)) < Val(Txt_Amt.Text) Then '- AdjDebitQty + AdjCreditQty removed #Task 3864  09/09/12
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                    Me.Txt_Amt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amt)
                End If
                If Base._next_Unaudited_YearID <> Nothing Then
                    If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value").ToString)) < Val(Txt_Amt.Text) Then '- AdjDebitQty + AdjCreditQty removed #Task 3864  09/09/12
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e   i n   n e x t    y e a r. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                        Me.Txt_Amt.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Amt)
                    End If
                End If
            Else
                If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString)) + Val(Txt_Amt.Text) < 0 Then '- AdjDebitQty + AdjCreditQty removed #Task 3864  09/09/12
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                    Me.Txt_Amt.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Amt)
                End If
                If Base._next_Unaudited_YearID <> Nothing Then
                    If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value").ToString)) + Val(Txt_Amt.Text) < 0 Then '- AdjDebitQty + AdjCreditQty removed #Task 3864  09/09/12
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("V a l u e   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   v a l u e   i n   n e x t    y e a r. . . !", Me.Txt_Amt, 0, Me.Txt_Amt.Height, 5000)
                        Me.Txt_Amt.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_Amt)
                    End If
                End If
            End If
        End If

        If Txt_Action.Text.ToUpper = "CREDIT" And iItemProfile = "OTHER ASSETS" Then
            If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Qty").ToString)) < Val(Txt_Qty.Text) Then '- AdjDebitQty + AdjCreditQty removed #Task 3864  09/09/12
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Q u a n t i t y   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   q u a n t i t y. . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If

            If ((Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Qty").ToString)) = Val(Txt_Qty.Text) Or Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString) = Val(Txt_Amt.Text)) _
                And ((Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Qty").ToString)) <> Val(Txt_Qty.Text) Or Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString) <> Val(Txt_Amt.Text)) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Q u a n t i t y   &   A m o u n t   B o t h   N e e d   t o   b e c o m e   Z e r o   S i m u l t a n e o u s l y . . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If
        Else
            Me.ToolTip1.Hide(Me.Txt_Qty)
        End If

        If Txt_Action.Text.ToUpper = "CREDIT" And (iItemProfile = "GOLD" Or iItemProfile = "SILVER") Then
            If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Weight").ToString)) < Val(Txt_Qty.Text) Then '- AdjDebitQty + AdjCreditQty removed #Task 3864 dated 09/09/12
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("W e i g h t   r e d u c e d   i s   g r e a t e r   t h a n   e x i s t i n g   q u a n t i t y. . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If

            If ((Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Weight").ToString)) = Val(Txt_Qty.Text) Or Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString) = Val(Txt_Amt.Text)) _
              And ((Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Weight").ToString)) <> Val(Txt_Qty.Text) Or Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString) <> Val(Txt_Amt.Text)) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("W e i g h t   &   A m o u n t   B o t h   N e e d   t o   b e c o m e   Z e r o   S i m u l t a n e o u s l y . . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If
        Else
            Me.ToolTip1.Hide(Me.Txt_Qty)
        End If

        If Txt_Action.Text.ToUpper = "CREDIT" And (iItemProfile = "WIP") Then
            'Dim xFrm1 As New Frm_Existing_References
            ''xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
            ''xfrm.Txn_M_ID = Me.iTxnM_ID
            'xFrm1.Led_ID = iLed_ID
            'xFrm1.Ref_Rec_ID = Ref_RecID
            'xFrm1.Txn_M_ID = iTxnM_ID
            'xFrm1.Tag = Me.Tag
            'xFrm1.ShowDialog(Me)
            ' If xFrm1.DialogResult = Windows.Forms.DialogResult.OK Then
            If (Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString)) + Val(Txt_Amt.Text) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Current Year for WIP( " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reference").ToString & " ) with Original Value " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Org Value").ToString, Me.GLookUp_ReferenceList, 0, Me.GLookUp_ReferenceList.Height, 5000)
                Me.GLookUp_ReferenceList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Base._next_Unaudited_YearID <> Nothing Then
                If Val(Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Next Year Closing Value").ToString) + Val(Txt_Amt.Text) < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Next Year for WIP( " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reference").ToString & " ) with Original Value " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Org Value").ToString, Me.GLookUp_ReferenceList, 0, Me.GLookUp_ReferenceList.Height, 5000)
                    Me.GLookUp_ReferenceList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            End If

            '' If iRefType = "EXISTING" Then
            'If Not Ref_Rec_ID Is Nothing Then
            '    If Ref_Rec_ID.Length > 0 And xFrm1.Ref_Rec_ID <> Ref_Rec_ID Then
            '        Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
            '        Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", iSpecific_ItemID, Nothing, iTxnM_ID, Common_Lib.Common.Navigation_Mode._Edit, Ref_RecID)
            '        If Base._next_Unaudited_YearID.Length > 0 Then
            '            If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
            '                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Next Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                Exit Sub
            '            End If
            '        End If

            '        If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
            '            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Changes in Selected Payment Entry creates a Negative Closing Balance in Current Year for WIP with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    End If
            'End If
            'End If
            ' Else
            'Me.ToolTip1.Hide(Me.Txt_Qty)
            ' End If
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
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amt.GotFocus, Txt_Amt.Click, Txt_Qty.GotFocus, Txt_Qty.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amt.Name Or txt.Name = Txt_Qty.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amt.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amt.KeyDown, Txt_Qty.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs)
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
        Me.TitleX.Text = "Reference Item Details"
        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Then Me.TitleX.Text = "New ~ " & Me.TitleX.Text Else Me.TitleX.Text = "Edit ~ " & Me.TitleX.Text
        GLookUp_ReferenceList.Tag = "" : LookUp_GetReferenceList()
        ReferenceBind()

        If iItemProfile <> "OTHER ASSETS" And iItemProfile <> "GOLD" And iItemProfile <> "SILVER" Then
            'Txt_Qty.Text = ""
            LayoutControlItem16.Control.Enabled = False
        End If


        If iItemProfile = "GOLD" Or iItemProfile = "SILVER" Then
            LayoutControlItem16.Text = "Weight(gm):"
            Me.Txt_Qty.Properties.DisplayFormat.FormatString = "f3"
            Me.Txt_Qty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.Txt_Qty.Properties.EditFormat.FormatString = "f3"
            Me.Txt_Qty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.Txt_Qty.Properties.Mask.EditMask = "f3"
        Else
            LayoutControlItem16.Text = "Quantity:"
            Me.Txt_Qty.Properties.DisplayFormat.FormatString = "d"
            Me.Txt_Qty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.Txt_Qty.Properties.EditFormat.FormatString = "d"
            Me.Txt_Qty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.Txt_Qty.Properties.Mask.EditMask = "d"
        End If
       
        Me.GLookUp_ReferenceList.Focus()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            BUT_SAVE_COM.Enabled = False
            BUT_CANCEL.Text = "Close"
        End If
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_Item.Enabled = False : Me.Txt_Item.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amt.Enabled = False : Me.Txt_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ReferenceList.Enabled = False : Me.GLookUp_ReferenceList.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Hide_Properties()
        ' Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.Txt_Amt)
    End Sub
    Private Sub ReferenceBind()
        Me.GLookUp_ReferenceList.Properties.ReadOnly = False
        If Me.SelectedRefID Is Nothing Then Exit Sub
        If Me.SelectedRefID.Length > 0 Then
            Me.GLookUp_ReferenceList.ShowPopup() : Me.GLookUp_ReferenceList.ClosePopup()
            Me.GLookUp_ReferenceListView.FocusedRowHandle = Me.GLookUp_ReferenceListView.LocateByValue("REC_ID", SelectedRefID)
            Me.GLookUp_ReferenceList.EditValue = SelectedRefID
            Me.GLookUp_ReferenceList.Properties.Tag = "SHOW"
            'Me.GLookUp_ReferenceList.Properties.ReadOnly = True
            Me.GLookUp_ReferenceList.Tag = SelectedRefID
        End If
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then Me.GLookUp_PartyList.Properties.ReadOnly = True
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_PartyList1
    Private Sub GLookUp_ReferenceList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_ReferenceList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_ReferenceListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_ReferenceListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_ReferenceList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_ReferenceListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_ReferenceListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_ReferenceList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_ReferenceList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_ReferenceList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_ReferenceList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_ReferenceList.CancelPopup()
            Hide_Properties()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_ReferenceList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
        End If
    End Sub
    Private Sub GLookUp_ReferenceList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ReferenceList.EditValueChanged
        If Me.GLookUp_ReferenceList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_ReferenceListView.RowCount > 0 And Val(Me.GLookUp_ReferenceListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_ReferenceList.Tag = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "REC_ID").ToString
                Select Case iItemProfile
                    Case "OTHER ASSETS"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Make").ToString & ", " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Model").ToString & ", Curr Qty:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Qty").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "ADVANCES"
                        Me.Txt_Description.Text = "Given Date:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Date").ToString & ", Detail:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reason").ToString & " , " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Detail").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "GOLD", "SILVER"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "DESC").ToString & ", " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "DETAILS").ToString & ", Curr Weight:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Weight").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "LAND & BUILDING"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Type").ToString & ", " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Category").ToString & "(" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Use").ToString & ")" & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "LIVESTOCK"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Item").ToString & ", Birth Year : " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "BIRTH YEAR").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "OTHER DEPOSITS"
                        Me.Txt_Description.Text = "Given Date:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Date").ToString & ", Due Date :" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Due").ToString & ", Period:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Period").ToString & ", Detail:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reason").ToString & " , " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Detail").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "OTHER LIABILITIES"
                        Me.Txt_Description.Text = "Given Date:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Date").ToString & ", Due Date :" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Due").ToString & ", Detail:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reason").ToString & " , " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Detail").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "VEHICLES"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Make").ToString & ", " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Model").ToString & ", Reg.No.:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reg No").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "WIP"
                        Me.Txt_Description.Text = Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Ledger").ToString & ", " & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Reference").ToString & ", Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                    Case "Opening"
                        Me.Txt_Description.Text = "Curr Value:" & Me.GLookUp_ReferenceListView.GetRowCellValue(Me.GLookUp_ReferenceListView.FocusedRowHandle, "Curr Value").ToString
                End Select
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetReferenceList()
        Dim ctr As Int32 = 0
        If ReferenceData Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        For Each col As DataColumn In ReferenceData.Columns
            Dim cCol As DevExpress.XtraGrid.Columns.GridColumn = New DevExpress.XtraGrid.Columns.GridColumn
            cCol.FieldName = col.ColumnName
            cCol.Name = col.ColumnName
            cCol.Visible = True
            cCol.Caption = col.ColumnName
            cCol.VisibleIndex = ctr
            GLookUp_ReferenceListView.Columns.Add(cCol)
            ctr += 1
        Next
      
        Select Case iItemProfile
            Case "OTHER ASSETS"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Item"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "ADVANCES"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Org Value"
                Me.GLookUp_ReferenceListView.Columns("Reason").Visible = False
                ' Me.GLookUp_ReferenceListView.Columns("Detail").Visible = False
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Next Year Closing Value").Visible = False
            Case "GOLD", "SILVER"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Item"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "LAND & BUILDING"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Item"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "LIVESTOCK"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Name"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "OTHER DEPOSITS"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Org Value"
                Me.GLookUp_ReferenceListView.Columns("Reason").Visible = False
                ' Me.GLookUp_ReferenceListView.Columns("Detail").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Due").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Period").Visible = False
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Next Year Closing Value").Visible = False
            Case "OTHER LIABILITIES"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Org Value"
                Me.GLookUp_ReferenceListView.Columns("Reason").Visible = False
                ' Me.GLookUp_ReferenceListView.Columns("Detail").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Due").Visible = False
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
                Me.GLookUp_ReferenceListView.Columns("Next Year Closing Value").Visible = False
            Case "VEHICLES"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Vehicle"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "OPENING"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Item"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case "WIP"
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Reference"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
            Case Else 'Constt Items
                Me.GLookUp_ReferenceList.Properties.DisplayMember = "Item"
                Me.GLookUp_ReferenceListView.Columns("REF_CREATION_DATE").Visible = False
        End Select
        ' Dim ROW As DataRow : ROW = d1.NewRow() : ROW("Name") = "" : d1.Rows.Add(ROW)
        Dim dview As New DataView(ReferenceData) ': dview.Sort = "Name"
        If dview.Count > 0 Then
            Me.GLookUp_ReferenceList.Properties.ValueMember = "REC_ID"
            'Me.GLookUp_ReferenceList.Properties.DisplayMember = "Name"
            Me.GLookUp_ReferenceList.Properties.DataSource = dview
            Me.GLookUp_ReferenceListView.RefreshData()
            Me.GLookUp_ReferenceList.Properties.Tag = "SHOW"
            Me.GLookUp_ReferenceListView.Columns("REC_ID").Visible = False
            Me.GLookUp_ReferenceListView.Columns("REC_EDIT_ON").Visible = False
        Else
            Me.GLookUp_ReferenceList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ReferenceList.Properties.ReadOnly = False
    End Sub

    Private Sub GLookUp_ReferenceList_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GLookUp_ReferenceList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub

    Private Sub FilterLookup(sender As Object)
        ' Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Select iItemProfile
            Case "OTHER ASSETS"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Make", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Model", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "ADVANCES"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Detail", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
            Case "GOLD", "SILVER"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("DESC", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("DETAILS", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "LAND & BUILDING"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Category", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("OWNER", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Type", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op5 As New BinaryOperator("Use", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4, op5}).ToString()
            Case "LIVESTOCK"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Name", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("BIRTH YEAR", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "OTHER DEPOSITS"
                  Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Detail", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
            Case "OTHER LIABILITIES"
                Dim op1 As New BinaryOperator("Due", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Detail", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
            Case "VEHICLES"
                Dim op1 As New BinaryOperator("Vehicle", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Make", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Model", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Reg No", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
            Case "OPENING"
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Head Type", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Curr Value", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "WIP"
                Dim op1 As New BinaryOperator("Ledger", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Reference", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2}).ToString()
            Case Else 'Constt Items
                Dim op1 As New BinaryOperator("Item", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Category", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("OWNER", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Type", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op5 As New BinaryOperator("Use", edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4, op5}).ToString()
        End Select
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
#End Region


End Class