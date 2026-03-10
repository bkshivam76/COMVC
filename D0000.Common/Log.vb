Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.IO
<Serializable>
Public Class Log
    Public Enum LogMode
        Console
        File
    End Enum
    Public Enum LogType
        [Error]
        Info
        Important
        VeryImportant
    End Enum
    Public Enum LogSuffix
        Reminder
        ClientApplication
        HO
        WebService
        TrafficCtrl
        SilentSync
    End Enum

    Public Shared LogPath As String = "C:\ConnectOne\Logs\"

    ''' <summary>
    ''' Registers Log info, as per specified mode
    ''' </summary>
    Public Shared Sub Write(ByVal LType As LogType, ByVal [Class] As String, ByVal Method As String, ByVal Message As String, ByVal currLogSuffix As LogSuffix)
        If currLogSuffix = LogSuffix.WebService OrElse currLogSuffix = LogSuffix.HO Then
            Return
        End If
        Dim _allowInfoLogs As [Boolean] = False
        Try
            Dim Base As New Common_Lib.Common()
            _allowInfoLogs = Base._allowInfoLogs
        Catch ex As Exception
        End Try

        If _allowInfoLogs = False AndAlso LType.Equals(LogType.Info) Then
            Return
        End If
        'skip writing info logs 
        Dim LogModeUsed As LogMode = LogMode.File
        Select Case LogModeUsed
            Case LogMode.Console
                Console.WriteLine(LType.ToString() & " - " & DateTime.Now.ToString() & ":")
                If LType = LogType.[Error] Then
                    Console.WriteLine("Application has experienced an exception in " & [Class] & "." & Method & " : " & Message)
                Else
                    Console.WriteLine("Process Info : " & [Class] & "." & Method & " : " & Message)
                End If
                Exit Select

                'TODO:To be Removed
                Console.WriteLine(LType.ToString() & " - " & DateTime.Now.ToString() & ":")
                If LType = LogType.[Error] Then
                    Console.WriteLine("Application has experienced an exception in " & [Class] & "." & Method & " : " & Message)
                ElseIf LType = LogType.Important Then
                    Console.WriteLine("Important Info : " & [Class] & "." & Method & " : " & Message)
                Else
                    Console.WriteLine("Process Info : " & [Class] & "." & Method & " : " & Message)
                End If
                'End:TODO

            Case LogMode.File
                Dim FileLocation As String
                Dim FolderLocation As String = LogPath
                If currLogSuffix = LogSuffix.WebService Then
                    FileLocation = FolderLocation & DateTime.Today.Day.ToString() & DateTime.Today.Month.ToString() & DateTime.Today.Year.ToString() & "_" & Guid.NewGuid().ToString() & ".txt"
                Else
                    FileLocation = FolderLocation & DateTime.Today.Day.ToString() & DateTime.Today.Month.ToString() & DateTime.Today.Year.ToString() & "_" & currLogSuffix.ToString() & ".txt"
                End If

                If Not System.IO.Directory.Exists(FolderLocation) Then
                    System.IO.Directory.CreateDirectory(FolderLocation)
                End If
                'if (!System.IO.Directory.Exists(SubFolderLocation)) System.IO.Directory.CreateDirectory(SubFolderLocation);
                Dim fsSQLLog As FileStream
                If Not System.IO.File.Exists(FileLocation) Then
                    fsSQLLog = File.Create(FileLocation)
                    fsSQLLog.Flush()
                    fsSQLLog.Close()
                End If

                fsSQLLog = File.Open(FileLocation, FileMode.Append, FileAccess.Write, FileShare.Read)
                Dim writer As New StreamWriter(fsSQLLog)
                writer.WriteLine(LType.ToString() & " - " & DateTime.Now.ToString() & ":")
                If LType = LogType.[Error] Then
                    writer.WriteLine("Application has experienced an exception in " & [Class] & "." & Method & " : " & Message)
                Else
                    writer.WriteLine("Process Info : " & [Class] & "." & Method & " : " & Message)
                End If
                writer.Close()
                Exit Select
        End Select
    End Sub
End Class
