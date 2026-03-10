<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ReportInput
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ReportInput))
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BE_View_Period = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.Cmb_View = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_OK = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelReport = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_ReportTitle = New DevExpress.XtraEditors.LabelControl()
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Top
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(-1, 34)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(481, 4)
        Me.LabelControl1.TabIndex = 187
        '
        'BE_View_Period
        '
        Me.BE_View_Period.EditValue = "View Period"
        Me.BE_View_Period.EnterMoveNextControl = True
        Me.BE_View_Period.Location = New System.Drawing.Point(216, 57)
        Me.BE_View_Period.Name = "BE_View_Period"
        Me.BE_View_Period.Properties.AllowFocused = False
        Me.BE_View_Period.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BE_View_Period.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_View_Period.Properties.Appearance.Options.UseFont = True
        Me.BE_View_Period.Properties.Appearance.Options.UseForeColor = True
        Me.BE_View_Period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_View_Period.Size = New System.Drawing.Size(260, 20)
        Me.BE_View_Period.TabIndex = 179
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(216, 41)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(45, 14)
        Me.LabelControl3.TabIndex = 181
        Me.LabelControl3.Text = "Period:"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(3, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(69, 14)
        Me.LabelControl2.TabIndex = 180
        Me.LabelControl2.Text = "View Type:"
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
        Me.Lbl_Separator2.Size = New System.Drawing.Size(575, 1)
        Me.Lbl_Separator2.TabIndex = 190
        '
        'Cmb_View
        '
        Me.Cmb_View.EditValue = ""
        Me.Cmb_View.EnterMoveNextControl = True
        Me.Cmb_View.Location = New System.Drawing.Point(3, 57)
        Me.Cmb_View.Name = "Cmb_View"
        Me.Cmb_View.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.Appearance.Options.UseFont = True
        Me.Cmb_View.Properties.Appearance.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Cmb_View.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray
        Me.Cmb_View.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.Cmb_View.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.Cmb_View.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceDropDown.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceDropDown.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Cmb_View.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceFocused.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Cmb_View.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem3.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem3.Appearance.Options.UseImage = True
        ToolTipTitleItem3.Image = CType(resources.GetObject("ToolTipTitleItem3.Image"), System.Drawing.Image)
        ToolTipTitleItem3.Text = "To set specific period..."
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "        Shortcut Key"
        ToolTipTitleItem4.LeftIndent = 6
        ToolTipTitleItem4.Text = "          Ctrl + F2"
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        SuperToolTip2.Items.Add(ToolTipItem2)
        SuperToolTip2.Items.Add(ToolTipTitleItem4)
        Me.Cmb_View.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Change Period", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, SuperToolTip2, True)})
        Me.Cmb_View.Properties.DropDownRows = 21
        Me.Cmb_View.Properties.ImmediatePopup = True
        Me.Cmb_View.Properties.LookAndFeel.SkinName = "Liquid Sky"
        Me.Cmb_View.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Cmb_View.Properties.NullValuePrompt = "Select Type..."
        Me.Cmb_View.Properties.NullValuePromptShowForEmptyValue = True
        Me.Cmb_View.Properties.PopupFormMinSize = New System.Drawing.Size(126, 0)
        Me.Cmb_View.Properties.PopupFormSize = New System.Drawing.Size(126, 0)
        Me.Cmb_View.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.Cmb_View.Size = New System.Drawing.Size(210, 20)
        Me.Cmb_View.TabIndex = 0
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(408, 85)
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 28)
        Me.BUT_CANCEL.TabIndex = 189
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'BUT_OK
        '
        Me.BUT_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OK.Image = CType(resources.GetObject("BUT_OK.Image"), System.Drawing.Image)
        Me.BUT_OK.Location = New System.Drawing.Point(338, 85)
        Me.BUT_OK.Name = "BUT_OK"
        Me.BUT_OK.Size = New System.Drawing.Size(70, 28)
        Me.BUT_OK.TabIndex = 188
        Me.BUT_OK.Text = "OK"
        '
        'PanelReport
        '
        Me.PanelReport.BackColor = System.Drawing.Color.Tan
        Me.PanelReport.BackgroundImage = CType(resources.GetObject("PanelReport.BackgroundImage"), System.Drawing.Image)
        Me.PanelReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelReport.Controls.Add(Me.Panel2)
        Me.PanelReport.Controls.Add(Me.Txt_ReportTitle)
        Me.PanelReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelReport.Location = New System.Drawing.Point(0, 0)
        Me.PanelReport.Name = "PanelReport"
        Me.PanelReport.Size = New System.Drawing.Size(479, 34)
        Me.PanelReport.TabIndex = 178
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel2.Location = New System.Drawing.Point(3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(30, 30)
        Me.Panel2.TabIndex = 46
        '
        'Txt_ReportTitle
        '
        Me.Txt_ReportTitle.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_ReportTitle.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_ReportTitle.Location = New System.Drawing.Point(40, 4)
        Me.Txt_ReportTitle.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_ReportTitle.Name = "Txt_ReportTitle"
        Me.Txt_ReportTitle.Size = New System.Drawing.Size(75, 26)
        Me.Txt_ReportTitle.TabIndex = 5
        Me.Txt_ReportTitle.Text = "Report "
        '
        'Frm_ReportInput
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 114)
        Me.Controls.Add(Me.Cmb_View)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.BE_View_Period)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.BUT_OK)
        Me.Controls.Add(Me.PanelReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_ReportInput"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Choose Period for the Report"
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelReport.ResumeLayout(False)
        Me.PanelReport.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BE_View_Period As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Txt_ReportTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelReport As System.Windows.Forms.Panel
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_OK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Cmb_View As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
