Imports Common_Lib.RealTimeService
Imports System.Configuration
Imports System.IO
Imports System.Data.SqlClient
Imports Common_Lib.DbOperations
Imports Common_Lib.QAService

Partial Public Class DbOperations
    <Serializable>
    Public Class TestSupport
        'Inherits SharedVariables
        Shared Sub New()
            Dim testmode As Boolean = False
            Try
                Dim Make_INI_File As New Utility.INI_FileTask("c:\co_build\COMMON\ConnectOne.Set.ini")
                testmode = IIf(Make_INI_File.GetString("TEST", "MODE", "LIVE").ToUpper = "TEST", True, False)
                TestDBUpdateMode = IIf(Make_INI_File.GetString("TEST-DBUPDATE", "MODE", "FALSE").ToUpper = "TRUE", True, False)
                '_qa_service = New WebService
                _qa_service.Url = Make_INI_File.GetString("HTTP-SERVER", "SERVER", "http://accountserver.bkinfo.in/") & "QAService/service.asmx"
            Catch
                Exit Sub
            End Try
            If Not testmode Then Exit Sub
            'Dim ConStr As String
            'ConStr = "Data Source=192.168.5.7;Initial Catalog=ConnectOneQA;Integrated Security=False;User ID=sa;Password=DoubleLight;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"
            'Connection = New SqlConnection(ConStr)
            'Connection.Open()
        End Sub
        Shared FrmName As String
        'Shared ConStr As String
        Shared TestCaseNum As String
        ' Shared Connection As SqlConnection
        Shared DTab As DataTable
        Shared DTab_ScheduledTestCases As New DataTable
        Shared DTab_TestSteps As New DataTable
        Shared _qa_service As New QAService.WebService
        'Shared _base As Common_Lib.Common
        Sub quit()

            ' Base._CenterDBOps.LogOut()
            ''Process.GetProcessesByName("ConnectOne.E0001")

            'If Process.GetProcessesByName("ConnectOne.E0001").Length > 0 Then
            '    Try : Process.GetProcessesByName("ConnectOne.E0001")(0).Kill() : Catch ex As Exception : DevExpress.XtraEditors.XtraMessageBox.Show("Some Connect One feature are not working due to this error...!" & vbNewLine & vbNewLine & "Filename : ConnectOne.E0001.exe" & vbNewLine & vbNewLine & "Filepath : " & Base._App_path & vbNewLine & vbNewLine & ex.Message, "Connect One Silent Tool Not Working....!", MessageBoxButtons.OK, MessageBoxIcon.Stop) : End Try
            'End If
            'If Process.GetProcessesByName("ConnectOne.E0002").Length > 0 Then
            '    Try : Process.GetProcessesByName("ConnectOne.E0002")(0).Kill() : Catch ex As Exception : DevExpress.XtraEditors.XtraMessageBox.Show("Some Connect One feature are not working due to this error...!" & vbNewLine & vbNewLine & "Filename : ConnectOne.E0002.exe" & vbNewLine & vbNewLine & "Filepath : " & Base._App_path & vbNewLine & vbNewLine & ex.Message, "Connect One Silent Tool Not Working....!", MessageBoxButtons.OK, MessageBoxIcon.Stop) : End Try
            'End If
            '    Me.Close()
        End Sub
        Shared Function Scheduled_TestCases() As DataTable
            'ConStr = "Data Source=192.168.5.7;Initial Catalog=ConnectOneQA;Integrated Security=False;User ID=sa;Password=DoubleLight;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"
            'Connection = New SqlConnection(ConStr)
            'Connection.Open()
            DTab_ScheduledTestCases = New DataTable()
            DTab_ScheduledTestCases = _qa_service.GetData("SELECT TOP 1 TESTCASENO FROM SCHEDULEDTESTCASES WHERE TESTSTATUS='NOT COMPLETED'  ORDER BY  TIME_OF_START")
            Return DTab_ScheduledTestCases
        End Function
        Shared Function TestSteps(TestCAseNum As String) As DataTable
            Dim Qry As String
            Dim dtab As New DataTable
            'Qry = " SELECT TCD.*,SM.AccessibleName ,SM.TITLE as Title,CM.COLUMN_TYPE as ColumnType,CM.ControlNameInCode,CM.AccessibleDescription,CM.ParentControl FROM  TESTCASE_DESC TCD LEFT OUTER JOIN SCREEN_MASTER SM ON TCD.SCREEN_ID=SM.SCREEN_ID " +
            '                " LEFT OUTER JOIN COLUMN_MASTER CM ON TCD.COLUMN_ID=CM.COLUMN_ID " +
            '                " WHERE TESTCASENO= " + TestCAseNum + "  order by stepsrlno "
            Return _qa_service.GetData(" SELECT TCD.*,SM.AccessibleName ,SM.TITLE as Title,CM.COLUMN_TYPE as ColumnType,CM.ControlNameInCode,CM.AccessibleDescription,CM.ParentControl FROM  TESTCASE_DESC TCD LEFT OUTER JOIN SCREEN_MASTER SM ON TCD.SCREEN_ID=SM.SCREEN_ID " +
                          " LEFT OUTER JOIN COLUMN_MASTER CM ON TCD.COLUMN_ID=CM.COLUMN_ID " +
                          " WHERE TESTCASENO= " + TestCAseNum + "  order by stepsrlno ")

            'Dim Adpr As SqlDataAdapter
            'Adpr = New SqlDataAdapter(Qry, Connection)
            'Adpr.Fill(dtab)
            'Return dtab
        End Function
        Shared Sub MakeEntryInTestResultMaster()
            '  -- Insert record in  TestResultMaster --
            Dim TestResNum As Int64
            TestResNum = Convert.ToInt64(_qa_service.GetData("SELECT COALESCE(MAX(TestResultNo),0)+1 FROM TestResultMaster  ").Rows(0)(0).ToString())
            _qa_service.ExecuteQuery("INSERT INTO TestResultMaster values(" + TestCaseNum + "," + TestResNum.ToString() + ",getdate(),'ver4.0',null,null,'Under progress')")
            ' Cmd.ExecuteNonQuery()
            'Cmd.Dispose()
        End Sub
        Shared Sub UpdateTestStatus(TestCAseNum As String, TestStatus As String)
            _qa_service.ExecuteQuery("UPDATE SCHEDULEDTESTCASES SET TESTSTATUS='" + TestStatus + "' WHERE TESTCASENO=" + TestCAseNum.ToString())
        End Sub
        Shared CurrentStepNum As Integer = 0
        Shared TestDBUpdateMode As Boolean = False
        Shared UID As String
        Shared t As Type
        Public Shared Sub StoreControlDetail(Frm As Form)
            Try
                UID = Guid.NewGuid().ToString()
                Dim testmode As Boolean = False
                ' Dim sw As New System.IO.StreamWriter("c:/Script/" + FrmName + "_script.sql")
                'MessageBox.Show("Baba")
                'If _base Is Nothing Then
                'Dim cmn As New Common_Lib.Common
                'Dim Make_INI_File As New Utility.INI_FileTask(cmn._App_path & "COMMON\ConnectOne.Set.ini")
                Try
                    Dim Make_INI_File As New Utility.INI_FileTask("c:\co_build\COMMON\ConnectOne.Set.ini")
                    testmode = IIf(Make_INI_File.GetString("TEST", "MODE", "LIVE").ToUpper = "TEST", True, False)
                    TestDBUpdateMode = IIf(Make_INI_File.GetString("TEST-DBUPDATE", "MODE", "FALSE").ToUpper = "TRUE", True, False)
                Catch
                    Exit Sub
                End Try

                'Else
                'testmode = _base._test_Mode
                'End If
                If Not testmode Then Exit Sub
                Try
                    If Frm.GetType().Name = "PleaseWait" Then Exit Sub

                    t = Frm.GetType
                    FrmName = t.Name
                    Frm.KeyPreview = True
                    'If Not Frm.Text.Contains(":") Then
                    '    Frm.Text = Frm.Name + ":" + Frm.Text
                    'End If
                    'Frm.Name = FrmName
                    Frm.Text = FrmName
                    AddHandler Frm.Shown, AddressOf Frm_Shown
                    'If FrmName = "Frm_Voucher_Win_Journal" Then
                    'AddHandler Frm.Activated, AddressOf Frm_Activated
                    'AddHandler Frm.MdiChildActivate, AddressOf Frm_Activated



                    'AddHandler Frm.GotFocus, AddressOf Frm_Activated

                    'End If

                    Frm.AccessibleName = FrmName ' Frm.Name
                    If TestDBUpdateMode = True Then
                        '  If Not Directory.Exists("C:/script") Then Directory.CreateDirectory("C:/script")
                        'sw = New System.IO.StreamWriter("c:/Script/" + FrmName + "_script.sql")
                        InsertScript(UID, "  if not exists (select * from screen_master where AccessibleName ='" + FrmName + "') ")
                        'InsertScript(UID, "  if not exists (select * from screen_master where AccessibleName ='" + FrmName + "') ")
                        'InsertScript(UID, " begin")
                        'InsertScript(UID, "   insert into SCREEN_MASTER (screen_id,screen_name,screen_code,AccessibleName,title,REC_ADD_ON)  values(newid(),'" + FrmName + "','" + FrmName + "','" + FrmName + "','" + Frm.Text + "',getdate());")
                        'InsertScript(UID, " end ;")
                        InsertScript(UID, " begin ")
                        InsertScript(UID, "   insert into SCREEN_MASTER (screen_id,screen_name,screen_code,AccessibleName,title,REC_ADD_ON)  values(newid(),'" + FrmName + "','" + FrmName + "','" + FrmName + "','" + Frm.Text + "',getdate());")
                        InsertScript(UID, " end ;")
                        'sw.Close()
                        'ExecuteScriptFile("c:/Script/" + FrmName + "_script.sql")
                        ExecuteScriptFile()
                    End If
                    For Each ctrl As System.Windows.Forms.Control In Frm.Controls
                        'ctrl.ForeColor = Color.OrangeRed
                        'ctrl.Enabled = True
                        Try
                            ctrl.AccessibleDescription = ctrl.Name '+ "^" + ctrl.Parent.Name + "^" + FrmName
                            StoreChildControlDetail(ctrl)
                        Catch ex As Exception
                        End Try
                    Next
                    'ExecuteScriptFile("c:/Script/" + Frm.Name + "_script.sql")
                Catch ex As Exception
                    'MsgBox(" Err from TestSupport " + ex.Message)
                Finally
                    'sw.Close()
                End Try
            Catch ex As Exception
                Exit Sub
            End Try
        End Sub
        Shared Function InsertScript(UID As String, script As String)
            Dim Query As String
            Query = "insert into QAScript values('" + UID + "','" + script.Replace("'", "''") + "')"
            _qa_service.ExecuteQuery(Query)
        End Function
        Shared Function GetScript(UID As String) As String
            Dim dt As New DataTable
            dt = _qa_service.GetData("SELECT SCRIPT FROM QAScript WHERE UID='" + UID + "' ORDER BY ID")
            Dim i As Integer
            Dim Query As String = ""
            For i = 1 To dt.Rows.Count
                Query += dt.Rows(i - 1)(0).ToString()
            Next
            Return Query
        End Function
        'Shared Function GetData(Query As String) As DataTable
        '    Dim DTab As New DataTable
        '    Dim Adpr As New SqlDataAdapter(Query, Connection)
        '    Adpr.Fill(DTab)
        '    Return DTab
        'End Function
