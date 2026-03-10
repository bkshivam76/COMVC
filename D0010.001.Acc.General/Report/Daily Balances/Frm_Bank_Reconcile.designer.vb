<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bank_Reconcile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Bank_Reconcile))
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lbl_Separator3 = New DevExpress.XtraEditors.LabelControl()
        Me.But_Find = New DevExpress.XtraEditors.SimpleButton()
        Me.But_Filter = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTxnBalance = New DevExpress.XtraEditors.TextEdit()
        Me.lblClearingBalance = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNetBalance = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.lblTxnBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblClearingBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(615, 4)
        Me.BUT_CLOSE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_CLOSE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CLOSE.Name = "BUT_CLOSE"
        Me.BUT_CLOSE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_CLOSE.TabIndex = 7
        Me.BUT_CLOSE.Text = "Close"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.BackgroundImage = CType(resources.GetObject("StatusStrip1.BackgroundImage"), System.Drawing.Image)
        Me.StatusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.GripMargin = New System.Windows.Forms.Padding(0)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Lbl_ToolMsg})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 297)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(689, 18)
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
        Me.Lbl_ToolMsg.Size = New System.Drawing.Size(674, 13)
        Me.Lbl_ToolMsg.Spring = True
        Me.Lbl_ToolMsg.Text = "Ready"
        Me.Lbl_ToolMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Zoom1
        '
        Me.Zoom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Zoom1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Zoom1.EditValue = 8
        Me.Zoom1.Location = New System.Drawing.Point(131, 274)
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
        Me.Zoom1.TabIndex = 51
        Me.Zoom1.Value = 8
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
        Me.GridControl1.Location = New System.Drawing.Point(0, 63)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(691, 189)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupFormat = "{1} "
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsDetail.AutoZoomDetail = True
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(689, 34)
        Me.Panel1.TabIndex = 43
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(145, 22)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Reconciliation of "
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(521, 4)
        Me.BUT_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_PRINT.Name = "BUT_PRINT"
        Me.BUT_PRINT.Size = New System.Drawing.Size(95, 26)
        Me.BUT_PRINT.TabIndex = 6
        Me.BUT_PRINT.Text = "List Preview"
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
        'Lbl_Separator3
        '
        Me.Lbl_Separator3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator3.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator3.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator3.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Separator3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Separator3.LineVisible = True
        Me.Lbl_Separator3.Location = New System.Drawing.Point(121, 268)
        Me.Lbl_Separator3.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator3.Name = "Lbl_Separator3"
        Me.Lbl_Separator3.Size = New System.Drawing.Size(10, 29)
        Me.Lbl_Separator3.TabIndex = 142
        '
        'But_Find
        '
        Me.But_Find.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Find.Image = CType(resources.GetObject("But_Find.Image"), System.Drawing.Image)
        Me.But_Find.Location = New System.Drawing.Point(60, 270)
        Me.But_Find.LookAndFeel.SkinName = "iMaginary"
        Me.But_Find.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Find.Name = "But_Find"
        Me.But_Find.Size = New System.Drawing.Size(60, 25)
        Me.But_Find.TabIndex = 141
        Me.But_Find.Text = "Find"
        '
        'But_Filter
        '
        Me.But_Filter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Filter.Image = CType(resources.GetObject("But_Filter.Image"), System.Drawing.Image)
        Me.But_Filter.Location = New System.Drawing.Point(2, 270)
        Me.But_Filter.LookAndFeel.SkinName = "iMaginary"
        Me.But_Filter.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Filter.Name = "But_Filter"
        Me.But_Filter.Size = New System.Drawing.Size(58, 25)
        Me.But_Filter.TabIndex = 140
        Me.But_Filter.Text = "Filter"
        '
        'LabelControl31
        '
        Me.LabelControl31.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl31.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl31.Location = New System.Drawing.Point(396, 257)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Size = New System.Drawing.Size(147, 14)
        Me.LabelControl31.TabIndex = 257
        Me.LabelControl31.Text = "Nett Difference (in Rs.):"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl1.Location = New System.Drawing.Point(331, 42)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(213, 14)
        Me.LabelControl1.TabIndex = 257
        Me.LabelControl1.Text = "Balance as per Txn Dates (in Rs.):"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl2.Location = New System.Drawing.Point(300, 277)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(243, 14)
        Me.LabelControl2.TabIndex = 257
        Me.LabelControl2.Text = "Balance as per Clearing Dates (in Rs.):"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl3.Location = New System.Drawing.Point(5, -211)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(99, 14)
        Me.LabelControl3.TabIndex = 257
        Me.LabelControl3.Text = "Nett Difference:"
        '
        'lblTxnBalance
        '
        Me.lblTxnBalance.EditValue = "0"
        Me.lblTxnBalance.Location = New System.Drawing.Point(545, 40)
        Me.lblTxnBalance.Name = "lblTxnBalance"
        Me.lblTxnBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblTxnBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxnBalance.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblTxnBalance.Properties.Appearance.Options.UseBackColor = True
        Me.lblTxnBalance.Properties.Appearance.Options.UseFont = True
        Me.lblTxnBalance.Properties.Appearance.Options.UseForeColor = True
        Me.lblTxnBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.lblTxnBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTxnBalance.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblTxnBalance.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxnBalance.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.lblTxnBalance.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblTxnBalance.Properties.AppearanceDisabled.Options.UseFont = True
        Me.lblTxnBalance.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.lblTxnBalance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.lblTxnBalance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxnBalance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.lblTxnBalance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.lblTxnBalance.Properties.AppearanceFocused.Options.UseFont = True
        Me.lblTxnBalance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.lblTxnBalance.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.lblTxnBalance.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTxnBalance.Properties.DisplayFormat.FormatString = "f2"
        Me.lblTxnBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblTxnBalance.Properties.EditFormat.FormatString = "f2"
        Me.lblTxnBalance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblTxnBalance.Properties.ReadOnly = True
        Me.lblTxnBalance.Size = New System.Drawing.Size(141, 18)
        Me.lblTxnBalance.TabIndex = 276
        Me.lblTxnBalance.TabStop = False
        '
        'lblClearingBalance
        '
        Me.lblClearingBalance.EditValue = "0"
        Me.lblClearingBalance.Location = New System.Drawing.Point(545, 275)
        Me.lblClearingBalance.Name = "lblClearingBalance"
        Me.lblClearingBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblClearingBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClearingBalance.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblClearingBalance.Properties.Appearance.Options.UseBackColor = True
        Me.lblClearingBalance.Properties.Appearance.Options.UseFont = True
        Me.lblClearingBalance.Properties.Appearance.Options.UseForeColor = True
        Me.lblClearingBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.lblClearingBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblClearingBalance.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblClearingBalance.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClearingBalance.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.lblClearingBalance.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblClearingBalance.Properties.AppearanceDisabled.Options.UseFont = True
        Me.lblClearingBalance.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.lblClearingBalance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.lblClearingBalance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClearingBalance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.lblClearingBalance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.lblClearingBalance.Properties.AppearanceFocused.Options.UseFont = True
        Me.lblClearingBalance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.lblClearingBalance.Properties.DisplayFormat.FormatString = "f2"
        Me.lblClearingBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblClearingBalance.Properties.EditFormat.FormatString = "f2"
        Me.lblClearingBalance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblClearingBalance.Properties.ReadOnly = True
        Me.lblClearingBalance.Size = New System.Drawing.Size(141, 18)
        Me.lblClearingBalance.TabIndex = 276
        Me.lblClearingBalance.TabStop = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl4.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.LabelControl4.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.LabelControl4.LineVisible = True
        Me.LabelControl4.Location = New System.Drawing.Point(287, 268)
        Me.LabelControl4.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl4.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(5, 29)
        Me.LabelControl4.TabIndex = 142
        '
        'lblNetBalance
        '
        Me.lblNetBalance.EditValue = "0"
        Me.lblNetBalance.Location = New System.Drawing.Point(545, 255)
        Me.lblNetBalance.Name = "lblNetBalance"
        Me.lblNetBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblNetBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetBalance.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNetBalance.Properties.Appearance.Options.UseBackColor = True
        Me.lblNetBalance.Properties.Appearance.Options.UseFont = True
        Me.lblNetBalance.Properties.Appearance.Options.UseForeColor = True
        Me.lblNetBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.lblNetBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblNetBalance.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblNetBalance.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetBalance.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.lblNetBalance.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblNetBalance.Properties.AppearanceDisabled.Options.UseFont = True
        Me.lblNetBalance.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.lblNetBalance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.lblNetBalance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetBalance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.lblNetBalance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.lblNetBalance.Properties.AppearanceFocused.Options.UseFont = True
        Me.lblNetBalance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.lblNetBalance.Properties.DisplayFormat.FormatString = "f2"
        Me.lblNetBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblNetBalance.Properties.EditFormat.FormatString = "f2"
        Me.lblNetBalance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblNetBalance.Properties.ReadOnly = True
        Me.lblNetBalance.Size = New System.Drawing.Size(141, 18)
        Me.lblNetBalance.TabIndex = 276
        Me.lblNetBalance.TabStop = False
        '
        'LabelControl5
        '
        Me.LabelControl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl5.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom
        Me.LabelControl5.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl5.LineVisible = True
        Me.LabelControl5.Location = New System.Drawing.Point(123, 261)
        Me.LabelControl5.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl5.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(165, 10)
        Me.LabelControl5.TabIndex = 142
        '
        'Frm_Bank_Reconcile
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 315)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.lblNetBalance)
        Me.Controls.Add(Me.lblClearingBalance)
        Me.Controls.Add(Me.lblTxnBalance)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl31)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.Lbl_Separator3)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.Name = "Frm_Bank_Reconcile"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Reconciliation"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.lblTxnBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblClearingBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Lbl_ToolMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Zoom1 As DevExpress.XtraEditors.ZoomTrackBarControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_Separator3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents But_Find As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents But_Filter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Public WithEvents lblTxnBalance As DevExpress.XtraEditors.TextEdit
    Public WithEvents lblClearingBalance As DevExpress.XtraEditors.TextEdit
    Public WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Public WithEvents lblNetBalance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl


End Class
