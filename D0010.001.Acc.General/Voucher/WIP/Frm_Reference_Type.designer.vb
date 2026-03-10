<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Reference_Type
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Reference_Type))
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_OK = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioGroup1 = New DevExpress.XtraEditors.RadioGroup()
        Me.Panel1.SuspendLayout()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Lbl_Separator2
        '
        Me.Lbl_Separator2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.Lbl_Separator2.LineVisible = True
        Me.Lbl_Separator2.Location = New System.Drawing.Point(-2, 116)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(383, 1)
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
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
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
        Me.Panel1.Size = New System.Drawing.Size(378, 34)
        Me.Panel1.TabIndex = 55
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
        Me.Txt_TitleX.Size = New System.Drawing.Size(119, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Select Type"
        '
        'BUT_OK
        '
        Me.BUT_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OK.Image = CType(resources.GetObject("BUT_OK.Image"), System.Drawing.Image)
        Me.BUT_OK.Location = New System.Drawing.Point(237, 119)
        Me.BUT_OK.Name = "BUT_OK"
        Me.BUT_OK.Size = New System.Drawing.Size(70, 28)
        Me.BUT_OK.TabIndex = 9
        Me.BUT_OK.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(307, 119)
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 28)
        Me.BUT_CANCEL.TabIndex = 10
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'RadioGroup1
        '
        Me.RadioGroup1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioGroup1.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioGroup1.EditValue = "Existing Reference"
        Me.RadioGroup1.Location = New System.Drawing.Point(3, 38)
        Me.RadioGroup1.Name = "RadioGroup1"
        Me.RadioGroup1.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioGroup1.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.RadioGroup1.Properties.Appearance.Options.UseFont = True
        Me.RadioGroup1.Properties.Appearance.Options.UseForeColor = True
        Me.RadioGroup1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.AliceBlue
        Me.RadioGroup1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.RadioGroup1.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.RadioGroup1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.RadioGroup1.Properties.AppearanceFocused.Options.UseFont = True
        Me.RadioGroup1.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.RadioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("Existing Reference", "&1. Add Value to a Existing WIP Reference"), New DevExpress.XtraEditors.Controls.RadioGroupItem("New Reference", "&2. Create a New WIP Reference")})
        Me.RadioGroup1.Properties.LookAndFeel.SkinName = "McSkin"
        Me.RadioGroup1.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.RadioGroup1.Size = New System.Drawing.Size(373, 75)
        Me.RadioGroup1.TabIndex = 0
        '
        'Frm_Reference_Type
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 148)
        Me.Controls.Add(Me.RadioGroup1)
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
        Me.Name = "Frm_Reference_Type"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reference Type"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_OK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents RadioGroup1 As DevExpress.XtraEditors.RadioGroup

End Class
