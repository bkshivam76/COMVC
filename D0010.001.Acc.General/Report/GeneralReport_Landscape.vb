Imports System.ComponentModel
Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI.PivotGrid
Imports DevExpress.XtraPivotGrid

Public Class GeneralReport_Landscape
#Region "Variables"
    Public ReportType As String
    Dim pivotgrid_Const As New XRPivotGrid()
    Dim pivotgrid_WIP As New XRPivotGrid()
#End Region

#Region "start --> Transaction"

    Public Function RecreateTransactionDataSet(ByVal TransactionDatas1 As DataTable) As DataTable
        Dim dt As New DataTable()

        Dim i As Int32 = 0
        Dim colName As String = ""
        Dim position As Int32 = 0
        'add 3 columns which are repeated for every row
        dt.Columns.Add(ReportDataObjects.TransactionData.Properties.VNo)
        dt.Columns.Add(ReportDataObjects.TransactionData.Properties.TrDate)
        dt.Columns.Add(ReportDataObjects.TransactionData.Properties.Particulars)

        Dim dr As DataRow = dt.NewRow()
        dr("Particulars") = "Total"

        'Dim TransactionDatas As New DataSet()
        'Dim rptHelper As New ReportHelper
        'TransactionDatas = rptHelper.GetDataSetHacked(TransactionDatas1)
        For Each row As DataRow In TransactionDatas1.Rows
            'get the value of Type. If it is Receipt then add description column to left of particulars

            'getposition of particulars
            position = dt.Columns(ReportDataObjects.TransactionData.Properties.Particulars).Ordinal
            colName = row.Item(ReportDataObjects.TransactionData.Properties.Head).ToString()

            'check whether description column already exists
            If dt.Columns.Contains(colName) Then
                'do nothing
            Else
                If row.Item(ReportDataObjects.TransactionData.Properties.Type).ToString().ToUpper() = "RECEIPT" Then
                    'dt.Columns.Add(colName).SetOrdinal(position)
                    dt.Columns.Add(colName, System.Type.GetType("System.Double")).SetOrdinal(position)
                Else
                    'dt.Columns.Add(colName).SetOrdinal(position + 1)
                    dt.Columns.Add(colName, System.Type.GetType("System.Double")).SetOrdinal(position + 1)
                End If
            End If
            'Fill value
            Dim newDataRow As DataRow = dt.Rows.Add()
            newDataRow.Item(ReportDataObjects.TransactionData.Properties.VNo) = row.Item(ReportDataObjects.TransactionData.Properties.VNo)
            newDataRow.Item(ReportDataObjects.TransactionData.Properties.TrDate) = row.Item(ReportDataObjects.TransactionData.Properties.TrDate)
            newDataRow.Item(ReportDataObjects.TransactionData.Properties.Particulars) = row.Item(ReportDataObjects.TransactionData.Properties.Particulars)
            newDataRow.Item(colName) = row.Item(ReportDataObjects.TransactionData.Properties.Amt)

        Next

        dt.Columns("TrDate").ColumnName = "Date"

        Dim sumValue As Double = 0.0
        For Each col As DataColumn In dt.Columns
            If (col.ToString() <> "VNo" And col.ToString() <> "Date" And col.ToString() <> "Particulars") Then
                colName = col.ToString()
                For Each XROW As DataRow In dt.Rows
                    Dim outputParam = XROW(colName)
                    If (IsDBNull(outputParam)) Then
                        sumValue = sumValue
                    Else
                        sumValue = sumValue + XROW(colName)
                    End If
                Next
                dr(colName) = sumValue
                sumValue = 0.0
            End If
        Next
        dt.Rows.Add(dr)
        dt.AcceptChanges()
        Return dt
    End Function

