Imports Common_Lib.RealTimeService
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Configuration

<Serializable>
Partial Public Class DbOperations
    'Service replaces it with its datetime
    Public Const DateTimePlaceHolder As String = "%datetime%"
    Public Shared originPath As String = ConfigurationManager.AppSettings("OriginPath")
    Public Shared AttachmentPath As String = ConfigurationManager.AppSettings("thumbnailpath")
    Public Shared ServicesPath As String = ConfigurationManager.AppSettings("Servicespath")
    ''' <summary>
    ''' Contains Protected Functions Not called directly by page level functions but shared by the functions called by Page
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable>
    Public Class SharedVariables
        'Protected _open_User_ID As String
        'Protected _open_Cen_ID As String
        ' Protected RealService As RealTimeService.ConnectOneWS
        Protected cBase As Common
        <Serializable>
        Public Class CommonReturnFields
            ''' <summary>
            ''' Actual Field name is Add By
            ''' </summary>
            ''' <returns></returns>
            Public Property Add_By As String
            ''' <summary>
            ''' Actual Field name is Add Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Add_Date As String
            ''' <summary>
            ''' Actual Field name is Edit By
            ''' </summary>
            ''' <returns></returns>
            Public Property Edit_By As String
            ''' <summary>
            ''' Actual Field name is Edit Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Edit_Date As String
            ''' <summary>
            ''' Actual Field name is Action Status
            ''' </summary>
            ''' <returns></returns>
            Public Property Action_Status As String
            ''' <summary>
            ''' Actual Field name is Action By
            ''' </summary>
            ''' <returns></returns>
            Public Property Action_By As String
            ''' <summary>
            ''' Actual Field name is Action Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Action_Date As String
            Public Property ID As String

        End Class
        <Serializable>
        Public Class CommonOriginalReturnFields
            ''' <summary>
            ''' Actual Field name is Add By
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_ADD_BY As String
            ''' <summary>
            ''' Actual Field name is Add Date
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_ADD_ON As String
            ''' <summary>
            ''' Actual Field name is Edit By
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_EDIT_BY As String
            ''' <summary>
            ''' Actual Field name is Edit Date
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_EDIT_ON As String
            ''' <summary>
            ''' Actual Field name is Action Status
            ''' </summary>
            ''' <returns></returns>
            Public Property ACTION_STATUS As String
            ''' <summary>
            ''' Actual Field name is Action By
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_STATUS_BY As String
            ''' <summary>
            ''' Actual Field name is Action Date
            ''' </summary>
            ''' <returns></returns>
            Public Property REC_STATUS_ON As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetAllItems
            Public Property Item As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetItemDocuments
            Public Property Item_Name As String
            Public Property Document_Name As String
            Public Property Screen_Name As String
        End Class

        Public Sub New(ByVal _cBase As Common)
            cBase = _cBase
            '_open_Cen_ID = cBase._open_Cen_ID
            '_open_User_ID = cBase._open_User_ID
            Try 'Commented for serialization. as now all objects are newly created 
                'System.Net.ServicePointManager.Expect100Continue = False
                'RealService = New RealTimeService.ConnectOneWS()
                'RealService.Url = cBase._RealTime_Server
                'RealService.Timeout = 999999999
            Catch ex As Exception
                DevExpress.XtraEditors.XtraMessageBox.Show(Messages.CouldNotContactServer, "Could not Contact Server!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub
        Public Function NewRealService(ByVal _cBase As Common) As RealTimeService.ConnectOneWS
            Dim RealServiceObj As RealTimeService.ConnectOneWS = New RealTimeService.ConnectOneWS()
            Try
                System.Net.ServicePointManager.Expect100Continue = False
                RealServiceObj.Url = cBase._RealTime_Server
                RealServiceObj.Timeout = 999999999
                RealServiceObj.AllowAutoRedirect = True
            Catch ex As Exception
                DevExpress.XtraEditors.XtraMessageBox.Show(Messages.CouldNotContactServer, "Could not Contact Server!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            Return RealServiceObj
        End Function

#Region "Main Web Service Interaction Library"

        ''' <summary>
        '''  Mark as Locked by ID, For system and data tables only , To be removed
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <param name="ConStr"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLocked(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLocked(tablename, Rec_Id, GetBaseParams(screen))
                End Select
                Return True

            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLocked", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLocked(Rec_Id, tablename, screen, ConStr)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Locked by ID, For system and data tables only , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLocked(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLocked(tablename, Rec_Id, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLocked", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLocked(Rec_Id, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Locked by Custom column, For system and data tables only , To be removed
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <param name="ConStr"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLocked(ByVal ConditionColumnName As String, ByVal ConditionColumnValue As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLocked(tablename, ConditionColumnName, ConditionColumnValue, GetBaseParams(screen))
                        'Case Common.DbConnectionMode.Local
                        '    Dim trans As OleDbTransaction = Nothing
                        '    Using Con As New OleDbConnection(ConStr)
                        '        Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
                        '        Try
                        '            trans = Con.BeginTransaction
                        '            command.Connection = Con
                        '            command.Transaction = trans
                        '            Dim STR1 As String = " UPDATE " + tablename.ToString() + " SET " & _
                        '                                            "REC_STATUS        =" & Common_Lib.Common.Record_Status._Locked & "," & _
                        '                                            "REC_STATUS_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
                        '                                            "REC_EDIT_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
                        '                                            "REC_EDIT_BY     ='" & cBase._open_User_ID & "'," & _
                        '                                            "REC_STATUS_BY     ='" & cBase._open_User_ID & "'  " & _
                        '                                            "  WHERE " & ConditionColumnName & "    ='" & ConditionColumnValue & "'"
                        '            command.CommandText = STR1 : command.ExecuteNonQuery()
                        '            trans.Commit()
                        '        Catch ex As Exception
                        '            trans.Rollback()
                        '            Throw ex
                        '        End Try
                        '    End Using
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLocked", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLocked(ConditionColumnName, ConditionColumnValue, tablename, screen, ConStr, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Locked by Custom column, For system and data tables only ,Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLocked(ByVal ConditionColumnName As String, ByVal ConditionColumnValue As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLocked(tablename, ConditionColumnName, ConditionColumnValue, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLocked", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLocked(ConditionColumnName, ConditionColumnValue, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Locked by Custom column, For system and data tables only , To be removed
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <param name="ConStr"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLockedCustom(ByVal Condition As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLockedCustom(tablename, Condition, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLockedCustom", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLockedCustom(Condition, tablename, screen, ConStr, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Locked by Custom column, For system and data tables only , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsLockedCustom(ByVal Condition As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsLockedCustom(tablename, Condition, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsLockedCustom", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLockedCustom(Condition, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Complete by ID, For system and data tables only , To be removed
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <param name="ConStr"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsComplete(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsComplete(tablename, Rec_Id, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsComplete", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLockedCustom(Rec_Id, tablename, screen, ConStr, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Complete by ID, For system and data tables only , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsComplete(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsComplete(tablename, Rec_Id, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsComplete", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsLockedCustom(Rec_Id, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Complete by Custom column, For system and data tables only  , To be removed
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <param name="ConStr"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsComplete(ByVal ConditionColumnName As String, ByVal ConditionColumnValue As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsComplete(tablename, ConditionColumnName, ConditionColumnValue, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsComplete", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsComplete(ConditionColumnName, ConditionColumnValue, tablename, screen, ConStr, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        '''  Mark as Complete by Custom column, For system and data tables only  , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function MarkAsComplete(ByVal ConditionColumnName As String, ByVal ConditionColumnValue As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.MarkAsComplete(tablename, ConditionColumnName, ConditionColumnValue, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "MarkAsComplete", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return MarkAsComplete(ConditionColumnName, ConditionColumnValue, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        'Protected Function DeleteRecord(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String) As Boolean
        '    Try
        '        Dim trans As OleDbTransaction = Nothing
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.Delete(cBase._open_User_ID, tablename, cBase._open_Cen_ID, Rec_Id, screen, cBase._PC_ID, cBase._Current_Version)
        '                'Case Common.DbConnectionMode.Local
        '                '    Using Con As New OleDbConnection(ConStr)
        '                '        Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
        '                '        trans = Con.BeginTransaction
        '                '        command.Connection = Con
        '                '        command.Transaction = trans
        '                '        Dim STR1 As String = " UPDATE " + tablename.ToString + " SET " & _
        '                '                                       "REC_STATUS        =" & Common_Lib.Common.Record_Status._Deleted & "," & _
        '                '                                            "REC_STATUS_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
        '                '                                            "REC_EDIT_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
        '                '                                            "REC_EDIT_BY     ='" & cBase._open_User_ID & "'," & _
        '                '                                            "REC_STATUS_BY     ='" & cBase._open_User_ID & "'  " & _
        '                '                                      "  WHERE REC_ID    ='" & Rec_Id & "'"
        '                '        command.CommandText = STR1 : command.ExecuteNonQuery()
        '                '        trans.Commit()
        '                '    End Using
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        '''  Delete records by ID, For system and data tables only  , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function DeleteRecord(ByVal Rec_Id As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim trans As OleDbTransaction = Nothing
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.Delete(tablename, Rec_Id, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "DeleteRecord", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return DeleteRecord(Rec_Id, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        '
        'Protected Function Delete_SO_Table_Record(ByVal Query As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.Common_Lib.RealTimeService.ClientScreen) As Boolean
        '    Try
        '        Dim trans As OleDbTransaction = Nothing
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.Delete_SO_Table_Record(tablename, Query, GetBaseParams(screen))
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        '''  Delete records by Query, For SO tables only , Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function Delete_SO_Table_Record(ByVal FunctionCalled As RealServiceFunctions, ByVal screen As ClientScreen, Optional ByVal Parameter As Object = Nothing, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim trans As OleDbTransaction = Nothing
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        If Parameter Is Nothing Then
                            _RealService.Wrap_Delete_SO_Table_Record(FunctionCalled, GetBaseParams(screen))
                        Else
                            _RealService.Wrap_Delete_SO_Table_Record(FunctionCalled, GetBaseParams(screen), Parameter)
                        End If
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "Delete_SO_Table_Record", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return Delete_SO_Table_Record(FunctionCalled, screen, Parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function


        'Protected Function DeleteByCondition(ByVal ConditionQuery As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.Common_Lib.RealTimeService.ClientScreen, ByVal ConStr As String) As Boolean
        '    Try
        '        Dim trans As OleDbTransaction = Nothing
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.DeleteByCondition(tablename, ConditionQuery, GetBaseParams(screen))
        '                'Case Common.DbConnectionMode.Local
        '                '    Using Con As New OleDbConnection(ConStr)
        '                '        Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
        '                '        trans = Con.BeginTransaction
        '                '        command.Connection = Con
        '                '        command.Transaction = trans
        '                '        Dim STR1 As String = " UPDATE " + tablename.ToString + " SET " & _
        '                '                                     "REC_STATUS        =" & Common_Lib.Common.Record_Status._Deleted & "," & _
        '                '                                            "REC_STATUS_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
        '                '                                            "REC_EDIT_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," & _
        '                '                                            "REC_EDIT_BY     ='" & cBase._open_User_ID & "'," & _
        '                '                                            "REC_STATUS_BY     ='" & cBase._open_User_ID & "'  " & _
        '                '                                      "  WHERE " & ConditionQuery
        '                '        command.CommandText = STR1 : command.ExecuteNonQuery()
        '                '        trans.Commit()
        '                '    End Using
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        ''' Delete records by a condition, For system and data tables only , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="ConditionQuery"></param>
        ''' <param name="tablename"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function DeleteByCondition(ByVal ConditionQuery As String, ByVal tablename As RealTimeService.Tables, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional Retry As Boolean = False) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim trans As OleDbTransaction = Nothing
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.DeleteByCondition(tablename, ConditionQuery, GetBaseParams(screen))
                End Select
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "DeleteByCondition", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return DeleteByCondition(ConditionQuery, tablename, screen, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record Status , to be removed
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="CenIDColName"></param>
        ''' <param name="ConStr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordStatus(ByVal Rec_Id As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal CenIDColName As String, ByVal ConStr As String, Optional Retry As Boolean = False) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim SQL_STR As String = ""
                Dim RetObj As Object = 0
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        ' SQL_STR = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND REC_ID  = '" & Rec_Id & "' "
                        RetObj = _RealService.Wrap_GetRecordStatus(GetBaseParams(screen), tableName, CenIDColName, Rec_Id)
                        'Case Common.DbConnectionMode.Local
                        '    Using T As New OleDbConnection(ConStr)
                        '        T.Open() : Dim command As OleDbCommand = T.CreateCommand()
                        '        command.CommandText = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND REC_ID  = '" & Rec_Id & "' "
                        '        RetObj = command.ExecuteScalar()
                        '    End Using
                End Select
                If RetObj Is Nothing Then
                    Return Nothing
                ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
                    Return System.DBNull.Value
                Else
                    Return RetObj
                End If
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordStatus", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordStatus(Rec_Id, screen, tableName, CenIDColName, ConStr, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record Status , manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="CenIDColName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordStatus(ByVal Rec_Id As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal CenIDColName As String, Optional Retry As Boolean = False) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim SQL_STR As String = ""
                Dim RetObj As Object = 0
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        ' SQL_STR = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND REC_ID  = '" & Rec_Id & "' "
                        RetObj = _RealService.Wrap_GetRecordStatus(GetBaseParams(screen), tableName, CenIDColName, Rec_Id)
                End Select
                If RetObj Is Nothing Then
                    Return Nothing
                ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
                    Return System.DBNull.Value
                Else
                    Return RetObj
                End If
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordStatus", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordStatus(Rec_Id, screen, tableName, CenIDColName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetRecordStatus(ByVal Rec_Id As String, ByVal RecIDColumnName As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal CenIDColName As String, ByVal ConStr As String) As Object
        '    Try
        '        Dim SQL_STR As String = ""
        '        Dim RetObj As Object = 0
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                'SQL_STR = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND " & RecIDColumnName & "  = '" & Rec_Id & "' "
        '                RetObj = RealService.Wrap_GetRecordStatus(screen, cBase._open_User_ID, cBase._open_Cen_ID, cBase._PC_ID, cBase._Current_Version, cBase._open_Year_ID, tableName, CenIDColName, RecIDColumnName, Rec_Id)
        '                'Case Common.DbConnectionMode.Local
        '                '    Using T As New OleDbConnection(ConStr)
        '                '        T.Open() : Dim command As OleDbCommand = T.CreateCommand()
        '                '        command.CommandText = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND REC_ID  = '" & Rec_Id & "' "
        '                '        RetObj = command.ExecuteScalar()
        '                '    End Using
        '        End Select
        '        If RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
        '            Return System.DBNull.Value
        '        Else
        '            Return RetObj
        '        End If
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get Record Status, Manipulated , No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="CenIDColName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordStatus(ByVal Rec_Id As String, ByVal RecIDColumnName As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal CenIDColName As String, Optional Retry As Boolean = False) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim SQL_STR As String = ""
                Dim RetObj As Object = 0
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        'SQL_STR = "SELECT REC_STATUS FROM " & tableName.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND " & RecIDColumnName & "  = '" & Rec_Id & "' "
                        RetObj = _RealService.Wrap_GetRecordStatus(GetBaseParams(screen), tableName, CenIDColName, RecIDColumnName, Rec_Id)
                End Select
                If RetObj Is Nothing Then
                    Return Nothing
                ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
                    Return System.DBNull.Value
                Else
                    Return RetObj
                End If
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordStatus", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordStatus(Rec_Id, RecIDColumnName, screen, tableName, CenIDColName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record By RecID, for System and Data tables , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="folderName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByID(ByVal Rec_ID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim RetObj As DataTable = Nothing
                'Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & Rec_ID & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        RetObj = Decompress_Data(_RealService.Wrap_GetRecordByID(GetBaseParams(screen), tableName, Rec_ID))
                    Case Else
                        RetObj = Nothing
                End Select
                Return RetObj
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByID", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByID(Rec_ID, screen, tableName, folderName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Common Function to Get Record By RecID, for System and Data tables , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Txn_Rec_ID"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="folderName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByTxn_Rec_ID(ByVal Txn_Rec_ID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim RetObj As DataTable = Nothing
                'Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & Rec_ID & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        RetObj = Decompress_Data(_RealService.Wrap_GetRecordByID_SplVchrRef(GetBaseParams(screen), tableName, Txn_Rec_ID))
                    Case Else
                        RetObj = Nothing
                End Select
                Return RetObj
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByTxn_Rec_ID", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByTxn_Rec_ID(Txn_Rec_ID, screen, tableName, folderName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record By RecID for Current Center, For System and Data Folders , Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="folderName"></param>
        ''' <param name="CenIDColName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByID(ByVal Rec_ID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, ByVal CenIDColName As String, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & cBase._open_Cen_ID & "' AND REC_ID= '" & Rec_ID & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return Decompress_Data(_RealService.Wrap_GetRecordByID(GetBaseParams(screen), tableName, Rec_ID))
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByID", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByID(Rec_ID, screen, tableName, folderName, CenIDColName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record By Custom Column for Current Center, For Sys/Data Folders, to be removed
        ''' </summary>
        ''' <param name="ConditionColumnName"></param>
        ''' <param name="ConditionValue"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <param name="folderName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByColumn(ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                ' Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        'Return RealService.GetSingleRecord(cBase._open_User_ID, tableName, cBase._open_Cen_ID, SQL_STR, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
                        Return Decompress_Data(_RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, tableName.ToString(), ConditionColumnName, ConditionValue))
                        'Case Common.DbConnectionMode.Local
                        '    Dim d1 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), SQL_STR)
                        '    Return d1._dc_DataTable
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByColumn", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByColumn(ConditionColumnName, ConditionValue, screen, tableName, folderName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record By Custom Column for Current Center, For Sys/Data Folders, Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="ConditionColumnName"></param>
        ''' <param name="ConditionValue"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByColumn(ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                ' Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        'Return RealService.GetSingleRecord(cBase._open_User_ID, tableName, cBase._open_Cen_ID, SQL_STR, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
                        Return Decompress_Data(_RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, tableName.ToString(), ConditionColumnName, ConditionValue))
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByColumn", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByColumn(ConditionColumnName, ConditionValue, screen, tableName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Record By Custom Column for Current Center, For Sys/Data Folders, Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="ConditionColumnName"></param>
        ''' <param name="ConditionValue"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByColumn(ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal tableDefinedName As String, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                ' Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'"
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return Decompress_Data(_RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, tableDefinedName, ConditionColumnName, ConditionValue))
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByColumn", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByColumn(ConditionColumnName, ConditionValue, screen, tableName, tableDefinedName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetRecordByColumn(ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode) As DataTable
        '    Try
        '        'Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
        '        '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                'Return RealService.GetSingleRecord(cBase._open_User_ID, tableName, cBase._open_Cen_ID, SQL_STR, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
        '                Return RealService.Wrap_GetRecordByColumn(screen, cBase._open_User_ID, cBase._open_Cen_ID, cBase._PC_ID, cBase._Current_Version, cBase._open_Year_ID, tableName, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value)
        '                'Case Common.DbConnectionMode.Local
        '                '    Dim d1 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), SQL_STR)
        '                '    Return d1._dc_DataTable
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get Record By 2 Custom Columns, For System and Data Folders, Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="ConditionColumnName"></param>
        ''' <param name="ConditionValue"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByColumn(ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                'Dim SQL_STR As String = " SELECT * FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return Decompress_Data(_RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value))
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByColumn", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByColumn(ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value, screen, tableName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetRecordByColumn(ByVal SelectedColumns As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode) As DataTable
        '    Try
        '        'Dim SQL_STR As String = " SELECT " & SelectedColumns & " FROM " & tableName.ToString() & " " & _
        '        '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                'Return RealService.GetSingleRecord(cBase._open_User_ID, tableName, cBase._open_Cen_ID, SQL_STR, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
        '                Return RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, SelectedColumns, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value)
        '                'Case Common.DbConnectionMode.Local
        '                '    Dim d1 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), SQL_STR)
        '                '    Return d1._dc_DataTable
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get Specific Cols for 2 column filter, For System and Data Folders, Manipulated, No Need to create Server Structure
        ''' </summary>
        ''' <param name="ConditionColumnName"></param>
        ''' <param name="ConditionValue"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByColumn(ByVal SelectedColumns As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                'Dim SQL_STR As String = " SELECT " & SelectedColumns & " FROM " & tableName.ToString() & " " & _
                '                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return Decompress_Data(_RealService.Wrap_GetRecordByColumn(GetBaseParams(screen), tableName, SelectedColumns, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value))
                        'Case Common.DbConnectionMode.Local
                        '    Dim d1 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), SQL_STR)
                        '    Return d1._dc_DataTable
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByColumn", "Unable to connect to the remote server(" & cBase._RealTime_Server & "), Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByColumn(SelectedColumns, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value, screen, tableName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        ''' <summary>
        ''' Common Function to Get Record By Custom Condition
        ''' </summary>
        ''' <param name="CustomCondition"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetRecordByCustom(ByVal CustomCondition As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return Decompress_Data(_RealService.Wrap_GetRecordByCustom(GetBaseParams(screen), tableName, CustomCondition))
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetRecordByCustom", "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetRecordByCustom(CustomCondition, screen, tableName, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetListOfRecords(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode) As DataTable
        '    Try
        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                Return RealService.List(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '            Case Common.DbConnectionMode.Local
        '                Dim d3 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), localQuery)
        '                Return d3._dc_DataTable : d3._dc_Connection.Close()
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get List , For Data and System, Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function GetDataListOfRecords(ByVal FunctionCalled As RealServiceFunctions, ByVal screen As ClientScreen, Optional ByVal Parameter As Object = Nothing, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim RetTable As DataTable = Nothing
                If IsSecureConnRequired(FunctionCalled) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        If Parameter Is Nothing Then
                            RetTable = Decompress_Data(_RealService.Wrap_List(FunctionCalled, GetBaseParams(screen)))
                        Else
                            RetTable = Decompress_Data(_RealService.Wrap_List(FunctionCalled, GetBaseParams(screen), Parameter))
                        End If
                    Case Else
                        RetTable = Nothing
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return RetTable
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetDataListOfRecords:" & FunctionCalled.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetDataListOfRecords(FunctionCalled, screen, Parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetListOfRecords(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, ByVal CenID As String) As DataTable
        '    Try
        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                Return RealService.List(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen, CenID))
        '            Case Common.DbConnectionMode.Local
        '                Dim d3 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), localQuery)
        '                Return d3._dc_DataTable : d3._dc_Connection.Close()
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get List , For Data and System with Custom CenID,Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetDataListOfRecordsWithCenID(ByVal FunctionCalled As RealServiceFunctions, ByVal screen As ClientScreen, ByVal CenID As String, Optional ByVal Parameter As Object = Nothing, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim retTable As DataTable = Nothing
                If IsSecureConnRequired(FunctionCalled) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        If Parameter Is Nothing Then
                            retTable = Decompress_Data(_RealService.Wrap_List(FunctionCalled, GetBaseParams(screen, CenID)))
                        Else
                            retTable = Decompress_Data(_RealService.Wrap_List(FunctionCalled, GetBaseParams(screen, CenID), Parameter))
                        End If
                    Case Else
                        retTable = Nothing
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return retTable
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetDataListOfRecordsWithCenID" & FunctionCalled.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetDataListOfRecordsWithCenID(FunctionCalled, screen, CenID, Parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetListOfRecords(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables) As DataTable
        '    Try
        '        Select Case (cBase.curr_Db_Conn_Mode(screen, tableName))
        '            Case Common.DbConnectionMode.Online
        '                Return RealService.List(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '            Case Common.DbConnectionMode.Local
        '                Dim d3 As New Common_Lib.Get_Data(cBase, Common.ClientDBFolderCode.CORE.ToString(), tableName.ToString(), localQuery)
        '                Return d3._dc_DataTable : d3._dc_Connection.Close()
        '            Case Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                Try
        '                    Dim d3 As New Common_Lib.Get_Data(cBase, Common.ClientDBFolderCode.CORE.ToString(), tableName.ToString(), localQuery)
        '                    Return d3._dc_DataTable : d3._dc_Connection.Close()
        '                Catch ex As Exception
        '                    If ShowCoreNotPresentMsg(tableName) Then cBase.FlashAlert(Messages.CoreNotWorkingMsg)
        '                    Return RealService.List(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '                End Try
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' New Common Function to Get List, For Core Tables Only, Manipulated
        ''' </summary>
        ''' <param name="localQuery"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCoreListOfRecords(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal LocalQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional ByVal parameter As Object = Nothing, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case (cBase.curr_Db_Conn_Mode(screen, tableName))
                    Case Common.DbConnectionMode.Online
                        If IsInCache(OnlineFunction, screen) Then
                            Return Check_Fill_CoreDataInCache(OnlineFunction, screen, tableName, parameter)
                        End If
                        Return OnlineDBOperations.CallCoreListFunctions(_RealService, cBase, OnlineFunction, parameter, screen)
                    Case Common.DbConnectionMode.Local
                        Dim d3 As New Common_Lib.Get_Data(cBase, Common.ClientDBFolderCode.CORE.ToString(), tableName.ToString(), LocalQuery)
                        Return d3._dc_DataTable : d3._dc_Connection.Close()
                    Case Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return OnlineDBOperations.CallCoreListFunctions(_RealService, cBase, OnlineFunction, parameter, screen)
                        'Try
                        '    Dim d3 As New Common_Lib.Get_Data(cBase, Common.ClientDBFolderCode.CORE.ToString(), tableName.ToString(), LocalQuery)
                        '    Return d3._dc_DataTable : d3._dc_Connection.Close()
                        'Catch ex As Exception
                        '    If ShowCoreNotPresentMsg(tableName) Then cBase.FlashAlert(Messages.CoreNotWorkingMsg)
                        '    If IsInCache(OnlineFunction, screen) Then
                        '        Return Check_Fill_CoreDataInCache(OnlineFunction, screen, tableName, parameter)
                        '    End If
                        '    Return OnlineDBOperations.CallCoreListFunctions(RealService, cBase, OnlineFunction, parameter, screen)
                        'End Try
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetCoreListOfRecords" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetCoreListOfRecords(OnlineFunction, LocalQuery, screen, tableName, parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        Private Function IsInCache(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Boolean
            If OnlineFunction = RealServiceFunctions.Core_GetItems_Ledger_Common Or OnlineFunction = RealServiceFunctions.Core_GetItems_Ledger Or OnlineFunction = RealServiceFunctions.Core_GetOpeningProfileItems Then
                Return True
            End If
            If OnlineFunction = RealServiceFunctions.Core_GetItemsByQuery_Common Then
                If screen = ClientScreen.Accounts_Voucher_FD Or screen = ClientScreen.Profile_OpeningBalances Then
                    Return True
                End If
            End If
        End Function



        'Protected Function GetListOfRecordsBySP(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal folderName As Common.ClientDBFolderCode, _
        '                                        ByVal parameters As String(), ByVal values As Object(), ByVal types As System.Data.DbType(), ByVal length As Int32()) As DataTable
        '    Try
        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                Return RealService.ListFromSP(cBase._open_User_ID, tableName, cBase._open_Cen_ID, OnlineQuery, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version, parameters, values, types, length)
        '                'Case Common.DbConnectionMode.Local
        '                '    Dim d3 As New Common_Lib.Get_Data(cBase, folderName.ToString(), tableName.ToString(), localQuery)
        '                '    Return d3._dc_DataTable : d3._dc_Connection.Close()
        '            Case Else
        '                Return Nothing
        '        End Select
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Get List from SP, For Data and System, manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetListOfRecordsBySP(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional ByVal inParam As Object = Nothing, Optional Retry As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim retTable As DataTable = Nothing
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        If inParam Is Nothing Then
                            retTable = Decompress_Data(_RealService.Wrap_GetListBySP(OnlineFunction, GetBaseParams(screen)))
                        Else
                            retTable = Decompress_Data(_RealService.Wrap_GetListBySP(OnlineFunction, GetBaseParams(screen), inParam))
                        End If
                    Case Else
                        retTable = Nothing
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return retTable
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetListOfRecordsBySP" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetListOfRecordsBySP(OnlineFunction, screen, inParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Scalar value from SP, 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetScalarBySP(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal inParam As Object, Optional Retry As Boolean = False) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Return _RealService.Wrap_GetScalarBySP(OnlineFunction, GetBaseParams(screen), inParam)
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetScalarBySP" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetScalarBySP(OnlineFunction, screen, inParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Common Function to Get Dataset from SP, For Data and System
        ''' </summary>
        ''' <param name="OnlineFunction"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetDatasetOfRecordsBySP(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal inParam As Object, Optional Retry As Boolean = False) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim retSet As DataSet = Nothing
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        retSet = Decompress_Data_Dataset(_RealService.Wrap_GetListDatasetBySP(OnlineFunction, GetBaseParams(screen), inParam))
                    Case Else
                        retSet = Nothing
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return retSet
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetDatasetOfRecordsBySP" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetDatasetOfRecordsBySP(OnlineFunction, screen, inParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetSingleValue(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables) As Object
        '    Dim RetObj As Object
        '    Try
        '        Select Case (cBase.curr_Db_Conn_Mode(screen, tableName))
        '            Case Common.DbConnectionMode.Online
        '                RetObj = RealService.GetScalar(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '            Case Common.DbConnectionMode.Local
        '                Using T As New OleDbConnection(cBase._data_ConStr_Core)
        '                    T.Open() : Dim command As OleDbCommand = T.CreateCommand()
        '                    command.CommandText = localQuery
        '                    RetObj = command.ExecuteScalar()
        '                End Using
        '            Case Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                Try
        '                    Using T As New OleDbConnection(cBase._data_ConStr_Core)
        '                        T.Open() : Dim command As OleDbCommand = T.CreateCommand()
        '                        command.CommandText = localQuery
        '                        RetObj = command.ExecuteScalar()
        '                    End Using
        '                Catch ex As Exception
        '                    If ShowCoreNotPresentMsg(tableName) Then cBase.FlashAlert(Messages.CoreNotWorkingMsg)
        '                    RetObj = RealService.GetScalar(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '                End Try
        '            Case Else
        '                RetObj = Nothing
        '        End Select
        '        If RetObj Is Nothing Then
        '            Return System.DBNull.Value
        '        ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
        '            Return System.DBNull.Value
        '        Else
        '            Return RetObj
        '        End If
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Get Scalar Value, for Core Tables , Manipulated
        ''' </summary>
        ''' <param name="localQuery"></param>
        ''' <param name="screen"></param>
        ''' <param name="tableName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetSingleValue_Core(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional ByVal parameter As Object = Nothing, Optional Retry As Boolean = False) As Object
            Dim RetObj As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case (cBase.curr_Db_Conn_Mode(screen, tableName))
                    Case Common.DbConnectionMode.Online
                        ' RetObj = _RealService.GetScalar(cBase._open_User_ID, tableName, cBase._open_Cen_ID, OnlineQuery, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
                        RetObj = OnlineDBOperations.CallCoreSingleValueFunctions(_RealService, cBase, OnlineFunction, parameter, screen)
                    Case Common.DbConnectionMode.Local
                        Using T As New OleDbConnection(cBase._data_ConStr_Core)
                            T.Open() : Dim command As OleDbCommand = T.CreateCommand()
                            command.CommandText = localQuery
                            RetObj = command.ExecuteScalar()
                        End Using
                    Case Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Try
                            Using T As New OleDbConnection(cBase._data_ConStr_Core)
                                T.Open() : Dim command As OleDbCommand = T.CreateCommand()
                                command.CommandText = localQuery
                                RetObj = command.ExecuteScalar()
                            End Using
                        Catch ex As Exception
                            If ShowCoreNotPresentMsg(tableName) Then cBase.FlashAlert(Messages.CoreNotWorkingMsg)
                            'RetObj = _RealService.GetScalar(cBase._open_User_ID, tableName, cBase._open_Cen_ID, OnlineQuery, tableName.ToString(), screen, cBase._PC_ID, cBase._Current_Version)
                            RetObj = OnlineDBOperations.CallCoreSingleValueFunctions(_RealService, cBase, OnlineFunction, parameter, screen)
                        End Try
                    Case Else
                        RetObj = Nothing
                End Select
                If RetObj Is Nothing Then
                    Return System.DBNull.Value
                ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
                    Return System.DBNull.Value
                Else
                    Return RetObj
                End If
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetSingleValue_Core" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetSingleValue_Core(OnlineFunction, localQuery, screen, tableName, parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
                'CommonExceptionCall(ex)
                'Return Nothing
            End Try
        End Function


        'Protected Function GetSingleValue(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal ConStr As String) As Object
        '    Dim RetObj As Object
        '    Try

        '        Select Case (cBase.curr_Db_Conn_Mode(screen))
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RetObj = RealService.GetScalar(tableName, OnlineQuery, tableName.ToString(), GetBaseParams(screen))
        '            Case Common.DbConnectionMode.Local
        '                Using T As New OleDbConnection(ConStr)
        '                    T.Open() : Dim command As OleDbCommand = T.CreateCommand()
        '                    command.CommandText = localQuery
        '                    RetObj = command.ExecuteScalar()
        '                End Using
        '            Case Else
        '                RetObj = Nothing
        '        End Select
        '        If RetObj Is Nothing Then
        '            Return System.DBNull.Value
        '        ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
        '            Return System.DBNull.Value
        '        Else
        '            Return RetObj
        '        End If
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' Get Scalar Value, for System,Data Tables , Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetSingleValue_Data(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional ByVal Parameter As Object = Nothing, Optional Retry As Boolean = False) As Object
            Dim RetObj As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Select Case (cBase.curr_Db_Conn_Mode(screen))
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        If Parameter Is Nothing Then
                            RetObj = _RealService.Wrap_GetSingleValue(OnlineFunction, GetBaseParams(screen))
                        Else
                            RetObj = _RealService.Wrap_GetSingleValue(OnlineFunction, GetBaseParams(screen), Parameter)
                        End If
                    Case Else
                        RetObj = Nothing
                End Select
                If RetObj Is Nothing Then
                    Return System.DBNull.Value
                ElseIf RetObj.GetType().FullName.ToLower().Contains("realtimeservice.dbnull") Then
                    Return System.DBNull.Value
                Else
                    Return RetObj
                End If
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "GetSingleValue_Data" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return GetSingleValue_Data(OnlineFunction, screen, Parameter, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return Nothing
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return Nothing
                End If
            End Try
        End Function

        ' ''' <summary>
        ' '''  Common Function to Insert Data,For Data and System Tables, to be removed
        ' ''' </summary>
        ' ''' <param name="OnlineQuery"></param>
        ' ''' <param name="localQuery"></param>
        ' ''' <param name="screen"></param>
        ' ''' <param name="tableName"></param>
        ' ''' <param name="ConStr"></param>
        ' ''' <remarks></remarks>
        'Protected Function InsertRecord(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal ConStr As String, ByVal RecID As String) As Boolean
        '    Dim STR1 As String = ""
        '    Try
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.Insert(cBase._open_User_ID, tableName, cBase._open_Cen_ID, OnlineQuery, screen, cBase._PC_ID, cBase._Current_Version, RecID)
        '            Case Common.DbConnectionMode.Local
        '                Dim trans As OleDbTransaction = Nothing
        '                Using Con As New OleDbConnection(ConStr)
        '                    Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
        '                    trans = Con.BeginTransaction
        '                    command.Connection = Con
        '                    command.Transaction = trans
        '                    command.CommandText = localQuery : command.ExecuteNonQuery()
        '                    trans.Commit()
        '                End Using
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        '''  Common Function to Insert Data,For Data and System Tables, Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function InsertRecord(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal InParam As Object, Optional Retry As Boolean = False) As Boolean
            Dim STR1 As String = ""
            Dim ret As Boolean = Nothing
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        ret = _RealService.Wrap_Insert(OnlineFunction, GetBaseParams(screen), InParam)
                    Case Else
                        ret = Nothing
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return ret
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "InsertRecord" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return InsertRecord(OnlineFunction, screen, InParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ' ''' <summary>
        ' '''  Common Function to Insert Data,For Data and System Tables with custom open centerID, to be removed
        ' ''' </summary>
        ' ''' <param name="OnlineQuery"></param>
        ' ''' <param name="localQuery"></param>
        ' ''' <param name="screen"></param>
        ' ''' <param name="tableName"></param>
        ' ''' <param name="ConStr"></param>
        ' ''' <remarks></remarks>
        'Protected Function InsertRecord(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal ConStr As String, ByVal CenID As String, ByVal RecID As String) As Boolean
        '    Dim STR1 As String = ""
        '    Try
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.Insert(cBase._open_User_ID, tableName, CenID, OnlineQuery, screen, cBase._PC_ID, cBase._Current_Version, RecID)
        '            Case Common.DbConnectionMode.Local
        '                Dim trans As OleDbTransaction = Nothing
        '                Using Con As New OleDbConnection(ConStr)
        '                    Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
        '                    trans = Con.BeginTransaction
        '                    command.Connection = Con
        '                    command.Transaction = trans
        '                    command.CommandText = localQuery : command.ExecuteNonQuery()
        '                    trans.Commit()
        '                End Using
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        '''  Common Function to Insert Data,For Data and System Tables with Custom Centre ID, Manipulated
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function InsertRecord(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal CustomCentreID As String, ByVal InParam As Object, Optional Retry As Boolean = False) As Boolean
            Dim STR1 As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.Wrap_Insert(OnlineFunction, GetBaseParams(screen, CustomCentreID), InParam)
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "InsertRecord" & OnlineFunction.ToString, "Unable to connect to the remote server(" & cBase._RealTime_Server & "),Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return InsertRecord(OnlineFunction, screen, CustomCentreID, InParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        ' ''' <summary>
        ' ''' Common Function to Update Data, For Data and System Tables, to be removed
        ' ''' </summary>
        ' ''' <param name="OnlineQuery"></param>
        ' ''' <param name="localQuery"></param>
        ' ''' <param name="screen"></param>
        ' ''' <param name="tableName"></param>
        ' ''' <param name="ConStr"></param>
        ' ''' <remarks></remarks>
        'Protected Function UpdateRecord(ByVal OnlineQuery As String, ByVal localQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, ByVal ConStr As String) As Boolean
        '    Dim STR1 As String = ""
        '    Try
        '        Select Case cBase.curr_Db_Conn_Mode(screen)
        '            Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        '                RealService.Update(cBase._open_User_ID, tableName, cBase._open_Cen_ID, OnlineQuery, screen, cBase._PC_ID, cBase._Current_Version)
        '            Case Common.DbConnectionMode.Local
        '                Dim trans As OleDbTransaction = Nothing
        '                Using Con As New OleDbConnection(ConStr)
        '                    Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
        '                    trans = Con.BeginTransaction
        '                    command.Connection = Con
        '                    command.Transaction = trans
        '                    command.CommandText = localQuery : command.ExecuteNonQuery()
        '                    trans.Commit()
        '                End Using
        '        End Select
        '        Return True
        '    Catch ex As Exception
        '        CommonExceptionCall(ex)
        '        Return False
        '    End Try
        'End Function

        ''' <summary>
        ''' Common Function to Update Data, For Data and System Tables,Manipulated
        ''' </summary>
        Protected Function UpdateRecord(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal UpParam As Object, Optional Retry As Boolean = False) As Boolean
            Dim STR1 As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        _RealService.Wrap_Update(OnlineFunction, GetBaseParams(screen), UpParam)
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return True
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "UpdateRecord" & OnlineFunction.ToString, "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return UpdateRecord(OnlineFunction, screen, UpParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

        Protected Function ExecuteGroup(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal UpParam As Object, Optional Retry As Boolean = False) As Object
            Dim STR1 As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim ReturnVar As Object = Nothing
            Try
                If IsSecureConnRequired(OnlineFunction) Then
                    _RealService.Url = ConvertToSecure(_RealService)
                End If
                Select Case cBase.curr_Db_Conn_Mode(screen)
                    Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        ReturnVar = _RealService.Wrap_ExecuteFunction(OnlineFunction, GetBaseParams(screen), UpParam)
                End Select
                _RealService.Url = ConvertToRegular(_RealService.Url)
                Return ReturnVar
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "ExecuteGroup" & OnlineFunction.ToString, "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return ExecuteGroup(OnlineFunction, screen, UpParam, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function
        Protected Function InsertBySPPublic(Table As Tables, SP As String, paramters As String(), values() As Object, dbTypes() As System.Data.DbType, lengths() As Integer, screen As ClientScreen, _RealService As RealTimeService.ConnectOneWS, Optional Retry As Boolean = False)
            Try
                Return _RealService.InsertBySPPublic(Table, SP, paramters, values, dbTypes, lengths, GetBaseParams(screen))
            Catch ex As Exception
                If ex.Message = "Unable to connect to the remote server" And Not Retry Then
                    Try
                        Log.Write(Log.LogType.Error, screen.ToString(), "InsertBySPPublic", "Unable to connect to the remote server,Redirecting to " & cBase._RealTime_Server2, Log.LogSuffix.ClientApplication)
                        _RealService.Url = cBase._RealTime_Server2
                        Return InsertBySPPublic(Table, SP, paramters, values, dbTypes, lengths, screen, _RealService, True)
                    Catch ex1 As Exception
                        CommonExceptionCall(ex1)
                        Return False
                    End Try
                Else
                    CommonExceptionCall(ex)
                    Return False
                End If
                'CommonExceptionCall(ex)
                'Return False
            End Try
        End Function

#End Region

#Region "Core Table functions-Shifted"
        ''' <summary>
        ''' Returns HOEvents, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetHOEvents(ByVal Screen As ClientScreen, ByVal OnlyOpenEvents As Boolean) As DataTable
            Dim Query As String = " SELECT HE_EVENT_ID as ID ,HE_NAME as Name,HE_FROM , HE_TO , HE_LOCATION, HE_CONTACT_PERSON ,HE_CONTACT_NO,HE_CATEGORY FROM SO_HO_EVENT_INFO"
            If OnlyOpenEvents Then
                Query += " WHERE UPPER(HE_STATUS) = 'OPEN'  AND HE_COD_YEAR_ID = '" & cBase._open_Year_ID
            Else
                Query += " WHERE HE_COD_YEAR_ID = '" & cBase._open_Year_ID & "';"
            End If
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetHOEvents, Query, Screen, Tables.SO_HO_EVENT_INFO, OnlyOpenEvents)
        End Function

        ''' <summary>
        ''' Returns TDSRecords, common function, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetTDSRecords(ByVal Query As String, ByVal Screen As ClientScreen, Optional ByVal inParam As Param_GetTDSRecords_Common = Nothing) As DataTable
            ' ClientScreen.Accounts_Voucher_Payment
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetTDSRecords_Common, Query, Screen, Tables.TDS_INFO, inParam)
        End Function

        ''' <summary>
        ''' Returns Bank details, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankInfo(ByVal Screen As ClientScreen) As DataTable
            Dim Query As String = " SELECT BI_BANK_NAME,BI_BANK_PAN_NO,REC_ID  FROM BANK_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY BI_BANK_NAME  "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankInfo, Query, Screen, Tables.BANK_INFO)
        End Function

        ''' <summary>
        ''' Returns Bank details, common function, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankInfo(ByVal OnlineQuery As String, ByVal LocalQuery As String, ByVal Screen As ClientScreen, Optional ByVal inParam As Object = Nothing) As DataTable
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankInfo_Common, LocalQuery, Screen, Tables.BANK_INFO, inParam)
        End Function

        ''' <summary>
        ''' Returns Bank and branch details for specified Multiple Comma Seperated Branch Rec IDS, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBranchesForMultipleIDs(ByVal Branch_IDs As String, ByVal Screen As ClientScreen) As DataTable
            Dim Query As String = "SELECT B.BI_BANK_NAME AS Name,B.BI_BANK_PAN_NO,A.BB_BRANCH_NAME as Branch,A.BB_IFSC_CODE,A.BB_MICR_CODE, A.REC_ID AS BB_BRANCH_ID, A.BI_BANK_ID, B.BI_SHORT_NAME " &
                                         " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                         " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID IN (" & Branch_IDs & ")  ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankBranchesForMultipleIDs, Query, Screen, Tables.BANK_BRANCH_INFO, Branch_IDs)
        End Function

        ''' <summary>
        ''' Returns Bank and branch specified Columns  for specified Multiple Comma Seperated Branch Rec IDS, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBranchesForMultipleIDs(ByVal Branch_IDs As String, ByVal Screen As ClientScreen, ByVal Columns As String) As DataTable
            Dim Query As String = "SELECT " & Columns &
                                         " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                         " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID IN (" & Branch_IDs & ")  ;"
            Dim InParam As Param_GetBankBranchesForMultipleIDs = New Param_GetBankBranchesForMultipleIDs()
            InParam._BranchID = Branch_IDs
            InParam._Columns = Columns
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankBranchesForMultipleIDsWithCustomColumnNames, Query, Screen, Tables.BANK_BRANCH_INFO, InParam)
        End Function

        ''' <summary>
        ''' Returns branch details for specified Bank ID, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBranchesByBankID(ByVal BankIDs As String, ByVal Screen As ClientScreen) As DataTable
            Dim Query As String = " SELECT BB_BRANCH_NAME ,BB_IFSC_CODE  ,BB_MICR_CODE  ,REC_ID AS BB_ID   FROM BANK_BRANCH_INFO " &
                                        "WHERE BI_BANK_ID ='" & BankIDs & "' AND REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY BB_BRANCH_NAME "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankBranchesByBankID, Query, Screen, Tables.BANK_BRANCH_INFO, BankIDs)
        End Function

        ''' <summary>
        ''' Returns Bank and branch details for specified Branch Rec ID, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBranches(ByVal Branch_ID As String, ByVal Screen As ClientScreen) As DataTable
            Dim Query As String = "SELECT A.BI_BANK_ID,B.BI_BANK_PAN_NO,A.BB_IFSC_CODE,A.BB_MICR_CODE, A.REC_ID AS BB_BRANCH_ID " &
                                     " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                     " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID = '" & Branch_ID & "'  ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetBankBranches, Query, Screen, Tables.BANK_BRANCH_INFO, Branch_ID)
        End Function

        ''' <summary>
        ''' Get Misc Records by Misc ID, Shifted
        ''' </summary>
        ''' <param name="MiscId"></param>
        ''' <param name="screen"></param>
        ''' <param name="MiscNameColumnHead"></param>
        ''' <param name="RecIDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetMisc(ByVal MiscId As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            If Not MiscId.StartsWith("'") Then MiscId = "'" & MiscId
            If Not MiscId.EndsWith("'") Then MiscId = MiscId & "'"
            If (Not Remark2Filter.StartsWith("'")) And Remark2Filter.Length > 0 Then Remark2Filter = "'" & Remark2Filter
            If (Not Remark2Filter.EndsWith("'")) And Remark2Filter.Length > 0 Then Remark2Filter = Remark2Filter & "'"
            Dim Remark2Filter_Query As String = ""
            If Remark2Filter.Length > 0 Then
                Remark2Filter_Query = " AND MISC_REMARK2 IN (" & Remark2Filter & ")"
            End If
            Dim ScreenFilter As String = ""
            If screen = ClientScreen.Facility_ServiceReport Then
                ScreenFilter = " AND COALESCE(MISC_REMARK2,'') NOT IN ('Exclude From Service Report')" 'Exclude Azadi Ka Amrit Mahotsav And Occasions from project in godly service report
            End If
            Dim SQL_STR As String = " SELECT MISC_NAME as " & MiscNameColumnHead & ",REC_ID as " & RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN (" & MiscId & ") " & Remark2Filter & ScreenFilter & " order by misc_name "
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = MiscNameColumnHead
            InParam.RecIDColumnHead = RecIDColumnHead
            InParam.MISC_REMARK2 = Remark2Filter
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetMisc, SQL_STR, screen, RealTimeService.Tables.MISC_INFO, InParam)
        End Function

        ''' <summary>
        ''' Get Misc Record Details by multiple Misc ID, Shifted 
        ''' </summary>
        ''' <param name="MiscIds"></param>
        ''' <param name="screen"></param>
        ''' <param name="MiscNameColumnHead"></param>
        ''' <param name="RecIDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetMiscDetails(ByVal MiscIds As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Dim SQL_STR As String = " Select MISC_NAME As " & MiscNameColumnHead & ", MISC_ID As MASTERID, MISC_SRNO As SR, REC_ID As " & RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " And MISC_ID In (" & MiscIds & ") order by misc_name "
            Dim InParam As Param_GetMiscDetails = New Param_GetMiscDetails()
            InParam.MiscIds = MiscIds
            InParam.MiscNameColumnHead = MiscNameColumnHead
            InParam.RecIDColumnHead = RecIDColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetMiscDetails, SQL_STR, screen, RealTimeService.Tables.MISC_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Misc Record Details by multiple Misc ID, common function,Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetMiscDetails(ByVal OnlineQuery As String, ByVal LocalQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional ByVal inParam As Object = Nothing) As DataTable
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetMiscDetails_Common, LocalQuery, screen, RealTimeService.Tables.MISC_INFO, inParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Misc Records by custom Query, common function, Shifted
        ''' </summary>
        ''' <param name="OnlineQuery"></param>
        ''' <param name="LocalQuery"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetMisc_Common(ByVal OnlineQuery As String, ByVal LocalQuery As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen, Optional ByVal InParam As Object = Nothing) As DataTable
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetMisc_Common, LocalQuery, screen, RealTimeService.Tables.MISC_INFO, InParam)
        End Function

        ''' <summary>
        ''' Get Items by Item Profile, Shifted
        ''' </summary>
        ''' <param name="item_profile"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItems(ByVal item_profile As String, ByVal screen As ClientScreen, ByVal IDColumnHeadName As String, ByVal NameColumnHeadName As String) As DataTable
            Dim SQL_STR1 As String = "Select REC_ID As " & IDColumnHeadName & " , ITEM_NAME As " & NameColumnHeadName & " from ITEM_INFO where    UCASE(ITEM_PROFILE)='" & item_profile & "' AND  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
            Dim InParam As Param_GetItems_ItemProfile = New Param_GetItems_ItemProfile()
            InParam.IDColumnHeadName = IDColumnHeadName
            InParam.item_profile = item_profile
            InParam.NameColumnHeadName = NameColumnHeadName
            InParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemsByItemProfile, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get All Items, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItems(ByVal screen As ClientScreen, ByVal IDColumnHeadName As String, ByVal NameColumnHeadName As String) As DataTable
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & IDColumnHeadName & " ,ITEM_NAME as " & NameColumnHeadName & " from ITEM_INFO where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
            Dim InParam As Param_GetItemsAll = New Param_GetItemsAll()
            InParam.IDColumnHeadName = IDColumnHeadName
            InParam.NameColumnHeadName = NameColumnHeadName
            InParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemsAllItems, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Multiple Item IDS, Shifted
        ''' </summary>
        ''' <param name="ITEM_IDS"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItems(ByVal ITEM_IDS As String, ByVal screen As ClientScreen) As DataTable
            Dim SQL_STR1 As String = "SELECT REC_ID AS ID ,ITEM_NAME from ITEM_INFO where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & ITEM_IDS & ");"
            Dim InParam As Param_GetItemsByMultipleItemIDs = New Param_GetItemsByMultipleItemIDs
            InParam.iTEM_iDs = ITEM_IDS
            InParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemsByMultipleItemIDs, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Ledgers
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetLedgers(ByVal screen As ClientScreen) As DataTable
            Dim SQL_STR1 As String = "SELECT LED_ID AS ID ,LED_NAME as Name,LED_TYPE AS Type from acc_ledger_info where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LED_ID <> '00000' ;"
            If screen = ClientScreen.Report_LedgerReport Then
                SQL_STR1 = "SELECT LED_ID AS ID ,LED_NAME as Name,LED_TYPE AS Type, '' AS Sub_Led_ID from acc_ledger_info where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LED_ID NOT IN ('00000','00079') " &
                        " UNION ALL SELECT '00079', BI_SHORT_NAME + '-' + BA_ACCOUNT_NO, 'ASSET', BA.REC_ID from bank_account_info BA INNER JOIN bank_branch_info AS BR ON BA.BA_BRANCH_ID = BR.REC_ID INNER JOIN bank_info BI ON BR.BI_BANK_ID = BI.REC_ID WHERE BA_CEN_ID =" + cBase._open_Cen_ID.ToString + " AND BA_COD_YEAR_ID=" + cBase._open_Year_ID.ToString + ""
            End If
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetLedgerDetails, SQL_STR1, screen, RealTimeService.Tables.ACC_LEDGER_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by custom query, common function, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemsByQuery_Common(ByVal SQL_STR1 As String, ByVal screen As ClientScreen, Optional ByVal inParam As Param_GetItemsByQueryCommon = Nothing) As DataTable
            If inParam Is Nothing Then
                inParam = New Param_GetItemsByQueryCommon
            End If
            inParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemsByQuery_Common, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, inParam)
        End Function

        ''' <summary>
        ''' Common Function to Get TDS Code for Items 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItemTDSCode(ByVal screen As ClientScreen, ItemID As String) As DataTable
            Dim SQL_STR1 As String = "SELECT ITEM_TDS_CODE FROM ITEM_INFO where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID = '" & ItemID & "' ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemTDSCode, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, ItemID)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Item Profile, Shifted
        ''' </summary>
        ''' <param name="item_profile"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItems(ByVal item_profile As String, ByVal item_profile2 As String, ByVal screen As ClientScreen, ByVal IDColumnHeadName As String, ByVal NameColumnHeadName As String) As DataTable
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & IDColumnHeadName & " ,ITEM_NAME as " & NameColumnHeadName & " from ITEM_INFO where    UCASE(ITEM_PROFILE) IN ('" & item_profile & "','" & item_profile2 & "') AND  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
            Dim InParam As Param_GetItemsByMultipleItemProfile = New Param_GetItemsByMultipleItemProfile()
            InParam.IDColumnHeadName = IDColumnHeadName
            InParam.item_profile = item_profile
            InParam.item_profile2 = item_profile2
            InParam.NameColumnHeadName = NameColumnHeadName
            InParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemsByMultipleItemProfiles, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItems(ByVal screen As ClientScreen) As DataTable
            Dim SQL_STR1 As String = "SELECT REC_ID AS ID ,ITEM_NAME ,ITEM_LED_ID,ITEM_TRANS_STMT  from Item_Info where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItems, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, cBase._open_Ins_ID)
        End Function

        Protected Function GetItems_Ledger(ByVal screen As ClientScreen, ByVal ItemProfile As String) As DataTable
            If Not ItemProfile.StartsWith("'") Then ItemProfile = "'" & ItemProfile
            If Not ItemProfile.EndsWith("'") Then ItemProfile = ItemProfile & "'"
            Dim SQL_STR1 As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                     " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UCASE(I.ITEM_VOUCHER_TYPE) IN (" & ItemProfile & ")"
            Dim inParam As Param_GetItems_Ledger = New Param_GetItems_Ledger
            inParam.Item_Profile = ItemProfile
            inParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItems_Ledger, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, inParam)
        End Function

        Protected Function GetItems_Ledger(ByVal screen As ClientScreen, ByVal LocalQuery As String, ByVal OnlineParam As Param_GetItemsLedgerCommon) As DataTable
            If OnlineParam Is Nothing Then
                OnlineParam = New Param_GetItemsLedgerCommon
                OnlineParam.currInsttID = cBase._open_Ins_ID
                Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItems_Ledger_Common, LocalQuery, screen, RealTimeService.Tables.ITEM_INFO, OnlineParam)
            Else
                OnlineParam.currInsttID = cBase._open_Ins_ID
                Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItems_Ledger_Common, LocalQuery, screen, RealTimeService.Tables.ITEM_INFO, OnlineParam)
            End If
        End Function

        'Removed Function, Just for reference 
        'Protected Function GetItems_Ledger(ByVal screen As ClientScreen, ByVal OnlineQuery As String, ByVal LocalQuery As String) As DataTable
        '    Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItems_Ledger_Common, LocalQuery, screen, RealTimeService.Tables.ITEM_INFO)
        '    'Return GetListOfRecords(OnlineQuery, LocalQuery, screen, RealTimeService.Tables.ITEM_INFO)
        'End Function

        ''' <summary>
        ''' Common Function to Get Ledgers, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetLedgersList(ByVal screen As ClientScreen) As DataTable
            Dim SQL_STR1 As String = "SELECT LED_ID,LED_NAME From Acc_Ledger_Info where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetLedgersList, SQL_STR1, screen, RealTimeService.Tables.ACC_LEDGER_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get Items' Details, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetItemDetails(ByVal screen As ClientScreen) As DataTable
            Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ ,ITEM_PROFILE,  REC_ID AS ITEM_ID   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UCASE(ITEM_VOUCHER_TYPE) IN ('PAYMENT') AND UCASE(ITEM_PETTY_CASH)='YES' "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetItemDetails, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, cBase._open_Ins_ID)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Item Profile with Opening Profile = Yes, Shifted
        ''' </summary>
        ''' <param name="item_profile"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOpeningProfileItems(ByVal item_profile As String, ByVal screen As ClientScreen, ByVal IDColumnHeadName As String, ByVal NameColumnHeadName As String) As DataTable
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & IDColumnHeadName & " ,ITEM_NAME as " & NameColumnHeadName & " from ITEM_INFO where    UCASE(ITEM_PROFILE)='" & item_profile & "' AND  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "   AND ITEM_PROFILE_OPENING='YES'   order by item_name ;"
            Dim InParam As Param_GetOpeningProfileItems = New Param_GetOpeningProfileItems()
            InParam.IDColumnHeadName = IDColumnHeadName
            InParam.item_profile = item_profile
            InParam.NameColumnHeadName = NameColumnHeadName
            InParam.currInsttID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetOpeningProfileItems, SQL_STR1, screen, RealTimeService.Tables.ITEM_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Center Task Info for Active Center, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenterTaskInfo(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim Query As String = " SELECT TASK_NAME,PERMISSION FROM CENTRE_TASK_INFO  Where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID='" & cBase._open_Cen_ID & "' AND TASK_COD_YEAR_ID = '" & cBase._open_Year_ID & "'; "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterTaskInfo, Query, screen, RealTimeService.Tables.CENTRE_TASK_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get WINGS Center Task Info for Active Center, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetWingsCenterTaskInfo(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim Query As String = " SELECT TASK_NAME,PERMISSION,TASK_REF_ID FROM CENTRE_TASK_INFO  Where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID='" & cBase._open_Cen_ID & "' AND TASK_TYPE='WING' AND TASK_COD_YEAR_ID = '" & cBase._open_Year_ID & "'; "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetWingsCenterTaskInfo, Query, screen, RealTimeService.Tables.CENTRE_TASK_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get Currency Name by ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCurrencyName(ByVal CurrID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim Query As String = "SELECT CUR_NAME FROM Currency_Info WHERE REC_ID ='" & CurrID & "' ;"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCurrencyName, Query, screen, RealTimeService.Tables.CURRENCY_INFO, CurrID)
        End Function

        ''' <summary>
        ''' Common Function to Get Currency List, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCurrencyList(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim Query As String = " SELECT CUR_NAME,CUR_CODE,CUR_SYMBOL,REC_ID AS CUR_ID FROM Currency_Info where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY CUR_NAME "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCurrencyList, Query, screen, RealTimeService.Tables.CURRENCY_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get HQ Centers, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetHQCentersForCurrInstt(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim Query As String = " SELECT  DISTINCT T.CEN_ID AS HQ_CEN_ID  FROM Centre_Task_Info AS T INNER JOIN CENTRE_INFO AS C ON T.CEN_ID = C.CEN_ID WHERE T.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND C.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND  T.TASK_NAME='H.Q. CENTRE' AND C.CEN_INS_ID='" & cBase._open_Ins_ID & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetHQCentersForCurrInstt, Query, screen, RealTimeService.Tables.CENTRE_TASK_INFO, cBase._open_Ins_ID)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenterDetailsForCenID(ByVal screen As ClientScreen) As DataTable
            Dim Query As String = "SELECT *  from CENTRE_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = '" & cBase._open_Cen_ID_Main & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterDetailsForCenID, Query, screen, Tables.CENTRE_INFO, cBase._open_Cen_ID_Main)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, designed for letter PAD, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenterDetailsForLetterPAD(ByVal screen As ClientScreen) As DataTable
            Dim LocalQuery As String = "SELECT CEN_NAME," &
                                     "   IIF(ISNULL(CEN_ADD1),'',  IIF(LEN(LTRIM(RTRIM(CEN_ADD1)))>0, LTRIM(RTRIM(CEN_ADD1)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_ADD2),'',  IIF(LEN(LTRIM(RTRIM(CEN_ADD2)))>0, LTRIM(RTRIM(CEN_ADD2)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_ADD3),'',  IIF(LEN(LTRIM(RTRIM(CEN_ADD3)))>0, LTRIM(RTRIM(CEN_ADD3)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_ADD4),'',  IIF(LEN(LTRIM(RTRIM(CEN_ADD4)))>0, LTRIM(RTRIM(CEN_ADD4)) + ' ',''))  " &
                                     " + IIF(ISNULL(CEN_CITY),'',  IIF(LEN(LTRIM(RTRIM(CEN_CITY)))>0, LTRIM(RTRIM(CEN_CITY)) + ' - ','')) " &
                                     " + IIF(ISNULL(CEN_PINCODE),'',  IIF(LEN(LTRIM(RTRIM(CEN_PINCODE)))>0, LTRIM(RTRIM(CEN_PINCODE)) + ', ','')) " &
                                     " + IIF(ISNULL(CEN_DISTRICT),'',  IIF(LEN(LTRIM(RTRIM(CEN_DISTRICT)))>0, LTRIM(RTRIM(CEN_DISTRICT)) + ', ','')) " &
                                     " + IIF(ISNULL(CEN_STATE),'',  IIF(LEN(LTRIM(RTRIM(CEN_STATE)))>0, LTRIM(RTRIM(CEN_STATE)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_COUNTRY),'',  IIF(LEN(LTRIM(RTRIM(CEN_COUNTRY)))>0, LTRIM(RTRIM(CEN_COUNTRY)) + ' ','')) As [CEN_ADDRESS], " &
                                     "   IIF(ISNULL(CEN_TEL_NO_1),'',  IIF(LEN(LTRIM(RTRIM(CEN_TEL_NO_1)))>0, 'Tel.No.: ' + LTRIM(RTRIM(CEN_TEL_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_TEL_NO_2),'',  IIF(LEN(LTRIM(RTRIM(CEN_TEL_NO_2)))>0, ', ' + LTRIM(RTRIM(CEN_TEL_NO_2))  ,''))  As [CEN_TEL_NO], " &
                                     "   IIF(ISNULL(CEN_MOB_NO_1),'',  IIF(LEN(LTRIM(RTRIM(CEN_MOB_NO_1)))>0, 'Mob.No.: ' + LTRIM(RTRIM(CEN_MOB_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_MOB_NO_2),'',  IIF(LEN(LTRIM(RTRIM(CEN_MOB_NO_2)))>0, ', ' + LTRIM(RTRIM(CEN_MOB_NO_2))  ,''))  As [CEN_MOB_NO], " &
                                     "   IIF(ISNULL(CEN_FAX_NO_1),'',  IIF(LEN(LTRIM(RTRIM(CEN_FAX_NO_1)))>0, 'Fax No.: ' + LTRIM(RTRIM(CEN_FAX_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_FAX_NO_2),'',  IIF(LEN(LTRIM(RTRIM(CEN_FAX_NO_2)))>0, ', ' + LTRIM(RTRIM(CEN_FAX_NO_2))  ,''))  As [CEN_FAX_NO], " &
                                     "   IIF(ISNULL(CEN_EMAIL_ID_1),'',  IIF(LEN(LTRIM(RTRIM(CEN_EMAIL_ID_1)))>0, 'Email: ' + LTRIM(RTRIM(CEN_EMAIL_ID_1)) + ' ','')) " &
                                     " + IIF(ISNULL(CEN_EMAIL_ID_2),'',  IIF(LEN(LTRIM(RTRIM(CEN_EMAIL_ID_2)))>0, ', ' + LTRIM(RTRIM(CEN_EMAIL_ID_2)) ,''))  As [CEN_EMAIL] " &
                                     " FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = '" & cBase._open_Cen_ID & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterDetailsForLetterPAD, LocalQuery, screen, Tables.CENTRE_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, in Short, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetMainCenterAddress(ByVal screen As ClientScreen) As DataTable
            Dim LocalQuery As String = " SELECT IIF(ISNULL(CEN_B_NAME),'',CEN_B_NAME) AS CEN_B_NAME, " &
                                      "        IIF(ISNULL(CEN_ADD1),'',CEN_ADD1) AS CEN_ADD1, " &
                                      "        IIF(ISNULL(CEN_ADD2),'',CEN_ADD2) AS CEN_ADD2, " &
                                      "        IIF(ISNULL(CEN_ADD3),'',CEN_ADD3) AS CEN_ADD3, " &
                                      "        IIF(ISNULL(CEN_ADD4),'',CEN_ADD4) AS CEN_ADD4, " &
                                      "        IIF(ISNULL(CEN_CITY),'',CEN_CITY) AS CEN_CITY, " &
                                      "        IIF(ISNULL(CEN_STATE),'',CEN_STATE) AS CEN_STATE, " &
                                      "        IIF(ISNULL(CEN_COUNTRY),'',CEN_COUNTRY) AS CEN_COUNTRY  " &
                                      " FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID= '" & cBase._open_Cen_ID_Main & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterAddress, LocalQuery, screen, Tables.CENTRE_INFO, cBase._open_Cen_ID_Main)
        End Function

        ''' <summary>
        ''' Common Function to Get Original Password, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOrgPasswordForCenID(ByVal CenID As Integer, ByVal screen As ClientScreen) As Object
            Dim Query As String = " SELECT CEN_CON_SCANCODE  " &
                                  " FROM CENTRE_INFO WHERE   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID=" & CenID.ToString & ""
            Return GetSingleValue_Core(RealTimeService.RealServiceFunctions.Core_GetOrgPasswordForCenID, Query, screen, Tables.CENTRE_INFO, CenID)
        End Function

        ''' <summary>
        ''' Common Function to CenIDs for Center BK PAD No, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenIDForBKPad(ByVal CenBKPadNo As String, ByVal screen As ClientScreen) As DataTable
            Dim Query As String = " select CEN_ID from CENTRE_INFO WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_BK_PAD_NO = '" & CenBKPadNo & "';"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenIDForBKPad, Query, screen, Tables.CENTRE_INFO, CenBKPadNo)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, common function, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenterDetailsByQuery(ByVal LocalQuery As String, ByVal screen As ClientScreen, Optional ByVal Param As Object = Nothing) As DataTable
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterDetailsByQuery_Common, LocalQuery, screen, Tables.CENTRE_INFO, Param)
        End Function

        ''' <summary>
        ''' Common Function to Get Center List by BK PAD No, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCentersByBKPAD(ByVal screen As ClientScreen) As DataTable
            Dim Query As String = "select CEN_INS_ID ,CEN_UID  , CEN_NAME , CEN_INCHARGE  FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_BK_PAD_NO ='" & cBase._open_PAD_No_Main & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCentersByBKPAD, Query, screen, Tables.CENTRE_INFO, cBase._open_PAD_No_Main)
        End Function

        ''' <summary>
        ''' Common Function to Get Center ID by Cen RecID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCenterIDByCenRecID(ByVal screen As ClientScreen, ByVal Cen_Rec_ID As String) As Object
            Dim Query As String = "select CEN_ID FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID ='" & Cen_Rec_ID & "'"
            'Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCenterIDByCenRecID, Query, screen, Tables.CENTRE_INFO, Cen_Rec_ID).Rows(0)(0)
            Return GetSingleValue_Core(RealTimeService.RealServiceFunctions.Core_GetCenterIDByCenRecID, Query, screen, Tables.CENTRE_INFO, Cen_Rec_ID)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetInstituteList(ByVal screen As ClientScreen) As DataTable
            Dim Query As String = "select INS_NAME , INS_ID, INS_SHORT  FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetInstituteList, Query, screen, Tables.INSTITUTE_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetInstituteList(ByVal screen As ClientScreen, ByVal NameColHead As String, ByVal IDColHead As String, ByVal SNameColHead As String) As DataTable
            ' Dim OnlineQuery As String = "select INS_NAME as '" & NameColHead & "', INS_ID as '" & IDColHead & "', INS_SHORT as '" & SNameColHead & "' FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Dim LocalQuery As String = "select INS_NAME as [" & NameColHead & "], INS_ID as [" & IDColHead & "], INS_SHORT as [" & SNameColHead & "] FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Dim InParam As Param_GetInstituteList = New Param_GetInstituteList()
            InParam.IDColHead = IDColHead
            InParam.NameColHead = NameColHead
            InParam.SNameColHead = SNameColHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetInstituteListNameAndID, LocalQuery, screen, Tables.INSTITUTE_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetInstituteList(ByVal screen As ClientScreen, ByVal NameColHeader As String, ByVal IDColHEader As String) As DataTable
            Dim Query As String = "select INS_SHORT AS " & NameColHeader & " ,INS_ID  AS " & IDColHEader & "  FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Dim InParam As Param_GetInstituteListNameInShort = New Param_GetInstituteListNameInShort()
            InParam.IDColHEader = IDColHEader
            InParam.NameColHeader = NameColHeader
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetInstituteListNameInShort, Query, screen, Tables.INSTITUTE_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Datails, specifically formatted , for letter head, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetInstituteDetails(ByVal InsID As String, ByVal screen As ClientScreen) As DataTable
            Dim LocalQuery As String = " SELECT I.INS_NAME, I.INS_HEADER1, I.INS_HEADER2, I.INS_HEADER3, I.INS_HEADER4, " &
                                     " 'Head Office: ' +  IIF(ISNULL(I.INS_HO_ADD1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_ADD1)))>0, LTRIM(RTRIM(I.INS_HO_ADD1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_ADD2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_ADD2)))>0, LTRIM(RTRIM(I.INS_HO_ADD2)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_ADD3),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_ADD3)))>0, LTRIM(RTRIM(I.INS_HO_ADD3)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_ADD4),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_ADD4)))>0, LTRIM(RTRIM(I.INS_HO_ADD4)) + ' ','')) " &
                                     " + ', ' + A.MAP_CITY + ' - ' + A.MAP_PINCODE + ', ' + A.MAP_DISTRICT  + ', ' + A.MAP_STATE  + ', ' + A.MAP_COUNTRY " &
                                     " + ' ' + IIF(ISNULL(I.INS_HO_TEL_NO_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_TEL_NO_1)))>0, 'Tel.No.: ' + LTRIM(RTRIM(I.INS_HO_TEL_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_TEL_NO_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_TEL_NO_2)))>0, LTRIM(RTRIM(I.INS_HO_TEL_NO_2)) + ' ','')) " &
                                     " + ' ' + IIF(ISNULL(I.INS_HO_FAX_NO_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_FAX_NO_1)))>0, 'Fax No.: ' + LTRIM(RTRIM(I.INS_HO_Fax_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_FAX_NO_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_FAX_NO_2)))>0, LTRIM(RTRIM(I.INS_HO_FAX_NO_2)) + ' ','')) " &
                                     " + ' ' + IIF(ISNULL(I.INS_HO_EMAIL_ID_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_EMAIL_ID_1)))>0, 'Email: ' + LTRIM(RTRIM(I.INS_HO_EMAIL_ID_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_HO_EMAIL_ID_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_HO_EMAIL_ID_2)))>0, LTRIM(RTRIM(I.INS_HO_EMAIL_ID_2)) + ' ','')) " &
                                     " AS [INS_HO_ADDRESS], " &
                                     " 'Administrative Office: ' +  IIF(ISNULL(I.INS_AO_ADD1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_ADD1)))>0, LTRIM(RTRIM(I.INS_AO_ADD1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_ADD2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_ADD2)))>0, LTRIM(RTRIM(I.INS_AO_ADD2)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_ADD3),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_ADD3)))>0, LTRIM(RTRIM(I.INS_AO_ADD3)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_ADD4),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_ADD4)))>0, LTRIM(RTRIM(I.INS_AO_ADD4)) + ' ','')) " &
                                     " + ', ' + B.MAP_CITY + ' - ' + B.MAP_PINCODE + ', ' + B.MAP_DISTRICT + ', ' + B.MAP_STATE + ', ' +  B.MAP_COUNTRY " &
                                     " + ' ' + IIF(ISNULL(I.INS_AO_TEL_NO_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_TEL_NO_1)))>0, 'Tel.No.: ' + LTRIM(RTRIM(I.INS_AO_TEL_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_TEL_NO_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_TEL_NO_2)))>0, LTRIM(RTRIM(I.INS_AO_TEL_NO_2)) + ' ','')) " &
                                     " + ' ' + IIF(ISNULL(I.INS_AO_FAX_NO_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_FAX_NO_1)))>0, 'Fax No.: ' + LTRIM(RTRIM(I.INS_AO_Fax_NO_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_FAX_NO_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_FAX_NO_2)))>0, LTRIM(RTRIM(I.INS_AO_FAX_NO_2)) + ' ','')) " &
                                     " + ' ' + IIF(ISNULL(I.INS_AO_EMAIL_ID_1),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_EMAIL_ID_1)))>0, 'Email: ' + LTRIM(RTRIM(I.INS_AO_EMAIL_ID_1)) + ' ','')) " &
                                     " + IIF(ISNULL(I.INS_AO_EMAIL_ID_2),'',  IIF(LEN(LTRIM(RTRIM(I.INS_AO_EMAIL_ID_2)))>0, LTRIM(RTRIM(I.INS_AO_EMAIL_ID_2)) + ' ','')) " &
                                     " as [INS_AO_ADDRESS] " &
                                     " FROM  (Institute_Info AS I LEFT JOIN City_Info AS A ON I.INS_HO_CITY_ID = A.REC_ID) LEFT JOIN City_Info AS B ON I.INS_AO_CITY_ID = B.REC_ID " &
                                     " WHERE I.INS_ID = '" & InsID & "'"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetInstituteDetails, LocalQuery, screen, Tables.INSTITUTE_INFO, InsID)
        End Function

        ''' <summary>
        ''' Common Function to Get Wings' Name and ID, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetWingsList(ByVal screen As ClientScreen) As DataTable
            Dim Query As String = "select WING_NAME as Name,WING_ID AS ID,REC_ID FROM WINGS_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ORDER BY WING_NAME "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetWingsList, Query, screen, Tables.WINGS_INFO)
        End Function

        ''' <summary>
        ''' Common Function to Get Countries, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCountriesList(ByVal Screen As ClientScreen, ByVal NameColumnHead As String, ByVal CodeColumnHead As String, ByVal IDColumnHead As String) As DataTable
            Dim Query As String = "SELECT CO_NAME AS " & NameColumnHead & ", CO_CODE AS " & CodeColumnHead & " ,REC_ID as " & IDColumnHead & "  FROM Map_Country_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Dim InParam As Param_GetCountriesList = New Param_GetCountriesList()
            InParam.CodeColumnHead = CodeColumnHead
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCountriesList, Query, Screen, Tables.MAP_COUNTRY_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Countries By ID, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCountriesByID(ByVal Screen As ClientScreen, ByVal NameColumnHead As String, ByVal IDColumnHead As String, ByVal Country_IDs As String) As DataTable
            Dim Query As String = "SELECT CO_NAME AS " & NameColumnHead & " ,REC_ID as " & IDColumnHead & "  FROM Map_Country_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & Country_IDs & ") ;"
            Dim InParam As Param_GetCountriesByID = New Param_GetCountriesByID()
            InParam.Country_IDs = Country_IDs
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCountriesByID, Query, Screen, Tables.MAP_COUNTRY_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get States, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetStatesList(ByVal Screen As ClientScreen, ByVal CountryCode As String, ByVal NameColumnHead As String, ByVal CodeColumnHead As String, ByVal IDColumnHead As String) As DataTable
            Dim Query As String = " SELECT ST_NAME AS " & NameColumnHead & "  , ST_CODE AS " & CodeColumnHead & " ,REC_ID as " & IDColumnHead & " FROM Map_State_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & CountryCode & "'  "
            Dim InParam As Param_GetStatesList = New Param_GetStatesList()
            InParam.CodeColumnHead = CodeColumnHead
            InParam.CountryCode = CountryCode
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetStatesList, Query, Screen, Tables.MAP_STATE_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get States by ID, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetStatesByID(ByVal Screen As ClientScreen, ByVal NameColumnHead As String, ByVal IDColumnHead As String, ByVal State_IDs As String) As DataTable
            Dim Query As String = " SELECT ST_NAME AS " & NameColumnHead & " ,REC_ID as " & IDColumnHead & " FROM Map_State_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & State_IDs & ") ;"
            Dim InParam As Param_GetStatesByID = New Param_GetStatesByID()
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            InParam.State_IDs = State_IDs
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetStatesByID, Query, Screen, Tables.MAP_STATE_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get DISTRICTS, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetDistrictsList(ByVal Screen As ClientScreen, ByVal CountryCode As String, ByVal StateCode As Double, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As DataTable
            Dim Query As String = " SELECT DI_NAME  AS " & NameColumnHead & " , REC_ID as " & IDColumnHead & "  FROM Map_District_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & CountryCode & "' AND ST_CODE= " & StateCode
            Dim InParam As Param_GetDistrictsList = New Param_GetDistrictsList()
            InParam.CountryCode = CountryCode
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            InParam.StateCode = StateCode
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetDistrictsList, Query, Screen, Tables.MAP_DISTRICT_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get DISTRICTS ByIDs, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetDistrictsByID(ByVal Screen As ClientScreen, ByVal NameColumnHead As String, ByVal IDColumnHead As String, ByVal District_IDs As String) As DataTable
            Dim Query As String = " SELECT DI_NAME  AS " & NameColumnHead & " , REC_ID as " & IDColumnHead & "  FROM Map_District_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & District_IDs & ") ;"
            Dim InParam As Param_GetDistrictsByID = New Param_GetDistrictsByID()
            InParam.District_IDs = District_IDs
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetDistrictsByID, Query, Screen, Tables.MAP_DISTRICT_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities by Country and State, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCitiesList(ByVal Screen As ClientScreen, ByVal CountryCode As String, ByVal StateCode As Double, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As DataTable
            Dim Query As String = " SELECT CI_NAME AS " & NameColumnHead & "  , REC_ID as " & IDColumnHead & "  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & CountryCode & "' AND ST_CODE= " & StateCode
            Dim InParam As Param_GetCitiesListByCountryandstate = New Param_GetCitiesListByCountryandstate()
            InParam.CountryCode = CountryCode
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            InParam.StateCode = StateCode
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCitiesList, Query, Screen, Tables.MAP_CITY_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities by Country, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCitiesList(ByVal Screen As ClientScreen, ByVal CountryCode As String, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As DataTable
            Dim Query As String = " SELECT CI_NAME AS " & NameColumnHead & "  , REC_ID as " & IDColumnHead & "  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & CountryCode & "' "
            Dim InParam As Param_GetCitiesListByCountry = New Param_GetCitiesListByCountry()
            InParam.CountryCode = CountryCode
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCitiesListByCountry, Query, Screen, Tables.MAP_CITY_INFO, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities from given IDs, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCitiesByID(ByVal Screen As ClientScreen, ByVal NameColumnHead As String, ByVal IDColumnHead As String, ByVal City_IDs As String) As DataTable
            Dim Query As String = " SELECT CI_NAME AS " & NameColumnHead & "  , REC_ID as " & IDColumnHead & "  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & City_IDs & ") ;"
            Dim InParam As Param_GetCitiesByID = New Param_GetCitiesByID()
            InParam.City_IDs = City_IDs
            InParam.IDColumnHead = IDColumnHead
            InParam.NameColumnHead = NameColumnHead
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCitiesByID, Query, Screen, Tables.MAP_CITY_INFO, InParam)
        End Function

        ''' <summary>
        ''' Returns City Name and RecID, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCities(ByVal Screen As ClientScreen)
            Dim Query As String = "SELECT CI_NAME, REC_ID FROM MAP_CITY_INFO"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Core_GetCities, Query, Screen, Tables.MAP_CITY_INFO)
        End Function
        Public Function GetAllItemsMappedToDocuments() As List(Of Return_GetAllItems)
            Dim rTable As DataTable = GetItemsByQuery_Common("", ClientScreen.Report_Items_Documents) 'type=2
            Dim _ItemList As List(Of Return_GetAllItems) = New List(Of Return_GetAllItems)
            If (Not (rTable) Is Nothing) Then
                For Each row As DataRow In rTable.Rows
                    Dim newdata = New Return_GetAllItems
                    newdata.Item = row.Field(Of String)("ITEM_NAME")
                    newdata.ID = row.Field(Of String)("REC_ID")
                    _ItemList.Add(newdata)
                Next
            End If
            Return _ItemList
        End Function

        Public Function GetItemDocuments(ItemID As String) As List(Of Return_GetItemDocuments)
            Dim rTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Core_GetItemDocuments, ClientScreen.Report_Items_Documents, ItemID)
            Dim _ItemList As List(Of Return_GetItemDocuments) = New List(Of Return_GetItemDocuments)
            If (Not (rTable) Is Nothing) Then
                For Each row As DataRow In rTable.Rows
                    Dim newdata = New Return_GetItemDocuments
                    newdata.Item_Name = row.Field(Of String)("ITEM_NAME")
                    newdata.Document_Name = row.Field(Of String)("DOC")
                    newdata.Screen_Name = row.Field(Of String)("ATTACH_SCREEN")
                    _ItemList.Add(newdata)
                Next
            End If
            Return _ItemList
        End Function
#End Region

#Region "Data Table Functions-Shifted"
        Protected Function GetAuditedPeriod(ByVal Screen As ClientScreen) As DataTable
            Dim inParam As New Param_GetYearPeriod
            inParam.YrStartDate = cBase._open_Year_Sdt
            inParam.YrEndDate = cBase._open_Year_Edt
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetAuditedPeriod, Screen, inParam)
        End Function
        Protected Function GetAccountsSubmittedPeriod(ByVal Screen As ClientScreen) As DataTable
            Dim inParam As New Param_GetYearPeriod
            inParam.YrStartDate = cBase._open_Year_Sdt
            inParam.YrEndDate = cBase._open_Year_Edt
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetAccountsSubmittedPeriod, Screen, inParam)
        End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="RecIDColHeader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOpeningBalance(ByVal Screen As ClientScreen, ByVal RecIDColHeader As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetOpeningBalance, Screen, RecIDColHeader)
        End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, common function, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOpeningBalance(ByVal Screen As ClientScreen, ByVal inParam As Param_GetOpeningBalance_Common) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetOpeningBalance_Common, Screen, inParam)
        End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="RecIDColHeader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOpeningBalance(ByVal Screen As ClientScreen, ByVal RecIDColHeader As String, ByVal AccountRecID As String) As DataTable
            Dim InParam As Param_GetOpeningBalance = New Param_GetOpeningBalance()
            InParam.AccountRecID = AccountRecID
            InParam.RecIDColHeader = RecIDColHeader
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetOpeningBalanceRecIDWithCustomColHeadName, Screen, InParam)
        End Function

        ''' <summary>
        ''' Returns opening Balance Rows count For a RecID, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetOpeningBalanceRowCount(ByVal Screen As ClientScreen, ByVal RecID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Data_GetOpeningBalanceRowCount, Screen, RecID)
        End Function

        ''' <summary>
        ''' Common Function to Get IDs for Bank Accounts of Type Saving, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>called for ClientScreen.Accounts_Vouchers, ClientScreen.Accounts_Notebook</remarks>
        Protected Function GetSavingAccountsList(ByVal screen As ClientScreen) As DataTable
            Dim _BA As DbOperations.BankAccounts = New DbOperations.BankAccounts(cBase)
            Return _BA.GetList(screen)
        End Function

        Protected Function GetCashOpeningBalanceAmount(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetCashOpeningBalanceAmount, screen)
        End Function

        ''' <summary>
        ''' Common Function to get Cash, Bank Cr , Dr Sum since a particular date, Shifted 
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetCashBankTransSumAmount</remarks>
        Protected Function GetCashBankTransSumAmount(ByVal FromDate As Date, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetCashBankTransSumAmount, screen, FromDate)
        End Function

        ''' <summary>
        ''' Common Function to get Cash Cr , Dr Sum, Shifted 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCashTransSumAmount(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetCashTransSumAmount, screen)
        End Function

        ''' <summary>
        ''' Common Function to get Bank Sum with custom Query, common function, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankTransSumAmount(ByVal screen As ClientScreen, ByVal inPAram As Object) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetBankTransSumAmount_Common, screen, inPAram)
        End Function

        ''' <summary>
        ''' Common Function to get Cash, Bank Cr , Dr Sum between two particular dates, Shifted 
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetCashBankTransSumAmount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal screen As ClientScreen) As DataTable
            Dim InParam As Param_GetCashBankTransSumAmountTwoDates = New Param_GetCashBankTransSumAmountTwoDates()
            InParam.FromDate = FromDate
            InParam.ToDate = ToDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetCashBankTransSumAmountBetweenTwoDates, screen, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Total Bank Balance for selected Bank IDs, Shifted
        ''' </summary>
        ''' <param name="BankIDs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBalanceAmount(ByVal BankIDs As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetBankBalanceAmount, screen, BankIDs)
        End Function

        ''' <summary>
        ''' Common Function to Get Bank Balance for selected Bank IDs, Shifted
        ''' </summary>
        ''' <param name="BankIDs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetBankBalanceAmountIdWise(ByVal BankIDs As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetBankBalanceAmountIdWise, screen, BankIDs)
        End Function

        ''' <summary>
        ''' Common Function to Add Opening Balance, Shifted
        ''' </summary>
        ''' <param name="Amount"></param>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function AddOpeningBalance(ByVal Amount As Double, ByVal RecID As String, ByVal screen As ClientScreen) As Boolean
            Dim InParam As Param_AddOpeningBalance = New Param_AddOpeningBalance()
            InParam.Amount = Amount
            InParam.openYearID = cBase._open_Year_ID
            InParam.RecID = RecID
            Return InsertRecord(RealServiceFunctions.Data_AddOpeningBalance, screen, InParam)
        End Function

        ''' <summary>
        ''' Common Function to Update Opening Balance, Shifted
        ''' </summary>
        ''' <param name="Amount"></param>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Function UpdateOpeningBalance(ByVal Amount As Double, ByVal RecID As String, ByVal screen As ClientScreen, ByVal Status_Action As String) As Boolean
            Dim UpParam As Param_UpdateOpeningBalance = New Param_UpdateOpeningBalance()
            UpParam.Amount = Amount
            UpParam.RecID = RecID
            ' UpParam.Status_Action = Status_Action
            Return UpdateRecord(RealServiceFunctions.Data_UpdateOpeningBalance, screen, UpParam)
        End Function

        ''' <summary>
        ''' Common Function to Remove Opening Balances, Shifted 
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="screen"></param>
        ''' <remarks></remarks>
        Protected Overloads Function DeleteOpeningBalance(ByVal Rec_Id As String, ByVal screen As ClientScreen) As Boolean
            Return DeleteRecord(Rec_Id, Tables.OPENING_BALANCES_INFO, screen)
        End Function

        ''' <summary>
        ''' Common Function to Get Used PartyIDs from various Tables, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetPartyIDList(ByVal Screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPartyIDList, Screen)
        End Function

        ''' <summary>
        ''' Common Function to Get Used ItemIDs from various Tables, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetUsedItemIDList(ByVal Screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetUsedItemIDList, Screen)
        End Function

        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="cTable"></param>
        ''' <param name="conStr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetLastEditOn(ByVal Rec_ID As String, ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal cTable As Tables, ByVal conStr As String) As Object
            Dim Query As String = " SELECT REC_EDIT_ON FROM " & cTable.ToString & " WHERE REC_ID ='" & Rec_ID & "' AND REC_STATUS IN (0,1,2)"
            Dim InParam As Param_GetLastEditOn = New Param_GetLastEditOn()
            InParam.cTable = cTable
            InParam.Rec_ID = Rec_ID
            Return GetSingleValue_Data(RealServiceFunctions.Data_GetLastEditOn, Screen, InParam)
        End Function

        Protected Function IsRecordCarriedForward(ByVal Rec_ID As String, ByVal recYearID As String, ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal cTable As Tables) As Boolean?
            Dim InParam As Param_IsRecordCarriedForward = New Param_IsRecordCarriedForward()
            InParam.cTable = cTable
            InParam.RecID = Rec_ID
            InParam.recYearID = recYearID
            Return GetSingleValue_Data(RealServiceFunctions.Data_IsRecordCarriedForward, Screen, InParam)
        End Function

