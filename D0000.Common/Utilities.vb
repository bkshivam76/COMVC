Imports System.Xml
Imports System.Data
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Diagnostics

Namespace Utility

#Region "INI_FileTask  ......................."
    'How to Use

    'Dim INI_FileTask As New Utility.INI_FileTask("TestINI_FileTask.ini")
    '   'Write a int32 value
    '			INI_FileTask.WriteValue("section1", "key1", 42)
    '   'Write a string value
    '			INI_FileTask.WriteValue("section1", "key2", "This is a test")
    '   'Write a double value
    '			INI_FileTask.WriteValue("section2", "key3", 16.84)


    '   'Read section/key names
    '   Dim sections As String() = INI_FileTask.GetSectionNames()
    '			For Each section As String In sections
    '				Console.WriteLine("[" & section & "]")
    '   Dim keys As String() = INI_FileTask.GetKeyNames(section)
    '                For Each key As String In keys
    '                    Console.WriteLine(key)
    '                Next
    '			Next

    '   'Read int32 value.
    '   Dim value1 As Integer = INI_FileTask.GetInt32("section1", "key1", 0)
    '			Console.WriteLine("key1 = " & value1)
    '   'Read string value.
    '   Dim value2 As String = INI_FileTask.GetString("section1", "key2", "test")
    '			Console.WriteLine("key2 = " & value2)
    '   'Read double value.
    '   Dim value3 As Double = INI_FileTask.GetDouble("section2", "key3", 0)
    '			Console.WriteLine("key3 = " & value3)


    '   'Delete value key2
    '			Console.WriteLine("Deleting section1/key2")
    '			INI_FileTask.DeleteKey("section1", "key2")

    '            value2 = INI_FileTask.GetString("section1", "key2", "")
    '			Console.WriteLine("key2=" & value2)

    '   'Delete section2
    '			Console.WriteLine("Deleting section2")
    '			INI_FileTask.DeleteSection("section2")

    '			value3 = INI_FileTask.GetDouble("section2", "key3", 0)
    '			Console.WriteLine("key3=" & value3)
    <Serializable>
    Public Class INI_FileTask

#Region "Public Declaration"

        Public Const MaxSectionSize As Integer = 32767
        Private m_path As String

        <System.Security.SuppressUnmanagedCodeSecurity()>
        <Serializable>
        Private NotInheritable Class NativeMethods
            Private Sub New()
            End Sub
            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileSectionNames(ByVal lpszReturnBuffer As IntPtr, ByVal nSize As UInteger, ByVal lpFileName As String) As Integer
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As UInteger
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, <[In](), Out()> ByVal _
         lpReturnedString As Char(), ByVal nSize As Integer, ByVal lpFileName As String) As UInteger
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As IntPtr, ByVal nSize As UInteger, ByVal lpFileName As String) As Integer
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileInt(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As Integer, ByVal lpFileName As String) As Integer
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
            Public Shared Function GetPrivateProfileSection(ByVal lpAppName As String, ByVal lpReturnedString As IntPtr, ByVal nSize As UInteger, ByVal lpFileName As String) As Integer
            End Function

            <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
            Public Shared Function WritePrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Boolean
            End Function

        End Class

        Public Sub New(ByVal path As String)
            m_path = System.IO.Path.GetFullPath(path)
        End Sub
        Public ReadOnly Property Path() As String
            Get
                Return m_path
            End Get
        End Property

#End Region

#Region "Get Value Methods"
        Public Function GetString(ByVal sectionName As String, ByVal keyName As String, ByVal defaultValue As String) As String
            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            If keyName Is Nothing Then
                Throw New ArgumentNullException("keyName")
            End If

            Dim retval As New StringBuilder(INI_FileTask.MaxSectionSize)

            NativeMethods.GetPrivateProfileString(sectionName, keyName, defaultValue, retval, INI_FileTask.MaxSectionSize, m_path)

            Return retval.ToString()
        End Function
        Public Function GetInt16(ByVal sectionName As String, ByVal keyName As String, ByVal defaultValue As Short) As Integer
            Dim retval As Integer = GetInt32(sectionName, keyName, defaultValue)

            Return Convert.ToInt16(retval)
        End Function
        Public Function GetInt32(ByVal sectionName As String, ByVal keyName As String, ByVal defaultValue As Integer) As Integer
            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            If keyName Is Nothing Then
                Throw New ArgumentNullException("keyName")
            End If


            Return NativeMethods.GetPrivateProfileInt(sectionName, keyName, defaultValue, m_path)
        End Function
        Public Function GetDouble(ByVal sectionName As String, ByVal keyName As String, ByVal defaultValue As Double) As Double
            Dim retval As String = GetString(sectionName, keyName, "")

            If retval Is Nothing OrElse retval.Length = 0 Then
                Return defaultValue
            End If

            Return Convert.ToDouble(retval, CultureInfo.InvariantCulture)
        End Function
#End Region

