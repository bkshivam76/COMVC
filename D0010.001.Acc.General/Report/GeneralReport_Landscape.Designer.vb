<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class GeneralReport_Landscape
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralReport_Landscape))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TableOuterBorder = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FinalRow = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.Xr_Version = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid()
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrTopMarginPanel = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Xr_Printable = New DevExpress.XtraReports.UI.XRLabel()
        Me.Xr_As_On = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLbl_stmtName = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLbl_Zone = New DevExpress.XtraReports.UI.XRLabel()
        Me.Xr_Cen_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.Xr_Ins_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrFieldValueStyle = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.XrFieldHeaderStyle = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.XrGrandTotalCell = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.Xr_Incharge = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine69 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel69 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.ReportHeader1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLbl_stmt_Const_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLblRupeeInfo = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrHeaderPanel = New DevExpress.XtraReports.UI.XRPanel()
        Me.DetailReport1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail2 = New DevExpress.XtraReports.UI.DetailBand()
        Me.ReportHeader2 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLbl_stmt_wip_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.XrTopMarginPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Expanded = False
        Me.Detail.FormattingRules.Add(Me.TableOuterBorder)
        Me.Detail.HeightF = 0.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TableOuterBorder
        '
        '
        '
        '
        Me.TableOuterBorder.Formatting.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.TableOuterBorder.Formatting.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.TableOuterBorder.Name = "TableOuterBorder"
        '
        'FinalRow
        '
        Me.FinalRow.Condition = "[DataSource.CurrentRowIndex] ==  [DataSource.RowCount]-1"
        '
        '
        '
        Me.FinalRow.Formatting.BackColor = System.Drawing.Color.Transparent
        Me.FinalRow.Formatting.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.FinalRow.Formatting.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.FinalRow.Formatting.BorderWidth = 2
        Me.FinalRow.Formatting.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FinalRow.Formatting.ForeColor = System.Drawing.Color.DarkRed
        Me.FinalRow.Name = "FinalRow"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 49.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.Xr_Version, Me.XrPictureBox1, Me.XrPageInfo1, Me.XrPivotGrid1})
        Me.BottomMargin.HeightF = 107.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Format = "Printed On: {0:dd-MMM, yyyy}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(566.1658!, 0.99998!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(300.0!, 30.0!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Xr_Version
        '
        Me.Xr_Version.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_Version.LocationFloat = New DevExpress.Utils.PointFloat(159.375!, 10.99998!)
        Me.Xr_Version.Name = "Xr_Version"
        Me.Xr_Version.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Version.SizeF = New System.Drawing.SizeF(86.05582!, 11.0!)
        Me.Xr_Version.StylePriority.UseFont = False
        Me.Xr_Version.Text = "Ver. 2.0"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Image = CType(resources.GetObject("XrPictureBox1.Image"), System.Drawing.Image)
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(1.041667!, 0.0!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(158.3333!, 30.0!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(1217.666!, 11.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(83.33333!, 13.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.FieldHeaderStyleName = "XrFieldHeaderStyle"
        Me.XrPivotGrid1.FieldValueStyleName = "XrFieldValueStyle"
        Me.XrPivotGrid1.GrandTotalCellStyleName = "XrGrandTotalCell"
        Me.XrPivotGrid1.LocationFloat = New DevExpress.Utils.PointFloat(1199.958!, 82.41666!)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.OptionsChartDataSource.UpdateDelay = 300
        Me.XrPivotGrid1.SizeF = New System.Drawing.SizeF(78.54163!, 14.58333!)
        Me.XrPivotGrid1.Visible = False
        '
        'FormattingRule1
        '
        '
        '
        '
        Me.FormattingRule1.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify
        Me.FormattingRule1.Name = "FormattingRule1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.HeightF = 0.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTopMarginPanel})
        Me.ReportHeader.HeightF = 95.83334!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrTopMarginPanel
        '
        Me.XrTopMarginPanel.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrTopMarginPanel.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTopMarginPanel.Name = "XrTopMarginPanel"
        Me.XrTopMarginPanel.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTopMarginPanel.SizeF = New System.Drawing.SizeF(1301.0!, 92.08333!)
        Me.XrTopMarginPanel.StylePriority.UseTextAlignment = False
        Me.XrTopMarginPanel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 4.1400000000000006R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.Xr_Printable, Me.Xr_As_On, Me.XrLbl_stmtName, Me.XrLbl_Zone, Me.Xr_Cen_Name, Me.Xr_Ins_Name})
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.Weight = 7.7000000000000011R
        '
        'Xr_Printable
        '
        Me.Xr_Printable.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.Xr_Printable.AutoWidth = True
        Me.Xr_Printable.CanGrow = False
        Me.Xr_Printable.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Xr_Printable.ForeColor = System.Drawing.Color.DarkRed
        Me.Xr_Printable.LocationFloat = New DevExpress.Utils.PointFloat(575.0!, 45.58004!)
        Me.Xr_Printable.Name = "Xr_Printable"
        Me.Xr_Printable.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.Xr_Printable.SizeF = New System.Drawing.SizeF(151.0417!, 19.25!)
        Me.Xr_Printable.StylePriority.UseFont = False
        Me.Xr_Printable.StylePriority.UseForeColor = False
        Me.Xr_Printable.StylePriority.UsePadding = False
        Me.Xr_Printable.StylePriority.UseTextAlignment = False
        Me.Xr_Printable.Text = "Xr_Printable"
        Me.Xr_Printable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Xr_As_On
        '
        Me.Xr_As_On.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.Xr_As_On.BorderColor = System.Drawing.Color.Empty
        Me.Xr_As_On.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.Xr_As_On.ForeColor = System.Drawing.Color.DarkRed
        Me.Xr_As_On.LocationFloat = New DevExpress.Utils.PointFloat(435.75!, 71.58005!)
        Me.Xr_As_On.Name = "Xr_As_On"
        Me.Xr_As_On.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_As_On.SizeF = New System.Drawing.SizeF(419.0418!, 21.0!)
        Me.Xr_As_On.StylePriority.UseBorderColor = False
        Me.Xr_As_On.StylePriority.UseFont = False
        Me.Xr_As_On.StylePriority.UseForeColor = False
        Me.Xr_As_On.StylePriority.UsePadding = False
        Me.Xr_As_On.StylePriority.UseTextAlignment = False
        Me.Xr_As_On.Text = "Detail as on 31 March, 20XXg"
        Me.Xr_As_On.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLbl_stmtName
        '
        Me.XrLbl_stmtName.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold)
        Me.XrLbl_stmtName.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLbl_stmtName.LocationFloat = New DevExpress.Utils.PointFloat(1.041667!, 25.50004!)
        Me.XrLbl_stmtName.Multiline = True
        Me.XrLbl_stmtName.Name = "XrLbl_stmtName"
        Me.XrLbl_stmtName.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrLbl_stmtName.SizeF = New System.Drawing.SizeF(1299.958!, 26.08!)
        Me.XrLbl_stmtName.StylePriority.UseFont = False
        Me.XrLbl_stmtName.StylePriority.UseForeColor = False
        Me.XrLbl_stmtName.StylePriority.UsePadding = False
        Me.XrLbl_stmtName.StylePriority.UseTextAlignment = False
        Me.XrLbl_stmtName.Text = "XrLbl_StatementName"
        Me.XrLbl_stmtName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLbl_Zone
        '
        Me.XrLbl_Zone.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrLbl_Zone.AutoWidth = True
        Me.XrLbl_Zone.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLbl_Zone.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLbl_Zone.LocationFloat = New DevExpress.Utils.PointFloat(1.875959!, 72.58005!)
        Me.XrLbl_Zone.Name = "XrLbl_Zone"
        Me.XrLbl_Zone.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLbl_Zone.SizeF = New System.Drawing.SizeF(211.5009!, 21.0!)
        Me.XrLbl_Zone.StylePriority.UseFont = False
        Me.XrLbl_Zone.StylePriority.UseForeColor = False
        Me.XrLbl_Zone.StylePriority.UsePadding = False
        Me.XrLbl_Zone.StylePriority.UseTextAlignment = False
        Me.XrLbl_Zone.Text = "XrLbl_Zone"
        Me.XrLbl_Zone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Xr_Cen_Name
        '
        Me.Xr_Cen_Name.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.Xr_Cen_Name.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Cen_Name.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.Xr_Cen_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.Xr_Cen_Name.LocationFloat = New DevExpress.Utils.PointFloat(969.2939!, 70.58005!)
        Me.Xr_Cen_Name.Name = "Xr_Cen_Name"
        Me.Xr_Cen_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Cen_Name.SizeF = New System.Drawing.SizeF(329.2065!, 21.0!)
        Me.Xr_Cen_Name.StylePriority.UseBorderColor = False
        Me.Xr_Cen_Name.StylePriority.UseFont = False
        Me.Xr_Cen_Name.StylePriority.UseForeColor = False
        Me.Xr_Cen_Name.StylePriority.UseTextAlignment = False
        Me.Xr_Cen_Name.Text = "Centre name (UID)"
        Me.Xr_Cen_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'Xr_Ins_Name
        '
        Me.Xr_Ins_Name.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Ins_Name.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Xr_Ins_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.Xr_Ins_Name.LocationFloat = New DevExpress.Utils.PointFloat(1.041667!, 0.0!)
        Me.Xr_Ins_Name.Name = "Xr_Ins_Name"
        Me.Xr_Ins_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Ins_Name.SizeF = New System.Drawing.SizeF(1299.958!, 25.0!)
        Me.Xr_Ins_Name.StylePriority.UseBorderColor = False
        Me.Xr_Ins_Name.StylePriority.UseFont = False
        Me.Xr_Ins_Name.StylePriority.UseForeColor = False
        Me.Xr_Ins_Name.StylePriority.UseTextAlignment = False
        Me.Xr_Ins_Name.Text = "Name of Institution"
        Me.Xr_Ins_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GroupHeader1
        '
        Me.GroupHeader1.HeightF = 0.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'PageFooter
        '
        Me.PageFooter.HeightF = 0.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'PageHeader
        '
        Me.PageHeader.Expanded = False
        Me.PageHeader.HeightF = 0.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrFieldValueStyle
        '
        Me.XrFieldValueStyle.BackColor = System.Drawing.Color.Transparent
        Me.XrFieldValueStyle.Name = "XrFieldValueStyle"
        Me.XrFieldValueStyle.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        '
        'XrFieldHeaderStyle
        '
        Me.XrFieldHeaderStyle.BackColor = System.Drawing.Color.Transparent
        Me.XrFieldHeaderStyle.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrFieldHeaderStyle.Name = "XrFieldHeaderStyle"
        Me.XrFieldHeaderStyle.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        '
        'XrGrandTotalCell
        '
        Me.XrGrandTotalCell.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrGrandTotalCell.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrGrandTotalCell.Name = "XrGrandTotalCell"
        Me.XrGrandTotalCell.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        '
        'Xr_Incharge
        '
        Me.Xr_Incharge.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Incharge.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_Incharge.ForeColor = System.Drawing.Color.Black
        Me.Xr_Incharge.LocationFloat = New DevExpress.Utils.PointFloat(5.0!, 133.8959!)
        Me.Xr_Incharge.Name = "Xr_Incharge"
        Me.Xr_Incharge.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Incharge.SizeF = New System.Drawing.SizeF(381.0038!, 18.00001!)
        Me.Xr_Incharge.StylePriority.UseBorderColor = False
        Me.Xr_Incharge.StylePriority.UseFont = False
        Me.Xr_Incharge.StylePriority.UseForeColor = False
        Me.Xr_Incharge.StylePriority.UseTextAlignment = False
        Me.Xr_Incharge.Text = "Name:"
        Me.Xr_Incharge.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLine69
        '
        Me.XrLine69.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.XrLine69.BorderWidth = 1
        Me.XrLine69.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.XrLine69.LineWidth = 0
        Me.XrLine69.LocationFloat = New DevExpress.Utils.PointFloat(5.0!, 103.3959!)
        Me.XrLine69.Name = "XrLine69"
        Me.XrLine69.SizeF = New System.Drawing.SizeF(185.0!, 2.0!)
        Me.XrLine69.StylePriority.UseBorderDashStyle = False
        Me.XrLine69.StylePriority.UseBorderWidth = False
        '
        'XrLabel69
        '
        Me.XrLabel69.BorderColor = System.Drawing.Color.Empty
        Me.XrLabel69.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel69.ForeColor = System.Drawing.Color.Black
        Me.XrLabel69.LocationFloat = New DevExpress.Utils.PointFloat(5.000003!, 115.8958!)
        Me.XrLabel69.Name = "XrLabel69"
        Me.XrLabel69.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel69.SizeF = New System.Drawing.SizeF(381.0035!, 18.0!)
        Me.XrLabel69.StylePriority.UseBorderColor = False
        Me.XrLabel69.StylePriority.UseFont = False
        Me.XrLabel69.StylePriority.UseForeColor = False
        Me.XrLabel69.StylePriority.UseTextAlignment = False
        Me.XrLabel69.Text = "Sign of Centre Incharge"
        Me.XrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo3, Me.XrLabel1, Me.XrLabel69, Me.XrLine69, Me.Xr_Incharge})
        Me.ReportFooter.HeightF = 270.8333!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo3.Format = "{0:dd-MMM, yyyy}"
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(69.58334!, 151.8959!)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo3.SizeF = New System.Drawing.SizeF(133.3334!, 22.99998!)
        Me.XrPageInfo3.StylePriority.UseFont = False
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.ForeColor = System.Drawing.Color.Black
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(5.000003!, 151.8959!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(64.58334!, 23.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseForeColor = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Date:"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1, Me.ReportHeader1, Me.GroupHeader2})
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.HeightF = 17.70833!
        Me.Detail1.Name = "Detail1"
        Me.Detail1.StylePriority.UseTextAlignment = False
        Me.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLbl_stmt_Const_Name})
        Me.ReportHeader1.HeightF = 37.5!
        Me.ReportHeader1.Name = "ReportHeader1"
        '
        'XrLbl_stmt_Const_Name
        '
        Me.XrLbl_stmt_Const_Name.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold)
        Me.XrLbl_stmt_Const_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLbl_stmt_Const_Name.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLbl_stmt_Const_Name.Multiline = True
        Me.XrLbl_stmt_Const_Name.Name = "XrLbl_stmt_Const_Name"
        Me.XrLbl_stmt_Const_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrLbl_stmt_Const_Name.SizeF = New System.Drawing.SizeF(1299.958!, 26.0!)
        Me.XrLbl_stmt_Const_Name.StylePriority.UseFont = False
        Me.XrLbl_stmt_Const_Name.StylePriority.UseForeColor = False
        Me.XrLbl_stmt_Const_Name.StylePriority.UsePadding = False
        Me.XrLbl_stmt_Const_Name.StylePriority.UseTextAlignment = False
        Me.XrLbl_stmt_Const_Name.Text = "XrLbl_StatementName"
        Me.XrLbl_stmt_Const_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLblRupeeInfo, Me.XrHeaderPanel})
        Me.GroupHeader2.HeightF = 19.70833!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'XrLblRupeeInfo
        '
        Me.XrLblRupeeInfo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Italic)
        Me.XrLblRupeeInfo.LocationFloat = New DevExpress.Utils.PointFloat(1074.624!, 0.0!)
        Me.XrLblRupeeInfo.Name = "XrLblRupeeInfo"
        Me.XrLblRupeeInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLblRupeeInfo.SizeF = New System.Drawing.SizeF(226.3755!, 17.70833!)
        Me.XrLblRupeeInfo.StylePriority.UseFont = False
        Me.XrLblRupeeInfo.StylePriority.UseTextAlignment = False
        Me.XrLblRupeeInfo.Text = "All Figures in INR"
        Me.XrLblRupeeInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrHeaderPanel
        '
        Me.XrHeaderPanel.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrHeaderPanel.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 17.70833!)
        Me.XrHeaderPanel.Name = "XrHeaderPanel"
        Me.XrHeaderPanel.SizeF = New System.Drawing.SizeF(1301.0!, 2.0!)
        Me.XrHeaderPanel.StylePriority.UseBorders = False
        Me.XrHeaderPanel.Visible = False
        '
        'DetailReport1
        '
        Me.DetailReport1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail2, Me.ReportHeader2, Me.GroupHeader3})
        Me.DetailReport1.Level = 1
        Me.DetailReport1.Name = "DetailReport1"
        '
        'Detail2
        '
        Me.Detail2.HeightF = 0.0!
        Me.Detail2.Name = "Detail2"
        '
        'ReportHeader2
        '
        Me.ReportHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLbl_stmt_wip_Name})
        Me.ReportHeader2.HeightF = 58.33333!
        Me.ReportHeader2.Name = "ReportHeader2"
        '
        'XrLbl_stmt_wip_Name
        '
        Me.XrLbl_stmt_wip_Name.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold)
        Me.XrLbl_stmt_wip_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLbl_stmt_wip_Name.LocationFloat = New DevExpress.Utils.PointFloat(1.041992!, 9.999974!)
        Me.XrLbl_stmt_wip_Name.Multiline = True
        Me.XrLbl_stmt_wip_Name.Name = "XrLbl_stmt_wip_Name"
        Me.XrLbl_stmt_wip_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrLbl_stmt_wip_Name.SizeF = New System.Drawing.SizeF(1299.958!, 26.0!)
        Me.XrLbl_stmt_wip_Name.StylePriority.UseFont = False
        Me.XrLbl_stmt_wip_Name.StylePriority.UseForeColor = False
        Me.XrLbl_stmt_wip_Name.StylePriority.UsePadding = False
        Me.XrLbl_stmt_wip_Name.StylePriority.UseTextAlignment = False
        Me.XrLbl_stmt_wip_Name.Text = "XrLbl_StatementName"
        Me.XrLbl_stmt_wip_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel6})
        Me.GroupHeader3.HeightF = 17.70833!
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Italic)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(1074.625!, 0.0!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(226.3755!, 17.70833!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "All Figures in INR"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'GeneralReport_Landscape
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupFooter1, Me.ReportHeader, Me.GroupHeader1, Me.ReportFooter, Me.PageFooter, Me.PageHeader, Me.DetailReport, Me.DetailReport1})
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FinalRow, Me.TableOuterBorder})
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(53, 46, 49, 107)
        Me.PageHeight = 850
        Me.PageWidth = 1400
        Me.PaperKind = System.Drawing.Printing.PaperKind.Legal
        Me.ShowPrintMarginsWarning = False
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.XrFieldValueStyle, Me.XrFieldHeaderStyle, Me.XrGrandTotalCell})
        Me.Version = "12.1"
        CType(Me.XrTopMarginPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents FinalRow As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents TableOuterBorder As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrFieldValueStyle As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrFieldHeaderStyle As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrGrandTotalCell As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents Xr_Version As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents Xr_Incharge As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine69 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel69 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader1 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLblRupeeInfo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrHeaderPanel As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents DetailReport1 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail2 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader2 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrTopMarginPanel As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Xr_As_On As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_stmtName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_Zone As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Xr_Cen_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Xr_Ins_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_stmt_Const_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_stmt_wip_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Xr_Printable As DevExpress.XtraReports.UI.XRLabel
End Class
