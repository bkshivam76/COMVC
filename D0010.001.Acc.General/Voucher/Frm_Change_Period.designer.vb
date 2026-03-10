<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Change_Period
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Change_Period))
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_OK = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.Txt_Fr_Date = New DevExpress.XtraEditors.DateEdit()
        Me.Txt_To_Date = New DevExpress.XtraEditors.DateEdit()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.Txt_Fr_Date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_To_Date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_To_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Lbl_Separator2
        '
        Me.Lbl_Separator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.Lbl_Separator2.LineVisible = True
        Me.Lbl_Separator2.Location = New System.Drawing.Point(0, 81)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(450, 1)
        Me.Lbl_Separator2.TabIndex = 143
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(0, 34)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(450, 1)
        Me.LabelControl1.TabIndex = 144
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(265, 34)
        Me.Panel1.TabIndex = 55
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 32)
        Me.Panel2.TabIndex = 6
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(40, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(151, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Change Period"
        '
        'BUT_OK
        '
        Me.BUT_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OK.Image = CType(resources.GetObject("BUT_OK.Image"), System.Drawing.Image)
        Me.BUT_OK.Location = New System.Drawing.Point(124, 85)
        Me.BUT_OK.Name = "BUT_OK"
        Me.BUT_OK.Size = New System.Drawing.Size(70, 28)
        Me.BUT_OK.TabIndex = 3
        Me.BUT_OK.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(194, 85)
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 28)
        Me.BUT_CANCEL.TabIndex = 4
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(5, 38)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl6.TabIndex = 172
        Me.LabelControl6.Text = "From:"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(136, 38)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(20, 14)
        Me.LabelControl5.TabIndex = 173
        Me.LabelControl5.Text = "To:"
        '
        'Txt_Fr_Date
        '
        Me.Txt_Fr_Date.EditValue = Nothing
        Me.Txt_Fr_Date.EnterMoveNextControl = True
        Me.Txt_Fr_Date.Location = New System.Drawing.Point(5, 55)
        Me.Txt_Fr_Date.Name = "Txt_Fr_Date"
        Me.Txt_Fr_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Fr_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Navy
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Txt_Fr_Date.Properties.LookAndFeel.SkinName = "Liquid Sky"
        Me.Txt_Fr_Date.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_Fr_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_Fr_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_Fr_Date.Properties.NullDate = ""
        Me.Txt_Fr_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_Fr_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_Fr_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_Fr_Date.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[False]
        Me.Txt_Fr_Date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Txt_Fr_Date.Size = New System.Drawing.Size(125, 20)
        Me.Txt_Fr_Date.TabIndex = 0
        '
        'Txt_To_Date
        '
        Me.Txt_To_Date.EditValue = Nothing
        Me.Txt_To_Date.EnterMoveNextControl = True
        Me.Txt_To_Date.Location = New System.Drawing.Point(136, 55)
        Me.Txt_To_Date.Name = "Txt_To_Date"
        Me.Txt_To_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_To_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_To_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_To_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Navy
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Txt_To_Date.Properties.LookAndFeel.SkinName = "Liquid Sky"
        Me.Txt_To_Date.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_To_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_To_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_To_Date.Properties.NullDate = ""
        Me.Txt_To_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_To_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_To_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_To_Date.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[False]
        Me.Txt_To_Date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Txt_To_Date.Size = New System.Drawing.Size(125, 20)
        Me.Txt_To_Date.TabIndex = 1
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Frm_Change_Period
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(265, 114)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.Txt_Fr_Date)
        Me.Controls.Add(Me.Txt_To_Date)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BUT_OK)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Change_Period"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Period"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Txt_Fr_Date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_To_Date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_To_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_OK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Txt_Fr_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Txt_To_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
