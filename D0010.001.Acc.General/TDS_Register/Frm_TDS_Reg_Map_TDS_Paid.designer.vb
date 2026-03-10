<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TDS_Reg_Map_TDS_Paid
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_TDS_Reg_Map_TDS_Paid))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Center = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Cen_UID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Txn_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Party = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Dr_Amount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TDS_Deducted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Remaining_Amount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TDS_Paid_Already = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TDS_Paid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.declared_date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.REC_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SR_NO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblCount = New DevExpress.XtraEditors.LabelControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.BUT_ACCEPT = New DevExpress.XtraEditors.SimpleButton()
        Me.Hyper_Request = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblOnScreenSelection = New DevExpress.XtraEditors.TextEdit()
        Me.lblNetBalance = New DevExpress.XtraEditors.TextEdit()
        Me.lblMentionedInvoucher = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.Txt_Fr_Date = New DevExpress.XtraEditors.DateEdit()
        Me.BUT_OK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.lblOnScreenSelection.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMentionedInvoucher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Fr_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        GridLevelNode1.RelationName = "Level1"
        Me.GridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GridControl1.Location = New System.Drawing.Point(-1, 58)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.RepositoryItemTextEdit1, Me.RepositoryItemDateEdit1})
        Me.GridControl1.Size = New System.Drawing.Size(903, 235)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Center, Me.Cen_UID, Me.Txn_Date, Me.Party, Me.Dr_Amount, Me.TDS_Deducted, Me.Remaining_Amount, Me.TDS_Paid_Already, Me.TDS_Paid, Me.declared_date, Me.REC_ID, Me.SR_NO})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupFormat = "{1} "
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace
        Me.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
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
        'Center
        '
        Me.Center.Caption = "Center"
        Me.Center.FieldName = "Center"
        Me.Center.Name = "Center"
        Me.Center.Visible = True
        Me.Center.VisibleIndex = 0
        Me.Center.Width = 100
        '
        'Cen_UID
        '
        Me.Cen_UID.Caption = "Cen_UID"
        Me.Cen_UID.FieldName = "Cen_UID"
        Me.Cen_UID.Name = "Cen_UID"
        Me.Cen_UID.Visible = True
        Me.Cen_UID.VisibleIndex = 1
        '
        'Txn_Date
        '
        Me.Txn_Date.Caption = "Txn Date"
        Me.Txn_Date.FieldName = "Txn_Date"
        Me.Txn_Date.Name = "Txn_Date"
        Me.Txn_Date.OptionsColumn.AllowEdit = False
        Me.Txn_Date.OptionsColumn.ReadOnly = True
        Me.Txn_Date.Visible = True
        Me.Txn_Date.VisibleIndex = 2
        '
        'Party
        '
        Me.Party.Caption = "Party"
        Me.Party.FieldName = "Party"
        Me.Party.Name = "Party"
        Me.Party.OptionsColumn.AllowEdit = False
        Me.Party.OptionsColumn.ReadOnly = True
        Me.Party.Visible = True
        Me.Party.VisibleIndex = 3
        Me.Party.Width = 150
        '
        'Dr_Amount
        '
        Me.Dr_Amount.Caption = "Dr Amount"
        Me.Dr_Amount.FieldName = "Dr Amount"
        Me.Dr_Amount.Name = "Dr_Amount"
        Me.Dr_Amount.OptionsColumn.AllowEdit = False
        Me.Dr_Amount.OptionsColumn.ReadOnly = True
        Me.Dr_Amount.Visible = True
        Me.Dr_Amount.VisibleIndex = 4
        Me.Dr_Amount.Width = 70
        '
        'TDS_Deducted
        '
        Me.TDS_Deducted.Caption = "TDS Deducted"
        Me.TDS_Deducted.FieldName = "TDS_Deducted"
        Me.TDS_Deducted.Name = "TDS_Deducted"
        Me.TDS_Deducted.OptionsColumn.AllowEdit = False
        Me.TDS_Deducted.OptionsColumn.ReadOnly = True
        Me.TDS_Deducted.Visible = True
        Me.TDS_Deducted.VisibleIndex = 5
        Me.TDS_Deducted.Width = 80
        '
        'Remaining_Amount
        '
        Me.Remaining_Amount.Caption = "Remaining Amount"
        Me.Remaining_Amount.FieldName = "Remaining_Amount"
        Me.Remaining_Amount.Name = "Remaining_Amount"
        Me.Remaining_Amount.OptionsColumn.AllowEdit = False
        Me.Remaining_Amount.OptionsColumn.ReadOnly = True
        Me.Remaining_Amount.Visible = True
        Me.Remaining_Amount.VisibleIndex = 6
        Me.Remaining_Amount.Width = 80
        '
        'TDS_Paid_Already
        '
        Me.TDS_Paid_Already.Caption = "TDS Already Paid to Govt."
        Me.TDS_Paid_Already.FieldName = "TDS_Already_Paid"
        Me.TDS_Paid_Already.Name = "TDS_Paid_Already"
        Me.TDS_Paid_Already.OptionsColumn.AllowEdit = False
        Me.TDS_Paid_Already.OptionsColumn.ReadOnly = True
        Me.TDS_Paid_Already.Visible = True
        Me.TDS_Paid_Already.VisibleIndex = 7
        Me.TDS_Paid_Already.Width = 125
        '
        'TDS_Paid
        '
        Me.TDS_Paid.AppearanceCell.BackColor = System.Drawing.Color.Gold
        Me.TDS_Paid.AppearanceCell.BorderColor = System.Drawing.Color.Black
        Me.TDS_Paid.AppearanceCell.Options.UseBackColor = True
        Me.TDS_Paid.AppearanceCell.Options.UseBorderColor = True
        Me.TDS_Paid.Caption = "TDS Being Paid"
        Me.TDS_Paid.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.TDS_Paid.FieldName = "TDS_Paid"
        Me.TDS_Paid.Name = "TDS_Paid"
        Me.TDS_Paid.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.TDS_Paid.Visible = True
        Me.TDS_Paid.VisibleIndex = 8
        Me.TDS_Paid.Width = 100
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "f2"
        Me.RepositoryItemTextEdit1.EditFormat.FormatString = "f2"
        Me.RepositoryItemTextEdit1.Mask.EditMask = "f2"
        Me.RepositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'declared_date
        '
        Me.declared_date.AppearanceCell.BackColor = System.Drawing.Color.Gold
        Me.declared_date.AppearanceCell.BorderColor = System.Drawing.Color.Black
        Me.declared_date.AppearanceCell.Options.UseBackColor = True
        Me.declared_date.AppearanceCell.Options.UseBorderColor = True
        Me.declared_date.Caption = "Declared Ded. Date"
        Me.declared_date.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.declared_date.FieldName = "declared_date"
        Me.declared_date.Name = "declared_date"
        Me.declared_date.UnboundType = DevExpress.Data.UnboundColumnType.DateTime
        Me.declared_date.Width = 125
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'REC_ID
        '
        Me.REC_ID.Caption = "REC_ID"
        Me.REC_ID.FieldName = "REC_ID"
        Me.REC_ID.Name = "REC_ID"
        '
        'SR_NO
        '
        Me.SR_NO.Caption = "SR_NO"
        Me.SR_NO.FieldName = "SR_NO"
        Me.SR_NO.Name = "SR_NO"
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lblCount)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 34)
        Me.Panel1.TabIndex = 43
        '
        'lblCount
        '
        Me.lblCount.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(642, 11)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 14)
        Me.lblCount.TabIndex = 148
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
        Me.Txt_TitleX.Size = New System.Drawing.Size(568, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Unmapped TDS Deduction Entries for TDS Paid to Govt."
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.T_Print, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 60)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(173, 6)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(176, 22)
        Me.T_Print.Text = "&List Preview"
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
        'BUT_ACCEPT
        '
        Me.BUT_ACCEPT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_ACCEPT.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_ACCEPT.Appearance.Options.UseFont = True
        Me.BUT_ACCEPT.Image = CType(resources.GetObject("BUT_ACCEPT.Image"), System.Drawing.Image)
        Me.BUT_ACCEPT.Location = New System.Drawing.Point(754, 367)
        Me.BUT_ACCEPT.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_ACCEPT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_ACCEPT.Name = "BUT_ACCEPT"
        Me.BUT_ACCEPT.Size = New System.Drawing.Size(69, 27)
        Me.BUT_ACCEPT.TabIndex = 1
        Me.BUT_ACCEPT.Text = "Save"
        '
        'Hyper_Request
        '
        Me.Hyper_Request.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Hyper_Request.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hyper_Request.Appearance.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Hyper_Request.Appearance.Image = CType(resources.GetObject("Hyper_Request.Appearance.Image"), System.Drawing.Image)
        Me.Hyper_Request.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hyper_Request.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.Hyper_Request.Location = New System.Drawing.Point(0, 370)
        Me.Hyper_Request.LookAndFeel.SkinName = "Liquid Sky"
        Me.Hyper_Request.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Hyper_Request.Name = "Hyper_Request"
        Me.Hyper_Request.Size = New System.Drawing.Size(228, 20)
        ToolTipTitleItem1.Text = "Note..."
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "If you do not find or need any new information in this Window then click here to " & _
    "send your request to Madhuban."
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.Hyper_Request.SuperTip = SuperToolTip1
        Me.Hyper_Request.TabIndex = 113
        Me.Hyper_Request.Text = "Send Your Request to Madhuban"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(821, 367)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(80, 27)
        Me.BUT_CANCEL.TabIndex = 114
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl2.Location = New System.Drawing.Point(547, 341)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(221, 14)
        Me.LabelControl2.TabIndex = 291
        Me.LabelControl2.Text = "TDS Deduction Reference Selected:"
        '
        'LabelControl31
        '
        Me.LabelControl31.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl31.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl31.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl31.Location = New System.Drawing.Point(620, 319)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Size = New System.Drawing.Size(147, 14)
        Me.LabelControl31.TabIndex = 290
        Me.LabelControl31.Text = "Nett Difference (in Rs.):"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl1.Location = New System.Drawing.Point(539, 299)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(228, 14)
        Me.LabelControl1.TabIndex = 289
        Me.LabelControl1.Text = "TDS payment mentioned in voucher:"
        '
        'lblOnScreenSelection
        '
        Me.lblOnScreenSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOnScreenSelection.EditValue = "0"
        Me.lblOnScreenSelection.Location = New System.Drawing.Point(768, 338)
        Me.lblOnScreenSelection.Name = "lblOnScreenSelection"
        Me.lblOnScreenSelection.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblOnScreenSelection.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnScreenSelection.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblOnScreenSelection.Properties.Appearance.Options.UseBackColor = True
        Me.lblOnScreenSelection.Properties.Appearance.Options.UseFont = True
        Me.lblOnScreenSelection.Properties.Appearance.Options.UseForeColor = True
        Me.lblOnScreenSelection.Properties.Appearance.Options.UseTextOptions = True
        Me.lblOnScreenSelection.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.Options.UseFont = True
        Me.lblOnScreenSelection.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.lblOnScreenSelection.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.lblOnScreenSelection.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnScreenSelection.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.lblOnScreenSelection.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.lblOnScreenSelection.Properties.AppearanceFocused.Options.UseFont = True
        Me.lblOnScreenSelection.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.lblOnScreenSelection.Properties.DisplayFormat.FormatString = "f2"
        Me.lblOnScreenSelection.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblOnScreenSelection.Properties.EditFormat.FormatString = "f2"
        Me.lblOnScreenSelection.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblOnScreenSelection.Properties.Mask.EditMask = "f2"
        Me.lblOnScreenSelection.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblOnScreenSelection.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblOnScreenSelection.Properties.ReadOnly = True
        Me.lblOnScreenSelection.Size = New System.Drawing.Size(130, 20)
        Me.lblOnScreenSelection.TabIndex = 288
        Me.lblOnScreenSelection.TabStop = False
        '
        'lblNetBalance
        '
        Me.lblNetBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetBalance.EditValue = "0"
        Me.lblNetBalance.Location = New System.Drawing.Point(768, 317)
        Me.lblNetBalance.Name = "lblNetBalance"
        Me.lblNetBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetBalance.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
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
        Me.lblNetBalance.Properties.Mask.EditMask = "f2"
        Me.lblNetBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblNetBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblNetBalance.Properties.ReadOnly = True
        Me.lblNetBalance.Size = New System.Drawing.Size(130, 20)
        Me.lblNetBalance.TabIndex = 287
        Me.lblNetBalance.TabStop = False
        '
        'lblMentionedInvoucher
        '
        Me.lblMentionedInvoucher.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMentionedInvoucher.EditValue = "0"
        Me.lblMentionedInvoucher.Location = New System.Drawing.Point(768, 296)
        Me.lblMentionedInvoucher.Name = "lblMentionedInvoucher"
        Me.lblMentionedInvoucher.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblMentionedInvoucher.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMentionedInvoucher.Properties.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblMentionedInvoucher.Properties.Appearance.Options.UseBackColor = True
        Me.lblMentionedInvoucher.Properties.Appearance.Options.UseFont = True
        Me.lblMentionedInvoucher.Properties.Appearance.Options.UseForeColor = True
        Me.lblMentionedInvoucher.Properties.Appearance.Options.UseTextOptions = True
        Me.lblMentionedInvoucher.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.Options.UseFont = True
        Me.lblMentionedInvoucher.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.Options.UseFont = True
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.lblMentionedInvoucher.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblMentionedInvoucher.Properties.DisplayFormat.FormatString = "c2"
        Me.lblMentionedInvoucher.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblMentionedInvoucher.Properties.EditFormat.FormatString = "c2"
        Me.lblMentionedInvoucher.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblMentionedInvoucher.Properties.Mask.EditMask = "f2"
        Me.lblMentionedInvoucher.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblMentionedInvoucher.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblMentionedInvoucher.Properties.ReadOnly = True
        Me.lblMentionedInvoucher.Size = New System.Drawing.Size(130, 20)
        Me.lblMentionedInvoucher.TabIndex = 286
        Me.lblMentionedInvoucher.TabStop = False
        '
        'LabelControl3
        '
        Me.LabelControl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Center
        Me.LabelControl3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl3.LineVisible = True
        Me.LabelControl3.Location = New System.Drawing.Point(-2, 361)
        Me.LabelControl3.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(901, 5)
        Me.LabelControl3.TabIndex = 292
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(0, 38)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(42, 14)
        Me.LabelControl6.TabIndex = 294
        Me.LabelControl6.Text = "Since :"
        '
        'Txt_Fr_Date
        '
        Me.Txt_Fr_Date.EditValue = Nothing
        Me.Txt_Fr_Date.EnterMoveNextControl = True
        Me.Txt_Fr_Date.Location = New System.Drawing.Point(45, 34)
        Me.Txt_Fr_Date.Name = "Txt_Fr_Date"
        Me.Txt_Fr_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.Txt_Fr_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Fr_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Navy
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Txt_Fr_Date.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[False]
        Me.Txt_Fr_Date.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Txt_Fr_Date.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.Txt_Fr_Date.Properties.LookAndFeel.SkinName = "Liquid Sky"
        Me.Txt_Fr_Date.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_Fr_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_Fr_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_Fr_Date.Properties.NullDate = ""
        Me.Txt_Fr_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_Fr_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_Fr_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_Fr_Date.Size = New System.Drawing.Size(186, 24)
        Me.Txt_Fr_Date.TabIndex = 293
        '
        'BUT_OK
        '
        Me.BUT_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OK.Image = CType(resources.GetObject("BUT_OK.Image"), System.Drawing.Image)
        Me.BUT_OK.Location = New System.Drawing.Point(244, 34)
        Me.BUT_OK.Name = "BUT_OK"
        Me.BUT_OK.Size = New System.Drawing.Size(70, 24)
        Me.BUT_OK.TabIndex = 295
        Me.BUT_OK.Text = "Show"
        '
        'Frm_TDS_Reg_Map_TDS_Paid
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 396)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ControlBox = False
        Me.Controls.Add(Me.BUT_OK)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.Txt_Fr_Date)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl31)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblOnScreenSelection)
        Me.Controls.Add(Me.lblNetBalance)
        Me.Controls.Add(Me.lblMentionedInvoucher)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.Hyper_Request)
        Me.Controls.Add(Me.BUT_ACCEPT)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GridControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "Frm_TDS_Reg_Map_TDS_Paid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TDS paid to Govt. Breakup"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.lblOnScreenSelection.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMentionedInvoucher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Fr_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BUT_ACCEPT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Hyper_Request As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Txn_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Party As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Dr_Amount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TDS_Deducted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Remaining_Amount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TDS_Paid_Already As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TDS_Paid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents lblOnScreenSelection As DevExpress.XtraEditors.TextEdit
    Public WithEvents lblNetBalance As DevExpress.XtraEditors.TextEdit
    Public WithEvents lblMentionedInvoucher As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Public WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents REC_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents declared_date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents Center As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Cen_UID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SR_NO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Txt_Fr_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents BUT_OK As DevExpress.XtraEditors.SimpleButton

End Class
