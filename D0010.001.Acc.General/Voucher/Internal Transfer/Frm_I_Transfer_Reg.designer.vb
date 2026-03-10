<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_I_Transfer_Reg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_I_Transfer_Reg))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_ACCEPT = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_New = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Edit = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Accept = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Unmatch = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Separator3 = New DevExpress.XtraEditors.LabelControl()
        Me.But_Find = New DevExpress.XtraEditors.SimpleButton()
        Me.But_Filter = New DevExpress.XtraEditors.SimpleButton()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Pic_Status = New DevExpress.XtraEditors.PictureEdit()
        Me.Lbl_Create = New System.Windows.Forms.Label()
        Me.Lbl_Modify = New System.Windows.Forms.Label()
        Me.Lbl_StatusOn = New System.Windows.Forms.Label()
        Me.Lbl_StatusBy = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(700, 4)
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
        'Zoom1
        '
        Me.Zoom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Zoom1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Zoom1.EditValue = 8
        Me.Zoom1.Location = New System.Drawing.Point(620, 410)
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
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(770, 145)
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
        Me.Panel1.Controls.Add(Me.BUT_ACCEPT)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(774, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_ACCEPT
        '
        Me.BUT_ACCEPT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_ACCEPT.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_ACCEPT.Appearance.Options.UseFont = True
        Me.BUT_ACCEPT.Image = CType(resources.GetObject("BUT_ACCEPT.Image"), System.Drawing.Image)
        Me.BUT_ACCEPT.Location = New System.Drawing.Point(506, 4)
        Me.BUT_ACCEPT.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_ACCEPT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_ACCEPT.Name = "BUT_ACCEPT"
        Me.BUT_ACCEPT.Size = New System.Drawing.Size(108, 27)
        Me.BUT_ACCEPT.TabIndex = 2
        Me.BUT_ACCEPT.Text = "Accept Transfer"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(614, 4)
        Me.BUT_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_PRINT.Name = "BUT_PRINT"
        Me.BUT_PRINT.Size = New System.Drawing.Size(86, 26)
        Me.BUT_PRINT.TabIndex = 145
        Me.BUT_PRINT.Text = "List Preview"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(260, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Internal Transfer Register"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_New, Me.T_Edit, Me.T_Delete, Me.ToolStripSeparator1, Me.T_Accept, Me.T_Unmatch, Me.T_Print, Me.ToolStripMenuItem1, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 220)
        '
        'T_New
        '
        Me.T_New.Image = CType(resources.GetObject("T_New.Image"), System.Drawing.Image)
        Me.T_New.Name = "T_New"
        Me.T_New.Size = New System.Drawing.Size(176, 22)
        Me.T_New.Text = "New"
        Me.T_New.Visible = False
        '
        'T_Edit
        '
        Me.T_Edit.Image = CType(resources.GetObject("T_Edit.Image"), System.Drawing.Image)
        Me.T_Edit.Name = "T_Edit"
        Me.T_Edit.Size = New System.Drawing.Size(176, 22)
        Me.T_Edit.Text = "Edit"
        Me.T_Edit.Visible = False
        '
        'T_Delete
        '
        Me.T_Delete.Image = CType(resources.GetObject("T_Delete.Image"), System.Drawing.Image)
        Me.T_Delete.Name = "T_Delete"
        Me.T_Delete.Size = New System.Drawing.Size(176, 22)
        Me.T_Delete.Text = "Delete"
        Me.T_Delete.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
        '
        'T_Accept
        '
        Me.T_Accept.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.greenaccept
        Me.T_Accept.Name = "T_Accept"
        Me.T_Accept.Size = New System.Drawing.Size(176, 22)
        Me.T_Accept.Text = "Accept Transfer"
        '
        'T_Unmatch
        '
        Me.T_Unmatch.Image = CType(resources.GetObject("T_Unmatch.Image"), System.Drawing.Image)
        Me.T_Unmatch.Name = "T_Unmatch"
        Me.T_Unmatch.Size = New System.Drawing.Size(176, 22)
        Me.T_Unmatch.Text = "Unmatch Transfer"
        Me.T_Unmatch.Visible = False
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
        'Lbl_Separator2
        '
        Me.Lbl_Separator2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator2.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Separator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Separator2.LineVisible = True
        Me.Lbl_Separator2.Location = New System.Drawing.Point(489, 405)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Separator2.TabIndex = 131
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
        Me.Lbl_Separator3.Location = New System.Drawing.Point(615, 405)
        Me.Lbl_Separator3.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator3.Name = "Lbl_Separator3"
        Me.Lbl_Separator3.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Separator3.TabIndex = 142
        '
        'But_Find
        '
        Me.But_Find.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Find.Image = CType(resources.GetObject("But_Find.Image"), System.Drawing.Image)
        Me.But_Find.Location = New System.Drawing.Point(554, 407)
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
        Me.But_Filter.Location = New System.Drawing.Point(493, 407)
        Me.But_Filter.LookAndFeel.SkinName = "iMaginary"
        Me.But_Filter.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Filter.Name = "But_Filter"
        Me.But_Filter.Size = New System.Drawing.Size(60, 25)
        Me.But_Filter.TabIndex = 140
        Me.But_Filter.Text = "Filter"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 34)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.AppearanceCaption.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainerControl1.Panel1.AppearanceCaption.ForeColor = System.Drawing.Color.Navy
        Me.SplitContainerControl1.Panel1.AppearanceCaption.Options.UseFont = True
        Me.SplitContainerControl1.Panel1.AppearanceCaption.Options.UseForeColor = True
        Me.SplitContainerControl1.Panel1.AppearanceCaption.Options.UseTextOptions = True
        Me.SplitContainerControl1.Panel1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.SplitContainerControl1.Panel1.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GridControl1)
        Me.SplitContainerControl1.Panel1.MinSize = 170
        Me.SplitContainerControl1.Panel1.ShowCaption = True
        Me.SplitContainerControl1.Panel1.Text = "Currently Posted Entries"
        Me.SplitContainerControl1.Panel2.AppearanceCaption.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SplitContainerControl1.Panel2.AppearanceCaption.ForeColor = System.Drawing.Color.Navy
        Me.SplitContainerControl1.Panel2.AppearanceCaption.Options.UseFont = True
        Me.SplitContainerControl1.Panel2.AppearanceCaption.Options.UseForeColor = True
        Me.SplitContainerControl1.Panel2.AppearanceCaption.Options.UseTextOptions = True
        Me.SplitContainerControl1.Panel2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.SplitContainerControl1.Panel2.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GridControl2)
        Me.SplitContainerControl1.Panel2.MinSize = 170
        Me.SplitContainerControl1.Panel2.ShowCaption = True
        Me.SplitContainerControl1.Panel2.Text = "Pending Entries to be accepted by us"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(774, 371)
        Me.SplitContainerControl1.SplitterPosition = 170
        Me.SplitContainerControl1.TabIndex = 0
        '
        'GridControl2
        '
        Me.GridControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl2.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.GridControl2.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Red
        Me.GridControl2.EmbeddedNavigator.Appearance.Options.UseBackColor = True
        Me.GridControl2.EmbeddedNavigator.Appearance.Options.UseForeColor = True
        Me.GridControl2.EmbeddedNavigator.Buttons.Append.Tag = "Append"
        Me.GridControl2.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.CancelEdit.Tag = "CancelEdit"
        Me.GridControl2.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.GridControl2.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.GridControl2.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.First.Tag = "First"
        Me.GridControl2.EmbeddedNavigator.Buttons.Last.Tag = "Last"
        Me.GridControl2.EmbeddedNavigator.Buttons.Next.Tag = "Next"
        Me.GridControl2.EmbeddedNavigator.Buttons.NextPage.Tag = "NextPage"
        Me.GridControl2.EmbeddedNavigator.Buttons.Prev.Tag = "Prev"
        Me.GridControl2.EmbeddedNavigator.Buttons.PrevPage.Tag = "PrevPage"
        Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.GridControl2.EmbeddedNavigator.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridControl2.EmbeddedNavigator.CustomButtons.AddRange(New DevExpress.XtraEditors.NavigatorCustomButton() {New DevExpress.XtraEditors.NavigatorCustomButton(-1, 6, True, True, "Open Column Chooser", "OPEN_COL"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 8, True, True, "Show Group Box", "GROUP_BOX"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 14, True, True, "Show Grouped Column", "GROUPED_COL"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 18, True, True, "Show Footer Bar", "FOOTER_BAR"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 7, True, True, "Show Group Footer Bar", "GROUP_FOOTER"), New DevExpress.XtraEditors.NavigatorCustomButton(-1, 9, True, True, "Open Filter Builder", "FILTER")})
        Me.GridControl2.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
        Me.GridControl2.Location = New System.Drawing.Point(0, 0)
        Me.GridControl2.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.Size = New System.Drawing.Size(770, 169)
        Me.GridControl2.TabIndex = 1
        Me.GridControl2.UseEmbeddedNavigator = True
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.GroupFormat = "{1} "
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsDetail.AutoZoomDetail = True
        Me.GridView2.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView2.OptionsPrint.PrintDetails = True
        Me.GridView2.OptionsSelection.InvertSelection = True
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.ViewCaption = "Group"
        '
        'Pic_Status
        '
        Me.Pic_Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Pic_Status.EditValue = Global.ConnectOne.D0010._001.My.Resources.Resources.unlock
        Me.Pic_Status.Location = New System.Drawing.Point(-1, 404)
        Me.Pic_Status.Name = "Pic_Status"
        Me.Pic_Status.Size = New System.Drawing.Size(32, 31)
        ToolTipTitleItem1.Text = "Entry Status"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.Pic_Status.SuperTip = SuperToolTip1
        Me.Pic_Status.TabIndex = 149
        '
        'Lbl_Create
        '
        Me.Lbl_Create.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Create.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Create.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Create.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Create.Location = New System.Drawing.Point(194, 405)
        Me.Lbl_Create.Name = "Lbl_Create"
        Me.Lbl_Create.Size = New System.Drawing.Size(294, 15)
        Me.Lbl_Create.TabIndex = 151
        Me.Lbl_Create.Text = "Created On: 12-12-2010, 04:55:20 PM, By: Connect"
        Me.Lbl_Create.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Lbl_Modify
        '
        Me.Lbl_Modify.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Modify.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Modify.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Modify.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Modify.Location = New System.Drawing.Point(194, 420)
        Me.Lbl_Modify.Name = "Lbl_Modify"
        Me.Lbl_Modify.Size = New System.Drawing.Size(294, 15)
        Me.Lbl_Modify.TabIndex = 152
        Me.Lbl_Modify.Text = "Edited On: 12-12-2010, 04:55:20 PM, By: Connect"
        Me.Lbl_Modify.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Lbl_StatusOn
        '
        Me.Lbl_StatusOn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_StatusOn.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_StatusOn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_StatusOn.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_StatusOn.Location = New System.Drawing.Point(37, 405)
        Me.Lbl_StatusOn.Name = "Lbl_StatusOn"
        Me.Lbl_StatusOn.Size = New System.Drawing.Size(146, 15)
        Me.Lbl_StatusOn.TabIndex = 152
        Me.Lbl_StatusOn.Text = "Status On: 12-12-2010, 04:55:20 PM"
        '
        'Lbl_StatusBy
        '
        Me.Lbl_StatusBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_StatusBy.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_StatusBy.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_StatusBy.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_StatusBy.Location = New System.Drawing.Point(37, 420)
        Me.Lbl_StatusBy.Name = "Lbl_StatusBy"
        Me.Lbl_StatusBy.Size = New System.Drawing.Size(146, 15)
        Me.Lbl_StatusBy.TabIndex = 153
        Me.Lbl_StatusBy.Text = "Status By:  Connect"
        '
        'Frm_I_Transfer_Reg
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 452)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.Lbl_StatusBy)
        Me.Controls.Add(Me.Lbl_StatusOn)
        Me.Controls.Add(Me.Lbl_Create)
        Me.Controls.Add(Me.Pic_Status)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.Lbl_Separator3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.Lbl_Modify)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "Frm_I_Transfer_Reg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Internal Transfer Register"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Lbl_ToolMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Zoom1 As DevExpress.XtraEditors.ZoomTrackBarControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents But_Find As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents But_Filter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Pic_Status As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents Lbl_Create As System.Windows.Forms.Label
    Friend WithEvents Lbl_Modify As System.Windows.Forms.Label
    Friend WithEvents Lbl_StatusOn As System.Windows.Forms.Label
    Friend WithEvents Lbl_StatusBy As System.Windows.Forms.Label
    Friend WithEvents BUT_ACCEPT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents T_Accept As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_New As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_Edit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_Unmatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator

End Class
