<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Voucher_Win_Gen_Pay_Adv
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Voucher_Win_Gen_Pay_Adv))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.Layout_GS = New DevExpress.XtraLayout.LayoutControl()
        Me.BE_OS_Amt = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Adjust_Amt = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_ItemName = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Other_Detail = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Purpose = New DevExpress.XtraEditors.ButtonEdit()
        Me.BE_Given_Date = New DevExpress.XtraEditors.ButtonEdit()
        Me.Txt_Amount = New DevExpress.XtraEditors.TextEdit()
        Me.BE_Adv_Amt = New DevExpress.XtraEditors.ButtonEdit()
        Me.Layout_GS_Group = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.xID = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TopLine = New DevExpress.XtraEditors.LabelControl()
        Me.Hyper_Request = New DevExpress.XtraEditors.LabelControl()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_SAVE_COM = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BUT_DEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BE_Refund_Amt = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.Layout_GS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Layout_GS.SuspendLayout()
        CType(Me.BE_OS_Amt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Adjust_Amt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_ItemName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Other_Detail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Purpose.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Given_Date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Amount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BE_Adv_Amt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Layout_GS_Group, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        CType(Me.BE_Refund_Amt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Layout_GS
        '
        Me.Layout_GS.AllowCustomizationMenu = False
        Me.Layout_GS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Layout_GS.Controls.Add(Me.BE_Refund_Amt)
        Me.Layout_GS.Controls.Add(Me.BE_OS_Amt)
        Me.Layout_GS.Controls.Add(Me.BE_Adjust_Amt)
        Me.Layout_GS.Controls.Add(Me.BE_ItemName)
        Me.Layout_GS.Controls.Add(Me.BE_Other_Detail)
        Me.Layout_GS.Controls.Add(Me.BE_Purpose)
        Me.Layout_GS.Controls.Add(Me.BE_Given_Date)
        Me.Layout_GS.Controls.Add(Me.Txt_Amount)
        Me.Layout_GS.Controls.Add(Me.BE_Adv_Amt)
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
        Me.Layout_GS.Size = New System.Drawing.Size(580, 152)
        Me.Layout_GS.TabIndex = 0
        Me.Layout_GS.Text = "LayoutControl1"
        '
        'BE_OS_Amt
        '
        Me.BE_OS_Amt.EditValue = ""
        Me.BE_OS_Amt.Enabled = False
        Me.BE_OS_Amt.EnterMoveNextControl = True
        Me.BE_OS_Amt.Location = New System.Drawing.Point(306, 126)
        Me.BE_OS_Amt.Name = "BE_OS_Amt"
        Me.BE_OS_Amt.Properties.AllowFocused = False
        Me.BE_OS_Amt.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_OS_Amt.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_OS_Amt.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_OS_Amt.Properties.Appearance.Options.UseBackColor = True
        Me.BE_OS_Amt.Properties.Appearance.Options.UseFont = True
        Me.BE_OS_Amt.Properties.Appearance.Options.UseForeColor = True
        Me.BE_OS_Amt.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_OS_Amt.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_OS_Amt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_OS_Amt.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_OS_Amt.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_OS_Amt.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_OS_Amt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_OS_Amt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_OS_Amt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_OS_Amt.Size = New System.Drawing.Size(94, 20)
        Me.BE_OS_Amt.StyleController = Me.Layout_GS
        Me.BE_OS_Amt.TabIndex = 184
        '
        'BE_Adjust_Amt
        '
        Me.BE_Adjust_Amt.EditValue = ""
        Me.BE_Adjust_Amt.Enabled = False
        Me.BE_Adjust_Amt.EnterMoveNextControl = True
        Me.BE_Adjust_Amt.Location = New System.Drawing.Point(306, 96)
        Me.BE_Adjust_Amt.Name = "BE_Adjust_Amt"
        Me.BE_Adjust_Amt.Properties.AllowFocused = False
        Me.BE_Adjust_Amt.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adjust_Amt.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Adjust_Amt.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Adjust_Amt.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Adjust_Amt.Properties.Appearance.Options.UseFont = True
        Me.BE_Adjust_Amt.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Adjust_Amt.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Adjust_Amt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adjust_Amt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Adjust_Amt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Adjust_Amt.Size = New System.Drawing.Size(94, 20)
        Me.BE_Adjust_Amt.StyleController = Me.Layout_GS
        Me.BE_Adjust_Amt.TabIndex = 183
        '
        'BE_ItemName
        '
        Me.BE_ItemName.EditValue = ""
        Me.BE_ItemName.Enabled = False
        Me.BE_ItemName.EnterMoveNextControl = True
        Me.BE_ItemName.Location = New System.Drawing.Point(101, 6)
        Me.BE_ItemName.Name = "BE_ItemName"
        Me.BE_ItemName.Properties.AllowFocused = False
        Me.BE_ItemName.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_ItemName.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_ItemName.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_ItemName.Properties.Appearance.Options.UseBackColor = True
        Me.BE_ItemName.Properties.Appearance.Options.UseFont = True
        Me.BE_ItemName.Properties.Appearance.Options.UseForeColor = True
        Me.BE_ItemName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_ItemName.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_ItemName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_ItemName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_ItemName.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_ItemName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_ItemName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_ItemName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_ItemName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_ItemName.Size = New System.Drawing.Size(473, 20)
        Me.BE_ItemName.StyleController = Me.Layout_GS
        Me.BE_ItemName.TabIndex = 183
        '
        'BE_Other_Detail
        '
        Me.BE_Other_Detail.EditValue = ""
        Me.BE_Other_Detail.Enabled = False
        Me.BE_Other_Detail.EnterMoveNextControl = True
        Me.BE_Other_Detail.Location = New System.Drawing.Point(101, 66)
        Me.BE_Other_Detail.Name = "BE_Other_Detail"
        Me.BE_Other_Detail.Properties.AllowFocused = False
        Me.BE_Other_Detail.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Other_Detail.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Other_Detail.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Other_Detail.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Other_Detail.Properties.Appearance.Options.UseFont = True
        Me.BE_Other_Detail.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Other_Detail.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Other_Detail.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Other_Detail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Other_Detail.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Other_Detail.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Other_Detail.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Other_Detail.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Other_Detail.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Other_Detail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Other_Detail.Size = New System.Drawing.Size(473, 20)
        Me.BE_Other_Detail.StyleController = Me.Layout_GS
        Me.BE_Other_Detail.TabIndex = 182
        '
        'BE_Purpose
        '
        Me.BE_Purpose.EditValue = ""
        Me.BE_Purpose.Enabled = False
        Me.BE_Purpose.EnterMoveNextControl = True
        Me.BE_Purpose.Location = New System.Drawing.Point(101, 36)
        Me.BE_Purpose.Name = "BE_Purpose"
        Me.BE_Purpose.Properties.AllowFocused = False
        Me.BE_Purpose.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Purpose.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Purpose.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Purpose.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Purpose.Properties.Appearance.Options.UseFont = True
        Me.BE_Purpose.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Purpose.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Purpose.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Purpose.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Purpose.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Purpose.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Purpose.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Purpose.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Purpose.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Purpose.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Purpose.Size = New System.Drawing.Size(473, 20)
        Me.BE_Purpose.StyleController = Me.Layout_GS
        Me.BE_Purpose.TabIndex = 182
        '
        'BE_Given_Date
        '
        Me.BE_Given_Date.EditValue = ""
        Me.BE_Given_Date.Enabled = False
        Me.BE_Given_Date.EnterMoveNextControl = True
        Me.BE_Given_Date.Location = New System.Drawing.Point(101, 126)
        Me.BE_Given_Date.Name = "BE_Given_Date"
        Me.BE_Given_Date.Properties.AllowFocused = False
        Me.BE_Given_Date.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Given_Date.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Given_Date.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Given_Date.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Given_Date.Properties.Appearance.Options.UseFont = True
        Me.BE_Given_Date.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Given_Date.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Given_Date.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Given_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Given_Date.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Given_Date.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Given_Date.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Given_Date.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Given_Date.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Given_Date.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Given_Date.Size = New System.Drawing.Size(100, 20)
        Me.BE_Given_Date.StyleController = Me.Layout_GS
        Me.BE_Given_Date.TabIndex = 181
        '
        'Txt_Amount
        '
        Me.Txt_Amount.EditValue = ""
        Me.Txt_Amount.EnterMoveNextControl = True
        Me.Txt_Amount.Location = New System.Drawing.Point(475, 126)
        Me.Txt_Amount.Name = "Txt_Amount"
        Me.Txt_Amount.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.Txt_Amount.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Amount.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.Txt_Amount.Properties.Appearance.Options.UseBackColor = True
        Me.Txt_Amount.Properties.Appearance.Options.UseFont = True
        Me.Txt_Amount.Properties.Appearance.Options.UseForeColor = True
        Me.Txt_Amount.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.Txt_Amount.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Me.Txt_Amount.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.Txt_Amount.Properties.AppearanceDisabled.Options.UseFont = True
        Me.Txt_Amount.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.Txt_Amount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan
        Me.Txt_Amount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Txt_Amount.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.Txt_Amount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Txt_Amount.Properties.AppearanceFocused.Options.UseFont = True
        Me.Txt_Amount.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.Txt_Amount.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White
        Me.Txt_Amount.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Txt_Amount.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray
        Me.Txt_Amount.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.Txt_Amount.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.Txt_Amount.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.Txt_Amount.Properties.DisplayFormat.FormatString = "f2"
        Me.Txt_Amount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Txt_Amount.Properties.EditFormat.FormatString = "f2"
        Me.Txt_Amount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Txt_Amount.Properties.Mask.EditMask = "f2"
        Me.Txt_Amount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.Txt_Amount.Properties.NullValuePrompt = "Type Payment..."
        Me.Txt_Amount.Properties.NullValuePromptShowForEmptyValue = True
        Me.Txt_Amount.Size = New System.Drawing.Size(99, 20)
        Me.Txt_Amount.StyleController = Me.Layout_GS
        Me.Txt_Amount.TabIndex = 0
        '
        'BE_Adv_Amt
        '
        Me.BE_Adv_Amt.EditValue = ""
        Me.BE_Adv_Amt.Enabled = False
        Me.BE_Adv_Amt.EnterMoveNextControl = True
        Me.BE_Adv_Amt.Location = New System.Drawing.Point(101, 96)
        Me.BE_Adv_Amt.Name = "BE_Adv_Amt"
        Me.BE_Adv_Amt.Properties.AllowFocused = False
        Me.BE_Adv_Amt.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adv_Amt.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Adv_Amt.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Adv_Amt.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Adv_Amt.Properties.Appearance.Options.UseFont = True
        Me.BE_Adv_Amt.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Adv_Amt.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Adv_Amt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Adv_Amt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Adv_Amt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Adv_Amt.Size = New System.Drawing.Size(100, 20)
        Me.BE_Adv_Amt.StyleController = Me.Layout_GS
        Me.BE_Adv_Amt.TabIndex = 182
        '
        'Layout_GS_Group
        '
        Me.Layout_GS_Group.CustomizationFormText = "Root"
        Me.Layout_GS_Group.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Layout_GS_Group.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem7, Me.LayoutControlItem2, Me.LayoutControlItem6, Me.LayoutControlItem8, Me.LayoutControlItem4, Me.LayoutControlItem9, Me.LayoutControlItem5})
        Me.Layout_GS_Group.Location = New System.Drawing.Point(0, 0)
        Me.Layout_GS_Group.Name = "Root"
        Me.Layout_GS_Group.OptionsItemText.TextToControlDistance = 5
        Me.Layout_GS_Group.Size = New System.Drawing.Size(580, 152)
        Me.Layout_GS_Group.Text = "Root"
        Me.Layout_GS_Group.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem1.Control = Me.Txt_Amount
        Me.LayoutControlItem1.CustomizationFormText = "Amount:"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(404, 120)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(174, 30)
        Me.LayoutControlItem1.Text = "Payment:"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(60, 14)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.BE_Purpose
        Me.LayoutControlItem3.CustomizationFormText = "Purpose:"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(578, 30)
        Me.LayoutControlItem3.Text = "Purpose:"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(90, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.BE_Given_Date
        Me.LayoutControlItem7.CustomizationFormText = "Head:"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(205, 30)
        Me.LayoutControlItem7.Text = "Given Date:"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(90, 20)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.BE_Other_Detail
        Me.LayoutControlItem2.CustomizationFormText = "Other Details:"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(578, 30)
        Me.LayoutControlItem2.Text = "Other Details:"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(90, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem6.Control = Me.BE_ItemName
        Me.LayoutControlItem6.CustomizationFormText = "Item Name:"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.ShowInCustomizationForm = False
        Me.LayoutControlItem6.Size = New System.Drawing.Size(578, 30)
        Me.LayoutControlItem6.Text = "Item Name:"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(90, 20)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.BE_Adjust_Amt
        Me.LayoutControlItem8.CustomizationFormText = "Paid:"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(205, 90)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(199, 30)
        Me.LayoutControlItem8.Text = "Adjusted:"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(90, 14)
        Me.LayoutControlItem8.TextToControlDistance = 5
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.BE_OS_Amt
        Me.LayoutControlItem9.CustomizationFormText = "Out-Standing:"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(205, 120)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(199, 30)
        Me.LayoutControlItem9.Text = "Out-Standing:"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(90, 14)
        Me.LayoutControlItem9.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.BE_Adv_Amt
        Me.LayoutControlItem4.CustomizationFormText = "Advance Amount:"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(205, 30)
        Me.LayoutControlItem4.Text = "Advance:"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(90, 14)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'xID
        '
        Me.xID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.xID.AutoSize = True
        Me.xID.Location = New System.Drawing.Point(250, 196)
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
        Me.TopLine.Size = New System.Drawing.Size(577, 3)
        Me.TopLine.TabIndex = 47
        '
        'Hyper_Request
        '
        Me.Hyper_Request.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Hyper_Request.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hyper_Request.Appearance.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Hyper_Request.Appearance.Image = CType(resources.GetObject("Hyper_Request.Appearance.Image"), System.Drawing.Image)
        Me.Hyper_Request.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hyper_Request.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.Hyper_Request.Location = New System.Drawing.Point(0, 192)
        Me.Hyper_Request.LookAndFeel.SkinName = "Liquid Sky"
        Me.Hyper_Request.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Hyper_Request.Name = "Hyper_Request"
        Me.Hyper_Request.Size = New System.Drawing.Size(228, 20)
        ToolTipTitleItem1.Appearance.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Post
        ToolTipTitleItem1.Appearance.Options.UseImage = True
        ToolTipTitleItem1.Image = Global.ConnectOne.D0010._001.My.Resources.Resources.Post
        ToolTipTitleItem1.Text = "Note..."
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "If you do not find or need any new information in this Window then click here to " & _
    "send your request to Madhuban."
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.Hyper_Request.SuperTip = SuperToolTip1
        Me.Hyper_Request.TabIndex = 112
        Me.Hyper_Request.Text = "Send Your Request to Madhuban"
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Tan
        Me.TopPanel.BackgroundImage = CType(resources.GetObject("TopPanel.BackgroundImage"), System.Drawing.Image)
        Me.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopPanel.Controls.Add(Me.LabelControl1)
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Controls.Add(Me.TitleX)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(578, 34)
        Me.TopPanel.TabIndex = 33
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl1.Location = New System.Drawing.Point(391, 7)
        Me.LabelControl1.LookAndFeel.SkinName = "Liquid Sky"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(183, 20)
        Me.LabelControl1.TabIndex = 111
        Me.LabelControl1.Text = "Red fields are mandatory."
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel2.Location = New System.Drawing.Point(2, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 32)
        Me.Panel2.TabIndex = 45
        '
        'TitleX
        '
        Me.TitleX.Appearance.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleX.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.TitleX.Location = New System.Drawing.Point(40, 4)
        Me.TitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TitleX.Name = "TitleX"
        Me.TitleX.Size = New System.Drawing.Size(45, 26)
        Me.TitleX.TabIndex = 5
        Me.TitleX.Text = "New"
        '
        'BUT_SAVE_COM
        '
        Me.BUT_SAVE_COM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_SAVE_COM.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_SAVE_COM.Appearance.Options.UseFont = True
        Me.BUT_SAVE_COM.Image = CType(resources.GetObject("BUT_SAVE_COM.Image"), System.Drawing.Image)
        Me.BUT_SAVE_COM.Location = New System.Drawing.Point(436, 188)
        Me.BUT_SAVE_COM.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_SAVE_COM.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_SAVE_COM.Name = "BUT_SAVE_COM"
        Me.BUT_SAVE_COM.Size = New System.Drawing.Size(70, 27)
        Me.BUT_SAVE_COM.TabIndex = 1
        Me.BUT_SAVE_COM.Text = "OK"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_CANCEL.Appearance.Options.UseFont = True
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(505, 188)
        Me.BUT_CANCEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_CANCEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 27)
        Me.BUT_CANCEL.TabIndex = 2
        Me.BUT_CANCEL.Text = "Cancel"
        '
        'BUT_DEL
        '
        Me.BUT_DEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_DEL.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BUT_DEL.Appearance.Options.UseFont = True
        Me.BUT_DEL.Image = CType(resources.GetObject("BUT_DEL.Image"), System.Drawing.Image)
        Me.BUT_DEL.Location = New System.Drawing.Point(436, 188)
        Me.BUT_DEL.LookAndFeel.SkinName = "Liquid Sky"
        Me.BUT_DEL.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BUT_DEL.Name = "BUT_DEL"
        Me.BUT_DEL.Size = New System.Drawing.Size(70, 27)
        Me.BUT_DEL.TabIndex = 8
        Me.BUT_DEL.Text = "Delete"
        Me.BUT_DEL.Visible = False
        '
        'BE_Refund_Amt
        '
        Me.BE_Refund_Amt.EditValue = ""
        Me.BE_Refund_Amt.Enabled = False
        Me.BE_Refund_Amt.EnterMoveNextControl = True
        Me.BE_Refund_Amt.Location = New System.Drawing.Point(475, 96)
        Me.BE_Refund_Amt.Name = "BE_Refund_Amt"
        Me.BE_Refund_Amt.Properties.AllowFocused = False
        Me.BE_Refund_Amt.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Refund_Amt.Properties.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Refund_Amt.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.BE_Refund_Amt.Properties.Appearance.Options.UseBackColor = True
        Me.BE_Refund_Amt.Properties.Appearance.Options.UseFont = True
        Me.BE_Refund_Amt.Properties.Appearance.Options.UseForeColor = True
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.Options.UseFont = True
        Me.BE_Refund_Amt.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.BE_Refund_Amt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.AliceBlue
        Me.BE_Refund_Amt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.BE_Refund_Amt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BE_Refund_Amt.Size = New System.Drawing.Size(99, 20)
        Me.BE_Refund_Amt.StyleController = Me.Layout_GS
        Me.BE_Refund_Amt.TabIndex = 184
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.BE_Refund_Amt
        Me.LayoutControlItem5.CustomizationFormText = "Refund:"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(404, 90)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(174, 30)
        Me.LayoutControlItem5.Text = "Refund:"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(60, 20)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'Frm_Voucher_Win_Gen_Pay_Adv
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 217)
        Me.Controls.Add(Me.Hyper_Request)
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
        Me.Name = "Frm_Voucher_Win_Gen_Pay_Adv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New..."
        CType(Me.Layout_GS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Layout_GS.ResumeLayout(False)
        CType(Me.BE_OS_Amt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Adjust_Amt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_ItemName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Other_Detail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Purpose.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Given_Date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Amount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BE_Adv_Amt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Layout_GS_Group, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        CType(Me.BE_Refund_Amt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BUT_SAVE_COM As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xID As System.Windows.Forms.Label
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TopLine As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Layout_GS As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Layout_GS_Group As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents Txt_Amount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BUT_DEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Hyper_Request As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BE_Given_Date As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_Other_Detail As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BE_Adv_Amt As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BE_Purpose As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_ItemName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_OS_Amt As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BE_Adjust_Amt As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BE_Refund_Amt As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
End Class
