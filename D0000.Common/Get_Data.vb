Imports System.Data
Imports System.Data.OleDb

Public Class Get_Data

    Public _Object As New Common_Lib.Common

    Public _dc_Connection As New OleDbConnection
    Public _dc_Adapter As OleDbDataAdapter
    Public _dc_DataSet As New DataSet
    Public _dc_DataTable As New DataTable
    Public _dc_DataTable_Name As String
    Public _dc_Command As OleDbCommand = _dc_Connection.CreateCommand()

    Public Sub New(ByVal BaseObject As Common_Lib.Common, ByVal _data_Access_From As String, ByVal _table_Name As String, ByVal _sql_Query As String)
        _Object = BaseObject
        _Object.Get_Configure_Setting()
        If _data_Access_From.ToUpper.Trim = "CORE" Then
            _dc_Connection.ConnectionString = _Object._data_ConStr_Core
        ElseIf _data_Access_From.ToUpper.Trim = "SYS" Then
            _dc_Connection.ConnectionString = _Object._data_ConStr_Sys
        ElseIf _data_Access_From.ToUpper.Trim = "DATA" Then
            _dc_Connection.ConnectionString = _Object._data_ConStr_Data
        ElseIf _data_Access_From.ToUpper.Trim = "TEMPLATE" Then
            _dc_Connection.ConnectionString = _Object._data_ConStr_Template
        Else
            Me._dc_DataTable = Nothing
            Exit Sub
        End If
        Me._dc_Connection.Open()
        Me._dc_Adapter = New OleDbDataAdapter(_sql_Query, Me._dc_Connection)
        Me._dc_Adapter.Fill(Me._dc_DataSet, _table_Name)
        Me._dc_DataTable = Me._dc_DataSet.Tables(_table_Name)
        Me._dc_DataTable_Name = _table_Name
    End Sub

    Public Sub Save()
        Dim cmdb As New OleDbCommandBuilder(Me._dc_Adapter)
        Me._dc_Adapter.Update(Me._dc_DataSet, Me._dc_DataTable_Name)
    End Sub

End Class
