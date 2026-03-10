<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Prompt_Window
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        isDisposed = True
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Prompt_Window))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BUT_SAVE = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Message = New DevExpress.XtraEditors.LabelControl()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.BUT_SAVE, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BUT_CANCEL, 1, 0)
        Me.TableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 80)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(393, 32)
        Me.TableLayoutPanel1.TabIndex = 178
        '
        'BUT_SAVE
        '
        Me.BUT_SAVE.Appearance.Options.UseFont = True
        Me.BUT_SAVE.Cursor = System.Windows.Forms.Cursors.Default
        Me.BUT_SAVE.Dock = System.Windows.Forms.DockStyle.Right
        Me.BUT_SAVE.Image = CType(resources.GetObject("BUT_SAVE.Image"), System.Drawing.Image)
        Me.BUT_SAVE.Location = New System.Drawing.Point(244, 3)
        Me.BUT_SAVE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_SAVE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SAVE.Name = "BUT_SAVE"
        Me.BUT_SAVE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_SAVE.TabIndex = 0
        Me.BUT_SAVE.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Dock = System.Windows.Forms.DockStyle.Right
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(320, 3)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 26)
        Me.BUT_CANCEL.TabIndex = 1
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Top
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(1, 79)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(399, 4)
        Me.LabelControl1.TabIndex = 179
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_Message, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.PictureBox1, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(393, 77)
        Me.TableLayoutPanel2.TabIndex = 180
        '
        'lbl_Message
        '
        Me.lbl_Message.AllowHtmlString = True
        Me.lbl_Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Message.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Message.Appearance.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.lbl_Message.Appearance.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.lbl_Message.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.lbl_Message.Location = New System.Drawing.Point(43, 3)
        Me.lbl_Message.MinimumSize = New System.Drawing.Size(0, 70)
        Me.lbl_Message.Name = "lbl_Message"
        Me.lbl_Message.Size = New System.Drawing.Size(347, 70)
        Me.lbl_Message.TabIndex = 181
        Me.lbl_Message.Text = "<color=red><b><i>Test</i></b></color><color=255,55, 228><b> Message</b></color>: " & _
    "<u><size=18>ABC</size></u>"
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Image = Global.Common_Lib.My.Resources.Resources.Info
        Me.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox1.Location = New System.Drawing.Point(5, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 71)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Prompt_Window
        '
        Me.AccessibleDescription = "Prompt_Window"
        Me.AccessibleName = "Prompt_Window"
        Me.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile
        Me.BackgroundImageStore = CType(resources.GetObject("$this.BackgroundImageStore"), System.Drawing.Image)
        Me.CancelButton = Me.BUT_CANCEL
        Me.ClientSize = New System.Drawing.Size(393, 112)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Prompt_Window"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Connect One"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DevExpress.Skins.SkinManager.EnableFormSkins()
        For Each ctrl As System.Windows.Forms.Control In Me.Controls
            ctrl.AccessibleDescription = ctrl.Name
        Next
    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BUT_SAVE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_Message As DevExpress.XtraEditors.LabelControl
End Class