#Region "Start --> Report Detail and header view Constructor"

    Public Sub InitTransactionDetailsBasedonXRTable()
        Dim ds As DataSet = Me.DataSource

        If (ds.Tables.Count > 0) Then
            Dim colCount As Integer = ds.Tables(0).Columns.Count
            ' Create a table to represent headers
            Dim tableHeader As XRTable = New XRTable()
            tableHeader.BeginInit()
            tableHeader.Height = 40
            tableHeader.Width = Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)
            Dim headerRow As XRTableRow = New XRTableRow()
            Dim headerRow1 As XRTableRow = New XRTableRow()
            headerRow.Width = tableHeader.Width
            headerRow1.Width = tableHeader.Width

            tableHeader.Rows.Add(headerRow1)
            tableHeader.Rows.Add(headerRow)

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

            Dim ParticularWidth As Integer = 0
            ' Create table cells, fill the header cells with text, bind the cells to data
            For i As Integer = 0 To colCount - 1
                Dim headerCell As XRTableCell = New XRTableCell()
                headerCell.Text = ds.Tables(0).Columns(i).Caption
                headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
                headerCell.CanGrow = True
                headerCell.Multiline = True
                headerCell.WordWrap = True
                headerCell.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
                headerRow.Cells.Add(headerCell)

                Dim detailCell As XRTableCell = New XRTableCell()
                detailCell.DataBindings.Add("Text", Nothing, ds.Tables(0).Columns(i).Caption)
                detailCell.CanGrow = True
                detailCell.Multiline = True
                detailCell.WordWrap = True
                detailCell.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 0)
                detailCell.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular)

                'right and left alignment of cells
                Dim captionString As String = ds.Tables(0).Columns(i).Caption
                If captionString <> "VNo" And captionString <> "TrDate" And captionString <> "Particulars" Then
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
                Else
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                End If

                'format columns
                'If Not ds.Tables(0).Columns(i).Caption = "TRDate" Or Not ds.Tables(0).Columns(i).Caption = "VNo" Or Not ds.Tables(0).Columns(i).Caption = "Particulars" Then
                '    detailCell.DataBindings.Add("Text", ds.Tables(0), ds.Tables(0).Columns(i).Caption, "{0:#,0.00}")
                'End If
                If captionString <> "VNo" And captionString <> "TrDate" And captionString <> "Particulars" Then
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
                    detailCell.DataBindings.Add("Text", ds.Tables(0), ds.Tables(0).Columns(i).Caption, "{0:n}")
                End If
                If ds.Tables(0).Columns(i).Caption = "TRDate" Then
                    detailCell.DataBindings.Add("Text", ds.Tables(0), ds.Tables(0).Columns(i).Caption, "{0:dd/MM/yyyy}")
                End If

                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom Or DevExpress.XtraPrinting.BorderSide.Top
                headerCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                'Draw solid borders
                'If (ds.Tables(0).Columns(i).Caption = "Particulars") Or (ds.Tables(0).Columns(i).Ordinal = 2) Then
                '    detailRow.Cells(i - 1).Borders = BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
                '    detailRow.Cells(i - 1).BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                'End If
                'If (ds.Tables(0).Columns(i).Caption = "Particulars") Then
                '    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right Or BorderSide.Top Or BorderSide.Bottom
                '    detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                'Else
                '    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right Or BorderSide.Top Or BorderSide.Bottom
                '    detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot
                'End If

                'left border
                'If (ds.Tables(0).Columns(i).Ordinal = 0) Then
                '    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or BorderSide.Right Or BorderSide.Top Or BorderSide.Bottom
                '    detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                'End If
                ''right border
                'If (ds.Tables(0).Columns(i).ToString() = ds.Tables(0).Columns.Count - 1) Then
                '    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Right Or BorderSide.Top Or BorderSide.Bottom
                '    detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                'End If

                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right Or BorderSide.Bottom
                detailCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid

                ' Place the cells into the corresponding tables
                detailRow.Cells.Add(detailCell)
            Next (i)
            'Set width of each column
            Dim colWidth As Single = CType(Me.PageWidth - (Me.Margins.Left + Me.Margins.Right), Single)

            'Set width with percentage
            '***********vno width = 40***********Date width = 90***********particular width = 1.75% of remaining***********
            'Calculate remaining width
            Dim totRemainingCol As Integer = colCount - 2
            Dim colWidthSingle As Single = CType((colWidth - (40 + 90)) / (totRemainingCol + 0.75), Single)
            Dim particularsWidth As Single = CType((colWidth - (40 + 90)) - (colWidthSingle * (totRemainingCol - 1)), Single)

            For i As Integer = 0 To colCount - 1
                If (ds.Tables(0).Columns(i).Caption.ToUpper() = "VNO") Then
                    headerRow.Cells(i).WidthF = 40
                ElseIf (ds.Tables(0).Columns(i).Caption.ToUpper() = "DATE") Then
                    headerRow.Cells(i).WidthF = 90
                ElseIf (ds.Tables(0).Columns(i).Caption.ToUpper() = "PARTICULARS") Then
                    headerRow.Cells(i).WidthF = particularsWidth
                Else
                    headerRow.Cells(i).WidthF = colWidthSingle
                End If
            Next (i)

            headerRow.PerformLayout()

            For i As Integer = 0 To colCount - 1
                detailRow.Cells(i).WidthF = headerRow.Cells(i).WidthF
            Next (i)
            detailRow.PerformLayout()

            tableDetail.EndInit()
            tableHeader.EndInit()

            'Add another row to header to specify receipts and payments
            Dim ParticularOrdinal As Integer = ds.Tables(0).Columns("Particulars").Ordinal
            Dim headerCell0 As XRTableCell = New XRTableCell()
            Dim headerCell1 As XRTableCell = New XRTableCell()
            Dim headerCell2 As XRTableCell = New XRTableCell()
            Dim headerCell3 As XRTableCell = New XRTableCell()

            Dim XrShape1 As New XRShape()
            Dim XrShape2 As New XRShape()
            Dim ShapeRectangle1 As DevExpress.XtraPrinting.Shape.ShapeRectangle = New DevExpress.XtraPrinting.Shape.ShapeRectangle()
            Dim ShapeRectangle2 As DevExpress.XtraPrinting.Shape.ShapeRectangle = New DevExpress.XtraPrinting.Shape.ShapeRectangle()

            Dim lblReceipt As New XRLabel()

            lblReceipt.Text = "RECEIPTS"
            lblReceipt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            lblReceipt.CanGrow = False
            lblReceipt.Multiline = True
            lblReceipt.WordWrap = True
            lblReceipt.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
            lblReceipt.ForeColor = Color.White
            lblReceipt.Borders = BorderSide.None


            XrShape1.AnchorVertical = CType((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top Or DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom), DevExpress.XtraReports.UI.VerticalAnchorStyles)
            XrShape1.BorderWidth = 0
            XrShape1.FillColor = System.Drawing.Color.DimGray
            XrShape1.ForeColor = System.Drawing.Color.Transparent
            XrShape1.Name = "XrShape1"
            ShapeRectangle1.Fillet = 70
            XrShape1.Shape = ShapeRectangle1
            XrShape1.SizeF = New System.Drawing.SizeF(lblReceipt.WidthF, lblReceipt.HeightF)
            XrShape1.StylePriority.UseBorderWidth = True
            XrShape1.Borders = BorderSide.None

            headerCell1.Controls.Add(lblReceipt)
            headerCell1.Controls.Add(XrShape1)
            headerCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter


            Dim lblPayments As New XRLabel()

            lblPayments.Text = "PAYMENTS"
            lblPayments.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            lblPayments.CanGrow = False
            lblPayments.Multiline = True
            lblPayments.WordWrap = True
            lblPayments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
            lblPayments.ForeColor = Color.White
            lblPayments.Borders = BorderSide.None


            XrShape2.AnchorVertical = CType((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top Or DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom), DevExpress.XtraReports.UI.VerticalAnchorStyles)
            XrShape2.BorderWidth = 0
            XrShape2.FillColor = System.Drawing.Color.DimGray
            XrShape2.ForeColor = System.Drawing.Color.Transparent
            XrShape2.Name = "XrShape2"
            ShapeRectangle2.Fillet = 70
            XrShape2.Shape = ShapeRectangle2
            XrShape2.SizeF = New System.Drawing.SizeF(lblPayments.WidthF, lblPayments.HeightF)
            XrShape2.StylePriority.UseBorderWidth = True
            XrShape2.Borders = BorderSide.None

            headerCell3.Controls.Add(lblPayments)
            headerCell3.Controls.Add(XrShape2)
            headerCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter


            headerCell1.Borders = BorderSide.Left Or BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
            headerCell3.Borders = BorderSide.Left Or BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
            headerCell0.Borders = BorderSide.Bottom
            headerCell2.Borders = BorderSide.Bottom

            headerCell1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
            headerCell3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid

            headerRow1.Cells.Add(headerCell0)
            headerRow1.Cells.Add(headerCell1)
            headerRow1.Cells.Add(headerCell2)
            headerRow1.Cells.Add(headerCell3)

            'set width
            headerCell0.WidthF = tableHeader.Rows(1).Cells(2).LocationF.X
            headerCell1.TopF = tableHeader.Rows(1).Cells(2).LocationF.X - 1
            headerCell1.WidthF = tableHeader.Rows(1).Cells(ParticularOrdinal).LocationF.X - tableHeader.Rows(1).Cells(2).LocationF.X + 1
            headerCell2.WidthF = tableHeader.Rows(1).Cells(ParticularOrdinal).WidthF
            headerCell3.TopF = tableHeader.Rows(1).Cells(ParticularOrdinal).LocationF.X + tableHeader.Rows(1).Cells(ParticularOrdinal).WidthF - 1

            'Set the location of label and shape
            Dim xValue = (headerCell1.WidthF / 2) - (lblReceipt.WidthF / 2)
            lblReceipt.LocationFloat = New DevExpress.Utils.PointFloat(xValue, 0.0!)
            Dim xPaymentValue = (headerCell3.WidthF / 2) - (lblPayments.WidthF / 2)
            lblPayments.LocationFloat = New DevExpress.Utils.PointFloat(xPaymentValue, 0.0!)

            headerRow1.PerformLayout()
            XrShape1.LocationFloat = lblReceipt.LocationFloat
            XrShape2.LocationFloat = lblPayments.LocationFloat

            Dim line As New XRLine
            line.WidthF = detailRow.WidthF
            line.HeightF = 2
            line.LineStyle = Drawing2D.DashStyle.Solid

            ' XrLinebottom.WidthF = detailRow.WidthF
            'XrLinefooter.WidthF = detailRow.WidthF

            tableHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
            tableHeader.Borders = BorderSide.All
            tableDetail.CanShrink = True
            XrHeaderPanel.WidthF = tableHeader.WidthF


            XrHeaderPanel.Controls.Add(tableHeader)
            XrHeaderPanel.Visible = True
            Me.Bands(BandKind.Detail).Controls.Add(tableDetail)
            'Me.Bands(BandKind.GroupFooter).Controls.Add(line)
        End If
        XrLblRupeeInfo.Visible = True
        Me.Bands(BandKind.Detail).FormattingRules.Add(Me.FinalRow)
        Me.CreateDocument()
    End Sub