#End Region

#Region "Local Only functions-Not to be Shifted"
        Protected Function LocalGetActiveCentersFromSelected(ByVal SelectedCentre As String) As DataTable
            Dim _db_Connection As OleDbConnection = New OleDbConnection(cBase._data_ConStr_Sys)
            _db_Connection.Open() : Dim _db_DataAdapter As OleDbDataAdapter = New OleDbDataAdapter("select CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT from COD_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND COD_ACTIVE=TRUE AND CEN_ID IN (" & SelectedCentre & ") ;", _db_Connection)
            Dim _db_Table As DataTable = New DataTable() : _db_DataAdapter.Fill(_db_Table) : _db_DataAdapter.Dispose()
            _db_Connection.Dispose()
            Return _db_Table
        End Function

        Protected Function LocalGet_Cen_Creation_Count_OnClient() As Object
            Dim MaxValue As Object = 0 : Dim xID As Integer = 0
            Using T As New OleDbConnection(cBase._data_ConStr_Sys)
                T.Open()
                Dim command As OleDbCommand = T.CreateCommand() : command.CommandText = "SELECT COUNT(*) FROM COD_INFO  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " " : MaxValue = command.ExecuteScalar()
            End Using
            Return MaxValue

        End Function

        Public Function GetSyncLogs() As DataTable
            Dim SQL_STR1 As String = " select SyncTime as [Date],SyncRemarks as [Remarks],Cen_id as [Centre ID] from SYNC_LOG  ;"
            Dim d1 As New Common_Lib.Get_Data(cBase, "SYS", "SYNC_LOG", SQL_STR1)
            d1._dc_Connection.Close()
            Return d1._dc_DataTable
        End Function

        Public Function CleanSyncLogs() As Boolean
            Dim trans As OleDbTransaction = Nothing : Dim FLAG As Boolean = False
            Using Con As New OleDbConnection(cBase._data_ConStr_Sys)
                Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
                Try
                    trans = Con.BeginTransaction
                    command.Connection = Con
                    command.Transaction = trans
                    Dim STR1 As String = ""
                    STR1 = " DELETE TABLE FROM SYNC_LOG "
                    command.CommandText = STR1 : command.ExecuteNonQuery()
                    trans.Commit()
                    FLAG = True
                Catch ex As Exception
                    trans.Rollback()
                    FLAG = False
                End Try
                Return FLAG
            End Using
        End Function
