'Offline Only
Imports Common_Lib.RealTimeService
Imports System.Data
Imports System.Data.OleDb
Partial Public Class DbOperations
    <Serializable>
    Public Class Parameter_Insert_Reminders
        Public Type As String
        Public Title As String
        Public Description As String
        Public SDate As String
        Public STime As String
        Public Recurrence As Double
        Public Music As String
        Public Rec_id As String
    End Class

#Region "Facility"
    'Works in Local Only 
    <Serializable>
    Public Class Reminders
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetList() As DataTable
            Dim d1 As New Common_Lib.Get_Data(cBase, "SYS", "REMINDER", "select REM_TITLE AS [Title],REM_DESCRIPTION as [Description],REM_TYPE AS [Type],REM_SDATE as [Date],REM_STIME as [Time],REM_RECUR as [Repeat],REM_LASTSHOW AS [Last Run] , REC_ID AS [ID] ," & cBase.Rec_Detail("REMINDER_INFO", Common.DbConnectionMode.Local) & " FROM REMINDER_INFO   WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REM_CEN_ID = " & cBase._open_Cen_ID.ToString & " order by  REM_SDATE,REM_STIME")
            d1._dc_Connection.Close()
            Return d1._dc_DataTable
        End Function

        Public Function GetRecord(ByVal RecID As String) As DataTable
            Dim d1 As New Common_Lib.Get_Data(cBase, "SYS", "REMINDER", "select *  from REMINDER_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & RecID & "'")
            d1._dc_Connection.Close()
            Return d1._dc_DataTable
        End Function

        Public Sub Insert(ByVal InParam As Parameter_Insert_Reminders)
            Dim trans As OleDbTransaction = Nothing
            Using Con As New OleDbConnection(cBase._data_ConStr_Sys)
                Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
                trans = Con.BeginTransaction
                command.Connection = Con
                command.Transaction = trans
                Dim STR1 As String = "INSERT INTO REMINDER_INFO(REM_CEN_ID,REM_TYPE,REM_TITLE,REM_DESCRIPTION,REM_SDATE,REM_STIME,REM_RECUR,REM_MUSIC," &
                                                                           "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                 "" & cBase._open_Cen_ID.ToString & ", " &
                                                 "'" & InParam.Type & "', " &
                                                 "'" & InParam.Title & "', " &
                                                 "'" & InParam.Description & "', " &
                                                 "#" & InParam.SDate & "#, " &
                                                 "'" & InParam.STime & "', " &
                                                 " " & InParam.Recurrence & ", " &
                                                 " " & InParam.Music & ", " &
                                                 "#" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', #" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', " & Common_Lib.Common.Record_Status._Completed & ", #" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', '" & InParam.Rec_id & "'" &
                                                 ")"
                command.CommandText = STR1 : command.ExecuteNonQuery()
                trans.Commit()
            End Using
        End Sub

        Public Sub Delete(ByVal RecID As String)
            Dim trans As OleDbTransaction = Nothing
            Using Con As New OleDbConnection(cBase._data_ConStr_Sys)
                Con.Open() : Dim command As OleDbCommand = Con.CreateCommand()
                trans = Con.BeginTransaction
                command.Connection = Con
                command.Transaction = trans
                Dim STR1 As String = " UPDATE REMINDER_INFO SET " &
                                      "REC_STATUS        =" & Common_Lib.Common.Record_Status._Deleted & "," &
                                      "REC_STATUS_ON     =#" & Now.ToString(cBase._Date_Format_Long) & "#," &
                                      "REC_STATUS_BY     ='" & cBase._open_User_ID & "'  " &
                                      "  WHERE REC_ID    ='" & RecID & "'"
                command.CommandText = STR1 : command.ExecuteNonQuery()
                trans.Commit()
            End Using
        End Sub
    End Class
#End Region
End Class
