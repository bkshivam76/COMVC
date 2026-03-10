<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Party_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Party_Report))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipSeparatorItem1 As DevExpress.Utils.ToolTipSeparatorItem = New DevExpress.Utils.ToolTipSeparatorItem()
        Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me._Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Particulars = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Item_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Debit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Credit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Balance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TR_M_ID = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNetBalance = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.Cmb_View = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BE_View_Period = New DevExpress.XtraEditors.ButtonEdit()
        Me.BUT_SHOW = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GLookUp_PartyList = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GLookUp_PartyListView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PartyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_PartyList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_PartyListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BUT_CLOSE
        '
        Me.BUT_CLOSE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CLOSE.Image = CType(resources.GetObject("BUT_CLOSE.Image"), System.Drawing.Image)
        Me.BUT_CLOSE.Location = New System.Drawing.Point(726, 4)
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
        Me.StatusStrip1.Size = New System.Drawing.Size(800, 18)
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
        Me.Lbl_ToolMsg.Size = New System.Drawing.Size(785, 13)
        Me.Lbl_ToolMsg.Spring = True
        Me.Lbl_ToolMsg.Text = "Ready"
        Me.Lbl_ToolMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Zoom1
        '
        Me.Zoom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Zoom1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Zoom1.EditValue = 8
        Me.Zoom1.Location = New System.Drawing.Point(646, 410)
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
        Me.GridControl1.Location = New System.Drawing.Point(-1, 54)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(802, 351)
        Me.GridControl1.TabIndex = 3
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.GridView1.Appearance.Preview.Options.UseForeColor = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me._Date, Me._Particulars, Me._Item_Name, Me._Debit, Me._Credit, Me._Balance, Me.TR_M_ID})
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
        Me.GridView1.OptionsView.AutoCalcPreviewLineCount = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowPreview = True
        Me.GridView1.PreviewFieldName = "NARRATION"
        Me.GridView1.ViewCaption = "Group"
        '
        '_Date
        '
        Me._Date.Caption = "Date"
        Me._Date.FieldName = "Date"
        Me._Date.Name = "_Date"
        Me._Date.Visible = True
        Me._Date.VisibleIndex = 0
        '
        '_Particulars
        '
        Me._Particulars.Caption = "Particulars"
        Me._Particulars.FieldName = "Particulars"
        Me._Particulars.Name = "_Particulars"
        Me._Particulars.Visible = True
        Me._Particulars.VisibleIndex = 1
        Me._Particulars.Width = 250
        '
        '_Item_Name
        '
        Me._Item_Name.Caption = "Item Name"
        Me._Item_Name.FieldName = "Item Name"
        Me._Item_Name.Name = "_Item_Name"
        Me._Item_Name.Visible = True
        Me._Item_Name.VisibleIndex = 2
        Me._Item_Name.Width = 125
        '
        '_Debit
        '
        Me._Debit.AppearanceCell.Options.UseTextOptions = True
        Me._Debit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me._Debit.Caption = "Debit"
        Me._Debit.FieldName = "Debit"
        Me._Debit.Name = "_Debit"
        Me._Debit.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me._Debit.Visible = True
        Me._Debit.VisibleIndex = 3
        Me._Debit.Width = 100
        '
        '_Credit
        '
        Me._Credit.AppearanceCell.Options.UseTextOptions = True
        Me._Credit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me._Credit.Caption = "Credit"
        Me._Credit.FieldName = "Credit"
        Me._Credit.Name = "_Credit"
        Me._Credit.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me._Credit.Visible = True
        Me._Credit.VisibleIndex = 4
        Me._Credit.Width = 100
        '
        '_Balance
        '
        Me._Balance.AppearanceCell.Options.UseTextOptions = True
        Me._Balance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me._Balance.Caption = "Balance"
        Me._Balance.FieldName = "Balance"
        Me._Balance.Name = "_Balance"
        Me._Balance.Visible = True
        Me._Balance.VisibleIndex = 5
        Me._Balance.Width = 100
        '
        'TR_M_ID
        '
        Me.TR_M_ID.Caption = "TR_M_ID"
        Me.TR_M_ID.FieldName = "TR_M_ID"
        Me.TR_M_ID.Name = "TR_M_ID"
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
        Me.Panel1.Size = New System.Drawing.Size(800, 34)
        Me.Panel1.TabIndex = 43
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(131, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Party Ledger"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(632, 4)
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
        Me.Lbl_Separator3.Location = New System.Drawing.Point(641, 405)
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
        Me.But_Find.Location = New System.Drawing.Point(580, 407)
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
        Me.But_Filter.Location = New System.Drawing.Point(519, 407)
        Me.But_Filter.LookAndFeel.SkinName = "iMaginary"
        Me.But_Filter.LookAndFeel.UseDefaultLookAndFeel = False
        Me.But_Filter.Name = "But_Filter"
        Me.But_Filter.Size = New System.Drawing.Size(60, 25)
        Me.But_Filter.TabIndex = 140
        Me.But_Filter.Text = "Filter"
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
        Me.Lbl_Separator2.Location = New System.Drawing.Point(515, 405)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Separator2.TabIndex = 144
        '
        'lblNetBalance
        '
        Me.lblNetBalance.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetBalance.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNetBalance.Location = New System.Drawing.Point(101, 412)
        Me.lblNetBalance.Name = "lblNetBalance"
        Me.lblNetBalance.Size = New System.Drawing.Size(9, 14)
        Me.lblNetBalance.TabIndex = 256
        Me.lblNetBalance.Text = "0"
        '
        'LabelControl31
        '
        Me.LabelControl31.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl31.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl31.Location = New System.Drawing.Point(12, 412)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Size = New System.Drawing.Size(85, 14)
        Me.LabelControl31.TabIndex = 257
        Me.LabelControl31.Text = "Nett Balance:"
        '
        'Cmb_View
        '
        Me.Cmb_View.EditValue = ""
        Me.Cmb_View.EnterMoveNextControl = True
        Me.Cmb_View.Location = New System.Drawing.Point(270, 34)
        Me.Cmb_View.Name = "Cmb_View"
        Me.Cmb_View.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.Appearance.Options.UseFont = True
        Me.Cmb_View.Properties.Appearance.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Cmb_View.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
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
        Me.Cmb_View.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem1.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem1.Appearance.Options.UseImage = True
        ToolTipTitleItem1.Image = CType(resources.GetObject("ToolTipTitleItem1.Image"), System.Drawing.Image)
        ToolTipTitleItem1.Text = "To set specific period..."
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "        Shortcut Key"
        ToolTipTitleItem2.LeftIndent = 6
        ToolTipTitleItem2.Text = "          Ctrl + F2"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        SuperToolTip1.Items.Add(ToolTipTitleItem2)
        Me.Cmb_View.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Change Period", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, SuperToolTip1, True)})
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
        Me.Cmb_View.TabIndex = 1
        '
        'BE_View_Period
        '
        Me.BE_View_Period.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BE_View_Period.EditValue = "View Period"
        Me.BE_View_Period.EnterMoveNextControl = True
        Me.BE_View_Period.Location = New System.Drawing.Point(539, 35)
        Me.BE_View_Period.Name = "BE_View_Period"
        Me.BE_View_Period.Properties.AllowFocused = False
        Me.BE_View_Period.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.BE_View_Period.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BE_View_Period.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_View_Period.Properties.Appearance.Options.UseBackColor = True
        Me.BE_View_Period.Properties.Appearance.Options.UseFont = True
        Me.BE_View_Period.Properties.Appearance.Options.UseForeColor = True
        Me.BE_View_Period.Properties.Appearance.Options.UseTextOptions = True
        Me.BE_View_Period.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BE_View_Period.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BE_View_Period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_View_Period.Size = New System.Drawing.Size(260, 18)
        Me.BE_View_Period.TabIndex = 265
        '
        'BUT_SHOW
        '
        Me.BUT_SHOW.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SHOW.Appearance.Options.UseFont = True
        Me.BUT_SHOW.Image = CType(resources.GetObject("BUT_SHOW.Image"), System.Drawing.Image)
        Me.BUT_SHOW.Location = New System.Drawing.Point(479, 34)
        Me.BUT_SHOW.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_SHOW.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SHOW.Name = "BUT_SHOW"
        Me.BUT_SHOW.Size = New System.Drawing.Size(57, 21)
        Me.BUT_SHOW.TabIndex = 2
        Me.BUT_SHOW.Text = "Show"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.LabelControl1.Location = New System.Drawing.Point(4, 37)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl1.TabIndex = 263
        Me.LabelControl1.Text = "Party:"
        '
        'GLookUp_PartyList
        '
        Me.GLookUp_PartyList.EnterMoveNextControl = True
        Me.GLookUp_PartyList.Location = New System.Drawing.Point(44, 34)
        Me.GLookUp_PartyList.Name = "GLookUp_PartyList"
        Me.GLookUp_PartyList.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GLookUp_PartyList.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.GLookUp_PartyList.Properties.Appearance.Options.UseFont = True
        Me.GLookUp_PartyList.Properties.Appearance.Options.UseForeColor = True
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GLookUp_PartyList.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.GLookUp_PartyList.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.GLookUp_PartyList.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_PartyList.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GLookUp_PartyList.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GLookUp_PartyList.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.GLookUp_PartyList.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_PartyList.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.GLookUp_PartyList.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GLookUp_PartyList.Properties.AppearanceFocused.Options.UseFont = True
        Me.GLookUp_PartyList.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GLookUp_PartyList.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem3.Text = "Advanced Filter (On/Off)"
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "Enabled : Auto Filter Bar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Column Filter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Custom Filter Edito" & _
    "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Footer Filter Display"
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        SuperToolTip2.Items.Add(ToolTipItem2)
        Me.GLookUp_PartyList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Filter : Off", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "OFF", SuperToolTip2, True)})
        Me.GLookUp_PartyList.Properties.ImmediatePopup = True
        Me.GLookUp_PartyList.Properties.NullText = ""
        Me.GLookUp_PartyList.Properties.NullValuePrompt = "Select Party..."
        Me.GLookUp_PartyList.Properties.NullValuePromptShowForEmptyValue = True
        Me.GLookUp_PartyList.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GLookUp_PartyList.Properties.PopupFormMinSize = New System.Drawing.Size(484, 250)
        Me.GLookUp_PartyList.Properties.PopupFormSize = New System.Drawing.Size(484, 250)
        Me.GLookUp_PartyList.Properties.ReadOnly = True
        Me.GLookUp_PartyList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GLookUp_PartyList.Properties.View = Me.GLookUp_PartyListView
        Me.GLookUp_PartyList.Size = New System.Drawing.Size(227, 20)
        ToolTipTitleItem4.Text = "Information..."
        ToolTipItem3.LeftIndent = 6
        ToolTipItem3.Text = "Select Item Name.."
        ToolTipTitleItem5.LeftIndent = 6
        ToolTipTitleItem5.Text = "Use F4 function key to Show List."
        SuperToolTip3.Items.Add(ToolTipTitleItem4)
        SuperToolTip3.Items.Add(ToolTipItem3)
        SuperToolTip3.Items.Add(ToolTipSeparatorItem1)
        SuperToolTip3.Items.Add(ToolTipTitleItem5)
        Me.GLookUp_PartyList.SuperTip = SuperToolTip3
        Me.GLookUp_PartyList.TabIndex = 0
        '
        'GLookUp_PartyListView
        '
        Me.GLookUp_PartyListView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.PartyName, Me.PAN, Me.ID})
        Me.GLookUp_PartyListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GLookUp_PartyListView.Name = "GLookUp_PartyListView"
        Me.GLookUp_PartyListView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_PartyListView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_PartyListView.OptionsBehavior.ReadOnly = True
        Me.GLookUp_PartyListView.OptionsCustomization.AllowFilter = False
        Me.GLookUp_PartyListView.OptionsCustomization.AllowGroup = False
        Me.GLookUp_PartyListView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GLookUp_PartyListView.OptionsLayout.Columns.AddNewColumns = False
        Me.GLookUp_PartyListView.OptionsLayout.Columns.RemoveOldColumns = False
        Me.GLookUp_PartyListView.OptionsMenu.EnableColumnMenu = False
        Me.GLookUp_PartyListView.OptionsMenu.EnableFooterMenu = False
        Me.GLookUp_PartyListView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GLookUp_PartyListView.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GLookUp_PartyListView.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GLookUp_PartyListView.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.GLookUp_PartyListView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GLookUp_PartyListView.OptionsSelection.InvertSelection = True
        Me.GLookUp_PartyListView.OptionsView.EnableAppearanceEvenRow = True
        Me.GLookUp_PartyListView.OptionsView.EnableAppearanceOddRow = True
        Me.GLookUp_PartyListView.OptionsView.ShowGroupPanel = False
        Me.GLookUp_PartyListView.OptionsView.ShowIndicator = False
        '
        'PartyName
        '
        Me.PartyName.Caption = "Name"
        Me.PartyName.FieldName = "Name"
        Me.PartyName.Name = "PartyName"
        Me.PartyName.Visible = True
        Me.PartyName.VisibleIndex = 0
        '
        'PAN
        '
        Me.PAN.Caption = "PAN"
        Me.PAN.FieldName = "PAN"
        Me.PAN.Name = "PAN"
        Me.PAN.Visible = True
        Me.PAN.VisibleIndex = 1
        '
        'ID
        '
        Me.ID.Caption = "ID"
        Me.ID.FieldName = "ID"
        Me.ID.Name = "ID"
        '
        'Frm_Party_Report
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 452)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.GLookUp_PartyList)
        Me.Controls.Add(Me.Cmb_View)
        Me.Controls.Add(Me.BE_View_Period)
        Me.Controls.Add(Me.BUT_SHOW)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblNetBalance)
        Me.Controls.Add(Me.LabelControl31)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.Lbl_Separator3)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "Frm_Party_Report"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Party Ledger"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_PartyList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_PartyListView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Particulars As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Item_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Debit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Credit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Balance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TR_M_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblNetBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Cmb_View As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BE_View_Period As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BUT_SHOW As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GLookUp_PartyList As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GLookUp_PartyListView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ID As DevExpress.XtraGrid.Columns.GridColumn


End Class
