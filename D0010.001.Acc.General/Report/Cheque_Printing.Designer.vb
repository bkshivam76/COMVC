<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Cheque_Printing
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrAmtInDecimal = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrAmountInWords = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPartyName = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrDate, Me.XrAmtInDecimal, Me.XrAmountInWords, Me.XrPartyName})
        Me.Detail.HeightF = 229.6251!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(241.0415!, 202.7918!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(99.58339!, 18.83334!)
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "A/c Payee"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrDate
        '
        Me.XrDate.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.XrDate.LocationFloat = New DevExpress.Utils.PointFloat(556.25!, 0!)
        Me.XrDate.Name = "XrDate"
        Me.XrDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrDate.SizeF = New System.Drawing.SizeF(103.75!, 25.08334!)
        Me.XrDate.StylePriority.UseFont = False
        Me.XrDate.StylePriority.UseTextAlignment = False
        Me.XrDate.Text = "XrDate"
        Me.XrDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrAmtInDecimal
        '
        Me.XrAmtInDecimal.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrAmtInDecimal.LocationFloat = New DevExpress.Utils.PointFloat(517.7083!, 106.7501!)
        Me.XrAmtInDecimal.Name = "XrAmtInDecimal"
        Me.XrAmtInDecimal.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrAmtInDecimal.SizeF = New System.Drawing.SizeF(142.2917!, 25.08334!)
        Me.XrAmtInDecimal.StylePriority.UseFont = False
        Me.XrAmtInDecimal.StylePriority.UseTextAlignment = False
        Me.XrAmtInDecimal.Text = "XrAmtInDecimal"
        Me.XrAmtInDecimal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrAmountInWords
        '
        Me.XrAmountInWords.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrAmountInWords.LocationFloat = New DevExpress.Utils.PointFloat(97.2915!, 82.66668!)
        Me.XrAmountInWords.Multiline = True
        Me.XrAmountInWords.Name = "XrAmountInWords"
        Me.XrAmountInWords.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrAmountInWords.SizeF = New System.Drawing.SizeF(420.4168!, 50.16674!)
        Me.XrAmountInWords.StylePriority.UseFont = False
        Me.XrAmountInWords.Text = "XrAmountInWords"
        '
        'XrPartyName
        '
        Me.XrPartyName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPartyName.LocationFloat = New DevExpress.Utils.PointFloat(0!, 38.08334!)
        Me.XrPartyName.Name = "XrPartyName"
        Me.XrPartyName.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPartyName.SizeF = New System.Drawing.SizeF(610.0001!, 25.08333!)
        Me.XrPartyName.StylePriority.UseFont = False
        Me.XrPartyName.Text = "XrPartyName"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 28.70833!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Cheque_Printing
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Margins = New System.Drawing.Printing.Margins(100, 50, 29, 100)
        Me.PageHeight = 360
        Me.PageWidth = 810
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "13.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrPartyName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrAmtInDecimal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrAmountInWords As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
End Class