#End Region

#Region "Mixed Mode Functions"
        ''' <summary>
        ''' Select Center Page :Gets Ins List for a Center 
        ''' </summary>
        ''' <param name="Main_Cen_PAD_No"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function Base_GetCen_Ins_List(ByVal Main_Cen_PAD_No As String) As Object
            Try
                '  Select Case cBase.curr_Db_Conn_Mode(ClientScreen.Start_Select_Center)
                ' Case Common.DbConnectionMode.Online, Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                'Dim SQL_STR As String = "Select DISTINCT CONCAT(INS_NAME, ' (', INS_SHORT, ')') AS INS_NAME_X ,INS_NAME,INS_SHORT ,INS_ID,IF(ISNULL(CEN_UID),'',CEN_UID) AS CEN_UID,CEN_PAD_NO,IF(ISNULL(CEN_ACC_TYPE_ID),'',CEN_ACC_TYPE_ID) AS CEN_ACC_TYPE_ID,ci.CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, cen.REC_ID as CEN_REC_ID FROM COD_INFO AS ci INNER JOIN centre_info AS cen ON ci.CEN_ID = cen.CEN_ID INNER JOIN institute_info AS Ins ON cen.cen_ins_id = ins.INS_ID WHERE(ci.REC_STATUS IN (0,1,2)) AND COD_ACTIVE=1 AND CEN_BK_PAD_NO='" + Main_Cen_PAD_No + "' ORDER BY INS_ID,CEN_UID"
                'If Common.UseSQL Then
                '    SQL_STR = "Select DISTINCT INS_NAME + ' (' + INS_SHORT + ')' AS INS_NAME_X ,INS_NAME,INS_SHORT ,INS_ID,COALESCE(CEN_UID,'') AS CEN_UID,CEN_PAD_NO,COALESCE(CEN_ACC_TYPE_ID,'') AS CEN_ACC_TYPE_ID,ci.CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, cen.REC_ID as CEN_REC_ID FROM COD_INFO AS ci INNER JOIN centre_info AS cen ON ci.CEN_ID = cen.CEN_ID INNER JOIN institute_info AS Ins ON cen.cen_ins_id = ins.INS_ID WHERE(ci.REC_STATUS  IN (0,1,2)) AND COD_ACTIVE=1 AND CEN_BK_PAD_NO='" + Main_Cen_PAD_No + "' ORDER BY INS_ID,CEN_UID"
                'End If
                'Return RealService.List(cBase._open_User_ID, RealTimeService.Tables.COD_INFO, cBase._open_Cen_ID, SQL_STR, "COD_INFO", ClientScreen.Start_Select_Center, cBase._PC_ID, cBase._Current_Version)
                Return GetDataListOfRecords(RealServiceFunctions.CentreRelated_CodInfo_Base_GetCen_Ins_List, ClientScreen.Start_Select_Center, Main_Cen_PAD_No)
                'Case Common.DbConnectionMode.Local
                '    Dim _db_Dataset As New DataSet()
                '    Dim _db_Connection As New OleDbConnection
                '    Dim _db_DataAdapter As OleDbDataAdapter = Nothing
                '    Dim _db_Table As New DataTable()

                '    _db_Dataset = New DataSet
                '    '1 RUN SQL QUERY
                '    _db_Connection = New OleDbConnection(cBase._data_ConStr_Core)
                '    _db_Connection.Open() : _db_DataAdapter = New OleDbDataAdapter("select CEN_ID,CEN_INS_ID,CEN_PAD_NO,CEN_BK_PAD_NO,IIF(ISNULL(CEN_UID),'',CEN_UID) AS XCEN_UID,IIF(ISNULL(CEN_ACC_TYPE_ID),'',CEN_ACC_TYPE_ID) AS XCEN_ACC_TYPE_ID, REC_ID as CEN_REC_ID  from CENTRE_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_BK_PAD_NO='" & Main_Cen_PAD_No & "';", _db_Connection)
                '    _db_Table = New DataTable() : _db_DataAdapter.Fill(_db_Table) : _db_DataAdapter.Dispose()
                '    _db_Connection.Dispose()
                '    _db_Dataset.Tables.Add(_db_Table)
                '    Dim SelectedCentre As String = ""
                '    For Each xRow As DataRow In _db_Table.Rows : SelectedCentre += "'" & xRow("CEN_ID").ToString & "'," : Next
                '    If SelectedCentre.Trim.Length > 0 Then SelectedCentre = IIf(SelectedCentre.Trim.EndsWith(","), Mid(SelectedCentre.Trim.ToString, 1, SelectedCentre.Trim.Length - 1), SelectedCentre.Trim.ToString)
                '    '2 RUN SQL QUERY
                '    _db_Dataset.Tables.Add(LocalGetActiveCentersFromSelected(SelectedCentre))
                '    '3 RUN SQL QUERY
                '    _db_Connection = New OleDbConnection(cBase._data_ConStr_Core)
                '    _db_Connection.Open() : _db_DataAdapter = New OleDbDataAdapter("select INS_ID, INS_NAME + ' (' + INS_SHORT + ')' AS INS_NAME_X , INS_NAME  , INS_SHORT from INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;", _db_Connection)
                '    _db_Table = New DataTable() : _db_DataAdapter.Fill(_db_Table) : _db_DataAdapter.Dispose()
                '    _db_Connection.Dispose()
                '    _db_Dataset.Tables.Add(_db_Table)
                '    'BUILD DATA
                '    _db_Dataset.Locale = CultureInfo.InvariantCulture
                '    Dim Centre_Table = _db_Dataset.Tables(0).AsEnumerable()
                '    Dim COD_Info_Table = _db_Dataset.Tables(1).AsEnumerable()
                '    Dim Institute_Table = _db_Dataset.Tables(2).AsEnumerable()
                '    Dim BuildData = From centre In Centre_Table, cod In COD_Info_Table, Institute In Institute_Table _
                '                    Order By Institute.Field(Of String)("INS_ID"), centre.Field(Of String)("CEN_ID"), cod.Field(Of String)("COD_YEAR_NAME") _
                '                    Where (cod.Field(Of String)("CEN_ID") = centre.Field(Of String)("CEN_ID")) _
                '                    And (centre.Field(Of String)("CEN_INS_ID") = Institute.Field(Of String)("INS_ID"))
                '                    Select New With { _
                '                                    .INS_NAME_X = Institute.Field(Of String)("INS_NAME_X"), _
                '                                    .INS_NAME = Institute.Field(Of String)("INS_NAME"), _
                '                                    .INS_SHORT = Institute.Field(Of String)("INS_SHORT"), _
                '                                    .INS_ID = Institute.Field(Of String)("INS_ID"), _
                '                                    .CEN_UID = centre.Field(Of String)("XCEN_UID"), _
                '                                    .CEN_ACC_TYPE_ID = centre.Field(Of String)("XCEN_ACC_TYPE_ID"), _
                '                                    .CEN_ID = centre.Field(Of String)("CEN_ID"), _
                '                                    .CEN_PAD_NO = centre.Field(Of String)("CEN_PAD_NO"), _
                '                                    .COD_YEAR_ID = cod.Field(Of String)("COD_YEAR_ID"), _
                '                                    .COD_YEAR_NAME = cod.Field(Of String)("COD_YEAR_NAME"), _
                '                                    .COD_YEAR_SDT = cod.Field(Of Date)("COD_YEAR_SDT"), _
                '                                    .COD_YEAR_EDT = cod.Field(Of Date)("COD_YEAR_EDT"), _
                '                                    .CEN_REC_ID = centre.Field(Of Date)("CEN_REC_ID") _
                '                                    }

                '    Return BuildData.ToList()
                'Case Else
                '    Return Nothing
                ' End Select
            Catch ex As Exception
                CommonExceptionCall(ex)
                Return Nothing
            End Try
        End Function

        Protected Function Base_GetSelectCentreList(Optional ByVal BKCertNo As String = "") As DataTable
            Try
                Select Case cBase.curr_Db_Conn_Mode(ClientScreen.Start_Select_Center)
                    Case Common.DbConnectionMode.Local
                        Dim SQL_STR1 As String = " select CEN_ID from COD_INFO WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ;"
                        Dim d1 As New Common_Lib.Get_Data(cBase, "SYS", "COD_INFO", SQL_STR1)
                        Dim SelectedCentre As String = "" : For Each xRow As DataRow In d1._dc_DataTable.Rows : SelectedCentre += "'" & xRow("CEN_ID").ToString & "'," : Next
                        If SelectedCentre.Trim.Length > 0 Then SelectedCentre = IIf(SelectedCentre.Trim.EndsWith(","), Mid(SelectedCentre.Trim.ToString, 1, SelectedCentre.Trim.Length - 1), SelectedCentre.Trim.ToString)
                        d1._dc_Connection.Close()
                        '2.
                        Dim SQL_STR2 As String = " SELECT CEN_NAME as [Centre Name],CEN_PAD_NO as [Certificate No],CEN_ID  as [ID],IIF(ISNULL(CEN_ZONE_ID),'',CEN_ZONE_ID) AS XCEN_ZONE_ID,IIF(ISNULL(CEN_ZONE_SUB_ID),'',CEN_ZONE_SUB_ID) AS XCEN_ZONE_SUB_ID  " &
                                                    " FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2) AND CEN_INS_ID = '00001' AND CEN_MAIN=TRUE AND " &
                                                    " CEN_PAD_NO IN ( SELECT DISTINCT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE  REC_STATUS <> -1  AND CEN_ID IN (" & SelectedCentre & ") ) ORDER BY CEN_NAME "

                        Dim d2 As New Common_Lib.Get_Data(cBase, "CORE", "CENTRE_INFO", SQL_STR2)
                        d2._dc_Connection.Close()
                        Return d2._dc_DataTable
                    Case Common.DbConnectionMode.Online
                        Dim SQL_STR As String = " SELECT CEN_NAME as 'Centre Name',CEN_PAD_NO as 'Certificate No',CEN_ID  as ID,COALESCE(CEN_ZONE_ID,'') AS XCEN_ZONE_ID,COALESCE(CEN_ZONE_SUB_ID,'') AS XCEN_ZONE_SUB_ID  " &
                                                    " FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2) AND CEN_INS_ID = '00001' AND CEN_MAIN=1 AND " &
                                                    " CEN_PAD_NO IN ('" & BKCertNo & "') ORDER BY CEN_NAME "
                        'Return GetListOfRecords(SQL_STR, SQL_STR, ClientScreen.Start_Select_Center, RealTimeService.Tables.CENTRE_INFO)
                        Return GetCoreListOfRecords(RealServiceFunctions.CentreRelated_CodInfo_Base_GetSelectCentreList, SQL_STR, ClientScreen.Start_Select_Center, RealTimeService.Tables.CENTRE_INFO, BKCertNo)
                    Case Common.DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
                        Try
                            Dim SQL_STR2 As String = " SELECT CEN_NAME as [Centre Name],CEN_PAD_NO as [Certificate No],CEN_ID  as [ID],IIF(ISNULL(CEN_ZONE_ID),'',CEN_ZONE_ID) AS XCEN_ZONE_ID,IIF(ISNULL(CEN_ZONE_SUB_ID),'',CEN_ZONE_SUB_ID) AS XCEN_ZONE_SUB_ID  " &
                                                   " FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2) AND CEN_INS_ID = '00001' AND CEN_MAIN=TRUE AND " &
                                                   " CEN_PAD_NO IN ( SELECT DISTINCT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2)  AND CEN_ID IN (" & BKCertNo & ") ) ORDER BY CEN_NAME "
                            Dim d2 As New Common_Lib.Get_Data(cBase, "CORE", "CENTRE_INFO", SQL_STR2)
                            d2._dc_Connection.Close()
                            Return d2._dc_DataTable
                        Catch ex As Exception
                            Dim SQL_STR As String = " SELECT CEN_NAME as 'Centre Name',CEN_PAD_NO as 'Certificate No',CEN_ID  as ID,COALESCE(CEN_ZONE_ID,'') AS XCEN_ZONE_ID,COALESCE(CEN_ZONE_SUB_ID,'') AS XCEN_ZONE_SUB_ID  " &
                                                    " FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2) AND CEN_INS_ID = '00001' AND CEN_MAIN=1 AND " &
                                                    " CEN_PAD_NO IN ('" & BKCertNo & "') ORDER BY CEN_NAME "
                            'Return GetListOfRecords(SQL_STR, SQL_STR, ClientScreen.Start_Select_Center, RealTimeService.Tables.CENTRE_INFO)
                            Return GetCoreListOfRecords(RealServiceFunctions.CentreRelated_CodInfo_Base_GetSelectCentreList, SQL_STR, ClientScreen.Start_Select_Center, RealTimeService.Tables.CENTRE_INFO, BKCertNo)
                        End Try
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                CommonExceptionCall(ex)
                Return Nothing
            End Try
        End Function
