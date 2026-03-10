<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Purpose_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Purpose_Report))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.PivotGridControl1 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.Ledger_f = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.Item_f = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.Purpose_f = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.Date_f = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.Amount_f = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.BackgroundImage = CType(resources.GetObject("StatusStrip1.BackgroundImage"), System.Drawing.Image)
        Me.StatusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.GripMargin = New System.Windows.Forms.Padding(0)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Lbl_ToolMsg})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 434)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(774, 18)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 33
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Lbl_ToolMsg
        '
        Me.Lbl_ToolMsg.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_ToolMsg.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_ToolMsg.ForeColor = System.Drawing.Color.Black
        Me.Lbl_ToolMsg.Name = "Lbl_ToolMsg"
        Me.Lbl_ToolMsg.Size = New System.Drawing.Size(759, 13)
        Me.Lbl_ToolMsg.Spring = True
        Me.Lbl_ToolMsg.Text = "Ready"
        Me.Lbl_ToolMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(774, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(701, 3)
        Me.BUT_CLOSE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_CLOSE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CLOSE.Name = "BUT_CLOSE"
        Me.BUT_CLOSE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_CLOSE.TabIndex = 9
        Me.BUT_CLOSE.Text = "Close"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(607, 3)
        Me.BUT_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_PRINT.Name = "BUT_PRINT"
        Me.BUT_PRINT.Size = New System.Drawing.Size(95, 26)
        Me.BUT_PRINT.TabIndex = 8
        Me.BUT_PRINT.Text = "List Preview"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(160, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Purpose Report"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_Print, Me.ToolStripMenuItem1, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 82)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(176, 22)
        Me.T_Print.Text = "&List Preview"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(173, 6)
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
        'PivotGridControl1
        '
        Me.PivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PivotGridControl1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.Ledger_f, Me.Item_f, Me.Purpose_f, Me.Date_f, Me.Amount_f})
        Me.PivotGridControl1.Location = New System.Drawing.Point(0, 34)
        Me.PivotGridControl1.Name = "PivotGridControl1"
        Me.PivotGridControl1.Size = New System.Drawing.Size(774, 400)
        Me.PivotGridControl1.TabIndex = 148
        '
        'Ledger_f
        '
        Me.Ledger_f.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.Ledger_f.AreaIndex = 0
        Me.Ledger_f.Caption = "Ledger"
        Me.Ledger_f.FieldName = "Ledger"
        Me.Ledger_f.Name = "Ledger_f"
        Me.Ledger_f.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        Me.Ledger_f.Width = 150
        '
        'Item_f
        '
        Me.Item_f.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.Item_f.AreaIndex = 1
        Me.Item_f.Caption = "Item"
        Me.Item_f.FieldName = "Item"
        Me.Item_f.Name = "Item_f"
        Me.Item_f.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        Me.Item_f.Width = 200
        '
        'Purpose_f
        '
        Me.Purpose_f.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.Purpose_f.AreaIndex = 0
        Me.Purpose_f.Caption = "Purpose"
        Me.Purpose_f.FieldName = "Purpose"
        Me.Purpose_f.Name = "Purpose_f"
        Me.Purpose_f.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        '
        'Date_f
        '
        Me.Date_f.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.Date_f.AreaIndex = 2
        Me.Date_f.Caption = "Date"
        Me.Date_f.FieldName = "Date"
        Me.Date_f.Name = "Date_f"
        Me.Date_f.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        '
        'Amount_f
        '
        Me.Amount_f.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.Amount_f.AreaIndex = 0
        Me.Amount_f.Caption = "Amount"
        Me.Amount_f.CellFormat.FormatString = "#,0.00"
        Me.Amount_f.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Amount_f.FieldName = "Amount"
        Me.Amount_f.Name = "Amount_f"
        Me.Amount_f.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        '
        'Frm_Purpose_Report
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 452)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.PivotGridControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "Frm_Purpose_Report"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purpose Report"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Lbl_ToolMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PivotGridControl1 As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents Ledger_f As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents Item_f As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents Purpose_f As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents Date_f As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents Amount_f As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton


End Class