#End Region
#End Region

#Region "Start --> BuildingExpense"
  
    Public Sub InitBuildingExpenseDetailsBasedonXRTable(ByVal ds As DataSet)
        ' Create a pivot grid and add it to the Detail band.
        Me.DataSource = Nothing
        Me.DataMember = Nothing

        Dim constDS As New DataSet
        constDS = ds.Copy()
        'Remove second table which is for WIP
        constDS.Tables.RemoveAt(1)

        Dim wipDS As New DataSet
        wipDS = ds.Copy()
        'Remove first table which is for construction
        wipDS.Tables.RemoveAt(0)

        pivotgrid_Const.DataSource = constDS

        'Handlers
        AddHandler pivotgrid_Const.PrintCell, AddressOf Me.xrPivotGrid_PrintCell
        AddHandler pivotgrid_Const.PrintFieldValue, AddressOf Me.xrPivotGrid_PrintFieldValue
        AddHandler pivotgrid_Const.PrintHeader, AddressOf Me.xrPivotGrid_PrintHeader
        AddHandler pivotgrid_Const.CustomFieldSort, AddressOf Me.xrPivotgrid_CustomFieldSort
        AddHandler pivotgrid_Const.CustomCellDisplayText, AddressOf Me.xrPivotGrid_CustomCellDisplayText
        AddHandler pivotgrid_Const.CustomFieldValueCells, AddressOf Me.xrPivotGrid_CustomFieldValueCells
        AddHandler pivotgrid_Const.FieldValueDisplayText, AddressOf Me.xrPivotGrid_FieldValueDisplayText

        'Fix width
        pivotgrid_Const.Width = Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)

        Detail1.Controls.Add(pivotgrid_Const)
        ' Generate pivot grid's fields.
        Dim fieldMonthName As New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField("Month", PivotArea.ColumnArea)
        Dim fieldItemsName As New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField("Items", PivotArea.RowArea)
        Dim fieldAmt As New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField("Amt", PivotArea.DataArea)
        Dim fieldProperty As New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField("Property", PivotArea.RowArea)
        'Specify rowarea index
        fieldProperty.AreaIndex = 0
        fieldItemsName.AreaIndex = 1
        'Add these fields to the pivot grid.
        pivotgrid_Const.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {fieldMonthName, fieldProperty, fieldItemsName, fieldAmt})

        'Remove automatic sorting
        Dim crosfield As PivotGridField = pivotgrid_Const.GetFieldByArea(PivotArea.RowArea, 0)
        crosfield.SortMode = PivotSortMode.None

        'Add custom sorting
        pivotgrid_Const.Fields("Month").SortMode = PivotSortMode.Custom
        pivotgrid_Const.OptionsView.ShowDataHeaders = False
        pivotgrid_Const.OptionsView.ShowGrandTotalsForSingleValues = True

        'Styles, options, appearance and borders
        Me.XrGrandTotalCell.Borders = BorderSide.All
        'Me.XrGrandTotalCell.Font = New System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold)
        Me.XrGrandTotalCell.Font = New System.Drawing.Font("Times New Roman", 9.0F, System.Drawing.FontStyle.Bold)
        Me.XrGrandTotalCell.Name = "XrGrandTotalCell"
        Me.XrGrandTotalCell.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0F)

        Me.XrFieldValueStyle.Borders = BorderSide.All
        'Me.XrFieldValueStyle.Font = New System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular)
        Me.XrFieldValueStyle.Font = New System.Drawing.Font("Times New Roman", 9.0F, System.Drawing.FontStyle.Regular)
        Me.XrFieldValueStyle.Name = "XrFieldValueStyle"
        Me.XrFieldValueStyle.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0F)

        pivotgrid_Const.Styles.FieldHeaderStyle = XrGrandTotalCell
        pivotgrid_Const.Styles.FieldValueStyle = XrFieldValueStyle
        pivotgrid_Const.Styles.GrandTotalCellStyle = XrGrandTotalCell
        pivotgrid_Const.Styles.TotalCellStyle = XrGrandTotalCell
        pivotgrid_Const.Styles.CellStyle = XrFieldValueStyle

        pivotgrid_Const.OptionsView.ShowColumnHeaders = False
        pivotgrid_Const.OptionsView.ShowDataHeaders = False
        pivotgrid_Const.OptionsView.ShowRowHeaders = False
        'Tree view
        pivotgrid_Const.OptionsView.RowTotalsLocation = DevExpress.XtraPivotGrid.PivotRowTotalsLocation.Tree
        pivotgrid_Const.OptionsView.RowTreeOffset = 5
        pivotgrid_Const.OptionsView.RowTreeWidth = 73

        'pivotgrid_Const.Appearance.FieldValue.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        pivotgrid_Const.Appearance.FieldValue.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold)
        pivotgrid_Const.Appearance.FieldValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        pivotgrid_Const.Appearance.FieldValue.Options.UseTextOptions = True
        pivotgrid_Const.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        pivotgrid_Const.Appearance.FieldHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        pivotgrid_Const.Appearance.FieldValueGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        pivotgrid_Const.OptionsChartDataSource.UpdateDelay = 300

        'Add wordwrap
        fieldProperty.Appearance.FieldValue.Options.UseTextOptions = True
        fieldProperty.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        fieldProperty.RowValueLineCount = 3

        fieldItemsName.Appearance.FieldValue.Options.UseTextOptions = True
        fieldItemsName.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        fieldItemsName.RowValueLineCount = 3

        ''Format for Amount field
        fieldAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        fieldAmt.CellFormat.FormatString = "N2"

        'Specify width
        fieldMonthName.Width = 90
        fieldProperty.Width = 0

        '***************************************WIP**********************************
        ReportHeader2.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand


        pivotgrid_WIP.DataSource = wipDS
        'Handlers
        AddHandler pivotgrid_WIP.PrintCell, AddressOf Me.xrPivotGrid_PrintCell
        AddHandler pivotgrid_WIP.PrintFieldValue, AddressOf Me.xrPivotGrid_PrintFieldValue
        AddHandler pivotgrid_WIP.PrintHeader, AddressOf Me.xrPivotGrid_PrintHeader
        AddHandler pivotgrid_WIP.CustomFieldSort, AddressOf Me.xrPivotgrid_CustomFieldSort
        AddHandler pivotgrid_WIP.CustomCellDisplayText, AddressOf Me.xrPivotGrid_CustomCellDisplayText
        AddHandler pivotgrid_WIP.CustomFieldValueCells, AddressOf Me.xrPivotGrid_CustomFieldValueCells
        AddHandler pivotgrid_WIP.FieldValueDisplayText, AddressOf Me.xrPivotGrid_FieldValueDisplayText

        'Width
        pivotgrid_WIP.Width = Me.PageWidth - (Me.Margins.Left + Me.Margins.Right)

        Detail2.Controls.Add(pivotgrid_WIP)
        ' Generate pivot grid's fields.
        Dim fldMonthName As New XRPivotGridField("Month", PivotArea.ColumnArea)
        Dim fldItemsName As New XRPivotGridField("Items", PivotArea.RowArea)
        Dim fldAmt As New XRPivotGridField("Amt", PivotArea.DataArea)
        Dim fldProperty As New XRPivotGridField("Property", PivotArea.RowArea)
       
        'Add these fields to the pivot grid.
        pivotgrid_WIP.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {fldMonthName, fldProperty, fldItemsName, fldAmt})

        'remove automatic sorting
        Dim crossfield As PivotGridField = pivotgrid_WIP.GetFieldByArea(PivotArea.RowArea, 0)
        crossfield.SortMode = PivotSortMode.None
        'Custom sorting
        pivotgrid_WIP.Fields("Month").SortMode = PivotSortMode.Custom

        'Options,Styles and Appearances
        pivotgrid_WIP.OptionsView.ShowDataHeaders = False
        pivotgrid_WIP.OptionsView.ShowRowHeaders = False
        pivotgrid_WIP.OptionsView.ShowGrandTotalsForSingleValues = True
        pivotgrid_WIP.OptionsView.ShowColumnHeaders = False
        pivotgrid_WIP.OptionsView.ShowDataHeaders = False
        pivotgrid_WIP.OptionsView.RowTotalsLocation = DevExpress.XtraPivotGrid.PivotRowTotalsLocation.Tree
        pivotgrid_WIP.OptionsView.RowTreeOffset = 5
        pivotgrid_WIP.OptionsView.RowTreeWidth = 73
        pivotgrid_WIP.OptionsChartDataSource.UpdateDelay = 300

        pivotgrid_WIP.Styles.FieldHeaderStyle = XrGrandTotalCell
        pivotgrid_WIP.Styles.FieldValueStyle = XrFieldValueStyle
        pivotgrid_WIP.Styles.GrandTotalCellStyle = XrGrandTotalCell
        pivotgrid_WIP.Styles.TotalCellStyle = XrGrandTotalCell
        pivotgrid_WIP.Styles.CellStyle = XrFieldValueStyle

        pivotgrid_WIP.Appearance.FieldValue.Options.UseTextOptions = True
        pivotgrid_WIP.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        pivotgrid_WIP.Appearance.FieldHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        'pivotgrid_WIP.Appearance.FieldValue.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        pivotgrid_WIP.Appearance.FieldValue.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold)
        pivotgrid_WIP.Appearance.FieldValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        pivotgrid_WIP.Appearance.FieldValueGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

        'Word wrap
        fldProperty.Appearance.FieldValue.Options.UseTextOptions = True
        fldProperty.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        fldProperty.RowValueLineCount = 3

        fldItemsName.Appearance.FieldValue.Options.UseTextOptions = True
        fldItemsName.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        fldItemsName.RowValueLineCount = 3

        'Format
        fldAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        fldAmt.CellFormat.FormatString = "N2"

        fldMonthName.Width = 90
        fldProperty.Width = 0

        'remove headerpanel
        XrHeaderPanel.Visible = False
        XrHeaderPanel.HeightF = 0
       
        'remove headerpanel
        XrHeaderPanel.Visible = False
        XrHeaderPanel.HeightF = 0

        DevExpress.Utils.Paint.XPaint.ForceGDIPlusPaint()
        Me.Detail.FormattingRules.Add(Me.TableOuterBorder)
        Me.CreateDocument()
    End Sub