#End Region

#Region "Common Functions"
        Protected Sub CommonExceptionCall(ByVal ex As Exception)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SessionProgressError As String = "No Session or Internet Available!!"
            Try
                SessionProgressError = _RealService.SessionInProgressError(cBase._PC_ID)
            Catch
            End Try
            cBase.HandleRealTimeServiceErrors(ex, SessionProgressError)
            'Throw ex
        End Sub

        ''' <summary>
        ''' Returns Server Current DateTime, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCurrentDateTime(ByVal Screen As ClientScreen) As DateTime
            Try
                Dim Query As String = ""
                If Common.UseSQL Then
                    Query = " SELECT GETDATE() FROM SO_LAST_USER_SESSION "
                Else
                    Query = " SELECT NOW() FROM SO_LAST_USER_SESSION "
                End If
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Data_GetCurrentDateTime, Screen)
            Catch ex As Exception
                CommonExceptionCall(ex)
                Return Nothing
            End Try
        End Function

        'specifies if "Core Not Present" message has to be shown 
        Public Function ShowCoreNotPresentMsg(ByVal table As Tables) As Boolean
            If table = Tables.MURLI_INFO Then Return False
            Return True
        End Function

        Public Function GetBaseParams(ByVal Screen As ClientScreen, Optional CenId As Integer = Nothing) As Object
            Dim Param As Basic_Param = New Basic_Param
            If CenId = Nothing Then
                Param.openCenID = cBase._open_Cen_ID
            Else
                Param.openCenID = CenId
            End If
            Param.openUserID = cBase._open_User_ID
            Param.screen = Screen
            Param.openYearID = cBase._open_Year_ID
            Param.PCID = cBase._PC_ID
            Param.version = cBase._Current_Version
            Param.ShowVouchingIndicator = cBase._prefer_show_vouching_indicator
            Param.ShowAttachmentIndicator = cBase._prefer_show_attachment_indicator
            Return Param
        End Function

        Private Function Check_Fill_CoreDataInCache(ByVal OnlineFunction As RealTimeService.RealServiceFunctions, ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal tableName As RealTimeService.Tables, Optional ByVal parameter As Object = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            'If cBase.Cached_Data Is Nothing Then
            '    cBase.Cached_Data = New Dictionary(Of String, DataTable)
            'End If
            'Dim Key As String = OnlineFunction.ToString & screen.ToString
            'Dim type As System.Type = parameter.GetType()
            'If Not type.GetProperty("Type") Is Nothing Then Key = Key & screen.ToString & parameter.GetType().GetProperty("Type").GetValue(parameter, Nothing)

            'If Not cBase.Cached_Data.ContainsKey(Key) Then
            '    cBase.Cached_Data(Key) = OnlineDBOperations.CallCoreListFunctions(RealService, cBase, OnlineFunction, parameter, screen)
            'End If
            'Return cBase.Cached_Data(Key)
            Return OnlineDBOperations.CallCoreListFunctions(_RealService, cBase, OnlineFunction, parameter, screen)
        End Function

        Public Function Decompress_Data(inStream As Byte()) As DataTable
            Dim mem As MemoryStream = New MemoryStream(inStream)
            Dim zip As GZipStream = New GZipStream(mem, CompressionMode.Decompress)
            Dim retDataset As DataSet = New DataSet
            retDataset.ReadXml(zip, XmlReadMode.ReadSchema)
            zip.Close()
            mem.Close()
            Return retDataset.Tables(0).Copy
        End Function

        Public Function Decompress_Data_Dataset(inStream As Byte()) As DataSet
            Dim mem As MemoryStream = New MemoryStream(inStream)
            Dim zip As GZipStream = New GZipStream(mem, CompressionMode.Decompress)
            Dim retDataset As DataSet = New DataSet
            retDataset.ReadXml(zip, XmlReadMode.ReadSchema)
            zip.Close()
            mem.Close()
            Return retDataset
        End Function

        'Private Function ConvertToSecure(Service As RealTimeService.ConnectOneWS) As String
        '    Dim URL As String = Service.Url
        '    If URL.Contains("localhost") Then Return URL
        '    URL = URL.Replace("http://", "https://")
        '    Dim request = System.Net.HttpWebRequest.Create(URL)
        '    request.Timeout = 10000
        '    Dim response = Nothing
        '    Try
        '        response = request.GetResponse()
        '    Catch ex As Exception
        '        Return ConvertToRegular(URL)
        '    End Try
        '    Return URL
        'End Function

        Private Function ConvertToSecure(Service As RealTimeService.ConnectOneWS) As String
            Return Service.Url
            'Dim URL As String = Service.Url
            'If URL.Contains("localhost") Then Return URL
            'URL = URL.Replace("http://", "https://")
            ''Dim request = System.Net.HttpWebRequest.Create(URL)
            ''request.Timeout = 10000
            ''Dim response = Nothing
            'Try
            '    Common_Lib.Log.Write(Log.LogType.Important, "DbOperations", "ConvertToSecure", "URL for SSL Verification:" + URL, Log.LogSuffix.ClientApplication)
            '    Service.Url = URL
            '    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptConnectOneCertifications1937
            '    RealService.GetCurrDate()
            '    '  MessageBox.Show(Service.GetCurrDate(), "Real Time Service WS Result")
            '    'response = request.GetResponse()
            'Catch ex As Exception
            '    Service.Url = ConvertToRegular(URL)
            '    Common_Lib.Log.Write(Log.LogType.Important, "DbOperations", "ConvertToSecure", "SSL Verification for" + URL + " failed :" + ex.Message + "Inner Msg \n" + ex.InnerException.Message, Log.LogSuffix.ClientApplication)
            '    Return Service.Url
            'End Try
            'Return URL
        End Function

        Private Function ConvertToRegular(URL As String) As String
            Return URL.Replace("https://", "http://")
            'Return URL
        End Function

        Private Function IsSecureConnRequired(cFunction As RealTimeService.RealServiceFunctions) As Boolean
            Select Case cFunction
                Case RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfo,
                    RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfoCustomCenId,
                    RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_AddNew,
                    RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_ChangePassword,
                    RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetList,
                    RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId
                    Return True
            End Select
            Return False
        End Function

        Public Function AcceptConnectOneCertifications1937(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
            Try
                Dim strCertFile As String = Application.StartupPath + "\DilaramCertificate1936.cer"
                Dim myCertificate As New System.Security.Cryptography.X509Certificates.X509Certificate(strCertFile, "merababa")
                Dim myPublicKey As String = myCertificate.GetPublicKeyString
                Dim myIssuerName As String = certification.Issuer
                Dim strPublicKey As String = certification.GetPublicKeyString
                Dim strIssuerName As String = certification.Issuer
                If (myPublicKey.Equals(strPublicKey)) And myCertificate.Issuer.Equals(certification.Issuer) Then
                    Common_Lib.Log.Write(Log.LogType.Important, "DbOperations", "AcceptConnectOneCertifications1937", "SSL Verified Successfully", Log.LogSuffix.ClientApplication)
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Common_Lib.Log.Write(Log.LogType.Important, "DbOperations", "AcceptConnectOneCertifications1937", "SSL Verification Failed: ErrorMsg" + ex.Message, Log.LogSuffix.ClientApplication)
            End Try
        End Function

        Public Function IsMultiuserAllowed() As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.Data_IsMultiuserAllowed, ClientScreen.CommonFunctions)
        End Function

        Public Function IsInsuranceAudited() As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.Data_IsInsuranceAudited, ClientScreen.CommonFunctions)
        End Function

        Public Function IsInsuranceAuditor(UserID As String) As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.Data_IsInsuranceAuditor, ClientScreen.CommonFunctions, UserID)
        End Function

        Public Function GetClientAuthorizations() As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Data_GetClientAuthorizations, ClientScreen.CommonFunctions, cBase._open_User_Type)
        End Function

        Public Function GetDynaicClientRestriction() As String
            Return GetScalarBySP(RealServiceFunctions.Data_GetDynamicClientRestriction, ClientScreen.CommonFunctions, Nothing)
        End Function
        Public Function UploadFile(f As Byte(), fileName As String, ID As String, Optional GuidAsFileName As Boolean = False) As String
            ' the byte array argument contains the content of the file
            ' the string argument contains the name and extension
            ' of the file passed in the byte array
            Try
                'use the original file name
                ' to name the resulting file

                Dim separator() As Char = {"."}
                Dim Fileparts As String() = fileName.Split(separator)

                Dim fileType As String = ""
                If Fileparts.Length > 1 Then fileType = "." & Fileparts(Fileparts.Length - 1)

                ' instance a memory stream and pass the
                ' byte array to its constructor
                Dim ms As New MemoryStream(f)

                ' instance a filestream pointing to the
                ' storage folder,

                Dim fs As New FileStream(ConfigurationManager.AppSettings("FilePhysicalPath") & If(GuidAsFileName = True, Fileparts(0), ID) & fileType, FileMode.Create)

                ' write the memory stream containing the original
                ' file as a byte array to the filestream
                ms.WriteTo(fs)

                ' clean up
                ms.Close()
                ms.Dispose()
                fs.Close()
                fs.Dispose()


                f = Nothing
                ' return OK if we made it this far
                Return "OK"
            Catch ex As Exception
                Common_Lib.Log.Write(Common_Lib.Log.LogType.Error, "Attachment", "uploadfile", ex.Message, Common_Lib.Log.LogSuffix.ClientApplication)
                ' return the error message if the operation fails
                Return ex.Message.ToString()
            End Try
        End Function
        Public Function GetDataFromTables(TableName As String, CenID As Int32, UserId As String, Optional FilterByUser As Boolean = False, Optional ProjectID As String = Nothing, Optional ChartID As Integer = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Data_FromTables"
            Dim paramters As String() = {"@CEN_ID", "@TableName", "@FilterByUser", "@UserID", "@ProjectId", "@ChartID"}
            Dim values() As Object = {CenID, TableName, FilterByUser, UserId, ProjectID, ChartID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 255, 4, 255, 36, 4}
            Dim Table = DirectCast([Enum].Parse(GetType(Tables), TableName), Tables)
            Return _RealService.ListFromSP(Table, SPName, TableName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.CommonFunctions))
        End Function
