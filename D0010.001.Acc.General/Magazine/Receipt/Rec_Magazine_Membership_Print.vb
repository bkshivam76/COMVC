Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode
Imports System.Windows.Forms
Public Class Rec_Magazine_Membership_Print
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public Master_ID As String
    Public Sub New(_MainBase As Common_Lib.Common, _masterID As String)

        ' This call is required by the designer.
        InitializeComponent()
        MainBase = _MainBase
        Master_ID = _masterID
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Rec_Membership_Print_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g   R e c e i p t")
        ' Me.Xr_Ins_Name.Text = "For " & MainBase._open_Ins_Name

        Dim R1 As DataTable = MainBase._Reports_Common_DBOps.GetMagazineReceipt(Common_Lib.RealTimeService.ClientScreen.Magazine_Receipt_Register, Master_ID)
        If R1 Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        'Dim R2 As DataTable = MainBase._Reports_Common_DBOps.GetMembershipReceiptPayment(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership, Master_ID)
        'Dim R3 As DataTable = MainBase._Reports_Common_DBOps.GetMembershipSubscriptionFee(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership, Master_ID)
        'If R1 Is Nothing Or R2 Is Nothing Or R3 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        AddHandler Me.PrintingSystem.StartPrint, AddressOf ReportOnStartPrint

        'If R1.Rows.Count > 0 Then
        '    Dim _Str As String = ""
        '    'Cash Payment
        '    If Val(R1.Rows(0)("TR_CASH_AMT").ToString()) > 0 Then
        '        _Str = "By CASH: Rs." & Format(Val(R1.Rows(0)("TR_CASH_AMT").ToString()), "#,0.00")
        '    End If
        '    'Bank Payment
        '    Dim _Mode As String = ""
        '    If R2.Rows.Count > 0 Then
        '        _Mode = R2.Rows(0)("TR_MODE") : If _Str.Length > 0 Then _Str = _Str & ";  By " & _Mode & ": " Else _Str = "By " & _Mode & ": "
        '        For Each XRow In R2.Rows
        '            If _Mode = XRow("TR_MODE") Then
        '                _Str = _Str & " (" & XRow("BI_SHORT_NAME") & ") No." & XRow("TR_REF_NO") & ", Dt." & XRow("TR_REF_DATE") & " for Rs." & Format(Val(XRow("TR_REF_AMT")), "#,0.00") & "; "
        '            Else
        '                If _Str.Trim.Length > 0 Then _Str = IIf(_Str.Trim.EndsWith(";"), Mid(_Str.Trim.ToString, 1, _Str.Trim.Length - 1), _Str.Trim.ToString)
        '                _Mode = XRow("TR_MODE") : If _Str.Length > 0 Then _Str = _Str & ";  By " & _Mode & ": " Else _Str = "By " & _Mode & ": "
        '                _Str = _Str & " (" & XRow("BI_SHORT_NAME") & ") No." & XRow("TR_REF_NO") & ", Dt." & XRow("TR_REF_DATE") & " for Rs." & Format(Val(XRow("TR_REF_AMT")), "#,0.00") & "; "
        '            End If
        '        Next
        '    End If
        '    If _Str.Trim.Length > 0 Then _Str = IIf(_Str.Trim.EndsWith(";"), Mid(_Str.Trim.ToString, 1, _Str.Trim.Length - 1), _Str.Trim.ToString)
        '    R1.Rows(0)("MS_PAYMENT") = _Str
        '    Me.Xr_TotalAmt.Text = Format(Val(R1.Rows(0)("TR_SUB_AMT").ToString()), "#,0.00")
        '    R1.Rows(0)("MS_INWORDS") = "Rupees (in words): " & StrConv(MainBase.ConvertNumToAlphaValue(Val(R1.Rows(0)("TR_SUB_AMT").ToString())), VbStrConv.ProperCase) ' & " Only."
        '    If Not R1.Rows(0)("MS_WING_NAME") Is DBNull.Value Then R1.Rows(0)("MS_WING_NAME") = R1.Rows(0)("MS_WING_NAME")
        '    R1.Rows(0)("MS_TYPE") = StrConv(R1.Rows(0)("MS_TYPE"), VbStrConv.ProperCase)
        'End If
        'If R3.Rows.Count > 0 Then
        '    Me.Xr_Copies.Text = Format(Val(R3.Rows(0)("ENTRANCE")), "#,0.00")
        '    Me.Xr_Cash.Text = Format(Val(R3.Rows(0)("ARREARS")), "#,0.00")
        '    Me.Xr_Bank.Text = Format(Val(R3.Rows(0)("CURRENT")), "#,0.00")
        '    Me.Xr_AdvanceAmt.Text = Format(Val(R3.Rows(0)("ADVANCE")), "#,0.00")
        '    Dim d6 As DataTable = MainBase._Membership_Conversion_Voucher_DBOps.GetAdjustmentPaymentRecord(Master_ID)
        '    If d6.Rows.Count > 0 Then
        '        Me.Xr_Bank.Text = Val(R3.Rows(0)("CURRENT")) - Val(d6.Rows(0)("TR_REF_AMT"))
        '    End If
        'End If
        R1.Rows(0)("MS_INWORDS") = "Rupees (in words): " & StrConv(MainBase.ConvertNumToAlphaValue(Val(R1.Rows(0)("TOT_AMOUNT").ToString())), VbStrConv.ProperCase) ' & " Only."
        For Each XRow In R1.Rows
            Receipt_Data1.Tables("MAINTABLE").ImportRow(XRow)
        Next

        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub

End Class