<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Donation_Register
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Donation_Register))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_DON_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_DON_REQ = New DevExpress.XtraEditors.SimpleButton()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DonationRequestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Request = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Cancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_FORM_PRINT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lbl_Create = New System.Windows.Forms.Label()
        Me.Lbl_Modify = New System.Windows.Forms.Label()
        Me.Lbl_Seprator2 = New DevExpress.XtraEditors.LabelControl()
        Me.But_Find = New DevExpress.XtraEditors.SimpleButton()
        Me.But_Filter = New DevExpress.XtraEditors.SimpleButton()
        Me.Lbl_Seprator1 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Status = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Seprator3 = New DevExpress.XtraEditors.LabelControl()
        Me.Pic_Status = New DevExpress.XtraEditors.PictureEdit()
        Me.AttachmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Add_Attachment = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Manage_Attachment = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_Link_Attachment = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GridControl1.Location = New System.Drawing.Point(-1, 34)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(776, 371)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
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
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.BUT_DON_PRINT)
        Me.Panel1.Controls.Add(Me.BUT_DON_REQ)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(774, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_DON_PRINT
        '
        Me.BUT_DON_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_DON_PRINT.Image = CType(resources.GetObject("BUT_DON_PRINT.Image"), System.Drawing.Image)
        Me.BUT_DON_PRINT.Location = New System.Drawing.Point(384, 4)
        Me.BUT_DON_PRINT.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_DON_PRINT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_DON_PRINT.Name = "BUT_DON_PRINT"
        Me.BUT_DON_PRINT.Size = New System.Drawing.Size(84, 26)
        Me.BUT_DON_PRINT.TabIndex = 9
        Me.BUT_DON_PRINT.Text = "Print Form"
        '
        'BUT_DON_REQ
        '
        Me.BUT_DON_REQ.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_DON_REQ.Image = CType(resources.GetObject("BUT_DON_REQ.Image"), System.Drawing.Image)
        Me.BUT_DON_REQ.Location = New System.Drawing.Point(467, 4)
        Me.BUT_DON_REQ.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_DON_REQ.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_DON_REQ.Name = "BUT_DON_REQ"
        Me.BUT_DON_REQ.Size = New System.Drawing.Size(140, 26)
        Me.BUT_DON_REQ.TabIndex = 8
        Me.BUT_DON_REQ.Text = "Request For Receipt"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(184, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Donation Register"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(606, 4)
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
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_Print, Me.ToolStripMenuItem4, Me.AttachmentsToolStripMenuItem, Me.DonationRequestToolStripMenuItem, Me.T_FORM_PRINT, Me.ToolStripMenuItem1, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(183, 176)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(182, 22)
        Me.T_Print.Text = "&List Preview"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(179, 6)
        '
        'DonationRequestToolStripMenuItem
        '
        Me.DonationRequestToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_Request, Me.T_Cancel})
        Me.DonationRequestToolStripMenuItem.Image = CType(resources.GetObject("DonationRequestToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DonationRequestToolStripMenuItem.Name = "DonationRequestToolStripMenuItem"
        Me.DonationRequestToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.DonationRequestToolStripMenuItem.Text = "&Request"
        '
        'T_Request
        '
        Me.T_Request.Image = CType(resources.GetObject("T_Request.Image"), System.Drawing.Image)
        Me.T_Request.Name = "T_Request"
        Me.T_Request.Size = New System.Drawing.Size(160, 22)
        Me.T_Request.Text = "For &Receipt"
        '
        'T_Cancel
        '
        Me.T_Cancel.Image = CType(resources.GetObject("T_Cancel.Image"), System.Drawing.Image)
        Me.T_Cancel.Name = "T_Cancel"
        Me.T_Cancel.Size = New System.Drawing.Size(160, 22)
        Me.T_Cancel.Text = "For &Cancellation"
        Me.T_Cancel.Visible = False
        '
        'T_FORM_PRINT
        '
        Me.T_FORM_PRINT.Name = "T_FORM_PRINT"
        Me.T_FORM_PRINT.Size = New System.Drawing.Size(182, 22)
        Me.T_FORM_PRINT.Text = "Print Donation Form"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(179, 6)
        '
        'T_Refresh
        '
        Me.T_Refresh.Image = CType(resources.GetObject("T_Refresh.Image"), System.Drawing.Image)
        Me.T_Refresh.Name = "T_Refresh"
        Me.T_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.T_Refresh.Size = New System.Drawing.Size(182, 22)
        Me.T_Refresh.Text = "&Refresh"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(179, 6)
        '
        'T_Close
        '
        Me.T_Close.Image = CType(resources.GetObject("T_Close.Image"), System.Drawing.Image)
        Me.T_Close.Name = "T_Close"
        Me.T_Close.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.T_Close.Size = New System.Drawing.Size(182, 22)
        Me.T_Close.Text = "&Close"
        '
        'Lbl_Create
        '
        Me.Lbl_Create.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Create.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Create.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Create.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Create.Location = New System.Drawing.Point(158, 405)
        Me.Lbl_Create.Name = "Lbl_Create"
        Me.Lbl_Create.Size = New System.Drawing.Size(330, 15)
        Me.Lbl_Create.TabIndex = 132
        Me.Lbl_Create.Text = "Created On: 12-12-2010, 04:55:20 PM, By: Connect"
        '
        'Lbl_Modify
        '
        Me.Lbl_Modify.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Modify.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Modify.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Modify.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Modify.Location = New System.Drawing.Point(158, 420)
        Me.Lbl_Modify.Name = "Lbl_Modify"
        Me.Lbl_Modify.Size = New System.Drawing.Size(330, 15)
        Me.Lbl_Modify.TabIndex = 133
        Me.Lbl_Modify.Text = "Edited On: 12-12-2010, 04:55:20 PM, By: Connect"
        '
        'Lbl_Seprator2
        '
        Me.Lbl_Seprator2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Seprator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Seprator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Seprator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Seprator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Seprator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Seprator2.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Seprator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Seprator2.LineVisible = True
        Me.Lbl_Seprator2.Location = New System.Drawing.Point(489, 405)
        Me.Lbl_Seprator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Seprator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Seprator2.Name = "Lbl_Seprator2"
        Me.Lbl_Seprator2.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Seprator2.TabIndex = 142
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
        'Lbl_Seprator1
        '
        Me.Lbl_Seprator1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Seprator1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Seprator1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Seprator1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Seprator1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Seprator1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Seprator1.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Seprator1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Seprator1.LineVisible = True
        Me.Lbl_Seprator1.Location = New System.Drawing.Point(156, 405)
        Me.Lbl_Seprator1.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Seprator1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Seprator1.Name = "Lbl_Seprator1"
        Me.Lbl_Seprator1.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Seprator1.TabIndex = 144
        '
        'Lbl_Status
        '
        Me.Lbl_Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Status.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Status.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Status.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Lbl_Status.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Lbl_Status.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Status.Location = New System.Drawing.Point(33, 405)
        Me.Lbl_Status.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Status.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Status.Name = "Lbl_Status"
        Me.Lbl_Status.Size = New System.Drawing.Size(122, 29)
        ToolTipTitleItem1.Text = "Entry Status"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.Lbl_Status.SuperTip = SuperToolTip1
        Me.Lbl_Status.TabIndex = 145
        Me.Lbl_Status.Text = "Status"
        '
        'Lbl_Seprator3
        '
        Me.Lbl_Seprator3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Seprator3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Seprator3.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Seprator3.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Seprator3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Seprator3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Seprator3.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Seprator3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Seprator3.LineVisible = True
        Me.Lbl_Seprator3.Location = New System.Drawing.Point(615, 405)
        Me.Lbl_Seprator3.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Seprator3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Seprator3.Name = "Lbl_Seprator3"
        Me.Lbl_Seprator3.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Seprator3.TabIndex = 146
        '
        'Pic_Status
        '
        Me.Pic_Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Pic_Status.EditValue = Global.ConnectOne.D0010._001.My.Resources.Resources.unlock
        Me.Pic_Status.Location = New System.Drawing.Point(-1, 404)
        Me.Pic_Status.Name = "Pic_Status"
        Me.Pic_Status.Size = New System.Drawing.Size(32, 31)
        ToolTipTitleItem2.Text = "Entry Status"
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        Me.Pic_Status.SuperTip = SuperToolTip2
        Me.Pic_Status.TabIndex = 148
        '
        'AttachmentsToolStripMenuItem
        '
        Me.AttachmentsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_Add_Attachment, Me.T_Manage_Attachment, Me.T_Link_Attachment})
        Me.AttachmentsToolStripMenuItem.Image = CType(resources.GetObject("AttachmentsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AttachmentsToolStripMenuItem.Name = "AttachmentsToolStripMenuItem"
        Me.AttachmentsToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.AttachmentsToolStripMenuItem.Text = "Attachments"
        '
        'T_Add_Attachment
        '
        Me.T_Add_Attachment.Image = CType(resources.GetObject("T_Add_Attachment.Image"), System.Drawing.Image)
        Me.T_Add_Attachment.Name = "T_Add_Attachment"
        Me.T_Add_Attachment.Size = New System.Drawing.Size(188, 22)
        Me.T_Add_Attachment.Text = "Add Attachment"
        '
        'T_Manage_Attachment
        '
        Me.T_Manage_Attachment.Image = CType(resources.GetObject("T_Manage_Attachment.Image"), System.Drawing.Image)
        Me.T_Manage_Attachment.Name = "T_Manage_Attachment"
        Me.T_Manage_Attachment.Size = New System.Drawing.Size(188, 22)
        Me.T_Manage_Attachment.Text = "Manage Attachments"
        '
        'T_Link_Attachment
        '
        Me.T_Link_Attachment.Image = CType(resources.GetObject("T_Link_Attachment.Image"), System.Drawing.Image)
        Me.T_Link_Attachment.Name = "T_Link_Attachment"
        Me.T_Link_Attachment.Size = New System.Drawing.Size(188, 22)
        Me.T_Link_Attachment.Text = "Link Attachment"
        '
        'Frm_Donation_Register
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 452)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.Pic_Status)
        Me.Controls.Add(Me.Lbl_Seprator1)
        Me.Controls.Add(Me.Lbl_Seprator3)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.Lbl_Status)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Lbl_Seprator2)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Lbl_Create)
        Me.Controls.Add(Me.Lbl_Modify)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.Name = "Frm_Donation_Register"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Donation Register"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
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
    Friend WithEvents BUT_PRINT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Zoom1 As DevExpress.XtraEditors.ZoomTrackBarControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_Create As System.Windows.Forms.Label
    Friend WithEvents Lbl_Modify As System.Windows.Forms.Label
    Friend WithEvents Lbl_Seprator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents But_Find As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents But_Filter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Lbl_Seprator1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Status As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Seprator3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Pic_Status As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DonationRequestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_Request As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_Cancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BUT_DON_REQ As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents T_FORM_PRINT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BUT_DON_PRINT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents AttachmentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents T_Add_Attachment As ToolStripMenuItem
    Friend WithEvents T_Manage_Attachment As ToolStripMenuItem
    Friend WithEvents T_Link_Attachment As ToolStripMenuItem
End Class