#Region "Private Functions"
        'Shared Sub ExecuteScriptFile(FilePath As String)
        Shared Sub ExecuteScriptFile()
            'Dim Con As System.Data.SqlClient.SqlConnection
            'Con = New System.Data.SqlClient.SqlConnection("Data Source=192.168.5.7;Initial Catalog=ConnectOneQA;Persist Security Info=True;User ID=sa;Password=DoubleLight")
            'Dim sr As New System.IO.StreamReader(FilePath)
            Try
                Dim dbstring As String
                'dbstring = sr.ReadToEnd()
                'dbstring = GetData("select script from QAScript where UID='" + UID + "'").Rows(0)(0).ToString()
                dbstring = GetScript(UID)
                'sr.Close()
                If dbstring.Trim.Length = 0 Then Exit Sub
                'Con.Open()
                _qa_service.ExecuteQuery(dbstring)
                'Dim Cmd As New System.Data.SqlClient.SqlCommand
                'Cmd.Connection = Connection
                'Cmd.CommandText = dbstring
                'Cmd.ExecuteNonQuery()
                'Cmd.Dispose()

            Catch ex As Exception
                MsgBox("Err in Executing script file : " + ex.Message)
            Finally

            End Try
        End Sub
        'Shared Function FindControl(Frm As Form, CtrlName As String) As Control
        '    Dim res As Integer
        '    Dim i As Integer
        '    For i = 0 To Frm.Controls.Count - 1
        '        If Frm.Controls(i).Name = CtrlName Then
        '            Return i
        '        Else
        '            Return FindControl co
        '        End If
        '    Next
        '    Return res
        'End Function
        Public Shared Sub FindCtrl(controlname As String, parentControl As System.Windows.Forms.Control)
            If Not IsNothing(Ctrl) Then Exit Sub
            For Each cCtrl As Control In parentControl.Controls
                If cCtrl.Name = controlname Then
                    Ctrl = cCtrl
                    Exit Sub
                End If
                FindCtrl(controlname, cCtrl)
            Next
        End Sub
        Public Shared Function FindCtrlFn(controlname As String, parentControl As System.Windows.Forms.Control) As Control
            If Not IsNothing(Ctrl) Then Exit Function
            For Each cCtrl As Control In parentControl.Controls
                If cCtrl.Name = controlname Then
                    Return cCtrl
                    Exit Function
                End If
                Return FindCtrlFn(controlname, cCtrl)
            Next
        End Function
        Public Shared Function GetAll(control As ArrayList, parentControl As System.Windows.Forms.Control) As ArrayList
            control.Add(parentControl)
            For Each cCtrl As Control In parentControl.Controls
                GetAll(control, cCtrl)
            Next
            Return control
        End Function
        'Shared Function GetList(Query As String) As DataTable
        '    'Dim Con As System.Data.SqlClient.SqlConnection
        '    'Con = New System.Data.SqlClient.SqlConnection("Data Source=192.168.5.7;Initial Catalog=ConnectOneQA;Persist Security Info=True;User ID=sa;Password=DoubleLight")
        '    'Con.Open()
        '    Dim Adpr As SqlClient.SqlDataAdapter
        '    Dim dtab As New DataTable
        '    Adpr = New System.Data.SqlClient.SqlDataAdapter(Query, Connection)
        '    Adpr.Fill(dtab)
        '    Return dtab
        'End Function

        ''' <summary>
        ''' Updates Column_master 
        ''' </summary>
        ''' <param name="CtrlName">It is AccessibleDescription;used to identify a column uniquely</param>
        ''' <param name="Value">Name/desc[Value to be stored in database]</param>
        ''' <param name="Frm">Name of the form</param>
        ''' <param name="FieldName">Name of the field in Column_master to be updated</param>
        ''' <remarks></remarks>
        Shared Sub UpdateControlName(CtrlName As String, Value As String, Frm As Form, FieldName As String)
            'Dim Con As System.Data.SqlClient.SqlConnection
            'Con = New System.Data.SqlClient.SqlConnection("Data Source=192.168.5.7;Initial Catalog=ConnectOneQA;Persist Security Info=True;User ID=sa;Password=DoubleLight")
            'Con.Open()
            Dim cmd As String
            ' Dim Cmd As New System.Data.SqlClient.SqlCommand
            ' Cmd.Connection = Connection
            cmd = "update column_master set " + FieldName + "='" + Value
            cmd += "' where ControlNameInCode='" + CtrlName + "' and screen_id=(select screen_id from screen_master where accessiblename='" + FrmName + "')"
            _qa_service.ExecuteQuery(cmd)
            'Cmd.Dispose()
        End Sub

        Public Shared Sub StoreChildControlDetail(Ctrl As Control)
            Try
                'Ctrl.ForeColor = Color.OrangeRed
                ' FrmName = Ctrl.FindForm.Name
                Dim rec As String
                ' Dim sw As System.IO.StreamWriter
                'sw = New System.IO.StreamWriter("c:/Script/" + FrmName + "_script.sql", True)
                Try
                    AddHandler Ctrl.MouseDown, AddressOf Ctrl_MouseEvent
                    Ctrl.AccessibleDescription = Ctrl.Name  '+ "^" + Ctrl.Parent.Name + "^" + FrmName               
                    If TestDBUpdateMode Then
                        'Ctrl.Enabled = True
                        If Ctrl.Name <> "" Then
                            InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where AccessibleName='" + FrmName + "') and  accessibledescription='" + Ctrl.Name + "' and parentcontrol='" + Ctrl.Parent.Name + "')")
                            InsertScript(UID, " begin ")
                            rec = "INSERT INTO COLUMN_MASTER (column_id,column_name,screen_id,column_type,controlnameincode,"
                            rec += "classnameincode,accessibledescription,parentcontrol,parenttype,fullname,sequence,REC_ADD_ON) "
                            rec += " VALUES(newid(),"
                            rec += "'" + Ctrl.Name + "',"
                            rec += "(select screen_id from screen_master where AccessibleName='" + FrmName + "'),"
                            rec += "'" + Ctrl.GetType().Name + "',"
                            rec += "'" + Ctrl.Name + "',"
                            rec += "'" + Ctrl.GetType().Name + "',"
                            rec += "'" + Ctrl.Name + "',"
                            rec += "'" + Ctrl.Parent.Name + "',"
                            rec += "'" + Ctrl.Parent.GetType().Name + "',"
                            rec += "'" + Ctrl.Name + ";" + Ctrl.Parent.Name + ";" + Ctrl.GetType().Name + "'," + Ctrl.TabIndex.ToString + ",getdate())"
                            InsertScript(UID, rec)
                            InsertScript(UID, "End;")
                            'sw.Close()
                            'sw.Dispose()
                        End If
                    End If
                    'If Ctrl.GetType.Name = "ToolTip" Then

                    '    Dim obj As System.Windows.Forms.ToolTip
                    '    obj = CType(Ctrl, System.Windows.Forms.ToolTip)
                    '    obj.AutomaticDelay = 25000
                    'End If

                    If Ctrl.GetType.Name = "GridControl" Then
                        '  MessageBox.Show("Working... on GridControl")
                        Dim obj As DevExpress.XtraGrid.GridControl
                        obj = CType(Ctrl, DevExpress.XtraGrid.GridControl)
                        'obj.ForeColor 
                        CType(obj.MainView, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = True
                        'obj.de
                        ' Me.GridView1.OptionsView.ShowAutoFilterRow = True
                        AddHandler obj.Load, AddressOf GridControl_Loaded
                        AddHandler obj.Click, AddressOf GridControl_Loaded
                        'AddHandler obj.MouseClick, AddressOf GridControl_Focus
                        'GridControl_Update(Ctrl)
                    End If


                    If Ctrl.GetType.Name = "GridLookUpEdit" Then
                        Dim obj As DevExpress.XtraEditors.GridLookUpEdit
                        obj = CType(Ctrl, DevExpress.XtraEditors.GridLookUpEdit)
                        obj.Properties.View.OptionsView.ShowAutoFilterRow = True
                        'AddHandler obj.Popup, AddressOf GridLookUpEdit_Loaded 
                        '  AddHandler obj.Load, AddressOf GridLookUpEdit_Load
                        'CType(obj, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = True

                        'GridLookUpEdit_Update(Ctrl)
                        ''Dim obj2 As DevExpress.XtraEditors.GridLookUpEdit
                        ''obj2 = CType(Ctrl, DevExpress.XtraEditors.GridLookUpEdit)
                        ''     AddHandler obj2., AddressOf GridLookUpEdit_Loaded
                    End If
                    If TestDBUpdateMode Then
                        'ExecuteScriptFile("c:/Script/" + FrmName + "_script.sql")
                        ExecuteScriptFile()
                    End If
                    For Each C As Control In Ctrl.Controls
                        Try
                            C.AccessibleDescription = C.Name
                            StoreChildControlDetail(C)
                        Catch ex As Exception
                        End Try
                    Next
                Catch ex As Exception
                Finally
                    'sw.Close()
                    'sw.Dispose()
                End Try
            Catch ex As Exception
            End Try
        End Sub

#End Region
        Shared Ctrl As Control = Nothing
#Region " -- Events -- "
        Private Shared Sub Frm_KeyUp(sender As Object, e As KeyEventArgs)
            '        A, Shift, Control, Alt
            ' If Alt Control Shift Key is not pressed then exit 
            Try
                Dim CtrlIndex As Integer
                Ctrl = Nothing
                If Not e.KeyData.ToString().Contains("Shift, Control, Alt") Then Exit Sub
                Select Case FrmName
                    Case "Frm_Voucher_Win_Cash"
                        Select Case e.KeyCode.ToString
                            Case "A"
                                FindCtrl("GLookUp_ItemList", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Control).FindForm().Controls(1).Focus() ' this works
                                '                        CType(sender, Control).FindForm().Controls("GLookUp_ItemList").Focus() ' this not works 
                                ' So Ctrl Index Pair has to be identified and coded well

                                '  Dim C As Control = FindControl(CType(sender, Control).FindForm(), "GLookUp_ItemList")
                                '    CType(CType(sender, Control).FindForm(), ConnectOne.D0010._001.Frm_Voucher_Win_Cash).Controls("GLookUp_ItemList").Focus()
                                'Dim obj As New ConnectOne.D0010._001.Frm_Voucher_Win_Cash
                                'Dim obj As New ConnectOne.D0010._001.Frm_Voucher_Win_Cash
                                'CType(sender, Control).FindForm().Controls(CtrlIndex).Focus()
                                ' C.Focus()
                            Case "B"
                                FindCtrl("Txt_V_Date", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                ' CType(sender, Control).FindForm().Controls(2).Focus()
                                ' '  CType(CType(sender, Control).FindForm(), ConnectOne.D0010._001.Frm_Voucher_Win_Cash).Controls("Txt_V_Date").Focus()
                                ' CtrlIndex = FindIndex(CType(sender, Control).FindForm(), "Txt_V_Date")
                                'CType(sender, Control).FindForm().Controls(CtrlIndex).Focus()
                                'CType(sender, Form).Controls("Txt_V_Date").Focus()
                                'CType(sender, Control).FindForm().Controls(2).Focus()
                                'CType(CType(sender, Control).FindForm(), ConnectOne.D0010._001.Frm_Voucher_Win_Cash).Controls("Txt_V_Date").Focus()
                            Case "C"
                                FindCtrl("GLookUp_BankList", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                ' CType(sender, Control).FindForm().Controls(3).Focus()
                                'CType(sender, Form).Controls("GLookUp_BankList").Focus()
                            Case "D"
                                FindCtrl("Txt_Ref_No", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                '  CType(sender, Control).FindForm().Controls(4).Focus()
                                'CType(sender, Form).Controls("Txt_Ref_No").Focus()
                            Case "E"
                                FindCtrl("Txt_Ref_Date", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Control).FindForm().Controls(5).Focus()
                                'CType(sender, Form).Controls("Txt_Ref_Date").Focus()
                            Case "F"
                                FindCtrl("Txt_Ref_CDate", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                ' CType(sender, Control).FindForm().Controls(6).Focus()
                                '                        CType(sender, Form).Controls("Txt_Ref_CDate").Focus()
                            Case "G"
                                FindCtrl("Txt_Narration", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Control).FindForm().Controls(7).Focus()
                                '                        CType(sender, Form).Controls("Txt_Narration").Focus()
                            Case "H"
                                FindCtrl("Txt_Remarks", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Form).Controls("Txt_Remarks").Focus()
                            Case "I"
                                FindCtrl("Txt_Reference", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                ' CType(sender, Form).Controls("Txt_Reference").Focus()
                            Case "J"
                                FindCtrl("Txt_Amount", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Form).Controls("Txt_Amount").Focus()
                            Case "K"
                                FindCtrl("BUT_SAVE_COM", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                '  CType(sender, Form).Controls("BUT_SAVE_COM").Focus()
                            Case "L"
                                FindCtrl("BUT_CANCEL", CType(sender, Control).FindForm())
                                Ctrl.Focus()
                                'CType(sender, Form).Controls("BUT_CANCEL").Focus()
                        End Select
                End Select
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub Ctrl_MouseEvent(sender As Object, e As MouseEventArgs)
            Try
                Dim ValidCtrlName As String
                Dim ExistingCtrlName As String
                Dim ExistingCtrlDesc As String
                Dim ShortCut As String
                '----------------------ShortCut-----------------------------------------------
                'If Control.ModifierKeys = Keys.Alt Then
                '    ExistingCtrlName = GetList("select column_name from column_master where accessibleDescription='" + CType(sender, Control).Name + "^" + CType(sender, Control).Parent.Name + "^" + CType(sender, Control).FindForm.Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString
                '    ExistingCtrlDesc = GetList("select column_desc from column_master where accessibleDescription='" + CType(sender, Control).Name + "^" + CType(sender, Control).Parent.Name + "^" + CType(sender, Control).FindForm.Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString

                '    ShortCut = InputBox("Enter Valid ShortCutKey [Ctrl+Shift+F12+?] : ", "Name:" + ExistingCtrlName + ";Desc:" + ExistingCtrlDesc + ";Type:" + CType(sender, Control).GetType.Name)
                '    If ShortCut = Nothing Or ShortCut.Trim.Length = 0 Then Exit Sub
                '    UpdateControlName(CType(sender, Control).Name, ShortCut, CType(sender, Control).FindForm, "ShortCut")
                'End If
                '-------------------------------------------------------------------------------------


                ' ---------------------AccessibleDesc Display-------------
                If Control.ModifierKeys = Keys.Alt Then
                    'MsgBox("msgbox test1")
                    'MessageBox.Show("MessageBox.Show test2")
                    If CType(sender, Control).GetType.Name = "GridControl" Then
                        Dim dg As DevExpress.XtraGrid.GridControl
                        dg = CType(sender, DevExpress.XtraGrid.GridControl)
                        Dim gv As DevExpress.XtraGrid.Views.Grid.GridView
                        gv = dg.DefaultView
                        MsgBox(gv.FocusedColumn.Name)
                        MessageBox.Show("AccessibleDescription : " + gv.FocusedColumn.Name + " Parent : " + CType(sender, Control).Parent.Name + " Form : " + CType(sender, Control).FindForm().Name)
                        Exit Sub
                    End If

                    MessageBox.Show("AccessibleDescription : " + CType(sender, Control).AccessibleDescription + " Parent : " + CType(sender, Control).Parent.Name + " Form : " + CType(sender, Control).FindForm().Name)
                    'StoreControlDetail(CType(sender, Control).FindForm)
                End If

                If Control.ModifierKeys = Keys.Shift Then
                    ExistingCtrlName = _qa_service.GetData("select column_name from column_master where ControlNameInCode='" + CType(sender, Control).Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString

                    Dim seq As String
                    seq = _qa_service.GetData("select sequence from column_master where ControlNameInCode='" + CType(sender, Control).Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString
                    seq = InputBox("Enter Valid Sequence : ", "Name:" + ExistingCtrlName + ",Sequence:" + seq)
                    'ValidCtrlName = InputBox("Enter Valid Control Name : ")
                    If seq = Nothing Or seq.Trim.Length = 0 Then Exit Sub
                    UpdateControlName(CType(sender, Control).Name, seq, CType(sender, Control).FindForm, "Sequence")
                End If

                '----------------------------------------------------------------------

                If Control.ModifierKeys = Keys.Control Then
                    ExistingCtrlName = _qa_service.GetData("select column_name from column_master where ControlNameInCode='" + CType(sender, Control).Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString
                    ' ExistingCtrlDesc = GetList("select column_desc from column_master where accessibleDescription='" + CType(sender, Control).Name + "^" + CType(sender, Control).Parent.Name + "^" + CType(sender, Control).FindForm.Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString

                    ValidCtrlName = InputBox("Enter Valid Control Name : ", "Name:" + ExistingCtrlName)
                    'ValidCtrlName = InputBox("Enter Valid Control Name : ")
                    If ValidCtrlName = Nothing Or ValidCtrlName.Trim.Length = 0 Then Exit Sub
                    UpdateControlName(CType(sender, Control).Name, ValidCtrlName, CType(sender, Control).FindForm, "Column_Name")
                End If
                If Control.ModifierKeys = Keys.ShiftKey Then
                    ExistingCtrlName = _qa_service.GetData("select column_name from column_master where accessibleDescription='" + CType(sender, Control).Name + "^" + CType(sender, Control).Parent.Name + "^" + CType(sender, Control).FindForm.Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString
                    ExistingCtrlDesc = _qa_service.GetData("select column_desc from column_master where accessibleDescription='" + CType(sender, Control).Name + "^" + CType(sender, Control).Parent.Name + "^" + CType(sender, Control).FindForm.Name + "' and screen_id=(select screen_id from screen_master where accessiblename='" + CType(sender, Control).FindForm().Name + "')").Rows(0)(0).ToString

                    ValidCtrlName = InputBox("Enter Valid Control Description : ", "Name:" + ExistingCtrlName + ";Desc:" + ExistingCtrlDesc + ";Type:" + CType(sender, Control).GetType.Name)
                    If ValidCtrlName = Nothing Or ValidCtrlName.Trim.Length = 0 Then Exit Sub
                    UpdateControlName(CType(sender, Control).Name, ValidCtrlName, CType(sender, Control).FindForm, "Column_Desc")
                End If

            Catch ex As Exception
                'MsgBox("Err:" + ex.Message)
            End Try
        End Sub
        Public Shared Sub Frm_Activated(sender As Object, e As EventArgs)
            Try
                Dim Frm As Form
                MsgBox("Activated")
                Frm = CType(sender, System.Windows.Forms.Form)
                Frm.AccessibleName = Frm.GetType().Name
                'If Not Frm.Text.Contains(":") Then
                Frm.Text = Frm.GetType().Name 'Frm.Name '+ ":" + Frm.Text
                ' End If
                '   If Not Frm.Text.Contains("FormName") Then
                'Frm.Text += "#" + Frm.Name
                ' End If

            Catch ex As Exception
            End Try
        End Sub
        Public Shared Sub Frm_Shown(sender As Object, e As EventArgs)
            Try
                Dim Frm As Form
                Frm = CType(sender, System.Windows.Forms.Form)
                Frm.AccessibleName = Frm.GetType().Name
                'If Not Frm.Text.Contains(":") Then
                Frm.Text = Frm.GetType().Name 'Frm.Name '+ ":" + Frm.Text
                ' End If
                '   If Not Frm.Text.Contains("FormName") Then
                'Frm.Text += "#" + Frm.Name
                ' End If
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub GridControl_Update(sender As DevExpress.XtraGrid.GridControl)
            Try
                CType(sender.MainView, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = True
                'sender.OptionsView.ShowAutoFilterRow = True
                FrmName = sender.FindForm.GetType().Name
                Dim rec As String
                Dim GridTabIndex As Integer
                GridTabIndex = (CType(sender, DevExpress.XtraGrid.GridControl)).TabIndex
                Dim colindex As Integer = 0
                ' Dim sw As System.IO.StreamWriter
                'sw = New System.IO.StreamWriter("c:/Script/" + FrmName + "grid_script.sql")

                For Each Ctrl As DevExpress.XtraGrid.Columns.GridColumn In CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
                    ' Ctrl.Visible = True
                    'Ctrl.VisibleIndex = CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).VisibleColumns.Count
                    Ctrl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                    Ctrl.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default
                    'Ctrl.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                    colindex += 1
                    If TestDBUpdateMode Then
                        InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where AccessibleName='" + FrmName + "') and accessibledescription='" + Ctrl.Name + "' and parentcontrol='" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "')")
                        InsertScript(UID, " begin ")
                        rec = "INSERT INTO COLUMN_MASTER (column_id,column_name,screen_id,column_type,controlnameincode,"
                        rec += "classnameincode,accessibledescription,column_desc,parentcontrol,parenttype,fullname,sequence,REC_ADD_ON) "
                        rec += " VALUES(newid(),"
                        rec += "'" + Ctrl.ToString() + "',"
                        rec += "(select screen_id from screen_master where AccessibleName='" + FrmName + "'),"
                        rec += "'" + Ctrl.GetType().Name + "',"
                        rec += "'" + Ctrl.Name + "',"
                        rec += "'" + Ctrl.GetType().Name + "',"
                        rec += "'" + Ctrl.Name + "',"
                        rec += "'ControlName:" + Ctrl.Name + ";FieldName:" + Ctrl.FieldName + ";ColEditorName:" + Ctrl.ColumnEditName + ";Ctrl.ToString() : " + Ctrl.ToString() + "',"
                        rec += "'" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "',"
                        rec += "'" + (CType(sender, DevExpress.XtraGrid.GridControl)).GetType().Name + "',"
                        rec += "'" + Ctrl.Name + ";" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + ";" + Ctrl.GetType().Name + "',(select max(sequence)+" + colindex.ToString() + " from column_master where  screen_id=(select screen_id from screen_master where  accessiblename='" + FrmName + "'))," + "getdate())"
                        InsertScript(UID, rec + ";")
                        InsertScript(UID, "End;")
                        Dim GridViewName As String = CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Name
                        InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where AccessibleName='" + FrmName + "') and accessibledescription='" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "' and parentcontrol='" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "')")
                        InsertScript(UID, "begin ")
                        rec = "INSERT INTO COLUMN_MASTER VALUES(newid(),'" + Ctrl.ToString() + " - GridFilter',(select screen_id from screen_master where AccessibleName='" + FrmName + "'),'',0,'TextEdit','" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "',' TextEdit', '" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "','" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "',null,null,null,402,getdate(),getdate(),(select concat('GridControl1TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "@TextEdit;',NavigationString)  from column_master I where I.COLUMN_NAME= '" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "' and SCREEN_ID =(select screen_id from SCREEN_MASTER  where AccessibleName ='" + FrmName + "')),'',1)"
                        InsertScript(UID, rec + ";")
                        InsertScript(UID, "End;")
                    End If
                Next

                'sw.Close()
                'sw.Dispose()

                If FrmName = "Frm_COD_Selection_Ins" Or FrmName = "Frm_Login" Then Exit Sub
                If TestDBUpdateMode Then
                    'ExecuteScriptFile("c:/Script/" + FrmName + "grid_script.sql")
                    ExecuteScriptFile()
                    System.Threading.Thread.Sleep(2000)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub GridLookUpEdit_Loaded(sender As Object, e As EventArgs)

            'GridLookUpEdit_Update(sender)
        End Sub
        Public Shared Sub GridControl_Focus(sender As Object, e As EventArgs)
            Try
                'MsgBox("Focus")
                Dim i As Integer = 0

                For Each Ctrl As DevExpress.XtraGrid.Columns.GridColumn In CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
                    ' MsgBox(Ctrl.Name & " " & Ctrl.FieldName & " " & Ctrl.Caption)
                    Ctrl.Visible = True
                    Ctrl.VisibleIndex = CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).VisibleColumns.Count + i
                    'MsgBox(Ctrl.Name & " " & Ctrl.FieldName & " " & Ctrl.Caption & " " & Ctrl.VisibleIndex)
                    'MsgBox(CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).VisibleColumns.Count + i)
                    i += 1
                Next
                ' CType(sender, DevExpress.XtraGrid.GridControl).DefaultView
                'CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).re
            Catch ex As Exception
            End Try
        End Sub


        Public Shared Sub GridControl_Loaded(sender As Object, e As EventArgs)
            Try
                GridControl_Update(sender)

            Catch ex As Exception
            End Try
            'Dim 'sw As New System.IO.StreamWriter("c:/Script/" + FrmName + "grid_script.sql")
            'Dim rec As String 
            'Dim GridTabIndex As Integer
            'GridTabIndex = (CType(sender, DevExpress.XtraGrid.GridControl)).TabIndex
            'Dim colindex As Integer = 0
            'For Each Ctrl As DevExpress.XtraGrid.Columns.GridColumn In CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '    'MsgBox(col.Name)
            '    'MsgBox(col.Caption)
            '    'MsgBox(col.FieldName)
            '    'MsgBox(col.ColumnEditName)
            '    'MsgBox(col.ColumnType)
            '    Dim GridViewName As String = CType((CType(sender, DevExpress.XtraGrid.GridControl)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Name
            '    colindex += 1
            '    InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where AccessibleName='" + FrmName + "') and accessibledescription='" + Ctrl.Name + "' and parentcontrol='" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "')")
            '    InsertScript(UID, " begin")
            '    rec = "INSERT INTO COLUMN_MASTER (column_id,column_name,screen_id,column_type,controlnameincode,"
            '    rec += "classnameincode,accessibledescription,column_desc,parentcontrol,parenttype,fullname,sequence,REC_ADD_ON) "
            '    rec += " VALUES(newid(),"
            '    rec += "'" + Ctrl.ToString() + "',"
            '    rec += "(select screen_id from screen_master where AccessibleName='" + FrmName + "'),"
            '    rec += "'" + Ctrl.GetType().Name + "',"
            '    rec += "'" + Ctrl.Name + "',"
            '    rec += "'" + Ctrl.GetType().Name + "',"
            '    rec += "'" + Ctrl.Name + "',"
            '    rec += "'ControlName:" + Ctrl.Name + ";FieldName:" + Ctrl.FieldName + ";ColEditorName:" + Ctrl.ColumnEditName + ";Ctrl.ToString() : " + Ctrl.ToString() + "',"
            '    rec += "'" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "',"
            '    rec += "'" + (CType(sender, DevExpress.XtraGrid.GridControl)).GetType().Name + "',"
            '    rec += "'" + Ctrl.Name + ";" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + ";" + Ctrl.GetType().Name + "',(select max(sequence)+" + colindex.ToString() + " from column_master where  screen_id=(select screen_id from screen_master where  accessiblename='" + FrmName + "'))," + "getdate())"
            '    InsertScript(UID, rec)
            '    InsertScript(UID, "End;")
            '    InsertScript(UID, "begin")
            '    rec = "INSERT INTO COLUMN_MASTER VALUES(newid(),'" + Ctrl.ToString() + " - GridFilter',(select screen_id from screen_master where AccessibleName='" + FrmName + "'),'',0,'TextEdit','" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "',' TextEdit', '" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "','" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "',null,null,null,402,getdate(),getdate(),(select concat('GridControl1TextEdit[View]" + GridViewName + "[Row]-999997[Column]iTR_ITEM@TextEdit;',NavigationString)  from column_master I where I.COLUMN_NAME= '" + (CType(sender, DevExpress.XtraGrid.GridControl)).Name + "' and SCREEN_ID =(select screen_id from SCREEN_MASTER  where AccessibleName ='" + FrmName + "')),'',1)"
            '    InsertScript(UID, rec)
            '    InsertScript(UID, "End;")
            'Next
            ''sw.Close()
            ''sw.Dispose()
            'If FrmName = "Frm_COD_Selection_Ins" Or FrmName = "Frm_Login" Then Exit Sub
            'ExecuteScriptFile("c:/Script/" + FrmName + "grid_script.sql")
            ''System.Threading.Thread.Sleep(2000)
        End Sub

        Shared Sub GridLookUpEdit_Update(sender As DevExpress.XtraEditors.GridLookUpEdit)
            Dim rec As String
            Dim GridTabIndex As Integer
            GridTabIndex = sender.TabIndex
            Dim colindex As Integer = 0
            ' Dim sw As New System.IO.StreamWriter("c:/Script/" + FrmName + "_gridlookupedit_script.sql")
            For Each Ctrl As DevExpress.XtraGrid.Columns.GridColumn In sender.Properties.View.Columns
                'CType((CType(sender, DevExpress.XtraEditors.GridLookUpEdit)).Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
                Ctrl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                Ctrl.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default
                Ctrl.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                colindex += 1
                If TestDBUpdateMode Then
                    InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where  AccessibleName='" + FrmName + "') and accessibledescription='" + Ctrl.Name + "' and parentcontrol='" + sender.Name + "')")
                    InsertScript(UID, " begin ")
                    rec = "INSERT INTO COLUMN_MASTER (column_id,column_name,screen_id,column_type,controlnameincode,"
                    rec += "classnameincode,accessibledescription,column_desc,parentcontrol,parenttype,fullname,sequence,REC_ADD_ON) "
                    rec += " VALUES(newid(),"
                    rec += "'" + Ctrl.ToString() + "',"
                    rec += "(select screen_id from screen_master where AccessibleName='" + FrmName + "'),"
                    rec += "'" + Ctrl.GetType().Name + "',"
                    rec += "'" + Ctrl.Name + "',"
                    rec += "'" + Ctrl.GetType().Name + "',"
                    rec += "'" + Ctrl.Name + "',"
                    rec += "'ControlName:" + Ctrl.Name + ";FieldName:" + Ctrl.FieldName + ";ColEditorName:" + Ctrl.ColumnEditName + ";Ctrl.ToString() : " + Ctrl.ToString() + "',"
                    rec += "'" + sender.Name + "',"
                    rec += "'" + sender.GetType().Name + "',"
                    rec += "'" + Ctrl.Name + ";" + sender.Name + ";" + Ctrl.GetType().Name + "',(select max(sequence)+" + colindex.ToString() + " from column_master where  screen_id=(select screen_id from screen_master where  accessiblename='" + FrmName + "'))," + "getdate())"
                    InsertScript(UID, rec + ";")
                    InsertScript(UID, "End;")
                    Dim GridViewName As String = sender.Properties.View.Name
                    InsertScript(UID, "if not exists ( select * from column_master where screen_id=(select screen_id from screen_master where AccessibleName='" + FrmName + "') and accessibledescription='" + sender.Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "' and parentcontrol='" + sender.Name + "')")
                    InsertScript(UID, " begin ")
                    rec = "INSERT INTO COLUMN_MASTER VALUES(newid(),'" + Ctrl.ToString() + " - GridFilter',(select screen_id from screen_master where AccessibleName='" + FrmName + "'),'',0,'TextEdit','" + sender.Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "',' TextEdit', '" + sender.Name + "TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "','" + sender.Name + "',null,null,null,402,getdate(),getdate(),(select concat('GridControl1TextEdit[View]" + GridViewName + "[Row]-999997[Column]" + Ctrl.Name + "@TextEdit;',NavigationString)  from column_master I where I.COLUMN_NAME= '" + sender.Name + "' and SCREEN_ID =(select screen_id from SCREEN_MASTER  where AccessibleName ='" + FrmName + "')),'',1)"
                    InsertScript(UID, rec + ";")
                    InsertScript(UID, "End;")
                End If
            Next
            'sw.Close()
            'sw.Dispose()
            'ExecuteScriptFile("c:/Script/" + FrmName + "_gridlookupedit_script.sql")
            ExecuteScriptFile()
            If FrmName = "Frm_COD_Selection_Ins" Or FrmName = "Frm_Login" Then Exit Sub

            'ExecuteScriptFile("c:/Script/" + FrmName + "grid_script.sql")
            'System.Threading.Thread.Sleep(2000)

        End Sub
#End Region

        Private Sub Close()
            Throw New NotImplementedException
        End Sub

    End Class

End Class