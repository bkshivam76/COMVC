Public Class LB_Report
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    'Public FromDate As Date
    Public OnDate As Date
    Private Sub XtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g    P r o p e r t y    R e p o r t ")
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name : Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Title.Text += " lying at " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No & "(" & MainBase._open_PAD_No & ")"
        Me.Xr_Period.Text = "Period: Fr. " & Format(MainBase._open_Year_Sdt, "dd-MMM, yy") & "  to  " & Format(OnDate, "dd-MMM, yy")
        Me.XrLabel70.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")")
        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""
        '
        '---------------------------------------------------------------
        Dim param As Common_Lib.RealTimeService.Param_GetLBReport = New Common_Lib.RealTimeService.Param_GetLBReport
        param.PrevYearID = MainBase._prev_Unaudited_YearID
        param.YearID = MainBase._open_Year_ID
        param.OnDate = OnDate
        ' param.toDate = ToDate
        Dim LB_DS As DataSet = MainBase._Reports_Ledgers_DBOps.GetLBData(param)
        If LB_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        For Each XRow In LB_DS.Tables(0).Rows
            LB_Data.Tables("Default_Table").ImportRow(XRow)
        Next
        For Each XRow In LB_DS.Tables(1).Rows
            LB_Data.Tables("Default_Totals").ImportRow(XRow)
        Next
        For Each XRow In LB_DS.Tables(2).Rows
            LB_Data.Tables("Detail_Table").ImportRow(XRow)
        Next
        For Each XRow In LB_DS.Tables(3).Rows
            LB_Data.Tables("Detail_totals_Donation").ImportRow(XRow)
        Next
        For Each XRow In LB_DS.Tables(4).Rows
            LB_Data.Tables("Detail_totals_Self").ImportRow(XRow)
        Next

        XrTOT_GTotal.Text = Convert.ToDouble(Val(LB_DS.Tables(3).Rows(0)("TOTAL")) + Val(LB_DS.Tables(4).Rows(0)("TOTAL"))).ToString("#0.00")
        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub
End Class