<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AdvancedFilters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_AdvancedFilters))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim ToolTipSeparatorItem1 As DevExpress.Utils.ToolTipSeparatorItem = New DevExpress.Utils.ToolTipSeparatorItem()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_OK = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.Lbl_FilterTypes = New DevExpress.XtraEditors.LabelControl()
        Me.Cmb_FilterTypes = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Lbl_FilterCriteria = New DevExpress.XtraEditors.LabelControl()
        Me.GLookUp_FilterCriteria = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GLookUp_FilterCriteriaView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1.SuspendLayout()
        CType(Me.Cmb_FilterTypes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_FilterCriteria.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GLookUp_FilterCriteriaView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(0, 34)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(450, 1)
        Me.LabelControl1.TabIndex = 144
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(404, 34)
        Me.Panel1.TabIndex = 145
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 32)
        Me.Panel2.TabIndex = 6
        '
        'Txt_TitleX
        '
        Me.Txt_TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!)
        Me.Txt_TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_TitleX.Location = New System.Drawing.Point(40, 4)
        Me.Txt_TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Txt_TitleX.Name = "Txt_TitleX"
        Me.Txt_TitleX.Size = New System.Drawing.Size(168, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Advanced Filters"
        '
        'BUT_OK
        '
        Me.BUT_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_OK.Image = CType(resources.GetObject("BUT_OK.Image"), System.Drawing.Image)
        Me.BUT_OK.Location = New System.Drawing.Point(263, 142)
        Me.BUT_OK.Name = "BUT_OK"
        Me.BUT_OK.Size = New System.Drawing.Size(70, 28)
        Me.BUT_OK.TabIndex = 3
        Me.BUT_OK.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(333, 142)
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 28)
        Me.BUT_CANCEL.TabIndex = 4
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'Lbl_FilterTypes
        '
        Me.Lbl_FilterTypes.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_FilterTypes.Location = New System.Drawing.Point(8, 40)
        Me.Lbl_FilterTypes.Name = "Lbl_FilterTypes"
        Me.Lbl_FilterTypes.Size = New System.Drawing.Size(81, 14)
        Me.Lbl_FilterTypes.TabIndex = 174
        Me.Lbl_FilterTypes.Text = "Filter Types :"
        '
        'Cmb_FilterTypes
        '
        Me.Cmb_FilterTypes.EditValue = ""
        Me.Cmb_FilterTypes.EnterMoveNextControl = True
        Me.Cmb_FilterTypes.Location = New System.Drawing.Point(8, 60)
        Me.Cmb_FilterTypes.Name = "Cmb_FilterTypes"
        Me.Cmb_FilterTypes.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Cmb_FilterTypes.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Cmb_FilterTypes.Properties.Appearance.Options.UseFont = True
        Me.Cmb_FilterTypes.Properties.Appearance.Options.UseForeColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Cmb_FilterTypes.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.Cmb_FilterTypes.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Cmb_FilterTypes.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceDropDown.Options.UseFont = True
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.Options.UseFont = True
        Me.Cmb_FilterTypes.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Cmb_FilterTypes.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Cmb_FilterTypes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Cmb_FilterTypes.Properties.DropDownRows = 8
        Me.Cmb_FilterTypes.Properties.ImmediatePopup = True
        Me.Cmb_FilterTypes.Properties.Items.AddRange(New Object() {"FD", "LAND AND BUILDING", "GOLD", "SILVER", "MOVABLE ASSETS", "VEHICLES", "LIVESTOCK", "ADVANCES", "LIABILITIES", "OTHER DEPOSITS", "BANK ACCOUNTS"})
        Me.Cmb_FilterTypes.Properties.NullValuePrompt = "Select Type..."
        Me.Cmb_FilterTypes.Properties.NullValuePromptShowForEmptyValue = True
        Me.Cmb_FilterTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.Cmb_FilterTypes.Size = New System.Drawing.Size(390, 20)
        Me.Cmb_FilterTypes.TabIndex = 176
        '
        'Lbl_FilterCriteria
        '
        Me.Lbl_FilterCriteria.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_FilterCriteria.Location = New System.Drawing.Point(8, 86)
        Me.Lbl_FilterCriteria.Name = "Lbl_FilterCriteria"
        Me.Lbl_FilterCriteria.Size = New System.Drawing.Size(90, 14)
        Me.Lbl_FilterCriteria.TabIndex = 177
        Me.Lbl_FilterCriteria.Text = "Filter Criteria :"
        '
        'GLookUp_FilterCriteria
        '
        Me.GLookUp_FilterCriteria.EnterMoveNextControl = True
        Me.GLookUp_FilterCriteria.Location = New System.Drawing.Point(8, 106)
        Me.GLookUp_FilterCriteria.Name = "GLookUp_FilterCriteria"
        Me.GLookUp_FilterCriteria.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GLookUp_FilterCriteria.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.GLookUp_FilterCriteria.Properties.Appearance.Options.UseFont = True
        Me.GLookUp_FilterCriteria.Properties.Appearance.Options.UseForeColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FloralWhite
        Me.GLookUp_FilterCriteria.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_FilterCriteria.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.Options.UseFont = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GLookUp_FilterCriteria.Properties.AppearanceReadOnly.Options.UseForeColor = True
        ToolTipTitleItem1.Text = "Advanced Filter (On/Off)"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Enabled : Auto Filter Bar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Column Filter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Custom Filter Edito" & _
    "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabled : Footer Filter Display"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.GLookUp_FilterCriteria.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Filter : Off", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "OFF", SuperToolTip1, True)})
        Me.GLookUp_FilterCriteria.Properties.ImmediatePopup = True
        Me.GLookUp_FilterCriteria.Properties.NullText = ""
        Me.GLookUp_FilterCriteria.Properties.NullValuePrompt = "Select Criteria..."
        Me.GLookUp_FilterCriteria.Properties.NullValuePromptShowForEmptyValue = True
        Me.GLookUp_FilterCriteria.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GLookUp_FilterCriteria.Properties.PopupFormMinSize = New System.Drawing.Size(484, 250)
        Me.GLookUp_FilterCriteria.Properties.PopupFormSize = New System.Drawing.Size(484, 250)
        Me.GLookUp_FilterCriteria.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GLookUp_FilterCriteria.Properties.View = Me.GLookUp_FilterCriteriaView
        Me.GLookUp_FilterCriteria.Size = New System.Drawing.Size(390, 20)
        ToolTipTitleItem2.Text = "Information..."
        ToolTipItem2.LeftIndent = 6
        ToolTipItem2.Text = "Select Criteria.."
        ToolTipTitleItem3.LeftIndent = 6
        ToolTipTitleItem3.Text = "Use F4 function key to Show List."
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        SuperToolTip2.Items.Add(ToolTipItem2)
        SuperToolTip2.Items.Add(ToolTipSeparatorItem1)
        SuperToolTip2.Items.Add(ToolTipTitleItem3)
        Me.GLookUp_FilterCriteria.SuperTip = SuperToolTip2
        Me.GLookUp_FilterCriteria.TabIndex = 178
        '
        'GLookUp_FilterCriteriaView
        '
        Me.GLookUp_FilterCriteriaView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GLookUp_FilterCriteriaView.Name = "GLookUp_FilterCriteriaView"
        Me.GLookUp_FilterCriteriaView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_FilterCriteriaView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GLookUp_FilterCriteriaView.OptionsBehavior.AutoPopulateColumns = False
        Me.GLookUp_FilterCriteriaView.OptionsBehavior.ReadOnly = True
        Me.GLookUp_FilterCriteriaView.OptionsCustomization.AllowFilter = False
        Me.GLookUp_FilterCriteriaView.OptionsCustomization.AllowGroup = False
        Me.GLookUp_FilterCriteriaView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GLookUp_FilterCriteriaView.OptionsLayout.Columns.AddNewColumns = False
        Me.GLookUp_FilterCriteriaView.OptionsLayout.Columns.RemoveOldColumns = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.EnableColumnMenu = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.EnableFooterMenu = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GLookUp_FilterCriteriaView.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.GLookUp_FilterCriteriaView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GLookUp_FilterCriteriaView.OptionsSelection.InvertSelection = True
        Me.GLookUp_FilterCriteriaView.OptionsView.EnableAppearanceEvenRow = True
        Me.GLookUp_FilterCriteriaView.OptionsView.EnableAppearanceOddRow = True
        Me.GLookUp_FilterCriteriaView.OptionsView.ShowGroupPanel = False
        Me.GLookUp_FilterCriteriaView.OptionsView.ShowIndicator = False
        '
        'Frm_AdvancedFilters
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 171)
        Me.Controls.Add(Me.GLookUp_FilterCriteria)
        Me.Controls.Add(Me.Lbl_FilterCriteria)
        Me.Controls.Add(Me.Cmb_FilterTypes)
        Me.Controls.Add(Me.Lbl_FilterTypes)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.BUT_OK)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_AdvancedFilters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Advanced Filters"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Cmb_FilterTypes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_FilterCriteria.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GLookUp_FilterCriteriaView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_OK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Lbl_FilterTypes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Cmb_FilterTypes As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Lbl_FilterCriteria As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GLookUp_FilterCriteria As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GLookUp_FilterCriteriaView As DevExpress.XtraGrid.Views.Grid.GridView

End Class
