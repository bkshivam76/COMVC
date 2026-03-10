Imports System.Data.OleDb
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraBars
Imports System.IO
Module Global_Set

    'Me.Text = (1234567890.55).ToString("N", New System.Globalization.CultureInfo("en-IN"))
    '        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-IN")
    '        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-IN")
    Public Base As New Common_Lib.Common
    '  Public xPleaseWait As New Common_Lib.PleaseWait
    Public OpenCashBook As Boolean = False

    Public Sub Programming_Testing()
        '   Programming_Mode()
    End Sub

    Public Sub Programming_Mode()
        Base.Allow_Membership = True
        Base._IsVolumeCenter = True
        Base._open_PAD_No_Main = "711"
        Base._open_User_ID = Base._open_PAD_No_Main
        Base._open_Cen_ID = "00706"
        Base._open_Cen_ID_Main = "00706"
        Base._open_Ins_ID = "00001"
        Base._open_Cen_ID_Child = "'00706'"
        Base._open_Cen_Rec_ID = "b7d8e80e-ed49-4d12-8b6e-4481b0861e42"
        Base._open_Cen_Name = "Abiohar"
        Base._open_Year_Acc_Type = "General"

        'Base._open_PAD_No_Main = "208"
        'Base._open_User_ID = Base._open_PAD_No_Main
        'Base._open_Cen_ID = "03093"
        'Base._open_Cen_ID_Main = "00207"
        'Base._open_Ins_ID = "00002"
        'Base._open_Cen_ID_Child = "'03093'"
        'Base._open_Cen_Rec_ID = "24ae3400-f552-457d-86c3-e02e600c7bef"
        'Base._open_Cen_Name = "Maninagar"
        'Base._open_Year_Acc_Type = "GENERAL"


        'Base._open_PAD_No_Main = "9000"
        'Base._open_User_ID = "4219"
        'Base._open_Cen_ID = "04219"
        'Base._open_Cen_ID_Main = "04216"
        'Base._open_Ins_ID = "00003"
        'Base._open_Cen_ID_Child = "'04219'"
        'Base._open_Cen_Rec_ID = "e7eb47fc-8ea8-41cc-b7aa-e6320981ac42"
        'Base._open_Cen_Name = "ABU Membership"
        'Base._open_Year_Acc_Type = "MEMBERSHIP"

        'Base._open_PAD_No_Main = "207"
        'Base._open_User_ID = Base._open_PAD_No_Main
        'Base._open_Cen_ID = "00206"
        'Base._open_Cen_ID_Main = "00206"
        'Base._open_Ins_ID = "00001"
        'Base._open_Cen_ID_Child = "'00206'"
        'Base._open_Cen_Rec_ID = "ccc5768a-fdf0-4063-a816-654ce78896c0"
        'Base._open_Cen_Name = "Mahadev Nagar"
        'Base._open_Year_Acc_Type = "GENERAL"


        Base._open_Year_ID = "1213" : Base._open_Year_Name = "2012 - 2013"
        Base._open_Year_Sdt = #4/1/2012# : Base._open_Year_Edt = #3/31/2013#
        Base._Current_Version = "4.7.0.0"
        Base._open_Ins_Name = "BK"
        'Base._open_User_Type = "Auditor"

        Base._Sync_Last_DateTime = Now
        Base.Get_Configure_Setting()
        Dim PER_DB As DataTable = Base._ClientUserDBOps.GetCenterTasks()
        If PER_DB Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim DV1 As New DataView(PER_DB) : DV1.Sort = "TASK_NAME"
        If DV1.Count > 0 Then
            Dim index As Integer = DV1.Find("FOREIGN DONATION")
            ' Me.Text = DV1(index)("PERMISSION").ToString()
            If index >= 0 Then If InStr(DV1(index)("PERMISSION").ToString(), "F") <> 0 Then Base.Allow_Foreign_Donation = True Else Base.Allow_Foreign_Donation = False
            index = DV1.Find("COLLECTION BOX - BANK")
            If index >= 0 Then If InStr(DV1(index)("PERMISSION").ToString(), "F") <> 0 Then Base.Allow_Bank_In_C_Box = True Else Base.Allow_Bank_In_C_Box = False
            index = DV1.Find("H.Q. CENTRE")
            If index >= 0 Then If InStr(DV1(index)("PERMISSION").ToString(), "F") <> 0 Then Base.Is_HQ_Centre = True Else Base.Is_HQ_Centre = False
            index = DV1.Find("MEMBERSHIP")
            If index >= 0 Then If InStr(DV1(index)("PERMISSION").ToString(), "F") <> 0 Then Base.Allow_Membership = True Else Base.Allow_Membership = False
            index = DV1.Find("PRINT STATEMENTS")
            If index >= 0 Then If InStr(DV1(index)("PERMISSION").ToString(), "F") <> 0 Then Base.Allow_Statements_Without_Restrictions = True Else Base.Allow_Statements_Without_Restrictions = False
        Else
            Base.Allow_Foreign_Donation = False : Base.Allow_Bank_In_C_Box = False : Base.Is_HQ_Centre = False : Base.Allow_Membership = False
        End If
        MsgBox("Running in Programming Mode...!!!", MsgBoxStyle.Information, "Message for Developer...")
    End Sub

    Public Sub HighlightRemarks(ByVal GridView1 As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim condition As StyleFormatCondition = New StyleFormatCondition()
        condition.Appearance.BackColor = Color.Pink
        condition.Appearance.Options.UseBackColor = True
        condition.Condition = FormatConditionEnum.Expression
        condition.Expression = "IsNullOrEmpty(iCross_Ref_ID) AND (iTR_CODE = 8 OR iTR_CODE = 15)"
        GridView1.FormatConditions.Add(condition)
    End Sub

    Public Sub View_Actions(ByVal RecID As String, ByVal Table As Common_Lib.RealTimeService.Tables, ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal status As String, ByVal xWinFrom As System.Windows.Forms.Form)
        'Dim xfrm As New D0008.Frm_Action_Items_Info : xfrm.MainBase = Base
        'xfrm.RefScreen = Screen.ToString.ToUpper : xfrm.RefTable = Table.ToString.ToUpper : xfrm.RefRecID = RecID : xfrm.Status = status
        'xfrm.ShowDialog(xWinFrom)
        'xfrm.Dispose()
    End Sub

    Public Sub Add_Actions(ByVal RecID As String, ByVal Table As Common_Lib.RealTimeService.Tables, ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal xWinFrom As System.Windows.Forms.Form)
        'If Base.CheckActionRights(Screen, Common_Lib.Common.ClientAction.Add_Edit_Action) Then
        '    Dim xfrm As New D0008.Frm_Action_Items_Window : xfrm.MainBase = Base
        '    xfrm.Text = "New ~ Action" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
        '    xfrm._Ref_Screen = Screen.ToString.ToUpper : xfrm._Ref_Table = Table.ToString.ToUpper : xfrm._Ref_Rec_ID = RecID
        '    xfrm.ShowDialog(xWinFrom)
        '    xfrm.Dispose()
        'End If
    End Sub

    Public Sub Drop_Request(ByVal xSetTitle As String, ByVal xSendFrom As String, ByVal xWinFrom As System.Windows.Forms.Form)
        'Dim xfrm As New D0008.Frm_Request_Window : xfrm.MainBase = Base : xfrm.Owner = xWinFrom
        'xfrm.Text = xSetTitle : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm._Send_From = xSendFrom
        'xfrm.ShowDialog(xWinFrom)
        'xfrm.Dispose()
    End Sub
    Public Sub Add_Attachment(ByVal xSetTitle As String, ByVal xSendFrom As String, ByVal xWinFrom As System.Windows.Forms.Form, ID As String)
        'Dim xfrm As New D0008.Frm_Attachment_Window : xfrm.MainBase = Base
        'xfrm.Text = xSetTitle
        'xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
        'xfrm._Ref_Screen = xSendFrom
        'xfrm._Ref_Rec_id = ID
        'xfrm.ShowDialog(xWinFrom)
        'xfrm.Dispose()
    End Sub
    Public Sub Manage_Attachment(ByVal xSetTitle As String, ByVal xSendFrom As String, ByVal xWinFrom As System.Windows.Forms.Form, ID As String)
        'Dim xfrm As New D0008.Frm_Attachment_Info : xfrm.MainBase = Base
        'xfrm.Text = xSetTitle
        'xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
        'xfrm._Ref_Screen = xSendFrom
        'xfrm._Ref_Rec_id = ID
        'xfrm.ShowDialog(xWinFrom)
        'xfrm.Dispose()
    End Sub

    Public Sub Link_Attachment(ByVal xSetTitle As String, ByVal xSendFrom As String, ByVal xWinFrom As System.Windows.Forms.Form, Ref_ID As String)
        'Dim xfrm As New D0008.Frm_Select_Attachment : xfrm.MainBase = Base
        'xfrm.Text = xSetTitle
        'xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
        'xfrm.Referece_Screen = xSendFrom
        'xfrm.Referece_ID = Ref_ID
        'xfrm.ShowDialog(xWinFrom)
        'xfrm.Dispose()
    End Sub
    Public Sub View_File_Attachment(ID As String, fileExtension As String, FileName As String)
        'Dim FileURL As String = Base._UPDATE_Server.Substring(0, Base._UPDATE_Server.Length - 1) & "/Attachments/" & ID & "." & fileExtension
        'Dim CustomFileName As String = ID & "." & fileExtension
        'Dim DownloadPath As String = "" & Base._App_path & "ATTACHMENTS\" & FileName
        'If Not System.IO.Directory.Exists("" & Base._App_path & "ATTACHMENTS\") Then
        '    System.IO.Directory.CreateDirectory("" & Base._App_path & "ATTACHMENTS\")
        'End If
        ''Dim client As New System.Net.WebClient()
        ''client.DownloadFile(FileURL, DownloadPath)
        'Dim fs1 As System.IO.FileStream = Nothing
        'Dim b1() As Byte = Base._Attachments_DBOps.Download_File(CustomFileName)
        'fs1 = New System.IO.FileStream(DownloadPath, FileMode.Create)
        'fs1.Write(b1, 0, b1.Length)
        'fs1.Close()
        'fs1 = Nothing

        'Dim sInfo As New ProcessStartInfo(DownloadPath)
        'Try
        '    Process.Start(sInfo)
        'Catch ex As Exception
        '    Process.Start("iexplore.exe", sInfo.FileName)
        'End Try
    End Sub
    ' ''Dim SQL_STR8 As String = " SELECT SG_ID FROM Acc_Seconary_Group_Info "
    ' ''Dim d8 As New Common_Lib.Get_Data(Base, "CORE", "Acc_Seconary_Group_Info", SQL_STR8)
    ' ''Dim trans As OleDbTransaction = Nothing
    ' ''    Using Con As New OleDbConnection(Base._data_ConStr_Core)
    ' ''        Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
    ' ''        Try
    ' ''            trans = Con.BeginTransaction
    ' ''            command.Connection = Con
    ' ''            command.Transaction = trans
    ' ''            For Each currRow In d8._dc_DataTable.Rows
    '' '' Me.Text = currRow("rec_id").ToString()

    ' ''Dim STR1 As String = " UPDATE Acc_Seconary_Group_Info SET " & _
    ' ''                     " " & _
    ' ''                 "REC_ID='" & System.Guid.NewGuid.ToString & "'," & _
    ' ''                 "REC_ADD_ON       =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
    ' ''                 "REC_ADD_BY       ='" & "HO_ADMIN" & "', " & _
    ' ''                 "REC_EDIT_ON       =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
    ' ''                 "REC_EDIT_BY       ='" & "HO_ADMIN" & "', " & _
    ' ''                 "REC_STATUS        =" & Common_Lib.Common.Record_Status._Locked & "," & _
    ' ''                 "REC_STATUS_ON     =#" & Now.ToString(Base._Date_Format_Long) & "#," & _
    ' ''                 "REC_STATUS_BY     ='" & "HO_ADMIN" & "'  " & _
    ' ''                 "  WHERE SG_ID    = '" & currRow("SG_ID").ToString() & "' "
    ' ''                command.CommandText = STR1 : command.ExecuteNonQuery()

    ' ''            Next
    ' ''            trans.Commit()
    ' ''        Catch ex As Exception
    ' ''            trans.Rollback()
    ' ''        End Try
    ' ''    End Using
    ' ''    d8._dc_Connection.Close()
End Module
