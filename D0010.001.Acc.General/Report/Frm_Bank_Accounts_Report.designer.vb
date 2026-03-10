<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bank_Accounts_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Bank_Accounts_Report))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_SHOW = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.But_Filter = New DevExpress.XtraEditors.SimpleButton()
        Me.But_Find = New DevExpress.XtraEditors.SimpleButton()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.BUT_SHOW)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Controls.Add(Me.LabelControl1)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(709, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_SHOW
        '
        Me.BUT_SHOW.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SHOW.Image = CType(resources.GetObject("BUT_SHOW.Image"), System.Drawing.Image)
        Me.BUT_SHOW.Location = New System.Drawing.Point(446, 5)
        Me.BUT_SHOW.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_SHOW.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SHOW.Name = "BUT_SHOW"
        Me.BUT_SHOW.Size = New System.Drawing.Size(95, 26)
        Me.BUT_SHOW.TabIndex = 261
        Me.BUT_SHOW.Text = "Show Details"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(541, 5)
        Me.BUT_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_PRINT.Name = "BUT_PRINT"
        Me.BUT_PRINT.Size = New System.Drawing.Size(95, 26)
        Me.BUT_PRINT.TabIndex = 261
        Me.BUT_PRINT.Text = "List Preview"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.LabelControl1.Location = New System.Drawing.Point(1, 3)
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(210, 26)
        Me.LabelControl1.TabIndex = 261
        Me.LabelControl1.Text = "Bank Account Listing"
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(636, 5)
        Me.BUT_CLOSE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_CLOSE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CLOSE.Name = "BUT_CLOSE"
        Me.BUT_CLOSE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_CLOSE.TabIndex = 257
        Me.BUT_CLOSE.Text = "Close"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(40, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(0, 26)
        Me.Txt_TitleX.TabIndex = 5
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(493, 4)
        Me.SimpleButton1.LookAndFeel.SkinName = "iMaginary"
        Me.SimpleButton1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(70, 26)
        Me.SimpleButton1.TabIndex = 8
        Me.SimpleButton1.Text = "Import"
        '
        'GridControl1
        '
        Me.GridControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.GridControl1.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Red
        Me.GridControl1.EmbeddedNavigator.Appearance.Options.UseBackColor = True
        Me.GridControl1.EmbeddedNavigator.Appearance.Options.UseForeColor = True
        Me.GridControl1.EmbeddedNavigator.Buttons.Append.Tag = "Append"
        Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.CancelEdit.Tag = "CancelEdit"
        Me.GridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.GridControl1.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.GridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.First.Tag = "First"
        Me.GridControl1.EmbeddedNavigator.Buttons.Last.Tag = "Last"
        Me.GridControl1.EmbeddedNavigator.Buttons.Next.Tag = "Next"
        Me.GridControl1.EmbeddedNavigator.Buttons.NextPage.Tag = "NextPage"
        Me.GridControl1.EmbeddedNavigator.Buttons.Prev.Tag = "Prev"
        Me.GridControl1.EmbeddedNavigator.Buttons.PrevPage.Tag = "PrevPage"
        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.GridControl1.EmbeddedNavigator.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridControl1.EmbeddedNavigator.CustomButtons.AddRange(New DevExpress.XtraEditors.NavigatorCustomButton() {New DevExpress.XtraEditors.NavigatorCustomButton(-1, 6, True, True, "Open Column Chooser", "OPEN_COL"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 8, True, True, "Show Group Box", "GROUP_BOX"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 14, True, True, "Show Grouped Column", "GROUPED_COL"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 18, True, True, "Show Footer Bar", "FOOTER_BAR"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 7, True, True, "Show Group Footer Bar", "GROUP_FOOTER"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 9, True, True, "Open Filter Builder", "FILTER")})
        Me.GridControl1.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
        Me.GridControl1.Location = New System.Drawing.Point(1, 35)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        Me.GridControl1.Size = New System.Drawing.Size(708, 226)
        Me.GridControl1.TabIndex = 257
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'GridView1
        '
        Me.GridView1.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.GridView1.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.GridView1.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.GridView1.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupFormat = "{1}"
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsDetail.AllowExpandEmptyDetails = True
        Me.GridView1.OptionsDetail.AllowZoomDetail = False
        Me.GridView1.OptionsDetail.AutoZoomDetail = True
        Me.GridView1.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled
        Me.GridView1.OptionsDetail.SmartDetailHeight = True
        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView1.OptionsPrint.PrintDetails = True
        Me.GridView1.OptionsSelection.InvertSelection = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.ViewCaption = "Group"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.GridControl1
        Me.GridView2.Name = "GridView2"
        '
        'But_Filter
        '
        Me.But_Filter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Filter.Image = CType(resources.GetObject("But_Filter.Image"), System.Drawing.Image)
        Me.But_Filter.Location = New System.Drawing.Point(429, 263)
        Me.But_Filter.LookAndFeel.SkinName = "iMaginary"
        Me.But_Filter.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Filter.Name = "But_Filter"
        Me.But_Filter.Size = New System.Drawing.Size(60, 25)
        Me.But_Filter.TabIndex = 259
        Me.But_Filter.Text = "Filter"
        '
        'But_Find
        '
        Me.But_Find.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Find.Image = CType(resources.GetObject("But_Find.Image"), System.Drawing.Image)
        Me.But_Find.Location = New System.Drawing.Point(490, 263)
        Me.But_Find.LookAndFeel.SkinName = "iMaginary"
        Me.But_Find.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Find.Name = "But_Find"
        Me.But_Find.Size = New System.Drawing.Size(60, 25)
        Me.But_Find.TabIndex = 260
        Me.But_Find.Text = "Find"
        '
        'Zoom1
        '
        Me.Zoom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Zoom1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Zoom1.EditValue = 8
        Me.Zoom1.Location = New System.Drawing.Point(556, 266)
        Me.Zoom1.Name = "Zoom1"
        Me.Zoom1.Properties.LargeChange = 2
        Me.Zoom1.Properties.LookAndFeel.SkinName = "Blue"
        Me.Zoom1.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Zoom1.Properties.Maximum = 50
        Me.Zoom1.Properties.Middle = 5
        Me.Zoom1.Properties.Minimum = 8
        Me.Zoom1.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.Bar
        Me.Zoom1.Properties.SmallChange = 2
        Me.Zoom1.Size = New System.Drawing.Size(150, 19)
        Me.Zoom1.TabIndex = 258
        Me.Zoom1.Value = 8
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
        'Frm_Bank_Accounts_Report
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 290)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Bank_Accounts_Report"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Accounts Listing"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents But_Filter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents But_Find As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Zoom1 As DevExpress.XtraEditors.ZoomTrackBarControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_SHOW As DevExpress.XtraEditors.SimpleButton

End Class
