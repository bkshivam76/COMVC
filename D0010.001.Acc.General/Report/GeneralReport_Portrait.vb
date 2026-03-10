Imports System.ComponentModel
Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI.PivotGrid
Imports DevExpress.XtraPivotGrid


Public Class GeneralReport_Portrait
#Region "Variables"
    Public ReportType As String
    Public MainBase As New Common_Lib.Common
#End Region

#Region "Start --> Collection Box Report"
    Public Sub InitCollectionBox(ByVal ds As DataSet)
        InitDetailBandBasedonXRTable(ds, True)

        Dim colCount As Integer = ds.Tables(0).Columns.Count

        ' Create a table to represent headers
        Dim panel As XRPanel = Me.Bands(BandKind.PageHeader).Controls(0)
        Dim tableHeader As XRTable = panel.Controls(0)
        tableHeader.Height = 40

        Dim headerRow1 As XRTableRow = New XRTableRow()
        headerRow1.Width = tableHeader.Width
        tableHeader.Rows.Insert(0, headerRow1)

        'Extra header row
        'Add another row to header to specify name of sisters who opened collection box
        Dim ParticularOrdinal As Integer = ds.Tables(0).Columns("Centre-in-charge Name").Ordinal
        Dim headerCell0 As XRTableCell = New XRTableCell()
        Dim headerCell1 As XRTableCell = New XRTableCell()
        Dim headerCell2 As XRTableCell = New XRTableCell()

        Dim lblName As New XRLabel()

        lblName.Text = "Name of Persons who have opened Collection Box"
        lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        lblName.CanGrow = False
        lblName.Multiline = True
        lblName.WordWrap = True
        lblName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        lblName.ForeColor = Color.Black
        lblName.Borders = BorderSide.None
        lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

        headerCell1.Controls.Add(lblName)
        headerCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

        headerCell1.Borders = BorderSide.Left Or BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
        headerCell0.Borders = BorderSide.Bottom
        headerCell2.Borders = BorderSide.Bottom

        headerCell1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid

        headerRow1.Cells.Add(headerCell0)
        headerRow1.Cells.Add(headerCell1)
        headerRow1.Cells.Add(headerCell2)

        'set width
        headerCell0.WidthF = tableHeader.Rows(1).Cells(1).LocationF.X
        headerCell1.TopF = tableHeader.Rows(1).Cells(1).LocationF.X - 1
        headerCell1.WidthF = tableHeader.Rows(1).Cells(1).WidthF + tableHeader.Rows(1).Cells(2).WidthF + 1
        headerCell2.WidthF = tableHeader.Rows(1).Cells(2).WidthF - 1
        lblName.WidthF = headerCell1.WidthF

        headerRow1.PerformLayout()
        tableHeader.PerformLayout()

        Dim footerMessage As New XRLabel
        footerMessage.Text = "Collection box was opened in the presence of two persons "
        ' footerMessage.Padding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 1)
        footerMessage.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular)
        'footerMessage.WidthF = tableHeader.Width
        'footerMessage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight

        'Me.Bands(BandKind.ReportFooter).Controls.Add(footerMessage)
        'XrPanelInfo.Controls.Add(footerMessage)
        XrLblCollectionBoxInfo.Visible = True
        'footerMessage.AutoWidth = True
        'footerMessage.WidthF = tableHeader.WidthF
        'footerMessage.TopF = 199
        'footerMessage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.Bands(BandKind.Detail).FormattingRules.Add(Me.FinalRow)
        Me.PerformLayout()
        Me.CreateDocument()

    End Sub

    Public Sub InitTransactionsSummary(ByVal ds As DataSet)
        InitDetailBandBasedonXRTable(ds)
        Me.Bands(BandKind.Detail).FormattingRules.Add(Me.FinalRow)
        Me.PerformLayout()
        Me.CreateDocument()
    End Sub
#End Region

