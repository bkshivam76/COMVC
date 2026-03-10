<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TDS_Register
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_TDS_Register))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_MAP_RECD = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_UNMAP_RECD = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_MAP_PAID = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_UNMAP_PAID = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_UNMAP_DED = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.RECD_TDS_M_ID = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PAID_TDS_M_ID = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.DED_TR_M_ID = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.Center = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.UID = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PARTY = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PAN = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ACTION_STATUS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.TDS_CODE = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ACTUAL_DED_DATE = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.DECLARED_DED_DATE = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BILL = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.TDS_DED = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.RECD_DATE = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RECD_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INTEREST_RECD_ON_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PENALTY_RECD_ON_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.PAID_DATE = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PAID_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INTEREST_PAID_ON_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PENALTY_PAID_ON_TDS = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.Balances = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.Receivable_Center = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.Received_Excess_Center = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_ADD_BY = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_ADD_ON = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_EDIT_BY = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_EDIT_ON = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_STATUS_BY = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.REC_STATUS_ON = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.DED_TR_SR_NO = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RECD_TR_SR_NO = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.PAID_TR_SR_NO = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(792, 4)
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
        Me.StatusStrip1.Size = New System.Drawing.Size(866, 18)
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
        Me.Lbl_ToolMsg.Size = New System.Drawing.Size(851, 13)
        Me.Lbl_ToolMsg.Spring = True
        Me.Lbl_ToolMsg.Text = "Ready"
        Me.Lbl_ToolMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Zoom1
        '
        Me.Zoom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Zoom1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Zoom1.EditValue = 8
        Me.Zoom1.Location = New System.Drawing.Point(712, 410)
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
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(866, 34)
        Me.Panel1.TabIndex = 43
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(138, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "TDS Register"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(698, 4)
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
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_Print, Me.ToolStripMenuItem4, Me.T_MAP_RECD, Me.T_UNMAP_RECD, Me.ToolStripSeparator1, Me.T_MAP_PAID, Me.T_UNMAP_PAID, Me.ToolStripMenuItem1, Me.T_UNMAP_DED, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(351, 204)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(350, 22)
        Me.T_Print.Text = "&List Preview"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(347, 6)
        '
        'T_MAP_RECD
        '
        Me.T_MAP_RECD.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.greenaccept
        Me.T_MAP_RECD.Name = "T_MAP_RECD"
        Me.T_MAP_RECD.Size = New System.Drawing.Size(350, 22)
        Me.T_MAP_RECD.Text = "Map Selected TDS Recd Entry with TDS Deducted"
        '
        'T_UNMAP_RECD
        '
        Me.T_UNMAP_RECD.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Collapse
        Me.T_UNMAP_RECD.Name = "T_UNMAP_RECD"
        Me.T_UNMAP_RECD.Size = New System.Drawing.Size(350, 22)
        Me.T_UNMAP_RECD.Text = "Unmap Selected TDS Recd Entry from all Deductions"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(347, 6)
        '
        'T_MAP_PAID
        '
        Me.T_MAP_PAID.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.greenaccept
        Me.T_MAP_PAID.Name = "T_MAP_PAID"
        Me.T_MAP_PAID.Size = New System.Drawing.Size(350, 22)
        Me.T_MAP_PAID.Text = "Map Selected TDS Paid Entry with TDS Deducted"
        '
        'T_UNMAP_PAID
        '
        Me.T_UNMAP_PAID.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Collapse
        Me.T_UNMAP_PAID.Name = "T_UNMAP_PAID"
        Me.T_UNMAP_PAID.Size = New System.Drawing.Size(350, 22)
        Me.T_UNMAP_PAID.Text = "Unmap Selected TDS Paid Entry from all Deductions"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(347, 6)
        '
        'T_UNMAP_DED
        '
        Me.T_UNMAP_DED.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Collapse
        Me.T_UNMAP_DED.Name = "T_UNMAP_DED"
        Me.T_UNMAP_DED.Size = New System.Drawing.Size(350, 22)
        Me.T_UNMAP_DED.Text = "Unmap Selected TDS Deducted Entry"
        '
        'T_Refresh
        '
        Me.T_Refresh.Image = CType(resources.GetObject("T_Refresh.Image"), System.Drawing.Image)
        Me.T_Refresh.Name = "T_Refresh"
        Me.T_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.T_Refresh.Size = New System.Drawing.Size(350, 22)
        Me.T_Refresh.Text = "&Refresh"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(347, 6)
        '
        'T_Close
        '
        Me.T_Close.Image = CType(resources.GetObject("T_Close.Image"), System.Drawing.Image)
        Me.T_Close.Name = "T_Close"
        Me.T_Close.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.T_Close.Size = New System.Drawing.Size(350, 22)
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
        Me.Lbl_Create.Size = New System.Drawing.Size(422, 15)
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
        Me.Lbl_Modify.Size = New System.Drawing.Size(422, 15)
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
        Me.Lbl_Seprator2.Location = New System.Drawing.Point(581, 405)
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
        Me.But_Find.Location = New System.Drawing.Point(646, 407)
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
        Me.But_Filter.Location = New System.Drawing.Point(585, 407)
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
        Me.Lbl_Seprator3.Location = New System.Drawing.Point(707, 405)
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
        Me.GridControl1.EmbeddedNavigator.CustomButtons.AddRange(New DevExpress.XtraEditors.NavigatorCustomButton() {New DevExpress.XtraEditors.NavigatorCustomButton(-1, 6, True, True, "Open Col Chooser", "OPEN_COL")})
        Me.GridControl1.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
        Me.GridControl1.Location = New System.Drawing.Point(0, 34)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.BandedGridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        Me.GridControl1.Size = New System.Drawing.Size(862, 370)
        Me.GridControl1.TabIndex = 149
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView1})
        '
        'BandedGridView1
        '
        Me.BandedGridView1.Appearance.FixedLine.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BandedGridView1.Appearance.FixedLine.Options.UseBackColor = True
        Me.BandedGridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightCyan
        Me.BandedGridView1.Appearance.FocusedCell.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BandedGridView1.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Blue
        Me.BandedGridView1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.BandedGridView1.Appearance.FocusedCell.Options.UseBorderColor = True
        Me.BandedGridView1.Appearance.FocusedCell.Options.UseFont = True
        Me.BandedGridView1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.BandedGridView1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Italic)
        Me.BandedGridView1.Appearance.Preview.ForeColor = System.Drawing.Color.DimGray
        Me.BandedGridView1.Appearance.Preview.Options.UseFont = True
        Me.BandedGridView1.Appearance.Preview.Options.UseForeColor = True
        Me.BandedGridView1.AppearancePrint.BandPanel.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.BandPanel.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.EvenRow.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.EvenRow.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.GroupRow.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.GroupRow.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.OddRow.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.OddRow.Options.UseFont = True
        Me.BandedGridView1.AppearancePrint.Row.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.BandedGridView1.AppearancePrint.Row.Options.UseFont = True
        Me.BandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1, Me.GridBand3, Me.GridBand2, Me.gridBand4, Me.Balances})
        Me.BandedGridView1.ColumnPanelRowHeight = 0
        Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.Center, Me.UID, Me.PARTY, Me.ACTUAL_DED_DATE, Me.DECLARED_DED_DATE, Me.PAN, Me.TDS_CODE, Me.BILL, Me.TDS_DED, Me.RECD_DATE, Me.RECD_TDS, Me.INTEREST_RECD_ON_TDS, Me.PENALTY_RECD_ON_TDS, Me.PAID_DATE, Me.PAID_TDS, Me.INTEREST_PAID_ON_TDS, Me.PENALTY_PAID_ON_TDS, Me.DED_TR_SR_NO, Me.DED_TR_M_ID, Me.RECD_TR_SR_NO, Me.RECD_TDS_M_ID, Me.PAID_TR_SR_NO, Me.PAID_TDS_M_ID, Me.REC_ADD_BY, Me.REC_ADD_ON, Me.REC_EDIT_BY, Me.REC_EDIT_ON, Me.ACTION_STATUS, Me.REC_STATUS_BY, Me.REC_STATUS_ON, Me.Receivable_Center, Me.Received_Excess_Center})
        Me.BandedGridView1.FixedLineWidth = 1
        Me.BandedGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.BandedGridView1.GridControl = Me.GridControl1
        Me.BandedGridView1.GroupFormat = "{1}"
        Me.BandedGridView1.Name = "BandedGridView1"
        Me.BandedGridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.BandedGridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.BandedGridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.BandedGridView1.OptionsBehavior.Editable = False
        Me.BandedGridView1.OptionsDetail.AllowExpandEmptyDetails = True
        Me.BandedGridView1.OptionsDetail.AutoZoomDetail = True
        Me.BandedGridView1.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled
        Me.BandedGridView1.OptionsDetail.SmartDetailHeight = True
        Me.BandedGridView1.OptionsFilter.AllowColumnMRUFilterList = False
        Me.BandedGridView1.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.BandedGridView1.OptionsFilter.AllowMRUFilterList = False
        Me.BandedGridView1.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False
        Me.BandedGridView1.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = False
        Me.BandedGridView1.OptionsMenu.EnableFooterMenu = False
        Me.BandedGridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.BandedGridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.BandedGridView1.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.BandedGridView1.OptionsNavigation.AutoMoveRowFocus = False
        Me.BandedGridView1.OptionsNavigation.EnterMoveNextColumn = True
        Me.BandedGridView1.OptionsPrint.PrintDetails = True
        Me.BandedGridView1.OptionsSelection.InvertSelection = True
        Me.BandedGridView1.OptionsView.ColumnAutoWidth = False
        Me.BandedGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.BandedGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.BandedGridView1.OptionsView.ShowFooter = True
        Me.BandedGridView1.PreviewFieldName = "LED_NAME"
        '
        'GridBand1
        '
        Me.GridBand1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridBand1.AppearanceHeader.Options.UseFont = True
        Me.GridBand1.Caption = "Particulars"
        Me.GridBand1.Columns.Add(Me.RECD_TDS_M_ID)
        Me.GridBand1.Columns.Add(Me.PAID_TDS_M_ID)
        Me.GridBand1.Columns.Add(Me.DED_TR_M_ID)
        Me.GridBand1.Columns.Add(Me.Center)
        Me.GridBand1.Columns.Add(Me.UID)
        Me.GridBand1.Columns.Add(Me.PARTY)
        Me.GridBand1.Columns.Add(Me.PAN)
        Me.GridBand1.Columns.Add(Me.ACTION_STATUS)
        Me.GridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.OptionsBand.FixedWidth = True
        Me.GridBand1.VisibleIndex = 0
        Me.GridBand1.Width = 450
        '
        'RECD_TDS_M_ID
        '
        Me.RECD_TDS_M_ID.Caption = "RECD_TDS_M_ID"
        Me.RECD_TDS_M_ID.FieldName = "RECD_TDS_M_ID"
        Me.RECD_TDS_M_ID.Name = "RECD_TDS_M_ID"
        '
        'PAID_TDS_M_ID
        '
        Me.PAID_TDS_M_ID.Caption = "PAID_TDS_M_ID"
        Me.PAID_TDS_M_ID.FieldName = "PAID_TDS_M_ID"
        Me.PAID_TDS_M_ID.Name = "PAID_TDS_M_ID"
        '
        'DED_TR_M_ID
        '
        Me.DED_TR_M_ID.Caption = "DED_TR_M_ID"
        Me.DED_TR_M_ID.FieldName = "DED_TR_M_ID"
        Me.DED_TR_M_ID.Name = "DED_TR_M_ID"
        '
        'Center
        '
        Me.Center.Caption = "Center"
        Me.Center.FieldName = "Center"
        Me.Center.Name = "Center"
        Me.Center.Visible = True
        Me.Center.Width = 162
        '
        'UID
        '
        Me.UID.Caption = "UID"
        Me.UID.FieldName = "UID"
        Me.UID.Name = "UID"
        Me.UID.Visible = True
        Me.UID.Width = 97
        '
        'PARTY
        '
        Me.PARTY.Caption = "Party"
        Me.PARTY.FieldName = "PARTY"
        Me.PARTY.Name = "PARTY"
        Me.PARTY.Visible = True
        Me.PARTY.Width = 116
        '
        'PAN
        '
        Me.PAN.Caption = "PAN"
        Me.PAN.FieldName = "PAN"
        Me.PAN.Name = "PAN"
        Me.PAN.Visible = True
        '
        'ACTION_STATUS
        '
        Me.ACTION_STATUS.Caption = "ACTION_STATUS"
        Me.ACTION_STATUS.FieldName = "ACTION_STATUS"
        Me.ACTION_STATUS.Name = "ACTION_STATUS"
        '
        'GridBand3
        '
        Me.GridBand3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridBand3.AppearanceHeader.BackColor2 = System.Drawing.Color.Blue
        Me.GridBand3.AppearanceHeader.BorderColor = System.Drawing.Color.Red
        Me.GridBand3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridBand3.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GridBand3.AppearanceHeader.Options.UseBackColor = True
        Me.GridBand3.AppearanceHeader.Options.UseBorderColor = True
        Me.GridBand3.AppearanceHeader.Options.UseFont = True
        Me.GridBand3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridBand3.Caption = "TDS Deduction"
        Me.GridBand3.Columns.Add(Me.TDS_CODE)
        Me.GridBand3.Columns.Add(Me.ACTUAL_DED_DATE)
        Me.GridBand3.Columns.Add(Me.DECLARED_DED_DATE)
        Me.GridBand3.Columns.Add(Me.BILL)
        Me.GridBand3.Columns.Add(Me.TDS_DED)
        Me.GridBand3.Name = "GridBand3"
        Me.GridBand3.OptionsBand.FixedWidth = True
        Me.GridBand3.VisibleIndex = 1
        Me.GridBand3.Width = 426
        '
        'TDS_CODE
        '
        Me.TDS_CODE.Caption = "TDS Code"
        Me.TDS_CODE.FieldName = "TDS_CODE"
        Me.TDS_CODE.Name = "TDS_CODE"
        Me.TDS_CODE.Visible = True
        '
        'ACTUAL_DED_DATE
        '
        Me.ACTUAL_DED_DATE.Caption = "Actual Ded. Date"
        Me.ACTUAL_DED_DATE.DisplayFormat.FormatString = "d"
        Me.ACTUAL_DED_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ACTUAL_DED_DATE.FieldName = "ACTUAL_DED_DATE"
        Me.ACTUAL_DED_DATE.Name = "ACTUAL_DED_DATE"
        Me.ACTUAL_DED_DATE.Visible = True
        Me.ACTUAL_DED_DATE.Width = 103
        '
        'DECLARED_DED_DATE
        '
        Me.DECLARED_DED_DATE.Caption = "Declared Ded. Date"
        Me.DECLARED_DED_DATE.DisplayFormat.FormatString = "d"
        Me.DECLARED_DED_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DECLARED_DED_DATE.FieldName = "DECLARED_DED_DATE"
        Me.DECLARED_DED_DATE.Name = "DECLARED_DED_DATE"
        Me.DECLARED_DED_DATE.Visible = True
        Me.DECLARED_DED_DATE.Width = 104
        '
        'BILL
        '
        Me.BILL.Caption = "Bill Amt."
        Me.BILL.DisplayFormat.FormatString = "#,0.00"
        Me.BILL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BILL.FieldName = "BILL"
        Me.BILL.Name = "BILL"
        Me.BILL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.BILL.Visible = True
        Me.BILL.Width = 72
        '
        'TDS_DED
        '
        Me.TDS_DED.Caption = "TDS Ded."
        Me.TDS_DED.DisplayFormat.FormatString = "#,0.00"
        Me.TDS_DED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TDS_DED.FieldName = "TDS_DED"
        Me.TDS_DED.Name = "TDS_DED"
        Me.TDS_DED.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.TDS_DED.Visible = True
        Me.TDS_DED.Width = 72
        '
        'GridBand2
        '
        Me.GridBand2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridBand2.AppearanceHeader.Options.UseFont = True
        Me.GridBand2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridBand2.Caption = "TDS Recd. At HQ"
        Me.GridBand2.Columns.Add(Me.RECD_DATE)
        Me.GridBand2.Columns.Add(Me.RECD_TDS)
        Me.GridBand2.Columns.Add(Me.INTEREST_RECD_ON_TDS)
        Me.GridBand2.Columns.Add(Me.PENALTY_RECD_ON_TDS)
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.OptionsBand.FixedWidth = True
        Me.GridBand2.VisibleIndex = 2
        Me.GridBand2.Width = 300
        '
        'RECD_DATE
        '
        Me.RECD_DATE.Caption = "Recd on"
        Me.RECD_DATE.DisplayFormat.FormatString = "d"
        Me.RECD_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RECD_DATE.FieldName = "RECD_DATE"
        Me.RECD_DATE.Name = "RECD_DATE"
        Me.RECD_DATE.Visible = True
        '
        'RECD_TDS
        '
        Me.RECD_TDS.Caption = "TDS"
        Me.RECD_TDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RECD_TDS.FieldName = "RECD_TDS"
        Me.RECD_TDS.Name = "RECD_TDS"
        Me.RECD_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.RECD_TDS.Visible = True
        '
        'INTEREST_RECD_ON_TDS
        '
        Me.INTEREST_RECD_ON_TDS.Caption = "Interet"
        Me.INTEREST_RECD_ON_TDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INTEREST_RECD_ON_TDS.FieldName = "INTEREST_RECD_ON_TDS"
        Me.INTEREST_RECD_ON_TDS.Name = "INTEREST_RECD_ON_TDS"
        Me.INTEREST_RECD_ON_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.INTEREST_RECD_ON_TDS.Visible = True
        '
        'PENALTY_RECD_ON_TDS
        '
        Me.PENALTY_RECD_ON_TDS.Caption = "Penalty"
        Me.PENALTY_RECD_ON_TDS.FieldName = "PENALTY_RECD_ON_TDS"
        Me.PENALTY_RECD_ON_TDS.Name = "PENALTY_RECD_ON_TDS"
        Me.PENALTY_RECD_ON_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PENALTY_RECD_ON_TDS.Visible = True
        '
        'gridBand4
        '
        Me.gridBand4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gridBand4.AppearanceHeader.Options.UseFont = True
        Me.gridBand4.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand4.Caption = "TDS Paid to Govt."
        Me.gridBand4.Columns.Add(Me.PAID_DATE)
        Me.gridBand4.Columns.Add(Me.PAID_TDS)
        Me.gridBand4.Columns.Add(Me.INTEREST_PAID_ON_TDS)
        Me.gridBand4.Columns.Add(Me.PENALTY_PAID_ON_TDS)
        Me.gridBand4.Name = "gridBand4"
        Me.gridBand4.VisibleIndex = 3
        Me.gridBand4.Width = 300
        '
        'PAID_DATE
        '
        Me.PAID_DATE.Caption = "Paid On"
        Me.PAID_DATE.FieldName = "PAID_DATE"
        Me.PAID_DATE.Name = "PAID_DATE"
        Me.PAID_DATE.Visible = True
        '
        'PAID_TDS
        '
        Me.PAID_TDS.Caption = "TDS"
        Me.PAID_TDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PAID_TDS.FieldName = "PAID_TDS"
        Me.PAID_TDS.Name = "PAID_TDS"
        Me.PAID_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PAID_TDS.Visible = True
        '
        'INTEREST_PAID_ON_TDS
        '
        Me.INTEREST_PAID_ON_TDS.Caption = "Interest"
        Me.INTEREST_PAID_ON_TDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INTEREST_PAID_ON_TDS.FieldName = "INTEREST_PAID_ON_TDS"
        Me.INTEREST_PAID_ON_TDS.Name = "INTEREST_PAID_ON_TDS"
        Me.INTEREST_PAID_ON_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.INTEREST_PAID_ON_TDS.Visible = True
        '
        'PENALTY_PAID_ON_TDS
        '
        Me.PENALTY_PAID_ON_TDS.Caption = "Penalty"
        Me.PENALTY_PAID_ON_TDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PENALTY_PAID_ON_TDS.FieldName = "PENALTY_PAID_ON_TDS"
        Me.PENALTY_PAID_ON_TDS.Name = "PENALTY_PAID_ON_TDS"
        Me.PENALTY_PAID_ON_TDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PENALTY_PAID_ON_TDS.Visible = True
        '
        'Balances
        '
        Me.Balances.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Balances.AppearanceHeader.Options.UseFont = True
        Me.Balances.AppearanceHeader.Options.UseTextOptions = True
        Me.Balances.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Balances.Caption = "Balances"
        Me.Balances.Columns.Add(Me.Receivable_Center)
        Me.Balances.Columns.Add(Me.Received_Excess_Center)
        Me.Balances.Name = "Balances"
        Me.Balances.VisibleIndex = 4
        Me.Balances.Width = 167
        '
        'Receivable_Center
        '
        Me.Receivable_Center.Caption = "Due from Center"
        Me.Receivable_Center.FieldName = "Receivable_Center"
        Me.Receivable_Center.Name = "Receivable_Center"
        Me.Receivable_Center.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.Receivable_Center.Visible = True
        Me.Receivable_Center.Width = 92
        '
        'Received_Excess_Center
        '
        Me.Received_Excess_Center.Caption = "Excess Recd."
        Me.Received_Excess_Center.FieldName = "Received_Excess_Center"
        Me.Received_Excess_Center.Name = "Received_Excess_Center"
        Me.Received_Excess_Center.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.Received_Excess_Center.Visible = True
        '
        'REC_ADD_BY
        '
        Me.REC_ADD_BY.Caption = "REC_ADD_BY"
        Me.REC_ADD_BY.FieldName = "REC_ADD_BY"
        Me.REC_ADD_BY.Name = "REC_ADD_BY"
        '
        'REC_ADD_ON
        '
        Me.REC_ADD_ON.Caption = "REC_ADD_ON"
        Me.REC_ADD_ON.FieldName = "REC_ADD_ON"
        Me.REC_ADD_ON.Name = "REC_ADD_ON"
        '
        'REC_EDIT_BY
        '
        Me.REC_EDIT_BY.Caption = "REC_EDIT_BY"
        Me.REC_EDIT_BY.FieldName = "REC_EDIT_BY"
        Me.REC_EDIT_BY.Name = "REC_EDIT_BY"
        '
        'REC_EDIT_ON
        '
        Me.REC_EDIT_ON.Caption = "REC_EDIT_ON"
        Me.REC_EDIT_ON.FieldName = "REC_EDIT_ON"
        Me.REC_EDIT_ON.Name = "REC_EDIT_ON"
        '
        'REC_STATUS_BY
        '
        Me.REC_STATUS_BY.Caption = "REC_STATUS_BY"
        Me.REC_STATUS_BY.FieldName = "REC_STATUS_BY"
        Me.REC_STATUS_BY.Name = "REC_STATUS_BY"
        '
        'REC_STATUS_ON
        '
        Me.REC_STATUS_ON.Caption = "REC_STATUS_ON"
        Me.REC_STATUS_ON.FieldName = "REC_STATUS_ON"
        Me.REC_STATUS_ON.Name = "REC_STATUS_ON"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AppearanceFocused.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemTextEdit1.AppearanceFocused.Options.UseFont = True
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Mask.BeepOnError = True
        Me.RepositoryItemTextEdit1.Mask.EditMask = "f"
        Me.RepositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'DED_TR_SR_NO
        '
        Me.DED_TR_SR_NO.Caption = "DED_TR_SR_NO"
        Me.DED_TR_SR_NO.FieldName = "DED_TR_SR_NO"
        Me.DED_TR_SR_NO.Name = "DED_TR_SR_NO"
        '
        'RECD_TR_SR_NO
        '
        Me.RECD_TR_SR_NO.Caption = "RECD_TR_SR_NO"
        Me.RECD_TR_SR_NO.FieldName = "RECD_TR_SR_NO"
        Me.RECD_TR_SR_NO.Name = "RECD_TR_SR_NO"
        '
        'PAID_TR_SR_NO
        '
        Me.PAID_TR_SR_NO.Caption = "PAID_TR_SR_NO"
        Me.PAID_TR_SR_NO.FieldName = "PAID_TR_SR_NO"
        Me.PAID_TR_SR_NO.Name = "PAID_TR_SR_NO"
        '
        'Frm_TDS_Register
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 452)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Pic_Status)
        Me.Controls.Add(Me.Lbl_Seprator1)
        Me.Controls.Add(Me.Lbl_Seprator3)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.Lbl_Status)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Lbl_Seprator2)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Lbl_Create)
        Me.Controls.Add(Me.Lbl_Modify)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.Name = "Frm_TDS_Register"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TDS Register"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Lbl_ToolMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BUT_CLOSE As DevExpress.XtraEditors.SimpleButton
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
    Friend WithEvents T_MAP_RECD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents UID As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ACTUAL_DED_DATE As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BILL As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents Center As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ACTION_STATUS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents DECLARED_DED_DATE As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents TDS_DED As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RECD_DATE As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RECD_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INTEREST_RECD_ON_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PENALTY_RECD_ON_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PAID_DATE As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PAID_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INTEREST_PAID_ON_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PENALTY_PAID_ON_TDS As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents DED_TR_M_ID As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RECD_TDS_M_ID As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PAID_TDS_M_ID As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_ADD_BY As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_ADD_ON As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_EDIT_BY As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_EDIT_ON As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_STATUS_BY As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents REC_STATUS_ON As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents T_MAP_PAID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Receivable_Center As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents Received_Excess_Center As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PARTY As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents T_UNMAP_RECD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_UNMAP_PAID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PAN As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents TDS_CODE As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents T_UNMAP_DED As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents Balances As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents DED_TR_SR_NO As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RECD_TR_SR_NO As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PAID_TR_SR_NO As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

End Class
