<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIP_Report
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIP_Report))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_CHANGE = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tool_Sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.Hyper_Request = New DevExpress.XtraEditors.LabelControl()
        Me.PivotGridControl1 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me._Month = New DevExpress.XtraPivotGrid.PivotGridField()
        Me._Amt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me._Items = New DevExpress.XtraPivotGrid.PivotGridField()
        Me._Ledger = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(711, 4)
        Me.BUT_CLOSE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_CLOSE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CLOSE.Name = "BUT_CLOSE"
        Me.BUT_CLOSE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_CLOSE.TabIndex = 151
        Me.BUT_CLOSE.Text = "Close"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(617, 4)
        Me.BUT_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_PRINT.Name = "BUT_PRINT"
        Me.BUT_PRINT.Size = New System.Drawing.Size(95, 26)
        Me.BUT_PRINT.TabIndex = 150
        Me.BUT_PRINT.Text = "List Preview"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel2.Location = New System.Drawing.Point(2, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 32)
        Me.Panel2.TabIndex = 46
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(40, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(254, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Work In Progress Details"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_CHANGE, Me.ToolStripMenuItem2, Me.T_Print, Me.Tool_Sep2, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 110)
        '
        'T_CHANGE
        '
        Me.T_CHANGE.Enabled = False
        Me.T_CHANGE.Image = CType(resources.GetObject("T_CHANGE.Image"), System.Drawing.Image)
        Me.T_CHANGE.Name = "T_CHANGE"
        Me.T_CHANGE.Size = New System.Drawing.Size(176, 22)
        Me.T_CHANGE.Text = "Change &Matching"
        Me.T_CHANGE.Visible = False
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(173, 6)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(176, 22)
        Me.T_Print.Text = "&List Preview"
        '
        'Tool_Sep2
        '
        Me.Tool_Sep2.Name = "Tool_Sep2"
        Me.Tool_Sep2.Size = New System.Drawing.Size(173, 6)
        '
        'T_Refresh
        '
        Me.T_Refresh.Image = CType(resources.GetObject("T_Refresh.Image"), System.Drawing.Image)
        Me.T_Refresh.Name = "T_Refresh"
        Me.T_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.T_Refresh.Size = New System.Drawing.Size(176, 22)
        Me.T_Refresh.Text = "&Refresh"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(173, 6)
        '
        'T_Close
        '
        Me.T_Close.Image = CType(resources.GetObject("T_Close.Image"), System.Drawing.Image)
        Me.T_Close.Name = "T_Close"
        Me.T_Close.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.T_Close.Size = New System.Drawing.Size(176, 22)
        Me.T_Close.Text = "&Close"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(701, 333)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(80, 27)
        Me.BUT_CANCEL.TabIndex = 3
        Me.BUT_CANCEL.Text = "Close"
        '
        'Hyper_Request
        '
        Me.Hyper_Request.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Hyper_Request.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hyper_Request.Appearance.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Hyper_Request.Appearance.Image = CType(resources.GetObject("Hyper_Request.Appearance.Image"), System.Drawing.Image)
        Me.Hyper_Request.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hyper_Request.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.Hyper_Request.Location = New System.Drawing.Point(0, 336)
        Me.Hyper_Request.LookAndFeel.SkinName = "Liquid Sky"
        Me.Hyper_Request.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Hyper_Request.Name = "Hyper_Request"
        Me.Hyper_Request.Size = New System.Drawing.Size(228, 20)
        ToolTipTitleItem1.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem1.Appearance.Options.UseImage = True
        ToolTipTitleItem1.Image = CType(resources.GetObject("ToolTipTitleItem1.Image"), System.Drawing.Image)
        ToolTipTitleItem1.Text = "Note..."
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "If you do not find or need any new information in this Window then click here to " & _
    "send your request to Madhuban."
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.Hyper_Request.SuperTip = SuperToolTip1
        Me.Hyper_Request.TabIndex = 113
        Me.Hyper_Request.Text = "Send Your Request to Madhuban"
        '
        'PivotGridControl1
        '
        Me.PivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PivotGridControl1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me._Month, Me._Amt, Me._Items, Me._Ledger})
        Me.PivotGridControl1.Location = New System.Drawing.Point(0, 34)
        Me.PivotGridControl1.Name = "PivotGridControl1"
        Me.PivotGridControl1.Size = New System.Drawing.Size(784, 328)
        Me.PivotGridControl1.TabIndex = 149
        '
        '_Month
        '
        Me._Month.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me._Month.AreaIndex = 0
        Me._Month.Caption = "Month"
        Me._Month.FieldName = "Month"
        Me._Month.Name = "_Month"
        Me._Month.SortBySummaryInfo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom
        Me._Month.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
        '
        '_Amt
        '
        Me._Amt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me._Amt.AreaIndex = 0
        Me._Amt.Caption = "Amount"
        Me._Amt.CellFormat.FormatString = "#,0.00"
        Me._Amt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me._Amt.FieldName = "Amt"
        Me._Amt.Name = "_Amt"
        Me._Amt.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        '
        '_Items
        '
        Me._Items.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me._Items.AreaIndex = 1
        Me._Items.Caption = "Item"
        Me._Items.FieldName = "Items"
        Me._Items.Name = "_Items"
        Me._Items.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        Me._Items.Width = 175
        '
        '_Ledger
        '
        Me._Ledger.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me._Ledger.AreaIndex = 0
        Me._Ledger.Caption = "Ledger"
        Me._Ledger.FieldName = "Property"
        Me._Ledger.Name = "_Ledger"
        Me._Ledger.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        Me._Ledger.Width = 150
        '
        'WIP_Report
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BUT_CLOSE
        Me.ClientSize = New System.Drawing.Size(784, 362)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.PivotGridControl1)
        Me.Controls.Add(Me.Hyper_Request)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "WIP_Report"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Work In Progress Details"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents T_CHANGE As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Hyper_Request As DevExpress.XtraEditors.LabelControl
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tool_Sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PivotGridControl1 As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents _Month As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents _Amt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents _Items As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents _Ledger As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton


End Class