#Region "GetSectionValues Methods"

        Public Function GetSectionValuesAsList(ByVal sectionName As String) As List(Of KeyValuePair(Of String, String))
            Dim retval As List(Of KeyValuePair(Of String, String))
            Dim keyValuePairs As String()
            Dim key As String, value As String
            Dim equalSignPos As Integer

            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            'Allocate a buffer for the returned section names.
            Dim ptr As IntPtr = Marshal.AllocCoTaskMem(INI_FileTask.MaxSectionSize)

            Try
                'Get the section key/value pairs into the buffer.
                Dim len As Integer = NativeMethods.GetPrivateProfileSection(sectionName, ptr, INI_FileTask.MaxSectionSize, m_path)

                keyValuePairs = ConvertNullSeperatedStringToStringArray(ptr, len)
            Finally
                'Free the buffer
                Marshal.FreeCoTaskMem(ptr)
            End Try

            'Parse keyValue pairs and add them to the list.
            retval = New List(Of KeyValuePair(Of String, String))(keyValuePairs.Length)

            For i As Integer = 0 To keyValuePairs.Length - 1
                'Parse the "key=value" string into its constituent parts
                equalSignPos = keyValuePairs(i).IndexOf("="c)

                key = keyValuePairs(i).Substring(0, equalSignPos)

                value = keyValuePairs(i).Substring(equalSignPos + 1, keyValuePairs(i).Length - equalSignPos - 1)

                retval.Add(New KeyValuePair(Of String, String)(key, value))
            Next

            Return retval
        End Function
        Public Function GetSectionValues(ByVal sectionName As String) As Dictionary(Of String, String)
            Dim keyValuePairs As List(Of KeyValuePair(Of String, String))
            Dim retval As Dictionary(Of String, String)

            keyValuePairs = GetSectionValuesAsList(sectionName)

            'Convert list into a dictionary.
            retval = New Dictionary(Of String, String)(keyValuePairs.Count)

            For Each keyValuePair As KeyValuePair(Of String, String) In keyValuePairs
                'Skip any key we have already seen.
                If Not retval.ContainsKey(keyValuePair.Key) Then
                    retval.Add(keyValuePair.Key, keyValuePair.Value)
                End If
            Next

            Return retval
        End Function

#End Region

#Region "Get Key/Section Names"

        Public Function GetKeyNames(ByVal sectionName As String) As String()
            Dim len As Integer
            Dim retval As String()

            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            'Allocate a buffer for the returned section names.
            Dim ptr As IntPtr = Marshal.AllocCoTaskMem(INI_FileTask.MaxSectionSize)

            Try
                'Get the section names into the buffer.
                len = NativeMethods.GetPrivateProfileString(sectionName, Nothing, Nothing, ptr, INI_FileTask.MaxSectionSize, m_path)

                retval = ConvertNullSeperatedStringToStringArray(ptr, len)
            Finally
                'Free the buffer
                Marshal.FreeCoTaskMem(ptr)
            End Try

            Return retval
        End Function
        Public Function GetSectionNames() As String()
            Dim retval As String()
            Dim len As Integer

            'Allocate a buffer for the returned section names.
            Dim ptr As IntPtr = Marshal.AllocCoTaskMem(INI_FileTask.MaxSectionSize)

            Try
                'Get the section names into the buffer.
                len = NativeMethods.GetPrivateProfileSectionNames(ptr, INI_FileTask.MaxSectionSize, m_path)

                retval = ConvertNullSeperatedStringToStringArray(ptr, len)
            Finally
                'Free the buffer
                Marshal.FreeCoTaskMem(ptr)
            End Try

            Return retval
        End Function
        Private Shared Function ConvertNullSeperatedStringToStringArray(ByVal ptr As IntPtr, ByVal valLength As Integer) As String()
            Dim retval As String()

            If valLength = 0 Then
                'Return an empty array.
                retval = New String(-1) {}
            Else
                'Convert the buffer into a string.  Decrease the length 
                'by 1 so that we remove the second null off the end.
                Dim buff As String = Marshal.PtrToStringAuto(ptr, valLength - 1)

                'Parse the buffer into an array of strings by searching for nulls.
                retval = buff.Split(Chr(0))
            End If

            Return retval
        End Function

#End Region

#Region "Write Methods"

        Private Sub WriteValueInternal(ByVal sectionName As String, ByVal keyName As String, ByVal value As String)
            If Not NativeMethods.WritePrivateProfileString(sectionName, keyName, value, m_path) Then
                Throw New System.ComponentModel.Win32Exception()
            End If
        End Sub
        Public Sub WriteValue(ByVal sectionName As String, ByVal keyName As String, ByVal value As String)
            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            If keyName Is Nothing Then
                Throw New ArgumentNullException("keyName")
            End If

            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If

            WriteValueInternal(sectionName, keyName, value)
        End Sub
        Public Sub WriteValue(ByVal sectionName As String, ByVal keyName As String, ByVal value As Short)
            WriteValue(sectionName, keyName, CInt(value))
        End Sub
        Public Sub WriteValue(ByVal sectionName As String, ByVal keyName As String, ByVal value As Integer)
            WriteValue(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture))
        End Sub
        Public Sub WriteValue(ByVal sectionName As String, ByVal keyName As String, ByVal value As Single)
            WriteValue(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture))
        End Sub
        Public Sub WriteValue(ByVal sectionName As String, ByVal keyName As String, ByVal value As Double)
            WriteValue(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture))
        End Sub

#End Region

#Region "Delete Methods"

        Public Sub DeleteKey(ByVal sectionName As String, ByVal keyName As String)
            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            If keyName Is Nothing Then
                Throw New ArgumentNullException("keyName")
            End If

            WriteValueInternal(sectionName, keyName, Nothing)
        End Sub
        Public Sub DeleteSection(ByVal sectionName As String)
            If sectionName Is Nothing Then
                Throw New ArgumentNullException("sectionName")
            End If

            WriteValueInternal(sectionName, Nothing, Nothing)
        End Sub

#End Region

    End Class

#End Region

End Namespace



