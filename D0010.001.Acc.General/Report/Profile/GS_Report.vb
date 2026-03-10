Public Class GS_Report
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public OnDate As Date
    Private Sub XtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g    G o l d / S i l v e r    R e p o r t ")
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name : Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Title.Text += " lying at " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No + "(" + MainBase._open_PAD_No + ")"
        ' Me.Xr_Cen_Name.Text = "Centre: " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No & "(" & MainBase._open_PAD_No & ")"
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
        Dim param As Common_Lib.RealTimeService.Param_GetGSReport = New Common_Lib.RealTimeService.Param_GetGSReport
        param.PrevYearID = MainBase._prev_Unaudited_YearID
        param.YearID = MainBase._open_Year_ID
        param.OnDate = OnDate
        param.YearStartDate = MainBase._open_Year_Sdt
        Dim GS_DS As DataSet = MainBase._Reports_Ledgers_DBOps.GetGSData(param)
        If GS_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        For Each XRow In GS_DS.Tables(0).Rows
            GS_Data1.Tables("GOLD_LISTING").ImportRow(XRow)
        Next
        For Each XRow In GS_DS.Tables(1).Rows
            GS_Data1.Tables("GOLD_SUMMARY").ImportRow(XRow)
        Next
        For Each XRow In GS_DS.Tables(2).Rows
            GS_Data1.Tables("GOLD_SUMMARY_DETAIL").ImportRow(XRow)
        Next
        For Each XRow In GS_DS.Tables(3).Rows
            GS_Data1.Tables("SILVER_LISTING").ImportRow(XRow)
        Next
        For Each XRow In GS_DS.Tables(4).Rows
            GS_Data1.Tables("SILVER_SUMMARY").ImportRow(XRow)
        Next
        For Each XRow In GS_DS.Tables(5).Rows
            GS_Data1.Tables("SILVER_SUMMARY_DETAIL").ImportRow(XRow)
        Next

        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub
End Class