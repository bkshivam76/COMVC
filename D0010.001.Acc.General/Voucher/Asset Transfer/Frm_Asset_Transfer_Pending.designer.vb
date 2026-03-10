<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Asset_Transfer_Pending
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Asset_Transfer_Pending))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipSeparatorItem1 As DevExpress.Utils.ToolTipSeparatorItem = New DevExpress.Utils.ToolTipSeparatorItem()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GLookUp_Cen_List = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GLookUp_Cen_ListView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TO_CEN_NAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TO_PAD_NO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TO_UID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TO_ZONE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TO_CEN_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblCount = New DevExpress.XtraEditors.LabelControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.T_ACCEPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.T_REJECT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Print = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.T_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.BUT_ACCEPT = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_REJECT = New DevExpress.XtraEditors.SimpleButton()
        Me.Hyper_Request = New DevExpress.XtraEditors.LabelControl()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.GLookUp_Cen_List.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_Cen_ListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.GridControl1.Size = New System.Drawing.Size(786, 296)
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
        Me.Panel1.Controls.Add(Me.SimpleButton1)
        Me.Panel1.Controls.Add(Me.LabelControl1)
        Me.Panel1.Controls.Add(Me.GLookUp_Cen_List)
        Me.Panel1.Controls.Add(Me.lblCount)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 34)
        Me.Panel1.TabIndex = 43
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(737, 7)
        Me.SimpleButton1.LookAndFeel.SkinName = "iMaginary"
        Me.SimpleButton1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(44, 20)
        Me.SimpleButton1.TabIndex = 114
        Me.SimpleButton1.Text = "Go"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.LabelControl1.Location = New System.Drawing.Point(378, 10)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 14)
        Me.LabelControl1.TabIndex = 150
        Me.LabelControl1.Text = "from"
        '
        'GLookUp_Cen_List
        '
        Me.GLookUp_Cen_List.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GLookUp_Cen_List.EnterMoveNextControl = True
        Me.GLookUp_Cen_List.Location = New System.Drawing.Point(420, 7)
        Me.GLookUp_Cen_List.Name = "GLookUp_Cen_List"
        Me.GLookUp_Cen_List.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.GLookUp_Cen_List.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GLookUp_Cen_List.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.GLookUp_Cen_List.Properties.Appearance.Options.UseFont = True
        Me.GLookUp_Cen_List.Properties.Appearance.Options.UseForeColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GLookUp_Cen_List.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.GLookUp_Cen_List.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_Cen_List.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.Options.UseFont = True
        Me.GLookUp_Cen_List.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GLookUp_Cen_List.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem1.Text = "Advanced Filter (On/Off)"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Enabled : Auto Filter Bar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Column Filter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Custom Filter Edito" & _
    "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Footer Filter Display"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.GLookUp_Cen_List.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Filter : Off", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "OFF", SuperToolTip1, True)})
        Me.GLookUp_Cen_List.Properties.ImmediatePopup = True
        Me.GLookUp_Cen_List.Properties.NullText = ""
        Me.GLookUp_Cen_List.Properties.NullValuePrompt = "Select Centre..."
        Me.GLookUp_Cen_List.Properties.NullValuePromptShowForEmptyValue = True
        Me.GLookUp_Cen_List.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GLookUp_Cen_List.Properties.PopupFormMinSize = New System.Drawing.Size(655, 335)
        Me.GLookUp_Cen_List.Properties.PopupFormSize = New System.Drawing.Size(655, 335)
        Me.GLookUp_Cen_List.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GLookUp_Cen_List.Properties.View = Me.GLookUp_Cen_ListView
        Me.GLookUp_Cen_List.Size = New System.Drawing.Size(314, 20)
        ToolTipTitleItem2.Text = "Information..."
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "Select Centre ..."
        ToolTipTitleItem3.LeftIndent = 6
        ToolTipTitleItem3.Text = "Use F4 function key to Show List."
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        SuperToolTip2.Items.Add(ToolTipItem2)
        SuperToolTip2.Items.Add(ToolTipSeparatorItem1)
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        Me.GLookUp_Cen_List.SuperTip = SuperToolTip2
        Me.GLookUp_Cen_List.TabIndex = 149
        '
        'GLookUp_Cen_ListView
        '
        Me.GLookUp_Cen_ListView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.TO_CEN_NAME, Me.TO_PAD_NO, Me.TO_UID, Me.TO_ZONE, Me.TO_CEN_ID})
        Me.GLookUp_Cen_ListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GLookUp_Cen_ListView.Name = "GLookUp_Cen_ListView"
        Me.GLookUp_Cen_ListView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_Cen_ListView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_Cen_ListView.OptionsBehavior.ReadOnly = True
        Me.GLookUp_Cen_ListView.OptionsCustomization.AllowFilter = False
        Me.GLookUp_Cen_ListView.OptionsCustomization.AllowGroup = False
        Me.GLookUp_Cen_ListView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GLookUp_Cen_ListView.OptionsLayout.Columns.AddNewColumns = False
        Me.GLookUp_Cen_ListView.OptionsLayout.Columns.RemoveOldColumns = False
        Me.GLookUp_Cen_ListView.OptionsMenu.EnableColumnMenu = False
        Me.GLookUp_Cen_ListView.OptionsMenu.EnableFooterMenu = False
        Me.GLookUp_Cen_ListView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GLookUp_Cen_ListView.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GLookUp_Cen_ListView.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GLookUp_Cen_ListView.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.GLookUp_Cen_ListView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GLookUp_Cen_ListView.OptionsSelection.InvertSelection = True
        Me.GLookUp_Cen_ListView.OptionsView.EnableAppearanceEvenRow = True
        Me.GLookUp_Cen_ListView.OptionsView.EnableAppearanceOddRow = True
        Me.GLookUp_Cen_ListView.OptionsView.ShowGroupPanel = False
        Me.GLookUp_Cen_ListView.OptionsView.ShowIndicator = False
        '
        'TO_CEN_NAME
        '
        Me.TO_CEN_NAME.Caption = "CENTRE"
        Me.TO_CEN_NAME.FieldName = "TO_CEN_NAME"
        Me.TO_CEN_NAME.Name = "TO_CEN_NAME"
        Me.TO_CEN_NAME.Visible = True
        Me.TO_CEN_NAME.VisibleIndex = 0
        '
        'TO_PAD_NO
        '
        Me.TO_PAD_NO.Caption = "PAD No"
        Me.TO_PAD_NO.FieldName = "TO_PAD_NO"
        Me.TO_PAD_NO.Name = "TO_PAD_NO"
        Me.TO_PAD_NO.Visible = True
        Me.TO_PAD_NO.VisibleIndex = 1
        '
        'TO_UID
        '
        Me.TO_UID.Caption = "UID"
        Me.TO_UID.FieldName = "TO_UID"
        Me.TO_UID.Name = "TO_UID"
        Me.TO_UID.Visible = True
        Me.TO_UID.VisibleIndex = 2
        '
        'TO_ZONE
        '
        Me.TO_ZONE.Caption = "Zone"
        Me.TO_ZONE.FieldName = "TO_ZONE"
        Me.TO_ZONE.Name = "TO_ZONE"
        Me.TO_ZONE.Visible = True
        Me.TO_ZONE.VisibleIndex = 3
        '
        'TO_CEN_ID
        '
        Me.TO_CEN_ID.Caption = "CEN ID"
        Me.TO_CEN_ID.FieldName = "TO_CEN_ID"
        Me.TO_CEN_ID.Name = "TO_CEN_ID"
        Me.TO_CEN_ID.Visible = True
        Me.TO_CEN_ID.VisibleIndex = 4
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
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.T_ACCEPT, Me.T_REJECT, Me.ToolStripMenuItem2, Me.T_Print, Me.ToolStripMenuItem3, Me.T_Close})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 104)
        '
        'T_ACCEPT
        '
        Me.T_ACCEPT.Image = CType(resources.GetObject("T_ACCEPT.Image"), System.Drawing.Image)
        Me.T_ACCEPT.Name = "T_ACCEPT"
        Me.T_ACCEPT.Size = New System.Drawing.Size(176, 22)
        Me.T_ACCEPT.Text = "Accept Transfer"
        '
        'T_REJECT
        '
        Me.T_REJECT.Image = CType(resources.GetObject("T_REJECT.Image"), System.Drawing.Image)
        Me.T_REJECT.Name = "T_REJECT"
        Me.T_REJECT.Size = New System.Drawing.Size(176, 22)
        Me.T_REJECT.Text = "Reject Transfer"
        Me.T_REJECT.Visible = False
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
        Me.BUT_ACCEPT.Location = New System.Drawing.Point(405, 333)
        Me.BUT_ACCEPT.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_ACCEPT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_ACCEPT.Name = "BUT_ACCEPT"
        Me.BUT_ACCEPT.Size = New System.Drawing.Size(126, 27)
        Me.BUT_ACCEPT.TabIndex = 1
        Me.BUT_ACCEPT.Text = "Accept Transfer"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(531, 333)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(250, 27)
        Me.BUT_CANCEL.TabIndex = 3
        Me.BUT_CANCEL.Text = "New Entry not linked to above Transfer"
        '
        'BUT_REJECT
        '
        Me.BUT_REJECT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_REJECT.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_REJECT.Appearance.Options.UseFont = True
        Me.BUT_REJECT.Image = CType(resources.GetObject("BUT_REJECT.Image"), System.Drawing.Image)
        Me.BUT_REJECT.Location = New System.Drawing.Point(274, 333)
        Me.BUT_REJECT.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_REJECT.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_REJECT.Name = "BUT_REJECT"
        Me.BUT_REJECT.Size = New System.Drawing.Size(70, 27)
        Me.BUT_REJECT.TabIndex = 2
        Me.BUT_REJECT.Text = "Reject"
        Me.BUT_REJECT.Visible = False
        '
        'Hyper_Request
        '
        Me.Hyper_Request.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Hyper_Request.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hyper_Request.Appearance.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Hyper_Request.Appearance.Image = CType(resources.GetObject("Hyper_Request.Appearance.Image"), System.Drawing.Image)
        Me.Hyper_Request.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hyper_Request.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.Hyper_Request.Location = New System.Drawing.Point(0, 336)
        Me.Hyper_Request.LookAndFeel.SkinName = "Liquid Sky"
        Me.Hyper_Request.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Hyper_Request.Name = "Hyper_Request"
        Me.Hyper_Request.Size = New System.Drawing.Size(228, 20)
        ToolTipTitleItem4.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem4.Appearance.Options.UseImage = True
        ToolTipTitleItem4.Image = CType(resources.GetObject("ToolTipTitleItem4.Image"), System.Drawing.Image)
        ToolTipTitleItem4.Text = "Note..."
        ToolTipItem3.LeftIndent = 6
        ToolTipItem3.Text = "If you do not find or need any new information in this Window then click here to " & _
    "send your request to Madhuban."
        SuperToolTip3.Items.Add(ToolTipTitleItem4)
        SuperToolTip3.Items.Add(ToolTipItem3)
        Me.Hyper_Request.SuperTip = SuperToolTip3
        Me.Hyper_Request.TabIndex = 113
        Me.Hyper_Request.Text = "Send Your Request to Madhuban"
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(40, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(315, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Pending Asset Transfer Entries"
        '
        'Frm_Asset_Transfer_Pending
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 362)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.Hyper_Request)
        Me.Controls.Add(Me.BUT_REJECT)
        Me.Controls.Add(Me.BUT_ACCEPT)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GridControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "Frm_Asset_Transfer_Pending"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pending List"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GLookUp_Cen_List.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_Cen_ListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents T_Print As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents T_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BUT_ACCEPT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_REJECT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents T_ACCEPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_REJECT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Hyper_Request As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GLookUp_Cen_List As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GLookUp_Cen_ListView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TO_CEN_NAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TO_PAD_NO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TO_UID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TO_ZONE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TO_CEN_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl

End Class
