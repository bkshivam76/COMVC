<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_View_Summary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_View_Summary))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SubTitleX = New DevExpress.XtraEditors.LabelControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_TitleX = New DevExpress.XtraEditors.LabelControl()
        Me.BUT_CANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1.SuspendLayout()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.SubTitleX)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Txt_TitleX)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(704, 34)
        Me.Panel1.TabIndex = 55
        '
        'SubTitleX
        '
        Me.SubTitleX.Appearance.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTitleX.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.SubTitleX.Appearance.Image = CType(resources.GetObject("SubTitleX.Appearance.Image"), System.Drawing.Image)
        Me.SubTitleX.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SubTitleX.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SubTitleX.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SubTitleX.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.SubTitleX.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.SubTitleX.LineVisible = True
        Me.SubTitleX.Location = New System.Drawing.Point(385, 2)
        Me.SubTitleX.LookAndFeel.UseDefaultLookAndFeel = False
        Me.SubTitleX.Name = "SubTitleX"
        Me.SubTitleX.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.SubTitleX.Size = New System.Drawing.Size(314, 30)
        Me.SubTitleX.TabIndex = 118
        Me.SubTitleX.Text = "Period  Fr.: 01-Apr, 2011  to 31-Mar,2012"
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
        Me.Txt_TitleX.Size = New System.Drawing.Size(212, 26)
        Me.Txt_TitleX.TabIndex = 5
        Me.Txt_TitleX.Text = "Cash Bank Summary"
        '
        'BUT_CANCEL
        '
        Me.BUT_CANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BUT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BUT_CANCEL.Image = CType(resources.GetObject("BUT_CANCEL.Image"), System.Drawing.Image)
        Me.BUT_CANCEL.Location = New System.Drawing.Point(633, 283)
        Me.BUT_CANCEL.Name = "BUT_CANCEL"
        Me.BUT_CANCEL.Size = New System.Drawing.Size(70, 28)
        Me.BUT_CANCEL.TabIndex = 1
        Me.BUT_CANCEL.Text = "Close"
        '
        'GridControl2
        '
        Me.GridControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControl2.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.GridControl2.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Red
        Me.GridControl2.EmbeddedNavigator.Appearance.Options.UseBackColor = True
        Me.GridControl2.EmbeddedNavigator.Appearance.Options.UseForeColor = True
        Me.GridControl2.EmbeddedNavigator.Buttons.Append.Tag = "Append"
        Me.GridControl2.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.CancelEdit.Tag = "CancelEdit"
        Me.GridControl2.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.GridControl2.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.GridControl2.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.GridControl2.EmbeddedNavigator.Buttons.First.Tag = "First"
        Me.GridControl2.EmbeddedNavigator.Buttons.Last.Tag = "Last"
        Me.GridControl2.EmbeddedNavigator.Buttons.Next.Tag = "Next"
        Me.GridControl2.EmbeddedNavigator.Buttons.NextPage.Tag = "NextPage"
        Me.GridControl2.EmbeddedNavigator.Buttons.Prev.Tag = "Prev"
        Me.GridControl2.EmbeddedNavigator.Buttons.PrevPage.Tag = "PrevPage"
        Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.GridControl2.EmbeddedNavigator.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridControl2.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
        Me.GridControl2.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridControl2.Location = New System.Drawing.Point(0, 34)
        Me.GridControl2.LookAndFeel.SkinName = "Liquid Sky"
        Me.GridControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.Size = New System.Drawing.Size(704, 248)
        Me.GridControl2.TabIndex = 0
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FooterPanel.ForeColor = System.Drawing.Color.MediumBlue
        Me.GridView2.Appearance.FooterPanel.Options.UseFont = True
        Me.GridView2.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.GroupFooter.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        StyleFormatCondition1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        StyleFormatCondition1.Appearance.Options.UseFont = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
        StyleFormatCondition1.Expression = "[Description]='Closing Balance'"
        Me.GridView2.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.GroupFormat = "{1}"
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView2.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView2.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsCustomization.AllowColumnMoving = False
        Me.GridView2.OptionsCustomization.AllowFilter = False
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsCustomization.AllowSort = False
        Me.GridView2.OptionsDetail.AutoZoomDetail = True
        Me.GridView2.OptionsDetail.SmartDetailHeight = True
        Me.GridView2.OptionsFind.AllowFindPanel = False
        Me.GridView2.OptionsMenu.EnableColumnMenu = False
        Me.GridView2.OptionsMenu.EnableFooterMenu = False
        Me.GridView2.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView2.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GridView2.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GridView2.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GridView2.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView2.OptionsPrint.PrintDetails = True
        Me.GridView2.OptionsSelection.InvertSelection = True
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowIndicator = False
        Me.GridView2.ViewCaption = "Group"
        '
        'Frm_View_Summary
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 312)
        Me.Controls.Add(Me.GridControl2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BUT_CANCEL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_View_Summary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cash Bank Summary"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BUT_CANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Txt_TitleX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SubTitleX As DevExpress.XtraEditors.LabelControl

End Class
