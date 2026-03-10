Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports DevExpress.XtraPrinting

Public Class Frm_I_Transfer_Reg

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False

    Private RowFlag2 As Boolean
    Private ColumnFormVisibleFlag2 As Boolean = False

    Dim xID As String = ""

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Internal Transfer Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        Me.Txt_TitleX.Text = "Internal Transfers Register"
        '/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        '\................................../

        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

    Private Sub Form_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        Try
            SplitContainerControl1.SplitterPosition = (Me.Height - 82) / 2
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.GotFocus, But_Filter.GotFocus, But_Find.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.LostFocus, But_Filter.LostFocus, But_Find.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click, But_Filter.Click, But_Find.Click, BUT_PRINT.Click, BUT_ACCEPT.Click, T_Accept.Click, T_Unmatch.Click, T_New.Click, T_Edit.Click, T_Delete.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_ACCEPT" Then Me.DataNavigation("ACCEPT")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation_Volume("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation_Volume("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation_Volume("DELETE")
            If UCase(T_btn.Name) = "T_UNMATCH" Then Me.DataNavigation_Volume("UNMATCH TRANSFERS")
            If UCase(T_btn.Name) = "T_ACCEPT" Then Me.DataNavigation("ACCEPT")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_CLOSE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Grid Events"

    Private Sub Zoom1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zoom1.EditValueChanged
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", Zoom1.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub GridDefaultProperty()
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsBehavior.Editable = False

        Me.GridView1.OptionsDetail.AllowZoomDetail = True
        Me.GridView1.OptionsDetail.AutoZoomDetail = True

        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True

        Me.GridView1.OptionsSelection.InvertSelection = True

        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = False



        Me.GridView2.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView2.OptionsBehavior.Editable = False

        Me.GridView2.OptionsDetail.AllowZoomDetail = True
        Me.GridView2.OptionsDetail.AutoZoomDetail = True

        Me.GridView2.OptionsNavigation.EnterMoveNextColumn = True

        Me.GridView2.OptionsSelection.InvertSelection = True

        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowChildrenInGroupPanel = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = False
    End Sub

    Dim ActiveGrid As String = ""
    Private Sub GridView1_GotFocus(sender As Object, e As System.EventArgs) Handles GridView1.GotFocus
        ActiveGrid = GridView1.Name
        CreationDetail1(Me.GridView1.FocusedRowHandle)
    End Sub
    Private Sub GridView2_GotFocus(sender As Object, e As System.EventArgs) Handles GridView2.GotFocus
        ActiveGrid = GridView2.Name
        CreationDetail2(Me.GridView2.FocusedRowHandle)
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        Me.DataNavigation("EDIT")
    End Sub
    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Insert Then
            e.SuppressKeyPress = True
            Me.DataNavigation("NEW")
        End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("EDIT")
        End If
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True
            Me.DataNavigation("DELETE")
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation("VIEW")
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            Exit Sub
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If

        '  Me.Txt_Search.Text = Me.Txt_Search.Text & Chr(e.KeyCode)

    End Sub
    Private Sub GridView1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.ShowCustomizationForm
        Try
            Me.ColumnFormVisibleFlag1 = True
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridView1_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.HideCustomizationForm
        Try
            Me.ColumnFormVisibleFlag1 = False
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
            Me.GridControl1.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridControl1_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.buttonclick
        Select Case e.Button.Tag.ToString.Trim.ToUpper
            Case "OPEN_COL"
                If Me.ColumnFormVisibleFlag1 Then
                    Me.GridView1.DestroyCustomization()
                    e.Button.Hint = "Show Column Chooser"
                    e.Button.ImageIndex = 6
                Else
                    Me.GridView1.ColumnsCustomization()
                    e.Button.Hint = "Hide Column Chooser"
                    e.Button.ImageIndex = 10
                End If

            Case "GROUP_BOX"
                If Me.GridView1.OptionsView.ShowGroupPanel Then
                    Me.GridView1.OptionsView.ShowGroupPanel = False
                    e.Button.Hint = "Show Group Box"
                    e.Button.ImageIndex = 8
                Else
                    Me.GridView1.OptionsView.ShowGroupPanel = True
                    e.Button.Hint = "Hide Group Box"
                    e.Button.ImageIndex = 16
                End If
            Case "GROUPED_COL"
                If Me.GridView1.OptionsView.ShowGroupedColumns Then
                    Me.GridView1.OptionsView.ShowGroupedColumns = False
                    e.Button.Hint = "Show Group Column"
                    e.Button.ImageIndex = 14
                Else
                    Me.GridView1.OptionsView.ShowGroupedColumns = True
                    e.Button.Hint = "Hide Grouped Column"
                    e.Button.ImageIndex = 17
                End If
            Case "FOOTER_BAR"
                If Me.GridView1.OptionsView.ShowFooter Then
                    Me.GridView1.OptionsView.ShowFooter = False
                    e.Button.Hint = "Show Footer Bar"
                    e.Button.ImageIndex = 18
                Else
                    Me.GridView1.OptionsView.ShowFooter = True
                    e.Button.Hint = "Hide Footer Bar"
                    e.Button.ImageIndex = 13
                End If
            Case "GROUP_FOOTER"
                If Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
                    e.Button.Hint = "Show Group Footer Bar"
                Else
                    Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    e.Button.Hint = "Hide Group Footer Bar"
                End If
            Case "FIND_WINDOW"
                If Me.GridView1.IsFindPanelVisible Then
                    Me.GridView1.HideFindPanel()
                    e.Button.Hint = "Show Find Window"
                Else
                    Me.GridView1.ShowFindPanel()
                    e.Button.Hint = "Hide Find Window"
                End If
            Case "FILTER"
                Me.GridView1.ShowFilterEditor(Me.GridView1.FocusedColumn)
        End Select
    End Sub
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If Me.GridView1.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag1 Then
                    CreationDetail1(e.FocusedRowHandle)
                End If
            Else
                Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = ""
            End If
        Catch ex As Exception
            Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False : Lbl_Status.Text = ""
        End Try
    End Sub
    Private Sub CreationDetail1(ByVal Xrow As Integer)
        If Xrow >= 0 Then
            Me.Pic_Status.Visible = True : Dim Status As String = "" ': Me.Lbl_Seprator1.Visible = True
            Try
                Status = Me.GridView1.GetRowCellValue(Xrow, "Action Status").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Pic_Status.Image = My.Resources.lock ' Me.Lbl_Status.Text = "Completed" : : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Pic_Status.Image = My.Resources.unlock 'Me.Lbl_Status.Text = Status :  : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If
            Dim Add_Date As String = "" : Dim Add_By As String = "" : Dim Status_On As String = "" : Dim Status_By As String = "" : Dim StatusStr As String = "Completed"
            Try
                Add_Date = Me.GridView1.GetRowCellValue(Xrow, "Add Date").ToString
                Add_By = Me.GridView1.GetRowCellValue(Xrow, "Add By").ToString()
                Status_On = Me.GridView1.GetRowCellValue(Xrow, "Action Date").ToString
                Status_By = Me.GridView1.GetRowCellValue(Xrow, "Action By").ToString()
            Catch ex As Exception
            End Try
            If Status = "Locked" Then
                StatusStr = "Locked"
            Else
                StatusStr = "UnLocked"
            End If
            If IsDate(Status_On) Then
                Lbl_StatusOn.Text = StatusStr & " On: " & IIf(IsDBNull(Status_On), "", Status_On) : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            Else
                Lbl_StatusOn.Text = StatusStr & " On: " & "?" : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            End If

            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.GridView1.GetRowCellValue(Xrow, "Edit Date").ToString
                Edit_By = Me.GridView1.GetRowCellValue(Xrow, "Edit By").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
        End If
    End Sub
    Private Sub GridControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles GridControl1.MouseClick
        GridView2.FocusedRowHandle = -1
    End Sub


    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        Me.DataNavigation("EDIT")
    End Sub
    Private Sub GridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Insert Then
            e.SuppressKeyPress = True
            Me.DataNavigation("NEW")
        End If
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.DataNavigation("EDIT")
        End If
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True
            Me.DataNavigation("DELETE")
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation("VIEW")
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            Exit Sub
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If

        '  Me.Txt_Search.Text = Me.Txt_Search.Text & Chr(e.KeyCode)

    End Sub
    Private Sub GridView2_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.ShowCustomizationForm
        Try
            Me.ColumnFormVisibleFlag2 = True
            Me.GridControl2.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Hide Column Chooser"
            Me.GridControl2.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 10
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridView2_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.HideCustomizationForm
        Try
            Me.ColumnFormVisibleFlag2 = False
            Me.GridControl2.EmbeddedNavigator.Buttons.CustomButtons(0).Hint = "Show Column Chooser"
            Me.GridControl2.EmbeddedNavigator.Buttons.CustomButtons(0).ImageIndex = 6
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridControl2_Navigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.buttonclick
        Select Case e.Button.Tag.ToString.Trim.ToUpper
            Case "OPEN_COL"
                If Me.ColumnFormVisibleFlag2 Then
                    Me.GridView2.DestroyCustomization()
                    e.Button.Hint = "Show Column Chooser"
                    e.Button.ImageIndex = 6
                Else
                    Me.GridView2.ColumnsCustomization()
                    e.Button.Hint = "Hide Column Chooser"
                    e.Button.ImageIndex = 10
                End If

            Case "GROUP_BOX"
                If Me.GridView2.OptionsView.ShowGroupPanel Then
                    Me.GridView2.OptionsView.ShowGroupPanel = False
                    e.Button.Hint = "Show Group Box"
                    e.Button.ImageIndex = 8
                Else
                    Me.GridView2.OptionsView.ShowGroupPanel = True
                    e.Button.Hint = "Hide Group Box"
                    e.Button.ImageIndex = 16
                End If
            Case "GROUPED_COL"
                If Me.GridView2.OptionsView.ShowGroupedColumns Then
                    Me.GridView2.OptionsView.ShowGroupedColumns = False
                    e.Button.Hint = "Show Group Column"
                    e.Button.ImageIndex = 14
                Else
                    Me.GridView2.OptionsView.ShowGroupedColumns = True
                    e.Button.Hint = "Hide Grouped Column"
                    e.Button.ImageIndex = 17
                End If
            Case "FOOTER_BAR"
                If Me.GridView2.OptionsView.ShowFooter Then
                    Me.GridView2.OptionsView.ShowFooter = False
                    e.Button.Hint = "Show Footer Bar"
                    e.Button.ImageIndex = 18
                Else
                    Me.GridView2.OptionsView.ShowFooter = True
                    e.Button.Hint = "Hide Footer Bar"
                    e.Button.ImageIndex = 13
                End If
            Case "GROUP_FOOTER"
                If Me.GridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways Then
                    Me.GridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
                    e.Button.Hint = "Show Group Footer Bar"
                Else
                    Me.GridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    e.Button.Hint = "Hide Group Footer Bar"
                End If
            Case "FIND_WINDOW"
                If Me.GridView2.IsFindPanelVisible Then
                    Me.GridView2.HideFindPanel()
                    e.Button.Hint = "Show Find Window"
                Else
                    Me.GridView2.ShowFindPanel()
                    e.Button.Hint = "Hide Find Window"
                End If
            Case "FILTER"
                Me.GridView2.ShowFilterEditor(Me.GridView2.FocusedColumn)
        End Select
    End Sub
    Private Sub GridView2_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        Try
            If Me.GridView2.IsGroupRow(e.FocusedRowHandle) = False Then
                If RowFlag2 Then
                    CreationDetail2(e.FocusedRowHandle)
                End If
            Else
                Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False: Lbl_Status.Text = ""
            End If
        Catch ex As Exception
            Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False : Lbl_Status.Text = ""
        End Try
    End Sub
    Private Sub CreationDetail2(ByVal Xrow As Integer)
        If Xrow >= 0 Then
            Me.Pic_Status.Visible = True : Dim Status As String = "" ': Me.Lbl_Seprator1.Visible = True
            Try
                Status = Me.GridView2.GetRowCellValue(Xrow, "Action Status").ToString
            Catch ex As Exception
            End Try
            If Status.ToUpper.Trim.ToString = "LOCKED" Then
                Me.Pic_Status.Image = My.Resources.lock ' Me.Lbl_Status.Text = "Completed" : : Me.Lbl_Status.ForeColor = Color.Blue
            Else
                Me.Pic_Status.Image = My.Resources.unlock  ' Me.Lbl_Status.Text = Status : : If Status.ToUpper.Trim.ToString = "COMPLETED" Then Me.Lbl_Status.ForeColor = Color.DarkGreen Else Me.Lbl_Status.ForeColor = Color.Red
            End If
            Dim Add_Date As String = "" : Dim Add_By As String = "" : Dim Status_On As String = "" : Dim Status_By As String = "" : Dim StatusStr As String = "Completed"
            Try
                Add_Date = Me.GridView2.GetRowCellValue(Xrow, "Add Date").ToString
                Add_By = Me.GridView2.GetRowCellValue(Xrow, "Add By").ToString()
                Status_On = Me.GridView1.GetRowCellValue(Xrow, "Action Date").ToString
                Status_By = Me.GridView1.GetRowCellValue(Xrow, "Action By").ToString()
            Catch ex As Exception
            End Try

            If Status = "Locked" Then
                StatusStr = "Locked"
            Else
                StatusStr = "UnLocked"
            End If
            If IsDate(Status_On) Then
                Lbl_StatusOn.Text = StatusStr & " On: " & IIf(IsDBNull(Status_On), "", Status_On) : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            Else
                Lbl_StatusOn.Text = StatusStr & " On: " & "?" : Lbl_StatusBy.Text = StatusStr & " By: " & IIf(IsDBNull(Status_By), "", UCase(Trim(Status_By)))
            End If

            If IsDate(Add_Date) Then
                Lbl_Create.Text = "Add On: " & IIf(IsDBNull(Add_Date), "", Add_Date) & ", By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            Else
                Lbl_Create.Text = "Add On: " & "?, By: " & IIf(IsDBNull(Add_By), "", UCase(Trim(Add_By)))
            End If

            Dim Edit_Date As String = "" : Dim Edit_By As String = ""
            Try
                Edit_Date = Me.GridView2.GetRowCellValue(Xrow, "Edit Date").ToString
                Edit_By = Me.GridView2.GetRowCellValue(Xrow, "Edit By").ToString
            Catch ex As Exception
            End Try
            If IsDate(Edit_Date) Then
                Lbl_Modify.Text = "Edit On: " & IIf(IsDBNull(Edit_Date), "", Edit_Date) & ", By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            Else
                Lbl_Modify.Text = "Edit On: " & "?, By: " & IIf(IsDBNull(Edit_By), "", UCase(Trim(Edit_By)))
            End If
        End If
    End Sub
    Private Sub GridControl2_MouseClick(sender As Object, e As MouseEventArgs) Handles GridControl2.MouseClick
        GridView1.FocusedRowHandle = -1
    End Sub
    Private Sub ddlRecCount_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Pending_List()
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.Pic_Status.Visible = False : Lbl_Create.Text = "" : Lbl_Modify.Text = "" : Lbl_StatusOn.Text = "" : Lbl_StatusBy.Text = "" ': Lbl_Seprator1.Visible = False : Lbl_Status.Text = ""
        Me.CancelButton = Me.BUT_CLOSE
        GridDefaultProperty()
        Grid_Display()
        Pending_List()
        If Base._IsVolumeCenter Then
            T_New.Visible = True : T_Edit.Visible = True : T_Delete.Visible = True
            If Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Or Base._open_Cen_ID = 4216 Then
                T_Unmatch.Visible = True
            End If
        End If
        xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        xPleaseWait.Show("Internal Transfer Register" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag1 = False

        Dim P1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetList()
        If P1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = P1
        Me.GridView1.OptionsView.ShowFooter = True
        If Me.GridView1.Columns("Amount").Summary.Count = 0 Then Me.GridView1.Columns("Amount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:#,0.00}")

        Me.GridView1.Columns("Add By").Visible = False
        Me.GridView1.Columns("Add Date").Visible = False
        Me.GridView1.Columns("Edit By").Visible = False
        Me.GridView1.Columns("Edit Date").Visible = False
        Me.GridView1.Columns("Action Status").Visible = False
        Me.GridView1.Columns("Action By").Visible = False
        Me.GridView1.Columns("Action Date").Visible = False

        Me.GridView1.Columns("ID").Visible = False
        Me.GridView1.Columns("M.ID").Visible = False
        Me.GridView1.Columns("Description").Group()
        Me.GridView1.ExpandAllGroups()

        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        Me.GridView1.Columns("Centre Name").Width = 200

        Me.GridView1.Columns("No.").FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText

        If Me.GridView1.RowCount <= 0 Then
            RowFlag1 = False
        Else
            Try
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Centre Name")
                If xID.Length > 0 Then
                    Me.GridView1.FocusedRowHandle = Me.GridView1.LocateByValue("ID", xID)
                    Me.GridView1.SelectRow(Me.GridView1.FocusedRowHandle)
                Else
                    Me.GridView1.FocusedRowHandle = -1
                End If
            Catch ex As Exception
                Me.GridView1.FocusedColumn = Me.GridView1.Columns("Centre Name")
                Me.GridView1.FocusedRowHandle = GridView1.RowCount - 1
            End Try
            RowFlag1 = True
            Me.GridView1.Focus()
        End If
        xPleaseWait.Hide()
    End Sub

    Private Sub Pending_List()
        xPleaseWait.Show("Un-Matched Entries" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        RowFlag2 = False
        Dim RowCount As Int32 = 0
        Dim dSet As DataSet = Base._Internal_Tf_Voucher_DBOps.GetUnMatchedList(RowCount, Nothing)
        Dim P1 As DataTable = dSet.Tables(0)
        If P1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        If P1.Rows.Count Then
            Me.SplitContainerControl1.PanelVisibility = SplitPanelVisibility.Both
        Else
            Me.SplitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1
        End If
        Me.GridControl2.DataSource = P1
        Me.GridView2.OptionsView.ShowFooter = True
        If Me.GridView2.Columns("Amount").Summary.Count = 0 Then Me.GridView2.Columns("Amount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:#,0.00}")

        Me.GridView2.Columns("Add By").Visible = False
        Me.GridView2.Columns("Add Date").Visible = False
        Me.GridView2.Columns("Edit By").Visible = False
        Me.GridView2.Columns("Edit Date").Visible = False
        Me.GridView2.Columns("Action Status").Visible = False
        Me.GridView2.Columns("Action By").Visible = False
        Me.GridView2.Columns("Action Date").Visible = False

        Me.GridView2.Columns("ITEM_ID").Visible = False
        Me.GridView2.Columns("CEN_ID").Visible = False
        Me.GridView2.Columns("BI_ID").Visible = False
        Me.GridView2.Columns("PUR_ID").Visible = False
        Me.GridView2.Columns("ID").Visible = False
        Me.GridView2.Columns("M_ID").Visible = False

        Me.GridView2.Columns("REF_BI_ID").Visible = False
        Me.GridView2.Columns("Ref.Branch").Visible = False
        Me.GridView2.Columns("Ref.Others").Visible = False

        Me.GridView2.Columns("Description").Group()
        Me.GridView2.ExpandAllGroups()
        Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
        Me.GridView2.Columns("Centre Name").Width = 200

        If Me.GridView2.RowCount <= 0 Then
            RowFlag2 = False
        Else
            Try
                Me.GridView2.FocusedColumn = Me.GridView2.Columns("Centre Name")
                If xID.Length > 0 Then
                    Me.GridView2.FocusedRowHandle = Me.GridView2.LocateByValue("ID", xID)
                    Me.GridView2.SelectRow(Me.GridView2.FocusedRowHandle)
                Else
                    Me.GridView2.FocusedRowHandle = -1
                End If
            Catch ex As Exception
                Me.GridView2.FocusedColumn = Me.GridView2.Columns("Centre Name")
                Me.GridView2.FocusedRowHandle = GridView2.RowCount - 1
            End Try
            RowFlag2 = True
            Me.GridView2.Focus()
        End If
        xPleaseWait.Hide()

    End Sub

    Dim Closed_Bank_Account_No As String = ""
    Public Function Get_Closed_Bank_Status(ByVal xRecID As String) As Boolean
        Dim Flag As Boolean = False : Dim CR_LED_ID As String = "" : Dim DR_LED_ID As String = "" : Dim xTR_MODE As String = "" : Dim xTR_CODE As Integer = 0

        Dim d4 As DataTable = Base._Voucher_DBOps.GetTransactionDetail(xRecID)
        If d4.Rows.Count > 0 Then
            If Not IsDBNull(d4.Rows(0)("TR_SUB_CR_LED_ID")) Then CR_LED_ID = d4.Rows(0)("TR_SUB_CR_LED_ID") Else CR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_SUB_DR_LED_ID")) Then DR_LED_ID = d4.Rows(0)("TR_SUB_DR_LED_ID") Else DR_LED_ID = ""
            If Not IsDBNull(d4.Rows(0)("TR_CODE")) Then xTR_CODE = d4.Rows(0)("TR_CODE") Else xTR_CODE = 0
            If Not IsDBNull(d4.Rows(0)("TR_MODE")) Then xTR_MODE = d4.Rows(0)("TR_MODE") Else xTR_MODE = ""
        End If
        If xTR_CODE = 6 Or xTR_CODE = 1 Or UCase(xTR_MODE) <> "CASH" Then 'count(rec_id) 'Bug #4728 fix
            Dim MaxValue As Object = Nothing
            MaxValue = Base._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID)
            If IsDBNull(MaxValue) Then
                Flag = False : Closed_Bank_Account_No = ""
            Else
                Flag = True : Closed_Bank_Account_No = MaxValue
            End If
        End If
        Return Flag
    End Function

    Public Sub DataNavigation_Volume(ByVal Action As String)
        If Base._IsVolumeCenter Then
            '----------------------------------------
            If Base.AllowMultiuser() Then
                If Action = "UNMATCH TRANSFERS" Then
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                        If Not xTemp_ID.ToString.ToLower = "opening balance" And Not xTemp_ID.ToString.ToLower = "note-book" Then
                            Dim RecEdit_Date As Object = Base._Voucher_DBOps.GetEditOnByRecID(xTemp_ID)
                            If IsDBNull(RecEdit_Date) Or RecEdit_Date Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Voucher"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                                Grid_Display()
                                Exit Sub
                            End If
                            If RecEdit_Date <> Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date") Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Voucher"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                                Grid_Display()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
            '----------------------------------------
            Select Case Action
                Case "NEW"
                    Dim zfrm As New Frm_Voucher_Win_I_Transfer : zfrm.MainBase = Base
                    zfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    zfrm.ShowDialog(Me)
                    'If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    '    xID = zfrm.xID_1.Text : xMID = zfrm.xMID.Text : Flag = True : xEntryDate = zfrm.Txt_V_Date.DateTime
                    'End If
                    If Base._IsVolumeCenter And zfrm.DialogResult = Windows.Forms.DialogResult.OK And zfrm.iTrans_Type = "CREDIT" Then
                        Dim xfrm1 As New Frm_Receipt_Options()
                        xfrm1.chosen_Voucher = Frm_Receipt_Options.Voucher_Choice.Internal_Transfer
                        xfrm1.Selected_Bank_ID = zfrm.Ref_Bank_ID
                        xfrm1.Tr_Date = zfrm.Txt_V_Date.DateTime : xfrm1.Mode = zfrm.Cmd_Mode.Text : xfrm1.Selected_Bank_ID = zfrm.GLookUp_BankList.Tag
                        xfrm1.Selected_ItemID = zfrm.GLookUp_ItemList.Tag : xfrm1.Selected_Trans_Type = zfrm.iTrans_Type
                        : xfrm1.Selected_Purpose_ID = zfrm.GLookUp_PurList.EditValue ': xfrm1.Selected_Deposit_Slip = Val(zfrm.Txt_Slip_No.Text)
                        xfrm1.ShowDialog()
                    End If
                    If Not zfrm Is Nothing Then zfrm.Dispose()
                Case "EDIT"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                        Dim xTemp_MID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "M.ID").ToString()
                        Dim xMaster_ID As String = "" : If xTemp_MID.Length > 0 Then xMaster_ID = xTemp_MID Else xMaster_ID = xTemp_ID
                        Dim isRecChanged As Boolean = False '#5518 fix
                        Dim Flag As Boolean = False : Dim xEntryDate As Date = Nothing
                        If xTemp_ID.Length > 0 Then
                            Dim Entry_Found As Boolean = False
                            Dim xRec_Status As String = "" : Dim xTR_CODE As String = "" : Dim xTemp_D_Status As String = "" : Dim xCross_Ref_Id As String = ""
                            Dim multiUserMsg As String = ""
                            Dim AllowUser As Boolean = False
                            Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID)
                            If Status Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            If Status.Rows.Count > 0 Then ' checks for record existence here 
                                Entry_Found = True
                                xRec_Status = Status.Rows(0)("REC_STATUS")
                                For Each cRow As DataRow In Status.Rows
                                    If Not IsDBNull(cRow("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = cRow("TR_TRF_CROSS_REF_ID")
                                    If xCross_Ref_Id.Length > 0 Then Exit For
                                Next
                                'Status check for multiusers
                                Dim xStatus As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Action Status").ToString()
                                Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                                If value <> xRec_Status Then
                                    If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                        multiUserMsg = vbNewLine & vbNewLine & "The Record has been locked in the background by another user."
                                    ElseIf xRec_Status = Common_Lib.Common.Record_Status._Completed Then
                                        multiUserMsg = vbNewLine & vbNewLine & "T h e   R e c o r d   h a s   b e e n   u n l o c k e d   i n   t h e   b a c k g r o u n d   b y   a n o t h e r   u s e r."
                                        AllowUser = True
                                    Else
                                        multiUserMsg = vbNewLine & vbNewLine & "Record Status has been changed in the background by another user"
                                        AllowUser = True
                                    End If
                                    If AllowUser Then
                                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                                        If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", multiUserMsg & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                            'xPromptWindow.Dispose()
                                        Else
                                            'xPromptWindow.Dispose()
                                            Grid_Display()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                            'takes action if there is no record 
                            If Entry_Found = False Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   i n   b a c k g r o u n d  . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Grid_Display() : Exit Sub
                            End If


                            If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & multiUserMsg & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Exit Sub
                            End If

                            If Get_Closed_Bank_Status(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & Closed_Bank_Account_No & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If

                            Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                            If Not d1 Is Nothing Then
                                If d1.Rows.Count > 0 Then
                                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date") <> d1.Rows(0)("REC_EDIT_ON") Then
                                        isRecChanged = True
                                    End If
                                    If Not IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                        If d1.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                            multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                            DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   I n t e r n a l   T r a n s f e r   c a n n o t   b e   E d i t e d . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            If isRecChanged Then Grid_Display()
                                            Exit Sub
                                        End If
                                    Else
                                        If isRecChanged Then '#5518 fix
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Grid_Display()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                            If Base._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Internal_Transfer) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit slip has already been printed for current transaction!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Dim xfrm As New Frm_Voucher_Win_I_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit : xfrm.xID_1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                            If xRec_Status = Common_Lib.Common.Record_Status._Completed Then xfrm.Chk_Incompleted.Checked = False Else xfrm.Chk_Incompleted.Checked = True
                            '-----------------------------+
                            'Start : Edit date sent to Check if entry already changed 
                            '-----------------------------+
                            xfrm.Info_LastEditedOn = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date")
                            '-----------------------------+
                            'End : Edit date sent to Check if entry already changed 
                            '-----------------------------+
                            xfrm.ShowDialog(Me)
                            If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then
                                xID = xfrm.xID_1.Text
                                Grid_Display() 'standard function to refresh grid everywhere
                            End If

                            ': xMID = xfrm.xMID.Text : Flag = True : xEntryDate = xfrm.Txt_V_Date.DateTime
                            If xfrm.DialogResult = Windows.Forms.DialogResult.Cancel And isRecChanged Then Grid_Display() '#5518 fix
                            If Not xfrm Is Nothing Then xfrm.Dispose()
                        End If
                    Else
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        DevExpress.XtraEditors.XtraMessageBox.Show("P o s t e d   I n t e r n a l   T r a n s f e r   E n t r y   N o t   S e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Case "DELETE"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                        Dim xTemp_MID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "M.ID").ToString()
                        Dim xMaster_ID As String = "" : If xTemp_MID.Length > 0 Then xMaster_ID = xTemp_MID Else xMaster_ID = xTemp_ID
                        Dim isRecChanged As Boolean = False '#5518 fix

                        If xTemp_ID.Length > 0 Then
                            ' 
                            Dim Entry_Found As Boolean = False
                            Dim xRec_Status As String = "" : Dim xTR_CODE As String = "" : Dim xTemp_D_Status As String = "" : Dim xCross_Ref_Id As String = ""
                            Dim multiUserMsg As String = ""
                            Dim AllowUser As Boolean = False
                            Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID)
                            If Status Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            If Status.Rows.Count > 0 Then
                                Entry_Found = True
                                xRec_Status = Status.Rows(0)("REC_STATUS")
                                xTR_CODE = Status.Rows(0)("TR_CODE")
                                For Each cRow As DataRow In Status.Rows
                                    If Not IsDBNull(cRow("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = cRow("TR_TRF_CROSS_REF_ID")
                                    If xCross_Ref_Id.Length > 0 Then Exit For
                                Next

                                'Status check for multiusers
                                Dim xStatus As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Action Status").ToString()
                                Dim value = [Enum].Parse(GetType(Common_Lib.Common.Record_Status), "_" + xStatus)
                                If value <> xRec_Status Then
                                    If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                        multiUserMsg = vbNewLine & vbNewLine & "The Record has been locked in the background by another user."
                                    ElseIf xRec_Status = Common_Lib.Common.Record_Status._Completed Then
                                        multiUserMsg = vbNewLine & vbNewLine & "T h e   R e c o r d   h a s   b e e n   u n l o c k e d   i n   t h e   b a c k g r o u n d   b y   a n o t h e r   u s e r."
                                        AllowUser = True
                                    Else
                                        multiUserMsg = vbNewLine & vbNewLine & "Record Status has been changed in the background by another user"
                                        AllowUser = True
                                    End If
                                    If AllowUser Then
                                        Dim xPromptWindow As New Common_Lib.Prompt_Window
                                        If DialogResult.Yes = xPromptWindow.ShowDialog("Confirmation...", multiUserMsg & vbNewLine & vbNewLine & "Do you want to continue...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                                            'xPromptWindow.Dispose()
                                        Else
                                            ' xPromptWindow.Dispose()
                                            Grid_Display()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If

                            If Entry_Found = False Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d  /  C h a n g e d   i n   b a c k g r o u n d  . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Grid_Display() : Exit Sub
                            End If

                            If xRec_Status = Common_Lib.Common.Record_Status._Locked Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d /  D e l e t e d . . . !" & multiUserMsg & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Exit Sub
                            End If

                            If Get_Closed_Bank_Status(xTemp_ID) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & Closed_Bank_Account_No & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Exit Sub
                            End If

                            Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                            If Not d1 Is Nothing Then
                                If d1.Rows.Count > 0 Then
                                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date") <> d1.Rows(0)("REC_EDIT_ON") Then
                                        isRecChanged = True
                                    End If
                                    If Not IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                        If d1.Rows(0)("TR_TRF_CROSS_REF_ID").Length > 0 Then
                                            multiUserMsg = vbNewLine & vbNewLine & "Record has already been matched in the background"
                                            DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   I n t e r n a l   T r a n s f e r   c a n n o t   b e   D e l e t e d . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            If isRecChanged Then Grid_Display()
                                            Exit Sub
                                        End If
                                    Else
                                        If isRecChanged Then '#5518 fix
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Record has already been unmatched in the background", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Grid_Display()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If

                            If Base._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Internal_Transfer) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit slip has already been printed for current transaction!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            Dim xfrm As New Frm_Voucher_Win_I_Transfer : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._Delete : xfrm.xID_1.Text = xTemp_ID : xfrm.xMID.Text = xMaster_ID
                            '-----------------------------+
                            'Start : Edit date sent to Check if entry already changed 
                            '-----------------------------+
                            xfrm.Info_LastEditedOn = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date")
                            '-----------------------------+
                            'End : Edit date sent to Check if entry already changed 
                            '-----------------------------+
                            xfrm.ShowDialog(Me) : If xfrm.DialogResult = DialogResult.OK Or xfrm.DialogResult = DialogResult.Retry Then xID = xfrm.xID_1.Text : Grid_Display()
                            If xfrm.DialogResult = DialogResult.Cancel And isRecChanged Then Grid_Display()
                            If Not xfrm Is Nothing Then xfrm.Dispose()
                        End If
                    Else
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        DevExpress.XtraEditors.XtraMessageBox.Show("P o s t e d   I n t e r n a l   T r a n s f e r   E n t r y   N o t   S e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                Case "UNMATCH TRANSFERS"
                    If Base.CheckActionRights(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Common_Lib.Common.ClientAction.Lock_Unlock) Or Base._open_Cen_ID = 4216 Then
                        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                            Dim xTemp_ID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ID").ToString()
                            Dim xTemp_MID As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "M.ID").ToString()
                            Dim xTr_Date As String = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Date").ToString()

                            'check status of original rec
                            Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(xTemp_ID)
                            If Status Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            If Status.Rows.Count > 0 Then
                                If Common_Lib.Common.Record_Status._Locked = Status.Rows(0)("REC_STATUS") Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   U n m a t c h e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    : Exit Sub
                                End If
                            Else
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Grid_Display() : Exit Sub
                            End If
                            Dim multiUserMsg As String = "" : Dim isRecChanged As Boolean = False
                            Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1)
                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Edit Date") <> d1.Rows(0)("REC_EDIT_ON") Then
                                isRecChanged = True
                            End If
                            If IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Or d1.Rows(0)("TR_TRF_CROSS_REF_ID") Is Nothing Then
                                multiUserMsg = vbNewLine & vbNewLine & "Record has already been unmatched in the background"
                                DevExpress.XtraEditors.XtraMessageBox.Show("S e l e c t e d   r e c o r d   i s   a l r e a d y   u n m a t c h e d  . . . !" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                : Exit Sub
                                If isRecChanged Then Grid_Display() '#5518 fix
                                Exit Sub
                            Else
                                If isRecChanged Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Transfer Voucher matched in the background...!" & multiUserMsg, "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Grid_Display()
                                    Exit Sub
                                End If
                            End If

                            'check status of matched rec
                            Status = Base._Voucher_DBOps.GetStatus_TrCode_OtherCentre(d1.Rows(0)("TR_TRF_CROSS_REF_ID"))
                            If Status Is Nothing Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            If Status.Rows.Count > 0 Then
                                If Common_Lib.Common.Record_Status._Locked = Status.Rows(0)("REC_STATUS") Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   M a t c h e d   w i t h   t h i s   r e c o r d   i s   a l r e a d y   l o c k e d  . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock that Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    : Exit Sub
                                End If
                            Else
                                DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Grid_Display() : Exit Sub
                            End If

                            If IsDBNull(d1.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("T r a n s f e r   V o u c h e r   a l r e a d y   u n m a t c h e d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                                Exit Sub
                            End If
                            'Unmatch here
                            If Base._Internal_Tf_Voucher_DBOps.UnMatchTransfers(xTemp_ID, d1.Rows(0)("TR_TRF_CROSS_REF_ID"), xTr_Date) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("T r a n s f e r   E n t r y   u n m a t c h e d   s u c c e s s f u l l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Grid_Display()
                            Else
                                DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y !   T r a n s f e r   E n t r y   C o u l d   n o t   b e   u n m a t c h e d   s u c c e s s f u l l y . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        Else
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r n a l   T r a n s f e r   E n t r y   N o t   S e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If
            End Select
        End If
    End Sub

    Public Sub DataNavigation(ByVal Action As String)

        Select Case Action
            Case "ACCEPT"
                '  If GridControl2.IsFocused Then
                If Val(Me.GridView2.FocusedRowHandle) >= 0 Then
                    Dim xFrm As Frm_Voucher_Win_I_Transfer = New Frm_Voucher_Win_I_Transfer
                    xFrm._Date = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Date").ToString()
                    xFrm._a_Item_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "ITEM_ID").ToString()
                    xFrm._Mode = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Mode").ToString()
                    xFrm._CEN_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "CEN_ID").ToString()
                    xFrm._BI_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "BI_ID").ToString()
                    xFrm._BI_BRANCH = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Branch Name").ToString()
                    xFrm._BI_ACC_NO = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Bank A/c. No.").ToString()

                    xFrm._REF_BI_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "REF_BI_ID").ToString()
                    xFrm._REF_BRANCH = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Ref.Branch").ToString()
                    xFrm._REF_OTHERS = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Ref.Others").ToString()

                    xFrm._BI_REF_NO = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Ref.No.").ToString()
                    xFrm._BI_REF_DT = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Ref.Date").ToString()
                    xFrm._Amount = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Amount").ToString()
                    xFrm._a_PUR_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "PUR_ID").ToString()
                    xFrm.CROSS_REF_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "ID").ToString()
                    xFrm.CROSS_M_ID = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "M_ID").ToString()
                    xFrm.FR_REC_EDIT_ON = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Edit Date").ToString()
                    xFrm._REF_BANK_ACC_NO = Me.GridView2.GetRowCellValue(Val(Me.GridView2.FocusedRowHandle), "Ref_Bank_AccNo").ToString()  'used in DD credit case. #bug 5539 fix
                    xFrm._Accepted_From_Register = True : xFrm.MainBase = Base : xFrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    xFrm.ShowDialog(Me)
                    If xFrm.DialogResult = Windows.Forms.DialogResult.OK Then Pending_List()
                    If Not xFrm Is Nothing Then xFrm.Dispose()
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    DevExpress.XtraEditors.XtraMessageBox.Show("I n t e r n a l   T r a n s f e r   E n t r y   N o t   S e l e c t e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                ' End If
            Case "UNMATCH"
            Case "REFRESH"
                Grid_Display()
                Pending_List()
            Case "PRINT-LIST"
                If ActiveGrid = GridView1.Name Then
                    If Me.GridView1.RowCount > 0 Then Base.Show_ListPreview(GridControl1, Me.Text & " - All Entries", Me, True, Printing.PaperKind.A4, Me.Text & " - All Entries", "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                    Me.GridView1.Focus()
                ElseIf ActiveGrid = GridView2.Name Then
                    If Me.GridView2.RowCount > 0 Then Base.Show_ListPreview(GridControl2, Me.Text & " - Pending Entries to be accepted by us", Me, True, Printing.PaperKind.A4, "Int.Transfer - Pending Entries to be accepted by us", "UID: " & Base._open_UID_No, "Year: " & Base._open_Year_Name, True)
                    Me.GridView2.Focus()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("F i r s t   s  e l e c t   a n y   e n t r y . . . !", "List Preview...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "FILTER"
                If Me.GridView1.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = My.Resources.bluesearch : Me.GridView1.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = My.Resources.blueaccept : Me.GridView1.OptionsView.ShowAutoFilterRow = True
                End If
                If Me.GridView2.OptionsView.ShowAutoFilterRow Then
                    Me.But_Filter.Image = My.Resources.bluesearch : Me.GridView2.OptionsView.ShowAutoFilterRow = False
                Else
                    Me.But_Filter.Image = My.Resources.blueaccept : Me.GridView2.OptionsView.ShowAutoFilterRow = True
                End If
            Case "FIND"
                If Me.GridView1.IsFindPanelVisible Then
                    Me.But_Find.Image = My.Resources.greensearch : Me.GridView1.HideFindPanel()
                Else
                    Me.But_Find.Image = My.Resources.greenaccept : Me.GridView1.ShowFindPanel()
                End If
                If Me.GridView2.IsFindPanelVisible Then
                    Me.But_Find.Image = My.Resources.greensearch : Me.GridView2.HideFindPanel()
                Else
                    Me.But_Find.Image = My.Resources.greenaccept : Me.GridView2.ShowFindPanel()
                End If
            Case "CLOSE"
                Me.Close()
        End Select

    End Sub

#End Region

End Class
