<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Daily_Balances_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Daily_Balances_Report))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BUT_CLOSE = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Lbl_ToolMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Zoom1 = New DevExpress.XtraEditors.ZoomTrackBarControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.iTR_DATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_ITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_PARTY_1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_MODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Ref = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_REF_DATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_REF_CDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.iTR_RECEIPT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_PAYMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iREC_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_M_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_SR_NO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iLEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.iTR_NARRATION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BUT_SAVE = New DevExpress.XtraEditors.SimpleButton()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_OPTIONS = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_PRINT = New DevExpress.XtraEditors.SimpleButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_REC_TXN = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_REC_CLEARING = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Refresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lbl_Separator1 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Create = New System.Windows.Forms.Label()
        Me.Lbl_Modify = New System.Windows.Forms.Label()
        Me.Lbl_Separator3 = New DevExpress.XtraEditors.LabelControl()
        Me.But_Find = New DevExpress.XtraEditors.SimpleButton()
        Me.But_Filter = New DevExpress.XtraEditors.SimpleButton()
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Status = New DevExpress.XtraEditors.LabelControl()
        Me.Pic_Status = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNetBalance = New DevExpress.XtraEditors.TextEdit()
        Me.lblTxnBalance = New DevExpress.XtraEditors.TextEdit()
        Me.lblClearingBalance = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.T_PRINT_CHEQUE = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTxnBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblClearingBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 420)
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
        Me.Zoom1.Location = New System.Drawing.Point(620, 396)
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
        Me.GridControl1.Location = New System.Drawing.Point(-1, 32)
        Me.GridControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1})
        Me.GridControl1.Size = New System.Drawing.Size(776, 289)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.iTR_DATE, Me.iTR_ITEM, Me.iTR_PARTY_1, Me.iTR_MODE, Me.Ref, Me.iTR_REF_DATE, Me.iTR_REF_CDATE, Me.iTR_RECEIPT, Me.iTR_PAYMENT, Me.iREC_ID, Me.iTR_M_ID, Me.iTR_SR_NO, Me.iLEDGER, Me.iTR_NARRATION})
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
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
        Me.GridView1.ViewCaption = "Group"
        '
        'iTR_DATE
        '
        Me.iTR_DATE.Caption = "Date"
        Me.iTR_DATE.FieldName = "iTR_DATE"
        Me.iTR_DATE.Name = "iTR_DATE"
        Me.iTR_DATE.OptionsColumn.AllowEdit = False
        Me.iTR_DATE.Visible = True
        Me.iTR_DATE.VisibleIndex = 0
        '
        'iTR_ITEM
        '
        Me.iTR_ITEM.Caption = "Item"
        Me.iTR_ITEM.FieldName = "iTR_ITEM"
        Me.iTR_ITEM.Name = "iTR_ITEM"
        Me.iTR_ITEM.OptionsColumn.AllowEdit = False
        Me.iTR_ITEM.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_ITEM.Visible = True
        Me.iTR_ITEM.VisibleIndex = 1
        Me.iTR_ITEM.Width = 225
        '
        'iTR_PARTY_1
        '
        Me.iTR_PARTY_1.Caption = "Party"
        Me.iTR_PARTY_1.FieldName = "iTR_PARTY_1"
        Me.iTR_PARTY_1.Name = "iTR_PARTY_1"
        Me.iTR_PARTY_1.OptionsColumn.AllowEdit = False
        Me.iTR_PARTY_1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_PARTY_1.Visible = True
        Me.iTR_PARTY_1.VisibleIndex = 4
        Me.iTR_PARTY_1.Width = 150
        '
        'iTR_MODE
        '
        Me.iTR_MODE.Caption = "Mode"
        Me.iTR_MODE.FieldName = "iTR_MODE"
        Me.iTR_MODE.Name = "iTR_MODE"
        Me.iTR_MODE.OptionsColumn.AllowEdit = False
        Me.iTR_MODE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_MODE.Visible = True
        Me.iTR_MODE.VisibleIndex = 2
        '
        'Ref
        '
        Me.Ref.Caption = "Ref No."
        Me.Ref.FieldName = "Ref"
        Me.Ref.Name = "Ref"
        Me.Ref.OptionsColumn.AllowEdit = False
        Me.Ref.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.Ref.Visible = True
        Me.Ref.VisibleIndex = 3
        '
        'iTR_REF_DATE
        '
        Me.iTR_REF_DATE.Caption = "Instrument Date"
        Me.iTR_REF_DATE.FieldName = "iTR_REF_DATE"
        Me.iTR_REF_DATE.Name = "iTR_REF_DATE"
        Me.iTR_REF_DATE.OptionsColumn.AllowEdit = False
        Me.iTR_REF_DATE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_REF_DATE.Visible = True
        Me.iTR_REF_DATE.VisibleIndex = 5
        '
        'iTR_REF_CDATE
        '
        Me.iTR_REF_CDATE.Caption = "Clearing Date"
        Me.iTR_REF_CDATE.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.iTR_REF_CDATE.FieldName = "iTR_REF_CDATE"
        Me.iTR_REF_CDATE.Name = "iTR_REF_CDATE"
        Me.iTR_REF_CDATE.Visible = True
        Me.iTR_REF_CDATE.VisibleIndex = 6
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'iTR_RECEIPT
        '
        Me.iTR_RECEIPT.Caption = "Debit"
        Me.iTR_RECEIPT.FieldName = "iTR_RECEIPT"
        Me.iTR_RECEIPT.Name = "iTR_RECEIPT"
        Me.iTR_RECEIPT.OptionsColumn.AllowEdit = False
        Me.iTR_RECEIPT.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_RECEIPT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.iTR_RECEIPT.Visible = True
        Me.iTR_RECEIPT.VisibleIndex = 7
        '
        'iTR_PAYMENT
        '
        Me.iTR_PAYMENT.Caption = "Credit"
        Me.iTR_PAYMENT.FieldName = "iTR_PAYMENT"
        Me.iTR_PAYMENT.Name = "iTR_PAYMENT"
        Me.iTR_PAYMENT.OptionsColumn.AllowEdit = False
        Me.iTR_PAYMENT.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.iTR_PAYMENT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.iTR_PAYMENT.Visible = True
        Me.iTR_PAYMENT.VisibleIndex = 8
        '
        'iREC_ID
        '
        Me.iREC_ID.Caption = "iREC_ID"
        Me.iREC_ID.FieldName = "iREC_ID"
        Me.iREC_ID.Name = "iREC_ID"
        '
        'iTR_M_ID
        '
        Me.iTR_M_ID.Caption = "iTR_M_ID"
        Me.iTR_M_ID.FieldName = "iTR_M_ID"
        Me.iTR_M_ID.Name = "iTR_M_ID"
        '
        'iTR_SR_NO
        '
        Me.iTR_SR_NO.Caption = "iTR_SR_NO"
        Me.iTR_SR_NO.FieldName = "iTR_SR_NO"
        Me.iTR_SR_NO.Name = "iTR_SR_NO"
        '
        'iLEDGER
        '
        Me.iLEDGER.Caption = "iLEDGER"
        Me.iLEDGER.FieldName = "iLEDGER"
        Me.iLEDGER.Name = "iLEDGER"
        '
        'iTR_NARRATION
        '
        Me.iTR_NARRATION.Caption = "iTR_NARRATION"
        Me.iTR_NARRATION.FieldName = "iTR_NARRATION"
        Me.iTR_NARRATION.Name = "iTR_NARRATION"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.BUT_SAVE)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Controls.Add(Me.BUT_CLOSE)
        Me.Panel1.Controls.Add(Me.BUT_OPTIONS)
        Me.Panel1.Controls.Add(Me.BUT_PRINT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(774, 34)
        Me.Panel1.TabIndex = 43
        '
        'BUT_SAVE
        '
        Me.BUT_SAVE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SAVE.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SAVE.Appearance.Options.UseFont = True
        Me.BUT_SAVE.Image = CType(resources.GetObject("BUT_SAVE.Image"), System.Drawing.Image)
        Me.BUT_SAVE.Location = New System.Drawing.Point(630, 4)
        Me.BUT_SAVE.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_SAVE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SAVE.Name = "BUT_SAVE"
        Me.BUT_SAVE.Size = New System.Drawing.Size(70, 26)
        Me.BUT_SAVE.TabIndex = 148
        Me.BUT_SAVE.Text = "Save"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(5, 9)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(101, 17)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Daily Balances "
        '
        'BUT_OPTIONS
        '
        Me.BUT_OPTIONS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OPTIONS.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.bluesearch
        Me.BUT_OPTIONS.Location = New System.Drawing.Point(466, 4)
        Me.BUT_OPTIONS.LookAndFeel.SkinName = "iMaginary"
        Me.BUT_OPTIONS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_OPTIONS.Name = "BUT_OPTIONS"
        Me.BUT_OPTIONS.Size = New System.Drawing.Size(70, 26)
        Me.BUT_OPTIONS.TabIndex = 6
        Me.BUT_OPTIONS.Text = "Options"
        '
        'BUT_PRINT
        '
        Me.BUT_PRINT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_PRINT.Image = CType(resources.GetObject("BUT_PRINT.Image"), System.Drawing.Image)
        Me.BUT_PRINT.Location = New System.Drawing.Point(535, 4)
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
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_REC_TXN, Me.T_REC_CLEARING, Me.ToolStripSeparator1, Me.T_PRINT_CHEQUE, Me.T_Print, Me.ToolStripMenuItem1, Me.T_Refresh, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(216, 176)
        '
        'T_REC_TXN
        '
        Me.T_REC_TXN.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.bluesearch
        Me.T_REC_TXN.Name = "T_REC_TXN"
        Me.T_REC_TXN.Size = New System.Drawing.Size(215, 22)
        Me.T_REC_TXN.Text = "Reconcile by Txn. Date"
        '
        'T_REC_CLEARING
        '
        Me.T_REC_CLEARING.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.greensearch
        Me.T_REC_CLEARING.Name = "T_REC_CLEARING"
        Me.T_REC_CLEARING.Size = New System.Drawing.Size(215, 22)
        Me.T_REC_CLEARING.Text = "Reconcile by Clearing Date"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(212, 6)
        '
        'T_Print
        '
        Me.T_Print.Image = CType(resources.GetObject("T_Print.Image"), System.Drawing.Image)
        Me.T_Print.Name = "T_Print"
        Me.T_Print.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.T_Print.Size = New System.Drawing.Size(215, 22)
        Me.T_Print.Text = "&List Preview"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(212, 6)
        '
        'T_Refresh
        '
        Me.T_Refresh.Image = CType(resources.GetObject("T_Refresh.Image"), System.Drawing.Image)
        Me.T_Refresh.Name = "T_Refresh"
        Me.T_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.T_Refresh.Size = New System.Drawing.Size(215, 22)
        Me.T_Refresh.Text = "&Refresh"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(212, 6)
        '
        'T_Close
        '
        Me.T_Close.Image = CType(resources.GetObject("T_Close.Image"), System.Drawing.Image)
        Me.T_Close.Name = "T_Close"
        Me.T_Close.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.T_Close.Size = New System.Drawing.Size(215, 22)
        Me.T_Close.Text = "&Close"
        '
        'Lbl_Separator1
        '
        Me.Lbl_Separator1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator1.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.Lbl_Separator1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.Lbl_Separator1.LineVisible = True
        Me.Lbl_Separator1.Location = New System.Drawing.Point(156, 391)
        Me.Lbl_Separator1.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator1.Name = "Lbl_Separator1"
        Me.Lbl_Separator1.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Separator1.TabIndex = 131
        '
        'Lbl_Create
        '
        Me.Lbl_Create.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Create.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Create.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Create.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Create.Location = New System.Drawing.Point(158, 391)
        Me.Lbl_Create.Name = "Lbl_Create"
        Me.Lbl_Create.Size = New System.Drawing.Size(330, 15)
        Me.Lbl_Create.TabIndex = 132
        Me.Lbl_Create.Text = "Created On: 12-12-2010, 04:55:20 PM, By: Connect"
        Me.Lbl_Create.Visible = False
        '
        'Lbl_Modify
        '
        Me.Lbl_Modify.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Modify.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Modify.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Modify.ForeColor = System.Drawing.Color.DimGray
        Me.Lbl_Modify.Location = New System.Drawing.Point(158, 406)
        Me.Lbl_Modify.Name = "Lbl_Modify"
        Me.Lbl_Modify.Size = New System.Drawing.Size(330, 15)
        Me.Lbl_Modify.TabIndex = 133
        Me.Lbl_Modify.Text = "Edited On: 12-12-2010, 04:55:20 PM, By: Connect"
        Me.Lbl_Modify.Visible = False
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
        Me.Lbl_Separator3.Location = New System.Drawing.Point(615, 391)
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
        Me.But_Find.Location = New System.Drawing.Point(554, 393)
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
        Me.But_Filter.Location = New System.Drawing.Point(493, 393)
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
        Me.Lbl_Separator2.Location = New System.Drawing.Point(489, 391)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(5, 29)
        Me.Lbl_Separator2.TabIndex = 144
        '
        'Lbl_Status
        '
        Me.Lbl_Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Status.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Status.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Status.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Status.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Lbl_Status.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Status.Location = New System.Drawing.Point(33, 391)
        Me.Lbl_Status.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Status.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Status.Name = "Lbl_Status"
        Me.Lbl_Status.Size = New System.Drawing.Size(122, 29)
        ToolTipTitleItem1.Text = "Entry Status"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.Lbl_Status.SuperTip = SuperToolTip1
        Me.Lbl_Status.TabIndex = 145
        Me.Lbl_Status.Text = "Status"
        Me.Lbl_Status.Visible = False
        '
        'Pic_Status
        '
        Me.Pic_Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Pic_Status.EditValue = Global.ConnectOne.D0010._001.My.Resources.Resources.unlock
        Me.Pic_Status.Location = New System.Drawing.Point(-1, 387)
        Me.Pic_Status.Name = "Pic_Status"
        Me.Pic_Status.Size = New System.Drawing.Size(32, 31)
        ToolTipTitleItem2.Text = "Entry Status"
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        Me.Pic_Status.SuperTip = SuperToolTip2
        Me.Pic_Status.TabIndex = 147
        Me.Pic_Status.Visible = False
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
        Me.LabelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom
        Me.LabelControl3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl3.LineVisible = True
        Me.LabelControl3.Location = New System.Drawing.Point(30, 383)
        Me.LabelControl3.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(750, 5)
        Me.LabelControl3.TabIndex = 131
        '
        'lblNetBalance
        '
        Me.lblNetBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetBalance.EditValue = "0"
        Me.lblNetBalance.Location = New System.Drawing.Point(645, 345)
        Me.lblNetBalance.Name = "lblNetBalance"
        Me.lblNetBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblNetBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblNetBalance.Properties.Mask.EditMask = "c2"
        Me.lblNetBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblNetBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblNetBalance.Properties.ReadOnly = True
        Me.lblNetBalance.Size = New System.Drawing.Size(129, 20)
        Me.lblNetBalance.TabIndex = 281
        Me.lblNetBalance.TabStop = False
        '
        'lblTxnBalance
        '
        Me.lblTxnBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTxnBalance.EditValue = "0"
        Me.lblTxnBalance.Location = New System.Drawing.Point(645, 324)
        Me.lblTxnBalance.Name = "lblTxnBalance"
        Me.lblTxnBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblTxnBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblTxnBalance.Properties.DisplayFormat.FormatString = "c2"
        Me.lblTxnBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblTxnBalance.Properties.EditFormat.FormatString = "c2"
        Me.lblTxnBalance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lblTxnBalance.Properties.Mask.EditMask = "c2"
        Me.lblTxnBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblTxnBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblTxnBalance.Properties.ReadOnly = True
        Me.lblTxnBalance.Size = New System.Drawing.Size(130, 20)
        Me.lblTxnBalance.TabIndex = 278
        Me.lblTxnBalance.TabStop = False
        '
        'lblClearingBalance
        '
        Me.lblClearingBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClearingBalance.EditValue = "0"
        Me.lblClearingBalance.Location = New System.Drawing.Point(645, 366)
        Me.lblClearingBalance.Name = "lblClearingBalance"
        Me.lblClearingBalance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblClearingBalance.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblClearingBalance.Properties.Mask.EditMask = "c2"
        Me.lblClearingBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lblClearingBalance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lblClearingBalance.Properties.ReadOnly = True
        Me.lblClearingBalance.Size = New System.Drawing.Size(130, 20)
        Me.lblClearingBalance.TabIndex = 282
        Me.lblClearingBalance.TabStop = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl1.Location = New System.Drawing.Point(431, 326)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(213, 14)
        Me.LabelControl1.TabIndex = 283
        Me.LabelControl1.Text = "Balance as per Txn Dates (in Rs.):"
        '
        'LabelControl31
        '
        Me.LabelControl31.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl31.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl31.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl31.Location = New System.Drawing.Point(497, 347)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Size = New System.Drawing.Size(147, 14)
        Me.LabelControl31.TabIndex = 284
        Me.LabelControl31.Text = "Nett Difference (in Rs.):"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LabelControl2.Location = New System.Drawing.Point(400, 367)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(243, 14)
        Me.LabelControl2.TabIndex = 285
        Me.LabelControl2.Text = "Balance as per Clearing Dates (in Rs.):"
        '
        'T_PRINT_CHEQUE
        '
        Me.T_PRINT_CHEQUE.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Expand
        Me.T_PRINT_CHEQUE.Name = "T_PRINT_CHEQUE"
        Me.T_PRINT_CHEQUE.Size = New System.Drawing.Size(215, 22)
        Me.T_PRINT_CHEQUE.Text = "Print Cheque"
        '
        'Frm_Daily_Balances_Report
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 438)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl31)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.But_Filter)
        Me.Controls.Add(Me.Lbl_Status)
        Me.Controls.Add(Me.lblClearingBalance)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.lblNetBalance)
        Me.Controls.Add(Me.lblTxnBalance)
        Me.Controls.Add(Me.Lbl_Separator3)
        Me.Controls.Add(Me.But_Find)
        Me.Controls.Add(Me.Zoom1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.Lbl_Separator1)
        Me.Controls.Add(Me.Lbl_Create)
        Me.Controls.Add(Me.Lbl_Modify)
        Me.Controls.Add(Me.Pic_Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "Frm_Daily_Balances_Report"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daily Balances"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Zoom1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Zoom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Pic_Status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTxnBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblClearingBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Lbl_Separator1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Create As System.Windows.Forms.Label
    Friend WithEvents Lbl_Modify As System.Windows.Forms.Label
    Friend WithEvents Lbl_Separator3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents But_Find As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents But_Filter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Status As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Pic_Status As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents BUT_OPTIONS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents iTR_DATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_REF_CDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_ITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_PARTY_1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_MODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_RECEIPT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_PAYMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Ref As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T_REC_TXN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_REC_CLEARING As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BUT_SAVE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents iREC_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_M_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_SR_NO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_REF_DATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Public WithEvents lblNetBalance As DevExpress.XtraEditors.TextEdit
    Public WithEvents lblClearingBalance As DevExpress.XtraEditors.TextEdit
    Public WithEvents lblTxnBalance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents iLEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents iTR_NARRATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T_PRINT_CHEQUE As System.Windows.Forms.ToolStripMenuItem


End Class
