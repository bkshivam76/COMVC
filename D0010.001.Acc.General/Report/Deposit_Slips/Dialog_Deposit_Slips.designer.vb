<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog_Deposit_Slips
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog_Deposit_Slips))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TopLine = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_SAVE_COM = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BE_Slip_No = New DevExpress.XtraEditors.ButtonEdit()
        Me.Txt_Dep_Date = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BE_Bank_Acc = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TopPanel.SuspendLayout()
        CType(Me.BE_Slip_No.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Dep_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Dep_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Bank_Acc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TopLine
        '
        Me.TopLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopLine.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TopLine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TopLine.LineVisible = True
        Me.TopLine.Location = New System.Drawing.Point(0, 32)
        Me.TopLine.LookAndFeel.SkinName = "Liquid Sky"
        Me.TopLine.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TopLine.Name = "TopLine"
        Me.TopLine.Size = New System.Drawing.Size(287, 3)
        Me.TopLine.TabIndex = 47
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(3, 42)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(52, 14)
        Me.LabelControl6.TabIndex = 187
        Me.LabelControl6.Text = "Slip No.:"
        '
        'Lbl_Separator2
        '
        Me.Lbl_Separator2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.Lbl_Separator2.LineVisible = True
        Me.Lbl_Separator2.Location = New System.Drawing.Point(0, 106)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(383, 10)
        Me.Lbl_Separator2.TabIndex = 188
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Tan
        Me.TopPanel.BackgroundImage = CType(resources.GetObject("TopPanel.BackgroundImage"), System.Drawing.Image)
        Me.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Controls.Add(Me.TitleX)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(288, 34)
        Me.TopPanel.TabIndex = 33
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel2.Location = New System.Drawing.Point(3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(30, 30)
        Me.Panel2.TabIndex = 45
        '
        'TitleX
        '
        Me.TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.TitleX.Location = New System.Drawing.Point(40, 4)
        Me.TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TitleX.Name = "TitleX"
        Me.TitleX.Size = New System.Drawing.Size(120, 26)
        Me.TitleX.TabIndex = 5
        Me.TitleX.Text = "Deposit Slip"
        '
        'BUT_SAVE_COM
        '
        Me.BUT_SAVE_COM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SAVE_COM.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SAVE_COM.Appearance.Options.UseFont = True
        Me.BUT_SAVE_COM.Image = CType(resources.GetObject("BUT_SAVE_COM.Image"), System.Drawing.Image)
        Me.BUT_SAVE_COM.Location = New System.Drawing.Point(132, 118)
        Me.BUT_SAVE_COM.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_SAVE_COM.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SAVE_COM.Name = "BUT_SAVE_COM"
        Me.BUT_SAVE_COM.Size = New System.Drawing.Size(73, 27)
        Me.BUT_SAVE_COM.TabIndex = 8
        Me.BUT_SAVE_COM.Text = "Show"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(206, 118)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(79, 27)
        Me.BUT_CANCEL.TabIndex = 9
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'BE_Slip_No
        '
        Me.BE_Slip_No.EditValue = ""
        Me.BE_Slip_No.EnterMoveNextControl = True
        Me.BE_Slip_No.Location = New System.Drawing.Point(100, 40)
        Me.BE_Slip_No.Name = "BE_Slip_No"
        Me.BE_Slip_No.Properties.AllowFocused = False
        Me.BE_Slip_No.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Slip_No.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Slip_No.Properties.Appearance.Options.UseFont = True
        Me.BE_Slip_No.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Slip_No.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Slip_No.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Slip_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.BE_Slip_No.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Slip_No.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Slip_No.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Slip_No.Properties.Mask.EditMask = "d"
        Me.BE_Slip_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.BE_Slip_No.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Slip_No.Size = New System.Drawing.Size(185, 20)
        Me.BE_Slip_No.TabIndex = 182
        '
        'Txt_Dep_Date
        '
        Me.Txt_Dep_Date.EditValue = ""
        Me.Txt_Dep_Date.EnterMoveNextControl = True
        Me.Txt_Dep_Date.Location = New System.Drawing.Point(100, 86)
        Me.Txt_Dep_Date.Name = "Txt_Dep_Date"
        Me.Txt_Dep_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Dep_Date.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_Dep_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_Dep_Date.Properties.Appearance.Options.UseForeColor = True
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_Dep_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_Dep_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_Dep_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Dep_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.Txt_Dep_Date.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_Dep_Date.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_Dep_Date.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_Dep_Date.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_Dep_Date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Txt_Dep_Date.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[False]
        Me.Txt_Dep_Date.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Txt_Dep_Date.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.Txt_Dep_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_Dep_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_Dep_Date.Properties.NullDate = ""
        Me.Txt_Dep_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_Dep_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_Dep_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_Dep_Date.Size = New System.Drawing.Size(185, 20)
        Me.Txt_Dep_Date.TabIndex = 189
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(3, 89)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(91, 14)
        Me.LabelControl1.TabIndex = 187
        Me.LabelControl1.Text = "Deposit Date.:"
        '
        'BE_Bank_Acc
        '
        Me.BE_Bank_Acc.EditValue = ""
        Me.BE_Bank_Acc.EnterMoveNextControl = True
        Me.BE_Bank_Acc.Location = New System.Drawing.Point(100, 63)
        Me.BE_Bank_Acc.Name = "BE_Bank_Acc"
        Me.BE_Bank_Acc.Properties.AllowFocused = False
        Me.BE_Bank_Acc.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Acc.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Bank_Acc.Properties.Appearance.Options.UseFont = True
        Me.BE_Bank_Acc.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Bank_Acc.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Bank_Acc.Properties.Mask.EditMask = "d"
        Me.BE_Bank_Acc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.BE_Bank_Acc.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Bank_Acc.Size = New System.Drawing.Size(185, 20)
        Me.BE_Bank_Acc.TabIndex = 182
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(3, 65)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(64, 14)
        Me.LabelControl2.TabIndex = 187
        Me.LabelControl2.Text = "Bank Acc :"
        '
        'Dialog_Deposit_Slips
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(288, 147)
        Me.Controls.Add(Me.Txt_Dep_Date)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.BE_Bank_Acc)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.BE_Slip_No)
        Me.Controls.Add(Me.BUT_SAVE_COM)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.TopLine)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dialog_Deposit_Slips"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Deposit Slip"
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        CType(Me.BE_Slip_No.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Dep_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Dep_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Bank_Acc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_SAVE_COM As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TopLine As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Public WithEvents BE_Slip_No As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Txt_Dep_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents BE_Bank_Acc As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