#Region "Start --> Page headers and footers"
    Public Sub PerpareHeaderAndFooter(ByVal selectedView As String, ByVal fromDate As DateTime, ByVal toDate As DateTime)
        Dim dt As DataTable = MainBase._Reports_Common_DBOps.GetCentreDetails(MainBase._open_Cen_ID, Common_Lib.RealTimeService.ClientScreen.Report_Potrait)
        Xr_Ins_Name.Text = MainBase._open_Ins_Name
        Xr_Cen_Name.Text = "Centre: " + dt.Rows(0)("CEN_NAME").ToString() + " (" + dt.Rows(0)("CEN_UID").ToString() + " )" & "(" & MainBase._open_PAD_No & ")"
        Xr_As_On.Text = "Period: " + Format(fromDate, "dd-MMM-yyyy") + " TO " + Format(toDate, "dd-MMM-yyyy")
        XrLbl_Zone.Text = "Zone: " + MainBase._open_Zone_ID
        XrLbl_City.Text = dt.Rows(0)("CEN_CITY").ToString()
        'Xr_Cen_Name1.Text = dt.Rows(0)("CEN_NAME").ToString() + " (" + dt.Rows(0)("CEN_UID").ToString() + " )"
        XrLbl_stmtName.Text = ReportType.ToUpper()
        Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")")
        If ReportType = "Transaction Summary (Potamel)" Then
            Me.BarCode.Text = MainBase.GetBarCode(MainBase, Common_Lib.RealTimeService.ClientScreen.Report_Potamail)
        End If
        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""
    End Sub


    Public Sub InitBands()
        ' Create bands
        Dim detail As DetailBand = New DetailBand()
        Dim reportHeader As ReportHeaderBand = New ReportHeaderBand()

        detail.Height = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        reportHeader.Height = DataGridViewColumnHeadersHeightSizeMode.AutoSize

        ' Place the bands onto a report
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {detail, reportHeader})
    End Sub

    'creates a table with header and datas
    Public Sub InitDetailBandBasedonXRTable(ByVal ds As DataSet, Optional ByVal IsCollectionBox As Boolean = False)
        Dim colCount As Integer = ds.Tables(0).Columns.Count

        ' Create a table to represent headers
        Dim tableHeader As XRTable = New XRTable()
        tableHeader.BeginInit()
        tableHeader.Height = 40
        tableHeader.Width = Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)
        Dim headerRow As XRTableRow = New XRTableRow()
        headerRow.Width = tableHeader.Width
        tableHeader.Rows.Add(headerRow)

        Dim headerRow1 As XRTableRow = New XRTableRow()
        headerRow1.Width = tableHeader.Width


        ' Create a table to display data
        Dim tableDetail As XRTable = New XRTable()
        tableDetail.BeginInit()
        tableDetail.Height = 20
        tableDetail.Width = Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)
        Dim detailRow As XRTableRow = New XRTableRow()
        detailRow.Width = tableDetail.Width
        tableDetail.Rows.Add(detailRow)
        tableDetail.EvenStyleName = "EvenStyle"
        tableDetail.OddStyleName = "OddStyle"

        ' Create table cells, fill the header cells with text, bind the cells to data
        For i As Integer = 0 To colCount - 1
            Dim headerCell As XRTableCell = New XRTableCell()
            headerCell.Text = ds.Tables(0).Columns(i).ColumnName
            headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            headerCell.CanGrow = True
            headerCell.Multiline = True
            headerCell.WordWrap = True
            headerCell.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
            headerRow.Cells.Add(headerCell)

            Dim detailCell As XRTableCell = New XRTableCell()


            If ds.Tables(0).Columns(i).ColumnName.Trim().ToUpper() = "DATE" Then
                detailCell.DataBindings.Add("Text", ds.Tables(0), ds.Tables(0).Columns(i).Caption, "{0:dd/MM/yyyy}")
                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            ElseIf ds.Tables(0).Columns(i).ColumnName.Trim().ToUpper() = "AMOUNT" Or ds.Tables(0).Columns(i).ColumnName.Trim().ToUpper() = "TOTAL" Then
                'xrTablecell
                headerCell.Text = ds.Tables(0).Columns(i).ColumnName + "(Rs.)"
                'Dim XrPictureBox3 As XRPictureBox = New DevExpress.XtraReports.UI.XRPictureBox()
                'Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralReport_Portrait))
                ''XrPictureBox3
                ''
                'XrPictureBox3.Borders = DevExpress.XtraPrinting.BorderSide.None
                'XrPictureBox3.Image = CType(resources.GetObject("XrPictureBox42.Image"), System.Drawing.Image)
                'XrPictureBox3.Name = "XrPictureBoxRupee"
                'XrPictureBox3.SizeF = New System.Drawing.SizeF(11.08331!, 22.41669!)
                'XrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze
                'XrPictureBox3.StylePriority.UseBorders = False

                ''xrlabel
                'Dim xrlblAmt As New XRLabel()
                'xrlblAmt.Text = ds.Tables(0).Columns(i).ColumnName + "(Rs.)"

                ''xrlblAmt.WidthF = xrlblAmt.Text.Length
                'headerCell.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {XrPictureBox3, xrlblAmt})
                'headerCell.PerformLayout()
                'XrPictureBox3.LeftF = xrlblAmt.WidthF
                ''xrTableCell.Controls.AddRange(xrlblAmt, XrPictureBox3)
                detailCell.DataBindings.Add("Text", ds.Tables(0), ds.Tables(0).Columns(i).Caption, "{0:#,0.00}")
                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            Else
                detailCell.DataBindings.Add("Text", Nothing, ds.Tables(0).Columns(i).Caption)
                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            End If

            detailCell.CanGrow = True
            detailCell.Multiline = True
            detailCell.WordWrap = True
            detailCell.Padding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 5, 1)
            detailCell.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular)

            If i = 0 Then
                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right
            Else
                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right
            End If
            headerCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
            detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot

            'Left Border
            If (ds.Tables(0).Columns(i).Ordinal = 0) Then
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left
                detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
            End If
            'Right Border
            If (ds.Tables(0).Columns(i).Ordinal = ds.Tables(0).Columns.Count - 1) Then
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Right
                detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
            End If

            ' Place the cells into the corresponding tables
            detailRow.Cells.Add(detailCell)
        Next (i)

        'Set width of each column
        Dim colWidth As Single = CType(((Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)) / colCount), Single)

        'header
        For i As Integer = 0 To colCount - 1
            If IsCollectionBox Then
                headerRow.Cells(i).WidthF = colWidth
            Else
                If i Mod 3 = 0 Then
                    headerRow.Cells(i).WidthF = 195
                Else
                    headerRow.Cells(i).WidthF = 115
                End If
            End If
        Next (i)
        'data
        For i As Integer = 0 To colCount - 1
            detailRow.Cells(i).WidthF = headerRow.Cells(i).WidthF
        Next (i)

        'add a line to the bottom of table - in order to have complete look of table
        Dim line As New XRLine
        line.WidthF = detailRow.WidthF
        line.HeightF = 2
        line.LineStyle = Drawing2D.DashStyle.Solid

        tableDetail.EndInit()
        tableHeader.EndInit()
        tableDetail.CanShrink = True
        XrHeaderPanel.WidthF = tableHeader.WidthF
        XrHeaderPanel.Controls.Add(tableHeader)
        XrHeaderPanel.Visible = True
        Me.Bands(BandKind.Detail).Controls.Add(tableDetail)
        ' Me.Bands(BandKind.GroupFooter).Controls.Add(line)

        'Me.CreateDocument()

    End Sub


#End Region

#Region "Report Functions"
    Public Sub New()

        ' This call is required by the designer.
        'Programming_Mode()
        InitializeComponent()
        If MainBase._Reports_Common_DBOps Is Nothing Then
            MainBase.Get_Configure_Setting()
        End If
    End Sub

    Public Sub New(_base As Common_Lib.Common)

        ' This call is required by the designer.
        'Programming_Mode()
        InitializeComponent()
        MainBase = _base
    End Sub

    Private Sub GeneralReport_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        ' If (xPleaseWait.ShowIcon) Then

        'xPleaseWait.Hide()
        ' End If

        'Me.PrintingSystem.Document.AutoFitToPagesWidth = 1
    End Sub
#End Region

#Region "PivotGrid Events"
    Private Sub xrPivotGrid_PrintCell(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs)
        If e.ColumnIndex = 1 And Convert.ToDouble(e.Value) > 50 Then
            e.Brick.BackColor = Color.AliceBlue
        End If

    End Sub

    Private Sub xrPivotGrid_PrintFieldValue(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs)
        e.Brick.BackColor = Color.White
    End Sub

    Private Sub xrPivotGrid_PrintHeader(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs)
        e.Brick.BackColor = Color.White
    End Sub

#End Region
End Class