#End Region

#Region "Start --> Page headers and footers"
    Public Sub PerpareHeaderAndFooter(ByVal selectedView As String, ByVal fromDate As DateTime, ByVal toDate As DateTime)
        Dim dt As DataTable = Base._Reports_Common_DBOps.GetCentreDetails(Base._open_Cen_ID, Common_Lib.RealTimeService.ClientScreen.Report_Landscape)
        If dt Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Xr_Ins_Name.Text = Base._open_Ins_Name
        Xr_Cen_Name.Text = "UID: " + dt.Rows(0)("CEN_UID").ToString() + "(" & Base._open_PAD_No & ")"
        Xr_As_On.Text = "Period: " + Format(fromDate, "dd-MMM-yyyy") + " TO " + Format(toDate, "dd-MMM-yyyy")
        XrLbl_Zone.Text = "Zone: " + Base._open_Zone_ID
        XrLbl_stmtName.Text = "CONSTRUCTION/WIP EXPENSES at " + dt.Rows(0)("CEN_NAME").ToString().ToUpper()
        XrLbl_stmt_Const_Name.Text = "CONSTRUCTION STATEMENT"
        XrLbl_stmt_wip_Name.Text = "WIP STATEMENT"
        Xr_Version.Text = "Ver.: " & Base._Current_Version
        Me.Xr_Printable.Text = IIf(Base._ReportsToBePrinted = String.Empty, "", "(" & Base._ReportsToBePrinted & ")")

        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = Base._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
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
    Public Sub InitDetailBandBasedonXRTable(ByVal ds As DataSet)
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
            headerRow.Cells(i).WidthF = colWidth
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
        Me.Bands(BandKind.GroupFooter).Controls.Add(line)

        'Me.CreateDocument()

    End Sub

    Public Sub DynamicTable(ByVal ds As DataSet)
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom

        Dim padding As Integer = 10
        Dim tableWidth As Integer = (ds.Tables(0).Columns.Count * 250) ' - Me.Margins.Left - Me.Margins.Right - padding * 2

        Dim dynamicTable As XRTable = XRTable.CreateTable(
                              New Rectangle(padding, 2, tableWidth, 40), ds.Tables(0).Rows.Count, ds.Tables(0).Columns.Count)
        '2,         ' rect Y
        'tableWidth, ' width
        '40),        ' height
        '1,          'table row count
        '0);         ' table column count

        dynamicTable.Width = tableWidth
        dynamicTable.Rows.FirstRow.Width = tableWidth
        dynamicTable.Borders = DevExpress.XtraPrinting.BorderSide.All
        dynamicTable.BorderWidth = 1
        Dim i As Integer = 0 ' Int(i = 0)
        For Each dc As DataColumn In ds.Tables(0).Columns
            Dim cell As XRTableCell = New XRTableCell()

            Dim binding As XRBinding = New XRBinding("Text", ds, ds.Tables(0).Columns(i).ColumnName)
            cell.DataBindings.Add(binding)
            cell.CanGrow = False
            cell.Width = 100
            cell.Text = dc.ColumnName
            dynamicTable.Rows.FirstRow.Cells.Add(cell)
            i = i + 1
        Next
        dynamicTable.Font = New System.Drawing.Font("Verdana", 7.0F)

        Detail.Controls.Add(dynamicTable)

        'Label.Text = String.Format("Data table: {0}", tableName)

        Me.DataSource = ds
        Me.DataMember = ds.Tables(0).TableName


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

    Private Sub xrPivotgrid_CustomFieldSort(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs)
        If e.Field.FieldName = "Month" Then
            'Custom sorting w.r.t MonthNo
            Dim orderValue1 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MonthNo"), orderValue2 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MonthNo")
            e.Result = Comparer.Default.Compare(orderValue1, orderValue2)
            e.Handled = True
        End If
    End Sub

    Private Sub xrPivotGrid_CustomCellDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.PivotCellDisplayTextEventArgs)
        Dim ds As PivotDrillDownDataSource
        ds = e.CreateDrillDownDataSource()
        'If normal cells has 0, remove them
        If (ds.RowCount = 1 AndAlso ds(0)("Amt") Is Nothing) Or (e.RowValueType = DevExpress.XtraPivotGrid.PivotGridValueType.Total) Then
            e.DisplayText = ""
        End If

        If (e.ColumnField Is Nothing OrElse e.RowField Is Nothing) AndAlso ds(0)("Amt") Is Nothing Then
            'this is Grand Total cell, include 0 incase of empty cells
            e.DisplayText = "0.00"
            Return
        End If
    End Sub

    Protected Sub xrPivotGrid_CustomFieldValueCells(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs)
        If pivotgrid_WIP.DataSource Is Nothing Then
            Return
        End If

        If pivotgrid_Const.DataSource Is Nothing Then
            Return
        End If

        ' Iterates through all row headers.
        For i As Integer = e.GetCellCount(False) - 1 To 0 Step -1
            Dim cell As DevExpress.XtraReports.UI.PivotGrid.FieldValueCell = e.GetCell(False, i)
            ' If the current header corresponds to Total Row header, set grand total
            If cell.ValueType = PivotGridValueType.Total Then
                e.SetGrandTotalLocation(True, 1)
            End If
        Next i

    End Sub

    Private Sub xrPivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs)
        'Remove "Total" keyword from report
        If (e.ValueType = PivotGridValueType.Total) Then
            e.DisplayText = e.DisplayText.Replace("Total", String.Empty)
        End If
    End Sub

#End Region

#Region "Report Function"
    Public Sub New()

        ' This call is required by the designer.
        'Programming_Mode()
        InitializeComponent()
    End Sub

    Private Sub GeneralReport_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        ' If (xPleaseWait.ShowIcon) Then
        'xPleaseWait.Hide()
        'End If

        'Me.PrintingSystem.Document.AutoFitToPagesWidth = 1
    End Sub

    Public Sub InitTransactionsSummary(ByVal ds As DataSet)
        InitDetailBandBasedonXRTable(ds)
        Me.Bands(BandKind.Detail).FormattingRules.Add(Me.FinalRow)
        Me.PerformLayout()
        Me.CreateDocument()
    End Sub
#End Region

End Class