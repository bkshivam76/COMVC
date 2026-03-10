Public Class Asset_Report
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public OnDate As Date
    Private Sub XtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If MainBase._open_User_Type.ToUpper <> Common_Lib.Common.ClientUserType.SuperUser.ToUpper And MainBase._open_User_Type.ToUpper <> Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
            'Opening
            XrTableCell20.Visible = False : XrTotAdd.Visible = False : XrTableCell19.Visible = False
            XrTableCell17.WidthF = 180 : XrTableCell18.WidthF = 180 : XrTotOpening.WidthF = 180 : XrOpeningsLabel.WidthF = 160 : XrOpeningsLabel.LocationF = New PointF(469.69, 3.83)
            'Addition
            XrTableCell26.Visible = False : XrTotAddition.Visible = False : XrTableCell25.Visible = False
            XrTableCell23.WidthF = 153 : XrTableCell24.WidthF = 153 : XrTotAddQty.WidthF = 153 : XrAdditionsLabel.WidthF = 153 : XrAdditionsLabel.LocationF = New PointF(629.69, 3.83)
            'Deduction
            XrTableCell28.Visible = False : XrTotDeductions.Visible = False : XrTableCell31.Visible = False
            XrTableCell27.WidthF = 155 : XrTableCell30.WidthF = 155 : XrTotDedQty.WidthF = 155 : XrDeductionsLabel.WidthF = 155 : XrDeductionsLabel.LocationF = New PointF(787.69, 3.83)
            'Deduction
            XrTableCell34.Visible = False : XrTotClosingAmt.Visible = False : XrTableCell40.Visible = False
            XrTableCell29.WidthF = 150 : XrTableCell32.WidthF = 150 : XrTotCloseQty.WidthF = 150 : XrClosingLabel.WidthF = 160 : XrClosingLabel.LocationF = New PointF(942.69, 3.83)
            'Name
            XrTableCell9.WidthF = 163.4 : XrTableCell10.WidthF = 163.4
            'Location
            XrTableCell15.WidthF = 153.12 : XrTableCell16.WidthF = 153.12
            'Make
            XrTableCell11.WidthF = 70.96 : XrTableCell12.WidthF = 70.96
            'Model
            XrTableCell13.WidthF = 79.27 : XrTableCell14.WidthF = 79.27
            'Summary of Book Value
            XrBookLabel.Visible = False : XrOpenAmt.Visible = False : XrAddAmt.Visible = False : XrDedAmt.Visible = False : XrCloseAmt.Visible = False
        End If
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g   A s s e t   R e p o r t ")
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name : Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Title.Text += " lying at " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No + "(" + MainBase._open_PAD_No + ")"
        'Me.Xr_Cen_Name.Text = "Centre: " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No & "(" & MainBase._open_PAD_No & ")"
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
        Dim param As Common_Lib.RealTimeService.Param_GetAssetReport = New Common_Lib.RealTimeService.Param_GetAssetReport
        param.YearID = MainBase._open_Year_ID
        param.PrevYearID = MainBase._prev_Unaudited_YearID
        param.OnDate = OnDate
        Dim AssetDS As DataSet = MainBase._Reports_Ledgers_DBOps.GetAssetData(param)
        If AssetDS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        For Each XRow In AssetDS.Tables(0).Rows
            Asset_Data1.Tables("ASSET_LIST").ImportRow(XRow)
        Next
        For Each XRow In AssetDS.Tables(1).Rows
            Asset_Data1.Tables("ASSET_SUMMARY").ImportRow(XRow)
        Next
        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub
End Class