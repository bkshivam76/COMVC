<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog_Telephone_Bill
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipSeparatorItem1 As DevExpress.Utils.ToolTipSeparatorItem = New DevExpress.Utils.ToolTipSeparatorItem()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog_Telephone_Bill))
        Me.Layout_GS = New DevExpress.XtraLayout.LayoutControl()
        Me.Txt_To_Date = New DevExpress.XtraEditors.DateEdit()
        Me.Txt_Fr_Date = New DevExpress.XtraEditors.DateEdit()
        Me.GLookUp_TeleList = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GLookUp_TeleListView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TP_NO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TP_COMPANY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TP_CATEGORY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TP_TYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TP_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BE_Category = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Company = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Plantype = New DevExpress.XtraEditors.ButtonEdit()
        Me.Layout_GS_Group = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.xID = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TopLine = New DevExpress.XtraEditors.LabelControl()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_SAVE_COM = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_DEL = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.Layout_GS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Layout_GS.SuspendLayout()
        CType(Me.Txt_To_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_To_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Fr_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_TeleList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_TeleListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Category.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Company.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Plantype.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Layout_GS_Group, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Layout_GS
        '
        Me.Layout_GS.AllowCustomizationMenu = False
        Me.Layout_GS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Layout_GS.Controls.Add(Me.Txt_To_Date)
        Me.Layout_GS.Controls.Add(Me.Txt_Fr_Date)
        Me.Layout_GS.Controls.Add(Me.GLookUp_TeleList)
        Me.Layout_GS.Controls.Add(Me.BE_Category)
        Me.Layout_GS.Controls.Add(Me.BE_Company)
        Me.Layout_GS.Controls.Add(Me.BE_Plantype)
        Me.Layout_GS.Location = New System.Drawing.Point(-1, 34)
        Me.Layout_GS.LookAndFeel.SkinName = "Liquid Sky"
        Me.Layout_GS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Layout_GS.Name = "Layout_GS"
        Me.Layout_GS.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(614, 155, 250, 350)
        Me.Layout_GS.OptionsFocus.AllowFocusControlOnActivatedTabPage = True
        Me.Layout_GS.OptionsFocus.AllowFocusControlOnLabelClick = True
        Me.Layout_GS.OptionsFocus.EnableAutoTabOrder = False
        Me.Layout_GS.OptionsView.DrawItemBorders = True
        Me.Layout_GS.OptionsView.HighlightFocusedItem = True
        Me.Layout_GS.OptionsView.UseSkinIndents = False
        Me.Layout_GS.Root = Me.Layout_GS_Group
        Me.Layout_GS.Size = New System.Drawing.Size(596, 92)
        Me.Layout_GS.TabIndex = 0
        Me.Layout_GS.Text = "LayoutControl1"
        '
        'Txt_To_Date
        '
        Me.Txt_To_Date.EditValue = ""
        Me.Txt_To_Date.EnterMoveNextControl = True
        Me.Txt_To_Date.Location = New System.Drawing.Point(404, 66)
        Me.Txt_To_Date.Name = "Txt_To_Date"
        Me.Txt_To_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_To_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_To_Date.Properties.Appearance.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_To_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_To_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_To_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_To_Date.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_To_Date.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_To_Date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Txt_To_Date.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[False]
        Me.Txt_To_Date.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Txt_To_Date.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.Txt_To_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_To_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_To_Date.Properties.NullDate = ""
        Me.Txt_To_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_To_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_To_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_To_Date.Size = New System.Drawing.Size(186, 20)
        Me.Txt_To_Date.StyleController = Me.Layout_GS
        Me.Txt_To_Date.TabIndex = 48
        '
        'Txt_Fr_Date
        '
        Me.Txt_Fr_Date.EditValue = ""
        Me.Txt_Fr_Date.EnterMoveNextControl = True
        Me.Txt_Fr_Date.Location = New System.Drawing.Point(107, 66)
        Me.Txt_Fr_Date.Name = "Txt_Fr_Date"
        Me.Txt_Fr_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_Fr_Date.Properties.Appearance.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.Appearance.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_Fr_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_Fr_Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_Fr_Date.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Fr_Date.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
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
        Me.Txt_Fr_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.Txt_Fr_Date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Txt_Fr_Date.Properties.NullDate = ""
        Me.Txt_Fr_Date.Properties.NullValuePrompt = "Date..."
        Me.Txt_Fr_Date.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_Fr_Date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.Txt_Fr_Date.Size = New System.Drawing.Size(186, 20)
        Me.Txt_Fr_Date.StyleController = Me.Layout_GS
        Me.Txt_Fr_Date.TabIndex = 48
        '
        'GLookUp_TeleList
        '
        Me.GLookUp_TeleList.EnterMoveNextControl = True
        Me.GLookUp_TeleList.Location = New System.Drawing.Point(107, 6)
        Me.GLookUp_TeleList.Name = "GLookUp_TeleList"
        Me.GLookUp_TeleList.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GLookUp_TeleList.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.GLookUp_TeleList.Properties.Appearance.Options.UseFont = True
        Me.GLookUp_TeleList.Properties.Appearance.Options.UseForeColor = True
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GLookUp_TeleList.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.GLookUp_TeleList.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.GLookUp_TeleList.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_TeleList.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GLookUp_TeleList.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GLookUp_TeleList.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.GLookUp_TeleList.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_TeleList.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.GLookUp_TeleList.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GLookUp_TeleList.Properties.AppearanceFocused.Options.UseFont = True
        Me.GLookUp_TeleList.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GLookUp_TeleList.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem1.Text = "Advanced Filter (On/Off)"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Enabled : Auto Filter Bar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Column Filter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Custom Filter Edito" & _
    "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Footer Filter Display"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.GLookUp_TeleList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Filter : Off", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "OFF", SuperToolTip1, True)})
        Me.GLookUp_TeleList.Properties.ImmediatePopup = True
        Me.GLookUp_TeleList.Properties.NullText = ""
        Me.GLookUp_TeleList.Properties.NullValuePrompt = "Select Telephone No..."
        Me.GLookUp_TeleList.Properties.NullValuePromptShowForEmptyValue = True
        Me.GLookUp_TeleList.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GLookUp_TeleList.Properties.PopupFormMinSize = New System.Drawing.Size(600, 250)
        Me.GLookUp_TeleList.Properties.PopupFormSize = New System.Drawing.Size(600, 250)
        Me.GLookUp_TeleList.Properties.ReadOnly = True
        Me.GLookUp_TeleList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GLookUp_TeleList.Properties.View = Me.GLookUp_TeleListView
        Me.GLookUp_TeleList.Size = New System.Drawing.Size(186, 20)
        Me.GLookUp_TeleList.StyleController = Me.Layout_GS
        ToolTipTitleItem2.Text = "Information..."
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "Select Telephone No..."
        ToolTipTitleItem3.LeftIndent = 6
        ToolTipTitleItem3.Text = "Use F4 function key to Show List."
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        SuperToolTip2.Items.Add(ToolTipItem2)
        SuperToolTip2.Items.Add(ToolTipSeparatorItem1)
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        Me.GLookUp_TeleList.SuperTip = SuperToolTip2
        Me.GLookUp_TeleList.TabIndex = 0
        '
        'GLookUp_TeleListView
        '
        Me.GLookUp_TeleListView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.TP_NO, Me.TP_COMPANY, Me.TP_CATEGORY, Me.TP_TYPE, Me.TP_ID})
        Me.GLookUp_TeleListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GLookUp_TeleListView.Name = "GLookUp_TeleListView"
        Me.GLookUp_TeleListView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_TeleListView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_TeleListView.OptionsBehavior.ReadOnly = True
        Me.GLookUp_TeleListView.OptionsCustomization.AllowFilter = False
        Me.GLookUp_TeleListView.OptionsCustomization.AllowGroup = False
        Me.GLookUp_TeleListView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GLookUp_TeleListView.OptionsLayout.Columns.AddNewColumns = False
        Me.GLookUp_TeleListView.OptionsLayout.Columns.RemoveOldColumns = False
        Me.GLookUp_TeleListView.OptionsMenu.EnableColumnMenu = False
        Me.GLookUp_TeleListView.OptionsMenu.EnableFooterMenu = False
        Me.GLookUp_TeleListView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GLookUp_TeleListView.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GLookUp_TeleListView.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GLookUp_TeleListView.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.GLookUp_TeleListView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GLookUp_TeleListView.OptionsSelection.InvertSelection = True
        Me.GLookUp_TeleListView.OptionsView.EnableAppearanceEvenRow = True
        Me.GLookUp_TeleListView.OptionsView.EnableAppearanceOddRow = True
        Me.GLookUp_TeleListView.OptionsView.ShowGroupPanel = False
        Me.GLookUp_TeleListView.OptionsView.ShowIndicator = False
        '
        'TP_NO
        '
        Me.TP_NO.Caption = "Telephone No."
        Me.TP_NO.FieldName = "TP_NO"
        Me.TP_NO.Name = "TP_NO"
        Me.TP_NO.Visible = True
        Me.TP_NO.VisibleIndex = 0
        Me.TP_NO.Width = 180
        '
        'TP_COMPANY
        '
        Me.TP_COMPANY.Caption = "Company"
        Me.TP_COMPANY.FieldName = "TP_COMPANY"
        Me.TP_COMPANY.Name = "TP_COMPANY"
        Me.TP_COMPANY.Visible = True
        Me.TP_COMPANY.VisibleIndex = 1
        Me.TP_COMPANY.Width = 100
        '
        'TP_CATEGORY
        '
        Me.TP_CATEGORY.Caption = "Category"
        Me.TP_CATEGORY.FieldName = "TP_CATEGORY"
        Me.TP_CATEGORY.Name = "TP_CATEGORY"
        Me.TP_CATEGORY.Visible = True
        Me.TP_CATEGORY.VisibleIndex = 2
        Me.TP_CATEGORY.Width = 120
        '
        'TP_TYPE
        '
        Me.TP_TYPE.Caption = "Plan Type"
        Me.TP_TYPE.FieldName = "TP_TYPE"
        Me.TP_TYPE.Name = "TP_TYPE"
        Me.TP_TYPE.Visible = True
        Me.TP_TYPE.VisibleIndex = 3
        '
        'TP_ID
        '
        Me.TP_ID.Caption = "TP_ID"
        Me.TP_ID.FieldName = "TP_ID"
        Me.TP_ID.Name = "TP_ID"
        '
        'BE_Category
        '
        Me.BE_Category.EditValue = ""
        Me.BE_Category.Enabled = False
        Me.BE_Category.EnterMoveNextControl = True
        Me.BE_Category.Location = New System.Drawing.Point(107, 36)
        Me.BE_Category.Name = "BE_Category"
        Me.BE_Category.Properties.AllowFocused = False
        Me.BE_Category.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Category.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Category.Properties.Appearance.Options.UseFont = True
        Me.BE_Category.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Category.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Category.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Category.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Category.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Category.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Category.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Category.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Category.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Category.Size = New System.Drawing.Size(186, 20)
        Me.BE_Category.StyleController = Me.Layout_GS
        Me.BE_Category.TabIndex = 182
        '
        'BE_Company
        '
        Me.BE_Company.EditValue = ""
        Me.BE_Company.Enabled = False
        Me.BE_Company.EnterMoveNextControl = True
        Me.BE_Company.Location = New System.Drawing.Point(404, 6)
        Me.BE_Company.Name = "BE_Company"
        Me.BE_Company.Properties.AllowFocused = False
        Me.BE_Company.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Company.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Company.Properties.Appearance.Options.UseFont = True
        Me.BE_Company.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Company.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Company.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Company.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Company.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Company.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Company.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Company.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Company.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Company.Size = New System.Drawing.Size(186, 20)
        Me.BE_Company.StyleController = Me.Layout_GS
        Me.BE_Company.TabIndex = 181
        '
        'BE_Plantype
        '
        Me.BE_Plantype.EditValue = ""
        Me.BE_Plantype.Enabled = False
        Me.BE_Plantype.EnterMoveNextControl = True
        Me.BE_Plantype.Location = New System.Drawing.Point(404, 36)
        Me.BE_Plantype.Name = "BE_Plantype"
        Me.BE_Plantype.Properties.AllowFocused = False
        Me.BE_Plantype.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Plantype.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Plantype.Properties.Appearance.Options.UseFont = True
        Me.BE_Plantype.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Plantype.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Plantype.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Plantype.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Plantype.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Plantype.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Plantype.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Plantype.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Plantype.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Plantype.Size = New System.Drawing.Size(186, 20)
        Me.BE_Plantype.StyleController = Me.Layout_GS
        Me.BE_Plantype.TabIndex = 183
        '
        'Layout_GS_Group
        '
        Me.Layout_GS_Group.CustomizationFormText = "Root"
        Me.Layout_GS_Group.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Layout_GS_Group.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.LayoutControlItem3, Me.LayoutControlItem7, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem4})
        Me.Layout_GS_Group.Location = New System.Drawing.Point(0, 0)
        Me.Layout_GS_Group.Name = "Root"
        Me.Layout_GS_Group.OptionsItemText.TextToControlDistance = 5
        Me.Layout_GS_Group.Size = New System.Drawing.Size(596, 92)
        Me.Layout_GS_Group.Text = "Root"
        Me.Layout_GS_Group.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem5.Control = Me.GLookUp_TeleList
        Me.LayoutControlItem5.CustomizationFormText = "Telephone No.:"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem5.Text = "Telephone No.:"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(96, 14)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.BE_Category
        Me.LayoutControlItem3.CustomizationFormText = "A/c. No.:"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem3.Text = "Category:"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(96, 14)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.BE_Company
        Me.LayoutControlItem7.CustomizationFormText = "Company:"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(297, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem7.Text = "Company:"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(96, 14)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.BE_Plantype
        Me.LayoutControlItem1.CustomizationFormText = "Plan Type:"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(297, 30)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem1.Text = "Plan Type:"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(96, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem2.Control = Me.Txt_Fr_Date
        Me.LayoutControlItem2.CustomizationFormText = "Period From :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem2.Text = "Period From :"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(96, 14)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem4.Control = Me.Txt_To_Date
        Me.LayoutControlItem4.CustomizationFormText = "Period To :"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(297, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(297, 30)
        Me.LayoutControlItem4.Text = "Period To :"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(96, 14)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'xID
        '
        Me.xID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.xID.AutoSize = True
        Me.xID.Location = New System.Drawing.Point(268, 136)
        Me.xID.Name = "xID"
        Me.xID.Size = New System.Drawing.Size(24, 13)
        Me.xID.TabIndex = 9
        Me.xID.Text = "xID"
        Me.xID.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TopLine
        '
        Me.TopLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopLine.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TopLine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TopLine.LineVisible = True
        Me.TopLine.Location = New System.Drawing.Point(0, 32)
        Me.TopLine.LookAndFeel.SkinName = "Liquid Sky"
        Me.TopLine.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TopLine.Name = "TopLine"
        Me.TopLine.Size = New System.Drawing.Size(593, 3)
        Me.TopLine.TabIndex = 47
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Tan
        Me.TopPanel.BackgroundImage = CType(resources.GetObject("TopPanel.BackgroundImage"), System.Drawing.Image)
        Me.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Controls.Add(Me.LabelControl1)
        Me.TopPanel.Controls.Add(Me.TitleX)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(594, 34)
        Me.TopPanel.TabIndex = 33
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel2.Location = New System.Drawing.Point(3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(30, 30)
        Me.Panel2.TabIndex = 112
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl1.Location = New System.Drawing.Point(407, 7)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(183, 20)
        Me.LabelControl1.TabIndex = 111
        Me.LabelControl1.Text = "Red fields are mandatory."
        '
        'TitleX
        '
        Me.TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.TitleX.Location = New System.Drawing.Point(40, 4)
        Me.TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TitleX.Name = "TitleX"
        Me.TitleX.Size = New System.Drawing.Size(310, 26)
        Me.TitleX.TabIndex = 5
        Me.TitleX.Text = "Telephone Bill Payment Details"
        '
        'BUT_SAVE_COM
        '
        Me.BUT_SAVE_COM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SAVE_COM.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SAVE_COM.Appearance.Options.UseFont = True
        Me.BUT_SAVE_COM.Image = CType(resources.GetObject("BUT_SAVE_COM.Image"), System.Drawing.Image)
        Me.BUT_SAVE_COM.Location = New System.Drawing.Point(452, 128)
        Me.BUT_SAVE_COM.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_SAVE_COM.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SAVE_COM.Name = "BUT_SAVE_COM"
        Me.BUT_SAVE_COM.Size = New System.Drawing.Size(70, 27)
        Me.BUT_SAVE_COM.TabIndex = 8
        Me.BUT_SAVE_COM.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(521, 128)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 27)
        Me.BUT_CANCEL.TabIndex = 9
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'BUT_DEL
        '
        Me.BUT_DEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_DEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_DEL.Appearance.Options.UseFont = True
        Me.BUT_DEL.Image = CType(resources.GetObject("BUT_DEL.Image"), System.Drawing.Image)
        Me.BUT_DEL.Location = New System.Drawing.Point(452, 128)
        Me.BUT_DEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_DEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_DEL.Name = "BUT_DEL"
        Me.BUT_DEL.Size = New System.Drawing.Size(70, 27)
        Me.BUT_DEL.TabIndex = 8
        Me.BUT_DEL.Text = "Delete"
        Me.BUT_DEL.Visible = False
        '
        'Dialog_Telephone_Bill
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 157)
        Me.Controls.Add(Me.Layout_GS)
        Me.Controls.Add(Me.xID)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.BUT_SAVE_COM)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.TopLine)
        Me.Controls.Add(Me.BUT_DEL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dialog_Telephone_Bill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Telephone Bill Payment Details"
        CType(Me.Layout_GS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Layout_GS.ResumeLayout(False)
        CType(Me.Txt_To_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_To_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Fr_Date.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Fr_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_TeleList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_TeleListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Category.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Company.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Plantype.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Layout_GS_Group, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_SAVE_COM As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xID As System.Windows.Forms.Label
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TopLine As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Layout_GS As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Layout_GS_Group As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BUT_DEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GLookUp_TeleList As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GLookUp_TeleListView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TP_NO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TP_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TP_CATEGORY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TP_COMPANY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BE_Company As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_Category As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_Plantype As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TP_TYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Txt_To_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Txt_Fr_Date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