#End Region

#Region "Functions for BLL"
        Public Function CheckPayDetails(ByVal Rec_Id As String) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim base As Common_Lib.RealTimeService.Basic_Param
                base = GetBaseParams(ClientScreen.Accounts_Vouchers)
                Return _RealService.CheckPaymentEdit(Rec_Id, base, cBase._next_Unaudited_YearID, cBase._open_Ins_ID)
                'Return True
            Catch ex As Exception
                CommonExceptionCall(ex)
                Return ""
            End Try
        End Function

        Public Function GetPayDetails(ByVal Rec_Id As String) As Common_Lib.RealTimeService.Param_PaymentData
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Try
                Dim base As Basic_Param
                base = GetBaseParams(ClientScreen.Accounts_Vouchers)

                Dim retPaymentDS As Common_Lib.RealTimeService.Param_PaymentData
                retPaymentDS = _RealService.GetPaymentData(Rec_Id, base)
                Return retPaymentDS

            Catch ex As Exception
                CommonExceptionCall(ex)
                Return Nothing
            End Try
        End Function

        Public Function GetPaySaveChecks(ByVal PaymentVoucherDetails As Voucher_Payment.Param_paymentVoucherDetails) As Voucher_Payment.Param_SaveButtonChecks
            Try
                Dim base As Basic_Param
                base = GetBaseParams(ClientScreen.Accounts_Vouchers)

                Dim retPaymentSaveChecks As Voucher_Payment.Param_SaveButtonChecks
                ' retPaymentSaveChecks = ExecuteGroup(RealServiceFunctions.Payments_SavePaymentDetails, ClientScreen.Accounts_Voucher_Payment, PaymentVoucherDetails)
                retPaymentSaveChecks = cBase._Payment_DBOps.SavePaymentDetails(PaymentVoucherDetails)
                'retPaymentSaveChecks = RealService.SavePaymentDetails(PaymentVoucherDetails, base)
                Return retPaymentSaveChecks

            Catch ex As Exception
                CommonExceptionCall(ex)
                Return Nothing
            End Try
        End Function
#End Region
    End Class
End Class