<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Report_Potamel
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
        Dim QrCodeGenerator1 As DevExpress.XtraPrinting.BarCode.QRCodeGenerator = New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Report_Potamel))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TableOuterBorder = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FinalRow = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.XrTopMarginPanel = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Xr_Printable = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.Xr_Ins_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.Xr_Cen_Name = New DevExpress.XtraReports.UI.XRLabel()
        Me.Xr_As_On = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLbl_Zone = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLbl_stmtName = New DevExpress.XtraReports.UI.XRLabel()
        Me.WinControlContainer1 = New DevExpress.XtraReports.UI.WinControlContainer()
        Me.BarCode = New DevExpress.XtraEditors.BarCodeControl()
        Me.XrLbl_City = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.Xr_Version = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLinefooter = New DevExpress.XtraReports.UI.XRLine()
        Me.XrPanel1 = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLinebottom = New DevExpress.XtraReports.UI.XRLine()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrHeaderPanel = New DevExpress.XtraReports.UI.XRPanel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.Xr_Incharge = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        CType(Me.XrTopMarginPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
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
        Me.FinalRow.Formatting.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.FinalRow.Formatting.BorderWidth = 2.0!
        Me.FinalRow.Formatting.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FinalRow.Formatting.ForeColor = System.Drawing.Color.DarkRed
        Me.FinalRow.Name = "FinalRow"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 49.37499!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTopMarginPanel
        '
        Me.XrTopMarginPanel.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrTopMarginPanel.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTopMarginPanel.Name = "XrTopMarginPanel"
        Me.XrTopMarginPanel.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTopMarginPanel.SizeF = New System.Drawing.SizeF(747.0001!, 132.0417!)
        Me.XrTopMarginPanel.StylePriority.UseTextAlignment = False
        Me.XrTopMarginPanel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 3.8485358559520546R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.Xr_Printable, Me.XrLine1, Me.Xr_Ins_Name, Me.Xr_Cen_Name, Me.Xr_As_On, Me.XrLbl_Zone, Me.XrLbl_stmtName, Me.WinControlContainer1})
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
        Me.Xr_Printable.LocationFloat = New DevExpress.Utils.PointFloat(301.0417!, 98.7917!)
        Me.Xr_Printable.Name = "Xr_Printable"
        Me.Xr_Printable.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.Xr_Printable.SizeF = New System.Drawing.SizeF(151.0417!, 21.24998!)
        Me.Xr_Printable.StylePriority.UseFont = False
        Me.Xr_Printable.StylePriority.UseForeColor = False
        Me.Xr_Printable.StylePriority.UsePadding = False
        Me.Xr_Printable.StylePriority.UseTextAlignment = False
        Me.Xr_Printable.Text = "Xr_Printable"
        Me.Xr_Printable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLine1
        '
        Me.XrLine1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 125.7917!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(745.9583!, 6.250015!)
        Me.XrLine1.StylePriority.UseBorders = False
        '
        'Xr_Ins_Name
        '
        Me.Xr_Ins_Name.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.Xr_Ins_Name.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Ins_Name.CanGrow = False
        Me.Xr_Ins_Name.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Xr_Ins_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.Xr_Ins_Name.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.Xr_Ins_Name.Name = "Xr_Ins_Name"
        Me.Xr_Ins_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Ins_Name.SizeF = New System.Drawing.SizeF(747.0!, 25.0!)
        Me.Xr_Ins_Name.StylePriority.UseBorderColor = False
        Me.Xr_Ins_Name.StylePriority.UseFont = False
        Me.Xr_Ins_Name.StylePriority.UseForeColor = False
        Me.Xr_Ins_Name.StylePriority.UseTextAlignment = False
        Me.Xr_Ins_Name.Text = "Name of Institution"
        Me.Xr_Ins_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'Xr_Cen_Name
        '
        Me.Xr_Cen_Name.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.Xr_Cen_Name.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Cen_Name.CanShrink = True
        Me.Xr_Cen_Name.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_Cen_Name.ForeColor = System.Drawing.Color.Black
        Me.Xr_Cen_Name.LocationFloat = New DevExpress.Utils.PointFloat(15.87594!, 32.99999!)
        Me.Xr_Cen_Name.Name = "Xr_Cen_Name"
        Me.Xr_Cen_Name.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Cen_Name.SizeF = New System.Drawing.SizeF(288.8324!, 21.0!)
        Me.Xr_Cen_Name.StylePriority.UseBorderColor = False
        Me.Xr_Cen_Name.StylePriority.UseFont = False
        Me.Xr_Cen_Name.StylePriority.UseForeColor = False
        Me.Xr_Cen_Name.StylePriority.UseTextAlignment = False
        Me.Xr_Cen_Name.Text = "Centre name (UID)"
        Me.Xr_Cen_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'Xr_As_On
        '
        Me.Xr_As_On.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.Xr_As_On.BorderColor = System.Drawing.Color.Empty
        Me.Xr_As_On.CanShrink = True
        Me.Xr_As_On.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_As_On.ForeColor = System.Drawing.Color.Black
        Me.Xr_As_On.LocationFloat = New DevExpress.Utils.PointFloat(474.6666!, 32.99999!)
        Me.Xr_As_On.Name = "Xr_As_On"
        Me.Xr_As_On.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_As_On.SizeF = New System.Drawing.SizeF(262.3335!, 21.0!)
        Me.Xr_As_On.StylePriority.UseBorderColor = False
        Me.Xr_As_On.StylePriority.UseFont = False
        Me.Xr_As_On.StylePriority.UseForeColor = False
        Me.Xr_As_On.StylePriority.UsePadding = False
        Me.Xr_As_On.StylePriority.UseTextAlignment = False
        Me.Xr_As_On.Text = "Detail as on 31 March, 20XXg"
        Me.Xr_As_On.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLbl_Zone
        '
        Me.XrLbl_Zone.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrLbl_Zone.AutoWidth = True
        Me.XrLbl_Zone.CanShrink = True
        Me.XrLbl_Zone.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLbl_Zone.ForeColor = System.Drawing.Color.Black
        Me.XrLbl_Zone.LocationFloat = New DevExpress.Utils.PointFloat(364.0833!, 32.99999!)
        Me.XrLbl_Zone.Name = "XrLbl_Zone"
        Me.XrLbl_Zone.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLbl_Zone.SizeF = New System.Drawing.SizeF(110.5833!, 21.0!)
        Me.XrLbl_Zone.StylePriority.UseFont = False
        Me.XrLbl_Zone.StylePriority.UseForeColor = False
        Me.XrLbl_Zone.StylePriority.UsePadding = False
        Me.XrLbl_Zone.StylePriority.UseTextAlignment = False
        Me.XrLbl_Zone.Text = "XrLbl_Zone"
        Me.XrLbl_Zone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLbl_stmtName
        '
        Me.XrLbl_stmtName.AnchorVertical = CType((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top Or DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom), DevExpress.XtraReports.UI.VerticalAnchorStyles)
        Me.XrLbl_stmtName.AutoWidth = True
        Me.XrLbl_stmtName.CanGrow = False
        Me.XrLbl_stmtName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLbl_stmtName.KeepTogether = True
        Me.XrLbl_stmtName.LocationFloat = New DevExpress.Utils.PointFloat(1.041698!, 77.29168!)
        Me.XrLbl_stmtName.Multiline = True
        Me.XrLbl_stmtName.Name = "XrLbl_stmtName"
        Me.XrLbl_stmtName.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrLbl_stmtName.SizeF = New System.Drawing.SizeF(745.9584!, 23.50004!)
        Me.XrLbl_stmtName.StylePriority.UseFont = False
        Me.XrLbl_stmtName.StylePriority.UsePadding = False
        Me.XrLbl_stmtName.StylePriority.UseTextAlignment = False
        Me.XrLbl_stmtName.Text = "XrLbl_StatementName"
        Me.XrLbl_stmtName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'WinControlContainer1
        '
        Me.WinControlContainer1.LocationFloat = New DevExpress.Utils.PointFloat(603.25!, 64.41666!)
        Me.WinControlContainer1.Name = "WinControlContainer1"
        Me.WinControlContainer1.SizeF = New System.Drawing.SizeF(142.7083!, 68.25004!)
        Me.WinControlContainer1.WinControl = Me.BarCode
        '
        'BarCode
        '
        Me.BarCode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarCode.HorizontalAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BarCode.HorizontalTextAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BarCode.Location = New System.Drawing.Point(0, 0)
        Me.BarCode.Name = "BarCode"
        Me.BarCode.Padding = New System.Windows.Forms.Padding(10, 2, 10, 0)
        Me.BarCode.ShowText = False
        Me.BarCode.Size = New System.Drawing.Size(137, 66)
        QrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.[Byte]
        QrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version1
        Me.BarCode.Symbology = QrCodeGenerator1
        Me.BarCode.TabIndex = 0
        Me.BarCode.Text = "omshanti"
        '
        'XrLbl_City
        '
        Me.XrLbl_City.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrLbl_City.AutoWidth = True
        Me.XrLbl_City.CanGrow = False
        Me.XrLbl_City.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLbl_City.LocationFloat = New DevExpress.Utils.PointFloat(65.87598!, 42.45828!)
        Me.XrLbl_City.Name = "XrLbl_City"
        Me.XrLbl_City.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLbl_City.SizeF = New System.Drawing.SizeF(151.0417!, 33.50001!)
        Me.XrLbl_City.StylePriority.UseFont = False
        Me.XrLbl_City.StylePriority.UsePadding = False
        Me.XrLbl_City.StylePriority.UseTextAlignment = False
        Me.XrLbl_City.Text = "XrLbl_City"
        Me.XrLbl_City.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.Xr_Version, Me.XrPictureBox1, Me.XrPageInfo1, Me.XrLinefooter})
        Me.BottomMargin.HeightF = 49.16661!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Format = "Printed On: {0:dd-MMM, yyyy}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(245.4308!, 4.000028!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(173.3192!, 30.0!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Xr_Version
        '
        Me.Xr_Version.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_Version.LocationFloat = New DevExpress.Utils.PointFloat(159.375!, 15.0!)
        Me.Xr_Version.Name = "Xr_Version"
        Me.Xr_Version.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Version.SizeF = New System.Drawing.SizeF(86.05582!, 11.0!)
        Me.Xr_Version.StylePriority.UseFont = False
        Me.Xr_Version.Text = "Ver. 2.0"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Image = CType(resources.GetObject("XrPictureBox1.Image"), System.Drawing.Image)
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(1.041698!, 2.500026!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(158.3333!, 30.0!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(663.6667!, 12.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(83.33333!, 13.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLinefooter
        '
        Me.XrLinefooter.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot
        Me.XrLinefooter.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLinefooter.Name = "XrLinefooter"
        Me.XrLinefooter.SizeF = New System.Drawing.SizeF(747.0001!, 2.083333!)
        '
        'XrPanel1
        '
        Me.XrPanel1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrPanel1.CanGrow = False
        Me.XrPanel1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.XrLabel1, Me.XrLabel4, Me.XrLabel10, Me.XrLbl_City, Me.XrPageInfo3})
        Me.XrPanel1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrPanel1.Name = "XrPanel1"
        Me.XrPanel1.SizeF = New System.Drawing.SizeF(747.0001!, 76.95831!)
        '
        'XrLine2
        '
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(517.0416!, 42.4583!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(219.9585!, 2.0!)
        '
        'XrLabel1
        '
        Me.XrLabel1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrLabel1.CanGrow = False
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel1.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLabel1.KeepTogether = True
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(15.87596!, 10.95828!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(50.00002!, 33.50001!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseForeColor = False
        Me.XrLabel1.StylePriority.UsePadding = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Date :"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrLabel4.CanGrow = False
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel4.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLabel4.KeepTogether = True
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(15.87596!, 42.45831!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(50.0!, 33.5!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseForeColor = False
        Me.XrLabel4.StylePriority.UsePadding = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "Place :"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrLabel10.CanGrow = False
        Me.XrLabel10.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel10.ForeColor = System.Drawing.Color.DarkRed
        Me.XrLabel10.KeepTogether = True
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(517.0416!, 44.45828!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 5, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(219.9584!, 31.50005!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseForeColor = False
        Me.XrLabel10.StylePriority.UsePadding = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "Signature of Centre In - charge"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo3.Format = "{0:dd-MMM, yyyy}"
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(65.87598!, 12.45827!)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo3.SizeF = New System.Drawing.SizeF(133.3334!, 30.0!)
        Me.XrPageInfo3.StylePriority.UseFont = False
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
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
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLinebottom})
        Me.GroupFooter1.Expanded = False
        Me.GroupFooter1.HeightF = 2.416611!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLinebottom
        '
        Me.XrLinebottom.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot
        Me.XrLinebottom.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLinebottom.Name = "XrLinebottom"
        Me.XrLinebottom.SizeF = New System.Drawing.SizeF(747.0!, 2.416611!)
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTopMarginPanel})
        Me.ReportHeader.HeightF = 132.6667!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.HeightF = 0.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrHeaderPanel
        '
        Me.XrHeaderPanel.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrHeaderPanel.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrHeaderPanel.Name = "XrHeaderPanel"
        Me.XrHeaderPanel.SizeF = New System.Drawing.SizeF(747.0001!, 17.70834!)
        Me.XrHeaderPanel.StylePriority.UseBorders = False
        Me.XrHeaderPanel.Visible = False
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.Xr_Incharge, Me.XrPanel1})
        Me.ReportFooter.HeightF = 104.8751!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'Xr_Incharge
        '
        Me.Xr_Incharge.BorderColor = System.Drawing.Color.Empty
        Me.Xr_Incharge.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Xr_Incharge.ForeColor = System.Drawing.Color.Black
        Me.Xr_Incharge.LocationFloat = New DevExpress.Utils.PointFloat(517.0414!, 76.95831!)
        Me.Xr_Incharge.Name = "Xr_Incharge"
        Me.Xr_Incharge.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Xr_Incharge.SizeF = New System.Drawing.SizeF(219.9586!, 18.00002!)
        Me.Xr_Incharge.StylePriority.UseBorderColor = False
        Me.Xr_Incharge.StylePriority.UseFont = False
        Me.Xr_Incharge.StylePriority.UseForeColor = False
        Me.Xr_Incharge.StylePriority.UseTextAlignment = False
        Me.Xr_Incharge.Text = "Name:"
        Me.Xr_Incharge.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'PageFooter
        '
        Me.PageFooter.HeightF = 0.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrHeaderPanel})
        Me.PageHeader.HeightF = 17.70834!
        Me.PageHeader.Name = "PageHeader"
        '
        'Report_Potamel
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupFooter1, Me.ReportHeader, Me.GroupHeader1, Me.ReportFooter, Me.PageFooter, Me.PageHeader})
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FinalRow, Me.TableOuterBorder})
        Me.Margins = New System.Drawing.Printing.Margins(34, 46, 49, 49)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ShowPrintMarginsWarning = False
        Me.Version = "13.2"
        CType(Me.XrTopMarginPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPanel1 As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrTopMarginPanel As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Xr_Ins_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Xr_Cen_Name As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Xr_As_On As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_Zone As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_City As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLbl_stmtName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrHeaderPanel As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLinefooter As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents FinalRow As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrLinebottom As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents TableOuterBorder As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents Xr_Version As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents Xr_Printable As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents BarCode As DevExpress.XtraEditors.BarCodeControl
    Friend WithEvents Xr_Incharge As DevExpress.XtraReports.UI.XRLabel
End Class
