Imports System.Globalization

Public Class FD_Report
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public Start_Date As Date
    Public End_Date As Date

    Private Sub XtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint


        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g   F i x e d   D e p o s i t   R e p o r t ")

        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name : Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Cen_Name.Text = MainBase._open_Cen_Name & " (" & MainBase._open_UID_No & ")" & "(" & MainBase._open_PAD_No & ")" : Me.Xr_Cen_Name1.Text = Me.Xr_Cen_Name.Text
        Me.Xr_As_On.Text = "Fixed Deposit Summary for the period from " & Start_Date & " to " & End_Date
        Me.Xr_Header1.Text = "Break up of Fixed Deposits existing as on " & End_Date
        Me.Xr_Header2.Text = "Break up of Fixed Deposits opened or renewed during the period from " & Start_Date & " to " & End_Date
        Me.Xr_Header3.Text = "Break up of Fixed Deposits closed during the period from " & Start_Date & " to " & End_Date
        Me.Xr_Header4.Text = "Break up of Fixed Deposits existing as on " & Start_Date
        Me.XrLabel70.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        ' Me.XrLabel63.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")")
        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""

        'Dim SupportInfo As DataTable = MainBase._CoreDBOps.GetCenSupportInfo()
        'If SupportInfo Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If
        'If SupportInfo.Rows.Count > 0 Then
        '    Xr_Res_Person.Text = SupportInfo.Rows(0)("RES_PERSON")
        'End If


        'Xr_Res_Person
        'Break up of Fixed Deposits existing as on 31-Aug -11
        'Dim FD As DataTable
        Dim param As Common_Lib.RealTimeService.Param_GetFDReport = New Common_Lib.RealTimeService.Param_GetFDReport
        'param.InsttID = MainBase._open_Ins_ID
        param.YearID = MainBase._open_Year_ID
        param.FromDate = MainBase._open_Year_Sdt
        param.ToDate = End_Date
        param.RequiredList = Common_Lib.RealTimeService.FdListType.Existing_As_On

        Dim FD_DS As DataSet = MainBase._Reports_Ledgers_DBOps.GetFDData(param)
        If FD_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If FD_DS.Tables(0).Rows.Count > 0 Then
            FD_Data1.Tables("FD_MAIN").ImportRow(FD_DS.Tables(0).Rows(0))
        End If

        Dim dblFDAmount As Double = 0.0
        For Each XRow In FD_DS.Tables(0).Rows
            FD_Data1.Tables("FD_EXISTS_END").ImportRow(XRow)
            dblFDAmount += XRow("FD_AMOUNT")
        Next

        Me.Xr_Closing_Amt.Text = dblFDAmount.ToString("0,0.00", CultureInfo.InvariantCulture)

        'FD = result of the query which return Fixed Deposits opened or renewed during the period from START_DATE to END_DATE
        param = New Common_Lib.RealTimeService.Param_GetFDReport
        'param.InsttID = MainBase._open_Ins_ID
        param.YearID = MainBase._open_Year_ID
        param.FromDate = Start_Date
        param.ToDate = End_Date
        param.RequiredList = Common_Lib.RealTimeService.FdListType.Added_Renewed_in_Period

        FD_DS = MainBase._Reports_Ledgers_DBOps.GetFDData(param)
        If FD_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If FD_Data1.Tables("FD_MAIN").Rows.Count = 0 And FD_DS.Tables(0).Rows.Count > 0 Then
            FD_Data1.Tables("FD_MAIN").ImportRow(FD_DS.Tables(0).Rows(0))
        End If

        dblFDAmount = 0.0
        For Each XRow In FD_DS.Tables(0).Rows
            FD_Data1.Tables("FD_ADDED").ImportRow(XRow)
            dblFDAmount += XRow("FD_AMOUNT")
        Next
        Me.Xr_Added_Amt.Text = dblFDAmount.ToString("0,0.00", CultureInfo.InvariantCulture)

        'FD = result of the query which return  Fixed Deposits closed during the period from START_DATE to END_DATE
        param = New Common_Lib.RealTimeService.Param_GetFDReport
        'param.InsttID = MainBase._open_Ins_ID
        param.YearID = MainBase._open_Year_ID
        param.FromDate = Start_Date
        param.ToDate = End_Date
        param.RequiredList = Common_Lib.RealTimeService.FdListType.Closed_in_Period

        FD_DS = MainBase._Reports_Ledgers_DBOps.GetFDData(param)
        If FD_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If FD_Data1.Tables("FD_MAIN").Rows.Count = 0 And FD_DS.Tables(0).Rows.Count > 0 Then
            FD_Data1.Tables("FD_MAIN").ImportRow(FD_DS.Tables(0).Rows(0))
        End If

        dblFDAmount = 0.0
        For Each XRow In FD_DS.Tables(0).Rows
            FD_Data1.Tables("FD_CLOSED").ImportRow(XRow)
            dblFDAmount += XRow("FD_AMOUNT")
        Next
        Me.Xr_Deduct_Amt.Text = dblFDAmount.ToString("0,0.00", CultureInfo.InvariantCulture)

        'FD = result of the query which return  Fixed Deposits existing as on  START_DATE 
        param = New Common_Lib.RealTimeService.Param_GetFDReport
        'param.InsttID = MainBase._open_Ins_ID
        param.YearID = MainBase._open_Year_ID
        param.FromDate = MainBase._open_Year_Sdt
        param.ToDate = Start_Date
        param.RequiredList = Common_Lib.RealTimeService.FdListType.Existing_As_On

        FD_DS = MainBase._Reports_Ledgers_DBOps.GetFDData(param)
        If FD_DS Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If FD_Data1.Tables("FD_MAIN").Rows.Count = 0 And FD_DS.Tables(0).Rows.Count > 0 Then
            FD_Data1.Tables("FD_MAIN").ImportRow(FD_DS.Tables(0).Rows(0))
        End If

        dblFDAmount = 0.0
        For Each XRow In FD_DS.Tables(0).Rows
            FD_Data1.Tables("FD_EXISTS_START").ImportRow(XRow)
            dblFDAmount += XRow("FD_AMOUNT")
        Next
        Me.Xr_Opening_Amt.Text = dblFDAmount.ToString("0,0.00", CultureInfo.InvariantCulture)

        'xPleaseWait.Hide()
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub



End Class