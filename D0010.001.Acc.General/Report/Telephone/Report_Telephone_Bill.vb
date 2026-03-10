Public Class Report_Telephone_Bill

    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public xFr_Date As DateTime = Nothing : Public xTo_Date As DateTime = Nothing
    Public xTP_ID As String
    Private SrNo As Integer = 0
    Public Sub New()
    End Sub

    Public Sub New(ByVal fromDate As Date, ByVal toDate As Date, _Title As String, Telephone_ID As String, _base As Common_Lib.Common)
        ' This call is required by the designer.
        'Programming_Testing()
        xFr_Date = fromDate
        xTo_Date = toDate
        xTP_ID = Telephone_ID.Replace("'", "")
        Title = _Title
        MainBase = _base
        InitializeComponent()
    End Sub
    Private Sub Report_Telephone_Bill_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        ' xPleaseWait.Show("G e n e r a t i n g   S t a t e m e n t")
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name
        Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Period.Text = "Period: Fr. " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        Me.XrLabel70.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        Me.Xr_UID.Text = "No.: " & MainBase._open_PAD_No & ", UID:" & MainBase._open_UID_No
        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")").ToString()
        AddHandler Me.PrintingSystem.StartPrint, AddressOf ReportOnStartPrint

        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If Centre_Inc.Rows.Count > 0 Then
            If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE").ToString() Else Xr_Incharge.Text = ""
            If Not IsDBNull(Centre_Inc.Rows(0)("CEN_NAME")) Then Me.Xr_Cen_Name.Text = "Centre: " & MainBase._open_Cen_Name Else Me.Xr_Cen_Name.Text = "Centre: "
            If Not IsDBNull(Centre_Inc.Rows(0)("CEN_ZONE_ID")) Then Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID Else Me.Xr_Zone_Name.Text = "Zone: "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        Dim Telephone_data As DataTable = MainBase._Reports_Common_DBOps.GetTelephoneBillDetails(Common_Lib.RealTimeService.ClientScreen.Profile_Report, xTP_ID, xFr_Date, xTo_Date)
        If Telephone_data Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        For Each XRow As DataRow In Telephone_data.Rows
            Report_Telephone_Data1.Tables("Transaction_Info").ImportRow(XRow)
        Next
        SrNo = 1
        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = Convert.ToInt16(No_of_Copies)
    End Sub

    Private Sub GroupHeader1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        SrNo = 1
    End Sub

    Private Sub XrTableCell14_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrTableCell14.BeforePrint
        TryCast(sender, DevExpress.XtraReports.UI.XRTableCell).Text = (SrNo).ToString()
        SrNo += 1
    End Sub

End Class