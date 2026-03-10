<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog_DailyBalances
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog_DailyBalances))
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BE_Bank_Acc_No = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Bank_Branch = New DevExpress.XtraEditors.ButtonEdit()
        Me.GLookUp_BankList = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GLookUp_BankListView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BANK_NAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BANK_BRANCH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BANK_ACC_NO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BA_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TopLine = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BE_View_Period = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.rdo_Balances_Mode = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.Lbl_Separator2 = New DevExpress.XtraEditors.LabelControl()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_SAVE_COM = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_DEL = New DevExpress.XtraEditors.SimpleButton()
        Me.Cmb_View = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioGroup2 = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioGroup3 = New DevExpress.XtraEditors.RadioGroup()
        CType(Me.BE_Bank_Acc_No.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Bank_Branch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_BankList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_BankListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdo_Balances_Mode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroup2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroup3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BE_Bank_Acc_No
        '
        Me.BE_Bank_Acc_No.EditValue = ""
        Me.BE_Bank_Acc_No.Enabled = False
        Me.BE_Bank_Acc_No.EnterMoveNextControl = True
        Me.BE_Bank_Acc_No.Location = New System.Drawing.Point(78, 202)
        Me.BE_Bank_Acc_No.Name = "BE_Bank_Acc_No"
        Me.BE_Bank_Acc_No.Properties.AllowFocused = False
        Me.BE_Bank_Acc_No.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Acc_No.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Bank_Acc_No.Properties.Appearance.Options.UseFont = True
        Me.BE_Bank_Acc_No.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Bank_Acc_No.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Bank_Acc_No.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Bank_Acc_No.Size = New System.Drawing.Size(304, 20)
        Me.BE_Bank_Acc_No.TabIndex = 7
        Me.BE_Bank_Acc_No.TabStop = False
        '
        'BE_Bank_Branch
        '
        Me.BE_Bank_Branch.EditValue = ""
        Me.BE_Bank_Branch.Enabled = False
        Me.BE_Bank_Branch.EnterMoveNextControl = True
        Me.BE_Bank_Branch.Location = New System.Drawing.Point(78, 179)
        Me.BE_Bank_Branch.Name = "BE_Bank_Branch"
        Me.BE_Bank_Branch.Properties.AllowFocused = False
        Me.BE_Bank_Branch.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Branch.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Bank_Branch.Properties.Appearance.Options.UseFont = True
        Me.BE_Bank_Branch.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Bank_Branch.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Bank_Branch.Properties.NullValuePrompt = "Bank Branch Name..."
        Me.BE_Bank_Branch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Bank_Branch.Size = New System.Drawing.Size(304, 20)
        Me.BE_Bank_Branch.TabIndex = 6
        Me.BE_Bank_Branch.TabStop = False
        '
        'GLookUp_BankList
        '
        Me.GLookUp_BankList.Enabled = False
        Me.GLookUp_BankList.EnterMoveNextControl = True
        Me.GLookUp_BankList.Location = New System.Drawing.Point(78, 156)
        Me.GLookUp_BankList.Name = "GLookUp_BankList"
        Me.GLookUp_BankList.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GLookUp_BankList.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.GLookUp_BankList.Properties.Appearance.Options.UseFont = True
        Me.GLookUp_BankList.Properties.Appearance.Options.UseForeColor = True
        Me.GLookUp_BankList.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.GLookUp_BankList.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_BankList.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GLookUp_BankList.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GLookUp_BankList.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.GLookUp_BankList.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.GLookUp_BankList.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_BankList.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GLookUp_BankList.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GLookUp_BankList.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.GLookUp_BankList.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_BankList.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.GLookUp_BankList.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GLookUp_BankList.Properties.AppearanceFocused.Options.UseFont = True
        Me.GLookUp_BankList.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GLookUp_BankList.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem1.Text = "Advanced Filter (On/Off)"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Enabled : Auto Filter Bar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Column Filter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Custom Filter Edito" & _
    "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Footer Filter Display"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.GLookUp_BankList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Filter : Off", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "OFF", SuperToolTip1, True)})
        Me.GLookUp_BankList.Properties.ImmediatePopup = True
        Me.GLookUp_BankList.Properties.NullText = ""
        Me.GLookUp_BankList.Properties.NullValuePrompt = "Select Bank Name..."
        Me.GLookUp_BankList.Properties.NullValuePromptShowForEmptyValue = True
        Me.GLookUp_BankList.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GLookUp_BankList.Properties.PopupFormMinSize = New System.Drawing.Size(670, 110)
        Me.GLookUp_BankList.Properties.PopupFormSize = New System.Drawing.Size(670, 110)
        Me.GLookUp_BankList.Properties.ReadOnly = True
        Me.GLookUp_BankList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GLookUp_BankList.Properties.View = Me.GLookUp_BankListView
        Me.GLookUp_BankList.Size = New System.Drawing.Size(304, 20)
        ToolTipTitleItem2.Text = "Information..."
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "Select Bank Name.."
        ToolTipTitleItem3.LeftIndent = 6
        ToolTipTitleItem3.Text = "Use F4 function key to Show List."
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        SuperToolTip2.Items.Add(ToolTipItem2)
        SuperToolTip2.Items.Add(ToolTipSeparatorItem1)
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        Me.GLookUp_BankList.SuperTip = SuperToolTip2
        Me.GLookUp_BankList.TabIndex = 5
        '
        'GLookUp_BankListView
        '
        Me.GLookUp_BankListView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.BANK_NAME, Me.BANK_BRANCH, Me.BANK_ACC_NO, Me.BA_ID})
        Me.GLookUp_BankListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GLookUp_BankListView.Name = "GLookUp_BankListView"
        Me.GLookUp_BankListView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_BankListView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_BankListView.OptionsBehavior.ReadOnly = True
        Me.GLookUp_BankListView.OptionsCustomization.AllowFilter = False
        Me.GLookUp_BankListView.OptionsCustomization.AllowGroup = False
        Me.GLookUp_BankListView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GLookUp_BankListView.OptionsLayout.Columns.AddNewColumns = False
        Me.GLookUp_BankListView.OptionsLayout.Columns.RemoveOldColumns = False
        Me.GLookUp_BankListView.OptionsMenu.EnableColumnMenu = False
        Me.GLookUp_BankListView.OptionsMenu.EnableFooterMenu = False
        Me.GLookUp_BankListView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GLookUp_BankListView.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GLookUp_BankListView.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GLookUp_BankListView.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.GLookUp_BankListView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GLookUp_BankListView.OptionsSelection.InvertSelection = True
        Me.GLookUp_BankListView.OptionsView.EnableAppearanceEvenRow = True
        Me.GLookUp_BankListView.OptionsView.EnableAppearanceOddRow = True
        Me.GLookUp_BankListView.OptionsView.ShowGroupPanel = False
        Me.GLookUp_BankListView.OptionsView.ShowIndicator = False
        '
        'BANK_NAME
        '
        Me.BANK_NAME.Caption = "Name"
        Me.BANK_NAME.FieldName = "BANK_NAME"
        Me.BANK_NAME.Name = "BANK_NAME"
        Me.BANK_NAME.Visible = True
        Me.BANK_NAME.VisibleIndex = 0
        Me.BANK_NAME.Width = 180
        '
        'BANK_BRANCH
        '
        Me.BANK_BRANCH.Caption = "Branch"
        Me.BANK_BRANCH.FieldName = "BANK_BRANCH"
        Me.BANK_BRANCH.Name = "BANK_BRANCH"
        Me.BANK_BRANCH.Visible = True
        Me.BANK_BRANCH.VisibleIndex = 1
        Me.BANK_BRANCH.Width = 100
        '
        'BANK_ACC_NO
        '
        Me.BANK_ACC_NO.Caption = "Account No."
        Me.BANK_ACC_NO.FieldName = "BANK_ACC_NO"
        Me.BANK_ACC_NO.Name = "BANK_ACC_NO"
        Me.BANK_ACC_NO.Visible = True
        Me.BANK_ACC_NO.VisibleIndex = 2
        Me.BANK_ACC_NO.Width = 120
        '
        'BA_ID
        '
        Me.BA_ID.Caption = "BA_ID"
        Me.BA_ID.FieldName = "BA_ID"
        Me.BA_ID.Name = "BA_ID"
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
        Me.TopLine.Size = New System.Drawing.Size(384, 3)
        Me.TopLine.TabIndex = 47
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(3, 65)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(45, 14)
        Me.LabelControl3.TabIndex = 181
        Me.LabelControl3.Text = "Period:"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(3, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(69, 14)
        Me.LabelControl2.TabIndex = 180
        Me.LabelControl2.Text = "View Type:"
        '
        'BE_View_Period
        '
        Me.BE_View_Period.EditValue = "View Period"
        Me.BE_View_Period.EnterMoveNextControl = True
        Me.BE_View_Period.Location = New System.Drawing.Point(78, 62)
        Me.BE_View_Period.Name = "BE_View_Period"
        Me.BE_View_Period.Properties.AllowFocused = False
        Me.BE_View_Period.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BE_View_Period.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_View_Period.Properties.Appearance.Options.UseFont = True
        Me.BE_View_Period.Properties.Appearance.Options.UseForeColor = True
        Me.BE_View_Period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_View_Period.Size = New System.Drawing.Size(304, 20)
        Me.BE_View_Period.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(3, 89)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(61, 14)
        Me.LabelControl1.TabIndex = 183
        Me.LabelControl1.Text = "Balances:"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(3, 158)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl4.TabIndex = 184
        Me.LabelControl4.Text = "Bank:"
        '
        'rdo_Balances_Mode
        '
        Me.rdo_Balances_Mode.EditValue = "CASH"
        Me.rdo_Balances_Mode.EnterMoveNextControl = True
        Me.rdo_Balances_Mode.Location = New System.Drawing.Point(78, 86)
        Me.rdo_Balances_Mode.Name = "rdo_Balances_Mode"
        Me.rdo_Balances_Mode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.rdo_Balances_Mode.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdo_Balances_Mode.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.rdo_Balances_Mode.Properties.Appearance.Options.UseBackColor = True
        Me.rdo_Balances_Mode.Properties.Appearance.Options.UseFont = True
        Me.rdo_Balances_Mode.Properties.Appearance.Options.UseForeColor = True
        Me.rdo_Balances_Mode.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("CASH", "CASH"), New DevExpress.XtraEditors.Controls.RadioGroupItem("BANK", "BANK")})
        Me.rdo_Balances_Mode.Properties.LookAndFeel.SkinName = "Money Twins"
        Me.rdo_Balances_Mode.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.rdo_Balances_Mode.Size = New System.Drawing.Size(304, 20)
        Me.rdo_Balances_Mode.TabIndex = 2
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(3, 181)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(48, 14)
        Me.LabelControl5.TabIndex = 186
        Me.LabelControl5.Text = "Branch:"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(3, 204)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(53, 14)
        Me.LabelControl6.TabIndex = 187
        Me.LabelControl6.Text = "A/c. No.:"
        '
        'Lbl_Separator2
        '
        Me.Lbl_Separator2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Lbl_Separator2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Separator2.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Separator2.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.Lbl_Separator2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.Lbl_Separator2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Lbl_Separator2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.Lbl_Separator2.LineVisible = True
        Me.Lbl_Separator2.Location = New System.Drawing.Point(0, 225)
        Me.Lbl_Separator2.LookAndFeel.SkinName = "Liquid Sky"
        Me.Lbl_Separator2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Lbl_Separator2.Name = "Lbl_Separator2"
        Me.Lbl_Separator2.Size = New System.Drawing.Size(600, 1)
        Me.Lbl_Separator2.TabIndex = 188
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Tan
        Me.TopPanel.BackgroundImage = CType(resources.GetObject("TopPanel.BackgroundImage"), System.Drawing.Image)
        Me.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Controls.Add(Me.TitleX)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(385, 34)
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
        Me.Panel2.TabIndex = 45
        '
        'TitleX
        '
        Me.TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.TitleX.Location = New System.Drawing.Point(40, 4)
        Me.TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TitleX.Name = "TitleX"
        Me.TitleX.Size = New System.Drawing.Size(150, 26)
        Me.TitleX.TabIndex = 5
        Me.TitleX.Text = "Daily Balances"
        '
        'BUT_SAVE_COM
        '
        Me.BUT_SAVE_COM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SAVE_COM.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SAVE_COM.Appearance.Options.UseFont = True
        Me.BUT_SAVE_COM.Image = CType(resources.GetObject("BUT_SAVE_COM.Image"), System.Drawing.Image)
        Me.BUT_SAVE_COM.Location = New System.Drawing.Point(243, 229)
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
        Me.BUT_CANCEL.Location = New System.Drawing.Point(312, 229)
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
        Me.BUT_DEL.Location = New System.Drawing.Point(243, 229)
        Me.BUT_DEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_DEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_DEL.Name = "BUT_DEL"
        Me.BUT_DEL.Size = New System.Drawing.Size(70, 27)
        Me.BUT_DEL.TabIndex = 8
        Me.BUT_DEL.Text = "Delete"
        Me.BUT_DEL.Visible = False
        '
        'Cmb_View
        '
        Me.Cmb_View.EditValue = ""
        Me.Cmb_View.EnterMoveNextControl = True
        Me.Cmb_View.Location = New System.Drawing.Point(78, 38)
        Me.Cmb_View.Name = "Cmb_View"
        Me.Cmb_View.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_View.Properties.Appearance.Options.UseFont = True
        Me.Cmb_View.Properties.Appearance.Options.UseForeColor = True
        Me.Cmb_View.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Cmb_View.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_View.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray
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
        Me.Cmb_View.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Cmb_View.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem4.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem4.Appearance.Options.UseImage = True
        ToolTipTitleItem4.Image = CType(resources.GetObject("ToolTipTitleItem4.Image"), System.Drawing.Image)
        ToolTipTitleItem4.Text = "To set specific period..."
        ToolTipItem3.LeftIndent = 6
        ToolTipItem3.Text = "        Shortcut Key"
        ToolTipTitleItem5.LeftIndent = 6
        ToolTipTitleItem5.Text = "          Ctrl + F2"
        SuperToolTip3.Items.Add(ToolTipTitleItem4)
        SuperToolTip3.Items.Add(ToolTipItem3)
        SuperToolTip3.Items.Add(ToolTipTitleItem5)
        Me.Cmb_View.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Change Period", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, SuperToolTip3, True)})
        Me.Cmb_View.Properties.DropDownRows = 21
        Me.Cmb_View.Properties.ImmediatePopup = True
        Me.Cmb_View.Properties.LookAndFeel.SkinName = "Liquid Sky"
        Me.Cmb_View.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Cmb_View.Properties.NullValuePrompt = "Select Type..."
        Me.Cmb_View.Properties.NullValuePromptShowForEmptyValue = True
        Me.Cmb_View.Properties.PopupFormMinSize = New System.Drawing.Size(300, 0)
        Me.Cmb_View.Properties.PopupFormSize = New System.Drawing.Size(300, 0)
        Me.Cmb_View.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.Cmb_View.Size = New System.Drawing.Size(304, 20)
        Me.Cmb_View.TabIndex = 0
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(3, 113)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(49, 14)
        Me.LabelControl7.TabIndex = 183
        Me.LabelControl7.Text = "Output:"
        '
        'RadioGroup2
        '
        Me.RadioGroup2.EditValue = "On Screen"
        Me.RadioGroup2.EnterMoveNextControl = True
        Me.RadioGroup2.Location = New System.Drawing.Point(78, 110)
        Me.RadioGroup2.Name = "RadioGroup2"
        Me.RadioGroup2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup2.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioGroup2.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.RadioGroup2.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup2.Properties.Appearance.Options.UseFont = True
        Me.RadioGroup2.Properties.Appearance.Options.UseForeColor = True
        Me.RadioGroup2.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("On Screen", "On Screen"), New DevExpress.XtraEditors.Controls.RadioGroupItem("Printable", "Printable")})
        Me.RadioGroup2.Properties.LookAndFeel.SkinName = "Money Twins"
        Me.RadioGroup2.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.RadioGroup2.Size = New System.Drawing.Size(304, 20)
        Me.RadioGroup2.TabIndex = 3
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(3, 136)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(46, 14)
        Me.LabelControl8.TabIndex = 183
        Me.LabelControl8.Text = "Status:"
        '
        'RadioGroup3
        '
        Me.RadioGroup3.EditValue = "All"
        Me.RadioGroup3.EnterMoveNextControl = True
        Me.RadioGroup3.Location = New System.Drawing.Point(78, 133)
        Me.RadioGroup3.Name = "RadioGroup3"
        Me.RadioGroup3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup3.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioGroup3.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.RadioGroup3.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup3.Properties.Appearance.Options.UseFont = True
        Me.RadioGroup3.Properties.Appearance.Options.UseForeColor = True
        Me.RadioGroup3.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("Unreconciled", "Unreconciled"), New DevExpress.XtraEditors.Controls.RadioGroupItem("All", "All")})
        Me.RadioGroup3.Properties.LookAndFeel.SkinName = "Money Twins"
        Me.RadioGroup3.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.RadioGroup3.Size = New System.Drawing.Size(304, 20)
        Me.RadioGroup3.TabIndex = 4
        '
        'Dialog_DailyBalances
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 258)
        Me.Controls.Add(Me.Cmb_View)
        Me.Controls.Add(Me.Lbl_Separator2)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.RadioGroup3)
        Me.Controls.Add(Me.RadioGroup2)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.rdo_Balances_Mode)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.BE_View_Period)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.BE_Bank_Acc_No)
        Me.Controls.Add(Me.BUT_SAVE_COM)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.Controls.Add(Me.GLookUp_BankList)
        Me.Controls.Add(Me.BE_Bank_Branch)
        Me.Controls.Add(Me.TopLine)
        Me.Controls.Add(Me.BUT_DEL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dialog_DailyBalances"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Daily Balances Report"
        CType(Me.BE_Bank_Acc_No.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Bank_Branch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_BankList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_BankListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_View_Period.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdo_Balances_Mode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        CType(Me.Cmb_View.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroup2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroup3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_SAVE_COM As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TopLine As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BUT_DEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GLookUp_BankListView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BANK_NAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BA_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BANK_ACC_NO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BANK_BRANCH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BE_Bank_Branch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BE_View_Period As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_Separator2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Public WithEvents RadioGroup2 As DevExpress.XtraEditors.RadioGroup
    Public WithEvents rdo_Balances_Mode As DevExpress.XtraEditors.RadioGroup
    Public WithEvents GLookUp_BankList As DevExpress.XtraEditors.GridLookUpEdit
    Public WithEvents BE_Bank_Acc_No As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Public WithEvents RadioGroup3 As DevExpress.XtraEditors.RadioGroup
    Public WithEvents Cmb_View As DevExpress.XtraEditors.ComboBoxEdit
End Class
