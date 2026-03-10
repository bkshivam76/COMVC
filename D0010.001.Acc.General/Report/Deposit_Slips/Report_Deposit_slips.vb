Public Class Report_Deposit_slips

    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public SlipID As String
    Private Sub Report_DailyBalances_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g   D e p o s i t   S l i p")
        Dim _dataset As DataSet = Base._DepositSlipsDBOps.GetSlipReport(SlipID)

        XrBank_AccNo.Text = _dataset.Tables(0).Rows(0)("Bank_AccNo").ToString
        XrHeading.Text = _dataset.Tables(0).Rows(0)("Bank_Branch").ToString
        XrSlipInfo.Text = _dataset.Tables(0).Rows(0)("SlipInfo").ToString
        XrIns_Header.Text = _dataset.Tables(0).Rows(0)("Ins_Header").ToString

        Dim Total As Decimal = 0.0
        For Each XRow In _dataset.Tables(1).Rows
            Report_Deposit_slips_Data1.Tables(0).ImportRow(XRow)
            Total += XRow("Amount")
        Next
        XrAmountInWords.Text = "In words : " & StrConv(Base.ConvertNumToAlphaValue(Total), VbStrConv.ProperCase)
        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub
End Class